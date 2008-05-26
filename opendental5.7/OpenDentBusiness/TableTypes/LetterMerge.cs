using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Describes the templates for letter merges to Word.</summary>
	public class LetterMerge{
		///<summary>Primary key.</summary>
		public int LetterMergeNum;
		///<summary>Description of this letter.</summary>
		public string Description;
		///<summary>The filename of the Word template. eg MyTemplate.doc.</summary>
		public string TemplateName;
		///<summary>The name of the data file. eg MyTemplate.txt.</summary>
		public string DataFileName;
		///<summary>FK to definition.DefNum.</summary>
		public int Category;
		///<summary>Not a database column.  Filled using fk from the lettermergefields table.  The arrayList is a collection of strings representing field names.</summary>
		public ArrayList Fields;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			return cf;
		}*/

		


	}

	



}









