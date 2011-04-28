using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Prefs{
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM preference";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Pref";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			PrefC.Dict=new Dictionary<string,Pref>();
			Pref pref;
			//PrefName enumpn;
			//Can't use Crud.PrefCrud.TableToList(table) because it will fail the first time someone runs 7.6 before conversion.
			for(int i=0;i<table.Rows.Count;i++) {
				pref=new Pref();
				if(table.Columns.Contains("PrefNum")) {
					pref.PrefNum=PIn.Long(table.Rows[i]["PrefNum"].ToString());
				}
				pref.PrefName=PIn.String(table.Rows[i]["PrefName"].ToString());
				pref.ValueString=PIn.String(table.Rows[i]["ValueString"].ToString());
				//no need to load up the comments.  Especially since this will fail when user first runs version 5.8.
				PrefC.Dict.Add(pref.PrefName,pref);
			}
		}

		///<summary></summary>
		public static void ClearCache() {
			PrefC.Dict=null;
		}

		///<summary></summary>
		public static void Update(Pref pref) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pref);
				return;
			}
			//Don't use CRUD here because we want to update based on PrefName instead of PrefNum.  Otherwise, it might fail the first time someone runs 7.6.
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.String(pref.ValueString)+"' "
				+" WHERE PrefName = '"+POut.String(pref.PrefName)+"'";
			Db.NonQ(command);
		}

		///<summary>Updates a pref of type int.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateInt(PrefName prefName,int newValue) {
			return UpdateLong(prefName,newValue);
		}

		///<summary>Updates a pref of type long.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateLong(PrefName prefName,long newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.Dict.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetLong(prefName)==newValue) {
				return false;//no change needed
			}
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.Long(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else{
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=newValue.ToString();
			PrefC.Dict[prefName.ToString()]=pref;//in some cases, we just want to change the pref in local memory instead of doing a refresh afterwards.
			return retVal;
		}

		///<summary>Updates a pref of type double.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDouble(PrefName prefName,double newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.Dict.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetDouble(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.Double(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else{
				Db.NonQ(command);
			}
			return retVal;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(PrefName prefName,bool newValue) {
			//No need to check RemotingRole; no call to db.
			return UpdateBool(prefName,newValue,false);
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(PrefName prefName,bool newValue,bool isForced) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.Dict.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(!isForced && PrefC.GetBool(prefName)==newValue) {
				return false;//no change needed
			}
			string command="UPDATE preference SET "
				+"ValueString = '"+POut.Bool(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue,isForced);
			}
			else{			
				Db.NonQ(command);
			}
			return retVal;
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

		///<summary>Used for prefs that are non-standard.  Especially by outside programmers. Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateRaw(string prefName,string newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.Dict.ContainsKey(prefName)) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetRaw(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.String(newValue)+"' "
				+"WHERE PrefName = '"+POut.String(prefName)+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else {
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName;
			pref.ValueString=newValue;
			PrefC.Dict[prefName]=pref;
			return retVal;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDateT(PrefName prefName,DateTime newValue) {
			//Very unusual.  Involves cache, so Meth is used further down instead of here at the top.
			if(!PrefC.Dict.ContainsKey(prefName.ToString())) {
				throw new ApplicationException(prefName+" is an invalid pref name.");
			}
			if(PrefC.GetDateT(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.DateT(newValue,false)+"' "
				+"WHERE PrefName = '"+POut.String(prefName.ToString())+"'";
			bool retVal=true;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				retVal=Meth.GetBool(MethodBase.GetCurrentMethod(),prefName,newValue);
			}
			else{
				Db.NonQ(command);
			}
			Pref pref=new Pref();
			pref.PrefName=prefName.ToString();
			pref.ValueString=POut.DateT(newValue,false);
			PrefC.Dict[prefName.ToString()]=pref;//in some cases, we just want to change the pref in local memory instead of doing a refresh afterwards.
			return retVal;
		}

		///<summary>Only run from PrefL.CheckMySqlVersion41().</summary>
		public static void ConvertToMySqlVersion41() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SHOW TABLES";
			DataTable table=Db.GetTable(command);
			string[] tableNames=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++) {
				if(tableNames[i]!="procedurecode") {
					command="ALTER TABLE "+tableNames[i]+" CONVERT TO CHARACTER SET utf8";
					Db.NonQ(command);
				}
			}
			string[] commands=new string[]
				{
					//"ALTER TABLE procedurecode CHANGE OldCode OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE procedurecode DEFAULT character set utf8"
					"ALTER TABLE procedurecode MODIFY Descript varchar(255) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AbbrDesc varchar(50) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY ProcTime varchar(24) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY DefaultNote text character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AlternateCode1 varchar(15) character set utf8 NOT NULL"
					//,"ALTER TABLE procedurelog MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE autocodeitem MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE procbuttonitem MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE covspan MODIFY FromCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE covspan MODIFY ToCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE fee MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
				};
			Db.NonQ(commands);
			//and set the default too
			command="ALTER DATABASE CHARACTER SET utf8";
			Db.NonQ(command);
			command="INSERT INTO preference VALUES('DatabaseConvertedForMySql41','1')";
			Db.NonQ(command);
		}
		

	}

	


	


}










