using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class BenefitB {
		///<summary>This function is unit tested for accuracy because it has been a source of bugs in the past.</summary>
		public static DateTime ComputeRenewDate(DateTime asofDate,bool isCalendarYear,DateTime insStartDate){
			if(isCalendarYear) {
				return new DateTime(asofDate.Year,1,1);
			}
			//now, for benefit year not beginning on Jan 1.
			if(insStartDate.Year<1880) {//if no start date was entered.
				return new DateTime(asofDate.Year,1,1);
			}
			if(insStartDate.Month<=asofDate.Month && insStartDate.Day<=asofDate.Day) {//earlier this year, or today
				return new DateTime(asofDate.Year,insStartDate.Month,insStartDate.Day);
			}
			//late last year
			return new DateTime(asofDate.Year-1,insStartDate.Month,insStartDate.Day);
		}



	}
}
