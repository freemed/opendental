using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>HL7 messages sent and received.</summary>
	[DataObject("hl7msg")]
	public class HL7Msg : DataObjectBase{
		[DataField("HL7MsgNum",PrimaryKey=true,AutoNumber=true)]
		private int hL7MsgNum;
		private bool hL7MsgNumChanged;
		///<summary>Primary key.</summary>
		public int HL7MsgNum{
			get{return hL7MsgNum;}
			set{if(hL7MsgNum!=value){hL7MsgNum=value;MarkDirty();hL7MsgNumChanged=true;}}
		}
		public bool HL7MsgNumChanged{
			get{return hL7MsgNumChanged;}
		}

		[DataField("HL7Status")]
		private HL7MessageStatus hL7Status;
		private bool hL7StatusChanged;
		///<summary>Enum:HL7MessageStatus  OutPending, OutSent, InReceived, InProcessed.</summary>
		public HL7MessageStatus HL7Status{
			get{return hL7Status;}
			set{if(hL7Status!=value){hL7Status=value;MarkDirty();hL7StatusChanged=true;}}
		}
		public bool HL7StatusChanged{
			get{return hL7StatusChanged;}
		}

		[DataField("MsgText")]
		private string msgText;
		private bool msgTextChanged;
		///<summary>The actual HL7 message in its entirity.</summary>
		public string MsgText{
			get{return msgText;}
			set{if(msgText!=value){msgText=value;MarkDirty();msgTextChanged=true;}}
		}
		public bool MsgTextChanged{
			get{return msgTextChanged;}
		}

		[DataField("AptNum")]
		private int aptNum;
		private bool aptNumChanged;
		///<summary>FK to appointment.AptNum.  Many of the messages contain "Visit ID" which is equivalent to the our AptNum.</summary>
		public int AptNum {
			get { return aptNum; }
			set { if(aptNum!=value) { aptNum=value; MarkDirty(); aptNumChanged=true; } }
		}
		public bool AptNumChanged {
			get { return aptNumChanged; }
		}
		
		public HL7Msg Copy(){
			return (HL7Msg)Clone();
		}	
	}
}


