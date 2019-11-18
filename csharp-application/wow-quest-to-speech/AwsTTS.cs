using System;
using System.Collections.Generic;
using System.IO;
using Amazon.Polly;
using Amazon.Polly.Model;
using NAudio.Wave;

namespace wow_quest_to_speech {
	public static class AwsTTS {
		public static string GetVoices(ref QuestToSpeech.Voice[] voices, AwsAPIConfig config) {
			List<QuestToSpeech.Voice> list = new List<QuestToSpeech.Voice>();

			try {
				using (AmazonPollyClient client = new AmazonPollyClient(config.AccessKey, config.SecretKey, Amazon.RegionEndpoint.GetBySystemName(config.Region))) {
					DescribeVoicesRequest req = new DescribeVoicesRequest();
					string nextToken;

					do {
						DescribeVoicesResponse res = client.DescribeVoices(req);
						nextToken = res.NextToken;
						req.NextToken = nextToken;
						
						foreach (var voice in res.Voices) {
							list.Add(new QuestToSpeech.Voice() {
								Name = string.Format("{0}-{1}", voice.LanguageCode, voice.Id.ToString()),
								Gender = voice.Gender == Gender.Female ? QuestToSpeech.Gender.Female : QuestToSpeech.Gender.Male,
								LangCode = voice.LanguageCode,
								Module = QuestToSpeech.Module.AWS
							});
						}
					} while (nextToken != null);

					list.Sort(delegate (QuestToSpeech.Voice v1, QuestToSpeech.Voice v2) {
						return v1.Name.CompareTo(v2.Name);
					});

					voices = list.ToArray();
				}
			} catch (Exception ex) {
				return ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
			}

			return null;
		}

		public static string Speak(string txt, QuestToSpeech.Voice voice, string filePath, AwsAPIConfig config) {

			try {
				using (AmazonPollyClient client = new AmazonPollyClient(config.AccessKey, config.SecretKey, Amazon.RegionEndpoint.GetBySystemName(config.Region))) {
					string nameWoLang = voice.Name.Replace(voice.LangCode + "-", "");

					SynthesizeSpeechRequest req = new SynthesizeSpeechRequest() {
						OutputFormat = OutputFormat.Mp3,
						LanguageCode = voice.LangCode,
						VoiceId = (VoiceId)typeof(VoiceId).GetField(nameWoLang).GetValue(null),
						Text = txt
					};

					using (FileStream fileStream = new FileStream(filePath + ".mp3", FileMode.Create, FileAccess.Write)) {
						SynthesizeSpeechResponse res = client.SynthesizeSpeech(req);
						byte[] buffer = new byte[2 * 1024];
						int readBytes;

						using (Stream audioStream = res.AudioStream) {
							while ((readBytes = audioStream.Read(buffer, 0, 2 * 1024)) > 0) {
								fileStream.Write(buffer, 0, readBytes);
							}
						}
					}

					// convert to wav and delete mp3
					if (File.Exists(filePath + ".mp3")) {
						using (Mp3FileReader mp3 = new Mp3FileReader(filePath + ".mp3")) {
							using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3)) {
								WaveFileWriter.CreateWaveFile(filePath, pcm);
							}
						}

						File.Delete(filePath + ".mp3");
					}
				}
			} catch (Exception ex) {
				return ex.InnerException == null ? ex.Message : ex.InnerException.ToString();
			}

			return null;
		}
	}
}
