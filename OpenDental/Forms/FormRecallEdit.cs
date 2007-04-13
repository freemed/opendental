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
	public class FormRecallEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidNum textYears;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboStatus;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textDatePrevious;
		private OpenDental.ValidNum textMonths;
		private OpenDental.ValidNum textDays;
		private OpenDental.ValidNum textWeeks;
		private System.Windows.Forms.CheckBox checkIsDisabled;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		///<summary>The recall object to edit.</summary>
		public Recall RecallCur;
		private System.Windows.Forms.TextBox textDateDueCalc;
		private OpenDental.ValidDate textDateDue;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.ODtextBox textNote;
		private Patient PatCur;

		///<summary>Don't forget to set the RecallCur before opening this form.</summary>
		public FormRecallEdit(Patient patCur){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			PatCur=patCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecallEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.textDatePrevious = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDateDueCalc = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textDateDue = new OpenDental.ValidDate();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textWeeks = new OpenDental.ValidNum();
			this.label7 = new System.Windows.Forms.Label();
			this.textDays = new OpenDental.ValidNum();
			this.label6 = new System.Windows.Forms.Label();
			this.textMonths = new OpenDental.ValidNum();
			this.label5 = new System.Windows.Forms.Label();
			this.textYears = new OpenDental.ValidNum();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.checkIsDisabled = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textNote = new OpenDental.ODtextBox();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(589,405);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDatePrevious
			// 
			this.textDatePrevious.Location = new System.Drawing.Point(184,42);
			this.textDatePrevious.Name = "textDatePrevious";
			this.textDatePrevious.ReadOnly = true;
			this.textDatePrevious.Size = new System.Drawing.Size(85,20);
			this.textDatePrevious.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170,19);
			this.label1.TabIndex = 2;
			this.label1.Text = "Previous Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(11,67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(170,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Calculated Due Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateDueCalc
			// 
			this.textDateDueCalc.Location = new System.Drawing.Point(184,68);
			this.textDateDueCalc.Name = "textDateDueCalc";
			this.textDateDueCalc.ReadOnly = true;
			this.textDateDueCalc.Size = new System.Drawing.Size(85,20);
			this.textDateDueCalc.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,94);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(170,19);
			this.label3.TabIndex = 5;
			this.label3.Text = "Actual Due Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateDue
			// 
			this.textDateDue.Location = new System.Drawing.Point(184,94);
			this.textDateDue.Name = "textDateDue";
			this.textDateDue.Size = new System.Drawing.Size(86,20);
			this.textDateDue.TabIndex = 6;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textWeeks);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textDays);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textMonths);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textYears);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(79,123);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(170,115);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Recall Interval";
			// 
			// textWeeks
			// 
			this.textWeeks.Location = new System.Drawing.Point(105,63);
			this.textWeeks.MaxVal = 255;
			this.textWeeks.MinVal = 0;
			this.textWeeks.Name = "textWeeks";
			this.textWeeks.Size = new System.Drawing.Size(51,20);
			this.textWeeks.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(11,63);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(92,19);
			this.label7.TabIndex = 11;
			this.label7.Text = "Weeks";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDays
			// 
			this.textDays.Location = new System.Drawing.Point(105,85);
			this.textDays.MaxVal = 255;
			this.textDays.MinVal = 0;
			this.textDays.Name = "textDays";
			this.textDays.Size = new System.Drawing.Size(51,20);
			this.textDays.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(11,85);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(92,19);
			this.label6.TabIndex = 9;
			this.label6.Text = "Days";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMonths
			// 
			this.textMonths.Location = new System.Drawing.Point(105,40);
			this.textMonths.MaxVal = 255;
			this.textMonths.MinVal = 0;
			this.textMonths.Name = "textMonths";
			this.textMonths.Size = new System.Drawing.Size(51,20);
			this.textMonths.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(11,40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92,19);
			this.label5.TabIndex = 7;
			this.label5.Text = "Months";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textYears
			// 
			this.textYears.Location = new System.Drawing.Point(105,17);
			this.textYears.MaxVal = 127;
			this.textYears.MinVal = 0;
			this.textYears.Name = "textYears";
			this.textYears.Size = new System.Drawing.Size(51,20);
			this.textYears.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11,17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92,19);
			this.label4.TabIndex = 5;
			this.label4.Text = "Years";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12,243);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(170,19);
			this.label8.TabIndex = 8;
			this.label8.Text = "Status";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(184,243);
			this.comboStatus.MaxDropDownItems = 50;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(188,21);
			this.comboStatus.TabIndex = 9;
			// 
			// checkIsDisabled
			// 
			this.checkIsDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsDisabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsDisabled.Location = new System.Drawing.Point(29,17);
			this.checkIsDisabled.Name = "checkIsDisabled";
			this.checkIsDisabled.Size = new System.Drawing.Size(169,18);
			this.checkIsDisabled.TabIndex = 10;
			this.checkIsDisabled.Text = "Disabled";
			this.checkIsDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsDisabled.Click += new System.EventHandler(this.checkIsDisabled_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(65,269);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(117,92);
			this.label9.TabIndex = 11;
			this.label9.Text = "Administrative Note (this note will get deleted every time recall gets reset)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(589,363);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 13;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butDelete.Location = new System.Drawing.Point(31,406);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,26);
			this.butDelete.TabIndex = 14;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(184,271);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Recall;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(350,112);
			this.textNote.TabIndex = 15;
			// 
			// FormRecallEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(685,454);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDateDue);
			this.Controls.Add(this.textDateDueCalc);
			this.Controls.Add(this.textDatePrevious);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.checkIsDisabled);
			this.Controls.Add(this.comboStatus);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Recall";
			this.Load += new System.EventHandler(this.FormRecallEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRecallEdit_Load(object sender, System.EventArgs e) {
			checkIsDisabled.Checked=RecallCur.IsDisabled;
			if(checkIsDisabled.Checked){
				textDateDue.ReadOnly=true;
			}
			if(RecallCur.DatePrevious.Year>1880){
				textDatePrevious.Text=RecallCur.DatePrevious.ToShortDateString();
			}
			if(RecallCur.DateDueCalc.Year>1880){
				textDateDueCalc.Text=RecallCur.DateDueCalc.ToShortDateString();
			}
			if(RecallCur.DateDue.Year>1880){
				textDateDue.Text=RecallCur.DateDue.ToShortDateString();
			}
			textYears.Text=RecallCur.RecallInterval.Years.ToString();
			textMonths.Text=RecallCur.RecallInterval.Months.ToString();
			textWeeks.Text=RecallCur.RecallInterval.Weeks.ToString();
			textDays.Text=RecallCur.RecallInterval.Days.ToString();
			comboStatus.Items.Add(Lan.g(this,"None"));
			comboStatus.SelectedIndex=0;
			for(int i=0;i<DefB.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboStatus.Items.Add(DefB.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(DefB.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==RecallCur.RecallStatus)
					comboStatus.SelectedIndex=i+1;
			}
			textNote.Text=RecallCur.Note;
		}

		private void checkIsDisabled_Click(object sender, System.EventArgs e) {
			if(checkIsDisabled.Checked){
				textDateDue.Text="";
				textDateDue.ReadOnly=true;
			}
			else{
				textDateDue.Text=textDateDueCalc.Text;
				textDateDue.ReadOnly=false;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(RecallCur.DatePrevious.Year<1880){//Can only be deleted if DatePrevious is blank.
				if(IsNew){
					DialogResult=DialogResult.Cancel;
				}
				else{
					if(!MsgBox.Show(this,true,"Delete this recall?")){
						return;
					}
					Recalls.Delete(RecallCur);
					DialogResult=DialogResult.OK;
				}
			}
			else{
				MsgBox.Show(this,"This recall can not be deleted because Previous Date has a value.  You should use the Disabled checkBox instead.");
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDateDue.errorProvider1.GetError(textDateDue)!=""
				|| textYears.errorProvider1.GetError(textYears)!=""
				|| textMonths.errorProvider1.GetError(textMonths)!=""
				|| textWeeks.errorProvider1.GetError(textWeeks)!=""
				|| textDays.errorProvider1.GetError(textDays)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			RecallCur.IsDisabled=checkIsDisabled.Checked;
			RecallCur.DateDue=PIn.PDate(textDateDue.Text);
			RecallCur.RecallInterval.Years=PIn.PInt(textYears.Text);
			RecallCur.RecallInterval.Months=PIn.PInt(textMonths.Text);
			RecallCur.RecallInterval.Weeks=PIn.PInt(textWeeks.Text);
			RecallCur.RecallInterval.Days=PIn.PInt(textDays.Text);
      if(comboStatus.SelectedIndex==0){
				RecallCur.RecallStatus=0;
			}
			else{
				RecallCur.RecallStatus
					=DefB.Short[(int)DefCat.RecallUnschedStatus][comboStatus.SelectedIndex-1].DefNum;
			}
			RecallCur.Note=textNote.Text;
			if(IsNew){
				if(!Recalls.IsAllDefault(RecallCur)){//only save if something meaningful
					Recalls.Insert(RecallCur);
				}
			}
			else{
				if(Recalls.IsAllDefault(RecallCur)){
					Recalls.Delete(RecallCur);
					DialogResult=DialogResult.OK;
					return;
				}
				else{
					Recalls.Update(RecallCur);
				}
			}
			Recalls.Synch(PatCur.PatNum,RecallCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















