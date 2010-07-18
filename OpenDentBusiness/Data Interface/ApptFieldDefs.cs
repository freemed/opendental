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
			string command="SELECT * FROM apptfielddef ORDER BY ItemOrder";
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

		/*
		///<summary>Must supply the old field name so that the patient lists can be updated.</summary>
		public static void Update(PatFieldDef patFieldDef,string oldFieldName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patFieldDef,oldFieldName);
				return;
			}
			Crud.PatFieldDefCrud.Update(patFieldDef);
			string command="UPDATE patfield SET FieldName='"+POut.String(patFieldDef.FieldName)+"' "
				+"WHERE FieldName='"+POut.String(oldFieldName)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PatFieldDef patFieldDef) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				patFieldDef.PatFieldDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),patFieldDef);
				return patFieldDef.PatFieldDefNum;
			}
			return Crud.PatFieldDefCrud.Insert(patFieldDef);
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public static void Delete(PatFieldDef patFieldDef) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patFieldDef);
				return;
			}
			string command="SELECT LName,FName FROM patient,patfield WHERE "
				+"patient.PatNum=patfield.PatNum "
				+"AND FieldName='"+POut.String(patFieldDef.FieldName)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0) {
				string s=Lans.g("PatFieldDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lans.g("PatFieldDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>5) {
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM patfielddef WHERE PatFieldDefNum ="+POut.Long(patFieldDef.PatFieldDefNum);
			Db.NonQ(command);
		}*/


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

		///<summary></summary>
		public static long Insert(ApptFieldDef apptFieldDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				apptFieldDef.ApptFieldDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),apptFieldDef);
				return apptFieldDef.ApptFieldDefNum;
			}
			return Crud.ApptFieldDefCrud.Insert(apptFieldDef);
		}

		///<summary></summary>
		public static void Update(ApptFieldDef apptFieldDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldDef);
				return;
			}
			Crud.ApptFieldDefCrud.Update(apptFieldDef);
		}

		///<summary></summary>
		public static void Delete(long apptFieldDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldDefNum);
				return;
			}
			string command= "DELETE FROM apptfielddef WHERE ApptFieldDefNum = "+POut.Long(apptFieldDefNum);
			Db.NonQ(command);
		}
		*/



	}
}