using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Attached to procbuttons.  These tell the program what to do when a user clicks on a button.  There are two types: adacodes or autocodes.</summary>
	public class ProcButtonItem{
		///<summary>Primary key.</summary>
		public int ProcButtonItemNum;
		///<summary>FK to procbutton.ProcButtonNum.</summary>
		public int ProcButtonNum;
		///<summary>FK to procedurecode.ADACode.  0 if this is an autocode.</summary>
		public string ADACode;
		///<summary>FK to autocode.AutoCodeNum.  0 if this is a procedure code.</summary>
		public int AutoCodeNum;

		///<summary></summary>
		public ProcButtonItem Copy() {
			ProcButtonItem p=new ProcButtonItem();
			p.ProcButtonItemNum=ProcButtonItemNum;
			p.ProcButtonNum=ProcButtonNum;
			p.ADACode=ADACode;
			p.AutoCodeNum=AutoCodeNum;
			return p;
		}


	}

	




}










