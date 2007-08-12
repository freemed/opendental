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
					this.textBox1MainName = new System.Windows.Forms.TextBox();
					this.label1 = new System.Windows.Forms.Label();
					this.label2 = new System.Windows.Forms.Label();
					this.label3 = new System.Windows.Forms.Label();
					this.listBox1AutoNotes = new System.Windows.Forms.ListBox();
					this.textBox3AddOption = new System.Windows.Forms.TextBox();
					this.label4 = new System.Windows.Forms.Label();
					this.label5 = new System.Windows.Forms.Label();
					this.label6 = new System.Windows.Forms.Label();
					this.listBox3Options = new System.Windows.Forms.ListBox();
					this.textBox3Text = new System.Windows.Forms.TextBox();
					this.textBox3Name = new System.Windows.Forms.TextBox();
					this.listBox2AutoNoteVariables = new System.Windows.Forms.ListBox();
					this.label7 = new System.Windows.Forms.Label();
					this.listBox2Variables = new System.Windows.Forms.ListBox();
					this.textBox1 = new System.Windows.Forms.TextBox();
					this.button3Cancel = new OpenDental.UI.Button();
					this.button3EditVarDone = new OpenDental.UI.Button();
					this.button3AddOption = new OpenDental.UI.Button();
					this.button1EditAutoNote = new OpenDental.UI.Button();
					this.button1Cancel = new OpenDental.UI.Button();
					this.button1Create = new OpenDental.UI.Button();
					this.button2EditFinish = new OpenDental.UI.Button();
					this.button2Cancel = new OpenDental.UI.Button();
					this.button2EditVar = new OpenDental.UI.Button();
					this.label8 = new System.Windows.Forms.Label();
					this.SuspendLayout();
					// 
					// textBox1MainName
					// 
					this.textBox1MainName.Location = new System.Drawing.Point(444,124);
					this.textBox1MainName.Name = "textBox1MainName";
					this.textBox1MainName.Size = new System.Drawing.Size(356,20);
					this.textBox1MainName.TabIndex = 0;
					// 
					// label1
					// 
					this.label1.AutoSize = true;
					this.label1.Location = new System.Drawing.Point(365,124);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(73,13);
					this.label1.TabIndex = 1;
					this.label1.Text = "Enter A Name";
					this.label1.Click += new System.EventHandler(this.label1_Click);
					// 
					// label2
					// 
					this.label2.AutoSize = true;
					this.label2.Location = new System.Drawing.Point(240,54);
					this.label2.Name = "label2";
					this.label2.Size = new System.Drawing.Size(156,13);
					this.label2.TabIndex = 57;
					this.label2.Text = "Create A New Auto Note Below";
					// 
					// label3
					// 
					this.label3.AutoSize = true;
					this.label3.Location = new System.Drawing.Point(427,241);
					this.label3.Name = "label3";
					this.label3.Size = new System.Drawing.Size(138,13);
					this.label3.TabIndex = 58;
					this.label3.Text = "Select A Auto Note  To Edit";
					// 
					// listBox1AutoNotes
					// 
					this.listBox1AutoNotes.FormattingEnabled = true;
					this.listBox1AutoNotes.Location = new System.Drawing.Point(430,270);
					this.listBox1AutoNotes.Name = "listBox1AutoNotes";
					this.listBox1AutoNotes.Size = new System.Drawing.Size(120,147);
					this.listBox1AutoNotes.TabIndex = 59;
					// 
					// textBox3AddOption
					// 
					this.textBox3AddOption.Location = new System.Drawing.Point(630,298);
					this.textBox3AddOption.Name = "textBox3AddOption";
					this.textBox3AddOption.Size = new System.Drawing.Size(100,20);
					this.textBox3AddOption.TabIndex = 77;
					this.textBox3AddOption.Visible = false;
					// 
					// label4
					// 
					this.label4.AutoSize = true;
					this.label4.Location = new System.Drawing.Point(427,254);
					this.label4.Name = "label4";
					this.label4.Size = new System.Drawing.Size(165,13);
					this.label4.TabIndex = 76;
					this.label4.Text = "Options Double Click To Remove";
					this.label4.Visible = false;
					// 
					// label5
					// 
					this.label5.AutoSize = true;
					this.label5.Location = new System.Drawing.Point(441,102);
					this.label5.Name = "label5";
					this.label5.Size = new System.Drawing.Size(28,13);
					this.label5.TabIndex = 75;
					this.label5.Text = "Text";
					this.label5.Visible = false;
					// 
					// label6
					// 
					this.label6.AutoSize = true;
					this.label6.Location = new System.Drawing.Point(434,24);
					this.label6.Name = "label6";
					this.label6.Size = new System.Drawing.Size(35,13);
					this.label6.TabIndex = 74;
					this.label6.Text = "Name";
					this.label6.Visible = false;
					// 
					// listBox3Options
					// 
					this.listBox3Options.FormattingEnabled = true;
					this.listBox3Options.Location = new System.Drawing.Point(430,453);
					this.listBox3Options.Name = "listBox3Options";
					this.listBox3Options.Size = new System.Drawing.Size(120,147);
					this.listBox3Options.TabIndex = 73;
					this.listBox3Options.Visible = false;
					this.listBox3Options.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox3Options_MouseDoubleClick);
					// 
					// textBox3Text
					// 
					this.textBox3Text.Location = new System.Drawing.Point(475,98);
					this.textBox3Text.Name = "textBox3Text";
					this.textBox3Text.ReadOnly = true;
					this.textBox3Text.Size = new System.Drawing.Size(177,20);
					this.textBox3Text.TabIndex = 70;
					this.textBox3Text.Visible = false;
					// 
					// textBox3Name
					// 
					this.textBox3Name.Location = new System.Drawing.Point(475,21);
					this.textBox3Name.Name = "textBox3Name";
					this.textBox3Name.ReadOnly = true;
					this.textBox3Name.Size = new System.Drawing.Size(177,20);
					this.textBox3Name.TabIndex = 69;
					this.textBox3Name.Visible = false;
					// 
					// listBox2AutoNoteVariables
					// 
					this.listBox2AutoNoteVariables.FormattingEnabled = true;
					this.listBox2AutoNoteVariables.Location = new System.Drawing.Point(243,70);
					this.listBox2AutoNoteVariables.Name = "listBox2AutoNoteVariables";
					this.listBox2AutoNoteVariables.Size = new System.Drawing.Size(120,316);
					this.listBox2AutoNoteVariables.TabIndex = 79;
					this.listBox2AutoNoteVariables.Visible = false;
					this.listBox2AutoNoteVariables.DoubleClick += new System.EventHandler(this.listBox2AutoNoteVariables_DoubleClick);
					// 
					// label7
					// 
					this.label7.AutoSize = true;
					this.label7.Location = new System.Drawing.Point(240,28);
					this.label7.Name = "label7";
					this.label7.Size = new System.Drawing.Size(129,13);
					this.label7.TabIndex = 84;
					this.label7.Text = " Double Click To Remove";
					this.label7.Visible = false;
					// 
					// listBox2Variables
					// 
					this.listBox2Variables.FormattingEnabled = true;
					this.listBox2Variables.Location = new System.Drawing.Point(35,70);
					this.listBox2Variables.Name = "listBox2Variables";
					this.listBox2Variables.Size = new System.Drawing.Size(120,316);
					this.listBox2Variables.TabIndex = 85;
					this.listBox2Variables.Visible = false;
					this.listBox2Variables.DoubleClick += new System.EventHandler(this.listBox2Variables_DoubleClick);
					// 
					// textBox1
					// 
					this.textBox1.Location = new System.Drawing.Point(55,392);
					this.textBox1.Name = "textBox1";
					this.textBox1.Size = new System.Drawing.Size(100,20);
					this.textBox1.TabIndex = 86;
					this.textBox1.Visible = false;
					// 
					// button3Cancel
					// 
					this.button3Cancel.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button3Cancel.Autosize = true;
					this.button3Cancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button3Cancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button3Cancel.CornerRadius = 4F;
					this.button3Cancel.Location = new System.Drawing.Point(577,606);
					this.button3Cancel.Name = "button3Cancel";
					this.button3Cancel.Size = new System.Drawing.Size(91,25);
					this.button3Cancel.TabIndex = 88;
					this.button3Cancel.Text = "Cancel";
					this.button3Cancel.Visible = false;
					this.button3Cancel.Click += new System.EventHandler(this.button3Cancel_Click);
					// 
					// button3EditVarDone
					// 
					this.button3EditVarDone.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button3EditVarDone.Autosize = true;
					this.button3EditVarDone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button3EditVarDone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button3EditVarDone.CornerRadius = 4F;
					this.button3EditVarDone.Location = new System.Drawing.Point(577,575);
					this.button3EditVarDone.Name = "button3EditVarDone";
					this.button3EditVarDone.Size = new System.Drawing.Size(91,25);
					this.button3EditVarDone.TabIndex = 87;
					this.button3EditVarDone.Text = "Done";
					this.button3EditVarDone.Visible = false;
					this.button3EditVarDone.Click += new System.EventHandler(this.button3EditVarDone_Click);
					// 
					// button3AddOption
					// 
					this.button3AddOption.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button3AddOption.Autosize = true;
					this.button3AddOption.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button3AddOption.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button3AddOption.CornerRadius = 4F;
					this.button3AddOption.Location = new System.Drawing.Point(748,293);
					this.button3AddOption.Name = "button3AddOption";
					this.button3AddOption.Size = new System.Drawing.Size(91,25);
					this.button3AddOption.TabIndex = 78;
					this.button3AddOption.Text = "Add";
					this.button3AddOption.Visible = false;
					this.button3AddOption.Click += new System.EventHandler(this.buttonAddOption_Click);
					// 
					// button1EditAutoNote
					// 
					this.button1EditAutoNote.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button1EditAutoNote.Autosize = true;
					this.button1EditAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button1EditAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button1EditAutoNote.CornerRadius = 4F;
					this.button1EditAutoNote.Location = new System.Drawing.Point(577,392);
					this.button1EditAutoNote.Name = "button1EditAutoNote";
					this.button1EditAutoNote.Size = new System.Drawing.Size(91,25);
					this.button1EditAutoNote.TabIndex = 72;
					this.button1EditAutoNote.Text = "Edit";
					this.button1EditAutoNote.Click += new System.EventHandler(this.button1_Click);
					// 
					// button1Cancel
					// 
					this.button1Cancel.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button1Cancel.Autosize = true;
					this.button1Cancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button1Cancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button1Cancel.CornerRadius = 4F;
					this.button1Cancel.Location = new System.Drawing.Point(806,133);
					this.button1Cancel.Name = "button1Cancel";
					this.button1Cancel.Size = new System.Drawing.Size(91,25);
					this.button1Cancel.TabIndex = 56;
					this.button1Cancel.Text = "Cancel";
					this.button1Cancel.Click += new System.EventHandler(this.buttonCancel_Click);
					// 
					// button1Create
					// 
					this.button1Create.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button1Create.Autosize = true;
					this.button1Create.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button1Create.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button1Create.CornerRadius = 4F;
					this.button1Create.Location = new System.Drawing.Point(806,102);
					this.button1Create.Name = "button1Create";
					this.button1Create.Size = new System.Drawing.Size(91,25);
					this.button1Create.TabIndex = 55;
					this.button1Create.Text = "Create";
					this.button1Create.Click += new System.EventHandler(this.butCreate_Click);
					// 
					// button2EditFinish
					// 
					this.button2EditFinish.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button2EditFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.button2EditFinish.Autosize = true;
					this.button2EditFinish.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button2EditFinish.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button2EditFinish.CornerRadius = 4F;
					this.button2EditFinish.Location = new System.Drawing.Point(806,575);
					this.button2EditFinish.Name = "button2EditFinish";
					this.button2EditFinish.Size = new System.Drawing.Size(91,25);
					this.button2EditFinish.TabIndex = 83;
					this.button2EditFinish.Text = "Finish";
					this.button2EditFinish.Visible = false;
					this.button2EditFinish.Click += new System.EventHandler(this.buttonEditFinish_Click);
					// 
					// button2Cancel
					// 
					this.button2Cancel.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button2Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.button2Cancel.Autosize = true;
					this.button2Cancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button2Cancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button2Cancel.CornerRadius = 4F;
					this.button2Cancel.Location = new System.Drawing.Point(806,636);
					this.button2Cancel.Name = "button2Cancel";
					this.button2Cancel.Size = new System.Drawing.Size(91,25);
					this.button2Cancel.TabIndex = 82;
					this.button2Cancel.Text = "Cancel";
					this.button2Cancel.Visible = false;
					this.button2Cancel.Click += new System.EventHandler(this.button3_Click);
					// 
					// button2EditVar
					// 
					this.button2EditVar.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.button2EditVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.button2EditVar.Autosize = true;
					this.button2EditVar.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.button2EditVar.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.button2EditVar.CornerRadius = 4F;
					this.button2EditVar.Location = new System.Drawing.Point(806,606);
					this.button2EditVar.Name = "button2EditVar";
					this.button2EditVar.Size = new System.Drawing.Size(91,25);
					this.button2EditVar.TabIndex = 81;
					this.button2EditVar.Text = "Edit";
					this.button2EditVar.Visible = false;
					this.button2EditVar.Click += new System.EventHandler(this.buttonEditVar_Click);
					// 
					// label8
					// 
					this.label8.AutoSize = true;
					this.label8.Location = new System.Drawing.Point(32,54);
					this.label8.Name = "label8";
					this.label8.Size = new System.Drawing.Size(162,13);
					this.label8.TabIndex = 89;
					this.label8.Text = "Double Click A Variable To Add  ";
					this.label8.Visible = false;
					// 
					// FormAutoNoteEdit
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(909,673);
					this.Controls.Add(this.label8);
					this.Controls.Add(this.button3Cancel);
					this.Controls.Add(this.button3EditVarDone);
					this.Controls.Add(this.textBox1);
					this.Controls.Add(this.button3AddOption);
					this.Controls.Add(this.textBox3AddOption);
					this.Controls.Add(this.label4);
					this.Controls.Add(this.label5);
					this.Controls.Add(this.label6);
					this.Controls.Add(this.listBox3Options);
					this.Controls.Add(this.button1EditAutoNote);
					this.Controls.Add(this.textBox3Text);
					this.Controls.Add(this.textBox3Name);
					this.Controls.Add(this.listBox1AutoNotes);
					this.Controls.Add(this.label3);
					this.Controls.Add(this.label2);
					this.Controls.Add(this.button1Cancel);
					this.Controls.Add(this.button1Create);
					this.Controls.Add(this.label1);
					this.Controls.Add(this.textBox1MainName);
					this.Controls.Add(this.label7);
					this.Controls.Add(this.listBox2AutoNoteVariables);
					this.Controls.Add(this.listBox2Variables);
					this.Controls.Add(this.button2EditFinish);
					this.Controls.Add(this.button2Cancel);
					this.Controls.Add(this.button2EditVar);
					this.Name = "FormAutoNoteEdit";
					this.ShowInTaskbar = false;
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Auto Note";
					this.Load += new System.EventHandler(this.FormAutoNoteEdit_Load);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1MainName;
        private System.Windows.Forms.Label label1;
        private OpenDental.UI.Button button1Create;
        private OpenDental.UI.Button button1Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1AutoNotes;
        private OpenDental.UI.Button button3AddOption;
        private System.Windows.Forms.TextBox textBox3AddOption;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox3Options;
        private OpenDental.UI.Button button1EditAutoNote;
        private System.Windows.Forms.TextBox textBox3Text;
        private System.Windows.Forms.TextBox textBox3Name;
        private System.Windows.Forms.ListBox listBox2AutoNoteVariables;
        private OpenDental.UI.Button button2EditVar;
        private OpenDental.UI.Button button2Cancel;
        private OpenDental.UI.Button button2EditFinish;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox2Variables;
        private System.Windows.Forms.TextBox textBox1;
        private OpenDental.UI.Button button3EditVarDone;
        private OpenDental.UI.Button button3Cancel;
        private System.Windows.Forms.Label label8;
    }
}