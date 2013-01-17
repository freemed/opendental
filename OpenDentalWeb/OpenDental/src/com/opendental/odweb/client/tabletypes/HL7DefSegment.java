package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import java.util.ArrayList;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class HL7DefSegment {
		/** Primary key. */
		public int HL7DefSegmentNum;
		/** FK to HL7DefMessage.HL7DefMessageNum */
		public int HL7DefMessageNum;
		/** Since we don't enforce or automate, it can be 1-based or 0-based.  For outgoing, this affects the message structure.  For incoming, this is just for convenience and organization in the HL7 Def windows. */
		public int ItemOrder;
		/** For example, a DFT can have multiple FT1 segments.  This turns out to be a completely useless field, since we already know which ones can repeat. */
		public boolean CanRepeat;
		/** If this is false, and an incoming message is missing this segment, then it gets logged as an error/failure.  If this is true, then it will gracefully skip a missing incoming segment.  Not used for outgoing. */
		public boolean IsOptional;
		/** Stored in db as string, but used in OD as enum SegmentNameHL7. Example: PID. */
		public SegmentNameHL7 SegmentName;
		/** . */
		public String Note;
		/**  */
		public ArrayList<HL7DefField> hl7DefFields;

		/** Deep copy of object. */
		public HL7DefSegment deepCopy() {
			HL7DefSegment hl7defsegment=new HL7DefSegment();
			hl7defsegment.HL7DefSegmentNum=this.HL7DefSegmentNum;
			hl7defsegment.HL7DefMessageNum=this.HL7DefMessageNum;
			hl7defsegment.ItemOrder=this.ItemOrder;
			hl7defsegment.CanRepeat=this.CanRepeat;
			hl7defsegment.IsOptional=this.IsOptional;
			hl7defsegment.SegmentName=this.SegmentName;
			hl7defsegment.Note=this.Note;
			hl7defsegment.hl7DefFields=this.hl7DefFields;
			return hl7defsegment;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<HL7DefSegment>");
			sb.append("<HL7DefSegmentNum>").append(HL7DefSegmentNum).append("</HL7DefSegmentNum>");
			sb.append("<HL7DefMessageNum>").append(HL7DefMessageNum).append("</HL7DefMessageNum>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<CanRepeat>").append((CanRepeat)?1:0).append("</CanRepeat>");
			sb.append("<IsOptional>").append((IsOptional)?1:0).append("</IsOptional>");
			sb.append("<SegmentName>").append(SegmentName.ordinal()).append("</SegmentName>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</HL7DefSegment>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"HL7DefSegmentNum")!=null) {
					HL7DefSegmentNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"HL7DefSegmentNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"HL7DefMessageNum")!=null) {
					HL7DefMessageNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"HL7DefMessageNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanRepeat")!=null) {
					CanRepeat=(Serializing.getXmlNodeValue(doc,"CanRepeat")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsOptional")!=null) {
					IsOptional=(Serializing.getXmlNodeValue(doc,"IsOptional")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"SegmentName")!=null) {
					SegmentName=SegmentNameHL7.valueOf(Serializing.getXmlNodeValue(doc,"SegmentName"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing HL7DefSegment: "+e.getMessage());
			}
		}

		/** The items in this enumeration can be freely rearranged without damaging the database.  But can't change spelling or remove existing item. */
		public enum SegmentNameHL7 {
			/** Db Resource Appointment Information */
			AIG,
			/** Location Resource Appointment Information */
			AIL,
			/** Personnel Resource Appointment Information */
			AIP,
			/** Diagnosis Information */
			DG1,
			/** Event Type */
			EVN,
			/** Financial Transaction Information */
			FT1,
			/** Guarantor Information */
			GT1,
			/** Insurance Information */
			IN1,
			/** Message Header */
			MSH,
			/** Observations Request */
			OBR,
			/** Observation Related to OBR */
			OBX,
			/** Common Order.  Used in outgoing vaccinations VXUs as well as incoming lab result ORUs. */
			ORC,
			/** Patient Identification */
			PID,
			/** Patient additional demographics */
			PD1,
			/** Patient Visit */
			PV1,
			/** Pharmacy Administration Segment */
			RXA,
			/** Scheduling Activity Information */
			SCH,
			/** This can happen for unsupported segments. */
			Unknown,
			/** We use for PDF Data */
			ZX1
		}


}
