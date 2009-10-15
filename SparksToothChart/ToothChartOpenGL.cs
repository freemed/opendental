/*=============================================================================================================
Copyright (C) 2006  Jordan Sparks, DMD.  http://www.open-dent.com,  http://www.docsparks.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU Db Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU Db Public License
for more details, available at http://www.opensource.org/licenses/gpl-license.php

Any changes to this program must follow the guidelines of the GPL license if a modified version is to be
redistributed.
===============================================================================================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CodeBase;
using Tao.OpenGl;
using Tao.Platform.Windows;
using OpenDentBusiness;

namespace SparksToothChart {
	public partial class ToothChartOpenGL:CodeBase.OpenGLWinFormsControl{//.SimpleOpenGlControl {
		///<summary>A strongly typed collection of ToothGraphics.  This includes all 32 perm and all 20 primary teeth, whether they will be drawn or not.  If a tooth is missing, it gets marked as visible false.  If it's set to primary, then the permanent tooth gets repositioned under the primary, and a primary gets set to visible true.  If a tooth is impacted, it gets repositioned.  Supernumerary graphics are not yet supported, but they might be handled by adding to this list.  "implant" is also stored as another tooth in this collection.  It is just used to store the graphics for any implant.</summary>
		private ToothGraphicCollection ListToothGraphics;
		float[] specular_color_normal;//white
		float[] specular_color_cementum;//gray
		float[] shininess;
		///<summary>The width that the scene covers.  This is in mm in terms of the scene itself.  This is the only number required in order for this control to be any size.  The height of the viewport is calculated in terms of the same ratio as the width, with the resulting image being centered vertically, so no distortion.</summary>
		private double WidthProjection;
		///<summary>valid values are "1" to "32", and "A" to "Z"</summary>
		private string[] selectedTeeth;
		///<summary>valid values are 1 to 32 (int)</summary>
		private ArrayList ALSelectedTeeth;
		private Color colorBackground;
		///<summary></summary>
		public Color ColorText;
		///<summary></summary>
		public Color ColorTextHighlight;
		///<summary></summary>
		public Color ColorBackHighlight;		
		private bool MouseIsDown;
		///<summary>Mouse move causes this variable to be updated with the current tooth that the mouse is hovering over.</summary>
		private int hotTooth;
		///<summary>The previous hotTooth.  If this is different than hotTooth, then mouse has just now moved to a new tooth.  Can be 0 to represent no previous.</summary>
		private int hotToothOld;
		private int fontOffset;
		private int displayListOffset;
		private string[][] fontsymbols;
		///<summary>This gets set to true during certain operations where we do not need to redraw all the teeth.  Specifically, during tooth selection where only the color of the tooth number text needs to change.  In this case, the rest of the scene will not be rendered again.</summary>
		private bool suspendRendering;
		private int selectedPixelFormat;
		private List<ToothInitial> DrawingSegmentList;
		public CursorTool CursorTool;
		///<summary>A list of points for a line currently being drawn.  Once the mouse is raised, this list gets cleared.</summary>
		private List<Point> PointList;
		///<summary></summary>
		[Category("Action"),Description("Occurs when the mouse goes up ending a drawing segment.")]
		public event ToothChartDrawEventHandler SegmentDrawn=null;
		public Color DrawingColor;
		///<summary>When the drawing feature was originally added, this was the size of the tooth chart.  This number must forever be preserved and drawings scaled to account for it.</summary>
		private Size originalDrawingSize=new Size(410,307);


		///<summary>Specify the hardware mode to create the tooth chart with. Set hardwareMode=true to try for hardware accelerated graphics, and set hardwareMode=false to try and get software graphics.</summary>
		public ToothChartOpenGL(bool hardwareMode,int preferredPixelFormatNum) {
			usehardware=hardwareMode;
			InitializeComponent();
			this.TaoSetupContext += new System.EventHandler(ToothChart_TaoSetupContext);
			this.TaoRenderScene += new System.EventHandler(ToothChart_TaoRenderScene);
			selectedPixelFormat=TaoInitializeContexts(preferredPixelFormatNum);
			TaoRenderEnabled=true;
			WidthProjection=130;
			ListToothGraphics=new ToothGraphicCollection();
			ALSelectedTeeth=new ArrayList();
			DrawingSegmentList=new List<ToothInitial>();
			DrawingColor=Color.Black;
			PointList=new List<Point>();
			//set default colors
			colorBackground=Color.FromArgb(150,145,153);//95,95,130);
			ColorText=Color.White;
			ColorTextHighlight=Color.Black;//.Purple;
			ColorBackHighlight=Color.White;//.Orange;
			Gl.glDisable(Gl.GL_TEXTURE);//Disable texturing, since we don't use it.
																	//This should prevent a glCopyPixels() problem in
																	//Gdi.SwapBuffersFast() on ATI graphics cards.
			ResetTeeth();
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
		}

		private void ToothChart_TaoSetupContext(object sender, System.EventArgs e){//event from base class when context needs to be setup.
			MakeRasterFont();
		}

		///<summary>Valid values are 1-32 and A-Z.</summary>
		public string[] SelectedTeeth{
			get{
				return selectedTeeth;
			}
		}

		///<summary></summary>
		public Color ColorBackground {
			get {
				return colorBackground;
			}
			set {
				colorBackground=value;
				Gl.glClearColor((float)ColorBackground.R/255f,(float)ColorBackground.G/255f,(float)ColorBackground.B/255f,0f);
				Invalidate();
			}
		}

		public int SelectedPixelFormatNumber {
				get{
						return selectedPixelFormat;
				}
		}

		#region Public Methods

		///<summary>If ListToothGraphics is empty, then this fills it, including the complex process of loading all drawing points from local resources.  Or if not empty, then this resets all 32+20 teeth to default postitions, no restorations, etc. Primary teeth set to visible false.  Also clears selected.  Should surround with SuspendLayout / ResumeLayout.</summary>
		public void ResetTeeth() {
			if(ListToothGraphics.Count==0) {//so this will only happen once when program first loads.
				ListToothGraphics.Clear();
				ToothGraphic tooth;
				for(int i=1;i<=32;i++) {
					tooth=new ToothGraphic(i.ToString());
					tooth.Visible=true;
					ListToothGraphics.Add(tooth);
					//primary
					if(ToothGraphic.PermToPri(i.ToString())!="") {
						tooth=new ToothGraphic(ToothGraphic.PermToPri(i.ToString()));
						tooth.Visible=false;
						ListToothGraphics.Add(tooth);
					}
				}
				tooth=new ToothGraphic("implant");
				ListToothGraphics.Add(tooth);
				MakeDisplayLists();
			}
			else {//list was already initially filled, but now user needs to reset it.
				for(int i=0;i<ListToothGraphics.Count;i++) {//loop through all perm and pri teeth.
					ListToothGraphics[i].Reset();
				}
			}
			ALSelectedTeeth.Clear();
			selectedTeeth=new string[0];
			PointList=new List<Point>();
			DrawingSegmentList=new List<ToothInitial>();
			this.Invalidate();
		}

		///<summary>Moves position of tooth.  Rotations first in order listed, then translations.  Tooth doesn't get moved immediately, just when painting.  All changes are cumulative and are in addition to any previous translations and rotations.  So, for instance, if tooth has already been shifted as part of SetToPrimary, then this will move it more.</summary>
		public void MoveTooth(string toothID,float rotate,float tipM,float tipB,float shiftM,float shiftO,float shiftB) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].ShiftM+=shiftM;
			ListToothGraphics[toothID].ShiftO+=shiftO;
			ListToothGraphics[toothID].ShiftB+=shiftB;
			ListToothGraphics[toothID].Rotate+=rotate;
			ListToothGraphics[toothID].TipM+=tipM;
			ListToothGraphics[toothID].TipB+=tipB;
			this.Invalidate();
		}

		///<summary>Sets the specified permanent tooth to primary as follows: Sets ShowPrimary to true for the perm tooth.  Makes pri tooth visible=true, repositions perm tooth by translating -Y.  Moves primary tooth slightly to M or D sometimes for better alignment.  And if 2nd primary molar, then because of the larger size, it must move all perm molars to distal.  Ignores if invalid perm tooth.</summary>
		public void SetToPrimary(string toothID) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			if(ToothGraphic.IsPrimary(toothID)){
				return;
			}
			ListToothGraphics[toothID].ShowPrimary=true;
			ListToothGraphics[toothID].ShiftO-=12;
			this.Invalidate();
			if(!ToothGraphic.IsValidToothID(ToothGraphic.PermToPri(toothID))) {
				return;
			}
			ListToothGraphics[ToothGraphic.PermToPri(toothID)].Visible=true;
			//first pri mand molars, shift slightly to M
			if(toothID=="21") {
				ListToothGraphics["J"].ShiftM+=0.5f;
			}
			if(toothID=="28") {
				ListToothGraphics["S"].ShiftM+=0.5f;
			}
			//second pri molars are huge, so shift distally for space
			//and move all the perm molars distally too
			if(toothID=="4") {
				ListToothGraphics["A"].ShiftM-=0.5f;
				ListToothGraphics["1"].ShiftM-=1;
				ListToothGraphics["2"].ShiftM-=1;
				ListToothGraphics["3"].ShiftM-=1;
			}
			if(toothID=="13") {
				ListToothGraphics["J"].ShiftM-=0.5f;
				ListToothGraphics["14"].ShiftM-=1;
				ListToothGraphics["15"].ShiftM-=1;
				ListToothGraphics["16"].ShiftM-=1;
			}
			if(toothID=="20") {
				ListToothGraphics["K"].ShiftM-=1.2f;
				ListToothGraphics["17"].ShiftM-=2.3f;
				ListToothGraphics["18"].ShiftM-=2.3f;
				ListToothGraphics["19"].ShiftM-=2.3f;
			}
			if(toothID=="29") {
				ListToothGraphics["T"].ShiftM-=1.2f;
				ListToothGraphics["30"].ShiftM-=2.3f;
				ListToothGraphics["31"].ShiftM-=2.3f;
				ListToothGraphics["32"].ShiftM-=2.3f;
			}
		}

		///<summary>This is used for crowns and for retainers.  Crowns will be visible on missing teeth with implants.  Crowns are visible on F and O views, unlike ponics which are only visible on F view.  If the tooth is not visible, that should be set before this call, because then, this will set the root invisible.</summary>
		public void SetCrown(string toothID,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].IsCrown=true;
			if(!ListToothGraphics[toothID].Visible) {//tooth not visible, so set root invisible.
				ListToothGraphics[toothID].SetGroupVisibility(ToothGroupType.Cementum,false);
			}
			ListToothGraphics[toothID].SetSurfaceColors("MODBLFIV",color);
			ListToothGraphics[toothID].SetGroupColor(ToothGroupType.Enamel,color);
			this.Invalidate();
		}

		///<summary></summary>
		public void SetSurfaceColors(string toothID,string surfaces,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].SetSurfaceColors(surfaces,color);
			this.Invalidate();
		}

		///<summary>Used for missing teeth.  This should always be done before setting restorations, because a pontic will cause the tooth to become visible again except for the root.  So if setInvisible after a pontic, then the pontic can't show.</summary>
		public void SetInvisible(string toothID) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].Visible=false;
			this.Invalidate();
		}

		///<summary>This is just the same as SetInvisible, except that it also hides the number from showing.  This is used, for example, if premolars are missing, and ortho has completely closed the space.  User will not be able to select this tooth because the number is hidden.</summary>
		public void HideTooth(string toothID) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].Visible=false;
			ListToothGraphics[toothID].HideNumber=true;
			this.Invalidate();
		}

		///<summary>This is used for any pontic, including bridges, full dentures, and partials.  It is usually used on a tooth that has already been set invisible.  This routine sets the tooth to visible again, but makes the root invisible.  Then, it sets the entire crown to the specified color.  If the tooth was not initially invisible, then it does not set the root invisible.  Any connector bars for bridges are set separately.</summary>
		public void SetPontic(string toothID,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].IsPontic=true;
			if(!ListToothGraphics[toothID].Visible){//tooth not visible, so set root invisible.
				ListToothGraphics[toothID].SetGroupVisibility(ToothGroupType.Cementum,false);
			}
			ListToothGraphics[toothID].SetSurfaceColors("MODBLFIV",color);
			ListToothGraphics[toothID].SetGroupColor(ToothGroupType.Enamel,color);
		}

		///<summary>Root canals are initially not visible.  This routine sets the canals visible, changes the color to the one specified, and also sets the cementum for the tooth to be semitransparent so that the canals can be seen.  Also sets the IsRCT flag for the tooth to true.</summary>
		public void SetRCT(string toothID,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].IsRCT=true;
			ListToothGraphics[toothID].colorRCT=color;
		}

		///<summary>This draws a big red extraction X right on top of the tooth.  It's up to the calling application to figure out when it's appropriate to do this.  Even if the tooth has been marked invisible, there's a good chance that this will still get drawn because a tooth can be set visible again for the drawing the pontic.  So the calling application needs to figure out when it's appropriate to draw the X, and not set this otherwise.</summary>
		public void SetBigX(string toothID,Color color){
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].DrawBigX=true;
			ListToothGraphics[toothID].colorX=color;
		}

		///<summary>Set this tooth to show a BU or post.</summary>
		public void SetBU(string toothID,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].IsBU=true;
			ListToothGraphics[toothID].colorBU=color;
		}

		///<summary>Set this tooth to show an implant</summary>
		public void SetImplant(string toothID,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].IsImplant=true;
			ListToothGraphics[toothID].colorImplant=color;
		}

		///<summary>Set this tooth to show a sealant</summary>
		public void SetSealant(string toothID,Color color) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			ListToothGraphics[toothID].IsSealant=true;
			ListToothGraphics[toothID].colorSealant=color;
		}

		///<summary></summary>
		public void AddDrawingSegment(ToothInitial drawingSegment) {
			bool alreadyAdded=false;
			for(int i=0;i<DrawingSegmentList.Count;i++){
				if(DrawingSegmentList[i].DrawingSegment==drawingSegment.DrawingSegment){
					alreadyAdded=true;
					break;
				}
			}
			if(!alreadyAdded){
				DrawingSegmentList.Add(drawingSegment);
			}
		}

		///<summary>Returns a bitmap of what is showing in the control.  Used for printing.</summary>
		public Bitmap GetBitmap(){
			Gl.glFlush();
			Bitmap bitmap=new Bitmap(this.Width,this.Height);
			Graphics g=Graphics.FromImage(bitmap);
			Point screenLoc=PointToScreen(Location);
			g.CopyFromScreen(screenLoc.X,screenLoc.Y,0,0,new Size(Width,Height));
			g.Dispose();
			return bitmap;
		}

		#endregion Public Methods

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
		}

		private void ToothChart_TaoRenderScene(object sender, System.EventArgs e){
			if(suspendRendering){
				return;
			}
			//This first part was originally in setup context
			Gl.glClearColor((float)ColorBackground.R/255f,(float)ColorBackground.G/255f,(float)ColorBackground.B/255f,0f);
			Gl.glClearAccum(0f,0f,0f,0f);
			//Lighting
			float ambI=.2f;
			float difI=.6f;
			float specI=1f;
			float[] light_ambient = new float[] { ambI,ambI,ambI,1f };//RGB,A=1 for no transparency. Default 0001
			float[] light_diffuse = new float[] { difI,difI,difI,1f };//RGBA. Default 1111. 'typical' 
			float[] light_specular = new float[] { specI,specI,specI,1f };//RGBA. Default 1111
			float[] light_position = new float[] { -0.5f,0.1f,1f,0f };//xyz(direction, not position), w=0 for infinite
			Gl.glLightfv(Gl.GL_LIGHT0,Gl.GL_AMBIENT,light_ambient);
			Gl.glLightfv(Gl.GL_LIGHT0,Gl.GL_DIFFUSE,light_diffuse);
			Gl.glLightfv(Gl.GL_LIGHT0,Gl.GL_SPECULAR,light_specular);
			Gl.glLightfv(Gl.GL_LIGHT0,Gl.GL_POSITION,light_position);
			//Materials
			Gl.glShadeModel(Gl.GL_SMOOTH);
			//OK to just set these three once.
			specular_color_normal = new float[] { 1.0f,1.0f,1.0f,1.0f };//1111 for white. RGBA
			specular_color_cementum = new float[] { 0.1f,0.1f,0.1f,1.0f };//gray
			shininess = new float[] { 90f };//0 to 128. Size of specular reflection. 128 smallest
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT0);
			//Render Scene starts here----------------------------------------------------------------------------------
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);//Clears the color buffer and depth buffer.
			//viewing transformation.  gluLookAt is too complex, so not used
			//default was Z=1, looking towards the origin
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();//clears the matrix
			Gl.glMatrixMode(Gl.GL_PROJECTION);//only the projection matrix will be affected.
			Gl.glLoadIdentity();
			double HeightProjection=WidthProjection*this.Height/this.Width;
			Gl.glOrtho(-WidthProjection/2,WidthProjection/2,//orthographic projection. L,R
				-HeightProjection/2,HeightProjection/2,//Bot,Top
				-WidthProjection/2,WidthProjection/2);//Near,Far
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			//viewport transformation not used. Default is to fill entire control.
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glEnable(Gl.GL_LINE_SMOOTH);
			Gl.glHint(Gl.GL_LINE_SMOOTH_HINT,Gl.GL_DONT_CARE);
			DrawScene();
			//jitter code for antialias starts here, but I can't get it to work:
			//Gl.glFlush();//handled for me in base class
		}

		private void DrawScene() {
			for(int t=0;t<ListToothGraphics.Count;t++) {//loop through each tooth
				if(ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				DrawFacialView(ListToothGraphics[t]);
				DrawOcclusalView(ListToothGraphics[t]);
			}
			DrawTextAndLines();
			DrawDrawingSegments();
			Gl.glFlush();
		}

		private void DrawFacialView(ToothGraphic toothGraphic) {
			Gl.glPushMatrix();//remember position of origin
			Gl.glTranslatef(GetTransX(toothGraphic.ToothID),//Move the tooth to the correct position for facial view
				GetTransYfacial(toothGraphic.ToothID),
				0);
			RotateAndTranslateUser(toothGraphic);
			if(toothGraphic.Visible
				|| (toothGraphic.IsCrown && toothGraphic.IsImplant)
				|| toothGraphic.IsPontic)
			{
				DrawTooth(toothGraphic);
			}
			Gl.glDisable(Gl.GL_DEPTH_TEST);
			if(toothGraphic.DrawBigX) {
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glEnable(Gl.GL_BLEND);
				//move the bigX 6mm to the Facial so it will paint in front of the tooth
				Gl.glTranslatef(0,0,6f);
				Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
				Gl.glLineWidth((float)Width/275f);//1.5f);//thickness of line depends on size of window
				Gl.glColor3f (
					(float)toothGraphic.colorX.R/255f,
					(float)toothGraphic.colorX.G/255f,
					(float)toothGraphic.colorX.B/255f);
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)){
					Gl.glBegin(Gl.GL_LINES);
					Gl.glVertex2f(-2f,12f);
					Gl.glVertex2f(2f,-6f);
					Gl.glEnd();
					Gl.glBegin(Gl.GL_LINES);
					Gl.glVertex2f(2f,12f);
					Gl.glVertex2f(-2f,-6f);
					Gl.glEnd();
				}
				else{
					Gl.glBegin(Gl.GL_LINES);
					Gl.glVertex2f(-2f,6f);
					Gl.glVertex2f(2f,-12f);
					Gl.glEnd();
					Gl.glBegin(Gl.GL_LINES);
					Gl.glVertex2f(2f,6f);
					Gl.glVertex2f(-2f,-12f);
					Gl.glEnd();
				}
			}
			Gl.glPopMatrix();//reset to origin
			if(toothGraphic.Visible && toothGraphic.IsRCT) {//draw RCT
				Gl.glPushMatrix();
				Gl.glTranslatef(0,0,10f);//move RCT forward 10mm so it will be visible.
				Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glEnable(Gl.GL_BLEND);
				Gl.glColor3f(
					(float)toothGraphic.colorRCT.R/255f,
					(float)toothGraphic.colorRCT.G/255f,
					(float)toothGraphic.colorRCT.B/255f);
					//.5f);//only 1/2 darkness
				Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
				Gl.glLineWidth((float)Width/225f);
				Gl.glPointSize((float)Width/275f);//point is slightly smaller since no antialiasing
				RotateAndTranslateUser(toothGraphic);
				List<Line> lines=toothGraphic.GetRctLines();
				for(int i=0;i<lines.Count;i++){
					Gl.glBegin(Gl.GL_LINE_STRIP);
					for(int j=0;j<lines[i].Vertices.Count;j++){
						Gl.glVertex3f(lines[i].Vertices[j].X,lines[i].Vertices[j].Y,lines[i].Vertices[j].Z);
					}
					Gl.glEnd();
				}
				Gl.glPopMatrix();
				//This section is a necessary workaround for OpenGL.
				//It draws a point at each intersection to hide the unsightly transitions between line segments.
				Gl.glPushMatrix();
				Gl.glTranslatef(0,0,10.5f);//move forward 10.5mm so it will cover the lines
				Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
				RotateAndTranslateUser(toothGraphic);
				Gl.glDisable(Gl.GL_BLEND);
				for(int i=0;i<lines.Count;i++){
					Gl.glBegin(Gl.GL_POINTS);
					for(int j=0;j<lines[i].Vertices.Count;j++) {
						//but ignore the first and last.  We are only concerned with where lines meet.
						if(j==0 || j==lines[i].Vertices.Count-1) {
							continue;
						}
						Gl.glVertex3f(lines[i].Vertices[j].X,lines[i].Vertices[j].Y,lines[i].Vertices[j].Z);
					}
					Gl.glEnd();
				}
				Gl.glPopMatrix();
			}
			if(toothGraphic.Visible && toothGraphic.IsBU) {//BU or Post
				Gl.glPushMatrix();
				Gl.glTranslatef(0,0,13f);//move BU forward 13mm so it will be visible.
				Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glDisable(Gl.GL_BLEND);
				Gl.glColor3f(
					(float)toothGraphic.colorBU.R/255f,
					(float)toothGraphic.colorBU.G/255f,
					(float)toothGraphic.colorBU.B/255f);
				RotateAndTranslateUser(toothGraphic);
				Polygon poly=toothGraphic.GetBUpoly();
				Gl.glBegin(Gl.GL_POLYGON);
				for(int i=0;i<poly.Vertices.Count;i++) {
					Gl.glVertex3f(poly.Vertices[i].X,poly.Vertices[i].Y,poly.Vertices[i].Z);
				}
				Gl.glEnd();
				Gl.glPopMatrix();
			}
			if(toothGraphic.IsImplant){
				Gl.glPushMatrix();
				Gl.glTranslatef(GetTransX(toothGraphic.ToothID),//Move the tooth to the correct position for facial view
					GetTransYfacial(toothGraphic.ToothID),
					0);
				RotateAndTranslateUser(toothGraphic);
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
					//flip the implant upside down
					Gl.glRotatef(180f,0,0,1f);
				}
				Gl.glEnable(Gl.GL_LIGHTING);
				Gl.glEnable(Gl.GL_BLEND);
				Gl.glEnable(Gl.GL_DEPTH_TEST);
				ToothGroup group=(ToothGroup)ListToothGraphics["implant"].Groups[0];
				float[] material_color=new float[] {
					(float)toothGraphic.colorImplant.R/255f,
					(float)toothGraphic.colorImplant.G/255f,
					(float)toothGraphic.colorImplant.B/255f,
					(float)toothGraphic.colorImplant.A/255f
				};//RGBA
				Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SPECULAR,specular_color_normal);
				Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SHININESS,shininess);
				Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_AMBIENT_AND_DIFFUSE,material_color);
				Gl.glBlendFunc(Gl.GL_ONE,Gl.GL_ZERO);
				Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT,Gl.GL_NICEST);
				for(int i=0;i<group.Faces.Count;i++){//  .GetLength(0);i++) {//loop through each face
					Gl.glBegin(Gl.GL_POLYGON);
					for(int j=0;j<group.Faces[i].IndexList.Count;j++){//.Length;j++) {//loop through each vertex
						//The index for both will always be the same because we enforce a 1:1 relationship.
						//We show grabbing a float[3], but we could just as easily use the index itself.
						Gl.glVertex3fv(ListToothGraphics["implant"].VertexNormals[group.Faces[i].IndexList[j]].Vertex.GetFloatArray());//Vertices[group.Faces[i][j][0]]);
						Gl.glNormal3fv(ListToothGraphics["implant"].VertexNormals[group.Faces[i].IndexList[j]].Normal.GetFloatArray()); //.Normals[group.Faces[i][j][1]]);
					}
					Gl.glEnd();
				}
				Gl.glPopMatrix();
			}
		}

		private void DrawOcclusalView(ToothGraphic toothGraphic) {
			//now the occlusal surface. Notice that it's relative to origin again
			Gl.glPushMatrix();//remember position of origin
			Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYocclusal(toothGraphic.ToothID),0);
			if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
				Gl.glRotatef(-110f,1f,0,0);//rotate angle about line from origin to x,y,z
			}
			else {//mandibular
				if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
					Gl.glRotatef(110f,1f,0,0);
				}
				else {
					Gl.glRotatef(120f,1f,0,0);
				}
			}
			RotateAndTranslateUser(toothGraphic);
			if(toothGraphic.Visible//might not be visible if an implant
				|| (toothGraphic.IsCrown && toothGraphic.IsImplant))//a crown on an implant will paint
				//pontics won't paint, because tooth is invisible
			{
				DrawTooth(toothGraphic);
			}
			Gl.glPopMatrix();//reset to origin
			if(toothGraphic.Visible && 
				toothGraphic.IsSealant){//draw sealant
				Gl.glPushMatrix();
				Gl.glTranslatef(0,0,6f);//move forward 6mm so it will be visible.
				Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYocclusal(toothGraphic.ToothID),0);
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
					Gl.glRotatef(-110f,1f,0,0);//rotate angle about line from origin to x,y,z
				}
				else {//mandibular
					if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
						Gl.glRotatef(110f,1f,0,0);
					}
					else {
						Gl.glRotatef(120f,1f,0,0);
					}
				}
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glEnable(Gl.GL_BLEND);
				Gl.glColor3f(
					(float)toothGraphic.colorSealant.R/255f,
					(float)toothGraphic.colorSealant.G/255f,
					(float)toothGraphic.colorSealant.B/255f);
				//.5f);//only 1/2 darkness
				Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
				Gl.glLineWidth((float)Width/225f);
				Gl.glPointSize((float)Width/275f);//point is slightly smaller since no antialiasing
				RotateAndTranslateUser(toothGraphic);
				Line line=toothGraphic.GetSealantLine();
				Gl.glBegin(Gl.GL_LINE_STRIP);
				for(int j=0;j<line.Vertices.Count;j++) {//loop through each vertex
					Gl.glVertex3f(line.Vertices[j].X,line.Vertices[j].Y,line.Vertices[j].Z);
				}
				Gl.glEnd();
				//The next 30 or so lines are all a stupid OpenGL workaround to hide the line intersections with big dots.
				Gl.glPopMatrix();
				//now, draw a point at each intersection to hide the unsightly transitions
				Gl.glPushMatrix();
				//move foward so it will cover the lines
				Gl.glTranslatef(0,0,6.5f);
				Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYocclusal(toothGraphic.ToothID),0);
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
					Gl.glRotatef(-110f,1f,0,0);//rotate angle about line from origin to x,y,z
				}
				else {//mandibular
					if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
						Gl.glRotatef(110f,1f,0,0);
					}
					else {
						Gl.glRotatef(120f,1f,0,0);
					}
				}
				RotateAndTranslateUser(toothGraphic);
				Gl.glDisable(Gl.GL_BLEND);
				Gl.glBegin(Gl.GL_POINTS);
				for(int j=0;j<line.Vertices.Count;j++) {//loop through each vertex
					//but ignore the first and last.  We are only concerned with where lines meet.
					if(j==0 || j==line.Vertices.Count-1) {
						continue;
					}
					Gl.glVertex3f(line.Vertices[j].X,line.Vertices[j].Y,line.Vertices[j].Z);
				}
				Gl.glEnd();
				Gl.glPopMatrix();
			}
		}

		private void DrawTextAndLines() {
			Gl.glPushMatrix();
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDisable(Gl.GL_BLEND);
			Gl.glDisable(Gl.GL_DEPTH_TEST);
			Gl.glColor3f(
				(float)Color.White.R/255f,
				(float)Color.White.G/255f,
				(float)Color.White.B/255f);
			Gl.glLineWidth((float)Width/400f);//about 1
			Gl.glBegin(Gl.GL_LINE_STRIP);
				Gl.glVertex3f(-(float)WidthProjection/2f,0,0);
				Gl.glVertex3f((float)WidthProjection/2f,0,0);
			Gl.glEnd();
			for(int i=1;i<=32;i++){
				if(ALSelectedTeeth.Contains(i)) {
					DrawNumber(i,true,true);
				}
				else {
					DrawNumber(i,false,true);
				}
			}
			Gl.glPopMatrix();
		}

		private void DrawDrawingSegments(){
			Gl.glPushMatrix();
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
			Gl.glDisable(Gl.GL_DEPTH_TEST);
			float lWidth=(float)Width/300f;//about 1.5
			Gl.glLineWidth(lWidth);
			Gl.glPointSize((float)Width/350f);//slightly smaller
			string[] pointStr;
			List<Point> points;
			Point point;
			string[] xy;
			PointF pointMm;
			Color color;
			float scaleDrawing=(float)Width/(float)originalDrawingSize.Width;
			for(int s=0;s<DrawingSegmentList.Count;s++){
				color=DrawingSegmentList[s].ColorDraw;
				Gl.glColor3f(
					(float)color.R/255f,
					(float)color.G/255f,
					(float)color.B/255f);
				pointStr=DrawingSegmentList[s].DrawingSegment.Split(';');
				points=new List<Point>();
				for(int p=0;p<pointStr.Length;p++){
					xy=pointStr[p].Split(',');
					if(xy.Length==2){
						point=new Point((int)(float.Parse(xy[0])*scaleDrawing),(int)(float.Parse(xy[1])*scaleDrawing));
						points.Add(point);
					}
				}
				Gl.glBegin(Gl.GL_LINE_STRIP);
				for(int i=0;i<points.Count;i++){
					//if we set 0,0 to center, then this is where we would convert it back.
					pointMm=PixToMm(new Point(points[i].X,points[i].Y));
					Gl.glVertex3f(pointMm.X,pointMm.Y,0);
				}
				Gl.glEnd();
				//now draw a filled circle at each line strip intersection to make it look nicer
				Gl.glBegin(Gl.GL_POINTS);
				for(int i=0;i<points.Count;i++){
					//but ignore the first and last.  We are only concerned with where lines meet.
					if(i==0 || i==points.Count-1){
						continue;
					}
					pointMm=PixToMm(new Point(points[i].X,points[i].Y));
					Gl.glVertex3f(pointMm.X,pointMm.Y,0);
				}
				Gl.glEnd();
			}
			Gl.glPopMatrix();
		}

		///<summary>Gets the rectangle surrounding a tooth number.  Used to draw the box and to invalidate the area.</summary>
		private RectangleF GetNumberRecMm(string tooth_id){
			float xPos=0;
			float yPos=0;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				yPos+=1.3f;
			}
			else {
				yPos-=3.8f;
			}
			xPos+=GetTransX(tooth_id);
			string displayNum=OpenDentBusiness.Tooth.GetToothLabelGraphic(tooth_id);
			float strWidth=MeasureStringMm(displayNum);
			xPos-=strWidth/2f;
			//only use the ShiftM portion of the user translation
			if(ToothGraphic.IsRight(tooth_id)) {
				xPos+=ListToothGraphics[tooth_id].ShiftM;
			}
			else {
				xPos-=ListToothGraphics[tooth_id].ShiftM;
			}
			float toMm=(float)WidthProjection/(float)Width;//mm/pix
			RectangleF recMm=new RectangleF(xPos-2f*toMm,yPos-2f*toMm,strWidth+3f*toMm,12f*toMm);//this rec has origin at LL
			return recMm;
		}

		///<summary>First, use GetNumberRecMm to get the rectangle surrounding a tooth num.  The, use this to convert it to control coords.</summary>
		private Rectangle ConvertRecToPix(RectangleF recMm){
			float toMm=(float)WidthProjection/(float)Width;//mm/pix
			Rectangle recPix=new Rectangle((int)(Width/2+recMm.X/toMm),(int)(Height/2-recMm.Y/toMm-recMm.Height/toMm),
				(int)(recMm.Width/toMm),(int)(recMm.Height/toMm));
			return recPix;
		}

		///<summary>This also adjusts the result up or down along the Y axis to account for a control that is not the same proportion as the original.</summary>
		private PointF PixToMm(Point pixPoint){
			float toMmRatio=(float)WidthProjection/(float)Width;//mm/pix
			float mmX=(((float)pixPoint.X)*toMmRatio)-((float)WidthProjection)/2f;
			float idealHeightProjection=(float)WidthProjection*(float)originalDrawingSize.Height/(float)originalDrawingSize.Width;
			float actualHeightProjection=(float)WidthProjection*(float)this.Height/(float)this.Width;
			float mmY=(idealHeightProjection)/2f-(((float)pixPoint.Y)*toMmRatio);
			return new PointF(mmX,mmY);
		}

		///<summary>Draws the number and the rectangle behind it.  Draws in the appropriate color</summary>
		private void DrawNumber(int intTooth, bool isSelected, bool isFullRedraw) {
			Gl.glPushMatrix();
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glDisable(Gl.GL_BLEND);
			Gl.glDisable(Gl.GL_DEPTH_TEST);
			string tooth_id=intTooth.ToString();
			if(ToothGraphic.IsValidToothID(ToothGraphic.PermToPri(intTooth.ToString()))//pri is valid
				&& ListToothGraphics[ToothGraphic.PermToPri(intTooth.ToString())].Visible)//and pri visible
			{
				tooth_id=ToothGraphic.PermToPri(intTooth.ToString());
			}
			if(isFullRedraw && ListToothGraphics[tooth_id].HideNumber){//if redrawing all numbers, and this is a "hidden" number
				return;//skip
			}
			string displayNum=OpenDentBusiness.Tooth.GetToothLabelGraphic(tooth_id);
			float toMm=(float)WidthProjection/(float)Width;//mm/pix
			RectangleF recMm=GetNumberRecMm(tooth_id);
			Rectangle recPix=ConvertRecToPix(recMm);
			if(isSelected){
				Gl.glColor3f(
					(float)ColorBackHighlight.R/255f,
					(float)ColorBackHighlight.G/255f,
					(float)ColorBackHighlight.B/255f);
				Gl.glBegin(Gl.GL_QUADS);
					Gl.glVertex3f(recMm.X,recMm.Y,14);//LL
					Gl.glVertex3f(recMm.X,recMm.Y+recMm.Height,14);//UL
					Gl.glVertex3f(recMm.X+recMm.Width,recMm.Y+recMm.Height,14);//UR
					Gl.glVertex3f(recMm.X+recMm.Width,recMm.Y,14);//LR
				Gl.glEnd();
				Gl.glColor3f(
					(float)ColorTextHighlight.R/255f,
					(float)ColorTextHighlight.G/255f,
					(float)ColorTextHighlight.B/255f);
				Gl.glRasterPos3f(recMm.X+2f*toMm,recMm.Y+2f*toMm,15f);
				if(!ListToothGraphics[tooth_id].HideNumber){//Only draw if number is not hidden.
					PrintString(displayNum);
				}
			} 
			else{
				Gl.glColor3f(
					(float)ColorBackground.R/255f,
					(float)ColorBackground.G/255f,
					(float)ColorBackground.B/255f);
				Gl.glBegin(Gl.GL_QUADS);
					Gl.glVertex3f(recMm.X,recMm.Y,14);//LL
					Gl.glVertex3f(recMm.X,recMm.Y+recMm.Height,14);//UL
					Gl.glVertex3f(recMm.X+recMm.Width,recMm.Y+recMm.Height,14);//UR
					Gl.glVertex3f(recMm.X+recMm.Width,recMm.Y,14);//LR
				Gl.glEnd();
				Gl.glColor3f(
					(float)ColorText.R/255f,
					(float)ColorText.G/255f,
					(float)ColorText.B/255f);
				Gl.glRasterPos3f(recMm.X+2f*toMm,recMm.Y+2f*toMm,15f);
				if(!ListToothGraphics[tooth_id].HideNumber){//Only draw if number is not hidden.
					PrintString(displayNum);
				}
			}
			Gl.glPopMatrix();
			Gl.glFlush();
		}

		///<summary>Return value is in tooth coordinates, not pixels.</summary>
		private float MeasureStringPix(string text){
			float retVal=0;
			for(int i=0;i<text.Length;i++){
				if (fontsymbols[(byte)text[i]] != null) retVal+=fontsymbols[(byte)text[i]][0].Length+1;
			}
			return retVal;
		}

		private float MeasureStringMm(string text){
			return MeasureStringPix(text)/(float)Width*(float)WidthProjection;
		} 

		private void MakeRasterFont() {
			fontsymbols = new string[255][];
			fontsymbols['+'] = new string[] { "00000", "00000", "00100", "00100", "11111", "00100", "00100", "00000", "00000" };
			fontsymbols['-'] = new string[] { "00000", "00000", "00000", "00000", "11111", "00000", "00000", "00000", "00000" };
			fontsymbols['A'] = new string[] { "0001000", "0001000", "0010100", "0010100", "0100010", "0100010", "0111110", "1000001", "1000001" };//A
			fontsymbols['B']=new string[] { "11110","10001","10001","10001","11110","10001","10001","10001","11110" };//B
			fontsymbols['C']=new string[] { "011110","100001","100000","100000","100000","100000","100000","100001","011110" };//C
			fontsymbols['D']=new string[] { "111100","100010","100001","100001","100001","100001","100001","100010","111100" };//D
			fontsymbols['E']=new string[] { "11111","10000","10000","10000","11110","10000","10000","10000","11111" };//E
			fontsymbols['F']=new string[] { "11111","10000","10000","10000","11110","10000","10000","10000","10000" };//F
			fontsymbols['G']=new string[] { "011110","100001","100000","100000","100111","100001","100001","100011","011101" };//G
			fontsymbols['H']=new string[] { "100001","100001","100001","100001","111111","100001","100001","100001","100001" };//H
			fontsymbols['I']=new string[] { "111","010","010","010","010","010","010","010","111" };//I
			fontsymbols['J']=new string[] { "00001","00001","00001","00001","00001","00001","10001","10001","01110" };//J
			fontsymbols['K']=new string[] { "100001","100010","100100","101000","110000","101000","100100","100010","100001" };//K
			fontsymbols['L']=new string[] { "10000","10000","10000","10000","10000","10000","10000","10000","11111" };//L
			fontsymbols['M']=new string[] { "1000001","1000001","1100011","1100011","1010101","1010101","1001001","1001001","1000001" };//M
			fontsymbols['N']=new string[] { "100001","110001","110001","101001","101001","100101","100011","100011","100001" };//N
			fontsymbols['O']=new string[] { "011110","100001","100001","100001","100001","100001","100001","100001","011110" };//O
			fontsymbols['P']=new string[] { "111110","100001","100001","100001","111110","100000","100000","100000","100000" };//P
			fontsymbols['Q']=new string[] { "011110","100001","100001","100001","100001","100001","100101","100010","011101" };//Q
			fontsymbols['R']=new string[] { "111110","100001","100001","100001","111110","100100","100010","100010","100001" };//R
			fontsymbols['S']=new string[] { "011110","100001","100000","100000","011110","000001","000001","100001","011110" };//S
			fontsymbols['T']=new string[] { "1111111","0001000","0001000","0001000","0001000","0001000","0001000","0001000","0001000" };//T
			fontsymbols['0'] = new string[] { "01110", "10001", "10001", "10001", "10001", "10001", "10001", "10001", "01110" };//0
			fontsymbols['1'] = new string[] { "0010", "1110", "0010", "0010", "0010", "0010", "0010", "0010", "0010" };//1
			fontsymbols['2'] = new string[] { "01110", "10001", "00001", "00001", "00010", "00100", "01000", "10000", "11111" };//2
			fontsymbols['3'] = new string[] { "01110", "10001", "00001", "00001", "00110", "00001", "00001", "10001", "01110" };//3
			fontsymbols['4'] = new string[] { "00010", "00110", "00110", "01010", "01010", "10010", "11111", "00010", "00010" };//4
			fontsymbols['5'] = new string[] { "11111", "10000", "10000", "11110", "10001", "00001", "00001", "10001", "01110" };//5
			fontsymbols['6'] = new string[] { "01110", "10001", "10000", "10000", "11110", "10001", "10001", "10001", "01110" };//6
			fontsymbols['7'] = new string[] { "11111", "00001", "00010", "00010", "00100", "00100", "01000", "01000", "01000" };//7
			fontsymbols['8'] = new string[] { "01110", "10001", "10001", "10001", "01110", "10001", "10001", "10001", "01110" };//8
			fontsymbols['9'] = new string[] { "01110", "10001", "10001", "10001", "01111", "00001", "00001", "10001", "01110" };//9

			int i;
			byte[] letter;
			Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT,1);
			fontOffset = Gl.glGenLists(128);
			int letterW;
			int letterH;
			string row;
			for(i=0;i<fontsymbols.Length;i++) {
				if (fontsymbols[i]==null) continue;
				letterW=fontsymbols[i][0].Length;
				letterH=fontsymbols[i].Length;
				letter = new byte[letterH];
				for(int h=0;h<letterH;h++) {//actually draws the letter from the bottom up.
					letter[h]=0;
					row=fontsymbols[i][letterH-h-1];
					for(int w=0;w<letterW;w++){
						if(row.Substring(w,1)=="1"){
							letter[h]=(byte)(letter[h] | (byte)Math.Pow(2,7-w));
						}
					}
				}
				Gl.glNewList(fontOffset + i,Gl.GL_COMPILE);
				Gl.glBitmap(letterW,letterH,0,0,letterW+1,0,letter);
				Gl.glEndList();
			}

		}

		private void PrintString(string text) {
			Gl.glListBase(fontOffset);
			byte[] textbytes = new byte[text.Length];
			for(int i = 0;i < text.Length;i++){
				textbytes[i] = (byte)text[i];
			}
			try{
				Gl.glCallLists(text.Length,Gl.GL_UNSIGNED_BYTE,textbytes);
			}
			catch{
				//Do nothing
			}
		}

		///<summary>Performs the rotations and translations entered by user for this tooth.  Usually, all numbers are just 0, resulting in no movement here.</summary>
		private void RotateAndTranslateUser(ToothGraphic toothGraphic) {
			//remembering that they actually show in the opposite order, so:
			//1: translate
			//2: tipM last
			//3: tipB second
			//4: rotate first
			if(ToothGraphic.IsRight(toothGraphic.ToothID)) {
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {//UR
					Gl.glTranslatef(toothGraphic.ShiftM,-toothGraphic.ShiftO,toothGraphic.ShiftB);
					Gl.glRotatef(toothGraphic.TipM,0,0,1f);
					Gl.glRotatef(-toothGraphic.TipB,1f,0,0);
					Gl.glRotatef(toothGraphic.Rotate,0,1f,0);
				}
				else {//LR
					Gl.glTranslatef(toothGraphic.ShiftM,toothGraphic.ShiftO,toothGraphic.ShiftB);
					Gl.glRotatef(-toothGraphic.TipM,0,0,1f);
					Gl.glRotatef(toothGraphic.TipB,1f,0,0);
					Gl.glRotatef(-toothGraphic.Rotate,0,1f,0);
				}
			}
			else {
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {//UL
					Gl.glTranslatef(-toothGraphic.ShiftM,-toothGraphic.ShiftO,toothGraphic.ShiftB);
					Gl.glRotatef(-toothGraphic.TipM,0,0,1f);
					Gl.glRotatef(-toothGraphic.TipB,1f,0,0);
					Gl.glRotatef(toothGraphic.Rotate,0,1f,0);
				}
				else {//LL
					Gl.glTranslatef(-toothGraphic.ShiftM,toothGraphic.ShiftO,toothGraphic.ShiftB);
					Gl.glRotatef(toothGraphic.TipM,0,0,1f);
					Gl.glRotatef(toothGraphic.TipB,1f,0,0);
					Gl.glRotatef(-toothGraphic.Rotate,0,1f,0);
				}
			}
		}

		///<summary></summary>
		private void DrawTooth(ToothGraphic toothGraphic) {
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			ToothGroup group;
			float[] material_color;
			for(int g=0;g<toothGraphic.Groups.Count;g++) {
				group=(ToothGroup)toothGraphic.Groups[g];
				if(!group.Visible) {
					continue;
				}
				//group.PaintColor=Color.FromArgb(255,255,253,209);//temp only for testing
				if(toothGraphic.ShiftO<-10){//if unerupted
					material_color=new float[] {
						(float)group.PaintColor.R/255f/2f,
						(float)group.PaintColor.G/255f/2f,
						(float)group.PaintColor.B/255f/2f,
						(float)group.PaintColor.A/255f/2f
					};
				}
				else{
					material_color=new float[] {
						(float)group.PaintColor.R/255f,
						(float)group.PaintColor.G/255f,
						(float)group.PaintColor.B/255f,
						(float)group.PaintColor.A/255f
					};
				}
				if(group.GroupType==ToothGroupType.Cementum) {
					Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SPECULAR,specular_color_cementum);
				}
				else {
					Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SPECULAR,specular_color_normal);
				}
				Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SHININESS,shininess);
				Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_AMBIENT_AND_DIFFUSE,material_color);
				Gl.glBlendFunc(Gl.GL_ONE,Gl.GL_ZERO);
				Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT,Gl.GL_NICEST);
				Gl.glListBase(displayListOffset);
				//draw the group
				Gl.glCallList(displayListOffset+toothGraphic.GetIndexForDisplayList(group));
			}
		}

		///<summary></summary>
		private void MakeDisplayLists(){
			//total number of display lists will be: (52 teeth) x (10 group types)=520. But 1-9 not used, and 521-529 are used. 
			displayListOffset=Gl.glGenLists(530);//not sure if I did this right
			ToothGraphic toothGraphic;
			ToothGroup group;
			for(int t=1;t<=52;t++) {
				if(t>32 && t<=42){//33-42:  A-J = 4-13
					toothGraphic=ListToothGraphics[Tooth.PermToPri(t-29)];
				}
				else if(t>42 && t<=52) {//43-52:  K-T = 20-29
					toothGraphic=ListToothGraphics[Tooth.PermToPri(t-23)];
				}
				else{//perm
					toothGraphic=ListToothGraphics[t.ToString()];
				}
				for(int g=0;g<10;g++){//groups 0-9
					group=toothGraphic.GetGroupForDisplayList((ToothGroupType)g);
					Gl.glNewList(displayListOffset+(t*10)+g,Gl.GL_COMPILE);
						//ToothGraphic.GetDisplayListNum(i.ToString())
					if(group!=null){
						for(int f=0;f<group.Faces.Count;f++){//.GetLength(0);f++) {//loop through each face
							Gl.glBegin(Gl.GL_POLYGON);
							for(int j=0;j<group.Faces[f].IndexList.Count;j++) {//.Length;j++) {//loop through each vertex
								//The index for both will always be the same because we enforce a 1:1 relationship.
								//We show grabbing a float[3], but we could just as easily use the index itself.
								Gl.glVertex3fv(toothGraphic.VertexNormals[group.Faces[f].IndexList[j]].Vertex.GetFloatArray());
								Gl.glNormal3fv(toothGraphic.VertexNormals[group.Faces[f].IndexList[j]].Normal.GetFloatArray()); 
							}
							//for(int v=0;v<group.Faces[f].VertexNormals.Count;v++){//  .Length;v++) {//loop through each vertex
							//	Gl.glNormal3fv(toothGraphic.Normals[group.Faces[f][v][1]]);
							//	Gl.glVertex3fv(toothGraphic.Vertices[group.Faces[f][v][0]]);
							//}
							Gl.glEnd();
						}
					}
					Gl.glEndList();
				}
			}
		}

		///<summary>Pri or perm tooth numbers are valid.  Only locations of perm teeth are stored.</summary>
		private float GetTransX(string tooth_id) {
			int toothInt=ToothGraphic.IdToInt(tooth_id);
			if(toothInt==-1) {
				throw new ApplicationException("Invalid tooth number: "+tooth_id);//only for debugging
			}
			return ToothGraphic.GetDefaultOrthoXpos(toothInt);
		}

		private float GetTransYfacial(string tooth_id) {
			float basic=29f;
			if(tooth_id=="6" || tooth_id=="11") {
				return basic+1f;
			}
			if(tooth_id=="7" || tooth_id=="10") {
				return basic+1f;
			}
			else if(tooth_id=="8" || tooth_id=="9") {
				return basic+2f;
			}
			else if(tooth_id=="22" || tooth_id=="27") {
				return -basic-2f;
			}
			else if(tooth_id=="23" || tooth_id=="24" || tooth_id=="25" || tooth_id=="26") {
				return -basic-2f;
			}
			else if(ToothGraphic.IsMaxillary(tooth_id)) {
				return basic;
			}
			return -basic;
		}

		private float GetTransYocclusal(string tooth_id) {
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				return 13f;
			}
			return -13f;
		}

		#region Mouse And Selections

		///<summary>Always returns a number between 1 and 32.  This isn't perfect, since it only operates on perm teeth, and assumes that any primary tooth will be at the same x pos as its perm tooth.</summary>
		private int GetToothAtPoint(int x,int y) {
			float closestDelta=(float)(WidthProjection*2);//start it off really big
			int closestTooth=1;
			float toothPos=0;
			float delta=0;
			float xPos=(float)((float)(x-Width/2)*WidthProjection/(float)Width);//in mm instead of screen coordinates
			if(y<Height/2) {//max
				for(int i=1;i<=16;i++) {
					if(ListToothGraphics[i.ToString()].HideNumber){
						continue;
					}
					toothPos=ToothGraphic.GetDefaultOrthoXpos(i);
					if(ToothGraphic.IsRight(i.ToString())) {
						toothPos+=(int)ListToothGraphics[i.ToString()].ShiftM;//*(float)Width/WidthProjection);
					}
					else {
						toothPos-=(int)ListToothGraphics[i.ToString()].ShiftM;//*(float)Width/WidthProjection);
					}
					if(xPos>toothPos) {
						delta=xPos-toothPos;
					}
					else {
						delta=toothPos-xPos;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=i;
					}
				}
				return closestTooth;
			}
			else {//mand
				for(int i=17;i<=32;i++) {
					if(ListToothGraphics[i.ToString()].HideNumber) {
						continue;
					}
					toothPos=ToothGraphic.GetDefaultOrthoXpos(i);//in mm.
					if(ToothGraphic.IsRight(i.ToString())) {
						toothPos+=(int)ListToothGraphics[i.ToString()].ShiftM;
					}
					else {
						toothPos-=(int)ListToothGraphics[i.ToString()].ShiftM;
					}
					if(xPos>toothPos) {
						delta=xPos-toothPos;
					}
					else {
						delta=toothPos-xPos;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=i;
					}
				}
				return closestTooth;
			}
		}

		protected override void OnMouseClick(MouseEventArgs e) {
			base.OnMouseClick(e);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			MouseIsDown=true;
			if(CursorTool==CursorTool.Pointer){
				int toothClicked=GetToothAtPoint(e.X,e.Y);
				//MessageBox.Show(toothClicked.ToString());
				if(ALSelectedTeeth.Contains(toothClicked)) {
					SetSelected(toothClicked,false);
				}
				else {
					SetSelected(toothClicked,true);
				}
			}
			else if(CursorTool==CursorTool.Pen){
				PointList.Add(new Point(e.X,e.Y));
			}
			else if(CursorTool==CursorTool.Eraser){
				//do nothing
			}
			else if(CursorTool==CursorTool.ColorChanger){
				//look for any lines near the "wand".
				//since the line segments are so short, it's sufficient to check end points.
				string[] xy;
				string[] pointStr;
				float x;
				float y;
				float dist;//the distance between the point being tested and the center of the eraser circle.
				float radius=2f;//by trial and error to achieve best feel.
				for(int i=0;i<DrawingSegmentList.Count;i++){
					pointStr=DrawingSegmentList[i].DrawingSegment.Split(';');
					for(int p=0;p<pointStr.Length;p++){
						xy=pointStr[p].Split(',');
						if(xy.Length==2){
							x=float.Parse(xy[0]);
							y=float.Parse(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-e.X),2)+Math.Pow(Math.Abs(y-e.Y),2));
							if(dist<=radius){//testing circle intersection here
								OnSegmentDrawn(DrawingSegmentList[i].DrawingSegment);
								DrawingSegmentList[i].ColorDraw=DrawingColor;
								Invalidate();
								return;;
							}
						}
					}
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if(CursorTool==CursorTool.Pointer){
				hotTooth=GetToothAtPoint(e.X,e.Y);
				if(hotTooth==hotToothOld) {//mouse has not moved to another tooth
					return;
				}
				hotToothOld=hotTooth;
				if(MouseIsDown) {//drag action
					if(ALSelectedTeeth.Contains(hotTooth)) {
						SetSelected(hotTooth,false);
					}
					else {
						SetSelected(hotTooth,true);
					}
				}
			}
			else if(CursorTool==CursorTool.Pen){
				if(!MouseIsDown){
					return;
				}
				PointList.Add(new Point(e.X,e.Y));
				//just add the last line segment instead of redrawing the whole thing.
				Gl.glPushMatrix();
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glEnable(Gl.GL_BLEND);
				Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
				Gl.glDisable(Gl.GL_DEPTH_TEST);
				Gl.glColor3f(
					(float)DrawingColor.R/255f,
					(float)DrawingColor.G/255f,
					(float)DrawingColor.B/255f);
				Gl.glLineWidth((float)Width/300f);//about 2
				int i=PointList.Count-1;
				Gl.glBegin(Gl.GL_LINE_STRIP);
				//if we set 0,0 to center, then this is where we would convert it back.
				PointF pointMm=PixToMm(PointList[i-1]);
				Gl.glVertex3f(pointMm.X,pointMm.Y,0);
				pointMm=PixToMm(PointList[i]);
				Gl.glVertex3f(pointMm.X,pointMm.Y,0);
				Gl.glEnd();
				Gl.glPopMatrix();
				Gl.glFlush();
			}
			else if(CursorTool==CursorTool.Eraser){
				if(!MouseIsDown){
					return;
				}
				//look for any lines that intersect the "eraser".
				//since the line segments are so short, it's sufficient to check end points.
				string[] xy;
				string[] pointStr;
				float x;
				float y;
				float dist;//the distance between the point being tested and the center of the eraser circle.
				float radius=8f;//by trial and error to achieve best feel.
				PointF eraserPt=new PointF(e.X+8.49f,e.Y+8.49f);
				for(int i=0;i<DrawingSegmentList.Count;i++){
					pointStr=DrawingSegmentList[i].DrawingSegment.Split(';');
					for(int p=0;p<pointStr.Length;p++){
						xy=pointStr[p].Split(',');
						if(xy.Length==2){
							x=float.Parse(xy[0]);
							y=float.Parse(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-eraserPt.X),2)+Math.Pow(Math.Abs(y-eraserPt.Y),2));
							if(dist<=radius){//testing circle intersection here
								OnSegmentDrawn(DrawingSegmentList[i].DrawingSegment);//triggers a deletion from db.
								DrawingSegmentList.RemoveAt(i);
								Invalidate();
								return;;
							}
						}
					}
				}	
			}
			else if(CursorTool==CursorTool.ColorChanger){
				//do nothing	
			}
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			MouseIsDown=false;
			if(CursorTool==CursorTool.Pen){
				string drawingSegment="";
				for(int i=0;i<PointList.Count;i++){
					if(i>0){
						drawingSegment+=";";
					}
					//I could compensate to center point here:
					drawingSegment+=PointList[i].X+","+PointList[i].Y;
				}
				OnSegmentDrawn(drawingSegment);
				PointList=new List<Point>();
				//Invalidate();//?
			}
			else if(CursorTool==CursorTool.Eraser){
				//do nothing
			}
			else if(CursorTool==CursorTool.ColorChanger){
				//do nothing
			}
		}

		///<summary></summary>
		protected void OnSegmentDrawn(string drawingSegment){
			ToothChartDrawEventArgs tArgs=new ToothChartDrawEventArgs(drawingSegment);
			if(SegmentDrawn!=null){
				SegmentDrawn(this,tArgs);
			}
		}

		///<summary>Used by mousedown and mouse move to set teeth selected or unselected.  Also used externally to set teeth selected.  Draws the changes also.</summary>
		public void SetSelected(int intTooth,bool setValue) {
			suspendRendering=true;
			if(setValue) {
				ALSelectedTeeth.Add(intTooth);
				DrawNumber(intTooth,true,false);
			}
			else {
				ALSelectedTeeth.Remove(intTooth);
				DrawNumber(intTooth,false,false);
			}
			RectangleF recMm=GetNumberRecMm(intTooth.ToString());
			Rectangle rec=ConvertRecToPix(recMm);
			Invalidate(rec);//but it invalidates the whole thing anyway.  Oh, well.
			Application.DoEvents();
			suspendRendering=false;
			if(ALSelectedTeeth.Count==0) {
				selectedTeeth=new string[0];
			}
			else {
				selectedTeeth=new string[ALSelectedTeeth.Count];
				for(int i=0;i<ALSelectedTeeth.Count;i++) {
					if(ToothGraphic.IsValidToothID(ToothGraphic.PermToPri(ALSelectedTeeth[i].ToString()))//pri is valid
						&& ListToothGraphics[ALSelectedTeeth[i].ToString()].ShowPrimary)//and set to show pri
					{
						selectedTeeth[i]=ToothGraphic.PermToPri(ALSelectedTeeth[i].ToString());
					}
					else{
						selectedTeeth[i]=((int)ALSelectedTeeth[i]).ToString();
					}
				}
			}
		}

		

		#endregion Mouse And Selections


	}
}




