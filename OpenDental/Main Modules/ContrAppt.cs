/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{

	///<summary></summary>
	public class ContrAppt : System.Windows.Forms.UserControl{
		private OpenDental.ContrApptSheet ContrApptSheet2;
		private ContrApptSingle[] ContrApptSingle3;//the '3' has no significance
		private System.Windows.Forms.MonthCalendar Calendar2;
		private System.Windows.Forms.Label labelDate;
		private System.Windows.Forms.Label labelDate2;
		private System.ComponentModel.IContainer components;// Required designer variable.
		private bool mouseIsDown=false;
		///<summary>The point where the mouse was originally down.  In Appt Sheet coordinates</summary>
		private Point mouseOrigin = new Point();
		///<summary>Control origin.  If moving an appointment, this is the location where the appointment was at the beginning of the drag.</summary>
		private Point contOrigin = new Point();
		private ContrApptSingle TempApptSingle;
		private System.Windows.Forms.ImageList imageListMain;
		private bool boolAptMoved=false;
		private OpenDental.UI.Button butToday;
		private System.Windows.Forms.Panel panelSheet;
		private System.Windows.Forms.Panel panelCalendar;
		private System.Windows.Forms.Panel panelArrows;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Panel panelOps;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ListBox listConfirmed;
		private System.Windows.Forms.Button butComplete;
		private System.Windows.Forms.Button butUnsched;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butBreak;
		///<summary>The actual operatoryNum of the clicked op.</summary>
		public static long SheetClickedonOp;
		///<summary></summary>
		public static int SheetClickedonHour;
		///<summary></summary>
		public static int SheetClickedonMin;
		private System.Drawing.Printing.PrintDocument pd2;
		private OpenDental.UI.Button butBack;
		private OpenDental.UI.Button butClearPin;
		private OpenDental.UI.Button butFwd;
		private System.Windows.Forms.Panel panelAptInfo;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.TextBox textLab;
		private System.Windows.Forms.TextBox textProduction;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboView;
		private System.Windows.Forms.ContextMenu menuPatient;	
		///<summary></summary>
	  public FormRpPrintPreview pView;
		private OpenDental.UI.Button butOther;
		private bool cardPrintFamily;
		private System.Windows.Forms.ContextMenu menuApt;
		private System.Windows.Forms.ContextMenu menuBlockout;
		private System.Windows.Forms.ContextMenu menuWeeklyApt;
		private List<Schedule> SchedListPeriod;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private OpenDental.UI.Button butSearch;
		private System.Windows.Forms.GroupBox groupSearch;
		private OpenDental.UI.Button butSearchNext;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBefore;
		private System.Windows.Forms.RadioButton radioBeforeAM;
		private System.Windows.Forms.RadioButton radioBeforePM;
		private System.Windows.Forms.RadioButton radioAfterPM;
		private System.Windows.Forms.RadioButton radioAfterAM;
		private System.Windows.Forms.TextBox textAfter;
		private System.Windows.Forms.Label label11;
		private OpenDental.UI.Button butSearchClose;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button butSearchCloseX;
		private System.Windows.Forms.ListBox listProviders;
		private System.Windows.Forms.ListBox listSearchResults;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DateTimePicker dateSearch;
		private DateTime[] SearchResults;
		private OpenDental.UI.Button butRefresh;
		private bool ResizingAppt;
		private int ResizingOrigH;
		//private bool isWeeklyView;
		public static DateTime WeekStartDate;
		public static DateTime WeekEndDate;
		private OpenDental.UI.Button butLab;
		///<summary>The index of the day as shown on the screen.  Only used in weekly view.</summary>
		public static int SheetClickedonDay;
		///<summary></summary>
		private Panel infoBubble;
		///<summary>The dataset that holds all the data (well, not quite all of it yet)</summary>
		private DataSet DS;
		///<summary>If the user has done a blockout/copy, then this will contain the blockout that is on the "clipboard".</summary>
		private Schedule BlockoutClipboard;
		///<summary>This has to be tracked globally because mouse might move directly from one appt to another without any break.  This is the only way to know if we are still over the same appt.</summary>
		private long bubbleAptNum;
		private DateTime bubbleTime;
		private Point bubbleLocation;
		private ODGrid gridEmpSched;
		private OpenDental.UI.PictureBox PicturePat;
		//private string PatCurName;
		//private int PatCurNum;
		private Timer timerInfoBubble;
		//private string PatCurChartNumber;
		private TabControl tabControl;
		private TabPage tabWaiting;
		private TabPage tabSched;
		private ODGrid gridWaiting;
		private OpenDental.UI.Button butMonth;
		//<summary></summary>
		//public static Size PinboardSize=new Size(106,92);
		private PinBoard pinBoard;
		//private ContrApptSingle PinApptSingle;
		///<summary>Local computer time.  Used by waiting room feature as delta time for display refresh.</summary>
		private DateTime LastTimeDataRetrieved;
		private Timer timerWaitingRoom;
		private OpenDental.UI.Button butBackMonth;
		private OpenDental.UI.Button butFwdMonth;
		private OpenDental.UI.Button butBackWeek;
		private OpenDental.UI.Button butFwdWeek;
		private RadioButton radioDay;
		private RadioButton radioWeek;
		private bool InitializedOnStartup;
		private Patient PatCur;
		private FormRecallList FormRecallL;

		///<summary></summary>
		public ContrAppt(){
			Logger.openlog.Log("Initializing appointment module...",Logger.Severity.INFO);
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			menuWeeklyApt=new System.Windows.Forms.ContextMenu();
			infoBubble=new Panel();
			infoBubble.Visible=false;
			infoBubble.Size=new Size(200,300);
			infoBubble.MouseMove+=new MouseEventHandler(InfoBubble_MouseMove);
			infoBubble.BackColor=Color.FromArgb(255,250,190);
			PicturePat=new OpenDental.UI.PictureBox();
			PicturePat.Location=new Point(6,17);
			PicturePat.Size=new Size(100,100);
			PicturePat.BackColor=Color.FromArgb(232,220,190);
			PicturePat.TextNullImage=Lan.g(this,"Patient Picture Unavailable");
			PicturePat.MouseMove+=new MouseEventHandler(PicturePat_MouseMove);
			infoBubble.Controls.Clear();
			infoBubble.Controls.Add(PicturePat);
			this.Controls.Add(infoBubble);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrAppt));
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.Calendar2 = new System.Windows.Forms.MonthCalendar();
			this.labelDate = new System.Windows.Forms.Label();
			this.labelDate2 = new System.Windows.Forms.Label();
			this.panelArrows = new System.Windows.Forms.Panel();
			this.butBackMonth = new OpenDental.UI.Button();
			this.butFwdMonth = new OpenDental.UI.Button();
			this.butBackWeek = new OpenDental.UI.Button();
			this.butFwdWeek = new OpenDental.UI.Button();
			this.butToday = new OpenDental.UI.Button();
			this.butBack = new OpenDental.UI.Button();
			this.butFwd = new OpenDental.UI.Button();
			this.panelSheet = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.ContrApptSheet2 = new OpenDental.ContrApptSheet();
			this.panelAptInfo = new System.Windows.Forms.Panel();
			this.listConfirmed = new System.Windows.Forms.ListBox();
			this.butComplete = new System.Windows.Forms.Button();
			this.butUnsched = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butBreak = new System.Windows.Forms.Button();
			this.panelCalendar = new System.Windows.Forms.Panel();
			this.radioWeek = new System.Windows.Forms.RadioButton();
			this.radioDay = new System.Windows.Forms.RadioButton();
			this.butMonth = new OpenDental.UI.Button();
			this.pinBoard = new OpenDental.UI.PinBoard();
			this.butLab = new OpenDental.UI.Button();
			this.butSearch = new OpenDental.UI.Button();
			this.textProduction = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textLab = new System.Windows.Forms.TextBox();
			this.comboView = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butClearPin = new OpenDental.UI.Button();
			this.panelOps = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.menuApt = new System.Windows.Forms.ContextMenu();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.menuBlockout = new System.Windows.Forms.ContextMenu();
			this.groupSearch = new System.Windows.Forms.GroupBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.listSearchResults = new System.Windows.Forms.ListBox();
			this.listProviders = new System.Windows.Forms.ListBox();
			this.butSearchClose = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textAfter = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.radioBeforePM = new System.Windows.Forms.RadioButton();
			this.radioBeforeAM = new System.Windows.Forms.RadioButton();
			this.textBefore = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioAfterAM = new System.Windows.Forms.RadioButton();
			this.radioAfterPM = new System.Windows.Forms.RadioButton();
			this.dateSearch = new System.Windows.Forms.DateTimePicker();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.butSearchCloseX = new System.Windows.Forms.Button();
			this.butSearchNext = new OpenDental.UI.Button();
			this.timerInfoBubble = new System.Windows.Forms.Timer(this.components);
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabWaiting = new System.Windows.Forms.TabPage();
			this.gridWaiting = new OpenDental.UI.ODGrid();
			this.tabSched = new System.Windows.Forms.TabPage();
			this.gridEmpSched = new OpenDental.UI.ODGrid();
			this.timerWaitingRoom = new System.Windows.Forms.Timer(this.components);
			this.butOther = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.panelArrows.SuspendLayout();
			this.panelSheet.SuspendLayout();
			this.panelAptInfo.SuspendLayout();
			this.panelCalendar.SuspendLayout();
			this.groupSearch.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabWaiting.SuspendLayout();
			this.tabSched.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Pat.gif");
			this.imageListMain.Images.SetKeyName(1,"print.gif");
			this.imageListMain.Images.SetKeyName(2,"apptLists.gif");
			// 
			// Calendar2
			// 
			this.Calendar2.Location = new System.Drawing.Point(0,24);
			this.Calendar2.Name = "Calendar2";
			this.Calendar2.ScrollChange = 1;
			this.Calendar2.TabIndex = 23;
			this.Calendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar2_DateSelected);
			// 
			// labelDate
			// 
			this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif",11.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDate.Location = new System.Drawing.Point(2,4);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(56,16);
			this.labelDate.TabIndex = 24;
			this.labelDate.Text = "Wed";
			// 
			// labelDate2
			// 
			this.labelDate2.Font = new System.Drawing.Font("Microsoft Sans Serif",11.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDate2.Location = new System.Drawing.Point(46,4);
			this.labelDate2.Name = "labelDate2";
			this.labelDate2.Size = new System.Drawing.Size(100,20);
			this.labelDate2.TabIndex = 25;
			this.labelDate2.Text = "-  Oct 20";
			// 
			// panelArrows
			// 
			this.panelArrows.Controls.Add(this.butBackMonth);
			this.panelArrows.Controls.Add(this.butFwdMonth);
			this.panelArrows.Controls.Add(this.butBackWeek);
			this.panelArrows.Controls.Add(this.butFwdWeek);
			this.panelArrows.Controls.Add(this.butToday);
			this.panelArrows.Controls.Add(this.butBack);
			this.panelArrows.Controls.Add(this.butFwd);
			this.panelArrows.Location = new System.Drawing.Point(1,189);
			this.panelArrows.Name = "panelArrows";
			this.panelArrows.Size = new System.Drawing.Size(217,24);
			this.panelArrows.TabIndex = 32;
			// 
			// butBackMonth
			// 
			this.butBackMonth.AdjustImageLocation = new System.Drawing.Point(-3,-1);
			this.butBackMonth.Autosize = true;
			this.butBackMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackMonth.CornerRadius = 4F;
			this.butBackMonth.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butBackMonth.Image = ((System.Drawing.Image)(resources.GetObject("butBackMonth.Image")));
			this.butBackMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBackMonth.Location = new System.Drawing.Point(1,0);
			this.butBackMonth.Name = "butBackMonth";
			this.butBackMonth.Size = new System.Drawing.Size(32,22);
			this.butBackMonth.TabIndex = 57;
			this.butBackMonth.Text = "M";
			this.butBackMonth.Click += new System.EventHandler(this.butBackMonth_Click);
			// 
			// butFwdMonth
			// 
			this.butFwdMonth.AdjustImageLocation = new System.Drawing.Point(5,-1);
			this.butFwdMonth.Autosize = false;
			this.butFwdMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwdMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwdMonth.CornerRadius = 4F;
			this.butFwdMonth.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butFwdMonth.Image = ((System.Drawing.Image)(resources.GetObject("butFwdMonth.Image")));
			this.butFwdMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butFwdMonth.Location = new System.Drawing.Point(188,0);
			this.butFwdMonth.Name = "butFwdMonth";
			this.butFwdMonth.Size = new System.Drawing.Size(29,22);
			this.butFwdMonth.TabIndex = 56;
			this.butFwdMonth.Text = "M";
			this.butFwdMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butFwdMonth.Click += new System.EventHandler(this.butFwdMonth_Click);
			// 
			// butBackWeek
			// 
			this.butBackWeek.AdjustImageLocation = new System.Drawing.Point(-3,-1);
			this.butBackWeek.Autosize = true;
			this.butBackWeek.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackWeek.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackWeek.CornerRadius = 4F;
			this.butBackWeek.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butBackWeek.Image = ((System.Drawing.Image)(resources.GetObject("butBackWeek.Image")));
			this.butBackWeek.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBackWeek.Location = new System.Drawing.Point(33,0);
			this.butBackWeek.Name = "butBackWeek";
			this.butBackWeek.Size = new System.Drawing.Size(33,22);
			this.butBackWeek.TabIndex = 55;
			this.butBackWeek.Text = "W";
			this.butBackWeek.Click += new System.EventHandler(this.butBackWeek_Click);
			// 
			// butFwdWeek
			// 
			this.butFwdWeek.AdjustImageLocation = new System.Drawing.Point(5,-1);
			this.butFwdWeek.Autosize = false;
			this.butFwdWeek.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwdWeek.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwdWeek.CornerRadius = 4F;
			this.butFwdWeek.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butFwdWeek.Image = ((System.Drawing.Image)(resources.GetObject("butFwdWeek.Image")));
			this.butFwdWeek.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butFwdWeek.Location = new System.Drawing.Point(158,0);
			this.butFwdWeek.Name = "butFwdWeek";
			this.butFwdWeek.Size = new System.Drawing.Size(30,22);
			this.butFwdWeek.TabIndex = 54;
			this.butFwdWeek.Text = "W";
			this.butFwdWeek.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butFwdWeek.Click += new System.EventHandler(this.butFwdWeek_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = false;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butToday.Location = new System.Drawing.Point(85,0);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(54,22);
			this.butToday.TabIndex = 29;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBack.Autosize = true;
			this.butBack.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBack.CornerRadius = 4F;
			this.butBack.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butBack.Image = ((System.Drawing.Image)(resources.GetObject("butBack.Image")));
			this.butBack.Location = new System.Drawing.Point(66,0);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(19,22);
			this.butBack.TabIndex = 51;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(5,-1);
			this.butFwd.Autosize = false;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.CornerRadius = 4F;
			this.butFwd.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butFwd.Image = ((System.Drawing.Image)(resources.GetObject("butFwd.Image")));
			this.butFwd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butFwd.Location = new System.Drawing.Point(139,0);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(19,22);
			this.butFwd.TabIndex = 53;
			this.butFwd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// panelSheet
			// 
			this.panelSheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSheet.Controls.Add(this.vScrollBar1);
			this.panelSheet.Controls.Add(this.ContrApptSheet2);
			this.panelSheet.Location = new System.Drawing.Point(0,17);
			this.panelSheet.Name = "panelSheet";
			this.panelSheet.Size = new System.Drawing.Size(235,726);
			this.panelSheet.TabIndex = 44;
			this.panelSheet.Resize += new System.EventHandler(this.panelSheet_Resize);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.Location = new System.Drawing.Point(216,0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17,724);
			this.vScrollBar1.TabIndex = 23;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// ContrApptSheet2
			// 
			this.ContrApptSheet2.Location = new System.Drawing.Point(2,-550);
			this.ContrApptSheet2.Name = "ContrApptSheet2";
			this.ContrApptSheet2.Size = new System.Drawing.Size(60,1728);
			this.ContrApptSheet2.TabIndex = 22;
			this.ContrApptSheet2.DoubleClick += new System.EventHandler(this.ContrApptSheet2_DoubleClick);
			this.ContrApptSheet2.MouseLeave += new System.EventHandler(this.ContrApptSheet2_MouseLeave);
			this.ContrApptSheet2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseMove);
			this.ContrApptSheet2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseDown);
			this.ContrApptSheet2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseUp);
			// 
			// panelAptInfo
			// 
			this.panelAptInfo.Controls.Add(this.listConfirmed);
			this.panelAptInfo.Controls.Add(this.butComplete);
			this.panelAptInfo.Controls.Add(this.butUnsched);
			this.panelAptInfo.Controls.Add(this.butDelete);
			this.panelAptInfo.Controls.Add(this.butBreak);
			this.panelAptInfo.Location = new System.Drawing.Point(665,404);
			this.panelAptInfo.Name = "panelAptInfo";
			this.panelAptInfo.Size = new System.Drawing.Size(219,116);
			this.panelAptInfo.TabIndex = 45;
			// 
			// listConfirmed
			// 
			this.listConfirmed.IntegralHeight = false;
			this.listConfirmed.Location = new System.Drawing.Point(145,2);
			this.listConfirmed.Name = "listConfirmed";
			this.listConfirmed.Size = new System.Drawing.Size(73,111);
			this.listConfirmed.TabIndex = 75;
			this.listConfirmed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listConfirmed_MouseDown);
			// 
			// butComplete
			// 
			this.butComplete.BackColor = System.Drawing.SystemColors.Control;
			this.butComplete.Image = ((System.Drawing.Image)(resources.GetObject("butComplete.Image")));
			this.butComplete.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butComplete.Location = new System.Drawing.Point(2,57);
			this.butComplete.Name = "butComplete";
			this.butComplete.Size = new System.Drawing.Size(28,28);
			this.butComplete.TabIndex = 69;
			this.butComplete.TabStop = false;
			this.butComplete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butComplete.UseVisualStyleBackColor = false;
			this.butComplete.Click += new System.EventHandler(this.butComplete_Click);
			// 
			// butUnsched
			// 
			this.butUnsched.BackColor = System.Drawing.SystemColors.Control;
			this.butUnsched.Image = ((System.Drawing.Image)(resources.GetObject("butUnsched.Image")));
			this.butUnsched.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butUnsched.Location = new System.Drawing.Point(2,1);
			this.butUnsched.Name = "butUnsched";
			this.butUnsched.Size = new System.Drawing.Size(28,28);
			this.butUnsched.TabIndex = 68;
			this.butUnsched.TabStop = false;
			this.butUnsched.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUnsched.UseVisualStyleBackColor = false;
			this.butUnsched.Click += new System.EventHandler(this.butUnsched_Click);
			// 
			// butDelete
			// 
			this.butDelete.BackColor = System.Drawing.SystemColors.Control;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butDelete.Location = new System.Drawing.Point(2,85);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(28,28);
			this.butDelete.TabIndex = 66;
			this.butDelete.TabStop = false;
			this.butDelete.UseVisualStyleBackColor = false;
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butBreak
			// 
			this.butBreak.BackColor = System.Drawing.SystemColors.Control;
			this.butBreak.Image = ((System.Drawing.Image)(resources.GetObject("butBreak.Image")));
			this.butBreak.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.butBreak.Location = new System.Drawing.Point(2,29);
			this.butBreak.Name = "butBreak";
			this.butBreak.Size = new System.Drawing.Size(28,28);
			this.butBreak.TabIndex = 65;
			this.butBreak.TabStop = false;
			this.butBreak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBreak.UseVisualStyleBackColor = false;
			this.butBreak.Click += new System.EventHandler(this.butBreak_Click);
			// 
			// panelCalendar
			// 
			this.panelCalendar.Controls.Add(this.radioWeek);
			this.panelCalendar.Controls.Add(this.panelArrows);
			this.panelCalendar.Controls.Add(this.radioDay);
			this.panelCalendar.Controls.Add(this.butMonth);
			this.panelCalendar.Controls.Add(this.pinBoard);
			this.panelCalendar.Controls.Add(this.butLab);
			this.panelCalendar.Controls.Add(this.butSearch);
			this.panelCalendar.Controls.Add(this.textProduction);
			this.panelCalendar.Controls.Add(this.label7);
			this.panelCalendar.Controls.Add(this.textLab);
			this.panelCalendar.Controls.Add(this.comboView);
			this.panelCalendar.Controls.Add(this.label2);
			this.panelCalendar.Controls.Add(this.butClearPin);
			this.panelCalendar.Controls.Add(this.Calendar2);
			this.panelCalendar.Controls.Add(this.labelDate);
			this.panelCalendar.Controls.Add(this.labelDate2);
			this.panelCalendar.Location = new System.Drawing.Point(665,28);
			this.panelCalendar.Name = "panelCalendar";
			this.panelCalendar.Size = new System.Drawing.Size(219,375);
			this.panelCalendar.TabIndex = 46;
			// 
			// radioWeek
			// 
			this.radioWeek.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioWeek.Location = new System.Drawing.Point(43,238);
			this.radioWeek.Name = "radioWeek";
			this.radioWeek.Size = new System.Drawing.Size(68,16);
			this.radioWeek.TabIndex = 92;
			this.radioWeek.Text = "Week";
			this.radioWeek.Click += new System.EventHandler(this.radioWeek_Click);
			// 
			// radioDay
			// 
			this.radioDay.Checked = true;
			this.radioDay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDay.Location = new System.Drawing.Point(43,218);
			this.radioDay.Name = "radioDay";
			this.radioDay.Size = new System.Drawing.Size(68,16);
			this.radioDay.TabIndex = 91;
			this.radioDay.TabStop = true;
			this.radioDay.Text = "Day";
			this.radioDay.Click += new System.EventHandler(this.radioDay_Click);
			// 
			// butMonth
			// 
			this.butMonth.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMonth.Autosize = false;
			this.butMonth.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMonth.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMonth.CornerRadius = 4F;
			this.butMonth.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butMonth.Location = new System.Drawing.Point(152,1);
			this.butMonth.Name = "butMonth";
			this.butMonth.Size = new System.Drawing.Size(65,22);
			this.butMonth.TabIndex = 79;
			this.butMonth.Text = "Month";
			this.butMonth.Visible = false;
			this.butMonth.Click += new System.EventHandler(this.butMonth_Click);
			// 
			// pinBoard
			// 
			this.pinBoard.Location = new System.Drawing.Point(119,213);
			this.pinBoard.Name = "pinBoard";
			this.pinBoard.SelectedIndex = -1;
			this.pinBoard.Size = new System.Drawing.Size(99,96);
			this.pinBoard.TabIndex = 78;
			this.pinBoard.Text = "pinBoard";
			this.pinBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pinBoard_MouseMove);
			this.pinBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pinBoard_MouseDown);
			this.pinBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pinBoard_MouseUp);
			this.pinBoard.SelectedIndexChanged += new System.EventHandler(this.pinBoard_SelectedIndexChanged);
			// 
			// butLab
			// 
			this.butLab.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLab.Autosize = true;
			this.butLab.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLab.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLab.CornerRadius = 4F;
			this.butLab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLab.Location = new System.Drawing.Point(3,333);
			this.butLab.Name = "butLab";
			this.butLab.Size = new System.Drawing.Size(79,21);
			this.butLab.TabIndex = 77;
			this.butLab.Text = "LabCases";
			this.butLab.Click += new System.EventHandler(this.butLab_Click);
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSearch.Location = new System.Drawing.Point(43,285);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75,24);
			this.butSearch.TabIndex = 40;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// textProduction
			// 
			this.textProduction.BackColor = System.Drawing.Color.White;
			this.textProduction.Location = new System.Drawing.Point(85,353);
			this.textProduction.Name = "textProduction";
			this.textProduction.ReadOnly = true;
			this.textProduction.Size = new System.Drawing.Size(133,20);
			this.textProduction.TabIndex = 38;
			this.textProduction.Text = "$100";
			this.textProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16,357);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(68,15);
			this.label7.TabIndex = 39;
			this.label7.Text = "Daily Prod";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLab
			// 
			this.textLab.BackColor = System.Drawing.Color.White;
			this.textLab.Location = new System.Drawing.Point(85,333);
			this.textLab.Name = "textLab";
			this.textLab.ReadOnly = true;
			this.textLab.Size = new System.Drawing.Size(133,20);
			this.textLab.TabIndex = 36;
			this.textLab.Text = "All Received";
			this.textLab.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// comboView
			// 
			this.comboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboView.Location = new System.Drawing.Point(85,312);
			this.comboView.MaxDropDownItems = 30;
			this.comboView.Name = "comboView";
			this.comboView.Size = new System.Drawing.Size(133,21);
			this.comboView.TabIndex = 35;
			this.comboView.SelectedIndexChanged += new System.EventHandler(this.comboView_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(17,314);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66,16);
			this.label2.TabIndex = 34;
			this.label2.Text = "View";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butClearPin
			// 
			this.butClearPin.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClearPin.Autosize = true;
			this.butClearPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearPin.CornerRadius = 4F;
			this.butClearPin.Image = ((System.Drawing.Image)(resources.GetObject("butClearPin.Image")));
			this.butClearPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClearPin.Location = new System.Drawing.Point(43,260);
			this.butClearPin.Name = "butClearPin";
			this.butClearPin.Size = new System.Drawing.Size(75,24);
			this.butClearPin.TabIndex = 33;
			this.butClearPin.Text = "Clear";
			this.butClearPin.Click += new System.EventHandler(this.butClearPin_Click);
			// 
			// panelOps
			// 
			this.panelOps.Location = new System.Drawing.Point(0,0);
			this.panelOps.Name = "panelOps";
			this.panelOps.Size = new System.Drawing.Size(676,17);
			this.panelOps.TabIndex = 48;
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 100;
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// groupSearch
			// 
			this.groupSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))),((int)(((byte)(255)))),((int)(((byte)(192)))));
			this.groupSearch.Controls.Add(this.butRefresh);
			this.groupSearch.Controls.Add(this.listSearchResults);
			this.groupSearch.Controls.Add(this.listProviders);
			this.groupSearch.Controls.Add(this.butSearchClose);
			this.groupSearch.Controls.Add(this.groupBox2);
			this.groupSearch.Controls.Add(this.label8);
			this.groupSearch.Controls.Add(this.butSearchCloseX);
			this.groupSearch.Controls.Add(this.butSearchNext);
			this.groupSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSearch.Location = new System.Drawing.Point(380,340);
			this.groupSearch.Name = "groupSearch";
			this.groupSearch.Size = new System.Drawing.Size(219,366);
			this.groupSearch.TabIndex = 74;
			this.groupSearch.TabStop = false;
			this.groupSearch.Text = "Search For Opening";
			this.groupSearch.Visible = false;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butRefresh.Location = new System.Drawing.Point(6,343);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(62,22);
			this.butRefresh.TabIndex = 88;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// listSearchResults
			// 
			this.listSearchResults.IntegralHeight = false;
			this.listSearchResults.Location = new System.Drawing.Point(6,32);
			this.listSearchResults.Name = "listSearchResults";
			this.listSearchResults.ScrollAlwaysVisible = true;
			this.listSearchResults.Size = new System.Drawing.Size(193,134);
			this.listSearchResults.TabIndex = 87;
			this.listSearchResults.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSearchResults_MouseDown);
			// 
			// listProviders
			// 
			this.listProviders.Location = new System.Drawing.Point(6,269);
			this.listProviders.MultiColumn = true;
			this.listProviders.Name = "listProviders";
			this.listProviders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProviders.Size = new System.Drawing.Size(193,69);
			this.listProviders.TabIndex = 86;
			// 
			// butSearchClose
			// 
			this.butSearchClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearchClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSearchClose.Autosize = true;
			this.butSearchClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearchClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearchClose.CornerRadius = 4F;
			this.butSearchClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSearchClose.Location = new System.Drawing.Point(161,343);
			this.butSearchClose.Name = "butSearchClose";
			this.butSearchClose.Size = new System.Drawing.Size(54,22);
			this.butSearchClose.TabIndex = 85;
			this.butSearchClose.Text = "Close";
			this.butSearchClose.Click += new System.EventHandler(this.butSearchClose_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textAfter);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.radioBeforePM);
			this.groupBox2.Controls.Add(this.radioBeforeAM);
			this.groupBox2.Controls.Add(this.textBefore);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Controls.Add(this.dateSearch);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(6,168);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(193,84);
			this.groupBox2.TabIndex = 84;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Date/Time Restrictions";
			// 
			// textAfter
			// 
			this.textAfter.Location = new System.Drawing.Point(57,60);
			this.textAfter.Name = "textAfter";
			this.textAfter.Size = new System.Drawing.Size(44,20);
			this.textAfter.TabIndex = 88;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(1,62);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(53,16);
			this.label11.TabIndex = 87;
			this.label11.Text = "After";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// radioBeforePM
			// 
			this.radioBeforePM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioBeforePM.Location = new System.Drawing.Point(151,41);
			this.radioBeforePM.Name = "radioBeforePM";
			this.radioBeforePM.Size = new System.Drawing.Size(37,15);
			this.radioBeforePM.TabIndex = 86;
			this.radioBeforePM.Text = "pm";
			// 
			// radioBeforeAM
			// 
			this.radioBeforeAM.Checked = true;
			this.radioBeforeAM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioBeforeAM.Location = new System.Drawing.Point(108,41);
			this.radioBeforeAM.Name = "radioBeforeAM";
			this.radioBeforeAM.Size = new System.Drawing.Size(37,15);
			this.radioBeforeAM.TabIndex = 85;
			this.radioBeforeAM.TabStop = true;
			this.radioBeforeAM.Text = "am";
			// 
			// textBefore
			// 
			this.textBefore.Location = new System.Drawing.Point(57,38);
			this.textBefore.Name = "textBefore";
			this.textBefore.Size = new System.Drawing.Size(44,20);
			this.textBefore.TabIndex = 84;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(1,40);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(53,16);
			this.label10.TabIndex = 83;
			this.label10.Text = "Before";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioAfterAM);
			this.panel1.Controls.Add(this.radioAfterPM);
			this.panel1.Location = new System.Drawing.Point(105,60);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(84,20);
			this.panel1.TabIndex = 86;
			// 
			// radioAfterAM
			// 
			this.radioAfterAM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAfterAM.Location = new System.Drawing.Point(3,2);
			this.radioAfterAM.Name = "radioAfterAM";
			this.radioAfterAM.Size = new System.Drawing.Size(37,15);
			this.radioAfterAM.TabIndex = 89;
			this.radioAfterAM.Text = "am";
			// 
			// radioAfterPM
			// 
			this.radioAfterPM.Checked = true;
			this.radioAfterPM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAfterPM.Location = new System.Drawing.Point(46,2);
			this.radioAfterPM.Name = "radioAfterPM";
			this.radioAfterPM.Size = new System.Drawing.Size(36,15);
			this.radioAfterPM.TabIndex = 90;
			this.radioAfterPM.TabStop = true;
			this.radioAfterPM.Text = "pm";
			// 
			// dateSearch
			// 
			this.dateSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateSearch.Location = new System.Drawing.Point(57,16);
			this.dateSearch.Name = "dateSearch";
			this.dateSearch.Size = new System.Drawing.Size(130,20);
			this.dateSearch.TabIndex = 90;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(1,19);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(54,16);
			this.label9.TabIndex = 89;
			this.label9.Text = "After";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6,251);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(92,16);
			this.label8.TabIndex = 80;
			this.label8.Text = "Providers";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSearchCloseX
			// 
			this.butSearchCloseX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butSearchCloseX.ForeColor = System.Drawing.SystemColors.Control;
			this.butSearchCloseX.Image = ((System.Drawing.Image)(resources.GetObject("butSearchCloseX.Image")));
			this.butSearchCloseX.Location = new System.Drawing.Point(185,7);
			this.butSearchCloseX.Name = "butSearchCloseX";
			this.butSearchCloseX.Size = new System.Drawing.Size(16,16);
			this.butSearchCloseX.TabIndex = 0;
			this.butSearchCloseX.Click += new System.EventHandler(this.butSearchCloseX_Click);
			// 
			// butSearchNext
			// 
			this.butSearchNext.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearchNext.Autosize = true;
			this.butSearchNext.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearchNext.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearchNext.CornerRadius = 4F;
			this.butSearchNext.Image = ((System.Drawing.Image)(resources.GetObject("butSearchNext.Image")));
			this.butSearchNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butSearchNext.Location = new System.Drawing.Point(111,9);
			this.butSearchNext.Name = "butSearchNext";
			this.butSearchNext.Size = new System.Drawing.Size(71,22);
			this.butSearchNext.TabIndex = 77;
			this.butSearchNext.Text = "More";
			this.butSearchNext.Click += new System.EventHandler(this.butSearchMore_Click);
			// 
			// timerInfoBubble
			// 
			this.timerInfoBubble.Interval = 300;
			this.timerInfoBubble.Tick += new System.EventHandler(this.timerInfoBubble_Tick);
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tabControl.Controls.Add(this.tabWaiting);
			this.tabControl.Controls.Add(this.tabSched);
			this.tabControl.Location = new System.Drawing.Point(665,521);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(219,187);
			this.tabControl.TabIndex = 78;
			// 
			// tabWaiting
			// 
			this.tabWaiting.Controls.Add(this.gridWaiting);
			this.tabWaiting.Location = new System.Drawing.Point(4,22);
			this.tabWaiting.Name = "tabWaiting";
			this.tabWaiting.Padding = new System.Windows.Forms.Padding(3);
			this.tabWaiting.Size = new System.Drawing.Size(211,161);
			this.tabWaiting.TabIndex = 0;
			this.tabWaiting.Text = "Waiting";
			this.tabWaiting.UseVisualStyleBackColor = true;
			// 
			// gridWaiting
			// 
			this.gridWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridWaiting.HScrollVisible = false;
			this.gridWaiting.Location = new System.Drawing.Point(0,0);
			this.gridWaiting.Margin = new System.Windows.Forms.Padding(0);
			this.gridWaiting.Name = "gridWaiting";
			this.gridWaiting.ScrollValue = 0;
			this.gridWaiting.Size = new System.Drawing.Size(211,161);
			this.gridWaiting.TabIndex = 78;
			this.gridWaiting.Title = "Waiting Room";
			this.gridWaiting.TranslationName = "TableApptEmpSched";
			// 
			// tabSched
			// 
			this.tabSched.Controls.Add(this.gridEmpSched);
			this.tabSched.Location = new System.Drawing.Point(4,22);
			this.tabSched.Name = "tabSched";
			this.tabSched.Padding = new System.Windows.Forms.Padding(3);
			this.tabSched.Size = new System.Drawing.Size(211,161);
			this.tabSched.TabIndex = 1;
			this.tabSched.Text = "Emp";
			this.tabSched.UseVisualStyleBackColor = true;
			// 
			// gridEmpSched
			// 
			this.gridEmpSched.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridEmpSched.HScrollVisible = false;
			this.gridEmpSched.Location = new System.Drawing.Point(0,0);
			this.gridEmpSched.Margin = new System.Windows.Forms.Padding(0);
			this.gridEmpSched.Name = "gridEmpSched";
			this.gridEmpSched.ScrollValue = 0;
			this.gridEmpSched.Size = new System.Drawing.Size(211,161);
			this.gridEmpSched.TabIndex = 77;
			this.gridEmpSched.Title = "Employee Schedules";
			this.gridEmpSched.TranslationName = "TableApptEmpSched";
			this.gridEmpSched.DoubleClick += new System.EventHandler(this.gridEmpSched_DoubleClick);
			this.gridEmpSched.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridEmpSched_CellDoubleClick);
			// 
			// timerWaitingRoom
			// 
			this.timerWaitingRoom.Enabled = true;
			this.timerWaitingRoom.Interval = 1000;
			this.timerWaitingRoom.Tick += new System.EventHandler(this.timerWaitingRoom_Tick);
			// 
			// butOther
			// 
			this.butOther.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOther.Autosize = true;
			this.butOther.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOther.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOther.CornerRadius = 4F;
			this.butOther.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butOther.Location = new System.Drawing.Point(706,492);
			this.butOther.Name = "butOther";
			this.butOther.Size = new System.Drawing.Size(92,24);
			this.butOther.TabIndex = 76;
			this.butOther.TabStop = false;
			this.butOther.Text = "Make/Find Appt";
			this.butOther.Click += new System.EventHandler(this.butOther_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(680,2);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(203,25);
			this.ToolBarMain.TabIndex = 73;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ContrAppt
			// 
			this.Controls.Add(this.groupSearch);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.butOther);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.panelOps);
			this.Controls.Add(this.panelCalendar);
			this.Controls.Add(this.panelAptInfo);
			this.Controls.Add(this.panelSheet);
			this.Name = "ContrAppt";
			this.Size = new System.Drawing.Size(939,708);
			this.Load += new System.EventHandler(this.ContrAppt_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAppt_Layout);
			this.Resize += new System.EventHandler(this.ContrAppt_Resize);
			this.panelArrows.ResumeLayout(false);
			this.panelSheet.ResumeLayout(false);
			this.panelAptInfo.ResumeLayout(false);
			this.panelCalendar.ResumeLayout(false);
			this.panelCalendar.PerformLayout();
			this.groupSearch.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabWaiting.ResumeLayout(false);
			this.tabSched.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void ContrApptSheet2_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e){
			int max=vScrollBar1.Maximum-vScrollBar1.LargeChange;//panelTable.Height-panelScroll.Height+3;
			int newScrollVal=vScrollBar1.Value-(int)(e.Delta/4);
			if(newScrollVal > max){
				vScrollBar1.Value=max;
			}
			else if(newScrollVal < vScrollBar1.Minimum){
				vScrollBar1.Value=vScrollBar1.Minimum;
			}
			else{
				vScrollBar1.Value=newScrollVal;
			}
			ContrApptSheet2.Location=new Point(0,-vScrollBar1.Value);
    }

		///<summary>Overload used when jumping here from another module, and you want to place appointments on the pinboard.</summary>
		public void ModuleSelected(long patNum,List<long> pinAptNums) {
			ModuleSelected(patNum);
			SendToPinBoard(pinAptNums);
		}

		///<summary></summary>
		public void ModuleSelected(long patNum) {
			RefreshModuleDataPatient(patNum);
			RefreshModuleDataPeriod();
			LayoutScrollOpProv();//can only call this as part of ModuleSelected.
			RefreshModuleScreenPatient();
			RefreshModuleScreenPeriod();
		}

		public void RefreshPeriod() {
			RefreshModuleDataPeriod();
			LayoutScrollOpProv();
			RefreshModuleScreenPeriod();
		}

		///<summary>Fills PatCur from the database unless the patnum has not changed.</summary>
		private void RefreshModuleDataPatient(long patNum) {
			if(patNum==0) {
				PatCur=null;
				return;
			}
			if(PatCur !=null && PatCur.PatNum==patNum) {//if patient has not changed
				return;//don't do anything
			}
			PatCur=Patients.GetPat(patNum);
		}

		private void RefreshModuleDataPeriod() {
			bubbleAptNum=0;
			DateTime startDate;
			DateTime endDate;
			if(ContrApptSheet.IsWeeklyView) {
				startDate=WeekStartDate;
				endDate=WeekEndDate;
			}
			else {
				startDate=AppointmentL.DateSelected;
				endDate=AppointmentL.DateSelected;
			}
			if(startDate.Year<1880 || endDate.Year<1880) {
				return;
			}
			//Calendar2.SetSelectionRange(startDate,endDate);
			if(PatCur==null) {
				//there cannot be a selected appointment if no patient is loaded.
				ContrApptSingle.SelectedAptNum=-1;//fixes a minor bug.
			}
			DS=Appointments.RefreshPeriod(startDate,endDate);
			LastTimeDataRetrieved=DateTime.Now;
			SchedListPeriod=Schedules.ConvertTableToList(DS.Tables["Schedule"]);
			ApptViewItemL.GetForCurView(comboView.SelectedIndex-1,ContrApptSheet.IsWeeklyView,SchedListPeriod);
		}

		/// <summary>This also clears listConfirmed.</summary>
		public void LayoutScrollOpProv() {//ModuleSelectedOld(int patNum){
			//the scrollbar logic cannot be moved to someplace where it will be activated while working in apptbook
			//RefreshVisops();//forces reset after changing databases
			if(DefC.Short!=null) {
				ApptViewItemL.GetForCurView(comboView.SelectedIndex-1,ContrApptSheet.IsWeeklyView,SchedListPeriod);//refreshes visops,etc
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
			this.SuspendLayout();
			vScrollBar1.Enabled=true;
			vScrollBar1.Minimum=0;
			vScrollBar1.LargeChange=12*ContrApptSheet.Lh;//12 rows
			vScrollBar1.Maximum=ContrApptSheet2.Height-panelSheet.Height+vScrollBar1.LargeChange;
			if(vScrollBar1.Maximum<0) {
				vScrollBar1.Maximum=0;
			}
			//Max is set again in Resize event
			vScrollBar1.SmallChange=6*ContrApptSheet.Lh;//6 rows
			if(vScrollBar1.Value==0) {//ApptViewC.List seems to already be not null by this point.
				int rowsPerHr=60/ContrApptSheet.MinPerIncr*ContrApptSheet.RowsPerIncr;
				//use the row setting from the first view.
				if(ApptViewC.List.Length>0) {
					rowsPerHr=60/ContrApptSheet.MinPerIncr*ApptViewC.List[0].RowsPerIncr;
				}
				if(8*rowsPerHr*ContrApptSheet.Lh<vScrollBar1.Maximum-vScrollBar1.LargeChange) {
					vScrollBar1.Value=8*rowsPerHr*ContrApptSheet.Lh;//8am
				}
			}
			if(vScrollBar1.Value>vScrollBar1.Maximum-vScrollBar1.LargeChange) {
				vScrollBar1.Value=vScrollBar1.Maximum-vScrollBar1.LargeChange;
			}
			ContrApptSheet2.MouseWheel+=new MouseEventHandler(ContrApptSheet2_MouseWheel);
			ContrApptSheet2.Location=new Point(0,-vScrollBar1.Value);
			panelOps.Controls.Clear();
			for(int i=0;i<ContrApptSheet.ProvCount;i++) {
				Panel panProv=new Panel();
				panProv.BackColor=ProviderC.List[ApptViewItemL.VisProvs[i]].ProvColor;
				panProv.Location=new Point(2+ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*i,0);
				panProv.Width=ContrApptSheet.ProvWidth;
				if(i==0) {//just looks a little nicer:
					panProv.Location=new Point(panProv.Location.X-1,panProv.Location.Y);
					panProv.Width=panProv.Width+1;
				}
				panProv.Height=18;
				panProv.BorderStyle=BorderStyle.Fixed3D;
				panProv.ForeColor=Color.DarkGray;
				panelOps.Controls.Add(panProv);
				toolTip1.SetToolTip(panProv,ProviderC.List[ApptViewItemL.VisProvs[i]].Abbr);
			}
			Operatory curOp;
			if(ContrApptSheet.IsWeeklyView) {
				for(int i=0;i<ContrApptSheet.NumOfWeekDaysToDisplay;i++) {
					Panel panOpName=new Panel();
					Label labOpName=new Label();
					labOpName.Text=WeekStartDate.AddDays(i).ToString("dddd-d");
					panOpName.Location=new Point
						(2+ContrApptSheet.TimeWidth+i*ContrApptSheet.ColDayWidth,0);
					panOpName.Width=ContrApptSheet.ColDayWidth;
					panOpName.Height=18;
					panOpName.BorderStyle=BorderStyle.Fixed3D;
					panOpName.ForeColor=Color.DarkGray;
					panOpName.MouseDown += new System.Windows.Forms.MouseEventHandler(panOpName_MouseDown);
					panOpName.Tag=i;//stores the day index
					//add label within panOpName
					labOpName.Location=new Point(0,-2);
					labOpName.Width=panOpName.Width;
					labOpName.Height=18;
					labOpName.TextAlign=ContentAlignment.MiddleCenter;
					labOpName.ForeColor=Color.Black;
					labOpName.MouseDown += new System.Windows.Forms.MouseEventHandler(panOpName_MouseDown);
					labOpName.Tag=i;//stores the day index
					panOpName.Controls.Add(labOpName);
					panelOps.Controls.Add(panOpName);
				}
			}
			else {
				for(int i=0;i<ContrApptSheet.ColCount;i++) {
					Panel panOpName=new Panel();
					Label labOpName=new Label();
					curOp=OperatoryC.ListShort[ApptViewItemL.VisOps[i]];
					labOpName.Text=curOp.OpName;
					if(curOp.ProvDentist!=0 && !curOp.IsHygiene) {
						panOpName.BackColor=Providers.GetColor(curOp.ProvDentist);
					}
					else if(curOp.ProvHygienist!=0 && curOp.IsHygiene) {
						panOpName.BackColor=Providers.GetColor(curOp.ProvHygienist);
					}
					panOpName.Location=new Point
						(2+ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount+i*ContrApptSheet.ColWidth,0);
					panOpName.Width=ContrApptSheet.ColWidth;
					panOpName.Height=18;
					panOpName.BorderStyle=BorderStyle.Fixed3D;
					panOpName.ForeColor=Color.DarkGray;
					//add label within panOpName
					labOpName.Location=new Point(0,-2);
					labOpName.Width=panOpName.Width;
					labOpName.Height=18;
					labOpName.TextAlign=ContentAlignment.MiddleCenter;
					labOpName.ForeColor=Color.Black;
					panOpName.Controls.Add(labOpName);
					panelOps.Controls.Add(panOpName);
				}
			}
			this.ResumeLayout();
			listConfirmed.Items.Clear();
			for(int i=0;i<DefC.Short[(int)DefCat.ApptConfirmed].Length;i++) {
				this.listConfirmed.Items.Add(DefC.Short[(int)DefCat.ApptConfirmed][i].ItemValue);
			}
		}

		///<summary>Refreshes the appointment info panel to the right if an appointment is currently selected.</summary>
		private void RefreshModuleScreenPatient() {
			if(PatCur==null) {
				butOther.Enabled=false;
			}
			else {
				butOther.Enabled=true;
			}
			if(ContrApptSingle.SelectedAptNum>0) {
				butUnsched.Enabled=true;
				butBreak.Enabled=true;
				butComplete.Enabled=true;
				butDelete.Enabled=true;
			}
			else{
				butUnsched.Enabled=false;
				butBreak.Enabled=false;
				butComplete.Enabled=false;
				butDelete.Enabled=false;
			}
			if(panelAptInfo.Enabled && DS!=null) {
				long aptconfirmed=0;
				string tableAptNum;
				if(pinBoard.SelectedIndex==-1) {//no pinboard appt selected
					for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
						tableAptNum=DS.Tables["Appointments"].Rows[i]["AptNum"].ToString();
						if(tableAptNum==ContrApptSingle.SelectedAptNum.ToString()) {
							aptconfirmed=PIn.PLong(DS.Tables["Appointments"].Rows[i]["Confirmed"].ToString());
							break;
						}
					}
				}
				else {//pinboard appt selected
					aptconfirmed=PIn.PLong(pinBoard.SelectedAppt.DataRoww["Confirmed"].ToString());
				}
				listConfirmed.SelectedIndex=DefC.GetOrder(DefCat.ApptConfirmed,aptconfirmed);//could be -1
			}
			else {
				listConfirmed.SelectedIndex=-1;
			}
		}

		///<summary>Important.  Gets all new day info from db and redraws screen</summary>
		public void RefreshModuleScreenPeriod() {
			DateTime startDate;
			DateTime endDate;
			if(ContrApptSheet.IsWeeklyView) {
				startDate=WeekStartDate;
				endDate=WeekEndDate;
			}
			else {
				startDate=AppointmentL.DateSelected;
				endDate=AppointmentL.DateSelected;
			}
			if(startDate.Year<1880 || endDate.Year<1880) {
				return;
			}
			Calendar2.SetSelectionRange(startDate,endDate);
			ContrApptSingle.ProvBar=new int[ApptViewItemL.VisProvs.Count][];
			for(int i=0;i<ApptViewItemL.VisProvs.Count;i++) {
				ContrApptSingle.ProvBar[i]=new int[24*ContrApptSheet.RowsPerHr]; //[144]; or 24*6
			}
			if(ContrApptSingle3!=null) {//I think this is not needed.
				for(int i=0;i<ContrApptSingle3.Length;i++) {
					if(ContrApptSingle3[i]!=null) {
						ContrApptSingle3[i].Dispose();
					}
					ContrApptSingle3[i]=null;
				}
				ContrApptSingle3=null;
			}
			labelDate.Text=startDate.ToString("ddd");
			labelDate2.Text=startDate.ToString("-  MMM d");
			ContrApptSheet2.Controls.Clear();
			ContrApptSingle3=new ContrApptSingle[DS.Tables["Appointments"].Rows.Count];
			int indexProv;
			DataRow row;
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
				row=DS.Tables["Appointments"].Rows[i];
				ContrApptSingle3[i]=new ContrApptSingle();
				ContrApptSingle3[i].Visible=false;
				if(ContrApptSingle.SelectedAptNum.ToString()==row["AptNum"].ToString()) {//if this is the selected apt
					//if the selected patient was changed from another module, then deselect the apt.
					if(PatCur==null || PatCur.PatNum.ToString()!=row["PatNum"].ToString()) {
						ContrApptSingle.SelectedAptNum=-1;
					}
				}
				ContrApptSingle3[i].DataRoww=row;
				if(!ContrApptSheet.IsWeeklyView) {
					//copy time pattern to provBar[]:
					indexProv=-1;
					if(row["IsHygiene"].ToString()=="1") {
						indexProv=ApptViewItemL.GetIndexProv(PIn.PLong(row["ProvHyg"].ToString()));
					}
					else {
						indexProv=ApptViewItemL.GetIndexProv(PIn.PLong(row["ProvNum"].ToString()));
					}
					if(indexProv!=-1 && row["AptStatus"].ToString()!=((int)ApptStatus.Broken).ToString()) {
						string pattern=ContrApptSingle.GetPatternShowing(row["Pattern"].ToString());
						int startIndex=ContrApptSingle3[i].ConvertToY()/ContrApptSheet.Lh;//rounds down
						for(int k=0;k<pattern.Length;k++) {
							if(pattern.Substring(k,1)=="X") {
								try {
									ContrApptSingle.ProvBar[indexProv][startIndex+k]++;
								}
								catch {
									//appointment must extend past midnight.  Very rare
								}
							}
						}
					}
				}
				ContrApptSingle3[i].SetLocation();
				ContrApptSheet2.Controls.Add(ContrApptSingle3[i]);
			}//end for
			//PinApptSingle.Refresh();
			pinBoard.Invalidate();
			ContrApptSheet2.SchedListPeriod=SchedListPeriod;
			ContrApptSheet2.CreateShadow();
			CreateAptShadows();
			ContrApptSheet2.DrawShadow();
			List<LabCase> labCaseList=LabCases.GetForPeriod(startDate,endDate);
			FillLab(labCaseList);
			FillProduction();
			FillEmpSched();
			FillWaitingRoom();
			LayoutPanels();
		}

		///<summary>Only used when viewing weekly.</summary>
		private void panOpName_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(e.Button!=MouseButtons.Left){
				return;
			}
			int dayI=0;
			if(sender.GetType()==typeof(Panel)){
				dayI=(int)((Panel)sender).Tag;
			}
			else{
				dayI=(int)((Label)sender).Tag;
			}
			AppointmentL.DateSelected=WeekStartDate.AddDays(dayI);
			SetWeeklyView(false);
		}

		///<summary>Sets the ContrApptSingle array to null.</summary>
		public void ModuleUnselected(){
			ContrApptSheet2.Shadow=null;
			if(ContrApptSingle3!=null){//too complex?
				for(int i=0;i<ContrApptSingle3.Length;i++){
					if(ContrApptSingle3[i]!=null){
						ContrApptSingle3[i].Dispose();
						ContrApptSingle3[i]=null;
					}
				}
				ContrApptSingle3=null;
			}
		}
		
		/*
		///<summary>Was RefreshModuleData and FillPatientButton.  Gets the data for the specified patient. Does not refresh any appointment data.  This function should always be called when the patient changes since that's all this function is responsible for.</summary>
		private void RefreshModulePatient(int patNum){//
			if(PatCur.PatNum==patNum) {//if patient has not changed
				return;//don't do anything
			}
			PatCurNum=patNum;//might be zero
			bool hasEmail;
			string chartNumber;
			if(PatCurNum==0){
				PatCurName="";
				PatCurChartNumber="";
				butOther.Enabled=false;
				hasEmail=false;
				chartNumber="";
			}
			else{
				Patient pat=Patients.GetPat(PatCurNum);
				PatCurName=pat.GetNameLF();
				PatCurChartNumber=pat.ChartNumber;
				hasEmail=pat.Email!="";
				chartNumber=pat.ChartNumber;
				//family can wait until user clicks on downarrow.
				PatientL.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCurName,PatCurNum);
				butOther.Enabled=true;
			}
			butUnsched.Enabled=butOther.Enabled;
			butBreak.Enabled=butOther.Enabled;
			butComplete.Enabled=butOther.Enabled;
			butDelete.Enabled=butOther.Enabled;
			//ParentForm.Text=Patients.GetMainTitle(PatCurName,PatCurNum,PatCurChartNumber);
			if(panelAptInfo.Enabled && DS!=null) {
				int aptconfirmed=0;
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					if(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()==ContrApptSingle.ClickedAptNum.ToString()) {
						aptconfirmed=PIn.PInt(DS.Tables["Appointments"].Rows[i]["Confirmed"].ToString());
						break;
					}
				}
				listConfirmed.SelectedIndex=DefC.GetOrder(DefCat.ApptConfirmed,aptconfirmed);//could be -1
			}
			else {
				listConfirmed.SelectedIndex=-1;
			}
//JSPARKS - THIS SHOULD BE MOVED TO BE CALLED EXPLICITLY FROM WITHIN VARIOUS PLACES IN THIS MODULE
//JUST LIKE IN THE OTHER MODULES.  WHEN IT'S HERE, IT IS ALSO GETTING CALLED EVERY TIME MODULESELECTED
//GETS TRIGGERED FROM PARENT FORM.
			OnPatientSelected(PatCurNum,PatCurName,hasEmail,chartNumber);
		}*/

		///<summary>Sends the PatientSelected event on up to the main form.  The only result is that the main window now knows the new patNum and patName.  Does nothing else.  Does not trigger any other methods to run which might cause a loop.  Only called from RefreshModulePatient, but it's separate so that it's the same as in the other modules.</summary>
		private void OnPatientSelected(long patNum,string patName,bool hasEmail,string chartNumber) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum,patName,hasEmail,chartNumber);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		///<summary>Currently only used when comboView really does change.  Otherwise, just call ModuleSelected.  Triggered in FunctionKeyPress, SetView, and  FillViews</summary>
		private void comboView_SelectedIndexChanged(object sender,System.EventArgs e) {
			if(PatCur==null) {
				ModuleSelected(0);
			}
			else {
				ModuleSelected(PatCur.PatNum);
			}
		}

		///<summary>Activated anytime a Patient menu item is clicked.</summary>
		private void menuPatient_Click(object sender,System.EventArgs e) {
			long newPatNum=PatientL.ButtonSelect(menuPatient,sender,null);
			ModuleSelected(newPatNum);
		}

		///<summary>Not used</summary>
		private void ContrAppt_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//This event actually happens quite frequently: once for every appointment placed on the screen.
			//Moved it all to Resize event.
		}

		///<summary>Might not be getting called enough.</summary>
		private void ContrAppt_Resize(object sender, System.EventArgs e) {
			//This didn't work so well.  Very slow and caused program to not be able to unminimize
			try{//so it doesn't crash if not connected to DB
				//ModuleSelected();
			}
			catch{}
			//even though the part above didn't work, we're going to try adding the stuff below.  It was important to get it out
			//of the Layout event, which fires very frequently.
			LayoutPanels();
		}

		///<summary>This might not be getting called frequently enough.  Done on resize and when refreshing the period.  But it used to be done very very frequently.</summary>
		private void LayoutPanels(){
			if(Calendar2.Width>panelCalendar.Width) {//for example, Chinese calendar
				panelCalendar.Width=Calendar2.Width+1;
				panelAptInfo.Width=Calendar2.Width+1;
			}
			//Assumes widths of the first 3 panels were set the same in the designer,
			ToolBarMain.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,0);
			panelCalendar.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,ToolBarMain.Height);
			panelAptInfo.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,ToolBarMain.Height+panelCalendar.Height);
			butOther.Location=new Point(panelAptInfo.Location.X+32,panelAptInfo.Location.Y+84);
			tabControl.Location=new Point(panelAptInfo.Left,panelAptInfo.Bottom+1);
			panelSheet.Width=ClientSize.Width-panelAptInfo.Width-2;
			panelSheet.Height=ClientSize.Height-panelSheet.Location.Y;
			if(!DefC.DefShortIsNull) {
				ApptViewItemL.GetForCurView(comboView.SelectedIndex-1,ContrApptSheet.IsWeeklyView,SchedListPeriod);//refreshes visops,etc
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
			panelOps.Width=panelSheet.Width;
		}

		///<summary>Called from FormOpenDental upon startup.</summary>
		public void InitializeOnStartup(){
			//jsparks-
			//This method is inefficient and can cause 4 refreshes: RefreshPeriod, FillViews->comboView_SelectedIndexChanged, SetView?, SetWeeklyView. 
			//This is especially inefficient, because after calling this method, FormOD refreshes this module anyway.  So about 5 refreshes on startup.
			//But it's not critical.
			if(InitializedOnStartup) {
				return;
			}
			InitializedOnStartup=true;
			//if(DefC.DefShortIsNull) {
			//	Defs.RefreshCache();//So that when RefreshPeriod, LayoutPanels, ComputeColWidth gets called.
			LayoutPanels();
			//}
			ContrApptSheet.RowsPerIncr=1;
			AppointmentL.DateSelected=DateTime.Now;
			ContrApptSingle.SelectedAptNum=-1;
			RefreshPeriod();
			FillViews();
			if(ApptViewC.List.Length>0){//if any views
				SetView(1);//default to first view
			}
			menuWeeklyApt.MenuItems.Clear();
			menuWeeklyApt.MenuItems.Add(Lan.g(this,"Copy to Pinboard"),new EventHandler(menuWeekly_Click));
			menuApt.MenuItems.Clear();
			menuApt.MenuItems.Add(Lan.g(this,"Copy to Pinboard"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add("-");
			menuApt.MenuItems.Add(Lan.g(this,"Send to Unscheduled List"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Break Appointment"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Set Complete"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Delete"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Other Appointments"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add("-");
			menuApt.MenuItems.Add(Lan.g(this,"Print Label"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Print Card"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Print Card for Entire Family"),new EventHandler(menuApt_Click));
			menuApt.MenuItems.Add(Lan.g(this,"Routing Slip"),new EventHandler(menuApt_Click));
			menuBlockout.MenuItems.Clear();
			menuBlockout.MenuItems.Add(Lan.g(this,"Edit Blockout"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Cut Blockout"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Copy Blockout"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Paste Blockout"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Delete Blockout"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Add Blockout"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Blockout Cut-Copy-Paste"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Clear All Blockouts for Day"),new EventHandler(menuBlockout_Click));
			menuBlockout.MenuItems.Add(Lan.g(this,"Edit Blockout Types"),new EventHandler(menuBlockout_Click));
			Lan.C(this,new Control[]
				{
				butToday,
				//butTodayWk,
				butSearch,
				butClearPin,
				label2,
				label7,
				butOther
				});
			LayoutToolBar();
			//Appointment action buttons
			toolTip1.SetToolTip(butUnsched, Lan.g(this,"Send to Unscheduled List"));
      toolTip1.SetToolTip(butBreak, Lan.g(this,"Break"));
			toolTip1.SetToolTip(butComplete, Lan.g(this,"Set Complete"));
			toolTip1.SetToolTip(butDelete, Lan.g(this,"Delete"));
			toolTip1.SetToolTip(butOther, Lan.g(this,"Other Appointments"));
			SetWeeklyView(false);
		}

		///<summary></summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			//ODToolBarButton button;
			//button=new ODToolBarButton("",0,Lan.g(this,"Select Patient"),"Patient");
			//button.Style=ODToolBarButtonStyle.DropDownButton;
			//button.DropDownMenu=menuPatient;
			//ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Appointment Lists"),"Lists"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Print Schedule"),"Print"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ApptModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		///<summary>Not in use.  See InstantClasses instead.</summary>
		private void ContrAppt_Load(object sender, System.EventArgs e){
			
		}

		///<summary>The key press from the main form is passed down to this module.</summary>
		public void FunctionKeyPress(Keys keys){
			switch(keys){
				case Keys.F1: SetView(1); break;
				case Keys.F2: SetView(2); break;
				case Keys.F3: SetView(3); break;
				case Keys.F4: SetView(4); break;
				case Keys.F5: SetView(5); break;
				case Keys.F6: SetView(6); break;
				case Keys.F7: SetView(7); break;
				case Keys.F8: SetView(8); break;
				case Keys.F9: SetView(9); break;
				case Keys.F10: SetView(10); break;
				case Keys.F11: SetView(11); break;
				case Keys.F12: SetView(12); break;
			}
		}

		/// <summary>Sets the view to the specified index, checking for validity in the process.</summary>
		private void SetView(int viewIndex){
			if(viewIndex > ApptViewC.List.Length){
				return;
			}
			comboView.SelectedIndex=viewIndex;//this also triggers SelectedIndexChanged
		}

		///<summary>Fills the comboView with the current list of views and then tries to reselect the previous selection.  Also called from FormOpenDental.RefreshLocalData().</summary>
		public void FillViews(){
			int selected=comboView.SelectedIndex;
			comboView.Items.Clear();
			comboView.Items.Add(Lan.g(this,"none"));
			string f="";
			for(int i=0;i<ApptViewC.List.Length;i++){
				if(i<=12)
					f="F"+(i+1).ToString()+"-";
				else
					f="";
				comboView.Items.Add(f+ApptViewC.List[i].Description);
			}
			if(selected<comboView.Items.Count){
				comboView.SelectedIndex=selected;//this also triggers SelectedIndexChanged
			}
			if(comboView.SelectedIndex==-1){
				comboView.SelectedIndex=0;
			}
		}

		///<summary>Sets appointment data invalid on all other computers, causing them to refresh.  Does NOT refresh the data for this computer which must be done separately.</summary>
		private void SetInvalid(){
			DataValid.SetInvalid(AppointmentL.DateSelected);
		}

		///<summary>Clicked today.</summary>
		private void butToday_Click(object sender,System.EventArgs e) {
			AppointmentL.DateSelected=DateTime.Today;
			SetWeeklyView(radioWeek.Checked);
		}

		///<summary>Clicked back one day.</summary>
		private void butBack_Click(object sender,System.EventArgs e) {
			AppointmentL.DateSelected=AppointmentL.DateSelected.AddDays(-1);
			SetWeeklyView(radioWeek.Checked);
		}

		///<summary>Clicked forward one day.</summary>
		private void butFwd_Click(object sender,System.EventArgs e) {
			AppointmentL.DateSelected=AppointmentL.DateSelected.AddDays(1);
			SetWeeklyView(radioWeek.Checked);
		}

		private void butBackMonth_Click(object sender,EventArgs e) {
			AppointmentL.DateSelected=AppointmentL.DateSelected.AddMonths(-1);
			SetWeeklyView(radioWeek.Checked);
		}

		private void butBackWeek_Click(object sender,EventArgs e) {
			AppointmentL.DateSelected=AppointmentL.DateSelected.AddDays(-7);
			SetWeeklyView(radioWeek.Checked);
		}

		private void butFwdWeek_Click(object sender,EventArgs e) {
			AppointmentL.DateSelected=AppointmentL.DateSelected.AddDays(7);
			SetWeeklyView(radioWeek.Checked);
		}

		private void butFwdMonth_Click(object sender,EventArgs e) {
			AppointmentL.DateSelected=AppointmentL.DateSelected.AddMonths(1);
			SetWeeklyView(radioWeek.Checked);
		}

		private void radioDay_Click(object sender,EventArgs e) {
			SetWeeklyView(false);
		}

		private void radioWeek_Click(object sender,EventArgs e) {
			SetWeeklyView(true);
		}

		///<summary>Clicked a date on the calendar.</summary>
		private void Calendar2_DateSelected(object sender,System.Windows.Forms.DateRangeEventArgs e) {
			AppointmentL.DateSelected=Calendar2.SelectionStart;
			SetWeeklyView(radioWeek.Checked);
		}

		///<summary>Switches between weekly view and daily view.  AppointmentL.DateSelected needs to be set first.  Calculates WeekStartDate and WeekEndDate based on AppointmentL.DateSelected.  Then calls either RefreshPeriod or ModuleSelected.</summary>
		private void SetWeeklyView(bool isWeeklyView) {
			//if the weekly view doesn't change, then just RefreshPeriod
			bool weeklyViewChanged=false;
			if(isWeeklyView!=ContrApptSheet.IsWeeklyView){
				weeklyViewChanged=true;
			}
			//for those few times when the radiobuttons aren't quite right:
			if(isWeeklyView) {
				radioWeek.Checked=true;
				butFwd.Enabled=false;
				butBack.Enabled=false;
			}
			else {
				radioDay.Checked=true;
				butFwd.Enabled=true;
				butBack.Enabled=true;
			}
			WeekStartDate=AppointmentL.DateSelected.AddDays(1-(int)AppointmentL.DateSelected.DayOfWeek).Date;
			WeekEndDate=WeekStartDate.AddDays(ContrApptSheet.NumOfWeekDaysToDisplay-1).Date;
			ContrApptSheet.IsWeeklyView=isWeeklyView;
			if(weeklyViewChanged || isWeeklyView){
				if(PatCur==null) {
					ModuleSelected(0);
				}
				else {
					ModuleSelected(PatCur.PatNum);
				}
			}
			else{
				RefreshPeriod();
			}
		}

		///<summary>Fills the lab summary for the day.</summary>
		private void FillLab(List<LabCase> labCaseList){
			int notRec=0;
			for(int i=0;i<labCaseList.Count;i++){
				if(labCaseList[i].DateTimeChecked.Year>1880){
					continue;
				}
				if(labCaseList[i].DateTimeRecd.Year>1880) {
					continue;
				}
				notRec++;
			}
			if(notRec==0){
				textLab.Font=new Font(FontFamily.GenericSansSerif,8,FontStyle.Regular);
				textLab.ForeColor=Color.Black;
				textLab.Text=Lan.g(this,"All Received");
			}
			else{
				textLab.Font=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
				textLab.ForeColor=Color.DarkRed;
				textLab.Text=notRec.ToString()+Lan.g(this," NOT RECEIVED");
			}
		}

		///<summary>Fills the production summary for the day.</summary>
		private void FillProduction(){
			bool showProduction=false;
			for(int i=0;i<ApptViewItemL.ApptRows.Count;i++) {
				if(ApptViewItemL.ApptRows[i].ElementDesc=="Production"){
					showProduction=true;
				}
			}
			if(showProduction){
				double totalproduction=0;
				double hygproduction = 0;
				double drproduction = 0;
				bool nohygAssigned = false;
				int indexProv;
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
					indexProv=-1;
					if(DS.Tables["Appointments"].Rows[i]["IsHygiene"].ToString()=="1"){
						if(DS.Tables["Appointments"].Rows[i]["ProvHyg"].ToString()=="0") {//set ishyg, but no hyg prov set.
							indexProv=ApptViewItemL.GetIndexProv(PIn.PLong(DS.Tables["Appointments"].Rows[i]["ProvNum"].ToString()));
						}
						else {
							indexProv=ApptViewItemL.GetIndexProv(PIn.PLong(DS.Tables["Appointments"].Rows[i]["ProvHyg"].ToString()));
						}
					}
					else{
						indexProv=ApptViewItemL.GetIndexProv(PIn.PLong(DS.Tables["Appointments"].Rows[i]["ProvNum"].ToString()));
					}
					if(indexProv==-1){
						continue;
					}
					if(DS.Tables["Appointments"].Rows[i]["AptStatus"].ToString()!=((int)ApptStatus.Broken).ToString()
						&& DS.Tables["Appointments"].Rows[i]["AptStatus"].ToString()!=((int)ApptStatus.UnschedList).ToString()
						&& DS.Tables["Appointments"].Rows[i]["AptStatus"].ToString() != ((int)ApptStatus.PtNote).ToString()
						&& DS.Tables["Appointments"].Rows[i]["AptStatus"].ToString() != ((int)ApptStatus.PtNoteCompleted).ToString())
					{
						//set individual production #'s
						if(DS.Tables["Appointments"].Rows[i]["IsHygiene"].ToString() == "1" && !nohygAssigned) {
							hygproduction += PIn.PDouble(DS.Tables["Appointments"].Rows[i]["productionVal"].ToString());
						}
						else {
							drproduction += PIn.PDouble(DS.Tables["Appointments"].Rows[i]["productionVal"].ToString());
						}
						//the production numbers above will not be accurate enough to be useful.  They are not based on individual
						//procedures, so the inaccuracies will only cause more complaints rather than providing useful information.
						//The only real solution would be to generate the total production numbers in another table
						//from the business layer.  But even that won't work until hyg procedures are appropriately assigned
						//when setting appointments.
						totalproduction+=PIn.PDouble(DS.Tables["Appointments"].Rows[i]["productionVal"].ToString());
					}
				}
				textProduction.Text=totalproduction.ToString("c0");
			}
			else{
				textProduction.Text="";
			}
		}

		///<summary></summary>
		private void FillEmpSched(){
			DataTable table=DS.Tables["EmpSched"];
			gridEmpSched.BeginUpdate();
			gridEmpSched.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableApptEmpSched","Employee"),80);
			gridEmpSched.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptEmpSched","Schedule"),100);
			gridEmpSched.Columns.Add(col);
			gridEmpSched.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["empName"].ToString());
				row.Cells.Add(table.Rows[i]["schedule"].ToString());
				gridEmpSched.Rows.Add(row);
			}
			gridEmpSched.EndUpdate();
		}

		///<summary></summary>
		private void FillWaitingRoom(){
			if(DS==null) {
				return;
			}
			TimeSpan delta=DateTime.Now-LastTimeDataRetrieved;
			DataTable table=DS.Tables["WaitingRoom"];
			gridWaiting.BeginUpdate();
			gridWaiting.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableApptWaiting","Patient"),130);
			gridWaiting.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableApptWaiting","Waited"),100,HorizontalAlignment.Center);
			gridWaiting.Columns.Add(col);
			gridWaiting.Rows.Clear();
			DateTime waitTime;
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["patName"].ToString());
				waitTime=DateTime.Parse(table.Rows[i]["waitTime"].ToString());//we ignore date
				waitTime+=delta;
				row.Cells.Add(waitTime.ToString("H:mm:ss"));
				gridWaiting.Rows.Add(row);
			}
			gridWaiting.EndUpdate();
		}

		private void gridEmpSched_CellDoubleClick(object sender,ODGridClickEventArgs e) {

		}

		private void gridEmpSched_DoubleClick(object sender,EventArgs e) {
			if(ContrApptSheet.IsWeeklyView){
				MsgBox.Show(this,"Not available in weekly view");
				return;
			}
			if(!Security.IsAuthorized(Permissions.Schedules)) {
				return;
			}
			FormScheduleDayEdit FormS=new FormScheduleDayEdit(AppointmentL.DateSelected);
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Schedules,0,"");
			SetWeeklyView(false);//to refresh
		}

		///<summary>Creates one bitmap image for each appointment if visible.</summary>
		private void CreateAptShadows(){
			if(ContrApptSheet2.Shadow==null){//if user resizes window to be very narrow
				return;
			}
			using(Graphics grfx=Graphics.FromImage(ContrApptSheet2.Shadow)) {
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					ContrApptSingle3[i].CreateShadow();
					if(ContrApptSingle3[i].Location.X>=ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount
					&& ContrApptSingle3[i].Width>3
					&& ContrApptSingle3[i].Shadow!=null) {
						grfx.DrawImage(ContrApptSingle3[i].Shadow
							,ContrApptSingle3[i].Location.X,ContrApptSingle3[i].Location.Y);
					}
					if(ContrApptSingle3[i].Shadow!=null) {
						ContrApptSingle3[i].Shadow.Dispose();
					}
				}
			}
		}

		///<summary>Gets the index within the array of appointment controls, based on the supplied primary key.</summary>
		private int GetIndex(long myAptNum) {
			int retVal=-1;
			for(int i=0;i<ContrApptSingle3.Length;i++){
				if(ContrApptSingle3[i].DataRoww["AptNum"].ToString()==myAptNum.ToString()){
					retVal=i;
				}
			}
			return retVal;
		}

		private void SendToPinBoard(long aptNum) {
			List<long> list=new List<long>();
			list.Add(aptNum);
			SendToPinBoard(list);
		}

		///<summary>Loads all info for for specified appointment into the control that displays the pinboard appointment. Runs RefreshModulePatient.  Sets pinboard appointment as selected.</summary>
		private void SendToPinBoard(List<long> aptNums) {
			if(aptNums.Count==0){
				return;
			}
			DataRow row=null;
			for(int a=0;a<aptNums.Count;a++){
				row=null;
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
					if(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()==aptNums[a].ToString()){
						row=DS.Tables["Appointments"].Rows[i];
						break;
					}
				}
				if(row==null){
					row=Appointments.RefreshOneApt(aptNums[a],false).Tables["Appointments"].Rows[0];
					if(row["AptStatus"].ToString()=="6") {//planned
						//then do it again the right way
						row=Appointments.RefreshOneApt(aptNums[a],true).Tables["Appointments"].Rows[0];
					}
				}
				pinBoard.AddAppointment(row);
			}
			//deal with this later:
			//try{
			//	
			//}
			//catch(ApplicationException ex){
			//	MessageBox.Show(ex.Message);//more of a notification really.  It will still highlight that appointment.
				//return;
			//}
			//if aptNum is already in DS, then use that row.  Otherwise, get a new row.
			//it will set pt to the last appt on the pinboard.
			RefreshModuleDataPatient(PIn.PLong(row["PatNum"].ToString()));
			OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
			//RefreshModulePatient(PIn.PInt(row["PatNum"].ToString()));
			mouseIsDown=false;
			boolAptMoved=false;
			ContrApptSingle.SelectedAptNum=-1;
		}

		private void pinBoard_SelectedIndexChanged(object sender,EventArgs e) {
			RefreshModuleDataPatient(PIn.PLong(pinBoard.ApptList[pinBoard.SelectedIndex].DataRoww["PatNum"].ToString()));
			RefreshModuleScreenPatient();
			OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
			//RefreshModulePatient(PIn.PInt(pinBoard.ApptList[pinBoard.SelectedIndex].DataRoww["PatNum"].ToString()));
			//Since this is usually caused by user mouse, then it goes right into pinBoard_MouseDown().
		}

		///<Summary>Sets selected and prepares for drag.</Summary>
		private void pinBoard_MouseDown(object sender,MouseEventArgs e) {
			if(pinBoard.SelectedIndex==-1){
				return;
			}
			mouseIsDown = true;
			//ContrApptSingle.PinBoardIsSelected=true;
			TempApptSingle=new ContrApptSingle();
			TempApptSingle.DataRoww=pinBoard.SelectedAppt.DataRoww;
			TempApptSingle.Visible=false;
			Controls.Add(TempApptSingle);
			TempApptSingle.SetLocation();
			TempApptSingle.CreateShadow();
			TempApptSingle.BringToFront();
			ContrApptSingle.SelectedAptNum=-1;
			//RefreshModulePatient(PIn.PInt(PinApptSingle.DataRoww["PatNum"].ToString()));//already done
			//PinApptSingle.CreateShadow();
			//PinApptSingle.Refresh();
			CreateAptShadows();//to clear previous selection
			ContrApptSheet2.DrawShadow();
			//mouseOrigin is in ContrAppt coordinates (essentially, the entire window)
			mouseOrigin.X=e.X+pinBoard.Location.X+panelCalendar.Location.X;
				//e.X+PinApptSingle.Location.X;
			mouseOrigin.Y=e.Y+pinBoard.SelectedAppt.Location.Y+pinBoard.Location.Y+panelCalendar.Location.Y;
				//e.Y+PinApptSingle.Location.Y;
			contOrigin=new Point(pinBoard.Location.X+panelCalendar.Location.X,
				pinBoard.SelectedAppt.Location.Y+pinBoard.Location.Y+panelCalendar.Location.Y);
				//PinApptSingle.Location;
		}

		///<Summary>Moves pinboard appt if mouse is down.</Summary>
		private void pinBoard_MouseMove(object sender,MouseEventArgs e) {
			if(!mouseIsDown){
				return;
			}
			//not sure what this is:
			//if((Math.Abs(e.X+PinApptSingle.Location.X-mouseOrigin.X)<1)
			//	&&(Math.Abs(e.Y+PinApptSingle.Location.Y-mouseOrigin.Y)<1)){
			//	return;
			//}
			if(TempApptSingle.Location==new Point(0,0)){
				TempApptSingle.Height=1;//to prevent flicker in UL corner
			}
			TempApptSingle.Visible=true;
			boolAptMoved=true;
			TempApptSingle.Location=new Point(
				contOrigin.X+(e.X+pinBoard.Location.X+panelCalendar.Location.X)-mouseOrigin.X,
				contOrigin.Y+(e.Y+pinBoard.SelectedAppt.Location.Y+pinBoard.Location.Y+panelCalendar.Location.Y)-mouseOrigin.Y);
			if(TempApptSingle.Height==1){
				TempApptSingle.SetSize();
			}
		}

		///<Summary>Usually happens after a pinboard appt has been dragged onto main appt sheet.</Summary>
		private void pinBoard_MouseUp(object sender,MouseEventArgs e) {
			if(!boolAptMoved){
				mouseIsDown=false;
				if(TempApptSingle!=null){
					TempApptSingle.Dispose();
				}
				return;
			}
			if(TempApptSingle.Location.X>ContrApptSheet2.Width){
				mouseIsDown=false;
				boolAptMoved=false;
				TempApptSingle.Dispose();
				return;
			}
			if(pinBoard.SelectedAppt.DataRoww["AptStatus"].ToString()==((int)ApptStatus.Planned).ToString()//if Planned appt is on pinboard
				&& !Security.IsAuthorized(Permissions.AppointmentCreate))//and no permission to create a new appt
			{
				mouseIsDown = false;
				boolAptMoved=false;
				TempApptSingle.Dispose();
				return;
			}
			//security prevents moving an appointment by preventing placing it on the pinboard, not here
			//We no longer ask user this question.  It just slows things down: "Move Appointment?"
			//convert loc to new time
			Appointment aptCur=Appointments.GetOneApt(PIn.PLong(pinBoard.SelectedAppt.DataRoww["AptNum"].ToString()));
			Appointment aptOld=aptCur.Copy();
			RefreshModuleDataPatient(PIn.PLong(pinBoard.SelectedAppt.DataRoww["PatNum"].ToString()));//redundant?
			//Patient pat=Patients.GetPat(PIn.PInt(pinBoard.SelectedAppt.DataRoww["PatNum"].ToString()));
			if(aptCur.IsNewPatient && AppointmentL.DateSelected!=aptCur.AptDateTime){
				Procedures.SetDateFirstVisit(AppointmentL.DateSelected,4,PatCur);
			}
			int tHr=ContrApptSheet2.ConvertToHour(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			int tMin=ContrApptSheet2.ConvertToMin(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			DateTime tDate=AppointmentL.DateSelected;
			if(ContrApptSheet.IsWeeklyView) {
				tDate=WeekStartDate.AddDays(ContrApptSheet2.ConvertToDay(TempApptSingle.Location.X-ContrApptSheet2.Location.X));
			}
			DateTime fromDate=aptCur.AptDateTime.Date;
			aptCur.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
			if(AppointmentRuleC.List.Length>0){
				//this is crude and temporary:
				List<long> aptNums=new List<long>();
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					aptNums.Add(PIn.PLong(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()));//ListDay[i].AptNum;
				}
				List<Procedure> procsMultApts=Procedures.GetProcsMultApts(aptNums);
				Procedure[] procsForOne=Procedures.GetProcsOneApt(aptCur.AptNum,procsMultApts);
				ArrayList doubleBookedCodes=new ArrayList();
					AppointmentL.GetDoubleBookedCodes(aptCur,DS.Tables["Appointments"].Copy(),procsMultApts,procsForOne);
				if(doubleBookedCodes.Count>0){//if some codes would be double booked
					if(AppointmentRules.IsBlocked(doubleBookedCodes)){
						MessageBox.Show(Lan.g(this,"Not allowed to double book:")+" "
							+AppointmentRules.GetBlockedDescription(doubleBookedCodes));
						mouseIsDown = false;
						boolAptMoved=false;
						TempApptSingle.Dispose();
						return;
					}
				}
			}
			Operatory curOp=OperatoryC.ListShort[ApptViewItemL.VisOps
				[ContrApptSheet2.ConvertToOp(TempApptSingle.Location.X-ContrApptSheet2.Location.X)]];
			aptCur.Op=curOp.OperatoryNum;
			if(DoesOverlap(aptCur)){
				int startingOp=ApptViewItemL.GetIndexOp(aptCur.Op);
				bool stillOverlaps=true;
				for(int i=startingOp;i<ApptViewItemL.VisOps.Count;i++) {
					aptCur.Op=OperatoryC.ListShort[ApptViewItemL.VisOps[i]].OperatoryNum;
					if(!DoesOverlap(aptCur)){
						stillOverlaps=false;
						break;
					}
				}
				if(stillOverlaps){
					for(int i=startingOp;i>=0;i--){
						aptCur.Op=OperatoryC.ListShort[ApptViewItemL.VisOps[i]].OperatoryNum;
						if(!DoesOverlap(aptCur)){
							stillOverlaps=false;
							break;
						}
					}
				}
				if(stillOverlaps){
					MessageBox.Show(Lan.g(this,"Appointment overlaps existing appointment."));
					mouseIsDown=false;
					boolAptMoved=false;
					TempApptSingle.Dispose();
					return;
				}
			}
			if(aptCur.AptStatus==ApptStatus.Broken){
				aptCur.AptStatus=ApptStatus.Scheduled;
			}
			if(aptCur.AptStatus==ApptStatus.UnschedList){
				aptCur.AptStatus=ApptStatus.Scheduled;
			}
			long assignedDent=Schedules.GetAssignedProvNumForSpot(SchedListPeriod,curOp,false,aptCur.AptDateTime);
			long assignedHyg=Schedules.GetAssignedProvNumForSpot(SchedListPeriod,curOp,true,aptCur.AptDateTime);
			if(aptCur.AptStatus!=ApptStatus.PtNote && aptCur.AptStatus!=ApptStatus.PtNoteCompleted) {
				//if no dentist/hygienist is assigned to spot, then keep the original dentist/hygienist without prompt.  All appts must have prov.
				if((assignedDent!=0 && assignedDent!=aptCur.ProvNum) || (assignedHyg!=0 && assignedHyg!=aptCur.ProvHyg)) {
					if(MessageBox.Show(Lan.g(this,"Change provider?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes) {
						if(assignedDent!=0) {
							aptCur.ProvNum=assignedDent;
						}
						if(assignedHyg!=0) {//the hygienist will only be changed if the spot has a hygienist.
							aptCur.ProvHyg=assignedHyg;
						}
						if(curOp.IsHygiene) {
							aptCur.IsHygiene=true;
						}
						else {//op not marked as hygiene op
							if(assignedDent==0) {//no dentist assigned
								if(assignedHyg!=0) {//hyg is assigned (we don't really have to test for this)
									aptCur.IsHygiene=true;
								}
							}
							else {//dentist is assigned
								if(assignedHyg==0) {//hyg is not assigned
									aptCur.IsHygiene=false;
								}
								//if both dentist and hyg are assigned, it's tricky
								//only explicitly set it if user has a dentist assigned to the op
								if(curOp.ProvDentist!=0) {
									aptCur.IsHygiene=false;
								}
							}
						}
					}
				}
			}
			aptCur.ClinicNum=curOp.ClinicNum;//we always make clinic match without prompt
			if(aptCur.AptStatus==ApptStatus.Planned){//if Planned appt is on pinboard
				long plannedAptNum=aptCur.AptNum;
				LabCase lab=LabCases.GetForPlanned(aptCur.AptNum);
				aptCur.NextAptNum=aptCur.AptNum;
				aptCur.AptStatus=ApptStatus.Scheduled;
				try{
					Appointments.Insert(aptCur);//now, aptnum is different.
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
				SecurityLogs.MakeLogEntry(Permissions.AppointmentCreate,aptCur.PatNum,
					Patients.GetLim(aptCur.PatNum).GetNameFL()+", "
					+aptCur.AptDateTime.ToString()+", "
					+aptCur.ProcDescript);
				List<Procedure> ProcList=Procedures.Refresh(aptCur.PatNum);
				bool procAlreadyAttached=false;
				for(int i=0;i<ProcList.Count;i++){
					if(ProcList[i].PlannedAptNum==plannedAptNum){//if on the planned apt
						if(ProcList[i].AptNum>0){//already attached to another appt
							procAlreadyAttached=true;
						}
						else{//only update procedures not already attached to another apt
							Procedures.UpdateAptNum(ProcList[i].ProcNum,aptCur.AptNum);
								//.Update(ProcCur,ProcOld);//recall synch not required.
						}
					}
				}
				if(procAlreadyAttached){
					MessageBox.Show(Lan.g(this,"One or more procedures could not be scheduled because they were already attached to another appointment. Someone probably forgot to update the Next appointment in the Chart module."));
				}
				if(lab!=null) {
					LabCases.AttachToAppt(lab.LabCaseNum,aptCur.AptNum);
				}
			}//if planned appointment is on pinboard
			else{//simple drag off pinboard to a new date/time
				try{
					Appointments.Update(aptCur,aptOld);
					SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,aptCur.PatNum,
						Patients.GetLim(aptCur.PatNum).GetNameFL()+", "
						+aptCur.ProcDescript
						+", From "
						+aptOld.AptDateTime.ToString()+", "
						+" To "
						+aptCur.AptDateTime.ToString());
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
			}
			Procedures.SetProvidersInAppointment(aptCur,Procedures.GetProcsForSingle(aptCur.AptNum,false));
			TempApptSingle.Dispose();
			pinBoard.ClearSelected();
			//PinApptSingle.Visible=false;
			//ContrApptSingle.PinBoardIsSelected=false;
			ContrApptSingle.SelectedAptNum=aptCur.AptNum;
			RefreshModuleScreenPatient();
			//RefreshModulePatient(PatCurNum);
			RefreshPeriod();//date moving to for this computer
			SetInvalid();//date moving to for other computers
			AppointmentL.DateSelected=fromDate;
			SetInvalid();//for date moved from for other computers.
			AppointmentL.DateSelected=aptCur.AptDateTime;
			mouseIsDown = false;
			boolAptMoved=false;
		}
		
		///<summary>Called when releasing an appointment to make sure it does not overlap any other appointment.  Tests all appts for the day, even if not visible.</summary>
		private bool DoesOverlap(Appointment aptCur){
			DateTime aptDateTime;
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
				if(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()==aptCur.AptNum.ToString()){
					continue;
				}
				if(DS.Tables["Appointments"].Rows[i]["Op"].ToString()!=aptCur.Op.ToString()){
					continue;
				}
				aptDateTime=PIn.PDateT(DS.Tables["Appointments"].Rows[i]["AptDateTime"].ToString());
				if(aptDateTime.Date!=aptCur.AptDateTime.Date) {
					continue;
				}
				//tests start time
				if(aptCur.AptDateTime.TimeOfDay >= aptDateTime.TimeOfDay
					&& aptCur.AptDateTime.TimeOfDay < aptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(
					DS.Tables["Appointments"].Rows[i]["Pattern"].ToString().Length*5)))
				{
					//Debug.WriteLine(TimeSpan.FromMinutes(ListDay[i].Pattern.Length*5).ToString());
					return true;
				}
				//tests stop time
				if(aptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(aptCur.Pattern.Length*5)) > aptDateTime.TimeOfDay
					&& aptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(aptCur.Pattern.Length*5))
					<= aptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(DS.Tables["Appointments"].Rows[i]["Pattern"].ToString().Length*5)))
				{
					return true;
				}
				//tests engulf
				if(aptCur.AptDateTime.TimeOfDay <= aptDateTime.TimeOfDay
					&& aptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(aptCur.Pattern.Length*5))
					>= aptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(DS.Tables["Appointments"].Rows[i]["Pattern"].ToString().Length*5)))
				{
					return true;
				}
			}
			return false;
		}

		///<summary>Returns the apptNum of the appointment at these coordinates, or 0 if none.  This is new code which is going to replace some of the outdated code on this page.</summary>
		private long HitTestAppt(Point point) {
			if(ApptViewItemL.VisOps.Count==0) {//no ops visible.
				return 0;
			}
			int day=ContrApptSheet.XPosToDay(point.X);
			if(ContrApptSheet.IsWeeklyView) {
				//this is a workaround because we start on Monday:
				if(day==6) {
					day=0;
				}
				else {
					day=day+1;
				}
			}
			//if operatories were just hidden and VisOps is mismatched with ListShort
			int xOp=ApptViewItemL.VisOps[ContrApptSheet.XPosToOp(point.X)];
			if(xOp>OperatoryC.ListShort.Count-1){
				return 0;
			}
			long op=OperatoryC.ListShort[xOp].OperatoryNum;
			int hour=ContrApptSheet.YPosToHour(point.Y);
			int minute=ContrApptSheet.YPosToMin(point.Y);
			TimeSpan time=new TimeSpan(hour,minute,0);
			TimeSpan aptTime;
			int aptDayOfWeek;
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
				if(op.ToString()!=DS.Tables["Appointments"].Rows[i]["Op"].ToString()){
					continue;
				}
				aptDayOfWeek=(int)PIn.PDateT(DS.Tables["Appointments"].Rows[i]["AptDateTime"].ToString()).DayOfWeek;
				if(ContrApptSheet.IsWeeklyView && aptDayOfWeek!=day){
					continue;
				}
				aptTime=PIn.PDateT(DS.Tables["Appointments"].Rows[i]["AptDateTime"].ToString()).TimeOfDay;
				if(aptTime <= time
					&& time < aptTime+TimeSpan.FromMinutes(DS.Tables["Appointments"].Rows[i]["Pattern"].ToString().Length*5))
				{
					return PIn.PLong(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());
				}
			}
			return 0;
		}

		///<summary>If the given point is in the bottom few pixels of an appointment, then this returns true.  Use HitTestAppt to figure out which appointment.</summary>
		private bool HitTestApptBottom(Point point){
			long aptnum=HitTestAppt(point);
			if(aptnum==0){
				return false;
			}
			//get the appointment control in order to measure
			ContrApptSingle apptSing=null;
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
				//ListDay.Length;i++) {
				if(aptnum.ToString()==DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()){
					apptSing=ContrApptSingle3[i];
				}
			}
			//find the bottom border of the appointment
			int bottom=apptSing.Bottom;
			if(point.Y>bottom-8){
				return true;
			}
			return false;
		}

		///<summary>Mouse down event anywhere on the sheet.  Could be a blank space or on an actual appointment.</summary>
		private void ContrApptSheet2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(infoBubble.Visible) {
				infoBubble.Visible=false;
			}
			if(ApptViewItemL.VisOps.Count==0) {//no ops visible.
				return;
			}
			if(mouseIsDown){//if user clicks right mouse button while dragging
				return;
			}
			//some of this is a little redundant, but still necessary for now.
			SheetClickedonHour=ContrApptSheet.YPosToHour(e.Y);
			SheetClickedonMin=ContrApptSheet.YPosToMin(e.Y);
			TimeSpan sheetClickedOnTime=new TimeSpan(SheetClickedonHour,SheetClickedonMin,0);
			ContrApptSingle.ClickedAptNum=HitTestAppt(e.Location);
			SheetClickedonOp=OperatoryC.ListShort[ApptViewItemL.VisOps[ContrApptSheet.XPosToOp(e.X)]].OperatoryNum;
			SheetClickedonDay=ContrApptSheet.XPosToDay(e.X);
			if(!ContrApptSheet.IsWeeklyView) {
				SheetClickedonDay=((int)AppointmentL.DateSelected.DayOfWeek)-1;
			}
			Graphics grfx=ContrApptSheet2.CreateGraphics();
			//if clicked on an appt-----------------------------------------------------------------------------------------------
			if(ContrApptSingle.ClickedAptNum!=0){
				if(e.Button==MouseButtons.Left){
					mouseIsDown = true;
				}
				int thisIndex=GetIndex(ContrApptSingle.ClickedAptNum);
				pinBoard.SelectedIndex=-1;
				//ContrApptSingle.PinBoardIsSelected=false;
				if(ContrApptSingle.SelectedAptNum!=-1//unselects previously selected unless it's the same appt
					&& ContrApptSingle.SelectedAptNum!=ContrApptSingle.ClickedAptNum){
					int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
					//has to be done before refresh prev:
					ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
					if(prevSel!=-1){
						ContrApptSingle3[prevSel].CreateShadow();
						if(ContrApptSingle3[prevSel].Shadow!=null) {
							grfx.DrawImage(ContrApptSingle3[prevSel].Shadow,ContrApptSingle3[prevSel].Location.X
								,ContrApptSingle3[prevSel].Location.Y);
						}
					}
				}
				//again, in case missed in loop above:
				ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
				ContrApptSingle3[thisIndex].CreateShadow();
				grfx.DrawImage(ContrApptSingle3[thisIndex].Shadow,ContrApptSingle3[thisIndex].Location.X
					,ContrApptSingle3[thisIndex].Location.Y);
				RefreshModuleDataPatient(PIn.PLong(ContrApptSingle3[thisIndex].DataRoww["PatNum"].ToString()));
				RefreshModuleScreenPatient();
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(PIn.PInt(ContrApptSingle3[thisIndex].DataRoww["PatNum"].ToString()));
				if(e.Button==MouseButtons.Right){
					//if(ContrApptSheet.IsWeeklyView){
					//	menuWeeklyApt.Show(ContrApptSheet2,new Point(e.X,e.Y));
					//}
					//else{
						menuApt.Show(ContrApptSheet2,new Point(e.X,e.Y));
					//}
				}
				else{
					TempApptSingle=new ContrApptSingle();
					TempApptSingle.Visible=false;//otherwise I get a phantom appt while holding mouse down
					TempApptSingle.DataRoww=ContrApptSingle3[thisIndex].DataRoww;
					Controls.Add(TempApptSingle);
					TempApptSingle.SetLocation();
					TempApptSingle.BringToFront();
					//mouseOrigin is in ApptSheet coordinates
					mouseOrigin=e.Location;
					contOrigin=ContrApptSingle3[thisIndex].Location;
					TempApptSingle.CreateShadow();
					if(HitTestApptBottom(e.Location)){
						ResizingAppt=true;
						ResizingOrigH=TempApptSingle.Height;
					}
					else{
						ResizingAppt=false;
					}
				}
			}
			//not clicked on appt---------------------------------------------------------------------------------------------------
			else{ 
				if(e.Button==MouseButtons.Right){
					bool clickedOnBlock=false;
					Schedule[] ListForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Blockout,0);
					//List<ScheduleOp> listForSched;
					for(int i=0;i<ListForType.Length;i++){
						if(ListForType[i].SchedDate.Date!=WeekStartDate.AddDays(SheetClickedonDay).Date){
							continue;
						}
						if(ListForType[i].StartTime.TimeOfDay > sheetClickedOnTime
							|| ListForType[i].StopTime.TimeOfDay <= sheetClickedOnTime)
						{
							continue;
						}
						//listForSched=ScheduleOps.GetForSched(ListForType[i].ScheduleNum);
						for(int p=0;p<ListForType[i].Ops.Count;p++){
							if(ListForType[i].Ops[p]==SheetClickedonOp){
								clickedOnBlock=true;
								break;
							}
						}
					}
					if(clickedOnBlock){
						menuBlockout.MenuItems[0].Enabled=true;//Edit
						menuBlockout.MenuItems[1].Enabled=true;//Cut
						menuBlockout.MenuItems[2].Enabled=true;//Copy
						menuBlockout.MenuItems[3].Enabled=false;//paste. Can't paste on top of an existing blockout
						menuBlockout.MenuItems[4].Enabled=true;//Delete
					}
					else{
						menuBlockout.MenuItems[0].Enabled=false;//edit
						menuBlockout.MenuItems[1].Enabled=false;//edit
						menuBlockout.MenuItems[2].Enabled=false;//copy
						if(BlockoutClipboard==null){
							menuBlockout.MenuItems[3].Enabled=false;//paste
						}
						else{
							menuBlockout.MenuItems[3].Enabled=true;
						}
						menuBlockout.MenuItems[4].Enabled=false;//delete
					}
					menuBlockout.Show(ContrApptSheet2,new Point(e.X,e.Y));
				}
			}
			grfx.Dispose();
			pinBoard.Invalidate();
			//if(PinApptSingle.Visible){
			//	PinApptSingle.CreateShadow();
			//	PinApptSingle.Refresh();
			//}
			CreateAptShadows();
		}

		///<summary></summary>
		private void ContrApptSheet2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown){
				InfoBubbleDraw(e.Location);
				//decide what the pointer should look like.
				if(HitTestApptBottom(e.Location)){
					Cursor=Cursors.SizeNS;
				}
				else{
					Cursor=Cursors.Default;
				}
				return;
			}
			//if resizing an appointment
			if(ResizingAppt){
				TempApptSingle.Location=new Point(
					contOrigin.X+ContrApptSheet2.Location.X+panelSheet.Location.X+1,//the 1 is an unknown factor
					contOrigin.Y+ContrApptSheet2.Location.Y+panelSheet.Location.Y+1);//ditto
				TempApptSingle.Height=ResizingOrigH+e.Y-mouseOrigin.Y;
				TempApptSingle.CreateShadow();
				TempApptSingle.Visible=true;
				return;
			}
			//dragging an appointment
			//if(ContrApptSheet.IsWeeklyView){
			//	boolAptMoved=false;
			//	return;
			//}
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			if ((Math.Abs(e.X-mouseOrigin.X)<3)//enhances double clicking
				&(Math.Abs(e.Y-mouseOrigin.Y)<3)){
				boolAptMoved=false;
				return;
			}
			boolAptMoved=true;
			TempApptSingle.Location=new Point(
				contOrigin.X+e.X-mouseOrigin.X+ContrApptSheet2.Location.X+panelSheet.Location.X,
				contOrigin.Y+e.Y-mouseOrigin.Y+ContrApptSheet2.Location.Y+panelSheet.Location.Y);
			TempApptSingle.Visible=true;
		}

		///<summary>Used by parent form when a dialog needs to be displayed on the mouse down.</summary>
		public void MouseUpForced(){
			if(!mouseIsDown){
				return;
			}
			mouseIsDown=false;
			if(TempApptSingle!=null){
				TempApptSingle.Dispose();
			}
		}

		///<summary>Usually dropping an appointment to a new location.</summary>
		private void ContrApptSheet2_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown){
				return;
			}
			/*if(ContrApptSheet.IsWeeklyView) {
				ResizingAppt=false;
				mouseIsDown=false;
				TempApptSingle.Dispose();
				return;
			}*/
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointment aptOld;
			//Resizing-------------------------------------------------------------------------------------------------------------
			if(ResizingAppt) {
				if(!TempApptSingle.Visible) {//click with no drag
					ResizingAppt=false;
					mouseIsDown=false;
					TempApptSingle.Dispose();
					return;
				}
				//convert Bottom to a time
				int hr=ContrApptSheet2.ConvertToHour
					(TempApptSingle.Bottom-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
				int minute=ContrApptSheet2.ConvertToMin
					(TempApptSingle.Bottom-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
				TimeSpan bottomSpan=new TimeSpan(hr,minute,0);
				//subtract to get the new length of appt
				TimeSpan newspan=bottomSpan-PIn.PDateT(TempApptSingle.DataRoww["AptDateTime"].ToString()).TimeOfDay;
				int newpatternL=(int)newspan.TotalMinutes/5;
				if(newpatternL < ContrApptSheet.MinPerIncr/5) {//eg. if 1 < 10/5, would make appt too short. 
					newpatternL=ContrApptSheet.MinPerIncr/5;//sets new pattern length at one increment, typically 2 or 3 5min blocks
				}
				else if(newpatternL>78) {//max length of 390 minutes
					newpatternL=78;
				}
				string pattern=TempApptSingle.DataRoww["Pattern"].ToString();
				if(newpatternL<pattern.Length) {//shorten to match new pattern length
					pattern=pattern.Substring(0,newpatternL);
				}
				else if(newpatternL>pattern.Length) {//make pattern longer.
					pattern=pattern.PadRight(newpatternL,'/');
				}
				//Now, check for overlap with other appts.
				DateTime aptDateTime;
				DateTime aptDateTimeCur=PIn.PDateT(TempApptSingle.DataRoww["AptDateTime"].ToString());
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					if(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()==TempApptSingle.DataRoww["AptNum"].ToString()) {
						continue;
					}
					if(DS.Tables["Appointments"].Rows[i]["Op"].ToString()!=TempApptSingle.DataRoww["Op"].ToString()) {
						continue;
					}
					aptDateTime=PIn.PDateT(DS.Tables["Appointments"].Rows[i]["AptDateTime"].ToString());
					if(ContrApptSheet.IsWeeklyView && aptDateTime.Date!=aptDateTimeCur.Date) {
						continue;
					}
					if(aptDateTime<aptDateTimeCur) {
						continue;//we don't care about appointments that are earlier than this one
					}
					if(aptDateTime.TimeOfDay < aptDateTimeCur.TimeOfDay+TimeSpan.FromMinutes(5*pattern.Length)) {
						newspan=aptDateTime.TimeOfDay-aptDateTimeCur.TimeOfDay;
						newpatternL=(int)newspan.TotalMinutes/5;
						pattern=pattern.Substring(0,newpatternL);
					}
				}
				if(pattern=="") {
					pattern="///";
				}
				Appointments.SetPattern(PIn.PLong(TempApptSingle.DataRoww["AptNum"].ToString()),pattern);
				ResizingAppt=false;
				mouseIsDown=false;
				TempApptSingle.Dispose();
				RefreshModuleDataPatient(PatCur.PatNum);
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(PatCurNum);
				RefreshPeriod();
				bubbleAptNum=0;
				SetInvalid();
				return;
			}
			if((Math.Abs(e.X-mouseOrigin.X)<7)
				&&(Math.Abs(e.Y-mouseOrigin.Y)<7)) {
				boolAptMoved=false;
			}
			//it was a click with no drag-----------------------------------------------------------------------------------------
			if(!boolAptMoved) {
				mouseIsDown=false;
				TempApptSingle.Dispose();
				return;
			}
			//dragging to pinboard, so place a copy there---------------------------------------------------------------------------
			if(TempApptSingle.Location.X>ContrApptSheet2.Width) {
				if(!Security.IsAuthorized(Permissions.AppointmentMove)) {
					mouseIsDown=false;
					TempApptSingle.Dispose();
					return;
				}
				//cannot allow moving completed procedure because it could cause completed procs to change date.  Security must block this.
				if(TempApptSingle.DataRoww["AptStatus"].ToString()=="2"){//complete
					mouseIsDown=false;
					TempApptSingle.Dispose();
					MsgBox.Show(this,"Not allowed to move completed appointments.");
					return;
				}
				int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
				List<long> list=new List<long>();
				list.Add(ContrApptSingle.SelectedAptNum);
				SendToPinBoard(list);//sets selectedAptNum=-1. do before refresh prev
				if(prevSel!=-1) {
					CreateAptShadows();
					ContrApptSheet2.DrawShadow();
				}
				RefreshModuleDataPatient(PatCur.PatNum);
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(PatCurNum);
				TempApptSingle.Dispose();
				return;
			}
			//moving to a new location-----------------------------------------------------------------------------------------------
			Appointment apt=Appointments.GetOneApt(ContrApptSingle.SelectedAptNum);
			aptOld=apt.Copy();
			int tHr=ContrApptSheet2.ConvertToHour
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			int tMin=ContrApptSheet2.ConvertToMin
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			bool timeWasMoved=tHr!=apt.AptDateTime.Hour
				|| tMin!=apt.AptDateTime.Minute;
			if(timeWasMoved) {//no question for notes
				if(apt.AptStatus == ApptStatus.PtNote | apt.AptStatus == ApptStatus.PtNoteCompleted) {
					if(!Security.IsAuthorized(Permissions.AppointmentMove)) {
						mouseIsDown = false;
						boolAptMoved = false;
						TempApptSingle.Dispose();
						return;
					}
				}
				else {
					if(!Security.IsAuthorized(Permissions.AppointmentMove) || !MsgBox.Show(this,true,"Move Appointment?")) {
						mouseIsDown = false;
						boolAptMoved = false;
						TempApptSingle.Dispose();
						return;
					}
				}
			}
			//convert loc to new date/time
			DateTime tDate=apt.AptDateTime.Date;
			if(ContrApptSheet.IsWeeklyView) {
				tDate=WeekStartDate.AddDays(ContrApptSheet2.ConvertToDay(TempApptSingle.Location.X-ContrApptSheet2.Location.X));
			}
			apt.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
			List<Procedure> procsMultApts=null;
			Procedure[] procsForOne=null;
			if(AppointmentRuleC.List.Length>0) {
				List<long> aptNums=new List<long>();
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					aptNums.Add(PIn.PLong(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()));//ListDay[i].AptNum;
				}
				procsMultApts=Procedures.GetProcsMultApts(aptNums);
			}
			if(AppointmentRuleC.List.Length>0) {
				long[] aptNums=new long[DS.Tables["Appointments"].Rows.Count];
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					aptNums[i]=PIn.PLong(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());//ListDay[i].AptNum;
				}
				procsForOne=Procedures.GetProcsOneApt(apt.AptNum,procsMultApts);
				ArrayList doubleBookedCodes=
					AppointmentL.GetDoubleBookedCodes(apt,DS.Tables["Appointments"].Copy(),procsMultApts,procsForOne);
				if(doubleBookedCodes.Count>0) {//if some codes would be double booked
					if(AppointmentRules.IsBlocked(doubleBookedCodes)) {
						MessageBox.Show(Lan.g(this,"Not allowed to double book:")+" "
							+AppointmentRules.GetBlockedDescription(doubleBookedCodes));
						mouseIsDown = false;
						boolAptMoved=false;
						TempApptSingle.Dispose();
						return;
					}
				}
			}
			Operatory curOp=OperatoryC.ListShort
				[ApptViewItemL.VisOps[ContrApptSheet2.ConvertToOp(TempApptSingle.Location.X-ContrApptSheet2.Location.X)]];
			apt.Op=curOp.OperatoryNum;
			if(DoesOverlap(apt)) {
				int startingOp=ApptViewItemL.GetIndexOp(apt.Op);
				bool stillOverlaps=true;
				for(int i=startingOp;i<ApptViewItemL.VisOps.Count;i++) {
					apt.Op=OperatoryC.ListShort[ApptViewItemL.VisOps[i]].OperatoryNum;
					if(!DoesOverlap(apt)) {
						stillOverlaps=false;
						break;
					}
				}
				if(stillOverlaps) {
					for(int i=startingOp;i>=0;i--) {
						apt.Op=OperatoryC.ListShort[ApptViewItemL.VisOps[i]].OperatoryNum;
						if(!DoesOverlap(apt)) {
							stillOverlaps=false;
							break;
						}
					}
				}
				if(stillOverlaps) {
					MessageBox.Show(Lan.g(this,"Appointment overlaps existing appointment."));
					mouseIsDown=false;
					boolAptMoved=false;
					TempApptSingle.Dispose();
					return;
				}
			}//end if DoesOverlap
			if(apt.AptStatus==ApptStatus.Broken && timeWasMoved) {
				apt.AptStatus=ApptStatus.Scheduled;
			}
			long assignedDent=Schedules.GetAssignedProvNumForSpot(SchedListPeriod,curOp,false,apt.AptDateTime);
			long assignedHyg=Schedules.GetAssignedProvNumForSpot(SchedListPeriod,curOp,true,apt.AptDateTime);
			if(apt.AptStatus!=ApptStatus.PtNote && apt.AptStatus!=ApptStatus.PtNoteCompleted) {
				//if no dentist/hygenist is assigned to spot, then keep the original dentist/hygenist without prompt.  All appts must have prov.
				if((assignedDent!=0 && assignedDent!=apt.ProvNum) || (assignedHyg!=0 && assignedHyg!=apt.ProvHyg)) {
					if(MessageBox.Show(Lan.g(this,"Change provider?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes) {
						if(assignedDent!=0) {//the dentist will only be changed if the spot has a dentist.
							apt.ProvNum=assignedDent;
						}
						if(assignedHyg!=0) {//the hygienist will only be changed if the spot has a hygienist.
							apt.ProvHyg=assignedHyg;
						}
						if(curOp.IsHygiene) {
							apt.IsHygiene=true;
						}
						else {//op not marked as hygiene op
							if(assignedDent==0) {//no dentist assigned
								if(assignedHyg!=0) {//hyg is assigned (we don't really have to test for this)
									apt.IsHygiene=true;
								}
							}
							else {//dentist is assigned
								if(assignedHyg==0) {//hyg is not assigned
									apt.IsHygiene=false;
								}
								//if both dentist and hyg are assigned, it's tricky
								//only explicitly set it if user has a dentist assigned to the op
								if(curOp.ProvDentist!=0) {
									apt.IsHygiene=false;
								}
							}
						}
					}
				}
			}
			apt.ClinicNum=curOp.ClinicNum;//we always make clinic match without prompt
			try {
				Appointments.Update(apt,aptOld);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
			}
			if(apt.AptStatus!=ApptStatus.Complete){
				Procedures.SetProvidersInAppointment(apt,Procedures.GetProcsForSingle(apt.AptNum,false));
			}
			SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,apt.PatNum,
				Patients.GetLim(apt.PatNum).GetNameFL()+", "
				+apt.ProcDescript
				+" From "
				+aptOld.AptDateTime.ToString()+", "
				+" To "
				+apt.AptDateTime.ToString());
			RefreshModuleDataPatient(PatCur.PatNum);
			OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
			//RefreshModulePatient(PatCurNum);
			RefreshPeriod();
			SetInvalid();
			mouseIsDown = false;
			boolAptMoved=false;
			TempApptSingle.Dispose();
		}

		private void ContrApptSheet2_MouseLeave(object sender,EventArgs e) {
			InfoBubbleDraw(new Point(-1,-1));
			timerInfoBubble.Enabled=false;//redundant?
			Cursor=Cursors.Default;
		}

		///<summary></summary>
		private void InfoBubble_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Calculate the real point in sheet coordinates
			Point p=new Point(e.X+infoBubble.Left-ContrApptSheet2.Left-panelSheet.Left,
				e.Y+infoBubble.Top-ContrApptSheet2.Top-panelSheet.Top);
			InfoBubbleDraw(p);
		}

		///<summary></summary>
		private void PicturePat_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			//Calculate the real point in sheet coordinates
			Point p=new Point(e.X+infoBubble.Left+PicturePat.Left-ContrApptSheet2.Left-panelSheet.Left,
				e.Y+infoBubble.Top+PicturePat.Top-ContrApptSheet2.Top-panelSheet.Top);
			InfoBubbleDraw(p);
		}

		///<summary>Does a hit test to determine if over an appointment.  Fills the bubble with data and then positions it.</summary>
		private void InfoBubbleDraw(Point p){
			//remember where to draw for hover effect
			if(PrefC.GetBool(PrefName.AppointmentBubblesDisabled)){
				infoBubble.Visible=false;
				timerInfoBubble.Enabled=false;
				return;
			}
			bubbleLocation=p;
			long aptNum=HitTestAppt(p);
			if(aptNum==0 || HitTestApptBottom(p)) {
				if(infoBubble.Visible) {
					infoBubble.Visible=false;
					timerInfoBubble.Enabled=false;
				}
				return;
			}
			if(aptNum!=bubbleAptNum){
				//reset timer for popup delay
				timerInfoBubble.Enabled=false;
				timerInfoBubble.Enabled=true;
				//delay for hover effect 0.28 sec
				bubbleTime=DateTime.Now;
				bubbleAptNum=aptNum;
				//most data is already present in DS.Appointment, but we do need to get the patient picture
				infoBubble.BackgroundImage=new Bitmap(infoBubble.Width,800);
				Image img=infoBubble.BackgroundImage;//alias
				Graphics g=Graphics.FromImage(img);//infoBubble.BackgroundImage);
				g.TextRenderingHint=TextRenderingHint.ClearTypeGridFit;
				g.SmoothingMode=SmoothingMode.HighQuality;
				g.FillRectangle(new SolidBrush(infoBubble.BackColor),0,0,img.Width,img.Height);
				DataRow row=null;
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
					if(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString()==aptNum.ToString()){
						row=DS.Tables["Appointments"].Rows[i];
					}
				}
				//row will never be null
				Font font=new Font(FontFamily.GenericSansSerif,10f,FontStyle.Bold);
				float y=0;
				Brush brush=Brushes.Black;
				g.DrawString(row["patientName"].ToString(),font,brush,8,y);
				y+=(int)g.MeasureString("X",font).Height;
				PicturePat.Image=null;
				if(//PatCur==null ||
					row["ImageFolder"].ToString()!=""
					&& PrefC.UsingAtoZfolder)//Do not use patient image when A to Z folders are disabled.
					//return;
				{
					try {
						Bitmap patPict;
						Documents.GetPatPict(PIn.PLong(row["PatNum"].ToString()),
							ODFileUtils.CombinePaths(ImageStore.GetPreferredImagePath(),
								row["ImageFolder"].ToString().Substring(0,1).ToUpper(),
								row["ImageFolder"].ToString(),""),
							out patPict);
						PicturePat.Image=patPict;
					}
					catch {		}
				}
				float x=110;
				font=new Font(FontFamily.GenericSansSerif,9f);
				float rowH=g.MeasureString("X",font).Height;
				y-=3;
				g.DrawString(row["aptDay"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["aptDate"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["aptTime"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["aptLength"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["provider"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["production"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["confirmed"].ToString(),font,brush,x,y);
				y+=rowH;
				y=120;
				x=2;
				if(row["AptStatus"].ToString()==((int)ApptStatus.ASAP).ToString()){
					g.DrawString(Lan.g("enumApptStatus","ASAP"),font,Brushes.Red,x,y);
					y+=rowH;
				}
				if(row["preMedFlag"].ToString()!=""){
					g.DrawString(row["preMedFlag"].ToString(),font,Brushes.Red,x,y);
					y+=rowH;
				}
				float h;
				if(row["MedUrgNote"].ToString()!="") {
					h=g.MeasureString(row["MedUrgNote"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["MedUrgNote"].ToString(),font,Brushes.Red,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				if(row["lab"].ToString()!="") {
					g.DrawString(row["lab"].ToString(),font,Brushes.Red,x,y);
					y+=rowH;
				}
				if(row["procs"].ToString()!="") {
					h=g.MeasureString(row["procs"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["procs"].ToString(),font,brush,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				if(row["Note"].ToString()!="") {
					h=g.MeasureString(row["Note"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["Note"].ToString(),font,Brushes.Blue,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				//patient info---------------------
				y+=3;
				g.DrawLine(new Pen(Brushes.Gray,1.5f),3,y,infoBubble.Width-3,y);
				y+=2;
				g.DrawString(row["patNum"].ToString(),font,brush,x,y);
				y+=rowH;
				if(row["chartNumber"].ToString()!="") {
					g.DrawString(row["chartNumber"].ToString(),font,brush,x,y);
					y+=rowH;
				}
				g.DrawString(row["billingType"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["age"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["hmPhone"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["wkPhone"].ToString(),font,brush,x,y);
				y+=rowH;
				g.DrawString(row["wirelessPhone"].ToString(),font,brush,x,y);
				y+=rowH;
				if(row["contactMethods"].ToString()!="") {
					h=g.MeasureString(row["contactMethods"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["contactMethods"].ToString(),font,brush,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				if(row["insurance"].ToString()!="") {//overkill since it's only one line
					h=g.MeasureString(row["insurance"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["insurance"].ToString(),font,brush,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				if(row["addrNote"].ToString()!="") {
					h=g.MeasureString(row["addrNote"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["addrNote"].ToString(),font,Brushes.Red,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				if(row["famFinUrgNote"].ToString()!="") {
					h=g.MeasureString(row["famFinUrgNote"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["famFinUrgNote"].ToString(),font,Brushes.Red,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				if(row["apptModNote"].ToString()!="") {
					h=g.MeasureString(row["apptModNote"].ToString(),font,infoBubble.Width).Height;
					g.DrawString(row["apptModNote"].ToString(),font,Brushes.Red,new RectangleF(x,y,infoBubble.Width,h));
					y+=h;
				}
				//other family members?
				g.DrawRectangle(Pens.Gray,0,0,infoBubble.Width-1,(int)y+4);
				g.Dispose();
				infoBubble.Size=new Size(infoBubble.Width,(int)y+5);
				infoBubble.BringToFront();
			}
			int yval=p.Y+ContrApptSheet2.Top+panelSheet.Top+10;
			if(yval > panelSheet.Bottom-infoBubble.Height){
				yval=panelSheet.Bottom-infoBubble.Height;
			}
			infoBubble.Location=new Point(p.X+ContrApptSheet2.Left+panelSheet.Left+10,yval);
			/*only show right away if option set for no delay, otherwise, it will not show
			until mouse had hovered for at least 0.28 seconds(arbitrary #)
			the timer fires at 0.30 seconds, so the difference was introduced because
			of what seemed to be inconsistencies in the timer function */
			if (DateTime.Now.AddMilliseconds(-280) > bubbleTime | !PrefC.GetBool(PrefName.ApptBubbleDelay)){
				infoBubble.Visible=true;
			}
			else{
				infoBubble.Visible=false;
			}
		}

		///<summary>Double click on appt sheet or on a single appointment.</summary>
		private void ContrApptSheet2_DoubleClick(object sender, System.EventArgs e) {
			mouseIsDown=false;
			//this logic is a little different than mouse down for now because on the first click of a 
			//double click, an appointment control is created under the mouse.
			if(ContrApptSingle.ClickedAptNum!=0){//on appt
				long patnum=PIn.PLong(TempApptSingle.DataRoww["PatNum"].ToString());
				TempApptSingle.Dispose();
				//security handled inside the form
				FormApptEdit FormAE=new FormApptEdit(ContrApptSingle.ClickedAptNum);
				FormAE.ShowDialog();
				if(FormAE.DialogResult==DialogResult.OK){
					Appointment apt=Appointments.GetOneApt(ContrApptSingle.ClickedAptNum);
					if(apt!=null && DoesOverlap(apt)){
						Appointment aptOld=apt.Copy();
						MsgBox.Show(this,"Appointment is too long and would overlap another appointment.  Automatically shortened to fit.");
						while(DoesOverlap(apt)){
							apt.Pattern=apt.Pattern.Substring(0,apt.Pattern.Length-1);
							if(apt.Pattern.Length==1){
								break;
							}
						}
						try{
							Appointments.Update(apt,aptOld);
						}
						catch(ApplicationException ex){
							MessageBox.Show(ex.Message);
						}
					}
					ModuleSelected(patnum);//apt.PatNum);//apt might be null if user deleted appt.
					SetInvalid();
				}
			}
			//not on apt, so trying to schedule an appointment---------------------------------------------------------------------
			else{
				if(!Security.IsAuthorized(Permissions.AppointmentCreate)){
					return;
				}
				FormPatientSelect FormPS=new FormPatientSelect();
				if(PatCur!=null){
					FormPS.InitialPatNum=PatCur.PatNum;
				}
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					return;
				}
				if(PatCur==null || FormPS.SelectedPatNum!=PatCur.PatNum){//if the patient was changed
					RefreshModuleDataPatient(FormPS.SelectedPatNum);
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
					//RefreshModulePatient(FormPS.SelectedPatNum);
				}
				Appointment apt;
				if(FormPS.NewPatientAdded){
					//Patient pat=Patients.GetPat(PatCurNum);
					apt=new Appointment();
					apt.PatNum=PatCur.PatNum;
					apt.IsNewPatient=true;
					apt.Pattern="/X/";
					if(PatCur.PriProv==0){
						apt.ProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
					}
					else{			
						apt.ProvNum=PatCur.PriProv;
					}
					apt.ProvHyg=PatCur.SecProv;
					apt.ClinicNum=PatCur.ClinicNum;
					apt.AptStatus=ApptStatus.Scheduled;
					DateTime d=AppointmentL.DateSelected;
					if(ContrApptSheet.IsWeeklyView){
						d=WeekStartDate.AddDays(SheetClickedonDay);
					}
					//minutes always rounded down.
					int minutes=(int)(ContrAppt.SheetClickedonMin/ContrApptSheet.MinPerIncr)*ContrApptSheet.MinPerIncr;
					apt.AptDateTime=new DateTime(d.Year,d.Month,d.Day,ContrAppt.SheetClickedonHour,minutes,0);
					apt.Op=SheetClickedonOp;
					Operatory curOp=Operatories.GetOperatory(apt.Op);
					if(curOp.ProvDentist!=0) {//if no dentist is assigned to op, then keep the original dentist.  All appts must have prov.
						apt.ProvNum=curOp.ProvDentist;
					}
					apt.ProvHyg=curOp.ProvHygienist;
					apt.IsHygiene=curOp.IsHygiene;
					apt.ClinicNum=curOp.ClinicNum;
					try{
						Appointments.Insert(apt);
					}
					catch(ApplicationException ex){
						MessageBox.Show(ex.Message);
					}
					FormApptEdit FormAE=new FormApptEdit(apt.AptNum);//this is where security log entry is made
					FormAE.IsNew=true;
					FormAE.ShowDialog();
					if(FormAE.DialogResult==DialogResult.OK){
						RefreshModuleDataPatient(PatCur.PatNum);
						OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
						//RefreshModulePatient(PatCurNum);
						RefreshPeriod();
						SetInvalid();
					}
				}
				else{
					DisplayOtherDlg(true);//this also refreshes screen if needed.
				}
			}
		}

		///<summary>Displays the Other Appointments for the current patient, then refreshes screen as needed.  initialClick specifies whether the user doubleclicked on a blank time to get to this dialog.</summary>
		private void DisplayOtherDlg(bool initialClick){
			if(PatCur==null){
				return;
			}
			FormApptsOther FormAO=new FormApptsOther(PatCur.PatNum);
			FormAO.InitialClick=initialClick;
			FormAO.ShowDialog();
			if(FormAO.OResult==OtherResult.Cancel){
				return;
			}
			switch(FormAO.OResult){
				case OtherResult.CopyToPinBoard:
					SendToPinBoard(FormAO.AptNumsSelected);
					RefreshModuleDataPatient(FormAO.SelectedPatNum);
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
					//RefreshModulePatient(FormAO.SelectedPatNum);
					RefreshPeriod();
					break;
				case OtherResult.NewToPinBoard:
					SendToPinBoard(FormAO.AptNumsSelected);
					RefreshModuleDataPatient(FormAO.SelectedPatNum);
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
					//RefreshModulePatient(FormAO.SelectedPatNum);
					RefreshPeriod();
					break;
				case OtherResult.PinboardAndSearch:
					SendToPinBoard(FormAO.AptNumsSelected);
					if(ContrApptSheet.IsWeeklyView) {
						break;
					}
					dateSearch.Text=FormAO.DateJumpToString;
					if(!groupSearch.Visible){//if search not already visible
						ShowSearch();
					}
					DoSearch();
					break;
				case OtherResult.CreateNew:
					ContrApptSingle.SelectedAptNum=FormAO.AptNumsSelected[0];
					RefreshModuleDataPatient(FormAO.SelectedPatNum);
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
					//RefreshModulePatient(FormAO.SelectedPatNum);
					RefreshPeriod();
					SetInvalid();
					break;
				case OtherResult.GoTo:
					ContrApptSingle.SelectedAptNum=FormAO.AptNumsSelected[0];
					AppointmentL.DateSelected=PIn.PDate(FormAO.DateJumpToString);
					RefreshModuleDataPatient(FormAO.SelectedPatNum);
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
					//RefreshModulePatient(FormAO.SelectedPatNum);
					RefreshPeriod();
					break;
			}
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Lists":
						OnLists_Click();
						break;
					case "Print":
						OnPrint_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(long)){
				if(PatCur!=null){
					Patient pat=Patients.GetPat(PatCur.PatNum);
					ProgramL.Execute((long)e.Button.Tag,pat);
				}
			}
		}

		/*private void OnPat_Click() {
			FormPatientSelect formPS = new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult!=DialogResult.OK){
				return;
			}
			RefreshModulePatient(formPS.SelectedPatNum);
			DisplayOtherDlg(false);
		}*/

		private void OnUnschedList_Click() {
			Cursor=Cursors.WaitCursor;
			FormUnsched FormUnsched2=new FormUnsched();
			FormUnsched2.ShowDialog();
			if(FormUnsched2.PinClicked){
				SendToPinBoard(FormUnsched2.AptSelected);
			}
			if(FormUnsched2.SelectedPatNum!=0){
				RefreshModuleDataPatient(FormUnsched2.SelectedPatNum);
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(FormUnsched2.SelectedPatNum);
			}
			Cursor=Cursors.Default;
		}

		private void OnASAPList_Click() {
			Cursor=Cursors.WaitCursor;
			FormASAP FormA=new FormASAP();
			FormA.ShowDialog();
			if(FormA.PinClicked){
				SendToPinBoard(FormA.AptSelected);
			}
			if(FormA.SelectedPatNum!=0){
				RefreshModuleDataPatient(FormA.SelectedPatNum);
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(FormA.SelectedPatNum);
			}
			Cursor=Cursors.Default;
		}

		private void OnRecall_Click() {
			//Cursor=Cursors.WaitCursor;
			if(FormRecallL==null || FormRecallL.IsDisposed) {
				FormRecallL=new FormRecallList();
			}
			FormRecallL.Show();
			if(FormRecallL.WindowState==FormWindowState.Minimized) {
				FormRecallL.WindowState=FormWindowState.Normal;
			}
			FormRecallL.BringToFront();
			//if(FormRL.PinClicked){
			//	SendToPinBoard(FormRL.AptNumsSelected);
			//}
			//if(FormRL.SelectedPatNum!=0){
			//	RefreshModuleDataPatient(FormRL.SelectedPatNum);
			//	OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(FormRL.SelectedPatNum);
			//}
			//Cursor=Cursors.Default;
		}

		private void OnConfirm_Click() {
			Cursor=Cursors.WaitCursor;
			FormConfirmList FormC=new FormConfirmList();
			FormC.ShowDialog();
			if(FormC.PinClicked) {
				SendToPinBoard(FormC.AptSelected);
			}
			if(FormC.SelectedPatNum!=0){
				RefreshModuleDataPatient(FormC.SelectedPatNum);
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(FormC.SelectedPatNum);
			}
			Cursor=Cursors.Default;
		}

		private void OnTrack_Click() {
			Cursor=Cursors.WaitCursor;
			FormTrackNext FormTN=new FormTrackNext();
			FormTN.ShowDialog();
			if(FormTN.PinClicked){
				SendToPinBoard(FormTN.AptSelected);
			}
			if(FormTN.SelectedPatNum!=0) {
				RefreshModuleDataPatient(FormTN.SelectedPatNum);
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(FormTN.SelectedPatNum);
			}
			Cursor=Cursors.Default;
		}

		private void OnLists_Click(){
			FormApptLists FormA=new FormApptLists();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.Cancel){
				return;
			}
			switch(FormA.SelectionResult){
				case ApptListSelection.Recall:
					OnRecall_Click();
					break;
				case ApptListSelection.Confirm:
					OnConfirm_Click();
					break;
				case ApptListSelection.Planned:
					OnTrack_Click();
					break;
				case ApptListSelection.Unsched:
					OnUnschedList_Click();
					break;
				case ApptListSelection.ASAP:
					OnASAPList_Click();
					break;
			}
		}

		private void OnPrint_Click() {
			if(PrinterSettings.InstalledPrinters.Count==0){
				MessageBox.Show(Lan.g(this,"Printer not installed"));
			}
			else{
				PrintReport();
			}
		}

		///<summary></summary>
		public void PrintReport(){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			//pd2.DefaultPageSettings.Margins= new Margins(10,40,40,60);
			#if DEBUG
				pView = new FormRpPrintPreview();
				pView.printPreviewControl2.Document=pd2;
  			pView.ShowDialog();
			#else
				if(!PrinterL.SetPrinter(pd2,PrintSituation.Appointments)){
					return;
				}
				try{
					pd2.Print();
				}
				catch{
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			#endif
	}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			//MessageBox.Show(e.PageSettings.PrinterSettings.Copies.ToString());
      int xPos=0;//starting pos
			int yPos=(int)27.5;//starting pos
      //Print Title
		
			string title;
			string date;
			if(ContrApptSheet.IsWeeklyView) {
				title=Lan.g(this,"Weekly Appointments");
				date=WeekStartDate.DayOfWeek.ToString()+" "+WeekStartDate.ToShortDateString()
					+" - "+WeekEndDate.DayOfWeek.ToString()+" "+WeekEndDate.ToShortDateString();
			}
			else {
				title=Lan.g(this,"Daily Appointments");
				date=AppointmentL.DateSelected.DayOfWeek.ToString()+"   "+AppointmentL.DateSelected.ToShortDateString();
			}
			Font titleFont=new Font("Arial",14,FontStyle.Bold);
			float xTitle = (float)(400-((e.Graphics.MeasureString(title,titleFont).Width/2)));
			e.Graphics.DrawString(title,titleFont,Brushes.Black,xTitle,yPos);//centered
			//Print Date
 			//string date = AppointmentL.DateSelected.DayOfWeek.ToString()+"   "
			//	+AppointmentL.DateSelected.ToShortDateString();
			Font dateFont=new Font("Arial",10,FontStyle.Regular);
			float xDate = (float)(400-((e.Graphics.MeasureString(date,dateFont).Width/2)));
			yPos+=25;
			e.Graphics.DrawString(date,dateFont,Brushes.Black,xDate,yPos);//centered
			//FIGURING OUT SIZE OF IMAGE
			int recHeight=0;
      int recWidth=0;
			int recX=0;
			int recY=0;
      ArrayList AListStart=new ArrayList();
      ArrayList AListStop=new ArrayList();
      DateTime StartTime;
      DateTime StopTime;  
      Rectangle imageRect;  //holds new dimensions for temp image
		  Bitmap imageTemp;  //clone of shadow image with correct dimensions depending on day of week. Needs to be rewritten.
      for(int i=0;i<SchedListPeriod.Count;i++){
				if(SchedListPeriod[i].SchedType!=ScheduleType.Provider){
					continue;
				}
				if(SchedListPeriod[i].StartTime.TimeOfDay==TimeSpan.FromHours(0)) {//ignore notes at midnight
					continue;
				}
        AListStart.Add(SchedListPeriod[i].StartTime);
        AListStop.Add(SchedListPeriod[i].StopTime);
      } 
			if(AListStart.Count > 0){//makes sure there is at least one timeblock
        StartTime=(DateTime)AListStart[0]; 
				for(int i=0;i<AListStart.Count;i++){
          //if (A) OR (B AND C)
					if((((DateTime)(AListStart[i])).Hour < StartTime.Hour) 
						|| (((DateTime)(AListStart[i])).Hour==StartTime.Hour 
						&& ((DateTime)(AListStart[i])).Minute < StartTime.Minute)){
            StartTime=(DateTime)AListStart[i];   
					}
				}
				StopTime=(DateTime)AListStop[0]; 
				for(int i=0;i<AListStop.Count;i++){
          //if (A) OR (B AND C)
					if((((DateTime)(AListStop[i])).Hour > StopTime.Hour) 
						|| (((DateTime)(AListStop[i])).Hour==StopTime.Hour 
						&& ((DateTime)(AListStop[i])).Minute > StopTime.Minute)){
            StopTime=(DateTime)AListStop[i];   
					}
				}
      }
      else{//office is closed
				StartTime=new DateTime(AppointmentL.DateSelected.Year,AppointmentL.DateSelected.Month
					,AppointmentL.DateSelected.Day
					,ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)
					,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
					,0);
				if(ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)+12<23){
					//we will be adding an extra hour later
					StopTime=new DateTime(AppointmentL.DateSelected.Year,AppointmentL.DateSelected.Month
						,AppointmentL.DateSelected.Day
						,ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)+12//add 12 hours
						,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
						,0);
				}
				else{
					StopTime=new DateTime(AppointmentL.DateSelected.Year,AppointmentL.DateSelected.Month
						,AppointmentL.DateSelected.Day
						,22
						,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
						,0);
				}
			}
			recY=(int)(ContrApptSheet.Lh*ContrApptSheet.RowsPerHr*StartTime.Hour);
			recWidth=(int)ContrApptSheet2.Shadow.Width;
			recHeight=(int)((ContrApptSheet.Lh*ContrApptSheet.RowsPerHr
				*(StopTime.Hour-StartTime.Hour+1)));
			//recHeight+=ContrApptSheet.Lh*6;
      imageRect = new Rectangle(recX,recY,recWidth,recHeight);
			imageTemp=ContrApptSheet2.Shadow.Clone(imageRect,PixelFormat.DontCare);  //clones image and sets size to only show the time open for that day
			int horRes=100;
			int vertRes=100;
			if(imageTemp.Width>775)  {
				horRes+=(int)((imageTemp.Width-775)/8);
				if((imageTemp.Width-750)%8!=0)
				  horRes+=1;
      }
	    if (imageTemp.Height>960)  {
        vertRes+=((imageTemp.Height-960)/8);
				if((imageTemp.Height-960)%8!=0)
				  vertRes+=1;
			}
      imageTemp.SetResolution(horRes,vertRes);  //sets resolution to fit image on screen
			//HEADER POSITION AND PRINTING		 
			string[] headers = new string[ContrApptSheet.ColCount];
			Font headerFont=new Font("Arial",8);
      yPos+=30;   //y Position  
			//need to size to horizontal resolution if bigger than 100
      xPos+=(int)(ContrApptSheet.TimeWidth+(ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount)*(100/imageTemp.HorizontalResolution));  // x position
			int xCenter=0;
			for(int i=0;i<ContrApptSheet.ColCount;i++){
				headers[i]=OperatoryC.ListShort[ApptViewItemL.VisOps[i]].OpName;	
				xCenter=(int)((ContrApptSheet.ColWidth/2)-(e.Graphics.MeasureString(headers[i],headerFont).Width/2));
			  e.Graphics.DrawString(headers[i],headerFont,Brushes.Black,(int)((xPos+xCenter)*(100/imageTemp.HorizontalResolution)),yPos);
        xPos+=ContrApptSheet.ColWidth;
			}   
			//DRAW IMAGE:
      xPos=0;
			yPos+=12;
      e.Graphics.DrawImage(imageTemp,xPos,yPos); 
		}

		///<summary>Clears the pinboard.</summary>
		private void butClearPin_Click(object sender, System.EventArgs e) {
			if(pinBoard.ApptList.Count==0) {
				MsgBox.Show(this,"There are no appointments on the pinboard to clear.");
				return;
			}
			if(pinBoard.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			DataRow row=pinBoard.ApptList[pinBoard.SelectedIndex].DataRoww;
			pinBoard.ClearSelected();
			ContrApptSingle.SelectedAptNum=-1;
			if(row["AptStatus"].ToString()==((int)ApptStatus.UnschedList).ToString()){//on unscheduled list
				//do nothing to database
			}
			else if(PIn.PDateT(row["AptDateTime"].ToString()).Year>1880){//already scheduled
				//do nothing to database
			}
			else if(row["AptStatus"].ToString()==((int)ApptStatus.Planned).ToString()){
				//do nothing except remove it from pinboard
			}
			else{//for normal appt:
				//this gets rid of new appointments that never made it off the pinboard
				Appointments.Delete(PIn.PLong(row["AptNum"].ToString()));
			}
			if(pinBoard.SelectedIndex==-1){
				RefreshModuleDataPatient(0);
				//RefreshModulePatient(0);
			}
			else{
				RefreshModuleDataPatient(PIn.PLong(pinBoard.ApptList[pinBoard.SelectedIndex].DataRoww["PatNum"].ToString()));
				OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				//RefreshModulePatient(PIn.PInt(pinBoard.ApptList[pinBoard.SelectedIndex].DataRoww["PatNum"].ToString()));
			}
		}

		///<summary>The scrollbar has been moved by the user.</summary>
		private void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			if(e.Type==ScrollEventType.ThumbTrack){//moving
				ContrApptSheet2.IsScrolling=true;
				ContrApptSheet2.Location=new Point(0,-e.NewValue);
			}
			if(e.Type==ScrollEventType.EndScroll){//done moving
				ContrApptSheet2.IsScrolling=true;
				ContrApptSheet2.Location=new Point(0,-e.NewValue);
				ContrApptSheet2.IsScrolling=false;
				ContrApptSheet2.Select();
			}			
		}

		///<summary>Occurs whenever the panel holding the appt sheet is resized.</summary>
		private void panelSheet_Resize(object sender, System.EventArgs e) {
			vScrollBar1.Maximum=ContrApptSheet2.Height-panelSheet.Height+vScrollBar1.LargeChange;
		}

		private void menuWeekly_Click(object sender,System.EventArgs e) {
			switch(((MenuItem)sender).Index) {
				case 0:
					OnCopyToPin_Click();
					break;
			}
		}

		private void menuApt_Click(object sender, System.EventArgs e) {
			switch(((MenuItem)sender).Index){
				case 0:
					OnCopyToPin_Click();
					break;
				//1: divider
				case 2:
					OnUnsched_Click();
					break;
				case 3:
					OnBreak_Click();
					break;
				case 4:
					OnComplete_Click();
					break;
				case 5:
					OnDelete_Click();
					break;
				case 6:
					DisplayOtherDlg(false);
					break;
				//7: divider
				case 8:
					PrintApptLabel();
					break;
				case 9:
					cardPrintFamily=false;
					PrintApptCard();
					break;
				case 10:
					cardPrintFamily=true;
					PrintApptCard();
					break;
				case 11:
					FormRpRouting FormR=new FormRpRouting();
					FormR.ApptNum=ContrApptSingle.ClickedAptNum;
					FormR.ShowDialog();
					break;
			}
		}

		private void menuBlockout_Click(object sender, System.EventArgs e) {
			switch(((MenuItem)sender).Index){
				case 0:
					OnBlockEdit_Click();
					break;
				case 1:
					OnBlockCut_Click();
					break;
				case 2:
					OnBlockCopy_Click();
					break;
				case 3:
					OnBlockPaste_Click();
					break;
				case 4:
					OnBlockDelete_Click();
					break;
				case 5:
					OnBlockAdd_Click();
					break;
				case 6:
					OnBlockCutCopyPaste_Click();
					break;
				case 7:
					OnClearBlockouts_Click();
					break;
				case 8:
					OnBlockTypes_Click();
					break;
			}
		}
		
		///<summary>Sends current appointment to unscheduled list.</summary>
		private void butUnsched_Click(object sender, System.EventArgs e) {
			OnUnsched_Click();
		}

		private void butBreak_Click(object sender, System.EventArgs e) {
			OnBreak_Click();
		}

		private void butComplete_Click(object sender, System.EventArgs e) {
			OnComplete_Click();
		}
	
		private void butDelete_Click(object sender, System.EventArgs e) {
			OnDelete_Click();
		}

		private void butOther_Click(object sender, System.EventArgs e) {
			DisplayOtherDlg(false);
		}

		private void OnUnsched_Click(){
			if(ContrApptSingle.SelectedAptNum==-1){
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			Appointment apt = Appointments.GetOneApt(ContrApptSingle.SelectedAptNum);
			if(!Security.IsAuthorized(Permissions.AppointmentMove)){
				return;
			}
			if (apt.AptStatus == ApptStatus.PtNote | apt.AptStatus == ApptStatus.PtNoteCompleted) {
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Send Appointment to Unscheduled List?")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			int thisI=GetIndex(ContrApptSingle.SelectedAptNum);
			if(thisI==-1) {//selected appt is on a different day
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			Appointments.SetAptStatus(ContrApptSingle.SelectedAptNum,ApptStatus.UnschedList);
			Patient pat=Patients.GetPat(PIn.PLong(ContrApptSingle3[thisI].DataRoww["PatNum"].ToString()));
			SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,pat.PatNum,
				pat.GetNameLF()+", "
				+ContrApptSingle3[thisI].DataRoww["procs"].ToString()+", "
				+ContrApptSingle3[thisI].DataRoww["AptDateTime"].ToString()+", "
				+"Sent to Unscheduled List");
			ModuleSelected(pat.PatNum);
			SetInvalid();
		}

		private void OnBreak_Click(){
			if(!PrefC.GetBool(PrefName.BrokenApptCommLogNotAdjustment) && PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType)==0){
				MsgBox.Show(this,"Broken appointment adjustment type is not setup yet.  Please go to Setup | Modules to fix this.");
				return;
			}
			int thisI=GetIndex(ContrApptSingle.SelectedAptNum);
			if(thisI==-1) {//selected appt is on a different day
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			Appointment apt = Appointments.GetOneApt(ContrApptSingle.SelectedAptNum);
			Patient pat=Patients.GetPat(PIn.PLong(ContrApptSingle3[thisI].DataRoww["PatNum"].ToString()));
			if(!Security.IsAuthorized(Permissions.AppointmentEdit)) {
				return;
			}
			if(apt.AptStatus == ApptStatus.PtNote || apt.AptStatus == ApptStatus.PtNoteCompleted) {
				MsgBox.Show(this,"Only appointments may be broken, not notes.");
				return;
			}
			if(PrefC.GetBool(PrefName.BrokenApptCommLogNotAdjustment)){
				if(!MsgBox.Show(this,true,"Break appointment?")){
					return;
				}
			}
			Appointments.SetAptStatus(ContrApptSingle.SelectedAptNum,ApptStatus.Broken);
			SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,pat.PatNum,
				pat.GetNameLF()+", "
				+ContrApptSingle3[thisI].DataRoww["procs"].ToString()+", "
				+ContrApptSingle3[thisI].DataRoww["AptDateTime"].ToString()+", "
				+"Broke");
			long provNum=PIn.PLong(ContrApptSingle3[thisI].DataRoww["ProvNum"].ToString());//remember before ModuleSelected
			ModuleSelected(pat.PatNum);
			SetInvalid();		
			if(PrefC.GetBool(PrefName.BrokenApptCommLogNotAdjustment)){
				Commlog CommlogCur=new Commlog();
				CommlogCur.PatNum=pat.PatNum;
				CommlogCur.CommDateTime=DateTime.Now;
				CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
				CommlogCur.Note=Lan.g(this,"Appt BROKEN for ")+apt.ProcDescript+"  "+apt.AptDateTime.ToString();
				CommlogCur.Mode_=CommItemMode.None;
				CommlogCur.UserNum=Security.CurUser.UserNum;
				FormCommItem FormCI=new FormCommItem(CommlogCur);
				FormCI.IsNew=true;
				FormCI.ShowDialog();
			}
			else {
				Adjustment AdjustmentCur=new Adjustment();
				AdjustmentCur.DateEntry=DateTime.Today;
				AdjustmentCur.AdjDate=DateTime.Today;
				AdjustmentCur.ProcDate=DateTime.Today;
				AdjustmentCur.ProvNum=provNum;
				AdjustmentCur.PatNum=pat.PatNum;
				AdjustmentCur.AdjType=PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType);
				FormAdjust FormA=new FormAdjust(pat,AdjustmentCur);
				FormA.IsNew=true;
				FormA.ShowDialog();
			}
		}

		private void OnComplete_Click(){
			if(!Security.IsAuthorized(Permissions.AppointmentEdit)){
				return;
			}
			int thisI=GetIndex(ContrApptSingle.SelectedAptNum);
			if(thisI==-1) {//selected appt is on a different day
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			Appointment apt = Appointments.GetOneApt(ContrApptSingle.SelectedAptNum);
			if(apt.AptStatus == ApptStatus.PtNoteCompleted) {
				return;
			}
			if(!Security.IsAuthorized(Permissions.ProcComplCreate,apt.AptDateTime)){
				return;
			}
			//Procedures.SetDateFirstVisit(Appointments.Cur.AptDateTime.Date);//done when making appt instead
			Family fam = Patients.GetFamily(apt.PatNum);
			Patient pat = fam.GetPatient(apt.PatNum);
			List<InsPlan> PlanList = InsPlans.Refresh(fam);
			List<PatPlan> PatPlanList = PatPlans.Refresh(apt.PatNum);
			if (apt.AptStatus == ApptStatus.PtNote) {
				Appointments.SetAptStatus(apt.AptNum,ApptStatus.PtNoteCompleted);
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,apt.PatNum,
					pat.GetNameLF() + ", "
					+ apt.AptDateTime.ToString() + ", "
					+ "Pt NOTE Set Complete");//shouldn't ever happen, but don't allow procedures to be completed from notes
			}
			else {
				Appointments.SetAptStatusComplete(apt.AptNum,PatPlans.GetPlanNum(PatPlanList,1),PatPlans.GetPlanNum(PatPlanList,2));
				Procedures.SetCompleteInAppt(apt, PlanList, PatPlanList,pat.SiteNum,pat.Age);//loops through each proc
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit, apt.PatNum,
					pat.GetNameLF() + ", "
					+ ContrApptSingle3[GetIndex(apt.AptNum)].DataRoww["procs"].ToString() + ", "
					+ apt.AptDateTime.ToString() + ", "
					+ "Set Complete");
			}
			ModuleSelected(pat.PatNum);
			SetInvalid();
			SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,pat.PatNum,
				pat.GetNameLF()+" "+apt.AptDateTime.ToShortDateString());
		}

		private void OnDelete_Click(){
			long selectedAptNum=ContrApptSingle.SelectedAptNum;
			Appointment apt = Appointments.GetOneApt(selectedAptNum);
			if (!Security.IsAuthorized(Permissions.AppointmentEdit)) {
				return;
			}
			int thisI=GetIndex(selectedAptNum);
			if(thisI==-1) {//selected appt is on a different day
				MsgBox.Show(this,"Please select an appointment first.");
				return;
			}
			if (apt.AptStatus == ApptStatus.PtNote | apt.AptStatus == ApptStatus.PtNoteCompleted) {
				if (!MsgBox.Show(this, true, "Delete Patient Note?")) {
					return;
				}
				if (apt.Note != "") {
					if (MessageBox.Show(Lan.g(this, "Save a copy of this note in CommLog? ") + "\r\n" + "\r\n" + apt.Note, "Question...",
							MessageBoxButtons.YesNo) == DialogResult.Yes) {
						Commlog CommlogCur = new Commlog();
						CommlogCur.PatNum = apt.PatNum;
						CommlogCur.CommDateTime = apt.AptDateTime;
						CommlogCur.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note = "Deleted Pt NOTE from schedule, saved copy: ";
						CommlogCur.Note += apt.Note;
						//there is no dialog here because it is just a simple entry
						Commlogs.Insert(CommlogCur);
					}
				}
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit, PatCur.PatNum,
					PatCur.GetNameFL() + ", "
					+ ContrApptSingle3[thisI].DataRoww["procs"].ToString() + ", "
					+ ContrApptSingle3[thisI].DataRoww["AptDateTime"].ToString() + ", "
					+ "NOTE Deleted");
			}
			else {
				if (!MsgBox.Show(this, true, "Delete Appointment?")) {
					return;
				}
				if (apt.Note != "") {
					if (MessageBox.Show(Lan.g(this, "Save appointment note in CommLog? ") + "\r\n" + "\r\n" + apt.Note, "Question...",
							MessageBoxButtons.YesNo) == DialogResult.Yes){
						Commlog CommlogCur = new Commlog();
						CommlogCur.PatNum = apt.PatNum;
						CommlogCur.CommDateTime = apt.AptDateTime;
						CommlogCur.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.APPT);
						CommlogCur.Note = "Deleted Appointment & saved note: ";
						if (apt.ProcDescript != "") {
							CommlogCur.Note += apt.ProcDescript + ": ";
						}
						CommlogCur.Note += apt.Note;
						//there is no dialog here because it is just a simple entry
						Commlogs.Insert(CommlogCur);
					}
				}
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit, PatCur.PatNum,
					PatCur.GetNameFL() + ", "
					+ ContrApptSingle3[thisI].DataRoww["procs"].ToString() + ", "
					+ ContrApptSingle3[thisI].DataRoww["AptDateTime"].ToString() + ", "
					+ "Deleted");
			}
			Appointments.Delete(selectedAptNum);
			ContrApptSingle.SelectedAptNum=-1;
			pinBoard.SelectedIndex=-1;
			DataRow row;
			for(int i=0;i<pinBoard.ApptList.Count;i++) {
				row=pinBoard.ApptList[i].DataRoww;
				if(selectedAptNum.ToString()==row["AptNum"].ToString()) {
					pinBoard.SelectedIndex=i;
					pinBoard.ClearSelected();
					pinBoard.SelectedIndex=-1;
				}
			}
			//ContrApptSingle.PinBoardIsSelected=false;
			//PatCurNum=0;
			ModuleSelected(0);
			SetInvalid();
		}		

		private void PrintApptLabel(){
			LabelSingle.PrintAppointment(ContrApptSingle.SelectedAptNum);
		}

		private void OnBlockCopy_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)) {
				return;
			}
			//not even enabled if not right click on a blockout
			Schedule SchedCur=GetClickedBlockout();
			if(SchedCur==null) {
				MessageBox.Show("Blockout not found.");
				return;//should never happen
			}
			BlockoutClipboard=SchedCur.Copy();
		}

		private void OnBlockCut_Click() {
			if(!Security.IsAuthorized(Permissions.Blockouts)) {
				return;
			}
			//not even enabled if not right click on a blockout
			Schedule SchedCur=GetClickedBlockout();
			if(SchedCur==null) {
				MessageBox.Show("Blockout not found.");
				return;//should never happen
			}
			BlockoutClipboard=SchedCur.Copy();
			Schedules.Delete(SchedCur);
			RefreshPeriod();
		}

		private void OnBlockPaste_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)) {
				return;
			}
			Schedule sched=BlockoutClipboard.Copy();
			sched.Ops=new List<long>();
			sched.Ops.Add(SheetClickedonOp);
			sched.SchedDate=AppointmentL.DateSelected;
			if(ContrApptSheet.IsWeeklyView){
				sched.SchedDate=WeekStartDate.AddDays(SheetClickedonDay);
			}
			TimeSpan span=sched.StopTime-sched.StartTime;
			TimeSpan timeOfDay=new TimeSpan(SheetClickedonHour,SheetClickedonMin,0);
			timeOfDay=TimeSpan.FromMinutes(
				((int)Math.Round((decimal)timeOfDay.TotalMinutes/(decimal)ContrApptSheet.MinPerIncr))*ContrApptSheet.MinPerIncr);
			sched.StartTime=DateTime.Today+timeOfDay;
			sched.StopTime=sched.StartTime.Add(span);
			if(sched.StopTime.Date!=sched.StartTime.Date) {//long span that spills over to next day
				sched.StopTime=DateTime.Today+(new TimeSpan(23,59,0));
			}
			Schedules.Insert(sched);
			RefreshPeriod();
		}

		private void OnBlockEdit_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			//not even enabled if not right click on a blockout
			Schedule SchedCur=GetClickedBlockout();
			if(SchedCur==null){
				MessageBox.Show("Blockout not found.");
				return;//should never happen
			}
			FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,0,"Blockout edit.");
			RefreshPeriod();
		}

		private Schedule GetClickedBlockout(){
			DateTime startDate;
			DateTime endDate;
			if(ContrApptSheet.IsWeeklyView) {
				startDate=WeekStartDate;
				endDate=WeekEndDate;
			}
			else {
				startDate=AppointmentL.DateSelected;
				endDate=AppointmentL.DateSelected;
			}
			//no need to do this since schedule is refreshed in RefreshPeriod().
			//SchedListPeriod=Schedules.RefreshPeriod(startDate,endDate);
			Schedule[] ListForType=Schedules.GetForType(SchedListPeriod,ScheduleType.Blockout,0);
			//now find which blockout
			Schedule SchedCur=null;
			//date is irrelevant. This is just for the time:
			DateTime SheetClickedonTime=new DateTime(2000,1,1,SheetClickedonHour,SheetClickedonMin,0);
			for(int i=0;i<ListForType.Length;i++) {
				//skip if op doesn't match
				if(!ListForType[i].Ops.Contains(SheetClickedonOp)){
					continue;
				}
				if(ListForType[i].SchedDate.Date!=WeekStartDate.AddDays(SheetClickedonDay)){
					continue;
				}
				if(ListForType[i].StartTime.TimeOfDay <= SheetClickedonTime.TimeOfDay
					&& SheetClickedonTime.TimeOfDay < ListForType[i].StopTime.TimeOfDay) {
					SchedCur=ListForType[i];
					break;
				}
			}
			return SchedCur;//might be null;
		}

		private void OnBlockDelete_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			Schedule SchedCur=GetClickedBlockout();
			if(SchedCur==null) {
				MessageBox.Show("Blockout not found.");
				return;//should never happen
			}
			Schedules.Delete(SchedCur);
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,0,"Blockout delete.");
			RefreshPeriod();
		}

		private void OnBlockAdd_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
      Schedule SchedCur=new Schedule();
      SchedCur.SchedDate=AppointmentL.DateSelected;
			if(ContrApptSheet.IsWeeklyView) {
				SchedCur.SchedDate=WeekStartDate.AddDays(SheetClickedonDay);
			}
			SchedCur.SchedType=ScheduleType.Blockout;
		  FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.IsNew=true;
      FormSB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,0,"Blockout add.");
			RefreshPeriod();
		}

		private void OnBlockCutCopyPaste_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			FormBlockoutCutCopyPaste FormB=new FormBlockoutCutCopyPaste();
			FormB.DateSelected=AppointmentL.DateSelected;
			if(ContrApptSheet.IsWeeklyView) {
				FormB.DateSelected=WeekStartDate.AddDays(SheetClickedonDay);
			}
			if(comboView.SelectedIndex==0){
				FormB.ApptViewNumCur=0;
			}
			else{
				FormB.ApptViewNumCur=ApptViewC.List[comboView.SelectedIndex-1].ApptViewNum;
			}
			FormB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,0,"Blockout cut copy paste.");
			RefreshPeriod();
		}

		private void OnClearBlockouts_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			if(!MsgBox.Show(this,true,"Clear all blockouts for day?")){
				return;
			}
			if(ContrApptSheet.IsWeeklyView) {
				Schedules.ClearBlockoutsForDay(WeekStartDate.AddDays(SheetClickedonDay));
			}
			else{
				Schedules.ClearBlockoutsForDay(AppointmentL.DateSelected);
			}
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,0,"Blockout clear.");
			RefreshPeriod();
		}

		private void OnBlockTypes_Click(){
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDefinitions FormD=new FormDefinitions(DefCat.BlockoutTypes);
			FormD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Definitions.");
			RefreshPeriod();
		}

		private void OnCopyToPin_Click() {
			if(!Security.IsAuthorized(Permissions.AppointmentMove)) {
				return;
			}
			//cannot allow moving completed procedure because it could cause completed procs to change date.  Security must block this.
			//ContrApptSingle3[thisIndex].DataRoww;
			Appointment appt=Appointments.GetOneApt(ContrApptSingle.SelectedAptNum);
			if(appt==null){
				MsgBox.Show(this,"Appointment not found.");
				return;
			}
			if(appt.AptStatus==ApptStatus.Complete){
				MsgBox.Show(this,"Not allowed to move completed appointments.");
				return;
			}
			int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
			SendToPinBoard(ContrApptSingle.SelectedAptNum);//sets selectedAptNum=-1. do before refresh prev
			if(prevSel!=-1) {
				CreateAptShadows();
				ContrApptSheet2.DrawShadow();
			}
			//RefreshModulePatient(PatCurNum);
			//RefreshPeriod();
		}

		private void listConfirmed_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listConfirmed.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			if(ContrApptSingle.SelectedAptNum==-1) {
				return;
			}
			long newStatus=DefC.Short[(int)DefCat.ApptConfirmed][listConfirmed.IndexFromPoint(e.X,e.Y)].DefNum;
			Appointments.SetConfirmed(ContrApptSingle.SelectedAptNum,newStatus);
			RefreshPeriod();
			SetInvalid();
		}

		private void butSearch_Click(object sender, System.EventArgs e) {
			if(pinBoard.ApptList.Count==0){
				MsgBox.Show(this,"An appointment must be placed on the pinboard before a search can be done.");
				return;
			}
			if(pinBoard.SelectedIndex==-1){
				if(pinBoard.ApptList.Count==1){
					pinBoard.SelectedIndex=0;
				}
				else{
					MsgBox.Show(this,"An appointment on the pinboard must be selected before a search can be done.");
					return;
				}
			}
			if(!groupSearch.Visible){//if search not already visible
				dateSearch.Text=DateTime.Today.ToShortDateString();
				ShowSearch();
			}
			DoSearch();
		}

		///<summary>Positions the search box, fills it with initial data except date, and makes it visible.</summary>
		private void ShowSearch(){
			groupSearch.Location=new Point(panelCalendar.Location.X,panelCalendar.Location.Y+pinBoard.Bottom+2);
			//if(!groupSearch.Visible){//if search not already visible, 
			textBefore.Text="";
			textAfter.Text="";
			listProviders.Items.Clear();
			for(int i=0;i<ProviderC.List.Length;i++){
				listProviders.Items.Add(ProviderC.List[i].Abbr);
				if(pinBoard.SelectedAppt.DataRoww["IsHygiene"].ToString()=="1"
					&& ProviderC.List[i].ProvNum.ToString()==pinBoard.SelectedAppt.DataRoww["ProvHyg"].ToString())
				{
					listProviders.SetSelected(i,true);
				}
				else if(pinBoard.SelectedAppt.DataRoww["IsHygiene"].ToString()=="0"
					&& ProviderC.List[i].ProvNum.ToString()==pinBoard.SelectedAppt.DataRoww["ProvNum"].ToString())
				{
					listProviders.SelectedIndex=i;
				}
			}
			//}
			groupSearch.Visible=true;
		}

		private void DoSearch(){
			Cursor=Cursors.WaitCursor;
			DateTime afterDate;
			try{
				afterDate=PIn.PDate(dateSearch.Text);
				if(afterDate.Year<1880){
					throw new Exception();
				}
			}
			catch{
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Invalid date.");
				return;
			}
			TimeSpan beforeTime=new TimeSpan(0);
			if(textBefore.Text!=""){
				try{
					string[] hrmin=textBefore.Text.Split(new char[] {':'},StringSplitOptions.RemoveEmptyEntries);//doesn't work with foreign times.
					string hr="0";
					if(hrmin.Length>0){
						hr=hrmin[0];
					}
					string min="0";
					if(hrmin.Length>1) {
						min=hrmin[1];
					}
					beforeTime=TimeSpan.FromHours(PIn.PDouble(hr))
						+TimeSpan.FromMinutes(PIn.PDouble(min));
					if(radioBeforePM.Checked && beforeTime.Hours<12){
						beforeTime=beforeTime+TimeSpan.FromHours(12);
					}
				}
				catch{
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Invalid time.");
					return;
				}
			}
			TimeSpan afterTime=new TimeSpan(0);
			if(textAfter.Text!=""){
				try{
					string[] hrmin=textAfter.Text.Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);//doesn't work with foreign times.
					string hr="0";
					if(hrmin.Length>0) {
						hr=hrmin[0];
					}
					string min="0";
					if(hrmin.Length>1) {
						min=hrmin[1];
					}
					afterTime=TimeSpan.FromHours(PIn.PDouble(hr))
						+TimeSpan.FromMinutes(PIn.PDouble(min));
					if(radioAfterPM.Checked && afterTime.Hours<12){
						afterTime=afterTime+TimeSpan.FromHours(12);
					}
				}
				catch{
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Invalid time.");
					return;
				}
			}
			if(listProviders.SelectedIndices.Count==0){
				Cursor=Cursors.Default;
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			long[] providers=new long[listProviders.SelectedIndices.Count];
			for(int i=0;i<providers.Length;i++){
				providers[i]=ProviderC.List[listProviders.SelectedIndices[i]].ProvNum;
			}
			//the result might be empty
			SearchResults=AppointmentL.GetSearchResults(PIn.PLong(pinBoard.SelectedAppt.DataRoww["AptNum"].ToString()),
				afterDate,providers,10,beforeTime,afterTime);
			listSearchResults.Items.Clear();
			for(int i=0;i<SearchResults.Length;i++){
				listSearchResults.Items.Add(
					SearchResults[i].ToString("ddd")+"\t"+SearchResults[i].ToShortDateString()+"     "+SearchResults[i].ToShortTimeString());
			}
			if(listSearchResults.Items.Count>0){
				listSearchResults.SetSelected(0,true);
				AppointmentL.DateSelected=SearchResults[0];
			}
			SetWeeklyView(false);//jump to that day.
			Cursor=Cursors.Default;
			//scroll to make visible?
			//highlight schedule?*/
		}

		private void butSearchMore_Click(object sender, System.EventArgs e) {
			if(pinBoard.SelectedAppt==null){
				MsgBox.Show(this,"There is no appointment on the pinboard.");
				return;
			}
			dateSearch.Text=SearchResults[SearchResults.Length-1].ToShortDateString();
			DoSearch();
		}

		private void listSearchResults_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			int clickedI=listSearchResults.IndexFromPoint(e.X,e.Y);
			if(clickedI==-1){
				return;
			}
			AppointmentL.DateSelected=SearchResults[clickedI];
			SetWeeklyView(false);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(pinBoard.SelectedAppt==null){
				MsgBox.Show(this,"There is no appointment on the pinboard.");
				return;
			}
			DoSearch();
		}

		private void butSearchCloseX_Click(object sender, System.EventArgs e) {
			groupSearch.Visible=false;
		}

		private void butSearchClose_Click(object sender, System.EventArgs e) {
			groupSearch.Visible=false;
		}

		private void button1_Click_1(object sender, System.EventArgs e) {
			MessageBox.Show(Lan.g(this,this.GetType().Name));
		}

		private void butLab_Click(object sender,EventArgs e) {
			FormLabCases FormL=new FormLabCases();
			FormL.ShowDialog();
			if(FormL.GoToAptNum!=0) {
				Appointment apt=Appointments.GetOneApt(FormL.GoToAptNum);
				Patient pat=Patients.GetPat(apt.PatNum);
				//PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(pat.PatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber);
				//if(PatientSelected!=null){
				//	PatientSelected(this,eArgs);
				//}
				//Contr_PatientSelected(this,eArgs);
				OnPatientSelected(pat.PatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber);
				GotoModule.GotoAppointment(apt.AptDateTime,apt.AptNum);
			}
		}

		///<summary>Happens once per minute.  It used to just move the red timebar down without querying the database.  But now it queries the database so that the waiting room list shows accurately.</summary>
		public void TickRefresh(){
			try{
				DateTime startDate;
				DateTime endDate;
				if(ContrApptSheet.IsWeeklyView) {
					startDate=WeekStartDate;
					endDate=WeekEndDate;
				}
				else {
					startDate=AppointmentL.DateSelected;
					endDate=AppointmentL.DateSelected;
				}
				if(PrefC.GetBool(PrefName.ApptModuleRefreshesEveryMinute)){
					RefreshPeriod();
				}
				else{
					ContrApptSheet2.CreateShadow();
					CreateAptShadows();
					ContrApptSheet2.DrawShadow();
				}
			}
			catch{
				//prevents rare malfunctions. For instance, during editing of views, if tickrefresh happens.
			}
			//GC.Collect();	
		}
		
		///<summary>"Ganga's Code: Printing the Appointment Card - 9/9/2004"</summary>
		private void PrintApptCard(){
			pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintApptCard);
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;//forces origin to upper left of actual page
			if(PrinterL.SetPrinter(pd2,PrintSituation.Postcard)){
				pd2.Print();
			}
		}
			
		private void pd2_PrintApptCard(object sender, PrintPageEventArgs ev){
			Graphics g=ev.Graphics;
			//Return Address--------------------------------------------------------------------------
			string str=PrefC.GetString(PrefName.PracticeTitle)+"\r\n";
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold),Brushes.Black,60,60);
			str=PrefC.GetString(PrefName.PracticeAddress)+"\r\n";
			if(PrefC.GetString(PrefName.PracticeAddress2)!=""){
				str+=PrefC.GetString(PrefName.PracticeAddress2)+"\r\n";
			}
			str+=PrefC.GetString(PrefName.PracticeCity)+"  "
				+PrefC.GetString(PrefName.PracticeST)+"  "
				+PrefC.GetString(PrefName.PracticeZip)+"\r\n";
			string phone=PrefC.GetString(PrefName.PracticePhone);
			if(CultureInfo.CurrentCulture.Name=="en-US"
				&& phone.Length==10)
			{
				str+="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
			}
			else{//any other phone format
				str+=phone;
			}
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,8),Brushes.Black,60,75);
			//Body text-------------------------------------------------------------------------------
			string name;
			str="Appointment Reminders:"+"\r\n\r\n";
			Appointment[] aptsOnePat;
			Family fam=Patients.GetFamily(PatCur.PatNum);
			Patient pat=fam.GetPatient(PatCur.PatNum);
			for(int i=0;i<fam.ListPats.Length;i++){
				if(!cardPrintFamily && fam.ListPats[i].PatNum!=pat.PatNum){
					continue;
				}
				name=fam.ListPats[i].FName;
				if(name.Length>15){//trim name so it won't be too long
					name=name.Substring(0,15);
				}
				aptsOnePat=Appointments.GetForPat(fam.ListPats[i].PatNum);
				for(int a=0;a<aptsOnePat.Length;a++){
					if(aptsOnePat[a].AptDateTime.Date<=DateTime.Today){
						continue;//ignore old appts
					}
					str+=name+": "+aptsOnePat[a].AptDateTime.ToString()+"\r\n";
				}
			}
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,9),Brushes.Black,40,180);
			//Patient's Address-----------------------------------------------------------------------
			Patient guar;
			if(cardPrintFamily){
				guar=fam.ListPats[0].Copy();
			}
			else{
				guar=pat.Copy();
			}
			str=guar.FName+" "+guar.LName+"\r\n"
				+guar.Address+"\r\n";
			if(guar.Address2!=""){
				str+=guar.Address2+"\r\n";
			}
			str+=guar.City+"  "+guar.State+"  "+guar.Zip;
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,300,240);		
			//CommLog entry---------------------------------------------------------------------------
			Commlog CommlogCur=new Commlog();
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
			CommlogCur.Note="Appointment card sent";
			CommlogCur.PatNum=pat.PatNum;
			//there is no dialog here because it is just a simple entry
			Commlogs.Insert(CommlogCur);
			ev.HasMorePages = false;
		}

		private void timerInfoBubble_Tick(object sender,EventArgs e) {
			InfoBubbleDraw(bubbleLocation);
			timerInfoBubble.Enabled =false;
		}

		private void butMonth_Click(object sender,EventArgs e) {
			FormMonthView FormM=new FormMonthView();
			FormM.ShowDialog();
		}

		private void timerWaitingRoom_Tick(object sender,EventArgs e) {
			FillWaitingRoom();
		}

		

		

		

		

		

		

		

		

		

		
		

		

	


		

	}

	


}













