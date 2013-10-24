namespace OpenDental {
	partial class FormEhrVaccinePatEdit {
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
			this.label5 = new System.Windows.Forms.Label();
			this.labelAmount = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.textAmount = new System.Windows.Forms.TextBox();
			this.comboVaccine = new System.Windows.Forms.ComboBox();
			this.labelVaccine = new System.Windows.Forms.Label();
			this.comboUnits = new System.Windows.Forms.ComboBox();
			this.textManufacturer = new System.Windows.Forms.TextBox();
			this.labelDateTimeStartStop = new System.Windows.Forms.Label();
			this.textDateTimeStart = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textLotNum = new System.Windows.Forms.TextBox();
			this.textDateTimeStop = new System.Windows.Forms.TextBox();
			this.labelDateTimeStop = new System.Windows.Forms.Label();
			this.checkNotGiven = new System.Windows.Forms.CheckBox();
			this.labelDocument = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(40,57);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(93,17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Manufacturer";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(40,136);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(93,17);
			this.labelAmount.TabIndex = 10;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(424,365);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,23);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(343,365);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,23);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(26,365);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,23);
			this.butDelete.TabIndex = 8;
			this.butDelete.TabStop = false;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(136,134);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(63,20);
			this.textAmount.TabIndex = 3;
			// 
			// comboVaccine
			// 
			this.comboVaccine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboVaccine.FormattingEnabled = true;
			this.comboVaccine.Location = new System.Drawing.Point(136,29);
			this.comboVaccine.Name = "comboVaccine";
			this.comboVaccine.Size = new System.Drawing.Size(201,21);
			this.comboVaccine.TabIndex = 0;
			this.comboVaccine.SelectedIndexChanged += new System.EventHandler(this.comboVaccine_SelectedIndexChanged);
			// 
			// labelVaccine
			// 
			this.labelVaccine.Location = new System.Drawing.Point(41,29);
			this.labelVaccine.Name = "labelVaccine";
			this.labelVaccine.Size = new System.Drawing.Size(93,17);
			this.labelVaccine.TabIndex = 13;
			this.labelVaccine.Text = "Vaccine Def";
			this.labelVaccine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboUnits
			// 
			this.comboUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUnits.FormattingEnabled = true;
			this.comboUnits.Location = new System.Drawing.Point(136,160);
			this.comboUnits.Name = "comboUnits";
			this.comboUnits.Size = new System.Drawing.Size(63,21);
			this.comboUnits.TabIndex = 4;
			// 
			// textManufacturer
			// 
			this.textManufacturer.Location = new System.Drawing.Point(136,56);
			this.textManufacturer.Name = "textManufacturer";
			this.textManufacturer.ReadOnly = true;
			this.textManufacturer.Size = new System.Drawing.Size(201,20);
			this.textManufacturer.TabIndex = 14;
			this.textManufacturer.TabStop = false;
			// 
			// labelDateTimeStartStop
			// 
			this.labelDateTimeStartStop.Location = new System.Drawing.Point(8,82);
			this.labelDateTimeStartStop.Name = "labelDateTimeStartStop";
			this.labelDateTimeStartStop.Size = new System.Drawing.Size(125,17);
			this.labelDateTimeStartStop.TabIndex = 11;
			this.labelDateTimeStartStop.Text = "Date Time Start";
			this.labelDateTimeStartStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTimeStart
			// 
			this.textDateTimeStart.Location = new System.Drawing.Point(136,82);
			this.textDateTimeStart.Name = "textDateTimeStart";
			this.textDateTimeStart.Size = new System.Drawing.Size(151,20);
			this.textDateTimeStart.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(40,189);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93,17);
			this.label2.TabIndex = 9;
			this.label2.Text = "Lot Number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLotNum
			// 
			this.textLotNum.Location = new System.Drawing.Point(136,187);
			this.textLotNum.Name = "textLotNum";
			this.textLotNum.Size = new System.Drawing.Size(118,20);
			this.textLotNum.TabIndex = 5;
			// 
			// textDateTimeStop
			// 
			this.textDateTimeStop.Location = new System.Drawing.Point(136,108);
			this.textDateTimeStop.Name = "textDateTimeStop";
			this.textDateTimeStop.Size = new System.Drawing.Size(151,20);
			this.textDateTimeStop.TabIndex = 2;
			// 
			// labelDateTimeStop
			// 
			this.labelDateTimeStop.Location = new System.Drawing.Point(8,109);
			this.labelDateTimeStop.Name = "labelDateTimeStop";
			this.labelDateTimeStop.Size = new System.Drawing.Size(125,17);
			this.labelDateTimeStop.TabIndex = 11;
			this.labelDateTimeStop.Text = "Date Time Stop";
			this.labelDateTimeStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkNotGiven
			// 
			this.checkNotGiven.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNotGiven.Location = new System.Drawing.Point(20,214);
			this.checkNotGiven.Name = "checkNotGiven";
			this.checkNotGiven.Size = new System.Drawing.Size(130,17);
			this.checkNotGiven.TabIndex = 15;
			this.checkNotGiven.Text = "Vaccine Not Given";
			this.checkNotGiven.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNotGiven.UseVisualStyleBackColor = true;
			// 
			// labelDocument
			// 
			this.labelDocument.Location = new System.Drawing.Point(156,214);
			this.labelDocument.Name = "labelDocument";
			this.labelDocument.Size = new System.Drawing.Size(372,36);
			this.labelDocument.TabIndex = 16;
			this.labelDocument.Text = "Document reason not given below.  Reason can include a specific allergy, adverse " +
    "effect, intollerance, patient declines, specific disease, etc.";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(136,255);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(327,61);
			this.textNote.TabIndex = 17;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(41,255);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93,17);
			this.label3.TabIndex = 18;
			this.label3.Text = "Note";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40,162);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93,17);
			this.label1.TabIndex = 19;
			this.label1.Text = "Units";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormVaccinePatEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526,406);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.labelDocument);
			this.Controls.Add(this.checkNotGiven);
			this.Controls.Add(this.textDateTimeStop);
			this.Controls.Add(this.textDateTimeStart);
			this.Controls.Add(this.labelDateTimeStop);
			this.Controls.Add(this.labelDateTimeStartStop);
			this.Controls.Add(this.comboUnits);
			this.Controls.Add(this.comboVaccine);
			this.Controls.Add(this.labelVaccine);
			this.Controls.Add(this.textLotNum);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.textManufacturer);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.label5);
			this.Name = "FormVaccinePatEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Vaccine Edit";
			this.Load += new System.EventHandler(this.FormVaccinePatEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelAmount;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.TextBox textAmount;
		private System.Windows.Forms.ComboBox comboVaccine;
		private System.Windows.Forms.Label labelVaccine;
		private System.Windows.Forms.ComboBox comboUnits;
		private System.Windows.Forms.TextBox textManufacturer;
		private System.Windows.Forms.Label labelDateTimeStartStop;
		private System.Windows.Forms.TextBox textDateTimeStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textLotNum;
		private System.Windows.Forms.TextBox textDateTimeStop;
		private System.Windows.Forms.Label labelDateTimeStop;
		private System.Windows.Forms.CheckBox checkNotGiven;
		private System.Windows.Forms.Label labelDocument;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
	}
}