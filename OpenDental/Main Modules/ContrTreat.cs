/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using OpenDental.UI;
using SparksToothChart;
using OpenDentBusiness;
using CodeBase;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;

namespace OpenDental{
///<summary></summary>
	public class ContrTreat : System.Windows.Forms.UserControl{
		//private AxFPSpread.AxvaSpread axvaSpread2;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;// Required designer variable.
		private System.Windows.Forms.ListBox listSetPr;
		//<summary></summary>
		//public static ArrayList TPLines2;
		//private bool[] selectedPrs;//had to use this because of deficiency in available Listbox events.
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		//private int linesPrinted=0;
		///<summary></summary>
    public FormRpPrintPreview pView;
//		private System.Windows.Forms.PrintDialog printDialog2;
		//private bool headingPrinted;
		//private bool graphicsPrinted;
		//private bool mainPrinted;
		//private bool benefitsPrinted;
		//private bool notePrinted;
		private double[] ColTotal;
		private System.Drawing.Font bodyFont=new System.Drawing.Font("Arial",9);
		private System.Drawing.Font nameFont=new System.Drawing.Font("Arial",9,FontStyle.Bold);
		//private Font headingFont=new Font("Arial",13,FontStyle.Bold);
		private System.Drawing.Font subHeadingFont=new System.Drawing.Font("Arial",10,FontStyle.Bold);
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Drawing.Font totalFont=new System.Drawing.Font("Arial",9,FontStyle.Bold);
		//private int yPos=938;
	  //private	int xPos=25;
		private System.Windows.Forms.TextBox textPriMax;
		private System.Windows.Forms.TextBox textSecUsed;
		private System.Windows.Forms.TextBox textSecDed;
		private System.Windows.Forms.TextBox textSecMax;
		private System.Windows.Forms.TextBox textPriRem;
		private System.Windows.Forms.TextBox textPriPend;
		private System.Windows.Forms.TextBox textPriUsed;
		private System.Windows.Forms.TextBox textPriDed;
		private System.Windows.Forms.TextBox textSecRem;
		private System.Windows.Forms.TextBox textSecPend;
		private System.Windows.Forms.TextBox textPriDedRem;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textSecDedRem;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.ODToolBar ToolBarMain;
    private ArrayList ALPreAuth;
		///<summary>This is a list of all procedures for the patient.</summary>
		private List<Procedure> ProcList;
		///<summary>This is a filtered list containing only TP procedures.  It's also already sorted by priority and tooth number.</summary>
		private Procedure[] ProcListTP;
		private System.Windows.Forms.CheckBox checkShowCompleted;
		private System.Windows.Forms.GroupBox groupShow;
		private System.Windows.Forms.CheckBox checkShowIns;
		private List<ClaimProc> ClaimProcList;
		private Family FamCur;
		private Patient PatCur;
		private System.Windows.Forms.CheckBox checkShowFees;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.ODGrid gridPreAuth;
		private List <InsPlan> InsPlanList;
		private OpenDental.UI.ODGrid gridPlans;
		private TreatPlan[] PlanList;
		///<summary>A list of all ProcTP objects for this patient.</summary>
		private ProcTP[] ProcTPList;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.ImageList imageListMain;
		///<summary>A list of all ProcTP objects for the selected tp.</summary>
		private ProcTP[] ProcTPSelectList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private List <PatPlan> PatPlanList;
		private List <Benefit> BenefitList;
		private ArrayList ProcAL;
		///<summary>Only used for printing graphical chart.</summary>
		private List<ToothInitial> ToothInitialList;
		///<summary>Only used for printing graphical chart.</summary>
		private ToothChartWrapper toothChart;
		private CheckBox checkShowSubtotals;
		private CheckBox checkShowMaxDed;
		///<summary>Only used for printing graphical chart.</summary>
		private Bitmap chartBitmap;
		//private int headingPrintH;
		private CheckBox checkShowTotals;
		//private int pagesPrinted;
		private CheckBox checkShowDiscount;
		private List<Claim> ClaimList;
		private bool InitializedOnStartup;
		private List<ClaimProcHist> HistList;
		private TextBox textPriDedFam;
		private TextBox textSecDedFam;
		private Label label2;
		private List<ClaimProcHist> LoopList;

