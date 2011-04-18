namespace OpenDental {
	partial class FormFormularies {
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
			this.butAdd = new System.Windows.Forms.Button();
			this.butClose = new System.Windows.Forms.Button();
			this.listMain = new System.Windows.Forms.ListBox();
			this.labelSelect = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butAdd
			// 
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Location = new System.Drawing.Point(28,268);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,23);
			this.butAdd.TabIndex = 1;
			this.butAdd.Text = "Add";
			this.butAdd.UseVisualStyleBackColor = true;
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Location = new System.Drawing.Point(227,268);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,23);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// listMain
			// 
			this.listMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listMain.Location = new System.Drawing.Point(28,44);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(274,199);
			this.listMain.TabIndex = 16;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// labelSelect
			// 
			this.labelSelect.Location = new System.Drawing.Point(25,23);
			this.labelSelect.Name = "labelSelect";
			this.labelSelect.Size = new System.Drawing.Size(211,16);
			this.labelSelect.TabIndex = 74;
			this.labelSelect.Text = "Please select a formulary";
			this.labelSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormFormularies
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333,313);
			this.Controls.Add(this.labelSelect);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Name = "FormFormularies";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Formularies";
			this.Load += new System.EventHandler(this.FormFormularies_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.ListBox listMain;
		private System.Windows.Forms.Label labelSelect;


	}
}