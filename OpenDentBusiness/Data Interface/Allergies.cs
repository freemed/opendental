using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Allergies{
		///<summary></summary>
		public static List<Allergy> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Allergy>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM allergy WHERE PatNum = "+POut.Long(patNum);
			return Crud.AllergyCrud.SelectMany(command);
		}

		///<summary>Gets one Allergy from the db.</summary>
		public static Allergy GetOne(long allergyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Allergy>(MethodBase.GetCurrentMethod(),allergyNum);
			}
			return Crud.AllergyCrud.SelectOne(allergyNum);
		}

		///<summary></summary>
		public static long Insert(Allergy allergy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				allergy.AllergyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),allergy);
				return allergy.AllergyNum;
			}
			return Crud.AllergyCrud.Insert(allergy);
		}

		///<summary></summary>
		public static void Update(Allergy allergy){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergy);
				return;
			}
			Crud.AllergyCrud.Update(allergy);
		}

		///<summary></summary>
		public static void Delete(long allergyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),allergyNum);
				return;
			}
			string command= "DELETE FROM allergy WHERE AllergyNum = "+POut.Long(allergyNum);
			Db.NonQ(command);
		}



	}
}