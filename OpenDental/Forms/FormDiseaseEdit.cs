using System;
using System.Drawing;
using System.Collections;
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
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.labelStatus = new System.Windows.Forms.Label();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(116,99);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(322,120);
			this.textNote.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14,100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14,13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,19);
			this.label2.TabIndex = 5;
			this.label2.Text = "Problem";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProblem
			// 
			this.textProblem.Location = new System.Drawing.Point(118,12);
			this.textProblem.Name = "textProblem";
			this.textProblem.ReadOnly = true;
			this.textProblem.Size = new System.Drawing.Size(199,20);
			this.textProblem.TabIndex = 7;
			// 
			// textIcd9
			// 
			this.textIcd9.Location = new System.Drawing.Point(118,38);
			this.textIcd9.Name = "textIcd9";
			this.textIcd9.ReadOnly = true;
			this.textIcd9.Size = new System.Drawing.Size(199,20);
			this.textIcd9.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(14,39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,19);
			this.label3.TabIndex = 8;
			this.label3.Text = "ICD9";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butDelete.Location = new System.Drawing.Point(12,255);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,26);
			this.butDelete.TabIndex = 6;
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
			this.butOK.Location = new System.Drawing.Point(323,255);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(404,255);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// labelStatus
			// 
			this.labelStatus.Location = new System.Drawing.Point(3,71);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(111,15);
			this.labelStatus.TabIndex = 82;
			this.labelStatus.Text = "Status";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.Cursor = System.Windows.Forms.Cursors.Default;
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(116,68);
			this.comboStatus.MaxDropDownItems = 10;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(126,21);
			this.comboStatus.TabIndex = 83;
			// 
			// FormDiseaseEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(491,293);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.textIcd9);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textProblem);
			this.Controls.Add(this.butDelete);
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
			// Fill problem/icd9 - only one will exist (with a value not zero).
			if(DiseaseCur.DiseaseDefNum!=0) {
				textProblem.Text=DiseaseDefs.GetItem(DiseaseCur.DiseaseDefNum).DiseaseName;
			}
			else {
				textIcd9.Text=ICD9s.GetOne(DiseaseCur.ICD9Num).Description;
			}
			comboStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(ProblemStatus)).Length;i++) {
				comboStatus.Items.Add(Enum.GetNames(typeof(ProblemStatus))[i]);
			}
			comboStatus.SelectedIndex=(int)DiseaseCur.ProbStatus;
			textNote.Text=DiseaseCur.PatNote;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			Diseases.Delete(DiseaseCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DiseaseCur.ProbStatus=(ProblemStatus)comboStatus.SelectedIndex;
			DiseaseCur.PatNote=textNote.Text;
			if(IsNew){
				Diseases.Insert(DiseaseCur);
			}
			else{
				Diseases.Update(DiseaseCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















