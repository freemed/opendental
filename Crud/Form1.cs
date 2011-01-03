using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace Crud {
	public partial class Form1:Form {
		private string crudDir;
		private string crudmDir;
		private string convertDbFile;
		private string convertDbFilem;
		private const string rn="\r\n";
		private const string t="\t";
		private const string t2="\t\t";
		private const string t3="\t\t\t";
		private const string t4="\t\t\t\t";
		private const string t5="\t\t\t\t\t";
		private List<Type> tableTypes;
		private List<Type> tableTypesAll;
		private List<Type> tableTypesM;

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender,EventArgs e) {
			crudDir=@"..\..\..\OpenDentBusiness\Crud";
			crudmDir=@"..\..\..\OpenDentBusiness\Mobile\Crud";
			convertDbFile=@"..\..\..\OpenDentBusiness\Misc\ConvertDatabases2.cs";
			convertDbFilem=@"..\..\..\OpenDentBusiness\Mobile\ConvertDatabasem.cs";
			if(!Directory.Exists(crudDir)) {
				MessageBox.Show(crudDir+" is an invalid path.");
				Application.Exit();
			}
			if(!Directory.Exists(crudmDir)) {
				MessageBox.Show(crudmDir+" is an invalid path.");
				Application.Exit();
			}
			tableTypes=new List<Type>();
			tableTypesAll=new List<Type>();
			tableTypesM=new List<Type>();
			Type typeTableBase=typeof(TableBase);
			Assembly assembly=Assembly.GetAssembly(typeTableBase);
			foreach(Type typeClass in assembly.GetTypes()){
				if(typeClass.BaseType==typeTableBase) {
					if(CrudGenHelper.IsMobile(typeClass)){
						tableTypesM.Add(typeClass);	
					}
					else{
						tableTypes.Add(typeClass);	
					}
					tableTypesAll.Add(typeClass);	
				}
			}
			tableTypesAll.Sort(CompareTypesByName);
			tableTypes.Sort(CompareTypesByName);
			tableTypesM.Sort(CompareTypesByName);
			for(int i=0;i<tableTypes.Count;i++){
				listClass.Items.Add(tableTypesAll[i].Name);
			}
			for(int i=0;i<Enum.GetNames(typeof(SnippetType)).Length;i++){
				comboType.Items.Add(Enum.GetNames(typeof(SnippetType))[i].ToString());
			}
			comboType.SelectedIndex=(int)SnippetType.EntireSclass;
		}

		private static int CompareTypesByName(Type x, Type y){
			return x.Name.CompareTo(y.Name);
		}

		private void butRun_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			string[] files;
			StringBuilder strb;
			string className;
			if(checkRun.Checked){
				files=Directory.GetFiles(crudDir);
				CrudGenHelper.ConnectToDatabase(textDb.Text);
				for(int i=0;i<tableTypes.Count;i++){
					className=tableTypes[i].Name+"Crud";
					strb=new StringBuilder();
					CrudGenHelper.ValidateTypes(tableTypes[i],textDb.Text);
					WriteAll(strb,className,tableTypes[i],false);
					File.WriteAllText(Path.Combine(crudDir,className+".cs"),strb.ToString());
					CrudQueries.Write(convertDbFile,tableTypes[i],textDb.Text,false);
					CrudGenDataInterface.Create(convertDbFile,tableTypes[i],textDb.Text,false);
					Application.DoEvents();
				}
			}
			if(checkRunM.Checked) {
				files=Directory.GetFiles(crudmDir);
				CrudGenHelper.ConnectToDatabaseM(textDbM.Text);
				for(int i=0;i<tableTypesM.Count;i++) {
					className=tableTypesM[i].Name+"Crud";
					strb=new StringBuilder();
					CrudGenHelper.ValidateTypes(tableTypesM[i],textDbM.Text);
					WriteAll(strb,className,tableTypesM[i],true);
					File.WriteAllText(Path.Combine(crudmDir,className+".cs"),strb.ToString());
					CrudQueries.Write(convertDbFilem,tableTypesM[i],textDbM.Text,true);
					CrudGenDataInterface.Create(convertDbFilem,tableTypesM[i],textDbM.Text,true);
				}
			}
			if(checkRunSchema.Checked) {
				File.WriteAllText(@"..\..\..\OpenDentBusiness\Db\SchemaCrudTest.cs",CrudSchemaForUnitTest.Create());
			}
			Cursor=Cursors.Default;
			MessageBox.Show("Done");
			//Application.Exit();
		}

		///<summary>Example of className is 'AccountCrud' or 'PatientmCrud'.</summary>
		private void WriteAll(StringBuilder strb,string className,Type typeClass,bool isMobile) {
			#region initialize variables
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
			string obj=typeClass.Name.Substring(0,1).ToLower()+typeClass.Name.Substring(1);//lowercase initial letter.  Example feeSched
			string oldObj="old"+typeClass.Name;//used in the second update overload.  Example oldFeeSched
			#endregion initialize variables
			#region class header
			strb.Append("//This file is automatically generated."+rn
				+"//Do not attempt to make changes to this file because the changes will be erased and overwritten."+rn
				+@"using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;"+rn);
			if(isMobile){
				strb.Append(rn+"namespace OpenDentBusiness.Mobile.Crud{");
			}
			else{
				strb.Append(rn+"namespace OpenDentBusiness.Crud{");
			}
			strb.Append(rn+t+"internal class "+className+" {");
			#endregion class header
			#region SelectOne
			//SelectOne------------------------------------------------------------------------------------------
			if(isMobile) {
				strb.Append(rn+t2+"///<summary>Gets one "+typeClass.Name+" object from the database using primaryKey1(CustomerNum) and primaryKey2.  Returns null if not found.</summary>");
				strb.Append(rn+t2+"internal static "+typeClass.Name+" SelectOne(long "+priKeyParam1+",long "+priKeyParam2+"){");
				strb.Append(rn+t3+"string command=\"SELECT * FROM "+tablename+" \"");
				strb.Append(rn+t4+"+\"WHERE "+priKey1.Name+" = \"+POut.Long("+priKeyParam1+")+\" AND "+priKey2.Name+" = \"+POut.Long("+priKeyParam2+")+\" LIMIT 1\";");
			}
			else {
				strb.Append(rn+t2+"///<summary>Gets one "+typeClass.Name+" object from the database using the primary key.  Returns null if not found.</summary>");
				strb.Append(rn+t2+"internal static "+typeClass.Name+" SelectOne(long "+priKeyParam+"){");
				strb.Append(rn+t3+"string command=\"SELECT * FROM "+tablename+" \"");
				strb.Append(rn+t4+"+\"WHERE "+priKey.Name+" = \"+POut.Long("+priKeyParam+")+\" LIMIT 1\";");
			}
			strb.Append(rn+t3+"List<"+typeClass.Name+"> list=TableToList(Db.GetTable(command));");
			strb.Append(rn+t3+"if(list.Count==0) {");
			strb.Append(rn+t4+"return null;");
			strb.Append(rn+t3+"}");
			strb.Append(rn+t3+"return list[0];");
			strb.Append(rn+t2+"}");
			#endregion SelectOne
			#region SelectOne(command)
			//SelectOne(string command)--------------------------------------------------------------------------
			strb.Append(rn+rn+t2+"///<summary>Gets one "+typeClass.Name+" object from the database using a query.</summary>");
			strb.Append(rn+t2+"internal static "+typeClass.Name+" SelectOne(string command){");
			strb.Append(rn+t3+@"if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException(""Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n""+command);
			}");
			strb.Append(rn+t3+"List<"+typeClass.Name+"> list=TableToList(Db.GetTable(command));");
			strb.Append(rn+t3+"if(list.Count==0) {");
			strb.Append(rn+t4+"return null;");
			strb.Append(rn+t3+"}");
			strb.Append(rn+t3+"return list[0];");
			strb.Append(rn+t2+"}");
			#endregion SelectOne(command)
			#region SelectMany
			//SelectMany-----------------------------------------------------------------------------------------
			strb.Append(rn+rn+t2+"///<summary>Gets a list of "+typeClass.Name+" objects from the database using a query.</summary>");
			strb.Append(rn+t2+"internal static List<"+typeClass.Name+"> SelectMany(string command){");
			strb.Append(rn+t3+@"if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException(""Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n""+command);
			}");
			strb.Append(rn+t3+"List<"+typeClass.Name+"> list=TableToList(Db.GetTable(command));");
			strb.Append(rn+t3+"return list;");
			strb.Append(rn+t2+"}");
			#endregion SelectMany
			#region TableToList
			//TableToList----------------------------------------------------------------------------------------
			strb.Append(rn+rn+t2+"///<summary>Converts a DataTable to a list of objects.</summary>");
			strb.Append(rn+t2+"internal static List<"+typeClass.Name+"> TableToList(DataTable table){");
			strb.Append(rn+t3+"List<"+typeClass.Name+"> retVal=new List<"+typeClass.Name+">();");
			strb.Append(rn+t3+typeClass.Name+" "+obj+";");
			strb.Append(rn+t3+"for(int i=0;i<table.Rows.Count;i++) {");
			strb.Append(rn+t4+obj+"=new "+typeClass.Name+"();");
			List<FieldInfo> fieldsInDb=CrudGenHelper.GetFieldsExceptNotDb(fields);
			//get the longest fieldname for alignment purposes
			int longestField=0;
			for(int f=0;f<fieldsInDb.Count;f++){
				if(fieldsInDb[f].Name.Length>longestField){
					longestField=fieldsInDb[f].Name.Length;
				}
			}
			CrudSpecialColType specialType;
			for(int f=0;f<fieldsInDb.Count;f++){
				//Fields are not guaranteed to be in any particular order.
				specialType=CrudGenHelper.GetSpecialType(fieldsInDb[f]);
				if(specialType==CrudSpecialColType.EnumAsString) {
					string fieldLower=fieldsInDb[f].Name.Substring(0,1).ToLower()+fieldsInDb[f].Name.Substring(1);//lowercase initial letter.  Example clockStatus
					strb.Append(rn+t4+"string "+fieldLower+"=table.Rows[i][\""+fieldsInDb[f].Name+"\"].ToString();");
					strb.Append(rn+t4+"if("+fieldLower+"==\"\"){");
					strb.Append(rn+t5+obj+"."+fieldsInDb[f].Name.PadRight(longestField-2,' ')+"="
						+"("+fieldsInDb[f].FieldType.Name+")0;");
					strb.Append(rn+t4+"}");
					strb.Append(rn+t4+"else try{");
					strb.Append(rn+t5+obj+"."+fieldsInDb[f].Name.PadRight(longestField-2,' ')+"="
						+"("+fieldsInDb[f].FieldType.Name+")Enum.Parse(typeof("+fieldsInDb[f].FieldType.Name+"),"+fieldLower+");");
					strb.Append(rn+t4+"}");
					strb.Append(rn+t4+"catch{");
					strb.Append(rn+t5+obj+"."+fieldsInDb[f].Name.PadRight(longestField-2,' ')+"="
						+"("+fieldsInDb[f].FieldType.Name+")0;");
					strb.Append(rn+t4+"}");
					continue;
				}
				strb.Append(rn+t4+obj+"."+fieldsInDb[f].Name.PadRight(longestField,' ')+"= ");
				if(specialType==CrudSpecialColType.DateT
					|| specialType==CrudSpecialColType.TimeStamp
					|| specialType==CrudSpecialColType.DateTEntry
					|| specialType==CrudSpecialColType.DateTEntryEditable)
				{
					//specialTypes.DateEntry and DateEntryEditable is handled fine by the normal DateTime (date) below.
					strb.Append("PIn.DateT (");
				}
				//else if(specialType==CrudSpecialColType.EnumAsString) {//moved up
				else if(fieldsInDb[f].FieldType.IsEnum) {
					strb.Append("("+fieldsInDb[f].FieldType.Name+")PIn.Int(");
				}
				else switch(fieldsInDb[f].FieldType.Name) {
					default:
						throw new ApplicationException("Type not yet supported: "+fieldsInDb[f].FieldType.Name);
					case "Bitmap":
						strb.Append("PIn.Bitmap(");
						break;
					case "Boolean":
						strb.Append("PIn.Bool  (");
						break;
					case "Byte":
						strb.Append("PIn.Byte  (");
						break;
					case "Color":
						strb.Append("Color.FromArgb(PIn.Int(");
						break;
					case "DateTime"://This ONLY handles date, not dateT which is a special type.
						strb.Append("PIn.Date  (");
						break;
					case "Double":
						strb.Append("PIn.Double(");
						break;
					case "Interval":
						strb.Append("new Interval(PIn.Int(");
						break;
					case "Int64":
						strb.Append("PIn.Long  (");
						break;
					case "Int32":
						strb.Append("PIn.Int   (");
						break;
					case "Single":
						strb.Append("PIn.Float (");
						break;
					case "String":
						strb.Append("PIn.String(");
						break;
					case "TimeSpan":
						strb.Append("PIn.TimeSpan(");
						break;
				}
				strb.Append("table.Rows[i][\""+fieldsInDb[f].Name+"\"].ToString())");
				if(fieldsInDb[f].FieldType.Name=="Color" || fieldsInDb[f].FieldType.Name=="Interval") {
					strb.Append(")");
				}
				strb.Append(";");
			}
			strb.Append(rn+t4+"retVal.Add("+obj+");");
			strb.Append(rn+t3+"}");
			strb.Append(rn+t3+"return retVal;");
			strb.Append(rn+t2+"}");
			#endregion TableToList
			#region Insert
			//Insert---------------------------------------------------------------------------------------------
			List<FieldInfo> fieldsExceptPri=null; 
			if(isMobile) {
				fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey2);
				//first override not used for mobile.
				//second override
				strb.Append(rn+rn+t2+"///<summary>Usually set useExistingPK=true.  Inserts one "+typeClass.Name+" into the database.</summary>");
				strb.Append(rn+t2+"internal static long Insert("+typeClass.Name+" "+obj+",bool useExistingPK){");
				strb.Append(rn+t3+"if(!useExistingPK) {");// && PrefC.RandomKeys) {");PrefC.RandomKeys is always true for mobile, since autoincr is just not possible.
//Todo: ReplicationServers.GetKey() needs to work for mobile.  Not needed until we start inserting records from mobile.
				strb.Append(rn+t4+obj+"."+priKey2.Name+"=ReplicationServers.GetKey(\""+tablename+"\",\""+priKey2.Name+"\");");
				strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"string command=\"INSERT INTO "+tablename+" (\";");
				//strb.Append(rn+t3+"if(useExistingPK || PrefC.RandomKeys) {");//PrefC.RandomKeys is always true
				strb.Append(rn+t3+"command+=\""+priKey2.Name+",\";");
				//strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"command+=\"");
				for(int f=0;f<fieldsExceptPri.Count;f++) {//all fields except PK2.
					if(CrudGenHelper.GetSpecialType(fieldsExceptPri[f])==CrudSpecialColType.TimeStamp) {
						continue;
					}
					if(f>0) {
						strb.Append(",");
					}
					strb.Append(fieldsExceptPri[f].Name);
				}
				strb.Append(") VALUES(\";");
				//strb.Append(rn+t3+"if(useExistingPK || PrefC.RandomKeys) {");//PrefC.RandomKeys is always true
				strb.Append(rn+t3+"command+=POut.Long("+obj+"."+priKey2.Name+")+\",\";");
				//strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"command+=");
			}
			else {
				fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey);
				strb.Append(rn+rn+t2+"///<summary>Inserts one "+typeClass.Name+" into the database.  Returns the new priKey.</summary>");
				strb.Append(rn+t2+"internal static long Insert("+typeClass.Name+" "+obj+"){");
				strb.Append(rn+t3+"return Insert("+obj+",false);");
				strb.Append(rn+t2+"}");
				//second override
				strb.Append(rn+rn+t2+"///<summary>Inserts one "+typeClass.Name+" into the database.  Provides option to use the existing priKey.</summary>");
				strb.Append(rn+t2+"internal static long Insert("+typeClass.Name+" "+obj+",bool useExistingPK){");
				strb.Append(rn+t3+"if(!useExistingPK && PrefC.RandomKeys) {");
				strb.Append(rn+t4+obj+"."+priKey.Name+"=ReplicationServers.GetKey(\""+tablename+"\",\""+priKey.Name+"\");");
				strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"string command=\"INSERT INTO "+tablename+" (\";");
				strb.Append(rn+t3+"if(useExistingPK || PrefC.RandomKeys) {");
				strb.Append(rn+t4+"command+=\""+priKey.Name+",\";");
				strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"command+=\"");
				for(int f=0;f<fieldsExceptPri.Count;f++) {
					if(CrudGenHelper.GetSpecialType(fieldsExceptPri[f])==CrudSpecialColType.TimeStamp) {
						continue;
					}
					if(f>0) {
						strb.Append(",");
					}
					strb.Append(fieldsExceptPri[f].Name);
				}
				strb.Append(") VALUES(\";");
				strb.Append(rn+t3+"if(useExistingPK || PrefC.RandomKeys) {");
				strb.Append(rn+t4+"command+=POut.Long("+obj+"."+priKey.Name+")+\",\";");
				strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"command+=");
			}
			//a quick and dirty temporary list that just helps keep track of which columns take parameters
			List<OdSqlParameter> paramList=new List<OdSqlParameter>();
			for(int f=0;f<fieldsExceptPri.Count;f++) {
				strb.Append(rn+t4);
				specialType=CrudGenHelper.GetSpecialType(fieldsExceptPri[f]);
				if(specialType==CrudSpecialColType.TimeStamp) {
					strb.Append("//"+fieldsExceptPri[f].Name+" can only be set by MySQL");
					continue;
				}
				if(f==0) {
					strb.Append(" ");
				}
				else {
					strb.Append("+");
				}
				if(specialType==CrudSpecialColType.DateEntry
					|| specialType==CrudSpecialColType.DateEntryEditable
					|| specialType==CrudSpecialColType.DateTEntry
					|| specialType==CrudSpecialColType.DateTEntryEditable) 
				{
					strb.Append("\"NOW()");
				}
				else if(specialType==CrudSpecialColType.DateT) {
					strb.Append("    POut.DateT ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
				}
				else if(specialType==CrudSpecialColType.EnumAsString) {
					strb.Append("    POut.String("+obj+"."+fieldsExceptPri[f].Name+".ToString())+\"");
				}
				else if(specialType==CrudSpecialColType.TimeSpanNeg) {
					strb.Append("\"'\"+POut.TSpan ("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
				}
				else if(specialType==CrudSpecialColType.TextIsClob) {
					strb.Append("DbHelper.ParamChar+\"param"+fieldsExceptPri[f].Name);
					paramList.Add(new OdSqlParameter(fieldsExceptPri[f].Name,OdDbType.Text,null));
				}
				else if(fieldsExceptPri[f].FieldType.IsEnum) {
					strb.Append("    POut.Int   ((int)"+obj+"."+fieldsExceptPri[f].Name+")+\"");
				}
				else switch(fieldsExceptPri[f].FieldType.Name) {
					default:
						throw new ApplicationException("Type not yet supported: "+fieldsExceptPri[f].FieldType.Name);
					case "Bitmap":
						strb.Append("    POut.Bitmap("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Boolean":
						strb.Append("    POut.Bool  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Byte":
						strb.Append("    POut.Byte  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Color":
						strb.Append("    POut.Int   ("+obj+"."+fieldsExceptPri[f].Name+".ToArgb())+\"");
						break;
					case "DateTime"://This is only for date, not dateT.
						strb.Append("    POut.Date  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Double":
						strb.Append("\"'\"+POut.Double("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
						break;
					case "Interval":
						strb.Append("    POut.Int   ("+obj+"."+fieldsExceptPri[f].Name+".ToInt())+\"");
						break;
					case "Int64":
						strb.Append("    POut.Long  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Int32":
						strb.Append("    POut.Int   ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Single":
						strb.Append("    POut.Float ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "String":
						strb.Append("\"'\"+POut.String("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
						break;
					case "TimeSpan":
						strb.Append("    POut.Time  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
				}
				if(f==fieldsExceptPri.Count-2
					&& CrudGenHelper.GetSpecialType(fieldsExceptPri[f+1])==CrudSpecialColType.TimeStamp) 
				{
					//in case the last field is a timestamp
					strb.Append(")\";");
				}
				else if(f<fieldsExceptPri.Count-1) {
					strb.Append(",\"");
				}
				else {
					strb.Append(")\";");
				}
			}
			for(int i=0;i<paramList.Count;i++) {
				//example: OdSqlParameter paramNote=new OdSqlParameter("paramNote",
				//           OdDbType.Text,procNote.Note);
				strb.Append(rn+t3+"OdSqlParameter param"+paramList[i].ParameterName+"=new OdSqlParameter(\"param"+paramList[i].ParameterName+"\","
					+"OdDbType.Text,"+obj+"."+paramList[i].ParameterName+");");
			}
			string paramsString="";//example: ,paramNote,paramAltNote
			for(int i=0;i<paramList.Count;i++){
				paramsString+=",param"+paramList[i].ParameterName;
			}	
			if(isMobile) {
				strb.Append(rn+t3+"Db.NonQ(command"+paramsString+");//There is no autoincrement in the mobile server.");
				strb.Append(rn+t3+"return "+obj+"."+priKey2.Name+";");
				strb.Append(rn+t2+"}");
			}
			else {
				strb.Append(rn+t3+"if(useExistingPK || PrefC.RandomKeys) {");
				strb.Append(rn+t4+"Db.NonQ(command"+paramsString+");");
				strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"else {");
				strb.Append(rn+t4+obj+"."+priKey.Name+"=Db.NonQ(command,true"+paramsString+");");
				strb.Append(rn+t3+"}");
				strb.Append(rn+t3+"return "+obj+"."+priKey.Name+";");
				strb.Append(rn+t2+"}");
			}
			#endregion Insert
			#region Update
			//Update---------------------------------------------------------------------------------------------
			strb.Append(rn+rn+t2+"///<summary>Updates one "+typeClass.Name+" in the database.</summary>");
			strb.Append(rn+t2+"internal static void Update("+typeClass.Name+" "+obj+"){");
			strb.Append(rn+t3+"string command=\"UPDATE "+tablename+" SET \"");
			for(int f=0;f<fieldsExceptPri.Count;f++) {
				if(isMobile && fieldsExceptPri[f]==priKey1) {//2 already skipped
					continue;
				}
				specialType=CrudGenHelper.GetSpecialType(fieldsExceptPri[f]);
				if(specialType==CrudSpecialColType.DateEntry) {
					strb.Append(rn+t4+"//"+fieldsExceptPri[f].Name+" not allowed to change");
					continue;
				}
				if(specialType==CrudSpecialColType.DateTEntry) {
					strb.Append(rn+t4+"//"+fieldsExceptPri[f].Name+" not allowed to change");
					continue;
				}
				if(specialType==CrudSpecialColType.TimeStamp) {
					strb.Append(rn+t4+"//"+fieldsExceptPri[f].Name+" can only be set by MySQL");
					continue;
				}
				if(specialType==CrudSpecialColType.ExcludeFromUpdate) {
					strb.Append(rn+t4+"//"+fieldsExceptPri[f].Name+" excluded from update");
					continue;
				}
				strb.Append(rn+t4+"+\""+fieldsExceptPri[f].Name.PadRight(longestField,' ')+"= ");
				if(specialType==CrudSpecialColType.DateT){
					strb.Append(" \"+POut.DateT ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
				}
				else if(specialType==CrudSpecialColType.DateEntryEditable){
					strb.Append(" \"+POut.Date  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
				}
				else if(specialType==CrudSpecialColType.DateTEntryEditable){
					strb.Append(" \"+POut.DateT ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
				}
				else if(specialType==CrudSpecialColType.EnumAsString) {
					strb.Append(" \"+POut.String("+obj+"."+fieldsExceptPri[f].Name+".ToString())+\"");
				}
				else if(specialType==CrudSpecialColType.TimeSpanNeg) {
					strb.Append("'\"+POut.TSpan ("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
				}
				else if(specialType==CrudSpecialColType.TextIsClob) {
					strb.Append(" \"+DbHelper.ParamChar+\"param"+fieldsExceptPri[f].Name);
					//paramList is already set above
				}
				else if(fieldsExceptPri[f].FieldType.IsEnum) {
					strb.Append(" \"+POut.Int   ((int)"+obj+"."+fieldsExceptPri[f].Name+")+\"");
				}
				else switch(fieldsExceptPri[f].FieldType.Name) {
					default:
						throw new ApplicationException("Type not yet supported: "+fieldsExceptPri[f].FieldType.Name);
					case "Bitmap":
						strb.Append(" \"+POut.Bitmap("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Boolean":
						strb.Append(" \"+POut.Bool  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Byte":
						strb.Append(" \"+POut.Byte  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Color":
						strb.Append(" \"+POut.Int   ("+obj+"."+fieldsExceptPri[f].Name+".ToArgb())+\"");
						break;
					case "DateTime"://This is only for date, not dateT
						strb.Append(" \"+POut.Date  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Double":
						strb.Append("'\"+POut.Double("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
						break;
					case "Interval":
						strb.Append(" \"+POut.Int   ("+obj+"."+fieldsExceptPri[f].Name+".ToInt())+\"");
						break;
					case "Int64":
						strb.Append(" \"+POut.Long  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Int32":
						strb.Append(" \"+POut.Int   ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "Single":
						strb.Append(" \"+POut.Float ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
					case "String":
						strb.Append("'\"+POut.String("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
						break;
					case "TimeSpan":
						strb.Append(" \"+POut.Time  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
						break;
				}
				if(f==fieldsExceptPri.Count-2
					&& CrudGenHelper.GetSpecialType(fieldsExceptPri[f+1])==CrudSpecialColType.TimeStamp) 
				{
					//in case the last field is a timestamp
					//strb.Append(" \"");
				}
				else if(f<fieldsExceptPri.Count-1) {
					strb.Append(",");
				}
				strb.Append(" \"");
			}
			if(isMobile) {
				strb.Append(rn+t4+"+\"WHERE "+priKey1.Name+" = \"+POut.Long("+obj+"."+priKey1.Name+")+\" AND "+priKey2.Name+" = \"+POut.Long("+obj+"."+priKey2.Name+");");
			}
			else {
				strb.Append(rn+t4+"+\"WHERE "+priKey.Name+" = \"+POut.Long("+obj+"."+priKey.Name+");");
			}
			for(int i=0;i<paramList.Count;i++) {
				strb.Append(rn+t3+"OdSqlParameter param"+paramList[i].ParameterName+"=new OdSqlParameter(\"param"+paramList[i].ParameterName+"\","
					+"OdDbType.Text,"+obj+"."+paramList[i].ParameterName+");");
			}
			strb.Append(rn+t3+"Db.NonQ(command"+paramsString+");");
			strb.Append(rn+t2+"}");
			#endregion Update
			#region Update 2nd override
			//Update, 2nd override-------------------------------------------------------------------------------
			if(!isMobile) {
				strb.Append(rn+rn+t2+"///<summary>Updates one "+typeClass.Name+" in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>");
				strb.Append(rn+t2+"internal static void Update("+typeClass.Name+" "+obj+","+typeClass.Name+" "+oldObj+"){");
				strb.Append(rn+t3+"string command=\"\";");
				for(int f=0;f<fieldsExceptPri.Count;f++) {
					//if(isMobile && fieldsExceptPri[f]==priKey1) {//2 already skipped
					//	continue;
					//}
					specialType=CrudGenHelper.GetSpecialType(fieldsExceptPri[f]);
					if(specialType==CrudSpecialColType.DateEntry) {
						strb.Append(rn+t3+"//"+fieldsExceptPri[f].Name+" not allowed to change");
						continue;
					}
					if(specialType==CrudSpecialColType.DateTEntry) {
						strb.Append(rn+t3+"//"+fieldsExceptPri[f].Name+" not allowed to change");
						continue;
					}
					if(specialType==CrudSpecialColType.TimeStamp) {
						strb.Append(rn+t3+"//"+fieldsExceptPri[f].Name+" can only be set by MySQL");
						continue;
					}
					if(specialType==CrudSpecialColType.ExcludeFromUpdate) {
						strb.Append(rn+t3+"//"+fieldsExceptPri[f].Name+" excluded from update");
						continue;
					}
					strb.Append(rn+t3+"if("+obj+"."+fieldsExceptPri[f].Name+" != "+oldObj+"."+fieldsExceptPri[f].Name+") {");
					strb.Append(rn+t4+"if(command!=\"\"){ command+=\",\";}");
					strb.Append(rn+t4+"command+=\""+fieldsExceptPri[f].Name+" = ");
					if(specialType==CrudSpecialColType.DateT) {
						strb.Append("\"+POut.DateT("+obj+"."+fieldsExceptPri[f].Name+")+\"");
					}
					else if(specialType==CrudSpecialColType.DateEntryEditable) {
						strb.Append("\"+POut.Date("+obj+"."+fieldsExceptPri[f].Name+")+\"");
					}
					else if(specialType==CrudSpecialColType.DateTEntryEditable) {
						strb.Append("\"+POut.DateT("+obj+"."+fieldsExceptPri[f].Name+")+\"");
					}
					else if(specialType==CrudSpecialColType.EnumAsString) {
						strb.Append("\"+POut.String("+obj+"."+fieldsExceptPri[f].Name+".ToString())+\"");
					}
					else if(specialType==CrudSpecialColType.TimeSpanNeg) {
						strb.Append("'\"+POut.TSpan ("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
					}
					else if(specialType==CrudSpecialColType.TextIsClob) {
						strb.Append("\"+DbHelper.ParamChar+\"param"+fieldsExceptPri[f].Name);
						//paramList is already set above
					}
					else if(fieldsExceptPri[f].FieldType.IsEnum) {
						strb.Append("\"+POut.Int   ((int)"+obj+"."+fieldsExceptPri[f].Name+")+\"");
					}
					else switch(fieldsExceptPri[f].FieldType.Name) {
							default:
								throw new ApplicationException("Type not yet supported: "+fieldsExceptPri[f].FieldType.Name);
							case "Boolean":
								strb.Append("\"+POut.Bool("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "Bitmap":
								strb.Append("\"+POut.Bitmap("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "Byte":
								strb.Append("\"+POut.Byte("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "Color":
								strb.Append("\"+POut.Int("+obj+"."+fieldsExceptPri[f].Name+".ToArgb())+\"");
								break;
							case "DateTime"://This is only for date, not dateT.
								strb.Append("\"+POut.Date("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "Double":
								strb.Append("'\"+POut.Double("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
								break;
							case "Interval":
								strb.Append("\"+POut.Int("+obj+"."+fieldsExceptPri[f].Name+".ToInt())+\"");
								break;
							case "Int64":
								strb.Append("\"+POut.Long("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "Int32":
								strb.Append("\"+POut.Int("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "Single":
								strb.Append("\"+POut.Float("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
							case "String":
								strb.Append("'\"+POut.String("+obj+"."+fieldsExceptPri[f].Name+")+\"'");
								break;
							case "TimeSpan":
								strb.Append("\"+POut.Time  ("+obj+"."+fieldsExceptPri[f].Name+")+\"");
								break;
						}
					strb.Append("\";");
					strb.Append(rn+t3+"}");
				}
				strb.Append(rn+t3+"if(command==\"\"){");
				strb.Append(rn+t4+"return;");
				strb.Append(rn+t3+"}");
				for(int i=0;i<paramList.Count;i++) {
					strb.Append(rn+t3+"OdSqlParameter param"+paramList[i].ParameterName+"=new OdSqlParameter(\"param"+paramList[i].ParameterName+"\","
					+"OdDbType.Text,"+obj+"."+paramList[i].ParameterName+");");
				}
				strb.Append(rn+t3+"command=\"UPDATE "+tablename+" SET \"+command");
				strb.Append(rn+t4+"+\" WHERE "+priKey.Name+" = \"+POut.Long("+obj+"."+priKey.Name+");");
				strb.Append(rn+t3+"Db.NonQ(command"+paramsString+");");
				strb.Append(rn+t2+"}");
			}
			#endregion Update 2nd override
			#region Delete
			//Delete---------------------------------------------------------------------------------------------
			if(CrudGenHelper.IsDeleteForbidden(typeClass)) {
				strb.Append(rn+rn+t2+"//Delete not allowed for this table");
				strb.Append(rn+t2+"//internal static void Delete(long "+priKeyParam+"){");
				strb.Append(rn+t2+"//");
				strb.Append(rn+t2+"//}");
			}
			else {
				strb.Append(rn+rn+t2+"///<summary>Deletes one "+typeClass.Name+" from the database.</summary>");
				if(isMobile) {
					strb.Append(rn+t2+"internal static void Delete(long "+priKeyParam1+",long "+priKeyParam2+"){");
					strb.Append(rn+t3+"string command=\"DELETE FROM "+tablename+" \"");
					strb.Append(rn+t4+"+\"WHERE "+priKey1.Name+" = \"+POut.Long("+priKeyParam1+")+\" AND "+priKey2.Name+" = \"+POut.Long("+priKeyParam2+");");
				}
				else {
					strb.Append(rn+t2+"internal static void Delete(long "+priKeyParam+"){");
					strb.Append(rn+t3+"string command=\"DELETE FROM "+tablename+" \"");
					strb.Append(rn+t4+"+\"WHERE "+priKey.Name+" = \"+POut.Long("+priKeyParam+");");
				}
				strb.Append(rn+t3+"Db.NonQ(command);");
				strb.Append(rn+t2+"}");
			}
			#endregion Delete
			#region ConvertToM
			if(isMobile) {
				//ConvertToM------------------------------------------------------------------------------------------
				Type typeClassReg=CrudGenHelper.GetTypeFromMType(typeClass.Name,tableTypes);//gets the non-mobile type
				string tablenameReg=CrudGenHelper.GetTableName(typeClassReg);//in lowercase now.
				string objReg=typeClassReg.Name.Substring(0,1).ToLower()+typeClassReg.Name.Substring(1);//lowercase initial letter.  Example feeSched
				FieldInfo[] fieldsReg=typeClassReg.GetFields();//We can't assume they are in the correct order.
				List<FieldInfo> fieldsInDbReg=CrudGenHelper.GetFieldsExceptNotDb(fieldsReg);
				strb.Append(rn+rn+t2+"///<summary>Converts one "+typeClassReg.Name+" object to its mobile equivalent.  Warning! CustomerNum will always be 0.</summary>");
				strb.Append(rn+t2+"internal static "+typeClass.Name+" ConvertToM("+typeClassReg.Name+" "+objReg+"){");
				strb.Append(rn+t3+typeClass.Name+" "+obj+"=new "+typeClass.Name+"();");
				for(int f=0;f<fieldsInDb.Count;f++) {
					if(fieldsInDb[f].Name=="CustomerNum") {
						strb.Append(rn+t3+"//CustomerNum cannot be set.  Remains 0.");
						continue;
					}
					bool matchfound=false;
					for(int r=0;r<fieldsInDbReg.Count;r++) {
						if(fieldsInDb[f].Name==fieldsInDbReg[r].Name) {
							strb.Append(rn+t3+obj+"."+fieldsInDb[f].Name.PadRight(longestField,' ')+"="+objReg+"."+fieldsInDbReg[r].Name+";");
							matchfound=true;
						}
					}
					if(!matchfound) {
						throw new ApplicationException("Match not found.");
					}
				}
				strb.Append(rn+t3+"return "+obj+";");
				strb.Append(rn+t2+"}");
			}
			#endregion ConvertToM
			//Footer
			strb.Append(rn);
			strb.Append(@"
	}
}");
		}

		private void butSnippet_Click(object sender,EventArgs e) {
			if(listClass.SelectedIndex==-1){
				MessageBox.Show("Please select a class.");
				return;
			}
			//if(comboType.SelectedIndex==-1){
			//	MessageBox.Show("Please select a type.");
			//	return;
			//}
			Type typeClass=tableTypesAll[listClass.SelectedIndex];
			SnippetType snipType=(SnippetType)comboType.SelectedIndex;
			bool isMobile=CrudGenHelper.IsMobile(typeClass);
			string snippet=CrudGenDataInterface.GetSnippet(typeClass,snipType,isMobile);
			textSnippet.Text=snippet;
			Clipboard.SetText(snippet);
		}


	}
}
