using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in License Tool (FormLicenseTool).</summary>
	public class ProcLicense {
		///<summary>Primary key.</summary>
		public int ProcLicenseNum;
		///<summary>The code that the user typed in to identify procedures.</summary>
		public string ProcCode;
		///<summary>The description of the code.</summary>
		public string Descript;

		public ProcLicense Copy(){
			ProcLicense pl=new ProcLicense();
			pl.ProcLicenseNum=ProcLicenseNum;
			pl.ProcCode=ProcCode;
			pl.Descript=Descript;
			return pl;
		}

	}
}
