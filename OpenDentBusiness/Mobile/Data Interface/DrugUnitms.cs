using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class DrugUnitms{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<DrugUnitm> Refresh(long patNum){
			string command="SELECT * FROM drugunitm WHERE PatNum = "+POut.Long(patNum);
			return Crud.DrugUnitmCrud.SelectMany(command);
		}

		///<summary>Gets one DrugUnitm from the db.</summary>
		public static DrugUnitm GetOne(long customerNum,long drugUnitNum){
			return Crud.DrugUnitmCrud.SelectOne(customerNum,drugUnitNum);
		}

		///<summary></summary>
		public static long Insert(DrugUnitm drugUnitm){
			return Crud.DrugUnitmCrud.Insert(drugUnitm,true);
		}

		///<summary></summary>
		public static void Update(DrugUnitm drugUnitm){
			Crud.DrugUnitmCrud.Update(drugUnitm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long drugUnitNum) {
			string command= "DELETE FROM drugunitm WHERE CustomerNum = "+POut.Long(customerNum)+" AND DrugUnitNum = "+POut.Long(drugUnitNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<DrugUnitm> ConvertListToM(List<DrugUnit> list) {
			List<DrugUnitm> retVal=new List<DrugUnitm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.DrugUnitmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<DrugUnitm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				DrugUnitm drugUnitm=Crud.DrugUnitmCrud.SelectOne(customerNum,list[i].DrugUnitNum);
				if(drugUnitm==null){//not in db
					Crud.DrugUnitmCrud.Insert(list[i],true);
				}
				else{
					Crud.DrugUnitmCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}