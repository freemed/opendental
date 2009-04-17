using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class Cache {
		///<summary>This is only called from the UI.  Its purpose is to refresh the cache for one type on both the workstation and server.</summary>
		public static void Refresh(InvalidType itype) {
			//List<int> itypes=new List<int>();
			//itypes.Add((int)itype);
			/*string itypesStr="";
			for(int i=0;i<itypes.Count;i++) {
				if(i>0) {
					itypesStr+=",";
				}
				itypesStr+=itypes[i].ToString();
			}*/
			//Refresh(itypesStr);
			int intItype=(int)itype;
			RefreshCache(intItype.ToString());
		}
			 
		///<summary>itypesStr= comma-delimited list of int.  Called directly from UI in one spot.  Called from above repeatedly.  The end result is that both server and client have been properly refreshed.</summary>
		public static void RefreshCache(string itypesStr){
			DataSet ds=GetCacheDs(itypesStr);
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				//Because otherwise it was handled just fine as part of the GetChacheDs
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
			if(itypes.Contains((int)InvalidType.Carriers) || isAll){
				ds.Tables.Add(Carriers.RefreshCache());//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll){
				ds.Tables.Add(ClaimFormItems.RefreshCache());
				ClaimForms.Refresh();
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll){
				//kh until we add an EasyHideClearHouses						Clearinghouses.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Computers) || isAll){
				Computers.Refresh();
				Printers.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll){
				ds.Tables.Add(Defs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll){
				SchoolClasses.Refresh();
				SchoolCourses.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Diseases) || isAll) {
				DiseaseDefs.Refresh();
			}
			if(itypes.Contains((int)InvalidType.DisplayFields) || isAll) {
				ds.Tables.Add(DisplayFields.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll){
				EmailTemplates.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll){
				Employees.Refresh();
				PayPeriods.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll){
				Fees.Refresh();
			}
			if(itypes.Contains((int)InvalidType.FeeScheds) || isAll){
				ds.Tables.Add(FeeScheds.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll){
				ds.Tables.Add(CovCats.RefreshCache());
				ds.Tables.Add(CovSpans.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll){
				Letters.Refresh();
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll){
				LetterMergeFields.Refresh();
				LetterMerges.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll){
				ds.Tables.Add(Operatories.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.PatFields) || isAll) {
				PatFieldDefs.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll){
				ds.Tables.Add(Pharmacies.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll){
				ds.Tables.Add(Prefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ProcButtons) || isAll) {
				ProcButtons.Refresh();
				ProcButtonItems.Refresh();
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll){
				ds.Tables.Add(ProcedureCodes.RefreshCache());
				ProcCodeNotes.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll){
				ds.Tables.Add(Programs.RefreshCache());
				ds.Tables.Add(ProgramProperties.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll){
				ds.Tables.Add(Providers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll){
				QuickPasteNotes.Refresh();
				QuickPasteCats.Refresh();
			}
			if(itypes.Contains((int)InvalidType.RecallTypes) || isAll){
				ds.Tables.Add(RecallTypes.RefreshCache());
				ds.Tables.Add(RecallTriggers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll){
				ds.Tables.Add(Userods.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Sheets) || isAll){
				ds.Tables.Add(SheetDefs.RefreshCache());
				ds.Tables.Add(SheetFieldDefs.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Signals) || isAll) {
				SigElementDefs.Refresh();
				SigButDefs.Refresh();//includes SigButDefElements.Refresh()
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll){
				ds.Tables.Add(Sites.RefreshCache());
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll){
				ToolButItems.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll){
				ds.Tables.Add(ApptViews.RefreshCache());
				ds.Tables.Add(ApptViewItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll){
				ZipCodes.Refresh();
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
			if(itypes.Contains((int)InvalidType.Carriers) || isAll) {
				Carriers.FillCache(ds.Tables["Carrier"]);//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll) {
				ClaimFormItems.FillCache(ds.Tables["ClaimFormItem"]);
				ClaimForms.Refresh();
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll) {
				//kh until we add an EasyHideClearHouses						Clearinghouses.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Computers) || isAll) {
				Computers.Refresh();
				Printers.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll) {
				Defs.FillCache(ds.Tables["Def"]);
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll) {
				SchoolClasses.Refresh();
				SchoolCourses.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Diseases) || isAll) {
				DiseaseDefs.Refresh();
			}
			if(itypes.Contains((int)InvalidType.DisplayFields) || isAll) {
				DisplayFields.FillCache(ds.Tables["DisplayField"]); ;
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll) {
				EmailTemplates.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll) {
				Employees.Refresh();
				PayPeriods.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll) {
				Fees.Refresh();
			}
			if(itypes.Contains((int)InvalidType.FeeScheds) || isAll) {
				FeeScheds.FillCache(ds.Tables["FeeSched"]);
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll) {
				FeeScheds.FillCache(ds.Tables["CovCat"]);
				FeeScheds.FillCache(ds.Tables["CovSpan"]);
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll) {
				Letters.Refresh();
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll) {
				LetterMergeFields.Refresh();
				LetterMerges.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll) {
				Operatories.FillCache(ds.Tables["Operatory"]);
			}
			if(itypes.Contains((int)InvalidType.PatFields) || isAll) {
				PatFieldDefs.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll) {
				Pharmacies.FillCache(ds.Tables["Pharmacy"]);
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll) {
				Prefs.FillCache(ds.Tables["Pref"]);
			}
			if(itypes.Contains((int)InvalidType.ProcButtons) || isAll) {
				ProcButtons.Refresh();
				ProcButtonItems.Refresh();
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll) {
				ProcedureCodes.FillCache(ds.Tables["ProcedureCode"]);
				ProcCodeNotes.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll) {
				Programs.FillCache(ds.Tables["Program"]);
				Programs.FillCache(ds.Tables["ProgramProperty"]);
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll) {
				Providers.FillCache(ds.Tables["Provider"]);
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll) {
				QuickPasteNotes.Refresh();
				QuickPasteCats.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll) {
				Userods.FillCache(ds.Tables["Userod"]);
			}
			if(itypes.Contains((int)InvalidType.Sheets) || isAll) {
				SheetDefs.FillCache(ds.Tables["SheetDef"]);
				SheetFieldDefs.FillCache(ds.Tables["SheetFieldDef"]);
			}
			if(itypes.Contains((int)InvalidType.Signals) || isAll) {
				SigElementDefs.Refresh();
				SigButDefs.Refresh();//includes SigButDefElements.Refresh()
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll) {
				Sites.FillCache(ds.Tables["Site"]);
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll) {
				ToolButItems.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll) {
				ApptViews.FillCache(ds.Tables["ApptView"]);
				ApptViewItems.FillCache(ds.Tables["ApptViewItem"]);
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll) {
				ZipCodes.Refresh();
			}

		}









	}
}
