package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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

		/** Deep copy of object. */
		public ClaimForm Copy() {
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
			return claimform;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimForm>");
			sb.append("<ClaimFormNum>").append(ClaimFormNum).append("</ClaimFormNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<FontName>").append(Serializing.EscapeForXml(FontName)).append("</FontName>");
			sb.append("<FontSize>").append(FontSize).append("</FontSize>");
			sb.append("<UniqueID>").append(Serializing.EscapeForXml(UniqueID)).append("</UniqueID>");
			sb.append("<PrintImages>").append((PrintImages)?1:0).append("</PrintImages>");
			sb.append("<OffsetX>").append(OffsetX).append("</OffsetX>");
			sb.append("<OffsetY>").append(OffsetY).append("</OffsetY>");
			sb.append("</ClaimForm>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClaimFormNum=Integer.valueOf(doc.getElementsByTagName("ClaimFormNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				FontName=doc.getElementsByTagName("FontName").item(0).getFirstChild().getNodeValue();
				FontSize=Float.valueOf(doc.getElementsByTagName("FontSize").item(0).getFirstChild().getNodeValue());
				UniqueID=doc.getElementsByTagName("UniqueID").item(0).getFirstChild().getNodeValue();
				PrintImages=(doc.getElementsByTagName("PrintImages").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				OffsetX=Integer.valueOf(doc.getElementsByTagName("OffsetX").item(0).getFirstChild().getNodeValue());
				OffsetY=Integer.valueOf(doc.getElementsByTagName("OffsetY").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
