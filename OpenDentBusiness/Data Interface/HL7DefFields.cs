using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7DefFields{
		#region CachePattern
		///<summary>A list of all HL7DefFields.</summary>
		private static List<HL7DefField> listt;

		///<summary>A list of all HL7DefFields.</summary>
		public static List<HL7DefField> Listt{
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
			string command="SELECT * FROM hl7deffield ORDER BY OrdinalPos";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="HL7DefField";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.HL7DefFieldCrud.TableToList(table);
		}
		#endregion

		public static List<HL7DefField> GetForDefSegment(long hl7DefSegmentNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7DefField>>(MethodBase.GetCurrentMethod(),hl7DefSegmentNum);
			}
			string command="SELECT * FROM hl7deffield WHERE HL7DefSegmentNum='"+POut.Long(hl7DefSegmentNum)+"'";
			return Crud.HL7DefFieldCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(HL7DefField hL7DefField) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				hL7DefField.HL7DefFieldNum=Meth.GetLong(MethodBase.GetCurrentMethod(),hL7DefField);
				return hL7DefField.HL7DefFieldNum;
			}
			return Crud.HL7DefFieldCrud.Insert(hL7DefField);
		}

		///<summary></summary>
		public static void Update(HL7DefField hL7DefField) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefField);
				return;
			}
			Crud.HL7DefFieldCrud.Update(hL7DefField);
		}

		///<summary></summary>
		public static void Delete(long hL7DefFieldNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefFieldNum);
				return;
			}
			string command= "DELETE FROM hl7deffield WHERE HL7DefFieldNum = "+POut.Long(hL7DefFieldNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<HL7DefField> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7DefField>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM hl7deffield WHERE PatNum = "+POut.Long(patNum);
			return Crud.HL7DefFieldCrud.SelectMany(command);
		}

		///<summary>Gets one HL7DefField from the db.</summary>
		public static HL7DefField GetOne(long hL7DefFieldNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<HL7DefField>(MethodBase.GetCurrentMethod(),hL7DefFieldNum);
			}
			return Crud.HL7DefFieldCrud.SelectOne(hL7DefFieldNum);
		}

		*/



	}
}