using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>If replication is being used, then this stores information about each server.  Each row is one server.</summary>
	[DataObject("replicationserver")]
	public class ReplicationServer:DataObjectBase {
		[DataField("ReplicationServerNum",PrimaryKey=true,AutoNumber=true)]
		private long replicationServerNum;
		private bool replicationServerNumChanged;
		///<summary>Primary key.</summary>
		public long ReplicationServerNum{
			get{return replicationServerNum;}
			set{if(replicationServerNum!=value){replicationServerNum=value;MarkDirty();replicationServerNumChanged=true;}}
		}
		public bool ReplicationServerNumChanged{
			get{return replicationServerNumChanged;}
		}

		[DataField("Descript")]
		private string descript;
		private bool descriptChanged;
		///<summary>The description of the descript column.</summary>
		public string Descript{
			get{return descript;}
			set{if(descript!=value){descript=value;MarkDirty();descriptChanged=true;}}
		}
		public bool DescriptChanged{
			get{return descriptChanged;}
		}

		[DataField("ServerId")]
		private int serverId;
		private bool serverIdChanged;
		///<summary>Db admin sets this server_id server variable on each replication server.  Allows us to know what server each workstation is connected to.  In display, it's ordered by this value.</summary>
		public int ServerId{
			get{return serverId;}
			set{if(serverId!=value){serverId=value;MarkDirty();serverIdChanged=true;}}
		}
		public bool ServerIdChanged{
			get{return serverIdChanged;}
		}

		[DataField("RangeStart")]
		private long rangeStart;
		private bool rangeStartChanged;
		///<summary>The start of the key range for this server.</summary>
		public long RangeStart{
			get{return rangeStart;}
			set{if(rangeStart!=value){rangeStart=value;MarkDirty();rangeStartChanged=true;}}
		}
		public bool RangeStartChanged{
			get{return rangeStartChanged;}
		}

		[DataField("RangeEnd")]
		private long rangeEnd;
		private bool rangeEndChanged;
		///<summary>The end of the key range for this server.</summary>
		public long RangeEnd{
			get{return rangeEnd;}
			set{if(rangeEnd!=value){rangeEnd=value;MarkDirty();rangeEndChanged=true;}}
		}
		public bool RangeEndChanged{
			get{return rangeEndChanged;}
		}

		public ReplicationServer Copy() {
			return (ReplicationServer)Clone();
		}	
	}
}