using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for WebMD.
	/// </summary>
	public class WebClaim{
		///<summary></summary>
		public WebClaim()
		{
			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. If they failed, a rollback will happen automatically by deleting the previously created X12 file. The batchnum is supplied for the possible rollback.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			try{
				//Step 1: Post authentication request:
				Version myVersion=new Version(Application.ProductVersion);
				HttpWebRequest webReq;
				WebResponse response;
				StreamReader readStream;
				string str;
				string[] responseParams;
				string status="";
				string group="";
				string userid="";
				string authid="";
				string errormsg="";
				string alertmsg="";
				string curParam="";
				string serverName=//"https://prelive.dentalxchange.com/dci/upload.svl";
					"https://claimconnect.dentalxchange.com/dci/upload.svl";
				webReq=(HttpWebRequest)WebRequest.Create(serverName);
				string postData=
					"Function=Auth"//CONSTANT; signifies that this is an authentication request
					+"&Source=EDI"//CONSTANT; file format
					+"&Username="+clearhouse.LoginID
					+"&Password="+clearhouse.Password
					+"&UploaderName=OpenDental"//CONSTANT
					+"&UploaderVersion="+myVersion.Major.ToString()+"."+myVersion.Minor.ToString();//eg 3.4
				webReq.KeepAlive=false;
				webReq.Method="POST";
				webReq.ContentType="application/x-www-form-urlencoded";
				webReq.ContentLength=postData.Length;
				ASCIIEncoding encoding=new ASCIIEncoding();
				byte[] bytes=encoding.GetBytes(postData);
				Stream streamOut=webReq.GetRequestStream();
				streamOut.Write(bytes,0,bytes.Length);
				streamOut.Close();
				response=webReq.GetResponse();
				//Process the authentication response:
				readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
				str=readStream.ReadToEnd();
				readStream.Close();
				//MessageBox.Show(str);
				responseParams=str.Split('&');
				for(int i=0;i<responseParams.Length;i++){
					curParam=GetParam(responseParams[i]);
					switch(curParam){
						case "Status":
							status=GetParamValue(responseParams[i]);
							break;
						case "GROUP":
							group=GetParamValue(responseParams[i]);
							break;
						case "UserID":
							userid=GetParamValue(responseParams[i]);
							break;
						case "AuthenticationID":
							authid=GetParamValue(responseParams[i]);
							break;
						case "ErrorMessage":
							errormsg=GetParamValue(responseParams[i]);
							break;
						case "AlertMessage":
							alertmsg=GetParamValue(responseParams[i]);
							break;
						default:
							throw new Exception("Unexpected parameter: "+curParam);
					}
				}
				//Process response for errors:
				if(alertmsg!=""){
					MessageBox.Show(alertmsg);
				}
				switch(status){
					case "0":
						//MessageBox.Show("Authentication successful.");
						break;
					case "1":
						throw new Exception("Authentication failed. "+errormsg);
					case "2":
						throw new Exception("Cannot authenticate at this time. "+errormsg);
					case "3":
						throw new Exception("Invalid authentication request. "+errormsg);
					case "4":
						throw new Exception("Invalid program version. "+errormsg);
					case "5":
						throw new Exception("No customer contract. "+errormsg);
				}
				//Step 2: Post upload request:
				string fileName=Directory.GetFiles(clearhouse.ExportPath)[0];
				string boundary="------------7d13e425b00d0";
				postData=
					"--"+boundary+"\r\n"
					+"Content-Disposition: form-data; name=\"Function\"\r\n"
					+"\r\n"
					+"Upload\r\n"
					+"--"+boundary+"\r\n"
					+"Content-Disposition: form-data; name=\"Source\"\r\n"
					+"\r\n"
					+"EDI\r\n"
					+"--"+boundary+"\r\n"
					+"Content-Disposition: form-data; name=\"AuthenticationID\"\r\n"
					+"\r\n"
					+authid+"\r\n"
					+"--"+boundary+"\r\n"
					+"Content-Disposition: form-data; name=\"File\"; filename=\""
						+fileName+"\"\r\n"
					+"Content-Type: text/plain\r\n"
					+"\r\n";
				using(StreamReader sr=new StreamReader(fileName)){
					postData+=sr.ReadToEnd()+"\r\n"
						+"--"+boundary+"--";
        }
				//Debug.Write(postData);
				//MessageBox.Show(postData);
				webReq=(HttpWebRequest)WebRequest.Create(serverName);
				webReq.KeepAlive=false;
				webReq.Method="POST";
				webReq.ContentType="multipart/form-data; boundary="+boundary;
				webReq.ContentLength=postData.Length;
				bytes=encoding.GetBytes(postData);
				streamOut=webReq.GetRequestStream();
				streamOut.Write(bytes,0,bytes.Length);
				streamOut.Close();
				response=webReq.GetResponse();
				//Process the response
				readStream=new StreamReader(response.GetResponseStream(),Encoding.ASCII);
				str=readStream.ReadToEnd();
				readStream.Close();
				errormsg="";
				status="";
				str=str.Replace("\r\n","");
				Debug.Write(str);
				if(str.Length>300){
					throw new Exception("Unknown lengthy error message received.");
				}
				responseParams=str.Split('&');
				for(int i=0;i<responseParams.Length;i++){
					curParam=GetParam(responseParams[i]);
					switch(curParam){
						case "Status":
							status=GetParamValue(responseParams[i]);
							break;
						case "ErrorMessage":
							errormsg=GetParamValue(responseParams[i]);
							break;
						case "Filename":
						case "Timestamp":
							break;
						case ""://errorMessage blank
							break;
						default:
							throw new Exception("Unexpected parameter: "+curParam+"*");
					}
				}
				switch(status){
					case "0":
						MessageBox.Show("Upload successful.");
						break;
					case "1":
						throw new Exception("Authentication failed. "+errormsg);
					case "2":
						throw new Exception("Cannot upload at this time. "+errormsg);
				}
				//delete the uploaded claim
				File.Delete(fileName);
			}
			catch(Exception e){
				MessageBox.Show(e.Message);
				X12.Rollback(clearhouse,batchNum);
				return false;
			}
			return true;
		}

		private static string GetParam(string paramAndValue){
			if(paramAndValue==""){
				return "";
			}
			string[] pair=paramAndValue.Split('=');
			//if(pair.Length!=2){
			//	throw new Exception("Unexpected parameter from server: "+paramAndValue);
			return pair[0];
		}

		private static string GetParamValue(string paramAndValue){
			if(paramAndValue==""){
				return "";
			}
			string[] pair=paramAndValue.Split('=');
			//if(pair.Length!=2){
			//	throw new Exception("Unexpected parameter from server: "+paramAndValue);
			//}
			if(pair.Length==1){
				return "";
			}
			return pair[1];
		}


	}
}
