using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RxPat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RxPat rxpat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RxPat>");
			sb.Append("<RxNum>").Append(rxpat.RxNum).Append("</RxNum>");
			sb.Append("<PatNum>").Append(rxpat.PatNum).Append("</PatNum>");
			sb.Append("<RxDate>").Append(rxpat.RxDate.ToString("yyyyMMddHHmmss")).Append("</RxDate>");
			sb.Append("<Drug>").Append(SerializeStringEscapes.EscapeForXml(rxpat.Drug)).Append("</Drug>");
			sb.Append("<Sig>").Append(SerializeStringEscapes.EscapeForXml(rxpat.Sig)).Append("</Sig>");
			sb.Append("<Disp>").Append(SerializeStringEscapes.EscapeForXml(rxpat.Disp)).Append("</Disp>");
			sb.Append("<Refills>").Append(SerializeStringEscapes.EscapeForXml(rxpat.Refills)).Append("</Refills>");
			sb.Append("<ProvNum>").Append(rxpat.ProvNum).Append("</ProvNum>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(rxpat.Notes)).Append("</Notes>");
			sb.Append("<PharmacyNum>").Append(rxpat.PharmacyNum).Append("</PharmacyNum>");
			sb.Append("<IsControlled>").Append((rxpat.IsControlled)?1:0).Append("</IsControlled>");
			sb.Append("<DateTStamp>").Append(rxpat.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<SendStatus>").Append((int)rxpat.SendStatus).Append("</SendStatus>");
			sb.Append("<RxCui>").Append(rxpat.RxCui).Append("</RxCui>");
			sb.Append("<DosageCode>").Append(SerializeStringEscapes.EscapeForXml(rxpat.DosageCode)).Append("</DosageCode>");
			sb.Append("<NewCropGuid>").Append(SerializeStringEscapes.EscapeForXml(rxpat.NewCropGuid)).Append("</NewCropGuid>");
			sb.Append("</RxPat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RxPat Deserialize(string xml) {
			OpenDentBusiness.RxPat rxpat=new OpenDentBusiness.RxPat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RxNum":
							rxpat.RxNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							rxpat.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "RxDate":
							rxpat.RxDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Drug":
							rxpat.Drug=reader.ReadContentAsString();
							break;
						case "Sig":
							rxpat.Sig=reader.ReadContentAsString();
							break;
						case "Disp":
							rxpat.Disp=reader.ReadContentAsString();
							break;
						case "Refills":
							rxpat.Refills=reader.ReadContentAsString();
							break;
						case "ProvNum":
							rxpat.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Notes":
							rxpat.Notes=reader.ReadContentAsString();
							break;
						case "PharmacyNum":
							rxpat.PharmacyNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "IsControlled":
							rxpat.IsControlled=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							rxpat.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "SendStatus":
							rxpat.SendStatus=(OpenDentBusiness.RxSendStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "RxCui":
							rxpat.RxCui=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DosageCode":
							rxpat.DosageCode=reader.ReadContentAsString();
							break;
						case "NewCropGuid":
							rxpat.NewCropGuid=reader.ReadContentAsString();
							break;
					}
				}
			}
			return rxpat;
		}


	}
}