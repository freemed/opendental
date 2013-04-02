using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Keeps track of column widths in Wiki Lists.</summary>
	[Serializable]
	public class WikiListHeaderWidth:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long WikiListHeaderWidthNum;
		///<summary>Name of the list that this header belongs to.  Tablename without the prefix.</summary>
		public string ListName;
		///<summary>Name of the column that this header belongs to.</summary>
		public string ColName;
		///<summary>Width in pixels of column.</summary>
		public int ColWidth;
		/////<summary>Text to be displayed, less restrictive than MySQL column names.  We may add this some day.</summary>
		//public string DisplayText;
		
	}
}