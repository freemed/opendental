using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClockEventEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textTimeEntered1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listStatus;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.TextBox textTimeDisplayed1;
		private OpenDental.UI.Button butDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private TextBox textTimeEntered2;
		private Label label5;
		private Label label6;
		private TextBox textTimeDisplayed2;
		private OpenDental.UI.Button butNow2;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.Button butNow1;
		private TextBox textOTimeHours;
		private Label label7;
		private TextBox textTotalTime;
		private Label label8;
		private TextBox textRegTime;
		private Label label9;
		private GroupBox groupTimeSpans;
		private TextBox textOTimeAuto;
		private TextBox textAdjust;
		private Label label12;
		private Label label11;
		private Label label10;
		private TextBox textAdjustAuto;
		private ClockEvent ClockEventCur;

		///<summary></summary>
		public FormClockEventEdit(ClockEvent clockEventCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			ClockEventCur=clockEventCur.Copy();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClockEventEdit));
			this.textTimeEntered1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textTimeDisplayed1 = new System.Windows.Forms.TextBox();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butNow1 = new OpenDental.UI.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textTimeEntered2 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butClear = new OpenDental.UI.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.butNow2 = new OpenDental.UI.Button();
			this.textTimeDisplayed2 = new System.Windows.Forms.TextBox();
			this.textOTimeHours = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTotalTime = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textRegTime = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.groupTimeSpans = new System.Windows.Forms.GroupBox();
			this.textOTimeAuto = new System.Windows.Forms.TextBox();
			this.textAdjust = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textAdjustAuto = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupTimeSpans.SuspendLayout();
			this.SuspendLayout();
			// 
			// textTimeEntered1
			// 
			this.textTimeEntered1.Location = new System.Drawing.Point(101,19);
			this.textTimeEntered1.Name = "textTimeEntered1";
			this.textTimeEntered1.ReadOnly = true;
			this.textTimeEntered1.Size = new System.Drawing.Size(156,20);
			this.textTimeEntered1.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94,16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Entered";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93,16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Displayed";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTimeDisplayed1
			// 
			this.textTimeDisplayed1.Location = new System.Drawing.Point(101,43);
			this.textTimeDisplayed1.Name = "textTimeDisplayed1";
			this.textTimeDisplayed1.Size = new System.Drawing.Size(156,20);
			this.textTimeDisplayed1.TabIndex = 4;
			this.textTimeDisplayed1.TextChanged += new System.EventHandler(this.textTimeDisplayed1_TextChanged);
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(179,118);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(120,43);
			this.listStatus.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(72,118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105,16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Out Status";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(72,298);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105,16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(179,297);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(317,110);
			this.textNote.TabIndex = 11;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textTimeEntered1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butNow1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textTimeDisplayed1);
			this.groupBox1.Location = new System.Drawing.Point(79,12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(267,100);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Clock In Date and Time";
			// 
			// butNow1
			// 
			this.butNow1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNow1.Autosize = true;
			this.butNow1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow1.CornerRadius = 4F;
			this.butNow1.Location = new System.Drawing.Point(101,69);
			this.butNow1.Name = "butNow1";
			this.butNow1.Size = new System.Drawing.Size(70,24);
			this.butNow1.TabIndex = 17;
			this.butNow1.Text = "Now";
			this.butNow1.Click += new System.EventHandler(this.butNow1_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textTimeEntered2);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.butClear);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.butNow2);
			this.groupBox2.Controls.Add(this.textTimeDisplayed2);
			this.groupBox2.Location = new System.Drawing.Point(363,12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(267,100);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Clock Out Date and Time";
			// 
			// textTimeEntered2
			// 
			this.textTimeEntered2.Location = new System.Drawing.Point(101,19);
			this.textTimeEntered2.Name = "textTimeEntered2";
			this.textTimeEntered2.ReadOnly = true;
			this.textTimeEntered2.Size = new System.Drawing.Size(156,20);
			this.textTimeEntered2.TabIndex = 2;
			this.textTimeEntered2.TextChanged += new System.EventHandler(this.textTimeEntered2_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(94,16);
			this.label5.TabIndex = 3;
			this.label5.Text = "Entered";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butClear.Location = new System.Drawing.Point(177,69);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(80,24);
			this.butClear.TabIndex = 16;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6,45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93,16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Displayed";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butNow2
			// 
			this.butNow2.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNow2.Autosize = true;
			this.butNow2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNow2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNow2.CornerRadius = 4F;
			this.butNow2.Location = new System.Drawing.Point(101,69);
			this.butNow2.Name = "butNow2";
			this.butNow2.Size = new System.Drawing.Size(70,24);
			this.butNow2.TabIndex = 15;
			this.butNow2.Text = "Now";
			this.butNow2.Click += new System.EventHandler(this.butNow2_Click);
			// 
			// textTimeDisplayed2
			// 
			this.textTimeDisplayed2.Location = new System.Drawing.Point(101,43);
			this.textTimeDisplayed2.Name = "textTimeDisplayed2";
			this.textTimeDisplayed2.Size = new System.Drawing.Size(156,20);
			this.textTimeDisplayed2.TabIndex = 4;
			this.textTimeDisplayed2.TextChanged += new System.EventHandler(this.textTimeDisplayed2_TextChanged);
			// 
			// textOTimeHours
			// 
			this.textOTimeHours.Location = new System.Drawing.Point(176,78);
			this.textOTimeHours.Name = "textOTimeHours";
			this.textOTimeHours.Size = new System.Drawing.Size(68,20);
			this.textOTimeHours.TabIndex = 25;
			this.textOTimeHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textOTimeHours.TextChanged += new System.EventHandler(this.textOvertime_TextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8,78);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(92,18);
			this.label7.TabIndex = 24;
			this.label7.Text = "- Overtime";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTotalTime
			// 
			this.textTotalTime.Location = new System.Drawing.Point(100,34);
			this.textTotalTime.Name = "textTotalTime";
			this.textTotalTime.ReadOnly = true;
			this.textTotalTime.Size = new System.Drawing.Size(68,20);
			this.textTotalTime.TabIndex = 27;
			this.textTotalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8,34);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(92,18);
			this.label8.TabIndex = 26;
			this.label8.Text = "Total Time";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRegTime
			// 
			this.textRegTime.Location = new System.Drawing.Point(100,100);
			this.textRegTime.Name = "textRegTime";
			this.textRegTime.ReadOnly = true;
			this.textRegTime.Size = new System.Drawing.Size(68,20);
			this.textRegTime.TabIndex = 29;
			this.textRegTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8,100);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(92,18);
			this.label9.TabIndex = 28;
			this.label9.Text = "Regular Time";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupTimeSpans
			// 
			this.groupTimeSpans.Controls.Add(this.textOTimeAuto);
			this.groupTimeSpans.Controls.Add(this.textAdjust);
			this.groupTimeSpans.Controls.Add(this.label12);
			this.groupTimeSpans.Controls.Add(this.label11);
			this.groupTimeSpans.Controls.Add(this.label10);
			this.groupTimeSpans.Controls.Add(this.textAdjustAuto);
			this.groupTimeSpans.Controls.Add(this.label8);
			this.groupTimeSpans.Controls.Add(this.textRegTime);
			this.groupTimeSpans.Controls.Add(this.label7);
			this.groupTimeSpans.Controls.Add(this.label9);
			this.groupTimeSpans.Controls.Add(this.textOTimeHours);
			this.groupTimeSpans.Controls.Add(this.textTotalTime);
			this.groupTimeSpans.Location = new System.Drawing.Point(79,164);
			this.groupTimeSpans.Name = "groupTimeSpans";
			this.groupTimeSpans.Size = new System.Drawing.Size(267,127);
			this.groupTimeSpans.TabIndex = 30;
			this.groupTimeSpans.TabStop = false;
			this.groupTimeSpans.Text = "Time Spans";
			// 
			// textOTimeAuto
			// 
			this.textOTimeAuto.Location = new System.Drawing.Point(100,78);
			this.textOTimeAuto.Name = "textOTimeAuto";
			this.textOTimeAuto.ReadOnly = true;
			this.textOTimeAuto.Size = new System.Drawing.Size(68,20);
			this.textOTimeAuto.TabIndex = 35;
			this.textOTimeAuto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textAdjust
			// 
			this.textAdjust.Location = new System.Drawing.Point(176,56);
			this.textAdjust.Name = "textAdjust";
			this.textAdjust.Size = new System.Drawing.Size(68,20);
			this.textAdjust.TabIndex = 34;
			this.textAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textAdjust.TextChanged += new System.EventHandler(this.textAdjust_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(176,13);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(68,18);
			this.label12.TabIndex = 33;
			this.label12.Text = "Override";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(100,13);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(68,18);
			this.label11.TabIndex = 32;
			this.label11.Text = "Calculated";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8,56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(92,18);
			this.label10.TabIndex = 30;
			this.label10.Text = "+ Adj";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAdjustAuto
			// 
			this.textAdjustAuto.Location = new System.Drawing.Point(100,56);
			this.textAdjustAuto.Name = "textAdjustAuto";
			this.textAdjustAuto.ReadOnly = true;
			this.textAdjustAuto.Size = new System.Drawing.Size(68,20);
			this.textAdjustAuto.TabIndex = 31;
			this.textAdjustAuto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			this.butDelete.Location = new System.Drawing.Point(31,435);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84,24);
			this.butDelete.TabIndex = 12;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(464,434);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butCancel.Location = new System.Drawing.Point(555,434);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormClockEventEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(669,477);
			this.Controls.Add(this.groupTimeSpans);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listStatus);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClockEventEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Clock Event";
			this.Load += new System.EventHandler(this.FormClockEventEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupTimeSpans.ResumeLayout(false);
			this.groupTimeSpans.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClockEventEdit_Load(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardDeleteEntry,ClockEventCur.TimeEntered1,true)) {
				butDelete.Enabled=false;
				butClear.Enabled=false;
				butNow1.Enabled=false;
				butNow2.Enabled=false;
			}
			if(ClockEventCur.ClockStatus==TimeClockStatus.Break){
				groupBox1.Text=Lan.g(this,"Clock Out Date and Time");
				groupBox2.Text=Lan.g(this,"Clock In Date and Time");
				groupTimeSpans.Visible=false;
			}
			textTimeEntered1.Text=ClockEventCur.TimeEntered1.ToString();
			textTimeDisplayed1.Text=ClockEventCur.TimeDisplayed1.ToString();
			if(ClockEventCur.TimeEntered2.Year>1880){
				textTimeEntered2.Text=ClockEventCur.TimeEntered2.ToString();
			}
			if(ClockEventCur.TimeDisplayed2.Year>1880){
				textTimeDisplayed2.Text=ClockEventCur.TimeDisplayed2.ToString();
			}
			listStatus.Items.Clear();
			for(int i=0;i<Enum.GetNames(typeof(TimeClockStatus)).Length;i++){
				listStatus.Items.Add(Lan.g("enumTimeClockStatus",Enum.GetNames(typeof(TimeClockStatus))[i]));
			}
			listStatus.SelectedIndex=(int)ClockEventCur.ClockStatus;//all clockevents have a status
			textAdjustAuto.Text=ClockEvents.Format(ClockEventCur.AdjustAuto);
			if(ClockEventCur.AdjustIsOverridden) {
				if(ClockEventCur.Adjust==TimeSpan.Zero) {
					textAdjust.Text="0";
				}
				else {
					textAdjust.Text=ClockEvents.Format(ClockEventCur.Adjust);
				}
			}
			else {
				textAdjust.Text="";
			}
			textOTimeAuto.Text=ClockEvents.Format(ClockEventCur.OTimeAuto);
			if(ClockEventCur.OTimeHours==TimeSpan.FromHours(-1)) {//no override
				textOTimeHours.Text="";
			}
			else {
				textOTimeHours.Text=ClockEvents.Format(ClockEventCur.OTimeHours);
			}
			textNote.Text=ClockEventCur.Note;
		}

		///<summary>Does not alter the overrides, but only the auto calc boxes.  Triggered by many things on this form.  It's better to have it be triggered too frequently than to miss something.</summary>
		private void FillTimeSpans() {
			if(ClockEventCur.ClockStatus==TimeClockStatus.Break){//TimeSpans not showing
				return;
			}
			if(textTimeEntered2.Text=="" || textTimeDisplayed2.Text=="") {
				textTotalTime.Text="";
				textRegTime.Text="";
				return;
			}
			try{
				DateTime.Parse(textTimeDisplayed1.Text);//because this must always be valid
				DateTime.Parse(textTimeDisplayed2.Text);//this must also be filled in order to calculate timespans
			}
			catch{//an invalid date/time.
				//textTotalTime.Text="";
				//textRegTime.Text="";
				return;
			}
			DateTime dt1=DateTime.Parse(textTimeDisplayed1.Text);
			DateTime dt2=DateTime.Parse(textTimeDisplayed2.Text);
			if(dt1 > dt2){
				return;
			}
			TimeSpan totalTime=dt2-dt1;
			textTotalTime.Text=ClockEvents.Format(totalTime);
			TimeSpan overtime=ClockEventCur.OTimeAuto;
			if(textOTimeHours.Text!="") {
				try {
					if(textOTimeHours.Text.Contains(":")) {
						overtime=TimeSpan.Parse(textOTimeHours.Text);
					}
					else {
						overtime=TimeSpan.FromHours(Double.Parse(textOTimeHours.Text));
					}
				}
				catch {
					return;
				}
			}
			TimeSpan adjust=ClockEventCur.AdjustAuto;
			if(textAdjust.Text!="") {
				try {
					if(textAdjust.Text.Contains(":")) {
						adjust=TimeSpan.Parse(textAdjust.Text);
					}
					else {
						adjust=TimeSpan.FromHours(Double.Parse(textAdjust.Text));
					}
				}
				catch {
					return;
				}
			}
			TimeSpan regTime=totalTime-overtime+adjust;//adjust is typically a negative value
			if(regTime<TimeSpan.Zero) {
				textRegTime.Text="";
				return;
			}
			textRegTime.Text=ClockEvents.Format(regTime);
		}

		private void textTimeDisplayed2_TextChanged(object sender,EventArgs e) {
			FillTimeSpans();
		}

		private void textTimeDisplayed1_TextChanged(object sender,EventArgs e) {
			FillTimeSpans();
		}

		private void textTimeEntered2_TextChanged(object sender,EventArgs e) {
			FillTimeSpans();
		}

		private void textAdjust_TextChanged(object sender,EventArgs e) {
			FillTimeSpans();
		}

		private void textOvertime_TextChanged(object sender,EventArgs e) {
			FillTimeSpans();
		}

		private void butNow1_Click(object sender,EventArgs e) {
			textTimeDisplayed1.Text=DateTime.Now.ToString();
		}

		private void butNow2_Click(object sender,EventArgs e) {
			textTimeDisplayed2.Text=DateTime.Now.ToString();
			if(textTimeEntered2.Text=="") {//only set the time entered if it's blank
				textTimeEntered2.Text=MiscData.GetNowDateTime().ToString();
				ClockEventCur.TimeEntered2=MiscData.GetNowDateTime();
			}
			FillTimeSpans();//not really needed because of the TextChanged event, but might prevent a bug.
		}

		private void butClear_Click(object sender,EventArgs e) {
			textTimeDisplayed2.Text="";
			textTimeEntered2.Text="";
			ClockEventCur.TimeEntered2=DateTime.MinValue;
			FillTimeSpans();//not really needed because of the TextChanged event, but might prevent a bug.
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete this clock event?")){
				return;
			}
			ClockEvents.Delete(ClockEventCur.ClockEventNum);
			SecurityLogs.MakeLogEntry(Permissions.TimecardDeleteEntry,0,
				"Original entry: "+ClockEventCur.TimeEntered1.ToString());
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DateTime timeDisplayed1=DateTime.MinValue;
			try{
				timeDisplayed1=DateTime.Parse(textTimeDisplayed1.Text);//because this must always be valid
			}
			catch{
				if(ClockEventCur.ClockStatus==TimeClockStatus.Break){
					MsgBox.Show(this,"Please enter a valid clock-out date and time.");
				}
				else{
					MsgBox.Show(this,"Please enter a valid clock-in date and time.");
				}
				return;
			}
			if(timeDisplayed1.Date > DateTime.Today) {
				if(ClockEventCur.ClockStatus==TimeClockStatus.Break){
					MsgBox.Show(this,"Clock-out date cannot be a future date.");
				}
				else{
					MsgBox.Show(this,"Clock-in date cannot be a future date.");
				}
				return;
			}
			DateTime timeDisplayed2=DateTime.MinValue;
			if(textTimeDisplayed2.Text!=""){//it can be empty
				try{
					timeDisplayed2=DateTime.Parse(textTimeDisplayed2.Text);
				}
				catch{
					if(ClockEventCur.ClockStatus==TimeClockStatus.Break){
						MsgBox.Show(this,"Please enter a valid clock-in date and time.");
					}
					else{
						MsgBox.Show(this,"Please enter a valid clock-out date and time.");
					}
					return;
				}
			}
			if(timeDisplayed2.Date > DateTime.Today) {
				if(ClockEventCur.ClockStatus==TimeClockStatus.Break){
					MsgBox.Show(this,"Clock-in date cannot be a future date.");
				}
				else{
					MsgBox.Show(this,"Clock-out date cannot be a future date.");
				}
				return;
			}
			if(textTimeDisplayed2.Text!="" && timeDisplayed1 > timeDisplayed2){
				if(ClockEventCur.ClockStatus==TimeClockStatus.Break) {
					MsgBox.Show(this,"Break end time cannot be earlier than break start time.");
					return;
				}
				else {
					MsgBox.Show(this,"Clock out time cannot be earlier than clock in time.");
					return;
				}
			}
			if(textTimeDisplayed2.Text=="" && textTimeEntered2.Text!="") {//user is trying to clear the time manually
				MsgBox.Show(this,"A date and time must be entered in the second box, or use the Clear button.");
				return;
			}
			TimeSpan overtime=TimeSpan.Zero;
			TimeSpan adjust=TimeSpan.Zero;
			if(ClockEventCur.ClockStatus!=TimeClockStatus.Break) {
				if(textOTimeHours.Text!="") {
					try {
						if(textOTimeHours.Text.Contains(":")) {
							overtime=TimeSpan.Parse(textOTimeHours.Text);
						}
						else {
							overtime=TimeSpan.FromHours(Double.Parse(textOTimeHours.Text));
						}
						if(overtime < TimeSpan.Zero) {
							MsgBox.Show(this,"Overtime must be positive.");
							return;
						}
					}
					catch {
						MsgBox.Show(this,"Please enter a valid overtime amount.");
						return;
					}
				}
				if(textAdjust.Text!="") {
					try {
						if(textAdjust.Text.Contains(":")) {
							adjust=TimeSpan.Parse(textAdjust.Text);
						}
						else {
							adjust=TimeSpan.FromHours(Double.Parse(textAdjust.Text));
						}
					}
					catch {
						MsgBox.Show(this,"Please enter a valid adjustment amount.");
						return;
					}
				}
				if(textRegTime.Text==""){//must be invalid calc.
					MsgBox.Show(this,"Overtime and adjustments cannot excede the total time.");
					return;
				}
			}
			//timeEntered2 is largely taken care of, except for this one situation
			if(textTimeDisplayed2.Text!="" && textTimeEntered2.Text=="") {
				ClockEventCur.TimeEntered2=MiscData.GetNowDateTime();
			}
			ClockEventCur.TimeDisplayed1=timeDisplayed1;
			ClockEventCur.TimeDisplayed2=timeDisplayed2;
			ClockEventCur.ClockStatus=(TimeClockStatus)listStatus.SelectedIndex;
			if(textAdjust.Text=="") {//no override
				ClockEventCur.AdjustIsOverridden=false;
				ClockEventCur.Adjust=TimeSpan.Zero;
			}
			else {
				ClockEventCur.AdjustIsOverridden=true;
				ClockEventCur.Adjust=adjust;
			}
			if(textOTimeHours.Text=="") {//no override
				ClockEventCur.OTimeHours=TimeSpan.FromHours(-1d);
			}
			else {
				ClockEventCur.OTimeHours=overtime;
			}
			//The two auto fields are only set externally.
			ClockEventCur.Note=textNote.Text;
			ClockEvents.Update(ClockEventCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
	

		

		

	


	}
}






































