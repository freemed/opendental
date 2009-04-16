using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace OpenDentBusiness {
	
	public class ODDataSet {
		public ODDataTableCollection Tables;

		public ODDataSet(string xmlData){
			Tables=new ODDataTableCollection();
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xmlData);
			ODDataTable currentTable=new ODDataTable();
			ODDataRow currentRow;
			XmlNodeList nodesRows=doc.DocumentElement.ChildNodes;
			for(int i=0;i<nodesRows.Count;i++) {
			//foreach(XElement elementRow in rootElement.Elements()) {
				currentRow=new ODDataRow();
				if(currentTable.Name=="") {
					currentTable.Name=nodesRows[i].Name;
				}
				else if(currentTable.Name!=nodesRows[i].Name) {
					this.Tables.Add(currentTable);
					currentTable=new ODDataTable();
					currentTable.Name=nodesRows[i].Name;
				}
				foreach(XmlNode nodeCell in nodesRows[i].ChildNodes) {
					currentRow.Add(nodeCell.Name,nodeCell.InnerXml);
				}
				currentTable.Rows.Add(currentRow);
			}
			this.Tables.Add(currentTable);
			/*
			XElement rootElement=XElement.Parse(xmlData);
			ODDataTable currentTable=new ODDataTable();
			//Dictionary<string,string> currentRow;
			ODDataRow currentRow;
			foreach(XElement elementRow in rootElement.Elements()){
				currentRow=new ODDataRow();//new Dictionary<string,string>();
				if(currentTable.Name==""){
					currentTable.Name=elementRow.Name.ToString();
				}
				else if(currentTable.Name!=elementRow.Name.ToString()){
					this.Tables.Add(currentTable);
					currentTable=new ODDataTable();
					currentTable.Name=elementRow.Name.ToString();
				}
				foreach(XElement elementCell in elementRow.Elements()){
					currentRow.Add(elementCell.Name.ToString(),elementCell.Value);
				}
				currentTable.Rows.Add(currentRow);
			}
			this.Tables.Add(currentTable);*/
		}
	}

	///<summary></summary>
	public class ODDataTableCollection:System.Collections.ObjectModel.Collection<ODDataTable>{
		///<summary></summary>
		public ODDataTable this[string name]{
      get{
				foreach(ODDataTable table in this){
					if(table.Name==name){
						return table;
					}
				}
				ODDataTable tbl=new ODDataTable();
				tbl.Name=name;
				return tbl;
      }
		}
	}
	
	

	


}
