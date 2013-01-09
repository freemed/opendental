using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class WikiPageHist {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.WikiPageHist wikipagehist) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<WikiPageHist>");
			sb.Append("<WikiPageNum>").Append(wikipagehist.WikiPageNum).Append("</WikiPageNum>");
			sb.Append("<UserNum>").Append(wikipagehist.UserNum).Append("</UserNum>");
			sb.Append("<PageTitle>").Append(SerializeStringEscapes.EscapeForXml(wikipagehist.PageTitle)).Append("</PageTitle>");
			sb.Append("<PageContent>").Append(SerializeStringEscapes.EscapeForXml(wikipagehist.PageContent)).Append("</PageContent>");
			sb.Append("<DateTimeSaved>").Append(wikipagehist.DateTimeSaved.ToString("yyyyMMddHHmmss")).Append("</DateTimeSaved>");
			sb.Append("<IsDeleted>").Append((wikipagehist.IsDeleted)?1:0).Append("</IsDeleted>");
			sb.Append("</WikiPageHist>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.WikiPageHist Deserialize(string xml) {
			OpenDentBusiness.WikiPageHist wikipagehist=new OpenDentBusiness.WikiPageHist();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "WikiPageNum":
							wikipagehist.WikiPageNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "UserNum":
							wikipagehist.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PageTitle":
							wikipagehist.PageTitle=reader.ReadContentAsString();
							break;
						case "PageContent":
							wikipagehist.PageContent=reader.ReadContentAsString();
							break;
						case "DateTimeSaved":
							wikipagehist.DateTimeSaved=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "IsDeleted":
							wikipagehist.IsDeleted=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return wikipagehist;
		}


	}
}