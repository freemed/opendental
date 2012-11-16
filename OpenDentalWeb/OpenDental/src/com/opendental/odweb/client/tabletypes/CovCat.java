package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class CovCat {
		/** Primary key.  Only used in Benefit and CovSpan tables. */
		public int CovCatNum;
		/** Description of this category. */
		public String Description;
		/** Default percent for this category. -1 to skip this category and not apply a percentage. */
		public int DefaultPercent;
		/** The order in which the categories are displayed.  Includes hidden categories. 0-based. */
		public byte CovOrder;
		/** If true, this category will be hidden. */
		public boolean IsHidden;
		/** Enum:EbenefitCategory  The X12 benefit categories.  Each CovCat can link to one X12 category.  Default is 0 (unlinked). */
		public EbenefitCategory EbenefitCat;

		/** Deep copy of object. */
		public CovCat Copy() {
			CovCat covcat=new CovCat();
			covcat.CovCatNum=this.CovCatNum;
			covcat.Description=this.Description;
			covcat.DefaultPercent=this.DefaultPercent;
			covcat.CovOrder=this.CovOrder;
			covcat.IsHidden=this.IsHidden;
			covcat.EbenefitCat=this.EbenefitCat;
			return covcat;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CovCat>");
			sb.append("<CovCatNum>").append(CovCatNum).append("</CovCatNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<DefaultPercent>").append(DefaultPercent).append("</DefaultPercent>");
			sb.append("<CovOrder>").append(CovOrder).append("</CovOrder>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<EbenefitCat>").append(EbenefitCat.ordinal()).append("</EbenefitCat>");
			sb.append("</CovCat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"CovCatNum")!=null) {
					CovCatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CovCatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"DefaultPercent")!=null) {
					DefaultPercent=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DefaultPercent"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CovOrder")!=null) {
					CovOrder=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CovOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"EbenefitCat")!=null) {
					EbenefitCat=EbenefitCategory.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EbenefitCat"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** The X12 benefit categories.  Used to link the user-defined CovCats to the corresponding X12 category. */
		public enum EbenefitCategory {
			/** 0- Default.  Applies to all codes. */
			None,
			/** 1- X12: 30 and 35. All ADA codes except ortho.  D0000-D7999 and D9000-D9999 */
			General,
			/** 2- X12: 23. ADA D0000-D0999.  This includes DiagnosticXray. */
			Diagnostic,
			/** 3- X12: 24. ADA D4000 */
			Periodontics,
			/** 4- X12: 25. ADA D2000-D2699, and D2800-D2999. */
			Restorative,
			/** 5- X12: 26. ADA D3000 */
			Endodontics,
			/** 6- X12: 27. ADA D5900-D5999 */
			MaxillofacialProsth,
			/** 7- X12: 36. Exclusive subcategory of restorative.  D2700-D2799 */
			Crowns,
			/** 8- X12: 37. ADA range? */
			Accident,
			/** 9- X12: 38. ADA D8000-D8999 */
			Orthodontics,
			/** 10- X12: 39. ADA D5000-D5899 (removable), and D6200-D6899 (fixed) */
			Prosthodontics,
			/** 11- X12: 40. ADA D7000 */
			OralSurgery,
			/** 12- X12: 41. ADA D1000 */
			RoutinePreventive,
			/** 13- X12: 4. ADA D0200-D0399.  So this is like an optional category which is otherwise considered to be diagnosic. */
			DiagnosticXRay,
			/** 14- X12: 28. ADA D9000-D9999 */
			Adjunctive
		}


}
