using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormPerioGraphical:Form {
		public PerioExam PerioExamCur;
		//public List<PerioMeasure> ListPerioMeasures; 

		public FormPerioGraphical() {
			InitializeComponent();
			toothChart.DrawMode=DrawingMode.DirectX;
		}

		private void FormPerioGraphic_Load(object sender,EventArgs e) {
			toothChart.ColorBackground=Color.White;
			toothChart.ColorText=Color.Black;
			toothChart.PerioMode=true;
			toothChart.ColorBleeding=DefC.Short[(int)DefCat.MiscColors][1].ItemColor;
			butColorBleed.BackColor=DefC.Short[(int)DefCat.MiscColors][1].ItemColor;
			toothChart.ColorSuppuration=DefC.Short[(int)DefCat.MiscColors][2].ItemColor;
			butColorPus.BackColor=DefC.Short[(int)DefCat.MiscColors][2].ItemColor;
			toothChart.ColorProbing=PrefC.GetColor(PrefName.PerioColorProbing);
			butColorProbing.BackColor=PrefC.GetColor(PrefName.PerioColorProbing);
			toothChart.ColorProbingRed=PrefC.GetColor(PrefName.PerioColorProbingRed);
			butColorProbingRed.BackColor=PrefC.GetColor(PrefName.PerioColorProbingRed);
			toothChart.ColorGingivalMargin=PrefC.GetColor(PrefName.PerioColorGM);
			panelColorGM.BackColor=PrefC.GetColor(PrefName.PerioColorGM);
			toothChart.ColorCAL=PrefC.GetColor(PrefName.PerioColorCAL);
			panelColorCAL.BackColor=PrefC.GetColor(PrefName.PerioColorCAL);
			toothChart.ColorMGJ=PrefC.GetColor(PrefName.PerioColorMGJ);
			panelColorMGJ.BackColor=PrefC.GetColor(PrefName.PerioColorMGJ);
			toothChart.ColorFurcations=PrefC.GetColor(PrefName.PerioColorFurcations);
			toothChart.ColorFurcationsRed=PrefC.GetColor(PrefName.PerioColorFurcationsRed);
			toothChart.RedLimitProbing=PrefC.GetInt(PrefName.PerioRedProb);
			toothChart.RedLimitFurcations=PrefC.GetInt(PrefName.PerioRedFurc);
			List<PerioMeasure> listMeas=PerioMeasures.GetAllForExam(PerioExamCur.PerioExamNum);
			//compute CAL's for each site.  If a CAL is valid, pass it in.
			PerioMeasure measureProbe;
			PerioMeasure measureGM;
			int gm;
			int pd;
			int calMB;
			int calB;
			int calDB;
			int calML;
			int calL;
			int calDL;
			for(int t=1;t<=32;t++) {
				measureProbe=null;
				measureGM=null;
				for(int i=0;i<listMeas.Count;i++) {
					if(listMeas[i].IntTooth!=t){
						continue;
					}
					if(listMeas[i].SequenceType==PerioSequenceType.Probing) {
						measureProbe=listMeas[i];
					}
					if(listMeas[i].SequenceType==PerioSequenceType.GingMargin) {
						measureGM=listMeas[i];
					}
				}
				if(measureProbe==null || measureGM==null) {
					continue;//to the next tooth
				}
				//mb
				calMB=-1;
				gm=measureGM.MBvalue;
				pd=measureProbe.MBvalue;
				if(gm!=-1 && pd!=-1) {
					calMB=gm+pd;
				}
				//B
				calB=-1;
				gm=measureGM.Bvalue;
				pd=measureProbe.Bvalue;
				if(gm!=-1 && pd!=-1) {
					calB=gm+pd;
				}
				//DB
				calDB=-1;
				gm=measureGM.DBvalue;
				pd=measureProbe.DBvalue;
				if(gm!=-1 && pd!=-1) {
					calDB=gm+pd;
				}
				//ML
				calML=-1;
				gm=measureGM.MLvalue;
				pd=measureProbe.MLvalue;
				if(gm!=-1 && pd!=-1) {
					calML=gm+pd;
				}
				//L
				calL=-1;
				gm=measureGM.Lvalue;
				pd=measureProbe.Lvalue;
				if(gm!=-1 && pd!=-1) {
					calL=gm+pd;
				}
				//DL
				calDL=-1;
				gm=measureGM.DLvalue;
				pd=measureProbe.DLvalue;
				if(gm!=-1 && pd!=-1) {
					calDL=gm+pd;
				}
				if(calMB!=-1 || calB!=-1 || calDB!=-1 || calML!=-1 || calL!=-1 || calDL!=-1) {
					toothChart.AddPerioMeasure(t,PerioSequenceType.CAL,calMB,calB,calDB,calML,calL,calDL);
				}
			}
			for(int i=0;i<listMeas.Count;i++) {
				if(listMeas[i].SequenceType==PerioSequenceType.SkipTooth) {
					toothChart.SetMissing(listMeas[i].IntTooth.ToString());
				}
				else if(listMeas[i].SequenceType==PerioSequenceType.Mobility) {
					int mob=listMeas[i].ToothValue;
					Color color=Color.Black;
					if(mob >= PrefC.GetInt(PrefName.PerioRedMob)){
						color=Color.Red;	
					}
					toothChart.SetMobility(listMeas[i].IntTooth.ToString(),mob.ToString(),color);
				}
				else {
					toothChart.AddPerioMeasure(listMeas[i]);
				}
			}

	
			/*
			toothChart.SetMissing("13");
			toothChart.SetMissing("14");
			toothChart.SetMissing("18");
			toothChart.SetMissing("25");
			toothChart.SetMissing("26");
			toothChart.SetImplant("14",Color.Gray);
			//Movements are too low of a priority to test right now.  We might not even want to implement them.
			//toothChart.MoveTooth("4",0,0,0,0,-5,0);
			//toothChart.MoveTooth("16",0,20,0,-3,0,0);
			//toothChart.MoveTooth("24",15,2,0,0,0,0);
			toothChart.SetMobility("2","3",Color.Red);
			toothChart.SetMobility("7","2",Color.Red);
			toothChart.SetMobility("8","2",Color.Red);
			toothChart.SetMobility("9","2",Color.Red);
			toothChart.SetMobility("10","2",Color.Red);
			toothChart.SetMobility("16","3",Color.Red);
			toothChart.SetMobility("24","2",Color.Red);
			toothChart.SetMobility("31","3",Color.Red);
			toothChart.AddPerioMeasure(1,PerioSequenceType.Furcation,-1,2,-1,1,-1,-1);
			toothChart.AddPerioMeasure(2,PerioSequenceType.Furcation,-1,2,-1,1,-1,-1);
			toothChart.AddPerioMeasure(3,PerioSequenceType.Furcation,-1,2,-1,1,-1,-1);
			toothChart.AddPerioMeasure(5,PerioSequenceType.Furcation,1,-1,-1,-1,-1,-1);
			toothChart.AddPerioMeasure(30,PerioSequenceType.Furcation,-1,-1,-1,-1,3,-1);
			for(int i=1;i<=32;i++) {
				//string tooth_id=i.ToString();
				//bleeding and suppuration on all MB sites
				//bleeding only all DL sites
				//suppuration only all B sites
				//blood=1, suppuration=2, both=3
				toothChart.AddPerioMeasure(i,PerioSequenceType.Bleeding,  3,2,-1,-1,-1,1);
				toothChart.AddPerioMeasure(i,PerioSequenceType.GingMargin,0,1,1,1,0,0);
				toothChart.AddPerioMeasure(i,PerioSequenceType.Probing,   3,2,3,4,2,3);
				toothChart.AddPerioMeasure(i,PerioSequenceType.CAL,       3,3,4,5,2,3);//basically GingMargin+Probing, unless one of them is -1
				toothChart.AddPerioMeasure(i,PerioSequenceType.MGJ,       5,5,5,6,6,6);
			}*/
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintDocument pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.Print();
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			Graphics g=ev.Graphics;
			Bitmap bitmap=toothChart.GetBitmap();
			g.DrawImage(bitmap,75,75,bitmap.Width,bitmap.Height);
		}

		private void butSetup_Click(object sender,EventArgs e) {

		}

		private void butSave_Click(object sender,EventArgs e) {

		}
	}
}
