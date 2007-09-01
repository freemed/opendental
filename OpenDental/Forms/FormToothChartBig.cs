using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tao.Platform.Windows;
using SparksToothChart;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormToothChartingBig:System.Windows.Forms.Form {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool ShowBySelectedTeeth;
		private ToothInitial[] ToothInitialList;
		private GraphicalToothChart toothChart;
		private List<DataRow> ProcList;

		///<summary></summary>
		public FormToothChartingBig(bool showBySelectedTeeth,ToothInitial[] toothInitialList,List<DataRow> procList)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			ShowBySelectedTeeth=showBySelectedTeeth;
			ToothInitialList=toothInitialList;
			ProcList=procList;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.toothChart = new SparksToothChart.GraphicalToothChart();
			this.SuspendLayout();
			// 
			// toothChart
			// 
			this.toothChart.ColorBackground = System.Drawing.Color.Empty;
			this.toothChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toothChart.Location = new System.Drawing.Point(0,0);
			this.toothChart.Name = "toothChart";
			this.toothChart.Size = new System.Drawing.Size(926,858);
			this.toothChart.TabIndex = 0;
			this.toothChart.UseHardware = false;
			this.toothChart.UseInternational = false;
			// 
			// FormToothChartingBig
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(926,858);
			this.Controls.Add(this.toothChart);
			this.Name = "FormToothChartingBig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResizeEnd += new System.EventHandler(this.FormToothChartingBig_ResizeEnd);
			this.Load += new System.EventHandler(this.FormToothChartingBig_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormToothChartingBig_Load(object sender,EventArgs e) {
			ComputerPref computerPref=ComputerPrefs.GetForLocalComputer();
			toothChart.UseHardware=computerPref.GraphicsUseHardware;
			toothChart.PreferredPixelFormatNumber=computerPref.PreferredPixelFormatNum;
			toothChart.SimpleMode=computerPref.GraphicsSimple;//Must be last preference set, last so that all
																												//settings are caried through in the reinitialization
																												//this line triggers.
			//The preferred pixel format number changes to the selected pixel format number after a context is chosen.
			computerPref.PreferredPixelFormatNum=toothChart.PreferredPixelFormatNumber;
			ComputerPrefs.Update(computerPref);
			FillToothChart();
			//toothChart.Refresh();
		}

		private void FormToothChartingBig_ResizeEnd(object sender,EventArgs e) {
			FillToothChart();
			//toothChart.Refresh();
		}

		///<summary>This is, of course, called when module refreshed.  But it's also called when user sets missing teeth or tooth movements.  In that case, the Progress notes are not refreshed, so it's a little faster.  This also fills in the movement amounts.</summary>
		private void FillToothChart(){
			Cursor=Cursors.WaitCursor;
			toothChart.SuspendLayout();
			toothChart.UseInternational=PrefB.GetBool("UseInternationalToothNumbers");
			toothChart.ColorBackground=DefB.Long[(int)DefCat.ChartGraphicColors][10].ItemColor;
			toothChart.ColorText=DefB.Long[(int)DefCat.ChartGraphicColors][11].ItemColor;
			toothChart.ColorTextHighlight=DefB.Long[(int)DefCat.ChartGraphicColors][12].ItemColor;
			toothChart.ColorBackHighlight=DefB.Long[(int)DefCat.ChartGraphicColors][13].ItemColor;
			//remember which teeth were selected
			ArrayList selectedTeeth=new ArrayList();//integers 1-32
			for(int i=0;i<toothChart.SelectedTeeth.Length;i++) {
				selectedTeeth.Add(Tooth.ToInt(toothChart.SelectedTeeth[i]));
			}
			toothChart.ResetTeeth();
			/*if(PatCur==null) {
				toothChart.ResumeLayout();
				FillMovementsAndHidden();
				Cursor=Cursors.Default;
				return;
			}*/
			if(ShowBySelectedTeeth) {
				for(int i=0;i<selectedTeeth.Count;i++) {
					toothChart.SetSelected((int)selectedTeeth[i],true);
				}
			}
			//first, primary.  That way, you can still set a primary tooth missing afterwards.
			for(int i=0;i<ToothInitialList.Length;i++) {
				if(ToothInitialList[i].InitialType==ToothInitialType.Primary) {
					toothChart.SetToPrimary(ToothInitialList[i].ToothNum);
				}
			}
			for(int i=0;i<ToothInitialList.Length;i++) {
				switch(ToothInitialList[i].InitialType) {
					case ToothInitialType.Missing:
						toothChart.SetInvisible(ToothInitialList[i].ToothNum);
						break;
					case ToothInitialType.Hidden:
						toothChart.HideTooth(ToothInitialList[i].ToothNum);
						break;
					//case ToothInitialType.Primary:
					//	break;
					case ToothInitialType.Rotate:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,ToothInitialList[i].Movement,0,0,0,0,0);
						break;
					case ToothInitialType.TipM:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,ToothInitialList[i].Movement,0,0,0,0);
						break;
					case ToothInitialType.TipB:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,ToothInitialList[i].Movement,0,0,0);
						break;
					case ToothInitialType.ShiftM:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,ToothInitialList[i].Movement,0,0);
						break;
					case ToothInitialType.ShiftO:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,0,ToothInitialList[i].Movement,0);
						break;
					case ToothInitialType.ShiftB:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,0,0,ToothInitialList[i].Movement);
						break;
				}
			}
			DrawProcsOfStatus(ProcStat.EO);
			DrawProcsOfStatus(ProcStat.EC);
			DrawProcsOfStatus(ProcStat.C);
			DrawProcsOfStatus(ProcStat.R);
			DrawProcsOfStatus(ProcStat.TP);
			toothChart.ResumeLayout();
			//FillMovementsAndHidden();
			Cursor=Cursors.Default;
		}

		private void DrawProcsOfStatus(ProcStat procStat) {
			//this requires: ProcStatus, ProcCode, ToothNum, Surf, and ToothRange.  All need to be raw database values.
			string[] teeth;
			Color cLight=Color.White;
			Color cDark=Color.White;
			for(int i=0;i<ProcList.Count;i++) {
				if(PIn.PInt(ProcList[i]["ProcStatus"].ToString())!=(int)procStat) {
					continue;
				}
				if(ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).PaintType==ToothPaintingType.Extraction && (
					PIn.PInt(ProcList[i]["ProcStatus"].ToString())==(int)ProcStat.C
					|| PIn.PInt(ProcList[i]["ProcStatus"].ToString())==(int)ProcStat.EC
					|| PIn.PInt(ProcList[i]["ProcStatus"].ToString())==(int)ProcStat.EO
					)) {
					continue;//prevents the red X. Missing teeth already handled.
				}
				if(ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).GraphicColor==Color.FromArgb(0)) {
					switch((ProcStat)PIn.PInt(ProcList[i]["ProcStatus"].ToString())) {
						case ProcStat.C:
							cDark=DefB.Short[(int)DefCat.ChartGraphicColors][1].ItemColor;
							cLight=DefB.Short[(int)DefCat.ChartGraphicColors][6].ItemColor;
							break;
						case ProcStat.TP:
							cDark=DefB.Short[(int)DefCat.ChartGraphicColors][0].ItemColor;
							cLight=DefB.Short[(int)DefCat.ChartGraphicColors][5].ItemColor;
							break;
						case ProcStat.EC:
							cDark=DefB.Short[(int)DefCat.ChartGraphicColors][2].ItemColor;
							cLight=DefB.Short[(int)DefCat.ChartGraphicColors][7].ItemColor;
							break;
						case ProcStat.EO:
							cDark=DefB.Short[(int)DefCat.ChartGraphicColors][3].ItemColor;
							cLight=DefB.Short[(int)DefCat.ChartGraphicColors][8].ItemColor;
							break;
						case ProcStat.R:
							cDark=DefB.Short[(int)DefCat.ChartGraphicColors][4].ItemColor;
							cLight=DefB.Short[(int)DefCat.ChartGraphicColors][9].ItemColor;
							break;
					}
				}
				else {
					cDark=ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).GraphicColor;
					cLight=ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).GraphicColor;
				}
				switch(ProcedureCodes.GetProcCode(ProcList[i]["ProcCode"].ToString()).PaintType) {
					case ToothPaintingType.BridgeDark:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,ProcList[i]["ToothNum"].ToString())) {
							toothChart.SetPontic(ProcList[i]["ToothNum"].ToString(),cDark);
						}
						else {
							toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cDark);
						}
						break;
					case ToothPaintingType.BridgeLight:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,ProcList[i]["ToothNum"].ToString())) {
							toothChart.SetPontic(ProcList[i]["ToothNum"].ToString(),cLight);
						}
						else {
							toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cLight);
						}
						break;
					case ToothPaintingType.CrownDark:
						toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.CrownLight:
						toothChart.SetCrown(ProcList[i]["ToothNum"].ToString(),cLight);
						break;
					case ToothPaintingType.DentureDark:
						if(ProcList[i]["Surf"].ToString()=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(ProcList[i]["Surf"].ToString()=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=ProcList[i]["ToothRange"].ToString().Split(new char[] { ',' });
						}
						for(int t=0;t<teeth.Length;t++) {
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cDark);
							}
							else {
								toothChart.SetCrown(teeth[t],cDark);
							}
						}
						break;
					case ToothPaintingType.DentureLight:
						if(ProcList[i]["Surf"].ToString()=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(ProcList[i]["Surf"].ToString()=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=ProcList[i]["ToothRange"].ToString().Split(new char[] { ',' });
						}
						for(int t=0;t<teeth.Length;t++) {
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cLight);
							}
							else {
								toothChart.SetCrown(teeth[t],cLight);
							}
						}
						break;
					case ToothPaintingType.Extraction:
						toothChart.SetBigX(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.FillingDark:
						toothChart.SetSurfaceColors(ProcList[i]["ToothNum"].ToString(),ProcList[i]["Surf"].ToString(),cDark);
						break;
					case ToothPaintingType.FillingLight:
						toothChart.SetSurfaceColors(ProcList[i]["ToothNum"].ToString(),ProcList[i]["Surf"].ToString(),cLight);
						break;
					case ToothPaintingType.Implant:
						toothChart.SetImplant(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.PostBU:
						toothChart.SetBU(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.RCT:
						toothChart.SetRCT(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
					case ToothPaintingType.Sealant:
						toothChart.SetSealant(ProcList[i]["ToothNum"].ToString(),cDark);
						break;
				}
			}
		}

	

		


	}
}





















