using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentalWebService{
	///<summary></summary>
	public class Appointments{

		public static List<OpenDentBusiness.Appointment> RefreshASAP(long provNum,long siteNum,long clinicNum) {
			return OpenDentBusiness.Appointments.RefreshASAP(provNum,siteNum,clinicNum);
		}

		///<summary>Returns an image of the appointment schedule for the day passed in.</summary>
		public static string GetScheduleAsImage(DateTime date) {
			//Using a test image for now.  This will change to call a method that will return an image of the schedule.
			string command= "SELECT ButtonImage FROM procbutton WHERE ProcButtonNum=2";
			DataTable table=OpenDentBusiness.DataCore.GetTable(command);
			return table.Rows[0]["ButtonImage"].ToString();
		}

	}
}