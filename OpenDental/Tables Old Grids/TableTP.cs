using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableTP : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableTP(){
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
			// TableTP
			// 
			this.Name = "TableTP";
			this.Load += new System.EventHandler(this.TableTP_Load);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			MaxRows=20;
			MaxCols=10;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableTP","Treatment Plan");
			Fields[0]=Lan.g("TableTP","Priority");
			Fields[1]=Lan.g("TableTP","Tth");
			Fields[2]=Lan.g("TableTP","Surf");
			Fields[3]=Lan.g("TableTP","Code");
			Fields[4]=Lan.g("TableTP","Description");
			Fields[5]=Lan.g("TableTP","Fee");
			Fields[6]=Lan.g("TableTP","Pri Ins");
			Fields[7]=Lan.g("TableTP","Sec Ins");
			Fields[8]=Lan.g("TableTP","Pat");
			Fields[9]=Lan.g("TableTP","Pre Est");
			ColAlign[5]=HorizontalAlignment.Right;
			ColAlign[6]=HorizontalAlignment.Right;
			ColAlign[7]=HorizontalAlignment.Right;
			ColAlign[8]=HorizontalAlignment.Right;
			ColAlign[9]=HorizontalAlignment.Right;
			ColWidth[0]=55;
			ColWidth[1]=40;
			ColWidth[2]=40;
			ColWidth[3]=50;
			ColWidth[4]=250;
			ColWidth[5]=45;
			ColWidth[6]=45;
			ColWidth[7]=45;
			ColWidth[8]=45;
			ColWidth[9]=45;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		private void TableTP_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

