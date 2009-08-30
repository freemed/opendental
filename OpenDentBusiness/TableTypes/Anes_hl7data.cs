/*
using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>A unique HL7 message from a networked vital sign monitor.</summary>
	[DataObject("anes_hl7data")]
	public class Anes_hl7data : DataObjectBase{

		[DataField("MessageID",PrimaryKey=true,AutoNumber=false)]
		private string messageID;
		private bool messageIDChanged;
		///<summary>Primary key.</summary>
		public string MessageID{
			get{return messageID;}
			set{if(messageID!=value){messageID=value;MarkDirty();messageIDChanged=true;}}
		}
		public bool MessageIDChanged{
			get{return messageIDChanged;}
		}

		[DataField("VendorName")]
		private string vendorName;
		private bool vendorNameChanged;
		///<summary>VS Monitor Vendor Name</summary>
		public string VendorName{
			get{return vendorName;}
			set{if(vendorName!=value){vendorName=value;MarkDirty();vendorNameChanged=true;}}
		}
		public bool VendorNameChanged{
			get{return vendorNameChanged;}
		}

		[DataField("VendorVersion")]
		private string vendorVersion;
		private bool vendorVersionChanged;
		///<summary>The HL7 version used by the VS monitor</summary>
		public string VendorVersion{
			get{return vendorVersion;}
			set{if(vendorVersion!=value){vendorVersion=value;MarkDirty();vendorVersionChanged=true;}}
		}
		public bool VendorVersionChanged{
			get{return vendorVersionChanged;}
		}

		[DataField("MsgControl")]
		private string msgControl;
		private bool msgControlChanged;
		///<summary>Comprised of VS Monitor serial number and timestamp.</summary>
		public string MsgControl{
			get{return msgControl;}
			set{if(msgControl!=value){msgControl=value;MarkDirty();msgControlChanged=true;}}
		}
		public bool MsgControlChanged{
			get{return msgControlChanged;}
		}

		[DataField("PartnerAPP")]
		private string partnerAPP;
		private bool partnerAPPChanged;
		///<summary>ORUR01.</summary>
		public string PartnerAPP{
			get{return partnerAPP;}
			set{if(partnerAPP!=value){partnerAPP=value;MarkDirty();partnerAPPChanged=true;}}
		}
		public bool PartnerAPPChanged{
			get{return partnerAPPChanged;}
		}

		[DataField("DateLoaded")]
		private DateTime dateLoaded;
		private bool dateLoadedChanged;
		///<summary></summary>
		public DateTime DateLoaded{
			get{return dateLoaded;}
			set{if(dateLoaded!=value){dateLoaded=value;MarkDirty();dateLoadedChanged=true;}}
		}
		public bool DateLoadedChanged{
			get{return dateLoadedChanged;}
		}

		[DataField("LastLoaded")]
		private DateTime lastLoaded;
		private bool lastLoadedChanged;
		///<summary></summary>
		public DateTime LastLoaded{
			get{return lastLoaded;}
			set{if(lastLoaded!=value){lastLoaded=value;MarkDirty();lastLoadedChanged=true;}}
		}
		public bool LastLoadedChanged{
			get{return lastLoadedChanged;}
		}

		[DataField("LoadCount")]
		private long loadCount;
		private bool loadCountChanged;
		///<summary></summary>
		public long LoadCount{
			get{return loadCount;}
			set{if(loadCount!=value){loadCount=value;MarkDirty();loadCountChanged=true;}}
		}
		public bool LoadCountChanged{
			get{return loadCountChanged;}
		}

		[DataField("MsgType")]
		private string msgType;
		private bool msgTypeChanged;
		///<summary></summary>
		public string MsgType{
			get{return msgType;}
			set{if(msgType!=value){msgType=value;MarkDirty();msgTypeChanged=true;}}
		}
		public bool MsgTypeChanged{
			get{return msgTypeChanged;}
		}

		[DataField("MsgEvent")]
		private string msgEvent;
		private bool msgEventChanged;
		///<summary></summary>
		public string MsgEvent{
			get{return msgEvent;}
			set{if(msgEvent!=value){msgEvent=value;MarkDirty();msgEventChanged=true;}}
		}
		public bool MsgEventChanged{
			get{return msgEventChanged;}
		}

		[DataField("Outbound")]
		private long outbound;
		private bool outboundChanged;
		///<summary>Two char, uppercase.</summary>
		public long Outbound{
			get{return outbound;}
			set{if(outbound!=value){outbound=value;MarkDirty();outboundChanged=true;}}
		}
		public bool OutboundChanged{
			get{return outboundChanged;}
		}

		[DataField("Inbound")]
		private long inbound;
		private bool inboundChanged;
		///<summary></summary>
		public long Inbound{
			get{return inbound;}
			set{if(inbound!=value){inbound=value;MarkDirty();inboundChanged=true;}}
		}
		public bool InboundChanged{
			get{return inboundChanged;}
		}

		[DataField("Processed")]
		private long processed;
		private bool processedChanged;
		///<summary></summary>
		public long Processed{
			get{return processed;}
			set{if(processed!=value){processed=value;MarkDirty();processedChanged=true;}}
		}
		public bool ProcessedChanged{
			get{return processedChanged;}
		}

		[DataField("Warnings")]
		private long warnings;
		private bool warningsChanged;
		///<summary></summary>
		public long Warnings{
			get{return warnings;}
			set{if(warnings!=value){warnings=value;MarkDirty();warningsChanged=true;}}
		}
		public bool WarningsChanged{
			get{return warningsChanged;}
		}

		[DataField("Loaded")]
		private long loaded;
		private bool loadedChanged;
		///<summary></summary>
		public long Loaded{
			get{return loaded;}
			set{if(loaded!=value){loaded=value;MarkDirty();loadedChanged=true;}}
		}
		public bool LoadedChanged{
			get{return loadedChanged;}
		}

		[DataField("SchemaLoaded")]
		private long schemaLoaded;
		private bool schemaLoadedChanged;
		///<summary></summary>
		public long SchemaLoaded{
			get{return schemaLoaded;}
			set{if(schemaLoaded!=value){schemaLoaded=value;MarkDirty();schemaLoadedChanged=true;}}
		}
		public bool SchemaLoadedChanged{
			get{return schemaLoadedChanged;}
		}

		[DataField("StatusMessage")]
		private string statusMessage;
		private bool statusMessageChanged;
		///<summary>A freeform note for any info that is needed about the pharmacy, such as hours.</summary>
		public string StatusMessage{
			get{return statusMessage;}
			set{if(statusMessage!=value){statusMessage=value;MarkDirty();statusMessageChanged=true;}}
		}
		public bool StatusMessageChanged{
			get{return statusMessageChanged;}
		}
		
		[DataField("ArchiveID")]
		private string archiveID;
		private bool archiveIDChanged;
		///<summary>A freeform note for any info that is needed about the pharmacy, such as hours.</summary>
		public string ArchiveID{
			get{return archiveID;}
			set{if(archiveID!=value){archiveID=value;MarkDirty();archiveIDChanged=true;}}
		}
		public bool ArchiveIDChanged{
			get{return archiveIDChanged;}
		}

		[DataField("HL7Format")]
		private long hL7Format;
		private bool hL7FormatChanged;
		///<summary></summary>
		public long HL7Format{
			get{return hL7Format;}
			set{if(hL7Format!=value){hL7Format=value;MarkDirty();hL7FormatChanged=true;}}
		}
		public bool HL7FormatChanged{
			get{return hL7FormatChanged;}
		}

		[DataField("SegmentCount")]
		private long segmentCount;
		private bool segmentCountChanged;
		///<summary></summary>
		public long SegmentCount{
			get{return segmentCount;}
			set{if(segmentCount!=value){segmentCount=value;MarkDirty();segmentCountChanged=true;}}
		}
		public bool SegmentCountChanged{
			get{return segmentCountChanged;}
		}

		[DataField("MessageSize")]
		private long messageSize;
		private bool messageSizeChanged;
		///<summary></summary>
		public long MessageSize{
			get{return messageSize;}
			set{if(messageSize!=value){messageSize=value;MarkDirty();messageSizeChanged=true;}}
		}
		public bool MessageSizeChanged{
			get{return messageSizeChanged;}
		}

		[DataField("HL7Message")]
		private string hL7Message;
		private bool hL7MessageChanged;
		///<summary>The entire HL7 message string.</summary>
		public string HL7Message{
			get{return hL7Message;}
			set{if(hL7Message!=value){hL7Message=value;MarkDirty();hL7MessageChanged=true;}}
		}
		public bool HL7MessageChanged{
			get{return hL7MessageChanged;}
		}

		public Anes_hl7data Copy(){
			return (Anes_hl7data)Clone();
		}	
	}
}

*/

