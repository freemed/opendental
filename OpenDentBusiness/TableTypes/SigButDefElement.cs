using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>This table stores references to the sequence of sounds and lights that should get sent out with a button push.</summary>
	public class SigButDefElement{//:IComparable{
		///<summary>Primary key.</summary>
		public long ElementNum;
		///<summary>FK to sigbutdef.SigButDefNum  A few elements are usually attached to a single button.</summary>
		public long SigButDefNum;
		///<summary>FK to sigelementdef.SigElementDefNum, which contains the actual sound or light.</summary>
		public long SigElementDefNum;

		/*///<summary>IComparable.CompareTo implementation.  This is used to order SigButDefElements by type so that the sounds are fired in the correct sequence.</summary>
		public long CompareTo(object obj) {
			return 0;
			if(!(obj is SigButDefElement)) {
				throw new ArgumentException("object is not a SigButDefElement");
			}
			SigButDefElement element=(SigButDefElement)obj;
			long type1=(long)(SigElementDefs.GetElement(SigElementDefNum).SigElementType);//0,1,or 2
			long type2=(long)(SigElementDefs.GetElement(element.SigElementDefNum).SigElementType);
			return type1.CompareTo(type2);
		}*/
		
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




















