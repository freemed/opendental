using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7DefSegments{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all HL7DefSegments.</summary>
		private static List<HL7DefSegment> listt;

		///<summary>A list of all HL7DefSegments.</summary>
		public static List<HL7DefSegment> Listt{
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
			string command="SELECT * FROM hl7defsegment ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="HL7DefSegment";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.HL7DefSegmentCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<HL7DefSegment> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7DefSegment>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM hl7defsegment WHERE PatNum = "+POut.Long(patNum);
			return Crud.HL7DefSegmentCrud.SelectMany(command);
		}

		///<summary>Gets one HL7DefSegment from the db.</summary>
		public static HL7DefSegment GetOne(long hL7DefSegmentNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<HL7DefSegment>(MethodBase.GetCurrentMethod(),hL7DefSegmentNum);
			}
			return Crud.HL7DefSegmentCrud.SelectOne(hL7DefSegmentNum);
		}

		///<summary></summary>
		public static long Insert(HL7DefSegment hL7DefSegment){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				hL7DefSegment.HL7DefSegmentNum=Meth.GetLong(MethodBase.GetCurrentMethod(),hL7DefSegment);
				return hL7DefSegment.HL7DefSegmentNum;
			}
			return Crud.HL7DefSegmentCrud.Insert(hL7DefSegment);
		}

		///<summary></summary>
		public static void Update(HL7DefSegment hL7DefSegment){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefSegment);
				return;
			}
			Crud.HL7DefSegmentCrud.Update(hL7DefSegment);
		}

		///<summary></summary>
		public static void Delete(long hL7DefSegmentNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7DefSegmentNum);
				return;
			}
			string command= "DELETE FROM hl7defsegment WHERE HL7DefSegmentNum = "+POut.Long(hL7DefSegmentNum);
			Db.NonQ(command);
		}
		*/



	}
}