using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
	///<summary></summary>
	public class TableProg : OpenDental.ContrTable{
		///<summary></summary>
		public float NoteWidth;
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableProg(){			
			InitializeComponent();// This call is required by the Windows Form Designer.
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
			// TableProg
			// 
			this.Name = "TableProg";
			this.Size = new System.Drawing.Size(484, 168);
			this.Load += new System.EventHandler(this.TableProg_Load);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			
		}

		private void TableProg_Load(object sender, System.EventArgs e) {
			MaxRows=20;
			MaxCols=8;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableProg","Progress Notes");
			Fields[0]=Lan.g("TableProg","Date");
			Fields[1]=Lan.g("TableProg","Th");
			Fields[2]=Lan.g("TableProg","Surf");
			Fields[3]=Lan.g("TableProg","Dx");
			Fields[4]=Lan.g("TableProg","Description");
			Fields[5]=Lan.g("TableProg","Stat");
			Fields[6]=Lan.g("TableProg","Prov");
			Fields[7]=Lan.g("TableProg","Amount");
			ColAlign[7]=HorizontalAlignment.Right;
			ColWidth[0]=63;
			ColWidth[1]=22;
			ColWidth[2]=37;
			ColWidth[3]=22;
			ColWidth[4]=225;
			ColWidth[5]=25;
			ColWidth[6]=35;
			ColWidth[7]=50;
			LayoutTables();
			NoteWidth=(float)(ColWidth[2]+ColWidth[3]+ColWidth[4]+ColWidth[5]+ColWidth[6]+ColWidth[7]);
			//GridColor=Color.Gray;
		}

		///<summary></summary>
		public void SetProcRow(int row){
			for(int j=0;j<MaxCols;j++){
				LeftBorder[j,row]=DefaultGridColor;
				TopBorder[j,row]=DefaultGridColor;
				IsOverflow[j,row]=false;
			}
		}

		///<summary></summary>
		public void SetNoteRow(int row){
			for(int j=0;j<2;j++){
				LeftBorder[j,row]=DefaultGridColor;
				TopBorder[j,row]=DefaultGridColor;
				IsOverflow[j,row]=false;
			}
			LeftBorder[2,row]=DefaultGridColor;
			TopBorder[2,row]=Color.White;
			IsOverflow[2,row]=false;
			for(int j=3;j<MaxCols;j++){
				LeftBorder[j,row]=Color.White;
				TopBorder[j,row]=Color.White;
				IsOverflow[j,row]=true;
			}
		}

		///<summary></summary>
		public void SetFirstNoteRow(int row){
			for(int j=0;j<2;j++){
				LeftBorder[j,row]=DefaultGridColor;
				TopBorder[j,row]=DefaultGridColor;
				IsOverflow[j,row]=false;
			}
			LeftBorder[2,row]=DefaultGridColor;
			TopBorder[2,row]=DefaultGridColor;
			IsOverflow[2,row]=false;
			for(int j=3;j<MaxCols;j++){
				LeftBorder[j,row]=Color.White;
				TopBorder[j,row]=DefaultGridColor;
				IsOverflow[j,row]=true;
			}
		}

	}
}

