using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ToothInitial {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ToothInitial toothinitial) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ToothInitial>");
			sb.Append("<ToothInitialNum>").Append(toothinitial.ToothInitialNum).Append("</ToothInitialNum>");
			sb.Append("<PatNum>").Append(toothinitial.PatNum).Append("</PatNum>");
			sb.Append("<ToothNum>").Append(SerializeStringEscapes.EscapeForXml(toothinitial.ToothNum)).Append("</ToothNum>");
			sb.Append("<InitialType>").Append((int)toothinitial.InitialType).Append("</InitialType>");
			sb.Append("<Movement>").Append(toothinitial.Movement).Append("</Movement>");
			sb.Append("<DrawingSegment>").Append(SerializeStringEscapes.EscapeForXml(toothinitial.DrawingSegment)).Append("</DrawingSegment>");
			sb.Append("<ColorDraw>").Append(toothinitial.ColorDraw.ToArgb()).Append("</ColorDraw>");
			sb.Append("</ToothInitial>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ToothInitial Deserialize(string xml) {
			OpenDentBusiness.ToothInitial toothinitial=new OpenDentBusiness.ToothInitial();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ToothInitialNum":
							toothinitial.ToothInitialNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							toothinitial.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ToothNum":
							toothinitial.ToothNum=reader.ReadContentAsString();
							break;
						case "InitialType":
							toothinitial.InitialType=(OpenDentBusiness.ToothInitialType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Movement":
							toothinitial.Movement=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "DrawingSegment":
							toothinitial.DrawingSegment=reader.ReadContentAsString();
							break;
						case "ColorDraw":
							toothinitial.ColorDraw=Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
					}
				}
			}
			return toothinitial;
		}


	}
}