package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class MountDef {
		/** Primary key. */
		public int MountDefNum;
		/** . */
		public String Description;
		/** The order that the mount defs will show in various lists. */
		public int ItemOrder;
		/** Set to true if this is just xrays.  If true, this prevents image from being scaled to fit inside the mount.  If false (composite photographs for example) then the images will be scaled to fit inside the mount. Later, the basic appearance or background color might be set based on this flag as well. */
		public boolean IsRadiograph;
		/** The width of the mount, in pixels.  For radiograph mounts, this could be very large.  It must be large enough for the radiographs to fit in the mount without scaling.  For photos, it should also be large so that the scaling won't be too noticeable.  Shrinking to view or print will always produce nicer results than enlarging to view or print. */
		public int Width;
		/** Height of the mount in pixels. */
		public int Height;

		/** Deep copy of object. */
		public MountDef deepCopy() {
			MountDef mountdef=new MountDef();
			mountdef.MountDefNum=this.MountDefNum;
			mountdef.Description=this.Description;
			mountdef.ItemOrder=this.ItemOrder;
			mountdef.IsRadiograph=this.IsRadiograph;
			mountdef.Width=this.Width;
			mountdef.Height=this.Height;
			return mountdef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MountDef>");
			sb.append("<MountDefNum>").append(MountDefNum).append("</MountDefNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<IsRadiograph>").append((IsRadiograph)?1:0).append("</IsRadiograph>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("</MountDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"MountDefNum")!=null) {
					MountDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MountDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsRadiograph")!=null) {
					IsRadiograph=(Serializing.getXmlNodeValue(doc,"IsRadiograph")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Width")!=null) {
					Width=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Width"));
				}
				if(Serializing.getXmlNodeValue(doc,"Height")!=null) {
					Height=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Height"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing MountDef: "+e.getMessage());
			}
		}


}
