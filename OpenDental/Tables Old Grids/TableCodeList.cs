using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableCodeList : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;
		///<summary></summary>
		public static int rowHeight=14;

		///<summary></summary>
		public TableCodeList(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=10;
			MaxCols=3;
			ShowScroll=false;
			FieldsArePresent=false;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,9,14);
			ColAlign[1]=HorizontalAlignment.Right;
			ColWidth[0]=90;
			ColWidth[1]=50;
			ColWidth[2]=45;
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
			// TableCodeList
			// 
			this.Name = "TableCodeList";
			this.Click += new System.EventHandler(this.TableCodeList_Click);
			this.Load += new System.EventHandler(this.TableCodeList_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableCodeList_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

		private void TableCodeList_Click(object sender, System.EventArgs e) {

		}
	}
}

