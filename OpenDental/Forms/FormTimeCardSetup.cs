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
	public class FormTimeCardSetup:System.Windows.Forms.Form {
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private CheckBox checkUseDecimal;
		private ODGrid gridRules;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butAddRule;
		private bool changed;

		///<summary></summary>
		public FormTimeCardSetup()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeCardSetup));
			this.checkUseDecimal = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butAddRule = new OpenDental.UI.Button();
			this.gridRules = new OpenDental.UI.ODGrid();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkUseDecimal
			// 
			this.checkUseDecimal.Location = new System.Drawing.Point(12,19);
			this.checkUseDecimal.Name = "checkUseDecimal";
			this.checkUseDecimal.Size = new System.Drawing.Size(295,18);
			this.checkUseDecimal.TabIndex = 12;
			this.checkUseDecimal.Text = "Use decimal format rather than colon format";
			this.checkUseDecimal.UseVisualStyleBackColor = true;
			this.checkUseDecimal.Click += new System.EventHandler(this.checkUseDecimal_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.checkUseDecimal);
			this.groupBox1.Location = new System.Drawing.Point(19,536);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(391,55);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// butAddRule
			// 
			this.butAddRule.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddRule.Autosize = true;
			this.butAddRule.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddRule.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddRule.CornerRadius = 4F;
			this.butAddRule.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddRule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddRule.Location = new System.Drawing.Point(305,499);
			this.butAddRule.Name = "butAddRule";
			this.butAddRule.Size = new System.Drawing.Size(80,24);
			this.butAddRule.TabIndex = 15;
			this.butAddRule.Text = "Add";
			this.butAddRule.Click += new System.EventHandler(this.butAddRule_Click);
			// 
			// gridRules
			// 
			this.gridRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridRules.HScrollVisible = false;
			this.gridRules.Location = new System.Drawing.Point(305,12);
			this.gridRules.Name = "gridRules";
			this.gridRules.ScrollValue = 0;
			this.gridRules.Size = new System.Drawing.Size(394,481);
			this.gridRules.TabIndex = 13;
			this.gridRules.Title = "Rules";
			this.gridRules.TranslationName = "FormTimeCardSetup";
			this.gridRules.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridRules_CellDoubleClick);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(19,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(272,481);
			this.gridMain.TabIndex = 11;
			this.gridMain.Title = "Pay Periods";
			this.gridMain.TranslationName = "TablePayPeriods";
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
			this.butAdd.Location = new System.Drawing.Point(19,499);
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
			this.butClose.Location = new System.Drawing.Point(628,567);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormTimeCardSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(727,608);
			this.Controls.Add(this.butAddRule);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridRules);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTimeCardSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card Setup";
			this.Load += new System.EventHandler(this.FormPayPeriods_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPayPeriods_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPayPeriods_Load(object sender, System.EventArgs e) {
			checkUseDecimal.Checked=PrefC.GetBool(PrefName.TimeCardsUseDecimalInsteadOfColon);
			Employees.RefreshCache();
			FillGrid();
			FillRules();
		}

		private void FillGrid(){
			PayPeriods.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Start Date",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("End Date",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Paycheck Date",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			UI.ODGridRow row;
			for(int i=0;i<PayPeriods.List.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(PayPeriods.List[i].DateStart.ToShortDateString());
				row.Cells.Add(PayPeriods.List[i].DateStop.ToShortDateString());
				if(PayPeriods.List[i].DatePaycheck.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(PayPeriods.List[i].DatePaycheck.ToShortDateString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void FillRules(){
			TimeCardRules.RefreshCache();
			gridRules.BeginUpdate();
			gridRules.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Employee",150);
			gridRules.Columns.Add(col);
			col=new ODGridColumn("OT after x Hours",110);
			gridRules.Columns.Add(col);
			col=new ODGridColumn("OT after x Time",70);
			gridRules.Columns.Add(col);
			gridRules.Rows.Clear();
			UI.ODGridRow row;
			for(int i=0;i<TimeCardRules.Listt.Count;i++){
				row=new OpenDental.UI.ODGridRow();
				if(TimeCardRules.Listt[i].EmployeeNum==0){
					row.Cells.Add(Lan.g(this,"All Employees"));
				}
				else{
					Employee emp=Employees.GetEmp(TimeCardRules.Listt[i].EmployeeNum);
					row.Cells.Add(emp.FName+" "+emp.LName);
				}
				row.Cells.Add(TimeCardRules.Listt[i].OverHoursPerDay.ToStringHmm());
				row.Cells.Add(TimeCardRules.Listt[i].AfterTimeOfDay.ToStringHmm());
				gridRules.Rows.Add(row);
			}
			gridRules.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormPayPeriodEdit FormP=new FormPayPeriodEdit(PayPeriods.List[e.Row]);
			FormP.ShowDialog();
			FillGrid();
			changed=true;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			PayPeriod payPeriodCur=new PayPeriod();
			if(PayPeriods.List.Length==0){
				payPeriodCur.DateStart=DateTime.Today;
			}
			else{
				payPeriodCur.DateStart=PayPeriods.List[PayPeriods.List.Length-1].DateStop.AddDays(1);
			}
			payPeriodCur.DateStop=payPeriodCur.DateStart.AddDays(14);
			payPeriodCur.DatePaycheck=payPeriodCur.DateStop.AddDays(4);
			FormPayPeriodEdit FormP=new FormPayPeriodEdit(payPeriodCur);
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				return;
			}
			FillGrid();
			changed=true;
		}

		private void checkUseDecimal_Click(object sender,EventArgs e) {
			if(Prefs.UpdateBool(PrefName.TimeCardsUseDecimalInsteadOfColon,checkUseDecimal.Checked)){
				changed=true;
			}
		}

		private void butAddRule_Click(object sender,EventArgs e) {
			TimeCardRule rule=new TimeCardRule();
			rule.IsNew=true;
			FormTimeCardRuleEdit FormT=new FormTimeCardRuleEdit();
			FormT.timeCardRule=rule;
			FormT.ShowDialog();
			FillRules();
			changed=true;
		}

		private void gridRules_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormTimeCardRuleEdit FormT=new FormTimeCardRuleEdit();
			FormT.timeCardRule=TimeCardRules.Listt[e.Row];
			FormT.ShowDialog();
			FillRules();
			changed=true;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormPayPeriods_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed) {
				DataValid.SetInvalid(InvalidType.Employees,InvalidType.Prefs,InvalidType.TimeCardRules);
			}
		}

	

		

		

		



		
	}
}





















