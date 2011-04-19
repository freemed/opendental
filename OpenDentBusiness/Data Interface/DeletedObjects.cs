using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DeletedObjects{

		///<summary></summary>
		public static void SetDeleted(DeletedObjectType objType,long objectNum){
			DeletedObject delObj=new DeletedObject();
			delObj.ObjectNum=objectNum;
			delObj.ObjectType=objType;
			Crud.DeletedObjectCrud.Insert(delObj);
		}

		public static List<DeletedObject> GetDeletedSince(DateTime changedSince){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DeletedObject>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM deletedobject WHERE DateTStamp > "+POut.DateT(changedSince);
			return Crud.DeletedObjectCrud.SelectMany(command);
		}

		///<summary>This is only run at the server for the mobile db.  It currently handles deleted appointments.  Deleted patients are not handled here because patients never get deleted.</summary>
		public static void DeleteForMobile(List<DeletedObject> list,long customerNum) {
			for(int i=0;i<list.Count;i++) {
				//mobile
				if(list[i].ObjectType==DeletedObjectType.Appointment) {
					Mobile.Crud.AppointmentmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.RxPat) {
					Mobile.Crud.RxPatmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				//pat portal
				if(list[i].ObjectType==DeletedObjectType.Medication) {
					Mobile.Crud.MedicationmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.MedicationPat) {
					Mobile.Crud.MedicationPatmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.Allergy) {
					Mobile.Crud.AllergymCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.AllergyDef) {
					Mobile.Crud.AllergyDefmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.Disease) {
					Mobile.Crud.DiseasemCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.DiseaseDef) {
					Mobile.Crud.DiseaseDefmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.ICD9) {
					Mobile.Crud.ICD9mCrud.Delete(customerNum,list[i].ObjectNum);
				}
			}
		}


	}
}