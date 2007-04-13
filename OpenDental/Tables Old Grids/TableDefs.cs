using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableDefs : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableDefs(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=24;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,23,14);
			ColWidth[0]=180;
			ColWidth[1]=170;
			ColWidth[2]=50;
			ColWidth[3]=40;
			Fields[0]=Lan.g("TableDefs","Name");
			Fields[1]=Lan.g("TableDefs","Value");
			Fields[2]=Lan.g("TableDefs","Color");
			Fields[3]=Lan.g("TableDefs","Hide");
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
			// TableDefs
			// 
			this.Name = "TableDefs";
			this.Load += new System.EventHandler(this.TableDefs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableDefs_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}
	}
}

