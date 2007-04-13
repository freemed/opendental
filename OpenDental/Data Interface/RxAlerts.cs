using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class RxAlerts {

		///<summary>Gets a list of all RxAlerts for one RxDef.</summary>
		public static RxAlert[] Refresh(int rxDefNum) {
			string command="SELECT * FROM rxalert WHERE RxDefNum="+POut.PInt(rxDefNum);
			DataTable table=General.GetTable(command);
			RxAlert[] List=new RxAlert[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RxAlert();
				List[i].RxAlertNum   = PIn.PInt(table.Rows[i][0].ToString());
				List[i].RxDefNum     = PIn.PInt(table.Rows[i][1].ToString());
				List[i].DiseaseDefNum= PIn.PInt(table.Rows[i][2].ToString());
			}
			return List;
		}
	

		///<summary></summary>
		public static void Update(RxAlert alert) {
			string command="UPDATE rxalert SET " 
				+"RxDefNum = '"      +POut.PInt   (alert.RxDefNum)+"'"
				+",DiseaseDefNum = '"+POut.PInt   (alert.DiseaseDefNum)+"'"
				+" WHERE RxAlertNum  ='"+POut.PInt   (alert.RxAlertNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(RxAlert alert) {
			string command="INSERT INTO rxalert (RxDefNum,DiseaseDefNum) VALUES("
				+"'"+POut.PInt   (alert.RxDefNum)+"', "
				+"'"+POut.PInt   (alert.DiseaseDefNum)+"')";
			alert.RxAlertNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Delete(RxAlert alert) {
			string command="DELETE FROM rxalert WHERE RxAlertNum ="+POut.PInt(alert.RxAlertNum);
			General.NonQ(command);
		}

	
	

		
		
		
	}

		



		
	

	

	


}










