using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ApptViewItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ApptViewItem apptviewitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ApptViewItem>");
			sb.Append("<ApptViewItemNum>").Append(apptviewitem.ApptViewItemNum).Append("</ApptViewItemNum>");
			sb.Append("<ApptViewNum>").Append(apptviewitem.ApptViewNum).Append("</ApptViewNum>");
			sb.Append("<OpNum>").Append(apptviewitem.OpNum).Append("</OpNum>");
			sb.Append("<ProvNum>").Append(apptviewitem.ProvNum).Append("</ProvNum>");
			sb.Append("<ElementDesc>").Append(SerializeStringEscapes.EscapeForXml(apptviewitem.ElementDesc)).Append("</ElementDesc>");
			sb.Append("<ElementOrder>").Append(apptviewitem.ElementOrder).Append("</ElementOrder>");
			sb.Append("<ElementColor>").Append(apptviewitem.ElementColor.ToArgb()).Append("</ElementColor>");
			sb.Append("<ElementAlignment>").Append((int)apptviewitem.ElementAlignment).Append("</ElementAlignment>");
			sb.Append("<ApptFieldDefNum>").Append(apptviewitem.ApptFieldDefNum).Append("</ApptFieldDefNum>");
			sb.Append("<PatFieldDefNum>").Append(apptviewitem.PatFieldDefNum).Append("</PatFieldDefNum>");
			sb.Append("</ApptViewItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ApptViewItem Deserialize(string xml) {
			OpenDentBusiness.ApptViewItem apptviewitem=new OpenDentBusiness.ApptViewItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ApptViewItemNum":
							apptviewitem.ApptViewItemNum=reader.ReadContentAsLong();
							break;
						case "ApptViewNum":
							apptviewitem.ApptViewNum=reader.ReadContentAsLong();
							break;
						case "OpNum":
							apptviewitem.OpNum=reader.ReadContentAsLong();
							break;
						case "ProvNum":
							apptviewitem.ProvNum=reader.ReadContentAsLong();
							break;
						case "ElementDesc":
							apptviewitem.ElementDesc=reader.ReadContentAsString();
							break;
						case "ElementOrder":
							apptviewitem.ElementOrder=(byte)reader.ReadContentAsInt();
							break;
						case "ElementColor":
							apptviewitem.ElementColor=Color.FromArgb(reader.ReadContentAsInt());
							break;
						case "ElementAlignment":
							apptviewitem.ElementAlignment=(OpenDentBusiness.ApptViewAlignment)reader.ReadContentAsInt();
							break;
						case "ApptFieldDefNum":
							apptviewitem.ApptFieldDefNum=reader.ReadContentAsLong();
							break;
						case "PatFieldDefNum":
							apptviewitem.PatFieldDefNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return apptviewitem;
		}


	}
}