using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace Crud {
	public class CrudGenDataInterface {
		private const string rn="\r\n";
		private const string t="\t";
		private const string t2="\t\t";
		private const string t3="\t\t\t";
		private const string t4="\t\t\t\t";
		private const string t5="\t\t\t\t\t";

		public static string GetSnippet(Type typeClass,SnippetType snipType,bool isMobile){
			//bool isMobile=CrudGenHelper.IsMobile(typeClass);
			string Sname=GetSname(typeClass.Name);
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
			string tablename=CrudGenHelper.GetTableName(typeClass);//in lowercase now.  Example feesched
			List<FieldInfo> fieldsExceptPri=null;
			if(isMobile) {
				fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey2);//for mobile, only excludes PK2
			}
			else {
				fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey);
			}
			switch(snipType){
				default:
					return "snippet type not found.";
				case SnippetType.Insert:
					if(isMobile) {
						return GetInsert(typeClass.Name,obj,null,true);
					}
					else {
						return GetInsert(typeClass.Name,obj,priKey.Name,false);
					}
				case SnippetType.Update:
					return GetUpdate(typeClass.Name,obj,isMobile);
				case SnippetType.EntireSclass:
					if(isMobile) {
						return GetEntireSclassMobile(typeClass.Name,obj,priKey1.Name,priKey2.Name,Sname,tablename,priKeyParam1,priKeyParam2);
					}
					else {
						return GetEntireSclass(typeClass.Name,obj,priKey.Name,Sname,tablename,priKeyParam);
					}
				case SnippetType.CreateTable:
					if(isMobile) {
						return GetCreateTable(tablename,priKey1.Name,priKey2.Name,fieldsExceptPri);
					}
					else {
						return GetCreateTable(tablename,priKey.Name,null,fieldsExceptPri);
					}
			}
		}

		private static string GetSname(string typeClassName){
			string Sname=typeClassName;
			if(typeClassName=="Etrans") {
				return "Etranss";
			}
			if(Sname.EndsWith("s")){
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ch")){
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ay")) {
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

		///<summary>Creates the Data Interface "s" classes for new tables, complete with typical stubs.  Asks user first.</summary>
		public static void Create(string convertDbFile,Type typeClass,string dbName,bool isMobile) {
			string Sname=GetSname(typeClass.Name);
			string fileName=null;
			if(isMobile) {
				fileName=@"..\..\..\OpenDentBusiness\Mobile\Data Interface\"+Sname+".cs";
			}
			else {
				fileName=@"..\..\..\OpenDentBusiness\Data Interface\"+Sname+".cs";
			}
			if(File.Exists(fileName)) {
				return;
			}
			if(CrudGenHelper.IsMissingInGeneral(typeClass)) {
				return;
			}
			if(MessageBox.Show("Create stub for "+fileName+"?","",MessageBoxButtons.YesNo)!=DialogResult.Yes) {
				return;
			}
			string snippet=GetSnippet(typeClass,SnippetType.EntireSclass,isMobile);
			File.WriteAllText(fileName,snippet);
			//Process.Start(fileName);
			MessageBox.Show(fileName+" has been created.  Be sure to add it to the project and to SVN");
		}

		/// <summary>priKeyName will be null if mobile.</summary>
		private static string GetInsert(string typeClassName,string obj,string priKeyName,bool isMobile){
			string retVal=null;
			if(isMobile) {
				retVal=@"		///<summary></summary>
		public static long Insert("+typeClassName+@" "+obj+@"){
			return Crud."+typeClassName+@"Crud.Insert("+obj+@",true);
		}";
			}
			else {
				retVal=@"		///<summary></summary>
		public static long Insert("+typeClassName+@" "+obj+@"){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				"+obj+@"."+priKeyName+@"=Meth.GetLong(MethodBase.GetCurrentMethod(),"+obj+@");
				return "+obj+@"."+priKeyName+@";
			}
			return Crud."+typeClassName+@"Crud.Insert("+obj+@");
		}";
			}
			return retVal;
		}

		private static string GetUpdate(string typeClassName,string obj,bool isMobile){
			string retVal=null;
			if(isMobile) {
				retVal=@"		///<summary></summary>
		public static void Update("+typeClassName+@" "+obj+@"){
			Crud."+typeClassName+@"Crud.Update("+obj+@");
		}";
			}
			else {
				retVal=@"		///<summary></summary>
		public static void Update("+typeClassName+@" "+obj+@"){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),"+obj+@");
				return;
			}
			Crud."+typeClassName+@"Crud.Update("+obj+@");
		}";
			}
			return retVal;
		}

		private static string GetEntireSclass(string typeClassName,string obj,string priKeyName,string Sname,string tablename,string priKeyParam){
			string str=@"using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class "+Sname+@"{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all "+Sname+@".</summary>
		private static List<"+typeClassName+@"> listt;

		///<summary>A list of all "+Sname+@".</summary>
		public static List<"+typeClassName+@"> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=""SELECT * FROM "+tablename+@" ORDER BY ItemOrder"";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="""+typeClassName+@""";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud."+typeClassName+@"Crud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<"+typeClassName+@"> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<"+typeClassName+@">>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=""SELECT * FROM "+tablename+@" WHERE PatNum = ""+POut.Long(patNum);
			return Crud."+typeClassName+@"Crud.SelectMany(command);
		}

		///<summary>Gets one "+typeClassName+@" from the db.</summary>
		public static "+typeClassName+@" GetOne(long "+priKeyParam+@"){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<"+typeClassName+@">(MethodBase.GetCurrentMethod(),"+priKeyParam+@");
			}
			return Crud."+typeClassName+@"Crud.SelectOne("+priKeyParam+@");
		}

