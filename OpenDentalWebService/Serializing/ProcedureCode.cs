using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcedureCode {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcedureCode procedurecode) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcedureCode>");
			sb.Append("<CodeNum>").Append(procedurecode.CodeNum).Append("</CodeNum>");
			sb.Append("<ProcCode>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.ProcCode)).Append("</ProcCode>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.Descript)).Append("</Descript>");
			sb.Append("<AbbrDesc>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.AbbrDesc)).Append("</AbbrDesc>");
			sb.Append("<ProcTime>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.ProcTime)).Append("</ProcTime>");
			sb.Append("<ProcCat>").Append(procedurecode.ProcCat).Append("</ProcCat>");
			sb.Append("<TreatArea>").Append((int)procedurecode.TreatArea).Append("</TreatArea>");
			sb.Append("<NoBillIns>").Append((procedurecode.NoBillIns)?1:0).Append("</NoBillIns>");
			sb.Append("<IsProsth>").Append((procedurecode.IsProsth)?1:0).Append("</IsProsth>");
			sb.Append("<DefaultNote>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.DefaultNote)).Append("</DefaultNote>");
			sb.Append("<IsHygiene>").Append((procedurecode.IsHygiene)?1:0).Append("</IsHygiene>");
			sb.Append("<GTypeNum>").Append(procedurecode.GTypeNum).Append("</GTypeNum>");
			sb.Append("<AlternateCode1>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.AlternateCode1)).Append("</AlternateCode1>");
			sb.Append("<MedicalCode>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.MedicalCode)).Append("</MedicalCode>");
			sb.Append("<IsTaxed>").Append((procedurecode.IsTaxed)?1:0).Append("</IsTaxed>");
			sb.Append("<PaintType>").Append((int)procedurecode.PaintType).Append("</PaintType>");
			sb.Append("<GraphicColor>").Append(procedurecode.GraphicColor.ToArgb()).Append("</GraphicColor>");
			sb.Append("<LaymanTerm>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.LaymanTerm)).Append("</LaymanTerm>");
			sb.Append("<IsCanadianLab>").Append((procedurecode.IsCanadianLab)?1:0).Append("</IsCanadianLab>");
			sb.Append("<PreExisting>").Append((procedurecode.PreExisting)?1:0).Append("</PreExisting>");
			sb.Append("<BaseUnits>").Append(procedurecode.BaseUnits).Append("</BaseUnits>");
			sb.Append("<SubstitutionCode>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.SubstitutionCode)).Append("</SubstitutionCode>");
			sb.Append("<SubstOnlyIf>").Append((int)procedurecode.SubstOnlyIf).Append("</SubstOnlyIf>");
			sb.Append("<DateTStamp>").Append(procedurecode.DateTStamp.ToLongDateString()).Append("</DateTStamp>");
			sb.Append("<IsMultiVisit>").Append((procedurecode.IsMultiVisit)?1:0).Append("</IsMultiVisit>");
			sb.Append("<DrugNDC>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.DrugNDC)).Append("</DrugNDC>");
			sb.Append("<RevenueCodeDefault>").Append(SerializeStringEscapes.EscapeForXml(procedurecode.RevenueCodeDefault)).Append("</RevenueCodeDefault>");
			sb.Append("<ProvNumDefault>").Append(procedurecode.ProvNumDefault).Append("</ProvNumDefault>");
			sb.Append("</ProcedureCode>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcedureCode Deserialize(string xml) {
			OpenDentBusiness.ProcedureCode procedurecode=new OpenDentBusiness.ProcedureCode();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CodeNum":
							procedurecode.CodeNum=reader.ReadContentAsLong();
							break;
						case "ProcCode":
							procedurecode.ProcCode=reader.ReadContentAsString();
							break;
						case "Descript":
							procedurecode.Descript=reader.ReadContentAsString();
							break;
						case "AbbrDesc":
							procedurecode.AbbrDesc=reader.ReadContentAsString();
							break;
						case "ProcTime":
							procedurecode.ProcTime=reader.ReadContentAsString();
							break;
						case "ProcCat":
							procedurecode.ProcCat=reader.ReadContentAsLong();
							break;
						case "TreatArea":
							procedurecode.TreatArea=(OpenDentBusiness.TreatmentArea)reader.ReadContentAsInt();
							break;
						case "NoBillIns":
							procedurecode.NoBillIns=reader.ReadContentAsString()!="0";
							break;
						case "IsProsth":
							procedurecode.IsProsth=reader.ReadContentAsString()!="0";
							break;
						case "DefaultNote":
							procedurecode.DefaultNote=reader.ReadContentAsString();
							break;
						case "IsHygiene":
							procedurecode.IsHygiene=reader.ReadContentAsString()!="0";
							break;
						case "GTypeNum":
							procedurecode.GTypeNum=reader.ReadContentAsInt();
							break;
						case "AlternateCode1":
							procedurecode.AlternateCode1=reader.ReadContentAsString();
							break;
						case "MedicalCode":
							procedurecode.MedicalCode=reader.ReadContentAsString();
							break;
						case "IsTaxed":
							procedurecode.IsTaxed=reader.ReadContentAsString()!="0";
							break;
						case "PaintType":
							procedurecode.PaintType=(OpenDentBusiness.ToothPaintingType)reader.ReadContentAsInt();
							break;
						case "GraphicColor":
							procedurecode.GraphicColor=Color.FromArgb(reader.ReadContentAsInt());
							break;
						case "LaymanTerm":
							procedurecode.LaymanTerm=reader.ReadContentAsString();
							break;
						case "IsCanadianLab":
							procedurecode.IsCanadianLab=reader.ReadContentAsString()!="0";
							break;
						case "PreExisting":
							procedurecode.PreExisting=reader.ReadContentAsString()!="0";
							break;
						case "BaseUnits":
							procedurecode.BaseUnits=reader.ReadContentAsInt();
							break;
						case "SubstitutionCode":
							procedurecode.SubstitutionCode=reader.ReadContentAsString();
							break;
						case "SubstOnlyIf":
							procedurecode.SubstOnlyIf=(OpenDentBusiness.SubstitutionCondition)reader.ReadContentAsInt();
							break;
						case "DateTStamp":
							procedurecode.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "IsMultiVisit":
							procedurecode.IsMultiVisit=reader.ReadContentAsString()!="0";
							break;
						case "DrugNDC":
							procedurecode.DrugNDC=reader.ReadContentAsString();
							break;
						case "RevenueCodeDefault":
							procedurecode.RevenueCodeDefault=reader.ReadContentAsString();
							break;
						case "ProvNumDefault":
							procedurecode.ProvNumDefault=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return procedurecode;
		}


	}
}