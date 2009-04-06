using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProcedureCodeC {	
		private static List<ProcedureCode> list;
		private static Hashtable hList;

		public static List<ProcedureCode> Listt {
			get {
				if(list==null) {
					ProcedureCodes.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary>key:ProcCode, value:ProcedureCode</summary>
		public static Hashtable HList {
			get {
				if(hList==null) {
					ProcedureCodes.RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}

		
	}
}
