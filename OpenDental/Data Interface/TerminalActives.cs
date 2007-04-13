using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class TerminalActives {

		///<summary>Gets a list of all TerminalActives.  Used by the terminal monitor window and by the terminal window itself.  Data is retrieved at regular short intervals, so functions as a messaging system.</summary>
		public static TerminalActive[] Refresh() {
			string command="SELECT * FROM terminalactive ORDER BY ComputerName";
			return RefreshAndFill(command);
		}

		///<summary></summary>
		public static TerminalActive GetTerminal(string computerName) {
			string command="SELECT * FROM terminalactive WHERE ComputerName ='"+POut.PString(computerName)+"'";
			TerminalActive[] List=RefreshAndFill(command);
			if(List.Length>0) {
				return List[0];
			}
			return null;
		}

		///<summary>Gets a list of all TerminalActives.  Used by the terminal monitor window and by the terminal window itself.  Data is retrieved at regular short intervals, so functions as a messaging system.</summary>
		private static TerminalActive[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			TerminalActive[] List=new TerminalActive[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TerminalActive();
				List[i].TerminalActiveNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].ComputerName     = PIn.PString(table.Rows[i][1].ToString());
				List[i].TerminalStatus   = (TerminalStatusEnum)PIn.PInt(table.Rows[i][2].ToString());
				List[i].PatNum           = PIn.PInt(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(TerminalActive te) {
			string command="UPDATE terminalactive SET " 
				+"ComputerName = '"   +POut.PString(te.ComputerName)+"'"
				+",TerminalStatus = '"+POut.PInt   ((int)te.TerminalStatus)+"'"
				+",PatNum = '"        +POut.PInt   (te.PatNum)+"'"
				+" WHERE TerminalActiveNum  ='"+POut.PInt   (te.TerminalActiveNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(TerminalActive te) {
			if(PrefB.RandomKeys) {
				te.TerminalActiveNum=MiscData.GetKey("terminalactive","TerminalActiveNum");
			}
			string command="INSERT INTO terminalactive (";
			if(PrefB.RandomKeys) {
				command+="TerminalActiveNum,";
			}
			command+="ComputerName,TerminalStatus,PatNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(te.TerminalActiveNum)+"', ";
			}
			command+=
				 "'"+POut.PString(te.ComputerName)+"', "
				+"'"+POut.PInt   ((int)te.TerminalStatus)+"', "
				+"'"+POut.PInt   (te.PatNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				te.TerminalActiveNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(TerminalActive te) {
			string command="DELETE FROM terminalactive WHERE TerminalActiveNum ="+POut.PInt(te.TerminalActiveNum);
			General.NonQ(command);
		}

	

	
		///<summary>Run when starting up a terminal window to delete any previous entries for this computer that didn't get deleted properly.</summary>
		public static void DeleteAllForComputer(string computerName){
			string command="DELETE FROM terminalactive WHERE ComputerName ='"+POut.PString(computerName)+"'";
			General.NonQ(command);
		}

		///<summary>Called whenever user wants to edit patient info.  Not allowed to if patient edit window is open at a terminal.  Once patient is done at terminal, then staff allowed back into patient edit window.</summary>
		public static bool PatIsInUse(int patNum){
			string command="SELECT COUNT(*) FROM terminalactive WHERE PatNum="+POut.PInt(patNum)
				+" AND (TerminalStatus="+POut.PInt((int)TerminalStatusEnum.PatientInfo)
				+" OR TerminalStatus="+POut.PInt((int)TerminalStatusEnum.UpdateOnly)+")";
			if(General.GetCount(command)=="0"){
				return false;
			}
			return true;
		}
	
		
		
	}

		



		
	

	

	


}










