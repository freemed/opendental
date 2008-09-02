using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RecallTypes{
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM recalltype ORDER BY Description";
			DataTable table=General.GetTable(c);
			table.TableName="RecallType";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			List<RecallType> list=new List<RecallType>();
			RecallType rtype;
			for(int i=0;i<table.Rows.Count;i++){
				rtype=new RecallType();
				rtype.IsNew=false;
				rtype.RecallTypeNum  = PIn.PInt   (table.Rows[i][0].ToString());
				rtype.Description    = PIn.PString(table.Rows[i][1].ToString());
				rtype.DefaultInterval= new Interval(PIn.PInt(table.Rows[i][2].ToString()));
				rtype.TimePattern    = PIn.PString(table.Rows[i][3].ToString());
				rtype.Procedures     = PIn.PString(table.Rows[i][4].ToString());
				rtype.TriggerProcs   = PIn.PString(table.Rows[i][5].ToString());
				list.Add(rtype);
			}
			//reorder rows for better usability
			RecallTypeC.Listt=new List<RecallType>();
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetInt("RecallTypeSpecialProphy")){
					RecallTypeC.Listt.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetInt("RecallTypeSpecialChildProphy")){
					RecallTypeC.Listt.Add(list[i]);
					break;
				}
			}
			for(int i=0;i<list.Count;i++){
				if(list[i].RecallTypeNum==PrefC.GetInt("RecallTypeSpecialPerio")){
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
		public static void WriteObject(RecallType recallType){
			DataObjectFactory<RecallType>.WriteObject(recallType);
		}

		/*//<summary></summary>
		public static void DeleteObject(int RecallTypeNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE RecallTypeNum="+POut.PInt(RecallTypeNum);
			DataTable table=General.GetTable(command);
			//int count=PIn.PInt(General.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("RecallTypes","RecallType is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<RecallType>.DeleteObject(RecallTypeNum);
		}*/

		//public static void DeleteObject(int RecallTypeNum){
		//	DataObjectFactory<RecallType>.DeleteObject(RecallTypeNum);
		//}

		public static string GetDescription(int recallTypeNum){
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

		public static Interval GetInterval(int recallTypeNum){
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

		public static string GetSpecialTypeStr(int recallTypeNum){
			if(recallTypeNum==PrefC.GetInt("RecallTypeSpecialProphy")){
				return Lan.g("FormRecallTypeEdit","Prophy");
			}
			if(recallTypeNum==PrefC.GetInt("RecallTypeSpecialChildProphy")){
				return Lan.g("FormRecallTypeEdit","ChildProphy");
			}
			if(recallTypeNum==PrefC.GetInt("RecallTypeSpecialPerio")){
				return Lan.g("FormRecallTypeEdit","Perio");
			}
			return "";
		}

		/*public static void SetSpecial(int recallTypeNum,int selectedIndex){
			int rowsAffected=0;
			string command;
			if(selectedIndex==0){//none
				command="UPDATE preference SET ValueString='"+POut.PInt(recallTypeNum)+"' WHERE PrefName=''";
			}
		}*/

	}
}