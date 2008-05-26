using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class TableProcIns : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableProcIns(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=13;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableProcIns","Insurance Estimates and Payments");
			Fields[0]=Lan.g("TableProcIns","Ins Plan");
			Fields[1]=Lan.g("TableProcIns","Pri/Sec");
			Fields[2]=Lan.g("TableProcIns","Status");
			Fields[3]=Lan.g("TableProcIns","NoBill");
			Fields[4]=Lan.g("TableProcIns","Percent");
			Fields[5]=Lan.g("TableProcIns","Copay");
			Fields[6]=Lan.g("TableProcIns","BaseEst");
			Fields[7]=Lan.g("TableProcIns","Override");
			Fields[8]=Lan.g("TableProcIns","Deduct");
			Fields[9]=Lan.g("TableProcIns","Ins Est");
			Fields[10]=Lan.g("TableProcIns","Ins Pay");
			Fields[11]=Lan.g("TableProcIns","WriteOff");
			Fields[12]=Lan.g("TableProcIns","Remarks");
			ColAlign[1]=HorizontalAlignment.Center;
			ColAlign[2]=HorizontalAlignment.Center;
			ColAlign[3]=HorizontalAlignment.Center;
			ColAlign[4]=HorizontalAlignment.Center;
			ColAlign[5]=HorizontalAlignment.Right;
			ColAlign[6]=HorizontalAlignment.Right;
			ColAlign[7]=HorizontalAlignment.Right;
			ColAlign[8]=HorizontalAlignment.Right;
			ColAlign[9]=HorizontalAlignment.Right;
			ColAlign[10]=HorizontalAlignment.Right;
			ColAlign[11]=HorizontalAlignment.Right;
			ColWidth[0]=190;
			ColWidth[1]=50;
			ColWidth[2]=50;
			ColWidth[3]=45;
			ColWidth[4]=55;
			ColWidth[5]=55;
			ColWidth[6]=55;
			ColWidth[7]=55;
			ColWidth[8]=55;
			ColWidth[9]=55;
			ColWidth[10]=55;
			ColWidth[11]=55;
			ColWidth[12]=165;
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
			// TableProcIns
			// 
			this.Name = "TableProcIns";
			this.Load += new System.EventHandler(this.TableProcIns_Load);

		}
		#endregion

		private void TableProcIns_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}










	}
}

