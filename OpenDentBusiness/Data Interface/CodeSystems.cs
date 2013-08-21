using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using CodeBase;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CodeSystems{

		///<summary>Returns a hard coded list of code systems available for current version. Adding codes to this list also requires codes to be added to Webservice.</summary>
		public static List<CodeSystem> GetForCurrentVersion() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CodeSystem>>(MethodBase.GetCurrentMethod());
			}
			//Hard Coded list prevents users from "spoofing" code systems for use in the code system import window by adding them to the table.
			List<CodeSystem> retVal=new List<CodeSystem>();
			retVal.Add(new CodeSystem("AdministrativeSex"));
			retVal.Add(new CodeSystem("CDCREC"));
			retVal.Add(new CodeSystem("CDT"));//already provided using procedure code tools
			retVal.Add(new CodeSystem("CPT"));
			retVal.Add(new CodeSystem("CVX"));
			retVal.Add(new CodeSystem("HCPCS"));
			retVal.Add(new CodeSystem("ICD10CM"));
			retVal.Add(new CodeSystem("ICD9CM"));//different than our old icd9 codes.
			retVal.Add(new CodeSystem("LOINC"));
			retVal.Add(new CodeSystem("RxNorm"));
			retVal.Add(new CodeSystem("SNOMEDCT"));
			retVal.Add(new CodeSystem("SOP"));
			retVal.Add(new CodeSystem("UCUM???"));
			retVal.Add(new CodeSystem("ThisOneDoesntExistOnTheServer"));
			string command="SELECT * FROM codesystem";
			List<CodeSystem> listCodeSystemsDB=Crud.CodeSystemCrud.SelectMany(command);
			for(int i=0;i<retVal.Count;i++) {
				for(int j=0;j<listCodeSystemsDB.Count;j++) {
					if(retVal[i].CodeSystemName==listCodeSystemsDB[j].CodeSystemName) {
						retVal[i]=listCodeSystemsDB[j];//updates retVal with values from the DB, to include current version and version available.
					}
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(CodeSystem codeSystem){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),codeSystem);
				return;
			}
			Crud.CodeSystemCrud.Update(codeSystem);
		}

		///<summary>Called after file is downloaded.Drops Table, Creates Table, Imports codes. Throws exceptions.</summary>
		public static void ImportAdministrativeSex() {
			string command="";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="DROP TABLE IF EXISTS administrativesex";
				Db.NonQ(command);
				command=@"CREATE TABLE administrativesex (
							AdministrativeSexNum bigint NOT NULL auto_increment PRIMARY KEY,
							CodeValue varchar(255) NOT NULL,
							DescriptionLong varchar(255) NOT NULL
							) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
			}
			else {//oracle
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE administrativesex'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				Db.NonQ(command);
				command=@"CREATE TABLE administrativesex (
							AdministrativeSexNum number(20) NOT NULL,
							CodeValue varchar2(255),
							DescriptionLong varchar2(255),
							CONSTRAINT administrativesex_Administr PRIMARY KEY (AdministrativeSexNum)
							)";
				Db.NonQ(command);
			}
			string destDir=ImageStore.GetPreferredAtoZpath();
			if(destDir==null) {//Not using A to Z folders?
				destDir=Path.GetTempPath();
			}
			string filepath=@"c:\OpenDentImages\CodeSystems\AdministrativeSex.txt";//TODO:point this to the AtoZimages folder to import codes.
			System.IO.StreamReader sr=new System.IO.StreamReader(filepath);
			sr.Peek();
			//Import AdministrativeSex Codes----------------------------------------------------------------------------------------------------------
			string[] arrayAdministrativeSex;
			AdministrativeSex administrativeSexTemp=new AdministrativeSex();
			while(!sr.EndOfStream) {//each loop should read exactly one line of code. and each line of code should be a unique LOINC code
				arrayAdministrativeSex=sr.ReadLine().Split('\t');
				administrativeSexTemp.CodeValue				=arrayAdministrativeSex[0];
				administrativeSexTemp.DescriptionLong	=arrayAdministrativeSex[1];
				AdministrativeSexes.Insert(administrativeSexTemp);
			}
			//TODO: delete code file?
		}


		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets one CodeSystem from the db.</summary>
		public static CodeSystem GetOne(long codeSystemNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CodeSystem>(MethodBase.GetCurrentMethod(),codeSystemNum);
			}
			return Crud.CodeSystemCrud.SelectOne(codeSystemNum);
		}

		///<summary></summary>
		public static long Insert(CodeSystem codeSystem){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				codeSystem.CodeSystemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),codeSystem);
				return codeSystem.CodeSystemNum;
			}
			return Crud.CodeSystemCrud.Insert(codeSystem);
		}

		///<summary></summary>
		public static void Delete(long codeSystemNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),codeSystemNum);
				return;
			}
			string command= "DELETE FROM codesystem WHERE CodeSystemNum = "+POut.Long(codeSystemNum);
			Db.NonQ(command);
		}
		*/



	}
}