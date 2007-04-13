using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used to view employee timecards.  Timecard entries are not linked to a pay period.  Instead, payperiods are setup, and the user can only view specific pay periods.  So it feels like they are linked, but it's date based.</summary>
	public class PayPeriod{
		///<summary>Primary key.</summary>
		public int PayPeriodNum;
		///<summary>The first day of the payperiod</summary>
		public DateTime DateStart;
		///<summary>The last day of the payperiod.</summary>
		public DateTime DateStop;
		///<summary>The date that paychecks will be dated.  A few days after the dateStop.  Optional.</summary>
		public DateTime DatePaycheck;

		///<summary></summary>
		public PayPeriod Copy() {
			PayPeriod p=new PayPeriod();
			p.PayPeriodNum=PayPeriodNum;
			p.DateStart=DateStart;
			p.DateStop=DateStop;
			p.DatePaycheck=DatePaycheck;
			return p;
		}


	}

	
}




