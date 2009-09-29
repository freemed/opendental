using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using OpenDental.Imaging;

namespace OpenDental{
///<summary></summary>
	public class FormPath : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textExportPath;
		private System.Windows.Forms.TextBox textDocPath;
		private OpenDental.UI.Button butBrowseExport;
		private OpenDental.UI.Button butBrowseDoc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butBrowseLetter;
		private System.Windows.Forms.TextBox textLetterMergePath;
		private FolderBrowserDialog fb;
		private CheckBox checkMultiplePaths;
		private RadioButton optDisableFolders;
		private RadioButton optUseDatabase;
		private RadioButton optUseFolder;
		private Label label4;
		private TextBox textLocalPath;
		private OpenDental.UI.Button butBrowseLocal;
		private GroupBox groupbox1;
    //private bool IsBackup=false;
		//private User user;
		///<summary>Remembers the computerpref.AtoZpath.</summary>
		private static string localAtoZpath=null;

		///<summary></summary>
		public FormPath(){
			InitializeComponent();
			Lan.F(this);
			//Lan.C(this, new System.Windows.Forms.Control[] {
			//	this.textBox1,
			//	this.textBox3
			//});
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPath));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textDocPath = new System.Windows.Forms.TextBox();
			this.textExportPath = new System.Windows.Forms.TextBox();
			this.butBrowseExport = new OpenDental.UI.Button();
			this.butBrowseDoc = new OpenDental.UI.Button();
			this.fb = new System.Windows.Forms.FolderBrowserDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butBrowseLetter = new OpenDental.UI.Button();
			this.textLetterMergePath = new System.Windows.Forms.TextBox();
			this.checkMultiplePaths = new System.Windows.Forms.CheckBox();
			this.groupbox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textLocalPath = new System.Windows.Forms.TextBox();
			this.butBrowseLocal = new OpenDental.UI.Button();
			this.optDisableFolders = new System.Windows.Forms.RadioButton();
			this.optUseDatabase = new System.Windows.Forms.RadioButton();
			this.optUseFolder = new System.Windows.Forms.RadioButton();
			this.groupbox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(440,483);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
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
			this.butCancel.Location = new System.Drawing.Point(539,483);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDocPath
			// 
			this.textDocPath.Location = new System.Drawing.Point(26,82);
			this.textDocPath.Name = "textDocPath";
			this.textDocPath.Size = new System.Drawing.Size(497,20);
			this.textDocPath.TabIndex = 0;
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(19,346);
			this.textExportPath.Name = "textExportPath";
			this.textExportPath.Size = new System.Drawing.Size(515,20);
			this.textExportPath.TabIndex = 1;
			// 
			// butBrowseExport
			// 
			this.butBrowseExport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseExport.Autosize = true;
			this.butBrowseExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseExport.CornerRadius = 4F;
			this.butBrowseExport.Location = new System.Drawing.Point(538,343);
			this.butBrowseExport.Name = "butBrowseExport";
			this.butBrowseExport.Size = new System.Drawing.Size(76,25);
			this.butBrowseExport.TabIndex = 91;
			this.butBrowseExport.Text = "Browse";
			this.butBrowseExport.Click += new System.EventHandler(this.butBrowseExport_Click);
			// 
			// butBrowseDoc
			// 
			this.butBrowseDoc.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseDoc.Autosize = true;
			this.butBrowseDoc.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseDoc.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseDoc.CornerRadius = 4F;
			this.butBrowseDoc.Location = new System.Drawing.Point(529,78);
			this.butBrowseDoc.Name = "butBrowseDoc";
			this.butBrowseDoc.Size = new System.Drawing.Size(76,25);
			this.butBrowseDoc.TabIndex = 90;
			this.butBrowseDoc.Text = "&Browse";
			this.butBrowseDoc.Click += new System.EventHandler(this.butBrowseDoc_Click);
			// 
			// fb
			// 
			this.fb.SelectedPath = "C:\\";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20,282);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(596,59);
			this.label1.TabIndex = 92;
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(26,37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(579,41);
			this.label2.TabIndex = 93;
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20,372);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(596,57);
			this.label3.TabIndex = 96;
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butBrowseLetter
			// 
			this.butBrowseLetter.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseLetter.Autosize = true;
			this.butBrowseLetter.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseLetter.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseLetter.CornerRadius = 4F;
			this.butBrowseLetter.Location = new System.Drawing.Point(538,432);
			this.butBrowseLetter.Name = "butBrowseLetter";
			this.butBrowseLetter.Size = new System.Drawing.Size(76,25);
			this.butBrowseLetter.TabIndex = 95;
			this.butBrowseLetter.Text = "Browse";
			this.butBrowseLetter.Click += new System.EventHandler(this.butBrowseLetter_Click);
			// 
			// textLetterMergePath
			// 
			this.textLetterMergePath.Location = new System.Drawing.Point(19,435);
			this.textLetterMergePath.Name = "textLetterMergePath";
			this.textLetterMergePath.Size = new System.Drawing.Size(515,20);
			this.textLetterMergePath.TabIndex = 94;
			// 
			// checkMultiplePaths
			// 
			this.checkMultiplePaths.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkMultiplePaths.Location = new System.Drawing.Point(26,108);
			this.checkMultiplePaths.Name = "checkMultiplePaths";
			this.checkMultiplePaths.Size = new System.Drawing.Size(580,44);
			this.checkMultiplePaths.TabIndex = 98;
			this.checkMultiplePaths.Text = resources.GetString("checkMultiplePaths.Text");
			this.checkMultiplePaths.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.checkMultiplePaths.UseVisualStyleBackColor = true;
			// 
			// groupbox1
			// 
			this.groupbox1.Controls.Add(this.label4);
			this.groupbox1.Controls.Add(this.textLocalPath);
			this.groupbox1.Controls.Add(this.butBrowseLocal);
			this.groupbox1.Controls.Add(this.optDisableFolders);
			this.groupbox1.Controls.Add(this.optUseDatabase);
			this.groupbox1.Controls.Add(this.optUseFolder);
			this.groupbox1.Controls.Add(this.checkMultiplePaths);
			this.groupbox1.Controls.Add(this.label2);
			this.groupbox1.Controls.Add(this.textDocPath);
			this.groupbox1.Controls.Add(this.butBrowseDoc);
			this.groupbox1.Location = new System.Drawing.Point(10,12);
			this.groupbox1.Name = "groupbox1";
			this.groupbox1.Size = new System.Drawing.Size(654,267);
			this.groupbox1.TabIndex = 99;
			this.groupbox1.TabStop = false;
			this.groupbox1.Text = "A to Z Images Folder for storing images and documents";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(26,152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(622,36);
			this.label4.TabIndex = 104;
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textLocalPath
			// 
			this.textLocalPath.Location = new System.Drawing.Point(26,194);
			this.textLocalPath.Name = "textLocalPath";
			this.textLocalPath.Size = new System.Drawing.Size(497,20);
			this.textLocalPath.TabIndex = 102;
			// 
			// butBrowseLocal
			// 
			this.butBrowseLocal.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseLocal.Autosize = true;
			this.butBrowseLocal.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseLocal.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseLocal.CornerRadius = 4F;
			this.butBrowseLocal.Location = new System.Drawing.Point(529,190);
			this.butBrowseLocal.Name = "butBrowseLocal";
			this.butBrowseLocal.Size = new System.Drawing.Size(76,25);
			this.butBrowseLocal.TabIndex = 103;
			this.butBrowseLocal.Text = "Browse";
			this.butBrowseLocal.Click += new System.EventHandler(this.butBrowseLocal_Click);
			// 
			// optDisableFolders
			// 
			this.optDisableFolders.Location = new System.Drawing.Point(10,243);
			this.optDisableFolders.Name = "optDisableFolders";
			this.optDisableFolders.Size = new System.Drawing.Size(311,17);
			this.optDisableFolders.TabIndex = 101;
			this.optDisableFolders.TabStop = true;
			this.optDisableFolders.Text = "Do not use folder. (Some features will be unavailable)";
			this.optDisableFolders.UseVisualStyleBackColor = true;
			this.optDisableFolders.Click += new System.EventHandler(this.optDisableFolders_Click);
			this.optDisableFolders.CheckedChanged += new System.EventHandler(this.optUseFolder_CheckedChanged);
			// 
			// optUseDatabase
			// 
			this.optUseDatabase.Location = new System.Drawing.Point(10,220);
			this.optUseDatabase.Name = "optUseDatabase";
			this.optUseDatabase.Size = new System.Drawing.Size(596,17);
			this.optUseDatabase.TabIndex = 100;
			this.optUseDatabase.TabStop = true;
			this.optUseDatabase.Text = "Store images and documents in the database. (there is no synching mechanism for a" +
    "ny current images)";
			this.optUseDatabase.UseVisualStyleBackColor = true;
			this.optUseDatabase.Click += new System.EventHandler(this.optUseDatabase_Click);
			this.optUseDatabase.CheckedChanged += new System.EventHandler(this.optUseFolder_CheckedChanged);
			// 
			// optUseFolder
			// 
			this.optUseFolder.Location = new System.Drawing.Point(9,19);
			this.optUseFolder.Name = "optUseFolder";
			this.optUseFolder.Size = new System.Drawing.Size(333,17);
			this.optUseFolder.TabIndex = 99;
			this.optUseFolder.TabStop = true;
			this.optUseFolder.Text = "Store images and documents on a local or network folder.";
			this.optUseFolder.UseVisualStyleBackColor = true;
			this.optUseFolder.CheckedChanged += new System.EventHandler(this.optUseFolder_CheckedChanged);
			// 
			// FormPath
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(683,521);
			this.Controls.Add(this.groupbox1);
			this.Controls.Add(this.butBrowseLetter);
			this.Controls.Add(this.butBrowseExport);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textLetterMergePath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textExportPath);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPath";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Paths";
			this.Load += new System.EventHandler(this.FormPath_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPath_Closing);
			this.groupbox1.ResumeLayout(false);
			this.groupbox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPath_Load(object sender, System.EventArgs e){
			textDocPath.Text=PrefC.GetString("DocPath");
			//ComputerPref compPref=ComputerPrefs.GetForLocalComputer();
			textLocalPath.Text=localAtoZpath;//This was set on startup.  //compPref.AtoZpath;
			textExportPath.Text=PrefC.GetString("ExportPath");
			textLetterMergePath.Text=PrefC.GetString("LetterMergePath");
			if(PrefC.GetBool("AtoZfolderNotRequired")) {
				optDisableFolders.Checked = true;
			} else if(ImageStoreBase.ImageStoreTypeName == typeof(SqlStore).FullName) {
				optUseDatabase.Checked = true;
			} else {
				optUseFolder.Checked = true;
			}
			// The opt***_checked event will enable/disable the appropriate UI elements.
			checkMultiplePaths.Checked=(textDocPath.Text.LastIndexOf(';')!=-1);	//Also set the "multiple paths" checkbox at
																																					//startup based on the current image folder list
																																					//format. No need to store this info in the db.
		}

		///<summary>Returns the given path with the local OS path separators as necessary.</summary>
		public static string FixDirSeparators(string path){
			if(Environment.OSVersion.Platform==PlatformID.Unix){
				path.Replace('\\',Path.DirectorySeparatorChar);
			}else{//Windows
				path.Replace('/',Path.DirectorySeparatorChar);
			}
			return path;
		}

		private void butBrowseDoc_Click(object sender,EventArgs e) {
			if(fb.ShowDialog()!=DialogResult.OK) {
				return;
			}
			//Ensure that the path entered has slashes matching the current OS (in case entered manually).
			string path=FixDirSeparators(fb.SelectedPath);
			if(checkMultiplePaths.Checked && textDocPath.Text.Length>0) {
				string messageText=Lan.g(this,"Replace existing document paths? Click No to add path to existing document paths.");
				switch(MessageBox.Show(messageText,"",MessageBoxButtons.YesNoCancel)) {
					case DialogResult.Yes:
						textDocPath.Text=path;//Replace existing paths with new path.
						break;
					case DialogResult.No://Append to existing paths?
						//Do not append a path which is already present in the list.
						if(!IsImagePath(path,textDocPath.Text)){
							textDocPath.Text=textDocPath.Text+";"+path;
						}
						break;
					default://Cancel button.
						break;
				}
			}else{
				textDocPath.Text=path;//Just replace existing paths with new path.
			}
		}

		private void butBrowseLocal_Click(object sender,EventArgs e) {
			if(fb.ShowDialog()==DialogResult.OK) {
				textLocalPath.Text=fb.SelectedPath;
			}
		}

		private void butBrowseExport_Click(object sender, System.EventArgs e){
		  if(fb.ShowDialog()==DialogResult.OK){
				textExportPath.Text=fb.SelectedPath;
			}
		}

		private void butBrowseLetter_Click(object sender, System.EventArgs e) {
			if(fb.ShowDialog()==DialogResult.OK){
				textLetterMergePath.Text=fb.SelectedPath;
			}
		}

		private static string GetPreferredImagePath(string documentPaths){
			string[] preferredPathsByOrder=documentPaths.Split(new char[] { ';' });
			for(int i=0;i<preferredPathsByOrder.Length;i++){
				string path=preferredPathsByOrder[i];
				string tryPath=ODFileUtils.CombinePaths(path,"A");
				if(Directory.Exists(tryPath)) {
					return path;
				}
			}
			return null;
		}

		///<summary>Returns the most preferred fully qualified network path or null if no valid paths were found.</summary>
		public static string GetPreferredImagePath(){
			if(!PrefC.UsingAtoZfolder) {
				return null;
			}
			//this requires a query to the database each time this method is used.  This might need to be optimized.
			//ComputerPref compPref=ComputerPrefs.GetForLocalComputer();
			if(localAtoZpath==null){//on startup
				localAtoZpath=ComputerPrefs.GetForLocalComputer().AtoZpath;
				FileStoreSettings.LocalAtoZpath=localAtoZpath;
			}
			if(localAtoZpath!=""){//compPref.AtoZpath!=""){
				return localAtoZpath;//compPref.AtoZpath;
			}
			return GetPreferredImagePath(PrefC.GetString("DocPath"));
		}

		///<summary>Returns true if the given path is part of the imagePaths list, false otherwise.</summary>
		public static bool IsImagePath(string path,string imagePaths){
			string[] pathArray=imagePaths.Split(new char[] { ';' });
			for(int i=0;i<pathArray.Length;i++){
				if(pathArray[i]==path){//Case sensitive (since these could be unix paths).
					return true;
				}
			}
			return false;
		}

		private void optUseFolder_CheckedChanged(object sender, EventArgs e) {
			label2.Enabled = optUseFolder.Checked;
			textDocPath.Enabled = optUseFolder.Checked;
			butBrowseDoc.Enabled = optUseFolder.Checked;
			checkMultiplePaths.Enabled = optUseFolder.Checked;
			textLocalPath.Enabled=optUseFolder.Checked;
			butBrowseLocal.Enabled=optUseFolder.Checked;
		}

		private void optUseDatabase_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"This is not recommended.  Only for experimental use by advanced users.  Continue anyway?")){
				optUseFolder.Checked=true;
				return;
			}
		}

		private void optDisableFolders_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"This is not recommended.  Only for experimental use by advanced users.  Continue anyway?")){
				optUseFolder.Checked=true;
				return;
			}
		}

		///<summary>Returns true if the given path is part of the image paths stored in the database list, false otherwise.</summary>
		public static bool IsImagePath(string path){
			string imagePaths=PrefC.GetString("DocPath");
			return IsImagePath(path,imagePaths);
		}

		private void butOK_Click(object sender, System.EventArgs e){
			/*string remoteUri = "http://www.open-dent.com/languages/";
			string fileName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName+".sql";//eg. es.sql for spanish
			//string fileName="bogus.sql";
			string myStringWebResource = null;
			WebClient myWebClient = new WebClient();
			myStringWebResource = remoteUri + fileName;
			try{
				//myWebClient.Credentials=new NetworkCredential("username","password","www.open-dent.com");
				myWebClient.DownloadFile(myStringWebResource,fileName);
			}
			catch{
				MessageBox.Show("Either you do not have internet access, or no translations are available for "+CultureInfo.CurrentCulture.Parent.DisplayName);
				return;
			}
			ClassConvertDatabase ConvertDB=new ClassConvertDatabase();
			if(!ConvertDB.ExecuteFile(fileName)){
				MessageBox.Show("Translations not installed properly.");
				return;
			}
			LanguageForeigns.Refresh();
			MessageBox.Show("Done");*/
			//remember that user might be using a website or a linux box to store images, therefore must allow forward slashes.
			if(optUseFolder.Checked && GetPreferredImagePath(textDocPath.Text)==null && GetPreferredImagePath(textLocalPath.Text)==null){
				MsgBox.Show(this,"Please enter a valid path in one of the first two boxes.");
				return;
    	}
			if(
				Prefs.UpdateBool("AtoZfolderNotRequired",optDisableFolders.Checked)
				| Prefs.UpdateString("DocPath",textDocPath.Text)
				| Prefs.UpdateString("ExportPath",textExportPath.Text)
				| Prefs.UpdateString("LetterMergePath",textLetterMergePath.Text)
				| Prefs.UpdateString("ImageStore",optUseDatabase.Checked ? typeof(SqlStore).FullName : typeof(FileStore).FullName))
			{
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			if(localAtoZpath!=textLocalPath.Text){//if local path changed
				localAtoZpath=textLocalPath.Text;
				ComputerPref compPref=ComputerPrefs.GetForLocalComputer();
				compPref.AtoZpath=localAtoZpath;
				ComputerPrefs.Update(compPref);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormPath_Closing(object sender,System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(optUseFolder.Checked && GetPreferredImagePath(textDocPath.Text)==null && GetPreferredImagePath(textLocalPath.Text)==null) {
				MsgBox.Show(this,"Invalid A to Z path.  Closing program.");
				Application.Exit();
			}
		}

		

		
	}
}
