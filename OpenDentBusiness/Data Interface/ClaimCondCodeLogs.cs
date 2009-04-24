using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness {
	public class ClaimCondCodeLogs {
		public static ClaimCondCodeLog CurCondCodeLog;

		public static ClaimCondCodeLog GetOne(int ClaimNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClaimCondCodeLog>(MethodBase.GetCurrentMethod(),ClaimNum);
			}
			string command="SELECT * FROM claimcondcodelog WHERE ClaimNum='" + ClaimNum + "'";
			DataTable table=Db.GetTable(command);
			CurCondCodeLog = new ClaimCondCodeLog();
			if(table.Rows.Count>0){
			//for(int i=0;i<table.Rows.Count;i++){
				CurCondCodeLog.ClaimCondCodeNum=PIn.PInt(table.Rows[0][0].ToString());
				CurCondCodeLog.ClaimNum=PIn.PInt(table.Rows[0][1].ToString());
				CurCondCodeLog.Code0=PIn.PString(table.Rows[0][2].ToString());
				CurCondCodeLog.Code1=PIn.PString(table.Rows[0][3].ToString());
				CurCondCodeLog.Code2=PIn.PString(table.Rows[0][4].ToString());
				CurCondCodeLog.Code3=PIn.PString(table.Rows[0][5].ToString());
				CurCondCodeLog.Code4=PIn.PString(table.Rows[0][6].ToString());
				CurCondCodeLog.Code5=PIn.PString(table.Rows[0][7].ToString());
				CurCondCodeLog.Code6=PIn.PString(table.Rows[0][8].ToString());
				CurCondCodeLog.Code7=PIn.PString(table.Rows[0][9].ToString());
				CurCondCodeLog.Code8=PIn.PString(table.Rows[0][10].ToString());
				CurCondCodeLog.Code9=PIn.PString(table.Rows[0][11].ToString());
				CurCondCodeLog.Code10=PIn.PString(table.Rows[0][12].ToString());
			}
			return CurCondCodeLog;
		}
	}
}
