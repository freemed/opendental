using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RecallTypes{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM recalltype ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="RecallType";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<RecallType> list=new List<RecallType>();
			RecallType rtype;
			for(int i=0;i<table.Rows.Count;i++){
				rtype=new RecallType();
				rtype.IsNew=false;
				rtype.RecallTypeNum  = PIn.Long   (table.Rows[i][0].ToString());
				rtype.Description    = PIn.String(table.Rows[i][1].ToString());
				rtype.DefaultInterval= new Interval(PIn.Int(table.Rows[i][2].ToString()));
				rtype.TimePattern    = PIn.String(table.Rows[i][3].ToString());
				rtype.Procedures     = PIn.String(table.Rows[i][4].ToString());
				list.Add(rtype);
			}
			//reorder rows for better usability
			RecallTypeC.Listt=new List<RecallType>();
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)){
					RecallTypeC.Listt.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy)){
					RecallTypeC.Listt.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)){
					RecallTypeC.Listt.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){//now add the rest
				if(!RecallTypeC.Listt.Contains(list[i])){
					RecallTypeC.Listt.Add(list[i]);
				}
			}
		}

		/*
		///<Summary>Gets one RecallType from the database.</Summary>
		public static RecallType CreateObject(int recallTypeNum){
			return DataObjectFactory<RecallType>.CreateObject(recallTypeNum);
		}

		public static List<RecallType> GetRecallTypes(int[] RecallTypeNums){
			Collection<RecallType> collectState=DataObjectFactory<RecallType>.CreateObjects(RecallTypeNums);
			return new List<RecallType>(collectState);		
		}*/

		///<summary></summary>
		public static long WriteObject(RecallType recallType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				recallType.RecallTypeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),recallType);
				return recallType.RecallTypeNum;
			}
			DataObjectFactory<RecallType>.WriteObject(recallType);
			return recallType.RecallTypeNum;
		}

		/*//<summary></summary>
		public static void DeleteObject(int RecallTypeNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE RecallTypeNum="+POut.PInt(RecallTypeNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("RecallTypes","RecallType is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<RecallType>.DeleteObject(RecallTypeNum);
		}*/

		//public static void DeleteObject(int RecallTypeNum){
		//	DataObjectFactory<RecallType>.DeleteObject(RecallTypeNum);
		//}

		public static string GetDescription(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==0){
				return "";
			}
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				if(RecallTypeC.Listt[i].RecallTypeNum==recallTypeNum){
					return RecallTypeC.Listt[i].Description;
				}
			}
			return "";
		}

		public static Interval GetInterval(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==0){
				return new Interval(0,0,0,0);
			}
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				if(RecallTypeC.Listt[i].RecallTypeNum==recallTypeNum){
					return RecallTypeC.Listt[i].DefaultInterval;
				}
			}
			return new Interval(0,0,0,0);
		}

		///<summary>Returns a collection of proccodes (D####).  Count could be zero.</summary>
		public static List<string> GetProcs(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			List<string> retVal=new List<string>();
			if(recallTypeNum==0){
				return retVal;
			}
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				if(RecallTypeC.Listt[i].RecallTypeNum==recallTypeNum){
					if(RecallTypeC.Listt[i].Procedures==""){
						return retVal;
					}
					string[] strArray=RecallTypeC.Listt[i].Procedures.Split(',');
					retVal.AddRange(strArray);
					return retVal;
				}
			}
			return retVal;
		}

		///<summary>Also makes sure both types are defined as special types.</summary>
		public static bool PerioAndProphyBothHaveTriggers(){
			//No need to check RemotingRole; no call to db.
			if(RecallTypes.PerioType==0 || RecallTypes.ProphyType==0){
				return false;
			}
			if(RecallTriggers.GetForType(RecallTypes.PerioType).Count==0){
				return false;
			}
			if(RecallTriggers.GetForType(RecallTypes.ProphyType).Count==0){
				return false;
			}
			return true;
		}

		public static string GetTimePattern(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==0){
				return "";
			}
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				if(RecallTypeC.Listt[i].RecallTypeNum==recallTypeNum){
					return RecallTypeC.Listt[i].TimePattern;
				}
			}
			return "";
		}

		public static string GetSpecialTypeStr(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)){
				return Lans.g("FormRecallTypeEdit","Prophy");
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy)){
				return Lans.g("FormRecallTypeEdit","ChildProphy");
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)){
				return Lans.g("FormRecallTypeEdit","Perio");
			}
			return "";
		}

		public static bool IsSpecialRecallType(long recallTypeNum) {
			//No need to check RemotingRole; no call to db.
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialProphy)) {
				return true;
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy)) {
				return true;
			}
			if(recallTypeNum==PrefC.GetLong(PrefName.RecallTypeSpecialPerio)) {
				return true;
			}
			return false;
		}

		///<summary>Gets a list of all active recall types.  Those without triggers are excluded.  Perio and Prophy are both included.  One of them should later be removed from the collection.</summary>
		public static List<RecallType> GetActive(){
			//No need to check RemotingRole; no call to db.
			List<RecallType> retVal=new List<RecallType>();
			List<RecallTrigger> triggers;
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				triggers=RecallTriggers.GetForType(RecallTypeC.Listt[i].RecallTypeNum);
				if(triggers.Count>0){
					retVal.Add(RecallTypeC.Listt[i].Copy());
				}
			}
			return retVal;
		}

		/*
		///<summary>Gets a list of all inactive recall types.  Only those without triggers are included.</summary>
		public static List<RecallType> GetInactive(){
			//No need to check RemotingRole; no call to db.
			List<RecallType> retVal=new List<RecallType>();
			List<RecallTrigger> triggers;
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				triggers=RecallTriggers.GetForType(RecallTypeC.Listt[i].RecallTypeNum);
				if(triggers.Count==0){
					retVal.Add(RecallTypeC.Listt[i].Clone());
				}
			}
			return retVal;
		}*/

		///<summary>Gets the pref table RecallTypeSpecialProphy RecallTypeNum.</summary>
		public static long ProphyType{
			//No need to check RemotingRole; no call to db.
			get{
				return PrefC.GetLong(PrefName.RecallTypeSpecialProphy);
			}
		}

		///<summary>Gets the pref table RecallTypeSpecialPerio RecallTypeNum.</summary>
		public static long PerioType{
			//No need to check RemotingRole; no call to db.
			get{
				return PrefC.GetLong(PrefName.RecallTypeSpecialPerio);
			}
		}

		///<summary>Gets the pref table RecallTypeSpecialChildProphy RecallTypeNum.</summary>
		public static long ChildProphyType{
			//No need to check RemotingRole; no call to db.
			get{
				return PrefC.GetLong(PrefName.RecallTypeSpecialChildProphy);
			}
		}

		

	}
}