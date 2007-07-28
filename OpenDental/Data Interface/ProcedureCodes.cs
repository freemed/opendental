using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Text.RegularExpressions;

namespace OpenDental{
	///<summary></summary>
	public class ProcedureCodes {
		///<summary></summary>
		private static DataTable tableStat;
		///<summary></summary>
		public static ArrayList RecallAL;
		///<summary>key:ProcCode, value:ProcedureCode</summary>
		public static Hashtable HList;//
		///<summary></summary>
		public static ProcedureCode[] List;

		///<summary></summary>
		public static void Refresh() {
			HList=new Hashtable();
			ProcedureCode tempCode=new ProcedureCode();
			string command="SELECT * FROM procedurecode ORDER BY ProcCat,ProcCode";
			DataTable table=General.GetTable(command);
			tableStat=table.Copy();
			RecallAL=new ArrayList();
			List=new ProcedureCode[tableStat.Rows.Count];
			for(int i=0;i<tableStat.Rows.Count;i++) {
				tempCode=new ProcedureCode();
				tempCode.CodeNum    	 =PIn.PInt(tableStat.Rows[i][0].ToString());
				tempCode.ProcCode      =PIn.PString(tableStat.Rows[i][1].ToString());
				tempCode.Descript      =PIn.PString(tableStat.Rows[i][2].ToString());
				tempCode.AbbrDesc      =PIn.PString(tableStat.Rows[i][3].ToString());
				tempCode.ProcTime      =PIn.PString(tableStat.Rows[i][4].ToString());
				tempCode.ProcCat       =PIn.PInt(tableStat.Rows[i][5].ToString());
				tempCode.TreatArea     =(TreatmentArea)PIn.PInt(tableStat.Rows[i][6].ToString());
				//tempCode.RemoveTooth   =PIn.PBool  (tableStat.Rows[i][7].ToString());
				tempCode.SetRecall     =PIn.PBool(tableStat.Rows[i][8].ToString());
				tempCode.NoBillIns     =PIn.PBool(tableStat.Rows[i][9].ToString());
				tempCode.IsProsth      =PIn.PBool(tableStat.Rows[i][10].ToString());
				tempCode.DefaultNote   =PIn.PString(tableStat.Rows[i][11].ToString());
				tempCode.IsHygiene     =PIn.PBool(tableStat.Rows[i][12].ToString());
				tempCode.GTypeNum      =PIn.PInt(tableStat.Rows[i][13].ToString());
				tempCode.AlternateCode1=PIn.PString(tableStat.Rows[i][14].ToString());
				tempCode.MedicalCode   =PIn.PString(tableStat.Rows[i][15].ToString());
				tempCode.IsTaxed       =PIn.PBool(tableStat.Rows[i][16].ToString());
				tempCode.PaintType     =(ToothPaintingType)PIn.PInt(tableStat.Rows[i][17].ToString());
				tempCode.GraphicColor  =Color.FromArgb(PIn.PInt(tableStat.Rows[i][18].ToString()));
				tempCode.LaymanTerm    =PIn.PString(tableStat.Rows[i][19].ToString());
				tempCode.IsCanadianLab =PIn.PBool  (tableStat.Rows[i][20].ToString());
				tempCode.PreExisting	 =PIn.PBool  (tableStat.Rows[i][21].ToString());
				HList.Add(tempCode.ProcCode,tempCode.Copy());
				List[i]=tempCode.Copy();
				if(tempCode.SetRecall) {
					RecallAL.Add(tempCode);
				}
			}
		}

