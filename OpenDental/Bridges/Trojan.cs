using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
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
		private static DataTable pendingDeletionTable;
		private static DataTable pendingDeletionTableTrojan;

		public static void StartupCheck(){
			//Skip all if not using Trojan.
			Program ProgramCur=Programs.GetCur("Trojan");
			if(!ProgramCur.Enabled) {
				return;
			}
			//Ensure that Trojan has a sane install.
			RegistryKey regKey=Registry.LocalMachine.OpenSubKey("Software\\TROJAN BENEFIT SERVICE");
			if(regKey==null||regKey.GetValue("INSTALLDIR")==null) {
				//The old trojan registry key is missing. Try to locate the new Trojan registry key.
				regKey=Registry.LocalMachine.OpenSubKey("Software\\Trojan Eligibility");
				if(regKey==null||regKey.GetValue("INSTALLDIR")==null) {//Unix OS will exit here.
					return;
				}
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
			Collection <string[]> records=new Collection<string[]>();
			for(int i=0;i<trojanplans.Length;i++) {
				string[] record=trojanplans[i].Split(new string[] {"\t"},StringSplitOptions.None);
				for(int j=0;j<record.Length;j++){
					//Remove any white space around the field and remove the surrounding quotes.
					record[j]=record[j].Trim().Substring(1);
					record[j]=record[j].Substring(0,record[j].Length-1);
				}
				records.Add(record);
				string whoToContact=record[3].ToUpper();
				if(whoToContact=="T"){
					deleteTrojanRecords.Add(record);
				}else{//whoToContact="P"
					deletePatientRecords.Add(record);
				}
			}
			if(deletePatientRecords.Count>0){
				//Get the list of records for the pending plan deletion report for plans that need to be brought to the patient's attention.
				string whereTrojanID="";
				for(int i=0;i<deletePatientRecords.Count;i++) {
					if(i>0) {
						whereTrojanID+="OR ";
					}
					whereTrojanID+="i.TrojanID='"+deletePatientRecords[i][0]+"' ";
				}
				string command="SELECT DISTINCT "+
					"p.FName,"+
					"p.LName,"+
					"p.FName,"+
					"p.LName,"+
					"p.SSN,"+
					"p.Birthdate,"+
					"i.GroupNum,"+
					"i.SubscriberID,"+
					"i.TrojanID,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.EmpName END,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.Phone END,"+
					"c.CarrierName,"+
					"c.Phone "+
					"FROM patient p,insplan i,employer e,carrier c "+
					"WHERE p.PatNum=i.Subscriber AND "+
					"("+whereTrojanID+") AND "+
					"i.CarrierNum=c.CarrierNum AND "+
					"(i.EmployerNum=e.EmployerNum OR i.EmployerNum=0) AND "+
					"(SELECT COUNT(*) FROM patplan a WHERE a.PlanNum=i.PlanNum) > 0 "+
					"ORDER BY i.TrojanID,p.LName,p.FName";
				pendingDeletionTable=Db.GetTable(command);
				if(pendingDeletionTable.Rows.Count>0){
					FormPrintReport fpr=new FormPrintReport();
					fpr.Text="Trojan Plans Pending Deletion: Contact Patients";
					fpr.ScrollAmount=10;
					fpr.printGenerator=ShowPendingDeletionReportForPatients;
					fpr.UsePageNumbers(new Font(FontFamily.GenericMonospace,8));
					fpr.MinimumTimesToPrint=1;
					fpr.ShowDialog();
				}
			}
			if(deleteTrojanRecords.Count>0) {
				//Get the list of records for the pending plan deletion report for plans which need to be bought to Trojan's attention.
				string whereTrojanID="";
				for(int i=0;i<deleteTrojanRecords.Count;i++) {
					if(i>0) {
						whereTrojanID+="OR ";
					}
					whereTrojanID+="i.TrojanID='"+deleteTrojanRecords[i][0]+"' ";
				}
				string command="SELECT DISTINCT "+
					"p.FName,"+
					"p.LName,"+
					"p.FName,"+
					"p.LName,"+
					"p.SSN,"+
					"p.Birthdate,"+
					"i.GroupNum,"+
					"i.SubscriberID,"+
					"i.TrojanID,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.EmpName END,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.Phone END,"+
					"c.CarrierName,"+
					"c.Phone "+
					"FROM patient p,insplan i,employer e,carrier c "+
					"WHERE p.PatNum=i.Subscriber AND "+
					"("+whereTrojanID+") AND "+
					"i.CarrierNum=c.CarrierNum AND "+
					"(i.EmployerNum=e.EmployerNum OR i.EmployerNum=0) AND "+
					"(SELECT COUNT(*) FROM patplan a WHERE a.PlanNum=i.PlanNum) > 0 "+
					"ORDER BY i.TrojanID,p.LName,p.FName";
				pendingDeletionTableTrojan=Db.GetTable(command);
				if(pendingDeletionTableTrojan.Rows.Count>0) {
					FormPrintReport fpr=new FormPrintReport();
					fpr.Text="Trojan Plans Pending Deletion: Contact Trojan";
					fpr.ScrollAmount=10;
					fpr.printGenerator=ShowPendingDeletionReportForTrojan;
					fpr.UsePageNumbers(new Font(FontFamily.GenericMonospace,8));
					fpr.MinimumTimesToPrint=1;
					fpr.Landscape=true;
					fpr.ShowDialog();
				}
			}
			//Now that the plans have been reported, drop the plans that are marked finally deleted.
			for(int i=0;i<records.Count;i++){
				if(records[i][1]=="F") {
					try {
						InsPlan[] insplans=InsPlans.GetByTrojanID(records[i][0]);
						for(int j=0;j<insplans.Length;j++) {
							insplans[j].PlanNote="PLAN DROPED BY TROJAN"+Environment.NewLine+insplans[j].PlanNote;
							insplans[j].TrojanID="";
							InsPlans.Update(insplans[j]);
							PatPlan[] patplans=PatPlans.GetByPlanNum(insplans[j].PlanNum);
							for(int k=0;k<patplans.Length;k++) {
								PatPlans.Delete(patplans[k].PatPlanNum);
							}
						}
					} catch(ApplicationException ex) {
						MessageBox.Show(ex.Message);
						return;
					}
				}
			}
			File.Delete(file);
		}

		private static void ShowPendingDeletionReportForPatients(FormPrintReport fpr){
			//Print the header on the report.
			Font font=new Font(FontFamily.GenericMonospace,12);
			string text=PrefC.GetString("PracticeTitle");
			SizeF size=fpr.Graph.MeasureString(text,font);
			float y=20;
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
			text="INSTRUCTIONS: These plans no longer exist, please do not contact Trojan. Please contact your patient for current benefit information.";
			fpr.Graph.DrawString(text,new Font(font,FontStyle.Bold),Brushes.Black,new RectangleF(0,y,650,500));
			y+=70;//Skip a line or so.
			text="Patient&Insured";
			font=new Font(font.FontFamily,9);
			fpr.Graph.DrawString(text,font,Brushes.Black,20,y);
			text="TrojanID";
			fpr.Graph.DrawString(text,font,Brushes.Black,240,y);
			text="Employer";
			fpr.Graph.DrawString(text,font,Brushes.Black,330,y);
			text="Carrier";
			fpr.Graph.DrawString(text,font,Brushes.Black,500,y);
			y+=20;
			//Use a static height for the records, to keep the math simple.
			float recordHeight=140;
			float recordSpacing=10;
			//Calculate the total number of pages in the report the first time this function is called only.
			if(fpr.TotalPages==0){
				fpr.TotalPages=(int)Math.Ceiling((y+recordHeight*pendingDeletionTable.Rows.Count+
					((pendingDeletionTable.Rows.Count>1)?pendingDeletionTable.Rows.Count-1:0)*recordSpacing)/fpr.PageHeight);
			}
			float pageBoundry=fpr.PageHeight;
			for(int i=0;i<pendingDeletionTable.Rows.Count;i++){
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
					PIn.PString(pendingDeletionTable.Rows[i][0].ToString())+" "+PIn.PString(pendingDeletionTable.Rows[i][1].ToString())+Environment.NewLine+
					PIn.PString(pendingDeletionTable.Rows[i][2].ToString())+" "+PIn.PString(pendingDeletionTable.Rows[i][3].ToString())+Environment.NewLine+
					" SSN: "+PIn.PString(pendingDeletionTable.Rows[i][4].ToString())+Environment.NewLine+
					" Birth: "+PIn.PDate(pendingDeletionTable.Rows[i][5].ToString()).ToShortDateString()+Environment.NewLine+
					" Group: "+PIn.PString(pendingDeletionTable.Rows[i][6].ToString()),font,Brushes.Black,
					new RectangleF(20,y+5,215,95));
				//Pending deletion reason.
				for(int j=0;j<deletePatientRecords.Count;j++) {
					if(deletePatientRecords[j][0]==PIn.PString(pendingDeletionTable.Rows[i][8].ToString())) {
						text="REASON FOR DELETION: "+deletePatientRecords[j][7];
						if(deletePatientRecords[j][1].ToUpper()=="F"){
							text="FINALLY DELETED"+Environment.NewLine+text;
						}
						fpr.Graph.DrawString(text,font,Brushes.Black,
							new RectangleF(20,y+100,fpr.GraphWidth-40,40));
						break;
					}
				}
				//Trojan ID.
				fpr.Graph.DrawString(PIn.PString(pendingDeletionTable.Rows[i][8].ToString()),font,Brushes.Black,new RectangleF(240,y+5,85,95));
				//Employer Name and Phone.
				fpr.Graph.DrawString(PIn.PString(pendingDeletionTable.Rows[i][9].ToString())+Environment.NewLine+
					PIn.PString(pendingDeletionTable.Rows[i][10].ToString()),font,Brushes.Black,new RectangleF(330,y+5,170,95));
				//Carrier Name and Phone
				fpr.Graph.DrawString(PIn.PString(pendingDeletionTable.Rows[i][11].ToString())+Environment.NewLine+
					PIn.PString(pendingDeletionTable.Rows[i][12].ToString()),font,Brushes.Black,
					new RectangleF(500,y+5,150,95));
				//Leave space between records.
				y+=recordHeight+recordSpacing;
				//Watch out for the bottom of each page for the next record.
				if(y+recordHeight>pageBoundry) {
					y=pageBoundry+fpr.MarginBottom+20;
					pageBoundry+=fpr.PageHeight+fpr.MarginBottom;
					text="Patient&Insured";
					font=new Font(font.FontFamily,9);
					fpr.Graph.DrawString(text,font,Brushes.Black,20,y);
					text="TrojanID";
					fpr.Graph.DrawString(text,font,Brushes.Black,240,y);
					text="Employer";
					fpr.Graph.DrawString(text,font,Brushes.Black,330,y);
					text="Carrier";
					fpr.Graph.DrawString(text,font,Brushes.Black,500,y);
					y+=20;
				}
			}
		}

		private static void ShowPendingDeletionReportForTrojan(FormPrintReport fpr) {
			//Print the header on the report.
			Font font=new Font(FontFamily.GenericMonospace,12);
			string text=PrefC.GetString("PracticeTitle");
			SizeF size=fpr.Graph.MeasureString(text,font);
			float y=20;
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth/2-size.Width/2,y);
			text=DateTime.Today.ToShortDateString();
			size=fpr.Graph.MeasureString(text,font);
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth-size.Width,y);
			y+=size.Height;
			text="PLANS PENDING DELETION: Please Fax or Mail to Trojan";
			size=fpr.Graph.MeasureString(text,font);
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth/2-fpr.Graph.MeasureString(text,font).Width/2,y);
			y+=size.Height;
			text="Fax: 800-232-9788";
			size=fpr.Graph.MeasureString(text,font);
			fpr.Graph.DrawString(text,font,Brushes.Black,fpr.GraphWidth/2-fpr.Graph.MeasureString(text,font).Width/2,y);
			y+=size.Height;
			y+=20;//Skip a line or so.
			text="INSTRUCTIONS: Please complete the information requested below to help Trojan research these plans.\n"+
				"Active Patient: \"Yes\" means the patient has been in the office within the past 6 to 8 months.\n"+
				"Correct Employer: \"Yes\" means the insured currently is insured through this employer.\n"+
				"Correct Carrier: \"Yes\" means the insured currently has coverage with this carrier.";
			fpr.Graph.DrawString(text,new Font(new Font(font.FontFamily,10),FontStyle.Bold),Brushes.Black,new RectangleF(0,y,900,500));
			y+=85;//Skip a line or so.
			font=new Font(font.FontFamily,9);
			text="Active\nPatient?";
			fpr.Graph.DrawString(text,font,Brushes.Black,5,y);
			text="\nPatient&Insured";
			fpr.Graph.DrawString(text,font,Brushes.Black,80,y);
			text="\nTrojanID";
			fpr.Graph.DrawString(text,font,Brushes.Black,265,y);
			text="Correct\nEmployer?";
			fpr.Graph.DrawString(text,font,Brushes.Black,345,y);
			text="\nEmployer";
			fpr.Graph.DrawString(text,font,Brushes.Black,420,y);
			text="Correct\nCarrier?";
			fpr.Graph.DrawString(text,font,Brushes.Black,600,y);
			text="\nCarrier";
			fpr.Graph.DrawString(text,font,Brushes.Black,670,y);
			y+=30;
			//Use a static height for the records, to keep the math simple.
			float recordHeight=200;
			float recordSpacing=10;
			//Calculate the total number of pages in the report the first time this function is called only.
			if(fpr.TotalPages==0) {
				fpr.TotalPages=(int)Math.Ceiling((y+recordHeight*pendingDeletionTableTrojan.Rows.Count+
					((pendingDeletionTableTrojan.Rows.Count>1)?pendingDeletionTableTrojan.Rows.Count-1:0)*recordSpacing)/fpr.PageHeight);
			}
			float pageBoundry=fpr.PageHeight;
			for(int i=0;i<pendingDeletionTableTrojan.Rows.Count;i++) {
				//Draw the outlines around this record.
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y),new PointF(fpr.GraphWidth-1,y));
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y+recordHeight),new PointF(fpr.GraphWidth-1,y+recordHeight));
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y),new PointF(0,y+recordHeight));
				fpr.Graph.DrawLine(Pens.Black,new PointF(fpr.GraphWidth-1,y),new PointF(fpr.GraphWidth-1,y+recordHeight));
				fpr.Graph.DrawLine(Pens.Black,new PointF(0,y+recordHeight-40),new PointF(fpr.GraphWidth-1,y+recordHeight-40));
				fpr.Graph.DrawLine(Pens.Black,new PointF(260,y),new PointF(260,y+recordHeight-40));
				fpr.Graph.DrawLine(Pens.Black,new PointF(340,y),new PointF(340,y+recordHeight-40));
				fpr.Graph.DrawLine(Pens.Black,new PointF(595,y),new PointF(595,y+recordHeight-40));
				//Patient active boxes.
				text="Yes No";
				fpr.Graph.DrawString(text,font,Brushes.Black,10,y);
				fpr.Graph.DrawRectangle(Pens.Black,new Rectangle(15,(int)(y+15),15,15));
				fpr.Graph.DrawRectangle(Pens.Black,new Rectangle(40,(int)(y+15),15,15));
				//Install the information for the record into the outline box.
				//Patient name, Guarantor name, guarantor SSN, guarantor birthdate, insurance plan group number,
				//and reason for pending deletion.
				fpr.Graph.DrawString(
					PIn.PString(pendingDeletionTableTrojan.Rows[i][0].ToString())+" "+PIn.PString(pendingDeletionTableTrojan.Rows[i][1].ToString())+Environment.NewLine+
					PIn.PString(pendingDeletionTableTrojan.Rows[i][2].ToString())+" "+PIn.PString(pendingDeletionTableTrojan.Rows[i][3].ToString())+Environment.NewLine+
					" SSN: "+PIn.PString(pendingDeletionTableTrojan.Rows[i][4].ToString())+Environment.NewLine+
					" Birth: "+PIn.PDate(pendingDeletionTableTrojan.Rows[i][5].ToString()).ToShortDateString()+Environment.NewLine+
					" Group: "+PIn.PString(pendingDeletionTableTrojan.Rows[i][6].ToString()),font,Brushes.Black,
					new RectangleF(80,y+5,185,95));
				//Pending deletion reason.
				for(int j=0;j<deleteTrojanRecords.Count;j++) {
					if(deleteTrojanRecords[j][0]==PIn.PString(pendingDeletionTableTrojan.Rows[i][8].ToString())) {
						text="REASON FOR DELETION: "+deleteTrojanRecords[j][7];
						if(deleteTrojanRecords[j][1].ToUpper()=="F"){
							text="FINALLY DELETED"+Environment.NewLine+text;
						}
						fpr.Graph.DrawString(text,font,Brushes.Black,
							new RectangleF(5,y+recordHeight-40,fpr.GraphWidth-10,40));
						break;
					}
				}
				//Trojan ID.
				fpr.Graph.DrawString(PIn.PString(pendingDeletionTableTrojan.Rows[i][8].ToString()),font,Brushes.Black,new RectangleF(265,y+5,85,95));
				//Correct Employer boxes and arrow.
				text="Yes No";
				fpr.Graph.DrawString(text,font,Brushes.Black,345,y);
				fpr.Graph.DrawRectangle(Pens.Black,new Rectangle(350,(int)(y+15),15,15));
				fpr.Graph.DrawRectangle(Pens.Black,new Rectangle(375,(int)(y+15),15,15));
				//Employer Name and Phone.
				fpr.Graph.DrawString(PIn.PString(pendingDeletionTableTrojan.Rows[i][9].ToString())+Environment.NewLine+
					PIn.PString(pendingDeletionTableTrojan.Rows[i][10].ToString()),font,Brushes.Black,new RectangleF(420,y+5,175,95));
				//New employer information if necessary.
				text="New\nEmployer:";
				fpr.Graph.DrawString(text,font,Brushes.Black,345,y+85);
				fpr.Graph.DrawLine(Pens.Black,415,y+110,590,y+110);
				fpr.Graph.DrawLine(Pens.Black,415,y+125,590,y+125);
				text="Phone:";
				fpr.Graph.DrawString(text,font,Brushes.Black,345,y+130);
				fpr.Graph.DrawLine(Pens.Black,415,y+140,590,y+140);
				//Correct Carrier boxes and arrow.
				text="Yes No";
				fpr.Graph.DrawString(text,font,Brushes.Black,600,y);
				fpr.Graph.DrawRectangle(Pens.Black,new Rectangle(605,(int)(y+15),15,15));
				fpr.Graph.DrawRectangle(Pens.Black,new Rectangle(630,(int)(y+15),15,15));
				//Carrier Name and Phone
				fpr.Graph.DrawString(PIn.PString(pendingDeletionTableTrojan.Rows[i][11].ToString())+Environment.NewLine+
					PIn.PString(pendingDeletionTableTrojan.Rows[i][12].ToString()),font,Brushes.Black,
					new RectangleF(670,y+5,225,95));
				//New carrier information if necessary.
				text="New\nCarrier:";
				fpr.Graph.DrawString(text,font,Brushes.Black,600,y+85);
				fpr.Graph.DrawLine(Pens.Black,670,y+110,895,y+110);
				fpr.Graph.DrawLine(Pens.Black,670,y+125,895,y+125);
				text="Phone:";
				fpr.Graph.DrawString(text,font,Brushes.Black,600,y+130);
				fpr.Graph.DrawLine(Pens.Black,670,y+140,895,y+140);
				//Leave space between records.
				y+=recordHeight+recordSpacing;
				//Watch out for the bottom of each page for the next record.
				if(y+recordHeight>pageBoundry) {
					y=pageBoundry+fpr.MarginBottom+20;
					pageBoundry+=fpr.PageHeight+fpr.MarginBottom;
					text="Active\nPatient?";
					fpr.Graph.DrawString(text,font,Brushes.Black,5,y);
					text="\nPatient&Insured";
					fpr.Graph.DrawString(text,font,Brushes.Black,80,y);
					text="\nTrojanID";
					fpr.Graph.DrawString(text,font,Brushes.Black,265,y);
					text="Correct\nEmployer?";
					fpr.Graph.DrawString(text,font,Brushes.Black,345,y);
					text="\nEmployer";
					fpr.Graph.DrawString(text,font,Brushes.Black,420,y);
					text="Correct\nCarrier?";
					fpr.Graph.DrawString(text,font,Brushes.Black,600,y);
					text="\nCarrier";
					fpr.Graph.DrawString(text,font,Brushes.Black,670,y);
					y+=30;
				}
			}
		}

		///<summary>Process existing insurance plan updates from the ALLPLANS.TXT file.</summary>
		private static void ProcessTrojanPlanUpdates(string file){
			if(!File.Exists(file)) {
				//Nothing to process.
				return;
			}
			MessageBox.Show("Trojan update found.  Please print the text file when it opens, then close it.  You will be given a chance to cancel the update after that.");
			Process.Start(file);
			if(!MsgBox.Show("Trojan",true,"Trojan update found.  All plans will now be updated.")) {
				return;
			}
			string allplantext="";
			using(StreamReader sr=new StreamReader(file)) {
				allplantext=sr.ReadToEnd();
			}
			if(allplantext=="") {
				MessageBox.Show("Could not read file contents: "+file);
				return;
			}
			string[] trojanplans=allplantext.Split(new string[] { "TROJANID" },StringSplitOptions.RemoveEmptyEntries);
			int plansAffected=0;
			for(int i=0;i<trojanplans.Length;i++) {
				trojanplans[i]="TROJANID"+trojanplans[i];
				plansAffected+=ProcessTrojanPlan(trojanplans[i]);
			}
			MessageBox.Show(plansAffected.ToString()+" plans updated.");
			try{
				File.Delete(file);
			}
			catch{
				MessageBox.Show(file+" could not be deleted.  Please delete manually.");
			}
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
						if(CovCatC.ListShort.Count>0) {
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
			DataTable table=Db.GetTable(command);
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
				Db.NonQ(command);
				//clear benefits
				command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNum);
				Db.NonQ(command);
				//benefitList
				for(int j=0;j<benefitList.Count;j++) {
					((Benefit)benefitList[j]).PlanNum=planNum;
					Benefits.Insert((Benefit)benefitList[j]);
				}
				InsPlanL.ComputeEstimatesForPlan(planNum);
			}
			return table.Rows.Count;
			//MessageBox.Show(plan.BenefitNotes);
		}





	}
}
