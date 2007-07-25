using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReqStudentEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textStudent;
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;	
		private OpenDental.UI.Button butDelete;
		public ReqStudent ReqCur;
		private TextBox textAppointment;
		private Label label4;
		private OpenDental.UI.Button butDetachApt;
		private OpenDental.UI.Button butDetachPat;
		private TextBox textPatient;
		private Label label5;
		private Label label6;
		private TextBox textDateCompleted;
		private ComboBox comboInstructor;
		private Label label11;
		private OpenDental.UI.Button butNow;
		private TextBox textDescription;
		private Label label2;
		private TextBox textCourse;
		private Label label10;
		private OpenDental.UI.Button butSelectPat;

		///<summary></summary>
		public FormReqStudentEdit(){
			InitializeComponent();// Required for Windows Form Designer support
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReqStudentEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textStudent = new System.Windows.Forms.TextBox();
			this.textAppointment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textDateCompleted = new System.Windows.Forms.TextBox();
			this.comboInstructor = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.butNow = new OpenDental.UI.Button();
			this.butDetachPat = new OpenDental.UI.Button();
			this.butDetachApt = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textCourse = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.butSelectPat = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Student";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textStudent
			// 
			this.textStudent.Location = new System.Drawing.Point(133,8);
			this.textStudent.Name = "textStudent";
			this.textStudent.ReadOnly = true;
			this.textStudent.Size = new System.Drawing.Size(319,20);
			this.textStudent.TabIndex = 0;
			// 
			// textAppointment
			// 
			this.textAppointment.Location = new System.Drawing.Point(133,133);
			this.textAppointment.Name = "textAppointment";
			this.textAppointment.ReadOnly = true;
			this.textAppointment.Size = new System.Drawing.Size(319,20);
			this.textAppointment.TabIndex = 103;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(44,134);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(89,17);
			this.label4.TabIndex = 104;
			this.label4.Text = "Appointment";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(133,108);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(319,20);
			this.textPatient.TabIndex = 106;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(44,109);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(89,17);
			this.label5.TabIndex = 107;
			this.label5.Text = "Patient";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8,84);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(123,17);
			this.label6.TabIndex = 110;
			this.label6.Text = "Date Completed";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateCompleted
			// 
			this.textDateCompleted.Location = new System.Drawing.Point(133,83);
			this.textDateCompleted.Name = "textDateCompleted";
			this.textDateCompleted.Size = new System.Drawing.Size(114,20);
			this.textDateCompleted.TabIndex = 111;
			// 
			// comboInstructor
			// 
			this.comboInstructor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboInstructor.FormattingEnabled = true;
			this.comboInstructor.Location = new System.Drawing.Point(133,158);
			this.comboInstructor.MaxDropDownItems = 25;
			this.comboInstructor.Name = "comboInstructor";
			this.comboInstructor.Size = new System.Drawing.Size(158,21);
			this.comboInstructor.TabIndex = 121;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(44,161);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(89,17);
			this.label11.TabIndex = 120;
			this.label11.Text = "Instructor";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butNow
			// 
			this.butNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNow.Autosize = true;
			this.butNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow.CornerRadius = 4F;
			this.butNow.Location = new System.Drawing.Point(249,81);
			this.butNow.Name = "butNow";
			this.butNow.Size = new System.Drawing.Size(75,24);
			this.butNow.TabIndex = 118;
			this.butNow.Text = "Now";
			this.butNow.Click += new System.EventHandler(this.butNow_Click);
			// 
			// butDetachPat
			// 
			this.butDetachPat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetachPat.Autosize = true;
			this.butDetachPat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetachPat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetachPat.CornerRadius = 4F;
			this.butDetachPat.Location = new System.Drawing.Point(458,105);
			this.butDetachPat.Name = "butDetachPat";
			this.butDetachPat.Size = new System.Drawing.Size(75,24);
			this.butDetachPat.TabIndex = 108;
			this.butDetachPat.Text = "Detach";
			this.butDetachPat.Click += new System.EventHandler(this.butDetachPat_Click);
			// 
			// butDetachApt
			// 
			this.butDetachApt.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetachApt.Autosize = true;
			this.butDetachApt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetachApt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetachApt.CornerRadius = 4F;
			this.butDetachApt.Location = new System.Drawing.Point(458,130);
			this.butDetachApt.Name = "butDetachApt";
			this.butDetachApt.Size = new System.Drawing.Size(75,24);
			this.butDetachApt.TabIndex = 105;
			this.butDetachApt.Text = "Detach";
			this.butDetachApt.Click += new System.EventHandler(this.butDetachApt_Click);
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
			this.butDelete.Location = new System.Drawing.Point(27,245);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 4;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(458,245);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
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
			this.butCancel.Location = new System.Drawing.Point(536,245);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(133,58);
			this.textDescription.Name = "textDescription";
			this.textDescription.ReadOnly = true;
			this.textDescription.Size = new System.Drawing.Size(319,20);
			this.textDescription.TabIndex = 123;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121,17);
			this.label2.TabIndex = 124;
			this.label2.Text = "Requirement";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCourse
			// 
			this.textCourse.Location = new System.Drawing.Point(133,33);
			this.textCourse.Name = "textCourse";
			this.textCourse.ReadOnly = true;
			this.textCourse.Size = new System.Drawing.Size(319,20);
			this.textCourse.TabIndex = 125;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(44,34);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(89,17);
			this.label10.TabIndex = 126;
			this.label10.Text = "Course";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSelectPat
			// 
			this.butSelectPat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSelectPat.Autosize = true;
			this.butSelectPat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelectPat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSelectPat.CornerRadius = 4F;
			this.butSelectPat.Location = new System.Drawing.Point(536,105);
			this.butSelectPat.Name = "butSelectPat";
			this.butSelectPat.Size = new System.Drawing.Size(75,24);
			this.butSelectPat.TabIndex = 127;
			this.butSelectPat.Text = "Select";
			this.butSelectPat.Click += new System.EventHandler(this.butSelectPat_Click);
			// 
			// FormReqStudentEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(657,289);
			this.Controls.Add(this.butSelectPat);
			this.Controls.Add(this.textCourse);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butNow);
			this.Controls.Add(this.textDateCompleted);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboInstructor);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.butDetachPat);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butDetachApt);
			this.Controls.Add(this.textAppointment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textStudent);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReqStudentEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Student Requirement";
			this.Load += new System.EventHandler(this.FormReqStudentEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormReqStudentEdit_Load(object sender, System.EventArgs e) {
			//There should only be two types of users who are allowed to get this far:
			//Students editing their own req, and users with setup perm.  But we will double check.
			Provider provUser=Providers.GetProv(Security.CurUser.ProvNum);
			if(provUser!=null && provUser.SchoolClassNum!=0) {//A student is logged in
				//the student only has permission to view/attach/detach their own requirements
				if(provUser.ProvNum!=ReqCur.ProvNum) {
					//but this should never happen
					MsgBox.Show(this,"Students may only edit their own requirements.");
					butDelete.Enabled=false;
					butOK.Enabled=false;
				}
				else{//the student matches
					butDelete.Enabled=false;
					textDateCompleted.Enabled=false;
					butNow.Enabled=false;
					comboInstructor.Enabled=false;
					//a student is only allowed to change the patient and appointment.
				}
			}
			else {//A student is not logged in
				if(!Security.IsAuthorized(Permissions.Setup,DateTime.MinValue,true)) {//suppress message
					butDelete.Enabled=false;
					butOK.Enabled=false;
				}
			}
			textStudent.Text=Providers.GetNameLF(ReqCur.ProvNum);
			textCourse.Text=SchoolCourses.GetDescript(ReqCur.SchoolCourseNum);
			textDescription.Text=ReqCur.Descript;
			if(ReqCur.DateCompleted.Year>1880){
				textDateCompleted.Text=ReqCur.DateCompleted.ToShortDateString();
			}
			//if an apt is attached, then the same pat must be attached.
			Patient pat=Patients.GetPat(ReqCur.PatNum);
			if(pat!=null) {
				textPatient.Text=pat.GetNameFL();
			}
			Appointment apt=Appointments.GetOneApt(ReqCur.AptNum);
			if(apt!=null) {
				if(apt.AptStatus==ApptStatus.UnschedList) {
					textAppointment.Text=Lan.g(this,"Unscheduled");
				}
				else {
					textAppointment.Text=apt.AptDateTime.ToShortDateString()+" "+apt.AptDateTime.ToShortTimeString();
				}
				textAppointment.Text+=", "+apt.ProcDescript;
			}
			comboInstructor.Items.Add(Lan.g(this,"None"));
			comboInstructor.SelectedIndex=0;
			for(int i=0;i<Providers.List.Length;i++) {
				comboInstructor.Items.Add(Providers.GetNameLF(Providers.List[i].ProvNum));
				if(Providers.List[i].ProvNum==ReqCur.InstructorNum) {
					comboInstructor.SelectedIndex=i+1;
				}
			}
		}

		private void butDetachApt_Click(object sender,EventArgs e) {
			ReqCur.AptNum=0;
			textAppointment.Text="";
		}

		private void butDetachPat_Click(object sender,EventArgs e) {
			ReqCur.PatNum=0;
			textPatient.Text="";
		}

		private void butNow_Click(object sender,EventArgs e) {
			textDateCompleted.Text=MiscData.GetNowDateTime().ToShortDateString();
		}

		private void butSelectPat_Click(object sender,EventArgs e) {
			FormPatientSelect FormP=new FormPatientSelect();
			FormP.SelectionModeOnly=true;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			ReqCur.PatNum=FormP.SelectedPatNum;
			textPatient.Text=Patients.GetPat(ReqCur.PatNum).GetNameFL();
			//if the patient changed, then the appointment must be detached.
			ReqCur.AptNum=0;
			textAppointment.Text="";
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			//if(!MsgBox.Show(this,true,"Delete this student requirement?  This is typically handled by using the synch feature from requirements needed.")){
			//	return;
			//}
			try{
				ReqStudents.Delete(ReqCur.ReqStudentNum);//disallows deleting req that exists in reqNeeded.
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateCompleted.Text!=""){
				try{
					DateTime.Parse(textDateCompleted.Text);
				}
				catch{
					MsgBox.Show(this,"Date is invalid.");
					return;
				}
			}
			//ReqNeededNum-not editable
			//Descript-not editable
			//SchoolCourseNum-not editable
			//ProvNum-Student not editable
			//AptNum-handled earlier
			//PatNum-handled earlier
			//InstructorNum
			if(comboInstructor.SelectedIndex==0){
				ReqCur.InstructorNum=0;
			}
			else{
				ReqCur.InstructorNum=Providers.List[comboInstructor.SelectedIndex-1].ProvNum;
			}
			//DateCompleted
			ReqCur.DateCompleted=PIn.PDate(textDateCompleted.Text);
			try{
				//if(IsNew){//inserting is always done as part of reqneededs.synch
				//	LabCases.Insert(CaseCur);
				//}
				//else{
				ReqStudents.Update(ReqCur);
				//}
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		

		

		

		

		


	}
}





















