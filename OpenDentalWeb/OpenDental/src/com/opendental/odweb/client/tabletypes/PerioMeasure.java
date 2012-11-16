package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PerioMeasure {
		/** Primary key. */
		public int PerioMeasureNum;
		/** FK to perioexam.PerioExamNum. */
		public int PerioExamNum;
		/** Enum:PerioSequenceType  eg probing, mobility, recession, etc. */
		public PerioSequenceType SequenceType;
		/** Valid values are 1-32. Every measurement must be associated with a tooth. */
		public int IntTooth;
		/** This is used when the measurement does not apply to a surface(mobility and skiptooth).  Valid values for all surfaces are 0 through 19, or -1 to represent no measurement taken. */
		public int ToothValue;
		/** -1 represents no measurement. Values of 100+ represent negative values (only used for Gingival Margins). e.g. To use a value of 105, subtract it from 100. (100 - 105 = -5) */
		public int MBvalue;
		/** . */
		public int Bvalue;
		/** . */
		public int DBvalue;
		/** . */
		public int MLvalue;
		/** . */
		public int Lvalue;
		/** . */
		public int DLvalue;

		/** Deep copy of object. */
		public PerioMeasure Copy() {
			PerioMeasure periomeasure=new PerioMeasure();
			periomeasure.PerioMeasureNum=this.PerioMeasureNum;
			periomeasure.PerioExamNum=this.PerioExamNum;
			periomeasure.SequenceType=this.SequenceType;
			periomeasure.IntTooth=this.IntTooth;
			periomeasure.ToothValue=this.ToothValue;
			periomeasure.MBvalue=this.MBvalue;
			periomeasure.Bvalue=this.Bvalue;
			periomeasure.DBvalue=this.DBvalue;
			periomeasure.MLvalue=this.MLvalue;
			periomeasure.Lvalue=this.Lvalue;
			periomeasure.DLvalue=this.DLvalue;
			return periomeasure;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PerioMeasure>");
			sb.append("<PerioMeasureNum>").append(PerioMeasureNum).append("</PerioMeasureNum>");
			sb.append("<PerioExamNum>").append(PerioExamNum).append("</PerioExamNum>");
			sb.append("<SequenceType>").append(SequenceType.ordinal()).append("</SequenceType>");
			sb.append("<IntTooth>").append(IntTooth).append("</IntTooth>");
			sb.append("<ToothValue>").append(ToothValue).append("</ToothValue>");
			sb.append("<MBvalue>").append(MBvalue).append("</MBvalue>");
			sb.append("<Bvalue>").append(Bvalue).append("</Bvalue>");
			sb.append("<DBvalue>").append(DBvalue).append("</DBvalue>");
			sb.append("<MLvalue>").append(MLvalue).append("</MLvalue>");
			sb.append("<Lvalue>").append(Lvalue).append("</Lvalue>");
			sb.append("<DLvalue>").append(DLvalue).append("</DLvalue>");
			sb.append("</PerioMeasure>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"PerioMeasureNum")!=null) {
					PerioMeasureNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PerioMeasureNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PerioExamNum")!=null) {
					PerioExamNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PerioExamNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SequenceType")!=null) {
					SequenceType=PerioSequenceType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SequenceType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"IntTooth")!=null) {
					IntTooth=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"IntTooth"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothValue")!=null) {
					ToothValue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToothValue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MBvalue")!=null) {
					MBvalue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MBvalue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Bvalue")!=null) {
					Bvalue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Bvalue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DBvalue")!=null) {
					DBvalue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DBvalue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MLvalue")!=null) {
					MLvalue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MLvalue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Lvalue")!=null) {
					Lvalue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Lvalue"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DLvalue")!=null) {
					DLvalue=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DLvalue"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** In perio, the type of measurements for a given row. */
		public enum PerioSequenceType {
			/** 0 */
			Mobility,
			/** 1 */
			Furcation,
			/** 2-AKA recession. */
			GingMargin,
			/** 3-MucoGingivalJunction- the division between attached and unattached mucosa. */
			MGJ,
			/** 4 */
			Probing,
			/** 5-For the skiptooth type, set surf to none, and ToothValue to 1. */
			SkipTooth,
			/** 6. Sum of flags for bleeding(1), suppuration(2), plaque(4), and calculus(8). */
			Bleeding,
			/** 7. But this type is never saved to the db. It is always calculated on the fly. */
			CAL
		}


}
