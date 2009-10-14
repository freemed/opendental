using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class TerminalActives {

		///<summary>Gets a list of all TerminalActives.  Used by the terminal monitor window and by the terminal window itself.  Data is retrieved at regular short intervals, so functions as a messaging system.</summary>
		public static TerminalActive[] Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TerminalActive[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM terminalactive ORDER BY ComputerName";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary></summary>
		public static TerminalActive GetTerminal(string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TerminalActive>(MethodBase.GetCurrentMethod(),computerName);
			}
			string command="SELECT * FROM terminalactive WHERE ComputerName ='"+POut.PString(computerName)+"'";
			TerminalActive[] List=RefreshAndFill(Db.GetTable(command));
			if(List.Length>0) {
				return List[0];
			}
			return null;
		}

		///<summary>Gets a list of all TerminalActives.  Used by the terminal monitor window and by the terminal window itself.  Data is retrieved at regular short intervals, so functions as a messaging system.</summary>
		private static TerminalActive[] RefreshAndFill(DataTable table) {
			//No need to check RemotingRole; no call to db.
			TerminalActive[] List=new TerminalActive[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TerminalActive();
				List[i].TerminalActiveNum= PIn.PLong(table.Rows[i][0].ToString());
				List[i].ComputerName     = PIn.PString(table.Rows[i][1].ToString());
				List[i].TerminalStatus   = (TerminalStatusEnum)PIn.PLong(table.Rows[i][2].ToString());
				List[i].PatNum           = PIn.PLong(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(TerminalActive te) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),te);
				return;
			}
			string command="UPDATE terminalactive SET " 
				+"ComputerName = '"   +POut.PString(te.ComputerName)+"'"
				+",TerminalStatus = '"+POut.PLong   ((int)te.TerminalStatus)+"'"
				+",PatNum = '"        +POut.PLong   (te.PatNum)+"'"
				+" WHERE TerminalActiveNum  ='"+POut.PLong   (te.TerminalActiveNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(TerminalActive te) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				te.TerminalActiveNum=Meth.GetLong(MethodBase.GetCurrentMethod(),te);
				return te.TerminalActiveNum;
			}
			if(PrefC.RandomKeys) {
				te.TerminalActiveNum=ReplicationServers.GetKey("terminalactive","TerminalActiveNum");
			}
			string command="INSERT INTO terminalactive (";
			if(PrefC.RandomKeys) {
				command+="TerminalActiveNum,";
			}
			command+="ComputerName,TerminalStatus,PatNum) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(te.TerminalActiveNum)+"', ";
			}
			command+=
				 "'"+POut.PString(te.ComputerName)+"', "
				+"'"+POut.PLong   ((int)te.TerminalStatus)+"', "
				+"'"+POut.PLong   (te.PatNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				te.TerminalActiveNum=Db.NonQ(command,true);
			}
			return te.TerminalActiveNum;
		}

		///<summary></summary>
		public static void Delete(TerminalActive te) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),te);
				return;
			}
			string command="DELETE FROM terminalactive WHERE TerminalActiveNum ="+POut.PLong(te.TerminalActiveNum);
			Db.NonQ(command);
		}
	
		///<summary>Run when starting up a terminal window to delete any previous entries for this computer that didn't get deleted properly.</summary>
		public static void DeleteAllForComputer(string computerName){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),computerName);
				return;
			}
			string command="DELETE FROM terminalactive WHERE ComputerName ='"+POut.PString(computerName)+"'";
			Db.NonQ(command);
		}

		///<summary>Called whenever user wants to edit patient info.  Not allowed to if patient edit window is open at a terminal.  Once patient is done at terminal, then staff allowed back into patient edit window.</summary>
		public static bool PatIsInUse(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM terminalactive WHERE PatNum="+POut.PLong(patNum)
				+" AND (TerminalStatus="+POut.PLong((int)TerminalStatusEnum.PatientInfo)
				+" OR TerminalStatus="+POut.PLong((int)TerminalStatusEnum.UpdateOnly)+")";
			if(Db.GetCount(command)=="0"){
				return false;
			}
			return true;
		}
	
		
		
	}

		



		
	

	

	


}










