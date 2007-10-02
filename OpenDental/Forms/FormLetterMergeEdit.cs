using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLetterMergeEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textDescription;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textTemplateName;
		private System.Windows.Forms.TextBox textDataFileName;
		private System.Windows.Forms.ComboBox comboCategory;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textPath;
		private OpenDental.UI.Button butEditPaths;
		private LetterMerge LetterMergeCur;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label labelPatient;
		private string mergePath;
		private System.Windows.Forms.ListBox listPatSelect;
		private OpenDental.UI.Button butBrowse;
		private System.Windows.Forms.OpenFileDialog openFileDlg;
		private System.Windows.Forms.Label labelReferredFrom;
		private System.Windows.Forms.ListBox listReferral;
		private OpenDental.UI.Button butNew;
		private System.Windows.Forms.TextBox textBox1;
		//private ArrayList ALpatSelect;

		///<summary></summary>
		public FormLetterMergeEdit(LetterMerge letterMergeCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			LetterMergeCur=letterMergeCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLetterMergeEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.textTemplateName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textDataFileName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboCategory = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textPath = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butEditPaths = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.labelPatient = new System.Windows.Forms.Label();
			this.listPatSelect = new System.Windows.Forms.ListBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butBrowse = new OpenDental.UI.Button();
			this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.labelReferredFrom = new System.Windows.Forms.Label();
			this.listReferral = new System.Windows.Forms.ListBox();
			this.butNew = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(799,638);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
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
			this.butOK.Location = new System.Drawing.Point(799,597);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20,11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(250,14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(272,7);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(221,20);
			this.textDescription.TabIndex = 0;
			// 
			// textTemplateName
			// 
			this.textTemplateName.Location = new System.Drawing.Point(272,54);
			this.textTemplateName.Name = "textTemplateName";
			this.textTemplateName.Size = new System.Drawing.Size(346,20);
			this.textTemplateName.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20,58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(250,14);
			this.label1.TabIndex = 5;
			this.label1.Text = "Template File Name (eg MyTemplate.doc)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDataFileName
			// 
			this.textDataFileName.Location = new System.Drawing.Point(272,77);
			this.textDataFileName.Name = "textDataFileName";
			this.textDataFileName.Size = new System.Drawing.Size(346,20);
			this.textDataFileName.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20,82);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(250,14);
			this.label3.TabIndex = 7;
			this.label3.Text = "Datafile Name (eg MyTemplate.txt)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboCategory
			// 
			this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCategory.Location = new System.Drawing.Point(272,100);
			this.comboCategory.MaxDropDownItems = 30;
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.Size = new System.Drawing.Size(222,21);
			this.comboCategory.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(20,104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(250,14);
			this.label4.TabIndex = 9;
			this.label4.Text = "Category";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPath
			// 
			this.textPath.Location = new System.Drawing.Point(272,30);
			this.textPath.Name = "textPath";
			this.textPath.ReadOnly = true;
			this.textPath.Size = new System.Drawing.Size(346,20);
			this.textPath.TabIndex = 23;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(21,34);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(250,14);
			this.label5.TabIndex = 24;
			this.label5.Text = "Letter Merge Path";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butEditPaths
			// 
			this.butEditPaths.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditPaths.Autosize = true;
			this.butEditPaths.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditPaths.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditPaths.CornerRadius = 4F;
			this.butEditPaths.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEditPaths.Location = new System.Drawing.Point(622,27);
			this.butEditPaths.Name = "butEditPaths";
			this.butEditPaths.Size = new System.Drawing.Size(87,25);
			this.butEditPaths.TabIndex = 25;
			this.butEditPaths.Text = "Edit Paths";
			this.butEditPaths.Click += new System.EventHandler(this.butEditPaths_Click);
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
			this.butDelete.Location = new System.Drawing.Point(12,638);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(87,26);
			this.butDelete.TabIndex = 26;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(12,121);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(170,14);
			this.labelPatient.TabIndex = 28;
			this.labelPatient.Text = "Patient Fields";
			this.labelPatient.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPatSelect
			// 
			this.listPatSelect.Location = new System.Drawing.Point(12,139);
			this.listPatSelect.Name = "listPatSelect";
			this.listPatSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPatSelect.Size = new System.Drawing.Size(170,472);
			this.listPatSelect.TabIndex = 27;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(190,137);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(622,23);
			this.textBox1.TabIndex = 29;
			this.textBox1.Text = "Hint: Use the Ctrl key when clicking.  Also you can simply drag the pointer acros" +
    "s muliple rows to select quickly.";
			// 
			// butBrowse
			// 
			this.butBrowse.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowse.Autosize = true;
			this.butBrowse.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowse.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowse.CornerRadius = 4F;
			this.butBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBrowse.Location = new System.Drawing.Point(622,53);
			this.butBrowse.Name = "butBrowse";
			this.butBrowse.Size = new System.Drawing.Size(87,25);
			this.butBrowse.TabIndex = 30;
			this.butBrowse.Text = "Browse";
			this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
			// 
			// labelReferredFrom
			// 
			this.labelReferredFrom.Location = new System.Drawing.Point(206,161);
			this.labelReferredFrom.Name = "labelReferredFrom";
			this.labelReferredFrom.Size = new System.Drawing.Size(170,14);
			this.labelReferredFrom.TabIndex = 32;
			this.labelReferredFrom.Text = "Referred From";
			this.labelReferredFrom.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listReferral
			// 
			this.listReferral.Location = new System.Drawing.Point(206,178);
			this.listReferral.Name = "listReferral";
			this.listReferral.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listReferral.Size = new System.Drawing.Size(170,433);
			this.listReferral.TabIndex = 31;
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNew.Autosize = true;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNew.CornerRadius = 4F;
			this.butNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butNew.Location = new System.Drawing.Point(714,53);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(68,25);
			this.butNew.TabIndex = 33;
			this.butNew.Text = "New";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// FormLetterMergeEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(894,672);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.labelReferredFrom);
			this.Controls.Add(this.listReferral);
			this.Controls.Add(this.butBrowse);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.labelPatient);
			this.Controls.Add(this.listPatSelect);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butEditPaths);
			this.Controls.Add(this.textPath);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboCategory);
			this.Controls.Add(this.textDataFileName);
			this.Controls.Add(this.textTemplateName);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLetterMergeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Letter Merge";
			this.Load += new System.EventHandler(this.FormLetterMergeEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormLetterMergeEdit_Load(object sender, System.EventArgs e) {
			textDescription.Text=LetterMergeCur.Description;
			mergePath=PrefB.GetString("LetterMergePath");
			textPath.Text=mergePath;
			textTemplateName.Text=LetterMergeCur.TemplateName;
			textDataFileName.Text=LetterMergeCur.DataFileName;
			for(int i=0;i<DefB.Short[(int)DefCat.LetterMergeCats].Length;i++){
				comboCategory.Items.Add(DefB.Short[(int)DefCat.LetterMergeCats][i].ItemName);
				if(LetterMergeCur.Category==DefB.Short[(int)DefCat.LetterMergeCats][i].DefNum){
					comboCategory.SelectedIndex=i;
				}
			}
			FillPatSelect();
			FillListReferral();
		}

		private void FillPatSelect(){
			listPatSelect.Items.Clear();
			listPatSelect.Items.Add("PatNum"); 
			listPatSelect.Items.Add("LName");
			listPatSelect.Items.Add("FName");
			listPatSelect.Items.Add("MiddleI"); 
      listPatSelect.Items.Add("Preferred");
      listPatSelect.Items.Add("Salutation"); 
      listPatSelect.Items.Add("Address"); 
      listPatSelect.Items.Add("Address2");
      listPatSelect.Items.Add("City"); 
      listPatSelect.Items.Add("State");   
      listPatSelect.Items.Add("Zip");
      listPatSelect.Items.Add("HmPhone");
      listPatSelect.Items.Add("WkPhone"); 
      listPatSelect.Items.Add("WirelessPhone"); 
      listPatSelect.Items.Add("Birthdate");
      listPatSelect.Items.Add("Email");
      listPatSelect.Items.Add("SSN");
      listPatSelect.Items.Add("Gender");
      listPatSelect.Items.Add("PatStatus");
      listPatSelect.Items.Add("Position");  
      listPatSelect.Items.Add("CreditType");
      listPatSelect.Items.Add("BillingType"); 
      listPatSelect.Items.Add("ChartNumber");   
      listPatSelect.Items.Add("PriProv"); 
      listPatSelect.Items.Add("SecProv");
      listPatSelect.Items.Add("FeeSched"); 
      listPatSelect.Items.Add("ApptModNote");
      listPatSelect.Items.Add("AddrNote"); 
      listPatSelect.Items.Add("EstBalance"); 
      listPatSelect.Items.Add("FamFinUrgNote"); 
      listPatSelect.Items.Add("Guarantor");   
      listPatSelect.Items.Add("ImageFolder");
      listPatSelect.Items.Add("MedUrgNote"); 
      listPatSelect.Items.Add("NextAptNum"); 
      //listPatSelect.Items.Add("PriPlanNum");//Primary Carrier?
      //listPatSelect.Items.Add("PriRelationship");// ?
			//listPatSelect.Items.Add("SecPlanNum");//Secondary Carrier? 
      //listPatSelect.Items.Add("SecRelationship");// ?
			//listPatSelect.Items.Add("RecallInterval")); 
      //listPatSelect.Items.Add("RecallStatus");  
      listPatSelect.Items.Add("SchoolName"); 
      listPatSelect.Items.Add("StudentStatus");
			listPatSelect.Items.Add("MedicaidID");
			listPatSelect.Items.Add("Bal_0_30");
			listPatSelect.Items.Add("Bal_31_60");
			listPatSelect.Items.Add("Bal_61_90");
			listPatSelect.Items.Add("BalOver90");
			listPatSelect.Items.Add("InsEst");
			listPatSelect.Items.Add("PrimaryTeeth");
			listPatSelect.Items.Add("BalTotal");
			listPatSelect.Items.Add("EmployerNum");
			//EmploymentNote
			listPatSelect.Items.Add("Race");
			listPatSelect.Items.Add("County");
			listPatSelect.Items.Add("GradeSchool");
			listPatSelect.Items.Add("GradeLevel");
			listPatSelect.Items.Add("Urgency");
			listPatSelect.Items.Add("DateFirstVisit");
			//listPatSelect.Items.Add("PriPending");
			//listPatSelect.Items.Add("SecPending");
			for(int i=0;i<LetterMergeCur.Fields.Count;i++){
				for(int j=0;j<listPatSelect.Items.Count;j++){
					if(listPatSelect.Items[j].ToString()==(string)LetterMergeCur.Fields[i]){
						listPatSelect.SetSelected(j,true);
					}
				}
			}
 		}

		private void FillListReferral(){
      listReferral.Items.Add("LName");
      listReferral.Items.Add("FName");
      listReferral.Items.Add("MName");       
      listReferral.Items.Add("Title"); 
      listReferral.Items.Add("Address"); 
      listReferral.Items.Add("Address2");
      listReferral.Items.Add("City");
      listReferral.Items.Add("ST");
      listReferral.Items.Add("Zip");
      listReferral.Items.Add("Telephone");
      listReferral.Items.Add("Phone2"); 
      listReferral.Items.Add("Email");
      listReferral.Items.Add("IsHidden"); 
      listReferral.Items.Add("NotPerson");  
      listReferral.Items.Add("PatNum"); 
			listReferral.Items.Add("ReferralNum");
      listReferral.Items.Add("Specialty"); 
      listReferral.Items.Add("SSN");
      listReferral.Items.Add("UsingTIN"); 
      listReferral.Items.Add("Note");
			for(int i=0;i<LetterMergeCur.Fields.Count;i++){
				for(int j=0;j<listReferral.Items.Count;j++){
					if("referral."+listReferral.Items[j].ToString()==(string)LetterMergeCur.Fields[i]){
						listReferral.SetSelected(j,true);
					}
				}
			}
    }


		private void butEditPaths_Click(object sender, System.EventArgs e) {
			FormPath FormP=new FormPath();
			FormP.ShowDialog();
			mergePath=PrefB.GetString("LetterMergePath");
			textPath.Text=mergePath;
		}

		private void butBrowse_Click(object sender, System.EventArgs e) {
			if(!Directory.Exists(PrefB.GetString("LetterMergePath"))){
				MsgBox.Show(this,"Letter merge path invalid");
				return;
			}
			openFileDlg.InitialDirectory=PrefB.GetString("LetterMergePath");
			if(openFileDlg.ShowDialog() !=DialogResult.OK){
				return;
			}
			textTemplateName.Text=Path.GetFileName(openFileDlg.FileName);
		}

		private void butNew_Click(object sender, System.EventArgs e) {
#if !DISABLE_MICROSOFT_OFFICE
			if(!Directory.Exists(PrefB.GetString("LetterMergePath"))){
				MsgBox.Show(this,"Letter merge path invalid");
				return;
			}
			if(textTemplateName.Text==""){
				MsgBox.Show(this,"Please enter a template file name first.");
				return;
			}
			string templateFile=ODFileUtils.CombinePaths(PrefB.GetString("LetterMergePath"),textTemplateName.Text);
			if(File.Exists(templateFile)){
				MsgBox.Show(this,"A file with that name already exists.  Choose a different name, or close this window to edit the template.");
				return;
			}
			Object oMissing=System.Reflection.Missing.Value;
			Object oFalse=false;
			//Create an instance of Word.
			Word.Application WrdApp;
			try{
				WrdApp=LetterMerges.WordApp;
			}
			catch{
				MsgBox.Show(this,"Error.  Is MS Word installed?");
				return;
			}
			//Create a new document.
			Object oName=templateFile;
			Word._Document wrdDoc;
			wrdDoc=WrdApp.Documents.Add(ref oMissing,ref oMissing,ref oMissing,
				ref oMissing);
			wrdDoc.SaveAs(ref oName,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing,
				ref oMissing,ref oMissing,ref oMissing,ref oMissing,ref oMissing);
			wrdDoc.Saved=true;
			wrdDoc.Close(ref oFalse,ref oMissing,ref oMissing);
			WrdApp.WindowState=Word.WdWindowState.wdWindowStateMinimize;
			wrdDoc=null;
			MsgBox.Show(this,"Done. You can edit the new template after closing this window.");
#else
			MessageBox.Show(this, "This version of Open Dental does not support Microsoft Word.");
#endif
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			LetterMerges.Delete(LetterMergeCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MsgBox.Show(this,"Please enter a description");
				return;
			}
			if(this.textDataFileName.Text==""
				|| this.textTemplateName.Text=="")
			{
				MsgBox.Show(this,"Filenames cannot be left blank.");
				return;
			}
			if(comboCategory.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a category");
				return;
			}
			if(listPatSelect.SelectedIndices.Count==0
				&& listReferral.SelectedIndices.Count==0)
			{
				MsgBox.Show(this,"Please select at least one field.");
				return;
			}
			Cursor.Current=Cursors.WaitCursor;
			LetterMergeCur.Description=textDescription.Text;
			LetterMergeCur.TemplateName=textTemplateName.Text;
			LetterMergeCur.DataFileName=textDataFileName.Text;
			LetterMergeCur.Category
				=DefB.Short[(int)DefCat.LetterMergeCats][comboCategory.SelectedIndex].DefNum;
			if(IsNew){
				LetterMerges.Insert(LetterMergeCur);
			}
			else{
				LetterMerges.Update(LetterMergeCur);
			}
			LetterMergeFields.DeleteForLetter(LetterMergeCur.LetterMergeNum);
			LetterMergeField field;
			for(int i=0;i<listPatSelect.SelectedItems.Count;i++){
				field=new LetterMergeField();
				field.LetterMergeNum=LetterMergeCur.LetterMergeNum;
				field.FieldName=(string)listPatSelect.SelectedItems[i];
					//(string)listPatSelect.Items[listPatSelect.SelectedIndices[i]];
				LetterMergeFields.Insert(field);
			}
			for(int i=0;i<listReferral.SelectedItems.Count;i++){
				field=new LetterMergeField();
				field.LetterMergeNum=LetterMergeCur.LetterMergeNum;
				field.FieldName="referral."+(string)listReferral.SelectedItems[i];
				LetterMergeFields.Insert(field);
			}
			Cursor.Current=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}





















