using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class FormularyMeds{
		//Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary>Gets the formulary meds for a formulary.</summary>
		public static List<FormularyMed> GetMedsForFormulary(long formularyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetObject<List<FormularyMed>>(MethodBase.GetCurrentMethod(),formularyNum);
			}
			string command="SELECT * FROM formularymed WHERE FormularyNum = "+POut.Long(formularyNum);
			return Crud.FormularyMedCrud.SelectMany(command);
		}

		/*
		///<summary>Gets one FormularyMed from the db.</summary>
		public static FormularyMed GetOne(long formularyMedNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<FormularyMed>(MethodBase.GetCurrentMethod(),formularyMedNum);
			}
			return Crud.FormularyMedCrud.SelectOne(formularyMedNum);
		}

		///<summary></summary>
		public static long Insert(FormularyMed formularyMed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				formularyMed.FormularyMedNum=Meth.GetLong(MethodBase.GetCurrentMethod(),formularyMed);
				return formularyMed.FormularyMedNum;
			}
			return Crud.FormularyMedCrud.Insert(formularyMed);
		}

		///<summary></summary>
		public static void Update(FormularyMed formularyMed){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formularyMed);
				return;
			}
			Crud.FormularyMedCrud.Update(formularyMed);
		}

		///<summary></summary>
		public static void Delete(long formularyMedNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formularyMedNum);
				return;
			}
			string command= "DELETE FROM formularymed WHERE FormularyMedNum = "+POut.Long(formularyMedNum);
			Db.NonQ(command);
		}
		*/



	}
}