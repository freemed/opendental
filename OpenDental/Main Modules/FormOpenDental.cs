/*=============================================================================================================
Open Dental is a dental practice management program.
Copyright (C) 2003,2004,2005,2006,2007  Jordan Sparks, DMD.  http://www.open-dent.com,  http://www.docsparks.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU Db Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU Db Public License
for more details, available at http://www.opensource.org/licenses/gpl-license.php

Any changes to this program must follow the guidelines of the GPL license if a modified version is to be
redistributed.
===============================================================================================================*/
//For now, all screens are assumed to have available 990x734.  That would be a screen resolution of 1024x768 with a single width taskbar docked to any one of the four sides of the screen.  Screens can be optimized to the larger 1280x1024 (1246x990 to allow for docked taskbar) as long as scrollbars appear when user is at 1024x768.  This can be visualized for design purposes by placing hash lines radiating from 982,700.

//The 7 main controls are slightly smaller due to menu bar on left of 51 and the toolbars on the top of 29. Max size 939x679, or the larger 1195x935 as long as it still functions acceptably at 939x679.

//#define ORA_DB
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Media;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using SparksToothChart;
//using OpenDental.SmartCards;
using OpenDental.UI;
#if EHRTEST
using EHR;
#endif

//#if(ORA_DB)
//using OD_CRYPTO;
//#endif

namespace OpenDental{
	///<summary></summary>
	public class FormOpenDental:System.Windows.Forms.Form {
		private System.ComponentModel.IContainer components;
		//private bool[,] buttonDown=new bool[2,6];
		private System.Windows.Forms.Timer timerTimeIndic;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItemReports;
		private System.Windows.Forms.MenuItem menuItemPrinter;
		private System.Windows.Forms.MenuItem menuItemImaging;
		private System.Windows.Forms.MenuItem menuItemDataPath;
		private System.Windows.Forms.MenuItem menuItemConfig;
		private System.Windows.Forms.MenuItem menuItemAutoCodes;
		private System.Windows.Forms.MenuItem menuItemDefinitions;
		private System.Windows.Forms.MenuItem menuItemInsCats;
		private System.Windows.Forms.MenuItem menuItemLinks;
		private System.Windows.Forms.MenuItem menuItemRecall;
		private System.Windows.Forms.MenuItem menuItemEmployees;
		private System.Windows.Forms.MenuItem menuItemPractice;
		private System.Windows.Forms.MenuItem menuItemPrescriptions;
		private System.Windows.Forms.MenuItem menuItemProviders;
		private System.Windows.Forms.MenuItem menuItemProcCodes;
		private System.Windows.Forms.MenuItem menuItemPrintScreen;
		private System.Windows.Forms.MenuItem menuItemFinanceCharge;
		private System.Windows.Forms.MenuItem menuItemAging;
		private System.Windows.Forms.MenuItem menuItemSched;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItemTranslation;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItemLists;
		private System.Windows.Forms.MenuItem menuItemTools;
		private System.Windows.Forms.MenuItem menuItemReferrals;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemDatabaseMaintenance;
		private System.Windows.Forms.MenuItem menuItemProcedureButtons;
		private System.Windows.Forms.MenuItem menuItemZipCodes;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuTelephone;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItemHelpIndex;
		private System.Windows.Forms.MenuItem menuItemClaimForms;
		private System.Windows.Forms.MenuItem menuItemContacts;
		private System.Windows.Forms.MenuItem menuItemMedications;
		private System.Windows.Forms.ImageList imageList32;
		private System.Windows.Forms.MenuItem menuItemApptViews;
		private System.Windows.Forms.MenuItem menuItemComputers;
		private System.Windows.Forms.MenuItem menuItemEmployers;
		private System.Windows.Forms.MenuItem menuItemEasy;
		private System.Windows.Forms.MenuItem menuItemCarriers;
		private System.Windows.Forms.MenuItem menuItemSchools;
		private System.Windows.Forms.MenuItem menuItemCounties;
		private System.Windows.Forms.MenuItem menuItemScreening;
		private System.Windows.Forms.MenuItem menuItemEmail;
		private System.Windows.Forms.MenuItem menuItemHelpContents;
		private System.Windows.Forms.MenuItem menuItemHelp;
		///<summary>The only reason this is public static is so that it can be seen from the terminal manager.  Otherwise, it's passed around properly.  I also used it in UserControlPhonePanel for simplicity</summary>
		public static long CurPatNum;
		private System.Windows.Forms.MenuItem menuItemClearinghouses;
		private System.Windows.Forms.MenuItem menuItemUpdate;
		private System.Windows.Forms.MenuItem menuItemHelpWindows;
		private System.Windows.Forms.MenuItem menuItemMisc;
		private System.Windows.Forms.MenuItem menuItemRemote;
		private System.Windows.Forms.MenuItem menuItemSchoolClass;
		private System.Windows.Forms.MenuItem menuItemSchoolCourses;
		private System.Windows.Forms.MenuItem menuItemSecurity;
		private System.Windows.Forms.MenuItem menuItemLogOff;
		private System.Windows.Forms.MenuItem menuItemInsPlans;
		private System.Windows.Forms.MenuItem menuItemClinics;
		private System.Windows.Forms.MenuItem menuItemOperatories;
		private System.Windows.Forms.Timer timerSignals;
		///<summary>When user logs out, this keeps track of where they were for when they log back in.</summary>
		private int LastModule;
		private System.Windows.Forms.MenuItem menuItemRepeatingCharges;
		private System.Windows.Forms.MenuItem menuItemImportXML;
		private MenuItem menuItemTimeCards;
		private MenuItem menuItemApptRules;
		private MenuItem menuItemAuditTrail;
		private MenuItem menuItemPatFieldDefs;
		private MenuItem menuItemProblems;
		private MenuItem menuItemTerminal;
		private MenuItem menuItemTerminalManager;
		private MenuItem menuItemQuestions;
		private MenuItem menuItemCustomReports;
		private MenuItem menuItemMessaging;
		private OpenDental.UI.LightSignalGrid lightSignalGrid1;
		private MenuItem menuItemMessagingButs;
		///<summary>This is not the actual date/time last refreshed.  It is really the date/time of the last item in the database retrieved on previous refreshes.  That way, the local workstation time is irrelevant.</summary>
		public static DateTime signalLastRefreshed;
		private FormSplash Splash;
		private Bitmap bitmapIcon;
		private MenuItem menuItemCreateAtoZFolders;
		private MenuItem menuItemLaboratories;
		///<summary>A list of button definitions for this computer.</summary>
		private SigButDef[] SigButDefList;
		/// <summary>Added these 3 fields for Oracle encrypted connection string</summary>
		private string connStr;
		private string key;
		private MenuItem menuItemGraphics;
		private MenuItem menuItemLabCases;
		private MenuItem menuItemRequirementsNeeded;
		private MenuItem menuItemReqStudents;
		private MenuItem menuItemAutoNotes;
		private MenuItem menuItemDisplayFields;
		private Panel panelSplitter;
		//private string dconnStr;
		private bool MouseIsDownOnSplitter;
		private Point SplitterOriginalLocation;
		private ContextMenu menuSplitter;
		private MenuItem menuItemDockBottom;
		private MenuItem menuItemDockRight;
		private ImageList imageListMain;
		private ContextMenu menuPatient;
		private ContextMenu menuLabel;
		private ContextMenu menuEmail;
		private ContextMenu menuLetter;
		private Point OriginalMousePos;
		private MenuItem menuItemCustomerManage;
		private System.Windows.Forms.Timer timerDisabledKey;
		///<summary>This list will only contain events for this computer where the users clicked to disable a popup for a specified period of time.  So it won't typically have many items in it.</summary>
		private List<PopupEvent> PopupEventList;
		private MenuItem menuItemPharmacies;
		private MenuItem menuItemSheets;
		private MenuItem menuItemRequestFeatures;
		private MenuItem menuItemModules;
		private MenuItem menuItemRecallTypes;
		private MenuItem menuItemFeeScheds;
		private MenuItem menuItemMobileSetup;
		private MenuItem menuItemLetters;
		//private UserControlPhonePanel phonePanel;
		///<summary>Command line args passed in when program starts.</summary>
		public string[] CommandLineArgs;
		private Thread ThreadCommandLine;
		///<summary>Listens for commands coming from other instances of Open Dental on this computer.</summary>
		private TcpListener TcpListenerCommandLine;
		///<summary>True if there is already a different instance of OD running.  This prevents attempting to start the listener.</summary>
		public bool IsSecondInstance;
		private UserControlTasks userControlTasks1;
		private ContrAppt ContrAppt2;
		private ContrFamily ContrFamily2;
		private ContrFamilyEcw ContrFamily2Ecw;
		private ContrAccount ContrAccount2;
		private ContrTreat ContrTreat2;
		private ContrChart ContrChart2;
		private ContrImages ContrImages2;
		private ContrStaff ContrManage2;
		private OutlookBar myOutlookBar;
		private MenuItem menuItemShutdown;
		private System.Windows.Forms.Timer timerHeartBeat;
		private MenuItem menuItemInsFilingCodes;
		private MenuItem menuItemReplication;
		private MenuItem menuItemAutomation;
		private MenuItem menuItemMergePatients;
		private MenuItem menuItemDuplicateBlockouts;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private MenuItem menuItemPassword;
		private MenuItem menuItem3;
		private MenuItem menuApptFieldDefs;
		private MenuItem menuItemWebForms;
		private System.Windows.Forms.Timer timerPhoneWebCam;
		private FormTerminalManager formTerminalManager;
		private FormPhoneTiles formPhoneTiles;
		private System.Windows.Forms.Timer timerWebHostSynch;
		private MenuItem menuItemCCRecurring;
		private UserControlPhoneSmall phoneSmall;
		///<summary>This will be null if EHR didn't load up.</summary>
		public static object FormEHR;
		private MenuItem menuItemEHR;
		private System.Windows.Forms.Timer timerLogoff;
		//<summary>This will be null if EHR didn't load up.</summary>
		public static Assembly AssemblyEHR;
		///<summary>The last local datetime that there was any mouse or keyboard activity.  Used for auto logoff comparison.</summary>
		private DateTime dateTimeLastActivity;
		private Form FormRecentlyOpenForLogoff;
		private Point locationMouseForLogoff;
		private MenuItem menuItemPayerIDs;
		private MenuItem menuItemTestLatency;
		private FormLogOn FormLogOn_;
		private System.Windows.Forms.Timer timerReplicationMonitor;
		///<summary>When auto log off is in use, we don't want to log off user if they are in the FormLogOn window.  Mostly a problem when using web service because CurUser is not null.</summary>
		private bool IsFormLogOnLastActive;
		private Panel panelPhoneSmall;
		private UI.Button butTriage;
		private UI.Button butBigPhones;
		private Label labelWaitTime;
		private Label labelTriage;
		private Label labelMsg;
		///<summary>This thread fills labelMsg</summary>
		private Thread ThreadVM;
		///<summary>Holds the time for the oldest triage task.</summary>
		private DateTime triageTime;

