using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in License Tool (FormLicenseTool).</summary>
	public class ProcLicense {
		///<summary>Primary key.</summary>
		public int ProcLicenseNum;
		///<summary>The ADA code used to identify procedures.</summary>
		public string ADACode;
		///<summary>The description of the ADA code.</summary>
		public string Descript;

		public ProcLicense Copy(){
			ProcLicense pl=new ProcLicense();
			pl.ProcLicenseNum=ProcLicenseNum;
			pl.ADACode=ADACode;
			pl.Descript=Descript;
			return pl;
		}

	}
}
