using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace wow_quest_to_speech {
	public static class SerializeXMLFile {
		public static string Read<T>(string path, ref T obj) {
			if (File.Exists(path)) {
				try {
					XmlSerializer xml = new XmlSerializer(typeof(T));

					using (FileStream f = new FileStream(path, FileMode.Open)) {
						if (f.Length > 0) {
							byte[] buffer = new byte[f.Length];
							f.Read(buffer, 0, (int)f.Length);

							using (MemoryStream stream = new MemoryStream(buffer)) {
								obj = (T)xml.Deserialize(stream);
							}
						}
					}
				} catch (Exception ex) {
					return string.Format("Failed to read \"{0}\".\r\nException: {1}", path, ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
				}
			}

			return null;
		}

		public static string Write<T>(string path, T obj) {
			try {
				using (FileStream f = File.Create(path)) {
					XmlSerializer xml = new XmlSerializer(typeof(T));
					xml.Serialize(f, obj);
				}
			} catch (Exception ex) {
				return string.Format("Failed to write \"{0}\".\r\nException: {1}", path, ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
			}

			return null;
		}
	}
}
