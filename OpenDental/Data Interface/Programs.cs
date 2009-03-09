using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDental.Bridges;
using OpenDentBusiness;

namespace OpenDental{
	

	///<summary></summary>
	public class Programs{
		///<summary></summary>
		public static Hashtable HList;
		///<summary></summary>
		public static List<Program> Listt;

		///<summary></summary>
		public static void Refresh(){
			//MessageBox.Show("refreshing");
			HList=new Hashtable();
			Program prog = new Program();
			string command = 
				"SELECT * from program ORDER BY ProgDesc";
			DataTable table=General.GetTable(command);
			Listt=new List<Program>();
			for (int i=0;i<table.Rows.Count;i++){
				prog=new Program();
				prog.ProgramNum =PIn.PInt(table.Rows[i][0].ToString());
				prog.ProgName   =PIn.PString(table.Rows[i][1].ToString());
				prog.ProgDesc   =PIn.PString(table.Rows[i][2].ToString());
				prog.Enabled    =PIn.PBool(table.Rows[i][3].ToString());
				prog.Path       =PIn.PString(table.Rows[i][4].ToString());
				prog.CommandLine=PIn.PString(table.Rows[i][5].ToString());
				prog.Note       =PIn.PString(table.Rows[i][6].ToString());
				Listt.Add(prog);
				if(!HList.ContainsKey(prog.ProgName)) {
					HList.Add(prog.ProgName,prog);
				}
			}
			//MessageBox.Show(HList.Count.ToString());
		}

		///<summary></summary>
		public static void Update(Program Cur){
			string command= "UPDATE program SET "
				+"ProgName = '"     +POut.PString(Cur.ProgName)+"'"
				+",ProgDesc  = '"   +POut.PString(Cur.ProgDesc)+"'"
				+",Enabled  = '"    +POut.PBool  (Cur.Enabled)+"'"
				+",Path = '"        +POut.PString(Cur.Path)+"'"
				+",CommandLine  = '"+POut.PString(Cur.CommandLine)+"'"
				+",Note  = '"       +POut.PString(Cur.Note)+"'"
				+" WHERE programnum = '"+POut.PInt(Cur.ProgramNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Program Cur){
			string command= "INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				+") VALUES("
				+"'"+POut.PString(Cur.ProgName)+"', "
				+"'"+POut.PString(Cur.ProgDesc)+"', "
				+"'"+POut.PBool  (Cur.Enabled)+"', "
				+"'"+POut.PString(Cur.Path)+"', "
				+"'"+POut.PString(Cur.CommandLine)+"', "
				+"'"+POut.PString(Cur.Note)+"')";
			//MessageBox.Show(cmd.CommandText);
			Cur.ProgramNum=General.NonQ(command, true);
		}

		///<summary>This can only be called by the user if it is a program link that they created. Included program links cannot be deleted.  If calling this from ClassConversion, must delete any dependent ProgramProperties first.  It will delete ToolButItems for you.</summary>
		public static void Delete(Program Cur){
			ToolButItems.DeleteAllForProgram(Cur.ProgramNum);
			string command = "DELETE from program WHERE programnum = '"+Cur.ProgramNum.ToString()+"'";
			General.NonQ(command);
			
		}

		///<summary>Returns true if a Program link with the given name or number exists and is enabled.</summary>
		public static bool IsEnabled(string progName){
			if(HList.ContainsKey(progName) && ((Program)HList[progName]).Enabled){
				return true;
			}
			return false;
		}

		///<summary></summary>
		public static bool IsEnabled(int programNum){
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].ProgramNum==programNum && Listt[i].Enabled){
					return true;
				}
			}
			return false;
		}

		///<summary>Supply a valid program Name, and this will set Cur to be the corresponding Program object.</summary>
		public static Program GetCur(string progName){
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].ProgName==progName){
					return Listt[i];
				}
			}
			return null;//to signify that the program could not be located. (user deleted it in an older version)
		}

		///<summary>Supply a valid program Name.  Will return 0 if not found.</summary>
		public static int GetProgramNum(string progName) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ProgName==progName) {
					return Listt[i].ProgramNum;
				}
			}
			return 0;
		}

		///<summary>Typically used when user clicks a button to a Program link.  This method attempts to identify and execute the program based on the given programNum.</summary>
		public static void Execute(int programNum,Patient pat){
			Program prog=null;
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].ProgramNum==programNum){
					prog=Listt[i];
				}
			}
			if(prog==null) {//no match was found
				MessageBox.Show("Error, program entry not found in database.");
				return;
			}
			if(prog.ProgName=="Apteryx") {
				Apteryx.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="DBSWin") {
				DBSWin.SendData(prog,pat);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(prog.ProgName=="DentalEye") {
				DentalEye.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="DentX") {
				DentX.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="DrCeph") {
				DrCeph.SendData(prog,pat);
				return;
			}
#endif
			else if(prog.ProgName=="DentForms") {
				DentForms.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Dexis") {
				Dexis.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Digora") {
				Digora.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Dolphin") {
				Dolphin.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Dxis") {
				Dxis.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="FloridaProbe") {
				FloridaProbe.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="HouseCalls") {
				FormHouseCalls FormHC=new FormHouseCalls();
				FormHC.ProgramCur=prog;
				FormHC.ShowDialog();
				return;
			}
			else if(prog.ProgName=="ImageFX") {
				ImageFX.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Lightyear") {
				Lightyear.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="MediaDent") {
				MediaDent.SendData(prog,pat);
				return;
			}
			//else if(prog.ProgName=="NewPatientForm.com") {
			//	NewPatientForm npf=new NewPatientForm();
			//	npf.ShowDownload(prog.Path);//NewPatientForm.com
			//	return;
			//}
			else if(prog.ProgName=="Owandy") {
				Owandy.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="PerioPal") {
				PerioPal.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Planmeca") {
				Planmeca.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="PT") {
				PaperlessTechnology.SendData(prog,pat,false);
				return;
			}
			else if(prog.ProgName=="PTupdate") {
				PaperlessTechnology.SendData(prog,pat,true);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(prog.ProgName=="Schick") {
				Schick.SendData(prog,pat);
				return;
			}
#endif
			else if(prog.ProgName=="Sirona") {
				Sirona.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="TigerView"){
				TigerView.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Trophy") {
				Trophy.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="TrophyEnhanced") {
				TrophyEnhanced.SendData(prog,pat);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(prog.ProgName=="Vipersoft") {
				Vipersoft.SendData(prog,pat);
				return;
			}
#endif
			else if(prog.ProgName=="VixWin") {
				VixWin.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="VixWinOld") {
				VixWinOld.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="XDR") {
				Dexis.SendData(prog,pat);//XDR uses the Dexis protocol
				return;
			}
			//all remaining programs:
			try{
				string cmdline=prog.CommandLine;
				string path=prog.Path;
				if(pat!=null) {
					cmdline=cmdline.Replace("[PatNum]",pat.PatNum.ToString());
					cmdline=cmdline.Replace("[ChartNumber]",pat.ChartNumber);
					path=path.Replace("[PatNum]",pat.PatNum.ToString());
					path=path.Replace("[ChartNumber]",pat.ChartNumber);
				}
				Process.Start(path,cmdline);
			}
			catch{
				MessageBox.Show(prog.ProgDesc+" is not available.");
				return;
			}
		}




	}

	

	


}










