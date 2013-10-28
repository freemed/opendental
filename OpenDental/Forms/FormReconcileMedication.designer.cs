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
			this.butClose = new OpenDental.UI.Button();
			this.gridReconcileEvents = new OpenDental.UI.ODGrid();
			this.gridMedImport = new OpenDental.UI.ODGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.gridMedExisting = new OpenDental.UI.ODGrid();
			this.button1 = new OpenDental.UI.Button();
			this.button2 = new OpenDental.UI.Button();
			this.button3 = new OpenDental.UI.Button();
			this.button4 = new OpenDental.UI.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
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
			this.gridMedImport.Location = new System.Drawing.Point(3, 0);
			this.gridMedImport.Name = "gridMedImport";
			this.gridMedImport.ScrollValue = 0;
			this.gridMedImport.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMedImport.Size = new System.Drawing.Size(443, 364);
			this.gridMedImport.TabIndex = 77;
			this.gridMedImport.Title = "New Medications";
			this.gridMedImport.TranslationName = "GridMedImport";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(4, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gridMedImport);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gridMedExisting);
			this.splitContainer1.Size = new System.Drawing.Size(909, 366);
			this.splitContainer1.SplitterDistance = 449;
			this.splitContainer1.TabIndex = 78;
			// 
			// gridMedExisting
			// 
			this.gridMedExisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMedExisting.HScrollVisible = false;
			this.gridMedExisting.Location = new System.Drawing.Point(3, 0);
			this.gridMedExisting.Name = "gridMedExisting";
			this.gridMedExisting.ScrollValue = 0;
			this.gridMedExisting.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMedExisting.Size = new System.Drawing.Size(430, 364);
			this.gridMedExisting.TabIndex = 65;
			this.gridMedExisting.Title = "Existing Medications";
			this.gridMedExisting.TranslationName = "GridMedExisting";
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Image = global::OpenDental.Properties.Resources.down;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(636, 379);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(51, 24);
			this.button1.TabIndex = 79;
			this.button1.Text = "Add";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button2
			// 
			this.button2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Autosize = true;
			this.button2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button2.CornerRadius = 4F;
			this.button2.Image = global::OpenDental.Properties.Resources.down;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(180, 379);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(51, 24);
			this.button2.TabIndex = 80;
			this.button2.Text = "Add";
			this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button3
			// 
			this.button3.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Autosize = true;
			this.button3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button3.CornerRadius = 4F;
			this.button3.Location = new System.Drawing.Point(757, 640);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 24);
			this.button3.TabIndex = 81;
			this.button3.Text = "&OK";
			// 
			// button4
			// 
			this.button4.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Autosize = true;
			this.button4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button4.CornerRadius = 4F;
			this.button4.Location = new System.Drawing.Point(409, 640);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(99, 24);
			this.button4.TabIndex = 82;
			this.button4.Text = "Remove Selected";
			// 
			// FormReconcileMedication
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(918, 676);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.gridReconcileEvents);
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

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridReconcileEvents;
		private UI.ODGrid gridMedImport;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private UI.ODGrid gridMedExisting;
		private UI.Button button1;
		private UI.Button button2;
		private UI.Button button3;
		private UI.Button button4;
	}
}