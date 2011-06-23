using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRxEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textSig;
		private System.Windows.Forms.TextBox textDisp;
		private System.Windows.Forms.TextBox textRefills;
		private System.Windows.Forms.TextBox textDrug;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDate;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label7;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ODtextBox textNotes;
		///<summary></summary>
    public FormRpPrintPreview pView = new FormRpPrintPreview();
		private Patient PatCur;
		private Label label8;
		private OpenDental.UI.Button butPick;
		private TextBox textPharmacy;
		private CheckBox checkControlled;
		private OpenDental.UI.Button butView;
		private Label labelView;
		//private User user;
		private RxPat RxPatCur;
		private UI.Button butSend;
		private Label label9;
		private ComboBox comboSendStatus;
		private Label labelRxNorm;
		private TextBox textDosageCode;
		private Label labelDosageCode;
		private UI.Button butRxNormSelect;
		private TextBox textRxNorm;
		///<summary>If the Rx has already been printed, this will contain the archived sheet. The print button will be not visible, and the view button will be visible.</summary>
		private Sheet sheet;
		///<summary>Used to prevent infinite loop of windows when called from FormRxSend.</summary>
		public bool HideSendBut;

		///<summary></summary>
		public FormRxEdit(Patient patCur,RxPat rxPatCur){
			//){//
			InitializeComponent();
			RxPatCur=rxPatCur;
			PatCur=patCur;
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRxEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textSig = new System.Windows.Forms.TextBox();
			this.textDisp = new System.Windows.Forms.TextBox();
			this.textRefills = new System.Windows.Forms.TextBox();
			this.textDrug = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butPrint = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textNotes = new OpenDental.ODtextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.butPick = new OpenDental.UI.Button();
			this.textPharmacy = new System.Windows.Forms.TextBox();
			this.checkControlled = new System.Windows.Forms.CheckBox();
			this.butView = new OpenDental.UI.Button();
			this.labelView = new System.Windows.Forms.Label();
			this.butSend = new OpenDental.UI.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.comboSendStatus = new System.Windows.Forms.ComboBox();
			this.labelRxNorm = new System.Windows.Forms.Label();
			this.textDosageCode = new System.Windows.Forms.TextBox();
			this.labelDosageCode = new System.Windows.Forms.Label();
			this.butRxNormSelect = new OpenDental.UI.Button();
			this.textRxNorm = new System.Windows.Forms.TextBox();
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(632,412);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(632,372);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textSig
			// 
			this.textSig.AcceptsReturn = true;
			this.textSig.Location = new System.Drawing.Point(138,83);
			this.textSig.Multiline = true;
			this.textSig.Name = "textSig";
			this.textSig.Size = new System.Drawing.Size(254,44);
			this.textSig.TabIndex = 2;
			// 
			// textDisp
			// 
			this.textDisp.Location = new System.Drawing.Point(138,132);
			this.textDisp.Name = "textDisp";
			this.textDisp.Size = new System.Drawing.Size(114,20);
			this.textDisp.TabIndex = 3;
			// 
			// textRefills
			// 
			this.textRefills.Location = new System.Drawing.Point(138,156);
			this.textRefills.Name = "textRefills";
			this.textRefills.Size = new System.Drawing.Size(114,20);
			this.textRefills.TabIndex = 4;
			// 
			// textDrug
			// 
			this.textDrug.Location = new System.Drawing.Point(138,58);
			this.textDrug.Name = "textDrug";
			this.textDrug.Size = new System.Drawing.Size(254,20);
			this.textDrug.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(49,87);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89,14);
			this.label6.TabIndex = 17;
			this.label6.Text = "Sig";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(39,136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99,14);
			this.label5.TabIndex = 16;
			this.label5.Text = "Disp";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(39,160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99,14);
			this.label4.TabIndex = 15;
			this.label4.Text = "Refills";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(31,205);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107,36);
			this.label3.TabIndex = 14;
			this.label3.Text = "Notes (will not show on printout)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(45,60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93,14);
			this.label1.TabIndex = 13;
			this.label1.Text = "Drug";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(34,15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105,14);
			this.label2.TabIndex = 25;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(138,11);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 0;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(532,28);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(169,212);
			this.listProv.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(530,10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(103,14);
			this.label7.TabIndex = 28;
			this.label7.Text = "Provider";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(378,412);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(81,24);
			this.butPrint.TabIndex = 29;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(20,412);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(88,24);
			this.butDelete.TabIndex = 30;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(138,204);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Rx;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(373,111);
			this.textNotes.TabIndex = 31;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(39,322);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(99,14);
			this.label8.TabIndex = 32;
			this.label8.Text = "Pharmacy";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butPick
			// 
			this.butPick.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPick.Autosize = true;
			this.butPick.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPick.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPick.CornerRadius = 4F;
			this.butPick.Location = new System.Drawing.Point(338,317);
			this.butPick.Name = "butPick";
			this.butPick.Size = new System.Drawing.Size(58,23);
			this.butPick.TabIndex = 65;
			this.butPick.TabStop = false;
			this.butPick.Text = "Pick";
			this.butPick.Click += new System.EventHandler(this.butPick_Click);
			// 
			// textPharmacy
			// 
			this.textPharmacy.AcceptsReturn = true;
			this.textPharmacy.Location = new System.Drawing.Point(138,319);
			this.textPharmacy.Name = "textPharmacy";
			this.textPharmacy.ReadOnly = true;
			this.textPharmacy.Size = new System.Drawing.Size(198,20);
			this.textPharmacy.TabIndex = 64;
			// 
			// checkControlled
			// 
			this.checkControlled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkControlled.Location = new System.Drawing.Point(7,35);
			this.checkControlled.Name = "checkControlled";
			this.checkControlled.Size = new System.Drawing.Size(145,20);
			this.checkControlled.TabIndex = 66;
			this.checkControlled.Text = "Controlled Substance";
			this.checkControlled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkControlled.UseVisualStyleBackColor = true;
			// 
			// butView
			// 
			this.butView.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butView.Autosize = true;
			this.butView.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butView.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butView.CornerRadius = 4F;
			this.butView.Image = global::OpenDental.Properties.Resources.printPreview20;
			this.butView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butView.Location = new System.Drawing.Point(465,412);
			this.butView.Name = "butView";
			this.butView.Size = new System.Drawing.Size(81,24);
			this.butView.TabIndex = 244;
			this.butView.Text = "View";
			this.butView.Click += new System.EventHandler(this.butView_Click);
			// 
			// labelView
			// 
			this.labelView.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.labelView.Location = new System.Drawing.Point(462,439);
			this.labelView.Name = "labelView";
			this.labelView.Size = new System.Drawing.Size(199,14);
			this.labelView.TabIndex = 245;
			this.labelView.Text = "This Rx has already been printed.";
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSend.Location = new System.Drawing.Point(199,412);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(78,24);
			this.butSend.TabIndex = 247;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(39,345);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(99,14);
			this.label9.TabIndex = 250;
			this.label9.Text = "Send Status";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboSendStatus
			// 
			this.comboSendStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSendStatus.FormattingEnabled = true;
			this.comboSendStatus.Location = new System.Drawing.Point(138,342);
			this.comboSendStatus.Name = "comboSendStatus";
			this.comboSendStatus.Size = new System.Drawing.Size(198,21);
			this.comboSendStatus.TabIndex = 251;
			// 
			// labelRxNorm
			// 
			this.labelRxNorm.Location = new System.Drawing.Point(12,184);
			this.labelRxNorm.Name = "labelRxNorm";
			this.labelRxNorm.Size = new System.Drawing.Size(126,14);
			this.labelRxNorm.TabIndex = 253;
			this.labelRxNorm.Text = "RxNorm";
			this.labelRxNorm.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDosageCode
			// 
			this.textDosageCode.Location = new System.Drawing.Point(377,180);
			this.textDosageCode.Name = "textDosageCode";
			this.textDosageCode.Size = new System.Drawing.Size(114,20);
			this.textDosageCode.TabIndex = 256;
			this.textDosageCode.Visible = false;
			// 
			// labelDosageCode
			// 
			this.labelDosageCode.Location = new System.Drawing.Point(283,184);
			this.labelDosageCode.Name = "labelDosageCode";
			this.labelDosageCode.Size = new System.Drawing.Size(94,14);
			this.labelDosageCode.TabIndex = 257;
			this.labelDosageCode.Text = "Dosage Code";
			this.labelDosageCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelDosageCode.Visible = false;
			// 
			// butRxNormSelect
			// 
			this.butRxNormSelect.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRxNormSelect.Autosize = true;
			this.butRxNormSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRxNormSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRxNormSelect.CornerRadius = 4F;
			this.butRxNormSelect.Location = new System.Drawing.Point(255,179);
			this.butRxNormSelect.Name = "butRxNormSelect";
			this.butRxNormSelect.Size = new System.Drawing.Size(22,22);
			this.butRxNormSelect.TabIndex = 259;
			this.butRxNormSelect.Text = "...";
			this.butRxNormSelect.Click += new System.EventHandler(this.butRxNormSelect_Click);
			// 
			// textRxNorm
			// 
			this.textRxNorm.Location = new System.Drawing.Point(138,180);
			this.textRxNorm.Name = "textRxNorm";
			this.textRxNorm.ReadOnly = true;
			this.textRxNorm.Size = new System.Drawing.Size(113,20);
			this.textRxNorm.TabIndex = 260;
			// 
			// FormRxEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(724,460);
			this.Controls.Add(this.textRxNorm);
			this.Controls.Add(this.butRxNormSelect);
			this.Controls.Add(this.textDosageCode);
			this.Controls.Add(this.labelDosageCode);
			this.Controls.Add(this.labelRxNorm);
			this.Controls.Add(this.comboSendStatus);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.labelView);
			this.Controls.Add(this.butView);
			this.Controls.Add(this.checkControlled);
			this.Controls.Add(this.butPick);
			this.Controls.Add(this.textPharmacy);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textSig);
			this.Controls.Add(this.textDisp);
			this.Controls.Add(this.textRefills);
			this.Controls.Add(this.textDrug);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Rx";
			this.Load += new System.EventHandler(this.FormRxEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRxEdit_Load(object sender, System.EventArgs e) {
			if(HideSendBut) {
				butSend.Visible=false;
			}
			if(IsNew){
				butView.Visible=false;
				labelView.Visible=false;
				sheet=null;
			}
			else{
				sheet=Sheets.GetRx(RxPatCur.PatNum,RxPatCur.RxNum);
				if(sheet==null){
					butView.Visible=false;
					labelView.Visible=false;
				}
				else{
					butPrint.Visible=false;
				}
			}
			//security is handled on the Rx button click in the Chart module
			for(int i=0;i<ProviderC.List.Length;i++){
				this.listProv.Items.Add(ProviderC.List[i].GetLongDesc());
				if(ProviderC.List[i].ProvNum==RxPatCur.ProvNum){
					listProv.SelectedIndex=i;
				}
			}
			if(listProv.SelectedIndex==-1){
				listProv.SelectedIndex=0;
			}
			textDate.Text=RxPatCur.RxDate.ToString("d");
			checkControlled.Checked=RxPatCur.IsControlled;
			for(int i=0;i<Enum.GetNames(typeof(RxSendStatus)).Length;i++) {
				comboSendStatus.Items.Add(Enum.GetNames(typeof(RxSendStatus))[i]);
			}
			comboSendStatus.SelectedIndex=(int)RxPatCur.SendStatus;
			textDrug.Text=RxPatCur.Drug;
			textSig.Text=RxPatCur.Sig;
			textDisp.Text=RxPatCur.Disp;
			textRefills.Text=RxPatCur.Refills;
			textRxNorm.Text=RxPatCur.RxCui.ToString();
			if(FormOpenDental.FormEHR!=null) {
				labelDosageCode.Visible=true;
				textDosageCode.Visible=true;
				textDosageCode.Text=RxPatCur.DosageCode;
			}
			textNotes.Text=RxPatCur.Notes;
			textPharmacy.Text=Pharmacies.GetDescription(RxPatCur.PharmacyNum);
		}

		private void butPick_Click(object sender,EventArgs e) {
			FormPharmacies FormP=new FormPharmacies();
			FormP.IsSelectionMode=true;
			FormP.SelectedPharmacyNum=RxPatCur.PharmacyNum;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			RxPatCur.PharmacyNum=FormP.SelectedPharmacyNum;
			textPharmacy.Text=Pharmacies.GetDescription(RxPatCur.PharmacyNum);
		}

		private bool SaveRx(){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				//|| textRxNorm.errorProvider1.GetError(textRxNorm)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(listProv.SelectedIndex!=-1){
				RxPatCur.ProvNum=ProviderC.List[listProv.SelectedIndex].ProvNum;
			}
			//RxCui was set when butRxNormSelect was clicked.
			RxPatCur.RxDate=PIn.Date(textDate.Text);
			RxPatCur.Drug=textDrug.Text;
			RxPatCur.IsControlled=checkControlled.Checked;
			RxPatCur.Sig=textSig.Text;
			RxPatCur.Disp=textDisp.Text;
			RxPatCur.Refills=textRefills.Text;
			RxPatCur.DosageCode=textDosageCode.Text;
			RxPatCur.Notes=textNotes.Text;
			RxPatCur.SendStatus=(RxSendStatus)comboSendStatus.SelectedIndex;
			//pharmacy is set when using pick button.
			if(IsNew){
				RxPatCur.RxNum=RxPats.Insert(RxPatCur);
				//SecurityLogs.MakeLogEntry("Prescription Create",RxPats.cmd.CommandText,user);
				if(FormProcGroup.IsOpen){
					FormProcGroup.RxNum=RxPatCur.RxNum;
				}
			}
			else{
				RxPats.Update(RxPatCur);
				//SecurityLogs.MakeLogEntry("Prescription Edit",RxPats.cmd.CommandText,user);
			}
			IsNew=false;//so that we can save it again after printing if needed.
			return true;
		}

		private void butRxNormSelect_Click(object sender,EventArgs e) {
			FormRxNorms FormRN=new FormRxNorms();
			FormRN.ShowDialog();
			if(FormRN.DialogResult!=DialogResult.OK) {
				return;
			}
			RxPatCur.RxCui=PIn.Long(FormRN.SelectedRxNorm.RxCui);
			textRxNorm.Text=RxPatCur.RxCui.ToString();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Prescription?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			RxPats.Delete(RxPatCur.RxNum);
			DialogResult=DialogResult.OK;	
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			//only visible if sheet==null.
			if(comboSendStatus.SelectedIndex==(int)RxSendStatus.InElectQueue
				|| comboSendStatus.SelectedIndex==(int)RxSendStatus.SentElect) 
			{
				//do not change status
			}
			else {
				comboSendStatus.SelectedIndex=(int)RxSendStatus.Printed;
			}
			if(!SaveRx()){
				return;
			}
			SheetDef sheetDef;
			List<SheetDef> customSheetDefs=SheetDefs.GetCustomForType(SheetTypeEnum.Rx);
			if(customSheetDefs.Count==0){
				sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.Rx);
			}
			else{
				sheetDef=customSheetDefs[0];
				SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			sheet=SheetUtil.CreateSheet(sheetDef,PatCur.PatNum);
			SheetParameter.SetParameter(sheet,"RxNum",RxPatCur.RxNum);
			SheetFiller.FillFields(sheet);
			SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
			SheetPrinting.PrintRx(sheet,RxPatCur.IsControlled);
			DialogResult=DialogResult.OK;
		}

		private void butView_Click(object sender,EventArgs e) {
			//only visible if there is already a sheet.
			if(!SaveRx()){
				return;
			}
			SheetFields.GetFieldsAndParameters(sheet);
			FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
			FormSF.ShowDialog();
			if(FormSF.DialogResult==DialogResult.OK){
				DialogResult=DialogResult.OK;
			}
			//if user clicked cancel, then we can just stay in this form.
		}

		private void butSend_Click(object sender,EventArgs e) {
			comboSendStatus.SelectedIndex=(int)RxSendStatus.InElectQueue;
			if(!SaveRx()){
				return;
			}
			FormRxSend FormRS=new FormRxSend();
			FormRS.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveRx()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

	}
}
