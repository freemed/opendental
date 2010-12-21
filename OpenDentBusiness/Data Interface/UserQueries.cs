using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

///<summary></summary>
	public class UserQueries{
		///<summary>Only used in UI.</summary>
		private static UserQuery[] list;
		//<summary></summary>
		//public static bool IsSelected;

		public static UserQuery[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					Refresh();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary></summary>
		public static void Refresh(){
			//No need to check RemotingRole; no call to db.
			DataTable table=GetAllUserQueries();
			list=new UserQuery[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				list[i]=new UserQuery();
				list[i].QueryNum    = PIn.Long   (table.Rows[i][0].ToString());
				list[i].Description = PIn.String(table.Rows[i][1].ToString());
				list[i].FileName    = PIn.String(table.Rows[i][2].ToString());
				list[i].QueryText   = PIn.String(table.Rows[i][3].ToString());
			}
		}

		public static DataTable GetAllUserQueries() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command =
				"SELECT querynum,description,filename,querytext"
				+" FROM userquery"
				//+" WHERE hidden != '1'";
				+" ORDER BY description";
			DataTable table=Db.GetTable(command);
			return table;
		}

		///<summary></summary>
		public static long Insert(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.QueryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.QueryNum;
			}
			return Crud.UserQueryCrud.Insert(Cur);
		}
		
		///<summary></summary>
		public static void Delete(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from userquery WHERE querynum = '"+POut.Long(Cur.QueryNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Update(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.UserQueryCrud.Update(Cur);
		}
	}

	

	
}













