using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

namespace OpenDental.UI
{
	/// <summary>
	/// Summary description for Button
	/// </summary>
	public class Button : System.Windows.Forms.Button{
		private ControlState butState = ControlState.Normal;
		private bool bCanClick = false;
		private Point adjustImageLocation;
		private bool autosize=true;
		private float cornerRadius=4;
		private Color colorBorder;
		private Color colorDisabledFore;
		private Color colorShadow;
		private Color colorDarkest;
		private Color colorMain;
		private Color colorLightest;
		private Color colorDarkDefault;
		private Color colorHoverDark;//the outline when hovering
		private Color colorHoverLight;

		///<summary></summary>
		public enum ControlState{
			/// <summary>button is in the normal state.</summary>
			Normal,
			/// <summary>button is in the hover state.</summary>
			Hover,
			/// <summary>button is in the pressed state.</summary>
			Pressed,
			/// <summary>button is in the default state.</summary>
			Default,
			/// <summary>button is in the disabled state.</summary>
			Disabled		
		}		
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;

		///<summary>Initializes a new instance of the Button class.</summary>
		public Button(){
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
				ControlStyles.DoubleBuffer, true);
			colorBorder      =Color.FromArgb(28,81,128);//150,190,210);
			colorDisabledFore=Color.FromArgb(161,161,146);
			colorShadow      =Color.FromArgb(180,180,180);
			colorDarkest     =Color.FromArgb(157,164,196);//125,136,184);//87,102,166);//50,70,150);
			colorLightest    =Color.FromArgb(255,255,255);
			colorMain        =Color.FromArgb(200,202,220);
			colorDarkDefault =Color.FromArgb(50,70,230);
			colorHoverDark   =Color.FromArgb(255,190,100);//(255,165,0) is pure orange
			colorHoverLight  =Color.FromArgb(255,210,130);//(255,223,163) is a fairly light orange
		}

		///<summary>Clean up any resources being used</summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		///<summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Properties
		///<summary>Just for compatibility</summary>
		public enumType.BtnShape BtnShape {
			get{
				return enumType.BtnShape.Rectangle;
			}
			set {
			}
		}

		///<summary></summary>
		//[DefaultValue("Silver"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
		public enumType.XPStyle BtnStyle {
			get {
				return enumType.XPStyle.Silver;
			}
			set {
				//m_btnStyle = value;
				//this.Invalidate();
			}
		}

		///<summary></summary>
		public new FlatStyle FlatStyle{
			get{
				return base.FlatStyle;
			}
			set{//if user tries to change, they can't
				base.FlatStyle=FlatStyle.Standard;
			}
		}

		///<summary></summary>
		public bool Autosize{
			get{return autosize;}
			set{autosize=value;}
		}

		///<summary></summary>
		public Point AdjustImageLocation{
			get{
				return adjustImageLocation;
			}
			set{ 
				adjustImageLocation=value;
				this.Invalidate();
			}
		}

		///<summary>Typically 4. This property is rarely used</summary>
		public float CornerRadius {
			get {
				return cornerRadius;
			}
			set {
				cornerRadius=value;
				this.Invalidate();
			}
		}
	
		#endregion

