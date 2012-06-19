using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges{
	/// <summary></summary>
	public class CaptureLink{

		///<summary></summary>
		public CaptureLink(){

		}

		///<summary>CaptureLink reads the bridge parameters from the clipboard.</summary>
		//Command format: LName FName PatID
		public static void SendData(Program ProgramCur, Patient pat){
			if(pat==null){
				MessageBox.Show("No patient selected.");
				return;
			}
			ArrayList ForProgram=ProgramProperties.GetForProgram(ProgramCur.ProgramNum);
			string info=Tidy(pat.LName)+" "+Tidy(pat.FName)+" ";
			ProgramProperty PPCur=ProgramProperties.GetCur(ForProgram, "Enter 0 to use PatientNum, or 1 to use ChartNum");
			if(PPCur.PropertyValue=="0"){
				info+=pat.PatNum.ToString();
			}
			else{
				info+=pat.ChartNumber;
			}
			Clipboard.SetText(info,TextDataFormat.Text);
		}

		private static string Tidy(string str){
			return str.Replace("\"","");
		}

	}
}

//WE NEED TO ADD THE FOLLOWING CODE TO ConvertDatabases2.cs WHEN WE CAN VERIFY THE FUNCTIONALITY OF THIS BRIDGE WITH SPECIFICATIONS (WHICH WE DO NOT HAVE YET).
////Insert CaptureLink Bridge
//if(DataConnection.DBtype==DatabaseType.MySql) {
//  command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
//    +") VALUES("
//    +"'CaptureLink', "
//    +"'CaptureLink from www.henryschein.ca', "
//    +"'0', "
//    +"'"+POut.String(@"CaptureLink.exe")+"',"
//    +"'', "
//    +"'')";
//  long programNum=Db.NonQ(command,true);
//  command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
//    +") VALUES("
//    +"'"+POut.Long(programNum)+"', "
//    +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
//    +"'0')";
//  Db.NonQ(command);
//  command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
//    +"VALUES ("
//    +"'"+POut.Long(programNum)+"', "
//    +"'"+POut.Int(((int)ToolBarsAvail.ChartModule))+"', "
//    +"'CaptureLink')";
//  Db.NonQ(command);
//}
//else {//oracle
//  command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
//    +") VALUES("
//    +"(SELECT MAX(ProgramNum)+1 FROM program),"
//    +"'CaptureLink', "
//    +"'CaptureLink from www.henryschein.ca', "
//    +"'0', "
//    +"'"+POut.String(@"CaptureLink.exe")+"',"
//    +"'', "
//    +"'')";
//  long programNum=Db.NonQ(command,true);
//  command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue"
//    +") VALUES("
//    +"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
//    +"'"+POut.Long(programNum)+"', "
//    +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
//    +"'0')";
//  Db.NonQ(command);
//  command="INSERT INTO toolbutitem (ToolButItemNum,ProgramNum,ToolBar,ButtonText) "
//    +"VALUES ("
//    +"(SELECT MAX(ToolButItemNum)+1 FROM toolbutitem),"
//    +"'"+POut.Long(programNum)+"', "
//    +"'"+POut.Int(((int)ToolBarsAvail.ChartModule))+"', "
//    +"'CaptureLink')";
//  Db.NonQ(command);
//}//end CaptureLink bridge