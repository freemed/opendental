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
			string command="SELECT * FROM terminalactive WHERE ComputerName ='"+POut.String(computerName)+"'";
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
				List[i].TerminalActiveNum= PIn.Long(table.Rows[i][0].ToString());
				List[i].ComputerName     = PIn.String(table.Rows[i][1].ToString());
				List[i].TerminalStatus   = (TerminalStatusEnum)PIn.Long(table.Rows[i][2].ToString());
				List[i].PatNum           = PIn.Long(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(TerminalActive te) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),te);
				return;
			}
			Crud.TerminalActiveCrud.Update(te);
		}

		///<summary></summary>
		public static long Insert(TerminalActive te) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				te.TerminalActiveNum=Meth.GetLong(MethodBase.GetCurrentMethod(),te);
				return te.TerminalActiveNum;
			}
			return Crud.TerminalActiveCrud.Insert(te);
		}

		///<summary></summary>
		public static void Delete(TerminalActive te) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),te);
				return;
			}
			string command="DELETE FROM terminalactive WHERE TerminalActiveNum ="+POut.Long(te.TerminalActiveNum);
			Db.NonQ(command);
		}
	
		///<summary>Run when starting up a terminal window to delete any previous entries for this computer that didn't get deleted properly.</summary>
		public static void DeleteAllForComputer(string computerName){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),computerName);
				return;
			}
			string command="DELETE FROM terminalactive WHERE ComputerName ='"+POut.String(computerName)+"'";
			Db.NonQ(command);
		}

		///<summary>Called whenever user wants to edit patient info.  Not allowed to if patient edit window is open at a terminal.  Once patient is done at terminal, then staff allowed back into patient edit window.</summary>
		public static bool PatIsInUse(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM terminalactive WHERE PatNum="+POut.Long(patNum)
				+" AND (TerminalStatus="+POut.Long((int)TerminalStatusEnum.PatientInfo)
				+" OR TerminalStatus="+POut.Long((int)TerminalStatusEnum.UpdateOnly)+")";
			if(Db.GetCount(command)=="0"){
				return false;
			}
			return true;
		}
	
		
		
	}

		



		
	

	

	


}










