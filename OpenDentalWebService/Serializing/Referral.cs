using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Referral {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Referral referral) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Referral>");
			sb.Append("<ReferralNum>").Append(referral.ReferralNum).Append("</ReferralNum>");
			sb.Append("<LName>").Append(SerializeStringEscapes.EscapeForXml(referral.LName)).Append("</LName>");
			sb.Append("<FName>").Append(SerializeStringEscapes.EscapeForXml(referral.FName)).Append("</FName>");
			sb.Append("<MName>").Append(SerializeStringEscapes.EscapeForXml(referral.MName)).Append("</MName>");
			sb.Append("<SSN>").Append(SerializeStringEscapes.EscapeForXml(referral.SSN)).Append("</SSN>");
			sb.Append("<UsingTIN>").Append((referral.UsingTIN)?1:0).Append("</UsingTIN>");
			sb.Append("<Specialty>").Append((int)referral.Specialty).Append("</Specialty>");
			sb.Append("<ST>").Append(SerializeStringEscapes.EscapeForXml(referral.ST)).Append("</ST>");
			sb.Append("<Telephone>").Append(SerializeStringEscapes.EscapeForXml(referral.Telephone)).Append("</Telephone>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(referral.Address)).Append("</Address>");
			sb.Append("<Address2>").Append(SerializeStringEscapes.EscapeForXml(referral.Address2)).Append("</Address2>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(referral.City)).Append("</City>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(referral.Zip)).Append("</Zip>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(referral.Note)).Append("</Note>");
			sb.Append("<Phone2>").Append(SerializeStringEscapes.EscapeForXml(referral.Phone2)).Append("</Phone2>");
			sb.Append("<IsHidden>").Append((referral.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<NotPerson>").Append((referral.NotPerson)?1:0).Append("</NotPerson>");
			sb.Append("<Title>").Append(SerializeStringEscapes.EscapeForXml(referral.Title)).Append("</Title>");
			sb.Append("<EMail>").Append(SerializeStringEscapes.EscapeForXml(referral.EMail)).Append("</EMail>");
			sb.Append("<PatNum>").Append(referral.PatNum).Append("</PatNum>");
			sb.Append("<NationalProvID>").Append(SerializeStringEscapes.EscapeForXml(referral.NationalProvID)).Append("</NationalProvID>");
			sb.Append("<Slip>").Append(referral.Slip).Append("</Slip>");
			sb.Append("<IsDoctor>").Append((referral.IsDoctor)?1:0).Append("</IsDoctor>");
			sb.Append("</Referral>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Referral Deserialize(string xml) {
			OpenDentBusiness.Referral referral=new OpenDentBusiness.Referral();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ReferralNum":
							referral.ReferralNum=reader.ReadContentAsLong();
							break;
						case "LName":
							referral.LName=reader.ReadContentAsString();
							break;
						case "FName":
							referral.FName=reader.ReadContentAsString();
							break;
						case "MName":
							referral.MName=reader.ReadContentAsString();
							break;
						case "SSN":
							referral.SSN=reader.ReadContentAsString();
							break;
						case "UsingTIN":
							referral.UsingTIN=reader.ReadContentAsString()!="0";
							break;
						case "Specialty":
							referral.Specialty=(OpenDentBusiness.DentalSpecialty)reader.ReadContentAsInt();
							break;
						case "ST":
							referral.ST=reader.ReadContentAsString();
							break;
						case "Telephone":
							referral.Telephone=reader.ReadContentAsString();
							break;
						case "Address":
							referral.Address=reader.ReadContentAsString();
							break;
						case "Address2":
							referral.Address2=reader.ReadContentAsString();
							break;
						case "City":
							referral.City=reader.ReadContentAsString();
							break;
						case "Zip":
							referral.Zip=reader.ReadContentAsString();
							break;
						case "Note":
							referral.Note=reader.ReadContentAsString();
							break;
						case "Phone2":
							referral.Phone2=reader.ReadContentAsString();
							break;
						case "IsHidden":
							referral.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "NotPerson":
							referral.NotPerson=reader.ReadContentAsString()!="0";
							break;
						case "Title":
							referral.Title=reader.ReadContentAsString();
							break;
						case "EMail":
							referral.EMail=reader.ReadContentAsString();
							break;
						case "PatNum":
							referral.PatNum=reader.ReadContentAsLong();
							break;
						case "NationalProvID":
							referral.NationalProvID=reader.ReadContentAsString();
							break;
						case "Slip":
							referral.Slip=reader.ReadContentAsLong();
							break;
						case "IsDoctor":
							referral.IsDoctor=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return referral;
		}


	}
}