package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public ToothInitial Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothInitial>");
			sb.append("<ToothInitialNum>").append(ToothInitialNum).append("</ToothInitialNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ToothNum>").append(Serializing.EscapeForXml(ToothNum)).append("</ToothNum>");
			sb.append("<InitialType>").append(InitialType.ordinal()).append("</InitialType>");
			sb.append("<Movement>").append(Movement).append("</Movement>");
			sb.append("<DrawingSegment>").append(Serializing.EscapeForXml(DrawingSegment)).append("</DrawingSegment>");
			sb.append("<ColorDraw>").append(ColorDraw).append("</ColorDraw>");
			sb.append("</ToothInitial>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ToothInitialNum")!=null) {
					ToothInitialNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToothInitialNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothNum")!=null) {
					ToothNum=Serializing.GetXmlNodeValue(doc,"ToothNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"InitialType")!=null) {
					InitialType=ToothInitialType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InitialType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Movement")!=null) {
					Movement=Float.valueOf(Serializing.GetXmlNodeValue(doc,"Movement"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DrawingSegment")!=null) {
					DrawingSegment=Serializing.GetXmlNodeValue(doc,"DrawingSegment");
				}
				if(Serializing.GetXmlNodeValue(doc,"ColorDraw")!=null) {
					ColorDraw=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ColorDraw"));
				}
			}
			catch(Exception e) {
				throw e;
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
