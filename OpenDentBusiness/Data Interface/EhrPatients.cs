using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrPatients{

		///<summary>Only call when EHR is enabled.  Creates the ehrpatient record for the patient if a record does not already exist.  Always returns a non-null EhrPatient.</summary>
		public static EhrPatient Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<EhrPatient>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM ehrpatient WHERE patnum='"+POut.Long(patNum)+"'";
			if(Db.GetCount(command)=="0") {//A record does not exist for this patient yet.
				Insert(patNum);//Create a new record.
			}
			command ="SELECT * FROM ehrpatient WHERE patnum ='"+POut.Long(patNum)+"'";
			return Crud.EhrPatientCrud.SelectOne(command);
		}

		///<summary></summary>
		public static void Update(EhrPatient ehrPatient) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrPatient);
				return;
			}
			Crud.EhrPatientCrud.Update(ehrPatient);
		}

		///<summary></summary>
		private static void Insert(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			//Random keys not necessary to check because of 1:1 patNum.
			//However, this is a lazy insert, so multiple locations might attempt it.
			//Just in case, we will have it fail silently.
			EhrPatient ehrPatient=new EhrPatient();
			ehrPatient.PatNum=patNum;
			try {
				Crud.EhrPatientCrud.Insert(ehrPatient,true);
			}
			catch {
				//Fail Silently.
			}
		}
	
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrPatient> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrPatient>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrpatient WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrPatientCrud.SelectMany(command);
		}

		///<summary>Gets one EhrPatient from the db.</summary>
		public static EhrPatient GetOne(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrPatient>(MethodBase.GetCurrentMethod(),patNum);
			}
			return Crud.EhrPatientCrud.SelectOne(patNum);
		}

		///<summary></summary>
		public static void Delete(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command= "DELETE FROM ehrpatient WHERE PatNum = "+POut.Long(patNum);
			Db.NonQ(command);
		}
		*/



	}
}