package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public PerioMeasure deepCopy() {
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
		public String serialize() {
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PerioMeasureNum")!=null) {
					PerioMeasureNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PerioMeasureNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PerioExamNum")!=null) {
					PerioExamNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PerioExamNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SequenceType")!=null) {
					SequenceType=PerioSequenceType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SequenceType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"IntTooth")!=null) {
					IntTooth=Integer.valueOf(Serializing.getXmlNodeValue(doc,"IntTooth"));
				}
				if(Serializing.getXmlNodeValue(doc,"ToothValue")!=null) {
					ToothValue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ToothValue"));
				}
				if(Serializing.getXmlNodeValue(doc,"MBvalue")!=null) {
					MBvalue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MBvalue"));
				}
				if(Serializing.getXmlNodeValue(doc,"Bvalue")!=null) {
					Bvalue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Bvalue"));
				}
				if(Serializing.getXmlNodeValue(doc,"DBvalue")!=null) {
					DBvalue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DBvalue"));
				}
				if(Serializing.getXmlNodeValue(doc,"MLvalue")!=null) {
					MLvalue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MLvalue"));
				}
				if(Serializing.getXmlNodeValue(doc,"Lvalue")!=null) {
					Lvalue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Lvalue"));
				}
				if(Serializing.getXmlNodeValue(doc,"DLvalue")!=null) {
					DLvalue=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DLvalue"));
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
