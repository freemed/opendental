package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Benefit {
		/** Primary key. */
		public int BenefitNum;
		/** FK to insplan.PlanNum.  Most benefits should be attached using PlanNum.  The exception would be if each patient has a different percentage.  If PlanNum is used, then PatPlanNum should be 0. */
		public int PlanNum;
		/** FK to patplan.PatPlanNum.  It is rare to attach benefits this way.  Usually only used to override percentages for patients.   In this case, PlanNum should be 0. */
		public int PatPlanNum;
		/** FK to covcat.CovCatNum.  Corresponds to X12 EB03- Service Type code.  Situational, so it can be 0.  Will probably be 0 for general deductible and annual max.  There are very specific categories covered by X12. Users should set their InsCovCats to the defaults we provide. */
		public int CovCatNum;
		/** Enum:InsBenefitType Corresponds to X12 EB01. Examples: 0=ActiveCoverage, 1=CoInsurance, 2=Deductible, 3=CoPayment, 4=Exclusions, 5=Limitations. ActiveCoverage doesn't really provide meaningful information. */
		public InsBenefitType BenefitType;
		/** Only used if BenefitType=CoInsurance.  Valid values are 0 to 100.  -1 indicates empty, which is almost always true if not CoInsurance.  The percentage that insurance will pay on the procedure.  Note that benefits coming from carriers are usually backwards, indicating the percetage that the patient is responsible for. */
		public int Percent;
		/** Used for CoPayment, Limitations, and Deductible.  -1 indicates empty */
		public double MonetaryAmt;
		/** Enum:BenefitTimePeriod Corresponds to X12 EB06, Time Period Qualifier.  Examples: 0=None,1=ServiceYear,2=CalendarYear,3=Lifetime,4=Years. Might add Visit and Remaining. */
		public BenefitTimePeriod TimePeriod;
		/** Enum:BenefitQuantity Corresponds to X12 EB09. Not used very much. Examples: 0=None,1=NumberOfServices,2=AgeLimit,3=Visits,4=Years,5=Months */
		public BenefitQuantity QuantityQualifier;
		/** Corresponds to X12 EB10. Qualify the quantity using QuantityQualifier. */
		public byte Quantity;
		/** FK to procedurecode.CodeNum.  Typical uses include fluoride, sealants, etc.  If a specific code is used here, then the CovCat should be None. */
		public int CodeNum;
		/** Enum:BenefitCoverageLevel Corresponds to X12 EB02.  None, Individual, or Family.  Individual and Family are commonly used for deductibles and maximums.  None is commonly used for percentages and copays. */
		public BenefitCoverageLevel CoverageLevel;

		/** Deep copy of object. */
		public Benefit Copy() {
			Benefit benefit=new Benefit();
			benefit.BenefitNum=this.BenefitNum;
			benefit.PlanNum=this.PlanNum;
			benefit.PatPlanNum=this.PatPlanNum;
			benefit.CovCatNum=this.CovCatNum;
			benefit.BenefitType=this.BenefitType;
			benefit.Percent=this.Percent;
			benefit.MonetaryAmt=this.MonetaryAmt;
			benefit.TimePeriod=this.TimePeriod;
			benefit.QuantityQualifier=this.QuantityQualifier;
			benefit.Quantity=this.Quantity;
			benefit.CodeNum=this.CodeNum;
			benefit.CoverageLevel=this.CoverageLevel;
			return benefit;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Benefit>");
			sb.append("<BenefitNum>").append(BenefitNum).append("</BenefitNum>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<PatPlanNum>").append(PatPlanNum).append("</PatPlanNum>");
			sb.append("<CovCatNum>").append(CovCatNum).append("</CovCatNum>");
			sb.append("<BenefitType>").append(BenefitType.ordinal()).append("</BenefitType>");
			sb.append("<Percent>").append(Percent).append("</Percent>");
			sb.append("<MonetaryAmt>").append(MonetaryAmt).append("</MonetaryAmt>");
			sb.append("<TimePeriod>").append(TimePeriod.ordinal()).append("</TimePeriod>");
			sb.append("<QuantityQualifier>").append(QuantityQualifier.ordinal()).append("</QuantityQualifier>");
			sb.append("<Quantity>").append(Quantity).append("</Quantity>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<CoverageLevel>").append(CoverageLevel.ordinal()).append("</CoverageLevel>");
			sb.append("</Benefit>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"BenefitNum")!=null) {
					BenefitNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BenefitNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatPlanNum")!=null) {
					PatPlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatPlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CovCatNum")!=null) {
					CovCatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CovCatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BenefitType")!=null) {
					BenefitType=InsBenefitType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BenefitType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Percent")!=null) {
					Percent=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Percent"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MonetaryAmt")!=null) {
					MonetaryAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"MonetaryAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TimePeriod")!=null) {
					TimePeriod=BenefitTimePeriod.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TimePeriod"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"QuantityQualifier")!=null) {
					QuantityQualifier=BenefitQuantity.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QuantityQualifier"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Quantity")!=null) {
					Quantity=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"Quantity"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CoverageLevel")!=null) {
					CoverageLevel=BenefitCoverageLevel.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CoverageLevel"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Used in the benefit table.  Corresponds to X12 EB01. */
		public enum InsBenefitType {
			/** 0- Not usually used.  Would only be used if you are just indicating that the patient is covered, but without any specifics. */
			ActiveCoverage,
			/** 1- Used for percentages to indicate portion that insurance will cover.  When interpreting electronic benefit information, this is the opposite percentage, the percentage that the patient will pay after deductible. */
			CoInsurance,
			/** 2- The deductible amount.  Might be two entries if, for instance, deductible is waived on preventive. */
			Deductible,
			/** 3- A dollar amount. */
			CoPayment,
			/** 4- Services that are simply not covered at all. */
			Exclusions,
			/** 5- Covers a variety of limitations, including Max, frequency, fee reductions, etc. */
			Limitations
		}

		/** Used in the benefit table.  Corresponds to X12 EB06. */
		public enum BenefitTimePeriod {
			/** 0- A timeperiod is frequenly not needed.  For example, percentages. */
			None,
			/** 1- The renewal month is not Jan.  In this case, we need to know the effective date so that we know which month the benefits start over in. */
			ServiceYear,
			/** 2- Renewal month is Jan. */
			CalendarYear,
			/** 3- Usually used for ortho max. */
			Lifetime,
			/** 4- Wouldn't be used alone.  Years would again be specified in the quantity field along with a number. */
			Years
		}

		/** Used in the benefit table in conjunction with an integer quantity. */
		public enum BenefitQuantity {
			/** 0- This is used a lot. Most benefits do not need any sort of quantity. */
			None,
			/** 1- For example, two exams per year */
			NumberOfServices,
			/** 2- For example, 18 when flouride only covered to 18 y.o. */
			AgeLimit,
			/** 3- For example, copay per 1 visit. */
			Visits,
			/** 4- For example, pano every 5 years. */
			Years,
			/** 5- For example, BWs every 6 months. */
			Months
		}

		/** Used in the benefit table. */
		public enum BenefitCoverageLevel {
			/** 0- Since this is a situational X12 field, we can also have none.  Typical for percentages and copayments. */
			None,
			/** 1- The default for deductibles and maximums. */
			Individual,
			/** 2- For example, family deductible or family maximum. */
			Family
		}


}
