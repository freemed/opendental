using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>Anesthetic Medications delivered to a patient during an Anesthetic.</summary>
	[DataObject("anesthmedsinventory")]
	public class AnesthMedsInventory : DataObjectBase {

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

		[DataField("AnesthMedName")]
		private string anesthMedName;
		bool anesthMedNameChanged;
		/// <summary>Name of an anesthetic medication</summary>
		public string AnesthMedName {
			get { return anesthMedName; }
            set { anesthMedName = value; MarkDirty(); anesthMedNameChanged = true; }
		}
		public bool AnesthMedNameChanged {
			get { return anesthMedNameChanged; }
		}

        [DataField("AnesthHowSupplied")]
        private string anesthHowSupplied;
        bool anesthHowSuppliedChanged;
        /// <summary>The quantity and dose of an individual supplied Anesthetic Medication</summary>
        public string AnesthHowSupplied
        {
            get { return anesthHowSupplied; }
            set { anesthHowSupplied = value; MarkDirty(); anesthHowSuppliedChanged = true; }
        }
        public bool AnesthHowSuppliedChanged {
            get { return anesthHowSuppliedChanged; }
        }

		[DataField("QtyOnHand")]
		private string qtyOnHand;
		bool qtyOnHandChanged;
		/// <summary>Quantity of an Anesthetic Medication that has been delivered to a patient</summary>
		
		public string QtyOnHand {
			get { return qtyOnHand; }
            set { qtyOnHand = value; MarkDirty(); qtyOnHandChanged = true; }
		}
		public bool QtyOnHandChanged {
			get { return qtyOnHandChanged; }
		}
      /*  public AnesthMedsInventory Copy()
        {
            return (AnesthMedsInventory)Clone();
        }*/	
	}

	

}









