using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile.UI {
	public class ODGridClickEventArgs {
		private int col;
		private int row;
	
		///<summary></summary>
		public ODGridClickEventArgs(int col,int row){
			this.col=col;
			this.row=row;
		}

		///<summary></summary>
		public int Row{
			get{ 
				return row;
			}
		}

		///<summary></summary>
		public int Col{
			get{ 
				return col;
			}
		}
	}

	///<summary></summary>
	public delegate void ODGridClickEventHandler(object sender,ODGridClickEventArgs e);
}
