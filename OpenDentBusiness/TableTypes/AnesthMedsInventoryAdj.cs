using System;

namespace OpenDentBusiness{

		///<summary>One inventory adjustment record for one anesthetic medication at a given timestamp.</summary>
		public class AnesthMedsInventoryAdj{
		///<summary>Primary key.</summary>
		public int AdjustNum;
		///<summary>FK to anesthmedsinventory.AnestheticMedNum.</summary>
		public int AnestheticMedNum;
		public double QtyAdj;
		///<summary>FK to provider.ProvNum.</summary>
		public int UserNum;
		public string Notes;
		public DateTime TimeStamp;
		
		
		}





}







