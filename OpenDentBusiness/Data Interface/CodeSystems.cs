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

		///<summary>Returns a list of code all systems In the code system table.</summary>
		public static List<CodeSystem> GetForCurrentVersion() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CodeSystem>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM codesystem WHERE codesystemname!='AdministrativeSex'";
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
			List<string> codeList=Cdcrecs.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayCDCREC;
			Cdcrec cdcrecTemp=new Cdcrec();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayCDCREC=lines[i].Split('\t');
				if(codeList.Contains(arrayCDCREC[0])){//code already existed
					continue;
				}
				cdcrecTemp.CdcrecCode				=arrayCDCREC[0];
				cdcrecTemp.HeirarchicalCode	=arrayCDCREC[1];
				cdcrecTemp.Description			=arrayCDCREC[2];
				Cdcrecs.Insert(cdcrecTemp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
	//public static void ImportCDT(string tempFileName) ... not necessary.

		///<summary>Called after user provides resource file.  Throws exceptions.</summary>
		public static void ImportCpt(string tempFileName) {
			throw new Exception("Not Implemented.");
			//handled differently because users must download and provide resource files independantly
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportCvx(string tempFileName) {
			List<string> codeList=Cvxs.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayCvx;
			Cvx cvxTemp=new Cvx();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayCvx=lines[i].Split('\t');
				if(codeList.Contains(arrayCvx[0])) {//code already exists
					continue;
				}
				cvxTemp.CvxCode			=arrayCvx[0];
				cvxTemp.Description	=arrayCvx[1];
				Cvxs.Insert(cvxTemp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportHcpcs(string tempFileName) {
			List<string> codeList=Hcpcses.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayHCPCS;
			Hcpcs hcpcsTemp=new Hcpcs();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayHCPCS=lines[i].Split('\t');
				if(codeList.Contains(arrayHCPCS[0])) {//code already exists
					continue;
				}
				hcpcsTemp.HcpcsCode					=arrayHCPCS[0];
				hcpcsTemp.DescriptionShort	=arrayHCPCS[1];
				Hcpcses.Insert(hcpcsTemp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportIcd10(string tempFileName) {
			List<string> codeList=Icd10s.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayICD10;
			Icd10 icd10Temp=new Icd10();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayICD10=lines[i].Split('\t');
				if(codeList.Contains(arrayICD10[0])) {//code already exists
					continue;
				}
				icd10Temp.Icd10Code		=arrayICD10[0];
				icd10Temp.Description	=arrayICD10[1];
				icd10Temp.IsCode		=arrayICD10[2];
				Icd10s.Insert(icd10Temp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportIcd9(string tempFileName) {
			List<string> codeList=ICD9s.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayICD9;
			ICD9 icd9Temp=new ICD9();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayICD9=lines[i].Split('\t');
				if(codeList.Contains(arrayICD9[0])) {//code already exists
					continue;
				}
				icd9Temp.ICD9Code		=arrayICD9[0];
				icd9Temp.Description=arrayICD9[1];
				ICD9s.Insert(icd9Temp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportLoinc(string tempFileName) {
			List<string> codeList=Loincs.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayLoinc;
			Loinc loincTemp=new Loinc();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayLoinc=lines[i].Split('\t');
				if(codeList.Contains(arrayLoinc[0])) {//code already exists
					continue;
				}
				loincTemp.LoincCode								=arrayLoinc[0];
				loincTemp.Component								=arrayLoinc[1];
				loincTemp.PropertyObserved				=arrayLoinc[2];
				loincTemp.TimeAspct								=arrayLoinc[3];
				loincTemp.SystemMeasured					=arrayLoinc[4];
				loincTemp.ScaleType								=arrayLoinc[5];
				loincTemp.MethodType							=arrayLoinc[6];
				loincTemp.StatusOfCode						=arrayLoinc[7];
				loincTemp.NameShort								=arrayLoinc[8];
				loincTemp.ClassType								=PIn.Int(arrayLoinc[9]);
				loincTemp.UnitsRequired						=arrayLoinc[10]=="Y";
				loincTemp.OrderObs								=arrayLoinc[11];
				loincTemp.HL7FieldSubfieldID			=arrayLoinc[12];
				loincTemp.ExternalCopyrightNotice	=arrayLoinc[13];
				loincTemp.NameLongCommon					=arrayLoinc[14];
				loincTemp.UnitsUCUM								=arrayLoinc[15];
				loincTemp.RankCommonTests					=PIn.Int(arrayLoinc[16]);
				loincTemp.RankCommonOrders				=PIn.Int(arrayLoinc[17]);
				Loincs.Insert(loincTemp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportRxNorm(string tempFileName) {
			List<string> codeList=RxNorms.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arrayRxNorm;
			RxNorm rxNormTemp=new RxNorm();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arrayRxNorm=lines[i].Split('\t');
				if(codeList.Contains(arrayRxNorm[0])) {//code already exists
					continue;
				}
				rxNormTemp.RxCui				=arrayRxNorm[0];
				rxNormTemp.MmslCode			=arrayRxNorm[1];
				rxNormTemp.Description	=arrayRxNorm[2];
				RxNorms.Insert(rxNormTemp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportSnomed(string tempFileName) {
			List<string> codeList=Snomeds.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arraySnomed;
			Snomed snomedTemp=new Snomed();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arraySnomed=lines[i].Split('\t');
				if(codeList.Contains(arraySnomed[0])) {//code already exists
					continue;
				}
				snomedTemp.SnomedCode		=arraySnomed[0];
				snomedTemp.Description	=arraySnomed[1];
				Snomeds.Insert(snomedTemp);
			}
			File.Delete(tempFileName);
		}

		///<summary>Called after file is downloaded.  Throws exceptions.</summary>
		public static void ImportSop(string tempFileName) {
			List<string> codeList=Sops.GetAllCodes();
			string[] lines=File.ReadAllLines(tempFileName);
			string[] arraySop;
			Sop sopTemp=new Sop();
			for(int i=0;i<lines.Length;i++) {//each loop should read exactly one line of code. and each line of code should be a unique code
				arraySop=lines[i].Split('\t');
				if(codeList.Contains(arraySop[0])) {//code already exists
					continue;
				}
				sopTemp.SopCode			=arraySop[0];
				sopTemp.Description	=arraySop[1];
				Sops.Insert(sopTemp);
			}
			File.Delete(tempFileName);
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