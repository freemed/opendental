using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProcApptColorEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textStoreName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ColorDialog colorDialog1;
		private OpenDental.UI.Button butColor;
		private ListBox listBox1;
		private TextBox textBox1;
		private Label label2;
		public Pharmacy PharmCur;

		///<summary></summary>
		public FormProcApptColorEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcApptColorEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.textStoreName = new System.Windows.Forms.TextBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butColor = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6,23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Code Range";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listBox1
			// 
			this.listBox1.BackColor = System.Drawing.SystemColors.ControlText;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(183,86);
			this.listBox1.Margin = new System.Windows.Forms.Padding(0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(50,30);
			this.listBox1.TabIndex = 14;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(183,23);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(200,20);
			this.textBox1.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(186,53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92,13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Ex: D1050-D1060";
			// 
			// butColor
			// 
			this.butColor.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butColor.Autosize = true;
			this.butColor.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColor.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColor.CornerRadius = 4F;
			this.butColor.Location = new System.Drawing.Point(79,86);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(75,30);
			this.butColor.TabIndex = 13;
			this.butColor.Text = "Color";
			this.butColor.UseVisualStyleBackColor = true;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(248,157);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 9;
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
			this.butCancel.Location = new System.Drawing.Point(339,157);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 10;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormProcApptColorEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(443,204);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.butColor);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcApptColorEdit";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit ProcApptColor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcApptColorEdit_Load(object sender,System.EventArgs e) {

		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butColor_Click(object sender,EventArgs e) {
			ColorDialog colorDlg = new ColorDialog();
			colorDlg.AllowFullOpen = false;
			colorDlg.AnyColor = true;
			colorDlg.SolidColorOnly = false;
			colorDlg.Color = Color.Black;

			if(colorDlg.ShowDialog() == DialogResult.OK) {
				listBox1.BackColor = colorDlg.Color;
			}
		}

		

		

		

		


	}
}





















