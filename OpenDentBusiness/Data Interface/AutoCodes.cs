using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AutoCodes{

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from autocode";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoCode";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			AutoCodeC.HList=new Hashtable();
			AutoCodeC.List=Crud.AutoCodeCrud.TableToList(table).ToArray();
			ArrayList ALshort=new ArrayList();//int of indexes of short list
			for(int i = 0;i<AutoCodeC.List.Length;i++){
				AutoCodeC.HList.Add(AutoCodeC.List[i].AutoCodeNum,AutoCodeC.List[i]);
				if(!AutoCodeC.List[i].IsHidden){
					ALshort.Add(i);
				}
			}
			AutoCodeC.ListShort=new AutoCode[ALshort.Count];
			for(int i=0;i<ALshort.Count;i++){
				AutoCodeC.ListShort[i]=AutoCodeC.List[(int)ALshort[i]];
			}
		}

		///<summary></summary>
		public static long Insert(AutoCode Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.AutoCodeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.AutoCodeNum;
			}
			return Crud.AutoCodeCrud.Insert(Cur);
		}

		///<summary></summary>
		public static void Update(AutoCode Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.AutoCodeCrud.Update(Cur);
		}

		///<summary>This could be improved since it does not delete any autocode items.</summary>
		public static void Delete(AutoCode Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "DELETE from autocode WHERE autocodenum = '"+POut.Long(Cur.AutoCodeNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Used in ProcButtons.SetToDefault.  Returns 0 if the given autocode does not exist.</summary>
		public static long GetNumFromDescript(string descript) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<AutoCodeC.ListShort.Length;i++) {
				if(AutoCodeC.ListShort[i].Description==descript) {
					return AutoCodeC.ListShort[i].AutoCodeNum;
				}
			}
			return 0;
		}

		//------

		///<summary>Deletes all current autocodes and then adds the default autocodes.  Procedure codes must have already been entered or they cannot be added as an autocode.</summary>
		public static void SetToDefault() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="DELETE FROM autocode";
			Db.NonQ(command);
			command="DELETE FROM autocodecond";
			Db.NonQ(command);
			command="DELETE FROM autocodeitem";
			Db.NonQ(command);
			long autoCodeNum;
			long autoCodeItemNum;
			//Amalgam-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Amalgam',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//1Surf
			if(ProcedureCodes.IsValidCode("D2140")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2140")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
				+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
			}
			//2Surf
			if(ProcedureCodes.IsValidCode("D2150")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2150")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
			}
			//3Surf
			if(ProcedureCodes.IsValidCode("D2160")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2160")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
			}
			//4Surf
			if(ProcedureCodes.IsValidCode("D2161")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2161")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
			}
			//5Surf
			if(ProcedureCodes.IsValidCode("D2161")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2161")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
			}
			//Composite-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Composite',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//1SurfAnt
			if(ProcedureCodes.IsValidCode("D2330")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2330")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//2SurfAnt
			if(ProcedureCodes.IsValidCode("D2331")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2331")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//3SurfAnt
			if(ProcedureCodes.IsValidCode("D2332")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2332")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//4SurfAnt
			if(ProcedureCodes.IsValidCode("D2335")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2335")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//5SurfAnt
			if(ProcedureCodes.IsValidCode("D2335")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2335")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//Posterior Composite----------------------------------------------------------------------------------------------
			//1SurfPost
			if(ProcedureCodes.IsValidCode("D2391")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2391")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//2SurfPost
			if(ProcedureCodes.IsValidCode("D2392")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2392")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//3SurfPost
			if(ProcedureCodes.IsValidCode("D2393")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2393")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//4SurfPost
			if(ProcedureCodes.IsValidCode("D2394")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2394")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//5SurfPost
			if(ProcedureCodes.IsValidCode("D2394")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2394")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//Root Canal-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Root Canal',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Ant
			if(ProcedureCodes.IsValidCode("D3310")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3310")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//Premolar
			if(ProcedureCodes.IsValidCode("D3320")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3320")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//Molar
			if(ProcedureCodes.IsValidCode("D3330")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3330")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//Bridge-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Bridge',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Pontic
			if(ProcedureCodes.IsValidCode("D6242")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D6242")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Pontic)+")";
				Db.NonQ(command);
			}
			//Retainer
			if(ProcedureCodes.IsValidCode("D6752")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D6752")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Retainer)+")";
				Db.NonQ(command);
			}
			//Denture-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Denture',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Max
			if(ProcedureCodes.IsValidCode("D5110")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D5110")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Maxillary)+")";
				Db.NonQ(command);
			}
			//Mand
			if(ProcedureCodes.IsValidCode("D5120")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D5120")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Mandibular)+")";
				Db.NonQ(command);
			}
			//BU/P&C-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('BU/P&C',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//BU
			if(ProcedureCodes.IsValidCode("D2950")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2950")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//P&C
			if(ProcedureCodes.IsValidCode("D2954")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D2954")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//Root Canal Retreat--------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('RCT Retreat',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Ant
			if(ProcedureCodes.IsValidCode("D3346")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3346")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//Premolar
			if(ProcedureCodes.IsValidCode("D3347")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3347")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//Molar
			if(ProcedureCodes.IsValidCode("D3348")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("D3348")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
		}


	}

	


}









