using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

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
		public static HL7Def GetInternalFromDb(string Hl7InternalType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<HL7Def>(MethodBase.GetCurrentMethod(),Hl7InternalType);
			}
			string command="SELECT * FROM hl7def WHERE IsInternal=1 "
				+"AND InternalType='"+POut.String(Hl7InternalType)+"'";
			return Crud.HL7DefCrud.SelectOne(command);
		}

		///<summary>Gets list of all defs that are not internal from the database.</summary>
		public static List<HL7Def> GetCustomList() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Def>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM hl7def WHERE IsInternal=0";
			return Crud.HL7DefCrud.SelectMany(command);
		}

		///<summary>Gets list of enabled defnums from the database.</summary>
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

		///<summary>Gets a full deep list of all defs that are not internal from the database.</summary>
		public static List<HL7Def> GetDeepCustomList(){
			List<HL7Def> hl7defs=new List<HL7Def>();
			hl7defs=GetCustomList();
			foreach(HL7Def d in hl7defs) {
				d.hl7DefMessages=HL7DefMessages.GetDeepForDef(d.HL7DefNum);
			}
			return hl7defs;
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
