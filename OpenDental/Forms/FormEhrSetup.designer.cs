namespace OpenDental{
	partial class FormEhrSetup {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.butICD9s = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butAllergies = new OpenDental.UI.Button();
			this.butFormularies = new OpenDental.UI.Button();
			this.butVaccineDef = new OpenDental.UI.Button();
			this.butDrugManufacturer = new OpenDental.UI.Button();
			this.butDrugUnit = new OpenDental.UI.Button();
			this.butEmergencyNow = new OpenDental.UI.Button();
			this.panelEmergencyNow = new OpenDental.UI.PanelOD();
			this.butReminderRules = new OpenDental.UI.Button();
			this.butInboundEmail = new OpenDental.UI.Button();
			this.butEducationalResources = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butICD9s
			// 
			this.butICD9s.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butICD9s.Autosize = true;
			this.butICD9s.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butICD9s.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butICD9s.CornerRadius = 4F;
			this.butICD9s.Location = new System.Drawing.Point(27,60);
			this.butICD9s.Name = "butICD9s";
			this.butICD9s.Size = new System.Drawing.Size(128,24);
			this.butICD9s.TabIndex = 119;
			this.butICD9s.Text = "ICD9s";
			this.butICD9s.Click += new System.EventHandler(this.butICD9s_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(506,438);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAllergies
			// 
			this.butAllergies.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllergies.Autosize = true;
			this.butAllergies.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllergies.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllergies.CornerRadius = 4F;
			this.butAllergies.Location = new System.Drawing.Point(27,100);
			this.butAllergies.Name = "butAllergies";
			this.butAllergies.Size = new System.Drawing.Size(128,24);
			this.butAllergies.TabIndex = 120;
			this.butAllergies.Text = "Allergies";
			this.butAllergies.Click += new System.EventHandler(this.butAllergies_Click);
			// 
			// butFormularies
			// 
			this.butFormularies.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFormularies.Autosize = true;
			this.butFormularies.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFormularies.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFormularies.CornerRadius = 4F;
			this.butFormularies.Location = new System.Drawing.Point(27,140);
			this.butFormularies.Name = "butFormularies";
			this.butFormularies.Size = new System.Drawing.Size(128,24);
			this.butFormularies.TabIndex = 121;
			this.butFormularies.Text = "Formularies";
			this.butFormularies.Click += new System.EventHandler(this.butFormularies_Click);
			// 
			// butVaccineDef
			// 
			this.butVaccineDef.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butVaccineDef.Autosize = true;
			this.butVaccineDef.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butVaccineDef.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butVaccineDef.CornerRadius = 4F;
			this.butVaccineDef.Location = new System.Drawing.Point(27,180);
			this.butVaccineDef.Name = "butVaccineDef";
			this.butVaccineDef.Size = new System.Drawing.Size(128,24);
			this.butVaccineDef.TabIndex = 122;
			this.butVaccineDef.Text = "Vaccine Def";
			this.butVaccineDef.Click += new System.EventHandler(this.butVaccineDef_Click);
			// 
			// butDrugManufacturer
			// 
			this.butDrugManufacturer.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDrugManufacturer.Autosize = true;
			this.butDrugManufacturer.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrugManufacturer.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrugManufacturer.CornerRadius = 4F;
			this.butDrugManufacturer.Location = new System.Drawing.Point(27,220);
			this.butDrugManufacturer.Name = "butDrugManufacturer";
			this.butDrugManufacturer.Size = new System.Drawing.Size(128,24);
			this.butDrugManufacturer.TabIndex = 123;
			this.butDrugManufacturer.Text = "Drug Manufacturer";
			this.butDrugManufacturer.Click += new System.EventHandler(this.butDrugManufacturer_Click);
			// 
			// butDrugUnit
			// 
			this.butDrugUnit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDrugUnit.Autosize = true;
			this.butDrugUnit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrugUnit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrugUnit.CornerRadius = 4F;
			this.butDrugUnit.Location = new System.Drawing.Point(27,260);
			this.butDrugUnit.Name = "butDrugUnit";
			this.butDrugUnit.Size = new System.Drawing.Size(128,24);
			this.butDrugUnit.TabIndex = 124;
			this.butDrugUnit.Text = "Drug Unit";
			this.butDrugUnit.Click += new System.EventHandler(this.butDrugUnit_Click);
			// 
			// butEmergencyNow
			// 
			this.butEmergencyNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEmergencyNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butEmergencyNow.Autosize = true;
			this.butEmergencyNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmergencyNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmergencyNow.CornerRadius = 4F;
			this.butEmergencyNow.Location = new System.Drawing.Point(453,60);
			this.butEmergencyNow.Name = "butEmergencyNow";
			this.butEmergencyNow.Size = new System.Drawing.Size(98,24);
			this.butEmergencyNow.TabIndex = 124;
			this.butEmergencyNow.Text = "Emergency Now";
			this.butEmergencyNow.Click += new System.EventHandler(this.butEmergencyNow_Click);
			// 
			// panelEmergencyNow
			// 
			this.panelEmergencyNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelEmergencyNow.Location = new System.Drawing.Point(557,60);
			this.panelEmergencyNow.Name = "panelEmergencyNow";
			this.panelEmergencyNow.Size = new System.Drawing.Size(24,24);
			this.panelEmergencyNow.TabIndex = 125;
			// 
			// butReminderRules
			// 
			this.butReminderRules.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butReminderRules.Autosize = true;
			this.butReminderRules.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReminderRules.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReminderRules.CornerRadius = 4F;
			this.butReminderRules.Location = new System.Drawing.Point(27,300);
			this.butReminderRules.Name = "butReminderRules";
			this.butReminderRules.Size = new System.Drawing.Size(128,24);
			this.butReminderRules.TabIndex = 126;
			this.butReminderRules.Text = "Reminder Rules";
			this.butReminderRules.Click += new System.EventHandler(this.butReminderRules_Click);
			// 
			// butInboundEmail
			// 
			this.butInboundEmail.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInboundEmail.Autosize = true;
			this.butInboundEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInboundEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInboundEmail.CornerRadius = 4F;
			this.butInboundEmail.Location = new System.Drawing.Point(27,340);
			this.butInboundEmail.Name = "butInboundEmail";
			this.butInboundEmail.Size = new System.Drawing.Size(128,24);
			this.butInboundEmail.TabIndex = 127;
			this.butInboundEmail.Text = "Inbound Email";
			this.butInboundEmail.Click += new System.EventHandler(this.butInboundEmail_Click);
			// 
			// butEducationalResources
			// 
			this.butEducationalResources.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEducationalResources.Autosize = true;
			this.butEducationalResources.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEducationalResources.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEducationalResources.CornerRadius = 4F;
			this.butEducationalResources.Location = new System.Drawing.Point(27,380);
			this.butEducationalResources.Name = "butEducationalResources";
			this.butEducationalResources.Size = new System.Drawing.Size(128,24);
			this.butEducationalResources.TabIndex = 128;
			this.butEducationalResources.Text = "Educational Resources";
			this.butEducationalResources.Click += new System.EventHandler(this.butEducationalResources_Click);
			// 
			// FormEhrSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(606,489);
			this.Controls.Add(this.butEducationalResources);
			this.Controls.Add(this.butInboundEmail);
			this.Controls.Add(this.butReminderRules);
			this.Controls.Add(this.panelEmergencyNow);
			this.Controls.Add(this.butEmergencyNow);
			this.Controls.Add(this.butDrugUnit);
			this.Controls.Add(this.butDrugManufacturer);
			this.Controls.Add(this.butVaccineDef);
			this.Controls.Add(this.butFormularies);
			this.Controls.Add(this.butAllergies);
			this.Controls.Add(this.butICD9s);
			this.Controls.Add(this.butClose);
			this.Name = "FormEhrSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Electronic Health Record (EHR) Setup";
			this.Load += new System.EventHandler(this.FormEhrSetup_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.Button butICD9s;
		private UI.Button butAllergies;
		private UI.Button butFormularies;
		private UI.Button butVaccineDef;
		private UI.Button butDrugManufacturer;
		private UI.Button butDrugUnit;
		private UI.Button butEmergencyNow;
		private UI.PanelOD panelEmergencyNow;
		private UI.Button butReminderRules;
		private UI.Button butInboundEmail;
		private UI.Button butEducationalResources;
	}
}