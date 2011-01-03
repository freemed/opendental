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
			this.butWebService = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.butCore = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butSchema = new System.Windows.Forms.Button();
			this.radioSchema1 = new System.Windows.Forms.RadioButton();
			this.radioSchema2 = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// textResults
			// 
			this.textResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textResults.Location = new System.Drawing.Point(12,158);
			this.textResults.Multiline = true;
			this.textResults.Name = "textResults";
			this.textResults.Size = new System.Drawing.Size(733,637);
			this.textResults.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(347,18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Before running the tests below, make sure \'unittest\' database exists.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butNewDb
			// 
			this.butNewDb.Location = new System.Drawing.Point(12,104);
			this.butNewDb.Name = "butNewDb";
			this.butNewDb.Size = new System.Drawing.Size(75,23);
			this.butNewDb.TabIndex = 0;
			this.butNewDb.Text = "Fresh Db";
			this.butNewDb.UseVisualStyleBackColor = true;
			this.butNewDb.Click += new System.EventHandler(this.butNewDb_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(92,106);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(505,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "The scripts are all designed so that this will not normally be necessary except f" +
    "or new versions.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butRun
			// 
			this.butRun.Location = new System.Drawing.Point(12,129);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,23);
			this.butRun.TabIndex = 7;
			this.butRun.Text = "Run";
			this.butRun.UseVisualStyleBackColor = true;
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(92,131);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90,18);
			this.label3.TabIndex = 8;
			this.label3.Text = "Specific test #";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textSpecificTest
			// 
			this.textSpecificTest.Location = new System.Drawing.Point(177,131);
			this.textSpecificTest.Name = "textSpecificTest";
			this.textSpecificTest.Size = new System.Drawing.Size(74,20);
			this.textSpecificTest.TabIndex = 9;
			// 
			// butWebService
			// 
			this.butWebService.Location = new System.Drawing.Point(12,5);
			this.butWebService.Name = "butWebService";
			this.butWebService.Size = new System.Drawing.Size(75,23);
			this.butWebService.TabIndex = 10;
			this.butWebService.Text = "WebService";
			this.butWebService.UseVisualStyleBackColor = true;
			this.butWebService.Click += new System.EventHandler(this.butWebService_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(92,7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(600,18);
			this.label4.TabIndex = 11;
			this.label4.Text = "Set both this project and OpenDentalServer as startup.  Edit OpenDentalServer.Ope" +
    "nDentalServerConfig.xml to be valid.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butCore
			// 
			this.butCore.Location = new System.Drawing.Point(12,79);
			this.butCore.Name = "butCore";
			this.butCore.Size = new System.Drawing.Size(75,23);
			this.butCore.TabIndex = 12;
			this.butCore.Text = "Core Types";
			this.butCore.UseVisualStyleBackColor = true;
			this.butCore.Click += new System.EventHandler(this.butCore_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(93,82);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(547,18);
			this.label5.TabIndex = 13;
			this.label5.Text = "Stores and retrieves core data types in database, ensuring that db engine and con" +
    "nector are working.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listType
			// 
			this.listType.FormattingEnabled = true;
			this.listType.Location = new System.Drawing.Point(646,63);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(99,30);
			this.listType.TabIndex = 22;
			this.listType.SelectedIndexChanged += new System.EventHandler(this.listType_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(643,42);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(99,18);
			this.label7.TabIndex = 21;
			this.label7.Text = "Database Type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSchema
			// 
			this.butSchema.Location = new System.Drawing.Point(12,54);
			this.butSchema.Name = "butSchema";
			this.butSchema.Size = new System.Drawing.Size(75,23);
			this.butSchema.TabIndex = 23;
			this.butSchema.Text = "Schema";
			this.butSchema.UseVisualStyleBackColor = true;
			this.butSchema.Click += new System.EventHandler(this.butSchema_Click);
			// 
			// radioSchema1
			// 
			this.radioSchema1.Checked = true;
			this.radioSchema1.Location = new System.Drawing.Point(97,57);
			this.radioSchema1.Name = "radioSchema1";
			this.radioSchema1.Size = new System.Drawing.Size(133,18);
			this.radioSchema1.TabIndex = 24;
			this.radioSchema1.TabStop = true;
			this.radioSchema1.Text = "Test proposed crud";
			this.radioSchema1.UseVisualStyleBackColor = true;
			// 
			// radioSchema2
			// 
			this.radioSchema2.Location = new System.Drawing.Point(234,57);
			this.radioSchema2.Name = "radioSchema2";
			this.radioSchema2.Size = new System.Drawing.Size(189,18);
			this.radioSchema2.TabIndex = 25;
			this.radioSchema2.Text = "Compare proposed to generated";
			this.radioSchema2.UseVisualStyleBackColor = true;
			// 
			// FormUnitTests
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(757,807);
			this.Controls.Add(this.radioSchema2);
			this.Controls.Add(this.radioSchema1);
			this.Controls.Add(this.butSchema);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butCore);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butWebService);
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
		private System.Windows.Forms.Button butWebService;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button butCore;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listType;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button butSchema;
		private System.Windows.Forms.RadioButton radioSchema1;
		private System.Windows.Forms.RadioButton radioSchema2;
	}
}

