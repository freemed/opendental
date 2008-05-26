using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class TableClaimProc : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableClaimProc(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=13;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableClaimProc","Procedures");
			Fields[0]=Lan.g("TableClaimProc","Date");
			Fields[1]=Lan.g("TableClaimProc","Prov");
			Fields[2]=Lan.g("TableClaimProc","Code");
			Fields[3]=Lan.g("TableClaimProc","Tth");
			Fields[4]=Lan.g("TableClaimProc","Description");
			Fields[5]=Lan.g("TableClaimProc","Fee Billed");
			Fields[6]=Lan.g("TableClaimProc","Deduct");
			Fields[7]=Lan.g("TableClaimProc","Ins Est");
			Fields[8]=Lan.g("TableClaimProc","Ins Pay");
			Fields[9]=Lan.g("TableClaimProc","WriteOff");
			Fields[10]=Lan.g("TableClaimProc","Status");
			Fields[11]=Lan.g("TableClaimProc","Pmt");
			Fields[12]=Lan.g("TableClaimProc","Remarks");
			ColAlign[5]=HorizontalAlignment.Right;
			ColAlign[6]=HorizontalAlignment.Right;
			ColAlign[7]=HorizontalAlignment.Right;
			ColAlign[8]=HorizontalAlignment.Right;
			ColAlign[9]=HorizontalAlignment.Right;
			ColAlign[10]=HorizontalAlignment.Center;
			ColAlign[11]=HorizontalAlignment.Center;
			ColWidth[0]=70;
			ColWidth[1]=50;
			ColWidth[2]=50;
			ColWidth[3]=35;
			ColWidth[4]=130;
			ColWidth[5]=65;
			ColWidth[6]=65;
			ColWidth[7]=65;
			ColWidth[8]=65;
			ColWidth[9]=65;
			ColWidth[10]=50;
			ColWidth[11]=40;
			ColWidth[12]=170;

			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code

		private void InitializeComponent(){
			this.SuspendLayout();
			// 
			// TableClaimProc
			// 
			this.Name = "TableClaimProc";
			this.Load += new System.EventHandler(this.TableClaimProc_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableClaimProc_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

