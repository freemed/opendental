using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary></summary>
	[Serializable()]
	public class SupplyNeeded : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SupplyNeededNum;
		/// <summary>.</summary>
		public string Description;
		/// <summary>.</summary>
		public DateTime DateAdded;

		

			
	}

	

}









