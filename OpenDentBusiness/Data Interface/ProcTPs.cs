using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcTPs {
		///<summary>Gets all ProcTPs for a given Patient ordered by ItemOrder.</summary>
		public static ProcTP[] Refresh(int patNum) {
			string command="SELECT * FROM proctp "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY ItemOrder";
			return RefreshAndFill(command).ToArray();
		}

		///<summary>Only used when obtaining the signature data.  Ordered by ItemOrder.</summary>
		public static List<ProcTP> RefreshForTP(int tpNum){
			string command="SELECT * FROM proctp "
				+"WHERE TreatPlanNum="+POut.PInt(tpNum)
				+" ORDER BY ItemOrder";
			return RefreshAndFill(command);
		}

		private static List<ProcTP> RefreshAndFill(string command){
			DataTable table=Db.GetTable(command);
			List<ProcTP> retVal=new List<ProcTP>();
			ProcTP proc;
			for(int i=0;i<table.Rows.Count;i++) {
				proc=new ProcTP();
				proc.ProcTPNum   = PIn.PInt(table.Rows[i][0].ToString());
				proc.TreatPlanNum= PIn.PInt(table.Rows[i][1].ToString());
				proc.PatNum      = PIn.PInt(table.Rows[i][2].ToString());
				proc.ProcNumOrig = PIn.PInt(table.Rows[i][3].ToString());
				proc.ItemOrder   = PIn.PInt(table.Rows[i][4].ToString());
				proc.Priority    = PIn.PInt(table.Rows[i][5].ToString());
				proc.ToothNumTP  = PIn.PString(table.Rows[i][6].ToString());
				proc.Surf        = PIn.PString(table.Rows[i][7].ToString());
				proc.ProcCode    = PIn.PString(table.Rows[i][8].ToString());
				proc.Descript    = PIn.PString(table.Rows[i][9].ToString());
				proc.FeeAmt      = PIn.PDouble(table.Rows[i][10].ToString());
				proc.PriInsAmt   = PIn.PDouble(table.Rows[i][11].ToString());
				proc.SecInsAmt   = PIn.PDouble(table.Rows[i][12].ToString());
				proc.PatAmt      = PIn.PDouble(table.Rows[i][13].ToString());
				proc.Discount    = PIn.PDouble(table.Rows[i][14].ToString());
				retVal.Add(proc);
			}
			return retVal;
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
 			Db.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(ProcTP proc){
			if(PrefC.RandomKeys){
				proc.ProcTPNum=MiscData.GetKey("proctp","ProcTPNum");
			}
			string command= "INSERT INTO proctp (";
			if(PrefC.RandomKeys){
				command+="ProcTPNum,";
			}
			command+="TreatPlanNum,PatNum,ProcNumOrig,ItemOrder,Priority,ToothNumTP,Surf,ProcCode,Descript,FeeAmt,"
				+"PriInsAmt,SecInsAmt,PatAmt,Discount) VALUES(";
			if(PrefC.RandomKeys){
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
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				proc.ProcTPNum=Db.NonQ(command,true);
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
 			Db.NonQ(command);
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
			Db.NonQ(command);
		}

	
	}

	

	


}




















