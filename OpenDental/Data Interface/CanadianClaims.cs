using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class CanadianClaims{
	
		///<summary>Will frequently return null when no canadianClaim saved yet.</summary>
		public static CanadianClaim GetForClaim(int claimNum){
			string command="SELECT * FROM canadianclaim WHERE ClaimNum="+POut.PInt(claimNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return null;
			}
			CanadianClaim retVal=new CanadianClaim();
			retVal.ClaimNum=claimNum;
			retVal.MaterialsForwarded =PIn.PString(table.Rows[0][1].ToString());
			retVal.ReferralProviderNum=PIn.PString(table.Rows[0][2].ToString());
			retVal.ReferralReason     =PIn.PInt   (table.Rows[0][3].ToString());
			//retVal.CardSequenceNumber =PIn.PInt   (table.Rows[0][4].ToString());
			retVal.SecondaryCoverage  =PIn.PString(table.Rows[0][4].ToString());
			retVal.IsInitialLower     =PIn.PString(table.Rows[0][5].ToString());
			retVal.DateInitialLower   =PIn.PDate  (table.Rows[0][6].ToString());
			retVal.MandProsthMaterial =PIn.PInt   (table.Rows[0][7].ToString());
			retVal.IsInitialUpper     =PIn.PString(table.Rows[0][8].ToString());
			retVal.DateInitialUpper   =PIn.PDate  (table.Rows[0][9].ToString());
			retVal.MaxProsthMaterial  =PIn.PInt   (table.Rows[0][10].ToString());
			retVal.EligibilityCode    =PIn.PInt   (table.Rows[0][11].ToString());
			retVal.SchoolName         =PIn.PString(table.Rows[0][12].ToString());
			retVal.PayeeCode          =PIn.PInt   (table.Rows[0][13].ToString());
			return retVal;
		}

		///<summary>An important part of creating a "canadian claim" is setting all the missing teeth.  So this must be passed in.  It is preferrable to not include any dates with the missing teeth.  This will force the user to enter dates.</summary>
		public static CanadianClaim Insert(int claimNum, List<CanadianExtract> missingList){
			CanadianExtracts.UpdateForClaim(claimNum,missingList);
			string command="INSERT INTO canadianclaim (ClaimNum) VALUES("
				+"'"+POut.PInt   (claimNum)+"')";
				//+"'"+POut.PString(schoolName)+"')";
			General.NonQ(command);
			CanadianClaim retVal=new CanadianClaim();
			retVal.ClaimNum=claimNum;
			retVal.MaterialsForwarded="";
			return retVal;
		}



		///<summary></summary>
		public static void Update(CanadianClaim Cur){
			string command="UPDATE canadianclaim SET "
				+ "MaterialsForwarded = '"+POut.PString(Cur.MaterialsForwarded)+"' "
				+ ",ReferralProviderNum='"+POut.PString(Cur.ReferralProviderNum)+"' "
				+ ",ReferralReason = '"   +POut.PInt   (Cur.ReferralReason)+"' "
				//+ ",CardSequenceNumber ='"+POut.PInt   (Cur.CardSequenceNumber)+"' "
				+ ",SecondaryCoverage = '"+POut.PString(Cur.SecondaryCoverage)+"' "
				+ ",IsInitialLower = '"   +POut.PString(Cur.IsInitialLower)+"' "
				+ ",DateInitialLower = " +POut.PDate  (Cur.DateInitialLower)+" "
				+ ",MandProsthMaterial ='"+POut.PInt   (Cur.MandProsthMaterial)+"' "
				+ ",IsInitialUpper = '"   +POut.PString(Cur.IsInitialUpper)+"' "
				+ ",DateInitialUpper = " +POut.PDate  (Cur.DateInitialUpper)+" "
				+ ",MaxProsthMaterial = '"+POut.PInt   (Cur.MaxProsthMaterial)+"' "
				+ ",EligibilityCode = '"  +POut.PInt   (Cur.EligibilityCode)+"' "
				+ ",SchoolName = '"       +POut.PString(Cur.SchoolName)+"' "
				+ ",PayeeCode = '"        +POut.PInt   (Cur.PayeeCode)+"' "
				+"WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		
/*
 * Deletions are handled in Claims.Delete.
 */


	


		 
		 
	}
}













