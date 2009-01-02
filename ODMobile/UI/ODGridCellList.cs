using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile.UI {
	public class ODGridCellList:List<OpenDentMobile.UI.ODGridCell>{
		///<summary></summary>
		public void Add(string value) {
			this.Add(new ODGridCell(value));
		}
	}
}
