using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;




namespace OpenDentBusiness.Mobile {
	public class Prefms {
		public static PrefmC LoadPreferences(long customerNum) {
			string command="SELECT * FROM preferencem WHERE CustomerNum = "+POut.Long(customerNum); ;
			DataTable table=Db.GetTable(command);
			Prefm prefm=new Prefm(); 
			PrefmC prefmc=new PrefmC();
			for(int i=0;i<table.Rows.Count;i++) {
				prefm=new Prefm();
				if(table.Columns.Contains("PrefmNum")) {
					prefm.PrefmNum=PIn.Long(table.Rows[i]["PrefmNum"].ToString());
				}
				prefm.PrefmName=PIn.String(table.Rows[i]["PrefmName"].ToString());
				prefm.ValueString=PIn.String(table.Rows[i]["ValueString"].ToString());
				prefmc.Dict.Add(prefm.PrefmName,prefm);
			}
			HttpContext.Current.Session["prefmC"]=prefmc;
			return prefmc;
		}






	}
}