using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TableMedications : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableMedications(){
			InitializeComponent();
			MaxRows=50;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,49,14);
			Fields[0]=Lan.g("TableMedications","Drug Name");
			Fields[1]=Lan.g("TableMedications","Generic Name");
			Fields[2]=Lan.g("TableMedications","Notes");
			Fields[3]=Lan.g("TableMedications","Notes for Patient");
			ColWidth[0]=100;
			ColWidth[1]=100;
			ColWidth[2]=370;
			ColWidth[3]=370;
			//ColAlign[1]=HorizontalAlignment.Right;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if (components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Designer generated code

		private void InitializeComponent(){
			// 
			// TableAutoBilling
			// 
			this.Name = "TableAutoBilling";
			this.Load += new System.EventHandler(this.TableAutoBilling_Load);

		}
		#endregion

		
		private void TableAutoBilling_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}

	}
}

