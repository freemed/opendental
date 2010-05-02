using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormDisplayFieldCategories:System.Windows.Forms.Form {
		private Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private bool changed;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private ListBox listCategory;
		//private List<DisplayField> ListShowing;
		//private List<DisplayField> ListAvailable;

		///<summary></summary>
		public FormDisplayFieldCategories()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplayFieldCategories));
			this.label1 = new System.Windows.Forms.Label();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(23,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(194,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Select a category";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listCategory
			// 
			this.listCategory.FormattingEnabled = true;
			this.listCategory.Location = new System.Drawing.Point(23,34);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(155,69);
			this.listCategory.TabIndex = 57;
			this.listCategory.DoubleClick += new System.EventHandler(this.listCategory_DoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(92,133);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 56;
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
			this.butCancel.Location = new System.Drawing.Point(173,133);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormDisplayFieldCategories
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(271,172);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDisplayFieldCategories";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Display Fields";
			this.Load += new System.EventHandler(this.FormDisplayFields_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDisplayFields_FormClosing);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDisplayFields_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(DisplayFieldCategory)).Length;i++){
				listCategory.Items.Add(Enum.GetNames(typeof(DisplayFieldCategory))[i]);
			}
			listCategory.SelectedIndex=0;
		}

		private void listCategory_DoubleClick(object sender,EventArgs e) {
			FormDisplayFields FormF=new FormDisplayFields();
			FormF.category=(DisplayFieldCategory)listCategory.SelectedIndex;
			FormF.ShowDialog();
			Close();
		}

		private void butOK_Click(object sender,EventArgs e) {
			FormDisplayFields FormF=new FormDisplayFields();
			FormF.category=(DisplayFieldCategory)listCategory.SelectedIndex;
			FormF.ShowDialog();
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormDisplayFields_FormClosing(object sender,FormClosingEventArgs e) {

		}

		

		

		

		

		

		

		


	}
}





















