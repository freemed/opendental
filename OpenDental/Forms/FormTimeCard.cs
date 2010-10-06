using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTimeCard:System.Windows.Forms.Form {
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textTotal;
		private System.Windows.Forms.Timer timer1;
		///<summary>True to default to viewing breaks. False for regular hours.</summary>
		public bool IsBreaks;
		///<summary>Server time minus local computer time, usually +/- 1 or 2 minutes</summary>
		private TimeSpan TimeDelta;
		private OpenDental.UI.ODGrid gridMain;
		private GroupBox groupBox1;
		private OpenDental.UI.Button butRight;
		private OpenDental.UI.Button butLeft;
		private Label label4;
		private TextBox textDateStart;
		private TextBox textDatePaycheck;
		private TextBox textDateStop;
		private List<ClockEvent> ClockEventList;
		private OpenDental.UI.Button butAdj;
		private int SelectedPayPeriod;
		private Label labelOvertime;
		private TextBox textOvertime;
		private OpenDental.UI.Button butCompute;
		private OpenDental.UI.Button butPrint;
		private List<TimeAdjust> TimeAdjustList;
		private PrintDocument pd;
		private int linesPrinted;
		private GroupBox groupBox2;
		private RadioButton radioBreaks;
		private RadioButton radioTimeCard;
		//private OpenDental.UI.PrintPreview printPreview;
		///<summary>An array list of ojects representing the rows in the table. Can be either clockEvents or timeAdjusts.</summary>
		private ArrayList mergedAL;
		///<summary>The running weekly total, whether it gets displayed or not.</summary>
		private TimeSpan[] WeeklyTotals;
		private TextBox textOvertime2;
		private TextBox textTotal2;
		public Employee EmployeeCur;
		private OpenDental.UI.Button butDaily;
		private bool cannotEdit;

		///<summary></summary>
		public FormTimeCard()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeCard));
			this.textTotal = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textDatePaycheck = new System.Windows.Forms.TextBox();
			this.textDateStop = new System.Windows.Forms.TextBox();
			this.textDateStart = new System.Windows.Forms.TextBox();
			this.butRight = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.labelOvertime = new System.Windows.Forms.Label();
			this.textOvertime = new System.Windows.Forms.TextBox();
			this.butCompute = new OpenDental.UI.Button();
			this.butAdj = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioBreaks = new System.Windows.Forms.RadioButton();
			this.radioTimeCard = new System.Windows.Forms.RadioButton();
			this.textOvertime2 = new System.Windows.Forms.TextBox();
			this.textTotal2 = new System.Windows.Forms.TextBox();
			this.butDaily = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textTotal
			// 
			this.textTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textTotal.Location = new System.Drawing.Point(491,642);
			this.textTotal.Name = "textTotal";
			this.textTotal.Size = new System.Drawing.Size(66,20);
			this.textTotal.TabIndex = 3;
			this.textTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label1.Location = new System.Drawing.Point(385,643);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Regular Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(146,8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96,18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Start Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(143,28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99,18);
			this.label3.TabIndex = 8;
			this.label3.Text = "End Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textDatePaycheck);
			this.groupBox1.Controls.Add(this.textDateStop);
			this.groupBox1.Controls.Add(this.textDateStart);
			this.groupBox1.Controls.Add(this.butRight);
			this.groupBox1.Controls.Add(this.butLeft);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(18,3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(659,51);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Pay Period";
			// 
			// textDatePaycheck
			// 
			this.textDatePaycheck.Location = new System.Drawing.Point(473,19);
			this.textDatePaycheck.Name = "textDatePaycheck";
			this.textDatePaycheck.ReadOnly = true;
			this.textDatePaycheck.Size = new System.Drawing.Size(100,20);
			this.textDatePaycheck.TabIndex = 14;
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(244,28);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.ReadOnly = true;
			this.textDateStop.Size = new System.Drawing.Size(100,20);
			this.textDateStop.TabIndex = 13;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(244,8);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.ReadOnly = true;
			this.textDateStart.Size = new System.Drawing.Size(100,20);
			this.textDateStart.TabIndex = 12;
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(63,18);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(39,24);
			this.butRight.TabIndex = 11;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(13,18);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(39,24);
			this.butLeft.TabIndex = 10;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(354,19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(117,18);
			this.label4.TabIndex = 9;
			this.label4.Text = "Paycheck Date";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelOvertime
			// 
			this.labelOvertime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelOvertime.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelOvertime.Location = new System.Drawing.Point(385,663);
			this.labelOvertime.Name = "labelOvertime";
			this.labelOvertime.Size = new System.Drawing.Size(100,17);
			this.labelOvertime.TabIndex = 17;
			this.labelOvertime.Text = "Overtime";
			this.labelOvertime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOvertime
			// 
			this.textOvertime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textOvertime.Location = new System.Drawing.Point(491,662);
			this.textOvertime.Name = "textOvertime";
			this.textOvertime.Size = new System.Drawing.Size(66,20);
			this.textOvertime.TabIndex = 16;
			this.textOvertime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butCompute
			// 
			this.butCompute.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCompute.Autosize = true;
			this.butCompute.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCompute.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCompute.CornerRadius = 4F;
			this.butCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCompute.Location = new System.Drawing.Point(139,650);
			this.butCompute.Name = "butCompute";
			this.butCompute.Size = new System.Drawing.Size(90,24);
			this.butCompute.TabIndex = 18;
			this.butCompute.Text = "Calc Week OT";
			this.butCompute.Click += new System.EventHandler(this.butCompute_Click);
			// 
			// butAdj
			// 
			this.butAdj.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdj.Autosize = true;
			this.butAdj.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdj.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdj.CornerRadius = 4F;
			this.butAdj.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdj.Location = new System.Drawing.Point(18,650);
			this.butAdj.Name = "butAdj";
			this.butAdj.Size = new System.Drawing.Size(115,24);
			this.butAdj.TabIndex = 15;
			this.butAdj.Text = "Add Adjustment";
			this.butAdj.Click += new System.EventHandler(this.butAdj_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(18,60);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(851,580);
			this.gridMain.TabIndex = 13;
			this.gridMain.Title = "Time Card";
			this.gridMain.TranslationName = "TableTimeCard";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(794,650);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(691,650);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(86,24);
			this.butPrint.TabIndex = 19;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioBreaks);
			this.groupBox2.Controls.Add(this.radioTimeCard);
			this.groupBox2.Location = new System.Drawing.Point(747,3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(122,51);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			// 
			// radioBreaks
			// 
			this.radioBreaks.Location = new System.Drawing.Point(14,27);
			this.radioBreaks.Name = "radioBreaks";
			this.radioBreaks.Size = new System.Drawing.Size(97,19);
			this.radioBreaks.TabIndex = 1;
			this.radioBreaks.Text = "Breaks";
			this.radioBreaks.UseVisualStyleBackColor = true;
			this.radioBreaks.Click += new System.EventHandler(this.radioBreaks_Click);
			// 
			// radioTimeCard
			// 
			this.radioTimeCard.Checked = true;
			this.radioTimeCard.Location = new System.Drawing.Point(14,10);
			this.radioTimeCard.Name = "radioTimeCard";
			this.radioTimeCard.Size = new System.Drawing.Size(97,19);
			this.radioTimeCard.TabIndex = 0;
			this.radioTimeCard.TabStop = true;
			this.radioTimeCard.Text = "Time Card";
			this.radioTimeCard.UseVisualStyleBackColor = true;
			this.radioTimeCard.Click += new System.EventHandler(this.radioTimeCard_Click);
			// 
			// textOvertime2
			// 
			this.textOvertime2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textOvertime2.Location = new System.Drawing.Point(563,662);
			this.textOvertime2.Name = "textOvertime2";
			this.textOvertime2.Size = new System.Drawing.Size(66,20);
			this.textOvertime2.TabIndex = 22;
			this.textOvertime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textTotal2
			// 
			this.textTotal2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textTotal2.Location = new System.Drawing.Point(563,642);
			this.textTotal2.Name = "textTotal2";
			this.textTotal2.Size = new System.Drawing.Size(66,20);
			this.textTotal2.TabIndex = 21;
			this.textTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butDaily
			// 
			this.butDaily.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDaily.Autosize = true;
			this.butDaily.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDaily.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDaily.CornerRadius = 4F;
			this.butDaily.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDaily.Location = new System.Drawing.Point(235,650);
			this.butDaily.Name = "butDaily";
			this.butDaily.Size = new System.Drawing.Size(78,24);
			this.butDaily.TabIndex = 23;
			this.butDaily.Text = "Calc Daily";
			this.butDaily.Click += new System.EventHandler(this.butDaily_Click);
			// 
			// FormTimeCard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(891,686);
			this.Controls.Add(this.butDaily);
			this.Controls.Add(this.textOvertime2);
			this.Controls.Add(this.textTotal2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butCompute);
			this.Controls.Add(this.labelOvertime);
			this.Controls.Add(this.textOvertime);
			this.Controls.Add(this.butAdj);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textTotal);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTimeCard";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Card";
			this.Load += new System.EventHandler(this.FormTimeCard_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTimeCard_Load(object sender, System.EventArgs e){
			//Check to see if the employee currently logged in can edit this time-card.
			cannotEdit=Security.CurUser!=null &&
				Security.CurUser.EmployeeNum==EmployeeCur.EmployeeNum &&
				PrefC.GetBool(PrefName.TimecardSecurityEnabled) &&
				PrefC.GetBool(PrefName.TimecardUsersDontEditOwnCard);
			if(cannotEdit) {
				butAdj.Enabled=false;
				butCompute.Enabled=false;
			}
			Text=Lan.g(this,"TimeCard for")+" "+EmployeeCur.FName+" "+EmployeeCur.LName
				+(cannotEdit?" - You cannot modify your timecard":"");
			TimeDelta=MiscData.GetNowDateTime()-DateTime.Now;
			SelectedPayPeriod=PayPeriods.GetForDate(DateTime.Today);
			if(IsBreaks){
				textOvertime.Visible=false;
				labelOvertime.Visible=false;
				butCompute.Visible=false;
				butAdj.Visible=false;
			}
			radioTimeCard.Checked=!IsBreaks;
			radioBreaks.Checked=IsBreaks;
			FillPayPeriod();
			FillMain(true);
		}

		private void butLeft_Click(object sender,EventArgs e) {
			if(SelectedPayPeriod==0){
				return;
			}
			SelectedPayPeriod--;
			FillPayPeriod();
			FillMain(true);
		}

		private void butRight_Click(object sender,EventArgs e) {
			if(SelectedPayPeriod==PayPeriods.List.Length-1) {
				return;
			}
			SelectedPayPeriod++;
			FillPayPeriod();
			FillMain(true);
		}

		///<summary>SelectedPayPeriod should already be set.  This simply fills the screen with that data.</summary>
		private void FillPayPeriod(){
			textDateStart.Text=PayPeriods.List[SelectedPayPeriod].DateStart.ToShortDateString();
			textDateStop.Text=PayPeriods.List[SelectedPayPeriod].DateStop.ToShortDateString();
			if(PayPeriods.List[SelectedPayPeriod].DatePaycheck.Year<1880){
				textDatePaycheck.Text="";
			}
			else{
				textDatePaycheck.Text=PayPeriods.List[SelectedPayPeriod].DatePaycheck.ToShortDateString();
			}
		}

		private void radioTimeCard_Click(object sender,EventArgs e) {
			IsBreaks=false;
			textOvertime.Visible=true;
			labelOvertime.Visible=true;
			butCompute.Visible=true;
			butAdj.Visible=true;
			FillMain(true);
		}

		private void radioBreaks_Click(object sender,EventArgs e) {
			IsBreaks=true;
			textOvertime.Visible=false;
			labelOvertime.Visible=false;
			butCompute.Visible=false;
			butAdj.Visible=false;
			FillMain(true);
		}

		private DateTime GetDateForRow(int i){
			if(mergedAL[i].GetType()==typeof(ClockEvent)){
				return ((ClockEvent)mergedAL[i]).TimeDisplayed1.Date;
			}
			else if(mergedAL[i].GetType()==typeof(TimeAdjust)){
				return ((TimeAdjust)mergedAL[i]).TimeEntry.Date;
			}
			return DateTime.MinValue;
		}

		///<summary>fromDB is set to false when it is refreshing every second so that there will be no extra network traffic.</summary>
		private void FillMain(bool fromDB){
			if(fromDB){
				ClockEventList=ClockEvents.Refresh(EmployeeCur.EmployeeNum,PIn.Date(textDateStart.Text),
					PIn.Date(textDateStop.Text),false,IsBreaks);
				if(IsBreaks){
					TimeAdjustList=new List<TimeAdjust>();
				}
				else{
					TimeAdjustList=TimeAdjusts.Refresh(EmployeeCur.EmployeeNum,PIn.Date(textDateStart.Text),
						PIn.Date(textDateStop.Text));
				}
			}
			mergedAL=new ArrayList();
			for(int i=0;i<ClockEventList.Count;i++) {
				mergedAL.Add(ClockEventList[i]);
			}
			for(int i=0;i<TimeAdjustList.Count;i++) {
				mergedAL.Add(TimeAdjustList[i]);
			}
			IComparer myComparer=new ObjectDateComparer();
			mergedAL.Sort(myComparer);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Weekday"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Altered"),50,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			if(IsBreaks){
				col=new ODGridColumn(Lan.g(this,"Out"),60,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"In"),60,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
			}
			else{
				col=new ODGridColumn(Lan.g(this,"In"),60,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Out"),60,HorizontalAlignment.Right);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g(this,"Total"),50,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Adjust"),55,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Overtime"),55,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Daily"),50,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Weekly"),50,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),5);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			WeeklyTotals=new TimeSpan[mergedAL.Count];
			TimeSpan alteredSpan=new TimeSpan(0);//used to display altered times
			TimeSpan oneSpan=new TimeSpan(0);//used to sum one pair of clock-in/clock-out
			TimeSpan oneAdj;
			TimeSpan oneOT;
			TimeSpan daySpan=new TimeSpan(0);//used for daily totals.
			TimeSpan weekSpan=new TimeSpan(0);//used for weekly totals.
			if(mergedAL.Count>0){
				weekSpan=ClockEvents.GetWeekTotal(EmployeeCur.EmployeeNum,GetDateForRow(0));
			}
			TimeSpan periodSpan=new TimeSpan(0);//used to add up totals for entire page.
			TimeSpan otspan=new TimeSpan(0);//overtime for the entire period
      Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			DateTime curDate=DateTime.MinValue;
			DateTime previousDate=DateTime.MinValue;
			Type type;
			ClockEvent clock;
			TimeAdjust adjust;
			for(int i=0;i<mergedAL.Count;i++){
				row=new ODGridRow();
				type=mergedAL[i].GetType();
				row.Tag=mergedAL[i];
				previousDate=curDate;
				//clock event row---------------------------------------------------------------------------------------------
				if(type==typeof(ClockEvent)){
					clock=(ClockEvent)mergedAL[i];
					curDate=clock.TimeDisplayed1.Date;
					if(curDate==previousDate){
						row.Cells.Add("");
						row.Cells.Add("");
					}
					else{
						row.Cells.Add(curDate.ToShortDateString());
						row.Cells.Add(curDate.DayOfWeek.ToString());
					}
					//altered--------------------------------------
					string str="";
					if(clock.TimeEntered1!=clock.TimeDisplayed1){
						if(IsBreaks){
							str=Lan.g(this,"out");
						}
						else{
							str=Lan.g(this,"in");
						}
					}
					if(clock.TimeEntered2!=clock.TimeDisplayed2){
						if(str!="") {
							str+="/";
						}
						if(IsBreaks){
							str+=Lan.g(this,"in");
						}
						else{
							str+=Lan.g(this,"out");
						}
					}
					row.Cells.Add(str);
					//status--------------------------------------
					//row.Cells.Add(clock.ClockStatus.ToString());
					//in------------------------------------------
					row.Cells.Add(clock.TimeDisplayed1.ToShortTimeString());
					//out-----------------------------
					if(clock.TimeDisplayed2.Year<1880){
						row.Cells.Add("");//not clocked out yet
					}
					else{
						row.Cells.Add(clock.TimeDisplayed2.ToShortTimeString());
					}
					//total-------------------------------
					if(IsBreaks){ //breaks
						if(clock.TimeDisplayed2.Year<1880){
							row.Cells.Add("");
						}
						else{
							oneSpan=clock.TimeDisplayed2-clock.TimeDisplayed1;
							row.Cells.Add(ClockEvents.Format(oneSpan));
							daySpan+=oneSpan;
							periodSpan+=oneSpan;
						}
					}
					else{//regular hours
						if(clock.TimeDisplayed2.Year<1880){
							row.Cells.Add("");
						}
						else{
							oneSpan=clock.TimeDisplayed2-clock.TimeDisplayed1;
							row.Cells.Add(ClockEvents.Format(oneSpan));
							daySpan+=oneSpan;
							weekSpan+=oneSpan;
							periodSpan+=oneSpan;
						}
					}
					//Adjust---------------------------------
					oneAdj=TimeSpan.Zero;
					if(clock.AdjustIsOverridden) {
						oneAdj+=clock.Adjust;
					}
					else {
						oneAdj+=clock.AdjustAuto;//typically zero
					}
					/*
					if(clock.OTimeHours!=TimeSpan.FromHours(-1)) {//overridden
						oneAdj-=clock.OTimeHours;
					}
					else {
						oneAdj-=clock.OTimeAuto;//typically zero
					}*/
					daySpan+=oneAdj;
					weekSpan+=oneAdj;
					periodSpan+=oneAdj;
					row.Cells.Add(oneAdj.ToStringHmm());
					//Overtime------------------------------
					oneOT=TimeSpan.Zero;
					if(clock.OTimeHours!=TimeSpan.FromHours(-1)) {//overridden
						oneOT=clock.OTimeHours;
					}
					else {
						oneOT=clock.OTimeAuto;//typically zero
					}
					otspan+=oneOT;
					daySpan-=oneOT;
					weekSpan-=oneOT;
					periodSpan-=oneOT;
					row.Cells.Add(oneOT.ToStringHmm());
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
						|| GetDateForRow(i+1) != curDate)//or the next row is a different date
					{
						if(IsBreaks){
							if(clock.TimeDisplayed2.Year<1880){//if they have not clocked back in yet from break
								//display the timespan of oneSpan using current time as the other number.
								oneSpan=DateTime.Now-clock.TimeDisplayed1+TimeDelta;
								row.Cells.Add(ClockEvents.Format(false,oneSpan,true));
								daySpan+=oneSpan;
							}
							else{
								row.Cells.Add(ClockEvents.Format(daySpan));
							}
						}
						else{
							row.Cells.Add(ClockEvents.Format(daySpan));
						}
						daySpan=new TimeSpan(0);
					}
					else{//not the last entry for the day
						row.Cells.Add("");
					}
					//Weekly-------------------------------------
					WeeklyTotals[i]=weekSpan;
					if(IsBreaks){
						row.Cells.Add("");
					}
					//if this is the last entry for a given week
					else if(i==mergedAL.Count-1//if this is the last row 
						|| cal.GetWeekOfYear(GetDateForRow(i+1),rule,DayOfWeek.Sunday)//or the next row has a
						!= cal.GetWeekOfYear(clock.TimeDisplayed1.Date,rule,DayOfWeek.Sunday))//different week of year
					{
						row.Cells.Add(ClockEvents.Format(weekSpan));
						weekSpan=new TimeSpan(0);
					}
					else {
						//row.Cells.Add(ClockEvents.Format(weekSpan));
						row.Cells.Add("");
					}
					//Note-----------------------------------------
					row.Cells.Add(clock.Note);
				}
				//adjustment row--------------------------------------------------------------------------------------
				else if(type==typeof(TimeAdjust)){
					adjust=(TimeAdjust)mergedAL[i];
					curDate=adjust.TimeEntry.Date;
					if(curDate==previousDate){
						row.Cells.Add("");
						row.Cells.Add("");
					}
					else{
						row.Cells.Add(curDate.ToShortDateString());
						row.Cells.Add(curDate.DayOfWeek.ToString());
					}
					//altered--------------------------------------
					row.Cells.Add(Lan.g(this,"Adjust"));//2
					row.ColorText=Color.Red;
					//status--------------------------------------
					//row.Cells.Add("");//3
					//in/out------------------------------------------
					row.Cells.Add("");//4
					//time-----------------------------
					row.Cells.Add(adjust.TimeEntry.ToShortTimeString());//5
					//total-------------------------------
					row.Cells.Add("");//
					//Adjust------------------------------
					if(adjust.RegHours.TotalHours==0){
						row.Cells.Add("");//
					}
					else{
						daySpan+=adjust.RegHours;//might be negative
						weekSpan+=adjust.RegHours;
						periodSpan+=adjust.RegHours;
						row.Cells.Add(ClockEvents.Format(adjust.RegHours));//6
					}
					//Overtime------------------------------
					if(adjust.OTimeHours.TotalHours!=0){
						otspan+=adjust.OTimeHours;
						row.Cells.Add(ClockEvents.Format(adjust.OTimeHours));//7
					}
					else{
						row.Cells.Add("");//
					}
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
						|| GetDateForRow(i+1) != curDate)//or the next row is a different date
					{
						row.Cells.Add(ClockEvents.Format(daySpan));//
						daySpan=new TimeSpan(0);
					}
					else{
						row.Cells.Add("");
					}
					//Weekly-------------------------------------
					WeeklyTotals[i]=weekSpan;
					if(IsBreaks){
						row.Cells.Add("");
					}
					//if this is the last entry for a given week
					else if(i==mergedAL.Count-1//if this is the last row 
						|| cal.GetWeekOfYear(GetDateForRow(i+1),rule,DayOfWeek.Sunday)//or the next row has a
						!= cal.GetWeekOfYear(adjust.TimeEntry.Date,rule,DayOfWeek.Sunday))//different week of year
					{
						ODGridCell cell=new ODGridCell(ClockEvents.Format(weekSpan));
						cell.ColorText=Color.Black;
						row.Cells.Add(cell);
						weekSpan=new TimeSpan(0);
					}
					else {
						row.Cells.Add("");
					}
					//Note-----------------------------------------
					row.Cells.Add(adjust.Note);
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			if(IsBreaks){
				textTotal.Text="";
			}
			else{
				textTotal.Text=ClockEvents.Format(periodSpan);
				textOvertime.Text=ClockEvents.Format(otspan);
				textTotal2.Text=ClockEvents.Format(true,periodSpan,false);
				textOvertime2.Text=ClockEvents.Format(true,otspan,false);
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(cannotEdit) {
				return;
			}
			timer1.Enabled=false;
			if(gridMain.Rows[e.Row].Tag.GetType()==typeof(TimeAdjust)) {
				if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
					timer1.Enabled=true;
					return;
				}
				TimeAdjust adjust=(TimeAdjust)gridMain.Rows[e.Row].Tag;
				FormTimeAdjustEdit FormT=new FormTimeAdjustEdit(adjust);
				FormT.ShowDialog();
			}
			else {
				ClockEvent ce=(ClockEvent)gridMain.Rows[e.Row].Tag;
				FormClockEventEdit FormCEE=new FormClockEventEdit(ce);
				FormCEE.ShowDialog();
			}
			FillMain(true);
			timer1.Enabled=true;
		}

		private void butAdj_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			TimeAdjust adjust=new TimeAdjust();
			adjust.EmployeeNum=EmployeeCur.EmployeeNum;
			DateTime dateStop=PIn.Date(textDateStop.Text);
			if(DateTime.Today<=dateStop && DateTime.Today>=PIn.Date(textDateStart.Text)) {
				adjust.TimeEntry=DateTime.Now;
			}
			else {
				adjust.TimeEntry=new DateTime(dateStop.Year,dateStop.Month,dateStop.Day,
					DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
			}
			FormTimeAdjustEdit FormT=new FormTimeAdjustEdit(adjust);
			FormT.IsNew=true;
			FormT.ShowDialog();
			if(FormT.DialogResult==DialogResult.Cancel) {
				return;
			}
			FillMain(true);
		}

		private void butCompute_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			//first, delete all existing overtime entries
			for(int i=0;i<TimeAdjustList.Count;i++) {
				if(TimeAdjustList[i].OTimeHours.TotalHours!=0) {
					TimeAdjusts.Delete(TimeAdjustList[i]);
				}
			}
			//then, fill grid
			FillMain(true);
			Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			//loop through all rows
			for(int i=0;i<mergedAL.Count;i++){
				//ignore rows that aren't weekly totals
				if(i<mergedAL.Count-1//if not the last row
					//if the next row has the same week as this row
					&& cal.GetWeekOfYear(GetDateForRow(i+1),rule,DayOfWeek.Sunday)
					== cal.GetWeekOfYear(GetDateForRow(i),rule,DayOfWeek.Sunday))
				{
					continue;
				}
				if(WeeklyTotals[i]<=TimeSpan.FromHours(40)){
					continue;
				}
				//found a weekly total over 40 hours
				TimeAdjust adjust=new TimeAdjust();
				adjust.EmployeeNum=EmployeeCur.EmployeeNum;
				adjust.TimeEntry=GetDateForRow(i).AddHours(20);//puts it at 8pm on the same day.
				adjust.OTimeHours=WeeklyTotals[i]-TimeSpan.FromHours(40);
				adjust.RegHours=-adjust.OTimeHours;
				TimeAdjusts.Insert(adjust);
			}
			FillMain(true);
		}

		private void butDaily_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}

		}

		private void butPrint_Click(object sender,EventArgs e) {
			linesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
			#if DEBUG
			PrintPreview printPreview=new PrintPreview(PrintSituation.Default,pd,1);
			printPreview.ShowDialog();
			#else
				try {
					if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				}
				catch {
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			#endif
		}

		///<summary>raised for each page to be printed.</summary>
		private void pd_PrintPage(object sender,PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			float yPos=75;
			float xPos=55;
			string str;
			Font font=new Font(FontFamily.GenericSansSerif,8);
			Font fontTitle=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
			Font fontHeader=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
			SolidBrush brush=new SolidBrush(Color.Black);
			Pen pen=new Pen(Color.Black);
			//Title
			str=EmployeeCur.FName+" "+EmployeeCur.LName;
			g.DrawString(str,fontTitle,brush,xPos,yPos);
			yPos+=30;
			//define columns
			int[] colW=new int[11];
			colW[0]=70;//date
			colW[1]=70;//weekday
			colW[2]=50;//altered
			colW[3]=60;//in
			colW[4]=60;//out
			colW[5]=50;//total
			colW[6]=50;//adjust
			colW[7]=55;//overtime
			colW[8]=50;//daily
			colW[9]=50;//weekly
			colW[10]=160;//note
			int[] colPos=new int[colW.Length+1];
			colPos[0]=45;
			for(int i=1;i<colPos.Length;i++) {
				colPos[i]=colPos[i-1]+colW[i-1];
			}
			string[] ColCaption=new string[11];
			ColCaption[0]=Lan.g(this,"Date");
			ColCaption[1]=Lan.g(this,"Weekday");
			ColCaption[2]=Lan.g(this,"Altered");
			ColCaption[3]=Lan.g(this,"In");
			ColCaption[4]=Lan.g(this,"Out");
			ColCaption[5]=Lan.g(this,"Total");
			ColCaption[6]=Lan.g(this,"Adjust");
			ColCaption[7]=Lan.g(this,"Overtime");
			ColCaption[8]=Lan.g(this,"Daily");
			ColCaption[9]=Lan.g(this,"Weekly");
			ColCaption[10]=Lan.g(this,"Note");
			//column headers-----------------------------------------------------------------------------------------
			e.Graphics.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			e.Graphics.DrawRectangle(pen,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			for(int i=1;i<colPos.Length;i++) {
				e.Graphics.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+18);
			}
			//Prints the Column Titles
			for(int i=0;i<ColCaption.Length;i++) {
				e.Graphics.DrawString(ColCaption[i],fontHeader,brush,colPos[i]+2,yPos+1);
			}
			yPos+=18;
			while(yPos < e.PageBounds.Height-75-50-32-16 && linesPrinted < gridMain.Rows.Count) {
				for(int i=0;i<colPos.Length-1;i++) {
					e.Graphics.DrawString(gridMain.Rows[linesPrinted].Cells[i].Text,font,brush
						,new RectangleF(colPos[i]+2,yPos,colPos[i+1]-colPos[i]-5,font.GetHeight(e.Graphics)));
				}
				//Column lines		
				for(int i=0;i<colPos.Length;i++) {
					e.Graphics.DrawLine(Pens.Gray,colPos[i],yPos+16,colPos[i],yPos);
				}
				linesPrinted++;
				yPos+=16;
				e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
			}
			//bottom line
			//e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
			//totals will print on every page for simplicity
			yPos+=10;
			g.DrawString(Lan.g(this,"Regular Time")+": "+textTotal.Text+" ("+textTotal2.Text+")",fontHeader,brush,xPos,yPos);
			yPos+=16;
			g.DrawString(Lan.g(this,"Overtime")+": "+textOvertime.Text+" ("+textOvertime2.Text+")",fontHeader,brush,xPos,yPos);
			if(linesPrinted==gridMain.Rows.Count) {
				e.HasMorePages=false;
			}
			else {
				e.HasMorePages=true;
			}
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			if(IsBreaks){
				FillMain(false);
			}
		}

		

		

	

		

		

		

		

		

		

		

		

		


	}
}





















