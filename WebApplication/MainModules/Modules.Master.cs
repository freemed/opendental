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

namespace WebApplication {
	public partial class MasterPage:System.Web.UI.MasterPage {
		private int selectedModule;//0 or 1 for now

		protected void Page_Load(object sender,EventArgs e) {
			if(Session["SelectedModule"]==null){
				selectedModule=-1;
			}
			else{
				selectedModule=(int)Session["SelectedModule"];
			}
			if(selectedModule==0) {
				PanelApt.BackColor=Color.White;
				PanelApt.BorderStyle=BorderStyle.Solid;
			}
			else {
				PanelApt.BackColor=Color.Empty;
				PanelApt.BorderStyle=BorderStyle.None;
			}
			if(selectedModule==1) {
				PanelFam.BackColor=Color.White;
				PanelFam.BorderStyle=BorderStyle.Solid;
			}
			else {
				PanelFam.BackColor=Color.Empty;
				PanelFam.BorderStyle=BorderStyle.None;
			}		
		}

		protected void ImgButApt_Click(object sender,ImageClickEventArgs e) {
			//not useful
		}

		protected void ImgButFamily_Click(object sender,ImageClickEventArgs e) {
			//not useful
		}

		protected void Page_PreRender(object sender,EventArgs e){
			
		}



	}
}
