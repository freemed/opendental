using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Ionic.Zip;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RxNorms{
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all RxNorms.</summary>
		private static List<RxNorm> listt;

		///<summary>A list of all RxNorms.</summary>
		public static List<RxNorm> Listt{
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
			string command="SELECT * FROM rxnorm ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="RxNorm";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.RxNormCrud.TableToList(table);
		}
		#endregion*/

		public static bool IsRxNormTableEmpty() {
			string command="SELECT COUNT(*) FROM rxnorm";
			if(Db.GetCount(command)=="0") {
				return true;
			}
			return false;
		}

		public static RxNorm GetByRxCUI(string rxCui) {
			string command="SELECT * FROM rxnorm WHERE RxCui='"+POut.String(rxCui)+"' AND MmslCode=''";
			return Crud.RxNormCrud.SelectOne(command);
		}

		/////<summary>Deprecated. Truncates the current rxnorm and refills based on the rxnorm.zip resource.  May take a few seconds.</summary>
		//public static void CreateFreshRxNormTableFromZip() {
		//	if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
		//		Meth.GetVoid(MethodBase.GetCurrentMethod());
		//		return;
		//	}
		//	MemoryStream ms=new MemoryStream();
		//	using(ZipFile unzipped=ZipFile.Read(Properties.Resources.rxnorm)) {
		//		ZipEntry ze=unzipped["rxnorm.txt"];
		//		ze.Extract(ms);
		//	}
		//	StreamReader reader=new StreamReader(ms);
		//	ms.Position=0;
		//	StringBuilder command=new StringBuilder();
		//	command.AppendLine("TRUNCATE TABLE rxnorm;");
		//	string line;
		//	if(DataConnection.DBtype==DatabaseType.MySql) {
		//		while((line=reader.ReadLine())!=null) {
		//			string[] lineSplit=line.Split('\t');
		//			command.AppendLine("INSERT INTO rxnorm(RxCui,MmslCode,Description) VALUES('"+lineSplit[0]+"','"+lineSplit[1]+"','"+lineSplit[2]+"');");
		//		}
		//	}
		//	else {//oracle
		//		long count=0;
		//		while((line=reader.ReadLine())!=null) {
		//			string[] lineSplit=line.Split('\t');
		//			if(count<1) {//Hardcode first insert for oracle.
		//				command.AppendLine("INSERT INTO rxnorm(RxNormNum,RxCui,MmslCode,Description) "
		//				+"VALUES(1,'"+lineSplit[0]+"','"+lineSplit[1]+"','"+lineSplit[2]+"');");
		//			}
		//			else {
		//				command.AppendLine("INSERT INTO rxnorm(RxNormNum,RxCui,MmslCode,Description) "
		//				+"VALUES((SELECT MAX(RxNormNum)+1 FROM rxnorm),'"+lineSplit[0]+"','"+lineSplit[1]+"','"+lineSplit[2]+"');");
		//			}
		//			count++;
		//		}
		//	}
		//	Db.NonQ(command.ToString());
		//	ms.Close();
		//	reader.Close();
		//}

		///<summary>Never returns multums, only used for displaying after a search.</summary>
		public static List<RxNorm> GetListByCodeOrDesc(string codeOrDesc,bool isExact,bool ignoreNumbers) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<List<RxNorm>>(MethodBase.GetCurrentMethod(),codeOrDesc);
			}
			string command="";
			if(isExact) {
				command="SELECT * FROM rxnorm WHERE (RxCui LIKE '"+POut.String(codeOrDesc)+"' OR Description LIKE '"+POut.String(codeOrDesc)+"') "
					+"AND MmslCode=''";
			}
			else {
				command="SELECT * FROM rxnorm WHERE (RxCui LIKE '%"+POut.String(codeOrDesc)+"%' OR Description LIKE '%"+POut.String(codeOrDesc)+"%') "
					+"AND MmslCode=''";
			}
			if(ignoreNumbers) {
				command+=" AND Description NOT REGEXP '.*[0-9]+.*'";
			}
			command+=" ORDER BY Description";
			return Crud.RxNormCrud.SelectMany(command);
		}

		///<summary>Used to return the multum code based on RxCui.  If blank, use the Description instead.</summary>
		public static string GetMmslCodeByRxCui(string rxCui) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetString(MethodBase.GetCurrentMethod(),rxCui);
			}
			string command="SELECT MmslCode FROM rxnorm WHERE MmslCode!='' AND RxCui='"+rxCui+"'";
			return Db.GetScalar(command);
		}

		///<summary></summary>
		public static string GetDescByRxCui(string rxCui) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetString(MethodBase.GetCurrentMethod(),rxCui);
			}
			string command="SELECT Description FROM rxnorm WHERE MmslCode='' AND RxCui='"+rxCui+"'";
			return Db.GetScalar(command);
		}

		///<summary>Gets one RxNorm from the db.</summary>
		public static RxNorm GetOne(long rxNormNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<RxNorm>(MethodBase.GetCurrentMethod(),rxNormNum);
			}
			return Crud.RxNormCrud.SelectOne(rxNormNum);
		}

		///<summary></summary>
		public static long Insert(RxNorm rxNorm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				rxNorm.RxNormNum=Meth.GetLong(MethodBase.GetCurrentMethod(),rxNorm);
				return rxNorm.RxNormNum;
			}
			return Crud.RxNormCrud.Insert(rxNorm);
		}

		///<summary>Returns a list of just the codes for use in the codesystem import tool.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT RxCui FROM rxnorm";//will return some duplicates due to the nature of the data in the table. This is acceptable.
			DataTable table=DataCore.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(table.Rows[i].ItemArray[0].ToString());
			}
			return retVal;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<RxNorm> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<RxNorm>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM rxnorm WHERE PatNum = "+POut.Long(patNum);
			return Crud.RxNormCrud.SelectMany(command);
		}
		*/



	}
}