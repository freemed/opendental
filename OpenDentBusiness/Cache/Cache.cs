using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class Cache {
		/// <summary>This is only used in the RefreshCache methods.  Used instead of Meth.  The command is only used if not ClientWeb.</summary>
		public static DataTable GetTableRemotelyIfNeeded(MethodBase methodBase,string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(methodBase);
			}
			else {
				return Db.GetTable(command);
			}
		}

		///<summary>This is only called from the UI.  Its purpose is to refresh the cache for one type on both the workstation and server.</summary>
		public static void Refresh(InvalidType itype) {
			int intItype=(int)itype;
			RefreshCache(intItype.ToString());
		}
			 
		///<summary>itypesStr= comma-delimited list of int.  Called directly from UI in one spot.  Called from above repeatedly.  The end result is that both server and client have been properly refreshed.</summary>
		public static void RefreshCache(string itypesStr){
			DataSet ds=GetCacheDs(itypesStr);
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				//Because otherwise it was handled just fine as part of the GetCacheDs
				FillCache(ds,itypesStr);
			}
		}

		///<summary>If ClientWeb, then this method is instead run on the server, and the result passed back to the client.  And since it's ClientWeb, FillCache will be run on the client.</summary>
		public static DataSet GetCacheDs(string itypesStr){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetDS(MethodBase.GetCurrentMethod(),itypesStr);
			}
			//so this part below only happens if direct or server------------------------------------------------
			List<int> itypes=new List<int>();
			string[] strArray=itypesStr.Split(',');
			for(int i=0;i<strArray.Length;i++){
				itypes.Add(PIn.PInt(strArray[i]));
			}
			bool isAll=false;
			if(itypes.Contains((int)InvalidType.AllLocal)){
				isAll=true;
			}
			DataSet ds=new DataSet();
			if(itypes.Contains((int)InvalidType.AccountingAutoPays) || isAll) {
				ds.Tables.Add(AccountingAutoPays.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.AutoCodes) || isAll){
				ds.Tables.Add(AutoCodes.RefreshCache());
				ds.Tables.Add(AutoCodeItems.RefreshCache());
				ds.Tables.Add(AutoCodeConds.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.AutoNotes) || isAll) {
				ds.Tables.Add(AutoNotes.RefreshCache());
				ds.Tables.Add(AutoNoteControls.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Carriers) || isAll){
				ds.Tables.Add(Carriers.RefreshCache());//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll){
				ds.Tables.Add(ClaimFormItems.RefreshCache());
				ds.Tables.Add(ClaimForms.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll){
				ds.Tables.Add(Clearinghouses.RefreshCache());//kh wants to add an EasyHideClearHouses to disable this
			}
			if(itypes.Contains((int)InvalidType.Computers) || isAll){
				ds.Tables.Add(Computers.RefreshCache());
				ds.Tables.Add(Printers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll){
				ds.Tables.Add(Defs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll){
				ds.Tables.Add(SchoolClasses.RefreshCache());
				ds.Tables.Add(SchoolCourses.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Diseases) || isAll) {
				ds.Tables.Add(DiseaseDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.DisplayFields) || isAll) {
				ds.Tables.Add(DisplayFields.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ElectIDs) || isAll) {
				ds.Tables.Add(ElectIDs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll){
				ds.Tables.Add(EmailTemplates.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll){
				ds.Tables.Add(Employees.RefreshCache());
				ds.Tables.Add(PayPeriods.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Employers) || isAll) {
				ds.Tables.Add(Employers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll){
				ds.Tables.Add(Fees.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.FeeScheds) || isAll){
				ds.Tables.Add(FeeScheds.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll){
				ds.Tables.Add(CovCats.RefreshCache());
				ds.Tables.Add(CovSpans.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.InsFilingCodes) || isAll){
				ds.Tables.Add(InsFilingCodes.RefreshCache());
				ds.Tables.Add(InsFilingCodeSubtypes.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Languages) || isAll) {
				ds.Tables.Add(Lans.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll){
				ds.Tables.Add(Letters.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll){
				ds.Tables.Add(LetterMergeFields.RefreshCache());
				ds.Tables.Add(LetterMerges.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll){
				ds.Tables.Add(Operatories.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.PatFields) || isAll) {
				ds.Tables.Add(PatFieldDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll){
				ds.Tables.Add(Pharmacies.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll){
				ds.Tables.Add(Prefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProcButtons) || isAll) {
				ds.Tables.Add(ProcButtons.RefreshCache());
				ds.Tables.Add(ProcButtonItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll){
				ds.Tables.Add(ProcedureCodes.RefreshCache());
				ds.Tables.Add(ProcCodeNotes.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll){
				ds.Tables.Add(Programs.RefreshCache());
				ds.Tables.Add(ProgramProperties.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProviderIdents) || isAll) {
				ds.Tables.Add(ProviderIdents.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll){
				ds.Tables.Add(Providers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll){
				ds.Tables.Add(QuickPasteNotes.RefreshCache());
				ds.Tables.Add(QuickPasteCats.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.RecallTypes) || isAll){
				ds.Tables.Add(RecallTypes.RefreshCache());
				ds.Tables.Add(RecallTriggers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll){
				ds.Tables.Add(Userods.RefreshCache());
				ds.Tables.Add(UserGroups.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Sheets) || isAll){
				ds.Tables.Add(SheetDefs.RefreshCache());
				ds.Tables.Add(SheetFieldDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Signals) || isAll) {
				ds.Tables.Add(SigElementDefs.RefreshCache());
				ds.Tables.Add(SigButDefs.RefreshCache());//includes SigButDefElements.Refresh()
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll){
				ds.Tables.Add(Sites.RefreshCache());
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll){
				ds.Tables.Add(ToolButItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll){
				ds.Tables.Add(ApptViews.RefreshCache());
				ds.Tables.Add(ApptViewItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll){
				ds.Tables.Add(ZipCodes.RefreshCache());
			}
			return ds;
		}

		///<summary>only if ClientWeb</summary>
		public static void FillCache(DataSet ds,string itypesStr) {
			List<int> itypes=new List<int>();
			string[] strArray=itypesStr.Split(',');
			for(int i=0;i<strArray.Length;i++) {
				itypes.Add(PIn.PInt(strArray[i]));
			}
			bool isAll=false;
			if(itypes.Contains((int)InvalidType.AllLocal)) {
				isAll=true;
			}
			if(itypes.Contains((int)InvalidType.AccountingAutoPays) || isAll) {
				AccountingAutoPays.FillCache(ds.Tables["AccountingAutoPay"]);
			}
			if(itypes.Contains((int)InvalidType.AutoCodes) || isAll) {
				AutoCodes.FillCache(ds.Tables["AutoCode"]);
				AutoCodeItems.FillCache(ds.Tables["AutoCodeItem"]);
				AutoCodeConds.FillCache(ds.Tables["AutoCodeCond"]);
			}
			if(itypes.Contains((int)InvalidType.AutoNotes) || isAll) {
				AutoNotes.FillCache(ds.Tables["AutoNote"]);
				AutoNoteControls.FillCache(ds.Tables["AutoNoteControl"]);
			}
			if(itypes.Contains((int)InvalidType.Carriers) || isAll) {
				Carriers.FillCache(ds.Tables["Carrier"]);//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll) {
				ClaimFormItems.FillCache(ds.Tables["ClaimFormItem"]);
				ClaimForms.FillCache(ds.Tables["ClaimForm"]);
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll) {
				Clearinghouses.FillCache(ds.Tables["Clearinghouse"]);//kh wants to add an EasyHideClearHouses to disable this
			}
			if(itypes.Contains((int)InvalidType.Computers) || isAll) {
				Computers.FillCache(ds.Tables["Computer"]);
				Printers.FillCache(ds.Tables["Printer"]);
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll) {
				Defs.FillCache(ds.Tables["Def"]);
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll) {
				SchoolClasses.FillCache(ds.Tables["SchoolClass"]);
				SchoolClasses.FillCache(ds.Tables["SchoolCourse"]);
			}
			if(itypes.Contains((int)InvalidType.Diseases) || isAll) {
				DiseaseDefs.FillCache(ds.Tables["DiseaseDef"]);
			}
			if(itypes.Contains((int)InvalidType.DisplayFields) || isAll) {
				DisplayFields.FillCache(ds.Tables["DisplayField"]);
			}
			if(itypes.Contains((int)InvalidType.ElectIDs) || isAll) {
				ElectIDs.FillCache(ds.Tables["ElectID"]);
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll) {
				EmailTemplates.FillCache(ds.Tables["EmailTemplate"]);
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll) {
				Employees.FillCache(ds.Tables["Employee"]);
				PayPeriods.FillCache(ds.Tables["PayPeriod"]);
			}
			if(itypes.Contains((int)InvalidType.Employers) || isAll) {
				Employers.FillCache(ds.Tables["Employers"]);
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll) {
				Fees.FillCache(ds.Tables["Fee"]);
			}
			if(itypes.Contains((int)InvalidType.FeeScheds) || isAll) {
				FeeScheds.FillCache(ds.Tables["FeeSched"]);
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll) {
				FeeScheds.FillCache(ds.Tables["CovCat"]);
				FeeScheds.FillCache(ds.Tables["CovSpan"]);
			}
			if(itypes.Contains((int)InvalidType.InsFilingCodes) || isAll){
				InsFilingCodes.FillCache(ds.Tables["InsFilingCode"]);
				InsFilingCodeSubtypes.FillCache(ds.Tables["InsFilingCodeSubtype"]);
			}
			if(itypes.Contains((int)InvalidType.Languages) || isAll) {
				Lans.FillCache(ds.Tables["Language"]);
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll) {
				Letters.FillCache(ds.Tables["Fee"]);
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll) {
				LetterMergeFields.FillCache(ds.Tables["LetterMergeFields"]);
				LetterMerges.FillCache(ds.Tables["LetterMerge"]);
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll) {
				Operatories.FillCache(ds.Tables["Operatory"]);
			}
			if(itypes.Contains((int)InvalidType.PatFields) || isAll) {
				PatFieldDefs.FillCache(ds.Tables["PatFieldDef"]);
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll) {
				Pharmacies.FillCache(ds.Tables["Pharmacy"]);
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll) {
				Prefs.FillCache(ds.Tables["Pref"]);
			}
			if(itypes.Contains((int)InvalidType.ProcButtons) || isAll) {
				ProcButtons.FillCache(ds.Tables["ProcButton"]);
				ProcButtonItems.FillCache(ds.Tables["ProcButtonItem"]);
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll) {
				ProcedureCodes.FillCache(ds.Tables["ProcedureCode"]);
				ProcCodeNotes.FillCache(ds.Tables["ProcCodeNote"]);
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll) {
				Programs.FillCache(ds.Tables["Program"]);
				ProgramProperties.FillCache(ds.Tables["ProgramProperty"]);
			}
			if(itypes.Contains((int)InvalidType.ProviderIdents) || isAll) {
				ProviderIdents.FillCache(ds.Tables["ProviderIdent"]);
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll) {
				Providers.FillCache(ds.Tables["Provider"]);
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll) {
				QuickPasteNotes.FillCache(ds.Tables["QuickPasteNote"]);
				QuickPasteCats.FillCache(ds.Tables["QuickPasteCat"]);
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll) {
				Userods.FillCache(ds.Tables["Userod"]);
				UserGroups.FillCache(ds.Tables["UserGroup"]);
			}
			if(itypes.Contains((int)InvalidType.Sheets) || isAll) {
				SheetDefs.FillCache(ds.Tables["SheetDef"]);
				SheetFieldDefs.FillCache(ds.Tables["SheetFieldDef"]);
			}
			if(itypes.Contains((int)InvalidType.Signals) || isAll) {
				SigElementDefs.FillCache(ds.Tables["SigElementDef"]);
				SigButDefs.FillCache(ds.Tables["SigButDef"]);//includes SigButDefElements.Refresh()
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll) {
				Sites.FillCache(ds.Tables["Site"]);
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll) {
				ToolButItems.FillCache(ds.Tables["ToolButItem"]);
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll) {
				ApptViews.FillCache(ds.Tables["ApptView"]);
				ApptViewItems.FillCache(ds.Tables["ApptViewItem"]);
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll) {
				ZipCodes.FillCache(ds.Tables["ZipCode"]);
			}

		}

	}
}