		///<summary></summary>
		public static void Insert(ProcedureCode code){
			//must have already checked procCode for nonduplicate.
			string command="INSERT INTO procedurecode (CodeNum,ProcCode,descript,abbrdesc,"
				+"proctime,proccat,treatarea,RemoveTooth,setrecall,"
				+"nobillins,isprosth,defaultnote,ishygiene,gtypenum,alternatecode1,MedicalCode,IsTaxed,"
				+"PaintType,GraphicColor,LaymanTerm,IsCanadianLab,PreExisting) VALUES("
				+"'"+POut.PInt(code.CodeNum)+"', "
				+"'"+POut.PString(code.ProcCode)+"', "
				+"'"+POut.PString(code.Descript)+"', "
				+"'"+POut.PString(code.AbbrDesc)+"', "
				+"'"+POut.PString(code.ProcTime)+"', "
				+"'"+POut.PInt   (code.ProcCat)+"', "
				+"'"+POut.PInt   ((int)code.TreatArea)+"', "
				+"'0', " //No longer used, but remains part of the table so that ordinal values are not upset.
									//The value is set to 0 here, so that conversion to extraction paint type is not necessary.
				+"'"+POut.PBool  (code.SetRecall)+"', "
				+"'"+POut.PBool  (code.NoBillIns)+"', "
				+"'"+POut.PBool  (code.IsProsth)+"', "
				+"'"+POut.PString(code.DefaultNote)+"', "
				+"'"+POut.PBool  (code.IsHygiene)+"', "
				+"'"+POut.PInt   (code.GTypeNum)+"', "
				+"'"+POut.PString(code.AlternateCode1)+"', "
				+"'"+POut.PString(code.MedicalCode)+"', "
				+"'"+POut.PBool  (code.IsTaxed)+"', "
				+"'"+POut.PInt   ((int)code.PaintType)+"', "
				+"'"+POut.PInt   (code.GraphicColor.ToArgb())+"', "
				+"'"+POut.PString(code.LaymanTerm)+"', "
				+"'"+POut.PBool  (code.IsCanadianLab)+"', "
				+"'"+POut.PBool  (code.PreExisting)+"')";
			code.CodeNum=General.NonQ(command,true);
			ProcedureCodes.Refresh();
			//Cur already set
			//MessageBox.Show(Cur.PayNum.ToString());
		}

		///<summary></summary>
		public static void Update(ProcedureCode code){
			//MessageBox.Show("Updating");
			string command="UPDATE procedurecode SET " 
				//+ "ProcCode = '"       +POut.PString(code.ProcCode)+"'"
				+ "descript = '"       +POut.PString(code.Descript)+"'"
				+ ",abbrdesc = '"      +POut.PString(code.AbbrDesc)+"'"
				+ ",proctime = '"      +POut.PString(code.ProcTime)+"'"
				+ ",proccat = '"       +POut.PInt   (code.ProcCat)+"'"
				+ ",treatarea = '"     +POut.PInt   ((int)code.TreatArea)+"'"
				//+ ",removetooth = '"   +POut.PBool  (RemoveTooth)+"'"
				+ ",setrecall = '"     +POut.PBool  (code.SetRecall)+"'"
				+ ",nobillins = '"     +POut.PBool  (code.NoBillIns)+"'"
				+ ",isprosth = '"      +POut.PBool  (code.IsProsth)+"'"
				+ ",defaultnote = '"   +POut.PString(code.DefaultNote)+"'"
				+ ",ishygiene = '"     +POut.PBool  (code.IsHygiene)+"'"
				+ ",gtypenum = '"      +POut.PInt   (code.GTypeNum)+"'"
				+ ",alternatecode1 = '"+POut.PString(code.AlternateCode1)+"'"
				+ ",MedicalCode = '"   +POut.PString(code.MedicalCode)+"'"
				+ ",IsTaxed = '"       +POut.PBool  (code.IsTaxed)+"'"
				+ ",PaintType = '"     +POut.PInt   ((int)code.PaintType)+"'"
				+ ",GraphicColor = '"  +POut.PInt   (code.GraphicColor.ToArgb())+"'"
				+ ",LaymanTerm = '"    +POut.PString(code.LaymanTerm)+"'"
				+ ",IsCanadianLab = '" +POut.PBool  (code.IsCanadianLab)+"'"
				+ ",PreExisting = '"	 +POut.PBool(code.PreExisting)+"'"
				+" WHERE CodeNum = '"+POut.PInt(code.CodeNum)+"'";
			General.NonQ(command);
		}

