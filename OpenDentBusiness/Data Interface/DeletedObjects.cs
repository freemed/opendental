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

		#region Used only on OD
		///<summary>The values returned are sent to the webserver.</summary>
		public static List<long> GetChangedSinceDeletedObjectNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT DeletedObjectNum FROM deletedobject WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> deletedObjectnums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				deletedObjectnums.Add(PIn.Long(dt.Rows[i]["DeletedObjectNum"].ToString()));
			}
			return deletedObjectnums;
		}

		public static List<DeletedObject> GetMultDeletedObjects(List<long> deletedObjectNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DeletedObject>>(MethodBase.GetCurrentMethod(),deletedObjectNums);
			}
			string strDeletedObjectNums="";
			DataTable table;
			if(deletedObjectNums.Count>0) {
				for(int i=0;i<deletedObjectNums.Count;i++) {
					if(i>0) {
						strDeletedObjectNums+="OR ";
					}
					strDeletedObjectNums+="DeletedObjectNum='"+deletedObjectNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM deletedobject WHERE "+strDeletedObjectNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			DeletedObject[] multDeletedObjects=Crud.DeletedObjectCrud.TableToList(table).ToArray();
			List<DeletedObject> deletedObjectList=new List<DeletedObject>(multDeletedObjects);
			return deletedObjectList;
		}
		#endregion

		///<summary>This is only run at the server for the mobile db. Deleted patients are not handled here because patients never get deleted.</summary>
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
				if(list[i].ObjectType==DeletedObjectType.LabPanel) {
					Mobile.Crud.LabPanelmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.LabResult) {
					Mobile.Crud.LabResultmCrud.Delete(customerNum,list[i].ObjectNum);
				}
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
				if(list[i].ObjectType==DeletedObjectType.Statement) {
					Mobile.Crud.StatementmCrud.Delete(customerNum,list[i].ObjectNum);
				}
				if(list[i].ObjectType==DeletedObjectType.Document) {
					Mobile.Crud.DocumentmCrud.Delete(customerNum,list[i].ObjectNum);
				}
			}
		}


	}
}