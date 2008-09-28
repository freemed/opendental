/*=============================================================================================================
Open Dental is a dental practice management program.
Copyright (C) 2003,2004,2005,2006,2007  Jordan Sparks, DMD.  http://www.open-dent.com,  http://www.docsparks.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU General Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU General Public License
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
using System.Net;
using System.Net.Sockets;
using System.IO;
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
using SparksToothChart;
using OpenDental.SmartCards;
using OpenDental.UI;

#if(ORA_DB)
using OD_CRYPTO;
#endif

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
		private OpenDental.OutlookBar myOutlookBar;
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
		///<summary>The only reason this is public static is so that it can be seen from the terminal manager.  Otherwise, it's passed around properly.</summary>
		public static int CurPatNum;
		private System.Windows.Forms.MenuItem menuItemClearinghouses;
		private System.Windows.Forms.MenuItem menuItemUpdate;
		private System.Windows.Forms.MenuItem menuItemHelpWindows;
		private System.Windows.Forms.MenuItem menuItemMisc;
		private System.Windows.Forms.MenuItem menuItemRemote;
		private System.Windows.Forms.MenuItem menuItemSchoolClass;
		private System.Windows.Forms.MenuItem menuItemSchoolCourses;
		private System.Windows.Forms.MenuItem menuItemPatientImport;
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
		private MenuItem menuItemPayPeriods;
		private MenuItem menuItemApptRules;
		private MenuItem menuItemAuditTrail;
		private MenuItem menuItemPatFieldDefs;
		private MenuItem menuItemDiseases;
		private MenuItem menuItemTerminal;
		private MenuItem menuItemTerminalManager;
		private MenuItem menuItemQuestions;
		private MenuItem menuItemCustomReports;
		private MenuItem menuItemMessaging;
		private OpenDental.UI.LightSignalGrid lightSignalGrid1;
		private MenuItem menuItemMessagingButs;
		///<summary>This is not the actual date/time last refreshed.  It is really the date/time of the last item in the database retrieved on previous refreshes.  That way, the local workstation time is irrelevant.</summary>
		private DateTime signalLastRefreshed;
		private FormSplash Splash;
		private Bitmap bitmapIcon;
		private ContrAppt ContrAppt2;
		private ContrFamily ContrFamily2;
		private ContrAccount ContrAccount2;
		private ContrTreat ContrTreat2;
		private ContrDocs ContrDocs2;
		private ContrChart ContrChart2;
		private ContrStaff ContrManage2;
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
		private MenuItem menuItemReallocate;
		private UserControlTasks userControlTasks1;
		private MenuItem menuItemMergeDatabases;
		private MenuItem menuItemDisplayFields;
		private Panel panelSplitter;
		//private string dconnStr;
		private bool MouseIsDownOnSplitter;
		private Point SplitterOriginalLocation;
		private ContextMenu menuSplitter;
		private MenuItem menuItemDockBottom;
		private MenuItem menuItemDockRight;
		private OpenDental.SmartCards.SmartCardWatcher smartCardWatcher1;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private ImageList imageListMain;
		private ContextMenu menuPatient;
		private ContextMenu menuLabel;
		private ContextMenu menuEmail;
		private ContextMenu menuLetter;
		private Point OriginalMousePos;
		private MenuItem menuItemCustomerManage;
		private System.Windows.Forms.Timer timerDisabledKey;
		private MenuItem menuItem_ProviderAllocatorSetup;
		private MenuItem menuItemAnestheticMeds;
		///<summary>This list will only contain events for this computer where the users clicked to disable a popup for a specified period of time.  So it won't typically have many items in it.</summary>
		private List<PopupEvent> PopupEventList;
		private MenuItem menuItemPharmacies;
		private MenuItem menuItemSheets;
		private MenuItem menuItemRequestFeatures;
		private MenuItem menuItemModules;
		private MenuItem menuItemRecallTypes;
		private MenuItem menuItemFeeScheds;
		private UserControlPhonePanel phonePanel;

		///<summary></summary>
		public FormOpenDental(){
			Logger.openlog.Log("Initializing Open Dental...",Logger.Severity.INFO);
			Splash=new FormSplash();
			Splash.Show();
			InitializeComponent();
			ContrAppt2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrFamily2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrAccount2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrTreat2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrChart2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrDocs2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrManage2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			GotoModule.ModuleSelected+=new ModuleEventHandler(GotoModule_ModuleSelected);
			panelSplitter.ContextMenu=menuSplitter;
			menuItemDockBottom.Checked=true;
			phonePanel=new UserControlPhonePanel();
			phonePanel.Visible=false;
			this.Controls.Add(phonePanel);
			Logger.openlog.Log("Open Dental initialization complete.",Logger.Severity.INFO);
			menuItem_ProviderAllocatorSetup.Visible=false;
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
			this.menuItemPrinter = new System.Windows.Forms.MenuItem();
			this.menuItemGraphics = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemConfig = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.menuItemAnestheticMeds = new System.Windows.Forms.MenuItem();
			this.menuItemApptRules = new System.Windows.Forms.MenuItem();
			this.menuItemApptViews = new System.Windows.Forms.MenuItem();
			this.menuItemAutoCodes = new System.Windows.Forms.MenuItem();
			this.menuItemAutoNotes = new System.Windows.Forms.MenuItem();
			this.menuItemClaimForms = new System.Windows.Forms.MenuItem();
			this.menuItemClearinghouses = new System.Windows.Forms.MenuItem();
			this.menuItemComputers = new System.Windows.Forms.MenuItem();
			this.menuItemDataPath = new System.Windows.Forms.MenuItem();
			this.menuItemDefinitions = new System.Windows.Forms.MenuItem();
			this.menuItemDiseases = new System.Windows.Forms.MenuItem();
			this.menuItemDisplayFields = new System.Windows.Forms.MenuItem();
			this.menuItemEasy = new System.Windows.Forms.MenuItem();
			this.menuItemEmail = new System.Windows.Forms.MenuItem();
			this.menuItemFeeScheds = new System.Windows.Forms.MenuItem();
			this.menuItemImaging = new System.Windows.Forms.MenuItem();
			this.menuItemInsCats = new System.Windows.Forms.MenuItem();
			this.menuItemLaboratories = new System.Windows.Forms.MenuItem();
			this.menuItemMessaging = new System.Windows.Forms.MenuItem();
			this.menuItemMessagingButs = new System.Windows.Forms.MenuItem();
			this.menuItemMisc = new System.Windows.Forms.MenuItem();
			this.menuItemModules = new System.Windows.Forms.MenuItem();
			this.menuItemOperatories = new System.Windows.Forms.MenuItem();
			this.menuItemPatFieldDefs = new System.Windows.Forms.MenuItem();
			this.menuItemPayPeriods = new System.Windows.Forms.MenuItem();
			this.menuItemPractice = new System.Windows.Forms.MenuItem();
			this.menuItemProcedureButtons = new System.Windows.Forms.MenuItem();
			this.menuItemLinks = new System.Windows.Forms.MenuItem();
			this.menuItem_ProviderAllocatorSetup = new System.Windows.Forms.MenuItem();
			this.menuItemQuestions = new System.Windows.Forms.MenuItem();
			this.menuItemRecall = new System.Windows.Forms.MenuItem();
			this.menuItemRecallTypes = new System.Windows.Forms.MenuItem();
			this.menuItemRequirementsNeeded = new System.Windows.Forms.MenuItem();
			this.menuItemSched = new System.Windows.Forms.MenuItem();
			this.menuItemSecurity = new System.Windows.Forms.MenuItem();
			this.menuItemSheets = new System.Windows.Forms.MenuItem();
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
			this.menuItemPatientImport = new System.Windows.Forms.MenuItem();
			this.menuItemCreateAtoZFolders = new System.Windows.Forms.MenuItem();
			this.menuItemReallocate = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemAuditTrail = new System.Windows.Forms.MenuItem();
			this.menuItemDatabaseMaintenance = new System.Windows.Forms.MenuItem();
			this.menuItemImportXML = new System.Windows.Forms.MenuItem();
			this.menuItemAging = new System.Windows.Forms.MenuItem();
			this.menuItemFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemRepeatingCharges = new System.Windows.Forms.MenuItem();
			this.menuItemTranslation = new System.Windows.Forms.MenuItem();
			this.menuItemScreening = new System.Windows.Forms.MenuItem();
			this.menuItemTerminal = new System.Windows.Forms.MenuItem();
			this.menuItemTerminalManager = new System.Windows.Forms.MenuItem();
			this.menuItemReqStudents = new System.Windows.Forms.MenuItem();
			this.menuItemMergeDatabases = new System.Windows.Forms.MenuItem();
			this.menuItemCustomerManage = new System.Windows.Forms.MenuItem();
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
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.userControlTasks1 = new OpenDental.UserControlTasks();
			this.ContrManage2 = new OpenDental.ContrStaff();
			this.ContrChart2 = new OpenDental.ContrChart();
			this.ContrDocs2 = new OpenDental.ContrDocs();
			this.ContrTreat2 = new OpenDental.ContrTreat();
			this.ContrAccount2 = new OpenDental.ContrAccount();
			this.ContrFamily2 = new OpenDental.ContrFamily();
			this.ContrAppt2 = new OpenDental.ContrAppt();
			this.lightSignalGrid1 = new OpenDental.UI.LightSignalGrid();
			this.myOutlookBar = new OpenDental.OutlookBar();
			this.smartCardWatcher1 = new OpenDental.SmartCards.SmartCardWatcher();
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
            this.menuItemPrinter,
            this.menuItemGraphics,
            this.menuItem6,
            this.menuItemConfig,
            this.menuItem7,
            this.menuItemExit});
			this.menuItemFile.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItemFile.Text = "&File";
			// 
			// menuItemPrinter
			// 
			this.menuItemPrinter.Index = 0;
			this.menuItemPrinter.Text = "&Printers";
			this.menuItemPrinter.Click += new System.EventHandler(this.menuItemPrinter_Click);
			// 
			// menuItemGraphics
			// 
			this.menuItemGraphics.Index = 1;
			this.menuItemGraphics.Text = "Graphics";
			this.menuItemGraphics.Click += new System.EventHandler(this.menuItemGraphics_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "-";
			// 
			// menuItemConfig
			// 
			this.menuItemConfig.Index = 3;
			this.menuItemConfig.Text = "&Choose Database";
			this.menuItemConfig.Click += new System.EventHandler(this.menuItemConfig_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 5;
			this.menuItemExit.ShowShortcut = false;
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 2;
			this.menuItemSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAnestheticMeds,
            this.menuItemApptRules,
            this.menuItemApptViews,
            this.menuItemAutoCodes,
            this.menuItemAutoNotes,
            this.menuItemClaimForms,
            this.menuItemClearinghouses,
            this.menuItemComputers,
            this.menuItemDataPath,
            this.menuItemDefinitions,
            this.menuItemDiseases,
            this.menuItemDisplayFields,
            this.menuItemEasy,
            this.menuItemEmail,
            this.menuItemFeeScheds,
            this.menuItemImaging,
            this.menuItemInsCats,
            this.menuItemLaboratories,
            this.menuItemMessaging,
            this.menuItemMessagingButs,
            this.menuItemMisc,
            this.menuItemModules,
            this.menuItemOperatories,
            this.menuItemPatFieldDefs,
            this.menuItemPayPeriods,
            this.menuItemPractice,
            this.menuItemProcedureButtons,
            this.menuItemLinks,
            this.menuItem_ProviderAllocatorSetup,
            this.menuItemQuestions,
            this.menuItemRecall,
            this.menuItemRecallTypes,
            this.menuItemRequirementsNeeded,
            this.menuItemSched,
            this.menuItemSecurity,
            this.menuItemSheets});
			this.menuItemSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItemSettings.Text = "&Setup";
			// 
			// menuItemAnestheticMeds
			// 
			this.menuItemAnestheticMeds.Index = 0;
			this.menuItemAnestheticMeds.Text = "Anesthetic Medications";
			this.menuItemAnestheticMeds.Click += new System.EventHandler(this.menuItemAnestheticMedications_Click);
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
			// menuItemAutoNotes
			// 
			this.menuItemAutoNotes.Index = 4;
			this.menuItemAutoNotes.Text = "Auto Notes";
			this.menuItemAutoNotes.Click += new System.EventHandler(this.menuItemAutoNotes_Click);
			// 
			// menuItemClaimForms
			// 
			this.menuItemClaimForms.Index = 5;
			this.menuItemClaimForms.Text = "Claim Forms";
			this.menuItemClaimForms.Click += new System.EventHandler(this.menuItemClaimForms_Click);
			// 
			// menuItemClearinghouses
			// 
			this.menuItemClearinghouses.Index = 6;
			this.menuItemClearinghouses.Text = "Clearinghouses";
			this.menuItemClearinghouses.Click += new System.EventHandler(this.menuItemClearinghouses_Click);
			// 
			// menuItemComputers
			// 
			this.menuItemComputers.Index = 7;
			this.menuItemComputers.Text = "Computers";
			this.menuItemComputers.Click += new System.EventHandler(this.menuItemComputers_Click);
			// 
			// menuItemDataPath
			// 
			this.menuItemDataPath.Index = 8;
			this.menuItemDataPath.Text = "Data Paths";
			this.menuItemDataPath.Click += new System.EventHandler(this.menuItemDataPath_Click);
			// 
			// menuItemDefinitions
			// 
			this.menuItemDefinitions.Index = 9;
			this.menuItemDefinitions.Text = "Definitions";
			this.menuItemDefinitions.Click += new System.EventHandler(this.menuItemDefinitions_Click);
			// 
			// menuItemDiseases
			// 
			this.menuItemDiseases.Index = 10;
			this.menuItemDiseases.Text = "Diseases";
			this.menuItemDiseases.Click += new System.EventHandler(this.menuItemDiseases_Click);
			// 
			// menuItemDisplayFields
			// 
			this.menuItemDisplayFields.Index = 11;
			this.menuItemDisplayFields.Text = "Display Fields";
			this.menuItemDisplayFields.Click += new System.EventHandler(this.menuItemDisplayFields_Click);
			// 
			// menuItemEasy
			// 
			this.menuItemEasy.Index = 12;
			this.menuItemEasy.Text = "Easy Options";
			this.menuItemEasy.Click += new System.EventHandler(this.menuItemEasy_Click);
			// 
			// menuItemEmail
			// 
			this.menuItemEmail.Index = 13;
			this.menuItemEmail.Text = "E-mail";
			this.menuItemEmail.Click += new System.EventHandler(this.menuItemEmail_Click);
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
			// menuItemLaboratories
			// 
			this.menuItemLaboratories.Index = 17;
			this.menuItemLaboratories.Text = "Laboratories";
			this.menuItemLaboratories.Click += new System.EventHandler(this.menuItemLaboratories_Click);
			// 
			// menuItemMessaging
			// 
			this.menuItemMessaging.Index = 18;
			this.menuItemMessaging.Text = "Messaging";
			this.menuItemMessaging.Click += new System.EventHandler(this.menuItemMessaging_Click);
			// 
			// menuItemMessagingButs
			// 
			this.menuItemMessagingButs.Index = 19;
			this.menuItemMessagingButs.Text = "Messaging Buttons";
			this.menuItemMessagingButs.Click += new System.EventHandler(this.menuItemMessagingButs_Click);
			// 
			// menuItemMisc
			// 
			this.menuItemMisc.Index = 20;
			this.menuItemMisc.Text = "Miscellaneous";
			this.menuItemMisc.Click += new System.EventHandler(this.menuItemMisc_Click);
			// 
			// menuItemModules
			// 
			this.menuItemModules.Index = 21;
			this.menuItemModules.Text = "Modules";
			this.menuItemModules.Click += new System.EventHandler(this.menuItemModules_Click);
			// 
			// menuItemOperatories
			// 
			this.menuItemOperatories.Index = 22;
			this.menuItemOperatories.Text = "Operatories";
			this.menuItemOperatories.Click += new System.EventHandler(this.menuItemOperatories_Click);
			// 
			// menuItemPatFieldDefs
			// 
			this.menuItemPatFieldDefs.Index = 23;
			this.menuItemPatFieldDefs.Text = "Patient Field Defs";
			this.menuItemPatFieldDefs.Click += new System.EventHandler(this.menuItemPatFieldDefs_Click);
			// 
			// menuItemPayPeriods
			// 
			this.menuItemPayPeriods.Index = 24;
			this.menuItemPayPeriods.Text = "Pay Periods";
			this.menuItemPayPeriods.Click += new System.EventHandler(this.menuItemPayPeriods_Click);
			// 
			// menuItemPractice
			// 
			this.menuItemPractice.Index = 25;
			this.menuItemPractice.Text = "Practice";
			this.menuItemPractice.Click += new System.EventHandler(this.menuItemPractice_Click);
			// 
			// menuItemProcedureButtons
			// 
			this.menuItemProcedureButtons.Index = 26;
			this.menuItemProcedureButtons.Text = "Procedure Buttons";
			this.menuItemProcedureButtons.Click += new System.EventHandler(this.menuItemProcedureButtons_Click);
			// 
			// menuItemLinks
			// 
			this.menuItemLinks.Index = 27;
			this.menuItemLinks.Text = "Program Links";
			this.menuItemLinks.Click += new System.EventHandler(this.menuItemLinks_Click);
			// 
			// menuItem_ProviderAllocatorSetup
			// 
			this.menuItem_ProviderAllocatorSetup.Index = 28;
			this.menuItem_ProviderAllocatorSetup.Text = "Provider Allocator Setup";
			this.menuItem_ProviderAllocatorSetup.Click += new System.EventHandler(this.menuItem_ProviderAllocatorSetup_Click);
			// 
			// menuItemQuestions
			// 
			this.menuItemQuestions.Index = 29;
			this.menuItemQuestions.Text = "Questionnaire";
			this.menuItemQuestions.Click += new System.EventHandler(this.menuItemQuestions_Click);
			// 
			// menuItemRecall
			// 
			this.menuItemRecall.Index = 30;
			this.menuItemRecall.Text = "Recall";
			this.menuItemRecall.Click += new System.EventHandler(this.menuItemRecall_Click);
			// 
			// menuItemRecallTypes
			// 
			this.menuItemRecallTypes.Index = 31;
			this.menuItemRecallTypes.Text = "RecallTypes";
			this.menuItemRecallTypes.Click += new System.EventHandler(this.menuItemRecallTypes_Click);
			// 
			// menuItemRequirementsNeeded
			// 
			this.menuItemRequirementsNeeded.Index = 32;
			this.menuItemRequirementsNeeded.Text = "Requirements Needed";
			this.menuItemRequirementsNeeded.Click += new System.EventHandler(this.menuItemRequirementsNeeded_Click);
			// 
			// menuItemSched
			// 
			this.menuItemSched.Index = 33;
			this.menuItemSched.Text = "Schedules";
			this.menuItemSched.Click += new System.EventHandler(this.menuItemSched_Click);
			// 
			// menuItemSecurity
			// 
			this.menuItemSecurity.Index = 34;
			this.menuItemSecurity.Text = "Security";
			this.menuItemSecurity.Click += new System.EventHandler(this.menuItemSecurity_Click);
			// 
			// menuItemSheets
			// 
			this.menuItemSheets.Index = 35;
			this.menuItemSheets.Text = "Sheets";
			this.menuItemSheets.Click += new System.EventHandler(this.menuItemSheets_Click);
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
            this.menuItemAuditTrail,
            this.menuItemDatabaseMaintenance,
            this.menuItemImportXML,
            this.menuItemAging,
            this.menuItemFinanceCharge,
            this.menuItemRepeatingCharges,
            this.menuItemTranslation,
            this.menuItemScreening,
            this.menuItemTerminal,
            this.menuItemTerminalManager,
            this.menuItemReqStudents,
            this.menuItemMergeDatabases,
            this.menuItemCustomerManage});
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
            this.menuItemPatientImport,
            this.menuItemCreateAtoZFolders,
            this.menuItemReallocate});
			this.menuItem1.Text = "Misc Tools";
			// 
			// menuTelephone
			// 
			this.menuTelephone.Index = 0;
			this.menuTelephone.Text = "Telephone Numbers";
			this.menuTelephone.Click += new System.EventHandler(this.menuTelephone_Click);
			// 
			// menuItemPatientImport
			// 
			this.menuItemPatientImport.Index = 1;
			this.menuItemPatientImport.Text = "Import Patient From Text File";
			this.menuItemPatientImport.Click += new System.EventHandler(this.menuItemPatientImport_Click);
			// 
			// menuItemCreateAtoZFolders
			// 
			this.menuItemCreateAtoZFolders.Index = 2;
			this.menuItemCreateAtoZFolders.Text = "Create A to Z Folders";
			this.menuItemCreateAtoZFolders.Click += new System.EventHandler(this.menuItemCreateAtoZFolders_Click);
			// 
			// menuItemReallocate
			// 
			this.menuItemReallocate.Index = 3;
			this.menuItemReallocate.Text = "Reallocate Family Balances";
			this.menuItemReallocate.Click += new System.EventHandler(this.menuItemReallocate_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.Text = "-";
			// 
			// menuItemAuditTrail
			// 
			this.menuItemAuditTrail.Index = 3;
			this.menuItemAuditTrail.Text = "Audit Trail";
			this.menuItemAuditTrail.Click += new System.EventHandler(this.menuItemAuditTrail_Click);
			// 
			// menuItemDatabaseMaintenance
			// 
			this.menuItemDatabaseMaintenance.Index = 4;
			this.menuItemDatabaseMaintenance.Text = "Database Maintenance";
			this.menuItemDatabaseMaintenance.Click += new System.EventHandler(this.menuItemDatabaseMaintenance_Click);
			// 
			// menuItemImportXML
			// 
			this.menuItemImportXML.Index = 5;
			this.menuItemImportXML.Text = "Import Patient XML";
			this.menuItemImportXML.Click += new System.EventHandler(this.menuItemImportXML_Click);
			// 
			// menuItemAging
			// 
			this.menuItemAging.Index = 6;
			this.menuItemAging.Text = "Calculate &Aging";
			this.menuItemAging.Click += new System.EventHandler(this.menuItemAging_Click);
			// 
			// menuItemFinanceCharge
			// 
			this.menuItemFinanceCharge.Index = 7;
			this.menuItemFinanceCharge.Text = "Run &Finance/Billing Charges";
			this.menuItemFinanceCharge.Click += new System.EventHandler(this.menuItemFinanceCharge_Click);
			// 
			// menuItemRepeatingCharges
			// 
			this.menuItemRepeatingCharges.Index = 8;
			this.menuItemRepeatingCharges.Text = "Update Repeating Charges";
			this.menuItemRepeatingCharges.Click += new System.EventHandler(this.menuItemRepeatingCharges_Click);
			// 
			// menuItemTranslation
			// 
			this.menuItemTranslation.Index = 9;
			this.menuItemTranslation.Text = "Language Translation";
			this.menuItemTranslation.Click += new System.EventHandler(this.menuItemTranslation_Click);
			// 
			// menuItemScreening
			// 
			this.menuItemScreening.Index = 10;
			this.menuItemScreening.Text = "Public Health Screening";
			this.menuItemScreening.Click += new System.EventHandler(this.menuItemScreening_Click);
			// 
			// menuItemTerminal
			// 
			this.menuItemTerminal.Index = 11;
			this.menuItemTerminal.Text = "Terminal";
			this.menuItemTerminal.Click += new System.EventHandler(this.menuItemTerminal_Click);
			// 
			// menuItemTerminalManager
			// 
			this.menuItemTerminalManager.Index = 12;
			this.menuItemTerminalManager.Text = "Terminal Manager";
			this.menuItemTerminalManager.Click += new System.EventHandler(this.menuItemTerminalManager_Click);
			// 
			// menuItemReqStudents
			// 
			this.menuItemReqStudents.Index = 13;
			this.menuItemReqStudents.Text = "Student Requirements";
			this.menuItemReqStudents.Click += new System.EventHandler(this.menuItemReqStudents_Click);
			// 
			// menuItemMergeDatabases
			// 
			this.menuItemMergeDatabases.Index = 14;
			this.menuItemMergeDatabases.Text = "Merge Replicating Databases";
			this.menuItemMergeDatabases.Visible = false;
			this.menuItemMergeDatabases.Click += new System.EventHandler(this.menuItemMergeDatabases_Click);
			// 
			// menuItemCustomerManage
			// 
			this.menuItemCustomerManage.Index = 15;
			this.menuItemCustomerManage.Text = "Customer Management";
			this.menuItemCustomerManage.Click += new System.EventHandler(this.menuItemCustomerManage_Click);
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
			this.panelSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseMove);
			this.panelSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSplitter_MouseDown);
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
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(51, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(931, 25);
			this.ToolBarMain.TabIndex = 178;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// userControlTasks1
			// 
			this.userControlTasks1.Location = new System.Drawing.Point(57, 498);
			this.userControlTasks1.Name = "userControlTasks1";
			this.userControlTasks1.Size = new System.Drawing.Size(783, 139);
			this.userControlTasks1.TabIndex = 28;
			this.userControlTasks1.Visible = false;
			this.userControlTasks1.GoToChanged += new System.EventHandler(this.userControlTasks1_GoToChanged);
			// 
			// ContrManage2
			// 
			this.ContrManage2.Location = new System.Drawing.Point(77, 31);
			this.ContrManage2.Name = "ContrManage2";
			this.ContrManage2.Size = new System.Drawing.Size(877, 547);
			this.ContrManage2.TabIndex = 27;
			this.ContrManage2.Visible = false;
			// 
			// ContrChart2
			// 
			this.ContrChart2.Location = new System.Drawing.Point(77, 49);
			this.ContrChart2.Name = "ContrChart2";
			this.ContrChart2.Size = new System.Drawing.Size(865, 589);
			this.ContrChart2.TabIndex = 26;
			this.ContrChart2.Visible = false;
			// 
			// ContrDocs2
			// 
			this.ContrDocs2.Location = new System.Drawing.Point(97, 41);
			this.ContrDocs2.Name = "ContrDocs2";
			this.ContrDocs2.Size = new System.Drawing.Size(845, 606);
			this.ContrDocs2.TabIndex = 25;
			this.ContrDocs2.Visible = false;
			// 
			// ContrTreat2
			// 
			this.ContrTreat2.Location = new System.Drawing.Point(97, 47);
			this.ContrTreat2.Name = "ContrTreat2";
			this.ContrTreat2.Size = new System.Drawing.Size(857, 612);
			this.ContrTreat2.TabIndex = 24;
			this.ContrTreat2.Visible = false;
			// 
			// ContrAccount2
			// 
			this.ContrAccount2.Location = new System.Drawing.Point(109, 38);
			this.ContrAccount2.Name = "ContrAccount2";
			this.ContrAccount2.Size = new System.Drawing.Size(796, 599);
			this.ContrAccount2.TabIndex = 23;
			this.ContrAccount2.Visible = false;
			// 
			// ContrFamily2
			// 
			this.ContrFamily2.Location = new System.Drawing.Point(109, 38);
			this.ContrFamily2.Name = "ContrFamily2";
			this.ContrFamily2.Size = new System.Drawing.Size(845, 599);
			this.ContrFamily2.TabIndex = 22;
			this.ContrFamily2.Visible = false;
			// 
			// ContrAppt2
			// 
			this.ContrAppt2.Location = new System.Drawing.Point(97, 42);
			this.ContrAppt2.Name = "ContrAppt2";
			this.ContrAppt2.Size = new System.Drawing.Size(857, 643);
			this.ContrAppt2.TabIndex = 21;
			this.ContrAppt2.Visible = false;
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
			// myOutlookBar
			// 
			this.myOutlookBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.myOutlookBar.ImageList = this.imageList32;
			this.myOutlookBar.Location = new System.Drawing.Point(0, 0);
			this.myOutlookBar.Name = "myOutlookBar";
			this.myOutlookBar.Size = new System.Drawing.Size(51, 0);
			this.myOutlookBar.TabIndex = 18;
			this.myOutlookBar.Text = "outlookBar1";
			this.myOutlookBar.ButtonClicked += new OpenDental.ButtonClickedEventHandler(this.myOutlookBar_ButtonClicked);
			// 
			// smartCardWatcher1
			// 
			this.smartCardWatcher1.PatientCardInserted += new OpenDental.SmartCards.PatientCardInsertedEventHandler(this.OnPatientCardInserted);
			// 
			// FormOpenDental
			// 
			this.ClientSize = new System.Drawing.Size(982, 0);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.panelSplitter);
			this.Controls.Add(this.userControlTasks1);
			this.Controls.Add(this.ContrManage2);
			this.Controls.Add(this.ContrChart2);
			this.Controls.Add(this.ContrDocs2);
			this.Controls.Add(this.ContrTreat2);
			this.Controls.Add(this.ContrAccount2);
			this.Controls.Add(this.ContrFamily2);
			this.Controls.Add(this.ContrAppt2);
			this.Controls.Add(this.lightSignalGrid1);
			this.Controls.Add(this.myOutlookBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Menu = this.mainMenu;
			this.Name = "FormOpenDental";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "Open Dental";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormOpenDental_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormOpenDental_Closing);
			this.Resize += new System.EventHandler(this.FormOpenDental_Resize);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOpenDental_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion
	
		[STAThread]
		static void Main() {
			//Register an EventHandler which handles unhandeled exceptions.
			//AppDomain.CurrentDomain.UnhandledException+=new UnhandledExceptionEventHandler(OnUnhandeledExceptionPolicy);
			Application.EnableVisualStyles();//changes appearance to XP
			Application.DoEvents();//workaround for a known MS bug in the line above
			Application.Run(new FormOpenDental());
		}

		/*
		///<summary>Overrides the default Windows unhandled exception functionality.</summary>
		static void OnUnhandeledExceptionPolicy(Object Sender,UnhandledExceptionEventArgs e) {
			Exception ex=e.ExceptionObject as Exception;
			string message="Unhandled Exception: ";
			if(ex!=null) {//The unhandeled Exception is CLS compliant.
				message+=ex.ToString();
			}else{//The unhandeled Exception is not CLS compliant.				
				//You can only handle this Exception as Object
				message+="Object Type: "+e.ExceptionObject.GetType()+", Object String: "+e.ExceptionObject.ToString();
			}
			if(!e.IsTerminating){
				//Exception occurred in a thread pool or finalizer thread. Debugger launches only explicitly.
				Logger.openlog.LogMB(message,Logger.Severity.ERROR);
#if(DEBUG)
				Debugger.Launch();
#endif
			}else{
				//Exception occurred in managed thread. Debugger will automatically launch in visual studio.
				Logger.openlog.LogMB(message,Logger.Severity.FATAL_ERROR);
			}
		}*/

		private void FormOpenDental_Load(object sender, System.EventArgs e){
			Splash.Dispose();
			allNeutral();
			if(GetOraConfig()){  //Oracle config file exists
				if(!TryWithConnStr()){  //connection failed
					Application.Exit();
					return;
				}
			}
			else{
				FormChooseDatabase formChooseDb=new FormChooseDatabase();
				formChooseDb.GetConfig();
				if(formChooseDb.NoShow) {
					//DataClass.SetConnection();
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
			}
			Cursor=Cursors.WaitCursor;
			Splash=new FormSplash();
			Splash.Show();
			if(!PrefsStartup()){
				Cursor=Cursors.Default;
				Splash.Dispose();
				return;
			}
			//This next line used to read InvalidTypes.AllLocal-InvalidTypes.Prefs.  But we can't really do that now.
			RefreshLocalData(InvalidType.AllLocal);
			//Lan.Refresh();//automatically skips if current culture is en-US
			//LanguageForeigns.Refresh(CultureInfo.CurrentCulture);//automatically skips if current culture is en-US
			DataValid.BecameInvalid += new OpenDental.ValidEventHandler(DataValid_BecameInvalid);
			signalLastRefreshed=MiscData.GetNowDateTime();
			timerSignals.Interval=PrefC.GetInt("ProcessSigsIntervalInSecs")*1000;
			timerSignals.Enabled=true;
			ContrAccount2.InitializeOnStartup();
			ContrAppt2.InitializeOnStartup();
			ContrChart2.InitializeOnStartup();
			ContrDocs2.InitializeOnStartup();
			ContrFamily2.InitializeOnStartup();
			ContrManage2.InitializeOnStartup();
			ContrTreat2.InitializeOnStartup();
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
			}
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				menuItemTranslation.Visible=false;
			}
			if(!File.Exists("Help.chm")){
				menuItemHelpWindows.Visible=false;
			}
			//if(!File.Exists("remoteclient.exe")){
			//	menuItemRemote.Visible=false;
			//}
			//menuItemClaimForms.Visible=PrefC.UsingAtoZfolder;
			if(Environment.OSVersion.Platform==PlatformID.Unix){//Create A to Z unsupported on Unix for now.
				menuItemCreateAtoZFolders.Visible=false;
			}
			#if !DEBUG
				//only visible in debug mode.  It's only usefulness is after a conversion, so no need for user to see it.
				menuItemReallocate.Visible=false;
			#endif
			if(!PrefC.GetBool("ADAdescriptionsReset")) {
				ProcedureCodes.ResetADAdescriptions();
			}
			Splash.Dispose();
			Userod adminUser=Userods.GetAdminUser();
			if(adminUser.Password=="") {
				Security.CurUser=adminUser.Copy();
			}
			else {
				FormLogOn FormL=new FormLogOn();
				FormL.ShowDialog();
				if(FormL.DialogResult==DialogResult.Cancel) {
					Cursor=Cursors.Default;
					Application.Exit();
					return;
				}
			}
			if(userControlTasks1.Visible) {
				userControlTasks1.InitializeOnStartup();
			}
			myOutlookBar.SelectedIndex=Security.GetModule(0);
			myOutlookBar.Invalidate();
			LayoutToolBar();
			SetModuleSelected();
			Cursor=Cursors.Default;
			if(myOutlookBar.SelectedIndex==-1){
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
			Bridges.Trojan.StartupCheck();
			FormUAppoint.StartThreadIfEnabled();
		}

		///<summary>Returns false if it can't complete a conversion, find datapath, or validate registration key.</summary>
		private bool PrefsStartup(){
			CacheL.Refresh(InvalidType.Prefs);
			if(!PrefL.CheckMySqlVersion41()){
				return false;
			}
			if(!PrefL.ConvertDB()){
				return false;
			}
			if(PrefC.UsingAtoZfolder && FormPath.GetPreferredImagePath()==null){//AtoZ folder not found
				CacheL.Refresh(InvalidType.Security);
				FormPath FormP=new FormPath();
				FormP.ShowDialog();
				if(FormP.DialogResult!=DialogResult.OK){
					return false;
				}
				//bool usingAtoZ=FormPath.UsingImagePath();
				//ContrDocs2.Enabled=usingAtoZ;
				//menuItemClaimForms.Visible=usingAtoZ;
				//CheckCustomReports();
				//this.RefreshCurrentModule();
				CacheL.Refresh(InvalidType.Prefs);//because listening thread not started yet.
			}
			if(!PrefL.CheckProgramVersion()){
				return false;
			}
			if(!FormRegistrationKey.ValidateKey(PrefC.GetString("RegistrationKey"))){
				//true){
				FormRegistrationKey FormR=new FormRegistrationKey();
				FormR.ShowDialog();
				if(FormR.DialogResult!=DialogResult.OK){
					Application.Exit();
					return false;
				}
				CacheL.Refresh(InvalidType.Prefs);
			}
			Lan.Refresh();//automatically skips if current culture is en-US
			LanguageForeigns.Refresh(CultureInfo.CurrentCulture);//automatically skips if current culture is en-US
			menuItemMergeDatabases.Visible=PrefC.GetBool("RandomPrimaryKeys");
			return true;
		}

		///<summary>Refreshes certain rarely used data from database.  Must supply the types of data to refresh as flags.  Also performs a few other tasks that must be done when local data is changed.</summary>
		private void RefreshLocalData(params InvalidType[] itypes){
			List<int> itypeList=new List<int>();
			for(int i=0;i<itypes.Length;i++){
				itypeList.Add((int)itypes[i]);
			}
			CacheL.Refresh(itypeList);
			bool isAll=false;
			if(itypeList.Contains((int)InvalidType.AllLocal)){
				isAll=true;
			}
			if(itypeList.Contains((int)InvalidType.Prefs) || isAll){
			//if((itypes & InvalidTypes.Prefs)==InvalidTypes.Prefs){
				//Prefs_client.RefreshClient();
				if(((Pref)PrefC.HList["EasyHidePublicHealth"]).ValueString=="1"){
					menuItemSchools.Visible=false;
					menuItemCounties.Visible=false;
					menuItemScreening.Visible=false;
					//menuItemPHSep.Visible=false;
					//menuItemPHRawScreen.Visible=false;
					//menuItemPHRawPop.Visible=false;
					//menuItemPHScreen.Visible=false;
				}
				else{
					menuItemSchools.Visible=true;
					menuItemCounties.Visible=true;
					menuItemScreening.Visible=true;
					//menuItemPHSep.Visible=true;
					//menuItemPublicHealth.Visible=true;
					//menuItemPHRawScreen.Visible=true;
					//menuItemPHRawPop.Visible=true;
					//menuItemPHScreen.Visible=true;
				}
				if(PrefC.GetBool("EasyNoClinics")){
					menuItemClinics.Visible=false;
				}
				else{
					menuItemClinics.Visible=true;
				}
				if(((Pref)PrefC.HList["EasyHideClinical"]).ValueString=="1"){
					myOutlookBar.Buttons[4].Caption=Lan.g(this,"Procs");
				}
				else{
					myOutlookBar.Buttons[4].Caption=Lan.g(this,"Chart");
				}
				if(((Pref)PrefC.HList["EasyBasicModules"]).ValueString=="1"){
					myOutlookBar.Buttons[3].Visible=false;
					myOutlookBar.Buttons[5].Visible=false;
					myOutlookBar.Buttons[6].Visible=false;
					//pictButtons.Visible=false;
				}
				else{
					myOutlookBar.Buttons[3].Visible=true;
					myOutlookBar.Buttons[5].Visible=true;
					myOutlookBar.Buttons[6].Visible=true;
					//pictButtons.Visible=true;
				}
				myOutlookBar.Invalidate();
				if(PrefC.GetBool("EasyHideDentalSchools")){
					menuItemSchoolClass.Visible=false;
					menuItemSchoolCourses.Visible=false;
					//menuItemInstructors.Visible=false;
					//menuItemCourseGrades.Visible=false;
					menuItemRequirementsNeeded.Visible=false;
					menuItemReqStudents.Visible=false;
				}
				else{
					menuItemSchoolClass.Visible=true;
					menuItemSchoolCourses.Visible=true;
					//menuItemInstructors.Visible=true;
					//menuItemCourseGrades.Visible=true;
					menuItemRequirementsNeeded.Visible=true;
					menuItemReqStudents.Visible=true;
				}
				if(PrefC.GetBool("EasyHideRepeatCharges")){
					menuItemRepeatingCharges.Visible=false;
				}
				else{
					menuItemRepeatingCharges.Visible=true;
				}

				if(PrefC.GetString("DistributorKey")=="") {
					menuItemCustomerManage.Visible=false;
				}
				else {
					menuItemCustomerManage.Visible=true;
				}
				ContrDocs2.Enabled=PrefC.UsingAtoZfolder;
				//menuItemClaimForms.Visible=PrefC.UsingAtoZfolder;
				CheckCustomReports();
				ContrChart2.InitializeLocalData();
				if(PrefC.GetBool("TaskListAlwaysShowsAtBottom")){
					//separate if statement to prevent database call if not showing task list at bottom to begin with
					ComputerPref computerPref = ComputerPrefs.GetForLocalComputer();
					if(computerPref.TaskKeepListHidden){
						userControlTasks1.Visible = false;
					}
					else{//task list show
						userControlTasks1.Visible = true;
						userControlTasks1.InitializeOnStartup();
						if(computerPref.TaskDock == 0){//bottom
							menuItemDockBottom.Checked = true;
							menuItemDockRight.Checked = false;
							panelSplitter.Cursor=Cursors.HSplit;
							panelSplitter.Height=7;
							int splitterNewY=540;
							if(computerPref.TaskY!=0){
								splitterNewY=computerPref.TaskY;
								if(splitterNewY<300){
									splitterNewY=300;//keeps it from going too high
								}
								if(splitterNewY>ClientSize.Height-50){
									splitterNewY=ClientSize.Height-panelSplitter.Height-50;//keeps it from going off the bottom edge
								}
							}
							panelSplitter.Location=new Point(myOutlookBar.Width,splitterNewY);
						}
						else{//right
							menuItemDockRight.Checked = true;
							menuItemDockBottom.Checked = false;
							panelSplitter.Cursor=Cursors.VSplit;
							panelSplitter.Width=7;
							int splitterNewX=900;
							if(computerPref.TaskX!=0){
								splitterNewX=computerPref.TaskX;
								if(splitterNewX<300){
									splitterNewX=300;//keeps it from going too far to the left
								}
								if(splitterNewX>ClientSize.Width-50){
									splitterNewX=ClientSize.Width-panelSplitter.Width-50;//keeps it from going off the right edge
								}
							}
							panelSplitter.Location=new Point(splitterNewX,ToolBarMain.Height);
						}
					}
				}
				else{
					userControlTasks1.Visible = false;
				}
				LayoutControls();
			}//if(InvalidTypes.Prefs)
			if(itypeList.Contains((int)InvalidType.AutoCodesProcButtons) || isAll){
				AutoCodeL.Refresh();
				AutoCodeItemL.Refresh();
				AutoCodeCondL.Refresh();
				ProcButtons.Refresh();
				ProcButtonItems.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Carriers) || isAll){
				Carriers.Refresh();//run on startup, after telephone reformat, after list edit.
			}
			if(itypeList.Contains((int)InvalidType.ClaimForms) || isAll){
				ClaimFormItemL.Refresh();
				ClaimForms.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.ClearHouses) || isAll){
				//kh until we add an EasyHideClearHouses						Clearinghouses.Refresh();
				SigElementDefs.Refresh();
				SigButDefs.Refresh();//includes SigButDefElements.Refresh()
				FillSignalButtons(null);
			}
			if(itypeList.Contains((int)InvalidType.Computers) || isAll){
				Computers.Refresh();
				Printers.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Defs) || isAll){
			//if((itypes & InvalidTypes.Defs)==InvalidTypes.Defs){
				//Defs_client.RefreshClient();
			}
			if(itypeList.Contains((int)InvalidType.DentalSchools) || isAll){
				SchoolClasses.Refresh();
				SchoolCourses.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Email) || isAll){
			//if((itypes & InvalidTypes.Email)==InvalidTypes.Email){
				EmailTemplates.Refresh();
				DiseaseDefs.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Employees) || isAll){
				Employees.Refresh();
				PayPeriods.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Fees) || isAll){
				Fees.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.InsCats) || isAll){
				CovCatL.Refresh();
				CovSpanL.Refresh();
				DisplayFields.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Letters) || isAll){
				Letters.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.LetterMerge) || isAll){
				LetterMergeFields.Refresh();
				LetterMerges.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Operatories) || isAll){
				//Operatory_client.Refresh();
				AccountingAutoPayL.Refresh();
			}
			//if((itypes & InvalidTypes.Prefs)==InvalidTypes.Prefs){

			//}
			if(itypeList.Contains((int)InvalidType.ProcCodes) || isAll){
				ProcedureCodes.Refresh();
				ProcCodeNotes.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Programs) || isAll){
				Programs.Refresh();
				ProgramProperties.Refresh();
				if(Programs.GetCur("PT").Enabled){
					Bridges.PaperlessTechnology.InitializeFileWatcher();
				}
			}
			if(itypeList.Contains((int)InvalidType.Providers) || isAll){
				//Provider_client.RefreshOnClient();
				ProviderIdents.Refresh();
				Clinics.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.QuickPaste) || isAll){
				QuickPasteNotes.Refresh();
				QuickPasteCats.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Security) || isAll){
				//Userod_client.Refresh();
				UserGroups.Refresh();
				GroupPermissionL.Refresh();
			}
			if(itypeList.Contains((int)InvalidType.Startup) || isAll){
				Employers.Refresh();//only needed when opening the prog. After that, automated.
				ElectIDs.Refresh();//only run on startup
				Referrals.Refresh();//Referrals are also refreshed dynamically.
			}
			//InvalidTypes.Tasks not handled here.
			if(itypeList.Contains((int)InvalidType.ToolBut) || isAll){
				ToolButItems.Refresh();
				ContrAccount2.LayoutToolBar();
				ContrAppt2.LayoutToolBar();
				ContrChart2.LayoutToolBar();
				ContrDocs2.LayoutToolBar();
				ContrFamily2.LayoutToolBar();
			}
			if(itypeList.Contains((int)InvalidType.Views) || isAll){
				AppointmentRuleL.Refresh();
				//ApptView_client.Refresh();
				//ApptViewItem_client.Refresh();
				ContrAppt2.FillViews();
			}
			if(itypeList.Contains((int)InvalidType.ZipCodes) || isAll){
				ZipCodes.Refresh();
				PatFieldDefs.Refresh();
			}
			ContrTreat2.InitializeLocalData();//easier to leave this here for now than to split it.
		}

		///<summary>Sets up the custom reports list in the main menu when certain requirements are met, or disables the custom reports menu item when those same conditions are not met. This function is called during initialization, and on the event that the A to Z folder usage has changed.</summary>
		private void CheckCustomReports(){
			menuItemCustomReports.MenuItems.Clear();
			//Try to load custom reports, but only if using the A to Z folders.
			if(PrefC.UsingAtoZfolder) {
				string imagePath=FormPath.GetPreferredImagePath();
				string reportFolderName=PrefC.GetString("ReportFolderName");
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Commlog"),1,Lan.g(this,"New Commlog Entry"),"Commlog"));
			button=new ODToolBarButton(Lan.g(this,"E-mail"),2,Lan.g(this,"Send E-mail"),"Email");
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton("",-1,"","EmailDropdown");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuEmail;
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton(Lan.g(this,"Letter"),-1,Lan.g(this,"Quick Letter"),"Letter");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuLetter;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"To Task List"),3,Lan.g(this,"Send to Task List"),"Tasklist"));
			button=new ODToolBarButton(Lan.g(this,"Label"),4,Lan.g(this,"Print Label"),"Label");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuLabel;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Popups"),-1,Lan.g(this,"Edit popups for this patient"),"Popups"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.AllModules);
			for(int i=0;i<toolButItems.Count;i++) {
				//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void menuPatient_Popup(object sender,EventArgs e) {
			if(CurPatNum==0){
				return;
			}
			Family fam=Patients.GetFamily(CurPatNum);
			Patients.AddFamilyToMenu(menuPatient,new EventHandler(menuPatient_Click),CurPatNum,fam);
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
					case "Letter":
						OnLetter_Click();
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
			else if(e.Button.Tag.GetType()==typeof(int)) {
				Programs.Execute((int)e.Button.Tag,Patients.GetPat(CurPatNum));
			}
		}

		private void OnPatient_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK) {
				CurPatNum=formPS.SelectedPatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				RefreshCurrentModule();
				FillPatientButton(CurPatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber,pat.SiteNum);
			}
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			Family fam=Patients.GetFamily(CurPatNum);
			CurPatNum=Patients.ButtonSelect(menuPatient,sender,fam);
			//new family now
			Patient pat=Patients.GetPat(CurPatNum);
			RefreshCurrentModule();
			FillPatientButton(CurPatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber,pat.SiteNum);
		}

		///<summary>Happens when any of the modules changes the current patient or when this main form changes the patient.  The calling module should refresh itself.  The current patNum is stored here in the parent form so that when switching modules, the parent form knows which patient to call up for that module.</summary>
		private void Contr_PatientSelected(object sender,PatientSelectedEventArgs e) {
			CurPatNum=e.PatNum;
			int siteNum=0;
			if(PrefC.GetBool("TitleBarShowSite") && e.PatNum!=0){
				Patient pat=Patients.GetPat(e.PatNum);
				siteNum=pat.SiteNum;
			}
			FillPatientButton(CurPatNum,e.PatName,e.HasEmail,e.ChartNumber,siteNum);
		}

		///<Summary>Serves four functions.  1. Sends the new patient to the dropdown menu for select patient.  2. Changes which toolbar buttons are enabled.  3. Sets main form text. 4. Displays any popup.</Summary>
		private void FillPatientButton(int patNum,string patName,bool hasEmail,string chartNumber,int siteNum) {
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),patName,patNum);
			if(ToolBarMain.Buttons==null || ToolBarMain.Buttons.Count<2){
				return;
			}
			if(CurPatNum==0){//Only on startup, I think.
				ToolBarMain.Buttons["Email"].Enabled=false;
				ToolBarMain.Buttons["EmailDropdown"].Enabled=false;
				ToolBarMain.Buttons["Commlog"].Enabled=false;
				ToolBarMain.Buttons["Letter"].Enabled=false;
				ToolBarMain.Buttons["Tasklist"].Enabled=false;
				ToolBarMain.Buttons["Label"].Enabled=false;
				ToolBarMain.Buttons["Popups"].Enabled=false;
			}
			else{
				if(hasEmail){
					ToolBarMain.Buttons["Email"].Enabled=true;
				}
				else{
					ToolBarMain.Buttons["Email"].Enabled=false;
				}
				ToolBarMain.Buttons["EmailDropdown"].Enabled=true;
				ToolBarMain.Buttons["Commlog"].Enabled=true;
				ToolBarMain.Buttons["Letter"].Enabled=true;
				ToolBarMain.Buttons["Tasklist"].Enabled=true;
				ToolBarMain.Buttons["Label"].Enabled=true;
				ToolBarMain.Buttons["Popups"].Enabled=true;
			}
			ToolBarMain.Invalidate();
			Text=Patients2.GetMainTitle(patName,patNum,chartNumber,siteNum);
			if(PopupEventList==null){
				PopupEventList=new List<PopupEvent>();
			}
			bool popupIsDisabled=false;
			for(int i=PopupEventList.Count-1;i>=0;i--){//go backwards
				if(PopupEventList[i].DisableUntil<DateTime.Now){//expired
					PopupEventList.RemoveAt(i);
					continue;
				}
				if(PopupEventList[i].PatNum==patNum){
					popupIsDisabled=true;
					break;
				}
			}
			if(!popupIsDisabled){
				List<Popup> popList=Popups.CreateObjects(patNum);
				if(popList.Count>0 && !popList[0].IsDisabled) {
					if(ContrAppt2.Visible) {
						ContrAppt2.MouseUpForced();
					}
					//MessageBox.Show(popList[0].Description,Lan.g(this,"Popup"));
					FormPopupDisplay FormP=new FormPopupDisplay();
					FormP.PopupCur=popList[0];
					FormP.ShowDialog();
					if(FormP.MinutesDisabled>0){
						PopupEvent popevent=new PopupEvent();
						popevent.PatNum=patNum;
						popevent.DisableUntil=DateTime.Now+TimeSpan.FromMinutes(FormP.MinutesDisabled);
						PopupEventList.Add(popevent);
						PopupEventList.Sort();
					}
				}
			}
		}

		private void OnEmail_Click() {
			//this button item will be disabled if pat does not have email address
			EmailMessage message=new EmailMessage();
			message.PatNum=CurPatNum;
			Patient pat=Patients.GetPat(CurPatNum);
			message.ToAddress=pat.Email;
			message.FromAddress=PrefC.GetString("EmailSenderAddress");
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
			RefAttach[] refAttaches=RefAttaches.Refresh(CurPatNum);
			Referral refer;
			string str;
			for(int i=0;i<refAttaches.Length;i++) {
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
				message.FromAddress=PrefC.GetString("EmailSenderAddress");
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
			CommlogCur.Mode_=CommItemMode.Phone;
			CommlogCur.SentOrReceived=CommSentOrReceived.Received;
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI = new FormCommItem(CommlogCur);
			FormCI.IsNew = true;
			FormCI.ShowDialog();
			if(FormCI.DialogResult == DialogResult.OK) {
				RefreshCurrentModule();
			}
		}

		private void OnLetter_Click() {
			Patient pat=Patients.GetPat(CurPatNum);
			FormLetters FormL=new FormLetters(pat);
			FormL.ShowDialog();
		}

		private void menuLetter_Popup(object sender,EventArgs e) {
			menuLetter.MenuItems.Clear();
			MenuItem menuItem;
			menuItem=new MenuItem(Lan.g(this,"Merge"),menuLetter_Click);
			menuItem.Tag="Merge";
			menuLetter.MenuItems.Add(menuItem);
			menuItem=new MenuItem(Lan.g(this,"Stationery"),menuLetter_Click);
			menuItem.Tag="Stationery";
			menuLetter.MenuItems.Add(menuItem);
			menuLetter.MenuItems.Add("-");
			//Referrals---------------------------------------------------------------------------------------
			menuItem=new MenuItem(Lan.g(this,"Referrals:"));
			menuItem.Tag=null;
			menuLetter.MenuItems.Add(menuItem);
			RefAttach[] refAttaches=RefAttaches.Refresh(CurPatNum);
			Referral refer;
			string str;
			for(int i=0;i<refAttaches.Length;i++) {
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
				if(((MenuItem)sender).Tag.ToString()=="Stationery") {
					FormCommunications.PrintStationery(pat);
				}
			}
			if(((MenuItem)sender).Tag.GetType()==typeof(Referral)) {
				Referral refer=(Referral)((MenuItem)sender).Tag;
				FormLetters FormL=new FormLetters(pat);
				FormL.ReferralCur=refer;
				FormL.ShowDialog();
			}
		}

		private void OnTasklist_Click(){
			FormTaskListSelect FormT=new FormTaskListSelect(TaskObjectType.Patient,CurPatNum);
			FormT.Location=new Point(50,50);
			FormT.ShowDialog();
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
			PatPlan[] PatPlanList=PatPlans.Refresh(CurPatNum);
			InsPlan[] PlanList=InsPlans.Refresh(fam);
			Carrier carrier;
			InsPlan plan;
			for(int i=0;i<PatPlanList.Length;i++) {
				plan=InsPlans.GetPlan(PatPlanList[i].PlanNum,PlanList);
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
			RefAttach[] refAttaches=RefAttaches.Refresh(CurPatNum);
			Referral refer;
			string str;
			for(int i=0;i<refAttaches.Length;i++){
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
			List<Popup> popList=Popups.CreateObjects(CurPatNum);
			FormPopupEdit FormP=new FormPopupEdit();
			if(popList.Count==0) {
				Popup pop=new Popup();
				pop.PatNum=CurPatNum;
				pop.IsNew=true;
				FormP.PopupCur=pop;
			}
			else{
				FormP.PopupCur=popList[0];	
			}
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK){
				for(int i=PopupEventList.Count-1;i>=0;i--){
					if(PopupEventList[i].PatNum==CurPatNum){
						PopupEventList.RemoveAt(i);
					}
				}
			}
			if(FormP.MinutesDisabled>0) {
				PopupEvent popevent=new PopupEvent();
				popevent.PatNum=CurPatNum;
				popevent.DisableUntil=DateTime.Now+TimeSpan.FromMinutes(FormP.MinutesDisabled);
				PopupEventList.Add(popevent);
				PopupEventList.Sort();
			}
		}

		private void FormOpenDental_Resize(object sender,EventArgs e) {
			LayoutControls();
		}

		///<summary>This used to be called much more frequently when it was an actual layout event.</summary>
		private void LayoutControls(){
			//Debug.WriteLine("layout");
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
					if(PrefC.GetBool("DockPhonePanelShow")){
						phonePanel.Visible=true;
						phonePanel.Location=new Point(position.X,panelSplitter.Bottom);
						phonePanel.Width=240;
						phonePanel.Height=this.ClientSize.Height-userControlTasks1.Top;
						userControlTasks1.Location=new Point(position.X+phonePanel.Width,panelSplitter.Bottom);
						userControlTasks1.Width=width-phonePanel.Width;
					}
					else{
						phonePanel.Visible=false;
						userControlTasks1.Location=new Point(position.X,panelSplitter.Bottom);
						userControlTasks1.Width=width;
					}
					userControlTasks1.Height=this.ClientSize.Height-userControlTasks1.Top;
					height=ClientSize.Height-panelSplitter.Height-userControlTasks1.Height-ToolBarMain.Height;
				}
				else {//docked Right
					phonePanel.Visible=false;
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
				phonePanel.Visible=false;
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
			ContrDocs2.Location=position;
			ContrDocs2.Width=width;
			ContrDocs2.Height=height;
			ContrFamily2.Location=position;
			ContrFamily2.Width=width;
			ContrFamily2.Height=height;
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
			ComputerPref computerPref = ComputerPrefs.GetForLocalComputer();
			if(menuItemDockBottom.Checked){
				computerPref.TaskY = panelSplitter.Top;
				computerPref.TaskDock = 0;
			}
			else{
				computerPref.TaskX = panelSplitter.Left;
				computerPref.TaskDock = 1;
			}
			ComputerPrefs.Update(computerPref);
		}
		
		///<summary>This is called when any local data becomes outdated.  It's purpose is to tell the other computers to update certain local data.</summary>
		private void DataValid_BecameInvalid(OpenDental.ValidEventArgs e){
			if(e.OnlyLocal){
				if(!PrefsStartup()){//??
					return;
				}
				RefreshLocalData(InvalidType.AllLocal);//does local computer only
				return;
			}
			if(!e.ITypes.Contains((int)InvalidType.Date) 
				&& !e.ITypes.Contains((int)InvalidType.Task)
				&& !e.ITypes.Contains((int)InvalidType.TaskPopup)){
				//local refresh for dates is handled within ContrAppt, not here
				InvalidType[] itypeArray=new InvalidType[e.ITypes.Count];
				for(int i=0;i<itypeArray.Length;i++){
					itypeArray[i]=(InvalidType)e.ITypes[i];
				}
				RefreshLocalData(itypeArray);//does local computer
			}
			string itypeString="";
			for(int i=0;i<e.ITypes.Count;i++){
				if(i>0){
					itypeString+=",";
				}
				itypeString+=e.ITypes[i].ToString();
			}
			Signal sig=new Signal();
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
			Signals.Insert(sig);
		}

		private void GotoModule_ModuleSelected(ModuleEventArgs e){
			if(e.DateSelected.Year>1880){
				Appointments.DateSelected=e.DateSelected;
			}
			if(e.SelectedAptNum>0){
				ContrApptSingle.SelectedAptNum=e.SelectedAptNum;
			}
			//patient would have been set separately ahead of time
			//CurPatNum=Appointments.Cur.PatNum;
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
			else if(e.IModule!=-1){
				myOutlookBar.SelectedIndex=e.IModule;
				SetModuleSelected();
			}
			myOutlookBar.Invalidate();
		}

		///<summary>If this is initial call when opening program, then set sigListButs=null.  This will cause a call to be made to database to get current status of buttons.  Otherwise, it adds the signals passed in to the current state, then paints.</summary>
		private void FillSignalButtons(Signal[] sigListButs){
			if(sigListButs==null){
				SigButDefList=SigButDefs.GetByComputer(SystemInformation.ComputerName);
				lightSignalGrid1.SetButtons(SigButDefList);
				sigListButs=Signals.RefreshCurrentButState();
			}
			SigElementDef element;
			SigButDef butDef;
			int row;
			Color color;
			for(int i=0;i<sigListButs.Length;i++){
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
							PaintOnIcon(butDef.SynchIcon,color);
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
				Signals.AckButton(e.ButtonIndex+1,signalLastRefreshed);
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
			Signal sig=new Signal();
			sig.SigType=SignalType.Button;
			//sig.ToUser=sigElementDefUser[listTo.SelectedIndex].SigText;
			//sig.FromUser=sigElementDefUser[listFrom.SelectedIndex].SigText;
			//need to do this all as a transaction?
			Signals.Insert(sig);
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
					Signals.Update(sig);
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
			Signal[] sigList=Signals.RefreshTimed(signalLastRefreshed);//this also attaches all elements to their sigs
			if(sigList.Length==0){
				return;
			}
			if(Security.CurUser==null){
				return;
			}
			if(sigList[sigList.Length-1].AckTime.Year>1880){
				signalLastRefreshed=sigList[sigList.Length-1].AckTime;
			}
			else{
				signalLastRefreshed=sigList[sigList.Length-1].SigDateTime;
			}
			if(ContrAppt2.Visible && Signals.ApptNeedsRefresh(sigList,Appointments.DateSelected.Date)){
				ContrAppt2.RefreshPeriod();
			}
			bool areAnySignalsTasks=false;
			for(int i=0;i<sigList.Length;i++){
				if(sigList[i].ITypes==((int)InvalidType.Task).ToString()
					|| sigList[i].ITypes==((int)InvalidType.TaskPopup).ToString())
				{
					areAnySignalsTasks=true;
				}
			}
			List<Task> tasksPopup=Signals.GetNewTaskPopupsThisUser(sigList,Security.CurUser.UserNum);
			if(tasksPopup.Count>0){
				for(int i=0;i<tasksPopup.Count;i++){
					//Even though this is triggered to popup, if this is my own task, then do not popup.
					if(tasksPopup[i].UserNum==Security.CurUser.UserNum){
						continue;
					}
					System.Media.SoundPlayer soundplay=new SoundPlayer(Properties.Resources.notify);
					soundplay.Play();
					this.BringToFront();//don't know if this is doing anything.
					FormTaskEdit FormT=new FormTaskEdit(tasksPopup[i]);
					FormT.IsPopup=true;
					FormT.ShowDialog();
				}
			}
			if(areAnySignalsTasks || tasksPopup.Count>0){
				//if user has the Task dialog open, we can't easily tell it to refresh,
				//So that dialog is responsible for auto refreshing every minute on a timer.
				if(userControlTasks1.Visible){
					userControlTasks1.RefreshTasks();
				}
			}
			List<int> itypes=Signals.GetInvalidTypes(sigList);
			InvalidType[] itypeArray=new InvalidType[itypes.Count];
			for(int i=0;i<itypeArray.Length;i++){
				itypeArray[i]=(InvalidType)itypes[i];
			}
			//InvalidTypes invalidTypes=Signals.GetInvalidTypes(sigList);
			if(itypes.Count>0){//invalidTypes!=0){
				RefreshLocalData(itypeArray);
			}
			Signal[] sigListButs=Signals.GetButtonSigs(sigList);
			ContrManage2.LogMsgs(sigListButs);
			FillSignalButtons(sigListButs);
			//Need to add a test to this: do not play messages that are over 2 minutes old.
			Thread newThread=new Thread(new ParameterizedThreadStart(PlaySounds));
			newThread.Start(sigListButs);
		}

		private void PlaySounds(Object objSignalList){
			Signal[] signalList=(Signal[])objSignalList;
			string strSound;
			byte[] rawData;
			MemoryStream stream;
			SoundPlayer simpleSound;
			//loop through each signal
			for(int s=0;s<signalList.Length;s++){
				if(signalList[s].AckTime.Year>1880){
					continue;//don't play any sounds for acks.
				}
				if(s>0){
					Thread.Sleep(1000);//pause 1 second between signals.
				}
				//play all the sounds.
				for(int e=0;e<signalList[s].ElementList.Length;e++){
					strSound=SigElementDefs.GetElement(signalList[s].ElementList[e].SigElementDefNum).Sound;
					if(strSound==""){
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

		private void myOutlookBar_ButtonClicked(object sender, OpenDental.ButtonClicked_EventArgs e){
			switch(myOutlookBar.SelectedIndex){
				case 0:
					if(!Security.IsAuthorized(Permissions.AppointmentsModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 1:
					if(!Security.IsAuthorized(Permissions.FamilyModule)){
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
					ContrAppt2.Visible=true;
					this.ActiveControl=this.ContrAppt2;
					ContrAppt2.ModuleSelected(CurPatNum);
					break;
				case 1:
					ContrFamily2.Visible=true;
					this.ActiveControl=this.ContrFamily2;
					ContrFamily2.ModuleSelected(CurPatNum);
					break;
				case 2:
					ContrAccount2.Visible=true;
					this.ActiveControl=this.ContrAccount2;
					ContrAccount2.ModuleSelected(CurPatNum);
					break;
				case 3:
					ContrTreat2.Visible=true;
					this.ActiveControl=this.ContrTreat2;
					ContrTreat2.ModuleSelected(CurPatNum);
					break;
				case 4:
					ContrChart2.Visible=true;
					this.ActiveControl=this.ContrChart2;
					ContrChart2.ModuleSelected(CurPatNum);
					break;
				case 5:
					ContrDocs2.Visible=true;
					this.ActiveControl=this.ContrDocs2;
					ContrDocs2.ModuleSelected(CurPatNum);
					break;
				case 6:
					ContrManage2.Visible=true;
					this.ActiveControl=this.ContrManage2;
					ContrManage2.ModuleSelected(CurPatNum);
					break;
			}
		}

		private void allNeutral(){
			ContrAppt2.Visible=false;
			ContrFamily2.Visible=false;
			ContrAccount2.Visible=false;
			ContrTreat2.Visible=false;
			ContrChart2.Visible=false;
			ContrDocs2.Visible=false;
			ContrManage2.Visible=false;
		}

		private void UnselectActive(){
			if(ContrAppt2.Visible)
				ContrAppt2.ModuleUnselected();
			if(ContrFamily2.Visible)
				ContrFamily2.ModuleUnselected();
			if(ContrAccount2.Visible)
				ContrAccount2.ModuleUnselected();
			if(ContrTreat2.Visible)
				ContrTreat2.ModuleUnselected();
			if(ContrChart2.Visible)
				ContrChart2.ModuleUnselected();
			if(ContrDocs2.Visible)
				ContrDocs2.ModuleUnselected();
		}

		///<Summary>This also passes CurPatNum down to the currently selected module (except the Manage module).</Summary>
		private void RefreshCurrentModule(){
			if(ContrAppt2.Visible)
				ContrAppt2.ModuleSelected(CurPatNum);
			if(ContrFamily2.Visible)
				ContrFamily2.ModuleSelected(CurPatNum);
			if(ContrAccount2.Visible)
				ContrAccount2.ModuleSelected(CurPatNum);
			if(ContrTreat2.Visible)
				ContrTreat2.ModuleSelected(CurPatNum);
			if(ContrChart2.Visible)
				ContrChart2.ModuleSelected(CurPatNum);
			if(ContrDocs2.Visible)
				ContrDocs2.ModuleSelected(CurPatNum);
			if(ContrManage2.Visible)
				ContrManage2.ModuleSelected(CurPatNum);
		}

		/// <summary>sends function key presses to the appointment module</summary>
		private void FormOpenDental_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(ContrAppt2.Visible && e.KeyCode>=Keys.F1 && e.KeyCode<=Keys.F12){
				ContrAppt2.FunctionKeyPress(e.KeyCode);
			}
			Keys keys=e.KeyCode;
			if((e.Modifiers&Keys.Alt)==Keys.Alt
				&& (e.Modifiers&Keys.Control)==Keys.Control
				&& (e.KeyCode&Keys.R)==Keys.R
				&& CurPatNum!=0)
			{
				FormReferralsPatient FormRE=new FormReferralsPatient();
				FormRE.PatNum=CurPatNum;
				FormRE.ShowDialog();
			}
		}

		private void timerTimeIndic_Tick(object sender, System.EventArgs e){
			if(WindowState!=FormWindowState.Minimized
				&& ContrAppt2.Visible){
				ContrAppt2.TickRefresh();
      }
		}

		private void timerSignals_Tick(object sender, System.EventArgs e) {
			ProcessSignals();
		}

		private void timerDisabledKey_Tick(object sender,EventArgs e) {
			if(PrefC.GetBoolSilent("RegistrationKeyIsDisabled",false)) {
				MessageBox.Show("Registration key has been disabled.  You are using an unauthorized version of this program.","Warning",
					MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
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
			if(userControlTasks1.GotoType==TaskObjectType.Patient) {
				if(userControlTasks1.GotoKeyNum!=0) {
					CurPatNum=userControlTasks1.GotoKeyNum;//OnPatientSelected(FormT.GotoKeyNum);
					//GotoModule.GotoAccount();
					Patient pat=Patients.GetPat(CurPatNum);
					RefreshCurrentModule();
					FillPatientButton(CurPatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber,pat.SiteNum);
				}
			}
			if(userControlTasks1.GotoType==TaskObjectType.Appointment) {
				if(userControlTasks1.GotoKeyNum!=0) {
					Appointment apt=Appointments.GetOneApt(userControlTasks1.GotoKeyNum);
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
			LastModule=myOutlookBar.SelectedIndex;
			myOutlookBar.SelectedIndex=-1;
			myOutlookBar.Invalidate();
			UnselectActive();
			allNeutral();
			FormLogOn FormL=new FormLogOn();
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.Cancel){
				Application.Exit();
				return;
			}
			myOutlookBar.SelectedIndex=Security.GetModule(LastModule);
			myOutlookBar.Invalidate();
			SetModuleSelected();
			if(CurPatNum==0){
				Text=Patients2.GetMainTitle("",0,"",0);
			}
			else{
				Patient pat=Patients.GetPat(CurPatNum);
				Text=Patients2.GetMainTitle(pat.GetNameLF(),pat.PatNum,pat.ChartNumber,pat.SiteNum);
			}
			if(userControlTasks1.Visible) {
				userControlTasks1.InitializeOnStartup();
			}
			if(myOutlookBar.SelectedIndex==-1) {
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
		}

		//File
		private void menuItemPrinter_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPrinterSetup FormPS=new FormPrinterSetup();
			FormPS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Printers");
		}

		private void menuItemConfig_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.ChooseDatabase)){
				return;
			}
			FormChooseDatabase FormC=new FormChooseDatabase();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.ChooseDatabase,0,"");
			if(FormC.DialogResult==DialogResult.Cancel){
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

		private void menuItemAnestheticMedications_Click(object sender, EventArgs e){
			if (!Security.IsAuthorized(Permissions.Setup))
			{
				return;
			}
			FormAnestheticMedsInventory FormAM = new FormAnestheticMedsInventory();
			FormAM.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup, 0, "Anesthetic Medications");
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

		private void menuItemApptRules_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormApptRules FormA=new FormApptRules();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Appointment Rules");
		}

		private void menuItemAutoCodes_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormAutoCode FormAC=new FormAutoCode();
			FormAC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Auto Codes");
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
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPath FormP=new FormPath();
			FormP.ShowDialog();
			ContrDocs2.Enabled=PrefC.UsingAtoZfolder;
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

		private void menuItemDiseases_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDiseaseDefs FormD=new FormDiseaseDefs();
			FormD.ShowDialog();
			//RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Disease Defs");
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

		private void menuItemEasy_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEasy FormE=new FormEasy();
			FormE.ShowDialog();
			ContrAccount2.LayoutToolBar();//for repeating charges
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Easy Options");
		}

		private void menuItemEmail_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEmailSetup FormE=new FormEmailSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Email");
		}

		private void menuItemFeeScheds_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormFeeScheds FormF=new FormFeeScheds();
			FormF.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Fee Schedules");
		}

		private void menuItemGraphics_Click(object sender,EventArgs e) {
			FormGraphics fg=new FormGraphics();
			if(fg.ShowDialog()==DialogResult.OK){
				ContrChart2.InitializeLocalData();
			}
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

		private void menuItemLaboratories_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormLaboratories FormL=new FormLaboratories();
			FormL.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Laboratories");
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
			menuItemMergeDatabases.Visible=PrefC.GetBool("RandomPrimaryKeys");
			if(timerSignals.Interval==0){
				timerSignals.Enabled=false;
			}
			else{
				timerSignals.Interval=PrefC.GetInt("ProcessSigsIntervalInSecs")*1000;
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

		private void menuItemPayPeriods_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormPayPeriods FormP=new FormPayPeriods();
			FormP.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Pay Periods");
		}

		private void menuItemPractice_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPractice FormPr=new FormPractice();
			FormPr.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Practice Info");
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
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Program Links");
		}

		private void menuItem_ProviderAllocatorSetup_Click(object sender, EventArgs e){
			// Check Permissions
			if (!Security.IsAuthorized(Permissions.Setup))
			{
				// Failed security prompts message box. Consider adding overload to not show message.
				//MessageBox.Show("Not Authorized to Run Setup for Provider Allocation Tool");
				return;
			}
			Reporting.Allocators.MyAllocator1.FormInstallAllocator_Provider fap = new OpenDental.Reporting.Allocators.MyAllocator1.FormInstallAllocator_Provider();
			fap.ShowDialog();
		}

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
				PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(pat.PatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber);
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
			if(!Security.IsAuthorized(Permissions.Setup)){
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
		}

		//Custom Reports
		private void menuItemRDLReport_Click(object sender,System.EventArgs e) {
			//This point in the code is only reached if the A to Z folders are enabled, thus
			//the image path should exist.
			FormReportCustom FormR=new FormReportCustom();
			FormR.SourceFilePath=ODFileUtils.CombinePaths(new string[]
				{FormPath.GetPreferredImagePath(),PrefC.GetString("ReportFolderName"),((MenuItem)sender).Text+".rdl"}
				);
			FormR.ShowDialog();
		}

		//Tools
		private void menuItemPrintScreen_Click(object sender, System.EventArgs e) {
			FormPrntScrn FormPS=new FormPrntScrn();
			FormPS.ShowDialog();
		}

		private void menuItemDatabaseMaintenance_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDatabaseMaintenance FormDM=new FormDatabaseMaintenance();
			FormDM.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Database Maintenance");
		}

		private void menuTelephone_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormTelephone FormT=new FormTelephone();
			FormT.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Telephone");
		}

		private void menuItemPatientImport_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)){
				return;
			}
			FormImport FormI=new FormImport();
			FormI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,0,"Patient Import Tool");
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

		private void menuItemReallocate_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormAllocate FormA=new FormAllocate();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Reallocated Family Balances");
			}
		}

		/*
		private void menuItemPaymentPlans_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPayPlanUpdate FormPPU=new FormPayPlanUpdate();
			FormPPU.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Payment Plan Update");
		}*/

		private void menuItemAuditTrail_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)) {
				return;
			}
			FormAudit FormA=new FormAudit();
			FormA.CurPatNum=CurPatNum;
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,0,"Audit Trail");
		}

		private void menuItemImportXML_Click(object sender, System.EventArgs e) {
			FormImportXML FormI=new FormImportXML();
			FormI.ShowDialog();
		}

		private void menuItemAging_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormAging FormAge=new FormAging();
			FormAge.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Aging Update");
		}

		private void menuItemFinanceCharge_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormFinanceCharges FormFC=new FormFinanceCharges();
			FormFC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Run Finance Charges");
		}

		private void menuItemRepeatingCharges_Click(object sender, System.EventArgs e) {
			FormRepeatChargesUpdate FormR=new FormRepeatChargesUpdate();
			FormR.ShowDialog();
		}

		private void menuItemTerminal_Click(object sender,EventArgs e) {
			FormTerminal FormT=new FormTerminal();
			FormT.ShowDialog(); 
			Application.Exit();//always close after coming out of terminal mode as a safety precaution.
		}

		private void menuItemTerminalManager_Click(object sender,EventArgs e) {
			FormTerminalManager FormT=new FormTerminalManager();
			FormT.Show();
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

		private void menuItemMergeDatabases_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormReplication FormR=new FormReplication();
			FormR.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Replication");
		}

		private void menuItemTranslation_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormTranslationCat FormTC=new FormTranslationCat();
			FormTC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Translations");
		}

		private void menuItemScreening_Click(object sender, System.EventArgs e) {
			FormScreenings FormS=new FormScreenings();
			FormS.ShowDialog();
		}

		private void menuItemCustomerManage_Click(object sender,EventArgs e) {
			FormCustomerManagement FormC=new FormCustomerManagement();
			FormC.ShowDialog();
			if(FormC.SelectedPatNum!=0) {
				CurPatNum=FormC.SelectedPatNum;
				Patient pat=Patients.GetPat(CurPatNum);
				RefreshCurrentModule();
				FillPatientButton(CurPatNum,pat.GetNameLF(),pat.Email!="",pat.ChartNumber,pat.SiteNum);
			}
		}

		//Help
		private void menuItemRemote_Click(object sender,System.EventArgs e) {
			try {
				Process.Start("http://www.open-dent.com/contact.html");
			}
			catch {
				MessageBox.Show(Lan.g(this,"Could not find")+" http://www.open-dent.com/contact.html");
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
				Process.Start("http://www.open-dent.com/manual/toc.html");
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
			}
		}

		private void menuItemHelpIndex_Click(object sender, System.EventArgs e) {
			try{
				Process.Start(@"http://www.open-dent.com/manual/alphabetical.html");
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

		private void FormOpenDental_Closing(object sender,System.ComponentModel.CancelEventArgs e) {
			FormUAppoint.AbortThread();
		}

		private void OnPatientCardInserted(object sender, PatientCardInsertedEventArgs e) {
			if (InvokeRequired) {
				Invoke(new PatientCardInsertedEventHandler(OnPatientCardInserted), new object[] { sender, e });
				return;
			}
			if (MessageBox.Show(this, string.Format(Lan.g(this, "A card belonging to {0} has been inserted. Do you wish to search for this patient now?"), e.Patient.GetNameFL()), "Open Dental", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			using (FormPatientSelect formPS = new FormPatientSelect()) {
				formPS.PreselectPatient(e.Patient);
				if(formPS.ShowDialog() == DialogResult.OK) {
					// OnPatientSelected(formPS.SelectedPatNum);
					// ModuleSelected(formPS.SelectedPatNum);
				}
			}
		}

	

		

		

		

		

		

		


		

		

	

		

		

		

		
	}

	class PopupEvent:IComparable{
		public int PatNum;
		///<summary>Disable popup for this patient until this time.</summary>
		public DateTime DisableUntil;

		public int CompareTo(object obj) {
			PopupEvent pop=(PopupEvent)obj;
			return DisableUntil.CompareTo(pop.DisableUntil);
		}

		public override string ToString() {
			return PatNum.ToString()+", "+DisableUntil.ToString();
		}
	}
}
