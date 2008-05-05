using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class Operatory_client {
		
		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(int opNum){
			for(int i=0;i<OperatoryC.ListShort.Count;i++){
				if(OperatoryC.ListShort[i].OperatoryNum==opNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets the abbreviation of an op.</summary>
		public static string GetAbbrev(int opNum){
			for(int i=0;i<OperatoryC.Listt.Count;i++){
				if(OperatoryC.Listt[i].OperatoryNum==opNum){
					return OperatoryC.Listt[i].Abbrev;
				}
			}
			return "";
		}

		///<summary></summary>
		public static Operatory GetOperatory(int operatoryNum){
			for(int i=0;i<OperatoryC.Listt.Count;i++){
				if(OperatoryC.Listt[i].OperatoryNum==operatoryNum){
					return OperatoryC.Listt[i].Copy();
				}
			}
			return null;
		}

	}
}
