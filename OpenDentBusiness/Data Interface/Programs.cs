using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary></summary>
	public class Programs{

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM program ORDER BY ProgDesc";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Program";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			ProgramC.HList=new Hashtable();
			ProgramC.Listt=Crud.ProgramCrud.TableToList(table);
			for(int i=0;i<ProgramC.Listt.Count;i++){
				if(!ProgramC.HList.ContainsKey(ProgramC.Listt[i].ProgName)) {
					ProgramC.HList.Add(ProgramC.Listt[i].ProgName,ProgramC.Listt[i]);
				}
			}
			//The lines below are replaced by the logic above.
			/*
			ProgramC.HList=new Hashtable();
			Program prog = new Program();
			ProgramC.Listt=new List<Program>();
			for (int i=0;i<table.Rows.Count;i++){
				prog=new Program();
				prog.ProgramNum =PIn.Long(table.Rows[i][0].ToString());
				prog.ProgName   =PIn.String(table.Rows[i][1].ToString());
				prog.ProgDesc   =PIn.String(table.Rows[i][2].ToString());
				prog.Enabled    =PIn.Bool(table.Rows[i][3].ToString());
				prog.Path       =PIn.String(table.Rows[i][4].ToString());
				prog.CommandLine=PIn.String(table.Rows[i][5].ToString());
				prog.Note       =PIn.String(table.Rows[i][6].ToString());
				prog.PluginDllName=PIn.String(table.Rows[i][7].ToString());
				ProgramC.Listt.Add(prog);
				if(!ProgramC.HList.ContainsKey(prog.ProgName)) {
					ProgramC.HList.Add(prog.ProgName,prog);
				}
			}*/
		}

		///<summary></summary>
		public static void Update(Program Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.ProgramCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(Program Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ProgramNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ProgramNum;
			}
			return Crud.ProgramCrud.Insert(Cur);
		}

		///<summary>This can only be called by the user if it is a program link that they created. Included program links cannot be deleted.  If calling this from ClassConversion, must delete any dependent ProgramProperties first.  It will delete ToolButItems for you.</summary>
		public static void Delete(Program prog){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),prog);
				return;
			}
			string command = "DELETE from toolbutitem WHERE ProgramNum = "+POut.Long(prog.ProgramNum);
			Db.NonQ(command);
			command = "DELETE from program WHERE ProgramNum = '"+prog.ProgramNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Returns true if a Program link with the given name or number exists and is enabled.</summary>
		public static bool IsEnabled(ProgramName progName){
			//No need to check RemotingRole; no call to db.
			if(ProgramC.HList==null) {
				Programs.RefreshCache();
			}
			if(ProgramC.HList.ContainsKey(progName.ToString()) && ((Program)ProgramC.HList[progName.ToString()]).Enabled) {
				return true;
			}
			return false;
		}

		///<summary></summary>
		public static bool IsEnabled(long programNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(ProgramC.Listt[i].ProgramNum==programNum && ProgramC.Listt[i].Enabled) {
					return true;
				}
			}
			return false;
		}

		///<summary>Supply a valid program Name, and this will set Cur to be the corresponding Program object.</summary>
		public static Program GetCur(ProgramName progName) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(ProgramC.Listt[i].ProgName==progName.ToString()) {
					return ProgramC.Listt[i];
				}
			}
			return null;//to signify that the program could not be located. (user deleted it in an older version)
		}

		///<summary>Supply a valid program Name.  Will return 0 if not found.</summary>
		public static long GetProgramNum(ProgramName progName) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(ProgramC.Listt[i].ProgName==progName.ToString()) {
					return ProgramC.Listt[i].ProgramNum;
				}
			}
			return 0;
		}

		/// <summary>Using eClinicalWorks tight integration.</summary>
		public static bool UsingEcwTightMode() {
			//No need to check RemotingRole; no call to db.
			if(Programs.IsEnabled(ProgramName.eClinicalWorks)	&& ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"eClinicalWorksMode")=="0") {
				return true;
			}
			return false;
		}

		/// <summary>Using eClinicalWorks full mode.</summary>
		public static bool UsingEcwFullMode() {
			//No need to check RemotingRole; no call to db.
			if(Programs.IsEnabled(ProgramName.eClinicalWorks)	&& ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"eClinicalWorksMode")=="2") {
				return true;
			}
			return false;
		}

		/// <summary>Returns true if using eCW in tight or full mode.  In these modes, appointments ARE allowed to overlap because we block users from seeing them.</summary>
		public static bool UsingEcwTightOrFullMode() {
			//No need to check RemotingRole; no call to db.
			if(UsingEcwTightMode() || UsingEcwFullMode()) {
				return true;
			}
			return false;
		}

		/// <summary></summary>
		public static bool UsingOrion {
			//No need to check RemotingRole; no call to db.
			get {
				return Programs.IsEnabled(ProgramName.Orion);
			}
		}

		///<summary>Returns the local override path if available or returns original program path.  Always returns a valid path.</summary>
		public static string GetProgramPath(Program program) {
			//No need to check RemotingRole; no call to db.
			string overridePath=ProgramProperties.GetLocalPathOverrideForProgram(program.ProgramNum);
			if(overridePath!="") {
				return overridePath;
			}
			return program.Path;
		}

		///<summary>For each enabled bridge, if the bridge uses a file to transmit patient data to the other software, then we need to remove the files or clear the files when OD is exiting.
		///Required for EHR 2014 module d.7 (as stated by proctor).</summary>
		public static void ScrubExportedPatientData() {
			//List all program links here. If there is nothing to do for that link, then create a comment stating so.
			string path="";
			//Apixia:
			ScrubFileForProperty(ProgramName.Apixia,"System path to Apixia Digital Imaging ini file","",true);//C:\Program Files\Digirex\Switch.ini
			//Apteryx: Has no file paths containing outgoing patient data from Open Dental.
			//BioPAK: Has no file paths containing outgoing patient data from Open Dental.
			//CallFire: Has no file paths containing outgoing patient data from Open Dental.
			//Camsight: Has no file paths containing outgoing patient data from Open Dental.
			//CaptureLink: Has no file paths containing outgoing patient data from Open Dental.
			//Cerec: Has no file paths containing outgoing patient data from Open Dental.
			//CliniView: Has no file paths containing outgoing patient data from Open Dental.
			//ClioSoft: Has no file paths containing outgoing patient data from Open Dental.
			//DBSWin:
			ScrubFileForProperty(ProgramName.DBSWin,"Text file path","",true);//C:\patdata.txt
			//DentalEye: Has no file paths containing outgoing patient data from Open Dental.
			//DentalStudio: Has no file paths containing outgoing patient data from Open Dental.
			//DentForms: Has no file paths containing outgoing patient data from Open Dental.
			//DentX: Has no file paths containing outgoing patient data from Open Dental.
			//Dexis:
			ScrubFileForProperty(ProgramName.Dexis,"InfoFile path","",true);//InfoFile.txt
			//Digora: Has no file paths containing outgoing patient data from Open Dental.
			//Divvy: Has no file paths containing outgoing patient data from Open Dental.
			//Dolphin:
			ScrubFileForProperty(ProgramName.Dolphin,"Filename","",true);//C:\Dolphin\Import\Import.txt
			//DrCeph: Has no file paths containing outgoing patient data from Open Dental.
			//Dxis: Has no file paths containing outgoing patient data from Open Dental.
			//EasyNotesPro: Has no file paths containing outgoing patient data from Open Dental.
			//eClinicalWorks: HL7 files are created, but eCW is supposed to consume and delete them.
			//EvaSoft: Has no file paths containing outgoing patient data from Open Dental.
			//EwooEZDent:
			Program program=Programs.GetCur(ProgramName.EwooEZDent);
			if(program.Enabled) {
				path=Programs.GetProgramPath(program);
				if(File.Exists(path)) {
					string dir=Path.GetDirectoryName(path);
					string linkage=CodeBase.ODFileUtils.CombinePaths(dir,"linkage.xml");
					if(File.Exists(linkage)) {
						try {
							File.Delete(linkage);
						}
						catch {
							//Another instance of OD might be closing at the same time, in which case the delete will fail. Could also be a permission issue or a concurrency issue. Ignore.
						}
					}
				}
			}
			//FloridaProbe: Has no file paths containing outgoing patient data from Open Dental.
			//Guru: Has no file paths containing outgoing patient data from Open Dental.
			//HouseCalls:
			ScrubFileForProperty(ProgramName.HouseCalls,"Export Path","Appt.txt",true);//C:\HouseCalls\Appt.txt
			//IAP: Has no file paths containing outgoing patient data from Open Dental.
			//iCat:
			ScrubFileForProperty(ProgramName.iCat,"XML output file path","",true);//C:\iCat\Out\pm.xml
			//ImageFX: Has no file paths containing outgoing patient data from Open Dental.
			//Lightyear: Has no file paths containing outgoing patient data from Open Dental.
			//MediaDent:
			ScrubFileForProperty(ProgramName.MediaDent,"Text file path","",true);//C:\MediadentInfo.txt
			//MiPACS: Has no file paths containing outgoing patient data from Open Dental.
			//Mountainside: Has no file paths containing outgoing patient data from Open Dental.
			//NewCrop: Has no file paths containing outgoing patient data from Open Dental.
			//Orion: Has no file paths containing outgoing patient data from Open Dental.
			//OrthoPlex: Has no file paths containing outgoing patient data from Open Dental.
			//Owandy: Has no file paths containing outgoing patient data from Open Dental.
			//PayConnect: Has no file paths containing outgoing patient data from Open Dental.
			//Patterson:
			ScrubFileForProperty(ProgramName.Patterson,"System path to Patterson Imaging ini","",true);//C:\Program Files\PDI\Shared files\Imaging.ini
			//PerioPal: Has no file paths containing outgoing patient data from Open Dental.
			//Planmeca: Has no file paths containing outgoing patient data from Open Dental.
			//PracticeWebReports: Has no file paths containing outgoing patient data from Open Dental.
			//Progeny: Has no file paths containing outgoing patient data from Open Dental.
			//PT: Per our website "The files involved get deleted immediately after they are consumed."
			//PTupdate: Per our website "The files involved get deleted immediately after they are consumed."
			//RayMage: Has no file paths containing outgoing patient data from Open Dental.
			//Schick: Has no file paths containing outgoing patient data from Open Dental.
			//Sirona:
			program=Programs.GetCur(ProgramName.Sirona);
			if(program.Enabled) {
				path=Programs.GetProgramPath(program);
				//read file C:\sidexis\sifiledb.ini
				string iniFile=Path.GetDirectoryName(path)+"\\sifiledb.ini";
				if(File.Exists(iniFile)) {
					string sendBox=ReadValueFromIni("FromStation0","File",iniFile);
					if(File.Exists(sendBox)) {
						File.WriteAllText(sendBox,"");//Clear the sendbox instead of deleting.
					}
				}
			}
			//Sopro: Has no file paths containing outgoing patient data from Open Dental.
			//TigerView:
			ScrubFileForProperty(ProgramName.TigerView,"Tiger1.ini path","",false);//C:\Program Files\PDI\Shared files\Imaging.ini.  TigerView complains if the file is not present.
			//Trojan: Has no file paths containing outgoing patient data from Open Dental.
			//Trophy: Has no file paths containing outgoing patient data from Open Dental.
			//TrophyEnhanced: Has no file paths containing outgoing patient data from Open Dental.
			//Tscan: Has no file paths containing outgoing patient data from Open Dental.
			//UAppoint: Has no file paths containing outgoing patient data from Open Dental.
			//Vipersoft: Has no file paths containing outgoing patient data from Open Dental.
			//VixWin: Has no file paths containing outgoing patient data from Open Dental.
			//VixWinBase41: Has no file paths containing outgoing patient data from Open Dental.
			//VixWinOld: Has no file paths containing outgoing patient data from Open Dental.
			//Xcharge: Has no file paths containing outgoing patient data from Open Dental.
			ScrubFileForProperty(ProgramName.XDR,"InfoFile path","",true);//C:\XDRClient\Bin\infofile.txt
		}

		///<summary>Needed for Sirona bridge data scrub in ScrubExportedPatientData().</summary>
		[DllImport("kernel32")]//this is the Windows function for reading from ini files.
		private static extern int GetPrivateProfileStringFromIni(string section,string key,string def
			,StringBuilder retVal,int size,string filePath);

		///<summary>Needed for Sirona bridge data scrub in ScrubExportedPatientData().</summary>
		private static string ReadValueFromIni(string section,string key,string iniFile) {
			StringBuilder strBuild=new StringBuilder(255);
			int i=GetPrivateProfileStringFromIni(section,key,"",strBuild,255,iniFile);
			return strBuild.ToString();
		}

		///<summary>If isRemovable is false, then the file referenced in the program property will be cleared.
		///If isRemovable is true, then the file referenced in the program property will be deleted.</summary>
		private static void ScrubFileForProperty(ProgramName programName,string strFileProperty,string strFilePropertySuffix,bool isRemovable) {
			Program program=Programs.GetCur(programName);
			if(!program.Enabled) {
				return;
			}
			string strFileToScrub=CodeBase.ODFileUtils.CombinePaths(ProgramProperties.GetPropVal(program.ProgramNum,strFileProperty),strFilePropertySuffix);
			if(!File.Exists(strFileToScrub)) {
				return;
			}
			try {
				File.WriteAllText(strFileToScrub,"");//Always clear the file contents, in case deleting fails below.
			}
			catch {
				//Another instance of OD might be closing at the same time, in which case the delete will fail. Could also be a permission issue or a concurrency issue. Ignore.
			}
			if(!isRemovable) {
				return;
			}
			try {
				File.Delete(strFileToScrub);
			}
			catch {
				//Another instance of OD might be closing at the same time, in which case the delete will fail. Could also be a permission issue or a concurrency issue. Ignore.
			}
		}


	}

	

	


}










