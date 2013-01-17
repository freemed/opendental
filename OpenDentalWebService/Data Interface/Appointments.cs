using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Appointments{

		public static List<OpenDentBusiness.Appointment> RefreshASAP(long provNum,long siteNum,long clinicNum) {
			return OpenDentBusiness.Appointments.RefreshASAP(provNum,siteNum,clinicNum);
		}

	}
}