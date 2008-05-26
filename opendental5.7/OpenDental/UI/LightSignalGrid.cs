using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.UI {
	///<summary></summary>
	public delegate void ODLightSignalGridClickEventHandler(object sender,ODLightSignalGridClickEventArgs e);

	public partial class LightSignalGrid:Control {
		//private bool IsUpdating;
		//<summary>collection of SignalButtonStates</summary>
		private SignalButtonState[] sigButStates;
		private int buttonH;
		private bool mouseIsDown;
		private int hotButton;
		///<summary></summary>
		[Category("Action"),Description("Occurs when a button is clicked.")]
		public event ODLightSignalGridClickEventHandler ButtonClick=null;

		public LightSignalGrid() {
			InitializeComponent();
			buttonH=25;
			hotButton=-1;
		}

		#region Properties
		protected override Size DefaultSize {
			get{
				return new Size(50,300);
			}
		}
		#endregion Properties

		#region Painting
		///<summary></summary>
		protected override void OnPaintBackground(PaintEventArgs pea) {
			//base.OnPaintBackground (pea);
			//don't paint background.  This reduces flickering when using double buffering.
		}

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs e) {
			if(Height==0 || sigButStates==null){
				e.Graphics.FillRectangle(new SolidBrush(Color.White),0,0,Width,Height);
				base.OnPaint(e);
				return;
			}
			Bitmap doubleBuffer=new Bitmap(Width,Height,e.Graphics);
			Graphics g=Graphics.FromImage(doubleBuffer);
			//button backgrounds
			Color mixedC;
			Color baseC;
			int R;
			int G;
			int B;
			for(int i=0;i<sigButStates.Length;i++){
				baseC=sigButStates[i].CurrentColor;
				switch(sigButStates[i].State) {
					case ToolBarButtonState.Normal://Control is 224,223,227
					case ToolBarButtonState.Pressed:
						g.FillRectangle(new SolidBrush(baseC),0,i*buttonH,Width,buttonH);
						break;
					case ToolBarButtonState.Hover://this is darker
						//if(baseC.ToArgb()==Color.White.ToArgb()){
						//	mixedC=Color.FromArgb(220,220,220);
						//}
						//else{
							R=baseC.R-40;
							if(R<0) {
								R=0;
							}
							G=baseC.G-40;
							if(G<0) {
								G=0;
							}
							B=baseC.B-40;
							if(B<0) {
								B=0;
							}
							mixedC=Color.FromArgb(R,G,B);
						//}
						g.FillRectangle(new SolidBrush(mixedC),0,i*buttonH,Width,buttonH);
						break;
					/*case ToolBarButtonState.Pressed://darker
						if(baseC.ToArgb()==Color.White.ToArgb()){
							mixedC=Color.FromArgb(190,190,190);
						}
						else{
							R=baseC.R+50;
							if(R>255) {
								R=255;
							}
							G=baseC.G+50;
							if(G>255) {
								G=255;
							}
							B=baseC.B+50;
							if(B>255) {
								B=255;
							}
							mixedC=Color.FromArgb(R,G,B);
						}
						g.FillRectangle(new SolidBrush(mixedC),0,i*buttonH,Width,buttonH);
						break;*/
				}
				//g.FillRectangle(new SolidBrush(sigButStates[i].CurrentColor),0,i*buttonH,Width,buttonH);
			}
			//grid
			Pen gridPen=new Pen(Color.DarkGray);
			g.DrawRectangle(gridPen,0,0,Width-1,Height-1);
			for(int i=0;i<sigButStates.Length;i++){
				g.DrawLine(gridPen,0,i*buttonH,Width,i*buttonH);
			}
			//button text
			RectangleF rect;
			StringFormat format=new StringFormat();
			format.Alignment=StringAlignment.Center;
			format.LineAlignment=StringAlignment.Center;
			for(int i=0;i<sigButStates.Length;i++){
				rect=new RectangleF(-2,i*buttonH,Width+4,buttonH);
				g.DrawString(sigButStates[i].Text,Font,Brushes.Black,rect,format);
			}
			e.Graphics.DrawImageUnscaled(doubleBuffer,0,0);
			g.Dispose();
			base.OnPaint(e);
		}

		//private void DrawBackground(Graphics g){
			
		//	g.DrawRectangle()
		//}
		#endregion Painting

		///<summary>This will clear the buttons, reset buttons to the specified list, reset the buttonstates, layout the rows, and invalidate for repaint.</summary>
		public void SetButtons(SigButDef[] butDefs) {
			if(butDefs.Length==0){
				sigButStates=new SignalButtonState[0];
				LayoutButtons();
				Invalidate();
				return;
			}
			//since defs are ordered by buttonIndex, the last def will contain the max number of buttons
			sigButStates=new SignalButtonState[butDefs[butDefs.Length-1].ButtonIndex+1];
			for(int i=0;i<sigButStates.Length;i++){
				sigButStates[i]=new SignalButtonState();
				sigButStates[i].ButDef=SigButDefs.GetByIndex(i,butDefs);//might be null
				if(sigButStates[i].ButDef!=null){
					sigButStates[i].Text=sigButStates[i].ButDef.ButtonText;
				}
				sigButStates[i].CurrentColor=Color.White;
			}
			LayoutButtons();
			Invalidate();
		}

		private void LayoutButtons(){
			if(sigButStates.Length==0){
				Height=0;
				return;
			}
			Height=sigButStates.Length*buttonH+1;
		}

		///<summary>Sets the specified buttonIndex to a color and attaches the signal responsible.  This is also used for the manual ack to increase responsiveness.  buttonIndex is 0-based.</summary>
		public void SetButtonActive(int buttonIndex,Color color,Signal activeSignal){
			if(buttonIndex>=sigButStates.Length){
				return;//no button to light up.
			}
			sigButStates[buttonIndex].CurrentColor=color;
			if(activeSignal==null){
				sigButStates[buttonIndex].ActiveSignal=null;
			}
			else{
				sigButStates[buttonIndex].ActiveSignal=activeSignal.Copy();
			}
			Invalidate();
		}

		///<summary>An ack coming from the database.  If it applies to any lights currently showing, then those lights will be turned off.  Returns the 0-based index of the light acked, or -1.</summary>
		public int ProcessAck(int signalNum){
			for(int i=0;i<sigButStates.Length;i++){
				if(sigButStates[i].ActiveSignal==null){
					continue;
				}
				if(sigButStates[i].ActiveSignal.SignalNum==signalNum){
					sigButStates[i].CurrentColor=Color.White;
					sigButStates[i].ActiveSignal=null;
					Invalidate();
					return i;
				}
			}
			return -1;
		}

		///<summary>This should only happen when mouse enters. Only causes a repaint if needed.</summary>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e) {
			base.OnMouseMove(e);
			if(mouseIsDown) {
				//regardless of whether a button is hot, nothing changes until the mouse is released.
				//a hot(pressed) button remains so, and no buttons are hot when hover
				//,so do nothing
			}
			else {//mouse is not down
				int button=HitTest(e.X,e.Y);//this is the button the mouse is over at the moment.
				//first handle the old hotbutton
				if(hotButton!=-1) {//if there is a previous hotbutton
					if(hotButton!=button) {//if we have moved to hover over a new button, or to hover over nothing
						sigButStates[hotButton].State=ToolBarButtonState.Normal;
						Invalidate();//hotButton.Bounds);
					}
				}
				//then, the new button
				if(button!=-1) {
					if(hotButton!=button) {//if we have moved to hover over a new button
						//toolTip1.SetToolTip(this,button.ToolTipText);
						sigButStates[button].State=ToolBarButtonState.Hover;
						Invalidate();//button.Bounds);
					}
					else {//Still hovering over the same button as before
						//do nothing.
					}
				}
				else {
					//toolTip1.SetToolTip(this,"");
				}
				hotButton=button;//this might be -1 if hovering over nothing.
				//if there was no previous hotbutton, and there is not current hotbutton, then do nothing.
			}
		}

		///<summary>Returns the 0-based button index that contains these coordinates, or -1 if no hit.</summary>
		private int HitTest(int x,int y) {
			for(int i=0;i<20;i++){
				if(y<buttonH*i){
					continue;
				}
				if(y>buttonH*(i+1)){
					continue;
				}
				if(i>sigButStates.Length-1){//button not visible
					return -1;
				}
				return i;
			}
			return -1;
		}

		///<summary>Resets button appearance. This will also deactivate the button if it has been pressed but not released. A pressed button will still be hot, however, so that if the mouse enters again, it will behave properly.  Repaints only if necessary.</summary>
		protected override void OnMouseLeave(System.EventArgs e) {
			base.OnMouseLeave(e);
			if(mouseIsDown) {//mouse is down
				//if a button is hot, it will remain so, even if leave.  As long as mouse is down.
				//,so do nothing.
				//Also, if a button is not hot, nothing will happen when leave
				//,so do nothing.
			}
			else {//mouse is not down
				if(hotButton!=-1) {//if there was a previous hotButton
					sigButStates[hotButton].State=ToolBarButtonState.Normal;
					Invalidate();//hotButton.Bounds);
					hotButton=-1;
				}
			}
		}

		///<summary>Change the button to a pressed state.</summary>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e) {
			base.OnMouseDown(e);
			if((e.Button & MouseButtons.Left)!=MouseButtons.Left) {
				return;
			}
			mouseIsDown=true;
			int button=HitTest(e.X,e.Y);
			if(button==-1) {//if there is no current hover button
				return;//don't set a hotButton
			}
			//if(!button.Enabled){
			//	return;//disabled buttons don't respond
			//}
			hotButton=button;
			//if(button.Style==ODToolBarButtonStyle.DropDownButton
			//	&& HitTestDrop(button,e.X,e.Y)) {
			//	button.State=ToolBarButtonState.DropPressed;
			//}
			//else {
				sigButStates[button].State=ToolBarButtonState.Pressed;
			//}
			Invalidate();//button.Bounds);
		}

		///<summary>Change button to hover state and repaint if needed.</summary>
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e) {
			base.OnMouseUp(e);
			if((e.Button & MouseButtons.Left)!=MouseButtons.Left) {
				return;
			}
			mouseIsDown=false;
			int button=HitTest(e.X,e.Y);
			if(hotButton==-1) {//if there was not a previous hotButton
				//do nothing
			}
			else {//there was a hotButton
				sigButStates[hotButton].State=ToolBarButtonState.Normal;
				//but can't set it null yet, because still need it for testing
				Invalidate();//hotButton.Bounds);//invalidate the old button
				//CLICK: 
				if(hotButton==button) {//if mouse was released over the same button as it was depressed
					OnButtonClicked(button,sigButStates[button].ButDef,sigButStates[button].ActiveSignal);
					return;//the button will not revert back to hover
				}//end of click section
				else {//there was a hot button, but it did not turn into a click
					hotButton=-1;
				}
			}
			if(button!=-1) {//no click, and now there is a hover button, not the same as original button.
				//this section could easily be deleted, since all the user has to do is move the mouse slightly.
				sigButStates[button].State=ToolBarButtonState.Hover;
				hotButton=button;//set the current hover button to be the new hotbutton
				Invalidate();//button.Bounds);
			}
		}

		///<summary></summary>
		protected void OnButtonClicked(int myButton,SigButDef myDef,Signal mySignal) {
			ODLightSignalGridClickEventArgs bArgs=new ODLightSignalGridClickEventArgs(myButton,myDef,mySignal);
			if(ButtonClick!=null)
				ButtonClick(this,bArgs);
		}



	}

	///<summary></summary>
	public class SignalButtonState{
		///<summary>This is also present in the def, but this makes it easier to access.</summary>
		public string Text;
		///<summary>The def assigned to this index.</summary>
		public SigButDef ButDef;
		///<summary></summary>
		public Color CurrentColor;
		///<summary></summary>
		public ToolBarButtonState State;
		///<summary>If this button is lit up, then this will contain the signal that caused it.  That way, when user clicks on the button to ack, the signal object in the db can be ack'd properly.</summary>
		public Signal ActiveSignal;
	}

	///<summary></summary>
	public class ODLightSignalGridClickEventArgs {
		private int buttonIndex;
		private SigButDef buttonDef;
		private Signal activeSignal;

		/// <summary></summary>
		/// <param name="myButton"></param>
		public ODLightSignalGridClickEventArgs(int myButton,SigButDef myDef,Signal mySignal) {
			buttonIndex=myButton;
			buttonDef=myDef;
			activeSignal=mySignal;
		}

		///<summary>Remember that this is the 0-based index, but the database uses 1-based.</summary>
		public int ButtonIndex {
			get {
				return buttonIndex;
			}
		}

		///<summary></summary>
		public SigButDef ButtonDef {
			get {
				return buttonDef;
			}
		}

		///<summary></summary>
		public Signal ActiveSignal{
			get {
				return activeSignal;
			}
		}

	}
}
