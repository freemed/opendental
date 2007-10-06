using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text; 
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class Shared{

		///<summary></summary>
		public Shared(){
			
		}

		///<summary>Converts a date to an age. If age is over 115, then returns 0.</summary>
		public static int DateToAge(DateTime date){
			if(date.Year<1880)
				return 0;
			if(date.Month < DateTime.Now.Month){//birthday in previous month
				return DateTime.Now.Year-date.Year;
			}
			if(date.Month == DateTime.Now.Month && date.Day <= DateTime.Now.Day){//birthday in this month
				return DateTime.Now.Year-date.Year;
			}
			return DateTime.Now.Year-date.Year-1;
		}

		///<summary>Converts a date to an age. If age is over 115, then returns 0.</summary>
		public static int DateToAge(DateTime birthdate,DateTime asofDate) {
			if(birthdate.Year<1880)
				return 0;
			if(birthdate.Month < asofDate.Month) {//birthday in previous month
				return asofDate.Year-birthdate.Year;
			}
			if(birthdate.Month == asofDate.Month && birthdate.Day <= asofDate.Day) {//birthday in this month
				return asofDate.Year-birthdate.Year;
			}
			return asofDate.Year-birthdate.Year-1;
		}

		///<summary></summary>
		public static string AgeToString(int age){
			if(age==0)
				return "";
			else
				return age.ToString();
		}

		///<summary>Converts numbers to ordinals.  For example, 120 to 120th, 73 to 73rd.  Probably doesn't work too well with foreign language translations.  Used in the Birthday postcards.</summary>
		public static string NumberToOrdinal(int number){
			string str=number.ToString();
			string last=str.Substring(str.Length-1);
			switch(last){
				case "0":
				case "4":
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					return str+"th";
				case "1":
					return str+"st";
				case "2":
					return str+"nd";
				case "3":
					return str+"rd";
			}
			return "";//will never happen
		}

		///<summary>Computes balance for a single patient without making any calls to the database. If the balance doesn't match the stored patient balance, then it makes one update to the database and returns true to trigger calculation of aging.</summary>
		public static bool ComputeBalances(Procedure[] procList,ClaimProc[] claimProcList,Patient PatCur,PaySplit[] paySplitList,Adjustment[] AdjustmentList,PayPlan[] payPlanList,PayPlanCharge[] payPlanChargeList){
			//must have refreshed all 5 first
			double calcBal
				=Procedures.ComputeBal(procList)
				+ClaimProcs.ComputeBal(claimProcList)
				+Adjustments.ComputeBal(AdjustmentList)
				-PaySplits.ComputeBal(paySplitList)
				+PayPlans.ComputeBal(PatCur.PatNum,payPlanList,payPlanChargeList);
			if(calcBal!=PatCur.EstBalance){
				Patient PatOld=PatCur.Copy();
				PatCur.EstBalance=calcBal;
				Patients.Update(PatCur,PatOld);
				return true;
			}
			return false;
		}

	}

