using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class ContrSchedGrid : System.Windows.Forms.UserControl{
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public int RowH;
		///<summary></summary>
		public int ColW;
		///<summary>The width of the time columns</summary>
		public int NumW;
		///<summary>The width of one operatory.</summary>
		public float opW;
		///<summary>The type of sched we are interested in seeing.</summary>
		public ScheduleType SchedType; 
		///<summary>For provider displays, this is the provider.</summary>
		public int ProvNum;
		private Font timeFont=new Font("Small Font",7);
		private Font blockFont=new Font("Arial",8);

		///<summary></summary>
		public ContrSchedGrid(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			RowH=4;
			ColW=90;
			NumW=36;
			if(Operatories.ListShort==null){
				opW=0;
			}
			else{
				opW=ColW/Operatories.ListShort.Length;
			}
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			// 
			// ContrSchedGrid
			// 
			this.BackColor = System.Drawing.Color.Silver;
			this.Name = "ContrSchedGrid";
			this.Size = new System.Drawing.Size(472, 342);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ContrSchedGrid_Paint);

		}
		#endregion

		private void ContrSchedGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e){
			SolidBrush blockBrush=new SolidBrush(Color.White);
			float blockW=ColW;//set in each loop
			float opOffset=0;
			if(SchedDefaults.List!=null){	
				for(int i=0;i<SchedDefaults.List.Length;i++){
					if(SchedType==ScheduleType.Practice){//for practice
						if(SchedDefaults.List[i].SchedType!=ScheduleType.Practice){
							continue;//only show practice blocks
						}
					}
					if(SchedType==ScheduleType.Provider){//for providers
						if(SchedDefaults.List[i].SchedType!=ScheduleType.Provider){
							continue;//only show prov blocks
						}
						if(SchedDefaults.List[i].ProvNum!=ProvNum){
							continue;//only show blocks for this prov
						}
					}
					if(SchedType==ScheduleType.Blockout){//for blockouts
						//only show practice blocks and blockout blocks
						if(SchedDefaults.List[i].SchedType==ScheduleType.Provider){
							continue;
						}
					}
					if(SchedDefaults.List[i].SchedType==ScheduleType.Practice){//open block color
						blockBrush=new SolidBrush(DefB.Long[(int)DefCat.AppointmentColors][0].ItemColor);
						blockW=ColW;
						opOffset=0;
					}
					if(SchedDefaults.List[i].SchedType==ScheduleType.Provider){//open block color
						blockBrush=new SolidBrush(DefB.Long[(int)DefCat.AppointmentColors][0].ItemColor);
						blockW=ColW;
						opOffset=0;
					}
					if(SchedDefaults.List[i].SchedType==ScheduleType.Blockout){
						blockBrush=new SolidBrush(DefB.GetColor(DefCat.BlockoutTypes
							,SchedDefaults.List[i].BlockoutType));
						if(SchedDefaults.List[i].Op==0){
							blockW=ColW;
						}
						else{
							blockW=opW;
						}
						if(SchedDefaults.List[i].Op==0){
							opOffset=0;
						}
						else{
							opOffset=Operatories.GetOrder(SchedDefaults.List[i].Op);
							if(opOffset==-1){//op not visible
								continue;
							}
							opOffset=opOffset*opW;
						}
					}
					e.Graphics.FillRectangle(blockBrush
						,NumW+SchedDefaults.List[i].DayOfWeek*ColW
						+opOffset//usually 0
						,SchedDefaults.List[i].StartTime.Hour*6*RowH
						+(int)SchedDefaults.List[i].StartTime.Minute/10*RowH
						,blockW
						,((SchedDefaults.List[i].StopTime
						-SchedDefaults.List[i].StartTime).Hours*6
						+(SchedDefaults.List[i].StopTime-SchedDefaults.List[i].StartTime).Minutes/10)*RowH);
					
				}
			}
			Pen bPen=new Pen(Color.Black);
			Pen gPen=new Pen(Color.LightGray);
			for(int y=0;y<24*6;y++){
				e.Graphics.DrawLine(gPen,NumW,y*RowH,NumW+ColW*7,y*RowH);
			}
			for(int y=0;y<25;y++){
				e.Graphics.DrawLine(bPen,NumW,y*RowH*6,NumW+ColW*7,y*RowH*6);
			}
			for(int x=0;x<8;x++){
				e.Graphics.DrawLine(bPen,NumW+x*ColW,0,NumW+x*ColW,RowH*6*24);
			}
			if(SchedDefaults.List!=null
				&& SchedType==ScheduleType.Blockout)
			{
				for(int i=0;i<SchedDefaults.List.Length;i++){
					if(SchedDefaults.List[i].SchedType==ScheduleType.Blockout){
						if(SchedDefaults.List[i].Op==0){
							blockW=ColW;
						}
						else{
							blockW=opW;
						}
						if(SchedDefaults.List[i].Op==0){
							opOffset=0;
						}
						else{
							opOffset=Operatories.GetOrder(SchedDefaults.List[i].Op);
							if(opOffset==-1){//op not visible
								continue;
							}
							opOffset=opOffset*opW;
						}
						e.Graphics.DrawString(
							DefB.GetName(DefCat.BlockoutTypes,SchedDefaults.List[i].BlockoutType)
							,blockFont,Brushes.Black
							,new RectangleF(
							NumW+SchedDefaults.List[i].DayOfWeek*ColW
							+opOffset//usually 0
							,SchedDefaults.List[i].StartTime.Hour*6*RowH
							+(int)SchedDefaults.List[i].StartTime.Minute/10*RowH
							,blockW
							,15));
					}
				}
			}
			CultureInfo ci=(CultureInfo)CultureInfo.CurrentCulture.Clone();
			string hFormat=Lan.GetShortTimeFormat(ci);
			for(int y=0;y<24;y++){
				e.Graphics.DrawString((new DateTime(2000,1,1,y,0,0)).ToString(hFormat,ci)
					,timeFont,new SolidBrush(Color.Black),0,y*RowH*6-3);
				e.Graphics.DrawString((new DateTime(2000,1,1,y,0,0)).ToString(hFormat,ci)
					,timeFont,new SolidBrush(Color.Black),NumW+ColW*7,y*RowH*6-3);
			}
			Width=NumW*2+ColW*7;
			Height=RowH*24*6+1;
		}
	}

	///<summary></summary>
	public struct TimeBlock{
		///<summary></summary>
		public int Col;
		///<summary></summary>
		public DateTime TStart;
		///<summary></summary>
		public DateTime TStop;
	}
}
