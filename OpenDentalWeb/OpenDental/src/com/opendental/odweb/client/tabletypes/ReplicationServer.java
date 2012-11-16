package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ReplicationServer {
		/** Primary key. */
		public int ReplicationServerNum;
		/** The description or name of the server.  Optional. */
		public String Descript;
		/** Db admin sets this server_id server variable on each replication server.  Allows us to know what server each workstation is connected to.  In display, it's ordered by this value.  Users are always forced to enter a value here. */
		public int ServerId;
		/** The start of the key range for this server.  0 if no value entered yet. */
		public int RangeStart;
		/** The end of the key range for this server.  0 if no value entered yet. */
		public int RangeEnd;
		/** The AtoZpath for this server. Optional. */
		public String AtoZpath;
		/** If true, then this server cannot initiate an update.  Typical for satellite servers. */
		public boolean UpdateBlocked;
		/** The description or name of the comptuer that will monitor replication for this server. */
		public String SlaveMonitor;

		/** Deep copy of object. */
		public ReplicationServer Copy() {
			ReplicationServer replicationserver=new ReplicationServer();
			replicationserver.ReplicationServerNum=this.ReplicationServerNum;
			replicationserver.Descript=this.Descript;
			replicationserver.ServerId=this.ServerId;
			replicationserver.RangeStart=this.RangeStart;
			replicationserver.RangeEnd=this.RangeEnd;
			replicationserver.AtoZpath=this.AtoZpath;
			replicationserver.UpdateBlocked=this.UpdateBlocked;
			replicationserver.SlaveMonitor=this.SlaveMonitor;
			return replicationserver;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReplicationServer>");
			sb.append("<ReplicationServerNum>").append(ReplicationServerNum).append("</ReplicationServerNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<ServerId>").append(ServerId).append("</ServerId>");
			sb.append("<RangeStart>").append(RangeStart).append("</RangeStart>");
			sb.append("<RangeEnd>").append(RangeEnd).append("</RangeEnd>");
			sb.append("<AtoZpath>").append(Serializing.EscapeForXml(AtoZpath)).append("</AtoZpath>");
			sb.append("<UpdateBlocked>").append((UpdateBlocked)?1:0).append("</UpdateBlocked>");
			sb.append("<SlaveMonitor>").append(Serializing.EscapeForXml(SlaveMonitor)).append("</SlaveMonitor>");
			sb.append("</ReplicationServer>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ReplicationServerNum")!=null) {
					ReplicationServerNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ReplicationServerNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.GetXmlNodeValue(doc,"Descript");
				}
				if(Serializing.GetXmlNodeValue(doc,"ServerId")!=null) {
					ServerId=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ServerId"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RangeStart")!=null) {
					RangeStart=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RangeStart"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RangeEnd")!=null) {
					RangeEnd=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RangeEnd"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AtoZpath")!=null) {
					AtoZpath=Serializing.GetXmlNodeValue(doc,"AtoZpath");
				}
				if(Serializing.GetXmlNodeValue(doc,"UpdateBlocked")!=null) {
					UpdateBlocked=(Serializing.GetXmlNodeValue(doc,"UpdateBlocked")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"SlaveMonitor")!=null) {
					SlaveMonitor=Serializing.GetXmlNodeValue(doc,"SlaveMonitor");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
