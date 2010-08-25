namespace OpenDental{
	partial class FormPatFieldDuplicatesFix {
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelCount = new System.Windows.Forms.Label();
			this.labelInstructions = new System.Windows.Forms.Label();
			this.butClear = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7,45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189,20);
			this.label1.TabIndex = 4;
			this.label1.Text = "Duplicate patient fields found:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(48,16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(380,20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Duplicate patient fields can cause database issues.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCount
			// 
			this.labelCount.Location = new System.Drawing.Point(202,49);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(100,23);
			this.labelCount.TabIndex = 6;
			this.labelCount.Text = "100000";
			// 
			// labelInstructions
			// 
			this.labelInstructions.Location = new System.Drawing.Point(48,82);
			this.labelInstructions.Name = "labelInstructions";
			this.labelInstructions.Size = new System.Drawing.Size(325,41);
			this.labelInstructions.TabIndex = 7;
			this.labelInstructions.Text = "Click the Clear button to fix the duplicates.  You should not have to run this to" +
    "ol again in the future.";
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Location = new System.Drawing.Point(255,149);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,24);
			this.butClear.TabIndex = 3;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(354,149);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormPatFieldDuplicatesFix
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(454,196);
			this.Controls.Add(this.labelInstructions);
			this.Controls.Add(this.labelCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.butClose);
			this.Name = "FormPatFieldDuplicatesFix";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Clear Duplicate Patient Fields";
			this.Load += new System.EventHandler(this.FormPatFieldDuplicatesFix_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butClear;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelCount;
		private System.Windows.Forms.Label labelInstructions;
	}
}