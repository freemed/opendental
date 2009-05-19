using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormUpdate : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label labelVersion;
		private OpenDental.UI.Button butDownload;
		private OpenDental.UI.Button butCheck;
		private System.Windows.Forms.TextBox textUpdateCode;
		private System.Windows.Forms.TextBox textWebsitePath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private IContainer components;
		private TextBox textResult;
		private TextBox textResult2;
		private Label label4;
		private Label label6;
		private Panel panel1;
		private Label label9;
		private Label label10;
		private Label label7;
		private Label label8;
		private MainMenu mainMenu1;
		private MenuItem menuItemSetup;
		private Panel panelClassic;
		private OpenDental.UI.Button butLicense;
		private TextBox textConnectionMessage;
		private GroupBox groupBuild;
		private Label label2;
		private TextBox textBuild;
		private OpenDental.UI.Button butInstallBuild;
		private GroupBox groupBeta;
		private TextBox textBeta;
		private OpenDental.UI.Button butInstallBeta;
		private Label label5;
		private GroupBox groupStable;
		private TextBox textStable;
		private OpenDental.UI.Button butInstallStable;
		private Label label11;
		private OpenDental.UI.Button butCheck2;//OD1
		//<summary>Includes path</summary>
		//string WriteToFile;
		private static string buildAvailable;
		private static string buildAvailableCode;
		private static string buildAvailableDisplay;
		private static string stableAvailable;
		private static string stableAvailableCode;
		private static string stableAvailableDisplay;
		private static string betaAvailable;
		private static string betaAvailableCode;
		private static string betaAvailableDisplay;

		///<summary></summary>
		public FormUpdate()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdate));
			this.labelVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textUpdateCode = new System.Windows.Forms.TextBox();
			this.textWebsitePath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textResult = new System.Windows.Forms.TextBox();
			this.textResult2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.panelClassic = new System.Windows.Forms.Panel();
			this.butCheck = new OpenDental.UI.Button();
			this.butDownload = new OpenDental.UI.Button();
			this.textConnectionMessage = new System.Windows.Forms.TextBox();
			this.groupBuild = new System.Windows.Forms.GroupBox();
			this.textBuild = new System.Windows.Forms.TextBox();
			this.butInstallBuild = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBeta = new System.Windows.Forms.GroupBox();
			this.textBeta = new System.Windows.Forms.TextBox();
			this.butInstallBeta = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.groupStable = new System.Windows.Forms.GroupBox();
			this.textStable = new System.Windows.Forms.TextBox();
			this.butInstallStable = new OpenDental.UI.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.butCheck2 = new OpenDental.UI.Button();
			this.butLicense = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.panelClassic.SuspendLayout();
			this.groupBuild.SuspendLayout();
			this.groupBeta.SuspendLayout();
			this.groupStable.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelVersion
			// 
			this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelVersion.Location = new System.Drawing.Point(74,9);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(176,20);
			this.labelVersion.TabIndex = 10;
			this.labelVersion.Text = "Using Version ";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,23);
			this.label1.TabIndex = 0;
			// 
			// textUpdateCode
			// 
			this.textUpdateCode.Location = new System.Drawing.Point(129,100);
			this.textUpdateCode.Name = "textUpdateCode";
			this.textUpdateCode.Size = new System.Drawing.Size(113,20);
			this.textUpdateCode.TabIndex = 19;
			// 
			// textWebsitePath
			// 
			this.textWebsitePath.Location = new System.Drawing.Point(129,77);
			this.textWebsitePath.Name = "textWebsitePath";
			this.textWebsitePath.Size = new System.Drawing.Size(388,20);
			this.textWebsitePath.TabIndex = 24;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24,78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105,19);
			this.label3.TabIndex = 26;
			this.label3.Text = "Website Path";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textResult
			// 
			this.textResult.AcceptsReturn = true;
			this.textResult.BackColor = System.Drawing.SystemColors.Window;
			this.textResult.Location = new System.Drawing.Point(129,156);
			this.textResult.Name = "textResult";
			this.textResult.ReadOnly = true;
			this.textResult.Size = new System.Drawing.Size(388,20);
			this.textResult.TabIndex = 34;
			// 
			// textResult2
			// 
			this.textResult2.AcceptsReturn = true;
			this.textResult2.BackColor = System.Drawing.SystemColors.Window;
			this.textResult2.Location = new System.Drawing.Point(129,179);
			this.textResult2.Multiline = true;
			this.textResult2.Name = "textResult2";
			this.textResult2.ReadOnly = true;
			this.textResult2.Size = new System.Drawing.Size(388,66);
			this.textResult2.TabIndex = 35;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6,100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120,19);
			this.label4.TabIndex = 34;
			this.label4.Text = "Update Code";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(10,8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(555,58);
			this.label6.TabIndex = 40;
			this.label6.Text = resources.GetString("label6.Text");
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(5,529);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(630,4);
			this.panel1.TabIndex = 42;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12,579);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(433,23);
			this.label9.TabIndex = 47;
			this.label9.Text = "All CDT codes are Copyrighted by the ADA.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12,557);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(550,23);
			this.label10.TabIndex = 44;
			this.label10.Text = "This program Copyright 2003-2007, Jordan S. Sparks, D.M.D., Frederik Carlier, and" +
    " others.";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12,538);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(549,20);
			this.label7.TabIndex = 46;
			this.label7.Text = "This software is licensed under the GPL, www.opensource.org/licenses/gpl-license." +
    "php";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12,601);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(433,20);
			this.label8.TabIndex = 45;
			this.label8.Text = "MySQL - Copyright 1995-2007, www.mysql.com";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// panelClassic
			// 
			this.panelClassic.Controls.Add(this.textWebsitePath);
			this.panelClassic.Controls.Add(this.textUpdateCode);
			this.panelClassic.Controls.Add(this.butCheck);
			this.panelClassic.Controls.Add(this.label3);
			this.panelClassic.Controls.Add(this.textResult);
			this.panelClassic.Controls.Add(this.label4);
			this.panelClassic.Controls.Add(this.label6);
			this.panelClassic.Controls.Add(this.textResult2);
			this.panelClassic.Controls.Add(this.butDownload);
			this.panelClassic.Location = new System.Drawing.Point(491,18);
			this.panelClassic.Name = "panelClassic";
			this.panelClassic.Size = new System.Drawing.Size(568,494);
			this.panelClassic.TabIndex = 48;
			this.panelClassic.Visible = false;
			// 
			// butCheck
			// 
			this.butCheck.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCheck.Autosize = true;
			this.butCheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheck.CornerRadius = 4F;
			this.butCheck.Location = new System.Drawing.Point(129,125);
			this.butCheck.Name = "butCheck";
			this.butCheck.Size = new System.Drawing.Size(117,25);
			this.butCheck.TabIndex = 21;
			this.butCheck.Text = "Check for Updates";
			this.butCheck.Click += new System.EventHandler(this.butCheck_Click);
			// 
			// butDownload
			// 
			this.butDownload.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDownload.Autosize = true;
			this.butDownload.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDownload.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDownload.CornerRadius = 4F;
			this.butDownload.Location = new System.Drawing.Point(129,251);
			this.butDownload.Name = "butDownload";
			this.butDownload.Size = new System.Drawing.Size(83,25);
			this.butDownload.TabIndex = 20;
			this.butDownload.Text = "Download";
			this.butDownload.Click += new System.EventHandler(this.butDownload_Click);
			// 
			// textConnectionMessage
			// 
			this.textConnectionMessage.AcceptsReturn = true;
			this.textConnectionMessage.BackColor = System.Drawing.SystemColors.Window;
			this.textConnectionMessage.Location = new System.Drawing.Point(77,62);
			this.textConnectionMessage.Multiline = true;
			this.textConnectionMessage.Name = "textConnectionMessage";
			this.textConnectionMessage.ReadOnly = true;
			this.textConnectionMessage.Size = new System.Drawing.Size(388,66);
			this.textConnectionMessage.TabIndex = 50;
			// 
			// groupBuild
			// 
			this.groupBuild.Controls.Add(this.textBuild);
			this.groupBuild.Controls.Add(this.butInstallBuild);
			this.groupBuild.Controls.Add(this.label2);
			this.groupBuild.Location = new System.Drawing.Point(77,141);
			this.groupBuild.Name = "groupBuild";
			this.groupBuild.Size = new System.Drawing.Size(388,111);
			this.groupBuild.TabIndex = 51;
			this.groupBuild.TabStop = false;
			this.groupBuild.Text = "A new build is available for the current version";
			this.groupBuild.Visible = false;
			// 
			// textBuild
			// 
			this.textBuild.AcceptsReturn = true;
			this.textBuild.BackColor = System.Drawing.SystemColors.Window;
			this.textBuild.Location = new System.Drawing.Point(6,54);
			this.textBuild.Name = "textBuild";
			this.textBuild.ReadOnly = true;
			this.textBuild.Size = new System.Drawing.Size(376,20);
			this.textBuild.TabIndex = 51;
			// 
			// butInstallBuild
			// 
			this.butInstallBuild.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInstallBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butInstallBuild.Autosize = true;
			this.butInstallBuild.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInstallBuild.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInstallBuild.CornerRadius = 4F;
			this.butInstallBuild.Location = new System.Drawing.Point(309,80);
			this.butInstallBuild.Name = "butInstallBuild";
			this.butInstallBuild.Size = new System.Drawing.Size(73,25);
			this.butInstallBuild.TabIndex = 28;
			this.butInstallBuild.Text = "Install";
			this.butInstallBuild.Click += new System.EventHandler(this.butInstallBuild_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(374,29);
			this.label2.TabIndex = 27;
			this.label2.Text = "These are typically bug fixes.  It is strongly recommended to install any availab" +
    "le fixes.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBeta
			// 
			this.groupBeta.Controls.Add(this.textBeta);
			this.groupBeta.Controls.Add(this.butInstallBeta);
			this.groupBeta.Controls.Add(this.label5);
			this.groupBeta.Location = new System.Drawing.Point(77,393);
			this.groupBeta.Name = "groupBeta";
			this.groupBeta.Size = new System.Drawing.Size(388,119);
			this.groupBeta.TabIndex = 52;
			this.groupBeta.TabStop = false;
			this.groupBeta.Text = "A new beta version is available";
			this.groupBeta.Visible = false;
			// 
			// textBeta
			// 
			this.textBeta.AcceptsReturn = true;
			this.textBeta.BackColor = System.Drawing.SystemColors.Window;
			this.textBeta.Location = new System.Drawing.Point(6,62);
			this.textBeta.Name = "textBeta";
			this.textBeta.ReadOnly = true;
			this.textBeta.Size = new System.Drawing.Size(376,20);
			this.textBeta.TabIndex = 51;
			// 
			// butInstallBeta
			// 
			this.butInstallBeta.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInstallBeta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butInstallBeta.Autosize = true;
			this.butInstallBeta.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInstallBeta.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInstallBeta.CornerRadius = 4F;
			this.butInstallBeta.Location = new System.Drawing.Point(309,88);
			this.butInstallBeta.Name = "butInstallBeta";
			this.butInstallBeta.Size = new System.Drawing.Size(73,25);
			this.butInstallBeta.TabIndex = 28;
			this.butInstallBeta.Text = "Install";
			this.butInstallBeta.Click += new System.EventHandler(this.butInstallBeta_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(374,46);
			this.label5.TabIndex = 27;
			this.label5.Text = "This beta version will be very functional, but will have some bugs.  Use a beta v" +
    "ersion only if you demand the latest features.  Be sure to update regularly.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupStable
			// 
			this.groupStable.Controls.Add(this.textStable);
			this.groupStable.Controls.Add(this.butInstallStable);
			this.groupStable.Controls.Add(this.label11);
			this.groupStable.Location = new System.Drawing.Point(77,267);
			this.groupStable.Name = "groupStable";
			this.groupStable.Size = new System.Drawing.Size(388,111);
			this.groupStable.TabIndex = 53;
			this.groupStable.TabStop = false;
			this.groupStable.Text = "A new stable version is available";
			this.groupStable.Visible = false;
			// 
			// textStable
			// 
			this.textStable.AcceptsReturn = true;
			this.textStable.BackColor = System.Drawing.SystemColors.Window;
			this.textStable.Location = new System.Drawing.Point(6,54);
			this.textStable.Name = "textStable";
			this.textStable.ReadOnly = true;
			this.textStable.Size = new System.Drawing.Size(376,20);
			this.textStable.TabIndex = 51;
			// 
			// butInstallStable
			// 
			this.butInstallStable.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInstallStable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butInstallStable.Autosize = true;
			this.butInstallStable.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInstallStable.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInstallStable.CornerRadius = 4F;
			this.butInstallStable.Location = new System.Drawing.Point(309,80);
			this.butInstallStable.Name = "butInstallStable";
			this.butInstallStable.Size = new System.Drawing.Size(73,25);
			this.butInstallStable.TabIndex = 28;
			this.butInstallStable.Text = "Install";
			this.butInstallStable.Click += new System.EventHandler(this.butInstallStable_Click);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6,22);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(374,29);
			this.label11.TabIndex = 27;
			this.label11.Text = "Will have nearly zero bugs.  Will provide many useful enhanced features.";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butCheck2
			// 
			this.butCheck2.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCheck2.Autosize = true;
			this.butCheck2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheck2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheck2.CornerRadius = 4F;
			this.butCheck2.Location = new System.Drawing.Point(77,31);
			this.butCheck2.Name = "butCheck2";
			this.butCheck2.Size = new System.Drawing.Size(117,25);
			this.butCheck2.TabIndex = 54;
			this.butCheck2.Text = "Check for Updates";
			this.butCheck2.Visible = false;
			this.butCheck2.Click += new System.EventHandler(this.butCheck2_Click);
			// 
			// butLicense
			// 
			this.butLicense.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butLicense.Autosize = true;
			this.butLicense.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLicense.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLicense.CornerRadius = 4F;
			this.butLicense.Location = new System.Drawing.Point(466,547);
			this.butLicense.Name = "butLicense";
			this.butLicense.Size = new System.Drawing.Size(88,25);
			this.butLicense.TabIndex = 49;
			this.butLicense.Text = "View Licenses";
			this.butLicense.Click += new System.EventHandler(this.butLicense_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(560,547);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,25);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormUpdate
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(647,587);
			this.Controls.Add(this.panelClassic);
			this.Controls.Add(this.butCheck2);
			this.Controls.Add(this.groupStable);
			this.Controls.Add(this.groupBeta);
			this.Controls.Add(this.groupBuild);
			this.Controls.Add(this.textConnectionMessage);
			this.Controls.Add(this.butLicense);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.labelVersion);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "FormUpdate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update";
			this.Load += new System.EventHandler(this.FormUpdate_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUpdate_FormClosing);
			this.panelClassic.ResumeLayout(false);
			this.panelClassic.PerformLayout();
			this.groupBuild.ResumeLayout(false);
			this.groupBuild.PerformLayout();
			this.groupBeta.ResumeLayout(false);
			this.groupBeta.PerformLayout();
			this.groupStable.ResumeLayout(false);
			this.groupStable.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormUpdate_Load(object sender, System.EventArgs e) {
			labelVersion.Text=Lan.g(this,"Using Version:")+" "+Application.ProductVersion;
			//keeps the trailing year up to date
			this.label10.Text=Lan.g(this, "This program Copyright 2003-")+DateTime.Now.ToString("yyyy")+Lan.g(this,", Jordan S. Sparks, D.M.D., Frederik Carlier, and others.");
			this.label8.Text=Lan.g(this, "MySQL - Copyright 1995-")+DateTime.Now.ToString("yyyy")+Lan.g(this,", www.mysql.com");
			if(PrefC.GetBool("UpdateWindowShowsClassicView")){
				panelClassic.Visible=true;
				panelClassic.Location=new Point(67,29);
				textUpdateCode.Text=PrefC.GetString("UpdateCode");
				textWebsitePath.Text=PrefC.GetString("UpdateWebsitePath");//should include trailing /
				butDownload.Enabled=false;
				if(!Security.IsAuthorized(Permissions.Setup)){//gives a message box if no permission
					butCheck.Enabled=false;
				}
			}
			else{
				if(Security.IsAuthorized(Permissions.Setup,true)) {
					butCheck2.Visible=true;
				}
				else {
					textConnectionMessage.Text=Lan.g(this,"Not authorized for")+" "+GroupPermissions.GetDesc(Permissions.Setup);
				}
			}
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			if(PrefC.GetBool("UpdateWindowShowsClassicView")){
				return;
			}
			FormUpdateSetup FormU=new FormUpdateSetup();
			FormU.ShowDialog();
		}

		private void butCheck2_Click(object sender,EventArgs e) {
			if(PrefC.GetString("WebServiceServerName") != "" //using web service
				&& PrefC.GetString("WebServiceServerName") != Environment.MachineName)//and not on web server 
			{
				MessageBox.Show(Lan.g(this,"Updates are only allowed from the web server: ")+PrefC.GetString("WebServiceServerName"));
				return;
			}
			Cursor=Cursors.WaitCursor;
			groupBuild.Visible=false;
			groupStable.Visible=false;
			groupBeta.Visible=false;
			textConnectionMessage.Text=Lan.g(this,"Attempting to connect to web service......");
			Application.DoEvents();
			string result="";
			try {
				result=SendAndReceiveXml();
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return;
			}
			textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			try {
				ParseXml(result);//fills the six static variables with values.
			}
			catch(Exception ex) {
				textConnectionMessage.Text=ex.Message;
				MessageBox.Show(ex.Message,"Error");
				return;
			}
			if(buildAvailableDisplay!="") {
				groupBuild.Visible=true;
				textBuild.Text=buildAvailableDisplay;
			}
			if(stableAvailableDisplay!="") {
				groupStable.Visible=true;
				textStable.Text=stableAvailableDisplay;
			}
			if(betaAvailableDisplay!="") {
				groupBeta.Visible=true;
				textBeta.Text=betaAvailableDisplay;
			}
			if(betaAvailable=="" && stableAvailable=="" && buildAvailable=="") {
				textConnectionMessage.Text+=Lan.g(this,"  There are no downloads available.");
			}
			else {
				textConnectionMessage.Text+=Lan.g(this,"  The following downloads are available.  Be sure to stop the program on all other computers in the office before installing.");
			}
		}

		///<summary>Parses the xml result from the server and uses it to fill the 9 static variables.  Or can throw an exception if some sort of error.</summary>
		private static void ParseXml(string result){
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				//textConnectionMessage.Text=node.InnerText;
				//MessageBox.Show(node.InnerText,"Error");
				//return;
				throw new Exception(node.InnerText);
			}
			node=doc.SelectSingleNode("//KeyDisabled");
			if(node==null) {
				//no error, and no disabled message
				if(Prefs.UpdateBool("RegistrationKeyIsDisabled",false)) {//this is one of two places in the program where this happens.
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
			else {
				//textConnectionMessage.Text=node.InnerText;
				//MessageBox.Show(node.InnerText);
				if(Prefs.UpdateBool("RegistrationKeyIsDisabled",true)) {//this is one of two places in the program where this happens.
					DataValid.SetInvalid(InvalidType.Prefs);
				}
				throw new Exception(node.InnerText);
				//return;
			}
			node=doc.SelectSingleNode("//BuildAvailable");
			buildAvailable="";
			buildAvailableCode="";
			buildAvailableDisplay="";
			if(node!=null) {
				node=doc.SelectSingleNode("//BuildAvailable/Display");
				if(node!=null) {
					buildAvailableDisplay=node.InnerText;
				}
				node=doc.SelectSingleNode("//BuildAvailable/MajMinBuildF");
				if(node!=null) {
					buildAvailable=node.InnerText;
				}
				node=doc.SelectSingleNode("//BuildAvailable/UpdateCode");
				if(node!=null) {
					buildAvailableCode=node.InnerText;
				}
			}
			node=doc.SelectSingleNode("//StableAvailable");
			stableAvailable="";
			stableAvailableCode="";
			stableAvailableDisplay="";
			if(node!=null) {
				node=doc.SelectSingleNode("//StableAvailable/Display");
				if(node!=null) {
					stableAvailableDisplay=node.InnerText;
				}
				node=doc.SelectSingleNode("//StableAvailable/MajMinBuildF");
				if(node!=null) {
					stableAvailable=node.InnerText;
				}
				node=doc.SelectSingleNode("//StableAvailable/UpdateCode");
				if(node!=null) {
					stableAvailableCode=node.InnerText;
				}
			}
			node=doc.SelectSingleNode("//BetaAvailable");
			betaAvailable="";
			betaAvailableCode="";
			betaAvailableDisplay="";
			if(node!=null) {
				node=doc.SelectSingleNode("//BetaAvailable/Display");
				if(node!=null) {
					betaAvailableDisplay=node.InnerText;
				}
				node=doc.SelectSingleNode("//BetaAvailable/MajMinBuildF");
				if(node!=null) {
					betaAvailable=node.InnerText;
				}
				node=doc.SelectSingleNode("//BetaAvailable/UpdateCode");
				if(node!=null) {
					betaAvailableCode=node.InnerText;
				}
			}
		}

		private static string SendAndReceiveXml(){
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("UpdateRequest");
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				writer.WriteStartElement("PracticeTitle");
				writer.WriteString(PrefC.GetString("PracticeTitle"));
				writer.WriteEndElement();
				writer.WriteStartElement("PracticePhone");
				writer.WriteString(PrefC.GetString("PracticePhone"));
				writer.WriteEndElement();
				writer.WriteStartElement("ProgramVersion");
				writer.WriteString(PrefC.GetString("ProgramVersion"));
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString("UpdateServerAddress");
			#endif
			string result="";
			//try {
				result=updateService.RequestUpdate(strbuild.ToString());//may throw error
			//}
			//catch(Exception ex) {
			//	Cursor=Cursors.Default;
			//	MessageBox.Show("Error: "+ex.Message);
			//	return;
			//}
			return result;
		}

		///<summary>Used if we already have the correct version of the program installed, but we need the UpdateCode in order to download the Setup.exe again.  Like when using multiple databases.</summary>
		public static string GetUpdateCodeForThisVersion() {
			string result=result=SendAndReceiveXml();//exception bubbles up.
			ParseXml(result);
			//see if any of the three versions exactly matches this current version.
			Version thisVersion=new Version(Application.ProductVersion);
			string thisVersStr=thisVersion.ToString(3);
			string testVers;
			testVers=buildAvailable.TrimEnd('f');
			if(testVers==thisVersStr) {
				return buildAvailableCode;
			}
			testVers=stableAvailable.TrimEnd('f');
			if(testVers==thisVersStr) {
				return stableAvailableCode;
			}
			testVers=betaAvailable.TrimEnd('f');
			if(testVers==thisVersStr) {
				return betaAvailableCode;
			}
			return "";
		}

		private void butInstallBuild_Click(object sender,EventArgs e) {
			string patchName="Setup.exe";
			string destDir=FormPath.GetPreferredImagePath();
			if(destDir==null) {//Not using A to Z folders?
				destDir=Path.GetTempPath();
			}
			DownloadInstallPatchFromURI(PrefC.GetString("UpdateWebsitePath")+buildAvailableCode+"/"+patchName,//Source URI
				ODFileUtils.CombinePaths(destDir,patchName),true,true);//Local destination file.
		}

		private void butInstallStable_Click(object sender,EventArgs e) {
			string patchName="Setup.exe";
			string destDir=FormPath.GetPreferredImagePath();
			if(destDir==null) {//Not using A to Z folders?
				destDir=Path.GetTempPath();
			}
			DownloadInstallPatchFromURI(PrefC.GetString("UpdateWebsitePath")+stableAvailableCode+"/"+patchName,//Source URI
				ODFileUtils.CombinePaths(destDir,patchName),true,true);//Local destination file.
		}

		private void butInstallBeta_Click(object sender,EventArgs e) {
			string patchName="Setup.exe";
			string destDir=FormPath.GetPreferredImagePath();
			if(destDir==null) {//Not using A to Z folders?
				destDir=Path.GetTempPath();
			}
			DownloadInstallPatchFromURI(PrefC.GetString("UpdateWebsitePath")+betaAvailableCode+"/"+patchName,//Source URI
				ODFileUtils.CombinePaths(destDir,patchName),true,true);//Local destination file.
		}

		private void butCheck_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			SavePrefs();
			CheckMain();
			//CheckClaimForm();
			Cursor=Cursors.Default;
		}

		private void CheckMain() {
			butDownload.Enabled=false;
			textResult.Text="";
			textResult2.Text="";
			if(textUpdateCode.Text.Length==0) {
				textResult.Text+=Lan.g(this,"Registration number not valid.");
				return;
			}
			string updateInfoMajor="";
			string updateInfoMinor="";
			butDownload.Enabled=ShouldDownloadUpdate(textWebsitePath.Text,textUpdateCode.Text,
							out updateInfoMajor,out updateInfoMinor);
			textResult.Text=updateInfoMajor;
			textResult2.Text=updateInfoMinor;
		}

		///<summary>Returns true if the download at the specified remoteUri with the given registration code should be downloaded and installed as an update, and false is returned otherwise. Also, information about the decision making process is stored in the updateInfoMajor and updateInfoMinor strings, but only holds significance to a human user.</summary>
		public static bool ShouldDownloadUpdate(string remoteUri,string updateCode,out string updateInfoMajor,out string updateInfoMinor){
			updateInfoMajor="";
			updateInfoMinor="";
			bool shouldDownload=false;
			string fileName="Manifest.txt";
			WebClient myWebClient=new WebClient();
			string myStringWebResource=remoteUri+updateCode+"/"+fileName;
			Version versionNewBuild=null;
			string strNewVersion="";
			string newBuild="";
			bool buildIsBeta=false;
			bool versionIsBeta=false;
			try{
				using(StreamReader sr=new StreamReader(myWebClient.OpenRead(myStringWebResource))){
					newBuild=sr.ReadLine();//must be be 3 or 4 components (revision is optional)
					strNewVersion=sr.ReadLine();//returns null if no second line
				}
				if(newBuild.EndsWith("b")){
					buildIsBeta=true;
					newBuild=newBuild.Replace("b","");
				}
				versionNewBuild=new Version(newBuild);
				if(versionNewBuild.Revision==-1){
					versionNewBuild=new Version(versionNewBuild.Major,versionNewBuild.Minor,versionNewBuild.Build,0);
				}
				if(strNewVersion!=null && strNewVersion.EndsWith("b")){
					versionIsBeta=true;
					strNewVersion=strNewVersion.Replace("b","");
				}
			}catch{
				updateInfoMajor+=Lan.g("FormUpdate","Registration number not valid, or internet connection failed.  ");
				return false;
			}
			if(versionNewBuild==new Version(Application.ProductVersion)){
				updateInfoMajor+=Lan.g("FormUpdate","You are using the most current build of this version.  ");
			}else{
				//this also allows users to install previous versions.
				updateInfoMajor+=Lan.g("FormUpdate","A new build of this version is available for download:  ")
					+versionNewBuild.ToString();
				if(buildIsBeta){
					updateInfoMajor+=Lan.g("FormUpdate","(beta)  ");
				}
				shouldDownload=true;
			}
			//Whether or not build is current, we want to inform user about the next minor version
			if(strNewVersion!=null){//we don't really care what it is.
				updateInfoMinor+=Lan.g("FormUpdate","A newer version is also available.  ");
				if(versionIsBeta) {//(checkNewBuild.Checked || checkNewVersion.Checked) && versionIsBeta){
					updateInfoMinor+=Lan.g("FormUpdate","It is beta (test), so it has some bugs and "+
						"you will need to update it frequently.  ");
				}
				updateInfoMinor+=Lan.g("FormUpdate","Contact us for a new Registration number if you wish to use it.  ");
			}
			return shouldDownload;
		}

		private void butDownload_Click(object sender, System.EventArgs e){
			string patchName="Setup.exe";
			string destDir=FormPath.GetPreferredImagePath();
			if(destDir==null){//Not using A to Z folders?
				destDir=Path.GetTempPath();
			}
			DownloadInstallPatchFromURI(textWebsitePath.Text+textUpdateCode.Text+"/"+patchName,//Source URI
				ODFileUtils.CombinePaths(destDir,patchName),true,false);//Local destination file.
		}

		public static void DownloadInstallPatchFromURI(string downloadUri,string destinationPath,bool runSetupAfterDownload,bool showShutdownWindow){
			if(showShutdownWindow) {
				FormShutdown FormSD=new FormShutdown();
				FormSD.ShowDialog();
				if(FormSD.DialogResult==DialogResult.OK) {
					//turn off signal reception for 5 seconds so this workstation will not shut down.
					FormOpenDental.signalLastRefreshed=MiscData.GetNowDateTime().AddSeconds(5);
					Signal sig=new Signal();
					sig.ITypes=((int)InvalidType.ShutDownNow).ToString();
					sig.SigType=SignalType.Invalid;
					Signals.Insert(sig);
					Computers.ClearAllHeartBeats(Environment.MachineName);//always assume success
					SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Shutdown all workstations.");
				}
				//continue on even if user clicked cancel
			}
			File.Delete(destinationPath);
			WebRequest wr=WebRequest.Create(downloadUri);
			WebResponse webResp=wr.GetResponse();
			int fileSize=(int)webResp.ContentLength/1024;
			FormProgress FormP=new FormProgress();
			//start the thread that will perform the download
			System.Threading.ThreadStart downloadDelegate=
					delegate { DownloadInstallPatchWorker(downloadUri,destinationPath,ref FormP); };
			Thread workerThread=new System.Threading.Thread(downloadDelegate);
			workerThread.Start();
			//display the progress dialog to the user:
			FormP.MaxVal=(double)fileSize/1024;
			FormP.NumberMultiplication=100;
			FormP.DisplayText="?currentVal MB of ?maxVal MB copied";
			FormP.NumberFormat="F";
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel) {
				workerThread.Abort();
				return;
			}
			if(!runSetupAfterDownload) {
				return;
			}
			if(!MsgBox.Show(FormP,MsgBoxButtons.OKCancel,"Download succeeded.  Setup program will now begin.  When done, restart the program on this computer, then on the other computers.")) 
			{
				//clicking cancel gives the user one last chance to avoid starting the update process.
				return;
			}
			//no other workstation will be able to start up until this value is reset.
			Prefs.UpdateString("UpdateInProgressOnComputerName",Environment.MachineName);
			try{
				Process.Start(destinationPath);
				Application.Exit();
			}
			catch{
				MsgBox.Show(FormP,"Could not launch setup");
			}
		}

		///<summary>This is the function that the worker thread uses to actually perform the download.  Can also call this method in the ordinary way if the file to be transferred is short.</summary>
		private static void DownloadInstallPatchWorker(string downloadUri,string destinationPath,ref FormProgress progressIndicator){
			int chunk=10;//KB
			byte[] buffer;
			int i=0;
			WebClient myWebClient=new WebClient();
			Stream readStream=myWebClient.OpenRead(downloadUri);
			BinaryReader br=new BinaryReader(readStream);
			FileStream writeStream=new FileStream(destinationPath,FileMode.Create);
			BinaryWriter bw=new BinaryWriter(writeStream);
			try{
				while(true){
					buffer=br.ReadBytes(chunk*1024);
					if(buffer.Length==0){
						break;
					}
					double curVal=((double)(chunk*i)+((double)buffer.Length/1024))/1024;
					progressIndicator.CurrentVal=curVal;
					bw.Write(buffer);
					i++;
				}
			}
			catch{//for instance, if abort.
				br.Close();
				bw.Close();
				File.Delete(destinationPath);
			}
			finally{
				br.Close();
				bw.Close();
			}
			//myWebClient.DownloadFile(downloadUri,ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"Setup.exe"));
		}

		private void SavePrefs(){
			bool changed=false;
			if(Prefs.UpdateString("UpdateCode",textUpdateCode.Text)){
				changed=true;
			}
			if(Prefs.UpdateString("UpdateWebsitePath",textWebsitePath.Text)){
				changed=true;
			}
			if(changed){
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

		private void butLicense_Click(object sender,EventArgs e) {
			FormLicense FormL=new FormLicense();
			FormL.ShowDialog();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormUpdate_FormClosing(object sender,FormClosingEventArgs e) {
			if(Security.IsAuthorized(Permissions.Setup,DateTime.Now,true)
				&& PrefC.GetBool("UpdateWindowShowsClassicView"))			
			{
				SavePrefs();
			}
		}

		

		

		

		

		

	

	

		

	

	}

	
}





















