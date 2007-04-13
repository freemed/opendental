using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the adjustment table in the db.</summary>
	public class Adjustments {

		///<summary></summary>
		private static void Update(Adjustment adj){
			string command="UPDATE adjustment SET " 
				+ "adjdate = "      +POut.PDate  (adj.AdjDate)
				+ ",adjamt = '"      +POut.PDouble(adj.AdjAmt)+"'"
				+ ",patnum = '"      +POut.PInt   (adj.PatNum)+"'"
				+ ",adjtype = '"     +POut.PInt   (adj.AdjType)+"'"
				+ ",provnum = '"     +POut.PInt   (adj.ProvNum)+"'"
				+ ",adjnote = '"     +POut.PString(adj.AdjNote)+"'"
				+ ",ProcDate = "    +POut.PDate  (adj.ProcDate)
				+ ",ProcNum = '"     +POut.PInt   (adj.ProcNum)+"'"
				//DateEntry not allowed to change
				+" WHERE adjNum = '" +POut.PInt   (adj.AdjNum)+"'";
			//MessageBox.Show(string command);
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(Adjustment adj){
			if(PrefB.RandomKeys){
				adj.AdjNum=MiscData.GetKey("adjustment","AdjNum");
			}
			string command= "INSERT INTO adjustment (";
			if(PrefB.RandomKeys){
				command+="AdjNum,";
			}
			command+="AdjDate,AdjAmt,PatNum, "
				+"AdjType,ProvNum,AdjNote,ProcDate,ProcNum,DateEntry) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(adj.AdjNum)+"', ";
			}
			command+=
				 POut.PDate  (adj.AdjDate)+", "
				+"'"+POut.PDouble(adj.AdjAmt)+"', "
				+"'"+POut.PInt   (adj.PatNum)+"', "
				+"'"+POut.PInt   (adj.AdjType)+"', "
				+"'"+POut.PInt   (adj.ProvNum)+"', "
				+"'"+POut.PString(adj.AdjNote)+"', "
				+POut.PDate  (adj.ProcDate)+", "
				+"'"+POut.PInt   (adj.ProcNum)+"', ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}else{//Assume MySQL
				command+="NOW()";//DateEntry set to server date
			}
			command+=")";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				adj.AdjNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(Adjustment adj, bool IsNew){
			//if(){
				//throw new Exception(Lan.g(this,""));
			//}
			if(IsNew){
				Insert(adj);
			}
			else{
				Update(adj);
			}
		}

		///<summary>This will soon be eliminated or changed to only allow deleting on same day as EntryDate.</summary>
		public static void Delete(Adjustment adj){
			string command="DELETE FROM adjustment "
				+"WHERE AdjNum = '"+adj.AdjNum.ToString()+"'";
 			General.NonQ(command);
		}

		///<summary>Gets all adjustments for a single patient.</summary>
		public static Adjustment[] Refresh(int patNum){
			string command=
				"SELECT * FROM adjustment"
				+" WHERE PatNum = '"+patNum.ToString()+"' ORDER BY AdjDate";
 			DataTable table=General.GetTable(command);
			Adjustment[] List=new Adjustment[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Adjustment();
				List[i].AdjNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AdjDate  = PIn.PDate  (table.Rows[i][1].ToString());
				List[i].AdjAmt   = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].PatNum   = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].AdjType  = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum  = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].AdjNote  = PIn.PString(table.Rows[i][6].ToString());
				List[i].ProcDate = PIn.PDate  (table.Rows[i][7].ToString());
				List[i].ProcNum  = PIn.PInt   (table.Rows[i][8].ToString());
				List[i].DateEntry= PIn.PDate  (table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary>Loops through the supplied list of adjustments and returns an ArrayList of adjustments for the given proc.</summary>
		public static ArrayList GetForProc(int procNum,Adjustment[] List){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Used from ContrAccount and ProcEdit to display and calculate adjustments attached to procs.</summary>
		public static double GetTotForProc(int procNum,Adjustment[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal+=List[i].AdjAmt;
				}
			}
			return retVal;
		}

		///<summary>Must make sure Refresh is done first.  Returns the sum of all adjustments for this patient.  Amount might be pos or neg.</summary>
		public static double ComputeBal(Adjustment[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].AdjAmt;
			}
			return retVal;
		}
	}

	


	


}









