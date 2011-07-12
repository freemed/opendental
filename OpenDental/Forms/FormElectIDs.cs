using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormElectIDs : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsSelectMode;
		private UI.ODGrid gridElectIDs;
		private UI.Button butAdd;
		///<summary></summary>
		public ElectID selectedID;

		///<summary></summary>
		public FormElectIDs()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormElectIDs));
			this.gridElectIDs = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridElectIDs
			// 
			this.gridElectIDs.AllowSelection = false;
			this.gridElectIDs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridElectIDs.HScrollVisible = false;
			this.gridElectIDs.Location = new System.Drawing.Point(7,12);
			this.gridElectIDs.Name = "gridElectIDs";
			this.gridElectIDs.ScrollValue = 0;
			this.gridElectIDs.Size = new System.Drawing.Size(879,617);
			this.gridElectIDs.TabIndex = 140;
			this.gridElectIDs.Title = "";
			this.gridElectIDs.TranslationName = "TableApptProcs";
			this.gridElectIDs.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridElectIDs_CellDoubleClick);
			this.gridElectIDs.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridElectIDs_CellClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(721,635);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(808,635);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
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
			this.butAdd.Location = new System.Drawing.Point(409,635);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,25);
			this.butAdd.TabIndex = 141;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormElectIDs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(892,674);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridElectIDs);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormElectIDs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Electronic Payer ID\'s";
			this.Load += new System.EventHandler(this.FormElectIDs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormElectIDs_Load(object sender, System.EventArgs e) {
			FillElectIDs();
		}

		private void FillElectIDs() {
			gridElectIDs.BeginUpdate();
			gridElectIDs.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableApptProcs","Carrier"),320);
			gridElectIDs.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Payer ID"),80);
			gridElectIDs.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Is Medicaid"),70,HorizontalAlignment.Center);
			gridElectIDs.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptProcs","Comments"),390);
			gridElectIDs.Columns.Add(col);
			gridElectIDs.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ElectIDs.List.Length;i++) {
				row=new ODGridRow();
				row.Cells.Add(ElectIDs.List[i].CarrierName);
				row.Cells.Add(ElectIDs.List[i].PayorID);
				row.Cells.Add(ElectIDs.List[i].IsMedicaid?"X":"");
				row.Cells.Add(ElectIDs.List[i].Comments);
				gridElectIDs.Rows.Add(row);
			}
			gridElectIDs.EndUpdate();
		}

		private void gridElectIDs_CellClick(object sender,ODGridClickEventArgs e) {
			gridElectIDs.SetSelected(e.Row,true);
		}

		private void gridElectIDs_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectMode) {
				selectedID=ElectIDs.List[e.Row];
				DialogResult=DialogResult.OK;
			}
			else {
				FormElectIDEdit FormEdit=new FormElectIDEdit();
				FormEdit.electIDCur=ElectIDs.List[e.Row];
				FormEdit.ShowDialog();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormElectIDEdit FormEdit=new FormElectIDEdit();
			FormEdit.electIDCur=new ElectID();
			FormEdit.electIDCur.IsNew=true;			
			FormEdit.ShowDialog();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(IsSelectMode && gridElectIDs.SelectedIndices.Length<1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			selectedID=ElectIDs.List[gridElectIDs.SelectedIndices[0]];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	}
}





















