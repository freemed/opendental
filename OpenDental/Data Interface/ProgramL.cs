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
			if(prog.ProgName==ProgramName.Apteryx.ToString()) {
				Apteryx.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Camsight.ToString()) {
				Camsight.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Cerec.ToString()) {
				Cerec.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.CliniView.ToString()) {
				Cliniview.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.DBSWin.ToString()) {
				DBSWin.SendData(prog,pat);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(prog.ProgName==ProgramName.DentalEye.ToString()) {
				DentalEye.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.DentX.ToString()) {
				DentX.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.DrCeph.ToString()) {
				DrCeph.SendData(prog,pat);
				return;
			}
#endif
			else if(prog.ProgName==ProgramName.DentForms.ToString()) {
				DentForms.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Dexis.ToString()) {
				Dexis.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Digora.ToString()) {
				Digora.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Dolphin.ToString()) {
				Dolphin.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Dxis.ToString()) {
				Dxis.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.EwooEZDent.ToString()) {
				Ewoo.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.FloridaProbe.ToString()) {
				FloridaProbe.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.HouseCalls.ToString()) {
				FormHouseCalls FormHC=new FormHouseCalls();
				FormHC.ProgramCur=prog;
				FormHC.ShowDialog();
				return;
			}
			else if(prog.ProgName==ProgramName.iCat.ToString()) {
				ICat.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.ImageFX.ToString()) {
				ImageFX.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Lightyear.ToString()) {
				Lightyear.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.MediaDent.ToString()) {
				MediaDent.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Owandy.ToString()) {
				Owandy.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Patterson.ToString()) {
				Patterson.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.PerioPal.ToString()) {
				PerioPal.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Planmeca.ToString()) {
				Planmeca.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Progeny.ToString()) {
				Progeny.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.PT.ToString()) {
				PaperlessTechnology.SendData(prog,pat,false);
				return;
			}
			else if(prog.ProgName==ProgramName.PTupdate.ToString()) {
				PaperlessTechnology.SendData(prog,pat,true);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(prog.ProgName==ProgramName.Schick.ToString()) {
				Schick.SendData(prog,pat);
				return;
			}
#endif
			else if(prog.ProgName==ProgramName.Sirona.ToString()) {
				Sirona.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Sopro.ToString()) {
				Sopro.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.TigerView.ToString()) {
				TigerView.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.Trophy.ToString()) {
				Trophy.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.TrophyEnhanced.ToString()) {
				TrophyEnhanced.SendData(prog,pat);
				return;
			}
#if !DISABLE_WINDOWS_BRIDGES
			else if(prog.ProgName==ProgramName.Vipersoft.ToString()) {
				Vipersoft.SendData(prog,pat);
				return;
			}
#endif
			else if(prog.ProgName==ProgramName.VixWin.ToString()) {
				VixWin.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.VixWinOld.ToString()) {
				VixWinOld.SendData(prog,pat);
				return;
			}
			else if(prog.ProgName==ProgramName.XDR.ToString()) {
				Dexis.SendData(prog,pat);//XDR uses the Dexis protocol
				return;
			}
			//all remaining programs:
			try{
				string cmdline=prog.CommandLine;
				string path=prog.Path;
				if(pat!=null) {
					cmdline=cmdline.Replace("[LName]",pat.LName);
					cmdline=cmdline.Replace("[FName]",pat.FName);
					cmdline=cmdline.Replace("[PatNum]",pat.PatNum.ToString());
					cmdline=cmdline.Replace("[ChartNumber]",pat.ChartNumber);
					cmdline=cmdline.Replace("[WirelessPhone]",pat.WirelessPhone);
					cmdline=cmdline.Replace("[HmPhone]",pat.HmPhone);
					path=path.Replace("[LName]",pat.LName);
					path=path.Replace("[FName]",pat.FName);
					path=path.Replace("[PatNum]",pat.PatNum.ToString());
					path=path.Replace("[ChartNumber]",pat.ChartNumber);
					path=path.Replace("[WirelessPhone]",pat.WirelessPhone);
					path=path.Replace("[HmPhone]",pat.HmPhone);
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