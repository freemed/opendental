using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormDiseaseEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Disease DiseaseCur;
		private TextBox textNote;
		private Label label1;
		private Label label2;
		private OpenDental.UI.Button butDelete;
		private TextBox textProblem;
		private TextBox textIcd9;
		private Label label3;
		private Label labelStatus;
		private ComboBox comboStatus;
		private Label label4;
		private Label label5;
		private ValidDate textDateStart;
		private ValidDate textDateStop;
		private UI.Button butTodayStart;
		private UI.Button butTodayStop;
		private TextBox textSnomed;
		private Label label6;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormDiseaseEdit(Disease diseaseCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			DiseaseCur=diseaseCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiseaseEdit));
			this.textNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textProblem = new System.Windows.Forms.TextBox();
			this.textIcd9 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelStatus = new System.Windows.Forms.Label();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textDateStop = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butTodayStart = new OpenDental.UI.Button();
			this.butTodayStop = new OpenDental.UI.Button();
			this.textSnomed = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(118, 169);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(322, 120);
			this.textNote.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 169);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 19);
			this.label2.TabIndex = 5;
			this.label2.Text = "Problem";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProblem
			// 
			this.textProblem.Location = new System.Drawing.Point(118, 12);
			this.textProblem.Name = "textProblem";
			this.textProblem.ReadOnly = true;
			this.textProblem.Size = new System.Drawing.Size(199, 20);
			this.textProblem.TabIndex = 7;
			// 
			// textIcd9
			// 
			this.textIcd9.Location = new System.Drawing.Point(118, 38);
			this.textIcd9.Name = "textIcd9";
			this.textIcd9.ReadOnly = true;
			this.textIcd9.Size = new System.Drawing.Size(321, 20);
			this.textIcd9.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(14, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 19);
			this.label3.TabIndex = 8;
			this.label3.Text = "ICD9";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(3, 92);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(111, 15);
			this.labelStatus.TabIndex = 82;
			this.labelStatus.Text = "Status";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.Cursor = System.Windows.Forms.Cursors.Default;
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(118, 90);
			this.comboStatus.MaxDropDownItems = 10;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(126, 21);
			this.comboStatus.TabIndex = 83;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(14, 117);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 19);
			this.label4.TabIndex = 5;
			this.label4.Text = "Start Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(14, 143);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 19);
			this.label5.TabIndex = 5;
			this.label5.Text = "Stop Date";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(118, 143);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(126, 20);
			this.textDateStop.TabIndex = 84;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(118, 117);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(126, 20);
			this.textDateStart.TabIndex = 84;
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
			this.butDelete.Location = new System.Drawing.Point(12, 305);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83, 26);
			this.butDelete.TabIndex = 6;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(327, 305);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(408, 305);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butTodayStart
			// 
			this.butTodayStart.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTodayStart.Autosize = true;
			this.butTodayStart.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTodayStart.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTodayStart.CornerRadius = 4F;
			this.butTodayStart.Location = new System.Drawing.Point(263, 115);
			this.butTodayStart.Name = "butTodayStart";
			this.butTodayStart.Size = new System.Drawing.Size(65, 23);
			this.butTodayStart.TabIndex = 85;
			this.butTodayStart.Text = "Today";
			this.butTodayStart.Click += new System.EventHandler(this.butTodayStart_Click);
			// 
			// butTodayStop
			// 
			this.butTodayStop.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTodayStop.Autosize = true;
			this.butTodayStop.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTodayStop.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTodayStop.CornerRadius = 4F;
			this.butTodayStop.Location = new System.Drawing.Point(263, 141);
			this.butTodayStop.Name = "butTodayStop";
			this.butTodayStop.Size = new System.Drawing.Size(64, 23);
			this.butTodayStop.TabIndex = 86;
			this.butTodayStop.Text = "Today";
			this.butTodayStop.Click += new System.EventHandler(this.butTodayStop_Click);
			// 
			// textSnomed
			// 
			this.textSnomed.Location = new System.Drawing.Point(118, 64);
			this.textSnomed.Name = "textSnomed";
			this.textSnomed.ReadOnly = true;
			this.textSnomed.Size = new System.Drawing.Size(322, 20);
			this.textSnomed.TabIndex = 87;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(14, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 19);
			this.label6.TabIndex = 88;
			this.label6.Text = "Snomed";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormDiseaseEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(495, 343);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textSnomed);
			this.Controls.Add(this.butTodayStop);
			this.Controls.Add(this.butTodayStart);
			this.Controls.Add(this.textDateStop);
			this.Controls.Add(this.textDateStart);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.textIcd9);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textProblem);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDiseaseEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Problem";
			this.Load += new System.EventHandler(this.FormDiseaseEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDiseaseEdit_Load(object sender,EventArgs e) {
			DiseaseDef diseasedef=DiseaseDefs.GetItem(DiseaseCur.DiseaseDefNum);//guaranteed to have one
			textProblem.Text=diseasedef.DiseaseName;
			string i9descript=ICD9s.GetCodeAndDescription(diseasedef.ICD9Code);
			if(i9descript=="") {
				textIcd9.Text=diseasedef.ICD9Code;
			}
			else {
				textIcd9.Text=i9descript;
			}
			string sdescript=Snomeds.GetCodeAndDescription(diseasedef.SnomedCode);
			if(sdescript=="") {
				textSnomed.Text=diseasedef.SnomedCode;
			}
			else {
				textSnomed.Text=sdescript;
			}
			comboStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(ProblemStatus)).Length;i++) {
				comboStatus.Items.Add(Enum.GetNames(typeof(ProblemStatus))[i]);
			}
			comboStatus.SelectedIndex=(int)DiseaseCur.ProbStatus;
			textNote.Text=DiseaseCur.PatNote;
			if(DiseaseCur.DateStart.Year>1880) {
				textDateStart.Text=DiseaseCur.DateStart.ToShortDateString();
			}
			if(DiseaseCur.DateStop.Year>1880) {
				textDateStop.Text=DiseaseCur.DateStop.ToShortDateString();
			}
		}

		private void butTodayStart_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Now.ToShortDateString();
			DiseaseCur.DateStart=DateTime.Now;
		}

		private void butTodayStop_Click(object sender,EventArgs e) {
			textDateStop.Text=DateTime.Now.ToShortDateString();
			DiseaseCur.DateStop=DateTime.Now;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				//This code is never hit in current implementation 09/26/2013.
				DialogResult=DialogResult.Cancel;
				return;
			}
			List<Vitalsign> listVitals=Vitalsigns.GetListFromPregDiseaseNum(DiseaseCur.DiseaseNum);
			if(listVitals.Count>0) {//if attached to vital sign exam, block delete
				string dates="";
				for(int i=0;i<listVitals.Count;i++) {
					if(i>5) {
						break;
					}
					dates+="\r\n"+listVitals[i].DateTaken.ToShortDateString();
				}
				MsgBox.Show(this,"Not allowed to delete this problem.  It is attached to "+listVitals.Count.ToString()+"vital sign exams with dates including:"+dates+".");
				return;
			}
			else {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
					return;
				}
			}
			SecurityLogs.MakeLogEntry(Permissions.PatProblemListEdit,DiseaseCur.PatNum,DiseaseDefs.GetName(DiseaseCur.DiseaseDefNum)+" deleted");
			Diseases.Delete(DiseaseCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!="")
			{
				MsgBox.Show(this,"Please fix date.");
				return;
			}
			DiseaseCur.DateStart=PIn.Date(textDateStart.Text);
			DiseaseCur.DateStop=PIn.Date(textDateStop.Text);
			DiseaseCur.ProbStatus=(ProblemStatus)comboStatus.SelectedIndex;
			DiseaseCur.PatNote=textNote.Text;
			//Todo: Save DateStop and DateStart values.
			if(IsNew){
				//This code is never hit in current implementation 09/26/2013.
				Diseases.Insert(DiseaseCur);
				SecurityLogs.MakeLogEntry(Permissions.PatProblemListEdit,DiseaseCur.PatNum,DiseaseDefs.GetName(DiseaseCur.DiseaseDefNum)+" added");
			}
			else{
				//See if this problem is the pregnancy linked to a vitalsign exam
				List<Vitalsign> listVitalsAttached=Vitalsigns.GetListFromPregDiseaseNum(DiseaseCur.DiseaseNum);
				if(listVitalsAttached.Count>0) {
					//See if the vitalsign exam date is now outside of the active dates of the disease (pregnancy)
					string dates="";
					for(int i=listVitalsAttached.Count-1;i>-1;i--) {
						if(listVitalsAttached[i].DateTaken<DiseaseCur.DateStart || (DiseaseCur.DateStop.Year>1880 && listVitalsAttached[i].DateTaken>DiseaseCur.DateStop)) {
							dates="\r\n"+listVitalsAttached[i].DateTaken.ToShortDateString();
						}
						else {
							listVitalsAttached.Remove(listVitalsAttached[i]);
						}
					}
					//If vitalsign exams still in the list, ask if they want to remove this problem from the exam pregnancy dx
					if(listVitalsAttached.Count>0) {
						if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"This problem is attached to 1 or more vital sign exams as a pregnancy diagnosis with dates:"+dates+"\r\nThe date of the exams is now outside the active dates of this problem.  Do you want to remove this problem from those vital sign exams?")) {
							return;
						}
						for(int j=0;j<listVitalsAttached.Count;j++) {
							listVitalsAttached[j].PregDiseaseNum=0;
							Vitalsigns.Update(listVitalsAttached[j]);
						}
					}
				}
				Diseases.Update(DiseaseCur);
				SecurityLogs.MakeLogEntry(Permissions.PatProblemListEdit,DiseaseCur.PatNum,DiseaseDefs.GetName(DiseaseCur.DiseaseDefNum)+" edited");
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















