using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary></summary>
	public class FormLicense : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private RichTextBox richTextAgreement;
		private RichTextBox textGPL;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormLicense()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicense));
			this.butClose = new OpenDental.UI.Button();
			this.richTextAgreement = new System.Windows.Forms.RichTextBox();
			this.textGPL = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(764,618);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// richTextAgreement
			// 
			this.richTextAgreement.Location = new System.Drawing.Point(12,5);
			this.richTextAgreement.Name = "richTextAgreement";
			this.richTextAgreement.Size = new System.Drawing.Size(827,300);
			this.richTextAgreement.TabIndex = 9;
			this.richTextAgreement.Text = "";
			// 
			// textGPL
			// 
			this.textGPL.Location = new System.Drawing.Point(12,311);
			this.textGPL.Name = "textGPL";
			this.textGPL.Size = new System.Drawing.Size(827,300);
			this.textGPL.TabIndex = 10;
			this.textGPL.Text = "";
			// 
			// FormLicense
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(851,656);
			this.Controls.Add(this.textGPL);
			this.Controls.Add(this.richTextAgreement);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLicense";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Licenses";
			this.Load += new System.EventHandler(this.FormLicense_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLicense_Load(object sender,EventArgs e) {
			richTextAgreement.Rtf=Properties.Resources.CDT_Content_End_User_License;
			textGPL.Text=Properties.Resources.GPL;
		}
		
		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		


	}
}





















