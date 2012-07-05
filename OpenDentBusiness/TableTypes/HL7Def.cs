using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>.</summary>
	[Serializable()]
	public class HL7Def:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefNum;
		///<summary></summary>
		public string Description;
		///<summary>Enum:ModeTxHL7 File, TcpIp</summary>
		public string ModeTx;
		///<summary>Only used for File mode</summary>
		public string IncomingFolder;
		///<summary>Only used for File mode</summary>
		public string OutgoingFolder;
		///<summary>Only used for tcpip mode. Example: 1461</summary>
		public string IncomingPort;
		///<summary>Only used for tcpip mode. Example: 192.168.0.23:1462</summary>
		public string OutgoingIpPort;
		///<summary>Only relevant for outgoing. Incoming field separators are defined in MSH. Default |.</summary>
		public string FieldSeparator;
		///<summary>Only relevant for outgoing. Incoming field separators are defined in MSH. Default ^.</summary>
		public string ComponentSeparator;
		///<summary>Only relevant for outgoing. Incoming field separators are defined in MSH. Default &.</summary>
		public string SubcomponentSeparator;
		///<summary>Only relevant for outgoing. Incoming field separators are defined in MSH. Default ~.</summary>
		public string RepetitionSeparator;
		///<summary>Only relevant for outgoing. Incoming field separators are defined in MSH. Default \.</summary>
		public string EscapeCharacter;
		///<summary>If this is set, then there will be no child tables. Internal types are fully defined within the C# code rather than in the database.</summary>
		public bool IsInternal;
		///<summary>This will always have a value because we always start with a copy of some internal type.</summary>
		public string InternalType;
		///<summary>Example: 12.2.14. This will be empty if IsInternal. This records the version at which they made their copy. We might have made significant improvements since their copy.</summary>
		public string InternalTypeVersion;
		///<summary>.</summary>
		public bool IsEnabled;
		///<summary></summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Note;

		///<summary></summary>
		public HL7Def Clone() {
			return (HL7Def)this.MemberwiseClone();
		}

	}

	
}