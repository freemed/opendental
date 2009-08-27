using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Corresponds to the autocodeitem table in the database.  There are multiple AutoCodeItems for a given AutoCode.  Each Item has one ADA code.</summary>
	public class AutoCodeItem{
		///<summary>Primary key.</summary>
		public long AutoCodeItemNum;
		///<summary>FK to autocode.AutoCodeNum</summary>
		public long AutoCodeNum;
		///<summary>Do not use</summary>
		public string OldCode;
		///<summary>FK to procedurecode.CodeNum</summary>
		public long CodeNum;
	}





	
	


}









