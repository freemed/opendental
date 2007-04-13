using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
//using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class TrophyEnhanced{

		/// <summary></summary>
		public TrophyEnhanced(){
			
		}

		///<summary>Launches the program using command line.  It is confirmed that there is no space after the -P or -N</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);;
			if(pat!=null){
				if(pat.TrophyFolder==""){
					MessageBox.Show("You must first enter a value for the Trophy Folder in the Patient Edit Window.");
					return;
				}
				ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Storage Path");
				string comline="-P"+PPCur.PropertyValue+@"\";
				comline+=pat.TrophyFolder;
				comline+=" -N"+pat.LName+","+pat.FName;
				comline=comline.Replace("\"","");//gets rid of any quotes
				comline=comline.Replace("'","");//gets rid of any single quotes
				//MessageBox.Show(comline);
				try{
					Process.Start(ProgramCur.Path,comline);
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}//if patient is loaded
			else{
				try{
					Process.Start(ProgramCur.Path);//should start Trophy without bringing up a pt.
				}
				catch{
					MessageBox.Show(ProgramCur.Path+" is not available.");
				}
			}
		}

	}
}










