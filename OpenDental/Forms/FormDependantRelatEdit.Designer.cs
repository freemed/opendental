namespace OpenDental{
	partial class FormDependantRelatEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDependantRelatEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textDependant = new System.Windows.Forms.TextBox();
			this.listFamilyMembers = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listRelationships = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60,13);
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
			// listFamilyMembers
			// 
			this.listFamilyMembers.FormattingEnabled = true;
			this.listFamilyMembers.Location = new System.Drawing.Point(15,65);
			this.listFamilyMembers.Name = "listFamilyMembers";
			this.listFamilyMembers.ScrollAlwaysVisible = true;
			this.listFamilyMembers.Size = new System.Drawing.Size(365,56);
			this.listFamilyMembers.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12,48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77,13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Family Member";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12,125);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65,13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Relationship";
			// 
			// listRelationships
			// 
			this.listRelationships.FormattingEnabled = true;
			this.listRelationships.Location = new System.Drawing.Point(15,142);
			this.listRelationships.Name = "listRelationships";
			this.listRelationships.Size = new System.Drawing.Size(365,95);
			this.listRelationships.TabIndex = 9;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(297,279);
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
			this.butCancel.Location = new System.Drawing.Point(297,320);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormDependantRelatEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(397,371);
			this.Controls.Add(this.listRelationships);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listFamilyMembers);
			this.Controls.Add(this.textDependant);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormDependantRelatEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Dependant Relationship";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textDependant;
		private System.Windows.Forms.ListBox listFamilyMembers;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listRelationships;
	}
}