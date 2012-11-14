using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Statement {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Statement statement) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Statement>");
			sb.Append("<StatementNum>").Append(statement.StatementNum).Append("</StatementNum>");
			sb.Append("<PatNum>").Append(statement.PatNum).Append("</PatNum>");
			sb.Append("<DateSent>").Append(statement.DateSent.ToString()).Append("</DateSent>");
			sb.Append("<DateRangeFrom>").Append(statement.DateRangeFrom.ToString()).Append("</DateRangeFrom>");
			sb.Append("<DateRangeTo>").Append(statement.DateRangeTo.ToString()).Append("</DateRangeTo>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(statement.Note)).Append("</Note>");
			sb.Append("<NoteBold>").Append(SerializeStringEscapes.EscapeForXml(statement.NoteBold)).Append("</NoteBold>");
			sb.Append("<Mode_>").Append((int)statement.Mode_).Append("</Mode_>");
			sb.Append("<HidePayment>").Append((statement.HidePayment)?1:0).Append("</HidePayment>");
			sb.Append("<SinglePatient>").Append((statement.SinglePatient)?1:0).Append("</SinglePatient>");
			sb.Append("<Intermingled>").Append((statement.Intermingled)?1:0).Append("</Intermingled>");
			sb.Append("<IsSent>").Append((statement.IsSent)?1:0).Append("</IsSent>");
			sb.Append("<DocNum>").Append(statement.DocNum).Append("</DocNum>");
			sb.Append("<DateTStamp>").Append(statement.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("<IsReceipt>").Append((statement.IsReceipt)?1:0).Append("</IsReceipt>");
			sb.Append("<IsInvoice>").Append((statement.IsInvoice)?1:0).Append("</IsInvoice>");
			sb.Append("<IsInvoiceCopy>").Append((statement.IsInvoiceCopy)?1:0).Append("</IsInvoiceCopy>");
			sb.Append("</Statement>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Statement Deserialize(string xml) {
			OpenDentBusiness.Statement statement=new OpenDentBusiness.Statement();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "StatementNum":
							statement.StatementNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							statement.PatNum=reader.ReadContentAsLong();
							break;
						case "DateSent":
							statement.DateSent=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateRangeFrom":
							statement.DateRangeFrom=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateRangeTo":
							statement.DateRangeTo=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Note":
							statement.Note=reader.ReadContentAsString();
							break;
						case "NoteBold":
							statement.NoteBold=reader.ReadContentAsString();
							break;
						case "Mode_":
							statement.Mode_=(OpenDentBusiness.StatementMode)reader.ReadContentAsInt();
							break;
						case "HidePayment":
							statement.HidePayment=reader.ReadContentAsString()!="0";
							break;
						case "SinglePatient":
							statement.SinglePatient=reader.ReadContentAsString()!="0";
							break;
						case "Intermingled":
							statement.Intermingled=reader.ReadContentAsString()!="0";
							break;
						case "IsSent":
							statement.IsSent=reader.ReadContentAsString()!="0";
							break;
						case "DocNum":
							statement.DocNum=reader.ReadContentAsLong();
							break;
						case "DateTStamp":
							statement.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "IsReceipt":
							statement.IsReceipt=reader.ReadContentAsString()!="0";
							break;
						case "IsInvoice":
							statement.IsInvoice=reader.ReadContentAsString()!="0";
							break;
						case "IsInvoiceCopy":
							statement.IsInvoiceCopy=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return statement;
		}


	}
}