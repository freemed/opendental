using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>AutoCode condition.  Always attached to an AutoCodeItem, which is then, in turn, attached to an autocode.  There is usually only one or two conditions for a given AutoCodeItem.</summary>
	public class AutoCodeCond{//
		///<summary>Primary key.</summary>
		public long AutoCodeCondNum;
		///<summary>FK to autocodeitem.AutoCodeItemNum.</summary>
		public long AutoCodeItemNum;
		///<summary>Enum:AutoCondition </summary>
		public AutoCondition Cond;


	}



	

	


}









