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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrSetup));
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.groupCodeSystems = new System.Windows.Forms.GroupBox();
			this.button1 = new OpenDental.UI.Button();
			this.butLoincs = new OpenDental.UI.Button();
			this.butRxNorm = new OpenDental.UI.Button();
			this.butSnomeds = new OpenDental.UI.Button();
			this.butICD9s = new OpenDental.UI.Button();
			this.butTimeSynch = new OpenDental.UI.Button();
			this.butKeys = new OpenDental.UI.Button();
			this.butEducationalResources = new OpenDental.UI.Button();
			this.butInboundEmail = new OpenDental.UI.Button();
			this.butReminderRules = new OpenDental.UI.Button();
			this.panelEmergencyNow = new OpenDental.UI.PanelOD();
			this.butEmergencyNow = new OpenDental.UI.Button();
			this.butDrugUnit = new OpenDental.UI.Button();
			this.butDrugManufacturer = new OpenDental.UI.Button();
			this.butVaccineDef = new OpenDental.UI.Button();
			this.butAllergies = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.groupCodeSystems.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSettings});
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 0;
			this.menuItemSettings.Text = "Settings";
			this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// groupCodeSystems
			// 
			this.groupCodeSystems.Controls.Add(this.butRxNorm);
			this.groupCodeSystems.Controls.Add(this.butICD9s);
			this.groupCodeSystems.Location = new System.Drawing.Point(202, 0);
			this.groupCodeSystems.Name = "groupCodeSystems";
			this.groupCodeSystems.Size = new System.Drawing.Size(152, 94);
			this.groupCodeSystems.TabIndex = 136;
			this.groupCodeSystems.TabStop = false;
			this.groupCodeSystems.Text = "Code Systems";
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(214, 174);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 24);
			this.button1.TabIndex = 14;
			this.button1.Text = "Code System Importer";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// butLoincs
			// 
			this.butLoincs.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butLoincs.Autosize = true;
			this.butLoincs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLoincs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLoincs.CornerRadius = 4F;
			this.butLoincs.Location = new System.Drawing.Point(214, 98);
			this.butLoincs.Name = "butLoincs";
			this.butLoincs.Size = new System.Drawing.Size(128, 24);
			this.butLoincs.TabIndex = 11;
			this.butLoincs.Text = "Loincs";
			this.butLoincs.Visible = false;
			this.butLoincs.Click += new System.EventHandler(this.butLoincs_Click);
			// 
			// butRxNorm
			// 
			this.butRxNorm.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRxNorm.Autosize = true;
			this.butRxNorm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRxNorm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRxNorm.CornerRadius = 4F;
			this.butRxNorm.Location = new System.Drawing.Point(12, 60);
			this.butRxNorm.Name = "butRxNorm";
			this.butRxNorm.Size = new System.Drawing.Size(128, 24);
			this.butRxNorm.TabIndex = 10;
			this.butRxNorm.Text = "RxNorms";
			this.butRxNorm.Click += new System.EventHandler(this.butRxNorm_Click);
			// 
			// butSnomeds
			// 
			this.butSnomeds.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSnomeds.Autosize = true;
			this.butSnomeds.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSnomeds.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSnomeds.CornerRadius = 4F;
			this.butSnomeds.Location = new System.Drawing.Point(214, 136);
			this.butSnomeds.Name = "butSnomeds";
			this.butSnomeds.Size = new System.Drawing.Size(128, 24);
			this.butSnomeds.TabIndex = 12;
			this.butSnomeds.Text = "SNOMED CTs";
			this.butSnomeds.Visible = false;
			this.butSnomeds.Click += new System.EventHandler(this.butSnomeds_Click);
			// 
			// butICD9s
			// 
			this.butICD9s.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butICD9s.Autosize = true;
			this.butICD9s.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butICD9s.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butICD9s.CornerRadius = 4F;
			this.butICD9s.Location = new System.Drawing.Point(12, 22);
			this.butICD9s.Name = "butICD9s";
			this.butICD9s.Size = new System.Drawing.Size(128, 24);
			this.butICD9s.TabIndex = 9;
			this.butICD9s.Text = "ICD9s";
			this.butICD9s.Click += new System.EventHandler(this.butICD9s_Click);
			// 
			// butTimeSynch
			// 
			this.butTimeSynch.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTimeSynch.Autosize = true;
			this.butTimeSynch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTimeSynch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTimeSynch.CornerRadius = 4F;
			this.butTimeSynch.Location = new System.Drawing.Point(214, 250);
			this.butTimeSynch.Name = "butTimeSynch";
			this.butTimeSynch.Size = new System.Drawing.Size(128, 24);
			this.butTimeSynch.TabIndex = 15;
			this.butTimeSynch.Text = "Time Synchronization";
			this.butTimeSynch.Click += new System.EventHandler(this.butTimeSynch_Click);
			// 
			// butKeys
			// 
			this.butKeys.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butKeys.Autosize = true;
			this.butKeys.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butKeys.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butKeys.CornerRadius = 4F;
			this.butKeys.Location = new System.Drawing.Point(27, 288);
			this.butKeys.Name = "butKeys";
			this.butKeys.Size = new System.Drawing.Size(128, 24);
			this.butKeys.TabIndex = 8;
			this.butKeys.Text = "Quarterly Keys";
			this.butKeys.Click += new System.EventHandler(this.butKeys_Click);
			// 
			// butEducationalResources
			// 
			this.butEducationalResources.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEducationalResources.Autosize = true;
			this.butEducationalResources.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEducationalResources.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEducationalResources.CornerRadius = 4F;
			this.butEducationalResources.Location = new System.Drawing.Point(27, 250);
			this.butEducationalResources.Name = "butEducationalResources";
			this.butEducationalResources.Size = new System.Drawing.Size(128, 24);
			this.butEducationalResources.TabIndex = 7;
			this.butEducationalResources.Text = "Educational Resources";
			this.butEducationalResources.Click += new System.EventHandler(this.butEducationalResources_Click);
			// 
			// butInboundEmail
			// 
			this.butInboundEmail.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInboundEmail.Autosize = true;
			this.butInboundEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInboundEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInboundEmail.CornerRadius = 4F;
			this.butInboundEmail.Location = new System.Drawing.Point(27, 212);
			this.butInboundEmail.Name = "butInboundEmail";
			this.butInboundEmail.Size = new System.Drawing.Size(128, 24);
			this.butInboundEmail.TabIndex = 6;
			this.butInboundEmail.Text = "Inbound Email";
			this.butInboundEmail.Click += new System.EventHandler(this.butInboundEmail_Click);
			// 
			// butReminderRules
			// 
			this.butReminderRules.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butReminderRules.Autosize = true;
			this.butReminderRules.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReminderRules.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReminderRules.CornerRadius = 4F;
			this.butReminderRules.Location = new System.Drawing.Point(27, 174);
			this.butReminderRules.Name = "butReminderRules";
			this.butReminderRules.Size = new System.Drawing.Size(128, 24);
			this.butReminderRules.TabIndex = 5;
			this.butReminderRules.Text = "Reminder Rules";
			this.butReminderRules.Click += new System.EventHandler(this.butReminderRules_Click);
			// 
			// panelEmergencyNow
			// 
			this.panelEmergencyNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelEmergencyNow.Location = new System.Drawing.Point(504, 60);
			this.panelEmergencyNow.Name = "panelEmergencyNow";
			this.panelEmergencyNow.Size = new System.Drawing.Size(24, 24);
			this.panelEmergencyNow.TabIndex = 125;
			// 
			// butEmergencyNow
			// 
			this.butEmergencyNow.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEmergencyNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butEmergencyNow.Autosize = true;
			this.butEmergencyNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmergencyNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmergencyNow.CornerRadius = 4F;
			this.butEmergencyNow.Location = new System.Drawing.Point(400, 60);
			this.butEmergencyNow.Name = "butEmergencyNow";
			this.butEmergencyNow.Size = new System.Drawing.Size(98, 24);
			this.butEmergencyNow.TabIndex = 15;
			this.butEmergencyNow.Text = "Emergency Now";
			this.butEmergencyNow.Click += new System.EventHandler(this.butEmergencyNow_Click);
			// 
			// butDrugUnit
			// 
			this.butDrugUnit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDrugUnit.Autosize = true;
			this.butDrugUnit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrugUnit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrugUnit.CornerRadius = 4F;
			this.butDrugUnit.Location = new System.Drawing.Point(27, 136);
			this.butDrugUnit.Name = "butDrugUnit";
			this.butDrugUnit.Size = new System.Drawing.Size(128, 24);
			this.butDrugUnit.TabIndex = 4;
			this.butDrugUnit.Text = "Drug Unit";
			this.butDrugUnit.Click += new System.EventHandler(this.butDrugUnit_Click);
			// 
			// butDrugManufacturer
			// 
			this.butDrugManufacturer.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDrugManufacturer.Autosize = true;
			this.butDrugManufacturer.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDrugManufacturer.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDrugManufacturer.CornerRadius = 4F;
			this.butDrugManufacturer.Location = new System.Drawing.Point(27, 98);
			this.butDrugManufacturer.Name = "butDrugManufacturer";
			this.butDrugManufacturer.Size = new System.Drawing.Size(128, 24);
			this.butDrugManufacturer.TabIndex = 3;
			this.butDrugManufacturer.Text = "Drug Manufacturer";
			this.butDrugManufacturer.Click += new System.EventHandler(this.butDrugManufacturer_Click);
			// 
			// butVaccineDef
			// 
			this.butVaccineDef.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butVaccineDef.Autosize = true;
			this.butVaccineDef.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butVaccineDef.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butVaccineDef.CornerRadius = 4F;
			this.butVaccineDef.Location = new System.Drawing.Point(27, 60);
			this.butVaccineDef.Name = "butVaccineDef";
			this.butVaccineDef.Size = new System.Drawing.Size(128, 24);
			this.butVaccineDef.TabIndex = 2;
			this.butVaccineDef.Text = "Vaccine Def";
			this.butVaccineDef.Click += new System.EventHandler(this.butVaccineDef_Click);
			// 
			// butAllergies
			// 
			this.butAllergies.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAllergies.Autosize = true;
			this.butAllergies.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllergies.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllergies.CornerRadius = 4F;
			this.butAllergies.Location = new System.Drawing.Point(27, 22);
			this.butAllergies.Name = "butAllergies";
			this.butAllergies.Size = new System.Drawing.Size(128, 24);
			this.butAllergies.TabIndex = 1;
			this.butAllergies.Text = "Allergies";
			this.butAllergies.Click += new System.EventHandler(this.butAllergies_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(461, 288);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 16;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormEhrSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(561, 339);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butTimeSynch);
			this.Controls.Add(this.butLoincs);
			this.Controls.Add(this.groupCodeSystems);
			this.Controls.Add(this.butSnomeds);
			this.Controls.Add(this.butKeys);
			this.Controls.Add(this.butEducationalResources);
			this.Controls.Add(this.butInboundEmail);
			this.Controls.Add(this.butReminderRules);
			this.Controls.Add(this.panelEmergencyNow);
			this.Controls.Add(this.butEmergencyNow);
			this.Controls.Add(this.butDrugUnit);
			this.Controls.Add(this.butDrugManufacturer);
			this.Controls.Add(this.butVaccineDef);
			this.Controls.Add(this.butAllergies);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "FormEhrSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Electronic Health Record (EHR) Setup";
			this.Load += new System.EventHandler(this.FormEhrSetup_Load);
			this.groupCodeSystems.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.Button butICD9s;
		private UI.Button butAllergies;
		private UI.Button butVaccineDef;
		private UI.Button butDrugManufacturer;
		private UI.Button butDrugUnit;
		private UI.Button butEmergencyNow;
		private UI.PanelOD panelEmergencyNow;
		private UI.Button butReminderRules;
		private UI.Button butInboundEmail;
		private UI.Button butEducationalResources;
		private UI.Button butRxNorm;
		private UI.Button butKeys;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private UI.Button butLoincs;
		private UI.Button butSnomeds;
		private UI.Button button1;
		private System.Windows.Forms.GroupBox groupCodeSystems;
		private UI.Button butTimeSynch;
	}
}