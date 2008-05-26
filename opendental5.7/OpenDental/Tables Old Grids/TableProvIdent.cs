using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental
{
	///<summary></summary>
	public class TableProvIdent : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableProvIdent(){
			InitializeComponent();//This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=3;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			//Heading=Lan.g("TableQueue","Provider Identifiers");
			Fields[0]=Lan.g("TableQueue","Payor ID");
			Fields[1]=Lan.g("TableQueue","Type");
			Fields[2]=Lan.g("TableQueue","ID Number");
			ColWidth[0]=90;
			ColWidth[1]=110;
			ColWidth[2]=100;
			//ColAlign[1]=HorizontalAlignment.Center;
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
			// TableProvIdent
			// 
			this.Name = "TableProvIdent";
			this.Load += new System.EventHandler(this.TableProvIdent_Load);

		}
		#endregion

		private void TableProvIdent_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}



	}
}

