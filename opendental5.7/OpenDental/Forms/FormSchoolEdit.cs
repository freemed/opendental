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
	public class FormSchoolEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textSchoolName;
		private System.Windows.Forms.TextBox textSchoolCode;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		public School SchoolCur;

		///<summary></summary>
		public FormSchoolEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchoolEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textSchoolName = new System.Windows.Forms.TextBox();
			this.textSchoolCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(355,182);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86,26);
			this.butCancel.TabIndex = 3;
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
			this.butOK.Location = new System.Drawing.Point(355,147);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(86,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130,20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Site Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSchoolName
			// 
			this.textSchoolName.Location = new System.Drawing.Point(144,47);
			this.textSchoolName.Name = "textSchoolName";
			this.textSchoolName.Size = new System.Drawing.Size(297,20);
			this.textSchoolName.TabIndex = 0;
			this.textSchoolName.TextChanged += new System.EventHandler(this.textSchoolName_TextChanged);
			// 
			// textSchoolCode
			// 
			this.textSchoolCode.Location = new System.Drawing.Point(144,83);
			this.textSchoolCode.Name = "textSchoolCode";
			this.textSchoolCode.Size = new System.Drawing.Size(297,20);
			this.textSchoolCode.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(64,86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78,52);
			this.label2.TabIndex = 4;
			this.label2.Text = "Site Code (use varies)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormSchoolEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(475,239);
			this.Controls.Add(this.textSchoolCode);
			this.Controls.Add(this.textSchoolName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchoolEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Site Edit";
			this.Load += new System.EventHandler(this.FormSchoolEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormSchoolEdit_Load(object sender, System.EventArgs e) {
			textSchoolName.Text=SchoolCur.SchoolName;
			textSchoolCode.Text=SchoolCur.SchoolCode;
		}

		private void textSchoolName_TextChanged(object sender, System.EventArgs e) {
			if(textSchoolName.Text.Length==1){
				textSchoolName.Text=textSchoolName.Text.ToUpper();
				textSchoolName.SelectionStart=1;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SchoolCur.SchoolName=textSchoolName.Text;
			SchoolCur.SchoolCode=textSchoolCode.Text;
			if(IsNew){
				if(Schools.DoesExist(SchoolCur.SchoolName)){
					MessageBox.Show(Lan.g(this,"School name already exists. Duplicate not allowed."));
					return;
				}
				Schools.Insert(SchoolCur);
			}
			else{//existing school
				if(SchoolCur.SchoolName!=SchoolCur.OldSchoolName){//school name was changed
					if(Schools.DoesExist(SchoolCur.SchoolName)){//changed to a name that already exists.
						MessageBox.Show(Lan.g(this,"School name already exists. Duplicate not allowed."));
						return;
					}
				}
				Schools.Update(SchoolCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















