namespace OpenDental
{
	partial class FormAnestheticMedsWasteQty
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsWasteQty));
			this.textDate = new System.Windows.Forms.TextBox();
			this.labelDate = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.textBoxPatID = new System.Windows.Forms.TextBox();
			this.labelPatID = new System.Windows.Forms.Label();
			this.textBoxPatient = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBoxAnesthMedWasteQty = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelNotes = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.comboBoxAnesthMed = new System.Windows.Forms.ComboBox();
			this.textQtyWasted = new System.Windows.Forms.TextBox();
			this.labelQty = new System.Windows.Forms.Label();
			this.groupBoxAnesthMedWasteQty.SuspendLayout();
			this.SuspendLayout();
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(40, 32);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(115, 20);
			this.textDate.TabIndex = 9;
			this.textDate.TextChanged += new System.EventHandler(this.textDate_TextChanged_1);
			// 
			// labelDate
			// 
			this.labelDate.AutoSize = true;
			this.labelDate.Location = new System.Drawing.Point(38, 16);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(30, 13);
			this.labelDate.TabIndex = 10;
			this.labelDate.Text = "Date";
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCancel.Location = new System.Drawing.Point(350, 277);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(66, 26);
			this.butCancel.TabIndex = 54;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClose.Location = new System.Drawing.Point(422, 277);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(90, 26);
			this.butClose.TabIndex = 137;
			this.butClose.Text = "Save and Close";
			this.butClose.UseVisualStyleBackColor = true;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textBoxPatID
			// 
			this.textBoxPatID.Location = new System.Drawing.Point(287, 77);
			this.textBoxPatID.Name = "textBoxPatID";
			this.textBoxPatID.ReadOnly = true;
			this.textBoxPatID.Size = new System.Drawing.Size(113, 20);
			this.textBoxPatID.TabIndex = 108;
			this.textBoxPatID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelPatID
			// 
			this.labelPatID.AutoSize = true;
			this.labelPatID.Location = new System.Drawing.Point(284, 61);
			this.labelPatID.Name = "labelPatID";
			this.labelPatID.Size = new System.Drawing.Size(38, 13);
			this.labelPatID.TabIndex = 107;
			this.labelPatID.Text = "ID No.";
			// 
			// textBoxPatient
			// 
			this.textBoxPatient.Location = new System.Drawing.Point(40, 77);
			this.textBoxPatient.Name = "textBoxPatient";
			this.textBoxPatient.ReadOnly = true;
			this.textBoxPatient.Size = new System.Drawing.Size(231, 20);
			this.textBoxPatient.TabIndex = 106;
			this.textBoxPatient.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 138;
			this.label1.Text = "Patient name";
			// 
			// groupBoxAnesthMedWasteQty
			// 
			this.groupBoxAnesthMedWasteQty.Controls.Add(this.label2);
			this.groupBoxAnesthMedWasteQty.Controls.Add(this.labelNotes);
			this.groupBoxAnesthMedWasteQty.Controls.Add(this.richTextBox1);
			this.groupBoxAnesthMedWasteQty.Controls.Add(this.comboBoxAnesthMed);
			this.groupBoxAnesthMedWasteQty.Controls.Add(this.textQtyWasted);
			this.groupBoxAnesthMedWasteQty.Controls.Add(this.labelQty);
			this.groupBoxAnesthMedWasteQty.Location = new System.Drawing.Point(25, 107);
			this.groupBoxAnesthMedWasteQty.Name = "groupBoxAnesthMedWasteQty";
			this.groupBoxAnesthMedWasteQty.Size = new System.Drawing.Size(487, 150);
			this.groupBoxAnesthMedWasteQty.TabIndex = 139;
			this.groupBoxAnesthMedWasteQty.TabStop = false;
			this.groupBoxAnesthMedWasteQty.Text = "Anesthetic Medication";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(259, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(199, 52);
			this.label2.TabIndex = 12;
			this.label2.Text = "NOTE: Wasted quantities must be in mL to be properly subtracted from inventory";
			// 
			// labelNotes
			// 
			this.labelNotes.AutoSize = true;
			this.labelNotes.Location = new System.Drawing.Point(15, 50);
			this.labelNotes.Name = "labelNotes";
			this.labelNotes.Size = new System.Drawing.Size(35, 13);
			this.labelNotes.TabIndex = 11;
			this.labelNotes.Text = "Notes";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(15, 69);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(231, 66);
			this.richTextBox1.TabIndex = 10;
			this.richTextBox1.Text = "";
			// 
			// comboBoxAnesthMed
			// 
			this.comboBoxAnesthMed.FormattingEnabled = true;
			this.comboBoxAnesthMed.Location = new System.Drawing.Point(15, 19);
			this.comboBoxAnesthMed.Name = "comboBoxAnesthMed";
			this.comboBoxAnesthMed.Size = new System.Drawing.Size(231, 21);
			this.comboBoxAnesthMed.TabIndex = 0;
			// 
			// textQtyWasted
			// 
			this.textQtyWasted.Location = new System.Drawing.Point(257, 20);
			this.textQtyWasted.Name = "textQtyWasted";
			this.textQtyWasted.Size = new System.Drawing.Size(219, 20);
			this.textQtyWasted.TabIndex = 2;
			// 
			// labelQty
			// 
			this.labelQty.AutoSize = true;
			this.labelQty.Location = new System.Drawing.Point(254, 0);
			this.labelQty.Name = "labelQty";
			this.labelQty.Size = new System.Drawing.Size(106, 13);
			this.labelQty.TabIndex = 4;
			this.labelQty.Text = "Quantity wasted (mL)";
			// 
			// FormAnestheticMedsWasteQty
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(532, 319);
			this.Controls.Add(this.groupBoxAnesthMedWasteQty);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxPatID);
			this.Controls.Add(this.labelPatID);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.labelDate);
			this.Controls.Add(this.textBoxPatient);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textDate);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsWasteQty";
			this.Text = "Anesthetic Medication Waste Form";
			this.groupBoxAnesthMedWasteQty.ResumeLayout(false);
			this.groupBoxAnesthMedWasteQty.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textDate;
		private System.Windows.Forms.Label labelDate;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textBoxPatID;
		private System.Windows.Forms.Label labelPatID;
		private System.Windows.Forms.TextBox textBoxPatient;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBoxAnesthMedWasteQty;
		private System.Windows.Forms.Label labelNotes;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ComboBox comboBoxAnesthMed;
		private System.Windows.Forms.TextBox textQtyWasted;
		private System.Windows.Forms.Label labelQty;
		private System.Windows.Forms.Label label2;
	}
}