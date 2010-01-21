using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using SparksToothChart;

namespace OpenDental {
	public partial class FormPerioGraphical:Form {
		public PerioExam PerioExamCur;
		public Patient PatCur;
		//public List<PerioMeasure> ListPerioMeasures; 

		public FormPerioGraphical(PerioExam perioExam,Patient patient) {
			PerioExamCur=perioExam;
			PatCur=patient;
			InitializeComponent();
			ComputerPref localComputerPrefs=ComputerPrefs.GetForLocalComputer();
			toothChart.DeviceFormat=new ToothChartDirectX.DirectXDeviceFormat(localComputerPrefs.DirectXFormat);//Must be set before draw mode
			toothChart.DrawMode=DrawingMode.DirectX;
			toothChart.ColorBackground=Color.White;
			toothChart.ColorText=Color.Black;
			toothChart.PerioMode=true;
			toothChart.ColorBleeding=DefC.Short[(int)DefCat.MiscColors][1].ItemColor;
			toothChart.ColorSuppuration=DefC.Short[(int)DefCat.MiscColors][2].ItemColor;
			toothChart.ColorProbing=PrefC.GetColor(PrefName.PerioColorProbing);
			toothChart.ColorProbingRed=PrefC.GetColor(PrefName.PerioColorProbingRed);
			toothChart.ColorGingivalMargin=PrefC.GetColor(PrefName.PerioColorGM);
			toothChart.ColorCAL=PrefC.GetColor(PrefName.PerioColorCAL);
			toothChart.ColorMGJ=PrefC.GetColor(PrefName.PerioColorMGJ);
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
					if(listMeas[i].IntTooth!=t) {
						continue;
					}
					if(listMeas[i].SequenceType==PerioSequenceType.Probing) {
						measureProbe=listMeas[i];
					}
					if(listMeas[i].SequenceType==PerioSequenceType.GingMargin) {
						measureGM=listMeas[i];
					}
				}
				if(measureProbe==null||measureGM==null) {
					continue;//to the next tooth
				}
				//mb
				calMB=-1;
				gm=measureGM.MBvalue;
				pd=measureProbe.MBvalue;
				if(gm!=-1&&pd!=-1) {
					calMB=gm+pd;
				}
				//B
				calB=-1;
				gm=measureGM.Bvalue;
				pd=measureProbe.Bvalue;
				if(gm!=-1&&pd!=-1) {
					calB=gm+pd;
				}
				//DB
				calDB=-1;
				gm=measureGM.DBvalue;
				pd=measureProbe.DBvalue;
				if(gm!=-1&&pd!=-1) {
					calDB=gm+pd;
				}
				//ML
				calML=-1;
				gm=measureGM.MLvalue;
				pd=measureProbe.MLvalue;
				if(gm!=-1&&pd!=-1) {
					calML=gm+pd;
				}
				//L
				calL=-1;
				gm=measureGM.Lvalue;
				pd=measureProbe.Lvalue;
				if(gm!=-1&&pd!=-1) {
					calL=gm+pd;
				}
				//DL
				calDL=-1;
				gm=measureGM.DLvalue;
				pd=measureProbe.DLvalue;
				if(gm!=-1&&pd!=-1) {
					calDL=gm+pd;
				}
				if(calMB!=-1||calB!=-1||calDB!=-1||calML!=-1||calL!=-1||calDL!=-1) {
					toothChart.AddPerioMeasure(t,PerioSequenceType.CAL,calMB,calB,calDB,calML,calL,calDL);
				}
			}
			for(int i=0;i<listMeas.Count;i++) {
				if(listMeas[i].SequenceType==PerioSequenceType.SkipTooth) {
					toothChart.SetMissing(listMeas[i].IntTooth.ToString());
				} else if(listMeas[i].SequenceType==PerioSequenceType.Mobility) {
					int mob=listMeas[i].ToothValue;
					Color color=Color.Black;
					if(mob>=PrefC.GetInt(PrefName.PerioRedMob)) {
						color=Color.Red;
					}
					toothChart.SetMobility(listMeas[i].IntTooth.ToString(),mob.ToString(),color);
				} else {
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

		private void FormPerioGraphic_Load(object sender,EventArgs e) {
			
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintDocument pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			//pd2.DefaultPageSettings.PrinterResolution.X=300;
			//pd2.DefaultPageSettings.PrinterResolution.Y=300;
			//This prevents a bug caused by some printer drivers not reporting their papersize.
			//But remember that other countries use A4 paper instead of 8 1/2 x 11.
			if(pd2.DefaultPageSettings.PaperSize.Height==0) {
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			pd2.Print();
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			Graphics g=ev.Graphics;
			RenderPerioPrintout(g,PatCur,new Rectangle(0,50,ev.MarginBounds.Width,ev.MarginBounds.Height));
		}

		public void RenderPerioPrintout(Graphics g,Patient pat,Rectangle marginBounds) {
			string clinicName="";
			//This clinic name could be more accurate here in the future if we make perio exams clinic specific.
			//Perhaps if there were a perioexam.ClinicNum column.
			if(pat.ClinicNum!=0) {
				Clinic clinic=Clinics.GetClinic(pat.ClinicNum);
				clinicName=clinic.Description;
			} else {
				clinicName=PrefC.GetString(PrefName.PracticeTitle);
			}
			float y=marginBounds.Y;
			SizeF m;
			Font font=new Font("Arial",15);
			string titleStr="PERIODONTAL EXAMINATION";
			m=g.MeasureString(titleStr,font);
			g.DrawString(titleStr,font,Brushes.Black,new PointF(marginBounds.Width/2f-m.Width/2f,y));
			y+=m.Height;
			font=new Font("Arial",11);
			m=g.MeasureString(clinicName,font);
			g.DrawString(clinicName,font,Brushes.Black,
				new PointF(marginBounds.Width/2f-m.Width/2f,y));
			y+=m.Height;
			string patNameStr=PatCur.GetNameFLFormal();
			m=g.MeasureString(patNameStr,font);
			g.DrawString(patNameStr,font,Brushes.Black,new PointF(marginBounds.Width/2f-m.Width/2f,y));
			y+=m.Height;
			DateTime serverTimeNow=MiscData.GetNowDateTime();
			string timeNowStr=serverTimeNow.ToShortDateString();//Locale specific date.
			m=g.MeasureString(timeNowStr,font);
			g.DrawString(timeNowStr,font,Brushes.Black,new PointF(marginBounds.Width/2f-m.Width/2f,y));
			y+=m.Height;
			Bitmap bitmap=toothChart.GetBitmap();
			g.DrawImage(bitmap,marginBounds.Width/2f-bitmap.Width/2f,y,bitmap.Width,bitmap.Height);
		}

		private void butSetup_Click(object sender,EventArgs e) {
			FormPerioGraphicalSetup fpgs=new FormPerioGraphicalSetup();
			if(fpgs.ShowDialog()==DialogResult.OK){
				toothChart.ColorCAL=PrefC.GetColor(PrefName.PerioColorCAL);
				toothChart.ColorFurcations=PrefC.GetColor(PrefName.PerioColorFurcations);
				toothChart.ColorFurcationsRed=PrefC.GetColor(PrefName.PerioColorFurcationsRed);
				toothChart.ColorGingivalMargin=PrefC.GetColor(PrefName.PerioColorGM);
				toothChart.ColorMGJ=PrefC.GetColor(PrefName.PerioColorMGJ);	
				toothChart.ColorProbing=PrefC.GetColor(PrefName.PerioColorProbing);
				toothChart.ColorProbingRed=PrefC.GetColor(PrefName.PerioColorProbingRed);
				this.toothChart.Invalidate();
			}
		}

		private void butSave_Click(object sender,EventArgs e) {
			Bitmap perioPrintImage=null;
			Graphics g=null;
			Document doc=new Document();
			bool docCreated=false;
			try {
				const int scale=6;
				perioPrintImage=new Bitmap(750*scale,1000*scale);
				g=Graphics.FromImage(perioPrintImage);
				perioPrintImage.SetResolution(100,100);
				g.Clear(Color.White);
				g.CompositingQuality=System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.InterpolationMode=System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				g.TextRenderingHint=System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				RenderPerioPrintout(g,PatCur,new Rectangle(0,0,perioPrintImage.Width,perioPrintImage.Height));
				string patImagePath=ImageStore.GetPatientFolder(PatCur);
				string filePath="";
				do {
					doc.DateCreated=MiscData.GetNowDateTime();
					doc.FileName="perioexam_"+doc.DateCreated.ToString("yyyy_MM_dd_hh_mm_ss")+".png";
					filePath=ODFileUtils.CombinePaths(patImagePath,doc.FileName);
				} while(File.Exists(filePath));
				doc.PatNum=PatCur.PatNum;
				doc.ImgType=ImageType.Photo;
				doc.DocCategory=DefC.GetByExactName(DefCat.ImageCats,"Tooth Charts");
				doc.Description="Perio Exam";
				Documents.Insert(doc,PatCur);
				docCreated=true;
				perioPrintImage.Save(filePath,System.Drawing.Imaging.ImageFormat.Png);
				MessageBox.Show(Lan.g(this,"Image saved."));
			} catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Image failed to save: "+Environment.NewLine+ex.ToString()));
				if(docCreated) {
					Documents.Delete(doc);
				}
			} finally {
				if(g!=null) {
					g.Dispose();
					g=null;
				}
				if(perioPrintImage!=null) {
					perioPrintImage.Dispose();
					perioPrintImage=null;
				}
			}
		}

	}
}
