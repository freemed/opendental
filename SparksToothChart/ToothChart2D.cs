using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SparksToothChart {
	public partial class ToothChart2D:UserControl {
		///<summary>This is a reference to the TcData object that's at the wrapper level.</summary>
		public ToothChartData TcData;

		public ToothChart2D() {
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			Graphics g=e.Graphics;
			g.DrawImage(pictBox.Image,new Rectangle(0,0,this.Width,this.Height));
			/*
			g.SmoothingMode=SmoothingMode.HighQuality;
			for(int t=0;t<ListToothGraphics.Count;t++) {//loop through each tooth
				if(ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				DrawFacialView(ListToothGraphics[t],g);
				DrawOcclusalView(ListToothGraphics[t],g);
			}
			DrawNumbers(g);
			DrawDrawingSegments(g);*/
			g.Dispose();
		}

	}
}
