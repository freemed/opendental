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

		//private static string emdeonITSUrl="https://cert.its.emdeon.com/ITS/ITSWS.asmx";//test url
		private static string emdeonITSUrl="https://its.emdeon.com/ITS/ITSWS.asmx";//production url

		///<summary></summary>
		public EmdeonMedical()
		{			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. If they failed, a rollback will happen automatically by deleting the previously created X12 file. The batchnum is supplied for the possible rollback.  Also used for mail retrieval.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum,EnumClaimMedType medType){
			//EmdeonITSTest.ITSWS itsws=new EmdeonITSTest.ITSWS();
			EmdeonITS.ITSWS itsws=new EmdeonITS.ITSWS();
			itsws.Url=emdeonITSUrl;
			string batchFile="";
			try {
				if(!Directory.Exists(clearhouse.ExportPath)) {
					throw new Exception("Clearinghouse export path is invalid.");
				}
				//We make sure to only send the X12 batch file for the current batch, so that if there is a failure, then we know
				//for sure that we need to reverse the batch. This will also help us avoid any exterraneous/old batch files in the
				//same directory which might be left if there is a permission issue when trying to delete the batch files after processing.
				batchFile=Path.Combine(clearhouse.ExportPath,"claims"+batchNum+".txt");
				byte[] fileBytes=File.ReadAllBytes(batchFile);
				string fileBytesBase64=Convert.ToBase64String(fileBytes);
				string messageType="MCT";//medical
				if(medType==EnumClaimMedType.Institutional) {
					messageType="HCT";
				}
				else if(medType==EnumClaimMedType.Dental) {
					//messageType="DCT";//not used/tested yet, but planned for future.
				}
				//EmdeonITSTest.ITSReturn response=itsws.PutFileExt(clearhouse.LoginID,clearhouse.Password,messageType,Path.GetFileName(batchFile),fileBytesBase64);
				EmdeonITS.ITSReturn response=itsws.PutFileExt(clearhouse.LoginID,clearhouse.Password,messageType,Path.GetFileName(batchFile),fileBytesBase64);
				if(response.ErrorCode!=0) { //Batch submission successful.
					throw new Exception("Emdeon rejected all claims in the current batch file "+batchFile+" for the following reason: "+response.Response);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				x837Controller.Rollback(clearhouse,batchNum);
				return false;
			}
			finally {
				try {
					if(batchFile!="") {
						File.Delete(batchFile);
					}
				}
				catch {
					MessageBox.Show("Failed to remove batch file "+batchFile+". Probably due to a permission issue. Check folder permissions and manually delete.");
				}
			}
			return true;
		}

		public static bool Retrieve(Clearinghouse clearhouse) {
			try {
				if(!Directory.Exists(clearhouse.ResponsePath)) {
					throw new Exception("Clearinghouse response path is invalid.");
				}
				string[] messageTypes=new string[] { 
					"MCT", //Medical
					"HCT", //Institutional
					//"DCT"  //Dental. Planned for future.
				};
				bool reportsDownloaded=false;
				for(int i=0;i<messageTypes.Length;i++) {					
					//EmdeonITSTest.ITSWS itsws=new EmdeonITSTest.ITSWS();
					EmdeonITS.ITSWS itsws=new EmdeonITS.ITSWS();
					itsws.Url=emdeonITSUrl;
					//Download the most up to date reports, but do not delete them from the server yet.
					//EmdeonITSTest.ITSReturn response=itsws.GetFile(clearhouse.LoginID,clearhouse.Password,messageTypes[i]+"G");
					EmdeonITS.ITSReturn response=itsws.GetFile(clearhouse.LoginID,clearhouse.Password,messageTypes[i]+"G");
					if(response.ErrorCode==0) { //Report retrieval successful.
						string reportFileDataBase64=response.Response;
						byte[] reportFileDataBytes=Convert.FromBase64String(reportFileDataBase64);
						string reportFilePath=CodeBase.ODFileUtils.CreateRandomFile(clearhouse.ResponsePath,".zip");
						File.WriteAllBytes(reportFilePath,reportFileDataBytes);
						reportsDownloaded=true;
						//Now that the file has been saved, remove the report file from the Emdeon server.
						//If deleting the report fails, we don't care because that will simply mean that we download it again next time.
						//Thus we don't need to check the status after this next call.
						itsws.GetFile(clearhouse.LoginID,clearhouse.Password,messageTypes[i]+"D");
					}
					else if(response.ErrorCode!=209) { //Report retrieval failure, excluding the error that can be returned when the mailbox is empty.
						throw new Exception("Failed to get reports from Emdeon. Error message from Emdeon: "+response.Response);
					}
				}
				if(!reportsDownloaded) {
					MessageBox.Show("Report mailbox is empty.");
				}
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

	}
}