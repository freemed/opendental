using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RxDefs {

		///<summary></summary>
		public static RxDef[] Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<RxDef[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM rxdef ORDER BY Drug";
			DataTable table=Db.GetTable(command);
			RxDef[] List=new RxDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RxDef();
				List[i].RxDefNum    = PIn.Long(table.Rows[i][0].ToString());
				List[i].Drug        = PIn.String(table.Rows[i][1].ToString());
				List[i].Sig         = PIn.String(table.Rows[i][2].ToString());
				List[i].Disp        = PIn.String(table.Rows[i][3].ToString());
				List[i].Refills     = PIn.String(table.Rows[i][4].ToString());
				List[i].Notes       = PIn.String(table.Rows[i][5].ToString());
				List[i].IsControlled= PIn.Bool  (table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(RxDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.RxDefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(RxDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.RxDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.RxDefNum;
			}
			return Crud.RxDefCrud.Insert(def);
		}

		///<summary>Also deletes all RxAlerts that were attached.</summary>
		public static void Delete(RxDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="DELETE FROM rxalert WHERE RxDefNum="+POut.Long(def.RxDefNum);
			Db.NonQ(command);
			command= "DELETE FROM rxdef WHERE RxDefNum = "+POut.Long(def.RxDefNum);
			Db.NonQ(command);
		}




	
	
	
	}

	

	


}













