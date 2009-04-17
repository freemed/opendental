using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PhoneNumbers{
		/*//<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * from phoneNumber ORDER BY Description";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),c);
			table.TableName="phoneNumber";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			phoneNumberC.List=new phoneNumber[table.Rows.Count];
			for(int i=0;i<phoneNumberC.List.Length;i++){
				phoneNumberC.List[i]=new phoneNumber();
				phoneNumberC.List[i].IsNew=false;
				phoneNumberC.List[i].phoneNumberNum    = PIn.PInt   (table.Rows[i][0].ToString());
				phoneNumberC.List[i].Description= PIn.PString(table.Rows[i][1].ToString());
				phoneNumberC.List[i].Note       = PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<Summary>Gets one phoneNumber from the database.</Summary>
		public static phoneNumber CreateObject(int phoneNumberNum){
			return DataObjectFactory<phoneNumber>.CreateObject(phoneNumberNum);
		}*/

		public static List<PhoneNumber> GetPhoneNumbers(int patNum){
			string command="SELECT * FROM phonenumber WHERE PatNum="+POut.PInt(patNum);
			Collection<PhoneNumber> collectState=DataObjectFactory<PhoneNumber>.CreateObjects(command);
			return new List<PhoneNumber>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(PhoneNumber phoneNumber){
			DataObjectFactory<PhoneNumber>.WriteObject(phoneNumber);
		}

		/*//<summary></summary>
		public static void DeleteObject(int phoneNumberNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE phoneNumberNum="+POut.PInt(phoneNumberNum);
			DataTable table=General.GetTable(command);
			//int count=PIn.PInt(General.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("phoneNumbers","phoneNumber is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<phoneNumber>.DeleteObject(phoneNumberNum);
		}*/

		public static void DeleteObject(int phoneNumberNum){
			DataObjectFactory<PhoneNumber>.DeleteObject(phoneNumberNum);
		}

		/*public static string GetDescription(int phoneNumberNum){
			if(phoneNumberNum==0){
				return "";
			}
			for(int i=0;i<phoneNumberC.List.Length;i++){
				if(phoneNumberC.List[i].phoneNumberNum==phoneNumberNum){
					return phoneNumberC.List[i].Description;
				}
			}
			return "";
		}*/

	}
}