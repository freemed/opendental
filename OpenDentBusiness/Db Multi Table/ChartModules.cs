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
			retVal.Tables.Add(GetProgNotes(patNum,isAuditMode,new ChartModuleComponentsToLoad()));
			retVal.Tables.Add(GetPlannedApt(patNum));
			return retVal;
		}

		public static DataSet GetAll(long patNum,bool isAuditMode,ChartModuleComponentsToLoad componentsToLoad) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),patNum,isAuditMode,componentsToLoad);
			}
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetProgNotes(patNum,isAuditMode,componentsToLoad));
			retVal.Tables.Add(GetPlannedApt(patNum));
			return retVal;
		}

		public static DataTable GetProgNotes(long patNum,	bool isAuditMode,ChartModuleComponentsToLoad componentsToLoad) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),componentsToLoad);
			}
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("ProgNotes");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("aptDateTime",typeof(DateTime));
			table.Columns.Add("AbbrDesc");
			table.Columns.Add("AptNum");
			table.Columns.Add("clinic");
			table.Columns.Add("CodeNum");
			table.Columns.Add("colorBackG");
			table.Columns.Add("colorText");
			table.Columns.Add("CommlogNum");
			table.Columns.Add("dateEntryC");
			table.Columns.Add("dateTP");
			table.Columns.Add("description");
			table.Columns.Add("dx");
			table.Columns.Add("Dx");
			table.Columns.Add("EmailMessageNum");
			table.Columns.Add("FormPatNum");
			table.Columns.Add("HideGraphics");
			table.Columns.Add("length");
			table.Columns.Add("LabCaseNum");
			table.Columns.Add("note");
			table.Columns.Add("orionDateScheduleBy");
			table.Columns.Add("orionDateStopClock");
			table.Columns.Add("orionDPC");
			table.Columns.Add("orionDPCpost");
			table.Columns.Add("orionIsEffectiveComm");
			table.Columns.Add("orionIsOnCall");
			table.Columns.Add("orionStatus2");
			table.Columns.Add("PatNum");//only used for Commlog and Task
			table.Columns.Add("Priority");//for sorting
			table.Columns.Add("priority");
			table.Columns.Add("ProcCode");
			table.Columns.Add("procDate");
			table.Columns.Add("ProcDate",typeof(DateTime));
			table.Columns.Add("procFee");
			table.Columns.Add("ProcNum");
			table.Columns.Add("ProcNumLab");
			table.Columns.Add("procStatus");
			table.Columns.Add("ProcStatus");
			table.Columns.Add("procTime");
			table.Columns.Add("procTimeEnd");
			table.Columns.Add("prognosis");
			table.Columns.Add("prov");
			table.Columns.Add("quadrant");
			table.Columns.Add("RxNum");
			table.Columns.Add("SheetNum");
			table.Columns.Add("signature");
			table.Columns.Add("Surf");
			table.Columns.Add("surf");
			table.Columns.Add("TaskNum");
			table.Columns.Add("toothNum");
			table.Columns.Add("ToothNum");
			table.Columns.Add("ToothRange");
			table.Columns.Add("user");
			//table.Columns.Add("");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			string command;
			DateTime dateT;
			string txt;
			List<DataRow> labRows=new List<DataRow>();//Canadian lab procs, which must be added in a loop at the very end.
			if(componentsToLoad.ShowTreatPlan
				|| componentsToLoad.ShowCompleted
				|| componentsToLoad.ShowExisting
				|| componentsToLoad.ShowReferred
				|| componentsToLoad.ShowConditions){
				#region Procedures
				command="SELECT provider.Abbr,procedurecode.AbbrDesc,appointment.AptDateTime,procedurelog.BaseUnits,procedurelog.ClinicNum,"
				+"procedurelog.CodeNum,procedurelog.DateEntryC,orionproc.DateScheduleBy,orionproc.DateStopClock,procedurelog.DateTP,"
				+"procedurecode.Descript,orionproc.DPC,orionproc.DPCpost,Dx,HideGraphics,orionproc.IsEffectiveComm,orionproc.IsOnCall,"
				+"LaymanTerm,Priority,procedurecode.ProcCode,ProcDate,ProcFee,procedurelog.ProcNum,ProcNumLab,procedurelog.ProcTime,"
				+"procedurelog.ProcTimeEnd,procedurelog.Prognosis,ProcStatus,orionproc.Status2,Surf,ToothNum,ToothRange,UnitQty "
				+"FROM procedurelog "
				+"LEFT JOIN procedurecode ON procedurecode.CodeNum=procedurelog.CodeNum "
				+"LEFT JOIN provider ON provider.ProvNum=procedurelog.ProvNum "
				+"LEFT JOIN orionproc ON procedurelog.ProcNum=orionproc.ProcNum "
				+"LEFT JOIN appointment ON appointment.AptNum=procedurelog.AptNum "
				+"AND (appointment.AptStatus="+POut.Long((int)ApptStatus.Scheduled)
				+" OR appointment.AptStatus="+POut.Long((int)ApptStatus.ASAP)
				+" OR appointment.AptStatus="+POut.Long((int)ApptStatus.Broken)
				+" OR appointment.AptStatus="+POut.Long((int)ApptStatus.Complete)
				+") WHERE procedurelog.PatNum="+POut.Long(patNum);
				if(!isAuditMode) {
					command+=" AND ProcStatus !=6";//don't include deleted
				}
				command+=" ORDER BY ProcDate";//we'll just have to reorder it anyway
				DataTable rawProcs=dcon.GetTable(command);
				command="SELECT ProcNum,EntryDateTime,UserNum,Note,"
				+"CASE WHEN Signature!='' THEN 1 ELSE 0 END AS SigPresent "
				+"FROM procnote WHERE PatNum="+POut.Long(patNum)
				+" ORDER BY EntryDateTime";// but this helps when looping for notes
				DataTable rawNotes=dcon.GetTable(command);
				for(int i=0;i<rawProcs.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]=rawProcs.Rows[i]["AbbrDesc"].ToString();
					row["aptDateTime"]=PIn.DateT(rawProcs.Rows[i]["AptDateTime"].ToString());
					row["AptNum"]=0;
					row["clinic"]=Clinics.GetDesc(PIn.Long(rawProcs.Rows[i]["ClinicNum"].ToString()));
					row["CodeNum"]=rawProcs.Rows[i]["CodeNum"].ToString();
					row["colorBackG"]=Color.White.ToArgb();
					if(((DateTime)row["aptDateTime"]).Date==DateTime.Today) {
						row["colorBackG"]=DefC.Long[(int)DefCat.MiscColors][6].ItemColor.ToArgb().ToString();
					}
					switch((ProcStat)PIn.Long(rawProcs.Rows[i]["ProcStatus"].ToString())) {
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
					dateT=PIn.DateT(rawProcs.Rows[i]["DateEntryC"].ToString());
					if(dateT.Year<1880) {
						row["dateEntryC"]="";
					}
					else {
						row["dateEntryC"]=dateT.ToString(Lans.GetShortDateTimeFormat());
					}
					dateT=PIn.DateT(rawProcs.Rows[i]["DateTP"].ToString());
					if(dateT.Year<1880) {
						row["dateTP"]="";
					}
					else {
						row["dateTP"]=dateT.ToString(Lans.GetShortDateTimeFormat());
					}
					if(rawProcs.Rows[i]["LaymanTerm"].ToString()=="") {
						row["description"]=rawProcs.Rows[i]["Descript"].ToString();
					}
					else {
						row["description"]=rawProcs.Rows[i]["LaymanTerm"].ToString();
					}
					if(rawProcs.Rows[i]["ToothRange"].ToString()!="") {
						row["description"]+=" #"+Tooth.FormatRangeForDisplay(rawProcs.Rows[i]["ToothRange"].ToString());
					}
					row["dx"]=DefC.GetValue(DefCat.Diagnosis,PIn.Long(rawProcs.Rows[i]["Dx"].ToString()));
					row["Dx"]=rawProcs.Rows[i]["Dx"].ToString();
					row["EmailMessageNum"]=0;
					row["FormPatNum"]=0;
					row["HideGraphics"]=rawProcs.Rows[i]["HideGraphics"].ToString();
					row["LabCaseNum"]=0;
					row["length"]="";
					row["signature"]="";
					row["user"]="";
					if(componentsToLoad.ShowProcNotes) {
						#region note-----------------------------------------------------------------------------------------------------------
						row["note"]="";
						dateT=PIn.DateT(rawProcs.Rows[i]["DateScheduleBy"].ToString());
						if(dateT.Year<1880) {
							row["orionDateScheduleBy"]="";
						}
						else {
							row["orionDateScheduleBy"]=dateT.ToString(Lans.GetShortDateTimeFormat());
						}
						dateT=PIn.DateT(rawProcs.Rows[i]["DateStopClock"].ToString());
						if(dateT.Year<1880) {
							row["orionDateStopClock"]="";
						}
						else {
							row["orionDateStopClock"]=dateT.ToString(Lans.GetShortDateTimeFormat());
						}
						if(((OrionDPC)PIn.Int(rawProcs.Rows[i]["DPC"].ToString())).ToString()=="NotSpecified") {
							row["orionDPC"]="";
						}
						else {
							row["orionDPC"]=((OrionDPC)PIn.Int(rawProcs.Rows[i]["DPC"].ToString())).ToString();
						}
						if(((OrionDPC)PIn.Int(rawProcs.Rows[i]["DPCpost"].ToString())).ToString()=="NotSpecified") {
							row["orionDPCpost"]="";
						}
						else {
							row["orionDPCpost"]=((OrionDPC)PIn.Int(rawProcs.Rows[i]["DPCpost"].ToString())).ToString();
						}
						row["orionIsEffectiveComm"]="";
						if(rawProcs.Rows[i]["IsEffectiveComm"].ToString()=="1") {
							row["orionIsEffectiveComm"]="Y";
						}
						else if(rawProcs.Rows[i]["IsEffectiveComm"].ToString()=="0") {
							row["orionIsEffectiveComm"]="";
						}
						row["orionIsOnCall"]="";
						if(rawProcs.Rows[i]["IsOnCall"].ToString()=="1") {
							row["orionIsOnCall"]="Y";
						}
						else if(rawProcs.Rows[i]["IsOnCall"].ToString()=="0") {
							row["orionIsOnCall"]="";
						}
						row["orionStatus2"]=((OrionStatus)PIn.Int(rawProcs.Rows[i]["Status2"].ToString())).ToString();
						if(isAuditMode) {//we will include all notes for each proc.  We will concat and make readable.
							for(int n=0;n<rawNotes.Rows.Count;n++) {//loop through each note
								if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
									continue;
								}
								if(row["note"].ToString()!="") {//if there is an existing note
									row["note"]+="\r\n------------------------------------------------------\r\n";//start a new line
								}
								row["note"]+=PIn.DateT(rawNotes.Rows[n]["EntryDateTime"].ToString()).ToString();
								row["note"]+="  "+Userods.GetName(PIn.Long(rawNotes.Rows[n]["UserNum"].ToString()));
								if(rawNotes.Rows[n]["SigPresent"].ToString()=="1") {
									row["note"]+="  "+Lans.g("ChartModule","(signed)");
								}
								row["note"]+="\r\n"+rawNotes.Rows[n]["Note"].ToString();
							}
						}
						else {//Not audit mode.  We just want the most recent note
							for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//loop through each note, backwards.
								if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
									continue;
								}
								row["note"]=rawNotes.Rows[n]["Note"].ToString();
								break;//out of note loop.
							}
						}
						#endregion Note
					}
					//This section is closely related to notes, but must be filled for all procedures regardless of whether showing the actual note.
					if(!isAuditMode) {//Audit mode is handled above by putting this info into the note section itself.
						for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//Loop through each note; backwards to get most recent note.
							if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
								continue;
							}
							row["user"]=Userods.GetName(PIn.Long(rawNotes.Rows[n]["UserNum"].ToString()));
							if(rawNotes.Rows[n]["SigPresent"].ToString()=="1") {
								row["signature"]=Lans.g("ChartModule","Signed");
							}
							else {
								row["signature"]="";
							}
							break;
						}
					}
					row["PatNum"]="";
					row["Priority"]=rawProcs.Rows[i]["Priority"].ToString();
					row["priority"]=DefC.GetName(DefCat.TxPriorities,PIn.Long(rawProcs.Rows[i]["Priority"].ToString()));
					row["ProcCode"]=rawProcs.Rows[i]["ProcCode"].ToString();
					dateT=PIn.DateT(rawProcs.Rows[i]["ProcDate"].ToString());
					if(dateT.Year<1880) {
						row["procDate"]="";
					}
					else {
						row["procDate"]=dateT.ToString(Lans.GetShortDateTimeFormat());
					}
					row["ProcDate"]=dateT;
					double amt = PIn.Double(rawProcs.Rows[i]["ProcFee"].ToString());
					int qty = PIn.Int(rawProcs.Rows[i]["UnitQty"].ToString()) + PIn.Int(rawProcs.Rows[i]["BaseUnits"].ToString());
					if(qty>0) {
						amt *= qty;
					}
					row["procFee"]=amt.ToString("F");
					row["ProcNum"]=rawProcs.Rows[i]["ProcNum"].ToString();
					row["ProcNumLab"]=rawProcs.Rows[i]["ProcNumLab"].ToString();
					row["procStatus"]=Lans.g("enumProcStat",((ProcStat)PIn.Long(rawProcs.Rows[i]["ProcStatus"].ToString())).ToString());
					row["ProcStatus"]=rawProcs.Rows[i]["ProcStatus"].ToString();
					row["procTime"]="";
					dateT=PIn.DateT(rawProcs.Rows[i]["ProcTime"].ToString());
					if(dateT.TimeOfDay!=TimeSpan.Zero) {
						row["procTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
					}
					row["procTimeEnd"]="";
					dateT=PIn.DateT(rawProcs.Rows[i]["ProcTimeEnd"].ToString());
					if(dateT.TimeOfDay!=TimeSpan.Zero) {
						row["procTimeEnd"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
					}
					row["prognosis"]=DefC.GetName(DefCat.Prognosis,PIn.Long(rawProcs.Rows[i]["Prognosis"].ToString()));
					row["prov"]=rawProcs.Rows[i]["Abbr"].ToString();
					row["quadrant"]="";
					if(ProcedureCodes.GetProcCode(PIn.Long(row["CodeNum"].ToString())).TreatArea==TreatmentArea.Tooth) {
						row["quadrant"]=Tooth.GetQuadrant(rawProcs.Rows[i]["ToothNum"].ToString());
					}
					else if(ProcedureCodes.GetProcCode(PIn.Long(row["CodeNum"].ToString())).TreatArea==TreatmentArea.Surf) {
						row["quadrant"]=Tooth.GetQuadrant(rawProcs.Rows[i]["ToothNum"].ToString());
					}
					else if(ProcedureCodes.GetProcCode(PIn.Long(row["CodeNum"].ToString())).TreatArea==TreatmentArea.Quad) {
						row["quadrant"]=rawProcs.Rows[i]["Surf"].ToString();
					}
					else if(ProcedureCodes.GetProcCode(PIn.Long(row["CodeNum"].ToString())).TreatArea==TreatmentArea.ToothRange) {
						string[] toothNum=rawProcs.Rows[i]["ToothRange"].ToString().Split(',');
						bool sameQuad=false;//Don't want true if length==0.
						for(int n=0;n<toothNum.Length;n++) {//But want true if length==1 (check index 0 against itself).
							if(Tooth.GetQuadrant(toothNum[n])==Tooth.GetQuadrant(toothNum[0])) {
								sameQuad=true;
							}
							else {
								sameQuad=false;
								break;
							}
						}
						if(sameQuad) {
							row["quadrant"]=Tooth.GetQuadrant(toothNum[0]);
						}
					}
					row["RxNum"]=0;
					row["SheetNum"]=0;
					row["Surf"]=rawProcs.Rows[i]["Surf"].ToString();
					if(ProcedureCodes.GetProcCode(PIn.Long(row["CodeNum"].ToString())).TreatArea==TreatmentArea.Surf) {
						row["surf"]=Tooth.SurfTidyFromDbToDisplay(rawProcs.Rows[i]["Surf"].ToString(),rawProcs.Rows[i]["ToothNum"].ToString());
					}
					else {
						row["surf"]=rawProcs.Rows[i]["Surf"].ToString();
					}
					row["TaskNum"]=0;
					row["toothNum"]=Tooth.GetToothLabel(rawProcs.Rows[i]["ToothNum"].ToString());
					row["ToothNum"]=rawProcs.Rows[i]["ToothNum"].ToString();
					row["ToothRange"]=rawProcs.Rows[i]["ToothRange"].ToString();
					if(rawProcs.Rows[i]["ProcNumLab"].ToString()=="0") {//normal proc
						rows.Add(row);
					}
					else {
						row["description"]="^ ^ "+row["description"].ToString();
						labRows.Add(row);//these will be added in the loop at the end
					}
				}
				#endregion Procedures
			}
			if(componentsToLoad.ShowCommLog) {//TODO: refine to use show Family
				#region Commlog
				command="SELECT CommlogNum,CommDateTime,commlog.DateTimeEnd,CommType,Note,commlog.PatNum,UserNum,p1.FName,"
				+"CASE WHEN Signature!='' THEN 1 ELSE 0 END SigPresent "
				+"FROM patient p1,patient p2,commlog "
				+"WHERE commlog.PatNum=p1.PatNum "
				+"AND p1.Guarantor=p2.Guarantor "
				+"AND p2.PatNum="+POut.Long(patNum)
				+" ORDER BY CommDateTime";
				DataTable rawComm=dcon.GetTable(command);
				for(int i=0;i<rawComm.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"]=DateTime.MinValue;
					row["AptNum"]=0;
					row["clinic"]="";
					row["CodeNum"]="";
					row["colorBackG"]=Color.White.ToArgb();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();
					row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
					row["dateEntryC"]="";
					row["dateTP"]="";
					if(rawComm.Rows[i]["PatNum"].ToString()==patNum.ToString()) {
						txt="";
					}
					else {
						txt="("+rawComm.Rows[i]["FName"].ToString()+") ";
					}
					row["description"]=txt+Lans.g("ChartModule","Comm - ")
					+DefC.GetName(DefCat.CommLogTypes,PIn.Long(rawComm.Rows[i]["CommType"].ToString()));
					row["dx"]="";
					row["Dx"]="";
					row["EmailMessageNum"]=0;
					row["FormPatNum"]=0;
					row["HideGraphics"]="";
					row["LabCaseNum"]=0;
					row["length"]="";
					if(PIn.DateT(rawComm.Rows[i]["DateTimeEnd"].ToString()).Year>1880) {
						DateTime startTime=PIn.DateT(rawComm.Rows[i]["CommDateTime"].ToString());
						DateTime endTime=PIn.DateT(rawComm.Rows[i]["DateTimeEnd"].ToString());
						row["length"]=(endTime-startTime).ToStringHmm();
					}
					row["note"]=rawComm.Rows[i]["Note"].ToString();
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"]=rawComm.Rows[i]["PatNum"].ToString();
					row["Priority"]="";
					row["priority"]="";
					row["ProcCode"]="";
					dateT=PIn.DateT(rawComm.Rows[i]["CommDateTime"].ToString());
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
					row["procTimeEnd"]="";
					row["procFee"]="";
					row["ProcNum"]=0;
					row["ProcNumLab"]="";
					row["procStatus"]="";
					row["ProcStatus"]="";
					row["prov"]="";
					row["quadrant"]="";
					row["RxNum"]=0;
					row["SheetNum"]=0;
					row["signature"]="";
					if(rawComm.Rows[i]["SigPresent"].ToString()=="1") {
						row["signature"]=Lans.g("ChartModule","Signed");
					}
					row["Surf"]="";
					row["TaskNum"]=0;
					row["toothNum"]="";
					row["ToothNum"]="";
					row["ToothRange"]="";
					row["user"]=Userods.GetName(PIn.Long(rawComm.Rows[i]["UserNum"].ToString()));
					rows.Add(row);
				}
				#endregion Commlog
			}
			if(componentsToLoad.ShowFormPat) {
				#region formpat
				command = "SELECT FormDateTime,FormPatNum "
				+ "FROM formpat WHERE PatNum ='" + POut.Long(patNum) + "' ORDER BY FormDateTime";
				DataTable rawForm = dcon.GetTable(command);
				for(int i = 0;i < rawForm.Rows.Count;i++) {
					row = table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"] = DateTime.MinValue;
					row["AptNum"] = 0;
					row["clinic"]="";
					row["CodeNum"] = "";
					row["colorBackG"] = Color.White.ToArgb();
					row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();
					row["CommlogNum"] =0;
					row["dateEntryC"]="";
					row["dateTP"]="";
					row["description"] = Lans.g("ChartModule","Questionnaire");
					row["dx"] = "";
					row["Dx"] = "";
					row["EmailMessageNum"] = 0;
					row["FormPatNum"] = rawForm.Rows[i]["FormPatNum"].ToString();
					row["HideGraphics"]="";
					row["LabCaseNum"] = 0;
					row["length"]="";
					row["note"] = "";
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"] = "";
					row["Priority"] = "";
					row["priority"]="";
					row["ProcCode"] = "";
					dateT = PIn.DateT(rawForm.Rows[i]["FormDateTime"].ToString());
					row["ProcDate"] = dateT.ToShortDateString();
					if(dateT.TimeOfDay != TimeSpan.Zero) {
						row["procTime"] = dateT.ToString("h:mm") + dateT.ToString("%t").ToLower();
					}
					if(dateT.Year < 1880) {
						row["procDate"] = "";
					}
					else {
						row["procDate"] = dateT.ToString(Lans.GetShortDateTimeFormat());
					}
					if(dateT.TimeOfDay != TimeSpan.Zero) {
						row["procTime"] = dateT.ToString("h:mm") + dateT.ToString("%t").ToLower();
					}
					row["procTimeEnd"]="";
					row["procFee"] = "";
					row["ProcNum"] = 0;
					row["ProcNumLab"] = "";
					row["procStatus"] = "";
					row["ProcStatus"] = "";
					row["prov"] = "";
					row["quadrant"]="";
					row["RxNum"] = 0;
					row["SheetNum"] = 0;
					row["signature"] = "";
					row["Surf"] = "";
					row["TaskNum"] = 0;
					row["toothNum"] = "";
					row["ToothNum"] = "";
					row["ToothRange"] = "";
					row["user"] = "";
					/*commlog code
					dateT = PIn.PDateT(rawForm.Rows[i]["FormDateTime"].ToString());
					row["CommDateTime"] = dateT;
					row["commDate"] = dateT.ToShortDateString();
					if (dateT.TimeOfDay != TimeSpan.Zero)
					{
							row["commTime"] = dateT.ToString("h:mm") + dateT.ToString("%t").ToLower();
					}
					row["CommlogNum"] = "0";
					row["commType"] = Lans.g("AccountModule", "Questionnaire");
					row["EmailMessageNum"] = "0";
					row["FormPatNum"] = rawForm.Rows[i]["FormPatNum"].ToString();
					row["mode"] = "";
					row["Note"] = "";
					row["patName"] = "";
					row["SheetNum"] = "0";
					//row["sentOrReceived"]="";
					*/
					rows.Add(row);
				}
				#endregion formpat
			}
			if(componentsToLoad.ShowRX) {
				#region Rx
				command="SELECT RxNum,RxDate,Drug,Disp,ProvNum,Notes,PharmacyNum FROM rxpat WHERE PatNum="+POut.Long(patNum)
				+" ORDER BY RxDate";
				DataTable rawRx=dcon.GetTable(command);
				for(int i=0;i<rawRx.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"]=DateTime.MinValue;
					row["AptNum"]=0;
					row["clinic"]="";
					row["CodeNum"]="";
					row["colorBackG"]=Color.White.ToArgb();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][5].ItemColor.ToArgb().ToString();
					row["CommlogNum"]=0;
					row["dateEntryC"]="";
					row["dateTP"]="";
					row["description"]=Lans.g("ChartModule","Rx - ")+rawRx.Rows[i]["Drug"].ToString()+" - #"+rawRx.Rows[i]["Disp"].ToString();
					if(rawRx.Rows[i]["PharmacyNum"].ToString()!="0") {
						row["description"]+="\r\n"+Pharmacies.GetDescription(PIn.Long(rawRx.Rows[i]["PharmacyNum"].ToString()));
					}
					row["dx"]="";
					row["Dx"]="";
					row["EmailMessageNum"]=0;
					row["FormPatNum"]=0;
					row["HideGraphics"]="";
					row["LabCaseNum"]=0;
					row["length"]="";
					row["note"]=rawRx.Rows[i]["Notes"].ToString();
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"]="";
					row["Priority"]="";
					row["priority"]="";
					row["ProcCode"]="";
					dateT=PIn.Date(rawRx.Rows[i]["RxDate"].ToString());
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
					row["procTimeEnd"]="";
					row["prov"]=Providers.GetAbbr(PIn.Long(rawRx.Rows[i]["ProvNum"].ToString()));
					row["quadrant"]="";
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
				#endregion Rx
			}
			if(componentsToLoad.ShowLabCases) {
				#region LabCase
				command="SELECT labcase.*,Description,Phone FROM labcase,laboratory "
				+"WHERE labcase.LaboratoryNum=laboratory.LaboratoryNum "
				+"AND PatNum="+POut.Long(patNum)
				+" ORDER BY DateTimeCreated";
				DataTable rawLab=dcon.GetTable(command);
				DateTime duedate;
				for(int i=0;i<rawLab.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"]=DateTime.MinValue;
					row["AptNum"]=0;
					row["clinic"]="";
					row["CodeNum"]="";
					row["colorBackG"]=Color.White.ToArgb();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][7].ItemColor.ToArgb().ToString();
					row["CommlogNum"]=0;
					row["dateEntryC"]="";
					row["dateTP"]="";
					row["description"]=Lans.g("ChartModule","LabCase - ")+rawLab.Rows[i]["Description"].ToString()+" "
					+rawLab.Rows[i]["Phone"].ToString();
					if(PIn.Date(rawLab.Rows[i]["DateTimeDue"].ToString()).Year>1880) {
						duedate=PIn.DateT(rawLab.Rows[i]["DateTimeDue"].ToString());
						row["description"]+="\r\n"+Lans.g("ChartModule","Due")+" "+duedate.ToString("ddd")+" "
						+duedate.ToShortDateString()+" "+duedate.ToShortTimeString();
					}
					if(PIn.Date(rawLab.Rows[i]["DateTimeChecked"].ToString()).Year>1880) {
						row["description"]+="\r\n"+Lans.g("ChartModule","Quality Checked");
					}
					else if(PIn.Date(rawLab.Rows[i]["DateTimeRecd"].ToString()).Year>1880) {
						row["description"]+="\r\n"+Lans.g("ChartModule","Received");
					}
					else if(PIn.Date(rawLab.Rows[i]["DateTimeSent"].ToString()).Year>1880) {
						row["description"]+="\r\n"+Lans.g("ChartModule","Sent");
					}
					row["dx"]="";
					row["Dx"]="";
					row["EmailMessageNum"]=0;
					row["FormPatNum"]=0;
					row["HideGraphics"]="";
					row["LabCaseNum"]=rawLab.Rows[i]["LabCaseNum"].ToString();
					row["length"]="";
					row["note"]=rawLab.Rows[i]["Instructions"].ToString();
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"]="";
					row["Priority"]="";
					row["priority"]="";
					row["ProcCode"]="";
					dateT=PIn.DateT(rawLab.Rows[i]["DateTimeCreated"].ToString());
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
					row["procTimeEnd"]="";
					row["procFee"]="";
					row["ProcNum"]=0;
					row["ProcNumLab"]="";
					row["procStatus"]="";
					row["ProcStatus"]="";
					row["prov"]="";
					row["quadrant"]="";
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
				#endregion LabCase
			}
			if(componentsToLoad.ShowTasks) {
				#region Task
				command="SELECT task.*,tasklist.Descript ListDisc,p1.FName "
				+"FROM patient p1,patient p2, task,tasklist "
				+"WHERE task.KeyNum=p1.PatNum "
				+"AND task.TaskListNum=tasklist.TaskListNum "
				+"AND p1.Guarantor=p2.Guarantor "
				+"AND p2.PatNum="+POut.Long(patNum)
				+" AND task.ObjectType=1 "
				+"ORDER BY DateTimeEntry";
				DataTable rawTask=dcon.GetTable(command);
				List<long> taskNums=new List<long>();
				for(int i=0;i<rawTask.Rows.Count;i++) {
					taskNums.Add(PIn.Long(rawTask.Rows[i]["TaskNum"].ToString()));
				}
				List<TaskNote> TaskNoteList=TaskNotes.RefreshForTasks(taskNums);
				for(int i=0;i<rawTask.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"]=DateTime.MinValue;
					row["AptNum"]=0;
					row["clinic"]="";
					row["CodeNum"]="";
					//colors the same as notes
					row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][18].ItemColor.ToArgb().ToString();
					row["colorBackG"] = DefC.Long[(int)DefCat.ProgNoteColors][19].ItemColor.ToArgb().ToString();
					//row["colorText"] = DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();//same as commlog
					row["CommlogNum"]=0;
					row["dateEntryC"]="";
					row["dateTP"]="";
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
					row["FormPatNum"]=0;
					row["HideGraphics"]="";
					row["LabCaseNum"]=0;
					row["length"]="";
					txt="";
					if(!rawTask.Rows[i]["Descript"].ToString().StartsWith("==") && rawTask.Rows[i]["UserNum"].ToString()!="") {
						txt+=Userods.GetName(PIn.Long(rawTask.Rows[i]["UserNum"].ToString()))+" - ";
					}
					txt+=rawTask.Rows[i]["Descript"].ToString();
					long taskNum=PIn.Long(rawTask.Rows[i]["TaskNum"].ToString());
					for(int n=0;n<TaskNoteList.Count;n++) {
						if(TaskNoteList[n].TaskNum!=taskNum) {
							continue;
						}
						txt+="\r\n"//even on the first loop
						+"=="+Userods.GetName(TaskNoteList[n].UserNum)+" - "
						+TaskNoteList[n].DateTimeNote.ToShortDateString()+" "
						+TaskNoteList[n].DateTimeNote.ToShortTimeString()
						+" - "+TaskNoteList[n].Note;
					}
					row["note"]=txt;
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"]=rawTask.Rows[i]["KeyNum"].ToString();
					row["Priority"]="";
					row["priority"]="";
					row["ProcCode"]="";
					dateT = PIn.DateT(rawTask.Rows[i]["DateTask"].ToString());
					row["procTime"]="";
					if(dateT.Year < 1880) {//check if due date set for task or note
						dateT = PIn.DateT(rawTask.Rows[i]["DateTimeEntry"].ToString());
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
					row["procTimeEnd"]="";
					row["procFee"]="";
					row["ProcNum"]=0;
					row["ProcNumLab"]="";
					row["procStatus"]="";
					row["ProcStatus"]="";
					row["prov"]="";
					row["quadrant"]="";
					row["RxNum"]=0;
					row["SheetNum"]=0;
					row["signature"]="";
					row["Surf"]="";
					row["TaskNum"]=taskNum;
					row["toothNum"]="";
					row["ToothNum"]="";
					row["ToothRange"]="";
					row["user"]="";
					rows.Add(row);
				}
				#endregion Task
			}
			#region Appointments
			command="SELECT * FROM appointment WHERE PatNum="+POut.Long(patNum);
			if(componentsToLoad.ShowAppointments) {//we will need this table later for planned appts, so always need to get.
				//get all appts
			}
			else{
				//only include planned appts.  We will need those later, but not in this grid.
				command+=" AND AptStatus = "+POut.Int((int)ApptStatus.Planned);
			}
			command+=" ORDER BY AptDateTime";
			rawApt=dcon.GetTable(command);
			long apptStatus;
			for(int i=0;i<rawApt.Rows.Count;i++) {
				row=table.NewRow();
				row["AbbrDesc"]="";
				row["aptDateTime"]=DateTime.MinValue;
				row["AptNum"]=rawApt.Rows[i]["AptNum"].ToString();
				row["clinic"]="";
				row["colorBackG"]=Color.White.ToArgb();
				dateT=PIn.DateT(rawApt.Rows[i]["AptDateTime"].ToString());
				apptStatus=PIn.Long(rawApt.Rows[i]["AptStatus"].ToString());
				row["colorBackG"]="";
				row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][8].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=0;
				row["dateEntryC"]="";
				row["dateTP"]="";
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
				row["FormPatNum"]=0;
				row["HideGraphics"]="";
				row["LabCaseNum"]=0;
				row["length"]="";
				if(rawApt.Rows[i]["Pattern"].ToString()!="") {
					row["length"]=new TimeSpan(0,rawApt.Rows[i]["Pattern"].ToString().Length*5,0).ToStringHmm();
				}
				row["note"]=rawApt.Rows[i]["Note"].ToString();
				row["orionDateScheduleBy"]="";
				row["orionDateStopClock"]="";
				row["orionDPC"]="";
				row["orionDPCpost"]="";
				row["orionIsEffectiveComm"]="";
				row["orionIsOnCall"]="";
				row["orionStatus2"]="";
				row["PatNum"]="";
				row["Priority"]="";
				row["priority"]="";
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
				row["procTimeEnd"]="";
				row["procFee"]="";
				row["ProcNum"]=0;
				row["ProcNumLab"]="";
				row["procStatus"]="";
				row["ProcStatus"]="";
				row["prov"]="";
				row["quadrant"]="";
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
			#endregion Appointments
			if(componentsToLoad.ShowEmail) {
				#region email
				command="SELECT EmailMessageNum,MsgDateTime,Subject,BodyText,PatNum,SentOrReceived "
				+"FROM emailmessage "
				+"WHERE PatNum="+POut.Long(patNum)
				+" ORDER BY MsgDateTime";
				DataTable rawEmail=dcon.GetTable(command);
				for(int i=0;i<rawEmail.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"]=DateTime.MinValue;
					row["AptNum"]=0;
					row["clinic"]="";
					row["CodeNum"]="";
					row["colorBackG"]=Color.White.ToArgb();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();//needs to change
					row["CommlogNum"]=0;
					row["dateEntryC"]="";
					row["dateTP"]="";
					txt="";
					if(rawEmail.Rows[i]["SentOrReceived"].ToString()=="0") {
						txt=Lans.g("ChartModule","(unsent) ");
					}
					row["description"]=Lans.g("ChartModule","Email - ")+txt+rawEmail.Rows[i]["Subject"].ToString();
					row["dx"]="";
					row["Dx"]="";
					row["EmailMessageNum"]=rawEmail.Rows[i]["EmailMessageNum"].ToString();
					row["FormPatNum"]=0;
					row["HideGraphics"]="";
					row["LabCaseNum"]=0;
					row["length"]="";
					row["note"]=rawEmail.Rows[i]["BodyText"].ToString();
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"]="";
					row["Priority"]="";
					row["priority"]="";
					row["ProcCode"]="";
					//row["PatNum"]=rawEmail.Rows[i]["PatNum"].ToString();
					dateT=PIn.DateT(rawEmail.Rows[i]["msgDateTime"].ToString());
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
					row["procTimeEnd"]="";
					row["procFee"]="";
					row["ProcNum"]=0;
					row["ProcNumLab"]="";
					row["procStatus"]="";
					row["ProcStatus"]="";
					row["prov"]="";
					row["quadrant"]="";
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
				#endregion email
			}
			if(componentsToLoad.ShowSheets) {
				#region sheet
				command="SELECT Description,SheetNum,DateTimeSheet,SheetType "
				+"FROM sheet "
				+"WHERE PatNum="+POut.Long(patNum)
				+" AND SheetType!="+POut.Long((int)SheetTypeEnum.Rx)//rx are only accesssible from within Rx edit window.
				+" AND SheetType!="+POut.Long((int)SheetTypeEnum.LabSlip)//labslips are only accesssible from within the labslip edit window.
				+" ORDER BY DateTimeSheet";
				DataTable rawSheet=dcon.GetTable(command);
				//SheetTypeEnum sheetType;
				for(int i=0;i<rawSheet.Rows.Count;i++) {
					row=table.NewRow();
					row["AbbrDesc"]="";
					row["aptDateTime"]=DateTime.MinValue;
					row["AptNum"]=0;
					row["clinic"]="";
					row["CodeNum"]="";
					row["colorBackG"]=Color.White.ToArgb();
					row["colorText"]=Color.Black.ToArgb();//DefC.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();//needs to change
					row["CommlogNum"]=0;
					dateT=PIn.DateT(rawSheet.Rows[i]["DateTimeSheet"].ToString());
					if(dateT.Year<1880) {
						row["dateEntryC"]="";
						row["dateTP"]="";
					}
					else {
						row["dateEntryC"]=dateT.ToString(Lans.GetShortDateTimeFormat());
						row["dateTP"]=dateT.ToString(Lans.GetShortDateTimeFormat());
					}
					//sheetType=(SheetTypeEnum)PIn.PLong(rawSheet.Rows[i]["SheetType"].ToString());
					row["description"]=rawSheet.Rows[i]["Description"].ToString();
					row["dx"]="";
					row["Dx"]="";
					row["EmailMessageNum"]=0;
					row["FormPatNum"]=0;
					row["HideGraphics"]="";
					row["LabCaseNum"]=0;
					row["length"]="";
					row["note"]="";
					row["orionDateScheduleBy"]="";
					row["orionDateStopClock"]="";
					row["orionDPC"]="";
					row["orionDPCpost"]="";
					row["orionIsEffectiveComm"]="";
					row["orionIsOnCall"]="";
					row["orionStatus2"]="";
					row["PatNum"]="";
					row["Priority"]="";
					row["priority"]="";
					row["ProcCode"]="";
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
					row["procTimeEnd"]="";
					row["procFee"]="";
					row["ProcNum"]=0;
					row["ProcNumLab"]="";
					row["procStatus"]="";
					row["ProcStatus"]="";
					row["prov"]="";
					row["quadrant"]="";
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
				#endregion sheet
			}
			#region Sorting
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
			#endregion Sorting
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		public static DataTable GetPlannedApt(long patNum) {
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
			table.Columns.Add("PlannedApptNum");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//The query below was causing a max join error for big offices.  It's fixed now, 
			//but a better option for next time would be to put SET SQL_BIG_SELECTS=1; before the query.
			string command="SELECT plannedappt.AptNum,ItemOrder,PlannedApptNum,appointment.AptDateTime,"
				+"appointment.Pattern,appointment.AptStatus,"//COUNT(procedurelog.ProcNum) someAreComplete "//The count won't be accurate, but it will tell us if not zero.
				+"(SELECT COUNT(*) FROM procedurelog WHERE procedurelog.PlannedAptNum=plannedappt.AptNum AND procedurelog.ProcStatus=2) someAreComplete "
				+"FROM plannedappt "
				+"LEFT JOIN appointment ON appointment.NextAptNum=plannedappt.AptNum "
				//+"LEFT JOIN procedurelog ON procedurelog.PlannedAptNum=plannedappt.AptNum "//grab all attached completed procs
				//+"AND procedurelog.ProcStatus=2 "
				+"WHERE plannedappt.PatNum="+POut.Long(patNum)+" "
				+"GROUP BY plannedappt.AptNum,ItemOrder,PlannedApptNum,appointment.AptDateTime,"
				+"appointment.Pattern,appointment.AptStatus "
				+"ORDER BY ItemOrder";
			//plannedappt.AptNum does refer to the planned appt, but the other fields in the result are for the linked scheduled appt.
			DataTable rawPlannedAppts=dcon.GetTable(command);
			DataRow aptRow;
			int itemOrder=1;
			DateTime dateSched;
			ApptStatus aptStatus;
			for(int i=0;i<rawPlannedAppts.Rows.Count;i++) {
				aptRow=null;
				for(int a=0;a<rawApt.Rows.Count;a++) {
					if(rawApt.Rows[a]["AptNum"].ToString()==rawPlannedAppts.Rows[i]["AptNum"].ToString()) {
						aptRow=rawApt.Rows[a];
						break;
					}
				}
				if(aptRow==null) {
					continue;//this will have to be fixed in dbmaint.
				}
				//repair any item orders here rather than in dbmaint. It's really fast.
				if(itemOrder.ToString()!=rawPlannedAppts.Rows[i]["ItemOrder"].ToString()) {
					command="UPDATE plannedappt SET ItemOrder="+POut.Long(itemOrder)
						+" WHERE PlannedApptNum="+rawPlannedAppts.Rows[i]["PlannedApptNum"].ToString();
					dcon.NonQ(command);
				}
				//end of repair
				row=table.NewRow();
				row["AptNum"]=aptRow["AptNum"].ToString();
				dateSched=PIn.Date(rawPlannedAppts.Rows[i]["AptDateTime"].ToString());
				//Colors----------------------------------------------------------------------------
				aptStatus=(ApptStatus)PIn.Long(rawPlannedAppts.Rows[i]["AptStatus"].ToString());
				//change color if completed, broken, or unscheduled no matter the date
				if(aptStatus==ApptStatus.Broken || aptStatus==ApptStatus.UnschedList) {
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][15].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][14].ItemColor.ToArgb().ToString();
				}
				else if(aptStatus==ApptStatus.Complete) {
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][11].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][10].ItemColor.ToArgb().ToString();
				}
				else if(aptStatus==ApptStatus.Scheduled && dateSched.Date!=DateTime.Today.Date) {
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][13].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][12].ItemColor.ToArgb().ToString();
				}
				else if(dateSched.Date<DateTime.Today && dateSched!=DateTime.MinValue) {//Past
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][11].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][10].ItemColor.ToArgb().ToString();
				}
				else if(dateSched.Date == DateTime.Today.Date) { //Today
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][9].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][8].ItemColor.ToArgb().ToString();
				}
				else if(dateSched.Date > DateTime.Today) { //Future
					row["colorBackG"]=DefC.Long[(int)DefCat.ProgNoteColors][13].ItemColor.ToArgb().ToString();
					row["colorText"]=DefC.Long[(int)DefCat.ProgNoteColors][12].ItemColor.ToArgb().ToString();
				}
				else {
					row["colorBackG"]=Color.White.ToArgb().ToString();
					row["colorText"]=Color.Black.ToArgb().ToString();
				}
				//end of colors------------------------------------------------------------------------------
				if(dateSched.Year<1880) {
					row["dateSched"]="";
				}
				else {
					row["dateSched"]=dateSched.ToShortDateString();
				}
				row["ItemOrder"]=itemOrder.ToString();
				row["minutes"]=(aptRow["Pattern"].ToString().Length*5).ToString();
				row["Note"]=aptRow["Note"].ToString();
				row["PlannedApptNum"]=rawPlannedAppts.Rows[i]["PlannedApptNum"].ToString();
				row["ProcDescript"]=aptRow["ProcDescript"].ToString();
				if(aptStatus==ApptStatus.Complete) {
					row["ProcDescript"]=Lans.g("ContrChart","(Completed) ")+ row["ProcDescript"];
				}
				else if(dateSched == DateTime.Today.Date) {
					row["ProcDescript"]=Lans.g("ContrChart","(Today's) ")+ row["ProcDescript"];
				}
				else if(rawPlannedAppts.Rows[i]["someAreComplete"].ToString()!="0"){
					row["ProcDescript"]=Lans.g("ContrChart","(Some procs complete) ")+ row["ProcDescript"];
				}
				rows.Add(row);
				itemOrder++;
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>The supplied DataRows must include the following columns: ProcNum,ProcDate,Priority,ToothRange,ToothNum,ProcCode. This sorts all objects in Chart module based on their dates, times, priority, and toothnum.  For time comparisons, procs are not included.  But if other types such as comm have a time component in ProcDate, then they will be sorted by time as well.</summary>
		public static int CompareChartRows(DataRow x,DataRow y) {
			//if dates are different, then sort by date
			if(((DateTime)x["ProcDate"]).Date!=((DateTime)y["ProcDate"]).Date){
				return ((DateTime)x["ProcDate"]).Date.CompareTo(((DateTime)y["ProcDate"]).Date);
			}
			//Sort by Type. Types are: Appointments, Procedures, CommLog, Tasks, Email, Lab Cases, Rx, Sheets.----------------------------------------------------
			int xInd=0;
			if(x["AptNum"].ToString()!="0") {
				xInd=0;
			}
			else if(x["ProcNum"].ToString()!="0") {
				xInd=1;
			}
			else if(x["CommlogNum"].ToString()!="0") {
				xInd=2;
			}
			else if(x["TaskNum"].ToString()!="0") {
				xInd=3;
			}
			else if(x["EmailMessageNum"].ToString()!="0") {
				xInd=4;
			}
			else if(x["LabCaseNum"].ToString()!="0") {
				xInd=5;
			}
			else if(x["RxNum"].ToString()!="0") {
				xInd=6;
			}
			else if(x["SheetNum"].ToString()!="0") {
				xInd=7;
			}
			int yInd=0;
			if(y["AptNum"].ToString()!="0") {
				yInd=0;
			}
			else if(y["ProcNum"].ToString()!="0") {
				yInd=1;
			}
			else if(y["CommlogNum"].ToString()!="0") {
				yInd=2;
			}
			else if(y["TaskNum"].ToString()!="0") {
				yInd=3;
			}
			else if(y["EmailMessageNum"].ToString()!="0") {
				yInd=4;
			}
			else if(y["LabCaseNum"].ToString()!="0") {
				yInd=5;
			}
			else if(y["RxNum"].ToString()!="0") {
				yInd=6;
			}
			else if(y["SheetNum"].ToString()!="0") {
				yInd=7;
			}
			if(xInd!=yInd) {
				return xInd.CompareTo(yInd);
			}//End sort by type------------------------------------------------------------------------------------------------------------------------------------
			//Sort procedures by status, priority, tooth region/num, proc code
			if(x["ProcNum"].ToString()!="0" && y["ProcNum"].ToString()!="0") {//if both are procedures
				return ProcedureLogic.CompareProcedures(x,y);
			}
			//nothing below this point can be a procedure.
			//dates are guaranteed to match at this point.
			//they are also guaranteed to be the same type.
			//Sort other types by time-----------------------------------------------------------------------------------------------------------------------------
			if(((DateTime)x["ProcDate"])!=((DateTime)y["ProcDate"])){
			  return ((DateTime)x["ProcDate"]).CompareTo(((DateTime)y["ProcDate"]));
			}
			return 0;
		}









	}

	public class ChartModuleComponentsToLoad{
			public bool ShowAppointments;
			public bool ShowCommLog;
			public bool ShowCompleted;
			public bool ShowConditions;
			public bool ShowEmail;
			public bool ShowExisting;
			public bool ShowFamilyCommLog;
			public bool ShowFormPat;
			public bool ShowLabCases;
			public bool ShowProcNotes;
			public bool ShowReferred;
			public bool ShowRX;
			public bool ShowSheets;
			public bool ShowTasks;
			public bool ShowTreatPlan;

		///<summary>All showComponents are set to true.</summary>
		public ChartModuleComponentsToLoad() {
			ShowAppointments=true;
			ShowCommLog=true;
			ShowCompleted=true;
			ShowConditions=true;
			ShowEmail=true;
			ShowExisting=true;
			ShowFamilyCommLog=true;
			ShowFormPat=true;
			ShowLabCases=true;
			ShowProcNotes=true;
			ShowReferred=true;
			ShowRX=true;
			ShowSheets=true;
			ShowTasks=true;
			ShowTreatPlan=true;
		}

		///<summary></summary>
		public ChartModuleComponentsToLoad(
			bool showAppointments,
			bool showCommLog,
			bool showCompleted,
			bool showConditions,
			bool showEmail,
			bool showExisting,
			bool showFamilyCommLog,
			bool showFormPat,
			bool showLabCases,
			bool showProcNotes,
			bool showReferred,
			bool showRX,
			bool showSheets,
			bool showTasks,
			bool showTreatPlan) 
		{
			ShowAppointments=showAppointments;
			ShowCommLog=showCommLog;
			ShowCompleted=showCompleted;
			ShowConditions=showConditions;
			ShowEmail=showEmail;
			ShowExisting=showExisting;
			ShowFamilyCommLog=showFamilyCommLog;
			ShowFormPat=showFormPat;
			ShowLabCases=showLabCases;
			ShowProcNotes=showProcNotes;
			ShowReferred=showReferred;
			ShowRX=showRX;
			ShowSheets=showSheets;
			ShowTasks=showTasks;
			ShowTreatPlan=showTreatPlan;
		}

	}


	//public class DtoChartModuleGetAll:DtoQueryBase {
	//	public int PatNum;
	//	public bool IsAuditMode;
	//}

}
