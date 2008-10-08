using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.TableTypes
{
	public class AnestheticMedsIntake{
		///<summary>Intake of anesthetic medications on a given date. Increments the existing anesthetic medication inventory quantities</summary>
	
			///<summary>Primary key.</summary>
			public int AnestheticMedNum;
            //Date that anesthetic medications were received from a supplier to be added to existing inventory of scheduled medications in the practice.
			public DateTime IntakeDate;
			///<summary</summary>>
			public string AnesthMedName;
			///<summary></summary>
            //Dosages are always recorded in mL so that inventory counts are correct
			public int Qty;
            //DEA schedule II-V
            public string DEASchedule;
            //Suppliers of anesthetic medications to the practice have a unique ID number
			public int SupplierIDNum;
            //Supplier's invoice numbers are recorded for audit and tracking purposes
			public string InvoiceNum;
	}
}
