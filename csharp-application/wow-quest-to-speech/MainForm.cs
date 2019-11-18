using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace wow_quest_to_speech {
	public partial class MainForm : Form {
		[DllImport("User32.dll")]
		protected static extern int SetClipboardViewer(int hWndNewViewer);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		private IntPtr nextClipboardViewer;
		private QuestToSpeech qts = null;

		public MainForm() {
			InitializeComponent();
			nextClipboardViewer = (IntPtr)SetClipboardViewer((int)Handle); // save clipboard handler

			txtActionLog.Text = string.Format("{0}[Loading]\r\n", txtActionLog.Text);

			qts = new QuestToSpeech();
			qts.Started += Qts_Started;
			qts.Stopped += Qts_Stopped;
			qts.TextAdded += Qts_TextAdded;
			qts.SpeechPrepare += Qts_SpeechPrepare;
			qts.SpeechStarted += Qts_SpeechStarted;
			qts.SpeechIdle += Qts_SpeechIdle;
			qts.Error += Qts_Error;
		}

		private void WTSMainForm_Load(object sender, EventArgs e) {
			checkModuleWindows.Checked = qts.IsModuleEnabled(QuestToSpeech.Module.Windows);
			checkModuleAzure.Checked = qts.IsModuleEnabled(QuestToSpeech.Module.Azure);
			checkModuleGoogle.Checked = qts.IsModuleEnabled(QuestToSpeech.Module.Google);
			checkModuleAws.Checked = qts.IsModuleEnabled(QuestToSpeech.Module.AWS);

			txtActionLog.Text = string.Format("{0}[Ready]\r\n", txtActionLog.Text);

			// create some tooltips
			//tipFallbackVoice.SetToolTip(comboFallbackVoice, "This voice will be used if no gender is detected");
			//tipAzuApiRegion.SetToolTip(txtAzuTtsApiRegion, "API-Endpoint where calls should be made at (e.g. westeurope or eastus)");
		}

		private void WTSMainForm_FormClosing(object sender, FormClosingEventArgs e) {
			qts.Stop();
			qts = null;
			ChangeClipboardChain(Handle, nextClipboardViewer); // restore default handler
			base.OnClosing(e);
		}

		private void WTSMainForm_Resize(object sender, EventArgs e) { // minimize to tray		
			if (WindowState == FormWindowState.Minimized) {
				Hide();
				trayIcon.Visible = true;
			}
		}

		private void trayIcon_DoubleClick(object sender, EventArgs e) { // restore from tray		
			Show(); 
			WindowState = FormWindowState.Normal;
			trayIcon.Visible = false;
		}

		private void btnStartStop_Click(object sender, EventArgs e) {
			grpModules.Enabled = false;
			grpSettings.Enabled = false;
			btnStartStop.Enabled = false;

			if (qts.IsRunning)
				qts.Stop();
			else
				qts.Start();
		}

		private void btnSettingsVoices_Click(object sender, EventArgs e) {
			using (SettingsVoicesForm f = new SettingsVoicesForm(qts.GetAvailableVoices(), qts.GetSelectedVoices(), qts.GetAzureAPIConfig(), qts.GetAwsAPIConfig())) {
				if (f.ShowDialog() == DialogResult.OK)
					qts.SetSelectedVoices(f.selectedVoices);
			}
		}

		private void btnSettingsApis_Click(object sender, EventArgs e) {
			using (SettingsApisForm f = new SettingsApisForm(qts.GetAzureAPIConfig(), qts.GetAwsAPIConfig())) {
				if (f.ShowDialog() == DialogResult.OK) {
					qts.SetAzureAPIConfig(f.AzureAPIConfig);

					if (f.AzureAPIConfig == null && qts.IsModuleEnabled(QuestToSpeech.Module.Azure)) {
						checkModuleAzure.Checked = false;
					}

					qts.SetAwsAPIConfig(f.AwsAPIConfig);

					if (f.AwsAPIConfig == null && qts.IsModuleEnabled(QuestToSpeech.Module.AWS)) {
						checkModuleAws.Checked = false;
					}
				}
			}
		}

		private void checkModuleWindows_CheckedChanged(object sender, EventArgs e) {
			qts.SetModuleEnabled(QuestToSpeech.Module.Windows, (sender as CheckBox).Checked);
		}

		private void checkModuleAzure_CheckedChanged(object sender, EventArgs e) {
			if (!qts.SetModuleEnabled(QuestToSpeech.Module.Azure, (sender as CheckBox).Checked)) {
				(sender as CheckBox).Checked = false;
				MessageBox.Show("Failed to enable module \"Azure\": Check API settings");
				return;
			}
		}

		private void checkModuleGoogle_CheckedChanged(object sender, EventArgs e) {
			if (!qts.SetModuleEnabled(QuestToSpeech.Module.Google, (sender as CheckBox).Checked)) {
				(sender as CheckBox).Checked = false;
				MessageBox.Show("Failed to enable module \"Google\": Authentication environment variable not set or file not found");
				return;
			}
		}

		private void checkModuleAws_CheckedChanged(object sender, EventArgs e) {
			if (!qts.SetModuleEnabled(QuestToSpeech.Module.AWS, (sender as CheckBox).Checked)) {
				(sender as CheckBox).Checked = false;
				MessageBox.Show("Failed to enable module \"AWS\": Check API settings");
				return;
			}
		}

		private void txtSpeechLog_TextChanged(object sender, EventArgs e) {
			txtActionLog.SelectionStart = txtActionLog.TextLength;
			txtActionLog.ScrollToCaret();
			txtActionLog.Refresh();
		}

		private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://github.com/mwiemarc/wow-quest-to-speech");
		}

		private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://github.com/mwiemarc/wow-quest-to-speech/blob/master/HELP.md");
		}

		private void Qts_Started(object sender, EventArgs e) {
			btnStartStop.Invoke(new Action(() => btnStartStop.Enabled = true));
			btnStartStop.Invoke(new Action(() => btnStartStop.Text = "Stop"));
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[Started]\r\n", txtActionLog.Text)));
		}

		private void Qts_Stopped(object sender, EventArgs e) {
			if (qts == null)
				return;

			grpModules.Invoke(new Action(() => grpModules.Enabled = true));
			grpSettings.Invoke(new Action(() => grpSettings.Enabled = true));
			btnStartStop.Invoke(new Action(() => btnStartStop.Enabled = true));
			btnStartStop.Invoke(new Action(() => btnStartStop.Text = "Start"));
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[Stopped]\r\n", txtActionLog.Text)));
		}

		private void Qts_TextAdded(object sender, string gender) {
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[TextAdded] {1}\r\n", txtActionLog.Text, gender)));
		}

		private void Qts_SpeechIdle(object sender, EventArgs e) {
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[Idle]\r\n", txtActionLog.Text)));
		}

		private void Qts_SpeechStarted(object sender, string msg) {
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[Play] {1}\r\n", txtActionLog.Text, msg)));
		}

		private void Qts_SpeechPrepare(object sender, string msg) {
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[Process] {1}\r\n", txtActionLog.Text, msg)));
		}

		private void Qts_Error(object sender, string err) {
			Console.WriteLine("[Error] {0}", err);

			Invoke(new Action(() => MessageBox.Show(err)));
			txtActionLog.Invoke(new Action(() => txtActionLog.Text = string.Format("{0}[Error] {1}\r\n", txtActionLog.Text, err)));
		}

		protected override void WndProc(ref Message m) {
			const int WM_DRAWCLIPBOARD = 0x308;
			const int WM_CHANGECBCHAIN = 0x030D;

			switch (m.Msg) {
				case WM_DRAWCLIPBOARD:
					IDataObject dataObj = Clipboard.GetDataObject();

					if (qts != null && dataObj.GetDataPresent(DataFormats.Text)) // is string
						qts.ClipboardTextRecieved(dataObj.GetData(DataFormats.Text) as string);

					SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam); // pass event
					break;
				case WM_CHANGECBCHAIN:
					if (m.WParam == nextClipboardViewer)
						nextClipboardViewer = m.LParam;
					else
						SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam); // pass event
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
	}
}
