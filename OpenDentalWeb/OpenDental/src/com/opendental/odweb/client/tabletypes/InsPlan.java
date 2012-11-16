package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class InsPlan {
		/** Primary key. */
		public int PlanNum;
		/** Optional */
		public String GroupName;
		/** Optional.  In Canada, this is called the Plan Number. */
		public String GroupNum;
		/** Note for this plan.  Same for all subscribers. */
		public String PlanNote;
		/** FK to feesched.FeeSchedNum. */
		public int FeeSched;
		/** ""=percentage(the default),"p"=ppo_percentage,"f"=flatCopay,"c"=capitation. */
		public String PlanType;
		/** FK to claimform.ClaimFormNum. eg. "1" for ADA2002.  For ADA2006, it varies by office. */
		public int ClaimFormNum;
		/** 0=no,1=yes.  could later be extended if more alternates required */
		public boolean UseAltCode;
		/** Fee billed on claim should be the UCR fee for the patient's provider. */
		public boolean ClaimsUseUCR;
		/** FK to feesched.FeeSchedNum. Not usually used. This fee schedule holds only co-pays(patient portions).  Only used for Capitation or for fixed copay plans. */
		public int CopayFeeSched;
		/** FK to employer.EmployerNum. */
		public int EmployerNum;
		/** FK to carrier.CarrierNum. */
		public int CarrierNum;
		/** FK to feesched.FeeSchedNum. Not usually used.  This fee schedule holds amounts allowed by carriers. */
		public int AllowedFeeSched;
		/** . */
		public String TrojanID;
		/** Only used in Canada. It's a suffix to the plan number (group number). */
		public String DivisionNo;
		/** True if this is medical insurance rather than dental insurance.  When creating a claim, this, along with pref. */
		public boolean IsMedical;
		/** FK to insfilingcode.InsFilingCodeNum.  Used for e-claims.  Also used for some complex reports in public health.  The e-claim usage might become obsolete when PlanID implemented by HIPAA.  Can be 0 to indicate none.  Then 'CI' will go out on claims. */
		public int FilingCode;
		/** Canadian e-claim field. D11 and E07.  Zero indicates empty.  Mandatory value for Dentaide.  Not used for all others.  2 digit. */
		public byte DentaideCardSequence;
		/** If checked, the units Qty will show the base units assigned to a procedure on the claim form. */
		public boolean ShowBaseUnits;
		/** Set to true to not allow procedure code downgrade substitution on this insurance plan. */
		public boolean CodeSubstNone;
		/** Set to true to hide it from the pick list and from the main list. */
		public boolean IsHidden;
		/** The month, 1 through 12 when the insurance plan renews.  It will renew on the first of the month.  To indicate calendar year, set renew month to 0. */
		public byte MonthRenew;
		/** FK to insfilingcodesubtype.insfilingcodesubtypenum */
		public int FilingCodeSubtype;
		/** Canadian C12.  Single char, usually blank.  If non-blank, then it's one of three kinds of Provincial Medical Plans.  A=Newfoundland MCP Plan.  V=Veteran's Affairs Plan.  N=NIHB.  N and V are not yet in use, so they will result in blank being sent instead.  See Elig5. */
		public String CanadianPlanFlag;
		/** Canadian C39. Required when CanadianPlanFlag is 'A'. */
		public String CanadianDiagnosticCode;
		/** Canadian C40. Required when CanadianPlanFlag is 'A'. */
		public String CanadianInstitutionCode;
		/** BIN location number.  Only used with EHR. */
		public String RxBIN;
		/** Enum:EnumCobRule. 0=Basic, 1=Standard, 2=CarveOut.  */
		public EnumCobRule CobRule;

		/** Deep copy of object. */
		public InsPlan Copy() {
			InsPlan insplan=new InsPlan();
			insplan.PlanNum=this.PlanNum;
			insplan.GroupName=this.GroupName;
			insplan.GroupNum=this.GroupNum;
			insplan.PlanNote=this.PlanNote;
			insplan.FeeSched=this.FeeSched;
			insplan.PlanType=this.PlanType;
			insplan.ClaimFormNum=this.ClaimFormNum;
			insplan.UseAltCode=this.UseAltCode;
			insplan.ClaimsUseUCR=this.ClaimsUseUCR;
			insplan.CopayFeeSched=this.CopayFeeSched;
			insplan.EmployerNum=this.EmployerNum;
			insplan.CarrierNum=this.CarrierNum;
			insplan.AllowedFeeSched=this.AllowedFeeSched;
			insplan.TrojanID=this.TrojanID;
			insplan.DivisionNo=this.DivisionNo;
			insplan.IsMedical=this.IsMedical;
			insplan.FilingCode=this.FilingCode;
			insplan.DentaideCardSequence=this.DentaideCardSequence;
			insplan.ShowBaseUnits=this.ShowBaseUnits;
			insplan.CodeSubstNone=this.CodeSubstNone;
			insplan.IsHidden=this.IsHidden;
			insplan.MonthRenew=this.MonthRenew;
			insplan.FilingCodeSubtype=this.FilingCodeSubtype;
			insplan.CanadianPlanFlag=this.CanadianPlanFlag;
			insplan.CanadianDiagnosticCode=this.CanadianDiagnosticCode;
			insplan.CanadianInstitutionCode=this.CanadianInstitutionCode;
			insplan.RxBIN=this.RxBIN;
			insplan.CobRule=this.CobRule;
			return insplan;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsPlan>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<GroupName>").append(Serializing.EscapeForXml(GroupName)).append("</GroupName>");
			sb.append("<GroupNum>").append(Serializing.EscapeForXml(GroupNum)).append("</GroupNum>");
			sb.append("<PlanNote>").append(Serializing.EscapeForXml(PlanNote)).append("</PlanNote>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<PlanType>").append(Serializing.EscapeForXml(PlanType)).append("</PlanType>");
			sb.append("<ClaimFormNum>").append(ClaimFormNum).append("</ClaimFormNum>");
			sb.append("<UseAltCode>").append((UseAltCode)?1:0).append("</UseAltCode>");
			sb.append("<ClaimsUseUCR>").append((ClaimsUseUCR)?1:0).append("</ClaimsUseUCR>");
			sb.append("<CopayFeeSched>").append(CopayFeeSched).append("</CopayFeeSched>");
			sb.append("<EmployerNum>").append(EmployerNum).append("</EmployerNum>");
			sb.append("<CarrierNum>").append(CarrierNum).append("</CarrierNum>");
			sb.append("<AllowedFeeSched>").append(AllowedFeeSched).append("</AllowedFeeSched>");
			sb.append("<TrojanID>").append(Serializing.EscapeForXml(TrojanID)).append("</TrojanID>");
			sb.append("<DivisionNo>").append(Serializing.EscapeForXml(DivisionNo)).append("</DivisionNo>");
			sb.append("<IsMedical>").append((IsMedical)?1:0).append("</IsMedical>");
			sb.append("<FilingCode>").append(FilingCode).append("</FilingCode>");
			sb.append("<DentaideCardSequence>").append(DentaideCardSequence).append("</DentaideCardSequence>");
			sb.append("<ShowBaseUnits>").append((ShowBaseUnits)?1:0).append("</ShowBaseUnits>");
			sb.append("<CodeSubstNone>").append((CodeSubstNone)?1:0).append("</CodeSubstNone>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<MonthRenew>").append(MonthRenew).append("</MonthRenew>");
			sb.append("<FilingCodeSubtype>").append(FilingCodeSubtype).append("</FilingCodeSubtype>");
			sb.append("<CanadianPlanFlag>").append(Serializing.EscapeForXml(CanadianPlanFlag)).append("</CanadianPlanFlag>");
			sb.append("<CanadianDiagnosticCode>").append(Serializing.EscapeForXml(CanadianDiagnosticCode)).append("</CanadianDiagnosticCode>");
			sb.append("<CanadianInstitutionCode>").append(Serializing.EscapeForXml(CanadianInstitutionCode)).append("</CanadianInstitutionCode>");
			sb.append("<RxBIN>").append(Serializing.EscapeForXml(RxBIN)).append("</RxBIN>");
			sb.append("<CobRule>").append(CobRule.ordinal()).append("</CobRule>");
			sb.append("</InsPlan>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"GroupName")!=null) {
					GroupName=Serializing.GetXmlNodeValue(doc,"GroupName");
				}
				if(Serializing.GetXmlNodeValue(doc,"GroupNum")!=null) {
					GroupNum=Serializing.GetXmlNodeValue(doc,"GroupNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"PlanNote")!=null) {
					PlanNote=Serializing.GetXmlNodeValue(doc,"PlanNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlanType")!=null) {
					PlanType=Serializing.GetXmlNodeValue(doc,"PlanType");
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimFormNum")!=null) {
					ClaimFormNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimFormNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UseAltCode")!=null) {
					UseAltCode=(Serializing.GetXmlNodeValue(doc,"UseAltCode")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimsUseUCR")!=null) {
					ClaimsUseUCR=(Serializing.GetXmlNodeValue(doc,"ClaimsUseUCR")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"CopayFeeSched")!=null) {
					CopayFeeSched=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CopayFeeSched"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmployerNum")!=null) {
					EmployerNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployerNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CarrierNum")!=null) {
					CarrierNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CarrierNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AllowedFeeSched")!=null) {
					AllowedFeeSched=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AllowedFeeSched"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TrojanID")!=null) {
					TrojanID=Serializing.GetXmlNodeValue(doc,"TrojanID");
				}
				if(Serializing.GetXmlNodeValue(doc,"DivisionNo")!=null) {
					DivisionNo=Serializing.GetXmlNodeValue(doc,"DivisionNo");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsMedical")!=null) {
					IsMedical=(Serializing.GetXmlNodeValue(doc,"IsMedical")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"FilingCode")!=null) {
					FilingCode=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FilingCode"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DentaideCardSequence")!=null) {
					DentaideCardSequence=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"DentaideCardSequence"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ShowBaseUnits")!=null) {
					ShowBaseUnits=(Serializing.GetXmlNodeValue(doc,"ShowBaseUnits")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeSubstNone")!=null) {
					CodeSubstNone=(Serializing.GetXmlNodeValue(doc,"CodeSubstNone")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"MonthRenew")!=null) {
					MonthRenew=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"MonthRenew"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FilingCodeSubtype")!=null) {
					FilingCodeSubtype=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FilingCodeSubtype"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianPlanFlag")!=null) {
					CanadianPlanFlag=Serializing.GetXmlNodeValue(doc,"CanadianPlanFlag");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianDiagnosticCode")!=null) {
					CanadianDiagnosticCode=Serializing.GetXmlNodeValue(doc,"CanadianDiagnosticCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianInstitutionCode")!=null) {
					CanadianInstitutionCode=Serializing.GetXmlNodeValue(doc,"CanadianInstitutionCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"RxBIN")!=null) {
					RxBIN=Serializing.GetXmlNodeValue(doc,"RxBIN");
				}
				if(Serializing.GetXmlNodeValue(doc,"CobRule")!=null) {
					CobRule=EnumCobRule.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CobRule"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum EnumCobRule {
			/** 0=Basic */
			Basic,
			/** 1=Standard */
			Standard,
			/** 2=CarveOut */
			CarveOut
		}


}
