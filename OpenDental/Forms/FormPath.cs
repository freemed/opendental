using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

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
		private System.Windows.Forms.Label labelPathSameForAll;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butBrowseLetter;
		private System.Windows.Forms.TextBox textLetterMergePath;
		private FolderBrowserDialog fb;
		private CheckBox checkMultiplePaths;
		private RadioButton radioAtoZfolderNotRequired;
		private RadioButton radioUseFolder;
		private Label labelLocalPath;
		private TextBox textLocalPath;
		private OpenDental.UI.Button butBrowseLocal;
		private OpenDental.UI.Button butBrowseServer;
		private Label labelServerPath;
		private TextBox textServerPath;
		private GroupBox groupbox1;
		///<summary>If this is set to true before opening this form, then the program cannot find the AtoZ path and needs user input.</summary>
		public bool IsStartingUp;

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
			this.labelPathSameForAll = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butBrowseLetter = new OpenDental.UI.Button();
			this.textLetterMergePath = new System.Windows.Forms.TextBox();
			this.checkMultiplePaths = new System.Windows.Forms.CheckBox();
			this.groupbox1 = new System.Windows.Forms.GroupBox();
			this.butBrowseLocal = new OpenDental.UI.Button();
			this.butBrowseServer = new OpenDental.UI.Button();
			this.labelServerPath = new System.Windows.Forms.Label();
			this.textServerPath = new System.Windows.Forms.TextBox();
			this.labelLocalPath = new System.Windows.Forms.Label();
			this.textLocalPath = new System.Windows.Forms.TextBox();
			this.radioAtoZfolderNotRequired = new System.Windows.Forms.RadioButton();
			this.radioUseFolder = new System.Windows.Forms.RadioButton();
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
			this.butOK.Location = new System.Drawing.Point(440,502);
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
			this.butCancel.Location = new System.Drawing.Point(539,502);
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
			this.textDocPath.TabIndex = 1;
			// 
			// textExportPath
			// 
			this.textExportPath.Location = new System.Drawing.Point(19,358);
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
			this.butBrowseExport.Location = new System.Drawing.Point(538,355);
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
			this.butBrowseDoc.TabIndex = 2;
			this.butBrowseDoc.Text = "&Browse";
			this.butBrowseDoc.Click += new System.EventHandler(this.butBrowseDoc_Click);
			// 
			// fb
			// 
			this.fb.SelectedPath = "C:\\";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20,294);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(596,59);
			this.label1.TabIndex = 92;
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelPathSameForAll
			// 
			this.labelPathSameForAll.Location = new System.Drawing.Point(26,37);
			this.labelPathSameForAll.Name = "labelPathSameForAll";
			this.labelPathSameForAll.Size = new System.Drawing.Size(579,41);
			this.labelPathSameForAll.TabIndex = 93;
			this.labelPathSameForAll.Text = resources.GetString("labelPathSameForAll.Text");
			this.labelPathSameForAll.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20,384);
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
			this.butBrowseLetter.Location = new System.Drawing.Point(538,444);
			this.butBrowseLetter.Name = "butBrowseLetter";
			this.butBrowseLetter.Size = new System.Drawing.Size(76,25);
			this.butBrowseLetter.TabIndex = 95;
			this.butBrowseLetter.Text = "Browse";
			this.butBrowseLetter.Click += new System.EventHandler(this.butBrowseLetter_Click);
			// 
			// textLetterMergePath
			// 
			this.textLetterMergePath.Location = new System.Drawing.Point(19,447);
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
			this.groupbox1.Controls.Add(this.butBrowseLocal);
			this.groupbox1.Controls.Add(this.butBrowseServer);
			this.groupbox1.Controls.Add(this.labelServerPath);
			this.groupbox1.Controls.Add(this.textServerPath);
			this.groupbox1.Controls.Add(this.labelLocalPath);
			this.groupbox1.Controls.Add(this.textLocalPath);
			this.groupbox1.Controls.Add(this.radioAtoZfolderNotRequired);
			this.groupbox1.Controls.Add(this.radioUseFolder);
			this.groupbox1.Controls.Add(this.checkMultiplePaths);
			this.groupbox1.Controls.Add(this.labelPathSameForAll);
			this.groupbox1.Controls.Add(this.textDocPath);
			this.groupbox1.Controls.Add(this.butBrowseDoc);
			this.groupbox1.Location = new System.Drawing.Point(10,12);
			this.groupbox1.Name = "groupbox1";
			this.groupbox1.Size = new System.Drawing.Size(654,291);
			this.groupbox1.TabIndex = 0;
			this.groupbox1.TabStop = false;
			this.groupbox1.Text = "A to Z Images Folder for storing images and documents";
			// 
			// butBrowseLocal
			// 
			this.butBrowseLocal.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseLocal.Autosize = true;
			this.butBrowseLocal.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseLocal.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseLocal.CornerRadius = 4F;
			this.butBrowseLocal.Location = new System.Drawing.Point(529,224);
			this.butBrowseLocal.Name = "butBrowseLocal";
			this.butBrowseLocal.Size = new System.Drawing.Size(76,25);
			this.butBrowseLocal.TabIndex = 103;
			this.butBrowseLocal.Text = "Browse";
			this.butBrowseLocal.Click += new System.EventHandler(this.butBrowseLocal_Click);
			// 
			// butBrowseServer
			// 
			this.butBrowseServer.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseServer.Autosize = true;
			this.butBrowseServer.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseServer.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseServer.CornerRadius = 4F;
			this.butBrowseServer.Location = new System.Drawing.Point(529,178);
			this.butBrowseServer.Name = "butBrowseServer";
			this.butBrowseServer.Size = new System.Drawing.Size(76,25);
			this.butBrowseServer.TabIndex = 106;
			this.butBrowseServer.Text = "Browse";
			this.butBrowseServer.Click += new System.EventHandler(this.butBrowseServer_Click);
			// 
			// labelServerPath
			// 
			this.labelServerPath.Location = new System.Drawing.Point(26,162);
			this.labelServerPath.Name = "labelServerPath";
			this.labelServerPath.Size = new System.Drawing.Size(488,17);
			this.labelServerPath.TabIndex = 107;
			this.labelServerPath.Text = "Path override for this server.  Usually leave blank.";
			this.labelServerPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textServerPath
			// 
			this.textServerPath.Location = new System.Drawing.Point(26,182);
			this.textServerPath.Name = "textServerPath";
			this.textServerPath.Size = new System.Drawing.Size(497,20);
			this.textServerPath.TabIndex = 105;
			// 
			// labelLocalPath
			// 
			this.labelLocalPath.Location = new System.Drawing.Point(26,208);
			this.labelLocalPath.Name = "labelLocalPath";
			this.labelLocalPath.Size = new System.Drawing.Size(498,17);
			this.labelLocalPath.TabIndex = 104;
			this.labelLocalPath.Text = "Path override for this computer.  Usually leave blank.";
			this.labelLocalPath.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textLocalPath
			// 
			this.textLocalPath.Location = new System.Drawing.Point(26,228);
			this.textLocalPath.Name = "textLocalPath";
			this.textLocalPath.Size = new System.Drawing.Size(497,20);
			this.textLocalPath.TabIndex = 102;
			// 
			// radioAtoZfolderNotRequired
			// 
			this.radioAtoZfolderNotRequired.Location = new System.Drawing.Point(10,262);
			this.radioAtoZfolderNotRequired.Name = "radioAtoZfolderNotRequired";
			this.radioAtoZfolderNotRequired.Size = new System.Drawing.Size(311,17);
			this.radioAtoZfolderNotRequired.TabIndex = 101;
			this.radioAtoZfolderNotRequired.TabStop = true;
			this.radioAtoZfolderNotRequired.Text = "Do not use folder. (Some features will be unavailable)";
			this.radioAtoZfolderNotRequired.UseVisualStyleBackColor = true;
			this.radioAtoZfolderNotRequired.Click += new System.EventHandler(this.radioAtoZfolderNotRequired_Click);
			// 
			// radioUseFolder
			// 
			this.radioUseFolder.Location = new System.Drawing.Point(9,19);
			this.radioUseFolder.Name = "radioUseFolder";
			this.radioUseFolder.Size = new System.Drawing.Size(333,17);
			this.radioUseFolder.TabIndex = 0;
			this.radioUseFolder.TabStop = true;
			this.radioUseFolder.Text = "Store images and documents on a local or network folder.";
			this.radioUseFolder.UseVisualStyleBackColor = true;
			this.radioUseFolder.CheckedChanged += new System.EventHandler(this.radioUseFolder_CheckedChanged);
			// 
			// FormPath
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(683,540);
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
			if(IsStartingUp) {//and failed to find path
				//if(Security.IsAuthorized(Permissions.Setup,true)) {//suppress the regular message//can't do this because we don't have access to security yet.
				//	MsgBox.Show(this,"Could not find the path for the AtoZ folder");
				//}
				//else{//not authorized for security
				MsgBox.Show(this,"Could not find the path for the AtoZ folder.");
				//butOK.Enabled=false;
				//}
			}
			else if(!Security.IsAuthorized(Permissions.Setup)) {
				butOK.Enabled=false;
			}
			textDocPath.Text=PrefC.GetString(PrefName.DocPath);
			//ComputerPref compPref=ComputerPrefs.GetForLocalComputer();
			if(ReplicationServers.Server_id==0) {
				labelServerPath.Visible=false;
				textServerPath.Visible=false;
				butBrowseServer.Visible=false;
			}
			else {
				labelServerPath.Text="Path override for this server.  Server id = "+ReplicationServers.Server_id.ToString();
				textServerPath.Text=ReplicationServers.GetAtoZpath();
			}
			textLocalPath.Text=ImageStore.LocalAtoZpath;//This was set on startup.  //compPref.AtoZpath;
			textExportPath.Text=PrefC.GetString(PrefName.ExportPath);
			textLetterMergePath.Text=PrefC.GetString(PrefName.LetterMergePath);
			if(PrefC.GetBool(PrefName.AtoZfolderNotRequired)) {
				radioAtoZfolderNotRequired.Checked = true;
			}  
			else {
				radioUseFolder.Checked = true;
			}
			// The opt***_checked event will enable/disable the appropriate UI elements.
			checkMultiplePaths.Checked=(textDocPath.Text.LastIndexOf(';')!=-1);	
			//Also set the "multiple paths" checkbox at startup based on the current image folder list format. No need to store this info in the db.
		}

		///<summary>Returns the given path with the local OS path separators as necessary.</summary>
		public static string FixDirSeparators(string path){
			if(Environment.OSVersion.Platform==PlatformID.Unix){
				path.Replace('\\',Path.DirectorySeparatorChar);
			}
			else{//Windows
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
						if(!IsPathInList(path,textDocPath.Text)) {
							textDocPath.Text=textDocPath.Text+";"+path;
						}
						break;
					default://Cancel button.
						break;
				}
			}
			else{
				textDocPath.Text=path;//Just replace existing paths with new path.
			}
		}

		private void butBrowseServer_Click(object sender,EventArgs e) {
			if(fb.ShowDialog()==DialogResult.OK) {
				textServerPath.Text=fb.SelectedPath;
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

		///<summary>Returns true if the given path is part of the imagePaths list, false otherwise.</summary>
		private static bool IsPathInList(string path,string imagePaths){
			string[] pathArray=imagePaths.Split(new char[] { ';' });
			for(int i=0;i<pathArray.Length;i++){
				if(pathArray[i]==path){//Case sensitive (since these could be unix paths).
					return true;
				}
			}
			return false;
		}

		private void radioUseFolder_CheckedChanged(object sender,EventArgs e) {
			labelPathSameForAll.Enabled = radioUseFolder.Checked;
			textDocPath.Enabled = radioUseFolder.Checked;
			butBrowseDoc.Enabled = radioUseFolder.Checked;
			checkMultiplePaths.Enabled = radioUseFolder.Checked;
			//even though server path might not be visible:
			labelServerPath.Enabled=radioUseFolder.Checked;
			textServerPath.Enabled=radioUseFolder.Checked;
			butBrowseServer.Enabled=radioUseFolder.Checked;
			//
			labelLocalPath.Enabled=radioUseFolder.Checked;
			textLocalPath.Enabled=radioUseFolder.Checked;
			butBrowseLocal.Enabled=radioUseFolder.Checked;
		}

		private void radioAtoZfolderNotRequired_Click(object sender,EventArgs e) {
			if(radioAtoZfolderNotRequired.Checked 
				&& !MsgBox.Show(this,MsgBoxButtons.OKCancel,"Warning! This is not recommended.  Only for advanced users.  Continue anyway?")) 
			{
				radioUseFolder.Checked=true;
				return;
			}
		}

		/*
		///<summary>Returns true if the given path is part of the image paths stored in the database list, false otherwise.</summary>
		public static bool IsImagePath(string path){
			string imagePaths=PrefC.GetString(PrefName.DocPath");
			return IsImagePath(path,imagePaths);
		}*/

		private void butOK_Click(object sender, System.EventArgs e){
			//remember that user might be using a website or a linux box to store images, therefore must allow forward slashes.
			if(radioUseFolder.Checked){
				if(textLocalPath.Text!=""){
					if(!Directory.Exists(textLocalPath.Text)){
						MsgBox.Show(this,"The path override for this computer is invalid.");
						return;
					}
				}
				else if(textServerPath.Text!=""){
					if(!Directory.Exists(textServerPath.Text)){
						MsgBox.Show(this,"The path override for this server is invalid.");
						return;
					}
				}
				else{
					if(ImageStore.GetValidPathFromString(textDocPath.Text)==null){
						MsgBox.Show(this,"The path is invalid.  The folder must exist and must contain all 26 A through Z folders.");
						return;
					}
				}				
    	}
			if(	Prefs.UpdateBool(PrefName.AtoZfolderNotRequired,radioAtoZfolderNotRequired.Checked)
				| Prefs.UpdateString(PrefName.DocPath,textDocPath.Text)
				| Prefs.UpdateString(PrefName.ExportPath,textExportPath.Text)
				| Prefs.UpdateString(PrefName.LetterMergePath,textLetterMergePath.Text))
			{
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			if(ImageStore.LocalAtoZpath!=textLocalPath.Text) {//if local path changed
				ImageStore.LocalAtoZpath=textLocalPath.Text;
				ComputerPref compPref=ComputerPrefs.GetForLocalComputer();
				compPref.AtoZpath=ImageStore.LocalAtoZpath;
				ComputerPrefs.Update(compPref);
			}
			if(ReplicationServers.GetAtoZpath()!=textServerPath.Text) {
				ReplicationServer server=ReplicationServers.GetForLocalComputer();
				server.AtoZpath=textServerPath.Text;
				ReplicationServers.Update(server);
				DataValid.SetInvalid(InvalidType.ReplicationServers);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormPath_Closing(object sender,System.ComponentModel.CancelEventArgs e) {
			/*
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(!IsStartingUp) {
				return;
			}
			//no need to check paths here.  If user hits cancel when starting up, it should always notify and exit.
			if(radioUseFolder.Checked 
				&& ImageStore.GetValidPathFromString(textDocPath.Text)==null 
				&& ImageStore.GetValidPathFromString(textLocalPath.Text)==null) 
			{
				MsgBox.Show(this,"Invalid A to Z path.  Closing program.");
				Application.Exit();
			}*/
		}

	

		

		

		

		
	}
}
