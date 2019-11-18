using Google.Cloud.TextToSpeech.V1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace wow_quest_to_speech {
	public static class GoogleTTS {
		public static string GetVoices(ref QuestToSpeech.Voice[] voices) {
			try {
				List<QuestToSpeech.Voice> list = new List<QuestToSpeech.Voice>();
				TextToSpeechClient client = TextToSpeechClient.Create();
				ListVoicesResponse response = client.ListVoices(new ListVoicesRequest() {}); // Performs the list voices request

				foreach (Voice voice in response.Voices) {
					list.Add(new QuestToSpeech.Voice() {
						Name = voice.Name,
						Gender = voice.SsmlGender == SsmlVoiceGender.Female ? QuestToSpeech.Gender.Female : QuestToSpeech.Gender.Male,
						LangCode = voice.LanguageCodes[0],
						Module = QuestToSpeech.Module.Google
					});
				}

				voices = list.ToArray();
			} catch(Exception ex) {
				return string.Format("GoogleTTS Exception: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
			}

			return null;
		}

		public static string Speak(string txt, QuestToSpeech.Voice voice, string filePath) {
			try {
				TextToSpeechClient client = TextToSpeechClient.Create();

				SynthesizeSpeechResponse res = client.SynthesizeSpeech(new SynthesizeSpeechRequest {
					Input = new SynthesisInput {
						Text = txt
					},
					Voice = new VoiceSelectionParams {
						Name = voice.Name,
						LanguageCode = voice.LangCode,
						SsmlGender = voice.Gender == QuestToSpeech.Gender.Female ? SsmlVoiceGender.Female : SsmlVoiceGender.Male
					},
					AudioConfig = new AudioConfig {
						AudioEncoding = AudioEncoding.Linear16
					}
				});

				using (FileStream output = File.Create(filePath)) {
					res.AudioContent.WriteTo(output);
				}
			} catch (Exception ex) {
				return string.Format("GoogleTTS Exception: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
			}

			return null;
		}
	}
}
