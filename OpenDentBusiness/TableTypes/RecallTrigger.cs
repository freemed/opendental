using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Links one procedurecode to one recalltype.  The presence of this trigger is used when determining DatePrevious in the recall table.</summary>
	[DataObject("recalltrigger")]
	public class RecallTrigger : DataObjectBase{
		[DataField("RecallTriggerNum",PrimaryKey=true,AutoNumber=true)]
		private int recallTriggerNum;
		private bool recallTriggerNumChanged;
		///<summary>Primary key.</summary>
		public int RecallTriggerNum{
			get{return recallTriggerNum;}
			set{if(recallTriggerNum!=value){recallTriggerNum=value;MarkDirty();recallTriggerNumChanged=true;}}
		}
		public bool RecallTriggerNumChanged{
			get{return recallTriggerNumChanged;}
		}

		[DataField("RecallTypeNum")]
		private int recallTypeNum;
		private bool recallTypeNumChanged;
		///<summary>FK to recalltype.RecallTypeNum</summary>
		public int RecallTypeNum{
			get{return recallTypeNum;}
			set{if(recallTypeNum!=value){recallTypeNum=value;MarkDirty();recallTypeNumChanged=true;}}
		}
		public bool RecallTypeNumChanged{
			get{return recallTypeNumChanged;}
		}

		[DataField("CodeNum")]
		private int codeNum;
		private bool codeNumChanged;
		///<summary>FK to procedurecode.CodeNum</summary>
		public int CodeNum{
			get{return codeNum;}
			set{if(codeNum!=value){codeNum=value;MarkDirty();codeNumChanged=true;}}
		}
		public bool CodeNumChanged{
			get{return codeNumChanged;}
		}
		
		public RecallTrigger Copy(){
			return (RecallTrigger)Clone();
		}	
	}
}

