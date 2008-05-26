using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class UserodC {
		///<summary>A list of all users.</summary>
		private static List<Userod> listt;

		public static List<Userod> Listt{
			get{
				if(listt==null){
					Userods.RefreshCache();
				}
				return listt;
			}
			set{
				listt=value;
			}
		}

		
	}
}
