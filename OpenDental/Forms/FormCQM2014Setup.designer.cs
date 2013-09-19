namespace OpenDental{
	partial class FormCQM2014Setup {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCQM2014Setup));
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listRecommendCodes = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupDefaultEncCode = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textCodeValue = new System.Windows.Forms.TextBox();
			this.butHcpcs = new OpenDental.UI.Button();
			this.butSnomed = new OpenDental.UI.Button();
			this.butCdtCpt = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.button1 = new OpenDental.UI.Button();
			this.textCodeDescript = new OpenDental.ODtextBox();
			this.labelEncounterWarning = new System.Windows.Forms.Label();
			this.groupDefaultEncCode.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(507, 55);
			this.label1.TabIndex = 4;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(105, 77);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(115, 17);
			this.label4.TabIndex = 110;
			this.label4.Text = "Recommended Codes";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 234);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 17);
			this.label2.TabIndex = 109;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listRecommendCodes
			// 
			this.listRecommendCodes.FormattingEnabled = true;
			this.listRecommendCodes.Location = new System.Drawing.Point(118, 96);
			this.listRecommendCodes.Name = "listRecommendCodes";
			this.listRecommendCodes.Size = new System.Drawing.Size(85, 134);
			this.listRecommendCodes.TabIndex = 111;
			this.listRecommendCodes.Click += new System.EventHandler(this.listRecommendEncCodes_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(303, 130);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(205, 42);
			this.label3.TabIndex = 113;
			this.label3.Text = "Choosing a code not in the recommended list might make it more difficult to incre" +
    "ase your CQM percentages.";
			// 
			// groupDefaultEncCode
			// 
			this.groupDefaultEncCode.Controls.Add(this.labelEncounterWarning);
			this.groupDefaultEncCode.Controls.Add(this.label6);
			this.groupDefaultEncCode.Controls.Add(this.label5);
			this.groupDefaultEncCode.Controls.Add(this.textCodeValue);
			this.groupDefaultEncCode.Controls.Add(this.textCodeDescript);
			this.groupDefaultEncCode.Controls.Add(this.label2);
			this.groupDefaultEncCode.Controls.Add(this.butHcpcs);
			this.groupDefaultEncCode.Controls.Add(this.butSnomed);
			this.groupDefaultEncCode.Controls.Add(this.butCdtCpt);
			this.groupDefaultEncCode.Controls.Add(this.label1);
			this.groupDefaultEncCode.Controls.Add(this.label4);
			this.groupDefaultEncCode.Controls.Add(this.listRecommendCodes);
			this.groupDefaultEncCode.Controls.Add(this.label3);
			this.groupDefaultEncCode.Location = new System.Drawing.Point(12, 12);
			this.groupDefaultEncCode.Name = "groupDefaultEncCode";
			this.groupDefaultEncCode.Size = new System.Drawing.Size(528, 302);
			this.groupDefaultEncCode.TabIndex = 118;
			this.groupDefaultEncCode.TabStop = false;
			this.groupDefaultEncCode.Text = "Default Encounter Code";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(13, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(102, 43);
			this.label6.TabIndex = 128;
			this.label6.Text = "Selecting \'none\' will disable automatic encounters.";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(269, 108);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(78, 17);
			this.label5.TabIndex = 127;
			this.label5.Text = "Code";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeValue
			// 
			this.textCodeValue.Location = new System.Drawing.Point(350, 107);
			this.textCodeValue.Name = "textCodeValue";
			this.textCodeValue.ReadOnly = true;
			this.textCodeValue.Size = new System.Drawing.Size(158, 20);
			this.textCodeValue.TabIndex = 126;
			// 
			// butHcpcs
			// 
			this.butHcpcs.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHcpcs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butHcpcs.Autosize = true;
			this.butHcpcs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHcpcs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHcpcs.CornerRadius = 4F;
			this.butHcpcs.Location = new System.Drawing.Point(352, 77);
			this.butHcpcs.Name = "butHcpcs";
			this.butHcpcs.Size = new System.Drawing.Size(75, 24);
			this.butHcpcs.TabIndex = 124;
			this.butHcpcs.Text = "HCPCS";
			this.butHcpcs.Click += new System.EventHandler(this.butHcpcs_Click);
			// 
			// butSnomed
			// 
			this.butSnomed.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSnomed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSnomed.Autosize = true;
			this.butSnomed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSnomed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSnomed.CornerRadius = 4F;
			this.butSnomed.Location = new System.Drawing.Point(269, 77);
			this.butSnomed.Name = "butSnomed";
			this.butSnomed.Size = new System.Drawing.Size(77, 24);
			this.butSnomed.TabIndex = 125;
			this.butSnomed.Text = "SNOMED CT";
			this.butSnomed.Click += new System.EventHandler(this.butSnomed_Click);
			// 
			// butCdtCpt
			// 
			this.butCdtCpt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCdtCpt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCdtCpt.Autosize = true;
			this.butCdtCpt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCdtCpt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCdtCpt.CornerRadius = 4F;
			this.butCdtCpt.Location = new System.Drawing.Point(433, 77);
			this.butCdtCpt.Name = "butCdtCpt";
			this.butCdtCpt.Size = new System.Drawing.Size(75, 24);
			this.butCdtCpt.TabIndex = 123;
			this.butCdtCpt.Text = "CDT/CPT";
			this.butCdtCpt.Click += new System.EventHandler(this.butCdtCpt_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(763, 616);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 121;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(844, 616);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 24);
			this.button1.TabIndex = 122;
			this.button1.Text = "&Cancel";
			this.button1.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textCodeDescript
			// 
			this.textCodeDescript.AcceptsTab = true;
			this.textCodeDescript.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.textCodeDescript.DetectUrls = false;
			this.textCodeDescript.Location = new System.Drawing.Point(118, 234);
			this.textCodeDescript.Name = "textCodeDescript";
			this.textCodeDescript.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textCodeDescript.ReadOnly = true;
			this.textCodeDescript.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textCodeDescript.Size = new System.Drawing.Size(404, 62);
			this.textCodeDescript.TabIndex = 108;
			this.textCodeDescript.Text = "";
			// 
			// labelEncounterWarning
			// 
			this.labelEncounterWarning.ForeColor = System.Drawing.Color.Red;
			this.labelEncounterWarning.Location = new System.Drawing.Point(224, 185);
			this.labelEncounterWarning.Name = "labelEncounterWarning";
			this.labelEncounterWarning.Size = new System.Drawing.Size(293, 45);
			this.labelEncounterWarning.TabIndex = 129;
			this.labelEncounterWarning.Text = "Warning: In order for patients to be considered for CQM calculations, you will ha" +
    "ve to manually create encounters with a qualified code specific to each measure." +
    "";
			// 
			// FormCQM2014Setup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(931, 652);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupDefaultEncCode);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormCQM2014Setup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Clinical Quality Measures (CQM) Setup";
			this.Load += new System.EventHandler(this.FormCQM2014Setup_Load);
			this.groupDefaultEncCode.ResumeLayout(false);
			this.groupDefaultEncCode.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private ODtextBox textCodeDescript;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listRecommendCodes;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupDefaultEncCode;
		private UI.Button butHcpcs;
		private UI.Button butSnomed;
		private UI.Button butCdtCpt;
		private UI.Button butOK;
		private UI.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textCodeValue;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelEncounterWarning;
	}
}