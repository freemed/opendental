using System;
using System.Drawing;
using System.Collections;
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
		private OpenDental.UI.Button butProcButtons;
		private Label label2;
		private OpenDental.UI.Button butAutocodes;
		private Label label1;
		private OpenDental.UI.Button butNewCust;
		private Label label3;
		private Label label4;
		public bool Changed;

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
			this.butClose = new OpenDental.UI.Button();
			this.butProcButtons = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.butAutocodes = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butNewCust = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
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
			this.butClose.Location = new System.Drawing.Point(539,226);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butProcButtons
			// 
			this.butProcButtons.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProcButtons.Autosize = true;
			this.butProcButtons.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProcButtons.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProcButtons.CornerRadius = 4F;
			this.butProcButtons.Location = new System.Drawing.Point(24,175);
			this.butProcButtons.Name = "butProcButtons";
			this.butProcButtons.Size = new System.Drawing.Size(103,26);
			this.butProcButtons.TabIndex = 39;
			this.butProcButtons.Text = "Procedure Buttons";
			this.butProcButtons.Click += new System.EventHandler(this.butProcButtons_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(132,175);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(501,36);
			this.label2.TabIndex = 38;
			this.label2.Text = "Deletes all current ProcButtons from the Chart module, and then adds the default " +
    "ProcButtons.  Procedure codes must have already been entered or they cannot be a" +
    "dded as a ProcButton.";
			// 
			// butAutocodes
			// 
			this.butAutocodes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAutocodes.Autosize = true;
			this.butAutocodes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAutocodes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAutocodes.CornerRadius = 4F;
			this.butAutocodes.Location = new System.Drawing.Point(24,128);
			this.butAutocodes.Name = "butAutocodes";
			this.butAutocodes.Size = new System.Drawing.Size(103,26);
			this.butAutocodes.TabIndex = 37;
			this.butAutocodes.Text = "Autocodes";
			this.butAutocodes.Click += new System.EventHandler(this.butAutocodes_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(132,128);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(443,36);
			this.label1.TabIndex = 36;
			this.label1.Text = "Deletes all current autocodes and then adds the default autocodes.  Procedure cod" +
    "es must have already been entered or they cannot be added as an autocode.";
			// 
			// butNewCust
			// 
			this.butNewCust.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNewCust.Autosize = true;
			this.butNewCust.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNewCust.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNewCust.CornerRadius = 4F;
			this.butNewCust.Location = new System.Drawing.Point(24,12);
			this.butNewCust.Name = "butNewCust";
			this.butNewCust.Size = new System.Drawing.Size(103,26);
			this.butNewCust.TabIndex = 41;
			this.butNewCust.Text = "New Customer";
			this.butNewCust.Click += new System.EventHandler(this.butNewCust_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(132,12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(443,50);
			this.label3.TabIndex = 40;
			this.label3.Text = "Performs all the necessary tasks to make a new database functional.  Removes dumm" +
    "y codes that start with \"T\".  Adds ADA codes if missing.  Resets autocodes and p" +
    "rocedure buttons.";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))),System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label4.Location = new System.Drawing.Point(21,91);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(184,18);
			this.label4.TabIndex = 42;
			this.label4.Text = "Other Tools";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormProcTools
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(644,276);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butNewCust);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butProcButtons);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butAutocodes);
			this.Controls.Add(this.label1);
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
			if(CDT.Class1.GetADAcodes()==""){
				butNewCust.Enabled=false;
			}
		}

		private void butNewCust_Click(object sender,EventArgs e) {
			int rowsInserted=0;
			try{
				rowsInserted=FormProcCodes.ImportProcCodes("",null);//CDT.Class1.GetADAcodes());
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			MessageBox.Show("Procedure codes inserted: "+rowsInserted);
			AutoCodes.SetToDefault();
			ProcButtons.SetToDefault();
			DataValid.SetInvalid(InvalidTypes.AutoCodes | InvalidTypes.ProcButtons | InvalidTypes.Defs);
			MessageBox.Show(Lan.g(this,"Done."));
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"New Customer Procedure codes tool was run.");
		}

		private void butDelete_Click(object sender,EventArgs e) {
			int affectedRows=ProcedureCodes.DeleteUnusedCodes();
			MessageBox.Show(affectedRows.ToString()+Lan.g(this," codes deleted."));
			if(affectedRows>0) {
				Changed=true;
			}
		}

		private void butAutocodes_Click(object sender,EventArgs e) {
			AutoCodes.SetToDefault();
			DataValid.SetInvalid(InvalidTypes.AutoCodes);
			MessageBox.Show(Lan.g(this,"Done."));
		}

		private void butProcButtons_Click(object sender,EventArgs e) {
			ProcButtons.SetToDefault();
			DataValid.SetInvalid(InvalidTypes.ProcButtons | InvalidTypes.Defs);
			MessageBox.Show(Lan.g(this,"Done."));
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		

		


	}
}





















