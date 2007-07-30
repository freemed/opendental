using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProcTools : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private CheckBox checkAutocodes;
		private CheckBox checkTcodes;
		private CheckBox checkDcodes;
		private CheckBox checkNcodes;
		private OpenDental.UI.Button butUncheck;
		private Label label5;
		private CheckBox checkProcButtons;
		private OpenDental.UI.Button butRun;
		public bool Changed;
		private CheckBox checkApptProcsQuickAdd;
		///<summary>The actual list of ADA codes as published by the ADA.  Only available on our compiled releases.  There is no other way to get this info.</summary>
		private List<ProcedureCode> codeList;

		///<summary></summary>
		public FormProcTools()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcTools));
			this.checkAutocodes = new System.Windows.Forms.CheckBox();
			this.checkTcodes = new System.Windows.Forms.CheckBox();
			this.checkDcodes = new System.Windows.Forms.CheckBox();
			this.checkNcodes = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkProcButtons = new System.Windows.Forms.CheckBox();
			this.butRun = new OpenDental.UI.Button();
			this.butUncheck = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.checkApptProcsQuickAdd = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkAutocodes
			// 
			this.checkAutocodes.Checked = true;
			this.checkAutocodes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAutocodes.Location = new System.Drawing.Point(15,238);
			this.checkAutocodes.Name = "checkAutocodes";
			this.checkAutocodes.Size = new System.Drawing.Size(646,36);
			this.checkAutocodes.TabIndex = 43;
			this.checkAutocodes.Text = "Autocodes - Deletes all current autocodes and then adds the default autocodes.  P" +
    "rocedure codes must have already been entered or they cannot be added as an auto" +
    "code.";
			this.checkAutocodes.UseVisualStyleBackColor = true;
			// 
			// checkTcodes
			// 
			this.checkTcodes.Checked = true;
			this.checkTcodes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkTcodes.Location = new System.Drawing.Point(15,115);
			this.checkTcodes.Name = "checkTcodes";
			this.checkTcodes.Size = new System.Drawing.Size(646,36);
			this.checkTcodes.TabIndex = 44;
			this.checkTcodes.Text = "T codes - Remove temp codes, codes that start with \"T\", which were only needed fo" +
    "r the trial version.  If a T code has already been used, then this moves it to t" +
    "he obsolete category.";
			this.checkTcodes.UseVisualStyleBackColor = true;
			// 
			// checkDcodes
			// 
			this.checkDcodes.Checked = true;
			this.checkDcodes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkDcodes.Location = new System.Drawing.Point(15,197);
			this.checkDcodes.Name = "checkDcodes";
			this.checkDcodes.Size = new System.Drawing.Size(646,36);
			this.checkDcodes.TabIndex = 45;
			this.checkDcodes.Text = "D codes - Add any missing ADA codes.  This option does not work in the trial vers" +
    "ion or compiled version.";
			this.checkDcodes.UseVisualStyleBackColor = true;
			// 
			// checkNcodes
			// 
			this.checkNcodes.Checked = true;
			this.checkNcodes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNcodes.Location = new System.Drawing.Point(15,156);
			this.checkNcodes.Name = "checkNcodes";
			this.checkNcodes.Size = new System.Drawing.Size(646,36);
			this.checkNcodes.TabIndex = 46;
			this.checkNcodes.Text = "N codes - Add any missing no-fee codes.";
			this.checkNcodes.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12,9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(666,54);
			this.label5.TabIndex = 48;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// checkProcButtons
			// 
			this.checkProcButtons.Checked = true;
			this.checkProcButtons.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkProcButtons.Location = new System.Drawing.Point(15,279);
			this.checkProcButtons.Name = "checkProcButtons";
			this.checkProcButtons.Size = new System.Drawing.Size(646,36);
			this.checkProcButtons.TabIndex = 49;
			this.checkProcButtons.Text = resources.GetString("checkProcButtons.Text");
			this.checkProcButtons.UseVisualStyleBackColor = true;
			// 
			// butRun
			// 
			this.butRun.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butRun.Autosize = true;
			this.butRun.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRun.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRun.CornerRadius = 4F;
			this.butRun.Location = new System.Drawing.Point(477,384);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(82,26);
			this.butRun.TabIndex = 50;
			this.butRun.Text = "Run Now";
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// butUncheck
			// 
			this.butUncheck.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUncheck.Autosize = true;
			this.butUncheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUncheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUncheck.CornerRadius = 4F;
			this.butUncheck.Location = new System.Drawing.Point(15,78);
			this.butUncheck.Name = "butUncheck";
			this.butUncheck.Size = new System.Drawing.Size(84,22);
			this.butUncheck.TabIndex = 47;
			this.butUncheck.Text = "Uncheck All";
			this.butUncheck.Click += new System.EventHandler(this.butUncheck_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(586,384);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(82,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// checkApptProcsQuickAdd
			// 
			this.checkApptProcsQuickAdd.Checked = true;
			this.checkApptProcsQuickAdd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkApptProcsQuickAdd.Location = new System.Drawing.Point(15,320);
			this.checkApptProcsQuickAdd.Name = "checkApptProcsQuickAdd";
			this.checkApptProcsQuickAdd.Size = new System.Drawing.Size(646,36);
			this.checkApptProcsQuickAdd.TabIndex = 51;
			this.checkApptProcsQuickAdd.Text = "Appt Procs Quick Add - This is the list of procedures that you pick from within t" +
    "he appt edit window.  This resets the list to default.";
			this.checkApptProcsQuickAdd.UseVisualStyleBackColor = true;
			// 
			// FormProcTools
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(698,434);
			this.Controls.Add(this.checkApptProcsQuickAdd);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.checkProcButtons);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butUncheck);
			this.Controls.Add(this.checkNcodes);
			this.Controls.Add(this.checkDcodes);
			this.Controls.Add(this.checkTcodes);
			this.Controls.Add(this.checkAutocodes);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcTools";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Code Tools";
			this.Load += new System.EventHandler(this.FormProcTools_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcTools_Load(object sender,EventArgs e) {
			#if TRIALONLY
				checkTcodes.Checked=false;
				checkNcodes.Checked=false;
				checkDcodes.Checked=false;
				checkAutocodes.Checked=false;
				checkProcButtons.Checked=false;
				checkApptProcsQuickAdd.Checked=false;
				checkTcodes.Enabled=false;
				//checkNcodes.Enabled=false;
				checkDcodes.Enabled=false;
				checkAutocodes.Enabled=false;
				checkProcButtons.Enabled=false;
				checkApptProcsQuickAdd.Enabled=false;
			#endif
			codeList=CDT.Class1.GetADAcodes();
			if(codeList.Count==0){
				checkDcodes.Checked=false;
				checkDcodes.Enabled=false;
			}
		}

		private void butUncheck_Click(object sender,EventArgs e) {
			checkTcodes.Checked=false;
			checkNcodes.Checked=false;
			checkDcodes.Checked=false;
			checkAutocodes.Checked=false;
			checkProcButtons.Checked=false;
			checkApptProcsQuickAdd.Checked=false;
		}

		private void butRun_Click(object sender,EventArgs e) {
			if(!checkTcodes.Checked && !checkNcodes.Checked && !checkDcodes.Checked && !checkAutocodes.Checked 
				&& !checkProcButtons.Checked && !checkApptProcsQuickAdd.Checked)
			{
				MsgBox.Show(this,"Please select at least one tool first.");
				return;
			}
			Changed=true;
			int rowsInserted=0;
			if(checkTcodes.Checked){
				ProcedureCodes.TcodesClear();
				//yes, this really does refresh before moving on.
				DataValid.SetInvalid(InvalidTypes.Defs | InvalidTypes.ProcCodes | InvalidTypes.Fees);
			}
			if(checkNcodes.Checked) {
				try {
					rowsInserted+=FormProcCodes.ImportProcCodes("",new List<ProcedureCode>(),Properties.Resources.NoFeeProcCodes);
				}
				catch(ApplicationException ex) {
					MessageBox.Show(ex.Message);
				}
				DataValid.SetInvalid(InvalidTypes.Defs | InvalidTypes.ProcCodes | InvalidTypes.Fees);
				//fees are included because they are grouped by defs.
			}
			if(checkDcodes.Checked) {
				try {
					rowsInserted+=FormProcCodes.ImportProcCodes("",codeList,"");
				}
				catch(ApplicationException ex) {
					MessageBox.Show(ex.Message);
				}
				DataValid.SetInvalid(InvalidTypes.Defs | InvalidTypes.ProcCodes | InvalidTypes.Fees);
			}
			if(checkNcodes.Checked || checkDcodes.Checked){
				MessageBox.Show("Procedure codes inserted: "+rowsInserted);
			}
			if(checkAutocodes.Checked) {
				AutoCodes.SetToDefault();
				DataValid.SetInvalid(InvalidTypes.AutoCodes);
			}
			if(checkProcButtons.Checked) {
				ProcButtons.SetToDefault();
				DataValid.SetInvalid(InvalidTypes.ProcButtons | InvalidTypes.Defs);
			}
			if(checkApptProcsQuickAdd.Checked) {
				ProcedureCodes.ResetApptProcsQuickAdd();
				DataValid.SetInvalid(InvalidTypes.Defs);
			}
			MessageBox.Show(Lan.g(this,"Done."));
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"New Customer Procedure codes tool was run.");
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	

	

		

		

		

		


	}
}





















