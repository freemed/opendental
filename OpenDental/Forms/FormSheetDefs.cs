using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary></summary>
	public class FormSheetDefs:System.Windows.Forms.Form {
		private OpenDental.UI.Button butNew;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid grid2;
		private ODGrid grid1;
		private OpenDental.UI.Button butCopy;
		//private bool changed;
		private OpenDental.UI.Button butOK;
		//public bool IsSelectionMode;
		//<summary>Only used if IsSelectionMode.  On OK, contains selected siteNum.  Can be 0.  Can also be set ahead of time externally.</summary>
		//public int SelectedSiteNum;
		private List<SheetDef> internalList;
		private bool changed;

		///<summary></summary>
		public FormSheetDefs()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSheetDefs));
			this.butCopy = new OpenDental.UI.Button();
			this.grid1 = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.grid2 = new OpenDental.UI.ODGrid();
			this.butNew = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopy.Autosize = true;
			this.butCopy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.CornerRadius = 4F;
			this.butCopy.Image = global::OpenDental.Properties.Resources.down;
			this.butCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCopy.Location = new System.Drawing.Point(196,278);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(75,24);
			this.butCopy.TabIndex = 15;
			this.butCopy.Text = "Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// grid1
			// 
			this.grid1.HScrollVisible = false;
			this.grid1.Location = new System.Drawing.Point(12,12);
			this.grid1.Name = "grid1";
			this.grid1.ScrollValue = 0;
			this.grid1.Size = new System.Drawing.Size(437,260);
			this.grid1.TabIndex = 14;
			this.grid1.Title = "Internal";
			this.grid1.TranslationName = null;
			this.grid1.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid1_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(428,576);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 13;
			this.butOK.Text = "OK";
			this.butOK.Visible = false;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// grid2
			// 
			this.grid2.HScrollVisible = false;
			this.grid2.Location = new System.Drawing.Point(12,308);
			this.grid2.Name = "grid2";
			this.grid2.ScrollValue = 0;
			this.grid2.Size = new System.Drawing.Size(437,245);
			this.grid2.TabIndex = 12;
			this.grid2.Title = "Custom";
			this.grid2.TranslationName = null;
			this.grid2.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid2_CellDoubleClick);
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butNew.Autosize = true;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNew.CornerRadius = 4F;
			this.butNew.Image = global::OpenDental.Properties.Resources.Add;
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.Location = new System.Drawing.Point(12,576);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(80,24);
			this.butNew.TabIndex = 10;
			this.butNew.Text = "New";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(509,576);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormSheetDefs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(596,612);
			this.Controls.Add(this.butCopy);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.grid2);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSheetDefs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sheet Defs";
			this.Load += new System.EventHandler(this.FormSheetDefs_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSheetDefs_FormClosing);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSheetDefs_Load(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup,true)){
				butAdd.Visible=false;
			}
			/*if(IsSelectionMode){
				butClose.Text=Lan.g(this,"Cancel");
				if(!Security.IsAuthorized(Permissions.Setup,true)){
					butAdd.Visible=false;
				}
			}
			else{
				butOK.Visible=false;
				butNone.Visible=false;
			}
			FillGrid();
			if(SelectedSiteNum!=0){
				for(int i=0;i<SiteC.List.Length;i++){
					if(SiteC.List[i].SiteNum==SelectedSiteNum){
						gridMain.SetSelected(i,true);
						break;
					}
				}
			}*/
			FillGrid1();
			FillGrid2();
		}

		private void FillGrid1(){
			grid1.BeginUpdate();
			grid1.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableSheetDef","Description"),155);
			grid1.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSheetDef","Type"),100);
			grid1.Columns.Add(col);
			grid1.Rows.Clear();
			ODGridRow row;
			internalList=SheetsInternal.GetAllInternal();
			for(int i=0;i<internalList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Enum.GetNames(typeof(SheetInternalType))[i]);
				row.Cells.Add(internalList[i].SheetType.ToString());
				grid1.Rows.Add(row);
			}
			grid1.EndUpdate();
		}

		private void FillGrid2(){
			SheetDefs.RefreshCache();
			SheetFieldDefs.RefreshCache();
			grid2.BeginUpdate();
			grid2.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableSheetDef","Description"),155);
			grid2.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSheetDef","Type"),100);
			grid2.Columns.Add(col);
			grid2.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(SheetDefC.Listt[i].Description);
				row.Cells.Add(SheetDefC.Listt[i].SheetType.ToString());
				grid2.Rows.Add(row);
			}
			grid2.EndUpdate();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			//This button is not visible unless user has appropriate permission for setup.

			//Not allowed to change sheettype once a sheet is created, so we need to let user pick.

			/*SheetDef sheetdef=SheetDefC.Listt[e.Row];
			SheetDefs.GetFieldsAndParameters(sheetdef);
			FormSheetDefEdit FormS=new FormSheetDefEdit(sheetdef);
			FormS.ShowDialog();
			FillGrid2();
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetDefNum==sheetdef.SheetDefNum){
					grid2.SetSelected(i,true);
				}
			}
			changed=true;*/


			/*FormSiteEdit FormS=new FormSiteEdit();
			FormS.SiteCur=new Site();
			FormS.SiteCur.IsNew=true;
			FormS.ShowDialog();
			FillGrid();
			changed=true;*/
		}

		private void butCopy_Click(object sender,EventArgs e) {
			if(grid1.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an internal sheet from the list above first.");
				return;
			}
			SheetDef sheetdef=internalList[grid1.GetSelectedIndex()].Copy();
			sheetdef.Description=Enum.GetNames(typeof(SheetInternalType))[grid1.GetSelectedIndex()];
			sheetdef.IsNew=true;
			SheetDefs.WriteObject(sheetdef);
			grid1.SetSelected(false);
			FillGrid2();
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetDefNum==sheetdef.SheetDefNum){
					grid2.SetSelected(i,true);
				}
			}
		}

		private void grid1_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSheetDefEdit FormS=new FormSheetDefEdit(internalList[e.Row]);
			FormS.IsInternal=true;
			FormS.ShowDialog();
		}

		private void grid2_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			/*if(IsSelectionMode){
				SelectedSiteNum=SiteC.List[e.Row].SiteNum;
				DialogResult=DialogResult.OK;
				return;
			}
			else{*/
				SheetDef sheetdef=SheetDefC.Listt[e.Row];
				SheetDefs.GetFieldsAndParameters(sheetdef);
				FormSheetDefEdit FormS=new FormSheetDefEdit(sheetdef);
				FormS.ShowDialog();
				FillGrid2();
				for(int i=0;i<SheetDefC.Listt.Count;i++){
					if(SheetDefC.Listt[i].SheetDefNum==sheetdef.SheetDefNum){
						grid2.SetSelected(i,true);
					}
				}
				changed=true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless is selection mode
			/*if(gridMain.GetSelectedIndex()==-1){
				SelectedSiteNum=0;
			}
			else{
				SelectedSiteNum=SiteC.List[gridMain.GetSelectedIndex()].SiteNum;
			}
			DialogResult=DialogResult.OK;*/
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			//if(IsSelectionMode){
				//DialogResult=DialogResult.Cancel;
			//}
			//else{
			Close();
			//}*/
		}

		private void FormSheetDefs_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidType.Sheets);
			}
		}

		

		

		

		

		

		



		
	}
}





















