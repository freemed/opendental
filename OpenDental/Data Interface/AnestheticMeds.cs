using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.DataAccess;

namespace OpenDental{
	///<summary></summary>
	public class AnestheticMeds {

		///<summary>Gets all AnestheticMedsGiven.</summary>
		/*public static List<AnestheticMedsGiven> CreateObjects() {
			string command="SELECT * FROM anestheticMedsGiven ORDER BY AnesthMed";
			return new List<AnestheticMedsGiven>(DataObjectFactory<AnestheticMedsGiven>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(AnestheticMedsGiven med){
			DataObjectFactory<AnestheticMedsGiven>.WriteObject(med);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(AnestheticMedsGiven med){
			//validate that not already in use.
			string command="SELECT COUNT(*) FROM anesthmedsgiven WHERE AnestheticMedsGivenNum="+POut.PInt(med.AnestheticMedsGivenNum);
			int count=PIn.PInt(General.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lan.g("Supplies","AnestheticMedsGiven is already in use on an order. Not allowed to delete."));
			}
			command="SELECT COUNT(*) FROM supply WHERE AnestheticMedsGivenNum="+POut.PInt(med.AnestheticMedsGivenNum);
			count=PIn.PInt(General.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lan.g("Supplies","AnestheticMedsGiven is already in use on a supply. Not allowed to delete."));
			}
			DataObjectFactory<AnestheticMedsGiven>.DeleteObject(med);
		}

		public static string GetName(List<AnestheticMedsGiven> listAnestheticMedsGiven,int anestheticMedsGivenNum){
			for(int i=0;i<listAnestheticMedsGiven.Count;i++){
				if(listAnestheticMedsGiven[i].AnestheticMedsGivenNum==anestheticMedsGivenNum){
					return listAnestheticMedsGiven[i].AnesthMed;
				}
			}
			return "";
		}*/


	}

	


	


}









