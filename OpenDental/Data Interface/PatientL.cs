using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class PatientL{
		///<summary>Collection of Patient Names. The last five patients. Gets displayed on dropdown button.</summary>
		private static List<string> buttonLastFiveNames;
		///<summary>Collection of PatNums. The last five patients. Used when clicking on dropdown button.</summary>
		private static List<long> buttonLastFivePatNums;

		///<summary>The current patient will already be on the button.  This adds the family members when user clicks dropdown arrow. Can handle null values for pat and fam.  Need to supply the menu to fill as well as the EventHandler to set for each item (all the same).</summary>
		public static void AddFamilyToMenu(ContextMenu menu,EventHandler onClick,long patNum,Family fam) {
			//No need to check RemotingRole; no call to db.
			//fill menu
			menu.MenuItems.Clear();
			for(int i=0;i<buttonLastFiveNames.Count;i++) {
				menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			}
			menu.MenuItems.Add("-");
			menu.MenuItems.Add("FAMILY");
			if(patNum!=0 && fam!=null) {
				for(int i=0;i<fam.ListPats.Length;i++) {
					menu.MenuItems.Add(fam.ListPats[i].GetNameLF(),onClick);
				}
			}
		}

		///<summary>Does not handle null values. Use zero.  Does not handle adding family members.  Returns</summary>
		public static bool AddPatsToMenu(ContextMenu menu,EventHandler onClick,string nameLF,long patNum) {
			//No need to check RemotingRole; no call to db.
			//add current patient
			if(buttonLastFivePatNums==null) {
				buttonLastFivePatNums=new List<long>();
			}
			if(buttonLastFiveNames==null) {
				buttonLastFiveNames=new List<string>();
			}
			if(patNum==0) {
				return false;
			}
			if(buttonLastFivePatNums.Count>0 && patNum==buttonLastFivePatNums[0]) {//same patient selected
				return false;
			}
			//Patient has changed
			buttonLastFivePatNums.Insert(0,patNum);
			buttonLastFiveNames.Insert(0,nameLF);
			if(buttonLastFivePatNums.Count>5) {
				buttonLastFivePatNums.RemoveAt(5);
				buttonLastFiveNames.RemoveAt(5);
			}
			if(AutomationL.Trigger(AutomationTrigger.OpenPatient,null,patNum)) {
				return true;//Will cause MouseUpForced if in ApptModule
			}
			return false;
			//fill menu
			//menu.MenuItems.Clear();
			//for(int i=0;i<buttonLastFiveNames.Count;i++) {
			//	menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			//}
		}

		///<summary>Determines which menu Item was selected from the Patient dropdown list and returns the patNum for that patient. This will not be activated when click on 'FAMILY' or on separator, because they do not have events attached.  Calling class then does a ModuleSelected.</summary>
		public static long ButtonSelect(ContextMenu menu,object sender,Family fam) {
			//No need to check RemotingRole; no call to db.
			int index=menu.MenuItems.IndexOf((MenuItem)sender);
			//Patients.PatIsLoaded=true;
			if(index<buttonLastFivePatNums.Count) {
				return (long)buttonLastFivePatNums[index];
			}
			if(fam==null) {
				return 0;//will never happen
			}
			return fam.ListPats[index-buttonLastFivePatNums.Count-2].PatNum;
		}

		///<summary>A simpler version which does not require as much data.</summary>
		public static string GetMainTitle(string nameLF,long patNum,string chartNumber,long siteNum) {
			string retVal=PrefC.GetString(PrefName.MainWindowTitle);
			if(Security.CurUser!=null){
				retVal+=" {"+Security.CurUser.UserName+"}";
			}
			if(patNum==0 || patNum==-1){
				return retVal;
			}
			retVal+=" - "+nameLF;
			if(PrefC.GetLong(PrefName.ShowIDinTitleBar)==1) {
				retVal+=" - "+patNum.ToString();
			}
			else if(PrefC.GetLong(PrefName.ShowIDinTitleBar)==2) {
				retVal+=" - "+chartNumber;
			}
			if(siteNum!=0){
				retVal+=" - "+Sites.GetDescription(siteNum);
			}
			return retVal;
		}
	}
	
	///<summary></summary>
	public class PatientSelectedEventArgs{
		private long patNum;
		private string patName;
		private bool hasEmail;
		private string chartNumber;

		///<summary></summary>
		public PatientSelectedEventArgs(long patNum,string patName,bool hasEmail,string chartNumber) {
			this.patNum=patNum;
			this.patName=patName;
			this.hasEmail=hasEmail;
			this.chartNumber=chartNumber;
		}

		///<summary></summary>
		public long PatNum {
			get{ 
				return patNum;
			}
		}

		///<summary></summary>
		public string PatName {
			get {
				return patName;
			}
		}

		///<summary>This is required in order to enable/disable the email button intelligently.</summary>
		public bool HasEmail {
			get {
				return hasEmail;
			}
		}

		///<summary>This is required in order to properly set the form title when user wants to see chart numbers.</summary>
		public string ChartNumber {
			get {
				return chartNumber;
			}
		}

	}

	///<summary></summary>
	public delegate void PatientSelectedEventHandler(object sender,PatientSelectedEventArgs e);


}










