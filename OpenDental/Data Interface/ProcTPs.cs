using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ProcTPs {
		///<summary>Gets all ProcTPs for a given Patient ordered by ItemOrder.</summary>
		public static ProcTP[] Refresh(int patNum) {
			string command="SELECT * FROM proctp "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			ProcTP[] List=new ProcTP[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ProcTP();
				List[i].ProcTPNum   = PIn.PInt(table.Rows[i][0].ToString());
				List[i].TreatPlanNum= PIn.PInt(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt(table.Rows[i][2].ToString());
				List[i].ProcNumOrig = PIn.PInt(table.Rows[i][3].ToString());
				List[i].ItemOrder   = PIn.PInt(table.Rows[i][4].ToString());
				List[i].Priority    = PIn.PInt(table.Rows[i][5].ToString());
				List[i].ToothNumTP  = PIn.PString(table.Rows[i][6].ToString());
				List[i].Surf        = PIn.PString(table.Rows[i][7].ToString());
				List[i].ProcCode    = PIn.PString(table.Rows[i][8].ToString());
				List[i].Descript    = PIn.PString(table.Rows[i][9].ToString());
				List[i].FeeAmt      = PIn.PDouble(table.Rows[i][10].ToString());
				List[i].PriInsAmt   = PIn.PDouble(table.Rows[i][11].ToString());
				List[i].SecInsAmt   = PIn.PDouble(table.Rows[i][12].ToString());
				List[i].PatAmt      = PIn.PDouble(table.Rows[i][13].ToString());
				List[i].Discount    = PIn.PDouble(table.Rows[i][14].ToString());
			}
			return List;
		}
		
		///<summary></summary>
		private static void Update(ProcTP proc){
			string command= "UPDATE proctp SET "
				+"TreatPlanNum = '"+POut.PInt   (proc.TreatPlanNum)+"'"
				+",PatNum = '"     +POut.PInt   (proc.PatNum)+"'"
				+",ProcNumOrig = '"+POut.PInt   (proc.ProcNumOrig)+"'"
				+",ItemOrder = '"  +POut.PInt   (proc.ItemOrder)+"'"
				+",Priority = '"   +POut.PInt   (proc.Priority)+"'"
				+",ToothNumTP = '" +POut.PString(proc.ToothNumTP)+"'"
				+",Surf = '"       +POut.PString(proc.Surf)+"'"
				+",ProcCode = '"   +POut.PString(proc.ProcCode)+"'"
				+",Descript = '"   +POut.PString(proc.Descript)+"'"
				+",FeeAmt = '"     +POut.PDouble(proc.FeeAmt)+"'"
				+",PriInsAmt = '"  +POut.PDouble(proc.PriInsAmt)+"'"
				+",SecInsAmt = '"  +POut.PDouble(proc.SecInsAmt)+"'"
				+",PatAmt = '"     +POut.PDouble(proc.PatAmt)+"'"
				+",Discount = '"   +POut.PDouble(proc.Discount)+"'"
				+" WHERE ProcTPNum = '"+POut.PInt(proc.ProcTPNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(ProcTP proc){
			if(PrefB.RandomKeys){
				proc.ProcTPNum=MiscData.GetKey("proctp","ProcTPNum");
			}
			string command= "INSERT INTO proctp (";
			if(PrefB.RandomKeys){
				command+="ProcTPNum,";
			}
			command+="TreatPlanNum,PatNum,ProcNumOrig,ItemOrder,Priority,ToothNumTP,Surf,ProcCode,Descript,FeeAmt,"
				+"PriInsAmt,SecInsAmt,PatAmt,Discount) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(proc.ProcTPNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (proc.TreatPlanNum)+"', "
				+"'"+POut.PInt   (proc.PatNum)+"', "
				+"'"+POut.PInt   (proc.ProcNumOrig)+"', "
				+"'"+POut.PInt   (proc.ItemOrder)+"', "
				+"'"+POut.PInt   (proc.Priority)+"', "
				+"'"+POut.PString(proc.ToothNumTP)+"', "
				+"'"+POut.PString(proc.Surf)+"', "
				+"'"+POut.PString(proc.ProcCode)+"', "
				+"'"+POut.PString(proc.Descript)+"', "
				+"'"+POut.PDouble(proc.FeeAmt)+"', "
				+"'"+POut.PDouble(proc.PriInsAmt)+"', "
				+"'"+POut.PDouble(proc.SecInsAmt)+"', "
				+"'"+POut.PDouble(proc.PatAmt)+"', "
				+"'"+POut.PDouble(proc.Discount)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				proc.ProcTPNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(ProcTP proc, bool isNew){
			if(isNew){
				Insert(proc);
			}
			else{
				Update(proc);
			}
		}

		///<summary>There are no dependencies.</summary>
		public static void Delete(ProcTP proc){
			string command= "DELETE from proctp WHERE ProcTPNum = '"+POut.PInt(proc.ProcTPNum)+"'";
 			General.NonQ(command);
		}

		///<summary>Gets a list for just one tp.  Used in TP module.  Supply a list of all ProcTPs for pt.</summary>
		public static ProcTP[] GetListForTP(int treatPlanNum, ProcTP[] listAll){
			ArrayList AL=new ArrayList();
			for(int i=0;i<listAll.Length;i++){
				if(listAll[i].TreatPlanNum!=treatPlanNum){
					continue;
				}
				AL.Add(listAll[i]);
			}
			ProcTP[] retVal=new ProcTP[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>No dependencies to worry about.</summary>
		public static void DeleteForTP(int treatPlanNum){
			string command="DELETE FROM proctp "
				+"WHERE TreatPlanNum="+POut.PInt(treatPlanNum);
			General.NonQ(command);
		}

	
	}

	

	


}




















