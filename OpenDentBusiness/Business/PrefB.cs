using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	public class PrefB {
		///<summary></summary>
		public static Hashtable HList;
		
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
			HList=new Hashtable();
			Pref pref;
			for(int i=0;i<table.Rows.Count;i++) {
				pref=new Pref();
				pref.PrefName=PIn.PString(table.Rows[i][0].ToString());
				pref.ValueString=PIn.PString(table.Rows[i][1].ToString());
				HList.Add(pref.PrefName,pref);
			}
		}

		///<summary>This property is just a shortcut to this pref to make typing faster.  This pref is used a lot.</summary>
		public static bool RandomKeys {
			get {
				return GetBool("RandomPrimaryKeys");
			}
		}

		///<summary>This property is just a shortcut to this pref to make typing faster.  This pref is used a lot.</summary>
		public static bool UsingAtoZfolder {
			get {
				return !GetBool("AtoZfolderNotRequired");
			}
		}

		/*
		///<summary></summary>
		public static bool CheckMySqlVersion50(){
			string command="SELECT @@version";
			DataConnection dcon=new DataConnection();
			string version=dcon.GetCount(command);
			if(version.Substring(0,3)=="5.0"){
				return true;
			}
			MessageBox.Show("This program will only work with MySQL version 5.0.  Closing program.");
			return false;
		}*/	

		///<summary>Gets a pref of type int.</summary>
		public static int GetInt(string prefName) {
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PInt(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type double.</summary>
		public static double GetDouble(string prefName) {
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDouble(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type bool.</summary>
		public static bool GetBool(string prefName) {
			if(HList==null){
				Refresh();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PBool(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type string.</summary>
		public static string GetString(string prefName) {
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return ((Pref)HList[prefName]).ValueString;
		}

		///<summary>Gets a pref of type date.</summary>
		public static DateTime GetDate(string prefName) {
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDate(((Pref)HList[prefName]).ValueString);
		}

		
		

		

		

		
		/*
		///<summary></summary>
		public static void FlushAndLock() {
			try {
				con.Open();
				cmd.CommandText="FLUSH TABLES WITH READ LOCK";
				int rowsUpdated = cmd.ExecuteNonQuery();
			}
			catch {
				//MessageBox.Show(con.ConnectionString);
				MessageBox.Show(Lan.g("Pref","Error in FlushAndLock"));
			}
		}

		///<summary></summary>
		public static void Unlock() {
			con.Close();
		}	
		*/



	}
}
