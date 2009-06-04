using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	public class X12Parse {
		public static DateTime ToDate(string element) {
			if(element.Length != 8) {
				return DateTime.MinValue;
			}
			int year=PIn.PInt(element.Substring(0,4));
			int month=PIn.PInt(element.Substring(4,2));
			int day=PIn.PInt(element.Substring(6,2));
			DateTime dt=new DateTime(year,month,day);
			return dt;
		}

	}
}
