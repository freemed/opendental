using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TablePercent : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TablePercent(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=6;
			MaxCols=2;
			ShowScroll=true;
			FieldsArePresent=false;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,5,14);
			Heading=Lan.g("TablePercent","Coverage Spans");
			ColWidth[0]=120;
			ColWidth[1]=132;
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
			// TablePercent
			// 
			this.Name = "TablePercent";
			this.Load += new System.EventHandler(this.TablePercent_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TablePercent_Load(object sender, System.EventArgs e) {

		}
	}
}

