using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile {
	public class Appointment {
		public int AptNum;
		public int PatNum;
		public ApptStatus AptStatus;
		public string Pattern;
		public int Confirmed;
		public int Op;
		public string Note;
		public int ProvNum;
		public int ProvHyg;
		///<summary>In OD, any date less than 1880 is always considered minimum val.  We will use 1850-01-01 as minimum.  We can't use 0001-01-01 because min allowed date is 1753.</summary>
		public DateTime AptDateTime;
		public string ProcDescript;
		public bool IsHygiene;
		
			

		


	}
}
