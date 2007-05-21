using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormEmployee : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.Windows.Forms.ListBox listEmployees;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private ArrayList ALemployees;
		//private User user;

		///<summary></summary>
		public FormEmployee(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmployee));
			this.butOK = new OpenDental.UI.Button();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.listEmployees = new System.Windows.Forms.ListBox();
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
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butOK.Location = new System.Drawing.Point(278,400);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 16;
			this.butOK.Text = "&Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkHidden
			// 
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(236,28);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkHidden.Size = new System.Drawing.Size(118,24);
			this.checkHidden.TabIndex = 17;
			this.checkHidden.Text = "Show Hidden";
			this.checkHidden.Click += new System.EventHandler(this.checkHidden_Click);
			// 
			// listEmployees
			// 
			this.listEmployees.Location = new System.Drawing.Point(16,28);
			this.listEmployees.Name = "listEmployees";
			this.listEmployees.Size = new System.Drawing.Size(212,316);
			this.listEmployees.TabIndex = 20;
			this.listEmployees.DoubleClick += new System.EventHandler(this.listEmployees_DoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(16,354);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(78,26);
			this.butAdd.TabIndex = 21;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormEmployee
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butOK;
			this.ClientSize = new System.Drawing.Size(376,440);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listEmployees);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmployee";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employees";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormEmployee_Closing);
			this.Load += new System.EventHandler(this.FormEmployee_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmployee_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			Employees.Refresh();
			listEmployees.Items.Clear();
			ALemployees=new ArrayList();
			for(int i=0;i<Employees.ListLong.Length;i++){
				if(Employees.ListLong[i].IsHidden){
					if(checkHidden.Checked){
						ALemployees.Add(Employees.ListLong[i]);
						listEmployees.Items.Add(Employees.GetNameFL(Employees.ListLong[i])+"(hidden)");
					}
				}
				else{
					ALemployees.Add(Employees.ListLong[i]);
					listEmployees.Items.Add(Employees.GetNameFL(Employees.ListLong[i]));
				}
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormEmployeeEdit FormEE=new FormEmployeeEdit();
			FormEE.EmployeeCur=new Employee();
			FormEE.IsNew=true;
			FormEE.ShowDialog();
			FillList();
		}

		private void listEmployees_DoubleClick(object sender, System.EventArgs e) {
			if(listEmployees.SelectedIndex==-1){
				return;
			}
			FormEmployeeEdit FormEE=new FormEmployeeEdit();
			FormEE.EmployeeCur=(Employee)ALemployees[listEmployees.SelectedIndex];
			FormEE.ShowDialog();
			FillList();
		}

		private void checkHidden_Click(object sender, System.EventArgs e) {
			FillList();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void FormEmployee_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//if(user!=null){
				//SecurityLogs.MakeLogEntry("Employees","Altered Employees",user);
			//}
		}

	}
}
