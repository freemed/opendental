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
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.textNumbers = new System.Windows.Forms.TextBox();
			this.toolBar2 = new OpenDental.UI.ODToolBar();
			this.textContent = new OpenDental.TextBoxWiki();
			this.webBrowserWiki = new System.Windows.Forms.WebBrowser();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.contextMenuMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"refresh.gif");
			this.imageListMain.Images.SetKeyName(1,"save.gif");
			this.imageListMain.Images.SetKeyName(2,"cancel.gif");
			this.imageListMain.Images.SetKeyName(3,"cut.gif");
			this.imageListMain.Images.SetKeyName(4,"copy.gif");
			this.imageListMain.Images.SetKeyName(5,"paste.gif");
			this.imageListMain.Images.SetKeyName(6,"undo.gif");
			this.imageListMain.Images.SetKeyName(7,"link.gif");
			this.imageListMain.Images.SetKeyName(8,"linkExternal.gif");
			this.imageListMain.Images.SetKeyName(9,"h1.gif");
			this.imageListMain.Images.SetKeyName(10,"h2.gif");
			this.imageListMain.Images.SetKeyName(11,"h3.gif");
			this.imageListMain.Images.SetKeyName(12,"bold.gif");
			this.imageListMain.Images.SetKeyName(13,"italic.gif");
			this.imageListMain.Images.SetKeyName(14,"color.gif");
			this.imageListMain.Images.SetKeyName(15,"table.gif");
			this.imageListMain.Images.SetKeyName(16,"image.gif");
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
			// textNumbers
			// 
			this.textNumbers.Font = new System.Drawing.Font("Courier New",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))),((int)(((byte)(117)))),((int)(((byte)(133)))));
			this.textNumbers.Location = new System.Drawing.Point(0,58);
			this.textNumbers.Multiline = true;
			this.textNumbers.Name = "textNumbers";
			this.textNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNumbers.Size = new System.Drawing.Size(50,514);
			this.textNumbers.TabIndex = 81;
			this.textNumbers.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n188\r\n288";
			this.textNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// toolBar2
			// 
			this.toolBar2.ImageList = this.imageListMain;
			this.toolBar2.Location = new System.Drawing.Point(0,26);
			this.toolBar2.Name = "toolBar2";
			this.toolBar2.Size = new System.Drawing.Size(863,25);
			this.toolBar2.TabIndex = 82;
			this.toolBar2.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.toolBar2_ButtonClick);
			// 
			// textContent
			// 
			this.textContent.ContextMenuStrip = this.contextMenuMain;
			this.textContent.Font = new System.Drawing.Font("Courier New",10F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textContent.Location = new System.Drawing.Point(32,58);
			this.textContent.Name = "textContent";
			this.textContent.ReadOnly = false;
			this.textContent.SelectedText = "";
			this.textContent.SelectionLength = 0;
			this.textContent.SelectionStart = 0;
			this.textContent.Size = new System.Drawing.Size(438,514);
			this.textContent.TabIndex = 80;
			this.textContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textContent_KeyPress);
			this.textContent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textContent_MouseDoubleClick);
			// 
			// webBrowserWiki
			// 
			this.webBrowserWiki.AllowWebBrowserDrop = false;
			this.webBrowserWiki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserWiki.Location = new System.Drawing.Point(474,58);
			this.webBrowserWiki.MinimumSize = new System.Drawing.Size(20,20);
			this.webBrowserWiki.Name = "webBrowserWiki";
			this.webBrowserWiki.Size = new System.Drawing.Size(470,514);
			this.webBrowserWiki.TabIndex = 78;
			this.webBrowserWiki.WebBrowserShortcutsEnabled = false;
			this.webBrowserWiki.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserWiki_Navigated);
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
			// FormWikiEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(944,573);
			this.Controls.Add(this.toolBar2);
			this.Controls.Add(this.textContent);
			this.Controls.Add(this.textNumbers);
			this.Controls.Add(this.webBrowserWiki);
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
		private System.Windows.Forms.ImageList imageListMain;
		private TextBoxWiki textContent;
		private System.Windows.Forms.ContextMenuStrip contextMenuMain;
		private System.Windows.Forms.ToolStripMenuItem menuItemCut;
		private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
		private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem menuItemUndo;
		private System.Windows.Forms.TextBox textNumbers;
		private UI.ODToolBar toolBar2;

	}
}