using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>The name of this class is still up in the air.  Not sure how any of this will work until we actually build the web service.</summary>
	class ClientSL {
		
		public static object CallWebService(MethodName methodName,params object[] parameters){
			//must test to see if a silverlight client first
			if(RemotingClient.RemotingRole!=RemotingRole.ClientWeb){
				throw new ApplicationException("Must be a SilverLight client to call this method.");
			}
			switch(methodName){
				case MethodName.AccountModule_GetAll:
					return null;//call the service.  On the server, it will NOT be a silverlight client, so it will return false above.
			}
			throw new System.NotImplementedException();
		}
	}

	//This will soon be moved to its own file.
	public enum MethodName{
		AccountModule_GetAll,
		AccountModule_GetPayPlanAmort,
		AccountModule_GetStatement,
		Appointment_GetApptEdit,
		Appointment_RefreshPeriod,
		Appointment_RefreshOneApt,
		Chart_GetAll,
		Definition_Refresh,
		Providers_RefreshOnServer
	}
}
