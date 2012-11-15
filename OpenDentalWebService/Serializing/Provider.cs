using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Provider {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Provider provider) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Provider>");
			sb.Append("<ProvNum>").Append(provider.ProvNum).Append("</ProvNum>");
			sb.Append("<Abbr>").Append(SerializeStringEscapes.EscapeForXml(provider.Abbr)).Append("</Abbr>");
			sb.Append("<ItemOrder>").Append(provider.ItemOrder).Append("</ItemOrder>");
			sb.Append("<LName>").Append(SerializeStringEscapes.EscapeForXml(provider.LName)).Append("</LName>");
			sb.Append("<FName>").Append(SerializeStringEscapes.EscapeForXml(provider.FName)).Append("</FName>");
			sb.Append("<MI>").Append(SerializeStringEscapes.EscapeForXml(provider.MI)).Append("</MI>");
			sb.Append("<Suffix>").Append(SerializeStringEscapes.EscapeForXml(provider.Suffix)).Append("</Suffix>");
			sb.Append("<FeeSched>").Append(provider.FeeSched).Append("</FeeSched>");
			sb.Append("<Specialty>").Append((int)provider.Specialty).Append("</Specialty>");
			sb.Append("<SSN>").Append(SerializeStringEscapes.EscapeForXml(provider.SSN)).Append("</SSN>");
			sb.Append("<StateLicense>").Append(SerializeStringEscapes.EscapeForXml(provider.StateLicense)).Append("</StateLicense>");
			sb.Append("<DEANum>").Append(SerializeStringEscapes.EscapeForXml(provider.DEANum)).Append("</DEANum>");
			sb.Append("<IsSecondary>").Append((provider.IsSecondary)?1:0).Append("</IsSecondary>");
			sb.Append("<ProvColor>").Append(provider.ProvColor.ToArgb()).Append("</ProvColor>");
			sb.Append("<IsHidden>").Append((provider.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<UsingTIN>").Append((provider.UsingTIN)?1:0).Append("</UsingTIN>");
			sb.Append("<BlueCrossID>").Append(SerializeStringEscapes.EscapeForXml(provider.BlueCrossID)).Append("</BlueCrossID>");
			sb.Append("<SigOnFile>").Append((provider.SigOnFile)?1:0).Append("</SigOnFile>");
			sb.Append("<MedicaidID>").Append(SerializeStringEscapes.EscapeForXml(provider.MedicaidID)).Append("</MedicaidID>");
			sb.Append("<OutlineColor>").Append(provider.OutlineColor.ToArgb()).Append("</OutlineColor>");
			sb.Append("<SchoolClassNum>").Append(provider.SchoolClassNum).Append("</SchoolClassNum>");
			sb.Append("<NationalProvID>").Append(SerializeStringEscapes.EscapeForXml(provider.NationalProvID)).Append("</NationalProvID>");
			sb.Append("<CanadianOfficeNum>").Append(SerializeStringEscapes.EscapeForXml(provider.CanadianOfficeNum)).Append("</CanadianOfficeNum>");
			sb.Append("<DateTStamp>").Append(provider.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<AnesthProvType>").Append(provider.AnesthProvType).Append("</AnesthProvType>");
			sb.Append("<TaxonomyCodeOverride>").Append(SerializeStringEscapes.EscapeForXml(provider.TaxonomyCodeOverride)).Append("</TaxonomyCodeOverride>");
			sb.Append("<IsCDAnet>").Append((provider.IsCDAnet)?1:0).Append("</IsCDAnet>");
			sb.Append("<EcwID>").Append(SerializeStringEscapes.EscapeForXml(provider.EcwID)).Append("</EcwID>");
			sb.Append("<EhrKey>").Append(SerializeStringEscapes.EscapeForXml(provider.EhrKey)).Append("</EhrKey>");
			sb.Append("<StateRxID>").Append(SerializeStringEscapes.EscapeForXml(provider.StateRxID)).Append("</StateRxID>");
			sb.Append("<EhrHasReportAccess>").Append((provider.EhrHasReportAccess)?1:0).Append("</EhrHasReportAccess>");
			sb.Append("<IsNotPerson>").Append((provider.IsNotPerson)?1:0).Append("</IsNotPerson>");
			sb.Append("<StateWhereLicensed>").Append(SerializeStringEscapes.EscapeForXml(provider.StateWhereLicensed)).Append("</StateWhereLicensed>");
			sb.Append("</Provider>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Provider Deserialize(string xml) {
			OpenDentBusiness.Provider provider=new OpenDentBusiness.Provider();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProvNum":
							provider.ProvNum=reader.ReadContentAsLong();
							break;
						case "Abbr":
							provider.Abbr=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							provider.ItemOrder=reader.ReadContentAsInt();
							break;
						case "LName":
							provider.LName=reader.ReadContentAsString();
							break;
						case "FName":
							provider.FName=reader.ReadContentAsString();
							break;
						case "MI":
							provider.MI=reader.ReadContentAsString();
							break;
						case "Suffix":
							provider.Suffix=reader.ReadContentAsString();
							break;
						case "FeeSched":
							provider.FeeSched=reader.ReadContentAsLong();
							break;
						case "Specialty":
							provider.Specialty=(OpenDentBusiness.DentalSpecialty)reader.ReadContentAsInt();
							break;
						case "SSN":
							provider.SSN=reader.ReadContentAsString();
							break;
						case "StateLicense":
							provider.StateLicense=reader.ReadContentAsString();
							break;
						case "DEANum":
							provider.DEANum=reader.ReadContentAsString();
							break;
						case "IsSecondary":
							provider.IsSecondary=reader.ReadContentAsString()!="0";
							break;
						case "ProvColor":
							provider.ProvColor=Color.FromArgb(reader.ReadContentAsInt());
							break;
						case "IsHidden":
							provider.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "UsingTIN":
							provider.UsingTIN=reader.ReadContentAsString()!="0";
							break;
						case "BlueCrossID":
							provider.BlueCrossID=reader.ReadContentAsString();
							break;
						case "SigOnFile":
							provider.SigOnFile=reader.ReadContentAsString()!="0";
							break;
						case "MedicaidID":
							provider.MedicaidID=reader.ReadContentAsString();
							break;
						case "OutlineColor":
							provider.OutlineColor=Color.FromArgb(reader.ReadContentAsInt());
							break;
						case "SchoolClassNum":
							provider.SchoolClassNum=reader.ReadContentAsLong();
							break;
						case "NationalProvID":
							provider.NationalProvID=reader.ReadContentAsString();
							break;
						case "CanadianOfficeNum":
							provider.CanadianOfficeNum=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							provider.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "AnesthProvType":
							provider.AnesthProvType=reader.ReadContentAsLong();
							break;
						case "TaxonomyCodeOverride":
							provider.TaxonomyCodeOverride=reader.ReadContentAsString();
							break;
						case "IsCDAnet":
							provider.IsCDAnet=reader.ReadContentAsString()!="0";
							break;
						case "EcwID":
							provider.EcwID=reader.ReadContentAsString();
							break;
						case "EhrKey":
							provider.EhrKey=reader.ReadContentAsString();
							break;
						case "StateRxID":
							provider.StateRxID=reader.ReadContentAsString();
							break;
						case "EhrHasReportAccess":
							provider.EhrHasReportAccess=reader.ReadContentAsString()!="0";
							break;
						case "IsNotPerson":
							provider.IsNotPerson=reader.ReadContentAsString()!="0";
							break;
						case "StateWhereLicensed":
							provider.StateWhereLicensed=reader.ReadContentAsString();
							break;
					}
				}
			}
			return provider;
		}


	}
}