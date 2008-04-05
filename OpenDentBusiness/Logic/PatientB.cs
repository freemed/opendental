using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness{
	public class PatientB {
		///<summary>Returns a formatted name, Last, First.</summary>
		public static string GetNameLF(string LName,string FName, string Preferred,string MiddleI) {
			if(LName==""){
				return "";
			}
			if(Preferred=="")
				return LName+", "+FName+" "+MiddleI;
			else
				return LName+", '"+Preferred+"' "+FName+" "+MiddleI;
		}

		///<summary>Converts a date to an age.  Blank if over 115.  Only used where it's important to show the month, too.</summary>
		public static string DateToAgeString(DateTime date) {
			if(date.Year<1880)
				return "";
			int years=0;
			int months=0;
			if(date.Month < DateTime.Now.Month) {//birthday was recently in a previous month
				years=DateTime.Now.Year-date.Year;
				if(date.Day < DateTime.Now.Day) {//birthday earlier in the month
					months=(DateTime.Now.Month-date.Month);
				}
				else if(date.Day==DateTime.Now.Day) {//birthday day of month same as today
					months=(DateTime.Now.Month-date.Month);
				}
				else {//day of month later than today
					months=(DateTime.Now.Month-date.Month)-1;
				}
			}
			else if(date.Month == DateTime.Now.Month) {//birthday this month
				if(date.Day < DateTime.Now.Day) {//birthday earlier in this month
					years=DateTime.Now.Year-date.Year;
					months=0;
				}
				else if(date.Month == DateTime.Now.Month && date.Day == DateTime.Now.Day) {//today
					years=DateTime.Now.Year-date.Year;
					months=0;
				}
				else {//later this month
					years=DateTime.Now.Year-date.Year-1;
					months=11;
				}
			}
			else {//Hasn't had birthday yet this year.  It will be in a future month.
				years=DateTime.Now.Year-date.Year-1;
				if(date.Day < DateTime.Now.Day) {//birthday earlier in the month
					months=12-(date.Month-DateTime.Now.Month);
				}
				else if(date.Day==DateTime.Now.Day) {//birthday day of month same as today
					months=12-(date.Month-DateTime.Now.Month);
				}
				else {//day of month later than today
					months=12-(date.Month-DateTime.Now.Month)-1;
				}
			}
			if(years<18) {
				return years.ToString()+"y "+months.ToString()+"m";
			}
			else {
				return years.ToString();
			}
			//return AgeToString(DateToAge(date));
		}

	}
}
