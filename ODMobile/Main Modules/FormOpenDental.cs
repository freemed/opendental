using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public partial class FormOpenDental:Form {
		private static int CurPatNum;
		private ContrAppt ContrAppt2;
		private ContrFamily ContrFamily2;

		public FormOpenDental() {
			InitializeComponent();
			ContrAppt2=new ContrAppt();
			ContrAppt2.Location=new Point(0,0);
			ContrAppt2.Dock=DockStyle.Fill;
			ContrAppt2.Size=this.ClientRectangle.Size;
			ContrAppt2.Visible=false;
			ContrAppt2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			this.Controls.Add(ContrAppt2);
			ContrFamily2=new ContrFamily();
			ContrFamily2.Location=new Point(0,0);
			ContrFamily2.Dock=DockStyle.Fill;
			ContrFamily2.Size=this.ClientRectangle.Size;
			ContrFamily2.Visible=false;
			this.Controls.Add(ContrFamily2);
		}

		private void Form1_KeyDown(object sender,KeyEventArgs e) {
			if((e.KeyCode == System.Windows.Forms.Keys.Up)) {
				// Up
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Down)) {
				// Down
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Left)) {
				// Left
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Right)) {
				// Right
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Enter)) {
				// Enter
			}

		}

		private void Form1_Load(object sender,EventArgs e) {
			allNeutral();
			string strAppDir=Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			if(File.Exists(Path.Combine(strAppDir,"OpenDentalConfig.xml"))){
				FormChooseDatabase formChooseDb=new FormChooseDatabase();
				formChooseDb.ShowDialog();
				if(formChooseDb.CloseApplication){
					Application.Exit();
					return;
				}
			}
			else if(!FormChooseDatabase.TryToConnect("OpenDental")){
				if(!MsgBox.Show("Database does not exist and will now be created.",true)){
					MessageBox.Show("Application will now exit.");
					Application.Exit();
				}
				try{
					ClassConvertDatabase.CreateNewDatabase("OpenDental");
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message+".  Application will now exit.");
					Application.Exit();
				}
				if(!FormChooseDatabase.TryToConnect("OpenDental")){
					MessageBox.Show("Could not connect to database.  Application will now exit.");
					Application.Exit();
				}
				MessageBox.Show("Database has been created.  Please do a full sync from the workstation, then reopen the program.");
				Application.Exit();
			}
			if(!PrefsStartup()){
				Application.Exit();
			}
			menuItemModule.Text="Appts";
			ContrAppt2.Visible=true;
			//look for new files
			if(!LoadAllNewFiles()){
				Application.Exit();
			}
			ContrAppt2.ModuleSelected(0);
		}

		///<summary>Returns false if it can't complete a conversion, find datapath, or validate registration key.</summary>
		private bool PrefsStartup(){
			Prefs.Refresh();
			//CacheL.Refresh(InvalidType.Prefs);
			if(!ClassConvertDatabase.ConvertDB()){
				return false;
			}
			return true;
		}

		private bool LoadAllNewFiles(){
			string dataPath=PrefC.GetString("ImportPath");
			//#if DEBUG
			//	dataPath=@"\Storage Card\Business\Open Dental";
			//#else
			//	dataPath=@"\My Documents\Business\Open Dental";
			//#endif
			if(!Directory.Exists(dataPath)){
				MessageBox.Show("Please enter a valid path.");
				FormSetup FormS=new FormSetup();
				FormS.ShowDialog();
				dataPath=PrefC.GetString("ImportPath");//if user changed path, it's guaranteed to be valid
				if(!Directory.Exists(dataPath)){//user explicity wants to close program
					return false;
				}
			}
			string[] fileArray=Directory.GetFiles(dataPath);
			List<string> fileList=new List<string>();
			string fileOnly;
			for(int i=0;i<fileArray.Length;i++){
				fileOnly=Path.GetFileName(fileArray[i]);
				if(!fileOnly.StartsWith("in")){
					continue;
				}
				if(!fileOnly.EndsWith(".xml")){
					continue;
				}
				fileList.Add(fileArray[i]);
			}
			if(fileList.Count==0){
				return true;
			}
			int totalFileSizes=0;
			FileInfo fileInfo;
			for(int i=0;i<fileList.Count;i++){
				fileInfo=new FileInfo(fileList[i]);
				totalFileSizes+=(int)fileInfo.Length;
			}
			//I've timed it as taking about 4.5min for a 3.03 MB xml file.
			//So a rough time ratio is .0000014 minutes per byte.
			string timeSnippet="";
			double estimatedMinutes=(double)totalFileSizes * 0.0000014;
			if(estimatedMinutes>1){
				timeSnippet="  Estimated time to complete: "+estimatedMinutes.ToString("f1")+" minutes.";
			}
			else{
				double estimatedSeconds=estimatedMinutes*60;
				timeSnippet="  Estimated time to complete: "+estimatedSeconds.ToString("f0")+" seconds.";
			}
			if(MessageBox.Show("'in' file(s) detected.  Import will now begin."+timeSnippet,"",MessageBoxButtons.OKCancel,MessageBoxIcon.None,MessageBoxDefaultButton.Button1)
				!=DialogResult.OK)
			{
				return true;
			}
			Cursor.Current=Cursors.WaitCursor;
			fileList.Sort(CompareFileNames);
			try{
				for(int i=0;i<fileList.Count;i++){
					XMLParser.ImportXML(fileList[i]);
					File.Delete(fileList[i]);
				}
			}
			catch(Exception ex){
				Cursor.Current=Cursors.Default;
				MessageBox.Show(ex.Message);
				return false;
			}
			Cursor.Current=Cursors.Default;
			return true;
		}

		///<summary></summary>
		private int CompareFileNames(string name1,string name2){
			string file1only=Path.GetFileName(name1);
			string file2only=Path.GetFileName(name2);
			int int1=PIn.PInt(file1only.Substring(2,file1only.Length-6));
			int int2=PIn.PInt(file2only.Substring(2,file2only.Length-6));
			return int1.CompareTo(int2);
		}

		///<summary>Happens when any of the modules changes the current patient or when this main form changes the patient.  The calling module should refresh itself.  The current patNum is stored here in the parent form so that when switching modules, the parent form knows which patient to call up for that module.</summary>
		private void Contr_PatientSelected(object sender,PatientSelectedEventArgs e) {
			CurPatNum=e.PatNum;
			FillPatientButton(e.PatName);
		}

		///<Summary>Sets main form text.</Summary>
		private void FillPatientButton(string patName) {
			Text=patName;
		}

		///<summary></summary>
		private void SetModuleSelected(int idx){
			switch(idx){
				case 0:
					ContrAppt2.Visible=true;
					menuItemModule.Text="Appts";
					ContrAppt2.ModuleSelected(CurPatNum);
					break;
				case 1:
					ContrFamily2.Visible=true;
					menuItemModule.Text="Family";
					ContrFamily2.ModuleSelected(CurPatNum);
					break;
			}
		}

		private void allNeutral(){
			ContrAppt2.Visible=false;
			ContrFamily2.Visible=false;
		}

		///<Summary>This also passes CurPatNum down to the currently selected module (except the Manage module).</Summary>
		private void RefreshCurrentModule(){
			if(ContrAppt2.Visible){
				ContrAppt2.ModuleSelected(CurPatNum);
			}
			if(ContrFamily2.Visible){
				ContrFamily2.ModuleSelected(CurPatNum);
			}
		}

		private void menuItemAppt_Click(object sender,EventArgs e) {
			allNeutral();
			SetModuleSelected(0);
		}

		private void menuItemFamily_Click(object sender,EventArgs e) {
			allNeutral();
			SetModuleSelected(1);
		}

		private void menuItemSelectPat_Click(object sender,EventArgs e) {
			FormPatientSelect FormP=new FormPatientSelect();
			FormP.ShowDialog();
			//MessageBox.Show(FormP.DialogResult.ToString());
			if(FormP.SelectedPatNum==0){
				return;
			}
			CurPatNum=FormP.SelectedPatNum;
			Patient pat=Patients.GetPat(CurPatNum);
			RefreshCurrentModule();
			FillPatientButton(pat.GetNameLF());
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormSetup FormS=new FormSetup();
			FormS.ShowDialog();
		}

		private void menuItemModule_Popup(object sender,EventArgs e) {
			menuItemAppt.Checked=false;
			menuItemFamily.Checked=false;
			if(ContrAppt2.Visible){
				menuItemAppt.Checked=true;
			}
			if(ContrFamily2.Visible){
				menuItemFamily.Checked=true;
			}
		}

		

		




	}
}