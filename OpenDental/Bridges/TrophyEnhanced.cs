using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class TrophyEnhanced{

		/// <summary></summary>
		public TrophyEnhanced(){
			
		}

		///<summary>Launches the program using command line.  It is confirmed that there is no space after the -P or -N</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat==null){
				try{
					Process.Start(ProgramCur.Path);//should start Trophy without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
				return;
			}
			string storagePath=ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"Storage Path");
			if(!Directory.Exists(storagePath)){
				MessageBox.Show("Invalid storage path: "+storagePath);
				return;
			}
			string patFolder="";
			if(pat.TrophyFolder=="") {
				try{
					patFolder=AutomaticallyGetTrophyFolder(pat,storagePath);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					return;
				}
				if(patFolder=="") {//exit without displaying any further message.
					return;
				}
			}
			else {//pat.TrophyFolder was already previously entered.
				patFolder=ODFileUtils.CombinePaths(storagePath,pat.TrophyFolder);
			}
			//can't do this because the folder won't exist yet for new patients.
			//if(!Directory.Exists(patFolder)) {
			//	MessageBox.Show("Invalid patient folder: "+patFolder);
			//	return;
			//}
			string comline="-P"+patFolder
				+" -N"+Tidy(pat.LName)+","+Tidy(pat.FName);
			//MessageBox.Show(comline);
			try{
				Process.Start(ProgramCur.Path,comline);
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

		///<summary>Guaranteed to always return a valid foldername unless major error or user chooses to exit.  This also saves the TrophyFolder value to this patient in the db.</summary>
		private static string AutomaticallyGetTrophyFolder(Patient pat,string storagePath) {
			string retVal="";
			//try to find the correct trophy folder
			string rvgPortion=pat.LName.Substring(0,1)+".rvg";
			string alphaPath=ODFileUtils.CombinePaths(storagePath,rvgPortion);
			if(!Directory.Exists(alphaPath)) {
				throw new ApplicationException("Could not find expected path: "+alphaPath+".  The enhanced Trophy bridge assumes that folders already exist with that naming convention.");
			}
			DirectoryInfo dirInfo=new DirectoryInfo(alphaPath);
			DirectoryInfo[] dirArray=dirInfo.GetDirectories();
			List<TrophyFolder> listMatchesNot=new List<TrophyFolder>();//list of all patients found, all with same first letter of last name.
			List<TrophyFolder> listMatchesName=new List<TrophyFolder>();//list of all perfect matches for name but not birthdate.
			TrophyFolder folder;
			string maxFolderName="";
			string datafilePath;
			string[] datafileLines;
			string date;
			//loop through each folder.
			for(int i=0;i<dirArray.Length;i++) {
				if(String.Compare(dirArray[i].Name,maxFolderName) > 0) {//eg, if G0000035 > G0000024
					maxFolderName=dirArray[i].Name;
				}
				datafilePath=ODFileUtils.CombinePaths(dirArray[i].FullName,"FILEDATA.txt");
				if(!File.Exists(datafilePath)){
					continue;//fail silently.
				}
				//if this folder is already in use by some other patient, then skip
				if(Patients.IsTrophyFolderInUse(dirArray[i].Name)) {
					continue;
				}
				folder=new TrophyFolder();
				folder.FolderName=dirArray[i].Name;
				datafileLines=File.ReadAllLines(datafilePath);
				if(datafileLines.Length<2) {
					continue;
				}
				folder.FName=GetValueFromLines("PRENOM",datafileLines);
				folder.LName=GetValueFromLines("NOM",datafileLines);
				date=GetValueFromLines("DATE",datafileLines);
				try{
					folder.BirthDate=DateTime.ParseExact(date,"yyyyMMdd",CultureInfo.CurrentCulture.DateTimeFormat);
				}
				catch{}
				if(pat.LName.ToUpper()==folder.LName.ToUpper() && pat.FName.ToUpper()==folder.FName.ToUpper()) {
					if(pat.Birthdate==folder.BirthDate) {
						//We found a perfect match here, so do not display any dialog to user.
						retVal=rvgPortion+@"\"+dirArray[i].Name;
					}
					else{//name is perfect match, but not birthdate.  Maybe birthdate was not entered in one system or the other.
						listMatchesName.Add(folder);
					}
				}
				listMatchesNot.Add(folder);
			}
			if(retVal=="") {//perfect match not found
				if(listMatchesName.Count==1) {//exactly one name matched even though birthdays did not
					retVal=rvgPortion+@"\"+listMatchesName[0].FolderName;
				}
				else{//no or multiple matches
					FormTrophyNamePick formPick=new FormTrophyNamePick();
					formPick.ListMatches=listMatchesNot;
					formPick.ShowDialog();
					if(formPick.DialogResult!=DialogResult.OK) {
						return "";//triggers total exit
					}
					if(formPick.PickedName=="") {//Need to generate new folder name
						int maxInt=0;
						if(maxFolderName!="") {
							maxInt=PIn.PInt(maxFolderName.Substring(1));//It will crash here if can't parse the int.
						}
						maxInt++;
						string paddedInt=maxInt.ToString().PadLeft(7,'0');
						retVal=rvgPortion+@"\"+pat.LName.Substring(0,1).ToUpper()+paddedInt;
					}
					else {
						retVal=rvgPortion+@"\"+formPick.PickedName;
					}
				}
			}
			Patient patOld=pat.Copy();
			pat.TrophyFolder=retVal;
			Patients.Update(pat,patOld);
			return retVal;
		}

		private static string GetValueFromLines(string keyName,string[] lines) {
			for(int i=0;i<lines.Length;i++) {
				if(lines[i].StartsWith(keyName)) {
					int equalsPos=lines[i].IndexOf('=');
					if(equalsPos==-1) {
						return "";
					}
					if(lines[i].Length <= equalsPos+1){//eg, L=4, equalsPos=3 (last char). Nothing comes after =.
						return "";
					}
					string retVal=lines[i].Substring(equalsPos+1);
					retVal=retVal.TrimStart(' ');
					return retVal;
				}
			}
			return "";
		}

		private static string Tidy(string str) {
			string retVal=str.Replace("\"","");//gets rid of any quotes
			retVal=retVal.Replace("'","");//gets rid of any single quotes
			return retVal;
		}

	}

	public class TrophyFolder {
		///<summary>The simple folder name, like G000002, without the .rvg\</summary>
		public string FolderName;
		public string LName;
		public string FName;
		public DateTime BirthDate;
	}
}










