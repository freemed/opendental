///Dennis Mathew: For using ADO.NET Entity Data Model/LINQ with Mysql/Visual Studio 2010, download and install Connector/Net from http://dev.mysql.com/downloads/connector/net/ 
/// Connector/Net is a ADO.NET driver for MySQL.
/// The web server which hosts the webservice will also need this install.
/// The integration with Visual Studio can be flakey. So a few cycles of install/uninstall/restart may be needed. I've also tried the non-install options of adding dlls but they don't seem to work in the few attempts that I made.


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
	/// For the next version of webforms -This is work in progress.
	/// </summary>
	public partial class Sheets:System.Web.UI.Page {
		private long DentalOfficeID=0;
		private long WebSheetDefID=0;
		private Hashtable FormValuesHashTable=new Hashtable();
		List<WControl> listwc=new List<WControl>();
		
		protected void Page_Load(object sender,EventArgs e) {
			try {
				if(Request["DentalOfficeID"]!=null) {
					Int64.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
				}
				if(Request["WebSheetDefID"]!=null) {
					Int64.TryParse(Request["WebSheetDefID"].ToString().Trim(),out WebSheetDefID);
				}
				Logger.Information("Page requested from IpAddress="+HttpContext.Current.Request.UserHostAddress+"for  DentalOfficeID="+DentalOfficeID);
				Panel2.Visible=true;
				GeneratePage(DentalOfficeID,WebSheetDefID);
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				DisplayMessage("Error: Your form is not available. Please contact your Dental Office");
			}
			
		}

		private void GeneratePage(long DentalOfficeID,long WebSheetDefID) {
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
				int SignatureFontSize=16;
				String SignatureFont="sans-serif";
				float heightfactor=1.2f;
				System.Web.HttpBrowserCapabilities browser=Request.Browser;
				if(browser.Browser=="Firefox") {
					RadioButtonXOffset+=RadioButtonXOffsetFirefox;
				}
				if(browser.Browser=="IE") {
					RadioButtonXOffset+=RadioButtonXOffsetIE;
				}
				ODWebServiceEntities db=new ODWebServiceEntities();
				int ColorBorder=db.webforms_preference.Where(pref=>pref.DentalOfficeID==DentalOfficeID).First().ColorBorder;
				bodytag.Attributes.Add("bgcolor",ColorTranslator.ToHtml(Color.FromArgb(ColorBorder)));
				var SheetDefObj=db.webforms_sheetdef.Where(sd=>sd.WebSheetDefID==WebSheetDefID && sd.webforms_preference.DentalOfficeID==DentalOfficeID).First();
				int SheetDefWidth=SheetDefObj.Width;
				int SheetDefHeight=SheetDefObj.Height;
				bool SheetDefIsLandscape=SheetDefObj.IsLandscape==(sbyte)1?true:false;
				if(SheetDefIsLandscape) {
					SheetDefWidth=SheetDefObj.Height;
					SheetDefHeight=SheetDefObj.Width;
				}
				form1.Style["position"]="absolute";
				form1.Style["top"]=FormXOffset+"px";
				form1.Style["left"]=FormYOffset+"px";
				form1.Style["width"]=SheetDefWidth+"px";
				form1.Style["height"]=SheetDefHeight+"px";
				form1.Style["background-color"]="white";
				var SheetFieldDefList=(db.webforms_sheetfielddef.Where(sfd=>sfd.webforms_sheetdef.WebSheetDefID==WebSheetDefID && sfd.webforms_sheetdef.webforms_preference.DentalOfficeID==DentalOfficeID)).ToList();
				for(int j=0;j<SheetFieldDefList.Count();j++) {
					String FieldName=SheetFieldDefList.ElementAt(j).FieldName;
					String FieldValue=SheetFieldDefList.ElementAt(j).FieldValue;
					SheetFieldType FieldType=(SheetFieldType)SheetFieldDefList.ElementAt(j).FieldType;
					int XPos=SheetFieldDefList.ElementAt(j).XPos;
					int YPos=SheetFieldDefList.ElementAt(j).YPos;
					int width=SheetFieldDefList.ElementAt(j).Width;
					int height=SheetFieldDefList.ElementAt(j).Height;
					float fontsize=SheetFieldDefList.ElementAt(j).FontSize;
					String fontname=SheetFieldDefList.ElementAt(j).FontName;
					bool fontIsBold=SheetFieldDefList.ElementAt(j).FontIsBold==(sbyte)1?true:false;
					long WebSheetFieldDefID=SheetFieldDefList.ElementAt(j).WebSheetFieldDefID;
					WebControl wc=null; // WebControl is the parent class of all controls
					if(FieldType==SheetFieldType.InputField) {
						TextBox tb=new TextBox();
						int rowcount=(int)Math.Floor((double)height/fontsize);
                        if (rowcount>1){
							tb.TextMode=TextBoxMode.MultiLine;
                            tb.Rows=rowcount;
						}
						tb.Text=FieldValue;
						wc=tb;						
					}
					if(FieldType==SheetFieldType.CheckBox) {
						wc=AddCheckBox(SheetFieldDefList.ElementAt(j));
					}
					if(FieldType==SheetFieldType.StaticText) {
						Label lb=new Label();
						if(FieldValue.Contains("[dateToday]")) {
							FieldValue=FieldValue.Replace("[dateToday]",DateTime.Today.ToString("M/d/yyyy"));
						}
						lb.Text=FieldValue;
						wc=lb;
					}
					if(FieldType==SheetFieldType.Image||FieldType==SheetFieldType.Rectangle||FieldType==SheetFieldType.Line) {
						// this is a bug which must be addressed. Horizontal and vertical lines may have either height or width as zero. this throws an error, so they have been excluded for now
						if(width!=0 && height!=0) { 
							System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
							img.ImageUrl=("~/Handler1.ashx?WebSheetFieldDefID="+WebSheetFieldDefID);
							wc=img;
						}
					}
					if(FieldType==SheetFieldType.SigBox) {
						Panel pa=new Panel();
						pa.BorderStyle=BorderStyle.Solid;
						pa.BorderWidth=Unit.Pixel(1);
						pa.HorizontalAlign=HorizontalAlign.Center;
						Label lb=new Label();
						lb.Style["font-family"]=SignatureFont;
						lb.Style["font-size"]=SignatureFontSize+"px";
						lb.Style["position"]="relative";						
						lb.Style["top"]=(height-SignatureFontSize)/2  +"px";
						lb.Text="Signature will be recorded later.";
						pa.Controls.Add(lb);
						wc=pa;
					}
					if(wc!=null) {
						wc.ID=""+WebSheetFieldDefID;
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
							wc.Style["font-size"]=fontsize+"pt";
							wc.Style["height"]=height/heightfactor+"px";
							if(fontIsBold) {
								wc.Font.Bold=true;
							}
							wc.BorderWidth=Unit.Pixel(0);
							wc.BackColor=Color.LightYellow;
							AddValidator(SheetFieldDefList.ElementAt(j));
							WControl wcobj=new WControl(XPos,YPos,wc);
							listwc.Add(wcobj);
						}
						if(wc.GetType()==typeof(RadioButtonList)) {
							wc.Style["position"]="static";
							WControl wcobj=new WControl(XPos,YPos,wc);
							listwc.Add(wcobj);
						}
						if(wc.GetType()==typeof(CheckBox)) {
							wc.Style["top"]=YPos+CheckBoxYOffset+"px";
							wc.Style["left"]=XPos+CheckBoxXOffset+"px";
							WControl wcobj=new WControl(XPos,YPos,wc);
							listwc.Add(wcobj);
						}
						if(FieldType==SheetFieldType.StaticText) {
							wc.Style["font-family"]=fontname;
							wc.Style["font-size"]=fontsize+"pt";
							if(fontIsBold) {
								wc.Font.Bold=true;
							}
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
					Logger.LogError(ex);
					DisplayMessage("Error: Your form is not available. Please contact your Dental Office");
				}

		}

		private WebControl AddCheckBox(webforms_sheetfielddef sfd)
        {
			String FieldName=sfd.FieldName;
			String RadioButtonValue=sfd.RadioButtonValue;
			String RadioButtonGroup=sfd.RadioButtonGroup;
			WebControl wc=null;
			CheckBox cb=new CheckBox();
			cb.ID=""+sfd.WebSheetFieldDefID;
			AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender mecb=new AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender();
			mecb.ID=cb.ID+"MutuallyExclusiveCheckBoxExtender";
			mecb.TargetControlID=cb.ID;
            if (!String.IsNullOrEmpty(RadioButtonGroup) && FieldName=="misc"){
				mecb.Key=RadioButtonGroup;
			}
			else if(!String.IsNullOrEmpty(RadioButtonValue)) {// cases like gender, position etc that have no value for RadioButtonGroup but have RadioButtonValue
				mecb.Key=FieldName;
			}
			Panel1.Controls.Add(mecb);
			wc=cb;
			return wc;
		}

		private void AssignTabOrder() {
			var sortedlist=listwc.OrderBy(wc=>wc.YPos).ThenBy(wc=>wc.XPos).ToList();
			for(short i=0;i<sortedlist.Count();i++){
				sortedlist[i].wc.TabIndex=(short)(i+1);
			}
		}

		/// <summary>
		/// A class made  just for sorting purposes - to assign tab order to the controls on a web page.
		/// </summary>
		private class WControl{
			public int XPos=0;
			public int YPos=0;
			public WebControl wc=null;
			public WControl(int XPos,int YPos,WebControl wc) {
				this.XPos=XPos;
				this.YPos=YPos;
				this.wc=wc;
			}
		}

		private void AddValidator(webforms_sheetfielddef sfd ) {
			String FieldName=sfd.FieldName;
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
			else if(sfd.IsRequired==(sbyte)1) {
				ErrorMessage="This is a required field";
			}
			else {
				return;
			}
			// required field validator
			RequiredFieldValidator rv=new RequiredFieldValidator();
			rv.ControlToValidate=""+sfd.WebSheetFieldDefID;
			rv.ErrorMessage=ErrorMessage;
			rv.Display=ValidatorDisplay.None;
			rv.SetFocusOnError=true;
			rv.ID="RequiredFieldValidator"+ rv.ControlToValidate;
			//callout extender
			AjaxControlToolkit.ValidatorCalloutExtender vc=new AjaxControlToolkit.ValidatorCalloutExtender();
			vc.TargetControlID=rv.ID;
			vc.ID="ValidatorCalloutExtender"+rv.ID;
			Panel1.Controls.Add(rv);
			Panel1.Controls.Add(vc);
			if(FieldName.ToLower()=="birthdate" || FieldName.ToLower()=="bdate") {
				//compare validator
				CompareValidator cv=new CompareValidator();
				cv.ControlToValidate=""+sfd.WebSheetFieldDefID;
				cv.ErrorMessage="Invalid Date of Birth.";
				cv.Display=ValidatorDisplay.None;
				cv.Type=ValidationDataType.Date;
				cv.Operator=ValidationCompareOperator.DataTypeCheck;
				cv.SetFocusOnError=true;
				cv.ID="CompareValidator"+cv.ControlToValidate;
				//callout extender
				AjaxControlToolkit.ValidatorCalloutExtender vc1=new AjaxControlToolkit.ValidatorCalloutExtender();
				vc1.TargetControlID=cv.ID;
				vc1.ID="ValidatorCalloutExtender"+cv.ID;
				Panel1.Controls.Add(cv);
				Panel1.Controls.Add(vc1);
				
			}
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
						string FieldName=tbox.ID;
						FormValuesHashTable.Add(FieldName,tbox.Text.Trim());
					}
				}
				if(c.GetType()==typeof(RadioButtonList)) {
					RadioButtonList rbl=((RadioButtonList)c);
					string FieldName=rbl.ID;
					if(rbl.SelectedIndex!=-1) {
						FormValuesHashTable.Add(FieldName,rbl.SelectedValue);
					}
				}
				if(c.GetType()==typeof(CheckBox)) {
					CheckBox cbox=((CheckBox)c);
					string FieldName=cbox.ID;
					if(cbox.Checked==true) {
						FormValuesHashTable.Add(FieldName,"X");
					}
				}
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString()+" IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
		}

		private void SaveFieldValuesInDB(long DentalOfficeID,long WebSheetDefID) {
			try {
				Logger.Information("In SaveFieldValuesInDB"+" IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				ODWebServiceEntities db=new ODWebServiceEntities();
				var SheetDefObj=db.webforms_sheetdef.Where(sd=>sd.WebSheetDefID==WebSheetDefID && sd.webforms_preference.DentalOfficeID==DentalOfficeID).First();
				webforms_sheet NewSheetObj=new webforms_sheet();
				NewSheetObj.DateTimeSheet=DateTime.Now;
				NewSheetObj.Height=SheetDefObj.Height;
				NewSheetObj.Width=SheetDefObj.Width;
				NewSheetObj.FontName=SheetDefObj.FontName;
				NewSheetObj.FontSize=SheetDefObj.FontSize;
				NewSheetObj.SheetType=SheetDefObj.SheetType;
				NewSheetObj.Description=SheetDefObj.Description;
				NewSheetObj.IsLandscape=SheetDefObj.IsLandscape;
				SheetDefObj.webforms_sheetfielddef.Load();
				var SheetFieldDefResult=SheetDefObj.webforms_sheetfielddef;
				//copy sheetfielddef values to sheetfield.The FieldValue, if any is overwritten from the hash table.
				for(int i=0; i<SheetFieldDefResult.Count();i++) {
					webforms_sheetfield NewSheetfieldObj=new webforms_sheetfield();
					var SheetFieldDefObj=SheetFieldDefResult.ElementAt(i);
					NewSheetfieldObj.FieldName=SheetFieldDefObj.FieldName;
					NewSheetfieldObj.FieldType=SheetFieldDefObj.FieldType;
					NewSheetfieldObj.FontIsBold=SheetFieldDefObj.FontIsBold;
					NewSheetfieldObj.FontName=SheetFieldDefObj.FontName;
					NewSheetfieldObj.FontSize=SheetFieldDefObj.FontSize;
					NewSheetfieldObj.Height=SheetFieldDefObj.Height;
					NewSheetfieldObj.Width=SheetFieldDefObj.Width;
					NewSheetfieldObj.XPos=SheetFieldDefObj.XPos;
					NewSheetfieldObj.YPos=SheetFieldDefObj.YPos;
					NewSheetfieldObj.IsRequired=SheetFieldDefObj.IsRequired;
					NewSheetfieldObj.RadioButtonGroup=SheetFieldDefObj.RadioButtonGroup;
					NewSheetfieldObj.RadioButtonValue=SheetFieldDefObj.RadioButtonValue;
					NewSheetfieldObj.GrowthBehavior=SheetFieldDefObj.GrowthBehavior;
					NewSheetfieldObj.FieldValue=SheetFieldDefObj.FieldValue;
					long WebSheetFieldDefID=SheetFieldDefObj.WebSheetFieldDefID;
					if(FormValuesHashTable.ContainsKey(WebSheetFieldDefID+"")) {
						NewSheetfieldObj.FieldValue=FormValuesHashTable[WebSheetFieldDefID+""].ToString();
					}
					string FieldValue=SheetFieldDefObj.FieldValue; 
					if(FieldValue.Contains("[dateToday]")) {
						FieldValue=FieldValue.Replace("[dateToday]",DateTime.Today.ToString("M/d/yyyy"));
						NewSheetfieldObj.FieldValue=FieldValue;
					}
					NewSheetObj.webforms_sheetfield.Add(NewSheetfieldObj);
				}
				var PrefObj=db.webforms_preference.Where(wp=>wp.DentalOfficeID==DentalOfficeID);
				if(PrefObj.Count()>0) {
					PrefObj.First().webforms_sheet.Add(NewSheetObj);
					db.SaveChanges();
					DisplayMessage("Your details have been successfully submitted");
					Logger.Information("Form values saved from IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				}

			}
			catch(Exception ex) {
				Logger.LogError(ex);
				Panel1.Visible=false;
				DisplayMessage("There has been a problem submitting your details. <br /> We apologize for the inconvenience.");
				Logger.Information("There has been a problem submitting your details IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
		}

		private void DisplayMessage(String Message) {
			LabelSubmitMessage.Text=Message;
			Panel1.Visible=false;
			// the form is reduced to size zero and the Panel2 is opened up. This is done because even when panels/forms are invisible only their controls are invisible. The pane is still shown on the web page
			form1.Style["width"]="0px";
			form1.Style["height"]="0px";
			Panel2.Width=Unit.Pixel(680);
			Panel2.Height=Unit.Pixel(300);
			Panel2.Visible=true;
		}

		protected void Button1_Click(object sender,EventArgs e) {
			LoopThroughControls(this.Page);
			SaveFieldValuesInDB(DentalOfficeID,WebSheetDefID);
		}





	}
}
