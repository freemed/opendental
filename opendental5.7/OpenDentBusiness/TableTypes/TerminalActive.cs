using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>Each row is one computer that currently acting as a terminal for new patient info input.</summary>
	public class TerminalActive{
		///<summary>Primary key.</summary>
		public int TerminalActiveNum;
		///<summary>The name of the computer where the terminal is active.</summary>
		public string ComputerName;
		///<summary>Enum:TerminalStatusEnum  Indicates at what point the patient is in the sequence. 0=standby, 1=PatientInfo, 2=Medical, 3=UpdateOnly.  If status is 1, then nobody else on the network can open the patient edit window for that patient.</summary>
		public TerminalStatusEnum TerminalStatus;
		///<summary>FK to patient.PatNum.  The patient currently showing in the terminal.  0 if terminal is in standby mode.</summary>
		public int PatNum;

		///<summary></summary>
		public TerminalActive Copy() {
			TerminalActive t=new TerminalActive();
			t.TerminalActiveNum=TerminalActiveNum;
			t.ComputerName=ComputerName;
			t.TerminalStatus=TerminalStatus;
			t.PatNum=PatNum;
			return t;
		}

	}

		



		
	

	

	


}










