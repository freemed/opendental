using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>Links one planned appointment to one patient.  Allows multiple planned appointments per patient.</summary>
	[DataObject("plannedappt")]
	public class PlannedAppt : DataObjectBase{
		[DataField("PlannedApptNum",PrimaryKey=true,AutoNumber=true)]
		private int plannedApptNum;
		private bool plannedApptNumChanged;
		///<summary>Primary key.</summary>
		public int PlannedApptNum{
			get{return plannedApptNum;}
			set{if(plannedApptNum!=value){plannedApptNum=value;MarkDirty();plannedApptNumChanged=true;}}
		}
		public bool PlannedApptNumChanged{
			get{return plannedApptNumChanged;}
		}

		[DataField("PatNum")]
		private int patNum;
		private bool patNumChanged;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum{
			get{return patNum;}
			set{if(patNum!=value){patNum=value;MarkDirty();patNumChanged=true;}}
		}
		public bool PatNumChanged{
			get{return patNumChanged;}
		}

		[DataField("AptNum")]
		private int aptNum;
		private bool aptNumChanged;
		///<summary>FK to appointment.AptNum.</summary>
		public int AptNum{
			get{return aptNum;}
			set{if(aptNum!=value){aptNum=value;MarkDirty();aptNumChanged=true;}}
		}
		public bool AptNumChanged{
			get{return aptNumChanged;}
		}

		[DataField("ItemOrder")]
		private int itemOrder;
		private bool itemOrderChanged;
		///<summary>Zero indexed order of item in group of planned appts..</summary>
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
