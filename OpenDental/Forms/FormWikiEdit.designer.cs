namespace OpenDental{
	partial class FormWikiEdit {
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
			this.textContent = new System.Windows.Forms.TextBox();
			this.webBrowserWiki = new System.Windows.Forms.WebBrowser();
			this.butRefresh = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// textContent
			// 
			this.textContent.AcceptsReturn = true;
			this.textContent.AcceptsTab = true;
			this.textContent.AllowDrop = true;
			this.textContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textContent.Font = new System.Drawing.Font("Courier New", 9.5F);
			this.textContent.Location = new System.Drawing.Point(0, 31);
			this.textContent.Multiline = true;
			this.textContent.Name = "textContent";
			this.textContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textContent.Size = new System.Drawing.Size(470, 542);
			this.textContent.TabIndex = 2;
			this.textContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textContent_KeyPress);
			// 
			// webBrowserWiki
			// 
			this.webBrowserWiki.AllowWebBrowserDrop = false;
			this.webBrowserWiki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserWiki.Location = new System.Drawing.Point(474, 31);
			this.webBrowserWiki.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowserWiki.Name = "webBrowserWiki";
			this.webBrowserWiki.Size = new System.Drawing.Size(470, 542);
			this.webBrowserWiki.TabIndex = 78;
			this.webBrowserWiki.WebBrowserShortcutsEnabled = false;
			this.webBrowserWiki.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserWiki_Navigated);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(474, 1);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75, 24);
			this.butRefresh.TabIndex = 77;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.ImageList = null;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(470, 25);
			this.ToolBarMain.TabIndex = 3;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// imageListMain
			// 
			this.imageListMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FormWikiEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(944, 573);
			this.Controls.Add(this.webBrowserWiki);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textContent);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "FormWikiEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Wiki Edit";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWikiEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormWikiEdit_Load);
			this.SizeChanged += new System.EventHandler(this.FormWikiEdit_SizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.TextBox textContent;
		private System.Windows.Forms.WebBrowser webBrowserWiki;
		private UI.Button butRefresh;
		private System.Windows.Forms.ImageList imageListMain;

	}
}