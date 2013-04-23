using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Resellers {

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

		///<summary>Gets a list of resellers and their patient information based on the criteria passed in.  Only used from FormResellers to fill the grid.</summary>
		public static DataTable GetResellerList(string lname,string fname,string phone,string address,string city,string state,string patNum,string email,bool showInactive) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),lname,fname,phone,address,city,state,patNum,email,showInactive);
			}
			string phonedigits="";
			for(int i=0;i<phone.Length;i++) {
				if(Regex.IsMatch(phone[i].ToString(),"[0-9]")) {
					phonedigits=phonedigits+phone[i];
				}
			}
			string regexp="";
			for(int i=0;i<phonedigits.Length;i++) {
				if(i!=0) {
					regexp+="[^0-9]*";//zero or more intervening digits that are not numbers
				}
				regexp+=phonedigits[i];
			}
			string command="SELECT ResellerNum,patient.PatNum,LName,FName,Preferred,HmPhone,WkPhone,WirelessPhone,PhoneNumberVal,Address,City,State,Email,PatStatus "
				+"FROM reseller "
				+"LEFT JOIN patient ON reseller.PatNum=patient.PatNum "
				+"LEFT JOIN phonenumber ON phonenumber.PatNum=patient.PatNum ";
			if(regexp!="") {
				command+="AND phonenumber.PhoneNumberVal REGEXP '"+POut.String(regexp)+"' ";
			}
			command+="WHERE PatStatus!='"+POut.Int((int)PatientStatus.Deleted)+"' ";
			if(!showInactive) {
				command+="AND PatStatus!='"+POut.Int((int)PatientStatus.Inactive)+"' ";
			}
			command+="AND (LName LIKE '"+POut.String(lname)+"%' OR Preferred LIKE '"+POut.String(lname)+"%') "
				+"AND (FName LIKE '"+POut.String(fname)+"%' OR Preferred LIKE '"+POut.String(fname)+"%') ";
			if(regexp!="") {
				command+="AND (HmPhone REGEXP '"+POut.String(regexp)+"' "
					+"OR WkPhone REGEXP '"+POut.String(regexp)+"' "
					+"OR WirelessPhone REGEXP '"+POut.String(regexp)+"' "
					+"OR phonenumber.PhoneNumberVal REGEXP '"+POut.String(regexp)+"') ";
			}
			command+=(address.Length>0?"AND Address LIKE '%"+POut.String(address)+"%' ":"")
					+(city.Length>0?"AND City LIKE '"+POut.String(city)+"%' ":"")
					+(state.Length>0?"AND State LIKE '"+POut.String(state)+"%' ":"")
					+(patNum.Length>0?"AND patient.PatNum LIKE '"+POut.String(patNum)+"%' ":"")
					+(email.Length>0?"AND Email LIKE '%"+POut.String(email)+"%' ":"");
			return Db.GetTable(command);
		}

		///<summary>Gets all of the customers of the reseller (family members) that have active services.  Only used from FormResellerEdit to fill the grid.</summary>
		public static DataTable GetResellerCustomersList(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			//TODO: Enhance to get all the customers attached to this reseller and their services.
			string command="SELECT patient.PatNum,RegKey,procedurecode.ProcCode,procedurecode.Descript,resellerservice.Fee,repeatcharge.DateStart,repeatcharge.DateStop,repeatcharge.Note "
				+"FROM patient "
				+"LEFT JOIN registrationkey ON patient.PatNum=registrationkey.PatNum AND IsResellerCustomer=1 "
				+"LEFT JOIN repeatcharge ON patient.PatNum=repeatcharge.PatNum "
				+"LEFT JOIN procedurecode ON repeatcharge.ProcCode=procedurecode.ProcCode "
				+"LEFT JOIN reseller ON patient.Guarantor=reseller.PatNum "
				+"LEFT JOIN resellerservice ON reseller.ResellerNum=resellerservice.resellerNum AND resellerservice.CodeNum=procedurecode.CodeNum "
				+"WHERE patient.PatNum!='"+POut.Long(patNum)+"' "
				+"AND patient.Guarantor='"+POut.Long(patNum)+"' ";
			return Db.GetTable(command);
		}

		///<summary>Checks the database to see if the user name is already in use.</summary>
		public static bool IsUserNameInUse(long patNum,string userName) {
			string command="SELECT COUNT(*) FROM reseller WHERE PatNum!="+POut.Long(patNum)+" AND UserName='"+POut.String(userName)+"'";
			if(PIn.Int(Db.GetCount(command))>0) {
				return true;//User name in use.
			}
			return false;
		}



	}
}