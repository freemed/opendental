using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class AutomationL {
		///<summary>ProcCodes will be null unless trigger is CompleteProcedure.  This routine will generally fail silently.</summary>
		public static void Trigger(AutomationTrigger trigger,List<string> procCodes,long patNum) {
			for(int i=0;i<Automations.Listt.Count;i++) {
				if(Automations.Listt[i].Autotrigger!=trigger) {
					continue;
				}
				if(trigger==AutomationTrigger.CompleteProcedure) {
					if(procCodes==null) {
						continue;//fail silently
					}
					bool codeFound=false;
					string[] arrayCodes=Automations.Listt[i].ProcCodes.Split(',');
					for(int p=0;p<procCodes.Count;p++) {
						for(int a=0;a<arrayCodes.Length;a++) {
							if(arrayCodes[a]==procCodes[p]) {
								codeFound=true;
								break;
							}
						}
					}
					if(!codeFound) {
						continue;
					}
				}
				//matching automation item has been found
				//Get possible list of conditions that exist for this automation item
				List<AutomationCondition> autoConditionsList=AutomationConditions.GetListByAutomationNum(Automations.Listt[i].AutomationNum);
				if(Automations.Listt[i].AutoAction==AutomationAction.CreateCommlog) {
					if(autoConditionsList.Count>0) {
						if(!CheckAutomationConditions(autoConditionsList,patNum)) {
							continue;
						}
					}
					Commlog CommlogCur=new Commlog();
					CommlogCur.PatNum=patNum;
					CommlogCur.CommDateTime=DateTime.Now;
					CommlogCur.CommType=Automations.Listt[i].CommType;
					CommlogCur.Note=Automations.Listt[i].MessageContent;
					CommlogCur.Mode_=CommItemMode.None;
					CommlogCur.UserNum=Security.CurUser.UserNum;
					FormCommItem FormCI=new FormCommItem(CommlogCur);
					FormCI.IsNew=true;
					FormCI.ShowDialog();
				}
				else if(Automations.Listt[i].AutoAction==AutomationAction.PrintPatientLetter) {
					if(autoConditionsList.Count>0) {
						if(!CheckAutomationConditions(autoConditionsList,patNum)) {
							continue;
						}
					}
					SheetDef sheetDef=SheetDefs.GetSheetDef(Automations.Listt[i].SheetDefNum);
					Sheet sheet=SheetUtil.CreateSheet(sheetDef,patNum);
					SheetParameter.SetParameter(sheet,"PatNum",patNum);
					//SheetParameter.SetParameter(sheet,"ReferralNum",referral.ReferralNum);
					SheetFiller.FillFields(sheet);
					using(Bitmap bmp=new Bitmap(100,100)) {//a dummy bitmap for the graphics object
						using(Graphics g=Graphics.FromImage(bmp)) {
							SheetUtil.CalculateHeights(sheet,g);
						}
					}
					FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
					FormSF.ShowDialog();
				}
				else if(Automations.Listt[i].AutoAction==AutomationAction.PrintReferralLetter) {
					if(autoConditionsList.Count>0) {
						if(!CheckAutomationConditions(autoConditionsList,patNum)) {
							continue;
						}
					}
					long referralNum=RefAttaches.GetReferralNum(patNum);
					if(referralNum==0) {
						MsgBox.Show("Automations","This patient has no referral source entered.");
						continue;
					}
					SheetDef sheetDef=SheetDefs.GetSheetDef(Automations.Listt[i].SheetDefNum);
					Sheet sheet=SheetUtil.CreateSheet(sheetDef,patNum);
					SheetParameter.SetParameter(sheet,"PatNum",patNum);
					SheetParameter.SetParameter(sheet,"ReferralNum",referralNum);
					SheetFiller.FillFields(sheet);
					using(Bitmap bmp=new Bitmap(100,100)) {//a dummy bitmap for the graphics object
						using(Graphics g=Graphics.FromImage(bmp)) {
							SheetUtil.CalculateHeights(sheet,g);
						}
					}
					FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
					FormSF.ShowDialog();
				}
				else if(Automations.Listt[i].AutoAction==AutomationAction.ShowExamSheet) {
					if(autoConditionsList.Count>0) {
						if(!CheckAutomationConditions(autoConditionsList,patNum)) {
							continue;
						}
					}
					SheetDef sheetDef=SheetDefs.GetSheetDef(Automations.Listt[i].SheetDefNum);
					Sheet sheet=SheetUtil.CreateSheet(sheetDef,patNum);
					SheetParameter.SetParameter(sheet,"PatNum",patNum);
					SheetFiller.FillFields(sheet);
					using(Bitmap bmp=new Bitmap(100,100)) {//a dummy bitmap for the graphics object
						using(Graphics g=Graphics.FromImage(bmp)) {
							SheetUtil.CalculateHeights(sheet,g);
						}
					}
					FormSheetFillEdit FormSF=new FormSheetFillEdit(sheet);
					FormSF.ShowDialog();
				}
			}
		}

		private static bool CheckAutomationConditions(List<AutomationCondition> autoConditionsList,long patNum) {
			//Make sure every condition returns true
			for(int i=0;i<autoConditionsList.Count;i++) {
				switch(autoConditionsList[i].CompareField) {
					case AutoCondField.SheetCompletedTodayWithName:
						if(!SheetCompletedTodayWithName(autoConditionsList[i],patNum)) {
							return false;
						}
						break;
				}
			}
			return true;
		}

		private static bool SheetCompletedTodayWithName(AutomationCondition autoCond, long patNum) {
			List<Sheet> sheetList=Sheets.GetForPatientForToday(patNum);
			switch(autoCond.Comparison) {//Find out what operand to use.
				case AutoCondComparison.Equals:
					//Loop through every sheet to find one that matches the condition.
					for(int i=0;i<sheetList.Count;i++) {
						if(sheetList[i].Description==autoCond.CompareString) {//Operand based on AutoCondComparison.
							return true;
						}
					}
					break;
			}
			return false;
		}






	}
}
