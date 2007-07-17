using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness; 

namespace OpenDental {
	class ClaimCondCodeLog {
		public static ClaimCondCodes CurCondCodes;

		public static ClaimCondCodes GetAllCodes(int ClaimNum){
			string command="SELECT * FROM claimcondcodelog WHERE ClaimNum='" + ClaimNum + "'";
			DataTable table=General.GetTable(command);
			CurCondCodes = new ClaimCondCodes();
			for(int i=0;i<table.Rows.Count;i++){
				CurCondCodes.ClaimCondCodeNum=PIn.PInt(table.Rows[0][0].ToString());
				CurCondCodes.ClaimNum=PIn.PInt(table.Rows[0][1].ToString());
				CurCondCodes.Code0=PIn.PString(table.Rows[0][2].ToString());
				CurCondCodes.Code1=PIn.PString(table.Rows[0][3].ToString());
				CurCondCodes.Code2=PIn.PString(table.Rows[0][4].ToString());
				CurCondCodes.Code3=PIn.PString(table.Rows[0][5].ToString());
				CurCondCodes.Code4=PIn.PString(table.Rows[0][6].ToString());
				CurCondCodes.Code5=PIn.PString(table.Rows[0][7].ToString());
				CurCondCodes.Code6=PIn.PString(table.Rows[0][8].ToString());
				CurCondCodes.Code7=PIn.PString(table.Rows[0][9].ToString());
				CurCondCodes.Code8=PIn.PString(table.Rows[0][10].ToString());
				CurCondCodes.Code9=PIn.PString(table.Rows[0][11].ToString());
				CurCondCodes.Code10=PIn.PString(table.Rows[0][12].ToString());
			}
			return CurCondCodes;
		}
	}
}
