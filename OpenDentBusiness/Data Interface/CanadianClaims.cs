using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CanadianClaims{
	
		///<summary>Will frequently return null when no canadianClaim saved yet.</summary>
		public static CanadianClaim GetForClaim(long claimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<CanadianClaim>(MethodBase.GetCurrentMethod(),claimNum);
			}
			string command="SELECT * FROM canadianclaim WHERE ClaimNum="+POut.Long(claimNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			CanadianClaim retVal=new CanadianClaim();
			retVal.ClaimNum=claimNum;
			retVal.MaterialsForwarded =PIn.String(table.Rows[0][1].ToString());
			retVal.ReferralProviderNum=PIn.String(table.Rows[0][2].ToString());
			retVal.ReferralReason     =PIn.Int   (table.Rows[0][3].ToString());
			//retVal.CardSequenceNumber =PIn.PInt   (table.Rows[0][4].ToString());
			retVal.SecondaryCoverage  =PIn.String(table.Rows[0][4].ToString());
			retVal.IsInitialLower     =PIn.String(table.Rows[0][5].ToString());
			retVal.DateInitialLower   =PIn.Date  (table.Rows[0][6].ToString());
			retVal.MandProsthMaterial =PIn.Int   (table.Rows[0][7].ToString());
			retVal.IsInitialUpper     =PIn.String(table.Rows[0][8].ToString());
			retVal.DateInitialUpper   =PIn.Date  (table.Rows[0][9].ToString());
			retVal.MaxProsthMaterial  =PIn.Int   (table.Rows[0][10].ToString());
			retVal.EligibilityCode    =PIn.Int   (table.Rows[0][11].ToString());
			retVal.SchoolName         =PIn.String(table.Rows[0][12].ToString());
			retVal.PayeeCode          =PIn.Int   (table.Rows[0][13].ToString());
			return retVal;
		}

		///<summary>An important part of creating a "canadian claim" is setting all the missing teeth.  So this must be passed in.  It is preferrable to not include any dates with the missing teeth.  This will force the user to enter dates.</summary>
		public static CanadianClaim Insert(long claimNum,List<CanadianExtract> missingList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<CanadianClaim>(MethodBase.GetCurrentMethod(),missingList);
			}
			CanadianExtracts.UpdateForClaim(claimNum,missingList);
			//Random primary keys do not need to be checked here, because of 1-1 relationship with claimNum
			string command="INSERT INTO canadianclaim (ClaimNum) VALUES("
				+"'"+POut.Long   (claimNum)+"')";
				//+"'"+POut.PString(schoolName)+"')";
			Db.NonQ(command);
			CanadianClaim retVal=new CanadianClaim();
			retVal.ClaimNum=claimNum;
			retVal.MaterialsForwarded="";
			return retVal;
		}

		///<summary></summary>
		public static void Update(CanadianClaim Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="UPDATE canadianclaim SET "
				+ "MaterialsForwarded = '"+POut.String(Cur.MaterialsForwarded)+"' "
				+ ",ReferralProviderNum='"+POut.String(Cur.ReferralProviderNum)+"' "
				+ ",ReferralReason = '"   +POut.Long   (Cur.ReferralReason)+"' "
				//+ ",CardSequenceNumber ='"+POut.PInt   (Cur.CardSequenceNumber)+"' "
				+ ",SecondaryCoverage = '"+POut.String(Cur.SecondaryCoverage)+"' "
				+ ",IsInitialLower = '"   +POut.String(Cur.IsInitialLower)+"' "
				+ ",DateInitialLower = " +POut.Date  (Cur.DateInitialLower)+" "
				+ ",MandProsthMaterial ='"+POut.Long   (Cur.MandProsthMaterial)+"' "
				+ ",IsInitialUpper = '"   +POut.String(Cur.IsInitialUpper)+"' "
				+ ",DateInitialUpper = " +POut.Date  (Cur.DateInitialUpper)+" "
				+ ",MaxProsthMaterial = '"+POut.Long   (Cur.MaxProsthMaterial)+"' "
				+ ",EligibilityCode = '"  +POut.Long   (Cur.EligibilityCode)+"' "
				+ ",SchoolName = '"       +POut.String(Cur.SchoolName)+"' "
				+ ",PayeeCode = '"        +POut.Long   (Cur.PayeeCode)+"' "
				+"WHERE ClaimNum = '"+POut.Long(Cur.ClaimNum)+"'";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		
/*
 * Deletions are handled in Claims.Delete.
 */


	


		 
		 
	}
}













