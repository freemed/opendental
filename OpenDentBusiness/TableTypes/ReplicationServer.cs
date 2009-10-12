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
		///<summary>The description or name of the server.  Optional.</summary>
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
		///<summary>Db admin sets this server_id server variable on each replication server.  Allows us to know what server each workstation is connected to.  In display, it's ordered by this value.  Users are always forced to enter a value here.</summary>
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
		///<summary>The start of the key range for this server.  0 if no value entered yet.</summary>
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
		///<summary>The end of the key range for this server.  0 if no value entered yet.</summary>
		public long RangeEnd{
			get{return rangeEnd;}
			set{if(rangeEnd!=value){rangeEnd=value;MarkDirty();rangeEndChanged=true;}}
		}
		public bool RangeEndChanged{
			get{return rangeEndChanged;}
		}

		[DataField("AtoZpath")]
		private string atoZpath;
		private bool atoZpathChanged;
		///<summary>The AtoZpath for this server. Optional.</summary>
		public string AtoZpath {
			get { return atoZpath; }
			set { if(atoZpath!=value) { atoZpath=value; MarkDirty(); atoZpathChanged=true; } }
		}
		public bool AtoZpathChanged {
			get { return atoZpathChanged; }
		}

		[DataField("UpdateBlocked")]
		private bool updateBlocked;
		private bool updateBlockedChanged;
		///<summary>If true, then this server cannot initiate an update.  Typical for satellite servers.</summary>
		public bool UpdateBlocked {
			get { return updateBlocked; }
			set { if(updateBlocked!=value) { updateBlocked=value; MarkDirty(); updateBlockedChanged=true; } }
		}
		public bool UpdateBlockedChanged {
			get { return updateBlockedChanged; }
		}

		public ReplicationServer Copy() {
			return (ReplicationServer)Clone();
		}	
	}
}