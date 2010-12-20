using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>This table stores references to the sequence of sounds and lights that should get sent out with a button push.</summary>
	[Serializable]
	public class SigButDefElement:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ElementNum;
		///<summary>FK to sigbutdef.SigButDefNum  A few elements are usually attached to a single button.</summary>
		public long SigButDefNum;
		///<summary>FK to sigelementdef.SigElementDefNum, which contains the actual sound or light.</summary>
		public long SigElementDefNum;
		
		///<summary></summary>
		public SigButDefElement Copy(){
			SigButDefElement s=new SigButDefElement();
			s.ElementNum=ElementNum;
			s.SigButDefNum=SigButDefNum;
			s.SigElementDefNum=SigElementDefNum;
			return s;
		}

	

	
	}

	

	


}




















