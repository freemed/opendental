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
	public class FormPharmacies:System.Windows.Forms.Form {
		private OpenDental.UI.Button butAdd;
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
		///<summary>Only used if IsSelectionMode.  On OK, contains selected pharmacyNum.  Can be 0.  Can also be set ahead of time externally.</summary>
		public int SelectedPharmacyNum;

		///<summary></summary>
		public FormPharmacies()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPharmacies));
			this.butNone = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNone.Location = new System.Drawing.Point(478,590);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(68,24);
			this.butNone.TabIndex = 16;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(626,590);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 15;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(765,565);
			this.gridMain.TabIndex = 11;
			this.gridMain.Title = "Pharmacies";
			this.gridMain.TranslationName = "TablePharmacies";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(17,590);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80,24);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(707,590);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormPharmacies
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(810,630);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPharmacies";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pharmacies";
			this.Load += new System.EventHandler(this.FormPharmacies_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPharmacies_FormClosing);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPharmacies_Load(object sender, System.EventArgs e) {
			if(IsSelectionMode){
				butClose.Text=Lan.g(this,"Cancel");
			}
			else{
				butOK.Visible=false;
				butNone.Visible=false;
			}
			FillGrid();
			if(SelectedPharmacyNum!=0){
				for(int i=0;i<PharmacyC.Listt.Count;i++){
					if(PharmacyC.Listt[i].PharmacyNum==SelectedPharmacyNum){
						gridMain.SetSelected(i,true);
						break;
					}
				}
			}
		}

		private void FillGrid(){
			Pharmacies.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePharmacies","Store Name"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePharmacies","Phone"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePharmacies","Fax"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePharmacies","Address"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePharmacies","City"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePharmacies","Note"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string txt;
			for(int i=0;i<PharmacyC.Listt.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(PharmacyC.Listt[i].StoreName);
				row.Cells.Add(PharmacyC.Listt[i].Phone);
				row.Cells.Add(PharmacyC.Listt[i].Fax);
				txt=PharmacyC.Listt[i].Address;
				if(PharmacyC.Listt[i].Address2!=""){
					txt+="\r\n"+PharmacyC.Listt[i].Address2;
				}
				row.Cells.Add(txt);
				row.Cells.Add(PharmacyC.Listt[i].City);
				row.Cells.Add(PharmacyC.Listt[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormPharmacyEdit FormPE=new FormPharmacyEdit();
			FormPE.PharmCur=new Pharmacy();
			FormPE.PharmCur.IsNew=true;
			FormPE.ShowDialog();
			FillGrid();
			changed=true;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode){
				SelectedPharmacyNum=PharmacyC.Listt[e.Row].PharmacyNum;
				DialogResult=DialogResult.OK;
				return;
			}
			else{
				FormPharmacyEdit FormP=new FormPharmacyEdit();
				FormP.PharmCur=PharmacyC.Listt[e.Row];
				FormP.ShowDialog();
				FillGrid();
				changed=true;
			}
		}

		private void butNone_Click(object sender,EventArgs e) {
			//not even visible unless is selection mode
			SelectedPharmacyNum=0;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless is selection mode
			if(gridMain.GetSelectedIndex()==-1){
			//	MsgBox.Show(this,"Please select an item first.");
			//	return;
				SelectedPharmacyNum=0;
			}
			else{
				SelectedPharmacyNum=PharmacyC.Listt[gridMain.GetSelectedIndex()].PharmacyNum;
			}
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormPharmacies_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidType.Pharmacies);
			}
		}

	

		

		

		



		
	}
}





















