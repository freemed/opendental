namespace OpenDental {
	partial class FormNewCropBilling {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewCropBilling));
			this.textBoxHTML = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butPaste = new OpenDental.UI.Button();
			this.butGo = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textBoxHTML
			// 
			this.textBoxHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxHTML.Location = new System.Drawing.Point(12,23);
			this.textBoxHTML.Multiline = true;
			this.textBoxHTML.Name = "textBoxHTML";
			this.textBoxHTML.ReadOnly = true;
			this.textBoxHTML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxHTML.Size = new System.Drawing.Size(958,639);
			this.textBoxHTML.TabIndex = 0;
			this.textBoxHTML.WordWrap = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(957,16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Provider List NewCrop HTML (manually paste into box below using paste button at b" +
    "ottom)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butPaste
			// 
			this.butPaste.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPaste.Autosize = true;
			this.butPaste.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPaste.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPaste.CornerRadius = 4F;
			this.butPaste.Location = new System.Drawing.Point(453,667);
			this.butPaste.Name = "butPaste";
			this.butPaste.Size = new System.Drawing.Size(75,23);
			this.butPaste.TabIndex = 4;
			this.butPaste.Text = "Paste";
			this.butPaste.UseVisualStyleBackColor = true;
			this.butPaste.Click += new System.EventHandler(this.butPaste_Click);
			// 
			// butGo
			// 
			this.butGo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butGo.Autosize = true;
			this.butGo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGo.CornerRadius = 4F;
			this.butGo.Location = new System.Drawing.Point(895,667);
			this.butGo.Name = "butGo";
			this.butGo.Size = new System.Drawing.Size(75,23);
			this.butGo.TabIndex = 5;
			this.butGo.Text = "Go";
			this.butGo.UseVisualStyleBackColor = true;
			this.butGo.Click += new System.EventHandler(this.butGo_Click);
			// 
			// FormNewCropBilling
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(982,707);
			this.Controls.Add(this.butGo);
			this.Controls.Add(this.butPaste);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxHTML);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(950,700);
			this.Name = "FormNewCropBilling";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NewCrop Billing";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxHTML;
		private System.Windows.Forms.Label label1;
		private UI.Button butPaste;
		private UI.Button butGo;
	}
}

