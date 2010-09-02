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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ColorDialog colorDialog1;
		private OpenDental.UI.Button butColor;
		private TextBox textCodeRange;
		private Label label2;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.PanelOD panelODColor;
		public ProcApptColor ProcApptColorCur;

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
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.textCodeRange = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panelODColor = new OpenDental.UI.PanelOD();
			this.butDelete = new OpenDental.UI.Button();
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
			// textCodeRange
			// 
			this.textCodeRange.Location = new System.Drawing.Point(156,23);
			this.textCodeRange.Name = "textCodeRange";
			this.textCodeRange.Size = new System.Drawing.Size(200,20);
			this.textCodeRange.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(160,45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92,13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Ex: D1050-D1060";
			// 
			// panelODColor
			// 
			this.panelODColor.Location = new System.Drawing.Point(377,50);
			this.panelODColor.Name = "panelODColor";
			this.panelODColor.Size = new System.Drawing.Size(38,17);
			this.panelODColor.TabIndex = 125;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(54,97);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(92,24);
			this.butDelete.TabIndex = 124;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butColor
			// 
			this.butColor.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butColor.Autosize = true;
			this.butColor.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColor.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColor.CornerRadius = 4F;
			this.butColor.Location = new System.Drawing.Point(360,20);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(75,24);
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
			this.butOK.Location = new System.Drawing.Point(269,97);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
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
			this.butCancel.Location = new System.Drawing.Point(360,97);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 10;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormProcApptColorEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(464,144);
			this.Controls.Add(this.panelODColor);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textCodeRange);
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
			this.Load += new System.EventHandler(this.FormProcApptColorEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcApptColorEdit_Load(object sender,System.EventArgs e) {
			textCodeRange.Text=ProcApptColorCur.CodeRange;
			if(!ProcApptColorCur.IsNew) {
				panelODColor.BackColor=ProcApptColorCur.ColorText;
			}
			else { panelODColor.BackColor=Color.Black; }
			textCodeRange.Focus();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textCodeRange.Text.Trim()=="") {
				MessageBox.Show(Lan.g(this,"Code range cannot be blank."));
				return;
			}
			ProcApptColorCur.ColorText=panelODColor.BackColor;
			ProcApptColorCur.CodeRange=textCodeRange.Text;
			try {
				if(ProcApptColorCur.IsNew) {
					ProcApptColors.Insert(ProcApptColorCur);
				}
				else {
					ProcApptColors.Update(ProcApptColorCur);
				}
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butColor_Click(object sender,EventArgs e) {
			ColorDialog colorDlg=new ColorDialog();
			colorDlg.AllowFullOpen=false;
			colorDlg.AnyColor=true;
			colorDlg.SolidColorOnly=false;
			colorDlg.Color=panelODColor.BackColor;
			if(colorDlg.ShowDialog()==DialogResult.OK) {
				panelODColor.BackColor=colorDlg.Color;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(ProcApptColorCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this procedure color range?")) {
				return;
			}
			try {
				ProcApptColors.Delete(ProcApptColorCur.ProcApptColorNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
	}
}





















