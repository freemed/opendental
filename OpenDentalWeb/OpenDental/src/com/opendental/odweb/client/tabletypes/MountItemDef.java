package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class MountItemDef {
		/** Primary key. */
		public int MountItemDefNum;
		/** FK to mountdef.MountDefNum. */
		public int MountDefNum;
		/** The x position, in pixels, of the item on the mount. */
		public int Xpos;
		/** The y position, in pixels, of the item on the mount. */
		public int Ypos;
		/** Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself. */
		public int Width;
		/** Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself. */
		public int Height;

		/** Deep copy of object. */
		public MountItemDef Copy() {
			MountItemDef mountitemdef=new MountItemDef();
			mountitemdef.MountItemDefNum=this.MountItemDefNum;
			mountitemdef.MountDefNum=this.MountDefNum;
			mountitemdef.Xpos=this.Xpos;
			mountitemdef.Ypos=this.Ypos;
			mountitemdef.Width=this.Width;
			mountitemdef.Height=this.Height;
			return mountitemdef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MountItemDef>");
			sb.append("<MountItemDefNum>").append(MountItemDefNum).append("</MountItemDefNum>");
			sb.append("<MountDefNum>").append(MountDefNum).append("</MountDefNum>");
			sb.append("<Xpos>").append(Xpos).append("</Xpos>");
			sb.append("<Ypos>").append(Ypos).append("</Ypos>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("</MountItemDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"MountItemDefNum")!=null) {
					MountItemDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MountItemDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MountDefNum")!=null) {
					MountDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MountDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Xpos")!=null) {
					Xpos=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Xpos"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Ypos")!=null) {
					Ypos=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Ypos"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Width")!=null) {
					Width=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Width"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Height")!=null) {
					Height=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Height"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
