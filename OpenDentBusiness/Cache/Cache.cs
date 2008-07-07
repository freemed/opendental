using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public class Cache {
		public static DataSet Refresh(string itypesStr){
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
				ds.Tables.Add(Defs.RefreshCache());
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
				ds.Tables.Add(DisplayFields.Refresh());
			}
			if(itypes.Contains((int)InvalidType.Letters) || isAll){
				//Letters.Refresh();
			}
			if(itypes.Contains((int)InvalidType.LetterMerge) || isAll){
				//LetterMergeFields.Refresh();
				//LetterMerges.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Operatories) || isAll){
				ds.Tables.Add(Operatories.RefreshCache());
				//AccountingAutoPayL.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Pharmacies) || isAll){
				ds.Tables.Add(Pharmacies.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Prefs) || isAll){
				ds.Tables.Add(Prefs.RefreshCache());
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
				ds.Tables.Add(Providers.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.QuickPaste) || isAll){
				//QuickPasteNotes.Refresh();
				//QuickPasteCats.Refresh();
			}
			if(itypes.Contains((int)InvalidType.Security) || isAll){
				ds.Tables.Add(Userods.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.Sites) || isAll){
				ds.Tables.Add(Sites.RefreshCache());
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
				ds.Tables.Add(ApptViews.RefreshCache());
				ds.Tables.Add(ApptViewItems.RefreshCache());
			}
			if(itypes.Contains((int)InvalidType.ZipCodes) || isAll){
				//ZipCodes.Refresh();
				//PatFieldDefs.Refresh();
			}
			return ds;
		}
	}
}
