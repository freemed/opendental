package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
		public CentralConnection Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<CentralConnection>");
			sb.append("<CentralConnectionNum>").append(CentralConnectionNum).append("</CentralConnectionNum>");
			sb.append("<ServerName>").append(Serializing.EscapeForXml(ServerName)).append("</ServerName>");
			sb.append("<DatabaseName>").append(Serializing.EscapeForXml(DatabaseName)).append("</DatabaseName>");
			sb.append("<MySqlUser>").append(Serializing.EscapeForXml(MySqlUser)).append("</MySqlUser>");
			sb.append("<MySqlPassword>").append(Serializing.EscapeForXml(MySqlPassword)).append("</MySqlPassword>");
			sb.append("<ServiceURI>").append(Serializing.EscapeForXml(ServiceURI)).append("</ServiceURI>");
			sb.append("<OdUser>").append(Serializing.EscapeForXml(OdUser)).append("</OdUser>");
			sb.append("<OdPassword>").append(Serializing.EscapeForXml(OdPassword)).append("</OdPassword>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<WebServiceIsEcw>").append((WebServiceIsEcw)?1:0).append("</WebServiceIsEcw>");
			sb.append("</CentralConnection>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CentralConnectionNum=Integer.valueOf(doc.getElementsByTagName("CentralConnectionNum").item(0).getFirstChild().getNodeValue());
				ServerName=doc.getElementsByTagName("ServerName").item(0).getFirstChild().getNodeValue();
				DatabaseName=doc.getElementsByTagName("DatabaseName").item(0).getFirstChild().getNodeValue();
				MySqlUser=doc.getElementsByTagName("MySqlUser").item(0).getFirstChild().getNodeValue();
				MySqlPassword=doc.getElementsByTagName("MySqlPassword").item(0).getFirstChild().getNodeValue();
				ServiceURI=doc.getElementsByTagName("ServiceURI").item(0).getFirstChild().getNodeValue();
				OdUser=doc.getElementsByTagName("OdUser").item(0).getFirstChild().getNodeValue();
				OdPassword=doc.getElementsByTagName("OdPassword").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				WebServiceIsEcw=(doc.getElementsByTagName("WebServiceIsEcw").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
