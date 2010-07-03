using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental.Eclaims {
	public class Canadian {

		//<summary>Gets the filename for this batch. Used when saving or when rolling back.</summary>
		//private static string GetFileName(Clearinghouse clearhouse,int interchangeNum) {
			
		//}

		///<summary>Called directly instead of from Eclaims.SendBatches.  Includes one claim.  Sets claim status internally.  Returns the EtransNum of the ack.</summary>
		public static long SendClaim(ClaimSendQueueItem queueItem,bool doPrint){//,int interchangeNum){
			Clearinghouse clearhouse=GetClearinghouse();//clearinghouse must be valid to get to this point.
				//ClearinghouseL.GetClearinghouse(((ClaimSendQueueItem)queueItems[0]).ClearinghouseNum);
//Warning: this path is not handled properly if trailing slash is missing:
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				throw new ApplicationException(saveFolder+" not found.");
			}
			Etrans etrans;
			Claim claim;
			Clinic clinic;
			Provider billProv;
			Provider treatProv;
			InsPlan insPlan;
			Carrier carrier;
			InsPlan insPlan2=null;
			Carrier carrier2=null;
			List <PatPlan> patPlansForPatient;
			Patient patient;
			Patient subscriber;
			List<ClaimProc> claimProcList;//all claimProcs for a patient.
			List<ClaimProc> claimProcsClaim;
			List<Procedure> procListAll;
			List<Procedure> extracted;
			List<Procedure> procListLabForOne;//Lab fees for one procedure
			Patient subscriber2=null;
			Procedure proc;
			ProcedureCode procCode;
			StringBuilder strb;
			//for(int i=0;i<queueItems.Count;i++){
			etrans=Etranss.SetClaimSentOrPrinted(queueItem.ClaimNum,queueItem.PatNum,clearhouse.ClearinghouseNum,EtransType.Claim_CA);
			strb=new StringBuilder();
			claim=Claims.GetClaim(queueItem.ClaimNum);
			clinic=Clinics.GetClinic(claim.ClinicNum);
			billProv=ProviderC.ListLong[Providers.GetIndexLong(claim.ProvBill)];
			treatProv=ProviderC.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
			insPlan=InsPlans.GetPlan(claim.PlanNum,new List <InsPlan> ());
			carrier=Carriers.GetCarrier(insPlan.CarrierNum);
			if(claim.PlanNum2>0) {
				insPlan2=InsPlans.GetPlan(claim.PlanNum2,new List <InsPlan> ());
				carrier2=Carriers.GetCarrier(insPlan2.CarrierNum);
				subscriber2=Patients.GetPat(insPlan2.Subscriber);
			}
			patPlansForPatient=PatPlans.Refresh(claim.PatNum);
			patient=Patients.GetPat(claim.PatNum);
			subscriber=Patients.GetPat(insPlan.Subscriber);
			claimProcList=ClaimProcs.Refresh(patient.PatNum);
			claimProcsClaim=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
			procListAll=Procedures.Refresh(claim.PatNum);
			extracted=new List<Procedure>();
			if(claim.CanadianIsInitialUpper=="Y" || claim.CanadianIsInitialLower=="Y") {//only set extracted teeth if initial prosth
				extracted=Procedures.GetCanadianExtractedTeeth(procListAll);
			}
			//A01 transaction prefix 12 AN
			strb.Append(TidyAN(carrier.CanadianTransactionPrefix,12));
			//A02 office sequence number 6 N
			strb.Append(TidyN(etrans.OfficeSequenceNumber,6));
			//A03 format version number 2 N
			strb.Append("04");
			//A04 transaction code 2 N
			strb.Append("01");//claim
			//A05 carrier id number 6 N
			strb.Append(carrier.ElectID);//already validated as 6 digit number.
			//A06 software system id 3 AN  The third character is for version of OD.
//todo
#if DEBUG
			strb.Append("TS1");
#else
			strb.Append("OD1");//To be later supplied by CDAnet staff to uniquely identify OD.
#endif
			//A10 encryption method 1 N
			strb.Append(carrier.CanadianEncryptionMethod);//validated in UI
			//A07 message length 5 N
			int len=344;
			if(claim.PlanNum2!=0){//if there is secondary coverage
				len+=192;
			}
			len+=44;//for the F section after the secondary coverage section
			len+=10*extracted.Count;
			len+=56*claimProcsClaim.Count;
			bool C19PlanRecordPresent=false;
			if(insPlan.CanadianPlanFlag=="A"){// || plan.CanadianPlanFlag=="N"){
				C19PlanRecordPresent=true;
			}
			if(C19PlanRecordPresent){
				len+=30;
			}
			strb.Append(TidyN(len,5));
			//A08 materials forwarded 1 AN
			strb.Append(GetMaterialsForwarded(claim.CanadianMaterialsForwarded));
			//A09 carrier transaction counter 5 N
			strb.Append(TidyN(etrans.CarrierTransCounter,5));
			//B01 CDA provider number 9 AN
			strb.Append(TidyAN(treatProv.NationalProvID,9));//already validated
			//B02 (treating) provider office number 4 AN
			strb.Append(TidyAN(treatProv.CanadianOfficeNum,4));//already validated	
			//B03 billing provider number 9 AN
			//might need to account for possible 5 digit prov id assigned by carrier
			strb.Append(TidyAN(billProv.NationalProvID,9));//already validated
			//B04 billing provider office number 4 AN
			strb.Append(TidyAN(billProv.CanadianOfficeNum,4));//already validated	
			//B05 referring provider 10 AN
			strb.Append(TidyAN(claim.CanadianReferralProviderNum,10));
			//B06 referral reason 2 N
			strb.Append(TidyN(claim.CanadianReferralReason,2));
			//C01 primary policy/plan number 12 AN
			//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
			strb.Append(TidyAN(insPlan.GroupNum,12));
			//C11 primary division/section number 10 AN
			strb.Append(TidyAN(insPlan.DivisionNo,10));
			//C02 subscriber id number 12 AN
			strb.Append(TidyAN(insPlan.SubscriberID.Replace("-",""),12));//validated
			//C17 primary dependant code 2 N
			string patID="";
			for(int p=0;p<patPlansForPatient.Count;p++){
				if(patPlansForPatient[p].PlanNum==claim.PlanNum){
					patID=patPlansForPatient[p].PatID;
				}
			}
			strb.Append(TidyN(patID,2));
			//C03 relationship code 1 N
			//User interface does not only show Canadian options, but all options are handled.
			strb.Append(GetRelationshipCode(claim.PatRelat));
			//C04 patient's sex 1 A
			//validated to not include "unknown"
			if(patient.Gender==PatientGender.Male){
				strb.Append("M");
			}
			else{
				strb.Append("F");
			}
			//C05 patient birthday 8 N
			strb.Append(patient.Birthdate.ToString("yyyyMMdd"));//validated
			//C06 patient last name 25 AE
			strb.Append(TidyAE(patient.LName,25,true));//validated
			//C07 patient first name 15 AE
			strb.Append(TidyAE(patient.FName,15,true));//validated
			//C08 patient middle initial 1 AE
			strb.Append(TidyAE(patient.MiddleI,1));
			//C09 eligibility exception code 1 N
			strb.Append(TidyN(patient.CanadianEligibilityCode,1));//validated
			//C10 name of school 25 AEN
			//validated if patient 18yrs or older and full-time student (or disabled student)
			strb.Append(TidyAEN(patient.SchoolName,25));
			//C12 plan flag 1 A
			strb.Append(Canadian.GetPlanFlag(insPlan.CanadianPlanFlag));
			//C18 plan record count 1 N
			if(C19PlanRecordPresent) {
				strb.Append("1");
			}
			else {
				strb.Append("0");
			}
			//D01 subscriber birthday 8 N
			strb.Append(subscriber.Birthdate.ToString("yyyyMMdd"));//validated
			//D02 subscriber last name 25 AE
			strb.Append(TidyAE(subscriber.LName,25,true));//validated
			//D03 subscriber first name 15 AE
			strb.Append(TidyAE(subscriber.FName,15,true));//validated
			//D04 subscriber middle initial 1 AE
			strb.Append(TidyAE(subscriber.MiddleI,1));
			//D05 subscriber address line one 30 AEN
			strb.Append(TidyAEN(subscriber.Address,30,true));//validated
			//D06 subscriber address line two 30 AEN
			strb.Append(TidyAEN(subscriber.Address2,30,true));
			//D07 subscriber city 20 AEN
			strb.Append(TidyAEN(subscriber.City,20,true));//validated
			//D08 subscriber province/state 2 A
			strb.Append(subscriber.State);//very throroughly validated previously
			//D09 subscriber postal/zip code 9 AN
			strb.Append(TidyAN(subscriber.Zip.Replace("-",""),9));//validated.
			//D10 language of insured 1 A
			if(subscriber.Language=="fr"){
				strb.Append("F");
			}
			else{
				strb.Append("E");
			}
			//D11 card sequence/version number 2 N
			//Not validated against type of carrier yet. Might need to check if Dentaide.
			strb.Append(TidyN(insPlan.DentaideCardSequence,2));
			//E18 secondary coverage flag 1 A
			if(claim.PlanNum2>0) {
				strb.Append("Y");
			}
			else {
				strb.Append("N");
			}
			//E20 secondary record count 1 N
			if(claim.PlanNum2==0){
				strb.Append("0");
			}
			else{
				strb.Append("1");
			}
			//F06 number of procedures performed 1 N. Must be between 1 and 7.
//todo User interface incomplete.  Must not allow attaching more than 7 procs to a claim
			strb.Append(TidyN(claimProcsClaim.Count,1));//number validated
			//F22 extracted teeth count 2 N
//todo: check validation
			strb.Append(TidyN(extracted.Count,2));//validated against matching prosthesis
			//Secondary carrier fields (E19 to E07) ONLY included if E20=1----------------------------------------------------
			if(claim.PlanNum2!=0){
//todo We still need to write the business logic for COB
//Sometimes, a secondary claim also needs to be created:
				//E19 secondary carrier transaction number 6 N
				strb.Append(TidyN(etrans.CarrierTransCounter2,6));
				//E01 sec carrier id number 6 N
				strb.Append(carrier2.ElectID);//already validated as 6 digit number.
				//E02 sec carrier policy/plan num 12 AN
				//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
				//We might later allow 999999 if sec carrier is unlisted or unknown.
				strb.Append(TidyAN(insPlan2.GroupNum,12));
				//E05 sec division/section num 10 AN
				strb.Append(TidyAN(insPlan2.DivisionNo,10));
				//E03 sec plan subscriber id 12 AN
				strb.Append(TidyAN(insPlan2.SubscriberID.Replace("-",""),12));//validated
				//E17 sec dependent code 2 N
				patID="";
				for(int p=0;p<patPlansForPatient.Count;p++) {
					if(patPlansForPatient[p].PlanNum==claim.PlanNum2) {
						patID=patPlansForPatient[p].PatID;
					}
				}
				strb.Append(TidyN(patID,2));
				//E06 sec relationship code 1 N
				//User interface does not only show Canadian options, but all options are handled.
				strb.Append(GetRelationshipCode(claim.PatRelat2));
				//E04 sec subscriber birthday 8 N
				strb.Append(subscriber2.Birthdate.ToString("yyyyMMdd"));//validated
				//E08 sec subscriber last name 25 AE
				strb.Append(TidyAE(subscriber2.LName,25,true));//validated
				//E09 sec subscriber first name 15 AE
				strb.Append(TidyAE(subscriber2.FName,15,true));//validated
				//E10 sec subscriber middle initial 1 AE
				strb.Append(TidyAE(subscriber2.MiddleI,1));
				//E11 sec subscriber address one 30 AEN
				strb.Append(TidyAEN(subscriber2.Address,30,true));//validated
				//E12 sec subscriber address two 30 AEN
				strb.Append(TidyAEN(subscriber2.Address2,30,true));
				//E13 sec subscriber city 20 AEN
				strb.Append(TidyAEN(subscriber2.City,20,true));//validated
				//E14 sec subscriber province/state 2 A
				strb.Append(subscriber2.State);//very throroughly validated previously
				//E15 sec subscriber postal/zip code 9 AN
				strb.Append(TidyAN(subscriber2.Zip.Replace("-",""),9));//validated
				//E16 sec language 1 A
				if(subscriber2.Language=="fr") {
					strb.Append("F");
				}
				else {
					strb.Append("E");
				}
				//E07 sec card sequence/version num 2 N
	//todo Not validated yet.
				strb.Append(TidyN(insPlan2.DentaideCardSequence,2));
				//End of secondary subscriber fields---------------------------------------------------------------------------
			}
			//F01 payee code 1 N
			if(insPlan.AssignBen) {
				strb.Append("4");//pay dentist
			}
			else {
				strb.Append("1");//pay subscriber
			}
			//F02 accident date 8 N
			if(claim.AccidentDate.Year>1900){//if accident related
				strb.Append(claim.AccidentDate.ToString("yyyyMMdd"));//validated
			}
			else{
				strb.Append(TidyN(0,8));
			}
			//F03 predetermination number 14 AN
			strb.Append(TidyAN(claim.PreAuthString,14));
			//F15 initial placement upper 1 A  Y or N or X
			strb.Append(claim.CanadianIsInitialUpper);//validated
			//F04 date of initial placement upper 8 N
			if(claim.CanadianDateInitialUpper.Year>1900) {
				strb.Append(claim.CanadianDateInitialUpper.ToString("yyyyMMdd"));
			}
			else{
				strb.Append("00000000");
			}
			//F18 initial placement lower 1 A
			strb.Append(claim.CanadianIsInitialLower);//validated
			//F19 date of initial placement lower 8 N
			if(claim.CanadianDateInitialLower.Year>1900) {
				strb.Append(claim.CanadianDateInitialLower.ToString("yyyyMMdd"));
			}
			else {
				strb.Append("00000000");
			}
			//F05 tx req'd for ortho purposes 1 A
			if(claim.IsOrtho){
				strb.Append("Y");
			}
			else{
				strb.Append("N");
			}
			//F20 max prosth material 1 N
			if(claim.CanadianMaxProsthMaterial==7) {//our fake crown code
				strb.Append("0");
			}
			else{
				strb.Append(claim.CanadianMaxProsthMaterial.ToString());//validated
			}
			//F21 mand prosth material 1 N
			if(claim.CanadianMandProsthMaterial==7) {//our fake crown code
				strb.Append("0");
			}
			else {
				strb.Append(claim.CanadianMandProsthMaterial.ToString());//validated
			}
			//If F22 is non-zero. Repeat for the number of times specified by F22.----------------------------------------------
			for(int t=0;t<extracted.Count;t++){
				//F23 extracted tooth num 2 N
//todo: check validation
				strb.Append(TidyN(Tooth.ToInternat(extracted[t].ToothNum),2));//validated
				//F24 extraction date 8 N
//todo: check validation
				strb.Append(extracted[t].ProcDate.ToString("yyyyMMdd"));//validated
			}
			//Procedures: Repeat for number of times specified by F06.----------------------------------------------------------
			for(int p=0;p<claimProcsClaim.Count;p++) {
				proc=Procedures.GetProcFromList(procListAll,claimProcsClaim[p].ProcNum);
				procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
				procListLabForOne=Procedures.GetCanadianLabFees(proc.ProcNum,procListAll);
				//F07 proc line number 1 N
				strb.Append((p+1).ToString());
				//F08 procedure code 5 AN
				strb.Append(TidyAN(claimProcsClaim[p].CodeSent,5).Trim().PadLeft(5,'0'));
				//F09 date of service 8 N
				strb.Append(claimProcsClaim[p].ProcDate.ToString("yyyyMMdd"));//validated
				//F10 international tooth, sextant, quad, or arch 2 N
				strb.Append(GetToothQuadOrArch(proc,procCode));
				//F11 tooth surface 5 A
				//the SurfTidy function is very thorough, so it's OK to use TidyAN
				if(procCode.TreatArea==TreatmentArea.Surf) {
#if DEBUG
					//since the scripts use impossible surfaces, we need to just use raw database here
					strb.Append(TidyAN(proc.Surf,5));
#else
					strb.Append(TidyAN(Tooth.SurfTidyForClaims(proc.Surf,proc.ToothNum),5));
#endif
				}
				else {
					strb.Append("     ");
				}
				//F12 dentist's fee claimed 6 D
				strb.Append(TidyD(claimProcsClaim[p].FeeBilled,6));
				//F34 lab procedure code #1 5 AN
				if(procListLabForOne.Count>0){
					strb.Append(TidyAN(ProcedureCodes.GetProcCode(procListLabForOne[0].CodeNum).ProcCode,5).Trim().PadLeft(5,'0'));
				}
				else{
					strb.Append("     ");
				}
				//F13 lab procedure fee #1 6 D
				if(procListLabForOne.Count>0){
					strb.Append(TidyD(procListLabForOne[0].ProcFee,6));
				}
				else{
					strb.Append("000000");
				}
				//F35 lab procedure code #2 5 AN
				if(procListLabForOne.Count>1){
					strb.Append(TidyAN(ProcedureCodes.GetProcCode(procListLabForOne[1].CodeNum).ProcCode,5).Trim().PadLeft(5,'0'));
				}
				else{
					strb.Append("     ");
				}
				//F36 lab procedure fee #2 6 D
				if(procListLabForOne.Count>1){
					strb.Append(TidyD(procListLabForOne[1].ProcFee,6));
				}
				else{
					strb.Append("000000");
				}
				//F16 procedure type codes 5 A
				strb.Append(TidyA(proc.CanadianTypeCodes,5));
				//F17 remarks code 2 N
//incomplete.  PMP field.  See C12.
				strb.Append("00");
			}
//todo If C18=1, then the following field would appear
			//C19 plan record 30 AN
			if(C19PlanRecordPresent) {
				//todo: what text goes here?  Not documented
				strb.Append(Canadian.TidyAN("",30));
			}
			//end of creating the message
			//this is where we attempt the actual sending:
			string result="";
			bool resultIsError=false;
			try{
				result=PassToIca(strb.ToString(),carrier.CanadianNetworkNum,clearhouse);
			}
			catch(ApplicationException ex) {
				result=ex.Message;
				resultIsError=true;
			}
			//Attach an ack to the etrans
			Etrans etransAck=new Etrans();
			etransAck.PatNum=etrans.PatNum;
			etransAck.PlanNum=etrans.PlanNum;
			etransAck.CarrierNum=etrans.CarrierNum;
			etransAck.ClaimNum=etrans.ClaimNum;
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
			return etransAck.EtransNum;
		}

		///<summary>Takes a string, creates a file, and drops it into the iCA folder.  Waits for the response, and then returns it as a string.  Will throw an exception if response not received in a reasonable amount of time.  </summary>
		public static string PassToIca(string msgText,long networkNum,Clearinghouse clearhouse) {
			if(clearhouse==null){
				throw new ApplicationException(Lan.g("Canadian","CDAnet Clearinghouse could not be found."));
			}
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				throw new ApplicationException(saveFolder+" not found.");
			}
			Process[] processes=Process.GetProcesses();
			bool isRunning=false;
			for(int i=0;i<processes.Length;i++) {
				if(processes[i].ProcessName.StartsWith("iCA")) {
					isRunning=true;
					break;
				}
			}
			if(!isRunning) {
				ProcessStartInfo startInfo=new ProcessStartInfo(clearhouse.ClientProgram);
				startInfo.WindowStyle=ProcessWindowStyle.Minimized;
				startInfo.WorkingDirectory=Path.GetDirectoryName(clearhouse.ClientProgram);
				Process process=Process.Start(startInfo);
				
				//IntPtr=process.MainWindowHandle
			}
			//if(Process.GetProcessesByName("iCA*").Length==0){
			//	Process.Start(clearhouse.ClientProgram);
			//}
			//Form iCAform=(Form)Form.FromHandle(process.MainWindowHandle);
			//iCAform.WindowState=FormWindowState.Minimized;
			//process.Dispose();
			//CanadianNetwork network=CanadianNetworks.GetNetwork(networkNum);
			//if(network==null){
			//	throw new ApplicationException("Invalid network.");
			//}
			//saveFolder=ODFileUtils.CombinePaths(saveFolder,network.Abbrev);
			//if(!Directory.Exists(saveFolder)) {
			//	Directory.CreateDirectory(saveFolder);
			//}
			//first, delete the result file just in case.
			string outputFile=ODFileUtils.CombinePaths(saveFolder,"output.000");
			if(File.Exists(outputFile)) {
				File.Delete(outputFile);//no exception thrown if file does not exist.
			}
			//create the input file with data:
			string inputFile=ODFileUtils.CombinePaths(saveFolder,"input.000");
			//if(File.Exists(inputFile){
			//File.Delete(inputFile);
			//string _nputFile=ODFileUtils.CombinePaths(saveFolder,"_nput.000"); 
			//File.Delete(_nputFile);
			/* //For testing
			Encoding encoding=Encoding.GetEncoding(850);
			Byte[] bytes=encoding.GetBytes(msgText);
			Byte[] comparebytes=File.ReadAllBytes(@"E:\My Documents\Standards\Canada\Certification\vendor_test_transactions\E1.NET");
			for(int i=0;i<bytes.Length;i++) {
				if(bytes[i]!=comparebytes[i]) {
					MessageBox.Show("Position "+i.ToString()+", byte="+bytes[i].ToString()+", comparebyte="+comparebytes[i].ToString());
				}
			}*/
			File.WriteAllText(inputFile,msgText,Encoding.GetEncoding(850));
			DateTime start=DateTime.Now;
			while(DateTime.Now<start.AddSeconds(45)){//wait for max of 20 seconds. We can increase it later.
				if(File.Exists(outputFile)){
					break;
				}
				Thread.Sleep(200);//1/10 second
				Application.DoEvents();
			}
			if(!File.Exists(outputFile)) {
				//File.Delete(inputFile);
				throw new ApplicationException("No response.");
			}
			string result=File.ReadAllText(outputFile,Encoding.GetEncoding(850));
			//strip the prefix.  Example prefix: 123456,0,000,
			//Find position of third comma
			Match match=Regex.Match(result,@"^\d+,\d+,\d+,");
			if(match.Success){
				result=result.Substring(match.Length);
			}
			//result.IndexOf(",",0,
			File.Delete(outputFile);
			//File.Delete(inputFile);//although this will always have been removed by iCA.exe
			//File.Delete(_nputFile);
			if(result.Length<50) {//can't be a valid message
				string errorFile=ODFileUtils.CombinePaths(Path.GetDirectoryName(clearhouse.ClientProgram),"ica.log");
				string errorlog="";
				if(File.Exists(errorFile)){
					errorlog=File.ReadAllText(errorFile);
				}
				throw new ApplicationException("Invalid response: "+result+"\r\nError log:\r\n"+errorlog+"\r\n\r\nPlease see http://goitrans.com/itrans_support/itrans_claim_support_error_codes.htm for more details.");
			}
			return result;
		}

		///<summary>Since this is only used for Canadian messages, it will always use the default clearinghouse if it's Canadian.  Otherwise, it uses the first Canadian clearinghouse that it can find.</summary>
		public static Clearinghouse GetClearinghouse(){
			for(int i=0;i<Clearinghouses.Listt.Length;i++) {
				if(Clearinghouses.Listt[i].IsDefault && Clearinghouses.Listt[i].CommBridge==EclaimsCommBridge.CDAnet) {
					return Clearinghouses.Listt[i];
				}
			}
			for(int i=0;i<Clearinghouses.Listt.Length;i++) {
				if(Clearinghouses.Listt[i].CommBridge==EclaimsCommBridge.CDAnet) {
					return Clearinghouses.Listt[i];
				}
			}
			return null;
		}

		///<summary>Decimal.</summary>
		public static string TidyD(double number,int width){
			string retVal=(number*100).ToString("F0");
			if(retVal.Length>width) {
				return retVal.Substring(0,width);//this should never happen, but it might prevent a malformed claim.
			}
			return retVal.PadLeft(width,'0');
		}

		///<summary>Numeric</summary>
		public static string TidyN(int number,int width){
			string retVal=number.ToString();
			if(retVal.Length>width){
				return retVal.Substring(0,width);//this should never happen, but it might prevent a malformed claim.
			}
			return retVal.PadLeft(width,'0');
		}

		///<summary>Numeric</summary>
		public static string TidyN(string numText,int width) {
			string retVal="";
			try{
				int number=Convert.ToInt32(numText);
				retVal=number.ToString();
			}
			catch{
				retVal="";
			}
			if(retVal.Length>width) {
				return retVal.Substring(0,width);//this should never happen, but it might prevent a malformed claim.
			}
			return retVal.PadLeft(width,'0');
		}

		///<summary>This should never involve use input and is rarely used.  It only handles width and padding.</summary>
		public static string TidyA(string text,int width){
			if(text.Length>width) {
				return text.Substring(0,width);
			}
			return text.PadRight(width,' ');
		}

		///<summary>Alphabetic, with extended characters. No numbers.</summary>
		public static string TidyAE(string text,int width) {
			return TidyAE(text,width,false);
		}

		///<summary>Alphabetic with extended characters. No numbers. For testing, here are some: à â ç é è ê ë î ï ô û ù ü ÿ</summary>
		public static string TidyAE(string text,int width,bool allowLowercase) {
			if(!allowLowercase) {
				text=text.ToUpper();
			}
			text=Regex.Replace(text,//replace
				@"[0-9]",//any character that's a number
				"");//with nothing (delete it)
			text=Regex.Replace(text,//replace
				@"[^\w'\-,]",//any character that's not a word character or an apost, dash, or comma
				"");//with nothing (delete it)
			if(text.Length>width) {
				return text.Substring(0,width);
			}
			return text.PadRight(width,' ');
		}

		///<summary>Alphabetic/Numeric, no extended characters.  Caps only.</summary>
		public static string TidyAN(string text,int width) {
			return TidyAN(text,width,false);
		}

		///<summary>Alphabetic/Numeric, no extended characters.</summary>
		public static string TidyAN(string text,int width,bool allowLowercase) {
			if(!allowLowercase){
				text=text.ToUpper();
			}
			text=Regex.Replace(text,//replace
				@"[^a-zA-Z0-9 '\-,]",//any char that is not A-Z,0-9,space,apost,dash,or comma
				"");//with nothing (delete it)
			if(text.Length>width) {
				return text.Substring(0,width);
			}
			return text.PadRight(width,' ');
		}

		///<summary>Alphabetic/Numeric, with extended characters.</summary>
		public static string TidyAEN(string text,int width) {
			return TidyAE(text,width,false);
		}

		///<summary>Alphabetic/Numeric with extended characters. For testing, here are some: à â ç é è ê ë î ï ô û ù ü ÿ</summary>
		public static string TidyAEN(string text,int width,bool allowLowercase) {
			if(!allowLowercase) {
				text=text.ToUpper();
			}
			if(text.Length>width) {
				return text.Substring(0,width);
			}
			return text.PadRight(width,' ');
		}

		///<summary>Must always return single char.</summary>
		private static string GetMaterialsForwarded(string materials){
			bool E=materials.Contains("E");
			bool C=materials.Contains("C");
			bool M=materials.Contains("M");
			bool X=materials.Contains("X");
			bool I=materials.Contains("I");
			if(E&&C&&M&&X&&I){
				return "Z";
			}
			if(C&&M&&X&&I) {
				return "Y";
			}
			if(E&&C&&X&&I) {
				return "W";
			}
			if(E&&C&&M&&I) {
				return "V";
			}
			if(E&&C&&M&&X) {
				return "U";
			}
			if(M&&X&&I) {
				return "T";
			}
			if(C&&M&&X) {
				return "R";
			}
			if(C&&M&&I) {
				return "R";
			}
			if(E&&C&&I) {
				return "Q";
			}
			if(E&&C&&X) {
				return "P";
			}
			if(E&&C&&M) {
				return "O";
			}
			if(X&&I) {
				return "N";
			}
			if(M&&I) {
				return "L";
			}
			if(M&&X) {
				return "K";
			}
			if(C&&I) {
				return "J";
			}
			if(C&&X) {
				return "H";
			}
			if(C&&M) {
				return "G";
			}
			if(E&&I) {
				return "F";
			}
			if(E&&X) {
				return "D";
			}
			if(E&&M) {
				return "B";
			}
			if(E&&C) {
				return "A";
			}
			if(I) {
				return "I";
			}
			if(X) {
				return "X";
			}
			if(M) {
				return "M";
			}
			if(C) {
				return "C";
			}
			if(E) {
				return "E";
			}
			return " ";
		}

		///<summary>Used in C03 and E06</summary>
		public static string GetRelationshipCode(Relat relat){
			switch (relat){
				case Relat.Self:
					return "1";
				case Relat.Spouse:
					return "2";
				case Relat.Child:
					return "3";
				case Relat.LifePartner:
				case Relat.SignifOther:
					return "4";//(commonlaw spouse)
				default:
					return "5";
			}
		}

		public static string GetPlanFlag(string planFlag){
			if(planFlag=="A" || planFlag=="V"){
				return planFlag;
			}
			return " ";
		}

		///<summary>Because the numberins scheme is slightly different for version 2, this field (C09) should always be passed through here.</summary>
		public static string GetEligibilityCode(byte rawCode,bool isVersion02) {
			if(isVersion02 && rawCode==4) {
				return "0";
			}
			return rawCode.ToString();
		}

		///<summary>Checks for either valid USA state or valid Canadian territory.</summary>
		private static bool IsValidST(string ST){
			if(IsValidTerritory(ST) || IsValidState(ST)){
				return true;
			}
			return false;
		}

		///<summary>Checks for valid USA state.</summary>
		private static bool IsValidState(string ST){
			string[] validStates=new string[] {
				"AL","AK","AZ","AR","CA","CO","CT","DE","DC","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
				"MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT","VA",
				"WA","WV","WI","WY"};
			for(int i=0;i<validStates.Length;i++){
				if(validStates[i]==ST){
					return true;
				}
			}
			return false;
		}

		///<summary>Checks for valid Canadian terriroty.</summary>
		private static bool IsValidTerritory(string ST){
			string[] validStates=new string[] {"NL","PE","NB","QC","ON","MB","SK","AB","BC","YT","NU"};
			for(int i=0;i<validStates.Length;i++){
				if(validStates[i]==ST){
					return true;
				}
			}
			return false;
		}

		///<summary>The state is required to determine whether USA or Canadian address.  It validates both types.  Canadian must be in form ANANAN.  Zip must be either 5 or 9 digits.</summary>
		private static bool IsValidZip(string zip, string ST){
			if(IsValidState(ST)){//USA
				zip=zip.Replace("-","");
				if(Regex.IsMatch(zip,@"^[0-9]{5}$")) {//5 digits
					return true;
				}
				if(Regex.IsMatch(zip,@"^[0-9]{9}$")) {//9 digits
					return true;
				}
			}
			if(IsValidTerritory(ST)){
				if(Regex.IsMatch(zip,@"^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$")) {//ANANAN
					return true;
				}
			}
			return false;
		}

		private static string GetToothQuadOrArch(Procedure proc,ProcedureCode procCode){
			switch(procCode.TreatArea){
				case TreatmentArea.Arch:
					//if(proc.Surf=="U"){
					return "00";
					//}
					//else{
					//	return "01";
					//}
				case TreatmentArea.Mouth:
				case TreatmentArea.None:
					return "00";
				case TreatmentArea.Quad:
					if(proc.Surf=="UR"){
						return "00";
					}
					else if(proc.Surf=="UL") {
						return "00";
					}
					else if(proc.Surf=="LR") {
						return "00";
					}
					else{//LL
						return "00";
					}
				case TreatmentArea.Sextant:
					if(proc.Surf=="1") {
						return "00";
					}
					else if(proc.Surf=="2") {
						return "00";
					}
					else if(proc.Surf=="3") {
						return "00";
					}
					else if(proc.Surf=="4") {
						return "00";
					}
					else if(proc.Surf=="5") {
						return "00";
					}
					else{//6
						return "00";
					}
				case TreatmentArea.Surf:
				case TreatmentArea.Tooth:
					return Tooth.ToInternat(proc.ToothNum);
				case TreatmentArea.ToothRange:
					string[] range=proc.ToothRange.Split(',');
					if(range.Length==0 || !Tooth.IsValidDB(range[0])){
						return "00";
					}
					else if(Tooth.IsMaxillary(range[0])){
						return "00";
					}
					return "00";
			}
			return "00";//will never happen
		}

		///<summary>Returns a string describing all missing data on this claim.  Claim will not be allowed to be sent electronically unless this string comes back empty.</summary>
		public static string GetMissingData(ClaimSendQueueItem queueItem) {
			string retVal="";
			Clearinghouse clearhouse=ClearinghouseL.GetClearinghouse(queueItem.ClearinghouseNum);
			Claim claim=Claims.GetClaim(queueItem.ClaimNum);
			Clinic clinic=Clinics.GetClinic(claim.ClinicNum);
			Provider billProv=ProviderC.ListLong[Providers.GetIndexLong(claim.ProvBill)];
			Provider treatProv=ProviderC.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
			InsPlan insPlan=InsPlans.GetPlan(claim.PlanNum,new List <InsPlan> ());
			Carrier carrier=Carriers.GetCarrier(insPlan.CarrierNum);
			InsPlan insPlan2=null;
			Carrier carrier2=null;
			Patient subscriber2=null;
			if(claim.PlanNum2>0) {
				insPlan2=InsPlans.GetPlan(claim.PlanNum2,new List <InsPlan> ());
				carrier2=Carriers.GetCarrier(insPlan2.CarrierNum);
				subscriber2=Patients.GetPat(insPlan2.Subscriber);
			}
			Patient patient=Patients.GetPat(claim.PatNum);
			Patient subscriber=Patients.GetPat(insPlan.Subscriber);
			List<ClaimProc> claimProcList=ClaimProcs.Refresh(patient.PatNum);//all claimProcs for a patient.
			List<ClaimProc> claimProcsClaim=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
			List<Procedure> procListAll=Procedures.Refresh(claim.PatNum);
			Procedure proc;
			ProcedureCode procCode;
			List<Procedure> extracted=Procedures.GetCanadianExtractedTeeth(procListAll);
			if(!Regex.IsMatch(carrier.ElectID,@"^[0-9]{6}$")) {
				if(retVal!="")
					retVal+=", ";
				retVal+="CarrierId 6 digits";
			}
			if(treatProv.NationalProvID.Length!=9) {
				if(retVal!="")
					retVal+=", ";
				retVal+="TreatingProv CDA num 9 digits";
			}
			if(treatProv.CanadianOfficeNum.Length!=4) {
				if(retVal!="")
					retVal+=", ";
				retVal+="TreatingProv office num 4 char";
			}
			if(billProv.NationalProvID.Length!=9) {
				if(retVal!="")
					retVal+=", ";
				retVal+="BillingProv CDA num 9 digits";
			}
			if(billProv.CanadianOfficeNum.Length!=4) {
				if(retVal!="")
					retVal+=", ";
				retVal+="BillingProv office num 4 char";
			}
			if(insPlan.GroupNum.Length==0 || insPlan.GroupNum.Length>12 || insPlan.GroupNum.Contains(" ")) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Plan Number";
			}
			if(insPlan.SubscriberID=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="SubscriberID";
			}
			if(claim.PatNum != insPlan.Subscriber//if patient is not subscriber
				&& claim.PatRelat==Relat.Self) {//and relat is self
				if(retVal!="")
					retVal+=", ";
				retVal+="Claim Relationship";
			}
			if(patient.Gender==PatientGender.Unknown) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Patient gender";
			}
			if(patient.Birthdate.Year<1880 || patient.Birthdate>DateTime.Today) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Patient birthdate";
			}
			if(patient.LName=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Patient lastname";
			}
			if(patient.FName=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Patient firstname";
			}
			if(patient.CanadianEligibilityCode==0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Patient eligibility code";
			}
			if(patient.Age>=18 && patient.CanadianEligibilityCode==1){//fulltimeStudent
				if(patient.SchoolName=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Patient school name";
				}
			}
			if(subscriber.Birthdate.Year<1880 || subscriber.Birthdate>DateTime.Today) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber birthdate";
			}
			if(subscriber.LName=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber lastname";
			}
			if(subscriber.FName=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber firstname";
			}
			if(subscriber.Address=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber address";
			}
			if(subscriber.City=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber city";
			}
			if(!IsValidST(subscriber.State)) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber ST";
			}
			if(!IsValidZip(subscriber.Zip,subscriber.State)) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Subscriber Postalcode";
			}
			if(claimProcsClaim.Count>7) {//user interface enforces prevention of claim with 0 procs.
				if(retVal!="")
					retVal+=", ";
				retVal+="Over 7 procs";
			}
