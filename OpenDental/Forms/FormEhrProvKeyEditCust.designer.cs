namespace OpenDental{
	partial class FormEhrProvKeyEditCust {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEhrProvKeyEditCust));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textLName = new System.Windows.Forms.TextBox();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textEhrKey = new System.Windows.Forms.TextBox();
			this.labelEhrKey = new System.Windows.Forms.Label();
			this.butGenerate = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.groupProcedure = new System.Windows.Forms.GroupBox();
			this.textProcDescription = new System.Windows.Forms.TextBox();
			this.textProcDate2 = new System.Windows.Forms.TextBox();
			this.textProcFee = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.butDetach = new OpenDental.UI.Button();
			this.butAttach = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.groupProcedure.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(499,587);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(594,587);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(188,124);
			this.textLName.MaxLength = 100;
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(161,20);
			this.textLName.TabIndex = 115;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(188,147);
			this.textFName.MaxLength = 100;
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(161,20);
			this.textFName.TabIndex = 116;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(54,128);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(132,14);
			this.label10.TabIndex = 118;
			this.label10.Text = "Prov Last Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(60,151);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,14);
			this.label8.TabIndex = 117;
			this.label8.Text = "Prov First Name";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(78,29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(422,89);
			this.label1.TabIndex = 114;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// textEhrKey
			// 
			this.textEhrKey.Location = new System.Drawing.Point(188,170);
			this.textEhrKey.MaxLength = 15;
			this.textEhrKey.Name = "textEhrKey";
			this.textEhrKey.Size = new System.Drawing.Size(161,20);
			this.textEhrKey.TabIndex = 112;
			// 
			// labelEhrKey
			// 
			this.labelEhrKey.Location = new System.Drawing.Point(100,174);
			this.labelEhrKey.Name = "labelEhrKey";
			this.labelEhrKey.Size = new System.Drawing.Size(88,14);
			this.labelEhrKey.TabIndex = 113;
			this.labelEhrKey.Text = "EHR Key";
			this.labelEhrKey.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butGenerate
			// 
			this.butGenerate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGenerate.Autosize = true;
			this.butGenerate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGenerate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGenerate.CornerRadius = 4F;
			this.butGenerate.Location = new System.Drawing.Point(355,169);
			this.butGenerate.Name = "butGenerate";
			this.butGenerate.Size = new System.Drawing.Size(75,24);
			this.butGenerate.TabIndex = 119;
			this.butGenerate.Text = "Generate";
			this.butGenerate.Click += new System.EventHandler(this.butGenerate_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(30,587);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 120;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// groupProcedure
			// 
			this.groupProcedure.Controls.Add(this.textBox2);
			this.groupProcedure.Controls.Add(this.label9);
			this.groupProcedure.Controls.Add(this.textProcDescription);
			this.groupProcedure.Controls.Add(this.textProcDate2);
			this.groupProcedure.Controls.Add(this.textProcFee);
			this.groupProcedure.Controls.Add(this.label6);
			this.groupProcedure.Controls.Add(this.label5);
			this.groupProcedure.Controls.Add(this.label7);
			this.groupProcedure.Controls.Add(this.butDetach);
			this.groupProcedure.Controls.Add(this.butAttach);
			this.groupProcedure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProcedure.Location = new System.Drawing.Point(57,199);
			this.groupProcedure.Name = "groupProcedure";
			this.groupProcedure.Size = new System.Drawing.Size(450,161);
			this.groupProcedure.TabIndex = 121;
			this.groupProcedure.TabStop = false;
			this.groupProcedure.Text = "Procedure";
			// 
			// textProcDescription
			// 
			this.textProcDescription.Location = new System.Drawing.Point(131,97);
			this.textProcDescription.Name = "textProcDescription";
			this.textProcDescription.ReadOnly = true;
			this.textProcDescription.Size = new System.Drawing.Size(241,20);
			this.textProcDescription.TabIndex = 43;
			// 
			// textProcDate2
			// 
			this.textProcDate2.Location = new System.Drawing.Point(131,51);
			this.textProcDate2.Name = "textProcDate2";
			this.textProcDate2.ReadOnly = true;
			this.textProcDate2.Size = new System.Drawing.Size(76,20);
			this.textProcDate2.TabIndex = 42;
			// 
			// textProcFee
			// 
			this.textProcFee.Location = new System.Drawing.Point(131,120);
			this.textProcFee.Name = "textProcFee";
			this.textProcFee.ReadOnly = true;
			this.textProcFee.Size = new System.Drawing.Size(76,20);
			this.textProcFee.TabIndex = 35;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(23,122);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104,16);
			this.label6.TabIndex = 28;
			this.label6.Text = "Fee";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(25,100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(104,16);
			this.label5.TabIndex = 26;
			this.label5.Text = "Description";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24,53);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104,16);
			this.label7.TabIndex = 25;
			this.label7.Text = "Procedure Date";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDetach
			// 
			this.butDetach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDetach.Autosize = true;
			this.butDetach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDetach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDetach.CornerRadius = 4F;
			this.butDetach.Location = new System.Drawing.Point(210,21);
			this.butDetach.Name = "butDetach";
			this.butDetach.Size = new System.Drawing.Size(75,24);
			this.butDetach.TabIndex = 9;
			this.butDetach.Text = "Detach";
			this.butDetach.Click += new System.EventHandler(this.butDetach_Click);
			// 
			// butAttach
			// 
			this.butAttach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAttach.Autosize = true;
			this.butAttach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAttach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAttach.CornerRadius = 4F;
			this.butAttach.Location = new System.Drawing.Point(131,21);
			this.butAttach.Name = "butAttach";
			this.butAttach.Size = new System.Drawing.Size(75,24);
			this.butAttach.TabIndex = 8;
			this.butAttach.Text = "Attach";
			this.butAttach.Click += new System.EventHandler(this.butAttach_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(188,366);
			this.textBox1.MaxLength = 15;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(46,20);
			this.textBox1.TabIndex = 122;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,370);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176,14);
			this.label2.TabIndex = 123;
			this.label2.Text = "FTE (Full-Time Equivalent)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNotes
			// 
			this.textNotes.Location = new System.Drawing.Point(188,390);
			this.textNotes.MaxLength = 15;
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.Size = new System.Drawing.Size(319,92);
			this.textNotes.TabIndex = 124;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(100,394);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88,14);
			this.label3.TabIndex = 125;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(238,370);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(463,14);
			this.label4.TabIndex = 126;
			this.label4.Text = "For example, half-time would be .5 and 1 day a week would be about .25";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(131,74);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(241,20);
			this.textBox2.TabIndex = 45;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(25,77);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104,16);
			this.label9.TabIndex = 44;
			this.label9.Text = "Customer";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormEhrProvKeyEditCust
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(694,631);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupProcedure);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butGenerate);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textEhrKey);
			this.Controls.Add(this.labelEhrKey);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormEhrProvKeyEditCust";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Provider Key";
			this.Load += new System.EventHandler(this.FormEhrProvKeyEditCust_Load);
			this.groupProcedure.ResumeLayout(false);
			this.groupProcedure.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textEhrKey;
		private System.Windows.Forms.Label labelEhrKey;
		private UI.Button butGenerate;
		private UI.Button butDelete;
		private System.Windows.Forms.GroupBox groupProcedure;
		private System.Windows.Forms.TextBox textProcDescription;
		private System.Windows.Forms.TextBox textProcDate2;
		private System.Windows.Forms.TextBox textProcFee;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private UI.Button butDetach;
		private UI.Button butAttach;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textNotes;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label9;
	}
}