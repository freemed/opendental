using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>A dental supply or office supply item.</summary>
	[DataObject("supply")]
	public class Supply : DataObjectBase {
		[DataField("SupplyNum", PrimaryKey=true, AutoNumber=true)]
		private int supplyNum;
		bool supplyNumChanged;
		/// <summary>Primary key.</summary>
		public int SupplyNum {
			get { return supplyNum; }
			set { supplyNum = value; MarkDirty(); supplyNumChanged = true; }
		}
		public bool SupplyNumChanged {
			get { return supplyNumChanged; }
		}

		[DataField("SupplierNum")]
		private int supplierNum;
		bool supplierNumChanged;
		/// <summary>FK to supplier.SupplierNum</summary>
		public int SupplierNum {
			get { return supplierNum; }
			set { supplierNum = value; MarkDirty(); supplierNumChanged = true; }
		}
		public bool SupplierNumChanged {
			get { return supplierNumChanged; }
		}

		[DataField("CatalogNumber")]
		private string catalogNumber;
		bool catalogNumberChanged;
		/// <summary>The catalog item number that the supplier uses to identify the supply.</summary>
		public string CatalogNumber {
			get { return catalogNumber; }
			set { catalogNumber = value; MarkDirty(); catalogNumberChanged = true; }
		}
		public bool CatalogNumberChanged {
			get { return catalogNumberChanged; }
		}

		[DataField("Descript")]
		private string descript;
		bool descriptChanged;
		/// <summary>The description can be similar to the catalog, but not required.  Typically includes qty per box/case, etc.</summary>
		public string Descript {
			get { return descript; }
			set { descript = value; MarkDirty(); descriptChanged = true; }
		}
		public bool DescriptChanged {
			get { return descriptChanged; }
		}

		[DataField("Category")]
		private int category;
		bool categoryChanged;
		/// <summary>FK to definition.DefNum.  User can define their own categories for supplies.</summary>
		public int Category {
			get { return category; }
			set { category = value; MarkDirty(); categoryChanged = true; }
		}
		public bool CategoryChanged {
			get { return categoryChanged; }
		}

		[DataField("ItemOrder")]
		private int itemOrder;
		bool itemOrderChanged;
		///<summary>The zero-based order of this supply within it's category.</summary>
		public int ItemOrder {
			get { return itemOrder; }
			set { itemOrder = value; MarkDirty(); itemOrderChanged = true; }
		}
		public bool ItemOrderChanged {
			get { return itemOrderChanged; }
		}

		[DataField("LevelDesired")]
		private float levelDesired;
		bool levelDesiredChanged;
		/// <summary>The level that a fresh order should bring item back up to.  Can include fractions.  If this is 0, then it will be displayed as having this field blank rather than showing 0.  This simply gives a cleaner look.</summary>
		public float LevelDesired {
			get { return levelDesired; }
			set { levelDesired = value; MarkDirty(); levelDesiredChanged = true; }
		}
		public bool LevelDesiredChanged {
			get { return levelDesiredChanged; }
		}

		[DataField("IsHidden")]
		private bool isHidden;
		bool isHiddenChanged;
		/// <summary>If hidden, then this supply item won't normally show in the main list.</summary>
		public bool IsHidden {
			get { return isHidden; }
			set { isHidden = value; MarkDirty(); isHiddenChanged = true; }
		}
		public bool IsHiddenChanged {
			get { return isHiddenChanged; }
		}

		[DataField("Price")]
		private double price;
		bool priceChanged;
		/// <summary>The price per unit that the supplier charges for this supply.  If this is 0.00, then no price will be displayed.</summary>
		public double Price {
			get { return price; }
			set { price = value; MarkDirty(); priceChanged = true; }
		}
		public bool PriceChanged {
			get { return priceChanged; }
		}

		

			
	}

	

}









