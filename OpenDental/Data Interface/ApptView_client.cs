using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class ApptView_client {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.ApptView_RefreshCache);
			ApptViews.FillCache(table);//now, we have an arrays on both the client and the server.
		}

		/*
		public static ApptView GetView(int apptViewNum){
			for(int i=0;i<ApptViewC.List.Length;i++){
				if(ApptViewC.List[i].ApptViewNum==apptViewNum){
					return ApptViewC.List[i];
				}
			}
			return null;//should never happen
		}*/

	}
}
