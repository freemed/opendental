using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class DrugUnitms{
		#region Only used for webserver for Patient Portal.
		///<summary>Gets one DrugUnitm from the db.</summary>
		public static DrugUnitm GetOne(long customerNum,long drugUnitNum) {
			return Crud.DrugUnitmCrud.SelectOne(customerNum,drugUnitNum);
		}
		#endregion

		#region Used only on OD
		///<summary>The values returned are sent to the webserver.</summary>
		public static List<long> GetChangedSinceDrugUnitNums(DateTime changedSince) {
			return DrugUnits.GetChangedSinceDrugUnitNums(changedSince);
		}

		///<summary>The values returned are sent to the webserver.</summary>
		public static List<DrugUnitm> GetMultDrugUnitms(List<long> drugUnitNums) {
			List<DrugUnit> DrugUnitList=DrugUnits.GetMultDrugUnits(drugUnitNums);
			List<DrugUnitm> DrugUnitmList=ConvertListToM(DrugUnitList);
			return DrugUnitmList;
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<DrugUnitm> ConvertListToM(List<DrugUnit> list) {
			List<DrugUnitm> retVal=new List<DrugUnitm>();
			for(int i=0;i<list.Count;i++) {
				retVal.Add(Crud.DrugUnitmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}
		#endregion

		#region Used only on the Mobile webservice server for  synching.
		public static void UpdateFromChangeList(List<DrugUnitm> list,long customerNum) {
			for(int i=0;i<list.Count;i++) {
				list[i].CustomerNum=customerNum;
				DrugUnitm drugUnitm=Crud.DrugUnitmCrud.SelectOne(customerNum,list[i].DrugUnitNum);
				if(drugUnitm==null) {//not in db
					Crud.DrugUnitmCrud.Insert(list[i],true);
				}
				else {
					Crud.DrugUnitmCrud.Update(list[i]);
				}
			}
		}

		///<summary>used in tandem with Full synch</summary>
		public static void DeleteAll(long customerNum) {
			string command= "DELETE FROM drugunitm WHERE CustomerNum = "+POut.Long(customerNum); ;
			Db.NonQ(command);
		}
		#endregion
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<DrugUnitm> Refresh(long patNum){
			string command="SELECT * FROM drugunitm WHERE PatNum = "+POut.Long(patNum);
			return Crud.DrugUnitmCrud.SelectMany(command);
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

		*/



	}
}