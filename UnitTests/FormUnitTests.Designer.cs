namespace UnitTests {
	partial class FormUnitTests {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUnitTests));
			this.textResults = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butNewDb = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.butRun = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textSpecificTest = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textResults
			// 
			this.textResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textResults.Location = new System.Drawing.Point(12,96);
			this.textResults.Multiline = true;
			this.textResults.Name = "textResults";
			this.textResults.Size = new System.Drawing.Size(663,699);
			this.textResults.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(347,18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Before running, make sure \'unittest\' database exists.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butNewDb
			// 
			this.butNewDb.Location = new System.Drawing.Point(12,35);
			this.butNewDb.Name = "butNewDb";
			this.butNewDb.Size = new System.Drawing.Size(75,23);
			this.butNewDb.TabIndex = 0;
			this.butNewDb.Text = "Fresh Db";
			this.butNewDb.UseVisualStyleBackColor = true;
			this.butNewDb.Click += new System.EventHandler(this.butNewDb_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(92,37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(360,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "The scripts are all designed so that this will not normally be necessary.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butRun
			// 
			this.butRun.Location = new System.Drawing.Point(12,64);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,23);
			this.butRun.TabIndex = 7;
			this.butRun.Text = "Run";
			this.butRun.UseVisualStyleBackColor = true;
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(92,66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90,18);
			this.label3.TabIndex = 8;
			this.label3.Text = "Specific test #";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textSpecificTest
			// 
			this.textSpecificTest.Location = new System.Drawing.Point(177,66);
			this.textSpecificTest.Name = "textSpecificTest";
			this.textSpecificTest.Size = new System.Drawing.Size(74,20);
			this.textSpecificTest.TabIndex = 9;
			// 
			// FormUnitTests
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(687,807);
			this.Controls.Add(this.textSpecificTest);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butNewDb);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textResults);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormUnitTests";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormUnitTests";
			this.Load += new System.EventHandler(this.FormUnitTests_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textResults;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butNewDb;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butRun;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textSpecificTest;
	}
}

