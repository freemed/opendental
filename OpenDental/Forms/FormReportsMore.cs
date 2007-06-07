using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReportsMore : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label1;
		private ListBox listPublicHealth;
		private ListBox listLists;
		private Label label2;
		private ListBox listBox1;
		private Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormReportsMore()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportsMore));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listPublicHealth = new System.Windows.Forms.ListBox();
			this.listLists = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(592,513);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(592,472);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(463,93);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118,18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Public Health";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPublicHealth
			// 
			this.listPublicHealth.FormattingEnabled = true;
			this.listPublicHealth.Items.AddRange(new object[] {
            "Raw Screening Data",
            "Raw Population Data",
            "Screening Report"});
			this.listPublicHealth.Location = new System.Drawing.Point(466,114);
			this.listPublicHealth.Name = "listPublicHealth";
			this.listPublicHealth.Size = new System.Drawing.Size(120,43);
			this.listPublicHealth.TabIndex = 3;
			// 
			// listLists
			// 
			this.listLists.FormattingEnabled = true;
			this.listLists.Items.AddRange(new object[] {
            "Appointments",
            "Birthdays",
            "Insurance Plans",
            "Patients",
            "Prescriptions",
            "Procedure Codes",
            "Referrals",
            "Routing Slips"});
			this.listLists.Location = new System.Drawing.Point(301,271);
			this.listLists.Name = "listLists";
			this.listLists.Size = new System.Drawing.Size(120,108);
			this.listLists.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(298,250);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118,18);
			this.label2.TabIndex = 4;
			this.label2.Text = "Lists";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Items.AddRange(new object[] {
            "Course Grades",
            "Requirements"});
			this.listBox1.Location = new System.Drawing.Point(466,194);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(120,30);
			this.listBox1.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(463,173);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118,18);
			this.label3.TabIndex = 6;
			this.label3.Text = "Dental Schools";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormReportsMore
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(719,564);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listLists);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listPublicHealth);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReportsMore";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reports";
			this.Load += new System.EventHandler(this.FormReportsMore_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReportsMore_Load(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















