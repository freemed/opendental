using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness;
using OpenDentBusiness.HL7;
using OpenDental.UI;

namespace OpenDental{
	/// <summary></summary>
	public class FormHL7Defs:System.Windows.Forms.Form {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private bool changed;
		//public bool IsSelectionMode;
		//<summary>Only used if IsSelectionMode.  On OK, contains selected siteNum.  Can be 0.  Can also be set ahead of time externally.</summary>
		//public int SelectedSiteNum;
		private List<String> internalList;
		private bool changed;
		private UI.Button butClose;
		private UI.Button butNew;
		private UI.Button butCopy;
		private ODGrid grid2;
		private ODGrid grid1;
		//List<HL7Def> LabelList;

		///<summary></summary>
		public FormHL7Defs()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7Defs));
			this.grid2 = new OpenDental.UI.ODGrid();
			this.grid1 = new OpenDental.UI.ODGrid();
			this.butCopy = new OpenDental.UI.Button();
			this.butNew = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// grid2
			// 
			this.grid2.HScrollVisible = false;
			this.grid2.Location = new System.Drawing.Point(445,42);
			this.grid2.Name = "grid2";
			this.grid2.ScrollValue = 0;
			this.grid2.Size = new System.Drawing.Size(424,583);
			this.grid2.TabIndex = 12;
			this.grid2.Title = "Custom";
			this.grid2.TranslationName = null;
			this.grid2.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid2_CellDoubleClick);
			this.grid2.Click += new System.EventHandler(this.grid2_Click);
			// 
			// grid1
			// 
			this.grid1.HScrollVisible = false;
			this.grid1.Location = new System.Drawing.Point(12,42);
			this.grid1.Name = "grid1";
			this.grid1.ScrollValue = 0;
			this.grid1.Size = new System.Drawing.Size(424,583);
			this.grid1.TabIndex = 14;
			this.grid1.Title = "Internal";
			this.grid1.TranslationName = null;
			this.grid1.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid1_CellDoubleClick);
			this.grid1.Click += new System.EventHandler(this.grid1_Click);
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopy.Autosize = true;
			this.butCopy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.CornerRadius = 4F;
			this.butCopy.Image = global::OpenDental.Properties.Resources.Right;
			this.butCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCopy.Location = new System.Drawing.Point(333,635);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(75,24);
			this.butCopy.TabIndex = 15;
			this.butCopy.Text = "Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
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
			this.butNew.Location = new System.Drawing.Point(615,635);
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
			this.butClose.Location = new System.Drawing.Point(794,635);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormHL7Defs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(881,669);
			this.Controls.Add(this.butCopy);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.grid2);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormHL7Defs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Defs";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSheetDefs_FormClosing);
			this.Load += new System.EventHandler(this.FormHL7Defs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormHL7Defs_Load(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup,true)){
				butNew.Enabled=false;
				butCopy.Enabled=false;
				grid2.Enabled=false;
			}
			FillGrid1();
			FillGrid2();
			
		}

		private void FillGrid1(){
			grid1.BeginUpdate();
			grid1.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableHL7Def","Description"),170);
			grid1.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableHL7Def","Type"),100);
			grid1.Columns.Add(col);
			grid1.Rows.Clear();
			ODGridRow row=new ODGridRow();
			HL7Def def;
			def=InternalEcw.GetHL7Def();
			row.Cells.Add(def.Description);
			row.Cells.Add(def.ModeTx.ToString());
			grid1.Rows.Add(row);
			//internalList=SheetsInternal.GetAllInternal();
			//for(int i=0;i<internalList.Count;i++){
			//  row=new ODGridRow();
			//  row.Cells.Add(internalList[i].Description);//Enum.GetNames(typeof(SheetInternalType))[i]);
			//  row.Cells.Add(internalList[i].SheetType.ToString());
			//  grid1.Rows.Add(row);
			//}
			grid1.EndUpdate();
		}

		private void FillGrid2(){
			SheetDefs.RefreshCache();
			SheetFieldDefs.RefreshCache();
			grid2.BeginUpdate();
			grid2.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableHL7Def","Description"),170);
			grid2.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableHL7Def","Type"),100);
			grid2.Columns.Add(col);
			grid2.Rows.Clear();
			//ODGridRow row;
			//for(int i=0;i<SheetDefC.Listt.Count;i++){
			//  row=new ODGridRow();
			//  row.Cells.Add(SheetDefC.Listt[i].Description);
			//  row.Cells.Add(SheetDefC.Listt[i].SheetType.ToString());
			//  grid2.Rows.Add(row);
			//}
			grid2.EndUpdate();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			////This button is not enabled unless user has appropriate permission for setup.
			////Not allowed to change sheettype once a sheet is created, so we need to let user pick.
			//FormSheetDef FormS=new FormSheetDef();
			//FormS.IsInitial=true;
			//FormS.IsReadOnly=false;
			//SheetDef sheetdef=new SheetDef();
			//sheetdef.FontName="Microsoft Sans Serif";
			//sheetdef.FontSize=9;
			//sheetdef.Height=1100;
			//sheetdef.Width=850;
			//FormS.SheetDefCur=sheetdef;
			//FormS.ShowDialog();
			//if(FormS.DialogResult!=DialogResult.OK){
			//  return;
			//}
			////what about parameters?
			//sheetdef.SheetFieldDefs=new List<SheetFieldDef>();
			//sheetdef.IsNew=true;
			//FormSheetDefEdit FormSD=new FormSheetDefEdit(sheetdef);
			//FormSD.ShowDialog();//It will be saved to db inside this form.
			//FillGrid2();
			//for(int i=0;i<SheetDefC.Listt.Count;i++){
			//  if(SheetDefC.Listt[i].SheetDefNum==sheetdef.SheetDefNum){
			//    grid2.SetSelected(i,true);
			//  }
			//}
			//changed=true;
		}

		private void butCopy_Click(object sender,EventArgs e) {
			if(grid1.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an internal setup from the list above first.");
				return;
			}
			//SheetDef sheetdef=internalList[grid1.GetSelectedIndex()].Copy();
			//sheetdef.IsNew=true;
			//SheetDefs.InsertOrUpdate(sheetdef);
			//if(sheetdef.SheetType==SheetTypeEnum.MedicalHistory
			//  && (sheetdef.Description=="Medical History New Patient" || sheetdef.Description=="Medical History Update")) 
			//{
			//  MsgBox.Show(this,"This is just a template, it may contain allergies and problems that do not exist in your setup.");
			//}
			//grid1.SetSelected(false);
			//FillGrid2();
			//for(int i=0;i<SheetDefC.Listt.Count;i++){
			//  if(SheetDefC.Listt[i].SheetDefNum==sheetdef.SheetDefNum){
			//    grid2.SetSelected(i,true);
			//  }
			//}
		}

		private void grid1_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefEdit FormS=new FormHL7DefEdit();
			HL7Def def=null;
			switch(e.Row) {
				case 0://eCW
					def=InternalEcw.GetHL7Def();
					break;
				default://Should never happen.
					throw new Exception(Lan.g(this,"Row selected in FormHL7Defs.cs was not added to switch statment."));//Just in case.
			}
			FormS.HL7DefCur=def;
			FormS.IsInternal=true;
			FormS.ShowDialog();
		}

		private void grid1_Click(object sender,EventArgs e) {
			if(grid1.GetSelectedIndex()>-1) {
				grid2.SetSelected(false);
			}
		}
		
		private void grid2_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//SheetDef sheetdef=SheetDefC.Listt[e.Row];
			//SheetDefs.GetFieldsAndParameters(sheetdef);
			//FormSheetDefEdit FormS=new FormSheetDefEdit(sheetdef);
			//FormS.ShowDialog();
			//FillGrid2();
			//for(int i=0;i<SheetDefC.Listt.Count;i++){
			//  if(SheetDefC.Listt[i].SheetDefNum==sheetdef.SheetDefNum){
			//    grid2.SetSelected(i,true);
			//  }
			//}
			//changed=true;
		}

		private void grid2_Click(object sender,EventArgs e) {
			if(grid2.GetSelectedIndex()>-1) {
				grid1.SetSelected(false);
			}
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormSheetDefs_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidType.Sheets);
			}
		}

	



	}
}





















