using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;


namespace WebForms
{
	public partial class WebForm1:System.Web.UI.Page
	{
		private Hashtable FormValuesHashTable = new Hashtable();
		private int DentalOfficeID = 0;

		protected void Page_Load(object sender,EventArgs e)
		{	
			try
			{				
			int DentalOfficeID = 0;
				if(Request["DentalOfficeID"] != null)
				{					DentalOfficeID = Convert.ToInt32(Request["DentalOfficeID"].ToString().Trim());
					Int32.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
				}
			//hard code for testing only
				DentalOfficeID=1;
				SetPagePreferences(DentalOfficeID);
			}
			catch(Exception ex)
			{
				Logger.Information(ex.Message.ToString());
			}
		}

		/// <summary>
		/// sets page specifics like colour etc based on the Dental office preferences form the db
		/// </summary>
		/// <param name="DentalOfficeID"></param>
		private void SetPagePreferences(int DentalOfficeID)
		{
			try
			{
				ODWebServiceEntities db = new ODWebServiceEntities();
				int ColorCode = 16777215; // this is the code for white
				string Heading1 = "";
				string Heading2= "";
				var PrefObj = from wp in db.webforms_preference where wp.DentalOfficeID==DentalOfficeID
							  select wp;
				if(PrefObj.Count() > 0)
				{
					ColorCode =PrefObj.First().ColorBorder;
					Heading1 = PrefObj.First().Heading1;
					Heading2= PrefObj.First().Heading2;
				}
				LabelHeading1.Text=Heading1;
				LabelHeading2.Text =Heading2;
				Panel1.BackColor = Color.FromArgb(ColorCode);
			}
			catch(Exception ex)
			{
				Logger.Information(ex.Message.ToString());
			}

		}
		
		protected void Submit_Click(object sender,EventArgs e)
		{
			DentalOfficeID=1;
			LoopThroughControls(this.Page);
			SaveFieldValuesInDB(DentalOfficeID);
		}

		private void LoopThroughControls(Page p)
		{
			try
			{
				foreach(Control c in p.Controls)
				{
					if(c.HasControls())
					{
						ExtractValue(c);
						FindControls(c);
					}
					else
					{
						ExtractValue(c);
					}
				}
			}
			catch(Exception ex)
			{
				Logger.Information(ex.Message.ToString());
			}
		}

		private void FindControls(Control c)
		{
			try
			{
				foreach(Control ctl in c.Controls)
				{
					if(ctl.HasControls())
					{
						ExtractValue(ctl);
						FindControls(ctl);
					}
					else
					{
						ExtractValue(ctl);
					}
				}
			}
			catch(Exception ex)
			{
				Logger.Information(ex.Message.ToString());
			}
		}

		/// <summary>
		/// Fill the FormValuesHashTable here.
		/// </summary>
		/// <param name="c"></param>
		private void ExtractValue(Control c)
		{

			try
			{
				if(c.GetType() == typeof(TextBox))
				{
					TextBox tbox = ((TextBox)c);
					if(tbox.Text.Trim()!="")
					{
						FormValuesHashTable.Add(tbox.ID,tbox.Text.Trim());
					}
				}

				if(c.GetType() == typeof(RadioButtonList))
				{
					RadioButtonList rbl = ((RadioButtonList)c);
					if(rbl.SelectedIndex!=-1)
					{
						FormValuesHashTable.Add(rbl.ID,rbl.SelectedValue);
					}
				}

				if(c.GetType() == typeof(CheckBox))
				{
					CheckBox cbox = ((CheckBox)c);
					if(cbox.Checked == true)
					{
						FormValuesHashTable.Add(cbox.ID,cbox.Checked.ToString());
					}
				}

			}
			catch(Exception ex)
			{
				Logger.Information(ex.Message.ToString());
			}

		}

		private void SaveFieldValuesInDB(int DentalOfficeID)
		{
			try
			{
				ODWebServiceEntities db = new ODWebServiceEntities();
				webforms_sheet NewSheetObj = new webforms_sheet();
				var PrefObj = from wp in db.webforms_preference where wp.DentalOfficeID==DentalOfficeID
							  select wp;
				NewSheetObj.DateTimeSubmitted= DateTime.Now;
				foreach(string key in FormValuesHashTable.Keys)
				{
					webforms_sheetfield NewSheetfieldObj = new webforms_sheetfield();
					NewSheetfieldObj.FieldName=key;
					NewSheetfieldObj.FieldValue=FormValuesHashTable[key].ToString();
					NewSheetObj.webforms_sheetfield.Add(NewSheetfieldObj);
				}
				if(PrefObj.Count() > 0)
				{
					PrefObj.First().webforms_sheet.Add(NewSheetObj);
				}
			}
			catch(Exception ex)
			{
				Logger.Information(ex.Message.ToString());
			}
		}
	}
}
