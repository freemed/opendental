package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public MountDef Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MountDef>");
			sb.append("<MountDefNum>").append(MountDefNum).append("</MountDefNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<IsRadiograph>").append((IsRadiograph)?1:0).append("</IsRadiograph>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("</MountDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				MountDefNum=Integer.valueOf(doc.getElementsByTagName("MountDefNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				IsRadiograph=(doc.getElementsByTagName("IsRadiograph").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Width=Integer.valueOf(doc.getElementsByTagName("Width").item(0).getFirstChild().getNodeValue());
				Height=Integer.valueOf(doc.getElementsByTagName("Height").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
