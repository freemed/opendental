using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCarriers : System.Windows.Forms.Form{
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;
		private OpenDental.UI.Button butCombine;
		//No longer used. <summary>Set to true if using this dialog to select a carrier.</summary>
		//public bool IsSelectMode;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridMain;
		private CheckBox checkCDAnet;
		private bool changed;//keeps track of whether an update is necessary.
		private DataTable table;

		///<summary></summary>
		public FormCarriers()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCarriers));
			this.butOK = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butCombine = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.checkCDAnet = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(873,592);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(90,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(873,468);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(90,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butCombine
			// 
			this.butCombine.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCombine.Autosize = true;
			this.butCombine.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCombine.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCombine.CornerRadius = 4F;
			this.butCombine.Location = new System.Drawing.Point(873,506);
			this.butCombine.Name = "butCombine";
			this.butCombine.Size = new System.Drawing.Size(90,26);
			this.butCombine.TabIndex = 10;
			this.butCombine.Text = "Co&mbine";
			this.toolTip1.SetToolTip(this.butCombine,"Combines multiple Employers");
			this.butCombine.Click += new System.EventHandler(this.butCombine_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(873,628);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(90,26);
			this.butCancel.TabIndex = 12;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(11,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(839,642);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Carriers";
			this.gridMain.TranslationName = "TableCarriers";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// checkCDAnet
			// 
			this.checkCDAnet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCDAnet.Location = new System.Drawing.Point(874,427);
			this.checkCDAnet.Name = "checkCDAnet";
			this.checkCDAnet.Size = new System.Drawing.Size(96,17);
			this.checkCDAnet.TabIndex = 99;
			this.checkCDAnet.Text = "CDAnet Only";
			this.checkCDAnet.Click += new System.EventHandler(this.checkCDAnet_Click);
			// 
			// FormCarriers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(970,677);
			this.Controls.Add(this.checkCDAnet);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCombine);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCarriers";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Carriers";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormCarriers_Closing);
			this.Load += new System.EventHandler(this.FormCarriers_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCarriers_Load(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				checkCDAnet.Checked=true;
			}
			else{
				checkCDAnet.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			Carriers.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			if(checkCDAnet.Checked){
				gridMain.Size=new Size(745,gridMain.Height);
				col=new ODGridColumn(Lan.g("TableCarriers","Carrier Name"),160);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","ElectID"),60);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","PMP"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Network"),50);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Version"),50);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","02"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","03"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","04"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","05"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","06"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","07"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","08"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
			}
			else{
				gridMain.Size=new Size(839,gridMain.Height);
				col=new ODGridColumn(Lan.g("TableCarriers","Carrier Name"),160);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Phone"),90);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Address"),130);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Address2"),120);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","City"),110);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","ST"),60);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Zip"),90);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","ElectID"),60);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			if(checkCDAnet.Checked){
				table=Carriers.Refresh(true);
				for(int i=0;i<table.Rows.Count;i++){
					row=new ODGridRow();
					//row.Tag//CarrierNum
					row.Cells.Add(table.Rows[i]["CarrierName"].ToString());
					row.Cells.Add(table.Rows[i]["ElectID"].ToString());
					row.Cells.Add(table.Rows[i]["PMP"].ToString());
					row.Cells.Add(table.Rows[i]["Network"].ToString());
					row.Cells.Add(table.Rows[i]["Version"].ToString());
					row.Cells.Add(table.Rows[i]["Trans02"].ToString());
					row.Cells.Add(table.Rows[i]["Trans03"].ToString());
					row.Cells.Add(table.Rows[i]["Trans04"].ToString());
					row.Cells.Add(table.Rows[i]["Trans05"].ToString());
					row.Cells.Add(table.Rows[i]["Trans06"].ToString());
					row.Cells.Add(table.Rows[i]["Trans07"].ToString());
					row.Cells.Add(table.Rows[i]["Trans08"].ToString());
					gridMain.Rows.Add(row);
				}
			}
			else{
				for(int i=0;i<Carriers.List.Length;i++) {
					row=new ODGridRow();
					row.Cells.Add(Carriers.List[i].CarrierName);
					row.Cells.Add(Carriers.List[i].Phone);
					row.Cells.Add(Carriers.List[i].Address);
					row.Cells.Add(Carriers.List[i].Address2);
					row.Cells.Add(Carriers.List[i].City);
					row.Cells.Add(Carriers.List[i].State);
					row.Cells.Add(Carriers.List[i].Zip);
					row.Cells.Add(Carriers.List[i].ElectID);
					gridMain.Rows.Add(row);
				}
			}
			
			gridMain.EndUpdate();
			//if(tbCarriers.SelectedIndices.Length>0){
			//	tbCarriers.ScrollToLine(tbCarriers.SelectedIndices[0]);
			//}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormCarrierEdit FormCE=null;
			if(checkCDAnet.Checked){
				FormCE=new FormCarrierEdit(PIn.PInt(table.Rows[e.Row]["CarrierNum"].ToString()));
			}
			else{
				FormCE=new FormCarrierEdit(Carriers.List[e.Row].CarrierNum);
			}
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			changed=true;
			FillGrid();
		}

		private void checkCDAnet_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormCarrierEdit FormCE=new FormCarrierEdit(0);
			FormCE.IsNew=true;
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			changed=true;
			FillGrid();
		}

		private void butCombine_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length<2){
				MessageBox.Show(Lan.g(this,"Please select multiple items first while holding down the control key."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Combine all these carriers into a single carrier? This will affect all patients using these carriers.  The next window will let you select which carrier to keep when combining."),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			int[] pickedCarrierNums=new int[gridMain.SelectedIndices.Length];
			if(checkCDAnet.Checked){
				for(int i=0;i<pickedCarrierNums.Length;i++){
					pickedCarrierNums[i]=PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["CarrierNum"].ToString());
				}
			}
			else{
				for(int i=0;i<pickedCarrierNums.Length;i++){
					pickedCarrierNums[i]=Carriers.List[gridMain.SelectedIndices[i]].CarrierNum;
				}
			}
			FormCarrierCombine FormCB=new FormCarrierCombine();
			FormCB.CarrierNums=pickedCarrierNums;
			FormCB.ShowDialog();
			if(FormCB.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			//int[] combCarrierNums=new int[tbCarriers.SelectedIndices.Length];
			//for(int i=0;i<tbCarriers.SelectedIndices.Length;i++){
			//	carrierNums[i]=Carriers.List[tbCarriers.SelectedIndices[i]].CarrierNum;
			//}
			try{
				Carriers.Combine(pickedCarrierNums,FormCB.PickedCarrierNum);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			/*if(IsSelectMode){
				if(tbCarriers.SelectedIndices.Length!=1){
					//Employers.Cur=new Employer();
					MessageBox.Show(Lan.g(this,"Please select one item first."));
					return;
				}
				Carriers.Cur=Carriers.List[tbCarriers.SelectedIndices[0]];
			}*/
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormCarriers_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//it doesn't matter whether the user hits ok, or cancel for this to happen
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Carriers);
			}
		}

		

		

		

		
		

		

		

		

		


	}
}



























