using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormTimeCardManage:Form {
		private int SelectedPayPeriod;
		private DateTime DateStart;
		private DateTime DateStop;
		private DataTable MainTable;
		private int pagesPrinted;
		private string totalTime;
		private string overTime;
		private string totalTime2;
		private string overTime2;

		public FormTimeCardManage() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTimeCardManage_Load(object sender,EventArgs e) {
			SelectedPayPeriod=PayPeriods.GetForDate(DateTime.Today);
			if(SelectedPayPeriod==-1) {
				MsgBox.Show(this,"At least one pay period needs to exist before you can manage time cards.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			FillPayPeriod();
			FillMain();
			//butCompute.Visible=false;			//only until unit tests are complete.
			//butDaily.Visible=false;			//only until unit tests are complete.
		}

		private void FillMain() {
			MainTable=ClockEvents.GetTimeCardManage(DateStart,DateStop);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Employee"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Total Hrs"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Reg Hrs"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"OT Hrs"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Auto Adj"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Reg Adj"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"OT Adj"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Breaks"),64);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Notes"),0);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<MainTable.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Employees.GetNameFL(PIn.Long(MainTable.Rows[i]["EmployeeNum"].ToString())));
				if(PrefC.GetBool(PrefName.TimeCardsUseDecimalInsteadOfColon)) {
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["tempTotalTime"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["tempRegHrs"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["tempOverTime"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["AdjEvent"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["AdjReg"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["AdjOTime"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["BreakTime"].ToString()).TotalHours.ToString("n"));
				}
				else {
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["tempTotalTime"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["tempRegHrs"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["tempOverTime"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["AdjEvent"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["AdjReg"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["AdjOTime"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["BreakTime"].ToString()).ToStringHmm());
				}
				row.Cells.Add(MainTable.Rows[i]["Note"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		///<summary>SelectedPayPeriod should already be set.  This simply fills the screen with that data.</summary>
		private void FillPayPeriod() {
			DateStart=PayPeriods.List[SelectedPayPeriod].DateStart;
			DateStop=PayPeriods.List[SelectedPayPeriod].DateStop;
			textDateStart.Text=DateStart.ToShortDateString();
			textDateStop.Text=DateStop.ToShortDateString();
			if(PayPeriods.List[SelectedPayPeriod].DatePaycheck.Year<1880) {
				textDatePaycheck.Text="";
			}
			else {
				textDatePaycheck.Text=PayPeriods.List[SelectedPayPeriod].DatePaycheck.ToShortDateString();
			}
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormTimeCard FormTC=new FormTimeCard();
			FormTC.EmployeeCur=Employees.GetEmp(PIn.Long(MainTable.Rows[e.Row]["EmployeeNum"].ToString()));
			FormTC.SelectedPayPeriod=SelectedPayPeriod;
			FormTC.ShowDialog();
			FillMain();
		}

		///<summary>Only used for printing all employee time cards at once.</summary>
		private ODGrid GetGridTimeCard(Employee emp) {
			ODGrid gridTimeCard=new ODGrid();
			List<ClockEvent> clockEventList=ClockEvents.Refresh(emp.EmployeeNum,PIn.Date(textDateStart.Text),
				PIn.Date(textDateStop.Text),false);
			List<TimeAdjust> timeAdjustList=TimeAdjusts.Refresh(emp.EmployeeNum,PIn.Date(textDateStart.Text),
				PIn.Date(textDateStop.Text));
			ArrayList mergedAL=new ArrayList();
			for(int i=0;i<clockEventList.Count;i++) {
				mergedAL.Add(clockEventList[i]);
			}
			for(int i=0;i<timeAdjustList.Count;i++) {
				mergedAL.Add(timeAdjustList[i]);
			}
			IComparer myComparer=new ObjectDateComparer();
			mergedAL.Sort(myComparer);
			gridTimeCard.BeginUpdate();
			gridTimeCard.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),70);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Weekday"),70);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Altered"),50,HorizontalAlignment.Center);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"In"),60,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Out"),60,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Total"),50,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Adjust"),55,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Overtime"),55,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Daily"),50,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Weekly"),50,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),5);
			gridTimeCard.Columns.Add(col);
			gridTimeCard.Rows.Clear();
			ODGridRow row;
			TimeSpan[] weeklyTotals=new TimeSpan[mergedAL.Count];
			TimeSpan alteredSpan=new TimeSpan(0);//used to display altered times
			TimeSpan oneSpan=new TimeSpan(0);//used to sum one pair of clock-in/clock-out
			TimeSpan oneAdj;
			TimeSpan oneOT;
			TimeSpan daySpan=new TimeSpan(0);//used for daily totals.
			TimeSpan weekSpan=new TimeSpan(0);//used for weekly totals.
			if(mergedAL.Count>0){
				weekSpan=ClockEvents.GetWeekTotal(emp.EmployeeNum,GetDateForRow(0,mergedAL));
			}
			TimeSpan periodSpan=new TimeSpan(0);//used to add up totals for entire page.
			TimeSpan otspan=new TimeSpan(0);//overtime for the entire period
      Calendar cal=CultureInfo.CurrentCulture.Calendar;
			CalendarWeekRule rule=CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
			DateTime curDate=DateTime.MinValue;
			DateTime previousDate=DateTime.MinValue;
			Type type;
			ClockEvent clock;
			TimeAdjust adjust;
			for(int i=0;i<mergedAL.Count;i++){
				row=new ODGridRow();
				type=mergedAL[i].GetType();
				row.Tag=mergedAL[i];
				previousDate=curDate;
				//clock event row---------------------------------------------------------------------------------------------
				if(type==typeof(ClockEvent)){
					clock=(ClockEvent)mergedAL[i];
					curDate=clock.TimeDisplayed1.Date;
					if(curDate==previousDate){
						row.Cells.Add("");
						row.Cells.Add("");
					}
					else{
						row.Cells.Add(curDate.ToShortDateString());
						row.Cells.Add(curDate.DayOfWeek.ToString());
					}
					//altered--------------------------------------
					string str="";
					if(clock.TimeEntered1!=clock.TimeDisplayed1){
						str=Lan.g(this,"in");
					}
					if(clock.TimeEntered2!=clock.TimeDisplayed2){
						if(str!="") {
							str+="/";
						}
						str+=Lan.g(this,"out");
					}
					row.Cells.Add(str);
					//status--------------------------------------
					//row.Cells.Add(clock.ClockStatus.ToString());
					//in------------------------------------------
					row.Cells.Add(clock.TimeDisplayed1.ToShortTimeString());
					//out-----------------------------
					if(clock.TimeDisplayed2.Year<1880){
						row.Cells.Add("");//not clocked out yet
					}
					else{
						row.Cells.Add(clock.TimeDisplayed2.ToShortTimeString());
					}
					//total-------------------------------
					if(clock.TimeDisplayed2.Year<1880){
						row.Cells.Add("");
					}
					else{
						oneSpan=clock.TimeDisplayed2-clock.TimeDisplayed1;
						row.Cells.Add(ClockEvents.Format(oneSpan));
						daySpan+=oneSpan;
						weekSpan+=oneSpan;
						periodSpan+=oneSpan;
					}
					//Adjust---------------------------------
					oneAdj=TimeSpan.Zero;
					if(clock.AdjustIsOverridden) {
						oneAdj+=clock.Adjust;
					}
					else {
						oneAdj+=clock.AdjustAuto;//typically zero
					}
					daySpan+=oneAdj;
					weekSpan+=oneAdj;
					periodSpan+=oneAdj;
					row.Cells.Add(ClockEvents.Format(oneAdj));
					//Overtime------------------------------
					oneOT=TimeSpan.Zero;
					if(clock.OTimeHours!=TimeSpan.FromHours(-1)) {//overridden
						oneOT=clock.OTimeHours;
					}
					else {
						oneOT=clock.OTimeAuto;//typically zero
					}
					otspan+=oneOT;
					daySpan-=oneOT;
					weekSpan-=oneOT;
					periodSpan-=oneOT;
					row.Cells.Add(ClockEvents.Format(oneOT));
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
						|| GetDateForRow(i+1,mergedAL) != curDate)//or the next row is a different date
					{
						row.Cells.Add(ClockEvents.Format(daySpan));
						daySpan=new TimeSpan(0);
					}
					else{//not the last entry for the day
						row.Cells.Add("");
					}
					//Weekly-------------------------------------
					weeklyTotals[i]=weekSpan;
					//if this is the last entry for a given week
					if(i==mergedAL.Count-1//if this is the last row 
						|| cal.GetWeekOfYear(GetDateForRow(i+1,mergedAL),rule,DayOfWeek.Sunday)//or the next row has a
						!= cal.GetWeekOfYear(clock.TimeDisplayed1.Date,rule,DayOfWeek.Sunday))//different week of year
					{
						row.Cells.Add(ClockEvents.Format(weekSpan));
						weekSpan=new TimeSpan(0);
					}
					else {
						//row.Cells.Add(ClockEvents.Format(weekSpan));
						row.Cells.Add("");
					}
					//Note-----------------------------------------
					row.Cells.Add(clock.Note);
				}
				//adjustment row--------------------------------------------------------------------------------------
				else if(type==typeof(TimeAdjust)){
					adjust=(TimeAdjust)mergedAL[i];
					curDate=adjust.TimeEntry.Date;
					if(curDate==previousDate){
						row.Cells.Add("");
						row.Cells.Add("");
					}
					else{
						row.Cells.Add(curDate.ToShortDateString());
						row.Cells.Add(curDate.DayOfWeek.ToString());
					}
					//altered--------------------------------------
					row.Cells.Add(Lan.g(this,"Adjust"));//2
					row.ColorText=Color.Red;
					//status--------------------------------------
					//row.Cells.Add("");//3
					//in/out------------------------------------------
					row.Cells.Add("");//4
					//time-----------------------------
					row.Cells.Add(adjust.TimeEntry.ToShortTimeString());//5
					//total-------------------------------
					row.Cells.Add("");//
					//Adjust------------------------------
					daySpan+=adjust.RegHours;//might be negative
					weekSpan+=adjust.RegHours;
					periodSpan+=adjust.RegHours;
					row.Cells.Add(ClockEvents.Format(adjust.RegHours));//6
					//Overtime------------------------------
					otspan+=adjust.OTimeHours;
					row.Cells.Add(ClockEvents.Format(adjust.OTimeHours));//7
					//Daily-----------------------------------
					//if this is the last entry for a given date
					if(i==mergedAL.Count-1//if this is the last row
						|| GetDateForRow(i+1,mergedAL) != curDate)//or the next row is a different date
					{
						row.Cells.Add(ClockEvents.Format(daySpan));//
						daySpan=new TimeSpan(0);
					}
					else{
						row.Cells.Add("");
					}
					//Weekly-------------------------------------
					weeklyTotals[i]=weekSpan;
					//if this is the last entry for a given week
					if(i==mergedAL.Count-1//if this is the last row 
						|| cal.GetWeekOfYear(GetDateForRow(i+1,mergedAL),rule,DayOfWeek.Sunday)//or the next row has a
						!= cal.GetWeekOfYear(adjust.TimeEntry.Date,rule,DayOfWeek.Sunday))//different week of year
					{
						ODGridCell cell=new ODGridCell(ClockEvents.Format(weekSpan));
						cell.ColorText=Color.Black;
						row.Cells.Add(cell);
						weekSpan=new TimeSpan(0);
					}
					else {
						row.Cells.Add("");
					}
					//Note-----------------------------------------
					row.Cells.Add(adjust.Note);
				}
				gridTimeCard.Rows.Add(row);
			}
			gridTimeCard.EndUpdate();
			totalTime=periodSpan.ToStringHmm();
			overTime=otspan.ToStringHmm();
			totalTime2=periodSpan.TotalHours.ToString("n");
			overTime2=otspan.TotalHours.ToString("n");
			return gridTimeCard;
		}

		private DateTime GetDateForRow(int i,ArrayList mergedAL){
			if(mergedAL[i].GetType()==typeof(ClockEvent)){
				return ((ClockEvent)mergedAL[i]).TimeDisplayed1.Date;
			}
			else if(mergedAL[i].GetType()==typeof(TimeAdjust)){
				return ((TimeAdjust)mergedAL[i]).TimeEntry.Date;
			}
			return DateTime.MinValue;
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			FormPrintPreview pView=new FormPrintPreview(PrintSituation.Default,pd,gridMain.Rows.Count);
			pView.ShowDialog();
		}

		private void pd2_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			PrintEveryTimeCard(sender,e);
		}

		private void PrintEveryTimeCard(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			//A preview of every single emp on their own page will show up. User will print from there.
			Graphics g=e.Graphics;
			Employee employeeCur=Employees.GetEmp(PIn.Long(MainTable.Rows[pagesPrinted]["EmployeeNum"].ToString()));
			ODGrid timeCardGrid=GetGridTimeCard(employeeCur);
			int linesPrinted=0;
			//Create a timecardgrid for this employee?
			float yPos=75;
			float xPos=55;
			string str;
			Font font=new Font(FontFamily.GenericSansSerif,8);
			Font fontTitle=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
			Font fontHeader=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
			SolidBrush brush=new SolidBrush(Color.Black);
			Pen pen=new Pen(Color.Black);
			//Title
			str=employeeCur.FName+" "+employeeCur.LName;
			g.DrawString(str,fontTitle,brush,xPos,yPos);
			yPos+=30;
			//define columns
			int[] colW=new int[11];
			colW[0]=70;//date
			colW[1]=70;//weekday
			colW[2]=50;//altered
			colW[3]=60;//in
			colW[4]=60;//out
			colW[5]=50;//total
			colW[6]=50;//adjust
			colW[7]=55;//overtime
			colW[8]=50;//daily
			colW[9]=50;//weekly
			colW[10]=160;//note
			int[] colPos=new int[colW.Length+1];
			colPos[0]=45;
			for(int i=1;i<colPos.Length;i++) {
				colPos[i]=colPos[i-1]+colW[i-1];
			}
			string[] ColCaption=new string[11];
			ColCaption[0]=Lan.g(this,"Date");
			ColCaption[1]=Lan.g(this,"Weekday");
			ColCaption[2]=Lan.g(this,"Altered");
			ColCaption[3]=Lan.g(this,"In");
			ColCaption[4]=Lan.g(this,"Out");
			ColCaption[5]=Lan.g(this,"Total");
			ColCaption[6]=Lan.g(this,"Adjust");
			ColCaption[7]=Lan.g(this,"Overtime");
			ColCaption[8]=Lan.g(this,"Daily");
			ColCaption[9]=Lan.g(this,"Weekly");
			ColCaption[10]=Lan.g(this,"Note");
			//column headers-----------------------------------------------------------------------------------------
			e.Graphics.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			e.Graphics.DrawRectangle(pen,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			for(int i=1;i<colPos.Length;i++) {
				e.Graphics.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+18);
			}
			//Prints the Column Titles
			for(int i=0;i<ColCaption.Length;i++) {
				e.Graphics.DrawString(ColCaption[i],fontHeader,brush,colPos[i]+2,yPos+1);
			}
			yPos+=18;
			while(yPos < e.PageBounds.Height-75-50-32-16 && linesPrinted < timeCardGrid.Rows.Count) {
				for(int i=0;i<colPos.Length-1;i++) {
					e.Graphics.DrawString(timeCardGrid.Rows[linesPrinted].Cells[i].Text,font,brush
						,new RectangleF(colPos[i]+2,yPos,colPos[i+1]-colPos[i]-5,font.GetHeight(e.Graphics)));
				}
				//Column lines		
				for(int i=0;i<colPos.Length;i++) {
					e.Graphics.DrawLine(Pens.Gray,colPos[i],yPos+16,colPos[i],yPos);
				}
				linesPrinted++;
				yPos+=16;
				e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
			}
			//totals will print on every page for simplicity
			yPos+=10;
			g.DrawString(Lan.g(this,"Regular Time")+": "+totalTime+" ("+totalTime2+")",fontHeader,brush,xPos,yPos);
			yPos+=16;
			g.DrawString(Lan.g(this,"Overtime")+": "+overTime+" ("+overTime2+")",fontHeader,brush,xPos,yPos);
			pagesPrinted++;
			if(gridMain.Rows.Count==pagesPrinted) {
				pagesPrinted=0;
				e.HasMorePages=false;
			}
			else {
				e.HasMorePages=true;
			}
		}

		private void butLeft_Click(object sender,EventArgs e) {
			if(SelectedPayPeriod==0){
				return;
			}
			SelectedPayPeriod--;
			FillPayPeriod();
			FillMain();
		}

		private void butRight_Click(object sender,EventArgs e) {
			if(SelectedPayPeriod==PayPeriods.List.Length-1) {
				return;
			}
			SelectedPayPeriod++;
			FillPayPeriod();
			FillMain();
		}

		private void butReport_Click(object sender,EventArgs e) {
			//Basically a preview of gridMain (every employee on one page), allow user to export as excel sheet or print it.
			string query=ClockEvents.GetTimeCardManageCommand(DateStart,DateStop);
			ReportSimpleGrid rsg=new ReportSimpleGrid();
			rsg.Query=query;
			FormQuery FormQ=new FormQuery(rsg);
			FormQ.textQuery.Text=query;
			FormQ.SubmitQuery();
			FormQ.ShowDialog();
		}

		private void butDaily_Click(object sender,EventArgs e) {
			//not even visible if viewing breaks.
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			List<Employee> employeesList = new List<Employee>();
			foreach(int index in gridMain.SelectedIndices) {
				foreach(Employee emp in Employees.ListLong) {
					if(emp.EmployeeNum.ToString()==MainTable.Rows[index]["EmployeeNum"].ToString()) {
						employeesList.Add(emp);
						break;//no need to check other employees, handle next selected index.
					}
				}
			}
			if(employeesList.Count==0) {//nothing in grid was selected so populate list with all non hidden employees.
				foreach(Employee emp in Employees.ListShort){
					employeesList.Add(emp);
				}
			}
			string errors="";
			foreach(Employee EmployeeCur in employeesList) {
				errors+=TimeCardRules.ValidatePayPeriod(EmployeeCur,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
			}
			if(errors != "") {
				MsgBox.Show(this,errors);
				Cursor=Cursors.Default;
				return;
			}
			foreach(Employee EmployeeCur in employeesList) {
				TimeCardRules.CalculateDailyOvertime(EmployeeCur,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
			}
			Cursor=Cursors.Default;
			//Cach selected indicies, fill grid, reselect indicies.
			List<int> listSelectedIndexCach=new List<int>();
			foreach(int ind in gridMain.SelectedIndices) {
				listSelectedIndexCach.Add(ind);
			}
			FillMain();
			foreach(int ind in listSelectedIndexCach) {
				gridMain.SetSelected(ind,true);
			}
		}

		private void butWeekly_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			List<Employee> employeesList = new List<Employee>();
			foreach(int selectedIndex in gridMain.SelectedIndices) {
				foreach(Employee emp in Employees.ListLong){
					if(emp.EmployeeNum.ToString()==MainTable.Rows[selectedIndex]["EmployeeNum"].ToString()) {
						employeesList.Add(emp);
						break;//no need to check other employees, handle next selected index.
					}
				}
			}
			if(employeesList.Count==0) {//nothing in grid was selected so populate list with all non hidden employees.
				foreach(Employee emp in Employees.ListShort) {
					employeesList.Add(emp);
				}
			}
			//if(gridMain.SelectedIndices.Length==0) {//Nothing selected, run on all employees.
			//  //MsgBox? for a warning?
			//  foreach(Employee emp in Employees.ListLong) {
			//    employeesList.Add(emp);
			//  }
			//}
			foreach(Employee EmployeeCur in employeesList) {
				TimeCardRules.CalculateWeeklyOvertime(EmployeeCur,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
			}
			Cursor=Cursors.Default;
			//Cach selected indicies, fill grid, reselect indicies.
			List<int> listSelectedIndexCach=new List<int>();
			foreach(int ind in gridMain.SelectedIndices) {
				listSelectedIndexCach.Add(ind);
			}
			FillMain();
			foreach(int ind in listSelectedIndexCach) {
				gridMain.SetSelected(ind,true);
			}
		}

		/*
		private void butDayAndWeek_Click(object sender,EventArgs e) {
			//not even visible if viewing breaks.
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			List<Employee> employeesList = new List<Employee>();
			foreach(int index in gridMain.SelectedIndices) {
				foreach(Employee emp in Employees.ListLong) {
					if(emp.EmployeeNum.ToString()==MainTable.Rows[index]["EmployeeNum"].ToString()) {
						employeesList.Add(emp);
						break;//no need to check other employees, handle next selected index.
					}
				}
			}
			foreach(Employee EmployeeCur in employeesList) {
				TimeCardRules.CalculateDailyOvertime(EmployeeCur,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
				TimeCardRules.CalculateWeeklyOvertime(EmployeeCur,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
			}
			Cursor=Cursors.Default;
			FillMain();
		}*/

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}