using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

namespace OpenDentMobile{
	public class XMLParser {
		public static void ImportXML(string fileName){
			DateTime startTime=DateTime.Now;
			System.Xml.XmlDocument doc=new XmlDocument();
			doc.Load(fileName);
			DateTime loadedTime=DateTime.Now;
			//	.LoadXml(xmlData);
			//node 0 is the xml declaration
			XmlNode mainNode=doc.ChildNodes[1];
			if(mainNode.Name!="InToMobile"){
				throw new ApplicationException("Main node must be 'InToMobile'");
			}
			XmlAttributeCollection attributes=mainNode.Attributes;
			string mainVersion="";
			string minimumMobileVersion="";
			string fullSync="";
			//MainVersion="6.3.0.0" MinimumMobileVersion="6.3.0.0" FullSync="true"
			for(int i=0;i<attributes.Count;i++){
				if(attributes[i].Name=="MainVersion"){
					mainVersion=attributes[i].Value;
				}
				if(attributes[i].Name=="MinimumMobileVersion"){
					minimumMobileVersion=attributes[i].Value;
				}
				if(attributes[i].Name=="FullSync"){
					fullSync=attributes[i].Value;
				}
			}
			Version minVersion=new Version(minimumMobileVersion);
			if(minVersion > Assembly.GetExecutingAssembly().GetName().Version){
				throw new ApplicationException("Please upgrade this program to a newer version.");
			}
			if(fullSync=="true"){
				Patients.DeleteAll();
			}
			XmlNodeList mainNodeList=mainNode.ChildNodes;
			//MessageBox.Show(nodeList.Count.ToString()+" nodes");
			XmlNode objectNode;
			//XmlNodeList childNodes;
			for(int i=0;i<mainNodeList.Count;i++){
				objectNode=mainNodeList[i];
				if(objectNode.Name=="patient"){
					Patients.Sync(objectNode);
				}
			}
			//MessageBox.Show("Time to load XML: "+(loadedTime-startTime).ToString()+"\r\n"
			//	+"Time to run queries: "+(DateTime.Now-loadedTime).ToString());
		}

	}
}
