using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile.UI {
	///<summary></summary>
	public class ODGridRow{		
		private ODGridCellList cells;
		private object tag;
		
		///<summary>Creates a new ODGridRow.</summary>
		public ODGridRow(){
			cells=new ODGridCellList();
			tag=null;
		}

		///<summary></summary>
		public ODGridCellList	Cells{
			get{
				return cells;
			}
		}

		///<summary>Used to store any kind of object that is associated with the row.</summary>
		public object Tag{
			get{
				return tag;
			}
			set{
				tag=value;
			}
		}

	        

	}
}
