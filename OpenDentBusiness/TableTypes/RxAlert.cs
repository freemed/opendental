using System;
using System.Collections;

namespace OpenDentBusiness {

	/// <summary>Many-to-many relationship connecting Rx with DiseaseDef.</summary>
	public class RxAlert{
		///<summary>Primary key.</summary>
		public long RxAlertNum;
		///<summary>FK to rxdef.RxDefNum.</summary>
		public long RxDefNum;
		///<summary>FK to diseasedef.DiseaseDefNum</summary>
		public long DiseaseDefNum;

		///<summary></summary>
		public RxAlert Copy() {
			RxAlert r=new RxAlert();
			r.RxAlertNum=RxAlertNum;
			r.RxDefNum=RxDefNum;
			r.DiseaseDefNum=DiseaseDefNum;
			return r;
		}

		
		
	}

		



		
	

	

	


}










