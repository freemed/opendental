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
	public class Patients2{
		//<summary>Collection of Patient Names. The last five patients. Gets displayed on dropdown button.</summary>
		//private static ArrayList buttonLastFiveNames;
		//<summary>Collection of PatNums. The last five patients. Used when clicking on dropdown button.</summary>
		//private static ArrayList buttonLastFivePatNums;
		/*
		///<summary>It is entirely acceptable to pass in a null value for PatCur.  In that case, no patient name will show.</summary>
		public static string GetMainTitle(Patient PatCur){
			string retVal=PrefC.GetString("MainWindowTitle");
			if(Security.CurUser!=null) {
				retVal+=" {"+Security.CurUser.UserName+"}";
			}
			if(PatCur==null){
				return retVal;
			}
			retVal+=" - "+PatCur.GetNameLF();
			//if(PrefC.GetInt("ShowIDinTitleBar")==0){//no action
			if(PrefC.GetInt("ShowIDinTitleBar")==1){
				retVal+=" - "+PatCur.PatNum.ToString();
			}
			else if(PrefC.GetInt("ShowIDinTitleBar")==2) {
				retVal+=" - "+PatCur.ChartNumber;
			}
			return retVal;
		}*/

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










