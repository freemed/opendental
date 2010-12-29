using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile {
	///<summary></summary>
	public class Appointmentms {

		///<summary>Gets one Appointmentm from the db.</summary>
		public static Appointmentm GetOne(long customerNum,long aptNum) {
			return Crud.AppointmentmCrud.SelectOne(customerNum,aptNum);
		}

		///<summary>Gets all Appointmentm from the db as specified by the customerNum </summary>
		public static List<Appointmentm> GetAppointmentmsForList(long customerNum,DateTime startDate,DateTime endDate) {
			string command=
				"SELECT * from appointmentm "
				+"WHERE AptDateTime BETWEEN '"+POut.Date(startDate,false)+"' AND '"+POut.Date(endDate.AddDays(1),false)+"'"
				+"AND CustomerNum = "+POut.Long(customerNum);
			return Crud.AppointmentmCrud.SelectMany(command);
		}
		///<summary></summary>
		public static long Insert(Appointmentm appointmentm) {
			return Crud.AppointmentmCrud.Insert(appointmentm,true);
		}

		///<summary></summary>
		public static void Update(Appointmentm appointmentm) {
			Crud.AppointmentmCrud.Update(appointmentm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long aptNum) {
			string command= "DELETE FROM appointmentm WHERE CustomerNum = "+POut.Long(customerNum)+" AND AptNum = "+POut.Long(aptNum);
			Db.NonQ(command);
		}

		///<summary>The values returned are sent to the webserver.</summary>
		public static List<Appointmentm> GetChanged(DateTime changedSince,DateTime excludeOlderThan) {
			List<Appointment> ChangedAppointmentList=Appointments.GetChangedSince(changedSince,excludeOlderThan);
			List<Appointmentm> ChangedAppointmentmList=ConvertListToM(ChangedAppointmentList);
			return ChangedAppointmentmList;
		}

		///<summary>The values returned are sent to the webserver.</summary>
		public static List<long> GetChangedSinceAptNums(DateTime changedSince,DateTime excludeOlderThan) {
			return Appointments.GetChangedSinceAptNums(changedSince,excludeOlderThan);
		}

		///<summary>The values returned are sent to the webserver. Used if GetChanged returns large recordsets.</summary>
		public static List<Appointmentm> GetMultApts(List<long> aptNums) {
			Appointment[] aptArray=Appointments.GetMultApts(aptNums);
			List<Appointment> aptList=new List<Appointment>(aptArray);
			List<Appointmentm> aptmList=ConvertListToM(aptList);
			return aptmList;
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Appointmentm> ConvertListToM(List<Appointment> list) {
			List<Appointmentm> retVal=new List<Appointmentm>();
			for(int i=0;i<list.Count;i++) {
				retVal.Add(Crud.AppointmentmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Appointmentm> list,long customerNum) {
			for(int i=0;i<list.Count;i++) {
				list[i].CustomerNum=customerNum;
				Appointmentm appointmentm=Crud.AppointmentmCrud.SelectOne(customerNum,list[i].AptNum);
				if(appointmentm==null) {//not in db
					Crud.AppointmentmCrud.Insert(list[i],true);
				}
				else {
					Crud.AppointmentmCrud.Update(list[i]);
				}
			}
		}
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Appointmentm> Refresh(long patNum){
			string command="SELECT * FROM appointmentm WHERE PatNum = "+POut.Long(patNum);
			return Crud.AppointmentmCrud.SelectMany(command);
		}

		
		
		*/


	}
}