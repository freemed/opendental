using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CodeBase {
	public class MiscUtils {

		public static List <T> ArrayToList<T> (T[] array){
			List <T> list=new List<T> ();
			for(int i=0;i<array.Length;i++){
				list.Add(array[i]);
			}
			return list;
		}

		public static string CreateRandomAlphaNumericString(int length){
			string result="";
			Random rand=new Random();
			string randChrs="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			for(int i=0;i<length;i++){
				result+=randChrs[rand.Next(0,randChrs.Length-1)];
			}
			return result;
		}

		public static string Decrypt(string encString) {
			try {
				byte[] encrypted=Convert.FromBase64String(encString);
				MemoryStream ms=null;
				CryptoStream cs=null;
				StreamReader sr=null;
				Aes aes=new AesManaged();
				UTF8Encoding enc=new UTF8Encoding();
				aes.Key=enc.GetBytes("AKQjlLUjlcABVbqp");//Random string will be key
				aes.IV=new byte[16];
				ICryptoTransform decryptor=aes.CreateDecryptor(aes.Key,aes.IV);
				ms=new MemoryStream(encrypted);
				cs=new CryptoStream(ms,decryptor,CryptoStreamMode.Read);
				sr=new StreamReader(cs);
				string decrypted=sr.ReadToEnd();
				ms.Dispose();
				cs.Dispose();
				sr.Dispose();
				if(aes!=null) {
					aes.Clear();
				}
				return decrypted;
			}
			catch {
				MessageBox.Show("Text entered was not valid encrypted text.");
				return "";
			}
		}

	}
}
