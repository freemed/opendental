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

		[DataField("CatalogDescript")]
		private string catalogDescript;
		bool catalogDescriptChanged;
		/// <summary>The description as found in the catalog or online.  Typically includes qty per box/case, etc.</summary>
		public string CatalogDescript {
			get { return catalogDescript; }
			set { catalogDescript = value; MarkDirty(); catalogDescriptChanged = true; }
		}
		public bool CatalogDescriptChanged {
			get { return catalogDescriptChanged; }
		}

		[DataField("CommonName")]
		private string commonName;
		bool commonNameChanged;
		/// <summary>Optional alternate name that this office prefers to use.  Usually simpler.</summary>
		public string CommonName {
			get { return commonName; }
			set { commonName = value; MarkDirty(); commonNameChanged = true; }
		}
		public bool CommonNameChanged {
			get { return commonNameChanged; }
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

		[DataField("UnitType")]
		private string unitType;
		bool unitTypeChanged;
		/// <summary>Pk, box, etc.  The qty per unit will be in the description.  This is just a text field for now.</summary>
		public string UnitType {
			get { return unitType; }
			set { unitType = value; MarkDirty(); unitTypeChanged = true; }
		}
		public bool UnitTypeChanged {
			get { return unitTypeChanged; }
		}

		[DataField("Note")]
		private string note;
		bool noteChanged;
		/// <summary>Any note regarding this supply.</summary>
		public string Note {
			get { return note; }
			set { note = value; MarkDirty(); noteChanged = true; }
		}
		public bool NoteChanged {
			get { return noteChanged; }
		}
		
		

		

		

			
	}

	

}









