package com.opendental.odweb.client.usercontrols;

import com.google.gwt.user.client.ui.Label;
import com.opendental.opendentbusiness.tabletypes.Patient;

public class LabelMainTitle extends Label {
	
	public LabelMainTitle() {
		
	}
	
	/** Gets the main title bar with the the patient's information. */
	public String getMainTitle(Patient pat) {
		String retVal="";//PrefC.GetString(PrefName.MainWindowTitle);
//		if(Security.CurUser!=null){
//			retVal+=" {"+Security.CurUser.UserName+"}";
//		}
		if(pat==null || pat.PatNum==0 || pat.PatNum==-1) {
			return retVal;
		}
//		retVal+=" - "+pat.GetNameLF();
//		if(PrefC.GetLong(PrefName.ShowIDinTitleBar)==1) {
//			retVal+=" - "+pat.PatNum.ToString();
//		}
//		else if(PrefC.GetLong(PrefName.ShowIDinTitleBar)==2) {
//			retVal+=" - "+pat.ChartNumber;
//		}
//		else if(PrefC.GetLong(PrefName.ShowIDinTitleBar)==3) {
//			if(pat.Birthdate.Year>1880) {
//				retVal+=" - "+pat.Birthdate.ToShortDateString();
//			}
//		}
//		if(pat.SiteNum!=0){
//			retVal+=" - "+Sites.GetDescription(pat.SiteNum);
//		}
		return retVal;
	}
	
	/** Sets the label's text to the passed in string. */
	public void setMainTitle(String text) {
		this.setText(text);
	}
}
