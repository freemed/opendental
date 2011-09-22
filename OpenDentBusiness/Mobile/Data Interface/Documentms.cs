using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class Documentms{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Documentm> Refresh(long patNum){
			string command="SELECT * FROM documentm WHERE PatNum = "+POut.Long(patNum);
			return Crud.DocumentmCrud.SelectMany(command);
		}

		///<summary>Gets one Documentm from the db.</summary>
		public static Documentm GetOne(long customerNum,long docNum){
			return Crud.DocumentmCrud.SelectOne(customerNum,docNum);
		}

		///<summary></summary>
		public static long Insert(Documentm documentm){
			return Crud.DocumentmCrud.Insert(documentm,true);
		}

		///<summary></summary>
		public static void Update(Documentm documentm){
			Crud.DocumentmCrud.Update(documentm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long docNum) {
			string command= "DELETE FROM documentm WHERE CustomerNum = "+POut.Long(customerNum)+" AND DocNum = "+POut.Long(docNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Documentm> ConvertListToM(List<Document> list) {
			List<Documentm> retVal=new List<Documentm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.DocumentmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Documentm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				Documentm documentm=Crud.DocumentmCrud.SelectOne(customerNum,list[i].DocNum);
				if(documentm==null){//not in db
					Crud.DocumentmCrud.Insert(list[i],true);
				}
				else{
					Crud.DocumentmCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}