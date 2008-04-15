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
		Chart_GetAll,
		CovCats_RefreshCache
	}

	///<summary>MethodNames for retrieving tables as DataTables or as Xml tables.</summary>
	public enum MethodNameTable{
		Account_RefreshCache,
		AccountingAutoPay_RefreshCache,
		AppointmentRule_RefreshCache,
		ApptView_RefreshCache,
		ApptViewItem_RefreshCache,
		AutoCode_RefreshCache,
		AutoCodeCond_RefreshCache,
		AutoCodeItem_RefreshCache,
		ClaimFormItem_RefreshCache,
		CovSpan_RefreshCache,
		Definition_RefreshCache,
		GroupPermission_RefreshCache,
		MountDef_RefreshCache,
		Patient_GetPtDataTable,
		Prefs_RefreshCache,
		Providers_RefreshCache,
		Userod_RefreshCache
	}

	///<summary>MethodNames for retrieving strings.</summary>
	public enum MethodNameString{
		Userod_CheckDbUserPassword
	}

	///<summary>MethodNames for retrieving objects.</summary>
	public enum MethodNameObject{

	}

	///<summary>MethodNames for sending commands.</summary>
	public enum MethodNameCmd{
		
	}


}
