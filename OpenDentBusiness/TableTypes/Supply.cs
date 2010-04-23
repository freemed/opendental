using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>A dental supply or office supply item.</summary>
	[Serializable()]
	public class Supply : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SupplyNum;
		/// <summary>FK to supplier.SupplierNum</summary>
		public long SupplierNum;
		/// <summary>The catalog item number that the supplier uses to identify the supply.</summary>
		public string CatalogNumber;
		/// <summary>The description can be similar to the catalog, but not required.  Typically includes qty per box/case, etc.</summary>
		public string Descript;
		/// <summary>FK to definition.DefNum.  User can define their own categories for supplies.</summary>
		public long Category;
		///<summary>The zero-based order of this supply within it's category.</summary>
		public int ItemOrder;
		/// <summary>The level that a fresh order should bring item back up to.  Can include fractions.  If this is 0, then it will be displayed as having this field blank rather than showing 0.  This simply gives a cleaner look.</summary>
		public float LevelDesired;
		/// <summary>If hidden, then this supply item won't normally show in the main list.</summary>
		public bool IsHidden;
		/// <summary>The price per unit that the supplier charges for this supply.  If this is 0.00, then no price will be displayed.</summary>
		public double Price;
		

			
	}

	

}









