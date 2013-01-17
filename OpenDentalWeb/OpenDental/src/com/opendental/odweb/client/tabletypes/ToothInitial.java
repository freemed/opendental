package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ToothInitial {
		/** Primary key. */
		public int ToothInitialNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** 1-32 or A-Z. Supernumeraries not supported here yet. */
		public String ToothNum;
		/** Enum:ToothInitialType */
		public ToothInitialType InitialType;
		/** Shift in mm, or rotation / tipping in degrees. */
		public float Movement;
		/** Point data for a drawing segment.  The format would look similar to this: 45,68;48,70;49,72;0,0;55,88;etc.  It's simply a sequence of points, separated by semicolons.  Only positive numbers are used.  0,0 is the upper left of the tooth chart at original size.  Stored in pixels as originally drawn.  If we ever change the tooth chart, we will have to also keep an old version as an alternate to display old drawings. */
		public String DrawingSegment;
		/** . */
		public int ColorDraw;

		/** Deep copy of object. */
		public ToothInitial deepCopy() {
			ToothInitial toothinitial=new ToothInitial();
			toothinitial.ToothInitialNum=this.ToothInitialNum;
			toothinitial.PatNum=this.PatNum;
			toothinitial.ToothNum=this.ToothNum;
			toothinitial.InitialType=this.InitialType;
			toothinitial.Movement=this.Movement;
			toothinitial.DrawingSegment=this.DrawingSegment;
			toothinitial.ColorDraw=this.ColorDraw;
			return toothinitial;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothInitial>");
			sb.append("<ToothInitialNum>").append(ToothInitialNum).append("</ToothInitialNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ToothNum>").append(Serializing.escapeForXml(ToothNum)).append("</ToothNum>");
			sb.append("<InitialType>").append(InitialType.ordinal()).append("</InitialType>");
			sb.append("<Movement>").append(Movement).append("</Movement>");
			sb.append("<DrawingSegment>").append(Serializing.escapeForXml(DrawingSegment)).append("</DrawingSegment>");
			sb.append("<ColorDraw>").append(ColorDraw).append("</ColorDraw>");
			sb.append("</ToothInitial>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ToothInitialNum")!=null) {
					ToothInitialNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ToothInitialNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ToothNum")!=null) {
					ToothNum=Serializing.getXmlNodeValue(doc,"ToothNum");
				}
				if(Serializing.getXmlNodeValue(doc,"InitialType")!=null) {
					InitialType=ToothInitialType.valueOf(Serializing.getXmlNodeValue(doc,"InitialType"));
				}
				if(Serializing.getXmlNodeValue(doc,"Movement")!=null) {
					Movement=Float.valueOf(Serializing.getXmlNodeValue(doc,"Movement"));
				}
				if(Serializing.getXmlNodeValue(doc,"DrawingSegment")!=null) {
					DrawingSegment=Serializing.getXmlNodeValue(doc,"DrawingSegment");
				}
				if(Serializing.getXmlNodeValue(doc,"ColorDraw")!=null) {
					ColorDraw=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ColorDraw"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ToothInitial: "+e.getMessage());
			}
		}

		/**  */
		public enum ToothInitialType {
			/** 0 */
			Missing,
			/** 1 - Also hides the number.  This is now also allowed for primary teeth. */
			Hidden,
			/** 2 - Only used with 1-32.  "sets" this tooth as a primary tooth.  The result is that the primary tooth shows in addition to the perm, and that the letter shows in addition to the number.  It also does a Shift0 -12 and some other handy movements.  Even if this is set to true, there can be a separate entry for a missing primary tooth; this would be almost equivalent to not even setting the tooth as primary, but would also allow user to select the letter. */
			Primary,
			/** 3 */
			ShiftM,
			/** 4 */
			ShiftO,
			/** 5 */
			ShiftB,
			/** 6 */
			Rotate,
			/** 7 */
			TipM,
			/** 8 */
			TipB,
			/** 9 One segment of a drawing. */
			Drawing
		}


}
