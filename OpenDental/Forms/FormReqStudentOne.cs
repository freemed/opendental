using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReqStudentOne : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public int ProvNum;
		private OpenDental.UI.ODGrid gridMain;
		private Provider prov;
		private DataTable table;
		//public bool IsSelectionMode;
		//<summary>If IsSelectionMode and DialogResult.OK, then this will contain the selected req.</summary>
		//public int SelectedReqStudentNum;

		///<summary></summary>
		public FormReqStudentOne()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReqStudentOne));
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
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
			this.butCancel.Location = new System.Drawing.Point(738,621);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(19,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(698,635);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Student Requirements";
			this.gridMain.TranslationName = "TableReqStudentOne";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormReqStudentOne
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(825,665);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReqStudentOne";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Student Requirements - One";
			this.Load += new System.EventHandler(this.FormReqStudentOne_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReqStudentOne_Load(object sender,EventArgs e) {
			//if(IsSelectionMode){
				
			//}
			//else{
				//labelSelection.Visible=false;
				//butOK.Visible=false;
				//butCancel.Text=Lan.g(this,"Close");
			//}
			prov=Providers.GetProv(ProvNum);
			Text=Lan.g(this,"Student Requirements - ")+Providers.GetNameLF(ProvNum);
			FillGrid();
		}

		private void FillGrid(){
			table=ReqStudents.RefreshOneStudent(ProvNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableReqStudentOne","Course"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableReqStudentOne","Requirement"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableReqStudentOne","Done"),40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableReqStudentOne","Patient"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableReqStudentOne","Appointment"),190);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["course"].ToString());
				row.Cells.Add(table.Rows[i]["requirement"].ToString());
				row.Cells.Add(table.Rows[i]["done"].ToString());
				row.Cells.Add(table.Rows[i]["patient"].ToString());
				row.Cells.Add(table.Rows[i]["appointment"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//if(IsSelectionMode){
			//	if(table.Rows[e.Row]["appointment"].ToString()!=""){
			//		MsgBox.Show(this,"Already attached to an appointment.");
			//		return;
			//	}
			//	SelectedReqStudentNum=PIn.PInt(table.Rows[e.Row]["ReqStudentNum"].ToString());
			//	DialogResult=DialogResult.OK;
			//}
			//else{
				FormReqStudentEdit FormRSE=new FormReqStudentEdit();
				FormRSE.ReqCur=ReqStudents.GetOne(PIn.PInt(table.Rows[e.Row]["ReqStudentNum"].ToString()));
				FormRSE.ShowDialog();
				if(FormRSE.DialogResult!=DialogResult.OK) {
					return;
				}
				FillGrid();
			//}
		}

		//private void butOK_Click(object sender, System.EventArgs e) {
			//not accessible
			/*if(IsSelectionMode){
				if(gridMain.GetSelectedIndex()==-1){
					MsgBox.Show(this,"Please select a requirement first.");
					return;
				}
				if(table.Rows[gridMain.GetSelectedIndex()]["appointment"].ToString()!="") {
					MsgBox.Show(this,"Selected requirement is already attached to an appointment.");
					return;
				}
				SelectedReqStudentNum=PIn.PInt(table.Rows[gridMain.GetSelectedIndex()]["ReqStudentNum"].ToString());
				DialogResult=DialogResult.OK;
			}*/
			//should never get to here.
		//}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















