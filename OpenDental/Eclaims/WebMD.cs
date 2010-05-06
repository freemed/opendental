using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using Ionic.Zip;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for WebMD.
	/// </summary>
	public class WebMD{
		///<summary></summary>
		public WebMD()
		{
			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. If they failed, a rollback will happen automatically by deleting the previously created X12 file. The batchnum is supplied for the possible rollback.  Also used for mail retrieval.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			string arguments="";
			try{
				if(!Directory.Exists(clearhouse.ExportPath)){
					throw new Exception("Clearinghouse export path is invalid.");
				}
				if(!Directory.Exists(clearhouse.ResponsePath)){
					throw new Exception("Clearinghouse response path is invalid.");
				}
				if(!File.Exists(clearhouse.ClientProgram)) {
					throw new Exception("Client program not installed properly.");
				}
				arguments=ODFileUtils.RemoveTrailingSeparators(clearhouse.ExportPath)+"\\"+"*.* "//upload claims path
					+ODFileUtils.RemoveTrailingSeparators(clearhouse.ResponsePath)+" "//Mail path
					+"316 "//vendor number.  
					+clearhouse.LoginID+" "//Client number. Assigned by us, and we have to coordinate for all other 'vendors' of Open Dental, because there is only one vendor number for OD for now.
					+clearhouse.Password;
				//call the WebMD client program
				Process process=Process.Start(clearhouse.ClientProgram,arguments);
				process.EnableRaisingEvents=true;
				process.WaitForExit();
				//delete the uploaded claims
				string[] files=Directory.GetFiles(clearhouse.ExportPath);
				for(int i=0;i<files.Length;i++){
					//string t=files[i];
					File.Delete(files[i]);
				}
				//rename the downloaded mail files to end with txt
				files=Directory.GetFiles(clearhouse.ResponsePath);
				for(int i=0;i<files.Length;i++){
					//string t=files[i];
					if(Path.GetExtension(files[i])!=".txt"){
						File.Move(files[i],files[i]+".txt");
					}
				}
			}
			catch(Exception e){
				MessageBox.Show(e.Message);//+"\r\n"+clearhouse.ClientProgram+" "+arguments);
				if(batchNum!=0){
					X12.Rollback(clearhouse,batchNum);
				}
				return false;
			}
			return true;
		}

		/*  Use Launch instead
		///<summary>Retrieves any waiting reports from this clearinghouse. Returns true if the communications were successful, and false if they failed.</summary>
		public static bool Retrieve(Clearinghouse clearhouse){
			bool retVal=true;
			try{
								
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				retVal=false;
			}
			return retVal;
		}*/

		private const string vendorId="";//TODO: Get from Emdeon!
		private const string testMode="true";//TODO: Set to 'false' on release.
		private const string emdeonServerUrl="";//TODO: Get from Emdeon!

		private static void SubmitBatch(Clearinghouse clearhouse,int batchNum){
			string[] files=Directory.GetFiles(clearhouse.ExportPath);
			for(int i=0;i<files.Length;i++){
				ZipFile zip=null;
				try{
					zip=new ZipFile();
					zip.AddFile(files[i]);
					MemoryStream ms=new MemoryStream();
					zip.Save(ms);
					string fileTextZippedBase64=Convert.ToBase64String(ms.GetBuffer());
					FileInfo fi=new FileInfo(files[i]);
					string claimXML="<?xml version=\"1.0\" ?>"
						+"<claim_submission_api xmlns=\"Emdeon_claim_submission_api\" revision=\"001\">"
							+"<authentication>"
								+"<vendor_id>"+vendorId+"</vendor_id>"
								+"<user_id>"+clearhouse.LoginID+"</user_id>"
								+"<password>"+clearhouse.Password+"</password>"
							+"</authentication>"
							+"<transaction>"
							+"<trace_id>"+batchNum+"</trace_id>"//TODO: Is this the right number to use?
							+"<trx_type>submit_claim_file_request</trx_type>"
							+"<test_mode>"+testMode+"</test_mode>"
							+"<trx_data>"
								+"<claim_file>"
									+"<file_name>"+Path.GetFileName(files[i])+"</file_name>"
									+"<file_format>DCDS2</file_format>"
									+"<file_size>"+fi.Length+"</file_size>"
									+"<file_compression>pkzip</file_compression>"
									+"<file_encoding>base64</file_encoding>"
									+"<file_data>"+fileTextZippedBase64+"</file_data>"
								+"</claim_file>"
							+"</trx_data>"
						+"</transaction>"
					+"</claim_submission_api>";
					byte[] claimXMLbytes=Encoding.UTF8.GetBytes(claimXML);
					WebClient myWebClient=new WebClient();
					myWebClient.Headers.Add("Content-Type","text/xml");
					byte[] responseBytes=myWebClient.UploadData(emdeonServerUrl,claimXMLbytes);




				}finally{
					if(zip!=null){
						zip.Dispose();
					}
				}
			}
		}




	}
}
