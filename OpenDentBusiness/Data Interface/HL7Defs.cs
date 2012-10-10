using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using OpenDentBusiness.HL7;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7Defs{
		#region CachePattern
		///<summary>A list of all HL7Defs.</summary>
		private static List<HL7Def> listt;

		///<summary>A list of all HL7Defs.</summary>
		public static List<HL7Def> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM hl7def ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="HL7Def";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.HL7DefCrud.TableToList(table);
		}
		#endregion

		///<summary>Gets an internal HL7Def from the database of the specified type.</summary>
		public static HL7Def GetInternalFromDb(string internalType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<HL7Def>(MethodBase.GetCurrentMethod(),internalType);
			}
			string command="SELECT * FROM hl7def WHERE IsInternal=1 "
				+"AND InternalType='"+POut.String(internalType)+"'";
			return Crud.HL7DefCrud.SelectOne(command);
		}

		/// <summary>Gets from cache.  This will return null if no HL7defs are enabled.  Since only one can be enabled, this will return only the enabled one.</summary>
		public static HL7Def GetOneDeepEnabled() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<HL7Def>(MethodBase.GetCurrentMethod());
			}
			HL7Def retval=null;
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].IsEnabled) {
					retval=Listt[i];
				}
			}
			if(retval==null) {
				return null;
			}
			if(retval.IsInternal) {//if internal, messages, segments, and fields will not be in the database
				GetDeepForInternal(retval);
			}
			else {
				retval.hl7DefMessages=HL7DefMessages.GetDeepFromCache(retval.HL7DefNum);
			}
			return retval;
		}

		///<summary>Gets a full deep list of all internal defs.  If one is enabled, then it might be in database.</summary>
		public static List<HL7Def> GetDeepInternalList(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Def>>(MethodBase.GetCurrentMethod());
			}
			List<HL7Def> listInternal=new List<HL7Def>();
			HL7Def def;
			def=GetInternalFromDb("eCWFull");//might be null
			def=InternalEcwFull.GetDeepInternal(def);
			listInternal.Add(def);
			def=GetInternalFromDb("eCWStandalone");
			def=InternalEcwStandalone.GetDeepInternal(def);
			listInternal.Add(def);
			def=GetInternalFromDb("eCWTight");
			def=InternalEcwTight.GetDeepInternal(def);
			listInternal.Add(def);
			//Add defs for other companies like Centricity here later.
			return listInternal;
		}

		///<summary>Gets from C# internal code rather than db</summary>
		private static void GetDeepForInternal(HL7Def def) {
			//No need to check RemotingRole; no call to db.
			if(def.InternalType=="eCWFull") {
				def=InternalEcwFull.GetDeepInternal(def);//def that we're passing in is guaranteed to not be null
			}
			else if(def.InternalType=="eCWStandalone") {
				def=InternalEcwStandalone.GetDeepInternal(def);
			}
			else if(def.InternalType=="eCWTight") {
				def=InternalEcwTight.GetDeepInternal(def);
			}
			//no need to return a def because the original reference won't have been lost.
		}

		///<summary>Tells us whether there is an existing enable HL7Def.</summary>
		public static bool IsExistingHL7Enabled(long excludeHL7DefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),excludeHL7DefNum);
			}
			string command="SELECT COUNT(*) FROM hl7def WHERE IsEnabled=1 AND HL7DefNum != "+POut.Long(excludeHL7DefNum);
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		///<summary>Tells us whether there is an existing enabled HL7Def.</summary>
		public static bool IsExistingHL7Enabled() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM hl7def WHERE IsEnabled=1";
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		///<summary>Gets a full deep list of all defs that are not internal from the database.</summary>
		public static List<HL7Def> GetDeepCustomList(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Def>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM hl7def WHERE IsInternal=0";
			List<HL7Def> customList=Crud.HL7DefCrud.SelectMany(command);
			for(int i=0;i<customList.Count;i++) {
				customList[i].hl7DefMessages=HL7DefMessages.GetDeepFromDb(customList[i].HL7DefNum);
			}
			return customList;
		}

		///<summary>Only used from Unit Tests.  Since we clear the db of hl7Defs we have to insert this internal def not update it.</summary>
		public static void EnableInternalForTests(string internalType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),internalType);
				return;
			}
			HL7Def hl7Def=null;
			List<HL7Def> defList=GetDeepInternalList();
			for(int i=0;i<defList.Count;i++){
				if(defList[i].InternalType==internalType){
					hl7Def=defList[i];
					break;
				}
			}
			if(hl7Def==null) {
				return;
			}
			hl7Def.IsEnabled=true;
			Insert(hl7Def);
		}

		///<summary></summary>
		public static long Insert(HL7Def hL7Def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				hL7Def.HL7DefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),hL7Def);
				return hL7Def.HL7DefNum;
			}
			return Crud.HL7DefCrud.Insert(hL7Def);
		}

		///<summary></summary>
		public static void Update(HL7Def hL7Def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7Def);
				return;
			}
			Crud.HL7DefCrud.Update(hL7Def);
		}

		///<summary></summary>
		public static void Delete(long hL7DefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefNum);
				return;
			}
			string command= "DELETE FROM hl7def WHERE HL7DefNum = "+POut.Long(hL7DefNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<HL7Def> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Def>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM hl7def WHERE PatNum = "+POut.Long(patNum);
			return Crud.HL7DefCrud.SelectMany(command);
		}

		///<summary>Gets one HL7Def from the db.</summary>
		public static HL7Def GetOne(long hL7DefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<HL7Def>(MethodBase.GetCurrentMethod(),hL7DefNum);
			}
			return Crud.HL7DefCrud.SelectOne(hL7DefNum);
		}

		

		
		*/



	}
}
