using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenDentBusiness;
using System.Drawing;
using System.Collections.ObjectModel;
using CodeBase;

namespace OpenDental.Bridges {
	class Trojan {

		private static Collection<string[]> deletePatientRecords;
		private static Collection<string[]> deleteTrojanRecords;

		public static void StartupCheck(){
			//Skip all if not using Trojan.
			Program ProgramCur=Programs.GetCur("Trojan");
			if(!ProgramCur.Enabled){
				return;
			}
			//Ensure that Trojan has a sane install.
			RegistryKey regKey=Registry.LocalMachine.OpenSubKey("Software\\TROJAN BENEFIT SERVICE");
			if(regKey==null){//Unix OS will exit here.
				//MessageBox.Show("Trojan not installed properly.");
				return;
			}
			//Process DELETEDPLANS.TXT for recently deleted insurance plans.
			string file=regKey.GetValue("INSTALLDIR").ToString()+@"\DELETEDPLANS.TXT";//C:\ETW\DELETEDPLANS.TXT
			ProcessDeletedPlans(file);
			//Process ALLPLANS.TXT for new insurance plan information.
			file=regKey.GetValue("INSTALLDIR").ToString()+@"\ALLPLANS.TXT";//C:\ETW\ALLPLANS.TXT
			ProcessTrojanPlanUpdates(file);
		}

		///<summary>Process the deletion of existing insurance plans.</summary>
		private static void ProcessDeletedPlans(string file){
			if(!File.Exists(file)) {
				//Nothing to process.
				return;
			}
			string deleteplantext=File.ReadAllText(file);
			if(deleteplantext=="") {
				//Nothing to process. Don't delete the file in-case Trojan is filling the file right now.
				return;
			}
			deletePatientRecords=new Collection<string[]>();
			deleteTrojanRecords=new Collection<string[]>();
			string[] trojanplans=deleteplantext.Split(new string[] { "\n" },StringSplitOptions.RemoveEmptyEntries);
			for(int i=0;i<trojanplans.Length;i++) {
				string[] record=trojanplans[i].Split(new string[] {"\t"},StringSplitOptions.None);
				for(int j=0;j<record.Length;j++){
					//Remove any white space around the field and remove the surrounding quotes.
					record[j]=record[j].Trim().Substring(1);
					record[j]=record[j].Substring(0,record[j].Length-1);
				}
				string whoToContact=record[3].ToUpper();
				string deleteType=record[1].ToUpper();
				if(deleteType=="F"){
					//TODO: delete the insurance plan if it exists.
					continue;//Do not report the deleted plan on the pending deletion reports.
				}
				if(whoToContact=="T"){
					deleteTrojanRecords.Add(record);
				}else{//whoToContact="P"
					deletePatientRecords.Add(record);
				}
			}
			FormPrintReport fpr=new FormPrintReport();
			fpr.Text="Trojan Plans Pending Deletion";
			fpr.printGenerator=ShowPendingDeletionReportForPatients;
			fpr.ShowDialog();
			//File.Delete(file);//TODO: uncomment!!!
		}

