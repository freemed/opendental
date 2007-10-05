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
			this.labelText = new System.Windows.Forms.Label();
			this.labelPrefaceText = new System.Windows.Forms.Label();
			this.listBoxControlToIncNum = new System.Windows.Forms.ListBox();
			this.textBoxTypeControl = new System.Windows.Forms.TextBox();
			this.labelTypeControl = new System.Windows.Forms.Label();
			this.butEditControl = new OpenDental.UI.Button();
			this.butCreateControl = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textBoxAutoNoteName
			// 
			this.textBoxAutoNoteName.Location = new System.Drawing.Point(240, 24);
			this.textBoxAutoNoteName.Name = "textBoxAutoNoteName";
			this.textBoxAutoNoteName.Size = new System.Drawing.Size(356, 20);
			this.textBoxAutoNoteName.TabIndex = 0;
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(92, 24);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(142, 13);
			this.labelName.TabIndex = 1;
			this.labelName.Text = "Change the Auto Note name";
			// 
			// labelControlToInc
			// 
			this.labelControlToInc.Location = new System.Drawing.Point(178, 86);
			this.labelControlToInc.Name = "labelControlToInc";
			this.labelControlToInc.Size = new System.Drawing.Size(247, 13);
			this.labelControlToInc.TabIndex = 57;
			this.labelControlToInc.Text = "The controls that will be added to the Auto Note";
			// 
			// labelLabelControl
			// 
			this.labelLabelControl.Location = new System.Drawing.Point(430, 174);
			this.labelLabelControl.Name = "labelLabelControl";
			this.labelLabelControl.Size = new System.Drawing.Size(48, 13);
			this.labelLabelControl.TabIndex = 75;
			this.labelLabelControl.Text = "Label";
			this.labelLabelControl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelLabelControl.Visible = false;
			// 
			// labelNameControl
			// 
			this.labelNameControl.Location = new System.Drawing.Point(428, 145);
			this.labelNameControl.Name = "labelNameControl";
			this.labelNameControl.Size = new System.Drawing.Size(50, 13);
			this.labelNameControl.TabIndex = 74;
			this.labelNameControl.Text = "Name";
			this.labelNameControl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelNameControl.Visible = false;
			// 
			// listBoxOptionsControl
			// 
			this.listBoxOptionsControl.Enabled = false;
			this.listBoxOptionsControl.FormattingEnabled = true;
			this.listBoxOptionsControl.Location = new System.Drawing.Point(479, 309);
			this.listBoxOptionsControl.Name = "listBoxOptionsControl";
			this.listBoxOptionsControl.Size = new System.Drawing.Size(120, 108);
			this.listBoxOptionsControl.TabIndex = 11;
			this.listBoxOptionsControl.Visible = false;
			this.listBoxOptionsControl.SelectedIndexChanged += new System.EventHandler(this.listBoxOptionsControl_SelectedIndexChanged);
			// 
			// textBoxLabelControl
			// 
			this.textBoxLabelControl.Location = new System.Drawing.Point(479, 171);
			this.textBoxLabelControl.Name = "textBoxLabelControl";
			this.textBoxLabelControl.ReadOnly = true;
			this.textBoxLabelControl.Size = new System.Drawing.Size(177, 20);
			this.textBoxLabelControl.TabIndex = 8;
			this.textBoxLabelControl.Visible = false;
			// 
			// textBoxDescriptControl
			// 
			this.textBoxDescriptControl.Location = new System.Drawing.Point(479, 145);
			this.textBoxDescriptControl.Name = "textBoxDescriptControl";
			this.textBoxDescriptControl.ReadOnly = true;
			this.textBoxDescriptControl.Size = new System.Drawing.Size(177, 20);
			this.textBoxDescriptControl.TabIndex = 7;
			this.textBoxDescriptControl.Visible = false;
			// 
			// listBoxControlsToIncl
			// 
			this.listBoxControlsToIncl.FormattingEnabled = true;
			this.listBoxControlsToIncl.Location = new System.Drawing.Point(240, 114);
			this.listBoxControlsToIncl.Name = "listBoxControlsToIncl";
			this.listBoxControlsToIncl.Size = new System.Drawing.Size(120, 277);
			this.listBoxControlsToIncl.TabIndex = 2;
			this.listBoxControlsToIncl.SelectedIndexChanged += new System.EventHandler(this.listBoxControlsToIncl_SelectedIndexChanged);
			this.listBoxControlsToIncl.DoubleClick += new System.EventHandler(this.listBoxControlsToIncl_DoubleClick);
			// 
			// listBoxControls
			// 
			this.listBoxControls.FormattingEnabled = true;
			this.listBoxControls.Location = new System.Drawing.Point(32, 114);
			this.listBoxControls.Name = "listBoxControls";
			this.listBoxControls.Size = new System.Drawing.Size(120, 277);
			this.listBoxControls.TabIndex = 1;
			this.listBoxControls.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxControls_MouseDoubleClick);
			this.listBoxControls.SelectedIndexChanged += new System.EventHandler(this.listBoxControls_SelectedIndexChanged);
			// 
			// labelAddControl
			// 
			this.labelAddControl.Location = new System.Drawing.Point(12, 86);
			this.labelAddControl.Name = "labelAddControl";
			this.labelAddControl.Size = new System.Drawing.Size(160, 13);
			this.labelAddControl.TabIndex = 89;
			this.labelAddControl.Text = "Double click a control to add";
			// 
			// labelControl
			// 
			this.labelControl.Location = new System.Drawing.Point(496, 93);
			this.labelControl.Name = "labelControl";
			this.labelControl.Size = new System.Drawing.Size(126, 13);
			this.labelControl.TabIndex = 91;
			this.labelControl.Text = "Selected Control Settings";
			this.labelControl.Visible = false;
			// 
			// textBoxTextPrefaceControl
			// 
			this.textBoxTextPrefaceControl.Location = new System.Drawing.Point(479, 197);
			this.textBoxTextPrefaceControl.Multiline = true;
			this.textBoxTextPrefaceControl.Name = "textBoxTextPrefaceControl";
			this.textBoxTextPrefaceControl.ReadOnly = true;
			this.textBoxTextPrefaceControl.Size = new System.Drawing.Size(177, 43);
			this.textBoxTextPrefaceControl.TabIndex = 9;
			this.textBoxTextPrefaceControl.Visible = false;
			// 
			// textBoxTextControl
			// 
			this.textBoxTextControl.Location = new System.Drawing.Point(479, 246);
			this.textBoxTextControl.Multiline = true;
			this.textBoxTextControl.Name = "textBoxTextControl";
			this.textBoxTextControl.ReadOnly = true;
			this.textBoxTextControl.Size = new System.Drawing.Size(177, 57);
			this.textBoxTextControl.TabIndex = 10;
			this.textBoxTextControl.Visible = false;
			// 
			// labelText
			// 
			this.labelText.Location = new System.Drawing.Point(435, 246);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(43, 13);
			this.labelText.TabIndex = 98;
			this.labelText.Text = "Text";
			this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelText.Visible = false;
			// 
			// labelPrefaceText
			// 
			this.labelPrefaceText.Location = new System.Drawing.Point(395, 197);
			this.labelPrefaceText.Name = "labelPrefaceText";
			this.labelPrefaceText.Size = new System.Drawing.Size(83, 13);
			this.labelPrefaceText.TabIndex = 99;
			this.labelPrefaceText.Text = "Preface Text";
			this.labelPrefaceText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelPrefaceText.Visible = false;
			// 
			// listBoxControlToIncNum
			// 
			this.listBoxControlToIncNum.FormattingEnabled = true;
			this.listBoxControlToIncNum.Location = new System.Drawing.Point(181, 397);
			this.listBoxControlToIncNum.Name = "listBoxControlToIncNum";
			this.listBoxControlToIncNum.Size = new System.Drawing.Size(120, 95);
			this.listBoxControlToIncNum.TabIndex = 100;
			this.listBoxControlToIncNum.Visible = false;
			// 
			// textBoxTypeControl
			// 
			this.textBoxTypeControl.Location = new System.Drawing.Point(479, 119);
			this.textBoxTypeControl.Name = "textBoxTypeControl";
			this.textBoxTypeControl.ReadOnly = true;
			this.textBoxTypeControl.Size = new System.Drawing.Size(177, 20);
			this.textBoxTypeControl.TabIndex = 101;
			this.textBoxTypeControl.Visible = false;
			// 
			// labelTypeControl
			// 
			this.labelTypeControl.AutoSize = true;
			this.labelTypeControl.Location = new System.Drawing.Point(442, 122);
			this.labelTypeControl.Name = "labelTypeControl";
			this.labelTypeControl.Size = new System.Drawing.Size(31, 13);
			this.labelTypeControl.TabIndex = 102;
			this.labelTypeControl.Text = "Type";
			this.labelTypeControl.Visible = false;
			// 
			// butEditControl
			// 
			this.butEditControl.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditControl.Autosize = true;
			this.butEditControl.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditControl.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditControl.CornerRadius = 4F;
			this.butEditControl.Location = new System.Drawing.Point(32, 428);
			this.butEditControl.Name = "butEditControl";
			this.butEditControl.Size = new System.Drawing.Size(123, 25);
			this.butEditControl.TabIndex = 4;
			this.butEditControl.Text = "Edit Control";
			this.butEditControl.Visible = false;
			this.butEditControl.Click += new System.EventHandler(this.butEditControl_Click);
			// 
			// butCreateControl
			// 
			this.butCreateControl.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCreateControl.Autosize = true;
			this.butCreateControl.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreateControl.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreateControl.CornerRadius = 4F;
			this.butCreateControl.Location = new System.Drawing.Point(32, 397);
			this.butCreateControl.Name = "butCreateControl";
			this.butCreateControl.Size = new System.Drawing.Size(120, 25);
			this.butCreateControl.TabIndex = 3;
			this.butCreateControl.Text = "Create New Control";
			this.butCreateControl.Click += new System.EventHandler(this.butCreateControl_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(602, 422);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78, 25);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(602, 453);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(78, 25);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormAutoNoteEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(704, 495);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.labelTypeControl);
			this.Controls.Add(this.textBoxTypeControl);
			this.Controls.Add(this.listBoxControlToIncNum);
			this.Controls.Add(this.labelPrefaceText);
			this.Controls.Add(this.labelText);
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
			this.Activated += new System.EventHandler(this.FormAutoNoteEdit_Activated);
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
		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.Label labelPrefaceText;
		private System.Windows.Forms.ListBox listBoxControlToIncNum;
		private System.Windows.Forms.TextBox textBoxTypeControl;
		private System.Windows.Forms.Label labelTypeControl;
    }
}