/*=================================Class DataValid=========================================
===========================================================================================*/

	///<summary>Handles a global event to keep local data synchronized.</summary>
	public class DataValid{

		///<summary></summary>
		public static event OpenDental.ValidEventHandler BecameInvalid;	

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(InvalidTypes itypes){
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,itypes,false));
		}

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(DateTime date){
			OnBecameInvalid(new OpenDental.ValidEventArgs(date,InvalidTypes.Date,false));
		}

		///<summary>Triggers an event that causes a signal to be sent to all other computers telling them what kind of locally stored data needs to be updated.  Either supply a set of flags for the types, or supply a date if the appointment screen needs to be refreshed.  Yes, this does immediately refresh the local data, too.  The AllLocal override does all types except appointment date for the local computer only, such as when starting up.</summary>
		public static void SetInvalid(bool onlyLocal){
			OnBecameInvalid(new OpenDental.ValidEventArgs(DateTime.MinValue,InvalidTypes.AllLocal,true));
		}

		///<summary></summary>
		protected static void OnBecameInvalid(OpenDental.ValidEventArgs e){
			if(BecameInvalid !=null){
				BecameInvalid(e);
			}
		}

	}

	///<summary></summary>
	public delegate void ValidEventHandler(ValidEventArgs e);

	///<summary></summary>
	public class ValidEventArgs : System.EventArgs{
		private DateTime dateViewing;
		private InvalidTypes itypes;
		private bool onlyLocal;
		
		///<summary></summary>
		public ValidEventArgs(DateTime dateViewing, InvalidTypes itypes,bool onlyLocal) : base(){
			this.dateViewing=dateViewing;
			this.itypes=itypes;
			this.onlyLocal=onlyLocal;
		}

		///<summary></summary>
		public DateTime DateViewing{
			get{return dateViewing;}
		}

		///<summary></summary>
		public InvalidTypes ITypes{
			get{return itypes;}
		}

		///<summary></summary>
		public bool OnlyLocal{
			get{return onlyLocal;}
		}

	}

	/*=================================Class GotoModule==================================================
	===========================================================================================*/

	///<summary>Used to trigger a global event to jump between modules and perform actions in other modules.</summary>
	public class GotoModule{
		///<summary></summary>
		public static event ModuleEventHandler ModuleSelected;

		/*
		///<summary>This triggers a global event which the main form responds to by going directly to a module.</summary>
		public static void GoNow(DateTime dateSelected,Appointment pinAppt,int selectedAptNum,int iModule,int claimNum) {
			OnModuleSelected(new ModuleEventArgs(dateSelected,pinAppt,selectedAptNum,iModule,claimNum));
		}*/

		///<summary>Goes directly to an existing appointment.</summary>
		public static void GotoAppointment(DateTime dateSelected,int selectedAptNum) {
			OnModuleSelected(new ModuleEventArgs(dateSelected,0,selectedAptNum,0,0));
		}

		///<summary>Goes directly to a claim in someone's Account.</summary>
		public static void GotoClaim(int claimNum){
			OnModuleSelected(new ModuleEventArgs(DateTime.MinValue,0,0,2,claimNum));
		}

		///<summary>Goes directly to an Account.  Patient should already have been selected</summary>
		public static void GotoAccount() {
			OnModuleSelected(new ModuleEventArgs(DateTime.MinValue,0,0,2,0));
		}

		///<summary>Puts appointment on pinboard, then jumps to Appointments module.  Patient should already have been selected</summary>
		public static void PinToAppt(int pinAptNum) {
			OnModuleSelected(new ModuleEventArgs(DateTime.Today,pinAptNum,0,0,0));
		}

		///<summary></summary>
		protected static void OnModuleSelected(ModuleEventArgs e){
			if(ModuleSelected !=null){
				ModuleSelected(e);
			}
		}
	}

	///<summary>This is used for our global module events.</summary>
	public delegate void ModuleEventHandler(ModuleEventArgs e);

	///<summary></summary>
	public class ModuleEventArgs : System.EventArgs{
		private DateTime dateSelected;
		private int pinAppt;
		private int selectedAptNum;
		private int iModule;
		private int claimNum;
		
		///<summary></summary>
		public ModuleEventArgs(DateTime dateSelected,int pinAppt,int selectedAptNum,int iModule,
			int claimNum) : base()
		{
			this.dateSelected=dateSelected;
			this.pinAppt=pinAppt;
			this.selectedAptNum=selectedAptNum;
			this.iModule=iModule;
			this.claimNum=claimNum;
		}

		///<summary>If going to the ApptModule, this lets you pick a date.</summary>
		public DateTime DateSelected{
			get{return dateSelected;}
		}

		///<summary>The aptNum of the appointment that we want to put on the pinboard of the Apt Module.</summary>
		public int PinAppt{
			get{return pinAppt;}
		}

		///<summary></summary>
		public int SelectedAptNum{
			get{return selectedAptNum;}
		}

		///<summary></summary>
		public int IModule{
			get{return iModule;}
		}

		///<summary>If going to Account module, this lets you pick a claim.</summary>
		public int ClaimNum{
			get{return claimNum;}
		}
	}

	/*=================================Class TelephoneNumbers============================================*/

	///<summary></summary>
	public class TelephoneNumbers{

		///<summary>Used in the tool that loops through the database fixing telephone numbers.  Also used in the patient import from XML tool, and PT Dental bridge.</summary>
		public static string ReFormat(string phoneNum){
			if(CultureInfo.CurrentCulture.Name!="en-US" && CultureInfo.CurrentCulture.Name.Substring(3)!="CA"){
				return phoneNum;
			}
			Regex regex;
			regex=new Regex(@"^\d{10}");//eg. 5033635432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(3,3)+"-"+phoneNum.Substring(6);
			}
			regex=new Regex(@"^\d{3}-\d{3}-\d{4}");//eg. 503-363-5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4);
			}
			regex=new Regex(@"^\d-\d{3}-\d{3}-\d{4}");//eg. 1-503-363-5432 to 1(503)363-5432
			if(regex.IsMatch(phoneNum)){
				return phoneNum.Substring(0,1)+"("+phoneNum.Substring(2,3)+")"+phoneNum.Substring(6);
			}
			regex=new Regex(@"^\d{3} \d{3}-\d{4}");//eg 503 363-5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4);
			}
			//Keyush Shah 04/21/05 Added more formats:
			regex=new Regex(@"^\d{3} \d{3} \d{4}");//eg 916 363 5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4,3)+"-"+phoneNum.Substring(8,4);
			}
      regex=new Regex(@"^\(\d{3}\) \d{3} \d{4}");//eg (916) 363 5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(1,3)+")"+phoneNum.Substring(6,3)+"-"+phoneNum.Substring(10,4);
			}
			regex=new Regex(@"^\(\d{3}\) \d{3}-\d{4}");//eg (916) 363-5432
			if(regex.IsMatch(phoneNum)){
				return "("+phoneNum.Substring(1,3)+")"+phoneNum.Substring(6,3)+"-"+phoneNum.Substring(10,4);
			}
			regex=new Regex(@"^\d{7}$");//eg 3635432
			if(regex.IsMatch(phoneNum)){
				return(phoneNum.Substring(0,3)+"-"+phoneNum.Substring(3));
			}
			return phoneNum;   
		}

		///<summary>reformats initial entry with each keystroke</summary>
		public static string AutoFormat(string phoneNum){
			if(CultureInfo.CurrentCulture.Name!="en-US"	&& CultureInfo.CurrentCulture.Name.Substring(3)!="CA"){
				return phoneNum;
			}
			if(Regex.IsMatch(phoneNum,@"^[2-9]$")){
				return "("+phoneNum;
			}
			if(Regex.IsMatch(phoneNum,@"^1\d$")){
				return "1("+phoneNum.Substring(1);
			}
			if(Regex.IsMatch(phoneNum,@"^\(\d\d\d\d$")){
				return( phoneNum.Substring(0,4)+")"+phoneNum.Substring(4));
			}
			if(Regex.IsMatch(phoneNum,@"^1\(\d\d\d\d$")){
				return( phoneNum.Substring(0,5)+")"+phoneNum.Substring(5));
			}
			if(Regex.IsMatch(phoneNum,@"^\(\d\d\d\)\d\d\d\d$")){
				return( phoneNum.Substring(0,8)+"-"+phoneNum.Substring(8));
			}
			if(Regex.IsMatch(phoneNum,@"^1\(\d\d\d\)\d\d\d\d$")){
				return( phoneNum.Substring(0,9)+"-"+phoneNum.Substring(9));
			}
			return phoneNum;
		}

		///<Summary>Also truncates if more than two non-numbers in a row.  This is to avoid the notes that can follow phone numbers.</Summary>
		public static string FormatNumbersOnly(string phoneStr){
			string retVal="";
			int nonnumcount=0;
			for(int i=0;i<phoneStr.Length;i++) {
				if(nonnumcount==2){
					return retVal;
				}
				if(Char.IsNumber(phoneStr,i)) {
					retVal+=phoneStr.Substring(i,1);
					nonnumcount=0;
				}
				else{
					nonnumcount++;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static string FormatNumbersExactTen(string phoneNum){
			string newPhoneNum="";
			for(int i=0;i<phoneNum.Length;i++){
				if(Char.IsNumber(phoneNum,i)){
					newPhoneNum+=phoneNum.Substring(i,1);
				}
			}
			if(newPhoneNum.Length==10){
				return newPhoneNum;
			}
			else{
				return "";
			}
		}

	}

	

}
