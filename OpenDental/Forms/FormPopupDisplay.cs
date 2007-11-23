using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormPopupDisplay:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private TextBox textDescription;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butDays;
		private OpenDental.UI.Button butMinutes;
		private OpenDental.UI.Button butHours;
		private ComboBox comboMinutes;
		private ComboBox comboHours;
		private ComboBox comboDays;
		private Label label3;
		private Label label2;
		private Label label1;
		public Popup PopupCur;

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
			this.butOK = new OpenDental.UI.Button();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butMinutes = new OpenDental.UI.Button();
			this.butHours = new OpenDental.UI.Button();
			this.butDays = new OpenDental.UI.Button();
			this.comboDays = new System.Windows.Forms.ComboBox();
			this.comboHours = new System.Windows.Forms.ComboBox();
			this.comboMinutes = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(357,221);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(50,34);
			this.textDescription.Multiline = true;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(271,91);
			this.textDescription.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.comboMinutes);
			this.groupBox1.Controls.Add(this.comboHours);
			this.groupBox1.Controls.Add(this.comboDays);
			this.groupBox1.Controls.Add(this.butMinutes);
			this.groupBox1.Controls.Add(this.butHours);
			this.groupBox1.Controls.Add(this.butDays);
			this.groupBox1.Location = new System.Drawing.Point(50,142);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(271,103);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Disable For";
			// 
			// butMinutes
			// 
			this.butMinutes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMinutes.Autosize = true;
			this.butMinutes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMinutes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMinutes.CornerRadius = 4F;
			this.butMinutes.Location = new System.Drawing.Point(91,71);
			this.butMinutes.Name = "butMinutes";
			this.butMinutes.Size = new System.Drawing.Size(68,24);
			this.butMinutes.TabIndex = 4;
			this.butMinutes.Text = "Minutes";
			// 
			// butHours
			// 
			this.butHours.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHours.Autosize = true;
			this.butHours.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHours.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHours.CornerRadius = 4F;
			this.butHours.Location = new System.Drawing.Point(91,44);
			this.butHours.Name = "butHours";
			this.butHours.Size = new System.Drawing.Size(68,24);
			this.butHours.TabIndex = 3;
			this.butHours.Text = "Hours";
			// 
			// butDays
			// 
			this.butDays.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDays.Autosize = true;
			this.butDays.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDays.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDays.CornerRadius = 4F;
			this.butDays.Location = new System.Drawing.Point(91,17);
			this.butDays.Name = "butDays";
			this.butDays.Size = new System.Drawing.Size(68,24);
			this.butDays.TabIndex = 2;
			this.butDays.Text = "Days";
			// 
			// comboDays
			// 
			this.comboDays.FormattingEnabled = true;
			this.comboDays.Location = new System.Drawing.Point(15,19);
			this.comboDays.MaxDropDownItems = 48;
			this.comboDays.Name = "comboDays";
			this.comboDays.Size = new System.Drawing.Size(70,21);
			this.comboDays.TabIndex = 25;
			// 
			// comboHours
			// 
			this.comboHours.FormattingEnabled = true;
			this.comboHours.Location = new System.Drawing.Point(15,46);
			this.comboHours.MaxDropDownItems = 48;
			this.comboHours.Name = "comboHours";
			this.comboHours.Size = new System.Drawing.Size(70,21);
			this.comboHours.TabIndex = 26;
			// 
			// comboMinutes
			// 
			this.comboMinutes.FormattingEnabled = true;
			this.comboMinutes.Location = new System.Drawing.Point(15,73);
			this.comboMinutes.MaxDropDownItems = 48;
			this.comboMinutes.Name = "comboMinutes";
			this.comboMinutes.Size = new System.Drawing.Size(70,21);
			this.comboMinutes.TabIndex = 27;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(162,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 4;
			this.label1.Text = "All workstations";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(162,47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 28;
			this.label2.Text = "This workstation";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(162,73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,18);
			this.label3.TabIndex = 29;
			this.label3.Text = "This workstation";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormPopupDisplay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(459,266);
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
			for(int i=1;i<=30;i++) {
				comboDays.Items.Add(i.ToString());
			}
			comboDays.Items.Add("45");
			comboDays.Items.Add("60");
			comboDays.Text="1";

			//comboStart.Text=SchedCur.StartTime.ToShortTimeString();
			//comboStop.Text=SchedCur.StopTime.ToShortTimeString();
			//checkIsDisabled.Checked=PopupCur.IsDisabled;
		}

		/*private void butDelete_Click(object sender,EventArgs e) {
			if(!PopupCur.IsNew){
				Popups.DeleteObject(PopupCur);
				DialogResult=DialogResult.OK;
			}
			else{
				DialogResult=DialogResult.Cancel;
			}
		}*/



		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MsgBox.Show(this,"Please enter text first");
				return;
			}
			PopupCur.Description=textDescription.Text;
			//PopupCur.IsDisabled=checkIsDisabled.Checked;
			try{
				Popups.WriteObject(PopupCur);
			}
			catch(System.Exception ex){
				MessageBox.Show(ex.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















