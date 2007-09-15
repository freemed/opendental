/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormPayPlanChargeEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ODtextBox textNote;
		private PayPlanCharge PayPlanChargeCur;
		private OpenDental.ValidDouble textPrincipal;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDouble textInterest;
		private ComboBox comboProvNum;
		private Label label9;
		private OpenDental.ValidDate textDate;

		///<summary></summary>
		public FormPayPlanChargeEdit(PayPlanCharge payPlanCharge){
			InitializeComponent();
			PayPlanChargeCur=payPlanCharge.Copy();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayPlanChargeEdit));
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textPrincipal = new OpenDental.ValidDouble();
			this.textNote = new OpenDental.ODtextBox();
			this.textInterest = new OpenDental.ValidDouble();
			this.label1 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.comboProvNum = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Principal";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(437,150);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 6;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(437,188);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
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
			this.butDelete.Location = new System.Drawing.Point(24,186);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(78,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textPrincipal
			// 
			this.textPrincipal.Location = new System.Drawing.Point(108,53);
			this.textPrincipal.Name = "textPrincipal";
			this.textPrincipal.Size = new System.Drawing.Size(100,20);
			this.textPrincipal.TabIndex = 1;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(108,101);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Adjustment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(245,55);
			this.textNote.TabIndex = 0;
			// 
			// textInterest
			// 
			this.textInterest.Location = new System.Drawing.Point(108,77);
			this.textInterest.Name = "textInterest";
			this.textInterest.Size = new System.Drawing.Size(100,20);
			this.textInterest.TabIndex = 20;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6,79);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,16);
			this.label1.TabIndex = 21;
			this.label1.Text = "Interest";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(108,4);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 22;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,16);
			this.label2.TabIndex = 23;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboProvNum
			// 
			this.comboProvNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvNum.Location = new System.Drawing.Point(108,28);
			this.comboProvNum.MaxDropDownItems = 30;
			this.comboProvNum.Name = "comboProvNum";
			this.comboProvNum.Size = new System.Drawing.Size(136,21);
			this.comboProvNum.TabIndex = 102;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(7,32);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100,14);
			this.label9.TabIndex = 101;
			this.label9.Text = "Provider";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormPayPlanChargeEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(541,236);
			this.Controls.Add(this.comboProvNum);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textInterest);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textPrincipal);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayPlanChargeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Payment Plan Charge";
			this.Load += new System.EventHandler(this.FormPayPlanCharge_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPayPlanCharge_Load(object sender, System.EventArgs e) {
			textDate.Text=PayPlanChargeCur.ChargeDate.ToShortDateString();
			comboProvNum.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++) {
				comboProvNum.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==PayPlanChargeCur.ProvNum)
					comboProvNum.SelectedIndex=i;
			}
			textPrincipal.Text=PayPlanChargeCur.Principal.ToString("n");
			textInterest.Text=PayPlanChargeCur.Interest.ToString("n");
			textNote.Text=PayPlanChargeCur.Note;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textDate.errorProvider1.GetError(textDate)!=""
				|| textPrincipal.errorProvider1.GetError(textPrincipal)!=""
				|| textInterest.errorProvider1.GetError(textInterest)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(comboProvNum.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			if(textPrincipal.Text==""){
				textPrincipal.Text="0";
			}
			if(textInterest.Text==""){
				textInterest.Text="0";
			}
			//todo: test dates?  The day of the month should be the same as all others
			PayPlanChargeCur.ChargeDate=PIn.PDate(textDate.Text);
			PayPlanChargeCur.ProvNum=Providers.List[comboProvNum.SelectedIndex].ProvNum;
			PayPlanChargeCur.Principal=PIn.PDouble(textPrincipal.Text);
			PayPlanChargeCur.Interest=PIn.PDouble(textInterest.Text);
			PayPlanChargeCur.Note=textNote.Text;
			try{
				PayPlanCharges.InsertOrUpdate(PayPlanChargeCur,IsNew);
			}
			catch(ApplicationException ex){//even though it doesn't currently throw any exceptions
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				PayPlanCharges.Delete(PayPlanChargeCur);
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}

	
}
