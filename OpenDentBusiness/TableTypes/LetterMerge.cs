using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenDentBusiness{

	///<summary>Describes the templates for letter merges to Word.</summary>
	[Serializable]
	public class LetterMerge:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long LetterMergeNum;
		///<summary>Description of this letter.</summary>
		public string Description;
		///<summary>The filename of the Word template. eg MyTemplate.doc.</summary>
		public string TemplateName;
		///<summary>The name of the data file. eg MyTemplate.txt.</summary>
		public string DataFileName;
		///<summary>FK to definition.DefNum.</summary>
		public long Category;
		///<summary>Not a database column.  Filled using fk from the lettermergefields table.  A collection of strings representing field names.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<string> Fields;

		

		


	}

	



}