//incomplete. Also duplicate for max
			//user interface also needs to be improved to prompt and remind about extracted teeth
			/*if(isInitialLowerProsth && MandProsthMaterial!=0 && CountLower(extracted.Count)==0){
				if(retVal!="")
					retVal+=",";
				retVal+="Missing teeth not entered";
			}*/
			if(claim.PlanNum2>0){
				if(!Regex.IsMatch(carrier2.ElectID,@"^[0-9]{6}$")) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec CarrierId 6 digits";
				}
				if(insPlan2.GroupNum.Length==0 || insPlan2.GroupNum.Length>12 || insPlan2.GroupNum.Contains(" ")) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Plan Number";
				}
				if(insPlan2.SubscriberID=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec SubscriberID";
				}
				if(claim.PatNum != insPlan2.Subscriber//if patient is not subscriber
					&& claim.PatRelat2==Relat.Self) {//and relat is self
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Relationship";
				}
				if(subscriber2.Birthdate.Year<1880 || subscriber2.Birthdate>DateTime.Today) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber birthdate";
				}
				if(subscriber2.LName=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber lastname";
				}
				if(subscriber2.FName=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber firstname";
				}
				if(subscriber2.Address=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber address";
				}
				if(subscriber2.City=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber city";
				}
				if(!IsValidST(subscriber2.State)) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber ST";
				}
				if(!IsValidZip(subscriber2.Zip,subscriber2.State)) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Sec Subscriber Postalcode";
				}
			}
			if(claim.CanadianReferralProviderNum!="" && claim.CanadianReferralReason==0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Referral reason";
			}
			if(claim.CanadianReferralProviderNum=="" && claim.CanadianReferralReason!=0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Referral provider";
			}
			//Max Prosth--------------------------------------------------------------------------------------------------
			if(claim.CanadianIsInitialUpper=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Max prosth";
			}
			if(claim.CanadianDateInitialUpper>DateTime.MinValue) {
				if(claim.CanadianDateInitialUpper.Year<1900 || claim.CanadianDateInitialUpper>=DateTime.Today) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Date initial upper";
				}
			}
			if(claim.CanadianIsInitialUpper=="N" && claim.CanadianDateInitialUpper.Year<1900) {//missing date
				if(retVal!="")
					retVal+=", ";
				retVal+="Date initial upper";
			}
			if(claim.CanadianIsInitialUpper=="N" && claim.CanadianMaxProsthMaterial==0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Max prosth material";
			}
			if(claim.CanadianIsInitialUpper=="X" && claim.CanadianMaxProsthMaterial!=0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Max prosth material";
			}
			//Mand prosth--------------------------------------------------------------------------------------------------
			if(claim.CanadianIsInitialLower=="") {
				if(retVal!="")
					retVal+=", ";
				retVal+="Mand prosth";
			}
			if(claim.CanadianDateInitialLower>DateTime.MinValue) {
				if(claim.CanadianDateInitialLower.Year<1900 || claim.CanadianDateInitialLower>=DateTime.Today) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Date initial lower";
				}
			}
			if(claim.CanadianIsInitialLower=="N" && claim.CanadianDateInitialLower.Year<1900) {//missing date
				if(retVal!="")
					retVal+=", ";
				retVal+="Date initial lower";
			}
			if(claim.CanadianIsInitialLower=="N" && claim.CanadianMandProsthMaterial==0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Mand prosth material";
			}
			if(claim.CanadianIsInitialLower=="X" && claim.CanadianMandProsthMaterial!=0) {
				if(retVal!="")
					retVal+=", ";
				retVal+="Mand prosth material";
			}
			//missing teeth---------------------------------------------------------------------------------------------------
			if(claim.CanadianIsInitialLower=="Y" && claim.CanadianMandProsthMaterial!=7) {//initial lower, but not crown
				if(extracted.Count==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Missing teeth not entered";
				}
			}
			if(claim.CanadianIsInitialUpper=="Y" && claim.CanadianMaxProsthMaterial!=7) {//initial upper, but not crown
				if(extracted.Count==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Missing teeth not entered";
				}
			}
			
			if(claim.AccidentDate>DateTime.MinValue){
				if(claim.AccidentDate.Year<1900 || claim.AccidentDate>DateTime.Today){
					if(retVal!="")
						retVal+=",";
					retVal+="Accident date";
				}
			}
			for(int i=0;i<claimProcsClaim.Count;i++) {
				proc=Procedures.GetProcFromList(procListAll,claimProcsClaim[i].ProcNum);
				procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
				if(claimProcsClaim[i].ProcDate.Year<1970 || claimProcsClaim[i].ProcDate>DateTime.Today){
					if(retVal!="") {
						retVal+=", ";
					}
					retVal+="proc "+procCode.ProcCode+" procedure date";
				}
				if(procCode.TreatArea==TreatmentArea.Arch && proc.Surf==""){
					if(retVal!="") {
						retVal+=", ";
					}
					retVal+="proc "+procCode.ProcCode+" missing arch";
				}
				if(procCode.TreatArea==TreatmentArea.ToothRange && proc.ToothRange==""){
					if(retVal!="") {
						retVal+=", ";
					}
					retVal+="proc "+procCode.ProcCode+" tooth range";
				}
				if((procCode.TreatArea==TreatmentArea.Tooth || procCode.TreatArea==TreatmentArea.Surf)
					&& !Tooth.IsValidDB(proc.ToothNum)) {
					if(retVal!="") {
						retVal+=", ";
					}
					retVal+="proc "+procCode.ProcCode+" tooth number";
				}
				if(procCode.IsProsth){
					if(proc.Prosthesis==""){//they didn't enter whether Initial or Replacement
						if(retVal!="") {
							retVal+=", ";
						}
						retVal+="proc "+procCode.ProcCode+" prosthesis";
					}
					if(proc.Prosthesis=="R"	&& proc.DateOriginalProsth.Year<1880){//if a replacement, they didn't enter a date
						if(retVal!="") {
							retVal+=", ";
						}
						retVal+="proc "+procCode.ProcCode+" prosth date";
					}
				}
			}

			return retVal;
		}

		
	}
}
