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

namespace OpenDental{
	public class ODDataTable{
		public string Name;
		public List<ODDataRow> Rows;

		public ODDataTable(){
			Rows=new List<ODDataRow>();
			Name="";
		}

		public ODDataTable(string xmlData){
			XElement rootElement=XElement.Parse(xmlData);
			Rows=new List<ODDataRow>();
			Name="";
			ODDataRow currentRow;
			foreach(XElement elementRow in rootElement.Elements()){
				currentRow=new ODDataRow();
				if(Name==""){
					Name=elementRow.Name.ToString();
				}
				foreach(XElement elementCell in elementRow.Elements()){
					currentRow.Add(elementCell.Name.ToString(),elementCell.Value);
				}
				Rows.Add(currentRow);
			}
		}

		public List<T> ToList<T>() {
			Type tp=typeof(T);
			//List<object> list=new List<object>();
			List<T> list=new List<T>();
			FieldInfo[] fieldInfo=tp.GetFields();
			Object obj=default(T);
			for(int i=0;i<Rows.Count;i++){
				ConstructorInfo constructor=tp.GetConstructor(System.Type.EmptyTypes);
				obj=constructor.Invoke(null);
				for(int f=0;f<fieldInfo.Length;f++){
					if(fieldInfo[f].FieldType==typeof(int)){
						fieldInfo[f].SetValue(obj,PIn.PInt(Rows[i][f]));
					}
					else if(fieldInfo[f].FieldType==typeof(bool)){
						fieldInfo[f].SetValue(obj,PIn.PBool(Rows[i][f]));
					}
					else if(fieldInfo[f].FieldType==typeof(string)){
						fieldInfo[f].SetValue(obj,PIn.PString(Rows[i][f]));
					}
					else if(fieldInfo[f].FieldType.IsEnum){
						object val=((object[])Enum.GetValues(fieldInfo[f].FieldType))[PIn.PInt(Rows[i][f])];
						fieldInfo[f].SetValue(obj,val);
					}
				}
				list.Add((T)obj);
				//Collection 
			}
			return list;//(List<T>)list.Cast<T>();
		}

		//public List ToList(){
			//public T Field;
		//	return null;
		//}

		
	}
}
