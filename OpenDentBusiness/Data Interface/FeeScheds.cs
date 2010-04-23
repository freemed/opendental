using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class FeeScheds{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM feesched ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="FeeSched";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			//FeeSchedC.ListLong=new List<FeeSched>();
			FeeSchedC.ListShort=new List<FeeSched>();
			FeeSchedC.ListLong=Crud.FeeSchedCrud.TableToList(table);
			for(int i=0;i<FeeSchedC.ListLong.Count;i++) {
				if(!FeeSchedC.ListLong[i].IsHidden) {
					FeeSchedC.ListShort.Add(FeeSchedC.ListLong[i]);
				}
			}
		}

		///<summary></summary>
		public static long Insert(FeeSched feeSched) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				feeSched.FeeSchedNum=Meth.GetLong(MethodBase.GetCurrentMethod(),feeSched);
				return feeSched.FeeSchedNum;
			}
			return Crud.FeeSchedCrud.Insert(feeSched);
		}

		///<summary></summary>
		public static void Update(FeeSched feeSched) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),feeSched);
				return;
			}
			Crud.FeeSchedCrud.Update(feeSched);
		}

		public static string GetDescription(long feeSchedNum) {
			//No need to check RemotingRole; no call to db.
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

		public static bool GetIsHidden(long feeSchedNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<FeeSchedC.ListLong.Count;i++){
				if(FeeSchedC.ListLong[i].FeeSchedNum==feeSchedNum){
					return FeeSchedC.ListLong[i].IsHidden;
				}
			}
			return true;
		}

		///<summary>Will return null if exact name not found.</summary>
		public static FeeSched GetByExactName(string description){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<FeeSchedC.ListLong.Count;i++){
				if(FeeSchedC.ListLong[i].Description==description){
					return FeeSchedC.ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Will return null if exact name not found.</summary>
		public static FeeSched GetByExactName(string description,FeeScheduleType feeSchedType){
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
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