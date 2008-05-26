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
	public class FormReqNeededEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.TextBox textDescript;
    public ReqNeeded ReqCur;

		///<summary></summary>
		public FormReqNeededEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReqNeededEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(474,113);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(375,113);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(168,25);
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(381,20);
			this.textDescript.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(11,25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(155,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(50,113);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(95,26);
			this.butDelete.TabIndex = 2;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormReqNeededEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(601,165);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReqNeededEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Requirement";
			this.Load += new System.EventHandler(this.FormReqNeededEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormReqNeededEdit_Load(object sender, System.EventArgs e) {
			/*for(int i=0;i<SchoolClasses.List.Length;i++) {
				comboClass.Items.Add(SchoolClasses.GetDescript(SchoolClasses.List[i]));
				if(SchoolClasses.List[i].SchoolClassNum==ReqCur.SchoolClassNum){
					comboClass.SelectedIndex=i;
				}
			}
			for(int i=0;i<SchoolCourses.List.Length;i++) {
				comboCourse.Items.Add(SchoolCourses.GetDescript(SchoolCourses.List[i]));
				if(SchoolCourses.List[i].SchoolCourseNum==ReqCur.SchoolCourseNum) {
					comboCourse.SelectedIndex=i;
				}
			}*/
			textDescript.Text=ReqCur.Descript;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				string inuseby=ReqStudents.InUseBy(ReqCur.ReqNeededNum);
				if(inuseby!=""){
					if(MessageBox.Show(Lan.g(this,"Requirement is already in use by student(s) with grade point(s) attached:")
						+"\r\n"+inuseby+Lan.g(this,"Delete anyway?  Student grades will not be affected."),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					{
						return;
					}
				}
				if(!MsgBox.Show(this,true,"Delete this Requirement?")){
					return;
				}
				try{
					ReqNeededs.Delete(ReqCur.ReqNeededNum);
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescript.Text==""){
				MsgBox.Show(this,"Please enter a description first.");
				return;
			}
			ReqCur.Descript=textDescript.Text;
			//ReqCur.SchoolClassNum=SchoolClasses.List[comboClass.SelectedIndex].SchoolClassNum;
			//ReqCur.SchoolCourseNum=SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum;
			if(IsNew){
				ReqNeededs.Insert(ReqCur);
			}
			else{
				ReqNeededs.Update(ReqCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















