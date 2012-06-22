using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using Acrobat;
using AFORMAUTLib;//Acrobat forms

namespace OpenDental {
	public partial class FormSheetImport:Form {
		public Sheet SheetCur;
		public Document DocCur;
		private List<SheetImportRow> Rows;
		private Patient Pat;
		private Family Fam;
		///<summary>We must have a readily available bool, whether or not this checkbox field is present on the sheet.  It gets set at the very beginning, then gets changes based on user input on the sheet and in this window.</summary>
		private bool AddressSameForFam;
		private InsPlan Plan1;
		private InsPlan Plan2;
		private List<PatPlan> PatPlanList;
		private List<InsPlan> PlanList;
		private PatPlan PatPlan1;
		private PatPlan PatPlan2;
		private Relat? Ins1Relat;
		private Relat? Ins2Relat;
		private Carrier Carrier1;
		private Carrier Carrier2;
		private Dictionary<string,string> DictAcrobatFields;
		private List<InsSub> SubList;
		private InsSub Sub1;
		private InsSub Sub2;

		public FormSheetImport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetImport_Load(object sender,EventArgs e) {
			if(SheetCur!=null) {
				Pat=Patients.GetPat(SheetCur.PatNum);
			}
			else {
				throw new NotImplementedException();//js this broke with the move to dot net 4.0.
				/*
				pat=Patients.GetPat(DocCur.PatNum);
				CAcroApp acroApp=null;
				try {
					acroApp=new AcroAppClass();//Initialize Acrobat by creating App object
				}
				catch {
					MsgBox.Show(this,"Requires Acrobat 9 Pro to be installed on this computer.");
					DialogResult=DialogResult.Cancel;
					return;
				}
				//acroApp.Show();// Show Acrobat Viewer
				//acroApp.Hide();//This is annoying if Acrobat is already open for some other reason.
				CAcroAVDoc avDoc=new AcroAVDocClass();
				string pathToPdf=CodeBase.ODFileUtils.CombinePaths(ImageStore.GetPatientFolder(pat),DocCur.FileName);
				if(!avDoc.Open(pathToPdf,"")){
					MessageBox.Show(Lan.g(this,"Could not open")+" "+pathToPdf);
					DialogResult=DialogResult.Cancel;
					return;
				}
				IAFormApp formApp=new AFormAppClass();//Create a IAFormApp object so we can access the form fields in the open document
				IFields myFields=(IFields)formApp.Fields;// Get the IFields object associated with the form
				IEnumerator myEnumerator = myFields.GetEnumerator();// Get the IEnumerator object for myFields
				dictAcrobatFields=new Dictionary<string,string>();
				IField myField;
				string nameClean;
				string valClean;
				while(myEnumerator.MoveNext()) {
					myField=(IField)myEnumerator.Current;// Get the IField object
					if(myField.Value==null){
						continue;
					}
					//if the form was designed in LiveCycle, the names will look like this: topmostSubform[0].page1[0].SSN[0]
					//Whereas, if it was designed in Acrobat, the names will look like this: SSN
					//So...
					nameClean=myField.Name;
					if(nameClean.Contains("[") && nameClean.Contains(".")) {
						nameClean=nameClean.Substring(nameClean.LastIndexOf(".")+1);
						nameClean=nameClean.Substring(0,nameClean.IndexOf("["));
					}
					if(nameClean=="misc") {
						int suffix=1;
						nameClean=nameClean+suffix.ToString();
						while(dictAcrobatFields.ContainsKey(nameClean)) {//untested.
							suffix++;
							nameClean=nameClean+suffix.ToString();
						}
					}
					valClean=myField.Value;
					if(valClean=="Off") {
						valClean="";
					}
					//myField.Type//possible values include text,radiobutton,checkbox
					//MessageBox.Show("Raw:"+myField.Name+"  Name:"+nameClean+"  Value:"+myField.Value);
					if(dictAcrobatFields.ContainsKey(nameClean)) {
						continue;
					}
					dictAcrobatFields.Add(nameClean,valClean);
					//name:topmostSubform[0].page1[0].SSN[0]
				}
				//acroApp.Hide();//Doesn't work well enough
				//this.BringToFront();//Doesn't work
				//acroApp.Minimize();
				acroApp.Exit();
				acroApp=null;
				*/
			}
			Fam=Patients.GetFamily(Pat.PatNum);
			AddressSameForFam=true;
			for(int i=0;i<Fam.ListPats.Length;i++) {
				if(Pat.HmPhone!=Fam.ListPats[i].HmPhone
					|| Pat.Address!=Fam.ListPats[i].Address
					|| Pat.Address2!=Fam.ListPats[i].Address2
					|| Pat.City!=Fam.ListPats[i].City
					|| Pat.State!=Fam.ListPats[i].State
					|| Pat.Zip!=Fam.ListPats[i].Zip) 
				{
					AddressSameForFam=false;
					break;
				}
			}
			PatPlanList=PatPlans.Refresh(Pat.PatNum);
			SubList=InsSubs.RefreshForFam(Fam);
			PlanList=InsPlans.RefreshForSubList(SubList);
			if(PatPlanList.Count==0) {
				PatPlan1=null;
				Plan1=null;
				Sub1=null;
				Ins1Relat=null;
				Carrier1=null;
			}
			else {
				PatPlan1=PatPlanList[0];
				Sub1=InsSubs.GetSub(PatPlan1.InsSubNum,SubList);
				Plan1=InsPlans.GetPlan(Sub1.PlanNum,PlanList);
				Ins1Relat=PatPlan1.Relationship;
				Carrier1=Carriers.GetCarrier(Plan1.CarrierNum);
			}
			if(PatPlanList.Count<2) {
				PatPlan2=null;
				Plan2=null;
				Sub2=null;
				Ins2Relat=null;
				Carrier2=null;
			}
			else {
				PatPlan2=PatPlanList[1];
				Sub2=InsSubs.GetSub(PatPlan2.InsSubNum,SubList);
				Plan2=InsPlans.GetPlan(Sub2.PlanNum,PlanList);
				Ins2Relat=PatPlan2.Relationship;
				Carrier2=Carriers.GetCarrier(Plan2.CarrierNum);
			}
			FillRows();
			FillGrid();
		}

