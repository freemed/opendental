using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;

namespace WebForms {
	public partial class WebForm1:System.Web.UI.Page {
		private Hashtable FormValuesHashTable=new Hashtable();
		private int DentalOfficeID=0;

		protected void Page_Load(object sender,EventArgs e) {
			try {if(Request["DentalOfficeID"]!=null) {
					Int32.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
				}
				SetPagePreferences(DentalOfficeID);
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
		}

		/// <summary>
		/// sets page specifics like colour etc based on the Dental office preferences form the db
		/// </summary>
		private void SetPagePreferences(int DentalOfficeID) {
			try {
				ODWebServiceEntities db=new ODWebServiceEntities();
				int ColorCode=3896686; // this is the Color Code for the default OpenDental color
				string Heading1="";
				string Heading2= "";
				var PrefObj=from wp in db.webforms_preference where wp.DentalOfficeID==DentalOfficeID
							  select wp;
				if(PrefObj.Count() > 0) {
					ColorCode =PrefObj.First().ColorBorder;
					Heading1=PrefObj.First().Heading1;
					Heading2= PrefObj.First().Heading2;
				}
				LabelHeading1.Text=Heading1;
				LabelHeading2.Text =Heading2;
				bodytag.Attributes.Add("bgcolor",ColorTranslator.ToHtml(Color.FromArgb(ColorCode)));
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}

		}

		protected void Submit_Click(object sender,EventArgs e) {
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
				Logger.Information(ex.Message.ToString());
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
					}else {
						ExtractValue(ctl);
					}
				}
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
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
						string FieldName=tbox.ID.Remove(0,"TextBox".Length);
						FormValuesHashTable.Add(FieldName,tbox.Text.Trim());
					}
				}
				if(c.GetType()==typeof(RadioButtonList)) {
					RadioButtonList rbl=((RadioButtonList)c); 
					string FieldName=rbl.ID.Remove(0,"RadioButtonList".Length);
					if(rbl.SelectedIndex!=-1) {
						FormValuesHashTable.Add(FieldName,rbl.SelectedValue);
					}
				}
				if(c.GetType()==typeof(CheckBox)) {
					CheckBox cbox=((CheckBox)c);
					string FieldName=cbox.ID.Remove(0,"CheckBox".Length);
					if(cbox.Checked==true) {
						FormValuesHashTable.Add(FieldName,cbox.Checked.ToString());
					}
				}
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
		}

		private void SaveFieldValuesInDB(int DentalOfficeID) {
			try {
				ODWebServiceEntities db=new ODWebServiceEntities();
				webforms_sheet NewSheetObj=new webforms_sheet();
				var PrefObj=from wp in db.webforms_preference where wp.DentalOfficeID==DentalOfficeID
							  select wp;
				NewSheetObj.DateTimeSubmitted= DateTime.Now;
				foreach(string key in FormValuesHashTable.Keys) {
					webforms_sheetfield NewSheetfieldObj=new webforms_sheetfield();
					NewSheetfieldObj.FieldName=key;
					NewSheetfieldObj.FieldValue=FormValuesHashTable[key].ToString();
					NewSheetObj.webforms_sheetfield.Add(NewSheetfieldObj);
				}
				if(PrefObj.Count() > 0) {
					PrefObj.First().webforms_sheet.Add(NewSheetObj);
				}
				db.SaveChanges();
				Panel1.Visible=false;
				LabelSubmitMessage.Text="Your details have been successfully submited";
				Panel2.Visible=true;
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
				Panel1.Visible=false;
				LabelSubmitMessage.Text="There has been a problem submitting your details. <br /> Please contact us at the phone number mentioned on the opendental website";
			}
		}
	}
}
