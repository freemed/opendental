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
	public class FormScreenEdit : System.Windows.Forms.Form{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.ComboBox comboGradeLevel;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textComments;
		private System.Windows.Forms.ListBox listUrgency;
		private System.Windows.Forms.CheckBox checkNeedsSealants;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.RadioButton radioM;
		private System.Windows.Forms.RadioButton radioF;
		private System.Windows.Forms.RadioButton radioUnknown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkHasCaries;
		private System.Windows.Forms.CheckBox checkExistingSealants;
		private System.Windows.Forms.CheckBox checkCariesExperience;
		private System.Windows.Forms.CheckBox checkEarlyChildCaries;
		private System.Windows.Forms.CheckBox checkMissingAllTeeth;
		private System.Windows.Forms.TextBox textBirthdate;
		private System.Windows.Forms.TextBox textAge;
		private System.Windows.Forms.DomainUpDown updownAgeArrows;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textScreenGroupOrder;
		private System.Windows.Forms.ListBox listRace;
		public OpenDentBusiness.Screen ScreenCur;
		private UI.Button butCancel;
		private UI.Button butDelete;
		private Label label2;
		public ScreenGroup ScreenGroupCur;

		///<summary></summary>
		public FormScreenEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScreenEdit));
			this.comboGradeLevel = new System.Windows.Forms.ComboBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textComments = new System.Windows.Forms.TextBox();
			this.listUrgency = new System.Windows.Forms.ListBox();
			this.checkHasCaries = new System.Windows.Forms.CheckBox();
			this.checkNeedsSealants = new System.Windows.Forms.CheckBox();
			this.updownAgeArrows = new System.Windows.Forms.DomainUpDown();
			this.radioM = new System.Windows.Forms.RadioButton();
			this.radioF = new System.Windows.Forms.RadioButton();
			this.radioUnknown = new System.Windows.Forms.RadioButton();
			this.checkExistingSealants = new System.Windows.Forms.CheckBox();
			this.checkCariesExperience = new System.Windows.Forms.CheckBox();
			this.checkEarlyChildCaries = new System.Windows.Forms.CheckBox();
			this.checkMissingAllTeeth = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBirthdate = new System.Windows.Forms.TextBox();
			this.textAge = new System.Windows.Forms.TextBox();
			this.listRace = new System.Windows.Forms.ListBox();
			this.textScreenGroupOrder = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// comboGradeLevel
			// 
			this.comboGradeLevel.BackColor = System.Drawing.SystemColors.Window;
			this.comboGradeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGradeLevel.Location = new System.Drawing.Point(111, 36);
			this.comboGradeLevel.MaxDropDownItems = 25;
			this.comboGradeLevel.Name = "comboGradeLevel";
			this.comboGradeLevel.Size = new System.Drawing.Size(149, 21);
			this.comboGradeLevel.TabIndex = 6;
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(23, 331);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(89, 14);
			this.label35.TabIndex = 16;
			this.label35.Text = "Urgency";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(37, 37);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(75, 17);
			this.label15.TabIndex = 13;
			this.label15.Text = "Grade Level";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(-1, 193);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(113, 17);
			this.label10.TabIndex = 10;
			this.label10.Text = "Race/Ethnicity";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(47, 59);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 14);
			this.label4.TabIndex = 108;
			this.label4.Text = "Age";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(35, 388);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(77, 16);
			this.label8.TabIndex = 115;
			this.label8.Text = "Comments";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textComments
			// 
			this.textComments.Location = new System.Drawing.Point(111, 387);
			this.textComments.MaxLength = 255;
			this.textComments.Name = "textComments";
			this.textComments.Size = new System.Drawing.Size(171, 20);
			this.textComments.TabIndex = 20;
			// 
			// listUrgency
			// 
			this.listUrgency.Location = new System.Drawing.Point(111, 331);
			this.listUrgency.Name = "listUrgency";
			this.listUrgency.Size = new System.Drawing.Size(97, 56);
			this.listUrgency.TabIndex = 9;
			// 
			// checkHasCaries
			// 
			this.checkHasCaries.AutoCheck = false;
			this.checkHasCaries.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHasCaries.Checked = true;
			this.checkHasCaries.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.checkHasCaries.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHasCaries.Location = new System.Drawing.Point(8, 97);
			this.checkHasCaries.Name = "checkHasCaries";
			this.checkHasCaries.Size = new System.Drawing.Size(116, 16);
			this.checkHasCaries.TabIndex = 14;
			this.checkHasCaries.Text = "Has Caries";
			this.checkHasCaries.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHasCaries.ThreeState = true;
			this.checkHasCaries.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkNeedsSealants
			// 
			this.checkNeedsSealants.AutoCheck = false;
			this.checkNeedsSealants.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNeedsSealants.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNeedsSealants.Location = new System.Drawing.Point(4, 161);
			this.checkNeedsSealants.Name = "checkNeedsSealants";
			this.checkNeedsSealants.Size = new System.Drawing.Size(120, 16);
			this.checkNeedsSealants.TabIndex = 18;
			this.checkNeedsSealants.Text = "Needs Sealants";
			this.checkNeedsSealants.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNeedsSealants.ThreeState = true;
			this.checkNeedsSealants.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// updownAgeArrows
			// 
			this.updownAgeArrows.InterceptArrowKeys = false;
			this.updownAgeArrows.Location = new System.Drawing.Point(146, 57);
			this.updownAgeArrows.Name = "updownAgeArrows";
			this.updownAgeArrows.Size = new System.Drawing.Size(20, 20);
			this.updownAgeArrows.TabIndex = 7;
			this.updownAgeArrows.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownAgeArrows_MouseDown);
			// 
			// radioM
			// 
			this.radioM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioM.Location = new System.Drawing.Point(111, 314);
			this.radioM.Name = "radioM";
			this.radioM.Size = new System.Drawing.Size(33, 17);
			this.radioM.TabIndex = 11;
			this.radioM.Text = "M";
			// 
			// radioF
			// 
			this.radioF.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioF.Location = new System.Drawing.Point(144, 314);
			this.radioF.Name = "radioF";
			this.radioF.Size = new System.Drawing.Size(33, 17);
			this.radioF.TabIndex = 12;
			this.radioF.Text = "F";
			// 
			// radioUnknown
			// 
			this.radioUnknown.Checked = true;
			this.radioUnknown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUnknown.Location = new System.Drawing.Point(177, 314);
			this.radioUnknown.Name = "radioUnknown";
			this.radioUnknown.Size = new System.Drawing.Size(33, 17);
			this.radioUnknown.TabIndex = 13;
			this.radioUnknown.TabStop = true;
			this.radioUnknown.Text = "?";
			// 
			// checkExistingSealants
			// 
			this.checkExistingSealants.AutoCheck = false;
			this.checkExistingSealants.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExistingSealants.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExistingSealants.Location = new System.Drawing.Point(-3, 145);
			this.checkExistingSealants.Name = "checkExistingSealants";
			this.checkExistingSealants.Size = new System.Drawing.Size(127, 16);
			this.checkExistingSealants.TabIndex = 17;
			this.checkExistingSealants.Text = "Existing Sealants";
			this.checkExistingSealants.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkExistingSealants.ThreeState = true;
			this.checkExistingSealants.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkCariesExperience
			// 
			this.checkCariesExperience.AutoCheck = false;
			this.checkCariesExperience.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCariesExperience.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCariesExperience.Location = new System.Drawing.Point(-1, 129);
			this.checkCariesExperience.Name = "checkCariesExperience";
			this.checkCariesExperience.Size = new System.Drawing.Size(125, 16);
			this.checkCariesExperience.TabIndex = 16;
			this.checkCariesExperience.Text = "Caries Experience";
			this.checkCariesExperience.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCariesExperience.ThreeState = true;
			this.checkCariesExperience.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkEarlyChildCaries
			// 
			this.checkEarlyChildCaries.AutoCheck = false;
			this.checkEarlyChildCaries.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEarlyChildCaries.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEarlyChildCaries.Location = new System.Drawing.Point(-1, 113);
			this.checkEarlyChildCaries.Name = "checkEarlyChildCaries";
			this.checkEarlyChildCaries.Size = new System.Drawing.Size(125, 16);
			this.checkEarlyChildCaries.TabIndex = 15;
			this.checkEarlyChildCaries.Text = "Early Child. Caries";
			this.checkEarlyChildCaries.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEarlyChildCaries.ThreeState = true;
			this.checkEarlyChildCaries.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkMissingAllTeeth
			// 
			this.checkMissingAllTeeth.AutoCheck = false;
			this.checkMissingAllTeeth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMissingAllTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMissingAllTeeth.Location = new System.Drawing.Point(-1, 177);
			this.checkMissingAllTeeth.Name = "checkMissingAllTeeth";
			this.checkMissingAllTeeth.Size = new System.Drawing.Size(125, 16);
			this.checkMissingAllTeeth.TabIndex = 19;
			this.checkMissingAllTeeth.Text = "Missing All Teeth";
			this.checkMissingAllTeeth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkMissingAllTeeth.ThreeState = true;
			this.checkMissingAllTeeth.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(32, 78);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 17);
			this.label5.TabIndex = 139;
			this.label5.Text = "Birthdate";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBirthdate
			// 
			this.textBirthdate.AcceptsReturn = true;
			this.textBirthdate.Location = new System.Drawing.Point(111, 77);
			this.textBirthdate.Multiline = true;
			this.textBirthdate.Name = "textBirthdate";
			this.textBirthdate.Size = new System.Drawing.Size(81, 20);
			this.textBirthdate.TabIndex = 8;
			this.textBirthdate.Validating += new System.ComponentModel.CancelEventHandler(this.textBirthdate_Validating);
			// 
			// textAge
			// 
			this.textAge.Location = new System.Drawing.Point(111, 57);
			this.textAge.Name = "textAge";
			this.textAge.Size = new System.Drawing.Size(35, 20);
			this.textAge.TabIndex = 141;
			this.textAge.Validating += new System.ComponentModel.CancelEventHandler(this.textAge_Validating);
			// 
			// listRace
			// 
			this.listRace.Location = new System.Drawing.Point(111, 193);
			this.listRace.Name = "listRace";
			this.listRace.Size = new System.Drawing.Size(113, 121);
			this.listRace.TabIndex = 142;
			// 
			// textScreenGroupOrder
			// 
			this.textScreenGroupOrder.Location = new System.Drawing.Point(111, 16);
			this.textScreenGroupOrder.Name = "textScreenGroupOrder";
			this.textScreenGroupOrder.Size = new System.Drawing.Size(35, 20);
			this.textScreenGroupOrder.TabIndex = 144;
			this.textScreenGroupOrder.Validating += new System.ComponentModel.CancelEventHandler(this.textScreenGroupOrder_Validating);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(73, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 14);
			this.label1.TabIndex = 143;
			this.label1.Text = "Row";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(-1, 314);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 17);
			this.label2.TabIndex = 10;
			this.label2.Text = "Gender";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12, 452);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 24;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(224, 452);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 24;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(224, 422);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 24;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormScreenEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(311, 488);
			this.Controls.Add(this.textComments);
			this.Controls.Add(this.listUrgency);
			this.Controls.Add(this.textScreenGroupOrder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listRace);
			this.Controls.Add(this.textAge);
			this.Controls.Add(this.checkNeedsSealants);
			this.Controls.Add(this.comboGradeLevel);
			this.Controls.Add(this.updownAgeArrows);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBirthdate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkMissingAllTeeth);
			this.Controls.Add(this.checkEarlyChildCaries);
			this.Controls.Add(this.checkCariesExperience);
			this.Controls.Add(this.checkExistingSealants);
			this.Controls.Add(this.radioUnknown);
			this.Controls.Add(this.radioF);
			this.Controls.Add(this.radioM);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.checkHasCaries);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label35);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label10);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormScreenEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Screening";
			this.Load += new System.EventHandler(this.FormScreenEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScreenEdit_Load(object sender, System.EventArgs e) {
			textScreenGroupOrder.Text=ScreenCur.ScreenGroupOrder.ToString();
			switch(ScreenCur.Gender){
				case PatientGender.Unknown:
					radioUnknown.Checked=true;
					break;
				case PatientGender.Male:
					radioM.Checked=true;
					break;
				case PatientGender.Female:
					radioF.Checked=true;
					break;
			}
			listRace.Items.AddRange(Enum.GetNames(typeof(PatientRace)));
			listRace.SelectedIndex=(int)ScreenCur.Race;
			comboGradeLevel.Items.AddRange(Enum.GetNames(typeof(PatientGrade)));
			comboGradeLevel.SelectedIndex=(int)ScreenCur.GradeLevel;
			ArrayList items=new ArrayList();
			if(ScreenCur.Age==0)
				textAge.Text="";
			else
				textAge.Text=ScreenCur.Age.ToString();
			listUrgency.Items.AddRange(Enum.GetNames(typeof(TreatmentUrgency)));
			listUrgency.SelectedIndex=(int)ScreenCur.Urgency;
			SetCheckState(checkHasCaries,ScreenCur.HasCaries);
			SetCheckState(checkNeedsSealants,ScreenCur.NeedsSealants);
			SetCheckState(checkCariesExperience,ScreenCur.CariesExperience);
			SetCheckState(checkEarlyChildCaries,ScreenCur.EarlyChildCaries);
			SetCheckState(checkExistingSealants,ScreenCur.ExistingSealants);
			SetCheckState(checkMissingAllTeeth,ScreenCur.MissingAllTeeth);
			if(ScreenCur.Birthdate.Year<1880)
				textBirthdate.Text="";
			else
				textBirthdate.Text=ScreenCur.Birthdate.ToShortDateString();
			textComments.Text=ScreenCur.Comments;
		}

		private void SetCheckState(CheckBox checkBox,YN state){
			switch(state){
				case YN.Unknown:
					checkBox.CheckState=CheckState.Indeterminate;
					break;
				case YN.Yes:
					checkBox.CheckState=CheckState.Checked;
					break;
				case YN.No:
					checkBox.CheckState=CheckState.Unchecked;
					break;
			}
		}

		private void textScreenGroupOrder_Validating(object sender,System.ComponentModel.CancelEventArgs e){
			try{
				Convert.ToInt32(textScreenGroupOrder.Text);
			}
			catch{
				MessageBox.Show("Order invalid.");
				e.Cancel=true;
			}
		}

		private void textAge_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textAge.Text=="")
				return;
			if(textAge.Text=="0"){
				textAge.Text="";
				return;
			}
			try{
				Convert.ToInt32(textAge.Text);
			}
			catch{
				MessageBox.Show("Age invalid.");
				e.Cancel=true;
			}
		}

		private void updownAgeArrows_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//this is necessary because Microsoft's updown control is too buggy to be useful
			int currentValue=0;
			try{
				currentValue=PIn.Int(textAge.Text);
			}
			catch{
				return;
			}
			if(e.Y<8){//up
				textAge.Text=(currentValue+1).ToString();
			}
			else{//down
				if(textAge.Text=="")
					return;
				if(textAge.Text=="1"){
					textAge.Text="";
					return;
				}
				textAge.Text=(currentValue-1).ToString();
			}
		}

		private void textBirthdate_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			if(textBirthdate.Text=="")
				return;
			try{
				DateTime.Parse(textBirthdate.Text);
			}
			catch{
				MessageBox.Show("Birthdate invalid.");
				e.Cancel=true;
			}
		}

		///<summary>Used by all 6 checkboxes to customize order of 3-state checking.</summary>
		private void checkBox_Click(object sender, System.EventArgs e) {
			switch(((CheckBox)sender).CheckState){
				case CheckState.Indeterminate:
					((CheckBox)sender).CheckState=CheckState.Checked;
					break;
				case CheckState.Checked:
					((CheckBox)sender).CheckState=CheckState.Unchecked;
					break;
				case CheckState.Unchecked:
					((CheckBox)sender).CheckState=CheckState.Indeterminate;
					break;
			}
		}

		private YN GetCheckState(CheckBox checkBox){
			switch(checkBox.CheckState){
				case CheckState.Indeterminate:
					return YN.Unknown;
				case CheckState.Checked:
					return YN.Yes;
				case CheckState.Unchecked:
					return YN.No;
			}
			return YN.Unknown;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				Screens.Delete(ScreenCur);
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Are you sure you want to delete this screening?")){
				return;
			}
			Screens.Delete(ScreenCur);
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//the first 6 fields are handled when the ScreenGroup is saved.
			ScreenCur.ScreenGroupOrder=PIn.Int(textScreenGroupOrder.Text);
			ScreenCur.ScreenGroupNum=ScreenGroupCur.ScreenGroupNum;
			if(radioUnknown.Checked) {
				ScreenCur.Gender=PatientGender.Unknown;
			}
			else if(radioM.Checked) {
				ScreenCur.Gender=PatientGender.Male;
			}
			else if(radioF.Checked) {
				ScreenCur.Gender=PatientGender.Female;
			}
			ScreenCur.Race=(PatientRace)listRace.SelectedIndex;
			ScreenCur.GradeLevel=(PatientGrade)comboGradeLevel.SelectedIndex;
			ScreenCur.Age=PIn.Byte(textAge.Text);//"" is OK
			ScreenCur.Urgency=(TreatmentUrgency)listUrgency.SelectedIndex;
			ScreenCur.HasCaries=GetCheckState(checkHasCaries);
			ScreenCur.NeedsSealants=GetCheckState(checkNeedsSealants);
			ScreenCur.CariesExperience=GetCheckState(checkCariesExperience);
			ScreenCur.EarlyChildCaries=GetCheckState(checkEarlyChildCaries);
			ScreenCur.ExistingSealants=GetCheckState(checkExistingSealants);
			ScreenCur.MissingAllTeeth=GetCheckState(checkMissingAllTeeth);
			ScreenCur.Birthdate=PIn.Date(textBirthdate.Text);//"" is OK
			ScreenCur.Comments=textComments.Text;
			if(IsNew) {
				Screens.Insert(ScreenCur);
			}
			else {
				Screens.Update(ScreenCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		

		

		

		

		

		

		


		

		

		


	}
}





















