namespace OpenDental{
	partial class FormEncounterEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEncounterEdit));
			this.label9 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.textCodeDescript = new OpenDental.ODtextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textCodeValue = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textCodeSystem = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butHcpcs = new OpenDental.UI.Button();
			this.butSnomed = new OpenDental.UI.Button();
			this.butCdt = new OpenDental.UI.Button();
			this.butPickProv = new OpenDental.UI.Button();
			this.butCpt = new OpenDental.UI.Button();
			this.textDateEnc = new OpenDental.ValidDate();
			this.SuspendLayout();
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 21);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(105, 17);
			this.label9.TabIndex = 100;
			this.label9.Text = "Date";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProv
			// 
			this.comboProv.Location = new System.Drawing.Point(120, 46);
			this.comboProv.MaxDropDownItems = 30;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(158, 21);
			this.comboProv.TabIndex = 2;
			this.comboProv.SelectionChangeCommitted += new System.EventHandler(this.comboProv_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 17);
			this.label1.TabIndex = 100;
			this.label1.Text = "Provider";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 256);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 17);
			this.label3.TabIndex = 100;
			this.label3.Text = "Note";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.AcceptsTab = true;
			this.textNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textNote.DetectUrls = false;
			this.textNote.Location = new System.Drawing.Point(120, 256);
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(433, 109);
			this.textNote.TabIndex = 8;
			this.textNote.Text = "";
			// 
			// textCodeDescript
			// 
			this.textCodeDescript.AcceptsTab = true;
			this.textCodeDescript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textCodeDescript.DetectUrls = false;
			this.textCodeDescript.Location = new System.Drawing.Point(120, 125);
			this.textCodeDescript.Name = "textCodeDescript";
			this.textCodeDescript.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textCodeDescript.ReadOnly = true;
			this.textCodeDescript.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textCodeDescript.Size = new System.Drawing.Size(433, 125);
			this.textCodeDescript.TabIndex = 7;
			this.textCodeDescript.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 125);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 17);
			this.label2.TabIndex = 102;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeValue
			// 
			this.textCodeValue.Location = new System.Drawing.Point(120, 73);
			this.textCodeValue.Name = "textCodeValue";
			this.textCodeValue.ReadOnly = true;
			this.textCodeValue.Size = new System.Drawing.Size(158, 20);
			this.textCodeValue.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105, 17);
			this.label4.TabIndex = 106;
			this.label4.Text = "Code";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCodeSystem
			// 
			this.textCodeSystem.Location = new System.Drawing.Point(120, 99);
			this.textCodeSystem.Name = "textCodeSystem";
			this.textCodeSystem.ReadOnly = true;
			this.textCodeSystem.Size = new System.Drawing.Size(158, 20);
			this.textCodeSystem.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(105, 17);
			this.label5.TabIndex = 108;
			this.label5.Text = "Code System";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(478, 380);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 126;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(15, 380);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 24);
			this.butDelete.TabIndex = 125;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(397, 380);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 127;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butHcpcs
			// 
			this.butHcpcs.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHcpcs.Autosize = true;
			this.butHcpcs.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHcpcs.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHcpcs.CornerRadius = 4F;
			this.butHcpcs.Location = new System.Drawing.Point(367, 72);
			this.butHcpcs.Name = "butHcpcs";
			this.butHcpcs.Size = new System.Drawing.Size(58, 21);
			this.butHcpcs.TabIndex = 131;
			this.butHcpcs.Text = "HCPCS";
			this.butHcpcs.Click += new System.EventHandler(this.butHcpcs_Click);
			// 
			// butSnomed
			// 
			this.butSnomed.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSnomed.Autosize = true;
			this.butSnomed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSnomed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSnomed.CornerRadius = 4F;
			this.butSnomed.Location = new System.Drawing.Point(284, 72);
			this.butSnomed.Name = "butSnomed";
			this.butSnomed.Size = new System.Drawing.Size(77, 21);
			this.butSnomed.TabIndex = 132;
			this.butSnomed.Text = "SNOMED CT";
			this.butSnomed.Click += new System.EventHandler(this.butSnomed_Click);
			// 
			// butCdt
			// 
			this.butCdt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCdt.Autosize = true;
			this.butCdt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCdt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCdt.CornerRadius = 4F;
			this.butCdt.Location = new System.Drawing.Point(431, 72);
			this.butCdt.Name = "butCdt";
			this.butCdt.Size = new System.Drawing.Size(58, 21);
			this.butCdt.TabIndex = 130;
			this.butCdt.Text = "CDT";
			this.butCdt.Click += new System.EventHandler(this.butCdt_Click);
			// 
			// butPickProv
			// 
			this.butPickProv.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPickProv.Autosize = true;
			this.butPickProv.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPickProv.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPickProv.CornerRadius = 4F;
			this.butPickProv.Location = new System.Drawing.Point(284, 46);
			this.butPickProv.Name = "butPickProv";
			this.butPickProv.Size = new System.Drawing.Size(26, 21);
			this.butPickProv.TabIndex = 129;
			this.butPickProv.Text = "...";
			this.butPickProv.Click += new System.EventHandler(this.butPickProv_Click);
			// 
			// butCpt
			// 
			this.butCpt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCpt.Autosize = true;
			this.butCpt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCpt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCpt.CornerRadius = 4F;
			this.butCpt.Location = new System.Drawing.Point(495, 72);
			this.butCpt.Name = "butCpt";
			this.butCpt.Size = new System.Drawing.Size(58, 21);
			this.butCpt.TabIndex = 133;
			this.butCpt.Text = "CPT";
			this.butCpt.Click += new System.EventHandler(this.butCpt_Click);
			// 
			// textDateEnc
			// 
			this.textDateEnc.Location = new System.Drawing.Point(120, 21);
			this.textDateEnc.Name = "textDateEnc";
			this.textDateEnc.Size = new System.Drawing.Size(88, 20);
			this.textDateEnc.TabIndex = 134;
			// 
			// FormEncounterEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(565, 415);
			this.Controls.Add(this.textDateEnc);
			this.Controls.Add(this.butCpt);
			this.Controls.Add(this.butHcpcs);
			this.Controls.Add(this.butSnomed);
			this.Controls.Add(this.butCdt);
			this.Controls.Add(this.butPickProv);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textCodeSystem);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textCodeValue);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCodeDescript);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label9);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(573, 442);
			this.Name = "FormEncounterEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Encounter Info";
			this.Load += new System.EventHandler(this.FormEncounters_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboProv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private ODtextBox textNote;
		private ODtextBox textCodeDescript;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textCodeValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textCodeSystem;
		private System.Windows.Forms.Label label5;
		private UI.Button butCancel;
		private UI.Button butDelete;
		private UI.Button butOK;
		private UI.Button butHcpcs;
		private UI.Button butSnomed;
		private UI.Button butCdt;
		private UI.Button butPickProv;
		private UI.Button butCpt;
		private ValidDate textDateEnc;
	}
}