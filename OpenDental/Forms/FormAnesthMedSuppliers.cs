using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAnesthMedSuppliers:System.Windows.Forms.Form {
		private OpenDental.UI.Button butAddNew;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button butNone;
		private OpenDental.UI.Button butOK;
		private bool changed;
        public bool IsSelectionMode;
		///<summary>Only used if IsSelectionMode.  On OK, contains selected anesthMedSuppliersNum.  Can be 0.  Can also be set ahead of time externally.</summary>
		public int SelectedSupplierIDNum;

		///<summary></summary>
		public FormAnesthMedSuppliers()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnesthMedSuppliers));
            this.butNone = new OpenDental.UI.Button();
            this.butOK = new OpenDental.UI.Button();
            this.gridMain = new OpenDental.UI.ODGrid();
            this.butAddNew = new OpenDental.UI.Button();
            this.butClose = new OpenDental.UI.Button();
            this.SuspendLayout();
            // 
            // butNone
            // 
            this.butNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butNone.Autosize = true;
            this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butNone.CornerRadius = 4F;
            this.butNone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butNone.Location = new System.Drawing.Point(726, 590);
            this.butNone.Name = "butNone";
            this.butNone.Size = new System.Drawing.Size(68, 24);
            this.butNone.TabIndex = 16;
            this.butNone.Text = "None";
            this.butNone.Click += new System.EventHandler(this.butNone_Click);
            // 
            // butOK
            // 
            this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Autosize = true;
            this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butOK.CornerRadius = 4F;
            this.butOK.Location = new System.Drawing.Point(874, 590);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 24);
            this.butOK.TabIndex = 15;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // gridMain
            // 
            this.gridMain.HScrollVisible = false;
            this.gridMain.Location = new System.Drawing.Point(17, 12);
            this.gridMain.Name = "gridMain";
            this.gridMain.ScrollValue = 0;
            this.gridMain.Size = new System.Drawing.Size(1030, 560);
            this.gridMain.TabIndex = 11;
            this.gridMain.Title = "Anesthetic Medication Suppliers";
            this.gridMain.TranslationName = "TableAnesthMedSuppliers";
            this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
            // 
            // butAddNew
            // 
            this.butAddNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butAddNew.Autosize = true;
            this.butAddNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAddNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAddNew.CornerRadius = 4F;
            this.butAddNew.Image = global::OpenDental.Properties.Resources.Add;
            this.butAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butAddNew.Location = new System.Drawing.Point(17, 590);
            this.butAddNew.Name = "butAddNew";
            this.butAddNew.Size = new System.Drawing.Size(85, 24);
            this.butAddNew.TabIndex = 10;
            this.butAddNew.Text = "&Add New";
            this.butAddNew.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // butClose
            // 
            this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Autosize = true;
            this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butClose.CornerRadius = 4F;
            this.butClose.Location = new System.Drawing.Point(955, 590);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 24);
            this.butClose.TabIndex = 0;
            this.butClose.Text = "&Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // FormAnesthMedSuppliers
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1058, 630);
            this.Controls.Add(this.butNone);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.gridMain);
            this.Controls.Add(this.butAddNew);
            this.Controls.Add(this.butClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAnesthMedSuppliers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anesthetic Medication Suppliers";
            this.Load += new System.EventHandler(this.FormAnesthMedSuppliers_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAnesthMedSuppliers_FormClosing);
            this.ResumeLayout(false);

		}
		#endregion

		private void FormAnesthMedSuppliers_Load(object sender, System.EventArgs e) {
			if(IsSelectionMode){
				butClose.Text=Lan.g(this,"Cancel");
			}
			else{
				butOK.Visible=false;
				butNone.Visible=false;
			}
			FillGrid();
			/*if(SelectedAnesthMedSupplierNum!=0){
				for(int i=0;i<AnesthMedSupplierC.Listt.Count;i++){
					if(AnesthMedSupplierC.Listt[i].AnesthMedSupplierNum==SelectedAnesthMedSupplierNum){
						gridMain.SetSelected(i,true);
						break;
					}
				}
			}*/
		}

		private void FillGrid(){
			AnesthMedSuppliers.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAnesthMedSuppliers","Supplier"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAnesthMedSuppliers","Phone"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAnesthMedSuppliers","Fax"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAnesthMedSuppliers","Address"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAnesthMedSuppliers","City"),100);
			gridMain.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedSuppliers","State"),60);
            gridMain.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedSuppliers","Zip"), 80);
            gridMain.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedSuppliers","WebSite"), 140);
            gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string txt;
			for(int i=0;i<AnesthMedSupplierC.Listt.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(AnesthMedSupplierC.Listt[i].SupplierName);
				row.Cells.Add(AnesthMedSupplierC.Listt[i].Phone);
				row.Cells.Add(AnesthMedSupplierC.Listt[i].Fax);
				txt=AnesthMedSupplierC.Listt[i].Addr1;
				/*if(AnesthMedSupplierC.Listt[i].Addr2!=""){
					txt+="\r\n"+AnesthMedSupplierC.Listt[i].Addr2;
				}
				row.Cells.Add(txt);*/
				row.Cells.Add(AnesthMedSupplierC.Listt[i].City);
                row.Cells.Add(AnesthMedSupplierC.Listt[i].State);
                row.Cells.Add(AnesthMedSupplierC.Listt[i].Zip);
                row.Cells.Add(AnesthMedSupplierC.Listt[i].WebSite);
				//row.Cells.Add(AnesthMedSupplierC.Listt[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormAnesthMedSuppliersEdit FormMS=new FormAnesthMedSuppliersEdit();
			//FormMS.SupplCur=new AnesthMedSupplier();
			//FormMS.SupplCur.IsNew=true;
			FormMS.ShowDialog();
			FillGrid();
			changed=true;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode){
				SelectedSupplierIDNum=AnesthMedSupplierC.Listt[e.Row].SupplierIDNum;
				DialogResult=DialogResult.OK;
				return;
			}
			else{
				FormAnesthMedSuppliersEdit FormMS=new FormAnesthMedSuppliersEdit();
				//FormMS.SupplCur=AnesthMedSupplierC.Listt[e.Row];
				FormMS.ShowDialog();
				FillGrid();
				changed=true;
			}
		}

		private void butNone_Click(object sender,EventArgs e) {
			//not even visible unless is selection mode
			SelectedSupplierIDNum=0;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless is selection mode
			if(gridMain.GetSelectedIndex()==-1){
			//	MsgBox.Show(this,"Please select an item first.");
			//	return;
				SelectedSupplierIDNum=0;
			}
			else{
				SelectedSupplierIDNum=AnesthMedSupplierC.Listt[gridMain.GetSelectedIndex()].SupplierIDNum;
			}
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormAnesthMedSuppliers_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed){
				//DataValid.SetInvalid(InvalidType.AnesthMedSuppliers);
			}
		}

	

		

		

		



		
	}
}





















