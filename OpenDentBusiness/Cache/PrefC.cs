using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class PrefC {
		///<summary></summary>
		public static Hashtable HList;

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

		///<summary>Gets a pref of type int.</summary>
		public static int GetInt(string prefName) {
			if(HList==null){
				Prefs.RefreshCache();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PInt(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type double.</summary>
		public static double GetDouble(string prefName) {
			if(HList==null){
				Prefs.RefreshCache();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDouble(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type bool.</summary>
		public static bool GetBool(string prefName) {
			if(HList==null){
				Prefs.RefreshCache();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PBool(((Pref)HList[prefName]).ValueString);
		}

		///<Summary>Gets a pref of type bool, but will not throw an exception if null or not found.  Indicate whether the silent default is true or false.</Summary>
		public static bool GetBoolSilent(string prefName,bool silentDefault) {
			if(HList==null) {
				return silentDefault;
			}
			if(!HList.ContainsKey(prefName)) {
				return silentDefault;
			}
			return PIn.PBool(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type string.</summary>
		public static string GetString(string prefName) {
			if(HList==null){
				Prefs.RefreshCache();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return ((Pref)HList[prefName]).ValueString;
		}

		///<summary>Gets a pref of type string.  Will not throw an exception if null or not found.</summary>
		public static string GetStringSilent(string prefName) {
			if(HList==null) {
				return "";
			}
			if(!HList.ContainsKey(prefName)) {
				return "";
			}
			return ((Pref)HList[prefName]).ValueString;
		}

		///<summary>Gets a pref of type date.</summary>
		public static DateTime GetDate(string prefName) {
			if(HList==null){
				Prefs.RefreshCache();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDate(((Pref)HList[prefName]).ValueString);
		}

		///<summary>Gets a pref of type datetime.</summary>
		public static DateTime GetDateT(string prefName) {
			if(HList==null){
				Prefs.RefreshCache();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDateT(((Pref)HList[prefName]).ValueString);
		}

	}
}
