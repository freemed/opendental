package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ClaimFormItem {
		/** Primary key. */
		public int ClaimFormItemNum;
		/** FK to claimform.ClaimFormNum */
		public int ClaimFormNum;
		/** If this item is an image.  Usually only one per claimform.  eg ADA2002.emf.  Otherwise it MUST be left blank, or it will trigger an error that the image cannot be found. */
		public String ImageFileName;
		/** Must be one of the hardcoded available fieldnames for claims. */
		public String FieldName;
		/** For dates, the format string. ie MM/dd/yyyy or M d y among many other possibilities. */
		public String FormatString;
		/** The x position of the item on the claim form.  In pixels. 100 pixels per inch. */
		public float XPos;
		/** The y position of the item. */
		public float YPos;
		/** Limits the printable area of the item. Set to zero to not limit. */
		public float Width;
		/** Limits the printable area of the item. Set to zero to not limit. */
		public float Height;

		/** Deep copy of object. */
		public ClaimFormItem Copy() {
			ClaimFormItem claimformitem=new ClaimFormItem();
			claimformitem.ClaimFormItemNum=this.ClaimFormItemNum;
			claimformitem.ClaimFormNum=this.ClaimFormNum;
			claimformitem.ImageFileName=this.ImageFileName;
			claimformitem.FieldName=this.FieldName;
			claimformitem.FormatString=this.FormatString;
			claimformitem.XPos=this.XPos;
			claimformitem.YPos=this.YPos;
			claimformitem.Width=this.Width;
			claimformitem.Height=this.Height;
			return claimformitem;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimFormItem>");
			sb.append("<ClaimFormItemNum>").append(ClaimFormItemNum).append("</ClaimFormItemNum>");
			sb.append("<ClaimFormNum>").append(ClaimFormNum).append("</ClaimFormNum>");
			sb.append("<ImageFileName>").append(Serializing.EscapeForXml(ImageFileName)).append("</ImageFileName>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FormatString>").append(Serializing.EscapeForXml(FormatString)).append("</FormatString>");
			sb.append("<XPos>").append(XPos).append("</XPos>");
			sb.append("<YPos>").append(YPos).append("</YPos>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("</ClaimFormItem>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClaimFormItemNum=Integer.valueOf(doc.getElementsByTagName("ClaimFormItemNum").item(0).getFirstChild().getNodeValue());
				ClaimFormNum=Integer.valueOf(doc.getElementsByTagName("ClaimFormNum").item(0).getFirstChild().getNodeValue());
				ImageFileName=doc.getElementsByTagName("ImageFileName").item(0).getFirstChild().getNodeValue();
				FieldName=doc.getElementsByTagName("FieldName").item(0).getFirstChild().getNodeValue();
				FormatString=doc.getElementsByTagName("FormatString").item(0).getFirstChild().getNodeValue();
				XPos=Float.valueOf(doc.getElementsByTagName("XPos").item(0).getFirstChild().getNodeValue());
				YPos=Float.valueOf(doc.getElementsByTagName("YPos").item(0).getFirstChild().getNodeValue());
				Width=Float.valueOf(doc.getElementsByTagName("Width").item(0).getFirstChild().getNodeValue());
				Height=Float.valueOf(doc.getElementsByTagName("Height").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
