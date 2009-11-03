using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using OpenDentBusiness;

namespace SparksToothChart {

	public partial class ToothChartDirectX:Control {

		///<summary>DirectX handle to this control.</summary>
		private Device device=null;
		///<summary>GDI+ handle to this control. Used for line drawing at least.</summary>
		private Graphics g=null;
		///<summary>This is a reference to the TcData object that's at the wrapper level.</summary>
		public ToothChartData TcData;
		private Color specular_color_normal;
		private Color specular_color_cementum;
		private float specularSharpness;
		private Microsoft.DirectX.Direct3D.Font xfont;

		public ToothChartDirectX() {
			InitializeComponent();
		}

		///<summary>Must be called after the ToothChartDirectX control has been added to a form and should be called before it is drawn the first time.</summary>
		public void InitializeGraphics(){
			PresentParameters pp=new PresentParameters();
			pp.Windowed=true;
			pp.SwapEffect=SwapEffect.Discard;
			pp.EnableAutoDepthStencil=true;
			pp.AutoDepthStencilFormat=DepthFormat.D16;//Z-buffer depth of 16 bits.
			pp.DeviceWindowHandle=this.Handle;
			pp.BackBufferWidth=this.Width;
			pp.BackBufferHeight=this.Height;
			pp.MultiSample=MultiSampleType.FourSamples;//Anti-alias settings.
			device=new Device(0,DeviceType.Hardware,this,CreateFlags.SoftwareVertexProcessing,pp);
			device.DeviceReset+=new EventHandler(this.OnDeviceReset);
			OnDeviceReset(device,null);
			this.Font=new System.Drawing.Font("Arial",9f);//Required for calculating font background rectangle size in ToothChartData.
			xfont=new Microsoft.DirectX.Direct3D.Font(device,
				15,6,FontWeight.Normal,1,false,CharacterSet.Ansi,Precision.Device,
				FontQuality.ClearType,PitchAndFamily.DefaultPitch,"Arial");
			g=this.CreateGraphics();// Graphics.FromHwnd(this.Handle);
		}

		public void CleanUpDirectX(){
			xfont.Dispose();
			for(int i=0;i<TcData.ListToothGraphics.Count;i++) {
				ToothGraphic toothGraphic=TcData.ListToothGraphics[i];
				for(int j=0;j<toothGraphic.Groups.Count;j++) {
					ToothGroup group=toothGraphic.Groups[j];
					group.facesDirectX.Dispose();
					group.facesDirectX=null;
				}
				toothGraphic.vb.Dispose();
				toothGraphic.vb=null;
			}
			device.Dispose();
			device=null;
		}

