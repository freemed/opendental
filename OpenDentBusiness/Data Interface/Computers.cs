using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Computers {
		///<summary>A list of all computers that have logged into the database in the past.  Might be some extra computer names in the list unless user has cleaned it up.</summary>
		private static Computer[] list;

		public static Computer[] List{
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

		public static void EnsureComputerInDB(string computerName){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),computerName);
				return;
			}
			string command=
				"SELECT * from computer "
				+"WHERE compname = '"+computerName+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				Computer Cur=new Computer();
				Cur.CompName=computerName;
				Computers.Insert(Cur);
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			EnsureComputerInDB(Environment.MachineName);
			string command="SELECT * FROM computer ORDER BY CompName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Computer";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new Computer[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Computer();
				List[i].ComputerNum=PIn.Long(table.Rows[i][0].ToString());
				List[i].CompName=PIn.String(table.Rows[i][1].ToString());
			}
		}

		///<summary>ONLY use this if compname is not already present</summary>
		public static long Insert(Computer comp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				comp.ComputerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),comp);
				return comp.ComputerNum;
			}
			return Crud.ComputerCrud.Insert(comp);
		}

		/*
		///<summary></summary>
		public static void Update(){
			string command= "UPDATE computer SET "
				+"compname = '"    +POut.PString(CompName)+"' "
				//+"printername = '" +POut.PString(PrinterName)+"' "
				+"WHERE ComputerNum = '"+POut.PInt(ComputerNum)+"'";
			//MessageBox.Show(string command);
			DataConnection dcon=new DataConnection();
 			Db.NonQ(command);
		}*/

		///<summary></summary>
		public static void Delete(Computer comp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),comp);
				return;
			}
			string command= "DELETE FROM computer WHERE computernum = '"+comp.ComputerNum.ToString()+"'";
 			Db.NonQ(command);
		}

		///<summary>Only called from Printers.GetForSit</summary>
		public static Computer GetCur(){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(Environment.MachineName.ToUpper()==List[i].CompName.ToUpper()) {
					return List[i];
				}
			}
			return null;//this will never happen
		}

		public static List<string> GetRunningComputers() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			//heartbeat is every three minutes.  We'll allow four to be generous.
			string command="SELECT CompName FROM computer WHERE LastHeartBeat > SUBTIME(NOW(),'00:04:00')";
			DataTable table=Db.GetTable(command);
			List<string> retVal=new List<string>();
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		public static void UpdateHeartBeat(string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),computerName);
				return;
			}
			string command= "UPDATE computer SET LastHeartBeat=NOW() WHERE CompName = '"+POut.String(computerName)+"'";
			Db.NonQ(command);
		}

		public static void ClearHeartBeat(string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),computerName);
				return;
			}
			string command= "UPDATE computer SET LastHeartBeat='0001-01-01' WHERE CompName = '"+POut.String(computerName)+"'";
			Db.NonQ(command);
		}

		public static void ClearAllHeartBeats(string machineNameException) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command= "UPDATE computer SET LastHeartBeat='0001-01-01' "
				+"WHERE CompName != '"+POut.String(machineNameException)+"'";
			Db.NonQ(command);
		}

	}
}