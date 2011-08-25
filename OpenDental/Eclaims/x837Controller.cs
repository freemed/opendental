using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental.Eclaims
{
	/// <summary></summary>
	public class x837Controller{
		///<summary></summary>
		public x837Controller()
		{
			
		}

		///<summary>Gets the filename for this batch. Used when saving or when rolling back.</summary>
		private static string GetFileName(Clearinghouse clearhouse,int batchNum){
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				MessageBox.Show(saveFolder+" not found.");
				return "";
			}
			if(clearhouse.CommBridge==EclaimsCommBridge.RECS){
				if(File.Exists(ODFileUtils.CombinePaths(saveFolder,"ecs.txt"))){
					MsgBox.Show("FormClaimsSend","You must send your existing claims from the RECS program before you can create another batch.");
					return "";//prevents overwriting an existing ecs.txt.
				}
				return ODFileUtils.CombinePaths(saveFolder,"ecs.txt");
			}
			else{
				return ODFileUtils.CombinePaths(saveFolder,"claims"+batchNum.ToString()+".txt");
			}
		}

		///<summary>If file creation was successful but communications failed, then this deletes the X12 file.  This is not used in the Tesia bridge because of the unique filenaming.</summary>
		public static void Rollback(Clearinghouse clearhouse,int batchNum){
			if(clearhouse.CommBridge==EclaimsCommBridge.RECS){
				//A RECS rollback never deletes the file, because there is only one
			}
			else{
				//This is a Windows extension, so we do not need to worry about Unix path separator characters.
				File.Delete(ODFileUtils.CombinePaths(clearhouse.ExportPath,"claims"+batchNum.ToString()+".txt"));
			}
		}

		///<summary>Called from Eclaims and includes multiple claims.  Returns the string that was sent.  The string needs to be parsed to determine the transaction numbers used for each claim.</summary>
		public static string SendBatch(List<ClaimSendQueueItem> queueItems,int batchNum){
			//js 8/25/11 This method will be rewritten to handle 5010s separately.
			//It would also be nice if we change the sw to a stringbuilder.
			Clearinghouse clearhouse=ClearinghouseL.GetClearinghouse(queueItems[0].ClearinghouseNum);
			List<ClaimSendQueueItem> functionalGroupDental=new List<ClaimSendQueueItem>();
			List<ClaimSendQueueItem> functionalGroupMedical=new List<ClaimSendQueueItem>();
			for(int i=0;i<queueItems.Count;i++){
				if(queueItems[i].IsMedical){
					functionalGroupMedical.Add(queueItems[i]);
				}
				else{
					functionalGroupDental.Add(queueItems[i]);
				}
			}
			if(functionalGroupMedical.Count>0 && functionalGroupDental.Count>0) {
				MessageBox.Show(Lan.g("X12","You must send dental and medical claims in separate batches."));
				return "";//not allowed
			}
			string saveFile=GetFileName(clearhouse,batchNum);
			if(saveFile==""){
				return "";
			}
			using(StreamWriter sw=new StreamWriter(saveFile,false,Encoding.ASCII)){
				X837_4010.GenerateMessageText(sw,clearhouse,batchNum,functionalGroupDental);
			}
			if(clearhouse.CommBridge==EclaimsCommBridge.PostnTrack){
				//need to clear out all CRLF from entire file
				string strFile="";
				using(StreamReader sr=new StreamReader(saveFile,Encoding.ASCII)){
					strFile=sr.ReadToEnd();
				}
				strFile=strFile.Replace("\r","");
				strFile=strFile.Replace("\n","");
				using(StreamWriter sw=new StreamWriter(saveFile,false,Encoding.ASCII)){
					sw.Write(strFile);
				}
			}
			CopyToArchive(saveFile);
			return File.ReadAllText(saveFile);
		}

	///<summary>Copies the given file to an archive directory within the same directory as the file.</summary>
		private static void CopyToArchive(string fileName){
			string direct=Path.GetDirectoryName(fileName);
			string fileOnly=Path.GetFileName(fileName);
			string archiveDir=ODFileUtils.CombinePaths(direct,"archive");
			if(!Directory.Exists(archiveDir)){
				Directory.CreateDirectory(archiveDir);
			}
			File.Copy(fileName,ODFileUtils.CombinePaths(archiveDir,fileOnly),true);
		}

		

		
	



		

	}
}
