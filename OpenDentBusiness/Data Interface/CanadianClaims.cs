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
			return Crud.CanadianClaimCrud.SelectOne(command);
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
			retVal.ReferralProviderNum="";
			retVal.SecondaryCoverage="";
			retVal.IsInitialLower="";
			retVal.IsInitialUpper="";
			retVal.SchoolName="";
			return retVal;
		}

		///<summary></summary>
		public static void Update(CanadianClaim canadianClaim){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),canadianClaim);
				return;
			}
			Crud.CanadianClaimCrud.Update(canadianClaim);
		}

		
/*
 * Deletions are handled in Claims.Delete.
 */


	


		 
		 
	}
}













