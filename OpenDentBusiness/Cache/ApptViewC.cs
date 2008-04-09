using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ApptViewC {
		///<summary>A list of all apptviews, in order.</summary>
		public static ApptView[] List;

		public static ApptView GetView(int apptViewNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ApptViewNum==apptViewNum){
					return List[i];
				}
			}
			return null;//should never happen
		}
	}
}
