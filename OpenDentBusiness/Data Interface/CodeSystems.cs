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

		///<summary>Returns a list of code systems in the code system table.  This query will change from version to version depending on what code systems we have available.</summary>
		public static List<CodeSystem> GetForCurrentVersion() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CodeSystem>>(MethodBase.GetCurrentMethod());
			}
			//string command="SELECT * FROM codesystem WHERE CodeSystemName!='AdministrativeSex' AND CodeSystemName!='CDT'";
#if DEBUG
			string command="SELECT * FROM codesystem";// WHERE CodeSystemName IN ('ICD9CM','RXNORM','SNOMEDCT','CPT')";
#else
			string command="SELECT * FROM codesystem WHERE CodeSystemName IN ('ICD9CM','RXNORM','SNOMEDCT','CPT')";
#endif
			return Crud.CodeSystemCrud.SelectMany(command);
		}

		///<summary>Returns a list of code systems in the code system table.  This query will change from version to version depending on what code systems we have available.</summary>
		public static List<CodeSystem> GetForCurrentVersionNoSnomed() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CodeSystem>>(MethodBase.GetCurrentMethod());
			}
			//string command="SELECT * FROM codesystem WHERE CodeSystemName!='AdministrativeSex' AND CodeSystemName!='CDT'";
			string command="SELECT * FROM codesystem WHERE CodeSystemName IN ('ICD9CM','RXNORM','CPT')";
			return Crud.CodeSystemCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Update(CodeSystem codeSystem){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),codeSystem);
				return;
			}
			Crud.CodeSystemCrud.Update(codeSystem);
		}

		///<summary>Updates VersionCurrent to the VersionAvail of the codeSystem object passed in. Used by code system importer after successful import.</summary>
		public static void UpdateCurrentVersion(CodeSystem codeSystem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),codeSystem);
				return;
			}
			codeSystem.VersionCur=codeSystem.VersionAvail;
			Crud.CodeSystemCrud.Update(codeSystem);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
	//public static void ImportAdministrativeSex(string tempFileName) ... not necessary.

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportCdcrec(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Cdcrecs.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayCDCREC;
			Cdcrec cdcrec=new Cdcrec();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayCDCREC=lines[i].Split('\t');
				if(codeHash.Contains(arrayCDCREC[0])) {//code already existed
					continue;
				}
				cdcrec.CdcrecCode				=arrayCDCREC[0];
				cdcrec.HeirarchicalCode	=arrayCDCREC[1];
				cdcrec.Description			=arrayCDCREC[2];
				Cdcrecs.Insert(cdcrec);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
	//public static void ImportCDT(string tempFileName) ... not necessary.

		///<summary>Called after user provides resource file.  Throws exceptions.</summary>
		public static void ImportCpt(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Cpts.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayCpt;
			bool isHeader=true;
			Cpt cpt=new Cpt();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				if(isHeader) {
					if(!lines[i].Contains("\t")) {
						continue;//Copyright info is present at the head of the file.
					}
					isHeader=false;
				}
				arrayCpt=lines[i].Split('\t');
				if(codeHash.Contains(arrayCpt[0])) {//code already exists
					continue;
				}
				cpt.CptCode			=arrayCpt[0];
				cpt.Description	=arrayCpt[1];
				Cpts.Insert(cpt);
			}
			//File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportCvx(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Cvxs.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayCvx;
			Cvx cvx=new Cvx();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayCvx=lines[i].Split('\t');
				if(codeHash.Contains(arrayCvx[0])) {//code already exists
					continue;
				}
				cvx.CvxCode			=arrayCvx[0];
				cvx.Description	=arrayCvx[1];
				Cvxs.Insert(cvx);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportHcpcs(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Hcpcses.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayHCPCS;
			Hcpcs hcpcs=new Hcpcs();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayHCPCS=lines[i].Split('\t');
				if(codeHash.Contains(arrayHCPCS[0])) {//code already exists
					continue;
				}
				hcpcs.HcpcsCode					=arrayHCPCS[0];
				hcpcs.DescriptionShort	=arrayHCPCS[1];
				Hcpcses.Insert(hcpcs);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportIcd10(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Icd10s.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayICD10;
			Icd10 icd10=new Icd10();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayICD10=lines[i].Split('\t');
				if(codeHash.Contains(arrayICD10[0])) {//code already exists
					continue;
				}
				icd10.Icd10Code		=arrayICD10[0];
				icd10.Description	=arrayICD10[1];
				icd10.IsCode			=arrayICD10[2];
				Icd10s.Insert(icd10);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportIcd9(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			//Customers may have an old codeset that has a truncated uppercase description, if so we want to update with new descriptions.
			bool IsOldDescriptions=ICD9s.IsOldDescriptions();
			HashSet<string> codeHash=new HashSet<string>(ICD9s.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayICD9;
			ICD9 icd9=new ICD9();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayICD9=lines[i].Split('\t');
				if(codeHash.Contains(arrayICD9[0])) {//code already exists
					if(!IsOldDescriptions) {
						continue;//code exists and has updated description
					}
					string command="UPDATE icd9 SET description='"+POut.String(arrayICD9[1])+"' WHERE ICD9Code='"+POut.String(arrayICD9[0])+"'";
					Db.NonQ(command);
					continue;//we have updated the description of an existing code.
				}
				icd9.ICD9Code		=arrayICD9[0];
				icd9.Description=arrayICD9[1];
				ICD9s.Insert(icd9);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportLoinc(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Loincs.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayLoinc;
			Loinc loinc=new Loinc();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayLoinc=lines[i].Split('\t');
				if(codeHash.Contains(arrayLoinc[0])) {//code already exists
					continue;
				}
				loinc.LoincCode								=arrayLoinc[0];
				loinc.Component								=arrayLoinc[1];
				loinc.PropertyObserved				=arrayLoinc[2];
				loinc.TimeAspct								=arrayLoinc[3];
				loinc.SystemMeasured					=arrayLoinc[4];
				loinc.ScaleType								=arrayLoinc[5];
				loinc.MethodType							=arrayLoinc[6];
				loinc.StatusOfCode						=arrayLoinc[7];
				loinc.NameShort								=arrayLoinc[8];
				loinc.ClassType								=arrayLoinc[9];
				loinc.UnitsRequired						=arrayLoinc[10]=="Y";
				loinc.OrderObs								=arrayLoinc[11];
				loinc.HL7FieldSubfieldID			=arrayLoinc[12];
				loinc.ExternalCopyrightNotice	=arrayLoinc[13];
				loinc.NameLongCommon					=arrayLoinc[14];
				loinc.UnitsUCUM								=arrayLoinc[15];
				loinc.RankCommonTests					=PIn.Int(arrayLoinc[16]);
				loinc.RankCommonOrders				=PIn.Int(arrayLoinc[17]);
				Loincs.Insert(loinc);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportRxNorm(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(RxNorms.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayRxNorm;
			RxNorm rxNorm=new RxNorm();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayRxNorm=lines[i].Split('\t');
				if(codeHash.Contains(arrayRxNorm[0])) {//code already exists
					continue;
				}
				rxNorm.RxCui				=arrayRxNorm[0];
				rxNorm.MmslCode			=arrayRxNorm[1];
				rxNorm.Description	=arrayRxNorm[2];
				RxNorms.Insert(rxNorm);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportSnomed(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Snomeds.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arraySnomed;
			Snomed snomed=new Snomed();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arraySnomed=lines[i].Split('\t');
				if(codeHash.Contains(arraySnomed[0])) {//code already exists
					continue;
				}
				snomed.SnomedCode		=arraySnomed[0];
				snomed.Description	=arraySnomed[1];
				Snomeds.Insert(snomed);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportSop(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Sops.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arraySop;
			Sop sop=new Sop();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arraySop=lines[i].Split('\t');
				if(codeHash.Contains(arraySop[0])) {//code already exists
					continue;
				}
				sop.SopCode			=arraySop[0];
				sop.Description	=arraySop[1];
				Sops.Insert(sop);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportUcum(string tempFileName) {
			if(tempFileName==null) {
				return;
			}
			HashSet<string> codeHash=new HashSet<string>(Ucums.GetAllCodes());
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayUcum;
			Ucum ucum=new Ucum();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayUcum=lines[i].Split('\t');
				if(codeHash.Contains(arrayUcum[0])) {//code already exists
					continue;
				}
				ucum.UcumCode			=arrayUcum[0];
				ucum.Description	=arrayUcum[1];
				ucum.IsInUse			=false;
				Ucums.Insert(ucum);
			}
			File.Delete(tempFileName);
		}

		///<summary>Returns number of codes imported.</summary>
		/// <param name="tempFile"></param>
		/// <param name="codeCount">Returns number of new codes inserted.</param>
		/// <param name="totalCodes">Returns number of total codes found.</param>
		/// <returns></returns>
//		public static void ImportEhrCodes(string tempFile,out int newCodeCount,out int totalCodeCount,out int availableCodeCount){
//			newCodeCount=0;
//			totalCodeCount=0;
//			availableCodeCount=0;
//			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
//				Meth.GetVoid(MethodBase.GetCurrentMethod(),tempFile,newCodeCount,totalCodeCount,availableCodeCount);
//				return;
//			}
//			//UNION ALL to speed up query.  Used to determine what codes to add to DB.
//			string command=@"SELECT CdcrecCode FROM cdcrec
//											UNION ALL
//											SELECT ProcCode FROM procedurecode
//											UNION ALL
//											SELECT CptCode FROM cpt
//											UNION ALL
//											SELECT CvxCode FROM cvx
//											UNION ALL
//											SELECT HcpcsCode FROM hcpcs
//											UNION ALL
//											SELECT Icd10Code FROM icd10
//											UNION ALL
//											SELECT ICD9Code FROM icd9
//											UNION ALL
//											SELECT LoincCode FROM loinc
//											UNION ALL
//											SELECT RxCui FROM rxnorm
//											UNION ALL
//											SELECT SnomedCode FROM snomed
//											UNION ALL
//											SELECT SopCode FROM sop";
//			DataTable T = DataCore.GetTable(command);
//			HashSet<string> allCodeHash=new HashSet<string>();
//			for(int i=0;i<T.Rows.Count;i++) {
//				allCodeHash.Add(T.Rows[i][0].ToString());
//			}
//			HashSet<string> ehrCodeHash=EhrCodes.GetAllCodesHashSet();
//			string[] lines=File.ReadAllLines(tempFile);
//			string[] arrayEHRCode;
//			EhrCode ehrc=new EhrCode();
//			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
//				arrayEHRCode=lines[i].Split('\t');
//				if(!allCodeHash.Contains(arrayEHRCode[0]) && arrayEHRCode[6]!="AdministrativeSex") {//exception for AdministrativeSex because it is not stored in the DB.
//					continue;//code does not exist in the database in one of the standard code system tables.
//				}
//				if(ehrCodeHash.Contains(arrayEHRCode[4]+arrayEHRCode[2])) {
//					continue;//Code already inserted in ehrCodes table
//				}
//				ehrc.MeasureIds		=arrayEHRCode[0];
//				ehrc.ValueSetName	=arrayEHRCode[1];
//				ehrc.ValueSetOID	=arrayEHRCode[2];
//				ehrc.QDMCategory	=arrayEHRCode[3];
//				ehrc.CodeValue		=arrayEHRCode[4];
//				ehrc.Description	=arrayEHRCode[5];
//				ehrc.CodeSystem		=arrayEHRCode[6];
//				ehrc.CodeSystemOID=arrayEHRCode[7];
//				EhrCodes.Insert(ehrc);
//				newCodeCount++;//return value
//			}
//			File.Delete(tempFile);
//			totalCodeCount=ehrCodeHash.Count+newCodeCount;//return value
//			availableCodeCount=lines.Length;//return value
//		}


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