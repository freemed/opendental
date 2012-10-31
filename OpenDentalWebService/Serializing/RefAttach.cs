using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RefAttach {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RefAttach refattach) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RefAttach>");
			sb.Append("<RefAttachNum>").Append(refattach.RefAttachNum).Append("</RefAttachNum>");
			sb.Append("<ReferralNum>").Append(refattach.ReferralNum).Append("</ReferralNum>");
			sb.Append("<PatNum>").Append(refattach.PatNum).Append("</PatNum>");
			sb.Append("<ItemOrder>").Append(refattach.ItemOrder).Append("</ItemOrder>");
			sb.Append("<RefDate>").Append(refattach.RefDate.ToLongDateString()).Append("</RefDate>");
			sb.Append("<IsFrom>").Append((refattach.IsFrom)?1:0).Append("</IsFrom>");
			sb.Append("<RefToStatus>").Append((int)refattach.RefToStatus).Append("</RefToStatus>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(refattach.Note)).Append("</Note>");
			sb.Append("<IsTransitionOfCare>").Append((refattach.IsTransitionOfCare)?1:0).Append("</IsTransitionOfCare>");
			sb.Append("<ProcNum>").Append(refattach.ProcNum).Append("</ProcNum>");
			sb.Append("<DateProcComplete>").Append(refattach.DateProcComplete.ToLongDateString()).Append("</DateProcComplete>");
			sb.Append("</RefAttach>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RefAttach Deserialize(string xml) {
			OpenDentBusiness.RefAttach refattach=new OpenDentBusiness.RefAttach();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RefAttachNum":
							refattach.RefAttachNum=reader.ReadContentAsLong();
							break;
						case "ReferralNum":
							refattach.ReferralNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							refattach.PatNum=reader.ReadContentAsLong();
							break;
						case "ItemOrder":
							refattach.ItemOrder=reader.ReadContentAsInt();
							break;
						case "RefDate":
							refattach.RefDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "IsFrom":
							refattach.IsFrom=reader.ReadContentAsString()!="0";
							break;
						case "RefToStatus":
							refattach.RefToStatus=(OpenDentBusiness.ReferralToStatus)reader.ReadContentAsInt();
							break;
						case "Note":
							refattach.Note=reader.ReadContentAsString();
							break;
						case "IsTransitionOfCare":
							refattach.IsTransitionOfCare=reader.ReadContentAsString()!="0";
							break;
						case "ProcNum":
							refattach.ProcNum=reader.ReadContentAsLong();
							break;
						case "DateProcComplete":
							refattach.DateProcComplete=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return refattach;
		}


	}
}