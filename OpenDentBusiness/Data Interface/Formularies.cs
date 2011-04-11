using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Formularies{
		
		///<summary></summary>
		public static List<Formulary> GetAllFormularies(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetObject<List<Formulary>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM formulary ORDER BY Description";
			return Crud.FormularyCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(Formulary formulary){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				formulary.FormularyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),formulary);
				return formulary.FormularyNum;
			}
			return Crud.FormularyCrud.Insert(formulary);
		}

		///<summary></summary>
		public static void Update(Formulary formulary){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formulary);
				return;
			}
			Crud.FormularyCrud.Update(formulary);
		}

		//Only pull out the methods below as you need them.  Otherwise, leave them commented out.
		/*
		///<summary>Gets one Formulary from the db.</summary>
		public static Formulary GetOne(long formularyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Formulary>(MethodBase.GetCurrentMethod(),formularyNum);
			}
			return Crud.FormularyCrud.SelectOne(formularyNum);
		}

		///<summary></summary>
		public static void Delete(long formularyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formularyNum);
				return;
			}
			string command= "DELETE FROM formulary WHERE FormularyNum = "+POut.Long(formularyNum);
			Db.NonQ(command);
		}
		*/



	}
}