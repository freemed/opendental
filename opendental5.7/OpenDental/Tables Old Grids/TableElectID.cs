using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental
{
	///<summary></summary>
	public class TableElectID : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableElectID(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=50;
			MaxCols=3;
			ShowScroll=true;
			FieldsArePresent=true;
			//HeadingIsPresent=true;
			//Heading=Lan.g("TableCommLog","Communications Log - Appointment Scheduling");
			InstantClassesPar();
			SetRowHeight(0,49,14);
			Fields[0]=Lan.g("TableElectID","Carrier");
			Fields[1]=Lan.g("TableElectID","Payer ID");
			Fields[2]=Lan.g("TableElectID","Comments");
			ColWidth[0]=320;
			ColWidth[1]=80;
			ColWidth[2]=460;
			//ColWidth[3]=100;
			//ColAlign[1]=HorizontalAlignment.Right;
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
			// TableElectID
			// 
			this.Name = "TableElectID";
			this.Load += new System.EventHandler(this.TableElectID_Load);

		}
		#endregion

		private void TableElectID_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}


	}
}

