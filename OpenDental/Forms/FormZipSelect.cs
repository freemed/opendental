using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormZipSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox listMatches;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butEdit;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.Container components=null;
		private bool changed;
		public ZipCode ZipSelected;

		///<summary></summary>
		public FormZipSelect(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZipSelect));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.listMatches = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
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
			this.butOK.Location = new System.Drawing.Point(336,153);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 0;
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
			this.butCancel.Location = new System.Drawing.Point(336,186);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listMatches
			// 
			this.listMatches.Location = new System.Drawing.Point(24,45);
			this.listMatches.Name = "listMatches";
			this.listMatches.Size = new System.Drawing.Size(197,95);
			this.listMatches.TabIndex = 3;
			this.listMatches.DoubleClick += new System.EventHandler(this.listMatches_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(25,23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(377,16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Cities attached to this zipcode:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Image = global::OpenDental.Properties.Resources.editPencil;
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(208,186);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75,26);
			this.butEdit.TabIndex = 12;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
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
			this.butDelete.Location = new System.Drawing.Point(119,186);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 11;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(30,186);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormZipSelect
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(430,235);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listMatches);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormZipSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Zipcode";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormZipSelect_Closing);
			this.Load += new System.EventHandler(this.FormZipSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		//This form is only accessed directly from the patient edit window, either by pushing the
		//button, or when user enters a zipcode that has more than one city available.
		
		private void FormZipSelect_Load(object sender, System.EventArgs e) {
		  FillList();
		}
		
		private void FillList(){
			//refreshing is done within each routine
			listMatches.Items.Clear();
			string itemText="";
			for(int i=0;i<ZipCodes.ALMatches.Count;i++){ 
				itemText=((ZipCode)ZipCodes.ALMatches[i]).City+" "
					+((ZipCode)ZipCodes.ALMatches[i]).State;
				if(((ZipCode)ZipCodes.ALMatches[i]).IsFrequent){
					itemText+=Lan.g(this," (freq)");
				}
				listMatches.Items.Add(itemText);
			}
			listMatches.SelectedIndex=-1;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.ZipCodeCur=new ZipCode();
			FormZCE.ZipCodeCur.ZipCodeDigits=((ZipCode)ZipCodes.ALMatches[0]).ZipCodeDigits;
			FormZCE.IsNew=true;
			FormZCE.ShowDialog();
			if(FormZCE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			ZipCodes.Refresh();
			ZipCodes.GetALMatches(FormZCE.ZipCodeCur.ZipCodeDigits);
			FillList();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.ZipCodeCur=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			FormZCE.ShowDialog();
			if(FormZCE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			ZipCodes.Refresh();
			ZipCodes.GetALMatches(FormZCE.ZipCodeCur.ZipCodeDigits);
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ZipCode ZipCodeCur=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			ZipCodes.Delete(ZipCodeCur);
			changed=true;
			ZipCodes.Refresh();
			//next line might not be right.
			ZipCodes.GetALMatches(ZipCodeCur.ZipCodeDigits);
			FillList();
		}

		private void listMatches_DoubleClick(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				return;
			}
			ZipSelected=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			DialogResult=DialogResult.OK;		
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listMatches.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			ZipSelected=(ZipCode)ZipCodes.ALMatches[listMatches.SelectedIndex];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormZipSelect_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.ZipCodes);
			}
		}

		


	}
}





