using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class CanadianExtracts{
	
		///<summary>The list can be 0 length.</summary>
		public static List<CanadianExtract> GetForClaim(int claimNum){
			string command="SELECT * FROM canadianextract WHERE ClaimNum="+POut.PInt(claimNum);
			DataTable table=General.GetTable(command);
			List<CanadianExtract> retVal=new List<CanadianExtract>();
			CanadianExtract extract;
			for(int i=0;i<table.Rows.Count;i++){
				extract=new CanadianExtract();
				extract.CanadianExtractNum=PIn.PInt   (table.Rows[i][0].ToString());
				extract.ClaimNum          =PIn.PInt   (table.Rows[i][1].ToString());
				extract.ToothNum          =PIn.PString(table.Rows[i][2].ToString());
				extract.DateExtraction    =PIn.PDate  (table.Rows[i][3].ToString());
				retVal.Add(extract);
			}
			return retVal;
		}

		public static int CompareByToothNum(CanadianExtract x1,CanadianExtract x2) {
			string t1=x1.ToothNum;
			string t2=x2.ToothNum;
			if(t1==null || t1=="" || t2==null || t2=="") {
				return 0;//should never happen
			}
			if(!Tooth.IsValidDB(t1) || !Tooth.IsValidDB(t2)){
				return 0;
			}
			if(Tooth.IsPrimary(t1) && !Tooth.IsPrimary(t2)){
				return -1;
			}
			if(!Tooth.IsPrimary(t1) && Tooth.IsPrimary(t2)) {
				return 1;
			}
			//so either both are primary or both are permanent.
			return Tooth.ToInt(t1).CompareTo(Tooth.ToInt(t2));
		}

		public static void UpdateForClaim(int claimNum, List<CanadianExtract> missinglist){
			string command="DELETE FROM canadianextract WHERE ClaimNum="+POut.PInt(claimNum);
			General.NonQ(command);
			for(int i=0;i<missinglist.Count;i++){
				missinglist[i].ClaimNum=claimNum;
				Insert(missinglist[i]);
			}
		}

		///<summary></summary>
		private static void Insert(CanadianExtract cur) {
			if(PrefB.RandomKeys) {
				cur.CanadianExtractNum=MiscData.GetKey("canadianextract","CanadianExtractNum");
			}
			string command="INSERT INTO canadianextract (";
			if(PrefB.RandomKeys) {
				command+="CanadianExtractNum,";
			}
			command+="ClaimNum,ToothNum,DateExtraction) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(cur.CanadianExtractNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (cur.ClaimNum)+"', "
				+"'"+POut.PString(cur.ToothNum)+"', "
				+POut.PDate  (cur.DateExtraction)+")";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				cur.CanadianExtractNum=General.NonQ(command,true);
			}
		}

/*update never used*/

//Delete handled in claim.delete, but not critical if left orphanned.

		///<summary>Supply a list of CanadianExtracts, and this function will filter it and return only items with dates.</summary>
		public static List<CanadianExtract> GetWithDates(List<CanadianExtract> missingList){
			List<CanadianExtract> retVal=new List<CanadianExtract>();
			foreach(CanadianExtract ce in missingList){
				if(ce.DateExtraction.Year>1880){
					retVal.Add(ce);
				}
			}
			return retVal;
		}

	


		 
		 
	}
}













