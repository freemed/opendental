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
		private List<SheetImportRow> rows;
		private Patient pat;
		private Family fam;
		///<summary>We must have a readily available bool, whether or not this checkbox field is present on the sheet.  It gets set at the very beginning, then gets changes based on user input on the sheet and in this window.</summary>
		private bool AddressSameForFam;
		private InsPlan plan1;
		private InsPlan plan2;
		private List<PatPlan> patPlanList;
		private List<InsPlan> planList;
		private PatPlan patPlan1;
		private PatPlan patPlan2;
		private Relat? ins1Relat;
		private Relat? ins2Relat;
		private Carrier carrier1;
		private Carrier carrier2;
		private Dictionary<string,string> dictAcrobatFields;
		private List<InsSub> subList;
		private InsSub sub1;
		private InsSub sub2;

		public FormSheetImport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetImport_Load(object sender,EventArgs e) {
			if(SheetCur!=null) {
				pat=Patients.GetPat(SheetCur.PatNum);
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
			fam=Patients.GetFamily(pat.PatNum);
			AddressSameForFam=true;
			for(int i=0;i<fam.ListPats.Length;i++) {
				if(pat.HmPhone!=fam.ListPats[i].HmPhone
					|| pat.Address!=fam.ListPats[i].Address
					|| pat.Address2!=fam.ListPats[i].Address2
					|| pat.City!=fam.ListPats[i].City
					|| pat.State!=fam.ListPats[i].State
					|| pat.Zip!=fam.ListPats[i].Zip) 
				{
					AddressSameForFam=false;
					break;
				}
			}
			patPlanList=PatPlans.Refresh(pat.PatNum);
			subList=InsSubs.RefreshForFam(fam);
			planList=InsPlans.RefreshForSubList(subList);
			if(patPlanList.Count==0) {
				patPlan1=null;
				plan1=null;
				sub1=null;
				ins1Relat=null;
				carrier1=null;
			}
			else {
				patPlan1=patPlanList[0];
				sub1=InsSubs.GetSub(patPlan1.InsSubNum,subList);
				plan1=InsPlans.GetPlan(sub1.PlanNum,planList);
				ins1Relat=patPlan1.Relationship;
				carrier1=Carriers.GetCarrier(plan1.CarrierNum);
			}
			if(patPlanList.Count<2) {
				patPlan2=null;
				plan2=null;
				sub2=null;
				ins2Relat=null;
				carrier2=null;
			}
			else {
				patPlan2=patPlanList[1];
				sub2=InsSubs.GetSub(patPlan2.InsSubNum,subList);
				plan2=InsPlans.GetPlan(sub2.PlanNum,planList);
				ins2Relat=patPlan2.Relationship;
				carrier2=Carriers.GetCarrier(plan2.CarrierNum);
			}
			FillRows();
			FillGrid();
		}

		///<summary>This can only be run once when the form first opens.  After that, the rows are just edited.</summary>
		private void FillRows() {
			#region Patient Form
			if(SheetCur.SheetType==SheetTypeEnum.PatientForm) {
				rows=new List<SheetImportRow>();
				SheetImportRow row;
				string fieldVal;
				rows.Add(CreateSeparator("Personal"));
				#region personal
				//LName---------------------------------------------
				fieldVal=GetInputValue("LName");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("LName","",pat.LName,pat.LName,fieldVal,fieldVal,fieldVal,fieldVal,pat.LName!=fieldVal,false,typeof(string),false,false));
				}
				//FName---------------------------------------------
				fieldVal=GetInputValue("FName");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("FName","",pat.FName,pat.FName,fieldVal,fieldVal,fieldVal,fieldVal,pat.FName!=fieldVal,false,typeof(string),false,false));
				}
				//MiddleI---------------------------------------------
				fieldVal=GetInputValue("MiddleI");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("MiddleI","",pat.MiddleI,pat.MiddleI,fieldVal,fieldVal,fieldVal,fieldVal,pat.MiddleI!=fieldVal,false,typeof(string),false,false));
				}
				//Preferred---------------------------------------------
				fieldVal=GetInputValue("Preferred");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("Preferred","",pat.Preferred,pat.Preferred,fieldVal,fieldVal,fieldVal,fieldVal,
						pat.Preferred!=fieldVal,false,typeof(string),false,false));
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
					rows.Add(CreateImportRow("Gender","",Lan.g("enumPatientGender",pat.Gender.ToString()),pat.Gender,
						newValDisplay,newValObj,newValDisplay,newValObj,newValObj!=null && (PatientGender)newValObj!=pat.Gender,false,typeof(PatientGender),false,false));
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
					rows.Add(CreateImportRow("Position","",Lan.g("enumPatientPositionr",pat.Position.ToString()),pat.Position,
						newValDisplay,newValObj,newValDisplay,newValObj,newValObj!=null && (PatientPosition)newValObj!=pat.Position,false,typeof(PatientPosition),false,false));
				}
				//Birthdate---------------------------------------------
				fieldVal=GetInputValue("Birthdate");
				if(fieldVal!=null) {
					string oldValDisplay="";
					string newValDisplay="";
					object newValObj=null;
					if(pat.Birthdate.Year>1880) {
						oldValDisplay=pat.Birthdate.ToShortDateString();
					}
					newValObj=PIn.Date(fieldVal);
					if(((DateTime)newValObj).Year>1880) {
						newValDisplay=((DateTime)newValObj).ToShortDateString();
					}
					rows.Add(CreateImportRow("Birthdate","",oldValDisplay,pat.Birthdate,newValDisplay,newValObj,newValDisplay,newValObj,
						oldValDisplay!=newValDisplay,false,typeof(DateTime),false,false));
				}
				//SSN---------------------------------------------
				fieldVal=GetInputValue("SSN");
				if(fieldVal!=null) {
					string newValDisplay=fieldVal.Replace("-","");//quickly strip dashes
					rows.Add(CreateImportRow("SSN","",pat.SSN,pat.SSN,newValDisplay,newValDisplay,newValDisplay,newValDisplay,
						pat.SSN!=newValDisplay,false,typeof(string),false,false));
				}
				//WkPhone---------------------------------------------
				fieldVal=GetInputValue("WkPhone");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("WkPhone","",pat.WkPhone,pat.WkPhone,fieldVal,fieldVal,fieldVal,fieldVal,pat.WkPhone!=fieldVal,false,typeof(string),false,false));
				}
				//WirelessPhone---------------------------------------------
				fieldVal=GetInputValue("WirelessPhone");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("WirelessPhone","",pat.WirelessPhone,pat.WirelessPhone,fieldVal,fieldVal,fieldVal,fieldVal,
						pat.WirelessPhone!=fieldVal,false,typeof(string),false,false));
				}
				//wirelessCarrier---------------------------------------------
				fieldVal=GetInputValue("wirelessCarrier");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("wirelessCarrier","","","",fieldVal,fieldVal,fieldVal,fieldVal,false,false,typeof(string),true,false));
				}
				//Email---------------------------------------------
				fieldVal=GetInputValue("Email");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("Email","",pat.Email,pat.Email,fieldVal,fieldVal,fieldVal,fieldVal,pat.Email!=fieldVal,false,typeof(string),false,false));
				}
				//PreferContactMethod---------------------------------------------
				fieldVal=GetRadioValue("PreferContactMethod");
				if(fieldVal!=null) {
					string oldValDisplay=Lan.g("enumContactMethod",pat.PreferContactMethod.ToString());
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
					rows.Add(CreateImportRow("PreferContactMethod","",oldValDisplay,pat.PreferContactMethod,newValDisplay,newValObj,newValDisplay,newValObj,
						newValObj!=null && (ContactMethod)newValObj!=pat.PreferContactMethod,false,typeof(ContactMethod),false,false));
				}
				//PreferConfirmMethod---------------------------------------------
				fieldVal=GetRadioValue("PreferConfirmMethod");
				if(fieldVal!=null) {
					string oldValDisplay=Lan.g("enumContactMethod",pat.PreferConfirmMethod.ToString());
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
					rows.Add(CreateImportRow("PreferConfirmMethod","",oldValDisplay,pat.PreferConfirmMethod,newValDisplay,newValObj,newValDisplay,newValObj,
						newValObj!=null && (ContactMethod)newValObj!=pat.PreferConfirmMethod,false,typeof(ContactMethod),false,false));
				}
				//PreferRecallMethod---------------------------------------------
				fieldVal=GetRadioValue("PreferRecallMethod");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					string oldValDisplay=Lan.g("enumContactMethod",pat.PreferRecallMethod.ToString());
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
					rows.Add(CreateImportRow("PreferRecallMethod","",oldValDisplay,pat.PreferRecallMethod,newValDisplay,newValObj,newValDisplay,newValObj,
						newValObj!=null && (ContactMethod)newValObj!=pat.PreferRecallMethod,false,typeof(ContactMethod),false,false));
				}
				//referredFrom---------------------------------------------
				fieldVal=GetInputValue("referredFrom");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="referredFrom";
					Referral refer=Referrals.GetReferralForPat(pat.PatNum);
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
					rows.Add(row);
				}
				#endregion personal
				//Separator-------------------------------------------
				rows.Add(CreateSeparator("Address and Home Phone"));
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
					rows.Add(row);
				}
				//Address---------------------------------------------
				fieldVal=GetInputValue("Address");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("Address","",pat.Address,pat.Address,fieldVal,fieldVal,fieldVal,fieldVal,pat.Address!=fieldVal,false,typeof(string),false,false));
				}
				//Address2---------------------------------------------
				fieldVal=GetInputValue("Address2");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("Address2","",pat.Address2,pat.Address2,fieldVal,fieldVal,fieldVal,fieldVal,pat.Address2!=fieldVal,false,typeof(string),false,false));
				}
				//City---------------------------------------------
				fieldVal=GetInputValue("City");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("City","",pat.City,pat.City,fieldVal,fieldVal,fieldVal,fieldVal,pat.City!=fieldVal,false,typeof(string),false,false));
				}
				//State---------------------------------------------
				fieldVal=GetInputValue("State");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("State","",pat.State,pat.State,fieldVal,fieldVal,fieldVal,fieldVal,pat.State!=fieldVal,false,typeof(string),false,false));
				}
				//Zip---------------------------------------------
				fieldVal=GetInputValue("Zip");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("Zip","",pat.Zip,pat.Zip,fieldVal,fieldVal,fieldVal,fieldVal,pat.Zip!=fieldVal,false,typeof(string),false,false));
				}
				//HmPhone---------------------------------------------
				fieldVal=GetInputValue("HmPhone");
				if(fieldVal!=null) {
					rows.Add(CreateImportRow("HmPhone","",pat.HmPhone,pat.HmPhone,fieldVal,fieldVal,fieldVal,fieldVal,pat.HmPhone!=fieldVal,false,typeof(string),false,false));
				}
				#endregion address
				//Separator-------------------------------------------
				rows.Add(CreateSeparator("Insurance Policy 1"));
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
					row.OldValDisplay=Lan.g("enumRelat",ins1Relat.ToString());
					row.OldValObj=ins1Relat;
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
					row.ImpValDisplay="";
					row.ImpValObj=null;
					row.ObjType=typeof(Relat);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1Subscriber---------------------------------------------
				fieldVal=GetInputValue("ins1SubscriberNameF");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1Subscriber";
					row.FieldDisplay="Subscriber";
					if(plan1!=null) {
						row.OldValDisplay=fam.GetNameInFamFirst(sub1.Subscriber);
						row.OldValObj=sub1.Subscriber;
					}
					else {
						row.OldValDisplay="";
						row.OldValObj=null;
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj=row.NewValDisplay;
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1SubscriberID---------------------------------------------
				fieldVal=GetInputValue("ins1SubscriberID");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1SubscriberID";
					row.FieldDisplay="Subscriber ID";
					if(plan1!=null) {
						row.OldValDisplay=sub1.SubscriberID;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1CarrierName---------------------------------------------
				fieldVal=GetInputValue("ins1CarrierName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1CarrierName";
					row.FieldDisplay="Carrier";
					if(carrier1!=null) {
						row.OldValDisplay=carrier1.CarrierName;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1CarrierPhone---------------------------------------------
				fieldVal=GetInputValue("ins1CarrierPhone");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1CarrierPhone";
					row.FieldDisplay="Phone";
					if(carrier1!=null) {
						row.OldValDisplay=carrier1.Phone;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1EmployerName---------------------------------------------
				fieldVal=GetInputValue("ins1EmployerName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1EmployerName";
					row.FieldDisplay="Employer";
					if(plan1==null) {
						row.OldValDisplay="";
					}
					else {
						row.OldValDisplay=Employers.GetName(plan1.EmployerNum);
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1GroupName---------------------------------------------
				fieldVal=GetInputValue("ins1GroupName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1GroupName";
					row.FieldDisplay="Group Name";
					if(plan1!=null) {
						row.OldValDisplay=plan1.GroupName;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins1GroupNum---------------------------------------------
				fieldVal=GetInputValue("ins1GroupNum");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins1GroupNum";
					row.FieldDisplay="Group Num";
					if(plan1!=null) {
						row.OldValDisplay=plan1.GroupNum;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				#endregion ins1
				//Separator-------------------------------------------
				rows.Add(CreateSeparator("Insurance Policy 2"));
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
					row.OldValDisplay=Lan.g("enumRelat",ins2Relat.ToString());
					row.OldValObj=ins2Relat;
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
					row.ImpValDisplay="";
					row.ImpValObj=null;
					row.ObjType=typeof(Relat);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2Subscriber---------------------------------------------
				fieldVal=GetInputValue("ins2SubscriberNameF");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2Subscriber";
					row.FieldDisplay="Subscriber";
					if(plan2!=null) {
						row.OldValDisplay=fam.GetNameInFamFirst(sub2.Subscriber);
						row.OldValObj=sub2.Subscriber;
					}
					else {
						row.OldValDisplay="";
						row.OldValObj=null;
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj=row.NewValDisplay;
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2SubscriberID---------------------------------------------
				fieldVal=GetInputValue("ins2SubscriberID");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2SubscriberID";
					row.FieldDisplay="Subscriber ID";
					if(plan2!=null) {
						row.OldValDisplay=sub2.SubscriberID;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2CarrierName---------------------------------------------
				fieldVal=GetInputValue("ins2CarrierName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2CarrierName";
					row.FieldDisplay="Carrier";
					if(carrier2!=null) {
						row.OldValDisplay=carrier2.CarrierName;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2CarrierPhone---------------------------------------------
				fieldVal=GetInputValue("ins2CarrierPhone");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2CarrierPhone";
					row.FieldDisplay="Phone";
					if(carrier2!=null) {
						row.OldValDisplay=carrier2.Phone;
						row.OldValObj="";
					}
					else {
						row.OldValDisplay="";
						row.OldValObj="";
					}
					row.NewValDisplay=fieldVal;//whether it's empty or has a value					
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2EmployerName---------------------------------------------
				fieldVal=GetInputValue("ins2EmployerName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2EmployerName";
					row.FieldDisplay="Employer";
					if(plan2==null) {
						row.OldValDisplay="";
					}
					else {
						row.OldValDisplay=Employers.GetName(plan2.EmployerNum);
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2GroupName---------------------------------------------
				fieldVal=GetInputValue("ins2GroupName");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2GroupName";
					row.FieldDisplay="Group Name";
					if(plan2!=null) {
						row.OldValDisplay=plan2.GroupName;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				//ins2GroupNum---------------------------------------------
				fieldVal=GetInputValue("ins2GroupNum");
				if(fieldVal!=null) {
					row=new SheetImportRow();
					row.FieldName="ins2GroupNum";
					row.FieldDisplay="Group Num";
					if(plan2!=null) {
						row.OldValDisplay=plan2.GroupNum;
					}
					else {
						row.OldValDisplay="";
					}
					row.OldValObj="";
					row.NewValDisplay=fieldVal;
					row.NewValObj="";
					row.ImpValDisplay="";
					row.ImpValObj="";
					row.ObjType=typeof(string);
					row.DoImport=false;
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.IsFlagged=true;
					}
					rows.Add(row);
				}
				#endregion ins2
				//Separator-------------------------------------------
				rows.Add(CreateSeparator("Misc"));
				//misc----------------------------------------------------
				List<string> miscVals=GetMiscValues();
				for(int i=0;i<miscVals.Count;i++) {
					rows.Add(CreateImportRow("misc","misc"+(i+1).ToString(),"","",miscVals[i],"","","",false,false,typeof(string),true,false));
				}
			}
			#endregion
			#region Medical History
			else if(SheetCur.SheetType==SheetTypeEnum.MedicalHistory) {
				rows=new List<SheetImportRow>();
				string fieldVal="";
				List<Allergy> allergies=null;
				List<Medication> meds=null;
				List<Disease> diseases=null;
				SheetImportRow row;
				rows.Add(CreateSeparator("Allergies"));
				#region Allergies
				//Get list of all the allergy check boxes
				List<SheetField> allergyList=GetSheetFieldsByFieldName("allergy:");
				for(int i=0;i<allergyList.Count;i++) {
					fieldVal="";
					if(i<1) {
						allergies=Allergies.GetAll(pat.PatNum,true);
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
							rows.Add(CreateImportRow(row.FieldName,"",row.OldValDisplay,row.OldValObj,"",allergyList[i],"","",false,false,typeof(Allergy),false,false));
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
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.DoImport=true;
					}
					rows.Add(row);
				}
				#endregion
				//Separator-------------------------------------------
				rows.Add(CreateSeparator("Medications"));
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
						meds=Medications.GetMedicationsByPat(pat.PatNum);
					}
					row=new SheetImportRow();
					row.FieldName=currentMedList[i].FieldValue;//Will be the name of the drug.
					row.OldValDisplay="N";
					row.OldValObj=null;
					for(int j=0;j<meds.Count;j++) {
						if(Medications.GetDescription(meds[j].MedicationNum)==currentMedList[i].FieldValue) {
							List<MedicationPat> medList=MedicationPats.GetMedicationPatsByMedicationNum(meds[j].MedicationNum,pat.PatNum);
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
					//Figure out which corresponding checkbox is checked.
					for(int j=0;j<checkMedList.Count;j++) {
						if(checkMedList[j].FieldName.Remove(0,8)==currentMedList[i].FieldName.Remove(0,8)
							&& checkMedList[j].FieldValue!="") 
						{
							if(checkMedList[j].RadioButtonValue=="Y") {
								fieldVal="Y";
							}
							else {
								fieldVal="N";
							}
							break;
						}
					}
					row.NewValDisplay=fieldVal;
					row.NewValObj=currentMedList[i];
					row.ImpValDisplay=row.NewValDisplay;
					row.ImpValObj=typeof(string);
					row.ObjType=typeof(Medication);
					if(row.OldValDisplay!=row.NewValDisplay && row.NewValDisplay!="") {
						row.DoImport=true;
					}
					rows.Add(row);
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
					rows.Add(row);
					#endregion
				}
				#endregion
				//Separator-------------------------------------------
				rows.Add(CreateSeparator("Problems"));
				#region Problems
				List<SheetField> problemList=GetSheetFieldsByFieldName("problem:");
				for(int i=0;i<problemList.Count;i++) {
					fieldVal="";
					if(i<1) {
						diseases=Diseases.Refresh(pat.PatNum,false);
					}
					row=new SheetImportRow();
					row.FieldName=problemList[i].FieldName.Remove(0,8);
					//Figure out the current status of this allergy
					row.OldValDisplay="";
					row.OldValObj=null;
					for(int j=0;j<diseases.Count;j++) {
						if(DiseaseDefs.GetName(diseases[j].DiseaseDefNum)==problemList[i].FieldName.Remove(0,8)) {
							if(diseases[j].ProbStatus==ProblemStatus.Active) {
								row.OldValDisplay="X";
							}
							row.OldValObj=diseases[j];
							break;
						}
					}
					SheetField oppositeBox=GetOppositeSheetFieldCheckBox(problemList,problemList[i]);
					if(problemList[i].FieldValue=="") {//Current box not checked.
						if(oppositeBox==null || oppositeBox.FieldValue=="") {//No opposite box or both boxes are not checked.
							//Create a blank row just in case they still want to import.
							rows.Add(CreateImportRow(row.FieldName,"",row.OldValDisplay,row.OldValObj,"",problemList[i],"","",false,false,typeof(Disease),false,false));
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
					if(row.OldValDisplay!=row.NewValDisplay) {
						row.DoImport=true;
					}
					rows.Add(row);
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
			for(int i=0;i<rows.Count;i++) {
				row=new ODGridRow();
				if(rows[i].IsSeparator) {
					row.Cells.Add(rows[i].FieldName);
					row.Cells.Add("");
					row.Cells.Add("");
					row.Cells.Add("");
					row.Cells.Add("");
					row.ColorBackG=Color.DarkSlateGray;
					row.ColorText=Color.White;
				}
				else {
					if(rows[i].FieldDisplay!=null) {
						row.Cells.Add(rows[i].FieldDisplay);
					}
					else {
						row.Cells.Add(rows[i].FieldName);
					}
					row.Cells.Add(rows[i].OldValDisplay);
					cell=new ODGridCell(rows[i].NewValDisplay);
					if(rows[i].IsFlagged) {
						cell.ColorText=Color.Firebrick;
						cell.Bold=YN.Yes;
					}
					row.Cells.Add(cell);
					cell=new ODGridCell(rows[i].ImpValDisplay);
					if(rows[i].IsFlaggedImp) {
						cell.ColorText=Color.Firebrick;
						cell.Bold=YN.Yes;
					}
					row.Cells.Add(cell);
					if(rows[i].DoImport) {
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
				if(dictAcrobatFields.ContainsKey(fieldName)) {
					return dictAcrobatFields[fieldName];
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
				if(dictAcrobatFields.ContainsKey(fieldName)) {
					return dictAcrobatFields[fieldName];
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
				if(dictAcrobatFields.ContainsKey(fieldName)) {
					if(dictAcrobatFields[fieldName]=="true") {//need to test this
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
				while(dictAcrobatFields.ContainsKey(keyname)) {//not rigorously tested
					retVal.Add(dictAcrobatFields[keyname]);
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
					if(dictAcrobatFields.ContainsKey(fieldNames[f])) {
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
				foreach(string fieldkey in dictAcrobatFields.Keys){
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
			if(rows[e.Row].IsSeparator) {
				return;
			}
			if(!IsImportable(rows[e.Row])) {
				return;
			}
			rows[e.Row].DoImport=!rows[e.Row].DoImport;
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
			if(row.FieldName.StartsWith("ins1") || row.FieldName.StartsWith("ins2")) {
			  //if(patPlanList.Count>0) {
			  MsgBox.Show(this,"Insurance cannot be imported yet.");
			  return false;
			  //}
			}
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
			if(rows[e.Row].IsSeparator) {
				return;
			}
			if(!IsEditable(rows[e.Row])){
				return;
			}
			if(rows[e.Row].FieldName=="referredFrom") {
				FormReferralSelect formRS=new FormReferralSelect();
				formRS.IsSelectionMode=true;
				formRS.ShowDialog();
				if(formRS.DialogResult!=DialogResult.OK) {
					return;
				}
				Referral referralSelected=formRS.SelectedReferral;
				rows[e.Row].DoImport=true;
				rows[e.Row].IsFlaggedImp=false;
				rows[e.Row].ImpValDisplay=referralSelected.GetNameFL();
				rows[e.Row].ImpValObj=referralSelected;
			}
			else if(rows[e.Row].ObjType==typeof(string)
				|| rows[e.Row].ObjType==typeof(Allergy)
				|| rows[e.Row].ObjType==typeof(Disease)) 
			{
				InputBox inputbox=new InputBox(rows[e.Row].FieldName);
				inputbox.textResult.Text=rows[e.Row].ImpValDisplay;
				inputbox.ShowDialog();
				if(inputbox.DialogResult!=DialogResult.OK) {
					return;
				}
				if(rows[e.Row].FieldName=="addressAndHmPhoneIsSameEntireFamily") {
					if(inputbox.textResult.Text==""){
						AddressSameForFam=false;	
					}
					else if(inputbox.textResult.Text!="X") {
						AddressSameForFam=true;
					}
					else{
						MsgBox.Show(this,"The only allowed values are X or blank.");
						return;
					}
				}
				if(rows[e.Row].OldValDisplay==inputbox.textResult.Text) {//value is now same as original
					rows[e.Row].DoImport=false;
				}
				else {
					rows[e.Row].DoImport=true;
				}
				rows[e.Row].ImpValDisplay=inputbox.textResult.Text;
				rows[e.Row].ImpValObj=inputbox.textResult.Text;
			}
			else if(rows[e.Row].ObjType==typeof(Medication)) {
				if(rows[e.Row].ImpValObj.GetType()==typeof(long)) {
					FormMedications FormM=new FormMedications();
					FormM.IsSelectionMode=true;
					FormM.textSearch.Text=rows[e.Row].FieldName;
					FormM.ShowDialog();
					if(FormM.DialogResult!=DialogResult.OK) {
						return;
					}
					rows[e.Row].ImpValDisplay="Y";
					rows[e.Row].ImpValObj=FormM.SelectedMedicationNum;
					string descript=Medications.GetDescription(FormM.SelectedMedicationNum);
					rows[e.Row].FieldDisplay=descript;
					((SheetField)rows[e.Row].NewValObj).FieldValue=descript;
					rows[e.Row].NewValDisplay="Y";
					rows[e.Row].DoImport=true;
					rows[e.Row].IsFlaggedImp=false;
				}
				else {
					InputBox inputbox=new InputBox(rows[e.Row].FieldName);
					inputbox.textResult.Text=rows[e.Row].ImpValDisplay;
					inputbox.ShowDialog();
					if(inputbox.DialogResult!=DialogResult.OK) {
						return;
					}
					if(rows[e.Row].OldValDisplay==inputbox.textResult.Text) {//value is now same as original
						rows[e.Row].DoImport=false;
					}
					else {
						rows[e.Row].DoImport=true;
					}
					rows[e.Row].ImpValDisplay=inputbox.textResult.Text;
					rows[e.Row].ImpValObj=inputbox.textResult.Text;
				}
			}
			/*else if(rows[e.Row].ObjType.IsGenericType){//==typeof(Nullable)) {
				Type underlyingT=Nullable.GetUnderlyingType(rows[e.Row].ObjType);
				FormSheetImportEnumPicker formEnum=new FormSheetImportEnumPicker(rows[e.Row].FieldName);
				formEnum.ShowClearButton=true;
				for(int i=0;i<Enum.GetNames(underlyingT).Length;i++) {
					formEnum.comboResult.Items.Add(Enum.GetNames(underlyingT)[i]);
					if(rows[e.Row].ImpValObj!=null && i==(int)rows[e.Row].ImpValObj) {
						formEnum.comboResult.SelectedIndex=i;
					}
				}
				formEnum.ShowDialog();
				if(formEnum.DialogResult==DialogResult.OK) {
					int selectedI=formEnum.comboResult.SelectedIndex;
					if(rows[e.Row].ImpValObj==null) {//was initially null
						if(selectedI!=-1) {//an item was selected
							rows[e.Row].ImpValObj=Enum.ToObject(underlyingT,selectedI);
							rows[e.Row].ImpValDisplay=rows[e.Row].ImpValObj.ToString();
						}
					}
					else {//was not initially null
						if((int)rows[e.Row].ImpValObj!=selectedI) {//value was changed.
							if(selectedI==-1){
								rows[e.Row].ImpValObj=null;
								rows[e.Row].ImpValDisplay="";
							}
							else{
								rows[e.Row].ImpValObj=Enum.ToObject(underlyingT,selectedI);
								rows[e.Row].ImpValDisplay=rows[e.Row].ImpValObj.ToString();
							}
						}
					}
					if(patPlanList.Count>0) {
						rows[e.Row].DoImport=false;//can't change an existing plan from here.
					}
					else if(selectedI==-1) {
						if(rows[e.Row].OldValObj==null){
							rows[e.Row].DoImport=false;//no change
						}
						else{
							rows[e.Row].DoImport=true;
						}
					}
					else if((int)rows[e.Row].ImpValObj==(int)rows[e.Row].OldValObj) {//it's the old setting for the patient, whether or not they actually changed it.
						rows[e.Row].DoImport=false;//so no need to import
					}
					else {
						rows[e.Row].DoImport=true;
					}
				}
			}*/
			else if(rows[e.Row].ObjType.IsEnum) {
				//Note.  This only works for zero-indexed enums.
				FormSheetImportEnumPicker formEnum=new FormSheetImportEnumPicker(rows[e.Row].FieldName);
				for(int i=0;i<Enum.GetNames(rows[e.Row].ObjType).Length;i++) {
					formEnum.comboResult.Items.Add(Enum.GetNames(rows[e.Row].ObjType)[i]);
					if(rows[e.Row].ImpValObj!=null && i==(int)rows[e.Row].ImpValObj) {
						formEnum.comboResult.SelectedIndex=i;
					}
				}
				formEnum.ShowDialog();
				if(formEnum.DialogResult==DialogResult.OK) {
					int selectedI=formEnum.comboResult.SelectedIndex;
					if(rows[e.Row].ImpValObj==null) {//was initially null
						if(selectedI!=-1) {//an item was selected
							rows[e.Row].ImpValObj=Enum.ToObject(rows[e.Row].ObjType,selectedI);
							rows[e.Row].ImpValDisplay=rows[e.Row].ImpValObj.ToString();
						}
					}
					else {//was not initially null
						if((int)rows[e.Row].ImpValObj!=selectedI) {//value was changed.
							//There's no way for the user to set it to null, so we do not need to test that
							rows[e.Row].ImpValObj=Enum.ToObject(rows[e.Row].ObjType,selectedI);
							rows[e.Row].ImpValDisplay=rows[e.Row].ImpValObj.ToString();
						}
					}
					if(selectedI==-1) {
						rows[e.Row].DoImport=false;//impossible to import a null
					}
					else if((int)rows[e.Row].ImpValObj==(int)rows[e.Row].OldValObj) {//it's the old setting for the patient, whether or not they actually changed it.
						rows[e.Row].DoImport=false;//so no need to import
					}
					else {
						rows[e.Row].DoImport=true;
					}
				}
			}
			else if(rows[e.Row].ObjType==typeof(DateTime)) {//this is only for one field so far: Birthdate
				InputBox inputbox=new InputBox(rows[e.Row].FieldName);
				inputbox.textResult.Text=rows[e.Row].ImpValDisplay;
				inputbox.ShowDialog();
				if(inputbox.DialogResult!=DialogResult.OK) {
					return;
				}
				DateTime enteredDate;
				if(inputbox.textResult.Text=="") {
					enteredDate=DateTime.MinValue;
					rows[e.Row].ImpValObj=enteredDate;
					rows[e.Row].ImpValDisplay="";
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
					rows[e.Row].ImpValObj=enteredDate;
					rows[e.Row].ImpValDisplay=enteredDate.ToShortDateString();
				}
				if(rows[e.Row].ImpValDisplay==rows[e.Row].OldValDisplay) {//value is now same as original
					rows[e.Row].DoImport=false;
				}
				else {
					rows[e.Row].DoImport=true;
				}
			}
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool importsPresent=false;
			for(int i=0;i<rows.Count;i++) {
				if(rows[i].DoImport) {
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
				Patient patientOld=pat.Copy();
				for(int i=0;i<rows.Count;i++) {
					if(!rows[i].DoImport) {
						continue;
					}
					switch(rows[i].FieldName) {
						case "LName":
							pat.LName=rows[i].ImpValDisplay;
							break;
						case "FName":
							pat.FName=rows[i].ImpValDisplay;
							break;
						case "MiddleI":
							pat.MiddleI=rows[i].ImpValDisplay;
							break;
						case "Preferred":
							pat.Preferred=rows[i].ImpValDisplay;
							break;
						case "Gender":
							pat.Gender=(PatientGender)rows[i].ImpValObj;
							break;
						case "Position":
							pat.Position=(PatientPosition)rows[i].ImpValObj;
							break;
						case "Birthdate":
							pat.Birthdate=(DateTime)rows[i].ImpValObj;
							break;
						case "SSN":
							pat.SSN=rows[i].ImpValDisplay;
							break;
						case "WkPhone":
							pat.WkPhone=rows[i].ImpValDisplay;
							break;
						case "WirelessPhone":
							pat.WirelessPhone=rows[i].ImpValDisplay;
							break;
						case "Email":
							pat.Email=rows[i].ImpValDisplay;
							break;
						case "PreferContactMethod":
							pat.PreferContactMethod=(ContactMethod)rows[i].ImpValObj;
							break;
						case "PreferConfirmMethod":
							pat.PreferConfirmMethod=(ContactMethod)rows[i].ImpValObj;
							break;
						case "PreferRecallMethod":
							pat.PreferRecallMethod=(ContactMethod)rows[i].ImpValObj;
							break;
						case "referredFrom":
							RefAttach ra=new RefAttach();
							ra.IsFrom=true;
							ra.ItemOrder=1;
							ra.PatNum=pat.PatNum;
							ra.RefDate=DateTime.Today;
							ra.ReferralNum=((Referral)rows[i].ImpValObj).ReferralNum;
							RefAttaches.Insert(ra);
							SecurityLogs.MakeLogEntry(Permissions.RefAttachAdd,pat.PatNum,"Referred From "+Referrals.GetNameFL(ra.ReferralNum));//no security to block this action.
							break;
						//AddressSameForFam already set, but not really importable by itself
						case "Address":
							pat.Address=rows[i].ImpValDisplay;
							break;
						case "Address2":
							pat.Address2=rows[i].ImpValDisplay;
							break;
						case "City":
							pat.City=rows[i].ImpValDisplay;
							break;
						case "State":
							pat.State=rows[i].ImpValDisplay;
							break;
						case "Zip":
							pat.Zip=rows[i].ImpValDisplay;
							break;
						case "HmPhone":
							pat.HmPhone=rows[i].ImpValDisplay;
							break;

						//ins1 and ins2 do not get imported.
					}
				}
				Patients.Update(pat,patientOld);
				if(AddressSameForFam) {
					Patients.UpdateAddressForFam(pat);
				}
			}
			#endregion
			#region Medical History
			else if(SheetCur.SheetType==SheetTypeEnum.MedicalHistory) {
				for(int i=0;i<rows.Count;i++) {
					if(!rows[i].DoImport) {
						continue;
					}
					if(rows[i].ObjType==null) {//Should never happen.
						continue;
					}
					bool hasValue=false;
					if(rows[i].ImpValDisplay!="") {
						hasValue=true;
					}
					#region Allergies
					if(rows[i].ObjType==typeof(Allergy)) {
						//Patient has this allergy in the db so just update the value.
						if(rows[i].OldValObj!=null) {
							Allergy oldAllergy=(Allergy)rows[i].OldValObj;
							oldAllergy.StatusIsActive=hasValue;
							Allergies.Update(oldAllergy);
							continue;
						}
						if(!hasValue) {
							continue;
						}
						//Allergy does not exist for this patient yet so create one.
						List<AllergyDef> allergyList=AllergyDefs.GetAll(false);
						SheetField allergySheet=(SheetField)rows[i].NewValObj;
						//Find what allergy user wants to import.
						for(int j=0;j<allergyList.Count;j++) {
							if(allergyList[j].Description==allergySheet.FieldName.Remove(0,8)) {
								Allergy newAllergy=new Allergy();
								newAllergy.AllergyDefNum=allergyList[j].AllergyDefNum;
								newAllergy.PatNum=pat.PatNum;
								newAllergy.StatusIsActive=true;
								Allergies.Insert(newAllergy);
								break;
							}
						}
					}
					#endregion
					#region Medications
					else if(rows[i].ObjType==typeof(Medication)) {
					  //Patient has this medication in the db so leave it alone or set the stop date.
					  if(rows[i].OldValObj!=null) {
					    //Set the stop date for the current medication(s).
					    Medication oldMed=(Medication)rows[i].OldValObj;
					    List<MedicationPat> patMeds=MedicationPats.GetMedicationPatsByMedicationNum(oldMed.MedicationNum,pat.PatNum);
					    for(int j=0;j<patMeds.Count;j++) {
					      if(rows[i].ImpValDisplay=="Y") {
					        //Check if med is currently inactive.
					        if(patMeds[j].DateStop.Year>1880 && patMeds[j].DateStop<=DateTime.Now) {
					          patMeds[j].DateStop=new DateTime(0001,1,1);//This will activate the med.
					        }
					      }
								else if(rows[i].ImpValDisplay=="N") {
									//Set the med as inactive.
									patMeds[j].DateStop=DateTime.Now;
								}
								else {
									continue;
								}
					      MedicationPats.Update(patMeds[j]);
					    }
					    continue;
					  }
						if(rows[i].ImpValDisplay=="" || (rows[i].OldValDisplay=="N" && rows[i].ImpValDisplay=="N")) {
							continue;
						}
					  //Medication does not exist for this patient yet so create it.
					  List<Medication> medList=Medications.GetList("");
					  SheetField medSheet=(SheetField)rows[i].NewValObj;
					  //Find what allergy user wants to import.
					  for(int j=0;j<medList.Count;j++) {
					    if(Medications.GetDescription(medList[j].MedicationNum)==medSheet.FieldValue) {
					      MedicationPat medPat=new MedicationPat();
					      medPat.PatNum=pat.PatNum;
					      medPat.MedicationNum=medList[j].MedicationNum;
					      MedicationPats.Insert(medPat);
					      break;
					    }
					  }
					}
					#endregion
					#region Diseases
					else if(rows[i].ObjType==typeof(Disease)) {
						//Patient has this problem in the db so just update the value.
						if(rows[i].OldValObj!=null) {
							Disease oldDisease=(Disease)rows[i].OldValObj;
							if(hasValue) {
								oldDisease.ProbStatus=ProblemStatus.Active;
							}
							else {
								oldDisease.ProbStatus=ProblemStatus.Inactive;
							}
							Diseases.Update(oldDisease);
							continue;
						}
						if(!hasValue) {
							continue;
						}
						//Problem does not exist for this patient yet so create one.
						SheetField diseaseSheet=(SheetField)rows[i].NewValObj;
						//Find what allergy user wants to import.
						for(int j=0;j<DiseaseDefs.List.Length;j++) {
							if(DiseaseDefs.List[j].DiseaseName==diseaseSheet.FieldName.Remove(0,8)) {
								Disease newDisease=new Disease();
								newDisease.PatNum=pat.PatNum;
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
			for(int i=0;i<rows.Count;i++) {
				if(rows[i].FieldName!=fieldName) {
					continue;
				}
				return rows[i].DoImport;
			}
			return false;
		}

		///<summary>Will return null if field not found or if field marked to not import.</summary>
		private object GetImpObj(string fieldName) {
			for(int i=0;i<rows.Count;i++) {
				if(rows[i].FieldName!=fieldName) {
					continue;
				}
				if(!rows[i].DoImport) {
					return null;
				}
				return rows[i].ImpValObj;
			}
			return null;
		}

		///<summary>Will return empty string field not found or if field marked to not import.</summary>
		private string GetImpDisplay(string fieldName) {
			for(int i=0;i<rows.Count;i++) {
				if(rows[i].FieldName!=fieldName) {
					continue;
				}
				if(!rows[i].DoImport) {
					return "";
				}
				return rows[i].ImpValDisplay;
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