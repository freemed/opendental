using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CentralConnections{
		///<summary></summary>
		public static List<CentralConnection> Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CentralConnection>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM centralconnection ORDER BY ItemOrder";
			return Crud.CentralConnectionCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(CentralConnection centralConnection){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				centralConnection.CentralConnectionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),centralConnection);
				return centralConnection.CentralConnectionNum;
			}
			return Crud.CentralConnectionCrud.Insert(centralConnection);
		}

		///<summary></summary>
		public static void Update(CentralConnection centralConnection){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),centralConnection);
				return;
			}
			Crud.CentralConnectionCrud.Update(centralConnection);
		}

		///<summary></summary>
		public static void Delete(long centralConnectionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),centralConnectionNum);
				return;
			}
			string command= "DELETE FROM centralconnection WHERE CentralConnectionNum = "+POut.Long(centralConnectionNum);
			Db.NonQ(command);
		}

		///<summary>Encrypts signature text and returns a base 64 string so that it can go directly into the database.</summary>
		public static string Encrypt(string str,byte[] key){
			//No need to check RemotingRole; no call to db.
			if(str==""){
				return "";
			}
			byte[] ecryptBytes=Encoding.UTF8.GetBytes(str);
			MemoryStream ms=new MemoryStream();
			CryptoStream cs=null;
			Aes aes=new AesManaged();
			aes.Key=key;
			aes.IV=new byte[16];
			ICryptoTransform encryptor=aes.CreateEncryptor(aes.Key,aes.IV);
			cs=new CryptoStream(ms,encryptor,CryptoStreamMode.Write);
			cs.Write(ecryptBytes,0,ecryptBytes.Length);
			cs.FlushFinalBlock();
			byte[] encryptedBytes=new byte[ms.Length];
			ms.Position=0;
			ms.Read(encryptedBytes,0,(int)ms.Length);
			cs.Dispose();
			ms.Dispose();
			if(aes!=null) {
				aes.Clear();
			}
			return Convert.ToBase64String(encryptedBytes);			
		}

		public static string Decrypt(string str,byte[] key) {
			//No need to check RemotingRole; no call to db.
			if(str==""){
				return "";
			}
			try {
				byte[] encrypted=Convert.FromBase64String(str);
				MemoryStream ms=null;
				CryptoStream cs=null;
				StreamReader sr=null;
				Aes aes=new AesManaged();
				aes.Key=key;
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
				//MessageBox.Show("Text entered was not valid encrypted text.");
				return"";
			}
		}

		///<summary></summary>
		private static string GenerateHash(string message) {
			//No need to check RemotingRole; no call to db.
			byte[] data=Encoding.ASCII.GetBytes(message);
			HashAlgorithm algorithm=SHA1.Create();
			byte[] hashbytes=algorithm.ComputeHash(data);
			byte digit1;
			byte digit2;
			string char1;
			string char2;
			StringBuilder strHash=new StringBuilder();
			for(int i=0;i<hashbytes.Length;i++) {
				if(hashbytes[i]==0) {
					digit1=0;
					digit2=0;
				}
				else {
					digit1=(byte)Math.Floor((double)hashbytes[i]/16d);
					//double remainder=Math.IEEERemainder((double)hashbytes[i],16d);
					digit2=(byte)(hashbytes[i]-(byte)(16*digit1));
				}
				char1=ByteToStr(digit1);
				char2=ByteToStr(digit2);
				strHash.Append(char1);
				strHash.Append(char2);
			}
			return strHash.ToString();
		}

		///<summary>The only valid input is a value between 0 and 15.  Text returned will be 1-9 or a-f.</summary>
		private static string ByteToStr(Byte byteVal) {
			//No need to check RemotingRole; no call to db.
			switch(byteVal) {
				case 10:
					return "a";
				case 11:
					return "b";
				case 12:
					return "c";
				case 13:
					return "d";
				case 14:
					return "e";
				case 15:
					return "f";
				default:
					return byteVal.ToString();
			}
		}


		/*
		
		///<summary>Gets one CentralConnection from the db.</summary>
		public static CentralConnection GetOne(long centralConnectionNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CentralConnection>(MethodBase.GetCurrentMethod(),centralConnectionNum);
			}
			return Crud.CentralConnectionCrud.SelectOne(centralConnectionNum);
		}

		
		*/



	}
}