package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class DiseaseDef {
		/** Primary key. */
		public int DiseaseDefNum;
		/** . */
		public String DiseaseName;
		/** 0-based.  The order that the diseases will show in various lists. */
		public int ItemOrder;
		/** If hidden, the disease will still show on any patient that it was previously attached to, but it will not be available for future patients. */
		public boolean IsHidden;
		/** The last date and time this row was altered.  Not user editable. */
		public Date DateTStamp;

		/** Deep copy of object. */
		public DiseaseDef Copy() {
			DiseaseDef diseasedef=new DiseaseDef();
			diseasedef.DiseaseDefNum=this.DiseaseDefNum;
			diseasedef.DiseaseName=this.DiseaseName;
			diseasedef.ItemOrder=this.ItemOrder;
			diseasedef.IsHidden=this.IsHidden;
			diseasedef.DateTStamp=this.DateTStamp;
			return diseasedef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DiseaseDef>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<DiseaseName>").append(Serializing.EscapeForXml(DiseaseName)).append("</DiseaseName>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("</DiseaseDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"DiseaseDefNum")!=null) {
					DiseaseDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DiseaseDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DiseaseName")!=null) {
					DiseaseName=Serializing.GetXmlNodeValue(doc,"DiseaseName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
