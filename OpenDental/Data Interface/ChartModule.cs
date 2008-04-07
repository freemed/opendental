using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	public class ChartModule{
		///<summary>This is just the first version of this function.  It only gets selected parts of the Chart refresh.</summary>
		public static DataSet GetAll(int patNum, bool isAuditMode) {
			return Gen.GetDS(MethodName.Chart_GetAll,patNum,isAuditMode);
		}




	}
}
