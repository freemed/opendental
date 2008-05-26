using System;
using System.Collections;


namespace OpenDentBusiness{

	/// <summary>A list of diseases that can be assigned to patients.  Cannot be deleted if in use by any patients.</summary>
	public class DiseaseDef{//:IComparable{
		///<summary>Primary key.</summary>
		public int DiseaseDefNum;
		///<summary>.</summary>
		public string DiseaseName;
		///<summary>The order that the diseases will show in various lists.</summary>
		public int ItemOrder;
		///<summary>If hidden, the disease will still show on any patient that it was previously attached to, but it will not be available for future patients.</summary>
		public bool IsHidden;

		/*
		///<summary>IComparable.CompareTo implementation.  This is used to order disease lists.</summary>
		public int CompareTo(object obj) {
			if(!(obj is DiseaseDef)) {
				throw new ArgumentException("object is not a DiseaseDef");
			}
			DiseaseDef diseaseDef=(DiseaseDef)obj;
			return 0;
			//need to fix:
			//return DiseaseDefs.GetOrder(DiseaseDefNum).CompareTo(DiseaseDefs.GetOrder(diseaseDef.DiseaseDefNum));
		}*/

		///<summary></summary>
		public DiseaseDef Copy() {
			DiseaseDef d=new DiseaseDef();
			d.DiseaseDefNum=DiseaseDefNum;
			d.DiseaseName=DiseaseName;
			d.ItemOrder=ItemOrder;
			d.IsHidden=IsHidden;
			return d;
		}

		
	}

		



		
	

	

	


}










