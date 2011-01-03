using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace Crud {
	public class CrudQueries {
		private const string rn="\r\n";
	
		///<summary>Writes any necessary queries to the end of the ConvertDatabase file.  Usually zero or one.  The convertDbFile could also be the one in the Mobile folder.</summary>
		public static void Write(string convertDbFile,Type typeClass,string dbName,bool isMobile) {
			StringBuilder strb;
			FieldInfo[] fields=typeClass.GetFields();//We can't assume they are in the correct order.
			FieldInfo priKey=null;
			FieldInfo priKey1=null;
			FieldInfo priKey2=null;
			if(isMobile) {
				priKey1=CrudGenHelper.GetPriKeyMobile1(fields,typeClass.Name);
				priKey2=CrudGenHelper.GetPriKeyMobile2(fields,typeClass.Name);
			}
			else {
				priKey=CrudGenHelper.GetPriKey(fields,typeClass.Name);
			}
			string tablename=CrudGenHelper.GetTableName(typeClass);//in lowercase now.
			string priKeyParam=null;
			string priKeyParam1=null;
			string priKeyParam2=null;
			if(isMobile) {
				priKeyParam1=priKey1.Name.Substring(0,1).ToLower()+priKey1.Name.Substring(1);//lowercase initial letter.  Example customerNum
				priKeyParam2=priKey2.Name.Substring(0,1).ToLower()+priKey2.Name.Substring(1);//lowercase initial letter.  Example patNum
			}
			else {
				priKeyParam=priKey.Name.Substring(0,1).ToLower()+priKey.Name.Substring(1);//lowercase initial letter.  Example patNum
			}
			string obj=typeClass.Name.Substring(0,1).ToLower()+typeClass.Name.Substring(1);//lowercase initial letter.  Example feeSched or feeSchedm
			List<FieldInfo> fieldsExceptPri=null;
			if(isMobile) {
				fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey2);//for mobile, only excludes PK2
			}
			else {
				fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey);
			}
			CrudSpecialColType specialType;
			string command="SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_schema = '"+dbName+"' AND table_name = '"+tablename+"'";
			if(DataCore.GetScalar(command)!="1") {
				if(!CrudGenHelper.IsMissingInGeneral(typeClass)) {
					MessageBox.Show("This table was not found in the database:"
						+rn+tablename
						+rn+"Queries will be found at the end of "+Path.GetFileName(convertDbFile));
					//needs to be moved into CrudSchemaRaw:
					//strb=new StringBuilder();
					//strb.Append(rn+rn+t4+"/*");
					//strb.Append(rn+t4+"command=\"DROP TABLE IF EXISTS "+tablename+"\";");
					//strb.Append(rn+t4+"Db.NonQ(command);");
					//if(isMobile) {
					//	GetCreateTable(strb,tablename,priKey1.Name,priKey2.Name,fieldsExceptPri);
					//}
					//else {
					//	GetCreateTable(strb,tablename,priKey.Name,null,fieldsExceptPri);
					//}
					//strb.Append(rn+t4+"*/");
					//File.AppendAllText(convertDbFile,strb.ToString());
				}
			}
			List<FieldInfo> newColumns=CrudGenHelper.GetNewFields(fields,typeClass,dbName);
			if(newColumns.Count>0) {
				strb=new StringBuilder();
				strb.Append("The following columns were not found in the database.");
				for(int f=0;f<newColumns.Count;f++) {
					strb.Append(rn+tablename+"."+newColumns[f].Name);
				}
				strb.Append(rn+"Query will be found at the end of "+Path.GetFileName(convertDbFile));
				MessageBox.Show(strb.ToString());//one message for all new columns in a table.
				/* Needs to be moved into CrudSchemaRaw:
				strb=new StringBuilder();
				strb.Append(rn+rn+t4+"/*");
				for(int f=0;f<newColumns.Count;f++) {
					strb.Append(rn+t4+"command=\"ALTER TABLE "+tablename+" ADD "+newColumns[f].Name+" ");
					specialType=CrudGenHelper.GetSpecialType(newColumns[f]);
					if(specialType==CrudSpecialColType.DateEntry
						|| specialType==CrudSpecialColType.DateEntryEditable) {
						strb.Append("date NOT NULL default '0001-01-01'");
						strb.Append("\";");
						strb.Append(rn+t4+"Db.NonQ(command);");
						continue;
					}
					if(specialType==CrudSpecialColType.TimeStamp) {
						strb.Append("timestamp");
						strb.Append("\";");
						strb.Append(rn+t4+"Db.NonQ(command);");
						continue;
					}
					if(specialType==CrudSpecialColType.DateT
						|| specialType==CrudSpecialColType.DateTEntry
						|| specialType==CrudSpecialColType.DateTEntryEditable) {
						strb.Append("datetime NOT NULL default '0001-01-01 00:00:00'");
						strb.Append("\";");
						strb.Append(rn+t4+"Db.NonQ(command);");
						continue;
					}
					if(specialType==CrudSpecialColType.EnumAsString) {
						strb.Append("varchar(255) NOT NULL");
					}
					else if(newColumns[f].FieldType.IsEnum) {
						strb.Append("tinyint NOT NULL");
					}
					else switch(newColumns[f].FieldType.Name) {
						default:
							throw new ApplicationException("Type not yet supported: "+newColumns[f].FieldType.Name);
						case "Bitmap":
							strb.Append("mediumtext NOT NULL");
							break;
						case "Boolean":
							strb.Append("tinyint NOT NULL");
							break;
						case "Byte":
							strb.Append("tinyint NOT NULL");
							break;
						case "Color":
							strb.Append("int NOT NULL");
							break;
						case "DateTime"://This is only for date, not dateT
							strb.Append("date NOT NULL default '0001-01-01' (if this is actually supposed to be a datetime, timestamp, DateEntry, DateTEntry, or DateTEntryEditable column, add the missing attribute, then rerun the crud generator)");
							break;
						case "Double":
							strb.Append("double NOT NULL");
							break;
						case "Interval":
							strb.Append("int NOT NULL");
							break;
						case "Int64":
							strb.Append("bigint NOT NULL");
							break;
						case "Int32":
							strb.Append("int NOT NULL");
							break;
						case "Single":
							strb.Append("float NOT NULL");
							break;
						case "String":
							strb.Append("varchar(255) NOT NULL  (or text NOT NULL)");
							break;
						case "TimeSpan":
							strb.Append("time NOT NULL");
							break;
					}
					strb.Append("\";");
					strb.Append(rn+t4+"Db.NonQ(command);");
				}*/
				//strb.Append(rn+t4+"*/");
				//File.AppendAllText(convertDbFile,strb.ToString());
			}
		}

		/*
		///<summary>priKeyName2=null for not mobile.</summary>
		public static void GetCreateTable(StringBuilder strb,string tablename,string priKeyName1,string priKeyName2,List<FieldInfo> fieldsExceptPri) {
			CrudSpecialColType specialType;
			strb.Append(rn+t4+"command=@\"CREATE TABLE "+tablename+" (");
			bool isMobile=(priKeyName2!=null);
			if(isMobile) {
				strb.Append(rn+t5+priKeyName1+" bigint NOT NULL,");
				strb.Append(rn+t5+priKeyName2+" bigint NOT NULL,");
			}
			else {
				strb.Append(rn+t5+priKeyName1+" bigint NOT NULL auto_increment,");
			}
			for(int f=0;f<fieldsExceptPri.Count;f++) {
				if(isMobile && fieldsExceptPri[f].Name==priKeyName1) {//2 already skipped
					continue;
				}
				strb.Append(rn+t5+fieldsExceptPri[f].Name+" ");
				specialType=CrudGenHelper.GetSpecialType(fieldsExceptPri[f]);
				if(specialType==CrudSpecialColType.DateEntry
					|| specialType==CrudSpecialColType.DateEntryEditable) 
				{
					strb.Append("date NOT NULL default '0001-01-01',");
					continue;
				}
				if(specialType==CrudSpecialColType.TimeStamp) {
					strb.Append("timestamp,");
					continue;
				}
				if(specialType==CrudSpecialColType.DateT
					|| specialType==CrudSpecialColType.DateTEntry
					|| specialType==CrudSpecialColType.DateTEntryEditable) 
				{
					strb.Append("datetime NOT NULL default '0001-01-01 00:00:00',");//untested
					continue;
				}
				if(specialType==CrudSpecialColType.EnumAsString) {
					strb.Append("varchar(255) NOT NULL,");
				}
				else if(fieldsExceptPri[f].FieldType.IsEnum) {
					strb.Append("tinyint NOT NULL,");
				}
				else switch(fieldsExceptPri[f].FieldType.Name) {
					default:
						throw new ApplicationException("Type not yet supported: "+fieldsExceptPri[f].FieldType.Name);
					case "Bitmap":
						strb.Append("mediumtext NOT NULL,");
						break;
					case "Boolean":
						strb.Append("tinyint NOT NULL,");
						break;
					case "Byte":
						strb.Append("tinyint NOT NULL,");
						break;
					case "Color":
						strb.Append("int NOT NULL,");
						break;
					case "DateTime"://This is only for date, not dateT
						strb.Append("date NOT NULL default '0001-01-01',  (if this is actually supposed to be a datetime, timestamp, DateEntry, DateTEntry, or DateTEntryEditable column, add the missing attribute, then rerun the crud generator)");
						break;
					case "Double":
						strb.Append("double NOT NULL,");
						break;
					case "Interval":
						strb.Append("int NOT NULL,");
						break;
					case "Int64":
						strb.Append("bigint NOT NULL,");
						break;
					case "Int32":
						strb.Append("int NOT NULL,");
						break;
					case "Single":
						strb.Append("float NOT NULL,");
						break;
					case "String":
						strb.Append("varchar(255) NOT NULL,");
						break;
					case "TimeSpan":
						strb.Append("time NOT NULL,");
						break;
				}
			}
			if(isMobile) {
				strb.Append(rn+t5+"PRIMARY KEY ("+priKeyName1+","+priKeyName2+"),");
			}
			else {
				strb.Append(rn+t5+"PRIMARY KEY ("+priKeyName1+"),");
			}
			List<FieldInfo> indexes=CrudGenHelper.GetBigIntFields(fieldsExceptPri,priKeyName2);//priKeyName2 will be null if not mobile.
			for(int f=0;f<indexes.Count;f++) {
				strb.Append(rn+t5+"INDEX("+indexes[f].Name+")");
				if(f<indexes.Count-1) {
					strb.Append(",");
				}
			}
			strb.Append(rn+t5+"(delete this comment as well as any INDEX rows above that do not apply.)");
			strb.Append(rn+t5+") DEFAULT CHARSET=utf8\";");
			strb.Append(rn+t4+"Db.NonQ(command);");
		}*/
				




	}
}
