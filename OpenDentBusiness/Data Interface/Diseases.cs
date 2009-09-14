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
			string command="SELECT * FROM disease WHERE PatNum="+POut.PLong(patNum)
				+" AND DiseaseDefNum="+POut.PLong(diseaseDefNum);
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
				+"AND PatNum="+POut.PLong(patNum)
				+" ORDER BY diseasedef.ItemOrder";
			return RefreshAndFill(Db.GetTable(command));
		}

		private static Disease[] RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			Disease[] List=new Disease[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Disease();
				List[i].DiseaseNum   = PIn.PLong(table.Rows[i][0].ToString());
				List[i].PatNum       = PIn.PLong(table.Rows[i][1].ToString());
				List[i].DiseaseDefNum= PIn.PLong(table.Rows[i][2].ToString());
				List[i].PatNote      = PIn.PString(table.Rows[i][3].ToString());
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
			string command="UPDATE disease SET " 
				+"PatNum = '"        +POut.PLong   (disease.PatNum)+"'"
				+",DiseaseDefNum = '"+POut.PLong   (disease.DiseaseDefNum)+"'"
				+",PatNote = '"      +POut.PString(disease.PatNote)+"'"
				+" WHERE DiseaseNum  ='"+POut.PLong   (disease.DiseaseNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Disease disease) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				disease.DiseaseNum=Meth.GetInt(MethodBase.GetCurrentMethod(),disease);
				return disease.DiseaseNum;
			}
			if(PrefC.RandomKeys) {
				disease.DiseaseNum=ReplicationServers.GetKey("disease","DiseaseNum");
			}
			string command="INSERT INTO disease (";
			if(PrefC.RandomKeys) {
				command+="DiseaseNum,";
			}
			command+="PatNum,DiseaseDefNum,PatNote) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(disease.DiseaseNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (disease.PatNum)+"', "
				+"'"+POut.PLong   (disease.DiseaseDefNum)+"', "
				+"'"+POut.PString(disease.PatNote)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				disease.DiseaseNum=Db.NonQ(command,true);
			}
			return disease.DiseaseNum;
		}

		///<summary></summary>
		public static void Delete(Disease disease) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),disease);
				return;
			}
			string command="DELETE FROM disease WHERE DiseaseNum ="+POut.PLong(disease.DiseaseNum);
			Db.NonQ(command);
		}

		///<summary>Deletes all diseases for one patient.</summary>
		public static void DeleteAllForPt(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="DELETE FROM disease WHERE PatNum ="+POut.PLong(patNum);
			Db.NonQ(command);
		}

		
		
		
		
	}

		



		
	

	

	


}










