/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{

	///<summary></summary>
	public class ContrFamily : System.Windows.Forms.UserControl{
		private System.Windows.Forms.ImageList imageListToolBar;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.ODToolBar ToolBarMain;
		///<summary>All recalls for this entire family.</summary>
		private List<Recall> RecallList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.PictureBox picturePat;
		private List <InsPlan> PlanList;
		private OpenDental.UI.ODGrid gridIns;
		private List <PatPlan> PatPlanList;
		private ODGrid gridPat;
		private ContextMenu menuInsurance;
		private MenuItem menuPlansForFam;
		private List <Benefit> BenefitList;
		private ODGrid gridFamily;
		private ODGrid gridRecall;
		private PatField[] PatFieldList;
		private bool InitializedOnStartup;

		///<summary></summary>
		public ContrFamily(){
			Logger.openlog.Log("Initializing family module...",Logger.Severity.INFO);
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
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

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrFamily));
			this.imageListToolBar = new System.Windows.Forms.ImageList(this.components);
			this.menuInsurance = new System.Windows.Forms.ContextMenu();
			this.menuPlansForFam = new System.Windows.Forms.MenuItem();
			this.gridRecall = new OpenDental.UI.ODGrid();
			this.gridFamily = new OpenDental.UI.ODGrid();
			this.gridPat = new OpenDental.UI.ODGrid();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.picturePat = new OpenDental.UI.PictureBox();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.SuspendLayout();
			// 
			// imageListToolBar
			// 
			this.imageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolBar.ImageStream")));
			this.imageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListToolBar.Images.SetKeyName(0,"");
			this.imageListToolBar.Images.SetKeyName(1,"");
			this.imageListToolBar.Images.SetKeyName(2,"");
			this.imageListToolBar.Images.SetKeyName(3,"");
			this.imageListToolBar.Images.SetKeyName(4,"");
			this.imageListToolBar.Images.SetKeyName(5,"");
			this.imageListToolBar.Images.SetKeyName(6,"Umbrella.gif");
			// 
			// menuInsurance
			// 
			this.menuInsurance.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuPlansForFam});
			// 
			// menuPlansForFam
			// 
			this.menuPlansForFam.Index = 0;
			this.menuPlansForFam.Text = "Plans for Family";
			this.menuPlansForFam.Click += new System.EventHandler(this.menuPlansForFam_Click);
			// 
			// gridRecall
			// 
			this.gridRecall.HScrollVisible = false;
			this.gridRecall.Location = new System.Drawing.Point(585,27);
			this.gridRecall.Name = "gridRecall";
			this.gridRecall.ScrollValue = 0;
			this.gridRecall.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridRecall.Size = new System.Drawing.Size(525,100);
			this.gridRecall.TabIndex = 32;
			this.gridRecall.Title = "Recall";
			this.gridRecall.TranslationName = "TableRecall";
			this.gridRecall.DoubleClick += new System.EventHandler(this.gridRecall_DoubleClick);
			this.gridRecall.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridRecall_CellDoubleClick);
			// 
			// gridFamily
			// 
			this.gridFamily.HScrollVisible = false;
			this.gridFamily.Location = new System.Drawing.Point(103,27);
			this.gridFamily.Name = "gridFamily";
			this.gridFamily.ScrollValue = 0;
			this.gridFamily.SelectedRowColor = System.Drawing.Color.DarkSalmon;
			this.gridFamily.Size = new System.Drawing.Size(480,100);
			this.gridFamily.TabIndex = 31;
			this.gridFamily.Title = "Family Members";
			this.gridFamily.TranslationName = "TablePatient";
			this.gridFamily.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFamily_CellClick);
			// 
			// gridPat
			// 
			this.gridPat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridPat.HScrollVisible = false;
			this.gridPat.Location = new System.Drawing.Point(0,129);
			this.gridPat.Name = "gridPat";
			this.gridPat.ScrollValue = 0;
			this.gridPat.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridPat.Size = new System.Drawing.Size(252,579);
			this.gridPat.TabIndex = 30;
			this.gridPat.Title = "Patient Information";
			this.gridPat.TranslationName = "TablePatient";
			this.gridPat.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPat_CellDoubleClick);
			// 
			// gridIns
			// 
			this.gridIns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridIns.HScrollVisible = true;
			this.gridIns.Location = new System.Drawing.Point(254,129);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridIns.Size = new System.Drawing.Size(685,579);
			this.gridIns.TabIndex = 29;
			this.gridIns.Title = "Insurance Plans";
			this.gridIns.TranslationName = "TableCoverage";
			this.gridIns.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellDoubleClick);
			// 
			// picturePat
			// 
			this.picturePat.Location = new System.Drawing.Point(1,27);
			this.picturePat.Name = "picturePat";
			this.picturePat.Size = new System.Drawing.Size(100,100);
			this.picturePat.TabIndex = 28;
			this.picturePat.Text = "picturePat";
			this.picturePat.TextNullImage = "Patient Picture Unavailable";
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListToolBar;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,25);
			this.ToolBarMain.TabIndex = 19;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ContrFamily
			// 
			this.Controls.Add(this.gridRecall);
			this.Controls.Add(this.gridFamily);
			this.Controls.Add(this.gridPat);
			this.Controls.Add(this.gridIns);
			this.Controls.Add(this.picturePat);
			this.Controls.Add(this.ToolBarMain);
			this.Name = "ContrFamily";
			this.Size = new System.Drawing.Size(939,708);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrFamily_Layout);
			this.Resize += new System.EventHandler(this.ContrFamily_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void ModuleSelected(long patNum) {
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PlanList=null;
		}

		private void RefreshModuleData(long patNum) {
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				PatPlanList=new List <PatPlan> (); 
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.RefreshForFam(FamCur);
			PatPlanList=PatPlans.Refresh(patNum);
			BenefitList=Benefits.Refresh(PatPlanList);
			RecallList=Recalls.GetList(MiscUtils.ArrayToList<Patient>(FamCur.ListPats));
			PatFieldList=PatFields.Refresh(patNum);
		}

		private void RefreshModuleScreen(){
			//ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(PatCur!=null){
				//ToolBarMain.Buttons["Recall"].Enabled=true;
				ToolBarMain.Buttons["Add"].Enabled=true;
				ToolBarMain.Buttons["Delete"].Enabled=true;
				ToolBarMain.Buttons["Guarantor"].Enabled=true;
				ToolBarMain.Buttons["Move"].Enabled=true;
				if(!PrefC.GetBool(PrefName.EasyHideInsurance)){
					ToolBarMain.Buttons["Ins"].Enabled=true;
				}
				ToolBarMain.Invalidate();
			}
			else{
				//ToolBarMain.Buttons["Recall"].Enabled=false;
				ToolBarMain.Buttons["Add"].Enabled=false;
				ToolBarMain.Buttons["Delete"].Enabled=false;
				ToolBarMain.Buttons["Guarantor"].Enabled=false;
				ToolBarMain.Buttons["Move"].Enabled=false;
				if(!PrefC.GetBool(PrefName.EasyHideInsurance)){
					ToolBarMain.Buttons["Ins"].Enabled=false;
				}
				ToolBarMain.Invalidate();
				//Patients.Cur=new Patient();
			}
			if(PrefC.GetBool(PrefName.EasyHideInsurance)){
				gridIns.Visible=false;
			}
			else{
				gridIns.Visible=true;
			}
			FillPatientPicture();
			FillPatientData();
			FillFamilyData();
			FillGridRecall();
			FillInsData();
		} 

		private void FillPatientPicture(){
			picturePat.Image=null;
			picturePat.TextNullImage=Lan.g(this,"Patient Picture Unavailable");
			if(PatCur==null || 
				!PrefC.UsingAtoZfolder){//Do not use patient image when A to Z folders are disabled.
				return;
			}
			try{
				Bitmap patPict;
				Documents.GetPatPict(	PatCur.PatNum,ImageStore.GetPatientFolder(PatCur),out patPict);
				picturePat.Image=patPict;
			}
			catch{
			}
		}

		///<summary></summary>
		public void InitializeOnStartup(){
			if(InitializedOnStartup) {
				return;
			}
			InitializedOnStartup=true;
			//tbFamily.InstantClasses();
			//cannot use Lan.F(this);
			Lan.C(this,new Control[]
				{
				//butPatEdit,
				//butEditPriCov,
				//butEditPriPlan,
				//butEditSecCov,
				//butEditSecPlan
				});
			LayoutToolBar();
			//gridPat.Height=this.ClientRectangle.Bottom-gridPat.Top-2;
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Recall"),1,"","Recall"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Family Members:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),2,"Add Family Member","Add"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Delete"),3,Lan.g(this,"Delete Family Member"),"Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Set Guarantor"),4,Lan.g(this,"Set as Guarantor"),"Guarantor"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Move"),5,Lan.g(this,"Move to Another Family"),"Move"));
			if(!PrefC.GetBool(PrefName.EasyHideInsurance)){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				button=new ODToolBarButton(Lan.g(this,"Add Insurance"),6,"","Ins");
				button.Style=ODToolBarButtonStyle.DropDownButton;
				button.DropDownMenu=menuInsurance;
				ToolBarMain.Buttons.Add(button);
			}
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.FamilyModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void ContrFamily_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			
		}

		private void ContrFamily_Resize(object sender,EventArgs e) {
			/*if(Height>gridPat.Top){
				gridPat.Height=Height-gridPat.Top-2;
				gridIns.Height=Height-gridIns.Top-2;
			}
			if(Width>gridIns.Left){
				gridIns.Width=Width-gridIns.Left-2;
			}*/
		}

		//private void butOutlook_Click(object sender, System.EventArgs e) {
			/*Process[] procsOutlook = Process.GetProcessesByName("outlook");
			if(procsOutlook.Length==0){
				try{
					Process.Start("Outlook");
				}
				catch{}
			}*/
		//}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					//case "Recall":
					//	ToolButRecall_Click();
					//	break;
					case "Add":
						ToolButAdd_Click();
						break;
					case "Delete":
						ToolButDelete_Click();
						break;
					case "Guarantor":
						ToolButGuarantor_Click();
						break;
					case "Move":
						ToolButMove_Click();
						break;
					case "Ins":
						ToolButIns_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(long)) {
				ProgramL.Execute((long)e.Button.Tag,PatCur);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(long patNum,string patName,bool hasEmail,string chartNumber) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum,patName,hasEmail,chartNumber);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		#region gridPatient

		private void gridPat_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(Plugins.Active && Plugins.HookMethod(this,"ContrFamily.gridPat_CellDoubleClick",PatCur)) {
				return;
			}
			if(TerminalActives.PatIsInUse(PatCur.PatNum)){
				MsgBox.Show(this,"Patient is currently entering info at a reception terminal.  Please try again later.");
				return;
			}
			if(gridPat.Rows[e.Row].Tag!=null){
				if(gridPat.Rows[e.Row].Tag.ToString()=="Referral"){
					//RefAttach refattach=(RefAttach)gridPat.Rows[e.Row].Tag;
					FormReferralsPatient FormRE=new FormReferralsPatient();
					FormRE.PatNum=PatCur.PatNum;
					FormRE.ShowDialog();
				}
				else{//patfield
					string tag=gridPat.Rows[e.Row].Tag.ToString();
					tag=tag.Substring(8);//strips off all but the number: PatField1
					int index=PIn.Int(tag);
					PatField field=PatFields.GetByName(PatFieldDefs.List[index].FieldName,PatFieldList);
					if(field==null) {
						field=new PatField();
						field.PatNum=PatCur.PatNum;
						field.FieldName=PatFieldDefs.List[index].FieldName;
						FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
						FormPF.IsNew=true;
						FormPF.ShowDialog();
					}
					else{
						FormPatFieldEdit FormPF=new FormPatFieldEdit(field);
						FormPF.ShowDialog();
					}
				}
			}
			else{
				string email=PatCur.Email;
				long siteNum=PatCur.SiteNum;
				//
				FormPatientEdit FormP=new FormPatientEdit(PatCur,FamCur);
				FormP.IsNew=false;
				FormP.ShowDialog();
				//there are many things which may have changed that need to trigger refresh:
				//FName, LName, MiddleI, Preferred, SiteNum, or ChartNumber should refresh title bar.
				//Email change should change email but enabled.
				//Instead of checking for each of those:
				/*
				if(email!=PatCur.Email){//PatCur.EmailChanged){//do it this way later
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				}
				if(siteNum!=PatCur.SiteNum){
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				}*/
				if(FormP.DialogResult==DialogResult.OK) {
					OnPatientSelected(PatCur.PatNum,PatCur.GetNameLF(),PatCur.Email!="",PatCur.ChartNumber);
				}
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void FillPatientData(){
			if(PatCur==null){
				gridPat.BeginUpdate();
				gridPat.Rows.Clear();
				gridPat.Columns.Clear();
				gridPat.EndUpdate();
				return;
			}
			gridPat.BeginUpdate();
			gridPat.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);
			gridPat.Columns.Add(col);
			col=new ODGridColumn("",150);
			gridPat.Columns.Add(col);
			gridPat.Rows.Clear();
			ODGridRow row;
			List<DisplayField> fields=DisplayFields.GetForCategory(DisplayFieldCategory.PatientInformation);
			for(int f=0;f<fields.Count;f++) {
				row=new ODGridRow();
				if(fields[f].Description==""){
					if(fields[f].InternalName=="SS#"){
						if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
							row.Cells.Add("SIN");
						}
						else if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="GB") {
							row.Cells.Add("");
						}
						else{
							row.Cells.Add("SS#");
						}
					}
					else if(fields[f].InternalName=="State"){
						if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
							row.Cells.Add("Province");
						}
						else if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="GB") {
							row.Cells.Add("");
						}
						else{
							row.Cells.Add("State");
						}
					}
					else if(fields[f].InternalName=="Zip"){
						if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
							row.Cells.Add("Postal Code");
						}
						else if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="GB") {
							row.Cells.Add("Postcode");
						}
						else{
							row.Cells.Add(Lan.g("TablePatient","Zip"));
						}
					}
					else if(fields[f].InternalName=="PatFields"){
						//don't add a cell
					}
					else{
						row.Cells.Add(fields[f].InternalName);
					}
				}
				else{
					if(fields[f].InternalName=="PatFields") {
						//don't add a cell
					}
					else {
						row.Cells.Add(fields[f].Description);
					}
				}
				switch(fields[f].InternalName){
					case "Last":
						row.Cells.Add(PatCur.LName);
						break;
					case "First":
						row.Cells.Add(PatCur.FName);
						break;
					case "Middle":
						row.Cells.Add(PatCur.MiddleI);
						break;
					case "Preferred":
						row.Cells.Add(PatCur.Preferred);
						break;
					case "Title":
						row.Cells.Add(PatCur.Title);
						break;
					case "Salutation":
						row.Cells.Add(PatCur.Salutation);
						break;
					case "Status":
						row.Cells.Add(PatCur.PatStatus.ToString());
						if(PatCur.PatStatus==PatientStatus.Deceased) {
							row.ColorText=Color.Red;
						}
						break;
					case "Gender":
						row.Cells.Add(PatCur.Gender.ToString());
						break;
					case "Position":
						row.Cells.Add(PatCur.Position.ToString());
						break;
					case "Birthdate":
						if(PatCur.Birthdate.Year < 1880){
							row.Cells.Add("");
						}
						else{
							row.Cells.Add(PatCur.Birthdate.ToString("d"));
						}
						break;
					case "Age":
						row.Cells.Add(PatientLogic.DateToAgeString(PatCur.Birthdate));
						break;
					case "SS#":
						if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="US" 
							&& PatCur.SSN !=null && PatCur.SSN.Length==9)
						{
							row.Cells.Add(PatCur.SSN.Substring(0,3)+"-"+PatCur.SSN.Substring(3,2)+"-"+PatCur.SSN.Substring(5,4));
						}
						else {
							row.Cells.Add(PatCur.SSN);
						}
						break;
					case "Address":
						row.Cells.Add(PatCur.Address);
						row.Bold=true;
						break;
					case "Address2":
						row.Cells.Add(PatCur.Address2);
						break;
					case "City":
						row.Cells.Add(PatCur.City);
						break;
					case "State":
						row.Cells.Add(PatCur.State);
						break;
					case "Zip":
						row.Cells.Add(PatCur.Zip);
						break;
					case "Hm Phone":
						row.Cells.Add(PatCur.HmPhone);
						if(PatCur.PreferContactMethod==ContactMethod.HmPhone || PatCur.PreferContactMethod==ContactMethod.None){
							row.Bold=true;
						}
						break;
					case "Wk Phone":
						row.Cells.Add(PatCur.WkPhone);
						if(PatCur.PreferContactMethod==ContactMethod.WkPhone) {
							row.Bold=true;
						}
						break;
					case "Wireless Ph":
						row.Cells.Add(PatCur.WirelessPhone);
						if(PatCur.PreferContactMethod==ContactMethod.WirelessPh) {
							row.Bold=true;
						}
						break;
					case "E-mail":
						row.Cells.Add(PatCur.Email);
						if(PatCur.PreferContactMethod==ContactMethod.Email) {
							row.Bold=true;
						}
						break;
					case "Contact Method":
						row.Cells.Add(PatCur.PreferContactMethod.ToString());
						if(PatCur.PreferContactMethod==ContactMethod.DoNotCall || PatCur.PreferContactMethod==ContactMethod.SeeNotes){
							row.Bold=true;
						}
						break;
					case "ABC0":
						row.Cells.Add(PatCur.CreditType);
						break;
					case "Chart Num":
						row.Cells.Add(PatCur.ChartNumber);
						break;
					case "Billing Type":
						row.Cells.Add(DefC.GetName(DefCat.BillingTypes,PatCur.BillingType));
						break;
					case "Ward":
						row.Cells.Add(PatCur.Ward);
						break;
					case "AdmitDate":
						row.Cells.Add(PatCur.AdmitDate.ToShortDateString());
						break;
					case "Primary Provider":
						row.Cells.Add(Providers.GetLongDesc(Patients.GetProvNum(PatCur)));
						break;
					case "Sec. Provider":
						if(PatCur.SecProv != 0){
							row.Cells.Add(Providers.GetLongDesc(PatCur.SecProv));
						}
						else{
							row.Cells.Add("None");
						}
						break;
					case "Language":
						if(PatCur.Language==""){
							row.Cells.Add("");
						}
						else{
							row.Cells.Add(CultureInfo.GetCultureInfo(PatCur.Language).DisplayName);
						}
						break;
					case "Clinic":
						row.Cells.Add(Clinics.GetDesc(PatCur.ClinicNum));
						break;
					case "ResponsParty":
						if(PatCur.ResponsParty==0){
							row.Cells.Add("");
						}
						else{
							row.Cells.Add(Patients.GetLim(PatCur.ResponsParty).GetNameLF());
						}
						row.ColorBackG=DefC.Short[(int)DefCat.MiscColors][8].ItemColor;
						break;
					case "Referrals":
						RefAttach[] RefList=RefAttaches.Refresh(PatCur.PatNum);
						if(RefList.Length==0){
							row.Cells.Add(Lan.g("TablePatient","None"));
							row.Tag="Referral";
							row.ColorBackG=DefC.Short[(int)DefCat.MiscColors][8].ItemColor;
						}
						//else{
						//	row.Cells.Add("");
						//	row.Tag="Referral";
						//	row.ColorBackG=DefC.Short[(int)DefCat.MiscColors][8].ItemColor;
						//}
						for(int i=0;i<RefList.Length;i++) {
							row=new ODGridRow();
							if(RefList[i].IsFrom){
								row.Cells.Add(Lan.g("TablePatient","Referred From"));
							}
							else{
								row.Cells.Add(Lan.g("TablePatient","Referred To"));
							}
							try{
								string refInfo=Referrals.GetNameLF(RefList[i].ReferralNum);
								string phoneInfo=Referrals.GetPhone(RefList[i].ReferralNum);
								if(phoneInfo!="" || RefList[i].Note!=""){
									refInfo+="\r\n"+phoneInfo+" "+RefList[i].Note;
								}
								row.Cells.Add(refInfo);
							}
							catch{
								row.Cells.Add("");//if referral is null because using random keys and had bug.
							}
							row.Tag="Referral";
							row.ColorBackG=DefC.Short[(int)DefCat.MiscColors][8].ItemColor;
							if(i<RefList.Length-1){
								gridPat.Rows.Add(row);
							}
						}
						break;
					case "Addr/Ph Note":
						row.Cells.Add(PatCur.AddrNote);
						if(PatCur.AddrNote!=""){
							row.ColorText=Color.Red;
							row.Bold=true;
						}
						break;
					case "Guardians":
						List<Guardian> guardianList=Guardians.Refresh(PatCur.PatNum);
						string str="";
						for(int g=0;g<guardianList.Count;g++) {
							if(g>0) {
								str+=",";
							}
							str+=FamCur.GetNameInFamFirst(guardianList[g].PatNumGuardian)+Guardians.GetGuardianRelationshipStr(guardianList[g].Relationship);
						}
						row.Cells.Add(str);
						break;
					case "PatFields":
						PatField field;
						for(int i=0;i<PatFieldDefs.List.Length;i++){
							if(i>0){
								row=new ODGridRow();
							}
							row.Cells.Add(PatFieldDefs.List[i].FieldName);
							field=PatFields.GetByName(PatFieldDefs.List[i].FieldName,PatFieldList);
							if(field==null){
								row.Cells.Add("");
							}
							else{
								row.Cells.Add(field.FieldValue);
							}
							row.Tag="PatField"+i.ToString();
							gridPat.Rows.Add(row);
						}
						break;
				}
				if(fields[f].InternalName=="PatFields"){
					//don't add the row here
				}
				else{
					gridPat.Rows.Add(row);
				}
			}
			gridPat.EndUpdate();
		}

		#endregion gridPatient 

		#region gridFamily

		private void FillFamilyData(){
			gridFamily.BeginUpdate();
			gridFamily.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePatient","Name"),140);
			gridFamily.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePatient","Position"),65);
			gridFamily.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePatient","Gender"),55);
			gridFamily.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePatient","Status"),65);
			gridFamily.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePatient","Age"),45);
			gridFamily.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePatient","Recall Due"),80);
			gridFamily.Columns.Add(col);
			gridFamily.Rows.Clear();
			if(PatCur==null){
				return;
			}
			ODGridRow row;
			DateTime recallDate;
			ODGridCell cell;
			for(int i=0;i<FamCur.ListPats.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(FamCur.GetNameInFamLFI(i));
				row.Cells.Add(Lan.g("enumPatientPosition",FamCur.ListPats[i].Position.ToString()));
				row.Cells.Add(Lan.g("enumPatientGender",FamCur.ListPats[i].Gender.ToString()));
				row.Cells.Add(Lan.g("enumPatientStatus",FamCur.ListPats[i].PatStatus.ToString()));
				row.Cells.Add(Patients.AgeToString(FamCur.ListPats[i].Age));
				recallDate=DateTime.MinValue;
				for(int j=0;j<RecallList.Count;j++){
					if(RecallList[j].PatNum==FamCur.ListPats[i].PatNum
						&& (RecallList[j].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)
						|| RecallList[j].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)))
					{
						recallDate=RecallList[j].DateDue;
					}
				}
				cell=new ODGridCell();
				if(recallDate.Year>1880){
					cell.Text=recallDate.ToShortDateString();
					if(recallDate<DateTime.Today){
						cell.Bold=YN.Yes;
						cell.ColorText=Color.Firebrick;
					}
				}
				row.Cells.Add(cell);
				if(i==0){//guarantor
					row.Bold=true;
				}
				gridFamily.Rows.Add(row);
			}
			gridFamily.EndUpdate();
			gridFamily.SetSelected(FamCur.GetIndex(PatCur.PatNum),true);
		}

		private void gridFamily_CellClick(object sender,ODGridClickEventArgs e) {
			//if (tbFamily.SelectedRow != -1){
			//	tbFamily.ColorRow(tbFamily.SelectedRow,Color.White);
			//}
			//tbFamily.SelectedRow=e.Row;
			//tbFamily.ColorRow(e.Row,Color.DarkSalmon);
			OnPatientSelected(FamCur.ListPats[e.Row].PatNum,FamCur.ListPats[e.Row].GetNameLF(),FamCur.ListPats[e.Row].Email!="",
				FamCur.ListPats[e.Row].ChartNumber);
			ModuleSelected(FamCur.ListPats[e.Row].PatNum);
		}

		//private void butAddPt_Click(object sender, System.EventArgs e) {
		private void ToolButAdd_Click(){
			Patient tempPat=new Patient();
			tempPat.LName      =PatCur.LName;
			tempPat.PatStatus  =PatientStatus.Patient;
			tempPat.Address    =PatCur.Address;
			tempPat.Address2   =PatCur.Address2;
			tempPat.City       =PatCur.City;
			tempPat.State      =PatCur.State;
			tempPat.Zip        =PatCur.Zip;
			tempPat.HmPhone    =PatCur.HmPhone;
			tempPat.Guarantor  =PatCur.Guarantor;
			tempPat.CreditType =PatCur.CreditType;
			tempPat.PriProv    =PatCur.PriProv;
			tempPat.SecProv    =PatCur.SecProv;
			tempPat.FeeSched   =PatCur.FeeSched;
			tempPat.BillingType=PatCur.BillingType;
			tempPat.AddrNote   =PatCur.AddrNote;
			tempPat.ClinicNum  =PatCur.ClinicNum;
			Patients.Insert(tempPat,false);
			FormPatientEdit FormPE=new FormPatientEdit(tempPat,FamCur);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				OnPatientSelected(tempPat.PatNum,tempPat.GetNameLF(),tempPat.Email!="",tempPat.ChartNumber);
				ModuleSelected(tempPat.PatNum);
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		//private void butDeletePt_Click(object sender, System.EventArgs e) {
		private void ToolButDelete_Click(){
			//this doesn't actually delete the patient, just changes their status
			//and they will never show again in the patient selection list.
			//check for plans, appointments, procedures, etc.
			List<Procedure> procList=Procedures.Refresh(PatCur.PatNum);
			List<Claim> claimList=Claims.Refresh(PatCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(PatCur.PatNum);
			PaySplit[] PaySplitList=PaySplits.Refresh(PatCur.PatNum);//
			List<ClaimProc> claimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			Commlog[] commlogList=Commlogs.Refresh(PatCur.PatNum);
			int payPlanCount=PayPlans.GetDependencyCount(PatCur.PatNum);
			List<InsPlan> planList=InsPlans.RefreshForFam(FamCur);
			List<MedicationPat> medList=MedicationPats.GetList(PatCur.PatNum);
			PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			//CovPats.Refresh(planList,PatPlanList);
			RefAttach[] RefAttachList=RefAttaches.Refresh(PatCur.PatNum);
			bool hasProcs=procList.Count>0;
			bool hasClaims=claimList.Count>0;
			bool hasAdj=AdjustmentList.Length>0;
			bool hasPay=PaySplitList.Length>0;
			bool hasClaimProcs=claimProcList.Count>0;
			bool hasComm=commlogList.Length>0;
			bool hasPayPlans=payPlanCount>0;
			bool hasInsPlans=false;
			bool hasMeds=medList.Count>0;
			for(int i=0;i<planList.Count;i++){
				if(planList[i].Subscriber==PatCur.PatNum){
					hasInsPlans=true;
				}
			}
			bool hasRef=RefAttachList.Length>0;
			if(hasProcs || hasClaims || hasAdj || hasPay || hasClaimProcs || hasComm || hasPayPlans
				|| hasInsPlans || hasRef || hasMeds)
			{
				string message=Lan.g(this,
					"You cannot delete this patient without first deleting the following data:")+"\r";
				if(hasProcs)
					message+=Lan.g(this,"Procedures")+"\r";
				if(hasClaims)
					message+=Lan.g(this,"Claims")+"\r";
				if(hasAdj)
					message+=Lan.g(this,"Adjustments")+"\r";
				if(hasPay)
					message+=Lan.g(this,"Payments")+"\r";
				if(hasClaimProcs)
					message+=Lan.g(this,"Procedures attached to claims")+"\r";
				if(hasComm)
					message+=Lan.g(this,"Commlog entries")+"\r";
				if(hasPayPlans)
					message+=Lan.g(this,"Payment plans")+"\r";
				if(hasInsPlans)
					message+=Lan.g(this,"Insurance plans")+"\r";
				if(hasRef)
					message+=Lan.g(this,"References")+"\r";
				if(hasMeds)
					message+=Lan.g(this,"Medications")+"\r";
				MessageBox.Show(message);
				return;
			}
			Patient PatOld=PatCur.Copy();
			if(PatCur.PatNum==PatCur.Guarantor){//if selecting guarantor
				if(FamCur.ListPats.Length==1){
					if(!MsgBox.Show(this,true,"Delete Patient?"))
						return;
					PatCur.PatStatus=PatientStatus.Deleted;
					PatCur.ChartNumber="";
					Patients.Update(PatCur,PatOld);
					for(int i=0;i<RecallList.Count;i++){
						if(RecallList[i].PatNum==PatCur.PatNum){
							RecallList[i].IsDisabled=true;
							RecallList[i].DateDue=DateTime.MinValue;
							Recalls.Update(RecallList[i]);
						}
					}
					ModuleSelected(0);
					//does not delete notes or plans, etc.
				}
				else{
					MessageBox.Show(Lan.g(this,"You cannot delete the guarantor if there are other family members. You would have to make a different family member the guarantor first."));
				}
			}
			else{//not selecting guarantor
				if(!MsgBox.Show(this,true,"Delete Patient?"))
					return;
				PatCur.PatStatus=PatientStatus.Deleted;
				PatCur.ChartNumber="";
				PatCur.Guarantor=PatCur.PatNum;
				Patients.Update(PatCur,PatOld);
				for(int i=0;i<RecallList.Count;i++){
					if(RecallList[i].PatNum==PatCur.PatNum){
						RecallList[i].IsDisabled=true;
						RecallList[i].DateDue=DateTime.MinValue;
						Recalls.Update(RecallList[i]);
					}
				}
				ModuleSelected(PatOld.Guarantor);
			}
		}

		//private void butSetGuar_Click(object sender,System.EventArgs e){
    private void ToolButGuarantor_Click(){
			//Patient tempPat=PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){
				MessageBox.Show(Lan.g(this
					,"Patient is already the guarantor.  Please select a different family member."));
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Make the selected patient the guarantor?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return;
				Patients.ChangeGuarantorToCur(FamCur,PatCur);
			}
			ModuleSelected(PatCur.PatNum);
		}

		//private void butMovePat_Click(object sender, System.EventArgs e) {
		private void ToolButMove_Click(){
			Patient PatOld=PatCur.Copy();
			//Patient PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){//if guarantor selected
				if(FamCur.ListPats.Length==1){//and no other family members
					//no need to check insurance.  It will follow.
					if(!MsgBox.Show(this,true,"Moving the guarantor will cause two families to be combined.  The financial notes for both families will be combined and may need to be edited.  The address notes will also be combined and may need to be edited. Do you wish to continue?"))
						return;
					if(!MsgBox.Show(this,true,"Select the family to move this patient to from the list that will come up next."))
						return;
					FormPatientSelect FormPS=new FormPatientSelect();
					FormPS.SelectionModeOnly=true;
					FormPS.ShowDialog();
					if(FormPS.DialogResult!=DialogResult.OK){
						return;
					}
					Patient Lim=Patients.GetLim(FormPS.SelectedPatNum);
					PatCur.Guarantor=Lim.Guarantor;
					Patients.Update(PatCur,PatOld);
					FamCur=Patients.GetFamily(PatCur.PatNum);
					Patients.CombineGuarantors(FamCur,PatCur);
				}
				else{//there are other family members
					MessageBox.Show(Lan.g(this,"You cannot move the guarantor.  If you wish to move the guarantor, you must make another family member the guarantor first."));
				}
			}
			else{//guarantor not selected
				if(!MsgBox.Show(this,true,"Preparing to move family member.  Financial notes and address notes will not be transferred.  Proceed to next step?")){
					return;
				}
				switch(MessageBox.Show(Lan.g(this,"Create new family instead of moving to an existing family?")
					,"",MessageBoxButtons.YesNoCancel)){
					case DialogResult.Cancel:
						return;
					case DialogResult.Yes://new family
						PatCur.Guarantor=PatCur.PatNum;
						Patients.Update(PatCur,PatOld);
						break;
					case DialogResult.No://move to an existing family
						if(!MsgBox.Show(this,true,"Select the family to move this patient to from the list that will come up next.")){
							return;
						}
						FormPatientSelect FormPS=new FormPatientSelect();
						FormPS.SelectionModeOnly=true;
						FormPS.ShowDialog();
						if(FormPS.DialogResult!=DialogResult.OK){
							return;
						}
						Patient Lim=Patients.GetLim(FormPS.SelectedPatNum);
						PatCur.Guarantor=Lim.Guarantor;
						Patients.Update(PatCur,PatOld);
						break;
				}//end switch
			}//end guarantor not selected
			ModuleSelected(PatCur.PatNum);
		}

		#endregion

		#region gridRecall
		private void FillGridRecall(){
			gridRecall.BeginUpdate();
			//standard width is 354.  Nice to grow it to 525 if space allows.
			int maxWidth=Width-gridRecall.Left;
			if(maxWidth>525){
				maxWidth=525;
			}
			if(maxWidth>354) {
				gridRecall.Width=maxWidth;
			}
			else {
				gridRecall.Width=354;
			}
			gridRecall.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecall","Type"),90);
			gridRecall.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecall","Due Date"),80);
			gridRecall.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecall","Sched Date"),80);
			gridRecall.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecall","Notes"),80);
			gridRecall.Columns.Add(col);
			gridRecall.Rows.Clear();
			if(PatCur==null){
				return;
			}
			//we just want the recall for the current patient
			List<Recall> recallListPat=new List<Recall>();
			for(int i=0;i<RecallList.Count;i++){
				if(RecallList[i].PatNum==PatCur.PatNum){
					recallListPat.Add(RecallList[i]);
				}
			}
			ODGridRow row;
			ODGridCell cell;
			for(int i=0;i<recallListPat.Count;i++){
				row=new ODGridRow();
				//Type
				string cellStr=RecallTypes.GetDescription(recallListPat[i].RecallTypeNum);
				row.Cells.Add(cellStr);
				//Due date
				if(recallListPat[i].DateDue.Year<1880){
					row.Cells.Add("");
				}
				else{
					cell=new ODGridCell(recallListPat[i].DateDue.ToShortDateString());
					if(recallListPat[i].DateDue<DateTime.Today){
						cell.Bold=YN.Yes;
						cell.ColorText=Color.Firebrick;
					}
					row.Cells.Add(cell);
				}
				//Sched Date
				if(recallListPat[i].DateScheduled.Year>1880){
					row.Cells.Add(recallListPat[i].DateScheduled.ToShortDateString());
				}
				else{
					row.Cells.Add("");
				}
				//Notes
				cellStr="";
				if(recallListPat[i].IsDisabled) {
					cellStr+=Lan.g(this,"Disabled");
					if(recallListPat[i].DatePrevious.Year>1800){
						cellStr+=Lan.g(this,". Previous: ")+recallListPat[i].DatePrevious.ToShortDateString();
						if(recallListPat[i].RecallInterval!=new Interval(0,0,0,0)){
							DateTime duedate=recallListPat[i].DatePrevious+recallListPat[i].RecallInterval;
							cellStr+=Lan.g(this,". (Due): ")+duedate.ToShortDateString();
						}
					}
				}
				if(recallListPat[i].DisableUntilDate.Year>1880) {
					if(cellStr!="") {
						cellStr+=", ";
					}
					cellStr+=Lan.g(this,"Disabled until ")+recallListPat[i].DisableUntilDate.ToShortDateString();
				}
				if(recallListPat[i].DisableUntilBalance>0) {
					if(cellStr!="") {
						cellStr+=", ";
					}
					cellStr+=Lan.g(this,"Disabled until balance ")+recallListPat[i].DisableUntilBalance.ToString("c");
				}
				if(recallListPat[i].RecallStatus!=0) {
					if(cellStr!="") {
						cellStr+=", ";
					}
					cellStr+=DefC.GetName(DefCat.RecallUnschedStatus,recallListPat[i].RecallStatus);
				}
				if(recallListPat[i].Note!="") {
					if(cellStr!="") {
						cellStr+=", ";
					}
					cellStr+=recallListPat[i].Note;
				}
				row.Cells.Add(cellStr);
				gridRecall.Rows.Add(row);
			}
			gridRecall.EndUpdate();
		}

		private void gridRecall_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//use doubleclick instead
		}

		private void gridRecall_DoubleClick(object sender,EventArgs e) {
			if(PatCur==null){
				return;
			}
			FormRecallsPat FormR=new FormRecallsPat();
			FormR.PatNum=PatCur.PatNum;
			FormR.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}
		#endregion gridRecall

		#region gridIns
		private void menuPlansForFam_Click(object sender,EventArgs e) {
			FormPlansForFamily FormP=new FormPlansForFamily();
			FormP.FamCur=FamCur;
			FormP.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void ToolButIns_Click(){
			DialogResult result=MessageBox.Show(Lan.g(this,"Is this patient the subscriber?"),"",MessageBoxButtons.YesNoCancel);
			if(result==DialogResult.Cancel){
				return;
			}
			//Pick a subscriber------------------------------------------------------------------------------------------------
			Patient subscriber;
			if(result==DialogResult.Yes){//current patient is subscriber
				subscriber=PatCur.Copy();
			}
			else{//patient is not subscriber
				//show list of patients in this family
				FormSubscriberSelect FormS=new FormSubscriberSelect(FamCur);
				FormS.ShowDialog();
				if(FormS.DialogResult==DialogResult.Cancel){
					return;
				}
				subscriber=Patients.GetPat(FormS.SelectedPatNum);
			}
			//Subscriber has been chosen. Now, pick a plan-------------------------------------------------------------------
			InsPlan plan=null;
			bool planIsNew=false;
			if(InsPlans.GetListForSubscriber(subscriber.PatNum).Count==0){
				planIsNew=true;
			}
			else{
				FormInsSelectSubscr FormISS=new FormInsSelectSubscr(subscriber.PatNum);
				FormISS.ShowDialog();
				if(FormISS.DialogResult==DialogResult.Cancel) {
					return;
				}
				if(FormISS.SelectedPlanNum==0){//'New' option selected.
					planIsNew=true;
				}
				else{
					plan=InsPlans.GetPlan(FormISS.SelectedPlanNum,PlanList);
				}
			}
			//New plan was selected instead of an existing plan.  Create the plan--------------------------------------------
			if(planIsNew){
				plan=new InsPlan();
				plan.EmployerNum=subscriber.EmployerNum;
				plan.Subscriber=subscriber.PatNum;
				if(subscriber.MedicaidID==""){
					plan.SubscriberID=subscriber.SSN;
				}
				else{
					plan.SubscriberID=subscriber.MedicaidID;
				}
				plan.ReleaseInfo=true;
				plan.AssignBen=true;
				//plan.DedBeforePerc=PrefC.GetBool(PrefName.DeductibleBeforePercentAsDefault");
				plan.PlanType="";
				InsPlans.Insert(plan);
				Benefit ben;
				for(int i=0;i<CovCatC.ListShort.Count;i++){
					if(CovCatC.ListShort[i].DefaultPercent==-1){
						continue;
					}
					ben=new Benefit();
					ben.BenefitType=InsBenefitType.CoInsurance;
					ben.CovCatNum=CovCatC.ListShort[i].CovCatNum;
					ben.PlanNum=plan.PlanNum;
					ben.Percent=CovCatC.ListShort[i].DefaultPercent;
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					ben.CodeNum=0;
					Benefits.Insert(ben);
				}
				//Zero deductible diagnostic
				if(CovCats.GetForEbenCat(EbenefitCategory.Diagnostic)!=null) {
					ben=new Benefit();
					ben.CodeNum=0;
					ben.BenefitType=InsBenefitType.Deductible;
					ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum;
					ben.PlanNum=plan.PlanNum;
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					ben.MonetaryAmt=0;
					ben.Percent=-1;
					ben.CoverageLevel=BenefitCoverageLevel.Individual;
					Benefits.Insert(ben);
				}
				//Zero deductible preventive
				if(CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive)!=null) {
					ben=new Benefit();
					ben.CodeNum=0;
					ben.BenefitType=InsBenefitType.Deductible;
					ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
					ben.PlanNum=plan.PlanNum;
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					ben.MonetaryAmt=0;
					ben.Percent=-1;
					ben.CoverageLevel=BenefitCoverageLevel.Individual;
					Benefits.Insert(ben);
				}
			}
			//Then attach plan------------------------------------------------------------------------------------------------
			PatPlan patplan=new PatPlan();
			patplan.Ordinal=PatPlanList.Count+1;//so the ordinal of the first entry will be 1, NOT 0.
			patplan.PatNum=PatCur.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;
			PatPlans.Insert(patplan);
			//Then, display insPlanEdit to user-------------------------------------------------------------------------------
			FormInsPlan FormI=new FormInsPlan(plan,patplan);
			FormI.IsNewPlan=planIsNew;
			FormI.IsNewPatPlan=true;
			FormI.ShowDialog();//this updates estimates also.
			//if cancel, then patplan is deleted from within that dialog.
			//if cancel, and planIsNew, then plan and benefits are also deleted.
			ModuleSelected(PatCur.PatNum);
		}

		private void FillInsData(){
			if(PatPlanList.Count==0){
				gridIns.BeginUpdate();
				gridIns.Columns.Clear();
				gridIns.Rows.Clear();
				gridIns.EndUpdate();
				return;
			}
			List <InsPlan> planArray=new List <InsPlan> ();//prevents repeated calls to db.
			for(int i=0;i<PatPlanList.Count;i++){
				planArray.Add(InsPlans.GetPlan(PatPlanList[i].PlanNum,PlanList));
			}
			gridIns.BeginUpdate();
			gridIns.Columns.Clear();
			gridIns.Rows.Clear();
			OpenDental.UI.ODGridColumn col;
			col=new ODGridColumn("",150);
			gridIns.Columns.Add(col);
			for(int i=0;i<PatPlanList.Count;i++){
				if(planArray[i].IsMedical){
					col=new ODGridColumn(Lan.g("TableCoverage","Medical"),170);
					gridIns.Columns.Add(col);
				}
				else if(i==0){
					col=new ODGridColumn(Lan.g("TableCoverage","Primary"),170);
					gridIns.Columns.Add(col);
				}
				else if(i==1){
					col=new ODGridColumn(Lan.g("TableCoverage","Secondary"),170);
					gridIns.Columns.Add(col);
				}
				else{
					col=new ODGridColumn(Lan.g("TableCoverage","Other"),170);
					gridIns.Columns.Add(col);
				}
			}
			OpenDental.UI.ODGridRow row=new ODGridRow();
			//subscriber
			row.Cells.Add(Lan.g("TableCoverage","Subscriber"));
			for(int i=0;i<PatPlanList.Count;i++){
				row.Cells.Add(FamCur.GetNameInFamFL(planArray[i].Subscriber));
			}
			row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//subscriber ID
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Subscriber ID"));
			for(int i=0;i<PatPlanList.Count;i++) {
				row.Cells.Add(planArray[i].SubscriberID);
			}
			row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//relationship
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Rel'ship to Sub"));
			for(int i=0;i<PatPlanList.Count;i++){
				row.Cells.Add(Lan.g("enumRelat",PatPlanList[i].Relationship.ToString()));
			}
			row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//patient ID
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Patient ID"));
			for(int i=0;i<PatPlanList.Count;i++){
				row.Cells.Add(PatPlanList[i].PatID);
			}
			row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//pending
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Pending"));
			for(int i=0;i<PatPlanList.Count;i++){
				if(PatPlanList[i].IsPending){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
			}
			row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][0].ItemColor;
			row.ColorLborder=Color.Black;
			gridIns.Rows.Add(row);
			//employer
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Employer"));
			for(int i=0;i<PatPlanList.Count;i++) {
				row.Cells.Add(Employers.GetName(planArray[i].EmployerNum));
			}
			gridIns.Rows.Add(row);
			//carrier
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Carrier"));
			for(int i=0;i<PatPlanList.Count;i++) {
				row.Cells.Add(InsPlans.GetCarrierName(PatPlanList[i].PlanNum,planArray));
			}
			gridIns.Rows.Add(row);
			//group name
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Group Name"));
			for(int i=0;i<PatPlanList.Count;i++) {
				row.Cells.Add(planArray[i].GroupName);
			}
			gridIns.Rows.Add(row);
			//group number
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Group Number"));
			for(int i=0;i<PatPlanList.Count;i++) {
				row.Cells.Add(planArray[i].GroupNum);
			}
			gridIns.Rows.Add(row);
			//plan type
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Type"));
			for(int i=0;i<planArray.Count;i++) {
				switch(planArray[i].PlanType){
					default://malfunction
						row.Cells.Add("");
						break;
					case "":
						row.Cells.Add(Lan.g(this,"Category Percentage"));
						break;
					case "p":
						row.Cells.Add(Lan.g(this,"PPO Percentage"));
						break;
					case "f":
						row.Cells.Add(Lan.g(this,"Medicaid or Flat Co-pay"));
						break;
					case "c":
						row.Cells.Add(Lan.g(this,"Capitation"));
						break;
				}
			}
			gridIns.Rows.Add(row);
			//fee schedule
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Fee Schedule"));
			for(int i=0;i<planArray.Count;i++) {
				row.Cells.Add(FeeScheds.GetDescription(planArray[i].FeeSched));
			}
			row.ColorLborder=Color.Black;
			gridIns.Rows.Add(row);
			//Calendar vs service year------------------------------------------------------------------------------------
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Benefit Period"));
			for(int i=0;i<planArray.Count;i++) {
				if(planArray[i].MonthRenew==0) {
					row.Cells.Add(Lan.g("TableCoverage","Calendar Year"));
				}
				else {
					DateTime dateservice=new DateTime(2000,planArray[i].MonthRenew,1);
					row.Cells.Add(Lan.g("TableCoverage","Service year begins:")+" "+dateservice.ToString("MMMM"));
				}
			}
			gridIns.Rows.Add(row);
			//Benefits-----------------------------------------------------------------------------------------------------
			List <Benefit> bensForPat=Benefits.Refresh(PatPlanList);
			Benefit[,] benMatrix=Benefits.GetDisplayMatrix(bensForPat,PatPlanList);
			string desc;
			string val;
			ProcedureCode proccode=null;
			for(int y=0;y<benMatrix.GetLength(1);y++){//rows
				row=new ODGridRow();
				desc="";
				//some of the columns might be null, but at least one will not be.  Find it.
				for(int x=0;x<benMatrix.GetLength(0);x++){//columns
					if(benMatrix[x,y]==null){
						continue;
					}
					//create a description for the benefit
					if(benMatrix[x,y].PatPlanNum!=0) {
						desc+=Lan.g(this,"(pat)")+" ";
					}
					if(benMatrix[x,y].CoverageLevel==BenefitCoverageLevel.Family) {
						desc+=Lan.g(this,"Fam")+" ";
					}
					if(benMatrix[x,y].CodeNum!=0) {
						proccode=ProcedureCodes.GetProcCode(benMatrix[x,y].CodeNum);
					}
					if(benMatrix[x,y].BenefitType==InsBenefitType.CoInsurance && benMatrix[x,y].Percent != -1) {
						if(benMatrix[x,y].CodeNum==0) {
							desc+=CovCats.GetDesc(benMatrix[x,y].CovCatNum)+" % ";
						}
						else {
							desc+=proccode.ProcCode+"-"+proccode.AbbrDesc+" % ";
						}
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Deductible) {
						desc+=Lan.g(this,"Deductible")+" "+CovCats.GetDesc(benMatrix[x,y].CovCatNum)+" ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Limitations
						&& benMatrix[x,y].QuantityQualifier==BenefitQuantity.None
						&& (benMatrix[x,y].TimePeriod==BenefitTimePeriod.ServiceYear
						|| benMatrix[x,y].TimePeriod==BenefitTimePeriod.CalendarYear))
					{//annual max
						desc+=Lan.g(this,"Annual Max")+" ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Limitations
						&& CovCats.GetForEbenCat(EbenefitCategory.Orthodontics)!=null
						&& benMatrix[x,y].CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum
						&& benMatrix[x,y].QuantityQualifier==BenefitQuantity.None
						&& benMatrix[x,y].TimePeriod==BenefitTimePeriod.Lifetime)
					{
						desc+=Lan.g(this,"Ortho Max")+" ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Limitations
						&& CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive)!=null
						&& benMatrix[x,y].CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum
						&& benMatrix[x,y].Quantity !=0)
					{
						desc+="Exam frequency ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Limitations
						&& benMatrix[x,y].CodeNum==ProcedureCodes.GetCodeNum("D0274")//4BW
						&& benMatrix[x,y].Quantity !=0)
					{
						desc+="BW frequency ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Limitations
						&& benMatrix[x,y].CodeNum==ProcedureCodes.GetCodeNum("D0330")//Pano
						&& benMatrix[x,y].Quantity !=0)
					{
						desc+="Pano/FMX frequency ";
					}
					else if(benMatrix[x,y].CodeNum==0){//e.g. flo
						desc+=ProcedureCodes.GetProcCode(benMatrix[x,y].CodeNum).AbbrDesc+" ";
					}
					else{
						desc+=Lan.g("enumInsBenefitType",benMatrix[x,y].BenefitType.ToString())+" ";
					}
					row.Cells.Add(desc);
					break;
				}
				//remember that matrix does not include the description column
				for(int x=0;x<benMatrix.GetLength(0);x++){//columns
					val="";
					//this matrix cell might be null
					if(benMatrix[x,y]==null){
						row.Cells.Add("");
						continue;
					}
					if(benMatrix[x,y].Percent != -1) {
						val+=benMatrix[x,y].Percent.ToString()+"% ";
					}
					if(benMatrix[x,y].MonetaryAmt != -1) {
						val+=benMatrix[x,y].MonetaryAmt.ToString("c0")+" ";
					}
					/*
					if(benMatrix[x,y].BenefitType==InsBenefitType.CoInsurance) {
						val+=benMatrix[x,y].Percent.ToString()+" ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Deductible
						&& benMatrix[x,y].MonetaryAmt==0)
					{//deductible 0
						val+=benMatrix[x,y].MonetaryAmt.ToString("c0")+" ";
					}
					else if(benMatrix[x,y].BenefitType==InsBenefitType.Limitations
						&& benMatrix[x,y].QuantityQualifier==BenefitQuantity.None
						&& (benMatrix[x,y].TimePeriod==BenefitTimePeriod.ServiceYear
						|| benMatrix[x,y].TimePeriod==BenefitTimePeriod.CalendarYear)
						&& benMatrix[x,y].MonetaryAmt==0)
					{//annual max 0
						val+=benMatrix[x,y].MonetaryAmt.ToString("c0")+" ";
					}*/
					if(benMatrix[x,y].BenefitType==InsBenefitType.Exclusions
						|| benMatrix[x,y].BenefitType==InsBenefitType.Limitations) 
					{
						if(benMatrix[x,y].CodeNum != 0) {
							proccode=ProcedureCodes.GetProcCode(benMatrix[x,y].CodeNum);
							val+=proccode.ProcCode+"-"+proccode.AbbrDesc+" ";
						}
						else if(benMatrix[x,y].CovCatNum != 0){
							val+=CovCats.GetDesc(benMatrix[x,y].CovCatNum)+" ";
						}
					}
					if(benMatrix[x,y].QuantityQualifier==BenefitQuantity.NumberOfServices){//eg 2 times per CalendarYear
						val+=benMatrix[x,y].Quantity.ToString()+" "+Lan.g(this,"times per")+" "
							+Lan.g("enumBenefitQuantity",benMatrix[x,y].TimePeriod.ToString())+" ";
					}
					else if(benMatrix[x,y].QuantityQualifier==BenefitQuantity.Months) {//eg Every 2 months
						val+=Lan.g(this,"Every ")+benMatrix[x,y].Quantity.ToString()+" month";
						if(benMatrix[x,y].Quantity>1){
							val+="s";
						}
					}
					else if(benMatrix[x,y].QuantityQualifier==BenefitQuantity.Years) {//eg Every 2 years
						val+="Every "+benMatrix[x,y].Quantity.ToString()+" year";
						if(benMatrix[x,y].Quantity>1) {
							val+="s";
						}
					}
					else{
						if(benMatrix[x,y].QuantityQualifier!=BenefitQuantity.None){//e.g. flo
							val+=Lan.g("enumBenefitQuantity",benMatrix[x,y].QuantityQualifier.ToString())+" ";
						}
						if(benMatrix[x,y].Quantity!=0){
							val+=benMatrix[x,y].Quantity.ToString()+" ";
						}
					}
					//if(benMatrix[x,y].MonetaryAmt!=0){
					//	val+=benMatrix[x,y].MonetaryAmt.ToString("c0")+" ";
					//}
					//if(val==""){
					//	val="val";
					//}
					row.Cells.Add(val);
				}
				gridIns.Rows.Add(row);
			}
			//Plan note
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Ins Plan Note"));
			OpenDental.UI.ODGridCell cell;
			for(int i=0;i<PatPlanList.Count;i++){
				cell=new ODGridCell();
				cell.Text=planArray[i].PlanNote;
				cell.ColorText=Color.Red;
				cell.Bold=YN.Yes;
				row.Cells.Add(cell);
			}
			gridIns.Rows.Add(row);
			//Subscriber Note
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Subscriber Note"));
			for(int i=0;i<PatPlanList.Count;i++) {
				cell=new ODGridCell();
				cell.Text=planArray[i].SubscNote;
				cell.ColorText=Color.Red;
				cell.Bold=YN.Yes;
				row.Cells.Add(cell);
			}
			gridIns.Rows.Add(row);
			gridIns.EndUpdate();
		}

		private void gridIns_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(e.Col==0){
				return;
			}
			Cursor=Cursors.WaitCursor;
			PatPlan patPlan=PatPlanList[e.Col-1];
			InsPlan insPlan=InsPlans.GetPlan(patPlan.PlanNum,PlanList);
			FormInsPlan FormIP=new FormInsPlan(insPlan,patPlan);
			FormIP.ShowDialog();
			Cursor=Cursors.Default;
			ModuleSelected(PatCur.PatNum);
		}

		#endregion gridIns

		

		
























	}
}
