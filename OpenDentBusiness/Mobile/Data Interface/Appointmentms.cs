using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile {
	///<summary></summary>
	public class Appointmentms {

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Appointmentm> Refresh(long patNum){
			string command="SELECT * FROM appointmentm WHERE PatNum = "+POut.Long(patNum);
			return Crud.AppointmentmCrud.SelectMany(command);
		}

		///<summary>Gets one Appointmentm from the db.</summary>
		public static Appointmentm GetOne(long customerNum,long aptNum){
			return Crud.AppointmentmCrud.SelectOne(customerNum,aptNum);
		}

		///<summary></summary>
		public static long Insert(Appointmentm appointmentm){
			return Crud.AppointmentmCrud.Insert(appointmentm,true);
		}

		///<summary></summary>
		public static void Update(Appointmentm appointmentm){
			Crud.AppointmentmCrud.Update(appointmentm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long aptNum) {
			string command= "DELETE FROM appointmentm WHERE CustomerNum = "+POut.Long(customerNum)+" AND AptNum = "+POut.Long(aptNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Appointmentm> ConvertListToM(List<Appointment> list) {
			List<Appointmentm> retVal=new List<Appointmentm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.AppointmentmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Appointmentm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				Appointmentm appointmentm=Crud.AppointmentmCrud.SelectOne(customerNum,list[i].AptNum);
				if(appointmentm==null){//not in db
					Crud.AppointmentmCrud.Insert(list[i],true);
				}
				else{
					Crud.AppointmentmCrud.Update(list[i]);
				}
			}
		}
		
		*/


	}
}