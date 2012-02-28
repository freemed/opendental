using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CustReferences{

		///<summary>Gets one CustReference from the db.</summary>
		public static CustReference GetOne(long custReferenceNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CustReference>(MethodBase.GetCurrentMethod(),custReferenceNum);
			}
			return Crud.CustReferenceCrud.SelectOne(custReferenceNum);
		}

		///<summary></summary>
		public static long Insert(CustReference custReference){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				custReference.CustReferenceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),custReference);
				return custReference.CustReferenceNum;
			}
			return Crud.CustReferenceCrud.Insert(custReference);
		}

		///<summary></summary>
		public static void Update(CustReference custReference){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custReference);
				return;
			}
			Crud.CustReferenceCrud.Update(custReference);
		}

		///<summary>Might not be used.  Might implement when a patient is deleted but doesn't happen often if ever.</summary>
		public static void Delete(long custReferenceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custReferenceNum);
				return;
			}
			string command= "DELETE FROM custreference WHERE CustReferenceNum = "+POut.Long(custReferenceNum);
			Db.NonQ(command);
		}



	}
}