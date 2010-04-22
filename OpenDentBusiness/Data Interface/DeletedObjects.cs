using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

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

		public static List<DeletedObject> GetUAppoint(DateTime changedSince){
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


	}
}