		///<summary>Returns the ProcedureCode for the supplied procCode.</summary>
		public static ProcedureCode GetProcCode(string myCode){
			if(myCode==null){
				MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			if(HList.Contains(myCode)){
				return (ProcedureCode)HList[myCode];
			}
			else{
				return new ProcedureCode();
			}
		}

		///<summary>The new way of getting a procCode. Uses the primary key instead of string code.</summary>
		public static ProcedureCode GetProcCode(int codeNum) {
			if(codeNum==0) {
				//MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
				return new ProcedureCode();
			}
			for(int i=0;i<List.Length;i++){
				if(List[i].CodeNum==codeNum){
					return List[i];
				}
			}
			return new ProcedureCode();
		}

		public static int GetCodeNum(string myCode){
			if(myCode==null || myCode=="") {
				return 0;
			}
			if(HList.Contains(myCode)) {
				return ((ProcedureCode)HList[myCode]).CodeNum;
			}
			return 0;
			//else {
			//	throw new ApplicationException("Missing code");
			//}
		}

		public static string GetStringProcCode(int codeNum) {
			if(codeNum==0) {
				return "";
				//throw new ApplicationException("CodeNum cannot be zero.");
			}
			for(int i=0;i<List.Length;i++) {
				if(List[i].CodeNum==codeNum) {
					return List[i].ProcCode;
				}
			}
			throw new ApplicationException("Missing codenum");
		}

		///<summary></summary>
		public static bool IsValidCode(string myCode){
			if(myCode==null || myCode=="") {
				return false;
			}
			if(HList.Contains(myCode)) {
				return true;
			}
			else {
				return false;
			}
		}

		///<summary>Grouped by Category.  Used in Procedures window and in FormRpProcCodes.</summary>
		public static ProcedureCode[] GetProcList(){
			//ProcedureCode[] ProcList=new ProcedureCode[tableStat.Rows.Count];
			//int i=0;
			ProcedureCode procCode;
			ArrayList AL=new ArrayList();
			for(int j=0;j<DefB.Short[(int)DefCat.ProcCodeCats].Length;j++){
				for(int k=0;k<tableStat.Rows.Count;k++){
					if(DefB.Short[(int)DefCat.ProcCodeCats][j].DefNum==PIn.PInt(tableStat.Rows[k][5].ToString())){
						procCode=new ProcedureCode();
						procCode.CodeNum=PIn.PInt(tableStat.Rows[k][0].ToString());
						procCode.ProcCode = PIn.PString(tableStat.Rows[k][1].ToString());
						procCode.Descript= PIn.PString(tableStat.Rows[k][2].ToString());
						procCode.AbbrDesc= PIn.PString(tableStat.Rows[k][3].ToString());
						procCode.ProcCat = PIn.PInt   (tableStat.Rows[k][5].ToString());
						AL.Add(procCode);
						//i++;
					}
				}
			}
			/*for(int k=0;k<tableStat.Rows.Count;k++){
				if(PIn.PInt(tableStat.Rows[k][4].ToString())==255){
					ProcList[i]=new ProcedureCode();
					ProcList[i].ProcCode = PIn.PString(tableStat.Rows[k][0].ToString());
					ProcList[i].Descript= PIn.PString(tableStat.Rows[k][1].ToString());
					ProcList[i].AbbrDesc= PIn.PString(tableStat.Rows[k][2].ToString());
					ProcList[i].ProcCat = 255;
					i++;
				}
			}*/
			ProcedureCode[] retVal=new ProcedureCode[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
			//return ProcList;
		}

		///<summary>Gets a list of procedure codes directly from the database.  If categories.length==0, then we will get for all categories.  Categories are defnums.  FeeScheds are, for now, defnums.</summary>
		public static DataTable GetProcTable(string abbr,string desc,string code,int[] categories,int feeSched,
			int feeSchedComp1,int feeSchedComp2){
			string whereCat;
			if(categories.Length==0){
				whereCat="1";
			}
			else{
				whereCat="(";
				for(int i=0;i<categories.Length;i++){
					if(i>0){
						whereCat+=" OR ";
					}
					whereCat+="ProcCat="+POut.PInt(categories[i]);
				}
				whereCat+=")";
			}
/*			string command="SELECT ProcCat,Descript,AbbrDesc,procedurecode.ProcCode,"
				+"IFNULL(fee1.Amount,'-1') AS FeeAmt1, "
				+"IFNULL(fee2.Amount,'-1') AS FeeAmt2, "
				+"IFNULL(fee3.Amount,'-1') AS FeeAmt3 "
				+"FROM procedurecode "
				+"LEFT JOIN fee AS fee1 ON fee1.CodeNum=procedurecode.CodeNum "
				+"AND fee1.FeeSched="+POut.PInt(feeSched)
				+" LEFT JOIN fee AS fee2 ON fee2.CodeNum=procedurecode.CodeNum "
				+"AND fee2.FeeSched="+POut.PInt(feeSchedComp1)
				+" LEFT JOIN fee AS fee3 ON fee3.CodeNum=procedurecode.CodeNum "
				+"AND fee3.FeeSched="+POut.PInt(feeSchedComp2)
				+" WHERE "+whereCat
				+" AND Descript LIKE '%"+POut.PString(desc)+"%' "
				+"AND AbbrDesc LIKE '%"+POut.PString(abbr)+"%' "
				+"AND procedurecode.ProcCode LIKE '%"+POut.PString(code)+"%' "
				+"ORDER BY ProcCat,procedurecode.ProcCode";*/

			//Query changed to be compatible with both MySQL and Oracle.
			string command="SELECT ProcCat,Descript,AbbrDesc,procedurecode.ProcCode,"
				+"CASE WHEN (fee1.Amount IS NULL) THEN -1 ELSE fee1.Amount END AS FeeAmt1,"
				+"CASE WHEN (fee2.Amount IS NULL) THEN -1 ELSE fee2.Amount END AS FeeAmt2,"
				+"CASE WHEN (fee3.Amount IS NULL) THEN -1 ELSE fee3.Amount END AS FeeAmt3, "
				+"procedurecode.CodeNum "
				+"FROM procedurecode "
				+"LEFT JOIN fee fee1 ON fee1.CodeNum=procedurecode.CodeNum "
				+"AND fee1.FeeSched="+POut.PInt(feeSched)
				+" LEFT JOIN fee fee2 ON fee2.CodeNum=procedurecode.CodeNum "
				+"AND fee2.FeeSched="+POut.PInt(feeSchedComp1)
				+" LEFT JOIN fee fee3 ON fee3.CodeNum=procedurecode.CodeNum "
				+"AND fee3.FeeSched="+POut.PInt(feeSchedComp2)
				+" WHERE "+whereCat
				+" AND Descript LIKE '%"+POut.PString(desc)+"%' "
				+"AND AbbrDesc LIKE '%"+POut.PString(abbr)+"%' "
				+"AND procedurecode.ProcCode LIKE '%"+POut.PString(code)+"%' "
				+"ORDER BY ProcCat,procedurecode.ProcCode";
			//MsgBoxCopyPaste msg=new MsgBoxCopyPaste(command);
			//msg.ShowDialog();
			return General.GetTable(command);
		}

		///<summary>Returns the LaymanTerm for the supplied codeNum, or the description if none present.</summary>
		public static string GetLaymanTerm(int codeNum) {
			//if(myADA==null) {
			//	MessageBox.Show(Lan.g("ProcCodes","Error. Invalid procedure code."));
			//	return "";
			//}
			for(int i=0;i<List.Length;i++){
				if(List[i].CodeNum==codeNum){
					if(List[i].LaymanTerm !=""){
						return List[i].LaymanTerm;
					}
					return List[i].Descript;
				}
			}
			return "";
		}

		///<summary>Used to check whether codes starting with T exist and are in a visible category.  If so, it moves them to the Obsolete category.  If the T code has never been used, then it deletes it.</summary>
		public static void TcodesClear() {
			//first delete any unused T codes
			string command=@"SELECT CodeNum,ProcCode FROM procedurecode
				WHERE NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.CodeNum=procedurecode.CodeNum)
				AND ProcCode LIKE 'T%'";
			DataTable table=General.GetTable(command);
			int codenum;
			for(int i=0;i<table.Rows.Count;i++) {
				codenum=PIn.PInt(table.Rows[i]["CodeNum"].ToString());
				command="DELETE FROM fee WHERE CodeNum="+POut.PInt(codenum);
				General.NonQ(command);
				command="DELETE FROM procedurecode WHERE CodeNum="+POut.PInt(codenum);
				General.NonQ(command);
			}
			//then, move any other T codes to obsolete category
			command=@"SELECT DISTINCT ProcCat FROM procedurecode,definition 
				WHERE procedurecode.ProcCode LIKE 'T%'
				AND definition.IsHidden=0
				AND procedurecode.ProcCat=definition.DefNum";
			table=General.GetTable(command);
			int catNum=DefB.GetByExactName(DefCat.ProcCodeCats,"Obsolete");//check to make sure an Obsolete category exists.
			Def def;
			if(catNum!=0) {//if a category exists with that name
				def=DefB.GetDef(DefCat.ProcCodeCats,catNum);
				if(!def.IsHidden) {
					def.IsHidden=true;
					Defs.Update(def);
					Defs.Refresh();
				}
			}
			if(catNum==0) {
				def=new Def();
				def.Category=DefCat.ProcCodeCats;
				def.ItemName="Obsolete";
				def.ItemOrder=DefB.Long[(int)DefCat.ProcCodeCats].Length;
				def.IsHidden=true;
				Defs.Insert(def);
				Defs.Refresh();
				catNum=def.DefNum;
			}
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE procedurecode SET ProcCat="+POut.PInt(catNum)
					+" WHERE ProcCat="+table.Rows[i][0].ToString();
				General.NonQ(command);
			}
			//finally, set Never Used category to be hidden.  This isn't really part of clearing Tcodes, but is required
			//because many customers won't have that category hidden
			catNum=DefB.GetByExactName(DefCat.ProcCodeCats,"Never Used");
			if(catNum!=0) {//if a category exists with that name
				def=DefB.GetDef(DefCat.ProcCodeCats,catNum);
				if(!def.IsHidden) {
					def.IsHidden=true;
					Defs.Update(def);
					Defs.Refresh();
				}
			}
		}

