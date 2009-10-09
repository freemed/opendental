/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRecallList : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;
		//private ArrayList MainAL;
		//<summary>Will be set to true when form closes if user click Send to Pinboard.</summary>
		//public bool PinClicked=false;
		private OpenDental.UI.Button butReport;
		private int pagesPrinted;
		private DataTable AddrTable;
		private int patientsPrinted;
		private OpenDental.UI.PrintPreview printPreview;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.UI.Button butSetStatus;
		private System.Windows.Forms.ComboBox comboStatus;
		private OpenDental.UI.Button butLabels;
		private OpenDental.UI.Button butPostcards;
		private PrintDocument pd;
		private OpenDental.UI.ODGrid gridMain;
		private ValidDate textDateEnd;
		private ValidDate textDateStart;
		private CheckBox checkGroupFamilies;
		///<summary>This is the patNum of the current (or last) patient selected.  The calling form should then make use of this to refresh to that patient.  If 0, then calling form should not refresh.</summary>
		public long SelectedPatNum;
		private OpenDental.UI.Button butPrint;
		DataTable table;
		private bool headingPrinted;
		private int headingPrintH;
		private ComboBox comboProv;
		private Label label4;
		private ComboBox comboClinic;
		private Label labelClinic;
		private OpenDental.UI.Button butSchedPat;
		private OpenDental.UI.Button butSchedFam;
		private ComboBox comboSite;
		private Label labelSite;
		private OpenDental.UI.Button butEmail;
		private Label labelPatientCount;
		private ComboBox comboSort;
		private Label label5;
		private OpenDental.UI.Button butLabelOne;
		private ComboBox comboNumberReminders;
		private Label label3;
		private ContextMenu menuRightClick;
		private OpenDental.UI.Button butGotoAccount;
		private OpenDental.UI.Button butCommlog;
		private MenuItem menuItemSeeFamily;
		private OpenDental.UI.Button butGotoFamily;
		private MenuItem menuItemSeeAccount;
		//<summary>Only used if PinClicked=true</summary>
		//public List<long> AptNumsSelected;

		///<summary></summary>
		public FormRecallList(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
			//Lan.C(this,new Control[]
			//	{
			//		textBox1
			//	});
			gridMain.ContextMenu=menuRightClick;
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

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecallList));
			this.labelClinic = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboNumberReminders = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboSort = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboSite = new System.Windows.Forms.ComboBox();
			this.labelSite = new System.Windows.Forms.Label();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkGroupFamilies = new System.Windows.Forms.CheckBox();
			this.textDateEnd = new OpenDental.ValidDate();
			this.textDateStart = new OpenDental.ValidDate();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butReport = new OpenDental.UI.Button();
			this.butLabels = new OpenDental.UI.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.butSetStatus = new OpenDental.UI.Button();
			this.butPostcards = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butPrint = new OpenDental.UI.Button();
			this.butSchedPat = new OpenDental.UI.Button();
			this.butSchedFam = new OpenDental.UI.Button();
			this.butEmail = new OpenDental.UI.Button();
			this.labelPatientCount = new System.Windows.Forms.Label();
			this.butLabelOne = new OpenDental.UI.Button();
			this.menuRightClick = new System.Windows.Forms.ContextMenu();
			this.menuItemSeeAccount = new System.Windows.Forms.MenuItem();
			this.menuItemSeeFamily = new System.Windows.Forms.MenuItem();
			this.butGotoAccount = new OpenDental.UI.Button();
			this.butCommlog = new OpenDental.UI.Button();
			this.butGotoFamily = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(386,37);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(70,14);
			this.labelClinic.TabIndex = 22;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(896,663);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(255,55);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(98,24);
			this.butRefresh.TabIndex = 2;
			this.butRefresh.Text = "&Refresh List";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.comboNumberReminders);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.comboSort);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.comboSite);
			this.groupBox1.Controls.Add(this.labelSite);
			this.groupBox1.Controls.Add(this.comboClinic);
			this.groupBox1.Controls.Add(this.labelClinic);
			this.groupBox1.Controls.Add(this.comboProv);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.checkGroupFamilies);
			this.groupBox1.Controls.Add(this.textDateEnd);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(6,2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(641,83);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// comboNumberReminders
			// 
			this.comboNumberReminders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboNumberReminders.Location = new System.Drawing.Point(103,57);
			this.comboNumberReminders.MaxDropDownItems = 40;
			this.comboNumberReminders.Name = "comboNumberReminders";
			this.comboNumberReminders.Size = new System.Drawing.Size(82,21);
			this.comboNumberReminders.TabIndex = 39;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3,60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99,14);
			this.label3.TabIndex = 38;
			this.label3.Text = "Show Reminders";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSort
			// 
			this.comboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSort.Location = new System.Drawing.Point(67,32);
			this.comboSort.MaxDropDownItems = 40;
			this.comboSort.Name = "comboSort";
			this.comboSort.Size = new System.Drawing.Size(118,21);
			this.comboSort.TabIndex = 37;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(11,35);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55,14);
			this.label5.TabIndex = 36;
			this.label5.Text = "Sort";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSite
			// 
			this.comboSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSite.Location = new System.Drawing.Point(458,57);
			this.comboSite.MaxDropDownItems = 40;
			this.comboSite.Name = "comboSite";
			this.comboSite.Size = new System.Drawing.Size(160,21);
			this.comboSite.TabIndex = 25;
			// 
			// labelSite
			// 
			this.labelSite.Location = new System.Drawing.Point(386,60);
			this.labelSite.Name = "labelSite";
			this.labelSite.Size = new System.Drawing.Size(70,14);
			this.labelSite.TabIndex = 24;
			this.labelSite.Text = "Site";
			this.labelSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(458,34);
			this.comboClinic.MaxDropDownItems = 40;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(160,21);
			this.comboClinic.TabIndex = 23;
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(458,11);
			this.comboProv.MaxDropDownItems = 40;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(160,21);
			this.comboProv.TabIndex = 21;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(386,14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70,14);
			this.label4.TabIndex = 20;
			this.label4.Text = "Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkGroupFamilies
			// 
			this.checkGroupFamilies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.Location = new System.Drawing.Point(77,12);
			this.checkGroupFamilies.Name = "checkGroupFamilies";
			this.checkGroupFamilies.Size = new System.Drawing.Size(108,18);
			this.checkGroupFamilies.TabIndex = 19;
			this.checkGroupFamilies.Text = "Group Families";
			this.checkGroupFamilies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.UseVisualStyleBackColor = true;
			this.checkGroupFamilies.Click += new System.EventHandler(this.checkGroupFamilies_Click);
			// 
			// textDateEnd
			// 
			this.textDateEnd.Location = new System.Drawing.Point(276,34);
			this.textDateEnd.Name = "textDateEnd";
			this.textDateEnd.Size = new System.Drawing.Size(77,20);
			this.textDateEnd.TabIndex = 18;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(276,13);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(77,20);
			this.textDateStart.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(191,37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,14);
			this.label2.TabIndex = 12;
			this.label2.Text = "End Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(191,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82,14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Start Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butReport
			// 
			this.butReport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butReport.Autosize = true;
			this.butReport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butReport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butReport.CornerRadius = 4F;
			this.butReport.Location = new System.Drawing.Point(353,637);
			this.butReport.Name = "butReport";
			this.butReport.Size = new System.Drawing.Size(87,24);
			this.butReport.TabIndex = 13;
			this.butReport.Text = "R&un Report";
			this.butReport.Click += new System.EventHandler(this.butReport_Click);
			// 
			// butLabels
			// 
			this.butLabels.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLabels.Autosize = true;
			this.butLabels.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabels.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabels.CornerRadius = 4F;
			this.butLabels.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabels.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabels.Location = new System.Drawing.Point(131,663);
			this.butLabels.Name = "butLabels";
			this.butLabels.Size = new System.Drawing.Size(119,24);
			this.butLabels.TabIndex = 14;
			this.butLabels.Text = "Label Preview";
			this.butLabels.Click += new System.EventHandler(this.butLabels_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboStatus);
			this.groupBox3.Controls.Add(this.butSetStatus);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(654,2);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(188,83);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Set Status";
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(17,19);
			this.comboStatus.MaxDropDownItems = 40;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(160,21);
			this.comboStatus.TabIndex = 15;
			// 
			// butSetStatus
			// 
			this.butSetStatus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSetStatus.Autosize = true;
			this.butSetStatus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetStatus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetStatus.CornerRadius = 4F;
			this.butSetStatus.Location = new System.Drawing.Point(110,47);
			this.butSetStatus.Name = "butSetStatus";
			this.butSetStatus.Size = new System.Drawing.Size(67,24);
			this.butSetStatus.TabIndex = 14;
			this.butSetStatus.Text = "Set";
			this.butSetStatus.Click += new System.EventHandler(this.butSetStatus_Click);
			// 
			// butPostcards
			// 
			this.butPostcards.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPostcards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPostcards.Autosize = true;
			this.butPostcards.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPostcards.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPostcards.CornerRadius = 4F;
			this.butPostcards.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPostcards.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPostcards.Location = new System.Drawing.Point(6,663);
			this.butPostcards.Name = "butPostcards";
			this.butPostcards.Size = new System.Drawing.Size(119,24);
			this.butPostcards.TabIndex = 16;
			this.butPostcards.Text = "Postcard Preview";
			this.butPostcards.Click += new System.EventHandler(this.butPostcards_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(6,88);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(965,544);
			this.gridMain.TabIndex = 18;
			this.gridMain.Title = "Recall List";
			this.gridMain.TranslationName = "TableRecallList";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(353,663);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87,24);
			this.butPrint.TabIndex = 19;
			this.butPrint.Text = "Print List";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butSchedPat
			// 
			this.butSchedPat.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSchedPat.Autosize = true;
			this.butSchedPat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSchedPat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSchedPat.CornerRadius = 4F;
			this.butSchedPat.Image = global::OpenDental.Properties.Resources.butPin;
			this.butSchedPat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSchedPat.Location = new System.Drawing.Point(642,637);
			this.butSchedPat.Name = "butSchedPat";
			this.butSchedPat.Size = new System.Drawing.Size(114,24);
			this.butSchedPat.TabIndex = 58;
			this.butSchedPat.Text = "Sched Patient";
			this.butSchedPat.Click += new System.EventHandler(this.butSchedPat_Click);
			// 
			// butSchedFam
			// 
			this.butSchedFam.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSchedFam.Autosize = true;
			this.butSchedFam.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSchedFam.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSchedFam.CornerRadius = 4F;
			this.butSchedFam.Image = global::OpenDental.Properties.Resources.butPin;
			this.butSchedFam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSchedFam.Location = new System.Drawing.Point(642,663);
			this.butSchedFam.Name = "butSchedFam";
			this.butSchedFam.Size = new System.Drawing.Size(114,24);
			this.butSchedFam.TabIndex = 59;
			this.butSchedFam.Text = "Sched Family";
			this.butSchedFam.Click += new System.EventHandler(this.butSchedFam_Click);
			// 
			// butEmail
			// 
			this.butEmail.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEmail.Autosize = true;
			this.butEmail.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEmail.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEmail.CornerRadius = 4F;
			this.butEmail.Image = global::OpenDental.Properties.Resources.email1;
			this.butEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEmail.Location = new System.Drawing.Point(256,663);
			this.butEmail.Name = "butEmail";
			this.butEmail.Size = new System.Drawing.Size(91,24);
			this.butEmail.TabIndex = 60;
			this.butEmail.Text = "E-Mail";
			this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
			// 
			// labelPatientCount
			// 
			this.labelPatientCount.Location = new System.Drawing.Point(759,669);
			this.labelPatientCount.Name = "labelPatientCount";
			this.labelPatientCount.Size = new System.Drawing.Size(114,14);
			this.labelPatientCount.TabIndex = 61;
			this.labelPatientCount.Text = "Patient Count:";
			this.labelPatientCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butLabelOne
			// 
			this.butLabelOne.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLabelOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butLabelOne.Autosize = true;
			this.butLabelOne.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLabelOne.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLabelOne.CornerRadius = 4F;
			this.butLabelOne.Image = global::OpenDental.Properties.Resources.butLabel;
			this.butLabelOne.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butLabelOne.Location = new System.Drawing.Point(131,637);
			this.butLabelOne.Name = "butLabelOne";
			this.butLabelOne.Size = new System.Drawing.Size(119,24);
			this.butLabelOne.TabIndex = 63;
			this.butLabelOne.Text = "Single Labels";
			this.butLabelOne.Click += new System.EventHandler(this.butLabelOne_Click);
			// 
			// menuRightClick
			// 
			this.menuRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSeeFamily,
            this.menuItemSeeAccount});
			// 
			// menuItemSeeAccount
			// 
			this.menuItemSeeAccount.Index = 1;
			this.menuItemSeeAccount.Text = "See Account";
			this.menuItemSeeAccount.Click += new System.EventHandler(this.menuItemSeeAccount_Click);
			// 
			// menuItemSeeFamily
			// 
			this.menuItemSeeFamily.Index = 0;
			this.menuItemSeeFamily.Text = "See Family";
			this.menuItemSeeFamily.Click += new System.EventHandler(this.menuItemSeeFamily_Click);
			// 
			// butGotoAccount
			// 
			this.butGotoAccount.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGotoAccount.Autosize = true;
			this.butGotoAccount.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGotoAccount.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGotoAccount.CornerRadius = 4F;
			this.butGotoAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butGotoAccount.Location = new System.Drawing.Point(446,663);
			this.butGotoAccount.Name = "butGotoAccount";
			this.butGotoAccount.Size = new System.Drawing.Size(96,24);
			this.butGotoAccount.TabIndex = 64;
			this.butGotoAccount.Text = "Go to Account";
			this.butGotoAccount.Click += new System.EventHandler(this.butGotoAccount_Click);
			// 
			// butCommlog
			// 
			this.butCommlog.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCommlog.Autosize = true;
			this.butCommlog.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCommlog.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCommlog.CornerRadius = 4F;
			this.butCommlog.Image = global::OpenDental.Properties.Resources.commlog;
			this.butCommlog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCommlog.Location = new System.Drawing.Point(548,663);
			this.butCommlog.Name = "butCommlog";
			this.butCommlog.Size = new System.Drawing.Size(88,24);
			this.butCommlog.TabIndex = 65;
			this.butCommlog.Text = "Comm";
			this.butCommlog.Click += new System.EventHandler(this.butCommlog_Click);
			// 
			// butGotoFamily
			// 
			this.butGotoFamily.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butGotoFamily.Autosize = true;
			this.butGotoFamily.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butGotoFamily.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butGotoFamily.CornerRadius = 4F;
			this.butGotoFamily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butGotoFamily.Location = new System.Drawing.Point(446,637);
			this.butGotoFamily.Name = "butGotoFamily";
			this.butGotoFamily.Size = new System.Drawing.Size(96,24);
			this.butGotoFamily.TabIndex = 66;
			this.butGotoFamily.Text = "Go to Family";
			this.butGotoFamily.Click += new System.EventHandler(this.butGotoFamily_Click);
			// 
			// FormRecallList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(975,691);
			this.Controls.Add(this.butGotoFamily);
			this.Controls.Add(this.butCommlog);
			this.Controls.Add(this.butGotoAccount);
			this.Controls.Add(this.butSchedFam);
			this.Controls.Add(this.butLabelOne);
			this.Controls.Add(this.butSchedPat);
			this.Controls.Add(this.labelPatientCount);
			this.Controls.Add(this.butEmail);
			this.Controls.Add(this.butPostcards);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.butLabels);
			this.Controls.Add(this.butReport);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormRecallList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recall List";
			this.Load += new System.EventHandler(this.FormRecallList_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRecallList_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecallList_Load(object sender, System.EventArgs e) {
			//AptNumsSelected=new List<long>();
			checkGroupFamilies.Checked=PrefC.GetBool(PrefName.RecallGroupByFamily);
			comboSort.Items.Add(Lan.g(this,"Due Date"));
			comboSort.Items.Add(Lan.g(this,"Alphabetical"));
			comboSort.SelectedIndex=0;
			comboNumberReminders.Items.Add(Lan.g(this,"all"));
			comboNumberReminders.Items.Add("0");
			comboNumberReminders.Items.Add("1");
			comboNumberReminders.Items.Add("2");
			comboNumberReminders.Items.Add("3");
			comboNumberReminders.Items.Add("4");
			comboNumberReminders.Items.Add("5");
			comboNumberReminders.Items.Add("6+");
			comboNumberReminders.SelectedIndex=0;
			int daysPast=PrefC.GetInt(PrefName.RecallDaysPast);
			int daysFuture=PrefC.GetInt(PrefName.RecallDaysFuture);
			if(daysPast==-1){
				textDateStart.Text="";
			}
			else{
				textDateStart.Text=DateTime.Today.AddDays(-daysPast).ToShortDateString();
			}
			if(daysFuture==-1) {
				textDateEnd.Text="";
			}
			else {
				textDateEnd.Text=DateTime.Today.AddDays(daysFuture).ToShortDateString();
			}
			comboProv.Items.Add(Lan.g(this,"All"));
			comboProv.SelectedIndex=0;
			for(int i=0;i<ProviderC.List.Length;i++) {
				comboProv.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)){
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			else{
				comboClinic.Items.Add(Lan.g(this,"All"));
				comboClinic.SelectedIndex=0;
				for(int i=0;i<Clinics.List.Length;i++) {
					comboClinic.Items.Add(Clinics.List[i].Description);
				}
			}
			if(PrefC.GetBool(PrefName.EasyHidePublicHealth)){
				comboSite.Visible=false;
				labelSite.Visible=false;
			}
			else{
				comboSite.Items.Add(Lan.g(this,"All"));
				comboSite.SelectedIndex=0;
				for(int i=0;i<SiteC.List.Length;i++) {
					comboSite.Items.Add(SiteC.List[i].Description);
				}
			}
			comboStatus.Items.Clear();
			comboStatus.Items.Add(Lan.g(this,"none"));
			comboStatus.SelectedIndex=0;
			for(int i=0;i<DefC.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboStatus.Items.Add(DefC.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
			}
			FillMain(null);
		}

		///<summary>OK to pass in null for excludePatNums.</summary>
		private void FillMain(List<long> excludePatNums){
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				return;
			}
			//remember which recallnums were selected
			List<string> recallNums=new List<string>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				recallNums.Add(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString());
			}
			DateTime fromDate;
			DateTime toDate;
			if(textDateStart.Text==""){
				fromDate=DateTime.MinValue.AddDays(1);//because we don't want to include 010101
			}
			else{
				fromDate=PIn.PDate(textDateStart.Text);
			}
			if(textDateEnd.Text=="") {
				toDate=DateTime.MaxValue;
			}
			else {
				toDate=PIn.PDate(textDateEnd.Text);
			}
			long provNum=0;
			if(comboProv.SelectedIndex!=0){
				provNum=ProviderC.List[comboProv.SelectedIndex-1].ProvNum;
			}
			long clinicNum=0;
			if(!PrefC.GetBool(PrefName.EasyNoClinics) && comboClinic.SelectedIndex!=0) {
				clinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			long siteNum=0;
			if(!PrefC.GetBool(PrefName.EasyHidePublicHealth) && comboSite.SelectedIndex!=0) {
				siteNum=SiteC.List[comboSite.SelectedIndex-1].SiteNum;
			}
			bool sortAlph=false;
			if(comboSort.SelectedIndex==1){
				sortAlph=true;
			}
			RecallListShowNumberReminders showReminders=(RecallListShowNumberReminders)comboNumberReminders.SelectedIndex;
			if(excludePatNums==null) {
				excludePatNums=new List<long>();
			}
			table=Recalls.GetRecallList(fromDate,toDate,checkGroupFamilies.Checked,provNum,clinicNum,siteNum,sortAlph,showReminders,excludePatNums);
			int scrollval=gridMain.ScrollValue;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecallList","Due Date"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Patient"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Age"),30);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Type"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Interval"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","#Remind"),55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","LastRemind"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Contact"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Status"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallList","Note"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["dueDate"].ToString());
				row.Cells.Add(table.Rows[i]["patientName"].ToString());
				row.Cells.Add(table.Rows[i]["age"].ToString());
				row.Cells.Add(table.Rows[i]["recallType"].ToString());
				row.Cells.Add(table.Rows[i]["recallInterval"].ToString());
				row.Cells.Add(table.Rows[i]["numberOfReminders"].ToString());
				row.Cells.Add(table.Rows[i]["dateLastReminder"].ToString());
				row.Cells.Add(table.Rows[i]["contactMethod"].ToString());
				row.Cells.Add(table.Rows[i]["status"].ToString());
				row.Cells.Add(table.Rows[i]["Note"].ToString());
				row.Tag=table.Rows[i];//although not used yet.
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			//reselect original items
			for(int i=0;i<table.Rows.Count;i++){
				if(recallNums.Contains(table.Rows[i]["RecallNum"].ToString())){
					gridMain.SetSelected(i,true);
				}
			}
			labelPatientCount.Text=Lan.g(this,"Patient Count:")+" "+table.Rows.Count.ToString();
		}

		private void gridMain_CellClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			//row selected before this event triggered
			SetFamilyColors();
			//comboStatus.SelectedIndex=-1;//mess with this later
			//SelectedPatNum=PIn.PLong(table.Rows[e.Row]["PatNum"].ToString());
		}

		private void SetFamilyColors() {
			if(gridMain.SelectedIndices.Length!=1) {
				for(int i=0;i<gridMain.Rows.Count;i++) {
					gridMain.Rows[i].ColorText=Color.Black;
				}
				gridMain.Invalidate();
				return;
			}
			long guar=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["Guarantor"].ToString());
			int famCount=0;
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(PIn.PLong(table.Rows[i]["Guarantor"].ToString())==guar){
					famCount++;
					gridMain.Rows[i].ColorText=Color.Red;
				}
				else {
					gridMain.Rows[i].ColorText=Color.Black;
				}
			}
			if(famCount==1) {//only the highlighted patient is red at this point
				gridMain.Rows[gridMain.SelectedIndices[0]].ColorText=Color.Black;
			}
			gridMain.Invalidate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SelectedPatNum=PIn.PLong(table.Rows[e.Row]["PatNum"].ToString());
			Recall recall=Recalls.GetRecall(PIn.PLong(table.Rows[e.Row]["RecallNum"].ToString()));
			FormRecallEdit FormR=new FormRecallEdit();
			FormR.RecallCur=recall.Copy();
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK) {
				return;
			}
			if(recall.RecallStatus!=FormR.RecallCur.RecallStatus//if the status has changed
				|| (recall.IsDisabled != FormR.RecallCur.IsDisabled)//or any of the three disabled fields was changed
				|| (recall.DisableUntilDate != FormR.RecallCur.DisableUntilDate)
				|| (recall.DisableUntilBalance != FormR.RecallCur.DisableUntilBalance)
				|| (recall.Note != FormR.RecallCur.Note))//or a note was added
			{
				//make a commlog entry
				//unless there is an existing recall commlog entry for today
				bool recallEntryToday=false;
				Commlog[] CommlogList=Commlogs.Refresh(SelectedPatNum);
				for(int i=0;i<CommlogList.Length;i++) {
					if(CommlogList[i].CommDateTime.Date==DateTime.Today	
						&& CommlogList[i].CommType==Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL)) {
						recallEntryToday=true;
					}
				}
				if(!recallEntryToday) {
					Commlog CommlogCur=new Commlog();
					CommlogCur.CommDateTime=DateTime.Now;
					CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
					CommlogCur.PatNum=SelectedPatNum;
					CommlogCur.Note="";
					if(recall.RecallStatus!=FormR.RecallCur.RecallStatus) {
						if(FormR.RecallCur.RecallStatus==0) {
							CommlogCur.Note+=Lan.g(this,"Status None");
						}
						else {
							CommlogCur.Note+=DefC.GetName(DefCat.RecallUnschedStatus,FormR.RecallCur.RecallStatus);
						}
					}
					if(recall.DisableUntilDate!=FormR.RecallCur.DisableUntilDate && FormR.RecallCur.DisableUntilDate.Year>1880) {
						if(CommlogCur.Note!="") {
							CommlogCur.Note+=",  ";
						}
						CommlogCur.Note+=Lan.g(this,"Disabled until ")+FormR.RecallCur.DisableUntilDate.ToShortDateString();
					}
					if(recall.DisableUntilBalance!=FormR.RecallCur.DisableUntilBalance && FormR.RecallCur.DisableUntilBalance>0) {
						if(CommlogCur.Note!="") {
							CommlogCur.Note+=",  ";
						}
						CommlogCur.Note+=Lan.g(this,"Disabled until balance below ")+FormR.RecallCur.DisableUntilBalance.ToString("c");
					}
					if(recall.Note!=FormR.RecallCur.Note) {
						if(CommlogCur.Note!="") {
							CommlogCur.Note+=",  ";
						}
						CommlogCur.Note+=FormR.RecallCur.Note;
					}
					CommlogCur.Note+=".  ";
					CommlogCur.UserNum=Security.CurUser.UserNum;
					FormCommItem FormCI=new FormCommItem(CommlogCur);
					FormCI.IsNew=true;
					//forces user to at least consider a commlog entry
					FormCI.ShowDialog();//typically saved in this window.
				}
			}
			FillMain(null);
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(PIn.PLong(table.Rows[i]["PatNum"].ToString())==SelectedPatNum){
					gridMain.SetSelected(i,true);
				}
			}
			SetFamilyColors();
		}

		private void butSchedPat_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			if(gridMain.SelectedIndices.Length>1) {
				MsgBox.Show(this,"Please select only one patient first.");
				return;
			}
			SelectedPatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			Family fam=Patients.GetFamily(SelectedPatNum);
			Patient pat=fam.GetPatient(SelectedPatNum);
			List<Procedure> procList;
			//List<Recall> recallList;
			List <InsPlan> planList;
			Appointment apt=null;
			//for(int i=0;i<fam.List.Length;i++) {
			procList=Procedures.Refresh(pat.PatNum);
			//List<int> patNums=new List<int>();
			//patNums.Add(pat.PatNum);
			//recallList=Recalls.GetList(patNums);//get the recall for this pt
			//if(recallList.Count==0) {
			//	MsgBox.Show(this,"This patient does not have any recall due.");
			//	return;
			//}
			planList=InsPlans.Refresh(fam);
			try{
				apt=AppointmentL.CreateRecallApt(pat,procList,planList);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			//PinClicked=true;
			//AptNumsSelected.Add(apt.AptNum);
			WindowState=FormWindowState.Minimized;
			List<long> pinAptNums=new List<long>();
			pinAptNums.Add(apt.AptNum);
			GotoModule.PinToAppt(pinAptNums,SelectedPatNum);
			gridMain.SetSelected(false);
			List<long> excludePatNums=new List<long>();
			excludePatNums.Add(SelectedPatNum);
			FillMain(excludePatNums);
		}

		private void butSchedFam_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			if(gridMain.SelectedIndices.Length>1) {
				MsgBox.Show(this,"Please select only one patient first.");
				return;
			}
			SelectedPatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			Family fam=Patients.GetFamily(SelectedPatNum);
			List<Procedure> procList;
			List <InsPlan> planList;
			Appointment apt;
			//List<long> patNums;
			List<long> pinAptNums=new List<long>();
			List<long> excludePatNums=new List<long>();
			for(int i=0;i<fam.ListPats.Length;i++) {
				procList=Procedures.Refresh(fam.ListPats[i].PatNum);
				//patNums=new List<long>();
				//patNums.Add(fam.ListPats[i].PatNum);
				planList=InsPlans.Refresh(fam);
				try{
					apt=AppointmentL.CreateRecallApt(fam.ListPats[i],procList,planList);
				}
				catch{//(Exception ex){
					continue;
				}
				pinAptNums.Add(apt.AptNum);
				excludePatNums.Add(apt.PatNum);
			}
			if(pinAptNums.Count==0) {
				MsgBox.Show(this,"No recall is due.");
				return;
			}
			WindowState=FormWindowState.Minimized;
			GotoModule.PinToAppt(pinAptNums,SelectedPatNum);
			gridMain.SetSelected(false);
			FillMain(excludePatNums);
		}

		private void checkGroupFamilies_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			FillMain(null);
			Cursor=Cursors.Default;
		}

		private void butReport_Click(object sender, System.EventArgs e) {
		  if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to run report."));    
        return;
      }
			List<long> recallNums=new List<long>();
      if(gridMain.SelectedIndices.Length < 1){
        for(int i=0;i<gridMain.Rows.Count;i++){
          recallNums.Add(PIn.PLong(table.Rows[i]["RecallNum"].ToString()));
        }
      }
      else{
        for(int i=0;i<gridMain.SelectedIndices.Length;i++){
          recallNums.Add(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()));
        }
      }
      FormRpRecall FormRPR=new FormRpRecall(recallNums);
      FormRPR.ShowDialog();      
		}

		private void butLabels_Click(object sender, System.EventArgs e) {
			if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to print."));    
        return;
      }
			if(PrefC.GetLong(PrefName.RecallStatusMailed)==0){
				MsgBox.Show(this,"You need to set a status first in the Recall Setup window.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				ContactMethod cmeth;
				for(int i=0;i<table.Rows.Count;i++){
					if(table.Rows[i]["status"].ToString()!=""){//we only want rows without a status
						continue;
					}
					cmeth=(ContactMethod)PIn.PLong(table.Rows[i]["PreferRecallMethod"].ToString());
					if(cmeth!=ContactMethod.Mail && cmeth!=ContactMethod.None){
						continue;
					}
					gridMain.SetSelected(i,true);
				}
				if(gridMain.SelectedIndices.Length==0){
					MsgBox.Show(this,"No patients of mail type.");
					return;
				}
				if(!MsgBox.Show(this,true,"Preview labels for all of the selected patients?")) {
					return;
				}
			}
			List<long> recallNums=new List<long>();
      for(int i=0;i<gridMain.SelectedIndices.Length;i++){
        recallNums.Add(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()));
      }
			bool sortAlph=false;
			if(comboSort.SelectedIndex==1){
				sortAlph=true;
			}
			AddrTable=Recalls.GetAddrTable(recallNums,checkGroupFamilies.Checked,sortAlph);
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdLabels_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.LabelSheet
				,pd,(int)Math.Ceiling((double)AddrTable.Rows.Count/30));
			//printPreview.Document=pd;
			//printPreview.TotalPages=;
			printPreview.ShowDialog();
			if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Change statuses and make commlog entries for all of the selected patients?")) {
				Cursor=Cursors.WaitCursor;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
					//make commlog entries for each patient
					Commlogs.InsertForRecall(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString()),CommItemMode.Mail,
						PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["numberOfReminders"].ToString()),PrefC.GetLong(PrefName.RecallStatusMailed));
				}
				for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
					Recalls.UpdateStatus(
						PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()),PrefC.GetLong(PrefName.RecallStatusMailed));
				}
			}
			FillMain(null);
			Cursor=Cursors.Default;
		}

		private void butLabelOne_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select patient(s) first.");
				return;
			}
			List<long> recallNums=new List<long>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				recallNums.Add(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()));
			}
			bool sortAlph=false;
			if(comboSort.SelectedIndex==1) {
				sortAlph=true;
			}
			AddrTable=Recalls.GetAddrTable(recallNums,checkGroupFamilies.Checked,sortAlph);
			patientsPrinted=0;
			string text;
			while(patientsPrinted<AddrTable.Rows.Count) {
				text="";
				if(checkGroupFamilies.Checked && AddrTable.Rows[patientsPrinted]["famList"].ToString()!="") {//print family label
					text=AddrTable.Rows[patientsPrinted]["guarLName"].ToString()+" "+Lan.g(this,"Household")+"\r\n";
				}
				else {//print single label
					text=AddrTable.Rows[patientsPrinted]["patientNameFL"].ToString()+"\r\n";
				}
				text+=AddrTable.Rows[patientsPrinted]["address"].ToString()+"\r\n";
				text+=AddrTable.Rows[patientsPrinted]["cityStZip"].ToString()+"\r\n";
				LabelSingle.PrintText(0,text);
				patientsPrinted++;
			}
			if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Did all the labels finish printing correctly?  Statuses will be changed and commlog entries made for all of the selected patients.  Click Yes only if labels printed successfully.")) {
				Cursor=Cursors.WaitCursor;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
					//make commlog entries for each patient
					Commlogs.InsertForRecall(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString()),CommItemMode.Mail,
						PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["numberOfReminders"].ToString()),PrefC.GetLong(PrefName.RecallStatusMailed));
				}
				for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
					Recalls.UpdateStatus(
						PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()),PrefC.GetLong(PrefName.RecallStatusMailed));
				}
			}
			FillMain(null);
			Cursor=Cursors.Default;
		}

		private void butPostcards_Click(object sender, System.EventArgs e) {
			if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one to print."));    
        return;
      }
			if(PrefC.GetLong(PrefName.RecallStatusMailed)==0){
				MsgBox.Show(this,"You need to set a status first in the Recall Setup window.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				ContactMethod cmeth;
				for(int i=0;i<table.Rows.Count;i++){
					//if(table.Rows[i]["status"].ToString()!=""){//we only want rows without a status
					//	continue;
					//}
					cmeth=(ContactMethod)PIn.PLong(table.Rows[i]["PreferRecallMethod"].ToString());
					if(cmeth!=ContactMethod.Mail && cmeth!=ContactMethod.None){
						continue;
					}
					gridMain.SetSelected(i,true);
				}
				if(gridMain.SelectedIndices.Length==0){
					MsgBox.Show(this,"No patients of mail type.");
					return;
				}
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Preview postcards for all of the selected patients?")) {
					return;
				}
			}
			List<long> recallNums=new List<long>();
      for(int i=0;i<gridMain.SelectedIndices.Length;i++){
        recallNums.Add(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()));
      }
			bool sortAlph=false;
			if(comboSort.SelectedIndex==1) {
				sortAlph=true;
			}
			AddrTable=Recalls.GetAddrTable(recallNums,checkGroupFamilies.Checked,sortAlph);
			pagesPrinted=0;
			patientsPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pdCards_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			if(PrefC.GetLong(PrefName.RecallPostcardsPerSheet)==1){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",400,600);
				pd.DefaultPageSettings.Landscape=true;
			}
			else if(PrefC.GetLong(PrefName.RecallPostcardsPerSheet)==3){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",850,1100);
			}
			else{//4
				pd.DefaultPageSettings.PaperSize=new PaperSize("Postcard",850,1100);
				pd.DefaultPageSettings.Landscape=true;
			}
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)PrefC.GetLong(PrefName.RecallPostcardsPerSheet));
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.Postcard,pd,totalPages);
			printPreview.ShowDialog();
			if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Did all the postcards finish printing correctly?  Statuses will be changed and commlog entries made for all of the selected patients.  Click Yes only if postcards printed successfully.")) {
				Cursor=Cursors.WaitCursor;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){
					//make commlog entries for each patient
					Commlogs.InsertForRecall(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString()),CommItemMode.Mail,
						PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["numberOfReminders"].ToString()),PrefC.GetLong(PrefName.RecallStatusMailed));
				}
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){
					Recalls.UpdateStatus(
						PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()),PrefC.GetLong(PrefName.RecallStatusMailed));
				}
			}
			FillMain(null);
			Cursor=Cursors.Default;
		}

		private void butEmail_Click(object sender,EventArgs e) {
			if(gridMain.Rows.Count < 1){
        MessageBox.Show(Lan.g(this,"There are no Patients in the Recall table.  Must have at least one."));    
        return;
      }
			if(PrefC.GetString(PrefName.EmailSMTPserver)==""){
				MsgBox.Show(this,"You need to enter an SMTP server name in e-mail setup before you can send e-mail.");
				return;
			}
			if(PrefC.GetLong(PrefName.RecallStatusEmailed)==0){
				MsgBox.Show(this,"You need to set a status first in the Recall Setup window.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				ContactMethod cmeth;
				for(int i=0;i<table.Rows.Count;i++){
					cmeth=(ContactMethod)PIn.PLong(table.Rows[i]["PreferRecallMethod"].ToString());
					if(cmeth!=ContactMethod.Email){
						continue;
					}
					gridMain.SetSelected(i,true);
				}
				if(gridMain.SelectedIndices.Length==0){
					MsgBox.Show(this,"No patients of email type.");
					return;
				}
			}
			else{//deselect the ones that do not have email addresses specified
				int skipped=0;
				for(int i=gridMain.SelectedIndices.Length-1;i>=0;i--){
					if(table.Rows[gridMain.SelectedIndices[i]]["Email"].ToString()==""){
						skipped++;
						gridMain.SetSelected(gridMain.SelectedIndices[i],false);
					}
				}
				if(gridMain.SelectedIndices.Length==0){
					MsgBox.Show(this,"None of the selected patients had email addresses entered.");
					return;
				}
				if(skipped>0){
					MessageBox.Show(Lan.g(this,"Selected patients skipped due to missing email addresses: ")+skipped.ToString());
				}
			}
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"Send email to all of the selected patients?")) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			List<long> recallNums=new List<long>();
      for(int i=0;i<gridMain.SelectedIndices.Length;i++){
        recallNums.Add(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()));
      }
			bool sortAlph=false;
			if(comboSort.SelectedIndex==1) {
				sortAlph=true;
			}
			AddrTable=Recalls.GetAddrTable(recallNums,checkGroupFamilies.Checked,sortAlph);
			EmailMessage message;
			string str="";
			string[] recallNumArray;
			string[] patNumArray;
			for(int i=0;i<AddrTable.Rows.Count;i++){
				message=new EmailMessage();
				message.PatNum=PIn.PLong(AddrTable.Rows[i]["emailPatNum"].ToString());
				message.ToAddress=PIn.PString(AddrTable.Rows[i]["email"].ToString());//might be guarantor email
				message.FromAddress=PrefC.GetString(PrefName.EmailSenderAddress);
				if(AddrTable.Rows[i]["numberOfReminders"].ToString()=="0") {
					message.Subject=PrefC.GetString(PrefName.RecallEmailSubject);
				}
				else if(AddrTable.Rows[i]["numberOfReminders"].ToString()=="1") {
					message.Subject=PrefC.GetString(PrefName.RecallEmailSubject2);
				}
				else {
					message.Subject=PrefC.GetString(PrefName.RecallEmailSubject3);
				}
				//family
				if(checkGroupFamilies.Checked	&& AddrTable.Rows[i]["famList"].ToString()!="") {
					if(AddrTable.Rows[i]["numberOfReminders"].ToString()=="0") {
						str=PrefC.GetString(PrefName.RecallEmailFamMsg);
					}
					else if(AddrTable.Rows[i]["numberOfReminders"].ToString()=="1") {
						str=PrefC.GetString(PrefName.RecallEmailFamMsg2);
					}
					else {
						str=PrefC.GetString(PrefName.RecallEmailFamMsg3);
					}
					str=str.Replace("[FamilyList]",AddrTable.Rows[i]["famList"].ToString());
				}
				//single
				else {
					if(AddrTable.Rows[i]["numberOfReminders"].ToString()=="0") {
						str=PrefC.GetString(PrefName.RecallEmailMessage);
					}
					else if(AddrTable.Rows[i]["numberOfReminders"].ToString()=="1") {
						str=PrefC.GetString(PrefName.RecallEmailMessage2);
					}
					else {
						str=PrefC.GetString(PrefName.RecallEmailMessage3);
					}
					str=str.Replace("[DueDate]",PIn.PDate(AddrTable.Rows[i]["dateDue"].ToString()).ToShortDateString());
					str=str.Replace("[NameF]",AddrTable.Rows[i]["patientNameF"].ToString());
					str=str.Replace("[NameFL]",AddrTable.Rows[i]["patientNameFL"].ToString());
				}
				message.BodyText=str;
				try{
					FormEmailMessageEdit.SendEmail(message);
				}
				catch(Exception ex){
					Cursor=Cursors.Default;
					MessageBox.Show(ex.Message+"\r\nPatient:"+AddrTable.Rows[i]["patientNameFL"].ToString());
					break;
				}
				message.MsgDateTime=DateTime.Now;
				message.SentOrReceived=CommSentOrReceived.Sent;
				EmailMessages.Insert(message);
				recallNumArray=AddrTable.Rows[i]["recallNums"].ToString().Split(',');
				patNumArray=AddrTable.Rows[i]["patNums"].ToString().Split(',');
				for(int r=0;r<recallNumArray.Length;r++){
					Commlogs.InsertForRecall(PIn.PLong(patNumArray[r]),CommItemMode.Email,PIn.PInt(AddrTable.Rows[i]["numberOfReminders"].ToString()),
						PrefC.GetLong(PrefName.RecallStatusEmailed));
					Recalls.UpdateStatus(PIn.PLong(recallNumArray[r]),PrefC.GetLong(PrefName.RecallStatusEmailed));
				}
			}
			FillMain(null);
			Cursor=Cursors.Default;
		}

		///<summary>raised for each page to be printed.</summary>
		private void pdLabels_PrintPage(object sender, PrintPageEventArgs ev){
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/30);
			Graphics g=ev.Graphics;
			float yPos=75;
			float xPos=50;
			string text="";
			while(yPos<1000 && patientsPrinted<AddrTable.Rows.Count){
				text="";
				if(checkGroupFamilies.Checked && AddrTable.Rows[patientsPrinted]["famList"].ToString()!=""){//print family label
					text=AddrTable.Rows[patientsPrinted]["guarLName"].ToString()+" "+Lan.g(this,"Household")+"\r\n";
				}
				else {//print single label
					text=AddrTable.Rows[patientsPrinted]["patientNameFL"].ToString()+"\r\n";
				}
				text+=AddrTable.Rows[patientsPrinted]["address"].ToString()+"\r\n";
				text+=AddrTable.Rows[patientsPrinted]["cityStZip"].ToString()+"\r\n";
				g.DrawString(text,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,xPos,yPos);
				//reposition for next label
				xPos+=275;
				if(xPos>850){//drop a line
					xPos=50;
					yPos+=100;
				}
				patientsPrinted++;
			}
			pagesPrinted++;
			if(pagesPrinted==totalPages){
				ev.HasMorePages=false;
				pagesPrinted=0;//because it has to print again from the print preview
				patientsPrinted=0;
			}
			else{
				ev.HasMorePages=true;
			}
		}

		///<summary>raised for each page to be printed.</summary>
		private void pdCards_PrintPage(object sender, PrintPageEventArgs ev){
			int totalPages=(int)Math.Ceiling((double)AddrTable.Rows.Count/(double)PrefC.GetLong(PrefName.RecallPostcardsPerSheet));
			Graphics g=ev.Graphics;
			int yAdj=(int)(PrefC.GetDouble(PrefName.RecallAdjustDown)*100);
			int xAdj=(int)(PrefC.GetDouble(PrefName.RecallAdjustRight)*100);
			float yPos=0+yAdj;//these refer to the upper left origin of each postcard
			float xPos=0+xAdj;
			string str;
			while(yPos<ev.PageBounds.Height-100 && patientsPrinted<AddrTable.Rows.Count){
				//Return Address--------------------------------------------------------------------------
				if(PrefC.GetBool(PrefName.RecallCardsShowReturnAdd)){
					str=PrefC.GetString(PrefName.PracticeTitle)+"\r\n";
					g.DrawString(str,new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold),Brushes.Black,xPos+45,yPos+60);
					str=PrefC.GetString(PrefName.PracticeAddress)+"\r\n";
					if(PrefC.GetString(PrefName.PracticeAddress2)!=""){
						str+=PrefC.GetString(PrefName.PracticeAddress2)+"\r\n";
					}
					str+=PrefC.GetString(PrefName.PracticeCity)+",  "+PrefC.GetString(PrefName.PracticeST)+"  "+PrefC.GetString(PrefName.PracticeZip)+"\r\n";
					string phone=PrefC.GetString(PrefName.PracticePhone);
					if(CultureInfo.CurrentCulture.Name=="en-US"&& phone.Length==10){
						str+="("+phone.Substring(0,3)+")"+phone.Substring(3,3)+"-"+phone.Substring(6);
					}
					else{//any other phone format
						str+=phone;
					}
					g.DrawString(str,new Font(FontFamily.GenericSansSerif,8),Brushes.Black,xPos+45,yPos+75);
				}
				//Body text, family card ------------------------------------------------------------------
				if(checkGroupFamilies.Checked	&& AddrTable.Rows[patientsPrinted]["famList"].ToString()!=""){
					if(AddrTable.Rows[patientsPrinted]["numberOfReminders"].ToString()=="0") {
						str=PrefC.GetString(PrefName.RecallPostcardFamMsg);
					}
					else if(AddrTable.Rows[patientsPrinted]["numberOfReminders"].ToString()=="1") {
						str=PrefC.GetString(PrefName.RecallPostcardFamMsg2);
					}
					else {
						str=PrefC.GetString(PrefName.RecallPostcardFamMsg3);
					}
					str=str.Replace("[FamilyList]",AddrTable.Rows[patientsPrinted]["famList"].ToString());
				}
				//Body text, single card-------------------------------------------------------------------
				else{
					if(AddrTable.Rows[patientsPrinted]["numberOfReminders"].ToString()=="0") {
						str=PrefC.GetString(PrefName.RecallPostcardMessage);
					}
					else if(AddrTable.Rows[patientsPrinted]["numberOfReminders"].ToString()=="1") {
						str=PrefC.GetString(PrefName.RecallPostcardMessage2);
					}
					else {
						str=PrefC.GetString(PrefName.RecallPostcardMessage3);
					}
					str=str.Replace("[DueDate]",AddrTable.Rows[patientsPrinted]["dateDue"].ToString());
				}
				g.DrawString(str,new Font(FontFamily.GenericSansSerif,10),Brushes.Black,new RectangleF(xPos+45,yPos+180,250,190));
				//Patient's Address-----------------------------------------------------------------------
				if(checkGroupFamilies.Checked
					&& AddrTable.Rows[patientsPrinted]["famList"].ToString()!="")//print family card
				{
					str=AddrTable.Rows[patientsPrinted]["guarLName"].ToString()+" "+Lan.g(this,"Household")+"\r\n";
				}
				else{//print single card
					str=AddrTable.Rows[patientsPrinted]["patientNameFL"].ToString()+"\r\n";
				}
				str+=AddrTable.Rows[patientsPrinted]["address"].ToString()+"\r\n";
				str+=AddrTable.Rows[patientsPrinted]["cityStZip"].ToString()+"\r\n";
				g.DrawString(str,new Font(FontFamily.GenericSansSerif,11),Brushes.Black,xPos+320,yPos+240);
				if(PrefC.GetLong(PrefName.RecallPostcardsPerSheet)==1){
					yPos+=400;
				}
				else if(PrefC.GetLong(PrefName.RecallPostcardsPerSheet)==3){
					yPos+=366;
				}
				else{//4
					xPos+=550;
					if(xPos>1000){
						xPos=0+xAdj;
						yPos+=425;
					}
				}
				patientsPrinted++;
			}//while
			pagesPrinted++;
			if(pagesPrinted==totalPages){
				ev.HasMorePages=false;
				pagesPrinted=0;
				patientsPrinted=0;
			}
			else{
				ev.HasMorePages=true;
			}
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			gridMain.SetSelected(false);
			FillMain(null);
		}

		private void butSetStatus_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			//bool makeCommEntries=MsgBox.Show(this,MsgBoxButtons.OKCancel,"Add Commlog (reminder) entries for each patient?");
			long newStatus=0;
			if(comboStatus.SelectedIndex>0) {
				newStatus=DefC.Short[(int)DefCat.RecallUnschedStatus][comboStatus.SelectedIndex-1].DefNum;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				Recalls.UpdateStatus(PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["RecallNum"].ToString()),newStatus);
			}
			//show the first one, and then make all the others very similar
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.SentOrReceived=CommSentOrReceived.Sent;
			CommlogCur.Mode_=CommItemMode.Phone;//user can change this, of course.
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
			CommlogCur.UserNum=Security.CurUser.UserNum;
			CommlogCur.Note=Lan.g(this,"Recall reminder.");
			if(comboStatus.SelectedIndex>0) {
				CommlogCur.Note+="  "+DefC.Short[(int)DefCat.RecallUnschedStatus][comboStatus.SelectedIndex-1].ItemName;
			}
			else{
				CommlogCur.Note+="  "+Lan.g(this,"Status None");
			}
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			if(FormCI.DialogResult!=DialogResult.OK) {//if user cancels, then the other comm entries won't go in either
				FillMain(null);
				return;
			}
			for(int i=1;i<gridMain.SelectedIndices.Length;i++) {
				CommlogCur.PatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString());
				Commlogs.Insert(CommlogCur);
			}
			FillMain(null);
		}

		private void menuItemSeeFamily_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			long patNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoFamily(patNum);
		}

		private void menuItemSeeAccount_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			//WindowState=FormWindowState.Minimized;
			long patNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoAccount(patNum);
		}

		private void butGotoFamily_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			WindowState=FormWindowState.Minimized;
			long patNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoFamily(patNum);
		}

		private void butGotoAccount_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			WindowState=FormWindowState.Minimized;
			long patNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoAccount(patNum);
		}

		private void butCommlog_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select a patient first.");
				return;
			}
			//show the first one, and then make all the others very similar
			Commlog CommlogCur=new Commlog();
			CommlogCur.PatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.SentOrReceived=CommSentOrReceived.Sent;
			CommlogCur.Mode_=CommItemMode.Phone;//user can change this, of course.
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
			CommlogCur.UserNum=Security.CurUser.UserNum;
			FormCommItem FormCI=new FormCommItem(CommlogCur);
			FormCI.IsNew=true;
			FormCI.ShowDialog();
			if(FormCI.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=1;i<gridMain.SelectedIndices.Length;i++) {
				CommlogCur.PatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString());
				Commlogs.Insert(CommlogCur);
			}
			FillMain(null);
		}

		/*
		private void MakeCommlogNoDisplay(long patNum,long newStatus) {
			//unless there is an existing recall commlog entry for today
			Commlog[] CommlogList=Commlogs.Refresh(patNum);
			for(int i=0;i<CommlogList.Length;i++){
				if(CommlogList[i].CommDateTime.Date==DateTime.Today	
					&& CommlogList[i].CommType==Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL))
				{
					return;
				}
			}
			Commlog CommlogCur=new Commlog();
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
			CommlogCur.PatNum=patNum;
			//if(newStatus!=RecallCur.RecallStatus){
			//Commlogs.Cur.Note+=Lan.g(this,"Status changed to")+" ";
			if(newStatus==0) {
				CommlogCur.Note=Lan.g(this,"Status None");
			}
			else {
				CommlogCur.Note=DefC.GetName(DefCat.RecallUnschedStatus,newStatus);
			}
			CommlogCur.UserNum=Security.CurUser.UserNum;
			//FormCommItem FormCI=new FormCommItem(CommlogCur);
			//FormCI.IsNew=true;
			//forces user to at least consider a commlog entry
			//FormCI.ShowDialog();//typically saved in this window.
			Commlogs.InsertForRecall(			
		}*/

		/*private void butSave_Click(object sender, System.EventArgs e) {
			if(  textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			int daysPast=((TimeSpan)(DateTime.Today-PIn.PDate(textDateStart.Text))).Days;//can be neg
			int daysFuture=((TimeSpan)(PIn.PDate(textDateEnd.Text)-DateTime.Today)).Days;//can be neg
			if(Prefs.UpdateBool(PrefName.RecallGroupByFamily",checkGroupFamilies.Checked)
				| Prefs.UpdateLong(PrefName.RecallDaysPast",daysPast)
				| Prefs.UpdateLong(PrefName.RecallDaysFuture",daysFuture))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}*/

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Landscape=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			try {
				#if DEBUG
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd;
					pView.ShowDialog();
				#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
				//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=Lan.g(this,"Recall List");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				text=textDateStart.Text+" "+Lan.g(this,"to")+" "+textDateEnd.Text;
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			int totalPages=gridMain.GetNumberOfPages(bounds,headingPrintH);
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(pagesPrinted < totalPages) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormRecallList_FormClosing(object sender,FormClosingEventArgs e) {
			if(gridMain.SelectedIndices.Length==1){
				SelectedPatNum=PIn.PLong(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			}
		}

		

	

		

		

	


	

		

		

	

		

		

		

		
	}

	/*
	///<summary>Mostly used just to display the recall list.</summary>
	public class RecallItem{
		///<summary></summary>
		public DateTime DueDate;
		///<summary></summary>
		public string PatientName;
		//<summary></summary>
		//public DateTime BirthDate;
		///<summary></summary>
		public Interval RecallInterval;
		///<summary></summary>
		public int RecallStatus;
		///<summary></summary>
		public int PatNum;
		///<summary>Stored as a string because it might be blank.</summary>
		public string Age;
		///<summary></summary>
		public string Note;
		///<summary></summary>
		public int RecallNum;
		///<summary></summary>
		public int Guarantor;
	}*/
}
