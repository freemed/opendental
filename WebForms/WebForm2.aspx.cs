using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using OpenDentBusiness;


using System.Drawing.Text;

namespace WebForms {
	/// <summary>
	/// For the next verion of webforms -+This is work in progress.
	/// </summary>
	public partial class WebForm2:System.Web.UI.Page {

		private long DentalOfficeID=0;
		private long WebSheetDefNum=0;
		private Hashtable FormValuesHashTable=new Hashtable();
		List<WControl> listwc= new List<WControl>();
		

		protected void Page_Load(object sender,EventArgs e) {
			try {

				if(Request["DentalOfficeID"]!=null) {
					Int64.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
				}
				if(Request["WebSheetDefNum"]!=null) {
					Int64.TryParse(Request["WebSheetDefNum"].ToString().Trim(),out WebSheetDefNum);
				}
				Panel2.Visible=true;
				GeneratePage(DentalOfficeID,WebSheetDefNum);
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
			
		}

		private void GeneratePage(long DentalOfficeID,long WebSheetDefNum) {
			try {
				int FormXOffset=37;
				int FormYOffset=26;

				int ImageXOffset=0;
				int ImageYOffset=0;
				int ImageZIndex=1;

				int DrawingZIndex=2;

				int ElementZIndex=3;

				int SubmitButtonXoffset=-150;
				int SubmitButtonYoffset=-50;

				int RadioButtonXOffset=-4;
				int RadioButtonYOffset=-5;
				int RadioButtonXOffsetIE=0;
				int RadioButtonXOffsetFirefox=-2;

				float CheckBoxXOffset=-4.0f;
				float CheckBoxYOffset=-4.0f;

				int SignatureFontSize=20;
				

				float heightfactor =1.2f;
				System.Web.HttpBrowserCapabilities browser=Request.Browser;
				
				if(browser.Browser == "Firefox") {
					RadioButtonXOffset=RadioButtonXOffset+RadioButtonXOffsetFirefox;
				}
				if(browser.Browser == "IE") {
					RadioButtonXOffset=RadioButtonXOffset+RadioButtonXOffsetIE;
				}
				ODWebServiceEntities db=new ODWebServiceEntities();
				int ColorBorder=db.webforms_preference.Where(pref => pref.DentalOfficeID==DentalOfficeID).First().ColorBorder;
				bodytag.Attributes.Add("bgcolor",ColorTranslator.ToHtml(Color.FromArgb(ColorBorder)));
				var SheetDefObj=db.webforms_sheetdef.Where(sd => sd.WebSheetDefNum==WebSheetDefNum && sd.webforms_preference.DentalOfficeID==DentalOfficeID).First();
				int SheetDefWidth=SheetDefObj.Width;
				int SheetDefHeight=SheetDefObj.Height;
				form1.Style["position"]="absolute";
				form1.Style["top"]=FormXOffset+"px";
				form1.Style["left"]=FormYOffset+"px";
				form1.Style["width"]=SheetDefWidth+"px";
				form1.Style["height"]=SheetDefHeight+"px";
				form1.Style["background-color"]="white";
				var sfdObj=(from sfd in db.webforms_sheetfielddef where sfd.webforms_sheetdef.WebSheetDefNum==WebSheetDefNum && sfd.webforms_sheetdef.webforms_preference.DentalOfficeID==DentalOfficeID
							select sfd).ToList();
				for(int j=0;j<sfdObj.Count();j++) {
					String FieldName=sfdObj.ElementAt(j).FieldName;
					String FieldValue=sfdObj.ElementAt(j).FieldValue;
					SheetFieldType FieldType=(SheetFieldType)sfdObj.ElementAt(j).FieldType;
					int XPos=sfdObj.ElementAt(j).XPos;
					int YPos=sfdObj.ElementAt(j).YPos;
					int width=sfdObj.ElementAt(j).Width;
					int height=sfdObj.ElementAt(j).Height;
					float fontsize=sfdObj.ElementAt(j).FontSize;
					String fontname=sfdObj.ElementAt(j).FontName;

					WebControl wc=null; // WebControl is the parent class of all controls
					if(FieldType==SheetFieldType.InputField) {
						TextBox tb=new TextBox();
						int rows = (int)Math.Floor((double)height/fontsize);
						if(rows>1) {
							tb.TextMode = TextBoxMode.MultiLine;
							tb.Rows=rows;
						}
						tb.Text=FieldValue;
						wc=tb;						
					}
					if(FieldType==SheetFieldType.CheckBox) {
						wc=AddRadioButtonOrCheckBox(sfdObj.ElementAt(j),XPos,YPos,RadioButtonXOffset,RadioButtonYOffset,ElementZIndex);
					}
					if(FieldType==SheetFieldType.StaticText) {
						Label lb=new Label();
						if(FieldValue.Contains("[dateToday]")) {
							FieldValue=FieldValue.Replace("[dateToday]",DateTime.Today.ToString("M/d/yyyy"));
						}

						lb.Text= FieldValue;
						wc=lb;
					}
					if(FieldType==SheetFieldType.Image||FieldType==SheetFieldType.Rectangle||FieldType==SheetFieldType.Line) {
						System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
						long WebSheetFieldDefNum=sfdObj.ElementAt(j).WebSheetFieldDefNum;
						img.ImageUrl=("~/Handler1.ashx?WebSheetFieldDefNum="+WebSheetFieldDefNum);
						wc=img;
					}
					if(FieldType==SheetFieldType.SigBox) {
						Panel pa = new Panel();
						pa.BorderStyle=BorderStyle.Solid;
						pa.Style["padding-top"]=height/2 +"px";
						pa.HorizontalAlign=HorizontalAlign.Center;
						
						Label lb=new Label();
						lb.Style["font-size"]=SignatureFontSize+"px";
						lb.Text= "Signature will be recorded later.";
						pa.Controls.Add(lb);
						wc=pa;
					}

					if(wc!=null) {
						if(String.IsNullOrEmpty(wc.ID)) {
							wc.ID=FieldName;
						}
						wc.Style["position"]="absolute";
						wc.Style["width"]=width+"px";
						wc.Style["height"]=height+"px";
						wc.Style["top"]=YPos+"px";
						wc.Style["left"]=XPos+"px";
						wc.Style["z-index"]=""+ElementZIndex;


						if(FieldType==SheetFieldType.Image) {
							wc.Style["top"]=YPos+ImageYOffset+"px";
							wc.Style["left"]=XPos+ImageXOffset+"px";
							wc.Style["z-index"]=""+ImageZIndex;
						}
						if(FieldType==SheetFieldType.Rectangle||FieldType==SheetFieldType.Line) {
							wc.Style["z-index"]=""+DrawingZIndex;
						}

						if(FieldType==SheetFieldType.InputField) { //textboxes
							wc.Style["font-family"]=fontname;
							wc.Style["font-size"]=fontsize+"px";
							wc.Style["height"]=height/heightfactor+"px";
							AddValidator(FieldName);
							WControl wcobj = new WControl(XPos,YPos,wc);
							listwc.Add(wcobj);

						}
						if(wc.GetType()==typeof(RadioButtonList)) {
							wc.Style["position"]="static";
							WControl wcobj = new WControl(XPos,YPos,wc);
							listwc.Add(wcobj);
						}
						if(wc.GetType()==typeof(CheckBox)) {
							wc.Style["top"]=YPos+CheckBoxYOffset+"px";
							wc.Style["left"]=XPos+CheckBoxXOffset+"px";
							WControl wcobj = new WControl(XPos,YPos,wc);
							listwc.Add(wcobj);
						}
						if(FieldType==SheetFieldType.StaticText) {
							wc.Style["font-family"]=fontname;
							wc.Style["font-size"]=fontsize+"px";
						}
						Panel1.Controls.Add(wc);
					}
				}

				AssignTabOrder();

				//position the submit button at the end of the page.
				Button1.Style["position"]="absolute";
				Button1.Style["left"]=SheetDefWidth+SubmitButtonXoffset+"px";
				Button1.Style["top"]=SheetDefHeight+SubmitButtonYoffset+"px";


				}
				catch(ApplicationException ex) {
					Logger.Information(ex.Message.ToString());
				}

		}

		private WebControl AddRadioButtonOrCheckBox(webforms_sheetfielddef sfd,int XPos,int YPos,int RadioButtonXOffset,int RadioButtonYOffset,int ElementZIndex) {
			

			/*

				bool RadioButtonListExists=false;
				RadioButtonList rb=null;
				ListItem li=new ListItem();
				li.Value=RadioButtonValue;
				li.Text="";

				li.Attributes.CssStyle.Add("position","absolute");
				li.Attributes.CssStyle.Add("left",XPos+RadioButtonXOffset+"px");
				li.Attributes.CssStyle.Add("top",YPos+RadioButtonYOffset+"px");
				li.Attributes.CssStyle.Add("z-index",""+ElementZIndex);
				//search for existing RadioButtonList by the same name.
				foreach(Control c in Panel1.Controls) {
					if(c.ID==FieldName && c.GetType()==typeof(RadioButtonList)) {
						rb=(RadioButtonList)c;
						RadioButtonListExists=true;
					}
					if(FieldName=="misc" && c.ID==RadioButtonGroup && c.GetType()==typeof(RadioButtonList)) {
						rb=(RadioButtonList)c;
						RadioButtonListExists=true;
					}
					if(FieldName=="misc" && RadioButtonValue=="" && c.GetType()==typeof(RadioButtonList)) {
						//put code here

					}
				}
				if(RadioButtonListExists==false) {
					if(RadioButtonValue=="") {
						CheckBox cb=new CheckBox();
						wc=cb;
					}
					else {
						rb=new RadioButtonList();
						rb.RepeatDirection=RepeatDirection.Horizontal;
						wc=rb;
					}
				}
				if(rb!=null) {
					rb.Items.Add(li);
				}

			//old code
			*/

			String FieldName=sfd.FieldName;
			String RadioButtonValue=sfd.RadioButtonValue;
			String RadioButtonGroup=sfd.RadioButtonGroup;
			WebControl wc=null;

			CheckBox cb=new CheckBox();
			cb.ID=FieldName+RadioButtonValue;
			AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender mecb = new AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender();
			mecb.ID=cb.ID+"MutuallyExclusiveCheckBoxExtender";
			mecb.TargetControlID=cb.ID;
			if(!String.IsNullOrEmpty(sfd.RadioButtonGroup) && FieldName=="misc") {
				mecb.Key=RadioButtonGroup;
			}
			else {// cases like gender, position etc that have no value for RadioButtonGroup
				mecb.Key=FieldName;
			}
			Panel1.Controls.Add(mecb);

			wc=cb;


			return wc;
		}

		private void AssignTabOrder() {
			var sortedlist= listwc.OrderBy(wc => wc.YPos).ThenBy(wc => wc.XPos).ToList();
			for(short i=0;i<sortedlist.Count();i++){
				sortedlist[i].wc.TabIndex=(short)(i+1);
			}
		}
		/// <summary>
		/// A class made  just for sorting purposes.
		/// </summary>
		public class WControl{
			public int XPos=0;
			public int YPos=0;
			public WebControl wc=null;

			public WControl(int XPos,int YPos,WebControl wc) {
				this.XPos=XPos;
				this.YPos=YPos;
				this.wc=wc;
			}

		}

		private void AddValidator(string FieldName) {

			String ErrorMessage="";
			

			if(FieldName.ToLower()=="fname" || FieldName.ToLower()=="firstname") {
				ErrorMessage="First Name is a required field";
			}
			else if(FieldName.ToLower()=="lname" || FieldName.ToLower()=="lastname") {
				ErrorMessage="Last Name is a required field";
			}
			else if(FieldName.ToLower()=="birthdate" || FieldName.ToLower()=="bdate") {
				ErrorMessage="Birthdate is a required field";
			}
			else {
				return;
			}
			RequiredFieldValidator rv = new RequiredFieldValidator();
			rv.ControlToValidate=FieldName;
			rv.ErrorMessage=ErrorMessage;
			rv.Display=ValidatorDisplay.None;
			rv.SetFocusOnError=true;
			rv.ID=FieldName+"RequiredFieldValidator";

			AjaxControlToolkit.ValidatorCalloutExtender vc = new AjaxControlToolkit.ValidatorCalloutExtender();
			vc.TargetControlID=rv.ID;
			vc.ID="ValidatorCalloutExtender"+rv.ID;
			Panel1.Controls.Add(rv);
			Panel1.Controls.Add(vc);

			if(FieldName.ToLower()=="birthdate" || FieldName.ToLower()=="bdate") {
				CompareValidator cv = new CompareValidator();
				cv.ControlToValidate=FieldName;
				cv.ErrorMessage="Invalid Date of Birth.";
				cv.Display=ValidatorDisplay.None;
				cv.Type=ValidationDataType.Date;
				cv.Operator=ValidationCompareOperator.DataTypeCheck;
				cv.SetFocusOnError=true;
				cv.ID=FieldName+"CompareValidator";
				AjaxControlToolkit.ValidatorCalloutExtender vc1 = new AjaxControlToolkit.ValidatorCalloutExtender();
				vc1.TargetControlID=cv.ID;
				vc1.ID="ValidatorCalloutExtender"+cv.ID;
				Panel1.Controls.Add(cv);
				Panel1.Controls.Add(vc1);
				
			}
		}
		protected void Button1_Click(object sender,EventArgs e) {
			LoopThroughControls(this.Page);
			SaveFieldValuesInDB(DentalOfficeID);
		}

		private void LoopThroughControls(Page p) {
			try {
				foreach(Control c in p.Controls) {
					if(c.HasControls()) {
						ExtractValue(c);
						FindControls(c);
					}
					else {
						ExtractValue(c);
					}
				}
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString()+" IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
		}

		/// <summary>
		/// This is a recursive function which searches through nested controls on a  webpage
		/// </summary>
		private void FindControls(Control c) {
			try {
				foreach(Control ctl in c.Controls) {
					if(ctl.HasControls()) {
						ExtractValue(ctl);
						FindControls(ctl);
					}
					else {
						ExtractValue(ctl);
					}
				}
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString()+" IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
		}

		/// <summary>
		/// Fill the FormValuesHashTable here.
		/// </summary>
		private void ExtractValue(Control c) {
			try {
				if(c.GetType()==typeof(TextBox)) {
					TextBox tbox=((TextBox)c);
					if(tbox.Text.Trim()!="") {
						//string FieldName=tbox.ID.Remove(0,"TextBox".Length);
						string FieldName=tbox.ID;
						FormValuesHashTable.Add(FieldName,tbox.Text.Trim());
					}
				}
				if(c.GetType()==typeof(RadioButtonList)) {
					RadioButtonList rbl=((RadioButtonList)c);
					//string FieldName=rbl.ID.Remove(0,"RadioButtonList".Length);
					string FieldName=rbl.ID;
					if(rbl.SelectedIndex!=-1) {
						FormValuesHashTable.Add(FieldName,rbl.SelectedValue);
					}
				}
				if(c.GetType()==typeof(CheckBox)) {
					CheckBox cbox=((CheckBox)c);
					//string FieldName=cbox.ID.Remove(0,"CheckBox".Length);
					string FieldName=cbox.ID;
					if(cbox.Checked==true) {
						FormValuesHashTable.Add(FieldName,cbox.Checked.ToString());
					}
				}
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString()+" IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
		}

		private void SaveFieldValuesInDB(long DentalOfficeID) {
			try {
				ODWebServiceEntities db=new ODWebServiceEntities();
				webforms_sheet NewSheetObj=new webforms_sheet();
				var PrefObj=from wp in db.webforms_preference where wp.DentalOfficeID==DentalOfficeID
							select wp;
				NewSheetObj.DateTimeSubmitted=DateTime.Now;
				foreach(string key in FormValuesHashTable.Keys) {
					webforms_sheetfield NewSheetfieldObj=new webforms_sheetfield();
					NewSheetfieldObj.FieldName=key;
					NewSheetfieldObj.FieldValue=FormValuesHashTable[key].ToString();
					NewSheetObj.webforms_sheetfield.Add(NewSheetfieldObj);
				}
				if(PrefObj.Count()>0) {
					PrefObj.First().webforms_sheet.Add(NewSheetObj);
					db.SaveChanges();
					LabelSubmitMessage.Text="Your details have been successfully submited";
					Logger.Information("Form values saved from IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				}
				Panel1.Visible=false;
				Panel2.Visible=true;
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
				Panel1.Visible=false;
				LabelSubmitMessage.Text="There has been a problem submitting your details. <br /> We apologize for the inconvenience.";
				Logger.Information("There has been a problem submitting your details IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
		}


	}
}
