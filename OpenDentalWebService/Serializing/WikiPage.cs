using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class WikiPage {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.WikiPage wikipage) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<WikiPage>");
			sb.Append("<WikiPageNum>").Append(wikipage.WikiPageNum).Append("</WikiPageNum>");
			sb.Append("<UserNum>").Append(wikipage.UserNum).Append("</UserNum>");
			sb.Append("<PageTitle>").Append(SerializeStringEscapes.EscapeForXml(wikipage.PageTitle)).Append("</PageTitle>");
			sb.Append("<PageContent>").Append(SerializeStringEscapes.EscapeForXml(wikipage.PageContent)).Append("</PageContent>");
			sb.Append("<DateTimeSaved>").Append(wikipage.DateTimeSaved.ToString("yyyyMMddHHmmss")).Append("</DateTimeSaved>");
			sb.Append("</WikiPage>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.WikiPage Deserialize(string xml) {
			OpenDentBusiness.WikiPage wikipage=new OpenDentBusiness.WikiPage();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "WikiPageNum":
							wikipage.WikiPageNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "UserNum":
							wikipage.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PageTitle":
							wikipage.PageTitle=reader.ReadContentAsString();
							break;
						case "PageContent":
							wikipage.PageContent=reader.ReadContentAsString();
							break;
						case "DateTimeSaved":
							wikipage.DateTimeSaved=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return wikipage;
		}


	}
}