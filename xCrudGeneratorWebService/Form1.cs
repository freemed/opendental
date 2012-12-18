using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Xml;
using System.Xml.XPath;

namespace xCrudGeneratorWebService {
	public partial class Form1:Form {
		private string SerialDir;
		private string DtoMethodsDir;
		private string JavaSClassesDir;
		private string JavaTableTypesDir;
		private string JavaSerializingDir;
		private XPathNavigator Navigator;
		private const string rn="\r\n";
		private const string t="\t";
		private const string t2="\t\t";
		private const string t3="\t\t\t";
		private const string t4="\t\t\t\t";
		private const string t5="\t\t\t\t\t";
		private const string t6="\t\t\t\t\t\t";
		private const string t7="\t\t\t\t\t\t\t";
		private List<Type> TableTypes;

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender,EventArgs e) {
			SerialDir=@"..\..\..\OpenDentalWebService\Serializing";
			DtoMethodsDir=@"..\..\..\OpenDentalWebService\Remoting\DtoMethods.cs";
			JavaSClassesDir=@"..\..\..\OpenDentalWeb\OpenDental\src\com\opendental\odweb\client\datainterface";
			JavaTableTypesDir=@"..\..\..\OpenDentalWeb\OpenDental\src\com\opendental\odweb\client\tabletypes";
			JavaSerializingDir=@"..\..\..\OpenDentalWeb\OpenDental\src\com\opendental\odweb\client\remoting\Serializing.java";
			TableTypes=new List<Type>();
			Type typeTableBase=typeof(TableBase);
			Assembly assembly=Assembly.GetAssembly(typeTableBase);
			Type[] t=assembly.GetTypes();
			foreach(Type typeClass in assembly.GetTypes()){
				if(typeClass.BaseType==typeTableBase) {
					if(IsMobile(typeClass)) {
						continue;
					}
					TableTypes.Add(typeClass);	
				}
			}
			TableTypes.Sort(CompareTypesByName);
		}

		private int CompareTypesByName(Type x, Type y){
			return x.Name.CompareTo(y.Name);
		}

		///<summary>This will allow us to skip the mobile types.</summary>
		private bool IsMobile(Type typeClass) {
			object[] attributes = typeClass.GetCustomAttributes(typeof(CrudTableAttribute),true);
			if(attributes.Length==0) {
				return false;
			}
			for(int i=0;i<attributes.Length;i++) {
				if(attributes[i].GetType()!=typeof(CrudTableAttribute)) {
					continue;
				}
				if(((CrudTableAttribute)attributes[i]).IsMobile) {
					return true;
				}
			}
			//couldn't find any.
			return false;
		}

