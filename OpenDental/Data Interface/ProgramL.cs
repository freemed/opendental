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
	public class ProgramL{

		///<summary>Typically used when user clicks a button to a Program link.  This method attempts to identify and execute the program based on the given programNum.</summary>
		public static void Execute(long programNum,Patient pat) {
			Program prog=null;
			for(int i=0;i<ProgramC.Listt.Count;i++){
				if(ProgramC.Listt[i].ProgramNum==programNum) {
					prog=ProgramC.Listt[i];
				}
			}
			if(prog==null) {//no match was found
				MessageBox.Show("Error, program entry not found in database.");
				return;
			}
			if(prog.PluginDllName!="") {
				if(pat!=null) {
					Plugins.LaunchToolbarButton(programNum,pat.PatNum);
				}
				return;
			}
			if(prog.ProgName=="Apteryx") {
				Apteryx.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="Camsight") {
				Camsight.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName=="CliniView") {
				Cliniview.SendData(prog,pat);
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
			else if(prog.ProgName=="EwooEZDent") {
				Ewoo.SendData(prog,pat);
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
			else if(prog.ProgName=="iCat") {
				ICat.SendData(prog,pat);
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