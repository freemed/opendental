package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Mount deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Mount>");
			sb.append("<MountNum>").append(MountNum).append("</MountNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DocCategory>").append(DocCategory).append("</DocCategory>");
			sb.append("<DateCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCreated)).append("</DateCreated>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<ImgType>").append(ImgType.ordinal()).append("</ImgType>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("</Mount>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"MountNum")!=null) {
					MountNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MountNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DocCategory")!=null) {
					DocCategory=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DocCategory"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateCreated")!=null) {
					DateCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateCreated"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"ImgType")!=null) {
					ImgType=ImageType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ImgType"))];
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
