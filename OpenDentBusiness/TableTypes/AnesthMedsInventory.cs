using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	/// <summary>The names and inventory counts of all scheduled anesthetic medications stored on premises </summary>
  
	[DataObject("anesthmedsinventory")]
	public class AnesthMedsInventory : DataObjectBase{

		[DataField("AnestheticMedNum", PrimaryKey = true, AutoNumber = true)]
		private int anestheticMedNum ;
		bool anestheticMedNumChanged ;
		///<summary> Primary key</summary>
		public int AnestheticMedNum
		{
			get { return anestheticMedNum; }
			set { anestheticMedNum = value ; MarkDirty() ; anestheticMedNumChanged = true ; }
		}
		public bool AnestheticMedNumChanged
		{
			get { return anestheticMedNumChanged; }
		}

		[DataField("AnesthMedName")]
		private string anesthMedName ;
		bool anesthMedNameChanged ;
		///<summary> Name of an Anesthetic Medication</summary>
		public string AnesthMedName
		{
			get { return anesthMedName ; }
			set { anesthMedName = value ; MarkDirty(); anesthMedNameChanged = true ; }
		}
		public bool AnesthMedNameChanged
		{
			get { return anesthMedNameChanged; }
		}

		[DataField("AnesthHowSupplied")]
		private string anesthHowSupplied ;
		bool anesthHowSuppliedChanged ;
		///<summary> The quantity and dose of an individual Anesthetic Medication as packaged by the manufacturer </summary>
		public string AnesthHowSupplied
		{
			get { return anesthHowSupplied ; }
			set { anesthHowSupplied = value ; MarkDirty() ; anesthHowSuppliedChanged = true ; }
		}
		public bool AnesthHowSuppliedChanged
		{
			get { return anesthHowSuppliedChanged; }
		}

		[DataField("QtyOnHand")]
		private string qtyOnHand ;
		bool qtyOnHandChanged ;
		///<summary> The quantity, in inventory, of a particular Anesthetic Medication </summary>
		public string QtyOnHand
		{
			get { return qtyOnHand ; }
			set { qtyOnHand = value ; MarkDirty() ; qtyOnHandChanged = true ; }
		}
		public bool QtyOnHandChanged
		{
			get { return qtyOnHandChanged ; }
		}

		[DataField("DEASchedule")]
		private string dEASchedule;
		bool dEAScheduleChanged;
		///<summary> The DEASchedule, II-V, of a particular Anesthetic Medication </summary>
		public string DEASchedule
		{
			get { return dEASchedule; }
			set { dEASchedule = value; MarkDirty(); dEAScheduleChanged = true; }
		}
		public bool DEAScheduleChanged
		{
			get { return dEAScheduleChanged; }
		}

		public AnesthMedsInventory Copy()
		{
			return (AnesthMedsInventory)Clone();
		}	

	}

}
