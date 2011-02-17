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
			this.textDb = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboType = new System.Windows.Forms.ComboBox();
			this.butSnippet = new System.Windows.Forms.Button();
			this.textSnippet = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.listClass = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textDbM = new System.Windows.Forms.TextBox();
			this.checkRun = new System.Windows.Forms.CheckBox();
			this.checkRunM = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.checkRunSchema = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(406,11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(421,54);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// butRun
			// 
			this.butRun.Location = new System.Drawing.Point(868,14);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,23);
			this.butRun.TabIndex = 1;
			this.butRun.Text = "Run";
			this.butRun.UseVisualStyleBackColor = true;
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// textDb
			// 
			this.textDb.Location = new System.Drawing.Point(105,24);
			this.textDb.Name = "textDb";
			this.textDb.Size = new System.Drawing.Size(127,20);
			this.textDb.TabIndex = 5;
			this.textDb.Text = "development78";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(17,25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87,17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Database";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label2.Location = new System.Drawing.Point(160,104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(163,17);
			this.label2.TabIndex = 9;
			this.label2.Text = "Snippet Generator";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,227);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(125,17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Class";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,182);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(125,17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Type";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboType
			// 
			this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboType.FormattingEnabled = true;
			this.comboType.Location = new System.Drawing.Point(11,202);
			this.comboType.MaxDropDownItems = 100;
			this.comboType.Name = "comboType";
			this.comboType.Size = new System.Drawing.Size(144,21);
			this.comboType.TabIndex = 11;
			// 
			// butSnippet
			// 
			this.butSnippet.Location = new System.Drawing.Point(59,156);
			this.butSnippet.Name = "butSnippet";
			this.butSnippet.Size = new System.Drawing.Size(96,23);
			this.butSnippet.TabIndex = 13;
			this.butSnippet.Text = "Create Snippet";
			this.butSnippet.UseVisualStyleBackColor = true;
			this.butSnippet.Click += new System.EventHandler(this.butSnippet_Click);
			// 
			// textSnippet
			// 
			this.textSnippet.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textSnippet.Location = new System.Drawing.Point(161,124);
			this.textSnippet.Multiline = true;
			this.textSnippet.Name = "textSnippet";
			this.textSnippet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textSnippet.Size = new System.Drawing.Size(988,529);
			this.textSnippet.TabIndex = 14;
			this.textSnippet.WordWrap = false;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(1,106);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(157,43);
			this.label6.TabIndex = 15;
			this.label6.Text = "A copy of the snippet will automatically be placed on the clipboard";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listClass
			// 
			this.listClass.FormattingEnabled = true;
			this.listClass.Location = new System.Drawing.Point(11,247);
			this.listClass.Name = "listClass";
			this.listClass.Size = new System.Drawing.Size(144,407);
			this.listClass.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(-1,49);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(105,17);
			this.label7.TabIndex = 18;
			this.label7.Text = "Mobile on .196";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDbM
			// 
			this.textDbM.Location = new System.Drawing.Point(105,48);
			this.textDbM.Name = "textDbM";
			this.textDbM.Size = new System.Drawing.Size(127,20);
			this.textDbM.TabIndex = 17;
			this.textDbM.Text = "mobile_dev";
			// 
			// checkRun
			// 
			this.checkRun.Checked = true;
			this.checkRun.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRun.Location = new System.Drawing.Point(239,26);
			this.checkRun.Name = "checkRun";
			this.checkRun.Size = new System.Drawing.Size(39,17);
			this.checkRun.TabIndex = 19;
			this.checkRun.UseVisualStyleBackColor = true;
			// 
			// checkRunM
			// 
			this.checkRunM.Location = new System.Drawing.Point(239,51);
			this.checkRunM.Name = "checkRunM";
			this.checkRunM.Size = new System.Drawing.Size(39,17);
			this.checkRunM.TabIndex = 20;
			this.checkRunM.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(223,4);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48,17);
			this.label8.TabIndex = 21;
			this.label8.Text = "Run";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(59,73);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(176,17);
			this.label9.TabIndex = 22;
			this.label9.Text = "Schema (no db needed)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkRunSchema
			// 
			this.checkRunSchema.Location = new System.Drawing.Point(239,74);
			this.checkRunSchema.Name = "checkRunSchema";
			this.checkRunSchema.Size = new System.Drawing.Size(39,17);
			this.checkRunSchema.TabIndex = 23;
			this.checkRunSchema.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1161,910);
			this.Controls.Add(this.checkRunSchema);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.checkRunM);
			this.Controls.Add(this.checkRun);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textDbM);
			this.Controls.Add(this.listClass);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textSnippet);
			this.Controls.Add(this.butSnippet);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDb);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Crud Generator";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butRun;
		private System.Windows.Forms.TextBox textDb;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboType;
		private System.Windows.Forms.Button butSnippet;
		private System.Windows.Forms.TextBox textSnippet;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox listClass;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textDbM;
		private System.Windows.Forms.CheckBox checkRun;
		private System.Windows.Forms.CheckBox checkRunM;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkRunSchema;
	}
}

