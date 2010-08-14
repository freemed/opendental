using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PhoneNumbers{

		public static List<PhoneNumber> GetPhoneNumbers(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PhoneNumber>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM phonenumber WHERE PatNum="+POut.Long(patNum);
			return Crud.PhoneNumberCrud.SelectMany(command);
		}

		public static PhoneNumber GetByVal(string phoneNumberVal) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PhoneNumber>(MethodBase.GetCurrentMethod(),phoneNumberVal);
			}
			string command="SELECT * FROM phonenumber WHERE PhoneNumberVal='"+POut.String(phoneNumberVal)+"'";
			return Crud.PhoneNumberCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(PhoneNumber phoneNumber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				phoneNumber.PhoneNumberNum=Meth.GetLong(MethodBase.GetCurrentMethod(),phoneNumber);
				return phoneNumber.PhoneNumberNum;
			}
			return Crud.PhoneNumberCrud.Insert(phoneNumber);
		}

		///<summary></summary>
		public static void Update(PhoneNumber phoneNumber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),phoneNumber);
				return;
			}
			Crud.PhoneNumberCrud.Update(phoneNumber);
		}

		public static void DeleteObject(long phoneNumberNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),phoneNumberNum);
				return;
			}
			Crud.PhoneNumberCrud.Delete(phoneNumberNum);
		}


	}
}