using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class AutoCodes{
		///<summary></summary>
		public static AutoCode[] List;
		///<summary></summary>
		public static AutoCode[] ListShort;
		///<summary>key=AutoCodeNum, value=AutoCode</summary>
		public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from autocode";
			DataTable table=General.GetTable(command);
			HList=new Hashtable();
			List=new AutoCode[table.Rows.Count];
			ArrayList ALshort=new ArrayList();//int of indexes of short list
			for(int i = 0;i<List.Length;i++){
				List[i]=new AutoCode();
				List[i].AutoCodeNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  = PIn.PString(table.Rows[i][1].ToString());
				List[i].IsHidden     = PIn.PBool  (table.Rows[i][2].ToString());	
				List[i].LessIntrusive= PIn.PBool  (table.Rows[i][3].ToString());	
				HList.Add(List[i].AutoCodeNum,List[i]);
				if(!List[i].IsHidden){
					ALshort.Add(i);
				}
			}
			ListShort=new AutoCode[ALshort.Count];
			for(int i=0;i<ALshort.Count;i++){
				ListShort[i]=List[(int)ALshort[i]];
			}
		}

		///<summary></summary>
		public static void Insert(AutoCode Cur){
			string command= "INSERT INTO autocode (Description,IsHidden,LessIntrusive) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.LessIntrusive)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCode Cur){
			string command= "UPDATE autocode SET "
				+"Description='"      +POut.PString(Cur.Description)+"'"
				+",IsHidden = '"      +POut.PBool  (Cur.IsHidden)+"'"
				+",LessIntrusive = '" +POut.PBool  (Cur.LessIntrusive)+"'"
				+" WHERE autocodenum = '"+POut.PInt (Cur.AutoCodeNum)+"'";
			General.NonQ(command);
		}

		///<summary>This could be improved since it does not delete any autocode items.</summary>
		public static void Delete(AutoCode Cur){
			string command= "DELETE from autocode WHERE autocodenum = '"+POut.PInt(Cur.AutoCodeNum)+"'";
			General.NonQ(command);
		}

		///<summary>Used in ProcButtons.SetToDefault.  Returns 0 if the given autocode does not exist.</summary>
		public static int GetNumFromDescript(string descript) {
			for(int i=0;i<ListShort.Length;i++) {
				if(ListShort[i].Description==descript) {
					return ListShort[i].AutoCodeNum;
				}
			}
			return 0;
		}

		///<summary>Deletes all current autocodes and then adds the default autocodes.  Procedure codes must have already been entered or they cannot be added as an autocode.</summary>
		public static void SetToDefault(){
			string command= @"
				DELETE FROM autocode;
				DELETE FROM autocodecond;
				DELETE FROM autocodeitem";
			General.NonQ(command);
			int autoCodeNum;
			int autoCodeItemNum;
			//Amalgam-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Amalgam',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//1Surf
			if(ProcedureCodes.IsValidCode("D2140")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2140")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
				+POut.PInt((int)AutoCondition.One_Surf)+")";
				General.NonQ(command);
			}
			//2Surf
			if(ProcedureCodes.IsValidCode("D2150")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2150")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Two_Surf)+")";
				General.NonQ(command);
			}
			//3Surf
			if(ProcedureCodes.IsValidCode("D2160")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2160")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Three_Surf)+")";
				General.NonQ(command);
			}
			//4Surf
			if(ProcedureCodes.IsValidCode("D2161")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2161")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Four_Surf)+")";
				General.NonQ(command);
			}
			//5Surf
			if(ProcedureCodes.IsValidCode("D2161")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2161")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Five_Surf)+")";
				General.NonQ(command);
			}
			//Composite-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Composite',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//1SurfAnt
			if(ProcedureCodes.IsValidCode("D2330")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2330")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.One_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//2SurfAnt
			if(ProcedureCodes.IsValidCode("D2331")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2331")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Two_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//3SurfAnt
			if(ProcedureCodes.IsValidCode("D2332")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2332")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Three_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//4SurfAnt
			if(ProcedureCodes.IsValidCode("D2335")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2335")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Four_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//5SurfAnt
			if(ProcedureCodes.IsValidCode("D2335")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2335")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Five_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//Posterior Composite----------------------------------------------------------------------------------------------
			//1SurfPost
			if(ProcedureCodes.IsValidCode("D2391")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2391")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.One_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Posterior)+")";
				General.NonQ(command);
			}
			//2SurfPost
			if(ProcedureCodes.IsValidCode("D2392")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2392")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Two_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Posterior)+")";
				General.NonQ(command);
			}
			//3SurfPost
			if(ProcedureCodes.IsValidCode("D2393")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2393")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Three_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Posterior)+")";
				General.NonQ(command);
			}
			//4SurfPost
			if(ProcedureCodes.IsValidCode("D2394")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2394")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Four_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Posterior)+")";
				General.NonQ(command);
			}
			//5SurfPost
			if(ProcedureCodes.IsValidCode("D2394")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2394")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Five_Surf)+")";
				General.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Posterior)+")";
				General.NonQ(command);
			}
			//Root Canal-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Root Canal',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//Ant
			if(ProcedureCodes.IsValidCode("D3310")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3310")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//Premolar
			if(ProcedureCodes.IsValidCode("D3320")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3320")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Premolar)+")";
				General.NonQ(command);
			}
			//Molar
			if(ProcedureCodes.IsValidCode("D3330")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3330")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Molar)+")";
				General.NonQ(command);
			}
			//Bridge-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Bridge',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//Pontic
			if(ProcedureCodes.IsValidCode("D6242")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D6242")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Pontic)+")";
				General.NonQ(command);
			}
			//Retainer
			if(ProcedureCodes.IsValidCode("D6752")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D6752")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Retainer)+")";
				General.NonQ(command);
			}
			//Denture-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Denture',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//Max
			if(ProcedureCodes.IsValidCode("D5110")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D5110")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Maxillary)+")";
				General.NonQ(command);
			}
			//Mand
			if(ProcedureCodes.IsValidCode("D5120")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D5120")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Mandibular)+")";
				General.NonQ(command);
			}
			//BU/P&C-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('BU/P&C',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//BU
			if(ProcedureCodes.IsValidCode("D2950")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2950")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Posterior)+")";
				General.NonQ(command);
			}
			//P&C
			if(ProcedureCodes.IsValidCode("D2954")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2954")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//Root Canal Retreat--------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('RCT Retreat',0,0)";
			autoCodeNum=General.NonQ(command,true);
			//Ant
			if(ProcedureCodes.IsValidCode("D3346")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3346")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Anterior)+")";
				General.NonQ(command);
			}
			//Premolar
			if(ProcedureCodes.IsValidCode("D3347")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3347")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Premolar)+")";
				General.NonQ(command);
			}
			//Molar
			if(ProcedureCodes.IsValidCode("D3348")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.PInt(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3348")+")";
				autoCodeItemNum=General.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.PInt(autoCodeItemNum)+","
					+POut.PInt((int)AutoCondition.Molar)+")";
				General.NonQ(command);
			}
		}


	}

	


}









