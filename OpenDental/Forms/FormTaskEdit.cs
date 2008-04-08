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
		private Task CurOld;
		private OpenDental.ValidDate textDateTask;
		private OpenDental.UI.Button butChange;
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
		private OpenDental.UI.Button butDelete;
		private TextBox textUser;
		private Label label16;
		private RadioButton radioNew;
		private RadioButton radioViewed;
		private RadioButton radioDone;
		private OpenDental.UI.Button butNowFinished;
		private TextBox textDateTimeFinished;
		private Label label7;
		private TextBox textTaskNum;
		private Label labelTaskNum;
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
			CurOld=cur.Copy();
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listDateType = new System.Windows.Forms.ListBox();
			this.checkFromNum = new System.Windows.Forms.CheckBox();
			this.labelObjectDesc = new System.Windows.Forms.Label();
			this.textObjectDesc = new System.Windows.Forms.TextBox();
			this.listObjectType = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panelObject = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.textDateTimeEntry = new System.Windows.Forms.TextBox();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.radioNew = new System.Windows.Forms.RadioButton();
			this.radioViewed = new System.Windows.Forms.RadioButton();
			this.radioDone = new System.Windows.Forms.RadioButton();
			this.textDateTimeFinished = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butNowFinished = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butNow = new OpenDental.UI.Button();
			this.butGoto = new OpenDental.UI.Button();
			this.butChange = new OpenDental.UI.Button();
			this.textDateTask = new OpenDental.ValidDate();
			this.textDescript = new OpenDental.ODtextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textTaskNum = new System.Windows.Forms.TextBox();
			this.labelTaskNum = new System.Windows.Forms.Label();
			this.panelObject.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116,19);
			this.label1.TabIndex = 2;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9,382);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(218,379);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(185,32);
			this.label3.TabIndex = 6;
			this.label3.Text = "Leave blank unless you want this task to show on a dated list";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,408);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116,19);
			this.label4.TabIndex = 7;
			this.label4.Text = "Date Type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listDateType
			// 
			this.listDateType.Location = new System.Drawing.Point(127,409);
			this.listDateType.Name = "listDateType";
			this.listDateType.Size = new System.Drawing.Size(120,56);
			this.listDateType.TabIndex = 2;
			// 
			// checkFromNum
			// 
			this.checkFromNum.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkFromNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkFromNum.Location = new System.Drawing.Point(8,470);
			this.checkFromNum.Name = "checkFromNum";
			this.checkFromNum.Size = new System.Drawing.Size(133,24);
			this.checkFromNum.TabIndex = 3;
			this.checkFromNum.Text = "Is From Repeating";
			this.checkFromNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			// listObjectType
			// 
			this.listObjectType.Location = new System.Drawing.Point(388,409);
			this.listObjectType.Name = "listObjectType";
			this.listObjectType.Size = new System.Drawing.Size(120,43);
			this.listObjectType.TabIndex = 13;
			this.listObjectType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listObjectType_MouseDown);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(269,408);
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
			this.panelObject.Location = new System.Drawing.Point(3,502);
			this.panelObject.Name = "panelObject";
			this.panelObject.Size = new System.Drawing.Size(594,101);
			this.panelObject.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,61);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116,19);
			this.label5.TabIndex = 17;
			this.label5.Text = "Date/Time Entry";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTimeEntry
			// 
			this.textDateTimeEntry.Location = new System.Drawing.Point(127,60);
			this.textDateTimeEntry.Name = "textDateTimeEntry";
			this.textDateTimeEntry.Size = new System.Drawing.Size(151,20);
			this.textDateTimeEntry.TabIndex = 18;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(460,7);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(119,20);
			this.textUser.TabIndex = 126;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(385,8);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73,16);
			this.label16.TabIndex = 125;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// radioNew
			// 
			this.radioNew.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioNew.Location = new System.Drawing.Point(37,4);
			this.radioNew.Name = "radioNew";
			this.radioNew.Size = new System.Drawing.Size(104,16);
			this.radioNew.TabIndex = 127;
			this.radioNew.TabStop = true;
			this.radioNew.Text = "New";
			this.radioNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioNew.UseVisualStyleBackColor = true;
			// 
			// radioViewed
			// 
			this.radioViewed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioViewed.Location = new System.Drawing.Point(37,20);
			this.radioViewed.Name = "radioViewed";
			this.radioViewed.Size = new System.Drawing.Size(104,16);
			this.radioViewed.TabIndex = 128;
			this.radioViewed.TabStop = true;
			this.radioViewed.Text = "Viewed";
			this.radioViewed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioViewed.UseVisualStyleBackColor = true;
			// 
			// radioDone
			// 
			this.radioDone.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioDone.Location = new System.Drawing.Point(37,37);
			this.radioDone.Name = "radioDone";
			this.radioDone.Size = new System.Drawing.Size(104,16);
			this.radioDone.TabIndex = 129;
			this.radioDone.TabStop = true;
			this.radioDone.Text = "Done";
			this.radioDone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioDone.UseVisualStyleBackColor = true;
			// 
			// textDateTimeFinished
			// 
			this.textDateTimeFinished.Location = new System.Drawing.Point(127,88);
			this.textDateTimeFinished.Name = "textDateTimeFinished";
			this.textDateTimeFinished.Size = new System.Drawing.Size(151,20);
			this.textDateTimeFinished.TabIndex = 131;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(9,89);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116,19);
			this.label7.TabIndex = 130;
			this.label7.Text = "Date/Time Finished";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butNowFinished
			// 
			this.butNowFinished.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNowFinished.Autosize = true;
			this.butNowFinished.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNowFinished.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNowFinished.CornerRadius = 4F;
			this.butNowFinished.Location = new System.Drawing.Point(284,86);
			this.butNowFinished.Name = "butNowFinished";
			this.butNowFinished.Size = new System.Drawing.Size(75,24);
			this.butNowFinished.TabIndex = 132;
			this.butNowFinished.Text = "Now";
			this.butNowFinished.Click += new System.EventHandler(this.butNowFinished_Click);
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
			this.butDelete.Location = new System.Drawing.Point(21,610);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(80,24);
			this.butDelete.TabIndex = 124;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butNow
			// 
			this.butNow.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNow.Autosize = true;
			this.butNow.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow.CornerRadius = 4F;
			this.butNow.Location = new System.Drawing.Point(284,58);
			this.butNow.Name = "butNow";
			this.butNow.Size = new System.Drawing.Size(75,24);
			this.butNow.TabIndex = 19;
			this.butNow.Text = "Now";
			this.butNow.Click += new System.EventHandler(this.butNow_Click);
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
			this.butGoto.Size = new System.Drawing.Size(75,24);
			this.butGoto.TabIndex = 12;
			this.butGoto.Text = "Go To";
			this.butGoto.Click += new System.EventHandler(this.butGoto_Click);
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
			this.butChange.Size = new System.Drawing.Size(75,24);
			this.butChange.TabIndex = 10;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// textDateTask
			// 
			this.textDateTask.Location = new System.Drawing.Point(127,382);
			this.textDateTask.Name = "textDateTask";
			this.textDateTask.Size = new System.Drawing.Size(87,20);
			this.textDateTask.TabIndex = 1;
			// 
			// textDescript
			// 
			this.textDescript.AcceptsReturn = true;
			this.textDescript.Location = new System.Drawing.Point(127,117);
			this.textDescript.Multiline = true;
			this.textDescript.Name = "textDescript";
			this.textDescript.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textDescript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textDescript.Size = new System.Drawing.Size(452,258);
			this.textDescript.TabIndex = 0;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(543,610);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 4;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(625,610);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textTaskNum
			// 
			this.textTaskNum.Location = new System.Drawing.Point(525,60);
			this.textTaskNum.Name = "textTaskNum";
			this.textTaskNum.ReadOnly = true;
			this.textTaskNum.Size = new System.Drawing.Size(54,20);
			this.textTaskNum.TabIndex = 134;
			this.textTaskNum.Visible = false;
			// 
			// labelTaskNum
			// 
			this.labelTaskNum.Location = new System.Drawing.Point(450,61);
			this.labelTaskNum.Name = "labelTaskNum";
			this.labelTaskNum.Size = new System.Drawing.Size(73,16);
			this.labelTaskNum.TabIndex = 133;
			this.labelTaskNum.Text = "TaskNum";
			this.labelTaskNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelTaskNum.Visible = false;
			// 
			// FormTaskEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(726,658);
			this.Controls.Add(this.textTaskNum);
			this.Controls.Add(this.labelTaskNum);
			this.Controls.Add(this.butNowFinished);
			this.Controls.Add(this.textDateTimeFinished);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.radioDone);
			this.Controls.Add(this.radioViewed);
			this.Controls.Add(this.radioNew);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.butDelete);
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
			#if DEBUG
				labelTaskNum.Visible=true;
				textTaskNum.Visible=true;
				textTaskNum.Text=Cur.TaskNum.ToString();
			#endif
			switch(Cur.TaskStatus){
				case TaskStatusEnum.New:
					radioNew.Checked=true;
					break;
				case TaskStatusEnum.Viewed:
					radioViewed.Checked=true;
					break;
				case TaskStatusEnum.Done:
					radioDone.Checked=true;
					break;
			}
			textUser.Text=Userods.GetName(Cur.UserNum);//might be blank. 
			if(Cur.DateTimeEntry.Year<1880){
				textDateTimeEntry.Text=DateTime.Now.ToString();
			}
			else{
				textDateTimeEntry.Text=Cur.DateTimeEntry.ToString();
			}
			if(Cur.DateTimeFinished.Year<1880){
				textDateTimeFinished.Text="";//DateTime.Now.ToString();
			}
			else{
				textDateTimeFinished.Text=Cur.DateTimeFinished.ToString();
			}
			textDescript.Text=Cur.Descript;
			if(Cur.DateTask.Year>1880){
				textDateTask.Text=Cur.DateTask.ToShortDateString();
			}
			if(Cur.IsRepeating){
				radioNew.Enabled=false;
				radioViewed.Enabled=false;
				radioDone.Enabled=false;
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

		private void butNow_Click(object sender,EventArgs e) {
			textDateTimeEntry.Text=MiscData.GetNowDateTime().ToString();
		}

		private void butNowFinished_Click(object sender,EventArgs e) {
			textDateTimeFinished.Text=MiscData.GetNowDateTime().ToString();
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
				Cur.KeyNum=FormA.AptNumsSelected[0];
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
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(textDateTimeEntry.Text!=""){
				try{
					DateTime.Parse(textDateTimeEntry.Text);
				}
				catch{
					MsgBox.Show(this,"Please fix Date/Time Entry.");
					return false;
				}
			}
			if(textDateTimeFinished.Text!=""){
				try{
					DateTime.Parse(textDateTimeFinished.Text);
				}
				catch{
					MsgBox.Show(this,"Please fix Date/Time Finished.");
					return false;
				}
			}
			if(radioNew.Checked){
				Cur.TaskStatus=TaskStatusEnum.New;
			}
			else if(radioViewed.Checked){
				Cur.TaskStatus=TaskStatusEnum.Viewed;
			}
			else{
				Cur.TaskStatus=TaskStatusEnum.Done;
			}
			//UserNum not allowed to change
			Cur.DateTimeEntry=PIn.PDateT(textDateTimeEntry.Text);
			if(Cur.TaskStatus==TaskStatusEnum.Done && textDateTimeFinished.Text==""){
				Cur.DateTimeFinished=DateTime.Now;
			}
			else{
				Cur.DateTimeFinished=PIn.PDateT(textDateTimeFinished.Text);
			}
			Cur.Descript=textDescript.Text;
			Cur.DateTask=PIn.PDate(textDateTask.Text);
			Cur.DateType=(TaskDateType)listDateType.SelectedIndex;
			if(!checkFromNum.Checked){//user unchecked the box. Never allowed to check if initially unchecked
				Cur.FromNum=0;
			}
			//ObjectType already handled
			//Cur.KeyNum already handled
			try{
				if(IsNew){
					Tasks.Insert(Cur);
				}
				else{
					Tasks.Update(Cur,CurOld);
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete?")) {
				return;
			}
			Tasks.Delete(Cur);
			DialogResult=DialogResult.OK;
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





















