using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>There is one entry in this table for each fee for a single procedurecode.  So if there are 5 different fees stored for one procedurecode, then there will be five entries here.</summary>
	public class Fee{
		///<summary>Primary key.</summary>
		public int FeeNum;
		///<summary>The amount usually charged.  If an amount is unknown, then the entire Fee entry is deleted from the database.  The absence of a fee is sometimes shown in the user interface as a blank entry, and sometimes as 0.00.</summary>
		public double Amount;
		///<summary>FK to procedurelog.ADACode.</summary>
		public string ADACode;
		///<summary>FK to definition.DefNum.</summary>
		public int FeeSched;
		///<summary>Not used.</summary>
		public bool UseDefaultFee;
		///<summary>Not used.</summary>
		public bool UseDefaultCov;

		///<summary></summary>
		public Fee Copy(){
			Fee f=new Fee();
			f.FeeNum=FeeNum;
			f.Amount=Amount;
			f.ADACode=ADACode;
			f.FeeSched=FeeSched;
			return f;
		}

	}

	

}













