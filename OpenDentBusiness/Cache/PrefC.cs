using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class PrefC {
		///<summary>Key is prefName.  Can't use the enum, because prefs are allowed to be added by outside programmers, and this framework will support those prefs, too.</summary>
		internal static Dictionary<string,Pref> Dict;

		///<summary>This property is just a shortcut to this pref to make typing faster.  This pref is used a lot.</summary>
		public static bool RandomKeys {
			get {
				return GetBool(PrefName.RandomPrimaryKeys);
			}
		}

		///<summary>This property is just a shortcut to this pref to make typing faster.  This pref is used a lot.</summary>
		public static bool UsingAtoZfolder {
			get {
				return !GetBool(PrefName.AtoZfolderNotRequired);
			}
		}

		///<summary>Gets a pref of type long.</summary>
		public static long GetLong(PrefName prefName) {
			if(Dict==null){
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PLong(Dict[prefName.ToString()].ValueString);
		}

		///<summary>Gets a pref of type int32.  Used when the pref is an enumeration, itemorder, etc.  Also used for historical queries in ConvertDatabase.</summary>
		public static int GetInt(PrefName prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PInt(Dict[prefName.ToString()].ValueString);
		}

		///<summary>Gets a pref of type double.</summary>
		public static double GetDouble(PrefName prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDouble(Dict[prefName.ToString()].ValueString);
		}

		///<summary>Gets a pref of type bool.</summary>
		public static bool GetBool(PrefName prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PBool(Dict[prefName.ToString()].ValueString);
		}

		///<Summary>Gets a pref of type bool, but will not throw an exception if null or not found.  Indicate whether the silent default is true or false.</Summary>
		public static bool GetBoolSilent(PrefName prefName,bool silentDefault) {
			if(Dict==null) {
				return silentDefault;
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				return silentDefault;
			}
			return PIn.PBool(Dict[prefName.ToString()].ValueString);
		}

		///<summary>Gets a pref of type string.</summary>
		public static string GetString(PrefName prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return Dict[prefName.ToString()].ValueString;
		}

		///<summary>Gets a pref of type string.  Will not throw an exception if null or not found.</summary>
		public static string GetStringSilent(PrefName prefName) {
			if(Dict==null) {
				return "";
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				return "";
			}
			return Dict[prefName.ToString()].ValueString;
		}

		///<summary>Gets a pref of type date.</summary>
		public static DateTime GetDate(PrefName prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDate(Dict[prefName.ToString()].ValueString);
		}

		///<summary>Gets a pref of type datetime.</summary>
		public static DateTime GetDateT(PrefName prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName.ToString())) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return PIn.PDateT(Dict[prefName.ToString()].ValueString);
		}

		///<summary>Used sometimes for prefs that are not part of the enum, especially for outside developers.</summary>
		public static string GetRaw(string prefName) {
			if(Dict==null) {
				Prefs.RefreshCache();
			}
			if(!Dict.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return Dict[prefName].ValueString;
		}

		///<summary>Used by an outside developer.</summary>
		public static bool ContainsKey(string prefName) {
			return Dict.ContainsKey(prefName);
		}

		///<summary>Used by an outside developer.</summary>
		public static bool HListIsNull() {
			return Dict==null;
		}

		///<summary>Only used in the unit tests.  This quick hack has not been tested.</summary>
		public static Dictionary<string,Pref> DictRef{
			get {
				return Dict;
			}
			set {
				Dict=value;
			}
		}

	}
}