		///<summary>TODO: Handle the situation when there are suboptimal graphics cards.</summary>
		public void OnDeviceReset(object sender,EventArgs e){
			device=sender as Device;
			for(int i=0;i<TcData.ListToothGraphics.Count;i++) {
				ToothGraphic tooth=TcData.ListToothGraphics[i];
				tooth.PrepareForDirectX(device);
				for(int j=0;j<tooth.Groups.Count;j++) {
					ToothGroup group=tooth.Groups[j];
					group.PrepareForDirectX(device);
				}
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e) {
			//Do nothing to eliminate flicker. 
		}

		protected override void OnSizeChanged(EventArgs e) {
			Invalidate();//Force the control to redraw. 
		}

		protected override void OnPaint(PaintEventArgs pe) {
			//Color backColor=Color.FromArgb(150,145,152);
			if(device==null) {
				//When no rendering context has been set, simply display the control
				//as a black rectangle. This will make the control draw as a blank
				//rectangle when in the designer. 
				pe.Graphics.FillRectangle(new SolidBrush(TcData.ColorBackground),new Rectangle(0,0,Width,Height));
				return;
			}
			Render();
		}

		protected void Render() {
			//Set the view and projection matricies for the camera.
			device.Transform.Projection=Matrix.OrthoLH(TcData.SizeOriginalProjection.Width,TcData.SizeOriginalProjection.Height,0,1000.0f);
			device.Transform.World=Matrix.Identity;
			//viewport transformation not used. Default is to fill entire control.
			device.RenderState.CullMode=Cull.None;//Do not cull triangles. Our triangles are too small for this feature to work reliably.
			device.RenderState.ZBufferEnable=true;
			device.RenderState.ZBufferFunction=Compare.Less;
			//Blend mode settings.
			device.RenderState.AlphaBlendEnable=false;//Disabled
			//device.RenderState.AlphaFunction=Compare.Always;
			//device.RenderState.AlphaTestEnable=true;
			//device.RenderState.SourceBlend=Blend.SourceAlpha;
			//device.RenderState.DestinationBlend=Blend.InvSourceAlpha;
			//device.RenderState.AlphaBlendOperation=BlendOperation.Add;
			//Lighting settings
			device.RenderState.Lighting=true;
			device.RenderState.SpecularEnable=true;
			device.RenderState.SpecularMaterialSource=ColorSource.Material;
			float ambI=.4f;//.2f;//Darker for testing
			float difI=.6f;//.4f;//Darker for testing
			float specI=.8f;//.2f;//Had to turn specular down to avoid bleedout.
			//I think we're going to need to eventually use shaders to get our pinpoint reflections.
			//Set properties for light 0. Diffuse light.
			device.Lights[0].Type=LightType.Directional;
			device.Lights[0].Ambient=Color.FromArgb(255,(int)(255*ambI),(int)(255*ambI),(int)(255*ambI));
			device.Lights[0].Diffuse=Color.FromArgb(255,(int)(255*difI),(int)(255*difI),(int)(255*difI));
			device.Lights[0].Specular=Color.FromArgb(255,(int)(255*specI),(int)(255*specI),(int)(255*specI));
			device.Lights[0].Direction=new Vector3(0.5f,-0.2f,1f);//(0.5f,0.1f,1f);
			device.Lights[0].Enabled=true;
			//Material settings
			float specNorm=1f;
			float specCem=.1f;
			//Also, see DrawTooth for the specular color used for enamel.
			specular_color_normal=Color.FromArgb(255,(int)(255*specNorm),(int)(255*specNorm),(int)(255*specNorm));
			specular_color_cementum=Color.FromArgb(255,(int)(255*specCem),(int)(255*specCem),(int)(255*specCem));
			specularSharpness=70f;//70f;//Not the same as in OpenGL. No maximum value. Smaller number means light is more spread out.
			//Draw
			DrawScene();
		}

		private void DrawScene() {
			device.Clear(ClearFlags.Target|ClearFlags.ZBuffer,TcData.ColorBackground,1.0f,0);
			device.BeginScene();
			//The Z values between OpenGL and DirectX are negated (the axis runs in the opposite direction).
			//We reflect that difference here by negating the z values for all coordinates.
			Matrix defOrient=Matrix.Identity;
			defOrient.Scale(1f,1f,-1f);
			//We make sure to move all teeth forward a large step so that specular lighting will calculate properly.
			//This step does not affect the tooth locations on the screen because changes in z position for a tooth
			//does not affect position in orthographic projections.
			Matrix trans=Matrix.Identity;
			trans.Translate(0f,0f,400f);
			defOrient=defOrient*trans;
			for(int t=0;t<TcData.ListToothGraphics.Count;t++) {//loop through each tooth
				if(TcData.ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				DrawFacialView(TcData.ListToothGraphics[t],defOrient);
				DrawOcclusalView(TcData.ListToothGraphics[t],defOrient);
			}
			//All DirectX drawing is finished for this frame.
			//Now draw all lines using GDI+.
			DrawNumbersAndLines();
			//DrawDrawingSegments();
			device.EndScene();
			device.Present();
		}

		private Matrix ScreenSpaceMatrix(){
			return device.Transform.World*device.Transform.View*device.Transform.Projection;
		}

		private void DrawFacialView(ToothGraphic toothGraphic,Matrix defOrient) {
			Matrix toothTrans=Matrix.Identity;//Start with world transform defined by calling function.
			toothTrans.Translate(GetTransX(toothGraphic.ToothID),
				GetTransYfacial(toothGraphic.ToothID),
				0);
			Matrix rotAndTranUser=RotateAndTranslateUser(toothGraphic);
			device.Transform.World=rotAndTranUser*toothTrans*defOrient;
			if(toothGraphic.Visible
				||(toothGraphic.IsCrown && toothGraphic.IsImplant)
				||toothGraphic.IsPontic) 
			{
				DrawTooth(toothGraphic);
			}
			device.RenderState.ZBufferEnable=false;
			device.RenderState.Lighting=false;
			Matrix lineMatrix=ScreenSpaceMatrix();
			Line line=new Line(device);
			line.Antialias=false;
			if(toothGraphic.DrawBigX) {
				//Thickness of line depends on size of window.
				//The line size needs to be slightly larger than in OpenGL because
				//lines are drawn with polygons in DirectX and they are anti-aliased,
				//even when the line.Antialias flag is set.
				line.Width=(float)Width/200f;
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
					line.DrawTransform(new Vector3[] {
						new Vector3(-2f,12f,0f),
						new Vector3(2f,-6f,0f),},
						lineMatrix,
						toothGraphic.colorX);
					line.DrawTransform(new Vector3[] {
						new Vector3(2f,12f,0f),
						new Vector3(-2f,-6f,0f),},
						lineMatrix,
						toothGraphic.colorX);
				} 
				else {
					line.DrawTransform(new Vector3[] {
						new Vector3(-2f,6f,0f),
						new Vector3(2f,-12f,0f),},
						lineMatrix,
						toothGraphic.colorX);
					line.DrawTransform(new Vector3[] {
						new Vector3(2f,6f,0f),
						new Vector3(-2f,-12f,0f),},
						lineMatrix,
						toothGraphic.colorX);
				}				
			}
			if(toothGraphic.Visible && toothGraphic.IsRCT) {//draw RCT
				//Thickness of lines depend on size of window.
				//The line size needs to be slightly larger than in OpenGL because
				//lines are drawn with polygons in DirectX and they are anti-aliased,
				//even when the line.Antialias flag is set.
				line.Width=(float)Width/160f;
				List<LineSimple> linesSimple=toothGraphic.GetRctLines();
				for(int i=0;i<linesSimple.Count;i++) {
					if(linesSimple[i].Vertices.Count<2){
						continue;//Just to avoid internal errors, even though not likely.
					}
					//Convert each line strip into very simple two point lines so that line extensions can be calculated more easily below.
					List <Vector3> twoPointLines=new List<Vector3> ();
					for(int j=0;j<linesSimple[i].Vertices.Count-1;j++){
						twoPointLines.Add(new Vector3(linesSimple[i].Vertices[j  ].X,linesSimple[i].Vertices[j  ].Y,linesSimple[i].Vertices[j  ].Z));
						twoPointLines.Add(new Vector3(linesSimple[i].Vertices[j+1].X,linesSimple[i].Vertices[j+1].Y,linesSimple[i].Vertices[j+1].Z));
					}
					//Draw each individual two point line. The lines must be broken down from line strips so that when individual two point
					//line locations are modified they do not affect any other two point lines within the same line strip.
					for(int j=0;j<twoPointLines.Count;j+=2){
						Vector3 p1=twoPointLines[j];
						Vector3 p2=twoPointLines[j+1];
						Vector3 lineDir=p2-p1;
						lineDir.Normalize();//Gives the line direction a single unit length.
						float extSize=0.25f;//The number of units to extend each end of the two point line.
						p1=p1-extSize*lineDir;
						p2=p2+extSize*lineDir;
						Vector3[] lineVerts=new Vector3[] {p1,p2};
						line.DrawTransform(lineVerts,lineMatrix,toothGraphic.colorRCT);
					}
					//List<Vector3> lineVerts=new List<Vector3> ();				
					//for(int j=0;j<linesSimple[i].Vertices.Count;j++) {
					//  lineVerts.Add(new Vector3(linesSimple[i].Vertices[j].X,linesSimple[i].Vertices[j].Y,linesSimple[i].Vertices[j].Z));
					//}
					//line.DrawTransform(lineVerts.ToArray(),
					//  lineMatrix,
					//  toothGraphic.colorRCT);
				}
				//Gl.glPointSize((float)Width/275f);//point is slightly smaller since no antialiasing
				////This section is a necessary workaround for OpenGL.
				////It draws a point at each intersection to hide the unsightly transitions between line segments.
				//Gl.glPushMatrix();
				//Gl.glTranslatef(0,0,10.5f);//move forward 10.5mm so it will cover the lines
				//Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
				//RotateAndTranslateUser(toothGraphic);
				//Gl.glDisable(Gl.GL_BLEND);
				//for(int i=0;i<lines.Count;i++) {
				//  Gl.glBegin(Gl.GL_POINTS);
				//  for(int j=0;j<lines[i].Vertices.Count;j++) {
				//    //but ignore the first and last.  We are only concerned with where lines meet.
				//    if(j==0||j==lines[i].Vertices.Count-1) {
				//      continue;
				//    }
				//    Gl.glVertex3f(lines[i].Vertices[j].X,lines[i].Vertices[j].Y,lines[i].Vertices[j].Z);
				//  }
				//  Gl.glEnd();
				//}
				//Gl.glPopMatrix();
			}
			//if(toothGraphic.Visible&&toothGraphic.IsBU) {//BU or Post
			//  Gl.glPushMatrix();
			//  Gl.glTranslatef(0,0,13f);//move BU forward 13mm so it will be visible.
			//  Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
			//  Gl.glDisable(Gl.GL_LIGHTING);
			//  Gl.glDisable(Gl.GL_BLEND);
			//  Gl.glColor3f(
			//    (float)toothGraphic.colorBU.R/255f,
			//    (float)toothGraphic.colorBU.G/255f,
			//    (float)toothGraphic.colorBU.B/255f);
			//  RotateAndTranslateUser(toothGraphic);
			//  Polygon poly=toothGraphic.GetBUpoly();
			//  Gl.glBegin(Gl.GL_POLYGON);
			//  for(int i=0;i<poly.Vertices.Count;i++) {
			//    Gl.glVertex3f(poly.Vertices[i].X,poly.Vertices[i].Y,poly.Vertices[i].Z);
			//  }
			//  Gl.glEnd();
			//  Gl.glPopMatrix();
			//}
			if(toothGraphic.IsImplant) {
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
					//flip the implant upside down
					Matrix flipVertMat=Matrix.Identity;
					flipVertMat.RotateZ((float)Math.PI);
					device.Transform.World=flipVertMat*device.Transform.World;
				}
				device.RenderState.ZBufferEnable=true;
				device.RenderState.Lighting=true;
				Material material=new Material();
				material.Ambient=toothGraphic.colorImplant;
				material.Diffuse=toothGraphic.colorImplant;
				material.Specular=specular_color_normal;
				material.SpecularSharpness=specularSharpness;
				device.Material=material;
				ToothGraphic implantGraphic=TcData.ListToothGraphics["implant"];
				device.VertexFormat=CustomVertex.PositionNormal.Format;
				device.SetStreamSource(0,implantGraphic.vb,0);
				for(int g=0;g<implantGraphic.Groups.Count;g++) {
					ToothGroup group=(ToothGroup)implantGraphic.Groups[g];
					if(!group.Visible) {
						continue;
					}
					device.Indices=group.facesDirectX;
					device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,implantGraphic.VertexNormals.Count,0,group.NumIndicies/3);
				}
			}
			line.Dispose();
			device.RenderState.ZBufferEnable=true;
			device.RenderState.Lighting=true;
		}

