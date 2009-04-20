using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Prefs{
		public static DataTable RefreshCache() {
			string command="SELECT * FROM preference";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Pref";
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
				//no need to load up the comments.  Especially since this will fail when user first runs version 5.8.
					//pref.Comments=PIn.PString(table.Rows[i][2].ToString());
				PrefC.HList.Add(pref.PrefName,pref);
			}
		}

		///<summary></summary>
		public static void Update(Pref pref) {
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.PString(pref.ValueString)+"' "
				//+",Comments = '"  +POut.PString(pref.Comments)+"' "
				+" WHERE PrefName = '"+POut.PString(pref.PrefName)+"'";
			Db.NonQ(command);
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
			Db.NonQ(command);
			Pref pref=new Pref();
			pref.PrefName=prefName;
			pref.ValueString=newValue.ToString();
			PrefC.HList[prefName]=pref;//in some cases, we just want to change the pref in local memory instead of doing a refresh afterwards.
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
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(string prefName,bool newValue) {
			return UpdateBool(prefName,newValue,false);
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(string prefName,bool newValue,bool isForced) {
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(!isForced && PrefC.GetBool(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PBool(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateString(string prefName,string newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetString(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PString(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				//Result of Meth is ignored.
				Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
				//doesn't exit out of this method here.
			}
			else {
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName;
			pref.ValueString=newValue;
			PrefC.HList[prefName]=pref;
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDateT(string prefName,DateTime newValue) {
			if(!PrefC.HList.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetDateT(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PDateT(newValue,false)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			Db.NonQ(command);
			Pref pref=new Pref();
			pref.PrefName=prefName;
			pref.ValueString=POut.PDateT(newValue,false);
			PrefC.HList[prefName]=pref;//in some cases, we just want to change the pref in local memory instead of doing a refresh afterwards.
			return true;
		}


		

	}

	


	


}










