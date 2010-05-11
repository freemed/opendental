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
		private bool[] usedLunch;
		private DateTime dateShowing;

		public FormGraphEmployeeTime() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormGraphEmployeeTime_Load(object sender,EventArgs e) {
			dateShowing=AppointmentL.DateSelected;
			FillData();
		}

		private void FillData(){
			labelDate.Text=dateShowing.ToString("dddd, MMMM d");
			listCalls=new List<PointF>();
			if(dateShowing.DayOfWeek==DayOfWeek.Friday) {
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
				listCalls.Add(new PointF(16.5f,0));
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
			buckets=new float[28];//every 30 minutes, starting at 5:15
			usedLunch=new bool[28];
			List<Schedule> scheds=Schedules.GetDayList(dateShowing);
			for(int i=0;i<scheds.Count;i++){
				if(scheds[i].SchedType!=ScheduleType.Employee){
					continue;
				}
				if(scheds[i].EmployeeNum==15//Derek
					|| scheds[i].EmployeeNum==17//Nathan
					|| scheds[i].EmployeeNum==22//Jordan
					|| scheds[i].EmployeeNum==18)//Spike
				{
					continue;
				}
				TimeSpan time;
				TimeSpan lunch=(scheds[i].StartTime + new TimeSpan((scheds[i].StopTime-scheds[i].StartTime).Ticks/2) - new TimeSpan(0,37,0)).TimeOfDay;//subtract 37 minutes to make it fall within a bucket, and because people seem to like to take lunch early, and because the logic will bump it forward if lunch already used.
				for(int b=0;b<buckets.Length;b++){
					time=new TimeSpan(5,15,0) + new TimeSpan(0,b*30,0);
					if(time<scheds[i].StartTime.TimeOfDay || time>scheds[i].StopTime.TimeOfDay){
						continue;
					}
					//if the lunch time is within 15 minutes of this, then don't add to the bucket
					if((lunch-time).Duration() < new TimeSpan(0,15,0)){
						if(usedLunch[b]){//can't use this bucket for lunch because someone else already did.
							lunch+=new TimeSpan(0,30,0);//use the next one
							buckets[b]+=1;
						}
						usedLunch[b]=true;
						continue;//use this bucket for lunch (don't add a drop to the bucket)
					}
					buckets[b]+=1;
				}
				//break;//just show one sched for debugging.
			}
			this.Invalidate();
		}

		private void FormGraphEmployeeTime_Paint(object sender,PaintEventArgs e) {
			e.Graphics.SmoothingMode=SmoothingMode.HighQuality;
			RectangleF rec=new RectangleF(panel1.Left,panel1.Top,panel1.Width,panel1.Height);
			e.Graphics.FillRectangle(Brushes.White,rec);
			if(listCalls==null){
				return;
			}
			float highcall=0;
			for(int i=0;i<listCalls.Count;i++){
				if(listCalls[i].Y > highcall){
					highcall=listCalls[i].Y;
				}
			}
			float totalhrs=14f;
			float x1;
			float y1;
			float x2;
			float y2;
			//draw grid
			//vertical
			for(int i=1;i<(int)totalhrs;i++){
				x1=rec.X + ( (float)i * rec.Width / totalhrs );
				y1=rec.Y+rec.Height;
				x2=rec.X + ( (float)i * rec.Width / totalhrs );
				y2=rec.Y;
				e.Graphics.DrawLine(Pens.Black,x1,y1,x2,y2);
			}
			//draw numbers
			//x-axis
			string str;
			float strW;
			for(int i=0;i<(int)totalhrs+1;i++){
				if(i<8){
					str=(i+5).ToString();
				}
				else{
					str=(i-7).ToString();
				}
				strW=e.Graphics.MeasureString(str,Font).Width;
				x1=rec.X + ( (float)i * rec.Width / totalhrs ) - strW / 2f;
				y1=y1=rec.Y+rec.Height+3;
				e.Graphics.DrawString(str,Font,Brushes.Black,x1,y1);
			}
			//find the biggest bar
			//hard code to 9 staff being max so that each day looks the same.
			float peak=9;//The ideal peak
			if(dateShowing.DayOfWeek==DayOfWeek.Friday){
				peak=7.5f;//The Friday graph is actually smaller than the other graphs.
			}
			float superPeak=11;//the most staff possible to schedule
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
			float barspacing=rec.Width / totalhrs / 2f;
			float firstbar=barspacing / 2f;
			float barW=barspacing / 2f;
			SolidBrush blueBrush=new SolidBrush(Color.FromArgb(162,193,222));
			for(int i=0;i<buckets.Length;i++){
				h=(float)buckets[i]*rec.Height/superPeak;
				x=rec.X + firstbar + (float)i*barspacing - barW/2f;
				y=rec.Y+rec.Height-h;
				w=barW;
				e.Graphics.FillRectangle(Brushes.LightBlue,x,y,w,h);
				//draw bar increments
				for(int o=1;o<(int)buckets[i];o++){
					x1=x;
					y1=rec.Y+rec.Height-(o*hOne);
					x2=x+barW;
					y2=rec.Y+rec.Height-(o*hOne);
					e.Graphics.DrawLine(Pens.Black,x1,y1,x2,y2);
				}
			}
			//Line graph in red
			float peakH=rec.Height * peak / superPeak;
			Pen redPen=new Pen(Brushes.Red,2f);
			for(int i=0;i<listCalls.Count-1;i++){
				x1=rec.X + ( (listCalls[i].X-5f) * rec.Width / totalhrs );
				y1=rec.Y+rec.Height - (listCalls[i].Y / highcall * peakH);
				x2=rec.X + ( (listCalls[i+1].X-5f) * rec.Width / totalhrs );
				y2=rec.Y+rec.Height - (listCalls[i+1].Y / highcall * peakH);
				e.Graphics.DrawLine(redPen,x1,y1,x2,y2);
			}
			redPen.Dispose();
			blueBrush.Dispose();
		}

		private void buttonLeft_Click(object sender,EventArgs e) {
			if(dateShowing.DayOfWeek==DayOfWeek.Monday) {
				dateShowing=dateShowing.AddDays(-3);
			} 
			else{
				dateShowing=dateShowing.AddDays(-1);
			}
			FillData();
		}

		private void buttonRight_Click(object sender,EventArgs e) {
			if(dateShowing.DayOfWeek==DayOfWeek.Friday) {
				dateShowing=dateShowing.AddDays(+3);
			}
			else {
				dateShowing=dateShowing.AddDays(+1);
			}
			FillData();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
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