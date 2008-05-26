using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.UI {
	[DefaultEvent("Scroll")]
	public partial class ContrWindowingSlider:Control {
		private int minVal=64;
		private int maxVal=192;
		private float endW=7;//the width of the end sliders
		private float tick;
		private Button.ControlState butStateM = Button.ControlState.Normal;
		private Button.ControlState butStateL = Button.ControlState.Normal;
		private Button.ControlState butStateR = Button.ControlState.Normal;
		private bool mouseIsDown;
		private int mouseDownX;
		///<summary>The original pixel position of the button in question. The pointy part.</summary>
		private float originalPixL;
		private float originalPixR;
		[Category("Behavior"),Description("Occurs when the slider moves.  UI is typically updated here.  Also see ScrollComplete")]
		public event EventHandler Scroll=null;
		[Category("Behavior"),Description("Occurs when user releases slider after moving.  Database is typically updated here.  Also see Scroll event.")]
		public event EventHandler ScrollComplete=null;

		public ContrWindowingSlider() {
			InitializeComponent();
			this.DoubleBuffered=true;
		}

		///<summary></summary>
		[Category("Data"),Description("The value of the left slider.")]
		[DefaultValue(64)]
		public int MinVal{
			get{
				if(!Enabled){
					return 0;
				}
				return minVal;
			}
			set{
				minVal=value;
				Invalidate();
			}
		}

		///<summary></summary>
		[Category("Data"),Description("The value of the right slider, max 255.")]
		[DefaultValue(192)]
		public int MaxVal {
			get {
				if(!Enabled){
					return 255;
				}
				return maxVal;
			}
			set {
				maxVal=value;
				Invalidate();
			}
		}

		protected override Size DefaultSize {
			get {
				return new Size(194,14);
			}
		}

		protected override void OnPaint(PaintEventArgs pe) {
			Graphics g=pe.Graphics;
			g.SmoothingMode=SmoothingMode.HighQuality;
			SolidBrush brush=new SolidBrush(SystemColors.Control);
			g.FillRectangle(brush,0,0,Width,Height);
			tick=(float)(Width-endW-1)/255f;//gets set in mousemove also
			if(butStateM==Button.ControlState.Hover) {
				g.FillRectangle(Brushes.White,GetRectMiddle());
			}
			g.DrawRectangle(new Pen(Color.FromArgb(28,81,128)),GetRectMiddle());
			//gradient bars
			Rectangle rect=new Rectangle((int)(endW/2),2,GetRectMiddle().Left-(int)(endW/2),Height-5);
			g.FillRectangle(Brushes.Black,rect);
			if(Enabled){
				rect=new Rectangle(GetRectMiddle().Left,2,GetRectMiddle().Width,Height-5);
				LinearGradientBrush gradientBrush=new LinearGradientBrush(
					new Point(GetRectMiddle().X+(int)(endW/2),0),
					new Point(GetRectMiddle().Right-(int)(endW/2)+1,0),
					Color.Black,Color.White);
					//new Point(0,0),new Point(rect.Right+1,0),
				g.FillRectangle(gradientBrush,rect);
			}
			//this one is just to eliminate a rounding artifact
			rect=new Rectangle(GetRectMiddle().Right-(int)(endW/2)+1,2,2,Height-5);
			g.FillRectangle(Brushes.White,rect);
			rect=new Rectangle(GetRectMiddle().Right,2,Width-(int)(endW/2)-GetRectMiddle().Right,Height-5);
			g.FillRectangle(Brushes.White,rect);
			DrawButton(g,GetPathLeft(),butStateL);
			DrawButton(g,GetPathRight(),butStateR);
			base.OnPaint(pe);
		}

		///<summary>Gets the outline path of the middle button that connects the two ends. But it's partly tucked under the end buttons.</summary>
		private Rectangle GetRectMiddle(){
			//GraphicsPath path=new GraphicsPath();
			return new Rectangle((int)(endW/2f+MinVal*tick),0,(int)((MaxVal-MinVal)*tick),Height-1);
			//path.AddRectangle(rect);
			//return path;
		}

		///<summary>Gets the outline path of the left end button.</summary>
		private GraphicsPath GetPathLeft() {
			GraphicsPath path=new GraphicsPath();
			path.AddLines(new PointF[] {
				new PointF(MinVal*tick,Height-4),//start at lower left, work clockwise
				new PointF(MinVal*tick,3),
				new PointF(MinVal*tick+endW/2f,0),
				new PointF(MinVal*tick+endW,3),
				new PointF(MinVal*tick+endW,Height-4),
				new PointF(MinVal*tick+endW/2f,Height-1),
				new PointF(MinVal*tick,Height-4)
			});
			return path;
		}

		///<summary>Gets the outline path of the right end button.</summary>
		private GraphicsPath GetPathRight() {
			GraphicsPath path=new GraphicsPath();
			path.AddLines(new PointF[] {
				new PointF(MaxVal*tick,Height-4),//start at lower left, work clockwise
				new PointF(MaxVal*tick,3),
				new PointF(MaxVal*tick+endW/2f,0),
				new PointF(MaxVal*tick+endW,3),
				new PointF(MaxVal*tick+endW,Height-4),
				new PointF(MaxVal*tick+endW/2f,Height-1),
				new PointF(MaxVal*tick,Height-4)
			});
			return path;
		}

		private void DrawButton(Graphics g,GraphicsPath pathmain,Button.ControlState state){
			Color clrMain=Color.FromArgb(200,202,220);
			if(state==Button.ControlState.Hover){
				clrMain=Color.FromArgb(240,240,255);
			}
			else if(state==Button.ControlState.Pressed) {
				clrMain=Color.FromArgb(150,150,160);
			}
			GraphicsPath pathsub=new GraphicsPath();
			RectangleF rect=pathmain.GetBounds();
			pathsub.AddEllipse(rect.Left-rect.Width/8f,rect.Top-rect.Height/2f,rect.Width,rect.Height*3f/2f);
			PathGradientBrush pathBrush=new PathGradientBrush(pathsub);
			pathBrush.CenterColor=Color.FromArgb(255,255,255,255);
			pathBrush.SurroundColors=new Color[] { Color.FromArgb(0,255,255,255) };
			g.FillPath(new SolidBrush(clrMain),pathmain);
			g.FillPath(pathBrush,pathmain);
			Color clrDarkOverlay=Color.FromArgb(50,125,125,125);
			LinearGradientBrush brush=new LinearGradientBrush(new PointF(rect.Left,rect.Bottom),
				new PointF(rect.Left,rect.Top+rect.Height/2),Color.FromArgb(0,0,0,0),
				Color.FromArgb(50,0,0,0));
			g.FillPath(brush,pathmain);
			Pen outline=new Pen(Color.FromArgb(28,81,128));
			g.DrawPath(outline,pathmain);
		}

		///<summary></summary>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e) {
			if(!Enabled){
				return;
			}
			base.OnMouseMove(e);
			tick=(float)(Width-endW-1)/255f;
			if(mouseIsDown) {
				int minAllowedL=0;
				int maxAllowedL;
				int minAllowedR;
				int maxAllowedR=255;
				float deltaPix=mouseDownX-e.X;
				if(butStateL==Button.ControlState.Pressed){
					MinVal=(int)((originalPixL-deltaPix-endW/2f)/tick);
					maxAllowedL=MaxVal-(int)(endW/tick);
					if(MinVal<minAllowedL){
						MinVal=minAllowedL;
					}
					else if(MinVal>maxAllowedL){
						MinVal=maxAllowedL;
					}
					OnScroll();
				}
				else if(butStateR==Button.ControlState.Pressed){
					MaxVal=(int)((originalPixR-deltaPix-endW/2f)/tick);
					minAllowedR=MinVal+(int)(endW/tick);
					if(MaxVal<minAllowedR){
						MaxVal=minAllowedR;
					}
					else if(MaxVal>maxAllowedR){
						MaxVal=maxAllowedR;
					}
					OnScroll();
				}
				else if(butStateM==Button.ControlState.Pressed) {
					MinVal=(int)((originalPixL-deltaPix-endW/2f)/tick);
					MaxVal=(int)((originalPixR-deltaPix-endW/2f)/tick);
					int originalValSpan=(int)((originalPixR-originalPixL)/tick);
					maxAllowedL=maxAllowedR-originalValSpan;
					minAllowedR=minAllowedL+originalValSpan;
					if(MinVal<minAllowedL) {
						MinVal=minAllowedL;
					}
					else if(MinVal>maxAllowedL) {
						MinVal=maxAllowedL;
					}
					if(MaxVal<minAllowedR) {
						MaxVal=minAllowedR;
					}
					else if(MaxVal>maxAllowedR) {
						MaxVal=maxAllowedR;
					}
					OnScroll();
				}
			}
			else {//mouse is not down
				butStateL=Button.ControlState.Normal;
				butStateR=Button.ControlState.Normal;
				butStateM=Button.ControlState.Normal;
				if(GetPathLeft().IsVisible(e.Location)){
					butStateL=Button.ControlState.Hover;
				}
				else if(GetPathRight().IsVisible(e.Location)) {
					butStateR=Button.ControlState.Hover;
				}
				else if(GetRectMiddle().Contains(e.Location)){
					butStateM=Button.ControlState.Hover;
				}
			}
			Invalidate();
		}

		///<summary>Resets button appearance.  Repaints only if necessary.</summary>
		protected override void OnMouseLeave(System.EventArgs e) {
			if(!Enabled) {
				return;
			}
			base.OnMouseLeave(e);
			if(mouseIsDown) {//mouse is down
				//if a button is pressed, it will remain so, even if leave.  As long as mouse is down.
				//,so do nothing.
				//Also, if a button is not pressed, nothing will happen when leave
				//,so do nothing.
			}
			else {//mouse is not down
				butStateL=Button.ControlState.Normal;
				butStateR=Button.ControlState.Normal;
				butStateM=Button.ControlState.Normal;
				Invalidate();
			}
		}

		///<summary>Change the button to a pressed state.</summary>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e) {
			if(!Enabled) {
				return;
			}
			base.OnMouseDown(e);
			if((e.Button & MouseButtons.Left)!=MouseButtons.Left) {
				return;
			}
			mouseIsDown=true;
			mouseDownX=e.X;
			butStateL=Button.ControlState.Normal;
			butStateR=Button.ControlState.Normal;
			butStateM=Button.ControlState.Normal;
			if(GetPathLeft().IsVisible(e.Location)){//if mouse pressed within the left button
				butStateL=Button.ControlState.Pressed;
				originalPixL=(float)MinVal*tick+endW/2f;
			}
			else if(GetPathRight().IsVisible(e.Location)) {
				butStateR=Button.ControlState.Pressed;
				originalPixR=(float)MaxVal*tick+endW/2f;
			}
			else if(GetRectMiddle().Contains(e.Location)) {
				butStateM=Button.ControlState.Pressed;
				originalPixL=(float)MinVal*tick+endW/2f;
				originalPixR=(float)MaxVal*tick+endW/2f;
			}
			Invalidate();
		}

		///<summary>Change button to hover state and repaint if needed.</summary>
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e) {
			if(!Enabled) {
				return;
			}
			base.OnMouseUp(e);
			if((e.Button & MouseButtons.Left)!=MouseButtons.Left) {
				return;
			}
			mouseIsDown=false;
			if(butStateL==Button.ControlState.Pressed
				|| butStateR==Button.ControlState.Pressed
				|| butStateM==Button.ControlState.Pressed)
			{
				OnScrollComplete();
			}
			butStateL=Button.ControlState.Normal;
			butStateR=Button.ControlState.Normal;
			butStateM=Button.ControlState.Normal;
			if(GetPathLeft().IsVisible(e.Location)) {
				butStateL=Button.ControlState.Hover;
			}
			else if(GetPathRight().IsVisible(e.Location)) {
				butStateR=Button.ControlState.Hover;
			}
			else if(GetRectMiddle().Contains(e.Location)) {
				butStateM=Button.ControlState.Hover;
			}
			Invalidate();
		}

		///<summary></summary>
		protected void OnScroll() {
			if(!Enabled) {
				return;
			}
			EventArgs ea=new EventArgs();
			if(Scroll!=null){
				Scroll(this,ea);
			}
		}

		///<summary></summary>
		protected void OnScrollComplete() {
			if(!Enabled) {
				return;
			}
			EventArgs ea=new EventArgs();
			if(ScrollComplete!=null) {
				ScrollComplete(this,ea);
			}
		}

	}
}
