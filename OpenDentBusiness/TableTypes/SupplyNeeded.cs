using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary></summary>
	[DataObject("supplyneeded")]
	public class SupplyNeeded : DataObjectBase {
		[DataField("SupplyNeededNum", PrimaryKey=true, AutoNumber=true)]
		private int supplyNeededNum;
		bool supplyNeededNumChanged;
		/// <summary>Primary key.</summary>
		public int SupplyNeededNum {
			get { return supplyNeededNum; }
			set { supplyNeededNum = value; MarkDirty(); supplyNeededNumChanged = true; }
		}
		public bool SupplyNeededNumChanged {
			get { return supplyNeededNumChanged; }
		}

		[DataField("Description")]
		private string description;
		bool descriptionChanged;
		/// <summary>.</summary>
		public string Description {
			get { return description; }
			set { description = value; MarkDirty(); descriptionChanged = true; }
		}
		public bool DescriptionChanged {
			get { return descriptionChanged; }
		}

		[DataField("DateAdded")]
		private DateTime dateAdded;
		bool dateAddedChanged;
		/// <summary>.</summary>
		public DateTime DateAdded {
			get { return dateAdded; }
			set { dateAdded = value; MarkDirty(); dateAddedChanged = true; }
		}
		public bool DateAddedChanged {
			get { return dateAddedChanged; }
		}

		

			
	}

	

}









