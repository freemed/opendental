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

		public static List<Userod> ShortList {
			get {
				if(listt==null) {
					Userods.RefreshCache();
				}
				List<Userod> shortList=new List<Userod>();
				for(int i=0;i<listt.Count;i++) {
					if(!listt[i].IsHidden) {
						shortList.Add(listt[i]);
					}
				}
				return shortList;
			}
		}

		
	}
}
