using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenDentBusiness;
using System.Windows.Forms;
using CodeBase;

namespace OpenDental.Eclaims {
	public class Dutch {

		///<summary>Called from Eclaims and includes multiple claims.  This should return the text of the file that was sent so that it can be saved inthe db.  If this returns an empty result, then claims won't be marked as sent.  The problem is that we create multiple files here.</summary>
		public static string SendBatch(List<ClaimSendQueueItem> queueItems,int batchNum) {
			//We assume for now one file per claim.
			for(int i=0;i<queueItems.Count;i++) {
				if(!CreateClaim(queueItems[i],batchNum)){
					return "";
				}
			}
			return "Sent";//no need to translate.  User will not see.
		}

		///<summary>Called once for each claim to be created.  For claims with a lot of procedures, this may actually create multiple claims.  Normally returns empty string unless something went wrong.</summary>
		public static bool CreateClaim(ClaimSendQueueItem queueItem,int batchNum) {
			StringBuilder strb=new StringBuilder();
			string t="\t";
			strb.Append("110\t111\t112\t118\t203/403\tF108/204/404\t205/405\t206\t207\t208\t209\t210\t211\t212\t215\t217\t219\t406\t408\t409\t410\t411\t413\t414\t415\t416\t418\t419\t420\t421\t422\t423\t424\t425\t426\t428\t429\t430\t432\t433\r\n");
			Clearinghouse clearhouse=ClearinghouseL.GetClearinghouse(queueItem.ClearinghouseNum);
			Claim claim=Claims.GetClaim(queueItem.ClaimNum);
			Provider provBill=Providers.GetProv(claim.ProvBill);
			Patient pat=Patients.GetPat(claim.PatNum);
			InsPlan insplan=InsPlans.GetPlan(claim.PlanNum,new List<InsPlan>());
			Carrier carrier=Carriers.GetCarrier(insplan.CarrierNum);
			List<ClaimProc> claimProcList=ClaimProcs.Refresh(pat.PatNum);
			List<ClaimProc> claimProcsForClaim=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
			List<Procedure> procList=Procedures.Refresh(claim.PatNum);
			Procedure proc;
			//ProcedureCode procCode;
			for(int i=0;i<claimProcsForClaim.Count;i++) {
				proc=Procedures.GetProcFromList(procList,claimProcsForClaim[i].ProcNum);
				//procCode=Pro
				strb.Append(provBill.SSN+t);//111
				strb.Append(provBill.SSN+t);
				strb.Append(t);
				strb.Append(t);
				strb.Append(pat.SSN+t);
				strb.Append(carrier.CarrierName+t);//carrier name?
				strb.Append(insplan.SubscriberID+t);
				strb.Append(pat.PatNum.ToString()+t);
				strb.Append(pat.Birthdate.ToString("dd-MM-yyyy")+t);
				if(pat.Gender==PatientGender.Female) {
					strb.Append("V"+t);
				}
				else {
					strb.Append("M"+t);
				}
				strb.Append("1"+t);
				strb.Append(DutchLName(pat.LName)+t);//last name without prefix
				strb.Append(DutchLNamePrefix(pat.LName)+t);//prefix
				strb.Append("2"+t);
				strb.Append(t);//initials??
				strb.Append(pat.Zip+t);
				strb.Append(pat.Address+t);//house number-Needs work
				strb.Append(t);
				strb.Append(proc.ProcDate.ToString("dd-MM-yyyy")+t);//procDate
				strb.Append("01"+t);
				strb.Append(t);
				strb.Append(t);
				strb.Append(ProcedureCodes.GetStringProcCode(proc.CodeNum)+t);
				strb.Append(t);//U/L needs work
				strb.Append(Tooth.ToInternat(proc.ToothNum)+t);
				strb.Append(proc.Surf+t);//needs validation
				strb.Append(t);
				if(claim.AccidentRelated=="") {//not accident
					strb.Append("N"+t);
				}
				else {
					strb.Append("J"+t);
				}
				strb.Append(pat.SSN+t);
				strb.Append(t);
				strb.Append(t);
				strb.Append(t);
				strb.Append(proc.ProcFee.ToString("F")+t);
				strb.Append("1"+t);
				strb.Append(proc.ProcFee.ToString("F")+t);
				strb.Append(t);
				strb.Append(t);
				strb.Append(proc.ProcFee.ToString("F")+t);
				strb.Append(t);
				strb.Append(t);
				strb.Append("\r\n");
			}
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				MessageBox.Show(saveFolder+" "+Lan.g("Dutch","not found."));
				return false;
			}
			string saveFile=ODFileUtils.CombinePaths(saveFolder,"claims"+claim.ClaimNum.ToString()+".txt");
			File.WriteAllText(saveFile,strb.ToString());
			//MessageBox.Show(strb.ToString());
			return true;
		}

		///<summary>Returns only the portion of the LName not including the prefix</summary>
		public static string DutchLName(string fullLName) {
			//eg. Berg, van der
			if(!fullLName.Contains(",")) {
				return fullLName;
			}
			return fullLName.Substring(0,fullLName.IndexOf(","));
		}

		///<summary>Returns only the prefix of the LName</summary>
		public static string DutchLNamePrefix(string fullLName) {
			//eg. Berg, van der
			if(!fullLName.Contains(",")) {
				return "";
			}
			if(fullLName.EndsWith(",")){
				return "";
			}
			string retVal=fullLName.Substring(fullLName.IndexOf(",")+1);// van der
			retVal.TrimStart(' ');
			return retVal;
		}

	}
}
