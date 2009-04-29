using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public class RpAdjSheet {
		///<summary>Use provNums empty list to indicate all providers.</summary>
		public static DataTable GetTable(List<int> provNums,List<int> types,DateTime dateStart,DateTime dateEnd) {
			//this would be the best way to do it if we want to use security on the reports.
			//but it's not absolutely necessary.
			return null;
		}
	}
}
