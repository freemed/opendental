using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile {
	///<summary></summary>
	public class Patientms {

		///<summary>Gets one Patientm from the db.</summary>
		public static Patientm GetOne(long customerNum,long patNum) {
			return Crud.PatientmCrud.SelectOne(customerNum,patNum);
		}

		///<summary>This would be executed on the webserver only</summary>
		public static long Insert(Patientm patientm) {
			return Crud.PatientmCrud.Insert(patientm,true);
		}

		///<summary>This would be executed on the webserver only</summary>
		public static void Update(Patientm patientm) {
			Crud.PatientmCrud.Update(patientm);
		}

		///<summary>This would be executed on the webserver only</summary>
		public static void Delete(long customerNum,long patNum) {
			Crud.PatientmCrud.Delete(customerNum, patNum);
		}

		/// <summary>This would be executed on the webserver only. One of the method which is used to decided if certain missing records are to be transferred to the webserver. This is especially true when synchronization happens for the very first time when there are no records on the  webserver./// </summary>
		public static int GetRecordCount(String RegistrationKey) {
			return 0;
		}
		
		/// <summary>This would be executed on the webserver only. One of the method which is used to decided if certain missing records are to be transferred to the webserver. This is especially true when synchronization happens for the very first time when there are no records on the  webserver./// </summary>
		public static long[] GetExistingRecordIds(String RegistrationKey) {
			long customerNum=0;
			string command="SELECT PatNum FROM patientm"
				+"WHERE CustomerNum = "+POut.Long(customerNum);
			Db.GetDataSet(command);
			long[] RecordIds= { };
			return RecordIds;
		}
		///<summary>The values returned are sent to the webserver.</summary>
		public static List<Patientm> GetChanged(DateTime changedSince) {
			List<Patient> ChangedPatientList=Patients.GetChangedSince(changedSince);
			List<Patientm> ChangedPatientmList=ConvertListToM(ChangedPatientList);
			return ChangedPatientmList;
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Patientm> ConvertListToM(List<Patient> list) {
			List<Patientm> retVal=new List<Patientm>();
			for(int i=0;i<list.Count;i++) {
				retVal.Add(Crud.PatientmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.</summary>
		public static void UpdateFromChangeList(List<Patientm> list,long customerNum) {
			for(int i=0;i<list.Count;i++) {
				list[i].CustomerNum=customerNum;
				Patientm patientm=Crud.PatientmCrud.SelectOne(customerNum,list[i].PatNum);
				if(patientm==null) {//not in db
					Crud.PatientmCrud.Insert(list[i],true);
				}
				else {
					Crud.PatientmCrud.Update(list[i]);
				}
			}
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Patientm> Refresh(long patNum){
			string command="SELECT * FROM patientm WHERE PatNum = "+POut.Long(patNum);
			return Crud.PatientmCrud.SelectMany(command);
		}




		*/



	}
}