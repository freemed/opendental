package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ClaimForm {
		/** Primary key. */
		public int ClaimFormNum;
		/** eg. ADA2002 or CA Medicaid */
		public String Description;
		/** If true, then it will not be displayed in various claim form lists as a choice. */
		public boolean IsHidden;
		/** Valid font name for all text on the form. */
		public String FontName;
		/** Font size for all text on the form. */
		public float FontSize;
		/** For instance OD12 or JoeDeveloper9.  If you are a developer releasing claimforms, then this should be your name or company followed by a unique number.  This will later make it easier for you to maintain your claimforms for your customers.  All claimforms that we release will be of the form OD##.  Forms that the user creates will have this field blank, protecting them from being changed by us.  So far, we have built the following claimforms: ADA2002=OD1, Denti-Cal=OD2, ADA2000=OD3, HCFA1500=OD4, HCFA1500preprinted=OD5, Canadian=OD6, Belgian=OD7, ADA2006=OD8, 1500=OD9, UB04=OD10, ADA2012=OD11 */
		public String UniqueID;
		/** Set to false to not print images.  This removes the background for printing on premade forms. */
		public boolean PrintImages;
		/** Shifts all items by x/100th's of an inch to compensate for printer, typically less than 1/4 inch. */
		public int OffsetX;
		/** Shifts all items by y/100th's of an inch to compensate for printer, typically less than 1/4 inch. */
		public int OffsetY;
		/** This is not a database column.  It is an array of all claimformItems that are attached to this ClaimForm. */
		public ClaimFormItem[] Items;

		/** Deep copy of object. */
		public ClaimForm deepCopy() {
			ClaimForm claimform=new ClaimForm();
			claimform.ClaimFormNum=this.ClaimFormNum;
			claimform.Description=this.Description;
			claimform.IsHidden=this.IsHidden;
			claimform.FontName=this.FontName;
			claimform.FontSize=this.FontSize;
			claimform.UniqueID=this.UniqueID;
			claimform.PrintImages=this.PrintImages;
			claimform.OffsetX=this.OffsetX;
			claimform.OffsetY=this.OffsetY;
			claimform.Items=this.Items;
			return claimform;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimForm>");
			sb.append("<ClaimFormNum>").append(ClaimFormNum).append("</ClaimFormNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<FontName>").append(Serializing.escapeForXml(FontName)).append("</FontName>");
			sb.append("<FontSize>").append(FontSize).append("</FontSize>");
			sb.append("<UniqueID>").append(Serializing.escapeForXml(UniqueID)).append("</UniqueID>");
			sb.append("<PrintImages>").append((PrintImages)?1:0).append("</PrintImages>");
			sb.append("<OffsetX>").append(OffsetX).append("</OffsetX>");
			sb.append("<OffsetY>").append(OffsetY).append("</OffsetY>");
			sb.append("</ClaimForm>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClaimFormNum")!=null) {
					ClaimFormNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimFormNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"FontName")!=null) {
					FontName=Serializing.getXmlNodeValue(doc,"FontName");
				}
				if(Serializing.getXmlNodeValue(doc,"FontSize")!=null) {
					FontSize=Float.valueOf(Serializing.getXmlNodeValue(doc,"FontSize"));
				}
				if(Serializing.getXmlNodeValue(doc,"UniqueID")!=null) {
					UniqueID=Serializing.getXmlNodeValue(doc,"UniqueID");
				}
				if(Serializing.getXmlNodeValue(doc,"PrintImages")!=null) {
					PrintImages=(Serializing.getXmlNodeValue(doc,"PrintImages")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"OffsetX")!=null) {
					OffsetX=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OffsetX"));
				}
				if(Serializing.getXmlNodeValue(doc,"OffsetY")!=null) {
					OffsetY=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OffsetY"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ClaimForm: "+e.getMessage());
			}
		}


}
