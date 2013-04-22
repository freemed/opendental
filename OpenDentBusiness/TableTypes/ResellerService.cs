using System;

namespace OpenDentBusiness {

	///<summary>An entry in a list of services for a specific reseller to pick from.</summary>
	[Serializable]
	public class ResellerService:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ResellerServiceNum;
		///<summary>FK to reseller.ResellerNum.</summary>
		public long ResellerNum;
		///<summary>FK to procedurecode.CodeNum.</summary>
		public long CodeNum;
		///<summary>Amount of the service.  Might be a default, or might be the same for all customers of the reseller.</summary>
		public double Fee;

		///<summary>Returns a copy of this ResellerService.</summary>
		public ResellerService Copy() {
			return (ResellerService)this.MemberwiseClone();
		}


	}





}













