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
	public class FormRepeatChargeEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textCode;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDouble textChargeAmt;
		private OpenDental.ValidDate textDateStart;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidDate textDateStop;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textDesc;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label7;
		private RepeatCharge RepeatCur;

		///<summary></summary>
		public FormRepeatChargeEdit(RepeatCharge repeatCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			RepeatCur=repeatCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepeatChargeEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textChargeAmt = new OpenDental.ValidDouble();
			this.textDateStart = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textDateStop = new OpenDental.ValidDate();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textDesc = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.label7 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(512,312);
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
			this.butOK.Location = new System.Drawing.Point(512,271);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4,19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(156,16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCode
			// 
			this.textCode.Location = new System.Drawing.Point(162,17);
			this.textCode.MaxLength = 15;
			this.textCode.Name = "textCode";
			this.textCode.ReadOnly = true;
			this.textCode.Size = new System.Drawing.Size(100,20);
			this.textCode.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(156,16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Charge Amount";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textChargeAmt
			// 
			this.textChargeAmt.Location = new System.Drawing.Point(162,46);
			this.textChargeAmt.Name = "textChargeAmt";
			this.textChargeAmt.Size = new System.Drawing.Size(100,20);
			this.textChargeAmt.TabIndex = 5;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(162,75);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(100,20);
			this.textDateStart.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4,78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(156,16);
			this.label3.TabIndex = 7;
			this.label3.Text = "Date Start";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4,106);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(156,16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Date Stop";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(162,104);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(100,20);
			this.textDateStop.TabIndex = 8;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(162,133);
			this.textNote.MaxLength = 10000;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(424,114);
			this.textNote.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(156,16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Note";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDesc
			// 
			this.textDesc.BackColor = System.Drawing.SystemColors.Control;
			this.textDesc.Location = new System.Drawing.Point(267,17);
			this.textDesc.Name = "textDesc";
			this.textDesc.Size = new System.Drawing.Size(241,20);
			this.textDesc.TabIndex = 40;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(265,1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(224,16);
			this.label6.TabIndex = 39;
			this.label6.Text = "Procedure Description:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
			this.butDelete.Location = new System.Drawing.Point(35,314);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,26);
			this.butDelete.TabIndex = 41;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(128,290);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(264,51);
			this.label7.TabIndex = 42;
			this.label7.Text = "It\'s OK to delete an obsolete repeating charge.   It does not affect any charges " +
    "already billed";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRepeatChargeEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(622,366);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDesc);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textDateStop);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.textChargeAmt);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRepeatChargeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Repeat Charge";
			this.Load += new System.EventHandler(this.FormRepeatChargeEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRepeatChargeEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				FormProcCodes FormP=new FormProcCodes();
				FormP.IsSelectionMode=true;
				FormP.ShowDialog();
				if(FormP.DialogResult!=DialogResult.OK){
					DialogResult=DialogResult.Cancel;
					return;
				}
				ProcedureCode procCode=ProcedureCodes.GetProcCode(FormP.SelectedCodeNum);
				if(procCode.TreatArea!=TreatmentArea.Mouth){
					MsgBox.Show(this,"Procedure codes that require tooth numbers are not allowed.");
					DialogResult=DialogResult.Cancel;
					return;
				}
				RepeatCur.ProcCode=ProcedureCodes.GetStringProcCode(FormP.SelectedCodeNum);
			}
			textCode.Text=RepeatCur.ProcCode;
			textDesc.Text=ProcedureCodes.GetProcCode(RepeatCur.ProcCode).Descript;
			textChargeAmt.Text=RepeatCur.ChargeAmt.ToString("F");
			if(RepeatCur.DateStart.Year>1880){
				textDateStart.Text=RepeatCur.DateStart.ToShortDateString();
			}
			if(RepeatCur.DateStop.Year>1880){
				textDateStop.Text=RepeatCur.DateStop.ToShortDateString();
			}
			textNote.Text=RepeatCur.Note;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			RepeatCharges.Delete(RepeatCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if( textChargeAmt.errorProvider1.GetError(textChargeAmt)!=""
				|| textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDateStart.Text==""){
				MsgBox.Show(this,"Start date cannot be left blank.");
				return;
			}
			if(PIn.PDate(textDateStart.Text)<DateTime.Today.AddMonths(-1)){
				MsgBox.Show(this,"Start date cannot be more than a month in the past.  But you can still enter previous charges manually in the account.");
				return;
			}
			RepeatCur.ProcCode=textCode.Text;
			RepeatCur.ChargeAmt=PIn.PDouble(textChargeAmt.Text);
			RepeatCur.DateStart=PIn.PDate(textDateStart.Text);
			RepeatCur.DateStop=PIn.PDate(textDateStop.Text);
			RepeatCur.Note=textNote.Text;
			if(IsNew){
				RepeatCharges.Insert(RepeatCur);
			}
			else{
				RepeatCharges.Update(RepeatCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















