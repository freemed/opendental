using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Dunnings {

		///<summary>Gets a list of all dunnings.</summary>
		public static Dunning[] Refresh() {
			string command="SELECT * FROM dunning "
				+"ORDER BY BillingType,AgeAccount,InsIsPending";
			DataTable table=General.GetTable(command);
			Dunning[] List=new Dunning[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Dunning();
				List[i].DunningNum     = PIn.PInt(table.Rows[i][0].ToString());
				List[i].DunMessage     = PIn.PString(table.Rows[i][1].ToString());
				List[i].BillingType    = PIn.PInt(table.Rows[i][2].ToString());
				List[i].AgeAccount     = PIn.PInt(table.Rows[i][3].ToString());
				List[i].InsIsPending   = (YN)PIn.PInt(table.Rows[i][4].ToString());
			}
			return List;
		}

		///<summary></summary>
		private static void Insert(Dunning dun){
			string command= "INSERT INTO dunning (DunMessage,BillingType,AgeAccount,InsIsPending) VALUES("
				+"'"+POut.PString(dun.DunMessage)+"', "
				+"'"+POut.PInt   (dun.BillingType)+"', "
				+"'"+POut.PInt   (dun.AgeAccount)+"', "
				+"'"+POut.PInt   ((int)dun.InsIsPending)+"')";
 			dun.DunningNum=General.NonQ(command,true);
		}

		///<summary></summary>
		private static void Update(Dunning dun){
			string command= "UPDATE dunning SET " 
				+ "DunMessage = '"       +POut.PString(dun.DunMessage)+"'"
				+ ",BillingType = '"     +POut.PInt   (dun.BillingType)+"'"
				+ ",AgeAccount = '"      +POut.PInt   (dun.AgeAccount)+"'"
				+ ",InsIsPending = '"    +POut.PInt   ((int)dun.InsIsPending)+"'"
				+" WHERE DunningNum = '" +POut.PInt   (dun.DunningNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(Dunning dun,bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert(dun);
			}
			else{
				Update(dun);
			}
		}

		///<summary></summary>
		public static void Delete(Dunning dun){
			string command="DELETE FROM dunning" 
				+" WHERE DunningNum = "+POut.PInt(dun.DunningNum);
 			General.NonQ(command);
		}

		///<summary></summary>
		public static string GetMessage(Dunning[] dunList, int billingType,int ageAccount,YN insIsPending){
			//loop backwards through Dunning list and find the first dunning that matches criteria.
			for(int i=dunList.Length-1;i>=0;i--){
				if(dunList[i].BillingType!=0//0 in the list matches all
					&& dunList[i].BillingType!=billingType){
					continue;
				}
				if(ageAccount < dunList[i].AgeAccount){//match if age is >= item in list
					continue;
				}
				if(dunList[i].InsIsPending!=YN.Unknown//unknown in the list matches all
					&& dunList[i].InsIsPending!=insIsPending){
					continue;
				}
				return dunList[i].DunMessage;
			}
			return "";
		}

		

	}
	


}













