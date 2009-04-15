using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	//Both enums below are STUBS.  Remove each enum as it is rewritten.
	
	///<summary>MethodNames for retrieving datasets.</summary>
	public enum MethodNameDS{
		AccountModule_GetAll,
		AccountModule_GetPayPlanAmort,
		AccountModule_GetStatement,
		Appointment_GetApptEdit,
		Appointment_RefreshPeriod,
		Appointment_RefreshOneApt,
		Cache_Refresh,
		Chart_GetAll,
		CovCats_RefreshCache,
		FamilyModule_GetAll
	}

	/*
	///<summary>MethodNames for retrieving strings.</summary>
	public enum MethodNameString{
		Userod_CheckDbUserPassword
	}

	///<summary>MethodNames for retrieving objects.</summary>
	public enum MethodNameObject{
		Patient_GetFamily,
		Patient_GetPat
	}

	///<summary>MethodNames for sending commands.</summary>
	public enum MethodNameCmd{
		
	}

	*/
}
