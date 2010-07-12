using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DependantRelats{

		///<summary>Get all dependant relationships for a particular dependant/patient.</summary>
		public static List<DependantRelat> Refresh(long patNumChild){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DependantRelat>>(MethodBase.GetCurrentMethod(),patNumChild);
			}
			string command="SELECT * FROM dependantrelat WHERE PatNumChild = "+POut.Long(patNumChild)+" ORDER BY Relationship";
			return Crud.DependantRelatCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Delete(long dependantRelatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dependantRelatNum);
				return;
			}
			string command= "DELETE FROM dependantrelat WHERE DependantRelatNum = "+POut.Long(dependantRelatNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets one DependantRelat from the db.</summary>
		public static DependantRelat GetOne(long dependantRelatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DependantRelat>(MethodBase.GetCurrentMethod(),dependantRelatNum);
			}
			return Crud.DependantRelatCrud.SelectOne(dependantRelatNum);
		}

		///<summary></summary>
		public static long Insert(DependantRelat dependantRelat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				dependantRelat.DependantRelatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),dependantRelat);
				return dependantRelat.DependantRelatNum;
			}
			return Crud.DependantRelatCrud.Insert(dependantRelat);
		}

		///<summary></summary>
		public static void Update(DependantRelat dependantRelat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dependantRelat);
				return;
			}
			Crud.DependantRelatCrud.Update(dependantRelat);
		}

		
		*/



	}
}