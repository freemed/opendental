using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.TableTypes {
	///<summary>There is no field for MessageStructureHL7 (ADT_A01), because that will be inferred. Defined in HL7 specs, section 2.16.3.</summary>
	[Serializable]
	public class HL7DefMessage:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefMessageNum;
		///<summary>FK to HL7Def.HL7DefNum</summary>
		public long HL7DefNum;
		///<summary>Example: ADT</summary>
		public MessageTypeHL7 MessageType;
		///<summary>Example: A04, which is only used with ADT/ACK.</summary>
		public EventTypeHL7 EventType;
		///<summary></summary>
		public InOutHL7 InOut;
		///<summary>This is mostly for outgoing, since incoming can have extra unexpected segments and since we don't usually care about order of incoming.</summary>
		public int ItemOrder;
		///<summary>text</summary>
		public string Note;

		///<summary></summary>
		public HL7DefMessage Clone() {
			return (HL7DefMessage)this.MemberwiseClone();
		}

	}

	public enum MessageTypeHL7 {
		//move over from HL7 namespace
	}

	public enum EventTypeHL7 {
		///<summary>0 - Only used with ADT/ACK.</summary>
		A04
	}

	public enum InOutHL7 {
		///<summary>0</summary>
		Incoming,
		///<summary>1</summary>
		Outgoing
	}

}
