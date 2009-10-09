using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// aka ClaimConnect.
	/// </summary>
	public class ClaimConnect{
		///<summary></summary>
		public ClaimConnect()
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

		public static string Benefits270(Clearinghouse clearhouse,string x12message) {
			com.dentalxchange.webservices.Credentials cred = new com.dentalxchange.webservices.Credentials();
			if(PrefC.GetBool(PrefName.CustomizedForPracticeWeb)) {//even though they currently use code from a different part of the program.
				cred.client="Practice-Web";
				cred.serviceID="DCI Web Service ID: 001513";
			}
			else {
				cred.client="OpenDental";
				cred.serviceID="DCI Web Service ID: 002778";
			}
			cred.version=Application.ProductVersion;
			cred.username=clearhouse.LoginID;
			cred.password=clearhouse.Password;
			com.dentalxchange.webservices.Request request=new com.dentalxchange.webservices.Request();
			request.content=x12message;
			com.dentalxchange.webservices.WebServiceService service = new com.dentalxchange.webservices.WebServiceService();
#if DEBUG
			//service.Url = "https://prelive2.dentalxchange.com/dws/services/dciservice.svl"; // testing
			service.Url = "https://webservices.dentalxchange.com/dws/services/dciservice.svl"; // production
#else
			service.Url = "https://webservices.dentalxchange.com/dws/services/dciservice.svl"; //always use production. So I don't forget
#endif
			string strResponse="";
			try {
				com.dentalxchange.webservices.Response response = service.lookupEligibility(cred,request);
				strResponse=response.content;
			}
			catch(SoapException ex) {
				strResponse=Lan.g("FormInsPlan","SoapException: ")+ex.Detail.InnerText;
			}
			//cleanup response.  Seems to start with \n and 4 spaces.  Ends with trailing \n.
			strResponse=strResponse.Replace("\n","");
			strResponse=strResponse.TrimStart(' ');
			//CodeBase.MsgBoxCopyPaste msgbox=new CodeBase.MsgBoxCopyPaste(response.content);
			//msgbox.ShowDialog();
			return strResponse;

			/*
			string strRawResponse="";
			string strRawResponseNormal="ISA*00*          *00*          *30*330989922      *29*AA0989922      *030606*0936*U*00401*000013966*0*T*:~GS*HB*330989922*AA0989922*20030606*0936*13966*X*004010X092~ST*271*0001~BHT*0022*11*ASX012145WEB*20030606*0936~HL*1**20*1~NM1*PR*2*ACME INC*****PI*12345~HL*2*1*21*1~NM1*1P*1*PROVLAST*PROVFIRST****SV*5558006~HL*3*2*22*0~TRN*2*100*1330989922~NM1*IL*1*SMITH*JOHN*B***MI*123456789~REF*6P*XYZ123*GROUPNAME~REF*18*2484568*TEST PLAN NAME~N3*29 FREMONT ST*~N4*PEACE*NY*10023~DMG*D8*19570515*M~DTP*307*RD8*19910712-19920525~EB*1*FAM*30~SE*17*0001~GE*1*13966~IEA*1*000013966~";
			string strRawResponseFailureAuth=@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
	<soapenv:Body>
		<soapenv:Fault>
			<faultcode>soapenv:Server.userException</faultcode>
			<faultstring>Authentication failed.</faultstring>
			<faultactor/>
			<detail>
				<string>Authentication failed.</string>
			</detail>
		</soapenv:Fault>
	</soapenv:Body>
</soapenv:Envelope>";
			string strRawResponseFailure997=@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
	<soapenv:Body>
		<soapenv:Fault>
			<faultcode>soapenv:Server.userException</faultcode>
			<faultstring>Malformed document sent. Please insure that the format is correct and all required data is present.</faultstring>
			<faultactor/>
			<detail>
				<string>ISA*00*          *00*          *30*330989922      *30*BB0989922      *030606*1351*U*00401*000014066*0*T*:~GS*FA*330989922**20030606*1351*14066*X*004010~ST*997*0001~AK1*HR*100~AK2*276*0001~AK3*DMG*10**8~AK4*2**8*20041210~AK5*R*5~AK9*R*0*0*0*3~SE*8*0001~GE*1*14066~IEA*1*000014066~</string>
			</detail>
		</soapenv:Fault>
	</soapenv:Body>
</soapenv:Envelope>";
			return strRawResponseNormal;*/
			 
			/*
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(strRawResponse);
			//StringReader strReader=new StringReader(strRawResponseNormal);
			//XmlReader xmlReader=XmlReader.Create(strReader);
			//xmlReader...MoveToElement(
			XmlNode node=doc.SelectSingleNode("//lookupEligibilityReturn");
			if(node!=null) {
				return node.InnerText;//271
			}
			node=doc.SelectSingleNode("//detail/string");
			if(node==null) {
				throw new ApplicationException("Returned data not in expected format: "+strRawResponse);
			}
			if(node.InnerText=="Authentication failed.") {
				throw new ApplicationException("Authentication failed.");
			}
			return node.InnerText;//997
			*/
		}


	}

	/*
	[System.Web.Services.WebServiceBindingAttribute(Name="MyMathSoap",Namespace="http://www.contoso.com/")]
	public class MyMath:System.Web.Services.Protocols.SoapHttpClientProtocol {
		
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		public MyMath() {
			this.Url = "http://www.contoso.com/math.asmx";
		}

		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.contoso.com/Add",RequestNamespace="http://www.contoso.com/",ResponseNamespace="http://www.contoso.com/",Use=System.Web.Services.Description.SoapBindingUse.Literal,ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public int Add(int num1,int num2) {
			object[] results = this.Invoke("Add",new object[] {num1,
                        num2});
			return ((int)(results[0]));
		}

		[System.Diagnostics.DebuggerStepThroughAttribute()]
		public System.IAsyncResult BeginAdd(int num1,int num2,System.AsyncCallback callback,object asyncState) {
			return this.BeginInvoke("Add",new object[] {num1,
                        num2},callback,asyncState);
		}

		[System.Diagnostics.DebuggerStepThroughAttribute()]
		public int EndAdd(System.IAsyncResult asyncResult) {
			object[] results = this.EndInvoke(asyncResult);
			return ((int)(results[0]));
		}
	}*/

}
