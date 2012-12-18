package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class CentralConnection {
		/** Primary key. */
		public int CentralConnectionNum;
		/** If direct db connection.  Can be ip address. */
		public String ServerName;
		/** If direct db connection. */
		public String DatabaseName;
		/** If direct db connection. */
		public String MySqlUser;
		/** If direct db connection.  Symmetrically encrypted. */
		public String MySqlPassword;
		/** If connecting to the web service. Can be on VPN, or can be over https. */
		public String ServiceURI;
		/** If connecting to the web service. */
		public String OdUser;
		/** If connecting to the web service.  Symmetrically encrypted. */
		public String OdPassword;
		/** . */
		public String Note;
		/** 0-based. */
		public int ItemOrder;
		/** If set to true, the password hash is calculated differently. */
		public boolean WebServiceIsEcw;

		/** Deep copy of object. */
		public CentralConnection deepCopy() {
			CentralConnection centralconnection=new CentralConnection();
			centralconnection.CentralConnectionNum=this.CentralConnectionNum;
			centralconnection.ServerName=this.ServerName;
			centralconnection.DatabaseName=this.DatabaseName;
			centralconnection.MySqlUser=this.MySqlUser;
			centralconnection.MySqlPassword=this.MySqlPassword;
			centralconnection.ServiceURI=this.ServiceURI;
			centralconnection.OdUser=this.OdUser;
			centralconnection.OdPassword=this.OdPassword;
			centralconnection.Note=this.Note;
			centralconnection.ItemOrder=this.ItemOrder;
			centralconnection.WebServiceIsEcw=this.WebServiceIsEcw;
			return centralconnection;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CentralConnection>");
			sb.append("<CentralConnectionNum>").append(CentralConnectionNum).append("</CentralConnectionNum>");
			sb.append("<ServerName>").append(Serializing.escapeForXml(ServerName)).append("</ServerName>");
			sb.append("<DatabaseName>").append(Serializing.escapeForXml(DatabaseName)).append("</DatabaseName>");
			sb.append("<MySqlUser>").append(Serializing.escapeForXml(MySqlUser)).append("</MySqlUser>");
			sb.append("<MySqlPassword>").append(Serializing.escapeForXml(MySqlPassword)).append("</MySqlPassword>");
			sb.append("<ServiceURI>").append(Serializing.escapeForXml(ServiceURI)).append("</ServiceURI>");
			sb.append("<OdUser>").append(Serializing.escapeForXml(OdUser)).append("</OdUser>");
			sb.append("<OdPassword>").append(Serializing.escapeForXml(OdPassword)).append("</OdPassword>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<WebServiceIsEcw>").append((WebServiceIsEcw)?1:0).append("</WebServiceIsEcw>");
			sb.append("</CentralConnection>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CentralConnectionNum")!=null) {
					CentralConnectionNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CentralConnectionNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ServerName")!=null) {
					ServerName=Serializing.getXmlNodeValue(doc,"ServerName");
				}
				if(Serializing.getXmlNodeValue(doc,"DatabaseName")!=null) {
					DatabaseName=Serializing.getXmlNodeValue(doc,"DatabaseName");
				}
				if(Serializing.getXmlNodeValue(doc,"MySqlUser")!=null) {
					MySqlUser=Serializing.getXmlNodeValue(doc,"MySqlUser");
				}
				if(Serializing.getXmlNodeValue(doc,"MySqlPassword")!=null) {
					MySqlPassword=Serializing.getXmlNodeValue(doc,"MySqlPassword");
				}
				if(Serializing.getXmlNodeValue(doc,"ServiceURI")!=null) {
					ServiceURI=Serializing.getXmlNodeValue(doc,"ServiceURI");
				}
				if(Serializing.getXmlNodeValue(doc,"OdUser")!=null) {
					OdUser=Serializing.getXmlNodeValue(doc,"OdUser");
				}
				if(Serializing.getXmlNodeValue(doc,"OdPassword")!=null) {
					OdPassword=Serializing.getXmlNodeValue(doc,"OdPassword");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"WebServiceIsEcw")!=null) {
					WebServiceIsEcw=(Serializing.getXmlNodeValue(doc,"WebServiceIsEcw")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
