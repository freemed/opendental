using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public delegate void ButtonClickedEventHandler(object sender,ButtonClicked_EventArgs e);
	/// <summary></summary>
	public class OutlookBar : System.Windows.Forms.Control{
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public OutlookButton[] Buttons;
		private ImageList imageList;
		///<summary></summary>
		public int SelectedIndex=-1;
		private int currentHot=-1;
		private Font textFont=new Font("Arial",8);
		///<summary></summary>
		[Category("Action"),Description("Occurs when a button is clicked.")]
		public event ButtonClickedEventHandler ButtonClicked = null;
		///<summary>Used when click event is cancelled.</summary>
		private int previousSelected;

		///<summary></summary>
		public OutlookBar(){
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			Buttons=new OutlookButton[7];
			Buttons[0]=new OutlookButton("Appts",0);
			Buttons[1]=new OutlookButton("Family",1);
			Buttons[2]=new OutlookButton("Account",2);
			Buttons[3]=new OutlookButton("Treat' Plan",3);
			Buttons[4]=new OutlookButton("Chart",4);
			Buttons[5]=new OutlookButton("Images",5);
			Buttons[6]=new OutlookButton("Manage",6);
			UpdateAll();
		}

		/// <summary>Clean up any resources being used.</summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		///<summary></summary>
		[Category("Appearance"),
			Description("The image list to get the image to display in the face of the control.")
		]
		public ImageList ImageList{
			get{ 
				return imageList; 
			}
			set{ 
				imageList=value;
			}
		}
		
		/// <summary>Triggered every time the control decides to repaint itself.</summary>
		/// <param name="pe"></param>
		protected override void OnPaint(PaintEventArgs pe){
			CalculateButtonInfo();
			bool isHot;
			bool isSelected;
			bool isPressed;
			pe.Graphics.DrawLine(Pens.Gray,Width-1,0,Width-1,Height-1);
			for(int i=0;i<Buttons.Length;i++){
				Point mouseLoc = this.PointToClient(Control.MousePosition);
				isHot=Buttons[i].Bounds.Contains(mouseLoc);
				if(Control.MouseButtons==MouseButtons.Left && isHot)
					isPressed=true;
				else
					isPressed=false;
				if(i==SelectedIndex)
					isSelected=true;
				else isSelected=false;
				DrawButton(Buttons[i],isHot,isPressed,isSelected);
			}
			// Calling the base class OnPaint
			base.OnPaint(pe);
		}

		/// <summary>Draws one button using the info specified.</summary>
		/// <param name="myButton">Contains caption, image and bounds info.</param>
		/// <param name="isHot">Is the mouse currently hovering over this button.</param>
		/// <param name="isPressed">Is the left mouse button currently down on this button.</param>
		/// <param name="isSelected">Is this the currently selected button</param>
		private void DrawButton(OutlookButton myButton,bool isHot,bool isPressed,bool isSelected){
			Graphics g=this.CreateGraphics();
			Color hotColor=Color.FromArgb(235,235,235);//.FromArgb(210,218,232);
			Color pressedColor=Color.FromArgb(210,210,210);//(182,193,214);
			Color selectedColor=Color.White;
			Color outlineColor=Color.FromArgb(28,81,128);//Color.Gray;
			SolidBrush textBrush = new SolidBrush(Color.Black);
			SolidBrush backgBrush=new SolidBrush(SystemColors.Control);
			StringFormat format = new StringFormat();
			format.Alignment=StringAlignment.Center;
			if(!myButton.Visible){
				g.FillRectangle(backgBrush,myButton.Bounds.X,myButton.Bounds.Y
					,myButton.Bounds.Width+1,myButton.Bounds.Height+1);
				g.Dispose();
				return;
			}
			if(isPressed){
				g.FillRectangle(new SolidBrush(pressedColor),myButton.Bounds.X,myButton.Bounds.Y
					,myButton.Bounds.Width+1,myButton.Bounds.Height+1);
			}
			else if(isSelected){
				g.FillRectangle(new SolidBrush(Color.White),myButton.Bounds.X,myButton.Bounds.Y
					,myButton.Bounds.Width+1,myButton.Bounds.Height+1);
				Rectangle gradientRect=new Rectangle(myButton.Bounds.X
					,myButton.Bounds.Y+myButton.Bounds.Height-10
					,myButton.Bounds.Width,10);
				LinearGradientBrush hotBrush=new LinearGradientBrush(gradientRect,Color.White,pressedColor,
						LinearGradientMode.Vertical);
				g.FillRectangle(hotBrush,myButton.Bounds.X,myButton.Bounds.Y+myButton.Bounds.Height-10
					,myButton.Bounds.Width+1,10);
			}
			else if(isHot){
				g.FillRectangle(new SolidBrush(hotColor),myButton.Bounds.X,myButton.Bounds.Y
					,myButton.Bounds.Width+1,myButton.Bounds.Height+1);
			}
			else{
				g.FillRectangle(new SolidBrush(SystemColors.Control),myButton.Bounds.X,myButton.Bounds.Y
					,myButton.Bounds.Width+1,myButton.Bounds.Height+1);
			}
			//outline
			if(isPressed || isSelected || isHot){
				//block out the corners so they won't show.  This can be improved later.
				g.FillPolygon(backgBrush,new Point[] {
					new Point(myButton.Bounds.X,myButton.Bounds.Y),
					new Point(myButton.Bounds.X+3,myButton.Bounds.Y),
					new Point(myButton.Bounds.X,myButton.Bounds.Y+3)});
				g.FillPolygon(backgBrush,new Point[] {//it's one pixel to the right because of the way rect drawn.
					new Point(myButton.Bounds.X+myButton.Bounds.Width-2,myButton.Bounds.Y),
					new Point(myButton.Bounds.X+myButton.Bounds.Width+1,myButton.Bounds.Y),
					new Point(myButton.Bounds.X+myButton.Bounds.Width+1,myButton.Bounds.Y+3)});
				g.FillPolygon(backgBrush,new Point[] {//it's one pixel down and right.
					new Point(myButton.Bounds.X+myButton.Bounds.Width+1,myButton.Bounds.Y+myButton.Bounds.Height-3),
					new Point(myButton.Bounds.X+myButton.Bounds.Width+1,myButton.Bounds.Y+myButton.Bounds.Height+1),
					new Point(myButton.Bounds.X+myButton.Bounds.Width-3,myButton.Bounds.Y+myButton.Bounds.Height+1)});
				g.FillPolygon(backgBrush,new Point[] {//it's one pixel down
					new Point(myButton.Bounds.X,myButton.Bounds.Y+myButton.Bounds.Height-3),
					new Point(myButton.Bounds.X+3,myButton.Bounds.Y+myButton.Bounds.Height+1),
					new Point(myButton.Bounds.X,myButton.Bounds.Y+myButton.Bounds.Height+1)});
				//g.FillRectangle(backgBrush,myButton.Bounds.X,myButton.Bounds.Y,2,2);
				//g.FillRectangle(backgBrush,myButton.Bounds.X+,myButton.Bounds.Y,2,2);
				//then draw outline
				DrawRoundedRectangle(g,new Pen(outlineColor,1),myButton.Bounds,4);
			}
			//Image
			Rectangle imgRect = new Rectangle((this.Width-32)/2,myButton.Bounds.Y+3,32,32);
			if(myButton.ImageIndex > -1 && this.ImageList != null 
				&& myButton.ImageIndex < this.ImageList.Images.Count)
			{
				g.DrawImage(ImageList.Images[myButton.ImageIndex],imgRect);
			}
			//Text
			Rectangle textRect = new Rectangle(myButton.Bounds.X-1,imgRect.Bottom+3
				,myButton.Bounds.Width+2,myButton.Bounds.Bottom-imgRect.Bottom+3);
			g.DrawString(myButton.Caption,textFont,textBrush,textRect,format);
			g.Dispose();
		}

		/// <summary>Draws a rectangle with rounded edges.</summary>
		/// <param name="grfx">The System.Drawing.Graphics object to be used to draw the rectangle.</param>
		/// <param name="pen">A System.Drawing.Pen object that determines the color, width, and style of the rectangle.</param>
		/// <param name="rect">A System.Drawing.Rectangle structure that represents the rectangle to draw.</param>
		/// <param name="round">Determines the radius of the corners.</param>
		public static void DrawRoundedRectangle(Graphics grfx, Pen pen, Rectangle rect, int round){
			SmoothingMode oldSmoothingMode = grfx.SmoothingMode;
			grfx.SmoothingMode = SmoothingMode.AntiAlias;
			//top,right
			grfx.DrawLine(pen,rect.Left+round,rect.Top,rect.Right-round,rect.Top);
			grfx.DrawArc(pen,rect.Right-round*2,rect.Top,round*2,round*2,270,90);
			//right,bottom
			grfx.DrawLine(pen,rect.Right,rect.Top+round,rect.Right,rect.Bottom-round);
			grfx.DrawArc(pen,rect.Right-round*2,rect.Bottom-round*2,round*2,round*2,0,90);
			//bottom,left
			grfx.DrawLine(pen,rect.Right-round,rect.Bottom,rect.Left+round,rect.Bottom);
			grfx.DrawArc(pen,rect.Left,rect.Bottom-round*2,round*2,round*2,90,90);
			//left,top
			grfx.DrawLine(pen,rect.Left,rect.Bottom-round,rect.Left,rect.Top+round);
			grfx.DrawArc(pen,rect.Left,rect.Top,round*2,round*2,180,90);
			//
			grfx.SmoothingMode = oldSmoothingMode;
		}

		internal void UpdateAll(){
			// Calculates Button info and redraws all.
			//if(!m_BeginUpdate){
				CalculateButtonInfo();
				this.Invalidate();
			//}
		}

		private void CalculateButtonInfo(){
			// Calculates button sizes and maybe more later
			//int barTop = 1;
			using(Graphics g = this.CreateGraphics()){
				int top=0;
				int width=this.Width-2;
				int textHeight=0;
				for(int i=0;i<Buttons.Length;i++){
					//--- Look if multiline text, if is add extra Height to button.
					SizeF textSize = g.MeasureString(Buttons[i].Caption,textFont,width+2);
					textHeight = (int)(Math.Ceiling(textSize.Height));
					if(textHeight<26)
						textHeight=26;//default to height of 2 lines of text for uniformity.
					Buttons[i].Bounds=new Rectangle(0,top,width,39+textHeight);
					top+=39+textHeight+1;
				}//for
			}//using

		}

		///<summary></summary>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e){
			base.OnMouseMove(e);
			//Graphics g=this.CreateGraphics();
			int hotBut=GetButtonI(new Point(e.X,e.Y));
			if(hotBut != currentHot){
				if(currentHot!=-1){
					//undraw previous button
					DrawButton(Buttons[currentHot],false,false,currentHot==SelectedIndex);
				}
				if(hotBut!=-1){
					//then draw current hot
					DrawButton(Buttons[hotBut],true,Control.MouseButtons==MouseButtons.Left,hotBut==SelectedIndex);
				}
				currentHot=hotBut;
			}			
		}

		///<summary></summary>
		protected override void OnMouseLeave(System.EventArgs e){
			base.OnMouseLeave(e);
			//Graphics g=this.CreateGraphics();
			if(currentHot!=-1){
				//undraw previous button
				DrawButton(Buttons[currentHot],false,false,currentHot==SelectedIndex);
			}
			currentHot=-1;		
		}

		///<summary></summary>
		protected override void OnSizeChanged(System.EventArgs e){
			base.OnSizeChanged(e);
			this.UpdateAll();
		}

		private int GetButtonI(Point myPoint){
			for(int i=0;i<Buttons.Length;i++){
				//Item item = activeBar.Items[it];
				if(Buttons[i].Bounds.Contains(myPoint)){
					return i;
				}
			}//for
			return -1;
		}

		///<summary></summary>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e){
			base.OnMouseDown(e);
			//Graphics g=this.CreateGraphics();
			if(currentHot != -1){
				//redraw current button to give feedback on mouse down.
				DrawButton(Buttons[currentHot],false,true,currentHot==SelectedIndex);
			}
		}

		///<summary></summary>
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e){
			base.OnMouseUp(e);
			if(e.Button != MouseButtons.Left){
				return;
			}
			int selectedBut=GetButtonI(new Point(e.X,e.Y));
			if(selectedBut==-1)
				return;
			int oldSelected=SelectedIndex;
			this.previousSelected=SelectedIndex;
			SelectedIndex=selectedBut;
			if(SelectedIndex!=oldSelected && oldSelected!=-1){
				//undraw old selected
				DrawButton(Buttons[oldSelected],false,false,false);
			}
			DrawButton(Buttons[SelectedIndex],true,false,true);	
			OnButtonClicked(Buttons[SelectedIndex],false);
		}

		///<summary></summary>
		protected void OnButtonClicked(OutlookButton myButton,bool myCancel){
			if(this.ButtonClicked != null){
				//previousSelected=SelectedIndex;
				ButtonClicked_EventArgs oArgs = new ButtonClicked_EventArgs(myButton,myCancel);
				this.ButtonClicked(this,oArgs);
				if(oArgs.Cancel){
					SelectedIndex=previousSelected;
					Invalidate();
				}
			}
		}


	}

	///<summary></summary>
	public struct OutlookButton{//this should be a class if I was going to ever reuse this control
		//private string caption;
		//private int imageIndex;
		///<summary></summary>
		public OutlookButton(string caption,int imageIndex){
			Caption=caption;
			ImageIndex=imageIndex;
			Bounds=new Rectangle(0,0,0,0);
			Visible=true;
		}

		///<summary></summary>
		public string Caption;
		///<summary></summary>
		public int ImageIndex;
		///<summary></summary>
		public Rectangle Bounds;
		///<summary></summary>
		public bool Visible;

	}

	///<summary></summary>
	public class ButtonClicked_EventArgs{
		private OutlookButton outlookButton;
		private bool cancel;

		///<summary></summary>
		public ButtonClicked_EventArgs(OutlookButton myButton,bool myCancel){
			outlookButton=myButton;
		}

		///<summary></summary>
		public OutlookButton OutlookButton{
			get{
				return outlookButton;
			}
		}

		///<summary>Set true to cancel the event.</summary>
		public bool Cancel{
			get{
				return cancel;
			}
			set{
				cancel=value;
			}
		}

	}

}







