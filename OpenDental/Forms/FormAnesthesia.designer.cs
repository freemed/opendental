namespace OpenDental{

	partial class FormAnesthesia{

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		//private bool isNew;

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
            this.labelAnesthReports = new System.Windows.Forms.Label();
            this.labelAnesthMedInv = new System.Windows.Forms.Label();
            this.labelOpenAnesthRecord = new System.Windows.Forms.Label();
            this.butReports = new OpenDental.UI.Button();
            this.butAnestheticInventory = new OpenDental.UI.Button();
            this.butAnestheticRecord = new OpenDental.UI.Button();
            this.butClose = new OpenDental.UI.Button();
            this.butCancel = new OpenDental.UI.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.butAnesthMedSuppl = new OpenDental.UI.Button();
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
            this.groupBoxAnesthesia.Controls.Add(this.label1);
            this.groupBoxAnesthesia.Controls.Add(this.butAnesthMedSuppl);
            this.groupBoxAnesthesia.Controls.Add(this.labelAnesthReports);
            this.groupBoxAnesthesia.Controls.Add(this.labelAnesthMedInv);
            this.groupBoxAnesthesia.Controls.Add(this.labelOpenAnesthRecord);
            this.groupBoxAnesthesia.Controls.Add(this.butReports);
            this.groupBoxAnesthesia.Controls.Add(this.butAnestheticInventory);
            this.groupBoxAnesthesia.Controls.Add(this.butAnestheticRecord);
            this.groupBoxAnesthesia.Location = new System.Drawing.Point(23, 19);
            this.groupBoxAnesthesia.Name = "groupBoxAnesthesia";
            this.groupBoxAnesthesia.Size = new System.Drawing.Size(548, 206);
            this.groupBoxAnesthesia.TabIndex = 147;
            this.groupBoxAnesthesia.TabStop = false;
            this.groupBoxAnesthesia.Text = "What would you like to do?";
            // 
            // labelAnesthReports
            // 
            this.labelAnesthReports.AutoSize = true;
            this.labelAnesthReports.Location = new System.Drawing.Point(245, 151);
            this.labelAnesthReports.Name = "labelAnesthReports";
            this.labelAnesthReports.Size = new System.Drawing.Size(246, 13);
            this.labelAnesthReports.TabIndex = 5;
            this.labelAnesthReports.Text = "Print reports of delivery of anesthetic medications...";
            // 
            // labelAnesthMedInv
            // 
            this.labelAnesthMedInv.AutoSize = true;
            this.labelAnesthMedInv.Location = new System.Drawing.Point(245, 77);
            this.labelAnesthMedInv.Name = "labelAnesthMedInv";
            this.labelAnesthMedInv.Size = new System.Drawing.Size(292, 13);
            this.labelAnesthMedInv.TabIndex = 4;
            this.labelAnesthMedInv.Text = "Setup and control your inventory of anesthetic medications...";
            // 
            // labelOpenAnesthRecord
            // 
            this.labelOpenAnesthRecord.AutoSize = true;
            this.labelOpenAnesthRecord.Location = new System.Drawing.Point(245, 39);
            this.labelOpenAnesthRecord.Name = "labelOpenAnesthRecord";
            this.labelOpenAnesthRecord.Size = new System.Drawing.Size(223, 13);
            this.labelOpenAnesthRecord.TabIndex = 3;
            this.labelOpenAnesthRecord.Text = "Create a new anesthetic record for a patient...";
            // 
            // butReports
            // 
            this.butReports.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butReports.Autosize = true;
            this.butReports.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butReports.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butReports.CornerRadius = 4F;
            this.butReports.Location = new System.Drawing.Point(15, 145);
            this.butReports.Name = "butReports";
            this.butReports.Size = new System.Drawing.Size(212, 26);
            this.butReports.TabIndex = 2;
            this.butReports.Text = "Run Reports";
            this.butReports.UseVisualStyleBackColor = true;
            this.butReports.Click += new System.EventHandler(this.butReports_Click);
            // 
            // butAnestheticInventory
            // 
            this.butAnestheticInventory.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAnestheticInventory.Autosize = true;
            this.butAnestheticInventory.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAnestheticInventory.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAnestheticInventory.CornerRadius = 4F;
            this.butAnestheticInventory.Location = new System.Drawing.Point(15, 70);
            this.butAnestheticInventory.Name = "butAnestheticInventory";
            this.butAnestheticInventory.Size = new System.Drawing.Size(212, 26);
            this.butAnestheticInventory.TabIndex = 1;
            this.butAnestheticInventory.Text = "Open Anesthetic Medication Inventory";
            this.butAnestheticInventory.UseVisualStyleBackColor = true;
            this.butAnestheticInventory.Click += new System.EventHandler(this.butAnestheticInventory_Click);
            // 
            // butAnestheticRecord
            // 
            this.butAnestheticRecord.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAnestheticRecord.Autosize = true;
            this.butAnestheticRecord.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAnestheticRecord.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAnestheticRecord.CornerRadius = 4F;
            this.butAnestheticRecord.Location = new System.Drawing.Point(15, 32);
            this.butAnestheticRecord.Name = "butAnestheticRecord";
            this.butAnestheticRecord.Size = new System.Drawing.Size(212, 26);
            this.butAnestheticRecord.TabIndex = 0;
            this.butAnestheticRecord.Text = "Open/Create New Anesthetic Record";
            this.butAnestheticRecord.UseVisualStyleBackColor = true;
            this.butAnestheticRecord.Click += new System.EventHandler(this.butAnestheticRecord_Click);
            // 
            // butClose
            // 
            this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butClose.Autosize = true;
            this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butClose.CornerRadius = 4F;
            this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butClose.Location = new System.Drawing.Point(494, 253);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(66, 26);
            this.butClose.TabIndex = 148;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
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
            this.butCancel.Location = new System.Drawing.Point(420, 253);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(66, 26);
            this.butCancel.TabIndex = 142;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Add, Edit or Lookup an Anesthetic Medication Supplier...";
            // 
            // butAnesthMedSuppl
            // 
            this.butAnesthMedSuppl.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAnesthMedSuppl.Autosize = true;
            this.butAnesthMedSuppl.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAnesthMedSuppl.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAnesthMedSuppl.CornerRadius = 4F;
            this.butAnesthMedSuppl.Location = new System.Drawing.Point(15, 107);
            this.butAnesthMedSuppl.Name = "butAnesthMedSuppl";
            this.butAnesthMedSuppl.Size = new System.Drawing.Size(212, 26);
            this.butAnesthMedSuppl.TabIndex = 6;
            this.butAnesthMedSuppl.Text = "Suppliers";
            this.butAnesthMedSuppl.UseVisualStyleBackColor = true;
            this.butAnesthMedSuppl.Click += new System.EventHandler(this.butAnesthMedSuppl_Click);
            // 
            // FormAnesthesia
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(593, 296);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.labelMain);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.groupBoxAnesthesia);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAnesthesia";
            this.Text = "Anesthesia Module";
            this.groupBoxAnesthesia.ResumeLayout(false);
            this.groupBoxAnesthesia.PerformLayout();
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
		private System.Windows.Forms.Label labelAnesthReports;
		private System.Windows.Forms.Label labelAnesthMedInv;
		private System.Windows.Forms.Label labelOpenAnesthRecord;
		private OpenDental.UI.Button butClose;
        private System.Windows.Forms.Label label1;
        private OpenDental.UI.Button butAnesthMedSuppl;
	}
}