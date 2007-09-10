using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{

	///<summary></summary>
	public class ContrStaff : System.Windows.Forms.UserControl{
		private OpenDental.UI.Button butTimeCard;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.Label textTime;
		private System.Windows.Forms.Timer timer1;
		private OpenDental.UI.Button butClockIn;
		private OpenDental.UI.Button butClockOut;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butSendClaims;
		private OpenDental.UI.Button butTasks;
		private OpenDental.UI.Button butBackup;
		private OpenDental.UI.ODGrid gridEmp;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.UI.Button butDeposit;
		private OpenDental.UI.Button butBreaks;
		private OpenDental.UI.Button butBilling;
		private OpenDental.UI.Button butAccounting;
		private Label label7;
		private ListBox listMessages;
		private Label label5;
		private ListBox listExtras;
		private Label label4;
		private ListBox listFrom;
		private Label label3;
		private ListBox listTo;
		private Label label1;
		private ODGrid gridMessages;
		private CheckBox checkIncludeAck;
		///<summary>Server time minus local computer time, usually +/- 1 or 2 minutes</summary>
		private TimeSpan TimeDelta;
		private OpenDental.UI.Button butSend;
		private Label label6;
		private ComboBox comboViewUser;
		private Label labelDays;
		private TextBox textDays;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		///<summary>Collection of Signals</summary>
		private ArrayList SignalList;
		private SigElementDef[] sigElementDefUser;
		private SigElementDef[] sigElementDefExtras;
		private Label labelSending;
		private Timer timerSending;
		private ODErrorProvider errorProvider1=new ODErrorProvider();
		private OpenDental.UI.Button butAck;
		private SigElementDef[] sigElementDefMessages;
		private Employee EmployeeCur;

		///<summary></summary>
		public ContrStaff(){
			Logger.openlog.Log("Initializing management module...",Logger.Severity.INFO);
			InitializeComponent();
			this.listStatus.Click += new System.EventHandler(this.listStatus_Click);
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

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrStaff));
			this.listStatus = new System.Windows.Forms.ListBox();
			this.textTime = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butBreaks = new OpenDental.UI.Button();
			this.gridEmp = new OpenDental.UI.ODGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.butClockOut = new OpenDental.UI.Button();
			this.butTimeCard = new OpenDental.UI.Button();
			this.butClockIn = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listMessages = new System.Windows.Forms.ListBox();
			this.butSend = new OpenDental.UI.Button();
			this.butAck = new OpenDental.UI.Button();
			this.labelSending = new System.Windows.Forms.Label();
			this.textDays = new System.Windows.Forms.TextBox();
			this.labelDays = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comboViewUser = new System.Windows.Forms.ComboBox();
			this.gridMessages = new OpenDental.UI.ODGrid();
			this.checkIncludeAck = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.listExtras = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.listFrom = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listTo = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butBilling = new OpenDental.UI.Button();
			this.butAccounting = new OpenDental.UI.Button();
			this.butDeposit = new OpenDental.UI.Button();
			this.butSendClaims = new OpenDental.UI.Button();
			this.butBackup = new OpenDental.UI.Button();
			this.butTasks = new OpenDental.UI.Button();
			this.timerSending = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(367,178);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(107,43);
			this.listStatus.TabIndex = 12;
			// 
			// textTime
			// 
			this.textTime.Font = new System.Drawing.Font("Microsoft Sans Serif",13F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textTime.Location = new System.Drawing.Point(365,99);
			this.textTime.Name = "textTime";
			this.textTime.Size = new System.Drawing.Size(109,21);
			this.textTime.TabIndex = 17;
			this.textTime.Text = "12:00:00 PM";
			this.textTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butBreaks);
			this.groupBox1.Controls.Add(this.gridEmp);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listStatus);
			this.groupBox1.Controls.Add(this.butClockOut);
			this.groupBox1.Controls.Add(this.butTimeCard);
			this.groupBox1.Controls.Add(this.textTime);
			this.groupBox1.Controls.Add(this.butClockIn);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(349,5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(510,230);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Time Clock";
			// 
			// butBreaks
			// 
			this.butBreaks.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBreaks.Autosize = true;
			this.butBreaks.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBreaks.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBreaks.CornerRadius = 4F;
			this.butBreaks.Location = new System.Drawing.Point(366,51);
			this.butBreaks.Name = "butBreaks";
			this.butBreaks.Size = new System.Drawing.Size(108,25);
			this.butBreaks.TabIndex = 22;
			this.butBreaks.Text = "View Breaks";
			this.butBreaks.Click += new System.EventHandler(this.butBreaks_Click);
			// 
			// gridEmp
			// 
			this.gridEmp.AllowSelection = false;
			this.gridEmp.HScrollVisible = false;
			this.gridEmp.Location = new System.Drawing.Point(22,22);
			this.gridEmp.Name = "gridEmp";
			this.gridEmp.ScrollValue = 0;
			this.gridEmp.Size = new System.Drawing.Size(303,199);
			this.gridEmp.TabIndex = 21;
			this.gridEmp.Title = "Employee";
			this.gridEmp.TranslationName = "TableEmpClock";
			this.gridEmp.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridEmp_CellClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(376,80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88,19);
			this.label2.TabIndex = 20;
			this.label2.Text = "Server Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// butClockOut
			// 
			this.butClockOut.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClockOut.Autosize = true;
			this.butClockOut.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClockOut.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClockOut.CornerRadius = 4F;
			this.butClockOut.Location = new System.Drawing.Point(366,150);
			this.butClockOut.Name = "butClockOut";
			this.butClockOut.Size = new System.Drawing.Size(108,25);
			this.butClockOut.TabIndex = 14;
			this.butClockOut.Text = "Clock Out For:";
			this.butClockOut.Click += new System.EventHandler(this.butClockOut_Click);
			// 
			// butTimeCard
			// 
			this.butTimeCard.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTimeCard.Autosize = true;
			this.butTimeCard.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTimeCard.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTimeCard.CornerRadius = 4F;
			this.butTimeCard.Location = new System.Drawing.Point(366,24);
			this.butTimeCard.Name = "butTimeCard";
			this.butTimeCard.Size = new System.Drawing.Size(108,25);
			this.butTimeCard.TabIndex = 16;
			this.butTimeCard.Text = "View Timecard";
			this.butTimeCard.Click += new System.EventHandler(this.butTimeCard_Click);
			// 
			// butClockIn
			// 
			this.butClockIn.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClockIn.Autosize = true;
			this.butClockIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClockIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClockIn.CornerRadius = 4F;
			this.butClockIn.Location = new System.Drawing.Point(366,123);
			this.butClockIn.Name = "butClockIn";
			this.butClockIn.Size = new System.Drawing.Size(108,25);
			this.butClockIn.TabIndex = 11;
			this.butClockIn.Text = "Clock In";
			this.butClockIn.Click += new System.EventHandler(this.butClockIn_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listMessages);
			this.groupBox2.Controls.Add(this.butSend);
			this.groupBox2.Controls.Add(this.butAck);
			this.groupBox2.Controls.Add(this.labelSending);
			this.groupBox2.Controls.Add(this.textDays);
			this.groupBox2.Controls.Add(this.labelDays);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.comboViewUser);
			this.groupBox2.Controls.Add(this.gridMessages);
			this.groupBox2.Controls.Add(this.checkIncludeAck);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.listExtras);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.listFrom);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.listTo);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textMessage);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(3,241);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(902,458);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Messaging";
			// 
			// listMessages
			// 
			this.listMessages.FormattingEnabled = true;
			this.listMessages.Location = new System.Drawing.Point(252,35);
			this.listMessages.Name = "listMessages";
			this.listMessages.Size = new System.Drawing.Size(98,368);
			this.listMessages.TabIndex = 10;
			this.listMessages.Click += new System.EventHandler(this.listMessages_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(252,428);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(98,25);
			this.butSend.TabIndex = 15;
			this.butSend.Text = "Send Text";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butAck
			// 
			this.butAck.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAck.Autosize = true;
			this.butAck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAck.CornerRadius = 4F;
			this.butAck.Location = new System.Drawing.Point(645,10);
			this.butAck.Name = "butAck";
			this.butAck.Size = new System.Drawing.Size(67,22);
			this.butAck.TabIndex = 25;
			this.butAck.Text = "Ack";
			this.butAck.Click += new System.EventHandler(this.butAck_Click);
			// 
			// labelSending
			// 
			this.labelSending.Font = new System.Drawing.Font("Microsoft Sans Serif",9.5F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelSending.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))),((int)(((byte)(0)))),((int)(((byte)(0)))));
			this.labelSending.Location = new System.Drawing.Point(251,404);
			this.labelSending.Name = "labelSending";
			this.labelSending.Size = new System.Drawing.Size(100,21);
			this.labelSending.TabIndex = 24;
			this.labelSending.Text = "Sending";
			this.labelSending.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.labelSending.Visible = false;
			// 
			// textDays
			// 
			this.textDays.Location = new System.Drawing.Point(594,12);
			this.textDays.Name = "textDays";
			this.textDays.Size = new System.Drawing.Size(45,20);
			this.textDays.TabIndex = 19;
			this.textDays.Visible = false;
			this.textDays.TextChanged += new System.EventHandler(this.textDays_TextChanged);
			// 
			// labelDays
			// 
			this.labelDays.Location = new System.Drawing.Point(531,14);
			this.labelDays.Name = "labelDays";
			this.labelDays.Size = new System.Drawing.Size(61,16);
			this.labelDays.TabIndex = 18;
			this.labelDays.Text = "Days";
			this.labelDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelDays.Visible = false;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(725,14);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57,16);
			this.label6.TabIndex = 17;
			this.label6.Text = "To User";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboViewUser
			// 
			this.comboViewUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboViewUser.FormattingEnabled = true;
			this.comboViewUser.Location = new System.Drawing.Point(783,11);
			this.comboViewUser.Name = "comboViewUser";
			this.comboViewUser.Size = new System.Drawing.Size(114,21);
			this.comboViewUser.TabIndex = 16;
			this.comboViewUser.SelectionChangeCommitted += new System.EventHandler(this.comboViewUser_SelectionChangeCommitted);
			// 
			// gridMessages
			// 
			this.gridMessages.HScrollVisible = false;
			this.gridMessages.Location = new System.Drawing.Point(356,35);
			this.gridMessages.Name = "gridMessages";
			this.gridMessages.ScrollValue = 0;
			this.gridMessages.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMessages.Size = new System.Drawing.Size(540,417);
			this.gridMessages.TabIndex = 13;
			this.gridMessages.Title = "Message History";
			this.gridMessages.TranslationName = "TableTextMessages";
			// 
			// checkIncludeAck
			// 
			this.checkIncludeAck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIncludeAck.Location = new System.Drawing.Point(356,16);
			this.checkIncludeAck.Name = "checkIncludeAck";
			this.checkIncludeAck.Size = new System.Drawing.Size(173,18);
			this.checkIncludeAck.TabIndex = 14;
			this.checkIncludeAck.Text = "Include Acknowledged";
			this.checkIncludeAck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIncludeAck.UseVisualStyleBackColor = true;
			this.checkIncludeAck.Click += new System.EventHandler(this.checkIncludeAck_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6,413);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100,16);
			this.label7.TabIndex = 12;
			this.label7.Text = "Text Message";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(250,16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 9;
			this.label5.Text = "Message (&& Send)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listExtras
			// 
			this.listExtras.FormattingEnabled = true;
			this.listExtras.Location = new System.Drawing.Point(171,35);
			this.listExtras.Name = "listExtras";
			this.listExtras.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listExtras.Size = new System.Drawing.Size(75,368);
			this.listExtras.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(169,16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78,16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Extras";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listFrom
			// 
			this.listFrom.FormattingEnabled = true;
			this.listFrom.Location = new System.Drawing.Point(90,35);
			this.listFrom.Name = "listFrom";
			this.listFrom.Size = new System.Drawing.Size(75,368);
			this.listFrom.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(88,16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78,16);
			this.label3.TabIndex = 5;
			this.label3.Text = "From";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listTo
			// 
			this.listTo.FormattingEnabled = true;
			this.listTo.Location = new System.Drawing.Point(9,35);
			this.listTo.Name = "listTo";
			this.listTo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listTo.Size = new System.Drawing.Size(75,368);
			this.listTo.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78,16);
			this.label1.TabIndex = 3;
			this.label1.Text = "To";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(9,432);
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(237,20);
			this.textMessage.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butBilling);
			this.groupBox3.Controls.Add(this.butAccounting);
			this.groupBox3.Controls.Add(this.butDeposit);
			this.groupBox3.Controls.Add(this.butSendClaims);
			this.groupBox3.Controls.Add(this.butBackup);
			this.groupBox3.Controls.Add(this.butTasks);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(34,5);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(286,119);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Daily";
			// 
			// butBilling
			// 
			this.butBilling.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBilling.Autosize = true;
			this.butBilling.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBilling.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBilling.CornerRadius = 4F;
			this.butBilling.Location = new System.Drawing.Point(16,45);
			this.butBilling.Name = "butBilling";
			this.butBilling.Size = new System.Drawing.Size(104,26);
			this.butBilling.TabIndex = 25;
			this.butBilling.Text = "Billing";
			this.butBilling.Click += new System.EventHandler(this.butBilling_Click);
			// 
			// butAccounting
			// 
			this.butAccounting.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAccounting.Autosize = true;
			this.butAccounting.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAccounting.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAccounting.CornerRadius = 4F;
			this.butAccounting.Image = ((System.Drawing.Image)(resources.GetObject("butAccounting.Image")));
			this.butAccounting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAccounting.Location = new System.Drawing.Point(148,71);
			this.butAccounting.Name = "butAccounting";
			this.butAccounting.Size = new System.Drawing.Size(104,26);
			this.butAccounting.TabIndex = 24;
			this.butAccounting.Text = "Accounting";
			this.butAccounting.Click += new System.EventHandler(this.butAccounting_Click);
			// 
			// butDeposit
			// 
			this.butDeposit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeposit.Autosize = true;
			this.butDeposit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeposit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeposit.CornerRadius = 4F;
			this.butDeposit.Image = ((System.Drawing.Image)(resources.GetObject("butDeposit.Image")));
			this.butDeposit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeposit.Location = new System.Drawing.Point(16,71);
			this.butDeposit.Name = "butDeposit";
			this.butDeposit.Size = new System.Drawing.Size(104,26);
			this.butDeposit.TabIndex = 23;
			this.butDeposit.Text = "Deposits";
			this.butDeposit.Click += new System.EventHandler(this.butDeposit_Click);
			// 
			// butSendClaims
			// 
			this.butSendClaims.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSendClaims.Autosize = true;
			this.butSendClaims.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSendClaims.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSendClaims.CornerRadius = 4F;
			this.butSendClaims.Image = ((System.Drawing.Image)(resources.GetObject("butSendClaims.Image")));
			this.butSendClaims.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSendClaims.Location = new System.Drawing.Point(16,19);
			this.butSendClaims.Name = "butSendClaims";
			this.butSendClaims.Size = new System.Drawing.Size(104,26);
			this.butSendClaims.TabIndex = 20;
			this.butSendClaims.Text = "Send Claims";
			this.butSendClaims.Click += new System.EventHandler(this.butSendClaims_Click);
			// 
			// butBackup
			// 
			this.butBackup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBackup.Autosize = true;
			this.butBackup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackup.CornerRadius = 4F;
			this.butBackup.Image = ((System.Drawing.Image)(resources.GetObject("butBackup.Image")));
			this.butBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBackup.Location = new System.Drawing.Point(148,45);
			this.butBackup.Name = "butBackup";
			this.butBackup.Size = new System.Drawing.Size(104,26);
			this.butBackup.TabIndex = 22;
			this.butBackup.Text = "Backup";
			this.butBackup.Click += new System.EventHandler(this.butBackup_Click);
			// 
			// butTasks
			// 
			this.butTasks.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butTasks.Autosize = true;
			this.butTasks.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTasks.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTasks.CornerRadius = 4F;
			this.butTasks.Image = ((System.Drawing.Image)(resources.GetObject("butTasks.Image")));
			this.butTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTasks.Location = new System.Drawing.Point(148,19);
			this.butTasks.Name = "butTasks";
			this.butTasks.Size = new System.Drawing.Size(104,26);
			this.butTasks.TabIndex = 21;
			this.butTasks.Text = "Tasks";
			this.butTasks.Click += new System.EventHandler(this.butTasks_Click);
			// 
			// timerSending
			// 
			this.timerSending.Interval = 1000;
			this.timerSending.Tick += new System.EventHandler(this.timerSending_Tick);
			// 
			// ContrStaff
			// 
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ContrStaff";
			this.Size = new System.Drawing.Size(908,702);
			this.Load += new System.EventHandler(this.ContrStaff_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrStaff_Load(object sender, System.EventArgs e) {
		
		}

		///<summary>Only gets run on startup.</summary>
		public void InitializeOnStartup(){
			//can't use Lan.F
			Lan.C(this,new Control[]
				{
				groupBox2,
				label1,
				butSend,
				groupBox1,
				butTimeCard,
				label2,
				butClockIn,
				butClockOut
				});
			RefreshFullMessages();//after this, messages just get added to the list.
			//But if checkIncludeAck is clicked,then it does RefreshMessages again.
		}

		///<summary></summary>
		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			//this is not getting triggered yet.
		}

		private void RefreshModuleData(){
			TimeDelta=ClockEvents.GetServerTime()-DateTime.Now;
			Employees.Refresh();
		}

		private void RefreshModuleScreen(){
			ParentForm.Text=Patients.GetMainTitle(null);
			textTime.Text=(DateTime.Now+TimeDelta).ToLongTimeString();
			FillEmps();
			FillMessageDefs();
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void butSendClaims_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			FormClaimsSend FormCS=new FormClaimsSend();
			FormCS.ShowDialog();
			if(FormCS.GotoPatNum!=0 && FormCS.GotoClaimNum!=0) {
				OnPatientSelected(FormCS.GotoPatNum);
				GotoModule.GotoClaim(FormCS.GotoClaimNum);
			}
			Cursor=Cursors.Default;
		}

		private void butBilling_Click(object sender,System.EventArgs e) {
			//if(!Security.IsAuthorized(Permissions.Setup)) {
			//	return;
			//}
			FormBillingOptions FormBO=new FormBillingOptions();
			FormBO.ShowDialog();
			//RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Billing");
		}


		private void butDeposit_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.DepositSlips,DateTime.Today)){
				return;
			}
			FormDeposits FormD=new FormDeposits();
			FormD.ShowDialog();
		}

		private void butAccounting_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Accounting)) {
				return;
			}
			FormAccounting FormA=new FormAccounting();
			FormA.Show();
		}

		private void butBackup_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Backup)){
				return;
			}
			FormBackup FormB=new FormBackup();
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.Cancel){
				return;
			}
			//ok signifies that a database was restored
			OnPatientSelected(0);
			ParentForm.Text=PrefB.GetString("MainWindowTitle");
			DataValid.SetInvalid(true);
			ModuleSelected();
		}

		private void butTasks_Click(object sender, System.EventArgs e) {
			FormTasks FormT=new FormTasks();
			FormT.ShowDialog();
			if(FormT.GotoType==TaskObjectType.Patient){
				if(FormT.GotoKeyNum!=0){
					OnPatientSelected(FormT.GotoKeyNum);
					GotoModule.GotoAccount();
				}
			}
			if(FormT.GotoType==TaskObjectType.Appointment){
				if(FormT.GotoKeyNum!=0){
					Appointment apt=Appointments.GetOneApt(FormT.GotoKeyNum);
					if(apt==null){
						MsgBox.Show(this,"Appointment has been deleted, so it's not available.");
						return;
						//this could be a little better, because window has closed, but they will learn not to push that button.
					}
					DateTime dateSelected=DateTime.MinValue;
					if(apt.AptStatus==ApptStatus.Planned || apt.AptStatus==ApptStatus.UnschedList){
						//I did not add feature to put planned or unsched apt on pinboard.
						MsgBox.Show(this,"Cannot navigate to appointment.  Use the Other Appointments button.");
						//return;
					}
					else{
						dateSelected=apt.AptDateTime;
					}
					OnPatientSelected(apt.PatNum);
					GotoModule.GotoAppointment(dateSelected,apt.AptNum);
				}
			}
		}

		

		//private void butClear_Click(object sender, System.EventArgs e) {
			//textMessage.Clear();
			//textMessage.Select();
		//}

		private void FillEmps(){
			gridEmp.BeginUpdate();
			gridEmp.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableEmpClock","Employee"),180);
			gridEmp.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableEmpClock","Status"),104);
			gridEmp.Columns.Add(col);
			gridEmp.Rows.Clear();
			UI.ODGridRow row;
			for(int i=0;i<Employees.ListShort.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(Employees.GetNameFL(Employees.ListShort[i]));
				row.Cells.Add(Employees.ListShort[i].ClockStatus);
				gridEmp.Rows.Add(row);
			}
			gridEmp.EndUpdate();
			listStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(TimeClockStatus)).Length;i++){
				listStatus.Items.Add(Lan.g("enumTimeClockStatus",Enum.GetNames(typeof(TimeClockStatus))[i]));
			}
			for(int i=0;i<Employees.ListShort.Length;i++){
				if(Employees.ListShort[i].EmployeeNum==Security.CurUser.EmployeeNum){
					SelectEmpI(i);
					return;
				}
			}
			SelectEmpI(-1);
		}

		///<summary>-1 is also valid.</summary>
		private void SelectEmpI(int index){
			gridEmp.SetSelected(false);
			if(index==-1){
				butClockIn.Enabled=false;
				butClockOut.Enabled=false;
				butTimeCard.Enabled=false;
				butBreaks.Enabled=false;
				listStatus.Enabled=false;
				return;
			}
			gridEmp.SetSelected(index,true);
			EmployeeCur=Employees.ListShort[index];
			if(ClockEvents.IsClockedIn(EmployeeCur.EmployeeNum)){
				butClockIn.Enabled=false;
				butClockOut.Enabled=true;
				butTimeCard.Enabled=true;
				butBreaks.Enabled=true;
				listStatus.Enabled=true;
			}
			else{
				butClockIn.Enabled=true;
				butClockOut.Enabled=false;
				butTimeCard.Enabled=true;
				butBreaks.Enabled=true;
				listStatus.SelectedIndex=(int)ClockEvents.GetLastStatus(EmployeeCur.EmployeeNum);
				listStatus.Enabled=false;
			}
		}

		private void gridEmp_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(PrefB.GetBool("TimecardSecurityEnabled")){
				if(Security.CurUser.EmployeeNum!=Employees.ListShort[e.Row].EmployeeNum){
					if(!Security.IsAuthorized(Permissions.TimecardsEditAll)){
						SelectEmpI(-1);
						return;
					}
				}
			}
			SelectEmpI(e.Row);
		}

		private void listStatus_Click(object sender, System.EventArgs e) {
			//
		}

		private void butClockIn_Click(object sender, System.EventArgs e) {
			ClockEvent ce=new ClockEvent();
			ce.EmployeeNum=EmployeeCur.EmployeeNum;
			ce.TimeEntered=DateTime.Now+TimeDelta;
			ce.TimeDisplayed=DateTime.Now+TimeDelta;
			ce.ClockIn=true;
			ce.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ClockEvents.Insert(ce);
			EmployeeCur.ClockStatus=Lan.g(this,"Working");;
			Employees.Update(EmployeeCur);
			ModuleSelected();
		}

		private void butClockOut_Click(object sender, System.EventArgs e) {
			if(listStatus.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a status first."));
				return;
			}
			ClockEvent ce=new ClockEvent();
			ce.EmployeeNum=EmployeeCur.EmployeeNum;
			ce.TimeEntered=DateTime.Now+TimeDelta;
			ce.TimeDisplayed=DateTime.Now+TimeDelta;
			ce.ClockIn=false;
			ce.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			ClockEvents.Insert(ce);
			EmployeeCur.ClockStatus=Lan.g("enumTimeClockStatus",ce.ClockStatus.ToString());
			Employees.Update(EmployeeCur);
			ModuleSelected();
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			//this will happen once a second
			if(this.Visible){
				textTime.Text=(DateTime.Now+TimeDelta).ToLongTimeString();
			}
		}

		private void butTimeCard_Click(object sender, System.EventArgs e) {
			if(PayPeriods.List.Length==0){
				MsgBox.Show(this,"The adminstrator needs to setup pay periods first.");
				return;
			}
			FormTimeCard FormTC=new FormTimeCard();
			FormTC.EmployeeCur=EmployeeCur;
			FormTC.ShowDialog();
			ModuleSelected();
		}

		private void butBreaks_Click(object sender,EventArgs e) {
			if(PayPeriods.List.Length==0) {
				MsgBox.Show(this,"The adminstrator needs to setup pay periods first.");
				return;
			}
			FormTimeCard FormTC=new FormTimeCard();
			FormTC.EmployeeCur=EmployeeCur;
			FormTC.IsBreaks=true;
			FormTC.ShowDialog();
			ModuleSelected();
		}

		#region Messaging
		///<summary>Gets run with each module selected.  Should be very fast.</summary>
		private void FillMessageDefs(){
			sigElementDefUser=SigElementDefs.GetSubList(SignalElementType.User);
			sigElementDefExtras=SigElementDefs.GetSubList(SignalElementType.Extra);
			sigElementDefMessages=SigElementDefs.GetSubList(SignalElementType.Message);
			listTo.Items.Clear();
			for(int i=0;i<sigElementDefUser.Length;i++){
				listTo.Items.Add(sigElementDefUser[i].SigText);
			}
			listFrom.Items.Clear();
			for(int i=0;i<sigElementDefUser.Length;i++) {
				listFrom.Items.Add(sigElementDefUser[i].SigText);
			}
			listExtras.Items.Clear();
			for(int i=0;i<sigElementDefExtras.Length;i++) {
				listExtras.Items.Add(sigElementDefExtras[i].SigText);
			}
			listMessages.Items.Clear();
			for(int i=0;i<sigElementDefMessages.Length;i++) {
				listMessages.Items.Add(sigElementDefMessages[i].SigText);
			}
			comboViewUser.Items.Clear();
			comboViewUser.Items.Add(Lan.g(this,"all"));
			for(int i=0;i<sigElementDefUser.Length;i++) {
				comboViewUser.Items.Add(sigElementDefUser[i].SigText);
			}
			comboViewUser.SelectedIndex=0;
		}

		///<summary>Gets all new data from the database for the text messages.  Not sure yet if this will also reset the lights along the left.</summary>
		private void RefreshFullMessages(){
			SignalList=Signals.RefreshFullText(DateTime.Today);//since midnight this morning.
			FillMessages();
		}

		///<summary>This does not refresh any data, just fills the grid.</summary>
		private void FillMessages(){
			if(textDays.Visible && errorProvider1.GetError(textDays) !=""){
				return;
			}
			int[] selected=new int[gridMessages.SelectedIndices.Length];
			for(int i=0;i<selected.Length;i++){
				selected[i]=((Signal)SignalList[gridMessages.SelectedIndices[i]]).SignalNum;
			}
			gridMessages.BeginUpdate();
			gridMessages.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTextMessages","To"),60);
			gridMessages.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTextMessages","From"),60);
			gridMessages.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTextMessages","Sent"),63);
			gridMessages.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTextMessages","Ack'd"),63);
			col.TextAlign=HorizontalAlignment.Center;
			gridMessages.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTextMessages","Text"),274);
			gridMessages.Columns.Add(col);
			gridMessages.Rows.Clear();
			ODGridRow row;
			Signal sig;
			string str;
			for(int i=0;i<SignalList.Count;i++){
				sig=(Signal)SignalList[i];
				if(checkIncludeAck.Checked){
					if(sig.AckTime.Year>1880//if this is acked
						&& sig.AckTime < DateTime.Today.AddDays(1-PIn.PInt(textDays.Text)))
					{
						continue;
					}
				}
				else{//user does not want to include acked
					if(sig.AckTime.Year>1880){//if this is acked
						continue;
					}
				}
				if(sig.ToUser!=""//blank user always shows
					&& comboViewUser.SelectedIndex!=0 //anything other than 'all'
					&& sigElementDefUser!=null//for startup
					&& sigElementDefUser[comboViewUser.SelectedIndex-1].SigText!=sig.ToUser)//and users don't match
				{
					continue;
				}
				row=new ODGridRow();
				row.Cells.Add(sig.ToUser);
				row.Cells.Add(sig.FromUser);
				if(sig.SigDateTime.Date==DateTime.Today){
					row.Cells.Add(sig.SigDateTime.ToShortTimeString());
				}
				else{
					row.Cells.Add(sig.SigDateTime.ToShortDateString()+"\r\n"+sig.SigDateTime.ToShortTimeString());
				}
				if(sig.AckTime.Year>1880){//ok
					if(sig.AckTime.Date==DateTime.Today) {
						row.Cells.Add(sig.AckTime.ToShortTimeString());
					}
					else {
						row.Cells.Add(sig.AckTime.ToShortDateString()+"\r\n"+sig.AckTime.ToShortTimeString());
					}
				}
				else{
					row.Cells.Add("");
				}
				str=sig.SigText;
				for(int e=0;e<sig.ElementList.Length;e++){
					if(SigElementDefs.GetElement(sig.ElementList[e].SigElementDefNum).SigElementType==SignalElementType.User){
						continue;//we already have text copies of the users.
					}
					if(str!=""){
						str+=".  ";
					}
					str+=SigElementDefs.GetElement(sig.ElementList[e].SigElementDefNum).SigText;
				}
				row.Cells.Add(str);
				row.Tag=sig.Copy();
				gridMessages.Rows.Add(row);
				if(Array.IndexOf(selected,sig.SignalNum)!=-1){
					gridMessages.SetSelected(gridMessages.Rows.Count-1,true);
				}
			}
			gridMessages.EndUpdate();
		}

		private void butSend_Click(object sender, System.EventArgs e){
			//if(listTo.SelectedIndex==-1){
			//	MsgBox.Show(this,"Please select who to send the message to.");
			//	return;
			//}
			if(textMessage.Text=="") {
				MsgBox.Show(this,"Please type in a message first.");
				return;
			}
			Signal sig=new Signal();
			sig.SigType=SignalType.Button;
			sig.SigText=textMessage.Text;
			if(listTo.SelectedIndex!=-1) {
				sig.ToUser=sigElementDefUser[listTo.SelectedIndex].SigText;
			}
			if(listFrom.SelectedIndex!=-1){
				sig.FromUser=sigElementDefUser[listFrom.SelectedIndex].SigText;
			}
			Signals.Insert(sig);
			SigElement element;
			if(listTo.SelectedIndex!=-1) {
				element=new SigElement();
				element.SigElementDefNum=sigElementDefUser[listTo.SelectedIndex].SigElementDefNum;
				element.SignalNum=sig.SignalNum;
				SigElements.Insert(element);
			}
			textMessage.Text="";
			listFrom.SelectedIndex=-1;
			listTo.SelectedIndex=-1;
			listExtras.SelectedIndex=-1;
			listMessages.SelectedIndex=-1;
			labelSending.Visible=true;
			timerSending.Enabled=true;
		}

		private void listMessages_Click(object sender,EventArgs e) {
			if(listMessages.SelectedIndex==-1){
				return;
			}
			Signal sig=new Signal();
			sig.SigType=SignalType.Button;
			sig.SigText=textMessage.Text;
			if(listTo.SelectedIndex!=-1) {
				sig.ToUser=sigElementDefUser[listTo.SelectedIndex].SigText;
			}
			if(listFrom.SelectedIndex!=-1) {
				sig.FromUser=sigElementDefUser[listFrom.SelectedIndex].SigText;
			}
			//need to do this all as a transaction, so need to do a writelock on the signal table first.
			//alternatively, we could just make sure not to retrieve any signals that were less the 300ms old.
			Signals.Insert(sig);
			SigElement element;
			if(listTo.SelectedIndex!=-1) {
				element=new SigElement();
				element.SigElementDefNum=sigElementDefUser[listTo.SelectedIndex].SigElementDefNum;
				element.SignalNum=sig.SignalNum;
				SigElements.Insert(element);
			}
			//We do not insert an element for From
			if(listExtras.SelectedIndex!=-1) {
				element=new SigElement();
				element.SigElementDefNum=sigElementDefExtras[listExtras.SelectedIndex].SigElementDefNum;
				element.SignalNum=sig.SignalNum;
				SigElements.Insert(element);
			}
			element=new SigElement();
			element.SigElementDefNum=sigElementDefMessages[listMessages.SelectedIndex].SigElementDefNum;
			element.SignalNum=sig.SignalNum;
			SigElements.Insert(element);
			//reset the controls
			textMessage.Text="";
			listFrom.SelectedIndex=-1;
			listTo.SelectedIndex=-1;
			listExtras.SelectedIndex=-1;
			listMessages.SelectedIndex=-1;
			labelSending.Visible=true;
			timerSending.Enabled=true;
		}

		///<summary>This processes timed messages coming in from the main form.  Buttons are handled in the main form, and then sent here for further display.  The list gets filtered before display.</summary>
		public void LogMsgs(Signal[] signalList){
			for(int i=0;i<signalList.Length;i++){
				if(signalList[i].AckTime.Year>1880){//if ack
					//then find the original
					for(int s=0;s<SignalList.Count;s++){
						if(((Signal)SignalList[s]).SignalNum==signalList[i].SignalNum){
							//alter the original
							((Signal)SignalList[s]).AckTime=signalList[i].AckTime;
							break;//out of s loop.
						}
					}
				}
				else{
					SignalList.Add(signalList[i].Copy());
				}
			}
			SignalList.Sort();
			FillMessages();
		}

		private void butAck_Click(object sender,EventArgs e) {
			if(gridMessages.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select at least one item first.");
				return;
			}
			Signal sig;
			for(int i=gridMessages.SelectedIndices.Length-1;i>=0;i--){//go backwards so that we can remove rows without problems.
				sig=(Signal)gridMessages.Rows[gridMessages.SelectedIndices[i]].Tag;
				if(sig.AckTime.Year>1880){
					continue;//totally ignore if trying to ack a previously acked signal
				}
				sig.AckTime=DateTime.Now+TimeDelta;
				Signals.Update(sig);
				//change the grid temporarily until the next timer event.  This makes it feel more responsive.
				if(checkIncludeAck.Checked){
					gridMessages.Rows[gridMessages.SelectedIndices[i]].Cells[3].Text=sig.AckTime.ToShortTimeString();					
				}
				else{
					try{
						gridMessages.Rows.RemoveAt(gridMessages.SelectedIndices[i]);
					}
					catch{
						;//do nothing
					}
				}
			}
			gridMessages.SetSelected(false);
			//gridMessages.Invalidate();
		}

		private void checkIncludeAck_Click(object sender,EventArgs e) {
			if(checkIncludeAck.Checked){
				textDays.Text="1";
				labelDays.Visible=true;
				textDays.Visible=true;
			}
			else{
				labelDays.Visible=false;
				textDays.Visible=false;
			}
			FillMessages();
		}

		private void textDays_TextChanged(object sender,EventArgs e) {
			if(!textDays.Visible){
				errorProvider1.SetError(textDays,"");
				return;
			}
			try{
				int days=int.Parse(textDays.Text);
				errorProvider1.SetError(textDays,"");
				FillMessages();
			}
			catch{
				errorProvider1.SetError(textDays,Lan.g(this,"Invalid number.  Usually 1 or 2."));
			}
		}

		private void comboViewUser_SelectionChangeCommitted(object sender,EventArgs e) {
			FillMessages();
		}

		private void timerSending_Tick(object sender,EventArgs e) {
			labelSending.Visible=false;
			timerSending.Enabled=false;
		}

		#endregion Messaging

		

		

		

		

		

		

		

		

		

















	}
}












