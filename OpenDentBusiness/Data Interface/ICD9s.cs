using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ICD9s{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ICD9s.</summary>
		private static List<ICD9> listt;

		///<summary>A list of all ICD9s.</summary>
		public static List<ICD9> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM icd9 ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ICD9";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ICD9Crud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ICD9> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ICD9>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM icd9 WHERE PatNum = "+POut.Long(patNum);
			return Crud.ICD9Crud.SelectMany(command);
		}
		*/
		///<summary>Gets one ICD9 from the db.</summary>
		public static ICD9 GetOne(long iCD9Num){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ICD9>(MethodBase.GetCurrentMethod(),iCD9Num);
			}
			return Crud.ICD9Crud.SelectOne(iCD9Num);
		}

		///<summary></summary>
		public static long Insert(ICD9 icd9){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				icd9.ICD9Num=Meth.GetLong(MethodBase.GetCurrentMethod(),icd9);
				return icd9.ICD9Num;
			}
			return Crud.ICD9Crud.Insert(icd9);
		}

		///<summary></summary>
		public static void Update(ICD9 icd9) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),icd9);
				return;
			}
			Crud.ICD9Crud.Update(icd9);
		}

		///<summary></summary>
		public static void Delete(long icd9Num) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),icd9Num);
				return;
			}
			string command= "DELETE FROM icd9 WHERE ICD9Num = "+POut.Long(icd9Num);
			Db.NonQ(command);
		}

		public static List<long> GetChangedSinceICD9Nums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT ICD9Num FROM icd9 WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> icd9Nums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				icd9Nums.Add(PIn.Long(dt.Rows[i]["ICD9Num"].ToString()));
			}
			return icd9Nums;
		}

		///<summary>Used along with GetChangedSinceICD9Nums</summary>
		public static List<ICD9> GetMultICD9s(List<long> icd9Nums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ICD9>>(MethodBase.GetCurrentMethod(),icd9Nums);
			}
			string strICD9Nums="";
			DataTable table;
			if(icd9Nums.Count>0) {
				for(int i=0;i<icd9Nums.Count;i++) {
					if(i>0) {
						strICD9Nums+="OR ";
					}
					strICD9Nums+="ICD9Num='"+icd9Nums[i].ToString()+"' ";
				}
				string command="SELECT * FROM icd9 WHERE "+strICD9Nums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			ICD9[] multICD9s=Crud.ICD9Crud.TableToList(table).ToArray();
			List<ICD9> icd9List=new List<ICD9>(multICD9s);
			return icd9List;
		}

	}
}