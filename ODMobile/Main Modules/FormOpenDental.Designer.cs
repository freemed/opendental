namespace OpenDentMobile {
	partial class FormOpenDental {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu1;

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemModule = new System.Windows.Forms.MenuItem();
			this.menuItemAppt = new System.Windows.Forms.MenuItem();
			this.menuItemFamily = new System.Windows.Forms.MenuItem();
			this.menuItemMenu = new System.Windows.Forms.MenuItem();
			this.menuItemSelectPat = new System.Windows.Forms.MenuItem();
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItemModule);
			this.mainMenu1.MenuItems.Add(this.menuItemMenu);
			// 
			// menuItemModule
			// 
			this.menuItemModule.MenuItems.Add(this.menuItemAppt);
			this.menuItemModule.MenuItems.Add(this.menuItemFamily);
			this.menuItemModule.Text = "Module";
			this.menuItemModule.Popup += new System.EventHandler(this.menuItemModule_Popup);
			// 
			// menuItemAppt
			// 
			this.menuItemAppt.Text = "Appointments";
			this.menuItemAppt.Click += new System.EventHandler(this.menuItemAppt_Click);
			// 
			// menuItemFamily
			// 
			this.menuItemFamily.Text = "Family";
			this.menuItemFamily.Click += new System.EventHandler(this.menuItemFamily_Click);
			// 
			// menuItemMenu
			// 
			this.menuItemMenu.MenuItems.Add(this.menuItemSetup);
			this.menuItemMenu.MenuItems.Add(this.menuItemSelectPat);
			this.menuItemMenu.Text = "Menu";
			// 
			// menuItemSelectPat
			// 
			this.menuItemSelectPat.Text = "Select Patient";
			this.menuItemSelectPat.Click += new System.EventHandler(this.menuItemSelectPat_Click);
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// FormOpenDental
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240,268);
			this.Font = new System.Drawing.Font("Tahoma",8F,System.Drawing.FontStyle.Regular);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "FormOpenDental";
			this.Text = "Open Dental";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuItem menuItemModule;
		private System.Windows.Forms.MenuItem menuItemAppt;
		private System.Windows.Forms.MenuItem menuItemFamily;
		private System.Windows.Forms.MenuItem menuItemMenu;
		private System.Windows.Forms.MenuItem menuItemSelectPat;
		private System.Windows.Forms.MenuItem menuItemSetup;

	}
}

