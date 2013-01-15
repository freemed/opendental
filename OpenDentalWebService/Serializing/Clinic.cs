using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Clinic {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Clinic clinic) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Clinic>");
			sb.Append("<ClinicNum>").Append(clinic.ClinicNum).Append("</ClinicNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(clinic.Description)).Append("</Description>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(clinic.Address)).Append("</Address>");
			sb.Append("<Address2>").Append(SerializeStringEscapes.EscapeForXml(clinic.Address2)).Append("</Address2>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(clinic.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(clinic.State)).Append("</State>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(clinic.Zip)).Append("</Zip>");
			sb.Append("<Phone>").Append(SerializeStringEscapes.EscapeForXml(clinic.Phone)).Append("</Phone>");
			sb.Append("<BankNumber>").Append(SerializeStringEscapes.EscapeForXml(clinic.BankNumber)).Append("</BankNumber>");
			sb.Append("<DefaultPlaceService>").Append((int)clinic.DefaultPlaceService).Append("</DefaultPlaceService>");
			sb.Append("<InsBillingProv>").Append(clinic.InsBillingProv).Append("</InsBillingProv>");
			sb.Append("<Fax>").Append(SerializeStringEscapes.EscapeForXml(clinic.Fax)).Append("</Fax>");
			sb.Append("<EmailAddressNum>").Append(clinic.EmailAddressNum).Append("</EmailAddressNum>");
			sb.Append("</Clinic>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Clinic Deserialize(string xml) {
			OpenDentBusiness.Clinic clinic=new OpenDentBusiness.Clinic();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClinicNum":
							clinic.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							clinic.Description=reader.ReadContentAsString();
							break;
						case "Address":
							clinic.Address=reader.ReadContentAsString();
							break;
						case "Address2":
							clinic.Address2=reader.ReadContentAsString();
							break;
						case "City":
							clinic.City=reader.ReadContentAsString();
							break;
						case "State":
							clinic.State=reader.ReadContentAsString();
							break;
						case "Zip":
							clinic.Zip=reader.ReadContentAsString();
							break;
						case "Phone":
							clinic.Phone=reader.ReadContentAsString();
							break;
						case "BankNumber":
							clinic.BankNumber=reader.ReadContentAsString();
							break;
						case "DefaultPlaceService":
							clinic.DefaultPlaceService=(OpenDentBusiness.PlaceOfService)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "InsBillingProv":
							clinic.InsBillingProv=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Fax":
							clinic.Fax=reader.ReadContentAsString();
							break;
						case "EmailAddressNum":
							clinic.EmailAddressNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return clinic;
		}


	}
}