		#region Overridden Event Handlers
		///<summary></summary>
		protected override void OnClick(EventArgs ea){
			this.Capture = false;
			bCanClick = false;
			if(this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
				butState = ControlState.Hover;
			else
				butState = ControlState.Normal;
			this.Invalidate();
			base.OnClick(ea);
		}

		///<summary></summary>
		protected override void OnMouseEnter(EventArgs ea){
			base.OnMouseEnter(ea);
			butState = ControlState.Hover;
			this.Invalidate();
		}

		///<summary></summary>
		protected override void OnMouseDown(MouseEventArgs mea){
			base.OnMouseDown(mea);
			if (mea.Button == MouseButtons.Left){
				bCanClick = true;
				butState = ControlState.Pressed;
				this.Invalidate();
			}
		}

		///<summary></summary>
		protected override void OnMouseMove(MouseEventArgs mea){
			base.OnMouseMove(mea);
			if (ClientRectangle.Contains(mea.X, mea.Y)) {
				if(butState == ControlState.Hover && this.Capture && !bCanClick){
					bCanClick = true;
					butState = ControlState.Pressed;
					this.Invalidate();
				}
			}
			else{
				if(butState == ControlState.Pressed){
					bCanClick = false;
					butState = ControlState.Hover;
					this.Invalidate();
				}
			}
		}

		///<summary></summary>
		protected override void OnMouseLeave(EventArgs ea){
			base.OnMouseLeave(ea);
			butState = ControlState.Normal;
			this.Invalidate();
		}

		///<summary></summary>
		protected override void OnEnabledChanged(EventArgs ea){
			base.OnEnabledChanged(ea);
			butState = ControlState.Normal;
			this.Invalidate();
		}

		///<summary></summary>
		protected override void OnTextChanged(EventArgs e) {
			base.OnTextChanged (e);
			if(autosize && Text!=""){
				Graphics g=this.CreateGraphics();
				int buffer=6;
				int textWidth=(int)g.MeasureString(Text,Font).Width;
				int oldWidth=Width;
				if(this.Image==null){
					if(Width<textWidth+buffer){
						Width=textWidth+buffer;
					}
				}
				else{
					if(Width<textWidth+Image.Size.Width+buffer){
						Width=textWidth+Image.Size.Width+buffer;
					}
				}
				if((Anchor & AnchorStyles.Right)==AnchorStyles.Right){
					//to be perfect, it would exclude if anchored left also
					//this works even if no change in width
					Left+=oldWidth-Width;
				}
			}
		}
		#endregion Overridden Event Handlers

		#region Painting
		///<summary></summary>
		protected override void OnPaint(PaintEventArgs p) {
			this.OnPaintBackground(p);
			Graphics g=p.Graphics;
			RectangleF recOutline=new RectangleF(0,0,ClientRectangle.Width-1,ClientRectangle.Height-1);
			float radius=cornerRadius;
			switch(butState) {
				case ControlState.Normal:
					if(Enabled) {
						radius=cornerRadius;
						if(Focused || IsDefault) {
							DrawBackground(g,recOutline,radius,colorDarkDefault,colorMain,colorLightest);
						}
						else{
							DrawBackground(g,recOutline,radius,colorDarkest,colorMain,colorLightest);
						}
					}
					else {
						radius=cornerRadius;
						//DrawBackground(g,recOutline,radius,colorDarkest,colorMain,colorLightest);
					}
					break;
				case ControlState.Hover:
					radius=cornerRadius;
					DrawBackground(g,recOutline,radius,colorHoverDark,colorMain,colorHoverLight);
					break;
				case ControlState.Pressed:
					radius=cornerRadius-3;
					DrawBackground(g,recOutline,radius,colorDarkest,colorMain,colorLightest);
					break;
			}
			// enmState will never be == ControlState.Default
			// When (IsDefault == true), enmState will be == ControlState.Normal
			// So when (IsDefault == true), pass ControlState.Default instead of enmState
			//g.DrawLine(Pens.Red,new PointF(recOutline.X,recOutline.Y),
			//	new PointF(recOutline.Right,recOutline.Bottom));
			float diagonalLength=(float)Math.Sqrt(recOutline.Height*recOutline.Height+recOutline.Width*recOutline.Width);
			//unit vector representing direction of diagonal
			float unitvectorx=recOutline.Width/diagonalLength;
			float unitvectory=-recOutline.Height/diagonalLength;
			//g.DrawLine(Pens.Red,0,recOutline.Bottom,unitvectorx*800,recOutline.Bottom+unitvectory*800);
			//unit vector rotated 90 degrees:
			float unitvector90x=-unitvectory;
			float unitvector90y=unitvectorx;
			//g.DrawLine(Pens.Red,0,0,unitvector90x*200,unitvector90y*200);
			//compute the location where the two vectors intersect.
			//solve for x,y, 
			//solve for x
			//x=recOutline.X+unitvectorx*length;
			//x=recOutline.X+unitvector90x*length90;
			//So   length=(unitvector90x*length90)/unitvectorx
			//y=recOutline.Height+unitvectory*length;
			//y=unitvector90y*length90;
			//So   length=(unitvector90y*length90-recOutline.Height)/unitvectory;
			//Combine the two equations:
			//(unitvector90x*length90)/unitvectorx=(unitvector90y*length90-recOutline.Height)/unitvectory;
			//Solve for length90
			//(unitvector90x*length90)*unitvectory=unitvectorx*(unitvector90y*length90-recOutline.Height);
			//unitvector90x*length90*unitvectory=unitvectorx*unitvector90y*length90-unitvectorx*recOutline.Height;
			//unitvectorx*unitvector90y*length90-unitvector90x*length90*unitvectory=unitvectorx*recOutline.Height;
			//length90(unitvectorx*unitvector90y-unitvector90x*unitvectory)=unitvectorx*recOutline.Height;
			float length90=unitvectorx*recOutline.Height/(unitvectorx*unitvector90y-unitvector90x*unitvectory);
			//g.DrawEllipse(Pens.Red,unitvector90x*length90-1,unitvector90y*length90-1,2,2);
			/*LinearGradientBrush brush=new LinearGradientBrush(new PointF(recOutline.X,recOutline.Y),
				new PointF(unitvector90x*length90*2,unitvector90y*length90*2),
				colorBorder,colorDarkest);
			if(IsDefault){
				DrawRoundedRectangle(g,new Pen(colorDarkDefault),recOutline,radius);
			}
			else{
				DrawRoundedRectangle(g,new Pen(brush),recOutline,radius);
			}*/
			DrawRoundedRectangle(g,new Pen(colorBorder),recOutline,radius);
			DrawTextAndImage(g);
			DrawReflection(g,recOutline,radius);
		}

		///<summary>Draws a rectangle with rounded edges.</summary>
		public static void DrawRoundedRectangle(Graphics grfx,Pen pen,RectangleF rect,float round) {
			SmoothingMode oldSmoothingMode = grfx.SmoothingMode;
			grfx.SmoothingMode = SmoothingMode.AntiAlias;
			//top
			grfx.DrawLine(pen,rect.Left+round,rect.Top,rect.Right-round,rect.Top);
			grfx.DrawArc(pen,rect.Right-round*2,rect.Top,round*2,round*2,270,90);
			//
			grfx.DrawLine(pen,rect.Right,rect.Top+round,rect.Right,rect.Bottom-round);
			grfx.DrawArc(pen,rect.Right-round*2,rect.Bottom-round*2,round*2,round*2,0,90);
			//
			grfx.DrawLine(pen,rect.Right-round,rect.Bottom,rect.Left+round,rect.Bottom);
			grfx.DrawArc(pen,rect.Left,rect.Bottom-round*2,round*2,round*2,90,90);
			//
			grfx.DrawLine(pen,rect.Left,rect.Bottom-round,rect.Left,rect.Top+round);
			grfx.DrawArc(pen,rect.Left,rect.Top,round*2,round*2,180,90);
			//
			grfx.SmoothingMode = oldSmoothingMode;
		}

		private void DrawReflection(Graphics g,RectangleF rect,float radius){
			//lower--------------------------------------------------------------------
			Color clrDarkOverlay=Color.FromArgb(50,125,125,125);
			LinearGradientBrush brush=new LinearGradientBrush(new PointF(rect.Left,rect.Bottom),
				new PointF(rect.Left,rect.Top+rect.Height/2-radius*2f),Color.FromArgb(0,0,0,0),
				Color.FromArgb(50,0,0,0));
			GraphicsPath path=new GraphicsPath();
			path.AddLine(rect.Left+radius,rect.Top+rect.Height/2f,rect.Right-radius*2f,rect.Top+rect.Height/2f);
			path.AddArc(new RectangleF(rect.Right-(radius*4f),rect.Top+rect.Height/2f-radius*4f,radius*4f,radius*4f),90,-90);
			path.AddLine(rect.Right,rect.Top+rect.Height/2f-radius,rect.Right,rect.Bottom);
			path.AddLine(rect.Right,rect.Bottom,rect.Left,rect.Bottom);
			path.AddLine(rect.Left,rect.Bottom,rect.Left,rect.Top+rect.Height/2f-radius/2f);
			path.AddArc(new RectangleF(rect.Left,rect.Top+rect.Height/2f-radius,radius*2f,radius),180,-90);
			//g.DrawPath(Pens.Red,path);
			g.FillPath(brush,path);
		}

		private void DrawBackground(Graphics g,RectangleF rect,float radius,Color clrDark,Color clrMain,Color clrLight){
			if(radius<0){
				radius=0;
			}
			LinearGradientBrush brush;
			SolidBrush brushS=new SolidBrush(clrMain);
			g.SmoothingMode = SmoothingMode.HighQuality;
			//sin(45)=.85. But experimentally, .7 works much better.
			//1/.85=1.18 But experimentally, 1.37 works better. What gives?
			//top
			brush=new LinearGradientBrush(new PointF(rect.Left+radius,rect.Top+radius),
				new PointF(rect.Left+radius,rect.Top),
				clrMain,clrLight);
			g.FillRectangle(brushS,rect.Left+radius,rect.Top,rect.Width-(radius*2),radius);
			//UR
			//2 pies of 45 each.
			brush=new LinearGradientBrush(new PointF(rect.Right-radius,rect.Top),
				new PointF(rect.Right-(radius/2f),rect.Top+(radius/2f)),
				clrLight,clrMain);
			g.FillPie(brushS,rect.Right-(radius*2),rect.Top,radius*2,radius*2,270,45);
			brush=new LinearGradientBrush(new PointF(rect.Right-(radius/2f)-.5f,rect.Top+(radius/2f)-.5f),
				new PointF(rect.Right,rect.Top+radius),
				clrMain,clrDark);
			g.FillPie(brush,rect.Right-(radius*2),rect.Top,radius*2,radius*2,315,45);
			//right
			brush=new LinearGradientBrush(new PointF(rect.Right-radius,rect.Top+radius),
				new PointF(rect.Right,rect.Top+radius),
				clrMain,clrDark);
			g.FillRectangle(brush,rect.Right-radius,rect.Top+radius-.5f,radius,rect.Height-(radius*2)+1f);
			//LR
			g.FillPie(new SolidBrush(clrDark),rect.Right-(radius*2),rect.Bottom-(radius*2),radius*2,radius*2,0,90);
			brush=new LinearGradientBrush(new PointF(rect.Right-radius,rect.Bottom-radius),
				new PointF(rect.Right-(radius*.5f)+.5f,rect.Bottom-(radius*.5f)+.5f),
				clrMain,clrDark);
			g.FillPolygon(brush,new PointF[] {
				new PointF(rect.Right-radius,rect.Bottom-radius),
				new PointF(rect.Right,rect.Bottom-radius),
				new PointF(rect.Right-radius,rect.Bottom)});
			//bottom
			brush=new LinearGradientBrush(new PointF(rect.Left+radius,rect.Bottom-radius),
				new PointF(rect.Left+radius,rect.Bottom),
				clrMain,clrDark);
			g.FillRectangle(brush,rect.Left+radius-.5f,rect.Bottom-radius,rect.Width-(radius*2)+1f,radius);
			//LL
			//2 pies of 45 each.
			brush=new LinearGradientBrush(new PointF(rect.Left+(radius/2f),rect.Bottom-(radius/2f)),
				new PointF(rect.Left+radius,rect.Bottom),
				clrMain,clrDark);
			g.FillPie(brush,rect.Left,rect.Bottom-(radius*2),radius*2,radius*2,90,45);
			brush=new LinearGradientBrush(new PointF(rect.Left+(radius/2f),rect.Bottom-(radius/2f)),
				new PointF(rect.Left,rect.Bottom-radius),
				clrMain,clrLight);
			g.FillPie(brushS,rect.Left,rect.Bottom-(radius*2),radius*2,radius*2,135,45);
			//left
			brush=new LinearGradientBrush(new PointF(rect.Left+radius,rect.Top),
				new PointF(rect.Left,rect.Top),
				clrMain,clrLight);
			g.FillRectangle(brushS,rect.Left,rect.Top+radius,radius,rect.Height-(radius*2));
			//UL
			g.FillPie(//new SolidBrush(clrLight)
				brushS,rect.Left,rect.Top,radius*2,radius*2,180,90);
			brush=new LinearGradientBrush(new PointF(rect.Left+radius,rect.Top+radius),
				new PointF(rect.Left+(radius/2f),rect.Top+(radius/2f)),
				clrMain,clrLight);
			//center
			GraphicsPath path=new GraphicsPath();
			path.AddEllipse(rect.Left-rect.Width/8f,rect.Top-rect.Height/2f,rect.Width,rect.Height*3f/2f);
			PathGradientBrush pathBrush=new PathGradientBrush(path);
			pathBrush.CenterColor=Color.FromArgb(255,255,255,255);
			pathBrush.SurroundColors=new Color[] {Color.FromArgb(0,255,255,255)};
			g.FillRectangle(new SolidBrush(clrMain),
				rect.Left+radius-.5f,rect.Top+radius-.5f,
				rect.Width-(radius*2)+1f,rect.Height-(radius*2)+1f);
			g.FillRectangle(
				pathBrush,
				rect.Left+radius-.5f,rect.Top+radius-.5f,
				rect.Width-(radius*2)+1f,rect.Height-(radius*2)+1f);
			//highlights
			brush=new LinearGradientBrush(new PointF(rect.Left+radius,rect.Top),
				new PointF(rect.Left+radius+rect.Width*2f/3f,rect.Top),
				clrLight,clrMain);
			g.FillRectangle(brush,rect.Left+radius,rect.Y+radius*3f/8f,rect.Width/2f,radius/4f);
			path=new GraphicsPath();
			path.AddLine(rect.Left+radius,rect.Top+radius*3/8,rect.Left+radius,rect.Top+radius*5/8);
			path.AddArc(new RectangleF(rect.Left+radius*5/8,rect.Top+radius*5/8,radius*3/4,radius*3/4),270,-90);
			path.AddArc(new RectangleF(rect.Left+radius*3/8,rect.Top+radius*7/8,radius*1/4,radius*1/4),0,180);
			path.AddArc(new RectangleF(rect.Left+radius*3/8,rect.Top+radius*3/8,radius*5/4,radius*5/4),180,90);
			//g.DrawPath(Pens.Red,path);
			g.FillPath(new SolidBrush(clrLight),path);
		}

		///<summary>Draws the text and image</summary>
		private void DrawTextAndImage(Graphics g){
			g.SmoothingMode = SmoothingMode.HighQuality;
			SolidBrush brushText;
			SolidBrush brushGlow=null;
			if(Enabled){
				brushText=new SolidBrush(ForeColor);
				brushGlow=new SolidBrush(Color.White);
			}
			else{
				brushText=new SolidBrush(colorDisabledFore);
			}
			StringFormat sf=GetStringFormat(this.TextAlign);
			if(ShowKeyboardCues){
				sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			}
			else{
				sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
			}
			RectangleF recGlow1;
			if(this.Image != null){
				Rectangle rc=new Rectangle();
				Point ImagePoint= new Point(6, 4);
				switch(this.ImageAlign)
				{
					case ContentAlignment.MiddleLeft:
						ImagePoint.X = 6;
						ImagePoint.Y = this.ClientRectangle.Height/2-Image.Height/2;
						rc.Width=this.ClientRectangle.Width-this.Image.Width;
						rc.Height=this.ClientRectangle.Height;
						rc.X=this.Image.Width;
						rc.Y=0;
						break;
					case ContentAlignment.MiddleRight:
						rc.Width=this.ClientRectangle.Width-this.Image.Width-8;
						rc.Height=this.ClientRectangle.Height;
						rc.X=0;
						rc.Y=0;
						ImagePoint.X = rc.Width;
						ImagePoint.Y = this.ClientRectangle.Height/2-Image.Height/2;
						break;
					case ContentAlignment.MiddleCenter:// no text in this alignment
						ImagePoint.X = (this.ClientRectangle.Width-this.Image.Width)/2;
						ImagePoint.Y = (this.ClientRectangle.Height-this.Image.Height)/2;
						rc.Width=0;
						rc.Height=0;
						rc.X=this.ClientRectangle.Width;
						rc.Y=this.ClientRectangle.Height;
						break;
				}
				ImagePoint.X+=adjustImageLocation.X;
				ImagePoint.Y+=adjustImageLocation.Y;
				if(this.Enabled){
					g.DrawImage(this.Image,ImagePoint);
				}
				else{
					System.Windows.Forms.ControlPaint.DrawImageDisabled(g,this.Image,ImagePoint.X,ImagePoint.Y,BackColor);
				}
				recGlow1=new RectangleF(rc.X+.5f,rc.Y+.5f,rc.Width,rc.Height);
				if(this.ImageAlign != ContentAlignment.MiddleCenter){
					if(Enabled){
						//first draw white text slightly off center
						g.DrawString(this.Text,this.Font,brushGlow,recGlow1,sf);
					}
					//then, the black text
					g.DrawString(this.Text,this.Font,brushText,rc,sf);
				}
			}
			else{//image is null
				recGlow1=new RectangleF(ClientRectangle.X+.5f,ClientRectangle.Y+.5f,ClientRectangle.Width,ClientRectangle.Height);
				if(this.Enabled){
					g.DrawString(this.Text,this.Font,brushGlow,recGlow1,sf);
				}
				g.DrawString(this.Text,this.Font,brushText,this.ClientRectangle,sf);
			}
			brushText.Dispose();
			sf.Dispose();
		}
		#endregion Painting

		private StringFormat GetStringFormat(ContentAlignment contentAlignment) {
			if(!Enum.IsDefined(typeof(ContentAlignment),(int)contentAlignment))
				throw new System.ComponentModel.InvalidEnumArgumentException(
					"contentAlignment",(int)contentAlignment,typeof(ContentAlignment));
			StringFormat stringFormat = new StringFormat();
			switch(contentAlignment) {
				case ContentAlignment.MiddleCenter:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleLeft:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopCenter:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopLeft:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomCenter:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomLeft:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.BottomRight:
					stringFormat.LineAlignment = StringAlignment.Far;
					stringFormat.Alignment = StringAlignment.Far;
					break;
			}
			return stringFormat;
		}

		

		
	}

	///<summary>Just for backward compatibility.</summary>
	public class enumType {
		///<summary></summary>
		public enum XPStyle {
			///<summary></summary>
			Default,
			///<summary></summary>
			Blue,
			///<summary></summary>
			OliveGreen,
			///<summary></summary>
			Silver
		}
		///<summary></summary>
		public enum BtnShape {
			///<summary></summary>
			Rectangle,
			///<summary></summary>
			Ellipse
		}
	}


}
