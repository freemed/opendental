using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness.DataAccess;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class PatientL{
		///<summary>Collection of Patient Names. The last five patients. Gets displayed on dropdown button.</summary>
		private static List<string> buttonLastFiveNames;
		///<summary>Collection of PatNums. The last five patients. Used when clicking on dropdown button.</summary>
		private static List<int> buttonLastFivePatNums;

		///<summary>The current patient will already be on the button.  This adds the family members when user clicks dropdown arrow. Can handle null values for pat and fam.  Need to supply the menu to fill as well as the EventHandler to set for each item (all the same).</summary>
		public static void AddFamilyToMenu(ContextMenu menu,EventHandler onClick,int patNum,Family fam) {
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

		///<summary>Does not handle null values. Use zero.  Does not handle adding family members.</summary>
		public static void AddPatsToMenu(ContextMenu menu,EventHandler onClick,string nameLF,int patNum) {
			//No need to check RemotingRole; no call to db.
			//add current patient
			if(buttonLastFivePatNums==null) {
				buttonLastFivePatNums=new List<int>();
			}
			if(buttonLastFiveNames==null) {
				buttonLastFiveNames=new List<string>();
			}
			if(patNum!=0) {
				if(buttonLastFivePatNums.Count==0	|| patNum!=buttonLastFivePatNums[0]) {//different patient selected
					buttonLastFivePatNums.Insert(0,patNum);
					buttonLastFiveNames.Insert(0,nameLF);
					if(buttonLastFivePatNums.Count>5) {
						buttonLastFivePatNums.RemoveAt(5);
						buttonLastFiveNames.RemoveAt(5);
					}
				}
			}
			//fill menu
			//menu.MenuItems.Clear();
			//for(int i=0;i<buttonLastFiveNames.Count;i++) {
			//	menu.MenuItems.Add(buttonLastFiveNames[i].ToString(),onClick);
			//}
		}

		///<summary>Determines which menu Item was selected from the Patient dropdown list and returns the patNum for that patient. This will not be activated when click on 'FAMILY' or on separator, because they do not have events attached.  Calling class then does a ModuleSelected.</summary>
		public static int ButtonSelect(ContextMenu menu,object sender,Family fam) {
			//No need to check RemotingRole; no call to db.
			int index=menu.MenuItems.IndexOf((MenuItem)sender);
			//Patients.PatIsLoaded=true;
			if(index<buttonLastFivePatNums.Count) {
				return (int)buttonLastFivePatNums[index];
			}
			if(fam==null) {
				return 0;//will never happen
			}
			return fam.ListPats[index-buttonLastFivePatNums.Count-2].PatNum;
		}

		///<summary>A simpler version which does not require as much data.</summary>
		public static string GetMainTitle(string nameLF,int patNum,string chartNumber,int siteNum) {
			string retVal=PrefC.GetString("MainWindowTitle");
			if(Security.CurUser!=null){
				retVal+=" {"+Security.CurUser.UserName+"}";
			}
			if(patNum==0 || patNum==-1){
				return retVal;
			}
			retVal+=" - "+nameLF;
			if(PrefC.GetInt("ShowIDinTitleBar")==1) {
				retVal+=" - "+patNum.ToString();
			}
			else if(PrefC.GetInt("ShowIDinTitleBar")==2) {
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
		private int patNum;
		private string patName;
		private bool hasEmail;
		private string chartNumber;

		///<summary></summary>
		public PatientSelectedEventArgs(int patNum,string patName,bool hasEmail,string chartNumber){
			this.patNum=patNum;
			this.patName=patName;
			this.hasEmail=hasEmail;
			this.chartNumber=chartNumber;
		}

		///<summary></summary>
		public int PatNum{
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










