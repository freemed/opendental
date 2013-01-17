package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Documentod {
		/** Primary key. */
		public int DocNum;
		/** Description of the document. */
		public String Description;
		/** Date created. */
		public Date DateCreated;
		/** FK to definition.DefNum. Categories for documents. */
		public int DocCategory;
		/** FK to patient.PatNum.  The document will be located in the patient folder of this patient. */
		public int PatNum;
		/** The name of the file. Does not include any directory info. */
		public String FileName;
		/** Enum:ImageType eg. document, radiograph, photo, file */
		public ImageType ImgType;
		/** True if flipped horizontally. A vertical flip would be stored as a horizontal flip plus a 180 rotation. */
		public boolean IsFlipped;
		/** Only allowed 0,90,180, and 270. */
		public int DegreesRotated;
		/** Incomplete.  An optional list of tooth numbers separated by commas.  The tooth numbers will be in American format and must be processed for display.  When displayed, dashes will be used for sequences of 3 or more tooth numbers. */
		public String ToothNumbers;
		/** . */
		public String Note;
		/** True if the signature is in Topaz format rather than OD format. */
		public boolean SigIsTopaz;
		/** The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence of the original image. */
		public String Signature;
		/** Crop rectangle X in original image pixel coordinates.  May be negative. */
		public int CropX;
		/** Crop rectangle Y in original image pixel coordinates.  May be negative. */
		public int CropY;
		/** Crop rectangle Width in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image width. */
		public int CropW;
		/** Crop rectangle Height in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image height. */
		public int CropH;
		/** The lower value of the "windowing" (contrast/brightness) for radiographs.  Default is 0.  Max is 255. */
		public int WindowingMin;
		/** The upper value of the "windowing" (contrast/brightness) for radiographs.  Default is 0(no windowing).  Max is 255. */
		public int WindowingMax;
		/** FK to mountitem.MountItemNum. If set, then this image will only show on a mount, not in the main tree. If set to 0, then no mount item is associated with this document. */
		public int MountItemNum;
		/** Date/time last altered. */
		public Date DateTStamp;
		/** The raw file data encoded as base64.  Only used if there is no AtoZ folder. */
		public String RawBase64;
		/** Thumbnail encoded as base64.  Only present if not using AtoZ folder. 100x100 pixels, jpg, takes around 5.5k. */
		public String Thumbnail;

		/** Deep copy of object. */
		public Documentod deepCopy() {
			Documentod documentod=new Documentod();
			documentod.DocNum=this.DocNum;
			documentod.Description=this.Description;
			documentod.DateCreated=this.DateCreated;
			documentod.DocCategory=this.DocCategory;
			documentod.PatNum=this.PatNum;
			documentod.FileName=this.FileName;
			documentod.ImgType=this.ImgType;
			documentod.IsFlipped=this.IsFlipped;
			documentod.DegreesRotated=this.DegreesRotated;
			documentod.ToothNumbers=this.ToothNumbers;
			documentod.Note=this.Note;
			documentod.SigIsTopaz=this.SigIsTopaz;
			documentod.Signature=this.Signature;
			documentod.CropX=this.CropX;
			documentod.CropY=this.CropY;
			documentod.CropW=this.CropW;
			documentod.CropH=this.CropH;
			documentod.WindowingMin=this.WindowingMin;
			documentod.WindowingMax=this.WindowingMax;
			documentod.MountItemNum=this.MountItemNum;
			documentod.DateTStamp=this.DateTStamp;
			documentod.RawBase64=this.RawBase64;
			documentod.Thumbnail=this.Thumbnail;
			return documentod;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Documentod>");
			sb.append("<DocNum>").append(DocNum).append("</DocNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<DateCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCreated)).append("</DateCreated>");
			sb.append("<DocCategory>").append(DocCategory).append("</DocCategory>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<FileName>").append(Serializing.escapeForXml(FileName)).append("</FileName>");
			sb.append("<ImgType>").append(ImgType.ordinal()).append("</ImgType>");
			sb.append("<IsFlipped>").append((IsFlipped)?1:0).append("</IsFlipped>");
			sb.append("<DegreesRotated>").append(DegreesRotated).append("</DegreesRotated>");
			sb.append("<ToothNumbers>").append(Serializing.escapeForXml(ToothNumbers)).append("</ToothNumbers>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<Signature>").append(Serializing.escapeForXml(Signature)).append("</Signature>");
			sb.append("<CropX>").append(CropX).append("</CropX>");
			sb.append("<CropY>").append(CropY).append("</CropY>");
			sb.append("<CropW>").append(CropW).append("</CropW>");
			sb.append("<CropH>").append(CropH).append("</CropH>");
			sb.append("<WindowingMin>").append(WindowingMin).append("</WindowingMin>");
			sb.append("<WindowingMax>").append(WindowingMax).append("</WindowingMax>");
			sb.append("<MountItemNum>").append(MountItemNum).append("</MountItemNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<RawBase64>").append(Serializing.escapeForXml(RawBase64)).append("</RawBase64>");
			sb.append("<Thumbnail>").append(Serializing.escapeForXml(Thumbnail)).append("</Thumbnail>");
			sb.append("</Documentod>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DocNum")!=null) {
					DocNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DocNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"DateCreated")!=null) {
					DateCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateCreated"));
				}
				if(Serializing.getXmlNodeValue(doc,"DocCategory")!=null) {
					DocCategory=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DocCategory"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FileName")!=null) {
					FileName=Serializing.getXmlNodeValue(doc,"FileName");
				}
				if(Serializing.getXmlNodeValue(doc,"ImgType")!=null) {
					ImgType=ImageType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ImgType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"IsFlipped")!=null) {
					IsFlipped=(Serializing.getXmlNodeValue(doc,"IsFlipped")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DegreesRotated")!=null) {
					DegreesRotated=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DegreesRotated"));
				}
				if(Serializing.getXmlNodeValue(doc,"ToothNumbers")!=null) {
					ToothNumbers=Serializing.getXmlNodeValue(doc,"ToothNumbers");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"SigIsTopaz")!=null) {
					SigIsTopaz=(Serializing.getXmlNodeValue(doc,"SigIsTopaz")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Signature")!=null) {
					Signature=Serializing.getXmlNodeValue(doc,"Signature");
				}
				if(Serializing.getXmlNodeValue(doc,"CropX")!=null) {
					CropX=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CropX"));
				}
				if(Serializing.getXmlNodeValue(doc,"CropY")!=null) {
					CropY=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CropY"));
				}
				if(Serializing.getXmlNodeValue(doc,"CropW")!=null) {
					CropW=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CropW"));
				}
				if(Serializing.getXmlNodeValue(doc,"CropH")!=null) {
					CropH=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CropH"));
				}
				if(Serializing.getXmlNodeValue(doc,"WindowingMin")!=null) {
					WindowingMin=Integer.valueOf(Serializing.getXmlNodeValue(doc,"WindowingMin"));
				}
				if(Serializing.getXmlNodeValue(doc,"WindowingMax")!=null) {
					WindowingMax=Integer.valueOf(Serializing.getXmlNodeValue(doc,"WindowingMax"));
				}
				if(Serializing.getXmlNodeValue(doc,"MountItemNum")!=null) {
					MountItemNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MountItemNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"RawBase64")!=null) {
					RawBase64=Serializing.getXmlNodeValue(doc,"RawBase64");
				}
				if(Serializing.getXmlNodeValue(doc,"Thumbnail")!=null) {
					Thumbnail=Serializing.getXmlNodeValue(doc,"Thumbnail");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Document: "+e.getMessage());
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
