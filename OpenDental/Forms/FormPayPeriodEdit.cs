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
	public class FormPayPeriodEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private ValidDate textDateStart;
		private Label label1;
		private ValidDate textDateStop;
		private Label label2;
		private ValidDate textDatePaycheck;
		private Label label3;
		private PayPeriod PayPeriodCur;

		///<summary></summary>
		public FormPayPeriodEdit(PayPeriod payPeriodCur)
		{
			//
			// Required for Windows Form Designer support
			//
			PayPeriodCur=payPeriodCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayPeriodEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textDateStart = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.textDateStop = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.textDatePaycheck = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
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
			butDelete.Location = new System.Drawing.Point(15,137);
			butDelete.Name = "butDelete";
			butDelete.Size = new System.Drawing.Size(75,26);
			butDelete.TabIndex = 16;
			butDelete.Text = "&Delete";
			butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(314,137);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
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
			this.butOK.Location = new System.Drawing.Point(314,105);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(111,24);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(100,20);
			this.textDateStart.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,20);
			this.label1.TabIndex = 11;
			this.label1.Text = "Start Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(111,50);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(100,20);
			this.textDateStop.TabIndex = 12;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,20);
			this.label2.TabIndex = 13;
			this.label2.Text = "End Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDatePaycheck
			// 
			this.textDatePaycheck.Location = new System.Drawing.Point(111,76);
			this.textDatePaycheck.Name = "textDatePaycheck";
			this.textDatePaycheck.Size = new System.Drawing.Size(100,20);
			this.textDatePaycheck.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,20);
			this.label3.TabIndex = 15;
			this.label3.Text = "Paycheck Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormPayPeriodEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(415,181);
			this.Controls.Add(butDelete);
			this.Controls.Add(this.textDatePaycheck);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateStop);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayPeriodEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Pay Period";
			this.Load += new System.EventHandler(this.FormPayPeriodEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPayPeriodEdit_Load(object sender, System.EventArgs e) {
			if(PayPeriodCur.DateStart.Year>1880){
				textDateStart.Text=PayPeriodCur.DateStart.ToShortDateString();
			}
			if(PayPeriodCur.DateStop.Year>1880){
				textDateStop.Text=PayPeriodCur.DateStop.ToShortDateString();
			}
			if(PayPeriodCur.DatePaycheck.Year>1880){
				textDatePaycheck.Text=PayPeriodCur.DatePaycheck.ToShortDateString();
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			PayPeriods.Delete(PayPeriodCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!=""
				|| textDatePaycheck.errorProvider1.GetError(textDatePaycheck)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDateStart.Text=="" || textDateStop.Text==""){
				MsgBox.Show(this,"Start and end dates are required.");
				return;
			}
			PayPeriodCur.DateStart=PIn.PDate(textDateStart.Text);
			PayPeriodCur.DateStop=PIn.PDate(textDateStop.Text);
			PayPeriodCur.DatePaycheck=PIn.PDate(textDatePaycheck.Text);
			if(IsNew){
				PayPeriods.Insert(PayPeriodCur);
			}
			else{
				PayPeriods.Update(PayPeriodCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		

		


	}
}





















