namespace OpenDental{
	partial class FormDatabaseMaintTemp {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseMaintTemp));
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.comboDbs = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textResults = new System.Windows.Forms.TextBox();
			this.butPrint = new OpenDental.UI.Button();
			this.butRun = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(97,43);
			this.linkLabel1.Location = new System.Drawing.Point(12,11);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(671,51);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(300,18);
			this.label1.TabIndex = 6;
			this.label1.Text = "Step 1.  Choose the most recent backup of this database.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboDbs
			// 
			this.comboDbs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDbs.FormattingEnabled = true;
			this.comboDbs.Location = new System.Drawing.Point(307,68);
			this.comboDbs.MaxDropDownItems = 30;
			this.comboDbs.Name = "comboDbs";
			this.comboDbs.Size = new System.Drawing.Size(317,21);
			this.comboDbs.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90,18);
			this.label2.TabIndex = 8;
			this.label2.Text = "Step 2.  Run";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,134);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(138,18);
			this.label3.TabIndex = 9;
			this.label3.Text = "Step 3.  View results";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12,593);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(138,18);
			this.label4.TabIndex = 10;
			this.label4.Text = "Step 4.  Print results";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textResults
			// 
			this.textResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textResults.Location = new System.Drawing.Point(15,155);
			this.textResults.Multiline = true;
			this.textResults.Name = "textResults";
			this.textResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textResults.Size = new System.Drawing.Size(706,421);
			this.textResults.TabIndex = 11;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(129,591);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,24);
			this.butPrint.TabIndex = 12;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butRun
			// 
			this.butRun.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRun.Autosize = true;
			this.butRun.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRun.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRun.CornerRadius = 4F;
			this.butRun.Location = new System.Drawing.Point(87,101);
			this.butRun.Name = "butRun";
			this.butRun.Size = new System.Drawing.Size(75,24);
			this.butRun.TabIndex = 3;
			this.butRun.Text = "Run";
			this.butRun.Click += new System.EventHandler(this.butRun_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(646,591);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormDatabaseMaintTemp
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(733,627);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textResults);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butRun);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboDbs);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.butCancel);
			this.Name = "FormDatabaseMaintTemp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Temporary Database Integrity Check";
			this.Load += new System.EventHandler(this.FormDatabaseMaintTemp_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butRun;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboDbs;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textResults;
		private OpenDental.UI.Button butPrint;
	}
}