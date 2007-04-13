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
	public class FormRxSetupPrinting : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private GroupBox groupBox1;
		private RadioButton radioHorizontal;
		private RadioButton radioVertical;
		private GroupBox groupBox2;
		private Label label1;
		private ValidDouble textDown;
		private Label label2;
		private ValidDouble textRight;
		private GroupBox groupBox3;
		private RadioButton radioTwoSig;
		private RadioButton radioNeither;
		private RadioButton radioGeneric;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRxSetupPrinting()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRxSetupPrinting));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioVertical = new System.Windows.Forms.RadioButton();
			this.radioHorizontal = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textDown = new OpenDental.ValidDouble();
			this.label2 = new System.Windows.Forms.Label();
			this.textRight = new OpenDental.ValidDouble();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.radioTwoSig = new System.Windows.Forms.RadioButton();
			this.radioNeither = new System.Windows.Forms.RadioButton();
			this.radioGeneric = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(352,220);
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
			this.butOK.Location = new System.Drawing.Point(352,179);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioVertical);
			this.groupBox1.Controls.Add(this.radioHorizontal);
			this.groupBox1.Location = new System.Drawing.Point(16,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(159,63);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Orientation";
			// 
			// radioVertical
			// 
			this.radioVertical.Location = new System.Drawing.Point(15,39);
			this.radioVertical.Name = "radioVertical";
			this.radioVertical.Size = new System.Drawing.Size(133,18);
			this.radioVertical.TabIndex = 4;
			this.radioVertical.Text = "Vertical";
			this.radioVertical.UseVisualStyleBackColor = true;
			// 
			// radioHorizontal
			// 
			this.radioHorizontal.Checked = true;
			this.radioHorizontal.Location = new System.Drawing.Point(15,19);
			this.radioHorizontal.Name = "radioHorizontal";
			this.radioHorizontal.Size = new System.Drawing.Size(133,18);
			this.radioHorizontal.TabIndex = 3;
			this.radioHorizontal.TabStop = true;
			this.radioHorizontal.Text = "Horizontal";
			this.radioHorizontal.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textDown);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textRight);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(16,93);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(159,76);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Adjust Position in Inches";
			// 
			// textDown
			// 
			this.textDown.Location = new System.Drawing.Point(73,44);
			this.textDown.Name = "textDown";
			this.textDown.Size = new System.Drawing.Size(73,20);
			this.textDown.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60,20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Down";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRight
			// 
			this.textRight.Location = new System.Drawing.Point(73,19);
			this.textRight.Name = "textRight";
			this.textRight.Size = new System.Drawing.Size(73,20);
			this.textRight.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60,20);
			this.label1.TabIndex = 4;
			this.label1.Text = "Right";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.radioTwoSig);
			this.groupBox3.Controls.Add(this.radioNeither);
			this.groupBox3.Controls.Add(this.radioGeneric);
			this.groupBox3.Location = new System.Drawing.Point(206,12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(220,87);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Generic Substitution";
			// 
			// radioTwoSig
			// 
			this.radioTwoSig.Location = new System.Drawing.Point(15,59);
			this.radioTwoSig.Name = "radioTwoSig";
			this.radioTwoSig.Size = new System.Drawing.Size(193,18);
			this.radioTwoSig.TabIndex = 6;
			this.radioTwoSig.Text = "Two signature lines";
			this.radioTwoSig.UseVisualStyleBackColor = true;
			// 
			// radioNeither
			// 
			this.radioNeither.Location = new System.Drawing.Point(15,39);
			this.radioNeither.Name = "radioNeither";
			this.radioNeither.Size = new System.Drawing.Size(193,18);
			this.radioNeither.TabIndex = 5;
			this.radioNeither.Text = "Neither box checked";
			this.radioNeither.UseVisualStyleBackColor = true;
			// 
			// radioGeneric
			// 
			this.radioGeneric.Checked = true;
			this.radioGeneric.Location = new System.Drawing.Point(15,19);
			this.radioGeneric.Name = "radioGeneric";
			this.radioGeneric.Size = new System.Drawing.Size(193,18);
			this.radioGeneric.TabIndex = 4;
			this.radioGeneric.TabStop = true;
			this.radioGeneric.Text = "Generic box checked";
			this.radioGeneric.UseVisualStyleBackColor = true;
			// 
			// FormRxSetupPrinting
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(479,286);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxSetupPrinting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Rx Printing";
			this.Load += new System.EventHandler(this.FormRxSetupPrinting_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRxSetupPrinting_Load(object sender,EventArgs e) {
			if(PrefB.GetBool("RxOrientVert")) {
				radioVertical.Checked=true;
			}
			else {
				radioHorizontal.Checked=true;
			}
			textRight.Text=PrefB.GetDouble("RxAdjustRight").ToString();
			textDown.Text=PrefB.GetDouble("RxAdjustDown").ToString();
			if(PrefB.GetInt("RxGeneric")==0) {
				radioGeneric.Checked=true;
			}
			else if(PrefB.GetInt("RxGeneric")==1){
				radioNeither.Checked=true;
			}
			else{
				radioTwoSig.Checked=true;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textRight.errorProvider1.GetError(textRight)!=""
				|| textDown.errorProvider1.GetError(textDown)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			bool changed=false;
			if(Prefs.UpdateBool("RxOrientVert",radioVertical.Checked)
				| Prefs.UpdateDouble("RxAdjustRight",PIn.PDouble(textRight.Text))
				| Prefs.UpdateDouble("RxAdjustDown",PIn.PDouble(textDown.Text)))
			{
				changed=true;
			}
			if(radioGeneric.Checked) {
				if(Prefs.UpdateInt("RxGeneric",0)) {
					changed=true;
				}
			}
			else if(radioNeither.Checked) {
				if(Prefs.UpdateInt("RxGeneric",1)) {
					changed=true;
				}
			}
			else if(radioTwoSig.Checked) {
				if(Prefs.UpdateInt("RxGeneric",2)) {
					changed=true;
				}
			}
			if(changed) {
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















