namespace OpenDental{
	partial class FormReconcileMedication {
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
			this.gridMedExisting = new OpenDental.UI.ODGrid();
			this.butPickRxListImage = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.gridReconcileEvents = new OpenDental.UI.ODGrid();
			this.gridMedImport = new OpenDental.UI.ODGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDocDateDesc
			// 
			this.textDocDateDesc.Enabled = false;
			this.textDocDateDesc.Location = new System.Drawing.Point(101, 5);
			this.textDocDateDesc.Name = "textDocDateDesc";
			this.textDocDateDesc.Size = new System.Drawing.Size(272, 20);
			this.textDocDateDesc.TabIndex = 71;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(1, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 16);
			this.label1.TabIndex = 73;
			this.label1.Text = "Rx List";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkDiscontinued
			// 
			this.checkDiscontinued.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.checkDiscontinued.Location = new System.Drawing.Point(609, 5);
			this.checkDiscontinued.Name = "checkDiscontinued";
			this.checkDiscontinued.Size = new System.Drawing.Size(212, 23);
			this.checkDiscontinued.TabIndex = 70;
			this.checkDiscontinued.Tag = "";
			this.checkDiscontinued.Text = "Show Discontinued Medications";
			this.checkDiscontinued.UseVisualStyleBackColor = true;
			this.checkDiscontinued.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkDiscontinued_KeyUp);
			this.checkDiscontinued.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkDiscontinued_MouseUp);
			// 
			// gridMedExisting
			// 
			this.gridMedExisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMedExisting.HScrollVisible = false;
			this.gridMedExisting.Location = new System.Drawing.Point(3, 0);
			this.gridMedExisting.Name = "gridMedExisting";
			this.gridMedExisting.ScrollValue = 0;
			this.gridMedExisting.Size = new System.Drawing.Size(450, 364);
			this.gridMedExisting.TabIndex = 65;
			this.gridMedExisting.Title = "Existing Medications";
			this.gridMedExisting.TranslationName = "GridMedExisting";
			this.gridMedExisting.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMeds_CellDoubleClick);
			// 
			// butPickRxListImage
			// 
			this.butPickRxListImage.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickRxListImage.Autosize = true;
			this.butPickRxListImage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickRxListImage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickRxListImage.CornerRadius = 4F;
			this.butPickRxListImage.Location = new System.Drawing.Point(379, 3);
			this.butPickRxListImage.Name = "butPickRxListImage";
			this.butPickRxListImage.Size = new System.Drawing.Size(22, 24);
			this.butPickRxListImage.TabIndex = 76;
			this.butPickRxListImage.Text = "...";
			this.butPickRxListImage.Click += new System.EventHandler(this.butPickRxListImage_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(468, 4);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(123, 23);
			this.butAdd.TabIndex = 75;
			this.butAdd.Text = "&Add Medication";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(838, 640);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridReconcileEvents
			// 
			this.gridReconcileEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridReconcileEvents.HScrollVisible = false;
			this.gridReconcileEvents.Location = new System.Drawing.Point(4, 404);
			this.gridReconcileEvents.Name = "gridReconcileEvents";
			this.gridReconcileEvents.ScrollValue = 0;
			this.gridReconcileEvents.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridReconcileEvents.Size = new System.Drawing.Size(909, 230);
			this.gridReconcileEvents.TabIndex = 67;
			this.gridReconcileEvents.Title = "Reconciled Medications";
			this.gridReconcileEvents.TranslationName = "gridReconcile";
			// 
			// gridMedImport
			// 
			this.gridMedImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMedImport.HScrollVisible = false;
			this.gridMedImport.Location = new System.Drawing.Point(3, 3);
			this.gridMedImport.Name = "gridMedImport";
			this.gridMedImport.ScrollValue = 0;
			this.gridMedImport.Size = new System.Drawing.Size(443, 361);
			this.gridMedImport.TabIndex = 77;
			this.gridMedImport.Title = "New Medications";
			this.gridMedImport.TranslationName = "GridMedImport";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(4, 31);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gridMedImport);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gridMedExisting);
			this.splitContainer1.Size = new System.Drawing.Size(909, 367);
			this.splitContainer1.SplitterDistance = 449;
			this.splitContainer1.TabIndex = 78;
			// 
			// FormReconcileMedication
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(918, 676);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.butPickRxListImage);
			this.Controls.Add(this.gridReconcileEvents);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textDocDateDesc);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkDiscontinued);
			this.Controls.Add(this.butClose);
			this.Name = "FormReconcileMedication";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reconcile Medication";
			this.Load += new System.EventHandler(this.BasicTemplate_Load);
			this.Resize += new System.EventHandler(this.FormMedicationReconcile_Resize);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textDocDateDesc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkDiscontinued;
		private UI.ODGrid gridMedExisting;
		private UI.Button butAdd;
		private UI.Button butPickRxListImage;
		private UI.ODGrid gridReconcileEvents;
		private UI.ODGrid gridMedImport;
		private System.Windows.Forms.SplitContainer splitContainer1;
	}
}