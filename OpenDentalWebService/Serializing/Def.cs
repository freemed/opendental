using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Def {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Def def) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Def>");
			sb.Append("<DefNum>").Append(def.DefNum).Append("</DefNum>");
			sb.Append("<Category>").Append((int)def.Category).Append("</Category>");
			sb.Append("<ItemOrder>").Append(def.ItemOrder).Append("</ItemOrder>");
			sb.Append("<ItemName>").Append(SerializeStringEscapes.EscapeForXml(def.ItemName)).Append("</ItemName>");
			sb.Append("<ItemValue>").Append(SerializeStringEscapes.EscapeForXml(def.ItemValue)).Append("</ItemValue>");
			sb.Append("<ItemColor>").Append(def.ItemColor.ToArgb()).Append("</ItemColor>");
			sb.Append("<IsHidden>").Append((def.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("</Def>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Def Deserialize(string xml) {
			OpenDentBusiness.Def def=new OpenDentBusiness.Def();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DefNum":
							def.DefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Category":
							def.Category=(OpenDentBusiness.DefCat)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							def.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ItemName":
							def.ItemName=reader.ReadContentAsString();
							break;
						case "ItemValue":
							def.ItemValue=reader.ReadContentAsString();
							break;
						case "ItemColor":
							def.ItemColor=Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
						case "IsHidden":
							def.IsHidden=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return def;
		}


	}
}