using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptLists : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butRecall;
		private OpenDental.UI.Button butConfirm;
		private OpenDental.UI.Button butPlanned;
		private OpenDental.UI.Button butUnsched;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>After this window closes, if dialog result is OK, this will contain which list was selected.</summary>
		public ApptListSelection SelectionResult;

		///<summary></summary>
		public FormApptLists()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptLists));
			this.butCancel = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butRecall = new OpenDental.UI.Button();
			this.butConfirm = new OpenDental.UI.Button();
			this.butPlanned = new OpenDental.UI.Button();
			this.butUnsched = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(561,364);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(130,83);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(458,34);
			this.label1.TabIndex = 2;
			this.label1.Text = "A list of due dates for patients who have previously been in for an exam or clean" +
    "ing";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(130,142);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(458,44);
			this.label2.TabIndex = 3;
			this.label2.Text = "A list of scheduled appointments.  These patients need to be reminded about their" +
    " appointments.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(130,204);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(458,47);
			this.label3.TabIndex = 4;
			this.label3.Text = "Planned appointments are created in the Chart module.  Every patient should have " +
    "one or be marked done.  This list is work that has been planned but never schedu" +
    "led.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(130,266);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(458,47);
			this.label5.TabIndex = 6;
			this.label5.Text = "A short list of appointments that need to be followed up on.   Keep this list sho" +
    "rt by deleting any that don\'t get scheduled quickly.  You would then track them " +
    " using the Planned Appointment Tracker";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(27,19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(519,31);
			this.label6.TabIndex = 7;
			this.label6.Text = "These lists may be used for calling patients, sending postcards, running reports," +
    " etc..  Make sure to make good Comm Log entries for everything.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butRecall
			// 
			this.butRecall.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRecall.Autosize = true;
			this.butRecall.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRecall.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRecall.CornerRadius = 4F;
			this.butRecall.Location = new System.Drawing.Point(28,78);
			this.butRecall.Name = "butRecall";
			this.butRecall.Size = new System.Drawing.Size(100,26);
			this.butRecall.TabIndex = 8;
			this.butRecall.Text = "Recall";
			this.butRecall.Click += new System.EventHandler(this.butRecall_Click);
			// 
			// butConfirm
			// 
			this.butConfirm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butConfirm.Autosize = true;
			this.butConfirm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butConfirm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butConfirm.CornerRadius = 4F;
			this.butConfirm.Location = new System.Drawing.Point(28,140);
			this.butConfirm.Name = "butConfirm";
			this.butConfirm.Size = new System.Drawing.Size(100,26);
			this.butConfirm.TabIndex = 9;
			this.butConfirm.Text = "Confirmations";
			this.butConfirm.Click += new System.EventHandler(this.butConfirm_Click);
			// 
			// butPlanned
			// 
			this.butPlanned.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPlanned.Autosize = true;
			this.butPlanned.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPlanned.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPlanned.CornerRadius = 4F;
			this.butPlanned.Location = new System.Drawing.Point(28,202);
			this.butPlanned.Name = "butPlanned";
			this.butPlanned.Size = new System.Drawing.Size(100,26);
			this.butPlanned.TabIndex = 10;
			this.butPlanned.Text = "Planned Tracker";
			this.butPlanned.Click += new System.EventHandler(this.butPlanned_Click);
			// 
			// butUnsched
			// 
			this.butUnsched.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUnsched.Autosize = true;
			this.butUnsched.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUnsched.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUnsched.CornerRadius = 4F;
			this.butUnsched.Location = new System.Drawing.Point(28,264);
			this.butUnsched.Name = "butUnsched";
			this.butUnsched.Size = new System.Drawing.Size(100,26);
			this.butUnsched.TabIndex = 11;
			this.butUnsched.Text = "Unscheduled";
			this.butUnsched.Click += new System.EventHandler(this.butUnsched_Click);
			// 
			// FormApptLists
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(666,415);
			this.Controls.Add(this.butUnsched);
			this.Controls.Add(this.butPlanned);
			this.Controls.Add(this.butConfirm);
			this.Controls.Add(this.butRecall);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptLists";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment Lists";
			this.Load += new System.EventHandler(this.FormApptLists_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptLists_Load(object sender, System.EventArgs e) {
			
		}

		private void butRecall_Click(object sender, System.EventArgs e) {
			SelectionResult=ApptListSelection.Recall;
			DialogResult=DialogResult.OK;
		}

		private void butConfirm_Click(object sender, System.EventArgs e) {
			SelectionResult=ApptListSelection.Confirm;
			DialogResult=DialogResult.OK;
		}

		private void butPlanned_Click(object sender, System.EventArgs e) {
			SelectionResult=ApptListSelection.Planned;
			DialogResult=DialogResult.OK;
		}

		private void butUnsched_Click(object sender, System.EventArgs e) {
			SelectionResult=ApptListSelection.Unsched;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}

	///<summary>Used in FormApptLists as the selection result.</summary>
	public enum ApptListSelection{
		///<summary></summary>
		Recall,
		///<summary></summary>
		Confirm,
		///<summary></summary>
		Planned,
		///<summary></summary>
		Unsched
	}

}





















