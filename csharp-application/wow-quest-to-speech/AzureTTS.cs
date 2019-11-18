using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace wow_quest_to_speech {
	public static class AzureTTS {
		public static string GetVoices(ref QuestToSpeech.Voice[] voices, AzureAPIConfig config) {
			string[] voicesStr = new string[] {
				"ar-EG-Hoda;Female",
				"ar-SA-Naayf;Male",
				"bg-BG-Ivan;Male",
				"ca-ES-HerenaRUS;Female",
				"cs-CZ-Jakub;Male",
				"da-DK-HelleRUS;Female",
				"de-AT-Michael;Male",
				"de-CH-Karsten;Male",
				"de-DE-Hedda;Female",
				"de-DE-HeddaRUS;Female",
				"de-DE-KatjaNeural;Female",
				"de-DE-Stefan-Apollo;Male",
				"el-GR-Stefanos;Male",
				"en-AU-Catherine;Female",
				"en-AU-HayleyRUS;Female",
				"en-CA-Linda;Female",
				"en-CA-HeatherRUS;Female",
				"en-GB-Susan-Apollo;Female",
				"en-GB-HazelRUS;Female",
				"en-GB-George-Apollo;Male",
				"en-IE-Sean;Male",
				"en-IN-Heera-Apollo;Female",
				"en-IN-PriyaRUS;Female",
				"en-IN-Ravi-Apollo;Male",
				"en-US-ZiraRUS;Female",
				"en-US-JessaRUS;Female",
				"en-US-Jessa24kRUS;Female",
				"en-US-JessaNeural;Female",
				"en-US-BenjaminRUS;Male",
				"en-US-GuyNeural;Male",
				"en-US-Guy24kRUS;Male",
				"es-ES-Laura-Apollo;Female",
				"es-ES-HelenaRUS;Female",
				"es-ES-Pablo-Apollo;Male",
				"es-MX-HildaRUS;Female",
				"es-MX-Raul-Apollo;Male",
				"fi-FI-HeidiRUS;Female",
				"fr-CA-Caroline;Female",
				"fr-CA-HarmonieRUS;Female",
				"fr-CH-Guillaume;Male",
				"fr-FR-Julie-Apollo;Female",
				"fr-FR-HortenseRUS;Female",
				"fr-FR-Paul-Apollo;Male",
				"he-IL-Asaf;Male",
				"hi-IN-Kalpana-Apollo;Female",
				"hi-IN-Kalpana;Female",
				"hi-IN-Hemant;Male",
				"hr-HR-Matej;Male",
				"hu-HU-Szabolcs;Male",
				"id-ID-Andika;Male",
				"it-IT-Cosimo-Apollo;Male",
				"it-IT-ElsaNeural;Female",
				"it-IT-LuciaRUS;Female",
				"ja-JP-Ayumi-Apollo;Female",
				"ja-JP-Ichiro-Apollo;Male",
				"ja-JP-HarukaRUS;Female",
				"ko-KR-HeamiRUS;Female",
				"ms-MY-Rizwan;Male",
				"nb-NO-HuldaRUS;Female",
				"nl-NL-HannaRUS;Female",
				"pl-PL-PaulinaRUS;Female",
				"pt-BR-HeloisaRUS;Female",
				"pt-BR-Daniel-Apollo;Male",
				"pt-PT-HeliaRUS;Female",
				"ro-RO-Andrei;Male",
				"ru-RU-Irina-Apollo;Female",
				"ru-RU-Pavel-Apollo;Male",
				"ru-RU-EkaterinaRUS;Female",
				"sk-SK-Filip;Male",
				"sl-SI-Lado;Male",
				"sv-SE-HedvigRUS;Female",
				"ta-IN-Valluvar;Male",
				"te-IN-Chitra;Female",
				"th-TH-Pattara;Male",
				"tr-TR-SedaRUS;Female",
				"vi-VN-An;Male",
				"zh-CN-HuihuiRUS;Female",
				"zh-CN-Yaoyao-Apollo;Female",
				"zh-CN-Kangkang-Apollo;Male",
				"zh-CN-XiaoxiaoNeural;Female",
				"zh-HK-Tracy-Apollo;Female",
				"zh-HK-TracyRUS;Female",
				"zh-HK-Danny-Apollo;Male",
				"zh-TW-Yating-Apollo;Female",
				"zh-TW-HanHanRUS;Female",
				"zh-TW-Zhiwei-Apollo;Male"
			};

			List<QuestToSpeech.Voice> list = new List<QuestToSpeech.Voice>();

			foreach(string s in voicesStr) {
				string[] sp = s.Split(';');

				list.Add(new QuestToSpeech.Voice() {
					Name = sp[0],
					Gender = sp[1] == "Female" ? QuestToSpeech.Gender.Female : QuestToSpeech.Gender.Male,
					LangCode = sp[0].Substring(0, 5),
					Module = QuestToSpeech.Module.Azure
				});
			}

			voices = list.ToArray();

			return null;
		}

		public static string Speak(string txt, QuestToSpeech.Voice voice, string filePath, AzureAPIConfig config) {
			try {
				SpeakAsync(txt, voice, filePath, config).Wait();
			} catch(Exception ex) {
				return string.Format("AzureTTS Exception: ", ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
			}

			return null;
		}

		public static async Task SpeakAsync(string txt, QuestToSpeech.Voice voice, string filePath, AzureAPIConfig config) {
			SpeechConfig speechConfig = SpeechConfig.FromSubscription(config.Key, config.Region);
			speechConfig.SpeechSynthesisVoiceName = voice.Name;
			speechConfig.SpeechSynthesisLanguage = voice.LangCode;

			using (AudioConfig fileOutput = AudioConfig.FromWavFileOutput(filePath)) {
				using (SpeechSynthesizer tts = new SpeechSynthesizer(speechConfig, fileOutput)) {
					using (SpeechSynthesisResult result = await tts.SpeakTextAsync(txt)) {
						if (result.Reason == ResultReason.Canceled) {
							var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);

							if (cancellation.Reason == CancellationReason.Error) {
								throw new Exception(string.Format("API Error (Code: {0}): {1}", cancellation.ErrorCode, cancellation.ErrorDetails));
							}
						}
					}
				}
			}
		}
	}
}
