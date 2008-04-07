using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	///<summary>MethodNames for retrieving datasets.</summary>
	public enum MethodNameDS{
		AccountModule_GetAll,
		AccountModule_GetPayPlanAmort,
		AccountModule_GetStatement,
		Appointment_GetApptEdit,
		Appointment_RefreshPeriod,
		Appointment_RefreshOneApt,
		Chart_GetAll
	}

	///<summary>MethodNames for retrieving tables.</summary>
	public enum MethodNameTable{
		Account_RefreshCache,
		Definition_RefreshCache,
		MountDef_RefreshCache,
		Prefs_RefreshCache,
		Providers_RefreshCache
	}
}
