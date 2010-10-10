using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcedureCodes {
		public const string GroupProcCode="~GRP~";

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM procedurecode ORDER BY ProcCat,ProcCode";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="ProcedureCode";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			ProcedureCodeC.Listt=Crud.ProcedureCodeCrud.TableToList(table);
			ProcedureCodeC.HList=new Hashtable();
			for(int i=0;i<ProcedureCodeC.Listt.Count;i++) {
				try {
					ProcedureCodeC.HList.Add(ProcedureCodeC.Listt[i].ProcCode,ProcedureCodeC.Listt[i].Copy());
				}
				catch {
				}
			}
		}

		public static List<ProcedureCode> GetUAppoint(DateTime changedSince){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcedureCode>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM procedurecode WHERE DateTStamp > "+POut.DateT(changedSince);
			return Crud.ProcedureCodeCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(ProcedureCode code) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				code.CodeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),code);
				return code.CodeNum;
			}
			//must have already checked procCode for nonduplicate.
			return Crud.ProcedureCodeCrud.Insert(code);
		}

		///<summary></summary>
		public static void Update(ProcedureCode code){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),code);
				return;
			}
			Crud.ProcedureCodeCrud.Update(code);
		}

		///<summary>Returns the ProcedureCode for the supplied procCode such as such as D####.</summary>
		public static ProcedureCode GetProcCode(string myCode){
			//No need to check RemotingRole; no call to db.
			if(myCode==null){
				//MessageBox.Show(Lans.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			if(ProcedureCodeC.HList.Contains(myCode)) {
				return (ProcedureCode)ProcedureCodeC.HList[myCode];
			}
			else{
				return new ProcedureCode();
			}
		}

		///<summary>The new way of getting a procCode. Uses the primary key instead of string code.</summary>
		public static ProcedureCode GetProcCode(long codeNum) {
			//No need to check RemotingRole; no call to db.
			if(codeNum==0) {
				//MessageBox.Show(Lans.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			for(int i=0;i<ProcedureCodeC.Listt.Count;i++) {
				if(ProcedureCodeC.Listt[i].CodeNum==codeNum) {
					return ProcedureCodeC.Listt[i];
				}
			}
			return new ProcedureCode();
		}

		///<summary>Gets code from db to avoid having to constantly refresh in FormProcCodes</summary>
		public static ProcedureCode GetProcCodeFromDb(long codeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ProcedureCode>(MethodBase.GetCurrentMethod(),codeNum);
			}
			ProcedureCode retval=Crud.ProcedureCodeCrud.SelectOne(codeNum);
			if(retval==null) {
				//We clasically return an empty procedurecode object here instead of null.
				return new ProcedureCode();
			}
			return retval;
		}

		///<summary>Supply the human readable proc code such as D####</summary>
		public static long GetCodeNum(string myCode) {
			//No need to check RemotingRole; no call to db.
			if(myCode==null || myCode=="") {
				return 0;
			}
			if(ProcedureCodeC.HList.Contains(myCode)) {
				return ((ProcedureCode)ProcedureCodeC.HList[myCode]).CodeNum;
			}
			return 0;
			//else {
			//	throw new ApplicationException("Missing code");
			//}
		}

		///<summary>If a substitute exists for the given proc code, then it will give the CodeNum of that code.  Otherwise, it will return the codeNum for the given procCode.</summary>
		public static long GetSubstituteCodeNum(string procCode,string toothNum) {
			//No need to check RemotingRole; no call to db.
			if(procCode==null || procCode=="") {
				return 0;
			}
			if(!ProcedureCodeC.HList.Contains(procCode)) {
				return 0;
			}
			ProcedureCode proc=(ProcedureCode)ProcedureCodeC.HList[procCode];
			if(proc.SubstitutionCode!="" && ProcedureCodeC.HList.Contains(proc.SubstitutionCode)) {
				if(proc.SubstOnlyIf==SubstitutionCondition.Always){
					return ((ProcedureCode)ProcedureCodeC.HList[proc.SubstitutionCode]).CodeNum;
				}
				if(proc.SubstOnlyIf==SubstitutionCondition.Molar && Tooth.IsMolar(toothNum)){
					return ((ProcedureCode)ProcedureCodeC.HList[proc.SubstitutionCode]).CodeNum;
				}
				if(proc.SubstOnlyIf==SubstitutionCondition.SecondMolar && Tooth.IsSecondMolar(toothNum)) {
					return ((ProcedureCode)ProcedureCodeC.HList[proc.SubstitutionCode]).CodeNum;
				}
			}
			return proc.CodeNum;
		}

		public static string GetStringProcCode(long codeNum) {
			//No need to check RemotingRole; no call to db.
			if(codeNum==0) {
				return "";
				//throw new ApplicationException("CodeNum cannot be zero.");
			}
			for(int i=0;i<ProcedureCodeC.Listt.Count;i++) {
				if(ProcedureCodeC.Listt[i].CodeNum==codeNum) {
					return ProcedureCodeC.Listt[i].ProcCode;
				}
			}
			throw new ApplicationException("Missing codenum");
		}

		///<summary></summary>
		public static bool IsValidCode(string myCode){
			//No need to check RemotingRole; no call to db.
			if(myCode==null || myCode=="") {
				return false;
			}
			if(ProcedureCodeC.HList.Contains(myCode)) {
				return true;
			}
			else {
				return false;
			}
		}

		///<summary>Grouped by Category.  Used only in FormRpProcCodes.</summary>
		public static ProcedureCode[] GetProcList(){
			//No need to check RemotingRole; no call to db.
			List<ProcedureCode> retVal=new List<ProcedureCode>();
			for(int j=0;j<DefC.Short[(int)DefCat.ProcCodeCats].Length;j++){
				for(int k=0;k<ProcedureCodeC.Listt.Count;k++) {
					if(DefC.Short[(int)DefCat.ProcCodeCats][j].DefNum==ProcedureCodeC.Listt[k].ProcCat) {
						retVal.Add(ProcedureCodeC.Listt[k].Copy());
					}
				}
			}
			return retVal.ToArray();
		}

		///<summary>Gets a list of procedure codes directly from the database.  If categories.length==0, then we will get for all categories.  Categories are defnums.  FeeScheds are, for now, defnums.</summary>
		public static DataTable GetProcTable(string abbr,string desc,string code,List<long> categories,long feeSched,
			long feeSchedComp1,long feeSchedComp2) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),abbr,desc,code,categories,feeSched,feeSchedComp1,feeSchedComp2);
			}
			string whereCat;
			if(categories.Count==0){
				whereCat="1";
			}
			else{
				whereCat="(";
				for(int i=0;i<categories.Count;i++){
					if(i>0){
						whereCat+=" OR ";
					}
					whereCat+="ProcCat="+POut.Long(categories[i]);
				}
				whereCat+=")";
			}
			//Query changed to be compatible with both MySQL and Oracle (not tested).
			string command="SELECT ProcCat,Descript,AbbrDesc,procedurecode.ProcCode,"
				+"CASE WHEN (fee1.Amount IS NULL) THEN -1 ELSE fee1.Amount END FeeAmt1,"
				+"CASE WHEN (fee2.Amount IS NULL) THEN -1 ELSE fee2.Amount END FeeAmt2,"
				+"CASE WHEN (fee3.Amount IS NULL) THEN -1 ELSE fee3.Amount END FeeAmt3, "
				+"procedurecode.CodeNum "
				+"FROM procedurecode "
				+"LEFT JOIN fee fee1 ON fee1.CodeNum=procedurecode.CodeNum AND fee1.FeeSched="+POut.Long(feeSched)+" "
				+"LEFT JOIN fee fee2 ON fee2.CodeNum=procedurecode.CodeNum AND fee2.FeeSched="+POut.Long(feeSchedComp1)+" "
				+"LEFT JOIN fee fee3 ON fee3.CodeNum=procedurecode.CodeNum AND fee3.FeeSched="+POut.Long(feeSchedComp2)+" "
				+"LEFT JOIN definition ON definition.DefNum=procedurecode.ProcCat "
				+"WHERE "+whereCat
				+" AND Descript LIKE '%"+POut.String(desc)+"%' "
				+"AND AbbrDesc LIKE '%"+POut.String(abbr)+"%' "
				+"AND procedurecode.ProcCode LIKE '%"+POut.String(code)+"%' "
				+"ORDER BY definition.ItemOrder,procedurecode.ProcCode";
			return Db.GetTable(command);
		}

		///<summary>Returns the LaymanTerm for the supplied codeNum, or the description if none present.</summary>
		public static string GetLaymanTerm(long codeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProcedureCodeC.Listt.Count;i++) {
				if(ProcedureCodeC.Listt[i].CodeNum==codeNum) {
					if(ProcedureCodeC.Listt[i].LaymanTerm !="") {
						return ProcedureCodeC.Listt[i].LaymanTerm;
					}
					return ProcedureCodeC.Listt[i].Descript;
				}
			}
			return "";
		}

		///<summary>Used to check whether codes starting with T exist and are in a visible category.  If so, it moves them to the Obsolete category.  If the T code has never been used, then it deletes it.</summary>
		public static void TcodesClear() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			//first delete any unused T codes
			string command=@"SELECT CodeNum,ProcCode FROM procedurecode
				WHERE NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.CodeNum=procedurecode.CodeNum)
				AND ProcCode LIKE 'T%'";
			DataTable table=Db.GetTable(command);
			long codenum;
			for(int i=0;i<table.Rows.Count;i++) {
				codenum=PIn.Long(table.Rows[i]["CodeNum"].ToString());
				command="DELETE FROM fee WHERE CodeNum="+POut.Long(codenum);
				Db.NonQ(command);
				command="DELETE FROM procedurecode WHERE CodeNum="+POut.Long(codenum);
				Db.NonQ(command);
			}
			//then, move any other T codes to obsolete category
			command=@"SELECT DISTINCT ProcCat FROM procedurecode,definition 
				WHERE procedurecode.ProcCode LIKE 'T%'
				AND definition.IsHidden=0
				AND procedurecode.ProcCat=definition.DefNum";
			table=Db.GetTable(command);
			long catNum=DefC.GetByExactName(DefCat.ProcCodeCats,"Obsolete");//check to make sure an Obsolete category exists.
			Def def;
			if(catNum!=0) {//if a category exists with that name
				def=DefC.GetDef(DefCat.ProcCodeCats,catNum);
				if(!def.IsHidden) {
					def.IsHidden=true;
					Defs.Update(def);
					Defs.RefreshCache();
				}
			}
			if(catNum==0) {
				def=new Def();
				def.Category=DefCat.ProcCodeCats;
				def.ItemName="Obsolete";
				def.ItemOrder=DefC.Long[(int)DefCat.ProcCodeCats].Length;
				def.IsHidden=true;
				Defs.Insert(def);
				Defs.RefreshCache();
				catNum=def.DefNum;
			}
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE procedurecode SET ProcCat="+POut.Long(catNum)
					+" WHERE ProcCat="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			//finally, set Never Used category to be hidden.  This isn't really part of clearing Tcodes, but is required
			//because many customers won't have that category hidden
			catNum=DefC.GetByExactName(DefCat.ProcCodeCats,"Never Used");
			if(catNum!=0) {//if a category exists with that name
				def=DefC.GetDef(DefCat.ProcCodeCats,catNum);
				if(!def.IsHidden) {
					def.IsHidden=true;
					Defs.Update(def);
					Defs.RefreshCache();
				}
			}
		}

		public static void ResetApptProcsQuickAdd() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command= "DELETE FROM definition WHERE Category=3";
			Db.NonQ(command);
			string[] array=new string[] {
				"CompEx-4BW-Pano-Pro-Flo","D0150,D0274,D0330,D1110,D1204",
				"CompEx-2BW-Pano-ChPro-Flo","D0150,D0272,D0330,D1120,D1203",
				"PerEx-4BW-Pro-Flo","D0120,D0274,D1110,D1204",
				"LimEx-PA","D0140,D0220",
				"PerEx-4BW-Pro-Flo","D0120,D0274,D1110,D1204",
				"PerEx-2BW-ChildPro-Flo","D0120,D0272,D1120,D1203",
				"Comp Exam","D0150",
				"Per Exam","D0120",
				"Lim Exam","D0140",
				"1 PA","D0220",
				"2BW","D0272",
				"4BW","D0274",
				"Pano","D0330",
				"Pro Adult","D1110",
				"Fluor Adult","D1204",
				"Pro Child","D1120",
				"Fuor Child","D1203",
				"PostOp","N4101",
				"DentAdj","N4102",
				"Consult","D9310"
			};
			Def def;
			string[] codelist;
			bool allvalid;
			int itemorder=0;
			for(int i=0;i<array.Length;i+=2) {
				//first, test all procedures for valid
				codelist=array[i+1].Split(',');
				allvalid=true;
				for(int c=0;c<codelist.Length;c++) {
					if(!ProcedureCodes.IsValidCode(codelist[c])) {
						allvalid=false;
					}
				}
				if(!allvalid) {
					continue;
				}
				def=new Def();
				def.Category=DefCat.ApptProcsQuickAdd;
				def.ItemOrder=itemorder;
				def.ItemName=array[i];
				def.ItemValue=array[i+1];
				Defs.Insert(def);
				itemorder++;
			}
		}

		///<summary>Resets the descriptions for all ADA codes to the official wording.  Required by the license.</summary>
		public static void ResetADAdescriptions() {
			//No need to check RemotingRole; no call to db.
			ResetADAdescriptions(CDT.Class1.GetADAcodes());
		}

		///<summary>Resets the descriptions for all ADA codes to the official wording.  Required by the license.</summary>
		public static void ResetADAdescriptions(List<ProcedureCode> codeList) {
			//No need to check RemotingRole; no call to db.
			ProcedureCode code;
			for(int i=0;i<codeList.Count;i++) {
				if(!ProcedureCodes.IsValidCode(codeList[i].ProcCode)) {
					continue;
				}
				code=ProcedureCodes.GetProcCode(codeList[i].ProcCode);
				code.Descript=codeList[i].Descript;
				ProcedureCodes.Update(code);
			}
			//don't forget to refresh procedurecodes.
		}



	}

	
	
	


}