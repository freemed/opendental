using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentMobile {
	public class Prefs {

		public static void Refresh(){
			PrefC.HList=new Dictionary<string,string>();
			string command="SELECT * FROM preference";
			DataTable table=General.GetTable(command);
			string prefName;
			string valueString;
			for(int i=0;i<table.Rows.Count;i++) {
				prefName=PIn.PString(table.Rows[i]["PrefName"].ToString());
				valueString=PIn.PString(table.Rows[i]["ValueString"].ToString());
				//no need to load up the comments.  Especially since this will fail when user first runs version 5.8.
					//pref.Comments=PIn.PString(table.Rows[i][2].ToString());
				PrefC.HList.Add(prefName,valueString);
			}
		}

		


	}
}
