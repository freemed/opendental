namespace Crud {
	partial class Form1 {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.label1 = new System.Windows.Forms.Label();
			this.butRun = new System.Windows.Forms.Button();
			this.butClear = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.checkOne = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(37,29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(350,56);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// butRun
			// 
			this.butRun.Location = new System.Drawing.Point(179,88);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,23);
			this.butRun.TabIndex = 1;
			this.butRun.Text = "Run";
			this.butRun.UseVisualStyleBackColor = true;
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// butClear
			// 
			this.butClear.Location = new System.Drawing.Point(179,259);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,23);
			this.butClear.TabIndex = 3;
			this.butClear.Text = "Clear";
			this.butClear.UseVisualStyleBackColor = true;
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(37,218);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(350,41);
			this.label2.TabIndex = 2;
			this.label2.Text = "If OpenDentBusiness won\'t compile due to broken Crud layer, then remove ODB from " +
    "config manager.  Then, use the clear button to clear all Crud files.";
			// 
			// checkOne
			// 
			this.checkOne.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkOne.Checked = true;
			this.checkOne.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkOne.Location = new System.Drawing.Point(40,142);
			this.checkOne.Name = "checkOne";
			this.checkOne.Size = new System.Drawing.Size(372,47);
			this.checkOne.TabIndex = 4;
			this.checkOne.Text = resources.GetString("checkOne.Text");
			this.checkOne.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkOne.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(440,308);
			this.Controls.Add(this.checkOne);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butRun;
		private System.Windows.Forms.Button butClear;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkOne;
	}
}

