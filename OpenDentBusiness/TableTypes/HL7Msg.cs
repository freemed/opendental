using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>HL7 messages sent and received.</summary>
	[Serializable()]
	public class HL7Msg:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7MsgNum;
		///<summary>Enum:HL7MessageStatus Out/In are relative to Open Dental.  This is in contrast to the names of the folders, which are relative to the other program.  OutPending, OutSent, InReceived, InProcessed.</summary>
		public HL7MessageStatus HL7Status;
		///<summary>The actual HL7 message in its entirity.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string MsgText;
		///<summary>FK to appointment.AptNum.  Many of the messages contain "Visit ID" which is equivalent to our AptNum.</summary>
		public long AptNum;
		
		public HL7Msg Copy(){
			return (HL7Msg)this.MemberwiseClone();
		}	
	}
}


