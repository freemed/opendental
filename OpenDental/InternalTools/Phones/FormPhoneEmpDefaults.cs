using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPhoneEmpDefaults:System.Windows.Forms.Form {
		private OpenDental.UI.ODGrid gridMain;
		private IContainer components;
		private OpenDental.UI.Button butClose;
		private UI.Button butAdd;
		private List<PhoneEmpDefault> ListPED;

		///<summary></summary>
		public FormPhoneEmpDefaults()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPhoneEmpDefaults));
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(879, 546);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 11;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Location = new System.Drawing.Point(446, 546);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 24);
			this.butAdd.TabIndex = 12;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.AllowSortingByColumn = true;
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(8, 14);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(946, 524);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = "Phone Settings";
			this.gridMain.TranslationName = "";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormPhoneEmpDefaults
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(966, 582);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.gridMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPhoneEmpDefaults";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Phone Settings";
			this.Load += new System.EventHandler(this.FormAccountPick_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAccountPick_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("EmployeeNum",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("EmpName",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("IsGraphed",65,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("HasColor",60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("RingGroup",65);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("PhoneExt",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("StatusOverride",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Notes",250);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ComputerName",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Private",50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Triage",50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			ListPED=PhoneEmpDefaults.Refresh();
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListPED.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ListPED[i].EmployeeNum.ToString());
				row.Cells.Add(ListPED[i].EmpName);
				row.Cells.Add(ListPED[i].IsGraphed?"X":"");
				row.Cells.Add(ListPED[i].HasColor?"X":"");
				row.Cells.Add(ListPED[i].RingGroups.ToString());
				row.Cells.Add(ListPED[i].PhoneExt.ToString());
				if(ListPED[i].StatusOverride==PhoneEmpStatusOverride.None) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ListPED[i].StatusOverride.ToString());
				}
				row.Cells.Add(ListPED[i].Notes);
				row.Cells.Add(ListPED[i].ComputerName);
				row.Cells.Add(ListPED[i].IsPrivateScreen?"X":"");
				row.Cells.Add(ListPED[i].IsTriageOperator?"X":"");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPhoneEmpDefaultEdit formPED=new FormPhoneEmpDefaultEdit();
			formPED.PedCur=ListPED[e.Row];
			formPED.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPhoneEmpDefaultEdit formPED=new FormPhoneEmpDefaultEdit();
			formPED.PedCur=new PhoneEmpDefault();
			formPED.IsNew=true;
			formPED.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		

	

		

		

	


	}
}





















