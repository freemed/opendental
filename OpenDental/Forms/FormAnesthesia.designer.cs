namespace OpenDental{

	partial class FormAnesthesia{

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnesthesia));
			this.labelMain = new System.Windows.Forms.Label();
			this.groupBoxAnesthesia = new System.Windows.Forms.GroupBox();
			this.butAnestheticRecord = new OpenDental.UI.Button();
			this.butAnestheticInventory = new OpenDental.UI.Button();
			this.butReports = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBoxAnesthesia.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelMain
			// 
			this.labelMain.AutoSize = true;
			this.labelMain.Location = new System.Drawing.Point(35, 27);
			this.labelMain.Name = "labelMain";
			this.labelMain.Size = new System.Drawing.Size(0, 13);
			this.labelMain.TabIndex = 143;
			// 
			// groupBoxAnesthesia
			// 
			this.groupBoxAnesthesia.Controls.Add(this.butReports);
			this.groupBoxAnesthesia.Controls.Add(this.butAnestheticInventory);
			this.groupBoxAnesthesia.Controls.Add(this.butAnestheticRecord);
			this.groupBoxAnesthesia.Location = new System.Drawing.Point(23, 27);
			this.groupBoxAnesthesia.Name = "groupBoxAnesthesia";
			this.groupBoxAnesthesia.Size = new System.Drawing.Size(252, 121);
			this.groupBoxAnesthesia.TabIndex = 147;
			this.groupBoxAnesthesia.TabStop = false;
			this.groupBoxAnesthesia.Text = "What would you like to do?";
			// 
			// butAnestheticRecord
			// 
			this.butAnestheticRecord.Location = new System.Drawing.Point(15, 20);
			this.butAnestheticRecord.Name = "butAnestheticRecord";
			this.butAnestheticRecord.Size = new System.Drawing.Size(212, 23);
			this.butAnestheticRecord.TabIndex = 0;
			this.butAnestheticRecord.Text = "Open/Create New Anesthetic Record";
			this.butAnestheticRecord.UseVisualStyleBackColor = true;
			this.butAnestheticRecord.Click += new System.EventHandler(this.butAnestheticRecord_Click);
			// 
			// butAnestheticInventory
			// 
			this.butAnestheticInventory.Location = new System.Drawing.Point(15, 50);
			this.butAnestheticInventory.Name = "butAnestheticInventory";
			this.butAnestheticInventory.Size = new System.Drawing.Size(212, 23);
			this.butAnestheticInventory.TabIndex = 1;
			this.butAnestheticInventory.Text = "Open Anesthetic Medication Inventory";
			this.butAnestheticInventory.UseVisualStyleBackColor = true;
			this.butAnestheticInventory.Click += new System.EventHandler(this.butAnestheticInventory_Click);
			// 
			// butReports
			// 
			this.butReports.Location = new System.Drawing.Point(15, 80);
			this.butReports.Name = "butReports";
			this.butReports.Size = new System.Drawing.Size(212, 23);
			this.butReports.TabIndex = 2;
			this.butReports.Text = "Run Reports";
			this.butReports.UseVisualStyleBackColor = true;
			this.butReports.Click += new System.EventHandler(this.butReports_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCancel.Location = new System.Drawing.Point(209, 154);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(66, 26);
			this.butCancel.TabIndex = 142;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			// 
			// Anesthesia
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(303, 192);
			this.Controls.Add(this.labelMain);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.groupBoxAnesthesia);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Anesthesia";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Anesthesia & Sedation Module";
			this.groupBoxAnesthesia.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label labelMain;
		private System.Windows.Forms.GroupBox groupBoxAnesthesia;
		private OpenDental.UI.Button butAnestheticRecord;
		private OpenDental.UI.Button butAnestheticInventory;
		private OpenDental.UI.Button butReports;
	}
}