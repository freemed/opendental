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
		private string rate2Time;
		private string totalTime2;
		private string overTime2;
		private string rate2Time2;
		private int PagesPrinted;
		private bool HeadingPrinted;

		public FormTimeCardManage() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTimeCardManage_Load(object sender,EventArgs e) {
			SelectedPayPeriod=PayPeriods.GetForDate(DateTimeOD.Today);
			if(SelectedPayPeriod==-1) {
				MsgBox.Show(this,"At least one pay period needs to exist before you can manage time cards.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			FillPayPeriod();
			FillMain();
			//butCompute.Visible=false;			//only until unit tests are complete. exceed
			//butDaily.Visible=false;			//only until unit tests are complete.
		}

		private void FillMain() {
			MainTable=ClockEvents.GetTimeCardManage(DateStart,DateStop);//,false);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Employee"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Total Hrs"),75);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Rate1"),75);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Rate1 OT"),75);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Rate2"),75);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Rate2 OT"),75);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Auto Adj"),64);
			//col.TextAlign=HorizontalAlignment.Right;
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Reg Adj"),64);
			//col.TextAlign=HorizontalAlignment.Right;
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"OT Adj"),64);
			//col.TextAlign=HorizontalAlignment.Right;
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Diff Adj"),64);
			//col.TextAlign=HorizontalAlignment.Right;
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Diff Auto"),64);
			//col.TextAlign=HorizontalAlignment.Right;
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Breaks"),64);
			//col.TextAlign=HorizontalAlignment.Right;
			//gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Notes"),0);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<MainTable.Rows.Count;i++) {
				row=new ODGridRow();
				//row.Cells.Add(Employees.GetNameFL(PIn.Long(MainTable.Rows[i]["EmployeeNum"].ToString())));
				row.Cells.Add(MainTable.Rows[i]["lastName"]+", "+MainTable.Rows[i]["firstName"]);
				if(PrefC.GetBool(PrefName.TimeCardsUseDecimalInsteadOfColon)) {
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["totalHours"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate1Hours"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate1OTHours"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate2Hours"].ToString()).TotalHours.ToString("n"));
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate2OTHours"].ToString()).TotalHours.ToString("n"));
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["BreakTime"].ToString()).TotalHours.ToString("n"));
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["ClockEventRegAdj"].ToString()).TotalHours.ToString("n"));
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["TimeAdjustRegAdj"].ToString()).TotalHours.ToString("n"));
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["TimeAdjustOTAdj"].ToString()).TotalHours.ToString("n"));
				}
				else {
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["totalHours"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate1Hours"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate1OTHours"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate2Hours"].ToString()).ToStringHmm());
					row.Cells.Add(PIn.Time(MainTable.Rows[i]["rate2OTHours"].ToString()).ToStringHmm());
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["BreakTime"].ToString()).ToStringHmm());
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["ClockEventRegAdj"].ToString()).ToStringHmm());
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["TimeAdjustRegAdj"].ToString()).ToStringHmm());
					//row.Cells.Add(PIn.Time(MainTable.Rows[i]["TimeAdjustOTAdj"].ToString()).ToStringHmm());
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

		///<summary>This is a modified version of FormTimeCard.FillMain().  It fills one time card per employee.</summary>
		private ODGrid GetGridForPrinting(Employee emp) {
			ODGrid gridTimeCard=new ODGrid();
			List<ClockEvent> clockEventList=ClockEvents.Refresh(emp.EmployeeNum,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text),false);
			List<TimeAdjust> timeAdjustList=TimeAdjusts.Refresh(emp.EmployeeNum,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
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
			col=new ODGridColumn(Lan.g(this,"In"),60,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Out"),60,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Total"),50,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Adjust"),55,HorizontalAlignment.Right);
			gridTimeCard.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Rate2"),55,HorizontalAlignment.Right);
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
			TimeSpan rate2span=new TimeSpan(0);//rate2 hours total
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
					//deprecated
					//status--------------------------------------
					//row.Cells.Add(clock.ClockStatus.ToString());
					//in------------------------------------------
					row.Cells.Add(clock.TimeDisplayed1.ToShortTimeString());
					if(clock.TimeEntered1!=clock.TimeDisplayed1){
						row.Cells[row.Cells.Count-1].ColorText = Color.Red;
					}
					//out-----------------------------
					if(clock.TimeDisplayed2.Year<1880){
						row.Cells.Add("");//not clocked out yet
					}
					else{
						row.Cells.Add(clock.TimeDisplayed2.ToShortTimeString());
						if (clock.TimeEntered2!=clock.TimeDisplayed2)
						{
							row.Cells[row.Cells.Count-1].ColorText = Color.Red;
						}
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
					if(clock.AdjustIsOverridden) {
						row.Cells[row.Cells.Count-1].ColorText = Color.Red;
					}
					//Rate2---------------------------------
					if(clock.Rate2Hours!=TimeSpan.FromHours(-1)) {
						rate2span+=clock.Rate2Hours;
						row.Cells.Add(ClockEvents.Format(clock.Rate2Hours));
						row.Cells[row.Cells.Count-1].ColorText = Color.Red;
					}
					else {
						rate2span+=clock.Rate2Auto;
						row.Cells.Add(ClockEvents.Format(clock.Rate2Auto));
					}
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
					if(clock.OTimeHours!=TimeSpan.FromHours(-1)) {//overridden
						row.Cells[row.Cells.Count-1].ColorText = Color.Red;
					}
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
						|| cal.GetWeekOfYear(GetDateForRow(i+1,mergedAL),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))//or the next row has a
						!= cal.GetWeekOfYear(clock.TimeDisplayed1.Date,rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek)))//different week of year
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
					//Deprecated
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
					//Rate2-------------------------------
					row.Cells.Add("");//
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
						|| cal.GetWeekOfYear(GetDateForRow(i+1,mergedAL),rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek))//or the next row has a
						!= cal.GetWeekOfYear(adjust.TimeEntry.Date,rule,(DayOfWeek)PrefC.GetInt(PrefName.TimeCardOvertimeFirstDayOfWeek)))//different week of year
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
					row.Cells.Add("(Adjust)"+adjust.Note);//used to indicate adjust rows.
					row.Cells[row.Cells.Count-1].ColorText=Color.Red;
				}
				gridTimeCard.Rows.Add(row);
			}
			gridTimeCard.EndUpdate();
			totalTime=periodSpan.ToStringHmm();
			overTime=otspan.ToStringHmm();
			rate2Time=rate2span.ToStringHmm();
			totalTime2=periodSpan.TotalHours.ToString("n");
			overTime2=otspan.TotalHours.ToString("n");
			rate2Time2=rate2span.TotalHours.ToString("n");
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

		//Prints one timecard for each employee.
		private void butPrintAll_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			FormPrintPreview pView=new FormPrintPreview(PrintSituation.Default,pd,gridMain.Rows.Count,0,"Employee timecards printed");
			pView.ShowDialog();
		}

		private void pd2_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			PrintEveryTimeCard(sender,e);
		}

		private void PrintEveryTimeCard(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			//A preview of every single emp on their own page will show up. User will print from there.
			Graphics g=e.Graphics;
			Employee employeeCur=Employees.GetEmp(PIn.Long(MainTable.Rows[pagesPrinted]["EmployeeNum"].ToString()));
			ODGrid timeCardGrid=GetGridForPrinting(employeeCur);
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
			//colW[2]=50;//altered
			colW[2]=60;//in
			colW[3]=60;//out
			colW[4]=50;//total
			colW[5]=50;//adjust
			colW[6]=50;//Rate2 //added
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
			//ColCaption[2]=Lan.g(this,"Altered");
			ColCaption[2]=Lan.g(this,"In");
			ColCaption[3]=Lan.g(this,"Out");
			ColCaption[4]=Lan.g(this,"Total");
			ColCaption[5]=Lan.g(this,"Adjust");
			ColCaption[6]=Lan.g(this,"Rate 2");
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
			yPos+=16;
			g.DrawString(Lan.g(this,"Rate 2 Time")+": "+rate2Time+" ("+rate2Time2+")",fontHeader,brush,xPos,yPos);
			pagesPrinted++;
			if(gridMain.Rows.Count==pagesPrinted) {
				pagesPrinted=0;
				e.HasMorePages=false;
			}
			else {
				e.HasMorePages=true;
			}
		}

		///<summary>Print timecards for selected employees only.</summary>
		private void butPrintSelected_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd2_PrintPageSelective);
			FormPrintPreview pView=new FormPrintPreview(PrintSituation.Default,pd,gridMain.SelectedIndices.Length,0,"Employee timecards printed");
			pView.ShowDialog();
		}

		///<summary>Similar to pd2_PrintPage except it iterates through selected indices instead of all indices.</summary>
		private void pd2_PrintPageSelective(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			PrintEmployeeTimeCard(sender,e);
		}

		private void PrintEmployeeTimeCard(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			//A preview of every single emp on their own page will show up. User will print from there.
			Graphics g=e.Graphics;
			Employee employeeCur=Employees.GetEmp(PIn.Long(MainTable.Rows[gridMain.SelectedIndices[pagesPrinted]]["EmployeeNum"].ToString()));
			ODGrid timeCardGrid=GetGridForPrinting(employeeCur);
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
			//colW[2]=50;//altered
			colW[2]=60;//in
			colW[3]=60;//out
			colW[4]=50;//total
			colW[5]=50;//adjust
			colW[6]=50;//Rate2 //added
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
			//ColCaption[2]=Lan.g(this,"Altered");
			ColCaption[2]=Lan.g(this,"In");
			ColCaption[3]=Lan.g(this,"Out");
			ColCaption[4]=Lan.g(this,"Total");
			ColCaption[5]=Lan.g(this,"Adjust");
			ColCaption[6]=Lan.g(this,"Rate 2");
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
			yPos+=16;
			g.DrawString(Lan.g(this,"Rate 2 Time")+": "+rate2Time+" ("+rate2Time2+")",fontHeader,brush,xPos,yPos);
			pagesPrinted++;
			if(gridMain.SelectedIndices.Length==pagesPrinted) {
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
			if(!Security.IsAuthorized(Permissions.UserQuery)) {
				return;
			}
			//Basically a preview of gridMain (every employee on one page), allow user to export as excel sheet or print it.
			string query=ClockEvents.GetTimeCardManageCommand(DateStart,DateStop,true);//true to get extra columns for printing.
			ReportSimpleGrid rsg=new ReportSimpleGrid();
			rsg.Query=query;
			FormQuery FormQ=new FormQuery(rsg);
			FormQ.textQuery.Text=query;
			FormQ.SubmitQuery();
			FormQ.ShowDialog();
		}

		private void butDaily_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"No employees selected. Would you like to run calculations for all employees?")) {
					return;
				}
				gridMain.SetSelected(true);
			}
			Cursor=Cursors.WaitCursor;
			string aggregateErrors="";
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				try {
					TimeCardRules.CalculateDailyOvertime(Employees.GetEmp(PIn.Long(MainTable.Rows[gridMain.SelectedIndices[i]]["EmployeeNum"].ToString()))
						,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
				}
				catch(Exception ex) {
					aggregateErrors+=ex.Message+"\r\n";
				}
			}
			Cursor=Cursors.Default;
			//Cache selected indicies, fill grid, reselect indicies.
			List<int> listSelectedIndexCach=new List<int>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				listSelectedIndexCach.Add(gridMain.SelectedIndices[i]);
			}
			FillMain();
			for(int i=0;i<listSelectedIndexCach.Count;i++) {
				gridMain.SetSelected(listSelectedIndexCach[i],true);
			}
			if(aggregateErrors=="") {
				MsgBox.Show(this,"Done.");
			}
			else {
				MessageBox.Show(this,Lan.g(this,"Timecards were not calculated for some Employees for the following reasons")+":\r\n"+aggregateErrors);
			}
		}

		private void butWeekly_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.TimecardsEditAll)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"No employees selected. Would you like to run calculations for all employees?")) {
					return;
				}
				gridMain.SetSelected(true);
			}
			Cursor=Cursors.WaitCursor;
			string aggregateErrors="";
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				try {
					TimeCardRules.CalculateWeeklyOvertime(Employees.GetEmp(PIn.Long(MainTable.Rows[gridMain.SelectedIndices[i]]["EmployeeNum"].ToString()))
						,PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
				}
				catch(Exception ex) {
					aggregateErrors+=ex.Message+"\r\n";
				}
			}
			Cursor=Cursors.Default;
			//Cache selected indices, fill grid, reselect indices.
			List<int> listSelectedIndexCach=new List<int>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				listSelectedIndexCach.Add(gridMain.SelectedIndices[i]);
			}
			FillMain();
			for(int i=0;i<listSelectedIndexCach.Count;i++) {
				gridMain.SetSelected(listSelectedIndexCach[i],true);
			}
			//Done or Error messages.
			if(aggregateErrors=="") {
				MsgBox.Show(this,"Done.");
			}
			else {
				MessageBox.Show(this,Lan.g(this,"Timecards were not calculated for some Employees for the following reasons")+":\r\n"+aggregateErrors);
			}
		}

		private void butClearManual_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"This cannot be undone. Would you like to continue?")) {
				return;
			}
			//List<Employee> employeesList = new List<Employee>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				try {
					TimeCardRules.ClearManual(PIn.Long(MainTable.Rows[gridMain.SelectedIndices[i]]["EmployeeNum"].ToString()),PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
				}
				catch(Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}
			//Cach selected indicies, fill grid, reselect indicies.
			List<int> listSelectedIndexCach=new List<int>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				listSelectedIndexCach.Add(gridMain.SelectedIndices[i]);
			}
			FillMain();
			for(int i=0;i<listSelectedIndexCach.Count;i++) {
				gridMain.SetSelected(listSelectedIndexCach[i],true);
			}
		}

		private void butClearAuto_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This cannot be undone, but you can run the Calc buttons again later.  Would you like to continue?")) {
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				try {
					TimeCardRules.ClearAuto(PIn.Long(MainTable.Rows[gridMain.SelectedIndices[i]]["EmployeeNum"].ToString()),PIn.Date(textDateStart.Text),PIn.Date(textDateStop.Text));
				}
				catch(Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}
			//Cach selected indicies, fill grid, reselect indicies.
			List<int> listSelectedIndexCach=new List<int>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				listSelectedIndexCach.Add(gridMain.SelectedIndices[i]);
			}
			FillMain();
			for(int i=0;i<listSelectedIndexCach.Count;i++) {
				gridMain.SetSelected(listSelectedIndexCach[i],true);
			}
		}

		///<summary>Print exactly what is showing in gridMain. (Including rows that do not fit in the UI.)</summary>
		private void butPrintGrid_Click(object sender,EventArgs e) {
			PagesPrinted=0;
			HeadingPrinted=false;
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			#if DEBUG
				FormRpPrintPreview pView = new FormRpPrintPreview();
				pView.printPreviewControl2.Document=pd;
				pView.ShowDialog();
			#else
				if(!PrinterL.SetPrinter(pd,PrintSituation.Default,0,"Printed employee time card grid.")) {
					return;
				}
				try{
					pd.Print();
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
				}
			#endif
		}

		private void pd_PrintPage(object sender,PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			int y=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			int headingPrintH=0;
			if(!HeadingPrinted) {
				text=Lan.g(this,"Heading Text");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,y);
				y+=25;
				HeadingPrinted=true;
				headingPrintH=y;
			}
			#endregion
			y=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			PagesPrinted++;
			if(y==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		///<summary>Exports MainTable (a data table) not the actual OD Grid. This allows for EmployeeNum and ADPNum without having to perform any lookups.</summary>
		private void butExportGrid_Click(object sender,EventArgs e) {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			if(fbd.ShowDialog()!=DialogResult.OK) {
				return;
			}
			StringBuilder strb = new StringBuilder();
			string headers="";
			for(int i=0;i<MainTable.Columns.Count;i++) {
				headers+=(i>0?"\t":"")+MainTable.Columns[i].ColumnName;
			}
			strb.AppendLine(headers);
			for(int i=0;i<MainTable.Rows.Count;i++) {
				string row="";
				for(int c=0;c<MainTable.Columns.Count;c++) {
					if(c>0) {
						row+="\t";
					}
					switch(MainTable.Columns[c].ColumnName) {
						case "PayrollID":
						case "EmployeeNum":
						case "firstName":
						case "lastName":
						case "Note":
							row+=MainTable.Rows[i][c].ToString().Replace("\t","").Replace("\r\n",";  ");
							break;
						case "totalHours":
						case "rate1Hours":
						case "rate1OTHours":
						case "rate2Hours":
						case "rate2OTHours":
							//Time must me formatted differently.
							if(PrefC.GetBool(PrefName.TimeCardsUseDecimalInsteadOfColon)) {
								row+=PIn.Time(MainTable.Rows[i][c].ToString()).TotalHours.ToString("n");
							}
							else {
								row+=PIn.Time(MainTable.Rows[i][c].ToString()).ToStringHmm();
							}
							break;
						default:
							//should never happen.
							throw new Exception("Unexpected column found in payroll table : "+MainTable.Columns[c].ColumnName);
					}//end switch
				}//end columns
				strb.AppendLine(row);
			}
			string fileName="ODPayroll"+DateTime.Now.ToString("yyyyMMdd_hhmmss")+".TXT";
			try {
				System.IO.File.WriteAllText(fbd.SelectedPath+"\\"+fileName,strb.ToString());
				MessageBox.Show(this,Lan.g(this,"File created")+" : "+fbd.SelectedPath+"\\"+fileName);
			}
			catch(Exception ex) {
				MessageBox.Show(this,"File not created:\r\n"+ex.Message);
			}
		}

		///<summary>Validates format and values and provides aggregate error and warning messages. Will save malformed files anyways.</summary>
		private void butExportADP_Click(object sender,EventArgs e) {
			StringBuilder strb = new StringBuilder();
			string errors="";
			string warnings="";
			string errorIndent="  ";
			strb.AppendLine("Co Code,Batch ID,File #,Reg Hours,O/T Hours,Shift");
			string coCode=PrefC.GetString(PrefName.ADPCompanyCode);
			string batchID=DateStop.ToString("yyyyMMdd");//max 8 characters
			if(coCode.Length<2 || coCode.Length>3){
				errors+=errorIndent+"Company code must be two to three alpha numeric characters long.  Go to Setup>TimeCards to edit.\r\n";
			}
			coCode=coCode.PadRight(3,'_');//for two digit company codes.
			for(int i=0;i<MainTable.Rows.Count;i++) {
				string errorsForEmployee="";
				string warningsForEmployee="";
				string fileNum="";
				fileNum=MainTable.Rows[i]["PayrollID"].ToString();
				try {
					if(PIn.Int(fileNum)<51 || PIn.Int(fileNum)>999999) {
						errorsForEmployee+=errorIndent+"Payroll ID not between 51 and 999999.\r\n";
					}
				}
				catch (Exception ex){
					//same error message as above.
					errorsForEmployee+=errorIndent+"Payroll ID not between 51 and 999999.\r\n";
				}
				if(fileNum.Length>6) {
					errorsForEmployee+=errorIndent+"Payroll ID must be less than 6 digits long.\r\n";
				}
				else {//pad payrollIDs that are too short. No effect if payroll ID is 6 digits long.
					fileNum=fileNum.PadLeft(6,'0');
				}
				string r1hours	=(PIn.TSpan(MainTable.Rows[i]["rate1Hours"  ].ToString())).TotalHours.ToString("F4").Replace("0.0000","");//adp allows 4 digit precision
				string r1OThours=(PIn.TSpan(MainTable.Rows[i]["rate1OTHours"].ToString())).TotalHours.ToString("F4").Replace("0.0000","");//adp allows 4 digit precision
				string r2hours	=(PIn.TSpan(MainTable.Rows[i]["rate2Hours"  ].ToString())).TotalHours.ToString("F4").Replace("0.0000","");//adp allows 4 digit precision
				string r2OThours=(PIn.TSpan(MainTable.Rows[i]["rate2OTHours"].ToString())).TotalHours.ToString("F4").Replace("0.0000","");//adp allows 4 digit precision
				string textToAdd="";
				if(r1hours!="" || r1OThours!="") {//no entry should be made unless there are actually hours for this employee.
					textToAdd+=coCode+","+batchID+","+fileNum+","+r1hours+","+r1OThours+",2\r\n";
				}
				if(r2hours!="" || r2OThours!="") {//no entry should be made unless there are actually hours for this employee.
					textToAdd+=coCode+","+batchID+","+fileNum+","+r2hours+","+r2OThours+",3\r\n";
				}
				if(textToAdd=="") {
					warningsForEmployee+=errorIndent+"No clocked hours.\r\n";// for "+Employees.GetNameFL(Employees.GetEmp(PIn.Long(MainTable.Rows[i]["EmployeeNum"].ToString())))+"\r\n";
				}
				else {
					strb.Append(textToAdd);
				}
				//validate characters in text.  Allowed values are 32 to 91 and 93 to 122----------------------------------------------------------------
				for(int j=0;j<textToAdd.Length;j++) {
					int charAsInt=(int)textToAdd[j];
					//these are the characters explicitly allowed by ADP per thier documentation.
					if(charAsInt>=32 && charAsInt<=122 && charAsInt!=92) {//
						continue;//valid character
					}
					if(charAsInt==10 || charAsInt==13) {//CR LF, not allowed as values but allowed to deliniate rows.
						continue;//valid character
					}
					errorsForEmployee+="Invalid character found (ASCII="+charAsInt+"): "+textToAdd.Substring(j,1)+".\r\n";
				}
				//Aggregate employee errors into aggregate error messages.--------------------------------------------------------------------------------
				if(errorsForEmployee!="") {
					errors+=Employees.GetNameFL(Employees.GetEmp(PIn.Long(MainTable.Rows[i]["EmployeeNum"].ToString())))+":\r\n"+errorsForEmployee+"\r\n";
				}
				if(warningsForEmployee!="") {
					warnings+=Employees.GetNameFL(Employees.GetEmp(PIn.Long(MainTable.Rows[i]["EmployeeNum"].ToString())))+":\r\n"+warningsForEmployee+"\r\n";
				}
			}
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			if(fbd.ShowDialog()!=DialogResult.OK){
				return;
			}
			string fileSuffix="";
			for(int i=0;i<=1297;i++) {//1296=36*36 to represent all acceptable suffixes for file name consisting of two alphanumeric digits; +1 to catch error. (A-Z, 0-9)
				fileSuffix="";
				//generate suffix from i
				if(i==1297) {
					//could not find acceptable file name.
					fileSuffix="NamingError";
					break;
				}
				if(i/36<10) {
					fileSuffix+=(i/36);//truncated to int on purpose.  (0 to 9)
				}
				else {
					fileSuffix+=(Char)((i/36)-10+65);//65='A' in ASCII.  (A to Z)
				}
				if(i%36<10){
					fileSuffix+=(i%36);//(0 to 9)
				}
				else {
					fileSuffix+=(Char)((i%36)-10+65);//65='A' in ASCII.  (A to Z)
				}
				//File suffix is now a a two digit alphanumeric string.
				if(!System.IO.File.Exists(fbd.SelectedPath+"\\EPI"+coCode+fileSuffix+".CSV")){
					break;
				}
			}
			try {
				System.IO.File.WriteAllText(fbd.SelectedPath+"\\EPI"+coCode+fileSuffix+".CSV",strb.ToString());
				if(errors!="") {
					MessageBox.Show("The following errors will prevent ADP from properly processing this export:\r\n"+errors);
				}
				if(warnings!="") {
					MessageBox.Show("The following warnings were detected:\r\n"+warnings);
				}
				MessageBox.Show(this,Lan.g(this,"File created")+" : "+fbd.SelectedPath+"\\EPI"+coCode+fileSuffix+".CSV");
			}
			catch(Exception ex) {
				MessageBox.Show(this,"File not created:\r\n"+ex.Message);
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}


	}
}