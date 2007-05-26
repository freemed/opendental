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
		private ContrApptSingle PinApptSingle;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.PictureBox pictureBox1;
		private bool boolAptMoved=false;
		private OpenDental.UI.Button butToday;
		private OpenDental.UI.Button butTodayWk;
		private System.Windows.Forms.Panel panelPinBoard;
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
		private System.Windows.Forms.ImageList imageList1;
		///<summary></summary>
		public static int SheetClickedonOp;
		///<summary></summary>
		public static int SheetClickedonHour;
		///<summary></summary>
		public static int SheetClickedonMin;
		//<summary></summary>
		//public static InfoApt CurInfo;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary></summary>
		public static Size PinboardSize=new Size(106,92);
		private OpenDental.UI.Button butBack;
		private OpenDental.UI.Button butClearPin;
		private OpenDental.UI.Button butBackWk;
		private OpenDental.UI.Button butFwdWk;
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
	  public FormRpPrintPreview pView = new FormRpPrintPreview();
		private OpenDental.UI.Button butOther;
		private bool cardPrintFamily;
		//private Patient PatCur;
		//private Family FamCur;
		private System.Windows.Forms.ContextMenu menuApt;
		private System.Windows.Forms.ContextMenu menuBlockout;
		private System.Windows.Forms.ContextMenu menuWeeklyApt;
		//private InsPlan[] PlanList;
		private Schedule[] SchedListDay;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		//<summary>The list of appointments for one day, whether hidden or not</summary>
		//private Appointment[] ListDay;
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
		//private Appointment AptCur;
		//<Summary>This is replacing AptCur as the way of tracking which appointment is clicked.</Summary>
		//private int AptNumCur;
		//private Appointment AptOld;
		private DateTime[] SearchResults;
		//private PatPlan[] PatPlanList;
		private OpenDental.UI.Button butRefresh;
		//<summary>During RefreshDay, this is filled with all procedures for all appointments for the day.  The list is then used for various display purposes.</summary>
		//private Procedure[] procsMultApts;
		private bool ResizingAppt;
		private int ResizingOrigH;
		//private bool isWeeklyView;
		private DateTime WeekStartDate;
		private DateTime WeekEndDate;
		public static int numOfWeekDaysToDisplay=5;
		private OpenDental.UI.Button butLab;
		public static int SheetClickedonDay;
		///<summary></summary>
		private Panel infoBubble;
		///<Summary>The datatable that holds the bubble data.  This will later become part of the dataset for the main screen.</Summary>
		private DataSet DS;
		///<summary>If the user has done a blockout/copy, then this will contain the blockout that is on the "clipboard".</summary>
		private Schedule BlockoutClipboard;
		//<Summary>This is the bitmap that is used to layout all the data for the bubble.  It's recreated each time a bubble is first made visible or whenever the aptNum changes.</Summary>
		//private Bitmap bubbleBitmap;
		///<Summary>This has to be tracked globally because mouse might move directly from one appt to another without any break.  This is the only way to know if we are still over the same appt.</Summary>
		private int bubbleAptNum;
		private ODGrid gridEmpSched;
		private OpenDental.UI.PictureBox PicturePat;
		private string PatCurName;
		private int PatCurNum;
		private string PatCurChartNumber;

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
			this.panelPinBoard = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.Calendar2 = new System.Windows.Forms.MonthCalendar();
			this.labelDate = new System.Windows.Forms.Label();
			this.labelDate2 = new System.Windows.Forms.Label();
			this.panelArrows = new System.Windows.Forms.Panel();
			this.butTodayWk = new OpenDental.UI.Button();
			this.butToday = new OpenDental.UI.Button();
			this.butBack = new OpenDental.UI.Button();
			this.butFwd = new OpenDental.UI.Button();
			this.butBackWk = new OpenDental.UI.Button();
			this.butFwdWk = new OpenDental.UI.Button();
			this.panelSheet = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.ContrApptSheet2 = new OpenDental.ContrApptSheet();
			this.panelAptInfo = new System.Windows.Forms.Panel();
			this.listConfirmed = new System.Windows.Forms.ListBox();
			this.butComplete = new System.Windows.Forms.Button();
			this.butUnsched = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butBreak = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panelCalendar = new System.Windows.Forms.Panel();
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
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
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
			this.butOther = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.gridEmpSched = new OpenDental.UI.ODGrid();
			this.panelPinBoard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panelArrows.SuspendLayout();
			this.panelSheet.SuspendLayout();
			this.panelAptInfo.SuspendLayout();
			this.panelCalendar.SuspendLayout();
			this.groupSearch.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelPinBoard
			// 
			this.panelPinBoard.BackColor = System.Drawing.SystemColors.Window;
			this.panelPinBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelPinBoard.Controls.Add(this.pictureBox1);
			this.panelPinBoard.Location = new System.Drawing.Point(101,187);
			this.panelPinBoard.Name = "panelPinBoard";
			this.panelPinBoard.Size = new System.Drawing.Size(101,98);
			this.panelPinBoard.TabIndex = 6;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(80,2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(20,20);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"");
			this.imageListMain.Images.SetKeyName(1,"");
			this.imageListMain.Images.SetKeyName(2,"");
			// 
			// Calendar2
			// 
			this.Calendar2.Location = new System.Drawing.Point(2,24);
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
			this.panelArrows.Controls.Add(this.butTodayWk);
			this.panelArrows.Controls.Add(this.butToday);
			this.panelArrows.Controls.Add(this.butBack);
			this.panelArrows.Controls.Add(this.butFwd);
			this.panelArrows.Controls.Add(this.butBackWk);
			this.panelArrows.Controls.Add(this.butFwdWk);
			this.panelArrows.Location = new System.Drawing.Point(2,189);
			this.panelArrows.Name = "panelArrows";
			this.panelArrows.Size = new System.Drawing.Size(100,45);
			this.panelArrows.TabIndex = 32;
			// 
			// butTodayWk
			// 
			this.butTodayWk.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTodayWk.Autosize = false;
			this.butTodayWk.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTodayWk.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTodayWk.CornerRadius = 4F;
			this.butTodayWk.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butTodayWk.Location = new System.Drawing.Point(17,22);
			this.butTodayWk.Name = "butTodayWk";
			this.butTodayWk.Size = new System.Drawing.Size(65,22);
			this.butTodayWk.TabIndex = 31;
			this.butTodayWk.Text = "Week";
			this.butTodayWk.Click += new System.EventHandler(this.butTodayWk_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = false;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butToday.Location = new System.Drawing.Point(17,0);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(65,22);
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
			this.butBack.Image = ((System.Drawing.Image)(resources.GetObject("butBack.Image")));
			this.butBack.Location = new System.Drawing.Point(-1,0);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(19,22);
			this.butBack.TabIndex = 51;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFwd.Autosize = true;
			this.butFwd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwd.CornerRadius = 4F;
			this.butFwd.Image = ((System.Drawing.Image)(resources.GetObject("butFwd.Image")));
			this.butFwd.Location = new System.Drawing.Point(81,0);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(19,22);
			this.butFwd.TabIndex = 53;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butBackWk
			// 
			this.butBackWk.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBackWk.Autosize = true;
			this.butBackWk.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackWk.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackWk.CornerRadius = 4F;
			this.butBackWk.Image = ((System.Drawing.Image)(resources.GetObject("butBackWk.Image")));
			this.butBackWk.Location = new System.Drawing.Point(-1,22);
			this.butBackWk.Name = "butBackWk";
			this.butBackWk.Size = new System.Drawing.Size(19,22);
			this.butBackWk.TabIndex = 51;
			this.butBackWk.Click += new System.EventHandler(this.butBackWk_Click);
			// 
			// butFwdWk
			// 
			this.butFwdWk.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFwdWk.Autosize = true;
			this.butFwdWk.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFwdWk.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFwdWk.CornerRadius = 4F;
			this.butFwdWk.Image = ((System.Drawing.Image)(resources.GetObject("butFwdWk.Image")));
			this.butFwdWk.Location = new System.Drawing.Point(81,22);
			this.butFwdWk.Name = "butFwdWk";
			this.butFwdWk.Size = new System.Drawing.Size(19,22);
			this.butFwdWk.TabIndex = 52;
			this.butFwdWk.Click += new System.EventHandler(this.butFwdWk_Click);
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
			this.ContrApptSheet2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseDown);
			this.ContrApptSheet2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseMove);
			this.ContrApptSheet2.MouseLeave += new System.EventHandler(this.ContrApptSheet2_MouseLeave);
			this.ContrApptSheet2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet2_MouseUp);
			// 
			// panelAptInfo
			// 
			this.panelAptInfo.Controls.Add(this.listConfirmed);
			this.panelAptInfo.Controls.Add(this.butComplete);
			this.panelAptInfo.Controls.Add(this.butUnsched);
			this.panelAptInfo.Controls.Add(this.butDelete);
			this.panelAptInfo.Controls.Add(this.butBreak);
			this.panelAptInfo.Location = new System.Drawing.Point(665,379);
			this.panelAptInfo.Name = "panelAptInfo";
			this.panelAptInfo.Size = new System.Drawing.Size(219,116);
			this.panelAptInfo.TabIndex = 45;
			// 
			// listConfirmed
			// 
			this.listConfirmed.IntegralHeight = false;
			this.listConfirmed.Location = new System.Drawing.Point(126,2);
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
			this.butBreak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBreak.UseVisualStyleBackColor = false;
			this.butBreak.Click += new System.EventHandler(this.butBreak_Click);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(22,22);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panelCalendar
			// 
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
			this.panelCalendar.Controls.Add(this.panelPinBoard);
			this.panelCalendar.Controls.Add(this.panelArrows);
			this.panelCalendar.Location = new System.Drawing.Point(665,28);
			this.panelCalendar.Name = "panelCalendar";
			this.panelCalendar.Size = new System.Drawing.Size(219,351);
			this.panelCalendar.TabIndex = 46;
			// 
			// butLab
			// 
			this.butLab.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butLab.Autosize = true;
			this.butLab.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLab.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLab.CornerRadius = 4F;
			this.butLab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLab.Location = new System.Drawing.Point(2,309);
			this.butLab.Name = "butLab";
			this.butLab.Size = new System.Drawing.Size(67,21);
			this.butLab.TabIndex = 77;
			this.butLab.Text = "LabCases";
			this.butLab.Click += new System.EventHandler(this.butLab_Click);
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSearch.Location = new System.Drawing.Point(28,259);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75,26);
			this.butSearch.TabIndex = 40;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// textProduction
			// 
			this.textProduction.BackColor = System.Drawing.Color.White;
			this.textProduction.Location = new System.Drawing.Point(70,329);
			this.textProduction.Name = "textProduction";
			this.textProduction.ReadOnly = true;
			this.textProduction.Size = new System.Drawing.Size(133,20);
			this.textProduction.TabIndex = 38;
			this.textProduction.Text = "$100";
			this.textProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(0,331);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(68,15);
			this.label7.TabIndex = 39;
			this.label7.Text = "Daily Prod";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLab
			// 
			this.textLab.BackColor = System.Drawing.Color.White;
			this.textLab.Location = new System.Drawing.Point(70,309);
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
			this.comboView.Location = new System.Drawing.Point(70,288);
			this.comboView.MaxDropDownItems = 30;
			this.comboView.Name = "comboView";
			this.comboView.Size = new System.Drawing.Size(134,21);
			this.comboView.TabIndex = 35;
			this.comboView.SelectedIndexChanged += new System.EventHandler(this.comboView_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0,291);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66,16);
			this.label2.TabIndex = 34;
			this.label2.Text = "View";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butClearPin
			// 
			this.butClearPin.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClearPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butClearPin.Autosize = true;
			this.butClearPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearPin.CornerRadius = 4F;
			this.butClearPin.Image = ((System.Drawing.Image)(resources.GetObject("butClearPin.Image")));
			this.butClearPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClearPin.Location = new System.Drawing.Point(28,233);
			this.butClearPin.Name = "butClearPin";
			this.butClearPin.Size = new System.Drawing.Size(75,26);
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
			this.groupSearch.Controls.Add(this.butRefresh);
			this.groupSearch.Controls.Add(this.listSearchResults);
			this.groupSearch.Controls.Add(this.listProviders);
			this.groupSearch.Controls.Add(this.butSearchClose);
			this.groupSearch.Controls.Add(this.groupBox2);
			this.groupSearch.Controls.Add(this.label8);
			this.groupSearch.Controls.Add(this.butSearchCloseX);
			this.groupSearch.Controls.Add(this.butSearchNext);
			this.groupSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSearch.Location = new System.Drawing.Point(380,309);
			this.groupSearch.Name = "groupSearch";
			this.groupSearch.Size = new System.Drawing.Size(219,397);
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
			this.butRefresh.Location = new System.Drawing.Point(6,374);
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
			this.listSearchResults.Size = new System.Drawing.Size(193,139);
			this.listSearchResults.TabIndex = 87;
			this.listSearchResults.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSearchResults_MouseDown);
			// 
			// listProviders
			// 
			this.listProviders.Location = new System.Drawing.Point(6,278);
			this.listProviders.MultiColumn = true;
			this.listProviders.Name = "listProviders";
			this.listProviders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProviders.Size = new System.Drawing.Size(193,95);
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
			this.butSearchClose.Location = new System.Drawing.Point(161,374);
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
			this.groupBox2.Location = new System.Drawing.Point(6,174);
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
			this.label8.Location = new System.Drawing.Point(6,260);
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
			// butOther
			// 
			this.butOther.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOther.Autosize = true;
			this.butOther.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOther.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOther.CornerRadius = 4F;
			this.butOther.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butOther.Location = new System.Drawing.Point(712,465);
			this.butOther.Name = "butOther";
			this.butOther.Size = new System.Drawing.Size(92,26);
			this.butOther.TabIndex = 76;
			this.butOther.Text = "Make/Find Appt";
			this.butOther.Click += new System.EventHandler(this.butOther_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(680,2);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(203,29);
			this.ToolBarMain.TabIndex = 73;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// gridEmpSched
			// 
			this.gridEmpSched.HScrollVisible = false;
			this.gridEmpSched.Location = new System.Drawing.Point(666,495);
			this.gridEmpSched.Name = "gridEmpSched";
			this.gridEmpSched.ScrollValue = 0;
			this.gridEmpSched.Size = new System.Drawing.Size(217,210);
			this.gridEmpSched.TabIndex = 77;
			this.gridEmpSched.Title = "Employee Schedules";
			this.gridEmpSched.TranslationName = "TableApptEmpSched";
			this.gridEmpSched.DoubleClick += new System.EventHandler(this.gridEmpSched_DoubleClick);
			this.gridEmpSched.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridEmpSched_CellDoubleClick);
			// 
			// ContrAppt
			// 
			this.Controls.Add(this.gridEmpSched);
			this.Controls.Add(this.groupSearch);
			this.Controls.Add(this.butOther);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.panelOps);
			this.Controls.Add(this.panelCalendar);
			this.Controls.Add(this.panelAptInfo);
			this.Controls.Add(this.panelSheet);
			this.Name = "ContrAppt";
			this.Size = new System.Drawing.Size(939,708);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAppt_Layout);
			this.Load += new System.EventHandler(this.ContrAppt_Load);
			this.Resize += new System.EventHandler(this.ContrAppt_Resize);
			this.panelPinBoard.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panelArrows.ResumeLayout(false);
			this.panelSheet.ResumeLayout(false);
			this.panelAptInfo.ResumeLayout(false);
			this.panelCalendar.ResumeLayout(false);
			this.panelCalendar.PerformLayout();
			this.groupSearch.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			//if(Environment.
		}

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
			//panelTable.Location=new Point(0,-vScrollBar1.Value);
    }

		///<summary>Includes RefreshModulePatient.  One overload is used when jumping here from another module, and you want to place an appointment on the pinboard.</summary>
		public void ModuleSelected(int patNum,Appointment pinAppt){
			ModuleSelected(patNum);
MessageBox.Show("Not functional");
			/*CurInfo=new InfoApt();
			CurInfo.MyApt=pinAppt.Copy();
			Procedure[] procsForSingle;
			if(pinAppt.AptNum==PatCur.NextAptNum) {//if is Next apt
				procsForSingle=Procedures.GetProcsForSingle(pinAppt.AptNum,true);
				CurInfo.Production=Procedures.GetProductionOneApt(pinAppt.AptNum,procsForSingle,true);
			}
			else {//normal apt
				procsForSingle=Procedures.GetProcsForSingle(pinAppt.AptNum,false);
				CurInfo.Production=Procedures.GetProductionOneApt(pinAppt.AptNum,procsForSingle,false);
			}
			CurInfo.Procs=procsForSingle;
			CurInfo.MyPatient=PatCur.Copy();*/
			CurToPinBoard();
			//RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			//ApptViewItems.GetForCurView(comboView.SelectedIndex-1);
			//ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			//the scrollbar logic cannot be moved to someplace where it will be activated while working in apptbook
			//MessageBox.Show("about to refresh");
			//RefreshVisops();//forces reset after changing databases
			if(DefB.Short!=null) {
				//ApptViews.SetCur();
				ApptViewItems.GetForCurView(comboView.SelectedIndex-1);//refreshes visops,etc
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
			this.SuspendLayout();
			vScrollBar1.Enabled=true;
			vScrollBar1.Minimum=0;
			vScrollBar1.LargeChange=12*ContrApptSheet.Lh;//12 rows
			vScrollBar1.Maximum=ContrApptSheet2.Height-panelSheet.Height+vScrollBar1.LargeChange;
			//Max is set again in Resize event
			vScrollBar1.SmallChange=6*ContrApptSheet.Lh;//6 rows
			if(vScrollBar1.Value==0 
				&& 8*ContrApptSheet.RowsPerHr*ContrApptSheet.Lh<vScrollBar1.Maximum-vScrollBar1.LargeChange)
			{
				vScrollBar1.Value=8*ContrApptSheet.RowsPerHr*ContrApptSheet.Lh;//8am
			}
			if(vScrollBar1.Value>vScrollBar1.Maximum-vScrollBar1.LargeChange){
				vScrollBar1.Value=vScrollBar1.Maximum-vScrollBar1.LargeChange;
			}
			ContrApptSheet2.MouseWheel+=new MouseEventHandler(ContrApptSheet2_MouseWheel);
			ContrApptSheet2.Location=new Point(0,-vScrollBar1.Value);
			panelOps.Controls.Clear();
			for(int i=0;i<ContrApptSheet.ProvCount;i++){
				Panel panProv=new Panel();
				//Prov.Text="";
				panProv.BackColor=Providers.List[ApptViewItems.VisProvs[i]].ProvColor;
				panProv.Location=new Point(2+ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*i,0);
				panProv.Width=ContrApptSheet.ProvWidth;
				if(i==0){//just looks a little nicer:
					panProv.Location=new Point(panProv.Location.X-1,panProv.Location.Y);
					panProv.Width=panProv.Width+1;
				}				
				panProv.Height=18;
				panProv.BorderStyle=BorderStyle.Fixed3D;
				panProv.ForeColor=Color.DarkGray;
				panelOps.Controls.Add(panProv);
				toolTip1.SetToolTip(panProv,Providers.List[ApptViewItems.VisProvs[i]].Abbr);
			}
			Operatory curOp;
			for(int i=0;i<ContrApptSheet.ColCount;i++){
				Panel panOpName=new Panel();
				Label labOpName=new Label();
				if(ContrApptSheet.IsWeeklyView){
					labOpName.Text=WeekStartDate.AddDays(i).ToString("dddd");
				}
				else{
					curOp=Operatories.ListShort[ApptViewItems.VisOps[i]];
					labOpName.Text=curOp.OpName;
					if(curOp.ProvDentist!=0 && !curOp.IsHygiene){
						panOpName.BackColor=Providers.GetColor(curOp.ProvDentist);
					}
					else if(curOp.ProvHygienist!=0 && curOp.IsHygiene){
						panOpName.BackColor=Providers.GetColor(curOp.ProvHygienist);
					}
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
			this.ResumeLayout();
			listConfirmed.Items.Clear();
			for(int i=0;i<DefB.Short[(int)DefCat.ApptConfirmed].Length;i++){
				this.listConfirmed.Items.Add(DefB.Short[(int)DefCat.ApptConfirmed][i].ItemValue);
				//if(Defs.Defns[(int)DefCat.ApptConfirmed][i].DefNum==Appointments.Cur.Confirmed)
				//	listConfirmed.SelectedIndex=i;
			}
			RefreshModulePatient(patNum);
			//RefreshModuleScreen();
			RefreshPeriod();
		}

		///<summary>Sets the ContrApptSingle array to null.</summary>
		public void ModuleUnselected(){
			//FamCur=null;
			//PatCur=null;
			//PlanList=null;
			//from RefreshDay:
			//ListDay=null;
			//Schedules.ListDay=null;
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

		///<summary>Was RefreshModuleData and FillPatientButton.  Gets the data for the specified patient. Does not refresh any appointment data.  This function should always be called when the patient changes since that's all this function is responsible for.</summary>
		private void RefreshModulePatient(int patNum){//
			PatCurNum=patNum;//might be zero
			//this is a bit messy for now. Cleanup later
			if(PatCurNum==0){
				PatCurName="";
				PatCurChartNumber="";
				butOther.Enabled=false;
				//return;
			}
			else{
				Patient pat=Patients.GetPat(PatCurNum);
				PatCurName=pat.GetNameLF();
				PatCurChartNumber=pat.ChartNumber;
				//family can wait until user clicks on downarrow.
				Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCurName,PatCurNum);
				butOther.Enabled=true;
			}
			ParentForm.Text=Patients.GetMainTitle(PatCurName,PatCurNum,PatCurChartNumber);
			OnPatientSelected(PatCurNum);
		}

		///<summary>Sends the PatientSelected event on up to the main form.  The only result is that the main window now knows the new patNum.  Does nothing else.  Does not trigger any other methods to run which might cause a loop.  Only called from RefreshModulePatient, but it's separate so that it's the same as in the other modules.</summary>
		private void OnPatientSelected(int patNum) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		///<summary>Currently only used when comboView really does change.  Otherwise, just call ModuleSelected.  Triggered in FunctionKeyPress, SetView, and  FillViews</summary>
		private void comboView_SelectedIndexChanged(object sender,System.EventArgs e) {
			//first two lines might be redundant:
			//ApptViews.SetCur(comboView.SelectedIndex-1);//also works for none (0-1);
			//Already done in RefreshVisops as part of ModuleSelected.
			//ApptViewItems.GetForCurView(comboView.SelectedIndex-1);
			//ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			//if(PatCur==null){
			//	ModuleSelected(0);
			//}
			//else{
			ModuleSelected(PatCurNum);//PatCur.PatNum);
			//}
		}

		///<summary>Activated anytime a Patient menu item is clicked.</summary>
		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,null);
			ModuleSelected(newPatNum);
		}

		///<summary>Triggered every time a control(including appt) is added or removed.  this is too often.</summary>
		private void ContrAppt_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//MessageBox.Show("appt layout");
			//This event actually happens quite frequently: once for every appointment placed on the screen.
			//Moved it all to Resize event.
		}

		//<summary>Called from ModuleSelected and from LayoutPanels.  Refreshes visops, gets apptViewItems, computes columns widths.  Needs to be called when switching databases.</summary>
		/*private void RefreshVisops(){
			if(DefB.Short!=null){
				//ApptViews.SetCur();
				ApptViewItems.GetForCurView(comboView.SelectedIndex-1);//refreshes visops,etc
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
		}*/

		///<summary>Might not be getting called enough.</summary>
		private void ContrAppt_Resize(object sender, System.EventArgs e) {
			//This didn't work so well.  Very slow and caused program to not be able to unminimize
			try{//so it doesn't crash if not connected to DB
				//ModuleSelected();
			}
			catch{}
			//even though the part above didn't work, we're going to try adding the stuff below.  It's important to get it out
			//of the Layout event, which fires very frequently.
			LayoutPanels();
		}

		///<Summary>This might not be getting called frequently enough.  Done on resize and when refreshing the period.  But it used to be done very very frequently.</Summary>
		private void LayoutPanels(){
			//Assumes widths of the first 3 panels were set the same in the designer,
			ToolBarMain.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,0);
			panelCalendar.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,ToolBarMain.Height);
			panelAptInfo.Location=new Point(ClientSize.Width-panelAptInfo.Width-2,ToolBarMain.Height+panelCalendar.Height);
			butOther.Location=new Point(panelAptInfo.Location.X+32,panelAptInfo.Location.Y+84);
			gridEmpSched.Location=new Point(panelAptInfo.Left,panelAptInfo.Bottom+1);
			//panelNotes.Location=new Point(ClientSize.Width-panelAptInfo.Width-2
			//	,ToolBarMain.Height+panelCalendar.Height+panelAptInfo.Height);
			panelSheet.Width=ClientSize.Width-panelAptInfo.Width-2;
			panelSheet.Height=ClientSize.Height-panelSheet.Location.Y;
			//RefreshVisops();//now below
			if(DefB.Short!=null) {
				//ApptViews.SetCur();
				ApptViewItems.GetForCurView(comboView.SelectedIndex-1);//refreshes visops,etc
				ContrApptSheet2.ComputeColWidth(panelSheet.Width-vScrollBar1.Width);
			}
			//ContrApptSheet2.Height=panelSheet.Height;
			panelOps.Width=panelSheet.Width;
		}

		///<summary>Called from FormOpenDental upon startup.</summary>
		public void InstantClasses(){
			PinApptSingle=new ContrApptSingle();
			PinApptSingle.Visible=false;
			PinApptSingle.ThisIsPinBoard=true;
			this.Controls.Add(PinApptSingle);
			PinApptSingle.MouseDown += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseDown);
			PinApptSingle.MouseUp += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseUp);
			PinApptSingle.MouseMove += new System.Windows.Forms.MouseEventHandler(PinApptSingle_MouseMove);
			ContrApptSheet.RowsPerIncr=1;
			Appointments.DateSelected=DateTime.Now;
			ContrApptSingle.SelectedAptNum=-1;
			//RefreshModuleScreen();
			RefreshPeriod();
			//MessageBox.Show(ApptViews.List.Length.ToString());
			if(ApptViews.List.Length>0){//if any views
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
				butTodayWk,
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
		}

		///<summary></summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton("",0,Lan.g(this,"Select Patient"),"Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Unscheduled List"),"Unsched"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Recall List"),"Recall"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",4,Lan.g(this,"Track Planned Appointments"),"Track"));
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
			//ContrApptInfo2.DataChanged += new EventHandler(ReactionDataChanged);
		}

		///<summary>The key press from the main form is passed down to this module.</summary>
		public void FunctionKeyPress(Keys keys){
			//MessageBox.Show("keydown");
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
			if(viewIndex > ApptViews.List.Length){
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
			for(int i=0;i<ApptViews.List.Length;i++){
				if(i<=12)
					f="F"+(i+1).ToString()+"-";
				else
					f="";
				comboView.Items.Add(f+ApptViews.List[i].Description);
			}
			if(selected<comboView.Items.Count){
				comboView.SelectedIndex=selected;//this also triggers SelectedIndexChanged
			}
			if(comboView.SelectedIndex==-1){
				comboView.SelectedIndex=0;
			}
		}

		//<summary></summary>
		/*private void FillPanelPatient(){
			if(PatCur!=null){
				butOther.Enabled=true;//this button is handled entirely separately from the others
			}
			else{
				butOther.Enabled=false;
			}
			if(ContrApptSingle.SelectedAptNum==-1){//apt not selected
				panelAptInfo.Enabled=false;
			}
			else{//apt selected
				bool isPresent=false;
				for(int i=0;i<ListDay.Length;i++){
					if(ListDay[i].AptNum==ContrApptSingle.SelectedAptNum){
						isPresent=true;
						AptCur=ListDay[i].Copy();
						//AptOld=ListDay[i].Copy();
					}
				}
				if(isPresent)	panelAptInfo.Enabled=true;//apt selected and present
				else panelAptInfo.Enabled=false;//apt selected but not present
			}
			if(panelAptInfo.Enabled){
				listConfirmed.SelectedIndex=DefB.GetOrder(DefCat.ApptConfirmed,AptCur.Confirmed);
			}
			else{
				listConfirmed.SelectedIndex=-1;
			}
		}*/

		///<summary>Sets appointment data invalid on all other computers, causing them to refresh.  Does NOT refresh the data for this computer which must be done separately.</summary>
		private void SetInvalid(){
			DataValid.SetInvalid(Appointments.DateSelected);
		}

		///<summary>Switches between weekly view and daily view.  Appointments.DateSelected needs to be set first.  Calculates WeekStartDate and WeekEndDate based on Appointments.DateSelected.  Then calls either RefreshPeriod or ModuleSelected.</summary>
		private void SetWeeklyView(bool isWeeklyView) {
			//if the weekly view doesn't change, then just RefreshPeriod
			bool weeklyViewChanged=false;
			if(isWeeklyView!=ContrApptSheet.IsWeeklyView){
				weeklyViewChanged=true;
			}
			if(isWeeklyView) {
				WeekStartDate=Appointments.DateSelected.AddDays(1-(int)Appointments.DateSelected.DayOfWeek);
				WeekEndDate=WeekStartDate.AddDays(numOfWeekDaysToDisplay-1);
			}
			ContrApptSheet.IsWeeklyView=isWeeklyView;
			if(weeklyViewChanged){
				ModuleSelected(PatCurNum);
			}
			else{
				RefreshPeriod();
			}
		}

		///<summary>Important.  Gets all new day info from db and redraws screen</summary>
		public void RefreshPeriod(){
			DateTime startDate;
			DateTime endDate;
			if(ContrApptSheet.IsWeeklyView) {
				startDate=WeekStartDate;
				endDate=WeekEndDate;
			}
			else {
				startDate=Appointments.DateSelected;
				endDate=Appointments.DateSelected;
			}
			if(startDate.Year<1880 || endDate.Year<1880) {
				return;
			}
			if(PatCurNum==0){//PatCur==null){
				//there cannot be a selected appointment if no patient is loaded.
				ContrApptSingle.SelectedAptNum=-1;//fixes a minor bug.
			}
			//else if(ContrApptSingle.se
			//all data storage is being moved into this dataset:
			DS=Appointments.RefreshPeriod(startDate,endDate);
			//ApptViews.SetCur();
			ApptViewItems.GetForCurView(comboView.SelectedIndex-1);
			ContrApptSingle.ProvBar=new int[ApptViewItems.VisProvs.Length][];
			for(int i=0;i<ApptViewItems.VisProvs.Length;i++){//Providers.List.Length;i++){
				ContrApptSingle.ProvBar[i]=new int[24*ContrApptSheet.RowsPerHr];//[144]; or 24*6
			}
			if(ContrApptSingle3!=null){//I think this is not needed.
				for(int i=0;i<ContrApptSingle3.Length;i++){
					if(ContrApptSingle3[i]!=null){
						ContrApptSingle3[i].Dispose();
					}
					ContrApptSingle3[i]=null;
				}
				ContrApptSingle3=null;
			}
			//ListDay=Appointments.GetForPeriod(startDate,endDate);
			SchedListDay=Schedules.RefreshPeriod(startDate,endDate);
			labelDate.Text=startDate.ToString("ddd");
			labelDate2.Text=startDate.ToString("-  MMM d");
			Calendar2.SetDate(startDate);
			ContrApptSheet2.Controls.Clear();
			ContrApptSingle3=new ContrApptSingle[DS.Tables["Appointments"].Rows.Count];//ListDay.Length];
			//int[] aptNums=new int[DS.Tables["Appointments"].Rows.Count];
			//int[] patNums=new int[DS.Tables["Appointments"].Rows.Count];
			//for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
			//	aptNums[i]=PIn.PInt(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());//ListDay[i].AptNum;
			//	patNums[i]=PIn.PInt(DS.Tables["Appointments"].Rows[i]["PatNum"].ToString());//ListDay[i].PatNum;
			//}
			//procsMultApts=Procedures.GetProcsMultApts(aptNums);
			//Patient[] multPats=Patients.GetMultPats(patNums);
			//List<LabCase> labCaseList=LabCases.GetForPeriod(startDate,endDate);
			//Procedure[] procsOneApt;
			int indexProv;
			DataRow row;
			//for(int i=0;i<ListDay.Length;i++){
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
				row=DS.Tables["Appointments"].Rows[i];
				ContrApptSingle3[i]=new ContrApptSingle();
				ContrApptSingle3[i].Visible=false;
				//ContrApptSingle3[i].Info=new InfoApt();
				//ContrApptSingle3[i].Info.MyApt=null;//ListDay[i].Copy();
				if(ContrApptSingle.SelectedAptNum.ToString()==row["AptNum"].ToString()){//ListDay[i].AptNum){//if this is the selected apt
					//if the selected patient was changed from another module, then deselect the apt.
					if(PatCurNum.ToString()!=row["PatNum"].ToString()){
						ContrApptSingle.SelectedAptNum=-1;
					}
				}
				//procsOneApt=Procedures.GetProcsOneApt(ListDay[i].AptNum,procsMultApts);
				//ContrApptSingle3[i].Info.Procs=procsOneApt;
				ContrApptSingle3[i].DataRoww=row;
				//ContrApptSingle3[i].Info.Production=Procedures.GetProductionOneApt(ListDay[i].AptNum,procsMultApts,false);
				//ContrApptSingle3[i].Info.MyPatient=Patients.GetOnePat(multPats,ListDay[i].PatNum);
				//ContrApptSingle3[i].Info.MyLabCase=LabCases.GetOneFromList(labCaseList,ListDay[i].AptNum);
				//copy time pattern to provBar[]:
				indexProv=-1;
				/*if(row["IsHygiene"].ToString()=="1"){
					indexProv=ApptViewItems.GetIndexProv(ListDay[i].ProvHyg);
				}
				else{
					indexProv=ApptViewItems.GetIndexProv(ListDay[i].ProvNum);
				}
				if(indexProv!=-1 && ListDay[i].AptStatus!=ApptStatus.Broken){
					string pattern=ContrApptSingle.GetPatternShowing(ListDay[i].Pattern);
					int startIndex=ContrApptSingle3[i].ConvertToY()/ContrApptSheet.Lh;//rounds down
					for(int k=0;k<pattern.Length;k++){
						if(pattern.Substring(k,1)=="X"){
							//int timeBarInc=ContrApptSingle3[i].ConvertToY()/ContrApptSheet.Lh+k;
							try{
								ContrApptSingle.ProvBar[indexProv][startIndex+k]++;
							}
							catch{
								//appointment must extend past midnight.  Very rare
							}
						}
					}
				}*/
				//ContrApptSingle3[i].IsWeeklyView=isWeeklyView;
				ContrApptSingle3[i].SetLocation();
				ContrApptSheet2.Controls.Add(ContrApptSingle3[i]);
			}//end for
			//if(ContrApptSingle.SelectedAptNum!=1
			PinApptSingle.Refresh();
			ContrApptSheet2.SchedListDay=SchedListDay;
			ContrApptSheet2.CreateShadow();
			CreateAptShadows();
			ContrApptSheet2.DrawShadow();
			//FillPanelPatient();
//broken:
			//FillLab(labCaseList);
			FillProduction();
			FillEmpSched();
			LayoutPanels();
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
			for(int i=0;i<ApptViewItems.ApptRows.Length;i++){
				if(ApptViewItems.ApptRows[i].ElementDesc=="Production"){
					showProduction=true;
				}
			}
//broken:
/*		if(showProduction){
				double production=0;
				int indexProv;
				for(int i=0;i<ListDay.Length;i++){
					indexProv=-1;
					if(ListDay[i].IsHygiene){
						indexProv=ApptViewItems.GetIndexProv(ListDay[i].ProvHyg);
					}
					else{
						indexProv=ApptViewItems.GetIndexProv(ListDay[i].ProvNum);
					}
					if(indexProv!=-1){
						//prevents canceled appt from totaling in production for the day
						if (ListDay[i].AptStatus!=ApptStatus.Broken && ListDay[i].AptStatus!=ApptStatus.UnschedList){
							production+=Procedures.GetProductionOneApt(ListDay[i].AptNum, procsMultApts, false);
						}

					}
				}
				textProduction.Text=production.ToString("c0");
			}
			else{*/
				textProduction.Text="";
			//}
		}

		///<Summary></Summary>
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

		private void gridEmpSched_CellDoubleClick(object sender,ODGridClickEventArgs e) {

		}

		private void gridEmpSched_DoubleClick(object sender,EventArgs e) {
			if(ContrApptSheet.IsWeeklyView){
				return;
			}
			if(!Security.IsAuthorized(Permissions.Schedules)) {
				return;
			}
			FormScheduleDayEdit FormS=new FormScheduleDayEdit(Appointments.DateSelected);
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Schedules,0,"");
			SetWeeklyView(false);//to refresh
		}

		///<summary>Creates one bitmap image for each appointment if visible.</summary>
		private void CreateAptShadows(){
			if(ContrApptSheet2.Shadow==null)//if user resizes window to be very narrow
				return;
			Graphics grfx=Graphics.FromImage(ContrApptSheet2.Shadow);
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
				//MessageBox.Show("i:"+i.ToString()+",height:"+ContrApptSingle3[i].Height.ToString());
				ContrApptSingle3[i].CreateShadow();
				if(ContrApptSingle3[i].Location.X>=ContrApptSheet.TimeWidth+ContrApptSheet.ProvWidth*ContrApptSheet.ProvCount
					&& ContrApptSingle3[i].Width>3
					&& ContrApptSingle3[i].Shadow!=null)
				{
					grfx.DrawImage(ContrApptSingle3[i].Shadow
						,ContrApptSingle3[i].Location.X,ContrApptSingle3[i].Location.Y);
				}
				ContrApptSingle3[i].Shadow=null;
			}
			grfx.Dispose();
		}

		///<summary>Gets the index within the array of appointment controls, based on the supplied primary key.</summary>
		private int GetIndex(int myAptNum){
			int retVal=-1;
			for(int i=0;i<ContrApptSingle3.Length;i++){
				if(ContrApptSingle3[i].DataRoww["AptNum"].ToString()==myAptNum.ToString()){
					retVal=i;
				}
			}
			return retVal;
		}

		///<summary>Copies all info for for current appointment into the control that displays the pinboard appointment. Sets pinboard appointment as selected.  CurInfo needs to be setup ahead of time.  Copy this functionality from another similar area of the program.</summary>
		private void CurToPinBoard(){
MessageBox.Show("Not functional");
			/*PinApptSingle.Visible=false;
			PinApptSingle.Info=CurInfo;
			PinApptSingle.SetLocation();//MUST come before next line
			PinApptSingle.Location=new Point(panelCalendar.Location.X+panelPinBoard.Location.X+2
				,panelCalendar.Location.Y+panelPinBoard.Location.Y+2);
			PinApptSingle.SetSize();
			Appointments.PinBoard=CurInfo.MyApt;//Appointments.List[ContrApptSingle.SelectedIndex];
			PinApptSingle.Visible=true;
			PinApptSingle.BringToFront();
			mouseIsDown=false;
			boolAptMoved=false;
			ContrApptSingle.PinBoardIsSelected=true;
			ContrApptSingle.SelectedAptNum=-1;//CurInfo.MyApt.AptNum;
			PinApptSingle.CreateShadow();
			PinApptSingle.Refresh();*/
		}

		///<summary>Mouse down event for the pinboard appointment. Sets selected and prepares for drag.</summary>
		private void PinApptSingle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			mouseIsDown = true;
			ContrApptSingle.PinBoardIsSelected=true;
			TempApptSingle=new ContrApptSingle();
			TempApptSingle.DataRoww=PinApptSingle.DataRoww;
			TempApptSingle.Visible=false;
			Controls.Add(TempApptSingle);
			TempApptSingle.SetLocation();
			TempApptSingle.CreateShadow();
			TempApptSingle.BringToFront();
			ContrApptSingle.SelectedAptNum=-1;//PinApptSingle.Info.MyApt.AptNum;
			//FamCur=Patients.GetFamily(PinApptSingle.Info.MyApt.PatNum);
			//PatCur=FamCur.GetPatient(PinApptSingle.Info.MyApt.PatNum);
			RefreshModulePatient(PIn.PInt(PinApptSingle.DataRoww["PatNum"].ToString()));
			//FillPatientButton();
			//ParentForm.Text=Patients.GetMainTitle(PatCur);
			//FillPanelPatient();
			PinApptSingle.CreateShadow();
			PinApptSingle.Refresh();
			CreateAptShadows();
			ContrApptSheet2.DrawShadow();
			//mouseOrigin is in ContrAppt coordinates (essentially, the entire window)
			mouseOrigin.X=e.X+PinApptSingle.Location.X;
			mouseOrigin.Y=e.Y+PinApptSingle.Location.Y;
			contOrigin=PinApptSingle.Location;
		}//end PinApptSingle_MouseDown

		///<summary>Mouse move event for pinboard appt. Moves pinboard appt if mouse is down.</summary>
		private void PinApptSingle_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e){
			if(mouseIsDown==false) return;
			if((Math.Abs(e.X+PinApptSingle.Location.X-mouseOrigin.X)<1)
				&&(Math.Abs(e.Y+PinApptSingle.Location.Y-mouseOrigin.Y)<1)){
				return;
			}
			if(TempApptSingle.Location==new Point(0,0)){
				TempApptSingle.Height=1;//to prevent flicker in UL corner
			}
			TempApptSingle.Visible=true;
			boolAptMoved=true;
			TempApptSingle.Location=new Point(
				contOrigin.X+(e.X+PinApptSingle.Location.X)-mouseOrigin.X,contOrigin.Y+(e.Y+PinApptSingle.Location.Y)-mouseOrigin.Y);
			if(TempApptSingle.Height==1){
				TempApptSingle.SetSize();
			}
		}

		///<summary>Mouse up event for pinboard appt.  Usually happens after pinboard appt has been dragged onto main appt sheet.</summary>
		private void PinApptSingle_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e){
			if(!boolAptMoved){
				mouseIsDown=false;
				TempApptSingle.Dispose();
				//PinApptSingle.Refresh();//?
				//ContrApptSheet2.Refresh();//?
				return;
			}
			if(TempApptSingle.Location.X>ContrApptSheet2.Width){//
				mouseIsDown=false;
				boolAptMoved=false;
				//pinIsOccupied=true;
				TempApptSingle.Dispose();
				//ContrApptSheet2.Refresh();//?
				return;
			}
			if(Appointments.PinBoard.AptStatus==ApptStatus.Planned//if Planned appt is on pinboard
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
			Appointment aptCur=Appointments.PinBoard.Copy();
			Appointment aptOld=Appointments.PinBoard.Copy();
			if(aptCur.IsNewPatient && Appointments.DateSelected!=aptCur.AptDateTime){
//broken:
				//Procedures.SetDateFirstVisit(Appointments.DateSelected,4,PatCur);
			}
			int tHr=ContrApptSheet2.ConvertToHour
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			int tMin=ContrApptSheet2.ConvertToMin
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			DateTime tDate=Appointments.DateSelected;
			DateTime fromDate=aptCur.AptDateTime.Date;
			aptCur.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
			if(AppointmentRules.List.Length>0){
				//this is crude and temporary:
				int[] aptNums=new int[DS.Tables["Appointments"].Rows.Count];
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
					aptNums[i]=PIn.PInt(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());//ListDay[i].AptNum;
				}
				Procedure[] procsMultApts=Procedures.GetProcsMultApts(aptNums);
				Procedure[] procsForOne=Procedures.GetProcsOneApt(aptCur.AptNum,procsMultApts);
//broken:
				ArrayList doubleBookedCodes=new ArrayList();
					//Appointments.GetDoubleBookedCodes(aptCur,ListDay,procsMultApts,procsForOne);
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
			Operatory curOp=Operatories.ListShort[ApptViewItems.VisOps
				[ContrApptSheet2.ConvertToOp(TempApptSingle.Location.X-ContrApptSheet2.Location.X)]];
			aptCur.Op=curOp.OperatoryNum;
			if(DoesOverlap(aptCur)){
				int startingOp=ApptViewItems.GetIndexOp(aptCur.Op);
					//DefB.GetOrder(DefCat.Operatories,Appointments.Cur.Op);
				bool stillOverlaps=true;
				for(int i=startingOp;i<ApptViewItems.VisOps.Length;i++){
					//DefB.Short[(int)DefCat.Operatories].Length
					aptCur.Op=Operatories.ListShort[ApptViewItems.VisOps[i]].OperatoryNum;
					if(!DoesOverlap(aptCur)){
						stillOverlaps=false;
						break;
					}
				}
				if(stillOverlaps){
					for(int i=startingOp;i>=0;i--){
						aptCur.Op=Operatories.ListShort[ApptViewItems.VisOps[i]].OperatoryNum;
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
			if(curOp.ProvDentist!=0){//dentist assigned to op
				aptCur.ProvNum=curOp.ProvDentist;
			}
			if(curOp.ProvHygienist!=0){//hygienist assigned to op
				aptCur.ProvHyg=curOp.ProvHygienist;
			}
			if((curOp.ProvHygienist!=0 && curOp.IsHygiene) || (curOp.ProvDentist!=0 && !curOp.IsHygiene)){
				aptCur.IsHygiene=curOp.IsHygiene;
			}
			if(curOp.ClinicNum!=0) {
				aptCur.ClinicNum=curOp.ClinicNum;
			}
			if(aptCur.AptStatus==ApptStatus.Planned){//if Planned appt is on pinboard
				aptCur.NextAptNum=aptCur.AptNum;
				aptCur.AptStatus=ApptStatus.Scheduled;
				try{
					Appointments.InsertOrUpdate(aptCur,null,true);//now, aptnum is different.
				}
				catch(ApplicationException ex){
					MessageBox.Show(ex.Message);
					return;
				}
				SecurityLogs.MakeLogEntry(Permissions.AppointmentCreate,PatCurNum,
					PatCurName+", "
					+aptCur.AptDateTime.ToString()+", "
					+aptCur.ProcDescript);
				Procedure[] ProcList=Procedures.Refresh(PatCurNum);
				bool procAlreadyAttached=false;
				//Procedure ProcCur;
				//Procedure ProcOld;
//broken:
				/*for(int i=0;i<ProcList.Length;i++){
					if(ProcList[i].PlannedAptNum==PatCur.NextAptNum){//if on the planned apt
						if(ProcList[i].AptNum>0){//already attached to another appt
							procAlreadyAttached=true;
						}
						else{//only update procedures not already attached to another apt
							//ProcCur=ProcList[i];
							//ProcOld=ProcCur.Copy();
							//ProcCur.AptNum=aptCur.AptNum;
							Procedures.UpdateAptNum(ProcList[i].ProcNum,aptCur.AptNum);
								//.Update(ProcCur,ProcOld);//recall synch not required.
						}
					}
				}*/
				if(procAlreadyAttached){
					MessageBox.Show(Lan.g(this,"One or more procedures could not be scheduled because they were already attached to another appointment. Someone probably forgot to update the Next appointment in the Chart module."));
				}
				//Procedure[] procs=Procedures.GetProcsForSingle(aptCur.AptNum,true);
				//CurInfo.Procs=procs;
				//CurInfo.Production=Procedures.GetProductionOneApt(aptCur.AptNum,procs,true);
				//CurInfo.MyApt...
				//CurInfo.MyPatient...
				//might be missing some CurInfo.
			}//if planned appointment is on pinboard
			else{//simple drag off pinboard to a new date/time
				try{
					Appointments.InsertOrUpdate(aptCur,aptOld,false);
					SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,PatCurNum,
						PatCurName+", "
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
			TempApptSingle.Dispose();
			PinApptSingle.Visible=false;
			ContrApptSingle.PinBoardIsSelected=false;
			ContrApptSingle.SelectedAptNum=aptCur.AptNum;
			RefreshModulePatient(PatCurNum);
			RefreshPeriod();//date moving to for this computer
			SetInvalid();//date moving to for other computers
			Appointments.DateSelected=fromDate;
			SetInvalid();//for date moved from for other computers.
			Appointments.DateSelected=aptCur.AptDateTime;
			mouseIsDown = false;
			boolAptMoved=false;
		}//end PinApptSingle_mouseup
		
		///<summary>Called when releasing an appointment to make sure it does not overlap any other appointment.  Tests all appts for the day, even if not visible.</summary>
		private bool DoesOverlap(Appointment aptCur){
//broken
			//bool retVal=false;
			/*
			for(int i=0;i<ListDay.Length;i++){
				if(ListDay[i].AptNum==aptCur.AptNum){
					continue;
				}
				if(ListDay[i].Op!=aptCur.Op){
					continue;
				}
				//tests start time
				if(aptCur.AptDateTime.TimeOfDay >= ListDay[i].AptDateTime.TimeOfDay
					&& aptCur.AptDateTime.TimeOfDay < ListDay[i].AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(ListDay[i].Pattern.Length*5)))
				{
					//Debug.WriteLine(TimeSpan.FromMinutes(ListDay[i].Pattern.Length*5).ToString());
					return true;
				}
				//tests stop time
				if(aptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(aptCur.Pattern.Length*5)) > ListDay[i].AptDateTime.TimeOfDay
					&& aptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(aptCur.Pattern.Length*5))
					<= ListDay[i].AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(ListDay[i].Pattern.Length*5)))
				{
					return true;
				}
				//tests engulf
				if(aptCur.AptDateTime.TimeOfDay <= ListDay[i].AptDateTime.TimeOfDay
					&& aptCur.AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(aptCur.Pattern.Length*5))
					>= ListDay[i].AptDateTime.TimeOfDay.Add(TimeSpan.FromMinutes(ListDay[i].Pattern.Length*5)))
				{
					return true;
				}
			}*/
			return false;
		}

		///<summary>Clicked today.</summary>
		private void butToday_Click(object sender, System.EventArgs e) {
			Appointments.DateSelected=DateTime.Now;
			SetWeeklyView(false);
		}

		///<summary>Clicked back one day.</summary>
		private void butBack_Click(object sender, System.EventArgs e) {
			Appointments.DateSelected=Appointments.DateSelected.AddDays(-1);
			SetWeeklyView(false);
		}

		///<summary>Clicked forward one day.</summary>
		private void butFwd_Click(object sender, System.EventArgs e) {
			Appointments.DateSelected=Appointments.DateSelected.AddDays(1);
			SetWeeklyView(false);
		}

		///<summary>Now clicking the button turns on the weekly view based on selected date.
		///Old behavior: Clicked week button, setting the date to the current week, but not necessarily to today.</summary>
		private void butTodayWk_Click(object sender, System.EventArgs e) {
			int dayChange = Appointments.DateSelected.DayOfWeek-DateTime.Now.DayOfWeek;
			Appointments.DateSelected=DateTime.Now.AddDays(dayChange);
			SetWeeklyView(false);//true);
		}

		///<summary>Clicked back one week.</summary>
		private void butBackWk_Click(object sender, System.EventArgs e) {
			Appointments.DateSelected=Appointments.DateSelected.AddDays(-7);
			SetWeeklyView(false);//true);
		}

		///<summary>Clicked forward one week.</summary>
		private void butFwdWk_Click(object sender, System.EventArgs e) {
			Appointments.DateSelected=Appointments.DateSelected.AddDays(7);
			SetWeeklyView(false);//true);
		}

		///<summary>Clicked a date on the calendar.</summary>
		private void Calendar2_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			Appointments.DateSelected=Calendar2.SelectionStart;
			SetWeeklyView(false);
		}

		///<summary>Returns the apptNum of the appointment at these coordinates, or 0 if none.  This is new code which is going to replace some of the outdated code on this page.</summary>
		private int HitTestAppt(Point point){
			if(ApptViewItems.VisOps.Length==0){//no ops visible.
				return 0;
			}
			int op=Operatories.ListShort[ApptViewItems.VisOps[ContrApptSheet.XPosToOp(point.X)]].OperatoryNum;
			int hour=ContrApptSheet.YPosToHour(point.Y);
			int minute=ContrApptSheet.YPosToMin(point.Y);
			TimeSpan time=new TimeSpan(hour,minute,0);
			TimeSpan aptTime;
			for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
				if(op.ToString()!=DS.Tables["Appointments"].Rows[i]["Op"].ToString()){
					continue;
				}
				aptTime=PIn.PDateT(DS.Tables["Appointments"].Rows[i]["AptDateTime"].ToString()).TimeOfDay;
				if(aptTime <= time
					&& time < aptTime+TimeSpan.FromMinutes(DS.Tables["Appointments"].Rows[i]["Pattern"].ToString().Length*5))
				{
					return PIn.PInt(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());
				}
			}
			return 0;
		}

		///<summary>If the given point is in the bottom few pixels of an appointment, then this returns true.  Use HitTestAppt to figure out which appointment.</summary>
		private bool HitTestApptBottom(Point point){
			int aptnum=HitTestAppt(point);
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
			if(ApptViewItems.VisOps.Length==0){//no ops visible.
				return;
			}
			//some of this is a little redundant, but still necessary until the rewrite is finished.
			SheetClickedonHour=ContrApptSheet.YPosToHour(e.Y);
			SheetClickedonMin=ContrApptSheet.YPosToMin(e.Y);
			TimeSpan sheetClickedOnTime=new TimeSpan(SheetClickedonHour,SheetClickedonMin,0);
			if(ContrApptSheet.IsWeeklyView){
//broken:
				/*SheetClickedonDay=ContrApptSheet2.ConvertToOp(e.X)+1;
				for(int i=0;i<ListDay.Length;i++) {
					if(SheetClickedonDay==(int)ListDay[i].AptDateTime.DayOfWeek
						&& ListDay[i].AptDateTime.TimeOfDay<=sheetClickedOnTime
						&& sheetClickedOnTime<ListDay[i].AptDateTime.TimeOfDay+TimeSpan.FromMinutes(ListDay[i].Pattern.Length*5))
					{
						ContrApptSingle.ClickedAptNum=ListDay[i].AptNum;
						SheetClickedonOp=ListDay[i].Op;
					}
				}*/
			}
			else {//daily view
				ContrApptSingle.ClickedAptNum=HitTestAppt(e.Location);//0;
				SheetClickedonOp=Operatories.ListShort[ApptViewItems.VisOps[ContrApptSheet.XPosToOp(e.X)]].OperatoryNum;
			}
			Graphics grfx=ContrApptSheet2.CreateGraphics();
			if(ContrApptSingle.ClickedAptNum!=0){//if clicked on an appt
				int thisIndex=GetIndex(ContrApptSingle.ClickedAptNum);
				ContrApptSingle.PinBoardIsSelected=false;
				//AptCur=PIn.PInt(ContrApptSingle3[thisIndex].DataRoww["AptNum"].ToString());
				//Appointments.CurOld=Appointments.Cur;
				if(ContrApptSingle.SelectedAptNum!=-1//unselects previously selected unless it's the same appt
					&& ContrApptSingle.SelectedAptNum!=ContrApptSingle.ClickedAptNum){
					int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
					//has to be done before refresh prev:
					ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
					if(prevSel!=-1){
						ContrApptSingle3[prevSel].CreateShadow();
						grfx.DrawImage(ContrApptSingle3[prevSel].Shadow,ContrApptSingle3[prevSel].Location.X
							,ContrApptSingle3[prevSel].Location.Y);
					}
				}
				//again, in case missed in loop above:
				ContrApptSingle.SelectedAptNum=ContrApptSingle.ClickedAptNum;
				//RefreshModuleData(//this would take too long
				//This will go away when the data portion of this module gets rewritten.
				//beware that PlanList and CovPats have NOT been refreshed
				//FamCur=Patients.GetFamily(ListDay[thisIndex].PatNum);
				//PatCur=FamCur.GetPatient(ListDay[thisIndex].PatNum);
				//FillPatientButton();
				//OnPatientSelected(PatCur.PatNum);
				//ParentForm.Text=Patients.GetMainTitle(PatCur);
				ContrApptSingle3[thisIndex].CreateShadow();
				grfx.DrawImage(ContrApptSingle3[thisIndex].Shadow,ContrApptSingle3[thisIndex].Location.X
					,ContrApptSingle3[thisIndex].Location.Y);
				//FillPanelPatient();
				RefreshModulePatient(PIn.PInt(ContrApptSingle3[thisIndex].DataRoww["PatNum"].ToString()));
				if(e.Button==MouseButtons.Right){
					if(ContrApptSheet.IsWeeklyView){
						menuWeeklyApt.Show(ContrApptSheet2,new Point(e.X,e.Y));
					}
					else{
						menuApt.Show(ContrApptSheet2,new Point(e.X,e.Y));
					}
				}
				else{
					mouseIsDown = true;
					TempApptSingle=new ContrApptSingle();
					TempApptSingle.Visible=false;//otherwise I get a phantom appt while holding mouse down
					TempApptSingle.DataRoww=ContrApptSingle3[thisIndex].DataRoww;
					Controls.Add(TempApptSingle);
					TempApptSingle.SetLocation();
					TempApptSingle.BringToFront();
					//mouseOrigin is in ApptSheet coordinates
					//This was wrong:
					//mouseOrigin=new Point(e.X+ContrApptSingle3[thisIndex].Location.X,e.Y+ContrApptSingle3[thisIndex].Location.Y);
					mouseOrigin=e.Location;
					contOrigin=ContrApptSingle3[thisIndex].Location;
					TempApptSingle.CreateShadow();
					if(HitTestApptBottom(e.Location)){
						ResizingAppt=true;
						ResizingOrigH=TempApptSingle.Height;
					}
				}
			}
			else{//not on appt. This was disabled so you can double click on empty area to sched
				if(ContrApptSheet.IsWeeklyView)
					return;
				if(e.Button==MouseButtons.Right){
					bool clickedOnBlock=false;
					Schedule[] ListForType=Schedules.GetForType(SchedListDay,ScheduleType.Blockout,0);
					for(int i=0;i<ListForType.Length;i++){
						//skip if op doesn't match
						if(ListForType[i].Op!=0){//if op is zero, it doesn't matter which op.
							if(ListForType[i].Op != SheetClickedonOp){
								continue;
							}
						}
						if(ListForType[i].StartTime.TimeOfDay <= sheetClickedOnTime
							&& sheetClickedOnTime < ListForType[i].StopTime.TimeOfDay)
						{
							clickedOnBlock=true;
							break;
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
			if(PinApptSingle.Visible){
				PinApptSingle.CreateShadow();
				PinApptSingle.Refresh();
			}
			CreateAptShadows();
			//CreateHundredShadows();
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
			if(ContrApptSheet.IsWeeklyView)
				return;
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

		///<Summary>Does a hit test to determine if over an appointment.  Fills the bubble with data and then positions it.</Summary>
		private void InfoBubbleDraw(Point p){
			int aptNum=HitTestAppt(p);
			if(aptNum==0 || HitTestApptBottom(p)) {
				if(infoBubble.Visible) {
					infoBubble.Visible=false;
				}
				return;
			}
			if(aptNum!=bubbleAptNum){
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
					&& PrefB.UsingAtoZfolder)//Do not use patient image when A to Z folders are disabled.
					//return;
				{
					try {
						Bitmap patPict;
						Documents.GetPatPict(PIn.PInt(row["PatNum"].ToString()),
							ODFileUtils.CombinePaths(new string[] {	FormPath.GetPreferredImagePath(),
							row["ImageFolder"].ToString().Substring(0,1).ToUpper(),
							row["ImageFolder"].ToString(),""}),
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
					g.DrawString(row["Note"].ToString(),font,brush,new RectangleF(x,y,infoBubble.Width,h));
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
					g.DrawString(row["addrNote"].ToString(),font,brush,new RectangleF(x,y,infoBubble.Width,h));
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
			infoBubble.Visible=true;
		}

		///<summary>Usually dropping an appointment to a new location.</summary>
		private void ContrApptSheet2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(ContrApptSheet.IsWeeklyView) return;//Because an operatory num is required
			if(!mouseIsDown) return;
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointment aptOld;
			if(ResizingAppt){
				/*
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
				if(newpatternL < ContrApptSheet.MinPerIncr/5){//eg. if 1 < 10/5, would make appt too short. 
					newpatternL=ContrApptSheet.MinPerIncr/5;//sets new pattern length at one increment, typically 2 or 3 5min blocks
				}
				else if(newpatternL>78){//max length of 390 minutes
					newpatternL=78;
				}
				
				AptCur=TempApptSingle.Info.MyApt.Copy();
				aptOld=AptCur.Copy();
				if(newpatternL<AptCur.Pattern.Length){//shorten to match new pattern length
					AptCur.Pattern=AptCur.Pattern.Substring(0,newpatternL);
				}
				else if(newpatternL>AptCur.Pattern.Length){//make pattern longer.
					AptCur.Pattern=AptCur.Pattern.PadRight(newpatternL,'/');
				}
				//Now, check for overlap with other appts.
				for(int i=0;i<ListDay.Length;i++) {
					if(ListDay[i].AptNum==AptCur.AptNum){
						continue;
					}
					if(ListDay[i].Op!=AptCur.Op){
						continue;
					}
					if(ListDay[i].AptDateTime<AptCur.AptDateTime){
						continue;//we don't care about appointments that are earlier than this one
					}
					if(ListDay[i].AptDateTime.TimeOfDay < AptCur.AptDateTime.TimeOfDay+TimeSpan.FromMinutes(5*AptCur.Pattern.Length)){
						newspan=ListDay[i].AptDateTime.TimeOfDay-AptCur.AptDateTime.TimeOfDay;
						newpatternL=(int)newspan.TotalMinutes/5;
						AptCur.Pattern=AptCur.Pattern.Substring(0,newpatternL);
					}
				}
				try {
					Appointments.InsertOrUpdate(AptCur,aptOld,false);
				}
				catch(ApplicationException ex) {
					MessageBox.Show(ex.Message);
				}*/
MessageBox.Show("Not functional");
				ResizingAppt=false;
				mouseIsDown=false;
				TempApptSingle.Dispose();
				RefreshModulePatient(PatCurNum);
				RefreshPeriod();
				SetInvalid();
				return;
			}
			//if((Math.Abs(e.X+ContrApptSingle3[thisIndex].Location.X-mouseOrigin.X)<7)
			//	&&(Math.Abs(e.Y+ContrApptSingle3[thisIndex].Location.Y-mouseOrigin.Y)<7)){
			if((Math.Abs(e.X-mouseOrigin.X)<7)
				&&(Math.Abs(e.Y-mouseOrigin.Y)<7)){
				boolAptMoved=false;
			}
			//it was a click with no drag-----------------------------------------------------------------------------------------
			if(!boolAptMoved){
				mouseIsDown=false;
				TempApptSingle.Dispose();
				//PlanList=InsPlans.Refresh(FamCur);
				//PatPlanList=PatPlans.Refresh(PatCur.PatNum);
				//CovPats.Refresh(PlanList,PatPlanList);
				//PinApptSingle.Refresh();
				return;
			}
			//dragging to pinboard, so place a copy there---------------------------------------------------------------------------
			if(TempApptSingle.Location.X>ContrApptSheet2.Width){
				if(!Security.IsAuthorized(Permissions.AppointmentMove)){
					mouseIsDown=false;
					TempApptSingle.Dispose();
					//PlanList=InsPlans.Refresh(FamCur);
					//PatPlanList=PatPlans.Refresh(PatCur.PatNum);
					//CovPats.Refresh(PlanList,PatPlanList);
					return;
				}
				int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
//CurInfo=TempApptSingle.Info;
CurToPinBoard();//sets selectedAptNum=-1. do before refresh prev
				if(prevSel!=-1){
					CreateAptShadows();
					ContrApptSheet2.DrawShadow();
				}
				//PlanList=InsPlans.Refresh(FamCur);
				//PatPlanList=PatPlans.Refresh(PatCur.PatNum);
				//CovPats.Refresh(PlanList,PatPlanList);
				//FillPanelPatient();
				RefreshModulePatient(PatCurNum);
				TempApptSingle.Dispose();
				return;
			}
			//moving to a new location-----------------------------------------------------------------------------------------------
			Appointment apt=Appointments.GetOneApt(ContrApptSingle.SelectedAptNum);
			int tHr=ContrApptSheet2.ConvertToHour
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			int tMin=ContrApptSheet2.ConvertToMin
				(TempApptSingle.Location.Y-ContrApptSheet2.Location.Y-panelSheet.Location.Y);
			bool timeWasMoved=tHr!=apt.AptDateTime.Hour
				|| tMin!=apt.AptDateTime.Minute;
			if(timeWasMoved){
				if(!Security.IsAuthorized(Permissions.AppointmentMove) || !MsgBox.Show(this,true,"Move Appointment?")){
					mouseIsDown=false;
					boolAptMoved=false;
					TempApptSingle.Dispose();
					//PlanList=InsPlans.Refresh(FamCur);//why?
					//PatPlanList=PatPlans.Refresh(PatCur.PatNum);//why?
					//CovPats.Refresh(PlanList,PatPlanList);
					return;
				}
			}
			//convert loc to new time
			//AptCur was set when mouse down on an appt, but now we are going to change it
			DateTime tDate=apt.AptDateTime.Date;
			//AptCur=TempApptSingle.Info.MyApt.Copy();
			aptOld=apt.Copy();
			apt.AptDateTime=new DateTime(tDate.Year,tDate.Month,tDate.Day,tHr,tMin,0);
			Procedure[] procsMultApts=null;
			Procedure[] procsForOne=null;
			if(AppointmentRules.List.Length>0){
				int[] aptNums=new int[DS.Tables["Appointments"].Rows.Count];
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++){
					aptNums[i]=PIn.PInt(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());//ListDay[i].AptNum;
				}
				procsMultApts=Procedures.GetProcsMultApts(aptNums);
			}
			if(AppointmentRules.List.Length>0) {
				int[] aptNums=new int[DS.Tables["Appointments"].Rows.Count];
				for(int i=0;i<DS.Tables["Appointments"].Rows.Count;i++) {
					aptNums[i]=PIn.PInt(DS.Tables["Appointments"].Rows[i]["AptNum"].ToString());//ListDay[i].AptNum;
				}
				procsForOne=Procedures.GetProcsOneApt(apt.AptNum,procsMultApts);
//broken:
				ArrayList doubleBookedCodes=new ArrayList();
					//Appointments.GetDoubleBookedCodes(apt,ListDay,procsMultApts,procsForOne);
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
			Operatory curOp=Operatories.ListShort
				[ApptViewItems.VisOps[ContrApptSheet2.ConvertToOp(TempApptSingle.Location.X-ContrApptSheet2.Location.X)]];
			apt.Op=curOp.OperatoryNum;
			if(DoesOverlap(apt)){
				int startingOp=ApptViewItems.GetIndexOp(apt.Op);
				bool stillOverlaps=true;
				for(int i=startingOp;i<ApptViewItems.VisOps.Length;i++){
					//DefB.Short[(int)DefCat.Operatories]
					apt.Op=Operatories.ListShort[ApptViewItems.VisOps[i]].OperatoryNum;
					if(!DoesOverlap(apt)){
						stillOverlaps=false;
						break;
					}
				}
				if(stillOverlaps){
					for(int i=startingOp;i>=0;i--){
						apt.Op=Operatories.ListShort[ApptViewItems.VisOps[i]].OperatoryNum;
						if(!DoesOverlap(apt)){
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
					//PlanList=InsPlans.Refresh(FamCur);
					//PatPlanList=PatPlans.Refresh(PatCur.PatNum);
					//CovPats.Refresh(PlanList,PatPlanList);
					return;
				}
			}//end if DoesOverlap
			if(apt.AptStatus==ApptStatus.Broken && timeWasMoved){
				apt.AptStatus=ApptStatus.Scheduled;
			}
			if(curOp.ProvDentist!=0){//dentist assigned to op
				apt.ProvNum=curOp.ProvDentist;
			}
			if(curOp.ProvHygienist!=0){//hygienist assigned to op
				apt.ProvHyg=curOp.ProvHygienist;
			}
			if((curOp.ProvHygienist!=0 && curOp.IsHygiene) || (curOp.ProvDentist!=0 && !curOp.IsHygiene)){
				apt.IsHygiene=curOp.IsHygiene;
			}
			if(curOp.ClinicNum!=0) {
				apt.ClinicNum=curOp.ClinicNum;
			}
			try{
				Appointments.InsertOrUpdate(apt,aptOld,false);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
			SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,PatCurNum,
				PatCurName+", "
				+apt.ProcDescript
				+" From "
				+aptOld.AptDateTime.ToString()+", "
				+" To "
				+apt.AptDateTime.ToString());
			RefreshModulePatient(PatCurNum);
			RefreshPeriod();
			SetInvalid();
			mouseIsDown = false;
			boolAptMoved=false;
			TempApptSingle.Dispose();
			//PlanList=InsPlans.Refresh(FamCur);
			//PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			//CovPats.Refresh(PlanList,PatPlanList);
		}

		private void ContrApptSheet2_MouseLeave(object sender,EventArgs e) {
			InfoBubbleDraw(new Point(-1,-1));
			Cursor=Cursors.Default;
		}

		///<summary>Double click on appt sheet or on a single appointment.</summary>
		private void ContrApptSheet2_DoubleClick(object sender, System.EventArgs e) {
			/*mouseIsDown=false;
			if(ContrApptSheet.IsWeeklyView){//Temporary solution - double clicking opens the day in the daily view mode
				Appointments.DateSelected=WeekStartDate.AddDays(SheetClickedonDay-1);
				SetWeeklyView(false);
				return;
			}
			//this logic is a little different than mouse down for now because on the first click of a 
			//double click, an appointment control is created under the mouse.
			if(ContrApptSingle.ClickedAptNum!=0){//on appt	
				TempApptSingle.Dispose();
				//security handled inside the form
				//FormApptEdit FormAE=new FormApptEdit(AptCur);
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
							Appointments.InsertOrUpdate(apt,aptOld,false);
						}
						catch(ApplicationException ex){
							MessageBox.Show(ex.Message);
						}
					}
					ModuleSelected(apt.PatNum);
					SetInvalid();
				}
			}
			else{//not on apt, so trying to schedule an appointment
				if(!Security.IsAuthorized(Permissions.AppointmentCreate)){
					return;
				}
				FormPatientSelect FormPS=new FormPatientSelect();
				if(PatCurNum!=0){
					FormPS.InitialPatNum=PatCurNum;
				}
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					return;
				}
				if(PatCurNum==0 || FormPS.SelectedPatNum!=PatCurNum){//if the patient was changed
					//OnPatientSelected(FormPS.SelectedPatNum);
					//RefreshModuleData(FormPS.SelectedPatNum);//then pull up all the new info.
					RefreshModulePatient(FormPS.SelectedPatNum);
				}
				if(FormPS.NewPatientAdded){
					Appointment apt=new Appointment();
					apt.PatNum=PatCurNum;
					apt.IsNewPatient=true;
					apt.Pattern="/X/";
					if(PatCur.PriProv==0){
						apt.ProvNum=PrefB.GetInt("PracticeDefaultProv");
					}
					else{			
						apt.ProvNum=PatCur.PriProv;
					}
					apt.ProvHyg=PatCur.SecProv;
					apt.ClinicNum=PatCur.ClinicNum;
					apt.AptStatus=ApptStatus.Scheduled;
					DateTime d=Appointments.DateSelected;
					//minutes always rounded down.
					int minutes=(int)(ContrAppt.SheetClickedonMin/ContrApptSheet.MinPerIncr)*ContrApptSheet.MinPerIncr;
					apt.AptDateTime=new DateTime(d.Year,d.Month,d.Day,ContrAppt.SheetClickedonHour,minutes,0);
					apt.Op=SheetClickedonOp;
					try{
						Appointments.InsertOrUpdate(apt,null,true);
					}
					catch(ApplicationException ex){
						MessageBox.Show(ex.Message);
					}
					FormApptEdit FormAE=new FormApptEdit(apt.AptNum);//this is where security log entry is made
					FormAE.IsNew=true;
					FormAE.ShowDialog();
					if(FormAE.DialogResult==DialogResult.OK){
						RefreshModulePatient(PatCurNum);
						RefreshPeriod();
						SetInvalid();
					}
				}
				else{
					DisplayOtherDlg(true);//this also refreshes screen if needed.
				}
			}*/
		}

		///<summary>Displays the Other Appointments for the current patient, then refreshes screen as needed.  initialClick specifies whether the user doubleclicked on a blank time to get to this dialog.</summary>
		private void DisplayOtherDlg(bool initialClick){
	MessageBox.Show("Broken");
			/*if(PatCurNum==0){
				return;
			}
			FormApptsOther FormAO=new FormApptsOther(PatCur,FamCur);
			FormAO.InitialClick=initialClick;
			FormAO.ShowDialog();
			if(FormAO.OResult==OtherResult.Cancel){
				return;
			}
			switch(FormAO.OResult){
				case OtherResult.CopyToPinBoard:
					CurToPinBoard();
					RefreshModulePatient(PatCurNum);
					RefreshPeriod();
					break;
				case OtherResult.NewToPinBoard:
					CurToPinBoard();
					RefreshModulePatient(PatCurNum);
					RefreshPeriod();
					break;
				case OtherResult.PinboardAndSearch:
					CurToPinBoard();
					dateSearch.Text=FormAO.DateJumpToString;
					if(!groupSearch.Visible){//if search not already visible
						ShowSearch();
					}
					DoSearch();
					break;
				case OtherResult.CreateNew:
					ContrApptSingle.SelectedAptNum=FormAO.SelectedAppt.AptNum;
					RefreshModulePatient(PatCurNum);
					RefreshPeriod();
					SetInvalid();
					break;
				case OtherResult.GoTo:
					ContrApptSingle.SelectedAptNum=FormAO.SelectedAppt.AptNum;
					Appointments.DateSelected=FormAO.SelectedAppt.AptDateTime;
					RefreshModulePatient(PatCurNum);
					RefreshPeriod();
					break;
			}*/
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Lists":
						OnLists_Click();
						break;
					//case "Unsched":
					//	OnUnschedList_Click();
					//	break;
					//case "Recall":
					//	OnRecall_Click();
					//	break;
					//case "Track":
					//	OnTrack_Click();
					//	break;
					case "Print":
						OnPrint_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Patient pat=Patients.GetPat(PatCurNum);
				Programs.Execute((int)e.Button.Tag,pat);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS = new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult!=DialogResult.OK){
				return;
			}
			//OnPatientSelected(formPS.SelectedPatNum);
			//RefreshModuleData(formPS.SelectedPatNum);
			//RefreshModuleScreen();//to change name in title
			RefreshModulePatient(PatCurNum);
			DisplayOtherDlg(false);
		}

		private void OnUnschedList_Click() {
			Cursor=Cursors.WaitCursor;
			FormUnsched FormUnsched2=new FormUnsched();
			FormUnsched2.ShowDialog();
			if(FormUnsched2.PinClicked){
				CurToPinBoard();
	//this is the wrong patnum:
				RefreshModulePatient(PatCurNum);
				//RefreshPeriod();
			}
			Cursor=Cursors.Default;
		}

		private void OnRecall_Click() {
			Cursor=Cursors.WaitCursor;
			FormRecallList FormRL=new FormRecallList();
			FormRL.ShowDialog();
			if(FormRL.PinClicked){
				CurToPinBoard();
			}
			if(FormRL.SelectedPatNum!=0){
				//OnPatientSelected(FormRL.SelectedPatNum);
				ModuleSelected(FormRL.SelectedPatNum);
			}
			Cursor=Cursors.Default;
		}

		private void OnConfirm_Click() {
			Cursor=Cursors.WaitCursor;
			FormConfirmList FormC=new FormConfirmList();
			FormC.ShowDialog();
			Cursor=Cursors.Default;
			if(FormC.SelectedPatNum!=0){
				//OnPatientSelected(FormC.SelectedPatNum);
				ModuleSelected(FormC.SelectedPatNum);
			}
		}

		private void OnTrack_Click() {
			Cursor=Cursors.WaitCursor;
			FormTrackNext FormTN=new FormTrackNext();
			FormTN.ShowDialog();
			if(FormTN.PinClicked){
				CurToPinBoard();
			}
			//if(PatCur==null){
			//	ModuleSelected(0);
			//}
			//else{
				ModuleSelected(PatCurNum);
			//}
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
				pView.printPreviewControl2.Document=pd2;
  			pView.ShowDialog();
			#else
				if(!Printers.SetPrinter(pd2,PrintSituation.Appointments)){
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
				date=Appointments.DateSelected.DayOfWeek.ToString()+"   "+Appointments.DateSelected.ToShortDateString();
			}
			Font titleFont=new Font("Arial",14,FontStyle.Bold);
			float xTitle = (float)(400-((e.Graphics.MeasureString(title,titleFont).Width/2)));
			e.Graphics.DrawString(title,titleFont,Brushes.Black,xTitle,yPos);//centered
			//Print Date
 			//string date = Appointments.DateSelected.DayOfWeek.ToString()+"   "
			//	+Appointments.DateSelected.ToShortDateString();
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
      //bool IsDefault=true;
			Schedule[] SchedListDay;
			if(ContrApptSheet.IsWeeklyView) {
				SchedListDay = Schedules.RefreshPeriod(WeekStartDate,WeekEndDate);
			}
			else {
				SchedListDay=Schedules.RefreshDay(Appointments.DateSelected);
			}
      if(SchedListDay.Length > 0){
        for(int i=0;i<SchedListDay.Length;i++){
					if(SchedListDay[i].SchedType!=ScheduleType.Practice){
						continue;
					}
          AListStart.Add(SchedListDay[i].StartTime);
          AListStop.Add(SchedListDay[i].StopTime);
					//IsDefault=false;
        } 
      }
      /*if(IsDefault){	
				SchedDefault[] schedDefPract=SchedDefaults.GetForType(ScheduleType.Practice,0);
				for(int i=0;i<schedDefPract.Length;i++){
					//if(SchedDefaults.List[i]
					if(schedDefPract[i].DayOfWeek==(int)Appointments.DateSelected.DayOfWeek){
            AListStart.Add(schedDefPract[i].StartTime);
            AListStop.Add(schedDefPract[i].StopTime); 
					}
				}
      }*/
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
				StartTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month
					,Appointments.DateSelected.Day
					,ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)
					,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
					,0);
				if(ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)+12<23){
					//we will be adding an extra hour later
					StopTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month
						,Appointments.DateSelected.Day
						,ContrApptSheet2.ConvertToHour(-ContrApptSheet2.Location.Y)+12//add 12 hours
						,ContrApptSheet2.ConvertToMin(-ContrApptSheet2.Location.Y)
						,0);
				}
				else{
					StopTime=new DateTime(Appointments.DateSelected.Year,Appointments.DateSelected.Month
						,Appointments.DateSelected.Day
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
				headers[i]=Operatories.ListShort[ApptViewItems.VisOps[i]].OpName;	
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
MessageBox.Show("Not functional");
			/*if(!PinApptSingle.Visible){
				return;
			}
			PinApptSingle.Visible=false;
			//Appointment apt=PinApptSingle.Info.MyApt.Copy();
			//get the patient associate with the pinboard appt so we can test for next apt.
			//Patient PatCur=Patients.Cur;//but we don't really care
			//PatCur.PatNum=Appointments.Cur.PatNum;
			//Patients.Cur=PatCur;
			RefreshModuleData(AptCur.PatNum);
			ContrApptSingle.SelectedAptNum=-1;
			ContrApptSingle.PinBoardIsSelected=false;
			if(AptCur.AptStatus==ApptStatus.UnschedList){//on unscheduled list
				//do nothing to database
			}
			else if(AptCur.AptDateTime.Year<1880){//not already scheduled
				if(AptCur.AptNum==PatCur.NextAptNum){//if next apt
					//do nothing except remove it from pinboard
				}
				else{//for normal appt:
					//this gets rid of new appointments that never made it off the pinboard
					//Procedures.UnattachProcsInAppt(AptCur.AptNum);
					Appointments.Delete(AptCur.AptNum);
				}
			}
			//PatCur=null;
			//RefreshModuleScreen();
			RefreshModulePatient(0);*/
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
					cardPrintFamily=false;
					PrintApptCard();
					break;
				case 9:
					cardPrintFamily=true;
					PrintApptCard();
					break;
				case 10:
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
MessageBox.Show("Not functional");
			/*if(!Security.IsAuthorized(Permissions.AppointmentMove)){
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Send Appointment to Unscheduled List?")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Appointment aptOld=AptCur.Copy();
			AptCur.AptStatus=ApptStatus.UnschedList;
			try{
				Appointments.InsertOrUpdate(AptCur,aptOld,false);
				SecurityLogs.MakeLogEntry(Permissions.AppointmentMove,AptCur.PatNum,
					PatCur.GetNameLF()+", "
					+AptCur.ProcDescript+", "
					+AptCur.AptDateTime.ToString()+", "
					+"Sent to Unscheduled List");
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
			RefreshModulePatient(PatCurNum);
			RefreshPeriod();
			SetInvalid();*/
		}

		private void OnBreak_Click(){
MessageBox.Show("Not working");
			/*
			if(!Security.IsAuthorized(Permissions.AppointmentEdit)){
				return;
			}
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointment aptOld=AptCur.Copy();
			AptCur.AptStatus=ApptStatus.Broken;
			try{
				Appointments.InsertOrUpdate(AptCur,aptOld,false);
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,AptCur.PatNum,
					PatCur.GetNameLF()+", "
					+AptCur.ProcDescript+", "
					+AptCur.AptDateTime.ToString()+", "
					+"Broke");
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
			RefreshModulePatient(PatCurNum);//??
			RefreshPeriod();//??
			SetInvalid();
			
			Adjustment AdjustmentCur=new Adjustment();
			AdjustmentCur.DateEntry=DateTime.Today;
			AdjustmentCur.AdjDate=DateTime.Today;
			AdjustmentCur.ProcDate=DateTime.Today;
			AdjustmentCur.ProvNum=AptCur.ProvNum;
			AdjustmentCur.PatNum=PatCur.PatNum;
			FormAdjust FormA=new FormAdjust(PatCur,AdjustmentCur);
			FormA.IsNew=true;
			FormA.ShowDialog();*/
		}

		private void OnComplete_Click(){
			MessageBox.Show("Not working");
			/*
			if(!Security.IsAuthorized(Permissions.ProcComplCreate)){
				return;
			}
			if(!Security.IsAuthorized(Permissions.AppointmentEdit)){
				return;
			}
			int thisIndex=GetIndex(ContrApptSingle.SelectedAptNum);
			Appointment aptOld=AptCur.Copy();
			AptCur.AptStatus=ApptStatus.Complete;
			//Procedures.SetDateFirstVisit(Appointments.Cur.AptDateTime.Date);//done when making appt instead
			InsPlan[] PlanList=InsPlans.Refresh(FamCur);
			PatPlan[] PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			Procedures.SetCompleteInAppt(AptCur,PlanList,PatPlanList);//loops through each proc
			try{
				Appointments.InsertOrUpdate(AptCur,aptOld,false);
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,AptCur.PatNum,
					PatCur.GetNameLF()+", "
					+AptCur.ProcDescript+", "
					+AptCur.AptDateTime.ToString()+", "
					+"Set Complete");
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
			RefreshModulePatient(PatCurNum);
			RefreshPeriod();
			SetInvalid();
			SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
				PatCur.GetNameLF()+" "+AptCur.AptDateTime.ToShortDateString());
			//ContrApptSingle3[thisIndex].Info.MyApt.AptStatus=ApptStatus.Complete;
			//ContrApptSingle3[thisIndex].Refresh();*/
		}

		private void OnDelete_Click(){
			MessageBox.Show("Not working");
			/*
			if(!Security.IsAuthorized(Permissions.AppointmentEdit)){
				return;
			}
			if(!MsgBox.Show(this,true,"Delete Appointment?")){
				return;
			}
			//Procedures.UnattachProcsInAppt(AptCur.AptNum);
			Appointments.Delete(AptCur.AptNum);
			SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit,AptCur.PatNum,
				PatCur.GetNameLF()+", "
				+AptCur.ProcDescript+", "
				+AptCur.AptDateTime.ToString()+", "
				+"Deleted");
			ContrApptSingle.SelectedAptNum=-1;
			ContrApptSingle.PinBoardIsSelected=false;
			PatCur=null;
			RefreshModulePatient(PatCurNum);
			RefreshPeriod();
			SetInvalid();*/
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
			sched.Op=SheetClickedonOp;
			sched.SchedDate=Appointments.DateSelected;
			TimeSpan span=sched.StopTime-sched.StartTime;
			TimeSpan timeOfDay=new TimeSpan(SheetClickedonHour,SheetClickedonMin,0);
			timeOfDay=TimeSpan.FromMinutes(
				((int)Math.Round((decimal)timeOfDay.TotalMinutes/(decimal)ContrApptSheet.MinPerIncr))*ContrApptSheet.MinPerIncr);
			sched.StartTime=DateTime.Today+timeOfDay;
			sched.StopTime=sched.StartTime.Add(span);
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
			RefreshPeriod();
		}

		private Schedule GetClickedBlockout(){
			//Schedules.ConvertFromDefault(Appointments.DateSelected,ScheduleType.Blockout,0);
			SchedListDay=Schedules.RefreshDay(Appointments.DateSelected);
			Schedule[] ListForType=Schedules.GetForType(SchedListDay,ScheduleType.Blockout,0);
			//now find which blockout
			Schedule SchedCur=null;
			//date is irrelevant. This is just for the time:
			DateTime SheetClickedonTime=new DateTime(2000,1,1,SheetClickedonHour,SheetClickedonMin,0);
			for(int i=0;i<ListForType.Length;i++) {
				//skip if op doesn't match
				if(ListForType[i].Op!=0) {//if op is zero, it doesn't matter which op.
					if(ListForType[i].Op != SheetClickedonOp) {
						continue;
					}
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
			//if(!MsgBox.Show(this,true,"Delete blockout?")){
			//	return;
			//}
			Schedules.Delete(SchedCur);
			RefreshPeriod();
		}

		private void OnBlockAdd_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			//Schedules.ConvertFromDefault(Appointments.DateSelected,ScheduleType.Blockout,0);
      Schedule SchedCur=new Schedule();
      SchedCur.SchedDate=Appointments.DateSelected;
			SchedCur.SchedType=ScheduleType.Blockout;
		  FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.IsNew=true;
      FormSB.ShowDialog();
			RefreshPeriod();
		}

		private void OnBlockCutCopyPaste_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			FormBlockoutCutCopyPaste FormB=new FormBlockoutCutCopyPaste();
			FormB.DateSelected=Appointments.DateSelected;
			if(comboView.SelectedIndex==0){
				FormB.ApptViewNumCur=0;
			}
			else{
				FormB.ApptViewNumCur=ApptViews.List[comboView.SelectedIndex-1].ApptViewNum;
			}
			FormB.ShowDialog();
			RefreshPeriod();
		}

		private void OnClearBlockouts_Click(){
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			if(!MsgBox.Show(this,true,"Clear all blockouts for day?")){
				return;
			}
			Schedules.ClearBlockoutsForDay(Appointments.DateSelected);
			RefreshPeriod();
		}

		private void OnBlockTypes_Click(){
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDefinitions FormD=new FormDefinitions(DefCat.BlockoutTypes);
			FormD.ShowDialog();
			RefreshPeriod();
		}

		private void OnCopyToPin_Click() {
			MessageBox.Show("Not working");
			/*
			if(!Security.IsAuthorized(Permissions.AppointmentMove)) {
				return;
			}
			int prevSel=GetIndex(ContrApptSingle.SelectedAptNum);
			CurInfo=new InfoApt();
			CurInfo.MyApt=AptCur.Copy();
			Procedure[] procsForSingle;
			procsForSingle=Procedures.GetProcsForSingle(AptCur.AptNum,false);
			CurInfo.Procs=procsForSingle;
			CurInfo.Production=Procedures.GetProductionOneApt(AptCur.AptNum,procsForSingle,false);
			CurInfo.MyPatient=PatCur.Copy();
			CurToPinBoard();//sets selectedAptNum=-1. do before refresh prev
			if(prevSel!=-1) {
				CreateAptShadows();
				ContrApptSheet2.DrawShadow();
			}
			RefreshModulePatient(PatCurNum);//??
			RefreshPeriod();//??


			//
			//CurInfo=TempApptSingle.Info;
			//CurToPinBoard();
			//
			//PlanList=InsPlans.Refresh(FamCur);
			//PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			//CovPats.Refresh(PlanList,PatPlanList);
			//FillPanelPatient();
			//TempApptSingle.Dispose();
			//return;*/
		}

		private void textMedicalNote_TextChanged(object sender, System.EventArgs e) {
		
		}

		/*private void listConfirmed_SelectedIndexChanged(object sender, System.EventArgs e) {
			listConfirmed.SelectedIndex=-1;
		}*/

		private void listConfirmed_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			MessageBox.Show("Not working");
			/*
			if(listConfirmed.IndexFromPoint(e.X,e.Y)==-1){
				return;
			}
			Appointment aptOld=AptCur.Copy();
			AptCur.Confirmed
				=DefB.Short[(int)DefCat.ApptConfirmed][listConfirmed.IndexFromPoint(e.X,e.Y)].DefNum;
			try{
				Appointments.InsertOrUpdate(AptCur,aptOld,false);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
			RefreshPeriod();//this is used because we are not changing the patient.
			//RefreshModuleScreen();
			//ModuleSelected();
			SetInvalid();*/
		}

		private void butSearch_Click(object sender, System.EventArgs e) {
			if(!PinApptSingle.Visible){
				MsgBox.Show(this,"An appointment must be placed on the pinboard before a search can be done.");
				return;
			}
			if(!groupSearch.Visible){//if search not already visible
				dateSearch.Text=DateTime.Today.ToShortDateString();
				ShowSearch();
			}
			DoSearch();
		}

		///<summary>Positions the search box, fills it with initial data except date, and makes it visible.</summary>
		private void ShowSearch(){
			MessageBox.Show("Not working");
			/*
			groupSearch.Location=new Point(panelCalendar.Location.X,panelCalendar.Location.Y+282);
			//if(!groupSearch.Visible){//if search not already visible, 
			textBefore.Text="";
			textAfter.Text="";
			listProviders.Items.Clear();
			for(int i=0;i<Providers.List.Length;i++){
				listProviders.Items.Add(Providers.List[i].Abbr);
				if(PinApptSingle.Info.MyApt.IsHygiene
					&& Providers.List[i].ProvNum==PinApptSingle.Info.MyApt.ProvHyg)
				{
					listProviders.SetSelected(i,true);
				}
				else if(!PinApptSingle.Info.MyApt.IsHygiene
					&& Providers.List[i].ProvNum==PinApptSingle.Info.MyApt.ProvNum)
				{
					listProviders.SelectedIndex=i;
				}
			}
			//}
			groupSearch.Visible=true;*/
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
					beforeTime=TimeSpan.FromHours(PIn.PDouble(textBefore.Text));
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
					afterTime=TimeSpan.FromHours(PIn.PDouble(textAfter.Text));
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
			int[] providers=new int[listProviders.SelectedIndices.Count];
			for(int i=0;i<providers.Length;i++){
				providers[i]=Providers.List[listProviders.SelectedIndices[i]].ProvNum;
			}
			//the result set is never empty
//broken:
			/*SearchResults=Appointments.GetSearchResults(PinApptSingle.Info.MyApt,afterDate,providers,10,beforeTime,afterTime);
			listSearchResults.Items.Clear();
			for(int i=0;i<SearchResults.Length;i++){
				listSearchResults.Items.Add(
					SearchResults[i].ToString("ddd")+"\t"+SearchResults[i].ToShortDateString()+"     "+SearchResults[i].ToShortTimeString());
			}
			listSearchResults.SetSelected(0,true);
			RefreshPeriod(SearchResults[0],SearchResults[0]);//jump to that day.*/
			//RefreshDay(SearchResults[0]);//jump to that day.
			Cursor=Cursors.Default;
			//scroll to make visible?
			//highlight schedule?
		}

		private void butSearchMore_Click(object sender, System.EventArgs e) {
			dateSearch.Text=SearchResults[SearchResults.Length-1].ToShortDateString();
			DoSearch();
		}

		private void listSearchResults_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			int clickedI=listSearchResults.IndexFromPoint(e.X,e.Y);
			if(clickedI==-1){
				return;
			}
			Appointments.DateSelected=SearchResults[clickedI];
			SetWeeklyView(false);
			//RefreshPeriod(SearchResults[clickedI],SearchResults[clickedI]);
			//RefreshDay(SearchResults[clickedI]);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
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
		}

		//private void timerTimeIndic_Tick(object sender, System.EventArgs e) {
			//if(FormOpenDental.ActiveForm.WindowState!=FormWindowState.Minimized){
		//}

		///<summary></summary>
		public void TickRefresh(){
			try{
				Schedule[] schedListDay=Schedules.RefreshDay(Appointments.DateSelected);
				ContrApptSheet2.SchedListDay=schedListDay;
				ContrApptSheet2.CreateShadow();
				CreateAptShadows();
				ContrApptSheet2.DrawShadow();
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
			if(Printers.SetPrinter(pd2,PrintSituation.Postcard)){
				pd2.Print();
			}
		}
			
		private void pd2_PrintApptCard(object sender, PrintPageEventArgs ev){
			Graphics g=ev.Graphics;
			//Return Address--------------------------------------------------------------------------
			string str=PrefB.GetString("PracticeTitle")+"\r\n";
			g.DrawString(str,new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold),Brushes.Black,60,60);
			str=PrefB.GetString("PracticeAddress")+"\r\n";
			if(PrefB.GetString("PracticeAddress2")!=""){
				str+=PrefB.GetString("PracticeAddress2")+"\r\n";
			}
			str+=PrefB.GetString("PracticeCity")+"  "
				+PrefB.GetString("PracticeST")+"  "
				+PrefB.GetString("PracticeZip")+"\r\n";
			string phone=PrefB.GetString("PracticePhone");
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
			Family fam=Patients.GetFamily(PatCurNum);
			Patient pat=fam.GetPatient(PatCurNum);
			for(int i=0;i<fam.List.Length;i++){
				if(!cardPrintFamily && fam.List[i].PatNum!=pat.PatNum){
					continue;
				}
				name=fam.List[i].FName;
				if(name.Length>15){//trim name so it won't be too long
					name=name.Substring(0,15);
				}
				aptsOnePat=Appointments.GetForPat(fam.List[i].PatNum);
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
				guar=fam.List[0].Copy();
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
			CommlogCur.CommType=CommItemType.Misc;
			CommlogCur.Note="Appointment card sent";
			CommlogCur.PatNum=pat.PatNum;
			//there is no dialog here because it is just a simple entry
			Commlogs.Insert(CommlogCur);
			ev.HasMorePages = false;
		}

		

		

		

		

		

		

		
		

		

	


		

	}

	//<summary>This is the info to display on an appointment. This will be eliminated when we move to datasets.</summary>
	//public class InfoApt{
		//<summary>Set to true if this appointment is simply being displayed in the Chart module Next apt section rather than int the Appointments module.</summary>
		//public bool IsNext;
		//<summary>A list of procedures attached to this appointment.</summary>
		//public Procedure[] Procs;
		//<summary>The appointment object holding the actual db info for this displayed appointment.</summary>
		//public Appointment MyApt;
		//<summary>The patient associated with this appt.</summary>
		//public Patient MyPatient;
		//<summary>The amount of money generated from this appointment.</summary>
		//public double Production;
		//<Summary>Could be null.</Summary>
		//public LabCase MyLabCase;
	//}


}













