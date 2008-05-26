using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TablePermissions : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TablePermissions(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TablePermissions","Name");
			Fields[1]=Lan.g("TablePermissions","Require Password");
			Fields[2]=Lan.g("TablePermissions","Before Date");
			Fields[3]=Lan.g("TablePermissions","Before Days");
			ColWidth[0]=200;
			ColWidth[1]=110;
			ColWidth[2]=100;
			ColWidth[3]=100;
			ColAlign[1]=HorizontalAlignment.Center;
			ColAlign[2]=HorizontalAlignment.Center;
			ColAlign[3]=HorizontalAlignment.Center;
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
			// TablePermissions
			// 
			this.Name = "TablePermissions";
			this.Load += new System.EventHandler(this.TablePermissions_Load);

		}
		#endregion

		private void TablePermissions_Load(object sender, System.EventArgs e) {
		  LayoutTables();	
		}
	}
}

