using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace wow_quest_to_speech {
	public static class Util {
		public static string Base64Decode(string str) {
			try {
				byte[] bytes = Convert.FromBase64String(str);

				return Encoding.UTF8.GetString(bytes);
			} catch (Exception ex) {
				Console.WriteLine("Failed to decode base64 string, maybe not a valid: " + str);
				Console.WriteLine(ex.InnerException != null ? ex.InnerException.ToString() : ex.Message);
			}

			return null;
		}

		public static string Base64Encode(string str) {
			try {
				byte[] bytes = Encoding.UTF8.GetBytes(str);

				return Convert.ToBase64String(bytes);
			} catch (Exception ex) {
				Console.WriteLine("Failed to base64 encode string: " + str);
				Console.WriteLine(ex.InnerException != null ? ex.InnerException.ToString() : ex.Message);
			}

			return null;
		}

		public static string MD5Hash(string str) {
			if ((str == null) || (str.Length == 0))
				return string.Empty;

			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] textToHash = Encoding.Default.GetBytes(str);
			byte[] result = md5.ComputeHash(textToHash);

			return BitConverter.ToString(result).Replace("-", "").ToLower();
		}
	}
}
