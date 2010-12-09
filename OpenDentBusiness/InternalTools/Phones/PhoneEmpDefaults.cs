using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Not a true Cache pattern.  It only loads the cache once on startup and then never again.  No entry in the Cache file.  No InvalidType for PhoneEmpDefault.</summary>
	public class PhoneEmpDefaults{
		#region CachePattern

		///<summary>A list of all PhoneEmpDefaults.</summary>
		private static List<PhoneEmpDefault> listt;

		///<summary>A list of all PhoneEmpDefaults.</summary>
		public static List<PhoneEmpDefault> Listt{
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

		///<summary>Not part of the true Cache pattern.  See notes above.</summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM phoneempdefault";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PhoneEmpDefault";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.PhoneEmpDefaultCrud.TableToList(table);
		}
		#endregion

		public static bool IsNoGraph(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmployeeNum==employeeNum) {
					return Listt[i].NoGraph;
				}
			}
			return false;
		}

		public static bool IsNoColor(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmployeeNum==employeeNum) {
					return Listt[i].NoColor;
				}
			}
			return false;
		}

		public static AsteriskRingGroups GetRingGroup(long employeeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmployeeNum==employeeNum) {
					return Listt[i].RingGroups;
				}
			}
			return AsteriskRingGroups.All;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<PhoneEmpDefault> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PhoneEmpDefault>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM phoneempdefault WHERE PatNum = "+POut.Long(patNum);
			return Crud.PhoneEmpDefaultCrud.SelectMany(command);
		}

		///<summary>Gets one PhoneEmpDefault from the db.</summary>
		public static PhoneEmpDefault GetOne(long employeeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PhoneEmpDefault>(MethodBase.GetCurrentMethod(),employeeNum);
			}
			return Crud.PhoneEmpDefaultCrud.SelectOne(employeeNum);
		}

		///<summary></summary>
		public static long Insert(PhoneEmpDefault phoneEmpDefault){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				phoneEmpDefault.EmployeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),phoneEmpDefault);
				return phoneEmpDefault.EmployeeNum;
			}
			return Crud.PhoneEmpDefaultCrud.Insert(phoneEmpDefault);
		}

		///<summary></summary>
		public static void Update(PhoneEmpDefault phoneEmpDefault){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),phoneEmpDefault);
				return;
			}
			Crud.PhoneEmpDefaultCrud.Update(phoneEmpDefault);
		}

		///<summary></summary>
		public static void Delete(long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),employeeNum);
				return;
			}
			string command= "DELETE FROM phoneempdefault WHERE EmployeeNum = "+POut.Long(employeeNum);
			Db.NonQ(command);
		}
		*/



	}
}