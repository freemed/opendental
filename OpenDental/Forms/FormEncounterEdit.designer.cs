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
			this.butDelete = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butPickProv = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.textDateTimeEnc = new System.Windows.Forms.TextBox();
			this.textCodeDescript = new OpenDental.ODtextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textCodeValue = new System.Windows.Forms.TextBox();
			this.butCodePicker = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(12, 380);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 23);
			this.butDelete.TabIndex = 10;
			this.butDelete.Text = "&Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(393, 380);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(312, 380);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 23);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(120, 46);
			this.comboProv.MaxDropDownItems = 30;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(158, 21);
			this.comboProv.TabIndex = 2;
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
			// butPickProv
			// 
			this.butPickProv.Location = new System.Drawing.Point(281, 46);
			this.butPickProv.Name = "butPickProv";
			this.butPickProv.Size = new System.Drawing.Size(26, 21);
			this.butPickProv.TabIndex = 3;
			this.butPickProv.Text = "...";
			this.butPickProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPickProv.UseVisualStyleBackColor = true;
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
			this.textNote.DetectUrls = false;
			this.textNote.Location = new System.Drawing.Point(120, 256);
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(348, 109);
			this.textNote.TabIndex = 8;
			this.textNote.Text = "";
			// 
			// textDateTimeEnc
			// 
			this.textDateTimeEnc.Location = new System.Drawing.Point(120, 20);
			this.textDateTimeEnc.Name = "textDateTimeEnc";
			this.textDateTimeEnc.Size = new System.Drawing.Size(158, 20);
			this.textDateTimeEnc.TabIndex = 1;
			// 
			// textCodeDescript
			// 
			this.textCodeDescript.AcceptsTab = true;
			this.textCodeDescript.DetectUrls = false;
			this.textCodeDescript.Location = new System.Drawing.Point(120, 125);
			this.textCodeDescript.Name = "textCodeDescript";
			this.textCodeDescript.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textCodeDescript.ReadOnly = true;
			this.textCodeDescript.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textCodeDescript.Size = new System.Drawing.Size(348, 125);
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
			// butCodePicker
			// 
			this.butCodePicker.Location = new System.Drawing.Point(281, 72);
			this.butCodePicker.Name = "butCodePicker";
			this.butCodePicker.Size = new System.Drawing.Size(26, 21);
			this.butCodePicker.TabIndex = 5;
			this.butCodePicker.Text = "...";
			this.butCodePicker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCodePicker.UseVisualStyleBackColor = true;
			this.butCodePicker.Click += new System.EventHandler(this.butCodePicker_Click);
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
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(120, 99);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(158, 20);
			this.textBox1.TabIndex = 6;
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
			// FormEncounterEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(480, 415);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textCodeValue);
			this.Controls.Add(this.butCodePicker);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCodeDescript);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDateTimeEnc);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butPickProv);
			this.Controls.Add(this.comboProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormEncounterEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Encounter Info";
			this.Load += new System.EventHandler(this.FormEncounters_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboProv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butPickProv;
		private System.Windows.Forms.Label label3;
		private ODtextBox textNote;
		private System.Windows.Forms.TextBox textDateTimeEnc;
		private ODtextBox textCodeDescript;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textCodeValue;
		private System.Windows.Forms.Button butCodePicker;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label5;
	}
}