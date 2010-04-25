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

		///<summary>Creates the Data Interface "s" classes for new tables, complete with typical stubs.  Asks user first.</summary>
		public static void Create(string convertDbFile,Type typeClass,string dbName) {
			string Sname=typeClass.Name;
			if(Sname.EndsWith("s")){
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ch")){
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("y")) {
				Sname=Sname.TrimEnd('y')+"ies";
			}
			else {
				Sname=Sname+"s";
			}
			string fileName=@"..\..\..\OpenDentBusiness\Data Interface\"+Sname+".cs";
			if(File.Exists(fileName)) {
				return;
			}
			if(MessageBox.Show("Create stub for "+fileName+"?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				return;
			}
			FieldInfo[] fields=typeClass.GetFields();//We can't assume they are in the correct order.
			FieldInfo priKey=CrudGenHelper.GetPriKey(fields,typeClass.Name);
			string priKeyParam=priKey.Name.Substring(0,1).ToLower()+priKey.Name.Substring(1);//lowercase initial letter.  Example patNum
			string obj=typeClass.Name.Substring(0,1).ToLower()+typeClass.Name.Substring(1);//lowercase initial letter.  Example feeSched
			string tablename=CrudGenHelper.GetTableName(typeClass);//in lowercase now.  Example feesched
			List<FieldInfo> fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey);
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
		//If leaving this region in place, be sure to add RefreshCache and FillCache to the Cache.cs file with all the other Cache types.

		///<summary>A list of all "+Sname+@".</summary>
		private static List<"+typeClass.Name+@"> listt;

		///<summary>A list of all "+Sname+@".</summary>
		public static List<"+typeClass.Name+@"> Listt{
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
			string command=""SELECT * FROM "+tablename+@" ORDER BY ItemOrder"";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="""+typeClass.Name+@""";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud."+typeClass.Name+@"Crud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<"+typeClass.Name+@"> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<"+typeClass.Name+@">>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=""SELECT * FROM "+tablename+@" WHERE PatNum = ""+POut.Long(patNum);
			return Crud."+typeClass.Name+@"Crud.SelectMany(command);
		}

		///<summary>Gets one "+typeClass.Name+@" from the db.</summary>
		public static "+typeClass.Name+@" GetOne(long "+priKeyParam+@"){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<"+typeClass.Name+@">(MethodBase.GetCurrentMethod(),"+priKeyParam+@");
			}
			return Crud."+typeClass.Name+@"Crud.SelectOne("+priKeyParam+@");
		}

		///<summary></summary>
		public static long Insert("+typeClass.Name+@" "+obj+@"){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				"+obj+@"."+priKey.Name+@"=Meth.GetLong(MethodBase.GetCurrentMethod(),"+obj+@");
				return "+obj+@"."+priKey.Name+@";
			}
			return Crud."+typeClass.Name+@"Crud.Insert("+obj+@");
		}

		///<summary></summary>
		public static void Update("+typeClass.Name+@" "+obj+@"){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),"+obj+@");
				return;
			}
			Crud."+typeClass.Name+@"Crud.Update("+obj+@");
		}
		*/



	}
}";
			File.WriteAllText(fileName,str);
			//Process.Start(fileName);
			MessageBox.Show(fileName+" has been created.  Be sure to add it to the project and to SVN");
			
		}
	}
}
