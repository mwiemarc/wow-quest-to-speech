using System;

namespace wow_quest_to_speech {
	[Serializable]
	public class QuestToSpeechConfig {
		public QuestToSpeech.Module[] EnabledModules { get; set; }
		public QuestToSpeech.Voice[] SelectedVoices { get; set; }
		public AzureAPIConfig AzureAPI { get; set; }
		public AwsAPIConfig AwsAPI { get; set; }

		public QuestToSpeechConfig() { // set defaults
			EnabledModules = new QuestToSpeech.Module[] { QuestToSpeech.Module.Windows };
			SelectedVoices = new QuestToSpeech.Voice[0];
			AzureAPI = null;
			AwsAPI = null;
		}
	}

	public class AzureAPIConfig {
		public string Key { get; set; }
		public string Region { get; set; }
	}

	public class AwsAPIConfig {
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
		public string Region { get; set; }
	}
}
