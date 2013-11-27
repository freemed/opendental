///Dennis Mathew: For using ADO.NET Entity Data Model/LINQ with Mysql/Visual Studio 2010, download and install Connector/Net from http://dev.mysql.com/downloads/connector/net/ 
/// Connector/Net is a ADO.NET driver for MySQL.
/// The web server which hosts the webservice will also need this install.
/// The integration with Visual Studio can be flakey. So a few cycles of install/uninstall/restart may be needed. I've also tried the non-install options of adding dlls but they don't seem to work in the few attempts that I made.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using OpenDentBusiness;

namespace WebForms {
	/// <summary>
	/// Displays the Open Dental sheets on a web page.
	/// </summary>
	public partial class Sheets:System.Web.UI.Page {
		private long DentalOfficeID=0;
		private long WebSheetDefID=0;
		private Hashtable FormValuesHashTable=new Hashtable();
		private Hashtable hiddenChkBoxGroupHashTable=new Hashtable();
		private List<long> dateTodayList=new List<long>();
		private List<WControl> listwc=new List<WControl>();
		private bool doTabOrder=true;
		private string ReturnURL="";//url the web forms will return to when all forms are complete
		private string ButtonText="";
		
		protected void Page_Load(object sender,EventArgs e) {
			try {
				if(Request["DentalOfficeID"]!=null) {
					Int64.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
				}
				if(Request["WebSheetDefID"]!=null) {
					Int64.TryParse(Request["WebSheetDefID"].ToString().Trim(),out WebSheetDefID);
				}
				if(Request["ReturnURL"]!=null) {
					ReturnURL=Request["ReturnURL"].ToString().Trim();
				}
				if(Request["ButtonText"]!=null) {
					ButtonText=Request["ButtonText"].ToString().Trim();
					Button1.Text=ButtonText;
				}
				Logger.Information("Page requested from IpAddress="+HttpContext.Current.Request.UserHostAddress+" for  DentalOfficeID="+DentalOfficeID);
				Panel2.Visible=true;
				GeneratePage(DentalOfficeID,WebSheetDefID);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID+" WebSheetDefID="+WebSheetDefID,ex);
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
					int SubmitButtonWidth=70;
					int SubmitButtonYoffset=10;
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
						//bool fontIsBold=SheetFieldDefList.ElementAt(j).FontIsBold==(sbyte)1?true:false;
						bool fontIsBold;
						if(SheetFieldDefList.ElementAt(j).FontIsBold==(sbyte)1) {
							fontIsBold=true;
						}
						else {
							fontIsBold=false;
						}
						short TabOrder=(short)SheetFieldDefList.ElementAt(j).TabOrder;
						if(TabOrder!=0) {
							doTabOrder=false;
						}
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
								dateTodayList.Add(WebSheetFieldDefID);// the replacing is done at the client side using javascript via a hidden variable.
							}
							lb.Text=FieldValue.Replace(Environment.NewLine,"<br />").Replace("\n","<br />"); //it appears that the text contains only "\n" as the newline character and not Environment.NewLine (i.e "\n\r") as the line break, so the code takes into account both cases.
							wc=lb;
						}
						if(FieldType==SheetFieldType.Image||FieldType==SheetFieldType.Rectangle||FieldType==SheetFieldType.Line) {
							System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
							img.ImageUrl=("~/Handler1.ashx?WebSheetFieldDefID="+WebSheetFieldDefID);
							wc=img;
							if(width==0 && height==0) {
								wc=null;//Image won't be visible anyway so don't waste time trying to draw it.
							}
							else if((FieldType==SheetFieldType.Image || FieldType==SheetFieldType.Rectangle) && (width==0 || height==0)) {
								wc=null;//Image with a width OR a height of 0 will cause an error.  Also, rectangles are stopped from having widths and heights of 0 within OD.
							}
							else if(FieldType==SheetFieldType.Line) {
								//Horizontal and vertical lines may have a height or a width of zero.  To show up on a web page, the image that the line is drawn on needs to have some sort of a width or height.
								if(width==0) {
									width+=4;//Increases the width of the "canvas" that the image of the line will be drawn on.  Handler1.ashx.cs is where the actual line image itself is created.
								}
								if(height==0) {
									height+=4;//Increases the height of the "canvas" that the image of the line will be drawn on.  Handler1.ashx.cs is where the actual line image itself is created.
								}
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
							lb.Text="Signature will be recorded later";
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
							wc.TabIndex=TabOrder;
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
								AddTextBoxValidator(SheetFieldDefList.ElementAt(j));
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
								AddRequiredChkBoxValidator(SheetFieldDefList.ElementAt(j),CheckBoxXOffset,CheckBoxYOffset);
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
					}//for loop end here
					AdjustErrorMessageForChkBoxes();	
					CreateChkBoxValidatorsHiddenFields();
					CreateHiddenFieldForDateToday();
					if(doTabOrder) {
					AssignTabOrder();
					}
					//position the submit button at the end of the page.
					Button1.Style["position"]="absolute";
					Button1.Style["left"]=SheetDefWidth/2-(SubmitButtonWidth/2)+"px";
					Button1.Style["top"]=SheetDefHeight+SubmitButtonYoffset+"px";
					Button1.Style["z-index"]=""+ElementZIndex;
					Button1.Width=Unit.Pixel(SubmitButtonWidth);
					Panel3.Style["position"]="absolute";
					Panel3.Style["top"]=FormXOffset+SheetDefHeight+SubmitButtonYoffset+"px";
				}
				catch(ApplicationException ex) {
					Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
					DisplayMessage("Error: Your form is not available. Please contact your Dental Office");
				}
		}

		private WebControl AddCheckBox(webforms_sheetfielddef sfd){
			WebControl wc=null;
			CheckBox cb=new CheckBox();
			cb.ID=""+sfd.WebSheetFieldDefID;
			AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender mecb=new AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender();
			mecb.ID=cb.ID+"MutuallyExclusiveCheckBoxExtender";
			mecb.TargetControlID=cb.ID;
			mecb.Key=GetChkBoxGroupName(sfd);
			Panel1.Controls.Add(mecb);
			wc=cb;
			return wc;
		}

		private string GetChkBoxGroupName(webforms_sheetfielddef sfd) {
			String FieldName=sfd.FieldName;
			String RadioButtonValue=sfd.RadioButtonValue;
			String RadioButtonGroup=sfd.RadioButtonGroup;
			String ChkBoxGroupName=null;
			if(!String.IsNullOrEmpty(RadioButtonGroup) && FieldName=="misc") {
				ChkBoxGroupName=RadioButtonGroup;
			}
			else if(!String.IsNullOrEmpty(RadioButtonValue)) {// cases like gender, position etc that have no value for RadioButtonGroup but have RadioButtonValue
				ChkBoxGroupName=FieldName;
			}
			return ChkBoxGroupName;
		}

		/// <summary>
		/// A single check boxes which are 'required' have a different error message as opposed to a 'required' group of check boxes.
		/// Also the position of the error message is adjusted by reassigning postions of the Textboxes related to each group
		/// </summary>
		private void AdjustErrorMessageForChkBoxes() {
			IEnumerable<CustomValidator> ListCustomValidators=Panel1.Controls.OfType<CustomValidator>();
			foreach(string strkey in hiddenChkBoxGroupHashTable.Keys) {//foreach1
				String Value=(string)hiddenChkBoxGroupHashTable[strkey];
				long ControlID=0;
				Int64.TryParse(Value.Trim(),out ControlID);
				if(ControlID==0) {// this corresponds to a group of checkboxes. Re-positioning of  error messages  is done here. No need to change the error message itself.
					string[] ControlIdArray = Value.Split(new char[] { ' ' });
					int MaxX=0;//this variable will hold the position of the element that is to the extreme right.
					foreach(string id in ControlIdArray) {//foreach2
						var TbResult=Panel1.Controls.OfType<TextBox>().Where(tb => tb.ID=="TextBoxForCheckbox"+id);
						if(TbResult.Count()>0) {
							string StrXpos=TbResult.ElementAt(0).Style["left"];
							StrXpos=StrXpos.Substring(0,StrXpos.IndexOf("px")).Trim();
							int XPos=0;
							Int32.TryParse(StrXpos,out XPos);
							if(XPos>MaxX) {
								MaxX=XPos;
							}
						}
					}// end foreach2
					// now assign the max value to all textboxes of that group.
					foreach(string id in ControlIdArray) {//foreach2
						var TbResult=Panel1.Controls.OfType<TextBox>().Where(tb => tb.ID=="TextBoxForCheckbox"+id);
						if(TbResult.Count()>0) {
							TbResult.ElementAt(0).Style["left"]=MaxX+"px";
						}
					}// end foreach2
				}
				else {// this else corresponds to a single checkbox not part of a group. No re-position of the error message is done here. Only the error message is changed.
					var CvResult =ListCustomValidators.Where(cv => cv.ID=="CustomValidatorTextBoxForCheckbox"+ControlID);
					if(CvResult.Count()>0) {
						CvResult.ElementAt(0).ErrorMessage="This is a required Check Box";
					}
				}
			}// end foreach3
		}

		///<summary>A single Hidden field is created which holds the ids of all dateTodays</summary>
		private void CreateHiddenFieldForDateToday() {
			HiddenField hf= new HiddenField();
			hf.ID="dateToday";
			for(int i=0;i<dateTodayList.Count();i++) {
				hf.Value+=" "+dateTodayList.ElementAt(i);
			}
			Panel1.Controls.Add(hf);
		}

		private void AssignTabOrder() {
			var sortedlist=listwc.OrderBy(wc=>wc.YPos).ThenBy(wc=>wc.XPos).ToList();
			for(short i=0;i<sortedlist.Count();i++){
				sortedlist[i].wc.TabIndex=(short)(i+1);
			}
		}

		private void CreateChkBoxValidatorsHiddenFields() {
			try{
				HiddenField hfAllGroupsList= new HiddenField();
				hfAllGroupsList.ID="hfAllGroupsList";
				foreach(string strkey in hiddenChkBoxGroupHashTable.Keys) {
					HiddenField hf= new HiddenField();
					hf.ID=strkey;
					hf.Value=(string)hiddenChkBoxGroupHashTable[strkey];
					Panel1.Controls.Add(hf);
					hfAllGroupsList.Value+=" "+hf.ID;
				}
				hfAllGroupsList.Value=hfAllGroupsList.Value.Trim();
				Panel1.Controls.Add(hfAllGroupsList);
			}catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID+" WebSheetDefID="+WebSheetDefID,ex);
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

		private void AddTextBoxValidator(webforms_sheetfielddef sfd ) {
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
			rv.ID="RequiredFieldValidator"+rv.ControlToValidate;
			Panel1.Controls.Add(rv);
			//callout extender
			AjaxControlToolkit.ValidatorCalloutExtender vc=new AjaxControlToolkit.ValidatorCalloutExtender();
			vc.TargetControlID=rv.ID;
			vc.ID="ValidatorCalloutExtender"+rv.ID;
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

		private void AddRequiredChkBoxValidator(webforms_sheetfielddef sfd,float CheckBoxXOffset,float CheckBoxYOffset) {
			if(sfd.IsRequired!=(sbyte)1) {
				return;
			}
			int XPosErrorMessageOffset=2;
			int XPos=sfd.XPos;
			int YPos=sfd.YPos;
			String ErrorMessage="This is a required section. Please check one of the Check Boxes";
			//add dummy textbox to get around the limitation of checkboxes not having validators and call outs.
			TextBox tb=new TextBox();
			tb.Rows=1;
			tb.Text=".";// there has to be some character here the least visible is the period.
			tb.MaxLength=1;
			tb.Width=Unit.Pixel(1);
			tb.ID="TextBoxForCheckbox"+sfd.WebSheetFieldDefID;
			tb.Style["position"]="absolute";
			tb.Style["top"]=YPos+CheckBoxYOffset+"px";
			tb.Style["left"]=XPos+CheckBoxXOffset+XPosErrorMessageOffset+ sfd.Width+"px";
			tb.Style["z-index"]="-2";
			tb.ReadOnly=true;
			tb.BorderWidth=Unit.Pixel(0);
			Panel1.Controls.Add(tb);
			CustomValidator cv=new CustomValidator();
			cv.ControlToValidate=tb.ID;
			cv.ErrorMessage=ErrorMessage;
			cv.Display=ValidatorDisplay.None;
			cv.SetFocusOnError=true;
			cv.ID="CustomValidator"+cv.ControlToValidate;
			cv.ClientValidationFunction="CheckCheckBoxes";
			Panel1.Controls.Add(cv);
			//callout extender
			AjaxControlToolkit.ValidatorCalloutExtender vc=new AjaxControlToolkit.ValidatorCalloutExtender();
			vc.TargetControlID=cv.ID;
			vc.ID="ValidatorCalloutExtender"+cv.ID;
			Panel1.Controls.Add(vc);
			AddChkBoxIdsToHashTable(sfd);			
		}

		/// <summary>
		/// All checkboxes that require validation are stored in the format:
		/// key=hiddenChkBoxGroup+(ChkBoxGroupName) and value= "chkbxid1 chkbxid1 chkbxid1"
		/// </summary>
		private void AddChkBoxIdsToHashTable(webforms_sheetfielddef sfd) {
			String ChkBoxGroupName=GetChkBoxGroupName(sfd);
			String Key="hiddenChkBoxGroup"+ChkBoxGroupName;
			String Value=""+sfd.WebSheetFieldDefID;
			if(hiddenChkBoxGroupHashTable.ContainsKey(Key)) {
				hiddenChkBoxGroupHashTable[Key]+=" "+Value;
			}
			else {
				hiddenChkBoxGroupHashTable.Add(Key,Value);
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
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
			}
		}

		/// <summary>
		/// This is a recursive function which searches through nested controls on a  webpage and stores the values in FormValuesHashTable 
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
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
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
						char[] charArr=tbox.Text.Trim().ToCharArray();
						StringBuilder sb=new StringBuilder();
						for(int i=0;i<charArr.Length;i++) {
							if(XmlConvert.IsXmlChar(charArr[i])) {
								sb.Append(charArr[i]);
							}
						}
						FormValuesHashTable.Add(FieldName,sb.ToString());
						//FormValuesHashTable.Add(FieldName,tbox.Text.Trim());
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
				if(c.GetType()==typeof(HiddenField)) {
					HiddenField hf=((HiddenField)c);
					string FieldName=hf.ID;
					if(hf.Value.Trim()!="") {
						FormValuesHashTable.Add(FieldName,hf.Value.Trim());
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
					NewSheetfieldObj.TabOrder=SheetFieldDefObj.TabOrder;
					NewSheetfieldObj.FieldValue=SheetFieldDefObj.FieldValue;
					long WebSheetFieldDefID=SheetFieldDefObj.WebSheetFieldDefID;
					if(FormValuesHashTable.ContainsKey(WebSheetFieldDefID+"")) {
						NewSheetfieldObj.FieldValue=FormValuesHashTable[WebSheetFieldDefID+""].ToString();
					}
					#region  saving dates in right formats
						string FieldValue=NewSheetfieldObj.FieldValue;
						string FieldName=NewSheetfieldObj.FieldName;
						string CultureName=db.webforms_preference.Where(pref=>pref.DentalOfficeID==DentalOfficeID).First().CultureName;// culture of the opendental installation
						if(String.IsNullOrEmpty(CultureName)) {
							CultureName="en-US";
						}
						if(FieldValue.Contains("[dateToday]")) {
							FieldValue=FieldValue.Replace("[dateToday]",ExtractBrowserDate().ToString("d",new CultureInfo(CultureName,false)));
							Logger.Information("FieldName="+FieldName+" FieldValue="+FieldValue);
							NewSheetfieldObj.FieldValue=FieldValue;
						}
						if(FieldName.ToLower()=="birthdate" || FieldName.ToLower()=="bdate") {
							Logger.Information("FieldName="+FieldName+" FieldValue="+FieldValue);
							DateTime birthdate=DateTime.Parse(FieldValue,System.Threading.Thread.CurrentThread.CurrentCulture);//use the browsers culture to get correct date.
							FieldValue= birthdate.ToString("d",new CultureInfo(CultureName,false));//now convert the birthdate into a string using the culture of the corresponding opendental installation.
							NewSheetfieldObj.FieldValue=FieldValue;
						}
					#endregion
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
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID+" WebSheetDefID="+WebSheetDefID,ex);
				Panel1.Visible=false;
				DisplayMessage("There has been a problem submitting your details. <br /> We apologize for the inconvenience.");
			}
		}

		///<summary> The browser date is extracted via cookies set by the browser</summary>
		private DateTime ExtractBrowserDate() {
			DateTime BrowserDateToday=DateTime.Today;
			try {
				if(Request.Cookies["DateCookieY"] != null && Request.Cookies["DateCookieM"] != null &&Request.Cookies["DateCookieD"] != null) {
					int y=0;
					int m=0;
					int d=0;
					int.TryParse(Request.Cookies["DateCookieY"].Value,out y) ;
					int.TryParse(Request.Cookies["DateCookieM"].Value,out m) ;
					int.TryParse(Request.Cookies["DateCookieD"].Value,out d) ;
					BrowserDateToday= new DateTime(y,m,d);
				}
			}
			catch(Exception ex) {
				//default to todays date
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
			}
			return BrowserDateToday;
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
			LoopThroughControls(this.Page);// Fills FormValuesHashTable here
			SaveFieldValuesInDB(DentalOfficeID,WebSheetDefID);
			if(ReturnURL!=null && ReturnURL!="") {//user has added a return url (there may or may not be NextFormIDs added to the URL to loop through before navigating to ReturnURL)
				BuildURL_Redirect();
			}
		}

		///<summary>Used to rebuild the url for the next form in the query string sequence. Example URL for 4 forms: https://opendentalsoft.com/WebForms/Sheets.aspx?DentalOfficeID=8526&WebSheetDefID=4321&ButtonText=Next&NextFormID=4322&NextFormID=4323&NextFormID=4324&ReturnURL=http://www.afiniadental.com/ </summary>
		private void BuildURL_Redirect() {
			if(Request["NextFormID"]==null) {//if there was a ReturnURL parameter, but no NextFormIDs, head back to homepage
				Response.Redirect(ReturnURL);
				return;
			}
			string[] nextFormIDs=Request.QueryString.GetValues("NextFormID");
			//build new Base URL
			string newURL="https://opendentalsoft.com/WebForms/Sheets.aspx?DentalOfficeID="+DentalOfficeID+"&WebSheetDefID="+nextFormIDs[0]+"&ButtonText=";
			if(ButtonText=="") {//There was no ButtonText parameter or it was not set, default to 'Submit'
				newURL+="Submit";
			}
			else {
				newURL+=ButtonText;
			}
			for(int i=1;i<nextFormIDs.Length;i++) {//position 0 in the array is now the WebSheetDefID, start at position 1
				newURL+="&NextFormID="+nextFormIDs[i];
			}
			newURL+="&ReturnURL="+ReturnURL;
			Response.Redirect(newURL);
		}
		

		
	}
}
