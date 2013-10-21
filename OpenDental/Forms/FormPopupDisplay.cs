using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>This control should not be resized, except by the user. Dentists can enter sensitive information in the area below what is normally shown.</summary>
	public class FormPopupDisplay:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private TextBox textDescription;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butMinutes;
		private OpenDental.UI.Button butHours;
		private ComboBox comboMinutes;
		private ComboBox comboHours;
		private Label label3;
		private Label label2;
		private Label label1;
		private OpenDental.UI.Button butPerm;
		public Popup PopupCur;
		private Label label4;
		private TextBox textUser;
		private Label label5;
		private TextBox textCreateDate;
		private TextBox textEditDate;
		private Label label6;
		///<summary>Will be zero unless user successfully clicked a disable time interval.  Accepted range is 1 to 1440 (24hrs)</summary>
		public int MinutesDisabled;

		///<summary></summary>
		public FormPopupDisplay()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopupDisplay));
			this.textDescription = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboMinutes = new System.Windows.Forms.ComboBox();
			this.comboHours = new System.Windows.Forms.ComboBox();
			this.butPerm = new OpenDental.UI.Button();
			this.butMinutes = new OpenDental.UI.Button();
			this.butHours = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textCreateDate = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.textEditDate = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(53, 98);
			this.textDescription.Multiline = true;
			this.textDescription.Name = "textDescription";
			this.textDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textDescription.Size = new System.Drawing.Size(271, 91);
			this.textDescription.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.comboMinutes);
			this.groupBox1.Controls.Add(this.comboHours);
			this.groupBox1.Controls.Add(this.butPerm);
			this.groupBox1.Controls.Add(this.butMinutes);
			this.groupBox1.Controls.Add(this.butHours);
			this.groupBox1.Location = new System.Drawing.Point(350, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(271, 103);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Disable for";
			this.groupBox1.Visible = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(171, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 18);
			this.label3.TabIndex = 30;
			this.label3.Text = "This workstation";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(171, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 18);
			this.label2.TabIndex = 29;
			this.label2.Text = "This workstation";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(171, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "All workstations";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboMinutes
			// 
			this.comboMinutes.FormattingEnabled = true;
			this.comboMinutes.Location = new System.Drawing.Point(15, 21);
			this.comboMinutes.MaxDropDownItems = 48;
			this.comboMinutes.Name = "comboMinutes";
			this.comboMinutes.Size = new System.Drawing.Size(70, 21);
			this.comboMinutes.TabIndex = 27;
			// 
			// comboHours
			// 
			this.comboHours.FormattingEnabled = true;
			this.comboHours.Location = new System.Drawing.Point(15, 48);
			this.comboHours.MaxDropDownItems = 48;
			this.comboHours.Name = "comboHours";
			this.comboHours.Size = new System.Drawing.Size(70, 21);
			this.comboHours.TabIndex = 26;
			// 
			// butPerm
			// 
			this.butPerm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPerm.Autosize = true;
			this.butPerm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPerm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPerm.CornerRadius = 4F;
			this.butPerm.Location = new System.Drawing.Point(91, 73);
			this.butPerm.Name = "butPerm";
			this.butPerm.Size = new System.Drawing.Size(77, 24);
			this.butPerm.TabIndex = 28;
			this.butPerm.Text = "Permanently";
			// 
			// butMinutes
			// 
			this.butMinutes.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMinutes.Autosize = true;
			this.butMinutes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMinutes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMinutes.CornerRadius = 4F;
			this.butMinutes.Location = new System.Drawing.Point(91, 19);
			this.butMinutes.Name = "butMinutes";
			this.butMinutes.Size = new System.Drawing.Size(77, 24);
			this.butMinutes.TabIndex = 4;
			this.butMinutes.Text = "Minutes";
			// 
			// butHours
			// 
			this.butHours.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHours.Autosize = true;
			this.butHours.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHours.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHours.CornerRadius = 4F;
			this.butHours.Location = new System.Drawing.Point(91, 46);
			this.butHours.Name = "butHours";
			this.butHours.Size = new System.Drawing.Size(77, 24);
			this.butHours.TabIndex = 3;
			this.butHours.Text = "Hours";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "User";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUser
			// 
			this.textUser.Enabled = false;
			this.textUser.Location = new System.Drawing.Point(79, 22);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(147, 20);
			this.textUser.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 49);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 15);
			this.label5.TabIndex = 11;
			this.label5.Text = "Date Created";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCreateDate
			// 
			this.textCreateDate.Enabled = false;
			this.textCreateDate.Location = new System.Drawing.Point(79, 47);
			this.textCreateDate.Name = "textCreateDate";
			this.textCreateDate.ReadOnly = true;
			this.textCreateDate.Size = new System.Drawing.Size(147, 20);
			this.textCreateDate.TabIndex = 12;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(323, 195);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textEditDate
			// 
			this.textEditDate.Enabled = false;
			this.textEditDate.Location = new System.Drawing.Point(79, 73);
			this.textEditDate.Name = "textEditDate";
			this.textEditDate.ReadOnly = true;
			this.textEditDate.Size = new System.Drawing.Size(147, 20);
			this.textEditDate.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 15);
			this.label6.TabIndex = 14;
			this.label6.Text = "Last Edited";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormPopupDisplay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(425, 240);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textEditDate);
			this.Controls.Add(this.textCreateDate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPopupDisplay";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Popup";
			this.Load += new System.EventHandler(this.FormPopupDisplay_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPopupDisplay_Load(object sender,EventArgs e) {
			textDescription.Text=PopupCur.Description;
			if(PopupCur.UserNum!=0) {
				textUser.Text=Userods.GetUser(PopupCur.UserNum).UserName;
			}
			textCreateDate.Text="";
			if(PopupCur.DateTimeEntry.Year>1880) {
				textCreateDate.Text=PopupCur.DateTimeEntry.ToShortDateString()+" "+PopupCur.DateTimeEntry.ToShortTimeString();
			}
			DateTime dateT=Popups.GetLastEditDateTimeForPopup(PopupCur.PopupNum);
			textEditDate.Text="";
			if(dateT.Year>1880) {
				textEditDate.Text=dateT.ToShortDateString()+" "+dateT.ToShortTimeString();//Sets the Edit date to the entry date of the last popup change that was archived for this popup.
			}
			for(int i=1;i<=4;i++) {
				comboMinutes.Items.Add(i.ToString());
			}
			for(int i=1;i<=11;i++) {
				comboMinutes.Items.Add((i*5).ToString());
			}
			comboMinutes.Text="10";
			for(int i=1;i<=12;i++) {
				comboHours.Items.Add(i.ToString());
			}
			comboHours.Text="1";
			MinutesDisabled=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(PopupCur.Description!=textDescription.Text) {//if user changed the note
				if(MsgBox.Show(this,true,"Save changes to note?")) {
					Popup popupArchive=PopupCur.Copy();
					popupArchive.IsArchived=true;
					popupArchive.PopupNumArchive=PopupCur.PopupNum;
					Popups.Insert(popupArchive);
					PopupCur.Description=textDescription.Text;
					PopupCur.DateTimeEntry=DateTime.Now;
					PopupCur.UserNum=Security.CurUser.UserNum;
					Popups.Update(PopupCur);
				}
			}
			else {
				MinutesDisabled=10;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















