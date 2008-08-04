using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class SheetCheckBox:Control {
		private bool isChecked;
		private Pen pen=new Pen(Color.Black,1.6f);
		private bool isHovering;
		private SolidBrush hoverBrush=new SolidBrush(Color.FromArgb(224,224,224));

		public bool IsChecked {
			get{ 
				return isChecked;
			}
			set{
				isChecked=value;
				Invalidate();
			}
		}

		public SheetCheckBox() {
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe) {
			PathGradientBrush brush=new PathGradientBrush(
				new Point[] {new Point(0,0),new Point(Width-1,0),new Point(Width-1,Height-1),new Point(0,Height-1)});
			brush.CenterColor=Color.White;
			Color surroundColor=Color.FromArgb(249,187,67);
			brush.SurroundColors=new Color[] {surroundColor,surroundColor,surroundColor,surroundColor};
			//brush.SurroundColors[0]=surroundColor;
			//brush.SurroundColors[1]=surroundColor;
			//brush.SurroundColors[2]=surroundColor;
			//brush.SurroundColors[3]=surroundColor;
			Blend blend=new Blend();
			float[] myFactors = {0f,.5f,1f,1f,1f,1f};
			float[] myPositions = {0f,.2f,.4f,.6f,.8f,1f};
			blend.Factors=myFactors;
			blend.Positions=myPositions;
			brush.Blend=blend;
			base.OnPaint(pe);
			Graphics g=pe.Graphics;
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.CompositingQuality=CompositingQuality.HighQuality;
			if(isHovering){
				g.FillRectangle(brush,0,0,Width-1,Height-1);
				g.DrawRectangle(new Pen(surroundColor),0,0,Width-1,Height-1);
			}
			if(isChecked){
				g.DrawLine(pen,0,0,Width-1,Height-1);
				g.DrawLine(pen,Width-1,0,0,Height-1);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			IsChecked=!IsChecked;
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			isHovering=true;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave(e);
			isHovering=false;
			Invalidate();
		}

	}
}
