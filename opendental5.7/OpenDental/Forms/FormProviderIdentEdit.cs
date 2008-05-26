using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProviderIdentEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>Set this field externally before using this window.</summary>
		public ProviderIdent ProvIdentCur;
		private System.Windows.Forms.ListBox listType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textPayorID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textIDNumber;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormProviderIdentEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProviderIdentEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listType = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textPayorID = new System.Windows.Forms.TextBox();
			this.textIDNumber = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(382,220);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(382,179);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(162,44);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(120,95);
			this.listType.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(41,42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118,19);
			this.label1.TabIndex = 3;
			this.label1.Text = "Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14,22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(145,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Payor (Electronic) ID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPayorID
			// 
			this.textPayorID.Location = new System.Drawing.Point(162,20);
			this.textPayorID.Name = "textPayorID";
			this.textPayorID.Size = new System.Drawing.Size(90,20);
			this.textPayorID.TabIndex = 5;
			// 
			// textIDNumber
			// 
			this.textIDNumber.Location = new System.Drawing.Point(162,143);
			this.textIDNumber.Name = "textIDNumber";
			this.textIDNumber.Size = new System.Drawing.Size(90,20);
			this.textIDNumber.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(14,145);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(145,19);
			this.label3.TabIndex = 6;
			this.label3.Text = "Assigned ID Number";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormProviderIdentEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(483,269);
			this.Controls.Add(this.textIDNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textPayorID);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProviderIdentEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Provider Identification";
			this.Load += new System.EventHandler(this.FormProviderIdentEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProviderIdentEdit_Load(object sender, System.EventArgs e) {
			textPayorID.Text=ProvIdentCur.PayorID;
			//for(int i=0;i<.Length;i++){
			listType.Items.AddRange(Enum.GetNames(typeof(ProviderSupplementalID)));
			//}
			listType.SelectedIndex=(int)ProvIdentCur.SuppIDType;
			textIDNumber.Text=ProvIdentCur.IDNumber;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ProvIdentCur.PayorID=textPayorID.Text;
			ProvIdentCur.SuppIDType=(ProviderSupplementalID)listType.SelectedIndex;
			ProvIdentCur.IDNumber=textIDNumber.Text;
			if(IsNew){
				ProviderIdents.Insert(ProvIdentCur);
			}
			else{
				ProviderIdents.Update(ProvIdentCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















