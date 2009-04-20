using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class FeeScheds{
		///<summary></summary>
		public static DataTable RefreshCache() {
			string c="SELECT * FROM feesched ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="FeeSched";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			FeeSchedC.ListLong=new List<FeeSched>();
			FeeSchedC.ListShort=new List<FeeSched>();
			FeeSched sched;
			for(int i=0;i<table.Rows.Count;i++){
				sched=new FeeSched();
				sched.IsNew=false;
				sched.FeeSchedNum = PIn.PInt   (table.Rows[i][0].ToString());
				sched.Description = PIn.PString(table.Rows[i][1].ToString());
				sched.FeeSchedType= (FeeScheduleType)PIn.PInt(table.Rows[i][2].ToString());
				sched.ItemOrder   = PIn.PInt   (table.Rows[i][3].ToString());
				sched.IsHidden    = PIn.PBool  (table.Rows[i][4].ToString());
				FeeSchedC.ListLong.Add(sched);
				if(!sched.IsHidden){
					FeeSchedC.ListShort.Add(sched);
				}
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
		}*/

		///<summary></summary>
		public static void WriteObject(FeeSched feeSched){
			DataObjectFactory<FeeSched>.WriteObject(feeSched);
		}

		/*//<summary></summary>
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
			for(int i=0;i<FeeSchedC.ListLong.Count;i++){
				if(FeeSchedC.ListLong[i].FeeSchedNum==feeSchedNum){
					return FeeSchedC.ListLong[i].Description;
				}
			}
			return "";
		}

		public static bool GetIsHidden(int feeSchedNum){
			for(int i=0;i<FeeSchedC.ListLong.Count;i++){
				if(FeeSchedC.ListLong[i].FeeSchedNum==feeSchedNum){
					return FeeSchedC.ListLong[i].IsHidden;
				}
			}
			return true;
		}

		public static FeeSched GetByExactName(string description){
			for(int i=0;i<FeeSchedC.ListLong.Count;i++){
				if(FeeSchedC.ListLong[i].Description==description){
					return FeeSchedC.ListLong[i].Copy();
				}
			}
			return null;
		}

		public static FeeSched GetByExactName(string description,FeeScheduleType feeSchedType){
			for(int i=0;i<FeeSchedC.ListLong.Count;i++){
				if(FeeSchedC.ListLong[i].FeeSchedType!=feeSchedType){
					continue;
				}
				if(FeeSchedC.ListLong[i].Description==description){
					return FeeSchedC.ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Only used in FormInsPlan and FormFeeScheds.</summary>
		public static List<FeeSched> GetListForType(FeeScheduleType feeSchedType,bool includeHidden) {
			List<FeeSched> retVal=new List<FeeSched>();
			for(int i=0;i<FeeSchedC.ListLong.Count;i++) {
				if(!includeHidden && FeeSchedC.ListLong[i].IsHidden){
					continue;
				}
				if(FeeSchedC.ListLong[i].FeeSchedType==feeSchedType){
					retVal.Add(FeeSchedC.ListLong[i].Copy());
				}
			}
			return retVal;
		}

	}
}