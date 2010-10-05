using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using OpenDentBusiness;

namespace WebForms {
	/// <summary>
	/// For the next verion of webforms -  This is work in progress.
	/// </summary>
	public partial class WebForm2:System.Web.UI.Page {

		private long DentalOfficeID=1486;
		private long SheetDefNum=7;
		

		protected void Page_Load(object sender,EventArgs e) {
			try {
				if(Request["DentalOfficeID"]!=null) {
					Int64.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
				}
				if(Request["SheetDefNum"]!=null) {
					Int64.TryParse(Request["SheetDefNum"].ToString().Trim(),out SheetDefNum);
				}
				GeneratePage(DentalOfficeID,SheetDefNum);
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
		}

		private void GeneratePage(long DentalOfficeID,long SheetDefNum) {
			try {
				int xoffset = 37;
				int yoffset = 26;
				int FormHeight = 0;
				int FormHeightOffset = 0;
				int maxYPos=0;
				int maxXPos=0;
				int maxHeight=0;
				int maxWidth=0;
				int buttonXoffset=0;
				int buttonYoffset=0;
				int RadioButtonXOffset=0;
				int RadioButtonYOffset=5;
				float heightfactor =1.2f;
				System.Web.HttpBrowserCapabilities browser = Request.Browser;
				
				if(browser.Browser == "Firefox") {
				}
				if(browser.Browser == "IE") {
					RadioButtonXOffset=-6;
				}

				form1.Style["background-color"]="#0066FF";
				//form1.Style["background-image"]="url('Patient Info.gif')";
				form1.Style["background-repeat"]="no-repeat";
				form1.Style["background-position"]=xoffset + "px "+ yoffset + "px";
				ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj = (from sfd in db.webforms_sheetfielddef where sfd.SheetDefNum==SheetDefNum && sfd.webforms_sheetdef.webforms_preference.DentalOfficeID==DentalOfficeID
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
					if(XPos>maxXPos) {
						maxXPos=XPos;
						maxWidth=width;
					}
					if(YPos>maxYPos) {
						maxYPos=YPos;
						maxHeight=height;
					}
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
						//search  for existing RadioButtonList by the same name.
						foreach(Control c in form1.Controls) {
							if(c.ID==FieldName && c.GetType()==typeof(RadioButtonList)) {
								rb = (RadioButtonList)c;
										RadioButtonListExists = true;
								}
						}
						if(RadioButtonListExists==false) {
							rb = new RadioButtonList();
							rb.RepeatDirection = RepeatDirection.Horizontal;
							wc = rb;
						}
						rb.Items.Add(li);
					}
					if(FieldType==SheetFieldType.StaticText) {

					}
					if(FieldType==SheetFieldType.Image) {
						System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

						long WebSheetFieldDefNum = sfdObj.ElementAt(j).WebSheetFieldDefNum;

						img.ImageUrl = ("~/Handler1.ashx?WebSheetFieldDefNum="+WebSheetFieldDefNum);
						wc = img;
						//Bitmap bitmap=PIn.Bitmap(FieldValue);
						//Response.ContentType = "image/Jpeg";
						//bitmap.Save(Response.OutputStream,ImageFormat.Jpeg);

						/*
						byte[] image = (byte[])FieldValue;
					stream.Write(image,0,image.Length);
					Bitmap bitmap = new Bitmap(stream);
					Response.ContentType = "image/gif";
					bitmap.Save(Response.OutputStream,ImageFormat.Gif);
					*/
					}
					if(wc!=null) {
						//wc.BorderStyle=BorderStyle.None;
						wc.ID = FieldName;
						wc.Style["position"]="absolute";
						wc.Style["width"]=width+"px";
						wc.Style["height"]=height/heightfactor+"px";
						wc.Style["top"]=YPos+"px";
						if(wc.GetType()==typeof(RadioButtonList)) {
							wc.Style["top"]=YPos - RadioButtonYOffset +"px";
						}else {
							wc.Style["left"]=XPos+"px";
						}
						form1.Controls.Add(wc);
					}

				}
				//position the submit button at the end of the page.
				Button1.Style["position"]="absolute";
				Button1.Style["left"]=maxXPos+maxWidth+buttonXoffset+"px";
				Button1.Style["top"]=maxYPos+maxHeight+buttonYoffset+"px";
				//set form height - if this is not set none of the elements in the form will be seen.
				FormHeight=maxYPos+maxHeight+buttonYoffset+ FormHeightOffset;
				form1.Style["height"]=FormHeight+"px";



				}
				catch(ApplicationException ex) {
					Logger.Information(ex.Message.ToString());
				}

		}
	}
}
