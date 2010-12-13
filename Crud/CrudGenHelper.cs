using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace Crud {
	public class CrudGenHelper {
		///<summary>Will throw exception if no primary key attribute defined.</summary>
		public static FieldInfo GetPriKey(FieldInfo[] fields,string tableName){
			for(int i=0;i<fields.Length;i++) {
				object[] attributes = fields[i].GetCustomAttributes(typeof(CrudColumnAttribute),true);
				if(attributes.Length!=1) {
					continue;
				}
				if(((CrudColumnAttribute)attributes[0]).IsPriKey) {
					return fields[i];
				}
			}
			throw new ApplicationException("No primary key defined for "+tableName);
		}

		///<summary>Will throw exception if no primary key attribute defined.</summary>
		public static FieldInfo GetPriKeyMobile1(FieldInfo[] fields,string tableName) {
			for(int i=0;i<fields.Length;i++) {
				object[] attributes = fields[i].GetCustomAttributes(typeof(CrudColumnAttribute),true);
				if(attributes.Length!=1) {
					continue;
				}
				if(((CrudColumnAttribute)attributes[0]).IsPriKeyMobile1) {
					return fields[i];
				}
			}
			throw new ApplicationException("No primary key 1 defined for "+tableName);
		}

		///<summary>Will throw exception if no primary key attribute defined.</summary>
		public static FieldInfo GetPriKeyMobile2(FieldInfo[] fields,string tableName) {
			for(int i=0;i<fields.Length;i++) {
				object[] attributes = fields[i].GetCustomAttributes(typeof(CrudColumnAttribute),true);
				if(attributes.Length!=1) {
					continue;
				}
				if(((CrudColumnAttribute)attributes[0]).IsPriKeyMobile2) {
					return fields[i];
				}
			}
			throw new ApplicationException("No primary key 2 defined for "+tableName);
		}

		///<summary>The name of the table in the database.  By default, the lowercase name of the class type.</summary>
		public static string GetTableName(Type typeClass) {
			object[] attributes = typeClass.GetCustomAttributes(typeof(CrudTableAttribute),true);
			if(attributes.Length==0) {
				return typeClass.Name.ToLower();
			}
			for(int i=0;i<attributes.Length;i++) {
				if(attributes[i].GetType()!=typeof(CrudTableAttribute)) {
					continue;
				}
				if(((CrudTableAttribute)attributes[i]).TableName!="") {
					return((CrudTableAttribute)attributes[i]).TableName;
				}
			}
			//couldn't find any override.
			return typeClass.Name.ToLower();
		}

		///<summary></summary>
		public static bool IsDeleteForbidden(Type typeClass) {
			object[] attributes = typeClass.GetCustomAttributes(typeof(CrudTableAttribute),true);
			if(attributes.Length==0) {
				return false;
			}
			for(int i=0;i<attributes.Length;i++) {
				if(attributes[i].GetType()!=typeof(CrudTableAttribute)) {
					continue;
				}
				if(((CrudTableAttribute)attributes[i]).IsDeleteForbidden) {
					return true;
				}
			}
			//couldn't find any.
			return false;
		}

		///<summary></summary>
		public static bool IsMissingInGeneral(Type typeClass) {
			object[] attributes = typeClass.GetCustomAttributes(typeof(CrudTableAttribute),true);
			if(attributes.Length==0) {
				return false;
			}
			for(int i=0;i<attributes.Length;i++) {
				if(attributes[i].GetType()!=typeof(CrudTableAttribute)) {
					continue;
				}
				if(((CrudTableAttribute)attributes[i]).IsMissingInGeneral) {
					return true;
				}
			}
			//couldn't find any.
			return false;
		}

		///<summary></summary>
		public static bool IsMobile(Type typeClass) {
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

		///<summary>For Mobile, this only excludes PK2; result includes PK1, the CustomerNum.  Always excludes fields that are not in the database, like patient.Age.</summary>
		public static List<FieldInfo> GetFieldsExceptPriKey(FieldInfo[] fields,FieldInfo priKey) {
			List<FieldInfo> retVal=new List<FieldInfo>();
			for(int i=0;i<fields.Length;i++) {
				if(fields[i].Name==priKey.Name) {
					continue;
				}
				if(IsNotDbColumn(fields[i])){
					continue;
				}
				retVal.Add(fields[i]);
			}
			return retVal;
		}

		///<summary>This only excludes fields that are not in the database, like patient.Age.</summary>
		public static List<FieldInfo> GetFieldsExceptNotDb(FieldInfo[] fields) {
			List<FieldInfo> retVal=new List<FieldInfo>();
			for(int i=0;i<fields.Length;i++) {
				if(IsNotDbColumn(fields[i])){
					continue;
				}
				retVal.Add(fields[i]);
			}
			return retVal;
		}

		///<summary>This gets all new fields which are found in the table definition but not in the database.  Result will be empty if the table itself is not in the database.</summary>
		public static List<FieldInfo> GetNewFields(FieldInfo[] fields,Type typeClass,string dbName) {
			string tablename=GetTableName(typeClass);
			string command="SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_schema = '"+dbName+"' AND table_name = '"+tablename+"'";
			if(DataCore.GetScalar(command)!="1") {
				return new List<FieldInfo>();
			}
			command="SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS "
				+"WHERE table_name = '"+tablename+"' AND table_schema = '"+dbName+"'";
			DataTable table=DataCore.GetTable(command);
			List<FieldInfo> retVal=new List<FieldInfo>();
			for(int i=0;i<fields.Length;i++) {
				if(IsNotDbColumn(fields[i])) {
					continue;
				}
				bool found=false; ;
				for(int t=0;t<table.Rows.Count;t++) {
					if(table.Rows[t]["COLUMN_NAME"].ToString().ToLower()==fields[i].Name.ToLower()) {
						found=true;
					}
				}
				if(!found) {
					retVal.Add(fields[i]);
				}
			}
			return retVal;
		}

		///<summary>Pass in fields processed by GetFieldsExceptPriKey.  This quick method returns the bigint fields so that indexes can possibly be added.  For mobile, pass in the priKeyName2 so that it can be excluded.  If not mobile, then set it to null.</summary>
		public static List<FieldInfo> GetBigIntFields(List<FieldInfo> fieldsExceptPri,string priKeyName2) {
			List<FieldInfo> retVal=new List<FieldInfo>();
			for(int i=0;i<fieldsExceptPri.Count;i++) {
				if(priKeyName2 != null) {//mobile
					if(fieldsExceptPri[i].Name==priKeyName2) {
						continue;
					}
				}
				if(fieldsExceptPri[i].FieldType.Name=="Int64") {
					retVal.Add(fieldsExceptPri[i]);
				}
			}
			return retVal;
		}

		public static CrudSpecialColType GetSpecialType(FieldInfo field) {
			object[] attributes = field.GetCustomAttributes(typeof(CrudColumnAttribute),true);
			if(attributes.Length==0) {
				return CrudSpecialColType.None;
			}
			return ((CrudColumnAttribute)attributes[0]).SpecialType;
		}

		///<summary>Normally false</summary>
		public static bool IsNotDbColumn(FieldInfo field) {
			object[] attributes = field.GetCustomAttributes(typeof(CrudColumnAttribute),true);
			if(attributes.Length==0) {
				return false;
			}
			return ((CrudColumnAttribute)attributes[0]).IsNotDbColumn;
		}

		public static void ConnectToDatabase(string dbName){
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			dcon.SetDb("localhost",dbName,"root","","","",DatabaseType.MySql);
			RemotingClient.RemotingRole=RemotingRole.ClientDirect;
		}

		public static void ConnectToDatabaseM(string dbName) {
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			dcon.SetDb("192.168.0.196",dbName,"root","","","",DatabaseType.MySql);
			RemotingClient.RemotingRole=RemotingRole.ClientDirect;
		}

		///<summary>Gets the regular non-mobile type by stripping the m off the end of the mobile type.  Quicker than formalizing the type with an attribute on the m table.</summary>
		public static Type GetTypeFromMType(string typeNameMobile,List<Type> typesReg) {
			string typeNameReg=typeNameMobile.Substring(0,typeNameMobile.Length-1);
			for(int i=0;i<typesReg.Count;i++) {
				if(typesReg[i].Name==typeNameReg) {
					return typesReg[i];
				}
			}
			throw new ApplicationException("Type not found.");
		}

		///<summary>Makes sure the tablename is valid.  Goes through each column and makes sure that the column is present and that the type in the database is a supported type for this C# data type.  Throws exception if it fails.</summary>
		public static void ValidateTypes(Type typeClass,string dbName) {
			string tablename=GetTableName(typeClass);
			string command="SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_schema = '"+dbName+"' AND table_name = '"+tablename+"'";
			if(DataCore.GetScalar(command)!="1"){
				return;//can't validate
			}
			command="SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS "
				+"WHERE table_name = '"+tablename+"' AND table_schema = '"+dbName+"'";
			DataTable table=DataCore.GetTable(command);
			FieldInfo[] fields=typeClass.GetFields();
			for(int i=0;i<fields.Length;i++){
				if(IsNotDbColumn(fields[i])){
					continue;
				}
				ValidateColumn(dbName,tablename,fields[i],table);
			}
		}

		public static void ValidateColumn(string dbName,string tablename,FieldInfo field,DataTable table){
			//make sure the column exists
			string dataTypeInDb="";
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["COLUMN_NAME"].ToString().ToLower()==field.Name.ToLower()){
					dataTypeInDb=table.Rows[i]["DATA_TYPE"].ToString();
				}
			}
			if(dataTypeInDb==""){
				return;//can't validate
			}
			CrudSpecialColType specialColType=GetSpecialType(field);
			string dataTypeExpected="";
			string dataTypeExpected2="";//if an alternate datatype is allowed
			string dataTypeExpected3="";
			string dataTypeExpected4="";
			if(specialColType==CrudSpecialColType.TimeStamp) {
				dataTypeExpected="timestamp";
			}
			else if(specialColType==CrudSpecialColType.DateEntry) {
				dataTypeExpected="date";
			}
			else if(specialColType==CrudSpecialColType.DateEntryEditable) {
				dataTypeExpected="date";
			}
			else if(specialColType==CrudSpecialColType.DateT) {
				dataTypeExpected="datetime";
			}
			else if(specialColType==CrudSpecialColType.DateTEntry) {
				dataTypeExpected="datetime";
			}
			else if(specialColType==CrudSpecialColType.DateTEntryEditable) {
				dataTypeExpected="datetime";
			}
			else if(specialColType==CrudSpecialColType.TinyIntUnsigned) {
				dataTypeExpected="tinyint";
			}
			else if(specialColType==CrudSpecialColType.EnumAsString) {
				dataTypeExpected="varchar";
			}
			else if(field.FieldType.IsEnum) {
				dataTypeExpected="tinyint";
				dataTypeExpected2="int";
				dataTypeExpected3="smallint";
			}
			else switch(field.FieldType.Name) {
				default:
					throw new ApplicationException("Type not yet supported: "+field.FieldType.Name);
				case "Bitmap":
					dataTypeExpected="mediumtext";
					break;
				case "Boolean":
					dataTypeExpected="tinyint";
					break;
				case "Byte":
					dataTypeExpected="tinyint";
					break;
				case "Color":
					dataTypeExpected="int";
					break;
				case "DateTime"://Need to handle DateT fields here better.
					dataTypeExpected="date";
					break;
				case "Double":
					dataTypeExpected="double";
					break;
				case "Interval":
					dataTypeExpected="int";
					break;
				case "Int64":
					dataTypeExpected="bigint";
					break;
				case "Int32":
					//use C# int for ItemOrder style fields.  We know they will not use random keys.
					dataTypeExpected="int";
					dataTypeExpected2="smallint";//ok as long as the coding is careful.  Less than ideal.
					//tinyint not allowed.  Change C# type to byte.
					break;
				case "Single":
					dataTypeExpected="float";//not 1:1, but we never use the full range anyway.
					dataTypeExpected2="float unsigned";
					break;
				case "String":
					dataTypeExpected="varchar";
					dataTypeExpected2="text";
					dataTypeExpected3="char";
					dataTypeExpected4="mediumtext";
					break;
				case "TimeSpan":
					dataTypeExpected="time";
					break;
			}
			if(dataTypeInDb!=dataTypeExpected && dataTypeInDb!=dataTypeExpected2 && dataTypeInDb!=dataTypeExpected3 && dataTypeInDb!=dataTypeExpected4) {
				throw new Exception(tablename+"."+field.Name+" type mismatch for type "+field.FieldType.Name+".  Found "+dataTypeInDb+", but expecting "+dataTypeExpected);
			}
		}





	}
}
