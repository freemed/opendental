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
	public class FormInsFilingCodeEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textDescription;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textEclaimCode;
		private System.Windows.Forms.Label label2;
		public InsFilingCode InsFilingCodeCur;

		///<summary></summary>
		public FormInsFilingCodeEdit()
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
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(FormInsFilingCodeEdit));
			this.butCancel=new OpenDental.UI.Button();
			this.butOK=new OpenDental.UI.Button();
			this.label1=new System.Windows.Forms.Label();
			this.textDescription=new System.Windows.Forms.TextBox();
			this.butDelete=new OpenDental.UI.Button();
			this.textEclaimCode=new System.Windows.Forms.TextBox();
			this.label2=new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.Location=new System.Drawing.Point(456,107);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=10;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(365,107);
			this.butOK.Name="butOK";
			this.butOK.Size=new System.Drawing.Size(75,26);
			this.butOK.TabIndex=9;
			this.butOK.Text="&OK";
			this.butOK.Click+=new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location=new System.Drawing.Point(9,21);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(148,17);
			this.label1.TabIndex=2;
			this.label1.Text="Description";
			this.label1.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location=new System.Drawing.Point(160,20);
			this.textDescription.Name="textDescription";
			this.textDescription.Size=new System.Drawing.Size(291,20);
			this.textDescription.TabIndex=0;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butDelete.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize=true;
			this.butDelete.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius=4F;
			this.butDelete.Image=global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location=new System.Drawing.Point(27,107);
			this.butDelete.Name="butDelete";
			this.butDelete.Size=new System.Drawing.Size(81,26);
			this.butDelete.TabIndex=4;
			this.butDelete.Text="Delete";
			this.butDelete.Click+=new System.EventHandler(this.butDelete_Click);
			// 
			// textEclaimCode
			// 
			this.textEclaimCode.Location=new System.Drawing.Point(160,40);
			this.textEclaimCode.MaxLength=255;
			this.textEclaimCode.Name="textEclaimCode";
			this.textEclaimCode.Size=new System.Drawing.Size(157,20);
			this.textEclaimCode.TabIndex=1;
			// 
			// label2
			// 
			this.label2.Location=new System.Drawing.Point(8,43);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(151,17);
			this.label2.TabIndex=99;
			this.label2.Text="Eclaim Code";
			this.label2.TextAlign=System.Drawing.ContentAlignment.TopRight;
			// 
			// FormInsFilingCodeEdit
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(557,151);
			this.Controls.Add(this.textEclaimCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox=false;
			this.MinimizeBox=false;
			this.Name="FormInsFilingCodeEdit";
			this.ShowInTaskbar=false;
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Edit Claim Filing Code";
			this.Load+=new System.EventHandler(this.FormInsFilingCodeEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsFilingCodeEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=InsFilingCodeCur.Descript;
			textEclaimCode.Text=InsFilingCodeCur.EclaimCode;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(InsFilingCodeCur.IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this Insurance Filing Code?")) {
				return;
			}
			try{
				InsFilingCodes.DeleteObject(InsFilingCodeCur.InsFilingCodeNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(this.textDescription.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter a description."));
				return;
			}
			if(this.textEclaimCode.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter an electronic claim code."));
				return;
			}
			InsFilingCodeCur.Descript=textDescription.Text;
			InsFilingCodeCur.EclaimCode=textEclaimCode.Text;
			try{
				InsFilingCodes.WriteObject(InsFilingCodeCur);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}





















