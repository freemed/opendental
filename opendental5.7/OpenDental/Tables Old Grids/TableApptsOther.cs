using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableApptsOther : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableApptsOther(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			Heading=Lan.g("TableApptOthers","Appointments for Patient");
			InstantClassesPar();
			SetRowHeight(0,19,14);
			//Fields[0]=Lan.g("TableApptOthers","Is Next?");
			Fields[0]=Lan.g("TableApptOthers","Appt Status");
			Fields[1]=Lan.g("TableApptOthers","Date");
			Fields[2]=Lan.g("TableApptOthers","Time");
			Fields[3]=Lan.g("TableApptOthers","Min");
			Fields[4]=Lan.g("TableApptOthers","Procedures");
			Fields[5]=Lan.g("TableApptOthers","Notes");
			//ColWidth[0]=70;
			ColWidth[0]=100;
			ColWidth[1]=70;
			ColWidth[2]=70;
			ColWidth[3]=40;
			ColWidth[4]=150;
			ColWidth[5]=320;
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
			this.SuspendLayout();
			// 
			// TableApptsOther
			// 
			this.Name = "TableApptsOther";
			this.Load += new System.EventHandler(this.TableApptsOther_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableApptsOther_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

