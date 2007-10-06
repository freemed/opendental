using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class Diseases {
		public static Disease GetSpecificDiseaseForPatient(int patNum,int diseaseDefNum) {
			string command="SELECT * FROM disease WHERE PatNum="+POut.PInt(patNum)
				+" AND DiseaseDefNum="+POut.PInt(diseaseDefNum);
			Disease[] disList=RefreshAndFill(command);
			if(disList.Length==0){
				return null;
			}
			return disList[0].Copy();
		}

		///<summary>Gets a list of all Diseases for a given patient.  Includes hidden. Sorted by diseasedef.ItemOrder.</summary>
		public static Disease[] Refresh(int patNum) {
			string command="SELECT disease.* FROM disease,diseasedef "
				+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
				+"AND PatNum="+POut.PInt(patNum)
				+" ORDER BY diseasedef.ItemOrder";
			return RefreshAndFill(command);
		}

		private static Disease[] RefreshAndFill(string command){
			DataTable table=General.GetTable(command);
			Disease[] List=new Disease[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Disease();
				List[i].DiseaseNum   = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum       = PIn.PInt(table.Rows[i][1].ToString());
				List[i].DiseaseDefNum= PIn.PInt(table.Rows[i][2].ToString());
				List[i].PatNote      = PIn.PString(table.Rows[i][3].ToString());
			}
			//Array.Sort(List);
			return List;
		}

		///<summary></summary>
		public static void Update(Disease disease) {
			string command="UPDATE disease SET " 
				+"PatNum = '"        +POut.PInt   (disease.PatNum)+"'"
				+",DiseaseDefNum = '"+POut.PInt   (disease.DiseaseDefNum)+"'"
				+",PatNote = '"      +POut.PString(disease.PatNote)+"'"
				+" WHERE DiseaseNum  ='"+POut.PInt   (disease.DiseaseNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Disease disease) {
			if(PrefB.RandomKeys) {
				disease.DiseaseNum=MiscData.GetKey("disease","DiseaseNum");
			}
			string command="INSERT INTO disease (";
			if(PrefB.RandomKeys) {
				command+="DiseaseNum,";
			}
			command+="PatNum,DiseaseDefNum,PatNote) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(disease.DiseaseNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (disease.PatNum)+"', "
				+"'"+POut.PInt   (disease.DiseaseDefNum)+"', "
				+"'"+POut.PString(disease.PatNote)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				disease.DiseaseNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(Disease disease) {
			string command="DELETE FROM disease WHERE DiseaseNum ="+POut.PInt(disease.DiseaseNum);
			General.NonQ(command);
		}

		///<summary>Deletes all diseases for one patient.</summary>
		public static void DeleteAllForPt(int patNum){
			string command="DELETE FROM disease WHERE PatNum ="+POut.PInt(patNum);
			General.NonQ(command);
		}

		
		
		
		
	}

		



		
	

	

	


}










