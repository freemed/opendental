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
		private System.Windows.Forms.TextBox textInstructions;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		public LabCase CaseCur;
		private TextBox textAppointment;
		private Label label4;
		private OpenDental.UI.Button butDetach;
		private OpenDental.UI.Button butDetachPlanned;
		private TextBox textPlanned;
		private Label label5;
		private Label label6;
		private TextBox textDateCreated;
		private TextBox textDateSent;
		private Label label7;
		private TextBox textDateRecd;
		private Label label8;
		private TextBox textDateChecked;
		private Label label9;
		private TextBox textDateDue;
		private Label label10;
		private ComboBox comboProv;
		private Label label11;
		private ListBox listLab;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butCheckedNow;
		private OpenDental.UI.Button butRecdNow;
		private OpenDental.UI.Button butSentNow;
		private OpenDental.UI.Button butCreatedNow;
		private ListBox listTurnaround;
		private Label label12;
		private List<Laboratory> ListLabs;
		private TextBox textWeekday;
		private List<LabTurnaround> turnaroundList;

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
			this.textInstructions = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textAppointment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPlanned = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textDateCreated = new System.Windows.Forms.TextBox();
			this.textDateSent = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textDateRecd = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textDateChecked = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textDateDue = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.listLab = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listTurnaround = new System.Windows.Forms.ListBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textWeekday = new System.Windows.Forms.TextBox();
			this.butCheckedNow = new OpenDental.UI.Button();
			this.butRecdNow = new OpenDental.UI.Button();
			this.butSentNow = new OpenDental.UI.Button();
			this.butCreatedNow = new OpenDental.UI.Button();
			this.butDetachPlanned = new OpenDental.UI.Button();
			this.butDetach = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Patient";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(94,15);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(319,20);
			this.textPatient.TabIndex = 0;
			// 
			// textInstructions
			// 
			this.textInstructions.Location = new System.Drawing.Point(94,285);
			this.textInstructions.MaxLength = 10000;
			this.textInstructions.Multiline = true;
			this.textInstructions.Name = "textInstructions";
			this.textInstructions.Size = new System.Drawing.Size(400,127);
			this.textInstructions.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5,287);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89,17);
			this.label3.TabIndex = 101;
			this.label3.Text = "Instructions";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(91,116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89,17);
			this.label2.TabIndex = 99;
			this.label2.Text = "Lab";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textAppointment
			// 
			this.textAppointment.Location = new System.Drawing.Point(94,40);
			this.textAppointment.Name = "textAppointment";
			this.textAppointment.ReadOnly = true;
			this.textAppointment.Size = new System.Drawing.Size(319,20);
			this.textAppointment.TabIndex = 103;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,41);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(89,17);
			this.label4.TabIndex = 104;
			this.label4.Text = "Appointment";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPlanned
			// 
			this.textPlanned.Location = new System.Drawing.Point(94,65);
			this.textPlanned.Name = "textPlanned";
			this.textPlanned.ReadOnly = true;
			this.textPlanned.Size = new System.Drawing.Size(319,20);
			this.textPlanned.TabIndex = 106;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5,66);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(89,17);
			this.label5.TabIndex = 107;
			this.label5.Text = "Planned Appt";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(17,20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81,17);
			this.label6.TabIndex = 110;
			this.label6.Text = "Created";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateCreated
			// 
			this.textDateCreated.Location = new System.Drawing.Point(100,19);
			this.textDateCreated.Name = "textDateCreated";
			this.textDateCreated.Size = new System.Drawing.Size(147,20);
			this.textDateCreated.TabIndex = 111;
			// 
			// textDateSent
			// 
			this.textDateSent.Location = new System.Drawing.Point(100,44);
			this.textDateSent.Name = "textDateSent";
			this.textDateSent.Size = new System.Drawing.Size(147,20);
			this.textDateSent.TabIndex = 113;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(17,45);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(81,17);
			this.label7.TabIndex = 112;
			this.label7.Text = "Sent";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateRecd
			// 
			this.textDateRecd.Location = new System.Drawing.Point(100,69);
			this.textDateRecd.Name = "textDateRecd";
			this.textDateRecd.Size = new System.Drawing.Size(147,20);
			this.textDateRecd.TabIndex = 115;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(17,70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(81,17);
			this.label8.TabIndex = 114;
			this.label8.Text = "Received";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateChecked
			// 
			this.textDateChecked.Location = new System.Drawing.Point(100,94);
			this.textDateChecked.Name = "textDateChecked";
			this.textDateChecked.Size = new System.Drawing.Size(147,20);
			this.textDateChecked.TabIndex = 117;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(2,95);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(96,17);
			this.label9.TabIndex = 116;
			this.label9.Text = "Quality Checked";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateDue
			// 
			this.textDateDue.Location = new System.Drawing.Point(336,261);
			this.textDateDue.Name = "textDateDue";
			this.textDateDue.Size = new System.Drawing.Size(158,20);
			this.textDateDue.TabIndex = 119;
			this.textDateDue.TextChanged += new System.EventHandler(this.textDateDue_TextChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(207,262);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(89,17);
			this.label10.TabIndex = 118;
			this.label10.Text = "Date Time Due";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.FormattingEnabled = true;
			this.comboProv.Location = new System.Drawing.Point(94,90);
			this.comboProv.MaxDropDownItems = 25;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(158,21);
			this.comboProv.TabIndex = 121;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(5,93);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(89,17);
			this.label11.TabIndex = 120;
			this.label11.Text = "Provider";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listLab
			// 
			this.listLab.FormattingEnabled = true;
			this.listLab.Location = new System.Drawing.Point(94,136);
			this.listLab.Name = "listLab";
			this.listLab.Size = new System.Drawing.Size(198,121);
			this.listLab.TabIndex = 0;
			this.listLab.SelectedIndexChanged += new System.EventHandler(this.listLab_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butCheckedNow);
			this.groupBox1.Controls.Add(this.butRecdNow);
			this.groupBox1.Controls.Add(this.butSentNow);
			this.groupBox1.Controls.Add(this.butCreatedNow);
			this.groupBox1.Controls.Add(this.textDateCreated);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textDateSent);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textDateRecd);
			this.groupBox1.Controls.Add(this.textDateChecked);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Location = new System.Drawing.Point(523,10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(329,123);
			this.groupBox1.TabIndex = 122;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tracking";
			// 
			// listTurnaround
			// 
			this.listTurnaround.FormattingEnabled = true;
			this.listTurnaround.Location = new System.Drawing.Point(296,136);
			this.listTurnaround.Name = "listTurnaround";
			this.listTurnaround.Size = new System.Drawing.Size(198,121);
			this.listTurnaround.TabIndex = 124;
			this.listTurnaround.Click += new System.EventHandler(this.listTurnaround_Click);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(293,116);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(89,17);
			this.label12.TabIndex = 125;
			this.label12.Text = "Set Due Date";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textWeekday
			// 
			this.textWeekday.BackColor = System.Drawing.Color.White;
			this.textWeekday.Location = new System.Drawing.Point(296,261);
			this.textWeekday.Name = "textWeekday";
			this.textWeekday.ReadOnly = true;
			this.textWeekday.Size = new System.Drawing.Size(40,20);
			this.textWeekday.TabIndex = 126;
			// 
			// butCheckedNow
			// 
			this.butCheckedNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCheckedNow.Autosize = true;
			this.butCheckedNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheckedNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheckedNow.CornerRadius = 4F;
			this.butCheckedNow.Location = new System.Drawing.Point(248,92);
			this.butCheckedNow.Name = "butCheckedNow";
			this.butCheckedNow.Size = new System.Drawing.Size(75,24);
			this.butCheckedNow.TabIndex = 121;
			this.butCheckedNow.Text = "Now";
			this.butCheckedNow.Click += new System.EventHandler(this.butCheckedNow_Click);
			// 
			// butRecdNow
			// 
			this.butRecdNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRecdNow.Autosize = true;
			this.butRecdNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecdNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecdNow.CornerRadius = 4F;
			this.butRecdNow.Location = new System.Drawing.Point(248,67);
			this.butRecdNow.Name = "butRecdNow";
			this.butRecdNow.Size = new System.Drawing.Size(75,24);
			this.butRecdNow.TabIndex = 120;
			this.butRecdNow.Text = "Now";
			this.butRecdNow.Click += new System.EventHandler(this.butRecdNow_Click);
			// 
			// butSentNow
			// 
			this.butSentNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSentNow.Autosize = true;
			this.butSentNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSentNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSentNow.CornerRadius = 4F;
			this.butSentNow.Location = new System.Drawing.Point(248,42);
			this.butSentNow.Name = "butSentNow";
			this.butSentNow.Size = new System.Drawing.Size(75,24);
			this.butSentNow.TabIndex = 119;
			this.butSentNow.Text = "Now";
			this.butSentNow.Click += new System.EventHandler(this.butSentNow_Click);
			// 
			// butCreatedNow
			// 
			this.butCreatedNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreatedNow.Autosize = true;
			this.butCreatedNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreatedNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreatedNow.CornerRadius = 4F;
			this.butCreatedNow.Location = new System.Drawing.Point(248,17);
			this.butCreatedNow.Name = "butCreatedNow";
			this.butCreatedNow.Size = new System.Drawing.Size(75,24);
			this.butCreatedNow.TabIndex = 118;
			this.butCreatedNow.Text = "Now";
			this.butCreatedNow.Click += new System.EventHandler(this.butCreatedNow_Click);
			// 
			// butDetachPlanned
			// 
			this.butDetachPlanned.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetachPlanned.Autosize = true;
			this.butDetachPlanned.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetachPlanned.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetachPlanned.CornerRadius = 4F;
			this.butDetachPlanned.Location = new System.Drawing.Point(419,62);
			this.butDetachPlanned.Name = "butDetachPlanned";
			this.butDetachPlanned.Size = new System.Drawing.Size(75,24);
			this.butDetachPlanned.TabIndex = 108;
			this.butDetachPlanned.Text = "Detach";
			this.butDetachPlanned.Click += new System.EventHandler(this.butDetachPlanned_Click);
			// 
			// butDetach
			// 
			this.butDetach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetach.Autosize = true;
			this.butDetach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetach.CornerRadius = 4F;
			this.butDetach.Location = new System.Drawing.Point(419,37);
			this.butDetach.Name = "butDetach";
			this.butDetach.Size = new System.Drawing.Size(75,24);
			this.butDetach.TabIndex = 105;
			this.butDetach.Text = "Detach";
			this.butDetach.Click += new System.EventHandler(this.butDetach_Click);
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
			this.butDelete.Location = new System.Drawing.Point(27,443);
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
			this.butOK.Location = new System.Drawing.Point(686,443);
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
			this.butCancel.Location = new System.Drawing.Point(777,443);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormLabCaseEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(878,487);
			this.Controls.Add(this.textWeekday);
			this.Controls.Add(this.listTurnaround);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listLab);
			this.Controls.Add(this.comboProv);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textDateDue);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.butDetachPlanned);
			this.Controls.Add(this.textPlanned);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butDetach);
			this.Controls.Add(this.textAppointment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textInstructions);
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
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormLabCaseEdit_Load(object sender, System.EventArgs e) {
			textPatient.Text=Patients.GetPat(CaseCur.PatNum).GetNameFL();
			ListLabs=Laboratories.Refresh();
			for(int i=0;i<ListLabs.Count;i++){
				listLab.Items.Add(ListLabs[i].Description+" "+ListLabs[i].Phone);
				if(ListLabs[i].LaboratoryNum==CaseCur.LaboratoryNum){
					listLab.SelectedIndex=i;
				}
			}
			for(int i=0;i<Providers.List.Length;i++){
				comboProv.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==CaseCur.ProvNum){
					comboProv.SelectedIndex=i;
				}
			}
			Appointment apt=Appointments.GetOneApt(CaseCur.AptNum);
			if(apt!=null){
				if(apt.AptStatus==ApptStatus.UnschedList){
					textAppointment.Text=Lan.g(this,"Unscheduled");
				}
				else{
					textAppointment.Text=apt.AptDateTime.ToShortDateString()+" "+apt.AptDateTime.ToShortTimeString();
				}
				textAppointment.Text+=", "+apt.ProcDescript;
			}
			apt=Appointments.GetOneApt(CaseCur.PlannedAptNum);
			if(apt!=null){
				textPlanned.Text=apt.ProcDescript;
				if(textPlanned.Text==""){
					textPlanned.Text=Lan.g(this,"Attached");
				}
			}
			if(CaseCur.DateTimeCreated.Year>1880){
				textDateCreated.Text=CaseCur.DateTimeCreated.ToString();
			}
			if(CaseCur.DateTimeSent.Year>1880) {
				textDateSent.Text=CaseCur.DateTimeSent.ToString();
			}
			if(CaseCur.DateTimeRecd.Year>1880) {
				textDateRecd.Text=CaseCur.DateTimeRecd.ToString();
			}
			if(CaseCur.DateTimeChecked.Year>1880) {
				textDateChecked.Text=CaseCur.DateTimeChecked.ToString();
			}
			if(CaseCur.DateTimeDue.Year>1880) {
				textDateDue.Text=CaseCur.DateTimeDue.ToShortDateString()+" "+CaseCur.DateTimeDue.ToShortTimeString();
			}
			textInstructions.Text=CaseCur.Instructions;
		}

		private void textDateDue_TextChanged(object sender,EventArgs e) {
			try{
				DateTime date=DateTime.Parse(textDateDue.Text);
				textWeekday.Text=date.ToString("ddd");
			}
			catch{
				textWeekday.Text="";
			}
		}

		private void butDetach_Click(object sender,EventArgs e) {
			CaseCur.AptNum=0;
			textAppointment.Text="";
		}

		private void butDetachPlanned_Click(object sender,EventArgs e) {
			CaseCur.PlannedAptNum=0;
			textPlanned.Text="";
		}

		private void listLab_SelectedIndexChanged(object sender,EventArgs e) {
			if(listLab.SelectedIndex==-1){
				return;
			}
			turnaroundList=LabTurnarounds.GetForLab(ListLabs[listLab.SelectedIndex].LaboratoryNum);
			listTurnaround.Items.Clear();
			for(int i=0;i<turnaroundList.Count;i++){
				listTurnaround.Items.Add(turnaroundList[i].Description+", "+turnaroundList[i].DaysActual.ToString());
			}
		}

		private void listTurnaround_Click(object sender,EventArgs e) {
			if(listTurnaround.SelectedIndex==-1){
				return;
			}
			DateTime duedate=LabTurnarounds.ComputeDueDate
				(MiscData.GetNowDateTime().Date,turnaroundList[listTurnaround.SelectedIndex].DaysActual);
			textDateDue.Text=duedate.ToShortDateString()+" "+duedate.ToShortTimeString();
			listTurnaround.SelectedIndex=-1;
		}

		private void butCreatedNow_Click(object sender,EventArgs e) {
			textDateCreated.Text=MiscData.GetNowDateTime().ToString();
		}

		private void butSentNow_Click(object sender,EventArgs e) {
			textDateSent.Text=MiscData.GetNowDateTime().ToString();
		}

		private void butRecdNow_Click(object sender,EventArgs e) {
			textDateRecd.Text=MiscData.GetNowDateTime().ToString();
		}

		private void butCheckedNow_Click(object sender,EventArgs e) {
			textDateChecked.Text=MiscData.GetNowDateTime().ToString();
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
			if(listLab.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a lab first.");
				return;
			}
			if(comboProv.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			if(textDateCreated.Text!=""){
				try{
					DateTime.Parse(textDateCreated.Text);
				}
				catch{
					MsgBox.Show(this,"Date Time Created is invalid.");
					return;
				}
			}
			if(textDateSent.Text!="") {
				try {
					DateTime.Parse(textDateSent.Text);
				}
				catch {
					MsgBox.Show(this,"Date Time Sent is invalid.");
					return;
				}
			}
			if(textDateRecd.Text!="") {
				try {
					DateTime.Parse(textDateRecd.Text);
				}
				catch {
					MsgBox.Show(this,"Date Time Received is invalid.");
					return;
				}
			}
			if(textDateChecked.Text!="") {
				try {
					DateTime.Parse(textDateChecked.Text);
				}
				catch {
					MsgBox.Show(this,"Date Time Checked is invalid.");
					return;
				}
			}
			if(textDateDue.Text!="") {
				try {
					DateTime.Parse(textDateDue.Text);
				}
				catch {
					MsgBox.Show(this,"Date Time Due is invalid.");
					return;
				}
			}
			CaseCur.LaboratoryNum=ListLabs[listLab.SelectedIndex].LaboratoryNum;
			//AptNum
			//PlannedAptNum
			CaseCur.ProvNum=Providers.List[comboProv.SelectedIndex].ProvNum;
			if(textDateCreated.Text==""){
				CaseCur.DateTimeCreated=DateTime.MinValue;
			}
			else{
				CaseCur.DateTimeCreated=DateTime.Parse(textDateCreated.Text);
			}
			if(textDateSent.Text==""){
				CaseCur.DateTimeSent=DateTime.MinValue;
			}
			else{
				CaseCur.DateTimeSent=DateTime.Parse(textDateSent.Text);
			}
			if(textDateRecd.Text==""){
				CaseCur.DateTimeRecd=DateTime.MinValue;
			}
			else{
				CaseCur.DateTimeRecd=DateTime.Parse(textDateRecd.Text);
			}
			if(textDateChecked.Text==""){
				CaseCur.DateTimeChecked=DateTime.MinValue;
			}
			else{
				CaseCur.DateTimeChecked=DateTime.Parse(textDateChecked.Text);
			}
			if(textDateDue.Text==""){
				CaseCur.DateTimeDue=DateTime.MinValue;
			}
			else{
				CaseCur.DateTimeDue=DateTime.Parse(textDateDue.Text);
			}
			CaseCur.Instructions=textInstructions.Text;
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





















