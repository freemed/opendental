using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>Letters are refreshed as local data.</summary>
	public class Letters{
		///<summary>List of</summary>
		private static Letter[] list;

		public static Letter[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * from letter ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Letter";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=Crud.LetterCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static void Update(Letter Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.LetterCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(Letter Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.LetterNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.LetterNum;
			}
			return Crud.LetterCrud.Insert(Cur);
		}

		///<summary></summary>
		public static void Delete(Letter Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE from letter WHERE LetterNum = '"+Cur.LetterNum.ToString()+"'";
			Db.NonQ(command);
		}

		
	}

	
	

}













