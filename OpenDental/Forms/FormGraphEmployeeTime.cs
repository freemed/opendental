using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormGraphEmployeeTime:Form {
		private List<PointF> listCalls;
		private float[] buckets;//a bucket can hold partial people.
		private DateTime DateShowing;
		private int[] minutesBehind;
		///<summary>Retrieved once when opening the form, then reused.</summary>
		private List<PhoneEmpDefault> ListPED;
		private List<Schedule> ListScheds;
		private List<Region> ListRegions;
		private int CurrentHoverRegionIdx=-1;
		///<summary>holds employee info gathered on paint and displayed on hover</summary>
		private Dictionary<int/*key = the bucket index*/,List<Employee>/*value = list of employees in this bucket*/> DictEmployeesPerBucket;

		public FormGraphEmployeeTime() {
			InitializeComponent();
			Lan.F(this);
			toolTip.ToolTipTitle=Lan.g(this,"Employees");
			ListRegions=new List<Region>();
		}

		private void FormGraphEmployeeTime_Load(object sender,EventArgs e) {
			butEdit.Visible=Security.IsAuthorized(Permissions.Schedules);
			ListPED=PhoneEmpDefaults.Refresh();
			DateShowing=AppointmentL.DateSelected.Date;
			//fill in the missing PhoneGraph entries for today
			PhoneGraphs.AddMissingEntriesForToday(ListPED);
			FillData();
		}

		private void FillData() {
			DictEmployeesPerBucket=new Dictionary<int,List<Employee>>();
			labelDate.Text=DateShowing.ToString("dddd, MMMM d");
			butEdit.Enabled=DateShowing.Date>=DateTime.Today; //do not allow editing of past dates
			listCalls=new List<PointF>();
			if(DateShowing.DayOfWeek==DayOfWeek.Friday) {
				listCalls.Add(new PointF(5f,0));
				listCalls.Add(new PointF(5.5f,50));//5-6am
				listCalls.Add(new PointF(6.5f,133));
				listCalls.Add(new PointF(7.5f,210));
				listCalls.Add(new PointF(8.5f,332));
				listCalls.Add(new PointF(9f,360));//-
				listCalls.Add(new PointF(9.5f,370));//was 380
				listCalls.Add(new PointF(10f,360));//-
				listCalls.Add(new PointF(10.5f,352));//was 348
				listCalls.Add(new PointF(11.5f,352));
				listCalls.Add(new PointF(12.5f,340));//was 313
				listCalls.Add(new PointF(13.5f,325));//was 363
				listCalls.Add(new PointF(14.5f,277));
				listCalls.Add(new PointF(15.5f,185));
				listCalls.Add(new PointF(16.5f,141));
				listCalls.Add(new PointF(17f,50));
				listCalls.Add(new PointF(17.0f,0));
			}
			else {
				listCalls.Add(new PointF(5f,0));
				listCalls.Add(new PointF(5.5f,284));//5-6am
				listCalls.Add(new PointF(6.5f,767));
				listCalls.Add(new PointF(7.5f,1246));
				listCalls.Add(new PointF(8.5f,1753));
				listCalls.Add(new PointF(9f,1920));//-
				listCalls.Add(new PointF(9.5f,2000));//was 2029
				listCalls.Add(new PointF(10f,1950));//-
				listCalls.Add(new PointF(10.5f,1875));//1846
				listCalls.Add(new PointF(11.5f,1890));//1899
				listCalls.Add(new PointF(12.5f,1820));
				listCalls.Add(new PointF(13.5f,1807));
				listCalls.Add(new PointF(14.5f,1565));
				listCalls.Add(new PointF(15.5f,1178));
				listCalls.Add(new PointF(16.5f,733));
				listCalls.Add(new PointF(17.5f,226));
				listCalls.Add(new PointF(17.5f,0));
			}
			buckets=new float[56];//Number of total bucket. 4 buckets per hour * 14 hours = 56 buckets.
			ListScheds=Schedules.GetDayList(DateShowing);
			//PhoneGraph exceptions will take precedence over employee default
			List<PhoneGraph> listPhoneGraphs=PhoneGraphs.GetAllForDate(DateShowing);			
			TimeSpan time1;
			TimeSpan time2;
			TimeSpan delta;
			for(int i=0;i<ListScheds.Count;i++) {
				if(ListScheds[i].SchedType!=ScheduleType.Employee) {
					continue;
				}
				//get this employee
				Employee employee=Employees.GetEmp(ListScheds[i].EmployeeNum);
				if(employee==null) {//employees will NEVER be deleted. even after they cease to work here. but just in case.
					continue;
				}
				bool hasPhoneGraphEntry=false;
				bool isGraphed=false; 
				//PhoneGraph entries will take priority over the default employee graph state
				for(int iPG=0;iPG<listPhoneGraphs.Count;iPG++) {
					if(listPhoneGraphs[iPG].EmployeeNum==employee.EmployeeNum) {
						isGraphed=listPhoneGraphs[iPG].IsGraphed;
						hasPhoneGraphEntry=true;
						break;
					}
				}
				if(!hasPhoneGraphEntry) {//no phone graph entry found (likely for a future date which does not have entries created yet OR past date where current employee didn't work here yet)
					if(DateShowing<=DateTime.Today) {//no phone graph entry and we are on a past OR current date. if it's not already created then don't graph this employee for this date
						continue;
					}
					//we are on a future date AND we don't have a PhoneGraph entry explicitly set so use the default for this employee
					PhoneEmpDefault ped=PhoneEmpDefaults.GetEmpDefaultFromList(ListScheds[i].EmployeeNum,ListPED);
					if(ped==null) {//we will default to PhoneEmpDefault.IsGraphed so make sure the deafult exists
						continue;
					}
					//no entry in PhoneGraph for the employee on this date so use the default
					isGraphed=ped.IsGraphed;
				}
				if(!isGraphed) {//only care about employees that are being graphed
					continue;
				}				
				for(int b=0;b<buckets.Length;b++) {
					//minutes field multiplier is a function of buckets per hour. answers the question, "how many minutes long is each bucket?"
					time1=new TimeSpan(5,0,0) + new TimeSpan(0,b*15,0); 
					time2=new TimeSpan(5,15,0) + new TimeSpan(0,b*15,0);
					//situation 1: this bucket is completely within the start and stop times.
					if(ListScheds[i].StartTime <= time1 && ListScheds[i].StopTime >= time2) {
						AddEmployeeToBucket(b,employee);
					}
					//situation 2: the start time is after this bucket
					else if(ListScheds[i].StartTime >= time2) {
						continue;
					}
					//situation 3: the stop time is before this bucket
					else if(ListScheds[i].StopTime <= time1) {
						continue;
					}
					//situation 4: start time falls within this bucket
					if(ListScheds[i].StartTime > time1) {
						delta=ListScheds[i].StartTime - time1;
						//7.5 minutes is half of one bucket.
						if(delta.TotalMinutes > 7.5) { //has to work more than 15 minutes to be considered *in* this bucket
							AddEmployeeToBucket(b,employee);												
						}
					}
					//situation 5: stop time falls within this bucket
					if(ListScheds[i].StopTime < time2) {
						delta= time2 - ListScheds[i].StopTime;
						if(delta.TotalMinutes > 7.5) { //has to work more than 15 minutes to be considered *in* this bucket
							AddEmployeeToBucket(b,employee);
						}
					}
				}
				//break;//just show one sched for debugging.
			}
			//missed calls
			//missedCalls=new int[28];
			//List<DateTime> callTimes=PhoneAsterisks.GetMissedCalls(dateShowing);
			//for(int i=0;i<callTimes.Count;i++) {
			//  for(int b=0;b<missedCalls.Length;b++) {
			//    time1=new TimeSpan(5,0,0) + new TimeSpan(0,b*30,0);
			//    time2=new TimeSpan(5,30,0) + new TimeSpan(0,b*30,0);
			//    if(callTimes[i].TimeOfDay >= time1 && callTimes[i].TimeOfDay < time2) {
			//      missedCalls[b]++;
			//    }
			//  }
			//}
			//Minutes Behind
			minutesBehind=PhoneMetrics.AverageMinutesBehind(DateShowing);
			this.Invalidate();
		}

		private void AddEmployeeToBucket(int bucketIndex, Employee employee){
			buckets[bucketIndex]+=1;
			List<Employee> employees = null;
			if(!DictEmployeesPerBucket.TryGetValue(bucketIndex,out employees)) {
				employees=new List<Employee>();
			}
			if(employee!=null) {
				employees.Add(employee);
			}
			DictEmployeesPerBucket[bucketIndex]=employees;
		}

		private void FormGraphEmployeeTime_Paint(object sender,PaintEventArgs e) {
			ListRegions.Clear();
			e.Graphics.SmoothingMode=SmoothingMode.HighQuality;
			RectangleF rec=new RectangleF(panel1.Left,panel1.Top,panel1.Width,panel1.Height);
			e.Graphics.FillRectangle(Brushes.White,rec);
			if(listCalls==null) {
				return;
			}
			float highcall=0;
			for(int i=0;i<listCalls.Count;i++) {
				if(listCalls[i].Y > highcall) {
					highcall=listCalls[i].Y;
				}
			}
			float totalhrs=14f;
			float x1;
			float y1;
			float x2;
			float y2=0;
			//draw grid
			//vertical
			for(int i=1;i<(int)totalhrs;i++) {
				x1=rec.X + ((float)i * rec.Width / totalhrs);
				y1=rec.Y+rec.Height;
				x2=rec.X + ((float)i * rec.Width / totalhrs);
				y2=rec.Y;
				e.Graphics.DrawLine(Pens.Black,x1,y1,x2,y2);
			}
			//draw numbers
			//x-axis
			string str;
			float strW;
			for(int i=0;i<(int)totalhrs+1;i++) {
				if(i<8) {
					str=(i+5).ToString();
				}
				else {
					str=(i-7).ToString();
				}
				strW=e.Graphics.MeasureString(str,Font).Width;
				x1=rec.X + ((float)i * rec.Width / totalhrs) - strW / 2f;
				y1=rec.Y+rec.Height+3;
				e.Graphics.DrawString(str,Font,Brushes.Black,x1,y1);
			}
			//find the biggest bar
			float peak=PIn.Int(PrefC.GetRaw("GraphEmployeeTimesPeak"));//The ideal peak.  Each day should look the same, except Friday.
			if(DateShowing.DayOfWeek==DayOfWeek.Friday) {
				peak=peak*0.8f;//The Friday graph is actually smaller than the other graphs.
			}
			float superPeak=PIn.Int(PrefC.GetRaw("GraphEmployeeTimesSuperPeak"));//the most staff possible to schedule
			/*for(int i=0;i<buckets.Length;i++){
				if(buckets[i]>biggest){
					biggest=buckets[i];
				}
			}*/
			float hOne=rec.Height/superPeak;
			//draw bars
			float x;
			float y;
			float w;
			float h;
			float barspacing=(rec.Width / totalhrs) / 4f; //4f means number of buckets per hours.  EG... 10 minute granularity = 6f;
			float firstbar=barspacing / 2f;
			float barW=barspacing / 1f;//increase denominator in order to increase spacing between bars. 1f = no space... 2f = full bar space. 1.5f = half bar space.
			SolidBrush blueBrush=new SolidBrush(Color.FromArgb(162,193,222));
			for(int i=0;i<buckets.Length;i++) {
				h=(float)buckets[i]*rec.Height/superPeak;
				x=rec.X + firstbar + (float)i*barspacing - barW/2f;
				y=rec.Y+rec.Height-h;
				w=barW;
				RectangleF rc = new RectangleF(x,y,w,h);
				e.Graphics.FillRectangle(Brushes.LightBlue,rc);
				ListRegions.Add(new System.Drawing.Region(rc)); //save this bucket for hover tooltip event
				//draw bar increments						
				for(int o=1;o<buckets[i];o++) {
					x1=x;
					y1=rec.Y+rec.Height-(o*hOne);
					x2=x+barW;
					y2=rec.Y+rec.Height-(o*hOne);
					e.Graphics.DrawLine(Pens.Black,x1,y1,x2,y2);
				}
				//draw the number of employees in this bucket
				SizeF sf=e.Graphics.MeasureString(buckets[i].ToString(),SystemFonts.DefaultFont);
				//removing for now. number of employees now included in hover text.
				//e.Graphics.DrawString(buckets[i].ToString(),SystemFonts.DefaultFont,Brushes.Blue,x+(barW-sf.Width)/2,y-(sf.Height+1));
			}
			//Line graph in red
			float peakH=rec.Height * peak / superPeak;
			Pen redPen=new Pen(Brushes.Red,2f);
			for(int i=0;i<listCalls.Count-1;i++) {
				x1=rec.X + ((listCalls[i].X-5f) * rec.Width / totalhrs);
				y1=rec.Y+rec.Height - (listCalls[i].Y / highcall * peakH);
				x2=rec.X + ((listCalls[i+1].X-5f) * rec.Width / totalhrs);
				y2=rec.Y+rec.Height - (listCalls[i+1].Y / highcall * peakH);
				e.Graphics.DrawLine(redPen,x1,y1,x2,y2);
			}
			//Missed call numbers
			for(int i=0;i<minutesBehind.Length;i++) {
				if(minutesBehind[i]==0) {
					continue;
				}
				str=minutesBehind[i].ToString();
				strW=e.Graphics.MeasureString(str,Font).Width;
				x1=rec.X + barW + ((float)i * rec.Width / totalhrs / 2) - strW / 2f;
				y1=rec.Y+rec.Height-(17*2);
				e.Graphics.DrawString(str,Font,Brushes.Red,x1,y1);
			}
			//Vertical red line for current time
			if(DateTime.Today.Date==DateShowing.Date) {
				TimeSpan now=DateTime.Now.AddHours(-5).TimeOfDay;
				float shift=(float)now.TotalHours * rec.Width / totalhrs;
				x1=rec.X + shift;
				y1=rec.Y+rec.Height;
				x2=rec.X + shift;
				y2=rec.Y;
				e.Graphics.DrawLine(Pens.Red,x1,y1,x2,y2);
			}
			redPen.Dispose();
			blueBrush.Dispose();
		}

		private void panel1_MouseMove(object sender,MouseEventArgs e) {
			//there is 1 region per bucket (synced in paint event), loop through the regions and see if we are hovering over one of them
			for(int i=0;i<ListRegions.Count;i++) {
				if(!ListRegions[i].IsVisible(new Point(e.X,e.Y))) {//we are hovering over this bucket
					continue;
				}
				if(i==CurrentHoverRegionIdx) {//only activate this bucket once (prevents flicker)
					return;
				}
				//build the display string for this hover bucket
				List<Employee> listEmps=null;
				TimeSpan tsStart=new TimeSpan(5,(i*15),0);
				toolTip.ToolTipTitle=tsStart.ToShortTimeString()+" - "+tsStart.Add(TimeSpan.FromMinutes(15)).ToShortTimeString();
				string employees="";
				if(DictEmployeesPerBucket.TryGetValue(i,out listEmps)) {
					toolTip.ToolTipTitle=toolTip.ToolTipTitle+" ("+listEmps.Count.ToString()+" Techs)";
					listEmps.Sort(new Employees.EmployeeComparer(Employees.EmployeeComparer.SortBy.firstName));
					for(int p=0;p<listEmps.Count;p++) {
						List<Schedule> sch=Schedules.GetForEmployee(ListScheds,listEmps[p].EmployeeNum);
						employees+=listEmps[p].FName;
						employees+=Schedules.GetCommaDelimStringForScheds(sch);
						employees+="\r\n";
					}
				}
				//activate and show this bucket's tooltip
				toolTip.Active=true;
				toolTip.SetToolTip(this,employees);
				//save this region as current so we only activate it once
				CurrentHoverRegionIdx=i;
				return;
			}
			//not hovering over a bucket so kill the tooltip
			toolTip.Active=false;
			CurrentHoverRegionIdx=-1;
		}

		private void buttonLeft_Click(object sender,EventArgs e) {
			if(DateShowing.DayOfWeek==DayOfWeek.Monday) {
				DateShowing=DateShowing.AddDays(-3);
			}
			else {
				DateShowing=DateShowing.AddDays(-1);
			}
			FillData();
		}

		private void buttonRight_Click(object sender,EventArgs e) {
			if(DateShowing.DayOfWeek==DayOfWeek.Friday) {
				DateShowing=DateShowing.AddDays(+3);
			}
			else {
				DateShowing=DateShowing.AddDays(+1);
			}
			FillData();
		}

		private void butEdit_Click(object sender,EventArgs e) {
			FormPhoneGraphDateEdit FormPGDE=new FormPhoneGraphDateEdit(DateShowing);
			FormPGDE.ShowDialog();
			FillData(); //always refill, we may have new entries regardless of form dialog result
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			if(PrinterL.SetPrinter(pd,PrintSituation.Default,0,"Employee time graph printed")) {
				pd.Print();
			}
		}

		private void pd2_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			e.Graphics.DrawString(labelDate.Text,labelDate.Font,Brushes.Black,350,120);
			Bitmap bitmap=new Bitmap(this.ClientSize.Width,this.ClientSize.Height);
			this.DrawToBitmap(bitmap,new Rectangle(0,0,bitmap.Width,bitmap.Height));
			e.Graphics.DrawImage(bitmap,50,200);
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}





	}
}