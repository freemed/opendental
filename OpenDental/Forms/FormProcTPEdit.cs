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
	public class FormProcTPEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ProcTP ProcCur;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textToothNumTP;
		private System.Windows.Forms.ComboBox comboPriority;
		private System.Windows.Forms.TextBox textSurf;
		private System.Windows.Forms.TextBox textCode;
		private System.Windows.Forms.TextBox textDescript;
		private OpenDental.ValidDouble textFeeAmt;
		private OpenDental.ValidDouble textPriInsAmt;
		private OpenDental.ValidDouble textSecInsAmt;
		private ValidDouble textDiscount;
		private Label label2;
		private OpenDental.ValidDouble textPatAmt;

		///<summary></summary>
		public FormProcTPEdit(ProcTP procCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			ProcCur=procCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcTPEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textToothNumTP = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.textSurf = new System.Windows.Forms.TextBox();
			this.textCode = new System.Windows.Forms.TextBox();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.textFeeAmt = new OpenDental.ValidDouble();
			this.textPriInsAmt = new OpenDental.ValidDouble();
			this.textSecInsAmt = new OpenDental.ValidDouble();
			this.textPatAmt = new OpenDental.ValidDouble();
			this.textDiscount = new OpenDental.ValidDouble();
			this.label2 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(480,330);
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
			this.butOK.Location = new System.Drawing.Point(480,292);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textToothNumTP
			// 
			this.textToothNumTP.Location = new System.Drawing.Point(175,63);
			this.textToothNumTP.Name = "textToothNumTP";
			this.textToothNumTP.Size = new System.Drawing.Size(50,20);
			this.textToothNumTP.TabIndex = 4;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(24,330);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86,26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(82,41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89,16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Priority";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(34,65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(137,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Tooth Num";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(82,89);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(89,16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Surf";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(82,113);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89,16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Code";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(82,137);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89,16);
			this.label7.TabIndex = 13;
			this.label7.Text = "Description";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(82,190);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(89,16);
			this.label8.TabIndex = 14;
			this.label8.Text = "Fee";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(82,214);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(89,16);
			this.label9.TabIndex = 15;
			this.label9.Text = "Pri Ins";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(82,238);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(89,16);
			this.label10.TabIndex = 16;
			this.label10.Text = "Sec Ins";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(43,286);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(128,16);
			this.label11.TabIndex = 17;
			this.label11.Text = "Patient Portion";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPriority
			// 
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.Location = new System.Drawing.Point(175,38);
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(94,21);
			this.comboPriority.TabIndex = 59;
			// 
			// textSurf
			// 
			this.textSurf.Location = new System.Drawing.Point(175,87);
			this.textSurf.Name = "textSurf";
			this.textSurf.Size = new System.Drawing.Size(50,20);
			this.textSurf.TabIndex = 60;
			// 
			// textCode
			// 
			this.textCode.Location = new System.Drawing.Point(175,111);
			this.textCode.Name = "textCode";
			this.textCode.Size = new System.Drawing.Size(77,20);
			this.textCode.TabIndex = 61;
			// 
			// textDescript
			// 
			this.textDescript.AcceptsReturn = true;
			this.textDescript.Location = new System.Drawing.Point(175,135);
			this.textDescript.Multiline = true;
			this.textDescript.Name = "textDescript";
			this.textDescript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textDescript.Size = new System.Drawing.Size(377,48);
			this.textDescript.TabIndex = 62;
			// 
			// textFeeAmt
			// 
			this.textFeeAmt.Location = new System.Drawing.Point(175,188);
			this.textFeeAmt.Name = "textFeeAmt";
			this.textFeeAmt.Size = new System.Drawing.Size(81,20);
			this.textFeeAmt.TabIndex = 63;
			// 
			// textPriInsAmt
			// 
			this.textPriInsAmt.Location = new System.Drawing.Point(175,212);
			this.textPriInsAmt.Name = "textPriInsAmt";
			this.textPriInsAmt.Size = new System.Drawing.Size(81,20);
			this.textPriInsAmt.TabIndex = 64;
			// 
			// textSecInsAmt
			// 
			this.textSecInsAmt.Location = new System.Drawing.Point(175,236);
			this.textSecInsAmt.Name = "textSecInsAmt";
			this.textSecInsAmt.Size = new System.Drawing.Size(81,20);
			this.textSecInsAmt.TabIndex = 65;
			// 
			// textPatAmt
			// 
			this.textPatAmt.Location = new System.Drawing.Point(175,284);
			this.textPatAmt.Name = "textPatAmt";
			this.textPatAmt.Size = new System.Drawing.Size(81,20);
			this.textPatAmt.TabIndex = 66;
			// 
			// textDiscount
			// 
			this.textDiscount.Location = new System.Drawing.Point(175,260);
			this.textDiscount.Name = "textDiscount";
			this.textDiscount.Size = new System.Drawing.Size(81,20);
			this.textDiscount.TabIndex = 68;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(43,262);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128,16);
			this.label2.TabIndex = 67;
			this.label2.Text = "Discount";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormProcTPEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(607,384);
			this.Controls.Add(this.textDiscount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textPatAmt);
			this.Controls.Add(this.textSecInsAmt);
			this.Controls.Add(this.textPriInsAmt);
			this.Controls.Add(this.textFeeAmt);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.textSurf);
			this.Controls.Add(this.comboPriority);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textToothNumTP);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcTPEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Treatment Plan Procedure";
			this.Load += new System.EventHandler(this.FormProcTPEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcTPEdit_Load(object sender, System.EventArgs e){
			comboPriority.Items.Add(Lan.g(this,"none"));
			comboPriority.SelectedIndex=0;
			for(int i=0;i<DefB.Short[(int)DefCat.TxPriorities].Length;i++){
				comboPriority.Items.Add(DefB.Short[(int)DefCat.TxPriorities][i].ItemName);
				if(ProcCur.Priority==DefB.Short[(int)DefCat.TxPriorities][i].DefNum){
					comboPriority.SelectedIndex=i+1;
				}
			}
			textToothNumTP.Text=ProcCur.ToothNumTP;
			textSurf.Text=ProcCur.Surf;
			textCode.Text=ProcCur.ProcCode;
			textDescript.Text=ProcCur.Descript;
			textFeeAmt.Text=ProcCur.FeeAmt.ToString("F");
			textPriInsAmt.Text=ProcCur.PriInsAmt.ToString("F");
			textSecInsAmt.Text=ProcCur.SecInsAmt.ToString("F");
			textDiscount.Text=ProcCur.Discount.ToString("F");
			textPatAmt.Text=ProcCur.PatAmt.ToString("F");
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			ProcTPs.Delete(ProcCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textFeeAmt.errorProvider1.GetError(textFeeAmt)!=""
				|| textPriInsAmt.errorProvider1.GetError(textPriInsAmt)!=""
				|| textSecInsAmt.errorProvider1.GetError(textSecInsAmt)!=""
				|| textDiscount.errorProvider1.GetError(textDiscount)!=""
				|| textPatAmt.errorProvider1.GetError(textPatAmt)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(comboPriority.SelectedIndex==0){
				ProcCur.Priority=0;
			}
			else{
				ProcCur.Priority=DefB.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			}
			ProcCur.ToothNumTP=textToothNumTP.Text;
			ProcCur.Surf=textSurf.Text;
			ProcCur.ProcCode=textCode.Text;
			ProcCur.Descript=textDescript.Text;
			ProcCur.FeeAmt=PIn.PDouble(textFeeAmt.Text);
			ProcCur.PriInsAmt=PIn.PDouble(textPriInsAmt.Text);
			ProcCur.SecInsAmt=PIn.PDouble(textSecInsAmt.Text);
			ProcCur.Discount=PIn.PDouble(textDiscount.Text);
			ProcCur.PatAmt=PIn.PDouble(textPatAmt.Text);
			ProcTPs.InsertOrUpdate(ProcCur,false);//IsNew not applicable here
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















