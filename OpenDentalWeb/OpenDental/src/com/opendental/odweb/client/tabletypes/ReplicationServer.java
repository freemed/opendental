package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ReplicationServer deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReplicationServer>");
			sb.append("<ReplicationServerNum>").append(ReplicationServerNum).append("</ReplicationServerNum>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<ServerId>").append(ServerId).append("</ServerId>");
			sb.append("<RangeStart>").append(RangeStart).append("</RangeStart>");
			sb.append("<RangeEnd>").append(RangeEnd).append("</RangeEnd>");
			sb.append("<AtoZpath>").append(Serializing.escapeForXml(AtoZpath)).append("</AtoZpath>");
			sb.append("<UpdateBlocked>").append((UpdateBlocked)?1:0).append("</UpdateBlocked>");
			sb.append("<SlaveMonitor>").append(Serializing.escapeForXml(SlaveMonitor)).append("</SlaveMonitor>");
			sb.append("</ReplicationServer>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ReplicationServerNum")!=null) {
					ReplicationServerNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReplicationServerNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"ServerId")!=null) {
					ServerId=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ServerId"));
				}
				if(Serializing.getXmlNodeValue(doc,"RangeStart")!=null) {
					RangeStart=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RangeStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"RangeEnd")!=null) {
					RangeEnd=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RangeEnd"));
				}
				if(Serializing.getXmlNodeValue(doc,"AtoZpath")!=null) {
					AtoZpath=Serializing.getXmlNodeValue(doc,"AtoZpath");
				}
				if(Serializing.getXmlNodeValue(doc,"UpdateBlocked")!=null) {
					UpdateBlocked=(Serializing.getXmlNodeValue(doc,"UpdateBlocked")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"SlaveMonitor")!=null) {
					SlaveMonitor=Serializing.getXmlNodeValue(doc,"SlaveMonitor");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
