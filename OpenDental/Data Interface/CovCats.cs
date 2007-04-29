using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class CovCats {
		
		///<summary></summary>
		public static void Refresh() {
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					CovCatB.Refresh();
				}
				else {
					DtoCovCatRefresh dto=new DtoCovCatRefresh();
					ds=RemotingClient.ProcessQuery(dto);
					CovCatB.FillLists(ds);//now, we have both lists on both the client and the server.
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
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
			CovCats.Refresh();
			int oldOrder=CovCatB.GetOrderLong(covcat.CovCatNum);
			if(oldOrder==0) {
				return;
			}
			SetOrder(CovCatB.Listt[oldOrder],oldOrder-1);
			SetOrder(CovCatB.Listt[oldOrder-1],oldOrder);
		}

		///<summary></summary>
		public static void MoveDown(CovCat covcat) {
			CovCats.Refresh();
			int oldOrder=CovCatB.GetOrderLong(covcat.CovCatNum);
			if(oldOrder==CovCatB.Listt.Length-1) {
				return;
			}
			SetOrder(CovCatB.Listt[oldOrder],oldOrder+1);
			SetOrder(CovCatB.Listt[oldOrder+1],oldOrder);
		}

		///<summary></summary>
		private static void SetOrder(CovCat covcat, int newOrder) {
			covcat.CovOrder=newOrder;
			Update(covcat);
		}

		///<summary></summary>
		public static CovCat GetCovCat(int covCatNum){
			for(int i=0;i<CovCatB.Listt.Length;i++) {
				if(covCatNum==CovCatB.Listt[i].CovCatNum) {
					return CovCatB.Listt[i].Copy();
				}
			}
			return null;//won't happen	
		}
		
		///<summary></summary>
		public static double GetDefaultPercent(int myCovCatNum){
			double retVal=0;
			for(int i=0;i<CovCatB.Listt.Length;i++){
				if(myCovCatNum==CovCatB.Listt[i].CovCatNum){
					retVal=(double)CovCatB.Listt[i].DefaultPercent;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static string GetDesc(int covCatNum){
			string retStr="";
			for(int i=0;i<CovCatB.Listt.Length;i++){
				if(covCatNum==CovCatB.Listt[i].CovCatNum){
					retStr=CovCatB.Listt[i].Description;
				}
			}
			return retStr;	
		}

		///<summary></summary>
		public static int GetCovCatNum(int orderShort){
			//need to check this again:
			int retVal=0;
			for(int i=0;i<CovCatB.ListShort.Length;i++){
				if(orderShort==CovCatB.ListShort[i].CovOrder){
					retVal=CovCatB.ListShort[i].CovCatNum;
				}
			}
			return retVal;	
		}

		///<summary></summary>
		public static int GetOrderShort(int CovCatNum){
			int retVal=-1;
			for(int i=0;i<CovCatB.ListShort.Length;i++){
				if(CovCatNum==CovCatB.ListShort[i].CovCatNum){
					retVal=i;
				}
			}
			return retVal;	
		}

		///<summary>Gets a matching benefit category from the short list.  Returns null if not found, which should be tested for.</summary>
		public static CovCat GetForEbenCat(EbenefitCategory eben){
			for(int i=0;i<CovCatB.ListShort.Length;i++) {
				if(eben==CovCatB.ListShort[i].EbenefitCat) {
					return CovCatB.ListShort[i];
				}
			}
			return null;
		}

		

	}

	



}









