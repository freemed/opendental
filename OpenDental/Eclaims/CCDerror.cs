using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Resources;

//TODO: make error messages in CDAErrorMessages.txt longer in length if there is enough space on the final forms.
namespace OpenDental.Eclaims {
	class CCDerror {
		private static Encoding encoding=Encoding.GetEncoding(850);//code page 850
		private static byte[] enMessages=null;
		private static byte[] frMessages=null;

		///<summary>Get the error message for the given CDA error code.</summary>
		public static string message(int errorCode,bool useFrench){
			byte[] sbytes=null;
			if(useFrench){
				if(frMessages==null){//Attempt to load the binary resource file.
					frMessages=Properties.Resources.CDAErrorMessagesFr;
				}
				sbytes=frMessages;
			}else{
				if(enMessages==null){//Attempt to load the binary resource file.
					enMessages=Properties.Resources.CDAErrorMessagesEn;
				}
				sbytes=enMessages;
			}
			if(sbytes!=null){//Make sure the resource loaded before using it.
				MemoryStream stream=new MemoryStream(sbytes);
				using(StreamReader sr=new StreamReader(stream,encoding)){
					string errData=sr.ReadLine();
					while(errData!=null){
						if(errorCode==Convert.ToInt32(errData.Substring(0,4).Trim())){
							return errData.Substring(5,errData.Length-5);
						}
						errData=sr.ReadLine();
					}
				}
			}
			return((useFrench?"CODE D'ERREUR INCONNU":"UNKNOWN ERROR CODE")+": "+errorCode);
		}

	}
}
