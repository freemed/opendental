namespace OpenDental{
	partial class FormGuardianEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGuardianEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textDependant = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listRelationship = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textGuardian = new System.Windows.Forms.TextBox();
			this.butPick = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76,13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Dependant";
			// 
			// textDependant
			// 
			this.textDependant.Location = new System.Drawing.Point(15,25);
			this.textDependant.Name = "textDependant";
			this.textDependant.ReadOnly = true;
			this.textDependant.Size = new System.Drawing.Size(365,20);
			this.textDependant.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13,49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66,13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Guardian";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13,95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81,13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Relationship";
			// 
			// listRelationship
			// 
			this.listRelationship.FormattingEnabled = true;
			this.listRelationship.Location = new System.Drawing.Point(15,111);
			this.listRelationship.Name = "listRelationship";
			this.listRelationship.Size = new System.Drawing.Size(213,95);
			this.listRelationship.TabIndex = 9;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(323,229);
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
			this.butCancel.Location = new System.Drawing.Point(323,270);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textGuardian
			// 
			this.textGuardian.Location = new System.Drawing.Point(15,64);
			this.textGuardian.Name = "textGuardian";
			this.textGuardian.ReadOnly = true;
			this.textGuardian.Size = new System.Drawing.Size(334,20);
			this.textGuardian.TabIndex = 10;
			// 
			// butPick
			// 
			this.butPick.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPick.Autosize = true;
			this.butPick.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPick.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPick.CornerRadius = 4F;
			this.butPick.Location = new System.Drawing.Point(353,63);
			this.butPick.Name = "butPick";
			this.butPick.Size = new System.Drawing.Size(27,21);
			this.butPick.TabIndex = 67;
			this.butPick.TabStop = false;
			this.butPick.Text = "...";
			this.butPick.Click += new System.EventHandler(this.butPick_Click);
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
			this.butDelete.Location = new System.Drawing.Point(15,270);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 68;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormGuardianEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(423,314);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butPick);
			this.Controls.Add(this.textGuardian);
			this.Controls.Add(this.listRelationship);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textDependant);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormGuardianEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Guardians";
			this.Load += new System.EventHandler(this.FormGuardianEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textDependant;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listRelationship;
		private System.Windows.Forms.TextBox textGuardian;
		private OpenDental.UI.Button butPick;
		private OpenDental.UI.Button butDelete;
	}
}