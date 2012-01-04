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
	public class EmdeonMedical{

		///<summary></summary>
		public EmdeonMedical()
		{			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. If they failed, a rollback will happen automatically by deleting the previously created X12 file. The batchnum is supplied for the possible rollback.  Also used for mail retrieval.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum,EnumClaimMedType medType){
			EmdeonITSTest.ITSWS itsws=new EmdeonITSTest.ITSWS();
			itsws.Url="https://cert.its.emdeon.com/ITS/ITSWS.asmx";//test url
			//itsws.Url="https://its.emdeon.com/ITS/ITSWS.asmx";//production url
			try{
			  if(!Directory.Exists(clearhouse.ExportPath)){
			    throw new Exception("Clearinghouse export path is invalid.");
			  }
			  if(!Directory.Exists(clearhouse.ResponsePath)){
			    throw new Exception("Clearinghouse response path is invalid.");
			  }
				string[] files=Directory.GetFiles(clearhouse.ExportPath);
				for(int i=0;i<files.Length;i++) {
					byte[] fileBytes=File.ReadAllBytes(files[i]);
					string fileBytesBase64=Convert.ToBase64String(fileBytes);
					string messageType="MCT";//medical
					if(medType==EnumClaimMedType.Institutional) {
						messageType="HCT";
					}
					else if(medType==EnumClaimMedType.Dental) {
						messageType="DCT";//not used yet, but planned for future.
					}
					EmdeonITSTest.ITSReturn response=itsws.PutFileExt(clearhouse.LoginID,clearhouse.Password,messageType,Path.GetFileName(files[i]),fileBytesBase64);
					if(response.ErrorCode==0) { //Submission successful.
						string successDetails=response.Response;//todo: store in db
						File.Delete(files[i]);//delete uploaded claims so they don't get resent later.
					}
					else { //Submission error.
						MessageBox.Show(Lan.g("EmdeonMedical","Claim submission failed. Batch file")+" "+files[i]+" "
							+Lan.g("EmdeonMedical","will not be deleted so it can be resent. Error message from Emdeon")+": "+response.Response);
					}
				}
			//  //rename the downloaded mail files to end with txt
			//  files=Directory.GetFiles(clearhouse.ResponsePath);
			//  for(int i=0;i<files.Length;i++){
			//    //string t=files[i];
			//    if(Path.GetExtension(files[i])!=".txt"){
			//      File.Move(files[i],files[i]+".txt");
			//    }
			//  }
			}
			catch(Exception e){
			  MessageBox.Show(e.Message);
			  if(batchNum!=0){
			    x837Controller.Rollback(clearhouse,batchNum);
			  }
			  return false;
			}
			return true;
		}

	}
}
