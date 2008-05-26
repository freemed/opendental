using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRxSetup : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butAdd2;
		private OpenDental.UI.Button butClose;
		private ODGrid gridMain;
		private OpenDental.UI.Button butPrinting;// Required designer variable.
		private RxDef[] RxDefList;

		///<summary></summary>
		public FormRxSetup(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRxSetup));
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butAdd2 = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butPrinting = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(850,636);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 4;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(548,636);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(92,26);
			this.butAdd.TabIndex = 14;
			this.butAdd.Text = "Add &New";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butAdd2
			// 
			this.butAdd2.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd2.Autosize = true;
			this.butAdd2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd2.CornerRadius = 4F;
			this.butAdd2.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd2.Location = new System.Drawing.Point(269,636);
			this.butAdd2.Name = "butAdd2";
			this.butAdd2.Size = new System.Drawing.Size(221,26);
			this.butAdd2.TabIndex = 16;
			this.butAdd2.Text = "&Add Using Selected as Starting Pt.";
			this.butAdd2.Click += new System.EventHandler(this.butAdd2_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,40);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(913,584);
			this.gridMain.TabIndex = 17;
			this.gridMain.Title = "Prescriptions";
			this.gridMain.TranslationName = "TableRxSetup";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butPrinting
			// 
			this.butPrinting.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrinting.Autosize = true;
			this.butPrinting.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrinting.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrinting.CornerRadius = 4F;
			this.butPrinting.Location = new System.Drawing.Point(12,8);
			this.butPrinting.Name = "butPrinting";
			this.butPrinting.Size = new System.Drawing.Size(87,26);
			this.butPrinting.TabIndex = 18;
			this.butPrinting.Text = "Printing Setup";
			this.butPrinting.Click += new System.EventHandler(this.butPrinting_Click);
			// 
			// FormRxSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(942,674);
			this.Controls.Add(this.butPrinting);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd2);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Rx Setup";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormRxSetup_Closing);
			this.Load += new System.EventHandler(this.FormRxSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRxSetup_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			RxDefList=RxDefs.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRxSetup","Drug"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Sig"),320);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Disp"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Refills"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Notes"),300);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<RxDefList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(RxDefList[i].Drug);
				row.Cells.Add(RxDefList[i].Sig);
				row.Cells.Add(RxDefList[i].Disp);
				row.Cells.Add(RxDefList[i].Refills);
				row.Cells.Add(RxDefList[i].Notes);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormRxDefEdit FormE=new FormRxDefEdit(RxDefList[e.Row]);
			FormE.ShowDialog();
			FillGrid();
		}

		private void butPrinting_Click(object sender,EventArgs e) {
			FormRxSetupPrinting FormR=new FormRxSetupPrinting();
			FormR.ShowDialog();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			RxDef RxDefCur=new RxDef();
			RxDefs.Insert(RxDefCur);//It gets deleted if user clicks cancel
			FormRxDefEdit FormE=new FormRxDefEdit(RxDefCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			FillGrid();
		}

		private void butAdd2_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MessageBox.Show(Lan.g(this,"Please select item first"));
				return;
			}
			RxDef RxDefCur=RxDefList[gridMain.GetSelectedIndex()].Copy();
			RxDefs.Insert(RxDefCur);//Now it has a new id.  It gets deleted if user clicks cancel. Alerts not copied.
			FormRxDefEdit FormE=new FormRxDefEdit(RxDefCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormRxSetup_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//SecurityLogs.MakeLogEntry("Prescription Setup","Altered Prescription Setup",user);
		}

	

		

	}
}
