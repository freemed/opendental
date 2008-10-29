namespace OpenDental{
	partial class FormAnestheticMedsInventory {
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
            this.groupAnestheticMeds = new System.Windows.Forms.GroupBox();
            this.gridAnesthMedsInventory = new OpenDental.UI.ODGrid();
            this.butAdjustQtys = new OpenDental.UI.Button();
            this.labelIntakeNewMeds = new System.Windows.Forms.Label();
            this.butAnesthMedIntake = new OpenDental.UI.Button();
            this.butClose = new OpenDental.UI.Button();
            this.button1 = new OpenDental.UI.Button();
            this.butAddAnesthMeds = new OpenDental.UI.Button();
            this.groupAnestheticMeds.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupAnestheticMeds
            // 
            this.groupAnestheticMeds.Controls.Add(this.gridAnesthMedsInventory);
            this.groupAnestheticMeds.Controls.Add(this.butAdjustQtys);
            this.groupAnestheticMeds.Controls.Add(this.labelIntakeNewMeds);
            this.groupAnestheticMeds.Controls.Add(this.butAnesthMedIntake);
            this.groupAnestheticMeds.Controls.Add(this.butClose);
            this.groupAnestheticMeds.Controls.Add(this.button1);
            this.groupAnestheticMeds.Controls.Add(this.butAddAnesthMeds);
            this.groupAnestheticMeds.Location = new System.Drawing.Point(10, 48);
            this.groupAnestheticMeds.Name = "groupAnestheticMeds";
            this.groupAnestheticMeds.Size = new System.Drawing.Size(705, 439);
            this.groupAnestheticMeds.TabIndex = 4;
            this.groupAnestheticMeds.TabStop = false;
            this.groupAnestheticMeds.Text = "Anesthetic Medications";
            // 
            // gridAnesthMedsInventory
            // 
            this.gridAnesthMedsInventory.HScrollVisible = false;
            this.gridAnesthMedsInventory.Location = new System.Drawing.Point(117, 28);
            this.gridAnesthMedsInventory.Name = "gridAnesthMedsInventory";
            this.gridAnesthMedsInventory.ScrollValue = 0;
            this.gridAnesthMedsInventory.Size = new System.Drawing.Size(580, 300);
            this.gridAnesthMedsInventory.TabIndex = 144;
            this.gridAnesthMedsInventory.Title = "Anesthetic Medication Inventory";
            this.gridAnesthMedsInventory.TranslationName = "TableAnesthMedsInventory";
            // 
            // butAdjustQtys
            // 
            this.butAdjustQtys.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAdjustQtys.Autosize = true;
            this.butAdjustQtys.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAdjustQtys.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAdjustQtys.CornerRadius = 4F;
            this.butAdjustQtys.Image = global::OpenDental.Properties.Resources.Add;
            this.butAdjustQtys.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butAdjustQtys.Location = new System.Drawing.Point(106, 392);
            this.butAdjustQtys.Name = "butAdjustQtys";
            this.butAdjustQtys.Size = new System.Drawing.Size(136, 26);
            this.butAdjustQtys.TabIndex = 143;
            this.butAdjustQtys.Text = "Adjust Qty on hand";
            this.butAdjustQtys.UseVisualStyleBackColor = true;
            // 
            // labelIntakeNewMeds
            // 
            this.labelIntakeNewMeds.Location = new System.Drawing.Point(248, 360);
            this.labelIntakeNewMeds.Name = "labelIntakeNewMeds";
            this.labelIntakeNewMeds.Size = new System.Drawing.Size(272, 26);
            this.labelIntakeNewMeds.TabIndex = 142;
            this.labelIntakeNewMeds.Text = "This button should only be used after anesthetic  medications are added to the li" +
                "st above";
            // 
            // butAnesthMedIntake
            // 
            this.butAnesthMedIntake.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAnesthMedIntake.Autosize = true;
            this.butAnesthMedIntake.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAnesthMedIntake.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAnesthMedIntake.CornerRadius = 4F;
            this.butAnesthMedIntake.Image = global::OpenDental.Properties.Resources.Add;
            this.butAnesthMedIntake.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butAnesthMedIntake.Location = new System.Drawing.Point(106, 360);
            this.butAnesthMedIntake.Name = "butAnesthMedIntake";
            this.butAnesthMedIntake.Size = new System.Drawing.Size(136, 26);
            this.butAnesthMedIntake.TabIndex = 141;
            this.butAnesthMedIntake.Text = "Intake new meds";
            this.butAnesthMedIntake.UseVisualStyleBackColor = true;
            // 
            // butClose
            // 
            this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butClose.Autosize = true;
            this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butClose.CornerRadius = 4F;
            this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butClose.Location = new System.Drawing.Point(598, 360);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(90, 26);
            this.butClose.TabIndex = 140;
            this.butClose.Text = "Save and Close";
            this.butClose.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.button1.Autosize = true;
            this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.button1.CornerRadius = 4F;
            this.button1.Image = global::OpenDental.Properties.Resources.deleteX;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(526, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 26);
            this.button1.TabIndex = 139;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // butAddAnesthMeds
            // 
            this.butAddAnesthMeds.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAddAnesthMeds.Autosize = true;
            this.butAddAnesthMeds.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAddAnesthMeds.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAddAnesthMeds.CornerRadius = 4F;
            this.butAddAnesthMeds.Image = global::OpenDental.Properties.Resources.Add;
            this.butAddAnesthMeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butAddAnesthMeds.Location = new System.Drawing.Point(13, 48);
            this.butAddAnesthMeds.Name = "butAddAnesthMeds";
            this.butAddAnesthMeds.Size = new System.Drawing.Size(82, 26);
            this.butAddAnesthMeds.TabIndex = 75;
            this.butAddAnesthMeds.Text = "Add New";
            this.butAddAnesthMeds.UseVisualStyleBackColor = true;
            // 
            // AnesthMedsInventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(749, 542);
            this.Controls.Add(this.groupAnestheticMeds);
            this.Name = "AnesthMedsInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupAnestheticMeds.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.GroupBox groupAnestheticMeds;
        private OpenDental.UI.ODGrid gridAnesthMedsInventory;
        private OpenDental.UI.Button butAdjustQtys;
        private System.Windows.Forms.Label labelIntakeNewMeds;
        private OpenDental.UI.Button butAnesthMedIntake;
        private OpenDental.UI.Button butClose;
        private OpenDental.UI.Button button1;
        private OpenDental.UI.Button butAddAnesthMeds;
        

    }
}