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
	public class FormEmployers : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listEmp;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butEdit;
		private System.Windows.Forms.ToolTip toolTip1;
		private OpenDental.UI.Button butCombine;
		//<summary>Set to true if using this dialog to select an employer.</summary>
		//public bool IsSelectMode;

		///<summary></summary>
		public FormEmployers()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmployers));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listEmp = new System.Windows.Forms.ListBox();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butEdit = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butCombine = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(349,597);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79,26);
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
			this.butOK.Location = new System.Drawing.Point(349,560);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(79,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listEmp
			// 
			this.listEmp.Location = new System.Drawing.Point(18,17);
			this.listEmp.Name = "listEmp";
			this.listEmp.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listEmp.Size = new System.Drawing.Size(265,602);
			this.listEmp.TabIndex = 2;
			this.listEmp.DoubleClick += new System.EventHandler(this.listEmp_DoubleClick);
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
			this.butAdd.Location = new System.Drawing.Point(349,352);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
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
			this.butDelete.Location = new System.Drawing.Point(349,389);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(79,26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
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
			this.butEdit.Location = new System.Drawing.Point(349,426);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(79,26);
			this.butEdit.TabIndex = 9;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butCombine
			// 
			this.butCombine.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCombine.Autosize = true;
			this.butCombine.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCombine.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCombine.CornerRadius = 4F;
			this.butCombine.Location = new System.Drawing.Point(349,463);
			this.butCombine.Name = "butCombine";
			this.butCombine.Size = new System.Drawing.Size(79,26);
			this.butCombine.TabIndex = 10;
			this.butCombine.Text = "Co&mbine";
			this.toolTip1.SetToolTip(this.butCombine,"Combines multiple Employers");
			this.butCombine.Click += new System.EventHandler(this.butCombine_Click);
			// 
			// FormEmployers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(447,649);
			this.Controls.Add(this.listEmp);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butCombine);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butEdit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmployers";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employers";
			this.Load += new System.EventHandler(this.FormEmployers_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmployers_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Employers.Refresh();
			listEmp.Items.Clear();
			for(int i=0;i<Employers.List.Length;i++){
				listEmp.Items.Add(Employers.List[i].EmpName);
				//if(IsSelectMode && Employers.List[i].EmployerNum==Employers.Cur.EmployerNum){
				//	listEmp.SetSelected(i,true);
				//}
			}
		}

		private void listEmp_DoubleClick(object sender, System.EventArgs e) {
			if(listEmp.SelectedIndices.Count==0)
				return;
			//EmployerCur=
			//if(IsSelectMode){
			//	DialogResult=DialogResult.OK;
			//	return;
			//}
			FormEmployerEdit FormEE=new FormEmployerEdit();
			FormEE.EmployerCur=Employers.List[listEmp.SelectedIndices[0]];
			FormEE.ShowDialog();
			if(FormEE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormEmployerEdit FormEE=new FormEmployerEdit();
			FormEE.EmployerCur=new Employer();
			FormEE.IsNew=true;
			FormEE.ShowDialog();
			FillGrid();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listEmp.SelectedIndices.Count!=1){
				MessageBox.Show(Lan.g(this,"Please select one item first."));
				return;
			}
			//Employers.Cur=;
			//make sure no dependent patients:
			string dependentNames=Employers.DependentPatients(Employers.List[listEmp.SelectedIndices[0]]);
			if(dependentNames!=""){
				MessageBox.Show(Lan.g(this,"Not allowed to delete this employer because it it attached to "
					+"the following patients.  You should combine employers instead.")
					+"\r\n\r\n"+dependentNames);
					return;
			}
			//make sure no dependent insplans:
			dependentNames=Employers.DependentInsPlans(Employers.List[listEmp.SelectedIndices[0]]);
			if(dependentNames!=""){
				MessageBox.Show(Lan.g(this,"Not allowed to delete this employer because it is attached to "
					+"the following insurance plans.  You should combine employers instead.")
					+"\r\n\r\n"+dependentNames);
					return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Employer?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Employers.Delete(Employers.List[listEmp.SelectedIndices[0]]);
			FillGrid();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(listEmp.SelectedIndices.Count!=1){
				MessageBox.Show(Lan.g(this,"Please select one item first."));
				return;
			}
			FormEmployerEdit FormEE=new FormEmployerEdit();
			FormEE.EmployerCur=Employers.List[listEmp.SelectedIndices[0]];
			FormEE.ShowDialog();
			if(FormEE.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		private void butCombine_Click(object sender, System.EventArgs e) {
			if(listEmp.SelectedIndices.Count<2){
				MessageBox.Show(Lan.g(this,"Please select multiple items first while holding down the control key."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Combine all these employers into a single employer? This will affect all patients using these employers."),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			int[] employerNums=new int[listEmp.SelectedIndices.Count];
			for(int i=0;i<listEmp.SelectedIndices.Count;i++){
				employerNums[i]=Employers.List[listEmp.SelectedIndices[i]].EmployerNum;
			}
			Employers.Combine(employerNums);
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			/*if(IsSelectMode){
				if(listEmp.SelectedIndices.Count!=1){
					Employers.Cur=new Employer();
					//MessageBox.Show(Lan.g(this,"Please select one item first."));
					//return;
				}
				else
					Employers.Cur=Employers.List[listEmp.SelectedIndices[0]];
			}
			else{
				//update the other computers:
				//DataValid.SetInvalid();//not needed due to intelligent refreshing
			}*/
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}



























