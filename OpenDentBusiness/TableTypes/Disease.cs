using System;
using System.Collections;

namespace OpenDentBusiness {

	/// <summary>Each row is one disease that one patient has.  A disease is a medical condition or allergy.  Diseases are defined in the DiseaseDef table.</summary>
	public class Disease{//:IComparable{
		///<summary>Primary key.</summary>
		public int DiseaseNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;
		///<summary>FK to diseasedef.DiseaseDefNum.  The disease description is in that table.</summary>
		public int DiseaseDefNum;
		///<summary>Any note about this disease that is specific to this patient.</summary>
		public string PatNote;

		/*
		///<summary>IComparable.CompareTo implementation.  This is used to order disease lists.</summary>
		public int CompareTo(object obj) {
			if(!(obj is Disease)) {
				throw new ArgumentException("object is not a Disease");
			}
			Disease disease=(Disease)obj;
			return 0;
			//return DiseaseDefs.GetOrder(DiseaseDefNum).CompareTo(DiseaseDefs.GetOrder(disease.DiseaseDefNum));
		}*/

		///<summary></summary>
		public Disease Copy() {
			Disease d=new Disease();
			d.DiseaseNum=DiseaseNum;
			d.PatNum=PatNum;
			d.DiseaseDefNum=DiseaseDefNum;
			d.PatNote=PatNote;
			return d;
		}

		
		
		
		
	}

		



		
	

	

	


}










