using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Diseases {
		public static Disease GetSpecificDiseaseForPatient(long patNum,long diseaseDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Disease>(MethodBase.GetCurrentMethod(),patNum,diseaseDefNum);
			}
			string command="SELECT * FROM disease WHERE PatNum="+POut.Long(patNum)
				+" AND DiseaseDefNum="+POut.Long(diseaseDefNum);
			Disease[] disList=RefreshAndFill(Db.GetTable(command));
			if(disList.Length==0){
				return null;
			}
			return disList[0].Copy();
		}

		///<summary>Gets a list of all Diseases for a given patient.  Includes hidden. Sorted by diseasedef.ItemOrder.</summary>
		public static Disease[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Disease[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT disease.* FROM disease,diseasedef "
				+"WHERE disease.DiseaseDefNum=diseasedef.DiseaseDefNum "
				+"AND PatNum="+POut.Long(patNum)
				+" ORDER BY diseasedef.ItemOrder";
			return RefreshAndFill(Db.GetTable(command));
		}

		private static Disease[] RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			Disease[] List=new Disease[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Disease();
				List[i].DiseaseNum   = PIn.Long(table.Rows[i][0].ToString());
				List[i].PatNum       = PIn.Long(table.Rows[i][1].ToString());
				List[i].DiseaseDefNum= PIn.Long(table.Rows[i][2].ToString());
				List[i].PatNote      = PIn.String(table.Rows[i][3].ToString());
			}
			//Array.Sort(List);
			return List;
		}

		///<summary></summary>
		public static void Update(Disease disease) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),disease);
				return;
			}
			Crud.DiseaseCrud.Update(disease);
		}

		///<summary></summary>
		public static long Insert(Disease disease) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				disease.DiseaseNum=Meth.GetLong(MethodBase.GetCurrentMethod(),disease);
				return disease.DiseaseNum;
			}
			return Crud.DiseaseCrud.Insert(disease);
		}

		///<summary></summary>
		public static void Delete(Disease disease) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),disease);
				return;
			}
			string command="DELETE FROM disease WHERE DiseaseNum ="+POut.Long(disease.DiseaseNum);
			Db.NonQ(command);
		}

		///<summary>Deletes all diseases for one patient.</summary>
		public static void DeleteAllForPt(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="DELETE FROM disease WHERE PatNum ="+POut.Long(patNum);
			Db.NonQ(command);
		}

		
		
		
		
	}

		



		
	

	

	


}










