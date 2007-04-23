namespace OpenDental {
	partial class FormLicenseTool {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicenseTool));
			this.description = new System.Windows.Forms.TextBox();
			this.addButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.adacode = new System.Windows.Forms.TextBox();
			this.codeGrid = new OpenDental.UI.ODGrid();
			this.checkcompliancebutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// description
			// 
			this.description.Location = new System.Drawing.Point(14,70);
			this.description.Name = "description";
			this.description.Size = new System.Drawing.Size(201,20);
			this.description.TabIndex = 2;
			this.description.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.description_KeyPress);
			// 
			// addButton
			// 
			this.addButton.Image = global::OpenDental.Properties.Resources.Add;
			this.addButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.addButton.Location = new System.Drawing.Point(255,61);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(79,29);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Enter += new System.EventHandler(this.addButton_Enter);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57,13);
			this.label1.TabIndex = 4;
			this.label1.Text = "ADA Code";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17,52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60,13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Description";
			// 
			// adacode
			// 
			this.adacode.Location = new System.Drawing.Point(14,26);
			this.adacode.Name = "adacode";
			this.adacode.Size = new System.Drawing.Size(201,20);
			this.adacode.TabIndex = 1;
			this.adacode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.adacode_KeyPress);
			// 
			// codeGrid
			// 
			this.codeGrid.HScrollVisible = false;
			this.codeGrid.Location = new System.Drawing.Point(12,110);
			this.codeGrid.Name = "codeGrid";
			this.codeGrid.ScrollValue = 0;
			this.codeGrid.Size = new System.Drawing.Size(496,429);
			this.codeGrid.TabIndex = 0;
			this.codeGrid.TabStop = false;
			this.codeGrid.Title = null;
			this.codeGrid.TranslationName = null;
			this.codeGrid.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.codeGrid_CellDoubleClick);
			// 
			// checkcompliancebutton
			// 
			this.checkcompliancebutton.Location = new System.Drawing.Point(190,545);
			this.checkcompliancebutton.Name = "checkcompliancebutton";
			this.checkcompliancebutton.Size = new System.Drawing.Size(129,29);
			this.checkcompliancebutton.TabIndex = 6;
			this.checkcompliancebutton.Text = "Check Compliance";
			this.checkcompliancebutton.UseVisualStyleBackColor = true;
			this.checkcompliancebutton.Click += new System.EventHandler(this.checkcompliancebutton_Click);
			// 
			// FormLicenseTool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(520,605);
			this.Controls.Add(this.checkcompliancebutton);
			this.Controls.Add(this.adacode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.description);
			this.Controls.Add(this.codeGrid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormLicenseTool";
			this.Text = "License Tool";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.ODGrid codeGrid;
		private System.Windows.Forms.TextBox description;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox adacode;
		private System.Windows.Forms.Button checkcompliancebutton;
	}
}