using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace wow_quest_to_speech {
	public static class WindowsTTS {
		public static string GetVoices(ref QuestToSpeech.Voice[] voices) {
			try {
				List<QuestToSpeech.Voice> list = new List<QuestToSpeech.Voice>();

				using (SpeechSynthesizer tts = new SpeechSynthesizer()) {
					foreach (InstalledVoice v in tts.GetInstalledVoices()) {
						list.Add(new QuestToSpeech.Voice() {
							Name = v.VoiceInfo.Name,
							Gender = v.VoiceInfo.Gender == VoiceGender.Female ? QuestToSpeech.Gender.Female : QuestToSpeech.Gender.Male,
							LangCode = v.VoiceInfo.Culture.Name,
							Module = QuestToSpeech.Module.Windows
						});
					}
				}

				voices = list.ToArray();
			} catch(Exception ex) {
				return string.Format("WindowsTTS Exception: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
			}

			return null;
		}

		public static string Speak(string txt, QuestToSpeech.Voice voice, string filePath) {
			try {
				using (SpeechSynthesizer tts = new SpeechSynthesizer()) {
					tts.SelectVoice(voice.Name);
					tts.SetOutputToWaveFile(filePath, new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
					tts.Speak(txt);
				}
			} catch (Exception ex) {
				return string.Format("WindowsTTS Exception: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
			}

			return null;
		}
	}
}
