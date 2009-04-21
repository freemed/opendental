using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PerioMeasures{
		///<summary>List of all perio measures for the current patient. Dim 1 is exams. Dim 2 is Sequences. Dim 3 is Measurements, always 33 per sequence(0 is not used).</summary>
		public static PerioMeasure[,,] List;

		///<summary></summary>
		public static void Update(PerioMeasure Cur){
			string command = "UPDATE periomeasure SET "
				+ "PerioExamNum = '"+POut.PInt   (Cur.PerioExamNum)+"'"
				+",SequenceType = '"+POut.PInt   ((int)Cur.SequenceType)+"'"
				+",IntTooth = '"    +POut.PInt   (Cur.IntTooth)+"'"
				+",ToothValue = '"  +POut.PInt   (Cur.ToothValue)+"'"
				+",MBvalue = '"     +POut.PInt   (Cur.MBvalue)+"'"
				+",Bvalue = '"      +POut.PInt   (Cur.Bvalue)+"'"
				+",DBvalue = '"     +POut.PInt   (Cur.DBvalue)+"'"
				+",MLvalue = '"     +POut.PInt   (Cur.MLvalue)+"'"
				+",Lvalue = '"      +POut.PInt   (Cur.Lvalue)+"'"
				+",DLvalue = '"     +POut.PInt   (Cur.DLvalue)+"'"
				+" WHERE PerioMeasureNum = '"+POut.PInt(Cur.PerioMeasureNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PerioMeasure Cur){
			if(PrefC.RandomKeys){
				Cur.PerioMeasureNum=MiscData.GetKey("periomeasure","PerioMeasureNum");
			}
			string command="INSERT INTO periomeasure (";
			if(PrefC.RandomKeys){
				command+="PerioMeasureNum,";
			}
			command+="PerioExamNum,SequenceType,IntTooth,ToothValue,"
				+"MBvalue,Bvalue,DBvalue,MLvalue,Lvalue,DLvalue"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.PerioMeasureNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PerioExamNum)+"', "
				+"'"+POut.PInt   ((int)Cur.SequenceType)+"', "
				+"'"+POut.PInt   (Cur.IntTooth)+"', "
				+"'"+POut.PInt   (Cur.ToothValue)+"', "
				+"'"+POut.PInt   (Cur.MBvalue)+"', "
				+"'"+POut.PInt   (Cur.Bvalue)+"', "
				+"'"+POut.PInt   (Cur.DBvalue)+"', "
				+"'"+POut.PInt   (Cur.MLvalue)+"', "
				+"'"+POut.PInt   (Cur.Lvalue)+"', "
				+"'"+POut.PInt   (Cur.DLvalue)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.PerioMeasureNum=Db.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(PerioMeasure Cur){
			string command= "DELETE from periomeasure WHERE PerioMeasureNum = '"
				+Cur.PerioMeasureNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>For the current exam, clears existing skipped teeth and resets them to the specified skipped teeth. The ArrayList valid values are 1-32 int.</summary>
		public static void SetSkipped(int perioExamNum, ArrayList skippedTeeth){
			//for(int i=0;i<skippedTeeth.Count;i++){
			//MessageBox.Show(skippedTeeth[i].ToString());
			//}
			//first, delete all skipped teeth for this exam
			string command = "DELETE from periomeasure WHERE "
				+"PerioExamNum = '"+perioExamNum.ToString()+"' "
				+"AND SequenceType = '"+POut.PInt((int)PerioSequenceType.SkipTooth)+"'";
			Db.NonQ(command);
			//then add the new ones in one at a time.
			PerioMeasure Cur;
			for(int i=0;i<skippedTeeth.Count;i++){
				Cur=new PerioMeasure();
				Cur.PerioExamNum=perioExamNum;
				Cur.SequenceType=PerioSequenceType.SkipTooth;
				Cur.IntTooth=(int)skippedTeeth[i];
				Cur.ToothValue=1;
				Cur.MBvalue=-1;
				Cur.Bvalue=-1;
				Cur.DBvalue=-1;
				Cur.MLvalue=-1;
				Cur.Lvalue=-1;
				Cur.DLvalue=-1;
				Insert(Cur);
			}
		}

		///<summary>Used in FormPerio.Add_Click. For the specified exam, gets a list of all skipped teeth. The ArrayList valid values are 1-32 int.</summary>
		public static ArrayList GetSkipped(int perioExamNum){
			string command = "SELECT IntTooth FROM periomeasure WHERE "
				+"SequenceType = '"+POut.PInt((int)PerioSequenceType.SkipTooth)+"' "
				+"AND PerioExamNum = '"+perioExamNum.ToString()+"' "
				+"AND ToothValue = '1'";
			DataTable table=Db.GetTable(command);
			ArrayList retList=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				retList.Add(PIn.PInt(table.Rows[i][0].ToString()));
			}
			return retList;
		}

	
	}
	
	

}















