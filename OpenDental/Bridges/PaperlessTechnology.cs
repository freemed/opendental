using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary>PT Dental.  www.gopaperlessnow.com</summary>
	public class PaperlessTechnology{
		private static string exportAddCsv=@"C:\PT\USI\addpatient_OD.csv";
		private static string exportUpdateCsv=@"C:\PT\USI\updatepatient_OD.csv";
		private static string importCsv=@"C:\PT\USI\patientinfo_PT.csv";
		private static string importMedCsv=@"C:\PT\USI\patientmedalerts.csv";
		private static string exportAddExe=@"C:\PT\USI\addpatient_OD.exe";
		private static string exportUpdateExe=@"C:\PT\USI\updatepatient_OD.exe";
		private static FileSystemWatcher watcher;

		/// <summary></summary>
		public PaperlessTechnology(){
			
		}

		///<Summary>There might be incoming files that we have to watch for.  They will get processed and deleted.  There is no user interface for this function.  This method is called when OD first starts up.</Summary>
		public static void InitializeFileWatcher(){
			string importDir=Path.GetDirectoryName(importCsv);
			if(!Directory.Exists(importDir)){
				if(watcher!=null){
					watcher.Dispose();
				}
				return;
			}
			watcher = new FileSystemWatcher();
			watcher.Path = importDir;
			//watcher.NotifyFilter = NotifyFilters.CreationTime;
			string importFileName=Path.GetFileName(importCsv);
			watcher.Filter=importFileName;
			watcher.Created+=new FileSystemEventHandler(OnCreated);
			watcher.EnableRaisingEvents = true;
		}

		private static void OnCreated(object source,FileSystemEventArgs e) {
			MessageBox.Show("File created.  It will now be deleted.");
			File.Delete(e.FullPath);
		}


		///<summary>Sends data for Patient.Cur to an export file and then launches an exe to notify PT.  Set isUpdate to false to trigger simple open or insert.  In PT, update is a separate function with a separate button.</summary>
		public static void SendData(Program ProgramCur, Patient pat,bool isUpdate){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "InfoFile path");
			string infoFile=PPCur.PropertyValue;
			if(pat!=null){
				try{
					using(StreamWriter sw=new StreamWriter(infoFile,false)){
						sw.WriteLine(pat.LName+", "+pat.FName
							+"  "+pat.Birthdate.ToShortDateString()
							+"  ("+pat.PatNum.ToString()+")");
						//patientID can be any string format, max 8 char.
						//There is no validation to ensure that length is 8 char or less.
						PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
						if(PPCur.PropertyValue=="0"){
							sw.WriteLine("PN="+pat.PatNum.ToString());
						}
						else{
							sw.WriteLine("PN="+pat.ChartNumber);
						}
						//sw.WriteLine("PN="+pat.PatNum.ToString());
						sw.WriteLine("LN="+pat.LName);
						sw.WriteLine("FN="+pat.FName);
						sw.WriteLine("BD="+pat.Birthdate.ToShortDateString());
						if(pat.Gender==PatientGender.Female)
							sw.WriteLine("SX=F");
						else
							sw.WriteLine("SX=M");
					}
					Process.Start(ProgramCur.Path,"@"+infoFile);
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start Dexis without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

	}
}










