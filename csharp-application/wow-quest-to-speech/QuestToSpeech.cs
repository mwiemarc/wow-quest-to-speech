using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;

namespace wow_quest_to_speech {
	public class QuestToSpeech {
		public enum Module {
			Windows,
			Azure,
			Google,
			AWS
		}

		public enum Gender {
			Female,
			Male
		}

		public class Voice {
			public string Name { get; set; }
			public string LangCode { get; set; }
			public Gender Gender { get; set; }
			public Module Module { get; set; }
		}

		public static string TempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");

		public event EventHandler Started;
		public event EventHandler Stopped;
		public event EventHandler SpeechIdle;
		public event EventHandler<string> SpeechPrepare;
		public event EventHandler<string> SpeechStarted;
		public event EventHandler<string> TextAdded;
		public event EventHandler<string> Error;
		public bool IsRunning { get; set; }

		private const string checkValue = "exmQvs3sLwMVXqe78fQL";

		private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
		private QuestToSpeechConfig config = new QuestToSpeechConfig();		
		private List<string> dataStrings;
		private Dictionary<string, Voice> tmpVoicesAllocations = new Dictionary<string, Voice>();
		private Thread ttsThread = null;
		private SoundPlayer player = null;
		private Voice[] cachedAvailableVoices = null;
		private bool keepThreadAlive;
		private bool isIdle = false;

		public QuestToSpeech() {
			LoadConfig();

			if (!Directory.Exists(TempDirectory)) // create tmp dir if not exists
				Directory.CreateDirectory(TempDirectory);
		}

		public void Start() {
			if (ttsThread != null)
				return;

			if (config.EnabledModules.Length == 0) {
				OnError("Error: No module enabled");
				return;
			}

			if (config.SelectedVoices.Length == 0) {
				OnError("Error: No voice selected");
				return;
			}

			ClearTemp();

			isIdle = false;
			player = new SoundPlayer();
			ttsThread = new Thread(ThreadWorker);
			ttsThread.Start();
		}

		public void Stop() {
			if(player != null) {
				player.Stop();
				player.Dispose();
				player = null;
			}

			if (ttsThread == null)
				return;

			keepThreadAlive = false;
		}

		public bool IsModuleEnabled(Module module) {
			return config.EnabledModules.Contains(module);
		}

