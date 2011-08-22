using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Not a true Cache pattern.  It only loads the cache once on startup and then never again.  No entry in the Cache file.  No InvalidType for PhoneEmpDefault.  Data is almost always pulled from db in realtime, and this cache is only used for default ringgroups.</summary>
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
			string command="SELECT * FROM phoneempdefault";
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
		
		///<summary></summary>
		public static List<PhoneEmpDefault> Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PhoneEmpDefault>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phoneempdefault ORDER BY PhoneExt";//because that's the order we are used to in the phone panel.
			return Crud.PhoneEmpDefaultCrud.SelectMany(command);
		}
		
		public static bool IsNoGraph(long employeeNum,List<PhoneEmpDefault> listPED) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<listPED.Count;i++) {
				if(listPED[i].EmployeeNum==employeeNum) {
					return listPED[i].NoGraph;
				}
			}
			return false;
		}

		///<summary>Can return null.</summary>
		public static PhoneEmpDefault GetByExtAndEmp(int extension,long employeeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PhoneEmpDefault>(MethodBase.GetCurrentMethod(),extension,employeeNum);
			}
			string command="SELECT * FROM phoneempdefault WHERE PhoneExt="+POut.Int(extension)+" "
				+"AND EmployeeNum="+POut.Long(employeeNum);
			return Crud.PhoneEmpDefaultCrud.SelectOne(command);
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

		///<summary>Was in phoneoverrides.</summary>
		public static void SetAvailable(int extension,long empNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),extension,empNum);
				return;
			}
			string command="UPDATE phoneempdefault "
				+"SET IsUnavailable = 0 "
				+"WHERE PhoneExt="+POut.Int(extension)+" "
				+"AND EmployeeNum="+POut.Long(empNum);
			Db.NonQ(command);
		}
	
		///<summary></summary>
		public static long Insert(PhoneEmpDefault phoneEmpDefault){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				phoneEmpDefault.EmployeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),phoneEmpDefault);
				return phoneEmpDefault.EmployeeNum;
			}
			return Crud.PhoneEmpDefaultCrud.Insert(phoneEmpDefault,true);//user specifies the PK
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

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		

		///<summary>Gets one PhoneEmpDefault from the db.</summary>
		public static PhoneEmpDefault GetOne(long employeeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PhoneEmpDefault>(MethodBase.GetCurrentMethod(),employeeNum);
			}
			return Crud.PhoneEmpDefaultCrud.SelectOne(employeeNum);
		}

		
		*/



	}
}