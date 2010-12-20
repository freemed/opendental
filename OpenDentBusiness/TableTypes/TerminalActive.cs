using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>Each row is one computer that currently acting as a terminal for new patient info input.</summary>
	[Serializable]
	public class TerminalActive:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TerminalActiveNum;
		///<summary>The name of the computer where the terminal is active.</summary>
		public string ComputerName;
		///<summary>Enum:TerminalStatusEnum  No longer used.  Instead, the PatNum field is used.  Used to indicates at what point the patient was in the sequence. 0=standby, 1=PatientInfo, 2=Medical, 3=UpdateOnly.  If status is 1, then nobody else on the network could open the patient edit window for that patient.</summary>
		public TerminalStatusEnum TerminalStatus;
		///<summary>FK to patient.PatNum.  The patient currently showing in the terminal.  If 0, then terminal is in standby mode.</summary>
		public long PatNum;

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