		private static void ShowPendingDeletionReportForPatients(FormPrintReport fpr){
			if(deletePatientRecords.Count<1){
				return;//Nothing to report.
			}
			//Print the header on the report.
			Font font=new Font(FontFamily.GenericMonospace,12);
			string text=PrefB.GetString("PracticeTitle");
			SizeF size=fpr.Graph.MeasureString(text,font);
			float y=0;
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth/2-size.Width/2,y);
			text=DateTime.Today.ToShortDateString();
			size=fpr.Graph.MeasureString(text,font);
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth-size.Width,y);
			y+=size.Height;
			text="PLANS PENDING DELETION WHICH REQUIRE YOUR ATTENTION";
			size=fpr.Graph.MeasureString(text,font);
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth/2-fpr.Graph.MeasureString(text,font).Width/2,y);
			y+=size.Height;
			y+=20;//Skip a line or so.
			text="Patient&Insured";
			font=new Font(font.FontFamily,11);
			fpr.Graph.DrawString(text,font,Brushes.Black,20,y);
			text="TrojanID";
			fpr.Graph.DrawString(text,font,Brushes.Black,240,y);
			text="Employer";
			fpr.Graph.DrawString(text,font,Brushes.Black,330,y);
			text="Carrier";
			fpr.Graph.DrawString(text,font,Brushes.Black,500,y);
			y+=20;
			string whereTrojanID="";
			for(int i=0;i<deletePatientRecords.Count;i++){
				if(i>0){
					whereTrojanID+="OR ";
				}
				whereTrojanID+="i.TrojanID='"+deletePatientRecords[i][0]+"' ";
			}
			string command="SELECT DISTINCT "+
				"p.FName,"+
				"p.LName,"+
				"g.FName,"+
				"g.LName,"+
				"g.SSN,"+
				"g.Birthdate,"+
				"i.GroupNum,"+
				"i.SubscriberID,"+
				"i.TrojanID,"+
				"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.EmpName END,"+
				"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.Phone END,"+
				"c.CarrierName,"+
				"c.Phone,"+
				"c.ElectID "+
				"FROM patient p,patient g,insplan i,employer e,carrier c "+
				"WHERE p.PatNum=i.Subscriber AND "+
				"("+whereTrojanID+") AND "+
				"i.CarrierNum=c.CarrierNum AND "+
				"(i.EmployerNum=e.EmployerNum OR i.EmployerNum=0) AND "+
				"p.Guarantor=g.PatNum";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count<1){
				return;//Nothing to report.
			}
			//Use a static height for the records, to keep the math simple.
			float recordHeight=140;
			float recordSpacing=10;
			fpr.TotalPages=(int)Math.Ceiling((y+recordHeight*table.Rows.Count+
				((table.Rows.Count>1)?table.Rows.Count-1:0)*recordSpacing)/fpr.pageHeight);
			for(int i=0;i<table.Rows.Count;i++){
				//Draw the outlines around this record.
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y),new PointF(fpr.GraphWidth-1,y));
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y+recordHeight),new PointF(fpr.GraphWidth-1,y+recordHeight));
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y),new PointF(0,y+recordHeight));
				fpr.Graph.DrawLine(Pens.Black,new PointF(fpr.GraphWidth-1,y),new PointF(fpr.GraphWidth-1,y+recordHeight));
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y+recordHeight-40),new PointF(fpr.GraphWidth-1,y+recordHeight-40));
				fpr.Graph.DrawLine(Pens.Black,new PointF(235,y),new PointF(235,y+recordHeight-40));
				fpr.Graph.DrawLine(Pens.Black,new PointF(325,y),new PointF(325,y+recordHeight-40));
				fpr.Graph.DrawLine(Pens.Black,new PointF(500,y),new PointF(500,y+recordHeight-40));
				//Install the information for the record into the outline box.
				//Patient name, Guarantor name, guarantor SSN, guarantor birthdate, insurance plan group number,
				//and reason for pending deletion.
				fpr.Graph.DrawString(
					PIn.PString(table.Rows[i][0].ToString())+" "+PIn.PString(table.Rows[i][1].ToString())+Environment.NewLine+
					PIn.PString(table.Rows[i][2].ToString())+" "+PIn.PString(table.Rows[i][3].ToString())+Environment.NewLine+
					" SSN: "+PIn.PString(table.Rows[i][4].ToString())+Environment.NewLine+
					" Birth: "+PIn.PDate(table.Rows[i][5].ToString()).ToShortDateString()+Environment.NewLine+
					" Group: "+PIn.PString(table.Rows[i][6].ToString()),font,Brushes.Black,
					new RectangleF(20,y+5,215,95));
				//Pending deletion reason.
				for(int j=0;j<deletePatientRecords.Count;j++) {
					if(deletePatientRecords[j][0]==PIn.PString(table.Rows[i][8].ToString())) {
						fpr.Graph.DrawString("REASON FOR DELETION: "+deletePatientRecords[j][7],font,Brushes.Black,
							new RectangleF(20,y+100,fpr.GraphWidth-40,40));
						break;
					}
				}
				//Trojan ID.
				fpr.Graph.DrawString(PIn.PString(table.Rows[i][8].ToString()),font,Brushes.Black,new RectangleF(240,y+5,85,95));
				//Employer Name and Phone.
				fpr.Graph.DrawString(PIn.PString(table.Rows[i][9].ToString())+Environment.NewLine+
					PIn.PString(table.Rows[i][10].ToString()),font,Brushes.Black,new RectangleF(330,y+5,170,95));
				//Carrier Name, Phone and Electronic ID.
				fpr.Graph.DrawString(PIn.PString(table.Rows[i][11].ToString())+Environment.NewLine+
					PIn.PString(table.Rows[i][12].ToString())+Environment.NewLine+
					PIn.PString(table.Rows[i][13].ToString()),font,Brushes.Black,
					new RectangleF(500,y+5,150,95));
				//Leave space between records.
				y+=recordHeight+recordSpacing;
			}
		}

		///<summary>Process existing insurance plan updates from the ALLPLANS.TXT file.</summary>
		private static void ProcessTrojanPlanUpdates(string file){
			if(!File.Exists(file)) {
				//Nothing to process.
				return;
			}
			if(!MsgBox.Show("Trojan",true,"Trojan update found.  All plans will now be updated.")) {
				return;
			}
			string allplantext="";
			using(StreamReader sr=new StreamReader(file)) {
				allplantext=sr.ReadToEnd();
			}
			if(allplantext=="") {
				return;
			}
			string[] trojanplans=allplantext.Split(new string[] { "TROJANID" },StringSplitOptions.RemoveEmptyEntries);
			int plansAffected=0;
			for(int i=0;i<trojanplans.Length;i++) {
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
