using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>These are the actual elements attached to each signal that is sent.  They contain references to the sounds and lights that should result.</summary>
	public class SigElement{//:IComparable{
		///<summary>Primary key.</summary>
		public int SigElementNum;
		///<summary>FK to sigelementdef.SigElementDefNum</summary>
		public int SigElementDefNum;
		///<summary>FK to signal.SignalNum.</summary>
		public int SignalNum;

		/*
		///<summary>IComparable.CompareTo implementation.  This is used to order sigelements by type so that the sounds are fired in the correct sequence.</summary>
		public int CompareTo(object obj) {
			return 0;
			if(!(obj is SigElement)) {
				throw new ArgumentException("object is not a SigElement");
			}
			SigElement element=(SigElement)obj;
			int type1=(int)(SigElementDefs.GetElement(SigElementDefNum).SigElementType);//0,1,or 2
			int type2=(int)(SigElementDefs.GetElement(element.SigElementDefNum).SigElementType);
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




