		///<summary></summary>
		public ContrTreat(){
			Logger.openlog.Log("Initializing treatment module...",Logger.Severity.INFO);
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrTreat));
			this.label1 = new System.Windows.Forms.Label();
			this.listSetPr = new System.Windows.Forms.ListBox();
			this.groupShow = new System.Windows.Forms.GroupBox();
			this.checkShowDiscount = new System.Windows.Forms.CheckBox();
			this.checkShowTotals = new System.Windows.Forms.CheckBox();
			this.checkShowMaxDed = new System.Windows.Forms.CheckBox();
			this.checkShowSubtotals = new System.Windows.Forms.CheckBox();
			this.checkShowFees = new System.Windows.Forms.CheckBox();
			this.checkShowIns = new System.Windows.Forms.CheckBox();
			this.checkShowCompleted = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textPriMax = new System.Windows.Forms.TextBox();
			this.textSecUsed = new System.Windows.Forms.TextBox();
			this.textSecDed = new System.Windows.Forms.TextBox();
			this.textSecMax = new System.Windows.Forms.TextBox();
			this.textPriRem = new System.Windows.Forms.TextBox();
			this.textPriPend = new System.Windows.Forms.TextBox();
			this.textPriUsed = new System.Windows.Forms.TextBox();
			this.textPriDed = new System.Windows.Forms.TextBox();
			this.textSecRem = new System.Windows.Forms.TextBox();
			this.textSecPend = new System.Windows.Forms.TextBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.textPriDedRem = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.textSecDedRem = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.gridPreAuth = new OpenDental.UI.ODGrid();
			this.gridPlans = new OpenDental.UI.ODGrid();
			this.textPriDedFam = new System.Windows.Forms.TextBox();
			this.textSecDedFam = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupShow.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",8F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label1.Location = new System.Drawing.Point(755,167);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97,15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Set Priority";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSetPr
			// 
			this.listSetPr.Location = new System.Drawing.Point(757,184);
			this.listSetPr.Name = "listSetPr";
			this.listSetPr.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listSetPr.Size = new System.Drawing.Size(70,212);
			this.listSetPr.TabIndex = 5;
			this.listSetPr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSetPr_MouseDown);
			// 
			// groupShow
			// 
			this.groupShow.Controls.Add(this.checkShowDiscount);
			this.groupShow.Controls.Add(this.checkShowTotals);
			this.groupShow.Controls.Add(this.checkShowMaxDed);
			this.groupShow.Controls.Add(this.checkShowSubtotals);
			this.groupShow.Controls.Add(this.checkShowFees);
			this.groupShow.Controls.Add(this.checkShowIns);
			this.groupShow.Controls.Add(this.checkShowCompleted);
			this.groupShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupShow.Location = new System.Drawing.Point(466,25);
			this.groupShow.Name = "groupShow";
			this.groupShow.Size = new System.Drawing.Size(173,138);
			this.groupShow.TabIndex = 59;
			this.groupShow.TabStop = false;
			this.groupShow.Text = "Show";
			// 
			// checkShowDiscount
			// 
			this.checkShowDiscount.Checked = true;
			this.checkShowDiscount.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowDiscount.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowDiscount.Location = new System.Drawing.Point(31,84);
			this.checkShowDiscount.Name = "checkShowDiscount";
			this.checkShowDiscount.Size = new System.Drawing.Size(131,17);
			this.checkShowDiscount.TabIndex = 25;
			this.checkShowDiscount.Text = "Discount (PPO)";
			this.checkShowDiscount.Click += new System.EventHandler(this.checkShowDiscount_Click);
			// 
			// checkShowTotals
			// 
			this.checkShowTotals.Checked = true;
			this.checkShowTotals.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowTotals.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowTotals.Location = new System.Drawing.Point(31,118);
			this.checkShowTotals.Name = "checkShowTotals";
			this.checkShowTotals.Size = new System.Drawing.Size(128,15);
			this.checkShowTotals.TabIndex = 24;
			this.checkShowTotals.Text = "Totals";
			this.checkShowTotals.Click += new System.EventHandler(this.checkShowTotals_Click);
			// 
			// checkShowMaxDed
			// 
			this.checkShowMaxDed.Checked = true;
			this.checkShowMaxDed.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowMaxDed.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowMaxDed.Location = new System.Drawing.Point(15,33);
			this.checkShowMaxDed.Name = "checkShowMaxDed";
			this.checkShowMaxDed.Size = new System.Drawing.Size(154,17);
			this.checkShowMaxDed.TabIndex = 23;
			this.checkShowMaxDed.Text = "Use Ins Max and Deduct";
			this.checkShowMaxDed.Click += new System.EventHandler(this.checkShowMaxDed_Click);
			// 
			// checkShowSubtotals
			// 
			this.checkShowSubtotals.Checked = true;
			this.checkShowSubtotals.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowSubtotals.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowSubtotals.Location = new System.Drawing.Point(31,101);
			this.checkShowSubtotals.Name = "checkShowSubtotals";
			this.checkShowSubtotals.Size = new System.Drawing.Size(128,17);
			this.checkShowSubtotals.TabIndex = 22;
			this.checkShowSubtotals.Text = "Subtotals";
			this.checkShowSubtotals.Click += new System.EventHandler(this.checkShowSubtotals_Click);
			// 
			// checkShowFees
			// 
			this.checkShowFees.Checked = true;
			this.checkShowFees.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowFees.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowFees.Location = new System.Drawing.Point(15,50);
			this.checkShowFees.Name = "checkShowFees";
			this.checkShowFees.Size = new System.Drawing.Size(146,17);
			this.checkShowFees.TabIndex = 20;
			this.checkShowFees.Text = "Fees";
			this.checkShowFees.Click += new System.EventHandler(this.checkShowFees_Click);
			// 
			// checkShowIns
			// 
			this.checkShowIns.Checked = true;
			this.checkShowIns.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowIns.Location = new System.Drawing.Point(31,67);
			this.checkShowIns.Name = "checkShowIns";
			this.checkShowIns.Size = new System.Drawing.Size(131,17);
			this.checkShowIns.TabIndex = 19;
			this.checkShowIns.Text = "Insurance Estimates";
			this.checkShowIns.Click += new System.EventHandler(this.checkShowIns_Click);
			// 
			// checkShowCompleted
			// 
			this.checkShowCompleted.Checked = true;
			this.checkShowCompleted.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCompleted.Location = new System.Drawing.Point(15,16);
			this.checkShowCompleted.Name = "checkShowCompleted";
			this.checkShowCompleted.Size = new System.Drawing.Size(154,17);
			this.checkShowCompleted.TabIndex = 18;
			this.checkShowCompleted.Text = "Graphical Completed Tx";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(817,485);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60,15);
			this.label10.TabIndex = 31;
			this.label10.Text = "Primary";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(737,506);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(78,15);
			this.label11.TabIndex = 32;
			this.label11.Text = "Annual Max";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(736,527);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(79,15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Deductible";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(736,589);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(79,13);
			this.label13.TabIndex = 34;
			this.label13.Text = "Ins Used";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(737,627);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(79,14);
			this.label14.TabIndex = 35;
			this.label14.Text = "Remaining";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(735,607);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(81,14);
			this.label15.TabIndex = 36;
			this.label15.Text = "Pending";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(876,485);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60,14);
			this.label16.TabIndex = 37;
			this.label16.Text = "Secondary";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textPriMax
			// 
			this.textPriMax.BackColor = System.Drawing.Color.White;
			this.textPriMax.Location = new System.Drawing.Point(817,504);
			this.textPriMax.Name = "textPriMax";
			this.textPriMax.ReadOnly = true;
			this.textPriMax.Size = new System.Drawing.Size(60,20);
			this.textPriMax.TabIndex = 38;
			this.textPriMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecUsed
			// 
			this.textSecUsed.BackColor = System.Drawing.Color.White;
			this.textSecUsed.Location = new System.Drawing.Point(876,584);
			this.textSecUsed.Name = "textSecUsed";
			this.textSecUsed.ReadOnly = true;
			this.textSecUsed.Size = new System.Drawing.Size(60,20);
			this.textSecUsed.TabIndex = 39;
			this.textSecUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecDed
			// 
			this.textSecDed.BackColor = System.Drawing.Color.White;
			this.textSecDed.Location = new System.Drawing.Point(876,524);
			this.textSecDed.Name = "textSecDed";
			this.textSecDed.ReadOnly = true;
			this.textSecDed.Size = new System.Drawing.Size(60,20);
			this.textSecDed.TabIndex = 40;
			this.textSecDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecMax
			// 
			this.textSecMax.BackColor = System.Drawing.Color.White;
			this.textSecMax.Location = new System.Drawing.Point(876,504);
			this.textSecMax.Name = "textSecMax";
			this.textSecMax.ReadOnly = true;
			this.textSecMax.Size = new System.Drawing.Size(60,20);
			this.textSecMax.TabIndex = 41;
			this.textSecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriRem
			// 
			this.textPriRem.BackColor = System.Drawing.Color.White;
			this.textPriRem.Location = new System.Drawing.Point(817,624);
			this.textPriRem.Name = "textPriRem";
			this.textPriRem.ReadOnly = true;
			this.textPriRem.Size = new System.Drawing.Size(60,20);
			this.textPriRem.TabIndex = 42;
			this.textPriRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriPend
			// 
			this.textPriPend.BackColor = System.Drawing.Color.White;
			this.textPriPend.Location = new System.Drawing.Point(817,604);
			this.textPriPend.Name = "textPriPend";
			this.textPriPend.ReadOnly = true;
			this.textPriPend.Size = new System.Drawing.Size(60,20);
			this.textPriPend.TabIndex = 43;
			this.textPriPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriUsed
			// 
			this.textPriUsed.BackColor = System.Drawing.Color.White;
			this.textPriUsed.Location = new System.Drawing.Point(817,584);
			this.textPriUsed.Name = "textPriUsed";
			this.textPriUsed.ReadOnly = true;
			this.textPriUsed.Size = new System.Drawing.Size(60,20);
			this.textPriUsed.TabIndex = 44;
			this.textPriUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriDed
			// 
			this.textPriDed.BackColor = System.Drawing.Color.White;
			this.textPriDed.Location = new System.Drawing.Point(817,524);
			this.textPriDed.Name = "textPriDed";
			this.textPriDed.ReadOnly = true;
			this.textPriDed.Size = new System.Drawing.Size(60,20);
			this.textPriDed.TabIndex = 45;
			this.textPriDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecRem
			// 
			this.textSecRem.BackColor = System.Drawing.Color.White;
			this.textSecRem.Location = new System.Drawing.Point(876,624);
			this.textSecRem.Name = "textSecRem";
			this.textSecRem.ReadOnly = true;
			this.textSecRem.Size = new System.Drawing.Size(60,20);
			this.textSecRem.TabIndex = 46;
			this.textSecRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecPend
			// 
			this.textSecPend.BackColor = System.Drawing.Color.White;
			this.textSecPend.Location = new System.Drawing.Point(876,604);
			this.textSecPend.Name = "textSecPend";
			this.textSecPend.ReadOnly = true;
			this.textSecPend.Size = new System.Drawing.Size(60,20);
			this.textSecPend.TabIndex = 47;
			this.textSecPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriDedRem
			// 
			this.textPriDedRem.BackColor = System.Drawing.Color.White;
			this.textPriDedRem.Location = new System.Drawing.Point(817,564);
			this.textPriDedRem.Name = "textPriDedRem";
			this.textPriDedRem.ReadOnly = true;
			this.textPriDedRem.Size = new System.Drawing.Size(60,20);
			this.textPriDedRem.TabIndex = 51;
			this.textPriDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(721,568);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(96,15);
			this.label18.TabIndex = 50;
			this.label18.Text = "Ded Remain";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecDedRem
			// 
			this.textSecDedRem.BackColor = System.Drawing.Color.White;
			this.textSecDedRem.Location = new System.Drawing.Point(876,564);
			this.textSecDedRem.Name = "textSecDedRem";
			this.textSecDedRem.ReadOnly = true;
			this.textSecDedRem.Size = new System.Drawing.Size(60,20);
			this.textSecDedRem.TabIndex = 52;
			this.textSecDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(827,470);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,12);
			this.label3.TabIndex = 53;
			this.label3.Text = "Insurance";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textNote
			// 
			this.textNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textNote.BackColor = System.Drawing.Color.White;
			this.textNote.Location = new System.Drawing.Point(0,654);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(698,52);
			this.textNote.TabIndex = 54;
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"");
			this.imageListMain.Images.SetKeyName(1,"");
			this.imageListMain.Images.SetKeyName(2,"");
			this.imageListMain.Images.SetKeyName(3,"Add.gif");
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,25);
			this.ToolBarMain.TabIndex = 58;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0,169);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(745,482);
			this.gridMain.TabIndex = 59;
			this.gridMain.Title = "Procedures";
			this.gridMain.TranslationName = "TableTP";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// gridPreAuth
			// 
			this.gridPreAuth.HScrollVisible = false;
			this.gridPreAuth.Location = new System.Drawing.Point(659,29);
			this.gridPreAuth.Name = "gridPreAuth";
			this.gridPreAuth.ScrollValue = 0;
			this.gridPreAuth.Size = new System.Drawing.Size(252,134);
			this.gridPreAuth.TabIndex = 62;
			this.gridPreAuth.Title = "Pre Authorizations";
			this.gridPreAuth.TranslationName = "TablePreAuth";
			this.gridPreAuth.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPreAuth_CellClick);
			this.gridPreAuth.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPreAuth_CellDoubleClick);
			// 
			// gridPlans
			// 
			this.gridPlans.HScrollVisible = false;
			this.gridPlans.Location = new System.Drawing.Point(0,29);
			this.gridPlans.Name = "gridPlans";
			this.gridPlans.ScrollValue = 0;
			this.gridPlans.Size = new System.Drawing.Size(460,134);
			this.gridPlans.TabIndex = 60;
			this.gridPlans.Title = "Treatment Plans";
			this.gridPlans.TranslationName = "TableTPList";
			this.gridPlans.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPlans_CellClick);
			this.gridPlans.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPlans_CellDoubleClick);
			// 
			// textPriDedFam
			// 
			this.textPriDedFam.BackColor = System.Drawing.Color.White;
			this.textPriDedFam.Location = new System.Drawing.Point(817,544);
			this.textPriDedFam.Name = "textPriDedFam";
			this.textPriDedFam.ReadOnly = true;
			this.textPriDedFam.Size = new System.Drawing.Size(60,20);
			this.textPriDedFam.TabIndex = 65;
			this.textPriDedFam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecDedFam
			// 
			this.textSecDedFam.BackColor = System.Drawing.Color.White;
			this.textSecDedFam.Location = new System.Drawing.Point(876,544);
			this.textSecDedFam.Name = "textSecDedFam";
			this.textSecDedFam.ReadOnly = true;
			this.textSecDedFam.Size = new System.Drawing.Size(60,20);
			this.textSecDedFam.TabIndex = 64;
			this.textSecDedFam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(736,547);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79,15);
			this.label2.TabIndex = 63;
			this.label2.Text = "Fam Ded";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ContrTreat
			// 
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textPriDedFam);
			this.Controls.Add(this.textSecDedFam);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listSetPr);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridPreAuth);
			this.Controls.Add(this.groupShow);
			this.Controls.Add(this.gridPlans);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textSecDedRem);
			this.Controls.Add(this.textPriDedRem);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.textSecPend);
			this.Controls.Add(this.textSecRem);
			this.Controls.Add(this.textPriDed);
			this.Controls.Add(this.textPriUsed);
			this.Controls.Add(this.textPriPend);
			this.Controls.Add(this.textPriRem);
			this.Controls.Add(this.textSecMax);
			this.Controls.Add(this.textSecDed);
			this.Controls.Add(this.textSecUsed);
			this.Controls.Add(this.textPriMax);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Name = "ContrTreat";
			this.Size = new System.Drawing.Size(939,708);
			this.groupShow.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		///<summary>Only called on startup, but after local data loaded from db.</summary>
		public void InitializeOnStartup() {
			if(InitializedOnStartup) {
				return;
			}
			InitializedOnStartup=true;
			checkShowCompleted.Checked=PrefC.GetBool(PrefName.TreatPlanShowCompleted);
			//checkShowIns.Checked=PrefC.GetBool(PrefName.TreatPlanShowIns");
			//checkShowDiscount.Checked=PrefC.GetBool(PrefName.TreatPlanShowDiscount");
			//showHidden=true;//shows hidden priorities
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
			{
				label1,
				groupShow,
				checkShowCompleted,
				checkShowIns,
				checkShowDiscount,
				checkShowMaxDed,
				checkShowFees,
				//checkShowStandard,
				checkShowSubtotals,
				checkShowTotals,
				label3,
				label10,
				label16,
				label11,
				label12,
				label18,
				label13,
				label15,
				label14,
				});
			LayoutToolBar();//redundant?
		}

		///<summary>Called every time local data is changed from any workstation.  Refreshes priority lists and lays out the toolbar.</summary>
		public void InitializeLocalData(){
			listSetPr.Items.Clear();
			listSetPr.Items.Add(Lan.g(this,"no priority"));
			for(int i=0;i<DefC.Short[(int)DefCat.TxPriorities].Length;i++){
				listSetPr.Items.Add(DefC.Short[(int)DefCat.TxPriorities][i].ItemName);
			}
			LayoutToolBar();
			if(PrefC.GetBool(PrefName.EasyHideInsurance)){
				checkShowIns.Visible=false;
				checkShowIns.Checked=false;
				checkShowDiscount.Visible=false;
				checkShowDiscount.Checked=false;
				checkShowMaxDed.Visible=false;
				//checkShowMaxDed.Checked=false;
			}
			else{
				checkShowIns.Visible=true;
				checkShowDiscount.Visible=true;
				checkShowMaxDed.Visible=true;
			}
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			//ODToolBarButton button;
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"PreAuthorization"),-1,"","PreAuth"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Update Fees"),1,"","Update"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Save TP"),3,"","Create"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print TP"),2,"","Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Email TP"),-1,"","Email"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Sign TP"),-1,"","Sign"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.TreatmentPlanModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		///<summary></summary>
		public void ModuleSelected(long patNum) {
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PatCur=null;
			InsPlanList=null;
			//Claims.List=null;
			//ClaimProcs.List=null;
			//from FillMain:
			ProcList=null;
			ProcListTP=null;
			//Procedures.HList=null;
			//Procedures.MissingTeeth=null;
		}

		private void RefreshModuleData(long patNum) {
			if(patNum!=0){
				FamCur=Patients.GetFamily(patNum);
				PatCur=FamCur.GetPatient(patNum);
				InsPlanList=InsPlans.Refresh(FamCur);
				PatPlanList=PatPlans.Refresh(patNum);
				BenefitList=Benefits.Refresh(PatPlanList);
				ClaimList=Claims.Refresh(PatCur.PatNum);
				HistList=ClaimProcs.GetHistList(PatCur.PatNum,BenefitList,PatPlanList,InsPlanList,DateTime.Today);
			}
		}

		private void RefreshModuleScreen(){
			//ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(PatCur!=null){
				gridMain.Enabled=true;
				groupShow.Enabled=true;
				listSetPr.Enabled=true;
				//panelSide.Enabled=true;
				ToolBarMain.Buttons["PreAuth"].Enabled=true;
				ToolBarMain.Buttons["Update"].Enabled=true;
				ToolBarMain.Buttons["Print"].Enabled=true;
				ToolBarMain.Buttons["Email"].Enabled=true;
				ToolBarMain.Invalidate();
				if(PatPlanList.Count==0){//patient doesn't have insurance
					checkShowIns.Checked=false;
					checkShowDiscount.Checked=false;
					checkShowMaxDed.Visible=false;
				}
				else{//patient has insurance
					if(!PrefC.GetBool(PrefName.EasyHideInsurance)){//if insurance isn't hidden
						checkShowMaxDed.Visible=true;
						if(checkShowFees.Checked){//if fees are showing
							checkShowIns.Checked=true;
							InsPlan plan=InsPlans.GetPlan(PatPlanList[0].PlanNum,InsPlanList);
							if(plan.PlanType=="p" || plan.PlanType=="c"){//ppo or cap
								checkShowDiscount.Checked=true;
							}
							else{
								checkShowDiscount.Checked=false;
							}
						}
					}
				}
			}
			else{
				gridMain.Enabled=false;
				groupShow.Enabled=false;
				listSetPr.Enabled=false;
				//panelSide.Enabled=false;
				ToolBarMain.Buttons["PreAuth"].Enabled=false;
				ToolBarMain.Buttons["Update"].Enabled=false;
				ToolBarMain.Buttons["Print"].Enabled=false;
				ToolBarMain.Buttons["Email"].Enabled=false;
				ToolBarMain.Invalidate();
        //listPreAuth.Enabled=false;
			}
			FillPlans();
			FillMain();
			FillSummary();
      FillPreAuth();
			//FillMisc();
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "PreAuth":
						OnPreAuth_Click();
						break;
					case "Update":
						OnUpdate_Click();
						break;
					case "Create":
						OnCreate_Click();
						break;
					case "Print":
						OnPrint_Click();
						break;
					case "Email":
						OnEmail_Click();
						break;
					case "Sign":
						OnSign_Click();
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

		private void FillPlans(){
			gridPlans.BeginUpdate();
			gridPlans.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTPList","Date"),80);
			gridPlans.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTPList","Heading"),310);
			gridPlans.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTPList","Signed"),80,HorizontalAlignment.Center);
			gridPlans.Columns.Add(col);
			gridPlans.Rows.Clear();
			if(PatCur==null){
				gridPlans.EndUpdate();
				return;
			}
			ProcList=Procedures.Refresh(PatCur.PatNum);
			ProcListTP=Procedures.GetListTP(ProcList);//sorted by priority, then toothnum
			PlanList=TreatPlans.Refresh(PatCur.PatNum);
			ProcTPList=ProcTPs.Refresh(PatCur.PatNum);
			OpenDental.UI.ODGridRow row;
			row=new ODGridRow();
			row.Cells.Add("");//date empty
			row.Cells.Add(Lan.g(this,"Default"));
			gridPlans.Rows.Add(row);
			string str;
			for(int i=0;i<PlanList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(PlanList[i].DateTP.ToShortDateString());
				str=PlanList[i].Heading;
				if(PlanList[i].ResponsParty!=0){
					str+="\r\n"+Lan.g(this,"Responsible Party: ")+Patients.GetLim(PlanList[i].ResponsParty).GetNameLF();
				}
				row.Cells.Add(str);
				if(PlanList[i].Signature==""){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add("X");
				}
				gridPlans.Rows.Add(row);
			}
			gridPlans.EndUpdate();
			gridPlans.SetSelected(0,true);
		}

		private void FillMain(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TableTP","Done"),50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTP","Priority"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTP","Tth"),40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTP","Surf"),45);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTP","Code"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTP","Description"),235);
			gridMain.Columns.Add(col);
			//caution: If you change the names of the columns here,
			//also be sure to change them in OnCreate_Click()
			if(checkShowFees.Checked){
				col=new ODGridColumn(Lan.g("TableTP","Fee"),50,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
			}
			if(checkShowIns.Checked){
				col=new ODGridColumn(Lan.g("TableTP","Pri Ins"),50,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g("TableTP","Sec Ins"),50,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
			}
			if(checkShowDiscount.Checked){
				col=new ODGridColumn(Lan.g("TableTP","Discount"),55,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
			}
			if(checkShowIns.Checked){
				col=new ODGridColumn(Lan.g("TableTP","Pat"),50,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			if(PatCur==null){
				gridMain.EndUpdate();
				return;
			}
			double fee;
			double priIns;
			double secIns;
			double discount;
			double pat;
			double subfee=0;
			double subpriIns=0;
			double subsecIns=0;
			double subdiscount=0;
			double subpat=0;
			double totFee=0;
			double totPriIns=0;
			double totSecIns=0;
			double totDiscount=0;
			double totPat=0;
			long feeSched=Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched;//for standard fee
			OpenDental.UI.ODGridRow row;
			#region currentTP
			if(gridPlans.SelectedIndices[0]==0){//current treatplan selected
				InsPlan	PriPlanCur=null;
				if(PatPlanList.Count>0) {//primary
					PriPlanCur=InsPlans.GetPlan(PatPlanList[0].PlanNum,InsPlanList);
				}
				InsPlan SecPlanCur=null;
				if(PatPlanList.Count>1) {//secondary
					SecPlanCur=InsPlans.GetPlan(PatPlanList[1].PlanNum,InsPlanList);
				}
				ClaimProc claimproc;//holds the estimate.
				string descript;
				//One thing to watch out for here is that we must be absolutely sure to include all claimprocs for the procedures listed,
				//regardless of status.  Needed for Procedures.ComputeEstimates.  This should be fine.
				ClaimProcList=ClaimProcs.RefreshForTP(PatCur.PatNum);
				List<ClaimProc> claimProcListOld=new List<ClaimProc>();//This didn't work:(ClaimProcList);//make a copy
				for(int i=0;i<ClaimProcList.Count;i++) {
					claimProcListOld.Add(ClaimProcList[i].Copy());
				}
				LoopList=new List<ClaimProcHist>();
				for(int i=0;i<ProcListTP.Length;i++){
					Procedures.ComputeEstimates(ProcListTP[i],PatCur.PatNum,ref ClaimProcList,false,InsPlanList,PatPlanList,BenefitList,
						HistList,LoopList,false,PatCur.Age);
					//then, add this information to loopList so that the next procedure is aware of it.
					LoopList.AddRange(ClaimProcs.GetHistForProc(ClaimProcList,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
				}
				//save changes in the list to the database
				ClaimProcs.Synch(ref ClaimProcList,claimProcListOld);
				//claimProcList=ClaimProcs.RefreshForTP(PatCur.PatNum);
				string estimateNote;
				for(int i=0;i<ProcListTP.Length;i++) {
					row=new ODGridRow();
					row.Cells.Add("");//never done
					row.Cells.Add(DefC.GetName(DefCat.TxPriorities,ProcListTP[i].Priority));
					row.Cells.Add(Tooth.ToInternat(ProcListTP[i].ToothNum));
					row.Cells.Add(ProcListTP[i].Surf);
					row.Cells.Add(ProcedureCodes.GetProcCode(ProcListTP[i].CodeNum).ProcCode);
					descript=ProcedureCodes.GetLaymanTerm(ProcListTP[i].CodeNum);
					if(ProcListTP[i].ToothRange!=""){
						descript+=" #"+Tooth.FormatRangeForDisplay(ProcListTP[i].ToothRange);
					}
					if(checkShowMaxDed.Checked) {
						estimateNote=ClaimProcs.GetEstimateNotes(ProcListTP[i].ProcNum,ClaimProcList);
						if(estimateNote!="") {
							descript+="\r\n"+estimateNote;
						}
					}
					row.Cells.Add(descript);
					fee=ProcListTP[i].ProcFee;
					int qty=ProcListTP[i].BaseUnits + ProcListTP[i].UnitQty;
					if(qty>0){
						fee*=qty;
					}
					subfee+=fee;
					totFee+=fee;
					#region ShowMaxDed
					if(PatPlanList.Count>0){//Primary
						claimproc=ClaimProcs.GetEstimate(ClaimProcList,ProcListTP[i].ProcNum,PriPlanCur.PlanNum);
						if(claimproc==null){
							priIns=0;
						}
						else{
							if(checkShowMaxDed.Checked) {//whether visible or not
								priIns=ClaimProcs.GetInsEstTotal(claimproc);
								double ded=ClaimProcs.GetDeductibleDisplay(claimproc);
								if(ded > 0) {
									row.Cells[5].Text+="\r\n"+Lan.g(this,"Pri Deduct Applied: ")+ded.ToString("c");
								}
							}
							else {
								priIns=claimproc.BaseEst;
							}
						}
					}
					else{//no primary ins
						priIns=0;
					}
					if(PatPlanList.Count>1) {//Secondary
						claimproc=ClaimProcs.GetEstimate(ClaimProcList,ProcListTP[i].ProcNum,SecPlanCur.PlanNum);
						if(claimproc==null){
							secIns=0;
						}
						else{
							if(checkShowMaxDed.Checked) {
								secIns=ClaimProcs.GetInsEstTotal(claimproc);
								double ded=ClaimProcs.GetDeductibleDisplay(claimproc);
								if(ded > 0) {
									row.Cells[5].Text+="\r\n"+Lan.g(this,"Sec Deduct Applied: ")+ded.ToString("c");
								}
							}
							else {
								secIns=claimproc.BaseEst;
							}
						}
					}//secondary
					else{//no secondary ins
						secIns=0;
					}
					#endregion ShowMaxDed
					subpriIns+=priIns;
					totPriIns+=priIns;
					subsecIns+=secIns;
					totSecIns+=secIns;
					discount=ClaimProcs.GetTotalWriteOffEstimateDisplay(ClaimProcList,ProcListTP[i].ProcNum);
					subdiscount+=discount;
					totDiscount+=discount;
					pat=fee-priIns-secIns-discount;
					if(pat<0){
						pat=0;
					}
					subpat+=pat;
					totPat+=pat;
					if(checkShowFees.Checked){
						row.Cells.Add(fee.ToString("F"));
					}
					if(checkShowIns.Checked){
						row.Cells.Add(priIns.ToString("F"));
						row.Cells.Add(secIns.ToString("F"));
					}
					if(checkShowDiscount.Checked){
						row.Cells.Add(discount.ToString("F"));
					}
					if(checkShowIns.Checked){	
						row.Cells.Add(pat.ToString("F"));
					}
					row.ColorText=DefC.GetColor(DefCat.TxPriorities,ProcListTP[i].Priority);
					if(row.ColorText==System.Drawing.Color.White){
						row.ColorText=System.Drawing.Color.Black;
					}
					row.Tag=ProcListTP[i].Copy();
					gridMain.Rows.Add(row);
					#region Canadian Lab
					/*
					if(ProcListTP[i].LabProcCode!=""){
						row=new ODGridRow();
						row.Cells.Add("");//done
						row.Cells.Add(DefB.GetName(DefCat.TxPriorities,ProcListTP[i].Priority));//priority
						row.Cells.Add(Tooth.ToInternat(ProcListTP[i].ToothNum));//toothnum
						row.Cells.Add("");//surf
						row.Cells.Add(ProcListTP[i].LabProcCode);//proccode
						row.Cells.Add("    "+ProcedureCodes.GetLaymanTerm(ProcListTP[i].LabProcCode));//descript (indented)
						if(checkShowStandard.Checked) {
							row.Cells.Add("");//standard
						}
						fee=ProcListTP[i].LabFee;
						subfee+=fee;
						totFee+=fee;
				//possibly incomplete. Insurance not considered
						pat=fee;//-priIns-secIns;
						if(pat<0) {
							pat=0;
						}
						subpat+=pat;
						totPat+=pat;
						if(checkShowFees.Checked) {
							row.Cells.Add(ProcListTP[i].LabFee.ToString("F"));//fee
						}
						if(checkShowIns.Checked) {
							row.Cells.Add("");//pri
							row.Cells.Add("");//sec
							row.Cells.Add("");//pat portion
						}
						row.ColorText=DefB.GetColor(DefCat.TxPriorities,ProcListTP[i].Priority);
						if(row.ColorText==Color.White) {
							row.ColorText=Color.Black;
						}
						row.Tag=ProcListTP[i].Copy();
						gridMain.Rows.Add(row);
					}*/
					#endregion Canadian Lab
					#region subtotal
					if(checkShowSubtotals.Checked &&
						(i==ProcListTP.Length-1 || ProcListTP[i+1].Priority != ProcListTP[i].Priority))
					{
						row=new ODGridRow();
						row.Cells.Add("");//done
						row.Cells.Add("");//priority
						row.Cells.Add("");//toothnum
						row.Cells.Add("");//surf
						row.Cells.Add("");//proccode
						row.Cells.Add(Lan.g(this,"Subtotal"));//descript
						if(checkShowFees.Checked){
							row.Cells.Add(subfee.ToString("F"));//fee
						}
						if(checkShowIns.Checked){
							row.Cells.Add(subpriIns.ToString("F"));//pri
							row.Cells.Add(subsecIns.ToString("F"));//sec
						}
						if(checkShowDiscount.Checked){
							row.Cells.Add(subdiscount.ToString("F"));//
						}
						if(checkShowIns.Checked){
							row.Cells.Add(subpat.ToString("F"));//pat portion
						}
						row.ColorText=DefC.GetColor(DefCat.TxPriorities,ProcListTP[i].Priority);
						if(row.ColorText==System.Drawing.Color.White) {
							row.ColorText=System.Drawing.Color.Black;
						}
						row.Bold=true;
						row.ColorLborder=System.Drawing.Color.Black;
						gridMain.Rows.Add(row);
						subfee=0;
						subpriIns=0;
						subsecIns=0;
						subdiscount=0;
						subpat=0;
					}
					#endregion subtotal
				}//for(int i=0;i<ProcListTP.Length
				textNote.Text=PrefC.GetString(PrefName.TreatmentPlanNote);
			}
			#endregion currentTP
			#region AnyTP except current
			else {//any except current tp selected
				ProcTPSelectList=ProcTPs.GetListForTP(PlanList[gridPlans.SelectedIndices[0]-1].TreatPlanNum,ProcTPList);
				bool isDone;
				for(int i=0;i<ProcTPSelectList.Length;i++){
					row=new ODGridRow();
					isDone=true;
					for(int j=0;j<ProcListTP.Length;j++) {
						if(ProcListTP[j].ProcNum==ProcTPSelectList[i].ProcNumOrig) {
							isDone=false;
						}
					}
					if(isDone){
						row.Cells.Add("X");
					}
					else {
						row.Cells.Add("");
					}	
					row.Cells.Add(DefC.GetName(DefCat.TxPriorities,ProcTPSelectList[i].Priority));
					row.Cells.Add(ProcTPSelectList[i].ToothNumTP);
					row.Cells.Add(ProcTPSelectList[i].Surf);
					row.Cells.Add(ProcTPSelectList[i].ProcCode);
					row.Cells.Add(ProcTPSelectList[i].Descript);
					//if(checkShowStandard.Checked) {
					//	standard=Fees.GetAmount0(ProcedureCodes.GetCodeNum(ProcTPSelectList[i].ProcCode),feeSched);
					//	row.Cells.Add(standard.ToString("F"));//standard
					//	substandard+=standard;
					//	totStandard+=standard;
					//}
					if(checkShowFees.Checked){
						row.Cells.Add(ProcTPSelectList[i].FeeAmt.ToString("F"));
						subfee+=ProcTPSelectList[i].FeeAmt;
						totFee+=ProcTPSelectList[i].FeeAmt;
					}
					if(checkShowIns.Checked){
						row.Cells.Add(ProcTPSelectList[i].PriInsAmt.ToString("F"));
						row.Cells.Add(ProcTPSelectList[i].SecInsAmt.ToString("F"));
						subpriIns+=ProcTPSelectList[i].PriInsAmt;
						totPriIns+=ProcTPSelectList[i].PriInsAmt;
						subsecIns+=ProcTPSelectList[i].SecInsAmt;
						totSecIns+=ProcTPSelectList[i].SecInsAmt;
					}
					if(checkShowDiscount.Checked){
						row.Cells.Add(ProcTPSelectList[i].Discount.ToString("F"));
						subdiscount+=ProcTPSelectList[i].Discount;
						totDiscount+=ProcTPSelectList[i].Discount;
					}
					if(checkShowIns.Checked){
						row.Cells.Add(ProcTPSelectList[i].PatAmt.ToString("F"));
						subpat+=ProcTPSelectList[i].PatAmt;
						totPat+=ProcTPSelectList[i].PatAmt;						
					}
					row.ColorText=DefC.GetColor(DefCat.TxPriorities,ProcTPSelectList[i].Priority);
					if(row.ColorText==System.Drawing.Color.White){
						row.ColorText=System.Drawing.Color.Black;
					}
					row.Tag=ProcTPSelectList[i].Copy();
					gridMain.Rows.Add(row);
					#region subtotal
					if(checkShowSubtotals.Checked &&
						(i==ProcTPSelectList.Length-1 || ProcTPSelectList[i+1].Priority != ProcTPSelectList[i].Priority)) {
						row=new ODGridRow();
						row.Cells.Add("");//done
						row.Cells.Add("");//priority
						row.Cells.Add("");//toothnum
						row.Cells.Add("");//surf
						row.Cells.Add("");//proccode
						row.Cells.Add(Lan.g(this,"Subtotal"));//descript
						//if(checkShowStandard.Checked) {
						//	row.Cells.Add(substandard.ToString("F"));//standard
						//}
						if(checkShowFees.Checked) {
							row.Cells.Add(subfee.ToString("F"));//fee
						}
						if(checkShowIns.Checked) {
							row.Cells.Add(subpriIns.ToString("F"));//pri
							row.Cells.Add(subsecIns.ToString("F"));//sec
						}
						if(checkShowDiscount.Checked){
							row.Cells.Add(subdiscount.ToString("F"));
						}
						if(checkShowIns.Checked) {
							
							row.Cells.Add(subpat.ToString("F"));//pat portion
						}
						row.ColorText=DefC.GetColor(DefCat.TxPriorities,ProcTPSelectList[i].Priority);
						if(row.ColorText==System.Drawing.Color.White) {
							row.ColorText=System.Drawing.Color.Black;
						}
						row.Bold=true;
						row.ColorLborder=System.Drawing.Color.Black;
						gridMain.Rows.Add(row);
						//substandard=0;
						subfee=0;
						subpriIns=0;
						subsecIns=0;
						subdiscount=0;
						subpat=0;
					}
					#endregion
				}
				textNote.Text=PlanList[gridPlans.SelectedIndices[0]-1].Note;
			}
			#endregion AnyTP except current
			#region Totals
			if(checkShowTotals.Checked) {
				row=new ODGridRow();
				row.Cells.Add("");//done
				row.Cells.Add("");//priority
				row.Cells.Add("");//toothnum
				row.Cells.Add("");//surf
				row.Cells.Add("");//proccode
				row.Cells.Add(Lan.g(this,"Total"));//descript
				//if(checkShowStandard.Checked) {
				//	row.Cells.Add(totStandard.ToString("F"));//standard
				//}
				if(checkShowFees.Checked) {
					row.Cells.Add(totFee.ToString("F"));//fee
				}
				if(checkShowIns.Checked) {
					row.Cells.Add(totPriIns.ToString("F"));//pri
					row.Cells.Add(totSecIns.ToString("F"));//sec
				}
				if(checkShowDiscount.Checked){
					row.Cells.Add(totDiscount.ToString("F"));
				}
				if(checkShowIns.Checked) {
					
					row.Cells.Add(totPat.ToString("F"));//pat portion
				}
				row.Bold=true;
				//row.ColorLborder=Color.Black;
				gridMain.Rows.Add(row);
			}
			#endregion Totals
			int gridW=0;
			for(int i=0;i<gridMain.Columns.Count;i++){
				gridW+=gridMain.Columns[i].ColWidth;
			}
			gridMain.Width=gridW+20;
			gridMain.EndUpdate();
		}

		private void FillSummary(){
			textPriMax.Text="";
			textPriDed.Text="";
			textPriDedFam.Text="";
			textPriDedRem.Text="";
			textPriUsed.Text="";
			textPriPend.Text="";
			textPriRem.Text="";
			textSecMax.Text="";
			textSecDed.Text="";
			textSecDedFam.Text="";
			textSecDedRem.Text="";
			textSecUsed.Text="";
			textSecPend.Text="";
			textSecRem.Text="";
			if(PatCur==null){
				return;
			}
			double max=0;
			double ded=0;
			double dedFam=0;
			double dedUsed=0;
			double remain=0;
			double pend=0;
			double used=0;
			InsPlan PlanCur;//=new InsPlan();
			if(PatPlanList.Count>0){
				PlanCur=InsPlans.GetPlan(PatPlanList[0].PlanNum,InsPlanList);
				pend=InsPlans.GetPendingDisplay(HistList,DateTime.Today,PlanCur,PatPlanList[0].PatPlanNum,-1,PatCur.PatNum);
				used=InsPlans.GetInsUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList);
				textPriPend.Text=pend.ToString("F");
				textPriUsed.Text=used.ToString("F");
				max=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum);
				if(max==-1){//if annual max is blank
					textPriMax.Text="";
					textPriRem.Text="";
				}
				else{
					remain=max-used-pend;
					if(remain<0){
						remain=0;
					}
					textPriMax.Text=max.ToString("F");
					textPriRem.Text=remain.ToString("F");
				}
				//deductible:
				ded=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,BenefitCoverageLevel.Individual);
				dedFam=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,BenefitCoverageLevel.Family);
				if(ded!=-1){
					textPriDed.Text=ded.ToString("F");
					dedUsed=InsPlans.GetDedUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,
						BenefitCoverageLevel.Individual,PatCur.PatNum);
					textPriDedRem.Text=(ded-dedUsed).ToString("F");
				}
				if(dedFam!=-1) {
					textPriDedFam.Text=dedFam.ToString("F");
				}
			}
			if(PatPlanList.Count>1){
				PlanCur=InsPlans.GetPlan(PatPlanList[1].PlanNum,InsPlanList);
				pend=InsPlans.GetPendingDisplay(HistList,DateTime.Today,PlanCur,PatPlanList[1].PatPlanNum,-1,PatCur.PatNum);
				textSecPend.Text=pend.ToString("F");
				used=InsPlans.GetInsUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList);
				textSecUsed.Text=used.ToString("F");
				max=Benefits.GetAnnualMaxDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				if(max==-1){
					textSecMax.Text="";
					textSecRem.Text="";
				}
				else{
					remain=max-used-pend;
					if(remain<0) {
						remain=0;
					}
					textSecMax.Text=max.ToString("F");
					textSecRem.Text=remain.ToString("F");
				}
				ded=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,BenefitCoverageLevel.Individual);
				dedFam=Benefits.GetDeductGeneralDisplay(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,BenefitCoverageLevel.Family);
				if(ded!=-1){
					textSecDed.Text=ded.ToString("F");
					dedUsed=InsPlans.GetDedUsedDisplay(HistList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,
						BenefitCoverageLevel.Individual,PatCur.PatNum);
					textSecDedRem.Text=(ded-dedUsed).ToString("F");
				}
				if(dedFam!=-1) {
					textSecDedFam.Text=dedFam.ToString("F");
				}
			}
		}		

    private void FillPreAuth(){
			gridPreAuth.BeginUpdate();
			gridPreAuth.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePreAuth","Date Sent"),80);
			gridPreAuth.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePreAuth","Carrier"),100);
			gridPreAuth.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePreAuth","Status"),53);
			gridPreAuth.Columns.Add(col);
			gridPreAuth.Rows.Clear();
      if(PatCur==null){
				gridPreAuth.EndUpdate();
				return;
			}
      ALPreAuth=new ArrayList();			
      for(int i=0;i<ClaimList.Count;i++){
        if(ClaimList[i].ClaimType=="PreAuth"){
          ALPreAuth.Add(ClaimList[i]);
        }
      }
			OpenDental.UI.ODGridRow row;
      for(int i=0;i<ALPreAuth.Count;i++){
				InsPlan PlanCur=InsPlans.GetPlan(((Claim)ALPreAuth[i]).PlanNum,InsPlanList);
				row=new ODGridRow();
				if(((Claim)ALPreAuth[i]).DateSent.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(((Claim)ALPreAuth[i]).DateSent.ToShortDateString());
				}
				row.Cells.Add(Carriers.GetName(PlanCur.CarrierNum));
				row.Cells.Add(((Claim)ALPreAuth[i]).ClaimStatus.ToString());
				gridPreAuth.Rows.Add(row);
      }
			gridPreAuth.EndUpdate();
    }

		private void gridMain_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			gridPreAuth.SetSelected(false);//is this a desirable behavior?
		}

		private void gridMain_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(gridMain.Rows[e.Row].Tag==null){
				return;//user double clicked on a subtotal row
			}
			if(gridPlans.SelectedIndices[0]==0){//current plan
				Procedure ProcCur=Procedures.GetOneProc(((Procedure)gridMain.Rows[e.Row].Tag).ProcNum,true); 
				//generate a new loop list containing only the procs before this one in it
				LoopList=new List<ClaimProcHist>();
				for(int i=0;i<ProcListTP.Length;i++) {
					if(ProcListTP[i].ProcNum==ProcCur.ProcNum) {
						break;
					}
					LoopList.AddRange(ClaimProcs.GetHistForProc(ClaimProcList,ProcListTP[i].ProcNum,ProcListTP[i].CodeNum));
				}
				FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur,FamCur);
				FormPE.LoopList=LoopList;
				FormPE.HistList=HistList;
				FormPE.ShowDialog();
				ModuleSelected(PatCur.PatNum);
				for(int i=0;i<gridMain.Rows.Count;i++){
					if(gridMain.Rows[i].Tag !=null && ((Procedure)gridMain.Rows[i].Tag).ProcNum==ProcCur.ProcNum){
						gridMain.SetSelected(i,true);
					}
				}
				return;
			}
			//any other TP
			ProcTP procT=(ProcTP)gridMain.Rows[e.Row].Tag;
			DateTime dateTP=PlanList[gridPlans.SelectedIndices[0]-1].DateTP;
			FormProcTPEdit FormP=new FormProcTPEdit(procT,dateTP);
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				return;
			}
			int selectedPlanI=gridPlans.SelectedIndices[0];
			long selectedProc=procT.ProcTPNum;
			ModuleSelected(PatCur.PatNum);
			gridPlans.SetSelected(selectedPlanI,true);
			FillMain();
			for(int i=0;i<gridMain.Rows.Count;i++){
				if(gridMain.Rows[i].Tag !=null && ((ProcTP)gridMain.Rows[i].Tag).ProcTPNum==selectedProc){ 
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void gridPlans_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FillMain();
			gridPreAuth.SetSelected(false);
		}

		private void gridPlans_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(e.Row==0){
				return;//there is nothing to edit if user clicks on current.
			}
			long tpNum=PlanList[e.Row-1].TreatPlanNum;
			FormTreatPlanEdit FormT=new FormTreatPlanEdit(PlanList[e.Row-1]);
			FormT.ShowDialog();
			ModuleSelected(PatCur.PatNum);
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].TreatPlanNum==tpNum){
					gridPlans.SetSelected(i+1,true);
				}
			}
			FillMain();
		}

		private void listSetPr_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			int clickedRow=listSetPr.IndexFromPoint(e.X,e.Y);
			if(clickedRow==-1)
				return;
			if(gridPlans.SelectedIndices[0]==0){//current TP
				//Procedure ProcCur;
				//Procedure ProcOld;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){//loop through the main list of selected procs
					if(gridMain.Rows[gridMain.SelectedIndices[i]].Tag==null){
						//user must have highlighted a subtotal row, so ignore
						continue;
					}
					//ProcCur=(Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag;
					//ProcOld=ProcCur.Copy();
					if(clickedRow==0){//set priority to "no priority"
						//ProcCur.Priority=0;
						Procedures.UpdatePriority(((Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ProcNum,0);
					}
					else{
						//ProcCur.Priority=DefC.Short[(int)DefCat.TxPriorities][clickedRow-1].DefNum;
						Procedures.UpdatePriority(((Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ProcNum,
							DefC.Short[(int)DefCat.TxPriorities][clickedRow-1].DefNum);
					}
					//Procedures.Update(ProcCur,ProcOld);//no recall synch required
				}
				ModuleSelected(PatCur.PatNum);
			}
			else{//any other TP
				int selectedTP=gridPlans.SelectedIndices[0];
				ProcTP proc;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){//loop through the main list of selected procTPs
					if(gridMain.Rows[gridMain.SelectedIndices[i]].Tag==null) {
						//user must have highlighted a subtotal row, so ignore
						continue;
					}
					proc=(ProcTP)gridMain.Rows[gridMain.SelectedIndices[i]].Tag;
					if(clickedRow==0)//set priority to "no priority"
						proc.Priority=0;
					else
						proc.Priority=DefC.Short[(int)DefCat.TxPriorities][clickedRow-1].DefNum;
					ProcTPs.InsertOrUpdate(proc,false);
				}
				ModuleSelected(PatCur.PatNum);
				gridPlans.SetSelected(selectedTP,true);
				FillMain();
			}
		}

		private void checkShowMaxDed_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void checkShowFees_Click(object sender,EventArgs e) {
			if(checkShowFees.Checked){
				//checkShowStandard.Checked=true;
				checkShowIns.Checked=true;
				//if(PrefC.GetBool(PrefName.TreatPlanShowDiscount")){
				checkShowDiscount.Checked=true;
				//}
				checkShowSubtotals.Checked=true;
				checkShowTotals.Checked=true;
			}
			else{
				//checkShowStandard.Checked=false;
				checkShowIns.Checked=false;
				checkShowDiscount.Checked=false;
				checkShowSubtotals.Checked=false;
				checkShowTotals.Checked=false;
			}
			FillMain();
		}

		private void checkShowStandard_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void checkShowIns_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void checkShowDiscount_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void checkShowSubtotals_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void checkShowTotals_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void OnPrint_Click() {
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				if(checkShowDiscount.Checked || checkShowIns.Checked) {
					if(MessageBox.Show(this,string.Format(Lan.g(this,"Do you want to remove insurance estimates and PPO discounts from printed treatment plan?")),"Open Dental",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.No) {
						checkShowDiscount.Checked=false;
						checkShowIns.Checked=false;
						FillMain();
					}
				}
			}
			PrepImageForPrinting();
			MigraDoc.DocumentObjectModel.Document doc=CreateDocument();
			MigraDoc.Rendering.Printing.MigraDocPrintDocument printdoc=new MigraDoc.Rendering.Printing.MigraDocPrintDocument();
			MigraDoc.Rendering.DocumentRenderer renderer=new MigraDoc.Rendering.DocumentRenderer(doc);
			renderer.PrepareDocument();
			printdoc.Renderer=renderer;
			//we might want to surround some of this with a try-catch
			#if DEBUG
				pView = new FormRpPrintPreview();
				pView.printPreviewControl2.Document=printdoc;
				pView.ShowDialog();			
			#else
				if(PrinterL.SetPrinter(pd2,PrintSituation.TPPerio)){
					printdoc.PrinterSettings=pd2.PrinterSettings;
					printdoc.Print();
				}
			#endif
		}

		private void OnEmail_Click() {
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				if(checkShowDiscount.Checked || checkShowIns.Checked) {
					if(MessageBox.Show(this,string.Format(Lan.g(this,"Do you want to remove insurance estimates and PPO discounts from e-mailed treatment plan?")),"Open Dental",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.No) {
						checkShowDiscount.Checked=false;
						checkShowIns.Checked=false;
						FillMain();
					}
				}
			}
			PrepImageForPrinting();
			string attachPath=FormEmailMessageEdit.GetAttachPath();
			Random rnd=new Random();
			string fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
			string filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
			MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer=new MigraDoc.Rendering.PdfDocumentRenderer(true,PdfFontEmbedding.Always);
			pdfRenderer.Document=CreateDocument();
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(filePathAndName);
			//Process.Start(filePathAndName);
			EmailMessage message=new EmailMessage();
			message.PatNum=PatCur.PatNum;
			message.ToAddress=PatCur.Email;
			message.FromAddress=PrefC.GetString(PrefName.EmailSenderAddress);
			message.Subject=Lan.g(this,"Treatment Plan");
			EmailAttach attach=new EmailAttach();
			attach.DisplayedFileName="TreatmentPlan.pdf";
			attach.ActualFileName=fileName;
			message.Attachments.Add(attach);
			FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
			FormE.IsNew=true;
			FormE.ShowDialog();
			//if(FormE.DialogResult==DialogResult.OK) {
			//	RefreshCurrentModule();
			//}
		}

		private void PrepImageForPrinting(){
			//linesPrinted=0;
			ColTotal = new double[10];
			//headingPrinted=false;
			//graphicsPrinted=false;
			//mainPrinted=false;
			//benefitsPrinted=false;
			//notePrinted=false;
			//pagesPrinted=0;
			if(PrefC.GetBool(PrefName.TreatPlanShowGraphics)){
				//prints the graphical tooth chart and legend
				//Panel panelHide=new Panel();
				//panelHide.Size=new Size(600,500);
				//panelHide.BackColor=this.BackColor;
				//panelHide.SendToBack();
				//this.Controls.Add(panelHide);
				toothChart=new ToothChartWrapper();
				toothChart.ColorBackground=DefC.Long[(int)DefCat.ChartGraphicColors][14].ItemColor;
				toothChart.ColorText=DefC.Long[(int)DefCat.ChartGraphicColors][15].ItemColor;
				//toothChart.TaoRenderEnabled=true;
				//toothChart.TaoInitializeContexts();
				toothChart.Size=new Size(500,370);
				//toothChart.Location=new Point(-600,-500);//off the visible screen
				//toothChart.SendToBack();
				ComputerPref computerPref=ComputerPrefs.GetForLocalComputer();
				toothChart.UseHardware=computerPref.GraphicsUseHardware;
				toothChart.PreferredPixelFormatNumber=computerPref.PreferredPixelFormatNum;
				toothChart.DeviceFormat=new ToothChartDirectX.DirectXDeviceFormat(computerPref.DirectXFormat);
				//Must be last setting set for preferences, because
																														//this is the line where the device pixel format is
																														//recreated.
																														//The preferred pixel format number changes to the selected pixel format number after a context is chosen.
				toothChart.DrawMode=computerPref.GraphicsSimple;
				computerPref.PreferredPixelFormatNum=toothChart.PreferredPixelFormatNumber;
				ComputerPrefs.Update(computerPref);
				this.Controls.Add(toothChart);
				toothChart.BringToFront();
				toothChart.ResetTeeth();
				ToothInitialList=ToothInitials.Refresh(PatCur.PatNum);
				//first, primary.  That way, you can still set a primary tooth missing afterwards.
				for(int i=0;i<ToothInitialList.Count;i++) {
					if(ToothInitialList[i].InitialType==ToothInitialType.Primary) {
						toothChart.SetPrimary(ToothInitialList[i].ToothNum);
					}
				}
				for(int i=0;i<ToothInitialList.Count;i++) {
					switch(ToothInitialList[i].InitialType) {
						case ToothInitialType.Missing:
							toothChart.SetMissing(ToothInitialList[i].ToothNum);
							break;
						case ToothInitialType.Hidden:
							toothChart.SetHidden(ToothInitialList[i].ToothNum);
							break;
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
				ComputeProcAL();
				DrawProcsOfStatus(ProcStat.EO);
				DrawProcsOfStatus(ProcStat.EC);
				DrawProcsOfStatus(ProcStat.C);
				DrawProcsOfStatus(ProcStat.R);
				DrawProcsOfStatus(ProcStat.TP);
				toothChart.AutoFinish=true;
				chartBitmap=toothChart.GetBitmap();
				toothChart.Dispose();
			}
		}

		private MigraDoc.DocumentObjectModel.Document CreateDocument(){
			MigraDoc.DocumentObjectModel.Document doc= new MigraDoc.DocumentObjectModel.Document();
			doc.DefaultPageSetup.PageWidth=Unit.FromInch(8.5);
			doc.DefaultPageSetup.PageHeight=Unit.FromInch(11);
			doc.DefaultPageSetup.TopMargin=Unit.FromInch(.5);
			doc.DefaultPageSetup.LeftMargin=Unit.FromInch(.5);
			doc.DefaultPageSetup.RightMargin=Unit.FromInch(.5);
			MigraDoc.DocumentObjectModel.Section section=doc.AddSection();
			string text;
			MigraDoc.DocumentObjectModel.Font headingFont=MigraDocHelper.CreateFont(13,true);
			MigraDoc.DocumentObjectModel.Font bodyFontx=MigraDocHelper.CreateFont(9,false);
			MigraDoc.DocumentObjectModel.Font nameFontx=MigraDocHelper.CreateFont(9,true);
			MigraDoc.DocumentObjectModel.Font totalFontx=MigraDocHelper.CreateFont(9,true);
			//Heading---------------------------------------------------------------------------------------------------------------
			#region printHeading
			Paragraph par=section.AddParagraph();
			ParagraphFormat parformat=new ParagraphFormat();
			parformat.Alignment=ParagraphAlignment.Center;
			parformat.Font=MigraDocHelper.CreateFont(10,true);
			par.Format=parformat;
			if(gridPlans.SelectedIndices[0]==0) {//current TP
				text=Lan.g(this,"Proposed Treatment Plan");
			}
			else {
				text=PlanList[gridPlans.SelectedIndices[0]-1].Heading;
			}
			par.AddFormattedText(text,headingFont);
			par.AddLineBreak();
			if(PatCur.ClinicNum==0) {
				text=PrefC.GetString(PrefName.PracticeTitle);
				par.AddText(text);
				par.AddLineBreak();
				text=PrefC.GetString(PrefName.PracticePhone);
			}
			else {
				Clinic clinic=Clinics.GetClinic(PatCur.ClinicNum);
				text=clinic.Description;
				par.AddText(text);
				par.AddLineBreak();
				text=clinic.Phone;
			}
			if(text.Length==10 && Application.CurrentCulture.Name=="en-US") {
				text="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
			}
			par.AddText(text);
			par.AddLineBreak();
			text=PatCur.GetNameFL();
			par.AddText(text);
			par.AddLineBreak();
			if(gridPlans.SelectedIndices[0]>0){//not the default plan
				if(PlanList[gridPlans.SelectedIndices[0]-1].ResponsParty!=0){
					text=Lan.g(this,"Responsible Party: ")
						+Patients.GetLim(PlanList[gridPlans.SelectedIndices[0]-1].ResponsParty).GetNameFL();
					par.AddText(text);
					par.AddLineBreak();
				}
			}
			if(gridPlans.SelectedIndices[0]==0) {//default TP
				text=DateTime.Today.ToShortDateString();
			}
			else {
				text=PlanList[gridPlans.SelectedIndices[0]-1].DateTP.ToShortDateString();
			}
			par.AddText(text);
			#endregion
			//Graphics---------------------------------------------------------------------------------------------------------------
			#region PrintGraphics
			TextFrame frame;
			int widthDoc=MigraDocHelper.GetDocWidth();
			if(PrefC.GetBool(PrefName.TreatPlanShowGraphics)) {	
				frame=MigraDocHelper.CreateContainer(section);
				MigraDocHelper.DrawString(frame,Lan.g(this,"Your")+"\r\n"+Lan.g(this,"Right"),bodyFontx,
					new RectangleF(widthDoc/2-toothChart.Width/2-50,toothChart.Height/2-10,50,100));
				MigraDocHelper.DrawBitmap(frame,chartBitmap,widthDoc/2-toothChart.Width/2,0);
				MigraDocHelper.DrawString(frame,Lan.g(this,"Your")+"\r\n"+Lan.g(this,"Left"),bodyFontx,
					new RectangleF(widthDoc/2+toothChart.Width/2+17,toothChart.Height/2-10,50,100));
				if(checkShowCompleted.Checked) {
					float yPos=toothChart.Height+15;
					float xPos=225;
					MigraDocHelper.FillRectangle(frame,DefC.Short[(int)DefCat.ChartGraphicColors][3].ItemColor,xPos,yPos,14,14);
					xPos+=16;
					MigraDocHelper.DrawString(frame,Lan.g(this,"Existing"),bodyFontx,xPos,yPos);
					Graphics g=this.CreateGraphics();//for measuring strings.
					xPos+=(int)g.MeasureString(Lan.g(this,"Existing"),bodyFont).Width+23;
					//The Complete work is actually a combination of EC and C. Usually same color.
					//But just in case they are different, this will show it.
					MigraDocHelper.FillRectangle(frame,DefC.Short[(int)DefCat.ChartGraphicColors][2].ItemColor,xPos,yPos,7,14);
					xPos+=7;
					MigraDocHelper.FillRectangle(frame,DefC.Short[(int)DefCat.ChartGraphicColors][1].ItemColor,xPos,yPos,7,14);
					xPos+=9;
					MigraDocHelper.DrawString(frame,Lan.g(this,"Complete"),bodyFontx,xPos,yPos);
					xPos+=(int)g.MeasureString(Lan.g(this,"Complete"),bodyFont).Width+23;
					MigraDocHelper.FillRectangle(frame,DefC.Short[(int)DefCat.ChartGraphicColors][4].ItemColor,xPos,yPos,14,14);
					xPos+=16;
					MigraDocHelper.DrawString(frame,Lan.g(this,"Referred Out"),bodyFontx,xPos,yPos);
					xPos+=(int)g.MeasureString(Lan.g(this,"Referred Out"),bodyFont).Width+23;
					MigraDocHelper.FillRectangle(frame,DefC.Short[(int)DefCat.ChartGraphicColors][0].ItemColor,xPos,yPos,14,14);
					xPos+=16;
					MigraDocHelper.DrawString(frame,Lan.g(this,"Treatment Planned"),bodyFontx,xPos,yPos);
					g.Dispose();
				}
			}	
			#endregion
			MigraDocHelper.InsertSpacer(section,10);
			MigraDocHelper.DrawGrid(section,gridMain);
			//Print benefits----------------------------------------------------------------------------------------------------
			#region printBenefits
			if(checkShowIns.Checked) {
				ODGrid gridIns=new ODGrid();
				this.Controls.Add(gridIns);
				gridIns.BeginUpdate();
				gridIns.Columns.Clear();
				ODGridColumn col=new ODGridColumn("",140);
				gridIns.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Primary"),70,HorizontalAlignment.Right);
				gridIns.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Secondary"),70,HorizontalAlignment.Right);
				gridIns.Columns.Add(col);
				gridIns.Rows.Clear();
				ODGridRow row;
				//Annual max--------------------------
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,"Annual Maximum"));
				row.Cells.Add(textPriMax.Text);
				row.Cells.Add(textSecMax.Text);
				gridIns.Rows.Add(row);
				//Deductible--------------------------
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,"Deductible"));
				row.Cells.Add(textPriDed.Text);
				row.Cells.Add(textSecDed.Text);
				gridIns.Rows.Add(row);
				//Deductible Remaining--------------------------
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,"Deductible Remaining"));
				row.Cells.Add(textPriDedRem.Text);
				row.Cells.Add(textSecDedRem.Text);
				gridIns.Rows.Add(row);
				//Insurance Used--------------------------
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,"Insurance Used"));
				row.Cells.Add(textPriUsed.Text);
				row.Cells.Add(textSecUsed.Text);
				gridIns.Rows.Add(row);
				//Pending--------------------------
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,"Pending"));
				row.Cells.Add(textPriPend.Text);
				row.Cells.Add(textSecPend.Text);
				gridIns.Rows.Add(row);
				//Remaining--------------------------
				row=new ODGridRow();
				row.Cells.Add(Lan.g(this,"Remaining"));
				row.Cells.Add(textPriRem.Text);
				row.Cells.Add(textSecRem.Text);
				gridIns.Rows.Add(row);
				gridIns.EndUpdate();
				MigraDocHelper.InsertSpacer(section,15);
				par=section.AddParagraph();
				par.Format.Alignment=ParagraphAlignment.Center;
				par.AddFormattedText(Lan.g(this,"Dental Insurance Benefits"),totalFontx);
				MigraDocHelper.InsertSpacer(section,2);
				MigraDocHelper.DrawGrid(section,gridIns);
				gridIns.Dispose();
			}
			#endregion
			//Note------------------------------------------------------------------------------------------------------------
			#region printNote
			string note="";
			if(gridPlans.SelectedIndices[0]==0) {//current TP
				note=PrefC.GetString(PrefName.TreatmentPlanNote);
			}
			else {
				note=PlanList[gridPlans.SelectedIndices[0]-1].Note;
			}
			MigraDocHelper.InsertSpacer(section,20);
			par=section.AddParagraph(note);
			par.Format.Font=bodyFontx;
			par.Format.Borders.Color=Colors.Gray;
			par.Format.Borders.DistanceFromLeft=Unit.FromInch(.05);
			par.Format.Borders.DistanceFromRight=Unit.FromInch(.05);
			par.Format.Borders.DistanceFromTop=Unit.FromInch(.05);
			par.Format.Borders.DistanceFromBottom=Unit.FromInch(.05);
			#endregion
			//Signature-----------------------------------------------------------------------------------------------------------
			#region signature
			if(gridPlans.SelectedIndices[0]!=0//can't be default TP
				&& PlanList[gridPlans.SelectedIndices[0]-1].Signature!="")
			{
				System.Drawing.Bitmap sigBitmap=null;
				List<ProcTP> proctpList=ProcTPs.RefreshForTP(PlanList[gridPlans.SelectedIndices[0]-1].TreatPlanNum);
				if(PlanList[gridPlans.SelectedIndices[0]-1].SigIsTopaz){
					Control sigBoxTopaz=CodeBase.TopazWrapper.GetTopaz();
					sigBoxTopaz.Size=new System.Drawing.Size(362,79);
					Controls.Add(sigBoxTopaz);
					CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);					
					string keystring=TreatPlans.GetHashString(PlanList[gridPlans.SelectedIndices[0]-1],proctpList);
					CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,keystring);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);//high encryption
					CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);//high compression
					CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,PlanList[gridPlans.SelectedIndices[0]-1].Signature);
					sigBoxTopaz.Refresh();
					sigBitmap=new Bitmap(362,79);
					sigBoxTopaz.DrawToBitmap(sigBitmap,new Rectangle(0,0,362,79));//GetBitmap would probably work.
					Controls.Remove(sigBoxTopaz);
					sigBoxTopaz.Dispose();
				}
				else{
					SignatureBox sigBox=new SignatureBox();
					sigBox.Size=new System.Drawing.Size(362,79);
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(TreatPlans.GetHashString(PlanList[gridPlans.SelectedIndices[0]-1],proctpList));
					//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(PlanList[gridPlans.SelectedIndices[0]-1].Signature);
					//if(sigBox.NumberOfTabletPoints()==0) {
					//	labelInvalidSig.Visible=true;
					//}
					//sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
					sigBitmap=(Bitmap)sigBox.GetSigImage(true);
				}
				if(sigBitmap!=null){
					frame=MigraDocHelper.CreateContainer(section);
					MigraDocHelper.DrawBitmap(frame,sigBitmap,widthDoc/2-sigBitmap.Width/2,20);
				}
			}
			#endregion
			return doc;
		}

		///<summary>Just used for printing the 3D chart.</summary>
		private void ComputeProcAL() {
			ProcAL=new ArrayList();
			//first, add all completed work. C,EC,EO, and Referred
			for(int i=0;i<ProcList.Count;i++) {
				if(ProcList[i].ProcStatus==ProcStat.C
					|| ProcList[i].ProcStatus==ProcStat.EC
					|| ProcList[i].ProcStatus==ProcStat.EO)
				{
					if(checkShowCompleted.Checked){
						ProcAL.Add(ProcList[i]);
					}
				}
				if(ProcList[i].ProcStatus==ProcStat.R){//always show all referred
					ProcAL.Add(ProcList[i]);
				}
			}
			//then add whatever is showing on the selected TP
			if(gridPlans.SelectedIndices[0]==0) {//current plan
				for(int i=0;i<ProcListTP.Length;i++) {
					ProcAL.Add(ProcListTP[i]);
				}
			}
			else {
				Procedure procDummy;//not a real procedure.  Just used to help display on graphical chart
				for(int i=0;i<ProcTPSelectList.Length;i++) {
					procDummy=new Procedure();
					//this next loop is a way to get missing fields like tooth range.  Could be improved.
					for(int j=0;j<ProcList.Count;j++) {
						if(ProcList[j].ProcNum==ProcTPSelectList[i].ProcNumOrig) {
							//but remember that even if the procedure is found, Status might have been altered
							procDummy=ProcList[j].Copy();
						}
					}
					if(Tooth.IsValidEntry(ProcTPSelectList[i].ToothNumTP)) {
						procDummy.ToothNum=Tooth.FromInternat(ProcTPSelectList[i].ToothNumTP);
					}
					procDummy.Surf=ProcTPSelectList[i].Surf;
					if(procDummy.ToothRange==null){
						procDummy.ToothRange="";
					}
					//procDummy.HideGraphical??
					procDummy.ProcStatus=ProcStat.TP;
					procDummy.CodeNum=ProcedureCodes.GetProcCode(ProcTPSelectList[i].ProcCode).CodeNum;
					ProcAL.Add(procDummy);
				}
			}
		}

		private void DrawProcsOfStatus(ProcStat procStat) {
			Procedure proc;
			string[] teeth;
			System.Drawing.Color cLight=System.Drawing.Color.White;
			System.Drawing.Color cDark=System.Drawing.Color.White;
			for(int i=0;i<ProcAL.Count;i++) {
				proc=(Procedure)ProcAL[i];
				if(proc.ProcStatus!=procStat) {
					continue;
				}
				//if(proc.HideGraphical) {
					//We don't care about HideGraphical anymore.  It will be enhanced later to a 3-state.
					//continue;
				//}
				if(ProcedureCodes.GetProcCode(proc.CodeNum).PaintType==ToothPaintingType.Extraction && (
					proc.ProcStatus==ProcStat.C
					|| proc.ProcStatus==ProcStat.EC
					|| proc.ProcStatus==ProcStat.EO
					)) {
					continue;//prevents the red X. Missing teeth already handled.
				}
				if(ProcedureCodes.GetProcCode(proc.CodeNum).GraphicColor==System.Drawing.Color.FromArgb(0)) {
					switch(proc.ProcStatus) {
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
					}
				}
				else {
					cDark=ProcedureCodes.GetProcCode(proc.CodeNum).GraphicColor;
					cLight=ProcedureCodes.GetProcCode(proc.CodeNum).GraphicColor;
				}
				switch(ProcedureCodes.GetProcCode(proc.CodeNum).PaintType) {
					case ToothPaintingType.BridgeDark:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,proc.ToothNum)) {
							toothChart.SetPontic(proc.ToothNum,cDark);
						}
						else {
							toothChart.SetCrown(proc.ToothNum,cDark);
						}
						break;
					case ToothPaintingType.BridgeLight:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,proc.ToothNum)) {
							toothChart.SetPontic(proc.ToothNum,cLight);
						}
						else {
							toothChart.SetCrown(proc.ToothNum,cLight);
						}
						break;
					case ToothPaintingType.CrownDark:
						toothChart.SetCrown(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.CrownLight:
						toothChart.SetCrown(proc.ToothNum,cLight);
						break;
					case ToothPaintingType.DentureDark:
						if(proc.Surf=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(proc.Surf=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=proc.ToothRange.Split(new char[] { ',' });
						}
						for(int t=0;t<teeth.Length;t++) {
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cDark);
							}
							else {
								toothChart.SetCrown(teeth[t],cDark);
							}
						}
						break;
					case ToothPaintingType.DentureLight:
						if(proc.Surf=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(proc.Surf=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=proc.ToothRange.Split(new char[] { ',' });
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
						toothChart.SetBigX(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.FillingDark:
						toothChart.SetSurfaceColors(proc.ToothNum,proc.Surf,cDark);
						break;
					case ToothPaintingType.FillingLight:
						toothChart.SetSurfaceColors(proc.ToothNum,proc.Surf,cLight);
						break;
					case ToothPaintingType.Implant:
						toothChart.SetImplant(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.PostBU:
						toothChart.SetBU(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.RCT:
						toothChart.SetRCT(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.Sealant:
						toothChart.SetSealant(proc.ToothNum,cDark);
						break;
				}
			}
		}

		private void OnUpdate_Click() {
			if(gridPlans.SelectedIndices[0]!=0){
				MsgBox.Show(this,"The update fee utility only works on the current treatment plan, not any saved plans.");
				return;
			}
		  if(!MsgBox.Show(this,true,"Update all fees and insurance estimates on this treatment plan to the current fees for this patient?")){
        return;   
      }
			Procedure procCur;
			//Procedure procOld
			//Find the primary plan------------------------------------------------------------------
			long priPlanNum=PatPlans.GetPlanNum(PatPlanList,1);
			InsPlan priplan=InsPlans.GetPlan(priPlanNum,InsPlanList);//can handle a plannum=0
			double standardfee;
			double insfee;
			List<ClaimProc> claimProcList=ClaimProcs.RefreshForTP(PatCur.PatNum);
      for(int i=0;i<ProcListTP.Length;i++){
				procCur=ProcListTP[i];
				//procOld=procCur.Copy();
				//first the fees
  
				//check code to see if it is a medical code
				bool isMed=false;
				if(procCur.MedicalCode != null && procCur.MedicalCode != "") {
					isMed=true;
				}
				//get fee schedule for medical ins or Fees.GetFeeSched if dental
				long feeSch;
				if(isMed){
					feeSch=Fees.GetMedFeeSched(PatCur,InsPlanList,PatPlanList);
				} 
				else{
					feeSch=Fees.GetFeeSched(PatCur,InsPlanList,PatPlanList);
				}
				insfee=Fees.GetAmount0(procCur.CodeNum,feeSch);
				if(priplan!=null && priplan.PlanType=="p" && !isMed) {//PPO
					standardfee=Fees.GetAmount0(procCur.CodeNum,Providers.GetProv(Patients.GetProvNum(PatCur)).FeeSched);
					if(standardfee>insfee) {
						procCur.ProcFee=standardfee;
					}
					else {
						procCur.ProcFee=insfee;
					}
				}
				else {
					procCur.ProcFee=insfee;
				}
				Procedures.ComputeEstimates(procCur,PatCur.PatNum,claimProcList,false,InsPlanList,PatPlanList,BenefitList,PatCur.Age);
				Procedures.UpdateFee(procCur.ProcNum,procCur.ProcFee);
				//Procedures.Update(procCur,procOld);//no recall synch required 
      }
      ModuleSelected(PatCur.PatNum);
		}

		private void OnCreate_Click(){
			if(gridPlans.SelectedIndices[0]!=0){
				MsgBox.Show(this,"The default TP must be selected before saving a TP.  You can highlight some procedures in the default TP to save a TP with only those procedures in it.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				gridMain.SetSelected(true);
			}
			TreatPlan tp=new TreatPlan();
			tp.Heading=Lan.g(this,"Proposed Treatment Plan");
			tp.DateTP=DateTime.Today;
			tp.PatNum=PatCur.PatNum;
			tp.Note=PrefC.GetString(PrefName.TreatmentPlanNote);
			tp.ResponsParty=PatCur.ResponsParty;
			TreatPlans.Insert(tp);
			ProcTP procTP;
			Procedure proc;
			int itemNo=0;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(gridMain.Rows[gridMain.SelectedIndices[i]].Tag==null){
					//user must have highlighted a subtotal row.
					continue;
				}
				proc=(Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag;
				procTP=new ProcTP();
				procTP.TreatPlanNum=tp.TreatPlanNum;
				procTP.PatNum=PatCur.PatNum;
				procTP.ProcNumOrig=proc.ProcNum;
				procTP.ItemOrder=itemNo;
				procTP.Priority=proc.Priority;
				procTP.ToothNumTP=Tooth.ToInternat(proc.ToothNum);
				procTP.Surf=proc.Surf;
				procTP.ProcCode=ProcedureCodes.GetStringProcCode(proc.CodeNum);
				procTP.Descript=gridMain.Rows[gridMain.SelectedIndices[i]]
					.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Description"))].Text;
				if(checkShowFees.Checked){
					procTP.FeeAmt=PIn.Double(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Fee"))].Text);
				}
				if(checkShowIns.Checked){
					procTP.PriInsAmt=PIn.Double(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Pri Ins"))].Text);
					procTP.SecInsAmt=PIn.Double(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Sec Ins"))].Text);
				}
				if(checkShowDiscount.Checked){
					procTP.Discount=PIn.Double(gridMain.Rows[gridMain.SelectedIndices[i]]
							.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Discount"))].Text);
				}
				if(checkShowIns.Checked){
					procTP.PatAmt=PIn.Double(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Pat"))].Text);
				}
				ProcTPs.InsertOrUpdate(procTP,true);
				itemNo++;
				#region Canadian Lab Fees
				/*
				proc=(Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag;
				procTP=new ProcTP();
				procTP.TreatPlanNum=tp.TreatPlanNum;
				procTP.PatNum=PatCur.PatNum;
				procTP.ProcNumOrig=proc.ProcNum;
				procTP.ItemOrder=itemNo;
				procTP.Priority=proc.Priority;
				procTP.ToothNumTP="";
				procTP.Surf="";
				procTP.Code=proc.LabProcCode;
				procTP.Descript=gridMain.Rows[gridMain.SelectedIndices[i]]
					.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Description"))].Text;
				if(checkShowFees.Checked) {
					procTP.FeeAmt=PIn.PDouble(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Fee"))].Text);
				}
				if(checkShowIns.Checked) {
					procTP.PriInsAmt=PIn.PDouble(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Pri Ins"))].Text);
					procTP.SecInsAmt=PIn.PDouble(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Sec Ins"))].Text);
					procTP.PatAmt=PIn.PDouble(gridMain.Rows[gridMain.SelectedIndices[i]]
						.Cells[gridMain.Columns.GetIndex(Lan.g("TableTP","Pat"))].Text);
				}
				ProcTPs.InsertOrUpdate(procTP,true);
				itemNo++;*/
				#endregion Canadian Lab Fees
			}
			ModuleSelected(PatCur.PatNum);
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].TreatPlanNum==tp.TreatPlanNum){
					gridPlans.SetSelected(i+1,true);
					FillMain();
				}
			}
		}

		private void OnSign_Click() {
			if(gridPlans.SelectedIndices[0]==0) {
				MsgBox.Show(this,"You may only sign a saved TP, not the default TP.");
				return;
			}
			PrepImageForPrinting();
			MigraDoc.DocumentObjectModel.Document doc=CreateDocument();
			MigraDoc.Rendering.Printing.MigraDocPrintDocument printdoc=new MigraDoc.Rendering.Printing.MigraDocPrintDocument();
			MigraDoc.Rendering.DocumentRenderer renderer=new MigraDoc.Rendering.DocumentRenderer(doc);
			renderer.PrepareDocument();
			printdoc.Renderer=renderer;
			FormTPsign FormT=new FormTPsign();
			FormT.Document=printdoc;
			FormT.TotalPages=renderer.FormattedDocument.PageCount;
			FormT.TPcur=PlanList[gridPlans.SelectedIndices[0]-1];
			FormT.ShowDialog();
			long tpNum=PlanList[gridPlans.SelectedIndices[0]-1].TreatPlanNum;
			ModuleSelected(PatCur.PatNum);
			for(int i=0;i<PlanList.Length;i++) {
				if(PlanList[i].TreatPlanNum==tpNum) {
					gridPlans.SetSelected(i+1,true);
				}
			}
			FillMain();
		}

		private void OnPreAuth_Click() {
			if(gridPlans.SelectedIndices[0]!=0){
				MsgBox.Show(this,"You can only send a preauth from the current TP, not a saved TP.");
				return;
			}
		  if(gridMain.SelectedIndices.Length==0){
        MessageBox.Show(Lan.g(this,"Please select procedures first."));
        return;
      }
			bool procsSelected=false;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(gridMain.Rows[gridMain.SelectedIndices[i]].Tag!=null){
					procsSelected=true;
				}
			}
			if(!procsSelected) {
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
      FormInsPlanSelect FormIPS=new FormInsPlanSelect(PatCur.PatNum); 
			FormIPS.ViewRelat=true;
      FormIPS.ShowDialog();
      if(FormIPS.DialogResult!=DialogResult.OK){
        return;
      }
			Claim ClaimCur=new Claim();
			ClaimCur.PatNum=PatCur.PatNum;
			ClaimCur.ClaimStatus="W";
			ClaimCur.DateSent=DateTime.Today;
			ClaimCur.PlanNum=FormIPS.SelectedPlan.PlanNum;
			ClaimCur.ProvTreat=0;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(gridMain.Rows[gridMain.SelectedIndices[i]].Tag==null){
					continue;//skip any hightlighted subtotal lines
				}
				if(ClaimCur.ProvTreat==0){//makes sure that at least one prov is set
					ClaimCur.ProvTreat=((Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ProvNum;
				}
				if(!Providers.GetIsSec(((Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ProvNum)){
					ClaimCur.ProvTreat=((Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag).ProvNum;
				}
			}
			ClaimCur.ClinicNum=PatCur.ClinicNum;
			if(Providers.GetIsSec(ClaimCur.ProvTreat)){
				ClaimCur.ProvTreat=PatCur.PriProv;
				//OK if 0, because auto select first in list when open claim
			}
			ClaimCur.ProvBill=Providers.GetBillingProvNum(ClaimCur.ProvTreat,ClaimCur.ClinicNum);
			ClaimCur.EmployRelated=YN.No;
      ClaimCur.ClaimType="PreAuth";
			//this could be a little better if we automate figuring out the patrelat
			//instead of making the user enter it:
			ClaimCur.PatRelat=FormIPS.PatRelat;
			Claims.Insert(ClaimCur);
			Procedure ProcCur;
			ClaimProc ClaimProcCur;
			ClaimProc cpExisting;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(gridMain.Rows[gridMain.SelectedIndices[i]].Tag==null) {
					continue;//skip any highlighted subtotal lines
				}
				ProcCur=(Procedure)gridMain.Rows[gridMain.SelectedIndices[i]].Tag;
        ClaimProcCur=new ClaimProc();
				ClaimProcCur.ProcNum=ProcCur.ProcNum;
        ClaimProcCur.ClaimNum=ClaimCur.ClaimNum;
        ClaimProcCur.PatNum=PatCur.PatNum;
        ClaimProcCur.ProvNum=ProcCur.ProvNum;
				ClaimProcCur.Status=ClaimProcStatus.Preauth;
				ClaimProcCur.FeeBilled=ProcCur.ProcFee;
				ClaimProcCur.PlanNum=FormIPS.SelectedPlan.PlanNum;
				cpExisting=ClaimProcs.GetEstimate(ClaimProcList,ProcCur.ProcNum,FormIPS.SelectedPlan.PlanNum);
				if(cpExisting!=null){
					ClaimProcCur.InsPayEst=cpExisting.InsPayEst;
				}
				if(FormIPS.SelectedPlan.UseAltCode && (ProcedureCodes.GetProcCode(ProcCur.CodeNum).AlternateCode1!="")){
					ClaimProcCur.CodeSent=ProcedureCodes.GetProcCode(ProcCur.CodeNum).AlternateCode1;
				}
				else if(FormIPS.SelectedPlan.IsMedical && ProcCur.MedicalCode!=""){
					ClaimProcCur.CodeSent=ProcCur.MedicalCode;
				}
				else{
					ClaimProcCur.CodeSent=ProcedureCodes.GetStringProcCode(ProcCur.CodeNum);
					if(ClaimProcCur.CodeSent.Length>5 && ClaimProcCur.CodeSent.Substring(0,1)=="D"){
						ClaimProcCur.CodeSent=ClaimProcCur.CodeSent.Substring(0,5);
					}
				}
				ClaimProcCur.LineNumber=i+1;
        ClaimProcs.Insert(ClaimProcCur);
				//ProcCur.Update(ProcOld);
			}
			ProcList=Procedures.Refresh(PatCur.PatNum);
			//ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			ClaimL.CalculateAndUpdate(ProcList,InsPlanList,ClaimCur,PatPlanList,BenefitList,PatCur.Age);
			FormClaimEdit FormCE=new FormClaimEdit(ClaimCur,PatCur,FamCur);
			//FormCE.CalculateEstimates(
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void gridPreAuth_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			Claim claim=Claims.GetClaim(((Claim)ALPreAuth[e.Row]).ClaimNum);//gets attached images.
 			FormClaimEdit FormC=new FormClaimEdit(claim,PatCur,FamCur);
      //FormClaimEdit2.IsPreAuth=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected(PatCur.PatNum);    
		}

		private void gridPreAuth_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(gridPlans.SelectedIndices[0]!=0){
				return;
			}
			gridMain.SetSelected(false);
			Claim ClaimCur=(Claim)ALPreAuth[e.Row];
			List<ClaimProc> ClaimProcsForClaim=ClaimProcs.RefreshForClaim(ClaimCur.ClaimNum);
			Procedure proc;
			for(int i=0;i<gridMain.Rows.Count;i++){//ProcListTP.Length;i++){
				if(gridMain.Rows[i].Tag==null){
					continue;//must be a subtotal row
				}
				proc=(Procedure)gridMain.Rows[i].Tag;
				for(int j=0;j<ClaimProcsForClaim.Count;j++){
					if(proc.ProcNum==ClaimProcsForClaim[j].ProcNum){
						gridMain.SetSelected(i,true);
					}
				}
			}
		}

	

	
		
	

	

		

		

		
	

		

		

		


		
	}

	
}
