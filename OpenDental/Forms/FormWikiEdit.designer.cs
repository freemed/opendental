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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWikiEdit));
			this.webBrowserWiki = new System.Windows.Forms.WebBrowser();
			this.butRefresh = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.textContent = new TextBoxWiki();
			this.contextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// webBrowserWiki
			// 
			this.webBrowserWiki.AllowWebBrowserDrop = false;
			this.webBrowserWiki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserWiki.Location = new System.Drawing.Point(474,31);
			this.webBrowserWiki.MinimumSize = new System.Drawing.Size(20,20);
			this.webBrowserWiki.Name = "webBrowserWiki";
			this.webBrowserWiki.Size = new System.Drawing.Size(470,542);
			this.webBrowserWiki.TabIndex = 78;
			this.webBrowserWiki.WebBrowserShortcutsEnabled = false;
			this.webBrowserWiki.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserWiki_Navigated);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(869,1);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,24);
			this.butRefresh.TabIndex = 77;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(863,25);
			this.ToolBarMain.TabIndex = 3;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"save.gif");
			this.imageListMain.Images.SetKeyName(1,"cancel.gif");
			this.imageListMain.Images.SetKeyName(2,"cut.gif");
			this.imageListMain.Images.SetKeyName(3,"copy.gif");
			this.imageListMain.Images.SetKeyName(4,"paste.gif");
			this.imageListMain.Images.SetKeyName(5,"undo.gif");
			this.imageListMain.Images.SetKeyName(6,"link.gif");
			this.imageListMain.Images.SetKeyName(7,"linkExternal.gif");
			this.imageListMain.Images.SetKeyName(8,"h1.gif");
			this.imageListMain.Images.SetKeyName(9,"h2.gif");
			this.imageListMain.Images.SetKeyName(10,"h3.gif");
			this.imageListMain.Images.SetKeyName(11,"bold.gif");
			this.imageListMain.Images.SetKeyName(12,"italic.gif");
			this.imageListMain.Images.SetKeyName(13,"color.gif");
			this.imageListMain.Images.SetKeyName(14,"table.gif");
			this.imageListMain.Images.SetKeyName(15,"image.gif");
			// 
			// textContent
			// 
			this.textContent.ContextMenuStrip = this.contextMenuMain;
			this.textContent.Location = new System.Drawing.Point(0,31);
			this.textContent.Name = "textContent";
			this.textContent.Size = new System.Drawing.Size(470,542);
			this.textContent.TabIndex = 80;
			// 
			// contextMenuMain
			// 
			this.contextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCut,
            this.menuItemCopy,
            this.menuItemPaste,
            this.toolStripMenuItem2,
            this.menuItemUndo});
			this.contextMenuMain.Name = "contextMenuMain";
			this.contextMenuMain.Size = new System.Drawing.Size(102,98);
			// 
			// menuItemCut
			// 
			this.menuItemCut.Name = "menuItemCut";
			this.menuItemCut.Size = new System.Drawing.Size(101,22);
			this.menuItemCut.Text = "Cut";
			this.menuItemCut.Click += new System.EventHandler(this.menuItemCut_Click);
			// 
			// menuItemCopy
			// 
			this.menuItemCopy.Name = "menuItemCopy";
			this.menuItemCopy.Size = new System.Drawing.Size(101,22);
			this.menuItemCopy.Text = "Copy";
			this.menuItemCopy.Click += new System.EventHandler(this.menuItemCopy_Click);
			// 
			// menuItemPaste
			// 
			this.menuItemPaste.Name = "menuItemPaste";
			this.menuItemPaste.Size = new System.Drawing.Size(101,22);
			this.menuItemPaste.Text = "Paste";
			this.menuItemPaste.Click += new System.EventHandler(this.menuItemPaste_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(98,6);
			// 
			// menuItemUndo
			// 
			this.menuItemUndo.Name = "menuItemUndo";
			this.menuItemUndo.Size = new System.Drawing.Size(101,22);
			this.menuItemUndo.Text = "Undo";
			this.menuItemUndo.Click += new System.EventHandler(this.menuItemUndo_Click);
			// 
			// FormWikiEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(944,573);
			this.Controls.Add(this.textContent);
			this.Controls.Add(this.webBrowserWiki);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "FormWikiEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Wiki Edit";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWikiEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormWikiEdit_Load);
			this.SizeChanged += new System.EventHandler(this.FormWikiEdit_SizeChanged);
			this.contextMenuMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.WebBrowser webBrowserWiki;
		private UI.Button butRefresh;
		private System.Windows.Forms.ImageList imageListMain;
		private TextBoxWiki textContent;
		private System.Windows.Forms.ContextMenuStrip contextMenuMain;
		private System.Windows.Forms.ToolStripMenuItem menuItemCut;
		private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
		private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem menuItemUndo;

	}
}