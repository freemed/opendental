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
			RecallTypeC.Listt=new List<RecallType>();
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
				RecallTypeC.Listt.Add(rtype);
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

		/*public static string GetDescription(int RecallTypeNum){
			if(RecallTypeNum==0){
				return "";
			}
			for(int i=0;i<RecallTypeC.List.Length;i++){
				if(RecallTypeC.List[i].RecallTypeNum==RecallTypeNum){
					return RecallTypeC.List[i].Description;
				}
			}
			return "";
		}*/

	}
}