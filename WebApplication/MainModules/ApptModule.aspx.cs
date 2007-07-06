using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebApplication.UserControls;
using OpenDentBusiness;

namespace WebApplication {
	public partial class ApptModule:System.Web.UI.Page {
		private DataSet DS;

		protected void Page_Load(object sender,EventArgs e) {
			Session["SelectedModule"]=0;
			//string scrollY=Page.Request.Params[panelSheet.ClientID+"ScrollY"];
			if(Calendar1.SelectedDate==DateTime.MinValue){
				Calendar1.SelectedDate=DateTime.Today;
			}
			RefreshPeriod();
		}

		private void RefreshPeriod(){
			DataConnection con=new DataConnection();
			con.SetDb("localhost","development51","root","","root","",DatabaseType.MySql);
			DateTime startDate;
			DateTime endDate;
			startDate=Calendar1.SelectedDate;
			endDate=Calendar1.SelectedDate;
			string[] parameters=new string[2];
			parameters[0]=startDate.ToString();
			parameters[1]=endDate.ToString();
			DS=AppointmentB.RefreshPeriod(parameters);
			DataRow row;
			for(int i=ContrApptSheet2.Controls.Count-1;i>=0;i--){//go backwards to allow removal
				if(ContrApptSheet2.Controls[i].GetType()==typeof(ContrApptSingle)) {
					ContrApptSheet2.Controls.RemoveAt(i);
				}
			}
			//foreach (Control ctrl in ContrApptSheet2.Controls){
				
			//}
			//ContrApptSheet2.Controls.Clear();
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
				row=DS.Tables["Appointments"].Rows[i];
				ContrApptSingle aptSingle=new ContrApptSingle();
				//i,i.ToString(),DateTime.Today.AddHours(i));
				aptSingle.heightInRows=4;
				aptSingle.patientName=row["patientName"].ToString();
				aptSingle.aptDateTime=PIn.PDateT(row["AptDateTime"].ToString());
				aptSingle.op=PIn.PInt(row["Op"].ToString());
				ContrApptSheet2.Controls.Add(aptSingle);
			} 
		}

		protected void Calendar1_SelectionChanged(object sender,EventArgs e) {
			
			RefreshPeriod();
		}


	}
}
