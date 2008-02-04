using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	public class AccountModule{

		///<summary>Gets the Account table and the Commlog table.</summary>
		public static DataSet GetAll(int patNum,bool viewingInRecall,DateTime fromDate,DateTime toDate,bool intermingled)
		{
			return General.GetDS("AccountModule.GetAll",patNum.ToString(),POut.PBool(viewingInRecall),fromDate.ToShortDateString(),toDate.ToShortDateString(),POut.PBool(intermingled));
		}

		///<summary>Gets the Account table(s) for a statement.</summary>
		public static DataSet GetStatement(int patNum,bool singlePatient,DateTime fromDate,DateTime toDate,bool intermingled)
		{
			return General.GetDS("AccountModule.GetAll",patNum.ToString(),POut.PBool(singlePatient),fromDate.ToShortDateString(),toDate.ToShortDateString(),POut.PBool(intermingled));
		}

	}
}
