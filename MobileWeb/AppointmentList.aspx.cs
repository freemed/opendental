using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace MobileWeb {
	public partial class AppointmentList:System.Web.UI.Page {
		private long CustomerNum=0;
		public int PreviousDateDay=0;
		public int PreviousDateMonth=0;
		public int PreviousDateYear=0;
		public int NextDateDay=0;
		public int NextDateMonth=0;
		public int NextDateYear=0;

		protected void Page_Load(object sender,EventArgs e) {
			Message.Text="";
			if(Session["CustomerNum"]!=null) {
				Message.Text="LoggedIn";
				int Year=0;
				int Month=0;
				int Day=0;
				DateTime AppointmentDate;
				try {
					if(Request["year"]!=null && Request["month"]!=null && Request["day"]!=null) {
						Int32.TryParse(Request["year"].ToString().Trim(),out Year);
						Int32.TryParse(Request["month"].ToString().Trim(),out Month);
						Int32.TryParse(Request["day"].ToString().Trim(),out Day);
						AppointmentDate= new DateTime(Year,Month,Day);
					}
					else {
						AppointmentDate= DateTime.Today;
					}
				}
				catch(Exception ex) {
					AppointmentDate= DateTime.Today;
				}
				DayLabel.Text=AppointmentDate.ToString("ddd") + ", " + AppointmentDate.ToString("MMM") + " " + AppointmentDate.ToString("dd");
				String appsuffix=DayLabel.Text;
				DateTime PreviousDate=AppointmentDate.AddDays(-1);
				PreviousDateDay=PreviousDate.Day;
				PreviousDateMonth=PreviousDate.Month;
				PreviousDateYear=PreviousDate.Year;
				DateTime NextDate=AppointmentDate.AddDays(1);
				NextDateDay=NextDate.Day;
				NextDateMonth=NextDate.Month;
				NextDateYear=NextDate.Year;
				if(Session["CustomerNum"]!=null) {
					Message.Text="LoggedIn";
					Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
					List<Appointmentm> appointmentmList=Appointmentms.GetAppointmentms(CustomerNum,AppointmentDate,AppointmentDate);
					Repeater1.DataSource=appointmentmList;
					Repeater1.DataBind();
				}
			}
		}

		public string GetPatientName(long PatNum) {
			try {
				Patientm pat=Patientms.GetOne(CustomerNum,PatNum);
				return pat.LName+ ", " +pat.FName;
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				return "";
			}

		}









	}
}