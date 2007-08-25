using System;
using System.Collections;
using System.Collections.Generic;
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

		///<summary>Called from Eclaims and includes multiple claims.</summary>
		public static bool SendBatch(List<ClaimSendQueueItem> queueItems,int interchangeNum){
			Clearinghouse clearhouse=GetClearinghouse();//clearinghouse must be valid to get to this point.
				//Clearinghouses.GetClearinghouse(((ClaimSendQueueItem)queueItems[0]).ClearinghouseNum);
//Warning: this path is not handled properly if trailing slash is missing:
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				MessageBox.Show(saveFolder+" not found.");
				return false;
			}
			Etrans etrans;
			Claim claim;
			CanadianClaim canClaim;
			Clinic clinic;
			Provider billProv;
			Provider treatProv;
			InsPlan insPlan;
			Carrier carrier;
			InsPlan insPlan2=null;
			Carrier carrier2=null;
			PatPlan[] patPlansForPatient;
			Patient patient;
			Patient subscriber;
			ClaimProc[] claimProcList;//all claimProcs for a patient.
			ClaimProc[] claimProcsClaim;
			Procedure[] procListAll;
			//List<Procedure> extracted;
			Patient subscriber2=null;
			Procedure proc;
			ProcedureCode procCode;
			List<CanadianExtract> missingListAll;
			List<CanadianExtract> missingListDates;
			string txt;
			for(int i=0;i<queueItems.Count;i++){
				etrans=Etranss.SetClaimSentOrPrinted(queueItems[i].ClaimNum,queueItems[i].PatNum,
					clearhouse.ClearinghouseNum,EtransType.Claim_CA,"",0);
				txt="";
				claim=Claims.GetClaim(queueItems[i].ClaimNum);
				canClaim=CanadianClaims.GetForClaim(claim.ClaimNum);//already validated to not=null
				clinic=Clinics.GetClinic(claim.ClinicNum);
				billProv=Providers.ListLong[Providers.GetIndexLong(claim.ProvBill)];
				treatProv=Providers.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
				insPlan=InsPlans.GetPlan(claim.PlanNum,new InsPlan[] { });
				carrier=Carriers.GetCarrier(insPlan.CarrierNum);
				if(claim.PlanNum2>0) {
					insPlan2=InsPlans.GetPlan(claim.PlanNum2,new InsPlan[] { });
					carrier2=Carriers.GetCarrier(insPlan2.CarrierNum);
					subscriber2=Patients.GetPat(insPlan2.Subscriber);
				}
				patPlansForPatient=PatPlans.Refresh(claim.PatNum);
				patient=Patients.GetPat(claim.PatNum);
				subscriber=Patients.GetPat(insPlan.Subscriber);
				claimProcList=ClaimProcs.Refresh(patient.PatNum);
				claimProcsClaim=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
				procListAll=Procedures.Refresh(claim.PatNum);
				missingListAll=CanadianExtracts.GetForClaim(claim.ClaimNum);
				missingListDates=CanadianExtracts.GetWithDates(missingListAll);
				//extracted=Procedures.GetExtractedTeeth(procListAll);
				//A01 transaction prefix 12 AN
	//todo
				txt+="123456789012";//To be later provided by the individual network.
				//A02 office sequence number 6 N
				txt+=TidyN(etrans.OfficeSequenceNumber,6);
				//A03 format version number 2 N
				txt+="04";
				//A04 transaction code 2 N
				txt+="01";//claim
				//A05 carrier id number 6 N
				txt+=carrier.ElectID;//already validated as 6 digit number.
				//A06 software system id 3 AN  The third character is for version of OD.
	//todo
				txt+="OD1";//To be later supplied by CDAnet staff to uniquely identify OD.
				//A10 encryption method 1 N
	//todo
				txt+="1";
				//A07 message length 5 N
				int len=344;
				if(claim.PlanNum2!=0){//if there is secondary coverage
					len+=192;
				}
				len+=44;//for the F section after the secondary coverage section
				len+=10*missingListDates.Count;
				len+=56*claimProcsClaim.Length;
				//if(C19 is used, Plan Record){
					//len+=30;
				//}
				txt+=TidyN(len,5);
				//A08 materials forwarded 1 AN
				txt+=GetMaterialsForwarded(canClaim.MaterialsForwarded);
				//A09 carrier transaction counter 5 N
				int carrierTransCount=etrans.CarrierTransCounter;
				txt+=TidyN(carrierTransCount,5);
				//B01 CDA provider number 9 AN
				txt+=TidyAN(treatProv.NationalProvID,9);//already validated
				//B02 (treating) provider office number 4 AN
				txt+=TidyAN(treatProv.CanadianOfficeNum,4);//already validated	
				//B03 billing provider number 9 AN
	//todo, need to account for possible 5 digit prov id assigned by carrier
				txt+=TidyAN(billProv.NationalProvID,9);//already validated
				//B04 billing provider office number 4 AN
				txt+=TidyAN(billProv.CanadianOfficeNum,4);//already validated	
				//B05 referring provider 10 AN
				txt+=TidyAN(canClaim.ReferralProviderNum,10);
				//B06 referral reason 2 N
				txt+=TidyN(canClaim.ReferralReason,2);
				//C01 primary policy/plan number 12 AN
				//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
				txt+=TidyAN(insPlan.GroupNum,12);
				//C11 primary division/section number 10 AN
				txt+=TidyAN(insPlan.DivisionNo,10);
				//C02 subscriber id number 12 AN
				txt+=TidyAN(insPlan.SubscriberID.Replace("-",""),12);//validated
				//C17 primary dependant code 2 N
				string patID="";
				for(int p=0;p<patPlansForPatient.Length;p++){
					if(patPlansForPatient[p].PlanNum==claim.PlanNum){
						patID=patPlansForPatient[p].PatID;
					}
				}
				txt+=TidyN(patID,2);
				//C03 relationship code 1 N
				//User interface does not only show Canadian options, but all options are handled.
				txt+=GetRelationshipCode(claim.PatRelat);
				//C04 patient's sex 1 A
				//validated to not include "unknown"
				if(patient.Gender==PatientGender.Male){
					txt+="M";
				}
				else{
					txt+="F";
				}
				//C05 patient birthday 8 N
				txt+=patient.Birthdate.ToString("yyyyMMdd");//validated
				//C06 patient last name 25 AE
				txt+=TidyAE(patient.LName,25,true);//validated
				//C07 patient first name 15 AE
				txt+=TidyAE(patient.FName,15,true);//validated
				//C08 patient middle initial 1 AE
				txt+=TidyAE(patient.MiddleI,1);
				//C09 eligibility exception code 1 N
				txt+=TidyN(canClaim.EligibilityCode,1);//validated
				//C10 name of school 25 AEN
				//validated if patient 18yrs or older and full-time student (or disabled student)
				txt+=TidyAEN(patient.SchoolName,25);
				//C12 plan flag 1 A
	//todo
				//might not be carrier.IsPMP.  Might have to do with plan, not carrier. See F17.
				txt+=" ";
				//C18 plan record count 1 N
	//todo
				txt+="0";
				//D01 subscriber birthday 8 N
				txt+=subscriber.Birthdate.ToString("yyyyMMdd");//validated
				//D02 subscriber last name 25 AE
				txt+=TidyAE(subscriber.LName,25,true);//validated
				//D03 subscriber first name 15 AE
				txt+=TidyAE(subscriber.FName,15,true);//validated
				//D04 subscriber middle initial 1 AE
				txt+=TidyAE(subscriber.MiddleI,1);
				//D05 subscriber address line one 30 AEN
				txt+=TidyAEN(subscriber.Address,30,true);//validated
				//D06 subscriber address line two 30 AEN
				txt+=TidyAEN(subscriber.Address2,30,true);
				//D07 subscriber city 20 AEN
				txt+=TidyAEN(subscriber.City,20,true);//validated
				//D08 subscriber province/state 2 A
				txt+=subscriber.State;//very throroughly validated previously
				//D09 subscriber postal/zip code 9 AN
				txt+=TidyAN(subscriber.Zip.Replace("-",""),9);//validated.
				//D10 language of insured 1 A
				if(subscriber.Language=="fr"){
					txt+="F";
				}
				else{
					txt+="E";
				}
				//D11 card sequence/version number 2 N
	//todo: Not validated against type of carrier yet.  Need to check if Dentaide.
				txt+=TidyN(insPlan.DentaideCardSequence,2);
				//E18 secondary coverage flag 1 A
				txt+=canClaim.SecondaryCoverage;//validated
				//E20 secondary record count 1 N
				if(claim.PlanNum2==0){
					txt+="0";
				}
				else{
					txt+="1";
				}
				//F06 number of procedures performed 1 N. Must be between 1 and 7.
	//todo User interface incomplete.  Must not allow attaching more than 7 procs to a claim
				txt+=TidyN(claimProcsClaim.Length,1);//number validated
				//F22 extracted teeth count 2 N
				txt+=TidyN(missingListDates.Count,2);//validated against matching prosthesis
				//Secondary carrier fields (E19 to E07) ONLY included if E20=1----------------------------------------------------
				if(claim.PlanNum2!=0){
	//todo We still need to write the business logic for COB
	//Sometimes, a secondary claim also needs to be created:
					//E19 secondary carrier transaction number 6 N
					txt+="00000";//Must always be zero-filled, since this field is for "future" use only.
					//txt+=TidyN(etrans.CarrierTransCounter2,5));
					//E01 sec carrier id number 6 N
					txt+=carrier2.ElectID;//already validated as 6 digit number.
					//E02 sec carrier policy/plan num 12 AN
					//only validated to ensure that it's not blank and is less than 12. Also that no spaces.
					//We might later allow 999999 if sec carrier is unlisted or unknown.
					txt+=TidyAN(insPlan2.GroupNum,12);
					//E05 sec division/section num 10 AN
					txt+=TidyAN(insPlan2.DivisionNo,10);
					//E03 sec plan subscriber id 12 AN
					txt+=TidyAN(insPlan2.SubscriberID.Replace("-",""),12);//validated
					//E17 sec dependent code 2 N
					patID="";
					for(int p=0;p<patPlansForPatient.Length;p++) {
						if(patPlansForPatient[p].PlanNum==claim.PlanNum2) {
							patID=patPlansForPatient[p].PatID;
						}
					}
					txt+=TidyN(patID,2);
					//E06 sec relationship code 1 N
					//User interface does not only show Canadian options, but all options are handled.
					txt+=GetRelationshipCode(claim.PatRelat2);
					//E04 sec subscriber birthday 8 N
					txt+=subscriber2.Birthdate.ToString("yyyyMMdd");//validated
					//E08 sec subscriber last name 25 AE
					txt+=TidyAE(subscriber2.LName,25,true);//validated
					//E09 sec subscriber first name 15 AE
					txt+=TidyAE(subscriber2.FName,15,true);//validated
					//E10 sec subscriber middle initial 1 AE
					txt+=TidyAE(subscriber2.MiddleI,1);
					//E11 sec subscriber address one 30 AEN
					txt+=TidyAEN(subscriber2.Address,30,true);//validated
					//E12 sec subscriber address two 30 AEN
					txt+=TidyAEN(subscriber2.Address2,30,true);
					//E13 sec subscriber city 20 AEN
					txt+=TidyAEN(subscriber2.City,20,true);//validated
					//E14 sec subscriber province/state 2 A
					txt+=subscriber2.State;//very throroughly validated previously
					//E15 sec subscriber postal/zip code 9 AN
					txt+=TidyAN(subscriber2.Zip.Replace("-",""),9);//validated
					//E16 sec language 1 A
					if(subscriber2.Language=="fr") {
						txt+="F";
					}
					else {
						txt+="E";
					}
					//E07 sec card sequence/version num 2 N
		//todo Not validated yet.
					txt+=TidyN(insPlan2.DentaideCardSequence,2);
					//End of secondary subscriber fields---------------------------------------------------------------------------
				}
				//F01 payee code 1 N
				txt+=TidyN(canClaim.PayeeCode,1);//validated
				//F02 accident date 8 N
				if(claim.AccidentDate.Year>1900){//if accident related
					txt+=claim.AccidentDate.ToString("yyyyMMdd");//validated
				}
				else{
					txt+=TidyN(0,8);
				}
				//F03 predetermination number 14 AN
				txt+=TidyAN(claim.PreAuthString,14);
				//F15 initial placement upper 1 A  Y or N or X
				txt+=canClaim.IsInitialUpper;//validated
				//F04 date of initial placement upper 8 N
				if(canClaim.DateInitialUpper.Year>1900){
					txt+=canClaim.DateInitialUpper.ToString("yyyyMMdd");
				}
				else{
					txt+="00000000";
				}
				//F18 initial placement lower 1 A
				txt+=canClaim.IsInitialLower;//validated
				//F19 date of initial placement lower 8 N
				if(canClaim.DateInitialLower.Year>1900) {
					txt+=canClaim.DateInitialLower.ToString("yyyyMMdd");
				}
				else {
					txt+="00000000";
				}
				//F05 tx req'd for ortho purposes 1 A
				if(claim.IsOrtho){
					txt+="Y";
				}
				else{
					txt+="N";
				}
				//F20 max prosth material 1 N
				if(canClaim.MaxProsthMaterial==7){//our fake crown code
					txt+="0";
				}
				else{
					txt+=canClaim.MaxProsthMaterial.ToString();//validated
				}
				//F21 mand prosth material 1 N
				if(canClaim.MandProsthMaterial==7) {//our fake crown code
					txt+="0";
				}
				else {
					txt+=canClaim.MandProsthMaterial.ToString();//validated
				}
				//If F22 is non-zero. Repeat for the number of times specified by F22.----------------------------------------------
				for(int t=0;t<missingListDates.Count;t++){
					//F23 extracted tooth num 2 N
					txt+=TidyN(Tooth.ToInternat(missingListDates[t].ToothNum),2);//validated
					//F24 extraction date 8 N
					txt+=missingListDates[t].DateExtraction.ToString("yyyyMMdd");//validated
				}
				//Procedures: Repeat for number of times specified by F06.----------------------------------------------------------
				for(int p=0;p<claimProcsClaim.Length;p++){
					proc=Procedures.GetProc(procListAll,claimProcsClaim[i].ProcNum);
					procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
					//F07 proc line number 1 N
					txt+=(p+1).ToString();
					//F08 procedure code 5 AN
					txt+=TidyAN(claimProcsClaim[p].CodeSent,5).Trim().PadLeft(5,'0');
					//F09 date of service 8 N
					txt+=claimProcsClaim[p].ProcDate.ToString("yyyyMMdd");//validated
					//F10 international tooth, sextant, quad, or arch 2 N
					txt+=GetToothQuadOrArch(proc,procCode);
					//F11 tooth surface 5 A
					//the SurfTidy function is very thorough, so it's OK to use TidyAN
					txt+=TidyAN(Tooth.SurfTidy(proc.Surf,proc.ToothNum,true),5);
					//F12 dentist's fee claimed 6 D
					txt+=TidyD(claimProcsClaim[i].FeeBilled,6);
					//F34 lab procedure code #1 5 AN
	//todo
					txt+="     ";
					//F13 lab procedure fee #1 6 D
	//incomplete
					txt+="000000";
					//F35 lab procedure code #2 5 AN
	//incomplete
					txt+="     ";
					//F36 lab procedure fee #2 6 D
	//incomplete
					txt+="000000";
					//F16 procedure type codes 5 A
	//incomplete
					txt+=TidyA("X",5);
					//F17 remarks code 2 N
	//incomplete.  PMP field.  See C12.
					txt+="00";
				}
//todo If C18=1, then the following field would appear
				//C19 plan record 30 AN
				//not used?
				//end of creating the message
				//this is where we attempt the actual sending:
				string ccdResult="";
				try{
					ccdResult=PassToCCD(txt,carrier.CanadianNetworkNum,clearhouse);
				}
				catch(ApplicationException ex){
					//if unsuccessful, then the saveFile needs to be deleted?
					MessageBox.Show(ex.Message);
					return false;
				}
				//continue if successful
				Etranss.SetMessage(etrans.EtransNum,txt);
			}//for i. Loop claims
			return true;
		}

		///<summary>Takes a string, creates a file, and drops it into the correct CCD folder.  Waits for the response, and then returns it as a string.  Will throw an exception if response not received in a reasonable amount of time.  </summary>
		public static string PassToCCD(string msgText, int networkNum, Clearinghouse clearhouse){
			if(clearhouse==null){
				throw new ApplicationException(Lan.g("Canadian","CDAnet Clearinghouse could not be found."));
			}
//warning: folder will not work unless trailing slash is included.
			string saveFolder=clearhouse.ExportPath;
			if(!Directory.Exists(saveFolder)) {
				throw new ApplicationException(saveFolder+" not found.");
			}
			//todo: add validation to make sure claims have a valid network.
			CanadianNetwork network=CanadianNetworks.GetNetwork(networkNum);
			if(network==null){
				throw new ApplicationException("Invalid network.");
			}
			saveFolder=ODFileUtils.CombinePaths(saveFolder,network.Abbrev);
			if(!Directory.Exists(saveFolder)) {
				Directory.CreateDirectory(saveFolder);
			}
			string readFile=ODFileUtils.CombinePaths(saveFolder,"OUTPUT.000");
			File.Delete(readFile);//no exception thrown if file does not exist.
			string saveFile=saveFolder+Path.DirectorySeparatorChar+"INPUT.000";
			using(StreamWriter sw=new StreamWriter(saveFile,false,Encoding.GetEncoding(850))) {
				sw.Write(msgText);
			}
			return "";//for now
			/*
			DateTime start=DateTime.Now;
			while(DateTime.Now<start.AddSeconds(10)){//wait for max of 10 seconds. We can increase it later.
				if(File.Exists(readFile)){
					break;
				}
				Thread.Sleep(500);//1/2 second
				Application.DoEvents();
			}
			if(!File.Exists(readFile)){
				throw new ApplicationException("No response.");
				File.Delete(saveFile);
			}
			//read file here, and return it as a string.
			*/
		}

		///<summary>Since this is only used for Canadian messages, it will always use the default clearinghouse if it's Canadian.  Otherwise, it uses the first Canadian clearinghouse that it can find.</summary>
		public static Clearinghouse GetClearinghouse(){
			for(int i=0;i<Clearinghouses.List.Length;i++) {
				if(Clearinghouses.List[i].IsDefault && Clearinghouses.List[i].CommBridge==EclaimsCommBridge.CDAnet) {
					return Clearinghouses.List[i];
				}
			}
			for(int i=0;i<Clearinghouses.List.Length;i++) {
				if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.CDAnet) {
					return Clearinghouses.List[i];
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

		///<summary>Alphabetic/Numeric, no extended characters.</summary>
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

//incomplete
		private static string GetToothQuadOrArch(Procedure proc,ProcedureCode procCode){
			switch(procCode.TreatArea){
				case TreatmentArea.Arch:
					if(proc.Surf=="U"){
						return "00";
					}
					else{
						return "01";
					}
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
			Clearinghouse clearhouse=Clearinghouses.GetClearinghouse(queueItem.ClearinghouseNum);
			Claim claim=Claims.GetClaim(queueItem.ClaimNum);
			CanadianClaim canClaim=CanadianClaims.GetForClaim(claim.ClaimNum);
			Clinic clinic=Clinics.GetClinic(claim.ClinicNum);
			Provider billProv=Providers.ListLong[Providers.GetIndexLong(claim.ProvBill)];
			Provider treatProv=Providers.ListLong[Providers.GetIndexLong(claim.ProvTreat)];
			InsPlan insPlan=InsPlans.GetPlan(claim.PlanNum,new InsPlan[] { });
			Carrier carrier=Carriers.GetCarrier(insPlan.CarrierNum);
			InsPlan insPlan2=null;
			Carrier carrier2=null;
			Patient subscriber2=null;
			if(claim.PlanNum2>0) {
				insPlan2=InsPlans.GetPlan(claim.PlanNum2,new InsPlan[] { });
				carrier2=Carriers.GetCarrier(insPlan2.CarrierNum);
				subscriber2=Patients.GetPat(insPlan2.Subscriber);
			}
			Patient patient=Patients.GetPat(claim.PatNum);
			Patient subscriber=Patients.GetPat(insPlan.Subscriber);
			ClaimProc[] claimProcList=ClaimProcs.Refresh(patient.PatNum);//all claimProcs for a patient.
			ClaimProc[] claimProcsClaim=ClaimProcs.GetForSendClaim(claimProcList,claim.ClaimNum);
			Procedure[] procListAll=Procedures.Refresh(claim.PatNum);
			Procedure proc;
			ProcedureCode procCode;
			List<CanadianExtract> missingListAll=CanadianExtracts.GetForClaim(claim.ClaimNum);
			List<CanadianExtract> missingListDates=CanadianExtracts.GetWithDates(missingListAll);
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
			if(claimProcsClaim.Length>7){//user interface enforces prevention of claim with 0 procs.
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
			if(canClaim==null){
				if(retVal!="")
					retVal+=", ";
				retVal+="Supplemental Claim Info";
			}
			else{
				if(canClaim.SecondaryCoverage==""){
					if(retVal!="")
						retVal+=", ";
					retVal+="Secondary Cov";
				}
				if(canClaim.SecondaryCoverage=="Y" && claim.PlanNum2==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Secondary Cov mismatch";
				}
				if((canClaim.SecondaryCoverage=="N" || canClaim.SecondaryCoverage=="X")  && claim.PlanNum2>0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Secondary Cov mismatch";
				}
	//check: we might not have to be this strict.  It might truly be optional
				if(canClaim.ReferralProviderNum!="" && canClaim.ReferralReason==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Referral reason";
				}
				if(canClaim.ReferralProviderNum=="" && canClaim.ReferralReason!=0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Referral provider";
				}
				if(canClaim.EligibilityCode==0){
					if(retVal!="")
						retVal+=", ";
					retVal+="Eligibility code";
				}
				if(patient.Age>=18 && canClaim.EligibilityCode==1 ) {//fulltimeStudent
					if(canClaim.SchoolName=="") {
						if(retVal!="")
							retVal+=", ";
						retVal+="School name";
					}
				}
				if(canClaim.PayeeCode==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Payee code";
				}
				//Max Prosth--------------------------------------------------------------------------------------------------
				if(canClaim.IsInitialUpper=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Max prosth";
				}
				if(canClaim.DateInitialUpper>DateTime.MinValue) {
					if(canClaim.DateInitialUpper.Year<1900 || canClaim.DateInitialUpper>=DateTime.Today){
						if(retVal!="")
							retVal+=", ";
						retVal+="Date initial upper";
					}
				}
				if(canClaim.IsInitialUpper=="N" && canClaim.DateInitialUpper.Year<1900) {//missing date
					if(retVal!="")
						retVal+=", ";
					retVal+="Date initial upper";
				}
				if(canClaim.IsInitialUpper=="N" && canClaim.MaxProsthMaterial==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Max prosth material";
				}
				if(canClaim.IsInitialUpper=="X" && canClaim.MaxProsthMaterial!=0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Max prosth material";
				}
				//Mand prosth--------------------------------------------------------------------------------------------------
				if(canClaim.IsInitialLower=="") {
					if(retVal!="")
						retVal+=", ";
					retVal+="Mand prosth";
				}
				if(canClaim.DateInitialLower>DateTime.MinValue) {
					if(canClaim.DateInitialLower.Year<1900 || canClaim.DateInitialLower>=DateTime.Today){
						if(retVal!="")
							retVal+=", ";
						retVal+="Date initial lower";
					}
				}
				if(canClaim.IsInitialLower=="N" && canClaim.DateInitialLower.Year<1900) {//missing date
					if(retVal!="")
						retVal+=", ";
					retVal+="Date initial lower";
				}
				if(canClaim.IsInitialLower=="N" && canClaim.MandProsthMaterial==0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Mand prosth material";
				}
				if(canClaim.IsInitialLower=="X" && canClaim.MandProsthMaterial!=0) {
					if(retVal!="")
						retVal+=", ";
					retVal+="Mand prosth material";
				}
				//missing teeth---------------------------------------------------------------------------------------------------
				if(canClaim.IsInitialLower=="Y" && canClaim.MandProsthMaterial!=7){//initial lower, but not crown
					if(missingListDates.Count==0) {
						if(retVal!="")
							retVal+=", ";
						retVal+="Missing teeth not entered";
					}
				}
				if(canClaim.IsInitialUpper=="Y" && canClaim.MaxProsthMaterial!=7) {//initial upper, but not crown
					if(missingListDates.Count==0) {
						if(retVal!="")
							retVal+=", ";
						retVal+="Missing teeth not entered";
					}
				}
			}//else canClaim!=null
			if(claim.AccidentDate>DateTime.MinValue){
				if(claim.AccidentDate.Year<1900 || claim.AccidentDate>DateTime.Today){
					if(retVal!="")
						retVal+=",";
					retVal+="Accident date";
				}
			}
			for(int i=0;i<claimProcsClaim.Length;i++){
				proc=Procedures.GetProc(procListAll,claimProcsClaim[i].ProcNum);
				procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
				if(claimProcsClaim[i].ProcDate.Year<1970 || claimProcsClaim[i].ProcDate>DateTime.Today){
					if(retVal!="")
						retVal+=", ";
					retVal+=procCode.AbbrDesc+"procedure date";
				}
				if(procCode.TreatArea==TreatmentArea.Arch && proc.Surf==""){
					if(retVal!="")
						retVal+=", ";
					retVal+=procCode.AbbrDesc+"missing arch";
				}
				if(procCode.TreatArea==TreatmentArea.ToothRange && proc.ToothRange==""){
					if(retVal!="")
						retVal+=", ";
					retVal+=procCode.AbbrDesc+"tooth range";
				}
				if((procCode.TreatArea==TreatmentArea.Tooth || procCode.TreatArea==TreatmentArea.Surf)
					&& !Tooth.IsValidDB(proc.ToothNum)) {
					if(retVal!="")
						retVal+=", ";
					retVal+=procCode.AbbrDesc+"tooth number";
				}
				if(procCode.IsProsth){
					if(proc.Prosthesis==""){//they didn't enter whether Initial or Replacement
						if(retVal!="")
							retVal+=", ";
						retVal+=procCode.AbbrDesc+"Prosthesis";
					}
					if(proc.Prosthesis=="R"	&& proc.DateOriginalProsth.Year<1880){//if a replacement, they didn't enter a date
						if(retVal!="")
							retVal+=", ";
						retVal+=procCode.AbbrDesc+"Prosth Date";
					}
				}
			}

			return retVal;
		}

		
	}
}
