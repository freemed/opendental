using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.TableTypes
{
	public class AnestheticMeds{
		///<summary>Intake of anesthetic medications on a given date.</summary>
	
			///<summary>Primary key.</summary>
			public int AnestheticMedNum;

			public DateTime IntakeDate;
			///<summary</summary>>
			public string AnesthMedName;
			///<summary></summary>
			public string DoseVol;
			///<summary></summary>
			public int Qty;

			public string Supplier;

			public string InvoiceNum;
	}
}
