package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Procedure {
		/** Primary key. */
		public int ProcNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to appointment.AptNum.  Only allowed to attach proc to one appt(not counting planned appt) */
		public int AptNum;
		/** No longer used. */
		public String OldCode;
		/** Procedure date that will show in the account as the date performed.  If just treatment planned, the date can be the date it was tp'd, or the date can be min val if we don't care.  Also see ProcTime column. */
		public Date ProcDate;
		/** Procedure fee. */
		public double ProcFee;
		/** Surfaces, or use "UL" etc for quadrant, "2" etc for sextant, "U","L" for arches. */
		public String Surf;
		/** May be blank, otherwise 1-32, 51-82, A-T, or AS-TS, 1 or 2 char. */
		public String ToothNum;
		/** May be blank, otherwise is series of toothnumbers separated by commas. */
		public String ToothRange;
		/** FK to definition.DefNum, which contains the text of the priority. */
		public int Priority;
		/** Enum:ProcStat TP=1,Complete=2,Existing Cur Prov=3,Existing Other Prov=4,Referred=5,Deleted=6,Condition=7. */
		public ProcStat ProcStatus;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** FK to definition.DefNum, which contains text of the Diagnosis. */
		public int Dx;
		/** FK to appointment.AptNum.  Was called NextAptNum in older versions.  Allows this procedure to be attached to a Planned appointment as well as a standard appointment. */
		public int PlannedAptNum;
		/** Enum:PlaceOfService  Only used in Public Health. Zero(Office) until procedure set complete. Then it's set to the value of the DefaultProcedurePlaceService preference. */
		public PlaceOfService PlaceService;
		/** Single char. Blank=no, I=Initial, R=Replacement. */
		public String Prosthesis;
		/** For a prosthesis Replacement, this is the original date. */
		public Date DateOriginalProsth;
		/** This note used to go out on e-claims, but the new format does not allow it.  So we removed the UI for this field.  We will probably drop this field completely soon. */
		public String ClaimNote;
		/** This is the date this procedure was entered or set complete.  If not status C, then the value is ignored.  This date is set automatically when Insert, but older data or converted data might not have this value set.  It gets updated when set complete.  User never allowed to edit.  This will be enhanced later. */
		public Date DateEntryC;
		/** FK to clinic.ClinicNum.  0 if no clinic. */
		public int ClinicNum;
		/** FK to procedurecode.ProcCode. Optional. */
		public String MedicalCode;
		/** Simple text for ICD-9 code. Gets sent with medical claims. */
		public String DiagnosticCode;
		/** Set true if this medical diagnostic code is the principal diagnosis for the visit.  If no principal diagnosis is marked for any procedures on a medical e-claim, then it won't be allowed to be sent.  If more than one is marked, then it will just use one at random. */
		public boolean IsPrincDiag;
		/** FK to procedurelog.ProcNum. Only used in Canada. If not zero, then this proc is a lab fee and this indicates to which actual procedure the lab fee is attached.  For ordinary use, they are treated like two separate procedures.  It's only for insurance claims that we need to know which lab fee belongs to which procedure.  Two lab fees may be attached to one procedure. */
		public int ProcNumLab;
		/** FK to definition.DefNum. Lets some users track charges for certain types of reports.  For example, a Medicaid billing type could be assigned to a procedure, flagging it for inclusion in a report mandated by goverment.  Would be more useful if it was automated to flow down based on insurance plan type, but that can be added later.  Not visible if prefs.EasyHideMedicaid is true. */
		public int BillingTypeOne;
		/** FK to definition.DefNum.  Same as BillingTypeOne, but used when there is a secondary billing type to account for. */
		public int BillingTypeTwo;
		/** FK to procedurecode.CodeNum */
		public int CodeNum;
		/** Modifier for certain CPT codes. */
		public String CodeMod1;
		/** Modifier for certain CPT codes. */
		public String CodeMod2;
		/** Modifier for certain CPT codes. */
		public String CodeMod3;
		/** Modifier for certain CPT codes. */
		public String CodeMod4;
		/** NUBC Revenue Code for medical/inst billing. Used on UB04 and 837I. */
		public String RevCode;
		/** Default is 1.  Becomes Service Unit Count on institutional UB claimforms SV205.  Becomes Service Unit Count on medical 1500 claimforms SV104.  Becomes procedure count on dental claims SV306.  Gets multiplied by fee in all accounting calculations. */
		public int UnitQty;
		/** Base units used for some billing codes.  Default is 0.  No UI for this field.  It is only edited in the ProcedureCode window.  The database maint tool changes BaseUnits of all procedures to match that of the procCode.  Not sure yet what it's for. */
		public int BaseUnits;
		/** Start time in military.  No longer used, but not deleting just in case someone has critical information stored here. */
		public int StartTime;
		/** Stop time in military.  No longer used, but not deleting just in case someone has critical information stored here. */
		public int StopTime;
		/** The date that the procedure was originally treatment planned.  Does not change when marked complete. */
		public Date DateTP;
		/** FK to site.SiteNum. */
		public int SiteNum;
		/** Set to true to hide the chart graphics for this procedure.  For example, a crown was done, but then tooth extracted. */
		public boolean HideGraphics;
		/** F16, up to 5 char. One or more of the following: A=Repair of a prior service, B=Temporary placement, C=TMJ, E=Implant, L=Appliance lost, S=Appliance stolen, X=none of the above.  Blank is equivalent to X for claim output, but one value will not be automatically converted to the other in this table.  That will allow us to track user entry for procedurecode.IsProsth. */
		public String CanadianTypeCodes;
		/** Used to be part of the ProcDate, but that was causing reporting issues. */
		public String ProcTime;
		/** Marks the time a procedure was finished. */
		public String ProcTimeEnd;
		/** Automatically updated by MySQL every time a row is added or changed. */
		public Date DateTStamp;
		/** FK to definition.DefNum, which contains text of the Prognosis. */
		public int Prognosis;
		/** Enum:EnumProcDrugUnit For 837I and UB04 */
		public EnumProcDrugUnit DrugUnit;
		/** Includes fractions. For 837I */
		public float DrugQty;
		/** Enum:ProcUnitQtyType For dental, the type is always sent electronically as MultiProcs. For institutional SV204, Days will be sent electronically if chosen, otherwise ServiceUnits will be sent. For medical SV103, MinutesAnesth will be sent electronically if chosen, otherwise ServiceUnits will be sent. */
		public ProcUnitQtyType UnitQtyType;
		/** FK to statement.StatementNum.  Only used when the statement in an invoice. */
		public int StatementNum;
		/** If this flag is set, then the proc is locked down tight.  No changes at all can be made except to append, sign, or invalidate. Invalidate really just sets the proc to status 'deleted'.  An invalidated proc retains its IsLocked status.  All locked procs will be status of C or D. */
		public boolean IsLocked;

		/** Deep copy of object. */
		public Procedure Copy() {
			Procedure procedure=new Procedure();
			procedure.ProcNum=this.ProcNum;
			procedure.PatNum=this.PatNum;
			procedure.AptNum=this.AptNum;
			procedure.OldCode=this.OldCode;
			procedure.ProcDate=this.ProcDate;
			procedure.ProcFee=this.ProcFee;
			procedure.Surf=this.Surf;
			procedure.ToothNum=this.ToothNum;
			procedure.ToothRange=this.ToothRange;
			procedure.Priority=this.Priority;
			procedure.ProcStatus=this.ProcStatus;
			procedure.ProvNum=this.ProvNum;
			procedure.Dx=this.Dx;
			procedure.PlannedAptNum=this.PlannedAptNum;
			procedure.PlaceService=this.PlaceService;
			procedure.Prosthesis=this.Prosthesis;
			procedure.DateOriginalProsth=this.DateOriginalProsth;
			procedure.ClaimNote=this.ClaimNote;
			procedure.DateEntryC=this.DateEntryC;
			procedure.ClinicNum=this.ClinicNum;
			procedure.MedicalCode=this.MedicalCode;
			procedure.DiagnosticCode=this.DiagnosticCode;
			procedure.IsPrincDiag=this.IsPrincDiag;
			procedure.ProcNumLab=this.ProcNumLab;
			procedure.BillingTypeOne=this.BillingTypeOne;
			procedure.BillingTypeTwo=this.BillingTypeTwo;
			procedure.CodeNum=this.CodeNum;
			procedure.CodeMod1=this.CodeMod1;
			procedure.CodeMod2=this.CodeMod2;
			procedure.CodeMod3=this.CodeMod3;
			procedure.CodeMod4=this.CodeMod4;
			procedure.RevCode=this.RevCode;
			procedure.UnitQty=this.UnitQty;
			procedure.BaseUnits=this.BaseUnits;
			procedure.StartTime=this.StartTime;
			procedure.StopTime=this.StopTime;
			procedure.DateTP=this.DateTP;
			procedure.SiteNum=this.SiteNum;
			procedure.HideGraphics=this.HideGraphics;
			procedure.CanadianTypeCodes=this.CanadianTypeCodes;
			procedure.ProcTime=this.ProcTime;
			procedure.ProcTimeEnd=this.ProcTimeEnd;
			procedure.DateTStamp=this.DateTStamp;
			procedure.Prognosis=this.Prognosis;
			procedure.DrugUnit=this.DrugUnit;
			procedure.DrugQty=this.DrugQty;
			procedure.UnitQtyType=this.UnitQtyType;
			procedure.StatementNum=this.StatementNum;
			procedure.IsLocked=this.IsLocked;
			return procedure;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Procedure>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<OldCode>").append(Serializing.EscapeForXml(OldCode)).append("</OldCode>");
			sb.append("<ProcDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ProcDate)).append("</ProcDate>");
			sb.append("<ProcFee>").append(ProcFee).append("</ProcFee>");
			sb.append("<Surf>").append(Serializing.EscapeForXml(Surf)).append("</Surf>");
			sb.append("<ToothNum>").append(Serializing.EscapeForXml(ToothNum)).append("</ToothNum>");
			sb.append("<ToothRange>").append(Serializing.EscapeForXml(ToothRange)).append("</ToothRange>");
			sb.append("<Priority>").append(Priority).append("</Priority>");
			sb.append("<ProcStatus>").append(ProcStatus.ordinal()).append("</ProcStatus>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Dx>").append(Dx).append("</Dx>");
			sb.append("<PlannedAptNum>").append(PlannedAptNum).append("</PlannedAptNum>");
			sb.append("<PlaceService>").append(PlaceService.ordinal()).append("</PlaceService>");
			sb.append("<Prosthesis>").append(Serializing.EscapeForXml(Prosthesis)).append("</Prosthesis>");
			sb.append("<DateOriginalProsth>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateOriginalProsth)).append("</DateOriginalProsth>");
			sb.append("<ClaimNote>").append(Serializing.EscapeForXml(ClaimNote)).append("</ClaimNote>");
			sb.append("<DateEntryC>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntryC)).append("</DateEntryC>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<MedicalCode>").append(Serializing.EscapeForXml(MedicalCode)).append("</MedicalCode>");
			sb.append("<DiagnosticCode>").append(Serializing.EscapeForXml(DiagnosticCode)).append("</DiagnosticCode>");
			sb.append("<IsPrincDiag>").append((IsPrincDiag)?1:0).append("</IsPrincDiag>");
			sb.append("<ProcNumLab>").append(ProcNumLab).append("</ProcNumLab>");
			sb.append("<BillingTypeOne>").append(BillingTypeOne).append("</BillingTypeOne>");
			sb.append("<BillingTypeTwo>").append(BillingTypeTwo).append("</BillingTypeTwo>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<CodeMod1>").append(Serializing.EscapeForXml(CodeMod1)).append("</CodeMod1>");
			sb.append("<CodeMod2>").append(Serializing.EscapeForXml(CodeMod2)).append("</CodeMod2>");
			sb.append("<CodeMod3>").append(Serializing.EscapeForXml(CodeMod3)).append("</CodeMod3>");
			sb.append("<CodeMod4>").append(Serializing.EscapeForXml(CodeMod4)).append("</CodeMod4>");
			sb.append("<RevCode>").append(Serializing.EscapeForXml(RevCode)).append("</RevCode>");
			sb.append("<UnitQty>").append(UnitQty).append("</UnitQty>");
			sb.append("<BaseUnits>").append(BaseUnits).append("</BaseUnits>");
			sb.append("<StartTime>").append(StartTime).append("</StartTime>");
			sb.append("<StopTime>").append(StopTime).append("</StopTime>");
			sb.append("<DateTP>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTP)).append("</DateTP>");
			sb.append("<SiteNum>").append(SiteNum).append("</SiteNum>");
			sb.append("<HideGraphics>").append((HideGraphics)?1:0).append("</HideGraphics>");
			sb.append("<CanadianTypeCodes>").append(Serializing.EscapeForXml(CanadianTypeCodes)).append("</CanadianTypeCodes>");
			sb.append("<ProcTime>").append(Serializing.EscapeForXml(ProcTime)).append("</ProcTime>");
			sb.append("<ProcTimeEnd>").append(Serializing.EscapeForXml(ProcTimeEnd)).append("</ProcTimeEnd>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<Prognosis>").append(Prognosis).append("</Prognosis>");
			sb.append("<DrugUnit>").append(DrugUnit.ordinal()).append("</DrugUnit>");
			sb.append("<DrugQty>").append(DrugQty).append("</DrugQty>");
			sb.append("<UnitQtyType>").append(UnitQtyType.ordinal()).append("</UnitQtyType>");
			sb.append("<StatementNum>").append(StatementNum).append("</StatementNum>");
			sb.append("<IsLocked>").append((IsLocked)?1:0).append("</IsLocked>");
			sb.append("</Procedure>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.GetXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcDate")!=null) {
					ProcDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"ProcDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcFee")!=null) {
					ProcFee=Double.valueOf(Serializing.GetXmlNodeValue(doc,"ProcFee"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Surf")!=null) {
					Surf=Serializing.GetXmlNodeValue(doc,"Surf");
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothNum")!=null) {
					ToothNum=Serializing.GetXmlNodeValue(doc,"ToothNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothRange")!=null) {
					ToothRange=Serializing.GetXmlNodeValue(doc,"ToothRange");
				}
				if(Serializing.GetXmlNodeValue(doc,"Priority")!=null) {
					Priority=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Priority"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcStatus")!=null) {
					ProcStatus=ProcStat.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcStatus"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Dx")!=null) {
					Dx=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Dx"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlannedAptNum")!=null) {
					PlannedAptNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlannedAptNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlaceService")!=null) {
					PlaceService=PlaceOfService.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlaceService"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Prosthesis")!=null) {
					Prosthesis=Serializing.GetXmlNodeValue(doc,"Prosthesis");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateOriginalProsth")!=null) {
					DateOriginalProsth=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateOriginalProsth"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimNote")!=null) {
					ClaimNote=Serializing.GetXmlNodeValue(doc,"ClaimNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateEntryC")!=null) {
					DateEntryC=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateEntryC"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MedicalCode")!=null) {
					MedicalCode=Serializing.GetXmlNodeValue(doc,"MedicalCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"DiagnosticCode")!=null) {
					DiagnosticCode=Serializing.GetXmlNodeValue(doc,"DiagnosticCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsPrincDiag")!=null) {
					IsPrincDiag=(Serializing.GetXmlNodeValue(doc,"IsPrincDiag")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcNumLab")!=null) {
					ProcNumLab=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcNumLab"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BillingTypeOne")!=null) {
					BillingTypeOne=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BillingTypeOne"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BillingTypeTwo")!=null) {
					BillingTypeTwo=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BillingTypeTwo"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeMod1")!=null) {
					CodeMod1=Serializing.GetXmlNodeValue(doc,"CodeMod1");
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeMod2")!=null) {
					CodeMod2=Serializing.GetXmlNodeValue(doc,"CodeMod2");
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeMod3")!=null) {
					CodeMod3=Serializing.GetXmlNodeValue(doc,"CodeMod3");
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeMod4")!=null) {
					CodeMod4=Serializing.GetXmlNodeValue(doc,"CodeMod4");
				}
				if(Serializing.GetXmlNodeValue(doc,"RevCode")!=null) {
					RevCode=Serializing.GetXmlNodeValue(doc,"RevCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"UnitQty")!=null) {
					UnitQty=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UnitQty"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BaseUnits")!=null) {
					BaseUnits=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BaseUnits"));
				}
				if(Serializing.GetXmlNodeValue(doc,"StartTime")!=null) {
					StartTime=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"StartTime"));
				}
				if(Serializing.GetXmlNodeValue(doc,"StopTime")!=null) {
					StopTime=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"StopTime"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTP")!=null) {
					DateTP=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTP"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SiteNum")!=null) {
					SiteNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SiteNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"HideGraphics")!=null) {
					HideGraphics=(Serializing.GetXmlNodeValue(doc,"HideGraphics")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianTypeCodes")!=null) {
					CanadianTypeCodes=Serializing.GetXmlNodeValue(doc,"CanadianTypeCodes");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcTime")!=null) {
					ProcTime=Serializing.GetXmlNodeValue(doc,"ProcTime");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcTimeEnd")!=null) {
					ProcTimeEnd=Serializing.GetXmlNodeValue(doc,"ProcTimeEnd");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Prognosis")!=null) {
					Prognosis=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Prognosis"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DrugUnit")!=null) {
					DrugUnit=EnumProcDrugUnit.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DrugUnit"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"DrugQty")!=null) {
					DrugQty=Float.valueOf(Serializing.GetXmlNodeValue(doc,"DrugQty"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UnitQtyType")!=null) {
					UnitQtyType=ProcUnitQtyType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UnitQtyType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"StatementNum")!=null) {
					StatementNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"StatementNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsLocked")!=null) {
					IsLocked=(Serializing.GetXmlNodeValue(doc,"IsLocked")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Procedure Status. */
		public enum ProcStat {
			/** 1- Treatment Plan. */
			TP,
			/** 2- Complete. */
			C,
			/** 3- Existing Current Provider. */
			EC,
			/** 4- Existing Other Provider. */
			EO,
			/** 5- Referred Out. */
			R,
			/** 6- Deleted. */
			D,
			/** 7- Condition. */
			Cn
		}

		/**  */
		public enum PlaceOfService {
			/** 0. CPT code 11 */
			Office,
			/** 1. CPT code 12 */
			PatientsHome,
			/** 2. CPT code 21 */
			InpatHospital,
			/** 3. CPT code 22 */
			OutpatHospital,
			/** 4. CPT code 31 */
			SkilledNursFac,
			/** 5. CPT code 33.  In X12, a similar code AdultLivCareFac 35 is mentioned. */
			CustodialCareFacility,
			/** 6. CPT code ?.  We use 11 for office. */
			OtherLocation,
			/** 7. CPT code 15 */
			MobileUnit,
			/** 8. CPT code 03 */
			School,
			/** 9. CPT code 26 */
			MilitaryTreatFac,
			/** 10. CPT code 50 */
			FederalHealthCenter,
			/** 11. CPT code 71 */
			PublicHealthClinic,
			/** 12. CPT code 72 */
			RuralHealthClinic,
			/** 13. CPT code 23 */
			EmergencyRoomHospital,
			/** 14. CPT code 24 */
			AmbulatorySurgicalCenter
		}

		/**  */
		public enum EnumProcDrugUnit {
			/** 0 */
			None,
			/** 1 - F2 on UB04. */
			InternationalUnit,
			/** 2 - GR on UB04. */
			Gram,
			/** 3 - GR on UB04. */
			Milligram,
			/** 4 - ML on UB04. */
			Milliliter,
			/** 5 - UN on UB04. */
			Unit
		}

		/**  */
		public enum ProcUnitQtyType {
			/** 0-Only allowed on dental, and only option allowed on dental.  This is also the default for all procs in our UI.  For example, 4 PAs all on one line on the e-claim. */
			MultProcs,
			/** 1-Only allowed on medical SV103. */
			MinutesAnesth,
			/** 2-Allowed on medical SV103 and institutional SV204.  This is the default for both medical and inst when creating X12 claims, regardless of what is set on the proc. */
			ServiceUnits,
			/** 3-Only allowed on institutional SV204. */
			Days
		}


}
