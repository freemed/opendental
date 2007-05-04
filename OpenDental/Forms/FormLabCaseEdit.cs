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
	public class FormLabCaseEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPatient;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textNotes;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		public LabCase CaseCur;
		private ComboBox comboLab;
		private TextBox textAppointment;
		private Label label4;
		private OpenDental.UI.Button butDetach;
		private OpenDental.UI.Button butDetachPlanned;
		private TextBox textPlanned;
		private Label label5;
		private List<Laboratory> ListLabs;

		///<summary></summary>
		public FormLabCaseEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabCaseEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.comboLab = new System.Windows.Forms.ComboBox();
			this.textAppointment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butDetach = new OpenDental.UI.Button();
			this.butDetachPlanned = new OpenDental.UI.Button();
			this.textPlanned = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Patient";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(160,20);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(357,20);
			this.textPatient.TabIndex = 0;
			// 
			// textNotes
			// 
			this.textNotes.Location = new System.Drawing.Point(160,169);
			this.textNotes.MaxLength = 255;
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.Size = new System.Drawing.Size(357,127);
			this.textNotes.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7,171);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151,17);
			this.label3.TabIndex = 101;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9,48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151,17);
			this.label2.TabIndex = 99;
			this.label2.Text = "Lab";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			this.butDelete.Location = new System.Drawing.Point(27,342);
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
			this.butOK.Location = new System.Drawing.Point(486,342);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
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
			this.butCancel.Location = new System.Drawing.Point(577,342);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// comboLab
			// 
			this.comboLab.FormattingEnabled = true;
			this.comboLab.Location = new System.Drawing.Point(160,45);
			this.comboLab.Name = "comboLab";
			this.comboLab.Size = new System.Drawing.Size(291,21);
			this.comboLab.TabIndex = 102;
			// 
			// textAppointment
			// 
			this.textAppointment.Location = new System.Drawing.Point(160,71);
			this.textAppointment.Name = "textAppointment";
			this.textAppointment.ReadOnly = true;
			this.textAppointment.Size = new System.Drawing.Size(357,20);
			this.textAppointment.TabIndex = 103;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(148,17);
			this.label4.TabIndex = 104;
			this.label4.Text = "Appointment";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDetach
			// 
			this.butDetach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetach.Autosize = true;
			this.butDetach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetach.CornerRadius = 4F;
			this.butDetach.Location = new System.Drawing.Point(523,68);
			this.butDetach.Name = "butDetach";
			this.butDetach.Size = new System.Drawing.Size(75,24);
			this.butDetach.TabIndex = 105;
			this.butDetach.Text = "Detach";
			this.butDetach.Click += new System.EventHandler(this.butDetach_Click);
			// 
			// butDetachPlanned
			// 
			this.butDetachPlanned.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetachPlanned.Autosize = true;
			this.butDetachPlanned.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetachPlanned.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetachPlanned.CornerRadius = 4F;
			this.butDetachPlanned.Location = new System.Drawing.Point(523,93);
			this.butDetachPlanned.Name = "butDetachPlanned";
			this.butDetachPlanned.Size = new System.Drawing.Size(75,24);
			this.butDetachPlanned.TabIndex = 108;
			this.butDetachPlanned.Text = "Detach";
			this.butDetachPlanned.Click += new System.EventHandler(this.butDetachPlanned_Click);
			// 
			// textPlanned
			// 
			this.textPlanned.Location = new System.Drawing.Point(160,96);
			this.textPlanned.Name = "textPlanned";
			this.textPlanned.ReadOnly = true;
			this.textPlanned.Size = new System.Drawing.Size(357,20);
			this.textPlanned.TabIndex = 106;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,97);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(148,17);
			this.label5.TabIndex = 107;
			this.label5.Text = "Planned Appointment";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormLabCaseEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(678,386);
			this.Controls.Add(this.butDetachPlanned);
			this.Controls.Add(this.textPlanned);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butDetach);
			this.Controls.Add(this.textAppointment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboLab);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLabCaseEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Lab Case";
			this.Load += new System.EventHandler(this.FormLabCaseEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormLabCaseEdit_Load(object sender, System.EventArgs e) {
			textPatient.Text=Patients.GetPat(CaseCur.PatNum).GetNameFL();
			ListLabs=Laboratories.Refresh();
			for(int i=0;i<ListLabs.Count;i++){
				comboLab.Items.Add(ListLabs[i].Description+" "+ListLabs[i].Phone);
				if(ListLabs[i].LaboratoryNum==CaseCur.LaboratoryNum){
					comboLab.SelectedIndex=i;
				}
			}
			Appointment apt=Appointments.GetOneApt(CaseCur.AptNum);
			if(apt!=null){
				textAppointment.Text=apt.AptDateTime.ToString()+" "+apt.ProcDescript;
			}
			apt=Appointments.GetOneApt(CaseCur.PlannedAptNum);
			if(apt!=null){
				textPlanned.Text=apt.ProcDescript;
			}

		}

		private void butDetach_Click(object sender,EventArgs e) {
			CaseCur.AptNum=0;
		}

		private void butDetachPlanned_Click(object sender,EventArgs e) {
			CaseCur.PlannedAptNum=0;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete Lab Case?")){
				return;
			}
			try{
				LabCases.Delete(CaseCur.LabCaseNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(comboLab.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a lab first.");
				return;
			}
			CaseCur.LaboratoryNum=ListLabs[comboLab.SelectedIndex].LaboratoryNum;
			//AptNum
			//PlannedAptNum



			try{
				if(IsNew){
					LabCases.Insert(CaseCur);
				}
				else{
					LabCases.Update(CaseCur);
				}
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





















