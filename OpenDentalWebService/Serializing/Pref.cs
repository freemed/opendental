using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Pref {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Pref pref) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Pref>");
			sb.Append("<PrefNum>").Append(pref.PrefNum).Append("</PrefNum>");
			sb.Append("<PrefName>").Append(SerializeStringEscapes.EscapeForXml(pref.PrefName)).Append("</PrefName>");
			sb.Append("<ValueString>").Append(SerializeStringEscapes.EscapeForXml(pref.ValueString)).Append("</ValueString>");
			sb.Append("<Comments>").Append(SerializeStringEscapes.EscapeForXml(pref.Comments)).Append("</Comments>");
			sb.Append("</Pref>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Pref Deserialize(string xml) {
			OpenDentBusiness.Pref pref=new OpenDentBusiness.Pref();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PrefNum":
							pref.PrefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PrefName":
							pref.PrefName=reader.ReadContentAsString();
							break;
						case "ValueString":
							pref.ValueString=reader.ReadContentAsString();
							break;
						case "Comments":
							pref.Comments=reader.ReadContentAsString();
							break;
					}
				}
			}
			return pref;
		}


	}
}