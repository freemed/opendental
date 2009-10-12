namespace OpenDental{
	partial class FormReplicationSetup {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReplicationSetup));
			this.checkRandomPrimaryKeys = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butTest = new OpenDental.UI.Button();
			this.butSetRanges = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// checkRandomPrimaryKeys
			// 
			this.checkRandomPrimaryKeys.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRandomPrimaryKeys.Location = new System.Drawing.Point(17,15);
			this.checkRandomPrimaryKeys.Name = "checkRandomPrimaryKeys";
			this.checkRandomPrimaryKeys.Size = new System.Drawing.Size(346,17);
			this.checkRandomPrimaryKeys.TabIndex = 56;
			this.checkRandomPrimaryKeys.Text = "Use Random Primary Keys (BE VERY CAREFUL.  IRREVERSIBLE)";
			this.checkRandomPrimaryKeys.Click += new System.EventHandler(this.checkRandomPrimaryKeys_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(113,506);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(451,45);
			this.label1.TabIndex = 61;
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(113,563);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(451,45);
			this.label2.TabIndex = 63;
			this.label2.Text = "Use the Test button to generate and display some sample key values for this compu" +
    "ter.  The displayed values should fall within the range set above.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butTest
			// 
			this.butTest.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTest.Autosize = true;
			this.butTest.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTest.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTest.CornerRadius = 4F;
			this.butTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTest.Location = new System.Drawing.Point(17,572);
			this.butTest.Name = "butTest";
			this.butTest.Size = new System.Drawing.Size(90,24);
			this.butTest.TabIndex = 62;
			this.butTest.Text = "Test";
			this.butTest.Click += new System.EventHandler(this.butTest_Click);
			// 
			// butSetRanges
			// 
			this.butSetRanges.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetRanges.Autosize = true;
			this.butSetRanges.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetRanges.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetRanges.CornerRadius = 4F;
			this.butSetRanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSetRanges.Location = new System.Drawing.Point(17,516);
			this.butSetRanges.Name = "butSetRanges";
			this.butSetRanges.Size = new System.Drawing.Size(90,24);
			this.butSetRanges.TabIndex = 59;
			this.butSetRanges.Text = "Set Ranges";
			this.butSetRanges.Click += new System.EventHandler(this.butSetRanges_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(798,470);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,24);
			this.butAdd.TabIndex = 58;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,42);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(764,452);
			this.gridMain.TabIndex = 57;
			this.gridMain.Title = "Servers";
			this.gridMain.TranslationName = "FormReplicationSetup";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(798,576);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormReplicationSetup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(885,614);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butTest);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butSetRanges);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.checkRandomPrimaryKeys);
			this.Controls.Add(this.butClose);
			this.Name = "FormReplicationSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Replication Setup";
			this.Load += new System.EventHandler(this.FormReplicationSetup_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormReplicationSetup_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.CheckBox checkRandomPrimaryKeys;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butSetRanges;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butTest;
		private System.Windows.Forms.Label label2;
	}
}