using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>An allergy attached to a patient and linked to an AllergyDef.</summary>
	[Serializable]
	public class WikiListHeaderWidth:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long WikiListHeaderWidthNum;
		///<summary>Name of the list that this header belongs to.</summary>
		public string ListName;
		///<summary>Name of the column that this header belongs to.</summary>
		public string ColName;
		///<summary>Width in pixels of column.</summary>
		public int ColWidth;
		/////<summary>Text to be displayed, less restrictive than MySQL column names.</summary>
		//public string DisplayText;
		
	}
}