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
			rows=new List<SheetImportRow>();
			SheetImportRow row;
			string fieldVal;
			row=new SheetImportRow();
			row.FieldName="Personal";
			row.IsSeparator=true;
			rows.Add(row);
			#region personal
			//LName---------------------------------------------
			fieldVal=GetInputValue("LName");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="LName";
				row.OldValDisplay=pat.LName;
				row.OldValObj=pat.LName;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//FName---------------------------------------------
			fieldVal=GetInputValue("FName");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="FName";
				row.OldValDisplay=pat.FName;
				row.OldValObj=pat.FName;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//MiddleI---------------------------------------------
			fieldVal=GetInputValue("MiddleI");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="MiddleI";
				row.OldValDisplay=pat.MiddleI;
				row.OldValObj=pat.MiddleI;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Preferred---------------------------------------------
			fieldVal=GetInputValue("Preferred");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Preferred";
				row.OldValDisplay=pat.Preferred;
				row.OldValObj=pat.Preferred;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Gender---------------------------------------------
			fieldVal=GetRadioValue("Gender");
			if(fieldVal!=null) {//field exists on form
				row=new SheetImportRow();
				row.FieldName="Gender";
				row.OldValDisplay=Lan.g("enumPatientGender",pat.Gender.ToString());
				row.OldValObj=pat.Gender;
				if(fieldVal=="") {//no box was checked
					row.NewValDisplay="";
					row.NewValObj=null;
				}
				else {
					try {
						PatientGender gender=(PatientGender)Enum.Parse(typeof(PatientGender),fieldVal);
						row.NewValDisplay=Lan.g("enumPatientGender",gender.ToString());
						row.NewValObj=gender;
					}
					catch {
						MessageBox.Show(fieldVal+Lan.g(this," is not a valid gender."));
					}
				}
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(PatientGender);
				if(row.NewValObj!=null && (PatientGender)row.NewValObj!=pat.Gender) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Position---------------------------------------------
			fieldVal=GetRadioValue("Position");
			if(fieldVal!=null) {//field exists on form
				row=new SheetImportRow();
				row.FieldName="Position";
				row.OldValDisplay=Lan.g("enumPatientPositionr",pat.Position.ToString());
				row.OldValObj=pat.Position;
				if(fieldVal=="") {//no box was checked
					row.NewValDisplay="";
					row.NewValObj=null;
				}
				else {
					try {
						PatientPosition position=(PatientPosition)Enum.Parse(typeof(PatientPosition),fieldVal);
						row.NewValDisplay=Lan.g("enumPatientPosition",position.ToString());
						row.NewValObj=position;
					}
					catch {
						MessageBox.Show(fieldVal+Lan.g(this," is not a valid PatientPosition."));
					}
				}
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(PatientPosition);
				if(row.NewValObj!=null && (PatientPosition)row.NewValObj!=pat.Position) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Birthdate---------------------------------------------
			fieldVal=GetInputValue("Birthdate");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Birthdate";
				if(pat.Birthdate.Year<1880) {
					row.OldValDisplay="";
				}
				else {
					row.OldValDisplay=pat.Birthdate.ToShortDateString();
				}
				row.OldValObj=pat.Birthdate;
				row.NewValObj=PIn.Date(fieldVal);
				if(((DateTime)row.NewValObj).Year<1880) {
					row.NewValDisplay="";
				}
				else {
					row.NewValDisplay=((DateTime)row.NewValObj).ToShortDateString();
				}
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(DateTime);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//SSN---------------------------------------------
			fieldVal=GetInputValue("SSN");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="SSN";
				row.OldValDisplay=pat.SSN;
				row.OldValObj=pat.SSN;
				row.NewValDisplay=fieldVal.Replace("-","");//quickly strip dashes
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//WkPhone---------------------------------------------
			fieldVal=GetInputValue("WkPhone");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="WkPhone";
				row.OldValDisplay=pat.WkPhone;
				row.OldValObj=pat.WkPhone;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//WirelessPhone---------------------------------------------
			fieldVal=GetInputValue("WirelessPhone");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="WirelessPhone";
				row.OldValDisplay=pat.WirelessPhone;
				row.OldValObj=pat.WirelessPhone;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//wirelessCarrier---------------------------------------------
			fieldVal=GetInputValue("wirelessCarrier");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="wirelessCarrier";
				row.OldValDisplay="";
				row.OldValObj="";
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				row.DoImport=false;
				row.IsFlagged=true;//if user entered nothing, the red text won't show anyway.
				rows.Add(row);
			}
			//Email---------------------------------------------
			fieldVal=GetInputValue("Email");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Email";
				row.OldValDisplay=pat.Email;
				row.OldValObj=pat.Email;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//PreferContactMethod---------------------------------------------
			fieldVal=GetRadioValue("PreferContactMethod");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="PreferContactMethod";
				row.OldValDisplay=Lan.g("enumContactMethod",pat.PreferContactMethod.ToString());
				row.OldValObj=pat.PreferContactMethod;
				if(fieldVal=="") {
					row.NewValDisplay="";
					row.NewValObj=null;
				}
				else {
					try {
						ContactMethod cmeth=(ContactMethod)Enum.Parse(typeof(ContactMethod),fieldVal);
						row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
						row.NewValObj=cmeth;
					}
					catch {
						MessageBox.Show(fieldVal+Lan.g(this," is not a valid ContactMethod."));
					}
				}
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(ContactMethod);
				if(row.NewValObj!=null && (ContactMethod)row.NewValObj!=pat.PreferContactMethod) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//PreferConfirmMethod---------------------------------------------
			fieldVal=GetRadioValue("PreferConfirmMethod");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="PreferConfirmMethod";
				row.OldValDisplay=Lan.g("enumContactMethod",pat.PreferConfirmMethod.ToString());
				row.OldValObj=pat.PreferConfirmMethod;
				if(fieldVal=="") {
					row.NewValDisplay="";
					row.NewValObj=null;
				}
				else {
					try {
						ContactMethod cmeth=(ContactMethod)Enum.Parse(typeof(ContactMethod),fieldVal);
						row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
						row.NewValObj=cmeth;
					}
					catch {
						MessageBox.Show(fieldVal+Lan.g(this," is not a valid ContactMethod."));
					}
				}
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(ContactMethod);
				if(row.NewValObj!=null && (ContactMethod)row.NewValObj!=pat.PreferConfirmMethod) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//PreferRecallMethod---------------------------------------------
			fieldVal=GetRadioValue("PreferRecallMethod");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="PreferRecallMethod";
				row.OldValDisplay=Lan.g("enumContactMethod",pat.PreferRecallMethod.ToString());
				row.OldValObj=pat.PreferRecallMethod;
				if(fieldVal=="") {
					row.NewValDisplay="";
					row.NewValObj=null;
				}
				else {
					try {
						ContactMethod cmeth=(ContactMethod)Enum.Parse(typeof(ContactMethod),fieldVal);
						row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
						row.NewValObj=cmeth;
					}
					catch {
						MessageBox.Show(fieldVal+Lan.g(this," is not a valid ContactMethod."));
					}
				}
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(ContactMethod);
				if(row.NewValObj!=null && (ContactMethod)row.NewValObj!=pat.PreferRecallMethod) {
					row.DoImport=true;
				}
				rows.Add(row);
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
			row=new SheetImportRow();
			row.FieldName="Address and Home Phone";
			row.IsSeparator=true;
			rows.Add(row);
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
				row=new SheetImportRow();
				row.FieldName="Address";
				row.OldValDisplay=pat.Address;
				row.OldValObj=pat.Address;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Address2---------------------------------------------
			fieldVal=GetInputValue("Address2");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Address2";
				row.OldValDisplay=pat.Address2;
				row.OldValObj=pat.Address2;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//City---------------------------------------------
			fieldVal=GetInputValue("City");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="City";
				row.OldValDisplay=pat.City;
				row.OldValObj=pat.City;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//State---------------------------------------------
			fieldVal=GetInputValue("State");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="State";
				row.OldValDisplay=pat.State;
				row.OldValObj=pat.State;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Zip---------------------------------------------
			fieldVal=GetInputValue("Zip");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Zip";
				row.OldValDisplay=pat.Zip;
				row.OldValObj=pat.Zip;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//HmPhone---------------------------------------------
			fieldVal=GetInputValue("HmPhone");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="HmPhone";
				row.OldValDisplay=pat.HmPhone;
				row.OldValObj=pat.HmPhone;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ImpValDisplay=row.NewValDisplay;
				row.ImpValObj=row.NewValObj;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			#endregion address
			//Separator-------------------------------------------
			row=new SheetImportRow();
			row.FieldName="Insurance Policy 1";
			row.IsSeparator=true;
			rows.Add(row);
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
				if(plan1==null){
					row.OldValDisplay="";
				}
				else{
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
			row=new SheetImportRow();
			row.FieldName="Insurance Policy 2";
			row.IsSeparator=true;
			rows.Add(row);
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
				if(plan2==null){
					row.OldValDisplay="";
				}
				else{
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
			row=new SheetImportRow();
			row.FieldName="Misc";
			row.IsSeparator=true;
			rows.Add(row);
			//misc----------------------------------------------------
			List<string> miscVals=GetMiscValues();
			for(int i=0;i<miscVals.Count;i++) {
				fieldVal=miscVals[i];
				row=new SheetImportRow();
				row.FieldName="misc";
				row.FieldDisplay="misc"+(i+1).ToString();
				row.OldValDisplay="";
				row.OldValObj="";
				row.NewValDisplay=fieldVal;
				row.NewValObj="";
				row.ImpValDisplay="";
				row.ImpValObj="";
				row.ObjType=typeof(string);
				row.DoImport=false;
				row.IsFlagged=true;
				rows.Add(row);
			}
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
			else if(rows[e.Row].ObjType==typeof(string)) {
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
			Patient patientOld=pat.Copy();
			for(int i=0;i<rows.Count;i++) {
				if(!rows[i].DoImport) {
					continue;
				}
				switch(rows[i].FieldName){
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