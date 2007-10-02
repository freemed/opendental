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
			this.tabMain = new System.Windows.Forms.TabPage();
			this.tabRepeating = new System.Windows.Forms.TabPage();
			this.tabDate = new System.Windows.Forms.TabPage();
			this.tabWeek = new System.Windows.Forms.TabPage();
			this.tabMonth = new System.Windows.Forms.TabPage();
			this.cal = new System.Windows.Forms.MonthCalendar();
			this.tree = new System.Windows.Forms.TreeView();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.listMain = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.menuEdit = new System.Windows.Forms.ContextMenu();
			this.menuItemEdit = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemCut = new System.Windows.Forms.MenuItem();
			this.menuItemCopy = new System.Windows.Forms.MenuItem();
			this.menuItemPaste = new System.Windows.Forms.MenuItem();
			this.menuItemDelete = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemGoto = new System.Windows.Forms.MenuItem();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.tabContr.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabContr
			// 
			this.tabContr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
			// tabMain
			// 
			this.tabMain.Location = new System.Drawing.Point(4,22);
			this.tabMain.Name = "tabMain";
			this.tabMain.Size = new System.Drawing.Size(933,0);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Main";
			// 
			// tabRepeating
			// 
			this.tabRepeating.Location = new System.Drawing.Point(4,22);
			this.tabRepeating.Name = "tabRepeating";
			this.tabRepeating.Size = new System.Drawing.Size(933,0);
			this.tabRepeating.TabIndex = 2;
			this.tabRepeating.Text = "Repeating";
			// 
			// tabDate
			// 
			this.tabDate.Location = new System.Drawing.Point(4,22);
			this.tabDate.Name = "tabDate";
			this.tabDate.Size = new System.Drawing.Size(933,0);
			this.tabDate.TabIndex = 1;
			this.tabDate.Text = "By Date";
			// 
			// tabWeek
			// 
			this.tabWeek.Location = new System.Drawing.Point(4,22);
			this.tabWeek.Name = "tabWeek";
			this.tabWeek.Size = new System.Drawing.Size(933,0);
			this.tabWeek.TabIndex = 3;
			this.tabWeek.Text = "By Week";
			// 
			// tabMonth
			// 
			this.tabMonth.Location = new System.Drawing.Point(4,22);
			this.tabMonth.Name = "tabMonth";
			this.tabMonth.Size = new System.Drawing.Size(933,0);
			this.tabMonth.TabIndex = 4;
			this.tabMonth.Text = "By Month";
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
			// 
			// listMain
			// 
			this.listMain.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.listMain.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.listMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listMain.AutoArrange = false;
			this.listMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listMain.Location = new System.Drawing.Point(0,333);
			this.listMain.MultiSelect = false;
			this.listMain.Name = "listMain";
			this.listMain.ShowItemToolTips = true;
			this.listMain.Size = new System.Drawing.Size(941,174);
			this.listMain.SmallImageList = this.imageListTree;
			this.listMain.TabIndex = 8;
			this.listMain.UseCompatibleStateImageBehavior = false;
			this.listMain.View = System.Windows.Forms.View.List;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			this.listMain.SelectedIndexChanged += new System.EventHandler(this.listMain_SelectedIndexChanged);
			this.listMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listMain_MouseDown);
			this.listMain.Click += new System.EventHandler(this.listMain_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Description";
			this.columnHeader1.Width = 600;
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
            this.menuItemGoto});
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
			// menuItemGoto
			// 
			this.menuItemGoto.Index = 7;
			this.menuItemGoto.Text = "Go To";
			this.menuItemGoto.Click += new System.EventHandler(this.menuItemGoto_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(941,29);
			this.ToolBarMain.TabIndex = 2;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// UserControlTasks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listMain);
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
		private System.Windows.Forms.ListView listMain;
		private System.Windows.Forms.ColumnHeader columnHeader1;
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
	}
}
