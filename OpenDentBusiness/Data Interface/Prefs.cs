using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Prefs{
		public static DataTable RefreshCache(){
			string command="SELECT * FROM preference";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			PrefC.HList=new Hashtable();
			Pref pref;
			for(int i=0;i<table.Rows.Count;i++) {
				pref=new Pref();
				pref.PrefName=PIn.PString(table.Rows[i][0].ToString());
				pref.ValueString=PIn.PString(table.Rows[i][1].ToString());
				PrefC.HList.Add(pref.PrefName,pref);
			}
		}

		///<summary></summary>
		public static void Update(Pref pref) {
			string command= "UPDATE preference SET "
				+"valuestring = '"  +POut.PString(pref.ValueString)+"'"
				+" WHERE prefname = '"+POut.PString(pref.PrefName)+"'";
			General.NonQ(command);
		}

		///<summary>Updates a pref of type int.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateInt(string prefName,int newValue) {
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetInt(prefName)==newValue) {
				return false;//no change needed
			}
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.PInt(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Updates a pref of type double.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDouble(string prefName,double newValue) {
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetDouble(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PDouble(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(string prefName,bool newValue) {
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetBool(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PBool(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateString(string prefName,string newValue) {
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetString(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PString(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		

		

	}

	


	


}










