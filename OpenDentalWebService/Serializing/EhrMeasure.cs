using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EhrMeasure {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EhrMeasure ehrmeasure) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EhrMeasure>");
			sb.Append("<EhrMeasureNum>").Append(ehrmeasure.EhrMeasureNum).Append("</EhrMeasureNum>");
			sb.Append("<MeasureType>").Append((int)ehrmeasure.MeasureType).Append("</MeasureType>");
			sb.Append("<Numerator>").Append(ehrmeasure.Numerator).Append("</Numerator>");
			sb.Append("<Denominator>").Append(ehrmeasure.Denominator).Append("</Denominator>");
			sb.Append("</EhrMeasure>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EhrMeasure Deserialize(string xml) {
			OpenDentBusiness.EhrMeasure ehrmeasure=new OpenDentBusiness.EhrMeasure();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EhrMeasureNum":
							ehrmeasure.EhrMeasureNum=reader.ReadContentAsLong();
							break;
						case "MeasureType":
							ehrmeasure.MeasureType=(OpenDentBusiness.EhrMeasureType)reader.ReadContentAsInt();
							break;
						case "Numerator":
							ehrmeasure.Numerator=reader.ReadContentAsInt();
							break;
						case "Denominator":
							ehrmeasure.Denominator=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return ehrmeasure;
		}


	}
}