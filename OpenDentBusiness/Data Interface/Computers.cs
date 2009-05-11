using System;
using System.Collections;
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
				List[i].ComputerNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].CompName=PIn.PString(table.Rows[i][1].ToString());
			}
		}

		///<summary>ONLY use this if compname is not already present</summary>
		public static void Insert(Computer comp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),comp);
				return;
			}
			if(PrefC.RandomKeys){
				comp.ComputerNum=MiscData.GetKey("computer","ComputerNum");
			}
			string command= "INSERT INTO computer (";
			if(PrefC.RandomKeys){
				command+="ComputerNum,";
			}
			command+="CompName"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(comp.ComputerNum)+"', ";
			}
			command+=
				"'"+POut.PString(comp.CompName)+"')";
				//+"'"+POut.PString(PrinterName)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				comp.ComputerNum=Db.NonQ(command,true);
			}
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

	}
}