using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental
{
	///<summary></summary>
	public class TableProcSelect : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableProcSelect(){InitializeComponent();
			MaxRows=5;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,4,14);
			//Heading=Lan.g("TableProcSelect","Patient Payments");
			Fields[0]=Lan.g("TableProcSelect","Date");
			Fields[1]=Lan.g("TableProcSelect","Prov");
			Fields[2]=Lan.g("TableProcSelect","Code");
			Fields[3]=Lan.g("TableProcSelect","Tooth");
			Fields[4]=Lan.g("TableProcSelect","Description");
			Fields[5]=Lan.g("TableProcSelect","Fee");
			//ColAlign[0]=HorizontalAlignment.Center;
			ColAlign[5]=HorizontalAlignment.Right;
			ColWidth[0]=70;
			ColWidth[1]=55;
			ColWidth[2]=55;
			ColWidth[3]=50;
			ColWidth[4]=250;
			ColWidth[5]=60;
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
			// TableProcSelect
			// 
			this.Name = "TableProcSelect";
			this.Load += new System.EventHandler(this.TableProcSelect_Load);

		}
		#endregion

		private void TableProcSelect_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}
	}
}

