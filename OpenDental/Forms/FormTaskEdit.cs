using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
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
		private OpenDental.ODtextBox textDescript;
		private Task TaskCur;
		private Task TaskOld;
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
		public long GotoKeyNum;
		private Label label8;
		private Label labelReply;
		private OpenDental.UI.Button butReply;
		private OpenDental.UI.Button butSend;
		private Label label9;
		private TextBox textTaskList;
		private Label label10;
		private ComboBox comboDateType;
		private TaskList TaskListCur;
		private UI.ODGrid gridMain;
		///<summary>Will be set to true if any note was added or an existing note changed. Does not track changes in the description.</summary>
		private bool notesChanged;
		private UI.Button butAddNote;
		private List<TaskNote> NoteList;

		///<summary>Task gets inserted ahead of time.</summary>
		public FormTaskEdit(Task taskCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			TaskCur=taskCur;
			TaskOld=taskCur.Copy();
			TaskListCur=TaskLists.GetOne(taskCur.TaskListNum);
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
			this.checkFromNum = new System.Windows.Forms.CheckBox();
			this.labelObjectDesc = new System.Windows.Forms.Label();
			this.textObjectDesc = new System.Windows.Forms.TextBox();
			this.listObjectType = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panelObject = new System.Windows.Forms.Panel();
			this.butGoto = new OpenDental.UI.Button();
			this.butChange = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textDateTimeEntry = new System.Windows.Forms.TextBox();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.radioNew = new System.Windows.Forms.RadioButton();
			this.radioViewed = new System.Windows.Forms.RadioButton();
			this.radioDone = new System.Windows.Forms.RadioButton();
			this.textDateTimeFinished = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTaskNum = new System.Windows.Forms.TextBox();
			this.labelTaskNum = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelReply = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.textTaskList = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.comboDateType = new System.Windows.Forms.ComboBox();
			this.butSend = new OpenDental.UI.Button();
			this.butReply = new OpenDental.UI.Button();
			this.butNowFinished = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butNow = new OpenDental.UI.Button();
			this.textDateTask = new OpenDental.ValidDate();
			this.textDescript = new OpenDental.ODtextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAddNote = new OpenDental.UI.Button();
			this.panelObject.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,105);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116,19);
			this.label1.TabIndex = 2;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9,472);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(218,469);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(185,32);
			this.label3.TabIndex = 6;
			this.label3.Text = "Leave blank unless you want this task to show on a dated list";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,498);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116,19);
			this.label4.TabIndex = 7;
			this.label4.Text = "Date Type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkFromNum
			// 
			this.checkFromNum.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkFromNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkFromNum.Location = new System.Drawing.Point(8,524);
			this.checkFromNum.Name = "checkFromNum";
			this.checkFromNum.Size = new System.Drawing.Size(133,18);
			this.checkFromNum.TabIndex = 3;
			this.checkFromNum.Text = "Is From Repeating";
			this.checkFromNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelObjectDesc
			// 
			this.labelObjectDesc.Location = new System.Drawing.Point(5,1);
			this.labelObjectDesc.Name = "labelObjectDesc";
			this.labelObjectDesc.Size = new System.Drawing.Size(116,19);
			this.labelObjectDesc.TabIndex = 8;
			this.labelObjectDesc.Text = "ObjectDesc";
			this.labelObjectDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textObjectDesc
			// 
			this.textObjectDesc.Location = new System.Drawing.Point(124,1);
			this.textObjectDesc.Multiline = true;
			this.textObjectDesc.Name = "textObjectDesc";
			this.textObjectDesc.Size = new System.Drawing.Size(452,37);
			this.textObjectDesc.TabIndex = 0;
			// 
			// listObjectType
			// 
			this.listObjectType.Location = new System.Drawing.Point(388,499);
			this.listObjectType.Name = "listObjectType";
			this.listObjectType.Size = new System.Drawing.Size(120,43);
			this.listObjectType.TabIndex = 13;
			this.listObjectType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listObjectType_MouseDown);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(269,498);
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
			this.panelObject.Location = new System.Drawing.Point(3,543);
			this.panelObject.Name = "panelObject";
			this.panelObject.Size = new System.Drawing.Size(594,64);
			this.panelObject.TabIndex = 15;
			// 
			// butGoto
			// 
			this.butGoto.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGoto.Autosize = true;
			this.butGoto.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGoto.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGoto.CornerRadius = 4F;
			this.butGoto.Location = new System.Drawing.Point(200,39);
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
			this.butChange.Location = new System.Drawing.Point(123,39);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75,24);
			this.butChange.TabIndex = 10;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(9,57);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116,19);
			this.label5.TabIndex = 17;
			this.label5.Text = "Date/Time Entry";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateTimeEntry
			// 
			this.textDateTimeEntry.Location = new System.Drawing.Point(127,56);
			this.textDateTimeEntry.Name = "textDateTimeEntry";
			this.textDateTimeEntry.Size = new System.Drawing.Size(151,20);
			this.textDateTimeEntry.TabIndex = 18;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(470,7);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(134,20);
			this.textUser.TabIndex = 0;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(375,9);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(94,16);
			this.label16.TabIndex = 125;
			this.label16.Text = "From User";
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
			this.textDateTimeFinished.Location = new System.Drawing.Point(127,81);
			this.textDateTimeFinished.Name = "textDateTimeFinished";
			this.textDateTimeFinished.Size = new System.Drawing.Size(151,20);
			this.textDateTimeFinished.TabIndex = 131;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(9,82);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116,19);
			this.label7.TabIndex = 130;
			this.label7.Text = "Date/Time Finished";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTaskNum
			// 
			this.textTaskNum.Location = new System.Drawing.Point(550,60);
			this.textTaskNum.Name = "textTaskNum";
			this.textTaskNum.ReadOnly = true;
			this.textTaskNum.Size = new System.Drawing.Size(54,20);
			this.textTaskNum.TabIndex = 134;
			this.textTaskNum.Visible = false;
			// 
			// labelTaskNum
			// 
			this.labelTaskNum.Location = new System.Drawing.Point(475,61);
			this.labelTaskNum.Name = "labelTaskNum";
			this.labelTaskNum.Size = new System.Drawing.Size(73,16);
			this.labelTaskNum.TabIndex = 133;
			this.labelTaskNum.Text = "TaskNum";
			this.labelTaskNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelTaskNum.Visible = false;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(606,10);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(133,17);
			this.label8.TabIndex = 139;
			this.label8.Text = "(most recent editor)";
			// 
			// labelReply
			// 
			this.labelReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelReply.Location = new System.Drawing.Point(204,654);
			this.labelReply.Name = "labelReply";
			this.labelReply.Size = new System.Drawing.Size(134,19);
			this.labelReply.TabIndex = 141;
			this.labelReply.Text = "(Send to author)";
			this.labelReply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.Location = new System.Drawing.Point(346,654);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(134,19);
			this.label9.TabIndex = 143;
			this.label9.Text = "(Send to other user)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textTaskList
			// 
			this.textTaskList.Location = new System.Drawing.Point(470,30);
			this.textTaskList.Name = "textTaskList";
			this.textTaskList.ReadOnly = true;
			this.textTaskList.Size = new System.Drawing.Size(134,20);
			this.textTaskList.TabIndex = 146;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(375,32);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(94,16);
			this.label10.TabIndex = 147;
			this.label10.Text = "Task List";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboDateType
			// 
			this.comboDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDateType.FormattingEnabled = true;
			this.comboDateType.Location = new System.Drawing.Point(127,498);
			this.comboDateType.Name = "comboDateType";
			this.comboDateType.Size = new System.Drawing.Size(145,21);
			this.comboDateType.TabIndex = 148;
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(375,628);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75,24);
			this.butSend.TabIndex = 142;
			this.butSend.Text = "Send To...";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butReply
			// 
			this.butReply.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butReply.Autosize = true;
			this.butReply.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReply.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReply.CornerRadius = 4F;
			this.butReply.Location = new System.Drawing.Point(233,628);
			this.butReply.Name = "butReply";
			this.butReply.Size = new System.Drawing.Size(75,24);
			this.butReply.TabIndex = 140;
			this.butReply.Text = "Reply";
			this.butReply.Click += new System.EventHandler(this.butReply_Click);
			// 
			// butNowFinished
			// 
			this.butNowFinished.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNowFinished.Autosize = true;
			this.butNowFinished.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNowFinished.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNowFinished.CornerRadius = 4F;
			this.butNowFinished.Location = new System.Drawing.Point(284,79);
			this.butNowFinished.Name = "butNowFinished";
			this.butNowFinished.Size = new System.Drawing.Size(75,24);
			this.butNowFinished.TabIndex = 132;
			this.butNowFinished.Text = "Now";
			this.butNowFinished.Click += new System.EventHandler(this.butNowFinished_Click);
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
			this.butDelete.Location = new System.Drawing.Point(21,628);
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
			this.butNow.Location = new System.Drawing.Point(284,54);
			this.butNow.Name = "butNow";
			this.butNow.Size = new System.Drawing.Size(75,24);
			this.butNow.TabIndex = 19;
			this.butNow.Text = "Now";
			this.butNow.Click += new System.EventHandler(this.butNow_Click);
			// 
			// textDateTask
			// 
			this.textDateTask.Location = new System.Drawing.Point(127,472);
			this.textDateTask.Name = "textDateTask";
			this.textDateTask.Size = new System.Drawing.Size(87,20);
			this.textDateTask.TabIndex = 2;
			// 
			// textDescript
			// 
			this.textDescript.AcceptsReturn = true;
			this.textDescript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textDescript.Location = new System.Drawing.Point(127,105);
			this.textDescript.Multiline = true;
			this.textDescript.Name = "textDescript";
			this.textDescript.QuickPasteType = OpenDentBusiness.QuickPasteType.None;
			this.textDescript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textDescript.Size = new System.Drawing.Size(683,67);
			this.textDescript.TabIndex = 1;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(653,628);
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
			this.butCancel.Location = new System.Drawing.Point(735,628);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,173);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(798,293);
			this.gridMain.TabIndex = 149;
			this.gridMain.Title = "Notes";
			this.gridMain.TranslationName = "FormTaskEdit";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butAddNote
			// 
			this.butAddNote.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddNote.Autosize = true;
			this.butAddNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddNote.CornerRadius = 4F;
			this.butAddNote.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddNote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddNote.Location = new System.Drawing.Point(648,472);
			this.butAddNote.Name = "butAddNote";
			this.butAddNote.Size = new System.Drawing.Size(80,24);
			this.butAddNote.TabIndex = 150;
			this.butAddNote.Text = "Add";
			this.butAddNote.Click += new System.EventHandler(this.butAddNote_Click);
			// 
			// FormTaskEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(836,676);
			this.Controls.Add(this.butAddNote);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.comboDateType);
			this.Controls.Add(this.textTaskList);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.labelReply);
			this.Controls.Add(this.butReply);
			this.Controls.Add(this.label8);
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
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormTaskEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Task";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTaskEdit_FormClosing);
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
				textTaskNum.Text=TaskCur.TaskNum.ToString();
			#endif
			switch(TaskCur.TaskStatus){
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
			textUser.Text=Userods.GetName(TaskCur.UserNum);//might be blank.
			if(TaskListCur!=null){
				textTaskList.Text=TaskListCur.Descript;
			}
			if(TaskCur.DateTimeEntry.Year<1880){
				textDateTimeEntry.Text=DateTime.Now.ToString();
			}
			else{
				textDateTimeEntry.Text=TaskCur.DateTimeEntry.ToString();
			}
			if(TaskCur.DateTimeFinished.Year<1880){
				textDateTimeFinished.Text="";//DateTime.Now.ToString();
			}
			else{
				textDateTimeFinished.Text=TaskCur.DateTimeFinished.ToString();
			}
			textDescript.Text=TaskCur.Descript;
			//textDescript.Select(textDescript.Text.Length,0);
			if(TaskCur.DateTask.Year>1880){
				textDateTask.Text=TaskCur.DateTask.ToShortDateString();
			}
			if(TaskCur.IsRepeating){
				radioNew.Enabled=false;
				radioViewed.Enabled=false;
				radioDone.Enabled=false;
				textDateTask.Enabled=false;
				listObjectType.Enabled=false;
				if(TaskCur.TaskListNum!=0){//not a main parent
					comboDateType.Enabled=false;
				}
			}
			for(int i=0;i<Enum.GetNames(typeof(TaskDateType)).Length;i++){
				comboDateType.Items.Add(Lan.g("enumTaskDateType",Enum.GetNames(typeof(TaskDateType))[i]));
				if((int)TaskCur.DateType==i){
					comboDateType.SelectedIndex=i;
				}
			}
			if(TaskCur.FromNum==0){
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
			if(IsNew){
				labelReply.Visible=false;
				butReply.Visible=false;
			}
			if(TaskListCur==null || TaskListCur.TaskListNum!=Security.CurUser.TaskListInBox){//if this task is not in my inbox
				labelReply.Visible=false;
				butReply.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Time"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"User"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),400);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			NoteList=TaskNotes.GetForTask(TaskCur.TaskNum);
			for(int i=0;i<NoteList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(NoteList[i].DateTimeNote.ToShortDateString()+" "+NoteList[i].DateTimeNote.ToShortTimeString());
				row.Cells.Add(Userods.GetName(NoteList[i].UserNum));
				row.Cells.Add(NoteList[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollToEnd();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormTaskNoteEdit form=new FormTaskNoteEdit();
			form.TaskNoteCur=NoteList[e.Row];
			form.ShowDialog();
			FillGrid();
		}

		private void butAddNote_Click(object sender,EventArgs e) {
			FormTaskNoteEdit form=new FormTaskNoteEdit();
			form.TaskNoteCur=new TaskNote();
			form.TaskNoteCur.TaskNum=TaskCur.TaskNum;
			form.TaskNoteCur.DateTimeNote=DateTime.Now;//Will be slightly adjusted at server.
			form.TaskNoteCur.UserNum=Security.CurUser.UserNum;
			form.TaskNoteCur.IsNew=true;
			form.ShowDialog();
			FillGrid();
		}

		private void FillObject(){
			if(TaskCur.ObjectType==TaskObjectType.None){
				listObjectType.SelectedIndex=0;
				panelObject.Visible=false;
			}
			else if(TaskCur.ObjectType==TaskObjectType.Patient){
				listObjectType.SelectedIndex=1;
				panelObject.Visible=true;
				labelObjectDesc.Text=Lan.g(this,"Patient Name");
				if(TaskCur.KeyNum>0){
					textObjectDesc.Text=Patients.GetPat(TaskCur.KeyNum).GetNameLF();
				}
				else{
					textObjectDesc.Text="";
				}
			}
			else if(TaskCur.ObjectType==TaskObjectType.Appointment){
				listObjectType.SelectedIndex=2;
				panelObject.Visible=true;
				labelObjectDesc.Text=Lan.g(this,"Appointment Desc");
				if(TaskCur.KeyNum>0){
					Appointment AptCur=Appointments.GetOneApt(TaskCur.KeyNum);
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
			if(TaskCur.KeyNum>0){
				if(!MsgBox.Show(this,true,"The linked object will no longer be attached.  Continue?")){
					FillObject();
					return;
				}
			}
			TaskCur.KeyNum=0;
			TaskCur.ObjectType=(TaskObjectType)listObjectType.SelectedIndex;
			FillObject();
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			if(TaskCur.ObjectType==TaskObjectType.Patient){
				TaskCur.KeyNum=FormPS.SelectedPatNum;
			}
			if(TaskCur.ObjectType==TaskObjectType.Appointment){
				FormApptsOther FormA=new FormApptsOther(FormPS.SelectedPatNum);
				FormA.SelectOnly=true;
				FormA.ShowDialog();
				if(FormA.DialogResult==DialogResult.Cancel){
					return;
				}
				TaskCur.KeyNum=FormA.AptNumsSelected[0];
			}
			FillObject();
		}

		private void butGoto_Click(object sender, System.EventArgs e) {
			if(!SaveCur(false)){
				return;
			}
			GotoType=TaskCur.ObjectType;
			GotoKeyNum=TaskCur.KeyNum;
			DialogResult=DialogResult.OK;
			Close();
		}

		private bool SaveCur(bool resetUser){
			if(textDateTask.errorProvider1.GetError(textDateTask)!=""){
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
				TaskCur.TaskStatus=TaskStatusEnum.New;
			}
			else if(radioViewed.Checked){
				TaskCur.TaskStatus=TaskStatusEnum.Viewed;
			}
			else{
				TaskCur.TaskStatus=TaskStatusEnum.Done;
			}
			//UserNum was not allowed to change in previous versions.  But now, it changes anytime the text changes.
			if(resetUser && TaskCur.Descript!=textDescript.Text){
				TaskCur.UserNum=Security.CurUser.UserNum;
			}
			TaskCur.DateTimeEntry=PIn.DateT(textDateTimeEntry.Text);
			if(TaskCur.TaskStatus==TaskStatusEnum.Done && textDateTimeFinished.Text==""){
				TaskCur.DateTimeFinished=DateTime.Now;
			}
			else{
				TaskCur.DateTimeFinished=PIn.DateT(textDateTimeFinished.Text);
			}
			TaskCur.Descript=textDescript.Text;
			TaskCur.DateTask=PIn.Date(textDateTask.Text);
			TaskCur.DateType=(TaskDateType)comboDateType.SelectedIndex;
			if(!checkFromNum.Checked){//user unchecked the box. Never allowed to check if initially unchecked
				TaskCur.FromNum=0;
			}
			//ObjectType already handled
			//Cur.KeyNum already handled
			try{
				if(IsNew){
					Tasks.Insert(TaskCur);
				}
				else{
					if(!TaskCur.Equals(TaskOld)){//If user clicks OK without making any changes, then skip.
						Tasks.Update(TaskCur,TaskOld);//if task has already been altered, then this is where it will fail.
					}
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!IsNew) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
					return;
				}
			}
			Tasks.Delete(TaskCur.TaskNum);//always do it this way to clean up all three tables
			DataValid.SetInvalidTask(TaskCur.TaskNum,false);//no popup
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butReply_Click(object sender,EventArgs e) {
			//This can't happen if IsNew
			//This also can't happen unless the task is in my inbox.
			if(!notesChanged && textDescript.Text==TaskCur.Descript) {//nothing changed
				MsgBox.Show(this,"Please add a note before using the reply button.");
				return;
			}
			if(TaskCur.UserNum==Security.CurUser.UserNum){
				MsgBox.Show(this,"You can't reply to yourself.");
				return;
			}
			long inbox=Userods.GetInbox(TaskCur.UserNum);
			if(inbox==0){
				MsgBox.Show(this,"No inbox has been set up for this user yet.");
				return;
			}
			TaskCur.TaskListNum=inbox;
			if(!SaveCur(true)){
				return;
			}
			DataValid.SetInvalidTask(TaskCur.TaskNum,true);//popup
			DialogResult=DialogResult.OK;
			Close();
		}

		///<summary>Send to another user.</summary>
		private void butSend_Click(object sender,EventArgs e) {
			//This button is always present.
			FormTaskSendUser FormT=new FormTaskSendUser();
			FormT.ShowDialog();
			if(FormT.DialogResult!=DialogResult.OK){
				return;
			}
			TaskCur.TaskListNum=FormT.TaskListNum;
			if(!SaveCur(true)){
				return;
			}
			DataValid.SetInvalidTask(TaskCur.TaskNum,true);//popup
			DialogResult=DialogResult.OK;
			Close();
		}

		/*
		private void butAppendAndPop_Click(object sender,EventArgs e) {
			if(IsNew) {
				MsgBox.Show(this,"Cannot append to a new task.");
				return;
			}
			if(textAppend.Text=="") {
				MsgBox.Show(this,"Please enter text in the box first.");
				return;
			}
			if(textDescript.Text!=Cur.Descript) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Text in the main description has changed and the change will not be saved.  Continue anyway?")) {
					return;
				}
			}
			Tasks.Append(Cur.TaskNum,textAppend.Text);
			DataValid.SetInvalidTask(Cur.TaskNum,true);
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butAppendNoPop_Click(object sender,EventArgs e) {
			if(IsNew) {
				MsgBox.Show(this,"Cannot append to a new task.");
				return;
			}
			if(textAppend.Text=="") {
				MsgBox.Show(this,"Please enter text in the box first.");
				return;
			}
			if(textDescript.Text!=Cur.Descript){
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Text in the main description has changed and the change will not be saved.  Continue anyway?")){
					return;
				}
			}
			Tasks.Append(Cur.TaskNum,textAppend.Text);
			DataValid.SetInvalidTask(Cur.TaskNum,false);
			DialogResult=DialogResult.OK;
			Close();
		}*/

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveCur(true)){//If user clicked OK without changing anything, then this will have no effect.
				return;
			}
			if(!notesChanged && TaskCur.Equals(TaskOld)){//if there were no changes, then don't bother with the signal
				DialogResult=DialogResult.OK;
				Close();
				return;
			}
			if(IsNew){
				DataValid.SetInvalidTask(TaskCur.TaskNum,true);//popup
			}
			else if(notesChanged || textDescript.Text!=TaskCur.Descript){//notes or descript changed
				DataValid.SetInvalidTask(TaskCur.TaskNum,true);//popup
			}
			else{
				DataValid.SetInvalidTask(TaskCur.TaskNum,false);//no popup
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
			Close();
		}

		private void FormTaskEdit_FormClosing(object sender,FormClosingEventArgs e) {
			TaskUnreads.SetRead(Security.CurUser.UserNum,TaskCur.TaskNum);//no matter why it was closed
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(IsNew) {
				Tasks.Delete(TaskCur.TaskNum);
			}
		}

		

		


		

		

		

		

		
		


		

		

		

		

		

		


	}
}





















