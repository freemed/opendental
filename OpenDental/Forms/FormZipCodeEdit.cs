using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormZipCodeEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.CheckBox checkIsFrequent;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		public ZipCode ZipCodeCur;

		///<summary></summary>
		public FormZipCodeEdit(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZipCodeEdit));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.checkIsFrequent = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(303,153);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 4;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(303,187);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textZip
			// 
			this.textZip.Location = new System.Drawing.Point(110,17);
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(127,20);
			this.textZip.TabIndex = 3;
			// 
			// textState
			// 
			this.textState.Location = new System.Drawing.Point(110,79);
			this.textState.MaxLength = 20;
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(97,20);
			this.textState.TabIndex = 1;
			this.textState.TextChanged += new System.EventHandler(this.textState_TextChanged);
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(110,48);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(267,20);
			this.textCity.TabIndex = 0;
			this.textCity.TextChanged += new System.EventHandler(this.textCity_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95,15);
			this.label1.TabIndex = 5;
			this.label1.Text = "Zip Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(31,83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77,13);
			this.label2.TabIndex = 6;
			this.label2.Text = "ST";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15,52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92,14);
			this.label3.TabIndex = 7;
			this.label3.Text = "City";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsFrequent
			// 
			this.checkIsFrequent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsFrequent.Location = new System.Drawing.Point(110,109);
			this.checkIsFrequent.Name = "checkIsFrequent";
			this.checkIsFrequent.Size = new System.Drawing.Size(138,22);
			this.checkIsFrequent.TabIndex = 2;
			this.checkIsFrequent.Text = "Used Frequently";
			// 
			// FormZipCodeEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(398,227);
			this.Controls.Add(this.checkIsFrequent);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.textState);
			this.Controls.Add(this.textZip);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormZipCodeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Zip Code";
			this.Load += new System.EventHandler(this.FormZipCodeEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormZipCodeEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
	      this.Text=Lan.g(this,"Add Zip Code");
			}
			else{
				this.Text=Lan.g(this,"Edit Zip Code");
			}
			textZip.Text=ZipCodeCur.ZipCodeDigits;
			textCity.Text=ZipCodeCur.City;
			textState.Text=ZipCodeCur.State;
			checkIsFrequent.Checked=ZipCodeCur.IsFrequent;
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			if(textCity.Text.Length==1){
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender, System.EventArgs e){
			if(CultureInfo.CurrentCulture.Name=="en-US" //if USA or Canada, capitalize first 2 letters
				|| CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				if(textState.Text.Length==1 || textState.Text.Length==2){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=2;
				}
			}
			else{
				if(textState.Text.Length==1){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=1;
				}
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textZip.Text=="" || textCity.Text=="" || textState.Text==""){
				MessageBox.Show(Lan.g(this,"City,State, or Zip Cannot be left blank"));
				return;
			}
      ZipCodeCur.City=textCity.Text;
			ZipCodeCur.State=textState.Text;
			ZipCodeCur.ZipCodeDigits=textZip.Text;
			ZipCodeCur.IsFrequent=checkIsFrequent.Checked;
			if(IsNew){
				ZipCodes.Insert(ZipCodeCur);
			}
			else{
				ZipCodes.Update(ZipCodeCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}
