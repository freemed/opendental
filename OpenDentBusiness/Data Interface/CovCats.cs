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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM covcat ORDER BY covorder";
			DataTable table=General.GetTable(command);
			table.TableName="CovCat";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			CovCat covcat;
			CovCatC.Listt=new List<CovCat>();
			CovCatC.ListShort=new List<CovCat>();
			for(int i=0;i<table.Rows.Count;i++) {
				covcat=new CovCat();
				covcat.CovCatNum     =PIn.PInt(table.Rows[i][0].ToString());
				covcat.Description   =PIn.PString(table.Rows[i][1].ToString());
				covcat.DefaultPercent=PIn.PInt(table.Rows[i][2].ToString());
				covcat.CovOrder      =PIn.PInt(table.Rows[i][3].ToString());
				covcat.IsHidden      =PIn.PBool(table.Rows[i][4].ToString());
				covcat.EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
				CovCatC.Listt.Add(covcat);
				if(!covcat.IsHidden) {
					CovCatC.ListShort.Add(covcat);
				}
			}
		}

		///<summary></summary>
		public static void Update(CovCat covcat) {
			string command= "UPDATE covcat SET "
				+ "Description = '"    +POut.PString(covcat.Description)+"'"
				+",DefaultPercent = '" +POut.PInt   (covcat.DefaultPercent)+"'"
				+",CovOrder = '"       +POut.PInt   (covcat.CovOrder)+"'"
				+",IsHidden = '"       +POut.PBool  (covcat.IsHidden)+"'"
				+",EbenefitCat = '"    +POut.PInt((int)covcat.EbenefitCat)+"'"
				+" WHERE covcatnum = '"+POut.PInt(covcat.CovCatNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(CovCat covcat) {
			string command="INSERT INTO covcat (Description,DefaultPercent,"
				+"CovOrder,IsHidden,EbenefitCat) VALUES("
				+"'"+POut.PString(covcat.Description)+"', "
				+"'"+POut.PInt(covcat.DefaultPercent)+"', "
				+"'"+POut.PInt(covcat.CovOrder)+"', "
				+"'"+POut.PBool(covcat.IsHidden)+"', "
				+"'"+POut.PInt((int)covcat.EbenefitCat)+"')";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void MoveUp(CovCat covcat) {
			RefreshCache();
			int oldOrder=CovCatC.GetOrderLong(covcat.CovCatNum);
			if(oldOrder==0) {
				return;
			}
			SetOrder(CovCatC.Listt[oldOrder],oldOrder-1);
			SetOrder(CovCatC.Listt[oldOrder-1],oldOrder);
		}

		///<summary></summary>
		public static void MoveDown(CovCat covcat) {
			RefreshCache();
			int oldOrder=CovCatC.GetOrderLong(covcat.CovCatNum);
			if(oldOrder==CovCatC.Listt.Count-1) {
				return;
			}
			SetOrder(CovCatC.Listt[oldOrder],oldOrder+1);
			SetOrder(CovCatC.Listt[oldOrder+1],oldOrder);
		}

		///<summary></summary>
		private static void SetOrder(CovCat covcat, int newOrder) {
			covcat.CovOrder=newOrder;
			Update(covcat);
		}

		///<summary></summary>
		public static CovCat GetCovCat(int covCatNum){
			for(int i=0;i<CovCatC.Listt.Count;i++) {
				if(covCatNum==CovCatC.Listt[i].CovCatNum) {
					return CovCatC.Listt[i].Copy();
				}
			}
			return null;//won't happen	
		}
		
		///<summary></summary>
		public static double GetDefaultPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<CovCatC.Listt.Count;i++){
				if(myCovCatNum==CovCatC.Listt[i].CovCatNum){
					retVal=(double)CovCatC.Listt[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static string GetDesc(int covCatNum){
			string retStr="";
			for(int i=0;i<CovCatC.Listt.Count;i++){
				if(covCatNum==CovCatC.Listt[i].CovCatNum){
					retStr=CovCatC.Listt[i].Description;
				}
			}
			return retStr;	
		}

		///<summary></summary>
		public static int GetCovCatNum(int orderShort){
			//need to check this again:
			int retVal=0;
			for(int i=0;i<CovCatC.ListShort.Count;i++){
				if(orderShort==CovCatC.ListShort[i].CovOrder){
					retVal=CovCatC.ListShort[i].CovCatNum;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static int GetOrderShort(int CovCatNum){
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
			for(int i=0;i<CovCatC.ListShort.Count;i++) {
				if(eben==CovCatC.ListShort[i].EbenefitCat) {
					return CovCatC.ListShort[i];
				}
			}
			return null;
		}

		

	}

	



}









