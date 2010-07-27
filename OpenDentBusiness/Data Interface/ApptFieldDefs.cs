using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ApptFieldDefs{
		#region CachePattern
		///<summary>A list of all ApptFieldDefs.</summary>
		private static List<ApptFieldDef> listt;

		///<summary>A list of all ApptFieldDefs.</summary>
		public static List<ApptFieldDef> Listt{
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
			string command="SELECT * FROM apptfielddef ORDER BY FieldName";//important to order so that listt matches order returned in other queries.
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ApptFieldDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ApptFieldDefCrud.TableToList(table);
		}
		#endregion

		///<summary>Must supply the old field name so that the apptFields attached to appointments can be updated.  Will throw exception if new FieldName is already in use.</summary>
		public static void Update(ApptFieldDef apptFieldDef,string oldFieldName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldDef,oldFieldName);
				return;
			}
			string command="SELECT COUNT(*) FROM apptfielddef WHERE FieldName='"+POut.String(apptFieldDef.FieldName)+"' "
				+"AND ApptFieldDefNum != "+POut.Long(apptFieldDef.ApptFieldDefNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("FormApptFieldDefEdit","Field name already in use."));
			}
			Crud.ApptFieldDefCrud.Update(apptFieldDef);
			command="UPDATE apptfield SET FieldName='"+POut.String(apptFieldDef.FieldName)+"' "
				+"WHERE FieldName='"+POut.String(oldFieldName)+"'";
			Db.NonQ(command);
		}

		///<summary>Surround with try/catch in case field name already in use.</summary>
		public static long Insert(ApptFieldDef apptFieldDef) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				apptFieldDef.ApptFieldDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),apptFieldDef);
				return apptFieldDef.ApptFieldDefNum;
			}
			string command="SELECT COUNT(*) FROM apptfielddef WHERE FieldName='"+POut.String(apptFieldDef.FieldName)+"'";
			if(Db.GetCount(command)!="0") {
				throw new ApplicationException(Lans.g("FormApptFieldDefEdit","Field name already in use."));
			}
			return Crud.ApptFieldDefCrud.Insert(apptFieldDef);
		}

		///<summary>Surround with try/catch, because it will throw an exception if any appointment is using this def.</summary>
		public static void Delete(ApptFieldDef apptFieldDef) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldDef);
				return;
			}
			string command="SELECT LName,FName,AptDateTime "
				+"FROM patient,apptfield,appointment WHERE "
				+"patient.PatNum=appointment.PatNum "
				+"AND appointment.AptNum=apptfield.AptNum "
				+"AND FieldName='"+POut.String(apptFieldDef.FieldName)+"'";
			DataTable table=Db.GetTable(command);
			DateTime aptDateTime;
			if(table.Rows.Count>0) {
				string s=Lans.g("FormApptFieldDefEdit","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lans.g("FormApptFieldDefEdit","appointments, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>5) {
						break;
					}
					aptDateTime=PIn.DateT(table.Rows[i]["AptDateTime"].ToString());
					s+=table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString()+POut.DateT(aptDateTime,false)+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM apptfielddef WHERE ApptFieldDefNum ="+POut.Long(apptFieldDef.ApptFieldDefNum);
			Db.NonQ(command);
		}
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ApptFieldDef> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ApptFieldDef>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM apptfielddef WHERE PatNum = "+POut.Long(patNum);
			return Crud.ApptFieldDefCrud.SelectMany(command);
		}

		///<summary>Gets one ApptFieldDef from the db.</summary>
		public static ApptFieldDef GetOne(long apptFieldDefNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ApptFieldDef>(MethodBase.GetCurrentMethod(),apptFieldDefNum);
			}
			return Crud.ApptFieldDefCrud.SelectOne(apptFieldDefNum);
		}
		*/

		public static string GetFieldName(long apptFieldDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].ApptFieldDefNum==apptFieldDefNum) {
					return Listt[i].FieldName;
				}
			}
			return "";
		}


	}
}