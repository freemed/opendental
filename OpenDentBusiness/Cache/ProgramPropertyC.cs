using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProgramPropertyC {
		private static List<ProgramProperty> listt;

		///<summary>A list of all program (link) properties.</summary>
		public static List<ProgramProperty> Listt {
			get {
				if(listt==null) {
					ProgramProperties.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}
		
	}
}
