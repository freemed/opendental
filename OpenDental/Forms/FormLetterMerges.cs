using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetterMerges : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private bool localChanged;
		private System.Drawing.Printing.PrintDocument pd2;
		//private int pagesPrinted=0;
		private System.Windows.Forms.ListBox listCategories;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listLetters;
		private OpenDental.UI.Button butEditCats;
		private Patient PatCur;
		private LetterMerge[] ListForCat;
		private bool changed;
		private string mergePath;
#if !DISABLE_MICROSOFT_OFFICE
		//private Word.Application wrdApp;
		private Word._Document wrdDoc;
		private Object oMissing = System.Reflection.Missing.Value;
		private Object oFalse = false;
#endif
		private OpenDental.UI.Button butMerge;
		private OpenDental.UI.Button butCreateData;
		private OpenDental.UI.Button butEditTemplate;
		private System.Windows.Forms.GroupBox groupBox1;
		private OpenDental.UI.Button butPreview;


		///<summary></summary>
		public FormLetterMerges(Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLetterMerges));
			this.butCancel = new OpenDental.UI.Button();
			this.listCategories = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butMerge = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.listLetters = new System.Windows.Forms.ListBox();
			this.butEditCats = new OpenDental.UI.Button();
			this.butCreateData = new OpenDental.UI.Button();
			this.butEditTemplate = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butPreview = new OpenDental.UI.Button();
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(462,405);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// listCategories
			// 
			this.listCategories.Location = new System.Drawing.Point(15,33);
			this.listCategories.Name = "listCategories";
			this.listCategories.Size = new System.Drawing.Size(164,368);
			this.listCategories.TabIndex = 2;
			this.listCategories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCategories_MouseDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124,14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Categories";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(206,408);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butMerge
			// 
			this.butMerge.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMerge.Autosize = true;
			this.butMerge.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMerge.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMerge.CornerRadius = 4F;
			this.butMerge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butMerge.Location = new System.Drawing.Point(22,56);
			this.butMerge.Name = "butMerge";
			this.butMerge.Size = new System.Drawing.Size(79,26);
			this.butMerge.TabIndex = 17;
			this.butMerge.Text = "Print";
			this.butMerge.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(205,14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124,14);
			this.label3.TabIndex = 19;
			this.label3.Text = "Letters";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listLetters
			// 
			this.listLetters.Location = new System.Drawing.Point(206,33);
			this.listLetters.Name = "listLetters";
			this.listLetters.Size = new System.Drawing.Size(164,368);
			this.listLetters.TabIndex = 18;
			this.listLetters.DoubleClick += new System.EventHandler(this.listLetters_DoubleClick);
			// 
			// butEditCats
			// 
			this.butEditCats.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditCats.Autosize = true;
			this.butEditCats.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditCats.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditCats.CornerRadius = 4F;
			this.butEditCats.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEditCats.Location = new System.Drawing.Point(14,408);
			this.butEditCats.Name = "butEditCats";
			this.butEditCats.Size = new System.Drawing.Size(98,26);
			this.butEditCats.TabIndex = 20;
			this.butEditCats.Text = "Edit Categories";
			this.butEditCats.Click += new System.EventHandler(this.butEditCats_Click);
			// 
			// butCreateData
			// 
			this.butCreateData.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreateData.Autosize = true;
			this.butCreateData.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreateData.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreateData.CornerRadius = 4F;
			this.butCreateData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCreateData.Location = new System.Drawing.Point(22,22);
			this.butCreateData.Name = "butCreateData";
			this.butCreateData.Size = new System.Drawing.Size(79,26);
			this.butCreateData.TabIndex = 21;
			this.butCreateData.Text = "Data File";
			this.butCreateData.Click += new System.EventHandler(this.butCreateData_Click);
			// 
			// butEditTemplate
			// 
			this.butEditTemplate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEditTemplate.Autosize = true;
			this.butEditTemplate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditTemplate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditTemplate.CornerRadius = 4F;
			this.butEditTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEditTemplate.Location = new System.Drawing.Point(449,348);
			this.butEditTemplate.Name = "butEditTemplate";
			this.butEditTemplate.Size = new System.Drawing.Size(92,26);
			this.butEditTemplate.TabIndex = 22;
			this.butEditTemplate.Text = "Edit Template";
			this.butEditTemplate.Click += new System.EventHandler(this.butEditTemplate_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butPreview);
			this.groupBox1.Controls.Add(this.butMerge);
			this.groupBox1.Controls.Add(this.butCreateData);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(441,193);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(126,128);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Create";
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.CornerRadius = 4F;
			this.butPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPreview.Location = new System.Drawing.Point(22,90);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(79,26);
			this.butPreview.TabIndex = 22;
			this.butPreview.Text = "Preview";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// FormLetterMerges
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(579,446);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butEditTemplate);
			this.Controls.Add(this.butEditCats);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listLetters);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCategories);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetterMerges";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Letter Merge";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormLetterMerges_Closing);
			this.Load += new System.EventHandler(this.FormLetterMerges_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		private void FormLetterMerges_Load(object sender, System.EventArgs e) {
			mergePath=PrefB.GetString("LetterMergePath");
			FillCats();
			if(listCategories.Items.Count>0){
				listCategories.SelectedIndex=0;
			}
			FillLetters();
			if(listLetters.Items.Count>0){
				listLetters.SelectedIndex=0;
			}
		}

		private void FillCats(){
			listCategories.Items.Clear();
			for(int i=0;i<DefB.Short[(int)DefCat.LetterMergeCats].Length;i++){
				listCategories.Items.Add(DefB.Short[(int)DefCat.LetterMergeCats][i].ItemName);
			}
		}

		private void FillLetters(){
			listLetters.Items.Clear();
			if(listCategories.SelectedIndex==-1){
				ListForCat=new LetterMerge[0];
				return;
			}
			LetterMergeFields.Refresh();
			LetterMerges.Refresh();
			ListForCat=LetterMerges.GetListForCat(listCategories.SelectedIndex);
			for(int i=0;i<ListForCat.Length;i++){
				listLetters.Items.Add(ListForCat[i].Description);
			}
		}

		private void listCategories_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//selectedIndex already changed.
			FillLetters();
			if(listLetters.Items.Count>0){
				listLetters.SelectedIndex=0;
			}
		}

		private void butEditCats_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDefinitions FormD=new FormDefinitions(DefCat.LetterMergeCats);
			FormD.ShowDialog();
			FillCats();
		}

		private void listLetters_DoubleClick(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				return;
			}
			int selectedRow=listLetters.SelectedIndex;
			LetterMerge letter=ListForCat[listLetters.SelectedIndex];
			FormLetterMergeEdit FormL=new FormLetterMergeEdit(letter);
			FormL.ShowDialog();
			FillLetters();
			if(listLetters.Items.Count>selectedRow){
				listLetters.SetSelected(selectedRow,true);
			}
			changed=true;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			if(listCategories.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a category first.");
				return;
			}
			LetterMerge letter=new LetterMerge();
			letter.Category=DefB.Short[(int)DefCat.LetterMergeCats][listCategories.SelectedIndex].DefNum;
			letter.Fields=new ArrayList();
			FormLetterMergeEdit FormL=new FormLetterMergeEdit(letter);
			FormL.IsNew=true;
			FormL.ShowDialog();
			FillLetters();
			changed=true;
		}

		private bool CreateDataFile(string fileName,LetterMerge letter){
			string command="SELECT ";
			for(int i=0;i<letter.Fields.Count;i++){
				if(i>0){
					command+=",";
				}
				if(((string)letter.Fields[i]).Length>9
					&& ((string)letter.Fields[i]).Substring(0,9)=="referral.")
				{
					command+="referral."+((string)letter.Fields[i]).Substring(9);
				}
				else{
					command+="patient."+(string)letter.Fields[i];
				}
			}
			command+=" FROM patient "
				+"LEFT JOIN refattach ON patient.PatNum=refattach.PatNum AND refattach.IsFrom=1 "
				+"LEFT JOIN referral ON refattach.ReferralNum=referral.ReferralNum "
				+"WHERE patient.PatNum="+POut.PInt(PatCur.PatNum)
				+" GROUP BY patient.PatNum "
				+"ORDER BY refattach.ItemOrder";
 			DataTable table=General.GetTable(command);
			table=FormQuery.MakeReadable(table);
			try{
			  using(StreamWriter sw=new StreamWriter(fileName,false)){
					string line="";  
					for(int i=0;i<letter.Fields.Count;i++){
						if(((string)letter.Fields[i]).Length>9
							&& ((string)letter.Fields[i]).Substring(0,9)=="referral.")
						{
							line+="Ref"+((string)letter.Fields[i]).Substring(9);
						}
						else{
							line+=(string)letter.Fields[i];
						}
						if(i<letter.Fields.Count-1){
							line+="\t";
						}
					}
					sw.WriteLine(line);
					string cell;
					for(int i=0;i<table.Rows.Count;i++){
						line="";
						for(int j=0;j<table.Columns.Count;j++){
							cell=table.Rows[i][j].ToString();
							cell=cell.Replace("\r","");
							cell=cell.Replace("\n","");
							cell=cell.Replace("\t","");
							cell=cell.Replace("\"","");
							line+=cell;
							if(j<table.Columns.Count-1){
								line+="\t";
							}
						}
						sw.WriteLine(line);
					}
				}
      }
      catch{
        MsgBox.Show(this,"File in use by another program.  Close and try again.");
				return false;
			}
			return true;
		}

		private void butCreateData_Click(object sender, System.EventArgs e) {
			if(listLetters.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a letter first.");
				return;
			}
			LetterMerge letterCur=ListForCat[listLetters.SelectedIndex];
			string dataFile=PrefB.GetString("LetterMergePath")+letterCur.DataFileName;
			if(!Directory.Exists(PrefB.GetString("LetterMergePath"))){
				MsgBox.Show(this,"Letter merge path not valid.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			if(!CreateDataFile(dataFile,letterCur)){
				Cursor=Cursors.Default;
				return;
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"done");
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
#if !DISABLE_MICROSOFT_OFFICE
			if(listLetters.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a letter first.");
				return;
			}
			LetterMerge letterCur=ListForCat[listLetters.SelectedIndex];
			string templateFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),letterCur.TemplateName);
			string dataFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),letterCur.DataFileName);
			if(!File.Exists(templateFile)){
				MsgBox.Show(this,"Template file does not exist.");
				return;
			}
			PrintDocument pd=new PrintDocument();
			if(!Printers.SetPrinter(pd,PrintSituation.Default)){
				return;
			}
			if(!CreateDataFile(dataFile,letterCur)){
				return;
			}
			
			Word.MailMerge wrdMailMerge;
			//Create an instance of Word.
			Word.Application WrdApp=LetterMerges.WordApp;
			//Open a document.
			Object oName=templateFile;
			wrdDoc=WrdApp.Documents.Open(ref oName,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			wrdDoc.Select();
			wrdMailMerge=wrdDoc.MailMerge;
			//Attach the data file.
			wrdDoc.MailMerge.OpenDataSource(dataFile,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			wrdMailMerge.Destination = Word.WdMailMergeDestination.wdSendToPrinter;
			WrdApp.ActivePrinter=pd.PrinterSettings.PrinterName;
			wrdMailMerge.Execute(ref oFalse);
			//Close the original form document since just one record.
			wrdDoc.Saved=true;
			wrdDoc.Close(ref oFalse,ref oMissing,ref oMissing);
			//At this point, Word remains open with no documents.
			WrdApp.WindowState=Word.WdWindowState.wdWindowStateMinimize;
			wrdMailMerge=null;
			wrdDoc=null;
			Commlog CommlogCur=new Commlog();
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
			CommlogCur.Mode_=CommItemMode.Mail;
			CommlogCur.SentOrReceived=CommSentOrReceived.Sent;
			CommlogCur.PatNum=PatCur.PatNum;
			CommlogCur.Note="Letter sent: "+letterCur.Description+". ";
			Commlogs.Insert(CommlogCur);
#else
			MessageBox.Show(this, "This version of Open Dental does not support Microsoft Word.");
#endif
			DialogResult=DialogResult.OK;
		}

		private void butPreview_Click(object sender, System.EventArgs e) {
#if !DISABLE_MICROSOFT_OFFICE
			if(listLetters.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a letter first.");
				return;
			}
			LetterMerge letterCur=ListForCat[listLetters.SelectedIndex];
			string templateFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),letterCur.TemplateName);
			string dataFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),letterCur.DataFileName);
			if(!File.Exists(templateFile)){
				MsgBox.Show(this,"Template file does not exist.");
				return;
			}
			if(!CreateDataFile(dataFile,letterCur)){
				return;
			}
			Word.MailMerge wrdMailMerge;
			//Create an instance of Word.
			Word.Application WrdApp;
			try{
				WrdApp=LetterMerges.WordApp;
			}
			catch{
				MsgBox.Show(this,"Error. Is Word installed?");
				return;
			}
			//Open a document.
			Object oName=templateFile;
			wrdDoc=WrdApp.Documents.Open(ref oName,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			wrdDoc.Select();
			wrdMailMerge=wrdDoc.MailMerge;
			//Attach the data file.
			wrdDoc.MailMerge.OpenDataSource(dataFile,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			wrdMailMerge.Destination = Word.WdMailMergeDestination.wdSendToNewDocument;
			wrdMailMerge.Execute(ref oFalse);
			//Close the original form document since just one record.
			wrdDoc.Saved=true;
			wrdDoc.Close(ref oFalse,ref oMissing,ref oMissing);
			//At this point, Word remains open with just one new document.
			WrdApp.Activate();
			if(WrdApp.WindowState==Word.WdWindowState.wdWindowStateMinimize){
				WrdApp.WindowState=Word.WdWindowState.wdWindowStateMaximize;
			}
			wrdMailMerge=null;
			wrdDoc=null;
			Commlog CommlogCur=new Commlog();
			CommlogCur.CommDateTime=DateTime.Now;
			CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
			CommlogCur.Mode_=CommItemMode.Mail;
			CommlogCur.SentOrReceived=CommSentOrReceived.Sent;
			CommlogCur.PatNum=PatCur.PatNum;
			CommlogCur.Note="Letter sent: "+letterCur.Description+". ";
			Commlogs.Insert(CommlogCur);
#else
			MessageBox.Show(this, "This version of Open Dental does not support Microsoft Word.");
#endif
			//this window now closes regardless of whether the user saved the comm item.
			DialogResult=DialogResult.OK;
		}

		private void butEditTemplate_Click(object sender, System.EventArgs e) {
#if !DISABLE_MICROSOFT_OFFICE
			if(listLetters.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a letter first.");
				return;
			}
			LetterMerge letterCur=ListForCat[listLetters.SelectedIndex];
			string templateFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),letterCur.TemplateName);
			string dataFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),letterCur.DataFileName);
			if(!File.Exists(templateFile)){
				MessageBox.Show(Lan.g(this,"Template file does not exist:")+"  "+templateFile);
				return;
			}
			if(!CreateDataFile(dataFile,letterCur)){
				return;
			}
			//Create an instance of Word.
			Word.Application WrdApp=LetterMerges.WordApp;
			//Open a document.
			Object oName=templateFile;
			wrdDoc=WrdApp.Documents.Open(ref oName,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			wrdDoc.Select();
			//Attach the data file.
			wrdDoc.MailMerge.OpenDataSource(dataFile,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			//At this point, Word remains open with just one new document.
			if(WrdApp.WindowState==Word.WdWindowState.wdWindowStateMinimize){
				WrdApp.WindowState=Word.WdWindowState.wdWindowStateMaximize;
			}
			wrdDoc=null;
#else
			MessageBox.Show(this, "This version of Open Dental does not support Microsoft Word.");
#endif
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormLetterMerges_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.LetterMerge);
			}
		}

		

		

		

		

		

		

		

		


		

		

		

		

		

		

		

		


	}
}





















