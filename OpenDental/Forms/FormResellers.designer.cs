namespace OpenDental{
	partial class FormResellers {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResellers));
			this.gridMain = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.menuRightClick = new System.Windows.Forms.ContextMenu();
			this.menuItemAccount = new System.Windows.Forms.MenuItem();
			this.labelResellerParagraph = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(12, 165);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(780, 372);
			this.gridMain.TabIndex = 38;
			this.gridMain.Title = "Resellers";
			this.gridMain.TranslationName = "FormMedications";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(502, 133);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(209, 26);
			this.label2.TabIndex = 40;
			this.label2.Text = "Add as a customer first.\r\nThey must also be the guarantor.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// menuRightClick
			// 
			this.menuRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAccount});
			// 
			// menuItemAccount
			// 
			this.menuItemAccount.Index = 0;
			this.menuItemAccount.Text = "Go to Account";
			this.menuItemAccount.Click += new System.EventHandler(this.menuItemAccount_Click);
			// 
			// labelResellerParagraph
			// 
			this.labelResellerParagraph.Location = new System.Drawing.Point(12, 15);
			this.labelResellerParagraph.Name = "labelResellerParagraph";
			this.labelResellerParagraph.Size = new System.Drawing.Size(780, 115);
			this.labelResellerParagraph.TabIndex = 41;
			this.labelResellerParagraph.Text = resources.GetString("labelResellerParagraph.Text");
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(717, 550);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(717, 133);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 35;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormResellers
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(808, 583);
			this.Controls.Add(this.labelResellerParagraph);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Name = "FormResellers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Resellers";
			this.Load += new System.EventHandler(this.FormResellers_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.Button butAdd;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenu menuRightClick;
		private System.Windows.Forms.MenuItem menuItemAccount;
		private System.Windows.Forms.Label labelResellerParagraph;
	}
}