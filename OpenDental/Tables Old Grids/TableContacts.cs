using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableContacts : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableContacts(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=5;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TableContacts","Last Name");
			Fields[1]=Lan.g("TableContacts","First Name");
			Fields[2]=Lan.g("TableContacts","Wk Phone");
			Fields[3]=Lan.g("TableContacts","Fax");
			Fields[4]=Lan.g("TableContacts","Notes");
			ColWidth[0]=120;
			ColWidth[1]=100;
			ColWidth[2]=90;
			ColWidth[3]=90;
			ColWidth[4]=250;
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
			// TableContacts
			// 
			this.Name = "TableContacts";
			this.Load += new System.EventHandler(this.TableContacts_Load);

		}
		#endregion

		private void TableContacts_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}








