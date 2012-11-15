using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Etrans {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Etrans etrans) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Etrans>");
			sb.Append("<EtransNum>").Append(etrans.EtransNum).Append("</EtransNum>");
			sb.Append("<DateTimeTrans>").Append(etrans.DateTimeTrans.ToString("yyyyMMddHHmmss")).Append("</DateTimeTrans>");
			sb.Append("<ClearingHouseNum>").Append(etrans.ClearingHouseNum).Append("</ClearingHouseNum>");
			sb.Append("<Etype>").Append((int)etrans.Etype).Append("</Etype>");
			sb.Append("<ClaimNum>").Append(etrans.ClaimNum).Append("</ClaimNum>");
			sb.Append("<OfficeSequenceNumber>").Append(etrans.OfficeSequenceNumber).Append("</OfficeSequenceNumber>");
			sb.Append("<CarrierTransCounter>").Append(etrans.CarrierTransCounter).Append("</CarrierTransCounter>");
			sb.Append("<CarrierTransCounter2>").Append(etrans.CarrierTransCounter2).Append("</CarrierTransCounter2>");
			sb.Append("<CarrierNum>").Append(etrans.CarrierNum).Append("</CarrierNum>");
			sb.Append("<CarrierNum2>").Append(etrans.CarrierNum2).Append("</CarrierNum2>");
			sb.Append("<PatNum>").Append(etrans.PatNum).Append("</PatNum>");
			sb.Append("<BatchNumber>").Append(etrans.BatchNumber).Append("</BatchNumber>");
			sb.Append("<AckCode>").Append(SerializeStringEscapes.EscapeForXml(etrans.AckCode)).Append("</AckCode>");
			sb.Append("<TransSetNum>").Append(etrans.TransSetNum).Append("</TransSetNum>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(etrans.Note)).Append("</Note>");
			sb.Append("<EtransMessageTextNum>").Append(etrans.EtransMessageTextNum).Append("</EtransMessageTextNum>");
			sb.Append("<AckEtransNum>").Append(etrans.AckEtransNum).Append("</AckEtransNum>");
			sb.Append("<PlanNum>").Append(etrans.PlanNum).Append("</PlanNum>");
			sb.Append("<InsSubNum>").Append(etrans.InsSubNum).Append("</InsSubNum>");
			sb.Append("</Etrans>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Etrans Deserialize(string xml) {
			OpenDentBusiness.Etrans etrans=new OpenDentBusiness.Etrans();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EtransNum":
							etrans.EtransNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTimeTrans":
							etrans.DateTimeTrans=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ClearingHouseNum":
							etrans.ClearingHouseNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Etype":
							etrans.Etype=(OpenDentBusiness.EtransType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ClaimNum":
							etrans.ClaimNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "OfficeSequenceNumber":
							etrans.OfficeSequenceNumber=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CarrierTransCounter":
							etrans.CarrierTransCounter=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CarrierTransCounter2":
							etrans.CarrierTransCounter2=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CarrierNum":
							etrans.CarrierNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CarrierNum2":
							etrans.CarrierNum2=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							etrans.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "BatchNumber":
							etrans.BatchNumber=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "AckCode":
							etrans.AckCode=reader.ReadContentAsString();
							break;
						case "TransSetNum":
							etrans.TransSetNum=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Note":
							etrans.Note=reader.ReadContentAsString();
							break;
						case "EtransMessageTextNum":
							etrans.EtransMessageTextNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AckEtransNum":
							etrans.AckEtransNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PlanNum":
							etrans.PlanNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "InsSubNum":
							etrans.InsSubNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return etrans;
		}


	}
}