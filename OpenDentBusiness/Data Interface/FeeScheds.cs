using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class FeeScheds{
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM feesched ORDER BY ItemOrder";
			DataTable table=General.GetTable(c);
			table.TableName="FeeSched";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			FeeSchedC.Listt=new List<FeeSched>();
			FeeSched sched;
			for(int i=0;i<table.Rows.Count;i++){
				sched=new FeeSched();
				sched.IsNew=false;
				sched.FeeSchedNum = PIn.PInt   (table.Rows[i][0].ToString());
				sched.Description = PIn.PString(table.Rows[i][1].ToString());
				sched.FeeSchedType= (FeeScheduleType)PIn.PInt(table.Rows[i][2].ToString());
				sched.ItemOrder   = PIn.PInt   (table.Rows[i][3].ToString());
				sched.IsHidden    = PIn.PBool  (table.Rows[i][4].ToString());
				FeeSchedC.Listt.Add(sched);
			}
		}

		/*
		///<Summary>Gets one FeeSched from the database.</Summary>
		public static FeeSched CreateObject(int feeSchedNum){
			return DataObjectFactory<FeeSched>.CreateObject(feeSchedNum);
		}

		public static List<FeeSched> GetFeeScheds(int[] FeeSchedNums){
			Collection<FeeSched> collectState=DataObjectFactory<FeeSched>.CreateObjects(FeeSchedNums);
			return new List<FeeSched>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(FeeSched FeeSched){
			DataObjectFactory<FeeSched>.WriteObject(FeeSched);
		}

		///<summary></summary>
		public static void DeleteObject(int FeeSchedNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE FeeSchedNum="+POut.PInt(FeeSchedNum);
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
				throw new ApplicationException(Lan.g("FeeScheds","FeeSched is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<FeeSched>.DeleteObject(FeeSchedNum);
		}

		//public static void DeleteObject(int FeeSchedNum){
		//	DataObjectFactory<FeeSched>.DeleteObject(FeeSchedNum);
		//}*/

		public static string GetDescription(int feeSchedNum){
			if(feeSchedNum==0){
				return "";
			}
			for(int i=0;i<FeeSchedC.Listt.Count;i++){
				if(FeeSchedC.Listt[i].FeeSchedNum==feeSchedNum){
					return FeeSchedC.Listt[i].Description;
				}
			}
			return "";
		}

	}
}