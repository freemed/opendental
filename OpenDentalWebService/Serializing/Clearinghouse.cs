using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Clearinghouse {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Clearinghouse clearinghouse) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Clearinghouse>");
			sb.Append("<ClearinghouseNum>").Append(clearinghouse.ClearinghouseNum).Append("</ClearinghouseNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.Description)).Append("</Description>");
			sb.Append("<ExportPath>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ExportPath)).Append("</ExportPath>");
			sb.Append("<Payors>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.Payors)).Append("</Payors>");
			sb.Append("<Eformat>").Append((int)clearinghouse.Eformat).Append("</Eformat>");
			sb.Append("<ISA05>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA05)).Append("</ISA05>");
			sb.Append("<SenderTIN>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.SenderTIN)).Append("</SenderTIN>");
			sb.Append("<ISA07>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA07)).Append("</ISA07>");
			sb.Append("<ISA08>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA08)).Append("</ISA08>");
			sb.Append("<ISA15>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA15)).Append("</ISA15>");
			sb.Append("<Password>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.Password)).Append("</Password>");
			sb.Append("<ResponsePath>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ResponsePath)).Append("</ResponsePath>");
			sb.Append("<CommBridge>").Append((int)clearinghouse.CommBridge).Append("</CommBridge>");
			sb.Append("<ClientProgram>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ClientProgram)).Append("</ClientProgram>");
			sb.Append("<LastBatchNumber>").Append(clearinghouse.LastBatchNumber).Append("</LastBatchNumber>");
			sb.Append("<ModemPort>").Append(clearinghouse.ModemPort).Append("</ModemPort>");
			sb.Append("<LoginID>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.LoginID)).Append("</LoginID>");
			sb.Append("<SenderName>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.SenderName)).Append("</SenderName>");
			sb.Append("<SenderTelephone>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.SenderTelephone)).Append("</SenderTelephone>");
			sb.Append("<GS03>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.GS03)).Append("</GS03>");
			sb.Append("<ISA02>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA02)).Append("</ISA02>");
			sb.Append("<ISA04>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA04)).Append("</ISA04>");
			sb.Append("<ISA16>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.ISA16)).Append("</ISA16>");
			sb.Append("<SeparatorData>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.SeparatorData)).Append("</SeparatorData>");
			sb.Append("<SeparatorSegment>").Append(SerializeStringEscapes.EscapeForXml(clearinghouse.SeparatorSegment)).Append("</SeparatorSegment>");
			sb.Append("</Clearinghouse>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Clearinghouse Deserialize(string xml) {
			OpenDentBusiness.Clearinghouse clearinghouse=new OpenDentBusiness.Clearinghouse();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClearinghouseNum":
							clearinghouse.ClearinghouseNum=reader.ReadContentAsLong();
							break;
						case "Description":
							clearinghouse.Description=reader.ReadContentAsString();
							break;
						case "ExportPath":
							clearinghouse.ExportPath=reader.ReadContentAsString();
							break;
						case "Payors":
							clearinghouse.Payors=reader.ReadContentAsString();
							break;
						case "Eformat":
							clearinghouse.Eformat=(OpenDentBusiness.ElectronicClaimFormat)reader.ReadContentAsInt();
							break;
						case "ISA05":
							clearinghouse.ISA05=reader.ReadContentAsString();
							break;
						case "SenderTIN":
							clearinghouse.SenderTIN=reader.ReadContentAsString();
							break;
						case "ISA07":
							clearinghouse.ISA07=reader.ReadContentAsString();
							break;
						case "ISA08":
							clearinghouse.ISA08=reader.ReadContentAsString();
							break;
						case "ISA15":
							clearinghouse.ISA15=reader.ReadContentAsString();
							break;
						case "Password":
							clearinghouse.Password=reader.ReadContentAsString();
							break;
						case "ResponsePath":
							clearinghouse.ResponsePath=reader.ReadContentAsString();
							break;
						case "CommBridge":
							clearinghouse.CommBridge=(OpenDentBusiness.EclaimsCommBridge)reader.ReadContentAsInt();
							break;
						case "ClientProgram":
							clearinghouse.ClientProgram=reader.ReadContentAsString();
							break;
						case "LastBatchNumber":
							clearinghouse.LastBatchNumber=reader.ReadContentAsInt();
							break;
						case "ModemPort":
							clearinghouse.ModemPort=(byte)reader.ReadContentAsInt();
							break;
						case "LoginID":
							clearinghouse.LoginID=reader.ReadContentAsString();
							break;
						case "SenderName":
							clearinghouse.SenderName=reader.ReadContentAsString();
							break;
						case "SenderTelephone":
							clearinghouse.SenderTelephone=reader.ReadContentAsString();
							break;
						case "GS03":
							clearinghouse.GS03=reader.ReadContentAsString();
							break;
						case "ISA02":
							clearinghouse.ISA02=reader.ReadContentAsString();
							break;
						case "ISA04":
							clearinghouse.ISA04=reader.ReadContentAsString();
							break;
						case "ISA16":
							clearinghouse.ISA16=reader.ReadContentAsString();
							break;
						case "SeparatorData":
							clearinghouse.SeparatorData=reader.ReadContentAsString();
							break;
						case "SeparatorSegment":
							clearinghouse.SeparatorSegment=reader.ReadContentAsString();
							break;
					}
				}
			}
			return clearinghouse;
		}


	}
}