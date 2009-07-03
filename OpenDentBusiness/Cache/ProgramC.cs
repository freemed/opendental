using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProgramC {
		///<summary></summary>
		private static Hashtable hList;
		///<summary>A list of all Program links.</summary>
		private static List<Program> listt;

		public static Hashtable HList{
			get {
				if(hList==null) {
					Programs.RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}

		public static List<Program> Listt {
			get {
				if(listt==null) {
					Programs.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		public static bool HListIsNull(){
			return hList==null;
		}
		
	}
}