"+GetInsert(typeClassName,obj,priKeyName,false)+@"

"+GetUpdate(typeClassName,obj,false)+@"

		///<summary></summary>
		public static void Delete(long "+priKeyParam+@") {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),"+priKeyParam+@");
				return;
			}
			string command= ""DELETE FROM "+tablename+@" WHERE "+priKeyName+@" = ""+POut.Long("+priKeyParam+@");
			Db.NonQ(command);
		}
		*/



	}
}";
			return str;			
		}

		/// <summary>priKeyParam1 is CustomerNum for now.</summary>
		private static string GetEntireSclassMobile(string typeClassName,string obj,string priKeyName1,string priKeyName2,string Sname,string tablename,string priKeyParam1,string priKeyParam2) {
			string str=@"using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class "+Sname+@"{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<"+typeClassName+@"> Refresh(long patNum){
			string command=""SELECT * FROM "+tablename+@" WHERE PatNum = ""+POut.Long(patNum);
			return Crud."+typeClassName+@"Crud.SelectMany(command);
		}

		///<summary>Gets one "+typeClassName+@" from the db.</summary>
		public static "+typeClassName+@" GetOne(long "+priKeyParam1+",long "+priKeyParam2+@"){
			return Crud."+typeClassName+@"Crud.SelectOne("+priKeyParam1+","+priKeyParam2+@");
		}

"+GetInsert(typeClassName,obj,null,true)+@"

"+GetUpdate(typeClassName,obj,true)+@"

		///<summary></summary>
		public static void Delete(long "+priKeyParam1+",long "+priKeyParam2+@") {
			string command= ""DELETE FROM "+tablename+@" WHERE "+priKeyName1+@" = ""+POut.Long("+priKeyParam1+")+\" AND "+priKeyName2+@" = ""+POut.Long("+priKeyParam2+@");
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<"+typeClassName+@"> ConvertListToM(List<"+typeClassName.Substring(0,typeClassName.Length-1)+@"> list) {
			List<"+typeClassName+@"> retVal=new List<"+typeClassName+@">();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud."+typeClassName+@"Crud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<"+typeClassName+@"> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				"+typeClassName+" "+obj+@"=Crud."+typeClassName+@"Crud.SelectOne(customerNum,list[i]."+priKeyName2+@");
				if("+obj+@"==null){//not in db
					Crud."+typeClassName+@"Crud.Insert(list[i],true);
				}
				else{
					Crud."+typeClassName+@"Crud.Update(list[i]);
				}
			}
		}
		*/



	}
}";
			return str;
		}

		///<summary>priKeyName2 will be null if not mobile.</summary>
		private static string GetCreateTable(string tablename,string priKeyName1,string priKeyName2,List<FieldInfo> fieldsExceptPri) {
			StringBuilder strb=new StringBuilder();
			CrudQueries.GetCreateTable(strb,tablename,priKeyName1,priKeyName2,fieldsExceptPri);
			return strb.ToString();
		}








	}
}
