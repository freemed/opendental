using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace xCrudGeneratorWebService {
	public partial class Form1:Form {
		private string SerialDir;
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
			TableTypes=new List<Type>();
			Type typeTableBase=typeof(TableBase);
			Assembly assembly=Assembly.GetAssembly(typeTableBase);
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
			string className;
			for(int i=0;i<TableTypes.Count;i++) {
				className=TableTypes[i].Name;
				FieldInfo[] fields=null;
				fields=TableTypes[i].GetFields();
				strb=new StringBuilder();
				WriteAll(strb,className,fields);
				File.WriteAllText(Path.Combine(SerialDir,className+".cs"),strb.ToString());
				Application.DoEvents();
			}
			Cursor=Cursors.Default;
			MessageBox.Show("Done");
		}

		///<summary>Example of className is 'Account' or 'Patient'.</summary>
		private void WriteAll(StringBuilder strb,string className,FieldInfo[] fields) {
			#region class header
			strb.Append(@"using System;"+rn
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
			strb.Append(@"
	}
}");
			#endregion footer
		}

		///<summary>Fill Serialize</summary>
		private StringBuilder GetSerialize(StringBuilder strb,string className,FieldInfo[] fields) {
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
						strb.Append(className.ToLower()+"."+field.Name+".ToLongDateString()).Append(\"</"+field.Name+">\");"+rn);
						continue;
					default:
						continue;
				}
			}
			return strb;
		}

		///<summary>Normally false</summary>
		private bool IsNotDbColumn(FieldInfo field) {
			object[] attributes = field.GetCustomAttributes(typeof(CrudColumnAttribute),true);
			if(attributes.Length==0) {
				return false;
			}
			return ((CrudColumnAttribute)attributes[0]).IsNotDbColumn;
		}

		///<summary></summary>
		private StringBuilder GetDeserialize(StringBuilder strb,string className,FieldInfo[] fields) {
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
						strb.Append("DateTime.Parse(reader.ReadContentAsString());"+rn
							+t7+"break;"+rn);
						continue;
					default:
						continue;
				}
			}
			return strb;
		}

	}
}
