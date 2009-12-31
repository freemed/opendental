using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class UserGroups {
		private static UserGroup[] list;

		///<summary>A list of all user groups, ordered by description.</summary>
		public static UserGroup[] List {
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

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from usergroup ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="UserGroup";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new UserGroup[table.Rows.Count];
			for(int i=0;i<list.Length;i++) {
				list[i]=new UserGroup();
				list[i].UserGroupNum  = PIn.Long(table.Rows[i][0].ToString());
				list[i].Description   = PIn.String(table.Rows[i][1].ToString());
			}
		}

		///<summary></summary>
		public static void Update(UserGroup group){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),group);
				return;
			}
			string command= "UPDATE usergroup SET " 
				+"Description = '"  +POut.String(group.Description)+"'"
				+" WHERE UserGroupNum = '"+POut.Long(group.UserGroupNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(UserGroup group) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				group.UserGroupNum=Meth.GetLong(MethodBase.GetCurrentMethod(),group);
				return group.UserGroupNum;
			}
			if(PrefC.RandomKeys) {
				group.UserGroupNum=ReplicationServers.GetKey("usergroup","UserGroupNum");
			}
			string command="INSERT INTO usergroup (";
			if(PrefC.RandomKeys) {
				command+="UserGroupNum,";
			}
			command+="Description) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(group.UserGroupNum)+", ";
			}
			command+=
				"'"+POut.String(group.Description)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				group.UserGroupNum=Db.NonQ(command,true);
			}
			return group.UserGroupNum;
		}

		///<summary>Checks for dependencies first</summary>
		public static void Delete(UserGroup group){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),group);
				return;
			}
			string command="SELECT COUNT(*) FROM userod WHERE UserGroupNum='"
				+POut.Long(group.UserGroupNum)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lans.g("UserGroups","Must move users to another group first."));
			}
			command= "DELETE FROM usergroup WHERE UserGroupNum='"
				+POut.Long(group.UserGroupNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static UserGroup GetGroup(long userGroupNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum==userGroupNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		

	}
 
	

	
}













