using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Resellers {

		///<summary>Gets a list of resellers and their patient information based on the criteria passed in.  Only used from FormResellers to fill the grid.</summary>
		public static DataTable GetResellerList(string lName,string fName,string phone,string address,string city,string state,string patNum,string email) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),lName,fName,phone,address,city,state,patNum,email);
			}
			string command="SELECT * FROM reseller WHERE ";
			//TODO: Enhance where clause to include the search filters.
			return Db.GetTable(command);
		}

		///<summary>Gets all of the customers of the reseller.  Only used from FormResellerEdit to fill the grid.</summary>
		public static DataTable GetResellerCustomersList() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM reseller WHERE ";//TODO: Enhance to get all the customers attached to this reseller and their services.
			return Db.GetTable(command);
		}

		///<summary>Gets one Reseller from the db.</summary>
		public static Reseller GetOne(long resellerNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Reseller>(MethodBase.GetCurrentMethod(),resellerNum);
			}
			return Crud.ResellerCrud.SelectOne(resellerNum);
		}

		///<summary></summary>
		public static long Insert(Reseller reseller) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				reseller.ResellerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),reseller);
				return reseller.ResellerNum;
			}
			return Crud.ResellerCrud.Insert(reseller);
		}

		///<summary></summary>
		public static void Update(Reseller reseller) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reseller);
				return;
			}
			Crud.ResellerCrud.Update(reseller);
		}

		///<summary>Make sure to check that the reseller does not have any customers before deleting them.</summary>
		public static void Delete(long resellerNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),resellerNum);
				return;
			}
			string command= "DELETE FROM reseller WHERE ResellerNum = "+POut.Long(resellerNum);
			Db.NonQ(command);
		}



	}
}