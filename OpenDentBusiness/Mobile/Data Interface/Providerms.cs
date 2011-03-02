using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class Providerms{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Providerm> Refresh(long patNum){
			string command="SELECT * FROM providerm WHERE PatNum = "+POut.Long(patNum);
			return Crud.ProvidermCrud.SelectMany(command);
		}

		///<summary>Gets one Providerm from the db.</summary>
		public static Providerm GetOne(long customerNum,long provNum){
			return Crud.ProvidermCrud.SelectOne(customerNum,provNum);
		}

		///<summary></summary>
		public static long Insert(Providerm providerm){
			return Crud.ProvidermCrud.Insert(providerm,true);
		}

		///<summary></summary>
		public static void Update(Providerm providerm){
			Crud.ProvidermCrud.Update(providerm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long provNum) {
			string command= "DELETE FROM providerm WHERE CustomerNum = "+POut.Long(customerNum)+" AND ProvNum = "+POut.Long(provNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Providerm> ConvertListToM(List<Provider> list) {
			List<Providerm> retVal=new List<Providerm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.ProvidermCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Providerm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				Providerm providerm=Crud.ProvidermCrud.SelectOne(customerNum,list[i].ProvNum);
				if(providerm==null){//not in db
					Crud.ProvidermCrud.Insert(list[i],true);
				}
				else{
					Crud.ProvidermCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}