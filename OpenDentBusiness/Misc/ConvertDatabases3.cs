using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace OpenDentBusiness {
	public partial class ConvertDatabases {
		public static System.Version LatestVersion=new Version("13.2.0.0");//This value must be changed when a new conversion is to be triggered.

		private static void To13_2_0() {
			if(FromVersion<new Version("13.2.0.0")) {
				string command;
				//Add TaskEdit permission to everyone------------------------------------------------------
				command="SELECT DISTINCT UserGroupNum FROM grouppermission";
				DataTable table=Db.GetTable(command);
				long groupNum;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					for(int i=0;i<table.Rows.Count;i++) {
						groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
						command="INSERT INTO grouppermission (UserGroupNum,PermType) "
							+"VALUES("+POut.Long(groupNum)+","+POut.Int((int)Permissions.TaskEdit)+")";
						Db.NonQ(command);
					}
				}
				else {//oracle
					for(int i=0;i<table.Rows.Count;i++) {
						groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
						command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
							+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+","+POut.Int((int)Permissions.TaskEdit)+")";
						Db.NonQ(command);
					}
				}
		



				command="UPDATE preference SET ValueString = '13.2.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To13_3_0();
		}





	}
}



