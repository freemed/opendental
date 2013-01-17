package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ScreenGroup {
		/** Primary key */
		public int ScreenGroupNum;
		/** Up to the user. */
		public String Description;
		/** Date used to help order the groups. */
		public Date SGDate;
		/** Not a database column. Used if ProvNum=0. */
		public String ProvName;
		/** Not a database column. Foreign key to provider.ProvNum. Can be 0 if not a standard provider.  In that case, a ProvName should be entered. */
		public int ProvNum;
		/** Not a database column. See the PlaceOfService enum. */
		public PlaceOfService PlaceService;
		/** Not a database column. Foreign key to county.CountyName, although it will not crash if key absent. */
		public String County;
		/** Not a database column. Foreign key to school.SchoolName, although it will not crash if key absent. */
		public String GradeSchool;

		/** Deep copy of object. */
		public ScreenGroup deepCopy() {
			ScreenGroup screengroup=new ScreenGroup();
			screengroup.ScreenGroupNum=this.ScreenGroupNum;
			screengroup.Description=this.Description;
			screengroup.SGDate=this.SGDate;
			screengroup.ProvName=this.ProvName;
			screengroup.ProvNum=this.ProvNum;
			screengroup.PlaceService=this.PlaceService;
			screengroup.County=this.County;
			screengroup.GradeSchool=this.GradeSchool;
			return screengroup;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ScreenGroup>");
			sb.append("<ScreenGroupNum>").append(ScreenGroupNum).append("</ScreenGroupNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<SGDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(SGDate)).append("</SGDate>");
			sb.append("<ProvName>").append(Serializing.escapeForXml(ProvName)).append("</ProvName>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<PlaceService>").append(PlaceService.ordinal()).append("</PlaceService>");
			sb.append("<County>").append(Serializing.escapeForXml(County)).append("</County>");
			sb.append("<GradeSchool>").append(Serializing.escapeForXml(GradeSchool)).append("</GradeSchool>");
			sb.append("</ScreenGroup>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ScreenGroupNum")!=null) {
					ScreenGroupNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ScreenGroupNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"SGDate")!=null) {
					SGDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"SGDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvName")!=null) {
					ProvName=Serializing.getXmlNodeValue(doc,"ProvName");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlaceService")!=null) {
					PlaceService=PlaceOfService.valueOf(Serializing.getXmlNodeValue(doc,"PlaceService"));
				}
				if(Serializing.getXmlNodeValue(doc,"County")!=null) {
					County=Serializing.getXmlNodeValue(doc,"County");
				}
				if(Serializing.getXmlNodeValue(doc,"GradeSchool")!=null) {
					GradeSchool=Serializing.getXmlNodeValue(doc,"GradeSchool");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ScreenGroup: "+e.getMessage());
			}
		}

		/**  */
		public enum PlaceOfService {
			/** 0. CPT code 11 */
			Office,
			/** 1. CPT code 12 */
			PatientsHome,
			/** 2. CPT code 21 */
			InpatHospital,
			/** 3. CPT code 22 */
			OutpatHospital,
			/** 4. CPT code 31 */
			SkilledNursFac,
			/** 5. CPT code 33.  In X12, a similar code AdultLivCareFac 35 is mentioned. */
			CustodialCareFacility,
			/** 6. CPT code ?.  We use 11 for office. */
			OtherLocation,
			/** 7. CPT code 15 */
			MobileUnit,
			/** 8. CPT code 03 */
			School,
			/** 9. CPT code 26 */
			MilitaryTreatFac,
			/** 10. CPT code 50 */
			FederalHealthCenter,
			/** 11. CPT code 71 */
			PublicHealthClinic,
			/** 12. CPT code 72 */
			RuralHealthClinic,
			/** 13. CPT code 23 */
			EmergencyRoomHospital,
			/** 14. CPT code 24 */
			AmbulatorySurgicalCenter
		}


}
