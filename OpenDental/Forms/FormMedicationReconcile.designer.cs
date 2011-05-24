namespace OpenDental{
	partial class FormMedicationReconcile {
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
			this.textDocDateDesc = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkDiscontinued = new System.Windows.Forms.CheckBox();
			this.pictBox = new System.Windows.Forms.PictureBox();
			this.gridMeds = new OpenDental.UI.ODGrid();
			this.butPickRxListImage = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.gridReconcileEvents = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.butAddEvent = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDocDateDesc
			// 
			this.textDocDateDesc.Enabled = false;
			this.textDocDateDesc.Location = new System.Drawing.Point(101,5);
			this.textDocDateDesc.Name = "textDocDateDesc";
			this.textDocDateDesc.Size = new System.Drawing.Size(272,20);
			this.textDocDateDesc.TabIndex = 71;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(1,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94,16);
			this.label1.TabIndex = 73;
			this.label1.Text = "Rx List";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkDiscontinued
			// 
			this.checkDiscontinued.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.checkDiscontinued.Location = new System.Drawing.Point(609,5);
			this.checkDiscontinued.Name = "checkDiscontinued";
			this.checkDiscontinued.Size = new System.Drawing.Size(212,23);
			this.checkDiscontinued.TabIndex = 70;
			this.checkDiscontinued.Tag = "";
			this.checkDiscontinued.Text = "Show Discontinued Medications";
			this.checkDiscontinued.UseVisualStyleBackColor = true;
			this.checkDiscontinued.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkDiscontinued_KeyUp);
			this.checkDiscontinued.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkDiscontinued_MouseUp);
			// 
			// pictBox
			// 
			this.pictBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictBox.InitialImage = null;
			this.pictBox.Location = new System.Drawing.Point(0,0);
			this.pictBox.Name = "pictBox";
			this.pictBox.Size = new System.Drawing.Size(460,600);
			this.pictBox.TabIndex = 66;
			this.pictBox.TabStop = false;
			// 
			// gridMeds
			// 
			this.gridMeds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMeds.HScrollVisible = false;
			this.gridMeds.Location = new System.Drawing.Point(0,0);
			this.gridMeds.Name = "gridMeds";
			this.gridMeds.ScrollValue = 0;
			this.gridMeds.Size = new System.Drawing.Size(445,361);
			this.gridMeds.TabIndex = 65;
			this.gridMeds.Title = "Medications";
			this.gridMeds.TranslationName = "TableMedications";
			this.gridMeds.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMeds_CellDoubleClick);
			// 
			// butPickRxListImage
			// 
			this.butPickRxListImage.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPickRxListImage.Autosize = true;
			this.butPickRxListImage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickRxListImage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickRxListImage.CornerRadius = 4F;
			this.butPickRxListImage.Location = new System.Drawing.Point(379,3);
			this.butPickRxListImage.Name = "butPickRxListImage";
			this.butPickRxListImage.Size = new System.Drawing.Size(22,24);
			this.butPickRxListImage.TabIndex = 76;
			this.butPickRxListImage.Text = "...";
			this.butPickRxListImage.Click += new System.EventHandler(this.butPickRxListImage_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(468,4);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(123,23);
			this.butAdd.TabIndex = 75;
			this.butAdd.Text = "&Add Medication";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(838,640);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(4,34);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pictBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.butAddEvent);
			this.splitContainer1.Panel2.Controls.Add(this.gridReconcileEvents);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.gridMeds);
			this.splitContainer1.Size = new System.Drawing.Size(909,600);
			this.splitContainer1.SplitterDistance = 460;
			this.splitContainer1.TabIndex = 77;
			// 
			// gridReconcileEvents
			// 
			this.gridReconcileEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridReconcileEvents.HScrollVisible = false;
			this.gridReconcileEvents.Location = new System.Drawing.Point(0,425);
			this.gridReconcileEvents.Name = "gridReconcileEvents";
			this.gridReconcileEvents.ScrollValue = 0;
			this.gridReconcileEvents.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridReconcileEvents.Size = new System.Drawing.Size(445,175);
			this.gridReconcileEvents.TabIndex = 67;
			this.gridReconcileEvents.Title = "Reconciles";
			this.gridReconcileEvents.TranslationName = "gridReconcile";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(68,394);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(309,27);
			this.label2.TabIndex = 66;
			this.label2.Text = "This is a historical record of medication reconciles for this patient.  Delete an" +
    "y entries that are inaccurate.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// butAddEvent
			// 
			this.butAddEvent.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddEvent.Autosize = true;
			this.butAddEvent.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddEvent.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddEvent.CornerRadius = 4F;
			this.butAddEvent.Location = new System.Drawing.Point(186,367);
			this.butAddEvent.Name = "butAddEvent";
			this.butAddEvent.Size = new System.Drawing.Size(75,24);
			this.butAddEvent.TabIndex = 78;
			this.butAddEvent.Text = "Add";
			this.butAddEvent.Click += new System.EventHandler(this.butAddEvent_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(468,640);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 78;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormMedicationReconcile
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(918,676);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.butPickRxListImage);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textDocDateDesc);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkDiscontinued);
			this.Controls.Add(this.butClose);
			this.Name = "FormMedicationReconcile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medication Reconcile";
			this.Load += new System.EventHandler(this.BasicTemplate_Load);
			this.Resize += new System.EventHandler(this.FormMedicationReconcile_Resize);
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textDocDateDesc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkDiscontinued;
		private System.Windows.Forms.PictureBox pictBox;
		private UI.ODGrid gridMeds;
		private UI.Button butAdd;
		private UI.Button butPickRxListImage;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label2;
		private UI.ODGrid gridReconcileEvents;
		private UI.Button butAddEvent;
		private UI.Button butDelete;
	}
}