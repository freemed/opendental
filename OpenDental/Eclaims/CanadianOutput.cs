using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims {
	public class CanadianOutput {
		///<summary>The result is the etransNum of the response message.  Or it might throw an exception if invalid data.  This class is also responsible for saving the returned message to the etrans table, and for printing out the required form.</summary>
		public static long SendElegibility(long patNum,InsPlan plan,DateTime date,Relat relat,string patID,bool doPrint,InsSub insSub){
			//string electID,long patNum,string groupNumber,string divisionNo,
			//string subscriberID,string patID,Relat patRelat,long subscNum,string dentaideCardSequence)
			//Note: This might be the only class of this kind that returns a string.  It's a special situation.
			//We are simply not going to bother with language translation here.
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			if(carrier==null){
				throw new ApplicationException("Invalid carrier.");
			}
			Patient patient=Patients.GetPat(patNum);
			Patient subscriber=Patients.GetPat(insSub.Subscriber);
			Provider prov=Providers.GetProv(Patients.GetProvNum(patient));
			Clearinghouse clearhouse=Canadian.GetClearinghouse();
			if(clearhouse==null){
				throw new ApplicationException("Canadian clearinghouse not found.");
			}
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				throw new ApplicationException(saveFolder+" not found.");
			}
			//validate----------------------------------------------------------------------------------------------------
			string error="";
			//if(carrier.CanadianNetworkNum==0){
			//	if(error!="") error+=", ";
			//	error+="Carrier does not have network specified";
			//}
			if(!Regex.IsMatch(carrier.ElectID,@"^[0-9]{6}$")){//not necessary, but nice
				if(error!="") error+=", ";
				error+="CarrierId 6 digits";
			}

			if(prov.NationalProvID.Length!=9) {
				if(error!="")	error+=", ";
				error+="Prov CDA num 9 digits";
			}
			if(prov.CanadianOfficeNum.Length!=4) {
				if(error!="") error+=", ";
				error+="Prov office num 4 char";
			}
			//if(plan.GroupNum.Length==0 || groupNumber.Length>12 || groupNumber.Contains(" ")){
			//	if(error!="") error+=", ";
			//	error+="Plan Number";
			//}
			//if(subscriberID==""){//already validated.  And it's allowed to be blank sometimes
			//	if(error!="") error+=", ";
			//	error+="SubscriberID";
			//}
			if(patNum != insSub.Subscriber && relat==Relat.Self) {//if patient is not subscriber, and relat is self
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
			if(patient.CanadianEligibilityCode==0) {
				if(error!="") error+=", ";
				error+="Patient eligibility exception code";
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
			Etrans etrans=Etranss.CreateCanadianOutput(patNum,carrier.CarrierNum,carrier.CanadianNetworkNum,
				clearhouse.ClearinghouseNum,EtransType.Eligibility_CA,plan.PlanNum,insSub.InsSubNum);
			StringBuilder strb=new StringBuilder();
			//create message----------------------------------------------------------------------------------------------
			//A01 transaction prefix 12 AN
			strb.Append(Canadian.TidyAN(carrier.CanadianTransactionPrefix,12));
			//A02 office sequence number 6 N
			strb.Append(Canadian.TidyN(etrans.OfficeSequenceNumber,6));
			//A03 format version number 2 N
			strb.Append(carrier.CDAnetVersion);//eg. "04", validated in UI
			//A04 transaction code 2 N
			if(carrier.CDAnetVersion=="02"){
				strb.Append("00");//eligibility
			}
			else{
				strb.Append("08");//eligibility
			}
			//A05 carrier id number 6 N
			strb.Append(carrier.ElectID);//already validated as 6 digit number.
			//A06 software system id 3 AN  The third character is for version of OD.
//todo
#if DEBUG
			strb.Append("TS1");
#else
			strb.Append("OD1");//To be later supplied by CDAnet staff to uniquely identify OD.
#endif
			if(carrier.CDAnetVersion=="04") {
				//A10 encryption method 1 N
				strb.Append(carrier.CanadianEncryptionMethod);//validated in UI
			}
			//A07 message length 5 N
			int len;
			bool C19PlanRecordPresent=false;
			if(carrier.CDAnetVersion=="02"){
				len=178;
				strb.Append(Canadian.TidyN(len,4));
			}
			else{
				len=214;
				if(plan.CanadianPlanFlag=="A"){// || plan.CanadianPlanFlag=="N"){
					C19PlanRecordPresent=true;
				}
				if(C19PlanRecordPresent){
					len+=30;
				}
				strb.Append(Canadian.TidyN(len,5));
				//A09 carrier transaction counter 5 N, only version 04
				strb.Append(Canadian.TidyN(etrans.CarrierTransCounter,5));
			}
			//B01 CDA provider number 9 AN
			strb.Append(Canadian.TidyAN(prov.NationalProvID,9));//already validated
			//B02 provider office number 4 AN
			strb.Append(Canadian.TidyAN(prov.CanadianOfficeNum,4));//already validated
			if(carrier.CDAnetVersion=="04"){
				//B03 billing provider number 9 AN
				//Might need to account for possible 5 digit prov id assigned by carrier
				//But the testing scripts do not supply any billing provider numbers, so we'll ignore this for now.
				//The testing scripts seem to indicate that the billing provider is always the default practice provider.
				Provider provBilling=Providers.GetProv(Providers.GetBillingProvNum(prov.ProvNum,patient.ClinicNum));
				strb.Append(Canadian.TidyAN(provBilling.NationalProvID,9));//already validated
			}
			if(carrier.CDAnetVersion=="02") {
				//C01 primary policy/plan number 8 AN (group number)
				//No special validation for version 02
				strb.Append(Canadian.TidyAN(plan.GroupNum,8));
			}
			else {
				//C01 primary policy/plan number 12 AN (group number)
				//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
				strb.Append(Canadian.TidyAN(plan.GroupNum,12));
			}
			//C11 primary division/section number 10 AN
			strb.Append(Canadian.TidyAN(plan.DivisionNo,10));
			if(carrier.CDAnetVersion=="02") {
				//C02 subscriber id number 11 AN
				strb.Append(Canadian.TidyAN(insSub.SubscriberID.Replace("-",""),11));//no extra validation for version 02
			}
			else{
				//C02 subscriber id number 12 AN
				strb.Append(Canadian.TidyAN(insSub.SubscriberID.Replace("-",""),12));//validated
			}
			if(carrier.CDAnetVersion=="04") {
				//C17 primary dependant code 2 N. Optional
				strb.Append(Canadian.TidyN(patID,2));
			}
			//C03 relationship code 1 N
			//User interface does not only show Canadian options, but all options are handled.
			strb.Append(Canadian.GetRelationshipCode(relat));
			//C04 patient's sex 1 A
			//validated to not include "unknown"
			if(patient.Gender==PatientGender.Male) {
				strb.Append("M");
			}
			else {
				strb.Append("F");
			}
			//C05 patient birthday 8 N
			strb.Append(patient.Birthdate.ToString("yyyyMMdd"));//validated
			//C06 patient last name 25 AE
			strb.Append(Canadian.TidyAE(patient.LName,25,true));//validated
			//C07 patient first name 15 AE
			strb.Append(Canadian.TidyAE(patient.FName,15,true));//validated
			//C08 patient middle initial 1 AE
			strb.Append(Canadian.TidyAE(patient.MiddleI,1));
			//C09 eligibility exception code 1 N
			strb.Append(Canadian.GetEligibilityCode(patient.CanadianEligibilityCode,carrier.CDAnetVersion=="02"));//validated
			if(carrier.CDAnetVersion=="04") {
				//C12 plan flag 1 A
				strb.Append(Canadian.GetPlanFlag(plan.CanadianPlanFlag));
				//C18 plan record count 1 N
				if(C19PlanRecordPresent) {
					strb.Append("1");
				}
				else {
					strb.Append("0");
				}
				//C16 Eligibility date. 8 N.
				strb.Append(date.ToString("yyyyMMdd"));
			}
			//D01 subscriber birthday 8 N
			strb.Append(subscriber.Birthdate.ToString("yyyyMMdd"));//validated
			//D02 subscriber last name 25 AE
			strb.Append(Canadian.TidyAE(subscriber.LName,25,true));//validated
			//D03 subscriber first name 15 AE
			strb.Append(Canadian.TidyAE(subscriber.FName,15,true));//validated
			//D04 subscriber middle initial 1 AE
			strb.Append(Canadian.TidyAE(subscriber.MiddleI,1));
			if(carrier.CDAnetVersion=="04") {
				//D10 language of insured 1 A
				if(subscriber.Language=="fr") {
					strb.Append("F");
				}
				else {
					strb.Append("E");
				}
				//D11 card sequence/version number 2 N
				//Not validated against type of carrier.  Might need to check if Dentaide.
				strb.Append(Canadian.TidyN(plan.DentaideCardSequence,2));
				//C19 plan record 30 AN
				if(C19PlanRecordPresent) {
					//todo: what text goes here?  Not documented
					strb.Append(Canadian.TidyAN("",30));
				}
			}
			string result="";
			bool resultIsError=false;
			try {
				result=Canadian.PassToIca(strb.ToString(),carrier.CanadianNetworkNum,clearhouse);
			}
			catch(ApplicationException ex) {
				result=ex.Message;
				resultIsError=true;
				//Etranss.Delete(etrans.EtransNum);//we don't want to do this, because we want the incremented etrans.OfficeSequenceNumber to be saved
				//Attach an ack indicating failure.
			}
			//Attach an ack to the etrans
			Etrans etransAck=new Etrans();
			etransAck.PatNum=etrans.PatNum;
			etransAck.PlanNum=etrans.PlanNum;
			etransAck.InsSubNum=etrans.InsSubNum;
			etransAck.CarrierNum=etrans.CarrierNum;
			etransAck.DateTimeTrans=DateTime.Now;
			CCDFieldInputter fieldInputter=null;
			if(resultIsError){
				etransAck.Etype=EtransType.AckError;
				etrans.Note="failed";
			}
			else{
				fieldInputter=new CCDFieldInputter(result);
				etransAck.Etype=fieldInputter.GetEtransType();
			}
			Etranss.Insert(etransAck);
			Etranss.SetMessage(etransAck.EtransNum,result);
			etrans.AckEtransNum=etransAck.EtransNum;
			Etranss.Update(etrans);
			Etranss.SetMessage(etrans.EtransNum,strb.ToString());
			if(resultIsError){
				throw new ApplicationException(result);
			}
			if(doPrint) {
				FormCCDPrint FormP=new FormCCDPrint(etrans,result);//Print the form.
				FormP.Print();
			}
			//Now we will process the 'result' here to extract the important data.  Basically Yes or No on the eligibility.
			//We might not do this for any other trans type besides eligibility.
			string strResponse="";//"Eligibility check on "+DateTime.Today.ToShortDateString()+"\r\n";
			//CCDField field=fieldInputter.GetFieldById("G05");//response status
			string valuestr=fieldInputter.GetValue("G05");//response status
			switch(valuestr){
				case "E":
					strResponse+="Patient is eligible.";
					break;
				case "R":
					strResponse+="Patient not eligible, or error in data.";
					break;
				case "M":
					strResponse+="Manual claimform should be submitted for employer certified plan.";
					break;
			}
			etrans=Etranss.GetEtrans(etrans.EtransNum);
			etrans.Note=strResponse;
			Etranss.Update(etrans);
			return etransAck.EtransNum;
			/*
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
			return retVal;*/
		}

		///<summary></summary>
		public static long SendClaimReversal(Claim claim,InsPlan plan,InsSub insSub) {
			StringBuilder strb=new StringBuilder();
			Clearinghouse clearhouse=Canadian.GetClearinghouse();
			if(clearhouse==null) {
				throw new ApplicationException("Canadian clearinghouse not found.");
			}
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				throw new ApplicationException(saveFolder+" not found.");
			}
			Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			Etrans etrans=Etranss.CreateCanadianOutput(claim.PatNum,carrier.CarrierNum,carrier.CanadianNetworkNum,
				clearhouse.ClearinghouseNum,EtransType.ReverseResponse_CA,plan.PlanNum,insSub.InsSubNum);
			Patient patient=Patients.GetPat(claim.PatNum);
			Provider prov=Providers.GetProv(Patients.GetProvNum(patient));
			Provider billProv=ProviderC.ListLong[Providers.GetIndexLong(claim.ProvBill)];
			InsPlan insPlan=InsPlans.GetPlan(claim.PlanNum,new List<InsPlan>());
			Patient subscriber=Patients.GetPat(insSub.Subscriber);
			//create message----------------------------------------------------------------------------------------------
			//A01 transaction prefix 12 AN
			strb.Append(Canadian.TidyAN(carrier.CanadianTransactionPrefix,12));
			//A02 office sequence number 6 N
			strb.Append(Canadian.TidyN(etrans.OfficeSequenceNumber,6));
			//A03 format version number 2 N
			strb.Append(carrier.CDAnetVersion);//eg. "04", validated in UI
			//A04 transaction code 2 N
			strb.Append("02");//Same for both versions 02 and 04.
			//A05 carrier id number 6 N
			strb.Append(carrier.ElectID);//already validated as 6 digit number.
			//A06 software system id 3 AN  The third character is for version of OD.
			//todo
#if DEBUG
			strb.Append("TS1");
#else
			strb.Append("OD1");//To be later supplied by CDAnet staff to uniquely identify OD.
#endif
			if(carrier.CDAnetVersion!="02") { //version 04
				//A10 encryption method 1 N
				strb.Append(carrier.CanadianEncryptionMethod);//validated in UI
			}
			if(carrier.CDAnetVersion=="02") {
				//A07 message length N4
				strb.Append(Canadian.TidyN("133",4));
			}
			else { //version 04
				//A07 message length N 5
				strb.Append(Canadian.TidyN("164",5));
			}
			if(carrier.CDAnetVersion!="02") { //version 04
				//A09 carrier transaction counter 5 N
				List <Etrans> etransHist=Etranss.GetHistoryOneClaim(claim.ClaimNum);
				for(int i=etransHist.Count-1;i>=0;i--) {
					if(etransHist[i].Etype==EtransType.Claim_CA) {
						strb.Append(Canadian.TidyN(etransHist[i].CarrierTransCounter,5));
					}
				}
			}
			//B01 CDA provider number 9 AN
			strb.Append(Canadian.TidyAN(prov.NationalProvID,9));//already validated
			//B02 provider office number 4 AN
			strb.Append(Canadian.TidyAN(prov.CanadianOfficeNum,4));//already validated
			if(claim.PlanNum2!=0) {
				if(carrier.CDAnetVersion!="02") { //version 04
					//E19 secondary carrier transaction number 6 N
					strb.Append(Canadian.TidyN(etrans.CarrierTransCounter2,6));
				}
			}
			if(carrier.CDAnetVersion!="02") { //version 04
				//B03 billing provider number 9 AN
				//might need to account for possible 5 digit prov id assigned by carrier
				strb.Append(Canadian.TidyAN(billProv.NationalProvID,9));//already validated
				//B04 billing provider office number 4 AN
				strb.Append(Canadian.TidyAN(billProv.CanadianOfficeNum,4));//already validated
			}
			if(carrier.CDAnetVersion=="02") {
				//C01 primary policy/plan number 8 AN
				//only validated to ensure that it's not blank and is less than 8. Also that no spaces.
				strb.Append(Canadian.TidyAN(insPlan.GroupNum,8));
			}
			else { //version 04
				//C01 primary policy/plan number 12 AN
				//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
				strb.Append(Canadian.TidyAN(insPlan.GroupNum,12));
			}
			//C11 primary division/section number 10 AN
			strb.Append(Canadian.TidyAN(insPlan.DivisionNo,10));
			if(carrier.CDAnetVersion=="02") {
				//C02 subscriber id number 11 AN
				strb.Append(Canadian.TidyAN(insSub.SubscriberID.Replace("-",""),11));//validated
			}
			else { //version 04
				//C02 subscriber id number 12 AN
				strb.Append(Canadian.TidyAN(insSub.SubscriberID.Replace("-",""),12));//validated
			}
			//C03 relationship code 1 N
			//User interface does not only show Canadian options, but all options are handled.
			strb.Append(Canadian.GetRelationshipCode(claim.PatRelat));
			if(carrier.CDAnetVersion=="02") {
				//D02 subscriber last name 25 A
				strb.Append(Canadian.TidyA(subscriber.LName,25));//validated
			}
			else { //version 04
				//D02 subscriber last name 25 AE
				strb.Append(Canadian.TidyAE(subscriber.LName,25,true));//validated
			}
			if(carrier.CDAnetVersion=="02") {
				//D03 subscriber first name 15 A
				strb.Append(Canadian.TidyA(subscriber.FName,15));//validated
			}
			else { //version 04
				//D03 subscriber first name 15 AE
				strb.Append(Canadian.TidyAE(subscriber.FName,15,true));//validated
			}
			if(carrier.CDAnetVersion=="02") {
				//D04 subscriber middle initial 1 A
				strb.Append(Canadian.TidyA(subscriber.MiddleI,1));
			}
			else { //version 04
				//D04 subscriber middle initial 1 AE
				strb.Append(Canadian.TidyAE(subscriber.MiddleI,1));
			}
			//For Future Use
			strb.Append("000000");
			//G01 transaction reference number of original claim AN 14
			strb.Append(Canadian.TidyAN(claim.CanadaTransRefNum,14));
			string result="";
			bool resultIsError=false;
			try {
			  result=Canadian.PassToIca(strb.ToString(),carrier.CanadianNetworkNum,clearhouse);
			}
			catch(ApplicationException ex) {
			  result=ex.Message;
			  resultIsError=true;
			  //Etranss.Delete(etrans.EtransNum);//we don't want to do this, because we want the incremented etrans.OfficeSequenceNumber to be saved
			  //Attach an ack indicating failure.
			}
			//Attach an ack to the etrans
			Etrans etransAck=new Etrans();
			etransAck.PatNum=etrans.PatNum;
			etransAck.PlanNum=etrans.PlanNum;
			etransAck.InsSubNum=etrans.InsSubNum;
			etransAck.CarrierNum=etrans.CarrierNum;
			etransAck.DateTimeTrans=DateTime.Now;
			CCDFieldInputter fieldInputter=null;
			if(resultIsError) {
			  etransAck.Etype=EtransType.AckError;
			  etrans.Note="failed";
			}
			else {
			  fieldInputter=new CCDFieldInputter(result);
			  etransAck.Etype=fieldInputter.GetEtransType();
			}
			Etranss.Insert(etransAck);
			Etranss.SetMessage(etransAck.EtransNum,result);
			etrans.AckEtransNum=etransAck.EtransNum;
			Etranss.Update(etrans);
			Etranss.SetMessage(etrans.EtransNum,strb.ToString());
			if(resultIsError) {
			  throw new ApplicationException(result);
			}
			CCDField field=fieldInputter.GetFieldById("G05");//response status
			etrans=Etranss.GetEtrans(etrans.EtransNum);
			etrans.Note="Response: "+field.valuestr;
			Etranss.Update(etrans);
			return etransAck.EtransNum;
		}


	}
}
