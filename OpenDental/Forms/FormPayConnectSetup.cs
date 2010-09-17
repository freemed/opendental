using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPayConnectSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private LinkLabel linkLabel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private CheckBox checkEnabled;
		private Label label1;
		private ComboBox comboPaymentType;
		private Program prog;
		private Label label2;
		private TextBox textUsername;
		private Label label3;
		private TextBox textPassword;
		private ProgramProperty propUsername=null;
		private ProgramProperty propPassword=null;
		private ProgramProperty propPayType=null;

		///<summary></summary>
		public FormPayConnectSetup()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayConnectSetup));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboPaymentType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textUsername = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
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
			this.butCancel.Location = new System.Drawing.Point(243,227);
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
			this.butOK.Location = new System.Drawing.Point(146,227);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(29,28);
			this.linkLabel1.Location = new System.Drawing.Point(20,20);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(312,17);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "The PayConnect website is at http://www.dentalxchange.com";
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// checkEnabled
			// 
			this.checkEnabled.Location = new System.Drawing.Point(21,49);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.Size = new System.Drawing.Size(104,18);
			this.checkEnabled.TabIndex = 4;
			this.checkEnabled.Text = "Enabled";
			this.checkEnabled.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18,158);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154,16);
			this.label1.TabIndex = 53;
			this.label1.Text = "Payment Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboPaymentType
			// 
			this.comboPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPaymentType.FormattingEnabled = true;
			this.comboPaymentType.Location = new System.Drawing.Point(21,177);
			this.comboPaymentType.MaxDropDownItems = 25;
			this.comboPaymentType.Name = "comboPaymentType";
			this.comboPaymentType.Size = new System.Drawing.Size(205,21);
			this.comboPaymentType.TabIndex = 54;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18,77);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55,13);
			this.label2.TabIndex = 55;
			this.label2.Text = "Username";
			// 
			// textUsername
			// 
			this.textUsername.Location = new System.Drawing.Point(20,94);
			this.textUsername.Name = "textUsername";
			this.textUsername.Size = new System.Drawing.Size(206,20);
			this.textUsername.TabIndex = 56;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18,118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53,13);
			this.label3.TabIndex = 57;
			this.label3.Text = "Password";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(21,135);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(205,20);
			this.textPassword.TabIndex = 58;
			// 
			// FormPayConnectSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(344,274);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textUsername);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboPaymentType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkEnabled);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayConnectSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PayConnect Setup";
			this.Load += new System.EventHandler(this.FormXchargeSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormXchargeSetup_Load(object sender,EventArgs e) {
			prog=Programs.GetCur("PayConnect");
			if(prog==null){
				return;
			}
			checkEnabled.Checked=prog.Enabled;
			List <ProgramProperty> props=ProgramProperties.GetListForProgram(prog.ProgramNum);
			for(int i=0;i<props.Count;i++){
				if(props[i].PropertyDesc=="Username"){
					propUsername=props[i];
				}
				else if(props[i].PropertyDesc=="Password"){
					propPassword=props[i];
				}
				else if(props[i].PropertyDesc=="PaymentType"){
					propPayType=props[i];
				}
			}
			textUsername.Text=propUsername.PropertyValue;
			textPassword.Text=propPassword.PropertyValue;
			for(int i=0;i<DefC.Short[(int)DefCat.PaymentTypes].Length;i++) {
				comboPaymentType.Items.Add(DefC.Short[(int)DefCat.PaymentTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.PaymentTypes][i].DefNum.ToString()==propPayType.PropertyValue)
					comboPaymentType.SelectedIndex=i;
			}
		}

		private void linkLabel1_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://www.dentalxchange.com");
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(prog==null){
				MsgBox.Show(this,"PayConnect entry is missing from the database.");//should never happen
				return;
			}
			if(comboPaymentType.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a payment type first.");
				return;
			}
			prog.Enabled=checkEnabled.Checked;
			Programs.Update(prog);
			propUsername.PropertyValue=textUsername.Text;
			ProgramProperties.Update(propUsername);
			propPassword.PropertyValue=textPassword.Text;
			ProgramProperties.Update(propPassword);
			propPayType.PropertyValue=DefC.Short[(int)DefCat.PaymentTypes][comboPaymentType.SelectedIndex].DefNum.ToString();
			ProgramProperties.Update(propPayType);
			DataValid.SetInvalid(InvalidType.Programs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















