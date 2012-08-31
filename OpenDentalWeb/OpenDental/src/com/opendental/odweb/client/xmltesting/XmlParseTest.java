package com.opendental.odweb.client.xmltesting;

import com.google.gwt.xml.client.Document;
import com.google.gwt.dom.client.Node;
import com.google.gwt.xml.client.NodeList;
import com.google.gwt.xml.client.XMLParser;
import com.google.gwt.xml.client.impl.DOMParseException;

public class XmlParseTest {
	
	// TODO Remove the entire xmltesting package before releasing.
	
	public static String GetNodeValueFromXml(String xmlMsg,String elementTagName) {
		try{
			Document doc=XMLParser.parse(xmlMsg);
			NodeList nodeList=doc.getElementsByTagName(elementTagName);
			Node node=(Node) nodeList.item(0).getFirstChild();
			return node.getNodeValue();
		}
		catch(DOMParseException e) {
			return e.getMessage();
		}
	}
	
}
