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
			DataTable table=Db.GetTable(command);
			List<DeletedObject> list=new List<DeletedObject>();
			DeletedObject delObj;
			for(int i=0;i<table.Rows.Count;i++) {
				delObj=new DeletedObject();
				delObj.DeletedObjectNum =PIn.Long(table.Rows[i][0].ToString());
				delObj.ObjectNum        =PIn.Long(table.Rows[i][1].ToString());
				delObj.ObjectType       =(DeletedObjectType)PIn.Long(table.Rows[i][2].ToString());
				//DateTStamp
				list.Add(delObj);
			}
			return list;
		}

		///<summary>This is only run at the server for the mobile db.  It currently handles deleted appointments.  Deleted patients are not handled here because patients never get deleted.</summary>
		public static void DeleteForMobile(List<DeletedObject> list,long customerNum) {
			for(int i=0;i<list.Count;i++) {
				if(list[i].ObjectType==DeletedObjectType.Appointment) {
					Mobile.Crud.AppointmentmCrud.Delete(customerNum,list[i].ObjectNum);
				}
			}
		}


	}
}