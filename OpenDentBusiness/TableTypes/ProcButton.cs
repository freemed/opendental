using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	
	///<summary>The 'buttons' to show in the Chart module.  They must have items attached in order to do anything.</summary>
	public class ProcButton{
		///<summary>Primary key</summary>
		public int ProcButtonNum;
		///<summary>The text to show on the button.</summary>
		public string Description;
		///<summary>Order that they will show in the Chart module.</summary>
		public int ItemOrder;
		///<summary>FK to definition.DefNum.</summary>
		public int Category;
		///<summary>If no image, then the blob will be an empty string.  In this case, the bitmap will be null when loaded from the database.</summary>
		public Bitmap ButtonImage;

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










