namespace OpenDental {
	partial class UserControlTasks {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlTasks));
			this.tabContr = new System.Windows.Forms.TabControl();
			this.tabUser = new System.Windows.Forms.TabPage();
			this.tabMain = new System.Windows.Forms.TabPage();
			this.tabRepeating = new System.Windows.Forms.TabPage();
			this.tabDate = new System.Windows.Forms.TabPage();
			this.tabWeek = new System.Windows.Forms.TabPage();
			this.tabMonth = new System.Windows.Forms.TabPage();
			this.cal = new System.Windows.Forms.MonthCalendar();
			this.tree = new System.Windows.Forms.TreeView();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.menuEdit = new System.Windows.Forms.ContextMenu();
			this.menuItemEdit = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemCut = new System.Windows.Forms.MenuItem();
			this.menuItemCopy = new System.Windows.Forms.MenuItem();
			this.menuItemPaste = new System.Windows.Forms.MenuItem();
			this.menuItemDelete = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemSubscribe = new System.Windows.Forms.MenuItem();
			this.menuItemUnsubscribe = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemGoto = new System.Windows.Forms.MenuItem();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.tabContr.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabContr
			// 
			this.tabContr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabContr.Controls.Add(this.tabUser);
			this.tabContr.Controls.Add(this.tabMain);
			this.tabContr.Controls.Add(this.tabRepeating);
			this.tabContr.Controls.Add(this.tabDate);
			this.tabContr.Controls.Add(this.tabWeek);
			this.tabContr.Controls.Add(this.tabMonth);
			this.tabContr.Location = new System.Drawing.Point(0,29);
			this.tabContr.Name = "tabContr";
			this.tabContr.SelectedIndex = 0;
			this.tabContr.Size = new System.Drawing.Size(941,23);
			this.tabContr.TabIndex = 5;
			this.tabContr.Click += new System.EventHandler(this.tabContr_Click);
			// 
			// tabUser
			// 
			this.tabUser.Location = new System.Drawing.Point(4,22);
			this.tabUser.Name = "tabUser";
			this.tabUser.Size = new System.Drawing.Size(933,0);
			this.tabUser.TabIndex = 5;
			this.tabUser.Text = "for User";
			this.tabUser.UseVisualStyleBackColor = true;
			// 
			// tabMain
			// 
			this.tabMain.Location = new System.Drawing.Point(4,22);
			this.tabMain.Name = "tabMain";
			this.tabMain.Size = new System.Drawing.Size(933,0);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Main";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// tabRepeating
			// 
			this.tabRepeating.Location = new System.Drawing.Point(4,22);
			this.tabRepeating.Name = "tabRepeating";
			this.tabRepeating.Size = new System.Drawing.Size(933,0);
			this.tabRepeating.TabIndex = 2;
			this.tabRepeating.Text = "Repeating (setup)";
			this.tabRepeating.UseVisualStyleBackColor = true;
			// 
			// tabDate
			// 
			this.tabDate.Location = new System.Drawing.Point(4,22);
			this.tabDate.Name = "tabDate";
			this.tabDate.Size = new System.Drawing.Size(933,0);
			this.tabDate.TabIndex = 1;
			this.tabDate.Text = "By Date";
			this.tabDate.UseVisualStyleBackColor = true;
			// 
			// tabWeek
			// 
			this.tabWeek.Location = new System.Drawing.Point(4,22);
			this.tabWeek.Name = "tabWeek";
			this.tabWeek.Size = new System.Drawing.Size(933,0);
			this.tabWeek.TabIndex = 3;
			this.tabWeek.Text = "By Week";
			this.tabWeek.UseVisualStyleBackColor = true;
			// 
			// tabMonth
			// 
			this.tabMonth.Location = new System.Drawing.Point(4,22);
			this.tabMonth.Name = "tabMonth";
			this.tabMonth.Size = new System.Drawing.Size(933,0);
			this.tabMonth.TabIndex = 4;
			this.tabMonth.Text = "By Month";
			this.tabMonth.UseVisualStyleBackColor = true;
			// 
			// cal
			// 
			this.cal.Location = new System.Drawing.Point(2,53);
			this.cal.MaxSelectionCount = 1;
			this.cal.Name = "cal";
			this.cal.TabIndex = 6;
			this.cal.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.cal_DateSelected);
			// 
			// tree
			// 
			this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tree.HideSelection = false;
			this.tree.ImageIndex = 0;
			this.tree.ImageList = this.imageListTree;
			this.tree.ItemHeight = 18;
			this.tree.Location = new System.Drawing.Point(0,206);
			this.tree.Name = "tree";
			this.tree.Scrollable = false;
			this.tree.SelectedImageIndex = 0;
			this.tree.ShowPlusMinus = false;
			this.tree.Size = new System.Drawing.Size(941,98);
			this.tree.TabIndex = 7;
			this.tree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tree_MouseDown);
			// 
			// imageListTree
			// 
			this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
			this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTree.Images.SetKeyName(0,"TaskList.gif");
			this.imageListTree.Images.SetKeyName(1,"checkBoxChecked.gif");
			this.imageListTree.Images.SetKeyName(2,"checkBoxUnchecked.gif");
			this.imageListTree.Images.SetKeyName(3,"TaskListHighlight.gif");
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"TaskListAdd.gif");
			this.imageListMain.Images.SetKeyName(1,"Add.gif");
			// 
			// menuEdit
			// 
			this.menuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEdit,
            this.menuItem6,
            this.menuItemCut,
            this.menuItemCopy,
            this.menuItemPaste,
            this.menuItemDelete,
            this.menuItem2,
            this.menuItemSubscribe,
            this.menuItemUnsubscribe,
            this.menuItem3,
            this.menuItemGoto});
			this.menuEdit.Popup += new System.EventHandler(this.menuEdit_Popup);
			// 
			// menuItemEdit
			// 
			this.menuItemEdit.Index = 0;
			this.menuItemEdit.Text = "Edit Properties";
			this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "-";
			// 
			// menuItemCut
			// 
			this.menuItemCut.Index = 2;
			this.menuItemCut.Text = "Cut";
			this.menuItemCut.Click += new System.EventHandler(this.menuItemCut_Click);
			// 
			// menuItemCopy
			// 
			this.menuItemCopy.Index = 3;
			this.menuItemCopy.Text = "Copy";
			this.menuItemCopy.Click += new System.EventHandler(this.menuItemCopy_Click);
			// 
			// menuItemPaste
			// 
			this.menuItemPaste.Index = 4;
			this.menuItemPaste.Text = "Paste";
			this.menuItemPaste.Click += new System.EventHandler(this.menuItemPaste_Click);
			// 
			// menuItemDelete
			// 
			this.menuItemDelete.Index = 5;
			this.menuItemDelete.Text = "Delete";
			this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 6;
			this.menuItem2.Text = "-";
			// 
			// menuItemSubscribe
			// 
			this.menuItemSubscribe.Index = 7;
			this.menuItemSubscribe.Text = "Subscribe";
			this.menuItemSubscribe.Click += new System.EventHandler(this.menuItemSubscribe_Click);
			// 
			// menuItemUnsubscribe
			// 
			this.menuItemUnsubscribe.Index = 8;
			this.menuItemUnsubscribe.Text = "Unsubscribe";
			this.menuItemUnsubscribe.Click += new System.EventHandler(this.menuItemUnsubscribe_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 9;
			this.menuItem3.Text = "-";
			// 
			// menuItemGoto
			// 
			this.menuItemGoto.Index = 10;
			this.menuItemGoto.Text = "Go To";
			this.menuItemGoto.Click += new System.EventHandler(this.menuItemGoto_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0,310);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(941,200);
			this.gridMain.TabIndex = 9;
			this.gridMain.Title = "Tasks";
			this.gridMain.TranslationName = "TableTasks";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridMain_MouseDown);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(941,25);
			this.ToolBarMain.TabIndex = 2;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// UserControlTasks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.tree);
			this.Controls.Add(this.cal);
			this.Controls.Add(this.tabContr);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "UserControlTasks";
			this.Size = new System.Drawing.Size(941,510);
			this.Load += new System.EventHandler(this.UserControlTasks_Load);
			this.tabContr.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.TabControl tabContr;
		private System.Windows.Forms.TabPage tabMain;
		private System.Windows.Forms.TabPage tabRepeating;
		private System.Windows.Forms.TabPage tabDate;
		private System.Windows.Forms.TabPage tabWeek;
		private System.Windows.Forms.TabPage tabMonth;
		private System.Windows.Forms.MonthCalendar cal;
		private System.Windows.Forms.TreeView tree;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.ContextMenu menuEdit;
		private System.Windows.Forms.MenuItem menuItemEdit;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItemCut;
		private System.Windows.Forms.MenuItem menuItemCopy;
		private System.Windows.Forms.MenuItem menuItemPaste;
		private System.Windows.Forms.MenuItem menuItemDelete;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemGoto;
		private System.Windows.Forms.ImageList imageListTree;
		private System.Windows.Forms.TabPage tabUser;
		private System.Windows.Forms.MenuItem menuItemSubscribe;
		private System.Windows.Forms.MenuItem menuItemUnsubscribe;
		private System.Windows.Forms.MenuItem menuItem3;
		private OpenDental.UI.ODGrid gridMain;
	}
}
