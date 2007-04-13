using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Each row is a single operatory or column in the appts module.</summary>
	public class Operatory{
		///<summary>Primary key</summary>
		public int OperatoryNum;
		///<summary>The full name to show in the column.</summary>
		public string OpName;
		///<summary>5 char or less. Not used much.</summary>
		public string Abbrev;
		///<summary>The order that this op column will show.  Changing views only hides some ops; it does not change their order.</summary>
		public int ItemOrder;
		///<summary>Used instead of deleting to hide an op that is no longer used.</summary>
		public bool IsHidden;
		///<summary>FK to provider.ProvNum.  The dentist assigned to this op.  If more than one dentist might be assigned to an op, then create a second op and use one for each dentist. If 0, then no dentist is assigned.</summary>
		public int ProvDentist;
		///<summary>FK to provider.ProvNum.  The hygienist assigned to this op.  If 0, then no hygienist is assigned.</summary>
		public int ProvHygienist;
		///<summary>Set true if this is a hygiene operatory.  The hygienist will then be considered the main provider for this op.</summary>
		public bool IsHygiene;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.</summary>
		public int ClinicNum;

		///<summary>Returns a copy of this Operatory.</summary>
		public Operatory Copy(){
			Operatory o=new Operatory();
			o.OperatoryNum=OperatoryNum;
			o.OpName=OpName;
			o.Abbrev=Abbrev;
			o.ItemOrder=ItemOrder;
			o.IsHidden=IsHidden;
			o.ProvDentist=ProvDentist;
			o.ProvHygienist=ProvHygienist;
			o.IsHygiene=IsHygiene;
			o.ClinicNum=ClinicNum;
			return o;
		}

	
	}
	


}













