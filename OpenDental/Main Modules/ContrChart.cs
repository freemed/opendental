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
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.UI;
using Tao.Platform.Windows;
using SparksToothChart;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
///<summary></summary>
	public class ContrChart : System.Windows.Forms.UserControl	{
		private OpenDental.UI.Button butAddProc;
		private OpenDental.UI.Button butM;
		private OpenDental.UI.Button butOI;
		private OpenDental.UI.Button butD;
		private OpenDental.UI.Button butBF;
		private OpenDental.UI.Button butL;
		private OpenDental.UI.Button butV;
		private System.Windows.Forms.TextBox textSurf;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioEntryTP;
		private System.Windows.Forms.RadioButton radioEntryEO;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.RadioButton radioEntryEC;
		private ProcStat newStatus;
		private OpenDental.UI.Button button1;
		private System.Windows.Forms.RadioButton radioEntryC;
		//private bool dataValid=false;
		private System.Windows.Forms.ListBox listDx;
		private int[] hiLightedRows=new int[1];
		//private ContrApptSingle ApptPlanned;
		private System.Windows.Forms.CheckBox checkDone;
		private System.Windows.Forms.RadioButton radioEntryR;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.CheckBox checkNotes;
		private System.Windows.Forms.CheckBox checkShowR;
		private OpenDental.UI.Button butShowNone;
		private OpenDental.UI.Button butShowAll;
		private System.Windows.Forms.CheckBox checkShowE;
		private System.Windows.Forms.CheckBox checkShowC;
		private System.Windows.Forms.CheckBox checkShowTP;
		private System.Windows.Forms.CheckBox checkRx;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.TextBox textProcCode;
		private System.Windows.Forms.Label label14;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butNew;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.Label labelDx;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ComboBox comboPriority;
		private System.Windows.Forms.ContextMenu menuProgRight;
		private System.Windows.Forms.MenuItem menuItemPrintProg;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabControl tabControlImages;
		private System.Windows.Forms.Panel panelImages;
		///<summary>public for plugins</summary>
		public bool TreatmentNoteChanged;
		///<summary>Keeps track of which tab is selected. It's the index of the selected tab.</summary>
		private int selectedImageTab=0;
		private bool MouseIsDownOnImageSplitter;
		private int ImageSplitterOriginalY;
		private int OriginalImageMousePos;
		private System.Windows.Forms.ImageList imageListThumbnails;
		private System.Windows.Forms.ListView listViewImages;
		///<summary>The indices of the image categories which are visible in Chart.</summary>
		private ArrayList visImageCats;
		///<summary>The indices within Documents.List[i] of docs which are visible in Chart.</summary>
		private ArrayList visImages;
		///<summary>Full path to the patient folder, including \ on the end</summary>
		private string patFolder;
		private OpenDental.ODtextBox textTreatmentNotes;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkToday;
		private FormImageViewer formImageViewer;
		private Family FamCur;
		private Patient PatCur;
		private List <InsPlan> PlanList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		///<summary>For one patient. Allows highlighting rows.</summary>
		private Appointment[] ApptList;
		private System.Drawing.Printing.PrintDocument pd2;
		private int pagesPrinted;
		private System.Windows.Forms.CheckBox checkShowTeeth;//used in printing progress notes
		private bool headingPrinted;
		private int headingPrintH;
		private Document[] DocumentList;
		private List <PatPlan> PatPlanList;
		private MenuItem menuItemSetComplete;
		private MenuItem menuItemEditSelected;
		private MenuItem menuItemGroupSelected;
		private OpenDental.UI.Button butPin;
		private ListBox listButtonCats;
		private ListView listViewButtons;
		private List <Benefit> BenefitList;
		private ImageList imageListProcButtons;
		private ColumnHeader columnHeader1;
		private TabControl tabProc;
		private TabPage tabEnterTx;
		private TabPage tabMissing;
		private ProcButton[] ProcButtonList;
		private TabPage tabPrimary;
		private TabPage tabMovements;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butUnhide;
		private Label label5;
		private ListBox listHidden;
		private OpenDental.UI.Button butEdentulous;
		private Label label7;
		private OpenDental.UI.Button butNotMissing;
		private OpenDental.UI.Button butMissing;
		private OpenDental.UI.Button butHidden;
		private GroupBox groupBox3;
		private Label label8;
		private GroupBox groupBox4;
		private ValidDouble textTipB;
		private Label label11;
		private ValidDouble textTipM;
		private Label label12;
		private ValidDouble textRotate;
		private Label label15;
		private ValidDouble textShiftB;
		private Label label10;
		private ValidDouble textShiftO;
		private Label label9;
		private ValidDouble textShiftM;
		private OpenDental.UI.Button butRotatePlus;
		private OpenDental.UI.Button butMixed;
		private OpenDental.UI.Button butAllPrimary;
		private OpenDental.UI.Button butAllPerm;
		private GroupBox groupBox5;
		private OpenDental.UI.Button butPerm;
		private OpenDental.UI.Button butPrimary;
		private int SelectedProcTab;
		private OpenDental.UI.Button butTipBplus;
		private OpenDental.UI.Button butTipMplus;
		private OpenDental.UI.Button butShiftBplus;
		private OpenDental.UI.Button butShiftOplus;
		private OpenDental.UI.Button butShiftMplus;
		private Label label16;
		private OpenDental.UI.Button butApplyMovements;
		private OpenDental.UI.Button butTipBminus;
		private OpenDental.UI.Button butTipMminus;
		private OpenDental.UI.Button butRotateMinus;
		private OpenDental.UI.Button butShiftBminus;
		private OpenDental.UI.Button butShiftOminus;
		private OpenDental.UI.Button butShiftMminus;
		private ODGrid gridProg;
		private ODGrid gridPtInfo;
		private CheckBox checkComm;
		private List<ToothInitial> ToothInitialList;
		private MenuItem menuItemPrintDay;
		///<summary>a list of the hidden teeth as strings. Includes "1"-"32", and "A"-"Z"</summary>
		private ArrayList HiddenTeeth;
		private CheckBox checkAudit;
		///<summary>This date will usually have minVal except while the hospital print function is running.</summary>
		private DateTime hospitalDate;
		private PatientNote PatientNoteCur;
		private DataSet DataSetMain;
		private MenuItem menuItemLabFee;
		private MenuItem menuItemLabFeeDetach;
		private MenuItem menuItemDelete;
		private ToothChartWrapper toothChart;
		private Panel panelQuickButtons;
		private OpenDental.UI.Button buttonCSeal;
		private OpenDental.UI.Button buttonCMO;
		private OpenDental.UI.Button buttonCMOD;
		private OpenDental.UI.Button buttonCO;
		private Label label23;
		private OpenDental.UI.Button buttonCDO;
		private OpenDental.UI.Button butCOB;
		private OpenDental.UI.Button butCOL;
		private OpenDental.UI.Button butML;
		private OpenDental.UI.Button butDL;
		///<summary>A subset of DataSetMain.  The procedures that need to be drawn in the graphical tooth chart.</summary>
		List<DataRow> ProcList;
		//private int lastPatNum;
		private OpenDental.UI.Button butCMDL;
		private Label label1;
		private TabPage tabPlanned;
		private TabPage tabShow;
		private GroupBox groupBox7;
		private GroupBox groupBox6;
		private CheckBox checkAppt;
		private CheckBox checkLabCase;
		private OpenDental.UI.Button buttonCMODL;
		private OpenDental.UI.Button buttonCMODB;
		private OpenDental.UI.Button butAddKey;
		//private MenuItem menuItemDeleteSelected;
		private CheckBox checkCommFamily;
		private OpenDental.UI.Button butForeignKey;
		private CheckBox checkTasks;
		private CheckBox checkEmail;
		private long PrevPtNum;
		private CheckBox checkSheets;
		private TabPage tabDraw;
		private RadioButton radioPointer;
		private RadioButton radioEraser;
		private RadioButton radioPen;
		private Panel panelDrawColor;
		private GroupBox groupBox8;
		private Panel panelTPlight;
		private Panel panelTPdark;
		private Label label18;
		private OpenDental.UI.Button butColorOther;
		private Panel panelRdark;
		private Label label21;
		private Panel panelRlight;
		private Panel panelEOdark;
		private Label label20;
		private Panel panelEOlight;
		private Panel panelECdark;
		private Label label19;
		private Panel panelEClight;
		private Panel panelCdark;
		private Label label17;
		private Panel panelClight;
		private RadioButton radioColorChanger;
		private Panel panelBlack;
		private Label label22;
		private Panel panelQuickPasteAmalgam;
		private OpenDental.UI.Button buttonAMODB;
		private OpenDental.UI.Button buttonAMODL;
		private OpenDental.UI.Button buttonAOB;
		private OpenDental.UI.Button buttonAOL;
		private OpenDental.UI.Button buttonAO;
		private OpenDental.UI.Button buttonAMO;
		private OpenDental.UI.Button buttonAMOD;
		private OpenDental.UI.Button buttonADO;
		private Label label24;
		private ODGrid gridPlanned;
		private ContextMenu menuConsent;
		private RadioButton radioEntryCn;
		private CheckBox checkShowCn;
		private int Chartscrollval;
		private OpenDental.UI.Button butECWdown;
		private OpenDental.UI.Button butECWup;
		private WebBrowser webBrowserEcw;
		private Panel panelEcw;
		private Label labelECWerror;
		private ContextMenu menuToothChart;
		private MenuItem menuItemChartBig;
		private MenuItem menuItemChartSave;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private PatField[] PatFieldList;
		private OpenDental.UI.Button butChartViewDown;
		private OpenDental.UI.Button butChartViewUp;
		private OpenDental.UI.Button butChartViewAdd;
		private Label labelCustView;
		private ODGrid gridChartViews;
		private bool InitializedOnStartup;
		//<summary>Can be null if user has not set up any views.  Defaults to first in list when starting up.</summary>
		private ChartView ChartViewCurDisplay;
		private bool chartCustViewChanged;
	
		///<summary></summary>
		public ContrChart(){
			Logger.openlog.Log("Initializing chart module...",Logger.Severity.INFO);
			InitializeComponent();
			tabControlImages.DrawItem += new DrawItemEventHandler(OnDrawItem);
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canada
				panelQuickButtons.Enabled=false;
				butBF.Text=Lan.g(this,"B/V");//vestibular instead of facial
				butV.Text=Lan.g(this,"5");
			}
			else{
				menuItemLabFee.Visible=false;
				menuItemLabFeeDetach.Visible=false;
			}
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
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrChart));
			SparksToothChart.ToothChartData toothChartData2 = new SparksToothChart.ToothChartData();
			this.textSurf = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioEntryCn = new System.Windows.Forms.RadioButton();
			this.radioEntryR = new System.Windows.Forms.RadioButton();
			this.radioEntryC = new System.Windows.Forms.RadioButton();
			this.radioEntryEO = new System.Windows.Forms.RadioButton();
			this.radioEntryEC = new System.Windows.Forms.RadioButton();
			this.radioEntryTP = new System.Windows.Forms.RadioButton();
			this.listDx = new System.Windows.Forms.ListBox();
			this.labelDx = new System.Windows.Forms.Label();
			this.checkDone = new System.Windows.Forms.CheckBox();
			this.listViewButtons = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.imageListProcButtons = new System.Windows.Forms.ImageList(this.components);
			this.listButtonCats = new System.Windows.Forms.ListBox();
			this.comboPriority = new System.Windows.Forms.ComboBox();
			this.checkToday = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textProcCode = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.checkAudit = new System.Windows.Forms.CheckBox();
			this.checkComm = new System.Windows.Forms.CheckBox();
			this.checkShowTeeth = new System.Windows.Forms.CheckBox();
			this.checkNotes = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkShowR = new System.Windows.Forms.CheckBox();
			this.checkShowE = new System.Windows.Forms.CheckBox();
			this.checkShowC = new System.Windows.Forms.CheckBox();
			this.checkShowTP = new System.Windows.Forms.CheckBox();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.menuProgRight = new System.Windows.Forms.ContextMenu();
			this.menuItemDelete = new System.Windows.Forms.MenuItem();
			this.menuItemSetComplete = new System.Windows.Forms.MenuItem();
			this.menuItemEditSelected = new System.Windows.Forms.MenuItem();
			this.menuItemGroupSelected = new System.Windows.Forms.MenuItem();
			this.menuItemPrintProg = new System.Windows.Forms.MenuItem();
			this.menuItemPrintDay = new System.Windows.Forms.MenuItem();
			this.menuItemLabFeeDetach = new System.Windows.Forms.MenuItem();
			this.menuItemLabFee = new System.Windows.Forms.MenuItem();
			this.tabControlImages = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.panelImages = new System.Windows.Forms.Panel();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.imageListThumbnails = new System.Windows.Forms.ImageList(this.components);
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.tabProc = new System.Windows.Forms.TabControl();
			this.tabEnterTx = new System.Windows.Forms.TabPage();
			this.panelQuickButtons = new System.Windows.Forms.Panel();
			this.panelQuickPasteAmalgam = new System.Windows.Forms.Panel();
			this.buttonAMODB = new OpenDental.UI.Button();
			this.buttonAMODL = new OpenDental.UI.Button();
			this.buttonAOB = new OpenDental.UI.Button();
			this.buttonAOL = new OpenDental.UI.Button();
			this.buttonAO = new OpenDental.UI.Button();
			this.buttonAMO = new OpenDental.UI.Button();
			this.buttonAMOD = new OpenDental.UI.Button();
			this.buttonADO = new OpenDental.UI.Button();
			this.label24 = new System.Windows.Forms.Label();
			this.buttonCMODB = new OpenDental.UI.Button();
			this.buttonCMODL = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butCMDL = new OpenDental.UI.Button();
			this.butML = new OpenDental.UI.Button();
			this.butDL = new OpenDental.UI.Button();
			this.butCOB = new OpenDental.UI.Button();
			this.butCOL = new OpenDental.UI.Button();
			this.buttonCSeal = new OpenDental.UI.Button();
			this.buttonCMO = new OpenDental.UI.Button();
			this.buttonCMOD = new OpenDental.UI.Button();
			this.buttonCO = new OpenDental.UI.Button();
			this.label23 = new System.Windows.Forms.Label();
			this.buttonCDO = new OpenDental.UI.Button();
			this.butD = new OpenDental.UI.Button();
			this.textDate = new OpenDental.ValidDate();
			this.butBF = new OpenDental.UI.Button();
			this.butL = new OpenDental.UI.Button();
			this.butM = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butAddProc = new OpenDental.UI.Button();
			this.butV = new OpenDental.UI.Button();
			this.butOI = new OpenDental.UI.Button();
			this.tabMissing = new System.Windows.Forms.TabPage();
			this.butUnhide = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.listHidden = new System.Windows.Forms.ListBox();
			this.butEdentulous = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.butNotMissing = new OpenDental.UI.Button();
			this.butMissing = new OpenDental.UI.Button();
			this.butHidden = new OpenDental.UI.Button();
			this.tabMovements = new System.Windows.Forms.TabPage();
			this.label16 = new System.Windows.Forms.Label();
			this.butApplyMovements = new OpenDental.UI.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.butTipBplus = new OpenDental.UI.Button();
			this.butTipBminus = new OpenDental.UI.Button();
			this.butTipMplus = new OpenDental.UI.Button();
			this.butTipMminus = new OpenDental.UI.Button();
			this.butRotatePlus = new OpenDental.UI.Button();
			this.butRotateMinus = new OpenDental.UI.Button();
			this.textTipB = new OpenDental.ValidDouble();
			this.label11 = new System.Windows.Forms.Label();
			this.textTipM = new OpenDental.ValidDouble();
			this.label12 = new System.Windows.Forms.Label();
			this.textRotate = new OpenDental.ValidDouble();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.butShiftBplus = new OpenDental.UI.Button();
			this.butShiftBminus = new OpenDental.UI.Button();
			this.butShiftOplus = new OpenDental.UI.Button();
			this.butShiftOminus = new OpenDental.UI.Button();
			this.butShiftMplus = new OpenDental.UI.Button();
			this.butShiftMminus = new OpenDental.UI.Button();
			this.textShiftB = new OpenDental.ValidDouble();
			this.label10 = new System.Windows.Forms.Label();
			this.textShiftO = new OpenDental.ValidDouble();
			this.label9 = new System.Windows.Forms.Label();
			this.textShiftM = new OpenDental.ValidDouble();
			this.label8 = new System.Windows.Forms.Label();
			this.tabPrimary = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.butPerm = new OpenDental.UI.Button();
			this.butPrimary = new OpenDental.UI.Button();
			this.butMixed = new OpenDental.UI.Button();
			this.butAllPrimary = new OpenDental.UI.Button();
			this.butAllPerm = new OpenDental.UI.Button();
			this.tabPlanned = new System.Windows.Forms.TabPage();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butPin = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.butNew = new OpenDental.UI.Button();
			this.gridPlanned = new OpenDental.UI.ODGrid();
			this.tabShow = new System.Windows.Forms.TabPage();
			this.gridChartViews = new OpenDental.UI.ODGrid();
			this.labelCustView = new System.Windows.Forms.Label();
			this.butChartViewDown = new OpenDental.UI.Button();
			this.butChartViewUp = new OpenDental.UI.Button();
			this.butChartViewAdd = new OpenDental.UI.Button();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.checkSheets = new System.Windows.Forms.CheckBox();
			this.checkTasks = new System.Windows.Forms.CheckBox();
			this.checkEmail = new System.Windows.Forms.CheckBox();
			this.checkCommFamily = new System.Windows.Forms.CheckBox();
			this.checkAppt = new System.Windows.Forms.CheckBox();
			this.checkLabCase = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkShowCn = new System.Windows.Forms.CheckBox();
			this.butShowAll = new OpenDental.UI.Button();
			this.butShowNone = new OpenDental.UI.Button();
			this.tabDraw = new System.Windows.Forms.TabPage();
			this.radioColorChanger = new System.Windows.Forms.RadioButton();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.panelBlack = new System.Windows.Forms.Panel();
			this.label22 = new System.Windows.Forms.Label();
			this.butColorOther = new OpenDental.UI.Button();
			this.panelRdark = new System.Windows.Forms.Panel();
			this.label21 = new System.Windows.Forms.Label();
			this.panelRlight = new System.Windows.Forms.Panel();
			this.panelEOdark = new System.Windows.Forms.Panel();
			this.label20 = new System.Windows.Forms.Label();
			this.panelEOlight = new System.Windows.Forms.Panel();
			this.panelECdark = new System.Windows.Forms.Panel();
			this.label19 = new System.Windows.Forms.Label();
			this.panelEClight = new System.Windows.Forms.Panel();
			this.panelCdark = new System.Windows.Forms.Panel();
			this.label17 = new System.Windows.Forms.Label();
			this.panelClight = new System.Windows.Forms.Panel();
			this.panelTPdark = new System.Windows.Forms.Panel();
			this.label18 = new System.Windows.Forms.Label();
			this.panelTPlight = new System.Windows.Forms.Panel();
			this.panelDrawColor = new System.Windows.Forms.Panel();
			this.radioEraser = new System.Windows.Forms.RadioButton();
			this.radioPen = new System.Windows.Forms.RadioButton();
			this.radioPointer = new System.Windows.Forms.RadioButton();
			this.menuConsent = new System.Windows.Forms.ContextMenu();
			this.panelEcw = new System.Windows.Forms.Panel();
			this.labelECWerror = new System.Windows.Forms.Label();
			this.webBrowserEcw = new System.Windows.Forms.WebBrowser();
			this.butECWdown = new OpenDental.UI.Button();
			this.butECWup = new OpenDental.UI.Button();
			this.menuToothChart = new System.Windows.Forms.ContextMenu();
			this.menuItemChartBig = new System.Windows.Forms.MenuItem();
			this.menuItemChartSave = new System.Windows.Forms.MenuItem();
			this.toothChart = new SparksToothChart.ToothChartWrapper();
			this.butForeignKey = new OpenDental.UI.Button();
			this.butAddKey = new OpenDental.UI.Button();
			this.gridProg = new OpenDental.UI.ODGrid();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.button1 = new OpenDental.UI.Button();
			this.textTreatmentNotes = new OpenDental.ODtextBox();
			this.gridPtInfo = new OpenDental.UI.ODGrid();
			this.groupBox2.SuspendLayout();
			this.tabControlImages.SuspendLayout();
			this.panelImages.SuspendLayout();
			this.tabProc.SuspendLayout();
			this.tabEnterTx.SuspendLayout();
			this.panelQuickButtons.SuspendLayout();
			this.panelQuickPasteAmalgam.SuspendLayout();
			this.tabMissing.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabMovements.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPrimary.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.tabPlanned.SuspendLayout();
			this.tabShow.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.tabDraw.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.panelEcw.SuspendLayout();
			this.SuspendLayout();
			// 
			// textSurf
			// 
			this.textSurf.BackColor = System.Drawing.SystemColors.Window;
			this.textSurf.Location = new System.Drawing.Point(8,2);
			this.textSurf.Name = "textSurf";
			this.textSurf.ReadOnly = true;
			this.textSurf.Size = new System.Drawing.Size(72,20);
			this.textSurf.TabIndex = 25;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox2.Controls.Add(this.radioEntryCn);
			this.groupBox2.Controls.Add(this.radioEntryR);
			this.groupBox2.Controls.Add(this.radioEntryC);
			this.groupBox2.Controls.Add(this.radioEntryEO);
			this.groupBox2.Controls.Add(this.radioEntryEC);
			this.groupBox2.Controls.Add(this.radioEntryTP);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(1,85);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(88,110);
			this.groupBox2.TabIndex = 35;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Entry Status";
			// 
			// radioEntryCn
			// 
			this.radioEntryCn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryCn.Location = new System.Drawing.Point(2,91);
			this.radioEntryCn.Name = "radioEntryCn";
			this.radioEntryCn.Size = new System.Drawing.Size(75,16);
			this.radioEntryCn.TabIndex = 5;
			this.radioEntryCn.Text = "Condition";
			this.radioEntryCn.CheckedChanged += new System.EventHandler(this.radioEntryCn_CheckedChanged);
			// 
			// radioEntryR
			// 
			this.radioEntryR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryR.Location = new System.Drawing.Point(2,76);
			this.radioEntryR.Name = "radioEntryR";
			this.radioEntryR.Size = new System.Drawing.Size(75,16);
			this.radioEntryR.TabIndex = 4;
			this.radioEntryR.Text = "Referred";
			this.radioEntryR.CheckedChanged += new System.EventHandler(this.radioEntryR_CheckedChanged);
			// 
			// radioEntryC
			// 
			this.radioEntryC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryC.Location = new System.Drawing.Point(2,31);
			this.radioEntryC.Name = "radioEntryC";
			this.radioEntryC.Size = new System.Drawing.Size(74,16);
			this.radioEntryC.TabIndex = 3;
			this.radioEntryC.Text = "Complete";
			this.radioEntryC.CheckedChanged += new System.EventHandler(this.radioEntryC_CheckedChanged);
			// 
			// radioEntryEO
			// 
			this.radioEntryEO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryEO.Location = new System.Drawing.Point(2,61);
			this.radioEntryEO.Name = "radioEntryEO";
			this.radioEntryEO.Size = new System.Drawing.Size(72,16);
			this.radioEntryEO.TabIndex = 2;
			this.radioEntryEO.Text = "ExistOther";
			this.radioEntryEO.CheckedChanged += new System.EventHandler(this.radioEntryEO_CheckedChanged);
			// 
			// radioEntryEC
			// 
			this.radioEntryEC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryEC.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.radioEntryEC.Location = new System.Drawing.Point(2,46);
			this.radioEntryEC.Name = "radioEntryEC";
			this.radioEntryEC.Size = new System.Drawing.Size(84,16);
			this.radioEntryEC.TabIndex = 1;
			this.radioEntryEC.Text = "ExistCurProv";
			this.radioEntryEC.CheckedChanged += new System.EventHandler(this.radioEntryEC_CheckedChanged);
			// 
			// radioEntryTP
			// 
			this.radioEntryTP.Checked = true;
			this.radioEntryTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioEntryTP.Location = new System.Drawing.Point(2,16);
			this.radioEntryTP.Name = "radioEntryTP";
			this.radioEntryTP.Size = new System.Drawing.Size(77,16);
			this.radioEntryTP.TabIndex = 0;
			this.radioEntryTP.TabStop = true;
			this.radioEntryTP.Text = "TreatPlan";
			this.radioEntryTP.CheckedChanged += new System.EventHandler(this.radioEntryTP_CheckedChanged);
			// 
			// listDx
			// 
			this.listDx.Location = new System.Drawing.Point(91,16);
			this.listDx.Name = "listDx";
			this.listDx.Size = new System.Drawing.Size(94,173);
			this.listDx.TabIndex = 46;
			this.listDx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listDx_MouseDown);
			// 
			// labelDx
			// 
			this.labelDx.Location = new System.Drawing.Point(89,-2);
			this.labelDx.Name = "labelDx";
			this.labelDx.Size = new System.Drawing.Size(100,18);
			this.labelDx.TabIndex = 47;
			this.labelDx.Text = "Diagnosis";
			this.labelDx.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkDone
			// 
			this.checkDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDone.Location = new System.Drawing.Point(413,5);
			this.checkDone.Name = "checkDone";
			this.checkDone.Size = new System.Drawing.Size(67,16);
			this.checkDone.TabIndex = 0;
			this.checkDone.Text = "Done";
			this.checkDone.Click += new System.EventHandler(this.checkDone_Click);
			// 
			// listViewButtons
			// 
			this.listViewButtons.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listViewButtons.AutoArrange = false;
			this.listViewButtons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listViewButtons.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewButtons.Location = new System.Drawing.Point(311,40);
			this.listViewButtons.MultiSelect = false;
			this.listViewButtons.Name = "listViewButtons";
			this.listViewButtons.Size = new System.Drawing.Size(178,192);
			this.listViewButtons.SmallImageList = this.imageListProcButtons;
			this.listViewButtons.TabIndex = 188;
			this.listViewButtons.UseCompatibleStateImageBehavior = false;
			this.listViewButtons.View = System.Windows.Forms.View.Details;
			this.listViewButtons.Click += new System.EventHandler(this.listViewButtons_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 155;
			// 
			// imageListProcButtons
			// 
			this.imageListProcButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProcButtons.ImageStream")));
			this.imageListProcButtons.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListProcButtons.Images.SetKeyName(0,"deposit.gif");
			// 
			// listButtonCats
			// 
			this.listButtonCats.IntegralHeight = false;
			this.listButtonCats.Location = new System.Drawing.Point(187,40);
			this.listButtonCats.MultiColumn = true;
			this.listButtonCats.Name = "listButtonCats";
			this.listButtonCats.Size = new System.Drawing.Size(122,192);
			this.listButtonCats.TabIndex = 59;
			this.listButtonCats.Click += new System.EventHandler(this.listButtonCats_Click);
			// 
			// comboPriority
			// 
			this.comboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriority.Location = new System.Drawing.Point(91,211);
			this.comboPriority.MaxDropDownItems = 40;
			this.comboPriority.Name = "comboPriority";
			this.comboPriority.Size = new System.Drawing.Size(96,21);
			this.comboPriority.TabIndex = 54;
			// 
			// checkToday
			// 
			this.checkToday.Checked = true;
			this.checkToday.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkToday.Location = new System.Drawing.Point(1,194);
			this.checkToday.Name = "checkToday";
			this.checkToday.Size = new System.Drawing.Size(80,18);
			this.checkToday.TabIndex = 58;
			this.checkToday.Text = "Today";
			this.checkToday.CheckedChanged += new System.EventHandler(this.checkToday_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(89,192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79,17);
			this.label6.TabIndex = 57;
			this.label6.Text = "Priority";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textProcCode
			// 
			this.textProcCode.Location = new System.Drawing.Point(330,3);
			this.textProcCode.Name = "textProcCode";
			this.textProcCode.Size = new System.Drawing.Size(108,20);
			this.textProcCode.TabIndex = 50;
			this.textProcCode.Text = "Type Proc Code";
			this.textProcCode.TextChanged += new System.EventHandler(this.textProcCode_TextChanged);
			this.textProcCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textProcCode_KeyDown);
			this.textProcCode.Enter += new System.EventHandler(this.textProcCode_Enter);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(282,5);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(45,17);
			this.label14.TabIndex = 51;
			this.label14.Text = "Or";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(308,21);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(128,18);
			this.label13.TabIndex = 49;
			this.label13.Text = "Or Single Click:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkAudit
			// 
			this.checkAudit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAudit.Location = new System.Drawing.Point(165,187);
			this.checkAudit.Name = "checkAudit";
			this.checkAudit.Size = new System.Drawing.Size(73,13);
			this.checkAudit.TabIndex = 17;
			this.checkAudit.Text = "Audit";
			this.checkAudit.Click += new System.EventHandler(this.checkAudit_Click);
			// 
			// checkComm
			// 
			this.checkComm.Checked = true;
			this.checkComm.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkComm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkComm.Location = new System.Drawing.Point(10,33);
			this.checkComm.Name = "checkComm";
			this.checkComm.Size = new System.Drawing.Size(102,13);
			this.checkComm.TabIndex = 16;
			this.checkComm.Text = "Comm Log";
			this.checkComm.Click += new System.EventHandler(this.checkComm_Click);
			// 
			// checkShowTeeth
			// 
			this.checkShowTeeth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTeeth.Location = new System.Drawing.Point(165,168);
			this.checkShowTeeth.Name = "checkShowTeeth";
			this.checkShowTeeth.Size = new System.Drawing.Size(104,13);
			this.checkShowTeeth.TabIndex = 15;
			this.checkShowTeeth.Text = "Selected Teeth";
			this.checkShowTeeth.Click += new System.EventHandler(this.checkShowTeeth_Click);
			// 
			// checkNotes
			// 
			this.checkNotes.AllowDrop = true;
			this.checkNotes.Checked = true;
			this.checkNotes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkNotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNotes.Location = new System.Drawing.Point(15,105);
			this.checkNotes.Name = "checkNotes";
			this.checkNotes.Size = new System.Drawing.Size(102,13);
			this.checkNotes.TabIndex = 11;
			this.checkNotes.Text = "Proc Notes";
			this.checkNotes.Click += new System.EventHandler(this.checkNotes_Click);
			// 
			// checkRx
			// 
			this.checkRx.Checked = true;
			this.checkRx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(10,114);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(102,13);
			this.checkRx.TabIndex = 8;
			this.checkRx.Text = "Rx";
			this.checkRx.Click += new System.EventHandler(this.checkRx_Click);
			// 
			// checkShowR
			// 
			this.checkShowR.Checked = true;
			this.checkShowR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowR.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowR.Location = new System.Drawing.Point(9,65);
			this.checkShowR.Name = "checkShowR";
			this.checkShowR.Size = new System.Drawing.Size(101,13);
			this.checkShowR.TabIndex = 14;
			this.checkShowR.Text = "Referred";
			this.checkShowR.Click += new System.EventHandler(this.checkShowR_Click);
			// 
			// checkShowE
			// 
			this.checkShowE.Checked = true;
			this.checkShowE.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowE.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowE.Location = new System.Drawing.Point(9,49);
			this.checkShowE.Name = "checkShowE";
			this.checkShowE.Size = new System.Drawing.Size(101,13);
			this.checkShowE.TabIndex = 10;
			this.checkShowE.Text = "Existing";
			this.checkShowE.Click += new System.EventHandler(this.checkShowE_Click);
			// 
			// checkShowC
			// 
			this.checkShowC.Checked = true;
			this.checkShowC.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowC.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowC.Location = new System.Drawing.Point(9,33);
			this.checkShowC.Name = "checkShowC";
			this.checkShowC.Size = new System.Drawing.Size(101,13);
			this.checkShowC.TabIndex = 9;
			this.checkShowC.Text = "Completed";
			this.checkShowC.Click += new System.EventHandler(this.checkShowC_Click);
			// 
			// checkShowTP
			// 
			this.checkShowTP.Checked = true;
			this.checkShowTP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTP.Location = new System.Drawing.Point(9,17);
			this.checkShowTP.Name = "checkShowTP";
			this.checkShowTP.Size = new System.Drawing.Size(101,13);
			this.checkShowTP.TabIndex = 8;
			this.checkShowTP.Text = "Treat Plan";
			this.checkShowTP.Click += new System.EventHandler(this.checkShowTP_Click);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Pat.gif");
			this.imageListMain.Images.SetKeyName(1,"Rx.gif");
			this.imageListMain.Images.SetKeyName(2,"Probe.gif");
			this.imageListMain.Images.SetKeyName(3,"Anesth.gif");
			this.imageListMain.Images.SetKeyName(4,"commlog.gif");
			// 
			// menuProgRight
			// 
			this.menuProgRight.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDelete,
            this.menuItemSetComplete,
            this.menuItemEditSelected,
						this.menuItemGroupSelected,
            this.menuItemPrintProg,
            this.menuItemPrintDay,
            this.menuItemLabFeeDetach,
            this.menuItemLabFee});
			// 
			// menuItemDelete
			// 
			this.menuItemDelete.Index = 0;
			this.menuItemDelete.Text = "Delete";
			this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
			// 
			// menuItemSetComplete
			// 
			this.menuItemSetComplete.Index = 1;
			this.menuItemSetComplete.Text = "Set Complete";
			this.menuItemSetComplete.Click += new System.EventHandler(this.menuItemSetComplete_Click);
			// 
			// menuItemEditSelected
			// 
			this.menuItemEditSelected.Index = 2;
			this.menuItemEditSelected.Text = "Edit All";
			this.menuItemEditSelected.Click += new System.EventHandler(this.menuItemEditSelected_Click);
			// 
			// menuItemGroupSelected
			// 
			this.menuItemGroupSelected.Index = 2;
			this.menuItemGroupSelected.Text = "Group Procedures";
			this.menuItemGroupSelected.Click += new System.EventHandler(this.menuItemGroupSelected_Click);
			// 
			// menuItemPrintProg
			// 
			this.menuItemPrintProg.Index = 3;
			this.menuItemPrintProg.Text = "Print Progress Notes ...";
			this.menuItemPrintProg.Click += new System.EventHandler(this.menuItemPrintProg_Click);
			// 
			// menuItemPrintDay
			// 
			this.menuItemPrintDay.Index = 4;
			this.menuItemPrintDay.Text = "Print Day for Hospital";
			this.menuItemPrintDay.Click += new System.EventHandler(this.menuItemPrintDay_Click);
			// 
			// menuItemLabFeeDetach
			// 
			this.menuItemLabFeeDetach.Index = 5;
			this.menuItemLabFeeDetach.Text = "Detach Lab Fee";
			this.menuItemLabFeeDetach.Click += new System.EventHandler(this.menuItemLabFeeDetach_Click);
			// 
			// menuItemLabFee
			// 
			this.menuItemLabFee.Index = 6;
			this.menuItemLabFee.Text = "Attach Lab Fee";
			this.menuItemLabFee.Click += new System.EventHandler(this.menuItemLabFee_Click);
			// 
			// tabControlImages
			// 
			this.tabControlImages.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControlImages.Controls.Add(this.tabPage1);
			this.tabControlImages.Controls.Add(this.tabPage2);
			this.tabControlImages.Controls.Add(this.tabPage4);
			this.tabControlImages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControlImages.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControlImages.ItemSize = new System.Drawing.Size(42,22);
			this.tabControlImages.Location = new System.Drawing.Point(0,681);
			this.tabControlImages.Name = "tabControlImages";
			this.tabControlImages.SelectedIndex = 0;
			this.tabControlImages.Size = new System.Drawing.Size(939,27);
			this.tabControlImages.TabIndex = 185;
			this.tabControlImages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControlImages_MouseDown);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4,4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(931,0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "BW\'s";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4,4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(931,0);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Pano";
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4,4);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(931,0);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// panelImages
			// 
			this.panelImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelImages.Controls.Add(this.listViewImages);
			this.panelImages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelImages.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelImages.Location = new System.Drawing.Point(0,592);
			this.panelImages.Name = "panelImages";
			this.panelImages.Padding = new System.Windows.Forms.Padding(0,4,0,0);
			this.panelImages.Size = new System.Drawing.Size(939,89);
			this.panelImages.TabIndex = 186;
			this.panelImages.Visible = false;
			this.panelImages.MouseLeave += new System.EventHandler(this.panelImages_MouseLeave);
			this.panelImages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseMove);
			this.panelImages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseDown);
			this.panelImages.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseUp);
			// 
			// listViewImages
			// 
			this.listViewImages.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.listViewImages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewImages.HideSelection = false;
			this.listViewImages.LabelWrap = false;
			this.listViewImages.LargeImageList = this.imageListThumbnails;
			this.listViewImages.Location = new System.Drawing.Point(0,4);
			this.listViewImages.MultiSelect = false;
			this.listViewImages.Name = "listViewImages";
			this.listViewImages.Size = new System.Drawing.Size(937,83);
			this.listViewImages.TabIndex = 0;
			this.listViewImages.UseCompatibleStateImageBehavior = false;
			this.listViewImages.DoubleClick += new System.EventHandler(this.listViewImages_DoubleClick);
			// 
			// imageListThumbnails
			// 
			this.imageListThumbnails.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListThumbnails.ImageSize = new System.Drawing.Size(100,100);
			this.imageListThumbnails.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// tabProc
			// 
			this.tabProc.Controls.Add(this.tabEnterTx);
			this.tabProc.Controls.Add(this.tabMissing);
			this.tabProc.Controls.Add(this.tabMovements);
			this.tabProc.Controls.Add(this.tabPrimary);
			this.tabProc.Controls.Add(this.tabPlanned);
			this.tabProc.Controls.Add(this.tabShow);
			this.tabProc.Controls.Add(this.tabDraw);
			this.tabProc.Location = new System.Drawing.Point(415,28);
			this.tabProc.Name = "tabProc";
			this.tabProc.SelectedIndex = 0;
			this.tabProc.Size = new System.Drawing.Size(524,259);
			this.tabProc.TabIndex = 190;
			this.tabProc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabProc_MouseDown);
			// 
			// tabEnterTx
			// 
			this.tabEnterTx.Controls.Add(this.panelQuickButtons);
			this.tabEnterTx.Controls.Add(this.listDx);
			this.tabEnterTx.Controls.Add(this.listViewButtons);
			this.tabEnterTx.Controls.Add(this.groupBox2);
			this.tabEnterTx.Controls.Add(this.listButtonCats);
			this.tabEnterTx.Controls.Add(this.butD);
			this.tabEnterTx.Controls.Add(this.comboPriority);
			this.tabEnterTx.Controls.Add(this.textSurf);
			this.tabEnterTx.Controls.Add(this.textDate);
			this.tabEnterTx.Controls.Add(this.butBF);
			this.tabEnterTx.Controls.Add(this.checkToday);
			this.tabEnterTx.Controls.Add(this.butL);
			this.tabEnterTx.Controls.Add(this.label6);
			this.tabEnterTx.Controls.Add(this.butM);
			this.tabEnterTx.Controls.Add(this.butOK);
			this.tabEnterTx.Controls.Add(this.butAddProc);
			this.tabEnterTx.Controls.Add(this.butV);
			this.tabEnterTx.Controls.Add(this.textProcCode);
			this.tabEnterTx.Controls.Add(this.butOI);
			this.tabEnterTx.Controls.Add(this.label14);
			this.tabEnterTx.Controls.Add(this.labelDx);
			this.tabEnterTx.Controls.Add(this.label13);
			this.tabEnterTx.Location = new System.Drawing.Point(4,22);
			this.tabEnterTx.Name = "tabEnterTx";
			this.tabEnterTx.Padding = new System.Windows.Forms.Padding(3);
			this.tabEnterTx.Size = new System.Drawing.Size(516,233);
			this.tabEnterTx.TabIndex = 0;
			this.tabEnterTx.Text = "Enter Treatment";
			this.tabEnterTx.UseVisualStyleBackColor = true;
			// 
			// panelQuickButtons
			// 
			this.panelQuickButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelQuickButtons.Controls.Add(this.panelQuickPasteAmalgam);
			this.panelQuickButtons.Controls.Add(this.buttonCMODB);
			this.panelQuickButtons.Controls.Add(this.buttonCMODL);
			this.panelQuickButtons.Controls.Add(this.label1);
			this.panelQuickButtons.Controls.Add(this.butCMDL);
			this.panelQuickButtons.Controls.Add(this.butML);
			this.panelQuickButtons.Controls.Add(this.butDL);
			this.panelQuickButtons.Controls.Add(this.butCOB);
			this.panelQuickButtons.Controls.Add(this.butCOL);
			this.panelQuickButtons.Controls.Add(this.buttonCSeal);
			this.panelQuickButtons.Controls.Add(this.buttonCMO);
			this.panelQuickButtons.Controls.Add(this.buttonCMOD);
			this.panelQuickButtons.Controls.Add(this.buttonCO);
			this.panelQuickButtons.Controls.Add(this.label23);
			this.panelQuickButtons.Controls.Add(this.buttonCDO);
			this.panelQuickButtons.Location = new System.Drawing.Point(311,41);
			this.panelQuickButtons.Name = "panelQuickButtons";
			this.panelQuickButtons.Size = new System.Drawing.Size(175,191);
			this.panelQuickButtons.TabIndex = 198;
			this.panelQuickButtons.Visible = false;
			this.panelQuickButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelQuickButtons_Paint);
			// 
			// panelQuickPasteAmalgam
			// 
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAMODB);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAMODL);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAOB);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAOL);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAO);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAMO);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonAMOD);
			this.panelQuickPasteAmalgam.Controls.Add(this.buttonADO);
			this.panelQuickPasteAmalgam.Controls.Add(this.label24);
			this.panelQuickPasteAmalgam.Location = new System.Drawing.Point(0,106);
			this.panelQuickPasteAmalgam.Name = "panelQuickPasteAmalgam";
			this.panelQuickPasteAmalgam.Size = new System.Drawing.Size(173,80);
			this.panelQuickPasteAmalgam.TabIndex = 221;
			// 
			// buttonAMODB
			// 
			this.buttonAMODB.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAMODB.Autosize = true;
			this.buttonAMODB.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAMODB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAMODB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAMODB.CornerRadius = 4F;
			this.buttonAMODB.Location = new System.Drawing.Point(94,37);
			this.buttonAMODB.Name = "buttonAMODB";
			this.buttonAMODB.Size = new System.Drawing.Size(43,18);
			this.buttonAMODB.TabIndex = 227;
			this.buttonAMODB.Text = "MODB";
			this.buttonAMODB.UseVisualStyleBackColor = false;
			this.buttonAMODB.Click += new System.EventHandler(this.buttonAMODB_Click);
			// 
			// buttonAMODL
			// 
			this.buttonAMODL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAMODL.Autosize = true;
			this.buttonAMODL.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAMODL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAMODL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAMODL.CornerRadius = 4F;
			this.buttonAMODL.Location = new System.Drawing.Point(52,37);
			this.buttonAMODL.Name = "buttonAMODL";
			this.buttonAMODL.Size = new System.Drawing.Size(42,18);
			this.buttonAMODL.TabIndex = 226;
			this.buttonAMODL.Text = "MODL";
			this.buttonAMODL.UseVisualStyleBackColor = false;
			this.buttonAMODL.Click += new System.EventHandler(this.buttonAMODL_Click);
			// 
			// buttonAOB
			// 
			this.buttonAOB.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAOB.Autosize = true;
			this.buttonAOB.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAOB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAOB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAOB.CornerRadius = 4F;
			this.buttonAOB.Location = new System.Drawing.Point(26,37);
			this.buttonAOB.Name = "buttonAOB";
			this.buttonAOB.Size = new System.Drawing.Size(26,18);
			this.buttonAOB.TabIndex = 225;
			this.buttonAOB.Text = "OB";
			this.buttonAOB.UseVisualStyleBackColor = false;
			this.buttonAOB.Click += new System.EventHandler(this.buttonAOB_Click);
			// 
			// buttonAOL
			// 
			this.buttonAOL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAOL.Autosize = true;
			this.buttonAOL.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAOL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAOL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAOL.CornerRadius = 4F;
			this.buttonAOL.Location = new System.Drawing.Point(0,37);
			this.buttonAOL.Name = "buttonAOL";
			this.buttonAOL.Size = new System.Drawing.Size(26,18);
			this.buttonAOL.TabIndex = 224;
			this.buttonAOL.Text = "OL";
			this.buttonAOL.UseVisualStyleBackColor = false;
			this.buttonAOL.Click += new System.EventHandler(this.buttonAOL_Click);
			// 
			// buttonAO
			// 
			this.buttonAO.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAO.Autosize = true;
			this.buttonAO.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAO.CornerRadius = 4F;
			this.buttonAO.Location = new System.Drawing.Point(62,19);
			this.buttonAO.Name = "buttonAO";
			this.buttonAO.Size = new System.Drawing.Size(18,18);
			this.buttonAO.TabIndex = 223;
			this.buttonAO.Text = "O";
			this.buttonAO.UseVisualStyleBackColor = false;
			this.buttonAO.Click += new System.EventHandler(this.buttonAO_Click);
			// 
			// buttonAMO
			// 
			this.buttonAMO.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAMO.Autosize = true;
			this.buttonAMO.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAMO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAMO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAMO.CornerRadius = 4F;
			this.buttonAMO.Location = new System.Drawing.Point(0,19);
			this.buttonAMO.Name = "buttonAMO";
			this.buttonAMO.Size = new System.Drawing.Size(27,18);
			this.buttonAMO.TabIndex = 222;
			this.buttonAMO.Text = "MO";
			this.buttonAMO.UseVisualStyleBackColor = false;
			this.buttonAMO.Click += new System.EventHandler(this.buttonAMO_Click);
			// 
			// buttonAMOD
			// 
			this.buttonAMOD.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAMOD.Autosize = true;
			this.buttonAMOD.BackColor = System.Drawing.SystemColors.Control;
			this.buttonAMOD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAMOD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAMOD.CornerRadius = 4F;
			this.buttonAMOD.Location = new System.Drawing.Point(27,19);
			this.buttonAMOD.Name = "buttonAMOD";
			this.buttonAMOD.Size = new System.Drawing.Size(36,18);
			this.buttonAMOD.TabIndex = 221;
			this.buttonAMOD.Text = "MOD";
			this.buttonAMOD.UseVisualStyleBackColor = false;
			this.buttonAMOD.Click += new System.EventHandler(this.buttonAMOD_Click);
			// 
			// buttonADO
			// 
			this.buttonADO.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonADO.Autosize = true;
			this.buttonADO.BackColor = System.Drawing.SystemColors.Control;
			this.buttonADO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonADO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonADO.CornerRadius = 4F;
			this.buttonADO.Location = new System.Drawing.Point(80,19);
			this.buttonADO.Name = "buttonADO";
			this.buttonADO.Size = new System.Drawing.Size(26,18);
			this.buttonADO.TabIndex = 220;
			this.buttonADO.Text = "DO";
			this.buttonADO.UseVisualStyleBackColor = false;
			this.buttonADO.Click += new System.EventHandler(this.buttonADO_Click);
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(4,2);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(56,13);
			this.label24.TabIndex = 219;
			this.label24.Text = "Amalgam";
			// 
			// buttonCMODB
			// 
			this.buttonCMODB.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCMODB.Autosize = true;
			this.buttonCMODB.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCMODB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCMODB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCMODB.CornerRadius = 4F;
			this.buttonCMODB.Location = new System.Drawing.Point(93,35);
			this.buttonCMODB.Name = "buttonCMODB";
			this.buttonCMODB.Size = new System.Drawing.Size(43,18);
			this.buttonCMODB.TabIndex = 220;
			this.buttonCMODB.Text = "MODB";
			this.buttonCMODB.UseVisualStyleBackColor = false;
			this.buttonCMODB.Click += new System.EventHandler(this.buttonCMODB_Click);
			// 
			// buttonCMODL
			// 
			this.buttonCMODL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCMODL.Autosize = true;
			this.buttonCMODL.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCMODL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCMODL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCMODL.CornerRadius = 4F;
			this.buttonCMODL.Location = new System.Drawing.Point(51,35);
			this.buttonCMODL.Name = "buttonCMODL";
			this.buttonCMODL.Size = new System.Drawing.Size(42,18);
			this.buttonCMODL.TabIndex = 219;
			this.buttonCMODL.Text = "MODL";
			this.buttonCMODL.UseVisualStyleBackColor = false;
			this.buttonCMODL.Click += new System.EventHandler(this.buttonCMODL_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3,65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84,13);
			this.label1.TabIndex = 214;
			this.label1.Text = "Ant. Composite";
			// 
			// butCMDL
			// 
			this.butCMDL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCMDL.Autosize = true;
			this.butCMDL.BackColor = System.Drawing.SystemColors.Control;
			this.butCMDL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCMDL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCMDL.CornerRadius = 4F;
			this.butCMDL.Location = new System.Drawing.Point(26,82);
			this.butCMDL.Name = "butCMDL";
			this.butCMDL.Size = new System.Drawing.Size(33,18);
			this.butCMDL.TabIndex = 213;
			this.butCMDL.Text = "MDL";
			this.butCMDL.UseVisualStyleBackColor = false;
			this.butCMDL.Click += new System.EventHandler(this.butCMDL_Click);
			// 
			// butML
			// 
			this.butML.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butML.Autosize = true;
			this.butML.BackColor = System.Drawing.SystemColors.Control;
			this.butML.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butML.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butML.CornerRadius = 4F;
			this.butML.Location = new System.Drawing.Point(59,82);
			this.butML.Name = "butML";
			this.butML.Size = new System.Drawing.Size(26,18);
			this.butML.TabIndex = 212;
			this.butML.Text = "ML";
			this.butML.UseVisualStyleBackColor = false;
			this.butML.Click += new System.EventHandler(this.butML_Click);
			// 
			// butDL
			// 
			this.butDL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDL.Autosize = true;
			this.butDL.BackColor = System.Drawing.SystemColors.Control;
			this.butDL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDL.CornerRadius = 4F;
			this.butDL.Location = new System.Drawing.Point(0,82);
			this.butDL.Name = "butDL";
			this.butDL.Size = new System.Drawing.Size(26,18);
			this.butDL.TabIndex = 211;
			this.butDL.Text = "DL";
			this.butDL.UseVisualStyleBackColor = false;
			this.butDL.Click += new System.EventHandler(this.butDL_Click);
			// 
			// butCOB
			// 
			this.butCOB.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCOB.Autosize = true;
			this.butCOB.BackColor = System.Drawing.SystemColors.Control;
			this.butCOB.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCOB.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCOB.CornerRadius = 4F;
			this.butCOB.Location = new System.Drawing.Point(26,35);
			this.butCOB.Name = "butCOB";
			this.butCOB.Size = new System.Drawing.Size(26,18);
			this.butCOB.TabIndex = 210;
			this.butCOB.Text = "OB";
			this.butCOB.UseVisualStyleBackColor = false;
			this.butCOB.Click += new System.EventHandler(this.butCOB_Click);
			// 
			// butCOL
			// 
			this.butCOL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCOL.Autosize = true;
			this.butCOL.BackColor = System.Drawing.SystemColors.Control;
			this.butCOL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCOL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCOL.CornerRadius = 4F;
			this.butCOL.Location = new System.Drawing.Point(0,35);
			this.butCOL.Name = "butCOL";
			this.butCOL.Size = new System.Drawing.Size(26,18);
			this.butCOL.TabIndex = 209;
			this.butCOL.Text = "OL";
			this.butCOL.UseVisualStyleBackColor = false;
			this.butCOL.Click += new System.EventHandler(this.butCOL_Click);
			// 
			// buttonCSeal
			// 
			this.buttonCSeal.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCSeal.Autosize = true;
			this.buttonCSeal.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCSeal.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCSeal.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCSeal.CornerRadius = 4F;
			this.buttonCSeal.Location = new System.Drawing.Point(136,17);
			this.buttonCSeal.Name = "buttonCSeal";
			this.buttonCSeal.Size = new System.Drawing.Size(32,18);
			this.buttonCSeal.TabIndex = 203;
			this.buttonCSeal.Text = "Seal";
			this.buttonCSeal.UseVisualStyleBackColor = false;
			this.buttonCSeal.Click += new System.EventHandler(this.buttonCSeal_Click);
			// 
			// buttonCMO
			// 
			this.buttonCMO.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCMO.Autosize = true;
			this.buttonCMO.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCMO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCMO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCMO.CornerRadius = 4F;
			this.buttonCMO.Location = new System.Drawing.Point(0,17);
			this.buttonCMO.Name = "buttonCMO";
			this.buttonCMO.Size = new System.Drawing.Size(27,18);
			this.buttonCMO.TabIndex = 202;
			this.buttonCMO.Text = "MO";
			this.buttonCMO.UseVisualStyleBackColor = false;
			this.buttonCMO.Click += new System.EventHandler(this.buttonCMO_Click);
			// 
			// buttonCMOD
			// 
			this.buttonCMOD.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCMOD.Autosize = true;
			this.buttonCMOD.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCMOD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCMOD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCMOD.CornerRadius = 4F;
			this.buttonCMOD.Location = new System.Drawing.Point(26,17);
			this.buttonCMOD.Name = "buttonCMOD";
			this.buttonCMOD.Size = new System.Drawing.Size(36,18);
			this.buttonCMOD.TabIndex = 201;
			this.buttonCMOD.Text = "MOD";
			this.buttonCMOD.UseVisualStyleBackColor = false;
			this.buttonCMOD.Click += new System.EventHandler(this.buttonCMOD_Click);
			// 
			// buttonCO
			// 
			this.buttonCO.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCO.Autosize = true;
			this.buttonCO.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCO.CornerRadius = 4F;
			this.buttonCO.Location = new System.Drawing.Point(62,17);
			this.buttonCO.Name = "buttonCO";
			this.buttonCO.Size = new System.Drawing.Size(18,18);
			this.buttonCO.TabIndex = 200;
			this.buttonCO.Text = "O";
			this.buttonCO.UseVisualStyleBackColor = false;
			this.buttonCO.Click += new System.EventHandler(this.buttonCO_Click);
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(4,1);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(88,13);
			this.label23.TabIndex = 198;
			this.label23.Text = "Post. Composite";
			// 
			// buttonCDO
			// 
			this.buttonCDO.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCDO.Autosize = true;
			this.buttonCDO.BackColor = System.Drawing.SystemColors.Control;
			this.buttonCDO.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCDO.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCDO.CornerRadius = 4F;
			this.buttonCDO.Location = new System.Drawing.Point(80,17);
			this.buttonCDO.Name = "buttonCDO";
			this.buttonCDO.Size = new System.Drawing.Size(26,18);
			this.buttonCDO.TabIndex = 197;
			this.buttonCDO.Text = "DO";
			this.buttonCDO.UseVisualStyleBackColor = false;
			this.buttonCDO.Click += new System.EventHandler(this.buttonCDO_Click);
			// 
			// butD
			// 
			this.butD.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butD.Autosize = true;
			this.butD.BackColor = System.Drawing.SystemColors.Control;
			this.butD.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butD.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butD.CornerRadius = 4F;
			this.butD.Location = new System.Drawing.Point(61,43);
			this.butD.Name = "butD";
			this.butD.Size = new System.Drawing.Size(24,20);
			this.butD.TabIndex = 20;
			this.butD.Text = "D";
			this.butD.UseVisualStyleBackColor = false;
			this.butD.Click += new System.EventHandler(this.butD_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(0,211);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(89,20);
			this.textDate.TabIndex = 55;
			// 
			// butBF
			// 
			this.butBF.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBF.Autosize = true;
			this.butBF.BackColor = System.Drawing.SystemColors.Control;
			this.butBF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBF.CornerRadius = 4F;
			this.butBF.Location = new System.Drawing.Point(22,23);
			this.butBF.Name = "butBF";
			this.butBF.Size = new System.Drawing.Size(28,20);
			this.butBF.TabIndex = 21;
			this.butBF.Text = "B/F";
			this.butBF.UseVisualStyleBackColor = false;
			this.butBF.Click += new System.EventHandler(this.butBF_Click);
			// 
			// butL
			// 
			this.butL.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butL.Autosize = true;
			this.butL.BackColor = System.Drawing.SystemColors.Control;
			this.butL.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butL.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butL.CornerRadius = 4F;
			this.butL.Location = new System.Drawing.Point(32,63);
			this.butL.Name = "butL";
			this.butL.Size = new System.Drawing.Size(24,20);
			this.butL.TabIndex = 22;
			this.butL.Text = "L";
			this.butL.UseVisualStyleBackColor = false;
			this.butL.Click += new System.EventHandler(this.butL_Click);
			// 
			// butM
			// 
			this.butM.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butM.Autosize = true;
			this.butM.BackColor = System.Drawing.SystemColors.Control;
			this.butM.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butM.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butM.CornerRadius = 4F;
			this.butM.Location = new System.Drawing.Point(3,43);
			this.butM.Name = "butM";
			this.butM.Size = new System.Drawing.Size(24,20);
			this.butM.TabIndex = 18;
			this.butM.Text = "M";
			this.butM.UseVisualStyleBackColor = false;
			this.butM.Click += new System.EventHandler(this.butM_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(442,1);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(44,23);
			this.butOK.TabIndex = 52;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butAddProc
			// 
			this.butAddProc.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddProc.Autosize = true;
			this.butAddProc.BackColor = System.Drawing.SystemColors.Control;
			this.butAddProc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddProc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddProc.CornerRadius = 4F;
			this.butAddProc.Location = new System.Drawing.Point(191,1);
			this.butAddProc.Name = "butAddProc";
			this.butAddProc.Size = new System.Drawing.Size(89,23);
			this.butAddProc.TabIndex = 17;
			this.butAddProc.Text = "Procedure List";
			this.butAddProc.UseVisualStyleBackColor = false;
			this.butAddProc.Click += new System.EventHandler(this.butAddProc_Click);
			// 
			// butV
			// 
			this.butV.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butV.Autosize = true;
			this.butV.BackColor = System.Drawing.SystemColors.Control;
			this.butV.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butV.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butV.CornerRadius = 4F;
			this.butV.Location = new System.Drawing.Point(50,23);
			this.butV.Name = "butV";
			this.butV.Size = new System.Drawing.Size(17,20);
			this.butV.TabIndex = 24;
			this.butV.Text = "V";
			this.butV.UseVisualStyleBackColor = false;
			this.butV.Click += new System.EventHandler(this.butV_Click);
			// 
			// butOI
			// 
			this.butOI.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOI.Autosize = true;
			this.butOI.BackColor = System.Drawing.SystemColors.Control;
			this.butOI.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOI.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOI.CornerRadius = 4F;
			this.butOI.Location = new System.Drawing.Point(27,43);
			this.butOI.Name = "butOI";
			this.butOI.Size = new System.Drawing.Size(34,20);
			this.butOI.TabIndex = 19;
			this.butOI.Text = "O/I";
			this.butOI.UseVisualStyleBackColor = false;
			this.butOI.Click += new System.EventHandler(this.butOI_Click);
			// 
			// tabMissing
			// 
			this.tabMissing.Controls.Add(this.butUnhide);
			this.tabMissing.Controls.Add(this.label5);
			this.tabMissing.Controls.Add(this.listHidden);
			this.tabMissing.Controls.Add(this.butEdentulous);
			this.tabMissing.Controls.Add(this.groupBox1);
			this.tabMissing.Location = new System.Drawing.Point(4,22);
			this.tabMissing.Name = "tabMissing";
			this.tabMissing.Padding = new System.Windows.Forms.Padding(3);
			this.tabMissing.Size = new System.Drawing.Size(516,233);
			this.tabMissing.TabIndex = 1;
			this.tabMissing.Text = "Missing Teeth";
			this.tabMissing.UseVisualStyleBackColor = true;
			// 
			// butUnhide
			// 
			this.butUnhide.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUnhide.Autosize = true;
			this.butUnhide.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUnhide.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUnhide.CornerRadius = 4F;
			this.butUnhide.Location = new System.Drawing.Point(307,113);
			this.butUnhide.Name = "butUnhide";
			this.butUnhide.Size = new System.Drawing.Size(71,23);
			this.butUnhide.TabIndex = 20;
			this.butUnhide.Text = "Unhide";
			this.butUnhide.Click += new System.EventHandler(this.butUnhide_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(304,12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(147,17);
			this.label5.TabIndex = 19;
			this.label5.Text = "Hidden Teeth";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listHidden
			// 
			this.listHidden.FormattingEnabled = true;
			this.listHidden.Location = new System.Drawing.Point(307,33);
			this.listHidden.Name = "listHidden";
			this.listHidden.Size = new System.Drawing.Size(94,69);
			this.listHidden.TabIndex = 18;
			// 
			// butEdentulous
			// 
			this.butEdentulous.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdentulous.Autosize = true;
			this.butEdentulous.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdentulous.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdentulous.CornerRadius = 4F;
			this.butEdentulous.Location = new System.Drawing.Point(31,113);
			this.butEdentulous.Name = "butEdentulous";
			this.butEdentulous.Size = new System.Drawing.Size(82,23);
			this.butEdentulous.TabIndex = 16;
			this.butEdentulous.Text = "Edentulous";
			this.butEdentulous.Click += new System.EventHandler(this.butEdentulous_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.butNotMissing);
			this.groupBox1.Controls.Add(this.butMissing);
			this.groupBox1.Controls.Add(this.butHidden);
			this.groupBox1.Location = new System.Drawing.Point(20,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(267,90);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Set Selected Teeth";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(115,46);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(146,17);
			this.label7.TabIndex = 20;
			this.label7.Text = "(including numbers)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butNotMissing
			// 
			this.butNotMissing.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNotMissing.Autosize = true;
			this.butNotMissing.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNotMissing.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNotMissing.CornerRadius = 4F;
			this.butNotMissing.Location = new System.Drawing.Point(11,53);
			this.butNotMissing.Name = "butNotMissing";
			this.butNotMissing.Size = new System.Drawing.Size(82,23);
			this.butNotMissing.TabIndex = 15;
			this.butNotMissing.Text = "Not Missing";
			this.butNotMissing.Click += new System.EventHandler(this.butNotMissing_Click);
			// 
			// butMissing
			// 
			this.butMissing.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMissing.Autosize = true;
			this.butMissing.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMissing.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMissing.CornerRadius = 4F;
			this.butMissing.Location = new System.Drawing.Point(11,21);
			this.butMissing.Name = "butMissing";
			this.butMissing.Size = new System.Drawing.Size(82,23);
			this.butMissing.TabIndex = 14;
			this.butMissing.Text = "Missing";
			this.butMissing.Click += new System.EventHandler(this.butMissing_Click);
			// 
			// butHidden
			// 
			this.butHidden.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHidden.Autosize = true;
			this.butHidden.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHidden.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHidden.CornerRadius = 4F;
			this.butHidden.Location = new System.Drawing.Point(172,21);
			this.butHidden.Name = "butHidden";
			this.butHidden.Size = new System.Drawing.Size(82,23);
			this.butHidden.TabIndex = 17;
			this.butHidden.Text = "Hidden";
			this.butHidden.Click += new System.EventHandler(this.butHidden_Click);
			// 
			// tabMovements
			// 
			this.tabMovements.Controls.Add(this.label16);
			this.tabMovements.Controls.Add(this.butApplyMovements);
			this.tabMovements.Controls.Add(this.groupBox4);
			this.tabMovements.Controls.Add(this.groupBox3);
			this.tabMovements.Location = new System.Drawing.Point(4,22);
			this.tabMovements.Name = "tabMovements";
			this.tabMovements.Size = new System.Drawing.Size(516,233);
			this.tabMovements.TabIndex = 3;
			this.tabMovements.Text = "Movements";
			this.tabMovements.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(180,183);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(267,18);
			this.label16.TabIndex = 29;
			this.label16.Text = "(if you typed in changes)";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butApplyMovements
			// 
			this.butApplyMovements.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butApplyMovements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butApplyMovements.Autosize = true;
			this.butApplyMovements.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butApplyMovements.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butApplyMovements.CornerRadius = 4F;
			this.butApplyMovements.Location = new System.Drawing.Point(404,154);
			this.butApplyMovements.Name = "butApplyMovements";
			this.butApplyMovements.Size = new System.Drawing.Size(68,23);
			this.butApplyMovements.TabIndex = 16;
			this.butApplyMovements.Text = "Apply";
			this.butApplyMovements.Click += new System.EventHandler(this.butApplyMovements_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.butTipBplus);
			this.groupBox4.Controls.Add(this.butTipBminus);
			this.groupBox4.Controls.Add(this.butTipMplus);
			this.groupBox4.Controls.Add(this.butTipMminus);
			this.groupBox4.Controls.Add(this.butRotatePlus);
			this.groupBox4.Controls.Add(this.butRotateMinus);
			this.groupBox4.Controls.Add(this.textTipB);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.textTipM);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.textRotate);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Location = new System.Drawing.Point(255,12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(207,109);
			this.groupBox4.TabIndex = 15;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Rotate/Tip degrees";
			// 
			// butTipBplus
			// 
			this.butTipBplus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTipBplus.Autosize = true;
			this.butTipBplus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTipBplus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTipBplus.CornerRadius = 4F;
			this.butTipBplus.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butTipBplus.Location = new System.Drawing.Point(159,76);
			this.butTipBplus.Name = "butTipBplus";
			this.butTipBplus.Size = new System.Drawing.Size(31,23);
			this.butTipBplus.TabIndex = 34;
			this.butTipBplus.Text = "+";
			this.butTipBplus.Click += new System.EventHandler(this.butTipBplus_Click);
			// 
			// butTipBminus
			// 
			this.butTipBminus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTipBminus.Autosize = true;
			this.butTipBminus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTipBminus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTipBminus.CornerRadius = 4F;
			this.butTipBminus.Font = new System.Drawing.Font("Microsoft Sans Serif",15F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butTipBminus.Location = new System.Drawing.Point(122,76);
			this.butTipBminus.Name = "butTipBminus";
			this.butTipBminus.Size = new System.Drawing.Size(31,23);
			this.butTipBminus.TabIndex = 35;
			this.butTipBminus.Text = "-";
			this.butTipBminus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butTipBminus.Click += new System.EventHandler(this.butTipBminus_Click);
			// 
			// butTipMplus
			// 
			this.butTipMplus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTipMplus.Autosize = true;
			this.butTipMplus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTipMplus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTipMplus.CornerRadius = 4F;
			this.butTipMplus.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butTipMplus.Location = new System.Drawing.Point(159,47);
			this.butTipMplus.Name = "butTipMplus";
			this.butTipMplus.Size = new System.Drawing.Size(31,23);
			this.butTipMplus.TabIndex = 32;
			this.butTipMplus.Text = "+";
			this.butTipMplus.Click += new System.EventHandler(this.butTipMplus_Click);
			// 
			// butTipMminus
			// 
			this.butTipMminus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTipMminus.Autosize = true;
			this.butTipMminus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTipMminus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTipMminus.CornerRadius = 4F;
			this.butTipMminus.Font = new System.Drawing.Font("Microsoft Sans Serif",15F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butTipMminus.Location = new System.Drawing.Point(122,47);
			this.butTipMminus.Name = "butTipMminus";
			this.butTipMminus.Size = new System.Drawing.Size(31,23);
			this.butTipMminus.TabIndex = 33;
			this.butTipMminus.Text = "-";
			this.butTipMminus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butTipMminus.Click += new System.EventHandler(this.butTipMminus_Click);
			// 
			// butRotatePlus
			// 
			this.butRotatePlus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRotatePlus.Autosize = true;
			this.butRotatePlus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRotatePlus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRotatePlus.CornerRadius = 4F;
			this.butRotatePlus.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butRotatePlus.Location = new System.Drawing.Point(159,18);
			this.butRotatePlus.Name = "butRotatePlus";
			this.butRotatePlus.Size = new System.Drawing.Size(31,23);
			this.butRotatePlus.TabIndex = 30;
			this.butRotatePlus.Text = "+";
			this.butRotatePlus.Click += new System.EventHandler(this.butRotatePlus_Click);
			// 
			// butRotateMinus
			// 
			this.butRotateMinus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRotateMinus.Autosize = true;
			this.butRotateMinus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRotateMinus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRotateMinus.CornerRadius = 4F;
			this.butRotateMinus.Font = new System.Drawing.Font("Microsoft Sans Serif",15F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butRotateMinus.Location = new System.Drawing.Point(122,18);
			this.butRotateMinus.Name = "butRotateMinus";
			this.butRotateMinus.Size = new System.Drawing.Size(31,23);
			this.butRotateMinus.TabIndex = 31;
			this.butRotateMinus.Text = "-";
			this.butRotateMinus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butRotateMinus.Click += new System.EventHandler(this.butRotateMinus_Click);
			// 
			// textTipB
			// 
			this.textTipB.Location = new System.Drawing.Point(72,77);
			this.textTipB.Name = "textTipB";
			this.textTipB.Size = new System.Drawing.Size(38,20);
			this.textTipB.TabIndex = 29;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(3,77);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(68,18);
			this.label11.TabIndex = 28;
			this.label11.Text = "Labial Tip";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTipM
			// 
			this.textTipM.Location = new System.Drawing.Point(72,49);
			this.textTipM.Name = "textTipM";
			this.textTipM.Size = new System.Drawing.Size(38,20);
			this.textTipM.TabIndex = 25;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(3,49);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(68,18);
			this.label12.TabIndex = 24;
			this.label12.Text = "Mesial Tip";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRotate
			// 
			this.textRotate.Location = new System.Drawing.Point(72,20);
			this.textRotate.Name = "textRotate";
			this.textRotate.Size = new System.Drawing.Size(38,20);
			this.textRotate.TabIndex = 21;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(3,20);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(68,18);
			this.label15.TabIndex = 20;
			this.label15.Text = "Rotate";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butShiftBplus);
			this.groupBox3.Controls.Add(this.butShiftBminus);
			this.groupBox3.Controls.Add(this.butShiftOplus);
			this.groupBox3.Controls.Add(this.butShiftOminus);
			this.groupBox3.Controls.Add(this.butShiftMplus);
			this.groupBox3.Controls.Add(this.butShiftMminus);
			this.groupBox3.Controls.Add(this.textShiftB);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.textShiftO);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.textShiftM);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Location = new System.Drawing.Point(20,12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(207,109);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Shift millimeters";
			// 
			// butShiftBplus
			// 
			this.butShiftBplus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShiftBplus.Autosize = true;
			this.butShiftBplus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShiftBplus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShiftBplus.CornerRadius = 4F;
			this.butShiftBplus.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butShiftBplus.Location = new System.Drawing.Point(158,76);
			this.butShiftBplus.Name = "butShiftBplus";
			this.butShiftBplus.Size = new System.Drawing.Size(31,23);
			this.butShiftBplus.TabIndex = 40;
			this.butShiftBplus.Text = "+";
			this.butShiftBplus.Click += new System.EventHandler(this.butShiftBplus_Click);
			// 
			// butShiftBminus
			// 
			this.butShiftBminus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShiftBminus.Autosize = true;
			this.butShiftBminus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShiftBminus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShiftBminus.CornerRadius = 4F;
			this.butShiftBminus.Font = new System.Drawing.Font("Microsoft Sans Serif",15F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butShiftBminus.Location = new System.Drawing.Point(121,76);
			this.butShiftBminus.Name = "butShiftBminus";
			this.butShiftBminus.Size = new System.Drawing.Size(31,23);
			this.butShiftBminus.TabIndex = 41;
			this.butShiftBminus.Text = "-";
			this.butShiftBminus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butShiftBminus.Click += new System.EventHandler(this.butShiftBminus_Click);
			// 
			// butShiftOplus
			// 
			this.butShiftOplus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShiftOplus.Autosize = true;
			this.butShiftOplus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShiftOplus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShiftOplus.CornerRadius = 4F;
			this.butShiftOplus.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butShiftOplus.Location = new System.Drawing.Point(158,47);
			this.butShiftOplus.Name = "butShiftOplus";
			this.butShiftOplus.Size = new System.Drawing.Size(31,23);
			this.butShiftOplus.TabIndex = 38;
			this.butShiftOplus.Text = "+";
			this.butShiftOplus.Click += new System.EventHandler(this.butShiftOplus_Click);
			// 
			// butShiftOminus
			// 
			this.butShiftOminus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShiftOminus.Autosize = true;
			this.butShiftOminus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShiftOminus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShiftOminus.CornerRadius = 4F;
			this.butShiftOminus.Font = new System.Drawing.Font("Microsoft Sans Serif",15F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butShiftOminus.Location = new System.Drawing.Point(121,47);
			this.butShiftOminus.Name = "butShiftOminus";
			this.butShiftOminus.Size = new System.Drawing.Size(31,23);
			this.butShiftOminus.TabIndex = 39;
			this.butShiftOminus.Text = "-";
			this.butShiftOminus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butShiftOminus.Click += new System.EventHandler(this.butShiftOminus_Click);
			// 
			// butShiftMplus
			// 
			this.butShiftMplus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShiftMplus.Autosize = true;
			this.butShiftMplus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShiftMplus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShiftMplus.CornerRadius = 4F;
			this.butShiftMplus.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butShiftMplus.Location = new System.Drawing.Point(158,18);
			this.butShiftMplus.Name = "butShiftMplus";
			this.butShiftMplus.Size = new System.Drawing.Size(31,23);
			this.butShiftMplus.TabIndex = 36;
			this.butShiftMplus.Text = "+";
			this.butShiftMplus.Click += new System.EventHandler(this.butShiftMplus_Click);
			// 
			// butShiftMminus
			// 
			this.butShiftMminus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShiftMminus.Autosize = true;
			this.butShiftMminus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShiftMminus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShiftMminus.CornerRadius = 4F;
			this.butShiftMminus.Font = new System.Drawing.Font("Microsoft Sans Serif",15F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.butShiftMminus.Location = new System.Drawing.Point(121,18);
			this.butShiftMminus.Name = "butShiftMminus";
			this.butShiftMminus.Size = new System.Drawing.Size(31,23);
			this.butShiftMminus.TabIndex = 37;
			this.butShiftMminus.Text = "-";
			this.butShiftMminus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butShiftMminus.Click += new System.EventHandler(this.butShiftMminus_Click);
			// 
			// textShiftB
			// 
			this.textShiftB.Location = new System.Drawing.Point(72,77);
			this.textShiftB.Name = "textShiftB";
			this.textShiftB.Size = new System.Drawing.Size(38,20);
			this.textShiftB.TabIndex = 29;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3,77);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(68,18);
			this.label10.TabIndex = 28;
			this.label10.Text = "Labial";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textShiftO
			// 
			this.textShiftO.Location = new System.Drawing.Point(72,49);
			this.textShiftO.Name = "textShiftO";
			this.textShiftO.Size = new System.Drawing.Size(38,20);
			this.textShiftO.TabIndex = 25;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(3,49);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(68,18);
			this.label9.TabIndex = 24;
			this.label9.Text = "Occlusal";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textShiftM
			// 
			this.textShiftM.Location = new System.Drawing.Point(72,20);
			this.textShiftM.Name = "textShiftM";
			this.textShiftM.Size = new System.Drawing.Size(38,20);
			this.textShiftM.TabIndex = 21;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(3,20);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68,18);
			this.label8.TabIndex = 20;
			this.label8.Text = "Mesial";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPrimary
			// 
			this.tabPrimary.Controls.Add(this.groupBox5);
			this.tabPrimary.Controls.Add(this.butMixed);
			this.tabPrimary.Controls.Add(this.butAllPrimary);
			this.tabPrimary.Controls.Add(this.butAllPerm);
			this.tabPrimary.Location = new System.Drawing.Point(4,22);
			this.tabPrimary.Name = "tabPrimary";
			this.tabPrimary.Size = new System.Drawing.Size(516,233);
			this.tabPrimary.TabIndex = 2;
			this.tabPrimary.Text = "Primary";
			this.tabPrimary.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.butPerm);
			this.groupBox5.Controls.Add(this.butPrimary);
			this.groupBox5.Location = new System.Drawing.Point(20,12);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(153,90);
			this.groupBox5.TabIndex = 21;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Set Selected Teeth";
			// 
			// butPerm
			// 
			this.butPerm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPerm.Autosize = true;
			this.butPerm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPerm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPerm.CornerRadius = 4F;
			this.butPerm.Location = new System.Drawing.Point(11,53);
			this.butPerm.Name = "butPerm";
			this.butPerm.Size = new System.Drawing.Size(82,23);
			this.butPerm.TabIndex = 15;
			this.butPerm.Text = "Permanent";
			this.butPerm.Click += new System.EventHandler(this.butPerm_Click);
			// 
			// butPrimary
			// 
			this.butPrimary.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrimary.Autosize = true;
			this.butPrimary.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrimary.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrimary.CornerRadius = 4F;
			this.butPrimary.Location = new System.Drawing.Point(11,21);
			this.butPrimary.Name = "butPrimary";
			this.butPrimary.Size = new System.Drawing.Size(82,23);
			this.butPrimary.TabIndex = 14;
			this.butPrimary.Text = "Primary";
			this.butPrimary.Click += new System.EventHandler(this.butPrimary_Click);
			// 
			// butMixed
			// 
			this.butMixed.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMixed.Autosize = true;
			this.butMixed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMixed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMixed.CornerRadius = 4F;
			this.butMixed.Location = new System.Drawing.Point(334,33);
			this.butMixed.Name = "butMixed";
			this.butMixed.Size = new System.Drawing.Size(107,23);
			this.butMixed.TabIndex = 20;
			this.butMixed.Text = "Set Mixed Dentition";
			this.butMixed.Click += new System.EventHandler(this.butMixed_Click);
			// 
			// butAllPrimary
			// 
			this.butAllPrimary.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllPrimary.Autosize = true;
			this.butAllPrimary.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllPrimary.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllPrimary.CornerRadius = 4F;
			this.butAllPrimary.Location = new System.Drawing.Point(201,33);
			this.butAllPrimary.Name = "butAllPrimary";
			this.butAllPrimary.Size = new System.Drawing.Size(107,23);
			this.butAllPrimary.TabIndex = 19;
			this.butAllPrimary.Text = "Set All Primary";
			this.butAllPrimary.Click += new System.EventHandler(this.butAllPrimary_Click);
			// 
			// butAllPerm
			// 
			this.butAllPerm.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAllPerm.Autosize = true;
			this.butAllPerm.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAllPerm.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAllPerm.CornerRadius = 4F;
			this.butAllPerm.Location = new System.Drawing.Point(201,65);
			this.butAllPerm.Name = "butAllPerm";
			this.butAllPerm.Size = new System.Drawing.Size(107,23);
			this.butAllPerm.TabIndex = 18;
			this.butAllPerm.Text = "Set All Permanent";
			this.butAllPerm.Click += new System.EventHandler(this.butAllPerm_Click);
			// 
			// tabPlanned
			// 
			this.tabPlanned.BackColor = System.Drawing.Color.White;
			this.tabPlanned.Controls.Add(this.butDown);
			this.tabPlanned.Controls.Add(this.butUp);
			this.tabPlanned.Controls.Add(this.butPin);
			this.tabPlanned.Controls.Add(this.butClear);
			this.tabPlanned.Controls.Add(this.butNew);
			this.tabPlanned.Controls.Add(this.checkDone);
			this.tabPlanned.Controls.Add(this.gridPlanned);
			this.tabPlanned.Location = new System.Drawing.Point(4,22);
			this.tabPlanned.Name = "tabPlanned";
			this.tabPlanned.Size = new System.Drawing.Size(516,233);
			this.tabPlanned.TabIndex = 4;
			this.tabPlanned.Text = "Planned Appts";
			this.tabPlanned.UseVisualStyleBackColor = true;
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(325,1);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(75,23);
			this.butDown.TabIndex = 195;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(244,1);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(75,23);
			this.butUp.TabIndex = 194;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPin.Autosize = true;
			this.butPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPin.CornerRadius = 4F;
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(163,1);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(75,23);
			this.butPin.TabIndex = 6;
			this.butPin.Text = "Pin Board";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(82,1);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,23);
			this.butClear.TabIndex = 5;
			this.butClear.Text = "Delete";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNew.Autosize = true;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNew.CornerRadius = 4F;
			this.butNew.Image = global::OpenDental.Properties.Resources.Add;
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.Location = new System.Drawing.Point(1,1);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(75,23);
			this.butNew.TabIndex = 4;
			this.butNew.Text = "Add";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// gridPlanned
			// 
			this.gridPlanned.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPlanned.HScrollVisible = false;
			this.gridPlanned.Location = new System.Drawing.Point(0,25);
			this.gridPlanned.Name = "gridPlanned";
			this.gridPlanned.ScrollValue = 0;
			this.gridPlanned.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridPlanned.Size = new System.Drawing.Size(516,208);
			this.gridPlanned.TabIndex = 193;
			this.gridPlanned.Title = "Planned Appointments";
			this.gridPlanned.TranslationName = "TablePlannedAppts";
			this.gridPlanned.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPlanned_CellDoubleClick);
			// 
			// tabShow
			// 
			this.tabShow.BackColor = System.Drawing.Color.White;
			this.tabShow.Controls.Add(this.gridChartViews);
			this.tabShow.Controls.Add(this.labelCustView);
			this.tabShow.Controls.Add(this.butChartViewDown);
			this.tabShow.Controls.Add(this.butChartViewUp);
			this.tabShow.Controls.Add(this.butChartViewAdd);
			this.tabShow.Controls.Add(this.groupBox7);
			this.tabShow.Controls.Add(this.groupBox6);
			this.tabShow.Controls.Add(this.checkShowTeeth);
			this.tabShow.Controls.Add(this.checkNotes);
			this.tabShow.Controls.Add(this.checkAudit);
			this.tabShow.Controls.Add(this.butShowAll);
			this.tabShow.Controls.Add(this.butShowNone);
			this.tabShow.Location = new System.Drawing.Point(4,22);
			this.tabShow.Name = "tabShow";
			this.tabShow.Size = new System.Drawing.Size(516,233);
			this.tabShow.TabIndex = 5;
			this.tabShow.Text = "Show";
			this.tabShow.UseVisualStyleBackColor = true;
			// 
			// gridChartViews
			// 
			this.gridChartViews.HScrollVisible = false;
			this.gridChartViews.Location = new System.Drawing.Point(303,8);
			this.gridChartViews.Name = "gridChartViews";
			this.gridChartViews.ScrollValue = 0;
			this.gridChartViews.Size = new System.Drawing.Size(191,173);
			this.gridChartViews.TabIndex = 44;
			this.gridChartViews.Title = "Chart Views";
			this.gridChartViews.TranslationName = "GridChartViews";
			this.gridChartViews.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridChartViews_CellClick);
			this.gridChartViews.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridChartViews_DoubleClick);
			// 
			// labelCustView
			// 
			this.labelCustView.AutoSize = true;
			this.labelCustView.Font = new System.Drawing.Font("Microsoft Sans Serif",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelCustView.ForeColor = System.Drawing.Color.Red;
			this.labelCustView.Location = new System.Drawing.Point(20,184);
			this.labelCustView.Name = "labelCustView";
			this.labelCustView.Size = new System.Drawing.Size(96,16);
			this.labelCustView.TabIndex = 43;
			this.labelCustView.Text = "Custom View";
			this.labelCustView.Visible = false;
			// 
			// butChartViewDown
			// 
			this.butChartViewDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChartViewDown.Autosize = true;
			this.butChartViewDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChartViewDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChartViewDown.CornerRadius = 4F;
			this.butChartViewDown.Image = global::OpenDental.Properties.Resources.down;
			this.butChartViewDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butChartViewDown.Location = new System.Drawing.Point(426,195);
			this.butChartViewDown.Name = "butChartViewDown";
			this.butChartViewDown.Size = new System.Drawing.Size(68,24);
			this.butChartViewDown.TabIndex = 41;
			this.butChartViewDown.Text = "&Down";
			this.butChartViewDown.Click += new System.EventHandler(this.butChartViewDown_Click);
			// 
			// butChartViewUp
			// 
			this.butChartViewUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChartViewUp.Autosize = true;
			this.butChartViewUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChartViewUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChartViewUp.CornerRadius = 4F;
			this.butChartViewUp.Image = global::OpenDental.Properties.Resources.up;
			this.butChartViewUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butChartViewUp.Location = new System.Drawing.Point(367,195);
			this.butChartViewUp.Name = "butChartViewUp";
			this.butChartViewUp.Size = new System.Drawing.Size(54,24);
			this.butChartViewUp.TabIndex = 42;
			this.butChartViewUp.Text = "&Up";
			this.butChartViewUp.Click += new System.EventHandler(this.butChartViewUp_Click);
			// 
			// butChartViewAdd
			// 
			this.butChartViewAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChartViewAdd.Autosize = true;
			this.butChartViewAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChartViewAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChartViewAdd.CornerRadius = 4F;
			this.butChartViewAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butChartViewAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butChartViewAdd.Location = new System.Drawing.Point(303,195);
			this.butChartViewAdd.Name = "butChartViewAdd";
			this.butChartViewAdd.Size = new System.Drawing.Size(59,24);
			this.butChartViewAdd.TabIndex = 40;
			this.butChartViewAdd.Text = "&Add";
			this.butChartViewAdd.Click += new System.EventHandler(this.butChartViewAdd_Click);
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.checkSheets);
			this.groupBox7.Controls.Add(this.checkTasks);
			this.groupBox7.Controls.Add(this.checkEmail);
			this.groupBox7.Controls.Add(this.checkCommFamily);
			this.groupBox7.Controls.Add(this.checkAppt);
			this.groupBox7.Controls.Add(this.checkLabCase);
			this.groupBox7.Controls.Add(this.checkRx);
			this.groupBox7.Controls.Add(this.checkComm);
			this.groupBox7.Location = new System.Drawing.Point(144,4);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(125,148);
			this.groupBox7.TabIndex = 19;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Object Types";
			// 
			// checkSheets
			// 
			this.checkSheets.Checked = true;
			this.checkSheets.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkSheets.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSheets.Location = new System.Drawing.Point(10,130);
			this.checkSheets.Name = "checkSheets";
			this.checkSheets.Size = new System.Drawing.Size(102,13);
			this.checkSheets.TabIndex = 219;
			this.checkSheets.Text = "Sheets";
			this.checkSheets.Click += new System.EventHandler(this.checkSheets_Click);
			// 
			// checkTasks
			// 
			this.checkTasks.Checked = true;
			this.checkTasks.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkTasks.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTasks.Location = new System.Drawing.Point(10,66);
			this.checkTasks.Name = "checkTasks";
			this.checkTasks.Size = new System.Drawing.Size(102,13);
			this.checkTasks.TabIndex = 218;
			this.checkTasks.Text = "Tasks";
			this.checkTasks.Click += new System.EventHandler(this.checkTasks_Click);
			// 
			// checkEmail
			// 
			this.checkEmail.Checked = true;
			this.checkEmail.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEmail.Location = new System.Drawing.Point(10,82);
			this.checkEmail.Name = "checkEmail";
			this.checkEmail.Size = new System.Drawing.Size(102,13);
			this.checkEmail.TabIndex = 217;
			this.checkEmail.Text = "Email";
			this.checkEmail.Click += new System.EventHandler(this.checkEmail_Click);
			// 
			// checkCommFamily
			// 
			this.checkCommFamily.Checked = true;
			this.checkCommFamily.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkCommFamily.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCommFamily.Location = new System.Drawing.Point(26,49);
			this.checkCommFamily.Name = "checkCommFamily";
			this.checkCommFamily.Size = new System.Drawing.Size(88,13);
			this.checkCommFamily.TabIndex = 20;
			this.checkCommFamily.Text = "Family";
			this.checkCommFamily.Click += new System.EventHandler(this.checkCommFamily_Click);
			// 
			// checkAppt
			// 
			this.checkAppt.Checked = true;
			this.checkAppt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAppt.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppt.Location = new System.Drawing.Point(10,17);
			this.checkAppt.Name = "checkAppt";
			this.checkAppt.Size = new System.Drawing.Size(102,13);
			this.checkAppt.TabIndex = 20;
			this.checkAppt.Text = "Appointments";
			this.checkAppt.Click += new System.EventHandler(this.checkAppt_Click);
			// 
			// checkLabCase
			// 
			this.checkLabCase.Checked = true;
			this.checkLabCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkLabCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLabCase.Location = new System.Drawing.Point(10,98);
			this.checkLabCase.Name = "checkLabCase";
			this.checkLabCase.Size = new System.Drawing.Size(102,13);
			this.checkLabCase.TabIndex = 17;
			this.checkLabCase.Text = "Lab Cases";
			this.checkLabCase.Click += new System.EventHandler(this.checkLabCase_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.checkShowCn);
			this.groupBox6.Controls.Add(this.checkShowE);
			this.groupBox6.Controls.Add(this.checkShowR);
			this.groupBox6.Controls.Add(this.checkShowC);
			this.groupBox6.Controls.Add(this.checkShowTP);
			this.groupBox6.Location = new System.Drawing.Point(6,4);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(121,99);
			this.groupBox6.TabIndex = 18;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Procedures";
			// 
			// checkShowCn
			// 
			this.checkShowCn.Checked = true;
			this.checkShowCn.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowCn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCn.Location = new System.Drawing.Point(9,81);
			this.checkShowCn.Name = "checkShowCn";
			this.checkShowCn.Size = new System.Drawing.Size(101,13);
			this.checkShowCn.TabIndex = 15;
			this.checkShowCn.Text = "Conditions";
			this.checkShowCn.Click += new System.EventHandler(this.checkShowCn_Click);
			// 
			// butShowAll
			// 
			this.butShowAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowAll.Autosize = true;
			this.butShowAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowAll.CornerRadius = 4F;
			this.butShowAll.Location = new System.Drawing.Point(10,129);
			this.butShowAll.Name = "butShowAll";
			this.butShowAll.Size = new System.Drawing.Size(53,23);
			this.butShowAll.TabIndex = 12;
			this.butShowAll.Text = "All";
			this.butShowAll.Click += new System.EventHandler(this.butShowAll_Click);
			// 
			// butShowNone
			// 
			this.butShowNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowNone.Autosize = true;
			this.butShowNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowNone.CornerRadius = 4F;
			this.butShowNone.Location = new System.Drawing.Point(69,129);
			this.butShowNone.Name = "butShowNone";
			this.butShowNone.Size = new System.Drawing.Size(58,23);
			this.butShowNone.TabIndex = 13;
			this.butShowNone.Text = "None";
			this.butShowNone.Click += new System.EventHandler(this.butShowNone_Click);
			// 
			// tabDraw
			// 
			this.tabDraw.Controls.Add(this.radioColorChanger);
			this.tabDraw.Controls.Add(this.groupBox8);
			this.tabDraw.Controls.Add(this.panelDrawColor);
			this.tabDraw.Controls.Add(this.radioEraser);
			this.tabDraw.Controls.Add(this.radioPen);
			this.tabDraw.Controls.Add(this.radioPointer);
			this.tabDraw.Location = new System.Drawing.Point(4,22);
			this.tabDraw.Name = "tabDraw";
			this.tabDraw.Size = new System.Drawing.Size(516,233);
			this.tabDraw.TabIndex = 6;
			this.tabDraw.Text = "Draw";
			this.tabDraw.UseVisualStyleBackColor = true;
			// 
			// radioColorChanger
			// 
			this.radioColorChanger.Location = new System.Drawing.Point(14,70);
			this.radioColorChanger.Name = "radioColorChanger";
			this.radioColorChanger.Size = new System.Drawing.Size(122,17);
			this.radioColorChanger.TabIndex = 5;
			this.radioColorChanger.TabStop = true;
			this.radioColorChanger.Text = "Color Changer";
			this.radioColorChanger.UseVisualStyleBackColor = true;
			this.radioColorChanger.Click += new System.EventHandler(this.radioColorChanger_Click);
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.panelBlack);
			this.groupBox8.Controls.Add(this.label22);
			this.groupBox8.Controls.Add(this.butColorOther);
			this.groupBox8.Controls.Add(this.panelRdark);
			this.groupBox8.Controls.Add(this.label21);
			this.groupBox8.Controls.Add(this.panelRlight);
			this.groupBox8.Controls.Add(this.panelEOdark);
			this.groupBox8.Controls.Add(this.label20);
			this.groupBox8.Controls.Add(this.panelEOlight);
			this.groupBox8.Controls.Add(this.panelECdark);
			this.groupBox8.Controls.Add(this.label19);
			this.groupBox8.Controls.Add(this.panelEClight);
			this.groupBox8.Controls.Add(this.panelCdark);
			this.groupBox8.Controls.Add(this.label17);
			this.groupBox8.Controls.Add(this.panelClight);
			this.groupBox8.Controls.Add(this.panelTPdark);
			this.groupBox8.Controls.Add(this.label18);
			this.groupBox8.Controls.Add(this.panelTPlight);
			this.groupBox8.Location = new System.Drawing.Point(160,11);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(157,214);
			this.groupBox8.TabIndex = 4;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Set Color";
			// 
			// panelBlack
			// 
			this.panelBlack.BackColor = System.Drawing.Color.Black;
			this.panelBlack.Location = new System.Drawing.Point(95,147);
			this.panelBlack.Name = "panelBlack";
			this.panelBlack.Size = new System.Drawing.Size(22,22);
			this.panelBlack.TabIndex = 194;
			this.panelBlack.Click += new System.EventHandler(this.panelBlack_Click);
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(11,150);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(82,17);
			this.label22.TabIndex = 193;
			this.label22.Text = "Black";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butColorOther
			// 
			this.butColorOther.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butColorOther.Autosize = true;
			this.butColorOther.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColorOther.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColorOther.CornerRadius = 4F;
			this.butColorOther.Location = new System.Drawing.Point(95,179);
			this.butColorOther.Name = "butColorOther";
			this.butColorOther.Size = new System.Drawing.Size(50,24);
			this.butColorOther.TabIndex = 192;
			this.butColorOther.Text = "Other";
			this.butColorOther.Click += new System.EventHandler(this.butColorOther_Click);
			// 
			// panelRdark
			// 
			this.panelRdark.BackColor = System.Drawing.Color.Black;
			this.panelRdark.Location = new System.Drawing.Point(95,121);
			this.panelRdark.Name = "panelRdark";
			this.panelRdark.Size = new System.Drawing.Size(22,22);
			this.panelRdark.TabIndex = 18;
			this.panelRdark.Click += new System.EventHandler(this.panelRdark_Click);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(11,124);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(82,17);
			this.label21.TabIndex = 17;
			this.label21.Text = "Referred";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelRlight
			// 
			this.panelRlight.BackColor = System.Drawing.Color.Black;
			this.panelRlight.Location = new System.Drawing.Point(123,121);
			this.panelRlight.Name = "panelRlight";
			this.panelRlight.Size = new System.Drawing.Size(22,22);
			this.panelRlight.TabIndex = 16;
			this.panelRlight.Click += new System.EventHandler(this.panelRlight_Click);
			// 
			// panelEOdark
			// 
			this.panelEOdark.BackColor = System.Drawing.Color.Black;
			this.panelEOdark.Location = new System.Drawing.Point(95,95);
			this.panelEOdark.Name = "panelEOdark";
			this.panelEOdark.Size = new System.Drawing.Size(22,22);
			this.panelEOdark.TabIndex = 15;
			this.panelEOdark.Click += new System.EventHandler(this.panelEOdark_Click);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(11,98);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(82,17);
			this.label20.TabIndex = 14;
			this.label20.Text = "ExistOther";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelEOlight
			// 
			this.panelEOlight.BackColor = System.Drawing.Color.Black;
			this.panelEOlight.Location = new System.Drawing.Point(123,95);
			this.panelEOlight.Name = "panelEOlight";
			this.panelEOlight.Size = new System.Drawing.Size(22,22);
			this.panelEOlight.TabIndex = 13;
			this.panelEOlight.Click += new System.EventHandler(this.panelEOlight_Click);
			// 
			// panelECdark
			// 
			this.panelECdark.BackColor = System.Drawing.Color.Black;
			this.panelECdark.Location = new System.Drawing.Point(95,69);
			this.panelECdark.Name = "panelECdark";
			this.panelECdark.Size = new System.Drawing.Size(22,22);
			this.panelECdark.TabIndex = 12;
			this.panelECdark.Click += new System.EventHandler(this.panelECdark_Click);
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(11,72);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(82,17);
			this.label19.TabIndex = 11;
			this.label19.Text = "ExistCurProv";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelEClight
			// 
			this.panelEClight.BackColor = System.Drawing.Color.Black;
			this.panelEClight.Location = new System.Drawing.Point(123,69);
			this.panelEClight.Name = "panelEClight";
			this.panelEClight.Size = new System.Drawing.Size(22,22);
			this.panelEClight.TabIndex = 10;
			this.panelEClight.Click += new System.EventHandler(this.panelEClight_Click);
			// 
			// panelCdark
			// 
			this.panelCdark.BackColor = System.Drawing.Color.Black;
			this.panelCdark.Location = new System.Drawing.Point(95,43);
			this.panelCdark.Name = "panelCdark";
			this.panelCdark.Size = new System.Drawing.Size(22,22);
			this.panelCdark.TabIndex = 9;
			this.panelCdark.Click += new System.EventHandler(this.panelCdark_Click);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(11,46);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(82,17);
			this.label17.TabIndex = 8;
			this.label17.Text = "Complete";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelClight
			// 
			this.panelClight.BackColor = System.Drawing.Color.Black;
			this.panelClight.Location = new System.Drawing.Point(123,43);
			this.panelClight.Name = "panelClight";
			this.panelClight.Size = new System.Drawing.Size(22,22);
			this.panelClight.TabIndex = 7;
			this.panelClight.Click += new System.EventHandler(this.panelClight_Click);
			// 
			// panelTPdark
			// 
			this.panelTPdark.BackColor = System.Drawing.Color.Black;
			this.panelTPdark.Location = new System.Drawing.Point(95,17);
			this.panelTPdark.Name = "panelTPdark";
			this.panelTPdark.Size = new System.Drawing.Size(22,22);
			this.panelTPdark.TabIndex = 6;
			this.panelTPdark.Click += new System.EventHandler(this.panelTPdark_Click);
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(11,20);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(82,17);
			this.label18.TabIndex = 5;
			this.label18.Text = "TreatPlan";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelTPlight
			// 
			this.panelTPlight.BackColor = System.Drawing.Color.Black;
			this.panelTPlight.Location = new System.Drawing.Point(123,17);
			this.panelTPlight.Name = "panelTPlight";
			this.panelTPlight.Size = new System.Drawing.Size(22,22);
			this.panelTPlight.TabIndex = 4;
			this.panelTPlight.Click += new System.EventHandler(this.panelTPlight_Click);
			// 
			// panelDrawColor
			// 
			this.panelDrawColor.BackColor = System.Drawing.Color.Black;
			this.panelDrawColor.Location = new System.Drawing.Point(13,101);
			this.panelDrawColor.Name = "panelDrawColor";
			this.panelDrawColor.Size = new System.Drawing.Size(22,22);
			this.panelDrawColor.TabIndex = 3;
			this.panelDrawColor.DoubleClick += new System.EventHandler(this.panelDrawColor_DoubleClick);
			// 
			// radioEraser
			// 
			this.radioEraser.Location = new System.Drawing.Point(14,51);
			this.radioEraser.Name = "radioEraser";
			this.radioEraser.Size = new System.Drawing.Size(122,17);
			this.radioEraser.TabIndex = 2;
			this.radioEraser.TabStop = true;
			this.radioEraser.Text = "Eraser";
			this.radioEraser.UseVisualStyleBackColor = true;
			this.radioEraser.Click += new System.EventHandler(this.radioEraser_Click);
			// 
			// radioPen
			// 
			this.radioPen.Location = new System.Drawing.Point(14,32);
			this.radioPen.Name = "radioPen";
			this.radioPen.Size = new System.Drawing.Size(122,17);
			this.radioPen.TabIndex = 1;
			this.radioPen.TabStop = true;
			this.radioPen.Text = "Pen";
			this.radioPen.UseVisualStyleBackColor = true;
			this.radioPen.Click += new System.EventHandler(this.radioPen_Click);
			// 
			// radioPointer
			// 
			this.radioPointer.Checked = true;
			this.radioPointer.Location = new System.Drawing.Point(14,13);
			this.radioPointer.Name = "radioPointer";
			this.radioPointer.Size = new System.Drawing.Size(122,17);
			this.radioPointer.TabIndex = 0;
			this.radioPointer.TabStop = true;
			this.radioPointer.Text = "Pointer";
			this.radioPointer.UseVisualStyleBackColor = true;
			this.radioPointer.Click += new System.EventHandler(this.radioPointer_Click);
			// 
			// menuConsent
			// 
			this.menuConsent.Popup += new System.EventHandler(this.menuConsent_Popup);
			// 
			// panelEcw
			// 
			this.panelEcw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelEcw.Controls.Add(this.labelECWerror);
			this.panelEcw.Controls.Add(this.webBrowserEcw);
			this.panelEcw.Controls.Add(this.butECWdown);
			this.panelEcw.Controls.Add(this.butECWup);
			this.panelEcw.Location = new System.Drawing.Point(444,521);
			this.panelEcw.Name = "panelEcw";
			this.panelEcw.Size = new System.Drawing.Size(373,65);
			this.panelEcw.TabIndex = 197;
			// 
			// labelECWerror
			// 
			this.labelECWerror.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelECWerror.Location = new System.Drawing.Point(25,22);
			this.labelECWerror.Name = "labelECWerror";
			this.labelECWerror.Size = new System.Drawing.Size(314,27);
			this.labelECWerror.TabIndex = 199;
			this.labelECWerror.Text = "Error:";
			this.labelECWerror.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// webBrowserEcw
			// 
			this.webBrowserEcw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserEcw.Location = new System.Drawing.Point(1,11);
			this.webBrowserEcw.MinimumSize = new System.Drawing.Size(20,20);
			this.webBrowserEcw.Name = "webBrowserEcw";
			this.webBrowserEcw.Size = new System.Drawing.Size(370,52);
			this.webBrowserEcw.TabIndex = 198;
			this.webBrowserEcw.Url = new System.Uri("",System.UriKind.Relative);
			// 
			// butECWdown
			// 
			this.butECWdown.AdjustImageLocation = new System.Drawing.Point(0,-1);
			this.butECWdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butECWdown.Autosize = true;
			this.butECWdown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butECWdown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butECWdown.CornerRadius = 2F;
			this.butECWdown.Image = global::OpenDental.Properties.Resources.arrowDownTriangle;
			this.butECWdown.Location = new System.Drawing.Point(321,1);
			this.butECWdown.Name = "butECWdown";
			this.butECWdown.Size = new System.Drawing.Size(24,9);
			this.butECWdown.TabIndex = 197;
			this.butECWdown.UseVisualStyleBackColor = true;
			this.butECWdown.Click += new System.EventHandler(this.butECWdown_Click);
			// 
			// butECWup
			// 
			this.butECWup.AdjustImageLocation = new System.Drawing.Point(0,-1);
			this.butECWup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butECWup.Autosize = true;
			this.butECWup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butECWup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butECWup.CornerRadius = 2F;
			this.butECWup.Image = global::OpenDental.Properties.Resources.arrowUpTriangle;
			this.butECWup.Location = new System.Drawing.Point(346,1);
			this.butECWup.Name = "butECWup";
			this.butECWup.Size = new System.Drawing.Size(24,9);
			this.butECWup.TabIndex = 196;
			this.butECWup.UseVisualStyleBackColor = true;
			this.butECWup.Click += new System.EventHandler(this.butECWup_Click);
			// 
			// menuToothChart
			// 
			this.menuToothChart.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemChartBig,
            this.menuItemChartSave});
			this.menuToothChart.Popup += new System.EventHandler(this.menuToothChart_Popup);
			// 
			// menuItemChartBig
			// 
			this.menuItemChartBig.Index = 0;
			this.menuItemChartBig.Text = "Show Big";
			this.menuItemChartBig.Click += new System.EventHandler(this.menuItemChartBig_Click);
			// 
			// menuItemChartSave
			// 
			this.menuItemChartSave.Index = 1;
			this.menuItemChartSave.Text = "Save to Images";
			this.menuItemChartSave.Click += new System.EventHandler(this.menuItemChartSave_Click);
			// 
			// toothChart
			// 
			this.toothChart.AutoFinish = false;
			this.toothChart.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.toothChart.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChart.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChart.DeviceFormat = null;
			this.toothChart.DrawMode = OpenDentBusiness.DrawingMode.Simple2D;
			this.toothChart.Location = new System.Drawing.Point(0,26);
			this.toothChart.Name = "toothChart";
			this.toothChart.PerioMode = false;
			this.toothChart.PreferredPixelFormatNumber = 0;
			this.toothChart.Size = new System.Drawing.Size(410,307);
			this.toothChart.TabIndex = 194;
			toothChartData2.SizeControl = new System.Drawing.Size(410,307);
			this.toothChart.TcData = toothChartData2;
			this.toothChart.UseHardware = false;
			this.toothChart.SegmentDrawn += new SparksToothChart.ToothChartDrawEventHandler(this.toothChart_SegmentDrawn);
			// 
			// butForeignKey
			// 
			this.butForeignKey.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butForeignKey.Autosize = true;
			this.butForeignKey.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butForeignKey.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butForeignKey.CornerRadius = 4F;
			this.butForeignKey.Enabled = false;
			this.butForeignKey.Location = new System.Drawing.Point(253,424);
			this.butForeignKey.Name = "butForeignKey";
			this.butForeignKey.Size = new System.Drawing.Size(75,14);
			this.butForeignKey.TabIndex = 196;
			this.butForeignKey.Text = "Foreign Key";
			this.butForeignKey.UseVisualStyleBackColor = true;
			this.butForeignKey.Visible = false;
			this.butForeignKey.Click += new System.EventHandler(this.butForeignKey_Click);
			// 
			// butAddKey
			// 
			this.butAddKey.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddKey.Autosize = true;
			this.butAddKey.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddKey.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddKey.CornerRadius = 4F;
			this.butAddKey.Enabled = false;
			this.butAddKey.Location = new System.Drawing.Point(334,424);
			this.butAddKey.Name = "butAddKey";
			this.butAddKey.Size = new System.Drawing.Size(78,14);
			this.butAddKey.TabIndex = 195;
			this.butAddKey.Text = "USA Key";
			this.butAddKey.UseVisualStyleBackColor = true;
			this.butAddKey.Visible = false;
			this.butAddKey.Click += new System.EventHandler(this.butAddKey_Click);
			// 
			// gridProg
			// 
			this.gridProg.HScrollVisible = true;
			this.gridProg.Location = new System.Drawing.Point(415,291);
			this.gridProg.Name = "gridProg";
			this.gridProg.ScrollValue = 0;
			this.gridProg.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProg.Size = new System.Drawing.Size(524,227);
			this.gridProg.TabIndex = 192;
			this.gridProg.Title = "Progress Notes";
			this.gridProg.TranslationName = "TableProg";
			this.gridProg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridProg_MouseUp);
			this.gridProg.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridProg_CellDoubleClick);
			this.gridProg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridProg_KeyDown);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,25);
			this.ToolBarMain.TabIndex = 177;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(127,692);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75,23);
			this.button1.TabIndex = 36;
			this.button1.Text = "invisible";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textTreatmentNotes
			// 
			this.textTreatmentNotes.AcceptsReturn = true;
			this.textTreatmentNotes.Location = new System.Drawing.Point(0,334);
			this.textTreatmentNotes.Multiline = true;
			this.textTreatmentNotes.Name = "textTreatmentNotes";
			this.textTreatmentNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.ChartTreatment;
			this.textTreatmentNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textTreatmentNotes.Size = new System.Drawing.Size(411,71);
			this.textTreatmentNotes.TabIndex = 187;
			this.textTreatmentNotes.TextChanged += new System.EventHandler(this.textTreatmentNotes_TextChanged);
			this.textTreatmentNotes.Leave += new System.EventHandler(this.textTreatmentNotes_Leave);
			// 
			// gridPtInfo
			// 
			this.gridPtInfo.HScrollVisible = false;
			this.gridPtInfo.Location = new System.Drawing.Point(0,405);
			this.gridPtInfo.Name = "gridPtInfo";
			this.gridPtInfo.ScrollValue = 0;
			this.gridPtInfo.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridPtInfo.Size = new System.Drawing.Size(411,325);
			this.gridPtInfo.TabIndex = 193;
			this.gridPtInfo.Title = "Patient Info";
			this.gridPtInfo.TranslationName = "TableChartPtInfo";
			this.gridPtInfo.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPtInfo_CellDoubleClick);
			// 
			// ContrChart
			// 
			this.Controls.Add(this.panelEcw);
			this.Controls.Add(this.butForeignKey);
			this.Controls.Add(this.butAddKey);
			this.Controls.Add(this.toothChart);
			this.Controls.Add(this.gridProg);
			this.Controls.Add(this.tabProc);
			this.Controls.Add(this.panelImages);
			this.Controls.Add(this.tabControlImages);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textTreatmentNotes);
			this.Controls.Add(this.gridPtInfo);
			this.Name = "ContrChart";
			this.Size = new System.Drawing.Size(939,708);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrChart_Layout);
			this.Resize += new System.EventHandler(this.ContrChart_Resize);
			this.groupBox2.ResumeLayout(false);
			this.tabControlImages.ResumeLayout(false);
			this.panelImages.ResumeLayout(false);
			this.tabProc.ResumeLayout(false);
			this.tabEnterTx.ResumeLayout(false);
			this.tabEnterTx.PerformLayout();
			this.panelQuickButtons.ResumeLayout(false);
			this.panelQuickPasteAmalgam.ResumeLayout(false);
			this.tabMissing.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabMovements.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabPrimary.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.tabPlanned.ResumeLayout(false);
			this.tabShow.ResumeLayout(false);
			this.tabShow.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.tabDraw.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.panelEcw.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void ContrChart_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
			gridProg.Height=ClientSize.Height-tabControlImages.Height-gridProg.Location.Y+1;
			if(panelImages.Visible){
				gridProg.Height-=(panelImages.Height+2);
			}
			gridProg.Invalidate();
		}

		private void ContrChart_Resize(object sender,EventArgs e) {
			if(!ProgramC.HListIsNull() && Programs.IsEnabled("eClinicalWorks") 
				&& ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") 
			{
				gridProg.Width=524;
				if(panelImages.Visible) {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1-(panelImages.Height+2);
				}
				else {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1;
				}
				return;
			}
			if(gridProg.Columns !=null && gridProg.Columns.Count>0){
				int gridW=0;
				for(int i=0;i<gridProg.Columns.Count;i++){
					gridW+=gridProg.Columns[i].ColWidth;
				}
				if(gridProg.Location.X+gridW+20 < ClientSize.Width){//if space is big enough to allow full width
					gridProg.Width=gridW+20;
				}
				else{
					if(ClientSize.Width>gridProg.Location.X){//prevents an error
						gridProg.Width=ClientSize.Width-gridProg.Location.X-1;
					}
				}
			}
			gridPtInfo.Height=tabControlImages.Top-gridPtInfo.Top;
		}

		///<summary></summary>
		public void InitializeOnStartup(){
			if(InitializedOnStartup) {
				return;
			}
			InitializedOnStartup=true;
			newStatus=ProcStat.TP;
			//ApptPlanned=new ContrApptSingle();
			//ApptPlanned.Location=new Point(1,3);
			//ApptPlanned.Visible=false;
			//tabPlanned.Controls.Add(ApptPlanned);
			//ApptPlanned.DoubleClick += new System.EventHandler(ApptPlanned_DoubleClick);
			tabProc.SelectedIndex=0;
			tabProc.Height=259;
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				toothChart.Location=new Point(524+2,26);
				textTreatmentNotes.Location=new Point(524+2,toothChart.Bottom+1);
				textTreatmentNotes.Size=new Size(411,40);//make it a bit smaller than usual
				gridPtInfo.Visible=false;
				panelEcw.Visible=true;
				panelEcw.Location=new Point(524+2,textTreatmentNotes.Bottom+1);
				panelEcw.Size=new Size(411,tabControlImages.Top-panelEcw.Top+1);
				butECWdown.Location=butECWup.Location;//they will be at the same location, but just hide one or the other.
				butECWdown.Visible=false;
				tabProc.Location=new Point(0,28);
				gridProg.Location=new Point(0,tabProc.Bottom+2);
				gridProg.Height=this.ClientSize.Height-gridProg.Location.Y-2;
			}
			else {//normal:
				toothChart.Location=new Point(0,26);
				textTreatmentNotes.Location=new Point(0,toothChart.Bottom+1);
				textTreatmentNotes.Size=new Size(411,71);
				gridPtInfo.Visible=true;
				gridPtInfo.Location=new Point(0,textTreatmentNotes.Bottom+1);
				panelEcw.Visible=false;
				tabProc.Location=new Point(415,28);
				gridProg.Location=new Point(415,tabProc.Bottom+2);
				gridProg.Height=this.ClientSize.Height-gridProg.Location.Y-2;
			}
			//original two lines replaced by code above:
			//gridProg.Location=new Point(tabProc.Left,tabProc.Bottom+2);
			//gridProg.Height=this.ClientSize.Height-gridProg.Location.Y-2;
			//can't use Lan.F
			Lan.C(this,new Control[]{
				//groupPlanned,
				checkDone,
				butNew,
				butClear,
				checkShowTP,
				checkShowC,
				checkShowE,
				checkShowR,
				checkRx,
				checkNotes,
				labelDx,
				butM,
				butOI,
				butD,
				butL,
				butBF,
				butV,
				groupBox2,
				radioEntryTP,
				radioEntryC,
				radioEntryEC,
				radioEntryEO,
				radioEntryR,
				checkToday,
				labelDx,
				label6,
				butAddProc,
				label14,
				//textProcCode is handled in ClearButtons()
				butOK,
				label13
			});
			Lan.C(this,new Control[]{
				//these were missing.  Added as recursive.
				tabEnterTx,
				tabMissing,
				tabMovements,
				tabPrimary,
				tabPlanned,
				tabShow,
				tabDraw},
				true);
			LayoutToolBar();
			ComputerPref localComputerPrefs=ComputerPrefs.GetForLocalComputer();
			this.toothChart.DeviceFormat=new ToothChartDirectX.DirectXDeviceFormat(localComputerPrefs.DirectXFormat);
			this.toothChart.DrawMode=localComputerPrefs.GraphicsSimple;//triggers ResetControls.
		}

		///<summary>Called every time prefs are changed from any workstation.</summary>
		public void InitializeLocalData(){
			butAddKey.Visible=PrefC.GetBool(PrefName.DistributorKey);
			butForeignKey.Visible=PrefC.GetBool(PrefName.DistributorKey);
			ComputerPref computerPref=ComputerPrefs.GetForLocalComputer();
			toothChart.SetToothNumberingNomenclature((ToothNumberingNomenclature)PrefC.GetInt(PrefName.UseInternationalToothNumbers));
			toothChart.UseHardware=computerPref.GraphicsUseHardware;
			toothChart.PreferredPixelFormatNumber=computerPref.PreferredPixelFormatNum;
			toothChart.DeviceFormat=new ToothChartDirectX.DirectXDeviceFormat(computerPref.DirectXFormat);
			//Must be last preference set here, because this causes the 
																													//pixel format to be recreated.
			toothChart.DrawMode=computerPref.GraphicsSimple;
			//The preferred pixel format number changes to the selected pixel format number after a context is chosen.
			computerPref.PreferredPixelFormatNum=toothChart.PreferredPixelFormatNumber;
			ComputerPrefs.Update(computerPref);
			if(PatCur!=null){
				FillToothChart(true);
			}
			if(PrefC.GetBoolSilent(PrefName.ChartQuickAddHideAmalgam,true)){
				panelQuickPasteAmalgam.Visible=false;
			}
			else{
				panelQuickPasteAmalgam.Visible=true;
			}
			if(ToolButItems.List!=null){
				LayoutToolBar();
				if(PatCur==null) {
					ToolBarMain.Buttons["Rx"].Enabled=false;
					ToolBarMain.Buttons["LabCase"].Enabled=false;
					ToolBarMain.Buttons["Perio"].Enabled = false;
					ToolBarMain.Buttons["Consent"].Enabled = false;
					ToolBarMain.Buttons["ToothChart"].Enabled = false;
					ToolBarMain.Buttons["ExamSheet"].Enabled=false;
					if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
						ToolBarMain.Buttons["Commlog"].Enabled=false;
						webBrowserEcw.Url=null;
					}
				}
			}
		}

		///<summary>Causes the toolbars to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"New Rx"),1,"","Rx"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"LabCase"),-1,"","LabCase"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Perio Chart"),2,"","Perio"));
			button=new ODToolBarButton(Lan.g(this,"Consent"),-1,"","Consent");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuConsent;
			ToolBarMain.Buttons.Add(button);
			if(PrefC.GetBool(PrefName.ToothChartMoveMenuToRight)) {
				ToolBarMain.Buttons.Add(new ODToolBarButton("                                .",-1,"",""));
			}
			button=new ODToolBarButton(Lan.g(this,"Tooth Chart"),-1,"","ToothChart");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuToothChart;
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton(Lan.g(this,"Exam Sheet"),-1,"","ExamSheet");
			button.Style=ODToolBarButtonStyle.PushButton;
			ToolBarMain.Buttons.Add(button);
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Commlog"),4,Lan.g(this,"New Commlog Entry"),"Commlog"));
			}
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ChartModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
			Plugins.HookAddCode(this,"ContrChart.LayoutToolBar_end",PatCur);
		}

		///<summary></summary>
		public void ModuleSelected(long patNum) {
			EasyHideClinicalData();
			RefreshModuleData(patNum);
			RefreshModuleScreen();
			Plugins.HookAddCode(this,"ContrChart.ModuleSelected_end",patNum);
		}

		///<summary></summary>
		public void ModuleUnselected(){
			//toothChart.Dispose();?
			if(FamCur==null)
				return;
			if(TreatmentNoteChanged){
				PatientNoteCur.Treatment=textTreatmentNotes.Text;
				PatientNotes.Update(PatientNoteCur, PatCur.Guarantor);
				TreatmentNoteChanged=false;
			}
			FamCur=null;
			PatCur=null;
			PlanList=null;
			Plugins.HookAddCode(this,"ContrChart.ModuleUnselected_end");
		}

		private void RefreshModuleData(long patNum) {
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.RefreshForFam(FamCur);
			PatPlanList=PatPlans.Refresh(patNum);
			BenefitList=Benefits.Refresh(PatPlanList);
			PatientNoteCur=PatientNotes.Refresh(patNum,PatCur.Guarantor);
			if(PrefC.UsingAtoZfolder) {
				patFolder=ImageStore.GetPatientFolder(PatCur);//GetImageFolder();
			}
			DocumentList=Documents.GetAllWithPat(patNum);
			ApptList=Appointments.GetForPat(patNum);
			ToothInitialList=ToothInitials.Refresh(patNum);
			PatFieldList=PatFields.Refresh(patNum);
		}		

		private void RefreshModuleScreen(){
			//ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(PatCur==null){
				//groupShow.Enabled=false;
				gridPtInfo.Enabled=false;
				//tabPlanned.Enabled=false;
				toothChart.Enabled=false;
				gridProg.Enabled=false;
				ToolBarMain.Buttons["Rx"].Enabled=false;
				ToolBarMain.Buttons["LabCase"].Enabled=false;
				ToolBarMain.Buttons["Perio"].Enabled = false;
				ToolBarMain.Buttons["Consent"].Enabled = false;
				ToolBarMain.Buttons["ToothChart"].Enabled = false;
				ToolBarMain.Buttons["ExamSheet"].Enabled=false;
				if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
					ToolBarMain.Buttons["Commlog"].Enabled=false;
					webBrowserEcw.Url=null;
				}
				tabProc.Enabled = false;
				butAddKey.Enabled=false;
				butForeignKey.Enabled=false;
			}
			else {
				//groupShow.Enabled=true;
				gridPtInfo.Enabled=true;
				//groupPlanned.Enabled=true;
				toothChart.Enabled=true;
				gridProg.Enabled=true;
				ToolBarMain.Buttons["Rx"].Enabled=true;
				ToolBarMain.Buttons["LabCase"].Enabled=true;
				ToolBarMain.Buttons["Perio"].Enabled = true;
				ToolBarMain.Buttons["Consent"].Enabled = true;
				ToolBarMain.Buttons["ToothChart"].Enabled =true;
				ToolBarMain.Buttons["ExamSheet"].Enabled=true;
				if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
					ToolBarMain.Buttons["Commlog"].Enabled=true;
					//the following sequence also gets repeated after exiting the Rx window to refresh.
					String strAppServer="";
					try {
						//ecwEx.InitClass oExInit=new ecwEx.InitClass();
						strAppServer=VBbridges.Ecw.GetAppServer((int)Bridges.ECW.UserId,Bridges.ECW.EcwConfigPath);
						//oExInit.getAppServer();
						webBrowserEcw.Url=new Uri("http://"+strAppServer+"/mobiledoc/jsp/dashboard/Overview.jsp?ptId="
							+PatCur.PatNum.ToString()+"&panelName=overview&pnencid="
							+Bridges.ECW.AptNum.ToString()+"&context=progressnotes&TrUserId="+Bridges.ECW.UserId.ToString());
						labelECWerror.Visible=false;
					}
					catch (Exception ex){
						webBrowserEcw.Url=null;
						labelECWerror.Text="Error: "+ex.Message;
						labelECWerror.Visible=true;
					}
				}
				tabProc.Enabled=true;
				butAddKey.Enabled=true;
				butForeignKey.Enabled=true;
				if(PrevPtNum != PatCur.PatNum) {//reset to TP status on every new patient selected
					if(PrefC.GetBool(PrefName.AutoResetTPEntryStatus)) {
						radioEntryTP.Select();
					}
					PrevPtNum = PatCur.PatNum;
				}
			}
			ToolBarMain.Invalidate();
			ClearButtons();
			FillChartViewsGrid();
			FillProgNotes();
			FillPlanned();
			FillPtInfo();
			FillDxProcImage();
			FillImages();
		}

		private void EasyHideClinicalData(){
			if(PrefC.GetBool(PrefName.EasyHideClinical)){
				gridPtInfo.Visible=false;
				checkShowE.Visible=false;
				checkShowR.Visible=false;
				checkRx.Visible=false;
				checkComm.Visible=false;
				checkNotes.Visible=false;
				butShowNone.Visible=false;
				butShowAll.Visible=false;
				//panelEnterTx.Visible=false;//next line changes it, though
				radioEntryEC.Visible=false;
				radioEntryEO.Visible=false;
				radioEntryR.Visible=false;
				labelDx.Visible=false;
				listDx.Visible=false;
			}
			else{
				gridPtInfo.Visible=true;
				checkShowE.Visible=true;
				checkShowR.Visible=true;
				checkRx.Visible=true;
				checkComm.Visible=true;
				checkNotes.Visible=true;
				butShowNone.Visible=true;
				butShowAll.Visible=true;
				radioEntryEC.Visible=true;
				radioEntryEO.Visible=true;
				radioEntryR.Visible=true;
				labelDx.Visible=true;
				listDx.Visible=true;
			}
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Rx":
						OnRx_Click();
						break;
					case "LabCase":
						OnLabCase_Click();
						break;
					case "Perio":
						OnPerio_Click();
						break;
					case "Anesthesia":
						OnAnesthesia_Click();
						break;
					case "Consent":
						OnConsent_Click();
						break;
					case "Commlog"://only for eCW
						OnCommlog_Click();
						break;
					case "ToothChart":
						OnToothChart_Click();
						break;
					case "ExamSheet":
						OnExamSheet_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(long)) {
				ProgramL.Execute((long)e.Button.Tag,PatCur);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(long patNum,string patName,bool hasEmail,string chartNumber) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum,patName,hasEmail,chartNumber);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		private void OnRx_Click(){
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				VBbridges.Ecw.LoadRxForm((int)Bridges.ECW.UserId,Bridges.ECW.EcwConfigPath,(int)Bridges.ECW.AptNum);
				//refresh the right panel:
				try {
					string strAppServer=VBbridges.Ecw.GetAppServer((int)Bridges.ECW.UserId,Bridges.ECW.EcwConfigPath);
					webBrowserEcw.Url=new Uri("http://"+strAppServer+"/mobiledoc/jsp/dashboard/Overview.jsp?ptId="
							+PatCur.PatNum.ToString()+"&panelName=overview&pnencid="
							+Bridges.ECW.AptNum.ToString()+"&context=progressnotes&TrUserId="+Bridges.ECW.UserId.ToString());
					labelECWerror.Visible=false;
				}
				catch(Exception ex) {
					webBrowserEcw.Url=null;
					labelECWerror.Text="Error: "+ex.Message;
					labelECWerror.Visible=true;
				}
			}
			else {
				if(!Security.IsAuthorized(Permissions.RxCreate)) {
					return;
				}
				FormRxSelect FormRS=new FormRxSelect(PatCur);
				FormRS.ShowDialog();
				if(FormRS.DialogResult!=DialogResult.OK) return;
				ModuleSelected(PatCur.PatNum);
				SecurityLogs.MakeLogEntry(Permissions.RxCreate,PatCur.PatNum,PatCur.GetNameLF());
			}
		}

		private void OnLabCase_Click() {
			LabCase lab=new LabCase();
			lab.PatNum=PatCur.PatNum;
			lab.ProvNum=Patients.GetProvNum(PatCur);
			lab.DateTimeCreated=MiscData.GetNowDateTime();
			LabCases.Insert(lab);//it will be deleted inside the form if user clicks cancel.
			//We need the primary key in order to attach lab slip.
			FormLabCaseEdit FormL=new FormLabCaseEdit();
			FormL.CaseCur=lab;
			FormL.IsNew=true;
			FormL.ShowDialog();
			if(FormL.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void OnPerio_Click(){
			FormPerio FormP=new FormPerio(PatCur);
			FormP.ShowDialog();
		}

		private void OnAnesthesia_Click(){
			/*
			AnestheticData AnestheticDataCur;
			AnestheticDataCur = new AnestheticData();
			FormAnestheticRecord FormAR = new FormAnestheticRecord(PatCur, AnestheticDataCur);
			FormAR.ShowDialog();

			PatCur = Patients.GetPat(Convert.ToInt32(PatCur.PatNum));
			OnPatientSelected(Convert.ToInt32(PatCur.PatNum), Convert.ToString(PatCur), true, Convert.ToString(PatCur));
			FillPtInfo();
			return;*/
		}

		private void OnConsent_Click() {
			List<SheetDef> listSheets=SheetDefs.GetCustomForType(SheetTypeEnum.Consent);
			if(listSheets.Count>0){
				MsgBox.Show(this,"Please use dropdown list.");
				return;
			}
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.Consent);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef,PatCur.PatNum);
			SheetParameter.SetParameter(sheet,"PatNum",PatCur.PatNum);
			SheetFiller.FillFields(sheet);
			Graphics g=this.CreateGraphics();
			SheetUtil.CalculateHeights(sheet,g);
			g.Dispose();
			FormSheetFillEdit FormS=new FormSheetFillEdit(sheet);
			FormS.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void OnToothChart_Click() {
			MsgBox.Show(this,"Please use dropdown list.");
			return;
		}

		private void OnExamSheet_Click(){
			FormExamSheets fes=new FormExamSheets();
			fes.PatNum=PatCur.PatNum;
			fes.ShowDialog();
			RefreshModuleScreen();
		}

		///<summary>Only used for eCW.  Everyone else has the commlog button up in the main toolbar.</summary>
		private void OnCommlog_Click() {
			Commlog CommlogCur = new Commlog();
			CommlogCur.PatNum = PatCur.PatNum;
			CommlogCur.CommDateTime = DateTime.Now;
			CommlogCur.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
			CommlogCur.Mode_=CommItemMode.Phone;
			CommlogCur.SentOrReceived=CommSentOrReceived.Received;
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI = new FormCommItem(CommlogCur);
			FormCI.IsNew = true;
			FormCI.ShowDialog();
			if(FormCI.DialogResult == DialogResult.OK) {
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void menuConsent_Popup(object sender,EventArgs e) {
			menuConsent.MenuItems.Clear();
			List<SheetDef> listSheets=SheetDefs.GetCustomForType(SheetTypeEnum.Consent);
			MenuItem menuItem;
			for(int i=0;i<listSheets.Count;i++){
				menuItem=new MenuItem(listSheets[i].Description);
				menuItem.Tag=listSheets[i];
				menuItem.Click+=new EventHandler(menuConsent_Click);
				menuConsent.MenuItems.Add(menuItem);
			}
		}

		private void menuConsent_Click(object sender,EventArgs e) {
			SheetDef sheetDef=(SheetDef)(((MenuItem)sender).Tag);
			SheetDefs.GetFieldsAndParameters(sheetDef);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef,PatCur.PatNum);
			SheetParameter.SetParameter(sheet,"PatNum",PatCur.PatNum);
			SheetFiller.FillFields(sheet);
			Graphics g=this.CreateGraphics();
			SheetUtil.CalculateHeights(sheet,g);
			g.Dispose();
			FormSheetFillEdit FormS=new FormSheetFillEdit(sheet);
			FormS.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void menuToothChart_Popup(object sender,EventArgs e) {
			//ComputerPref computerPref=ComputerPrefs.GetForLocalComputer();
			//only enable the big button if 3D graphics
			/*if(computerPref.GraphicsSimple) {
				menuItemChartBig.Enabled=false;
			}
			else {
				menuItemChartBig.Enabled=true;
			}*/
		}

		private void menuItemChartBig_Click(object sender,EventArgs e) {
			FormToothChartingBig FormT=new FormToothChartingBig(checkShowTeeth.Checked,ToothInitialList,ProcList);
			FormT.Show();
		}

		private void menuItemChartSave_Click(object sender,EventArgs e) {
			long defNum=0;
			Def[] defs=DefC.GetList(DefCat.ImageCats);
			for(int i=0;i<defs.Length;i++){
				if(defs[i].ItemValue.Contains("T")){
					defNum=defs[i].DefNum;
					break;
				}
			}
			if(defNum==0){//no category set for Tooth Charts.
				MessageBox.Show(Lan.g(this,"No Def set for Tooth Charts."));
				return;
			}
			Point origin=this.PointToScreen(toothChart.Location);
			Bitmap chartBitmap=new Bitmap(toothChart.Width,toothChart.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics g=Graphics.FromImage(chartBitmap);
			g.CopyFromScreen(origin,new Point(0,0),toothChart.Size,CopyPixelOperation.SourceCopy);
			g.Dispose();
			try {
				//OpenDental.Imaging.ImageStoreBase imageStore=OpenDental.Imaging.ImageStore.GetImageStore(PatCur);
				ImageStore.Import(chartBitmap,defNum,ImageType.Photo,PatCur);
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Unable to save file: ") + ex.Message);
				return;
			}
			MsgBox.Show(this,"Done.");
		}

		public void FillPtInfo() {
			if(Plugins.HookMethod(this,"ContrChart.FillPtInfo",PatCur)) {
				return;
			}
			gridPtInfo.Height=tabControlImages.Top-gridPtInfo.Top;
			textTreatmentNotes.Text="";
			if(PatCur==null) {
				gridPtInfo.BeginUpdate();
				gridPtInfo.Rows.Clear();
				gridPtInfo.Columns.Clear();
				gridPtInfo.EndUpdate();
				return;
			}
			else {
				textTreatmentNotes.Text=PatientNoteCur.Treatment;
				textTreatmentNotes.Enabled=true;
				textTreatmentNotes.Select(textTreatmentNotes.Text.Length+2,1);
				textTreatmentNotes.ScrollToCaret();
				TreatmentNoteChanged=false;
			}
			gridPtInfo.BeginUpdate();
			gridPtInfo.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);//Lan.g("TableChartPtInfo",""),);
			gridPtInfo.Columns.Add(col);
			col=new ODGridColumn("",300);
			gridPtInfo.Columns.Add(col);
			gridPtInfo.Rows.Clear();
			ODGridCell cell;
			ODGridRow row;
			List<DisplayField> fields=DisplayFields.GetForCategory(DisplayFieldCategory.ChartPatientInformation);
			for(int f=0;f<fields.Count;f++) {
				row=new ODGridRow();
				//within a case statement, the row may be re-instantiated if needed, effectively removing the first cell added here:
				if(fields[f].Description=="") {
					row.Cells.Add(fields[f].InternalName);
				}
				else {
					row.Cells.Add(fields[f].Description);
				}
				switch(fields[f].InternalName) {
					case "Age":
						row.Cells.Add(PatientLogic.DateToAgeString(PatCur.Birthdate));
						break;
					case "ABC0":
						row.Cells.Add(PatCur.CreditType);
						break;
					case "Billing Type":
						row.Cells.Add(DefC.GetName(DefCat.BillingTypes,PatCur.BillingType));
						break;
					case "Referred From":
						RefAttach[] RefAttachList=RefAttaches.Refresh(PatCur.PatNum);
						string referral="";
						for(int i=0;i<RefAttachList.Length;i++) {
							if(RefAttachList[i].IsFrom) {
								referral=Referrals.GetNameLF(RefAttachList[i].ReferralNum);
								break;
							}
						}
						if(referral=="") {
							referral="??";
						}
						row.Cells.Add(referral);
						row.Tag=null;
						break;
					case "Date First Visit":
						if(PatCur.DateFirstVisit.Year<1880) {
							row.Cells.Add("??");
						}
						else if(PatCur.DateFirstVisit==DateTime.Today) {
							row.Cells.Add(Lan.g("TableChartPtInfo","NEW PAT"));
						}
						else {
							row.Cells.Add(PatCur.DateFirstVisit.ToShortDateString());
						}
						row.Tag=null;
						break;
					case "Prov. (Pri, Sec)":
						if(PatCur.SecProv != 0) {
							row.Cells.Add(Providers.GetAbbr(PatCur.PriProv) + ", " + Providers.GetAbbr(PatCur.SecProv));
						}
						else {
							row.Cells.Add(Providers.GetAbbr(PatCur.PriProv) + ", " + Lan.g("TableChartPtInfo","None"));
						}
						row.Tag = null;
						break;
					case "Pri Ins":
						string name;
						if(PatPlanList.Count>0) {
							name=InsPlans.GetCarrierName(PatPlans.GetPlanNum(PatPlanList,1),PlanList);
							if(PatPlanList[0].IsPending) {
								name+=Lan.g("TableChartPtInfo"," (pending)");
							}
							row.Cells.Add(name);
						}
						else {
							row.Cells.Add("");
						}
						row.Tag=null;
						break;
					case "Sec Ins":
						if(PatPlanList.Count>1) {
							name=InsPlans.GetCarrierName(PatPlans.GetPlanNum(PatPlanList,2),PlanList);
							if(PatPlanList[1].IsPending) {
								name+=Lan.g("TableChartPtInfo"," (pending)");
							}
							row.Cells.Add(name);
						}
						else {
							row.Cells.Add("");
						}
						row.Tag=null;
						break;
					case "Registration Keys":
						//Not even available to most users.
						RegistrationKey[] keys=RegistrationKeys.GetForPatient(PatCur.PatNum);
						for(int i=0;i<keys.Length;i++) {
							row=new ODGridRow();
							row.Cells.Add(Lan.g("TableChartPtInfo","Registration Key"));
							string str=keys[i].RegKey.Substring(0,4)+"-"+keys[i].RegKey.Substring(4,4)+"-"
								+keys[i].RegKey.Substring(8,4)+"-"+keys[i].RegKey.Substring(12,4);
							if(keys[i].IsForeign) {
								str+="\r\nForeign";
							}
							else {
								str+="\r\nUSA";
							}
							str+="\r\nStarted: "+keys[i].DateStarted.ToShortDateString();
							if(keys[i].DateDisabled.Year>1880) {
								str+="\r\nDisabled: "+keys[i].DateDisabled.ToShortDateString();
							}
							if(keys[i].DateEnded.Year>1880) {
								str+="\r\nEnded: "+keys[i].DateEnded.ToShortDateString();
							}
							if(keys[i].Note!="") {
								str+=keys[i].Note;
							}
							row.Cells.Add(str);
							row.Tag=keys[i].Copy();
							gridPtInfo.Rows.Add(row);
						}
						break;
					case "Premedicate":
						if(PatCur.Premed) {
							row=new ODGridRow();
							row.Cells.Add("");
							cell=new ODGridCell();
							if(fields[f].Description=="") {
								cell.Text=fields[f].InternalName;
							}
							else {
								cell.Text=fields[f].Description;
							}
							cell.ColorText=Color.Red;
							cell.Bold=YN.Yes;
							row.Cells.Add(cell);
							row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
							row.Tag="med";
							gridPtInfo.Rows.Add(row);
						}
						break;
					case "Diseases":
						Disease[] DiseaseList=Diseases.Refresh(PatCur.PatNum);
						row=new ODGridRow();
						cell=new ODGridCell();
						if(fields[f].Description=="") {
							cell.Text=fields[f].InternalName;
						}
						else {
							cell.Text=fields[f].Description;
						}
						cell.Bold=YN.Yes;
						row.Cells.Add(cell);
						row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
						row.Tag="med";
						if(DiseaseList.Length>0) {
							row.Cells.Add("");
							gridPtInfo.Rows.Add(row);
						}
						else {
							row.Cells.Add(Lan.g("TableChartPtInfo","none"));
						}
						//Add a new row for each med.
						for(int i=0;i<DiseaseList.Length;i++) {
							row=new ODGridRow();
							cell=new ODGridCell(DiseaseDefs.GetName(DiseaseList[i].DiseaseDefNum));
							cell.ColorText=Color.Red;
							cell.Bold=YN.Yes;
							row.Cells.Add(cell);
							row.Cells.Add(DiseaseList[i].PatNote);
							row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
							row.Tag="med";
							if(i!=DiseaseList.Length-1) {
								gridPtInfo.Rows.Add(row);
							}
						}
						break;
					case "Med Urgent":
						cell=new ODGridCell();
						cell.Text=PatCur.MedUrgNote;
						cell.ColorText=Color.Red;
						cell.Bold=YN.Yes;
						row.Cells.Add(cell);
						row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
						row.Tag="med";
						break;
					case "Medical Summary":
						row.Cells.Add(PatientNoteCur.Medical);
						row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
						row.Tag="med";
						break;
					case "Service Notes":
						row.Cells.Add(PatientNoteCur.Service);
						row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
						row.Tag="med";
						break;
					case "Medications":
						Medications.Refresh();
						MedicationPats.Refresh(PatCur.PatNum);
						row=new ODGridRow();
						cell=new ODGridCell();
						if(fields[f].Description=="") {
							cell.Text=fields[f].InternalName;
						}
						else {
							cell.Text=fields[f].Description;
						}
						cell.Bold=YN.Yes;
						row.Cells.Add(cell);
						row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
						row.Tag="med";
						if(MedicationPats.List.Length>0) {
							row.Cells.Add("");
							gridPtInfo.Rows.Add(row);
						}
						else {
							row.Cells.Add(Lan.g("TableChartPtInfo","none"));
						}
						string text;
						Medication med;
						for(int i=0;i<MedicationPats.List.Length;i++) {
							row=new ODGridRow();
							med=Medications.GetMedication(MedicationPats.List[i].MedicationNum);
							text=med.MedName;
							if(med.MedicationNum != med.GenericNum) {
								text+="("+Medications.GetMedication(med.GenericNum).MedName+")";
							}
							row.Cells.Add(text);
							text=MedicationPats.List[i].PatNote
								+"("+Medications.GetGeneric(MedicationPats.List[i].MedicationNum).Notes+")";
							row.Cells.Add(text);
							row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][3].ItemColor;
							row.Tag="med";
							if(i!=MedicationPats.List.Length-1) {
								gridPtInfo.Rows.Add(row);
							}
						}
						break;
					case "PatFields":
						PatField field;
						for(int i=0;i<PatFieldDefs.List.Length;i++) {
							row=new ODGridRow();
							row.Cells.Add(PatFieldDefs.List[i].FieldName);
							field=PatFields.GetByName(PatFieldDefs.List[i].FieldName,PatFieldList);
							if(field==null) {
								row.Cells.Add("");
							}
							else {
								row.Cells.Add(field.FieldValue);
							}
							row.Tag="PatField"+i.ToString();
							gridPtInfo.Rows.Add(row);
						}
						break;
				}
				if(fields[f].InternalName=="PatFields"
					|| fields[f].InternalName=="Premedicate"
					|| fields[f].InternalName=="Registration Keys") {
					//For fields that might have zero rows, we can't add the row here.  Adding rows is instead done in the case clause.
					//But some fields that are based on lists will always have one row, even if there are no items in the list.
					//Do not add those kinds here.
				}
				else {
					gridPtInfo.Rows.Add(row);
				}
			}
			gridPtInfo.EndUpdate();
		}

		private void textTreatmentNotes_TextChanged(object sender, System.EventArgs e) {
			TreatmentNoteChanged=true;
		}

		private void textTreatmentNotes_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(FamCur==null)
				return;
			if(TreatmentNoteChanged){
				PatientNoteCur.Treatment=textTreatmentNotes.Text;
				PatientNotes.Update(PatientNoteCur,PatCur.Guarantor);
				TreatmentNoteChanged=false;
			}
		}

		///<summary>The supplied procedure row must include these columns: ProcDate,ProcStatus,ProcCode,Surf,ToothNum, and ToothRange, all in raw database format.</summary>
		private bool ShouldDisplayProc(DataRow row){
			//if printing for hospital
			if(hospitalDate.Year > 1880) {
				if(hospitalDate.Date != PIn.DateT(row["ProcDate"].ToString()).Date) {
					return false;
				}
				if(row["ProcStatus"].ToString() != ((int)ProcStat.C).ToString()) {
					return false;
				}
			}
			if(checkShowTeeth.Checked) {
				bool showProc = false;
				//ArrayList selectedTeeth = new ArrayList();//integers 1-32
				//for(int i = 0;i < toothChart.SelectedTeeth.Count;i++) {
				//	selectedTeeth.Add(Tooth.ToInt(toothChart.SelectedTeeth[i]));
				//}
				switch(ProcedureCodes.GetProcCode(row["ProcCode"].ToString()).TreatArea) {
					case TreatmentArea.Arch:
						for(int s=0;s<toothChart.SelectedTeeth.Count;s++) {
							if(row["Surf"].ToString() == "U" && Tooth.IsMaxillary(toothChart.SelectedTeeth[s]) ) {
								showProc = true;
							}
							else if(row["Surf"].ToString() == "L" && !Tooth.IsMaxillary(toothChart.SelectedTeeth[s])) {
								showProc = true;
							}
						}
						break;
					case TreatmentArea.Mouth:
					case TreatmentArea.None:
					case TreatmentArea.Sextant://nobody will miss it
						showProc = false;
						break;
					case TreatmentArea.Quad:
						for(int s = 0;s < toothChart.SelectedTeeth.Count;s++) {
							if(row["Surf"].ToString()=="UR" && Tooth.ToInt(toothChart.SelectedTeeth[s]) >= 1 && Tooth.ToInt(toothChart.SelectedTeeth[s]) <= 8) {
								showProc = true;
							}
							else if(row["Surf"].ToString()=="UL" && Tooth.ToInt(toothChart.SelectedTeeth[s]) >= 9 && Tooth.ToInt(toothChart.SelectedTeeth[s]) <= 16) {
								showProc = true;
							}
							else if(row["Surf"].ToString()=="LL" && Tooth.ToInt(toothChart.SelectedTeeth[s]) >= 17 && Tooth.ToInt(toothChart.SelectedTeeth[s]) <= 24) {
								showProc = true;
							}
							else if(row["Surf"].ToString()=="LR" && Tooth.ToInt(toothChart.SelectedTeeth[s]) >= 25 && Tooth.ToInt(toothChart.SelectedTeeth[s]) <= 32) {
								showProc = true;
							}
						}
						break;
					case TreatmentArea.Surf:
					case TreatmentArea.Tooth:
						for(int s=0;s<toothChart.SelectedTeeth.Count;s++) {
							if(row["ToothNum"].ToString()==toothChart.SelectedTeeth[s]) {
								showProc = true;
							}
						}
						break;
					case TreatmentArea.ToothRange:
						string[] range = row["ToothRange"].ToString().Split(',');
						for(int s = 0;s <toothChart.SelectedTeeth.Count;s++) {
							for(int r = 0;r < range.Length;r++) {
								if(range[r] == toothChart.SelectedTeeth[s]) {
									showProc = true;
								}
							}
						}
						break;
				}
				if(!showProc) {
					return false;
				}
			}
			return ProcStatDesired((ProcStat)PIn.Long(row["ProcStatus"].ToString()));
			// Put check for showing hygine in here
			// Put check for showing films in here
			// return false;
		}

		/// <summary> Checks ProcStat passed to see if one of the check boxes on the forms contains a +ve check for the ps passed. For example if ps is TP and the checkShowTP.Checked is true it will return true.</summary>
		private bool ProcStatDesired(ProcStat ps) {
			switch(ps) {
				case ProcStat.TP:
					if(checkShowTP.Checked) {
						return true;
					}
					break;
				case ProcStat.C:
					if(checkShowC.Checked) {
						return true;
					}
					break;
				case ProcStat.EC:
					if(checkShowE.Checked) {
						return true;
					}
					break;
				case ProcStat.EO:
					if(checkShowE.Checked) {
						return true;
					}
					break;
				case ProcStat.R:
					if(checkShowR.Checked) {
						return true;
					}
					break;
				case ProcStat.D:
					if(checkAudit.Checked) {
						return true;
					}
					break;
				case ProcStat.Cn:
					if(checkShowCn.Checked) {
						return true;
					}
					break;
			}
			return false;
		}

		private void FillProgNotes(){
			FillProgNotes(false);
		}

		private void FillProgNotes(bool retainSelection){
			Plugins.HookAddCode(this,"ContrChart.FillProgNotes_begin");
			//ArrayList selectedTeeth=new ArrayList();//integers 1-32
			//for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
			//	selectedTeeth.Add(Tooth.ToInt(toothChart.SelectedTeeth[i]));
			//}
			//List<string> selectedTeeth=new List<string>(toothChart.SelectedTeeth);
			DataSetMain=null;
			if(PatCur!=null){
				DataSetMain=ChartModules.GetAll(PatCur.PatNum,checkAudit.Checked);
			}
			gridProg.BeginUpdate();
			gridProg.Columns.Clear();
			ODGridColumn col;
			List<DisplayField> fields;
			DisplayFields.RefreshCache();
			if(gridChartViews.Rows.Count==0) {
				fields=DisplayFields.GetForCategory(DisplayFieldCategory.None);
				gridProg.Title="Progress Notes";
				if(!chartCustViewChanged) {
					checkSheets.Checked=true;
					checkTasks.Checked=true;
					checkEmail.Checked=true;
					checkCommFamily.Checked=true;
					checkAppt.Checked=true;
					checkLabCase.Checked=true;
					checkRx.Checked=true;
					checkComm.Checked=true;
					checkShowTP.Checked=true;
					checkShowC.Checked=true;
					checkShowE.Checked=true;
					checkShowR.Checked=true;
					checkShowCn.Checked=true;
					checkNotes.Checked=true;
				}
			}
			else {
				if(ChartViewCurDisplay==null) {
					ChartViewCurDisplay=ChartViews.Listt[0];
				}
				fields=DisplayFields.GetForChartView(ChartViewCurDisplay.ChartViewNum);
				gridProg.Title=ChartViewCurDisplay.Description;
				gridChartViews.SetSelected(ChartViewCurDisplay.ItemOrder,true);
				if(!chartCustViewChanged) {
					checkSheets.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.Sheets)==ChartViewObjs.Sheets;
					checkTasks.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.Tasks)==ChartViewObjs.Tasks;
					checkEmail.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.Email)==ChartViewObjs.Email;
					checkCommFamily.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.CommLogFamily)==ChartViewObjs.CommLogFamily;
					checkAppt.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.Appointments)==ChartViewObjs.Appointments;
					checkLabCase.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.LabCases)==ChartViewObjs.LabCases;
					checkRx.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.Rx)==ChartViewObjs.Rx;
					checkComm.Checked=(ChartViewCurDisplay.ObjectTypes&ChartViewObjs.CommLog)==ChartViewObjs.CommLog;
					checkShowTP.Checked=(ChartViewCurDisplay.ProcStatuses&ChartViewProcStat.TP)==ChartViewProcStat.TP;
					checkShowC.Checked=(ChartViewCurDisplay.ProcStatuses&ChartViewProcStat.C)==ChartViewProcStat.C;
					checkShowE.Checked=(ChartViewCurDisplay.ProcStatuses&ChartViewProcStat.EC)==ChartViewProcStat.EC;
					checkShowR.Checked=(ChartViewCurDisplay.ProcStatuses&ChartViewProcStat.R)==ChartViewProcStat.R;
					checkShowCn.Checked=(ChartViewCurDisplay.ProcStatuses&ChartViewProcStat.Cn)==ChartViewProcStat.Cn;
					checkShowTeeth.Checked=ChartViewCurDisplay.SelectedTeethOnly;
					checkNotes.Checked=ChartViewCurDisplay.ShowProcNotes;
					checkAudit.Checked=ChartViewCurDisplay.IsAudit;
				}
			}
			for(int i=0;i<fields.Count;i++){
				if(fields[i].Description==""){
					col=new ODGridColumn(fields[i].InternalName,fields[i].ColumnWidth);
				}
				else{
					col=new ODGridColumn(fields[i].Description,fields[i].ColumnWidth);
				}
				if(fields[i].InternalName=="Amount"){
					col.TextAlign=HorizontalAlignment.Right;
				}
				if(fields[i].InternalName=="ADA Code"
					|| fields[i].InternalName=="User"
					|| fields[i].InternalName=="Signed")
				{
					col.TextAlign=HorizontalAlignment.Center;
				}
				gridProg.Columns.Add(col);
			}
			if(gridProg.Columns.Count<3){//0 wouldn't be possible.
				gridProg.NoteSpanStart=0;
				gridProg.NoteSpanStop=gridProg.Columns.Count-1;
			}
			else{
				gridProg.NoteSpanStart=2;
				if(gridProg.Columns.Count>7) {
					gridProg.NoteSpanStop=7;
				}
				else{
					gridProg.NoteSpanStop=gridProg.Columns.Count-1;
				}
			}
			gridProg.Rows.Clear();
			ODGridRow row;
			//Type type;
			if(DataSetMain==null) {
				gridProg.EndUpdate();
				FillToothChart(false);//?
				return;
			}
			DataTable table=DataSetMain.Tables["ProgNotes"];
			ProcList=new List<DataRow>();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["ProcNum"].ToString()!="0"){//if this is a procedure
					if(ShouldDisplayProc(table.Rows[i])){
						ProcList.Add(table.Rows[i]);//show it in the graphical tooth chart
						//and add it to the grid below.
					}
					else{
						continue;
					}
				}
				else if(table.Rows[i]["CommlogNum"].ToString()!="0"){//if this is a commlog
					if(!checkComm.Checked) {
						continue;
					}
					if(table.Rows[i]["PatNum"].ToString()!=PatCur.PatNum.ToString()){//if this is a different family member
						if(!checkCommFamily.Checked) {
							continue;
						}
					}
				}
				else if(table.Rows[i]["RxNum"].ToString()!="0") {//if this is an Rx
					if(!checkRx.Checked){
						continue;
					}
				}
				else if(table.Rows[i]["LabCaseNum"].ToString()!="0") {//if this is a LabCase
					if(!checkLabCase.Checked) {
						continue;
					}
				}
				else if(table.Rows[i]["TaskNum"].ToString()!="0") {//if this is a TaskItem
					if(!checkTasks.Checked) {
						continue;
					}
					if(table.Rows[i]["PatNum"].ToString()!=PatCur.PatNum.ToString()){//if this is a different family member
						if(!checkCommFamily.Checked) { //uses same check box as commlog
							continue;
						}
					}
				}
				else if(table.Rows[i]["EmailMessageNum"].ToString()!="0") {//if this is an Email
					if(!checkEmail.Checked) {
						continue;
					}
				}
				else if(table.Rows[i]["AptNum"].ToString()!="0") {//if this is an Appointment
					if(!checkAppt.Checked) {
						continue;
					}
				}
				else if(table.Rows[i]["SheetNum"].ToString()!="0") {//if this is a sheet
					if(!checkSheets.Checked) {
						continue;
					}
				}
				row=new ODGridRow();
				row.ColorLborder=Color.Black;
				//remember that columns that start with lowercase are already altered for display rather than being raw data.
				for(int f=0;f<fields.Count;f++) {
					switch(fields[f].InternalName){
						case "Date":
							row.Cells.Add(table.Rows[i]["procDate"].ToString());
							break;
						case "Time":
							row.Cells.Add(table.Rows[i]["procTime"].ToString());
							break;
						case "Th":
							row.Cells.Add(table.Rows[i]["toothNum"].ToString());
							break;
						case "Surf":
							row.Cells.Add(table.Rows[i]["surf"].ToString());
							break;
						case "Dx":
							row.Cells.Add(table.Rows[i]["dx"].ToString());
							break;
						case "Description":
							row.Cells.Add(table.Rows[i]["description"].ToString());
							break;
						case "Stat":
							row.Cells.Add(table.Rows[i]["procStatus"].ToString());
							break;
						case "Prov":
							row.Cells.Add(table.Rows[i]["prov"].ToString());
							break;
						case "Amount":
							row.Cells.Add(table.Rows[i]["procFee"].ToString());
							break;
						case "ADA Code":
							row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
							break;
						case "User":
							row.Cells.Add(table.Rows[i]["user"].ToString());
							break;
						case "Signed":
							row.Cells.Add(table.Rows[i]["signature"].ToString());
							break;
						case "Priority":
							row.Cells.Add(table.Rows[i]["priority"].ToString());
							break;
					}
				}				
				if(checkNotes.Checked){
					row.Note=table.Rows[i]["note"].ToString();
				}
				row.ColorText=Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				row.ColorBackG=Color.FromArgb(PIn.Int(table.Rows[i]["colorBackG"].ToString()));
				row.Tag=table.Rows[i];
				gridProg.Rows.Add(row);
			}
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				gridProg.Width=524;
			}
			else if(gridProg.Columns !=null && gridProg.Columns.Count>0) {
				int gridW=0;
				for(int i=0;i<gridProg.Columns.Count;i++) {
					gridW+=gridProg.Columns[i].ColWidth;
				}
				if(gridProg.Location.X+gridW+20 < ClientSize.Width) {//if space is big enough to allow full width
					gridProg.Width=gridW+20;
				}
				else {
					gridProg.Width=ClientSize.Width-gridProg.Location.X-1;
				}
			}
			gridProg.EndUpdate();
			if(Chartscrollval==0) {
				gridProg.ScrollToEnd();
			}
			else {
				gridProg.ScrollValue=Chartscrollval;
				Chartscrollval=0;
			}
			FillToothChart(retainSelection);
		}

		private void FillChartViewsGrid() {
			if(PatCur==null) {
				butChartViewAdd.Enabled=false;
				butChartViewDown.Enabled=false;
				butChartViewUp.Enabled=false;
				gridChartViews.Enabled=false;
				return;
			}
			else {
				butChartViewAdd.Enabled=true;
				butChartViewDown.Enabled=true;
				butChartViewUp.Enabled=true;
				gridChartViews.Enabled=true;
			}
			ChartViews.RefreshCache();
			gridChartViews.BeginUpdate();
			gridChartViews.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("GridChartViews","F#"),25);
			gridChartViews.Columns.Add(col);
			col=new ODGridColumn(Lan.g("GridChartViews","View"),0);
			gridChartViews.Columns.Add(col);
			gridChartViews.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ChartViews.Listt.Count;i++) {
				row=new ODGridRow();
				//assign hot keys F1-F12
				if(i<11) {
					row.Cells.Add("F"+(i+1));
				}
				row.Cells.Add(ChartViews.Listt[i].Description);
				gridChartViews.Rows.Add(row);
			}
			gridChartViews.EndUpdate();
		}

		private void SetChartView(ChartView chartView) {
			ChartViewCurDisplay=chartView;
			labelCustView.Visible=false;
			chartCustViewChanged=false;
			FillProgNotes();
		}

		///<summary>This is, of course, called when module refreshed.  But it's also called when user sets missing teeth or tooth movements.  In that case, the Progress notes are not refreshed, so it's a little faster.  This also fills in the movement amounts.</summary>
		private void FillToothChart(bool retainSelection){
			Cursor=Cursors.WaitCursor;
			toothChart.SuspendLayout();
			toothChart.ColorBackground=DefC.Long[(int)DefCat.ChartGraphicColors][10].ItemColor;
			toothChart.ColorText=DefC.Long[(int)DefCat.ChartGraphicColors][11].ItemColor;
			toothChart.ColorTextHighlight=DefC.Long[(int)DefCat.ChartGraphicColors][12].ItemColor;
			toothChart.ColorBackHighlight=DefC.Long[(int)DefCat.ChartGraphicColors][13].ItemColor;
			//remember which teeth were selected
			//ArrayList selectedTeeth=new ArrayList();//integers 1-32
			//for(int i=0;i<toothChart.SelectedTeeth.Length;i++) {
			//	selectedTeeth.Add(Tooth.ToInt(toothChart.SelectedTeeth[i]));
			//}
			List<string> selectedTeeth=new List<string>(toothChart.SelectedTeeth);
			toothChart.ResetTeeth();
			if(PatCur==null) {
				toothChart.ResumeLayout();
				FillMovementsAndHidden();
				Cursor=Cursors.Default;
				return;
			}
			//primary teeth need to be set before resetting selected teeth, because some of them might be primary.
			//primary teeth also need to be set before initial list so that we can set a primary tooth missing.
			for(int i=0;i<ToothInitialList.Count;i++) {
				if(ToothInitialList[i].InitialType==ToothInitialType.Primary) {
					toothChart.SetPrimary(ToothInitialList[i].ToothNum);
				}
			}
			if(checkShowTeeth.Checked || retainSelection) {
				for(int i=0;i<selectedTeeth.Count;i++) {
					toothChart.SetSelected(selectedTeeth[i],true);
				}
			}
			for(int i=0;i<ToothInitialList.Count;i++){
				switch(ToothInitialList[i].InitialType){
					case ToothInitialType.Missing:
						toothChart.SetMissing(ToothInitialList[i].ToothNum);
						break;
					case ToothInitialType.Hidden:
						toothChart.SetHidden(ToothInitialList[i].ToothNum);
						break;
					//case ToothInitialType.Primary:
					//	break;
					case ToothInitialType.Rotate:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,ToothInitialList[i].Movement,0,0,0,0,0);
						break;
					case ToothInitialType.TipM:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,ToothInitialList[i].Movement,0,0,0,0);
						break;
					case ToothInitialType.TipB:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,ToothInitialList[i].Movement,0,0,0);
						break;
					case ToothInitialType.ShiftM:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,ToothInitialList[i].Movement,0,0);
						break;
					case ToothInitialType.ShiftO:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,0,ToothInitialList[i].Movement,0);
						break;
					case ToothInitialType.ShiftB:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,0,0,ToothInitialList[i].Movement);
						break;
					case ToothInitialType.Drawing:
						toothChart.AddDrawingSegment(ToothInitialList[i].Copy());
						break;
				}
			}
			DrawProcGraphics();
			toothChart.ResumeLayout();
			FillMovementsAndHidden();
			Cursor=Cursors.Default;
		}

		private void DrawProcGraphics(){
			//this requires: ProcStatus, ProcCode, ToothNum, HideGraphics, Surf, and ToothRange.  All need to be raw database values.
			string[] teeth;
			Color cLight=Color.White;
			Color cDark=Color.White;
			for(int i=0;i<ProcList.Count;i++) {
				if(ProcList[i]["HideGraphics"].ToString()=="1") {
					continue;
				}
				if(ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).PaintType==ToothPaintingType.Extraction && (
					PIn.Long(ProcList[i]["ProcStatus"].ToString())==(int)ProcStat.C
					|| PIn.Long(ProcList[i]["ProcStatus"].ToString())==(int)ProcStat.EC
					|| PIn.Long(ProcList[i]["ProcStatus"].ToString())==(int)ProcStat.EO
					)) {
					continue;//prevents the red X. Missing teeth already handled.
				}
				if(ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).GraphicColor==Color.FromArgb(0)){
					switch((ProcStat)PIn.Long(ProcList[i]["ProcStatus"].ToString())) {
						case ProcStat.C:
							cDark=DefC.Short[(int)DefCat.ChartGraphicColors][1].ItemColor;
							cLight=DefC.Short[(int)DefCat.ChartGraphicColors][6].ItemColor;
							break;
						case ProcStat.TP:
							cDark=DefC.Short[(int)DefCat.ChartGraphicColors][0].ItemColor;
							cLight=DefC.Short[(int)DefCat.ChartGraphicColors][5].ItemColor;
							break;
						case ProcStat.EC:
							cDark=DefC.Short[(int)DefCat.ChartGraphicColors][2].ItemColor;
							cLight=DefC.Short[(int)DefCat.ChartGraphicColors][7].ItemColor;
							break;
						case ProcStat.EO:
							cDark=DefC.Short[(int)DefCat.ChartGraphicColors][3].ItemColor;
							cLight=DefC.Short[(int)DefCat.ChartGraphicColors][8].ItemColor;
							break;
						case ProcStat.R:
							cDark=DefC.Short[(int)DefCat.ChartGraphicColors][4].ItemColor;
							cLight=DefC.Short[(int)DefCat.ChartGraphicColors][9].ItemColor;
							break;
						case ProcStat.Cn:
							cDark=DefC.Short[(int)DefCat.ChartGraphicColors][16].ItemColor;
							cLight=DefC.Short[(int)DefCat.ChartGraphicColors][17].ItemColor;
							break;
					}
				}
				else{
					cDark=ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).GraphicColor;
					cLight=ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).GraphicColor;
				}
				switch(ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).PaintType){
					case ToothPaintingType.BridgeDark:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,ProcList[i]["ToothNum"].ToString())){
							toothChart.SetPontic(ProcList[i]["ToothNum"].ToString(),cDark);
						}
						else{
							toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cDark);
						}
						break;
					case ToothPaintingType.BridgeLight:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,ProcList[i]["ToothNum"].ToString())) {
							toothChart.SetPontic(ProcList[i]["ToothNum"].ToString(),cLight);
						}
						else {
							toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cLight);
						}
						break;
					case ToothPaintingType.CrownDark:
						toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.CrownLight:
						toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cLight);
						break;
					case ToothPaintingType.DentureDark:
						if(ProcList[i]["Surf"].ToString()=="U"){
							teeth=new string[14];
							for(int t=0;t<14;t++){
								teeth[t]=(t+2).ToString();
							}
						}
						else if(ProcList[i]["Surf"].ToString()=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else{
							teeth=ProcList[i]["ToothRange"].ToString().Split(new char[] {','});
						}
						for(int t=0;t<teeth.Length;t++){
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cDark);
							}
							else {
								toothChart.SetCrown(teeth[t],cDark);
							}
						}
						break;
					case ToothPaintingType.DentureLight:
						if(ProcList[i]["Surf"].ToString()=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(ProcList[i]["Surf"].ToString()=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=ProcList[i]["ToothRange"].ToString().Split(new char[] { ',' });
						}
						for(int t=0;t<teeth.Length;t++) {
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cLight);
							}
							else {
								toothChart.SetCrown(teeth[t],cLight);
							}
						}
						break;
					case ToothPaintingType.Extraction:
						toothChart.SetBigX(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.FillingDark:
						toothChart.SetSurfaceColors(ProcList[i]["ToothNum"].ToString(),ProcList[i]["Surf"].ToString(),cDark);
						break;
				  case ToothPaintingType.FillingLight:
						toothChart.SetSurfaceColors(ProcList[i]["ToothNum"].ToString(),ProcList[i]["Surf"].ToString(),cLight);
						break;
					case ToothPaintingType.Implant:
						toothChart.SetImplant(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.PostBU:
						toothChart.SetBU(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.RCT:
						toothChart.SetRCT(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.Sealant:
						toothChart.SetSealant(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.Veneer:
						toothChart.SetVeneer(ProcList[i]["ToothNum"].ToString(),cLight);
						break;
					case ToothPaintingType.Watch:
						toothChart.SetWatch(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
				}
			}
		}

		private void checkToday_CheckedChanged(object sender, System.EventArgs e) {
			if(checkToday.Checked){
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			else{
				//
			}
		}

		///<summary>Gets run with each ModuleSelected.  Fills Dx, Priorities, ProcButtons, Date, and Image categories</summary>
    private void FillDxProcImage(){
			//if(textDate.errorProvider1.GetError(textDate)==""){
			if(checkToday.Checked){//textDate.Text=="" || 
				textDate.Text=DateTime.Today.ToShortDateString();
			}
			//}
			listDx.Items.Clear();
			for(int i=0;i<DefC.Short[(int)DefCat.Diagnosis].Length;i++){//move to instantClasses?
				this.listDx.Items.Add(DefC.Short[(int)DefCat.Diagnosis][i].ItemName);
			}
			int selectedPriority=comboPriority.SelectedIndex;//retain current selection
			comboPriority.Items.Clear();
			comboPriority.Items.Add(Lan.g(this,"no priority"));
			for(int i=0;i<DefC.Short[(int)DefCat.TxPriorities].Length;i++){
				this.comboPriority.Items.Add(DefC.Short[(int)DefCat.TxPriorities][i].ItemName);
			}
			if(selectedPriority>0 && selectedPriority<comboPriority.Items.Count)
				//set the selected to what it was before.
				comboPriority.SelectedIndex=selectedPriority;
			else
				comboPriority.SelectedIndex=0;
				//or just set to no priority
			int selectedButtonCat=listButtonCats.SelectedIndex;
			listButtonCats.Items.Clear();
			listButtonCats.Items.Add(Lan.g(this,"Quick Buttons"));
			for(int i=0;i<DefC.Short[(int)DefCat.ProcButtonCats].Length;i++){
				listButtonCats.Items.Add(DefC.Short[(int)DefCat.ProcButtonCats][i].ItemName);
			}
			if(selectedButtonCat < listButtonCats.Items.Count){
				listButtonCats.SelectedIndex=selectedButtonCat;
			}
			if(listButtonCats.SelectedIndex==-1	&& listButtonCats.Items.Count>0){
				listButtonCats.SelectedIndex=0;
			}
			FillProcButtons();
			int selectedImageTab=tabControlImages.SelectedIndex;//retains current selection
			tabControlImages.TabPages.Clear();
			TabPage page;
			page=new TabPage();
			page.Text=Lan.g(this,"All");
			tabControlImages.TabPages.Add(page);
			visImageCats=new ArrayList();
			for(int i=0;i<DefC.Short[(int)DefCat.ImageCats].Length;i++){
				if(DefC.Short[(int)DefCat.ImageCats][i].ItemValue=="X" || DefC.Short[(int)DefCat.ImageCats][i].ItemValue=="XP"){
					//if tagged to show in Chart
					visImageCats.Add(i);
					page=new TabPage();
					page.Text=DefC.Short[(int)DefCat.ImageCats][i].ItemName;
					tabControlImages.TabPages.Add(page);
				}
			}
			if(selectedImageTab<tabControlImages.TabCount){
				tabControlImages.SelectedIndex=selectedImageTab;
			}
			panelTPdark.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][0].ItemColor;
			panelCdark.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][1].ItemColor;
			panelECdark.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][2].ItemColor;
			panelEOdark.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][3].ItemColor;
			panelRdark.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][4].ItemColor;
			panelTPlight.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][5].ItemColor;
			panelClight.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][6].ItemColor;
			panelEClight.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][7].ItemColor;
			panelEOlight.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][8].ItemColor;
			panelRlight.BackColor=DefC.Long[(int)DefCat.ChartGraphicColors][9].ItemColor;
    }

		private void FillProcButtons(){
			listViewButtons.Items.Clear();
			imageListProcButtons.Images.Clear();
			panelQuickButtons.Visible = false;
			if(listButtonCats.SelectedIndex==-1){
				ProcButtonList=new ProcButton[0];
				return;
			}
			if(listButtonCats.SelectedIndex==0){
				panelQuickButtons.Visible = true;
				panelQuickButtons.Location=listViewButtons.Location;
				panelQuickButtons.Size=listViewButtons.Size;
				ProcButtonList=new ProcButton[0];
				return;
			}
			ProcButtons.RefreshCache();
			ProcButtonList=ProcButtons.GetForCat(DefC.Short[(int)DefCat.ProcButtonCats][listButtonCats.SelectedIndex-1].DefNum);
			ListViewItem item;
			for(int i=0;i<ProcButtonList.Length;i++){
				if(ProcButtonList[i].ButtonImage!=null) {
					//image keys are simply the ProcButtonNum
					imageListProcButtons.Images.Add(ProcButtonList[i].ProcButtonNum.ToString(),ProcButtonList[i].ButtonImage);
				}
				item=new ListViewItem(new string[] {ProcButtonList[i].Description},ProcButtonList[i].ProcButtonNum.ToString());
				listViewButtons.Items.Add(item);
			}
    }

		///<summary>Gets run on ModuleSelected and each time a different images tab is selected. It first creates any missing thumbnails, then displays them. So it will be faster after the first time.</summary>
		private void FillImages(){
			visImages=new ArrayList();
			listViewImages.Items.Clear();
			imageListThumbnails.Images.Clear();
			if(!PrefC.UsingAtoZfolder) {
				//Don't show any images if there is no document path.
				return;
			}
			if(PatCur==null){
				return;
			}
			if(patFolder==""){
				return;
			}
			if(!panelImages.Visible){
				return;
			}
			for(int i=0;i<DocumentList.Length;i++){
				if(!visImageCats.Contains(DefC.GetOrder(DefCat.ImageCats,DocumentList[i].DocCategory))){
					continue;//if category not visible, continue
				}
				if(tabControlImages.SelectedIndex>0){//any category except 'all'
					if(DocumentList[i].DocCategory!=DefC.Short[(int)DefCat.ImageCats]
						[(int)visImageCats[tabControlImages.SelectedIndex-1]].DefNum)
					{
						continue;//if not in category, continue
					}
				}
				//Documents.Cur=DocumentList[i];
				imageListThumbnails.Images.Add(Documents.GetThumbnail(DocumentList[i],patFolder,
					imageListThumbnails.ImageSize.Width));
				visImages.Add(i);
				ListViewItem item=new ListViewItem(DocumentList[i].DateCreated.ToShortDateString()+": "
					+DocumentList[i].Description,imageListThumbnails.Images.Count-1);
				//item.ToolTipText=patFolder+DocumentList[i].FileName;//not supported by Mono
        listViewImages.Items.Add(item);
			}//for
		}

		#region EnterTx
		private void ClearButtons() {
			//unfortunately, these colors no longer show since the XP button style was introduced.
			butM.BackColor=Color.FromName("Control"); ;
			butOI.BackColor=Color.FromName("Control");
			butD.BackColor=Color.FromName("Control");
			butL.BackColor=Color.FromName("Control");
			butBF.BackColor=Color.FromName("Control");
			butV.BackColor=Color.FromName("Control");
			textSurf.Text="";
			listDx.SelectedIndex=-1;
			//listProcButtons.SelectedIndex=-1;
			listViewButtons.SelectedIndices.Clear();
			textProcCode.Text=Lan.g(this,"Type Proc Code");
		}

		private void UpdateSurf (){
			textSurf.Text="";
			if(toothChart.SelectedTeeth.Count==0){
				return;
			}
			if(butM.BackColor==Color.White){
				textSurf.AppendText("M");
			}
			if(butOI.BackColor==Color.White){
				if(ToothGraphic.IsAnterior(toothChart.SelectedTeeth[0])){
					textSurf.AppendText("I");
				}
				else{	
					textSurf.AppendText("O");
				}
			}
			if(butD.BackColor==Color.White){
				textSurf.AppendText("D");
			}
			if(butV.BackColor==Color.White){
				if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {
					textSurf.AppendText("5");
				}
				else {
					textSurf.AppendText("V");
				}
			}
			if(butBF.BackColor==Color.White){
				if(ToothGraphic.IsAnterior(toothChart.SelectedTeeth[0])) {
					if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {
						textSurf.AppendText("V");//vestibular
					}
					else {
						textSurf.AppendText("F");
					}
				}
				else {
					textSurf.AppendText("B");
				}
			}
			if(butL.BackColor==Color.White){
				textSurf.AppendText("L");
			}
		}

		private void butBF_Click(object sender, System.EventArgs e){
			if(butBF.BackColor==Color.White){
				butBF.BackColor=SystemColors.Control;
			}
			else{
				butBF.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butV_Click(object sender, System.EventArgs e){
			if(butV.BackColor==Color.White){
				butV.BackColor=SystemColors.Control;
			}
			else{
				butV.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butM_Click(object sender, System.EventArgs e){
			if(butM.BackColor==Color.White){
				butM.BackColor=SystemColors.Control;
			}
			else{
				butM.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butOI_Click(object sender, System.EventArgs e){
			if(butOI.BackColor==Color.White){
				butOI.BackColor=SystemColors.Control;
			}
			else{
				butOI.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butD_Click(object sender, System.EventArgs e){
			if(butD.BackColor==Color.White){
				butD.BackColor=SystemColors.Control;
			}
			else{
				butD.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void butL_Click(object sender, System.EventArgs e){
			if(butL.BackColor==Color.White){
				butL.BackColor=SystemColors.Control;
			}
			else{
				butL.BackColor=Color.White;
			}
			UpdateSurf();
		}

		private void gridProg_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Chartscrollval=gridProg.ScrollValue;
			DataRow row=(DataRow)gridProg.Rows[e.Row].Tag;
			if(row["ProcNum"].ToString()!="0"){
				if(checkAudit.Checked){
					MsgBox.Show(this,"Not allowed to edit procedures when in audit mode.");
					return;
				}
				Procedure proc=Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),true);
				if(row["groupprocnum"].ToString()!="0"){
					//group
	
				}else{
					FormProcEdit FormP=new FormProcEdit(proc,PatCur,FamCur);
					FormP.ShowDialog();
					if(FormP.DialogResult!=DialogResult.OK) {
						return;
					}
				}
			}
			else if(row["CommlogNum"].ToString()!="0"){
				Commlog comm=Commlogs.GetOne(PIn.Long(row["CommlogNum"].ToString()));
				FormCommItem FormC=new FormCommItem(comm);
				FormC.ShowDialog();
				if(FormC.DialogResult!=DialogResult.OK){
					return;
				}
			}
			else if(row["RxNum"].ToString()!="0") {
				RxPat rx=RxPats.GetRx(PIn.Long(row["RxNum"].ToString()));
				FormRxEdit FormRxE=new FormRxEdit(PatCur,rx);
				FormRxE.ShowDialog();
				if(FormRxE.DialogResult!=DialogResult.OK){
					return;
				}
			}
			else if(row["LabCaseNum"].ToString()!="0") {
				LabCase lab=LabCases.GetOne(PIn.Long(row["LabCaseNum"].ToString()));
				FormLabCaseEdit FormL=new FormLabCaseEdit();
				FormL.CaseCur=lab;
				FormL.ShowDialog();
				if(FormL.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["TaskNum"].ToString()!="0") {
				Task curTask=Tasks.GetOne(PIn.Long(row["TaskNum"].ToString()));
				FormTaskEdit FormT=new FormTaskEdit(curTask);
				FormT.ShowDialog();
				if(FormT.GotoType!=TaskObjectType.None) {
					TaskObjectType GotoType=FormT.GotoType;
					long GotoKeyNum=FormT.GotoKeyNum;
					//OnGoToChanged();
					if(GotoType==TaskObjectType.Patient) {
						if(GotoKeyNum!=0) {
							Patient pat=Patients.GetPat(GotoKeyNum);
							OnPatientSelected(pat.PatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber);
							ModuleSelected(pat.PatNum);
							return;
						}
					}
					if(GotoType==TaskObjectType.Appointment) {
						/*There's nothing to do here, since we're not in the appt module.
							if(GotoKeyNum!=0) {
							Appointment apt=Appointments.GetOneApt(GotoKeyNum);
							//Patient pat=Patients.GetPat(apt.PatNum);
							if(apt==null) {
								MsgBox.Show(this,"Appointment has been deleted, so it's not available.");
								return;
							}
							DateTime dateSelected=DateTime.MinValue;
							if(apt.AptStatus==ApptStatus.Planned || apt.AptStatus==ApptStatus.UnschedList) {
								//I did not add feature to put planned or unsched apt on pinboard.
								MsgBox.Show(this,"Cannot navigate to appointment.  Use the Other Appointments button.");
								//return;
							}
							else {
								dateSelected=apt.AptDateTime;
							}
							PatCur.PatNum=apt.PatNum;//OnPatientSelected(apt.PatNum);
						}
						*/
						//DialogResult=DialogResult.OK;
						return;
					}
				}
				if(FormT.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["AptNum"].ToString()!="0") {
				//Appointment apt=Appointments.GetOneApt(
				FormApptEdit FormA=new FormApptEdit(PIn.Long(row["AptNum"].ToString()));
				//PinIsVisible=false
				FormA.ShowDialog();
				if(FormA.CloseOD) {
					((Form)this.Parent).Close();
					return;
				}
				if(FormA.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["EmailMessageNum"].ToString()!="0") {
				EmailMessage msg=EmailMessages.GetOne(PIn.Long(row["EmailMessageNum"].ToString()));
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(msg);
				FormE.ShowDialog();
				if(FormE.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["SheetNum"].ToString()!="0") {
				Sheet sheet=Sheets.GetSheet(PIn.Long(row["SheetNum"].ToString()));
				FormSheetFillEdit FormSFE=new FormSheetFillEdit(sheet);
				FormSFE.ShowDialog();
				if(FormSFE.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else if(row["FormPatNum"].ToString()!="0"){
				FormPat form=FormPats.GetOne(PIn.Long(row["FormPatNum"].ToString()));
				FormFormPatEdit FormP=new FormFormPatEdit();
				FormP.FormPatCur=form;
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.OK)
				{
					ModuleSelected(PatCur.PatNum);
				}
			}
			ModuleSelected(PatCur.PatNum);
			Reporting.Allocators.MyAllocator1_ProviderPayment.AllocateWithToolCheck(this.PatCur.Guarantor);
		}

		///<summary>Sets many fields for a new procedure, then displays it for editing before inserting it into the db.  No need to worry about ProcOld because it's an insert, not an update.</summary>
		private void AddProcedure(Procedure ProcCur){
			//procnum
			ProcCur.PatNum=PatCur.PatNum;
			//aptnum
			//proccode
			//ProcCur.CodeNum=ProcedureCodes.GetProcCode(ProcCur.OldCode).CodeNum;//already set
			if(newStatus==ProcStat.EO){
				ProcCur.ProcDate=DateTime.MinValue;
			}
			else if(textDate.errorProvider1.GetError(textDate)!=""){
				ProcCur.ProcDate=DateTime.Today;
			}
			else{
				ProcCur.ProcDate=PIn.Date(textDate.Text);
			}
			ProcCur.DateTP=ProcCur.ProcDate;
			if(newStatus==ProcStat.R || newStatus==ProcStat.EO || newStatus==ProcStat.EC) {
				ProcCur.ProcFee=0;
			}
			else {
				//int totUnits = ProcCur.BaseUnits + ProcCur.UnitQty;
				InsPlan priplan=null;
				if(PatPlanList.Count>0) {
					priplan=InsPlans.GetPlan(PatPlanList[0].PlanNum,PlanList);
				}
				//check to see if it is a med code
				double insfee;
				bool isMed = false;
				if(ProcCur.MedicalCode != null && ProcCur.MedicalCode != "") {
					isMed = true;
				}
				//get fee schedule for medical ins or dental
				long feeSch;
				if(isMed){
					feeSch = Fees.GetMedFeeSched(PatCur, PlanList, PatPlanList);
				} 
				else {
					feeSch = Fees.GetFeeSched(PatCur, PlanList, PatPlanList);
				}
				insfee = Fees.GetAmount0(ProcCur.CodeNum, feeSch);
				if(priplan!=null && priplan.PlanType=="p" && !isMed) {//PPO
					double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched);
					if(standardfee>insfee) {
						ProcCur.ProcFee=standardfee;
					}
					else {
						ProcCur.ProcFee=insfee;
					}
				}
				else {
					ProcCur.ProcFee=insfee;
				}
			}
			//ProcCur.OverridePri=-1;
			//ProcCur.OverrideSec=-1;
			//surf
			//ToothNum
			//Procedures.Cur.ToothRange
			//ProcCur.NoBillIns=ProcedureCodes.GetProcCode(ProcCur.ProcCode).NoBillIns;
			if(comboPriority.SelectedIndex==0) {
				ProcCur.Priority=0;
			}
			else {
				ProcCur.Priority=DefC.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			}
			ProcCur.ProcStatus=newStatus;
			long provPri=PatCur.PriProv;
			long provSec=PatCur.SecProv;
			for(int i=0;i<ApptList.Length;i++) {
				if(ApptList[i].AptDateTime.Date==DateTime.Today) {
					provPri=ApptList[i].ProvNum;
					provSec=ApptList[i].ProvHyg;
					break;
				}
			}
			if(ProcedureCodes.GetProcCode(ProcCur.CodeNum).IsHygiene
				&& provSec != 0)
			{
				ProcCur.ProvNum=provSec;
			}
			else{
				ProcCur.ProvNum=provPri;
			}
			if(newStatus==ProcStat.C) {
				ProcCur.Note=ProcCodeNotes.GetNote(ProcCur.ProvNum,ProcCur.CodeNum);
			}
			else {
				ProcCur.Note="";
			}
			ProcCur.ClinicNum=PatCur.ClinicNum;
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=DefC.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			//nextaptnum
			ProcCur.DateEntryC=DateTime.Now;
			ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
			ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
			ProcCur.SiteNum=PatCur.SiteNum;
			Procedures.Insert(ProcCur);
			if((ProcCur.ProcStatus==ProcStat.C || ProcCur.ProcStatus==ProcStat.EC || ProcCur.ProcStatus==ProcStat.EO)
				&& ProcedureCodes.GetProcCode(ProcCur.CodeNum).PaintType==ToothPaintingType.Extraction) {
				//if an extraction, then mark previous procs hidden
				//Procedures.SetHideGraphical(ProcCur);//might not matter anymore
				ToothInitials.SetValue(PatCur.PatNum,ProcCur.ToothNum,ToothInitialType.Missing);
			}
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,new List<ClaimProc>(),true,PlanList,PatPlanList,BenefitList,PatCur.Age);
			FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur.Copy(),FamCur);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.Cancel){
				//any created claimprocs are automatically deleted from within procEdit window.
				try{
					Procedures.Delete(ProcCur.ProcNum);//also deletes the claimprocs
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			}
			else if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				//do not synch recall.  Too slow
			}
			else{
				Recalls.Synch(PatCur.PatNum);
			}
		}
			
		///<summary>No user dialog is shown.  This only works for some kinds of procedures.  Set the codeNum first.</summary>
		private void AddQuick(Procedure ProcCur){
			Plugins.HookAddCode(this,"ContrChart.AddQuick_begin",ProcCur);
			//procnum
			ProcCur.PatNum=PatCur.PatNum;
			//aptnum
			//ProcCur.CodeNum=ProcedureCodes.GetProcCode(ProcCur.OldCode).CodeNum;//already set
			if(newStatus==ProcStat.EO){
				ProcCur.ProcDate=DateTime.MinValue;
			}
			else if(textDate.errorProvider1.GetError(textDate)!=""){
				ProcCur.ProcDate=DateTime.Today;
			}
			else{
				ProcCur.ProcDate=PIn.Date(textDate.Text);
			}
			ProcCur.DateTP=ProcCur.ProcDate;
			if(newStatus==ProcStat.R || newStatus==ProcStat.EO || newStatus==ProcStat.EC) {
				ProcCur.ProcFee=0;
			}
			else {
				InsPlan priplan=null;
				if(PatPlanList.Count>0) {
					priplan=InsPlans.GetPlan(PatPlanList[0].PlanNum,PlanList);
				}
				//check to see if it is a med code
				double insfee;
				bool isMed = false;
				if(ProcCur.MedicalCode != null && ProcCur.MedicalCode != "") {
					isMed = true;
				}
				//get fee schedule for medical ins or dental
				long feeSch;
				if(isMed){
					feeSch = Fees.GetMedFeeSched(PatCur, PlanList, PatPlanList);
				} 
				else {
					feeSch = Fees.GetFeeSched(PatCur, PlanList, PatPlanList);
				}
				insfee = Fees.GetAmount0(ProcCur.CodeNum, feeSch);
				if(priplan!=null && priplan.PlanType=="p" && !isMed) {//PPO
					double standardfee=Fees.GetAmount0(ProcCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched);
					if(standardfee>insfee) {
						ProcCur.ProcFee=standardfee;
					}
					else {
						ProcCur.ProcFee=insfee;
					}
				}
				else {
					ProcCur.ProcFee=insfee;
				}
			}
			//surf
			//toothnum
			//ToothRange
			if(comboPriority.SelectedIndex==0) {
				ProcCur.Priority=0;
			}
			else {
				ProcCur.Priority=DefC.Short[(int)DefCat.TxPriorities][comboPriority.SelectedIndex-1].DefNum;
			}
			ProcCur.ProcStatus=newStatus;
			long provPri=PatCur.PriProv;
			long provSec=PatCur.SecProv;
			for(int i=0;i<ApptList.Length;i++) {
				if(ApptList[i].AptDateTime.Date==DateTime.Today) {
					provPri=ApptList[i].ProvNum;
					provSec=ApptList[i].ProvHyg;
					break;
				}
			}
			if(ProcedureCodes.GetProcCode(ProcCur.CodeNum).IsHygiene
				&& provSec != 0) {
				ProcCur.ProvNum=provSec;
			}
			else {
				ProcCur.ProvNum=provPri;
			}
			if(newStatus==ProcStat.C) {
				ProcCur.Note=ProcCodeNotes.GetNote(ProcCur.ProvNum,ProcCur.CodeNum);
			}
			else {
				ProcCur.Note="";
			}
			ProcCur.ClinicNum=PatCur.ClinicNum;
			if(listDx.SelectedIndex!=-1)
				ProcCur.Dx=DefC.Short[(int)DefCat.Diagnosis][listDx.SelectedIndex].DefNum;
			ProcCur.MedicalCode=ProcedureCodes.GetProcCode(ProcCur.CodeNum).MedicalCode;
			ProcCur.BaseUnits=ProcedureCodes.GetProcCode(ProcCur.CodeNum).BaseUnits;
			ProcCur.SiteNum=PatCur.SiteNum;
			//nextaptnum
			Procedures.Insert(ProcCur);
			if((ProcCur.ProcStatus==ProcStat.C || ProcCur.ProcStatus==ProcStat.EC || ProcCur.ProcStatus==ProcStat.EO)
				&& ProcedureCodes.GetProcCode(ProcCur.CodeNum).PaintType==ToothPaintingType.Extraction) {
				//if an extraction, then mark previous procs hidden
				//Procedures.SetHideGraphical(ProcCur);//might not matter anymore
				ToothInitials.SetValue(PatCur.PatNum,ProcCur.ToothNum,ToothInitialType.Missing);
			}
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				//do not synch recall.  Too slow
			}
			else{
				Recalls.Synch(PatCur.PatNum);
			}
			Procedures.ComputeEstimates(ProcCur,PatCur.PatNum,new List<ClaimProc>(),true,PlanList,PatPlanList,BenefitList,PatCur.Age);
		}

		private void butAddProc_Click(object sender, System.EventArgs e){
			if(newStatus==ProcStat.C){
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.Date(textDate.Text))){
					return;
				}
			}
			bool isValid;
			TreatmentArea tArea;
			FormProcCodes FormP=new FormProcCodes();
			FormP.IsSelectionMode=true;
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) return;
			Procedures.SetDateFirstVisit(DateTime.Today,1,PatCur);
			Procedure ProcCur;
			for(int n=0;n==0 || n<toothChart.SelectedTeeth.Count;n++){
				isValid=true;
				ProcCur=new Procedure();//going to be an insert, so no need to set Procedures.CurOld
				//Procedure
				ProcCur.CodeNum = FormP.SelectedCodeNum;
				//Procedures.Cur.ProcCode=ProcButtonItems.CodeList[i];
				tArea=ProcedureCodes.GetProcCode(ProcCur.CodeNum).TreatArea;
				if((tArea==TreatmentArea.Arch
					|| tArea==TreatmentArea.Mouth
					|| tArea==TreatmentArea.Quad
					|| tArea==TreatmentArea.Sextant
					|| tArea==TreatmentArea.ToothRange)
					&& n>0){//the only two left are tooth and surf
					continue;//only entered if n=0, so they don't get entered more than once.
				}
				else if(tArea==TreatmentArea.Quad){
				//	switch(quadCount){
				//		case 0: Procedures.Cur.Surf="UR"; break;
				//		case 1: Procedures.Cur.Surf="UL"; break;
				//		case 2: Procedures.Cur.Surf="LL"; break;
				//		case 3: Procedures.Cur.Surf="LR"; break;
				//		default: Procedures.Cur.Surf="UR"; break;//this could happen.
				//	}
				//	quadCount++;
				//	AddQuick();
					//Procedures.Cur=ProcCur;
					AddProcedure(ProcCur);
				}
				else if(tArea==TreatmentArea.Surf){
					if(toothChart.SelectedTeeth.Count==0) {
						isValid=false;
					}
					else {
						ProcCur.ToothNum=toothChart.SelectedTeeth[n];
						//Procedures.Cur=ProcCur;
					}
					if(textSurf.Text=="") {
						isValid=false;
					}
					else {
						ProcCur.Surf=Tooth.SurfTidyFromDisplayToDb(textSurf.Text,ProcCur.ToothNum);
					}
					if(isValid) {
						AddQuick(ProcCur);
					}
					else {
						AddProcedure(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.Tooth){
					if(toothChart.SelectedTeeth.Count==0){
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);
					}
					else{
						ProcCur.ToothNum=toothChart.SelectedTeeth[n];
						//Procedures.Cur=ProcCur;
						AddQuick(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.ToothRange){
					if(toothChart.SelectedTeeth.Count==0){
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);
					}
					else{
						ProcCur.ToothRange="";
						for(int b=0;b<toothChart.SelectedTeeth.Count;b++) {
							if(b!=0) ProcCur.ToothRange+=",";
							ProcCur.ToothRange+=toothChart.SelectedTeeth[b];
						}
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);//it's nice to see the procedure to verify the range
					}
				}
				else if(tArea==TreatmentArea.Arch){
					if(toothChart.SelectedTeeth.Count==0) {
						//Procedures.Cur=ProcCur;
						AddProcedure(ProcCur);
						continue;
					}
					if(Tooth.IsMaxillary(toothChart.SelectedTeeth[0])){
						ProcCur.Surf="U";
					}
					else{
						ProcCur.Surf="L";
					}
					//Procedures.Cur=ProcCur;
					AddQuick(ProcCur);
				}
				else if(tArea==TreatmentArea.Sextant){
					//Procedures.Cur=ProcCur;
					AddProcedure(ProcCur);
				}
				else{//mouth
					//Procedures.Cur=ProcCur;
					AddQuick(ProcCur);
				}
			}//for n
			ModuleSelected(PatCur.PatNum);
			if(newStatus==ProcStat.C){
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "
					+DateTime.Today.ToShortDateString());
			}
		}
		
		private void listDx_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//newDx=Defs.Defns[(int)DefCat.Diagnosis][listDx.IndexFromPoint(e.X,e.Y)].DefNum;
		}

		private void gridProg_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Delete || e.KeyCode==Keys.Back) {
				DeleteRows();
			}
		}

		private void DeleteRows(){
			if(gridProg.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Selected Item(s)?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			int skippedC=0;
			int skippedComlog=0;
			DataRow row;
			for(int i=0;i<gridProg.SelectedIndices.Length;i++){
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				if(row["ProcNum"].ToString()!="0"){
					if(PIn.Long(row["ProcStatus"].ToString())==(int)ProcStat.C){
						skippedC++;
					}
					else{
						try{
							Procedures.Delete(PIn.Long(row["ProcNum"].ToString()));//also deletes the claimprocs
						}
						catch(Exception ex){
							MessageBox.Show(ex.Message);
						}
					}
				}
				else if(row["RxNum"].ToString()!="0"){
					RxPats.Delete(PIn.Long(row["RxNum"].ToString()));
				}
				else if(row["CommlogNum"].ToString()!="0"){
					skippedComlog++;
				}
			}
			Recalls.Synch(PatCur.PatNum);
			if(skippedC>0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete completed procedures from here.")+"\r"
					+skippedC.ToString()+" "+Lan.g(this,"item(s) skipped."));
			}
			if(skippedComlog>0) {
				MessageBox.Show(Lan.g(this,"Not allowed to delete commlog entries from here.")+"\r"
					+skippedComlog.ToString()+" "+Lan.g(this,"item(s) skipped."));
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void radioEntryEO_CheckedChanged(object sender,System.EventArgs e) {
			newStatus=ProcStat.EO;
		}

		private void radioEntryEC_CheckedChanged(object sender,System.EventArgs e) {
			newStatus=ProcStat.EC;
		}

		private void radioEntryTP_CheckedChanged(object sender,System.EventArgs e) {
			newStatus=ProcStat.TP;
		}

		private void radioEntryC_CheckedChanged(object sender,System.EventArgs e) {
			newStatus=ProcStat.C;
		}

		private void radioEntryR_CheckedChanged(object sender,System.EventArgs e) {
			newStatus=ProcStat.R;
		}

		private void radioEntryCn_CheckedChanged(object sender,EventArgs e) {
			newStatus=ProcStat.Cn;
		}

		private void listButtonCats_Click(object sender,EventArgs e) {
			FillProcButtons();
		}

		private void listViewButtons_Click(object sender,EventArgs e) {
			if(newStatus==ProcStat.C) {
				if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
					return;
				}
			}
			if(listViewButtons.SelectedIndices.Count==0) {
				return;
			}
			ProcButton ProcButtonCur=ProcButtonList[listViewButtons.SelectedIndices[0]];
			ProcButtonClicked(ProcButtonCur.ProcButtonNum,"");
		}

		///<summary>If quickbutton, then pass the code in and set procButtonNum to 0.</summary>
		private void ProcButtonClicked(long procButtonNum,string quickcode) {
			if(newStatus==ProcStat.C){
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.Date(textDate.Text))){
					return;
				}
			}
			#if TRIALONLY
				if(procButtonNum==0){
					MsgBox.Show(this,"Quick buttons do not work in the trial version because dummy codes are being used instead of real codes.  Just to the left, change to a different category to see other procedure buttons available which do work.");
					return;
				}
			#endif 
			Procedures.SetDateFirstVisit(DateTime.Today,1,PatCur);
			bool isValid;
			TreatmentArea tArea;
			int quadCount=0;//automates quadrant codes.
			long[] codeList;
			long[] autoCodeList;
			if(procButtonNum==0){
				codeList=new long[1];
				codeList[0]=ProcedureCodes.GetCodeNum(quickcode);
				autoCodeList=new long[0];
			}
			else{
				codeList=ProcButtonItems.GetCodeNumListForButton(procButtonNum);
				autoCodeList=ProcButtonItems.GetAutoListForButton(procButtonNum);
				//if(codeList.
			}
			Procedure ProcCur;
			for(int i=0;i<codeList.Length;i++){
				//needs to loop at least once, regardless of whether any teeth are selected.	
				for(int n=0;n==0 || n<toothChart.SelectedTeeth.Count;n++) {
					isValid=true;
					ProcCur=new Procedure();//insert, so no need to set CurOld
					ProcCur.CodeNum=ProcedureCodes.GetProcCode(codeList[i]).CodeNum;
					tArea=ProcedureCodes.GetProcCode(ProcCur.CodeNum).TreatArea;
					if((tArea==TreatmentArea.Arch
						|| tArea==TreatmentArea.Mouth
						|| tArea==TreatmentArea.Quad
						|| tArea==TreatmentArea.Sextant
						|| tArea==TreatmentArea.ToothRange)
						&& n>0){//the only two left are tooth and surf
						continue;//only entered if n=0, so they don't get entered more than once.
					}
					else if(tArea==TreatmentArea.Quad){
						switch(quadCount){
							case 0: ProcCur.Surf="UR"; break;
							case 1: ProcCur.Surf="UL"; break;
							case 2: ProcCur.Surf="LL"; break;
							case 3: ProcCur.Surf="LR"; break;
							default: ProcCur.Surf="UR"; break;//this could happen.
						}
						quadCount++;
						AddQuick(ProcCur);
					}
					else if(tArea==TreatmentArea.Surf){
						if(toothChart.SelectedTeeth.Count==0){
							isValid=false;
						}
						else{
							ProcCur.ToothNum=toothChart.SelectedTeeth[n];
						}
						if(textSurf.Text==""){
							isValid=false;
						}
						else{
							ProcCur.Surf=Tooth.SurfTidyFromDisplayToDb(textSurf.Text,ProcCur.ToothNum);//it's ok if toothnum is not valid.
						}
						if(isValid){
							AddQuick(ProcCur);
						}
						else{
							AddProcedure(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.Tooth){
						if(toothChart.SelectedTeeth.Count==0) {
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothNum=toothChart.SelectedTeeth[n];
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.ToothRange){
						if(toothChart.SelectedTeeth.Count==0) {
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothRange="";
							for(int b=0;b<toothChart.SelectedTeeth.Count;b++) {
								if(b!=0) ProcCur.ToothRange+=",";
								ProcCur.ToothRange+=toothChart.SelectedTeeth[b];
							}
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.Arch){
						if(toothChart.SelectedTeeth.Count==0) {
							AddProcedure(ProcCur);
							continue;
						}
						if(Tooth.IsMaxillary(toothChart.SelectedTeeth[0])){
							ProcCur.Surf="U";
						}
						else{
							ProcCur.Surf="L";
						}
						AddQuick(ProcCur);
					}
					else if(tArea==TreatmentArea.Sextant){
						AddProcedure(ProcCur);
					}
					else{//mouth
						AddQuick(ProcCur);
					}
				}//n selected teeth
			}//end Part 1 checking for ProcCodes, now will check for AutoCodes
			string toothNum;
			string surf;
			bool isAdditional;
			for(int i=0;i<autoCodeList.Length;i++){
				for(int n=0;n==0 || n<toothChart.SelectedTeeth.Count;n++) {
					isValid=true;
					if(toothChart.SelectedTeeth.Count!=0)
						toothNum=toothChart.SelectedTeeth[n];
					else
						toothNum="";
					surf=textSurf.Text;
					isAdditional= n!=0;
					ProcCur=new Procedure();//this will be an insert, so no need to set CurOld
					ProcCur.CodeNum=AutoCodeItems.GetCodeNum(autoCodeList[i],toothNum,surf,isAdditional,PatCur.PatNum,PatCur.Age);
					tArea=ProcedureCodes.GetProcCode(ProcCur.CodeNum).TreatArea;
					if((tArea==TreatmentArea.Arch
						|| tArea==TreatmentArea.Mouth
						|| tArea==TreatmentArea.Quad
						|| tArea==TreatmentArea.Sextant
						|| tArea==TreatmentArea.ToothRange)
						&& n>0){//the only two left are tooth and surf
						continue;//only entered if n=0, so they don't get entered more than once.
					}
					else if(tArea==TreatmentArea.Quad){
						switch(quadCount){
							case 0: ProcCur.Surf="UR"; break;
							case 1: ProcCur.Surf="UL"; break;
							case 2: ProcCur.Surf="LL"; break;
							case 3: ProcCur.Surf="LR"; break;
							default: ProcCur.Surf="UR"; break;//this could happen.
						}
						quadCount++;
						AddQuick(ProcCur);
					}
					else if(tArea==TreatmentArea.Surf){
						if(toothChart.SelectedTeeth.Count==0){
							isValid=false;
						}
						else{
							ProcCur.ToothNum=toothChart.SelectedTeeth[n];
						}
						if(textSurf.Text==""){
							isValid=false;
						}
						else{
							ProcCur.Surf=Tooth.SurfTidyFromDisplayToDb(textSurf.Text,ProcCur.ToothNum);//it's ok if toothnum is invalid
						}
						
						if(isValid){
							AddQuick(ProcCur);
						}
						else{
							AddProcedure(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.Tooth){
						if(toothChart.SelectedTeeth.Count==0) {
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothNum=toothChart.SelectedTeeth[n];
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.ToothRange){
						if(toothChart.SelectedTeeth.Count==0) {
							AddProcedure(ProcCur);
						}
						else{
							ProcCur.ToothRange="";
							for(int b=0;b<toothChart.SelectedTeeth.Count;b++) {
								if(b!=0) ProcCur.ToothRange+=",";
								ProcCur.ToothRange+=toothChart.SelectedTeeth[b];
							}
							AddQuick(ProcCur);
						}
					}
					else if(tArea==TreatmentArea.Arch){
						if(toothChart.SelectedTeeth.Count==0) {
							AddProcedure(ProcCur);
							continue;
						}
						if(Tooth.IsMaxillary(toothChart.SelectedTeeth[0])){
							ProcCur.Surf="U";
						}
						else{
							ProcCur.Surf="L";
						}
						AddQuick(ProcCur);
					}
					else if(tArea==TreatmentArea.Sextant){
						AddProcedure(ProcCur);
					}
					else{//mouth
						AddQuick(ProcCur);
					}
				}//n selected teeth
			}//for i
			ModuleSelected(PatCur.PatNum);
			if(newStatus==ProcStat.C){
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "
					+DateTime.Today.ToShortDateString());
			}
		}

		private void textProcCode_TextChanged(object sender,EventArgs e) {
			if(textProcCode.Text=="d") {
				textProcCode.Text="D";
				textProcCode.SelectionStart=1;
			}
		}

		private void textProcCode_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Return) {
				EnterTypedCode();
			}
		}

		private void textProcCode_Enter(object sender,EventArgs e) {
			if(textProcCode.Text==Lan.g(this,"Type Proc Code")) {
				textProcCode.Text="";
			}
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			EnterTypedCode();
		}

		private void EnterTypedCode() {
			if(newStatus==ProcStat.C) {
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.Date(textDate.Text))) {
					return;
				}
			}
			if(CultureInfo.CurrentCulture.Name=="en-US"
				&& Regex.IsMatch(textProcCode.Text,@"^\d{4}$")//if exactly 4 digits
				&& !ProcedureCodeC.HList.ContainsKey(textProcCode.Text))//and the 4 digit code is not found
			{
				textProcCode.Text="D"+textProcCode.Text;
			}
			if(!ProcedureCodeC.HList.ContainsKey(textProcCode.Text)) {
				MessageBox.Show(Lan.g(this,"Invalid code."));
				//textProcCode.Text="";
				textProcCode.SelectionStart=textProcCode.Text.Length;
				return;
			}
			Procedures.SetDateFirstVisit(DateTime.Today,1,PatCur);
			TreatmentArea tArea;
			Procedure ProcCur;
			int quadCount=0;//automates quadrant codes.
			for(int n=0;n==0 || n<toothChart.SelectedTeeth.Count;n++) {//always loops at least once.
				ProcCur=new Procedure();//this will be an insert, so no need to set CurOld
				ProcCur.CodeNum=ProcedureCodes.GetCodeNum(textProcCode.Text);
				bool isValid=true;
				tArea=ProcedureCodes.GetProcCode(ProcCur.CodeNum).TreatArea;
				if((tArea==TreatmentArea.Arch
					|| tArea==TreatmentArea.Mouth
					|| tArea==TreatmentArea.Quad
					|| tArea==TreatmentArea.Sextant
					|| tArea==TreatmentArea.ToothRange)
					&& n>0) {//the only two left are tooth and surf
					continue;//only entered if n=0, so they don't get entered more than once.
				}
				else if(tArea==TreatmentArea.Quad) {
					switch(quadCount) {
						case 0: ProcCur.Surf="UR"; break;
						case 1: ProcCur.Surf="UL"; break;
						case 2: ProcCur.Surf="LL"; break;
						case 3: ProcCur.Surf="LR"; break;
						default: ProcCur.Surf="UR"; break;//this could happen.
					}
					quadCount++;
					AddQuick(ProcCur);
				}
				else if(tArea==TreatmentArea.Surf) {
					if(toothChart.SelectedTeeth.Count==0){
						isValid=false;
					}
					else{
						ProcCur.ToothNum=toothChart.SelectedTeeth[n];
					}
					if(textSurf.Text==""){
						isValid=false;
					}
					else{
						ProcCur.Surf=Tooth.SurfTidyFromDisplayToDb(textSurf.Text,ProcCur.ToothNum);//it's ok if toothnum is invalid
					}
					if(isValid){
						AddQuick(ProcCur);
					}
					else{
						AddProcedure(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.Tooth) {
					if(toothChart.SelectedTeeth.Count==0) {
						AddProcedure(ProcCur);
					}
					else {
						ProcCur.ToothNum=toothChart.SelectedTeeth[n];
						AddQuick(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.ToothRange) {
					if(toothChart.SelectedTeeth.Count==0) {
						AddProcedure(ProcCur);
					}
					else {
						ProcCur.ToothRange="";
						for(int b=0;b<toothChart.SelectedTeeth.Count;b++) {
							if(b!=0) ProcCur.ToothRange+=",";
							ProcCur.ToothRange+=toothChart.SelectedTeeth[b];
						}
						AddQuick(ProcCur);
					}
				}
				else if(tArea==TreatmentArea.Arch) {
					if(toothChart.SelectedTeeth.Count==0) {
						AddProcedure(ProcCur);
						continue;
					}
					if(Tooth.IsMaxillary(toothChart.SelectedTeeth[0])) {
						ProcCur.Surf="U";
					}
					else {
						ProcCur.Surf="L";
					}
					AddQuick(ProcCur);
				}
				else if(tArea==TreatmentArea.Sextant) {
					AddProcedure(ProcCur);
				}
				else {//mouth
					AddQuick(ProcCur);
				}
			}//n selected teeth
			ModuleSelected(PatCur.PatNum);
			textProcCode.Text="";
			textProcCode.Select();
			if(newStatus==ProcStat.C) {
				SecurityLogs.MakeLogEntry(Permissions.ProcComplCreate,PatCur.PatNum,
					PatCur.GetNameLF()+", "
					+DateTime.Today.ToShortDateString());
			}
		}
		#endregion EnterTx

		#region MissingTeeth
		private void butMissing_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.Missing);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butNotMissing_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.ClearValue(PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.Missing);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butEdentulous_Click(object sender,EventArgs e) {
			ToothInitials.ClearAllValuesForType(PatCur.PatNum,ToothInitialType.Missing);
			for(int i=1;i<=32;i++){
				ToothInitials.SetValueQuick(PatCur.PatNum,i.ToString(),ToothInitialType.Missing,0);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butHidden_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.Hidden);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butUnhide_Click(object sender,EventArgs e) {
			if(listHidden.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an item from the list first.");
				return;
			}
			ToothInitials.ClearValue(PatCur.PatNum,(string)HiddenTeeth[listHidden.SelectedIndex],ToothInitialType.Hidden);
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}
		#endregion MissingTeeth

		#region Movements
		private void FillMovementsAndHidden(){
			if(tabProc.Height<50){//skip if the tab control is short(not visible){
				return;
			}
			if(tabProc.SelectedIndex==2)//movements tab
			{
				if(toothChart.SelectedTeeth.Count==0) {
					textShiftM.Text="";
					textShiftO.Text="";
					textShiftB.Text="";
					textRotate.Text="";
					textTipM.Text="";
					textTipB.Text="";
					return;
				}
				textShiftM.Text=
					ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[0],ToothInitialType.ShiftM).ToString();  
				textShiftO.Text=
					ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[0],ToothInitialType.ShiftO).ToString();
				textShiftB.Text=
					ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[0],ToothInitialType.ShiftB).ToString();
				textRotate.Text=
					ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[0],ToothInitialType.Rotate).ToString();
				textTipM.Text=
					ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[0],ToothInitialType.TipM).ToString();
				textTipB.Text=
					ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[0],ToothInitialType.TipB).ToString();
				//At this point, all 6 blanks have either a number or 0.
				//As we go through this loop, none of the values will change.
				//The only thing that will happen is that some of them will become blank.
				string move;
				for(int i=1;i<toothChart.SelectedTeeth.Count;i++) {
					move=ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[i],ToothInitialType.ShiftM).ToString();
					if(textShiftM.Text != move){
						textShiftM.Text="";
					}
					move=ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[i],ToothInitialType.ShiftO).ToString();
					if(textShiftO.Text != move) {
						textShiftO.Text="";
					}
					move=ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[i],ToothInitialType.ShiftB).ToString();
					if(textShiftB.Text != move) {
						textShiftB.Text="";
					}
					move=ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[i],ToothInitialType.Rotate).ToString();
					if(textRotate.Text != move) {
						textRotate.Text="";
					}
					move=ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[i],ToothInitialType.TipM).ToString();
					if(textTipM.Text != move) {
						textTipM.Text="";
					}
					move=ToothInitials.GetMovement(ToothInitialList,toothChart.SelectedTeeth[i],ToothInitialType.TipB).ToString();
					if(textTipB.Text != move) {
						textTipB.Text="";
					}
				}
			}//if movements tab
			if(tabProc.SelectedIndex==1){//missing teeth
				listHidden.Items.Clear();
				HiddenTeeth=ToothInitials.GetHiddenTeeth(ToothInitialList);
				for(int i=0;i<HiddenTeeth.Count;i++){
					listHidden.Items.Add(Tooth.ToInternat((string)HiddenTeeth[i]));
				}
			}
		}

		private void butShiftMminus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.ShiftM,-2);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butShiftMplus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.ShiftM,2);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butShiftOminus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.ShiftO,-2);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butShiftOplus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.ShiftO,2);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butShiftBminus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.ShiftB,-2);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butShiftBplus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.ShiftB,2);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butRotateMinus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.Rotate,-20);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butRotatePlus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.Rotate,20);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butTipMminus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.TipM,-10);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butTipMplus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.TipM,10);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butTipBminus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.TipB,-10);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butTipBplus_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.AddMovement(ToothInitialList,PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.TipB,10);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}

		private void butApplyMovements_Click(object sender,EventArgs e) {
			if(textShiftM.errorProvider1.GetError(textShiftM)!=""
				|| textShiftO.errorProvider1.GetError(textShiftO)!=""
				|| textShiftB.errorProvider1.GetError(textShiftB)!=""
				|| textRotate.errorProvider1.GetError(textRotate)!=""
				|| textTipM.errorProvider1.GetError(textTipM)!=""
				|| textTipB.errorProvider1.GetError(textTipB)!="")
			{
				MsgBox.Show(this,"Please fix errors first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				if(textShiftM.Text!=""){
					ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],
						ToothInitialType.ShiftM,PIn.Float(textShiftM.Text));
				}
				if(textShiftO.Text!="") {
					ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],
						ToothInitialType.ShiftO,PIn.Float(textShiftO.Text));
				}
				if(textShiftB.Text!="") {
					ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],
						ToothInitialType.ShiftB,PIn.Float(textShiftB.Text));
				}
				if(textRotate.Text!="") {
					ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],
						ToothInitialType.Rotate,PIn.Float(textRotate.Text));
				}
				if(textTipM.Text!="") {
					ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],
						ToothInitialType.TipM,PIn.Float(textTipM.Text));
				}
				if(textTipB.Text!="") {
					ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],
						ToothInitialType.TipB,PIn.Float(textTipB.Text));
				}
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(true);
		}
		#endregion Movements

		#region Primary
		private void butPrimary_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				ToothInitials.SetValue(PatCur.PatNum,toothChart.SelectedTeeth[i],ToothInitialType.Primary);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butPerm_Click(object sender,EventArgs e) {
			if(toothChart.SelectedTeeth.Count==0) {
				MsgBox.Show(this,"Please select teeth first.");
				return;
			}
			for(int i=0;i<toothChart.SelectedTeeth.Count;i++) {
				if(Tooth.IsPrimary(toothChart.SelectedTeeth[i])){
					ToothInitials.ClearValue(PatCur.PatNum,Tooth.PriToPerm(toothChart.SelectedTeeth[i])
						,ToothInitialType.Primary);
				}
				else{
					ToothInitials.ClearValue(PatCur.PatNum,toothChart.SelectedTeeth[i]
						,ToothInitialType.Primary);
				}
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butAllPrimary_Click(object sender,EventArgs e) {
			ToothInitials.ClearAllValuesForType(PatCur.PatNum,ToothInitialType.Primary);
			for(int i=1;i<=32;i++){
				ToothInitials.SetValueQuick(PatCur.PatNum,i.ToString(),ToothInitialType.Primary,0);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butAllPerm_Click(object sender,EventArgs e) {
			ToothInitials.ClearAllValuesForType(PatCur.PatNum,ToothInitialType.Primary);
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}

		private void butMixed_Click(object sender,EventArgs e) {
			ToothInitials.ClearAllValuesForType(PatCur.PatNum,ToothInitialType.Primary);
			string[] priTeeth=new string[] 
				{"1","2","4","5","6","11","12","13","15","16","17","18","20","21","22","27","28","29","31","32"};
			for(int i=0;i<priTeeth.Length;i++) {
				ToothInitials.SetValueQuick(PatCur.PatNum,priTeeth[i],ToothInitialType.Primary,0);
			}
			ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
			FillToothChart(false);
		}
		#endregion Primary

		#region Planned
		private void FillPlanned(){
			if(PatCur==null){
				checkDone.Checked=false;
				butNew.Enabled=false;
				butPin.Enabled=false;
				butClear.Enabled=false;
				butUp.Enabled=false;
				butDown.Enabled=false;
				gridPlanned.Enabled=false;
				return;
			}
			else{
				butNew.Enabled=true;
				butPin.Enabled=true;
				butClear.Enabled=true;
				butUp.Enabled=true;
				butDown.Enabled=true;
				gridPlanned.Enabled=true;
			}
			if(PatCur.PlannedIsDone) {
				checkDone.Checked=true;
			}
			else {
				checkDone.Checked=false;
			}
			//Fill grid
			gridPlanned.BeginUpdate();
			gridPlanned.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TablePlannedAppts","#"),25,HorizontalAlignment.Center);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","Min"),35);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","Procedures"),175);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","Note"),175);
			gridPlanned.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePlannedAppts","DateSched"),80);
			gridPlanned.Columns.Add(col);
			gridPlanned.Rows.Clear();
			ODGridRow row;
			DataTable table=DataSetMain.Tables["Planned"];
			//This gets done in the business layer:
			/*
			bool iochanged=false;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["ItemOrder"].ToString()!=i.ToString()) {
					PlannedAppt planned=PlannedAppts.CreateObject(PIn.PLong(table.Rows[i]["PlannedApptNum"].ToString()));
					planned.ItemOrder=i;
					PlannedAppts.InsertOrUpdate(planned);
					iochanged=true;
				}
			}
			if(iochanged) {
				DataSetMain=ChartModules.GetAll(PatCur.PatNum,checkAudit.Checked);
				table=DataSetMain.Tables["Planned"];
			}*/
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["ItemOrder"].ToString());
				row.Cells.Add(table.Rows[i]["minutes"].ToString());
				row.Cells.Add(table.Rows[i]["ProcDescript"].ToString());
				row.Cells.Add(table.Rows[i]["Note"].ToString());
				row.Cells.Add(table.Rows[i]["dateSched"].ToString());
				row.ColorText=Color.FromArgb(PIn.Int(table.Rows[i]["colorText"].ToString()));
				row.ColorBackG=Color.FromArgb(PIn.Int(table.Rows[i]["colorBackG"].ToString()));
				gridPlanned.Rows.Add(row);
			}
			gridPlanned.EndUpdate();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			/*if(ApptPlanned.Visible){
				if(MessageBox.Show(Lan.g(this,"Replace existing planned appointment?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
					return;
				//Procedures.UnattachProcsInPlannedAppt(ApptPlanned.Info.MyApt.AptNum);
				AppointmentL.Delete(PIn.PInt(ApptPlanned.DataRoww["AptNum"].ToString()));
			}*/
			Appointment AptCur=new Appointment();
			AptCur.PatNum=PatCur.PatNum;
			AptCur.ProvNum=PatCur.PriProv;
			AptCur.ClinicNum=PatCur.ClinicNum;
			AptCur.AptStatus=ApptStatus.Planned;
			AptCur.AptDateTime=DateTime.Today;
			AptCur.Pattern="/X/";
			Appointments.Insert(AptCur);
			PlannedAppt plannedAppt=new PlannedAppt();
			plannedAppt.AptNum=AptCur.AptNum;
			plannedAppt.PatNum=PatCur.PatNum;
			plannedAppt.ItemOrder=DataSetMain.Tables["Planned"].Rows.Count+1;
			PlannedAppts.Insert(plannedAppt);
			FormApptEdit FormApptEdit2=new FormApptEdit(AptCur.AptNum);
			FormApptEdit2.IsNew=true;
			FormApptEdit2.ShowDialog();
			if(FormApptEdit2.DialogResult!=DialogResult.OK){
				//delete new appt, delete plannedappt, and unattach procs already handled in dialog
				FillPlanned();
				return;
			}
			List<Procedure> myProcList=Procedures.Refresh(PatCur.PatNum);
			bool allProcsHyg=true;
			for(int i=0;i<myProcList.Count;i++){
				if(myProcList[i].PlannedAptNum!=AptCur.AptNum)
					continue;//only concerned with procs on this plannedAppt
				if(!ProcedureCodes.GetProcCode(myProcList[i].CodeNum).IsHygiene){
					allProcsHyg=false;
					break;
				}
			}
			if(allProcsHyg && PatCur.SecProv!=0){
				Appointment aptOld=AptCur.Clone();
				AptCur.ProvNum=PatCur.SecProv;
				Appointments.Update(AptCur,aptOld);
			}
			Patient patOld=PatCur.Copy();
			//PatCur.NextAptNum=AptCur.AptNum;
			PatCur.PlannedIsDone=false;
			Patients.Update(PatCur,patOld);
			ModuleSelected(PatCur.PatNum);//if procs were added in appt, then this will display them
		}

		///<summary></summary>
		private void butClear_Click(object sender, System.EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select an item first");
				return;
			}
			if(!MsgBox.Show(this,true,"Delete planned appointment(s)?")){
				return;
			}
			for(int i=0;i<gridPlanned.SelectedIndices.Length;i++){
				Appointments.Delete(PIn.Long(DataSetMain.Tables["Planned"].Rows[gridPlanned.SelectedIndices[i]]["AptNum"].ToString()));
			}
			ModuleSelected(PatCur.PatNum);
		}

		///<summary></summary>
		private void butPin_Click(object sender,EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select an item first");
				return;
			}
			List<long> aptNums=new List<long>();
			for(int i=0;i<gridPlanned.SelectedIndices.Length;i++){
				aptNums.Add(PIn.Long(DataSetMain.Tables["Planned"].Rows[gridPlanned.SelectedIndices[i]]["AptNum"].ToString()));
			}
			GotoModule.PinToAppt(aptNums,0);
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			if(gridPlanned.SelectedIndices.Length>1) {
				MsgBox.Show(this,"Please only select one item first.");
				return;
			}
			DataTable table=DataSetMain.Tables["Planned"];
			int idx=gridPlanned.SelectedIndices[0];
			if(idx==0) {
				return;
			}
			PlannedAppt planned;
			planned=PlannedAppts.GetOne(PIn.Long(table.Rows[idx]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx-1;
			PlannedAppts.Update(planned);
			planned=PlannedAppts.GetOne(PIn.Long(table.Rows[idx-1]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx;
			PlannedAppts.Update(planned);
			DataSetMain=ChartModules.GetAll(PatCur.PatNum,checkAudit.Checked);
			FillPlanned();
			gridPlanned.SetSelected(idx-1,true);
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(gridPlanned.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			if(gridPlanned.SelectedIndices.Length>1) {
				MsgBox.Show(this,"Please only select one item first.");
				return;
			}
			DataTable table=DataSetMain.Tables["Planned"];
			int idx=gridPlanned.SelectedIndices[0];
			if(idx==table.Rows.Count-1) {
				return;
			}
			PlannedAppt planned;
			planned=PlannedAppts.GetOne(PIn.Long(table.Rows[idx]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx+1;
			PlannedAppts.Update(planned);
			planned=PlannedAppts.GetOne(PIn.Long(table.Rows[idx+1]["PlannedApptNum"].ToString()));
			planned.ItemOrder=idx;
			PlannedAppts.Update(planned);
			DataSetMain=ChartModules.GetAll(PatCur.PatNum,checkAudit.Checked);
			FillPlanned();
			gridPlanned.SetSelected(idx+1,true);
		}

		private void gridPlanned_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long aptnum=PIn.Long(DataSetMain.Tables["Planned"].Rows[e.Row]["AptNum"].ToString());
			FormApptEdit FormAE=new FormApptEdit(aptnum);
			FormAE.ShowDialog();
			ModuleSelected(PatCur.PatNum);//if procs were added in appt, then this will display them*/
			for(int i=0;i<DataSetMain.Tables["Planned"].Rows.Count;i++){
				if(DataSetMain.Tables["Planned"].Rows[i]["AptNum"].ToString()==aptnum.ToString()){
					gridPlanned.SetSelected(i,true);
				}
			}
		}

		private void checkDone_Click(object sender, System.EventArgs e) {
			Patient oldPat=PatCur.Copy();
			if(checkDone.Checked){
				if(DataSetMain.Tables["Planned"].Rows.Count>0){
					if(!MsgBox.Show(this,true,"ALL planned appointment(s) will be deleted. Continue?")){
						checkDone.Checked=false;
						return; 
					}
					for(int i=0;i<DataSetMain.Tables["Planned"].Rows.Count;i++){
						Appointments.Delete(PIn.Long(DataSetMain.Tables["Planned"].Rows[i]["AptNum"].ToString()));
					}
				}
				PatCur.PlannedIsDone=true;
				Patients.Update(PatCur,oldPat);
			}
			else{
				PatCur.PlannedIsDone=false;
				Patients.Update(PatCur,oldPat);
			}
			ModuleSelected(PatCur.PatNum);
		}
		#endregion

		#region Show
		private void button1_Click(object sender, System.EventArgs e) {
			//sometimes used for testing purposes
		}

		private void checkShowTP_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkShowC_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkShowE_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkShowR_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkShowCn_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkNotes_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkAppt_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkComm_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkCommFamily_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkLabCase_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkRx_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkTasks_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkEmail_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkSheets_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void checkShowTeeth_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			if(checkShowTeeth.Checked) {
				checkShowTP.Checked=true;
				checkShowC.Checked=true;
				checkShowE.Checked=true;
				checkShowR.Checked=true;
				checkShowCn.Checked=true;
				checkNotes.Checked=true;
				checkAppt.Checked=false;
				checkComm.Checked=false;
				checkCommFamily.Checked=false;
				checkLabCase.Checked=false;
				checkRx.Checked=false;
				checkEmail.Checked=false;
				checkTasks.Checked=false;
				checkSheets.Checked=false;
			}
			else {
				checkShowTP.Checked=true;
				checkShowC.Checked=true;
				checkShowE.Checked=true;
				checkShowR.Checked=true;
				checkShowCn.Checked=true;
				checkNotes.Checked=true;
				checkAppt.Checked=true;
				checkComm.Checked=true;
				checkCommFamily.Checked=true;
				checkLabCase.Checked=true;
				checkRx.Checked=true;
				checkEmail.Checked=true;
				checkTasks.Checked=true;
				checkSheets.Checked=true;
			}
			FillProgNotes(true);
		}

		private void checkAudit_Click(object sender,EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			FillProgNotes();
		}

		private void butShowAll_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			checkShowTP.Checked=true;
			checkShowC.Checked=true;
			checkShowE.Checked=true;
			checkShowR.Checked=true;
			checkShowCn.Checked=true;
			checkNotes.Checked=true;
			checkAppt.Checked=true;
			checkComm.Checked=true;
			checkCommFamily.Checked=true;
			checkLabCase.Checked=true;
			checkRx.Checked=true;
			checkShowTeeth.Checked=false;
			checkTasks.Checked=true;
			checkEmail.Checked=true;
			checkSheets.Checked=true;
			FillProgNotes();
		}

		private void butShowNone_Click(object sender,System.EventArgs e) {
			if(gridChartViews.Rows.Count>0) {
				labelCustView.Visible=true;
			}
			chartCustViewChanged=true;
			checkShowTP.Checked=false;
			checkShowC.Checked=false;
			checkShowE.Checked=false;
			checkShowR.Checked=false;
			checkShowCn.Checked=false;
			checkNotes.Checked=false;
			checkAppt.Checked=false;
			checkComm.Checked=false;
			checkCommFamily.Checked=false;
			checkLabCase.Checked=false;
			checkRx.Checked=false;
			checkShowTeeth.Checked=false;
			checkTasks.Checked=false;
			checkEmail.Checked=false;
			checkSheets.Checked=false;
			FillProgNotes();
		}
		#endregion Show

		#region Draw
		private void radioPointer_Click(object sender,EventArgs e) {
			toothChart.CursorTool=CursorTool.Pointer;
		}

		private void radioPen_Click(object sender,EventArgs e) {
			toothChart.CursorTool=CursorTool.Pen;
		}

		private void radioEraser_Click(object sender,EventArgs e) {
			toothChart.CursorTool=CursorTool.Eraser;
		}

		private void radioColorChanger_Click(object sender,EventArgs e) {
			toothChart.CursorTool=CursorTool.ColorChanger;
		}

		private void panelDrawColor_DoubleClick(object sender,EventArgs e) {
			//do nothing
		}

		private void toothChart_SegmentDrawn(object sender,ToothChartDrawEventArgs e) {
			if(radioPen.Checked){
				ToothInitial ti=new ToothInitial();
				ti.DrawingSegment=e.DrawingSegement;
				ti.InitialType=ToothInitialType.Drawing;
				ti.PatNum=PatCur.PatNum;
				ti.ColorDraw=panelDrawColor.BackColor;
				ToothInitials.Insert(ti);
				ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
				FillToothChart(true);
			}
			else if(radioEraser.Checked){
				//for(int i=0;i<ToothInitialList.Count;i++){
				for(int i=ToothInitialList.Count-1;i>=0;i--) {//go backwards
					if(ToothInitialList[i].InitialType!=ToothInitialType.Drawing) {
						continue;
					}
					if(ToothInitialList[i].DrawingSegment!=e.DrawingSegement) {
						continue;
					}
					ToothInitials.Delete(ToothInitialList[i]);
					ToothInitialList.RemoveAt(i);
					//no need to refresh since that's handled by the toothchart.
				}
			}
			else if(radioColorChanger.Checked){
				for(int i=0;i<ToothInitialList.Count;i++){
					if(ToothInitialList[i].InitialType!=ToothInitialType.Drawing){
						continue;
					}
					if(ToothInitialList[i].DrawingSegment!=e.DrawingSegement){
						continue;
					}
					ToothInitialList[i].ColorDraw=panelDrawColor.BackColor;
					ToothInitials.Update(ToothInitialList[i]);
					FillToothChart(true);
				}
			}
		}

		private void panelTPdark_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelTPdark.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelTPlight_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelTPlight.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelCdark_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelCdark.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelClight_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelClight.BackColor;
		}

		private void panelECdark_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelECdark.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelEClight_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelEClight.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelEOdark_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelEOdark.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelEOlight_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelEOlight.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelRdark_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelRdark.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelRlight_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=panelRlight.BackColor;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}

		private void panelBlack_Click(object sender,EventArgs e) {
			panelDrawColor.BackColor=Color.Black;
			toothChart.ColorDrawing=Color.Black;
		}

		private void butColorOther_Click(object sender,EventArgs e) {
			ColorDialog cd=new ColorDialog();
			cd.Color=butColorOther.BackColor;
			if(cd.ShowDialog()!=DialogResult.OK){
				return;
			}
			panelDrawColor.BackColor=cd.Color;
			toothChart.ColorDrawing=panelDrawColor.BackColor;
		}
		#endregion Draw

		private void gridPtInfo_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(Plugins.HookMethod(this,"ContrChart.gridPtInfo_CellDoubleClick",PatCur,FamCur,e,PatientNoteCur)) {
				return;
			}
			if(gridPtInfo.Rows[e.Row].Tag==null){//pt info
				return;
			}
			if(gridPtInfo.Rows[e.Row].Tag.ToString()=="med"){
				FormMedical FormM=new FormMedical(PatientNoteCur,PatCur);
				FormM.ShowDialog();
				ModuleSelected(PatCur.PatNum);
				return;
			}
			if(gridPtInfo.Rows[e.Row].Tag.GetType()==typeof(RegistrationKey)){
				FormRegistrationKeyEdit FormR=new FormRegistrationKeyEdit();
				FormR.RegKey=(RegistrationKey)gridPtInfo.Rows[e.Row].Tag;
				FormR.ShowDialog();
				FillPtInfo();
				return;
			}
			/*else {//editing patfield
				string tag=gridPtInfo.Rows[e.Row].Tag.ToString();
				tag=tag.Substring(8);//strips off all but the number: PatField1
				int index=PIn.Int(tag);
				PatField field=PatFields.GetByName(PatFieldDefs.List[index].FieldName,PatFieldList);
				if(field==null) {
					field=new PatField();
					field.PatNum=PatCur.PatNum;
					field.FieldName=PatFieldDefs.List[index].FieldName;
					FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
					FormPF.IsNew=true;
					FormPF.ShowDialog();
				}
				else {
					FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
					FormPF.ShowDialog();
				}
			}*/
		}

		private void toothChart_Click(object sender,EventArgs e) {
			textSurf.Text="";
			//if(toothChart.SelectedTeeth.Length==1) {
				//butO.BackColor=SystemColors.Control;
				//butB.BackColor=SystemColors.Control;
				//butF.BackColor=SystemColors.Control;
				//if(Tooth.IsAnterior(toothChart.SelectedTeeth[0])) {
					//butB.Text="";
					//butO.Text="";
					//butB.Enabled=false;
					//butO.Enabled=false;
					//butF.Text="F";
					//butI.Text="I";
					//butF.Enabled=true;
					//butI.Enabled=true;
				//}
				//else {
					//butB.Text="B";
					//butO.Text="O";
					//butB.Enabled=true;
					//butO.Enabled=true;
					//butF.Text="";
					//butI.Text="";
					//butF.Enabled=false;
					//butI.Enabled=false;
				//}
			//}
			if(checkShowTeeth.Checked) {
				FillProgNotes();
			}
			FillMovementsAndHidden();
		}

		private void gridProg_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button==MouseButtons.Right) {
				if(PrefC.GetBool(PrefName.EasyHideHospitals)){
					menuItemPrintDay.Visible=false;
				}
				else{
					menuItemPrintDay.Visible=true;
				}
				menuProgRight.Show(gridProg,new Point(e.X,e.Y));
			}
		}

		private void menuItemPrintProg_Click(object sender, System.EventArgs e) {
			pagesPrinted=0;
			headingPrinted=false;
			#if DEBUG
				PrintReport(true);
			#else
				PrintReport(false);	
			#endif
		}

		private void menuItemPrintDay_Click(object sender,EventArgs e) {
			if(gridProg.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select at least one item first.");
				return;
			}
			DataRow row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[0]].Tag;
			hospitalDate=PIn.DateT(row["ProcDate"].ToString());
			bool showRx=this.checkRx.Checked;
			bool showComm=this.checkComm.Checked;
			bool showApt=this.checkAppt.Checked;
			bool showEmail=this.checkEmail.Checked;
			bool showTask=this.checkTasks.Checked;
			bool showLab=this.checkLabCase.Checked;
			bool showSheets=this.checkSheets.Checked;
			checkRx.Checked=false;
			checkComm.Checked=false;
			checkAppt.Checked=false;
			checkEmail.Checked=false;
			checkTasks.Checked=false;
			checkLabCase.Checked=false;
			checkSheets.Checked=false;
			FillProgNotes();
			try {
				pagesPrinted=0;
				headingPrinted=false;
				#if DEBUG
					PrintDay(true);
				#else
					PrintDay(false);
				#endif
			}
			catch {

			}
			hospitalDate=DateTime.MinValue;
			checkRx.Checked=showRx;
			checkComm.Checked=showComm;
			checkAppt.Checked=showApt;
			checkEmail.Checked=showEmail;
			checkTasks.Checked=showTask;
			checkLabCase.Checked=showLab;
			checkSheets.Checked=showSheets;
			FillProgNotes();
		}

		private void menuItemDelete_Click(object sender,EventArgs e) {
			DeleteRows();
		}

		private void menuItemSetComplete_Click(object sender,EventArgs e) {
			//moved down so we can have the date first
			//if(!Security.IsAuthorized(Permissions.ProcComplCreate)) {
			//	return;
			//}
			if(gridProg.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			if(checkAudit.Checked) {
				MsgBox.Show(this,"Not allowed in audit mode.");
				return;
			}
			DataRow row;
			Appointment apt;
			//One appointment-------------------------------------------------------------------------------------------------------------
			if(gridProg.SelectedIndices.Length==1
				&& ((DataRow)gridProg.Rows[gridProg.SelectedIndices[0]].Tag)["AptNum"].ToString()!="0")
			{
				if(!Security.IsAuthorized(Permissions.AppointmentEdit)){
					return;
				}
				apt=Appointments.GetOneApt(PIn.Long(((DataRow)gridProg.Rows[gridProg.SelectedIndices[0]].Tag)["AptNum"].ToString()));
				if(apt.AptStatus == ApptStatus.Complete) {
					MsgBox.Show(this,"Already complete.");
					return;
				}
				if(apt.AptStatus == ApptStatus.PtNote
					|| apt.AptStatus == ApptStatus.PtNoteCompleted
					|| apt.AptStatus == ApptStatus.Planned
					|| apt.AptStatus == ApptStatus.UnschedList)
				{
					MsgBox.Show(this,"Not allowed for that status.");
					return;
				}
				if(!Security.IsAuthorized(Permissions.ProcComplCreate,apt.AptDateTime)) {
					return;
				}
				if(apt.AptDateTime.Date>DateTime.Today) {
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Appointment is in the future.  Set complete anyway?")){
						return;
					}
				}
				else if(!MsgBox.Show(this,true,"Set appointment complete?")){
					return;
				}
				Appointments.SetAptStatusComplete(apt.AptNum,PatPlans.GetPlanNum(PatPlanList,1),PatPlans.GetPlanNum(PatPlanList,2));
				ProcedureL.SetCompleteInAppt(apt,PlanList,PatPlanList,PatCur.SiteNum,PatCur.Age);//loops through each proc
				SecurityLogs.MakeLogEntry(Permissions.AppointmentEdit, apt.PatNum,
					PatCur.GetNameLF() + ", "
					+ apt.ProcDescript + ", "
					+ apt.AptDateTime.ToString() + ", "
					+ "Set Complete");
				Recalls.Synch(PatCur.PatNum);
				ModuleSelected(PatCur.PatNum);
				return;
			}
			//Multiple procedures------------------------------------------------------------------------------------------------------
			if(!PrefC.GetBool(PrefName.AllowSettingProcsComplete)){
				MsgBox.Show(this,"Only single appointments may be set complete.  If you want to be able to set procedures complete, you must turn on that option in Setup Modules.");
				return;
			}
			//check to make sure we don't have non-procedures
			for(int i=0;i<gridProg.SelectedIndices.Length;i++) {
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				if(row["ProcNum"].ToString()=="0") {
					MsgBox.Show(this,"Only procedures or single appointments may be set complete.");
					return;
				}
			}
			Procedure procCur;
			Procedure procOld;
			ProcedureCode procCode;
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//this loop is just for security:
			for(int i=0;i<gridProg.SelectedIndices.Length;i++) {
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				procCur=Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),true);
				if(procCur.ProcStatus==ProcStat.C){
					continue;//because it will be skipped below anyway
				}
				if(procCur.AptNum!=0) {//if attached to an appointment
					apt=Appointments.GetOneApt(procCur.AptNum);
					if(apt.AptDateTime.Date > MiscData.GetNowDateTime().Date) {
						MessageBox.Show(Lan.g(this,"Not allowed because a procedure is attached to a future appointment with a date of ")+apt.AptDateTime.ToShortDateString());
						return;
					}
					if(!Security.IsAuthorized(Permissions.ProcComplCreate,apt.AptDateTime)) {
						return;
					}
				}
				else{
					if(!Security.IsAuthorized(Permissions.ProcComplCreate,PIn.Date(textDate.Text))) {
						return;
					}
				}
			}
			List<string> procCodeList=new List<string>();//for automation
			for(int i=0;i<gridProg.SelectedIndices.Length;i++) {
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				apt=null;
				procCur=Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),true);
				if(procCur.ProcStatus==ProcStat.C){
					continue;//don't allow setting a procedure complete again.  Important for security reasons.
				}
				procOld=procCur.Copy();
				procCode=ProcedureCodes.GetProcCode(procCur.CodeNum);
				procCodeList.Add(ProcedureCodes.GetStringProcCode(procCur.CodeNum));
				if(procOld.ProcStatus!=ProcStat.C) {
					//if procedure was already complete, then don't add more notes.
					procCur.Note+=ProcCodeNotes.GetNote(procCur.ProvNum,procCur.CodeNum);//note wasn't complete, so add notes
				}
				procCur.DateEntryC=DateTime.Now;
				if(procCur.AptNum!=0) {//if attached to an appointment
					apt=Appointments.GetOneApt(procCur.AptNum);
					procCur.ProcDate=apt.AptDateTime;
					procCur.ClinicNum=apt.ClinicNum;
					procCur.PlaceService=Clinics.GetPlaceService(apt.ClinicNum);
				}
				else {
					procCur.ProcDate=PIn.Date(textDate.Text);
					procCur.PlaceService=(PlaceOfService)PrefC.GetLong(PrefName.DefaultProcedurePlaceService);
				}
				procCur.SiteNum=PatCur.SiteNum;
				Procedures.SetDateFirstVisit(procCur.ProcDate,2,PatCur);
				if(procCode.PaintType==ToothPaintingType.Extraction){//if an extraction, then mark previous procs hidden
					//Procedures.SetHideGraphical(procCur);//might not matter anymore
					ToothInitials.SetValue(PatCur.PatNum,procCur.ToothNum,ToothInitialType.Missing);
				}
				procCur.ProcStatus=ProcStat.C;
				Procedures.Update(procCur,procOld);
				//Tried to move it to the business layer, but too complex for now.
				//Procedures.SetComplete(
				//	((Procedure)gridProg.Rows[gridProg.SelectedIndices[i]].Tag).ProcNum,PIn.PDate(textDate.Text));
				Procedures.ComputeEstimates(procCur,procCur.PatNum,ClaimProcList,false,PlanList,PatPlanList,BenefitList,PatCur.Age);
			}
			AutomationL.Trigger(AutomationTrigger.CompleteProcedure,procCodeList,PatCur.PatNum);
			Recalls.Synch(PatCur.PatNum);
			//if(skipped>0){
			//	MessageBox.Show(Lan.g(this,".")+"\r\n"
			//		+skipped.ToString()+" "+Lan.g(this,"procedures(s) skipped."));
			//}
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemEditSelected_Click(object sender,EventArgs e) {
			if(gridProg.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select procedures first.");
				return;
			}
			DataRow row;
			for(int i=0;i<gridProg.SelectedIndices.Length;i++){
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				if(row["ProcNum"].ToString()=="0") {
					MsgBox.Show(this,"Only procedures may be edited.");
					return;
				}
			}
			List<Procedure> proclist=new List<Procedure>();
			Procedure proc;
			for(int i=0;i<gridProg.SelectedIndices.Length;i++){
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				proc=Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),false);
				proclist.Add(proc);
			}
			FormProcEditAll FormP=new FormProcEditAll();
			FormP.ProcList=proclist;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void menuItemGroupSelected_Click(object sender,EventArgs e) {
			int procCount=0;
			DataRow row;
			for(int i=0;i<gridProg.SelectedIndices.Length;i++){
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				if(row["ProcNum"].ToString()=="0") {
					MsgBox.Show(this,"Only procedures may be grouped.");
					return;
				}
				else{
					procCount++;
				}
			}
			if(procCount<2){
				MsgBox.Show(this,"Please select multiple procedure to group.");
				return;
			}
			List<Procedure> proclist=new List<Procedure>();
			Procedure proc;
			for(int i=0;i<gridProg.SelectedIndices.Length;i++){//Create proclist from selected items.
				row=(DataRow)gridProg.Rows[gridProg.SelectedIndices[i]].Tag;
				proc=Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),false);
				proclist.Add(proc);
			}
			//Validate the list of procedures------------------------------------------------------------------------------------
			DateTime procDate=proclist[0].ProcDate;
			long clinicNum=proclist[0].ClinicNum;
			long provNum=proclist[0].ProvNum;
			for(int i=0;i<proclist.Count;i++){//starts at 0 to check procStatus
				if(proclist[i].ProcDate!=procDate){
					MsgBox.Show(this,"Only procedures with the same date may be grouped.");
					return;
				}
				if(proclist[i].ProcStatus!=ProcStat.C){
					MsgBox.Show(this,"You may only group completed procedures.");
					return;
				}
				if(proclist[i].ClinicNum!=clinicNum){
					MsgBox.Show(this,"Only procedures with the same clinic may be grouped.");
					return;
				}
				if(proclist[i].ProvNum!=provNum){
					MsgBox.Show(this,"Only procedures done by the same provider may be grouped.");
					return;
				}
			}
			//Procedures are valid. Create new Procedure "group" and ProcGroupItems-------------------------------------------------------
			Procedure group=new Procedure();
			group.PatNum=PatCur.PatNum;
			group.ProcStatus=ProcStat.C;
			group.ProcDate=procDate;
			group.ProvNum=provNum;
			group.ClinicNum=clinicNum;
			group.CodeNum=ProcedureCodes.GetCodeNum(ProcedureCodes.GroupProcCode);
			group.IsNew=true;
			Procedures.Insert(group);
			List<ProcGroupItem> groupItemList=new List<ProcGroupItem>();
			ProcGroupItem groupItem;
			for(int i=0;i<proclist.Count;i++){
				groupItem=new ProcGroupItem();
				groupItem.ProcNum=proclist[i].ProcNum;
				groupItem.GroupNum=group.ProcNum;
				ProcGroupItems.Insert(groupItem);
				groupItemList.Add(groupItem);
			}
			FormProcGroup FormP=new FormProcGroup();
			FormP.GroupCur=group;
			FormP.GroupItemList=groupItemList;
			FormP.ProcList=proclist;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK){
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void menuItemLabFee_Click(object sender,EventArgs e) {
			if(gridProg.SelectedIndices.Length<2 || gridProg.SelectedIndices.Length>3) {
				MsgBox.Show(this,"Please select two or three procedures, one regular and the other one or two lab.");
				return;
			}
			//One check that is not made is whether a lab proc is already attached to a different proc.
			DataRow row1=DataSetMain.Tables["ProgNotes"].Rows[gridProg.SelectedIndices[0]];
			DataRow row2=DataSetMain.Tables["ProgNotes"].Rows[gridProg.SelectedIndices[1]];
			DataRow row3=null;
			if(gridProg.SelectedIndices.Length==3) {
				row3=DataSetMain.Tables["ProgNotes"].Rows[gridProg.SelectedIndices[2]];
			}
			if(row1["ProcNum"].ToString()=="0" || row2["ProcNum"].ToString()=="0" || (row3!=null && row3["ProcNum"].ToString()=="0")) {
				MsgBox.Show(this,"All selected items must be procedures.");
				return;
			}
			List<long> procNumsReg=new List<long>();
			List<long> procNumsLab=new List<long>();
			if(ProcedureCodes.GetProcCode(row1["ProcCode"].ToString()).IsCanadianLab) {
				procNumsLab.Add(PIn.Long(row1["ProcNum"].ToString()));
			}
			else {
				procNumsReg.Add(PIn.Long(row1["ProcNum"].ToString()));
			}
			if(ProcedureCodes.GetProcCode(row2["ProcCode"].ToString()).IsCanadianLab) {
				procNumsLab.Add(PIn.Long(row2["ProcNum"].ToString()));
			}
			else {
				procNumsReg.Add(PIn.Long(row2["ProcNum"].ToString()));
			}
			if(row3!=null) {
				if(ProcedureCodes.GetProcCode(row3["ProcCode"].ToString()).IsCanadianLab) {
					procNumsLab.Add(PIn.Long(row3["ProcNum"].ToString()));
				}
				else {
					procNumsReg.Add(PIn.Long(row3["ProcNum"].ToString()));
				}
			}
			if(procNumsReg.Count==0) {
				MsgBox.Show(this,"One of the selected procedures must be a regular non-lab procedure as defined in Procedure Codes.");
				return;
			}
			if(procNumsReg.Count>1) {
				MsgBox.Show(this,"Only one of the selected procedures may be a regular non-lab procedure as defined in Procedure Codes.");
				return;
			}
			//We only alter the lab procedure(s), not the regular procedure.
			Procedure procLab;
			Procedure procOld;
			for(int i=0;i<procNumsLab.Count;i++) {
				procLab=Procedures.GetOneProc(procNumsLab[i],false);
				procOld=procLab.Copy();
				procLab.ProcNumLab=procNumsReg[0];
				Procedures.Update(procLab,procOld);
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void menuItemLabFeeDetach_Click(object sender,EventArgs e) {
			if(gridProg.SelectedIndices.Length!=1) {
				MsgBox.Show(this,"Please select exactly one lab procedure first.");
				return;
			}
			DataRow row=DataSetMain.Tables["ProgNotes"].Rows[gridProg.SelectedIndices[0]];
			if(row["ProcNum"].ToString()=="0") {
				MsgBox.Show(this,"Please select a lab procedure first.");
				return;
			}
			if(row["ProcNumLab"].ToString()=="0") {
				MsgBox.Show(this,"The selected procedure is not attached as a lab procedure.");
				return;
			}
			Procedure procLab=Procedures.GetOneProc(PIn.Long(row["ProcNum"].ToString()),false);
			Procedure procOld=procLab.Copy();
			procLab.ProcNumLab=0;
			Procedures.Update(procLab,procOld);
			ModuleSelected(PatCur.PatNum);
		}

		///<summary>Preview is only used for debugging.</summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			//pd2.DefaultPageSettings.Margins=new Margins(50,50,40,25);
			if(pd2.DefaultPageSettings.PaperSize.Height==0) {
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			try{
				if(justPreview){
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd2;
					pView.ShowDialog();				
			  }
				else{
					if(PrinterL.SetPrinter(pd2,PrintSituation.Default)){
						pd2.Print();
					}
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			#region printHeading
			if(!headingPrinted){
				text="Chart Progress Notes";
				g.DrawString(text,headingFont,Brushes.Black,400-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				text=PatCur.GetNameFL();
				g.DrawString(text,subHeadingFont,Brushes.Black,400-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text=DateTime.Today.ToShortDateString();
				g.DrawString(text,subHeadingFont,Brushes.Black,400-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=30;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			int totalPages=gridProg.GetNumberOfPages(bounds,headingPrintH);
			yPos=gridProg.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(pagesPrinted < totalPages){
				e.HasMorePages=true;
			}
			else{
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		///<summary>Preview is only used for debugging.</summary>
		public void PrintDay(bool justPreview) {
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPageDay);
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;
			if(pd2.DefaultPageSettings.PaperSize.Height==0) {
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			try {
				if(justPreview) {
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd2;
					pView.ShowDialog();
				}
				else {
					if(PrinterL.SetPrinter(pd2,PrintSituation.Default)) {
						pd2.Print();
					}
				}
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd2_PrintPageDay(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=new Rectangle(50,40,800,1020);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text="Chart Progress Notes";
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				//practice
				text=PrefC.GetString(PrefName.PracticeTitle);
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					DataRow row;
					long procNum;
					long clinicNum;
					for(int i=0;i<gridProg.Rows.Count;i++) {
						row=(DataRow)gridProg.Rows[i].Tag;
						procNum=PIn.Long(row["ProcNum"].ToString());
						if(procNum==0) {
							continue;
						}
						clinicNum=Procedures.GetClinicNum(procNum);
						if(clinicNum!=0) {//The first clinicNum that's encountered
							text=Clinics.GetDesc(clinicNum);
							break;
						}
					}
				}
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				//name
				text=PatCur.GetNameFL();
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text="Birthdate: "+PatCur.Birthdate.ToShortDateString();
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text="Printed: "+DateTime.Today.ToShortDateString();
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text="Ward: "+PatCur.Ward;
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				//Patient images are not shown when the A to Z folders are disabled.
				if(PrefC.UsingAtoZfolder){
					Bitmap picturePat;
					bool patientPictExists=Documents.GetPatPict(PatCur.PatNum,ImageStore.GetPatientFolder(PatCur),out picturePat);
					if(picturePat!=null){//Successfully loaded a patient picture?
						Bitmap thumbnail=ImageHelper.GetThumbnail(picturePat,80);
						g.DrawImage(thumbnail,center-40,yPos);
					}
					if(patientPictExists){
						yPos+=80;
					}
					yPos+=30;
					headingPrinted=true;
					headingPrintH=yPos;
				}
			}
			#endregion
			int totalPages=gridProg.GetNumberOfPages(bounds,headingPrintH);
			yPos=gridProg.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			
			pagesPrinted++;
			if(pagesPrinted < totalPages) {
				e.HasMorePages=true;
			}
			else {
				g.DrawString("Signature_________________________________________________________",
								subHeadingFont,Brushes.Black,160,yPos+20);
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		///<summary>Draws one button for the tabControlImages.</summary>
		private void OnDrawItem(object sender, DrawItemEventArgs e){
      Graphics g=e.Graphics;
      Pen penBlue=new Pen(Color.FromArgb(97,136,173));
			Pen penRed=new Pen(Color.FromArgb(140,51,46));
			Pen penOrange=new Pen(Color.FromArgb(250,176,3),2);
			Pen penDkOrange=new Pen(Color.FromArgb(227,119,4));
			SolidBrush brBlack=new SolidBrush(Color.Black);
			int selected=tabControlImages.TabPages.IndexOf(tabControlImages.SelectedTab);
			Rectangle bounds=e.Bounds;
			Rectangle rect=new Rectangle(bounds.X+2,bounds.Y+1,bounds.Width-5,bounds.Height-4);
			if(e.Index==selected){
				g.FillRectangle(new SolidBrush(Color.White),rect);
				//g.DrawRectangle(penBlue,rect);
				g.DrawLine(penOrange,rect.X,rect.Bottom-1,rect.Right,rect.Bottom-1);
				g.DrawLine(penDkOrange,rect.X+1,rect.Bottom,rect.Right-2,rect.Bottom);
				g.DrawString(tabControlImages.TabPages[e.Index].Text,Font,brBlack,bounds.X+3,bounds.Y+6);
			}
			else{
				g.DrawString(tabControlImages.TabPages[e.Index].Text,Font,brBlack,bounds.X,bounds.Y);
			}
    }

		private void panelImages_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Y>3){
				return;
			}
			MouseIsDownOnImageSplitter=true;
			ImageSplitterOriginalY=panelImages.Top;
			OriginalImageMousePos=panelImages.Top+e.Y;
		}

		private void panelImages_MouseLeave(object sender, System.EventArgs e) {
			//not needed.
		}

		private void panelImages_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnImageSplitter){
				if(e.Y<=3){
					panelImages.Cursor=Cursors.HSplit;
				}
				else{
					panelImages.Cursor=Cursors.Default;
				}
				return;
			}
			//panelNewTop
			int panelNewH=panelImages.Bottom
				-(ImageSplitterOriginalY+(panelImages.Top+e.Y)-OriginalImageMousePos);//-top
			if(panelNewH<10)//cTeeth.Bottom)
				panelNewH=10;//cTeeth.Bottom;//keeps it from going too low
			if(panelNewH>panelImages.Bottom-toothChart.Bottom)
				panelNewH=panelImages.Bottom-toothChart.Bottom;//keeps it from going too high
			panelImages.Height=panelNewH;
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				if(panelImages.Visible) {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1
						-(panelImages.Height+2);
				}
				else {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1;
				}
			}
		}

		private void panelImages_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnImageSplitter){
				return;
			}
			MouseIsDownOnImageSplitter=false;
		}

		private void tabProc_MouseDown(object sender,MouseEventArgs e) {
			//selected tab will have changed, so we need to test the original selected tab:
			Rectangle rect=tabProc.GetTabRect(SelectedProcTab);
			if(rect.Contains(e.X,e.Y) && tabProc.Height>27){//clicked on the already selected tab which was maximized
				tabProc.Height=27;
				tabProc.Refresh();
				gridProg.Location=new Point(tabProc.Left,tabProc.Bottom+1);
				gridProg.Height=this.ClientSize.Height-gridProg.Location.Y-2;
			}
			else if(tabProc.Height==27){//clicked on a minimized tab
				tabProc.Height=259;
				tabProc.Refresh();
				gridProg.Location=new Point(tabProc.Left,tabProc.Bottom+1);
				gridProg.Height=this.ClientSize.Height-gridProg.Location.Y-2;
			}
			else{//clicked on a new tab
				//height will have already been set, so do nothing
			}
			SelectedProcTab=tabProc.SelectedIndex;
			FillMovementsAndHidden();
		}

		private void tabControlImages_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(selectedImageTab==-1){
				selectedImageTab=tabControlImages.SelectedIndex;
				return;
			}
			Rectangle rect=tabControlImages.GetTabRect(selectedImageTab);
			if(rect.Contains(e.X,e.Y)){//clicked on the already selected tab
				if(panelImages.Visible){
					panelImages.Visible=false;
				}
				else{
					panelImages.Visible=true;
				}
			}
			else{//clicked on a new tab
				if(!panelImages.Visible){
					panelImages.Visible=true;
				}
			}
			selectedImageTab=tabControlImages.SelectedIndex;
			FillImages();//it will not actually fill the images unless panelImages is visible
			if(Programs.IsEnabled("eClinicalWorks") && ProgramProperties.GetPropVal("eClinicalWorks","IsStandalone")=="0") {
				if(panelImages.Visible) {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1-(panelImages.Height+2);
				}
				else {
					panelEcw.Height=tabControlImages.Top-panelEcw.Top+1;
				}
			}
		}

		private void listViewImages_DoubleClick(object sender, System.EventArgs e) {
			if(listViewImages.SelectedIndices.Count==0){
				return;//clicked on white space.
			}
			Document DocCur=DocumentList[(int)visImages[listViewImages.SelectedIndices[0]]];
			if(!ImageHelper.HasImageExtension(DocCur.FileName)){
				try{
					Process.Start(ODFileUtils.CombinePaths(patFolder,DocCur.FileName));
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
				return;
			}
			if(formImageViewer==null || !formImageViewer.Visible){
				formImageViewer=new FormImageViewer();
				formImageViewer.Show();
			}
			if(formImageViewer.WindowState==FormWindowState.Minimized){
				formImageViewer.WindowState=FormWindowState.Normal;
			}
			formImageViewer.BringToFront();
			formImageViewer.SetImage(DocCur,PatCur.GetNameLF()+" - "
				+DocCur.DateCreated.ToShortDateString()+": "+DocCur.Description);
		}

		private void gridChartViews_CellClick(object sender,ODGridClickEventArgs e) {
			SetChartView(ChartViews.Listt[e.Row]);
		}

		private void gridChartViews_DoubleClick(object sender,ODGridClickEventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			int count=gridChartViews.Rows.Count;
			FormChartView FormC=new FormChartView(); 
			FormC.ChartViewCur=ChartViews.Listt[e.Row];
			FormC.ShowDialog();
			FillChartViewsGrid();
			if(gridChartViews.Rows.Count==0) {
				FillProgNotes();
				return;//deleted last view, so display default
			}
			if(gridChartViews.Rows.Count==count) {
				gridChartViews.SetSelected(FormC.ChartViewCur.ItemOrder,true);
				SetChartView(ChartViews.Listt[FormC.ChartViewCur.ItemOrder]);
			}
			else if(gridChartViews.Rows.Count>0){
				for(int i=0;i<ChartViews.Listt.Count;i++) {
					ChartViews.Listt[i].ItemOrder=i;
					ChartViews.Update(ChartViews.Listt[i]);
				}
				if(FormC.ChartViewCur.ItemOrder!=0) {
					gridChartViews.SetSelected(FormC.ChartViewCur.ItemOrder-1,true);
					SetChartView(ChartViews.Listt[FormC.ChartViewCur.ItemOrder-1]);
				}
				else {
					gridChartViews.SetSelected(0,true);
					SetChartView(ChartViews.Listt[0]);
				}
			}
		}

		private void butChartViewAdd_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			int count=gridChartViews.Rows.Count;
			int selectedIndex=gridChartViews.GetSelectedIndex();
			FormChartView FormChartAdd=new FormChartView();
			FormChartAdd.ChartViewCur=new ChartView();
			FormChartAdd.ChartViewCur.IsNew=true;
			FormChartAdd.ShowDialog();
			FillChartViewsGrid();
			int count2=gridChartViews.Rows.Count;
			if(count2==0) { 
				return; 
			}
			if(count2==count) {
				gridChartViews.SetSelected(selectedIndex,true);
				SetChartView(ChartViews.Listt[selectedIndex]);
			}
			else {
				FormChartAdd.ChartViewCur.ItemOrder=count;
				ChartViews.Update(FormChartAdd.ChartViewCur);
				FillChartViewsGrid();
				SetChartView(ChartViews.Listt[count]);
				gridChartViews.SetSelected(count,true);
			}
		}

		private void butChartViewUp_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			if(gridChartViews.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a view first.");
				return;
			}
			int oldIdx;
			int newIdx;
			ChartView oldItem;
			ChartView newItem;
			if(gridChartViews.GetSelectedIndex()!=-1) {
				oldIdx=gridChartViews.GetSelectedIndex();
				if(oldIdx==0) {
					return;//can't move up any more
				}
				newIdx=oldIdx-1; 
				for(int i=0;i<ChartViews.Listt.Count;i++) {
					if(ChartViews.Listt[i].ItemOrder==oldIdx) {
						oldItem=ChartViews.Listt[i];
						newItem=ChartViews.Listt[newIdx];
						oldItem.ItemOrder=newItem.ItemOrder;
						newItem.ItemOrder+=1;
						ChartViews.Update(oldItem);
						ChartViews.Update(newItem);
					}
				}
				FillChartViewsGrid();
				gridChartViews.SetSelected(newIdx,true);
				SetChartView(ChartViews.Listt[newIdx]);
			}
		}

		private void butChartViewDown_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			if(gridChartViews.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a view first.");
				return;
			}
			int oldIdx;
			int newIdx;
			ChartView oldItem;
			ChartView newItem;
			if(gridChartViews.GetSelectedIndex()!=-1) {
				oldIdx=gridChartViews.GetSelectedIndex();
				if(oldIdx==ChartViews.Listt.Count-1) {
					return;//can't move down any more
				}
				newIdx=oldIdx+1;
				for(int i=0;i<ChartViews.Listt.Count;i++) {
					if(ChartViews.Listt[i].ItemOrder==newIdx) {
						newItem=ChartViews.Listt[i];
						oldItem=ChartViews.Listt[oldIdx];
						newItem.ItemOrder=oldItem.ItemOrder;
						oldItem.ItemOrder+=1;
						ChartViews.Update(oldItem);
						ChartViews.Update(newItem);
					}
				}
				FillChartViewsGrid();
				gridChartViews.SetSelected(newIdx,true);
				SetChartView(ChartViews.Listt[newIdx]);
			}
		}

		public void FunctionKeyPressContrChart(Keys keys) {
			switch(keys) {
				case Keys.F1: 
					if(gridChartViews.Rows.Count>0) {
						gridChartViews.SetSelected(0,true);
						SetChartView(ChartViews.Listt[0]);
					}
					break;
				case Keys.F2:
					if(gridChartViews.Rows.Count>1) {
						gridChartViews.SetSelected(1,true);
						SetChartView(ChartViews.Listt[1]);
					}
					break;
				case Keys.F3:
					if(gridChartViews.Rows.Count>2) {
						gridChartViews.SetSelected(2,true);
						SetChartView(ChartViews.Listt[2]);
					}
					break;
				case Keys.F4:
					if(gridChartViews.Rows.Count>3) {
						gridChartViews.SetSelected(3,true);
						SetChartView(ChartViews.Listt[3]);
					}
					break;
				case Keys.F5:
					if(gridChartViews.Rows.Count>4) {
						gridChartViews.SetSelected(4,true);
						SetChartView(ChartViews.Listt[4]);
					}
					break;
				case Keys.F6:
					if(gridChartViews.Rows.Count>5) {
						gridChartViews.SetSelected(5,true);
						SetChartView(ChartViews.Listt[5]);
					}
					break;
				case Keys.F7:
					if(gridChartViews.Rows.Count>6) {
						gridChartViews.SetSelected(6,true);
						SetChartView(ChartViews.Listt[6]);
					}
					break;
				case Keys.F8:
					if(gridChartViews.Rows.Count>7) {
						gridChartViews.SetSelected(7,true);
						SetChartView(ChartViews.Listt[7]);
					}
					break;
				case Keys.F9:
					if(gridChartViews.Rows.Count>8) {
						gridChartViews.SetSelected(8,true);
						SetChartView(ChartViews.Listt[8]);
					}
					break;
				case Keys.F10:
					if(gridChartViews.Rows.Count>9) {
						gridChartViews.SetSelected(9,true);
						SetChartView(ChartViews.Listt[9]);
					}
					break;
				case Keys.F11:
					if(gridChartViews.Rows.Count>10) {
						gridChartViews.SetSelected(10,true);
						SetChartView(ChartViews.Listt[10]);
					}
					break;
				case Keys.F12:
					if(gridChartViews.Rows.Count>11) {
						gridChartViews.SetSelected(11,true);
						SetChartView(ChartViews.Listt[11]);
					}
					break;
			}
		}

		private void butBig_Click(object sender,EventArgs e) {
			
		}

		private void butECWup_Click(object sender,EventArgs e) {
			panelEcw.Location=toothChart.Location;
			if(panelImages.Visible) {
				panelEcw.Height=tabControlImages.Top-panelEcw.Top+1-(panelImages.Height+2);
			}
			else {
				panelEcw.Height=tabControlImages.Top-panelEcw.Top+1;
			}
			butECWdown.Visible=true;
			butECWup.Visible=false;
		}

		private void butECWdown_Click(object sender,EventArgs e) {
			panelEcw.Location=new Point(524+2,textTreatmentNotes.Bottom+1);
			if(panelImages.Visible) {
				panelEcw.Height=tabControlImages.Top-panelEcw.Top+1-(panelImages.Height+2);
			}
			else {
				panelEcw.Height=tabControlImages.Top-panelEcw.Top+1;
			}
			butECWdown.Visible=false;
			butECWup.Visible=true;
		}

		#region Quick Buttons
		private void panelQuickButtons_Paint(object sender,PaintEventArgs e) {

		}
		
		private void buttonCDO_Click(object sender,EventArgs e) {
			textSurf.Text = "DO";
			ProcButtonClicked(0,"D2392");
		}

		private void buttonCMOD_Click(object sender,EventArgs e) {
			textSurf.Text = "MOD";
			ProcButtonClicked(0,"D2393");
		}

		private void buttonCO_Click(object sender,EventArgs e) {
			textSurf.Text = "O";
			ProcButtonClicked(0,"D2391");
		}

		private void buttonCMO_Click(object sender,EventArgs e) {
			textSurf.Text = "MO";
			ProcButtonClicked(0,"D2392");
		}

		private void butCOL_Click(object sender,EventArgs e) {
			textSurf.Text = "OL";
			ProcButtonClicked(0,"D2392");
		}

		private void butCOB_Click(object sender,EventArgs e) {
			textSurf.Text = "OB";
			ProcButtonClicked(0,"D2392");
		}

		private void butDL_Click(object sender,EventArgs e) {
			textSurf.Text = "DL";
			ProcButtonClicked(0,"D2331");
		}

		private void butML_Click(object sender,EventArgs e) {
			textSurf.Text = "ML";
			ProcButtonClicked(0,"D2331");
		}

		private void buttonCSeal_Click(object sender,EventArgs e) {
			textSurf.Text = "";
			ProcButtonClicked(0,"D1351");
		}

		private void buttonADO_Click(object sender,EventArgs e) {
			textSurf.Text = "DO";
			ProcButtonClicked(0,"D2150");
		}

		private void buttonAMOD_Click(object sender,EventArgs e) {
			textSurf.Text = "MOD";
			ProcButtonClicked(0,"D2160");
		}

		private void buttonAO_Click(object sender,EventArgs e) {
			textSurf.Text = "O";
			ProcButtonClicked(0,"D2140");
		}

		private void buttonAMO_Click(object sender,EventArgs e) {
			textSurf.Text = "MO";
			ProcButtonClicked(0,"D2150");
		}

		private void butCMDL_Click(object sender,EventArgs e) {
			textSurf.Text = "MDL";
			ProcButtonClicked(0,"D2332");
		}

		private void buttonAOL_Click(object sender, EventArgs e){
			textSurf.Text = "OL";
			ProcButtonClicked(0, "D2150");
		}

		private void buttonAOB_Click(object sender, EventArgs e){
			textSurf.Text = "OB";
			ProcButtonClicked(0, "D2150");
		}

		private void buttonAMODL_Click(object sender, EventArgs e){
			textSurf.Text = "MODL";
			ProcButtonClicked(0, "D2161");
		}

		private void buttonAMODB_Click(object sender, EventArgs e){
			textSurf.Text = "MODB";
			ProcButtonClicked(0, "D2161");
		}

		private void buttonCMODL_Click(object sender, EventArgs e){
			textSurf.Text = "MODL";
			ProcButtonClicked(0, "D2394");
		}

		private void buttonCMODB_Click(object sender, EventArgs e){
			textSurf.Text = "MODB";
			ProcButtonClicked(0, "D2394");
		}
		#endregion Quick Buttons

		private void butAddKey_Click(object sender,EventArgs e) {
			RegistrationKey key=new RegistrationKey();
			key.PatNum=PatCur.PatNum;
			//Notes are not commonly necessary, because most customers have only one office (thus only 1 key is necessary).
			//A tech can edit the note later after it is added if necessary.
			key.Note="";
			key.DateStarted=DateTime.Today;
			key.IsForeign=false;
			key.VotesAllotted=100;
			RegistrationKeys.Insert(key);
			FillPtInfo();//Refresh registration key list in patient info grid.
		}

		private void butForeignKey_Click(object sender,EventArgs e) {
			RegistrationKey key=new RegistrationKey();
			key.PatNum=PatCur.PatNum;
			key.Note="";
			key.DateStarted=DateTime.Today;
			key.IsForeign=true;
			key.VotesAllotted=100;
			RegistrationKeys.Insert(key);
			FillPtInfo();
		}

		private void butLoadDirectX_Click(object sender,EventArgs e) {
			//toothChart.LoadDirectX();
		}

		

		


		

		


		

		

		

		

		

		

		

		

		

		

		

		

	

		

		


		

		

		

		

		

		

		#region VisiQuick integration code written by Thomas Jensen tje@thomsystems.com 
		/*
		private void XrayLinkBtn_Click(object sender, System.EventArgs e)	// TJE
		{
			if (!Patients.PatIsLoaded || Patients.Cur.PatNum<1)
				return;
			VQLink.VQStart(false,"",0,0);
		}

		private void SetPanelCol(Panel p, char c)	// TJE
		{
			if (c != '0')
				p.BackColor=SystemColors.ActiveCaption;
			else
				p.BackColor=SystemColors.ActiveBorder;
		}

		private void VQUpdatePatient()	// TJE
		{
			String	s;
			if (!Patients.PatIsLoaded || Patients.Cur.PatNum<1)	
				s="";
			else
				s=VQLink.SearchTStatus(Patients.Cur.PatNum);
			if (s.Length>=32) 
			{
				SetPanelCol(tooth11,s[0]);
				SetPanelCol(tooth12,s[1]);
				SetPanelCol(tooth13,s[2]);
				SetPanelCol(tooth14,s[3]);
				SetPanelCol(tooth15,s[4]);
				SetPanelCol(tooth16,s[5]);
				SetPanelCol(tooth17,s[6]);
				SetPanelCol(tooth18,s[7]);
				SetPanelCol(tooth21,s[8]);
				SetPanelCol(tooth22,s[9]);
				SetPanelCol(tooth23,s[10]);
				SetPanelCol(tooth24,s[11]);
				SetPanelCol(tooth25,s[12]);
				SetPanelCol(tooth26,s[13]);
				SetPanelCol(tooth27,s[14]);
				SetPanelCol(tooth28,s[15]);
				SetPanelCol(tooth31,s[16]);
				SetPanelCol(tooth32,s[17]);
				SetPanelCol(tooth33,s[18]);
				SetPanelCol(tooth34,s[19]);
				SetPanelCol(tooth35,s[20]);
				SetPanelCol(tooth36,s[21]);
				SetPanelCol(tooth37,s[22]);
				SetPanelCol(tooth38,s[23]);
				SetPanelCol(tooth41,s[24]);
				SetPanelCol(tooth42,s[25]);
				SetPanelCol(tooth43,s[26]);
				SetPanelCol(tooth44,s[27]);
				SetPanelCol(tooth45,s[28]);
				SetPanelCol(tooth46,s[29]);
				SetPanelCol(tooth47,s[30]);
				SetPanelCol(tooth48,s[31]);
			}
			if (s.Length>=32+6) 
			{
				SetPanelCol(toothpanos,s[32]);
				SetPanelCol(toothcephs,s[33]);
				if (s[34]!='0' | s[35]!='0' | s[36]!='0' | s[37]!='0') 
				{
					SetPanelCol(toothbw,'1');
					SetPanelCol(toothbwfloat,'1');
				}
				else
				{
					SetPanelCol(toothbw,'0');
					SetPanelCol(toothbwfloat,'0');
				}
			}
			if (s.Length>=32+6+9) 
			{
				if (s[39]!='0' | s[40]!='0' | s[41]!='0' | s[43]!='0') 
					SetPanelCol(toothcolors,'1');
				else
					SetPanelCol(toothcolors,'0');
				SetPanelCol(toothxrays,s[42]);
				SetPanelCol(toothpanos,s[44]);
				SetPanelCol(toothcephs,s[45]);
				SetPanelCol(toothdocs,s[46]);
			}
			if (s.Length>=32+6+9+1) 
			{
				SetPanelCol(toothfiles,s[47]);
			}
		}

		private void tooth18_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos(((Panel)sender).Name.Substring(5,2),VisiQuick.spf_tinymode+VisiQuick.spf_single,0);	
		}

		private void toothbwfloat_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_tinymode+VisiQuick.spf_2horizontal,VisiQuick.spi_bitewings);
		}

		private void toothbw_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.npi_xrayview,VisiQuick.spi_bitewings);
		}

		private void toothxrays_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.VQStart(false,"",0,VisiQuick.npi_xrayview);
		}

		private void toothcolors_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.VQStart(false,"",0,VisiQuick.npi_colorview);
		}

		private void toothpanos_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_panview);
		}

		private void toothcephs_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_cephview);
		}

		private void toothdocs_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_docview);
		}

		private void toothfiles_Click(object sender, System.EventArgs e)	// TJE
		{
			VQLink.SearchPhotos("",VisiQuick.spf_single,VisiQuick.spi_fileview);
		}
		*/
		#endregion
	}//end class


	


}
