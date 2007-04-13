using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class RxDefs {

		///<summary></summary>
		public static RxDef[] Refresh() {
			string command="SELECT * FROM rxdef ORDER BY Drug";
			DataTable table=General.GetTable(command);
			RxDef[] List=new RxDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RxDef();
				List[i].RxDefNum   = PIn.PInt(table.Rows[i][0].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][2].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][4].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][5].ToString());
			}
			return List;
		}
	

		///<summary></summary>
		public static void Update(RxDef def) {
			string command= "UPDATE rxdef SET " 
				+"Drug = '"    +POut.PString(def.Drug)+"'"
				+",Sig = '"    +POut.PString(def.Sig)+"'"
				+",Disp = '"   +POut.PString(def.Disp)+"'"
				+",Refills = '"+POut.PString(def.Refills)+"'"
				+",Notes = '"  +POut.PString(def.Notes)+"'"
				+" WHERE RxDefNum = '" +POut.PInt(def.RxDefNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(RxDef def) {
			string command= "INSERT INTO rxdef (Drug,Sig,Disp,Refills,Notes) VALUES("
				+"'"+POut.PString(def.Drug)+"', "
				+"'"+POut.PString(def.Sig)+"', "
				+"'"+POut.PString(def.Disp)+"', "
				+"'"+POut.PString(def.Refills)+"', "
				+"'"+POut.PString(def.Notes)+"')";
			def.RxDefNum=General.NonQ(command,true);
		}

		///<summary>Also deletes all RxAlerts that were attached.</summary>
		public static void Delete(RxDef def) {
			string command="DELETE FROM rxalert WHERE RxDefNum="+POut.PInt(def.RxDefNum);
			General.NonQ(command);
			command= "DELETE FROM rxdef "
				+"WHERE rxdefnum = "+POut.PInt(def.RxDefNum);
			General.NonQ(command);
		}




	
	
	
	}

	

	


}













