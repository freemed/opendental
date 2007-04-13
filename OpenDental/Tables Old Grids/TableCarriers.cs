using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.Forms{
	///<summary></summary>
	public class TableCarriers : OpenDental.ContrTable{

		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableCarriers(){
			InitializeComponent();
			MaxRows=50;
			MaxCols=8;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,49,14);
			Fields[0]=Lan.g("TableCarriers","Carrier Name");
			Fields[1]=Lan.g("TableCarriers","Phone");
			Fields[2]=Lan.g("TableCarriers","Address");
			Fields[3]=Lan.g("TableCarriers","Address2");
			Fields[4]=Lan.g("TableCarriers","City");
			Fields[5]=Lan.g("TableCarriers","ST");
			Fields[6]=Lan.g("TableCarriers","Zip");
			Fields[7]=Lan.g("TableCarriers","ElectID");
			ColWidth[0]=160;
			ColWidth[1]=90;
			ColWidth[2]=130;
			ColWidth[3]=120;
			ColWidth[4]=110;
			ColWidth[5]=60;
			ColWidth[6]=90;
			ColWidth[7]=60;
			//ColAlign[1]=HorizontalAlignment.Right;
			//ColAlign[2]=HorizontalAlignment.Right;
			//ColAlign[3]=HorizontalAlignment.Right;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TableCarriers
			// 
			this.Name = "TableCarriers";
			this.Load += new System.EventHandler(this.TableCarriers_Load);

		}
		#endregion

		private void TableCarriers_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}



	}
}

