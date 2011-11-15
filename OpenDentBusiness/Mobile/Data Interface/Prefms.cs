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


		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateString(PrefName prefName,string newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.Dict.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetString(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.String(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else {
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=newValue;
			PrefC.Dict[prefName.ToString()]=pref;
			return retVal;
		}



	}
}