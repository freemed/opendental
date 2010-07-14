using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormApptViewEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label labelOps;
		private System.Windows.Forms.ListBox listOps;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.ListView listViewAvailable;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butLeft;
		private OpenDental.UI.Button butRight;
		///<summary></summary>
		public bool IsNew;
		///<summary>A collection of strings of all available element descriptions.</summary>
		private List<String> allElements;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textRowsPerIncr;
		///<summary>A local list of ApptViewItems which are displayed in all three lists on the right.  Not updated to db until the form is closed.</summary>
		private List<ApptViewItem> displayedElementsAll;
		private List<ApptViewItem> displayedElementsMain;
		private List<ApptViewItem> displayedElementsUR;
		private List<ApptViewItem> displayedElementsLR;
		private CheckBox checkOnlyScheduledProvs;
		private TextBox textBeforeTime;
		private GroupBox groupBox1;
		private Label labelBeforeTime;
		private Label labelAfterTime;
		private TextBox textAfterTime;
		private GroupBox groupBox2;
		private Label label8;
		private OpenDental.UI.ODGrid gridLR;
		private OpenDental.UI.ODGrid gridUR;
		private OpenDental.UI.ODGrid gridMain;
		private ODGrid gridAvailable;
		///<summary>Set this value before opening the form.</summary>
		public ApptView ApptViewCur;
		//<summary>Tracks MouseIsDown on listOps.</summary>
		//private bool MouseIsDown;

		///<summary></summary>
		public FormApptViewEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Row 1"},-1,System.Drawing.Color.Red,System.Drawing.Color.Empty,null);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("row2");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptViewEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.labelOps = new System.Windows.Forms.Label();
			this.listOps = new System.Windows.Forms.ListBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.listViewAvailable = new System.Windows.Forms.ListView();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.butRight = new OpenDental.UI.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label6 = new System.Windows.Forms.Label();
			this.textRowsPerIncr = new System.Windows.Forms.TextBox();
			this.checkOnlyScheduledProvs = new System.Windows.Forms.CheckBox();
			this.textBeforeTime = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelAfterTime = new System.Windows.Forms.Label();
			this.textAfterTime = new System.Windows.Forms.TextBox();
			this.labelBeforeTime = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.gridUR = new OpenDental.UI.ODGrid();
			this.gridLR = new OpenDental.UI.ODGrid();
			this.gridAvailable = new OpenDental.UI.ODGrid();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(752,632);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(652,632);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butDelete.Location = new System.Drawing.Point(32,632);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(87,24);
			this.butDelete.TabIndex = 38;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelOps
			// 
			this.labelOps.Location = new System.Drawing.Point(32,128);
			this.labelOps.Name = "labelOps";
			this.labelOps.Size = new System.Drawing.Size(165,40);
			this.labelOps.TabIndex = 39;
			this.labelOps.Text = "View Operatories";
			this.labelOps.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listOps
			// 
			this.listOps.Location = new System.Drawing.Point(32,170);
			this.listOps.Name = "listOps";
			this.listOps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listOps.Size = new System.Drawing.Size(120,186);
			this.listOps.TabIndex = 40;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(32,385);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120,212);
			this.listProv.TabIndex = 42;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32,360);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128,23);
			this.label2.TabIndex = 41;
			this.label2.Text = "View Provider Bars";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(110,10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(187,18);
			this.label3.TabIndex = 43;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(298,11);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(250,20);
			this.textDescription.TabIndex = 44;
			// 
			// listViewAvailable
			// 
			this.listViewAvailable.FullRowSelect = true;
			this.listViewAvailable.HideSelection = false;
			this.listViewAvailable.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listViewAvailable.LabelWrap = false;
			this.listViewAvailable.Location = new System.Drawing.Point(222,457);
			this.listViewAvailable.MultiSelect = false;
			this.listViewAvailable.Name = "listViewAvailable";
			this.listViewAvailable.Size = new System.Drawing.Size(175,103);
			this.listViewAvailable.TabIndex = 48;
			this.listViewAvailable.UseCompatibleStateImageBehavior = false;
			this.listViewAvailable.View = System.Windows.Forms.View.List;
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
			this.butDown.Location = new System.Drawing.Point(297,420);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(71,24);
			this.butDown.TabIndex = 50;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(219,420);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(71,24);
			this.butUp.TabIndex = 51;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(-1,0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(404,279);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(35,26);
			this.butLeft.TabIndex = 52;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(404,319);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(35,26);
			this.butRight.TabIndex = 53;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(51,34);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(246,18);
			this.label6.TabIndex = 54;
			this.label6.Text = "Rows Per Time Increment (usually 1)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textRowsPerIncr
			// 
			this.textRowsPerIncr.Location = new System.Drawing.Point(298,36);
			this.textRowsPerIncr.Name = "textRowsPerIncr";
			this.textRowsPerIncr.Size = new System.Drawing.Size(56,20);
			this.textRowsPerIncr.TabIndex = 55;
			this.textRowsPerIncr.Validating += new System.ComponentModel.CancelEventHandler(this.textRowsPerIncr_Validating);
			// 
			// checkOnlyScheduledProvs
			// 
			this.checkOnlyScheduledProvs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkOnlyScheduledProvs.Location = new System.Drawing.Point(6,11);
			this.checkOnlyScheduledProvs.Name = "checkOnlyScheduledProvs";
			this.checkOnlyScheduledProvs.Size = new System.Drawing.Size(282,18);
			this.checkOnlyScheduledProvs.TabIndex = 56;
			this.checkOnlyScheduledProvs.Text = "Only show operatories for scheduled providers";
			this.checkOnlyScheduledProvs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkOnlyScheduledProvs.UseVisualStyleBackColor = true;
			this.checkOnlyScheduledProvs.Click += new System.EventHandler(this.checkOnlyScheduledProvs_Click);
			// 
			// textBeforeTime
			// 
			this.textBeforeTime.Location = new System.Drawing.Point(263,30);
			this.textBeforeTime.Name = "textBeforeTime";
			this.textBeforeTime.Size = new System.Drawing.Size(56,20);
			this.textBeforeTime.TabIndex = 57;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelAfterTime);
			this.groupBox1.Controls.Add(this.textAfterTime);
			this.groupBox1.Controls.Add(this.labelBeforeTime);
			this.groupBox1.Controls.Add(this.checkOnlyScheduledProvs);
			this.groupBox1.Controls.Add(this.textBeforeTime);
			this.groupBox1.Location = new System.Drawing.Point(35,53);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(354,76);
			this.groupBox1.TabIndex = 58;
			this.groupBox1.TabStop = false;
			// 
			// labelAfterTime
			// 
			this.labelAfterTime.Location = new System.Drawing.Point(77,52);
			this.labelAfterTime.Name = "labelAfterTime";
			this.labelAfterTime.Size = new System.Drawing.Size(187,17);
			this.labelAfterTime.TabIndex = 60;
			this.labelAfterTime.Text = "Only if after time";
			this.labelAfterTime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textAfterTime
			// 
			this.textAfterTime.Location = new System.Drawing.Point(263,52);
			this.textAfterTime.Name = "textAfterTime";
			this.textAfterTime.Size = new System.Drawing.Size(56,20);
			this.textAfterTime.TabIndex = 59;
			// 
			// labelBeforeTime
			// 
			this.labelBeforeTime.Location = new System.Drawing.Point(77,30);
			this.labelBeforeTime.Name = "labelBeforeTime";
			this.labelBeforeTime.Size = new System.Drawing.Size(187,17);
			this.labelBeforeTime.TabIndex = 58;
			this.labelBeforeTime.Text = "Only if before time";
			this.labelBeforeTime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.gridLR);
			this.groupBox2.Controls.Add(this.gridUR);
			this.groupBox2.Controls.Add(this.gridMain);
			this.groupBox2.Controls.Add(this.butUp);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.butDown);
			this.groupBox2.Location = new System.Drawing.Point(449,152);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(378,457);
			this.groupBox2.TabIndex = 59;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Rows Displayed (double click to edit or to move to another corner)";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(11,423);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(209,17);
			this.label8.TabIndex = 59;
			this.label8.Text = "Move any item within its own list:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(11,18);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(175,390);
			this.gridMain.TabIndex = 60;
			this.gridMain.Title = "Main List";
			this.gridMain.TranslationName = null;
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// gridUR
			// 
			this.gridUR.HScrollVisible = false;
			this.gridUR.Location = new System.Drawing.Point(192,18);
			this.gridUR.Name = "gridUR";
			this.gridUR.ScrollValue = 0;
			this.gridUR.Size = new System.Drawing.Size(175,138);
			this.gridUR.TabIndex = 61;
			this.gridUR.Title = "Upper Right Corner";
			this.gridUR.TranslationName = null;
			this.gridUR.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridUR_CellClick);
			this.gridUR.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridUR_CellDoubleClick);
			// 
			// gridLR
			// 
			this.gridLR.HScrollVisible = false;
			this.gridLR.Location = new System.Drawing.Point(192,288);
			this.gridLR.Name = "gridLR";
			this.gridLR.ScrollValue = 0;
			this.gridLR.Size = new System.Drawing.Size(175,120);
			this.gridLR.TabIndex = 62;
			this.gridLR.Title = "Lower Right Corner";
			this.gridLR.TranslationName = null;
			this.gridLR.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridLR_CellClick);
			this.gridLR.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridLR_CellDoubleClick);
			// 
			// gridAvailable
			// 
			this.gridAvailable.HScrollVisible = false;
			this.gridAvailable.Location = new System.Drawing.Point(222,170);
			this.gridAvailable.Name = "gridAvailable";
			this.gridAvailable.ScrollValue = 0;
			this.gridAvailable.Size = new System.Drawing.Size(175,269);
			this.gridAvailable.TabIndex = 61;
			this.gridAvailable.Title = "Available Rows";
			this.gridAvailable.TranslationName = null;
			// 
			// FormApptViewEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(852,675);
			this.Controls.Add(this.gridAvailable);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textRowsPerIncr);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butRight);
			this.Controls.Add(this.butLeft);
			this.Controls.Add(this.listViewAvailable);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listOps);
			this.Controls.Add(this.labelOps);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptViewEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment View Edit";
			this.Load += new System.EventHandler(this.FormApptViewEdit_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptViewEdit_Closing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormApptViewEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=ApptViewCur.Description;
			if(ApptViewCur.RowsPerIncr==0){
				textRowsPerIncr.Text="1";
			}
			else{
				textRowsPerIncr.Text=ApptViewCur.RowsPerIncr.ToString();
			}
			checkOnlyScheduledProvs.Checked=ApptViewCur.OnlyScheduledProvs;
			if(ApptViewCur.OnlySchedBeforeTime > new TimeSpan(0,0,0)) {
				textBeforeTime.Text=(DateTime.Today+ApptViewCur.OnlySchedBeforeTime).ToShortTimeString();
			}
			if(ApptViewCur.OnlySchedAfterTime > new TimeSpan(0,0,0)) {
				textAfterTime.Text=(DateTime.Today+ApptViewCur.OnlySchedAfterTime).ToShortTimeString();
			}
			SetOpLabel();
			ApptViewItemL.GetForCurView(ApptViewCur,true,null);//passing in true triggers it to give us the proper list of ops.
			for(int i=0;i<OperatoryC.ListShort.Count;i++){
				listOps.Items.Add(OperatoryC.ListShort[i].OpName);
				if(ApptViewItemL.OpIsInView(OperatoryC.ListShort[i].OperatoryNum)){
					listOps.SetSelected(i,true);
				}
			}
			for(int i=0;i<ProviderC.List.Length;i++){
				listProv.Items.Add
					(ProviderC.List[i].GetLongDesc());
				if(ApptViewItemL.ProvIsInView(ProviderC.List[i].ProvNum)){
					listProv.SetSelected(i,true);
				}
			}
			allElements=new List<String>();
			allElements.Add("Address");
			allElements.Add("AddrNote");
			allElements.Add("Age");
			allElements.Add("ASAP");
			allElements.Add("ChartNumAndName");
			allElements.Add("ChartNumber");
			allElements.Add("HmPhone");
			allElements.Add("Lab");
			allElements.Add("MedUrgNote");
			allElements.Add("PremedFlag");
			allElements.Add("Note");
			allElements.Add("PatientName");
			allElements.Add("PatientNameF");
			allElements.Add("PatNum");
			allElements.Add("PatNumAndName");
			allElements.Add("Procs");
			allElements.Add("Production");
			allElements.Add("Provider");
			allElements.Add("WirelessPhone");
			allElements.Add("WkPhone");
			displayedElementsAll=new List<ApptViewItem>();
			for(int i=0;i<ApptViewItemL.ApptRows.Count;i++) {
				displayedElementsAll.Add(ApptViewItemL.ApptRows[i]);
			}
			FillElements();
		}

		///<summary>Fills the five lists based on the displayedElements lists. No database transactions are performed here.</summary>
		private void FillElements(){
			displayedElementsMain=new List<ApptViewItem>();
			displayedElementsUR=new List<ApptViewItem>();
			displayedElementsLR=new List<ApptViewItem>();
			for(int i=0;i<displayedElementsAll.Count;i++) {
				if(displayedElementsAll[i].ElementAlignment==ApptViewAlignment.Main) {
					displayedElementsMain.Add(ApptViewItemL.ApptRows[i]);
				}
				else if(displayedElementsAll[i].ElementAlignment==ApptViewAlignment.UR) {
					displayedElementsUR.Add(ApptViewItemL.ApptRows[i]);
				}
				else if(displayedElementsAll[i].ElementAlignment==ApptViewAlignment.LR) {
					displayedElementsLR.Add(ApptViewItemL.ApptRows[i]);
				}
			}
			//Now fill the lists on the screen--------------------------------------------------
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<displayedElementsMain.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(displayedElementsMain[i].ElementDesc);
				row.ColorText=displayedElementsMain[i].ElementColor;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			//gridUR---------------------------------------------------------
			gridUR.BeginUpdate();
			gridUR.Columns.Clear();
			col=new ODGridColumn("",100);
			gridUR.Columns.Add(col);
			gridUR.Rows.Clear();
			for(int i=0;i<displayedElementsUR.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(displayedElementsUR[i].ElementDesc);
				row.ColorText=displayedElementsUR[i].ElementColor;
				gridUR.Rows.Add(row);
			}
			gridUR.EndUpdate();
			//gridLR-----------------------------------------------------------
			gridLR.BeginUpdate();
			gridLR.Columns.Clear();
			col=new ODGridColumn("",100);
			gridLR.Columns.Add(col);
			gridLR.Rows.Clear();
			for(int i=0;i<displayedElementsLR.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(displayedElementsLR[i].ElementDesc);
				row.ColorText=displayedElementsLR[i].ElementColor;
				gridLR.Rows.Add(row);
			}
			gridLR.EndUpdate();
			//gridAvailable-----------------------------------------------------------
			gridAvailable.BeginUpdate();
			gridAvailable.Columns.Clear();
			col=new ODGridColumn("",100);
			gridAvailable.Columns.Add(col);
			gridAvailable.Rows.Clear();
			for(int i=0;i<allElements.Count;i++) {
				if(!ElementIsDisplayed(allElements[i])) {
					row=new ODGridRow();
					row.Cells.Add(allElements[i]);
					gridAvailable.Rows.Add(row);
				}
			}
			gridAvailable.EndUpdate();
		}

		///<summary>Called from FillElements. Used to determine whether a given element is already displayed. If not, then it is displayed in the available rows on the right.</summary>
		private bool ElementIsDisplayed(string elementDesc){
			for(int i=0;i<displayedElementsAll.Count;i++){
				if(displayedElementsAll[i].ElementDesc==elementDesc){
					return true;
				}
			}
			return false;
		}

		private void checkOnlyScheduledProvs_Click(object sender,EventArgs e) {
			SetOpLabel();
		}

		private void SetOpLabel(){
			if(checkOnlyScheduledProvs.Checked) {
				labelOps.Text=Lan.g(this,"View Operatories (week view only)");
				labelBeforeTime.Visible=true;
				labelAfterTime.Visible=true;
				textBeforeTime.Visible=true;
				textAfterTime.Visible=true;
			}
			else {
				labelOps.Text=Lan.g(this,"View Operatories");
				labelBeforeTime.Visible=false;
				labelAfterTime.Visible=false;
				textBeforeTime.Visible=false;
				textAfterTime.Visible=false;
			}
		}

		private void butLeft_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length>0) {
				displayedElementsAll.Remove(displayedElementsMain[gridMain.SelectedIndices[0]]);
			}
			else if(gridUR.SelectedIndices.Length>0) {
				displayedElementsAll.Remove(displayedElementsMain[gridUR.SelectedIndices[0]]);
			}
			else if(gridLR.SelectedIndices.Length>0) {
				displayedElementsAll.Remove(displayedElementsMain[gridLR.SelectedIndices[0]]);
			}
			FillElements();
		}

		private void butRight_Click(object sender, System.EventArgs e) {
			if(listViewAvailable.SelectedIndices.Count==0){
				return;
			}
			//the item order is not used until saving to db.
			ApptViewItem item=new ApptViewItem(listViewAvailable.SelectedItems[0].Text,0,Color.Black);
			if(gridMain.SelectedIndices.Length==1) {//insert
				int newIdx=displayedElementsAll.IndexOf(displayedElementsMain[gridMain.GetSelectedIndex()]);
				displayedElementsAll.Insert(newIdx,item);
				//displayedElementsMain.Insert(listViewMain.SelectedItems[0].Index,item);
			}
			else{//add to end
				displayedElementsAll.Add(item);
			}
			FillElements();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			int oldIdx;
			int newIdx;
			int newIdxAll;//within the list of all.
			ApptViewItem item;
			if(gridMain.SelectedIndices.Length>0) {
				oldIdx=gridMain.GetSelectedIndex();
				if(oldIdx==0) {
					return;//can't move up any more
				}
				item=displayedElementsMain[oldIdx];
				newIdx=oldIdx-1;
				newIdxAll=displayedElementsAll.IndexOf(displayedElementsMain[newIdx]);
				displayedElementsAll.Remove(item);
				displayedElementsAll.Insert(newIdxAll,item);
				FillElements();
				gridMain.SetSelected(newIdx,true);
			}
			/*
			if(listViewUR.SelectedIndices.Count>0) {
				
			}
			if(listViewLR.SelectedIndices.Count>0) {
				
			}*/
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			int oldIdx;
			int newIdx;
			int newIdxAll;
			ApptViewItem item;
			if(gridMain.SelectedIndices.Length>0) {
				oldIdx=gridMain.GetSelectedIndex();
				if(oldIdx==displayedElementsMain.Count-1) {
					return;//can't move down any more
				}
				item=displayedElementsMain[oldIdx];
				newIdx=oldIdx+1;
				newIdxAll=displayedElementsAll.IndexOf(displayedElementsMain[newIdx]);
				displayedElementsAll.Remove(item);
				displayedElementsAll.Insert(newIdxAll,item);
				FillElements();
				gridMain.SetSelected(newIdx,true);
			}
			if(gridUR.SelectedIndices.Length>0) {
			
			}
			if(gridLR.SelectedIndices.Length>0) {
				
			}
		}

		private void gridMain_CellClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			if(gridMain.SelectedIndices.Length>0) {
				gridUR.SetSelected(false);
				gridLR.SetSelected(false);
			}
		}

		private void gridUR_CellClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			if(gridUR.SelectedIndices.Length>0) {
				gridMain.SetSelected(false);
				gridLR.SetSelected(false);
			}
		}

		private void gridLR_CellClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			if(gridLR.SelectedIndices.Length>0) {
				gridUR.SetSelected(false);
				gridMain.SetSelected(false);
			}
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			int originalI=e.Row;//listViewMain.SelectedIndices[0];
			FormApptViewItemEdit formA=new FormApptViewItemEdit();
			formA.ApptVItem=displayedElementsMain[originalI];
			formA.ShowDialog();
			/*
			colorDialog1=new ColorDialog();
			colorDialog1.Color=item.ElementColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			item.ElementColor=colorDialog1.Color;
			displayedElements.RemoveAt(originalI);
			displayedElements.Insert(originalI,item);*/
			FillElements();
		}

		private void gridUR_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {

		}

		private void gridLR_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {

		}

		private void textRowsPerIncr_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			try{
				Convert.ToInt32(textRowsPerIncr.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Must be a number between 1 and 3."));
				e.Cancel=true;
				return;
			}
			if(PIn.Long(textRowsPerIncr.Text)<1 || PIn.Long(textRowsPerIncr.Text)>3){
				MessageBox.Show(Lan.g(this,"Must be a number between 1 and 3."));
				e.Cancel=true;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			//this does mess up the item orders a little, but missing numbers don't actually hurt anything.
			if(MessageBox.Show(Lan.g(this,"Delete this category?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			ApptViewItems.DeleteAllForView(ApptViewCur);
			ApptViews.Delete(ApptViewCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listProv.SelectedIndices.Count==0){
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(listOps.SelectedIndices.Count==0){// && !checkOnlyScheduledProvs.Checked) {
				MsgBox.Show(this,"At least one operatory must be selected.");
				return;
			}
			if(textDescription.Text==""){
				MessageBox.Show(Lan.g(this,"A description must be entered."));
				return;
			}
			if(displayedElementsMain.Count==0){
				MessageBox.Show(Lan.g(this,"At least one row type must be displayed."));
				return;
			}
			DateTime timeBefore=new DateTime();//only the time portion will be used.
			if(checkOnlyScheduledProvs.Checked && textBeforeTime.Text!="") {
				try {
					timeBefore=DateTime.Parse(textBeforeTime.Text);
				}
				catch {
					MsgBox.Show(this,"Time before invalid.");
					return;
				}
			}
			DateTime timeAfter=new DateTime();
			if(checkOnlyScheduledProvs.Checked && textAfterTime.Text!="") {
				try {
					timeAfter=DateTime.Parse(textAfterTime.Text);
				}
				catch {
					MsgBox.Show(this,"Time after invalid.");
					return;
				}
			}
			ApptViewItems.DeleteAllForView(ApptViewCur);//start with a clean slate
			for(int i=0;i<OperatoryC.ListShort.Count;i++){
				if(listOps.SelectedIndices.Contains(i)){
					ApptViewItem ApptViewItemCur=new ApptViewItem();
					ApptViewItemCur.ApptViewNum=ApptViewCur.ApptViewNum;
					ApptViewItemCur.OpNum=OperatoryC.ListShort[i].OperatoryNum;
					ApptViewItems.Insert(ApptViewItemCur);
				}
			}
			for(int i=0;i<ProviderC.List.Length;i++){
				if(listProv.SelectedIndices.Contains(i)){
					ApptViewItem ApptViewItemCur=new ApptViewItem();
					ApptViewItemCur.ApptViewNum=ApptViewCur.ApptViewNum;
					ApptViewItemCur.ProvNum=ProviderC.List[i].ProvNum;
					ApptViewItems.Insert(ApptViewItemCur);
				}
			}
			for(int i=0;i<displayedElementsMain.Count;i++){
				ApptViewItem ApptViewItemCur=displayedElementsMain[i];
				ApptViewItemCur.ApptViewNum=ApptViewCur.ApptViewNum;
				//elementDesc and elementColor already handled.
				ApptViewItemCur.ElementOrder=(byte)i;
				ApptViewItems.Insert(ApptViewItemCur);
			}
			ApptViewCur.Description=textDescription.Text;
			ApptViewCur.RowsPerIncr=PIn.Byte(textRowsPerIncr.Text);
			ApptViewCur.OnlyScheduledProvs=checkOnlyScheduledProvs.Checked;
			ApptViewCur.OnlySchedBeforeTime=timeBefore.TimeOfDay;
			ApptViewCur.OnlySchedAfterTime=timeAfter.TimeOfDay;
			ApptViews.Update(ApptViewCur);//same whether isnew or not
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;;
		}

		private void FormApptViewEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(IsNew){
				ApptViewItems.DeleteAllForView(ApptViewCur);
				ApptViews.Delete(ApptViewCur);
			}
		}

		

		

		

		

		

		

		

		

		

		


	}
}





















