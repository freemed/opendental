using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Attached to procbuttons.  These tell the program what to do when a user clicks on a button.  There are two types: proccodes or autocodes.</summary>
	public class ProcButtonItem{
		///<summary>Primary key.</summary>
		public int ProcButtonItemNum;
		///<summary>FK to procbutton.ProcButtonNum.</summary>
		public int ProcButtonNum;
		///<summary>Do not use.</summary>
		public string OldCode;
		///<summary>FK to autocode.AutoCodeNum.  0 if this is a procedure code.</summary>
		public int AutoCodeNum;
		///<summary>FK to procedurecode.CodeNum.  0 if this is an autocode.</summary>
		public int CodeNum;

		///<summary></summary>
		public ProcButtonItem Copy() {
			ProcButtonItem p=new ProcButtonItem();
			p.ProcButtonItemNum=ProcButtonItemNum;
			p.ProcButtonNum=ProcButtonNum;
			//p.OldCode=OldCode;
			p.AutoCodeNum=AutoCodeNum;
			p.CodeNum=CodeNum;
			return p;
		}


	}

	




}










