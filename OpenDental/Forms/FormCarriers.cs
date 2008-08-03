using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
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
		private OpenDental.UI.ODGrid gridMain;
		private CheckBox checkCDAnet;
		private bool changed;
		private CheckBox checkShowHidden;
		private TextBox textCarrier;
		private Label label2;//keeps track of whether an update is necessary.
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
			this.butAdd = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butCombine = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.checkCDAnet = new System.Windows.Forms.CheckBox();
			this.checkShowHidden = new System.Windows.Forms.CheckBox();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
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
			this.butAdd.Location = new System.Drawing.Point(830,463);
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
			this.butCombine.Location = new System.Drawing.Point(830,501);
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
			this.butCancel.Location = new System.Drawing.Point(830,623);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(90,26);
			this.butCancel.TabIndex = 12;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(11,29);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(796,620);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Carriers";
			this.gridMain.TranslationName = "TableCarriers";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// checkCDAnet
			// 
			this.checkCDAnet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCDAnet.Location = new System.Drawing.Point(428,6);
			this.checkCDAnet.Name = "checkCDAnet";
			this.checkCDAnet.Size = new System.Drawing.Size(96,17);
			this.checkCDAnet.TabIndex = 99;
			this.checkCDAnet.Text = "CDAnet Only";
			this.checkCDAnet.Click += new System.EventHandler(this.checkCDAnet_Click);
			// 
			// checkShowHidden
			// 
			this.checkShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowHidden.Location = new System.Drawing.Point(287,6);
			this.checkShowHidden.Name = "checkShowHidden";
			this.checkShowHidden.Size = new System.Drawing.Size(96,17);
			this.checkShowHidden.TabIndex = 100;
			this.checkShowHidden.Text = "Show Hidden";
			this.checkShowHidden.Click += new System.EventHandler(this.checkShowHidden_Click);
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(118,4);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(140,20);
			this.textCarrier.TabIndex = 101;
			this.textCarrier.TextChanged += new System.EventHandler(this.textCarrier_TextChanged);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(12,7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 102;
			this.label2.Text = "Carrier";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormCarriers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(927,672);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkShowHidden);
			this.Controls.Add(this.checkCDAnet);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butCombine);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCarriers";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Carriers";
			this.Load += new System.EventHandler(this.FormCarriers_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormCarriers_Closing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormCarriers_Load(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				checkCDAnet.Checked=true;
			}
			else{
				checkCDAnet.Visible=false;
			}
			Carriers.Refresh();
			FillGrid();
		}

		private void FillGrid(){
			List<string> selectedCarrierNums=new List<string>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				selectedCarrierNums.Add(table.Rows[gridMain.SelectedIndices[i]]["CarrierNum"].ToString());
			}
			//Carriers.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			if(checkCDAnet.Checked){
				//gridMain.Size=new Size(745,gridMain.Height);
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
				col=new ODGridColumn(Lan.g("TableCarriers","Hidden"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
			}
			else{
				//gridMain.Size=new Size(839,gridMain.Height);
				col=new ODGridColumn(Lan.g("TableCarriers","Carrier Name"),160);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Phone"),90);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Address"),130);
				gridMain.Columns.Add(col);
				//col=new ODGridColumn(Lan.g("TableCarriers","Address2"),120);
				//gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","City"),90);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","ST"),50);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Zip"),70);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","ElectID"),50);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Hidden"),50,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableCarriers","Plans"),50);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			table=Carriers.GetBigList(checkCDAnet.Checked,checkShowHidden.Checked,textCarrier.Text);
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				if(checkCDAnet.Checked){
					row.Cells.Add(table.Rows[i]["CarrierName"].ToString());
					row.Cells.Add(table.Rows[i]["ElectID"].ToString());
					row.Cells.Add(table.Rows[i]["pMP"].ToString());
					row.Cells.Add(table.Rows[i]["network"].ToString());
					row.Cells.Add(table.Rows[i]["version"].ToString());
					row.Cells.Add(table.Rows[i]["trans02"].ToString());
					row.Cells.Add(table.Rows[i]["trans03"].ToString());
					row.Cells.Add(table.Rows[i]["trans04"].ToString());
					row.Cells.Add(table.Rows[i]["trans05"].ToString());
					row.Cells.Add(table.Rows[i]["trans06"].ToString());
					row.Cells.Add(table.Rows[i]["trans07"].ToString());
					row.Cells.Add(table.Rows[i]["trans08"].ToString());
					row.Cells.Add(table.Rows[i]["isHidden"].ToString());
				}
				else{
					row.Cells.Add(table.Rows[i]["CarrierName"].ToString());
					row.Cells.Add(table.Rows[i]["Phone"].ToString());
					row.Cells.Add(table.Rows[i]["Address"].ToString());
					//row.Cells.Add(table.Rows[i]["Address2"].ToString());
					row.Cells.Add(table.Rows[i]["City"].ToString());
					row.Cells.Add(table.Rows[i]["State"].ToString());
					row.Cells.Add(table.Rows[i]["Zip"].ToString());
					row.Cells.Add(table.Rows[i]["ElectID"].ToString());
					row.Cells.Add(table.Rows[i]["isHidden"].ToString());
					row.Cells.Add(table.Rows[i]["insPlanCount"].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			for(int i=0;i<table.Rows.Count;i++){
				if(selectedCarrierNums.Contains(table.Rows[i]["CarrierNum"].ToString())){
					gridMain.SetSelected(i,true);
				}
			}
			//if(tbCarriers.SelectedIndices.Length>0){
			//	tbCarriers.ScrollToLine(tbCarriers.SelectedIndices[0]);
			//}
		}

		private void textCarrier_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.CarrierCur=Carriers.GetCarrier(PIn.PInt(table.Rows[e.Row]["CarrierNum"].ToString()));
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillGrid();
		}

		private void checkCDAnet_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkShowHidden_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.IsNew=true;
			FormCE.CarrierCur=new Carrier();
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillGrid();
			for(int i=0;i<table.Rows.Count;i++){
				if(FormCE.CarrierCur.CarrierNum.ToString()==table.Rows[i]["CarrierNum"].ToString()){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butCombine_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length<2){
				MessageBox.Show(Lan.g(this,"Please select multiple items first while holding down the control key."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Combine all these carriers into a single carrier? This will affect all patients using these carriers.  The next window will let you select which carrier to keep when combining."),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK)
			{
				return;
			}
			int[] pickedCarrierNums=new int[gridMain.SelectedIndices.Length];
			for(int i=0;i<pickedCarrierNums.Length;i++){
				pickedCarrierNums[i]=PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["CarrierNum"].ToString());
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
				DataValid.SetInvalid(InvalidType.Carriers);
			}
		}

		

		

		

		

		

		
		

		

		

		

		


	}
}



























