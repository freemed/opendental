using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenDentBusiness;

namespace WebForms {
	/// <summary>
	/// For the next verion of webforms -  This is work in progress.
	/// </summary>
	public partial class WebForm2:System.Web.UI.Page {

	
		protected void Page_Load(object sender,EventArgs e) {

			int xoffset = 37;
			int yoffset = 26;
			int FormHeight = 1150;
			System.Web.HttpBrowserCapabilities browser = Request.Browser;

			int RadioButtonXOffset=0;
			int RadioButtonYOffset=5;
			
			if(browser.Browser == "Firefox") {
			}
			if(browser.Browser == "IE") {
				RadioButtonXOffset=-6;
			}

			form1.Style["background-image"]="url('Patient Info.gif')";
			form1.Style["background-repeat"]="no-repeat";
			form1.Style["background-position"]=xoffset + "px "+ yoffset + "px";

			form1.Style["height"]=FormHeight+"px";

			opendental73Entities db=new opendental73Entities();
			var sfdObj = (from sfd in db.sheetfielddef where sfd.SheetDefNum==5
						  select sfd).ToList();

			for(int j=0;j<sfdObj.Count();j++) {
				String FieldName=sfdObj.ElementAt(j).FieldName;
				String FieldValue=sfdObj.ElementAt(j).FieldValue;
				String RadioButtonValue=sfdObj.ElementAt(j).RadioButtonValue;
				SheetFieldType FieldType=(SheetFieldType)sfdObj.ElementAt(j).FieldType;
				int XPos=sfdObj.ElementAt(j).XPos;
				int YPos=sfdObj.ElementAt(j).YPos;
				int width = sfdObj.ElementAt(j).Width;
				int height = sfdObj.ElementAt(j).Height;
				float fontsize = sfdObj.ElementAt(j).FontSize;
				String fontname = sfdObj.ElementAt(j).FontName;

				WebControl wc=null; // WebControl is the parent class of all controls

				if(FieldType==SheetFieldType.InputField) {
					TextBox tb = new TextBox();
					
					tb.Text = FieldValue;
					

					tb.Style["font-family"]=fontname;
					tb.Style["font-size"]=fontsize+"px";
					wc = tb;
				}
				if(FieldType==SheetFieldType.CheckBox) {


					bool RadioButtonListExists = false;
					RadioButtonList rb=null;
					ListItem li = new ListItem();
					li.Value = RadioButtonValue;
					li.Text = "";

					li.Attributes.CssStyle.Add("position","absolute");
					li.Attributes.CssStyle.Add("left",XPos + RadioButtonXOffset + "px");

					foreach(Control c in form1.Controls) {
						if(c.ID==FieldName && c.GetType()==typeof(RadioButtonList)) {
							rb = (RadioButtonList)c;
									//rb.Items.Add(li);
									RadioButtonListExists = true;
							}
					}

					if(RadioButtonListExists==false) {
						rb = new RadioButtonList();
						rb.RepeatDirection = RepeatDirection.Horizontal;
						//rb.Items.Add(li);
						wc = rb;
					}
					rb.Items.Add(li);

					/*
					CheckBox cb = new CheckBox();
					cb.ID = RadioButtonValue;
					wc = cb;
					*/

				}
				if(FieldType==SheetFieldType.StaticText) {

				}
				if(wc!=null) {
					wc.ID = FieldName;
					wc.Style["position"]="absolute";
					wc.Style["width"]=width+"px";
					wc.Style["height"]=height/1.5+"px";
					wc.Style["top"]=YPos+"px";

					if(wc.GetType()==typeof(RadioButtonList)) {

						wc.Style["top"]=YPos - RadioButtonYOffset +"px";
						
					}
					else {
						wc.Style["left"]=XPos+"px";
					}



					form1.Controls.Add(wc);
				}

			}


		}
	}
}
