using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormInstructors : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listInstructors;
		//private bool changed;
		
		///<summary></summary>
		public FormInstructors(){
			InitializeComponent();
			//Providers.Selected=-1;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInstructors));
			this.listInstructors = new System.Windows.Forms.ListBox();
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listInstructors
			// 
			this.listInstructors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listInstructors.Location = new System.Drawing.Point(16,12);
			this.listInstructors.Name = "listInstructors";
			this.listInstructors.Size = new System.Drawing.Size(790,524);
			this.listInstructors.TabIndex = 4;
			this.listInstructors.DoubleClick += new System.EventHandler(this.listInstructors_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(734,554);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(16,553);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormInstructors
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(825,596);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listInstructors);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInstructors";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Instructors";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormInstructors_Closing);
			this.Load += new System.EventHandler(this.FormInstructors_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInstructors_Load(object sender, System.EventArgs e) {
			//FillList();
		}

		private void FillList(){
			/*int previousSelected=-1;
			if(listInstructors.SelectedIndex!=-1){
				previousSelected=Instructors.List[listInstructors.SelectedIndex].InstructorNum;
			}
			Instructors.Refresh();
			listInstructors.Items.Clear();
			for(int i=0;i<Instructors.List.Length;i++){
				listInstructors.Items.Add(Instructors.List[i].LName+", "+Instructors.List[i].FName+", "+Instructors.List[i].Suffix);
				if(Instructors.List[i].InstructorNum==previousSelected){
					listInstructors.SelectedIndex=i;
				}
			}*/
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			/*Instructor cur=new Instructor();
			FormInstructorEdit FormI=new FormInstructorEdit(cur);
			FormI.IsNew=true;
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillList();
			listInstructors.SelectedIndex=-1;*/
		}

		private void listInstructors_DoubleClick(object sender, System.EventArgs e) {
			/*if(listInstructors.SelectedIndex==-1)
				return;
			FormInstructorEdit FormI=new FormInstructorEdit(Instructors.List[listInstructors.SelectedIndex]);
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillList();*/
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			//Close();
		}

		private void FormInstructors_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//if(changed){
			//	DataValid.SetInvalid(InvalidTypes.DentalSchools);
			//}
		}

	}
}
