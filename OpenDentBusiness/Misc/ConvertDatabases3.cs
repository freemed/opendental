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
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS wikilistheaderwidth";
					Db.NonQ(command);
					command=@"CREATE TABLE wikilistheaderwidth (
						WikiListHeaderWidthNum bigint NOT NULL auto_increment PRIMARY KEY,
						ListName varchar(255) NOT NULL,
						ColName varchar(255) NOT NULL,
						ColWidth int NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					//WikiLists Not Supported in Oracle. but we're still adding this table here for consistency of the schema.  Also, we might turn on this feature for Oracle some day.
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE wikilistheaderwidth'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE wikilistheaderwidth (
						WikiListHeaderWidthNum number(20) NOT NULL,
						ListName varchar2(255),
						ColName varchar2(255),
						ColWidth number(11) NOT NULL,
						CONSTRAINT wikilistheaderwidth_WikiListHe PRIMARY KEY (WikiListHeaderWidthNum)
						)";
					Db.NonQ(command);
				}



				command="UPDATE preference SET ValueString = '13.2.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To13_3_0();
		}





	}
}



