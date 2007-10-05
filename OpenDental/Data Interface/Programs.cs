using System;
using System.Collections;
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
		public static Program[] List;

		///<summary></summary>
		public static void Refresh(){
			//MessageBox.Show("refreshing");
			HList=new Hashtable();
			Program tempProgram = new Program();
			string command = 
				"SELECT * from program ORDER BY ProgDesc";
			DataTable table=General.GetTable(command);
			List=new Program[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				tempProgram=new Program();
				tempProgram.ProgramNum =PIn.PInt   (table.Rows[i][0].ToString());
				tempProgram.ProgName   =PIn.PString(table.Rows[i][1].ToString());
				tempProgram.ProgDesc   =PIn.PString(table.Rows[i][2].ToString());
				tempProgram.Enabled    =PIn.PBool  (table.Rows[i][3].ToString());
				tempProgram.Path       =PIn.PString(table.Rows[i][4].ToString());
				tempProgram.CommandLine=PIn.PString(table.Rows[i][5].ToString());
				tempProgram.Note       =PIn.PString(table.Rows[i][6].ToString());
				List[i]=tempProgram;
				if(!HList.ContainsKey(tempProgram.ProgName)){
					HList.Add(tempProgram.ProgName,tempProgram);
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
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==programNum && List[i].Enabled){
					return true;
				}
			}
			return false;
		}

		///<summary>Supply a valid program Name, and this will set Cur to be the corresponding Program object.</summary>
		public static Program GetCur(string progName){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgName==progName){
					return List[i];
				}
			}
			return null;//to signify that the program could not be located. (user deleted it in an older version)
		}

		///<summary>Typically used when user clicks a button to a Program link.  This method attempts to identify and execute the program based on the given programNum.</summary>
		public static void Execute(int programNum,Patient pat){
			Program Cur=null;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==programNum){
					Cur=List[i];
				}
			}
			if(Cur==null){//no match was found
				MessageBox.Show("Error, program entry not found in database.");
				return;
			}
			if(Cur.ProgName=="Apteryx") {
				Apteryx.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="DBSWin") {
				DBSWin.SendData(Cur,pat);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(Cur.ProgName=="DentalEye") {
				DentalEye.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="DentX") {
				DentX.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="DrCeph") {
				DrCeph.SendData(Cur,pat);
				return;
			}
#endif
			else if(Cur.ProgName=="DentForms") {
				DentForms.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="Dexis") {
				Dexis.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="Dxis") {
				Dxis.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="FloridaProbe") {
				FloridaProbe.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="HouseCalls") {
				FormHouseCalls FormHC=new FormHouseCalls();
				FormHC.ProgramCur=Cur;
				FormHC.ShowDialog();
				return;
			}
			else if(Cur.ProgName=="ImageFX") {
				ImageFX.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="Lightyear") {
				Lightyear.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="MediaDent") {
				MediaDent.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="NewPatientForm.com") {
				NewPatientForm npf=new NewPatientForm();
				npf.ShowDownload(Cur.Path);//NewPatientForm.com
				return;
			}
			else if(Cur.ProgName=="Owandy") {
				Owandy.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="PerioPal") {
				PerioPal.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="Planmeca") {
				Planmeca.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="PT") {
				PaperlessTechnology.SendData(Cur,pat,false);
				return;
			}
			else if(Cur.ProgName=="PTupdate") {
				PaperlessTechnology.SendData(Cur,pat,true);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(Cur.ProgName=="Schick") {
				Schick.SendData(Cur,pat);
				return;
			}
#endif
			else if(Cur.ProgName=="Sirona") {
				Sirona.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="TigerView"){
				TigerView.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="Trophy") {
				Trophy.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="TrophyEnhanced") {
				TrophyEnhanced.SendData(Cur,pat);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(Cur.ProgName=="Vipersoft") {
				Vipersoft.SendData(Cur,pat);
				return;
			}
#endif
			else if(Cur.ProgName=="VixWin") {
				VixWin.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="VixWinOld") {
				VixWinOld.SendData(Cur,pat);
				return;
			}
			else if(Cur.ProgName=="XDR") {
				Dexis.SendData(Cur,pat);//XDR uses the Dexis protocol
				return;
			}
			//all remaining programs:
			try{
				string cmdline=Cur.CommandLine;
				cmdline=cmdline.Replace("[PatNum]",pat.PatNum.ToString());
				cmdline=cmdline.Replace("[ChartNumber]",pat.ChartNumber);
				string path=Cur.Path;
				path=path.Replace("[PatNum]",pat.PatNum.ToString());
				path=path.Replace("[ChartNumber]",pat.ChartNumber);
				Process.Start(path,cmdline);
			}
			catch{
				MessageBox.Show(Cur.ProgDesc+" is not available.");
				return;
			}
		}




	}

	

	


}










