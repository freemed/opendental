using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace WebForms
{
	public partial class WebForm1:System.Web.UI.Page
	{
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
	}
}
