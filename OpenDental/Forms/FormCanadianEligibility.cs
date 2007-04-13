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
	public class FormCanadianEligibility : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label12;
		private ListBox listEligibilityCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ValidDate textDate;
		private Label label6;
		public int EligibilityCode;
		public DateTime AsOfDate;

		///<summary></summary>
		public FormCanadianEligibility()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCanadianEligibility));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.listEligibilityCode = new System.Windows.Forms.ListBox();
			this.textDate = new OpenDental.ValidDate();
			this.label6 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(275,194);
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
			this.butOK.Location = new System.Drawing.Point(275,153);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label12
			// 
			this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label12.Location = new System.Drawing.Point(38,25);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(211,20);
			this.label12.TabIndex = 5;
			this.label12.Text = "Eligibility Exception Code";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listEligibilityCode
			// 
			this.listEligibilityCode.FormattingEnabled = true;
			this.listEligibilityCode.Location = new System.Drawing.Point(38,48);
			this.listEligibilityCode.Name = "listEligibilityCode";
			this.listEligibilityCode.Size = new System.Drawing.Size(120,56);
			this.listEligibilityCode.TabIndex = 4;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(38,138);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(72,20);
			this.textDate.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(35,120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(135,15);
			this.label6.TabIndex = 8;
			this.label6.Text = "As of date";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormCanadianEligibility
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(402,245);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.listEligibilityCode);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCanadianEligibility";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Canadian Eligibility";
			this.Load += new System.EventHandler(this.FormCanadianEligibility_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormCanadianEligibility_Load(object sender,EventArgs e) {
			listEligibilityCode.Items.Add("Full-time student");//0
			listEligibilityCode.Items.Add("Disabled");
			listEligibilityCode.Items.Add("Disabled student");
			listEligibilityCode.Items.Add("Code not applicable");
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDate.errorProvider1.GetError(textDate)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listEligibilityCode.SelectedIndex==-1) {
				MsgBox.Show(this,"Eligibility Code must be selected.");
				return;
			}
			EligibilityCode=listEligibilityCode.SelectedIndex+1;
			if(textDate.Text==""){
				AsOfDate=DateTime.Today;
			}
			else{
				AsOfDate=PIn.PDate(textDate.Text);
			}
			if(AsOfDate.Year<1980 || AsOfDate.Year>DateTime.Today.Year+10){
				MsgBox.Show(this,"Unreasonable date.");
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















