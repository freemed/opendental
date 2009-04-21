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
		
		/*
		///<Summary>Gets one DeletedObject from the database.</Summary>
		public static DeletedObject CreateObject(int DeletedObjectNum){
			return DataObjectFactory<DeletedObject>.CreateObject(DeletedObjectNum);
		}

		public static List<DeletedObject> GetDeletedObjects(int[] DeletedObjectNums){
			Collection<DeletedObject> collectState=DataObjectFactory<DeletedObject>.CreateObjects(DeletedObjectNums);
			return new List<DeletedObject>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(DeletedObject deletedObject){
			DataObjectFactory<DeletedObject>.WriteObject(deletedObject);
		}*/

		///<summary></summary>
		public static void SetDeleted(DeletedObjectType objType,int objectNum){
			DeletedObject delObj=new DeletedObject();
			delObj.IsNew=true;
			delObj.ObjectNum=objectNum;
			delObj.ObjectType=objType;
			DataObjectFactory<DeletedObject>.WriteObject(delObj);
		}

		public static List<DeletedObject> GetUAppoint(DateTime changedSince){
			string command="SELECT * FROM deletedobject WHERE DateTStamp > "+POut.PDateT(changedSince);
			DataTable table=Db.GetTable(command);
			List<DeletedObject> list=new List<DeletedObject>();
			DeletedObject delObj;
			for(int i=0;i<table.Rows.Count;i++) {
				delObj=new DeletedObject();
				delObj.DeletedObjectNum =PIn.PInt(table.Rows[i][0].ToString());
				delObj.ObjectNum        =PIn.PInt(table.Rows[i][1].ToString());
				delObj.ObjectType       =(DeletedObjectType)PIn.PInt(table.Rows[i][2].ToString());
				//DateTStamp
				list.Add(delObj);
			}
			return list;
		}


	}
}