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
	public class FormTaskEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox listDateType;
		private OpenDental.ODtextBox textDescript;
		private Task Cur;
		private OpenDental.ValidDate textDateTask;
		private OpenDental.UI.Button butChange;
		private System.Windows.Forms.CheckBox checkTaskStatus;
		private OpenDental.UI.Button butGoto;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.CheckBox checkFromNum;
		private System.Windows.Forms.Label labelObjectDesc;
		private System.Windows.Forms.TextBox textObjectDesc;
		private System.Windows.Forms.ListBox listObjectType;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panelObject;
		///<summary>After closing, if this is not zero, then it will jump to the object specified in GotoKeyNum.</summary>
		public TaskObjectType GotoType;
		private Label label5;
		private TextBox textDateTimeEntry;
		private OpenDental.UI.Button butNow;
		///<summary>After closing, if this is not zero, then it will jump to the specified patient.</summary>
		public int GotoKeyNum;

		///<summary></summary>
		public FormTaskEdit(Task cur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Cur=cur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textDescript = new OpenDental.ODtextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listDateType = new System.Windows.Forms.ListBox();
			this.checkFromNum = new System.Windows.Forms.CheckBox();
			this.textDateTask = new OpenDental.ValidDate();
			this.labelObjectDesc = new System.Windows.Forms.Label();
			this.textObjectDesc = new System.Windows.Forms.TextBox();
			this.butChange = new OpenDental.UI.Button();
			this.checkTaskStatus = new System.Windows.Forms.CheckBox();
			this.butGoto = new OpenDental.UI.Button();
			this.listObjectType = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panelObject = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.textDateTimeEntry = new System.Windows.Forms.TextBox();
			this.butNow = new OpenDental.UI.Button();
			this.panelObject.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(623,473);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 5;
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
			this.butOK.Location = new System.Drawing.Point(623,440);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 4;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116,19);
			this.label1.TabIndex = 2;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(127,62);
			this.textDescript.Multiline = true;
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(452,141);
			this.textDescript.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,212);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(218,209);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(185,32);
			this.label3.TabIndex = 6;
			this.label3.Text = "Leave blank unless you want this task to show on a dated list";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8,240);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116,19);
			this.label4.TabIndex = 7;
			this.label4.Text = "Date Type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listDateType
			// 
			this.listDateType.Location = new System.Drawing.Point(127,241);
			this.listDateType.Name = "listDateType";
			this.listDateType.Size = new System.Drawing.Size(120,56);
			this.listDateType.TabIndex = 2;
			// 
			// checkFromNum
			// 
			this.checkFromNum.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkFromNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkFromNum.Location = new System.Drawing.Point(8,303);
			this.checkFromNum.Name = "checkFromNum";
			this.checkFromNum.Size = new System.Drawing.Size(133,24);
			this.checkFromNum.TabIndex = 3;
			this.checkFromNum.Text = "Is From Repeating";
			this.checkFromNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateTask
			// 
			this.textDateTask.Location = new System.Drawing.Point(127,212);
			this.textDateTask.Name = "textDateTask";
			this.textDateTask.Size = new System.Drawing.Size(87,20);
			this.textDateTask.TabIndex = 1;
			// 
			// labelObjectDesc
			// 
			this.labelObjectDesc.Location = new System.Drawing.Point(3,2);
			this.labelObjectDesc.Name = "labelObjectDesc";
			this.labelObjectDesc.Size = new System.Drawing.Size(116,19);
			this.labelObjectDesc.TabIndex = 8;
			this.labelObjectDesc.Text = "ObjectDesc";
			this.labelObjectDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textObjectDesc
			// 
			this.textObjectDesc.Location = new System.Drawing.Point(124,4);
			this.textObjectDesc.Multiline = true;
			this.textObjectDesc.Name = "textObjectDesc";
			this.textObjectDesc.Size = new System.Drawing.Size(452,63);
			this.textObjectDesc.TabIndex = 9;
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(123,71);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75,26);
			this.butChange.TabIndex = 10;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// checkTaskStatus
			// 
			this.checkTaskStatus.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkTaskStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTaskStatus.Location = new System.Drawing.Point(5,10);
			this.checkTaskStatus.Name = "checkTaskStatus";
			this.checkTaskStatus.Size = new System.Drawing.Size(136,21);
			this.checkTaskStatus.TabIndex = 11;
			this.checkTaskStatus.Text = "Is Complete";
			this.checkTaskStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butGoto
			// 
			this.butGoto.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGoto.Autosize = true;
			this.butGoto.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGoto.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGoto.CornerRadius = 4F;
			this.butGoto.Location = new System.Drawing.Point(200,71);
			this.butGoto.Name = "butGoto";
			this.butGoto.Size = new System.Drawing.Size(75,26);
			this.butGoto.TabIndex = 12;
			this.butGoto.Text = "Go To";
			this.butGoto.Click += new System.EventHandler(this.butGoto_Click);
			// 
			// listObjectType
			// 
			this.listObjectType.Location = new System.Drawing.Point(127,324);
			this.listObjectType.Name = "listObjectType";
			this.listObjectType.Size = new System.Drawing.Size(120,43);
			this.listObjectType.TabIndex = 13;
			this.listObjectType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listObjectType_MouseDown);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8,323);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(116,19);
			this.label6.TabIndex = 14;
			this.label6.Text = "Object Type";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelObject
			// 
			this.panelObject.Controls.Add(this.textObjectDesc);
			this.panelObject.Controls.Add(this.labelObjectDesc);
			this.panelObject.Controls.Add(this.butGoto);
			this.panelObject.Controls.Add(this.butChange);
			this.panelObject.Location = new System.Drawing.Point(3,373);
			this.panelObject.Name = "panelObject";
			this.panelObject.Size = new System.Drawing.Size(594,101);
			this.panelObject.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8,34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116,19);
			this.label5.TabIndex = 17;
			this.label5.Text = "Entry Date/Time";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTimeEntry
			// 
			this.textDateTimeEntry.Location = new System.Drawing.Point(127,33);
			this.textDateTimeEntry.Name = "textDateTimeEntry";
			this.textDateTimeEntry.Size = new System.Drawing.Size(151,20);
			this.textDateTimeEntry.TabIndex = 18;
			this.textDateTimeEntry.Validating += new System.ComponentModel.CancelEventHandler(this.textDateTimeEntry_Validating);
			// 
			// butNow
			// 
			this.butNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNow.Autosize = true;
			this.butNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow.CornerRadius = 4F;
			this.butNow.Location = new System.Drawing.Point(287,30);
			this.butNow.Name = "butNow";
			this.butNow.Size = new System.Drawing.Size(75,26);
			this.butNow.TabIndex = 19;
			this.butNow.Text = "Now";
			this.butNow.Click += new System.EventHandler(this.butNow_Click);
			// 
			// FormTaskEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(726,525);
			this.Controls.Add(this.butNow);
			this.Controls.Add(this.textDateTimeEntry);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.panelObject);
			this.Controls.Add(this.listObjectType);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textDateTask);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.checkTaskStatus);
			this.Controls.Add(this.checkFromNum);
			this.Controls.Add(this.listDateType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTaskEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Task";
			this.Load += new System.EventHandler(this.FormTaskListEdit_Load);
			this.panelObject.ResumeLayout(false);
			this.panelObject.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTaskListEdit_Load(object sender, System.EventArgs e) {
			checkTaskStatus.Checked=Cur.TaskStatus;
			if(Cur.DateTimeEntry.Year<1880){
				textDateTimeEntry.Text=DateTime.Now.ToString();
			}
			else{
				textDateTimeEntry.Text=Cur.DateTimeEntry.ToString();
			}
			textDescript.Text=Cur.Descript;
			if(Cur.DateTask.Year>1880){
				textDateTask.Text=Cur.DateTask.ToShortDateString();
			}
			if(Cur.IsRepeating){
				checkTaskStatus.Enabled=false;
				textDateTask.Enabled=false;
				listObjectType.Enabled=false;
				if(Cur.TaskListNum!=0){//not a main parent
					listDateType.Enabled=false;
				}
			}
			for(int i=0;i<Enum.GetNames(typeof(TaskDateType)).Length;i++){
				listDateType.Items.Add(Lan.g("enumTaskDateType",Enum.GetNames(typeof(TaskDateType))[i]));
				if((int)Cur.DateType==i){
					listDateType.SelectedIndex=i;
				}
			}
			if(Cur.FromNum==0){
				checkFromNum.Checked=false;
				checkFromNum.Enabled=false;
			}
			else{
				checkFromNum.Checked=true;
			}
			for(int i=0;i<Enum.GetNames(typeof(TaskObjectType)).Length;i++){
				listObjectType.Items.Add(Lan.g("enumTaskObjectType",Enum.GetNames(typeof(TaskObjectType))[i]));
			}
			FillObject();
		}

		private void FillObject(){
			if(Cur.ObjectType==TaskObjectType.None){
				listObjectType.SelectedIndex=0;
				panelObject.Visible=false;
			}
			else if(Cur.ObjectType==TaskObjectType.Patient){
				listObjectType.SelectedIndex=1;
				panelObject.Visible=true;
				labelObjectDesc.Text=Lan.g(this,"Patient Name");
				if(Cur.KeyNum>0){
					textObjectDesc.Text=Patients.GetPat(Cur.KeyNum).GetNameLF();
				}
				else{
					textObjectDesc.Text="";
				}
			}
			else if(Cur.ObjectType==TaskObjectType.Appointment){
				listObjectType.SelectedIndex=2;
				panelObject.Visible=true;
				labelObjectDesc.Text=Lan.g(this,"Appointment Desc");
				if(Cur.KeyNum>0){
					Appointment AptCur=Appointments.GetOneApt(Cur.KeyNum);
					if(AptCur==null){
						textObjectDesc.Text=Lan.g(this,"(appointment deleted)");
					}
					else{
						textObjectDesc.Text=Patients.GetPat(AptCur.PatNum).GetNameLF()
							+"  "+AptCur.AptDateTime.ToString()
							+"  "+AptCur.ProcDescript
							+"  "+AptCur.Note;
					}
				}
				else{
					textObjectDesc.Text="";
				}
			}
		}

		private void textDateTimeEntry_Validating(object sender,CancelEventArgs e) {
			if(textDateTimeEntry.Text=="") {
				return;
			}
			try{
				DateTime.Parse(textDateTimeEntry.Text);
			}
			catch{
				MsgBox.Show(this,"Invalid date/time format.");
				e.Cancel=true;
			}
		}

		private void butNow_Click(object sender,EventArgs e) {
			textDateTimeEntry.Text=MiscData.GetNowDateTime().ToString();
		}

		private void listObjectType_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(Cur.KeyNum>0){
				if(!MsgBox.Show(this,true,"The linked object will no longer be attached.  Continue?")){
					FillObject();
					return;
				}
			}
			Cur.KeyNum=0;
			Cur.ObjectType=(TaskObjectType)listObjectType.SelectedIndex;
			FillObject();
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			if(Cur.ObjectType==TaskObjectType.Patient){
				Cur.KeyNum=FormPS.SelectedPatNum;
			}
			if(Cur.ObjectType==TaskObjectType.Appointment){
				FormApptsOther FormA=new FormApptsOther(FormPS.SelectedPatNum);
				FormA.SelectOnly=true;
				FormA.ShowDialog();
				if(FormA.DialogResult==DialogResult.Cancel){
					return;
				}
				Cur.KeyNum=FormA.AptSelected;
			}
			FillObject();
		}

		private void butGoto_Click(object sender, System.EventArgs e) {
			if(!SaveCur()){
				return;
			}
			GotoType=Cur.ObjectType;
			GotoKeyNum=Cur.KeyNum;
			DialogResult=DialogResult.OK;
		}

		private bool SaveCur(){
			if(  textDateTask.errorProvider1.GetError(textDateTask)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			Cur.TaskStatus=checkTaskStatus.Checked;
			Cur.DateTimeEntry=PIn.PDateT(textDateTimeEntry.Text);
			Cur.Descript=textDescript.Text;
			Cur.DateTask=PIn.PDate(textDateTask.Text);
			Cur.DateType=(TaskDateType)listDateType.SelectedIndex;
			if(!checkFromNum.Checked){//user unchecked the box. Never allowed to check if initially unchecked
				Cur.FromNum=0;
			}
			//ObjectType already handled
			//Cur.KeyNum already handled
			try{
				Tasks.InsertOrUpdate(Cur,IsNew);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveCur()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		


	}
}





















