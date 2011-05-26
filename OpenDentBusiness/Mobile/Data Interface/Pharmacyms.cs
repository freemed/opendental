using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class Pharmacyms{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Pharmacym> Refresh(long patNum){
			string command="SELECT * FROM pharmacym WHERE PatNum = "+POut.Long(patNum);
			return Crud.PharmacymCrud.SelectMany(command);
		}

		///<summary>Gets one Pharmacym from the db.</summary>
		public static Pharmacym GetOne(long customerNum,long pharmacyNum){
			return Crud.PharmacymCrud.SelectOne(customerNum,pharmacyNum);
		}

		///<summary></summary>
		public static long Insert(Pharmacym pharmacym){
			return Crud.PharmacymCrud.Insert(pharmacym,true);
		}

		///<summary></summary>
		public static void Update(Pharmacym pharmacym){
			Crud.PharmacymCrud.Update(pharmacym);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long pharmacyNum) {
			string command= "DELETE FROM pharmacym WHERE CustomerNum = "+POut.Long(customerNum)+" AND PharmacyNum = "+POut.Long(pharmacyNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Pharmacym> ConvertListToM(List<Pharmacy> list) {
			List<Pharmacym> retVal=new List<Pharmacym>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.PharmacymCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Pharmacym> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				Pharmacym pharmacym=Crud.PharmacymCrud.SelectOne(customerNum,list[i].PharmacyNum);
				if(pharmacym==null){//not in db
					Crud.PharmacymCrud.Insert(list[i],true);
				}
				else{
					Crud.PharmacymCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}