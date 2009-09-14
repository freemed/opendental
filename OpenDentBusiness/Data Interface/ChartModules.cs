using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class ChartModules {
		private static DataTable rawApt;

		public static DataSet GetAll(long patNum,bool isAuditMode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),patNum,isAuditMode);
			} 
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetProgNotes(patNum,isAuditMode));
			retVal.Tables.Add(GetPlannedApt(patNum));
			return retVal;
		}

		public static DataTable GetProgNotes(long patNum,bool isAuditMode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum,isAuditMode);
			}
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("ProgNotes");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("aptDateTime",typeof(DateTime));
			table.Columns.Add("AptNum");
			table.Columns.Add("CodeNum");
			table.Columns.Add("colorBackG");
			table.Columns.Add("colorText");
			table.Columns.Add("CommlogNum");
			table.Columns.Add("description");
			table.Columns.Add("dx");
			table.Columns.Add("Dx");
			table.Columns.Add("EmailMessageNum");
			table.Columns.Add("LabCaseNum");
			table.Columns.Add("note");
			table.Columns.Add("PatNum");//only used for Commlog and Task
			table.Columns.Add("Priority");
			table.Columns.Add("ProcCode");
			table.Columns.Add("procDate");
			table.Columns.Add("ProcDate",typeof(DateTime));
			table.Columns.Add("procFee");
			table.Columns.Add("ProcNum");
			table.Columns.Add("ProcNumLab");
			table.Columns.Add("procStatus");
			table.Columns.Add("ProcStatus");
			table.Columns.Add("procTime");
			table.Columns.Add("prov");
			table.Columns.Add("RxNum");
			table.Columns.Add("SheetNum");
			table.Columns.Add("signature");
			table.Columns.Add("Surf");
			table.Columns.Add("TaskNum");
			table.Columns.Add("toothNum");
			table.Columns.Add("ToothNum");
			table.Columns.Add("ToothRange");
			table.Columns.Add("user");
			//table.Columns.Add("");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//Procedures-----------------------------------------------------------------------------------------------------
			string command="SELECT LaymanTerm,ProcDate,ProcStatus,ToothNum,Surf,Dx,UnitQty,procedurelog.BaseUnits,"
				+"procedurecode.ProcCode,ProcNum,procedurecode.Descript,"
				+"provider.Abbr,ProcFee,ProcNumLab,appointment.AptDateTime,Priority,ToothRange,procedurelog.CodeNum "
				+"FROM procedurelog "
				+"LEFT JOIN procedurecode ON procedurecode.CodeNum=procedurelog.CodeNum "
				+"LEFT JOIN provider ON provider.ProvNum=procedurelog.ProvNum "
				+"LEFT JOIN appointment ON appointment.AptNum=procedurelog.AptNum "
				+"AND (appointment.AptStatus="+POut.PLong((int)ApptStatus.Scheduled)
				+" OR appointment.AptStatus="+POut.PLong((int)ApptStatus.ASAP)
				+" OR appointment.AptStatus="+POut.PLong((int)ApptStatus.Broken)
				+" OR appointment.AptStatus="+POut.PLong((int)ApptStatus.Complete)
				+") WHERE procedurelog.PatNum="+POut.PLong(patNum);
			if(!isAuditMode) {
				command+=" AND ProcStatus !=6";//don't include deleted
			}
			command+=" ORDER BY ProcDate";//we'll just have to reorder it anyway
			DataTable rawProcs=dcon.GetTable(command);
			command="SELECT ProcNum,EntryDateTime,UserNum,Note,"
				+"CASE WHEN Signature!='' THEN 1 ELSE 0 END AS SigPresent "
				+"FROM procnote WHERE PatNum="+POut.PLong(patNum)
				+" ORDER BY EntryDateTime";// but this helps when looping for notes
			DataTable rawNotes=dcon.GetTable(command);
			DateTime dateT;
			List<DataRow> labRows=new List<DataRow>();//Canadian lab procs, which must be added in a loop at the very end.
			for(int i=0;i<rawProcs.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=PIn.PDateT(rawProcs.Rows[i]["AptDateTime"].ToString());
				row["AptNum"]=0;
				row["CodeNum"]=rawProcs.Rows[i]["CodeNum"].ToString();
				row["colorBackG"]=Color.White.ToArgb();
				if(((DateTime)row["aptDateTime"]).Date==DateTime.Today) {
					row["colorBackG"]=DefC.Long[(int)DefCat.MiscColors][6].ItemColor.ToArgb().ToString();
				}
				switch((ProcStat)PIn.PLong(rawProcs.Rows[i]["ProcStatus"].ToString())) {
					case ProcStat.TP:
						row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][0].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.C:
						row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][1].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.EC:
						row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][2].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.EO:
						row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][3].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.R:
						row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][4].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.D:
						row["colorText"]=Color.Black.ToArgb().ToString();
						break;
					case ProcStat.Cn:
						row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][22].ItemColor.ToArgb().ToString();
						break;
				}
				row["CommlogNum"]=0;
				if(rawProcs.Rows[i]["LaymanTerm"].ToString()=="") {
					row["description"]=rawProcs.Rows[i]["Descript"].ToString();
				}
				else {
					row["description"]=rawProcs.Rows[i]["LaymanTerm"].ToString();
				}
				if(rawProcs.Rows[i]["ToothRange"].ToString()!="") {
					row["description"]+=" #"+Tooth.FormatRangeForDisplay(rawProcs.Rows[i]["ToothRange"].ToString());
				}
				row["dx"]=DefC.GetValue(DefCat.Diagnosis,PIn.PLong(rawProcs.Rows[i]["Dx"].ToString()));
				row["Dx"]=rawProcs.Rows[i]["Dx"].ToString();
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=0;
				//note-----------------------------------------------------------------------------------------------------------
				row["user"]="";
				row["note"]="";
				row["signature"]="";
				if(isAuditMode) {//we will include all notes for each proc.  We will concat and make readable.
					for(int n=0;n<rawNotes.Rows.Count;n++) {//loop through each note
						if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
							continue;
						}
						if(row["Note"].ToString()!="") {//if there is an existing note
							row["note"]+="\r\n------------------------------------------------------\r\n";//start a new line
						}
						row["note"]+=PIn.PDateT(rawNotes.Rows[n]["EntryDateTime"].ToString()).ToString();
						row["note"]+="  "+Userods.GetName(PIn.PLong(rawNotes.Rows[n]["UserNum"].ToString()));
						if(rawNotes.Rows[n]["SigPresent"].ToString()=="1") {
							row["note"]+="  "+Lans.g("ChartModule","(signed)");
						}
						row["note"]+="\r\n"+rawNotes.Rows[n]["Note"].ToString();
					}
				}
				else {//we just want the most recent note
					for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//loop through each note, backwards.
						if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
							continue;
						}
						row["user"]		 =Userods.GetName(PIn.PLong(rawNotes.Rows[n]["UserNum"].ToString()));
						row["note"]		 =rawNotes.Rows[n]["Note"].ToString();
						if(rawNotes.Rows[n]["SigPresent"].ToString()=="1") {
							row["signature"]=Lans.g("ChartModule","Signed");
						}
						else {
							row["signature"]="";
						}
						break;//out of note loop.
					}
				}
				row["PatNum"]="";
				row["Priority"]=rawProcs.Rows[i]["Priority"].ToString();
				row["ProcCode"]=rawProcs.Rows[i]["ProcCode"].ToString();
				dateT=PIn.PDateT(rawProcs.Rows[i]["ProcDate"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["ProcDate"]=dateT;
				double amt = PIn.PDouble(rawProcs.Rows[i]["ProcFee"].ToString());
				int qty = PIn.PInt(rawProcs.Rows[i]["UnitQty"].ToString()) + PIn.PInt(rawProcs.Rows[i]["BaseUnits"].ToString());
				if(qty>0) {
					amt *= qty;
				}
				row["procFee"]=amt.ToString("F");
				row["ProcNum"]=rawProcs.Rows[i]["ProcNum"].ToString();
				row["ProcNumLab"]=rawProcs.Rows[i]["ProcNumLab"].ToString();
				row["procStatus"]=Lans.g("enumProcStat",((ProcStat)PIn.PLong(rawProcs.Rows[i]["ProcStatus"].ToString())).ToString());
				row["ProcStatus"]=rawProcs.Rows[i]["ProcStatus"].ToString();
				row["procTime"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["prov"]=rawProcs.Rows[i]["Abbr"].ToString();
				row["RxNum"]=0;
				row["SheetNum"]=0;
				row["Surf"]=rawProcs.Rows[i]["Surf"].ToString();
				row["TaskNum"]=0;
				row["toothNum"]=Tooth.GetToothLabel(rawProcs.Rows[i]["ToothNum"].ToString());
				row["ToothNum"]=rawProcs.Rows[i]["ToothNum"].ToString();
				row["ToothRange"]=rawProcs.Rows[i]["ToothRange"].ToString();
				if(rawProcs.Rows[i]["ProcNumLab"].ToString()=="0") {//normal proc
					rows.Add(row);
				}
				else {
					row["description"]="-----"+row["description"].ToString();
					labRows.Add(row);//these will be added in the loop at the end
				}
			}
			//Commlog-----------------------------------------------------------------------------------------------------------
			command="SELECT CommlogNum,CommDateTime,CommType,Note,commlog.PatNum,UserNum,p1.FName "
				+"FROM patient p1,patient p2,commlog "
				+"WHERE commlog.PatNum=p1.PatNum "
				+"AND p1.Guarantor=p2.Guarantor "
				+"AND p2.PatNum="+POut.PLong(patNum)
				+" AND IsStatementSent=0 ORDER BY CommDateTime";
			DataTable rawComm=dcon.GetTable(command);
			string txt;
			for(int i=0;i<rawComm.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=0;
				row["CodeNum"]="";
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
				if(rawComm.Rows[i]["PatNum"].ToString()==patNum.ToString()) {
					txt="";
				}
				else {
					txt="("+rawComm.Rows[i]["FName"].ToString()+") ";
				}
				row["description"]=txt+Lans.g("ChartModule","Comm - ")
					+DefC.GetName(DefCat.CommLogTypes,PIn.PLong(rawComm.Rows[i]["CommType"].ToString()));
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=0;
				row["note"]=rawComm.Rows[i]["Note"].ToString();
				row["PatNum"]=rawComm.Rows[i]["PatNum"].ToString();
				row["Priority"]="";
				row["ProcCode"]="";
				dateT=PIn.PDateT(rawComm.Rows[i]["CommDateTime"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["ProcDate"]=dateT;
				row["procTime"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["RxNum"]=0;
				row["SheetNum"]=0;
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=0;
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]=Userods.GetName(PIn.PLong(rawComm.Rows[i]["UserNum"].ToString()));
				rows.Add(row);
			}
			//Rx------------------------------------------------------------------------------------------------------------------
			command="SELECT RxNum,RxDate,Drug,Disp,ProvNum,Notes,PharmacyNum FROM rxpat WHERE PatNum="+POut.PLong(patNum)
				+" ORDER BY RxDate";
			DataTable rawRx=dcon.GetTable(command);
			for(int i=0;i<rawRx.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=0;
				row["CodeNum"]="";
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][5].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=0;
				row["description"]=Lans.g("ChartModule","Rx - ")+rawRx.Rows[i]["Drug"].ToString()+" - #"+rawRx.Rows[i]["Disp"].ToString();
				if(rawRx.Rows[i]["PharmacyNum"].ToString()!="0") {
					row["description"]+="\r\n"+Pharmacies.GetDescription(PIn.PLong(rawRx.Rows[i]["PharmacyNum"].ToString()));
				}
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=0;
				row["note"]=rawRx.Rows[i]["Notes"].ToString();
				row["PatNum"]="";
				row["Priority"]="";
				row["ProcCode"]="";
				dateT=PIn.PDate(rawRx.Rows[i]["RxDate"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["ProcDate"]=dateT;
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["procTime"]="";
				row["prov"]="";
				//row["prov"]=ProviderB. PIn.PInt(rawRx.Rows[i]["ProvNum"].ToString());
				row["RxNum"]=rawRx.Rows[i]["RxNum"].ToString();
				row["SheetNum"]=0;
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=0;
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]="";
				rows.Add(row);
			}
			//LabCase------------------------------------------------------------------------------------------------------------------
			command="SELECT labcase.*,Description,Phone FROM labcase,laboratory "
				+"WHERE labcase.LaboratoryNum=laboratory.LaboratoryNum "
				+"AND PatNum="+POut.PLong(patNum)
				+" ORDER BY DateTimeCreated";
			DataTable rawLab=dcon.GetTable(command);
			DateTime duedate;
			for(int i=0;i<rawLab.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=0;
				row["CodeNum"]="";
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][7].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=0;
				row["description"]=Lans.g("ChartModule","LabCase - ")+rawLab.Rows[i]["Description"].ToString()+" "
					+rawLab.Rows[i]["Phone"].ToString();
				if(PIn.PDate(rawLab.Rows[i]["DateTimeDue"].ToString()).Year>1880) {
					duedate=PIn.PDateT(rawLab.Rows[i]["DateTimeDue"].ToString());
					row["description"]+="\r\n"+Lans.g("ChartModule","Due")+" "+duedate.ToString("ddd")+" "
						+duedate.ToShortDateString()+" "+duedate.ToShortTimeString();
				}
				if(PIn.PDate(rawLab.Rows[i]["DateTimeChecked"].ToString()).Year>1880) {
					row["description"]+="\r\n"+Lans.g("ChartModule","Quality Checked");
				}
				else if(PIn.PDate(rawLab.Rows[i]["DateTimeRecd"].ToString()).Year>1880) {
					row["description"]+="\r\n"+Lans.g("ChartModule","Received");
				}
				else if(PIn.PDate(rawLab.Rows[i]["DateTimeSent"].ToString()).Year>1880) {
					row["description"]+="\r\n"+Lans.g("ChartModule","Sent");
				}
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=rawLab.Rows[i]["LabCaseNum"].ToString();
				row["note"]=rawLab.Rows[i]["Instructions"].ToString();
				row["PatNum"]="";
				row["Priority"]="";
				row["ProcCode"]="";
				dateT=PIn.PDateT(rawLab.Rows[i]["DateTimeCreated"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["procTime"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["ProcDate"]=dateT;
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["RxNum"]=0;
				row["SheetNum"]=0;
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=0;
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]="";
				rows.Add(row);
			}
			//Task------------------------------------------------------------------------------------------------------------------
			command="SELECT task.*,tasklist.Descript ListDisc,p1.FName "
				+"FROM patient p1,patient p2, task,tasklist "
				+"WHERE task.KeyNum=p1.PatNum "
				+"AND task.TaskListNum=tasklist.TaskListNum "
				+"AND p1.Guarantor=p2.Guarantor "
				+"AND p2.PatNum="+POut.PLong(patNum)
				+" AND task.ObjectType=1 "
				+"ORDER BY DateTimeEntry";
			DataTable rawTask=dcon.GetTable(command);
			for(int i=0;i<rawTask.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=0;
				row["CodeNum"]="";
				//colors the same as notes
				row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][18].ItemColor.ToArgb().ToString();
				row["colorBackG"] = DefC.Long[(int)DefCat.ProgNoteColors][19].ItemColor.ToArgb().ToString();
				//row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();//same as commlog
				row["CommlogNum"]=0;
				if(rawTask.Rows[i]["KeyNum"].ToString()==patNum.ToString()) {
					txt="";
				}
				else {
					txt="("+rawTask.Rows[i]["FName"].ToString()+") ";
				}
				if(rawTask.Rows[i]["TaskStatus"].ToString()=="2") {//completed
					txt += Lans.g("ChartModule","Completed ");
					row["colorBackG"] = Color.White.ToArgb();
					//use same as note colors for completed tasks
					row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][20].ItemColor.ToArgb().ToString();
					row["colorBackG"] = DefC.Long[(int)DefCat.ProgNoteColors][21].ItemColor.ToArgb().ToString();
				}
				row["description"]=txt+Lans.g("ChartModule","Task - In List: ")+rawTask.Rows[i]["ListDisc"].ToString();
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=0;
				row["note"]=rawTask.Rows[i]["Descript"].ToString();
				row["PatNum"]=rawTask.Rows[i]["KeyNum"].ToString();
				row["Priority"]="";
				row["ProcCode"]="";
				dateT = PIn.PDateT(rawTask.Rows[i]["DateTask"].ToString());
				row["procTime"]="";
				if(dateT.Year < 1880) {//check if due date set for task or note
					dateT = PIn.PDateT(rawTask.Rows[i]["DateTimeEntry"].ToString());
					if(dateT.Year < 1880) {//since dateT was just redefined, check it now
						row["procDate"] = "";
					}
					else {
						row["procDate"] = dateT.ToShortDateString();
					}
					if(dateT.TimeOfDay != TimeSpan.Zero) {
						row["procTime"] = dateT.ToString("h:mm") + dateT.ToString("%t").ToLower();
					}
					row["ProcDate"] = dateT;
				}
				else {
					row["procDate"] =dateT.ToString(Lans.GetShortDateTimeFormat());
					if(dateT.TimeOfDay != TimeSpan.Zero) {
						row["procTime"] = dateT.ToString("h:mm") + dateT.ToString("%t").ToLower();
					}
					row["ProcDate"] = dateT;
					//row["Surf"] = "DUE";
				}
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["RxNum"]=0;
				row["SheetNum"]=0;
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=rawTask.Rows[i]["TaskNum"].ToString();
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]="";
				rows.Add(row);
			}
			//Appointments---------------------------------------------------------------------------------------------------------
			command="SELECT * FROM appointment WHERE PatNum="+POut.PLong(patNum)
				+" ORDER BY AptDateTime";
			//+" AND AptStatus != 6"//do not include planned appts.
			rawApt=dcon.GetTable(command);
			long apptStatus;
			for(int i=0;i<rawApt.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=rawApt.Rows[i]["AptNum"].ToString();
				row["colorBackG"]=Color.White.ToArgb();
				dateT=PIn.PDateT(rawApt.Rows[i]["AptDateTime"].ToString());
				apptStatus=PIn.PLong(rawApt.Rows[i]["AptStatus"].ToString());
				row["colorBackG"]="";
				row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][8].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=0;
				row["description"]=Lans.g("ChartModule","Appointment - ")+dateT.ToShortTimeString()+"\r\n"
					+rawApt.Rows[i]["ProcDescript"].ToString();
				if(dateT.Date.Date==DateTime.Today.Date) {
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][9].ItemColor.ToArgb().ToString(); //deliniates nicely between old appts
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][8].ItemColor.ToArgb().ToString();
				}
				else if(dateT.Date<DateTime.Today) {
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][11].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][10].ItemColor.ToArgb().ToString();
				}
				else if(dateT.Date>DateTime.Today) {
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][13].ItemColor.ToArgb().ToString(); //at a glace, you see green...the pt is good to go as they have a future appt scheduled
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][12].ItemColor.ToArgb().ToString();
				}
				if(apptStatus==(int)ApptStatus.Broken) {
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][14].ItemColor.ToArgb().ToString();
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][15].ItemColor.ToArgb().ToString();
					row["description"]=Lans.g("ChartModule","BROKEN Appointment - ")+dateT.ToShortTimeString()+"\r\n"
						+rawApt.Rows[i]["ProcDescript"].ToString();
				}
				else if(apptStatus==(int)ApptStatus.UnschedList) {
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][14].ItemColor.ToArgb().ToString();
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][15].ItemColor.ToArgb().ToString();
					row["description"]=Lans.g("ChartModule","UNSCHEDULED Appointment - ")+dateT.ToShortTimeString()+"\r\n"
						+rawApt.Rows[i]["ProcDescript"].ToString();
				}
				else if(apptStatus==(int)ApptStatus.Planned) {
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][16].ItemColor.ToArgb().ToString();
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][17].ItemColor.ToArgb().ToString();
					row["description"]=Lans.g("ChartModule","PLANNED Appointment")+"\r\n"
						+rawApt.Rows[i]["ProcDescript"].ToString();
				}
				else if(apptStatus==(int)ApptStatus.PtNote) {
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][18].ItemColor.ToArgb().ToString();
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][19].ItemColor.ToArgb().ToString();
					row["description"] = Lans.g("ChartModule","*** Patient NOTE  *** - ") + dateT.ToShortTimeString();
				}
				else if(apptStatus ==(int)ApptStatus.PtNoteCompleted) {
					row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][20].ItemColor.ToArgb().ToString();
					row["colorBackG"] = DefC.Long[(int)DefCat.ProgNoteColors][21].ItemColor.ToArgb().ToString();
					row["description"] = Lans.g("ChartModule","** Complete Patient NOTE ** - ") + dateT.ToShortTimeString();
				}
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=0;
				row["note"]=rawApt.Rows[i]["Note"].ToString();
				row["PatNum"]="";
				row["Priority"]="";
				row["ProcCode"]="";
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["procTime"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["ProcDate"]=dateT;
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["RxNum"]=0;
				row["SheetNum"]=0;
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=0;
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]="";
				rows.Add(row);
			}
			//email------------------------------------------------------------------------------------------------------------------------------
			command="SELECT EmailMessageNum,MsgDateTime,Subject,BodyText,PatNum,SentOrReceived "
				+"FROM emailmessage "
				+"WHERE PatNum="+POut.PLong(patNum)
				+" ORDER BY MsgDateTime";
			DataTable rawEmail=dcon.GetTable(command);
			for(int i=0;i<rawEmail.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=0;
				row["CodeNum"]="";
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();//needs to change
				row["CommlogNum"]=0;
				txt="";
				if(rawEmail.Rows[i]["SentOrReceived"].ToString()=="0") {
					txt=Lans.g("ChartModule","(unsent) ");
				}
				row["description"]=Lans.g("ChartModule","Email - ")+txt+rawEmail.Rows[i]["Subject"].ToString();
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=rawEmail.Rows[i]["EmailMessageNum"].ToString();
				row["LabCaseNum"]=0;
				row["note"]=rawEmail.Rows[i]["BodyText"].ToString();
				row["PatNum"]="";
				row["Priority"]="";
				row["ProcCode"]="";
				//row["PatNum"]=rawEmail.Rows[i]["PatNum"].ToString();
				dateT=PIn.PDateT(rawEmail.Rows[i]["msgDateTime"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["ProcDate"]=dateT;
				row["procTime"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["RxNum"]=0;
				row["SheetNum"]=0;
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=0;
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]="";
				rows.Add(row);
			}
			//sheet-----------------------------------------------------------------------------------------------------------------
			command="SELECT SheetNum,DateTimeSheet,SheetType "
				+"FROM sheet "
				+"WHERE PatNum="+POut.PLong(patNum)
				+" AND SheetType!="+POut.PLong((int)SheetTypeEnum.Rx)//rx are only accesssible from within Rx edit window.
				+" ORDER BY DateTimeSheet";
			DataTable rawSheet=dcon.GetTable(command);
			SheetTypeEnum sheetType;
			for(int i=0;i<rawSheet.Rows.Count;i++) {
				row=table.NewRow();
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=0;
				row["CodeNum"]="";
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=Color.Black.ToArgb();//DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();//needs to change
				row["CommlogNum"]=0;
				sheetType=(SheetTypeEnum)PIn.PLong(rawSheet.Rows[i]["SheetType"].ToString());
				row["description"]=Lans.g("SheetTypeEnum",sheetType.ToString());
				row["dx"]="";
				row["Dx"]="";
				row["EmailMessageNum"]=0;
				row["LabCaseNum"]=0;
				row["note"]="";
				row["PatNum"]="";
				row["Priority"]="";
				row["ProcCode"]="";
				dateT=PIn.PDateT(rawSheet.Rows[i]["DateTimeSheet"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
				}
				row["ProcDate"]=dateT;
				row["procTime"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["RxNum"]=0;
				row["SheetNum"]=rawSheet.Rows[i]["SheetNum"].ToString();
				row["signature"]="";
				row["Surf"]="";
				row["TaskNum"]=0;
				row["toothNum"]="";
				row["ToothNum"]="";
				row["ToothRange"]="";
				row["user"]="";
				rows.Add(row);
			}
			//Sorting
			rows.Sort(CompareChartRows);
			//Canadian lab procedures need to come immediately after their corresponding proc---------------------------------
			for(int i=0;i<labRows.Count;i++) {
				for(int r=0;r<rows.Count;r++) {
					if(rows[r]["ProcNum"].ToString()==labRows[i]["ProcNumLab"].ToString()) {
						rows.Insert(r+1,labRows[i]);
						break;
					}
				}
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		private static DataTable GetPlannedApt(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Planned");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("AptNum");
			table.Columns.Add("colorBackG");
			table.Columns.Add("colorText");
			table.Columns.Add("dateSched");
			table.Columns.Add("ItemOrder");
			table.Columns.Add("minutes");
			table.Columns.Add("Note");
			table.Columns.Add("ProcDescript");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT plannedappt.AptNum,ItemOrder,PlannedApptNum,appointment.AptDateTime,"
				+"appointment.Pattern,appointment.AptStatus "
				+"FROM plannedappt "
				+"LEFT JOIN appointment ON appointment.NextAptNum=plannedappt.AptNum "
				+"WHERE plannedappt.PatNum="+POut.PLong(patNum)+" "
				+"GROUP BY plannedappt.AptNum "
				+"ORDER BY ItemOrder";
			DataTable rawPlannedAppts=dcon.GetTable(command);
			DataRow aptRow;
			int itemOrder=1;
			DateTime dateSched;
			ApptStatus aptStatus;
			for(int i=0;i<rawPlannedAppts.Rows.Count;i++) {
				aptRow=null;
				for(int a=0;a<rawApt.Rows.Count;a++){
					if(rawApt.Rows[a]["AptNum"].ToString()==rawPlannedAppts.Rows[i]["AptNum"].ToString()){
						aptRow=rawApt.Rows[a];
						break;
					}
				}
				if(aptRow==null){
					continue;//this will have to be fixed in dbmaint.
				}
				//repair any item orders here rather than in dbmaint. It's really fast.
				if(itemOrder.ToString()!=rawPlannedAppts.Rows[i]["ItemOrder"].ToString()){
					command="UPDATE plannedappt SET ItemOrder="+POut.PLong(itemOrder)
						+" WHERE PlannedApptNum="+rawPlannedAppts.Rows[i]["PlannedApptNum"].ToString();
					dcon.NonQ(command);
				}
				//end of repair
				row=table.NewRow();
				row["AptNum"]=aptRow["AptNum"].ToString();
				dateSched=PIn.PDate(rawPlannedAppts.Rows[i]["AptDateTime"].ToString());
				//Colors----------------------------------------------------------------------------
				aptStatus=(ApptStatus)PIn.PLong(rawPlannedAppts.Rows[i]["AptStatus"].ToString());
				//change color if completed, broken, or unscheduled no matter the date
				if(aptStatus==ApptStatus.Broken || aptStatus==ApptStatus.UnschedList){
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][15].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][14].ItemColor.ToArgb().ToString();
				}
				else if(aptStatus==ApptStatus.Complete){
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][11].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][10].ItemColor.ToArgb().ToString();
				}
				else if(aptStatus==ApptStatus.Scheduled && dateSched.Date!=DateTime.Today.Date){
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][13].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][12].ItemColor.ToArgb().ToString();
				}
				else if(dateSched.Date<DateTime.Today && dateSched!=DateTime.MinValue){//Past
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][11].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][10].ItemColor.ToArgb().ToString();
				}
				else if(dateSched.Date == DateTime.Today.Date){ //Today
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][9].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][8].ItemColor.ToArgb().ToString();
				}
				else if(dateSched.Date > DateTime.Today){ //Future
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][13].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][12].ItemColor.ToArgb().ToString();
				}
				else{
					row["colorBackG"]=Color.White.ToArgb().ToString();
					row["colorText"]=Color.Black.ToArgb().ToString();
				}
				//end of colors------------------------------------------------------------------------------
				if(dateSched.Year<1880){
					row["dateSched"]="";
				}
				else{
					row["dateSched"]=dateSched.ToShortDateString();
				}
				row["ItemOrder"]=itemOrder.ToString();
				row["minutes"]=(aptRow["Pattern"].ToString().Length*5).ToString();
				row["Note"]=aptRow["Note"].ToString();
				row["ProcDescript"]=aptRow["ProcDescript"].ToString();
				if(aptStatus==ApptStatus.Complete){
					row["ProcDescript"]="(Completed) "+ row["ProcDescript"];
				}
				else if(dateSched == DateTime.Today.Date){
					row["ProcDescript"]="(Today's) "+ row["ProcDescript"];
				}
				rows.Add(row);
				itemOrder++;
			}
			for(int i=0;i<rows.Count;i++){
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>The supplied DataRows must include the following columns: ProcNum,ProcDate,Priority,ToothRange,ToothNum,ProcCode. This sorts all objects in Chart module based on their dates, times, priority, and toothnum.  For time comparisons, procs are not included.  But if other types such as comm have a time component in ProcDate, then they will be sorted by time as well.</summary>
		public static int CompareChartRows(DataRow x,DataRow y) {
			//No need to check RemotingRole; no call to db.
			if(x["ProcNum"].ToString()!="0" && y["ProcNum"].ToString()!="0") {//if both are procedures
				if(((DateTime)x["ProcDate"]).Date==((DateTime)y["ProcDate"]).Date) {//and the dates are the same
					return ProcedureLogic.CompareProcedures(x,y);
					//IComparer procComparer=new ProcedureComparer();
					//return procComparer.Compare(x,y);//sort by priority, toothnum, procCode
					//return 0;
				}
			}
			//In all other situations, all we care about is the date/time.
			return ((DateTime)x["ProcDate"]).CompareTo(((DateTime)y["ProcDate"]));
			//IComparer myComparer = new ObjectDateComparer();
			//return myComparer.Compare(x,y);
		}









	}

	


	//public class DtoChartModuleGetAll:DtoQueryBase {
	//	public int PatNum;
	//	public bool IsAuditMode;
	//}

}