		///<summary>This can only be run once when the form first opens.  After that, the rows are just edited.</summary>
		private void FillRows() {
			#region Patient Form
			if(SheetCur.SheetType==SheetTypeEnum.PatientForm) {
				Rows=new List<SheetImportRow>();
				SheetImportRow row;
				string fieldVal;
				Rows.Add(CreateSeparator("Personal"));
				#region personal
				//LName---------------------------------------------
				fieldVal=GetInputValue("LName");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("LName","",Pat.LName,Pat.LName,fieldVal,fieldVal,fieldVal,fieldVal,Pat.LName!=fieldVal,false,typeof(string),false,false));
				}
				//FName---------------------------------------------
				fieldVal=GetInputValue("FName");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("FName","",Pat.FName,Pat.FName,fieldVal,fieldVal,fieldVal,fieldVal,Pat.FName!=fieldVal,false,typeof(string),false,false));
				}
				//MiddleI---------------------------------------------
				fieldVal=GetInputValue("MiddleI");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("MiddleI","",Pat.MiddleI,Pat.MiddleI,fieldVal,fieldVal,fieldVal,fieldVal,Pat.MiddleI!=fieldVal,false,typeof(string),false,false));
				}
				//Preferred---------------------------------------------
				fieldVal=GetInputValue("Preferred");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("Preferred","",Pat.Preferred,Pat.Preferred,fieldVal,fieldVal,fieldVal,fieldVal,
						Pat.Preferred!=fieldVal,false,typeof(string),false,false));
				}
				//Gender---------------------------------------------
				fieldVal=GetRadioValue("Gender");
				if(fieldVal!=null) {//field exists on form
					string newValDisplay="";
					object newValObj=null;
					if(fieldVal!="") {
						try {
							PatientGender gender=(PatientGender)Enum.Parse(typeof(PatientGender),fieldVal);
							newValDisplay=Lan.g("enumPatientGender",gender.ToString());
							newValObj=gender;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid gender."));
						}
					}
					Rows.Add(CreateImportRow("Gender","",Lan.g("enumPatientGender",Pat.Gender.ToString()),Pat.Gender,
						newValDisplay,newValObj,newValDisplay,newValObj,newValObj!=null && (PatientGender)newValObj!=Pat.Gender,false,typeof(PatientGender),false,false));
				}
				//Position---------------------------------------------
				fieldVal=GetRadioValue("Position");
				if(fieldVal!=null) {//field exists on form
					string newValDisplay="";
					object newValObj=null;
					if(fieldVal!="") {
						try {
							PatientPosition position=(PatientPosition)Enum.Parse(typeof(PatientPosition),fieldVal);
							newValDisplay=Lan.g("enumPatientPosition",position.ToString());
							newValObj=position;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid PatientPosition."));
						}
					}
					Rows.Add(CreateImportRow("Position","",Lan.g("enumPatientPositionr",Pat.Position.ToString()),Pat.Position,
						newValDisplay,newValObj,newValDisplay,newValObj,newValObj!=null && (PatientPosition)newValObj!=Pat.Position,false,typeof(PatientPosition),false,false));
				}
				//Birthdate---------------------------------------------
				fieldVal=GetInputValue("Birthdate");
				if(fieldVal!=null) {
					string oldValDisplay="";
					string newValDisplay="";
					object newValObj=null;
					if(Pat.Birthdate.Year>1880) {
						oldValDisplay=Pat.Birthdate.ToShortDateString();
					}
					newValObj=PIn.Date(fieldVal);
					if(((DateTime)newValObj).Year>1880) {
						newValDisplay=((DateTime)newValObj).ToShortDateString();
					}
					Rows.Add(CreateImportRow("Birthdate","",oldValDisplay,Pat.Birthdate,newValDisplay,newValObj,newValDisplay,newValObj,
						oldValDisplay!=newValDisplay,false,typeof(DateTime),false,false));
				}
				//SSN---------------------------------------------
				fieldVal=GetInputValue("SSN");
				if(fieldVal!=null) {
					string newValDisplay=fieldVal.Replace("-","");//quickly strip dashes
					Rows.Add(CreateImportRow("SSN","",Pat.SSN,Pat.SSN,newValDisplay,newValDisplay,newValDisplay,newValDisplay,
						Pat.SSN!=newValDisplay,false,typeof(string),false,false));
				}
				//WkPhone---------------------------------------------
				fieldVal=GetInputValue("WkPhone");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("WkPhone","",Pat.WkPhone,Pat.WkPhone,fieldVal,fieldVal,fieldVal,fieldVal,Pat.WkPhone!=fieldVal,false,typeof(string),false,false));
				}
				//WirelessPhone---------------------------------------------
				fieldVal=GetInputValue("WirelessPhone");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("WirelessPhone","",Pat.WirelessPhone,Pat.WirelessPhone,fieldVal,fieldVal,fieldVal,fieldVal,
						Pat.WirelessPhone!=fieldVal,false,typeof(string),false,false));
				}
				//wirelessCarrier---------------------------------------------
				fieldVal=GetInputValue("wirelessCarrier");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("wirelessCarrier","","","",fieldVal,fieldVal,fieldVal,fieldVal,false,false,typeof(string),true,false));
				}
				//Email---------------------------------------------
				fieldVal=GetInputValue("Email");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("Email","",Pat.Email,Pat.Email,fieldVal,fieldVal,fieldVal,fieldVal,Pat.Email!=fieldVal,false,typeof(string),false,false));
				}
				//PreferContactMethod---------------------------------------------
				fieldVal=GetRadioValue("PreferContactMethod");
				if(fieldVal!=null) {
					string oldValDisplay=Lan.g("enumContactMethod",Pat.PreferContactMethod.ToString());
					string newValDisplay="";
					object newValObj=null;
					if(fieldVal!="") {
						try {
							ContactMethod cmeth=(ContactMethod)Enum.Parse(typeof(ContactMethod),fieldVal);
							newValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
							newValObj=cmeth;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid ContactMethod."));
						}
					}
					Rows.Add(CreateImportRow("PreferContactMethod","",oldValDisplay,Pat.PreferContactMethod,newValDisplay,newValObj,newValDisplay,newValObj,
						newValObj!=null && (ContactMethod)newValObj!=Pat.PreferContactMethod,false,typeof(ContactMethod),false,false));
				}
				//PreferConfirmMethod---------------------------------------------
				fieldVal=GetRadioValue("PreferConfirmMethod");
				if(fieldVal!=null) {
					string oldValDisplay=Lan.g("enumContactMethod",Pat.PreferConfirmMethod.ToString());
					string newValDisplay="";
					object newValObj=null;
					if(fieldVal!="") {
						try {
							ContactMethod cmeth=(ContactMethod)Enum.Parse(typeof(ContactMethod),fieldVal);
							newValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
							newValObj=cmeth;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid ContactMethod."));
						}
					}
					Rows.Add(CreateImportRow("PreferConfirmMethod","",oldValDisplay,Pat.PreferConfirmMethod,newValDisplay,newValObj,newValDisplay,newValObj,
						newValObj!=null && (ContactMethod)newValObj!=Pat.PreferConfirmMethod,false,typeof(ContactMethod),false,false));
				}
				//PreferRecallMethod---------------------------------------------
				fieldVal=GetRadioValue("PreferRecallMethod");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					string oldValDisplay=Lan.g("enumContactMethod",Pat.PreferRecallMethod.ToString());
					string newValDisplay="";
					object newValObj=null;
					if(fieldVal!="") {
						try {
							ContactMethod cmeth=(ContactMethod)Enum.Parse(typeof(ContactMethod),fieldVal);
							row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
							row.NewValObj=cmeth;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid ContactMethod."));
						}
					}
					Rows.Add(CreateImportRow("PreferRecallMethod","",oldValDisplay,Pat.PreferRecallMethod,newValDisplay,newValObj,newValDisplay,newValObj,
						newValObj!=null && (ContactMethod)newValObj!=Pat.PreferRecallMethod,false,typeof(ContactMethod),false,false));
				}
				//referredFrom---------------------------------------------
				fieldVal=GetInputValue("referredFrom");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="referredFrom";
					Referral refer=Referrals.GetReferralForPat(Pat.PatNum);
					if(refer==null) {//there was no existing referral
						row.OldValDisplay="";
						row.OldValObj=null;
						row.NewValDisplay=fieldVal;
						row.NewValObj=null;
						if(row.NewValDisplay!="") {//user did enter a referral
							row.ImpValDisplay=Lan.g(this,"[double click to pick]");
							row.ImpValObj=null;
							row.IsFlaggedImp=true;
							row.DoImport=false;//this will change to true after they pick a referral
						}
						else {//user still did not enter a referral
							row.ImpValDisplay="";
							row.ImpValObj=null;
							row.DoImport=false;
						}
					}
					else {//there was an existing referral. We don't allow changing from here since mostly for new patients.
						row.OldValDisplay=refer.GetNameFL();
						row.OldValObj=refer;
						row.NewValDisplay=fieldVal;
						row.NewValObj=null;
						row.ImpValDisplay="";
						row.ImpValObj=null;
						row.DoImport=false;
						if(row.OldValDisplay!=row.NewValDisplay) {//if patient changed an existing referral, at least let user know.
							row.IsFlagged=true;//although they won't be able to do anything about it here
						}
					}
					row.ObjType=typeof(Referral);
					Rows.Add(row);
				}
				#endregion personal
				//Separator-------------------------------------------
				Rows.Add(CreateSeparator("Address and Home Phone"));
				#region address
				//SameForEntireFamily-------------------------------------------
				if(ContainsOneOfFields("addressAndHmPhoneIsSameEntireFamily")) {
					row=new SheetImportRow();
					row.FieldName="addressAndHmPhoneIsSameEntireFamily";
					row.FieldDisplay="Same for entire family";
					if(AddressSameForFam) {//remember we calculated this in the form constructor.
						row.OldValDisplay="X";
						row.OldValObj="X";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					//And now, we will revise AddressSameForFam based on user input
					AddressSameForFam=IsChecked("addressAndHmPhoneIsSameEntireFamily");
					if(AddressSameForFam) {
						row.NewValDisplay="X";
						row.NewValObj="X";
						row.ImpValDisplay="X";
						row.ImpValObj="X";
					}
					else {
						row.NewValDisplay="";
						row.NewValObj="";
						row.ImpValDisplay="";
						row.ImpValObj="";
					}
					row.ObjType=typeof(string);
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.DoImport=true;
					}
					Rows.Add(row);
				}
				//Address---------------------------------------------
				fieldVal=GetInputValue("Address");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("Address","",Pat.Address,Pat.Address,fieldVal,fieldVal,fieldVal,fieldVal,Pat.Address!=fieldVal,false,typeof(string),false,false));
				}
				//Address2---------------------------------------------
				fieldVal=GetInputValue("Address2");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("Address2","",Pat.Address2,Pat.Address2,fieldVal,fieldVal,fieldVal,fieldVal,Pat.Address2!=fieldVal,false,typeof(string),false,false));
				}
				//City---------------------------------------------
				fieldVal=GetInputValue("City");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("City","",Pat.City,Pat.City,fieldVal,fieldVal,fieldVal,fieldVal,Pat.City!=fieldVal,false,typeof(string),false,false));
				}
				//State---------------------------------------------
				fieldVal=GetInputValue("State");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("State","",Pat.State,Pat.State,fieldVal,fieldVal,fieldVal,fieldVal,Pat.State!=fieldVal,false,typeof(string),false,false));
				}
				//Zip---------------------------------------------
				fieldVal=GetInputValue("Zip");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("Zip","",Pat.Zip,Pat.Zip,fieldVal,fieldVal,fieldVal,fieldVal,Pat.Zip!=fieldVal,false,typeof(string),false,false));
				}
				//HmPhone---------------------------------------------
				fieldVal=GetInputValue("HmPhone");
				if(fieldVal!=null) {
					Rows.Add(CreateImportRow("HmPhone","",Pat.HmPhone,Pat.HmPhone,fieldVal,fieldVal,fieldVal,fieldVal,Pat.HmPhone!=fieldVal,false,typeof(string),false,false));
				}
				#endregion address
				//Separator-------------------------------------------
				Rows.Add(CreateSeparator("Insurance Policy 1"));
				#region ins1
				//It turns out that importing insurance is crazy complicated if we want it to be perfect.
				//So it's better to table that plan for now.
				//The new strategy is simply to show them what the user entered and notify them if it seems different.
				//ins1Relat------------------------------------------------------------
				fieldVal=GetRadioValue("ins1Relat");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1Relat";
					row.FieldDisplay="Relationship";
					row.OldValDisplay=Lan.g("enumRelat",Ins1Relat.ToString());
					row.OldValObj=Ins1Relat;
					if(fieldVal=="") {
						row.NewValDisplay="";
						row.NewValObj=null;
					}
					else {
						try {
							Relat relat=(Relat)Enum.Parse(typeof(Relat),fieldVal);
							row.NewValDisplay=Lan.g("enumRelat",relat.ToString());
							row.NewValObj=relat;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid Relationship."));
						}
					}
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1Subscriber---------------------------------------------
				fieldVal=GetInputValue("ins1SubscriberNameF");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1Subscriber";
					row.FieldDisplay="Subscriber";
					if(Plan1!=null) {
						row.OldValDisplay=Fam.GetNameInFamFirst(Sub1.Subscriber);
						row.OldValObj=Sub1.Subscriber;
					}
					else {
						row.OldValDisplay="";
						row.OldValObj=null;
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj=row.NewValDisplay;
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1SubscriberID---------------------------------------------
				fieldVal=GetInputValue("ins1SubscriberID");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1SubscriberID";
					row.FieldDisplay="Subscriber ID";
					if(Plan1!=null) {
						row.OldValDisplay=Sub1.SubscriberID;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1CarrierName---------------------------------------------
				fieldVal=GetInputValue("ins1CarrierName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1CarrierName";
					row.FieldDisplay="Carrier";
					if(Carrier1!=null) {
						row.OldValDisplay=Carrier1.CarrierName;
						row.OldValObj=Carrier1;
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1CarrierPhone---------------------------------------------
				fieldVal=GetInputValue("ins1CarrierPhone");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1CarrierPhone";
					row.FieldDisplay="Phone";
					if(Carrier1!=null) {
						row.OldValDisplay=Carrier1.Phone;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1EmployerName---------------------------------------------
				fieldVal=GetInputValue("ins1EmployerName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1EmployerName";
					row.FieldDisplay="Employer";
					if(Plan1==null) {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					else {
						row.OldValDisplay=Employers.GetName(Plan1.EmployerNum);
						row.OldValObj=Employers.GetEmployer(Plan1.EmployerNum);
					}
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1GroupName---------------------------------------------
				fieldVal=GetInputValue("ins1GroupName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1GroupName";
					row.FieldDisplay="Group Name";
					if(Plan1!=null) {
						row.OldValDisplay=Plan1.GroupName;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins1GroupNum---------------------------------------------
				fieldVal=GetInputValue("ins1GroupNum");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1GroupNum";
					row.FieldDisplay="Group Num";
					if(Plan1!=null) {
						row.OldValDisplay=Plan1.GroupNum;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				#endregion ins1
				//Separator-------------------------------------------
				Rows.Add(CreateSeparator("Insurance Policy 2"));
				#region ins2
				//It turns out that importing insurance is crazy complicated if want it to be perfect.
				//So it's better to table that plan for now.
				//The new strategy is simply to show them what the user entered and notify them if it seems different.
				//ins2Relat------------------------------------------------------------
				fieldVal=GetRadioValue("ins2Relat");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2Relat";
					row.FieldDisplay="Relationship";
					row.OldValDisplay=Lan.g("enumRelat",Ins2Relat.ToString());
					row.OldValObj=Ins2Relat;
					if(fieldVal=="") {
						row.NewValDisplay="";
						row.NewValObj=null;
					}
					else {
						try {
							Relat relat=(Relat)Enum.Parse(typeof(Relat),fieldVal);
							row.NewValDisplay=Lan.g("enumRelat",relat.ToString());
							row.NewValObj=relat;
						}
						catch {
							MessageBox.Show(fieldVal+Lan.g(this," is not a valid Relationship."));
						}
					}
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2Subscriber---------------------------------------------
				fieldVal=GetInputValue("ins2SubscriberNameF");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2Subscriber";
					row.FieldDisplay="Subscriber";
					if(Plan2!=null) {
						row.OldValDisplay=Fam.GetNameInFamFirst(Sub2.Subscriber);
						row.OldValObj=Sub2.Subscriber;
					}
					else {
						row.OldValDisplay="";
						row.OldValObj=null;
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj=row.NewValDisplay;
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2SubscriberID---------------------------------------------
				fieldVal=GetInputValue("ins2SubscriberID");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2SubscriberID";
					row.FieldDisplay="Subscriber ID";
					if(Plan2!=null) {
						row.OldValDisplay=Sub2.SubscriberID;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2CarrierName---------------------------------------------
				fieldVal=GetInputValue("ins2CarrierName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2CarrierName";
					row.FieldDisplay="Carrier";
					if(Carrier2!=null) {
						row.OldValDisplay=Carrier2.CarrierName;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2CarrierPhone---------------------------------------------
				fieldVal=GetInputValue("ins2CarrierPhone");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2CarrierPhone";
					row.FieldDisplay="Phone";
					if(Carrier2!=null) {
						row.OldValDisplay=Carrier2.Phone;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2EmployerName---------------------------------------------
				fieldVal=GetInputValue("ins2EmployerName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2EmployerName";
					row.FieldDisplay="Employer";
					if(Plan2==null) {
						row.OldValDisplay="";
					}
					else {
						row.OldValDisplay=Employers.GetName(Plan2.EmployerNum);
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2GroupName---------------------------------------------
				fieldVal=GetInputValue("ins2GroupName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2GroupName";
					row.FieldDisplay="Group Name";
					if(Plan2!=null) {
						row.OldValDisplay=Plan2.GroupName;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				//ins2GroupNum---------------------------------------------
				fieldVal=GetInputValue("ins2GroupNum");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2GroupNum";
					row.FieldDisplay="Group Num";
					if(Plan2!=null) {
						row.OldValDisplay=Plan2.GroupNum;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="[double click to pick]";
					row.ImpValObj=null;
					row.ObjType=typeof(InsPlan);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					row.IsFlaggedImp=true;
					Rows.Add(row);
				}
				#endregion ins2
				//Separator-------------------------------------------
				Rows.Add(CreateSeparator("Misc"));
				//misc----------------------------------------------------
				List<string> miscVals=GetMiscValues();
				for(int i=0;i<miscVals.Count;i++) {
					Rows.Add(CreateImportRow("misc","misc"+(i+1).ToString(),"","",miscVals[i],"","","",false,false,typeof(string),true,false));
				}
			}
			#endregion
			#region Medical History
			else if(SheetCur.SheetType==SheetTypeEnum.MedicalHistory) {
				Rows=new List<SheetImportRow>();
				string fieldVal="";
				List<Allergy> allergies=null;
				List<Medication> meds=null;
				List<Disease> diseases=null;
				SheetImportRow row;
				Rows.Add(CreateSeparator("Allergies"));
				#region Allergies
				//Get list of all the allergy check boxes
				List<SheetField> allergyList=GetSheetFieldsByFieldName("allergy:");
				for(int i=0;i<allergyList.Count;i++) {
					fieldVal="";
					if(i<1) {
						allergies=Allergies.GetAll(Pat.PatNum,true);
					}
					row=new SheetImportRow();
					row.FieldName=allergyList[i].FieldName.Remove(0,8);
					row.OldValDisplay="";
					row.OldValObj=null;
					//Check if allergy exists.
					for(int j=0;j<allergies.Count;j++) {
						if(AllergyDefs.GetDescription(allergies[j].AllergyDefNum)==allergyList[i].FieldName.Remove(0,8)) {
							if(allergies[j].StatusIsActive) {
								row.OldValDisplay="Y";
							}
							else {
								row.OldValDisplay="N";
							}
							row.OldValObj=allergies[j];
							break;
						}
					}
					SheetField oppositeBox=GetOppositeSheetFieldCheckBox(allergyList,allergyList[i]);
					if(allergyList[i].FieldValue=="") {//Current box not checked.
						if(oppositeBox==null || oppositeBox.FieldValue=="") {//No opposite box or both boxes are not checked.
							//Create a blank row just in case they want to import.
							Rows.Add(CreateImportRow(row.FieldName,"",row.OldValDisplay,row.OldValObj,"",allergyList[i],"","",false,false,typeof(Allergy),false,false));
							if(oppositeBox!=null) {
								allergyList.Remove(oppositeBox);//Removes possible duplicate entry.
							}
							continue;
						}
						//Opposite box is checked, figure out if it's a Y or N box.
						if(oppositeBox.RadioButtonValue=="Y") {
							fieldVal="Y";
						}
						else {
							fieldVal="N";
						}
					}
					else {//Current box is checked.  
						if(allergyList[i].RadioButtonValue=="Y") {
							fieldVal="Y";
						}
						else {
							fieldVal="N";
						}
					}
					//Get rid of the opposite check box so field doesn't show up twice.
					if(oppositeBox!=null) {
						allergyList.Remove(oppositeBox);
					}
					row.NewValDisplay=fieldVal;
					row.NewValObj=allergyList[i];
					row.ImpValDisplay=row.NewValDisplay;
					row.ImpValObj=typeof(string);
					row.ObjType=typeof(Allergy);
					if(row.OldValDisplay!=row.NewValDisplay && !(row.OldValDisplay=="" && row.NewValDisplay=="N")) {
						row.DoImport=true;
					}
					Rows.Add(row);
				}
				#endregion
				//Separator-------------------------------------------
				Rows.Add(CreateSeparator("Medications"));
				#region Medications
				List<SheetField> inputMedList=GetSheetFieldsByFieldName("inputMed");
				List<SheetField> checkMedList=GetSheetFieldsByFieldName("checkMed");
				List<SheetField> currentMedList=new List<SheetField>();
				List<SheetField> newMedList=new List<SheetField>();
				for(int i=0;i<inputMedList.Count;i++) {
					if(inputMedList[i].FieldType==SheetFieldType.OutputText) {
						currentMedList.Add(inputMedList[i]);
					}
					else {//User might have tried to type in a new medication they are taking.
						newMedList.Add(inputMedList[i]);
					}
				}
				for(int i=0;i<currentMedList.Count;i++) {
					#region existing medications
					fieldVal="";
					if(i<1) {
						meds=Medications.GetMedicationsByPat(Pat.PatNum);
					}
					row=new SheetImportRow();
					row.FieldName=currentMedList[i].FieldValue;//Will be the name of the drug.
					row.OldValDisplay="N";
					row.OldValObj=null;
					for(int j=0;j<meds.Count;j++) {
						if(Medications.GetDescription(meds[j].MedicationNum)==currentMedList[i].FieldValue) {
							List<MedicationPat> medList=MedicationPats.GetMedicationPatsByMedicationNum(meds[j].MedicationNum,Pat.PatNum);
							for(int k=0;k<medList.Count;k++) {
								//Check if medication is active.
								if(medList[k].DateStop.Year < 1880 || medList[k].DateStop > DateTime.Now) {
									row.OldValDisplay="Y";
								}
							}
							row.OldValObj=meds[j];
							break;
						}
					}
					List<SheetField> relatedChkBoxes=GetRelatedMedicalCheckBoxes(checkMedList,currentMedList[i]);
					for(int j=0;j<relatedChkBoxes.Count;j++) {//Figure out which corresponding checkbox is checked.
						if(relatedChkBoxes[j].FieldValue!="") {//Patient checked this box.
							if(checkMedList[j].RadioButtonValue=="Y") {
								fieldVal="Y";
							}
							else {
								fieldVal="N";
							}
							break;
						}
						//If sheet is only using N boxes and the patient already had this med marked as inactive and then they unchecked the N, so now we need to import it.
						if(relatedChkBoxes.Count==1 && relatedChkBoxes[j].RadioButtonValue=="N" //Only using N boxes for this current medication.
							&& row.OldValObj!=null && row.OldValDisplay=="N"											//Patient has this medication but is currently marked as inactive.
							&& relatedChkBoxes[j].FieldValue=="")																	//Patient unchecked the medication so we activate it again.
						{
							fieldVal="Y";
						}
					}
					if(relatedChkBoxes.Count==1 && relatedChkBoxes[0].RadioButtonValue=="N" && relatedChkBoxes[0].FieldValue=="" && row.OldValDisplay=="N" && row.OldValObj!=null) {
						row.DoImport=true;
					}
					row.NewValDisplay=fieldVal;
					row.NewValObj=currentMedList[i];
					row.ImpValDisplay=row.NewValDisplay;
					row.ImpValObj=typeof(string);
					row.ObjType=typeof(Medication);
					if(row.OldValDisplay!=row.NewValDisplay && row.NewValDisplay!="") {
						row.DoImport=true;
					}
					Rows.Add(row);
					#endregion
				}
				for(int i=0;i<newMedList.Count;i++) {
					#region medications the patient entered
					if(newMedList[i].FieldValue=="") {//No medication entered by patient.
						continue;
					}
					row=new SheetImportRow();
					row.FieldName=newMedList[i].FieldValue;//Whatever the patient typed in...
					row.OldValDisplay="";
					row.OldValObj=null;
					row.NewValDisplay="Y";
					row.NewValObj=newMedList[i];
					row.ImpValDisplay=Lan.g(this,"[double click to pick]");
					row.ImpValObj=new long();
					row.IsFlaggedImp=true;
					row.DoImport=false;//this will change to true after they pick a medication
					row.ObjType=typeof(Medication);
					Rows.Add(row);
					#endregion
				}
				#endregion
				//Separator-------------------------------------------
				Rows.Add(CreateSeparator("Problems"));
				#region Problems
				List<SheetField> problemList=GetSheetFieldsByFieldName("problem:");
				for(int i=0;i<problemList.Count;i++) {
					fieldVal="";
					if(i<1) {
						diseases=Diseases.Refresh(Pat.PatNum,false);
					}
					row=new SheetImportRow();
					row.FieldName=problemList[i].FieldName.Remove(0,8);
					//Figure out the current status of this allergy
					row.OldValDisplay="";
					row.OldValObj=null;
					for(int j=0;j<diseases.Count;j++) {
						if(DiseaseDefs.GetName(diseases[j].DiseaseDefNum)==problemList[i].FieldName.Remove(0,8)) {
							if(diseases[j].ProbStatus==ProblemStatus.Active) {
								row.OldValDisplay="Y";
							}
							else {
								row.OldValDisplay="N";
							}
							row.OldValObj=diseases[j];
							break;
						}
					}
					SheetField oppositeBox=GetOppositeSheetFieldCheckBox(problemList,problemList[i]);
					if(problemList[i].FieldValue=="") {//Current box not checked.
						if(oppositeBox==null || oppositeBox.FieldValue=="") {//No opposite box or both boxes are not checked.
							//Create a blank row just in case they still want to import.
							Rows.Add(CreateImportRow(row.FieldName,"",row.OldValDisplay,row.OldValObj,"",problemList[i],"","",false,false,typeof(Disease),false,false));
							if(oppositeBox!=null) {
								problemList.Remove(oppositeBox);//Removes possible duplicate entry.
							}
							continue;
						}
						//Opposite box is checked, figure out if it's a Y or N box.
						if(oppositeBox.RadioButtonValue=="Y") {
							fieldVal="Y";
						}
						else {
							fieldVal="N";
						}
					}
					else {//Current box is checked.  
						if(problemList[i].RadioButtonValue=="Y") {
							fieldVal="Y";
						}
						else {
							fieldVal="N";
						}
					}
					//Get rid of the opposite check box so field doesn't show up twice.
					if(oppositeBox!=null) {
						problemList.Remove(oppositeBox);
					}
					row.NewValDisplay=fieldVal;
					row.NewValObj=problemList[i];
					row.ImpValDisplay=row.NewValDisplay;
					row.ImpValObj=typeof(string);
					row.ObjType=typeof(Disease);
					if(row.OldValDisplay!=row.NewValDisplay && !(row.OldValDisplay=="" && row.NewValDisplay=="N")) {
						row.DoImport=true;
					}
					Rows.Add(row);
				}
				#endregion
			}
			#endregion 
		}

		private void FillGrid() {
			int scrollVal=gridMain.ScrollValue;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col; 
			col=new ODGridColumn(Lan.g(this,"FieldName"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Current Value"),175);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Entered Value"),175);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Import Value"),175);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Do Import"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			ODGridCell cell;
			for(int i=0;i<Rows.Count;i++) {
				row=new ODGridRow();
				if(Rows[i].IsSeparator) {
					row.Cells.Add(Rows[i].FieldName);
					row.Cells.Add("");
					row.Cells.Add("");
					row.Cells.Add("");
					row.Cells.Add("");
					row.ColorBackG=Color.DarkSlateGray;
					row.ColorText=Color.White;
				}
				else {
					if(Rows[i].FieldDisplay!=null) {
						row.Cells.Add(Rows[i].FieldDisplay);
					}
					else {
						row.Cells.Add(Rows[i].FieldName);
					}
					row.Cells.Add(Rows[i].OldValDisplay);
					cell=new ODGridCell(Rows[i].NewValDisplay);
					if(Rows[i].IsFlagged) {
						cell.ColorText=Color.Firebrick;
						cell.Bold=YN.Yes;
					}
					row.Cells.Add(cell);
					cell=new ODGridCell(Rows[i].ImpValDisplay);
					if(Rows[i].IsFlaggedImp) {
						cell.ColorText=Color.Firebrick;
						cell.Bold=YN.Yes;
					}
					row.Cells.Add(cell);
					if(Rows[i].DoImport) {
						row.Cells.Add("X");
						row.ColorBackG=Color.FromArgb(225,225,225);
					}
					else {
						row.Cells.Add("");
					}
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollValue=scrollVal;
		}

		///<summary>If the specified fieldName does not exist, returns null</summary>
		private string GetInputValue(string fieldName) {
			if(SheetCur!=null) {
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField) {
						continue;
					}
					if(SheetCur.SheetFields[i].FieldName != fieldName) {
						continue;
					}
					return SheetCur.SheetFields[i].FieldValue;
				}
			}
			else {//pdf
				if(DictAcrobatFields.ContainsKey(fieldName)) {
					return DictAcrobatFields[fieldName];
				}
			}
			return null;
		}

		///<summary>If no radiobox with that name exists, returns null.  If no box is checked, it returns empty string.</summary>
		private string GetRadioValue(string fieldName) {
			if(SheetCur!=null) {
				bool fieldFound=false;
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox) {
						continue;
					}
					if(SheetCur.SheetFields[i].FieldName != fieldName) {
						continue;
					}
					fieldFound=true;
					if(SheetCur.SheetFields[i].FieldValue=="X") {
						return SheetCur.SheetFields[i].RadioButtonValue;
					}
				}
				if(fieldFound) {//but no X
					return "";
				}
			}
			else {//pdf
				if(DictAcrobatFields.ContainsKey(fieldName)) {
					return DictAcrobatFields[fieldName];
				}
			}
			return null;
		}

		///<summary>Only the true condition is tested.  If the specified fieldName does not exist, returns false.</summary>
		private bool IsChecked(string fieldName) {
			if(SheetCur!=null) {
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox){
						continue;
					}
					if(SheetCur.SheetFields[i].FieldName != fieldName){
						continue;
					}
					if(SheetCur.SheetFields[i].FieldValue=="X") {
						return true;
					}
				}
			}
			else {
				if(DictAcrobatFields.ContainsKey(fieldName)) {
					if(DictAcrobatFields[fieldName]=="true") {//need to test this
						return true;
					}
				}
			}
			return false;
		}

		///<summary>Returns the values of all the "misc" textbox fields on this form.</summary>
		private List<string> GetMiscValues() {
			List<string> retVal=new List<string>();
			if(SheetCur!=null) {
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField) {
						continue;
					}
					if(SheetCur.SheetFields[i].FieldName != "misc") {
						continue;
					}
					retVal.Add(SheetCur.SheetFields[i].FieldValue);
				}
			}
			else {//pdf
				int suffix=1;
				string keyname="misc"+suffix.ToString();
				while(DictAcrobatFields.ContainsKey(keyname)) {//not rigorously tested
					retVal.Add(DictAcrobatFields[keyname]);
					suffix++;
					keyname="misc"+suffix.ToString();
				}
			}
			return retVal;
		}

		///<summary>Returns all the sheet fields with FieldNames that start with the passed in string.  Only works for check box, input and output fields for now.</summary>
		private List<SheetField> GetSheetFieldsByFieldName(string fieldName) {
			List<SheetField> retVal=new List<SheetField>();
			if(SheetCur!=null) {
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox
						&& SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField
						&& SheetCur.SheetFields[i].FieldType!=SheetFieldType.OutputText) 
					{
						continue;
					}
					if(!SheetCur.SheetFields[i].FieldName.StartsWith(fieldName)) {
						continue;
					}
					retVal.Add(SheetCur.SheetFields[i]);
				}
			}
			return retVal;
		}
		
		///<summary>Returns one sheet field with the same FieldName. Returns null if not found.</summary>
		private SheetImportRow GetImportRowByFieldName(string fieldName) {
			if(Rows==null) {
				return null;
			}
			for(int i=0;i<Rows.Count;i++) {
				if(Rows[i].FieldName==fieldName){
					return Rows[i];
				}
			}
			return null;
		}

		///<summary>Loops through the list passed in returns the opposite check box.  Returns null if one is not found.</summary>
		private SheetField GetOppositeSheetFieldCheckBox(List<SheetField> sheetFieldList,SheetField sheetFieldCur) {
			for(int i=0;i<sheetFieldList.Count;i++) {
				if(sheetFieldList[i].SheetFieldNum==sheetFieldCur.SheetFieldNum) {
					continue;
				}
				//FieldName will be the same.  Ex: allergy:Sudafed 
				if(sheetFieldList[i].FieldName!=sheetFieldCur.FieldName) {
					continue;
				}
				//This has to be the opposite check box.
				return sheetFieldList[i];
			}
			return null;
		}

		///<summary>Returns all checkboxes related to the inputMed passed in.</summary>
		private List<SheetField> GetRelatedMedicalCheckBoxes(List<SheetField> checkMedList,SheetField inputMed) {
			List<SheetField> checkBoxes=new List<SheetField>();
			for(int i=0;i<checkMedList.Count;i++) {
				if(checkMedList[i].FieldName.Remove(0,8)==inputMed.FieldName.Remove(0,8)) {
					checkBoxes.Add(checkMedList[i]);
				}
			}
			return checkBoxes;
		}

		private bool ContainsOneOfFields(params string[] fieldNames) {
			if(SheetCur!=null) {
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox
						&& SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField) 
					{
						continue;
					}
					for(int f=0;f<fieldNames.Length;f++) {
						if(SheetCur.SheetFields[i].FieldName==fieldNames[f]) {
							return true;
						}
					}
				}
			}
			else {
				for(int f=0;f<fieldNames.Length;f++) {
					if(DictAcrobatFields.ContainsKey(fieldNames[f])) {
						return true;
					}
				}
			}
			return false;
		}

		private bool ContainsFieldThatStartsWith(string fieldName) {
			if(SheetCur!=null) {
				for(int i=0;i<SheetCur.SheetFields.Count;i++) {
					if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox
					&& SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField) {
						continue;
					}
					if(SheetCur.SheetFields[i].FieldName.StartsWith(fieldName)) {
						return true;
					}
				}
			}
			else {
				foreach(string fieldkey in DictAcrobatFields.Keys){
					if(fieldkey.StartsWith(fieldName)) {
						return true;
					}
				}
			}
			return false;
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=4) {
				return;
			}
			if(Rows[e.Row].IsSeparator) {
				return;
			}
			if(!IsImportable(Rows[e.Row])) {
				return;
			}
			//Must import all or none of the insurance rows.
			if(Rows[e.Row].ObjType==typeof(InsPlan)) {
				bool isPrimary=true;
				if(Rows[e.Row].FieldName.StartsWith("ins2")) {
					isPrimary=false;
				}
				//This will update every insurance row's Import status at once.
				UpdateInsuranceRows(e,isPrimary,true);
			}
			else {//Not insurance row.
				Rows[e.Row].DoImport=!Rows[e.Row].DoImport;
			}
			FillGrid();
		}

		///<summary>Mostly the same as IsImportable.  But subtle differences.</summary>
		private bool IsEditable(SheetImportRow row) {
			if(row.FieldName=="wirelessCarrier"){
				MessageBox.Show(row.FieldName+" "+Lan.g(this,"cannot be imported."));
				return false;
			}
			if(row.FieldName=="referredFrom") {
				if(row.OldValObj!=null) {
					MsgBox.Show(this,"This patient already has a referral source selected and it cannot be changed from here.");
					return false;
				}
			}
			//if(row.FieldName.StartsWith("ins1") || row.FieldName.StartsWith("ins2")) {
			//  //if(patPlanList.Count>0) {
			//  MsgBox.Show(this,"Insurance cannot be imported yet.");
			//  return false;
			//  //}
			//}
			return true;
		}

		private bool IsImportable(SheetImportRow row) {
			if(row.ImpValObj==null) {
				MsgBox.Show(this,"Please enter a value for this row first.");
				return false;
			}
			return IsEditable(row);
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=3) {
				return;
			}
			if(Rows[e.Row].IsSeparator) {
				return;
			}
			if(!IsEditable(Rows[e.Row])){
				return;
			}
			if(Rows[e.Row].FieldName=="referredFrom") {
				FormReferralSelect formRS=new FormReferralSelect();
				formRS.IsSelectionMode=true;
				formRS.ShowDialog();
				if(formRS.DialogResult!=DialogResult.OK) {
					return;
				}
				Referral referralSelected=formRS.SelectedReferral;
				Rows[e.Row].DoImport=true;
				Rows[e.Row].IsFlaggedImp=false;
				Rows[e.Row].ImpValDisplay=referralSelected.GetNameFL();
				Rows[e.Row].ImpValObj=referralSelected;
			}
			#region string
			else if(Rows[e.Row].ObjType==typeof(string)) {
				InputBox inputbox=new InputBox(Rows[e.Row].FieldName);
				inputbox.textResult.Text=Rows[e.Row].ImpValDisplay;
				inputbox.ShowDialog();
				if(inputbox.DialogResult!=DialogResult.OK) {
					return;
				}
				if(Rows[e.Row].FieldName=="addressAndHmPhoneIsSameEntireFamily") {
					if(inputbox.textResult.Text=="") {
						AddressSameForFam=false;
					}
					else if(inputbox.textResult.Text!="X") {
						AddressSameForFam=true;
					}
					else {
						MsgBox.Show(this,"The only allowed values are X or blank.");
						return;
					}
				}
				if(Rows[e.Row].OldValDisplay==inputbox.textResult.Text) {//value is now same as original
					Rows[e.Row].DoImport=false;
				}
				else {
					Rows[e.Row].DoImport=true;
				}
				Rows[e.Row].ImpValDisplay=inputbox.textResult.Text;
				Rows[e.Row].ImpValObj=inputbox.textResult.Text;
			}
			#endregion
			#region Enum
			else if(Rows[e.Row].ObjType.IsEnum) {
				//Note.  This only works for zero-indexed enums.
				FormSheetImportEnumPicker formEnum=new FormSheetImportEnumPicker(Rows[e.Row].FieldName);
				for(int i=0;i<Enum.GetNames(Rows[e.Row].ObjType).Length;i++) {
					formEnum.listResult.Items.Add(Enum.GetNames(Rows[e.Row].ObjType)[i]);
					if(Rows[e.Row].ImpValObj!=null && i==(int)Rows[e.Row].ImpValObj) {
						formEnum.listResult.SelectedIndex=i;
					}
				}
				formEnum.ShowDialog();
				if(formEnum.DialogResult==DialogResult.OK) {
					int selectedI=formEnum.listResult.SelectedIndex;
					if(Rows[e.Row].ImpValObj==null) {//was initially null
						if(selectedI!=-1) {//an item was selected
							Rows[e.Row].ImpValObj=Enum.ToObject(Rows[e.Row].ObjType,selectedI);
							Rows[e.Row].ImpValDisplay=Rows[e.Row].ImpValObj.ToString();
						}
					}
					else {//was not initially null
						if((int)Rows[e.Row].ImpValObj!=selectedI) {//value was changed.
							//There's no way for the user to set it to null, so we do not need to test that
							Rows[e.Row].ImpValObj=Enum.ToObject(Rows[e.Row].ObjType,selectedI);
							Rows[e.Row].ImpValDisplay=Rows[e.Row].ImpValObj.ToString();
						}
					}
					if(selectedI==-1) {
						Rows[e.Row].DoImport=false;//impossible to import a null
					}
					else if((int)Rows[e.Row].ImpValObj==(int)Rows[e.Row].OldValObj) {//it's the old setting for the patient, whether or not they actually changed it.
						Rows[e.Row].DoImport=false;//so no need to import
					}
					else {
						Rows[e.Row].DoImport=true;
					}
				}
			}
			#endregion
			#region DateTime
			else if(Rows[e.Row].ObjType==typeof(DateTime)) {//this is only for one field so far: Birthdate
				InputBox inputbox=new InputBox(Rows[e.Row].FieldName);
				inputbox.textResult.Text=Rows[e.Row].ImpValDisplay;
				inputbox.ShowDialog();
				if(inputbox.DialogResult!=DialogResult.OK) {
					return;
				}
				DateTime enteredDate;
				if(inputbox.textResult.Text=="") {
					enteredDate=DateTime.MinValue;
					Rows[e.Row].ImpValObj=enteredDate;
					Rows[e.Row].ImpValDisplay="";
				}
				else {
					try {
						enteredDate=DateTime.Parse(inputbox.textResult.Text);
					}
					catch {
						MsgBox.Show(this,"Invalid date");
						return;
					}
					if(enteredDate.Year<1880 || enteredDate.Year>2050) {
						MsgBox.Show(this,"Invalid date");
						return;
					}
					Rows[e.Row].ImpValObj=enteredDate;
					Rows[e.Row].ImpValDisplay=enteredDate.ToShortDateString();
				}
				if(Rows[e.Row].ImpValDisplay==Rows[e.Row].OldValDisplay) {//value is now same as original
					Rows[e.Row].DoImport=false;
				}
				else {
					Rows[e.Row].DoImport=true;
				}
			}
			#endregion
			#region Medication, Allergy or Disease
			else if(Rows[e.Row].ObjType==typeof(Medication)
				|| Rows[e.Row].ObjType==typeof(Allergy)
				|| Rows[e.Row].ObjType==typeof(Disease)) 
			{
				//User entered medications will have a MedicationNum as the ImpValObj.
				if(Rows[e.Row].ImpValObj.GetType()==typeof(long)) {
					FormMedications FormM=new FormMedications();
					FormM.IsSelectionMode=true;
					FormM.textSearch.Text=Rows[e.Row].FieldName;
					FormM.ShowDialog();
					if(FormM.DialogResult!=DialogResult.OK) {
						return;
					}
					Rows[e.Row].ImpValDisplay="Y";
					Rows[e.Row].ImpValObj=FormM.SelectedMedicationNum;
					string descript=Medications.GetDescription(FormM.SelectedMedicationNum);
					Rows[e.Row].FieldDisplay=descript;
					((SheetField)Rows[e.Row].NewValObj).FieldValue=descript;
					Rows[e.Row].NewValDisplay="Y";
					Rows[e.Row].DoImport=true;
					Rows[e.Row].IsFlaggedImp=false;
				}
				else {
					FormSheetImportEnumPicker FormIEP=new FormSheetImportEnumPicker(Rows[e.Row].FieldName);
					for(int i=0;i<Enum.GetNames(typeof(YN)).Length;i++) {
						FormIEP.listResult.Items.Add(Enum.GetNames(typeof(YN))[i]);
					}
					FormIEP.listResult.SelectedIndex=0;//Unknown
					if(Rows[e.Row].ImpValDisplay=="Y") {
						FormIEP.listResult.SelectedIndex=1;
					}
					if(Rows[e.Row].ImpValDisplay=="N") {
						FormIEP.listResult.SelectedIndex=2;
					}
					FormIEP.ShowDialog();
					if(FormIEP.DialogResult!=DialogResult.OK) {
						return;
					}
					int selectedI=FormIEP.listResult.SelectedIndex;
					switch(selectedI) {
						case 0:
							Rows[e.Row].ImpValDisplay="";
							break;
						case 1:
							Rows[e.Row].ImpValDisplay="Y";
							break;
						case 2:
							Rows[e.Row].ImpValDisplay="N";
							break;
					}
					if(Rows[e.Row].OldValDisplay==Rows[e.Row].ImpValDisplay) {//value is now same as original
						Rows[e.Row].DoImport=false;
					}
					else {
						Rows[e.Row].DoImport=true;
					}
					if(selectedI==-1 || selectedI==0) {
						Rows[e.Row].DoImport=false;
					}
				}
			}
			#endregion
			#region InsPlan
			else if(Rows[e.Row].ObjType==typeof(InsPlan)) {
				InsPlan plan;
				bool isPrimary=true;
				if(Rows[e.Row].FieldName.StartsWith("ins2")) {
					isPrimary=false;
				}
				PatPlan patPlan=isPrimary?PatPlan1:PatPlan2;
				InsSub sub=isPrimary?Sub1:Sub2;
				Patient subscriber=null;
				if(sub!=null) {
					subscriber=Patients.GetPat(sub.Subscriber);
				}
				//Subscriber------------------------------------------------------------------------------------------------
				if(subscriber==null) {
					DialogResult result=MessageBox.Show(Lan.g(this,"Is this patient the subscriber?"),"",MessageBoxButtons.YesNoCancel);
					if(result==DialogResult.Cancel) {
						return;
					}
					if(result==DialogResult.Yes) {//current patient is subscriber
						subscriber=Pat.Copy();
					}
					else {//patient is not subscriber
						//show list of patients in this family
						FormSubscriberSelect FormS=new FormSubscriberSelect(Fam);
						FormS.ShowDialog();
						if(FormS.DialogResult==DialogResult.Cancel) {
							return;
						}
						subscriber=Patients.GetPat(FormS.SelectedPatNum);
					}
				}
				if(Rows[e.Row].ImpValObj==null) {
					plan=new InsPlan();
					FormInsPlans formI=new FormInsPlans();
					formI.IsSelectMode=true;
					formI.ShowDialog();
					if(formI.DialogResult!=DialogResult.OK) {
						return;
					}
					plan=formI.SelectedPlan;
				}
				else {
					plan=(InsPlan)Rows[e.Row].ImpValObj;
				}
				bool isNewPatPlan=false;
				if(patPlan==null) {
					patPlan=new PatPlan();
					isNewPatPlan=true;
				}
				if(sub==null) {
					sub=new InsSub();
				}
				sub.Subscriber=subscriber.PatNum;
				FormInsPlan FormIP=new FormInsPlan(plan,patPlan,sub);
				FormIP.IsNewPatPlan=isNewPatPlan;
				FormIP.ShowDialog();
				if(isPrimary) {
					Plan1=plan;
					Sub1=sub;
					PatPlan1=patPlan;
				}
				else {
					Plan2=plan;
					Sub2=sub;
					PatPlan2=patPlan;
				}
				UpdateInsuranceRows(e,isPrimary,false);
			}
			#endregion
			FillGrid();
		}

		///<summary>Updates all related insurance rows at once. Uses the class wide variables that should be set before calling this function (Plan,PatPlan,Sub).
		///Set isImportCheck to true to only affect the DoImport status on every insurance row. It will not update insurance information.
		///Every corresponding ins DoImport status will change to the status of the cell that was passed in.</summary>
		private void UpdateInsuranceRows(ODGridClickEventArgs e,bool isPrimary,bool isImportCheck) {
			bool doImport=!Rows[e.Row].DoImport;//Only used when isImportCheck is true.
			string insStr="ins1";
			if(!isPrimary) {
				insStr="ins2";
			}
			InsPlan plan=isPrimary?Plan1:Plan2;
			PatPlan patPlan=isPrimary?PatPlan1:PatPlan2;
			InsSub sub=isPrimary?Sub1:Sub2;
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			Employer employer=Employers.GetEmployer(plan.EmployerNum);
			//Load up every row related to the particular ins.
			SheetImportRow relationRow=GetImportRowByFieldName(insStr+"Relat");
			SheetImportRow subscriberRow=GetImportRowByFieldName(insStr+"Subscriber");
			SheetImportRow subscriberIdRow=GetImportRowByFieldName(insStr+"SubscriberID");
			SheetImportRow carrierNameRow=GetImportRowByFieldName(insStr+"CarrierName");
			SheetImportRow carrierPhoneRow=GetImportRowByFieldName(insStr+"CarrierPhone");
			SheetImportRow employerNameRow=GetImportRowByFieldName(insStr+"EmployerName");
			SheetImportRow groupNameRow=GetImportRowByFieldName(insStr+"GroupName");
			SheetImportRow groupNumRow=GetImportRowByFieldName(insStr+"GroupNum");
			//Set the values for the corresponding rows based on the selected plan.  
			//The sheet could be missing one of these fields so we have to check for nulls.
			if(relationRow!=null) {
				if(isImportCheck) {
					relationRow.DoImport=doImport;
				}
				else {
					relationRow.ImpValDisplay=((Relat)patPlan.Relationship).ToString();
					relationRow.ImpValObj=plan;
					relationRow.DoImport=true;
					relationRow.IsFlaggedImp=false;
				}
			}
			if(subscriberRow!=null) {
				if(isImportCheck) {
					subscriberRow.DoImport=doImport;
				}
				else {
					subscriberRow.ImpValDisplay=Patients.GetPat(sub.Subscriber).GetNameFirst();
					subscriberRow.ImpValObj=plan;
					subscriberRow.DoImport=true;
					subscriberRow.IsFlaggedImp=false;
				}
			}
			if(subscriberIdRow!=null) {
				if(isImportCheck) {
					subscriberIdRow.DoImport=doImport;
				}
				else {
					subscriberIdRow.ImpValDisplay=sub.SubscriberID;
					subscriberIdRow.ImpValObj=plan;
					subscriberIdRow.DoImport=true;
					subscriberIdRow.IsFlaggedImp=false;
				}
			}
			if(carrierNameRow!=null) {
				if(isImportCheck) {
					carrierNameRow.DoImport=doImport;
				}
				else {
					carrierNameRow.ImpValDisplay=carrier.CarrierName;
					carrierNameRow.ImpValObj=plan;
					carrierNameRow.DoImport=true;
					carrierNameRow.IsFlaggedImp=false;
				}
			}
			if(carrierPhoneRow!=null) {
				if(isImportCheck) {
					carrierPhoneRow.DoImport=doImport;
				}
				else {
					carrierPhoneRow.ImpValDisplay=carrier.Phone;
					carrierPhoneRow.ImpValObj=plan;
					carrierPhoneRow.DoImport=true;
					carrierPhoneRow.IsFlaggedImp=false;
				}
			}
			if(employerNameRow!=null) {
				if(isImportCheck) {
					employerNameRow.DoImport=doImport;
				}
				else {
					employerNameRow.ImpValDisplay=employer.EmpName;
					employerNameRow.ImpValObj=plan;
					employerNameRow.DoImport=true;
					employerNameRow.IsFlaggedImp=false;
				}
			}
			if(groupNameRow!=null) {
				if(isImportCheck) {
					groupNameRow.DoImport=doImport;
				}
				else {
					groupNameRow.ImpValDisplay=plan.GroupName;
					groupNameRow.ImpValObj=plan;
					groupNameRow.DoImport=true;
					groupNameRow.IsFlaggedImp=false;
				}
			}
			if(groupNumRow!=null) {
				if(isImportCheck) {
					groupNumRow.DoImport=doImport;
				}
				else {
					groupNumRow.ImpValDisplay=plan.GroupNum;
					groupNumRow.ImpValObj=plan;
					groupNumRow.DoImport=true;
					groupNumRow.IsFlaggedImp=false;
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool importsPresent=false;
			for(int i=0;i<Rows.Count;i++) {
				if(Rows[i].DoImport) {
					importsPresent=true;
					break;
				}
			}
			if(!importsPresent) {
				MsgBox.Show(this,"No rows are set for import.");
				return;
			}
			#region Patient Form
			if(SheetCur.SheetType==SheetTypeEnum.PatientForm) {
				Patient patientOld=Pat.Copy();
				for(int i=0;i<Rows.Count;i++) {
					if(!Rows[i].DoImport) {
						continue;
					}
					switch(Rows[i].FieldName) {
						case "LName":
							Pat.LName=Rows[i].ImpValDisplay;
							break;
						case "FName":
							Pat.FName=Rows[i].ImpValDisplay;
							break;
						case "MiddleI":
							Pat.MiddleI=Rows[i].ImpValDisplay;
							break;
						case "Preferred":
							Pat.Preferred=Rows[i].ImpValDisplay;
							break;
						case "Gender":
							Pat.Gender=(PatientGender)Rows[i].ImpValObj;
							break;
						case "Position":
							Pat.Position=(PatientPosition)Rows[i].ImpValObj;
							break;
						case "Birthdate":
							Pat.Birthdate=(DateTime)Rows[i].ImpValObj;
							break;
						case "SSN":
							Pat.SSN=Rows[i].ImpValDisplay;
							break;
						case "WkPhone":
							Pat.WkPhone=Rows[i].ImpValDisplay;
							break;
						case "WirelessPhone":
							Pat.WirelessPhone=Rows[i].ImpValDisplay;
							break;
						case "Email":
							Pat.Email=Rows[i].ImpValDisplay;
							break;
						case "PreferContactMethod":
							Pat.PreferContactMethod=(ContactMethod)Rows[i].ImpValObj;
							break;
						case "PreferConfirmMethod":
							Pat.PreferConfirmMethod=(ContactMethod)Rows[i].ImpValObj;
							break;
						case "PreferRecallMethod":
							Pat.PreferRecallMethod=(ContactMethod)Rows[i].ImpValObj;
							break;
						case "referredFrom":
							RefAttach ra=new RefAttach();
							ra.IsFrom=true;
							ra.ItemOrder=1;
							ra.PatNum=Pat.PatNum;
							ra.RefDate=DateTime.Today;
							ra.ReferralNum=((Referral)Rows[i].ImpValObj).ReferralNum;
							RefAttaches.Insert(ra);//no security to block this action.
							SecurityLogs.MakeLogEntry(Permissions.RefAttachAdd,Pat.PatNum,"Referred From "+Referrals.GetNameFL(ra.ReferralNum));
							break;
						//AddressSameForFam already set, but not really importable by itself
						case "Address":
							Pat.Address=Rows[i].ImpValDisplay;
							break;
						case "Address2":
							Pat.Address2=Rows[i].ImpValDisplay;
							break;
						case "City":
							Pat.City=Rows[i].ImpValDisplay;
							break;
						case "State":
							Pat.State=Rows[i].ImpValDisplay;
							break;
						case "Zip":
							Pat.Zip=Rows[i].ImpValDisplay;
							break;
						case "HmPhone":
							Pat.HmPhone=Rows[i].ImpValDisplay;
							break;

						//ins1 and ins2 do not get imported.
					}
				}
				Patients.Update(Pat,patientOld);
				if(AddressSameForFam) {
					Patients.UpdateAddressForFam(Pat);
				}
			}
			#endregion
			#region Medical History
			else if(SheetCur.SheetType==SheetTypeEnum.MedicalHistory) {
				for(int i=0;i<Rows.Count;i++) {
					if(!Rows[i].DoImport) {
						continue;
					}
					if(Rows[i].ObjType==null) {//Should never happen.
						continue;
					}
					YN hasValue=YN.Unknown;
					if(Rows[i].ImpValDisplay=="Y") {
						hasValue=YN.Yes;
					}
					if(Rows[i].ImpValDisplay=="N") {
						hasValue=YN.No;
					}
					if(hasValue==YN.Unknown) {//Unknown, nothing to do.
						continue;
					}
					#region Allergies
					if(Rows[i].ObjType==typeof(Allergy)) {
						//Patient has this allergy in the db so just update the value.
						if(Rows[i].OldValObj!=null) {
							Allergy oldAllergy=(Allergy)Rows[i].OldValObj;
							if(hasValue==YN.Yes) {
								oldAllergy.StatusIsActive=true;
							}
							else {
								oldAllergy.StatusIsActive=false;
							}
							Allergies.Update(oldAllergy);
							continue;
						}
						if(hasValue==YN.No) {//We never import allergies with inactive status.
							continue;
						}
						//Allergy does not exist for this patient yet so create one.
						List<AllergyDef> allergyList=AllergyDefs.GetAll(false);
						SheetField allergySheet=(SheetField)Rows[i].NewValObj;
						//Find what allergy user wants to import.
						for(int j=0;j<allergyList.Count;j++) {
							if(allergyList[j].Description==allergySheet.FieldName.Remove(0,8)) {
								Allergy newAllergy=new Allergy();
								newAllergy.AllergyDefNum=allergyList[j].AllergyDefNum;
								newAllergy.PatNum=Pat.PatNum;
								newAllergy.StatusIsActive=true;
								Allergies.Insert(newAllergy);
								break;
							}
						}
					}
					#endregion
					#region Medications
					else if(Rows[i].ObjType==typeof(Medication)) {
					  //Patient has this medication in the db so leave it alone or set the stop date.
					  if(Rows[i].OldValObj!=null) {
					    //Set the stop date for the current medication(s).
					    Medication oldMed=(Medication)Rows[i].OldValObj;
					    List<MedicationPat> patMeds=MedicationPats.GetMedicationPatsByMedicationNum(oldMed.MedicationNum,Pat.PatNum);
					    for(int j=0;j<patMeds.Count;j++) {
					      if(hasValue==YN.Yes) {
					        //Check if med is currently inactive.
					        if(patMeds[j].DateStop.Year>1880 && patMeds[j].DateStop<=DateTime.Now) {
					          patMeds[j].DateStop=new DateTime(0001,1,1);//This will activate the med.
					        }
					      }
								else {
									patMeds[j].DateStop=DateTime.Now;//Set the med as inactive.
								}
					      MedicationPats.Update(patMeds[j]);
					    }
					    continue;
					  }
						if(hasValue==YN.No) {//Don't import medications with inactive status.
							continue;
						}
					  //Medication does not exist for this patient yet so create it.
					  List<Medication> medList=Medications.GetList("");
					  SheetField medSheet=(SheetField)Rows[i].NewValObj;
					  //Find what allergy user wants to import.
					  for(int j=0;j<medList.Count;j++) {
					    if(Medications.GetDescription(medList[j].MedicationNum)==medSheet.FieldValue) {
					      MedicationPat medPat=new MedicationPat();
					      medPat.PatNum=Pat.PatNum;
					      medPat.MedicationNum=medList[j].MedicationNum;
					      MedicationPats.Insert(medPat);
					      break;
					    }
					  }
					}
					#endregion
					#region Diseases
					else if(Rows[i].ObjType==typeof(Disease)) {
						//Patient has this problem in the db so just update the value.
						if(Rows[i].OldValObj!=null) {
							Disease oldDisease=(Disease)Rows[i].OldValObj;
							if(hasValue==YN.Yes) {
								oldDisease.ProbStatus=ProblemStatus.Active;
							}
							else {
								oldDisease.ProbStatus=ProblemStatus.Inactive;
							}
							Diseases.Update(oldDisease);
							continue;
						}
						if(hasValue==YN.No) {//Don't create new problem with inactive status.
							continue;
						}
						//Problem does not exist for this patient yet so create one.
						SheetField diseaseSheet=(SheetField)Rows[i].NewValObj;
						//Find what allergy user wants to import.
						for(int j=0;j<DiseaseDefs.List.Length;j++) {
							if(DiseaseDefs.List[j].DiseaseName==diseaseSheet.FieldName.Remove(0,8)) {
								Disease newDisease=new Disease();
								newDisease.PatNum=Pat.PatNum;
								newDisease.DiseaseDefNum=DiseaseDefs.List[j].DiseaseDefNum;
								newDisease.ProbStatus=ProblemStatus.Active;
								Diseases.Insert(newDisease);
								break;
							}
						}
					}
					#endregion
				}
			}
			#endregion
			MsgBox.Show(this,"Done.");
			DialogResult=DialogResult.OK;
		}

		private bool DoImport(string fieldName) {
			for(int i=0;i<Rows.Count;i++) {
				if(Rows[i].FieldName!=fieldName) {
					continue;
				}
				return Rows[i].DoImport;
			}
			return false;
		}

		///<summary>Will return null if field not found or if field marked to not import.</summary>
		private object GetImpObj(string fieldName) {
			for(int i=0;i<Rows.Count;i++) {
				if(Rows[i].FieldName!=fieldName) {
					continue;
				}
				if(!Rows[i].DoImport) {
					return null;
				}
				return Rows[i].ImpValObj;
			}
			return null;
		}

		///<summary>Will return empty string field not found or if field marked to not import.</summary>
		private string GetImpDisplay(string fieldName) {
			for(int i=0;i<Rows.Count;i++) {
				if(Rows[i].FieldName!=fieldName) {
					continue;
				}
				if(!Rows[i].DoImport) {
					return "";
				}
				return Rows[i].ImpValDisplay;
			}
			return "";
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		///<summary>Returns a separator and sets the FieldName to the passed in string.</summary>
		private SheetImportRow CreateSeparator(string displayText) {
			SheetImportRow separator=new SheetImportRow();
			separator.FieldName=displayText;
			separator.IsSeparator=true;
			return separator;
		}

		///<summary>We create these import rows so often this will just save time.</summary>
		private SheetImportRow CreateImportRow(string fieldName,string fieldDisplay,string oldValDisplay,object oldValObj,string newValDisplay,object newValObj,string impValDisplay,object impValObj,bool doImport,bool isSeparator,Type objType,bool isFlagged,bool isFlaggedImp) {
			SheetImportRow importRow=new SheetImportRow();
			importRow.FieldName=fieldName;
			if(fieldDisplay!="") {
				importRow.FieldDisplay=fieldDisplay;
			}
			importRow.OldValDisplay=oldValDisplay;
			importRow.OldValObj=oldValObj;
			importRow.NewValDisplay=newValDisplay;
			importRow.NewValObj=newValObj;
			importRow.ImpValDisplay=impValDisplay;
			importRow.ImpValObj=impValObj;
			importRow.DoImport=doImport;
			importRow.IsSeparator=isSeparator;
			importRow.ObjType=objType;
			importRow.IsFlagged=isFlagged;
			importRow.IsFlaggedImp=isFlaggedImp;
			return importRow;
		}

		private class SheetImportRow {
			public string FieldName;
			///<summary>Overrides FieldName.  If null, use FieldName;</summary>
			public string FieldDisplay;
			public string OldValDisplay;
			public object OldValObj;
			public string NewValDisplay;
			public object NewValObj;
			public string ImpValDisplay;
			public object ImpValObj;
			public bool DoImport;
			public bool IsSeparator;
			///<summary>This is needed because the NewValObj might be null.</summary>
			public Type ObjType;
			///<summary>Some fields are not importable, but they still need to be made obvious to user by coloring the user-entered value red.</summary>
			public bool IsFlagged;
			///<summary>The import cell is shown with colored text to prompt user to notice.</summary>
			public bool IsFlaggedImp;
		}

		

		
	}

	
}