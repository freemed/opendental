using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TableAutoItem : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableAutoItem(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=3;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TableAutoItem","Code");
			Fields[1]=Lan.g("TableAutoItem","Description");
			Fields[2]=Lan.g("TableAutoItem","Conditions");
			ColWidth[0]=100;
			ColWidth[1]=200;
			ColWidth[2]=400;
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
			// TableAutoItem
			// 
			this.Name = "TableAutoItem";
			this.Load += new System.EventHandler(this.TableAutoItem_Load);

		}
		#endregion

		private void TableAutoItem_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}
	}
}

