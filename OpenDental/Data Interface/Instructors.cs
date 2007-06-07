using System;
using System.Collections;
/*using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Instructors {
		///<summary></summary>
		public static Instructor[] List;

		///<summary>Refreshes all instructors.</summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM instructor "
				+"ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			List=new Instructor[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Instructor();
				List[i].InstructorNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].LName        = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName        = PIn.PString(table.Rows[i][2].ToString());
				List[i].Suffix       = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		private static void Update(Instructor instr){
			string command= "UPDATE instructor SET " 
				+"InstructorNum = '"  +POut.PInt   (instr.InstructorNum)+"'"
				+",LName = '"         +POut.PString(instr.LName)+"'"
				+",FName = '"         +POut.PString(instr.FName)+"'"
				+",Suffix = '"        +POut.PString(instr.Suffix)+"'"
				+" WHERE InstructorNum = '"+POut.PInt(instr.InstructorNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(Instructor instr){
			if(PrefB.RandomKeys){
				instr.InstructorNum=MiscData.GetKey("instructor","InstructorNum");
			}
			string command= "INSERT INTO instructor (";
			if(PrefB.RandomKeys){
				command+="InstructorNum,";
			}
			command+="LName,FName,Suffix) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(instr.InstructorNum)+"', ";
			}
			command+=
				 "'"+POut.PString(instr.LName)+"', "
				+"'"+POut.PString(instr.FName)+"', "
				+"'"+POut.PString(instr.Suffix)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				instr.InstructorNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(Instructor instr, bool isNew){
			//if(IsRepeating && DateTask.Year>1880){
			//	throw new Exception(Lan.g(this,"Task cannot be tagged repeating and also have a date."));
			//}
			if(isNew){
				Insert(instr);
			}
			else{
				Update(instr);
			}
		}

		///<summary></summary>
		public static void Delete(Instructor instr){
			//todo: check for dependencies

			string command= "DELETE from instructor WHERE InstructorNum = '"
				+POut.PInt(instr.InstructorNum)+"'";
 			General.NonQ(command);
		}


	

	
	}

	

	


}
*/



















