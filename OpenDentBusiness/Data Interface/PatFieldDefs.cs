using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class PatFieldDefs {
		#region CachePattern
		private static PatFieldDef[] list;

		///<summary>A list of all allowable patFields.</summary>
		public static PatFieldDef[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM patfielddef";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PatFieldDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new PatFieldDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatFieldDef();
				List[i].PatFieldDefNum=PIn.Long(table.Rows[i][0].ToString());
				List[i].FieldName=PIn.String(table.Rows[i][1].ToString());
			}
		}
		#endregion

		///<summary>Must supply the old field name so that the patient lists can be updated.</summary>
		public static void Update(PatFieldDef patFieldDef, string oldFieldName) {
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
			if(table.Rows.Count>0){
				string s=Lans.g("PatFieldDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lans.g("PatFieldDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM patfielddef WHERE PatFieldDefNum ="+POut.Long(patFieldDef.PatFieldDefNum);
			Db.NonQ(command);
		}
				
	}
}