		///<Summary>Resets the descriptions for all ADA codes to the official wording.  Required by the license.</Summary>
		public static void ResetADAdescriptions() {
			ResetADAdescriptions(CDT.Class1.GetADAcodes());
		}

		///<Summary>Resets the descriptions for all ADA codes to the official wording.  Required by the license.</Summary>
		public static void ResetADAdescriptions(List<ProcedureCode> codeList) {
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

/*
		///<summary>Used by FormUpdate when converting from T codes to D codes.  It's not converting the actual codes.  It's converting the autocodes and procbuttons from T to D.</summary>
		public static void TcodesAlter(){
			//string command="UPDATE autocodeitem SET Code = REPLACE(Code,'T','D') WHERE Code LIKE 'T%'";
			//General.NonQ(command);
			string command="UPDATE preference SET ValueString = REPLACE(ValueString,'T','D') "
				+"WHERE PrefName ='RecallProcedures' OR PrefName='RecallBW'";
			General.NonQ(command);
			//command="UPDATE procbuttonitem SET Code = REPLACE(Code,'T','D') WHERE Code LIKE 'T%'";
			//General.NonQ(command);
		}

		///<summary>Deletes unused codes.  Returns the number of rows affected.</summary>
		public static int DeleteUnusedCodes() {
			string command=@"SELECT CodeNum,ProcCode FROM procedurecode
				WHERE NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.CodeNum=procedurecode.CodeNum)";
			DataTable table=General.GetTable(command);
			int codenum;
			string proccode;
			int rowsaffected=0;
			for(int i=0;i<table.Rows.Count;i++) {
				codenum=PIn.PInt(table.Rows[i]["CodeNum"].ToString());
				proccode=PIn.PString(table.Rows[i]["ProcCode"].ToString());
				if(!Regex.IsMatch(proccode,"^D([0-9]{4})$")) {
					continue;//ignore anything but D####
				}
				//make sure it's not used in fees
				command="SELECT COUNT(*) FROM fee WHERE CodeNum="+POut.PInt(codenum);
				if(General.GetCount(command)!="0") {
					continue;
				}
				command="DELETE FROM procedurecode WHERE CodeNum="+POut.PInt(codenum);
				rowsaffected+=General.NonQ(command);
			}
			return rowsaffected;
		}

		///<summary>Checks other tables which use ProcCodes elsewhere in the database and deletes codes from the procedurecode table which are not referenced in any of the other tables. This is used in FormLicenseMissing.cs.</summary>
		public static void DeleteUnusedProcCodes(){
			//First collect the individual proc codes currently in use from the various different tables.
			const string CodePattern="^D([0-9]{4})$";
			bool[] ProcCodesUsed=new bool[10000];//All elements start out as false automatically (C# feature).
			string command="SELECT CodeStart,CodeEnd from appointmentrule";
			DataTable table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				Match mStart=(new Regex(procCodePattern,RegexOptions.IgnoreCase)).Match(
					PIn.PString(table.Rows[i]["CodeStart"].ToString()));
				Match mEnd=(new Regex(CodePattern,RegexOptions.IgnoreCase)).Match(
					PIn.PString(table.Rows[i]["CodeEnd"].ToString()));
				if(mStart.Success && mEnd.Success){
					int startNum=Convert.ToInt32(mStart.Result("$1"));
					int endNum=Convert.ToInt32(mEnd.Result("$1"));
					for(int j=startNum;j<=endNum;j++){
						CodesUsed[j]=true;
					}
				}
			}
			//References to Codes which should be directly kept (as opposed to ranges shown above).
			string[] simpleCodeReferenceTables=new string[] {
				"autocodeitem",
				"benefit",
				"fee",
				"procbuttonitem",
				"procedurelog",
				//"proctp",
				//"repeatcharge",
			};
			for(int i=0;i<simpleCodeReferenceTables.Length;i++){
			string command="SELECT DISTINCT Code FROM procedurelog";
			DataTable table=General.GetTable(command);
			for(int j=0;j<table.Rows.Count;j++){
				if(!Regex.IsMatch(PIn.PString(table.Rows[j][0].ToString()),"^D([0-9]{4})$")){
					continue;
				}
					//Match m=(new Regex(CodePattern,RegexOptions.IgnoreCase)).Match(
					//	PIn.PString(table.Rows[j]["Code"].ToString()));
					//if(m.Success){
				int codenum=Convert.ToInt32(m.Result("$1"));
				CodesUsed[adanum]=true;
					//}
				//}
			}
			//Now remove unused codes (those marked false in the CodesUsed array).
			command="";
			for(int i=0;i<CodesUsed.Length;i++){
				if(!CodesUsed[i]){
					string Code="D"+i.ToString().PadLeft(4,'0');
					if(command==""){//We only construct the command if there are codes to be deleted.
						command="DELETE FROM procedurecode WHERE Code='"+Code+"'";
					}else{
						command+=" OR Code='"+Code+"'";
					}
				}
			}
			General.NonQEx(command);
		}

		///<summary>Returns the list of all Codes which are in the form D####.</summary>
		public static string[] GetAllStandardCodes(){
			//Get all values currently in the Cocde column.
			string command="SELECT Code from procedurecode";
			DataTable table=General.GetTableEx(command);
			//Now weed-out values not in the actual code form (D####).
			ArrayList resultList=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				string Code=PIn.PString(table.Rows[i]["Code"].ToString());
				Match m=(new Regex("^D[0-9]{4}$",RegexOptions.IgnoreCase)).Match(Code);
				if(m.Success){
					resultList.Add(Code);
				}
			}
			//Finally, convert the list into an array.
			return (string[])resultList.ToArray(typeof(string));
		}*/

	}

	
	
	


}