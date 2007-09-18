using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormEmployeeSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		//private ArrayList ALemployees;
		private OpenDental.UI.ODGrid gridMain;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butDelete;
		private Label label1;
		private bool isChanged;

		///<summary></summary>
		public FormEmployeeSelect(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmployeeSelect));
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.butClose.Location = new System.Drawing.Point(358,446);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 16;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
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
			this.butAdd.Location = new System.Drawing.Point(12,446);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(78,26);
			this.butAdd.TabIndex = 21;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(268,418);
			this.gridMain.TabIndex = 22;
			this.gridMain.Title = "";
			this.gridMain.TranslationName = "FormEmployees";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butDelete);
			this.groupBox1.Location = new System.Drawing.Point(327,170);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(117,87);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Advanced Tools";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108,29);
			this.label1.TabIndex = 24;
			this.label1.Text = "Delete all unused employees";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(9,47);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(97,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "Delete All";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormEmployeeSelect
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(456,486);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmployeeSelect";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employees";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormEmployee_Closing);
			this.Load += new System.EventHandler(this.FormEmployeeSelect_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormEmployeeSelect_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Employees.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormEmployeeSelect","FName"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormEmployeeSelect","LName"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormEmployeeSelect","MiddleI"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormEmployeeSelect","Hidden"),50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Employees.ListLong.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(Employees.ListLong[i].FName);
				row.Cells.Add(Employees.ListLong[i].LName);
				row.Cells.Add(Employees.ListLong[i].MiddleI);
				if(Employees.ListLong[i].IsHidden){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormEmployeeEdit FormEE=new FormEmployeeEdit();
			FormEE.EmployeeCur=new Employee();
			FormEE.IsNew=true;
			FormEE.ShowDialog();
			FillGrid();
			isChanged=true;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			int empNum=Employees.ListLong[e.Row].EmployeeNum;
			FormEmployeeEdit FormEE=new FormEmployeeEdit();
			FormEE.EmployeeCur=Employees.ListLong[e.Row];
			FormEE.ShowDialog();
			FillGrid();
			isChanged=true;
			for(int i=0;i<Employees.ListLong.Length;i++){
				if(Employees.ListLong[i].EmployeeNum==empNum){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Schedules may be lost.  Continue?")){
				return;
			}
			for(int i=0;i<Employees.ListLong.Length;i++){
				try{
					Employees.Delete(Employees.ListLong[i].EmployeeNum);
				}
				catch{}
			}
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void FormEmployee_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(isChanged){
				DataValid.SetInvalid(InvalidTypes.Employees);
			}
		}

		

		
	}
}
