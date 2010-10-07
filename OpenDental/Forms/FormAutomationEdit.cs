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
	public class FormAutomationEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private Label label1;
		private TextBox textDescription;
		private Label label2;
		private TextBox textProcCodes;
		private Label labelProcCodes;
		private Label label4;
		private Label labelSheetDef;
		private Label labelCommType;
		private Label labelMessage;
		private TextBox textMessage;
		private ComboBox comboTrigger;
		private ComboBox comboAction;
		private ComboBox comboCommType;
		private ComboBox comboSheetDef;
		private OpenDental.UI.Button butProcCode;
		private Automation AutoCur;

		///<summary></summary>
		public FormAutomationEdit(Automation autoCur)
		{
			//
			// Required for Windows Form Designer support
			//
			AutoCur=autoCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutomationEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textProcCodes = new System.Windows.Forms.TextBox();
			this.labelProcCodes = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelSheetDef = new System.Windows.Forms.Label();
			this.labelCommType = new System.Windows.Forms.Label();
			this.labelMessage = new System.Windows.Forms.Label();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.comboTrigger = new System.Windows.Forms.ComboBox();
			this.comboAction = new System.Windows.Forms.ComboBox();
			this.comboCommType = new System.Windows.Forms.ComboBox();
			this.comboSheetDef = new System.Windows.Forms.ComboBox();
			this.butProcCode = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butDelete
			// 
			butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			butDelete.Autosize = true;
			butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butDelete.CornerRadius = 4F;
			butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			butDelete.Location = new System.Drawing.Point(48,421);
			butDelete.Name = "butDelete";
			butDelete.Size = new System.Drawing.Size(75,24);
			butDelete.TabIndex = 16;
			butDelete.Text = "&Delete";
			butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48,24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111,20);
			this.label1.TabIndex = 11;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(161,25);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(316,20);
			this.textDescription.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(48,50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111,20);
			this.label2.TabIndex = 18;
			this.label2.Text = "Trigger";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProcCodes
			// 
			this.textProcCodes.Location = new System.Drawing.Point(161,77);
			this.textProcCodes.Name = "textProcCodes";
			this.textProcCodes.Size = new System.Drawing.Size(316,20);
			this.textProcCodes.TabIndex = 2;
			// 
			// labelProcCodes
			// 
			this.labelProcCodes.Location = new System.Drawing.Point(13,76);
			this.labelProcCodes.Name = "labelProcCodes";
			this.labelProcCodes.Size = new System.Drawing.Size(146,29);
			this.labelProcCodes.TabIndex = 20;
			this.labelProcCodes.Text = "Procedure Code(s)\r\n(separated with commas)";
			this.labelProcCodes.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(48,107);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111,20);
			this.label4.TabIndex = 21;
			this.label4.Text = "Action";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelSheetDef
			// 
			this.labelSheetDef.Location = new System.Drawing.Point(48,134);
			this.labelSheetDef.Name = "labelSheetDef";
			this.labelSheetDef.Size = new System.Drawing.Size(111,20);
			this.labelSheetDef.TabIndex = 22;
			this.labelSheetDef.Text = "Sheet Def";
			this.labelSheetDef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCommType
			// 
			this.labelCommType.Location = new System.Drawing.Point(48,159);
			this.labelCommType.Name = "labelCommType";
			this.labelCommType.Size = new System.Drawing.Size(111,20);
			this.labelCommType.TabIndex = 24;
			this.labelCommType.Text = "CommType";
			this.labelCommType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMessage
			// 
			this.labelMessage.Location = new System.Drawing.Point(39,185);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(120,20);
			this.labelMessage.TabIndex = 25;
			this.labelMessage.Text = "Message";
			this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(161,186);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(316,73);
			this.textMessage.TabIndex = 26;
			// 
			// comboTrigger
			// 
			this.comboTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTrigger.FormattingEnabled = true;
			this.comboTrigger.Location = new System.Drawing.Point(161,50);
			this.comboTrigger.Name = "comboTrigger";
			this.comboTrigger.Size = new System.Drawing.Size(183,21);
			this.comboTrigger.TabIndex = 27;
			this.comboTrigger.SelectedIndexChanged += new System.EventHandler(this.comboTrigger_SelectedIndexChanged);
			// 
			// comboAction
			// 
			this.comboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAction.FormattingEnabled = true;
			this.comboAction.Location = new System.Drawing.Point(161,107);
			this.comboAction.Name = "comboAction";
			this.comboAction.Size = new System.Drawing.Size(183,21);
			this.comboAction.TabIndex = 28;
			this.comboAction.SelectedIndexChanged += new System.EventHandler(this.comboAction_SelectedIndexChanged);
			// 
			// comboCommType
			// 
			this.comboCommType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCommType.FormattingEnabled = true;
			this.comboCommType.Location = new System.Drawing.Point(161,159);
			this.comboCommType.Name = "comboCommType";
			this.comboCommType.Size = new System.Drawing.Size(183,21);
			this.comboCommType.TabIndex = 29;
			// 
			// comboSheetDef
			// 
			this.comboSheetDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSheetDef.FormattingEnabled = true;
			this.comboSheetDef.Location = new System.Drawing.Point(161,133);
			this.comboSheetDef.Name = "comboSheetDef";
			this.comboSheetDef.Size = new System.Drawing.Size(183,21);
			this.comboSheetDef.TabIndex = 31;
			// 
			// butProcCode
			// 
			this.butProcCode.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProcCode.Autosize = true;
			this.butProcCode.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProcCode.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProcCode.CornerRadius = 4F;
			this.butProcCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butProcCode.Location = new System.Drawing.Point(479,75);
			this.butProcCode.Name = "butProcCode";
			this.butProcCode.Size = new System.Drawing.Size(23,24);
			this.butProcCode.TabIndex = 32;
			this.butProcCode.Text = "...";
			this.butProcCode.Click += new System.EventHandler(this.butProcCode_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(536,389);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
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
			this.butCancel.Location = new System.Drawing.Point(536,421);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormAutomationEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(637,465);
			this.Controls.Add(this.butProcCode);
			this.Controls.Add(this.comboSheetDef);
			this.Controls.Add(this.comboCommType);
			this.Controls.Add(this.comboAction);
			this.Controls.Add(this.comboTrigger);
			this.Controls.Add(this.textMessage);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.labelCommType);
			this.Controls.Add(this.labelSheetDef);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textProcCodes);
			this.Controls.Add(this.labelProcCodes);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(butDelete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutomationEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Automation";
			this.Load += new System.EventHandler(this.FormAutomationEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAutomationEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=AutoCur.Description;
			for(int i=0;i<Enum.GetNames(typeof(AutomationTrigger)).Length;i++){
				comboTrigger.Items.Add(Enum.GetNames(typeof(AutomationTrigger))[i]);
			}
			comboTrigger.SelectedIndex=(int)AutoCur.Autotrigger;
			textProcCodes.Text=AutoCur.ProcCodes;//although might not be visible.
			for(int i=0;i<Enum.GetNames(typeof(AutomationAction)).Length;i++) {
				comboAction.Items.Add(Enum.GetNames(typeof(AutomationAction))[i]);
			}
			comboAction.SelectedIndex=(int)AutoCur.AutoAction;
			for(int i=0;i<SheetDefC.Listt.Count;i++) {
				comboSheetDef.Items.Add(SheetDefC.Listt[i].Description);
				if(AutoCur.SheetDefNum==SheetDefC.Listt[i].SheetDefNum) {
					comboSheetDef.SelectedIndex=i;
				}
			}
			for(int i=0;i<DefC.Short[(int)DefCat.CommLogTypes].Length;i++) {
				comboCommType.Items.Add(DefC.Short[(int)DefCat.CommLogTypes][i].ItemName);
				if(DefC.Short[(int)DefCat.CommLogTypes][i].DefNum==AutoCur.CommType) {
					comboCommType.SelectedIndex=i;
				}
			}
			textMessage.Text=AutoCur.MessageContent;
		}

		private void comboTrigger_SelectedIndexChanged(object sender,EventArgs e) {
			if(comboTrigger.SelectedIndex==(int)AutomationTrigger.CompleteProcedure) {
				labelProcCodes.Visible=true;
				textProcCodes.Visible=true;
				butProcCode.Visible=true;
			}
			else{
				labelProcCodes.Visible=false;
				textProcCodes.Visible=false;
				butProcCode.Visible=false;
			}
		}

		private void comboAction_SelectedIndexChanged(object sender,EventArgs e) {
			if(comboAction.SelectedIndex==(int)AutomationAction.CreateCommlog) {
				labelSheetDef.Visible=false;
				comboSheetDef.Visible=false;
				labelCommType.Visible=true;
				comboCommType.Visible=true;
				labelMessage.Visible=true;
				textMessage.Visible=true;
			}
			else {
				labelSheetDef.Visible=true;
				comboSheetDef.Visible=true;
				labelCommType.Visible=false;
				comboCommType.Visible=false;
				labelMessage.Visible=false;
				textMessage.Visible=false;
			}
		}

		private void butProcCode_Click(object sender,EventArgs e) {
			FormProcCodes formp=new FormProcCodes();
			formp.IsSelectionMode=true;
			formp.ShowDialog();
			if(formp.DialogResult!=DialogResult.OK) {
				return;
			}
			if(textProcCodes.Text!="") {
				textProcCodes.Text+=",";
			}
			textProcCodes.Text+=ProcedureCodes.GetStringProcCode(formp.SelectedCodeNum);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			Automations.Delete(AutoCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MsgBox.Show(this,"Description not allowed to be blank.");
				return;
			}
			if(comboTrigger.SelectedIndex==(int)AutomationTrigger.CompleteProcedure){
				if(textProcCodes.Text.Contains(" ")){
					MsgBox.Show(this,"Procedure codes cannot contain any spaces.");
					return;
				}
				if(textProcCodes.Text=="") {
					MsgBox.Show(this,"Please enter valid procedure code(s) first.");
					return;
				}
				string[] arrayCodes=textProcCodes.Text.Split(',');
				for(int i=0;i<arrayCodes.Length;i++){
					if(!ProcedureCodes.IsValidCode(arrayCodes[i])){
						MessageBox.Show(arrayCodes[i]+Lan.g(this," is not a valid procedure code."));
						return;
					}
				}
			}
			if(comboAction.SelectedIndex==(int)AutomationAction.CreateCommlog){
				if(comboCommType.SelectedIndex==-1){
					MsgBox.Show(this,"A CommType must be selected.");
					return;
				}
			}
			if(comboAction.SelectedIndex==(int)AutomationAction.PrintPatientLetter){
				if(comboSheetDef.SelectedIndex==-1){
					MsgBox.Show(this,"A SheetDef must be selected.");
					return;
				}
				if(SheetDefC.Listt[comboSheetDef.SelectedIndex].SheetType!=SheetTypeEnum.PatientLetter) {
					MsgBox.Show(this,"The selected sheet type must be a patient letter.");
					return;
				}
			}
			if(comboAction.SelectedIndex==(int)AutomationAction.PrintReferralLetter) {
				if(comboSheetDef.SelectedIndex==-1) {
					MsgBox.Show(this,"A SheetDef must be selected.");
					return;
				}
				if(SheetDefC.Listt[comboSheetDef.SelectedIndex].SheetType!=SheetTypeEnum.ReferralLetter) {
					MsgBox.Show(this,"The selected sheet type must be a referral letter.");
					return;
				}
			}
			if(comboAction.SelectedIndex==(int)AutomationAction.ShowExamSheet) {
				if(comboSheetDef.SelectedIndex==-1) {
					MsgBox.Show(this,"A SheetDef must be selected.");
					return;
				}
				if(SheetDefC.Listt[comboSheetDef.SelectedIndex].SheetType!=SheetTypeEnum.ExamSheet) {
					MsgBox.Show(this,"The selected sheet type must be an exam sheet.");
					return;
				}
			}
			AutoCur.Description=textDescription.Text;
			AutoCur.Autotrigger=(AutomationTrigger)comboTrigger.SelectedIndex;
			if(comboTrigger.SelectedIndex==(int)AutomationTrigger.CompleteProcedure) {
				AutoCur.ProcCodes=textProcCodes.Text;
			}
			else {
				AutoCur.ProcCodes="";
			}
			AutoCur.AutoAction=(AutomationAction)comboAction.SelectedIndex;
			if(comboAction.SelectedIndex==(int)AutomationAction.PrintPatientLetter
				|| comboAction.SelectedIndex==(int)AutomationAction.PrintReferralLetter
				|| comboAction.SelectedIndex==(int)AutomationAction.ShowExamSheet) 
			{
				AutoCur.SheetDefNum=SheetDefC.Listt[comboSheetDef.SelectedIndex].SheetDefNum;
			}
			else {
				AutoCur.SheetDefNum=0;
			}
			if(comboAction.SelectedIndex==(int)AutomationAction.CreateCommlog) {
				AutoCur.CommType=DefC.Short[(int)DefCat.CommLogTypes][comboCommType.SelectedIndex].DefNum;
				AutoCur.MessageContent=textMessage.Text;
			}
			else {
				AutoCur.CommType=0;
				AutoCur.MessageContent="";
			}
			if(IsNew){
				Automations.Insert(AutoCur);
			}
			else{
				Automations.Update(AutoCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	

		

		

		


	}
}





















