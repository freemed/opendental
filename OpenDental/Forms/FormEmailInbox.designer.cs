namespace OpenDental{
	partial class FormEmailInbox {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmailInbox));
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.gridEmailMessages = new OpenDental.UI.ODGrid();
			this.labelInboxComputerName = new System.Windows.Forms.Label();
			this.labelThisComputer = new System.Windows.Forms.Label();
			this.butChangePat = new OpenDental.UI.Button();
			this.butMarkUnread = new OpenDental.UI.Button();
			this.butMarkRead = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.odToolBarButton1 = new OpenDental.UI.ODToolBarButton();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// gridEmailMessages
			// 
			this.gridEmailMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridEmailMessages.HScrollVisible = false;
			this.gridEmailMessages.Location = new System.Drawing.Point(12,27);
			this.gridEmailMessages.Name = "gridEmailMessages";
			this.gridEmailMessages.ScrollValue = 0;
			this.gridEmailMessages.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridEmailMessages.Size = new System.Drawing.Size(945,543);
			this.gridEmailMessages.TabIndex = 140;
			this.gridEmailMessages.Title = "Email Messages";
			this.gridEmailMessages.TranslationName = "TableApptProcs";
			this.gridEmailMessages.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridEmailMessages_CellDoubleClick);
			// 
			// labelInboxComputerName
			// 
			this.labelInboxComputerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelInboxComputerName.Location = new System.Drawing.Point(9,573);
			this.labelInboxComputerName.Name = "labelInboxComputerName";
			this.labelInboxComputerName.Size = new System.Drawing.Size(410,16);
			this.labelInboxComputerName.TabIndex = 144;
			this.labelInboxComputerName.Text = "Computer Name Where New Email Is Fetched: ";
			this.labelInboxComputerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelThisComputer
			// 
			this.labelThisComputer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelThisComputer.Location = new System.Drawing.Point(127,589);
			this.labelThisComputer.Name = "labelThisComputer";
			this.labelThisComputer.Size = new System.Drawing.Size(292,16);
			this.labelThisComputer.TabIndex = 145;
			this.labelThisComputer.Text = "This Computer Name: ";
			this.labelThisComputer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butChangePat
			// 
			this.butChangePat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChangePat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butChangePat.Autosize = true;
			this.butChangePat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangePat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangePat.CornerRadius = 4F;
			this.butChangePat.Location = new System.Drawing.Point(639,0);
			this.butChangePat.Name = "butChangePat";
			this.butChangePat.Size = new System.Drawing.Size(75,24);
			this.butChangePat.TabIndex = 146;
			this.butChangePat.Text = "Change Pat";
			this.butChangePat.Click += new System.EventHandler(this.butChangePat_Click);
			// 
			// butMarkUnread
			// 
			this.butMarkUnread.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMarkUnread.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMarkUnread.Autosize = true;
			this.butMarkUnread.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMarkUnread.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMarkUnread.CornerRadius = 4F;
			this.butMarkUnread.Location = new System.Drawing.Point(720,0);
			this.butMarkUnread.Name = "butMarkUnread";
			this.butMarkUnread.Size = new System.Drawing.Size(75,24);
			this.butMarkUnread.TabIndex = 143;
			this.butMarkUnread.Text = "Mark Unread";
			this.butMarkUnread.Click += new System.EventHandler(this.butMarkUnread_Click);
			// 
			// butMarkRead
			// 
			this.butMarkRead.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMarkRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMarkRead.Autosize = true;
			this.butMarkRead.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMarkRead.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMarkRead.CornerRadius = 4F;
			this.butMarkRead.Location = new System.Drawing.Point(801,0);
			this.butMarkRead.Name = "butMarkRead";
			this.butMarkRead.Size = new System.Drawing.Size(75,24);
			this.butMarkRead.TabIndex = 142;
			this.butMarkRead.Text = "Mark Read";
			this.butMarkRead.Click += new System.EventHandler(this.butMarkRead_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(882,0);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,24);
			this.butRefresh.TabIndex = 141;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(882,576);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// odToolBarButton1
			// 
			this.odToolBarButton1.Bounds = new System.Drawing.Rectangle(0,0,0,0);
			this.odToolBarButton1.DropDownMenu = null;
			this.odToolBarButton1.Enabled = true;
			this.odToolBarButton1.ImageIndex = -1;
			this.odToolBarButton1.Pushed = false;
			this.odToolBarButton1.State = OpenDental.UI.ToolBarButtonState.Normal;
			this.odToolBarButton1.Style = OpenDental.UI.ODToolBarButtonStyle.PushButton;
			this.odToolBarButton1.Tag = "";
			this.odToolBarButton1.Text = "";
			this.odToolBarButton1.ToolTipText = "";
			// 
			// FormEmailInbox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(982,627);
			this.Controls.Add(this.butChangePat);
			this.Controls.Add(this.labelThisComputer);
			this.Controls.Add(this.labelInboxComputerName);
			this.Controls.Add(this.butMarkUnread);
			this.Controls.Add(this.butMarkRead);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.gridEmailMessages);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size(864,200);
			this.Name = "FormEmailInbox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email Inbox for service@opendental.com";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormEmailInbox_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemSetup;
		private UI.ODGrid gridEmailMessages;
		private UI.ODToolBarButton odToolBarButton1;
		private UI.Button butRefresh;
		private UI.Button butMarkRead;
		private UI.Button butMarkUnread;
		private System.Windows.Forms.Label labelInboxComputerName;
		private System.Windows.Forms.Label labelThisComputer;
		private UI.Button butChangePat;
	}
}