using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>These are copies of procedures that are attached to treatment plans.</summary>
	[Serializable]
	public class ProcTP:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ProcTPNum;
		///<summary>FK to treatplan.TreatPlanNum.  The treatment plan to which this proc is attached.</summary>
		public long TreatPlanNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to procedurelog.ProcNum.  It is very common for the referenced procedure to be missing.  This procNum is only here to compare and test the existence of the referenced procedure.  If present, it will check to see whether the procedure is still status TP.</summary>
		public long ProcNumOrig;
		///<summary>The order of this proc within its tp.  This is set when the tp is first created and can't be changed.  Drastically simplifies loading the tp.</summary>
		public int ItemOrder;
		///<summary>FK to definition.DefNum which contains the text of the priority.</summary>
		public long Priority;
		///<summary>A simple string displaying the tooth number.  If international tooth numbers are used, then this will be in international format already.</summary>
		public string ToothNumTP;
		///<summary>Tooth surfaces or area.  This is already converted for international use.  If arch or quad, then it will have U,LR, etc.</summary>
		public string Surf;
		///<summary>Not a foreign key.  Simply display text.  Can be changed by user at any time.</summary>
		public string ProcCode;
		///<summary>Description is originally copied from procedurecode.Descript, but user can change it.</summary>
		public string Descript;
		///<summary>The fee charged to the patient. Never gets automatically updated.</summary>
		public double FeeAmt;
		///<summary>The amount primary insurance is expected to pay. Never gets automatically updated.</summary>
		public double PriInsAmt;
		///<summary>The amount secondary insurance is expected to pay. Never gets automatically updated.</summary>
		public double SecInsAmt;
		///<summary>The amount the patient is expected to pay. Never gets automatically updated.</summary>
		public double PatAmt;
		///<summary>The amount of discount.  Currently only used for PPOs.</summary>
		public double Discount;
		///<summary>Text from prognosis definition.  Can be changed by user at any time.</summary>
		public string Prognosis;
		
		///<summary></summary>
		public ProcTP Copy(){
			return (ProcTP)MemberwiseClone();
		}

	
	}

	

	


}




















