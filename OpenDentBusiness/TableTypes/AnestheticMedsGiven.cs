using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>Anesthetic Medications delivered to a patient during an Anesthetic.</summary>
	[DataObject("anestheticmedsgiven")]
	public class AnestheticMedsGiven : DataObjectBase {

		
		[DataField("AnestheticMedNum", PrimaryKey=true, AutoNumber=true)]
		private int anestheticMedNum;
		bool anestheticMedNumChanged;
		/// <summary>Primary key.</summary>
		public int AnestheticMedNum {
			get { return anestheticMedNum; }
			set { anestheticMedNum = value; MarkDirty(); anestheticMedNumChanged = true; }
		}
		public bool AnestheticMedNumChanged {
			get { return anestheticMedNumChanged; }
		}

		//F.K. table anestheticrecord
		[DataField("AnestheticRecordNum")]
		private string anestheticRecordNum;
		bool anestheticRecordNumChanged;
		/// <summary>Name of an anesthetic medication</summary>
		public string AnestheticRecordNum
		{
			get { return anestheticRecordNum; }
			set { anestheticRecordNum = value; MarkDirty(); anestheticRecordNumChanged = true; }
		}
		public bool AnestheticRecordNumChanged
		{
			get { return anestheticRecordNumChanged; }
		}

		[DataField("AnesthMedName")]
		private string anesthMedName;
		bool anesthMedNameChanged;
		/// <summary>Name of an anesthetic medication</summary>
		public string AnesthMedName {
			get { return anesthMedName; }
			set { anesthMedName = value; MarkDirty(); anesthMedNameChanged = true; }
		}
		public bool AnesthMedChanged {
			get { return anesthMedNameChanged; }
		}

		[DataField("QtyGiven")]
		private string qtyGiven;
		bool qtyGivenChanged;
		/// <summary>Quantity of an Anesthetic Medication that has been delivered to a patient</summary>
		
		public string QtyGiven {
			get { return qtyGiven; }
			set { qtyGiven = value; MarkDirty(); qtyGivenChanged = true; }
		}
		public bool QtyGivenChanged {
			get { return qtyGivenChanged; }
		}

		[DataField("QtyWasted")]
		private string qtyWasted;
		bool qtyWastedChanged;
		/// <summary>Quantity wasted of an Anesthetic Medication that has been drawn up but not delivered to a patient</summary>
		public string QtyWasted {
			get { return qtyWasted; }
			set { qtyWasted = value; MarkDirty(); qtyWastedChanged = true; }
		}
		public bool QtyWastedChanged {
			get { return qtyWastedChanged; }
		}

		[DataField("DoseTimeStamp")]
		private string doseTimeStamp;
		bool doseTimeStampChanged;
		/// <summary>TimeStamp that a dose of Anesthetic Medication is delivered to a patient</summary>
		public string DoseTimeStamp
		{
			get { return doseTimeStamp; }
			set { doseTimeStamp = value; MarkDirty(); doseTimeStampChanged = true; }
		}
		public bool DoseTimeStampChanged
		{
			get { return doseTimeStampChanged; }
		}
			
		[DataField("QtyOnHandOld")]
		private string qtyOnHandOld;
		bool qtyOnHandOldChanged;
		/// <summary>Quantity of an Anesthetic Medication that has been delivered to a patient</summary>

		public string QtyOnHandOld
		{
			get { return qtyOnHandOld; }
			set { qtyOnHandOld = value; MarkDirty(); qtyOnHandOldChanged = true; }
		}
		public bool QtyOnHandOldChanged
		{
			get { return qtyOnHandOldChanged; }
		}

		[DataField("AnesthMedNum")]
		private string anesthMedNum;
		bool anesthMedNumChanged;
		/// <summary>A unique anesthetic med num from anesthmedsinventory</summary>

		public string AnesthMedNum
		{
			get { return anesthMedNum; }
			set { anesthMedNum = value; MarkDirty(); anesthMedNumChanged = true; }
		}
		public bool AnesthMedNumChanged
		{
			get { return anesthMedNumChanged; }
		}


	}

	

}









