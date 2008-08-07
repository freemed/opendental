using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using OpenDentBusiness;

namespace OpenDental {
	public class ODDataSet {
		public ODDataTableCollection Tables;

		public ODDataSet(string xmlData){
			Tables=new ODDataTableCollection();
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
			this.Tables.Add(currentTable);
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
