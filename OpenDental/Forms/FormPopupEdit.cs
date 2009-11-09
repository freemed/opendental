using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormPopupEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private TextBox textDescription;
		private Label label1;
		private CheckBox checkIsDisabled;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butDelete;
		private GroupBox groupBox1;
		private Label label3;
		private Label label2;
		private ComboBox comboMinutes;
		private ComboBox comboHours;
		private OpenDental.UI.Button butMinutes;
		private OpenDental.UI.Button butHours;
		public Popup PopupCur;
		private OpenDental.UI.Button butNone;
		///<summary>Will be zero unless user successfully clicked a disable time interval.  Accepted range is 1 to 1440 (24hrs)</summary>
		public int MinutesDisabled;

		///<summary></summary>
		public FormPopupEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopupEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkIsDisabled = new System.Windows.Forms.CheckBox();
			this.butDelete = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butNone = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboMinutes = new System.Windows.Forms.ComboBox();
			this.comboHours = new System.Windows.Forms.ComboBox();
			this.butMinutes = new OpenDental.UI.Button();
			this.butHours = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(347,181);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(347,147);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(77,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(50,37);
			this.textDescription.Multiline = true;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(271,91);
			this.textDescription.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(47,15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126,19);
			this.label1.TabIndex = 3;
			this.label1.Text = "Text";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkIsDisabled
			// 
			this.checkIsDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkIsDisabled.Location = new System.Drawing.Point(50,152);
			this.checkIsDisabled.Name = "checkIsDisabled";
			this.checkIsDisabled.Size = new System.Drawing.Size(235,18);
			this.checkIsDisabled.TabIndex = 4;
			this.checkIsDisabled.Text = "Permanently Disabled";
			this.checkIsDisabled.UseVisualStyleBackColor = true;
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
			this.butDelete.Location = new System.Drawing.Point(50,181);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 5;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butNone);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.comboMinutes);
			this.groupBox1.Controls.Add(this.comboHours);
			this.groupBox1.Controls.Add(this.butMinutes);
			this.groupBox1.Controls.Add(this.butHours);
			this.groupBox1.Location = new System.Drawing.Point(357,15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(271,107);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Save and Disable for";
			this.groupBox1.Visible = false;
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.Location = new System.Drawing.Point(91,20);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(77,24);
			this.butNone.TabIndex = 31;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(171,49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94,18);
			this.label3.TabIndex = 30;
			this.label3.Text = "This workstation";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(171,76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94,18);
			this.label2.TabIndex = 29;
			this.label2.Text = "This workstation";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboMinutes
			// 
			this.comboMinutes.FormattingEnabled = true;
			this.comboMinutes.Location = new System.Drawing.Point(15,49);
			this.comboMinutes.MaxDropDownItems = 48;
			this.comboMinutes.Name = "comboMinutes";
			this.comboMinutes.Size = new System.Drawing.Size(70,21);
			this.comboMinutes.TabIndex = 27;
			// 
			// comboHours
			// 
			this.comboHours.FormattingEnabled = true;
			this.comboHours.Location = new System.Drawing.Point(15,76);
			this.comboHours.MaxDropDownItems = 48;
			this.comboHours.Name = "comboHours";
			this.comboHours.Size = new System.Drawing.Size(70,21);
			this.comboHours.TabIndex = 26;
			// 
			// butMinutes
			// 
			this.butMinutes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMinutes.Autosize = true;
			this.butMinutes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMinutes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMinutes.CornerRadius = 4F;
			this.butMinutes.Location = new System.Drawing.Point(91,47);
			this.butMinutes.Name = "butMinutes";
			this.butMinutes.Size = new System.Drawing.Size(77,24);
			this.butMinutes.TabIndex = 4;
			this.butMinutes.Text = "Minutes";
			this.butMinutes.Click += new System.EventHandler(this.butMinutes_Click);
			// 
			// butHours
			// 
			this.butHours.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHours.Autosize = true;
			this.butHours.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHours.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHours.CornerRadius = 4F;
			this.butHours.Location = new System.Drawing.Point(91,74);
			this.butHours.Name = "butHours";
			this.butHours.Size = new System.Drawing.Size(77,24);
			this.butHours.TabIndex = 3;
			this.butHours.Text = "Hours";
			this.butHours.Click += new System.EventHandler(this.butHours_Click);
			// 
			// FormPopupEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(437,219);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkIsDisabled);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPopupEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Popup";
			this.Load += new System.EventHandler(this.FormPopupEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPopupEdit_Load(object sender,EventArgs e) {
			textDescription.Text=PopupCur.Description;
			checkIsDisabled.Checked=PopupCur.IsDisabled;
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

		///<summary>Exactly the same as OK.</summary>
		private void butNone_Click(object sender,EventArgs e) {
			if(textDescription.Text=="") {
				MsgBox.Show(this,"Please enter text first");
				return;
			}
			PopupCur.Description=textDescription.Text;
			PopupCur.IsDisabled=checkIsDisabled.Checked;
			Popups.WriteObject(PopupCur);
			DialogResult=DialogResult.OK;
		}

		private void butMinutes_Click(object sender,EventArgs e) {
			int minutes=0;
			try {
				minutes=Convert.ToInt32(comboMinutes.Text);
			}
			catch {
				MsgBox.Show(this,"Invalid number.");
				return;
			}
			if(minutes<1 || minutes>1440) {
				MsgBox.Show(this,"Number out of range.");
				return;
			}
			PopupCur.Description=textDescription.Text;
			PopupCur.IsDisabled=checkIsDisabled.Checked;
			Popups.WriteObject(PopupCur);
			MinutesDisabled=minutes;
			DialogResult=DialogResult.OK;
		}

		private void butHours_Click(object sender,EventArgs e) {
			int hours=0;
			try {
				hours=Convert.ToInt32(comboHours.Text);
			}
			catch {
				MsgBox.Show(this,"Invalid number.");
				return;
			}
			if(hours<1 || hours>24) {
				MsgBox.Show(this,"Number out of range.");
				return;
			}
			PopupCur.Description=textDescription.Text;
			PopupCur.IsDisabled=checkIsDisabled.Checked;
			Popups.WriteObject(PopupCur);
			MinutesDisabled=hours*60;
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!PopupCur.IsNew){
				Popups.DeleteObject(PopupCur);
				DialogResult=DialogResult.OK;
			}
			else{
				DialogResult=DialogResult.Cancel;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MsgBox.Show(this,"Please enter text first");
				return;
			}
			PopupCur.Description=textDescription.Text;
			PopupCur.IsDisabled=checkIsDisabled.Checked;
			Popups.WriteObject(PopupCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















