using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Guardians{

		///<summary>Get all guardians for a one dependant/child.</summary>
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
		public static void Update(Guardian guardian){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),guardian);
				return;
			}
			Crud.GuardianCrud.Update(guardian);
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
		public static void DeleteForFamily(long patNumGuar) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNumGuar);
				return;
			}
			string command="DELETE FROM guardian "
				+"WHERE PatNumChild IN (SELECT p.PatNum FROM patient p WHERE p.Guarantor="+POut.Long(patNumGuar)+")";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static bool ExistForFamily(long patNumGuar) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),patNumGuar);
			}
			string command="SELECT COUNT(*) FROM guardian "
				+"WHERE PatNumChild IN (SELECT p.PatNum FROM patient p WHERE p.Guarantor="+POut.Long(patNumGuar)+")";
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		/// <summary>Short abbreviation of relationship within parentheses.</summary>
		public static string GetGuardianRelationshipStr(GuardianRelationship relat) {
			//No need to check RemotingRole; no call to db.
			switch(relat) {
				case GuardianRelationship.Brother: return "(br)";
				case GuardianRelationship.CareGiver: return "(cg)";
				case GuardianRelationship.Child: return "(c)";
				case GuardianRelationship.Father: return "(d)";
				case GuardianRelationship.FosterChild: return "(fc)";
				case GuardianRelationship.Friend: return "(f)";
				case GuardianRelationship.Grandchild: return "(gc)";
				case GuardianRelationship.Grandfather: return "(gf)";
				case GuardianRelationship.Grandmother: return "(gm)";
				case GuardianRelationship.Grandparent: return "(gp)";
				case GuardianRelationship.Guardian: return "(g)";
				case GuardianRelationship.LifePartner: return "(lp)";
				case GuardianRelationship.Mother: return "(m)";
				case GuardianRelationship.Other: return "(o)";
				case GuardianRelationship.Parent: return "(p)";
				case GuardianRelationship.Self: return "(se)";
				case GuardianRelationship.Sibling: return "(sb)";
				case GuardianRelationship.Sister: return "(ss)";
				case GuardianRelationship.Sitter: return "(s)";
				case GuardianRelationship.Spouse: return "(sp)";
				case GuardianRelationship.Stepchild: return "(sc)";
				case GuardianRelationship.Stepfather: return "(sf)";
				case GuardianRelationship.Stepmother: return "(sm)";
			}
			return "";
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

		

		
		*/



	}
}