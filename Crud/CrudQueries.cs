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
		private const string t="\t";
		private const string t2="\t\t";
		private const string t3="\t\t\t";
		private const string t4="\t\t\t\t";
		private const string t5="\t\t\t\t\t";

		///<summary>Writes any necessary queries to the end of the ConvertDatabases file.  Usually zero or one.</summary>
		public static void Write(string convertDbFile,Type typeClass,string dbName) {
			StringBuilder strb;
			FieldInfo[] fields=typeClass.GetFields();//We can't assume they are in the correct order.
			FieldInfo priKey=CrudGenHelper.GetPriKey(fields,typeClass.Name);
			string priKeyParam=priKey.Name.Substring(0,1).ToLower()+priKey.Name.Substring(1);//lowercase initial letter.  Example patNum
			string obj=typeClass.Name.Substring(0,1).ToLower()+typeClass.Name.Substring(1);//lowercase initial letter.  Example feeSched
			string tablename=CrudGenHelper.GetTableName(typeClass);//in lowercase now.
			List<FieldInfo> fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey);
			CrudSpecialColType specialType;
			string command="SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_schema = '"+dbName+"' AND table_name = '"+tablename+"'";
			if(DataCore.GetScalar(command)!="1") {
				MessageBox.Show("This table was not found in the database:"
					+rn+tablename
					+rn+"Query will be found at the end of "+Path.GetFileName(convertDbFile));
				strb=new StringBuilder();
				strb.Append(rn+rn+t4+"/*");
				strb.Append(rn+t4+"command=\"DROP TABLE IF EXISTS "+tablename+"\";");
				strb.Append(rn+t4+"Db.NonQ(command);");
				GetCreateTable(strb,tablename,priKey.Name,fieldsExceptPri);
				strb.Append(rn+t4+"*/");
				File.AppendAllText(convertDbFile,strb.ToString());
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
					if(newColumns[f].FieldType.IsEnum) {
						strb.Append("tinyint NOT NULL");
					}
					else switch(newColumns[f].FieldType.Name) {
							default:
								throw new ApplicationException("Type not yet supported: "+newColumns[f].FieldType.Name);
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
				}
				strb.Append(rn+t4+"*/");
				File.AppendAllText(convertDbFile,strb.ToString());
			}
		}

		public static void GetCreateTable(StringBuilder strb,string tablename,string priKeyName,List<FieldInfo> fieldsExceptPri){
			CrudSpecialColType specialType;
			strb.Append(rn+t4+"command=@\"CREATE TABLE "+tablename+" (");
			strb.Append(rn+t5+priKeyName+" bigint NOT NULL auto_increment,");
			for(int f=0;f<fieldsExceptPri.Count;f++) {
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
				if(fieldsExceptPri[f].FieldType.IsEnum) {
					strb.Append("tinyint NOT NULL,");
				}
				else switch(fieldsExceptPri[f].FieldType.Name) {
						default:
							throw new ApplicationException("Type not yet supported: "+fieldsExceptPri[f].FieldType.Name);
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
			strb.Append(rn+t5+"PRIMARY KEY ("+priKeyName+"),");
			List<FieldInfo> indexes=CrudGenHelper.GetBigIntFields(fieldsExceptPri);
			for(int f=0;f<indexes.Count;f++) {
				strb.Append(rn+t5+"INDEX("+indexes[f].Name+")");
				if(f<indexes.Count-1) {
					strb.Append(",");
				}
			}
			strb.Append(rn+t5+"(delete this comment as well as any INDEX rows above that do not apply.)");
			strb.Append(rn+t5+") DEFAULT CHARSET=utf8\";");
		}
				




	}
}
