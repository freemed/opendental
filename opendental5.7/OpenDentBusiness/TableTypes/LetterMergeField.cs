using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>When doing a lettermerge, a data file is created with certain fields.  This is a list of those fields for each lettermerge.</summary>
	public class LetterMergeField{
		///<summary>Primary key.</summary>
		public int FieldNum;
		///<summary>FK to lettermerge.LetterMergeNum.</summary>
		public int LetterMergeNum;
		///<summary>One of the preset available field names.</summary>
		public string FieldName;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			return cf;
		}*/

		


	}

	



}









