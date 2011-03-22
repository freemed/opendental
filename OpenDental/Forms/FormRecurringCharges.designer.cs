namespace OpenDental{
	partial class FormRecurringCharges {
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
			this.groupCounts = new System.Windows.Forms.GroupBox();
			this.labelFailed = new System.Windows.Forms.Label();
			this.labelCharged = new System.Windows.Forms.Label();
			this.labelSelected = new System.Windows.Forms.Label();
			this.labelTotal = new System.Windows.Forms.Label();
			this.butNone = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butPrintList = new OpenDental.UI.Button();
			this.groupCounts.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupCounts
			// 
			this.groupCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupCounts.Controls.Add(this.labelFailed);
			this.groupCounts.Controls.Add(this.labelCharged);
			this.groupCounts.Controls.Add(this.labelSelected);
			this.groupCounts.Controls.Add(this.labelTotal);
			this.groupCounts.Location = new System.Drawing.Point(617,196);
			this.groupCounts.Name = "groupCounts";
			this.groupCounts.Size = new System.Drawing.Size(96,103);
			this.groupCounts.TabIndex = 34;
			this.groupCounts.TabStop = false;
			this.groupCounts.Text = "Counts";
			// 
			// labelFailed
			// 
			this.labelFailed.Location = new System.Drawing.Point(4,76);
			this.labelFailed.Name = "labelFailed";
			this.labelFailed.Size = new System.Drawing.Size(89,16);
			this.labelFailed.TabIndex = 32;
			this.labelFailed.Text = "Failed=20";
			this.labelFailed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCharged
			// 
			this.labelCharged.Location = new System.Drawing.Point(4,57);
			this.labelCharged.Name = "labelCharged";
			this.labelCharged.Size = new System.Drawing.Size(89,16);
			this.labelCharged.TabIndex = 31;
			this.labelCharged.Text = "Charged=20";
			this.labelCharged.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelSelected
			// 
			this.labelSelected.Location = new System.Drawing.Point(4,38);
			this.labelSelected.Name = "labelSelected";
			this.labelSelected.Size = new System.Drawing.Size(89,16);
			this.labelSelected.TabIndex = 30;
			this.labelSelected.Text = "Selected=20";
			this.labelSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelTotal
			// 
			this.labelTotal.Location = new System.Drawing.Point(4,19);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(89,16);
			this.labelTotal.TabIndex = 29;
			this.labelTotal.Text = "Total=20";
			this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			this.butNone.Location = new System.Drawing.Point(102,498);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(75,24);
			this.butNone.TabIndex = 42;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(12,498);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75,24);
			this.butAll.TabIndex = 41;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(627,12);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,24);
			this.butRefresh.TabIndex = 40;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.AutoScroll = true;
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(592,474);
			this.gridMain.TabIndex = 29;
			this.gridMain.Title = "Recurring Charges";
			this.gridMain.TranslationName = "TableRecurring";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(627,462);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75,24);
			this.butSend.TabIndex = 3;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(627,498);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butPrintList
			// 
			this.butPrintList.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrintList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPrintList.Autosize = true;
			this.butPrintList.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintList.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintList.CornerRadius = 4F;
			this.butPrintList.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrintList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintList.Location = new System.Drawing.Point(318,498);
			this.butPrintList.Name = "butPrintList";
			this.butPrintList.Size = new System.Drawing.Size(88,24);
			this.butPrintList.TabIndex = 43;
			this.butPrintList.Text = "Print List";
			this.butPrintList.Click += new System.EventHandler(this.butPrintList_Click);
			// 
			// FormRecurringCharges
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(725,534);
			this.Controls.Add(this.butPrintList);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.groupCounts);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Name = "FormRecurringCharges";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recurring Charges";
			this.Load += new System.EventHandler(this.FormRecurringCharges_Load);
			this.groupCounts.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butSend;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.GroupBox groupCounts;
		private System.Windows.Forms.Label labelFailed;
		private System.Windows.Forms.Label labelCharged;
		private System.Windows.Forms.Label labelSelected;
		private System.Windows.Forms.Label labelTotal;
		private UI.Button butRefresh;
		private UI.Button butNone;
		private UI.Button butAll;
		private UI.Button butPrintList;
	}
}