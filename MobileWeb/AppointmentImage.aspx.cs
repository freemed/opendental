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
	public partial class AppointmentImage:System.Web.UI.Page {
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
				CustomerNum=util.GetCustomerNum(Message);
				if(CustomerNum==0) {
					return;
				}
				#region process dates
				int Year=0;
				int Month=0;
				int Day=0;
				DateTime AppointmentDate=DateTime.MinValue;
				if(Request["year"]!=null && Request["month"]!=null && Request["day"]!=null) {
					Int32.TryParse(Request["year"].ToString().Trim(),out Year);
					Int32.TryParse(Request["month"].ToString().Trim(),out Month);
					Int32.TryParse(Request["day"].ToString().Trim(),out Day);
					AppointmentDate= new DateTime(Year,Month,Day);
				}
				else {
					//dennis set cookies here this would be read by javascript on the client browser.
					HttpCookie DemoDateCookieY=new HttpCookie("DemoDateCookieY");
					HttpCookie DemoDateCookieM=new HttpCookie("DemoDateCookieM");
					HttpCookie DemoDateCookieD=new HttpCookie("DemoDateCookieD");
					if(CustomerNum==util.GetDemoDentalOfficeID()) {
						AppointmentDate=util.GetDemoTodayDate();//for demo only. The date is set to a preset date in webconfig.
						DemoDateCookieY.Value=AppointmentDate.Year+"";
						DemoDateCookieM.Value=AppointmentDate.Month+"";
						DemoDateCookieD.Value=AppointmentDate.Day+"";
					}
					else {
						DemoDateCookieY.Value="";// these are explicitely set to empty because javascript on the browser is picking values from previously set cookies
						DemoDateCookieM.Value="";
						DemoDateCookieD.Value="";
						AppointmentDate=DateTime.Today;
					}
					Response.Cookies.Add(DemoDateCookieY);// if expiry is not specified the cookie lasts till the end of session
					Response.Cookies.Add(DemoDateCookieM);
					Response.Cookies.Add(DemoDateCookieD);
				}
				DayLabel.Text=AppointmentDate.ToString("ddd")+", "+AppointmentDate.ToString("MMM")+AppointmentDate.ToString("dd");
				DateTime PreviousDate=AppointmentDate.AddDays(-1);
				PreviousDateDay=PreviousDate.Day;
				PreviousDateMonth=PreviousDate.Month;
				PreviousDateYear=PreviousDate.Year;
				DateTime NextDate=AppointmentDate.AddDays(1);
				NextDateDay=NextDate.Day;
				NextDateMonth=NextDate.Month;
				NextDateYear=NextDate.Year;
				#endregion

				List<String> appointmentmList = new List<String> { "133,46,183,94","272,84,369,172","375,557,482,622" };
				Repeater1.DataSource=appointmentmList;
				Repeater1.DataBind();

			}
			catch(Exception ex) {
				LabelError.Text=Util.ErrorMessage;
				Logger.LogError(ex);
			}
		}


	}
}