using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormBlockoutCutCopyPaste:System.Windows.Forms.Form {
		private GroupBox groupBox1;
		private OpenDental.UI.Button butCopyWeek;
		private OpenDental.UI.Button butCopyDay;
		private GroupBox groupBox2;
		private OpenDental.UI.Button butRepeat;
		private Label label4;
		private CheckBox checkReplace;
		private TextBox textRepeat;
		private OpenDental.UI.Button butPaste;
		private CheckBox checkWeekend;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public static DateTime DateCopyStart=DateTime.MinValue;
		private TextBox textClipboard;
		private Label label3;
		private Label label1;
		public static DateTime DateCopyEnd=DateTime.MinValue;

		///<summary></summary>
		public FormBlockoutCutCopyPaste()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBlockoutCutCopyPaste));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkReplace = new System.Windows.Forms.CheckBox();
			this.textRepeat = new System.Windows.Forms.TextBox();
			this.checkWeekend = new System.Windows.Forms.CheckBox();
			this.textClipboard = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butCopyWeek = new OpenDental.UI.Button();
			this.butCopyDay = new OpenDental.UI.Button();
			this.butRepeat = new OpenDental.UI.Button();
			this.butPaste = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textClipboard);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.butCopyWeek);
			this.groupBox1.Controls.Add(this.butCopyDay);
			this.groupBox1.Location = new System.Drawing.Point(26,92);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(158,118);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Copy";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butRepeat);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.checkReplace);
			this.groupBox2.Controls.Add(this.textRepeat);
			this.groupBox2.Controls.Add(this.butPaste);
			this.groupBox2.Location = new System.Drawing.Point(26,220);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(158,97);
			this.groupBox2.TabIndex = 45;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Paste";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(70,70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37,14);
			this.label4.TabIndex = 32;
			this.label4.Text = "#";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// checkReplace
			// 
			this.checkReplace.Checked = true;
			this.checkReplace.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkReplace.Location = new System.Drawing.Point(6,14);
			this.checkReplace.Name = "checkReplace";
			this.checkReplace.Size = new System.Drawing.Size(146,18);
			this.checkReplace.TabIndex = 31;
			this.checkReplace.Text = "Replace Existing";
			this.checkReplace.UseVisualStyleBackColor = true;
			// 
			// textRepeat
			// 
			this.textRepeat.Location = new System.Drawing.Point(110,67);
			this.textRepeat.Name = "textRepeat";
			this.textRepeat.Size = new System.Drawing.Size(39,20);
			this.textRepeat.TabIndex = 31;
			this.textRepeat.Text = "1";
			// 
			// checkWeekend
			// 
			this.checkWeekend.Location = new System.Drawing.Point(32,68);
			this.checkWeekend.Name = "checkWeekend";
			this.checkWeekend.Size = new System.Drawing.Size(143,18);
			this.checkWeekend.TabIndex = 46;
			this.checkWeekend.Text = "Include Weekends";
			this.checkWeekend.UseVisualStyleBackColor = true;
			// 
			// textClipboard
			// 
			this.textClipboard.Location = new System.Drawing.Point(6,33);
			this.textClipboard.Name = "textClipboard";
			this.textClipboard.ReadOnly = true;
			this.textClipboard.Size = new System.Drawing.Size(146,20);
			this.textClipboard.TabIndex = 30;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3,16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(146,14);
			this.label3.TabIndex = 29;
			this.label3.Text = "Clipboard Contents";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(29,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(245,43);
			this.label1.TabIndex = 47;
			this.label1.Text = "Remember that this tool only applies to the visible operatories for the current a" +
    "ppointment view";
			// 
			// butCopyWeek
			// 
			this.butCopyWeek.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopyWeek.Autosize = true;
			this.butCopyWeek.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopyWeek.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopyWeek.CornerRadius = 4F;
			this.butCopyWeek.Location = new System.Drawing.Point(6,85);
			this.butCopyWeek.Name = "butCopyWeek";
			this.butCopyWeek.Size = new System.Drawing.Size(75,24);
			this.butCopyWeek.TabIndex = 28;
			this.butCopyWeek.Text = "Copy Week";
			// 
			// butCopyDay
			// 
			this.butCopyDay.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopyDay.Autosize = true;
			this.butCopyDay.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopyDay.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopyDay.CornerRadius = 4F;
			this.butCopyDay.Location = new System.Drawing.Point(6,58);
			this.butCopyDay.Name = "butCopyDay";
			this.butCopyDay.Size = new System.Drawing.Size(75,24);
			this.butCopyDay.TabIndex = 27;
			this.butCopyDay.Text = "Copy Day";
			// 
			// butRepeat
			// 
			this.butRepeat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRepeat.Autosize = true;
			this.butRepeat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRepeat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRepeat.CornerRadius = 4F;
			this.butRepeat.Location = new System.Drawing.Point(6,64);
			this.butRepeat.Name = "butRepeat";
			this.butRepeat.Size = new System.Drawing.Size(75,24);
			this.butRepeat.TabIndex = 30;
			this.butRepeat.Text = "Repeat";
			// 
			// butPaste
			// 
			this.butPaste.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPaste.Autosize = true;
			this.butPaste.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPaste.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPaste.CornerRadius = 4F;
			this.butPaste.Location = new System.Drawing.Point(6,37);
			this.butPaste.Name = "butPaste";
			this.butPaste.Size = new System.Drawing.Size(75,24);
			this.butPaste.TabIndex = 29;
			this.butPaste.Text = "Paste";
			// 
			// FormBlockoutCutCopyPaste
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(281,338);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkWeekend);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBlockoutCutCopyPaste";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Blockout Cut-Copy-Paste";
			this.Load += new System.EventHandler(this.FormBlockoutCutCopyPaste_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBlockoutCutCopyPaste_Load(object sender,EventArgs e) {

		}

		private void FillClipboard(){

		}



	}
}





















