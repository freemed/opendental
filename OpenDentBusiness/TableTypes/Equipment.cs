using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Used for property tax tracking.</summary>
	[Serializable]
	public class Equipment:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EquipmentNum;
		///<summary>Short description, need not be very unique.</summary>
		public string Description;
		///<summary>Must be unique among all pieces of equipment.  Auto-generated 3 char alpha numeric gives 1.5M unique serial numbers.  Zero never part of autogenerated serial number.</summary>
		public string SerialNumber;
		///<summary>Limit 2 char.</summary>
		public string ModelYear;
		///<summary>Date when this corporation obtained the equipment.  Always has a valid value.</summary>
		public DateTime DatePurchased;
		///<summary>Normally 01-01-0001 if equipment still in possession.  Once sold, a date will be present.</summary>
		public DateTime DateSold;
		///<summary>.</summary>
		public double PurchaseCost;
		///<summary>.</summary>
		public double MarketValue;
		///<summary>Freeform text.</summary>
		public string Location;
		///<summary>Security uses this date to lock older entries from accidental deletion.  Date, no time.</summary>
		public DateTime DateEntry;
	}



	


}









