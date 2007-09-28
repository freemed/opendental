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
				tempCode.BaseUnits     =PIn.PInt   (tableStat.Rows[i][22].ToString());
				tempCode.SubstitutionCode=PIn.PString(tableStat.Rows[i][23].ToString());
				//tempCode.SubstOnlyIf   =(SubstitutionCondition)PIn.PInt(tableStat.Rows[i][24].ToString());
				try {
					HList.Add(tempCode.ProcCode,tempCode.Copy());
				}
				catch {
				}
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
				+"PaintType,GraphicColor,LaymanTerm,IsCanadianLab,PreExisting,BaseUnits,SubstitutionCode,"
				+"SubstOnlyIf) VALUES("
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
				+"'"+POut.PBool  (code.PreExisting)+"', "
				+"'"+POut.PInt(code.BaseUnits)+"', "
				+"'"+POut.PString(code.SubstitutionCode)+"', "
				+"'"+POut.PInt   ((int)code.SubstOnlyIf)+"')";
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
				+ ",BaseUnits = '"     +POut.PInt(code.BaseUnits)+"'"
				+ ",SubstitutionCode = '"+POut.PString(code.SubstitutionCode)+"'"
				+ ",SubstOnlyIf = '"   +POut.PInt   ((int)code.SubstOnlyIf)+"'"
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

		///<summary>If a substitute exists for the given proc code, then it will give the CodeNum of that code.  Otherwise, it will return the codeNum for the given procCode.</summary>
		public static int GetSubstituteCodeNum(string procCode,string toothNum) {
			if(procCode==null || procCode=="") {
				return 0;
			}
			if(!HList.Contains(procCode)) {
				return 0;
			}
			ProcedureCode proc=(ProcedureCode)HList[procCode];
			if(proc.SubstitutionCode!="" && HList.Contains(proc.SubstitutionCode)){
				if(proc.SubstOnlyIf==SubstitutionCondition.Always){
					return ((ProcedureCode)HList[proc.SubstitutionCode]).CodeNum;
				}
				if(proc.SubstOnlyIf==SubstitutionCondition.Molar && Tooth.IsMolar(toothNum)){
					return ((ProcedureCode)HList[proc.SubstitutionCode]).CodeNum;
				}
				if(proc.SubstOnlyIf==SubstitutionCondition.SecondMolar && Tooth.IsSecondMolar(toothNum)) {
					return ((ProcedureCode)HList[proc.SubstitutionCode]).CodeNum;
				}
			}
			return proc.CodeNum;
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

		///<summary>Grouped by Category.  Used only in FormRpProcCodes.</summary>
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
			//Query changed to be compatible with both MySQL and Oracle (not tested).
			string command="SELECT ProcCat,Descript,AbbrDesc,procedurecode.ProcCode,"
				+"CASE WHEN (fee1.Amount IS NULL) THEN -1 ELSE fee1.Amount END FeeAmt1,"
				+"CASE WHEN (fee2.Amount IS NULL) THEN -1 ELSE fee2.Amount END FeeAmt2,"
				+"CASE WHEN (fee3.Amount IS NULL) THEN -1 ELSE fee3.Amount END FeeAmt3, "
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

		///<summary>Resets the descriptions for all ADA codes to the official wording.  Required by the license.</summary>
		public static void ResetADAdescriptions() {
			ResetADAdescriptions(CDT.Class1.GetADAcodes());
		}

		///<summary>Resets the descriptions for all ADA codes to the official wording.  Required by the license.</summary>
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

		public static void ResetApptProcsQuickAdd() {
			string command= "DELETE FROM definition WHERE Category=3";
			General.NonQ(command);
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





	}

	
	
	


}