using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {

	///<summary>There is either one or zero per claim.</summary>
	[Serializable()]
	public class ClaimCondCodeLog:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ClaimCondCodeLogNum;
		///<summary>FK to claim.ClaimNum.</summary>
		public long ClaimNum;
		public string Code0;
		public string Code1;
		public string Code2;
		public string Code3;
		public string Code4;
		public string Code5;
		public string Code6;
		public string Code7;
		public string Code8;
		public string Code9;
		public string Code10;
	}
}
