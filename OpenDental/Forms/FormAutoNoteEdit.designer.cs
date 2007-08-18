namespace OpenDental
{
    partial class FormAutoNoteEdit
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoNoteEdit));
					this.textBoxAutoNoteName = new System.Windows.Forms.TextBox();
					this.labelName = new System.Windows.Forms.Label();
					this.labelControlToInc = new System.Windows.Forms.Label();
					this.labelLabelControl = new System.Windows.Forms.Label();
					this.labelNameControl = new System.Windows.Forms.Label();
					this.listBoxOptionsControl = new System.Windows.Forms.ListBox();
					this.textBoxLabelControl = new System.Windows.Forms.TextBox();
					this.textBoxDescriptControl = new System.Windows.Forms.TextBox();
					this.listBoxControlsToIncl = new System.Windows.Forms.ListBox();
					this.listBoxControls = new System.Windows.Forms.ListBox();
					this.labelAddControl = new System.Windows.Forms.Label();
					this.labelControl = new System.Windows.Forms.Label();
					this.textBoxTextPrefaceControl = new System.Windows.Forms.TextBox();
					this.textBoxTextControl = new System.Windows.Forms.TextBox();
					this.label1 = new System.Windows.Forms.Label();
					this.label2 = new System.Windows.Forms.Label();
					this.butEditControl = new OpenDental.UI.Button();
					this.butCreateControl = new OpenDental.UI.Button();
					this.butOK = new OpenDental.UI.Button();
					this.butCancel = new OpenDental.UI.Button();
					this.SuspendLayout();
					// 
					// textBoxAutoNoteName
					// 
					this.textBoxAutoNoteName.Location = new System.Drawing.Point(269,133);
					this.textBoxAutoNoteName.Name = "textBoxAutoNoteName";
					this.textBoxAutoNoteName.Size = new System.Drawing.Size(356,20);
					this.textBoxAutoNoteName.TabIndex = 0;
					// 
					// labelName
					// 
					this.labelName.AutoSize = true;
					this.labelName.Location = new System.Drawing.Point(121,136);
					this.labelName.Name = "labelName";
					this.labelName.Size = new System.Drawing.Size(142,13);
					this.labelName.TabIndex = 1;
					this.labelName.Text = "Change the Auto Note name";
					// 
					// labelControlToInc
					// 
					this.labelControlToInc.AutoSize = true;
					this.labelControlToInc.Location = new System.Drawing.Point(195,218);
					this.labelControlToInc.Name = "labelControlToInc";
					this.labelControlToInc.Size = new System.Drawing.Size(230,13);
					this.labelControlToInc.TabIndex = 57;
					this.labelControlToInc.Text = "The contols that will be added to the Auto Note";
					// 
					// labelLabelControl
					// 
					this.labelLabelControl.AutoSize = true;
					this.labelLabelControl.Location = new System.Drawing.Point(435,268);
					this.labelLabelControl.Name = "labelLabelControl";
					this.labelLabelControl.Size = new System.Drawing.Size(33,13);
					this.labelLabelControl.TabIndex = 75;
					this.labelLabelControl.Text = "Label";
					this.labelLabelControl.Visible = false;
					// 
					// labelNameControl
					// 
					this.labelNameControl.AutoSize = true;
					this.labelNameControl.Location = new System.Drawing.Point(435,218);
					this.labelNameControl.Name = "labelNameControl";
					this.labelNameControl.Size = new System.Drawing.Size(35,13);
					this.labelNameControl.TabIndex = 74;
					this.labelNameControl.Text = "Name";
					this.labelNameControl.Visible = false;
					// 
					// listBoxOptionsControl
					// 
					this.listBoxOptionsControl.Enabled = false;
					this.listBoxOptionsControl.FormattingEnabled = true;
					this.listBoxOptionsControl.Location = new System.Drawing.Point(471,403);
					this.listBoxOptionsControl.Name = "listBoxOptionsControl";
					this.listBoxOptionsControl.Size = new System.Drawing.Size(120,108);
					this.listBoxOptionsControl.TabIndex = 73;
					this.listBoxOptionsControl.Visible = false;
					// 
					// textBoxLabelControl
					// 
					this.textBoxLabelControl.Location = new System.Drawing.Point(480,265);
					this.textBoxLabelControl.Name = "textBoxLabelControl";
					this.textBoxLabelControl.ReadOnly = true;
					this.textBoxLabelControl.Size = new System.Drawing.Size(177,20);
					this.textBoxLabelControl.TabIndex = 70;
					this.textBoxLabelControl.Visible = false;
					// 
					// textBoxDescriptControl
					// 
					this.textBoxDescriptControl.Location = new System.Drawing.Point(480,218);
					this.textBoxDescriptControl.Name = "textBoxDescriptControl";
					this.textBoxDescriptControl.ReadOnly = true;
					this.textBoxDescriptControl.Size = new System.Drawing.Size(177,20);
					this.textBoxDescriptControl.TabIndex = 69;
					this.textBoxDescriptControl.Visible = false;
					// 
					// listBoxControlsToIncl
					// 
					this.listBoxControlsToIncl.FormattingEnabled = true;
					this.listBoxControlsToIncl.Location = new System.Drawing.Point(240,246);
					this.listBoxControlsToIncl.Name = "listBoxControlsToIncl";
					this.listBoxControlsToIncl.Size = new System.Drawing.Size(120,277);
					this.listBoxControlsToIncl.TabIndex = 79;
					this.listBoxControlsToIncl.SelectedIndexChanged += new System.EventHandler(this.listBoxControlsToIncl_SelectedIndexChanged);
					// 
					// listBoxControls
					// 
					this.listBoxControls.FormattingEnabled = true;
					this.listBoxControls.Location = new System.Drawing.Point(32,246);
					this.listBoxControls.Name = "listBoxControls";
					this.listBoxControls.Size = new System.Drawing.Size(120,277);
					this.listBoxControls.TabIndex = 85;
					this.listBoxControls.SelectedIndexChanged += new System.EventHandler(this.listBoxControls_SelectedIndexChanged);
					// 
					// labelAddControl
					// 
					this.labelAddControl.AutoSize = true;
					this.labelAddControl.Location = new System.Drawing.Point(12,218);
					this.labelAddControl.Name = "labelAddControl";
					this.labelAddControl.Size = new System.Drawing.Size(143,13);
					this.labelAddControl.TabIndex = 89;
					this.labelAddControl.Text = "Double click a control to add";
					// 
					// labelControl
					// 
					this.labelControl.AutoSize = true;
					this.labelControl.Location = new System.Drawing.Point(496,197);
					this.labelControl.Name = "labelControl";
					this.labelControl.Size = new System.Drawing.Size(126,13);
					this.labelControl.TabIndex = 91;
					this.labelControl.Text = "Selected Control Settings";
					this.labelControl.Visible = false;
					// 
					// textBoxTextPrefaceControl
					// 
					this.textBoxTextPrefaceControl.Location = new System.Drawing.Point(480,291);
					this.textBoxTextPrefaceControl.Multiline = true;
					this.textBoxTextPrefaceControl.Name = "textBoxTextPrefaceControl";
					this.textBoxTextPrefaceControl.ReadOnly = true;
					this.textBoxTextPrefaceControl.Size = new System.Drawing.Size(177,43);
					this.textBoxTextPrefaceControl.TabIndex = 96;
					this.textBoxTextPrefaceControl.Visible = false;
					// 
					// textBoxTextControl
					// 
					this.textBoxTextControl.Location = new System.Drawing.Point(480,340);
					this.textBoxTextControl.Multiline = true;
					this.textBoxTextControl.Name = "textBoxTextControl";
					this.textBoxTextControl.ReadOnly = true;
					this.textBoxTextControl.Size = new System.Drawing.Size(177,57);
					this.textBoxTextControl.TabIndex = 97;
					this.textBoxTextControl.Visible = false;
					// 
					// label1
					// 
					this.label1.AutoSize = true;
					this.label1.Location = new System.Drawing.Point(446,340);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(28,13);
					this.label1.TabIndex = 98;
					this.label1.Text = "Text";
					this.label1.Visible = false;
					// 
					// label2
					// 
					this.label2.AutoSize = true;
					this.label2.Location = new System.Drawing.Point(406,291);
					this.label2.Name = "label2";
					this.label2.Size = new System.Drawing.Size(68,13);
					this.label2.TabIndex = 99;
					this.label2.Text = "Preface Text";
					this.label2.Visible = false;
					// 
					// butEditControl
					// 
					this.butEditControl.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.butEditControl.Autosize = true;
					this.butEditControl.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.butEditControl.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.butEditControl.CornerRadius = 4F;
					this.butEditControl.Location = new System.Drawing.Point(32,560);
					this.butEditControl.Name = "butEditControl";
					this.butEditControl.Size = new System.Drawing.Size(123,25);
					this.butEditControl.TabIndex = 95;
					this.butEditControl.Text = "Edit Control";
					this.butEditControl.Visible = false;
					this.butEditControl.Click += new System.EventHandler(this.butEditControl_Click);
					// 
					// butCreateControl
					// 
					this.butCreateControl.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.butCreateControl.Autosize = true;
					this.butCreateControl.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.butCreateControl.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.butCreateControl.CornerRadius = 4F;
					this.butCreateControl.Location = new System.Drawing.Point(32,529);
					this.butCreateControl.Name = "butCreateControl";
					this.butCreateControl.Size = new System.Drawing.Size(120,25);
					this.butCreateControl.TabIndex = 94;
					this.butCreateControl.Text = "Create New Control";
					this.butCreateControl.Visible = false;
					this.butCreateControl.Click += new System.EventHandler(this.butCreateControl_Click);
					// 
					// butOK
					// 
					this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.butOK.Autosize = true;
					this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.butOK.CornerRadius = 4F;
					this.butOK.Location = new System.Drawing.Point(602,529);
					this.butOK.Name = "butOK";
					this.butOK.Size = new System.Drawing.Size(78,25);
					this.butOK.TabIndex = 83;
					this.butOK.Text = "OK";
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
					this.butCancel.Location = new System.Drawing.Point(602,560);
					this.butCancel.Name = "butCancel";
					this.butCancel.Size = new System.Drawing.Size(78,25);
					this.butCancel.TabIndex = 82;
					this.butCancel.Text = "Cancel";
					this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
					// 
					// FormAutoNoteEdit
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(704,602);
					this.Controls.Add(this.label2);
					this.Controls.Add(this.label1);
					this.Controls.Add(this.textBoxTextControl);
					this.Controls.Add(this.textBoxTextPrefaceControl);
					this.Controls.Add(this.butEditControl);
					this.Controls.Add(this.butCreateControl);
					this.Controls.Add(this.labelControl);
					this.Controls.Add(this.labelAddControl);
					this.Controls.Add(this.labelLabelControl);
					this.Controls.Add(this.labelNameControl);
					this.Controls.Add(this.listBoxOptionsControl);
					this.Controls.Add(this.textBoxLabelControl);
					this.Controls.Add(this.textBoxDescriptControl);
					this.Controls.Add(this.labelControlToInc);
					this.Controls.Add(this.labelName);
					this.Controls.Add(this.textBoxAutoNoteName);
					this.Controls.Add(this.listBoxControlsToIncl);
					this.Controls.Add(this.listBoxControls);
					this.Controls.Add(this.butOK);
					this.Controls.Add(this.butCancel);
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.Name = "FormAutoNoteEdit";
					this.ShowInTaskbar = false;
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Auto Note Edit";
					this.Load += new System.EventHandler(this.FormAutoNoteEdit_Load);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAutoNoteName;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Label labelControlToInc;
        private System.Windows.Forms.Label labelLabelControl;
        private System.Windows.Forms.Label labelNameControl;
		private System.Windows.Forms.ListBox listBoxOptionsControl;
        private System.Windows.Forms.TextBox textBoxLabelControl;
        private System.Windows.Forms.TextBox textBoxDescriptControl;
		private System.Windows.Forms.ListBox listBoxControlsToIncl;
        private OpenDental.UI.Button butCancel;
			private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listBoxControls;
		private System.Windows.Forms.Label labelAddControl;
		private System.Windows.Forms.Label labelControl;
		private OpenDental.UI.Button butCreateControl;
		private OpenDental.UI.Button butEditControl;
		private System.Windows.Forms.TextBox textBoxTextPrefaceControl;
		private System.Windows.Forms.TextBox textBoxTextControl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
    }
}