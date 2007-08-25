namespace DocumentationBuilder {
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
			this.butBuild = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textVersion = new System.Windows.Forms.TextBox();
			this.textConnStr = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(23,25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(558,41);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// butBuild
			// 
			this.butBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butBuild.Location = new System.Drawing.Point(477,279);
			this.butBuild.Name = "butBuild";
			this.butBuild.Size = new System.Drawing.Size(75,24);
			this.butBuild.TabIndex = 1;
			this.butBuild.Text = "Build";
			this.butBuild.UseVisualStyleBackColor = true;
			this.butBuild.Click += new System.EventHandler(this.butBuild_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(23,84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(558,41);
			this.label2.TabIndex = 2;
			this.label2.Text = "Step 1: Build the release, which also generates OpenDentBusiness.xml which contai" +
    "ns all the comments for each database column.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(23,129);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(558,33);
			this.label3.TabIndex = 3;
			this.label3.Text = "Step 2: Make sure you have run the exe so that the config file points to a runnin" +
    "g database of the same version as the program.  Connection string:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(23,224);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(558,41);
			this.label4.TabIndex = 4;
			this.label4.Text = "Step 4: Build.  The output file is DocumentationBuilder/OpenDentalDocumentation.x" +
    "ml, which will be automatically launched when done.  Approximate time to complet" +
    "e is 5 seconds on a fast computer.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(23,194);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(140,19);
			this.label6.TabIndex = 6;
			this.label6.Text = "Step 3: Specify the version:";
			// 
			// textVersion
			// 
			this.textVersion.Location = new System.Drawing.Point(160,191);
			this.textVersion.Name = "textVersion";
			this.textVersion.Size = new System.Drawing.Size(59,20);
			this.textVersion.TabIndex = 7;
			this.textVersion.Text = "5.1.0";
			// 
			// textConnStr
			// 
			this.textConnStr.Location = new System.Drawing.Point(26,158);
			this.textConnStr.Name = "textConnStr";
			this.textConnStr.ReadOnly = true;
			this.textConnStr.Size = new System.Drawing.Size(541,20);
			this.textConnStr.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612,332);
			this.Controls.Add(this.textConnStr);
			this.Controls.Add(this.textVersion);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butBuild);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Documentation Builder";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butBuild;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textVersion;
		private System.Windows.Forms.TextBox textConnStr;
	}
}

