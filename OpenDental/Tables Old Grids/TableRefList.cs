using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TableRefList : OpenDental.ContrTable{
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public TableRefList(){
			InitializeComponent();
			InstantClasses();
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent()
		{
			// 
			// TableReferral
			// 
			this.Name = "TableRefList";
			this.Load += new System.EventHandler(this.TableRefList_Load);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			MaxRows=4;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,3,14);
			SetGridColor(Color.LightGray);
			SetBackGColor(Color.White);
			Heading=Lan.g("TableRefList","Referrals Attached");

			Fields[0]=Lan.g("TableRefList","From/To");
			Fields[1]=Lan.g("TableRefList","Name");
			Fields[2]=Lan.g("TableRefList","Date");
			Fields[3]=Lan.g("TableRefList","Note");

			ColWidth[0]=50;
			ColWidth[1]=120;
			ColWidth[2]=70;
			ColWidth[3]=200;
			LayoutTables();
		}

		private void TableRefList_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}
	}
}
