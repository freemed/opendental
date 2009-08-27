using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>These are the actual elements attached to each signal that is sent.  They contain references to the sounds and lights that should result.</summary>
	public class SigElement{//:IComparable{
		///<summary>Primary key.</summary>
		public long SigElementNum;
		///<summary>FK to sigelementdef.SigElementDefNum</summary>
		public long SigElementDefNum;
		///<summary>FK to signal.SignalNum.</summary>
		public long SignalNum;

		/*
		///<summary>IComparable.CompareTo implementation.  This is used to order sigelements by type so that the sounds are fired in the correct sequence.</summary>
		public long CompareTo(object obj) {
			return 0;
			if(!(obj is SigElement)) {
				throw new ArgumentException("object is not a SigElement");
			}
			SigElement element=(SigElement)obj;
			long type1=(long)(SigElementDefs.GetElement(SigElementDefNum).SigElementType);//0,1,or 2
			long type2=(long)(SigElementDefs.GetElement(element.SigElementDefNum).SigElementType);
			return type1.CompareTo(type2);
		}*/
		
		///<summary></summary>
		public SigElement Copy(){
			SigElement s=new SigElement();
			s.SigElementNum=SigElementNum;
			s.SigElementDefNum=SigElementDefNum;
			s.SignalNum=SignalNum;
			return s;
		}

		

	
	}

	

	


}




















