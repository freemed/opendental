using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{

	///<summary>The 'buttons' to show in the Chart module.  They must have items attached in order to do anything.</summary>
	[Serializable]
	public class ProcButton:TableBase {
		///<summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long ProcButtonNum;
		///<summary>The text to show on the button.</summary>
		public string Description;
		///<summary>Order that they will show in the Chart module.</summary>
		public int ItemOrder;
		///<summary>FK to definition.DefNum.</summary>
		public long Category;
		///<summary>If no image, then the clob will be an empty string.  In this case, the bitmap will be null when loaded from the database.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string ButtonImage;

		///<summary></summary>
		public ProcButton Copy() {
			ProcButton p=new ProcButton();
			p.ProcButtonNum=ProcButtonNum;
			p.Description=Description;
			p.ItemOrder=ItemOrder;
			p.Category=Category;
			p.ButtonImage=ButtonImage;
			return p;
		}

		
	}

	

	


}










