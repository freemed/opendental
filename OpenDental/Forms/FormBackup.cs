using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormBackup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butRestore;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox4;
		private OpenDental.UI.Button butBackup;
		private OpenDental.UI.Button butBrowseTo;
		private OpenDental.UI.Button butBrowseFrom;
		private OpenDental.UI.Button butBrowseRestoreFrom;
		private System.Windows.Forms.TextBox textBox3;
		private OpenDental.UI.Button butBrowseRestoreTo;
		private System.Windows.Forms.TextBox textBackupToPath;
		private System.Windows.Forms.TextBox textBackupFromPath;
		private System.Windows.Forms.TextBox textBackupRestoreFromPath;
		private System.Windows.Forms.TextBox textBackupRestoreToPath;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBackupRestoreAtoZToPath;
		private OpenDental.UI.Button butBrowseRestoreAtoZTo;
		private OpenDental.UI.Button butSave;
		//Required designer variable.
		private System.ComponentModel.Container components = null;
		private FormProgress FormP;
		///<summary>Only used by one worker thread at a time. The value of the progressbar (in MB). Still passed in by delegate.</summary>
		private double curVal;

		///<summary></summary>
		public FormBackup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackup));
			this.butCancel = new OpenDental.UI.Button();
			this.butBackup = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBackupToPath = new System.Windows.Forms.TextBox();
			this.butBrowseTo = new OpenDental.UI.Button();
			this.butRestore = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.butBrowseRestoreAtoZTo = new OpenDental.UI.Button();
			this.textBackupRestoreAtoZToPath = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.butBrowseRestoreTo = new OpenDental.UI.Button();
			this.textBackupRestoreToPath = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.butBrowseRestoreFrom = new OpenDental.UI.Button();
			this.textBackupRestoreFromPath = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.butBrowseFrom = new OpenDental.UI.Button();
			this.textBackupFromPath = new System.Windows.Forms.TextBox();
			this.butSave = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(660,477);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butBackup
			// 
			this.butBackup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBackup.Autosize = true;
			this.butBackup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBackup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBackup.CornerRadius = 4F;
			this.butBackup.Location = new System.Drawing.Point(665,190);
			this.butBackup.Name = "butBackup";
			this.butBackup.Size = new System.Drawing.Size(86,26);
			this.butBackup.TabIndex = 1;
			this.butBackup.Text = "Backup";
			this.butBackup.Click += new System.EventHandler(this.butBackup_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18,5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(713,28);
			this.label1.TabIndex = 2;
			this.label1.Text = "BACKUPS ARE USELESS UNLESS YOU REGULARLY VERIFY THEIR QUALITY BY TAKING A BACKUP " +
    "HOME AND RESTORING IT TO YOUR HOME COMPUTER.  We suggest an inexpensive USB flas" +
    "h drive for this purpose.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBackupToPath
			// 
			this.textBackupToPath.Location = new System.Drawing.Point(18,193);
			this.textBackupToPath.Name = "textBackupToPath";
			this.textBackupToPath.Size = new System.Drawing.Size(481,20);
			this.textBackupToPath.TabIndex = 4;
			// 
			// butBrowseTo
			// 
			this.butBrowseTo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseTo.Autosize = true;
			this.butBrowseTo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseTo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseTo.CornerRadius = 4F;
			this.butBrowseTo.Location = new System.Drawing.Point(512,190);
			this.butBrowseTo.Name = "butBrowseTo";
			this.butBrowseTo.Size = new System.Drawing.Size(86,26);
			this.butBrowseTo.TabIndex = 5;
			this.butBrowseTo.Text = "Browse";
			this.butBrowseTo.Click += new System.EventHandler(this.butBrowseTo_Click);
			// 
			// butRestore
			// 
			this.butRestore.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butRestore.Autosize = true;
			this.butRestore.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRestore.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRestore.CornerRadius = 4F;
			this.butRestore.Location = new System.Drawing.Point(648,170);
			this.butRestore.Name = "butRestore";
			this.butRestore.Size = new System.Drawing.Size(86,26);
			this.butRestore.TabIndex = 6;
			this.butRestore.Text = "Restore";
			this.butRestore.Click += new System.EventHandler(this.butRestore_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox5);
			this.groupBox1.Controls.Add(this.butBrowseRestoreAtoZTo);
			this.groupBox1.Controls.Add(this.textBackupRestoreAtoZToPath);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.butBrowseRestoreTo);
			this.groupBox1.Controls.Add(this.textBackupRestoreToPath);
			this.groupBox1.Controls.Add(this.textBox4);
			this.groupBox1.Controls.Add(this.butBrowseRestoreFrom);
			this.groupBox1.Controls.Add(this.textBackupRestoreFromPath);
			this.groupBox1.Controls.Add(this.butRestore);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(12,236);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(747,213);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Restore";
			// 
			// textBox5
			// 
			this.textBox5.BackColor = System.Drawing.SystemColors.Control;
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Location = new System.Drawing.Point(7,142);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(396,27);
			this.textBox5.TabIndex = 21;
			this.textBox5.Text = "Restore A-Z images to this folder: (example:)\r\nC:\\OpenDentalData\\";
			// 
			// butBrowseRestoreAtoZTo
			// 
			this.butBrowseRestoreAtoZTo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseRestoreAtoZTo.Autosize = true;
			this.butBrowseRestoreAtoZTo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseRestoreAtoZTo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseRestoreAtoZTo.CornerRadius = 4F;
			this.butBrowseRestoreAtoZTo.Location = new System.Drawing.Point(500,170);
			this.butBrowseRestoreAtoZTo.Name = "butBrowseRestoreAtoZTo";
			this.butBrowseRestoreAtoZTo.Size = new System.Drawing.Size(86,26);
			this.butBrowseRestoreAtoZTo.TabIndex = 20;
			this.butBrowseRestoreAtoZTo.Text = "Browse";
			this.butBrowseRestoreAtoZTo.Click += new System.EventHandler(this.butBrowseRestoreAtoZTo_Click);
			// 
			// textBackupRestoreAtoZToPath
			// 
			this.textBackupRestoreAtoZToPath.Location = new System.Drawing.Point(6,173);
			this.textBackupRestoreAtoZToPath.Name = "textBackupRestoreAtoZToPath";
			this.textBackupRestoreAtoZToPath.Size = new System.Drawing.Size(481,20);
			this.textBackupRestoreAtoZToPath.TabIndex = 19;
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Control;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(7,81);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(247,27);
			this.textBox3.TabIndex = 18;
			this.textBox3.Text = "Restore database TO this folder: (example:)\r\nC:\\mysql\\data\\";
			// 
			// butBrowseRestoreTo
			// 
			this.butBrowseRestoreTo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseRestoreTo.Autosize = true;
			this.butBrowseRestoreTo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseRestoreTo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseRestoreTo.CornerRadius = 4F;
			this.butBrowseRestoreTo.Location = new System.Drawing.Point(500,109);
			this.butBrowseRestoreTo.Name = "butBrowseRestoreTo";
			this.butBrowseRestoreTo.Size = new System.Drawing.Size(86,26);
			this.butBrowseRestoreTo.TabIndex = 17;
			this.butBrowseRestoreTo.Text = "Browse";
			this.butBrowseRestoreTo.Click += new System.EventHandler(this.butBrowseRestoreTo_Click);
			// 
			// textBackupRestoreToPath
			// 
			this.textBackupRestoreToPath.Location = new System.Drawing.Point(6,112);
			this.textBackupRestoreToPath.Name = "textBackupRestoreToPath";
			this.textBackupRestoreToPath.Size = new System.Drawing.Size(481,20);
			this.textBackupRestoreToPath.TabIndex = 16;
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.SystemColors.Control;
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Location = new System.Drawing.Point(7,20);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(280,29);
			this.textBox4.TabIndex = 15;
			this.textBox4.Text = "Restore FROM this folder: (example:)\r\nD:\\";
			// 
			// butBrowseRestoreFrom
			// 
			this.butBrowseRestoreFrom.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseRestoreFrom.Autosize = true;
			this.butBrowseRestoreFrom.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseRestoreFrom.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseRestoreFrom.CornerRadius = 4F;
			this.butBrowseRestoreFrom.Location = new System.Drawing.Point(500,47);
			this.butBrowseRestoreFrom.Name = "butBrowseRestoreFrom";
			this.butBrowseRestoreFrom.Size = new System.Drawing.Size(86,26);
			this.butBrowseRestoreFrom.TabIndex = 14;
			this.butBrowseRestoreFrom.Text = "Browse";
			this.butBrowseRestoreFrom.Click += new System.EventHandler(this.butBrowseRestoreFrom_Click);
			// 
			// textBackupRestoreFromPath
			// 
			this.textBackupRestoreFromPath.Location = new System.Drawing.Point(6,50);
			this.textBackupRestoreFromPath.Name = "textBackupRestoreFromPath";
			this.textBackupRestoreFromPath.Size = new System.Drawing.Size(481,20);
			this.textBackupRestoreFromPath.TabIndex = 13;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(19,136);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(279,55);
			this.textBox1.TabIndex = 9;
			this.textBox1.Text = "Backup TO this folder: (examples:)\r\nD:\\\r\nD:\\Backups\\\r\n\\\\frontdesk\\backups\\";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(19,62);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(240,43);
			this.textBox2.TabIndex = 12;
			this.textBox2.Text = "Backup database FROM this folder: (examples:)\r\nC:\\mysql\\data\\\r\n\\\\server\\mysql\\dat" +
    "a\\";
			// 
			// butBrowseFrom
			// 
			this.butBrowseFrom.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseFrom.Autosize = true;
			this.butBrowseFrom.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseFrom.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseFrom.CornerRadius = 4F;
			this.butBrowseFrom.Location = new System.Drawing.Point(512,104);
			this.butBrowseFrom.Name = "butBrowseFrom";
			this.butBrowseFrom.Size = new System.Drawing.Size(86,26);
			this.butBrowseFrom.TabIndex = 11;
			this.butBrowseFrom.Text = "Browse";
			this.butBrowseFrom.Click += new System.EventHandler(this.butBrowseFrom_Click);
			// 
			// textBackupFromPath
			// 
			this.textBackupFromPath.Location = new System.Drawing.Point(18,107);
			this.textBackupFromPath.Name = "textBackupFromPath";
			this.textBackupFromPath.Size = new System.Drawing.Size(481,20);
			this.textBackupFromPath.TabIndex = 10;
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(19,477);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(86,26);
			this.butSave.TabIndex = 13;
			this.butSave.Text = "Save Defaults";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// FormBackup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(777,526);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.butBrowseFrom);
			this.Controls.Add(this.textBackupFromPath);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butBrowseTo);
			this.Controls.Add(this.textBackupToPath);
			this.Controls.Add(this.butBackup);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBackup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Backup";
			this.Load += new System.EventHandler(this.FormBackup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormBackup_Load(object sender, System.EventArgs e) {
			//already include the \
			textBackupFromPath.Text=PrefB.GetString("BackupFromPath");
			textBackupToPath.Text=PrefB.GetString("BackupToPath");
			textBackupRestoreFromPath.Text=PrefB.GetString("BackupRestoreFromPath");
			textBackupRestoreToPath.Text=PrefB.GetString("BackupRestoreToPath");
			textBackupRestoreAtoZToPath.Text=PrefB.GetString("BackupRestoreAtoZToPath");
			textBox5.Enabled=PrefB.UsingAtoZfolder;
			textBackupRestoreAtoZToPath.Enabled=PrefB.UsingAtoZfolder;
			butBrowseRestoreAtoZTo.Enabled=PrefB.UsingAtoZfolder;
		}

		private void butBrowseFrom_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog browserDlg=new FolderBrowserDialog();
			browserDlg.SelectedPath=textBackupFromPath.Text;
			if(browserDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textBackupFromPath.Text=ODFileUtils.CombinePaths(browserDlg.SelectedPath,"");//Add trail slash.
		}

		private void butBrowseTo_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog browserDlg=new FolderBrowserDialog();
			browserDlg.SelectedPath=textBackupToPath.Text;
			if(browserDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textBackupToPath.Text=ODFileUtils.CombinePaths(browserDlg.SelectedPath,"");//Add trail slash.
		}

		private void butBrowseRestoreFrom_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog browserDlg=new FolderBrowserDialog();
			browserDlg.SelectedPath=textBackupRestoreFromPath.Text;
			if(browserDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textBackupRestoreFromPath.Text=ODFileUtils.CombinePaths(browserDlg.SelectedPath,"");//Add trail slash.
		}

		private void butBrowseRestoreTo_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog browserDlg=new FolderBrowserDialog();
			browserDlg.SelectedPath=textBackupRestoreToPath.Text;
			if(browserDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textBackupRestoreToPath.Text=ODFileUtils.CombinePaths(browserDlg.SelectedPath,"");//Add trail slash.
		}

		private void butBrowseRestoreAtoZTo_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog browserDlg=new FolderBrowserDialog();
			browserDlg.SelectedPath=textBackupRestoreAtoZToPath.Text;
			if(browserDlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textBackupRestoreAtoZToPath.Text=ODFileUtils.CombinePaths(browserDlg.SelectedPath,"");//Add trail slash.
		}

		private void butBackup_Click(object sender, System.EventArgs e) {
			//test for trailing slashes
			if(textBackupFromPath.Text!="" && !textBackupFromPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MsgBox.Show(this,"Paths must end with "+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupToPath.Text!="" && !textBackupToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MsgBox.Show(this,"Paths must end with "+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreFromPath.Text!="" && !textBackupRestoreFromPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MsgBox.Show(this,"Paths must end with "+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreToPath.Text!="" && !textBackupRestoreToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MsgBox.Show(this,"Paths must end with "+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreAtoZToPath.Text!="" && !textBackupRestoreAtoZToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MsgBox.Show(this,"Paths must end with "+Path.DirectorySeparatorChar+".");
				return;
			}
			//test saving defaults
			if(textBackupFromPath.Text!=PrefB.GetString("BackupFromPath")
				|| textBackupToPath.Text!=PrefB.GetString("BackupToPath")
				|| textBackupRestoreFromPath.Text!=PrefB.GetString("BackupRestoreFromPath")
				|| textBackupRestoreToPath.Text!=PrefB.GetString("BackupRestoreToPath")
				|| textBackupRestoreAtoZToPath.Text!=PrefB.GetString("BackupRestoreAtoZToPath"))
			{
				if(MsgBox.Show(this,true,"Set as default?")){
					Prefs.UpdateString("BackupFromPath",textBackupFromPath.Text);
					Prefs.UpdateString("BackupToPath",textBackupToPath.Text);
					Prefs.UpdateString("BackupRestoreFromPath",textBackupRestoreFromPath.Text);
					Prefs.UpdateString("BackupRestoreToPath",textBackupRestoreToPath.Text);
					Prefs.UpdateString("BackupRestoreAtoZToPath",textBackupRestoreAtoZToPath.Text);
					DataValid.SetInvalid(InvalidTypes.Prefs);
				}
			}
			string dbName=MiscData.GetCurrentDatabase();
			if(!Directory.Exists(ODFileUtils.CombinePaths(textBackupFromPath.Text,dbName))){// C:\mysql\data\opendental
				MsgBox.Show(this,"Backup FROM path is invalid.");
				return;
			}
			if(!Directory.Exists(textBackupToPath.Text)){// D:\
				MsgBox.Show(this,"Backup TO path is invalid.");
				return;
			}
			FormP=new FormProgress();
			FormP.MaxVal=100;//We will be setting maxVal from worker thread.  (double)fileSize/1024;
			FormP.NumberMultiplication=100;
			FormP.DisplayText="";//We will set the text from the worker thread.
			FormP.NumberFormat="N1";
			//start the thread that will perform the database copy
			Thread workerThread=new Thread(new ThreadStart(InstanceMethodBackup));
			workerThread.Start();
			//display the progress dialog to the user:
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				workerThread.Abort();
				return;
			}
			Close();
		}

		///<summary>This is the function that the worker thread uses to perform the backup.</summary>
		private void InstanceMethodBackup(){
			curVal=0;
			Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { curVal,
				Lan.g(this,"Preparing to copy database"),//this happens very fast and probably won't be noticed.
				100 });//max of 100 keeps dlg from closing
			string dbName=MiscData.GetCurrentDatabase();
			double dbSize=GetFileSizes(textBackupFromPath.Text+dbName)/1024;
			//lower level objects are used here to prevent closing the connection.
			//MySqlConnection con=new MySqlConnection(FormChooseDatabase.GetConnectionString());
			//MySqlCommand cmd=new MySqlCommand();
			//cmd.Connection=con;
			//cmd.CommandText="FLUSH TABLES WITH READ LOCK";//locks all tables for all databases 
			//con.Open();
			try{
				//cmd.ExecuteNonQuery();
				string dbtopath=ODFileUtils.CombinePaths(textBackupToPath.Text,dbName);
				if(Directory.Exists(dbtopath)){// D:\opendental
					Directory.Delete(dbtopath,true);
				}
				string fromPath=ODFileUtils.CombinePaths(textBackupFromPath.Text,dbName);
				string toPath=textBackupToPath.Text;
				//CopyDirectory(,);
				DirectoryInfo dirInfo=new DirectoryInfo(fromPath);//does not check to see if dir exists
				Directory.CreateDirectory(ODFileUtils.CombinePaths(toPath,dirInfo.Name));
				FileInfo[] files=dirInfo.GetFiles();
				curVal=0;//curVal gets increased
				for(int i=0;i<files.Length;i++){
					File.Copy(files[i].FullName,ODFileUtils.CombinePaths(new string [] {	toPath,dirInfo.Name,files[i].Name}));
					curVal+=(double)files[i].Length/(double)1024/(double)1024;
					if(curVal<dbSize){//this avoids setting progress bar to max, which would close the dialog.
						Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { curVal,
							Lan.g(this,"Database: ?currentVal MB of ?maxVal MB copied"),
							dbSize});
					}
				}
				//cmd.CommandText="UNLOCK TABLES";//might not be needed if con is actually closed.
				//cmd.ExecuteNonQuery();
			}
			catch{//for instance, if abort.
				//con.Close();
				//con.Dispose();
				return;
			}
			//con.Close();
			//con.Dispose();
			//A to Z folder------------------------------------------------------------------------------------
			if(PrefB.UsingAtoZfolder){
				string atozFull=ODFileUtils.RemoveTrailingSeparators(FormPath.GetPreferredImagePath());
				string atozDir=atozFull.Substring(atozFull.LastIndexOf(Path.DirectorySeparatorChar)+1);//OpenDentalData
				Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { 0,
					Lan.g(this,"Calculating size of files in A to Z folder."),
					100 });//max of 100 keeps dlg from closing
				int atozSize=GetFileSizes(ODFileUtils.CombinePaths(atozFull,""),
					ODFileUtils.CombinePaths(new string[] {textBackupToPath.Text,atozDir,""}))/1024;
				if(!Directory.Exists(ODFileUtils.CombinePaths(textBackupToPath.Text,atozDir))){// D:\OpenDentalData
					Directory.CreateDirectory(ODFileUtils.CombinePaths(textBackupToPath.Text,atozDir));// D:\OpenDentalData
				}
				curVal=0;
				CopyDirectoryIncremental(ODFileUtils.CombinePaths(atozFull,""),// C:\OpenDentalData\
					ODFileUtils.CombinePaths(new string[] {textBackupToPath.Text,atozDir,""}),// D:\OpenDentalData\
					atozSize);
			}
			//force dlg to close even if no files copied or calculation was slightly off.
			Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { 0,"",0});
		}

		///<summary>This is the function that the worker thread uses to restore the A-Z folder.</summary>
		private void InstanceMethodRestore(){
			curVal=0;
			string atozFull=textBackupRestoreAtoZToPath.Text;// C:\OpenDentalData\
			//remove the trailing \
			atozFull=atozFull.Substring(0,atozFull.Length-1);// C:\OpenDentalData
			string atozDir=atozFull.Substring(atozFull.LastIndexOf(Path.DirectorySeparatorChar)+1);// OpenDentalData
			Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { 0,
				Lan.g(this,"Database restored.\r\nCalculating size of files in A to Z folder."),
				100 });//max of 100 keeps dlg from closing
			int atozSize=GetFileSizes(ODFileUtils.CombinePaths(new string[] {textBackupRestoreFromPath.Text,atozDir,""}),
				ODFileUtils.CombinePaths(atozFull,""))/1024;// C:\OpenDentalData\
			if(!Directory.Exists(atozFull)){// C:\OpenDentalData\
				Directory.CreateDirectory(atozFull);// C:\OpenDentalData\
			}
			curVal=0;
			CopyDirectoryIncremental(ODFileUtils.CombinePaths(new string[] {textBackupRestoreFromPath.Text,atozDir,""}),
				ODFileUtils.CombinePaths(atozFull,""),// C:\OpenDentalData\
				atozSize);
			//force dlg to close even if no files copied or calculation was slightly off.
			Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { 0,"",0});
		}

		///<summary>This function gets invoked from the worker threads.</summary>
		private void PassProgressToDialog(double currentVal,string displayText,double maxVal){
			FormP.CurrentVal=currentVal;
			FormP.DisplayText=displayText;
			FormP.MaxVal=maxVal;
		}

		///<summary>Counts the total KB of all files that will need to be copied from one directory to another.  Recursive.  Only includes missing files, not changed files.  Used to display the progress bar.  Supplied paths must end in \. toPath might not exist.</summary>
		private int GetFileSizes(string fromPath,string toPath){
			int retVal=0;
			DirectoryInfo dirInfo=new DirectoryInfo(fromPath);
			DirectoryInfo[] dirs=dirInfo.GetDirectories();
			for(int i=0;i<dirs.Length;i++){
				retVal+=GetFileSizes(ODFileUtils.CombinePaths(dirs[i].FullName,""),
					ODFileUtils.CombinePaths(new string[] {toPath,dirs[i].Name,""}));
			}
			FileInfo[] files=dirInfo.GetFiles();//of fromPath
			for(int i=0;i<files.Length;i++){
				if(!File.Exists(ODFileUtils.CombinePaths(toPath,files[i].Name))){
					retVal+=(int)(files[i].Length/1024);
					//Debug.WriteLine("Added: "+toPath+files[i].Name);
				}
			}
			return retVal;
		}

		///<summary>Counts the total KB of all files in the given directory.  Not recursive since it's just used for db files.  Used to display the progress bar.</summary>
		private int GetFileSizes(string fromPath){
			int retVal=0;
			DirectoryInfo dirInfo=new DirectoryInfo(fromPath);
			FileInfo[] files=dirInfo.GetFiles();
			for(int i=0;i<files.Length;i++){
				retVal+=(int)(files[i].Length/1024);
			}
			return retVal;
		}

		///<summary>A recursive fuction that copies any new or changed files or folders from one directory to another.  An exception will be thrown if either directory does not already exist.  fromPath is the fully qualified path of the directory to copy.  toPath is the fully qualified path of the destination directory.  Both paths must include a trailing \.  The max size should be calculated ahead of time.  It's passed in for use in progress bar.</summary>
		private void CopyDirectoryIncremental(string fromPath,string toPath,double maxSize){
			if(!Directory.Exists(fromPath)){
				throw new Exception(fromPath+" does not exist.");
			}
			if(!Directory.Exists(toPath)){
				throw new Exception(toPath+" does not exist.");
			}
			DirectoryInfo dirInfo=new DirectoryInfo(fromPath);
			DirectoryInfo[] dirs=dirInfo.GetDirectories();
			for(int i=0;i<dirs.Length;i++){
				string destPath=ODFileUtils.CombinePaths(toPath,dirs[i].Name);
				if(!Directory.Exists(destPath)){
					Directory.CreateDirectory(destPath);
				}
				CopyDirectoryIncremental(ODFileUtils.CombinePaths(dirs[i].FullName,""),
					ODFileUtils.CombinePaths(destPath,""),maxSize);
			}
			FileInfo[] files=dirInfo.GetFiles();//of fromPath
			for(int i=0;i<files.Length;i++){
				string file=ODFileUtils.CombinePaths(toPath,files[i].Name);
				if(File.Exists(file)){
					if(files[i].LastWriteTime!=File.GetLastWriteTime(file)){//if modification dates don't match
						File.Copy(files[i].FullName,file,true);
						//we don't show user progress of this type of copy since rare
						/*curVal+=(double)files[i].Length/(double)1024/(double)1024;
						if(curVal<maxSize){//this avoids setting progress bar to max, which would close the dialog.
							Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { curVal,
								Lan.g(this,"A to Z folder: ?currentVal MB of ?maxVal MB copied"),
								maxSize});
						}*/
					}
				}
				else{//file doesn't exist, so copy
					File.Copy(files[i].FullName,file);
					curVal+=(double)files[i].Length/(double)1024/(double)1024;
					if(curVal<maxSize){//this avoids setting progress bar to max, which would close the dialog.
						Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { curVal,
							Lan.g(this,"A to Z folder: ?currentVal MB of ?maxVal MB copied"),
							maxSize});
					}
				}
			}
		}

		/*
		///<summary>A simple function that copies a directory and all its contents to a new location.  An exception will be thrown if the directory already exists.  fromPath is the fully qualified path of the directory to copy.  toPath is the parent directory into which it will be copied.  Both paths must include a trailing \.  Only used for db copy, and includes informing the user of progress.</summary>
		private void CopyDirectorySimplified(string fromPath,string toPath){
			DirectoryInfo dirInfo=new DirectoryInfo(fromPath);//does not check to see if dir exists
			Directory.CreateDirectory(toPath+dirInfo.Name);//TODO: Use ODFileUtils.CombinePaths()!
			DirectoryInfo[] dirs=dirInfo.GetDirectories();
			for(int i=0;i<dirs.Length;i++){
				CopyDirectory(dirs[i].FullName+@"\",toPath+dirInfo.Name+@"\");
			}
			FileInfo[] files=dirInfo.GetFiles();
			for(int i=0;i<files.Length;i++){
				File.Copy(files[i].FullName,toPath+dirInfo.Name+@"\"+files[i].Name);//TODO: Use ODFileUtils.CombinePaths()!
				curVal+=(double)files[i].Length/1024/1024;
					//((double)(chunk*i)+((double)buffer.Length/1024))/1024;
				Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { curVal });
			}
		}*/

		/*
		///<summary>A recursive fuction that copies a directory and all its contents to a new location.  An exception will be thrown if the directory already exists.  fromPath is the fully qualified path of the directory to copy.  toPath is the parent directory into which it will be copied.  Both paths must include a trailing \.</summary>
		private void CopyDirectory(string fromPath,string toPath){
			DirectoryInfo dirInfo=new DirectoryInfo(fromPath);//does not check to see if dir exists
			Directory.CreateDirectory(toPath+dirInfo.Name);//TODO: Use ODFileUtils.CombinePaths()!
			DirectoryInfo[] dirs=dirInfo.GetDirectories();
			for(int i=0;i<dirs.Length;i++){
				CopyDirectory(dirs[i].FullName+@"\",toPath+dirInfo.Name+@"\");
			}
			FileInfo[] files=dirInfo.GetFiles();
			for(int i=0;i<files.Length;i++){
				File.Copy(files[i].FullName,toPath+dirInfo.Name+@"\"+files[i].Name);//TODO: Use ODFileUtils.CombinePaths()!
				curVal+=(double)files[i].Length/1024/1024;
				//((double)(chunk*i)+((double)buffer.Length/1024))/1024;
				Invoke(new PassProgressDelegate(PassProgressToDialog),new object [] { curVal });
			}
		}*/

		private void butRestore_Click(object sender, System.EventArgs e) {			
			if(textBackupRestoreFromPath.Text!="" && !textBackupRestoreFromPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreToPath.Text!="" && !textBackupRestoreToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(PrefB.UsingAtoZfolder){
				if(textBackupRestoreAtoZToPath.Text!="" && !textBackupRestoreAtoZToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
					MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
					return;
				}
			}
			if(Environment.OSVersion.Platform!=PlatformID.Unix){
				//dmg This check will not work on Linux, because mapped drives exist as regular (mounted) paths. Perhaps there
				//is another way to check for this on Linux.
				if(textBackupRestoreToPath.Text!="" && textBackupRestoreToPath.Text.StartsWith(""+Path.DirectorySeparatorChar)){
					MsgBox.Show(this,"The restore database TO folder must be on this computer.");
					return;
				}
			}
			//pointless to save defaults
			string dbName=MiscData.GetCurrentDatabase();
			if(!Directory.Exists(ODFileUtils.CombinePaths(textBackupRestoreFromPath.Text,dbName))){// D:\opendental
				MsgBox.Show(this,"Restore FROM path is invalid.");
				return;
			}
			if(!Directory.Exists(ODFileUtils.CombinePaths(textBackupRestoreToPath.Text,dbName))){// C:\mysql\data\opendental
				MsgBox.Show(this,"Restore TO path is invalid.");
				return;
			}
			if(PrefB.UsingAtoZfolder){
				if(!Directory.Exists(textBackupRestoreAtoZToPath.Text)) {// C:\OpenDentalData\
					MsgBox.Show(this,"Restore A-Z images TO path is invalid.");
					return;
				}
				string atozFull=textBackupRestoreAtoZToPath.Text;// C:\OpenDentalData\
				//remove the trailing \
				atozFull=atozFull.Substring(0,atozFull.Length-1);// C:\OpenDentalData
				string atozDir=atozFull.Substring(atozFull.LastIndexOf(Path.DirectorySeparatorChar)+1);// OpenDentalData
				if(!Directory.Exists(ODFileUtils.CombinePaths(textBackupRestoreFromPath.Text,atozDir))){// D:\OpenDentalData
					MsgBox.Show(this,"Restore A-Z images FROM path is invalid.");
					return;
				}
			}
			string fromPath=ODFileUtils.CombinePaths(new string[] {textBackupRestoreFromPath.Text,dbName,""});// D:\opendental\
			DirectoryInfo dirInfo=new DirectoryInfo(fromPath);//does not check to see if dir exists
			if(MessageBox.Show(Lan.g(this,"Restore from backup created on")+"\r\n"
				+dirInfo.LastWriteTime.ToString("dddd")+"  "+dirInfo.LastWriteTime.ToString()
				,"",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.Cancel)
			{
				return;
			}
			Cursor=Cursors.WaitCursor;
			//stop the service--------------------------------------------------------------------------------------
			ServiceController sc=new ServiceController("MySQL");
			try{
				sc.Stop();
				sc.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,10));
			}
			catch{
				MsgBox.Show(this,"Unable to stop MySQL service.");
				return;
			}
			//rename the current database---------------------------------------------------------------------------
			//Get a name for the new directory
			string newDb=dbName+"backup_"+DateTime.Today.ToString("MM_dd_yyyy");
			if(Directory.Exists(ODFileUtils.CombinePaths(textBackupRestoreToPath.Text,newDb))){//if the new database name already exists
				//find a unique one
				int uniqueID=1;
				string originalNewDb=newDb;
				do{
					newDb=originalNewDb+"_"+uniqueID.ToString();
					uniqueID++;
				}
				while(Directory.Exists(ODFileUtils.CombinePaths(textBackupRestoreToPath.Text,newDb)));
			}
			//move the current db (rename)
			Directory.Move(ODFileUtils.CombinePaths(textBackupRestoreToPath.Text,dbName),// C:\mysql\data\opendental
				ODFileUtils.CombinePaths(textBackupRestoreToPath.Text,newDb));// C:\mysql\data\opendentalbackup_03_25_2006
			//Restore----------------------------------------------------------------------------------------------
			string toPath=textBackupRestoreToPath.Text;// C:\mysql\data\
			Directory.CreateDirectory(ODFileUtils.CombinePaths(toPath,dirInfo.Name));
			FileInfo[] files=dirInfo.GetFiles();
			curVal=0;//curVal gets increased
			for(int i=0;i<files.Length;i++){
				File.Copy(files[i].FullName,ODFileUtils.CombinePaths(new string[] {toPath,dirInfo.Name,files[i].Name}));
			}
			//start the service--------------------------------------------------------------------------------------
			try{
				sc.Start();
				sc.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,10));
			}
			catch{
				//do nothing
			}
			Cursor=Cursors.Default;
			//restore A-Z folder, and give user a chance to cancel it.
			if(PrefB.UsingAtoZfolder){
				FormP=new FormProgress();
				FormP.MaxVal=100;//We will be setting maxVal from worker thread.  (double)fileSize/1024;
				FormP.NumberMultiplication=100;
				FormP.DisplayText="";//We will set the text from the worker thread.
				FormP.NumberFormat="N1";
				//start the thread that will perform the database copy
				Thread workerThread=new Thread(new ThreadStart(InstanceMethodRestore));
				workerThread.Start();
				//display the progress dialog to the user:
				FormP.ShowDialog();
				if(FormP.DialogResult==DialogResult.Cancel){
					workerThread.Abort();
					return;
				}
			}
			MsgBox.Show(this,"Done");
			Prefs.CheckProgramVersion();//this might force program to close if version mismatch could not be resolved.
			Close();
		}

		private void FillTableWithData(StringBuilder tableData,string tempFile,string tableName){
			using(StreamWriter sw=new StreamWriter(tempFile,false)){//new file each time
				sw.Write(tableData.ToString());
			}
			string command="LOAD DATA INFILE '"+tempFile.Replace("\\","/")+"' INTO TABLE "+tableName;
			General.NonQ(command);
		}

		private void butSave_Click(object sender, System.EventArgs e) {
			//test for trailing slashes
			if(textBackupFromPath.Text!="" && !textBackupFromPath.Text.EndsWith(""+Path.DirectorySeparatorChar)){
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupToPath.Text!="" && !textBackupToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreFromPath.Text!="" && !textBackupRestoreFromPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreToPath.Text!="" && !textBackupRestoreToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupRestoreAtoZToPath.Text!="" && !textBackupRestoreAtoZToPath.Text.EndsWith(""+Path.DirectorySeparatorChar)) {
				MessageBox.Show(Lan.g(this,"Paths must end with ")+Path.DirectorySeparatorChar+".");
				return;
			}
			if(textBackupFromPath.Text!=PrefB.GetString("BackupFromPath")
				|| textBackupToPath.Text!=PrefB.GetString("BackupToPath")
				|| textBackupRestoreFromPath.Text!=PrefB.GetString("BackupRestoreFromPath")
				|| textBackupRestoreToPath.Text!=PrefB.GetString("BackupRestoreToPath")
				|| textBackupRestoreAtoZToPath.Text!=PrefB.GetString("BackupRestoreAtoZToPath"))
			{
				Prefs.UpdateString("BackupFromPath",textBackupFromPath.Text);
				Prefs.UpdateString("BackupToPath",textBackupToPath.Text);
				Prefs.UpdateString("BackupRestoreFromPath",textBackupRestoreFromPath.Text);
				Prefs.UpdateString("BackupRestoreToPath",textBackupRestoreToPath.Text);
				Prefs.UpdateString("BackupRestoreAtoZToPath",textBackupRestoreAtoZToPath.Text);
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		


		

		

	}
}





















