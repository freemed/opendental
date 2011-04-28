namespace OpenDental.Forms {
	partial class FormReminderRuleEdit {
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
			this.butDelete = new System.Windows.Forms.Button();
			this.butOk = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.comboReminderCriterion = new System.Windows.Forms.ComboBox();
			this.textCriterionValue = new System.Windows.Forms.TextBox();
			this.textReminderMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelCriterionValue = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelCriterionFK = new System.Windows.Forms.Label();
			this.textCriterionFK = new System.Windows.Forms.TextBox();
			this.butSelectFK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butDelete
			// 
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Location = new System.Drawing.Point(12,172);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 0;
			this.butDelete.Text = "Delete";
			this.butDelete.UseVisualStyleBackColor = true;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOk
			// 
			this.butOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOk.Location = new System.Drawing.Point(278,172);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(75,24);
			this.butOk.TabIndex = 1;
			this.butOk.Text = "Ok";
			this.butOk.UseVisualStyleBackColor = true;
			this.butOk.Click += new System.EventHandler(this.butOk_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Location = new System.Drawing.Point(359,172);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// comboReminderCriterion
			// 
			this.comboReminderCriterion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboReminderCriterion.FormattingEnabled = true;
			this.comboReminderCriterion.Location = new System.Drawing.Point(114,28);
			this.comboReminderCriterion.Name = "comboReminderCriterion";
			this.comboReminderCriterion.Size = new System.Drawing.Size(121,21);
			this.comboReminderCriterion.TabIndex = 3;
			this.comboReminderCriterion.SelectedIndexChanged += new System.EventHandler(this.comboReminderCriterion_SelectedIndexChanged);
			// 
			// textCriterionValue
			// 
			this.textCriterionValue.Location = new System.Drawing.Point(114,55);
			this.textCriterionValue.Name = "textCriterionValue";
			this.textCriterionValue.Size = new System.Drawing.Size(100,20);
			this.textCriterionValue.TabIndex = 4;
			// 
			// textReminderMessage
			// 
			this.textReminderMessage.Location = new System.Drawing.Point(114,107);
			this.textReminderMessage.Name = "textReminderMessage";
			this.textReminderMessage.Size = new System.Drawing.Size(320,20);
			this.textReminderMessage.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5,29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107,17);
			this.label1.TabIndex = 6;
			this.label1.Text = "Reminder Criterion";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCriterionValue
			// 
			this.labelCriterionValue.Location = new System.Drawing.Point(5,56);
			this.labelCriterionValue.Name = "labelCriterionValue";
			this.labelCriterionValue.Size = new System.Drawing.Size(107,17);
			this.labelCriterionValue.TabIndex = 7;
			this.labelCriterionValue.Text = "Criterion Value";
			this.labelCriterionValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5,108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107,17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Reminder Message";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelCriterionFK
			// 
			this.labelCriterionFK.Location = new System.Drawing.Point(5,82);
			this.labelCriterionFK.Name = "labelCriterionFK";
			this.labelCriterionFK.Size = new System.Drawing.Size(107,17);
			this.labelCriterionFK.TabIndex = 10;
			this.labelCriterionFK.Text = "Criterion FK";
			this.labelCriterionFK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCriterionFK
			// 
			this.textCriterionFK.Location = new System.Drawing.Point(114,81);
			this.textCriterionFK.Name = "textCriterionFK";
			this.textCriterionFK.ReadOnly = true;
			this.textCriterionFK.Size = new System.Drawing.Size(159,20);
			this.textCriterionFK.TabIndex = 9;
			// 
			// butSelectFK
			// 
			this.butSelectFK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSelectFK.Location = new System.Drawing.Point(278,78);
			this.butSelectFK.Name = "butSelectFK";
			this.butSelectFK.Size = new System.Drawing.Size(24,24);
			this.butSelectFK.TabIndex = 11;
			this.butSelectFK.Text = "...";
			this.butSelectFK.UseVisualStyleBackColor = true;
			this.butSelectFK.Click += new System.EventHandler(this.butSelectFK_Click);
			// 
			// FormReminderRuleEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(446,207);
			this.Controls.Add(this.butSelectFK);
			this.Controls.Add(this.labelCriterionFK);
			this.Controls.Add(this.textCriterionFK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelCriterionValue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textReminderMessage);
			this.Controls.Add(this.textCriterionValue);
			this.Controls.Add(this.comboReminderCriterion);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOk);
			this.Controls.Add(this.butDelete);
			this.Name = "FormReminderRuleEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Reminder Rule";
			this.Load += new System.EventHandler(this.FormReminderRuleEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butOk;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.ComboBox comboReminderCriterion;
		private System.Windows.Forms.TextBox textCriterionValue;
		private System.Windows.Forms.TextBox textReminderMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelCriterionValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelCriterionFK;
		private System.Windows.Forms.TextBox textCriterionFK;
		private System.Windows.Forms.Button butSelectFK;
	}
}