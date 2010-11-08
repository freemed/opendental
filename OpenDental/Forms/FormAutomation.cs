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
	public class FormAutomation:System.Windows.Forms.Form {
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private Label label1;
		private bool changed;

		///<summary></summary>
		public FormAutomation()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutomation));
			this.label1 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(21,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(485,22);
			this.label1.TabIndex = 12;
			this.label1.Text = "One trigger event will cause one action to be taken";
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(21,34);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(735,394);
			this.gridMain.TabIndex = 11;
			this.gridMain.Title = "Automation";
			this.gridMain.TranslationName = "FormAutomation";
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
			this.butAdd.Location = new System.Drawing.Point(19,451);
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
			this.butClose.Location = new System.Drawing.Point(681,451);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormAutomation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(788,500);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutomation";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Automation";
			this.Load += new System.EventHandler(this.FormAutomation_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAutomation_FormClosing);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAutomation_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Automations.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Description",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Trigger",150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Action",150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Details",200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			UI.ODGridRow row;
			string detail;
			for(int i=0;i<Automations.Listt.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(Automations.Listt[i].Description);
				if(Automations.Listt[i].Autotrigger==AutomationTrigger.CompleteProcedure) {
					row.Cells.Add(Automations.Listt[i].ProcCodes);
				}
				else {
					row.Cells.Add(Automations.Listt[i].Autotrigger.ToString());
				}
				row.Cells.Add(Automations.Listt[i].AutoAction.ToString());
				//details: 
				detail="";
				if(Automations.Listt[i].AutoAction==AutomationAction.CreateCommlog) {
					detail+=DefC.GetName(DefCat.CommLogTypes,Automations.Listt[i].CommType)
						+".  "+Automations.Listt[i].MessageContent;
				}
				else if(Automations.Listt[i].AutoAction==AutomationAction.PrintPatientLetter) {
					detail+=SheetDefs.GetDescription(Automations.Listt[i].SheetDefNum);
				}
				else if(Automations.Listt[i].AutoAction==AutomationAction.PrintReferralLetter) {
					detail+=SheetDefs.GetDescription(Automations.Listt[i].SheetDefNum);
				}
				row.Cells.Add(detail);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormAutomationEdit FormA=new FormAutomationEdit(Automations.Listt[e.Row]);
			FormA.ShowDialog();
			FillGrid();
			changed=true;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Automation auto=new Automation();
			Automations.Insert(auto);//so that we can attach conditions
			FormAutomationEdit FormA=new FormAutomationEdit(auto);
			FormA.IsNew=true;
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK){
				return;
			}
			FillGrid();
			changed=true;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormAutomation_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed) {
				DataValid.SetInvalid(InvalidType.Automation);
			}
		}

		

		

		



		
	}
}





















