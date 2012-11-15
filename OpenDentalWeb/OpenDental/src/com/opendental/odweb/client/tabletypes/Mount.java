package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Mount {
		/** Primary key. */
		public int MountNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** FK to definition.DefNum. Categories for documents. */
		public int DocCategory;
		/** The date at which the mount itself was created. Has no bearing on the creation date of the images the mount houses. */
		public Date DateCreated;
		/** Used to provide a document description in the image module tree-view. */
		public String Description;
		/** To allow the user to enter specific information regarding the exam and tooth numbers, as well as points on interest in the xray images. */
		public String Note;
		/** Enum:ImageType This is so that an image can be properly associated with the mount in the image module tree-view. */
		public ImageType ImgType;
		/** The static width of the mount, in pixels. */
		public int Width;
		/** The static height of the mount, in pixels. */
		public int Height;

		/** Deep copy of object. */
		public Mount Copy() {
			Mount mount=new Mount();
			mount.MountNum=this.MountNum;
			mount.PatNum=this.PatNum;
			mount.DocCategory=this.DocCategory;
			mount.DateCreated=this.DateCreated;
			mount.Description=this.Description;
			mount.Note=this.Note;
			mount.ImgType=this.ImgType;
			mount.Width=this.Width;
			mount.Height=this.Height;
			return mount;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Mount>");
			sb.append("<MountNum>").append(MountNum).append("</MountNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DocCategory>").append(DocCategory).append("</DocCategory>");
			sb.append("<DateCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCreated)).append("</DateCreated>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ImgType>").append(ImgType.ordinal()).append("</ImgType>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("</Mount>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				MountNum=Integer.valueOf(doc.getElementsByTagName("MountNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DocCategory=Integer.valueOf(doc.getElementsByTagName("DocCategory").item(0).getFirstChild().getNodeValue());
				DateCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateCreated").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				ImgType=ImageType.values()[Integer.valueOf(doc.getElementsByTagName("ImgType").item(0).getFirstChild().getNodeValue())];
				Width=Integer.valueOf(doc.getElementsByTagName("Width").item(0).getFirstChild().getNodeValue());
				Height=Integer.valueOf(doc.getElementsByTagName("Height").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** The type of image for images module. */
		public enum ImageType {
			/** 0- Includes scanned documents and screenshots. */
			Document,
			/** 1 */
			Radiograph,
			/** 2 */
			Photo,
			/** 3- For instance a Word document or a spreadsheet. Not an image. */
			File,
			/** 4- For xray mount sets. */
			Mount
		}


}
