using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class InsSubs{
		///<summary>It's fastest if you supply a sub list that contains the sub, but it also works just fine if it can't initally locate the sub in the list.  You can supply an empty list.  If still not found, returns a new InsSub (probably bad behavior).</summary>
		public static InsSub GetSub(long insSubNum,List<InsSub> subList) {
			//No need to check RemotingRole; no call to db.
			InsSub retVal=new InsSub();
			if(insSubNum==0) {
				return null;
			}
			if(subList==null) {
				subList=new List<InsSub>();
			}
			bool found=false;
			for(int i=0;i<subList.Count;i++) {
				if(subList[i].InsSubNum==insSubNum) {
					found=true;
					retVal=subList[i];
				}
			}
			if(!found) {
				retVal=GetOne(insSubNum);//retVal will now be null if not found
			}
			if(retVal==null) {
				//MessageBox.Show(Lans.g("InsPlans","Database is inconsistent.  Please run the database maintenance tool."));
				return new InsSub();
			}
			return retVal;
		}

		///<summary>Gets one InsSub from the db.</summary>
		public static InsSub GetOne(long insSubNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<InsSub>(MethodBase.GetCurrentMethod(),insSubNum);
			}
			return Crud.InsSubCrud.SelectOne(insSubNum);
		}

		///<summary>Gets new List for the specified family.  The only insSubs it misses are for claims with no current coverage.  These are handled as needed.</summary>
		public static List<InsSub> RefreshForFam(Family Fam) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsSub>>(MethodBase.GetCurrentMethod(),Fam);
			}
			string command=
				"(SELECT * FROM inssub WHERE";
			//subscribers in family
			for(int i=0;i<Fam.ListPats.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" Subscriber="+POut.Long(Fam.ListPats[i].PatNum);
			}
			//in union, distinct is implied
			command+=") UNION (SELECT inssub.* FROM inssub,patplan WHERE insSub.InsSubNum=patplan.InsSubNum AND (";
			for(int i=0;i<Fam.ListPats.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" patplan.PatNum="+POut.Long(Fam.ListPats[i].PatNum);
			}
			//command+=")) ORDER BY DateEffective";//FIXME:UNION-ORDER-BY
			command+=")) ORDER BY 4";//***ORACLE ORDINAL
			return Crud.InsSubCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(InsSub insSub){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				insSub.InsSubNum=Meth.GetLong(MethodBase.GetCurrentMethod(),insSub);
				return insSub.InsSubNum;
			}
			return Crud.InsSubCrud.Insert(insSub);
		}

		///<summary></summary>
		public static void Update(InsSub insSub) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insSub);
				return;
			}
			Crud.InsSubCrud.Update(insSub);
		}

		///<summary>Throws exception if dependencies.  Doesn't delete anything else.</summary>
		public static void Delete(InsSub insSub) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insSub);
				return;
			}
			long subNum=insSub.InsSubNum;
			try {
				ValidateNoKeys(subNum,true);
			}
			catch(ApplicationException ex){
				throw new ApplicationException(Lans.g("FormInsPlan","Not allowed to delete: ")+ex.Message);
			}
			Crud.InsSubCrud.Delete(insSub.InsSubNum);
		}

		/// <summary>Will throw an exception if this InsSub is being used anywhere. Set strict true to test against every check.</summary>
		public static void ValidateNoKeys(long subNum, bool strict){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),subNum,strict);
				return;
			}
			string command;
			string result;
			//claim.InsSubNum/2
			command="SELECT COUNT(*) FROM claim WHERE InsSubNum = "+POut.Long(subNum);
			result=Db.GetScalar(command);
			if(result!="0") {
				throw new ApplicationException(Lans.g("FormInsPlan","Subscriber has existing claims."));
			}
			command="SELECT COUNT(*) FROM claim WHERE InsSubNum2 = "+POut.Long(subNum);
			result=Db.GetScalar(command);
			if(result!="0") {
				throw new ApplicationException(Lans.g("FormInsPlan","Subscriber has existing claims."));
			}
			//claimproc.InsSubNum
			if(strict) {
				command="SELECT COUNT(*) FROM claimproc WHERE InsSubNum = "+POut.Long(subNum);
				result=Db.GetScalar(command);
				if(result!="0") {
					throw new ApplicationException(Lans.g("FormInsPlan","Subscriber has existing claimprocs."));
				}
			}
			//etrans.InsSubNum
			command="SELECT COUNT(*) FROM etrans WHERE InsSubNum = "+POut.Long(subNum);
			result=Db.GetScalar(command);
			if(result!="0") {
				throw new ApplicationException(Lans.g("FormInsPlan","Subscriber has existing etrans entry."));
			}
			//patplan.InsSubNum
			if(strict) {
				command="SELECT COUNT(*) FROM patplan WHERE InsSubNum = "+POut.Long(subNum);
				result=Db.GetScalar(command);
				if(result!="0") {
					throw new ApplicationException(Lans.g("FormInsPlan","Subscriber has existing patplan entry."));
				}
			}
			//payplan.InsSubNum
			command="SELECT COUNT(*) FROM payplan WHERE InsSubNum = "+POut.Long(subNum);
			result=Db.GetScalar(command);
			if(result!="0") {
				throw new ApplicationException(Lans.g("FormInsPlan","Subscriber has existing payplans."));
			}
		}

		///<summary>A quick delete that is only used when cancelling out of a new edit window.</summary>
		public static void Delete(long insSubNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insSubNum);
				return;
			}
			Crud.InsSubCrud.Delete(insSubNum);
		}

		///<summary>Used in FormInsSelectSubscr to get a list of insplans for one subscriber directly from the database.</summary>
		public static List<InsSub> GetListForSubscriber(long subscriber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsSub>>(MethodBase.GetCurrentMethod(),subscriber);
			}
			string command="SELECT * FROM inssub WHERE Subscriber="+POut.Long(subscriber);
			return Crud.InsSubCrud.SelectMany(command);
		}

		///<summary>Only used once.  Gets a list of subscriber names from the database that have the specified plan. Used to display in the insplan window.  The returned list never includes the inssub that we're viewing.</summary>
		public static string[] GetSubscribersForPlan(long planNum,long excludeSub) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),planNum,excludeSub);
			}
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) "
				+"FROM inssub LEFT JOIN patient ON patient.PatNum=inssub.Subscriber "
				+"WHERE inssub.planNum="+POut.Long(planNum)+" "
				+"AND inssub.InsSubNum !="+POut.Long(excludeSub)+" "
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retStr[i]=PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Called from FormInsPlan when user wants to view a benefit note for other subscribers on a plan.  Should never include the current subscriber that the user is editing.  This function will get one note from the database, not including blank notes.  If no note can be found, then it returns empty string.</summary>
		public static string GetBenefitNotes(long planNum,long subNumExclude) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),planNum,subNumExclude);
			}
			string command="SELECT BenefitNotes FROM inssub WHERE BenefitNotes != '' AND PlanNum="+POut.Long(planNum)+" AND InsSubNum !="+POut.Long(subNumExclude)+" ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+="AND ROWNUM<=1";
			}
			else {//Assume MySQL
				command+="LIMIT 1";
			}
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				return "";
			}
			return PIn.String(table.Rows[0][0].ToString());
		}




	}
}