		private void butRun_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			StringBuilder strb;
			StringBuilder strbDtoMethods=new StringBuilder();
			StringBuilder strbDtoClassesSerialize=new StringBuilder();
			StringBuilder strbDtoClassesDeserialize=new StringBuilder();
			StringBuilder strbCallMethod=new StringBuilder();
			StringBuilder strbMethodCalls=new StringBuilder();
			StringBuilder strbJavaSerial=new StringBuilder();
			StringBuilder strbJavaGetSerialized=new StringBuilder();
			StringBuilder strbJavaGetDeserialized=new StringBuilder();
			StartDtoMethods(strbDtoMethods);
			StartCallMethod(strbCallMethod);
			StartJavaSerial(strbJavaSerial);
			string className;
			//this is so we can get the summary of the data types.
			XmlDocument document=new XmlDocument();
			document.Load(@"..\..\..\OpenDentBusiness\bin\Release\OpenDentBusiness.xml");
			Navigator=document.CreateNavigator();
			#region TableType loop
			for(int i=0;i<TableTypes.Count;i++) {
				className=TableTypes[i].Name;
				string docForjava="";
				if(className=="Document") {
					docForjava="od";
				}
				FieldInfo[] fields=null;
				fields=TableTypes[i].GetFields();
				strb=new StringBuilder();
				WriteAllSerializing(strb,className,fields);
				File.WriteAllText(Path.Combine(SerialDir,className+".cs"),strb.ToString());
				strb.Clear();
				if(!File.Exists(Path.Combine(JavaSClassesDir,GetSname(className+docForjava)+".java"))) {//Only create a java S class if one does not already exist.
					WriteAlljavaDataInterface(strb,className);
					File.WriteAllText(Path.Combine(JavaSClassesDir,GetSname(className+docForjava)+".java"),strb.ToString());
					strb.Clear();
				}
				WriteAlljavaTableTypes(strb,className,fields);
				File.WriteAllText(Path.Combine(JavaTableTypesDir,className+docForjava+".java"),strb.ToString());
				#region DtoMethods OpenDentalClasses for Serializing and Deserializing
				strbDtoClassesSerialize.Append(t3+"if(objectType==\""+"OpenDentBusiness."+className+"\") {"+rn
					+t4+"return "+className+".Serialize((OpenDentBusiness."+className+")obj);"+rn
					+t3+"}"+rn);
				strbDtoClassesDeserialize.Append(t3+"if(typeName==\""+className+"\") {"+rn
					+t4+"return "+className+".Deserialize(xml);"+rn
					+t3+"}"+rn);
				#endregion
				#region CallMethod 
				strbCallMethod.Append(t3+"if(className==\""+GetSname(className)+"\") {"+rn
					+t4+"return Method"+GetSname(className)+"(methodName,parameters);"+rn
					+t3+"}"+rn);
				#endregion
				WriteMethodCalls(strbMethodCalls,TableTypes[i]);
				#region GetJavaSerialized
				strbJavaGetSerialized.Append(t2+"if(qualifiedName.equals(\"com.opendental.odweb.client.tabletypes."+className+docForjava+"\")) {"+rn
					+t3+"return (("+className+docForjava+")obj).serialize();"+rn
					+t2+"}"+rn);
				#endregion
				#region GetJavaDeserialized
				strbJavaGetDeserialized.Append(t2+"if(type.equals(\""+className+docForjava+"\")) {"+rn
					+t3+className+docForjava+" "+className.ToLower()+docForjava+"=new "+className+docForjava+"();"+rn
					+t3+className.ToLower()+docForjava+".deserialize(doc);"+rn
					+t3+"return "+className.ToLower()+docForjava+";"+rn
					+t2+"}"+rn);
				#endregion
				Application.DoEvents();
			}
			#endregion
			#region CallClassSerializer
			StartCallClassSerializer(strbDtoMethods);
			#endregion
			#region Append OD Classes for Serializing
			strbDtoMethods.Append(strbDtoClassesSerialize);
			#endregion
			#region EndCallClassSerializer
			strbDtoMethods.Append(t3+"#endregion"+rn
				+t3+"throw new NotSupportedException(\"CallClassSerializer, unsupported class type: \"+objectType);"+rn
				+t2+"}"+rn+rn);
			#endregion
			#region CallClassDeserializer
			StartCallClassDeserializer(strbDtoMethods);
			#endregion
			#region Append OD Classes for Deserializing
			strbDtoMethods.Append(strbDtoClassesDeserialize);
			#endregion
			#region EndCallClassDeserializer
			strbDtoMethods.Append(t3+"#endregion"+rn
				+t3+"throw new NotSupportedException(\"CallClassDeserializer, unsupported class type: \"+typeName);"+rn
				+t2+"}"+rn+rn);
			#endregion
			#region EndCallMethod
			strbCallMethod.Append(t3+"#endregion"+rn
				+t3+"throw new NotSupportedException(\"CallMethod, unknown class: \"+classAndMethod);"+rn
				+t2+"}"+rn+rn
				+t2+"#region Method Calls"+rn+rn);
			#endregion
			strbDtoMethods.Append(strbCallMethod.ToString());
			strbDtoMethods.Append(strbMethodCalls.ToString());
			#region DtoMethods footer
			//do after appending all inner methods
			strbDtoMethods.Append(t2+"#endregion"+rn+rn
				+t+"}"+rn+"}"+rn);
			#endregion
			File.WriteAllText(DtoMethodsDir,strbDtoMethods.ToString());
			strbJavaSerial.Append(strbJavaGetSerialized.ToString());
			MiddleJavaSerial(strbJavaSerial);
			strbJavaSerial.Append(strbJavaGetDeserialized.ToString());
			EndJavaSerial(strbJavaSerial);
			File.WriteAllText(JavaSerializingDir,strbJavaSerial.ToString());
			Cursor=Cursors.Default;
			MessageBox.Show("Done");
		}
		
		///<summary></summary>
		private void StartDtoMethods(StringBuilder strb) {
			#region class header
			strb.Append("using System;"+rn
				+"using System.Collections.Generic;"+rn+rn
				+"namespace OpenDentalWebService {"+rn
				+t+"///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>"+rn
				+t+"public class DtoMethods {"+rn);
			#endregion
			#region ProcessDtoObject
			strb.Append(t2+"///<summary>Processes any type of data transfer object by calling the desired method.</summary>"+rn
				+t2+"public static object ProcessDtoObject(DataTransferObject dto) {"+rn
				+t3+"string classAndMethod=dto.MethodName;"+rn
				+t3+"List<object> parameters=new List<object>();"+rn
				+t3+"for(int i=0;i<dto.Params.Count;i++) {"+rn
				+t4+"parameters.Add(dto.Params[i].Obj);"+rn
				+t3+"}"+rn
				+t3+"return CallMethod(classAndMethod,parameters);"+rn
				+t2+"}"+rn+rn);
			#endregion
		}

		private void StartCallClassSerializer(StringBuilder strb) {
			strb.Append(t2+"///<summary>Calls the class serializer for any supported object, primitive or not.  objectType must be fully qualified.  Ex: System.Int32 or OpenDentBusiness.Account.  Throws exceptions.</summary>"+rn
				+t2+"public static string CallClassSerializer(string objectType,Object obj) {"+rn
				+t3+"#region Primitive and General Types"+rn
				+t3+"//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenSerializerTypes and manually add it there."+rn);
			GetPrimGenSerializerTypes(strb);
			strb.Append(t3+"#endregion"+rn
				+t3+"#region Open Dental Classes"+rn);
		}

		private void StartCallClassDeserializer(StringBuilder strb) {
			strb.Append(t2+"///<summary>Calls the class deserializer based on the typeName passed in.  Mainly used for deserializing parameters on DtoObjects.  Throws exceptions.</summary>"+rn
				+t2+"public static object CallClassDeserializer(string typeName,string xml) {"+rn
				+t3+"#region Primitive and General Types"+rn
				+t3+"//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenDeserializerTypes and manually add it there."+rn);
			GetPrimGenDeserializerTypes(strb);
			strb.Append(t3+"#endregion"+rn
				+t3+"#region Open Dental Classes"+rn);
		}

		///<summary></summary>
		private void StartCallMethod(StringBuilder strb) {
			#region CallMethod header
			strb.Append(t2+"///<summary>Finds the corresponding class, instantiates an instance of that class and invokes the method with the parameters.  Void methods will return null.</summary>"+rn
				+t2+"private static object CallMethod(string classAndMethod,List<object> parameters) {"+rn
				+t3+"string className=classAndMethod.Split('.')[0];"+rn
				+t3+"string methodName=classAndMethod.Split('.')[1];"+rn
				+t3+"#region SClasses"+rn);
			#endregion
		}

		///<summary></summary>
		private void StartJavaSerial(StringBuilder strb) {
			#region JavaSerializing header
			strb.Append("package com.opendental.odweb.client.remoting;"+rn+rn
				+"import com.google.gwt.xml.client.Document;"+rn
				+"import com.google.gwt.xml.client.Element;"+rn
				+"import com.google.gwt.xml.client.Node;"+rn
				+"import com.google.gwt.xml.client.NodeList;"+rn
				+"import com.google.gwt.xml.client.XMLParser;"+rn
				+"import com.opendental.odweb.client.data.*;"+rn
				+"import com.opendental.odweb.client.tabletypes.*;"+rn+rn
				+"/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes made will be overwritten.  To make changes, go to xCrudGeneratorWebService.Form1.cs and make the changes within StartJavaSerial(), MiddleJavaSerial(), and EndJavaSerial(). */"+rn
				+"public class Serializing {"+rn+rn);
			#endregion
			#region EscapeForXml
			strb.Append(t+"/** Escapes common characters used in XML. */"+rn
				+t+"public static String escapeForXml(String myString) {"+rn
				+t2+"StringBuilder strBuild=new StringBuilder();"+rn
				+t2+"int length=myString.length();"+rn
				+t2+"for(int i=0;i<length;i++) {"+rn
				+t3+"String character=myString.substring(i,i+1);"+rn
				+t3+"if(character.equals(\"<\")) {"+rn
				+t4+"strBuild.append(\"&lt;\");"+rn
				+t4+"continue;"+rn
				+t3+"}"+rn
				+t3+"else if(character.equals(\">\")) {"+rn
				+t4+"strBuild.append(\"&gt;\");"+rn
				+t4+"continue;"+rn
				+t3+"}"+rn
				+t3+"else if(character.equals(\"\\\"\")) {"+rn
				+t4+"strBuild.append(\"&quot;\");"+rn
				+t4+"continue;"+rn
				+t3+"}"+rn
				+t3+"else if(character.equals(\"\\\'\")) {"+rn
				+t4+"strBuild.append(\"&#039;\");"+rn
				+t4+"continue;"+rn
				+t3+"}"+rn
				+t3+"else if(character.equals(\"&\")) {"+rn
				+t4+"strBuild.append(\"&amp;\");"+rn
				+t4+"continue;"+rn
				+t3+"}"+rn
				+t3+"strBuild.append(character);"+rn
				+t2+"}"+rn
				+t2+"return strBuild.toString();"+rn
				+t+"}"+rn+rn);
			#endregion
			#region GetSerializedObject
			strb.Append(t+"/** Loops through all the known objects and calls the corresponding classes serialize method."+rn
				+t+" * @param obj Can be any type of object.  Error will occur if the type hasn't been implemented yet. "+rn
				+t+" * @throws Exception Throws exception if type is not yet supported. */"+rn
				+t+"public static String getSerializedObject(Object obj) throws Exception {"+rn
				+t2+"String result;"+rn
				+t2+"//Figure out what type of object we're dealing with and return the serialized form."+rn
				+t2+"String qualifiedName=obj.getClass().getName();//Ex: ArrayList = \"java.util.ArrayList\""+rn
				+t2+"//Primitives--------------------------------------------------------------------------------------------------------"+rn
				+t2+"if(qualifiedName.equals(\"Z\") || qualifiedName.equals(\"java.lang.Boolean\")) {//boolean  \"Z\""+rn
				+t3+"result=(Boolean)obj?\"1\":\"0\";"+rn
				+t3+"return \"<bool>\"+result+\"</bool>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"B\") || qualifiedName.equals(\"java.lang.Byte\")) {//byte  \"B\""+rn
				+t3+"return \"<byte>\"+(Byte)obj+\"</byte>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"C\") || qualifiedName.equals(\"java.lang.Character\")) {//char  \"C\""+rn
				+t3+"return \"<char>\"+(Character)obj+\"</char>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"S\") || qualifiedName.equals(\"java.lang.Short\")) {//short  \"S\""+rn
				+t3+"return \"<short>\"+(Short)obj+\"</short>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"I\") || qualifiedName.equals(\"java.lang.Integer\")) {//int  \"I\""+rn
				+t3+"return \"<int>\"+(Integer)obj+\"</int>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"J\") || qualifiedName.equals(\"java.lang.Long\")) {//long  \"J\""+rn
				+t3+"//return \"<long>\"+(Long)obj+\"</long>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"F\") || qualifiedName.equals(\"java.lang.Float\")) {//float  \"F\""+rn
				+t3+"return \"<float>\"+(Float)obj+\"</float>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"D\") || qualifiedName.equals(\"java.lang.Double\")) {//double  \"D\""+rn
				+t3+"return \"<double>\"+(Double)obj+\"</double>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName.equals(\"java.lang.String\")) {//String  \"java.lang.String\""+rn
				+t3+"return \"<string>\"+(String)obj+\"</string>\";"+rn
				+t2+"}"+rn
				+t2+"//Arrays------------------------------------------------------------------------------------------------------------"+rn
				+t2+"//Multidimensional arrays have equal number of brackets. Ex: Account[][] = [[L..."+rn
				+t2+"//Object[]  \"[Lcom.opendental.odweb.client.tabletypes.Account;\" from Account[]"+rn
				+t2+"//int[]     \"[I\""+rn
				+t2+"//String[]  \"[Ljava.lang.String;\""+rn
				+t2+"//Open Dental Objects-----------------------------------------------------------------------------------------------"+rn);
			#endregion
		}

		///<summary></summary>
		private void MiddleJavaSerial(StringBuilder strb) {
			#region GetSerializedObject footer
			strb.Append(t2+"throw new Exception(\"getSerializedObject, unsupported type: \"+qualifiedName);"+rn
				+t+"}"+rn+rn);
			#endregion
			#region GetDeserializedObject
			strb.Append("/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes will be overwritten.  To make changes, go to xCrudGeneratorWebService.Form1.cs and make the changes within StartJavaSerial(), MiddleJavaSerial(), and EndJavaSerial() */"+rn+rn
				+t+"/** Loops through all the known objects and calls the corresponding classes deserialize method."+rn
				+t+" * @param xml The serialized response from the server.  Handles DtoExceptions."+rn
				+t+" * @throws Exception Throws exception if type is not yet supported or if a DtoException was returned. */"+rn
				+t+"public static Object getDeserializedObject(String xml) throws Exception {"+rn
				+t2+"Document doc=XMLParser.parse(xml);"+rn
				+t2+"XMLParser.removeWhitespace(doc);"+rn
				+t2+"Element element=doc.getDocumentElement();"+rn
				+t2+"if(element==null) {"+rn
				+t3+"throw new Exception(\"getDeserializedObject, the response from server was not valid XML.\");"+rn
				+t2+"}"+rn
				+t2+"//Figure out the response type.  Response examples: <long>4</long> OR <DtoException><msg>Error</msg></DtoException>"+rn
				+t2+"String type=element.getNodeName();"+rn
				+t2+"if(type.equals(\"DtoException\")) {//Check for exceptions first."+rn
				+t3+"//Read the \"msg\" node and throw an exception with that error message."+rn
				+t3+"throw new Exception(doc.getElementsByTagName(\"msg\").item(0).getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"//Primitives-------------------------------------------------------------------------------------------------------"+rn
				+t2+"if(type.equals(\"boolean\")) {"+rn
				+t3+"return element.getFirstChild().getNodeValue()==\"0\"?false:true;"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"byte\")) {"+rn
				+t3+"return Byte.parseByte(element.getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"char\")) {"+rn
				+t3+"return element.getFirstChild().getNodeValue().charAt(0);"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"short\")) {"+rn
				+t3+"return Short.parseShort(element.getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"int\")) {"+rn
				+t3+"return Integer.parseInt(element.getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"long\")) {"+rn
				+t3+"return Long.parseLong(element.getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"float\")) {"+rn
				+t3+"return Float.parseFloat(element.getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"double\")) {"+rn
				+t3+"return Double.parseDouble(element.getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"String\")) {"+rn
				+t3+"return element.getFirstChild().getNodeValue();"+rn
				+t2+"}"+rn
				+t2+"if(type.equals(\"DataTable\")) {"+rn
				+t3+"return deserializeDataTable(element,new DataTable());"+rn
				+t2+"}"+rn
				+t2+"if(type.startsWith(\"List&lt;\")) {"+rn
				+t3+"return deserializeList(doc);"+rn
				+t2+"}"+rn
				+t2+"//Open Dental object-------------------------------------------------------------------------------------------------"+rn
				+t2+"Object result=deserializeOpenDentalObject(type,doc);"+rn
				+t2+"if(result!=null) {"+rn
				+t3+"return result;"+rn
				+t2+"}"+rn
				+t2+"throw new Exception(\"getDeserializedObject, unsupported type: \"+type);"+rn
				+t+"}"+rn+rn);
			#endregion
			#region DeserializeOpenDentalObject header
			strb.Append(t+"/** Pass in the type and just the xml for that object.  Returns null if no match found. */"+rn
				+t+"private static Object deserializeOpenDentalObject(String type,Document doc) throws Exception {"+rn);
			#endregion
		}

		///<summary></summary>
		private void EndJavaSerial(StringBuilder strb) {
			#region DeserializeOpenDentalObject footer
			strb.Append(t2+"return null;"+rn
				+t+"}"+rn+rn);
			#endregion
			#region DeserializeList
			strb.Append("/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes will be overwritten.  To make changes, go to xCrudGeneratorWebService.Form1.cs and make the changes within StartJavaSerial(), MiddleJavaSerial(), and EndJavaSerial() */"+rn+rn
				+t+"/** Pass in the entire xml response and this method will return a deserialized ArrayList."+rn
				+t+" * @throws Exception Throws exception if the list cannot be deserialized. */"+rn
				+t+"private static Object deserializeList(Document doc) throws Exception {"+rn
				+t2+"// TODO Figure out how to deserialize list objects without reflection here."+rn
				+t2+"throw new Exception(\"deserializeList, error deserializing list.\");"+rn
				+t+"}"+rn+rn);
			#endregion
			#region DeserializeDataTable
			strb.Append(t+"/** Pass in a node from the response and this method will use recursion to digest the entire XML and return a deserialized DataTable. */"+rn
				+t+"private static DataTable deserializeDataTable(Node node,DataTable table) {"+rn
				+t2+"String nodeName=node.getNodeName();"+rn
				+t2+"if(node.getNodeType()!=Node.TEXT_NODE) {"+rn
					+t3+"if(nodeName.equals(\"Name\")) {"+rn
						+t4+"if(node.getChildNodes().getLength()>0) {"+rn
						+t5+"table.setTableName(node.getChildNodes().item(0).getNodeValue());"+rn
						+t4+"}"+rn
						+t4+"else {//Set the table name to an empty string."+rn
						+t5+"table.setTableName(\"\");"+rn
						+t4+"}"+rn
					+t3+"}"+rn
					+t3+"else if(nodeName.equals(\"Col\")) {//Add a new column."+rn
						+t4+"if(node.getChildNodes().getLength()>0) {"+rn
						+t5+"table.Columns.add(new DataColumn(node.getChildNodes().item(0).getNodeValue()));"+rn
						+t4+"}"+rn
					+t3+"}"+rn
					+t3+"else if(nodeName.equals(\"y\")) {//Add a new Row."+rn
						+t4+"table.Rows.add(new DataRow());"+rn
					+t3+"}"+rn
					+t3+"else if(nodeName.equals(\"x\")) {//Add information to the next column in the last row."+rn
						+t4+"DataRow row=table.Rows.get(table.Rows.size()-1);"+rn
						+t4+"if(node.getChildNodes().getLength()>0) {"+rn
						+t5+"row.addCell(node.getChildNodes().item(0).getNodeValue());"+rn
						+t4+"}"+rn
						+t4+"else {//Add an empty cell."+rn
						+t5+"row.addCell(\"\");"+rn
						+t4+"}"+rn
					+t3+"}"+rn
					+t3+"//Recursively iterate through child nodes."+rn
					+t3+"NodeList children=node.getChildNodes();"+rn
					+t3+"for(int i=0;i<children.getLength();i++) {"+rn
						+t4+"Node childNode=children.item(i);"+rn
						+t4+"table=deserializeDataTable(childNode,table);"+rn
					+t3+"}"+rn
				+t2+"}"+rn
				+t2+"return table;"+rn
				+t+"}"+rn+rn);
			#endregion
			#region GetXmlNodeValue
			strb.Append(t+"/** Pass in the xml string parsed into a Document and the desired tagname to attempt to get the value."+rn
				+t+" * Returns the node value or null if node is not included in the Document. */"+rn
				+t+"public static String getXmlNodeValue(Document doc,String tagname) {"+rn
				+t2+"NodeList list=doc.getElementsByTagName(tagname);"+rn
				+t2+"if(list!=null && list.getLength()>0) {"+rn
				+t3+"Node node=list.item(0).getFirstChild();"+rn
				+t3+"if(node!=null) {"+rn
				+t4+"return node.getNodeValue();"+rn
				+t3+"}"+rn
				+t2+"}"+rn
				+t2+"return null;"+rn
				+t+"}"+rn+rn
				+"}"+rn);
			#endregion
		}

		///<summary>Example of className is 'Account' or 'Patient'.</summary>
		private void WriteAllSerializing(StringBuilder strb,string className,FieldInfo[] fields) {
			#region class header
			strb.Append("using System;"+rn
				+"using System.IO;"+rn
				+"using System.Text;"+rn
				+"using System.Xml;"+rn
				+"using System.Drawing;"+rn
				+rn+"namespace OpenDentalWebService {"+rn
				+t+"///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>"+rn
				+t+"public class "+className+" {"+rn);
			#endregion class header
			#region serialize
			strb.Append(rn+t2+"///<summary></summary>"+rn
				+t2+"public static string Serialize(OpenDentBusiness."+className+" "+className.ToLower()+") {"+rn
				+t3+"StringBuilder sb=new StringBuilder();"+rn
				+t3+"sb.Append(\"<"+className+">\");"+rn);
			GetSerialize(strb,className,fields);
			strb.Append(t3+"sb.Append(\"</"+className+">\");"+rn
				+t3+"return sb.ToString();"+rn+t2+"}"+rn);
			#endregion serialize
			#region deserialize
			strb.Append(rn+t2+"///<summary></summary>"+rn
				+t2+"public static OpenDentBusiness."+className+" Deserialize(string xml) {"+rn
				+t3+"OpenDentBusiness."+className+" "+className.ToLower()+"=new OpenDentBusiness."+className+"();"+rn
				+t3+"using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {"+rn
				+t4+"reader.MoveToContent();"+rn
				+t4+"while(reader.Read()) {"+rn
				+t5+"//Only detect start elements."+rn
				+t5+"if(!reader.IsStartElement()) {"+rn
				+t6+"continue;"+rn
				+t5+"}"+rn
				+t5+"switch(reader.Name) {"+rn);
			GetDeserialize(strb,className,fields);
			strb.Append(t5+"}"+rn+t4+"}"+rn+t3+"}"+rn+t3+"return "+className.ToLower()+";"+rn+t2+"}"+rn);
			#endregion deserialize
			#region footer
			strb.Append(rn);
			strb.Append(rn+t+"}"+rn+"}");
			#endregion footer
		}

		///<summary>Create java 's' class files.</summary>
		private void WriteAlljavaDataInterface(StringBuilder strb,string className) {
			if(className=="Document") {
				className=className+"od";
			}
			#region class header
			strb.Append("package com.opendental.odweb.client.datainterface;"+rn
				+rn+"public class "+GetSname(className)+" {"+rn);
			#endregion
			#region footer
			strb.Append(rn+rn+"}");
			#endregion
		}

		///<summary>Create java table type files.</summary>
		private void WriteAlljavaTableTypes(StringBuilder strb,string className,FieldInfo[] fields) {
			String docForjava="";
			if(className=="Document") {
				docForjava="od";
			}
			StringBuilder strbEnums=new StringBuilder();
			StringBuilder strbCopy=new StringBuilder();
			#region class header
			strb.Append("package com.opendental.odweb.client.tabletypes;"+rn
				+rn+"import com.google.gwt.xml.client.Document;"+rn
				+"import com.opendental.odweb.client.remoting.Serializing;"+rn);
			for(int i=0;i<fields.Length;i++) {
				if(fields[i].FieldType.Name=="DateTime") {
					strb.Append("import com.google.gwt.i18n.client.DateTimeFormat;"+rn
						+"import java.util.Date;"+rn);
					break;
				}
			}
			strb.Append(rn+"public class "+className+docForjava+" {"+rn) ;
			#endregion
			#region fields
			foreach(FieldInfo field in fields) {
				if(IsNotDbColumn(field)) {//if not a db column, skip
					continue;
				}
				string summary=GetSummary("F:OpenDentBusiness."+className+"."+field.Name);
				if(summary=="") {
					//this deals with the situation where the new data access layer has public Properties instead of public Fields.
					summary=GetSummary("P:OpenDentBusiness."+className+"."+field.Name);
				}
				strb.Append(t2+"/** "+summary+" */"+rn);
				strb.Append(t2+"public ");
				GetjavaDataType(strb,strbEnums,field);
				strb.Append(" "+field.Name+";"+rn);
				strbCopy.Append(t3+className.ToLower()+docForjava+"."+field.Name+"=this."+field.Name+";"+rn);
			}
			#endregion
			#region copy()
			strb.Append(rn+t2+"/** Deep copy of object. */"+rn
				+t2+"public "+className+docForjava+" deepCopy() {"+rn
				+t3+className+docForjava+" "+className.ToLower()+docForjava+"=new "+className+docForjava+"();"+rn
				+strbCopy.ToString()
				+t3+"return "+className.ToLower()+docForjava+";"+rn
				+t2+"}"+rn);
			#endregion
			#region serialize
			strb.Append(rn+t2+"/** Serialize the object into XML. */"+rn
				+t2+"public String serialize() {"+rn
				+t3+"StringBuilder sb=new StringBuilder();"+rn
				+t3+"sb.append(\"<"+className+docForjava+">\");"+rn);
			GetSerializeForjava(strb,fields);
			strb.Append(t3+"sb.append(\"</"+className+docForjava+">\");"+rn
				+t3+"return sb.toString();"+rn
				+t2+"}"+rn);
			#endregion
			#region deserialize
			strb.Append(rn+t2+"/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values."+rn
				+t2+" * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object."+rn
				+t2+" * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */"+rn
				+t2+"public void deserialize(Document doc) throws Exception {"+rn
				+t3+"try {"+rn);
			GetDeserializeForjava(strb,fields);
			strb.Append(t3+"}"+rn
				+t3+"catch(Exception e) {"+rn
				+t4+"throw e;"+rn
				+t3+"}"+rn
				+t2+"}"+rn);
			#endregion
			#region enums
			strb.Append(rn+strbEnums.ToString());
			#endregion
			#region footer
			strb.Append(rn+"}"+rn);
			#endregion
		}

		///<summary>Create all method call for DtoMethods</summary>
		private void WriteMethodCalls(StringBuilder strb,Type tableType) {
			Assembly wSassembly=Assembly.GetAssembly(typeof(OpenDentalWebService.Accounts));
			Type wSType=null;
			foreach(Type typeClass in wSassembly.GetTypes()) {
				if(typeClass.Name==GetSname(tableType.Name)) {
					wSType=typeClass;
				}
			}
			if(wSType==null) {
				return;
			}
			#region method header
			strb.Append(t2+"///<summary></summary>"+rn
				+t2+"private static object Method"+wSType.Name+"(string methodName,List<object> parameters) {"+rn
				+t3+"//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes."+rn);
			#endregion
			#region MethodCalls
			MethodInfo[] methodInfos=wSType.GetMethods();
			foreach(MethodInfo methodInfo in methodInfos) {
				if(!methodInfo.IsStatic) {
					continue;
				}
				strb.Append(t3+"if(methodName==\""+methodInfo.Name+"\") {"+rn
					+t4);
				if(methodInfo.ReturnType.Name!="Void") {
					strb.Append("return ");
				}
				strb.Append(GetSname(tableType.Name)+"."+methodInfo.Name+"(");
				ParameterInfo[] paramInfos=methodInfo.GetParameters();
				for(int i=0;i<paramInfos.Length;i++) {
					if(i>0) {
						strb.Append(",");
					}
					//strb.Append("("+paramInfos[i].ParameterType.FullName.ToString()+")parameters["+i+"]");
					strb.Append(GetParameterStringForMethodCall(paramInfos[i].ParameterType,i));
				}
				strb.Append(");"+rn);
				if(methodInfo.ReturnType.Name=="Void") {
					strb.Append(t4+"return null;"+rn);
				}
				strb.Append(t3+"}"+rn);
			}
			#endregion
			#region method footer
			strb.Append(t3+"throw new NotSupportedException(\"MethodAccounts, unknown method: \"+methodName);"+rn
				+t2+"}"+rn+rn);
			#endregion
		}

		///<summary>Helper function mainly for primitive casting.  A typical result will look like this: "(OpenDentBusiness.Account)parameters[1]"</summary>
		private string GetParameterStringForMethodCall(Type type,int i) {
			string fullName=type.FullName.ToString();
			switch(fullName) {
				case "System.Boolean":
					return "Convert.ToBoolean(parameters["+i+"])";
				case "System.Byte":
					return "Convert.ToByte(parameters["+i+"])";
				case "System.Char":
					return "Convert.ToChar(parameters["+i+"])";
				case "System.Decimal":
					return "Convert.ToDecimal(parameters["+i+"])";
				case "System.Double":
					return "Convert.ToDouble(parameters["+i+"])";
				case "System.Single":
					return "Convert.ToSingle(parameters["+i+"])";
				case "System.Int32":
					return "Convert.ToInt32(parameters["+i+"])";
				case "System.Int64":
					return "Convert.ToInt64(parameters["+i+"])";
				case "System.SByte":
					return "Convert.ToSByte(parameters["+i+"])";
				case "System.Int16":
					return "Convert.ToInt16(parameters["+i+"])";
				case "System.String":
					return "Convert.ToString(parameters["+i+"])";
				case "System.UInt32":
					return "Convert.ToUInt32(parameters["+i+"])";
				case "System.UInt64":
					return "Convert.ToUInt64(parameters["+i+"])";
				case "System.UInt16":
					return "Convert.ToUInt16(parameters["+i+"])";
				default://Not a primitive.
					return "("+fullName+")parameters["+i+"]";
			}
		}

		///<summary>Fill Serialize</summary>
		private void GetSerialize(StringBuilder strb,string className,FieldInfo[] fields) {
			foreach(FieldInfo field in fields) {
				if(IsNotDbColumn(field)) {//if not a db column, skip
					continue;
				}
				strb.Append(t3+"sb.Append(\"<"+field.Name+">\").Append(");
				if(field.FieldType.IsEnum) {
					strb.Append("(int)"+className.ToLower()+"."+field.Name+").Append(\"</"+field.Name+">\");"+rn);
					continue;
				}
				switch(field.FieldType.Name) {
					case "Byte":
					case "Double":
					case "Interval": //intervals are stored in db as int
					case "Int32": //int
					case "Int64": //long
					case "Single": //float
						strb.Append(className.ToLower()+"."+field.Name+").Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "Boolean":
						strb.Append("("+className.ToLower()+"."+field.Name+")?1:0).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "Color":
						strb.Append(className.ToLower()+"."+field.Name+".ToArgb()).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "DateTime":
						strb.Append(className.ToLower()+"."+field.Name+".ToString(\"yyyyMMddHHmmss\")).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "String":
						strb.Append("SerializeStringEscapes.EscapeForXml("+className.ToLower()+"."+field.Name+")).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "TimeSpan":
						strb.Append(className.ToLower()+"."+field.Name+".ToString()).Append(\"</"+field.Name+">\");"+rn);
						continue;
				}
			}
		}

		///<summary></summary>
		private void GetDeserialize(StringBuilder strb,string className,FieldInfo[] fields) {
			foreach(FieldInfo field in fields) {
				if(IsNotDbColumn(field)) {//if not a db column, skip
					continue;
				}
				strb.Append(t6+"case \""+field.Name+"\":"+rn
					+t7+className.ToLower()+"."+field.Name+"=");
				if(field.FieldType.IsEnum) {
					strb.Append("(OpenDentBusiness."+field.FieldType.Name+")System.Convert.ToInt32(reader.ReadContentAsString());"+rn
						+t7+"break;"+rn);
					continue;
				}
				switch(field.FieldType.Name) {
					case "Boolean":
						strb.Append("reader.ReadContentAsString()!=\"0\";"+rn
							+t7+"break;"+rn);
						continue;
					case "Byte":
						strb.Append("System.Convert.ToByte(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					case "Color":
						strb.Append("Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));"+rn
							+t7+"break;"+rn);
						continue;
					case "DateTime":
						strb.Append("DateTime.ParseExact(reader.ReadContentAsString(),\"yyyyMMddHHmmss\",null);"+rn
							+t7+"break;"+rn);
						continue;
					case "Double":
						strb.Append("System.Convert.ToDouble(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					case "Int32": //int
						strb.Append("System.Convert.ToInt32(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					case "Int64": //long
						strb.Append("System.Convert.ToInt64(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					case "Interval":
						strb.Append("new OpenDentBusiness.Interval(System.Convert.ToInt32(reader.ReadContentAsString()));"+rn
							+t7+"break;"+rn);
						continue;
					case "Single": //float
						strb.Append("System.Convert.ToSingle(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					case "String":
						strb.Append("reader.ReadContentAsString();"+rn
							+t7+"break;"+rn);
						continue;
					case "TimeSpan":
						strb.Append("TimeSpan.Parse(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
				}
			}
		}

		///<summary></summary>
		private void GetSerializeForjava(StringBuilder strb,FieldInfo[] fields) {
			foreach(FieldInfo field in fields) {
				if(IsNotDbColumn(field)) {//if not a db column, skip
					continue;
				}
				strb.Append(t3+"sb.append(\"<"+field.Name+">\").append(");
				if(field.FieldType.BaseType.Name=="Enum") {
					strb.Append(field.Name+".ordinal()).append(\"</"+field.Name+">\");"+rn);
					continue;
				}
				switch(field.FieldType.Name) {
					case "Boolean":
						strb.Append("("+field.Name+")?1:0");
						break;
					case "DateTime":
						strb.Append("DateTimeFormat.getFormat(\"yyyyMMddHHmmss\").format("+field.Name+")");
						break;
					case "String":
					case "TimeSpan":
						strb.Append("Serializing.escapeForXml("+field.Name+")");
						break;
					default:
						strb.Append(field.Name);
						break;
				}
				strb.Append(").append(\"</"+field.Name+">\");"+rn);
			}
		}

		///<summary></summary>
		private void GetDeserializeForjava(StringBuilder strb,FieldInfo[] fields) {
			foreach(FieldInfo field in fields) {
				if(IsNotDbColumn(field)) {//if not a db column, skip
					continue;
				}
				string ser="Serializing.getXmlNodeValue(doc,\""+field.Name+"\")";
				strb.Append(t4+"if("+ser+"!=null) {"+rn
					+t5+field.Name+"=");
				if(field.FieldType.BaseType.Name=="Enum") {
					strb.Append(field.FieldType.Name+".values()[Integer.valueOf("+ser+")];"+rn
						+t4+"}"+rn);
					continue;
				}
				switch(field.FieldType.Name) {
					case "Color":
					case "Int32":
					case "Int64":
					case "Interval":
						strb.Append("Integer.valueOf("+ser+");"+rn);
						break;
					case "Boolean":
						strb.Append("("+ser+"==\"0\")?false:true;"+rn);
						break;
					case "Byte":
						strb.Append("Byte.valueOf("+ser+");"+rn);
						break;
					case "DateTime":
						strb.Append("DateTimeFormat.getFormat(\"yyyyMMddHHmmss\").parseStrict("+ser+");"+rn);
						break;
					case "Double":
						strb.Append("Double.valueOf("+ser+");"+rn);
						break;
					case "Single":
						strb.Append("Float.valueOf("+ser+");"+rn);
						break;
					default:
						strb.Append(ser+";"+rn);
						break;
				}
				strb.Append(t4+"}"+rn);
			}
		}

		///<summary>Normally false</summary>
		private bool IsNotDbColumn(FieldInfo field) {
			object[] attributes = field.GetCustomAttributes(typeof(CrudColumnAttribute),true);
			if(attributes.Length==0) {
				return false;
			}
			return ((CrudColumnAttribute)attributes[0]).IsNotDbColumn;
		}

		private static string GetSname(string typeClassName) {
			string Sname=typeClassName;
			if(typeClassName=="Etrans") {
				return "Etranss";
			}
			if(typeClassName=="Language") {
				return "Lans";
			}
			if(Sname.EndsWith("s")) {
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ch")) {
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ay")) {
				Sname=Sname+"s";
			}
			else if(Sname.EndsWith("ey")) {//eg key
				Sname=Sname+"s";
			}
			else if(Sname.EndsWith("y")) {
				Sname=Sname.TrimEnd('y')+"ies";
			}
			else {
				Sname=Sname+"s";
			}
			return Sname;
		}

		///<summary>Gets the summary from the xml file.  The full and correct member name must be supplied.</summary>
		private string GetSummary(string member) {
			XPathNavigator navOne=Navigator.SelectSingleNode("//member[@name='"+member+"']");
			if(navOne==null) {
				return "";
			}
			XPathNavigator nav=navOne.SelectSingleNode("summary");
			if(nav==null) {
				return "";
			}
			return navOne.SelectSingleNode("summary").Value;
		}

		///<summary>All of the primitive/general types handled by serialization.  Any new primitive/general class should be manually added to this section of the crud.</summary>
		private void GetPrimGenSerializerTypes(StringBuilder strb) {
			strb.Append(t3+"switch(objectType) {"+rn
				+t4+@"case ""System.Int32"":"+rn
				+t4+@"case ""System.Int64"":"+rn     //long
				+t4+@"case ""System.Boolean"":"+rn
				+t4+@"case ""System.String"":"+rn
				+t4+@"case ""System.Char"":"+rn
				+t4+@"case ""System.Single"":"+rn    //float
				+t4+@"case ""System.Byte"":"+rn
				+t4+@"case ""System.Double"":"+rn
				+t4+@"case ""DataT"":"+rn
				+t5+"return aaGeneralTypes.Serialize(objectType,obj);"+rn
				+t3+"}"+rn);
			//Lists.
			strb.Append(t3+"if(objectType.StartsWith(\"List<\")) {//Lists."+rn
				+t4+"return aaGeneralTypes.Serialize(objectType,obj);"+rn
				+t3+"}"+rn);
			//Arrays.
			strb.Append(t3+"if(objectType.Contains(\"[\")) {//Arrays."+rn
				+t4+"return aaGeneralTypes.Serialize(objectType,obj);"+rn
				+t3+"}"+rn);
		}

		///<summary>All of the primitive/general types handled by deserialization.  Any new primitive/general class should be manually added to this section of the crud.</summary>
		private void GetPrimGenDeserializerTypes(StringBuilder strb) {
			strb.Append(t3+"switch(typeName) {"+rn
				+t4+@"case ""int"":"+rn
				+t4+@"case ""long"":"+rn
				+t4+@"case ""bool"":"+rn
				+t4+@"case ""string"":"+rn
				+t4+@"case ""char"":"+rn
				+t4+@"case ""float"":"+rn
				+t4+@"case ""byte"":"+rn
				+t4+@"case ""double"":"+rn
				+t4+@"case ""List&lt;"":"+rn
				+t5+"return aaGeneralTypes.Deserialize(typeName,xml);"+rn
				+t3+"}"+rn);
			//Arrays.
			strb.Append(t3+"if(typeName.Contains(\"[\")) {//Arrays."+rn
				+t4+"return aaGeneralTypes.Deserialize(typeName,xml);"+rn
				+t3+"}"+rn);
		}

		///<summary></summary>
		private void GetjavaDataType(StringBuilder strb,StringBuilder strbEnums,FieldInfo field) {
			if(field.FieldType.BaseType.Name=="Enum") {
				strb.Append(field.FieldType.Name);
				if(strbEnums.ToString().Contains(field.FieldType.Name)) {
					return;
				}
				string summary=GetSummary("T:OpenDentBusiness."+field.FieldType.Name);
				strbEnums.Append(t2+"/** "+summary+" */"+rn
					+t2+"public enum "+field.FieldType.Name+" {"+rn);
				string[] enumNames=field.FieldType.GetEnumNames();
				for(int i=0;i<enumNames.Length;i++) {
					if(i>0) {
						strbEnums.Append(","+rn);
					}
					summary=GetSummary("F:OpenDentBusiness."+field.FieldType.Name+"."+enumNames[i]);
					strbEnums.Append(t3+"/** "+summary+" */"+rn
						+t3+enumNames[i]);
				}
				strbEnums.Append(rn+t2+"}"+rn+rn);
				return;
			}
			switch(field.FieldType.Name) {
				case "Color":
				case "Int32":
				case "Int64":
				case "Interval":
					strb.Append("int");
					break;
				case "Boolean":
					strb.Append("boolean");
					break;
				case "Byte":
					strb.Append("byte");
					break;
				case "DateTime":
					strb.Append("Date");
					break;
				case "Double":
					strb.Append("double");
					break;
				case "Single":
					strb.Append("float");
					break;
				case "String":
				case "TimeSpan":
					strb.Append("String");
					break;
			}
		}
	}
}
