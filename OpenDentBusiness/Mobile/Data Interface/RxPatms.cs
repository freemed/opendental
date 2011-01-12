using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class RxPatms{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<RxPatm> Refresh(long patNum){
			string command="SELECT * FROM rxpatm WHERE PatNum = "+POut.Long(patNum);
			return Crud.RxPatmCrud.SelectMany(command);
		}

		///<summary>Gets one RxPatm from the db.</summary>
		public static RxPatm GetOne(long customerNum,long rxNum){
			return Crud.RxPatmCrud.SelectOne(customerNum,rxNum);
		}

		///<summary></summary>
		public static long Insert(RxPatm rxPatm){
			return Crud.RxPatmCrud.Insert(rxPatm,true);
		}

		///<summary></summary>
		public static void Update(RxPatm rxPatm){
			Crud.RxPatmCrud.Update(rxPatm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long rxNum) {
			string command= "DELETE FROM rxpatm WHERE CustomerNum = "+POut.Long(customerNum)+" AND RxNum = "+POut.Long(rxNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<RxPatm> ConvertListToM(List<RxPat> list) {
			List<RxPatm> retVal=new List<RxPatm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.RxPatmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<RxPatm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				RxPatm rxPatm=Crud.RxPatmCrud.SelectOne(customerNum,list[i].RxNum);
				if(rxPatm==null){//not in db
					Crud.RxPatmCrud.Insert(list[i],true);
				}
				else{
					Crud.RxPatmCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}