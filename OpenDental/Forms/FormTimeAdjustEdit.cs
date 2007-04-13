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
	public class FormTimeAdjustEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private Label label1;
		private Label label2;
		private TextBox textTimeEntry;
		private Label label4;
		private ValidDouble textHours;
		private TextBox textNote;
		private CheckBox checkOvertime;
		private OpenDental.UI.Button butDelete;
		private TimeAdjust TimeAdjustCur;

		///<summary></summary>
		public FormTimeAdjustEdit(TimeAdjust timeAdjustCur){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			TimeAdjustCur=timeAdjustCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeAdjustEdit));
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textTimeEntry = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.textHours = new OpenDental.ValidDouble();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkOvertime = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(37,269);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 16;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126,20);
			this.label1.TabIndex = 11;
			this.label1.Text = "Date/Time Entry";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(11,48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126,20);
			this.label2.TabIndex = 13;
			this.label2.Text = "Hours";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeEntry
			// 
			this.textTimeEntry.Location = new System.Drawing.Point(137,23);
			this.textTimeEntry.Name = "textTimeEntry";
			this.textTimeEntry.Size = new System.Drawing.Size(155,20);
			this.textTimeEntry.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(10,76);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(126,20);
			this.label4.TabIndex = 18;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(137,77);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(377,96);
			this.textNote.TabIndex = 21;
			// 
			// textHours
			// 
			this.textHours.Location = new System.Drawing.Point(137,49);
			this.textHours.Name = "textHours";
			this.textHours.Size = new System.Drawing.Size(66,20);
			this.textHours.TabIndex = 19;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(439,237);
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
			this.butCancel.Location = new System.Drawing.Point(439,269);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkOvertime
			// 
			this.checkOvertime.AutoSize = true;
			this.checkOvertime.Location = new System.Drawing.Point(212,51);
			this.checkOvertime.Name = "checkOvertime";
			this.checkOvertime.Size = new System.Drawing.Size(123,17);
			this.checkOvertime.TabIndex = 22;
			this.checkOvertime.Text = "Overtime Adjustment";
			this.checkOvertime.UseVisualStyleBackColor = true;
			// 
			// FormTimeAdjustEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(540,313);
			this.Controls.Add(this.checkOvertime);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textHours);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textTimeEntry);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTimeAdjustEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Time Adjustment";
			this.Load += new System.EventHandler(this.FormTimeAdjustEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTimeAdjustEdit_Load(object sender, System.EventArgs e) {
			textTimeEntry.Text=TimeAdjustCur.TimeEntry.ToString();
			if(TimeAdjustCur.OTimeHours.TotalHours==0){
				textHours.Text=TimeAdjustCur.RegHours.TotalHours.ToString("n");
			}
			else{
				checkOvertime.Checked=true;
				textHours.Text=TimeAdjustCur.OTimeHours.TotalHours.ToString("n");
			}
			textNote.Text=TimeAdjustCur.Note;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			TimeAdjusts.Delete(TimeAdjustCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			try {
				TimeAdjustCur.TimeEntry=DateTime.Parse(textTimeEntry.Text);
			}
			catch {
				MsgBox.Show(this,"Please enter a valid date and time.");
				return;
			}
			if( textHours.errorProvider1.GetError(textHours)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(checkOvertime.Checked){
				TimeAdjustCur.RegHours=TimeSpan.FromHours(-PIn.PDouble(textHours.Text));
				TimeAdjustCur.OTimeHours=TimeSpan.FromHours(PIn.PDouble(textHours.Text));
			}
			else{
				TimeAdjustCur.RegHours=TimeSpan.FromHours(PIn.PDouble(textHours.Text));
				TimeAdjustCur.OTimeHours=TimeSpan.FromHours(0);
			}
			TimeAdjustCur.Note=textNote.Text;
			if(IsNew){
				TimeAdjusts.Insert(TimeAdjustCur);
			}
			else{
				TimeAdjusts.Update(TimeAdjustCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	

		

		

		


	}
}





















