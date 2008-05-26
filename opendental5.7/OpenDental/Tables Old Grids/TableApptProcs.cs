using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableApptProcs : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableApptProcs(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading="Procedures";
			Fields[0]=Lan.g("TableApptProcs","Stat");
			Fields[1]=Lan.g("TableApptProcs","Priority");
			Fields[2]=Lan.g("TableApptProcs","Tth");
			Fields[3]=Lan.g("TableApptProcs","Surf");
			Fields[4]=Lan.g("TableApptProcs","Description");
			Fields[5]=Lan.g("TableApptProcs","Fee");
			ColAlign[5]=HorizontalAlignment.Right;
			ColWidth[0]=40;
			ColWidth[1]=55;
			ColWidth[2]=30;
			ColWidth[3]=40;
			ColWidth[4]=175;
			ColWidth[5]=60;			
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
			// TableApptProcs
			// 
			this.Name = "TableApptProcs";
			this.Load += new System.EventHandler(this.TableApptProcs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableApptProcs_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}




	}
}

