using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class TreatPlans {

		///<summary>Gets all TreatPlans for a given Patient, ordered by date.</summary>
		public static TreatPlan[] Refresh(int patNum) {
			string command="SELECT * FROM treatplan "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY DateTP";
			DataTable table=General.GetTable(command);
			TreatPlan[] List=new TreatPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TreatPlan();
				List[i].TreatPlanNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PInt(table.Rows[i][1].ToString());
				List[i].DateTP      = PIn.PDate(table.Rows[i][2].ToString());
				List[i].Heading     = PIn.PString(table.Rows[i][3].ToString());
				List[i].Note        = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}

		///<summary></summary>
		private static void Update(TreatPlan tp){
			string command= "UPDATE treatplan SET "
				+"PatNum = '"   +POut.PInt   (tp.PatNum)+"'"
				+",DateTP = "  +POut.PDate  (tp.DateTP)
				+",Heading = '" +POut.PString(tp.Heading)+"'"
				+",Note = '"    +POut.PString(tp.Note)+"'"
				+" WHERE TreatPlanNum = '"+POut.PInt(tp.TreatPlanNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(TreatPlan tp){
			if(PrefB.RandomKeys){
				tp.TreatPlanNum=MiscData.GetKey("treatplan","TreatPlanNum");
			}
			string command= "INSERT INTO treatplan (";
			if(PrefB.RandomKeys){
				command+="TreatPlanNum,";
			}
			command+="PatNum,DateTP,Heading,Note) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(tp.TreatPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (tp.PatNum)+"', "
				+POut.PDate  (tp.DateTP)+", "
				+"'"+POut.PString(tp.Heading)+"', "
				+"'"+POut.PString(tp.Note)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				tp.TreatPlanNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(TreatPlan tp, bool isNew){
			if(isNew){
				Insert(tp);
			}
			else{
				Update(tp);
			}
		}

		///<summary>Dependencies checked first and throws an exception if any found. So surround by try catch</summary>
		public static void Delete(TreatPlan tp){
			//check proctp for dependencies
			string command="SELECT * FROM proctp WHERE TreatPlanNum ="+POut.PInt(tp.TreatPlanNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				//this should never happen
				throw new InvalidProgramException(Lan.g("TreatPlans","Cannot delete treatment plan because it has ProcTP's attached"));
			}
			command= "DELETE from treatplan WHERE TreatPlanNum = '"+POut.PInt(tp.TreatPlanNum)+"'";
 			General.NonQ(command);
		}




	

	
	}

	

	


}




















