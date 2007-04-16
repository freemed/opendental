using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims {
	public class CanadianOutput {
		///<summary>The result is a string which can be dropped into the insplan.BenefitNotes.  Or it might throw an exception if invalid data.  This class is also responsible for saving the returned message to the etrans table and printing out the required form.</summary>
		public static string SendElegibility(string electID,int patNum,string groupNumber, string divisionNo,
			string subscriberID, string patID,Relat patRelat, int subscNum, string dentaideCardSequence)
		{
			//Note: This might be the only class of this kind that returns a string.  It's a special situation.
			//We are simply not going to bother with language translation here.
			//determine carrier.
			Carrier carrier=Carriers.GetCanadian(electID);//this also happens to validate missing or short value
			if(carrier==null){
				throw new ApplicationException("Invalid carrier EDI code.");
			}
			Clearinghouse clearhouse=Canadian.GetClearinghouse();
			if(clearhouse==null){
				throw new ApplicationException("Canadian clearinghouse not found.");
			}
//warning, might not work if trailing slash is missing.
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				throw new ApplicationException(saveFolder+" not found.");
			}
			//Initialize objects-----------------------------------------------------------------------------------------------
			Patient patient=Patients.GetPat(patNum);
			Patient subscriber=Patients.GetPat(subscNum);
			Provider treatProv=Providers.GetProv(Patients.GetProvNum(patient));
			Provider billProv=Providers.GetProv(Providers.GetBillingProvNum(treatProv.ProvNum));
			//I had to use a dialog box to get the eligibility code.

			//validate any missing info----------------------------------------------------------------------------------
			string error="";
			if(carrier.CanadianNetworkNum==0){
				if(error!="") error+=", ";
				error+="Carrier does not have network specified";
			}
			if(!Regex.IsMatch(carrier.ElectID,@"^[0-9]{6}$")){//not necessary, but nice
				if(error!="") error+=", ";
				error+="CarrierId 6 digits";
			}
			if(treatProv.NationalProvID.Length!=9) {
				if(error!="")	error+=", ";
				error+="TreatingProv CDA num 9 digits";
			}
			if(treatProv.CanadianOfficeNum.Length!=4) {
				if(error!="") error+=", ";
				error+="TreatingProv office num 4 char";
			}
			if(billProv.NationalProvID.Length!=9) {
				if(error!="") error+=", ";
				error+="BillingProv CDA num 9 digits";
			}
			if(groupNumber.Length==0 || groupNumber.Length>12 || groupNumber.Contains(" ")){
				if(error!="") error+=", ";
				error+="Plan Number";
			}
			if(subscriberID==""){
				if(error!="") error+=", ";
				error+="SubscriberID";
			}
			if(patNum != subscNum && patRelat==Relat.Self) {//if patient is not subscriber, and relat is self
				if(error!="") error+=", ";
				error+="Relationship cannot be self";
			}
			if(patient.Gender==PatientGender.Unknown){
				if(error!="") error+=", ";
				error+="Patient gender";
			}
			if(patient.Birthdate.Year<1880 || patient.Birthdate>DateTime.Today) {
				if(error!="") error+=", ";
				error+="Patient birthdate";
			}
			if(patient.LName=="") {
				if(error!="") error+=", ";
				error+="Patient lastname";
			}
			if(patient.FName=="") {
				if(error!="") error+=", ";
				error+="Patient firstname";
			}
			if(subscriber.Birthdate.Year<1880 || subscriber.Birthdate>DateTime.Today) {
				if(error!="") error+=", ";
				error+="Subscriber birthdate";
			}
			if(subscriber.LName=="") {
				if(error!="") error+=", ";
				error+="Subscriber lastname";
			}
			if(subscriber.FName=="") {
				if(error!="") error+=", ";
				error+="Subscriber firstname";
			}
			if(error!="") {
				throw new ApplicationException(error);
			}
			FormCanadianEligibility FormElig=new FormCanadianEligibility();
			FormElig.ShowDialog();
			if(FormElig.DialogResult!=DialogResult.OK){
				throw new ApplicationException("Eligibility Code or Date missing.");
			}
			//eligiblity code guaranteed to not be 0 at this point.  Also date will be between 1980 and 10 years from now.
			Etrans etrans=Etranss.CreateCanadianOutput(patNum,carrier.CarrierNum,carrier.CanadianNetworkNum,
				clearhouse.ClearinghouseNum,EtransType.Eligibility_CA);
			string txt="";
			//create message----------------------------------------------------------------------------------------------
			//A01 transaction prefix 12 AN
//todo
			txt+="123456789012";//To be later provided by the individual network.
			//A02 office sequence number 6 N
			txt+=Canadian.TidyN(etrans.OfficeSequenceNumber,6);
			//A03 format version number 2 N
			txt+="04";
			//A04 transaction code 2 N
			txt+="08";//eligibility
			//A05 carrier id number 6 N
			txt+=carrier.ElectID;//already validated as 6 digit number.
			//A06 software system id 3 AN  The third character is for version of OD.
//todo
			txt+="OD1";//To be later supplied by CDAnet staff to uniquely identify OD.
			//A10 encryption method 1 N
//todo
			txt+="1";
			//A07 message length 5 N
			int len=214;
//todo does not account for C19. Possibly 30 more.
			//if(C19 is used, Plan Record){
			//len+=30;
			//}
			txt+=Canadian.TidyN(len,5);
			//A09 carrier transaction counter 5 N
			txt+=Canadian.TidyN(etrans.CarrierTransCounter,5);
			//B01 CDA provider number 9 AN
			txt+=Canadian.TidyAN(treatProv.NationalProvID,9);//already validated
			//B02 (treating) provider office number 4 AN
			txt+=Canadian.TidyAN(treatProv.CanadianOfficeNum,4);//already validated	
			//B03 billing provider number 9 AN
//todo, need to account for possible 5 digit prov id assigned by carrier
			txt+=Canadian.TidyAN(billProv.NationalProvID,9);//already validated
			//C01 primary policy/plan number 12 AN (group number)
			//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
			txt+=Canadian.TidyAN(groupNumber,12);
			//C11 primary division/section number 10 AN
			txt+=Canadian.TidyAN(divisionNo,10);
			//C02 subscriber id number 12 AN
			txt+=Canadian.TidyAN(subscriberID.Replace("-",""),12);//validated
			//C17 primary dependant code 2 N. Optional
			txt+=Canadian.TidyN(patID,2);
			//C03 relationship code 1 N
			//User interface does not only show Canadian options, but all options are handled.
			txt+=Canadian.GetRelationshipCode(patRelat);
			//C04 patient's sex 1 A
			//validated to not include "unknown"
			if(patient.Gender==PatientGender.Male) {
				txt+="M";
			}
			else {
				txt+="F";
			}
			//C05 patient birthday 8 N
			txt+=patient.Birthdate.ToString("yyyyMMdd");//validated
			//C06 patient last name 25 AE
			txt+=Canadian.TidyAE(patient.LName,25,true);//validated
			//C07 patient first name 15 AE
			txt+=Canadian.TidyAE(patient.FName,15,true);//validated
			//C08 patient middle initial 1 AE
			txt+=Canadian.TidyAE(patient.MiddleI,1);
			//C09 eligibility exception code 1 N
			txt+=Canadian.TidyN(FormElig.EligibilityCode,1);//validated
			//C12 plan flag 1 A
//todo
			//might not be carrier.IsPMP.  Might have to do with plan, not carrier. See F17.
			txt+=" ";
			//C18 plan record count 1 N
//todo
			txt+="0";
			//C16 Eligibility date. 8 N.
			txt+=FormElig.AsOfDate.ToString("yyyyMMdd");//validated
			//D01 subscriber birthday 8 N
			txt+=subscriber.Birthdate.ToString("yyyyMMdd");//validated
			//D02 subscriber last name 25 AE
			txt+=Canadian.TidyAE(subscriber.LName,25,true);//validated
			//D03 subscriber first name 15 AE
			txt+=Canadian.TidyAE(subscriber.FName,15,true);//validated
			//D04 subscriber middle initial 1 AE
			txt+=Canadian.TidyAE(subscriber.MiddleI,1);
			//D10 language of insured 1 A
			if(subscriber.Language=="fr") {
				txt+="F";
			}
			else {
				txt+="E";
			}
			//D11 card sequence/version number 2 N
//todo: Not validated against type of carrier yet.  Need to check if Dentaide.
			txt+=Canadian.TidyN(dentaideCardSequence,2);
//todo If C18=1, then the following field would appear
			//C19 plan record 30 AN
			string result="";
			try {
				result=Canadian.PassToCCD(txt,carrier.CanadianNetworkNum,clearhouse);
			}
			catch(ApplicationException ex) {
				Etranss.Delete(etrans.EtransNum);
				throw new ApplicationException(ex.Message);
			}
			Etranss.SetMessage(etrans.EtransNum,txt);
			etrans.MessageText=txt;
			FormCCDPrint FormP=new FormCCDPrint(etrans);//Print the form.
			FormP.ShowDialog();
			//Now we will process the 'result' here to extract the important data.  Basically Yes or No on the eligibility.
			//We might not do this for any other trans type besides eligibility.
			string retVal="Eligibility check on "+DateTime.Today.ToShortDateString()+"\r\n";
			CCDFieldInputter fieldInputter=new CCDFieldInputter(result);
			CCDField field=fieldInputter.GetFieldById("G05");//response status
			//CCDFieldInputter could really use a GetValue(string fieldId) method so I don't have to use a field object.
			switch(field.valuestr){
				case "E":
					retVal+="Patient is eligible.";
					break;
				case "R":
					retVal+="Patient not eligible, or error in data.";
					break;
				case "M":
					retVal+="Manual claimform should be submitted for employer certified plan.";
					break;
			}
			CCDField[] fields=fieldInputter.GetFieldsById("G08");//Error Codes
			for(int i=0;i<fields.Length;i++){
				retVal+="\r\n";
				retVal+=fields[i].valuestr;//todo: need to turn this into a readable string.
			}
			fields=fieldInputter.GetFieldsById("G32");//Display messages
			for(int i=0;i<fields.Length;i++) {
				retVal+="\r\n";
				retVal+=fields[i].valuestr;
			}
			return retVal;
		}

		///<summary></summary>
		public static void SendClaimReversal(){

		}


	}
}
