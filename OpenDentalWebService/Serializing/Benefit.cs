using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Benefit {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Benefit benefit) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Benefit>");
			sb.Append("<BenefitNum>").Append(benefit.BenefitNum).Append("</BenefitNum>");
			sb.Append("<PlanNum>").Append(benefit.PlanNum).Append("</PlanNum>");
			sb.Append("<PatPlanNum>").Append(benefit.PatPlanNum).Append("</PatPlanNum>");
			sb.Append("<CovCatNum>").Append(benefit.CovCatNum).Append("</CovCatNum>");
			sb.Append("<BenefitType>").Append((int)benefit.BenefitType).Append("</BenefitType>");
			sb.Append("<Percent>").Append(benefit.Percent).Append("</Percent>");
			sb.Append("<MonetaryAmt>").Append(benefit.MonetaryAmt).Append("</MonetaryAmt>");
			sb.Append("<TimePeriod>").Append((int)benefit.TimePeriod).Append("</TimePeriod>");
			sb.Append("<QuantityQualifier>").Append((int)benefit.QuantityQualifier).Append("</QuantityQualifier>");
			sb.Append("<Quantity>").Append(benefit.Quantity).Append("</Quantity>");
			sb.Append("<CodeNum>").Append(benefit.CodeNum).Append("</CodeNum>");
			sb.Append("<CoverageLevel>").Append((int)benefit.CoverageLevel).Append("</CoverageLevel>");
			sb.Append("</Benefit>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Benefit Deserialize(string xml) {
			OpenDentBusiness.Benefit benefit=new OpenDentBusiness.Benefit();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "BenefitNum":
							benefit.BenefitNum=reader.ReadContentAsLong();
							break;
						case "PlanNum":
							benefit.PlanNum=reader.ReadContentAsLong();
							break;
						case "PatPlanNum":
							benefit.PatPlanNum=reader.ReadContentAsLong();
							break;
						case "CovCatNum":
							benefit.CovCatNum=reader.ReadContentAsLong();
							break;
						case "BenefitType":
							benefit.BenefitType=(OpenDentBusiness.InsBenefitType)reader.ReadContentAsInt();
							break;
						case "Percent":
							benefit.Percent=reader.ReadContentAsInt();
							break;
						case "MonetaryAmt":
							benefit.MonetaryAmt=reader.ReadContentAsDouble();
							break;
						case "TimePeriod":
							benefit.TimePeriod=(OpenDentBusiness.BenefitTimePeriod)reader.ReadContentAsInt();
							break;
						case "QuantityQualifier":
							benefit.QuantityQualifier=(OpenDentBusiness.BenefitQuantity)reader.ReadContentAsInt();
							break;
						case "Quantity":
							benefit.Quantity=(byte)reader.ReadContentAsInt();
							break;
						case "CodeNum":
							benefit.CodeNum=reader.ReadContentAsLong();
							break;
						case "CoverageLevel":
							benefit.CoverageLevel=(OpenDentBusiness.BenefitCoverageLevel)reader.ReadContentAsInt();
							break;
					}
				}
			}
			return benefit;
		}


	}
}