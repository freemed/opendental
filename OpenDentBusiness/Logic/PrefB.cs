using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	public class PrefB {
		
		public static DataSet Refresh(){
			string command="SELECT * FROM preference";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			DataSet retVal=new DataSet();
			retVal.Tables.Add(table);
			FillHList(table);
			return retVal;
		}

		///<summary></summary>
		public static void FillHList(DataTable table){
			PrefC.HList=new Hashtable();
			Pref pref;
			for(int i=0;i<table.Rows.Count;i++) {
				pref=new Pref();
				pref.PrefName=PIn.PString(table.Rows[i][0].ToString());
				pref.ValueString=PIn.PString(table.Rows[i][1].ToString());
				PrefC.HList.Add(pref.PrefName,pref);
			}
		}



	}
}
