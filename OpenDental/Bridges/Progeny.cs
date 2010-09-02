using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CodeBase;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	public class Progeny {

		///<summary>Launches the program using command line.</summary>
		public static void SendData(Program ProgramCur, Patient pat){
			Process pibridge;
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			try{
				if(pat!=null){
					ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
					string id="";
					if(PPCur.PropertyValue=="0"){
						id=pat.PatNum.ToString();
					}
					else{
						id=pat.ChartNumber;
					}
					string lname=pat.LName.Replace("\"","").Replace(",","");
					string fname=pat.FName.Replace("\"","").Replace(",","");
					Process[] progenyInstances=Process.GetProcessesByName("ProgenyImaging");
					if(progenyInstances.Length==0){
						pibridge=new Process();
						pibridge.StartInfo.CreateNoWindow=false;
						pibridge.StartInfo.UseShellExecute=true;
						pibridge.StartInfo.FileName=ProgramCur.Path;
						pibridge.StartInfo.Arguments="cmd=start";
						pibridge.Start();
						//We must now wait until progeny is completely initialized, or else the following commands will have no effect.
						Thread.Sleep(TimeSpan.FromSeconds(10));
					}
					else{
						pibridge=new Process();
						pibridge.StartInfo.CreateNoWindow=false;
						pibridge.StartInfo.UseShellExecute=true;
						pibridge.StartInfo.FileName=ProgramCur.Path;
						pibridge.StartInfo.Arguments="cmd=show";
						pibridge.Start();
					}
					pibridge=new Process();
					pibridge.StartInfo.CreateNoWindow=false;
					pibridge.StartInfo.UseShellExecute=true;
					pibridge.StartInfo.FileName=ProgramCur.Path;
					pibridge.StartInfo.Arguments="cmd=open, id="+id+", first="+fname+", last="+lname;
					pibridge.Start();
				}//if patient is loaded
				else{
					//Should start Progeny without bringing up a pt.
					pibridge=new Process();
					pibridge.StartInfo.CreateNoWindow=false;
					pibridge.StartInfo.UseShellExecute=true;
					pibridge.StartInfo.FileName=ProgramCur.Path;
					pibridge.StartInfo.Arguments="cmd=start";
					pibridge.Start();
				}
			}
			catch{
				MessageBox.Show(ProgramCur.Path+" is not available.");
			}
		}

	}
}
