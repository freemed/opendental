using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile{
	///<summary></summary>
	public class Statementms{
		
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Statementm> Refresh(long patNum){
			string command="SELECT * FROM statementm WHERE PatNum = "+POut.Long(patNum);
			return Crud.StatementmCrud.SelectMany(command);
		}

		///<summary>Gets one Statementm from the db.</summary>
		public static Statementm GetOne(long customerNum,long statementNum){
			return Crud.StatementmCrud.SelectOne(customerNum,statementNum);
		}

		///<summary></summary>
		public static long Insert(Statementm statementm){
			return Crud.StatementmCrud.Insert(statementm,true);
		}

		///<summary></summary>
		public static void Update(Statementm statementm){
			Crud.StatementmCrud.Update(statementm);
		}

		///<summary></summary>
		public static void Delete(long customerNum,long statementNum) {
			string command= "DELETE FROM statementm WHERE CustomerNum = "+POut.Long(customerNum)+" AND StatementNum = "+POut.Long(statementNum);
			Db.NonQ(command);
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Statementm> ConvertListToM(List<Statement> list) {
			List<Statementm> retVal=new List<Statementm>();
			for(int i=0;i<list.Count;i++){
				retVal.Add(Crud.StatementmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}

		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Statementm> list,long customerNum) {
			for(int i=0;i<list.Count;i++){
				list[i].CustomerNum=customerNum;
				Statementm statementm=Crud.StatementmCrud.SelectOne(customerNum,list[i].StatementNum);
				if(statementm==null){//not in db
					Crud.StatementmCrud.Insert(list[i],true);
				}
				else{
					Crud.StatementmCrud.Update(list[i]);
				}
			}
		}
		*/



	}
}