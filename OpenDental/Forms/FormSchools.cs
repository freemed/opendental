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
	public class FormSchools : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listSchools;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormSchools()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchools));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listSchools = new System.Windows.Forms.ListBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(338,605);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86,26);
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
			this.butOK.Location = new System.Drawing.Point(338,564);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(86,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listSchools
			// 
			this.listSchools.Location = new System.Drawing.Point(12,66);
			this.listSchools.Name = "listSchools";
			this.listSchools.Size = new System.Drawing.Size(298,563);
			this.listSchools.TabIndex = 2;
			this.listSchools.DoubleClick += new System.EventHandler(this.listSchools_DoubleClick);
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
			this.butDelete.Location = new System.Drawing.Point(338,461);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86,26);
			this.butDelete.TabIndex = 72;
			this.butDelete.Text = "&Remove";
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
			this.butAdd.Location = new System.Drawing.Point(338,423);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(86,26);
			this.butAdd.TabIndex = 71;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(438,50);
			this.label1.TabIndex = 73;
			this.label1.Text = "This list is usually used in public health.  It may be used for any location purp" +
    "ose such as grade schools, nursing homes, etc.  It is frequently used when mobil" +
    "e clinics are used.";
			// 
			// FormSchools
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(464,646);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listSchools);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchools";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sites";
			this.Load += new System.EventHandler(this.FormSchools_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSchools_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			Schools.Refresh();
			listSchools.Items.Clear();
			string s="";
			for(int i=0;i<Schools.List.Length;i++){
				s=Schools.List[i].SchoolName;
				if(Schools.List[i].SchoolCode != ""){
					s+=", "+Schools.List[i].SchoolCode;
				}
				listSchools.Items.Add(s);
			}
		}

		private void listSchools_DoubleClick(object sender, System.EventArgs e) {
			if(listSchools.SelectedIndex==-1){
				return;
			}
			FormSchoolEdit FormSE=new FormSchoolEdit();
			FormSE.SchoolCur=Schools.List[listSchools.SelectedIndex];
			FormSE.ShowDialog();
			if(FormSE.DialogResult!=DialogResult.OK){
				return;
			}
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormSchoolEdit FormSE=new FormSchoolEdit();
			FormSE.SchoolCur=new School();
			FormSE.IsNew=true;
			FormSE.ShowDialog();
			if(FormSE.DialogResult!=DialogResult.OK){
				return;
			}
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listSchools.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			School SchoolCur=Schools.List[listSchools.SelectedIndex];
			string usedBy=Schools.UsedBy(SchoolCur.SchoolName);
			if(usedBy != ""){
				MessageBox.Show(Lan.g(this,"Cannot delete site because it is already in use by the following patients: \r")+usedBy);
				return;
			}
			Schools.Delete(SchoolCur);
			FillList();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















