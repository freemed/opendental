using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CovSpans {

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * FROM covspan"
				+" ORDER BY FromCode";
			//+" ORDER BY CovCatNum";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="CovSpan";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			CovSpanC.List=Crud.CovSpanCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static void Update(CovSpan span) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),span);
				return;
			}
			Validate(span);
			Crud.CovSpanCrud.Update(span);
			return;
		}

		///<summary></summary>
		public static long Insert(CovSpan span) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				span.CovSpanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),span);
				return span.CovSpanNum;
			}
			Validate(span);
			return Crud.CovSpanCrud.Insert(span);
		}

		///<summary></summary>
		private static void Validate(CovSpan span){
			//No need to check RemotingRole; no call to db.
			if(span.FromCode=="" || span.ToCode=="") {
				throw new ApplicationException(Lans.g("FormInsSpanEdit","Codes not allowed to be blank."));
			}
			if(String.Compare(span.ToCode,span.FromCode)<0){
				throw new ApplicationException(Lans.g("FormInsSpanEdit","From Code must be less than To Code.  Remember that the comparison is alphabetical, not numeric.  For instance, 100 would come before 2, but after 02."));
			}
		}

		///<summary></summary>
		public static void Delete(CovSpan span) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),span);
				return;
			}
			string command="DELETE FROM covspan"
				+" WHERE CovSpanNum = '"+POut.Long(span.CovSpanNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void DeleteForCat(long covCatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),covCatNum);
				return;
			}
			string command="DELETE FROM covspan WHERE CovCatNum = "+POut.Long(covCatNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long GetCat(string myCode){
			//No need to check RemotingRole; no call to db.
			long retVal=0;
			for(int i=0;i<CovSpanC.List.Length;i++){
				if(String.Compare(myCode,CovSpanC.List[i].FromCode)>=0
					&& String.Compare(myCode,CovSpanC.List[i].ToCode)<=0){
					retVal=CovSpanC.List[i].CovCatNum;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static CovSpan[] GetForCat(long catNum){
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<CovSpanC.List.Length;i++){
				if(CovSpanC.List[i].CovCatNum==catNum){
					AL.Add(CovSpanC.List[i].Copy());
				}
			}
			CovSpan[] retVal=new CovSpan[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>If the supplied code falls within any of the supplied spans, then returns true.</summary>
		public static bool IsCodeInSpans(string strProcCode,CovSpan[] covSpanArray) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<covSpanArray.Length;i++) {
				if(String.Compare(strProcCode,covSpanArray[i].FromCode)>=0
					&& String.Compare(strProcCode,covSpanArray[i].ToCode)<=0) {
					return true;
				}
			}
			return false;
		}

	}

	


}









