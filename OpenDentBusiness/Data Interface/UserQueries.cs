using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

///<summary></summary>
	public class UserQueries{
		///<summary></summary>
		private static UserQuery[] list;
		///<summary></summary>
		public static bool IsSelected;

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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command =
				"SELECT querynum,description,filename,querytext"
				+" FROM userquery"
				//+" WHERE hidden != '1'";
				+" ORDER BY description";
			DataTable table=Db.GetTable(command);
			List=new UserQuery[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new UserQuery();
				List[i].QueryNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				List[i].FileName    = PIn.PString(table.Rows[i][2].ToString());
				List[i].QueryText   = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="INSERT INTO userquery (description,filename,querytext) VALUES("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.FileName)+"', "
				+"'"+POut.PString(Cur.QueryText)+"')";
			//MessageBox.Show(string command);
			Db.NonQ(command);
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













