using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EmailAddresses{
		#region CachePattern
		///<summary>A list of all EmailAddresses.</summary>
		private static List<EmailAddress> listt;

		///<summary>A list of all EmailAddresses.</summary>
		public static List<EmailAddress> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM emailaddress";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EmailAddress";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EmailAddressCrud.TableToList(table);
		}
		#endregion

		///<summary>Gets the default email address for the clinic/practice. Takes a clinic num. If clinic num is 0 or there is no default for that clinic, it will get practice default. May return a new blank object.</summary>
		public static EmailAddress GetByClinic(long clinicNum) {
			EmailAddress emailAddress=null;
			Clinic clinic=Clinics.GetClinic(clinicNum);
			if(PrefC.GetBool(PrefName.EasyNoClinics) || clinic==null) {//No clinic, get practice default
				emailAddress=GetOne(PrefC.GetLong(PrefName.EmailDefaultAddressNum));
			}
			else {
				emailAddress=GetOne(clinic.EmailAddressNum);
				if(emailAddress==null) {//clinic.EmailAddressNum 0. Use default.
					emailAddress=GetOne(PrefC.GetLong(PrefName.EmailDefaultAddressNum));
				}
			}
			if(emailAddress==null) {
				if(listt.Count>0) {//user didn't set a default
					emailAddress=listt[0];
				}
				else {
					emailAddress=new EmailAddress();//To avoid null checks.
				}
			}
			return emailAddress;
		}

		///<summary>Gets one EmailAddress from the cached listt.  Might be null.</summary>
		public static EmailAddress GetOne(long emailAddressNum){
			//No need to check RemoteRole; Calls GetTableRemotelyIfNeeded().
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmailAddressNum==emailAddressNum) {
					return Listt[i];
				}
			}
			return null;
		}

		///<summary>Checks to make sure at least one email address has a valid (not blank) SMTP server.</summary>
		public static bool ExistsValidEmail() {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].SMTPserver!="") {
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static long Insert(EmailAddress emailAddress) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				emailAddress.EmailAddressNum=Meth.GetLong(MethodBase.GetCurrentMethod(),emailAddress);
				return emailAddress.EmailAddressNum;
			}
			return Crud.EmailAddressCrud.Insert(emailAddress);
		}

		///<summary></summary>
		public static void Update(EmailAddress emailAddress){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailAddress);
				return;
			}
			Crud.EmailAddressCrud.Update(emailAddress);
		}

		///<summary></summary>
		public static void Delete(long emailAddressNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailAddressNum);
				return;
			}
			string command= "DELETE FROM emailaddress WHERE EmailAddressNum = "+POut.Long(emailAddressNum);
			Db.NonQ(command);
		}



	}
}