using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	public class ChartModuleB {
		public static DataSet GetAll(int patNum,bool isAuditMode) {
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetProgNotes(patNum,isAuditMode));
			return retVal;
		}

		private static DataTable GetProgNotes(int patNum,bool isAuditMode) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("ProgNotes");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("ADACode");
			table.Columns.Add("aptDateTime",typeof(DateTime));
			table.Columns.Add("colorBackG");
			table.Columns.Add("colorText");
			table.Columns.Add("CommlogNum");
			table.Columns.Add("description");
			table.Columns.Add("dx");
			table.Columns.Add("Dx");
			table.Columns.Add("note");
			table.Columns.Add("Priority");
			table.Columns.Add("procDate");
			table.Columns.Add("ProcDate",typeof(DateTime));
			table.Columns.Add("procFee");
			table.Columns.Add("ProcNum");
			table.Columns.Add("ProcNumLab");
			table.Columns.Add("procStatus");
			table.Columns.Add("ProcStatus");
			table.Columns.Add("prov");
			table.Columns.Add("RxNum");
			table.Columns.Add("signature");
			table.Columns.Add("Surf");
			table.Columns.Add("toothNum");
			table.Columns.Add("ToothNum");
			table.Columns.Add("ToothRange");
			table.Columns.Add("user");
			//table.Columns.Add("");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//Procedures-----------------------------------------------------------------------------------------------------
			string command="SELECT ProcDate,ProcStatus,ToothNum,Surf,Dx,procedurelog.ADACode,ProcNum,procedurecode.Descript,"
				+"provider.Abbr,ProcFee,ProcNumLab,appointment.AptDateTime,Priority,ToothRange "
				+"FROM procedurelog "
				+"LEFT JOIN procedurecode ON procedurecode.ADACode=procedurelog.ADACode "
				+"LEFT JOIN provider ON provider.ProvNum=procedurelog.ProvNum "
				+"LEFT JOIN appointment ON appointment.AptNum=procedurelog.AptNum "
				+"AND (appointment.AptStatus="+POut.PInt((int)ApptStatus.Scheduled)
				+" OR appointment.AptStatus="+POut.PInt((int)ApptStatus.ASAP)
				+" OR appointment.AptStatus="+POut.PInt((int)ApptStatus.Broken)
				+" OR appointment.AptStatus="+POut.PInt((int)ApptStatus.Complete)
				+") WHERE procedurelog.PatNum="+POut.PInt(patNum);
			if(!isAuditMode){
				command+=" AND ProcStatus !=6";//don't include deleted
			}
			command+=" ORDER BY ProcDate";//we'll just have to reorder it anyway
			DataTable rawProcs=dcon.GetTable(command);
			command="SELECT ProcNum,EntryDateTime,UserNum,Note,"
				+"CASE WHEN Signature!='' THEN 1 ELSE 0 END AS SigPresent "
				+"FROM procnote WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY EntryDateTime";// but this helps when looping for notes
			DataTable rawNotes=dcon.GetTable(command);
			DateTime dateT;
			List<DataRow> labRows=new List<DataRow>();//Canadian lab procs, which must be added in a loop at the very end.
			for(int i=0;i<rawProcs.Rows.Count;i++) {
				row=table.NewRow();
				row["ADACode"]=rawProcs.Rows[i]["ADACode"].ToString();
				row["aptDateTime"]=PIn.PDateT(rawProcs.Rows[i]["AptDateTime"].ToString());
				row["colorBackG"]=Color.White.ToArgb();
				if(((DateTime)row["aptDateTime"]).Date==DateTime.Today){
					row["colorBackG"]=DefB.Long[(int)DefCat.MiscColors][6].ItemColor.ToArgb().ToString();
				}
				switch((ProcStat)PIn.PInt(rawProcs.Rows[i]["ProcStatus"].ToString())){
					case ProcStat.TP:
						row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][0].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.C:
						row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][1].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.EC:
						row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][2].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.EO:
						row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][3].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.R:
						row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][4].ItemColor.ToArgb().ToString();
						break;
					case ProcStat.D:
						row["colorText"]=Color.Black.ToArgb().ToString();
						break;
				}
				row["CommlogNum"]=0;
				row["description"]=rawProcs.Rows[i]["Descript"].ToString();
				row["dx"]=DefB.GetValue(DefCat.Diagnosis,PIn.PInt(rawProcs.Rows[i]["Dx"].ToString()));
				row["Dx"]=rawProcs.Rows[i]["Dx"].ToString();
				//note-----------------------------------------------------------------------------------------------------------
				if(isAuditMode){//we will include all notes for each proc.  We will concat and make readable.
					for(int n=0;n<rawNotes.Rows.Count;n++) {//loop through each note
						if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
							continue;
						}
						if(row["Note"].ToString()!="") {//if there is an existing note
							row["note"]+="\r\n------------------------------------------------------\r\n";//start a new line
						}
						row["note"]+=PIn.PDateT(rawNotes.Rows[n]["EntryDateTime"].ToString()).ToString();
						row["note"]+="  "+UserodB.GetName(PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString()));
						if(rawNotes.Rows[n]["SigPresent"].ToString()=="1") {
							row["note"]+="  "+Lan.g("ChartModule","(signed)");
						}
						row["note"]+="\r\n"+rawNotes.Rows[n]["Note"].ToString();
					}
				}
				else{//we just want the most recent note
					for(int n=rawNotes.Rows.Count-1;n>=0;n--){//loop through each note, backwards.
						if(rawProcs.Rows[i]["ProcNum"].ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
							continue;
						}
						row["user"]		 =UserodB.GetName(PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString()));
						row["note"]		 =rawNotes.Rows[n]["Note"].ToString();
						if(rawNotes.Rows[n]["SigPresent"].ToString()=="1") {
							row["signature"]=Lan.g("ChartModule","Signed");
						}
						else{
							row["signature"]="";
						}
						break;//out of note loop.
					}
				}
				row["Priority"]=rawProcs.Rows[i]["Priority"].ToString();
				dateT=PIn.PDateT(rawProcs.Rows[i]["ProcDate"].ToString());
				if(dateT.Year<1880){
					row["procDate"]="";
				}
				else{
					row["procDate"]=dateT.ToShortDateString();
				}
				row["ProcDate"]=dateT;
				row["procFee"]=PIn.PDouble(rawProcs.Rows[i]["ProcFee"].ToString()).ToString("F");
				row["ProcNum"]=rawProcs.Rows[i]["ProcNum"].ToString();
				row["ProcNumLab"]=rawProcs.Rows[i]["ProcNumLab"].ToString();
				row["procStatus"]=Lan.g("enumProcStat",((ProcStat)PIn.PInt(rawProcs.Rows[i]["ProcStatus"].ToString())).ToString());
				row["ProcStatus"]=rawProcs.Rows[i]["ProcStatus"].ToString();
				row["prov"]=rawProcs.Rows[i]["Abbr"].ToString();
				row["RxNum"]=0;
				row["Surf"]=rawProcs.Rows[i]["Surf"].ToString();
				row["toothNum"]=Tooth.ToInternat(rawProcs.Rows[i]["ToothNum"].ToString());
				row["ToothNum"]=rawProcs.Rows[i]["ToothNum"].ToString();
				row["ToothRange"]=rawProcs.Rows[i]["ToothRange"].ToString();
				if(rawProcs.Rows[i]["ProcNumLab"].ToString()=="0"){//normal proc
					rows.Add(row);
				}
				else{
					row["description"]="-----"+row["description"].ToString();
					labRows.Add(row);//these will be added in the loop at the end
				}
			}
			//Commlog-----------------------------------------------------------------------------------------------------------
			command="SELECT CommlogNum,CommDateTime,CommType,Note FROM commlog WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY CommDateTime";
			DataTable rawComm=dcon.GetTable(command);
			for(int i=0;i<rawComm.Rows.Count;i++) {
				row=table.NewRow();
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][6].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
				row["description"]=Lan.g("ChartModule","Comm - ")
					+Lan.g("enumCommItemType",((CommItemType)PIn.PInt(rawComm.Rows[i]["CommType"].ToString())).ToString());
				row["note"]=rawComm.Rows[i]["Note"].ToString();
				dateT=PIn.PDateT(rawComm.Rows[i]["CommDateTime"].ToString());
				if(dateT.Year<1880){
					row["procDate"]="";
				}
				else{
					row["procDate"]=dateT.ToShortDateString();
				}
				row["ProcDate"]=dateT;
				row["ProcNum"]=0;
				row["RxNum"]=0;
				rows.Add(row);
			}
			//Rx------------------------------------------------------------------------------------------------------------------
			command="SELECT RxNum,RxDate,Drug,Disp,ProvNum,Notes FROM rxpat WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY RxDate";
			DataTable rawRx=dcon.GetTable(command);
			for(int i=0;i<rawRx.Rows.Count;i++) {
				row=table.NewRow();
				row["colorBackG"]=Color.White.ToArgb();
				row["colorText"]=DefB.Long[(int)DefCat.ProgNoteColors][5].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=0;
				row["description"]=Lan.g("ChartModule","Rx - ")+rawRx.Rows[i]["Drug"].ToString()+" - #"+rawRx.Rows[i]["Disp"].ToString();
				row["note"]=rawRx.Rows[i]["Notes"].ToString();
				dateT=PIn.PDate(rawRx.Rows[i]["RxDate"].ToString());
				if(dateT.Year<1880) {
					row["procDate"]="";
				}
				else {
					row["procDate"]=dateT.ToShortDateString();
				}
				row["ProcDate"]=dateT;
				row["ProcNum"]=0;
				row["RxNum"]=rawRx.Rows[i]["RxNum"].ToString();
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
			for(int i=0;i<rows.Count;i++){
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>The supplied DataRows must include the following columns: ProcNum,ProcDate,Priority,ToothRange,ToothNum,ADACode.
		///This sorts all objects in Chart module based on their dates, times, priority, and toothnum.</summary>
		public static int CompareChartRows(DataRow x,DataRow y){
			if(x["ProcNum"].ToString()!="0" && y["ProcNum"].ToString()!="0") {//if both are procedures
				if(((DateTime)x["ProcDate"]).Date==((DateTime)y["ProcDate"]).Date) {//and the dates are the same
					return ProcedureB.CompareProcedures(x,y);
					//IComparer procComparer=new ProcedureComparer();
					//return procComparer.Compare(x,y);//sort by priority, toothnum, adaCode
					//return 0;
				}
			}
			//In all other situations, all we care about is the dates.
			return ((DateTime)x["ProcDate"]).Date.CompareTo(((DateTime)y["ProcDate"]).Date);
			//IComparer myComparer = new ObjectDateComparer();
			//return myComparer.Compare(x,y);
		}

		
	






	}


	

	public class DtoChartModuleGetAll:DtoQueryBase {
		public int PatNum;
		public bool IsAuditMode;
	}

}
