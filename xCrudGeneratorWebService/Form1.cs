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
				WriteAlljavaDataInterface(strb,className);
				File.WriteAllText(Path.Combine(JavaSClassesDir,GetSname(className+docForjava)+".java"),strb.ToString());
				strb.Clear();
				WriteAlljavaTableTypes(strb,className,fields);
				File.WriteAllText(Path.Combine(JavaTableTypesDir,className+docForjava+".java"),strb.ToString());
				#region DtoMethods OpenDentalClasses
				strbDtoMethods.Append(t4+"if(typeName==\"OpenDentBusiness."+className+"\") {"+rn
					+t5+"return "+className+".Deserialize(xml);"+rn
					+t4+"}"+rn);
				#endregion
				#region CallMethod 
				strbCallMethod.Append(t3+"if(className==\""+GetSname(className)+"\") {"+rn
					+t4+"return Method"+GetSname(className)+"(methodName,parameters);"+rn
					+t3+"}"+rn);
				#endregion
				WriteMethodCalls(strbMethodCalls,TableTypes[i]);
				#region GetSerialized
				strbJavaGetSerialized.Append(t2+"if(qualifiedName==\"com.opendental.odweb.client.tabletypes."+className+docForjava+"\") {"+rn
					+t3+"return (("+className+docForjava+")obj).SerializeToXml();"+rn
					+t2+"}"+rn);
				#endregion
				#region GetDeserialized
				strbJavaGetDeserialized.Append(t2+"if(type==\""+className+docForjava+"\") {"+rn
					+t3+className+docForjava+" "+className.ToLower()+docForjava+"=new "+className+docForjava+"();"+rn
					+t3+className.ToLower()+docForjava+".DeserializeFromXml(xml);"+rn
					+t3+"return "+className.ToLower()+docForjava+";"+rn
					+t2+"}"+rn);
				#endregion
				Application.DoEvents();
			}
			#endregion
			#region EndCallClassDeserializer
			strbDtoMethods.Append(t4+"#endregion"+rn
				+t3+"}"+rn
				+t3+"catch {"+rn
				+t4+"throw new Exception(\"CallClassDeserializer, error deserializing class type: \"+typeName);"+rn
				+t3+"}"+rn
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
			#region CallClassDeserializer
			strb.Append(t2+"///<summary>Calls the classes deserializer based on the typeName passed in.  Mainly used for deserializing parameters on DtoObjects.  Throws exceptions.</summary>"+rn
				+t2+"public static object CallClassDeserializer(string typeName,string xml) {"+rn
				+t3+"try {"+rn
				+t4+"#region Primitive and General Types"+rn
				+t4+"//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenTypes and manually add it there."+rn);
			GetPrimGenTypes(strb);
			strb.Append(t4+"#endregion"+rn
				+t4+"#region Open Dental Classes"+rn);
			#endregion
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
			strb.Append("package com.opendental.odweb.client.remoting;"+rn
				+rn+"import com.google.gwt.xml.client.Document;"+rn
				+"import com.google.gwt.xml.client.XMLParser;"+rn
				+"import com.opendental.odweb.client.tabletypes.*;"+rn+rn
				+"/** Do not make changes to this file.  This class is automatically generated by the CRUD, any changes made will be overwritten. */"+rn
				+"public class Serializing {"+rn+rn);
			#endregion
			#region EscapeForXml
			strb.Append(t+"/** Escapes common characters used in XML. */"+rn
				+t+"public static String EscapeForXml(String myString) {"+rn
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
				+t+" * @throws Exception Throws exception if type is not yet supported. */"+rn
				+t+"public static String GetSerializedObject(Object obj) throws Exception {"+rn
				+t2+"String result;"+rn
				+t2+"//Figure out what type of object we're dealing with and return the serialized form."+rn
				+t2+"String qualifiedName=obj.getClass().getName();//Ex: ArrayList = \"java.util.ArrayList\""+rn
				+t2+"//Primitives--------------------------------------------------------------------------------------------------------"+rn
				+t2+"if(qualifiedName==\"Z\") {//boolean \"Z\""+rn
				+t3+"result=(boolean)obj?\"1\":\"0\";"+rn
				+t3+"return \"<Obj><bool>\"+result+\"</bool></Obj>\";"+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"B\") {//byte    \"B\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"C\") {//char    \"C\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"S\") {//short   \"S\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"I\") {//int     \"I\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"J\") {//long    \"J\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"F\") {//float   \"F\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"D\") {//double  \"D\""+rn
				+rn
				+t2+"}"+rn
				+t2+"if(qualifiedName==\"java.lang.String\") {//String  \"java.lang.String\""+rn
				+rn
				+t2+"}"+rn
				+t2+"//Arrays------------------------------------------------------------------------------------------------------------"+rn
				+t2+"//Multidimensional arrays have equal number of brackets. Ex: Account[][] = [[L..."+rn
				+t2+"//Object[]  \"[Lcom.opendental.odweb.client.tabletypes.Account;\" from Account[]"+rn
				+t2+"//int[]     \"[I\""+rn
				+t2+"//String[]  \"[Ljava.lang.String;\""+rn
				+t2+"//Open Dental Objects-----------------------------------------------------------------------------------------------"+rn);

				//+t2+"if(qualifiedName==\"com.opendental.odweb.client.tabletypes.Account\") {"+rn
				//+t3+"return ((Account)obj).SerializeToXml();"+rn
				//+t2+"}"+rn


			#endregion
		}

		///<summary></summary>
		private void MiddleJavaSerial(StringBuilder strb) {
			#region GetSerializedObject footer
			strb.Append(t2+"throw new Exception(\"GetSerializedObject, unsupported type: \"+qualifiedName);"+rn
				+t+"}"+rn+rn);
			#endregion
			#region GetDeserializedObject
			strb.Append(t+"/** Loops through all the known objects and calls the corresponding classes deserialize method."+rn
				+t+" * @throws Exception Throws exception if type is not yet supported or if a DtoException was returned. */"+rn
				+t+"public static Object GetDeserializedObject(String xml) throws Exception {"+rn
				+t2+"Document doc=XMLParser.parse(xml);"+rn
				+t2+"String type=\"\";// TODO Figure out the response type.  Response examples: <long>4</long> OR <DtoException><msg>Error</msg></DtoException>"+rn
				+t2+"if(type==\"DtoException\") {//Check for exceptions first."+rn
				+t3+"//Read the \"msg\" node and throw an exception with that error message."+rn
				+t3+"throw new Exception(doc.getElementsByTagName(\"msg\").item(0).getFirstChild().getNodeValue());"+rn
				+t2+"}"+rn
				+t2+"//Primitives-------------------------------------------------------------------------------------------------------"+rn
				+t2+"if(type==\"boolean\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"byte\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"char\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"short\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"int\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"long\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"float\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"double\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"String\") {"+rn
				+rn
				+t2+"}"+rn
				+t2+"if(type==\"DataT\") {"+rn
				+t3+"return DeserializeDataTable(xml);"+rn
				+t2+"}"+rn
				+t2+"if(type.startsWith(\"List&lt;\")) {"+rn
				+t3+"return DeserializeList(xml);"+rn
				+t2+"}"+rn
				+t2+"//Open Dental object-------------------------------------------------------------------------------------------------"+rn
				+t2+"Object result=DeserializeOpenDentalObject(type,xml);"+rn
				+t2+"if(result!=null) {"+rn
				+t3+"return result;"+rn
				+t2+"}"+rn
				+t2+"throw new Exception(\"GetDeserializedObject, unsupported type: \"+type);"+rn
				+t+"}"+rn+rn);
			#endregion
			#region DeserializeOpenDentalObject header
			strb.Append(t+"/** Pass in the type and just the xml for that object.  Returns null if no match found. */"+rn
				+t+"private static Object DeserializeOpenDentalObject(String type,String xml) throws Exception {"+rn);
			#endregion
		}

		///<summary></summary>
		private void EndJavaSerial(StringBuilder strb) {
			#region DeserializeOpenDentalObject footer
			strb.Append(t2+"return null;"+rn
				+t+"}"+rn+rn);
			#endregion
			#region DeserializeList
			strb.Append(t+"/** Pass in the entire xml response and this method will return a deserialized ArrayList."+rn
				+t+" * @throws Exception Throws exception if the list cannot be deserialized. */"+rn
				+t+"private static Object DeserializeList(String xml) throws Exception {"+rn
				+t2+"// TODO Figure out how to deserialize list objects without reflection here."+rn
				+t2+"throw new Exception(\"DeserializeList, error deserializing list.\");"+rn
				+t+"}"+rn+rn);
			#endregion
			#region DeserializeDataTable
			strb.Append(t+"/** Pass in the entire xml response and this method will return a deserialized ArrayList."+rn
				+t+" * @throws Exception Throws exception if the list cannot be deserialized. */"+rn
				+t+"private static Object DeserializeDataTable(String xml) throws Exception {"+rn
				+t2+"// TODO Figure out how to deserialize data table objects here."+rn
				+t2+"throw new Exception(\"DeserializeDataTable, error deserializing data table.\");"+rn
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
				+"import com.google.gwt.xml.client.XMLParser;"+rn
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
				+t2+"public "+className+docForjava+" Copy() {"+rn
				+t3+className+docForjava+" "+className.ToLower()+docForjava+"=new "+className+docForjava+"();"+rn
				+strbCopy.ToString()
				+t3+"return "+className.ToLower()+docForjava+";"+rn
				+t2+"}"+rn);
			#endregion
			#region serialize
			strb.Append(rn+t2+"/** Serialize the object into XML. */"+rn
				+t2+"public String SerializeToXml() {"+rn
				+t3+"StringBuilder sb=new StringBuilder();"+rn
				+t3+"sb.append(\"<"+className+docForjava+">\");"+rn);
			GetSerializeForjava(strb,fields);
			strb.Append(t3+"sb.append(\"</"+className+docForjava+">\");"+rn
				+t3+"return sb.toString();"+rn
				+t2+"}"+rn);
			#endregion
			#region deserialize
			strb.Append(rn+t2+"/** Sets the variables for this object based on the values from the XML."+rn
				+t2+" * @param xml The XML passed in must be valid and contain a node for every variable on this object."+rn
				+t2+" * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */"+rn
				+t2+"public void DeserializeFromXml(String xml) throws Exception {"+rn
				+t3+"try {"+rn
				+t4+"Document doc=XMLParser.parse(xml);"+rn);
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
					strb.Append("("+paramInfos[i].ParameterType.FullName.ToString()+")parameters["+i+"]");
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
					case "Int32": //int
					case "Int64": //long
					case "Single": //float
					case "Double":
					case "Interval": //intervals are stored in db as int
						strb.Append(className.ToLower()+"."+field.Name+").Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "String":
						strb.Append("SerializeStringEscapes.EscapeForXml("+className.ToLower()+"."+field.Name+")).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "Boolean":
						strb.Append("("+className.ToLower()+"."+field.Name+")?1:0).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "Color":
						strb.Append(className.ToLower()+"."+field.Name+".ToArgb()).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "TimeSpan":
						strb.Append(className.ToLower()+"."+field.Name+".ToString()).Append(\"</"+field.Name+">\");"+rn);
						continue;
					case "DateTime":
						strb.Append(className.ToLower()+"."+field.Name+".ToString(\"yyyyMMddHHmmss\")).Append(\"</"+field.Name+">\");"+rn);
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
					strb.Append("(OpenDentBusiness."+field.FieldType.Name+")reader.ReadContentAsInt();"+rn
						+t7+"break;"+rn);
					continue;
				}
				switch(field.FieldType.Name) {
					case "Byte":
						strb.Append("(byte)reader.ReadContentAsInt();"+rn
							+t7+"break;"+rn);
						continue;
					case "Interval":
						strb.Append("new OpenDentBusiness.Interval(reader.ReadContentAsInt());"+rn
							+t7+"break;"+rn);
						continue;
					case "Int32": //int
						strb.Append("reader.ReadContentAsInt();"+rn
							+t7+"break;"+rn);
						continue;
					case "Int64": //long
						strb.Append("reader.ReadContentAsLong();"+rn
							+t7+"break;"+rn);
						continue;
					case "Single": //float
						strb.Append("reader.ReadContentAsFloat();"+rn
							+t7+"break;"+rn);
						continue;
					case "Double":
						strb.Append("reader.ReadContentAsDouble();"+rn
							+t7+"break;"+rn);
						continue;
					case "String":
						strb.Append("reader.ReadContentAsString();"+rn
							+t7+"break;"+rn);
						continue;
					case "Boolean":
						strb.Append("reader.ReadContentAsString()!=\"0\";"+rn
							+t7+"break;"+rn);
						continue;
					case "Color":
						strb.Append("Color.FromArgb(reader.ReadContentAsInt());"+rn
							+t7+"break;"+rn);
						continue;
					case "TimeSpan":
						strb.Append("TimeSpan.Parse(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					case "DateTime":
						strb.Append("DateTime.ParseExact(reader.ReadContentAsString(),\"yyyyMMddHHmmss\",null);"+rn
							+t7+"break;"+rn);
						continue;
					default:
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
					case "String":
					case "TimeSpan":
						strb.Append("Serializing.EscapeForXml("+field.Name+")");
						break;
					case "Boolean":
						strb.Append("("+field.Name+")?1:0");
						break;
					case "DateTime":
						strb.Append("DateTimeFormat.getFormat(\"yyyyMMddHHmmss\").format(AptDateTime)");
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
				strb.Append(t4+field.Name+"=");
				if(field.FieldType.BaseType.Name=="Enum") {
					strb.Append(field.FieldType.Name+".values()[Integer.valueOf(doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue())];"+rn);
					continue;
				}
				switch(field.FieldType.Name) {
					case "Int32":
					case "Int64":
					case "Interval":
					case "Color":
						strb.Append("Integer.valueOf(doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue());"+rn);
						continue;
					case "Byte":
						strb.Append("Byte.valueOf(doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue());"+rn);
						continue;
					case "Single":
						strb.Append("Float.valueOf(doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue());"+rn);
						continue;
					case "Double":
						strb.Append("Double.valueOf(doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue());"+rn);
						continue;
					case "Boolean":
						strb.Append("(doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue()==\"0\")?false:true;"+rn);
						continue;
					case "DateTime":
						strb.Append("DateTimeFormat.getFormat(\"yyyyMMddHHmmss\").parseStrict(doc.getElementsByTagName(\""+field.Name+"\")."
							+"item(0).getFirstChild().getNodeValue());"+rn);
						continue;
					default:
						strb.Append("doc.getElementsByTagName(\""+field.Name+"\").item(0).getFirstChild().getNodeValue();"+rn);
						continue;
				}
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

		///<summary>All of the primitive/general types handled by serialization/deserialization.  Any new primitive/general class should be manually added to this section of the crud.</summary>
		private void GetPrimGenTypes(StringBuilder strb) {
			strb.Append(t4+"if(typeName==\"long\") {"+rn
				+t5+"return aaGeneralTypes.Deserialize(typeName,xml);"+rn
				+t4+"}"+rn);
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
				case "Int32":
				case "Int64":
				case "Interval":
				case "Color":
					strb.Append("int");
					break;
				case "Byte":
					strb.Append("byte");
					break;
				case "Single":
					strb.Append("float");
					break;
				case "Double":
					strb.Append("double");
					break;
				case "String":
				case "TimeSpan":
					strb.Append("String");
					break;
				case "Boolean":
					strb.Append("boolean");
					break;
				case "DateTime":
					strb.Append("Date");
					break;
			}
		}
	}
}
