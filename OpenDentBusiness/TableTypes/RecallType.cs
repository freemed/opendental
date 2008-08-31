using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>a procedure will just be linked to one recall type for now.  The link will remain as a field in the procedurecode table.  The interface will move to the recall setup window.  Later, we could allow attaching one procedure to multiple recall types, but it will require a linking table.</summary>
	[DataObject("recalltype")]
	public class RecallType : DataObjectBase{
		[DataField("RecallTypeNum",PrimaryKey=true,AutoNumber=true)]
		private int recallTypeNum;
		private bool recallTypeNumChanged;
		///<summary>Primary key.</summary>
		public int RecallTypeNum{
			get{return recallTypeNum;}
			set{if(recallTypeNum!=value){recallTypeNum=value;MarkDirty();recallTypeNumChanged=true;}}
		}
		public bool RecallTypeNumChanged{
			get{return recallTypeNumChanged;}
		}

		[DataField("Description")]
		private string description;
		private bool descriptionChanged;
		///<summary></summary>
		public string Description{
			get{return description;}
			set{if(description!=value){description=value;MarkDirty();descriptionChanged=true;}}
		}
		public bool DescriptionChanged{
			get{return descriptionChanged;}
		}

		[DataField("DefaultInterval")]
		private Interval defaultInterval;
		private bool defaultIntervalChanged;
		///<summary>The interval between recalls.  The Interval struct combines years, months, weeks, and days into a single integer value.</summary>
		public Interval DefaultInterval{
			get{return defaultInterval;}
			set{if(defaultInterval!=value){defaultInterval=value;MarkDirty();defaultIntervalChanged=true;}}
		}
		public bool DefaultIntervalChanged{
			get{return defaultIntervalChanged;}
		}

		[DataField("TimePattern")]
		private string timePattern;
		private bool timePatternChanged;
		///<summary>For scheduling the appointment.</summary>
		public string TimePattern{
			get{return timePattern;}
			set{if(timePattern!=value){timePattern=value;MarkDirty();timePatternChanged=true;}}
		}
		public bool TimePatternChanged{
			get{return timePatternChanged;}
		}

		[DataField("Procedures")]
		private string procedures;
		private bool proceduresChanged;
		///<summary>What procedures to put on the recall appointment.  Comma delimited.</summary>
		public string Procedures{
			get{return procedures;}
			set{if(procedures!=value){procedures=value;MarkDirty();proceduresChanged=true;}}
		}
		public bool ProceduresChanged{
			get{return proceduresChanged;}
		}
		
		public RecallType Copy(){
			return (RecallType)Clone();
		}	
	}
}