		private void DrawOcclusalView(ToothGraphic toothGraphic,Matrix defOrient) {
			//now the occlusal surface. Notice that it's relative to origin again
			Matrix toothTrans=Matrix.Identity;//Start with world transform defined by calling function.
			toothTrans.Translate(GetTransX(toothGraphic.ToothID),
				GetTransYocclusal(toothGraphic.ToothID),
				0);
			Matrix toothRot=Matrix.Identity;
			if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
				toothRot.RotateX((float)((-110f*Math.PI)/180f));//rotate angle about line from origin to x,y,z
			} else {//mandibular
				if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
					toothRot.RotateX((float)((110f*Math.PI)/180f));
				} else {
					toothRot.RotateX((float)((120f*Math.PI)/180f));
				}
			}
			Matrix rotAndTranUser=RotateAndTranslateUser(toothGraphic);
			device.Transform.World=rotAndTranUser*toothRot*toothTrans*defOrient;
			if(!Tooth.IsPrimary(toothGraphic.ToothID)//if perm tooth
				&&Tooth.IsValidDB(Tooth.PermToPri(toothGraphic.ToothID))
				&&TcData.ListToothGraphics[Tooth.PermToPri(toothGraphic.ToothID)].Visible)//and the primary tooth is visible
			{
				//do not paint
			} else if(toothGraphic.Visible//might not be visible if an implant
				||(toothGraphic.IsCrown&&toothGraphic.IsImplant))//a crown on an implant will paint
			//pontics won't paint, because tooth is invisible
			{
				DrawTooth(toothGraphic);
			}
			//Gl.glPopMatrix();//reset to origin
			//if(toothGraphic.Visible&&
			//  toothGraphic.IsSealant) {//draw sealant
			//  Gl.glPushMatrix();
			//  Gl.glTranslatef(0,0,6f);//move forward 6mm so it will be visible.
			//  Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYocclusal(toothGraphic.ToothID),0);
			//  if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
			//    Gl.glRotatef(-110f,1f,0,0);//rotate angle about line from origin to x,y,z
			//  } else {//mandibular
			//    if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
			//      Gl.glRotatef(110f,1f,0,0);
			//    } else {
			//      Gl.glRotatef(120f,1f,0,0);
			//    }
			//  }
			//  Gl.glDisable(Gl.GL_LIGHTING);
			//  Gl.glEnable(Gl.GL_BLEND);
			//  Gl.glColor3f(
			//    (float)toothGraphic.colorSealant.R/255f,
			//    (float)toothGraphic.colorSealant.G/255f,
			//    (float)toothGraphic.colorSealant.B/255f);
			//  //.5f);//only 1/2 darkness
			//  Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
			//  Gl.glLineWidth((float)Width/225f);
			//  Gl.glPointSize((float)Width/275f);//point is slightly smaller since no antialiasing
			//  RotateAndTranslateUser(toothGraphic);
			//  Line line=toothGraphic.GetSealantLine();
			//  Gl.glBegin(Gl.GL_LINE_STRIP);
			//  for(int j=0;j<line.Vertices.Count;j++) {//loop through each vertex
			//    Gl.glVertex3f(line.Vertices[j].X,line.Vertices[j].Y,line.Vertices[j].Z);
			//  }
			//  Gl.glEnd();
			//  //The next 30 or so lines are all a stupid OpenGL workaround to hide the line intersections with big dots.
			//  Gl.glPopMatrix();
			//  //now, draw a point at each intersection to hide the unsightly transitions
			//  Gl.glPushMatrix();
			//  //move foward so it will cover the lines
			//  Gl.glTranslatef(0,0,6.5f);
			//  Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYocclusal(toothGraphic.ToothID),0);
			//  if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
			//    Gl.glRotatef(-110f,1f,0,0);//rotate angle about line from origin to x,y,z
			//  } else {//mandibular
			//    if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
			//      Gl.glRotatef(110f,1f,0,0);
			//    } else {
			//      Gl.glRotatef(120f,1f,0,0);
			//    }
			//  }
			//  RotateAndTranslateUser(toothGraphic);
			//  Gl.glDisable(Gl.GL_BLEND);
			//  Gl.glBegin(Gl.GL_POINTS);
			//  for(int j=0;j<line.Vertices.Count;j++) {//loop through each vertex
			//    //but ignore the first and last.  We are only concerned with where lines meet.
			//    if(j==0||j==line.Vertices.Count-1) {
			//      continue;
			//    }
			//    Gl.glVertex3f(line.Vertices[j].X,line.Vertices[j].Y,line.Vertices[j].Z);
			//  }
			//  Gl.glEnd();
			//  Gl.glPopMatrix();
			//}
		}

		private void DrawTooth(ToothGraphic toothGraphic) {
			ToothGroup group;
			device.VertexFormat=CustomVertex.PositionNormal.Format;
			device.SetStreamSource(0,toothGraphic.vb,0);
			for(int g=0;g<toothGraphic.Groups.Count;g++) {
				group=(ToothGroup)toothGraphic.Groups[g];
				if(!group.Visible) {
					continue;
				}
				Material material=new Material();
				Color materialColor;
				if(toothGraphic.ShiftO < -10) {//if unerupted
					materialColor=Color.FromArgb(group.PaintColor.A/2,group.PaintColor.R/2,group.PaintColor.G/2,group.PaintColor.B/2);
				} 
				else {
					materialColor=group.PaintColor;
				}
				material.Ambient=materialColor;
				material.Diffuse=materialColor;
				if(group.GroupType==ToothGroupType.Cementum) {
					material.Specular=specular_color_cementum;
				} 
				else if(group.PaintColor.R>245 && group.PaintColor.G>245 && group.PaintColor.B>235){
					//because DirectX washes out the specular on the enamel, we have to turn it down only for the enamel color
					//for reference, this is the current enamel color: Color.FromArgb(255,250,250,240)
					float specEnamel=.4f;
					material.Specular=Color.FromArgb(255,(int)(255*specEnamel),(int)(255*specEnamel),(int)(255*specEnamel));
				}
				else {
					material.Specular=specular_color_normal;
				}				
				material.SpecularSharpness=specularSharpness;
				device.Material=material;
				//draw the group
			  device.Indices=group.facesDirectX;
				device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,toothGraphic.VertexNormals.Count,0,group.NumIndicies/3);				
			}
		}

		///<summary>Performs the rotations and translations entered by user for this tooth.  Usually, all numbers are just 0, resulting in no movement here. Returns the result as a Matrix that will need to be applied to any other movement and rotation matricies being applied to the tooth.</summary>
		private Matrix RotateAndTranslateUser(ToothGraphic toothGraphic) {
			//remembering that they actually show in the opposite order, so:
			//1: translate
			//2: tipM last
			//3: tipB second
			//4: rotate first
			Matrix tran=Matrix.Identity;
			Matrix rotM=Matrix.Identity;
			Matrix rotB=Matrix.Identity;
			Matrix rot=Matrix.Identity;
			if(ToothGraphic.IsRight(toothGraphic.ToothID)) {
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {//UR
					tran.Translate(toothGraphic.ShiftM,-toothGraphic.ShiftO,toothGraphic.ShiftB);					
					rotM.RotateZ(((float)(toothGraphic.TipM*Math.PI)/180));//Converts angle to radians as required.
					rotB.RotateX(((float)(-toothGraphic.TipB*Math.PI)/180));//Converts angle to radians as required.
					rot.RotateY(((float)(toothGraphic.Rotate*Math.PI)/180));//Converts angle to radians as required.
				} else {//LR
					tran.Translate(toothGraphic.ShiftM,toothGraphic.ShiftO,toothGraphic.ShiftB);
					rotM.RotateZ(((float)(-toothGraphic.TipM*Math.PI)/180));//Converts angle to radians as required.
					rotB.RotateX(((float)(toothGraphic.TipB*Math.PI)/180));//Converts angle to radians as required.
					rot.RotateY(((float)(-toothGraphic.Rotate*Math.PI)/180));//Converts angle to radians as required.
				}
			} else {
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {//UL
					tran.Translate(-toothGraphic.ShiftM,-toothGraphic.ShiftO,toothGraphic.ShiftB);
					rotM.RotateZ(((float)(-toothGraphic.TipM*Math.PI)/180));//Converts angle to radians as required.
					rotB.RotateX(((float)(-toothGraphic.TipB*Math.PI)/180));//Converts angle to radians as required.
					rot.RotateY(((float)(toothGraphic.Rotate*Math.PI)/180));//Converts angle to radians as required.
				} else {//LL
					tran.Translate(-toothGraphic.ShiftM,toothGraphic.ShiftO,toothGraphic.ShiftB);
					rotM.RotateZ(((float)(toothGraphic.TipM*Math.PI)/180));//Converts angle to radians as required.
					rotB.RotateX(((float)(toothGraphic.TipB*Math.PI)/180));//Converts angle to radians as required.
					rot.RotateY(((float)(-toothGraphic.Rotate*Math.PI)/180));//Converts angle to radians as required.
				}
			}
			return rot*rotB*rotM*tran;
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
			if(tooth_id=="6"||tooth_id=="11") {
				return basic+1f;
			}
			if(tooth_id=="7"||tooth_id=="10") {
				return basic+1f;
			} else if(tooth_id=="8"||tooth_id=="9") {
				return basic+2f;
			} else if(tooth_id=="22"||tooth_id=="27") {
				return -basic-2f;
			} else if(tooth_id=="23"||tooth_id=="24"||tooth_id=="25"||tooth_id=="26") {
				return -basic-2f;
			} else if(ToothGraphic.IsMaxillary(tooth_id)) {
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

		private void DrawNumbersAndLines() {
			//Draw the center line.
			g.DrawLine(new Pen(Brushes.White),0,this.Height/2,this.Width,this.Height/2);
			//Draw the tooth numbers.
			string tooth_id;
			for(int i=1;i<=52;i++) {
				tooth_id=Tooth.FromOrdinal(i);
				if(TcData.SelectedTeeth.Contains(tooth_id)) {
					DrawNumber(tooth_id,true,true);
				} else {
					DrawNumber(tooth_id,false,true);
				}
			}
		}

		///<summary>Draws the number and the small rectangle behind it.  Draws in the appropriate color.  isFullRedraw means that all of the toothnumbers are being redrawn.  This helps with a few optimizations and with not painting blank rectangles when not needed.</summary>
		private void DrawNumber(string tooth_id,bool isSelected,bool isFullRedraw) {
			if(!Tooth.IsValidDB(tooth_id)) {
				return;
			}
			device.RenderState.Lighting=false;
			device.RenderState.ZBufferEnable=false;
			device.Transform.World=Matrix.Identity;
			if(isFullRedraw) {//if redrawing all numbers
				if(TcData.ListToothGraphics[tooth_id].HideNumber) {//and this is a "hidden" number
					return;//skip
				}
				if(Tooth.IsPrimary(tooth_id)
					&&!TcData.ListToothGraphics[Tooth.PriToPerm(tooth_id)].ShowPrimaryLetter)//but not set to show primary letters
				{
					return;
				}
			}
			//fix this.  No calls to OpenDentBusiness that require database.
			string displayNum=tooth_id;
			float toMm=1f/TcData.ScaleMmToPix;
			RectangleF recMm=TcData.GetNumberRecMm(tooth_id,g);
			//recMm=new RectangleF(recMm.X,recMm.Y-1f*toMm,recMm.Width,recMm.Height);//Due to anti-aliasing, the boxes overlap one or two teeth unless adjusted.
			Color backColor;
			Color foreColor;
			if(isSelected) {
				backColor=TcData.ColorBackHighlight;
				foreColor=TcData.ColorTextHighlight;
			} else {
				backColor=TcData.ColorBackground;
				foreColor=TcData.ColorText;
			}
			//Draw the background rectangle.
			int backColorARGB=backColor.ToArgb();
			CustomVertex.PositionColored[] quadVerts=new CustomVertex.PositionColored[] {
					new CustomVertex.PositionColored(recMm.X,recMm.Y,14,backColorARGB),//LL
					new CustomVertex.PositionColored(recMm.X,recMm.Y+recMm.Height,14,backColorARGB),//UL
					new CustomVertex.PositionColored(recMm.X+recMm.Width,recMm.Y+recMm.Height,14,backColorARGB),//UR
					new CustomVertex.PositionColored(recMm.X+recMm.Width,recMm.Y,14,backColorARGB),//LR
				};
			VertexBuffer vb=new VertexBuffer(typeof(CustomVertex.PositionColored),CustomVertex.PositionColored.StrideSize*quadVerts.Length,
				device,Usage.WriteOnly,CustomVertex.PositionColored.Format,Pool.Managed);
			vb.SetData(quadVerts,0,LockFlags.None);
			int[] indicies=new int[] { 0,1,2,0,2,3 };
			IndexBuffer ib=new IndexBuffer(typeof(int),indicies.Length,device,Usage.None,Pool.Managed);
			ib.SetData(indicies,0,LockFlags.None);
			device.VertexFormat=CustomVertex.PositionColored.Format;
			device.SetStreamSource(0,vb,0);
			device.Indices=ib;
			device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,quadVerts.Length,0,indicies.Length/3);
			ib.Dispose();
			vb.Dispose();
			//Draw the foreground text if needed.
			if(TcData.ListToothGraphics[tooth_id].HideNumber) {//If number is hidden.
				//do not print string
			} else if(Tooth.IsPrimary(tooth_id)
				&&!TcData.ListToothGraphics[Tooth.PriToPerm(tooth_id)].ShowPrimaryLetter) {
				//do not print string
			} else {
				PrintString(displayNum,recMm.X+2f*toMm,recMm.Y+13f*toMm,15f,foreColor);
			}
		}

		private void PrintString(string text,float x,float y,float z,Color color) {
			Vector3 screenPoint=new Vector3(x,y,z);
			screenPoint.Project(device.Viewport,device.Transform.Projection,device.Transform.View,device.Transform.World);
			xfont.DrawText(null,text,new Point((int)screenPoint.X,(int)screenPoint.Y),color);
		}

		///<summary>Returns a bitmap of what is showing in the control.  Used for printing.</summary>
		public Bitmap GetBitmap() {
			Surface backBuffer=device.GetBackBuffer(0,0,BackBufferType.Mono);
			GraphicsStream gs=SurfaceLoader.SaveToStream(ImageFileFormat.Bmp,backBuffer);
			Bitmap bitmap=new Bitmap(gs);
			backBuffer.Dispose();
			return bitmap;
		}

	}
}
