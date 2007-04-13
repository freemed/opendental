using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ProcButtonItems {
		///<summary></summary>
		public static ProcButtonItem[] List;

		///<summary>Fills List in preparation for later usage.</summary>
		public static void Refresh() {
			string command="SELECT * FROM procbuttonitem";
			DataTable table=General.GetTable(command);
			List=new ProcButtonItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ProcButtonItem();
				List[i].ProcButtonItemNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].ProcButtonNum    =PIn.PInt(table.Rows[i][1].ToString());
				List[i].ADACode          =PIn.PString(table.Rows[i][2].ToString());
				List[i].AutoCodeNum      =PIn.PInt(table.Rows[i][3].ToString());
			}
		}	

		///<summary>Must have already checked ADACode for nonduplicate.</summary>
		public static void Insert(ProcButtonItem item) {
			string command="INSERT INTO procbuttonitem (procbuttonnum,adacode,autocodenum) VALUES("
				+"'"+POut.PInt   (item.ProcButtonNum)+"', "
				+"'"+POut.PString(item.ADACode)+"', "
				+"'"+POut.PInt   (item.AutoCodeNum)+"')";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Update(ProcButtonItem item) {
			string command="UPDATE procbuttonitem SET " 
				+ "procbuttonnum='"+POut.PInt   (item.ProcButtonNum)+"'"
				+ ",adacode='"     +POut.PString(item.ADACode)+"'"
				+ ",autocodenum='" +POut.PInt   (item.AutoCodeNum)+"'"
				+" WHERE procbuttonitemnum = '"+POut.PInt(item.ProcButtonItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ProcButtonItem item) {
			string command="DELETE from procbuttonitem WHERE procbuttonitemnum = '"+POut.PInt(item.ProcButtonItemNum)+"'";
			General.NonQ(command);
		}




	

		///<summary></summary>
		public static string[] GetADAListForButton(int procButtonNum){
			ArrayList ALadaCodes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum==0){
					ALadaCodes.Add(List[i].ADACode);
				} 
			}
			string[] adaCodeList=new string[ALadaCodes.Count];
			if(ALadaCodes.Count > 0){
				ALadaCodes.CopyTo(adaCodeList);
			}
			return adaCodeList;
		}

		///<summary></summary>
		public static int[] GetAutoListForButton(int procButtonNum) {
			ArrayList ALautoCodes=new ArrayList();
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcButtonNum==procButtonNum && List[i].AutoCodeNum > 0){
					ALautoCodes.Add(List[i].AutoCodeNum);
				}
			}
			int[] autoCodeList=new int[ALautoCodes.Count];
			if(ALautoCodes.Count > 0) {
				ALautoCodes.CopyTo(autoCodeList);
			}
			return autoCodeList;
		}

		///<summary></summary>
		public static void DeleteAllForButton(int procButtonNum){
			string command= "DELETE from procbuttonitem WHERE procbuttonnum = '"+POut.PInt(procButtonNum)+"'";
			General.NonQ(command);
		}

	}

	




}










