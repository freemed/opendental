using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CovSpans {

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemovelyIfNeeded().
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
			CovSpanC.List=new CovSpan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				CovSpanC.List[i]=new CovSpan();
				CovSpanC.List[i].CovSpanNum  = PIn.PInt(table.Rows[i][0].ToString());
				CovSpanC.List[i].CovCatNum   = PIn.PInt(table.Rows[i][1].ToString());
				CovSpanC.List[i].FromCode    = PIn.PString(table.Rows[i][2].ToString());
				CovSpanC.List[i].ToCode      = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		private static void Update(CovSpan span) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),span);
				return;
			}
			string command="UPDATE covspan SET "
				+"CovCatNum = '"+POut.PInt   (span.CovCatNum)+"'"
				+",FromCode = '"+POut.PString(span.FromCode)+"'"
				+",ToCode = '"  +POut.PString(span.ToCode)+"'"
				+" WHERE CovSpanNum = '"+POut.PInt(span.CovSpanNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(CovSpan span) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),span);
				return;
			}
			string command="INSERT INTO covspan (CovCatNum,"
				+"FromCode,ToCode) VALUES("
				+"'"+POut.PInt   (span.CovCatNum)+"', "
				+"'"+POut.PString(span.FromCode)+"', "
				+"'"+POut.PString(span.ToCode)+"')";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(CovSpan span, bool IsNew){
			//No need to check RemotingRole; no call to db.
			if(span.FromCode=="" || span.ToCode=="") {
				throw new ApplicationException(Lan.g("FormInsSpanEdit","Codes not allowed to be blank."));
			}
			if(String.Compare(span.ToCode,span.FromCode)<0){
				throw new ApplicationException(Lan.g("FormInsSpanEdit","From Code must be less than To Code.  Remember that the comparison is alphabetical, not numeric.  For instance, 100 would come before 2, but after 02."));
			}
			if(IsNew){
				Insert(span);
			}
			else{
				Update(span);
			}
		}

		///<summary></summary>
		public static void Delete(CovSpan span) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),span);
				return;
			}
			string command="DELETE FROM covspan"
				+" WHERE CovSpanNum = '"+POut.PInt(span.CovSpanNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int GetCat(string myCode){
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<CovSpanC.List.Length;i++){
				if(String.Compare(myCode,CovSpanC.List[i].FromCode)>=0
					&& String.Compare(myCode,CovSpanC.List[i].ToCode)<=0){
					retVal=CovSpanC.List[i].CovCatNum;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static CovSpan[] GetForCat(int catNum){
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

	}

	


}









