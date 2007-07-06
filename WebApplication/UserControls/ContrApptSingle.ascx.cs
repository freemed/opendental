using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebApplication.UserControls {
	public partial class ContrApptSingle:System.Web.UI.UserControl {
		public float heightInRows;
		public string patientName;
		public DateTime aptDateTime;
		public int op;

		/*
		///<summary>The heightInRows is typically an int, but might include half rows because it's based on the original 5 minute pattern.</summary>
		public ContrApptSingle(float heightInRows,string patientName,DateTime aptDateTime){
			this.heightInRows=heightInRows;
			this.patientName=patientName;
			this.aptDateTime=aptDateTime;
		}

		///<summary>Default Constructor.</summary>
		public ContrApptSingle() {
			this.heightInRows=6;
			this.patientName="Bill Smith";
			this.aptDateTime=DateTime.MinValue;
		}*/

		protected void Page_Init(object sender,EventArgs e) {
			if(patientName==null){
				patientName="Bill Smith";
			}
			if(heightInRows==0){
				heightInRows=6;
			}
			//if(aptDateTime.is==null){
			
			//}
			
			if(Panel1==null){
				Panel1=new Panel();
				Panel1.Width=100;
				Panel1.Height=100;
				Panel1.BackColor=Color.LightSteelBlue;
				Panel1.BorderStyle=BorderStyle.Solid;
				Panel1.BorderColor=Color.Gray;
				Panel1.BorderWidth=1;
				this.Controls.Add(Panel1);
			}
			if(Label1==null) {
				Label1=new Label();
				Panel1.Controls.Add(Label1);
			}
			Panel1.Height=(int)(heightInRows*(float)ContrApptSheet.Lh);
			Panel1.Width=ContrApptSheet.ColWidth-5;
			int yPos=(int)(((double)(ContrApptSheet.Lh*6))*aptDateTime.TimeOfDay.TotalHours);
			int xPos=op*ContrApptSheet.ColWidth+1;
			Panel1.Style.Clear();
			Panel1.Style.Add("position","absolute");
			Panel1.Style.Add("top",yPos.ToString()+"px");
			Panel1.Style.Add("left",xPos.ToString()+"px");
			//Panel1.Style.Add("overflow","hidden");
			Label1.Text=patientName;
		}

		protected void Page_Load(object sender,EventArgs e) {
			
		}
	}
}