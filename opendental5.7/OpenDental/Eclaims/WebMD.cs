using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

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



	}
}
