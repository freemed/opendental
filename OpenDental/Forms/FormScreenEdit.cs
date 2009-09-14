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
		private OpenDental.UI.Button but1;
		private OpenDental.UI.Button but5;
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
			this.but1 = new OpenDental.UI.Button();
			this.but5 = new OpenDental.UI.Button();
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
			this.butOK = new OpenDental.UI.Button();
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
			this.SuspendLayout();
			// 
			// but1
			// 
			this.but1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but1.Autosize = true;
			this.but1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but1.CornerRadius = 4F;
			this.but1.Location = new System.Drawing.Point(14,274);
			this.but1.Name = "but1";
			this.but1.Size = new System.Drawing.Size(48,21);
			this.but1.TabIndex = 21;
			this.but1.Text = "Add 1";
			this.but1.Click += new System.EventHandler(this.but1_Click);
			// 
			// but5
			// 
			this.but5.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but5.Autosize = true;
			this.but5.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but5.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but5.CornerRadius = 4F;
			this.but5.Location = new System.Drawing.Point(62,274);
			this.but5.Name = "but5";
			this.but5.Size = new System.Drawing.Size(48,21);
			this.but5.TabIndex = 22;
			this.but5.Text = "Add 5";
			this.but5.Click += new System.EventHandler(this.but5_Click);
			// 
			// comboGradeLevel
			// 
			this.comboGradeLevel.BackColor = System.Drawing.SystemColors.Window;
			this.comboGradeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGradeLevel.Location = new System.Drawing.Point(78,20);
			this.comboGradeLevel.MaxDropDownItems = 25;
			this.comboGradeLevel.Name = "comboGradeLevel";
			this.comboGradeLevel.Size = new System.Drawing.Size(149,21);
			this.comboGradeLevel.TabIndex = 6;
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(123,128);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(89,14);
			this.label35.TabIndex = 16;
			this.label35.Text = "Urgency";
			this.label35.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(1,21);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(75,17);
			this.label15.TabIndex = 13;
			this.label15.Text = "Grade Level";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(5,64);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(113,17);
			this.label10.TabIndex = 10;
			this.label10.Text = "Race/Ethnicity";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11,42);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65,14);
			this.label4.TabIndex = 108;
			this.label4.Text = "Age";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(2,255);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(77,16);
			this.label8.TabIndex = 115;
			this.label8.Text = "Comments";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textComments
			// 
			this.textComments.Location = new System.Drawing.Point(59,253);
			this.textComments.MaxLength = 255;
			this.textComments.Name = "textComments";
			this.textComments.Size = new System.Drawing.Size(171,20);
			this.textComments.TabIndex = 20;
			// 
			// listUrgency
			// 
			this.listUrgency.Location = new System.Drawing.Point(125,144);
			this.listUrgency.Name = "listUrgency";
			this.listUrgency.Size = new System.Drawing.Size(97,56);
			this.listUrgency.TabIndex = 9;
			// 
			// checkHasCaries
			// 
			this.checkHasCaries.AutoCheck = false;
			this.checkHasCaries.Checked = true;
			this.checkHasCaries.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.checkHasCaries.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHasCaries.Location = new System.Drawing.Point(6,204);
			this.checkHasCaries.Name = "checkHasCaries";
			this.checkHasCaries.Size = new System.Drawing.Size(98,16);
			this.checkHasCaries.TabIndex = 14;
			this.checkHasCaries.Text = "Has Caries";
			this.checkHasCaries.ThreeState = true;
			this.checkHasCaries.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkNeedsSealants
			// 
			this.checkNeedsSealants.AutoCheck = false;
			this.checkNeedsSealants.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNeedsSealants.Location = new System.Drawing.Point(113,220);
			this.checkNeedsSealants.Name = "checkNeedsSealants";
			this.checkNeedsSealants.Size = new System.Drawing.Size(102,16);
			this.checkNeedsSealants.TabIndex = 18;
			this.checkNeedsSealants.Text = "Needs Sealants";
			this.checkNeedsSealants.ThreeState = true;
			this.checkNeedsSealants.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(182,274);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(48,21);
			this.butOK.TabIndex = 24;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// updownAgeArrows
			// 
			this.updownAgeArrows.InterceptArrowKeys = false;
			this.updownAgeArrows.Location = new System.Drawing.Point(113,41);
			this.updownAgeArrows.Name = "updownAgeArrows";
			this.updownAgeArrows.Size = new System.Drawing.Size(20,20);
			this.updownAgeArrows.TabIndex = 7;
			this.updownAgeArrows.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownAgeArrows_MouseDown);
			// 
			// radioM
			// 
			this.radioM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioM.Location = new System.Drawing.Point(124,99);
			this.radioM.Name = "radioM";
			this.radioM.Size = new System.Drawing.Size(33,17);
			this.radioM.TabIndex = 11;
			this.radioM.Text = "M";
			// 
			// radioF
			// 
			this.radioF.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioF.Location = new System.Drawing.Point(157,99);
			this.radioF.Name = "radioF";
			this.radioF.Size = new System.Drawing.Size(33,17);
			this.radioF.TabIndex = 12;
			this.radioF.Text = "F";
			// 
			// radioUnknown
			// 
			this.radioUnknown.Checked = true;
			this.radioUnknown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUnknown.Location = new System.Drawing.Point(190,99);
			this.radioUnknown.Name = "radioUnknown";
			this.radioUnknown.Size = new System.Drawing.Size(33,17);
			this.radioUnknown.TabIndex = 13;
			this.radioUnknown.TabStop = true;
			this.radioUnknown.Text = "?";
			// 
			// checkExistingSealants
			// 
			this.checkExistingSealants.AutoCheck = false;
			this.checkExistingSealants.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExistingSealants.Location = new System.Drawing.Point(113,204);
			this.checkExistingSealants.Name = "checkExistingSealants";
			this.checkExistingSealants.Size = new System.Drawing.Size(109,16);
			this.checkExistingSealants.TabIndex = 17;
			this.checkExistingSealants.Text = "Existing Sealants";
			this.checkExistingSealants.ThreeState = true;
			this.checkExistingSealants.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkCariesExperience
			// 
			this.checkCariesExperience.AutoCheck = false;
			this.checkCariesExperience.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCariesExperience.Location = new System.Drawing.Point(6,236);
			this.checkCariesExperience.Name = "checkCariesExperience";
			this.checkCariesExperience.Size = new System.Drawing.Size(107,16);
			this.checkCariesExperience.TabIndex = 16;
			this.checkCariesExperience.Text = "Caries Experience";
			this.checkCariesExperience.ThreeState = true;
			this.checkCariesExperience.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkEarlyChildCaries
			// 
			this.checkEarlyChildCaries.AutoCheck = false;
			this.checkEarlyChildCaries.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEarlyChildCaries.Location = new System.Drawing.Point(6,220);
			this.checkEarlyChildCaries.Name = "checkEarlyChildCaries";
			this.checkEarlyChildCaries.Size = new System.Drawing.Size(107,16);
			this.checkEarlyChildCaries.TabIndex = 15;
			this.checkEarlyChildCaries.Text = "Early Child. Caries";
			this.checkEarlyChildCaries.ThreeState = true;
			this.checkEarlyChildCaries.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// checkMissingAllTeeth
			// 
			this.checkMissingAllTeeth.AutoCheck = false;
			this.checkMissingAllTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkMissingAllTeeth.Location = new System.Drawing.Point(113,236);
			this.checkMissingAllTeeth.Name = "checkMissingAllTeeth";
			this.checkMissingAllTeeth.Size = new System.Drawing.Size(107,16);
			this.checkMissingAllTeeth.TabIndex = 19;
			this.checkMissingAllTeeth.Text = "Missing All Teeth";
			this.checkMissingAllTeeth.ThreeState = true;
			this.checkMissingAllTeeth.Click += new System.EventHandler(this.checkBox_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(144,59);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80,17);
			this.label5.TabIndex = 139;
			this.label5.Text = "(Birthdate)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBirthdate
			// 
			this.textBirthdate.AcceptsReturn = true;
			this.textBirthdate.Location = new System.Drawing.Point(146,41);
			this.textBirthdate.Multiline = true;
			this.textBirthdate.Name = "textBirthdate";
			this.textBirthdate.Size = new System.Drawing.Size(81,20);
			this.textBirthdate.TabIndex = 8;
			this.textBirthdate.Validating += new System.ComponentModel.CancelEventHandler(this.textBirthdate_Validating);
			// 
			// textAge
			// 
			this.textAge.Location = new System.Drawing.Point(78,41);
			this.textAge.Name = "textAge";
			this.textAge.Size = new System.Drawing.Size(35,20);
			this.textAge.TabIndex = 141;
			this.textAge.Validating += new System.ComponentModel.CancelEventHandler(this.textAge_Validating);
			// 
			// listRace
			// 
			this.listRace.Location = new System.Drawing.Point(6,82);
			this.listRace.Name = "listRace";
			this.listRace.Size = new System.Drawing.Size(113,121);
			this.listRace.TabIndex = 142;
			// 
			// textScreenGroupOrder
			// 
			this.textScreenGroupOrder.Location = new System.Drawing.Point(78,0);
			this.textScreenGroupOrder.Name = "textScreenGroupOrder";
			this.textScreenGroupOrder.Size = new System.Drawing.Size(35,20);
			this.textScreenGroupOrder.TabIndex = 144;
			this.textScreenGroupOrder.Validating += new System.ComponentModel.CancelEventHandler(this.textScreenGroupOrder_Validating);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37,2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39,14);
			this.label1.TabIndex = 143;
			this.label1.Text = "Row";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormScreenEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(234,296);
			this.Controls.Add(this.textScreenGroupOrder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listRace);
			this.Controls.Add(this.textAge);
			this.Controls.Add(this.checkNeedsSealants);
			this.Controls.Add(this.textComments);
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
			this.Controls.Add(this.listUrgency);
			this.Controls.Add(this.radioF);
			this.Controls.Add(this.radioM);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.checkHasCaries);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label35);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.but1);
			this.Controls.Add(this.but5);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScreenEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Screening";
			this.Load += new System.EventHandler(this.FormScreenEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScreenEdit_Load(object sender, System.EventArgs e) {
			Location=new Point(200,200);
			//Size=new Size(240,346);
			if(IsNew){
				butOK.Visible=false;
			}
			else{//editing an existing screen
				but1.Visible=false;
				but5.Visible=false;
			}
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
				currentValue=PIn.PInt(textAge.Text);
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

		private void but1_Click(object sender, System.EventArgs e) {
			AddSome(1);
		}

		private void but5_Click(object sender, System.EventArgs e) {
			AddSome(5);
		}

		private void AddSome(int numberToAdd){
			FillCur();
			for(int i=0;i<numberToAdd;i++){
				Screens.Insert(ScreenCur);
				ScreenCur.ScreenGroupOrder=ScreenCur.ScreenGroupOrder+1;//increments for next
			}
			DialogResult=DialogResult.OK;//this triggers window to come back up again.
		}

		private void FillCur(){
			//the first 6 fields are handled when the ScreenGroup is saved.
			ScreenCur.ScreenGroupOrder=PIn.PInt(textScreenGroupOrder.Text);
			ScreenCur.ScreenGroupNum=ScreenGroupCur.ScreenGroupNum;
			if(radioUnknown.Checked)
        ScreenCur.Gender=PatientGender.Unknown;
			else if(radioM.Checked)
        ScreenCur.Gender=PatientGender.Male;
			else if(radioF.Checked)
        ScreenCur.Gender=PatientGender.Female;
			ScreenCur.Race=(PatientRace)listRace.SelectedIndex;
			ScreenCur.GradeLevel=(PatientGrade)comboGradeLevel.SelectedIndex;
			ScreenCur.Age=PIn.PInt(textAge.Text);//"" is OK
			ScreenCur.Urgency=(TreatmentUrgency)listUrgency.SelectedIndex;
			ScreenCur.HasCaries=GetCheckState(checkHasCaries);
			ScreenCur.NeedsSealants=GetCheckState(checkNeedsSealants);
			ScreenCur.CariesExperience=GetCheckState(checkCariesExperience);
			ScreenCur.EarlyChildCaries=GetCheckState(checkEarlyChildCaries);
			ScreenCur.ExistingSealants=GetCheckState(checkExistingSealants);
			ScreenCur.MissingAllTeeth=GetCheckState(checkMissingAllTeeth);
			ScreenCur.Birthdate=PIn.PDate(textBirthdate.Text);//"" is OK
			ScreenCur.Comments=textComments.Text;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//can only get to here if not IsNew
			FillCur();
			Screens.Update(ScreenCur);
			DialogResult=DialogResult.OK;
		}

		

		

		

		

		

		

		

		

		

		

		

		

		


		

		

		


	}
}





















