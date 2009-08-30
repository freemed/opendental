/*
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.TableTypes {
	///<summary>Intake of anesthetic medications on a given date. Increments the existing anesthetic medication inventory quantities</summary>
	public class AnestheticMedsIntake {


		///<summary>Primary key.</summary>
		public long AnestheticMedNum;
		//Date that anesthetic medications were received from a supplier to be added to existing inventory of scheduled medications in the practice.
		public DateTime IntakeDate;
		///<summary></summary>
		public string AnesthMedName;
		///<summary></summary>
		//Dosages are always recorded in mL so that inventory counts are correct
		public long Qty;
		//DEA schedule II-V
		public string DEASchedule;
		//Suppliers of anesthetic medications to the practice have a unique ID number
		public long SupplierIDNum;
		//Supplier's invoice numbers are recorded for audit and tracking purposes
		public string InvoiceNum;
	}
}
*/