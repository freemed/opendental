/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
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
		private OpenDental.TableFamily tbFamily;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ContextMenu menuPatient;
		///<summary>All recalls for this entire family.</summary>
		private Recall[] RecallList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.UI.PictureBox picturePat;
		private InsPlan[] PlanList;
		private OpenDental.UI.ODGrid gridIns;
		private PatPlan[] PatPlanList;
		private ODGrid gridPat;
		private ContextMenu menuInsurance;
		private MenuItem menuPlansForFam;
		private Benefit[] BenefitList;
		private PatField[] PatFieldList;

		///<summary></summary>
		public ContrFamily(){
			Logger.openlog.Log("Initializing family module...",Logger.Severity.INFO);
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbFamily.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbFamily_CellClicked);
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
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.gridPat = new OpenDental.UI.ODGrid();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.picturePat = new OpenDental.UI.PictureBox();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.tbFamily = new OpenDental.TableFamily();
			this.menuInsurance = new System.Windows.Forms.ContextMenu();
			this.menuPlansForFam = new System.Windows.Forms.MenuItem();
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
			// gridPat
			// 
			this.gridPat.HScrollVisible = false;
			this.gridPat.Location = new System.Drawing.Point(0,133);
			this.gridPat.Name = "gridPat";
			this.gridPat.ScrollValue = 0;
			this.gridPat.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridPat.Size = new System.Drawing.Size(252,467);
			this.gridPat.TabIndex = 30;
			this.gridPat.Title = "Patient Information";
			this.gridPat.TranslationName = "TablePatient";
			this.gridPat.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPat_CellDoubleClick);
			// 
			// gridIns
			// 
			this.gridIns.HScrollVisible = true;
			this.gridIns.Location = new System.Drawing.Point(256,133);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.SelectionMode = OpenDental.UI.GridSelectionMode.None;
			this.gridIns.Size = new System.Drawing.Size(657,467);
			this.gridIns.TabIndex = 29;
			this.gridIns.Title = "Insurance Plans";
			this.gridIns.TranslationName = "TableCoverage";
			this.gridIns.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellDoubleClick);
			// 
			// picturePat
			// 
			this.picturePat.Location = new System.Drawing.Point(1,31);
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
			this.ToolBarMain.Size = new System.Drawing.Size(939,29);
			this.ToolBarMain.TabIndex = 19;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// tbFamily
			// 
			this.tbFamily.BackColor = System.Drawing.SystemColors.Window;
			this.tbFamily.Location = new System.Drawing.Point(104,31);
			this.tbFamily.Name = "tbFamily";
			this.tbFamily.ScrollValue = 1;
			this.tbFamily.SelectedIndices = new int[0];
			this.tbFamily.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbFamily.Size = new System.Drawing.Size(489,100);
			this.tbFamily.TabIndex = 7;
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
			// ContrFamily
			// 
			this.Controls.Add(this.gridPat);
			this.Controls.Add(this.gridIns);
			this.Controls.Add(this.picturePat);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.tbFamily);
			this.Name = "ContrFamily";
			this.Size = new System.Drawing.Size(939,708);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrFamily_Layout);
			this.Resize += new System.EventHandler(this.ContrFamily_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PlanList=null;
		}

		private void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				PatPlanList=new PatPlan[0]; 
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(patNum);
			BenefitList=Benefits.Refresh(PatPlanList);
			//CovPats.Refresh(PlanList,PatPlanList);
			//RefAttaches.Refresh();
			RecallList=Recalls.GetList(FamCur.List);
			PatFieldList=PatFields.Refresh(patNum);
		}

		private void RefreshModuleScreen(){
			ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(PatCur!=null){
				ToolBarMain.Buttons["Recall"].Enabled=true;
				ToolBarMain.Buttons["Add"].Enabled=true;
				ToolBarMain.Buttons["Delete"].Enabled=true;
				ToolBarMain.Buttons["Guarantor"].Enabled=true;
				ToolBarMain.Buttons["Move"].Enabled=true;
				if(!PrefB.GetBool("EasyHideInsurance")){
					ToolBarMain.Buttons["Ins"].Enabled=true;
				}
				ToolBarMain.Invalidate();
			}
			else{
				ToolBarMain.Buttons["Recall"].Enabled=false;
				ToolBarMain.Buttons["Add"].Enabled=false;
				ToolBarMain.Buttons["Delete"].Enabled=false;
				ToolBarMain.Buttons["Guarantor"].Enabled=false;
				ToolBarMain.Buttons["Move"].Enabled=false;
				if(!PrefB.GetBool("EasyHideInsurance")){
					ToolBarMain.Buttons["Ins"].Enabled=false;
				}
				ToolBarMain.Invalidate();
				//Patients.Cur=new Patient();
			}
			if(PrefB.GetBool("EasyHideInsurance")){
				gridIns.Visible=false;
			}
			else{
				gridIns.Visible=true;
			}
			FillPatientButton();
			FillPatientPicture();
			FillPatientData();
			FillFamilyData();
			FillInsData();
		} 

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void FillPatientPicture(){
			picturePat.Image=null;
			picturePat.TextNullImage=Lan.g(this,"Patient Picture Unavailable");
			if(PatCur==null || 
				!PrefB.UsingAtoZfolder){//Do not use patient image when A to Z folders are disabled.
				return;
			}
			try{
				Bitmap patPict;
				Documents.GetPatPict(	PatCur.PatNum,
															ODFileUtils.CombinePaths(new string[] {	FormPath.GetPreferredImagePath(),
																																			PatCur.ImageFolder.Substring(0,1).ToUpper(),
																																			PatCur.ImageFolder,""}),
															out patPict);
				picturePat.Image=patPict;
			}
			catch{
			}
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		///<summary></summary>
		public void InitializeOnStartup(){
			tbFamily.InstantClasses();
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
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Recall"),1,"","Recall"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Family Members:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),2,"Add Family Member","Add"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Delete"),3,Lan.g(this,"Delete Family Member"),"Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Set Guarantor"),4,Lan.g(this,"Set as Guarantor"),"Guarantor"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Move"),5,Lan.g(this,"Move to Another Family"),"Move"));
			if(!PrefB.GetBool("EasyHideInsurance")){
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
			if(Height>gridPat.Top){
				gridPat.Height=Height-gridPat.Top-2;
				gridIns.Height=Height-gridIns.Top-2;
			}
			if(Width>gridIns.Left){
				gridIns.Width=Width-gridIns.Left-2;
			}
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
					case "Patient":
						OnPat_Click();
						break;
					case "Recall":
						OnRecall_Click();
						break;
					case "Add":
						OnAdd_Click();
						break;
					case "Delete":
						OnDelete_Click();
						break;
					case "Guarantor":
						OnGuarantor_Click();
						break;
					case "Move":
						OnMove_Click();
						break;
					case "Ins":
						ToolButIns_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK){
				OnPatientSelected(formPS.SelectedPatNum);
				ModuleSelected(formPS.SelectedPatNum);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void OnRecall_Click() {
			//patient may or may not have an existing recall.
			Recall recallCur=null;
			for(int i=0;i<RecallList.Length;i++){
				if(RecallList[i].PatNum==PatCur.PatNum){
					recallCur=RecallList[i];
				}
			}
			//for testing purposes and because synchronization might have bugs, always synch here:
			//This might add a recall.
			Recalls.Synch(PatCur.PatNum,recallCur);			
			FormRecallEdit FormRE=new FormRecallEdit(PatCur);
			if(recallCur==null){
				recallCur=new Recall();
				recallCur.PatNum=PatCur.PatNum;
				recallCur.RecallInterval=new Interval(0,0,6,0);
				FormRE.IsNew=true;
			}
			FormRE.RecallCur=recallCur;
			FormRE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		#region gridPatient

		private void gridPat_CellDoubleClick(object sender,ODGridClickEventArgs e) {
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
					int index=PIn.PInt(tag);
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
				FormPatientEdit FormP=new FormPatientEdit(PatCur,FamCur);
				FormP.IsNew=false;
				FormP.ShowDialog();
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
			//Last
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Last"));
			row.Cells.Add(PatCur.LName);
			gridPat.Rows.Add(row);
			//First
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","First"));
			row.Cells.Add(PatCur.FName);
			gridPat.Rows.Add(row);
			//Middle
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Middle"));
			row.Cells.Add(PatCur.MiddleI);
			gridPat.Rows.Add(row);
			//Preferred
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Preferred"));
			row.Cells.Add(PatCur.Preferred);
			gridPat.Rows.Add(row);
			//Title
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Title"));
			row.Cells.Add(PatCur.Title);
			gridPat.Rows.Add(row);
			//Salutation
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Salutation"));
			row.Cells.Add(PatCur.Salutation);
			//row.ColorLborder=Color.Black;
			gridPat.Rows.Add(row);
			//Status
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Status"));
			row.Cells.Add(Lan.g("enum PatientStatus",PatCur.PatStatus.ToString()));
			if(PatCur.PatStatus==PatientStatus.Deceased) {
				row.ColorText=Color.Red;
			}
			gridPat.Rows.Add(row);
			//Gender
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Gender"));
			row.Cells.Add(Lan.g("enum PatientGender",PatCur.Gender.ToString()));
			gridPat.Rows.Add(row);
			//Position
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Position"));
			row.Cells.Add(Lan.g("enum PatientPosition",PatCur.Position.ToString()));
			//row.ColorLborder=Color.Black;
			gridPat.Rows.Add(row);
			//Birthdate
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Birthdate"));
			if(PatCur.Birthdate.Year < 1880)
				row.Cells.Add("");
			else
				row.Cells.Add(PatCur.Birthdate.ToString("d"));
			gridPat.Rows.Add(row);
			//Age
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Age"));
			row.Cells.Add(PatientB.DateToAgeString(PatCur.Birthdate));
				//AgeToString(PatCur.Age));
			gridPat.Rows.Add(row);
			//SS#
			row=new ODGridRow();
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
				row.Cells.Add("SIN");
			}
			else if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB") {
				row.Cells.Add("");
			}
			else{
				row.Cells.Add(Lan.g("TablePatient","SS#"));
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="US" && PatCur.SSN !=null && PatCur.SSN.Length==9){
				row.Cells.Add(PatCur.SSN.Substring(0,3)+"-"
					+PatCur.SSN.Substring(3,2)+"-"+PatCur.SSN.Substring(5,4));
			}
			else {
				row.Cells.Add(PatCur.SSN);
			}
			//row.ColorLborder=Color.Black;
			gridPat.Rows.Add(row);
			//Address
			row=new ODGridRow();
			ODGridCell cell=new ODGridCell(Lan.g("TablePatient","Address"));
			//cell.Bold=YN.Yes;
			row.Cells.Add(cell);
			row.Cells.Add(PatCur.Address);
			row.Bold=true;
			gridPat.Rows.Add(row);
			//Address2
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Address2"));
			row.Cells.Add(PatCur.Address2);
			gridPat.Rows.Add(row);
			//City
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","City"));
			row.Cells.Add(PatCur.City);
			gridPat.Rows.Add(row);
			//State
			row=new ODGridRow();
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
				row.Cells.Add("Province");
			}
			else if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB") {
				row.Cells.Add("");
			}
			else{
				row.Cells.Add(Lan.g("TablePatient","State"));
			}
			row.Cells.Add(PatCur.State);
			gridPat.Rows.Add(row);
			//Zip
			row=new ODGridRow();
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {
				row.Cells.Add("Postal Code");
			}
			else if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB") {
				row.Cells.Add("Postcode");
			}
			else{
				row.Cells.Add(Lan.g("TablePatient","Zip"));
			}
			row.Cells.Add(PatCur.Zip);
			//row.ColorLborder=Color.Black;
			gridPat.Rows.Add(row);
			//Hm Phone
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Hm Phone"));
			row.Cells.Add(PatCur.HmPhone);
			if(PatCur.PreferContactMethod==ContactMethod.HmPhone
				|| PatCur.PreferContactMethod==ContactMethod.None)
			{
				row.Bold=true;
			}
			gridPat.Rows.Add(row);
			//Wk Phone
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Wk Phone"));
			row.Cells.Add(PatCur.WkPhone);
			if(PatCur.PreferContactMethod==ContactMethod.WkPhone) {
				row.Bold=true;
			}
			gridPat.Rows.Add(row);
			//Wireless Ph
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Wireless Ph"));
			row.Cells.Add(PatCur.WirelessPhone);
			if(PatCur.PreferContactMethod==ContactMethod.WirelessPh) {
				row.Bold=true;
			}
			gridPat.Rows.Add(row);
			//E-mail
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","E-mail"));
			row.Cells.Add(PatCur.Email);
			if(PatCur.PreferContactMethod==ContactMethod.Email) {
				row.Bold=true;
			}
			gridPat.Rows.Add(row);
			//Contact Method
			if(PatCur.PreferContactMethod==ContactMethod.DoNotCall
				|| PatCur.PreferContactMethod==ContactMethod.SeeNotes)
			{
				row=new ODGridRow();
				row.Cells.Add(Lan.g("TablePatient","Contact Method"));
				row.Cells.Add(Lan.g("enumContactMethod",((ContactMethod)PatCur.PreferContactMethod).ToString()));
				row.Bold=true;
				gridPat.Rows.Add(row);
			}
			//Credit Type
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","ABC0"));
			row.Cells.Add(PatCur.CreditType);
			gridPat.Rows.Add(row);
			//Chart Num
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Chart Num"));
			row.Cells.Add(PatCur.ChartNumber);
			gridPat.Rows.Add(row);
			//Billing Type
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Billing Type"));
			row.Cells.Add(DefB.GetName(DefCat.BillingTypes,PatCur.BillingType));
			gridPat.Rows.Add(row);
			//Ward
			if(!PrefB.GetBool("EasyHideHospitals")){
				row=new ODGridRow();
				row.Cells.Add(Lan.g("TablePatient","Ward"));
				row.Cells.Add(PatCur.Ward);
				gridPat.Rows.Add(row);
				//AdmitDate
				row=new ODGridRow();
				row.Cells.Add(Lan.g("TablePatient","AdmitDate"));
				row.Cells.Add(PatCur.AdmitDate.ToShortDateString());
				gridPat.Rows.Add(row);
			}
			//Primary provider (very useful in dental schools)
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TablePatient","Primary Provider"));
			row.Cells.Add(Providers.GetNameLF(Patients.GetProvNum(PatCur)));
			gridPat.Rows.Add(row);
			//Language
			if(PatCur.Language!=""){
				row=new ODGridRow();
				row.Cells.Add(Lan.g("TablePatient","Language"));
				row.Cells.Add(CultureInfo.GetCultureInfo(PatCur.Language).DisplayName);
				gridPat.Rows.Add(row);
			}
			//Referrals
			RefAttach[] RefList=RefAttaches.Refresh(PatCur.PatNum);
			if(RefList.Length==0){
				row=new ODGridRow();
				row.Cells.Add(Lan.g("TablePatient","Referrals"));
				row.Cells.Add(Lan.g("TablePatient","None"));
				row.Tag="Referral";
				gridPat.Rows.Add(row);
			}
			for(int i=0;i<RefList.Length;i++) {
				row=new ODGridRow();
				if(RefList[i].IsFrom){
					row.Cells.Add(Lan.g("TablePatient","Referred From"));
				}
				else{
					row.Cells.Add(Lan.g("TablePatient","Referred To"));
				}
				try{
					row.Cells.Add(Referrals.GetNameLF(RefList[i].ReferralNum)+"\r\n"
						+Referrals.GetPhone(RefList[i].ReferralNum)
						+" "+RefList[i].Note);
				}
				catch{
					row.Cells.Add("");//if referral is null because using random keys and had bug.
				}
				row.Tag="Referral";
				gridPat.Rows.Add(row);
			}
			//AddrNote
			if(PatCur.AddrNote!=""){
				row=new ODGridRow();
				row.Cells.Add(Lan.g("TablePatient","Addr/Ph Note"));
				row.Cells.Add(PatCur.AddrNote);
				row.ColorText=Color.Red;
				row.Bold=true;
				gridPat.Rows.Add(row);
			}
			//PatFields-------------------------------------------
			PatField field;
			for(int i=0;i<PatFieldDefs.List.Length;i++){
				row=new ODGridRow();
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
			gridPat.EndUpdate();
		}

		#endregion gridPatient 

		#region tbFamily

		private void FillFamilyData(){
			if(PatCur==null){
				tbFamily.SelectedRow=-1;
				tbFamily.ResetRows(0);
				tbFamily.LayoutTables();
				return;
			}
			tbFamily.ResetRows(FamCur.List.Length);
			tbFamily.SetGridColor(Color.Gray);
			tbFamily.SetBackGColor(Color.White);
			for(int i=0;i<FamCur.List.Length; i++){
				if(FamCur.List[i].PatNum==FamCur.List[i].Guarantor){
					for(int j=0;j<5;j++){
						tbFamily.FontBold[j,i]=true;
					}
					//tbFamily.Cell[0,i]=Lan.g(this,"Guar");
				}
				tbFamily.Cell[0,i]=FamCur.GetNameInFamLFI(i);
				tbFamily.Cell[1,i]=Lan.g("enumPatientPosition",FamCur.List[i].Position.ToString());
				tbFamily.Cell[2,i]=Lan.g("enumPatientGender",FamCur.List[i].Gender.ToString());
				tbFamily.Cell[3,i]=Lan.g("enumPatientStatus",FamCur.List[i].PatStatus.ToString());
				tbFamily.Cell[4,i]=Shared.AgeToString(FamCur.List[i].Age);
				for(int j=0;j<RecallList.Length;j++){
					if(RecallList[j].PatNum==FamCur.List[i].PatNum){
						if(RecallList[j].DateDue.Year>1880){
							tbFamily.Cell[5,i]=RecallList[j].DateDue.ToShortDateString();
						}
					}
				}
				if(FamCur.List[i].PatNum==PatCur.PatNum){
					tbFamily.SelectedRow=i;
					tbFamily.ColorRow(i,Color.DarkSalmon);
				}
			}//end for
			tbFamily.LayoutTables();
		}//end FillFamilyData

		private void tbFamily_CellClicked(object sender, CellEventArgs e){
			if (tbFamily.SelectedRow != -1){
				tbFamily.ColorRow(tbFamily.SelectedRow,Color.White);
			}
			tbFamily.SelectedRow=e.Row;
			tbFamily.ColorRow(e.Row,Color.DarkSalmon);
			OnPatientSelected(FamCur.List[e.Row].PatNum);
			ModuleSelected(FamCur.List[e.Row].PatNum);
		}

		//private void butAddPt_Click(object sender, System.EventArgs e) {
		private void OnAdd_Click(){
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
			Patients.Insert(tempPat,false);
			FormPatientEdit FormPE=new FormPatientEdit(tempPat,FamCur);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				OnPatientSelected(tempPat.PatNum);
				ModuleSelected(tempPat.PatNum);
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		//private void butDeletePt_Click(object sender, System.EventArgs e) {
		private void OnDelete_Click(){
			//this doesn't actually delete the patient, just changes their status
			//and they will never show again in the patient selection list.
			//check for plans, appointments, procedures, etc.
			Procedure[] procList=Procedures.Refresh(PatCur.PatNum);
			Claims.Refresh(PatCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(PatCur.PatNum);
			PaySplit[] PaySplitList=PaySplits.Refresh(PatCur.PatNum);//
			ClaimProc[] claimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			Commlog[] commlogList=Commlogs.Refresh(PatCur.PatNum);
			PayPlan[] payPlanList=PayPlans.Refresh(PatCur.Guarantor,PatCur.PatNum);
			InsPlan[] planList=InsPlans.Refresh(FamCur);
			PatPlanList=PatPlans.Refresh(PatCur.PatNum);
			//CovPats.Refresh(planList,PatPlanList);
			RefAttach[] RefAttachList=RefAttaches.Refresh(PatCur.PatNum);
			bool hasProcs=procList.Length>0;
			bool hasClaims=Claims.List.Length>0;
			bool hasAdj=AdjustmentList.Length>0;
			bool hasPay=PaySplitList.Length>0;
			bool hasClaimProcs=claimProcList.Length>0;
			bool hasComm=commlogList.Length>0;
			bool hasPayPlans=payPlanList.Length>0;
			bool hasInsPlans=false;
			for(int i=0;i<planList.Length;i++){
				if(planList[i].Subscriber==PatCur.PatNum){
					hasInsPlans=true;
				}
			}
			bool hasRef=RefAttachList.Length>0;
			if(hasProcs || hasClaims || hasAdj || hasPay || hasClaimProcs || hasComm || hasPayPlans
				|| hasInsPlans || hasRef)
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
				MessageBox.Show(message);
				return;
			}
			Patient PatOld=PatCur.Copy();
			if(PatCur.PatNum==PatCur.Guarantor){//if selecting guarantor
				if(FamCur.List.Length==1){
					if(!MsgBox.Show(this,true,"Delete Patient?"))
						return;
					PatCur.PatStatus=PatientStatus.Deleted;
					PatCur.ChartNumber="";
					Patients.Update(PatCur,PatOld);
					for(int i=0;i<RecallList.Length;i++){
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
				for(int i=0;i<RecallList.Length;i++){
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
    private void OnGuarantor_Click(){
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
		private void OnMove_Click(){
			Patient PatOld=PatCur.Copy();
			//Patient PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){//if guarantor selected
				if(FamCur.List.Length==1){//and no other family members
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
			if(InsPlans.GetListForSubscriber(subscriber.PatNum).Length==0){
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
				plan.DedBeforePerc=PrefB.GetBool("DeductibleBeforePercentAsDefault");
				plan.PlanType="";
				InsPlans.Insert(plan);
				Benefit ben;
				for(int i=0;i<CovCatB.ListShort.Length;i++){
					if(CovCatB.ListShort[i].DefaultPercent==-1){
						continue;
					}
					ben=new Benefit();
					ben.BenefitType=InsBenefitType.Percentage;
					ben.CovCatNum=CovCatB.ListShort[i].CovCatNum;
					ben.PlanNum=plan.PlanNum;
					ben.Percent=CovCatB.ListShort[i].DefaultPercent;
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					ben.CodeNum=0;
					Benefits.Insert(ben);
				}
			}
			//Then attach plan------------------------------------------------------------------------------------------------
			PatPlan patplan=new PatPlan();
			patplan.Ordinal=PatPlanList.Length+1;//so the ordinal of the first entry will be 1, NOT 0.
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
			if(PatPlanList.Length==0){
				gridIns.BeginUpdate();
				gridIns.Columns.Clear();
				gridIns.Rows.Clear();
				gridIns.EndUpdate();
				return;
			}
			InsPlan[] planArray=new InsPlan[PatPlanList.Length];//prevents repeated calls to db.
			for(int i=0;i<PatPlanList.Length;i++){
				planArray[i]=InsPlans.GetPlan(PatPlanList[i].PlanNum,PlanList);
			}
			gridIns.BeginUpdate();
			gridIns.Columns.Clear();
			gridIns.Rows.Clear();
			OpenDental.UI.ODGridColumn col;
			col=new ODGridColumn("",150);
			gridIns.Columns.Add(col);
			for(int i=0;i<PatPlanList.Length;i++){
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
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(FamCur.GetNameInFamFL(planArray[i].Subscriber));
			}
			row.ColorBackG=DefB.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//subscriber ID
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Subscriber ID"));
			for(int i=0;i<PatPlanList.Length;i++) {
				row.Cells.Add(planArray[i].SubscriberID);
			}
			row.ColorBackG=DefB.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//relationship
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Rel'ship to Sub"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(Lan.g("enumRelat",PatPlanList[i].Relationship.ToString()));
			}
			row.ColorBackG=DefB.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//patient ID
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Patient ID"));
			for(int i=0;i<PatPlanList.Length;i++){
				row.Cells.Add(PatPlanList[i].PatID);
			}
			row.ColorBackG=DefB.Long[(int)DefCat.MiscColors][0].ItemColor;
			gridIns.Rows.Add(row);
			//pending
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Pending"));
			for(int i=0;i<PatPlanList.Length;i++){
				if(PatPlanList[i].IsPending){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
			}
			row.ColorBackG=DefB.Long[(int)DefCat.MiscColors][0].ItemColor;
			row.ColorLborder=Color.Black;
			gridIns.Rows.Add(row);
			//employer
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Employer"));
			for(int i=0;i<PatPlanList.Length;i++) {
				row.Cells.Add(Employers.GetName(planArray[i].EmployerNum));
			}
			gridIns.Rows.Add(row);
			//carrier
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Carrier"));
			for(int i=0;i<PatPlanList.Length;i++) {
				row.Cells.Add(InsPlans.GetCarrierName(PatPlanList[i].PlanNum,planArray));
			}
			gridIns.Rows.Add(row);
			//plan type
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Type"));
			for(int i=0;i<planArray.Length;i++) {
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
			for(int i=0;i<planArray.Length;i++) {
				row.Cells.Add(DefB.GetName(DefCat.FeeSchedNames,planArray[i].FeeSched));
			}
			row.ColorLborder=Color.Black;
			gridIns.Rows.Add(row);
			//Benefits-----------------------------------------------------------------------------------------------------
			Benefit[] bensForPat=Benefits.Refresh(PatPlanList);
			Benefit[,] benMatrix=Benefits.GetDisplayMatrix(bensForPat,PatPlanList);
			string desc;
			string val;
			for(int y=0;y<benMatrix.GetLength(1);y++){//rows
				row=new ODGridRow();
				desc="";
				//some of the columns might be null, but at least one will not be.  Find it.
				for(int x=0;x<benMatrix.GetLength(1);x++){//columns
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
					if(benMatrix[x,y].BenefitType==InsBenefitType.Percentage){
						desc+=CovCats.GetDesc(benMatrix[x,y].CovCatNum)+" % ";
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
					if(benMatrix[x,y].BenefitType==InsBenefitType.Percentage) {
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
					}
					if(benMatrix[x,y].QuantityQualifier==BenefitQuantity.NumberOfServices){//eg 2 times per CalendarYear
						val+=benMatrix[x,y].Quantity.ToString()+" "+Lan.g(this,"times per")+" "
							+Lan.g("enumBenefitQuantity",benMatrix[x,y].TimePeriod.ToString())+" ";
					}
					else if(benMatrix[x,y].QuantityQualifier==BenefitQuantity.Months) {//eg Every 2 months
						val+="Every "+benMatrix[x,y].Quantity.ToString()+" month";
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
					if(benMatrix[x,y].MonetaryAmt!=0){
						val+=benMatrix[x,y].MonetaryAmt.ToString("c0")+" ";
					}
					if(val==""){
						val="val";
					}
					row.Cells.Add(val);
				}
				gridIns.Rows.Add(row);
			}
			//Plan note
			row=new ODGridRow();
			row.Cells.Add(Lan.g("TableCoverage","Ins Plan Note"));
			OpenDental.UI.ODGridCell cell;
			for(int i=0;i<PatPlanList.Length;i++){
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
			for(int i=0;i<PatPlanList.Length;i++) {
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
