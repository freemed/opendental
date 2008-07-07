using System;
using System.Collections.Generic;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
	public class CacheL {
		public static void Refresh(InvalidType itype){
			List<int> listInt=new List<int>();
			listInt.Add((int)itype);
			Refresh(listInt);
		}

		public static void Refresh(List<int> itypes){
			string itypesStr="";
			for(int i=0;i<itypes.Count;i++){
				if(i>0){
					itypesStr+=",";
				}
				itypesStr+=itypes[i].ToString();
			}
			DataSet ds=Gen.GetDS(MethodNameDS.Cache_Refresh,itypesStr);
			bool isAll=false;
			if(itypes.Contains((int)InvalidType.AllLocal)){
				isAll=true;
			}
			if(itypes.Contains((int)InvalidType.AutoCodesProcButtons) || isAll){
				//AutoCodeL.Refresh();
				//AutoCodeItemL.Refresh();
				//AutoCodeCondL.Refresh();
				//ProcButtons.Refresh();
				//ProcButtonItems.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Carriers) || isAll){
				//Carriers.Refresh();//run on startup, after telephone reformat, after list edit.
			}
			if(itypes.Contains((int)InvalidType.ClaimForms) || isAll){
				//ClaimFormItemL.Refresh();
				//ClaimForms.Refresh();
			}
			if(itypes.Contains((int)InvalidType.ClearHouses) || isAll){
				//kh until we add an EasyHideClearHouses						Clearinghouses.Refresh();
				//SigElementDefs.Refresh();
				//SigButDefs.Refresh();//includes SigButDefElements.Refresh()
			}
			if(itypes.Contains((int)InvalidType.Computers) || isAll){
				//Computers.Refresh();
				//Printers.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Defs) || isAll){
				Defs.FillCache(ds.Tables["Def"]);
			}
			if(itypes.Contains((int)InvalidType.DentalSchools) || isAll){
				//SchoolClasses.Refresh();
				//SchoolCourses.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Email) || isAll){
				//EmailTemplates.Refresh();
				//DiseaseDefs.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Employees) || isAll){
				//Employees.Refresh();
				//PayPeriods.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Fees) || isAll){
				//Fees.Refresh();
			}
			if(itypes.Contains((int)InvalidType.InsCats) || isAll){
				//CovCatL.Refresh();
				//CovSpanL.Refresh();
				DisplayFields.FillCache(ds.Tables["DisplayField"]);;
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll){
				//Letters.Refresh();
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll){
				//LetterMergeFields.Refresh();
				//LetterMerges.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll){
				Operatories.FillCache(ds.Tables["Operatory"]);
				//AccountingAutoPayL.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll){
				Pharmacies.FillCache(ds.Tables["Pharmacy"]);
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll){
				Prefs.FillCache(ds.Tables["Pref"]);
			}
			if(itypes.Contains((int)InvalidType.ProcCodes) || isAll){
				//ProcedureCodes.Refresh();
				//ProcCodeNotes.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Programs) || isAll){
				//Programs.Refresh();
				//ProgramProperties.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Providers) || isAll){
				Providers.FillCache(ds.Tables["Provider"]);
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll){
				//QuickPasteNotes.Refresh();
				//QuickPasteCats.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll){
				Userods.FillCache(ds.Tables["Userod"]);
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll){
				Sites.FillCache(ds.Tables["Site"]);
			}
			if(itypes.Contains((int)InvalidType.Startup) || isAll){
				//Employers.Refresh();//only needed when opening the prog. After that, automated.
				//ElectIDs.Refresh();//only run on startup
				//Referrals.Refresh();//Referrals are also refreshed dynamically.
			}
			//InvalidTypes.Tasks not handled here.
			if(itypes.Contains((int)InvalidType.ToolBut) || isAll){
				//ToolButItems.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Views) || isAll){
				ApptViews.FillCache(ds.Tables["ApptView"]);
				ApptViewItems.FillCache(ds.Tables["ApptViewItem"]);
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll){
				//ZipCodes.Refresh();
				//PatFieldDefs.Refresh();
			}




		}

	}
}