		///<summary></summary>
		public FormOpenDental(string[] cla){
			Logger.openlog.Log("Initializing Open Dental...",Logger.Severity.INFO);
			CommandLineArgs=cla;
			Splash=new FormSplash();
			if(CommandLineArgs.Length==0) {
				Splash.Show();
			}
			InitializeComponent();
			SystemEvents.SessionSwitch+=new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
			//toolbar
			ToolBarMain=new ODToolBar();
			ToolBarMain.Location=new Point(51,0);
			ToolBarMain.Size=new Size(931,25);
			ToolBarMain.Dock=DockStyle.Top;
			ToolBarMain.ImageList=imageListMain;
			ToolBarMain.ButtonClick+=new ODToolBarButtonClickEventHandler(ToolBarMain_ButtonClick);
			this.Controls.Add(ToolBarMain);
			//outlook bar
			myOutlookBar=new OutlookBar();
			myOutlookBar.Location=new Point(0,0);
			myOutlookBar.Size=new Size(51,626);
			myOutlookBar.ImageList=imageList32;
			myOutlookBar.Dock=DockStyle.Left;
			myOutlookBar.ButtonClicked+=new ButtonClickedEventHandler(myOutlookBar_ButtonClicked);
			this.Controls.Add(myOutlookBar);
			//contrAppt
			ContrAppt2=new ContrAppt();
			ContrAppt2.Visible=false;
			ContrAppt2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrAppt2);
			//contrFamily
			ContrFamily2=new ContrFamily();
			ContrFamily2.Visible=false;
			ContrFamily2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrFamily2);
			//contrFamilyEcw
			ContrFamily2Ecw=new ContrFamilyEcw();
			ContrFamily2Ecw.Visible=false;
			//ContrFamily2Ecw.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrFamily2Ecw);
			//contrAccount
			ContrAccount2=new ContrAccount();
			ContrAccount2.Visible=false;
			ContrAccount2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrAccount2);
			//contrTreat
			ContrTreat2=new ContrTreat();
			ContrTreat2.Visible=false;
			ContrTreat2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrTreat2);
			//contrChart
			ContrChart2=new ContrChart();
			ContrChart2.Visible=false;
			ContrChart2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrChart2);
			//contrImages
			ContrImages2=new ContrImages();
			ContrImages2.Visible=false;
			ContrImages2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrImages2);
			//contrManage
			ContrManage2=new ContrStaff();
			ContrManage2.Visible=false;
			ContrManage2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrManage2);
			//userControlTasks
			userControlTasks1=new UserControlTasks();
			userControlTasks1.Visible=false;
			userControlTasks1.GoToChanged+=new EventHandler(userControlTasks1_GoToChanged);
			GotoModule.ModuleSelected+=new ModuleEventHandler(GotoModule_ModuleSelected);
			this.Controls.Add(userControlTasks1);
			panelSplitter.ContextMenu=menuSplitter;
			menuItemDockBottom.Checked=true;
			phoneSmall=new UserControlPhoneSmall();
			phoneSmall.GoToChanged += new System.EventHandler(this.phoneSmall_GoToChanged);
			//phoneSmall.Visible=false;
			//this.Controls.Add(phoneSmall);
			panelPhoneSmall.Controls.Add(phoneSmall);
			panelPhoneSmall.Visible=false;
			//phonePanel=new UserControlPhonePanel();
			//phonePanel.Visible=false;
			//this.Controls.Add(phonePanel);
			//phonePanel.GoToChanged += new System.EventHandler(this.phonePanel_GoToChanged);
			Logger.openlog.Log("Open Dental initialization complete.",Logger.Severity.INFO);
			//Plugins.HookAddCode(this,"FormOpenDental.Constructor_end");//Can't do this because no plugins loaded.
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOpenDental));
			this.timerTimeIndic = new System.Windows.Forms.Timer(this.components);
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemLogOff = new System.Windows.Forms.MenuItem();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemPassword = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemPrinter = new System.Windows.Forms.MenuItem();
			this.menuItemGraphics = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemConfig = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.menuApptFieldDefs = new System.Windows.Forms.MenuItem();
			this.menuItemApptRules = new System.Windows.Forms.MenuItem();
			this.menuItemApptViews = new System.Windows.Forms.MenuItem();
			this.menuItemAutoCodes = new System.Windows.Forms.MenuItem();
			this.menuItemAutomation = new System.Windows.Forms.MenuItem();
			this.menuItemAutoNotes = new System.Windows.Forms.MenuItem();
			this.menuItemClaimForms = new System.Windows.Forms.MenuItem();
			this.menuItemClearinghouses = new System.Windows.Forms.MenuItem();
			this.menuItemComputers = new System.Windows.Forms.MenuItem();
			this.menuItemDataPath = new System.Windows.Forms.MenuItem();
			this.menuItemDefinitions = new System.Windows.Forms.MenuItem();
			this.menuItemDisplayFields = new System.Windows.Forms.MenuItem();
			this.menuItemEmail = new System.Windows.Forms.MenuItem();
			this.menuItemEHR = new System.Windows.Forms.MenuItem();
			this.menuItemFeeScheds = new System.Windows.Forms.MenuItem();
			this.menuItemImaging = new System.Windows.Forms.MenuItem();
			this.menuItemInsCats = new System.Windows.Forms.MenuItem();
			this.menuItemInsFilingCodes = new System.Windows.Forms.MenuItem();
			this.menuItemLaboratories = new System.Windows.Forms.MenuItem();
			this.menuItemLetters = new System.Windows.Forms.MenuItem();
			this.menuItemMessaging = new System.Windows.Forms.MenuItem();
			this.menuItemMessagingButs = new System.Windows.Forms.MenuItem();
			this.menuItemMisc = new System.Windows.Forms.MenuItem();
			this.menuItemModules = new System.Windows.Forms.MenuItem();
			this.menuItemOperatories = new System.Windows.Forms.MenuItem();
			this.menuItemPatFieldDefs = new System.Windows.Forms.MenuItem();
			this.menuItemPayerIDs = new System.Windows.Forms.MenuItem();
			this.menuItemPractice = new System.Windows.Forms.MenuItem();
			this.menuItemProblems = new System.Windows.Forms.MenuItem();
			this.menuItemProcedureButtons = new System.Windows.Forms.MenuItem();
			this.menuItemLinks = new System.Windows.Forms.MenuItem();
			this.menuItemQuestions = new System.Windows.Forms.MenuItem();
			this.menuItemRecall = new System.Windows.Forms.MenuItem();
			this.menuItemRecallTypes = new System.Windows.Forms.MenuItem();
			this.menuItemReplication = new System.Windows.Forms.MenuItem();
			this.menuItemRequirementsNeeded = new System.Windows.Forms.MenuItem();
			this.menuItemSched = new System.Windows.Forms.MenuItem();
			this.menuItemSecurity = new System.Windows.Forms.MenuItem();
			this.menuItemSheets = new System.Windows.Forms.MenuItem();
			this.menuItemEasy = new System.Windows.Forms.MenuItem();
			this.menuItemTimeCards = new System.Windows.Forms.MenuItem();
			this.menuItemLists = new System.Windows.Forms.MenuItem();
			this.menuItemProcCodes = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemClinics = new System.Windows.Forms.MenuItem();
			this.menuItemContacts = new System.Windows.Forms.MenuItem();
			this.menuItemCounties = new System.Windows.Forms.MenuItem();
			this.menuItemSchoolClass = new System.Windows.Forms.MenuItem();
			this.menuItemSchoolCourses = new System.Windows.Forms.MenuItem();
			this.menuItemEmployees = new System.Windows.Forms.MenuItem();
			this.menuItemEmployers = new System.Windows.Forms.MenuItem();
			this.menuItemCarriers = new System.Windows.Forms.MenuItem();
			this.menuItemInsPlans = new System.Windows.Forms.MenuItem();
			this.menuItemLabCases = new System.Windows.Forms.MenuItem();
			this.menuItemMedications = new System.Windows.Forms.MenuItem();
			this.menuItemPharmacies = new System.Windows.Forms.MenuItem();
			this.menuItemProviders = new System.Windows.Forms.MenuItem();
			this.menuItemPrescriptions = new System.Windows.Forms.MenuItem();
			this.menuItemReferrals = new System.Windows.Forms.MenuItem();
			this.menuItemSchools = new System.Windows.Forms.MenuItem();
			this.menuItemZipCodes = new System.Windows.Forms.MenuItem();
			this.menuItemReports = new System.Windows.Forms.MenuItem();
			this.menuItemCustomReports = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemPrintScreen = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuTelephone = new System.Windows.Forms.MenuItem();
			this.menuItemCreateAtoZFolders = new System.Windows.Forms.MenuItem();
			this.menuItemImportXML = new System.Windows.Forms.MenuItem();
			this.menuItemMergePatients = new System.Windows.Forms.MenuItem();
			this.menuItemDuplicateBlockouts = new System.Windows.Forms.MenuItem();
			this.menuItemTestLatency = new System.Windows.Forms.MenuItem();
			this.menuItemShutdown = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemAging = new System.Windows.Forms.MenuItem();
			this.menuItemAuditTrail = new System.Windows.Forms.MenuItem();
			this.menuItemFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemCCRecurring = new System.Windows.Forms.MenuItem();
			this.menuItemCustomerManage = new System.Windows.Forms.MenuItem();
			this.menuItemDatabaseMaintenance = new System.Windows.Forms.MenuItem();
			this.menuItemTerminal = new System.Windows.Forms.MenuItem();
			this.menuItemTerminalManager = new System.Windows.Forms.MenuItem();
			this.menuItemTranslation = new System.Windows.Forms.MenuItem();
			this.menuItemMobileSetup = new System.Windows.Forms.MenuItem();
			this.menuItemScreening = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatingCharges = new System.Windows.Forms.MenuItem();
			this.menuItemReqStudents = new System.Windows.Forms.MenuItem();
			this.menuItemWebForms = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuItemRemote = new System.Windows.Forms.MenuItem();
			this.menuItemHelpWindows = new System.Windows.Forms.MenuItem();
			this.menuItemHelpContents = new System.Windows.Forms.MenuItem();
			this.menuItemHelpIndex = new System.Windows.Forms.MenuItem();
			this.menuItemRequestFeatures = new System.Windows.Forms.MenuItem();
			this.menuItemUpdate = new System.Windows.Forms.MenuItem();
			this.imageList32 = new System.Windows.Forms.ImageList(this.components);
			this.timerSignals = new System.Windows.Forms.Timer(this.components);
			this.panelSplitter = new System.Windows.Forms.Panel();
			this.menuSplitter = new System.Windows.Forms.ContextMenu();
			this.menuItemDockBottom = new System.Windows.Forms.MenuItem();
			this.menuItemDockRight = new System.Windows.Forms.MenuItem();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.menuLabel = new System.Windows.Forms.ContextMenu();
			this.menuEmail = new System.Windows.Forms.ContextMenu();
			this.menuLetter = new System.Windows.Forms.ContextMenu();
			this.timerDisabledKey = new System.Windows.Forms.Timer(this.components);
			this.timerHeartBeat = new System.Windows.Forms.Timer(this.components);
			this.timerPhoneWebCam = new System.Windows.Forms.Timer(this.components);
			this.timerWebHostSynch = new System.Windows.Forms.Timer(this.components);
			this.timerLogoff = new System.Windows.Forms.Timer(this.components);
			this.timerReplicationMonitor = new System.Windows.Forms.Timer(this.components);
			this.panelPhoneSmall = new System.Windows.Forms.Panel();
			this.butTriage = new OpenDental.UI.Button();
			this.butBigPhones = new OpenDental.UI.Button();
			this.labelWaitTime = new System.Windows.Forms.Label();
			this.labelTriage = new System.Windows.Forms.Label();
			this.labelMsg = new System.Windows.Forms.Label();
			this.lightSignalGrid1 = new OpenDental.UI.LightSignalGrid();
			this.panelPhoneSmall.SuspendLayout();
			this.SuspendLayout();
			// 
			// timerTimeIndic
			// 
			this.timerTimeIndic.Interval = 60000;
			this.timerTimeIndic.Tick += new System.EventHandler(this.timerTimeIndic_Tick);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemLogOff,
            this.menuItemFile,
            this.menuItemSettings,
            this.menuItemLists,
            this.menuItemReports,
            this.menuItemCustomReports,
            this.menuItemTools,
            this.menuItemHelp});
			// 
			// menuItemLogOff
			// 
			this.menuItemLogOff.Index = 0;
			this.menuItemLogOff.Text = "Log &Off";
			this.menuItemLogOff.Click += new System.EventHandler(this.menuItemLogOff_Click);
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 1;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemPassword,
            this.menuItem3,
            this.menuItemPrinter,
            this.menuItemGraphics,
            this.menuItem6,
            this.menuItemConfig,
            this.menuItem7,
            this.menuItemExit});
			this.menuItemFile.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItemFile.Text = "&File";
			// 
			// menuItemPassword
			// 
			this.menuItemPassword.Index = 0;
			this.menuItemPassword.Text = "Change Password";
			this.menuItemPassword.Click += new System.EventHandler(this.menuItemPassword_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItemPrinter
			// 
			this.menuItemPrinter.Index = 2;
			this.menuItemPrinter.Text = "&Printers";
			this.menuItemPrinter.Click += new System.EventHandler(this.menuItemPrinter_Click);
			// 
			// menuItemGraphics
			// 
			this.menuItemGraphics.Index = 3;
			this.menuItemGraphics.Text = "Graphics";
			this.menuItemGraphics.Click += new System.EventHandler(this.menuItemGraphics_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "-";
			// 
			// menuItemConfig
			// 
			this.menuItemConfig.Index = 5;
			this.menuItemConfig.Text = "&Choose Database";
			this.menuItemConfig.Click += new System.EventHandler(this.menuItemConfig_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 7;
			this.menuItemExit.ShowShortcut = false;
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 2;
			this.menuItemSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuApptFieldDefs,
            this.menuItemApptRules,
            this.menuItemApptViews,
            this.menuItemAutoCodes,
            this.menuItemAutomation,
            this.menuItemAutoNotes,
            this.menuItemClaimForms,
            this.menuItemClearinghouses,
            this.menuItemComputers,
            this.menuItemDataPath,
            this.menuItemDefinitions,
            this.menuItemDisplayFields,
            this.menuItemEmail,
            this.menuItemEHR,
            this.menuItemFeeScheds,
            this.menuItemImaging,
            this.menuItemInsCats,
            this.menuItemInsFilingCodes,
            this.menuItemLaboratories,
            this.menuItemLetters,
            this.menuItemMessaging,
            this.menuItemMessagingButs,
            this.menuItemMisc,
            this.menuItemModules,
            this.menuItemOperatories,
            this.menuItemPatFieldDefs,
            this.menuItemPayerIDs,
            this.menuItemPractice,
            this.menuItemProblems,
            this.menuItemProcedureButtons,
            this.menuItemLinks,
            this.menuItemQuestions,
            this.menuItemRecall,
            this.menuItemRecallTypes,
            this.menuItemReplication,
            this.menuItemRequirementsNeeded,
            this.menuItemSched,
            this.menuItemSecurity,
            this.menuItemSheets,
            this.menuItemEasy,
            this.menuItemTimeCards});
			this.menuItemSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItemSettings.Text = "&Setup";
			// 
			// menuApptFieldDefs
			// 
			this.menuApptFieldDefs.Index = 0;
			this.menuApptFieldDefs.Text = "Appointment Field Defs";
			this.menuApptFieldDefs.Click += new System.EventHandler(this.menuItemApptFieldDefs_Click);
			// 
			// menuItemApptRules
			// 
			this.menuItemApptRules.Index = 1;
			this.menuItemApptRules.Text = "Appointment Rules";
			this.menuItemApptRules.Click += new System.EventHandler(this.menuItemApptRules_Click);
			// 
			// menuItemApptViews
			// 
			this.menuItemApptViews.Index = 2;
			this.menuItemApptViews.Text = "Appointment Views";
			this.menuItemApptViews.Click += new System.EventHandler(this.menuItemApptViews_Click);
			// 
			// menuItemAutoCodes
			// 
			this.menuItemAutoCodes.Index = 3;
			this.menuItemAutoCodes.Text = "Auto Codes";
			this.menuItemAutoCodes.Click += new System.EventHandler(this.menuItemAutoCodes_Click);
			// 
			// menuItemAutomation
			// 
			this.menuItemAutomation.Index = 4;
			this.menuItemAutomation.Text = "Automation";
			this.menuItemAutomation.Click += new System.EventHandler(this.menuItemAutomation_Click);
			// 
			// menuItemAutoNotes
			// 
			this.menuItemAutoNotes.Index = 5;
			this.menuItemAutoNotes.Text = "Auto Notes";
			this.menuItemAutoNotes.Click += new System.EventHandler(this.menuItemAutoNotes_Click);
			// 
			// menuItemClaimForms
			// 
			this.menuItemClaimForms.Index = 6;
			this.menuItemClaimForms.Text = "Claim Forms";
			this.menuItemClaimForms.Click += new System.EventHandler(this.menuItemClaimForms_Click);
			// 
			// menuItemClearinghouses
			// 
			this.menuItemClearinghouses.Index = 7;
			this.menuItemClearinghouses.Text = "Clearinghouses";
			this.menuItemClearinghouses.Click += new System.EventHandler(this.menuItemClearinghouses_Click);
			// 
			// menuItemComputers
			// 
			this.menuItemComputers.Index = 8;
			this.menuItemComputers.Text = "Computers";
			this.menuItemComputers.Click += new System.EventHandler(this.menuItemComputers_Click);
			// 
			// menuItemDataPath
			// 
			this.menuItemDataPath.Index = 9;
			this.menuItemDataPath.Text = "Data Paths";
			this.menuItemDataPath.Click += new System.EventHandler(this.menuItemDataPath_Click);
			// 
			// menuItemDefinitions
			// 
			this.menuItemDefinitions.Index = 10;
			this.menuItemDefinitions.Text = "Definitions";
			this.menuItemDefinitions.Click += new System.EventHandler(this.menuItemDefinitions_Click);
			// 
			// menuItemDisplayFields
			// 
			this.menuItemDisplayFields.Index = 11;
			this.menuItemDisplayFields.Text = "Display Fields";
			this.menuItemDisplayFields.Click += new System.EventHandler(this.menuItemDisplayFields_Click);
			// 
			// menuItemEmail
			// 
			this.menuItemEmail.Index = 12;
			this.menuItemEmail.Text = "E-mail";
			this.menuItemEmail.Click += new System.EventHandler(this.menuItemEmail_Click);
			// 
			// menuItemEHR
			// 
			this.menuItemEHR.Index = 13;
			this.menuItemEHR.Text = "EHR";
			this.menuItemEHR.Click += new System.EventHandler(this.menuItemEHR_Click);
			// 
			// menuItemFeeScheds
			// 
			this.menuItemFeeScheds.Index = 14;
			this.menuItemFeeScheds.Text = "Fee Schedules";
			this.menuItemFeeScheds.Click += new System.EventHandler(this.menuItemFeeScheds_Click);
			// 
			// menuItemImaging
			// 
			this.menuItemImaging.Index = 15;
			this.menuItemImaging.Text = "Imaging";
			this.menuItemImaging.Click += new System.EventHandler(this.menuItemImaging_Click);
			// 
			// menuItemInsCats
			// 
			this.menuItemInsCats.Index = 16;
			this.menuItemInsCats.Text = "Insurance Categories";
			this.menuItemInsCats.Click += new System.EventHandler(this.menuItemInsCats_Click);
			// 
			// menuItemInsFilingCodes
			// 
			this.menuItemInsFilingCodes.Index = 17;
			this.menuItemInsFilingCodes.Text = "Insurance Filing Codes";
			this.menuItemInsFilingCodes.Click += new System.EventHandler(this.menuItemInsFilingCodes_Click);
			// 
			// menuItemLaboratories
			// 
			this.menuItemLaboratories.Index = 18;
			this.menuItemLaboratories.Text = "Laboratories";
			this.menuItemLaboratories.Click += new System.EventHandler(this.menuItemLaboratories_Click);
			// 
			// menuItemLetters
			// 
			this.menuItemLetters.Index = 19;
			this.menuItemLetters.Text = "Letters";
			this.menuItemLetters.Click += new System.EventHandler(this.menuItemLetters_Click);
			// 
			// menuItemMessaging
			// 
			this.menuItemMessaging.Index = 20;
			this.menuItemMessaging.Text = "Messaging";
			this.menuItemMessaging.Click += new System.EventHandler(this.menuItemMessaging_Click);
			// 
			// menuItemMessagingButs
			// 
			this.menuItemMessagingButs.Index = 21;
			this.menuItemMessagingButs.Text = "Messaging Buttons";
			this.menuItemMessagingButs.Click += new System.EventHandler(this.menuItemMessagingButs_Click);
			// 
			// menuItemMisc
			// 
			this.menuItemMisc.Index = 22;
			this.menuItemMisc.Text = "Miscellaneous";
			this.menuItemMisc.Click += new System.EventHandler(this.menuItemMisc_Click);
			// 
			// menuItemModules
			// 
			this.menuItemModules.Index = 23;
			this.menuItemModules.Text = "Modules";
			this.menuItemModules.Click += new System.EventHandler(this.menuItemModules_Click);
			// 
			// menuItemOperatories
			// 
			this.menuItemOperatories.Index = 24;
			this.menuItemOperatories.Text = "Operatories";
			this.menuItemOperatories.Click += new System.EventHandler(this.menuItemOperatories_Click);
			// 
			// menuItemPatFieldDefs
			// 
			this.menuItemPatFieldDefs.Index = 25;
			this.menuItemPatFieldDefs.Text = "Patient Field Defs";
			this.menuItemPatFieldDefs.Click += new System.EventHandler(this.menuItemPatFieldDefs_Click);
			// 
			// menuItemPayerIDs
			// 
			this.menuItemPayerIDs.Index = 26;
			this.menuItemPayerIDs.Text = "Payer IDs";
			this.menuItemPayerIDs.Click += new System.EventHandler(this.menuItemPayerIDs_Click);
			// 
			// menuItemPractice
			// 
			this.menuItemPractice.Index = 27;
			this.menuItemPractice.Text = "Practice";
			this.menuItemPractice.Click += new System.EventHandler(this.menuItemPractice_Click);
			// 
			// menuItemProblems
			// 
			this.menuItemProblems.Index = 28;
			this.menuItemProblems.Text = "Problems";
			this.menuItemProblems.Click += new System.EventHandler(this.menuItemProblems_Click);
			// 
			// menuItemProcedureButtons
			// 
			this.menuItemProcedureButtons.Index = 29;
			this.menuItemProcedureButtons.Text = "Procedure Buttons";
			this.menuItemProcedureButtons.Click += new System.EventHandler(this.menuItemProcedureButtons_Click);
			// 
			// menuItemLinks
			// 
			this.menuItemLinks.Index = 30;
			this.menuItemLinks.Text = "Program Links";
			this.menuItemLinks.Click += new System.EventHandler(this.menuItemLinks_Click);
			// 
			// menuItemQuestions
			// 
			this.menuItemQuestions.Index = 31;
			this.menuItemQuestions.Text = "Questionnaire";
			this.menuItemQuestions.Click += new System.EventHandler(this.menuItemQuestions_Click);
			// 
			// menuItemRecall
			// 
			this.menuItemRecall.Index = 32;
			this.menuItemRecall.Text = "Recall";
			this.menuItemRecall.Click += new System.EventHandler(this.menuItemRecall_Click);
			// 
			// menuItemRecallTypes
			// 
			this.menuItemRecallTypes.Index = 33;
			this.menuItemRecallTypes.Text = "RecallTypes";
			this.menuItemRecallTypes.Click += new System.EventHandler(this.menuItemRecallTypes_Click);
			// 
			// menuItemReplication
			// 
			this.menuItemReplication.Index = 34;
			this.menuItemReplication.Text = "Replication";
			this.menuItemReplication.Click += new System.EventHandler(this.menuItemReplication_Click);
			// 
			// menuItemRequirementsNeeded
			// 
			this.menuItemRequirementsNeeded.Index = 35;
			this.menuItemRequirementsNeeded.Text = "Requirements Needed";
			this.menuItemRequirementsNeeded.Click += new System.EventHandler(this.menuItemRequirementsNeeded_Click);
			// 
			// menuItemSched
			// 
			this.menuItemSched.Index = 36;
			this.menuItemSched.Text = "Schedules";
			this.menuItemSched.Click += new System.EventHandler(this.menuItemSched_Click);
			// 
			// menuItemSecurity
			// 
			this.menuItemSecurity.Index = 37;
			this.menuItemSecurity.Text = "Security";
			this.menuItemSecurity.Click += new System.EventHandler(this.menuItemSecurity_Click);
			// 
			// menuItemSheets
			// 
			this.menuItemSheets.Index = 38;
			this.menuItemSheets.Text = "Sheets";
			this.menuItemSheets.Click += new System.EventHandler(this.menuItemSheets_Click);
			// 
			// menuItemEasy
			// 
			this.menuItemEasy.Index = 39;
			this.menuItemEasy.Text = "Show Features";
			this.menuItemEasy.Click += new System.EventHandler(this.menuItemEasy_Click);
			// 
			// menuItemTimeCards
			// 
			this.menuItemTimeCards.Index = 40;
			this.menuItemTimeCards.Text = "Time Cards";
			this.menuItemTimeCards.Click += new System.EventHandler(this.menuItemTimeCards_Click);
			// 
			// menuItemLists
			// 
			this.menuItemLists.Index = 3;
			this.menuItemLists.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemProcCodes,
            this.menuItem5,
            this.menuItemClinics,
            this.menuItemContacts,
            this.menuItemCounties,
            this.menuItemSchoolClass,
            this.menuItemSchoolCourses,
            this.menuItemEmployees,
            this.menuItemEmployers,
            this.menuItemCarriers,
            this.menuItemInsPlans,
            this.menuItemLabCases,
            this.menuItemMedications,
            this.menuItemPharmacies,
            this.menuItemProviders,
            this.menuItemPrescriptions,
            this.menuItemReferrals,
            this.menuItemSchools,
            this.menuItemZipCodes});
			this.menuItemLists.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
			this.menuItemLists.Text = "&Lists";
			// 
			// menuItemProcCodes
			// 
			this.menuItemProcCodes.Index = 0;
			this.menuItemProcCodes.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
			this.menuItemProcCodes.Text = "&Procedure Codes";
			this.menuItemProcCodes.Click += new System.EventHandler(this.menuItemProcCodes_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "-";
			// 
			// menuItemClinics
			// 
			this.menuItemClinics.Index = 2;
			this.menuItemClinics.Text = "Clinics";
			this.menuItemClinics.Click += new System.EventHandler(this.menuItemClinics_Click);
			// 
			// menuItemContacts
			// 
			this.menuItemContacts.Index = 3;
			this.menuItemContacts.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftC;
			this.menuItemContacts.Text = "&Contacts";
			this.menuItemContacts.Click += new System.EventHandler(this.menuItemContacts_Click);
			// 
			// menuItemCounties
			// 
			this.menuItemCounties.Index = 4;
			this.menuItemCounties.Text = "Counties";
			this.menuItemCounties.Click += new System.EventHandler(this.menuItemCounties_Click);
			// 
			// menuItemSchoolClass
			// 
			this.menuItemSchoolClass.Index = 5;
			this.menuItemSchoolClass.Text = "Dental School Classes";
			this.menuItemSchoolClass.Click += new System.EventHandler(this.menuItemSchoolClass_Click);
			// 
			// menuItemSchoolCourses
			// 
			this.menuItemSchoolCourses.Index = 6;
			this.menuItemSchoolCourses.Text = "Dental School Courses";
			this.menuItemSchoolCourses.Click += new System.EventHandler(this.menuItemSchoolCourses_Click);
			// 
			// menuItemEmployees
			// 
			this.menuItemEmployees.Index = 7;
			this.menuItemEmployees.Text = "&Employees";
			this.menuItemEmployees.Click += new System.EventHandler(this.menuItemEmployees_Click);
			// 
			// menuItemEmployers
			// 
			this.menuItemEmployers.Index = 8;
			this.menuItemEmployers.Text = "Employers";
			this.menuItemEmployers.Click += new System.EventHandler(this.menuItemEmployers_Click);
			// 
			// menuItemCarriers
			// 
			this.menuItemCarriers.Index = 9;
			this.menuItemCarriers.Text = "Insurance Carriers";
			this.menuItemCarriers.Click += new System.EventHandler(this.menuItemCarriers_Click);
			// 
			// menuItemInsPlans
			// 
			this.menuItemInsPlans.Index = 10;
			this.menuItemInsPlans.Text = "&Insurance Plans";
			this.menuItemInsPlans.Click += new System.EventHandler(this.menuItemInsPlans_Click);
			// 
			// menuItemLabCases
			// 
			this.menuItemLabCases.Index = 11;
			this.menuItemLabCases.Text = "Lab Cases";
			this.menuItemLabCases.Click += new System.EventHandler(this.menuItemLabCases_Click);
			// 
			// menuItemMedications
			// 
			this.menuItemMedications.Index = 12;
			this.menuItemMedications.Text = "&Medications";
			this.menuItemMedications.Click += new System.EventHandler(this.menuItemMedications_Click);
			// 
			// menuItemPharmacies
			// 
			this.menuItemPharmacies.Index = 13;
			this.menuItemPharmacies.Text = "Pharmacies";
			this.menuItemPharmacies.Click += new System.EventHandler(this.menuItemPharmacies_Click);
			// 
			// menuItemProviders
			// 
			this.menuItemProviders.Index = 14;
			this.menuItemProviders.Text = "Providers";
			this.menuItemProviders.Click += new System.EventHandler(this.menuItemProviders_Click);
			// 
			// menuItemPrescriptions
			// 
			this.menuItemPrescriptions.Index = 15;
			this.menuItemPrescriptions.Text = "Pre&scriptions";
			this.menuItemPrescriptions.Click += new System.EventHandler(this.menuItemPrescriptions_Click);
			// 
			// menuItemReferrals
			// 
			this.menuItemReferrals.Index = 16;
			this.menuItemReferrals.Text = "&Referrals";
			this.menuItemReferrals.Click += new System.EventHandler(this.menuItemReferrals_Click);
			// 
			// menuItemSchools
			// 
			this.menuItemSchools.Index = 17;
			this.menuItemSchools.Text = "Sites";
			this.menuItemSchools.Click += new System.EventHandler(this.menuItemSites_Click);
			// 
			// menuItemZipCodes
			// 
			this.menuItemZipCodes.Index = 18;
			this.menuItemZipCodes.Text = "&Zip Codes";
			this.menuItemZipCodes.Click += new System.EventHandler(this.menuItemZipCodes_Click);
			// 
			// menuItemReports
			// 
			this.menuItemReports.Index = 4;
			this.menuItemReports.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.menuItemReports.Text = "&Reports";
			this.menuItemReports.Click += new System.EventHandler(this.menuItemReports_Click);
			// 
			// menuItemCustomReports
			// 
			this.menuItemCustomReports.Index = 5;
			this.menuItemCustomReports.Text = "Custom Reports";
			// 
			// menuItemTools
			// 
			this.menuItemTools.Index = 6;
			this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemPrintScreen,
            this.menuItem1,
            this.menuItem9,
            this.menuItemAging,
            this.menuItemAuditTrail,
            this.menuItemFinanceCharge,
            this.menuItemCCRecurring,
            this.menuItemCustomerManage,
            this.menuItemDatabaseMaintenance,
            this.menuItemTerminal,
            this.menuItemTerminalManager,
            this.menuItemTranslation,
            this.menuItemMobileSetup,
            this.menuItemScreening,
            this.menuItemRepeatingCharges,
            this.menuItemReqStudents,
            this.menuItemWebForms});
			this.menuItemTools.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
			this.menuItemTools.Text = "&Tools";
			// 
			// menuItemPrintScreen
			// 
			this.menuItemPrintScreen.Index = 0;
			this.menuItemPrintScreen.Text = "&Print Screen Tool";
			this.menuItemPrintScreen.Click += new System.EventHandler(this.menuItemPrintScreen_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuTelephone,
            this.menuItemCreateAtoZFolders,
            this.menuItemImportXML,
            this.menuItemMergePatients,
            this.menuItemDuplicateBlockouts,
            this.menuItemTestLatency,
            this.menuItemShutdown});
			this.menuItem1.Text = "Misc Tools";
			// 
			// menuTelephone
			// 
			this.menuTelephone.Index = 0;
			this.menuTelephone.Text = "Telephone Numbers";
			this.menuTelephone.Click += new System.EventHandler(this.menuTelephone_Click);
			// 
			// menuItemCreateAtoZFolders
			// 
			this.menuItemCreateAtoZFolders.Index = 1;
			this.menuItemCreateAtoZFolders.Text = "Create A to Z Folders";
			this.menuItemCreateAtoZFolders.Click += new System.EventHandler(this.menuItemCreateAtoZFolders_Click);
			// 
			// menuItemImportXML
			// 
			this.menuItemImportXML.Index = 2;
			this.menuItemImportXML.Text = "Import Patient XML";
			this.menuItemImportXML.Click += new System.EventHandler(this.menuItemImportXML_Click);
			// 
			// menuItemMergePatients
			// 
			this.menuItemMergePatients.Index = 3;
			this.menuItemMergePatients.Text = "Merge Patients";
			this.menuItemMergePatients.Click += new System.EventHandler(this.menuItemMergePatients_Click);
			// 
			// menuItemDuplicateBlockouts
			// 
			this.menuItemDuplicateBlockouts.Index = 4;
			this.menuItemDuplicateBlockouts.Text = "Clear Duplicate Blockouts";
			this.menuItemDuplicateBlockouts.Click += new System.EventHandler(this.menuItemDuplicateBlockouts_Click);
			// 
			// menuItemTestLatency
			// 
			this.menuItemTestLatency.Index = 5;
			this.menuItemTestLatency.Text = "Test Latency";
			this.menuItemTestLatency.Click += new System.EventHandler(this.menuItemTestLatency_Click);
			// 
			// menuItemShutdown
			// 
			this.menuItemShutdown.Index = 6;
			this.menuItemShutdown.Text = "Shutdown All Workstations";
			this.menuItemShutdown.Click += new System.EventHandler(this.menuItemShutdown_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.Text = "-";
			// 
			// menuItemAging
			// 
			this.menuItemAging.Index = 3;
			this.menuItemAging.Text = "&Aging";
			this.menuItemAging.Click += new System.EventHandler(this.menuItemAging_Click);
			// 
			// menuItemAuditTrail
			// 
			this.menuItemAuditTrail.Index = 4;
			this.menuItemAuditTrail.Text = "Audit Trail";
			this.menuItemAuditTrail.Click += new System.EventHandler(this.menuItemAuditTrail_Click);
			// 
			// menuItemFinanceCharge
			// 
			this.menuItemFinanceCharge.Index = 5;
			this.menuItemFinanceCharge.Text = "Billing/&Finance Charges";
			this.menuItemFinanceCharge.Click += new System.EventHandler(this.menuItemFinanceCharge_Click);
			// 
			// menuItemCCRecurring
			// 
			this.menuItemCCRecurring.Index = 6;
			this.menuItemCCRecurring.Text = "CC Recurring Charges";
			this.menuItemCCRecurring.Click += new System.EventHandler(this.menuItemCCRecurring_Click);
			// 
			// menuItemCustomerManage
			// 
			this.menuItemCustomerManage.Index = 7;
			this.menuItemCustomerManage.Text = "Customer Management";
			this.menuItemCustomerManage.Click += new System.EventHandler(this.menuItemCustomerManage_Click);
			// 
			// menuItemDatabaseMaintenance
			// 
			this.menuItemDatabaseMaintenance.Index = 8;
			this.menuItemDatabaseMaintenance.Text = "Database Maintenance";
			this.menuItemDatabaseMaintenance.Click += new System.EventHandler(this.menuItemDatabaseMaintenance_Click);
			// 
			// menuItemTerminal
			// 
			this.menuItemTerminal.Index = 9;
			this.menuItemTerminal.Text = "Kiosk";
			this.menuItemTerminal.Click += new System.EventHandler(this.menuItemTerminal_Click);
			// 
			// menuItemTerminalManager
			// 
			this.menuItemTerminalManager.Index = 10;
			this.menuItemTerminalManager.Text = "Kiosk Manager";
			this.menuItemTerminalManager.Click += new System.EventHandler(this.menuItemTerminalManager_Click);
			// 
			// menuItemTranslation
			// 
			this.menuItemTranslation.Index = 11;
			this.menuItemTranslation.Text = "Language Translation";
			this.menuItemTranslation.Click += new System.EventHandler(this.menuItemTranslation_Click);
			// 
			// menuItemMobileSetup
			// 
			this.menuItemMobileSetup.Index = 12;
			this.menuItemMobileSetup.Text = "Mobile and Patient Portal Synch";
			this.menuItemMobileSetup.Click += new System.EventHandler(this.menuItemMobileSetup_Click);
			// 
			// menuItemScreening
			// 
			this.menuItemScreening.Index = 13;
			this.menuItemScreening.Text = "Public Health Screening";
			this.menuItemScreening.Click += new System.EventHandler(this.menuItemScreening_Click);
			// 
			// menuItemRepeatingCharges
			// 
			this.menuItemRepeatingCharges.Index = 14;
			this.menuItemRepeatingCharges.Text = "Repeating Charges";
			this.menuItemRepeatingCharges.Click += new System.EventHandler(this.menuItemRepeatingCharges_Click);
			// 
			// menuItemReqStudents
			// 
			this.menuItemReqStudents.Index = 15;
			this.menuItemReqStudents.Text = "Student Requirements";
			this.menuItemReqStudents.Click += new System.EventHandler(this.menuItemReqStudents_Click);
			// 
			// menuItemWebForms
			// 
			this.menuItemWebForms.Index = 16;
			this.menuItemWebForms.Text = "WebForms";
			this.menuItemWebForms.Click += new System.EventHandler(this.menuItemWebForms_Click);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 7;
			this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemRemote,
            this.menuItemHelpWindows,
            this.menuItemHelpContents,
            this.menuItemHelpIndex,
            this.menuItemRequestFeatures,
            this.menuItemUpdate});
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemRemote
			// 
			this.menuItemRemote.Index = 0;
			this.menuItemRemote.Text = "Online Support";
			this.menuItemRemote.Click += new System.EventHandler(this.menuItemRemote_Click);
			// 
			// menuItemHelpWindows
			// 
			this.menuItemHelpWindows.Index = 1;
			this.menuItemHelpWindows.Text = "Local Help-Windows";
			this.menuItemHelpWindows.Click += new System.EventHandler(this.menuItemHelpWindows_Click);
			// 
			// menuItemHelpContents
			// 
			this.menuItemHelpContents.Index = 2;
			this.menuItemHelpContents.Text = "Online Help - Contents";
			this.menuItemHelpContents.Click += new System.EventHandler(this.menuItemHelpContents_Click);
			// 
			// menuItemHelpIndex
			// 
			this.menuItemHelpIndex.Index = 3;
			this.menuItemHelpIndex.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
			this.menuItemHelpIndex.Text = "Online Help - Index";
			this.menuItemHelpIndex.Click += new System.EventHandler(this.menuItemHelpIndex_Click);
			// 
			// menuItemRequestFeatures
			// 
			this.menuItemRequestFeatures.Index = 4;
			this.menuItemRequestFeatures.Text = "Request Features";
			this.menuItemRequestFeatures.Click += new System.EventHandler(this.menuItemRequestFeatures_Click);
			// 
			// menuItemUpdate
			// 
			this.menuItemUpdate.Index = 5;
			this.menuItemUpdate.Text = "&Update";
			this.menuItemUpdate.Click += new System.EventHandler(this.menuItemUpdate_Click);
			// 
			// imageList32
			// 
			this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
			this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList32.Images.SetKeyName(0, "Appt32.gif");
			this.imageList32.Images.SetKeyName(1, "Family32b.gif");
			this.imageList32.Images.SetKeyName(2, "Account32b.gif");
			this.imageList32.Images.SetKeyName(3, "TreatPlan3D.gif");
			this.imageList32.Images.SetKeyName(4, "chart32.gif");
			this.imageList32.Images.SetKeyName(5, "Images32.gif");
			this.imageList32.Images.SetKeyName(6, "Manage32.gif");
			// 
			// timerSignals
			// 
			this.timerSignals.Tick += new System.EventHandler(this.timerSignals_Tick);
			// 
			// panelSplitter
			// 
			this.panelSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSplitter.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.panelSplitter.Location = new System.Drawing.Point(71, 542);
			this.panelSplitter.Name = "panelSplitter";
			this.panelSplitter.Size = new System.Drawing.Size(769, 7);
			this.panelSplitter.TabIndex = 50;
			this.panelSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseDown);
			this.panelSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseMove);
			this.panelSplitter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseUp);
			// 
			// menuSplitter
			// 
			this.menuSplitter.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDockBottom,
            this.menuItemDockRight});
			// 
			// menuItemDockBottom
			// 
			this.menuItemDockBottom.Index = 0;
			this.menuItemDockBottom.Text = "Dock to Bottom";
			this.menuItemDockBottom.Click += new System.EventHandler(this.menuItemDockBottom_Click);
			// 
			// menuItemDockRight
			// 
			this.menuItemDockRight.Index = 1;
			this.menuItemDockRight.Text = "Dock to Right";
			this.menuItemDockRight.Click += new System.EventHandler(this.menuItemDockRight_Click);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0, "Pat.gif");
			this.imageListMain.Images.SetKeyName(1, "commlog.gif");
			this.imageListMain.Images.SetKeyName(2, "email.gif");
			this.imageListMain.Images.SetKeyName(3, "tasksNicer.gif");
			this.imageListMain.Images.SetKeyName(4, "label.gif");
			this.imageListMain.Images.SetKeyName(5, "Text.gif");
			// 
			// menuPatient
			// 
			this.menuPatient.Popup += new System.EventHandler(this.menuPatient_Popup);
			// 
			// menuLabel
			// 
			this.menuLabel.Popup += new System.EventHandler(this.menuLabel_Popup);
			// 
			// menuEmail
			// 
			this.menuEmail.Popup += new System.EventHandler(this.menuEmail_Popup);
			// 
			// menuLetter
			// 
			this.menuLetter.Popup += new System.EventHandler(this.menuLetter_Popup);
			// 
			// timerDisabledKey
			// 
			this.timerDisabledKey.Enabled = true;
			this.timerDisabledKey.Interval = 600000;
			this.timerDisabledKey.Tick += new System.EventHandler(this.timerDisabledKey_Tick);
			// 
			// timerHeartBeat
			// 
			this.timerHeartBeat.Enabled = true;
			this.timerHeartBeat.Interval = 180000;
			this.timerHeartBeat.Tick += new System.EventHandler(this.timerHeartBeat_Tick);
			// 
			// timerPhoneWebCam
			// 
			this.timerPhoneWebCam.Interval = 1600;
			this.timerPhoneWebCam.Tick += new System.EventHandler(this.timerPhoneWebCam_Tick);
			// 
			// timerWebHostSynch
			// 
			this.timerWebHostSynch.Enabled = true;
			this.timerWebHostSynch.Interval = 30000;
			this.timerWebHostSynch.Tick += new System.EventHandler(this.timerWebHostSynch_Tick);
			// 
			// timerLogoff
			// 
			this.timerLogoff.Interval = 15000;
			this.timerLogoff.Tick += new System.EventHandler(this.timerLogoff_Tick);
			// 
			// timerReplicationMonitor
			// 
			this.timerReplicationMonitor.Interval = 10000;
			this.timerReplicationMonitor.Tick += new System.EventHandler(this.timerReplicationMonitor_Tick);
			// 
			// panelPhoneSmall
			// 
			this.panelPhoneSmall.Controls.Add(this.butTriage);
			this.panelPhoneSmall.Controls.Add(this.butBigPhones);
			this.panelPhoneSmall.Controls.Add(this.labelWaitTime);
			this.panelPhoneSmall.Controls.Add(this.labelTriage);
			this.panelPhoneSmall.Controls.Add(this.labelMsg);
			this.panelPhoneSmall.Location = new System.Drawing.Point(71, 333);
			this.panelPhoneSmall.Name = "panelPhoneSmall";
			this.panelPhoneSmall.Size = new System.Drawing.Size(150, 250);
			this.panelPhoneSmall.TabIndex = 56;
			// 
			// butTriage
			// 
			this.butTriage.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTriage.Autosize = true;
			this.butTriage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTriage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTriage.CornerRadius = 4F;
			this.butTriage.Location = new System.Drawing.Point(98, 0);
			this.butTriage.Name = "butTriage";
			this.butTriage.Size = new System.Drawing.Size(22, 24);
			this.butTriage.TabIndex = 52;
			this.butTriage.Text = "T";
			this.butTriage.Click += new System.EventHandler(this.butTriage_Click);
			// 
			// butBigPhones
			// 
			this.butBigPhones.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBigPhones.Autosize = true;
			this.butBigPhones.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBigPhones.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBigPhones.CornerRadius = 4F;
			this.butBigPhones.Location = new System.Drawing.Point(122, 0);
			this.butBigPhones.Name = "butBigPhones";
			this.butBigPhones.Size = new System.Drawing.Size(28, 24);
			this.butBigPhones.TabIndex = 52;
			this.butBigPhones.Text = "Big";
			this.butBigPhones.Click += new System.EventHandler(this.butBigPhones_Click);
			// 
			// labelWaitTime
			// 
			this.labelWaitTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWaitTime.ForeColor = System.Drawing.Color.Black;
			this.labelWaitTime.Location = new System.Drawing.Point(66, 2);
			this.labelWaitTime.Name = "labelWaitTime";
			this.labelWaitTime.Size = new System.Drawing.Size(32, 20);
			this.labelWaitTime.TabIndex = 53;
			this.labelWaitTime.Text = "00m";
			this.labelWaitTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelTriage
			// 
			this.labelTriage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTriage.ForeColor = System.Drawing.Color.Black;
			this.labelTriage.Location = new System.Drawing.Point(31, 2);
			this.labelTriage.Name = "labelTriage";
			this.labelTriage.Size = new System.Drawing.Size(38, 20);
			this.labelTriage.TabIndex = 53;
			this.labelTriage.Text = "T:00";
			this.labelTriage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelMsg
			// 
			this.labelMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelMsg.ForeColor = System.Drawing.Color.Firebrick;
			this.labelMsg.Location = new System.Drawing.Point(-1, 2);
			this.labelMsg.Name = "labelMsg";
			this.labelMsg.Size = new System.Drawing.Size(44, 20);
			this.labelMsg.TabIndex = 53;
			this.labelMsg.Text = "V:00";
			this.labelMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lightSignalGrid1
			// 
			this.lightSignalGrid1.Location = new System.Drawing.Point(0, 463);
			this.lightSignalGrid1.Name = "lightSignalGrid1";
			this.lightSignalGrid1.Size = new System.Drawing.Size(50, 206);
			this.lightSignalGrid1.TabIndex = 20;
			this.lightSignalGrid1.Text = "lightSignalGrid1";
			this.lightSignalGrid1.ButtonClick += new OpenDental.UI.ODLightSignalGridClickEventHandler(this.lightSignalGrid1_ButtonClick);
			// 
			// FormOpenDental
			// 
			this.ClientSize = new System.Drawing.Size(982, 564);
			this.Controls.Add(this.panelPhoneSmall);
			this.Controls.Add(this.panelSplitter);
			this.Controls.Add(this.lightSignalGrid1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Menu = this.mainMenu;
			this.Name = "FormOpenDental";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "Open Dental";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOpenDental_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormOpenDental_FormClosed);
			this.Load += new System.EventHandler(this.FormOpenDental_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOpenDental_KeyDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormOpenDental_MouseMove);
			this.Resize += new System.EventHandler(this.FormOpenDental_Resize);
			this.panelPhoneSmall.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void FormOpenDental_Load(object sender, System.EventArgs e){
			Splash.Dispose();
			Version versionOd=Assembly.GetAssembly(typeof(FormOpenDental)).GetName().Version;
			Version versionObBus=Assembly.GetAssembly(typeof(Db)).GetName().Version;
			if(versionOd!=versionObBus) {
				MessageBox.Show("Mismatched program file versions. Please run the Open Dental setup file again on this computer.");//No MsgBox or Lan.g() here, because we don't want to access the database if there is a version conflict.
				Application.Exit();
				return;
			}
			allNeutral();
			string odUser="";
			string odPassHash="";
			string webServiceUri="";
			YN webServiceIsEcw=YN.Unknown;
			string odPassword="";
			string serverName="";
			string databaseName="";
			string mySqlUser="";
			string mySqlPassword="";
			if(CommandLineArgs.Length!=0) {
				for(int i=0;i<CommandLineArgs.Length;i++) {
					if(CommandLineArgs[i].StartsWith("UserName=") && CommandLineArgs[i].Length>9) {
						odUser=CommandLineArgs[i].Substring(9).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("PassHash=") && CommandLineArgs[i].Length>9) {
						odPassHash=CommandLineArgs[i].Substring(9).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("WebServiceUri=") && CommandLineArgs[i].Length>14) {
						webServiceUri=CommandLineArgs[i].Substring(14).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("WebServiceIsEcw=") && CommandLineArgs[i].Length>16) {
						if(CommandLineArgs[i].Substring(16).Trim('"')=="True") {
							webServiceIsEcw=YN.Yes;
						}
						else {
							webServiceIsEcw=YN.No;
						}
					}
					if(CommandLineArgs[i].StartsWith("OdPassword=") && CommandLineArgs[i].Length>11) {
						odPassword=CommandLineArgs[i].Substring(11).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("ServerName=") && CommandLineArgs[i].Length>11) {
						serverName=CommandLineArgs[i].Substring(11).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("DatabaseName=") && CommandLineArgs[i].Length>13) {
						databaseName=CommandLineArgs[i].Substring(13).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("MySqlUser=") && CommandLineArgs[i].Length>10) {
						mySqlUser=CommandLineArgs[i].Substring(10).Trim('"');
					}
					if(CommandLineArgs[i].StartsWith("MySqlPassword=") && CommandLineArgs[i].Length>14) {
						mySqlPassword=CommandLineArgs[i].Substring(14).Trim('"');
					}
				}
			}
			YN noShow=YN.Unknown;
			if(webServiceUri!=""){//a web service was specified
				if(odUser!="" && odPassword!=""){//and both a username and password were specified
					noShow=YN.Yes;
				}
			}
			else if(databaseName!=""){
				noShow=YN.Yes;
			}
			FormChooseDatabase formChooseDb=new FormChooseDatabase();
			formChooseDb.OdUser=odUser;
			formChooseDb.OdPassHash=odPassHash;
			formChooseDb.WebServiceUri=webServiceUri;
			formChooseDb.WebServiceIsEcw=webServiceIsEcw;
			formChooseDb.OdPassword=odPassword;
			formChooseDb.ServerName=serverName;
			formChooseDb.DatabaseName=databaseName;
			formChooseDb.MySqlUser=mySqlUser;
			formChooseDb.MySqlPassword=mySqlPassword;
			formChooseDb.NoShow=noShow;//either unknown or yes
			formChooseDb.GetConfig();
			formChooseDb.GetCmdLine();
			while(true) {//Most users will loop through once.  If user tries to connect to a db with replication failure, they will loop through again.
				if(formChooseDb.NoShow==YN.Yes) {
					if(!formChooseDb.TryToConnect()) {
						formChooseDb.ShowDialog();
						if(formChooseDb.DialogResult==DialogResult.Cancel) {
							Application.Exit();
							return;
						}
					}
				}
				else {
					formChooseDb.ShowDialog();
					if(formChooseDb.DialogResult==DialogResult.Cancel) {
						Application.Exit();
						return;
					}
				}
				Cursor=Cursors.WaitCursor;
				Splash=new FormSplash();
				if(CommandLineArgs.Length==0) {
					Splash.Show();
				}
				if(!PrefsStartup()){//looks for the AtoZ folder here, but would like to eventually move that down to after login
					Cursor=Cursors.Default;
					Splash.Dispose();
					Application.Exit();
					return;
				}
				if(ReplicationServers.Server_id!=0 && ReplicationServers.Server_id==PrefC.GetInt(PrefName.ReplicationFailureAtServer_id)) {
					MsgBox.Show(this,"This database is temporarily unavailable.  Please connect instead to your alternate database at the other location.");
					formChooseDb.NoShow=YN.No;//This ensures they will get a choose db window next time through the loop.
					ReplicationServers.Server_id=-1;
					continue;
				}
				break;
			}
			//if(Programs.UsingEcwTight()) {
			if(Programs.UsingEcwTightOrFull()) {
				Splash.Dispose();//We don't show splash screen when bridging to eCW.
			}
			//We no longer do this shotgun approach because it can slow the loading time.
			//RefreshLocalData(InvalidType.AllLocal);
			List<InvalidType> invalidTypes=new List<InvalidType>();
			invalidTypes.Add(InvalidType.Prefs);
			invalidTypes.Add(InvalidType.Defs);
			invalidTypes.Add(InvalidType.Providers);//obviously heavily used
			invalidTypes.Add(InvalidType.Programs);//already done above, but needs to be done explicitly to trigger the PostCleanup 
			invalidTypes.Add(InvalidType.ToolBut);//so program buttons will show in all the toolbars
			if(Programs.UsingEcwTight()) {
				lightSignalGrid1.Visible=false;
			}
			else{
				invalidTypes.Add(InvalidType.Signals);//so when mouse moves over light buttons, it won't crash
			}
			Plugins.LoadAllPlugins(this);//moved up from right after optimizing tooth chart graphics.  New position might cause problems.
			//It was moved because RefreshLocalData()=>RefreshLocalDataPostCleanup()=>ContrChart2.InitializeLocalData()=>LayoutToolBar() has a hook.
			RefreshLocalData(invalidTypes.ToArray());
			FillSignalButtons(null);
			ContrManage2.InitializeOnStartup();//so that when a signal is received, it can handle it.
			//Lan.Refresh();//automatically skips if current culture is en-US
			//LanguageForeigns.Refresh(CultureInfo.CurrentCulture);//automatically skips if current culture is en-US
			DataValid.BecameInvalid += new OpenDental.ValidEventHandler(DataValid_BecameInvalid);
			signalLastRefreshed=MiscData.GetNowDateTime();
			if(PrefC.GetInt(PrefName.ProcessSigsIntervalInSecs)==0) {
				timerSignals.Enabled=false;
			}
			else {
				timerSignals.Interval=PrefC.GetInt(PrefName.ProcessSigsIntervalInSecs)*1000;
				timerSignals.Enabled=true;
			}
			timerTimeIndic.Enabled=true;
			myOutlookBar.Buttons[0].Caption=Lan.g(this,"Appts");
			myOutlookBar.Buttons[1].Caption=Lan.g(this,"Family");
			myOutlookBar.Buttons[2].Caption=Lan.g(this,"Account");
			myOutlookBar.Buttons[3].Caption=Lan.g(this,"Treat' Plan");
			//myOutlookBar.Buttons[4].Caption=Lan.g(this,"Chart");//??done in RefreshLocalData
			myOutlookBar.Buttons[5].Caption=Lan.g(this,"Images");
			myOutlookBar.Buttons[6].Caption=Lan.g(this,"Manage");
			foreach(MenuItem menuItem in mainMenu.MenuItems){
				TranslateMenuItem(menuItem);
			}
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				//for the business layer, this functionality is duplicated in the Lan class.  Need for SL.
				CultureInfo cInfo=(CultureInfo)CultureInfo.CurrentCulture.Clone();
				cInfo.DateTimeFormat.ShortDatePattern="MM/dd/yyyy";
				Application.CurrentCulture=cInfo;
				//
				//if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				menuItemTranslation.Visible=false;
			}
			if(!File.Exists("Help.chm")){
				menuItemHelpWindows.Visible=false;
			}
			if(Environment.OSVersion.Platform==PlatformID.Unix){//Create A to Z unsupported on Unix for now.
				menuItemCreateAtoZFolders.Visible=false;
			}
			if(!PrefC.GetBool(PrefName.ADAdescriptionsReset)) {
				ProcedureCodes.ResetADAdescriptions();
				Prefs.UpdateBool(PrefName.ADAdescriptionsReset,true);
			}
			Splash.Dispose();
			//Choose a default DirectX format when no DirectX format has been specified and running in DirectX tooth chart mode.
			//ComputerPref localComputerPrefs=ComputerPrefs.GetForLocalComputer();
			if(ComputerPrefs.LocalComputer.GraphicsSimple==DrawingMode.DirectX && ComputerPrefs.LocalComputer.DirectXFormat=="") {
				//MessageBox.Show(Lan.g(this,"Optimizing tooth chart graphics. This may take a few minutes. You will be notified when the process is complete."));
				ComputerPrefs.LocalComputer.DirectXFormat=FormGraphics.GetPreferredDirectXFormat(this);
				if(ComputerPrefs.LocalComputer.DirectXFormat=="invalid") {
					//No valid local DirectX format could be found.
					//Simply revert to OpenGL.
					ComputerPrefs.LocalComputer.GraphicsSimple=DrawingMode.OpenGL;
				}
				ComputerPrefs.Update(ComputerPrefs.LocalComputer);
				ContrChart2.InitializeOnStartup();
				//MsgBox.Show(this,"Done optimizing tooth chart graphics.");
			}
			if(Security.CurUser==null) {//It could already be set if using web service because login from ChooseDatabase window.
				//if(Programs.UsingEcwTight() && odUser!="") {//only leave it null if a user was passed in on the commandline.  If starting OD manually, it will jump into the else.
				if(Programs.UsingEcwTightOrFull() && odUser!="") {//only leave it null if a user was passed in on the commandline.  If starting OD manually, it will jump into the else.
					//leave user as null
				}
				else {
					if(odUser!="" && odPassword!=""){//if a username and password were passed in
						//Userod user=Userods.GetUserByName(odUser,Programs.UsingEcwTight());
						Userod user=Userods.GetUserByName(odUser,Programs.UsingEcwTightOrFull());
						if(user!=null){
							if(Userods.CheckTypedPassword(odPassword,user.Password)){//password matches
								Security.CurUser=user.Copy();
								if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
									string pw=odPassword;
									//if(Programs.UsingEcwTight()) {//ecw requires hash, but non-ecw requires actual password
									if(Programs.UsingEcwTightOrFull()) {//ecw requires hash, but non-ecw requires actual password
										pw=Userods.EncryptPassword(pw,true);
									}
									Security.PasswordTyped=pw;
								}
								//TasksCheckOnStartup is one thing that doesn't get done because login window wasn't used
							}
						}
					}
					if(Security.CurUser==null) {//normal manual log in process
						Userod adminUser=Userods.GetAdminUser();
						if(adminUser.Password=="") {
							Security.CurUser=adminUser.Copy();
						}
						else {
							FormLogOn_=new FormLogOn();
							FormLogOn_.ShowDialog(this);
							if(FormLogOn_.DialogResult==DialogResult.Cancel) {
								Cursor=Cursors.Default;
								Application.Exit();
								return;
							}
						}
					}
				}
			}
			if(userControlTasks1.Visible) {
				userControlTasks1.InitializeOnStartup();
			}
			myOutlookBar.SelectedIndex=Security.GetModule(0);//for eCW, this fails silently.
			//if(Programs.UsingEcwTight()) {
			if(Programs.UsingEcwTightOrFull()) {
				myOutlookBar.SelectedIndex=4;//Chart module
				//ToolBarMain.Height=0;//this should force the modules further up on the screen
				//ToolBarMain.Visible=false;
				LayoutControls();
			}
			if(Programs.UsingOrion) {
				myOutlookBar.SelectedIndex=1;//Family module
			}
			myOutlookBar.Invalidate();
			LayoutToolBar();
			SetModuleSelected();
			Cursor=Cursors.Default;
			if(myOutlookBar.SelectedIndex==-1){
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
			Bridges.Trojan.StartupCheck();
			FormUAppoint.StartThreadIfEnabled();
			Bridges.ICat.StartFileWatcher();
			if(PrefC.GetBool(PrefName.DockPhonePanelShow)){
				#if !DEBUG
					if(Process.GetProcessesByName("WebCamOD").Length==0) {
						try {
							Process.Start("WebCamOD.exe");
						}
						catch { }//for example, if working from home.
					}
				#endif
				ThreadVM=new Thread(new ThreadStart(this.ThreadVM_SetLabelMsg));
				ThreadVM.Start();//It's done this way because the file activity tends to lock the UI on slow connections.
				FillTriageLabels();
			}
			#if !TRIALONLY
				if(PrefC.GetDate(PrefName.BackupReminderLastDateRun).AddMonths(1)<DateTime.Today) {
					FormBackupReminder FormBR=new FormBackupReminder();
					FormBR.ShowDialog();
					if(FormBR.DialogResult==DialogResult.OK){
						Prefs.UpdateDateT(PrefName.BackupReminderLastDateRun,DateTime.Today);
					}
					else{
						Application.Exit();
						return;
					}
				}
			#endif
			FillPatientButton(null);
			ThreadCommandLine=new Thread(new ThreadStart(Listen));
			if(!IsSecondInstance) {//can't use a port that's already in use.
				//js We can't do this yet.  I tried it already, and it consumes nearly 100% CPU.  Not while testing, but later.
				//So until we really need to do it, it's easiest no just not start the thread for now.
				//ThreadCommandLine.Start();
			}
			//if(CommandLineArgs.Length>0) {
			ProcessCommandLine(CommandLineArgs);
			//}
			try {
				Computers.UpdateHeartBeat(Environment.MachineName);
			}
			catch { }
			string dllPathEHR=ODFileUtils.CombinePaths(Application.StartupPath,"EHR.dll");
			if(PrefC.GetBoolSilent(PrefName.ShowFeatureEhr,false)) {
				#if EHRTEST
					FormEHR=new FormEHR();
					ContrChart2.InitializeLocalData();//because toolbar is now missing the EHR button.  Only a problem if a db conversion is done when opening the program.
				#else
					FormEHR=null;
					AssemblyEHR=null;
					if(File.Exists(dllPathEHR)) {//EHR.dll is available, so load it up
						AssemblyEHR=Assembly.LoadFile(dllPathEHR);
						Type type=AssemblyEHR.GetType("EHR.FormEHR");//namespace.class
						FormEHR=Activator.CreateInstance(type);
					}
				#endif
			}
			dateTimeLastActivity=DateTime.Now;
			timerLogoff.Enabled=true;
			timerReplicationMonitor.Enabled=true;
			Plugins.HookAddCode(this,"FormOpenDental.Load_end");
		}

		///<summary>Returns false if it can't complete a conversion, find datapath, or validate registration key.</summary>
		private bool PrefsStartup(){
			Cache.Refresh(InvalidType.Prefs);
			if(!PrefL.CheckMySqlVersion()){
				return false;
			}
			if(DataConnection.DBtype==DatabaseType.MySql) {
				try {
					MiscData.SetSqlMode();
				}
				catch {
					MessageBox.Show("Unable to set global sql mode.  User probably does not have enough permission.");
					return false;
				}
				string updateComputerName=PrefC.GetStringSilent(PrefName.UpdateInProgressOnComputerName);
				if(updateComputerName != "" && Environment.MachineName != updateComputerName) {
					DialogResult result=MessageBox.Show("An update is in progress on "+updateComputerName+".  Not allowed to start up until that update is complete.\r\n\r\nIf you are the person who started the update and you wish to override this message because an update is not in progress, click Retry.\r\n\r\nDo not click Retry unless you started the update.",
						"",MessageBoxButtons.RetryCancel);
					if(result==DialogResult.Retry) {
						Prefs.UpdateString(PrefName.UpdateInProgressOnComputerName,"");
						MsgBox.Show(this,"You will be allowed access when you restart.");
					}
					return false;
				}
			}
			//if RemotingRole.ClientWeb, version will have already been checked at login, so no danger here.
			//ClientWeb version can be older than this version, but that will be caught in a moment.
			if(!PrefL.ConvertDB()){
				return false;
			}
			PrefL.MySqlVersion55Remind();
			if(PrefC.UsingAtoZfolder) {
				string prefImagePath=ImageStore.GetPreferredAtoZpath();
				if(prefImagePath==null || !Directory.Exists(prefImagePath)) {//AtoZ folder not found
					Cache.Refresh(InvalidType.Security);
					FormPath FormP=new FormPath();
					FormP.IsStartingUp=true;
					FormP.ShowDialog();
					if(FormP.DialogResult!=DialogResult.OK) {
						MsgBox.Show(this,"Invalid A to Z path.  Closing program.");
						Application.Exit();
						return false;
					}
					//bool usingAtoZ=FormPath.UsingImagePath();
					//ContrImages2.Enabled=usingAtoZ;
					//menuItemClaimForms.Visible=usingAtoZ;
					//CheckCustomReports();
					//this.RefreshCurrentModule();
					Cache.Refresh(InvalidType.Prefs);//because listening thread not started yet.
				}
			}
			if(!PrefL.CheckProgramVersion()){
				return false;
			}
			if(!FormRegistrationKey.ValidateKey(PrefC.GetString(PrefName.RegistrationKey))){
				//true){
				FormRegistrationKey FormR=new FormRegistrationKey();
				FormR.ShowDialog();
				if(FormR.DialogResult!=DialogResult.OK){
					Application.Exit();
					return false;
				}
				Cache.Refresh(InvalidType.Prefs);
			}
			Lans.RefreshCache();//automatically skips if current culture is en-US
			LanguageForeigns.Refresh(CultureInfo.CurrentCulture.Name,CultureInfo.CurrentCulture.TwoLetterISOLanguageName);//automatically skips if current culture is en-US
			//menuItemMergeDatabases.Visible=PrefC.GetBool(PrefName.RandomPrimaryKeys");
			return true;
		}
		
		///<summary>Refreshes certain rarely used data from database.  Must supply the types of data to refresh as flags.  Also performs a few other tasks that must be done when local data is changed.</summary>
		private void RefreshLocalData(params InvalidType[] itypes){
			List<int> itypeList=new List<int>();
			for(int i=0;i<itypes.Length;i++){
				itypeList.Add((int)itypes[i]);
			}
			string itypesStr="";
			bool isAll=false;
			for(int i=0;i<itypes.Length;i++) {
				if(i>0) {
					itypesStr+=",";
				}
				itypesStr+=((int)itypes[i]).ToString();
				if(itypes[i]==InvalidType.AllLocal){
					isAll=true;
				}
			}
			Cache.RefreshCache(itypesStr);
			RefreshLocalDataPostCleanup(itypeList,isAll,itypes);
		}
		
		///<summary>Performs a few tasks that must be done when local data is changed.</summary>
		private void RefreshLocalDataPostCleanup(List<int> itypeList,bool isAll,params InvalidType[] itypes) {
			if(itypeList.Contains((int)InvalidType.Prefs) || isAll) {
				if(PrefC.GetBool(PrefName.EasyHidePublicHealth)) {
					menuItemSchools.Visible=false;
					menuItemCounties.Visible=false;
					menuItemScreening.Visible=false;
				}
				else {
					menuItemSchools.Visible=true;
					menuItemCounties.Visible=true;
					menuItemScreening.Visible=true;
				}
				if(PrefC.GetBool(PrefName.EasyNoClinics)) {
					menuItemClinics.Visible=false;
				}
				else {
					menuItemClinics.Visible=true;
				}
				if(PrefC.GetBool(PrefName.EasyHideClinical)) {
					myOutlookBar.Buttons[4].Caption=Lan.g(this,"Procs");
				}
				else {
					myOutlookBar.Buttons[4].Caption=Lan.g(this,"Chart");
				}
				if(PrefC.GetBool(PrefName.EasyBasicModules)) {
					myOutlookBar.Buttons[3].Visible=false;
					myOutlookBar.Buttons[5].Visible=false;
					myOutlookBar.Buttons[6].Visible=false;
				}
				else {
					myOutlookBar.Buttons[3].Visible=true;
					myOutlookBar.Buttons[5].Visible=true;
					myOutlookBar.Buttons[6].Visible=true;
				}
				myOutlookBar.Invalidate();
				if(PrefC.GetBool(PrefName.EasyHideDentalSchools)) {
					menuItemSchoolClass.Visible=false;
					menuItemSchoolCourses.Visible=false;
					menuItemRequirementsNeeded.Visible=false;
					menuItemReqStudents.Visible=false;
				}
				else {
					menuItemSchoolClass.Visible=true;
					menuItemSchoolCourses.Visible=true;
					menuItemRequirementsNeeded.Visible=true;
					menuItemReqStudents.Visible=true;
				}
				if(PrefC.GetBool(PrefName.EasyHideRepeatCharges)) {
					menuItemRepeatingCharges.Visible=false;
				}
				else {
					menuItemRepeatingCharges.Visible=true;
				}

				if(PrefC.GetString(PrefName.DistributorKey)=="") {
					menuItemCustomerManage.Visible=false;
				}
				else {
					menuItemCustomerManage.Visible=true;
				}
				ContrImages2.Enabled=PrefC.UsingAtoZfolder;
				//menuItemClaimForms.Visible=PrefC.UsingAtoZfolder;
				CheckCustomReports();
				ContrChart2.InitializeLocalData();
				if(PrefC.GetBool(PrefName.TaskListAlwaysShowsAtBottom)) {
					//separate if statement to prevent database call if not showing task list at bottom to begin with
					//ComputerPref computerPref = ComputerPrefs.GetForLocalComputer();
					if(ComputerPrefs.LocalComputer.TaskKeepListHidden) {
						userControlTasks1.Visible = false;
					}
					else {//task list show
						userControlTasks1.Visible = true;
						userControlTasks1.InitializeOnStartup();
						if(ComputerPrefs.LocalComputer.TaskDock == 0) {//bottom
							menuItemDockBottom.Checked = true;
							menuItemDockRight.Checked = false;
							panelSplitter.Cursor=Cursors.HSplit;
							panelSplitter.Height=7;
							int splitterNewY=540;
							if(ComputerPrefs.LocalComputer.TaskY!=0) {
								splitterNewY=ComputerPrefs.LocalComputer.TaskY;
								if(splitterNewY<300) {
									splitterNewY=300;//keeps it from going too high
								}
								if(splitterNewY>ClientSize.Height-50) {
									splitterNewY=ClientSize.Height-panelSplitter.Height-50;//keeps it from going off the bottom edge
								}
							}
							panelSplitter.Location=new Point(myOutlookBar.Width,splitterNewY);
						}
						else {//right
							menuItemDockRight.Checked = true;
							menuItemDockBottom.Checked = false;
							panelSplitter.Cursor=Cursors.VSplit;
							panelSplitter.Width=7;
							int splitterNewX=900;
							if(ComputerPrefs.LocalComputer.TaskX!=0) {
								splitterNewX=ComputerPrefs.LocalComputer.TaskX;
								if(splitterNewX<300) {
									splitterNewX=300;//keeps it from going too far to the left
								}
								if(splitterNewX>ClientSize.Width-50) {
									splitterNewX=ClientSize.Width-panelSplitter.Width-50;//keeps it from going off the right edge
								}
							}
							panelSplitter.Location=new Point(splitterNewX,ToolBarMain.Height);
						}
					}
				}
				else {
					userControlTasks1.Visible = false;
				}
				LayoutControls();
			}//if(InvalidTypes.Prefs)
			if(itypeList.Contains((int)InvalidType.Signals) || isAll) {
				FillSignalButtons(null);
			}
			if(itypeList.Contains((int)InvalidType.Programs) || isAll) {
				if(Programs.GetCur(ProgramName.PT).Enabled) {
					Bridges.PaperlessTechnology.InitializeFileWatcher();
				}
				if(Programs.UsingEcwTight()) {
					myOutlookBar.Buttons[0].Visible=false;//Appt
					//The button for Family will be visible, but it will behave differently.
					//myOutlookBar.Buttons[1].Visible=false;//Family
					myOutlookBar.Buttons[2].Visible=false;//Account
					if(ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"ShowImagesModule")=="1") {
						myOutlookBar.Buttons[5].Visible=true;
					}
					else {
						myOutlookBar.Buttons[5].Visible=false;
					}
					myOutlookBar.Buttons[6].Visible=false;
				}
				else if(Programs.UsingEcwFull()) {
					//We might create a special Appt module for eCW full users so they can access Recall.
					myOutlookBar.Buttons[0].Visible=false;//Appt
					if(ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"ShowImagesModule")=="1") {
						myOutlookBar.Buttons[5].Visible=true;
					}
					else {
						myOutlookBar.Buttons[5].Visible=false;
					}
				}
				if(Programs.UsingOrion) {
					myOutlookBar.Buttons[0].Visible=false;//Appt module
					myOutlookBar.Buttons[2].Visible=false;//Account module
					myOutlookBar.Buttons[3].Visible=false;//TP module
				}
			}
			if(itypeList.Contains((int)InvalidType.ToolBut) || isAll) {
				ContrAccount2.LayoutToolBar();
				ContrAppt2.LayoutToolBar();
				ContrChart2.LayoutToolBar();
				ContrImages2.LayoutToolBar();
				ContrFamily2.LayoutToolBar();
			}
			if(itypeList.Contains((int)InvalidType.Views) || isAll) {
				ContrAppt2.FillViews();
			}
			ContrTreat2.InitializeLocalData();//easier to leave this here for now than to split it.
		}

		///<summary>Sets up the custom reports list in the main menu when certain requirements are met, or disables the custom reports menu item when those same conditions are not met. This function is called during initialization, and on the event that the A to Z folder usage has changed.</summary>
		private void CheckCustomReports(){
			menuItemCustomReports.MenuItems.Clear();
			//Try to load custom reports, but only if using the A to Z folders.
			if(PrefC.UsingAtoZfolder) {
				string imagePath=ImageStore.GetPreferredAtoZpath();
				string reportFolderName=PrefC.GetString(PrefName.ReportFolderName);
				string reportDir=ODFileUtils.CombinePaths(imagePath,reportFolderName);
				if(Directory.Exists(reportDir)) {
					DirectoryInfo infoDir=new DirectoryInfo(reportDir);
					FileInfo[] filesRdl=infoDir.GetFiles("*.rdl");
					for(int i=0;i<filesRdl.Length;i++) {
						string itemName=Path.GetFileNameWithoutExtension(filesRdl[i].Name);
						menuItemCustomReports.MenuItems.Add(itemName,new System.EventHandler(this.menuItemRDLReport_Click));
					}
				}
			}
			if(menuItemCustomReports.MenuItems.Count==0) {
				menuItemCustomReports.Visible=false;
			}else{
				menuItemCustomReports.Visible=true;
			}
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			if(!Programs.UsingEcwTight()) {//eCW tight only gets Patient Select and Popups toolbar buttons
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Commlog"),1,Lan.g(this,"New Commlog Entry"),"Commlog"));
				button=new ODToolBarButton(Lan.g(this,"E-mail"),2,Lan.g(this,"Send E-mail"),"Email");
				ToolBarMain.Buttons.Add(button);
				button=new ODToolBarButton("",-1,"","EmailDropdown");
				button.Style=ODToolBarButtonStyle.DropDownButton;
				button.DropDownMenu=menuEmail;
				ToolBarMain.Buttons.Add(button);
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Text"),5,Lan.g(this,"Send Text Message"),"Text"));
				button=new ODToolBarButton(Lan.g(this,"Letter"),-1,Lan.g(this,"Quick Letter"),"Letter");
				button.Style=ODToolBarButtonStyle.DropDownButton;
				button.DropDownMenu=menuLetter;
				ToolBarMain.Buttons.Add(button);
				button=new ODToolBarButton(Lan.g(this,"Forms"),-1,"","Form");
				//button.Style=ODToolBarButtonStyle.DropDownButton;
				//button.DropDownMenu=menuForm;
				ToolBarMain.Buttons.Add(button);
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"To Task List"),3,Lan.g(this,"Send to Task List"),"Tasklist"));
				button=new ODToolBarButton(Lan.g(this,"Label"),4,Lan.g(this,"Print Label"),"Label");
				button.Style=ODToolBarButtonStyle.DropDownButton;
				button.DropDownMenu=menuLabel;
				ToolBarMain.Buttons.Add(button);
			}
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Popups"),-1,Lan.g(this,"Edit popups for this patient"),"Popups"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.AllModules);
			for(int i=0;i<toolButItems.Count;i++) {
			  //ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			  ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
			    ,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			Plugins.HookAddCode(this,"FormOpenDental.LayoutToolBar_end");
			ToolBarMain.Invalidate();
		}

		private void menuPatient_Popup(object sender,EventArgs e) {
			if(CurPatNum==0){
				return;
			}
			Family fam=Patients.GetFamily(CurPatNum);
			PatientL.AddFamilyToMenu(menuPatient,new EventHandler(menuPatient_Click),CurPatNum,fam);
		}

		private void ToolBarMain_ButtonClick(object sender,ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)) {
				//standard predefined button
				switch(e.Button.Tag.ToString()) {
					case "Patient":
						OnPatient_Click();
						break;
					case "Commlog":
						OnCommlog_Click();
						break;
					case "Email":
						OnEmail_Click();
						break;
					case "Text":
						OnTxtMsg_Click();
						break;
					case "Letter":
						OnLetter_Click();
						break;
					case "Form":
						OnForm_Click();
						break;
					case "Tasklist":
						OnTasklist_Click();
						break;
					case "Label":
						OnLabel_Click();
						break;
					case "Popups":
						OnPopups_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(long)) {
				ProgramL.Execute((long)e.Button.Tag,Patients.GetPat(CurPatNum));
			}
		}

		private void OnPatient_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK) {
				CurPatNum=formPS.SelectedPatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				RefreshCurrentModule();
				FillPatientButton(pat);
				Plugins.HookAddCode(this,"FormOpenDental.OnPatient_Click_end");   
			}
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			Family fam=Patients.GetFamily(CurPatNum);
			CurPatNum=PatientL.ButtonSelect(menuPatient,sender,fam);
			//new family now
			Patient pat=Patients.GetPat(CurPatNum);
			RefreshCurrentModule();
			FillPatientButton(pat);
		}

		///<summary>Happens when any of the modules changes the current patient or when this main form changes the patient.  The calling module should refresh itself.  The current patNum is stored here in the parent form so that when switching modules, the parent form knows which patient to call up for that module.</summary>
		private void Contr_PatientSelected(object sender,PatientSelectedEventArgs e) {
			CurPatNum=e.Pat.PatNum;
			FillPatientButton(e.Pat);
		}

		///<Summary>Serves four functions.  1. Sends the new patient to the dropdown menu for select patient.  2. Changes which toolbar buttons are enabled.  3. Sets main form text. 4. Displays any popup.</Summary>
		private void FillPatientButton(Patient pat){
			if(pat==null) {
				pat=new Patient();
			}
			bool patChanged=PatientL.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),pat.GetNameLF(),pat.PatNum);
			if(patChanged){
				if(AutomationL.Trigger(AutomationTrigger.OpenPatient,null,pat.PatNum)) {//if a trigger happened
					if(ContrAppt2.Visible) {
						ContrAppt2.MouseUpForced();
					}
				}
			}
			if(ToolBarMain.Buttons==null || ToolBarMain.Buttons.Count<2){//on startup.  js Not sure why it's checking count.
				return;
			}
			if(CurPatNum==0) {//Only on startup, I think.
				if(!Programs.UsingEcwTight()) {//eCW tight only gets Patient Select and Popups toolbar buttons
					ToolBarMain.Buttons["Email"].Enabled=false;
					ToolBarMain.Buttons["EmailDropdown"].Enabled=false;
					ToolBarMain.Buttons["Text"].Enabled=false;
					ToolBarMain.Buttons["Commlog"].Enabled=false;
					ToolBarMain.Buttons["Letter"].Enabled=false;
					ToolBarMain.Buttons["Form"].Enabled=false;
					ToolBarMain.Buttons["Tasklist"].Enabled=false;
					ToolBarMain.Buttons["Label"].Enabled=false;
				}
				ToolBarMain.Buttons["Popups"].Enabled=false;
			}
			else {
				if(!Programs.UsingEcwTight()) {
					ToolBarMain.Buttons["Commlog"].Enabled=true;
					if(pat.Email!="") {
						ToolBarMain.Buttons["Email"].Enabled=true;
					}
					else {
						ToolBarMain.Buttons["Email"].Enabled=false;
					}
					if(pat.WirelessPhone=="" || !Programs.IsEnabled(ProgramName.CallFire)) {
						ToolBarMain.Buttons["Text"].Enabled=false;
					}
					else {//Pat has a wireless phone number and CallFire is enabled
						if(pat.TxtMsgOk==YN.Unknown) {
							ToolBarMain.Buttons["Text"].Enabled=!PrefC.GetBool(PrefName.TextMsgOkStatusTreatAsNo);//Not enabled since TxtMsgOk is ?? and "Treat ?? As No" is true.
						}
						else {
							ToolBarMain.Buttons["Text"].Enabled=(pat.TxtMsgOk==YN.Yes);
						}
					}
					ToolBarMain.Buttons["EmailDropdown"].Enabled=true;
					ToolBarMain.Buttons["Letter"].Enabled=true;
					ToolBarMain.Buttons["Form"].Enabled=true;
					ToolBarMain.Buttons["Tasklist"].Enabled=true;
					ToolBarMain.Buttons["Label"].Enabled=true;
				}
				ToolBarMain.Buttons["Popups"].Enabled=true;
			}
			ToolBarMain.Invalidate();
			Text=PatientL.GetMainTitle(pat);
			if(PopupEventList==null){
				PopupEventList=new List<PopupEvent>();
			}
			if(!patChanged){
				return;
			}
			//New patient selected.  Everything below here is for popups.
			//First, remove all expired popups from the event list.
			for(int i=PopupEventList.Count-1;i>=0;i--){//go backwards
				if(PopupEventList[i].DisableUntil<DateTime.Now){//expired
					PopupEventList.RemoveAt(i);
				}
			}
			//Now, loop through all popups for the patient.
			List<Popup> popList=Popups.GetForPatient(pat.PatNum);//get all possible 
			for(int i=0;i<popList.Count;i++) {
				//skip any popups that are disabled because they are on the event list
				bool popupIsDisabled=false;
				for(int e=0;e<PopupEventList.Count;e++){
					if(popList[i].PopupNum==PopupEventList[e].PopupNum){
						popupIsDisabled=true;
						break;
					}
				}
				if(popupIsDisabled){
					continue;
				}
				//This popup is not disabled, so show it.
				//A future improvement would be to assemble all the popups that are to be shown and then show them all in one large window.
				//But for now, they will show in sequence.
				if(ContrAppt2.Visible) {
					ContrAppt2.MouseUpForced();
				}
				FormPopupDisplay FormP=new FormPopupDisplay();
				FormP.PopupCur=popList[i];
				FormP.ShowDialog();
				if(FormP.MinutesDisabled>0){
					PopupEvent popevent=new PopupEvent();
					popevent.PopupNum=popList[i].PopupNum;
					popevent.DisableUntil=DateTime.Now+TimeSpan.FromMinutes(FormP.MinutesDisabled);
					PopupEventList.Add(popevent);
					PopupEventList.Sort();
				}
			}
		}

		private void OnEmail_Click() {
			//this button item will be disabled if pat does not have email address
			EmailMessage message=new EmailMessage();
			message.PatNum=CurPatNum;
			Patient pat=Patients.GetPat(CurPatNum);
			message.ToAddress=pat.Email;
			message.FromAddress=PrefC.GetString(PrefName.EmailSenderAddress);
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.OK) {
				RefreshCurrentModule();
			}
		}

		private void menuEmail_Popup(object sender,EventArgs e) {
			menuEmail.MenuItems.Clear();
			MenuItem menuItem;
			menuItem=new MenuItem(Lan.g(this,"Referrals:"));
			menuItem.Tag=null;
			menuEmail.MenuItems.Add(menuItem);
			List<RefAttach> refAttaches=RefAttaches.Refresh(CurPatNum);
			Referral refer;
			string str;
			for(int i=0;i<refAttaches.Count;i++) {
				refer=Referrals.GetReferral(refAttaches[i].ReferralNum);
				if(refAttaches[i].IsFrom) {
					str=Lan.g(this,"From ");
				}
				else {
					str=Lan.g(this,"To ");
				}
				str+=Referrals.GetNameFL(refer.ReferralNum)+" <";
				if(refer.EMail==""){
					str+=Lan.g(this,"no email");
				}
				else{
					str+=refer.EMail;
				}
				str+=">";
				menuItem=new MenuItem(str,menuEmail_Click);
				menuItem.Tag=refer;
				menuEmail.MenuItems.Add(menuItem);
			}
		}

		private void menuEmail_Click(object sender,System.EventArgs e) {
			if(((MenuItem)sender).Tag==null){
				return;
			}
			LabelSingle label=new LabelSingle();
			if(((MenuItem)sender).Tag.GetType()==typeof(Referral)) {
				Referral refer=(Referral)((MenuItem)sender).Tag;
				if(refer.EMail==""){
					return;
					//MsgBox.Show(this,"");
				}
				EmailMessage message=new EmailMessage();
				message.PatNum=CurPatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				message.ToAddress=refer.EMail;//pat.Email;
				message.FromAddress=PrefC.GetString(PrefName.EmailSenderAddress);
				message.Subject=Lan.g(this,"RE: ")+pat.GetNameFL();
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();
				if(FormE.DialogResult==DialogResult.OK) {
					RefreshCurrentModule();
				}
			}
		}

		private void OnCommlog_Click() {
			Commlog CommlogCur = new Commlog();
			CommlogCur.PatNum = CurPatNum;
			CommlogCur.CommDateTime = DateTime.Now;
			CommlogCur.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
			if(PrefC.GetBool(PrefName.DistributorKey)) {//for OD HQ
				CommlogCur.Mode_=CommItemMode.None;
				CommlogCur.SentOrReceived=CommSentOrReceived.Neither;
			}
			else {
				CommlogCur.Mode_=CommItemMode.Phone;
				CommlogCur.SentOrReceived=CommSentOrReceived.Received;
			}
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI = new FormCommItem(CommlogCur);
			FormCI.IsNew = true;
			FormCI.ShowDialog();
			if(FormCI.DialogResult == DialogResult.OK) {
				RefreshCurrentModule();
			}
		}

		private void OnLetter_Click() {
			FormSheetPicker FormS=new FormSheetPicker();
			FormS.SheetType=SheetTypeEnum.PatientLetter;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDef sheetDef=FormS.SelectedSheetDefs[0];
			Sheet sheet=SheetUtil.CreateSheet(sheetDef,CurPatNum);
			SheetParameter.SetParameter(sheet,"PatNum",CurPatNum);
			//SheetParameter.SetParameter(sheet,"ReferralNum",referral.ReferralNum);
			SheetFiller.FillFields(sheet);
			SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
			FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
			FormSF.ShowDialog();
			if(FormSF.DialogResult==DialogResult.OK) {
				RefreshCurrentModule();
			}
			//Patient pat=Patients.GetPat(CurPatNum);
			//FormLetters FormL=new FormLetters(pat);
			//FormL.ShowDialog();
		}

		private void menuLetter_Popup(object sender,EventArgs e) {
			menuLetter.MenuItems.Clear();
			MenuItem menuItem;
			menuItem=new MenuItem(Lan.g(this,"Merge"),menuLetter_Click);
			menuItem.Tag="Merge";
			menuLetter.MenuItems.Add(menuItem);
			//menuItem=new MenuItem(Lan.g(this,"Stationery"),menuLetter_Click);
			//menuItem.Tag="Stationery";
			//menuLetter.MenuItems.Add(menuItem);
			menuLetter.MenuItems.Add("-");
			//Referrals---------------------------------------------------------------------------------------
			menuItem=new MenuItem(Lan.g(this,"Referrals:"));
			menuItem.Tag=null;
			menuLetter.MenuItems.Add(menuItem);
			List<RefAttach> refAttaches=RefAttaches.Refresh(CurPatNum);
			Referral refer;
			string str;
			for(int i=0;i<refAttaches.Count;i++) {
				refer=Referrals.GetReferral(refAttaches[i].ReferralNum);
				if(refAttaches[i].IsFrom) {
					str=Lan.g(this,"From ");
				}
				else {
					str=Lan.g(this,"To ");
				}
				str+=Referrals.GetNameFL(refer.ReferralNum);
				menuItem=new MenuItem(str,menuLetter_Click);
				menuItem.Tag=refer;
				menuLetter.MenuItems.Add(menuItem);
			}
		}

		private void menuLetter_Click(object sender,System.EventArgs e) {
			if(((MenuItem)sender).Tag==null) {
				return;
			}
			Patient pat=Patients.GetPat(CurPatNum);
			if(((MenuItem)sender).Tag.GetType()==typeof(string)) {
				if(((MenuItem)sender).Tag.ToString()=="Merge") {
					FormLetterMerges FormL=new FormLetterMerges(pat);
					FormL.ShowDialog();
				}
				//if(((MenuItem)sender).Tag.ToString()=="Stationery") {
				//	FormCommunications.PrintStationery(pat);
				//}
			}
			if(((MenuItem)sender).Tag.GetType()==typeof(Referral)) {
				Referral refer=(Referral)((MenuItem)sender).Tag;
				FormSheetPicker FormS=new FormSheetPicker();
				FormS.SheetType=SheetTypeEnum.ReferralLetter;
				FormS.ShowDialog();
				if(FormS.DialogResult!=DialogResult.OK){
					return;
				}
				SheetDef sheetDef=FormS.SelectedSheetDefs[0];
				Sheet sheet=SheetUtil.CreateSheet(sheetDef,CurPatNum);
				SheetParameter.SetParameter(sheet,"PatNum",CurPatNum);
				SheetParameter.SetParameter(sheet,"ReferralNum",refer.ReferralNum);
				SheetFiller.FillFields(sheet);
				SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
				FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
				FormSF.ShowDialog();
				if(FormSF.DialogResult==DialogResult.OK) {
					RefreshCurrentModule();
				}
				//FormLetters FormL=new FormLetters(pat);
				//FormL.ReferralCur=refer;
				//FormL.ShowDialog();
			}
		}

		private void OnForm_Click() {
			FormPatientForms formP=new FormPatientForms();
			formP.PatNum=CurPatNum;
			formP.ShowDialog();
			//if(ContrAccount2.Visible || ContrChart2.Visible//The only two modules where a new form would show.
			//	|| ContrFamily2.Visible){//patient info
			//always refresh, especially to get the titlebar right after an import.
			Patient pat=Patients.GetPat(CurPatNum);
			RefreshCurrentModule();
			FillPatientButton(pat);
			//}
		}

		private void OnTasklist_Click(){
			FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Patient);//,CurPatNum);
			FormT.Location=new Point(50,50);
			FormT.ShowDialog();
			if(FormT.DialogResult!=DialogResult.OK) {
				return;
			}
			Task task=new Task();
			task.TaskListNum=-1;//don't show it in any list yet.
			Tasks.Insert(task);
			Task taskOld=task.Copy();
			task.KeyNum=CurPatNum;
			task.ObjectType=TaskObjectType.Patient;
			task.TaskListNum=FormT.SelectedTaskListNum;
			task.UserNum=Security.CurUser.UserNum;
			FormTaskEdit FormTE=new FormTaskEdit(task,taskOld);
			FormTE.IsNew=true;
			FormTE.Show();
		}

		private void OnLabel_Click() {
			//LabelSingle label=new LabelSingle();
			LabelSingle.PrintPat(CurPatNum);
		}

		private void menuLabel_Popup(object sender,EventArgs e) {
			menuLabel.MenuItems.Clear();
			MenuItem menuItem;
			List<SheetDef> LabelList=SheetDefs.GetCustomForType(SheetTypeEnum.LabelPatient);
			if(LabelList.Count==0){
				menuItem=new MenuItem(Lan.g(this,"LName, FName, Address"),menuLabel_Click);
				menuItem.Tag="PatientLFAddress";
				menuLabel.MenuItems.Add(menuItem);
				menuItem=new MenuItem(Lan.g(this,"Name, ChartNumber"),menuLabel_Click);
				menuItem.Tag="PatientLFChartNumber";
				menuLabel.MenuItems.Add(menuItem);
				menuItem=new MenuItem(Lan.g(this,"Name, PatNum"),menuLabel_Click);
				menuItem.Tag="PatientLFPatNum";
				menuLabel.MenuItems.Add(menuItem);
				menuItem=new MenuItem(Lan.g(this,"Radiograph"),menuLabel_Click);
				menuItem.Tag="PatRadiograph";
				menuLabel.MenuItems.Add(menuItem);
			}
			else{
				for(int i=0;i<LabelList.Count;i++) {
					menuItem=new MenuItem(LabelList[i].Description,menuLabel_Click);
					menuItem.Tag=LabelList[i];
					menuLabel.MenuItems.Add(menuItem);
				}
			}
			menuLabel.MenuItems.Add("-");
			//Carriers---------------------------------------------------------------------------------------
			Family fam=Patients.GetFamily(CurPatNum);
			List <PatPlan> PatPlanList=PatPlans.Refresh(CurPatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> PlanList=InsPlans.RefreshForSubList(subList);
			Carrier carrier;
			InsPlan plan;
			InsSub sub;
			for(int i=0;i<PatPlanList.Count;i++) {
				sub=InsSubs.GetSub(PatPlanList[i].InsSubNum,subList);
				plan=InsPlans.GetPlan(sub.PlanNum,PlanList);
				carrier=Carriers.GetCarrier(plan.CarrierNum);
				menuItem=new MenuItem(carrier.CarrierName,menuLabel_Click);
				menuItem.Tag=carrier;
				menuLabel.MenuItems.Add(menuItem);
			}
			menuLabel.MenuItems.Add("-");
			//Referrals---------------------------------------------------------------------------------------
			menuItem=new MenuItem(Lan.g(this,"Referrals:"));
			menuItem.Tag=null;
			menuLabel.MenuItems.Add(menuItem);
			List<RefAttach> refAttaches=RefAttaches.Refresh(CurPatNum);
			Referral refer;
			string str;
			for(int i=0;i<refAttaches.Count;i++){
				refer=Referrals.GetReferral(refAttaches[i].ReferralNum);
				if(refAttaches[i].IsFrom){
					str=Lan.g(this,"From ");
				}
				else{
					str=Lan.g(this,"To ");
				}
				str+=Referrals.GetNameFL(refer.ReferralNum);
				menuItem=new MenuItem(str,menuLabel_Click);
				menuItem.Tag=refer;
				menuLabel.MenuItems.Add(menuItem);
			}
		}

		private void menuLabel_Click(object sender,System.EventArgs e) {
			if(((MenuItem)sender).Tag==null) {
				return;
			}
			//LabelSingle label=new LabelSingle();
			if(((MenuItem)sender).Tag.GetType()==typeof(string)){
				if(((MenuItem)sender).Tag.ToString()=="PatientLFAddress"){
					LabelSingle.PrintPatientLFAddress(CurPatNum);
				}
				if(((MenuItem)sender).Tag.ToString()=="PatientLFChartNumber") {
					LabelSingle.PrintPatientLFChartNumber(CurPatNum);
				}
				if(((MenuItem)sender).Tag.ToString()=="PatientLFPatNum") {
					LabelSingle.PrintPatientLFPatNum(CurPatNum);
				}
				if(((MenuItem)sender).Tag.ToString()=="PatRadiograph") {
					LabelSingle.PrintPatRadiograph(CurPatNum);
				}
			}
			else if(((MenuItem)sender).Tag.GetType()==typeof(SheetDef)){
				LabelSingle.PrintCustomPatient(CurPatNum,(SheetDef)((MenuItem)sender).Tag);
			}
			else if(((MenuItem)sender).Tag.GetType()==typeof(Carrier)){
				Carrier carrier=(Carrier)((MenuItem)sender).Tag;
				LabelSingle.PrintCarrier(carrier.CarrierNum);
			}
			else if(((MenuItem)sender).Tag.GetType()==typeof(Referral)) {
				Referral refer=(Referral)((MenuItem)sender).Tag;
				LabelSingle.PrintReferral(refer.ReferralNum);
			}
		}

		private void OnPopups_Click() {
			FormPopupsForFam FormPFF=new FormPopupsForFam();
			FormPFF.PatCur=Patients.GetPat(CurPatNum);
			FormPFF.ShowDialog();
		}

		private void OnTxtMsg_Click() {
			FormTxtMsgEdit FormTME=new FormTxtMsgEdit();
			Patient pat=Patients.GetPat(CurPatNum);
			FormTME.PatNum=CurPatNum;
			FormTME.WirelessPhone=pat.WirelessPhone;
			FormTME.TxtMsgOk=pat.TxtMsgOk;
			FormTME.ShowDialog();
			if(FormTME.DialogResult==DialogResult.OK) {
				RefreshCurrentModule();
			}
		}

		private void FormOpenDental_Resize(object sender,EventArgs e) {
			LayoutControls();
			if(Plugins.PluginsAreLoaded) {
				Plugins.HookAddCode(this,"FormOpenDental.FormOpenDental_Resize_end");
			}
		}

		///<summary>This used to be called much more frequently when it was an actual layout event.</summary>
		private void LayoutControls(){
			//Debug.WriteLine("layout");
			if(this.WindowState==FormWindowState.Minimized) {
				return;
			}
			if(Width<200){
				Width=200;
			}
			Point position=new Point(myOutlookBar.Width,ToolBarMain.Height);
			int width=this.ClientSize.Width-position.X;
			int height=this.ClientSize.Height-position.Y;
			if(userControlTasks1.Visible) {
				if(menuItemDockBottom.Checked) {
					if(panelSplitter.Height>8) {//docking needs to be changed
						panelSplitter.Height=7;
						panelSplitter.Location=new Point(position.X,540);
					}
					panelSplitter.Location=new Point(position.X,panelSplitter.Location.Y);
					panelSplitter.Width=width;
					panelSplitter.Visible=true;
					if(PrefC.GetBool(PrefName.DockPhonePanelShow)){
						timerPhoneWebCam.Enabled=true;//the only place this happens
						//phoneSmall.Visible=true;
						//phoneSmall.Location=new Point(position.X,panelSplitter.Bottom+butBigPhones.Height);
						phoneSmall.Location=new Point(0,24);
						userControlTasks1.Location=new Point(position.X+phoneSmall.Width,panelSplitter.Bottom);
						userControlTasks1.Width=width-phoneSmall.Width;
						//butBigPhones.Visible=true;
						//butBigPhones.Location=new Point(position.X+phoneSmall.Width-butBigPhones.Width,panelSplitter.Bottom);
						//butBigPhones.BringToFront();
						//labelMsg.Visible=true;
						//labelMsg.Location=new Point(position.X+phoneSmall.Width-butBigPhones.Width-labelMsg.Width,panelSplitter.Bottom);
						//labelMsg.BringToFront();
						panelPhoneSmall.Visible=true;
						panelPhoneSmall.Location=new Point(position.X,panelSplitter.Bottom);
						panelPhoneSmall.BringToFront();
					}
					else{
						//phoneSmall.Visible=false;
						//phonePanel.Visible=false;
						//butBigPhones.Visible=false;
						//labelMsg.Visible=false;
						panelPhoneSmall.Visible=false;
						userControlTasks1.Location=new Point(position.X,panelSplitter.Bottom);
						userControlTasks1.Width=width;
					}
					userControlTasks1.Height=this.ClientSize.Height-userControlTasks1.Top;
					height=ClientSize.Height-panelSplitter.Height-userControlTasks1.Height-ToolBarMain.Height;
				}
				else {//docked Right
					//phoneSmall.Visible=false;
					//phonePanel.Visible=false;
					//butBigPhones.Visible=false;
					//labelMsg.Visible=false;
					panelPhoneSmall.Visible=false;
					if(panelSplitter.Width>8) {//docking needs to be changed
						panelSplitter.Width=7;
						panelSplitter.Location=new Point(900,position.Y);
					}
					panelSplitter.Location=new Point(panelSplitter.Location.X,position.Y);
					panelSplitter.Height=height;
					panelSplitter.Visible=true;
					userControlTasks1.Location=new Point(panelSplitter.Right,position.Y);
					userControlTasks1.Height=height;
					userControlTasks1.Width=this.ClientSize.Width-userControlTasks1.Left;
					width=ClientSize.Width-panelSplitter.Width-userControlTasks1.Width-position.X;
				}
				panelSplitter.BringToFront();
				panelSplitter.Invalidate();
			}
			else {
				//phoneSmall.Visible=false;
				//phonePanel.Visible=false;
				//butBigPhones.Visible=false;
				//labelMsg.Visible=false;
				panelPhoneSmall.Visible=false;
				panelSplitter.Visible=false;
			}
			ContrAccount2.Location=position;
			ContrAccount2.Width=width;
			ContrAccount2.Height=height;
			ContrAppt2.Location=position;
			ContrAppt2.Width=width;
			ContrAppt2.Height=height;
			ContrChart2.Location=position;
			ContrChart2.Width=width;
			ContrChart2.Height=height;
			ContrImages2.Location=position;
			ContrImages2.Width=width;
			ContrImages2.Height=height;
			ContrFamily2.Location=position;
			ContrFamily2.Width=width;
			ContrFamily2.Height=height;
			ContrFamily2Ecw.Location=position;
			ContrFamily2Ecw.Width=width;
			ContrFamily2Ecw.Height=height;
			ContrManage2.Location=position;
			ContrManage2.Width=width;
			ContrManage2.Height=height;
			ContrTreat2.Location=position;
			ContrTreat2.Width=width;
			ContrTreat2.Height=height;
		}

		private void panelSplitter_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=true;
			SplitterOriginalLocation=panelSplitter.Location;
			OriginalMousePos=new Point(panelSplitter.Left+e.X,panelSplitter.Top+e.Y);
		}

		private void panelSplitter_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDownOnSplitter){
				return;
			}
			if(menuItemDockBottom.Checked){
				int splitterNewY=SplitterOriginalLocation.Y+(panelSplitter.Top+e.Y)-OriginalMousePos.Y;
				if(splitterNewY<300){
					splitterNewY=300;//keeps it from going too high
				}
				if(splitterNewY>ClientSize.Height-50){
					splitterNewY=ClientSize.Height-panelSplitter.Height-50;//keeps it from going off the bottom edge
				}
				panelSplitter.Top=splitterNewY;
			}
			else{//docked right
				int splitterNewX=SplitterOriginalLocation.X+(panelSplitter.Left+e.X)-OriginalMousePos.X;
				if(splitterNewX<300) {
					splitterNewX=300;//keeps it from going too far to the left
				}
				if(splitterNewX>ClientSize.Width-50) {
					splitterNewX=ClientSize.Width-panelSplitter.Width-50;//keeps it from going off the right edge
				}
				panelSplitter.Left=splitterNewX;
			}
			LayoutControls();
		}

		private void panelSplitter_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			MouseIsDownOnSplitter=false;
			TaskDockSavePos();
		}

		private void menuItemDockBottom_Click(object sender,EventArgs e) {
			menuItemDockBottom.Checked=true;
			menuItemDockRight.Checked=false;
			panelSplitter.Cursor=Cursors.HSplit;
			TaskDockSavePos();
			LayoutControls();
		}

		private void menuItemDockRight_Click(object sender,EventArgs e) {
			menuItemDockBottom.Checked=false;
			menuItemDockRight.Checked=true;
			//included now with layoutcontrols
			panelSplitter.Cursor=Cursors.VSplit;
			TaskDockSavePos();
			LayoutControls();
		}

		///<summary>Every time user changes doc position, it will save automatically.</summary>
		private void TaskDockSavePos(){
			//ComputerPref computerPref = ComputerPrefs.GetForLocalComputer();
			if(menuItemDockBottom.Checked){
				ComputerPrefs.LocalComputer.TaskY = panelSplitter.Top;
				ComputerPrefs.LocalComputer.TaskDock = 0;
			}
			else{
				ComputerPrefs.LocalComputer.TaskX = panelSplitter.Left;
				ComputerPrefs.LocalComputer.TaskDock = 1;
			}
			ComputerPrefs.Update(ComputerPrefs.LocalComputer);
		}
		
		///<summary>This is called when any local data becomes outdated.  It's purpose is to tell the other computers to update certain local data.</summary>
		private void DataValid_BecameInvalid(OpenDental.ValidEventArgs e){
			if(e.OnlyLocal){//This is deprecated and doesn't seem to be used at all anymore.
				if(!PrefsStartup()){//??
					return;
				}
				RefreshLocalData(InvalidType.AllLocal);//does local computer only
				return;
			}
			if(!e.ITypes.Contains((int)InvalidType.Date) //local refresh for dates is handled within ContrAppt, not here
				&& !e.ITypes.Contains((int)InvalidType.Task)//Tasks are not "cached" data.
				&& !e.ITypes.Contains((int)InvalidType.TaskPopup))
			{
				InvalidType[] itypeArray=new InvalidType[e.ITypes.Count];
				for(int i=0;i<itypeArray.Length;i++){
					itypeArray[i]=(InvalidType)e.ITypes[i];
				}
				RefreshLocalData(itypeArray);//does local computer
			}
			if(e.ITypes.Contains((int)InvalidType.Task) || e.ITypes.Contains((int)InvalidType.TaskPopup)) {
				//One of the two task lists needs to be refreshed on this instance of OD
				if(userControlTasks1.Visible) {
					userControlTasks1.RefreshTasks();
				}
				//See if FormTasks is currently open.
				if(ContrManage2!=null && ContrManage2.FormT!=null && !ContrManage2.FormT.IsDisposed) {
					ContrManage2.FormT.RefreshUserControlTasks();
				}
			}
			string itypeString="";
			for(int i=0;i<e.ITypes.Count;i++){
				if(i>0){
					itypeString+=",";
				}
				itypeString+=e.ITypes[i].ToString();
			}
			Signalod sig=new Signalod();
			sig.ITypes=itypeString;
			if(e.ITypes.Contains((int)InvalidType.Date)){
				sig.DateViewing=e.DateViewing;
			}
			else{
				sig.DateViewing=DateTime.MinValue;
			}
			sig.SigType=SignalType.Invalid;
			if(e.ITypes.Contains((int)InvalidType.Task) || e.ITypes.Contains((int)InvalidType.TaskPopup)){
				sig.TaskNum=e.TaskNum;
			}
			Signalods.Insert(sig);
		}

		///<summary>Update labels for the triage list.  Also updates the variable triageTime which is used to calculate mins behind.</summary>
		private void FillTriageLabels() {
			triageTime=Phones.GetTriageTime();//Update last triage time.
			FillTriageMinutes();//Updates labelWaitTime.
			int count=0;//Contains a count of tasks that do not have task notes attached.
			int countExcluded=0;//Contains count of tasks with notes attached.  Only used for display purposes when count is 0.
			List<long> taskNums=Phones.GetTriageTaskNums();
			List<TaskNote> taskNotes=TaskNotes.RefreshForTasks(taskNums);
			List<long> taskNumsNote=new List<long>();
			for(int i=0;i<taskNotes.Count;i++) {//Make a list of all the TaskNums in the note list.
				if(taskNumsNote.Contains(taskNotes[i].TaskNum)) {
					continue;
				}
				taskNumsNote.Add(taskNotes[i].TaskNum);
			}
			for(int i=0;i<taskNums.Count;i++) {//Increase the corresponding counts.
				if(taskNumsNote.Contains(taskNums[i])) {
					countExcluded++;
				}
				else {
					count++;
				}
			}
			string countStr="0";
			if(count>0) {//Triage show red so users notice more.
				countStr=count.ToString();
				labelTriage.ForeColor=Color.Firebrick;
			}
			else {
				if(countExcluded>0) {
					countStr="("+countExcluded.ToString()+")";
				}
				labelTriage.ForeColor=Color.Black;
			}
			labelTriage.Text="T:"+countStr;
		}

		///<summary>Update labelWaitTime for the triage list.  Gets called frequently, does not make a call to db.</summary>
		private void FillTriageMinutes() {
			if(triageTime.Year<1880) {
				labelWaitTime.Text="0m";
			}
			else {
				double mins=(DateTime.Now-triageTime).TotalMinutes;
				labelWaitTime.Text=((int)mins).ToString()+"m";
			}			
		}

		private void GotoModule_ModuleSelected(ModuleEventArgs e){
			if(e.DateSelected.Year>1880){
				AppointmentL.DateSelected=e.DateSelected;
			}
			if(e.SelectedAptNum>0){
				ContrApptSingle.SelectedAptNum=e.SelectedAptNum;
			}
			//patient can also be set separately ahead of time instead of doing it this way:
			if(e.PatNum !=0) {
				CurPatNum=e.PatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				FillPatientButton(pat);
			}
			UnselectActive();
			allNeutral();
			if(e.ClaimNum>0){
				myOutlookBar.SelectedIndex=e.IModule;
				ContrAccount2.Visible=true;
				this.ActiveControl=this.ContrAccount2;
				ContrAccount2.ModuleSelected(CurPatNum,e.ClaimNum);
			}
			else if(e.PinAppts.Count!=0){
				myOutlookBar.SelectedIndex=e.IModule;
				ContrAppt2.Visible=true;
				this.ActiveControl=this.ContrAppt2;
				ContrAppt2.ModuleSelected(CurPatNum,e.PinAppts);
			}
			else if(e.DocNum>0) {
				myOutlookBar.SelectedIndex=e.IModule;
				ContrImages2.Visible=true;
				this.ActiveControl=this.ContrImages2;
				ContrImages2.ModuleSelected(CurPatNum,e.DocNum);
			}
			else if(e.IModule!=-1){
				myOutlookBar.SelectedIndex=e.IModule;
				SetModuleSelected();
			}
			myOutlookBar.Invalidate();
		}

		///<summary>If this is initial call when opening program, then set sigListButs=null.  This will cause a call to be made to database to get current status of buttons.  Otherwise, it adds the signals passed in to the current state, then paints.</summary>
		private void FillSignalButtons(List<Signalod> sigListButs) {
			if(!lightSignalGrid1.Visible){//for faster eCW loading
				return;
			}
			if(sigListButs==null){
				SigButDefList=SigButDefs.GetByComputer(SystemInformation.ComputerName);
				lightSignalGrid1.SetButtons(SigButDefList);
				sigListButs=Signalods.RefreshCurrentButState();
			}
			SigElementDef element;
			SigButDef butDef;
			int row;
			Color color;
			for(int i=0;i<sigListButs.Count;i++){
				if(sigListButs[i].AckTime.Year>1880){//process ack
					int rowAck=lightSignalGrid1.ProcessAck(sigListButs[i].SignalNum);
					if(rowAck!=-1){
						butDef=SigButDefs.GetByIndex(rowAck,SigButDefList);
						if(butDef!=null){
							PaintOnIcon(butDef.SynchIcon,Color.White);
						}
					}
				}
				else{//process normal message
					row=0;
					color=Color.White;
					for(int e=0;e<sigListButs[i].ElementList.Length;e++){
						element=SigElementDefs.GetElement(sigListButs[i].ElementList[e].SigElementDefNum);
						if(element.LightRow!=0){
							row=element.LightRow;
						}
						if(element.LightColor.ToArgb()!=Color.White.ToArgb()){
							color=element.LightColor;
						}
					}
					if(row!=0 && color!=Color.White) {
						lightSignalGrid1.SetButtonActive(row-1,color,sigListButs[i]);
						butDef=SigButDefs.GetByIndex(row-1,SigButDefList);
						if(butDef!=null){
							try{
								PaintOnIcon(butDef.SynchIcon,color);
							}
							catch{
								MessageBox.Show("Error painting on program icon.  Probably too many non-ack'd messages.");
							}
						}
					}
				}
			}
		}

		///<summary>Pass in the cellNum as 1-based.</summary>
		private void PaintOnIcon(int cellNum,Color color){
			Graphics g;
			if(bitmapIcon==null){
				bitmapIcon=new Bitmap(16,16);
				g=Graphics.FromImage(bitmapIcon);
				g.FillRectangle(new SolidBrush(Color.White),0,0,15,15);
				//horizontal
				g.DrawLine(Pens.Black,0,0,15,0);
				g.DrawLine(Pens.Black,0,5,15,5);
				g.DrawLine(Pens.Black,0,10,15,10);
				g.DrawLine(Pens.Black,0,15,15,15);
				//vertical
				g.DrawLine(Pens.Black,0,0,0,15);
				g.DrawLine(Pens.Black,5,0,5,15);
				g.DrawLine(Pens.Black,10,0,10,15);
				g.DrawLine(Pens.Black,15,0,15,15);
				g.Dispose();
			}
			if(cellNum==0){
				return;
			}
			g=Graphics.FromImage(bitmapIcon);
			int x=0;
			int y=0;
			switch(cellNum){
				case 1: x=1; y=1; break;
				case 2: x=6; y=1; break;
				case 3: x=11; y=1; break;
				case 4: x=1; y=6; break;
				case 5: x=6; y=6; break;
				case 6: x=11; y=6; break;
				case 7: x=1; y=11; break;
				case 8: x=6; y=11; break;
				case 9: x=11; y=11; break;
			}
			g.FillRectangle(new SolidBrush(color),x,y,4,4);
			Icon=Icon.FromHandle(bitmapIcon.GetHicon());
			g.Dispose();
		}

		private void lightSignalGrid1_ButtonClick(object sender,OpenDental.UI.ODLightSignalGridClickEventArgs e) {
			if(e.ActiveSignal!=null){//user trying to ack an existing light signal
				Signalods.AckButton(e.ButtonIndex+1,signalLastRefreshed);
				//then, manually ack the light on this computer.  The second ack in a few seconds will be ignored.
				lightSignalGrid1.SetButtonActive(e.ButtonIndex,Color.White,null);
				SigButDef butDef=SigButDefs.GetByIndex(e.ButtonIndex,SigButDefList);
				if(butDef!=null) {
					PaintOnIcon(butDef.SynchIcon,Color.White);
				}
				return;
			}
			if(e.ButtonDef==null || e.ButtonDef.ElementList.Length==0){//there is no signal to send
				return;
			}
			//user trying to send a signal
			Signalod sig=new Signalod();
			sig.SigType=SignalType.Button;
			//sig.ToUser=sigElementDefUser[listTo.SelectedIndex].SigText;
			//sig.FromUser=sigElementDefUser[listFrom.SelectedIndex].SigText;
			//need to do this all as a transaction?
			Signalods.Insert(sig);
			int row=0;
			Color color=Color.White;
			SigElementDef def;
			SigElement element;
			SigButDefElement[] butElements=SigButDefElements.GetForButton(e.ButtonDef.SigButDefNum);
			for(int i=0;i<butElements.Length;i++){
				element=new SigElement();
				element.SigElementDefNum=butElements[i].SigElementDefNum;
				element.SignalNum=sig.SignalNum;
				SigElements.Insert(element);
				if(SigElementDefs.GetElement(element.SigElementDefNum).SigElementType==SignalElementType.User){
					sig.ToUser=SigElementDefs.GetElement(element.SigElementDefNum).SigText;
					Signalods.Update(sig);
				}
				def=SigElementDefs.GetElement(element.SigElementDefNum);
				if(def.LightRow!=0) {
					row=def.LightRow;
				}
				if(def.LightColor.ToArgb()!=Color.White.ToArgb()) {
					color=def.LightColor;
				}
			}
			sig.ElementList=new SigElement[0];//we don't really care about these
			if(row!=0 && color!=Color.White) {
				lightSignalGrid1.SetButtonActive(row-1,color,sig);//this just makes it seem more responsive.
				//we can skip painting on the icon
			}
		}
	
		///<summary>Called every time timerSignals_Tick fires.  Usually about every 5-10 seconds.</summary>
		public void ProcessSignals(){
			try {
				List<Signalod> sigList=Signalods.RefreshTimed(signalLastRefreshed);//this also attaches all elements to their sigs
				if(PrefC.GetBool(PrefName.DockPhonePanelShow)) {
					FillTriageMinutes();
				}
				if(sigList.Count==0) {
					return;
				}
				if(Security.CurUser==null) {
					return;
				}
				//look for shutdown signal
				for(int i=0;i<sigList.Count;i++) {
					if(sigList[i].ITypes==((int)InvalidType.ShutDownNow).ToString()) {
						timerSignals.Enabled=false;//quit receiving signals.
						//close the webcam if present so that it can be updated too.
						if(PrefC.GetBool(PrefName.DockPhonePanelShow)) {
							Process[] processes=Process.GetProcessesByName("WebCamOD");
							for(int p=0;p<processes.Length;p++) {
								processes[p].Kill();
							}
						}
						//start the thread that will kill the application
						Thread killThread=new Thread(new ThreadStart(KillThread));
						killThread.Start();
						string msg="";
						if(Process.GetCurrentProcess().ProcessName=="OpenDental") {
							msg+="All copies of Open Dental ";
						}
						else {
							msg+=Process.GetCurrentProcess().ProcessName+" ";
						}
						msg+=Lan.g(this,"will shut down in 15 seconds.  Quickly click OK on any open windows with unsaved data.");
						MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(msg);
						msgbox.Size=new Size(300,300);
						msgbox.TopMost=true;
						msgbox.ShowDialog();
						return;
					}
				}
				if(sigList[sigList.Count-1].AckTime.Year>1880) {
					signalLastRefreshed=sigList[sigList.Count-1].AckTime;
				}
				else {
					signalLastRefreshed=sigList[sigList.Count-1].SigDateTime;
				}
				if(ContrAppt2.Visible && Signalods.ApptNeedsRefresh(sigList,AppointmentL.DateSelected.Date)) {
					ContrAppt2.RefreshPeriod();
				}
				bool areAnySignalsTasks=false;
				for(int i=0;i<sigList.Count;i++) {
					if(sigList[i].ITypes==((int)InvalidType.Task).ToString()
						|| sigList[i].ITypes==((int)InvalidType.TaskPopup).ToString()) {
						areAnySignalsTasks=true;
					}
				}
				List<Task> tasksPopup=Signalods.GetNewTaskPopupsThisUser(sigList,Security.CurUser.UserNum);
				if(tasksPopup.Count>0) {
					for(int i=0;i<tasksPopup.Count;i++) {
						//Even though this is triggered to popup, if this is my own task, then do not popup.
						List<TaskNote> notesForThisTask=TaskNotes.GetForTask(tasksPopup[i].TaskNum);
						if(notesForThisTask.Count==0) {//'sender' is the usernum on the task
							if(tasksPopup[i].UserNum==Security.CurUser.UserNum) {
								continue;
							}
						}
						else {//'sender' is the user on the last added note
							if(notesForThisTask[notesForThisTask.Count-1].UserNum==Security.CurUser.UserNum) {
								continue;
							}
						}
						if(tasksPopup[i].TaskListNum!=Security.CurUser.TaskListInBox//if not my inbox
							&& userControlTasks1.PopupsAreBlocked)//and popups blocked
						{
							continue;//no sound or popup
							//in other words, popups will always show for my inbox even if popups blocked.
						}
						System.Media.SoundPlayer soundplay=new SoundPlayer(Properties.Resources.notify);
						soundplay.Play();
						this.BringToFront();//don't know if this is doing anything.
						FormTaskEdit FormT=new FormTaskEdit(tasksPopup[i],tasksPopup[i].Copy());
						FormT.IsPopup=true;
						FormT.Closing+=new CancelEventHandler(TaskGoToEvent);
						FormT.Show();//non-modal
					}
				}
				if(areAnySignalsTasks || tasksPopup.Count>0) {
					if(userControlTasks1.Visible) {
						userControlTasks1.RefreshTasks();
					}
					//See if FormTasks is currently open.
					if(ContrManage2!=null && ContrManage2.FormT!=null && !ContrManage2.FormT.IsDisposed) {
						ContrManage2.FormT.RefreshUserControlTasks();
					}
				}
				if(PrefC.GetBool(PrefName.DockPhonePanelShow) && areAnySignalsTasks) {
					FillTriageLabels();
				}
				List<int> itypes=Signalods.GetInvalidTypes(sigList);
				InvalidType[] itypeArray=new InvalidType[itypes.Count];
				for(int i=0;i<itypeArray.Length;i++) {
					itypeArray[i]=(InvalidType)itypes[i];
				}
				//InvalidTypes invalidTypes=Signalods.GetInvalidTypes(sigList);
				if(itypes.Count>0) {//invalidTypes!=0){
					RefreshLocalData(itypeArray);
				}
				List<Signalod> sigListButs=Signalods.GetButtonSigs(sigList);
				ContrManage2.LogMsgs(sigListButs);
				FillSignalButtons(sigListButs);
				//Need to add a test to this: do not play messages that are over 2 minutes old.
				Thread newThread=new Thread(new ParameterizedThreadStart(PlaySounds));
				newThread.Start(sigListButs);
			}
			catch {
				signalLastRefreshed=DateTime.Now;
			}
		}

		public void TaskGoToEvent(object sender, CancelEventArgs e){
			FormTaskEdit FormT=(FormTaskEdit)sender;
			TaskGoTo(FormT.GotoType,FormT.GotoKeyNum);
		}

		///<summary>Gives users 15 seconds to finish what they were doing before the program shuts down.</summary>
		private void KillThread() {
			//Application.DoEvents();
			DateTime now=DateTime.Now;
			while(DateTime.Now < now.AddSeconds(15)) {
				Application.DoEvents();
			}
			//Thread.Sleep(30000);//30 sec
			Application.Exit();
		}

		private void PlaySounds(Object objSignalList){
			List<Signalod> signalList=(List<Signalod>)objSignalList;
			string strSound;
			byte[] rawData;
			MemoryStream stream=null;
			SoundPlayer simpleSound=null;
			try {
				//loop through each signal
				for(int s=0;s<signalList.Count;s++) {
					if(signalList[s].AckTime.Year>1880) {
						continue;//don't play any sounds for acks.
					}
					if(s>0) {
						Thread.Sleep(1000);//pause 1 second between signals.
					}
					//play all the sounds.
					for(int e=0;e<signalList[s].ElementList.Length;e++) {
						strSound=SigElementDefs.GetElement(signalList[s].ElementList[e].SigElementDefNum).Sound;
						if(strSound=="") {
							continue;
						}
						try {
							rawData=Convert.FromBase64String(strSound);
							stream=new MemoryStream(rawData);
							simpleSound = new SoundPlayer(stream);
							simpleSound.PlaySync();//sound will finish playing before thread continues
						}
						catch {
							//do nothing
						}
					}
				}
			}
			finally {
				if(stream!=null) {
					stream.Dispose();
				}
				if(simpleSound!=null) {
					simpleSound.Dispose();
				}
			}
		}

		private void myOutlookBar_ButtonClicked(object sender, OpenDental.ButtonClicked_EventArgs e){
			switch(myOutlookBar.SelectedIndex){
				case 0:
					if(!Security.IsAuthorized(Permissions.AppointmentsModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 1:
					if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {//if red emergency button is on
						if(Security.IsAuthorized(Permissions.EhrEmergencyAccess,true)) {
							break;//No need to check other permissions.
						}
					}
					//Whether or not they were authorized by the special situation above,
					//they can get into the Family module with the ordinary permissions.
					if(!Security.IsAuthorized(Permissions.FamilyModule)) {
						e.Cancel=true;
						return;
					}
					break;
				case 2:
					if(!Security.IsAuthorized(Permissions.AccountModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 3:
					if(!Security.IsAuthorized(Permissions.TPModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 4:
					if(!Security.IsAuthorized(Permissions.ChartModule)){
						e.Cancel=true;
						return;
					}
					SecurityLogs.MakeLogEntry(Permissions.ChartModule,CurPatNum,"");
					break;
				case 5:
					if(!Security.IsAuthorized(Permissions.ImagesModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 6:
					if(!Security.IsAuthorized(Permissions.ManageModule)){
						e.Cancel=true;
						return;
					}
					break;
			}
			UnselectActive();
			allNeutral();
			SetModuleSelected();
		}

		///<summary>Sets the currently selected module based on the selectedIndex of the outlook bar. If selectedIndex is -1, which might happen if user does not have permission to any module, then this does nothing.</summary>
		private void SetModuleSelected(){
			switch(myOutlookBar.SelectedIndex){
				case 0:
					ContrAppt2.InitializeOnStartup();
					ContrAppt2.Visible=true;
					this.ActiveControl=this.ContrAppt2;
					ContrAppt2.ModuleSelected(CurPatNum);
					break;
				case 1:
					if(Programs.UsingEcwTight()) {
						//ContrFamily2Ecw.InitializeOnStartup();
						ContrFamily2Ecw.Visible=true;
						this.ActiveControl=this.ContrFamily2Ecw;
						ContrFamily2Ecw.ModuleSelected(CurPatNum);
					}
					else {
						ContrFamily2.InitializeOnStartup();
						ContrFamily2.Visible=true;
						this.ActiveControl=this.ContrFamily2;
						ContrFamily2.ModuleSelected(CurPatNum);
					}
					break;
				case 2:
					ContrAccount2.InitializeOnStartup();
					ContrAccount2.Visible=true;
					this.ActiveControl=this.ContrAccount2;
					ContrAccount2.ModuleSelected(CurPatNum);
					break;
				case 3:
					ContrTreat2.InitializeOnStartup();
					ContrTreat2.Visible=true;
					this.ActiveControl=this.ContrTreat2;
					ContrTreat2.ModuleSelected(CurPatNum);
					break;
				case 4:
					ContrChart2.InitializeOnStartup();
					ContrChart2.Visible=true;
					this.ActiveControl=this.ContrChart2;
					ContrChart2.ModuleSelected(CurPatNum);
					break;
				case 5:
					ContrImages2.InitializeOnStartup();
					ContrImages2.Visible=true;
					this.ActiveControl=this.ContrImages2;
					ContrImages2.ModuleSelected(CurPatNum);
					break;
				case 6:
					//ContrManage2.InitializeOnStartup();//This gets done earlier.
					ContrManage2.Visible=true;
					this.ActiveControl=this.ContrManage2;
					ContrManage2.ModuleSelected(CurPatNum);
					break;
			}
		}

		private void allNeutral(){
			ContrAppt2.Visible=false;
			ContrFamily2.Visible=false;
			ContrFamily2Ecw.Visible=false;
			ContrAccount2.Visible=false;
			ContrTreat2.Visible=false;
			ContrChart2.Visible=false;
			ContrImages2.Visible=false;
			ContrManage2.Visible=false;
		}

		private void UnselectActive(){
			if(ContrAppt2.Visible){
				ContrAppt2.ModuleUnselected();
			}
			if(ContrFamily2.Visible){
				ContrFamily2.ModuleUnselected();
			}
			if(ContrFamily2Ecw.Visible) {
				//ContrFamily2Ecw.ModuleUnselected();
			}
			if(ContrAccount2.Visible){
				ContrAccount2.ModuleUnselected();
			}
			if(ContrTreat2.Visible){
				ContrTreat2.ModuleUnselected();
			}
			if(ContrChart2.Visible){
				ContrChart2.ModuleUnselected();
			}
			if(ContrImages2.Visible){
				ContrImages2.ModuleUnselected();
			}
		}

		///<Summary>This also passes CurPatNum down to the currently selected module (except the Manage module).</Summary>
		private void RefreshCurrentModule(){
			if(ContrAppt2.Visible){
				ContrAppt2.ModuleSelected(CurPatNum);
			}
			if(ContrFamily2.Visible){
				ContrFamily2.ModuleSelected(CurPatNum);
			}
			if(ContrFamily2Ecw.Visible) {
				ContrFamily2Ecw.ModuleSelected(CurPatNum);
			}
			if(ContrAccount2.Visible){
				ContrAccount2.ModuleSelected(CurPatNum);
			}
			if(ContrTreat2.Visible){
				ContrTreat2.ModuleSelected(CurPatNum);
			}
			if(ContrChart2.Visible){
				ContrChart2.ModuleSelected(CurPatNum);
			}
			if(ContrImages2.Visible){
				ContrImages2.ModuleSelected(CurPatNum);
			}
			if(ContrManage2.Visible){
				ContrManage2.ModuleSelected(CurPatNum);
			}
		}

		/// <summary>sends function key presses to the appointment module and chart module</summary>
		private void FormOpenDental_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(ContrAppt2.Visible && e.KeyCode>=Keys.F1 && e.KeyCode<=Keys.F12){
				ContrAppt2.FunctionKeyPress(e.KeyCode);
			}
			if(ContrChart2.Visible && e.KeyCode>=Keys.F1 && e.KeyCode<=Keys.F12) {
				ContrChart2.FunctionKeyPressContrChart(e.KeyCode);
			}
			Keys keys=e.KeyCode;
			//Ctrl-Alt-R is supposed to show referral window, but it doesn't work on some computers.
			if((e.Modifiers&Keys.Alt)==Keys.Alt
				&& (e.Modifiers&Keys.Control)==Keys.Control
				&& (e.KeyCode&Keys.R)==Keys.R
				&& CurPatNum!=0)
			{
				FormReferralsPatient FormRE=new FormReferralsPatient();
				FormRE.PatNum=CurPatNum;
				FormRE.ShowDialog();
			}
			//so we're also going to use Ctrl-X to show the referral window.
			if((e.Modifiers&Keys.Control)==Keys.Control
				&& (e.KeyCode&Keys.X)==Keys.X
				&& CurPatNum!=0) 
			{
				FormReferralsPatient FormRE=new FormReferralsPatient();
				FormRE.PatNum=CurPatNum;
				FormRE.ShowDialog();
			}
		}

		private void timerTimeIndic_Tick(object sender, System.EventArgs e){
			//every minute:
			if(WindowState!=FormWindowState.Minimized
				&& ContrAppt2.Visible){
				ContrAppt2.TickRefresh();
      }
		}

		private void timerSignals_Tick(object sender, System.EventArgs e) {
			//typically every 4 seconds:
			ProcessSignals();
		}

		private void timerDisabledKey_Tick(object sender,EventArgs e) {
			//every 10 minutes:
			if(PrefC.GetBoolSilent(PrefName.RegistrationKeyIsDisabled,false)) {
				MessageBox.Show("Registration key has been disabled.  You are using an unauthorized version of this program.","Warning",
					MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
		}

		private void timerHeartBeat_Tick(object sender,EventArgs e) {
			//every 3 minutes:
			try {
				Computers.UpdateHeartBeat(Environment.MachineName);
			}
			catch { }
		}

		///<summary>Gets the encrypted connection string for the Oracle database from a config file.</summary>
		public bool GetOraConfig() {
			if(!File.Exists("ODOraConfig.xml")) {
				return false;
			}
			try {
				XmlDocument document=new XmlDocument();
				document.Load("ODOraConfig.xml");
				XPathNavigator Navigator=document.CreateNavigator();
				XPathNavigator nav;
				nav=Navigator.SelectSingleNode("//DatabaseConnection");
				if(nav!=null) {
					connStr=nav.SelectSingleNode("ConnectionString").Value;
					key=nav.SelectSingleNode("Key").Value;
				}
				DataConnection.DBtype=DatabaseType.Oracle;
				return true;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		///<summary>Decrypt the connection string and try to connect to the database directly. Only called if using a connection string and ChooseDatabase is not to be shown. Must call GetOraConfig first.</summary>
		public bool TryWithConnStr() {
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			try {
				if(connStr!=null) {
#if ORA_DB
					OD_CRYPTO.Decryptor crypto=new OD_CRYPTO.Decryptor();
					dconnStr=crypto.Decrypt(connStr,key);
					crypto=null;
					dcon.SetDb(dconnStr,"",DatabaseType.Oracle);
#endif
				}
				//a direct connection does not utilize lower privileges.
				RemotingClient.RemotingRole=RemotingRole.ClientDirect;
				return true;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void userControlTasks1_GoToChanged(object sender,EventArgs e) {
			TaskGoTo(userControlTasks1.GotoType,userControlTasks1.GotoKeyNum);
		}

		private void TaskGoTo(TaskObjectType taskOT,long keyNum){
			if(taskOT==TaskObjectType.None) {
				return;
			}
			if(taskOT==TaskObjectType.Patient) {
				if(keyNum!=0) {
					CurPatNum=keyNum;
					Patient pat=Patients.GetPat(CurPatNum);
					RefreshCurrentModule();
					FillPatientButton(pat);
				}
			}
			if(taskOT==TaskObjectType.Appointment) {
				if(keyNum!=0) {
					Appointment apt=Appointments.GetOneApt(keyNum);
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
					CurPatNum=apt.PatNum;//OnPatientSelected(apt.PatNum);
					GotoModule.GotoAppointment(dateSelected,apt.AptNum);
				}
			}
		}

		private void butBigPhones_Click(object sender,EventArgs e) {
			if(formPhoneTiles==null || formPhoneTiles.IsDisposed) {
				formPhoneTiles=new FormPhoneTiles();
				formPhoneTiles.GoToChanged += new System.EventHandler(this.phonePanel_GoToChanged);
				formPhoneTiles.Show();
				Rectangle rect=System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
				formPhoneTiles.Location=new Point((rect.Width-formPhoneTiles.Width)/2+rect.X,10);
				formPhoneTiles.BringToFront();
			}
			else {
				formPhoneTiles.Show();
				formPhoneTiles.BringToFront();
			}
		}

		private void butTriage_Click(object sender,EventArgs e) {
			ContrManage2.JumpToTriageTaskWindow();
		}

		private void phonePanel_GoToChanged(object sender,EventArgs e) {
			if(formPhoneTiles.GotoPatNum!=0) {
				CurPatNum=formPhoneTiles.GotoPatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				RefreshCurrentModule();
				FillPatientButton(pat);
			}
		}

		private void phoneSmall_GoToChanged(object sender,EventArgs e) {
			if(phoneSmall.GotoPatNum==0) {
				return;
			}
			CurPatNum=phoneSmall.GotoPatNum;
			Patient pat=Patients.GetPat(CurPatNum);
			RefreshCurrentModule();
			FillPatientButton(pat);
			Commlog commlog=Commlogs.GetIncompleteEntry(Security.CurUser.UserNum,CurPatNum);
			PhoneEmpDefault ped=PhoneEmpDefaults.GetByExtAndEmp(phoneSmall.Extension,Security.CurUser.EmployeeNum);
			if(ped!=null && ped.IsTriageOperator) {
				Task task=new Task();
				task.TaskListNum=-1;//don't show it in any list yet.
				Tasks.Insert(task);
				Task taskOld=task.Copy();
				task.KeyNum=CurPatNum;
				task.ObjectType=TaskObjectType.Patient;
				task.TaskListNum=1697;//Hardcoded for internal Triage task list.
				task.UserNum=Security.CurUser.UserNum;
				task.Descript=Phones.GetPhoneForExtension(Phones.GetPhoneList(),phoneSmall.Extension).CustomerNumberRaw+" ";//Prefill description with customers number.
				FormTaskEdit FormTE=new FormTaskEdit(task,taskOld);
				FormTE.IsNew=true;
				FormTE.Show();
			}
			else {//Not a triage operator.
				if(commlog==null) {
					commlog = new Commlog();
					commlog.PatNum = CurPatNum;
					commlog.CommDateTime = DateTime.Now;
					commlog.CommType =Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
					commlog.Mode_=CommItemMode.Phone;
					commlog.SentOrReceived=CommSentOrReceived.Received;
					commlog.UserNum=Security.CurUser.UserNum;
					FormCommItem FormCI=new FormCommItem(commlog);
					FormCI.IsNew = true;
					FormCI.ShowDialog();
					if(FormCI.DialogResult==DialogResult.OK) {
						RefreshCurrentModule();
					}
				}
				else {
					FormCommItem FormCI=new FormCommItem(commlog);
					FormCI.ShowDialog();
					if(FormCI.DialogResult==DialogResult.OK) {
						RefreshCurrentModule();
					}
				}
			}
		}

		private delegate void DelegateSetString(String str,bool isBold,Color color);//typically at namespace level rather than class level

		///<summary>Always called using ThreadVM.</summary>
		private void ThreadVM_SetLabelMsg() {
#if DEBUG
      //Because path is not valid when Jordan is debugging from home.
#else
			while(true) {
				string msg;
				int msgCount;
				bool isBold;
				Color color;
				try {
					if(!Directory.Exists(PhoneUI.PathPhoneMsg)) {
						msg="error";
						isBold=false;
						color=Color.Black;
						this.Invoke(new DelegateSetString(SetString),new Object[] { msg,isBold,color });
						return;
					}
					msgCount=Directory.GetFiles(PhoneUI.PathPhoneMsg,"*.txt").Length;
					if(msgCount==0) {
						msg="V:0";
						isBold=false;
						color=Color.Black;
						this.Invoke(new DelegateSetString(SetString),new Object[] { msg,isBold,color });
					}
					else {
						msg="V:"+msgCount.ToString();
						isBold=true;
						color=Color.Firebrick;
						this.Invoke(new DelegateSetString(SetString),new Object[] { msg,isBold,color });
					}
				}
				catch(ThreadAbortException) {//OnClosing will abort the thread.
					return;//Exits the loop.
				}
				catch(Exception ex){
					throw ex;
				}
				Thread.Sleep(3000);
			}
#endif
		}

		///<summary>Called from worker thread using delegate and Control.Invoke</summary>
		private void SetString(String str,bool isBold,Color color) {
			labelMsg.Text=str;
			if(isBold) {
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,8.25f,FontStyle.Bold);
			}
			else {
				labelMsg.Font=new Font(FontFamily.GenericSansSerif,8.25f,FontStyle.Regular);
			}
			labelMsg.ForeColor=color;
		}

		/*private void moduleStaffBilling_GoToChanged(object sender,GoToEventArgs e) {
			if(e.PatNum==0) {
				return;
			}
			CurPatNum=e.PatNum;
			Patient pat=Patients.GetPat(CurPatNum);
			RefreshCurrentModule();
			FillPatientButton(CurPatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber);
		}*/

		///<summary>This is recursive</summary>
		private void TranslateMenuItem(MenuItem menuItem){
			//first translate any child menuItems
			foreach(MenuItem mi in menuItem.MenuItems){
				TranslateMenuItem(mi);
			}
			//then this menuitem
			Lan.C("MainMenu",menuItem);
		}

		#region MenuEvents
		private void menuItemLogOff_Click(object sender, System.EventArgs e) {
			LogOffNow();
		}

		//File
		private void menuItemPassword_Click(object sender,EventArgs e) {
			//no security blocking because everyone is allowed to change their own password.
			FormUserPassword FormU=new FormUserPassword(false,Security.CurUser.UserName);
			FormU.ShowDialog();
			if(FormU.DialogResult==DialogResult.Cancel) {
				return;
			}
			Security.CurUser.Password=FormU.hashedResult;
			if(PrefC.GetBool(PrefName.PasswordsMustBeStrong)) {
				Security.CurUser.PasswordIsStrong=true;
			}
			Userods.Update(Security.CurUser);
			DataValid.SetInvalid(InvalidType.Security);
		}

		private void menuItemPrinter_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPrinterSetup FormPS=new FormPrinterSetup();
			FormPS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Printers");
		}

		private void menuItemGraphics_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			FormGraphics fg=new FormGraphics();
			fg.ShowDialog();
			Cursor=Cursors.Default;
			if(fg.DialogResult==DialogResult.OK) {
				ContrChart2.InitializeLocalData();
				RefreshCurrentModule();
			}
		}

		private void menuItemConfig_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.ChooseDatabase)){
				return;
			}
			SecurityLogs.MakeLogEntry(Permissions.ChooseDatabase,0,"");//make the entry before switching databases.
			FormChooseDatabase FormC=new FormChooseDatabase();//Choose Database is read only from this menu.
			FormC.IsAccessedFromMainMenu=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				return;
			}
			CurPatNum=0;
			RefreshCurrentModule();//clumsy but necessary. Sets child PatNums to 0.
			if(!PrefsStartup()){
				return;
			}
			RefreshLocalData(InvalidType.AllLocal);
			//RefreshCurrentModule();
			menuItemLogOff_Click(this,e);//this is a quick shortcut.
		}

		private void menuItemExit_Click(object sender, System.EventArgs e) {
			//Thread2.Abort();
			//if(this.TcpListener2!=null){
			//	this.TcpListener2.Stop();  
			//}
			Application.Exit();
		}

		//FormBackupJobsSelect FormBJS=new FormBackupJobsSelect();
		//FormBJS.ShowDialog();	

		//Setup
		private void menuItemApptFieldDefs_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormApptFieldDefs FormA=new FormApptFieldDefs();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Appointment Field Defs");
		}

		private void menuItemApptRules_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormApptRules FormA=new FormApptRules();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Appointment Rules");
		}

		private void menuItemApptViews_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormApptViews FormAV=new FormApptViews();
			FormAV.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Appointment Views");
		}

		private void menuItemAutoCodes_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormAutoCode FormAC=new FormAutoCode();
			FormAC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Auto Codes");
		}

		private void menuItemAutomation_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormAutomation FormA=new FormAutomation();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Automation");
		}

		private void menuItemAutoNotes_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormAutoNotes FormA=new FormAutoNotes();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Auto Notes");
		}

		private void menuItemClaimForms_Click(object sender, System.EventArgs e) {
			if(!PrefC.UsingAtoZfolder){
				MsgBox.Show(this,"Claim Forms feature is unavailable when data path A to Z folder is disabled.");
				return;
			}
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormClaimForms FormCF=new FormClaimForms();
			FormCF.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Claim Forms");
		}

		private void menuItemClearinghouses_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormClearinghouses FormC=new FormClearinghouses();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Clearinghouses");
		}

		private void menuItemComputers_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormComputers FormC=new FormComputers();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Computers");
		}

		private void menuItemDataPath_Click(object sender, System.EventArgs e) {
			//security is handled from within the form.
			FormPath FormP=new FormPath();
			FormP.ShowDialog();
			ContrImages2.Enabled=PrefC.UsingAtoZfolder;
			//menuItemClaimForms.Visible=PrefC.UsingAtoZfolder;
			CheckCustomReports();
			this.RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Data Path");	
		}

		private void menuItemDefinitions_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDefinitions FormD=new FormDefinitions(DefCat.AccountColors);//just the first cat.
			FormD.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Definitions");
		}

		private void menuItemDisplayFields_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDisplayFieldCategories FormD=new FormDisplayFieldCategories();
			FormD.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Display Fields");
		}

		private void menuItemEmail_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEmailSetup FormE=new FormEmailSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Email");
		}

		private void menuItemEHR_Click(object sender,EventArgs e) {
			//if(!Security.IsAuthorized(Permissions.Setup)) {
			//  return;
			//}
			FormEhrSetup FormE=new FormEhrSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"EHR");
		}

		private void menuItemFeeScheds_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormFeeScheds FormF=new FormFeeScheds();
			FormF.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Fee Schedules");
		}

		private void menuItemImaging_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormImagingSetup FormI=new FormImagingSetup();
			FormI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Imaging");
		}

		private void menuItemInsCats_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormInsCatsSetup FormE=new FormInsCatsSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Insurance Categories");
		}

		private void menuItemInsFilingCodes_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormInsFilingCodes FormF=new FormInsFilingCodes();
			FormF.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Insurance Filing Codes");
		}

		private void menuItemLaboratories_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormLaboratories FormL=new FormLaboratories();
			FormL.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Laboratories");
		}

		private void menuItemLetters_Click(object sender,EventArgs e) {
			FormLetters FormL=new FormLetters();
			FormL.ShowDialog();
		}

		private void menuItemMessaging_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormMessagingSetup FormM=new FormMessagingSetup();
			FormM.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Messaging");
		}

		private void menuItemMessagingButs_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormMessagingButSetup FormM=new FormMessagingButSetup();
			FormM.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Messaging");
		}

		private void menuItemMisc_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormMisc FormM=new FormMisc();
			if(FormM.ShowDialog()!=DialogResult.OK) {
				return;
			}
			if(userControlTasks1.Visible) {
				userControlTasks1.InitializeOnStartup();
			}
			//menuItemMergeDatabases.Visible=PrefC.GetBool(PrefName.RandomPrimaryKeys");
			//if(timerSignals.Interval==0){
			if(PrefC.GetInt(PrefName.ProcessSigsIntervalInSecs)==0){
				timerSignals.Enabled=false;
			}
			else{
				timerSignals.Interval=PrefC.GetInt(PrefName.ProcessSigsIntervalInSecs)*1000;
				timerSignals.Enabled=true;
			}
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Misc");
		}

		private void menuItemModules_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormModuleSetup FormM=new FormModuleSetup();
			if(FormM.ShowDialog()!=DialogResult.OK) {
				return;
			}
			FillPatientButton(Patients.GetPat(CurPatNum));
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Modules");
		}

		private void menuItemOperatories_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormOperatories FormO=new FormOperatories();
			FormO.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Operatories");
		}

		private void menuItemPatFieldDefs_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormPatFieldDefs FormP=new FormPatFieldDefs();
			FormP.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Patient Field Defs");
		}

		private void menuItemPayerIDs_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormElectIDs FormE=new FormElectIDs();
			FormE.IsSelectMode=false;
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Payer IDs");
		}

		private void menuItemPractice_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPractice FormPr=new FormPractice();
			FormPr.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Practice Info");
		}

		private void menuItemProblems_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDiseaseDefs FormD=new FormDiseaseDefs();
			FormD.ShowDialog();
			//RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Disease Defs");
		}

		private void menuItemProcedureButtons_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormProcButtons FormPB=new FormProcButtons();
			FormPB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Procedure Buttons");	
		}

		private void menuItemLinks_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormProgramLinks FormPL=new FormProgramLinks();
			FormPL.ShowDialog();
			ContrChart2.InitializeLocalData();//for eCW
			LayoutToolBar();
			if(CurPatNum>0) {
				Patient pat=Patients.GetPat(CurPatNum);
				FillPatientButton(pat);
			}
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Program Links");
		}

		/*
		private void menuItem_ProviderAllocatorSetup_Click(object sender,EventArgs e) {
			// Check Permissions
			if(!Security.IsAuthorized(Permissions.Setup)) {
				// Failed security prompts message box. Consider adding overload to not show message.
				//MessageBox.Show("Not Authorized to Run Setup for Provider Allocation Tool");
				return;
			}
			Reporting.Allocators.MyAllocator1.FormInstallAllocator_Provider fap = new OpenDental.Reporting.Allocators.MyAllocator1.FormInstallAllocator_Provider();
			fap.ShowDialog();
		}*/

		private void menuItemQuestions_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormQuestionDefs FormQ=new FormQuestionDefs();
			FormQ.ShowDialog();
			//RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Questionnaire");
		}

		private void menuItemRecall_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormRecallSetup FormRS=new FormRecallSetup();
			FormRS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Recall");	
		}

		private void menuItemRecallTypes_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormRecallTypes FormRT=new FormRecallTypes();
			FormRT.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Recall Types");	
		}

		private void menuItemReplication_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)) {
				return;
			}
			FormReplicationSetup FormRS=new FormReplicationSetup();
			FormRS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,0,"Replication setup.");
		}

		private void menuItemRequirementsNeeded_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormReqNeededs FormR=new FormReqNeededs();
			FormR.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Requirements Needed");
		}

		private void menuItemSched_Click(object sender,EventArgs e) {
			//anyone should be able to view. Security must be inside schedule window.
			//if(!Security.IsAuthorized(Permissions.Schedules)) {
			//	return;
			//}
			FormSchedule FormS=new FormSchedule();
			FormS.ShowDialog();
			//SecurityLogs.MakeLogEntry(Permissions.Schedules,0,"");
		}

		/*private void menuItemBlockoutDefault_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Blockouts)) {
				return;
			}
			FormSchedDefault FormSD=new FormSchedDefault(ScheduleType.Blockout);
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,0,"Default");
		}*/

		private void menuItemSecurity_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)){
				return;
			}
			FormSecurity FormS=new FormSecurity(); 
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,0,"");
		}

		private void menuItemSheets_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormSheetDefs FormSD=new FormSheetDefs();
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Sheets");
		}

		//This shows as "Show Features"
		private void menuItemEasy_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormEasy FormE=new FormEasy();
			FormE.ShowDialog();
			ContrAccount2.LayoutToolBar();//for repeating charges
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Show Features");
		}

		private void menuItemTimeCards_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormTimeCardSetup FormTCS=new FormTimeCardSetup();
			FormTCS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Time Card Setup");
		}

		//Lists

		private void menuItemProcCodes_Click(object sender, System.EventArgs e) {
			//security handled within form
			FormProcCodes FormP=new FormProcCodes();
			FormP.ShowDialog();	
		}

		private void menuItemClinics_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormClinics FormC=new FormClinics();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Clinics");
		}
		
		private void menuItemContacts_Click(object sender, System.EventArgs e) {
			FormContacts FormC=new FormContacts();
			FormC.ShowDialog();
		}

		private void menuItemCounties_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormCounties FormC=new FormCounties();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Counties");
		}

		private void menuItemSchoolClass_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormSchoolClasses FormS=new FormSchoolClasses();
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Dental School Classes");
		}

		private void menuItemSchoolCourses_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormSchoolCourses FormS=new FormSchoolCourses();
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Dental School Courses");
		}

		private void menuItemEmployees_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEmployeeSelect FormEmp=new FormEmployeeSelect();
			FormEmp.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Employees");	
		}

		private void menuItemEmployers_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.ShowDialog();
		}

		private void menuItemInstructors_Click(object sender, System.EventArgs e) {
			/*if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormInstructors FormI=new FormInstructors();
			FormI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Dental School Instructors");*/
		}

		private void menuItemCarriers_Click(object sender, System.EventArgs e) {
			FormCarriers FormC=new FormCarriers();
			FormC.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemInsPlans_Click(object sender, System.EventArgs e) {
			FormInsPlans FormIP = new FormInsPlans();
			FormIP.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemLabCases_Click(object sender,EventArgs e) {
			FormLabCases FormL=new FormLabCases();
			FormL.ShowDialog();
			if(FormL.GoToAptNum!=0) {
				Appointment apt=Appointments.GetOneApt(FormL.GoToAptNum);
				Patient pat=Patients.GetPat(apt.PatNum);
				PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(pat);
				//if(PatientSelected!=null){
				//	PatientSelected(this,eArgs);
				//}
				Contr_PatientSelected(this,eArgs);
				//OnPatientSelected(pat.PatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber);
				GotoModule.GotoAppointment(apt.AptDateTime,apt.AptNum);
			}
		}

		private void menuItemMedications_Click(object sender, System.EventArgs e) {
			FormMedications FormM=new FormMedications();
			FormM.ShowDialog();
		}

		private void menuItemPharmacies_Click(object sender,EventArgs e) {
			FormPharmacies FormP=new FormPharmacies();
			FormP.ShowDialog();
		}

		private void menuItemProviders_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Providers)) {//formerly used Setup permission
				return;
			}
			FormProviderSelect FormPS=new FormProviderSelect();
			FormPS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Providers");		
		}

		private void menuItemPrescriptions_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormRxSetup FormRxSetup2=new FormRxSetup();
			FormRxSetup2.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Rx");		
		}

		private void menuItemReferrals_Click(object sender, System.EventArgs e) {
			FormReferralSelect FormRS=new FormReferralSelect();
			FormRS.ShowDialog();		
		}

		private void menuItemSites_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormSites FormS=new FormSites();
			FormS.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Sites");
		}

		private void menuItemZipCodes_Click(object sender, System.EventArgs e) {
			//if(!Security.IsAuthorized(Permissions.Setup)){
			//	return;
			//}
			FormZipCodes FormZ=new FormZipCodes();
			FormZ.ShowDialog();
			//SecurityLogs.MakeLogEntry(Permissions.Setup,"Zip Codes");
		}

		private void menuItemReports_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)) {
				return;
			}
			FormReportsMore FormR=new FormReportsMore();
			FormR.ShowDialog();
			if(FormR.RpModalSelection==ReportModalSelection.TreatmentFinder) {
				FormRpTreatmentFinder FormT=new FormRpTreatmentFinder();
				FormT.Show();
			}
			if(FormR.RpModalSelection==ReportModalSelection.OutstandingIns) {
				FormRpOutstandingIns FormOI=new FormRpOutstandingIns();
				FormOI.Show();
			}
		}

		//Custom Reports
		private void menuItemRDLReport_Click(object sender,System.EventArgs e) {
			//This point in the code is only reached if the A to Z folders are enabled, thus
			//the image path should exist.
			FormReportCustom FormR=new FormReportCustom();
			FormR.SourceFilePath=
				ODFileUtils.CombinePaths(ImageStore.GetPreferredAtoZpath(),PrefC.GetString(PrefName.ReportFolderName),((MenuItem)sender).Text+".rdl");
			FormR.ShowDialog();
		}

		//Tools
		private void menuItemPrintScreen_Click(object sender, System.EventArgs e) {
			FormPrntScrn FormPS=new FormPrntScrn();
			FormPS.ShowDialog();
		}

		//MiscTools
		private void menuTelephone_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormTelephone FormT=new FormTelephone();
			FormT.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Telephone");
		}

		private void menuItemCreateAtoZFolders_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormAtoZFoldersCreate FormA=new FormAtoZFoldersCreate();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK){
				SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Created AtoZ Folder");
			}
		}

		private void menuItemImportXML_Click(object sender,System.EventArgs e) {
			FormImportXML FormI=new FormImportXML();
			FormI.ShowDialog();
		}

		private void menuItemMergePatients_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormPatientMerge fpm=new FormPatientMerge();
			fpm.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Merge Patients");
		}

		private void menuItemDuplicateBlockouts_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormBlockoutDuplicatesFix form=new FormBlockoutDuplicatesFix();
			form.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Clear duplicate blockouts.");
		}

		private void menuItemTestLatency_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormTestLatency formTL=new FormTestLatency();
			formTL.ShowDialog();
		}

		private void menuItemShutdown_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormShutdown FormS=new FormShutdown();
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			//turn off signal reception for 5 seconds so this workstation will not shut down.
			signalLastRefreshed=MiscData.GetNowDateTime().AddSeconds(5);
			Signalod sig=new Signalod();
			sig.ITypes=((int)InvalidType.ShutDownNow).ToString();
			sig.SigType=SignalType.Invalid;
			Signalods.Insert(sig);
			Computers.ClearAllHeartBeats(Environment.MachineName);//always assume success
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Shutdown all workstations.");
		}

		//End of MiscTools, resume Tools.
		private void menuItemAging_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormAging FormAge=new FormAging();
			FormAge.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Aging Update");
		}

		private void menuItemAuditTrail_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)) {
				return;
			}
			FormAudit FormA=new FormAudit();
			FormA.CurPatNum=CurPatNum;
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,0,"Audit Trail");
		}

		private void menuItemFinanceCharge_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormFinanceCharges FormFC=new FormFinanceCharges();
			FormFC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Run Finance Charges");
		}

		private void menuItemCCRecurring_Click(object sender,EventArgs e) {
			FormCreditRecurringCharges FormRC=new FormCreditRecurringCharges();
			FormRC.Show();
		}

		private void menuItemCustomerManage_Click(object sender,EventArgs e) {
			FormCustomerManagement FormC=new FormCustomerManagement();
			FormC.ShowDialog();
			if(FormC.SelectedPatNum!=0) {
				CurPatNum=FormC.SelectedPatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				RefreshCurrentModule();
				FillPatientButton(pat);
			}
		}

		private void menuItemDatabaseMaintenance_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDatabaseMaintenance FormDM=new FormDatabaseMaintenance();
			FormDM.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Database Maintenance");
		}

		private void menuItemTerminal_Click(object sender,EventArgs e) {
			FormTerminal FormT=new FormTerminal();
			FormT.ShowDialog(); 
			Application.Exit();//always close after coming out of terminal mode as a safety precaution.*/
		}

		private void menuItemTerminalManager_Click(object sender,EventArgs e) {
			if(formTerminalManager==null || formTerminalManager.IsDisposed) {
				formTerminalManager=new FormTerminalManager();
			}
			formTerminalManager.Show();
			formTerminalManager.BringToFront();
		}

		private void menuItemTranslation_Click(object sender,System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormTranslationCat FormTC=new FormTranslationCat();
			FormTC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Translations");
		}

		private void menuItemMobileSetup_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			//MessageBox.Show("Not yet functional.");
			FormMobile FormM=new FormMobile();
			FormM.ShowDialog();
			//SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Mobile Sync");
		}

		private void menuItemRepeatingCharges_Click(object sender, System.EventArgs e) {
			FormRepeatChargesUpdate FormR=new FormRepeatChargesUpdate();
			FormR.ShowDialog();
		}

		private void menuItemScreening_Click(object sender,System.EventArgs e) {
			FormScreenings FormS=new FormScreenings();
			FormS.ShowDialog();
		}

		private void menuItemReqStudents_Click(object sender,EventArgs e) {
			Provider prov=Providers.GetProv(Security.CurUser.ProvNum);
			if(prov!=null && prov.SchoolClassNum!=0){//if a student is logged in
				//the student always has permission to view their own requirements
				FormReqStudentOne FormO=new FormReqStudentOne();
				FormO.ProvNum=prov.ProvNum;
				FormO.ShowDialog();
				return;
			}
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormReqStudentsMany FormM=new FormReqStudentsMany();
			FormM.ShowDialog();	
		}

		private void menuItemWebForms_Click(object sender,EventArgs e) {
			FormWebForms FormWF = new FormWebForms();
			FormWF.Show();
		}

		//Help
		private void menuItemRemote_Click(object sender,System.EventArgs e) {
			try {
				Process.Start("http://www.opendental.com/contact.html");
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Could not find")+" http://www.opendental.com/contact.html" + "\r\n"
					+"Please set up a default web browser.");
			}
			/*
			if(!MsgBox.Show(this,true,"A remote connection will now be attempted. Do NOT continue unless you are already on the phone with us.  Do you want to continue?"))
			{
				return;
			}
			try{
				Process.Start("remoteclient.exe");//Network streaming remote client or any other similar client
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
			}*/
		}

		private void menuItemHelpWindows_Click(object sender, System.EventArgs e) {
			try{
				Process.Start("Help.chm");
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
			}
		}

		private void menuItemHelpContents_Click(object sender, System.EventArgs e) {
			try{
				Process.Start("http://www.opendental.com/manual/toc.html");
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
			}
		}

		private void menuItemHelpIndex_Click(object sender, System.EventArgs e) {
			try{
				Process.Start(@"http://www.opendental.com/manual/alphabetical.html");
			}
			catch{
				MessageBox.Show("Could not find file.");
			}
		}

		private void menuItemRequestFeatures_Click(object sender,EventArgs e) {
			FormFeatureRequest FormF=new FormFeatureRequest();
			FormF.ShowDialog();
		}

		private void menuItemUpdate_Click(object sender, System.EventArgs e) {
			//If A to Z folders are disabled, this menu option is unavailable, since
			//updates are handled more automatically.
			FormUpdate FormU = new FormUpdate();
			FormU.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Update Version");
		}

		/*private void menuItemDaily_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
			//MessageBox.Show(e.Bounds.ToString());
			e.Graphics.DrawString("Dailyyyy",new Font("Microsoft Sans Serif",8),Brushes.Black,e.Bounds.X,e.Bounds.Y);
		}

		private void menuItemDaily_Click(object sender, System.EventArgs e) {
		
		}*/

		#endregion

		//private void OnPatientCardInserted(object sender, PatientCardInsertedEventArgs e) {
		//  if (InvokeRequired) {
		//    Invoke(new PatientCardInsertedEventHandler(OnPatientCardInserted), new object[] { sender, e });
		//    return;
		//  }
		//  if (MessageBox.Show(this, string.Format(Lan.g(this, "A card belonging to {0} has been inserted. Do you wish to search for this patient now?"), e.Patient.GetNameFL()), "Open Dental", MessageBoxButtons.YesNo) != DialogResult.Yes)
		//  {
		//    return;
		//  }
		//  using (FormPatientSelect formPS = new FormPatientSelect()) {
		//    formPS.PreselectPatient(e.Patient);
		//    if(formPS.ShowDialog() == DialogResult.OK) {
		//      // OnPatientSelected(formPS.SelectedPatNum);
		//      // ModuleSelected(formPS.SelectedPatNum);
		//    }
		//  }
		//}

		///<summary>separate thread</summary>
		public void Listen() {
			IPAddress ipAddress = Dns.GetHostAddresses("localhost")[0];
			TcpListenerCommandLine=new TcpListener(ipAddress,2123);
			TcpListenerCommandLine.Start();
			while(true) {
				if(!TcpListenerCommandLine.Pending()) {
					//Thread.Sleep(1000);//for 1 second
					continue;
				}
				TcpClient TcpClientRec = TcpListenerCommandLine.AcceptTcpClient();
				NetworkStream ns = TcpClientRec.GetStream();
				XmlSerializer serializer=new XmlSerializer(typeof(string[]));
				string[] args=(string[])serializer.Deserialize(ns);
				Invoke(new ProcessCommandLineDelegate(ProcessCommandLine),new object[] { args });
				ns.Close();
				TcpClientRec.Close();
			}
		}

		///<summary></summary>
		protected delegate void ProcessCommandLineDelegate(string[] args);

		///<summary></summary>
		public void ProcessCommandLine(string[] args) {
			//if(!Programs.UsingEcwTight() && args.Length==0){
			if(!Programs.UsingEcwTightOrFull() && args.Length==0){
				return;
			}
			/*string descript="";
			for(int i=0;i<args.Length;i++) {
				if(i>0) {
					descript+="\r\n";
				}
				descript+=args[i];
			}
			MessageBox.Show(descript);*/
			/*
			PatNum (the integer primary key)
			ChartNumber (alphanumeric)
			SSN (exactly nine digits. If required, we can gracefully handle dashes, but that is not yet implemented)
			UserName
			Password*/
			int patNum=0;
			string chartNumber="";
			string ssn="";
			string userName="";
			string passHash="";
			string aptNum="";
			string ecwConfigPath="";
			int userId=0;
			string jSessionId = "";
			string jSessionIdSSO = "";
			for(int i=0;i<args.Length;i++) {
				if(args[i].StartsWith("PatNum=") && args[i].Length>7) {
					string patNumStr=args[i].Substring(7).Trim('"');
					try {
						patNum=Convert.ToInt32(patNumStr);
					}
					catch { }
				}
				if(args[i].StartsWith("ChartNumber=") && args[i].Length>12) {
					chartNumber=args[i].Substring(12).Trim('"');
				}
				if(args[i].StartsWith("SSN=") && args[i].Length>4) {
					ssn=args[i].Substring(4).Trim('"');
				}
				if(args[i].StartsWith("UserName=") && args[i].Length>9) {
					userName=args[i].Substring(9).Trim('"');
				}
				if(args[i].StartsWith("PassHash=") && args[i].Length>9) {
					passHash=args[i].Substring(9).Trim('"');
				}
				if(args[i].StartsWith("AptNum=") && args[i].Length>7) {
					aptNum=args[i].Substring(7).Trim('"');
				}
				if(args[i].StartsWith("EcwConfigPath=") && args[i].Length>14) {
					ecwConfigPath=args[i].Substring(14).Trim('"');
				}
				if(args[i].StartsWith("UserId=") && args[i].Length>7) {
					string userIdStr=args[i].Substring(7).Trim('"');
					try {
						userId=Convert.ToInt32(userIdStr);
					}
					catch { }
				}
				if(args[i].StartsWith("JSESSIONID=") && args[i].Length > 11) {
					jSessionId=args[i].Substring(11).Trim('"');
				}
				if(args[i].StartsWith("JSESSIONIDSSO=") && args[i].Length > 14) {
					jSessionIdSSO = args[i].Substring(14).Trim('"');
				}
			}
			//eCW bridge values-------------------------------------------------------------
			Bridges.ECW.AptNum=PIn.Long(aptNum);
			Bridges.ECW.EcwConfigPath=ecwConfigPath;
			Bridges.ECW.UserId=userId;
			Bridges.ECW.JSessionId=jSessionId;
			Bridges.ECW.JSessionIdSSO=jSessionIdSSO;
			//Username and password-----------------------------------------------------
			//users are allowed to use ecw tight integration without command line.  They can manually launch Open Dental.
			//if((Programs.UsingEcwTight() && Security.CurUser==null)//We always want to trigger login window for eCW tight, even if no username was passed in.
			if((Programs.UsingEcwTightOrFull() && Security.CurUser==null)//We always want to trigger login window for eCW tight, even if no username was passed in.
				|| (userName!=""//if a username was passed in, but not in tight eCW mode
				&& (Security.CurUser==null || Security.CurUser.UserName != userName))//and it's different from the current user
			) {
				//The purpose of this loop is to use the username and password that were passed in to determine which user to log in
				//log out------------------------------------
				LastModule=myOutlookBar.SelectedIndex;
				myOutlookBar.SelectedIndex=-1;
				myOutlookBar.Invalidate();
				UnselectActive();
				allNeutral();
				Userod user=Userods.GetUserByName(userName,true);
				if(user==null) {
					//if(Programs.UsingEcwTight() && userName!="") {
					if(Programs.UsingEcwTightOrFull() && userName!="") {
						user=new Userod();
						user.UserName=userName;
						user.UserGroupNum=PIn.Long(ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"DefaultUserGroup"));
						if(passHash=="") {
							user.Password="";
						}
						else {
							user.Password=passHash;
						}
						Userods.Insert(user);//This can fail if duplicate username because of capitalization differences.
						DataValid.SetInvalid(InvalidType.Security);
					}
					else {//not using eCW in tight integration mode
						//So present logon screen
						FormLogOn_=new FormLogOn();
						FormLogOn_.ShowDialog(this);
						if(FormLogOn_.DialogResult==DialogResult.Cancel) {
							Application.Exit();
							return;
						}
						user=Security.CurUser.Copy();
					}
				}
				//Can't use Userods.CheckPassword, because we only have the hashed password.
				//if(passHash!=user.Password || !Programs.UsingEcwTight())//password not accepted or not using eCW
				if(passHash!=user.Password || !Programs.UsingEcwTightOrFull())//password not accepted or not using eCW
				{
					//So present logon screen
					FormLogOn_=new FormLogOn();
					FormLogOn_.ShowDialog(this);
					if(FormLogOn_.DialogResult==DialogResult.Cancel) {
						Application.Exit();
						return;
					}
				}
				else {//password accepted and using eCW tight.
					//this part usually happens in the logon window
					Security.CurUser = user.Copy();
					//let's skip tasks for now
					//if(PrefC.GetBool(PrefName.TasksCheckOnStartup")){
					//	int taskcount=Tasks.UserTasksCount(Security.CurUser.UserNum);
					//	if(taskcount>0){
					//		MessageBox.Show(Lan.g(this,"There are ")+taskcount+Lan.g(this," unfinished tasks on your tasklists."));
					//	}
					//}
				}
				myOutlookBar.SelectedIndex=Security.GetModule(LastModule);
				myOutlookBar.Invalidate();
				SetModuleSelected();
				if(CurPatNum==0) {
					Text=PatientL.GetMainTitle(null);
				}
				else {
					Patient pat=Patients.GetPat(CurPatNum);
					Text=PatientL.GetMainTitle(pat);
				}
				if(userControlTasks1.Visible) {
					userControlTasks1.InitializeOnStartup();
				}
				if(myOutlookBar.SelectedIndex==-1) {
					MsgBox.Show(this,"You do not have permission to use any modules.");
				}
			}
			//patient id----------------------------------------------------------------
			if(patNum!=0) {
				Patient pat=Patients.GetPat(patNum);
				if(pat==null) {
					CurPatNum=0;
					RefreshCurrentModule();
					FillPatientButton(null);
				}
				else {
					CurPatNum=patNum;
					RefreshCurrentModule();
					FillPatientButton(pat);
				}
			}
			else if(chartNumber!="") {
				Patient pat=Patients.GetPatByChartNumber(chartNumber);
				if(pat==null) {
					//todo: decide action
					CurPatNum=0;
					RefreshCurrentModule();
					FillPatientButton(null);
				}
				else {
					CurPatNum=pat.PatNum;
					RefreshCurrentModule();
					FillPatientButton(pat);
				}
			}
			else if(ssn!="") {
				Patient pat=Patients.GetPatBySSN(ssn);
				if(pat==null) {
					//todo: decide action
					CurPatNum=0;
					RefreshCurrentModule();
					FillPatientButton(null);
				}
				else {
					CurPatNum=pat.PatNum;
					RefreshCurrentModule();
					FillPatientButton(pat);
				}
			}
		}

		private void timerPhoneWebCam_Tick(object sender,EventArgs e) {
			//every 1.6s.  Performance was not very good when it was synchronous.
			//This won't even happen unless PrefName.DockPhonePanelShow==true
			Thread workerThread=new Thread(new ThreadStart(PhoneWebCamTickWorkerThread));
			workerThread.Start();
		}

		private void PhoneWebCamTickWorkerThread() {
			List<Phone> phoneList=Phones.GetPhoneList();
			string ipaddressStr="";
			IPHostEntry iphostentry=Dns.GetHostEntry(Environment.MachineName);
			foreach(IPAddress ipaddress in iphostentry.AddressList) {
				if(ipaddress.ToString().Contains("192.168.0.2")) {
					ipaddressStr=ipaddress.ToString();
				}
			}
			int extension=Phones.GetPhoneExtension(ipaddressStr,Environment.MachineName);
			//if(extension==0){
			//	return;
			//}
			Phone phone=Phones.GetPhoneForExtension(phoneList,extension);
			/*
			if(extension>0) {
				phone=Phones.GetPhoneForExtension(phoneList,extension);
				if(Security.CurUser!=null && phone!=null && phone.EmployeeNum!=Security.CurUser.EmployeeNum) {
					phone.EmployeeNum=Security.CurUser.EmployeeNum;
					phone.EmployeeName=Security.CurUser.UserName;
					Phones.SetPhoneForEmp(Security.CurUser.EmployeeNum,Security.CurUser.UserName,phone.Extension);
					//phoneList=Phones.GetPhoneList();
					//phone=Phones.GetPhoneForExtension(phoneList,extension);
				}
			}*/
			//send the results back to the UI layer for action.
			try {
				Invoke(new PhoneWebCamTickDisplayDelegate(PhoneWebCamTickDisplay),new object[] { phoneList,phone });
			}
			catch { }//prevents crash on closing if FormOpenDental has already been disposed.
		}

		///<summary></summary>
		protected delegate void PhoneWebCamTickDisplayDelegate(List<Phone> phoneList,Phone phone);

		///<summary>phoneList is the list of all phone rows just pulled from the database.  phone is the one that we should display here, and it can be null.</summary>
		public void PhoneWebCamTickDisplay(List<Phone> phoneList,Phone phone) {
			//send the phoneList to the 2 places where it's needed:
			phoneSmall.PhoneList=phoneList;
			if(formPhoneTiles!=null && !formPhoneTiles.IsDisposed) {
				formPhoneTiles.PhoneList=phoneList;
			}
			if(phone==null) {
				phoneSmall.Extension=0;
			}
			else {
				phoneSmall.Extension=phone.Extension;
			}
			phoneSmall.PhoneCur=phone;
		}

		/// <summary>This is set to 30 seconds</summary>
		private void timerWebHostSynch_Tick(object sender,EventArgs e) {
			string interval=PrefC.GetStringSilent(PrefName.MobileSyncIntervalMinutes);
			if(interval=="" || interval=="0") {//not a paid customer or chooses not to synch
				return;
			}
			if(System.Environment.MachineName.ToUpper()!=PrefC.GetStringSilent(PrefName.MobileSyncWorkstationName).ToUpper()) {
				//Since GetStringSilent returns "" before OD is connected to db, this gracefully loops out
				return;
			}
			if(PrefC.GetDate(PrefName.MobileExcludeApptsBeforeDate).Year<1880) {
				//full synch never run
				return;
			}
			FormMobile.SynchFromMain(false);
		}

		private void timerReplicationMonitor_Tick(object sender,EventArgs e) {
			//this timer doesn't get turned on until after user successfully logs in.
			if(ReplicationServers.Listt.Count==0) {//Listt will be automatically refreshed if null.
				return;//user must not be using any replication
			}
			bool isSlaveMonitor=false;
			for(int i=0;i<ReplicationServers.Listt.Count;i++) {
				if(ReplicationServers.Listt[i].SlaveMonitor.ToString()!=Dns.GetHostName()) {
					isSlaveMonitor=true;
				}
			}
			if(!isSlaveMonitor) {
				return;
			}
			DataTable table=ReplicationServers.GetSlaveStatus();
			if(table.Rows.Count==0) {
				return;
			}
			if(table.Rows[0]["Replicate_Do_Db"].ToString()!=DataConnection.GetDatabaseName()) {//if the database we're connected to is not even involved in replication
				return;
			}
			string status=table.Rows[0]["Slave_SQL_Running"].ToString();
			if(status=="Yes") {
				return;
			}
			//Shut down all copies of OD and set ReplicationFailureAtServer_id to this server_id
			//No workstations will be able to connect to this single server while this flag is set.
			Prefs.UpdateInt(PrefName.ReplicationFailureAtServer_id,ReplicationServers.Server_id);
			//shut down all workstations on all servers
			FormOpenDental.signalLastRefreshed=MiscData.GetNowDateTime().AddSeconds(5);
			Signalod sig=new Signalod();
			sig.ITypes=((int)InvalidType.ShutDownNow).ToString();
			sig.SigType=SignalType.Invalid;
			Signalods.Insert(sig);
			Computers.ClearAllHeartBeats(Environment.MachineName);//always assume success
			timerReplicationMonitor.Enabled=false;
			MsgBox.Show(this,"This database is temporarily unavailable.  Please connect instead to your alternate database at the other location.");
			Application.Exit();
		}

		#region Logoff
		///<summary>This is set to 15 seconds.  This interval must be longer than the interval of the timer in FormLogoffWarning (10s), or it will go into a loop.</summary>
		private void timerLogoff_Tick(object sender,EventArgs e) {
			if(PrefC.GetInt(PrefName.SecurityLogOffAfterMinutes)==0) {
				return;
			}
			//Warning.  When debugging this, the ActiveForm will be impossible to determine by setting breakpoints.
			//string activeFormText=Form.ActiveForm.Text;
			//If a breakpoint is set below here, ActiveForm will erroneously show as null.
			if(Form.ActiveForm==null) {//some other program has focus
				FormRecentlyOpenForLogoff=null;
				//Do not alter IsFormLogOnLastActive because it could still be active in background.
			}
			else if(Form.ActiveForm==this) {//main form active
				FormRecentlyOpenForLogoff=null;
				//User must have logged back in so IsFormLogOnLastActive should be false.
				IsFormLogOnLastActive=false;
			}
			else {//Some Open Dental dialog is active.
				if(Form.ActiveForm==FormRecentlyOpenForLogoff) {
					//The same form is active as last time, so don't add events again.
					//The active form will now be constantly resetting the dateTimeLastActivity.
				}
				else {//this is the first time this form has been encountered, so attach events and don't do anything else
					AttachMouseMoveToForm(Form.ActiveForm);
					FormRecentlyOpenForLogoff=Form.ActiveForm;
					dateTimeLastActivity=DateTime.Now;
					//Flag FormLogOn as the active form so that OD doesn't continue trying to log the user off when using the web service.
					if(Form.ActiveForm.GetType()==typeof(FormLogOn)) {
						IsFormLogOnLastActive=true;
					}
					else {
						IsFormLogOnLastActive=false;
					}
					return;
				}
			}
			DateTime dtDeadline=dateTimeLastActivity+TimeSpan.FromMinutes((double)PrefC.GetInt(PrefName.SecurityLogOffAfterMinutes));
			//Debug.WriteLine("Now:"+DateTime.Now.ToLongTimeString()+", Deadline:"+dtDeadline.ToLongTimeString());
			if(DateTime.Now<dtDeadline) {
				return;
			}
			if(Security.CurUser==null) {//nobody logged on
				return;
			}
			//The above check works unless using web service.  With web service, CurUser is not set to null when FormLogOn is shown.
			if(IsFormLogOnLastActive) {//Don't try to log off a user that is already logged off.
				return;
			}
			FormLogoffWarning formW=new FormLogoffWarning();
			formW.ShowDialog();
			//User could be working outside of OD and the Log On window will never become "active" so we set it here for a fail safe.
			IsFormLogOnLastActive=true;
			if(formW.DialogResult!=DialogResult.OK) {
				dateTimeLastActivity=DateTime.Now;
				return;//user hit cancel, so don't log off
			}
			try {
				LogOffNow();
			}
			catch { }
		}

		private void FormOpenDental_MouseMove(object sender,MouseEventArgs e) {
			//This has a few extra lines because my mouse during testing was firing a MouseMove every second,
			//even if no movement.  So this tests to see if there was actually movement.
			if(locationMouseForLogoff==e.Location) {
				//mouse hasn't moved
				return;
			}
			locationMouseForLogoff=e.Location;
			dateTimeLastActivity=DateTime.Now;
		}

		private void LogOffNow() {
			LastModule=myOutlookBar.SelectedIndex;
			myOutlookBar.SelectedIndex=-1;
			myOutlookBar.Invalidate();
			UnselectActive();
			allNeutral();
			if(userControlTasks1.Visible) {
				userControlTasks1.ClearLogOff();
			}
			Userod oldUser=Security.CurUser;
			if(CurPatNum==0) {
				Security.CurUser=null;
				Text=PatientL.GetMainTitle(null);
			}
			else {
				Patient pat=Patients.GetPat(CurPatNum);
				Security.CurUser=null;
				Text=PatientL.GetMainTitle(pat);
			}
			////Iterating through OpenForms with foreach did not work, probably because we were altering the collection when closing forms.
			////So we make a copy of the collection before iterating through to close each form.
			//List<Form> listForms=new List<Form>();
			//for(int f=0;f<Application.OpenForms.Count;f++) {
			//  listForms.Add(Application.OpenForms[f]);
			//}
			//for(int f=0;f<listForms.Count;f++) {
			//  if(listForms[f]!=this) {//don't close the main form
			//    listForms[f].Close();
			//    listForms[f].Dispose();
			//  }
			//}
			//Application.DoEvents();//so that the window background will refresh.
			for(int f=Application.OpenForms.Count-1;f>=0;f--) {
				if(Application.OpenForms[f]==this) {// main form
					continue;
				}
				Application.OpenForms[f].Hide();
				Application.OpenForms[f].Close();
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Security.CurUser=oldUser;//so that the queries in FormLogOn() will work for the web service, since the web service requires a valid user to run queries.
			}
			FormLogOn_=new FormLogOn();
			FormLogOn_.ShowDialog(this);
			if(FormLogOn_.DialogResult==DialogResult.Cancel) {
				Application.Exit();
				return;
			}
			myOutlookBar.SelectedIndex=Security.GetModule(LastModule);
			myOutlookBar.Invalidate();
			SetModuleSelected();
			if(CurPatNum==0) {
				Text=PatientL.GetMainTitle(null);
			}
			else {
				Patient pat=Patients.GetPat(CurPatNum);
				Text=PatientL.GetMainTitle(pat);
			}
			if(userControlTasks1.Visible) {
				userControlTasks1.InitializeOnStartup();
			}
			//User logged back in so log on form is no longer the active window.
			IsFormLogOnLastActive=false;
			dateTimeLastActivity=DateTime.Now;
			if(myOutlookBar.SelectedIndex==-1) {
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
		}

		//The purpose of the 11 methods below is to have every child control and child form report MouseMove events to this parent form.
		//It must be dynamic and recursive.
		protected override void OnControlAdded(ControlEventArgs e) {
			AttachMouseMoveToControl(e.Control);
			base.OnControlAdded(e);
		}

		protected override void OnControlRemoved(ControlEventArgs e) {
			DetachMouseMoveFromControl(e.Control);
			base.OnControlRemoved(e);
		}

		private void AttachMouseMoveToForm(Form form) {
			form.MouseMove += new MouseEventHandler(ChildForm_MouseMove);
			AttachMouseMoveToChildren(form);
		}

		private void AttachMouseMoveToControl(Control control) {
			control.MouseMove += new MouseEventHandler(Child_MouseMove);
			control.ControlAdded += new ControlEventHandler(Child_ControlAdded);
			control.ControlRemoved += new ControlEventHandler(Child_ControlRemoved);
			AttachMouseMoveToChildren(control);
		}

		private void AttachMouseMoveToChildren(Control parent) {
			foreach(Control child in parent.Controls) {
				AttachMouseMoveToControl(child);
			}
		}

		private void DetachMouseMoveFromControl(Control control) {
			DetachMouseMoveFromChildren(control);
			control.MouseMove -= new MouseEventHandler(Child_MouseMove);
			control.ControlAdded -= new ControlEventHandler(Child_ControlAdded);
			control.ControlRemoved -= new ControlEventHandler(Child_ControlRemoved);
		}

		private void DetachMouseMoveFromChildren(Control parent) {
			foreach(Control child in parent.Controls) {
				DetachMouseMoveFromControl(child);
			}
		}

		private void Child_ControlAdded(object sender,ControlEventArgs e) {
			AttachMouseMoveToControl(e.Control);
		}

		private void Child_ControlRemoved(object sender,ControlEventArgs e) {
			DetachMouseMoveFromControl(e.Control);
		}

		private void Child_MouseMove(object sender,MouseEventArgs e) {
			Point loc=e.Location;
			Control child=(Control)sender;
			//Debug.WriteLine("Child_MouseMove:"+DateTime.Now.ToLongTimeString()+", sender:"+child.Name);
			do {
				loc.Offset(child.Left,child.Top);
				child = child.Parent;
			}
			while(child.Parent != null);//When it hits a form, either this one or any other active form.
			MouseEventArgs newArgs = new MouseEventArgs(e.Button,e.Clicks,loc.X,loc.Y,e.Delta);
			OnMouseMove(newArgs);
		}

		private void ChildForm_MouseMove(object sender,MouseEventArgs e) {
			Point loc=e.Location;
			Form childForm=(Form)sender;
			loc.Offset(childForm.Left,childForm.Top);
			MouseEventArgs newArgs = new MouseEventArgs(e.Button,e.Clicks,loc.X,loc.Y,e.Delta);
			OnMouseMove(newArgs);
		}
		#endregion Logoff

		private void SystemEvents_SessionSwitch(object sender,SessionSwitchEventArgs e) {
			if(e.Reason!=SessionSwitchReason.SessionLock) {
				return;
			}
			if(!PrefC.GetBool(PrefName.SecurityLogOffWithWindows)) {
				return;
			}
			if(Security.CurUser==null) {//not sure if this is a good test.
				return;
			}
			//simply copied and pasted code from logoff menu click for testing.
			LastModule=myOutlookBar.SelectedIndex;
			myOutlookBar.SelectedIndex=-1;
			myOutlookBar.Invalidate();
			UnselectActive();
			allNeutral();
			if(FormLogOn_!=null) {//To prevent multiple log on screens from showing.
				FormLogOn_.Dispose();
			}
			FormLogOn_=new FormLogOn();
			FormLogOn_.ShowDialog(this);//Passing "this" brings FormL to the front when user logs back in.
			if(FormLogOn_.DialogResult==DialogResult.Cancel) {
				Application.Exit();
				return;
			}
			myOutlookBar.SelectedIndex=Security.GetModule(LastModule);
			myOutlookBar.Invalidate();
			SetModuleSelected();
			if(CurPatNum==0) {
				Text=PatientL.GetMainTitle(null);
			}
			else {
				Patient pat=Patients.GetPat(CurPatNum);
				Text=PatientL.GetMainTitle(pat);
			}
			if(userControlTasks1.Visible) {
				userControlTasks1.InitializeOnStartup();
			}
			if(myOutlookBar.SelectedIndex==-1) {
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
		}

		private void FormOpenDental_FormClosing(object sender,FormClosingEventArgs e) {
			try {
				Computers.ClearHeartBeat(Environment.MachineName);
			}
			catch { }
			FormUAppoint.AbortThread();
			//ICat.AbortThread
			//earlier, this wasn't working.  But I haven't tested it since moving it from Closing to FormClosing.
			if(ThreadCommandLine!=null) {
				ThreadCommandLine.Abort();
			}
			if(ThreadVM!=null) {
				ThreadVM.Abort();
				ThreadVM.Join();
				ThreadVM=null;
			}
		}

		private void FormOpenDental_FormClosed(object sender,FormClosedEventArgs e) {
			//Cleanup all resources related to the program which have their Dispose methods properly defined.
			//This helps ensure that the chart module and its tooth chart wrapper are properly disposed of in particular.
			//This step is necessary so that graphics memory does not fill up.
			Dispose();
		}

		

		

		


	



		


		

		

		

		

		

	

	

		

		

		


















	}

	public class PopupEvent:IComparable{
		public long PopupNum;
		///<summary>Disable this popup until this time.</summary>
		public DateTime DisableUntil;

		public int CompareTo(object obj) {
			PopupEvent pop=(PopupEvent)obj;
			return DisableUntil.CompareTo(pop.DisableUntil);
		}

		public override string ToString() {
			return PopupNum.ToString()+", "+DisableUntil.ToString();
		}
	}
}
