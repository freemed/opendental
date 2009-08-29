using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>One item on one supply order.  This table links supplies to orders as well as storing a small amount of additional info.</summary>
	[DataObject("supplyorderitem")]
	public class SupplyOrderItem : DataObjectBase {
		[DataField("SupplyOrderItemNum", PrimaryKey=true, AutoNumber=true)]
		private long supplyOrderItemNum;
		bool supplyOrderItemNumChanged;
		/// <summary>Primary key.</summary>
		public long SupplyOrderItemNum {
			get { return supplyOrderItemNum; }
			set { supplyOrderItemNum = value; MarkDirty(); supplyOrderItemNumChanged = true; }
		}
		public bool SupplyOrderItemNumChanged {
			get { return supplyOrderItemNumChanged; }
		}

		[DataField("SupplyOrderNum")]
		private long supplyOrderNum;
		bool supplyOrderNumChanged;
		/// <summary>FK to supplyorder.supplyOrderNum.</summary>
		public long SupplyOrderNum {
			get { return supplyOrderNum; }
			set { supplyOrderNum = value; MarkDirty(); supplyOrderNumChanged = true; }
		}
		public bool SupplyOrderNumChanged {
			get { return supplyOrderNumChanged; }
		}

		[DataField("SupplyNum")]
		private long supplyNum;
		bool supplyNumChanged;
		/// <summary>FK to supply.SupplyNum.</summary>
		public long SupplyNum {
			get { return supplyNum; }
			set { supplyNum = value; MarkDirty(); supplyNumChanged = true; }
		}
		public bool SupplyNumChanged {
			get { return supplyNumChanged; }
		}

		[DataField("Qty")]
		private int qty;
		bool qtyChanged;
		/// <summary>How many were ordered.</summary>
		public int Qty {
			get { return qty; }
			set { qty = value; MarkDirty(); qtyChanged = true; }
		}
		public bool QtyChanged {
			get { return qtyChanged; }
		}

		[DataField("Price")]
		private double price;
		bool priceChanged;
		/// <summary>Price per unit on this order.</summary>
		public double Price {
			get { return price; }
			set { price = value; MarkDirty(); priceChanged = true; }
		}
		public bool PriceChanged {
			get { return priceChanged; }
		}

			
	}

	

}









