using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableFamily : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableFamily(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			InstantClasses();
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
			// TableFamily
			// 
			this.Name = "TableFamily";
			this.Load += new System.EventHandler(this.TableFamily_Load);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			MaxRows=4;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,3,14);
			SetGridColor(Color.LightGray);
			SetBackGColor(Color.White);
			Heading=Lan.g("TableFamily","Family Members");
			Fields[0]=Lan.g("TableFamily","Name");
			Fields[1]=Lan.g("TableFamily","Position");
			Fields[2]=Lan.g("TableFamily","Gender");
			Fields[3]=Lan.g("TableFamily","Status");
			Fields[4]=Lan.g("TableFamily","Age");
			Fields[5]=Lan.g("TableFamily","Recall Due");
			ColWidth[0]=140;
			ColWidth[1]=70;
			ColWidth[2]=60;
			ColWidth[3]=70;
			ColWidth[4]=50;
			ColWidth[5]=80;
			LayoutTables();
		}

		private void TableFamily_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

