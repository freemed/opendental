using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public class CovCatB {
		///<summary>All CovCats</summary>
		public static CovCat[] Listt;
		///<summary>Only CovCats that are not hidden.</summary>
		public static CovCat[] ListShort;

		public static DataSet Refresh() {
			string command="SELECT * FROM covcat ORDER BY covorder;"
				+"SELECT * FROM covcat WHERE IsHidden = 0 ORDER BY CovOrder";
			DataConnection dcon=new DataConnection();
			DataSet retVal=dcon.GetDs(command);
			FillLists(retVal);
			return retVal;
		}

		///<summary></summary>
		public static void FillLists(DataSet ds) {
			DataTable table=ds.Tables[0];
			Listt=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				Listt[i]=new CovCat();
				Listt[i].CovCatNum     =PIn.PInt(table.Rows[i][0].ToString());
				Listt[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				Listt[i].DefaultPercent=PIn.PInt(table.Rows[i][2].ToString());
				Listt[i].CovOrder      =PIn.PInt(table.Rows[i][3].ToString());
				Listt[i].IsHidden      =PIn.PBool(table.Rows[i][4].ToString());
				Listt[i].EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
			}
			table=ds.Tables[1];
			ListShort=new CovCat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				ListShort[i]=new CovCat();
				ListShort[i].CovCatNum     =PIn.PInt(table.Rows[i][0].ToString());
				ListShort[i].Description   =PIn.PString(table.Rows[i][1].ToString());
				ListShort[i].DefaultPercent=PIn.PInt(table.Rows[i][2].ToString());
				ListShort[i].CovOrder      =PIn.PInt(table.Rows[i][3].ToString());
				ListShort[i].IsHidden      =PIn.PBool(table.Rows[i][4].ToString());
				ListShort[i].EbenefitCat   =(EbenefitCategory)PIn.PInt(table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static int GetOrderLong(int covCatNum) {
			for(int i=0;i<Listt.Length;i++) {
				if(covCatNum==Listt[i].CovCatNum) {
					return i;
				}
			}
			return -1;
		}	

		

		

	}
}
