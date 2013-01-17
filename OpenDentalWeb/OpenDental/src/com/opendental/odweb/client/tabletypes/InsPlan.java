package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** This is not a database column.  It is just used to display the number of plans with the same info. */
		public int NumberSubscribers;

		/** Deep copy of object. */
		public InsPlan deepCopy() {
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
			insplan.NumberSubscribers=this.NumberSubscribers;
			return insplan;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsPlan>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<GroupName>").append(Serializing.escapeForXml(GroupName)).append("</GroupName>");
			sb.append("<GroupNum>").append(Serializing.escapeForXml(GroupNum)).append("</GroupNum>");
			sb.append("<PlanNote>").append(Serializing.escapeForXml(PlanNote)).append("</PlanNote>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<PlanType>").append(Serializing.escapeForXml(PlanType)).append("</PlanType>");
			sb.append("<ClaimFormNum>").append(ClaimFormNum).append("</ClaimFormNum>");
			sb.append("<UseAltCode>").append((UseAltCode)?1:0).append("</UseAltCode>");
			sb.append("<ClaimsUseUCR>").append((ClaimsUseUCR)?1:0).append("</ClaimsUseUCR>");
			sb.append("<CopayFeeSched>").append(CopayFeeSched).append("</CopayFeeSched>");
			sb.append("<EmployerNum>").append(EmployerNum).append("</EmployerNum>");
			sb.append("<CarrierNum>").append(CarrierNum).append("</CarrierNum>");
			sb.append("<AllowedFeeSched>").append(AllowedFeeSched).append("</AllowedFeeSched>");
			sb.append("<TrojanID>").append(Serializing.escapeForXml(TrojanID)).append("</TrojanID>");
			sb.append("<DivisionNo>").append(Serializing.escapeForXml(DivisionNo)).append("</DivisionNo>");
			sb.append("<IsMedical>").append((IsMedical)?1:0).append("</IsMedical>");
			sb.append("<FilingCode>").append(FilingCode).append("</FilingCode>");
			sb.append("<DentaideCardSequence>").append(DentaideCardSequence).append("</DentaideCardSequence>");
			sb.append("<ShowBaseUnits>").append((ShowBaseUnits)?1:0).append("</ShowBaseUnits>");
			sb.append("<CodeSubstNone>").append((CodeSubstNone)?1:0).append("</CodeSubstNone>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<MonthRenew>").append(MonthRenew).append("</MonthRenew>");
			sb.append("<FilingCodeSubtype>").append(FilingCodeSubtype).append("</FilingCodeSubtype>");
			sb.append("<CanadianPlanFlag>").append(Serializing.escapeForXml(CanadianPlanFlag)).append("</CanadianPlanFlag>");
			sb.append("<CanadianDiagnosticCode>").append(Serializing.escapeForXml(CanadianDiagnosticCode)).append("</CanadianDiagnosticCode>");
			sb.append("<CanadianInstitutionCode>").append(Serializing.escapeForXml(CanadianInstitutionCode)).append("</CanadianInstitutionCode>");
			sb.append("<RxBIN>").append(Serializing.escapeForXml(RxBIN)).append("</RxBIN>");
			sb.append("<CobRule>").append(CobRule.ordinal()).append("</CobRule>");
			sb.append("<NumberSubscribers>").append(NumberSubscribers).append("</NumberSubscribers>");
			sb.append("</InsPlan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"GroupName")!=null) {
					GroupName=Serializing.getXmlNodeValue(doc,"GroupName");
				}
				if(Serializing.getXmlNodeValue(doc,"GroupNum")!=null) {
					GroupNum=Serializing.getXmlNodeValue(doc,"GroupNum");
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNote")!=null) {
					PlanNote=Serializing.getXmlNodeValue(doc,"PlanNote");
				}
				if(Serializing.getXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlanType")!=null) {
					PlanType=Serializing.getXmlNodeValue(doc,"PlanType");
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimFormNum")!=null) {
					ClaimFormNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimFormNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UseAltCode")!=null) {
					UseAltCode=(Serializing.getXmlNodeValue(doc,"UseAltCode")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimsUseUCR")!=null) {
					ClaimsUseUCR=(Serializing.getXmlNodeValue(doc,"ClaimsUseUCR")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"CopayFeeSched")!=null) {
					CopayFeeSched=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CopayFeeSched"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmployerNum")!=null) {
					EmployerNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployerNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierNum")!=null) {
					CarrierNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CarrierNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AllowedFeeSched")!=null) {
					AllowedFeeSched=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AllowedFeeSched"));
				}
				if(Serializing.getXmlNodeValue(doc,"TrojanID")!=null) {
					TrojanID=Serializing.getXmlNodeValue(doc,"TrojanID");
				}
				if(Serializing.getXmlNodeValue(doc,"DivisionNo")!=null) {
					DivisionNo=Serializing.getXmlNodeValue(doc,"DivisionNo");
				}
				if(Serializing.getXmlNodeValue(doc,"IsMedical")!=null) {
					IsMedical=(Serializing.getXmlNodeValue(doc,"IsMedical")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"FilingCode")!=null) {
					FilingCode=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FilingCode"));
				}
				if(Serializing.getXmlNodeValue(doc,"DentaideCardSequence")!=null) {
					DentaideCardSequence=Byte.valueOf(Serializing.getXmlNodeValue(doc,"DentaideCardSequence"));
				}
				if(Serializing.getXmlNodeValue(doc,"ShowBaseUnits")!=null) {
					ShowBaseUnits=(Serializing.getXmlNodeValue(doc,"ShowBaseUnits")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"CodeSubstNone")!=null) {
					CodeSubstNone=(Serializing.getXmlNodeValue(doc,"CodeSubstNone")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"MonthRenew")!=null) {
					MonthRenew=Byte.valueOf(Serializing.getXmlNodeValue(doc,"MonthRenew"));
				}
				if(Serializing.getXmlNodeValue(doc,"FilingCodeSubtype")!=null) {
					FilingCodeSubtype=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FilingCodeSubtype"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianPlanFlag")!=null) {
					CanadianPlanFlag=Serializing.getXmlNodeValue(doc,"CanadianPlanFlag");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianDiagnosticCode")!=null) {
					CanadianDiagnosticCode=Serializing.getXmlNodeValue(doc,"CanadianDiagnosticCode");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianInstitutionCode")!=null) {
					CanadianInstitutionCode=Serializing.getXmlNodeValue(doc,"CanadianInstitutionCode");
				}
				if(Serializing.getXmlNodeValue(doc,"RxBIN")!=null) {
					RxBIN=Serializing.getXmlNodeValue(doc,"RxBIN");
				}
				if(Serializing.getXmlNodeValue(doc,"CobRule")!=null) {
					CobRule=EnumCobRule.valueOf(Serializing.getXmlNodeValue(doc,"CobRule"));
				}
				if(Serializing.getXmlNodeValue(doc,"NumberSubscribers")!=null) {
					NumberSubscribers=Integer.valueOf(Serializing.getXmlNodeValue(doc,"NumberSubscribers"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing InsPlan: "+e.getMessage());
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
