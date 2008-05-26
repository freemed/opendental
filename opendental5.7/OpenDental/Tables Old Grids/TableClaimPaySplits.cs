using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableClaimPaySplits : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableClaimPaySplits(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=10;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,9,14);
			Heading=Lan.g("TableClaimPaySplits","Claim Payment Splits");
			Fields[0]=Lan.g("TableClaimPaySplits","Date");
			Fields[1]=Lan.g("TableClaimPaySplits","Prov");
			Fields[2]=Lan.g("TableClaimPaySplits","Patient");
			Fields[3]=Lan.g("TableClaimPaySplits","Carrier");
			Fields[4]=Lan.g("TableClaimPaySplits","Fee");
			Fields[5]=Lan.g("TableClaimPaySplits","Payment");
			ColAlign[4]=HorizontalAlignment.Right;
			ColAlign[5]=HorizontalAlignment.Right;
			ColWidth[0]=70;
			ColWidth[1]=40;
			ColWidth[2]=140;
			ColWidth[3]=140;
			ColWidth[4]=65;
			ColWidth[5]=65;
			LayoutTables();	// This call is required by the Windows Form Designer.
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
			// TableClaimPaySplits
			// 
			this.Name = "TableClaimPaySplits";
			this.Load += new System.EventHandler(this.TableClaimPaySplits_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableClaimPaySplits_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

