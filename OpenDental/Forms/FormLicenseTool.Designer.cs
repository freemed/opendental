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
			this.textDescription = new System.Windows.Forms.TextBox();
			this.addButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textCode = new System.Windows.Forms.TextBox();
			this.checkcompliancebutton = new System.Windows.Forms.Button();
			this.butPrint = new System.Windows.Forms.Button();
			this.butMerge = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(113,72);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(303,20);
			this.textDescription.TabIndex = 2;
			this.textDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.description_KeyPress);
			// 
			// addButton
			// 
			this.addButton.Image = global::OpenDental.Properties.Resources.Add;
			this.addButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.addButton.Location = new System.Drawing.Point(427,67);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(79,26);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Enter += new System.EventHandler(this.addButton_Enter);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94,16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(110,54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127,16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textCode
			// 
			this.textCode.Location = new System.Drawing.Point(12,72);
			this.textCode.Name = "textCode";
			this.textCode.Size = new System.Drawing.Size(92,20);
			this.textCode.TabIndex = 1;
			this.textCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textCode_KeyPress);
			// 
			// checkcompliancebutton
			// 
			this.checkcompliancebutton.Location = new System.Drawing.Point(12,632);
			this.checkcompliancebutton.Name = "checkcompliancebutton";
			this.checkcompliancebutton.Size = new System.Drawing.Size(129,26);
			this.checkcompliancebutton.TabIndex = 6;
			this.checkcompliancebutton.Text = "Check Completeness";
			this.checkcompliancebutton.UseVisualStyleBackColor = true;
			this.checkcompliancebutton.Click += new System.EventHandler(this.checkcompliancebutton_Click);
			// 
			// butPrint
			// 
			this.butPrint.Enabled = false;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(245,632);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(108,26);
			this.butPrint.TabIndex = 7;
			this.butPrint.Text = "Print Proof";
			this.butPrint.UseVisualStyleBackColor = true;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butMerge
			// 
			this.butMerge.Enabled = false;
			this.butMerge.Location = new System.Drawing.Point(147,632);
			this.butMerge.Name = "butMerge";
			this.butMerge.Size = new System.Drawing.Size(92,26);
			this.butMerge.TabIndex = 9;
			this.butMerge.Text = "Run Tool";
			this.butMerge.UseVisualStyleBackColor = true;
			this.butMerge.Click += new System.EventHandler(this.butMerge_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(496,45);
			this.label3.TabIndex = 10;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,99);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(496,524);
			this.gridMain.TabIndex = 0;
			this.gridMain.TabStop = false;
			this.gridMain.Title = null;
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormLicenseTool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(520,667);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butMerge);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.checkcompliancebutton);
			this.Controls.Add(this.textCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.gridMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormLicenseTool";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Code Update Tool";
			this.Load += new System.EventHandler(this.FormLicenseTool_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textCode;
		private System.Windows.Forms.Button checkcompliancebutton;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.Button butMerge;
		private System.Windows.Forms.Label label3;
	}
}