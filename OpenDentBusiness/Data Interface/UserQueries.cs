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
				list[i].QueryNum    = PIn.PInt   (table.Rows[i][0].ToString());
				list[i].Description = PIn.PString(table.Rows[i][1].ToString());
				list[i].FileName    = PIn.PString(table.Rows[i][2].ToString());
				list[i].QueryText   = PIn.PString(table.Rows[i][3].ToString());
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
				Cur.QueryNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.QueryNum;
			}
			if(PrefC.RandomKeys) {
				Cur.QueryNum=ReplicationServers.GetKey("userquery","QueryNum");
			}
			string command="INSERT INTO userquery (";
			if(PrefC.RandomKeys) {
				command+="QueryNum,";
			}
			command+="description,filename,querytext) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PInt(Cur.QueryNum)+", ";
			}
			command+=
				 "'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.FileName)+"', "
				+"'"+POut.PString(Cur.QueryText)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				Cur.QueryNum=Db.NonQ(command,true);
			}
			return Cur.QueryNum;
		}
		
		///<summary></summary>
		public static void Delete(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from userquery WHERE querynum = '"+POut.PInt(Cur.QueryNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Update(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE userquery SET "
				+ "description = '" +POut.PString(Cur.Description)+"'"
				+ ",filename = '"    +POut.PString(Cur.FileName)+"'"
				+",querytext = '"   +POut.PString(Cur.QueryText)+"'"
				+" WHERE querynum = '"+POut.PInt(Cur.QueryNum)+"'";
			Db.NonQ(command);
		}
	}

	

	
}













