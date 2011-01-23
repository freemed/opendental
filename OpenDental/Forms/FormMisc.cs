using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;

namespace OpenDental{
///<summary></summary>
	public class FormMisc : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private IContainer components;
		private System.Windows.Forms.TextBox textMainWindowTitle;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidNumber textSigInterval;
		private OpenDental.UI.Button butLanguages;
		private Label label4;
		private ToolTip toolTip1;
		private ComboBox comboShowID;
		private CheckBox checkTaskListAlwaysShow;
		private CheckBox checkTasksCheckOnStartup;
		private CheckBox checkBoxTaskKeepListHidden;
		//private ComputerPref computerPref;
		private ValidNumber validNumX;
		private Label labelX;
		private GroupBox groupBox2;
		private GroupBox groupBoxTaskDefaults;
		private ValidNumber validNumY;
		private Label labelY;
		private RadioButton radioRight;
		private RadioButton radioBottom;
		private Label label15;
		private Label label17;
		private GroupBox groupBox6;
		private CheckBox checkTitleBarShowSite;
		private TextBox textWebServiceServerName;
		private Label label2;
		private CheckBox checkReportsProcDate;
		private CheckBox checkReportsShowPatNum;
		private CheckBox checkReportProdWO;
		private GroupBox groupBox1;
		private CheckBox checkTasksNewTrackedByUser;
		private Label label1;
		//private List<Def> posAdjTypes;

