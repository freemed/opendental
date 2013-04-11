using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	public class XmlConverter {
		///<summary>Should accept any type, including simple types, OD types, Arrays, Lists, and arrays of DtoObject.  But not DataTable or DataSet.  If we find a type that isn't supported, then we need to add it.</summary>
		public static string Serialize<T>(T obj) {
			StringBuilder strBuild=new StringBuilder();
			#if DEBUG
				XmlWriterSettings settings=new XmlWriterSettings();
				settings.Indent=true;
				settings.IndentChars="   ";
				//using the constructor decreases performance and leads to memory leaks.
				//But it makes the xml much more readable
				XmlWriter writer=XmlWriter.Create(strBuild,settings);
			#else
				XmlWriter writer=XmlWriter.Create(strBuild);
			#endif
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(writer,obj);
			writer.Close();
			return strBuild.ToString();
		}

		///<summary>For late binding of class type.</summary>
		public static string Serialize(Type classType,object obj) {
			StringBuilder strBuild=new StringBuilder();
			#if DEBUG
				XmlWriterSettings settings=new XmlWriterSettings();
				settings.Indent=true;
				settings.IndentChars="   ";
				//settings.NewLineHandling=NewLineHandling.None;//an attempt to not remove \r in strings.  Failed.
				//using the constructor decreases performance and leads to memory leaks.
				//But it makes the xml much more readable
				XmlWriter writer=XmlWriter.Create(strBuild,settings);
			#else
				XmlWriter writer=XmlWriter.Create(strBuild);
			#endif
			XmlSerializer serializer;
			if(classType==typeof(Color)) {
				serializer = new XmlSerializer(typeof(int));
				serializer.Serialize(writer,((Color)obj).ToArgb());
			}
			else if(classType==typeof(TimeSpan)) {
				serializer = new XmlSerializer(typeof(long));
				serializer.Serialize(writer,((TimeSpan)obj).Ticks);
			}
			else {
				serializer = new XmlSerializer(classType);
				serializer.Serialize(writer,obj);
			}
			writer.Close();
			return strBuild.ToString();
			//the result will be fully qualified xml, including declaration.  Example:
			/*
			{<?xml version="1.0" encoding="utf-16"?>
<Userod xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
   <IsNew>false</IsNew>
   <UserNum>1</UserNum>
   <UserName>Admin</UserName>
   <Password />
   <UserGroupNum>1</UserGroupNum>
   <EmployeeNum>0</EmployeeNum>
   <ClinicNum>0</ClinicNum>
   <ProvNum>0</ProvNum>
   <IsHidden>false</IsHidden>
   <TaskListInBox>0</TaskListInBox>
   <AnesthProvType>3</AnesthProvType>
   <DefaultHidePopups>false</DefaultHidePopups>
   <PasswordIsStrong>false</PasswordIsStrong>
</Userod>}*/
		}

		///<summary>Should accept any type.  Tested types include System types, OD types, Arrays, Lists, arrays of DtoObject, null DataObjectBase, null arrays, null Lists.  But not DataTable or DataSet.  If we find a type that isn't supported, then we need to add it.  Types that are currently unsupported include Arrays of DataObjectBase that contain a null.  Lists that contain nulls are untested and may be an issue for DataObjectBase.</summary>
		public static T Deserialize<T>(string xmlData) {
			Type type = typeof(T);
			/*later.  I don't think arrays will null objects will be an issue.
			if(type.IsArray) {
				Type arrayType=type.GetElementType();
				if(arrayType.BaseType==typeof(DataObjectBase)) {
					//split into items
				}
			}*/
			if(type.IsGenericType) {//List<>
				//because the built-in deserializer does not handle null list<>, but instead returns an empty list.
				//<ArrayOfDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:nil="true" />
				if(Regex.IsMatch(xmlData,"<ArrayOf[^>]*xsi:nil=\"true\"")) {
					return default(T);//null
				}
			}
			StringReader strReader=new StringReader(xmlData);
			//XmlReader reader=XmlReader.Create(strReader);
			XmlTextReader reader=new XmlTextReader(strReader);
			XmlSerializer serializer;
			T retVal;
			if(type==typeof(Color)) {
				serializer = new XmlSerializer(typeof(int));
				retVal=(T)((object)Color.FromArgb((int)serializer.Deserialize(reader)));
			}
			else if(type==typeof(TimeSpan)) {
				serializer = new XmlSerializer(typeof(long));
				retVal=(T)((object)TimeSpan.FromTicks((long)serializer.Deserialize(reader)));
			}
			else {
				serializer = new XmlSerializer(type);
				retVal=(T)serializer.Deserialize(reader);
			}
			strReader.Close();
			reader.Close();
			return retVal;
		}

		/*
		public static string TableToXml(DataTable table){
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			if(table.TableName==""){
			//	throw new ApplicationException("OpenDentBusiness.WebServer.XmlConverter.TableToXml requires a tablename matching the type of the collection.");
				table.TableName="Table";
			}
			table.WriteXml(xmlWriter,XmlWriteMode.WriteSchema);
			xmlWriter.Close();
			return strBuild.ToString();
		}*/


		///<summary>Serializes a DataTable by looping through the rows and columns.</summary>
		public static string TableToXml(DataTable table) {
			StringBuilder result=new StringBuilder();
			result.Append("<DataTable>");
			//Table name.
			result.Append("<Name>").Append(EscapeForXml(table.TableName)).Append("</Name>");
			//Column names.
			result.Append("<Cols>");
			for(int i=0;i<table.Columns.Count;i++) {
				//result.Append("<Col>").Append(EscapeForXml(table.Columns[i].ColumnName)).Append("</Col>");
				//We escape column names just in case.
				result.Append("<Col");
				if(table.Columns[i].DataType==typeof(decimal)) {
					result.Append(" DataType=\"decimal\"");
				}
				else if(table.Columns[i].DataType==typeof(DateTime)) {
					result.Append(" DataType=\"DateTime\"");
				}
				result.Append(">");
				result.Append(EscapeForXml(table.Columns[i].ColumnName));
				result.Append("</Col>");
			}
			result.Append("</Cols>");
			//Set each cell by looping through each column row by row.
			result.Append("<Cells>");
			for(int i=0;i<table.Rows.Count;i++) {//Row loop.
				result.Append("<y>");
				for(int j=0;j<table.Columns.Count;j++) {//Column loop.
					//old way: <x>cell0</x><x>cell1</x><x>cellwith|pipe</x>
					//new way: cell0|cell1|cellwith\|pipe
					//strategy for deserialize: convert \| to &#124;
					//then, split by |.  Then convert &#124; back to |.
					string content=table.Rows[i][j].ToString();
					if(content.Trim()=="") {//Test if the element will be empty, this will save space on big DataTables.
						result.Append("<x/>");
					}
					else {
						//this step is probably too slow.  The only solution might be to rewrite the entire method to use an XmlWriter.
						result.Append("<x>").Append(EscapeForXml(table.Rows[i][j].ToString())).Append("</x>");
					}
					/*
					if(content.Trim()=="") {//Test if the element will be empty, this will save space on big DataTables.
						result.Append("|");
					}
					else {
						//this step is probably too slow.  The only solution might be to rewrite the entire method to use an XmlWriter.
						result.Append(EscapeForXml(table.Rows[i][j].ToString()));
						if(j<table.Columns.Count-1) {//append | only if not last column
							result.Append("|");
						}
					}*/
				}
				result.Append("</y>");
			}
			result.Append("</Cells>");
			result.Append("</DataTable>");
			return result.ToString();
		}

		/*public static string DsToXml(DataSet ds) {
			StringBuilder strBuild=new StringBuilder();
			XmlWriter xmlWriter=XmlWriter.Create(strBuild);
			ds.WriteXml(xmlWriter,XmlWriteMode.WriteSchema);
			xmlWriter.Close();
			return strBuild.ToString();
		}*/

		public static string DsToXml(DataSet ds) {
			StringBuilder strb=new StringBuilder();
			strb.Append("<DataSet>");
			strb.Append("<DataTables>");
			for(int i=0;i<ds.Tables.Count;i++) {
				strb.Append(TableToXml(ds.Tables[i]));
			}
			strb.Append("</DataTables>");
			strb.Append("</DataSet>");
			return strb.ToString();
		}


		/// <summary></summary>
		public static DataTable XmlToTable(string xmlData) {
			DataTable table=new DataTable();
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xmlData);
			//<DataTable><Name></Name><Cols><Col>cell00</Col></Cols><Cells><y><x>cell00</x></y></Cells></DataTable>
			XmlNode nodeName=doc.SelectSingleNode("//Name");
			table.TableName=nodeName.InnerText;
			XmlNode nodeCols=doc.SelectSingleNode("//Cols");
			for(int i=0;i<nodeCols.ChildNodes.Count;i++) {
				DataColumn col=new DataColumn(nodeCols.ChildNodes[i].InnerText);
				if(nodeCols.ChildNodes[i].Attributes.Count>0) {//if attribute is set for column
					string dataType=nodeCols.ChildNodes[i].Attributes["DataType"].InnerText;//this is safe because we created the xml
					if(dataType=="decimal") {
						col.DataType=typeof(decimal);
					}
					else if(dataType=="DateTime") {
						col.DataType=typeof(DateTime);
					}
				}
				table.Columns.Add(col);
			}
			XmlNodeList nodeListY=doc.SelectSingleNode("//Cells").ChildNodes;
			for(int y=0;y<nodeListY.Count;y++) {//loop y rows
				DataRow row=table.NewRow();
				XmlNodeList nodeListX=nodeListY[y].ChildNodes;
				for(int x=0;x<nodeListX.Count;x++) {//loop x cells 
					row[x]=nodeListX[x].InnerText;
				}
				table.Rows.Add(row);
				//XmlNodeList nodeListX=nodeListY[y].ChildNodes;//should only be one child node of cells separated by pipes
				////we replace \| here before splitting by | but the &#124; is not part of the serialization
				//string cellxml=nodeListX[0].InnerXml;
				//string cellText=nodeListX[0].InnerText.Replace(@"\|","&#124;");
				//string[] cells=cellText.Split('|');
				//for(int x=0;x<cells.Length;x++) {//loop x cells
				//  row[x]=cells[x].Replace("&#124;","|");
				//}
				//table.Rows.Add(row);
			}
			return table;
		}

		/*
		public static DataTable XmlToTable(string xmlData) {
			DataTable table=new DataTable();
			//XmlReader xmlReader=XmlReader.Create(new StringReader(xmlData));
			XmlTextReader xmlReader=new XmlTextReader(new StringReader(xmlData));
			table.ReadXml(xmlReader);
			xmlReader.Close();
			return table;
		}*/

		/*public static DataSet XmlToDs(string xmlData) {
			DataSet ds=new DataSet();
			//XmlReader xmlReader=XmlReader.Create(new StringReader(xmlData));
			XmlTextReader xmlReader=new XmlTextReader(new StringReader(xmlData));
			ds.ReadXml(xmlReader);
			xmlReader.Close();
			return ds;
		}*/

		public static DataSet XmlToDs(string xmlData) {
			DataSet ds=new DataSet();
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(xmlData);
			//<DataSet><DataTables><DataTable><Name>table0</Name><Cols><Col>cell00</Col></Cols><Cells><y><x>cell00</x></y></Cells></DataTable></DataTables></DataSet>
			XmlNode nodeTables=doc.SelectSingleNode("//DataTables");
			for(int t=0;t<nodeTables.ChildNodes.Count;t++) {
				ds.Tables.Add(XmlToTable(nodeTables.ChildNodes[t].OuterXml));//<DataTable>....</DataTable>
			}
			return ds;
		}

		///<summary>Escapes common characters used in XML from the passed in String.</summary>
		public static string EscapeForXml(string myString) {
			StringBuilder strBuild=new StringBuilder();
			int length=myString.Length;
			for(int i=0;i<length;i++) {
				String character=myString.Substring(i,1);
				if(character.Equals("<")) {
					strBuild.Append("&lt;");
					continue;
				}
				else if(character.Equals(">")) {
					strBuild.Append("&gt;");
					continue;
				}
				else if(character.Equals("\"")) {
					strBuild.Append("&quot;");
					continue;
				}
				else if(character.Equals("\'")) {
					strBuild.Append("&#039;");
					continue;
				}
				else if(character.Equals("&")) {
					strBuild.Append("&amp;");
					continue;
				}
				//else if(character.Equals("|")) {
				//  strBuild.Append("\\|");
				//  continue;
				//}
				////if last char is a '\' we must replace it with &#92; but only for last char in cell so we don't bloat the xml
				//else if(i==length-1 && character.Equals("\\")) {
				//  strBuild.Append("&#92;");
				//  continue;
				//}
				strBuild.Append(character);
			}
			return strBuild.ToString();
		}


	}
}
