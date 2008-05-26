using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental
{
	///<summary></summary>
	public class TableProcPay : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableProcPay(){
			InitializeComponent();
			MaxRows=5;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,4,14);
			Heading=Lan.g("TableProcPay","Patient Payments");
			Fields[0]=Lan.g("TableProcPay","Entry Date");
			//Fields[1]=Lan.g("TableProcPay","Prov");
			Fields[1]=Lan.g("TableProcPay","Amount");
			Fields[2]=Lan.g("TableProcPay","Tot Amt");
			Fields[3]=Lan.g("TableProcPay","Note");
			ColAlign[0]=HorizontalAlignment.Center;
			ColAlign[1]=HorizontalAlignment.Right;
			ColAlign[2]=HorizontalAlignment.Right;
			ColWidth[0]=70;
			//ColWidth[1]=55;
			ColWidth[1]=55;
			ColWidth[2]=55;
			ColWidth[3]=250;
			DefaultGridColor=Color.LightGray;
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
			// TableProcPay
			// 
			this.Name = "TableProcPay";
			this.Load += new System.EventHandler(this.TableProcPay_Load);

		}
		#endregion

		private void TableProcPay_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}
	}
}

