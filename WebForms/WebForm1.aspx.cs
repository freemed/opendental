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

		protected void Page_Load(object sender,EventArgs e)
		{
			SetPagePreferences(3);
		}

		private void SetPagePreferences(int CustomerId)
		{
			string sqlquery = "SELECT ColorCode,ImageName  FROM cutomerpreferences WHERE CustomerId =" + CustomerId;
			// DataSet ds = RunQuery(sqlquery);
			int ColorCode = 16777215; // thi is the code for white
				
			/*
						if (ds != null && ds.Tables[0].Rows.Count > 0)
						{
							ColorCode = Convert.ToInt32(ds.Tables[0].Rows[0]["ColorCode"].ToString());
							ImageName = ds.Tables[0].Rows[0]["ImageName"].ToString();
						}
						*/

			//ImageLogo.ImageUrl = "images/" + ImageName;

			Panel1.BackColor = Color.FromArgb(ColorCode);
			
			


		}
		protected void Submit_Click(object sender,EventArgs e)
		{


			ExtractFieldValues(this.Page); 


		}

		private void ExtractFieldValues(Page p)
		{
			foreach(Control c in p.Controls)
			{
				if(c.HasControls())
				{
					ExtractKeyValue(c);
					FindControls(c);
				}
				else
				{
					ExtractKeyValue(c);
				}
			}
		}
		private void FindControls(Control c)
		{
			foreach(Control ctl in c.Controls)
			{
				if(ctl.HasControls())
				{
					ExtractKeyValue(ctl);
					FindControls(ctl);
				}
				else
				{
					ExtractKeyValue(ctl);
				}
			}
		}
		private void ExtractKeyValue(Control c)
		{


			if(c.GetType() == typeof(TextBox))
			{
				TextBox tbox = ((TextBox)c);
				FormValuesHashTable.Add(tbox.ID,((TextBox)(tbox)).Text.Trim());
			}

			if(c.GetType() == typeof(RadioButtonList))
			{
				RadioButtonList rbl = ((RadioButtonList)c);
				FormValuesHashTable.Add(rbl.ID,((RadioButtonList)(rbl)).SelectedValue);
				
				
			}

			if(c.GetType() == typeof(CheckBox))
			{
				CheckBox cbox = ((CheckBox)c);
				FormValuesHashTable.Add(cbox.ID,((CheckBox)(cbox)).Checked.ToString());
			}

			

		}

		private void SaveFieldvaluesInDB()
		{
		}
}
}
