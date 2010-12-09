using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile {
	///<summary></summary>
	public class Patientms {

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Patientm> Refresh(long patNum){
			string command="SELECT * FROM patientm WHERE PatNum = "+POut.Long(patNum);
			return Crud.PatientmCrud.SelectMany(command);
		}

		///<summary>Gets one Patientm from the db.</summary>
		public static Patientm GetOne(long customerNum,long patNum){
			return Crud.PatientmCrud.SelectOne(customerNum,patNum);
		}

		///<summary></summary>
		public static long Insert(Patientm patientm){
			return Crud.PatientmCrud.Insert(patientm,true);
		}

		///<summary></summary>
		public static void Update(Patientm patientm){
			Crud.PatientmCrud.Update(patientm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long patNum) {
			string command= "DELETE FROM patientm WHERE CustomerNum = "+POut.Long(customerNum)+" AND PatNum = "+POut.Long(patNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Patientm> ConvertListToM(List<Patient> list) {
			List<Patientm> retVal=new List<Patientm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.PatientmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.</summary>
		public static void UpdateFromChangeList(List<Patientm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				Patientm patientm=Crud.PatientmCrud.SelectOne(customerNum,list[i].PatNum);
				if(patientm==null){//not in db
					Crud.PatientmCrud.Insert(list[i],true);
				}
				else{
					Crud.PatientmCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}