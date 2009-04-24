using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	///<summary></summary>
	public class TigerView {
		private static string iniFile;

		public TigerView() {
		}

		///<summary>A equivalant WriteProfile.. method that should work in linux.
		///Hashtable used to hold key and values.  Look for key and change middle section to key=value.
		///This also puts in a number of checks to prevent crashing.  Returns false if ther was an error creating the ini file.  
		///This also does all of the writing at one time reducing the risk of having file write problems.  The previous
		///method wrote each key separately.  Downside is  ini file has to meet exactly what we are looking for
		///keys need to have same text Case.  Section header has to have same case as well.  I really don't think
		///this will be a problem.</summary>
		private static bool WritePrivatePofileString2(string section,System.Collections.Hashtable keyVals,string filePath) {
			bool rVal=true;
			System.IO.StreamReader sr=null;
			System.IO.StreamWriter sw=null;
			System.Collections.Generic.List<string> Before_Section_Collection=new System.Collections.Generic.List<string>();
			System.Collections.Generic.List<string> Section_Collection=new System.Collections.Generic.List<string>();
			System.Collections.Generic.List<string> After_Section_Collection=new System.Collections.Generic.List<string>();
			System.Text.RegularExpressions.Regex r=new System.Text.RegularExpressions.Regex("\\[\\s*.+\\s*\\]");
			System.Text.RegularExpressions.Regex regSection=new System.Text.RegularExpressions.Regex("\\[\\s*"+section+"\\s*\\]");
			/// Create a backup file name
			string backupFileName=filePath+".bak0001";
			if(System.IO.File.Exists(backupFileName))
				for(int i=2;i<2000;i++) {
					string numString=String.Format("{0:0000}",i);     //0001, 0002 ect
					backupFileName=filePath+".bak"+numString;
					if(!System.IO.File.Exists(backupFileName)){
						break;
					}
				}
			try{
				//Create the backup file
				System.IO.File.Copy(filePath,backupFileName);
				sr=new StreamReader(filePath);
				string line1;
				bool SectionFound=false;
				bool BeforeSection=true;
				bool AfterSection=false;
				while((line1=sr.ReadLine())!=null) {
					// Locate Section
					System.Text.RegularExpressions.Match m1=regSection.Match(line1);
					if(m1.Success){
						SectionFound=true;
						BeforeSection=false;
					}
					if(SectionFound&&!AfterSection){
						if(Section_Collection.Count!=0&&r.Match(line1).Success){
							AfterSection=true;
						}
					}
					//Record line in collections
					if(BeforeSection){
						Before_Section_Collection.Add(line1);
					}
					if(SectionFound&&!AfterSection){
						Section_Collection.Add(line1);
					}
					if(AfterSection){
						After_Section_Collection.Add(line1);
					}
				}
				sr.Close(); //need to make sure file is not open so we can rewrite it
				sr.Dispose();
				sr=null;
				//Now have the three sections of lines
				//Let us now change the entries in the section in question
				foreach(DictionaryEntry de in keyVals) {
					for(int i=0;i<Section_Collection.Count;i++) {
						if(Section_Collection[i].Contains(de.Key.ToString()+"=")) {
							Section_Collection[i]=de.Key.ToString()+"="+de.Value.ToString();
							break;
						}
					}
				}
				//-- rewrite files
				sw=new StreamWriter(filePath);
				for(int i=0;i<Before_Section_Collection.Count;i++){
					sw.WriteLine(Before_Section_Collection[i]);
				}
				for(int i=0;i<Section_Collection.Count;i++){
					sw.WriteLine(Section_Collection[i]);
				}
				for(int i=0;i<After_Section_Collection.Count;i++){
					sw.WriteLine(After_Section_Collection[i]);
				}
				sw.Close();
				//All is good remove the backup file
				if(System.IO.File.Exists(backupFileName)){
					System.IO.File.Delete(backupFileName);
				}
			}
			catch{
				if(System.IO.File.Exists(backupFileName)){
					System.IO.File.Copy(backupFileName,filePath); // restore backup
				}
				MessageBox.Show("There was an error writing to file: "+filePath+"\nThe operation you have just tried to do will likely fail");
				rVal=false;
			}finally{
				if(sr!=null){
					sr.Close();
				}
				if(sw!=null){
					sw.Close();
				}
			}
			return rVal;
		}

		///<summary>
		///This is the new version of SendData that should be linux compliant.
		///Advoids the kernal32.WritePrivateProfileString call
		///</summary>
		public static void SendData(Program ProgramCur,Patient pat) {
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram,"Tiger1.ini path");
			iniFile=PPCur.PropertyValue;
			System.Collections.Hashtable htKeyVals=new Hashtable();
			if(pat!=null){
				if(!File.Exists(iniFile)) {
					MessageBox.Show("Could not find "+iniFile);
					return;
				}
				htKeyVals["LastName"]=pat.LName;
				htKeyVals["FirstName"]=pat.FName;
				//Patient Id can be any string format.
				PPCur=ProgramProperties.GetCur(ForProgram,"Enter 0 to use PatientNum, or 1 to use ChartNum");
				if(PPCur.PropertyValue=="0"){
					htKeyVals["PatientID"]=pat.PatNum.ToString();
				}else{
					htKeyVals["PatientID"]=pat.ChartNumber;
				}
				htKeyVals["PatientSSN"]=pat.SSN;
				//WriteValue("SubscriberSSN",pat);
				if(pat.Gender==PatientGender.Female){
					htKeyVals["Gender"]="Female";
				}else{
					htKeyVals["Gender"]="Male";
				}
				htKeyVals["DOB"]=pat.Birthdate.ToString("MM/dd/yy");
				//WriteValue("ClaimID",pat);??huh
				htKeyVals["AddrStreetNo"]=pat.Address;
				//WriteValue("AddrStreetName",pat);??
				//WriteValue("AddrSuiteNo",pat);??
				htKeyVals["AddrCity"]=pat.City;
				htKeyVals["AddrState"]=pat.State;
				htKeyVals["AddrZip"]=pat.Zip;
				htKeyVals["PhHome"]=LimitLength(pat.HmPhone,13);
				htKeyVals["PhWork"]=LimitLength(pat.WkPhone,13);
				if(!WritePrivatePofileString2("Slave",htKeyVals,iniFile)){
					MessageBox.Show(Lan.g(null,"Unable to start external program: ")+ProgramCur.Path);
				}else{
					try{
						Process.Start(ProgramCur.Path,ProgramCur.CommandLine);
					}catch{
						MessageBox.Show(ProgramCur.Path+" is not available.");
					}
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start TigerView without bringing up a pt.
				}catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

		private static string LimitLength(string str,int length) {
			if(str.Length<length+1) {
				return str;
			}
			return str.Substring(0,length);
		}

	}
}










