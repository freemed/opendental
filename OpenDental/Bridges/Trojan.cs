using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	class Trojan {

		public static void StartupCheck(){
			Program ProgramCur=Programs.GetCur("Trojan");
			if(!ProgramCur.Enabled){
				return;
			}
			RegistryKey regKey=Registry.LocalMachine.OpenSubKey("Software\\TROJAN BENEFIT SERVICE");
			if(regKey==null){//dmg Unix OS will exit here.
				//MessageBox.Show("Trojan not installed properly.");
				return;
			}
			string file=regKey.GetValue("INSTALLDIR").ToString()+@"\ALLPLANS.TXT";//C:\ETW\ALLPLANS.TXT
			if(!File.Exists(file)){
				//MessageBox.Show(file+" not found.");
				return;
			}
			if(!MsgBox.Show("Trojan",true,"Trojan update found.  All plans will now be updated.")){
				return;
			}
			string allplantext="";
			using(StreamReader sr=new StreamReader(file)){
				allplantext=sr.ReadToEnd();
			}
			if(allplantext==""){
				return;
			}
			string[] trojanplans=allplantext.Split(new string[] {"TROJANID"},StringSplitOptions.RemoveEmptyEntries);
			int plansAffected=0;
			for(int i=0;i<trojanplans.Length;i++){
				trojanplans[i]="TROJANID"+trojanplans[i];
				plansAffected+=ProcessTrojanPlan(trojanplans[i]);
			}
			MessageBox.Show(plansAffected.ToString()+" plans updated.");
			File.Delete(file);
		}

		///<summary>Returns the number of plans updated.</summary>
		private static int ProcessTrojanPlan(string trojanPlan){
			//MessageBox.Show(trojanPlan);
			string[] lines=trojanPlan.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
			//MessageBox.Show(lines[0]);
			//MessageBox.Show(lines.Length.ToString());
			string line;
			string[] fields;
			int percent;
			string[] splitField;//if a field is a sentence with more than one word, we can split it for analysis
			InsPlan plan=new InsPlan();//many fields will be absent.  This is a conglomerate.
			Carrier carrier=new Carrier();
			ArrayList benefitList=new ArrayList();
			bool usesAnnivers=false;
			Benefit ben;
      for(int i=0;i<lines.Length;i++){
				line=lines[i];
				fields=line.Split(new char[] {'\t'});
				if(fields.Length!=3){
					continue;
				}
				//remove any trailing or leading spaces:
				fields[0]=fields[0].Trim();
				fields[1]=fields[1].Trim();
				fields[2]=fields[2].Trim();
				if(fields[2]==""){
					continue;
				}
				else{//as long as there is data, add it to the notes
					if(plan.BenefitNotes!=""){
						plan.BenefitNotes+="\r\n";
					}
					plan.BenefitNotes+=fields[1]+": "+fields[2];
				}
				switch(fields[0]){
					//default://for all rows that are not handled below
					case "TROJANID":
						plan.TrojanID=fields[2];
						break;
					case "ENAME":
						plan.EmployerNum=Employers.GetEmployerNum(fields[2]);
						break;
					case "PLANDESC":
						plan.GroupName=fields[2];
						break;
					case "ELIGPHONE":
						carrier.Phone=fields[2];
						break;
					case "POLICYNO":
						plan.GroupNum=fields[2];
						break;
					case "ECLAIMS":
						if(fields[2]=="YES") {//accepts eclaims
							carrier.NoSendElect=false;
						}
						else {
							carrier.NoSendElect=true;
						}
						break;
					case "PAYERID":
						carrier.ElectID=fields[2];
						break;
					case "MAILTO":
						carrier.CarrierName=fields[2];
						break;
					case "MAILTOST":
						carrier.Address=fields[2];
						break;
					case "MAILCITYONLY":
						carrier.City=fields[2];
						break;
					case "MAILSTATEONLY":
						carrier.State=fields[2];
						break;
					case "MAILZIPONLY":
						carrier.Zip=fields[2];
						break;
					case "PLANMAX"://eg $3000 per person per year
						if(!fields[2].StartsWith("$"))
							break;
						fields[2]=fields[2].Remove(0,1);
						fields[2]=fields[2].Split(new char[] { ' ' })[0];
						if(CovCatB.ListShort.Length>0) {
							ben=new Benefit();
							ben.BenefitType=InsBenefitType.Limitations;
							ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
							ben.MonetaryAmt=PIn.PDouble(fields[2]);
							ben.TimePeriod=BenefitTimePeriod.CalendarYear;
							benefitList.Add(ben.Copy());
						}
						break;
					case "PLANYR"://eg Calendar year or Anniversary year
						if(fields[2]!="Calendar year") {
							usesAnnivers=true;
							//MessageBox.Show("Warning.  Plan uses Anniversary year rather than Calendar year.  Please verify the Plan Start Date.");
						}
						break;
					case "DEDUCT"://eg There is no deductible
						ben=new Benefit();
						ben.BenefitType=InsBenefitType.Deductible;
						ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
						if(!fields[2].StartsWith("$")) {
							ben.MonetaryAmt=0;
						}
						else {
							fields[2]=fields[2].Remove(0,1);
							fields[2]=fields[2].Split(new char[] { ' ' })[0];
							ben.MonetaryAmt=PIn.PDouble(fields[2]);
						}
						benefitList.Add(ben.Copy());
						break;
					case "PREV"://eg 100%
						splitField=fields[2].Split(new char[] { ' ' });
						if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
							break;
						}
						splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
						percent=PIn.PInt(splitField[0]);
						if(percent<0 || percent>100) {
							break;
						}
						ben=new Benefit();
						ben.BenefitType=InsBenefitType.Percentage;
						ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
						ben.Percent=percent;
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
						benefitList.Add(ben.Copy());
						break;
					case "BASIC":
						splitField=fields[2].Split(new char[] { ' ' });
						if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
							break;
						}
						splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
						percent=PIn.PInt(splitField[0]);
						if(percent<0 || percent>100) {
							break;
						}
						ben=new Benefit();
						ben.BenefitType=InsBenefitType.Percentage;
						ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
						ben.Percent=percent;
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
						benefitList.Add(ben.Copy());
						ben=new Benefit();
						ben.BenefitType=InsBenefitType.Percentage;
						ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
						ben.Percent=percent;
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
						benefitList.Add(ben.Copy());
						ben=new Benefit();
						ben.BenefitType=InsBenefitType.Percentage;
						ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
						ben.Percent=percent;
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
						benefitList.Add(ben.Copy());
						break;
					case "MAJOR":
						splitField=fields[2].Split(new char[] { ' ' });
						if(splitField.Length==0 || !splitField[0].EndsWith("%")) {
							break;
						}
						splitField[0]=splitField[0].Remove(splitField[0].Length-1,1);//remove %
						percent=PIn.PInt(splitField[0]);
						if(percent<0 || percent>100) {
							break;
						}
						ben=new Benefit();
						ben.BenefitType=InsBenefitType.Percentage;
						ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;
						ben.Percent=percent;
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
						benefitList.Add(ben.Copy());
						//does prosthodontics include crowns?
						break;
				}//switch
			}//for
			//now, save this all to the database.
			//carrier
			if(carrier.CarrierName==null || carrier.CarrierName==""){
				//if, for some reason, carrier is absent from the file, we can't do a thing with it.
				return 0;
			}
			//Carriers.Cur=carrier;
			Carriers.GetCurSame(carrier);
			//set calendar vs serviceyear
			if(usesAnnivers){
				for(int i=0;i<benefitList.Count;i++) {
					((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.ServiceYear;
				}
			}
			//plan
			plan.CarrierNum=carrier.CarrierNum;
			string command="SELECT PlanNum FROM insplan WHERE TrojanID='"+POut.PString(plan.TrojanID)+"'";
			DataTable table=General.GetTable(command);
			int planNum;
			for(int i=0;i<table.Rows.Count;i++){
				planNum=PIn.PInt(table.Rows[i][0].ToString());
				//update plan
				command="UPDATE insplan SET "
					+"EmployerNum='"+POut.PInt   (plan.EmployerNum)+"', "
					+"GroupName='"  +POut.PString(plan.GroupName)+"', "
					+"GroupNum='"   +POut.PString(plan.GroupNum)+"', "
					+"CarrierNum='" +POut.PInt   (plan.CarrierNum)+"', "
					+"BenefitNotes='"+POut.PString(plan.BenefitNotes)+"' "
					+"WHERE PlanNum="+POut.PInt(planNum);
				General.NonQ(command);
				//clear benefits
				command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNum);
				General.NonQ(command);
				//benefitList
				for(int j=0;j<benefitList.Count;j++) {
					((Benefit)benefitList[j]).PlanNum=planNum;
					Benefits.Insert((Benefit)benefitList[j]);
				}
				InsPlans.ComputeEstimatesForPlan(planNum);
			}
			return table.Rows.Count;
			//MessageBox.Show(plan.BenefitNotes);
		}





	}
}
