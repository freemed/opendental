using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class CovCats {
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM covcat ORDER BY covorder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="CovCat";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			CovCatC.Listt=Crud.CovCatCrud.TableToList(table);
			CovCatC.ListShort=new List<CovCat>();
			for(int i=0;i<CovCatC.Listt.Count;i++) {
				if(!CovCatC.Listt[i].IsHidden) {
					CovCatC.ListShort.Add(CovCatC.Listt[i]);
				}
			}
			

			
			
			
			CovCat covcat;
			CovCatC.Listt=new List<CovCat>();

				covcat=new CovCat();
				covcat.CovCatNum     =PIn.Long(table.Rows[i][0].ToString());
				covcat.Description   =PIn.String(table.Rows[i][1].ToString());
				covcat.DefaultPercent=PIn.Int(table.Rows[i][2].ToString());
				covcat.CovOrder      =PIn.Byte(table.Rows[i][3].ToString());
				covcat.IsHidden      =PIn.Bool(table.Rows[i][4].ToString());
				covcat.EbenefitCat   =(EbenefitCategory)PIn.Long(table.Rows[i][5].ToString());
				CovCatC.Listt.Add(covcat);
				
			}
		}

		///<summary></summary>
		public static void Update(CovCat covcat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),covcat);
				return;
			}
			Crud.CovCatCrud.Update(covcat);
		}

		///<summary></summary>
		public static long Insert(CovCat covcat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				covcat.CovCatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),covcat);
				return covcat.CovCatNum;
			}
			return Crud.CovCatCrud.Insert(covcat);
		}

		///<summary></summary>
		public static void MoveUp(CovCat covcat) {
			//No need to check RemotingRole; no call to db.
			RefreshCache();
			int oldOrder=CovCatC.GetOrderLong(covcat.CovCatNum);
			if(oldOrder==0 || oldOrder==-1) {
				return;
			}
			SetOrder(CovCatC.Listt[oldOrder],(byte)(oldOrder-1));
			SetOrder(CovCatC.Listt[oldOrder-1],(byte)oldOrder);
		}

		///<summary></summary>
		public static void MoveDown(CovCat covcat) {
			//No need to check RemotingRole; no call to db.
			RefreshCache();
			int oldOrder=CovCatC.GetOrderLong(covcat.CovCatNum);
			if(oldOrder==CovCatC.Listt.Count-1 || oldOrder==-1) {
				return;
			}
			SetOrder(CovCatC.Listt[oldOrder],(byte)(oldOrder+1));
			SetOrder(CovCatC.Listt[oldOrder+1],(byte)oldOrder);
		}

		///<summary></summary>
		private static void SetOrder(CovCat covcat, byte newOrder) {
			//No need to check RemotingRole; no call to db.
			covcat.CovOrder=newOrder;
			Update(covcat);
		}

		///<summary></summary>
		public static CovCat GetCovCat(long covCatNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<CovCatC.Listt.Count;i++) {
				if(covCatNum==CovCatC.Listt[i].CovCatNum) {
					return CovCatC.Listt[i].Copy();
				}
			}
			return null;//won't happen	
		}
		
		///<summary></summary>
		public static double GetDefaultPercent(long myCovCatNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<CovCatC.Listt.Count;i++){
				if(myCovCatNum==CovCatC.Listt[i].CovCatNum){
					retVal=(double)CovCatC.Listt[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static string GetDesc(long covCatNum) {
			//No need to check RemotingRole; no call to db.
			string retStr="";
			for(int i=0;i<CovCatC.Listt.Count;i++){
				if(covCatNum==CovCatC.Listt[i].CovCatNum){
					retStr=CovCatC.Listt[i].Description;
				}
			}
			return retStr;	
		}

		///<summary></summary>
		public static long GetCovCatNum(int orderShort){
			//No need to check RemotingRole; no call to db.
			//need to check this again:
			long retVal=0;
			for(int i=0;i<CovCatC.ListShort.Count;i++){
				if(orderShort==CovCatC.ListShort[i].CovOrder){
					retVal=CovCatC.ListShort[i].CovCatNum;
				}
			}
			return retVal;	
		}

		///<summary>Returns -1 if not in ListShort.</summary>
		public static int GetOrderShort(long CovCatNum) {
			//No need to check RemotingRole; no call to db.
			int retVal=-1;
			for(int i=0;i<CovCatC.ListShort.Count;i++){
				if(CovCatNum==CovCatC.ListShort[i].CovCatNum){
					retVal=i;
				}
			}
			return retVal;	
		}

		///<summary>Gets a matching benefit category from the short list.  Returns null if not found, which should be tested for.</summary>
		public static CovCat GetForEbenCat(EbenefitCategory eben){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<CovCatC.ListShort.Count;i++) {
				if(eben==CovCatC.ListShort[i].EbenefitCat) {
					return CovCatC.ListShort[i];
				}
			}
			return null;
		}

		///<summary>If none assigned, it will return None.</summary>
		public static EbenefitCategory GetEbenCat(long covCatNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<CovCatC.ListShort.Count;i++) {
				if(covCatNum==CovCatC.ListShort[i].CovCatNum) {
					return CovCatC.ListShort[i].EbenefitCat;
				}
			}
			return EbenefitCategory.None;
		}

		public static int CountForEbenCat(EbenefitCategory eben) {
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<CovCatC.ListShort.Count;i++) {
				if(CovCatC.ListShort[i].EbenefitCat == eben) {
					retVal++;
				}
			}
			return retVal;
		}

		public static void SetOrdersToDefault() {
			//This can only be run if the validation checks have been run first.
			//No need to check RemotingRole; no call to db.
			SetOrder(GetForEbenCat(EbenefitCategory.General),0);
			SetOrder(GetForEbenCat(EbenefitCategory.Diagnostic),1);
			SetOrder(GetForEbenCat(EbenefitCategory.DiagnosticXRay),2);
			SetOrder(GetForEbenCat(EbenefitCategory.RoutinePreventive),3);
			SetOrder(GetForEbenCat(EbenefitCategory.Restorative),4);
			SetOrder(GetForEbenCat(EbenefitCategory.Endodontics),5);
			SetOrder(GetForEbenCat(EbenefitCategory.Periodontics),6);
			SetOrder(GetForEbenCat(EbenefitCategory.OralSurgery),7);
			SetOrder(GetForEbenCat(EbenefitCategory.Crowns),8);
			SetOrder(GetForEbenCat(EbenefitCategory.Prosthodontics),9);
			SetOrder(GetForEbenCat(EbenefitCategory.MaxillofacialProsth),10);
			SetOrder(GetForEbenCat(EbenefitCategory.Accident),11);
			SetOrder(GetForEbenCat(EbenefitCategory.Orthodontics),12);
			SetOrder(GetForEbenCat(EbenefitCategory.Adjunctive),13);
			//now set the remaining categories to come after the ebens.
			byte idx=14;
			for(int i=0;i<CovCatC.ListShort.Count;i++) {
				if(CovCatC.ListShort[i].EbenefitCat !=EbenefitCategory.None) {
					continue;
				}
				SetOrder(CovCatC.ListShort[i],idx);
				idx++;
			}
			//finally, the hidden categories
			for(int i=0;i<CovCatC.Listt.Count;i++) {
				if(!CovCatC.Listt[i].IsHidden) {
					continue;
				}
				SetOrder(CovCatC.Listt[i],idx);
				idx++;
			}
		}

		public static void SetSpansToDefault() {
			//This can only be run if the validation checks have been run first.
			//No need to check RemotingRole; no call to db.
			long covCatNum;
			CovSpan span;
			covCatNum=GetForEbenCat(EbenefitCategory.General).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D0000";
			span.ToCode="D7999";
			CovSpans.Insert(span);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D9000";
			span.ToCode="D9999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D0000";
			span.ToCode="D0999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.DiagnosticXRay).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D0200";
			span.ToCode="D0399";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D1000";
			span.ToCode="D1999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D2000";
			span.ToCode="D2699";
			CovSpans.Insert(span);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D2800";
			span.ToCode="D2999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D3000";
			span.ToCode="D3999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D4000";
			span.ToCode="D4999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.OralSurgery).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D7000";
			span.ToCode="D7999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Crowns).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D2700";
			span.ToCode="D2799";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D5000";
			span.ToCode="D5899";
			CovSpans.Insert(span);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D6200";
			span.ToCode="D6899";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.MaxillofacialProsth).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D5900";
			span.ToCode="D5999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Accident).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			covCatNum=GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D8000";
			span.ToCode="D8999";
			CovSpans.Insert(span);
			covCatNum=GetForEbenCat(EbenefitCategory.Adjunctive).CovCatNum;
			CovSpans.DeleteForCat(covCatNum);
			span=new CovSpan();
			span.CovCatNum=covCatNum;
			span.FromCode="D9000";
			span.ToCode="D9999";
			CovSpans.Insert(span);


		}



	}

	



}









