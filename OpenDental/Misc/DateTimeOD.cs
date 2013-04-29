using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDental {
	public class DateTimeOD {

		///<summary>We are switching to using this method instead of DateTime.Today.</summary> 
		public static DateTime Today {
			//The problem is with dotNet serilazation to the middle tier.  It will tack on zulu change for UTC.  Like this:
			//2013-04-29T04:00:00-7:00
			//DateTime objects created with DateTimeKind.Local in one timezone and sent over the middle tier to another time zone will be different at their 
			//destination, because .NET will automatically try to adjust for the timezone change.
			//DateTime.Today uses DateTimeKind.Local and we want DateTimeKind.Unspecified.
			//DateTime DateTimeToday=DateTime.Today;//DateTimeKind.Local, so serialization seems attempt to convert it to z.
			//DateTime DateTimeSpecific=new DateTime(2013,4,29);//DateTimeKind.Unspecified.
			//DateTime DateTimeParsed=DateTime.Parse("4/29/2013");//DateTimeKind.Unspecified.
			get {
				return new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,0,0,0,DateTimeKind.Unspecified);//Today at midnight with no timezone information.
			}
		}

	}
}
