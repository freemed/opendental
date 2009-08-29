using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Links one planned appointment to one patient.  Allows multiple planned appointments per patient.  Also see the PlannedIsDone field. A planned appointment is an appointment that will show in the Chart module and in the Planned appointment tracker. It will never show in the Appointments module. In other words, it is the suggested next appoinment rather than an appointment that has already been scheduled.</summary>
	[DataObject("plannedappt")]
	public class PlannedAppt : DataObjectBase{
		[DataField("PlannedApptNum",PrimaryKey=true,AutoNumber=true)]
		private long plannedApptNum;
		private bool plannedApptNumChanged;
		///<summary>Primary key.</summary>
		public long PlannedApptNum{
			get{return plannedApptNum;}
			set{if(plannedApptNum!=value){plannedApptNum=value;MarkDirty();plannedApptNumChanged=true;}}
		}
		public bool PlannedApptNumChanged{
			get{return plannedApptNumChanged;}
		}

		[DataField("PatNum")]
		private long patNum;
		private bool patNumChanged;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum{
			get{return patNum;}
			set{if(patNum!=value){patNum=value;MarkDirty();patNumChanged=true;}}
		}
		public bool PatNumChanged{
			get{return patNumChanged;}
		}

		[DataField("AptNum")]
		private long aptNum;
		private bool aptNumChanged;
		///<summary>FK to appointment.AptNum.</summary>
		public long AptNum{
			get{return aptNum;}
			set{if(aptNum!=value){aptNum=value;MarkDirty();aptNumChanged=true;}}
		}
		public bool AptNumChanged{
			get{return aptNumChanged;}
		}

		[DataField("ItemOrder")]
		private int itemOrder;
		private bool itemOrderChanged;
		///<summary>One-indexed order of item in group of planned appts.</summary>
		public int ItemOrder{
			get{return itemOrder;}
			set{if(itemOrder!=value){itemOrder=value;MarkDirty();itemOrderChanged=true;}}
		}
		public bool ItemOrderChanged{
			get{return itemOrderChanged;}
		}
		
		public PlannedAppt Copy(){
			return (PlannedAppt)Clone();
		}	
	}
}
