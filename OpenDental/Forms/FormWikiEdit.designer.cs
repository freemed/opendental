namespace OpenDental{
	partial class FormWikiEdit {
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
			this.textContent = new System.Windows.Forms.TextBox();
			this.textKeyWords = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// textContent
			// 
			this.textContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textContent.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textContent.Location = new System.Drawing.Point(0, 48);
			this.textContent.Multiline = true;
			this.textContent.Name = "textContent";
			this.textContent.Size = new System.Drawing.Size(784, 539);
			this.textContent.TabIndex = 2;
			// 
			// textKeyWords
			// 
			this.textKeyWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textKeyWords.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textKeyWords.Location = new System.Drawing.Point(149, 26);
			this.textKeyWords.Multiline = true;
			this.textKeyWords.Name = "textKeyWords";
			this.textKeyWords.Size = new System.Drawing.Size(635, 20);
			this.textKeyWords.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(131, 20);
			this.label2.TabIndex = 76;
			this.label2.Text = "Key Words";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = null;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(784, 25);
			this.ToolBarMain.TabIndex = 3;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormWikiEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(784, 587);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textKeyWords);
			this.Controls.Add(this.textContent);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "FormWikiEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Wiki Edit";
			this.Load += new System.EventHandler(this.FormWikiEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.TextBox textContent;
		private System.Windows.Forms.TextBox textKeyWords;
		private System.Windows.Forms.Label label2;

	}
}