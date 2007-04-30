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
	public class FormApptRuleEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private Label label1;
		private TextBox textRuleDesc;
		private TextBox textCodeStart;
		private Label label2;
		private TextBox textCodeEnd;
		private Label label3;
		private CheckBox checkIsEnabled;
		private AppointmentRule ApptRuleCur;

		///<summary></summary>
		public FormApptRuleEdit(AppointmentRule apptRuleCur)
		{
			//
			// Required for Windows Form Designer support
			//
			ApptRuleCur=apptRuleCur.Copy();
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
			OpenDental.UI.Button butDelete;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptRuleEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textRuleDesc = new System.Windows.Forms.TextBox();
			this.textCodeStart = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textCodeEnd = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkIsEnabled = new System.Windows.Forms.CheckBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butDelete
			// 
			butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			butDelete.Autosize = true;
			butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butDelete.CornerRadius = 4F;
			butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			butDelete.Location = new System.Drawing.Point(15,178);
			butDelete.Name = "butDelete";
			butDelete.Size = new System.Drawing.Size(75,26);
			butDelete.TabIndex = 16;
			butDelete.Text = "&Delete";
			butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111,20);
			this.label1.TabIndex = 11;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRuleDesc
			// 
			this.textRuleDesc.Location = new System.Drawing.Point(125,25);
			this.textRuleDesc.Name = "textRuleDesc";
			this.textRuleDesc.Size = new System.Drawing.Size(297,20);
			this.textRuleDesc.TabIndex = 0;
			// 
			// textCodeStart
			// 
			this.textCodeStart.Location = new System.Drawing.Point(125,51);
			this.textCodeStart.Name = "textCodeStart";
			this.textCodeStart.Size = new System.Drawing.Size(113,20);
			this.textCodeStart.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111,20);
			this.label2.TabIndex = 18;
			this.label2.Text = "Code Start";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeEnd
			// 
			this.textCodeEnd.Location = new System.Drawing.Point(125,77);
			this.textCodeEnd.Name = "textCodeEnd";
			this.textCodeEnd.Size = new System.Drawing.Size(113,20);
			this.textCodeEnd.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111,20);
			this.label3.TabIndex = 20;
			this.label3.Text = "Code End";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsEnabled
			// 
			this.checkIsEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsEnabled.Location = new System.Drawing.Point(9,103);
			this.checkIsEnabled.Name = "checkIsEnabled";
			this.checkIsEnabled.Size = new System.Drawing.Size(130,21);
			this.checkIsEnabled.TabIndex = 3;
			this.checkIsEnabled.Text = "Is Enabled";
			this.checkIsEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsEnabled.UseVisualStyleBackColor = true;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(347,146);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 4;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(347,178);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormApptRuleEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(448,222);
			this.Controls.Add(this.checkIsEnabled);
			this.Controls.Add(this.textCodeEnd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textCodeStart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textRuleDesc);
			this.Controls.Add(butDelete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptRuleEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Appointment Rule";
			this.Load += new System.EventHandler(this.FormApptRuleEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormApptRuleEdit_Load(object sender, System.EventArgs e) {
			textRuleDesc.Text=ApptRuleCur.RuleDesc;
			textCodeStart.Text=ApptRuleCur.CodeStart;
			textCodeEnd.Text=ApptRuleCur.CodeEnd;
			checkIsEnabled.Checked=ApptRuleCur.IsEnabled;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			AppointmentRules.Delete(ApptRuleCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textRuleDesc.Text==""){
				MsgBox.Show(this,"Description not allowed to be blank.");
				return;
			}
			if(!ProcedureCodes.IsValidCode(textCodeStart.Text)
				|| !ProcedureCodes.IsValidCode(textCodeEnd.Text))
			{
				MsgBox.Show(this,"Start and end codes must be valid procedure codes.");
				return;
			}
			ApptRuleCur.RuleDesc=textRuleDesc.Text;
			ApptRuleCur.CodeStart=textCodeStart.Text;
			ApptRuleCur.CodeEnd=textCodeEnd.Text;
			ApptRuleCur.IsEnabled=checkIsEnabled.Checked;
			if(IsNew){
				AppointmentRules.Insert(ApptRuleCur);
			}
			else{
				AppointmentRules.Update(ApptRuleCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		

		


	}
}





















