using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>These are the actual elements attached to each signal that is sent.  They contain references to the sounds and lights that should result.</summary>
	[Serializable]
	public class SigElement:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SigElementNum;
		///<summary>FK to sigelementdef.SigElementDefNum</summary>
		public long SigElementDefNum;
		///<summary>FK to signalod.SignalNum.</summary>
		public long SignalNum;
		
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




















