using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Document {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Document document) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Document>");
			sb.Append("<DocNum>").Append(document.DocNum).Append("</DocNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(document.Description)).Append("</Description>");
			sb.Append("<DateCreated>").Append(document.DateCreated.ToString("yyyyMMddHHmmss")).Append("</DateCreated>");
			sb.Append("<DocCategory>").Append(document.DocCategory).Append("</DocCategory>");
			sb.Append("<PatNum>").Append(document.PatNum).Append("</PatNum>");
			sb.Append("<FileName>").Append(SerializeStringEscapes.EscapeForXml(document.FileName)).Append("</FileName>");
			sb.Append("<ImgType>").Append((int)document.ImgType).Append("</ImgType>");
			sb.Append("<IsFlipped>").Append((document.IsFlipped)?1:0).Append("</IsFlipped>");
			sb.Append("<DegreesRotated>").Append(document.DegreesRotated).Append("</DegreesRotated>");
			sb.Append("<ToothNumbers>").Append(SerializeStringEscapes.EscapeForXml(document.ToothNumbers)).Append("</ToothNumbers>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(document.Note)).Append("</Note>");
			sb.Append("<SigIsTopaz>").Append((document.SigIsTopaz)?1:0).Append("</SigIsTopaz>");
			sb.Append("<Signature>").Append(SerializeStringEscapes.EscapeForXml(document.Signature)).Append("</Signature>");
			sb.Append("<CropX>").Append(document.CropX).Append("</CropX>");
			sb.Append("<CropY>").Append(document.CropY).Append("</CropY>");
			sb.Append("<CropW>").Append(document.CropW).Append("</CropW>");
			sb.Append("<CropH>").Append(document.CropH).Append("</CropH>");
			sb.Append("<WindowingMin>").Append(document.WindowingMin).Append("</WindowingMin>");
			sb.Append("<WindowingMax>").Append(document.WindowingMax).Append("</WindowingMax>");
			sb.Append("<MountItemNum>").Append(document.MountItemNum).Append("</MountItemNum>");
			sb.Append("<DateTStamp>").Append(document.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<RawBase64>").Append(SerializeStringEscapes.EscapeForXml(document.RawBase64)).Append("</RawBase64>");
			sb.Append("<Thumbnail>").Append(SerializeStringEscapes.EscapeForXml(document.Thumbnail)).Append("</Thumbnail>");
			sb.Append("</Document>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Document Deserialize(string xml) {
			OpenDentBusiness.Document document=new OpenDentBusiness.Document();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DocNum":
							document.DocNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							document.Description=reader.ReadContentAsString();
							break;
						case "DateCreated":
							document.DateCreated=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DocCategory":
							document.DocCategory=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							document.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FileName":
							document.FileName=reader.ReadContentAsString();
							break;
						case "ImgType":
							document.ImgType=(OpenDentBusiness.ImageType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IsFlipped":
							document.IsFlipped=reader.ReadContentAsString()!="0";
							break;
						case "DegreesRotated":
							document.DegreesRotated=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ToothNumbers":
							document.ToothNumbers=reader.ReadContentAsString();
							break;
						case "Note":
							document.Note=reader.ReadContentAsString();
							break;
						case "SigIsTopaz":
							document.SigIsTopaz=reader.ReadContentAsString()!="0";
							break;
						case "Signature":
							document.Signature=reader.ReadContentAsString();
							break;
						case "CropX":
							document.CropX=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CropY":
							document.CropY=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CropW":
							document.CropW=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CropH":
							document.CropH=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "WindowingMin":
							document.WindowingMin=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "WindowingMax":
							document.WindowingMax=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MountItemNum":
							document.MountItemNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTStamp":
							document.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "RawBase64":
							document.RawBase64=reader.ReadContentAsString();
							break;
						case "Thumbnail":
							document.Thumbnail=reader.ReadContentAsString();
							break;
					}
				}
			}
			return document;
		}


	}
}