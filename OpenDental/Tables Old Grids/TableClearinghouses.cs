using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class TableClearinghouses : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableClearinghouses()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			MaxRows=10;
			MaxCols=5;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,9,14);
			Heading=Lan.g("TableClearinghouses","Clearinghouses");
			Fields[0]=Lan.g("TableClearinghouses","Description");
			Fields[1]=Lan.g("TableClearinghouses","Export Path");
			Fields[2]=Lan.g("TableClearinghouses","Format");
			Fields[3]=Lan.g("TableClearinghouses","Is Default");
			Fields[4]=Lan.g("TableClearinghouses","Payors");
			ColWidth[0]=150;
			ColWidth[1]=230;
			ColWidth[2]=110;
			ColWidth[3]=60;
			ColWidth[4]=310;
			LayoutTables();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TableClearinghouses
			// 
			this.Name = "TableClearinghouses";
			this.Load += new System.EventHandler(this.TableClearinghouses_Load);

		}
		#endregion

		private void TableClearinghouses_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

