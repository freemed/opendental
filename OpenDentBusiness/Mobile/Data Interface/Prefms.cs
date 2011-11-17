using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;




namespace OpenDentBusiness.Mobile {
	public class Prefms {
		#region Only used on Patient Portal
			public static PrefmC LoadPreferences(long customerNum) {
				string command="SELECT * FROM preferencem WHERE CustomerNum = "+POut.Long(customerNum); 
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

			/// <summary>Load the preferences form the session. If it is not found inthe session it's loaded from the database</summary>
			public static PrefmC LoadPreferences() {
				PrefmC prefmc=(PrefmC)HttpContext.Current.Session["prefmC"];
				if(prefmc==null) {
					if(HttpContext.Current.Session["Patient"]!=null) {
						long DentalOfficeID=((Patientm)HttpContext.Current.Session["Patient"]).CustomerNum;
						prefmc=LoadPreferences(DentalOfficeID);
					}
				}
				return prefmc;
			}
		#endregion

		#region Only used on Webhostsynch
			///<summary>Returns true if a change was required, or false if no change needed.</summary>
			public void UpdateString(long customerNum,PrefmName prefmName,string newValue) {
				string command="SELECT * FROM preferencem "
					+"WHERE CustomerNum =" +POut.Long(customerNum)+" AND PrefmName = '"+POut.String(prefmName.ToString())+"'";
				DataTable table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					command = "UPDATE preferencem SET "
					+"ValueString = '"+POut.String(newValue)+"' "
					+"WHERE CustomerNum =" +POut.Long(customerNum)+" AND PrefmName = '"+POut.String(prefmName.ToString())+"'";
					Db.NonQ(command);
				}
				else {
					command = "INSERT into preferencem " 
					+"(CustomerNum,PrefmName,ValueString) VALUES "
					+"("+POut.Long(customerNum)+",'"+POut.String(prefmName.ToString())+"','"+POut.String(newValue)+"')";
					Db.NonQ(command);
				}
			}
		#endregion


	}
}