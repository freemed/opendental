package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public MountItemDef deepCopy() {
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
		public String serialize() {
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"MountItemDefNum")!=null) {
					MountItemDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MountItemDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MountDefNum")!=null) {
					MountDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MountDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Xpos")!=null) {
					Xpos=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Xpos"));
				}
				if(Serializing.getXmlNodeValue(doc,"Ypos")!=null) {
					Ypos=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Ypos"));
				}
				if(Serializing.getXmlNodeValue(doc,"Width")!=null) {
					Width=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Width"));
				}
				if(Serializing.getXmlNodeValue(doc,"Height")!=null) {
					Height=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Height"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
