using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReqAppt : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.ODGrid gridStudents;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Label label2;
		private ComboBox comboCourse;
		private Label label1;
		private ComboBox comboClass;
		private ODGrid gridAttached;
		private OpenDental.UI.Button butRemove;
		private OpenDental.UI.Button butAdd;
		private ODGrid gridReqs;
		private OpenDental.UI.Button butOK;
		//private DataTable table;
		private List<Provider> StudentList;
		private DataTable ReqTable;
		private List<ReqStudent> reqsAttached;
		public int AptNum;
		private Label label3;
		private ComboBox comboInstructor;
		private bool hasChanged;
		public int PatNum;

		///<summary></summary>
		public FormReqAppt()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReqAppt));
			this.label2 = new System.Windows.Forms.Label();
			this.comboCourse = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboClass = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboInstructor = new System.Windows.Forms.ComboBox();
			this.butOK = new OpenDental.UI.Button();
			this.gridReqs = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.butRemove = new OpenDental.UI.Button();
			this.gridAttached = new OpenDental.UI.ODGrid();
			this.gridStudents = new OpenDental.UI.ODGrid();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(566,39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77,18);
			this.label2.TabIndex = 22;
			this.label2.Text = "Course";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboCourse
			// 
			this.comboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCourse.FormattingEnabled = true;
			this.comboCourse.Location = new System.Drawing.Point(647,39);
			this.comboCourse.Name = "comboCourse";
			this.comboCourse.Size = new System.Drawing.Size(234,21);
			this.comboCourse.TabIndex = 21;
			this.comboCourse.SelectionChangeCommitted += new System.EventHandler(this.comboCourse_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(563,12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81,18);
			this.label1.TabIndex = 20;
			this.label1.Text = "Class";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClass
			// 
			this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClass.FormattingEnabled = true;
			this.comboClass.Location = new System.Drawing.Point(647,12);
			this.comboClass.Name = "comboClass";
			this.comboClass.Size = new System.Drawing.Size(234,21);
			this.comboClass.TabIndex = 19;
			this.comboClass.SelectionChangeCommitted += new System.EventHandler(this.comboClass_SelectionChangeCommitted);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(497,66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(146,18);
			this.label3.TabIndex = 29;
			this.label3.Text = "Instructor";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboInstructor
			// 
			this.comboInstructor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboInstructor.FormattingEnabled = true;
			this.comboInstructor.Location = new System.Drawing.Point(647,66);
			this.comboInstructor.Name = "comboInstructor";
			this.comboInstructor.Size = new System.Drawing.Size(234,21);
			this.comboInstructor.TabIndex = 28;
			this.comboInstructor.SelectionChangeCommitted += new System.EventHandler(this.comboInstructor_SelectionChangeCommitted);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(806,591);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 27;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// gridReqs
			// 
			this.gridReqs.HScrollVisible = false;
			this.gridReqs.Location = new System.Drawing.Point(223,12);
			this.gridReqs.Name = "gridReqs";
			this.gridReqs.ScrollValue = 0;
			this.gridReqs.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridReqs.Size = new System.Drawing.Size(268,637);
			this.gridReqs.TabIndex = 26;
			this.gridReqs.Title = "Requirements";
			this.gridReqs.TranslationName = "TableReqStudentMany";
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.down;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(497,273);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(90,26);
			this.butAdd.TabIndex = 25;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butRemove
			// 
			this.butRemove.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRemove.Autosize = true;
			this.butRemove.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemove.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemove.CornerRadius = 4F;
			this.butRemove.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butRemove.Location = new System.Drawing.Point(596,273);
			this.butRemove.Name = "butRemove";
			this.butRemove.Size = new System.Drawing.Size(90,26);
			this.butRemove.TabIndex = 24;
			this.butRemove.Text = "Remove";
			this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
			// 
			// gridAttached
			// 
			this.gridAttached.HScrollVisible = false;
			this.gridAttached.Location = new System.Drawing.Point(497,305);
			this.gridAttached.Name = "gridAttached";
			this.gridAttached.ScrollValue = 0;
			this.gridAttached.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAttached.Size = new System.Drawing.Size(384,225);
			this.gridAttached.TabIndex = 23;
			this.gridAttached.Title = "Currently Attached Requirements";
			this.gridAttached.TranslationName = "TableReqStudentMany";
			this.gridAttached.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAttached_CellDoubleClick);
			// 
			// gridStudents
			// 
			this.gridStudents.HScrollVisible = false;
			this.gridStudents.Location = new System.Drawing.Point(10,12);
			this.gridStudents.Name = "gridStudents";
			this.gridStudents.ScrollValue = 0;
			this.gridStudents.Size = new System.Drawing.Size(207,637);
			this.gridStudents.TabIndex = 3;
			this.gridStudents.Title = "Students";
			this.gridStudents.TranslationName = "TableReqStudentMany";
			this.gridStudents.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridStudents_CellClick);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(806,623);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormReqAppt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(893,661);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboInstructor);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.gridReqs);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butRemove);
			this.Controls.Add(this.gridAttached);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboCourse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboClass);
			this.Controls.Add(this.gridStudents);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReqAppt";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Student Requirements for Appointment";
			this.Load += new System.EventHandler(this.FormReqAppt_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReqAppt_Load(object sender,EventArgs e) {
			for(int i=0;i<SchoolClasses.List.Length;i++) {
				comboClass.Items.Add(SchoolClasses.GetDescript(SchoolClasses.List[i]));
			}
			if(comboClass.Items.Count>0) {
				comboClass.SelectedIndex=0;
			}
			for(int i=0;i<SchoolCourses.List.Length;i++) {
				comboCourse.Items.Add(SchoolCourses.GetDescript(SchoolCourses.List[i]));
			}
			if(comboCourse.Items.Count>0) {
				comboCourse.SelectedIndex=0;
			}
			comboInstructor.Items.Add(Lan.g(this,"None"));
			comboInstructor.SelectedIndex=0;
			for(int i=0;i<ProviderC.List.Length;i++) {
				comboInstructor.Items.Add(ProviderC.List[i].GetLongDesc());
				//if(ProviderC.List[i].ProvNum==ReqCur.InstructorNum) {
				//	comboInstructor.SelectedIndex=i+1;
				//}
			}
			FillStudents();
			FillReqs();
			reqsAttached=ReqStudents.GetForAppt(AptNum);
			if(reqsAttached.Count>0){
				comboInstructor.SelectedIndex=Providers.GetIndex(reqsAttached[0].ProvNum)+1;//this will turn a -1 into a 0.
			}
			FillAttached();
		}

		private void FillStudents() {
			if(comboClass.SelectedIndex==-1) {
				return;
			}
			int schoolClass=SchoolClasses.List[comboClass.SelectedIndex].SchoolClassNum;
			//int schoolCourse=SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum;
			StudentList=ReqStudents.GetStudents(schoolClass);
			gridStudents.BeginUpdate();
			gridStudents.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);
			gridStudents.Columns.Add(col);
			gridStudents.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<StudentList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(StudentList[i].LName+", "+StudentList[i].FName);
				gridStudents.Rows.Add(row);
			}
			gridStudents.EndUpdate();
		}

		private void FillReqs(){
			int schoolCourse=0;
			if(comboCourse.SelectedIndex!=-1){
				schoolCourse=SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum;
			}
			int schoolClass=0;
			if(comboClass.SelectedIndex!=-1) {
				schoolClass=SchoolClasses.List[comboClass.SelectedIndex].SchoolClassNum;
			}
			gridReqs.BeginUpdate();
			gridReqs.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);
			gridReqs.Columns.Add(col);
			gridReqs.Rows.Clear();
			if(gridStudents.GetSelectedIndex()==-1) {
				gridReqs.EndUpdate();
				return;
			}
			ReqTable=ReqStudents.GetForCourseClass(schoolCourse,schoolClass);
			ODGridRow row;
			for(int i=0;i<ReqTable.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ReqTable.Rows[i]["Descript"].ToString());
				gridReqs.Rows.Add(row);
			}
			gridReqs.EndUpdate();
		}

		///<summary>All alterations to TableAttached should have been made</summary>
		private void FillAttached(){
			gridAttached.BeginUpdate();
			gridAttached.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Student",130);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn("Descript",150);
			gridAttached.Columns.Add(col);
			col=new ODGridColumn("Completed",40);
			gridAttached.Columns.Add(col);
			gridAttached.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<reqsAttached.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Providers.GetAbbr(reqsAttached[i].ProvNum));
				row.Cells.Add(reqsAttached[i].Descript);
				if(reqsAttached[i].DateCompleted.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add("X");
				}
				gridAttached.Rows.Add(row);
			}
			gridAttached.EndUpdate();
		}

		private void gridAttached_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(hasChanged){
				MsgBox.Show(this,"Not allowed to edit individual requirements immediately after adding or removing.");
				return;
			}
			FormReqStudentEdit FormRSE=new FormReqStudentEdit();
			FormRSE.ReqCur=reqsAttached[e.Row];
			FormRSE.ShowDialog();
			if(FormRSE.DialogResult!=DialogResult.OK) {
				return;
			}
			reqsAttached=ReqStudents.GetForAppt(AptNum);
			FillAttached();
		}

		private void gridStudents_CellClick(object sender,ODGridClickEventArgs e) {
			FillReqs();
		}

		private void comboClass_SelectionChangeCommitted(object sender,EventArgs e) {
			FillStudents();
			FillReqs();
		}

		private void comboCourse_SelectionChangeCommitted(object sender,EventArgs e) {
			FillReqs();
		}


		private void comboInstructor_SelectionChangeCommitted(object sender,EventArgs e) {
			for(int i=0;i<reqsAttached.Count;i++){
				if(reqsAttached[i].DateCompleted.Year>1880){
					continue;//don't alter instructor of completed reqs.
				}
				if(comboInstructor.SelectedIndex==0){
					reqsAttached[i].InstructorNum=0;
				}
				else{
					reqsAttached[i].InstructorNum=ProviderC.List[comboInstructor.SelectedIndex-1].ProvNum;
				}
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(gridReqs.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select at least one requirement from the list at the left first.");
				return;
			}
			ReqStudent req;
			for(int i=0;i<gridReqs.SelectedIndices.Length;i++){
				req=new ReqStudent();
				req.AptNum=AptNum;
				//req.DateCompleted
				req.Descript=ReqTable.Rows[gridReqs.SelectedIndices[i]]["Descript"].ToString();
				if(comboInstructor.SelectedIndex>0){
					req.InstructorNum=ProviderC.List[comboInstructor.SelectedIndex-1].ProvNum;
				}
				req.PatNum=PatNum;
				req.ProvNum=StudentList[gridStudents.GetSelectedIndex()].ProvNum;
				req.ReqNeededNum=PIn.PInt(ReqTable.Rows[gridReqs.SelectedIndices[i]]["ReqNeededNum"].ToString());
				//req.ReqStudentNum=0 until synch on OK.
				req.SchoolCourseNum=SchoolCourses.List[comboCourse.SelectedIndex].SchoolCourseNum;
				reqsAttached.Add(req);
				hasChanged=true;
			}
			FillAttached();
		}

		private void butRemove_Click(object sender,EventArgs e) {
			if(gridAttached.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select at least one requirement from the list below first.");
				return;
			}
			for(int i=gridAttached.SelectedIndices.Length-1;i>=0;i--){//go backwards to remove from end of list
				reqsAttached.RemoveAt(gridAttached.SelectedIndices[i]);
				hasChanged=true;
			}
			FillAttached();
		}

		private void butOK_Click(object sender,EventArgs e) {
			ReqStudents.SynchApt(reqsAttached,AptNum);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}





















