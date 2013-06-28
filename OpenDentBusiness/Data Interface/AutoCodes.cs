using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
		public static void ClearCache() {
			AutoCodeC.HList=null;
			AutoCodeC.List=null;
			AutoCodeC.ListShort=null;
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

		///<summary>Surround with try/catch.  Currently only called from FormAutoCode and FormAutoCodeEdit.</summary>
		public static void Delete(AutoCode autoCodeCur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoCodeCur);
				return;
			}
			//look for dependencies in ProcButton table.
			string strInUse="";
			for(int i=0;i<ProcButtons.List.Length;i++) {
				for(int j=0;j<ProcButtonItems.List.Length;j++) {
					if(ProcButtonItems.List[j].ProcButtonNum==ProcButtons.List[i].ProcButtonNum 
						&& ProcButtonItems.List[j].AutoCodeNum==autoCodeCur.AutoCodeNum) 
					{
						if(strInUse!="") {
							strInUse+="; ";
						}
						//Add the procedure button description to the list for display.
						strInUse+=ProcButtons.List[i].Description;
						break;//Button already added to the description, check the other buttons in the list.
					}
				}
			}
			if(strInUse!="") {
				throw new ApplicationException(Lans.g("AutoCodes","Not allowed to delete autocode because it is in use.  Procedure buttons using this autocode include ")+strInUse);
			}
			List<AutoCodeItem> listAutoCodeItems=AutoCodeItems.GetListForCode(autoCodeCur.AutoCodeNum);
			for(int i=0;i<listAutoCodeItems.Count;i++) {
				AutoCodeItem AutoCodeItemCur=listAutoCodeItems[i];
        AutoCodeConds.DeleteForItemNum(AutoCodeItemCur.AutoCodeItemNum);
        AutoCodeItems.Delete(AutoCodeItemCur);
      }
			Crud.AutoCodeCrud.Delete(autoCodeCur.AutoCodeNum);
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
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				SetToDefaultCanada();
				return;
			}
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

		///<summary>Deletes all current autocodes and then adds the default autocodes.  Procedure codes must have already been entered or they cannot be added as an autocode.</summary>
		public static void SetToDefaultCanada() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command;
			long autoCodeNum;
			long autoCodeItemNum;
			//Amalgam-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Amalgam',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//1SurfPrimary
			if(ProcedureCodes.IsValidCode("21111")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21111")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
			}
			//2SurfPrimary
			if(ProcedureCodes.IsValidCode("21112")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21112")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
			}
			//3SurfPrimary
			if(ProcedureCodes.IsValidCode("21113")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21113")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
			}
			//4SurfPrimary
			if(ProcedureCodes.IsValidCode("21114")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21114")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
			}
			//5SurfPrimary
			if(ProcedureCodes.IsValidCode("21115")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21115")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
			}
			//1SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("21211")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21211")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//2SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("21212")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21212")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//3SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("21213")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21213")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//4SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("21214")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21214")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//5SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("21215")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21215")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//1SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("21221")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21221")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//2SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("21222")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21222")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//3SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("21223")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21223")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//4SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("21224")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21224")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//5SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("21225")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("21225")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//Composite-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Composite',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//1SurfPermanentAnterior
			if(ProcedureCodes.IsValidCode("23111")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23111")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//2SurfPermanentAnterior
			if(ProcedureCodes.IsValidCode("23112")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23112")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//3SurfPermanentAnterior
			if(ProcedureCodes.IsValidCode("23113")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23113")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//4SurfPermanentAnterior
			if(ProcedureCodes.IsValidCode("23114")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23114")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//5SurfPermanentAnterior
			if(ProcedureCodes.IsValidCode("23115")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23115")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//1SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("23311")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23311")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//2SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("23312")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23312")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//3SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("23313")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23313")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//4SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("23314")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23314")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//5SurfPermanentPremolar
			if(ProcedureCodes.IsValidCode("23315")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23315")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//1SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("23321")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23321")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//2SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("23322")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23322")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//3SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("23323")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23323")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//4SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("23324")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23324")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//5SurfPermanentMolar
			if(ProcedureCodes.IsValidCode("23325")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23325")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Permanent)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//1SurfPrimaryAnterior
			if(ProcedureCodes.IsValidCode("23411")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23411")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//2SurfPrimaryAnterior
			if(ProcedureCodes.IsValidCode("23412")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23412")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//3SurfPrimaryAnterior
			if(ProcedureCodes.IsValidCode("23413")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23413")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//4SurfPrimaryAnterior
			if(ProcedureCodes.IsValidCode("23414")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23414")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//5SurfPrimaryAnterior
			if(ProcedureCodes.IsValidCode("23415")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23415")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//1SurfPrimaryPosterior
			if(ProcedureCodes.IsValidCode("23511")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23511")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.One_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//2SurfPrimaryPosterior
			if(ProcedureCodes.IsValidCode("23512")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23512")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Two_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//3SurfPrimaryPosterior
			if(ProcedureCodes.IsValidCode("23513")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23513")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Three_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//4SurfPrimaryPosterior
			if(ProcedureCodes.IsValidCode("23514")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23514")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Four_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//5SurfPrimaryPosterior
			if(ProcedureCodes.IsValidCode("23515")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("23515")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Five_Surf)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Primary)+")";
				Db.NonQ(command);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Posterior)+")";
				Db.NonQ(command);
			}
			//Root Canal-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Root Canal',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Ant
			if(ProcedureCodes.IsValidCode("33111")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("33111")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Anterior)+")";
				Db.NonQ(command);
			}
			//Premolar
			if(ProcedureCodes.IsValidCode("33121")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("33121")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Premolar)+")";
				Db.NonQ(command);
			}
			//Molar
			if(ProcedureCodes.IsValidCode("33131")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("33131")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Molar)+")";
				Db.NonQ(command);
			}
			//Denture-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Denture',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Max
			if(ProcedureCodes.IsValidCode("51101")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("51101")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Maxillary)+")";
				Db.NonQ(command);
			}
			//Mand
			if(ProcedureCodes.IsValidCode("51302")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("51302")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Mandibular)+")";
				Db.NonQ(command);
			}
			//Bridge-------------------------------------------------------------------------------------------------------
			command="INSERT INTO autocode (Description,IsHidden,LessIntrusive) VALUES ('Bridge',0,0)";
			autoCodeNum=Db.NonQ(command,true);
			//Pontic
			if(ProcedureCodes.IsValidCode("62501")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("62501")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Pontic)+")";
				Db.NonQ(command);
			}
			//Retainer
			if(ProcedureCodes.IsValidCode("67211")) {
				command="INSERT INTO autocodeitem (AutoCodeNum,CodeNum) VALUES ("+POut.Long(autoCodeNum)+","
					+ProcedureCodes.GetCodeNum("67211")+")";
				autoCodeItemNum=Db.NonQ(command,true);
				command="INSERT INTO autocodecond (AutoCodeItemNum,Cond) VALUES ("+POut.Long(autoCodeItemNum)+","
					+POut.Long((int)AutoCondition.Retainer)+")";
				Db.NonQ(command);
			}
		}


	}

	


}









