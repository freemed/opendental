using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>HL7 messages sent and received.</summary>
	[Serializable()]
	public class HL7Msg:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7MsgNum;
		///<summary>Enum:HL7MessageStatus  OutPending, OutSent, InReceived, InProcessed.</summary>
		public HL7MessageStatus HL7Status;
		///<summary>The actual HL7 message in its entirity.</summary>
		public string MsgText;
		///<summary>FK to appointment.AptNum.  Many of the messages contain "Visit ID" which is equivalent to the our AptNum.</summary>
		public long AptNum;
		
		public HL7Msg Copy(){
			return (HL7Msg)this.MemberwiseClone();
		}	
	}
}


