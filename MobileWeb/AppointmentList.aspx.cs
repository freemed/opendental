using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Drawing;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace MobileWeb {
	public partial class AppointmentList:System.Web.UI.Page {
		private long CustomerNum=0;
		private Util util=new Util();
		public int PreviousDateDay=0;
		public int PreviousDateMonth=0;
		public int PreviousDateYear=0;
		public int NextDateDay=0;
		public int NextDateMonth=0;
		public int NextDateYear=0;
		

		protected void Page_Load(object sender,EventArgs e) {
			try {
				if(!SetCustomerNum()) {
					return;
				}
				int Year=0;
				int Month=0; 
				int Day=0;
				DateTime AppointmentDate;
				if(Request["year"]!=null && Request["month"]!=null && Request["day"]!=null) {
					Int32.TryParse(Request["year"].ToString().Trim(),out Year);
					Int32.TryParse(Request["month"].ToString().Trim(),out Month);
					Int32.TryParse(Request["day"].ToString().Trim(),out Day);
					AppointmentDate= new DateTime(Year,Month,Day);
				}
				else {
					if(CustomerNum==util.GetDemoDentalOfficeID()) {
						AppointmentDate=util.GetDemoTodayDate();//for demo only. The date is set to a preset date in webconfig.
					}
					else {
						AppointmentDate=DateTime.Today;
					}
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
				List<Appointmentm> appointmentmList=Appointmentms.GetAppointmentms(CustomerNum,AppointmentDate,AppointmentDate);
				Repeater1.DataSource=appointmentmList;
				Repeater1.DataBind();
			}
			catch(Exception ex) {
				LabelError.Text=Util.ErrorMessage;
				Logger.LogError(ex);
			}
		}

		public string GetPatientName(long PatNum) {
			return util.GetPatientName(PatNum,CustomerNum);
		}

		public string GetProviderColor(Appointmentm ap) {
			Providerm pv=Providerms.GetOne(CustomerNum,ap.ProvNum);
			string HexColor=ColorTranslator.ToHtml(pv.ProvColor);
			return HexColor;
		}
		private bool SetCustomerNum(){
			Message.Text="";
			if(Session["CustomerNum"]==null) {
				return false;
			}
			Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
			if(CustomerNum!=0) {
				Message.Text="LoggedIn";
			}
			return true;
		}








	}
}