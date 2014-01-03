using EhrLaboratories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrLabs{

		///<summary>Surround with Try/Catch.  Processes an HL7 message into an EHRLab object.</summary>
		public EhrLab ProcessHl7Message(string message){
			Patient patcur;
			EhrLab retVal=new EhrLab();
			if(!message.StartsWith("MSH")){
				//cannnot parse message without message header at the very least
				throw new Exception("MSH segment not found.");
			}
			string[] segments=message.Split(new string[] { "\r\n" },StringSplitOptions.None);
			string[] fields;
			foreach(string segment in segments) {
				fields=segment.Split('|');
				switch(fields[0]) {//Segment Identifier.
					case "MSH":
						if(fields[8]!="ORU^R01^ORU_R01") {
							throw new Exception("MSH.9 contained wrong value.  \""+fields[8]+"\" was found, \"ORU^R01^ORU_R01\" was expected.");
						}
						if(fields[11]!="2.5.1") {
							throw new Exception("MSH.12 shows message version \""+fields[11]+"\", only version \"2.5.1\" is currently supported.");
						}
						containsRequiredSegmentsHelper(message); //validate required segments here, after we have verified this is an ORU_R01 message
						if(fields[21].Split('~').Length==0) {
							throw new Exception("MSH.21 does not contain any values, the LRI_GU_RU_Profile value \"2.16.840.1.113883.9.17\" is expected.");
						}
						for(int i=0;i<fields[21].Split('~').Length;i++) {
							if(i==fields[21].Split('~').Length) {
								throw new Exception("MSH.21 ("+i+") indicates sender's message does not conform to LRI_GU_RU_Profile \"2.16.840.1.113883.9.17\"");
							}
							if(fields[21].Split('~')[i]=="2.16.840.1.113883.9.17") {
								break;//found expected value.
							}
						}
						break;
					//case "SFT": //Software Segment
					//	break;
					case "PID":
						for(int i=0;i<fields[3].Split('~').Length;i++) {
							//This may not be implemented correctly.  Not sure if Assigning authority should be OID of the ID Number (For example 2.16.840.1.113883.4.1 for Social Security Numbers) 
							//or if it should be the OID of the organization that assigned the OID (For example 2.16.840.1.113883.3.184 for the Social Security Administration)
							//I am assuming it is the former (That the OID identifies the number, not the "Assigning Authority" Organization. (As per discussion between Ryan and Jason.)
							patcur=Patients.GetByGUID(fields[3].Split('~')[i].Split('^')[1],								//ID Number
																				fields[3].Split('~')[i].Split('^')[4].Split('&')[2]);	//Assigning Authority ID 
							if(patcur!=null) {
								break;//found patient.
							}
							else {
								if(i==fields[3].Split('~').Length) {
									throw new Exception("PID.3 does not contain a known patient ID.");//we should have an option to manually associate lab results with a patient record, in the UI layer.
								}
							}
						}
						//all other PID segments are informative, PID.3 is the only one we need to process.
						break;
					//case "PD1": //patient demographics
					//	break;
					//case "NK1": //Next of Kin/Associated Parties
					//	break;
					//case "PV1": //Patient Visit
					//	break;
					//case "PV2": //Patient Visit addiotional information
					//	break;
					case "ORC":
						try {
							retVal.OrderControlCode=(HL70119)Enum.Parse(typeof(HL70119),fields[1]);
						}
						catch {
							throw new Exception("ORC.1 does not contain a valid Order Control Code (HL70119 value set).");
						}
						//Placer Order Num
						if(fields[2].Length!=0) {//optional field, length may be 0 if field was ommitted.
							retVal.PlacerOrderNum							=fields[2].Split('^')[0];
							retVal.PlacerOrderNamespace				=fields[2].Split('^')[1];
							retVal.PlacerOrderUniversalID			=fields[2].Split('^')[2];
							retVal.PlacerOrderUniversalIDType	=fields[2].Split('^')[3];
						}
						//Filler Order Num
						retVal.FillerOrderNum							=fields[3].Split('^')[0];
						retVal.FillerOrderNamespace				=fields[3].Split('^')[1];
						retVal.FillerOrderUniversalID			=fields[3].Split('^')[2];
						retVal.FillerOrderUniversalIDType	=fields[3].Split('^')[3];
						//Filler Group Num
						retVal.PlacerGroupNum							=fields[4].Split('^')[0];
						retVal.PlacerGroupNamespace				=fields[4].Split('^')[1];
						retVal.PlacerGroupUniversalID			=fields[4].Split('^')[2];
						retVal.PlacerGroupUniversalIDType	=fields[4].Split('^')[3];
						//Ordering Provider
						retVal.OrderingProviderID															=fields[12].Split('^')[0];
						retVal.OrderingProviderLName													=fields[12].Split('^')[1];
						retVal.OrderingProviderFName													=fields[12].Split('^')[2];
						retVal.OrderingProviderMiddleNames										=fields[12].Split('^')[3];
						retVal.OrderingProviderSuffix													=fields[12].Split('^')[4];
						retVal.OrderingProviderPrefix													=fields[12].Split('^')[5];
						retVal.OrderingProviderAssigningAuthorityNamespaceID	=fields[12].Split('^')[9].Split('&')[0];
						retVal.OrderingProviderAssigningAuthorityUniversalID	=fields[12].Split('^')[9].Split('&')[1];
						retVal.OrderingProviderAssigningAuthorityIDType				=fields[12].Split('^')[9].Split('&')[2];
						try {
							retVal.OrderingProviderNameTypeCode=(HL70200)Enum.Parse(typeof(HL70200),fields[12].Split('^')[10]);
						}
						catch {
							throw new Exception("ORC.12.10 does not contain a valid Name Type Code (HL70200 value set).");
						}
						try {
							retVal.OrderingProviderIdentifierTypeCode	=(HL70203)Enum.Parse(typeof(HL70203),fields[12].Split('^')[13]);
						}
						catch {
							throw new Exception("ORC.12.13 does not contain a valid Identifier Type Code (HL70203 value set).");
						}
						break;
					case "OBR":
						retVal.SetIdOBR=PIn.Long(fields[1]);
						//OBR order num should always be identical to OCR order number
						if(retVal.FillerOrderNum!=fields[3].Split('^')[0]) {
							throw new Exception("Filler order numbers in OCR.3 and OBR.3 segments do not match.");
						}
						//Universal Servie ID
						retVal.UsiID											=																			fields[4].Split('^')[0];
						retVal.UsiText										=																			fields[4].Split('^')[1];
						try {retVal.UsiCodeSystemName			=(HL70369)Enum.Parse(typeof(HL70369),	fields[4].Split('^')[2]);}	catch {	}
						retVal.UsiIDAlt										=																			fields[4].Split('^')[3];
						retVal.UsiTextAlt									=																			fields[4].Split('^')[4];
						try {retVal.UsiCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),	fields[4].Split('^')[5]);}	catch {	}
						retVal.UsiTextOriginal						=																			fields[4].Split('^')[6];
						//Observation Date Time
						retVal.ObservationDateTimeStart		=fields[7];
						retVal.ObservationDateTimeEnd			=fields[8];
						try {retVal.SpecimenActionCode		=(HL70065)Enum.Parse(typeof(HL70065),	fields[11]);}	catch {	}
						if(retVal.ListRelevantClinicalInformation==null) {
							retVal.ListRelevantClinicalInformation=new List<EhrLabClinicalInfo>();
						}
						for(int i=0;i<fields[13].Split('~').Length;i++) {
							string tempClinInfo=fields[13].Split('~')[i];
							EhrLabClinicalInfo ehrLabClinicalInfo=new EhrLabClinicalInfo();
							ehrLabClinicalInfo.ClinicalInfoID											=tempClinInfo.Split('^')[0];
							ehrLabClinicalInfo.ClinicalInfoText										=tempClinInfo.Split('^')[1];
							try {ehrLabClinicalInfo.ClinicalInfoCodeSystemName		=(HL70369)Enum.Parse(typeof(HL70369),	tempClinInfo.Split('^')[2]);}	catch {	}
							ehrLabClinicalInfo.ClinicalInfoIDAlt									=tempClinInfo.Split('^')[3];
							ehrLabClinicalInfo.ClinicalInfoTextAlt								=tempClinInfo.Split('^')[4];
							try {ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),	tempClinInfo.Split('^')[5]);}	catch {	}
							ehrLabClinicalInfo.ClinicalInfoTextOriginal						=tempClinInfo.Split('^')[6];
							retVal.ListRelevantClinicalInformation.Add(ehrLabClinicalInfo);
						}
						//Ordering Provider same as OCR.
						retVal.ResultDateTime=fields[22];
						//Parent Result
						try { retVal.ResultStatus												=(HL70123)Enum.Parse(typeof(HL70123),fields[25]);}	catch { }
						retVal.ParentObservationID											=fields[29].Split('^')[0].Split('&')[0];
						retVal.ParentObservationText										=fields[29].Split('^')[0].Split('&')[1];
						try { retVal.ParentObservationCodeSystemName		=(HL70369)Enum.Parse(typeof(HL70369),fields[29].Split('^')[0].Split('&')[2]); }	catch { }
						retVal.ParentObservationIDAlt										=fields[29].Split('^')[0].Split('&')[3];
						retVal.ParentObservationTextAlt									=fields[29].Split('^')[0].Split('&')[4];
						try { retVal.ParentObservationCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),fields[29].Split('^')[0].Split('&')[5]); }	catch { }
						retVal.ParentObservationTextOriginal						=fields[29].Split('^')[0].Split('&')[6];
						retVal.ParentObservationSubID										=fields[29].Split('^')[1];
						//result Handling
						retVal.ListEhrLabResultsHandlingF										=fields[49].Contains("F");
						retVal.ListEhrLabResultsHandlingN										=fields[49].Contains("N");
						break;
					case "NTE":
						//Each not can contain any number of comments, these comments will be carrot delimited. That will be handled later in the UI.  Just store this NTE Segment in an EHRLabNote
						if(retVal.ListEhrLabNotes==null) {
							retVal.ListEhrLabNotes=new List<EhrLabNote>();
						}
						EhrLabNote ehrNote=new EhrLabNote();
						//todo:No SetIDNTE?
						ehrNote.comments=fields[3];
						retVal.ListEhrLabNotes.Add(ehrNote);
						break;
					case "TQ1":
						retVal.TQ1SetId=PIn.Long(fields[1]);
						retVal.TQ1DateTimeStart=fields[7];
						retVal.TQ1DateTimeEnd=fields[8];
						break;
					//case "TQ2": //Timing/Quantity Order Sequence
					//	break;
					//case "CTD": //Contact Data
					//	break;
					case "OBX":
						if(retVal.ListEhrLabResults==null) {
							retVal.ListEhrLabResults=new List<EhrLabResult>();
						}
						EhrLabResult labResult=new EhrLabResult();
						labResult.SetIdOBX=PIn.Long(fields[1]);
						try { labResult.ValueType=(HL70125)Enum.Parse(typeof(HL70125),fields[2]); }	catch { }
						//Lab Result Observation Identifier (LOINC)
						labResult.ObservationIdentifierID												=fields[3].Split('^')[0];
						labResult.ObservationIdentifierText											=fields[3].Split('^')[1];
						try { labResult.ObservationIdentifierCodeSystemName			=(HL70369)Enum.Parse(typeof(HL70369),fields[3].Split('^')[2]); }	catch { }
						labResult.ObservationIdentifierIDAlt										=fields[3].Split('^')[3];
						labResult.ObservationIdentifierTextAlt									=fields[3].Split('^')[4];
						try { labResult.ObservationIdentifierCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),fields[3].Split('^')[5]); }	catch { }
						labResult.ObservationIdentifierTextOriginal							=fields[3].Split('^')[6];
						labResult.ObservationIdentifierSub=fields[4];
						//Observation Value
						switch(labResult.ValueType) {
							case HL70125.CE:
							case HL70125.CWE:
								labResult.ObservationValueCodedElementID										=fields[5].Split('^')[0];
								labResult.ObservationValueCodedElementText									=fields[5].Split('^')[1];
								try{labResult.ObservationValueCodedElementCodeSystemName		=(HL70369)Enum.Parse(typeof(HL70369),fields[5].Split('^')[2]); }	catch { }
								labResult.ObservationValueCodedElementIDAlt									=fields[5].Split('^')[3];
								labResult.ObservationValueCodedElementTextAlt								=fields[5].Split('^')[4];
								try{labResult.ObservationValueCodedElementCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),fields[5].Split('^')[5]); }	catch { }
								if(labResult.ValueType==HL70125.CWE) {
									labResult.ObservationValueCodedElementTextOriginal=fields[5].Split('^')[6];
								}
								break;
							case HL70125.DT:
							case HL70125.TS:
								labResult.ObservationValueDateTime=fields[5];
								break;
							case HL70125.FT://formatted text
							case HL70125.ST://string
							case HL70125.TX://text
								labResult.ObservationValueText=fields[5];
								break;
							case HL70125.NM:
								//data may contain positive or negative sign.  Below, the sign is handled first, and then multiplied by PIn.Double(val)
								labResult.ObservationValueNumeric=  (fields[5].Contains("-")?-1f:1f)  *  PIn.Double(fields[5].Trim('+').Trim('-'));
								break;
							case HL70125.SN:
								labResult.ObservationValueComparator						=						fields[5].Split('^')[0];
								try{labResult.ObservationValueNumber1						=PIn.Double(fields[5].Split('^')[1]); }catch{}//optional, may be a null reference
								try{labResult.ObservationValueSeparatorOrSuffix	=						fields[5].Split('^')[2];	}catch{}//optional, may be a null reference
								try{labResult.ObservationValueNumber2						=PIn.Double(fields[5].Split('^')[3]); }catch{}//optional, may be a null reference
								break;
							case HL70125.TM:
								labResult.ObservationValueTime=PIn.Time(fields[5]);
								break;
						}
						//Units
						labResult.UnitsID											=fields[6].Split('^')[0];
						labResult.UnitsText										=fields[6].Split('^')[1];
						try{labResult.UnitsCodeSystemName			=(HL70369)Enum.Parse(typeof(HL70369),fields[6].Split('^')[2]); }	catch { }
						labResult.UnitsIDAlt									=fields[6].Split('^')[3];
						labResult.UnitsTextAlt								=fields[6].Split('^')[4];
						try{labResult.UnitsCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),fields[6].Split('^')[5]); }	catch { }
						labResult.UnitsTextOriginal						=fields[6].Split('^')[6];
						labResult.referenceRange=fields[7];
						labResult.AbnormalFlags=fields[8].Replace('~',',');//TODO: may need additional formatting/testing
						try{labResult.ObservationResultStatus	=(HL70085)Enum.Parse(typeof(HL70085),fields[11]); }	catch { }
						labResult.ObservationDateTime=fields[14];
						labResult.AnalysisDateTime=fields[19];
						//performing organization Name (with additional info)
						labResult.PerformingOrganizationName=fields[23].Split('^')[0];
						labResult.PerformingOrganizationNameAssigningAuthorityNamespaceId			=fields[23].Split('^')[6].Split('&')[0];
						labResult.PerformingOrganizationNameAssigningAuthorityUniversalId			=fields[23].Split('^')[6].Split('&')[1];
						labResult.PerformingOrganizationNameAssigningAuthorityUniversalIdType	=fields[23].Split('^')[6].Split('&')[2];
						try{labResult.PerformingOrganizationIdentifierTypeCode	=(HL70203)Enum.Parse(typeof(HL70203),fields[23].Split('^')[7]); }	catch { }
						labResult.PerformingOrganizationIdentifier=fields[23].Split('^')[9];
						//Performing Organization Address
						labResult.PerformingOrganizationAddressStreet								=fields[24].Split('^')[0].Split('&')[0];
						labResult.PerformingOrganizationAddressOtherDesignation			=fields[24].Split('^')[1];
						labResult.PerformingOrganizationAddressCity									=fields[24].Split('^')[2];
						try{labResult.PerformingOrganizationAddressStateOrProvince	=(USPSAlphaStateCode)Enum.Parse(typeof(USPSAlphaStateCode),fields[24].Split('^')[3]); }	catch { }
						labResult.PerformingOrganizationAddressZipOrPostalCode			=fields[24].Split('^')[4];
						labResult.PerformingOrganizationAddressCountryCode					=fields[24].Split('^')[5];
						try{labResult.PerformingOrganizationAddressAddressType			=(HL70190)Enum.Parse(typeof(HL70190),fields[24].Split('^')[6]); }	catch { }
						labResult.PerformingOrganizationAddressCountyOrParishCode		=fields[24].Split('^')[8];
						//Performing Organization Medical Director
						labResult.MedicalDirectorID								=fields[25].Split('^')[0];
						labResult.MedicalDirectorFName						=fields[25].Split('^')[1];
						labResult.MedicalDirectorLName						=fields[25].Split('^')[2];
						labResult.MedicalDirectorMiddleNames			=fields[25].Split('^')[3];
						labResult.MedicalDirectorSuffix						=fields[25].Split('^')[4];
						labResult.MedicalDirectorPrefix						=fields[25].Split('^')[5];
						labResult.MedicalDirectorAssigningAuthorityNamespaceID		=fields[25].Split('^')[8].Split('&')[0];
						labResult.MedicalDirectorAssigningAuthorityUniversalID		=fields[25].Split('^')[8].Split('&')[1];
						labResult.MedicalDirectorAssigningAuthorityIDType					=fields[25].Split('^')[8].Split('&')[2];
						try{labResult.MedicalDirectorNameTypeCode						=(HL70200)Enum.Parse(typeof(HL70200),fields[25].Split('^')[9]); }	catch { }
						try{labResult.MedicalDirectorIdentifierTypeCode			=(HL70203)Enum.Parse(typeof(HL70203),fields[25].Split('^')[12]); }	catch { }
						retVal.ListEhrLabResults.Add(labResult);
						break;
					//case "FTI": //Financial Transaction
					//	break;
					//case "CTI": //Clinical Trial Identification
					//	break;
					case "SPM":
						if(retVal.ListEhrLabSpecimin==null) {
							retVal.ListEhrLabSpecimin=new List<EhrLabSpecimen>();
						}
						EhrLabSpecimen ehrLabSpecimen=new EhrLabSpecimen();
						ehrLabSpecimen.SetIdSPM=PIn.Long(fields[1]);
						//Specimen Type
						ehrLabSpecimen.SpecimenTypeID											= fields[4].Split('^')[0];
						ehrLabSpecimen.SpecimenTypeText										= fields[4].Split('^')[1];
						try{ehrLabSpecimen.SpecimenTypeCodeSystemName			= (HL70369)Enum.Parse(typeof(HL70369),fields[4].Split('^')[2]); }	catch { }
						ehrLabSpecimen.SpecimenTypeIDAlt									= fields[4].Split('^')[3];
						ehrLabSpecimen.SpecimenTypeTextAlt								= fields[4].Split('^')[4];
						try{ehrLabSpecimen.SpecimenTypeCodeSystemNameAlt	= (HL70369)Enum.Parse(typeof(HL70369),fields[4].Split('^')[5]); }	catch { }
						ehrLabSpecimen.SpecimenTypeTextOriginal						= fields[4].Split('^')[6];
						//Collection Date Time
						ehrLabSpecimen.CollectionDateTimeStart	=fields[17].Split('^')[0];
						ehrLabSpecimen.CollectionDateTimeEnd		=fields[17].Split('^')[1];
						if(ehrLabSpecimen.ListEhrLabSpecimenRejectReason==null) {
							ehrLabSpecimen.ListEhrLabSpecimenRejectReason=new List<EhrLabSpecimenRejectReason>();
						}
						//Reject Reason
						for(int i=0;i<fields[21].Split('~').Length;i++) {
							EhrLabSpecimenRejectReason ehrLabRR=new EhrLabSpecimenRejectReason();
							ehrLabRR.SpecimenRejectReasonID											=fields[21].Split('~')[i].Split('^')[0];
							ehrLabRR.SpecimenRejectReasonText										=fields[21].Split('~')[i].Split('^')[1];
							try{ehrLabRR.SpecimenRejectReasonCodeSystemName			=(HL70369)Enum.Parse(typeof(HL70369),fields[21].Split('~')[i].Split('^')[2]); }	catch { }
							ehrLabRR.SpecimenRejectReasonIDAlt									=fields[21].Split('~')[i].Split('^')[3];
							ehrLabRR.SpecimenRejectReasonTextAlt								=fields[21].Split('~')[i].Split('^')[4];
							try{ehrLabRR.SpecimenRejectReasonCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),fields[21].Split('~')[i].Split('^')[5]); }	catch { }
							ehrLabRR.SpecimenRejectReasonTextOriginal						=fields[21].Split('~')[i].Split('^')[6];
							ehrLabSpecimen.ListEhrLabSpecimenRejectReason.Add(ehrLabRR);
						}
						//Specimen Condition
						for(int i=0;i<fields[21].Split('~').Length;i++) {
							EhrLabSpecimenCondition ehrLabSC=new EhrLabSpecimenCondition();
							ehrLabSC.SpecimenConditionID											=fields[21].Split('~')[i].Split('^')[0];
							ehrLabSC.SpecimenConditionText										=fields[21].Split('~')[i].Split('^')[1];
							try { ehrLabSC.SpecimenConditionCodeSystemName		=(HL70369)Enum.Parse(typeof(HL70369),fields[21].Split('~')[i].Split('^')[2]); }	catch { }
							ehrLabSC.SpecimenConditionIDAlt										=fields[21].Split('~')[i].Split('^')[3];
							ehrLabSC.SpecimenConditionTextAlt									=fields[21].Split('~')[i].Split('^')[4];
							try { ehrLabSC.SpecimenConditionCodeSystemNameAlt	=(HL70369)Enum.Parse(typeof(HL70369),fields[21].Split('~')[i].Split('^')[5]); }	catch { }
							ehrLabSC.SpecimenConditionTextOriginal						=fields[21].Split('~')[i].Split('^')[6];
							ehrLabSpecimen.ListEhrLabSpecimenCondition.Add(ehrLabSC);
						}
						retVal.ListEhrLabSpecimin.Add(ehrLabSpecimen);
						break;
					default:
						//to catch unsupported or malformed segments.
						break;
				}//end switch
			}//end foreach segment
			//TODO:Message has been processed into an EHR Lab... Now we can do other things if we want to...
			return retVal;
		}

		///<summary>Not implemented. We do not yet need to acknowledge incoming messages.</summary>
		public string GenerateAckMsg(string message) {
			StringBuilder strb=new StringBuilder();
			//we do not need to implement this yet. But probably will for EHR Round 3...
			return strb.ToString();
		}

		///<summary>Throws an exception if message does not contain all required segments, or contains too many segments of a given type.  Does not validate contents of segments.</summary>
		private void containsRequiredSegmentsHelper(string message) {
			string errors="";
			string[] segments=message.Split(new string[] { "\r\n" },StringSplitOptions.None);
			for(int i=0;i<segments.Length;i++){
				segments[i]=segments[i].Split('|')[0];///now each segment only contains the segment identifier.
			}
			//Look for each element/error sperately because there can be many variation of message structure
			//MSH
			int mshCount=0;
			for(int i=0;i<segments.Length;i++) {
				if(segments[i]=="MSH") {
					mshCount++;
				}
			}
			if(mshCount!=1) {
				errors+="Message should contain exactly 1 MSH segment, "+mshCount+" MSH segments found.\r\n";
			}
			//PID
			int pidCount=0;
			for(int i=0;i<segments.Length;i++){
				if(segments[i]=="PID"){
					pidCount++;
				}
			}
			if(pidCount!=1) {
				errors+="Message should contain exactly 1 PID segment, "+pidCount+" PID segments found.\r\n";
			}
			//ORC
			int orcCount=0;
			for(int i=0;i<segments.Length;i++){
				if(segments[i]=="ORC"){
					orcCount++;
				}
			}
			if(pidCount==0) {
				errors+="Message should contain 1 or more ORC segments, "+pidCount+" PID segments found.\r\n";
			}
			//ORC followed by OBR
			for(int i=0;i<segments.Length;i++){
				if(segments[i]=="ORC"){
					if(i+1==segments.Length || segments[i+1]!="OBR"){
						errors+="Message contains \"ORC\" segment that is not followed by \"OBR\" segment.\r\n";
						continue;
					}
					continue;
				}
			}
			//All other segments are optional according to the LRI standard.
			for(int i=0;i<segments.Length;i++) {
				switch(segments[i]) {
					case "MSH":
					case "SFT":
					case "PID":
					case "PD1":
					case "NTE":
					case "NK1":
					case "PV1":
					case "PV2":
					case "ORC":
					case "OBR":
					case "TQ1":
					case "TQ2":
					case "CTD":
					case "OBX":
					case "FTI":
					case "CTI":
					case "SPM":
						//these are the only allowed segments in this message type.
						break;
					default:
						errors+="\""+segments[i]+" is not a supported segment type.";
						break;
				}
			}
			if(errors!="") {
				throw new Exception(errors);
			}
		}

		///<summary>Gets one EhrLab from the db.</summary>
		public static EhrLab GetOne(long ehrLabNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<EhrLab>(MethodBase.GetCurrentMethod(),ehrLabNum);
			}
			return Crud.EhrLabCrud.SelectOne(ehrLabNum);
		}

		///<summary>Gets one EhrLab from the db.</summary>
		public static EhrLab GetByGUID(string root, string extension) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<EhrLab>(MethodBase.GetCurrentMethod(),root, extension);
			}
			string command="";
			//TODO:
			return Crud.EhrLabCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(EhrLab ehrLab) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				ehrLab.EhrLabNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLab);
				return ehrLab.EhrLabNum;
			}
			//TODO:Insert if new, Update if not; Update/Insert children like labresults and the like.
			return Crud.EhrLabCrud.Insert(ehrLab);
		}

		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrLabs.</summary>
		private static List<EhrLab> listt;

		///<summary>A list of all EhrLabs.</summary>
		public static List<EhrLab> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM ehrlab ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrLab";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrLabCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrLab> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLab>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrlab WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrLabCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(EhrLab ehrLab){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrLab.EhrLabNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLab);
				return ehrLab.EhrLabNum;
			}
			return Crud.EhrLabCrud.Insert(ehrLab);
		}

		///<summary></summary>
		public static void Update(EhrLab ehrLab){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLab);
				return;
			}
			Crud.EhrLabCrud.Update(ehrLab);
		}

		///<summary></summary>
		public static void Delete(long ehrLabNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabNum);
				return;
			}
			string command= "DELETE FROM ehrlab WHERE EhrLabNum = "+POut.Long(ehrLabNum);
			Db.NonQ(command);
		}
		*/



	}
}