using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Guardians{

		///<summary>Get all dependant relationships for a particular dependant/patient.</summary>
		public static List<Guardian> Refresh(long patNumChild){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Guardian>>(MethodBase.GetCurrentMethod(),patNumChild);
			}
			string command="SELECT * FROM guardian WHERE PatNumChild = "+POut.Long(patNumChild)+" ORDER BY Relationship";
			return Crud.GuardianCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(Guardian guardian){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				guardian.GuardianNum=Meth.GetLong(MethodBase.GetCurrentMethod(),guardian);
				return guardian.GuardianNum;
			}
			return Crud.GuardianCrud.Insert(guardian);
		}

		///<summary></summary>
		public static void Delete(long guardianNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),guardianNum);
				return;
			}
			Crud.GuardianCrud.Delete(guardianNum);
		}

		///<summary></summary>
		public static void DeleteForFamily(long PatNumGuar) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),PatNumGuar);
				return;
			}
			string command="DELETE FROM guardian "
				+"WHERE PatNumGuardian IN (SELECT p.PatNum FROM patient p WHERE p.Guarantor="+POut.Long(PatNumGuar)+")";
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets one Guardian from the db.</summary>
		public static Guardian GetOne(long guardianNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Guardian>(MethodBase.GetCurrentMethod(),guardianNum);
			}
			return Crud.GuardianCrud.SelectOne(guardianNum);
		}

		///<summary></summary>
		public static void Update(Guardian guardian){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),guardian);
				return;
			}
			Crud.GuardianCrud.Update(guardian);
		}

		
		*/



	}
}