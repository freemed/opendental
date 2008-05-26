using System;
using System.Collections;
using System.Data;

namespace OpenDentBusiness {
	///<summary></summary>
	public class CovCats {
		///<summary></summary>
		public static DataSet RefreshCache(){
			string command="SELECT * FROM covcat ORDER BY covorder;"
				+"SELECT * FROM covcat WHERE IsHidden = 0 ORDER BY CovOrder";
			DataSet ds=General.GetDataSet(command);
			FillCache(ds);
			return ds;
		}

		///<summary></summary>
		public static void FillCache(DataSet ds) {
			DataTable table=ds.Tables[0];
			CovCatC.Listt=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				CovCatC.Listt[i]=new CovCat();
				CovCatC.Listt[i].CovCatNum     =PIn.PInt(table.Rows[i][0].ToString());
				CovCatC.Listt[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				CovCatC.Listt[i].DefaultPercent=PIn.PInt(table.Rows[i][2].ToString());
				CovCatC.Listt[i].CovOrder      =PIn.PInt(table.Rows[i][3].ToString());
				CovCatC.Listt[i].IsHidden      =PIn.PBool(table.Rows[i][4].ToString());
				CovCatC.Listt[i].EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
			}
			table=ds.Tables[1];
			CovCatC.ListShort=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				CovCatC.ListShort[i]=new CovCat();
				CovCatC.ListShort[i].CovCatNum     =PIn.PInt(table.Rows[i][0].ToString());
				CovCatC.ListShort[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				CovCatC.ListShort[i].DefaultPercent=PIn.PInt(table.Rows[i][2].ToString());
				CovCatC.ListShort[i].CovOrder      =PIn.PInt(table.Rows[i][3].ToString());
				CovCatC.ListShort[i].IsHidden      =PIn.PBool(table.Rows[i][4].ToString());
				CovCatC.ListShort[i].EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
			}
		}
		
		/*
		///<summary></summary>
		public static void Refresh() {
			DataSet ds=null;
			try {
				if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
					DtoCovCatRefresh dto=new DtoCovCatRefresh();
					ds=RemotingClient.ProcessQuery(dto);
					CovCatB.FillLists(ds);//now, we have both lists on both the client and the server.
				}
				else {
					CovCatB.Refresh();
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
		}*/

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
			if(oldOrder==CovCatC.Listt.Length-1) {
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
			for(int i=0;i<CovCatC.Listt.Length;i++) {
				if(covCatNum==CovCatC.Listt[i].CovCatNum) {
					return CovCatC.Listt[i].Copy();
				}
			}
			return null;//won't happen	
		}
		
		///<summary></summary>
		public static double GetDefaultPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<CovCatC.Listt.Length;i++){
				if(myCovCatNum==CovCatC.Listt[i].CovCatNum){
					retVal=(double)CovCatC.Listt[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static string GetDesc(int covCatNum){
			string retStr="";
			for(int i=0;i<CovCatC.Listt.Length;i++){
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
			for(int i=0;i<CovCatC.ListShort.Length;i++){
				if(orderShort==CovCatC.ListShort[i].CovOrder){
					retVal=CovCatC.ListShort[i].CovCatNum;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static int GetOrderShort(int CovCatNum){
			int retVal=-1;
			for(int i=0;i<CovCatC.ListShort.Length;i++){
				if(CovCatNum==CovCatC.ListShort[i].CovCatNum){
					retVal=i;
				}
			}
			return retVal;	
		}

		///<summary>Gets a matching benefit category from the short list.  Returns null if not found, which should be tested for.</summary>
		public static CovCat GetForEbenCat(EbenefitCategory eben){
			for(int i=0;i<CovCatC.ListShort.Length;i++) {
				if(eben==CovCatC.ListShort[i].EbenefitCat) {
					return CovCatC.ListShort[i];
				}
			}
			return null;
		}

		

	}

	



}