		public bool IsModuleReady(Module module) {
			switch (module) {
				case Module.Azure:
					if (config.AzureAPI == null || string.IsNullOrWhiteSpace(config.AzureAPI.Key) || string.IsNullOrWhiteSpace(config.AzureAPI.Region))
						return false;
					break;
				case Module.Google:
					if (Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS") == null)
						return false;
					if (!File.Exists(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
						return false;
					break;
				case Module.AWS:
					if (config.AwsAPI == null || string.IsNullOrWhiteSpace(config.AwsAPI.AccessKey) || string.IsNullOrWhiteSpace(config.AwsAPI.SecretKey) || string.IsNullOrWhiteSpace(config.AwsAPI.Region))
						return false;
					break;
			}

			return true;
		}

		public AzureAPIConfig GetAzureAPIConfig() {
			return config.AzureAPI;
		}

		public AwsAPIConfig GetAwsAPIConfig() {
			return config.AwsAPI;
		}

		public Voice[] GetSelectedVoices() {
			return config.SelectedVoices;
		}

		public Voice[] GetAvailableVoices() {
			if (cachedAvailableVoices != null)
				return cachedAvailableVoices;

			List<Voice> voices = new List<Voice>();

			if (config.EnabledModules.Contains(Module.Windows)) {
				Voice[] arr = new Voice[0];

				string err = WindowsTTS.GetVoices(ref arr);

				if (err != null) {
					OnError(err);
					return null;
				} else {
					voices.AddRange(arr);
				}
			}

			if (config.EnabledModules.Contains(Module.Azure)) {
				Voice[] arr = new Voice[0];

				string err = AzureTTS.GetVoices(ref arr, config.AzureAPI);

				if (err != null) {
					OnError(err);
					return null;
				} else {
					voices.AddRange(arr);
				}
			}

			if (config.EnabledModules.Contains(Module.Google)) {
				Voice[] arr = new Voice[0];

				string err = GoogleTTS.GetVoices(ref arr);

				if (err != null) {
					OnError(err);
					return null;
				} else {
					voices.AddRange(arr);
				}
			}

			if (config.EnabledModules.Contains(Module.AWS)) {
				Voice[] arr = new Voice[0];

				string err = AwsTTS.GetVoices(ref arr, config.AwsAPI);

				if (err != null) {
					OnError(err);
					return null;
				} else {
					voices.AddRange(arr);
				}
			}

			cachedAvailableVoices = voices.ToArray(); // cache list

			return cachedAvailableVoices;
		}

		public void SetAzureAPIConfig(AzureAPIConfig apiConfig) {
			config.AzureAPI = apiConfig;
			SaveConfig();
		}

		public void SetAwsAPIConfig(AwsAPIConfig apiConfig) {
			config.AwsAPI = apiConfig;
			SaveConfig();
		}

		public bool SetModuleEnabled(Module module, bool enabled) {
			List<Module> modules = new List<Module>(config.EnabledModules);

			if(enabled) {
				if (!IsModuleReady(module))
					return false;

				if(!modules.Contains(module))
					modules.Add(module);
			} else {
				if (modules.Contains(module)) {
					modules.Remove(module);
										
					config.SelectedVoices = Array.FindAll(config.SelectedVoices, delegate (Voice v) { return v.Module != module; }); // remove selected voices by module
				}
			}

			config.EnabledModules = modules.ToArray<Module>();
			SaveConfig();

			cachedAvailableVoices = null;

			return true;
		}

		public void SetSelectedVoices(Voice[] voices) {
			config.SelectedVoices = voices;
			SaveConfig();
		}

		public void ClipboardTextRecieved(string str) {
			if (ttsThread == null || !IsRunning)
				return;

			string dataStr = Util.Base64Decode(str); // do base64 encode 
			string pkgStart = string.Format("<[[{0};;", checkValue);
			string pkgEnd = "]]>";

			if (dataStr != null) { // is not encoded in base64
				if (dataStr.StartsWith(pkgStart) && dataStr.EndsWith(pkgEnd)) { // validate package
					dataStr = dataStr.Substring(pkgStart.Length, dataStr.Length - (pkgStart.Length + pkgEnd.Length)); // get rid of open/close tag and check value

					if (dataStrings.Count == 0 || dataStr != (dataStrings[dataStrings.Count - 1] as string)) { // if has no strings left or not equals last string
						string[] splitData = dataStr.Split(new string[] { ";;" }, StringSplitOptions.None);

						if (splitData.Length == 3) { // has correct num of entrys
							dataStrings.Add(dataStr); // add to queue
							OnTextAdded(splitData[1], splitData[2].Length);
						} else {
							OnError("Error: Datapackage is invalid!");
						}
					}
				} else if (dataStr.IndexOf(pkgStart) > -1 && dataStrings.IndexOf(pkgEnd) > -1 && dataStr.IndexOf(";;") > -1) { // has package format but wrong key probably
					OnError("Error: Application and Addon versions are maybe different!");
				}
			}
		}

		public static Module GetModule(string str) {
			return (Module)Enum.Parse(typeof(Module), str);
		}

		public static bool IsVoice(Voice v1, Voice v2) {
			if (v1.LangCode == v2.LangCode && v1.Module == v2.Module && v1.Gender == v2.Gender && v1.Name == v2.Name)
				return true;

			return false;
		}

		private void ThreadWorker() {
			dataStrings = new List<string>();
			keepThreadAlive = true;
			IsRunning = true;

			OnStarted();

			while (keepThreadAlive) {
				// wait and skip
				if (dataStrings.Count == 0) {
					if(!isIdle) {
						OnSpeechIdle();
						isIdle = true;
					}

					Thread.Sleep(50);
					continue;
				}

				isIdle = false;

				// parse data string
				string[] splitData = dataStrings[0].Split(new string[] { ";;" }, StringSplitOptions.None); // find start of text

				// play cached or create new file
				string audioFilePath = Path.Combine(TempDirectory, string.Format("{0}.wav", Util.MD5Hash(dataStrings[0])));

				if (File.Exists(audioFilePath) || SpeakToFile(splitData, audioFilePath)) {
					PlayFile(audioFilePath);
					ClearTemp(25);
				}

				dataStrings.RemoveAt(0); // remove data string

				Thread.Sleep(1000); // little pause between texts
			}

			IsRunning = false;
			ttsThread = null;
			Stop();
			OnStopped();
		}

		private bool SpeakToFile(string[] splitData, string filePath) {
			if (!tmpVoicesAllocations.ContainsKey(splitData[1])) {
				// get random voice from selected voices by gender
				Voice[] voices = GetSelectedVoices();
				Gender gender = (splitData[0] == "f") ? Gender.Female : Gender.Male; // get gender
				Voice[] genVoices = Array.FindAll(voices, delegate (Voice v) { return (v.Gender == gender); }); // find voices by gender
				genVoices = genVoices.Length == 0 ? voices : genVoices; // if no voice for gender selected select ay
				tmpVoicesAllocations.Add(splitData[1], genVoices[new Random().Next(genVoices.Length)]); // allocate voice to npcid
			}

			Voice voice = tmpVoicesAllocations[splitData[1]];

			string err = null;

			switch (voice.Module) {
				case Module.Windows:
					err = WindowsTTS.Speak(splitData[2], voice, filePath);
					break;
				case Module.Azure:
					err = AzureTTS.Speak(splitData[2], voice, filePath, config.AzureAPI);
					break;
				case Module.Google:
					err = GoogleTTS.Speak(splitData[2], voice, filePath);
					break;
				case Module.AWS:
					err = AwsTTS.Speak(splitData[2], voice, filePath, config.AwsAPI);
					break;
			}

			if (err != null) {
				OnError(err);
				return false;
			}

			OnSpeechPrepare(voice);

			return true;
		}

		private void PlayFile(string filePath) {
			if(player != null) {
				OnSpeechStarted(filePath);

				player.SoundLocation = filePath;
				player.PlaySync();
			}
		}
		
		private void ClearTemp(int keepLastN = 0) {
			FileInfo[] files = new DirectoryInfo(TempDirectory).GetFiles().OrderBy(p => p.CreationTime).ToArray();

			for (int i = 0; i < files.Length; i++) {
				if (i >= keepLastN)
					files[i].Delete();
			}
		}

		private void LoadConfig() {
			string err = SerializeXMLFile.Read(configPath, ref config);

			if (err != null)
				throw new Exception(err);
		}

		private void SaveConfig() {
			string err = SerializeXMLFile.Write(configPath, config);

			if (err != null)
				throw new Exception(err);
		}

		protected virtual void OnStarted() {
			Started?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnStopped() {
			Stopped?.Invoke(this, EventArgs.Empty);
		}
		protected virtual void OnSpeechIdle() {
			SpeechIdle?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnSpeechPrepare(Voice v) {
			SpeechPrepare?.Invoke(this, string.Format("{0} {1} ({2})", v.Module.ToString(), v.Name, v.Gender.ToString()));
		}

		protected virtual void OnSpeechStarted(string fileName) {
			SpeechStarted?.Invoke(this, Path.GetFileName(fileName));
		}

		protected virtual void OnTextAdded(string npcId, int numChars) {
			TextAdded?.Invoke(this, string.Format("NpcID {0} ({1} chars)", npcId, numChars));
		}

		protected virtual void OnError(string err) {
			Error?.Invoke(this, err);
		}
	}	
}