		///<summary></summary>
		public FormMisc(){
			InitializeComponent();
			Lan.F(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMisc));
			this.textMainWindowTitle = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.radioBottom = new System.Windows.Forms.RadioButton();
			this.radioRight = new System.Windows.Forms.RadioButton();
			this.checkTaskListAlwaysShow = new System.Windows.Forms.CheckBox();
			this.checkTasksCheckOnStartup = new System.Windows.Forms.CheckBox();
			this.checkBoxTaskKeepListHidden = new System.Windows.Forms.CheckBox();
			this.validNumY = new OpenDental.ValidNumber();
			this.validNumX = new OpenDental.ValidNumber();
			this.checkTasksNewTrackedByUser = new System.Windows.Forms.CheckBox();
			this.labelX = new System.Windows.Forms.Label();
			this.labelY = new System.Windows.Forms.Label();
			this.comboShowID = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBoxTaskDefaults = new System.Windows.Forms.GroupBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkTitleBarShowSite = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butLanguages = new OpenDental.UI.Button();
			this.textSigInterval = new OpenDental.ValidNumber();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textWebServiceServerName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkReportsProcDate = new System.Windows.Forms.CheckBox();
			this.checkReportsShowPatNum = new System.Windows.Forms.CheckBox();
			this.checkReportProdWO = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2.SuspendLayout();
			this.groupBoxTaskDefaults.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textMainWindowTitle
			// 
			this.textMainWindowTitle.Location = new System.Drawing.Point(170,14);
			this.textMainWindowTitle.Name = "textMainWindowTitle";
			this.textMainWindowTitle.Size = new System.Drawing.Size(267,20);
			this.textMainWindowTitle.TabIndex = 38;
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(41,138);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(333,35);
			this.label3.TabIndex = 56;
			this.label3.Text = "Process Signal Interval in seconds.  Usually every 6 to 20 seconds.  Leave blank " +
    "to disable autorefresh";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(59,183);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(298,17);
			this.label4.TabIndex = 64;
			this.label4.Text = "Languages used by patients.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.AutoPopDelay = 600000;
			this.toolTip1.InitialDelay = 0;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 0;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Help";
			// 
			// radioBottom
			// 
			this.radioBottom.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioBottom.Location = new System.Drawing.Point(185,38);
			this.radioBottom.Name = "radioBottom";
			this.radioBottom.Size = new System.Drawing.Size(96,17);
			this.radioBottom.TabIndex = 190;
			this.radioBottom.TabStop = true;
			this.radioBottom.Text = "Dock Bottom";
			this.radioBottom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioBottom.UseVisualStyleBackColor = true;
			// 
			// radioRight
			// 
			this.radioRight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioRight.Location = new System.Drawing.Point(84,38);
			this.radioRight.Name = "radioRight";
			this.radioRight.Size = new System.Drawing.Size(99,17);
			this.radioRight.TabIndex = 191;
			this.radioRight.TabStop = true;
			this.radioRight.Text = "Dock Right";
			this.radioRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioRight.UseVisualStyleBackColor = true;
			// 
			// checkTaskListAlwaysShow
			// 
			this.checkTaskListAlwaysShow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTaskListAlwaysShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTaskListAlwaysShow.Location = new System.Drawing.Point(158,34);
			this.checkTaskListAlwaysShow.Name = "checkTaskListAlwaysShow";
			this.checkTaskListAlwaysShow.Size = new System.Drawing.Size(190,14);
			this.checkTaskListAlwaysShow.TabIndex = 74;
			this.checkTaskListAlwaysShow.Text = "Global - Always show Task List";
			this.checkTaskListAlwaysShow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.checkTaskListAlwaysShow,resources.GetString("checkTaskListAlwaysShow.ToolTip"));
			this.checkTaskListAlwaysShow.CheckedChanged += new System.EventHandler(this.checkTaskListAlwaysShow_CheckedChanged);
			// 
			// checkTasksCheckOnStartup
			// 
			this.checkTasksCheckOnStartup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTasksCheckOnStartup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTasksCheckOnStartup.Location = new System.Drawing.Point(118,12);
			this.checkTasksCheckOnStartup.Name = "checkTasksCheckOnStartup";
			this.checkTasksCheckOnStartup.Size = new System.Drawing.Size(230,19);
			this.checkTasksCheckOnStartup.TabIndex = 75;
			this.checkTasksCheckOnStartup.Text = "Check for new user tasks on startup";
			this.checkTasksCheckOnStartup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBoxTaskKeepListHidden
			// 
			this.checkBoxTaskKeepListHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxTaskKeepListHidden.Location = new System.Drawing.Point(63,19);
			this.checkBoxTaskKeepListHidden.Name = "checkBoxTaskKeepListHidden";
			this.checkBoxTaskKeepListHidden.Size = new System.Drawing.Size(218,20);
			this.checkBoxTaskKeepListHidden.TabIndex = 185;
			this.checkBoxTaskKeepListHidden.Text = "Don\'t show on this computer";
			this.checkBoxTaskKeepListHidden.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.checkBoxTaskKeepListHidden,resources.GetString("checkBoxTaskKeepListHidden.ToolTip"));
			this.checkBoxTaskKeepListHidden.UseVisualStyleBackColor = true;
			this.checkBoxTaskKeepListHidden.CheckedChanged += new System.EventHandler(this.checkBoxTaskKeepListHidden_CheckedChanged);
			// 
			// validNumY
			// 
			this.validNumY.Location = new System.Drawing.Point(235,59);
			this.validNumY.MaxLength = 4;
			this.validNumY.MaxVal = 1200;
			this.validNumY.MinVal = 300;
			this.validNumY.Name = "validNumY";
			this.validNumY.Size = new System.Drawing.Size(47,20);
			this.validNumY.TabIndex = 188;
			this.validNumY.Text = "542";
			this.validNumY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// validNumX
			// 
			this.validNumX.Location = new System.Drawing.Point(115,59);
			this.validNumX.MaxLength = 4;
			this.validNumX.MaxVal = 2000;
			this.validNumX.MinVal = 300;
			this.validNumX.Name = "validNumX";
			this.validNumX.Size = new System.Drawing.Size(47,20);
			this.validNumX.TabIndex = 186;
			this.validNumX.Text = "542";
			this.validNumX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkTasksNewTrackedByUser
			// 
			this.checkTasksNewTrackedByUser.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTasksNewTrackedByUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTasksNewTrackedByUser.Location = new System.Drawing.Point(48,53);
			this.checkTasksNewTrackedByUser.Name = "checkTasksNewTrackedByUser";
			this.checkTasksNewTrackedByUser.Size = new System.Drawing.Size(300,14);
			this.checkTasksNewTrackedByUser.TabIndex = 77;
			this.checkTasksNewTrackedByUser.Text = "New/Viewed status tracked by individual user";
			this.checkTasksNewTrackedByUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelX
			// 
			this.labelX.Location = new System.Drawing.Point(47,59);
			this.labelX.Name = "labelX";
			this.labelX.Size = new System.Drawing.Size(62,18);
			this.labelX.TabIndex = 187;
			this.labelX.Text = "X Default";
			this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelY
			// 
			this.labelY.Location = new System.Drawing.Point(167,59);
			this.labelY.Name = "labelY";
			this.labelY.Size = new System.Drawing.Size(62,18);
			this.labelY.TabIndex = 189;
			this.labelY.Text = "Y Default";
			this.labelY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboShowID
			// 
			this.comboShowID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboShowID.FormattingEnabled = true;
			this.comboShowID.Location = new System.Drawing.Point(307,36);
			this.comboShowID.Name = "comboShowID";
			this.comboShowID.Size = new System.Drawing.Size(130,21);
			this.comboShowID.TabIndex = 72;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkTasksNewTrackedByUser);
			this.groupBox2.Controls.Add(this.groupBoxTaskDefaults);
			this.groupBox2.Controls.Add(this.checkTaskListAlwaysShow);
			this.groupBox2.Controls.Add(this.checkTasksCheckOnStartup);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(101,308);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(386,166);
			this.groupBox2.TabIndex = 188;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Task List";
			// 
			// groupBoxTaskDefaults
			// 
			this.groupBoxTaskDefaults.Controls.Add(this.radioRight);
			this.groupBoxTaskDefaults.Controls.Add(this.radioBottom);
			this.groupBoxTaskDefaults.Controls.Add(this.validNumY);
			this.groupBoxTaskDefaults.Controls.Add(this.labelY);
			this.groupBoxTaskDefaults.Controls.Add(this.validNumX);
			this.groupBoxTaskDefaults.Controls.Add(this.labelX);
			this.groupBoxTaskDefaults.Controls.Add(this.checkBoxTaskKeepListHidden);
			this.groupBoxTaskDefaults.Enabled = false;
			this.groupBoxTaskDefaults.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBoxTaskDefaults.Location = new System.Drawing.Point(67,75);
			this.groupBoxTaskDefaults.Name = "groupBoxTaskDefaults";
			this.groupBoxTaskDefaults.Size = new System.Drawing.Size(297,83);
			this.groupBoxTaskDefaults.TabIndex = 76;
			this.groupBoxTaskDefaults.TabStop = false;
			this.groupBoxTaskDefaults.Text = "Local Computer Default Settings";
			// 
			// label15
			// 
			this.label15.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label15.Location = new System.Drawing.Point(86,17);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(83,17);
			this.label15.TabIndex = 39;
			this.label15.Text = "Title Text";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label17.Location = new System.Drawing.Point(110,39);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(195,15);
			this.label17.TabIndex = 73;
			this.label17.Text = "Show ID in title bar";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.checkTitleBarShowSite);
			this.groupBox6.Controls.Add(this.textMainWindowTitle);
			this.groupBox6.Controls.Add(this.label15);
			this.groupBox6.Controls.Add(this.comboShowID);
			this.groupBox6.Controls.Add(this.label17);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Location = new System.Drawing.Point(12,46);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(453,83);
			this.groupBox6.TabIndex = 195;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Main Window Title";
			// 
			// checkTitleBarShowSite
			// 
			this.checkTitleBarShowSite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkTitleBarShowSite.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTitleBarShowSite.Location = new System.Drawing.Point(75,60);
			this.checkTitleBarShowSite.Name = "checkTitleBarShowSite";
			this.checkTitleBarShowSite.Size = new System.Drawing.Size(362,17);
			this.checkTitleBarShowSite.TabIndex = 74;
			this.checkTitleBarShowSite.Text = "Show Site (public health must also be turned on)";
			this.checkTitleBarShowSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(410,17);
			this.label1.TabIndex = 196;
			this.label1.Text = "See Setup | Modules for setup options that were previously in this window.";
			// 
			// butLanguages
			// 
			this.butLanguages.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLanguages.Autosize = true;
			this.butLanguages.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLanguages.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLanguages.CornerRadius = 4F;
			this.butLanguages.Location = new System.Drawing.Point(360,178);
			this.butLanguages.Name = "butLanguages";
			this.butLanguages.Size = new System.Drawing.Size(88,24);
			this.butLanguages.TabIndex = 63;
			this.butLanguages.Text = "Edit Languages";
			this.butLanguages.Click += new System.EventHandler(this.butLanguages_Click);
			// 
			// textSigInterval
			// 
			this.textSigInterval.Location = new System.Drawing.Point(375,147);
			this.textSigInterval.MaxVal = 1000000;
			this.textSigInterval.MinVal = 1;
			this.textSigInterval.Name = "textSigInterval";
			this.textSigInterval.Size = new System.Drawing.Size(74,20);
			this.textSigInterval.TabIndex = 57;
			this.textSigInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			this.butCancel.Location = new System.Drawing.Point(548,452);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 8;
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
			this.butOK.Location = new System.Drawing.Point(548,414);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textWebServiceServerName
			// 
			this.textWebServiceServerName.Location = new System.Drawing.Point(284,212);
			this.textWebServiceServerName.Name = "textWebServiceServerName";
			this.textWebServiceServerName.Size = new System.Drawing.Size(165,20);
			this.textWebServiceServerName.TabIndex = 197;
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(4,213);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(279,17);
			this.label2.TabIndex = 198;
			this.label2.Text = "Web Service: Server Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkReportsProcDate
			// 
			this.checkReportsProcDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReportsProcDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportsProcDate.Location = new System.Drawing.Point(75,13);
			this.checkReportsProcDate.Name = "checkReportsProcDate";
			this.checkReportsProcDate.Size = new System.Drawing.Size(362,17);
			this.checkReportsProcDate.TabIndex = 199;
			this.checkReportsProcDate.Text = "Default to using Proc Date for PPO writeoffs";
			this.checkReportsProcDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkReportsShowPatNum
			// 
			this.checkReportsShowPatNum.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReportsShowPatNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportsShowPatNum.Location = new System.Drawing.Point(23,32);
			this.checkReportsShowPatNum.Name = "checkReportsShowPatNum";
			this.checkReportsShowPatNum.Size = new System.Drawing.Size(414,17);
			this.checkReportsShowPatNum.TabIndex = 200;
			this.checkReportsShowPatNum.Text = "Show PatNum: Aging, OutstandingIns, ProcsNotBilled";
			this.checkReportsShowPatNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkReportProdWO
			// 
			this.checkReportProdWO.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReportProdWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReportProdWO.Location = new System.Drawing.Point(23,51);
			this.checkReportProdWO.Name = "checkReportProdWO";
			this.checkReportProdWO.Size = new System.Drawing.Size(414,17);
			this.checkReportProdWO.TabIndex = 201;
			this.checkReportProdWO.Text = "Monthly P&&I scheduled production subtracts PPO writeoffs";
			this.checkReportProdWO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkReportsShowPatNum);
			this.groupBox1.Controls.Add(this.checkReportProdWO);
			this.groupBox1.Controls.Add(this.checkReportsProcDate);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(12,229);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(453,73);
			this.groupBox1.TabIndex = 202;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Reports";
			// 
			// FormMisc
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(670,502);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textWebServiceServerName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butLanguages);
			this.Controls.Add(this.textSigInterval);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMisc";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Miscellaneous Setup";
			this.Load += new System.EventHandler(this.FormMisc_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBoxTaskDefaults.ResumeLayout(false);
			this.groupBoxTaskDefaults.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormMisc_Load(object sender, System.EventArgs e) {
			if(PrefC.GetLong(PrefName.ProcessSigsIntervalInSecs)==0){
				textSigInterval.Text="";
			}
			else{
				textSigInterval.Text=PrefC.GetLong(PrefName.ProcessSigsIntervalInSecs).ToString();
			}
			textMainWindowTitle.Text=PrefC.GetString(PrefName.MainWindowTitle);
			comboShowID.Items.Add(Lan.g(this,"None"));
			comboShowID.Items.Add(Lan.g(this,"PatNum"));
			comboShowID.Items.Add(Lan.g(this,"ChartNumber"));
			comboShowID.SelectedIndex=PrefC.GetInt(PrefName.ShowIDinTitleBar);
			checkTasksCheckOnStartup.Checked=PrefC.GetBool(PrefName.TasksCheckOnStartup);
			checkTaskListAlwaysShow.Checked=PrefC.GetBool(PrefName.TaskListAlwaysShowsAtBottom);
			if(checkTaskListAlwaysShow.Checked) {
				groupBoxTaskDefaults.Enabled=true;
			}
			else {
				groupBoxTaskDefaults.Enabled=false;
			}
			checkTasksNewTrackedByUser.Checked=PrefC.GetBool(PrefName.TasksNewTrackedByUser);
			//computerPref=ComputerPrefs.GetForLocalComputer();
			checkBoxTaskKeepListHidden.Checked=ComputerPrefs.LocalComputer.TaskKeepListHidden;
			if(ComputerPrefs.LocalComputer.TaskDock==0) {
				radioBottom.Checked=true;
			}
			else {
				radioRight.Checked=true;
			}
			validNumX.Text=ComputerPrefs.LocalComputer.TaskX.ToString();
			validNumY.Text=ComputerPrefs.LocalComputer.TaskY.ToString();
			checkTitleBarShowSite.Checked=PrefC.GetBool(PrefName.TitleBarShowSite);
			textWebServiceServerName.Text=PrefC.GetString(PrefName.WebServiceServerName);
			checkReportsProcDate.Checked=PrefC.GetBool(PrefName.ReportsPPOwriteoffDefaultToProcDate);
			checkReportsShowPatNum.Checked=PrefC.GetBool(PrefName.ReportsShowPatNum);
			checkReportProdWO.Checked=PrefC.GetBool(PrefName.ReportPandIschedProdSubtractsWO);
		}

		private void butLanguages_Click(object sender,EventArgs e) {
			FormLanguagesUsed FormL=new FormLanguagesUsed();
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.OK){
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		private void checkTaskListAlwaysShow_CheckedChanged(object sender,EventArgs e) {
			if(checkTaskListAlwaysShow.Checked) {
				groupBoxTaskDefaults.Enabled=true;
			}
			else {
				groupBoxTaskDefaults.Enabled=false;
			}
		}

		private void checkBoxTaskKeepListHidden_CheckedChanged(object sender,EventArgs e) {
			if(checkBoxTaskKeepListHidden.Checked) {
				radioBottom.Enabled=false;
				radioRight.Enabled=false;
				labelX.Enabled=false;
				labelY.Enabled=false;
				validNumX.Enabled=false;
				validNumY.Enabled=false;
			}
			else {
				radioBottom.Enabled=true;
				radioRight.Enabled=true;
				labelX.Enabled=true;
				labelY.Enabled=true;
				validNumX.Enabled=true;
				validNumY.Enabled=true;

			}
		}
		private void butOK_Click(object sender, System.EventArgs e) {
			if( validNumX.errorProvider1.GetError(validNumX)!=""
				| validNumY.errorProvider1.GetError(validNumY)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			bool changed=false;
			if( Prefs.UpdateString(PrefName.MainWindowTitle,textMainWindowTitle.Text)
				| Prefs.UpdateLong(PrefName.ShowIDinTitleBar,comboShowID.SelectedIndex)
				| Prefs.UpdateBool(PrefName.TaskListAlwaysShowsAtBottom, checkTaskListAlwaysShow.Checked)
				| Prefs.UpdateBool(PrefName.TasksCheckOnStartup, checkTasksCheckOnStartup.Checked)
				| Prefs.UpdateBool(PrefName.TasksNewTrackedByUser, checkTasksNewTrackedByUser.Checked)
				| Prefs.UpdateBool(PrefName.TitleBarShowSite, checkTitleBarShowSite.Checked)
				| Prefs.UpdateString(PrefName.WebServiceServerName,textWebServiceServerName.Text)
				| Prefs.UpdateBool(PrefName.ReportsPPOwriteoffDefaultToProcDate,checkReportsProcDate.Checked)
				| Prefs.UpdateBool(PrefName.ReportsShowPatNum,checkReportsShowPatNum.Checked)
				| Prefs.UpdateBool(PrefName.ReportPandIschedProdSubtractsWO,checkReportProdWO.Checked)
				)
			{
				changed=true;
			}
			//task list------------------------------------------------------------------------------------------
			if(ComputerPrefs.LocalComputer.TaskKeepListHidden!=checkBoxTaskKeepListHidden.Checked) {
				ComputerPrefs.LocalComputer.TaskKeepListHidden=checkBoxTaskKeepListHidden.Checked;
				changed=true;//needed to trigger screen refresh
			}
			if(radioBottom.Checked && ComputerPrefs.LocalComputer.TaskDock!=0) {
				ComputerPrefs.LocalComputer.TaskDock=0;
				changed=true;
			}
			else if(!radioBottom.Checked && ComputerPrefs.LocalComputer.TaskDock!=1) {
				ComputerPrefs.LocalComputer.TaskDock=1;
				changed=true;
			}
			if(ComputerPrefs.LocalComputer.TaskX!=PIn.Int(validNumX.Text)) {
				ComputerPrefs.LocalComputer.TaskX=PIn.Int(validNumX.Text);
				changed=true;
			}
			if(ComputerPrefs.LocalComputer.TaskY!=PIn.Int(validNumY.Text)) {
				ComputerPrefs.LocalComputer.TaskY=PIn.Int(validNumY.Text);
				changed=true;
			}
			//end of tasklist section-----------------------------------------------------------------------------
			if(textSigInterval.Text==""){
				if(Prefs.UpdateLong(PrefName.ProcessSigsIntervalInSecs,0)){
					changed=true;
				}
			}
			else{
				if(Prefs.UpdateLong(PrefName.ProcessSigsIntervalInSecs,PIn.Long(textSigInterval.Text))){
					changed=true;
				}
			}
			if(changed){
				DataValid.SetInvalid(InvalidType.Prefs, InvalidType.Computers);
				ComputerPrefs.Update(ComputerPrefs.LocalComputer);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}





