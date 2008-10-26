using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>Anesthetic Medications delivered to a patient during an Anesthetic.</summary>
	[DataObject("anestheticmedsgiven")]
	public class AnestheticMedsGiven : DataObjectBase {

		

		[DataField("AnesthMedNum", PrimaryKey=true, AutoNumber=true)]
		private int anestheticMedNum;
		bool anestheticMedNumChanged;
		/// <summary>Primary key.</summary>
		public int AnesthMedNum {
			get { return anestheticMedNum; }
			set { anestheticMedNum = value; MarkDirty(); anestheticMedNumChanged = true; }
		}
		public bool AnesthMedNumChanged {
			get { return anestheticMedNumChanged; }
		}

		//F.K. table anestheticrecord
		private int AnestheticRecordNum;


		[DataField("AnesthMed")]
		private string anesthMed;
		bool anesthMedChanged;
		/// <summary>Name of an anesthetic medication</summary>
		public string AnesthMed {
			get { return anesthMed; }
			set { anesthMed = value; MarkDirty(); anesthMedChanged = true; }
		}
		public bool AnesthMedChanged {
			get { return anesthMedChanged; }
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
		public string DoseTimeStamp {
			get { return doseTimeStamp; }
			set { doseTimeStamp = value; MarkDirty(); doseTimeStampChanged = true; }
		}
		public bool DoseTimeStampChanged {
			get { return doseTimeStampChanged; }
		}
			
	}

	

}









