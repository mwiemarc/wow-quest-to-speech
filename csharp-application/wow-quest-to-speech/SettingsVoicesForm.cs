using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace wow_quest_to_speech {
	public partial class SettingsVoicesForm : Form {
		public QuestToSpeech.Voice[] selectedVoices = new QuestToSpeech.Voice[0];

		private AzureAPIConfig azureAPIConfig;
		private AwsAPIConfig awsAPIConfig;
		private SoundPlayer player = null;
		private QuestToSpeech.Voice[] voices;
		private List<QuestToSpeech.Voice> tmpSelectedList = new List<QuestToSpeech.Voice>();
		private string testString;
		private bool drawn = false;

		public SettingsVoicesForm(QuestToSpeech.Voice[] allVoices, QuestToSpeech.Voice[] preSelected, AzureAPIConfig azureConfig, AwsAPIConfig awsConfig) {
			InitializeComponent();

			azureAPIConfig = azureConfig;
			awsAPIConfig = awsConfig;

			voices = allVoices;
			tmpSelectedList.AddRange(preSelected);

			lblNumTotalSelected.Text = string.Format("(Total selected {0})", tmpSelectedList.Count);

			// insert voices
			foreach (QuestToSpeech.Voice v in voices) {
				if(!comboLangCodes.Items.Contains(v.LangCode))
					comboLangCodes.Items.Add(v.LangCode);
			}
		}

		private void SettingsVoicesForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (player != null) {
				player.Stop();
				player.Dispose();
			}
		}

		private void comboLangCodes_SelectedIndexChanged(object sender, EventArgs e) {
			drawn = false;

			listVoices.Items.Clear();

			string langCode = comboLangCodes.SelectedItem.ToString();
			//QuestToSpeech.Voice x = (QuestToSpeech.Voice)Array.Find(tmpSelectedList.ToArray(), delegate (QuestToSpeech.Voice v) { return v.LangCode == langCode; }); //if (x != null)

			foreach (QuestToSpeech.Voice v in voices) { // insert voices by selected lang code
				if (v.LangCode == langCode) {
					listVoices.Items.Add(v.Name).SubItems.AddRange(new string[] { v.Gender.ToString(), v.Module.ToString(), v.LangCode });

					if (tmpSelectedList.Exists(v1 => QuestToSpeech.IsVoice(v1, v)))
						listVoices.Items[listVoices.Items.Count - 1].Checked = true;
				}
			}

			drawn = true;
		}

		private void listVoices_ItemChecked(object sender, ItemCheckedEventArgs e) {
			//QuestToSpeech.Voice s = Array.Find(tmpSelectedList.ToArray(), delegate (QuestToSpeech.Voice tv) { return (tv.Gender == v.Gender && tv.Name == v.Name && tv.LangCode == v.LangCode && tv.Module == v.Module); });

			if (drawn) {
				QuestToSpeech.Voice voice = new QuestToSpeech.Voice() {
					Name = e.Item.SubItems[0].Text,
					Gender = e.Item.SubItems[1].Text == "Female" ? QuestToSpeech.Gender.Female : QuestToSpeech.Gender.Male,
					LangCode = e.Item.SubItems[3].Text,
					Module = QuestToSpeech.GetModule(e.Item.SubItems[2].Text)
				};

				if (e.Item.Checked) {
					if (!tmpSelectedList.Exists(vx => QuestToSpeech.IsVoice(vx, voice)))
						tmpSelectedList.Add(voice);
				} else {
					if (tmpSelectedList.Exists(vx => QuestToSpeech.IsVoice(vx, voice)))
						tmpSelectedList.Remove(tmpSelectedList.Find(vx => QuestToSpeech.IsVoice(vx, voice)));
				}
			}

			lblNumTotalSelected.Text = string.Format("(Total selected {0})", tmpSelectedList.Count);
		}

		private void btnSave_Click(object sender, EventArgs e) {
			selectedVoices = tmpSelectedList.ToArray();
		}

		private void btnTestVoice_Click(object sender, EventArgs e) {
			if (listVoices.SelectedItems.Count > 0) {
				ListViewItem i = listVoices.SelectedItems[0];

				testString = Prompt.InputDialog(string.Format("Voice: {2}, {0} ({1})", i.SubItems[0].Text, i.SubItems[1].Text, i.SubItems[2].Text), "Enter a sentence to speak", testString == null ? "" : testString);
				
				if(!string.IsNullOrWhiteSpace(testString)) {
					QuestToSpeech.Voice v = new QuestToSpeech.Voice() {
						Name = i.SubItems[0].Text,
						Gender = i.SubItems[1].Text == "Female" ? QuestToSpeech.Gender.Female : QuestToSpeech.Gender.Male,
						LangCode = i.SubItems[3].Text,
						Module = QuestToSpeech.GetModule(i.SubItems[2].Text)
					};

					Thread t = new Thread(() => VoiceTestSpeak(testString, v));
					t.Start();
				}
			} else {
				MessageBox.Show("No row selected");
			}
		}

		private void VoiceTestSpeak(string txt, QuestToSpeech.Voice voice) {
			string err = null;
			string fPath = Path.Combine(QuestToSpeech.TempDirectory, string.Format("test-{0}.wav", Util.MD5Hash(testString + voice.Name + voice.Module)));

			if (!File.Exists(fPath)) {
				if (voice.Module == QuestToSpeech.Module.Windows) {
					err = WindowsTTS.Speak(testString, voice, fPath);
				} else if (voice.Module == QuestToSpeech.Module.Azure) {
					err = AzureTTS.Speak(testString, voice, fPath, azureAPIConfig);
				} else if (voice.Module == QuestToSpeech.Module.Google) {
					err = GoogleTTS.Speak(testString, voice, fPath);
				} else if (voice.Module == QuestToSpeech.Module.AWS) {
					err = AwsTTS.Speak(testString, voice, fPath, awsAPIConfig);
				}
			}

			if (err != null) {
				MessageBox.Show(err);
				return;
			}

			if (File.Exists(fPath)) {
				if(player != null) {
					player.Stop();
					player.Dispose();
				}

				player = new SoundPlayer(fPath);
				player.Play();
			}
		}
	}
}
