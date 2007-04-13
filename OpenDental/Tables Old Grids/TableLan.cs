using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TableLan : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableLan(){
			InitializeComponent();
			MaxRows=50;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,49,14);
			Fields[0]="English";
			Fields[1]="Eng Comments";
			Fields[2]="Language";
			Fields[3]="Language Comments";
			//Fields[4]="Culture";

			ColWidth[0]=210;
			ColWidth[1]=210;
			ColWidth[2]=210;
			ColWidth[3]=210;
			//ColWidth[4]=200;

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
			// 
			// TableLan
			// 
			this.Name = "TableLan";
			this.Load += new System.EventHandler(this.TableLan_Load);

		}
		#endregion

		private void TableLan_Load(object sender, System.EventArgs e) {
			LayoutTables();		
		}

	}
}

