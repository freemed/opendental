using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>One supply order to one supplier.  Contains SupplyOrderItems.</summary>
	[DataObject("supplyorder")]
	public class SupplyOrder : DataObjectBase {
		[DataField("SupplyOrderNum", PrimaryKey=true, AutoNumber=true)]
		private int supplyOrderNum;
		bool supplyOrderNumChanged;
		/// <summary>Primary key.</summary>
		public int SupplyOrderNum {
			get { return supplyOrderNum; }
			set { supplyOrderNum = value; MarkDirty(); supplyOrderNumChanged = true; }
		}
		public bool SupplyOrderNumChanged {
			get { return supplyOrderNumChanged; }
		}

		[DataField("SupplierNum")]
		private int supplierNum;
		bool supplierNumChanged;
		/// <summary>FK to supplier.SupplierNum.</summary>
		public int SupplierNum {
			get { return supplierNum; }
			set { supplierNum = value; MarkDirty(); supplierNumChanged = true; }
		}
		public bool SupplierNumChanged {
			get { return supplierNumChanged; }
		}

		[DataField("DatePlaced")]
		private DateTime datePlaced;
		bool datePlacedChanged;
		/// <summary>A date greater than 2200 (eg 2500), is considered a max date.  A max date is used for an order that was started but has not yet been placed.  This puts it at the end of the list where it belongs, but it will display as blank.  Only one unplaced order is allowed per supplier.</summary>
		public DateTime DatePlaced {
			get { return datePlaced; }
			set { datePlaced = value; MarkDirty(); datePlacedChanged = true; }
		}
		public bool DatePlacedChanged {
			get { return datePlacedChanged; }
		}

		[DataField("Note")]
		private string note;
		bool noteChanged;
		/// <summary>.</summary>
		public string Note {
			get { return note; }
			set { note = value; MarkDirty(); noteChanged = true; }
		}
		public bool NoteChanged {
			get { return noteChanged; }
		}

		[DataField("AmountTotal")]
		private double amountTotal;
		bool amountTotalChanged;
		/// <summary>The sum of all the amounts of each item on the order.  If any of the item prices are zero, then it won't auto calculate this total.  This will allow the user to manually put in the total without having it get deleted.</summary>
		public double AmountTotal {
			get { return amountTotal; }
			set { amountTotal = value; MarkDirty(); amountTotalChanged = true; }
		}
		public bool AmountTotalChanged {
			get { return amountTotalChanged; }
		}

		public SupplyOrder Copy(){
			return (SupplyOrder)Clone();
		}
		
		

			
	}

	

}









