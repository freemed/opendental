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
		private float shininess;

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
			pp.MultiSample=MultiSampleType.FourSamples;//Anti-alias settings.
			device=new Device(0,DeviceType.Hardware,this,CreateFlags.SoftwareVertexProcessing,pp);
			device.DeviceReset+=new EventHandler(this.OnDeviceReset);
			OnDeviceReset(device,null);
			for(int i=0;i<TcData.ListToothGraphics.Count;i++) {
				ToothGraphic tooth=TcData.ListToothGraphics[i];
				for(int j=0;j<tooth.Groups.Count;j++) {
					ToothGroup group=tooth.Groups[j];
					group.PrepareForDirectX(device,tooth.VertexNormals);
				}
			}
			g=this.CreateGraphics();// Graphics.FromHwnd(this.Handle);
		}

		///<summary>TODO: Handle the situation when there are suboptimal graphics cards.</summary>
		public void OnDeviceReset(object sender,EventArgs e){
			device=sender as Device;
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
			float specI=.2f;//.5f;//Had to turn specular down to avoid bleedout.
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
			specular_color_normal=Color.FromArgb(255,(int)(255*specNorm),(int)(255*specNorm),(int)(255*specNorm));
			specular_color_cementum=Color.FromArgb(255,(int)(255*specCem),(int)(255*specCem),(int)(255*specCem));
			shininess=70f;//70f;//Not the same as in OpenGL. No maximum value. Smaller number means light is more spread out.
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
			device.EndScene();
			device.Present();
			//All DirectX drawing is finished for this frame.
			//Now draw all lines using GDI+.
			DrawTextAndLines();
			//DrawDrawingSegments();
		}

		private void DrawFacialView(ToothGraphic toothGraphic,Matrix defOrient) {
			Matrix toothTrans=Matrix.Identity;//Start with world transform defined by calling function.
			toothTrans.Translate(GetTransX(toothGraphic.ToothID),
				GetTransYfacial(toothGraphic.ToothID),
				0);
			Matrix rotAndTranUser=RotateAndTranslateUser(toothGraphic);
			device.Transform.World=rotAndTranUser*toothTrans*defOrient;
			if(toothGraphic.Visible
				||(toothGraphic.IsCrown&&toothGraphic.IsImplant)
				||toothGraphic.IsPontic) {
				DrawTooth(toothGraphic);
			}
			//Gl.glDisable(Gl.GL_DEPTH_TEST);
			//if(toothGraphic.DrawBigX) {
			//  Gl.glDisable(Gl.GL_LIGHTING);
			//  Gl.glEnable(Gl.GL_BLEND);
			//  //move the bigX 6mm to the Facial so it will paint in front of the tooth
			//  Gl.glTranslatef(0,0,6f);
			//  Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
			//  Gl.glLineWidth((float)Width/275f);//1.5f);//thickness of line depends on size of window
			//  Gl.glColor3f(
			//    (float)toothGraphic.colorX.R/255f,
			//    (float)toothGraphic.colorX.G/255f,
			//    (float)toothGraphic.colorX.B/255f);
			//  if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
			//    Gl.glBegin(Gl.GL_LINES);
			//    Gl.glVertex2f(-2f,12f);
			//    Gl.glVertex2f(2f,-6f);
			//    Gl.glEnd();
			//    Gl.glBegin(Gl.GL_LINES);
			//    Gl.glVertex2f(2f,12f);
			//    Gl.glVertex2f(-2f,-6f);
			//    Gl.glEnd();
			//  } else {
			//    Gl.glBegin(Gl.GL_LINES);
			//    Gl.glVertex2f(-2f,6f);
			//    Gl.glVertex2f(2f,-12f);
			//    Gl.glEnd();
			//    Gl.glBegin(Gl.GL_LINES);
			//    Gl.glVertex2f(2f,6f);
			//    Gl.glVertex2f(-2f,-12f);
			//    Gl.glEnd();
			//  }
			//}
			//Gl.glPopMatrix();//reset to origin
			//if(toothGraphic.Visible&&toothGraphic.IsRCT) {//draw RCT
			//  Gl.glPushMatrix();
			//  Gl.glTranslatef(0,0,10f);//move RCT forward 10mm so it will be visible.
			//  Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
			//  Gl.glDisable(Gl.GL_LIGHTING);
			//  Gl.glEnable(Gl.GL_BLEND);
			//  Gl.glColor3f(
			//    (float)toothGraphic.colorRCT.R/255f,
			//    (float)toothGraphic.colorRCT.G/255f,
			//    (float)toothGraphic.colorRCT.B/255f);
			//  //.5f);//only 1/2 darkness
			//  Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
			//  Gl.glLineWidth((float)Width/225f);
			//  Gl.glPointSize((float)Width/275f);//point is slightly smaller since no antialiasing
			//  RotateAndTranslateUser(toothGraphic);
			//  List<Line> lines=toothGraphic.GetRctLines();
			//  for(int i=0;i<lines.Count;i++) {
			//    Gl.glBegin(Gl.GL_LINE_STRIP);
			//    for(int j=0;j<lines[i].Vertices.Count;j++) {
			//      Gl.glVertex3f(lines[i].Vertices[j].X,lines[i].Vertices[j].Y,lines[i].Vertices[j].Z);
			//    }
			//    Gl.glEnd();
			//  }
			//  Gl.glPopMatrix();
			//  //This section is a necessary workaround for OpenGL.
			//  //It draws a point at each intersection to hide the unsightly transitions between line segments.
			//  Gl.glPushMatrix();
			//  Gl.glTranslatef(0,0,10.5f);//move forward 10.5mm so it will cover the lines
			//  Gl.glTranslatef(GetTransX(toothGraphic.ToothID),GetTransYfacial(toothGraphic.ToothID),0);
			//  RotateAndTranslateUser(toothGraphic);
			//  Gl.glDisable(Gl.GL_BLEND);
			//  for(int i=0;i<lines.Count;i++) {
			//    Gl.glBegin(Gl.GL_POINTS);
			//    for(int j=0;j<lines[i].Vertices.Count;j++) {
			//      //but ignore the first and last.  We are only concerned with where lines meet.
			//      if(j==0||j==lines[i].Vertices.Count-1) {
			//        continue;
			//      }
			//      Gl.glVertex3f(lines[i].Vertices[j].X,lines[i].Vertices[j].Y,lines[i].Vertices[j].Z);
			//    }
			//    Gl.glEnd();
			//  }
			//  Gl.glPopMatrix();
			//}
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
			//if(toothGraphic.IsImplant) {
			//  Gl.glPushMatrix();
			//  Gl.glTranslatef(GetTransX(toothGraphic.ToothID),//Move the tooth to the correct position for facial view
			//    GetTransYfacial(toothGraphic.ToothID),
			//    0);
			//  RotateAndTranslateUser(toothGraphic);
			//  if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
			//    //flip the implant upside down
			//    Gl.glRotatef(180f,0,0,1f);
			//  }
			//  Gl.glEnable(Gl.GL_LIGHTING);
			//  Gl.glEnable(Gl.GL_BLEND);
			//  Gl.glEnable(Gl.GL_DEPTH_TEST);
			//  ToothGroup group=(ToothGroup)ListToothGraphics["implant"].Groups[0];
			//  float[] material_color=new float[] {
			//    (float)toothGraphic.colorImplant.R/255f,
			//    (float)toothGraphic.colorImplant.G/255f,
			//    (float)toothGraphic.colorImplant.B/255f,
			//    (float)toothGraphic.colorImplant.A/255f
			//  };//RGBA
			//  Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SPECULAR,specular_color_normal);
			//  Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_SHININESS,shininess);
			//  Gl.glMaterialfv(Gl.GL_FRONT,Gl.GL_AMBIENT_AND_DIFFUSE,material_color);
			//  Gl.glBlendFunc(Gl.GL_ONE,Gl.GL_ZERO);
			//  Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT,Gl.GL_NICEST);
			//  for(int i=0;i<group.Faces.Count;i++) {//  .GetLength(0);i++) {//loop through each face
			//    Gl.glBegin(Gl.GL_POLYGON);
			//    for(int j=0;j<group.Faces[i].IndexList.Count;j++) {//.Length;j++) {//loop through each vertex
			//      //The index for both will always be the same because we enforce a 1:1 relationship.
			//      //We show grabbing a float[3], but we could just as easily use the index itself.
			//      Gl.glVertex3fv(ListToothGraphics["implant"].VertexNormals[group.Faces[i].IndexList[j]].Vertex.GetFloatArray());//Vertices[group.Faces[i][j][0]]);
			//      Gl.glNormal3fv(ListToothGraphics["implant"].VertexNormals[group.Faces[i].IndexList[j]].Normal.GetFloatArray()); //.Normals[group.Faces[i][j][1]]);
			//    }
			//    Gl.glEnd();
			//  }
			//  Gl.glPopMatrix();
			//}
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
			if(toothGraphic.Visible//might not be visible if an implant
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
			device.VertexFormat=CustomVertex.PositionNormalColored.Format;
			for(int g=0;g<toothGraphic.Groups.Count;g++) {
				group=(ToothGroup)toothGraphic.Groups[g];
				if(!group.Visible) {
					continue;
				}
				Material material=new Material();
				Color materialColor;
				if(toothGraphic.ShiftO<-10) {//if unerupted
					materialColor=Color.FromArgb(group.PaintColor.A/16,group.PaintColor.R/16,group.PaintColor.G/16,group.PaintColor.B/16);
				} else {
					materialColor=group.PaintColor;
				}
				material.Ambient=materialColor;
				material.Diffuse=materialColor;
				if(group.GroupType==ToothGroupType.Cementum) {
					material.Specular=specular_color_cementum;
				} else {
					material.Specular=specular_color_normal;
				}				
				material.SpecularSharpness=shininess;
				device.Material=material;
				//draw the group
				device.SetStreamSource(0,group.VertexBuffer,0);
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

		private void DrawTextAndLines() {

			g.DrawLine(new Pen(Brushes.White),0,this.Height/2,this.Width,this.Height/2);


			//device.RenderState.Lighting=false;
			//device.Lights[0].Enabled=false;
		
			////Draw the horizontal line accross the tooth chart.
			//device.Transform.World=Matrix.Identity;
			//CustomVertex.PositionColored[] linePoints=new CustomVertex.PositionColored[2];
			//linePoints[0].X=-(float)WidthProjection/2f;
			//linePoints[0].Color=Color.Red.ToArgb();
			//linePoints[1].X=(float)WidthProjection/2f;
			//linePoints[1].Color=Color.Blue.ToArgb();
			//VertexBuffer vb=new VertexBuffer(typeof(CustomVertex.PositionColored),CustomVertex.PositionColored.StrideSize*linePoints.Length,
			//  device,Usage.WriteOnly,CustomVertex.PositionColored.Format,Pool.Managed);
			//vb.SetData(linePoints,0,LockFlags.None);
			//device.SetStreamSource(0,vb,0);
			//int[] indicies=new int[] { 0,1 };
			//IndexBuffer ib=new IndexBuffer(typeof(int),indicies.Length,device,Usage.None,Pool.Managed);
			//ib.SetData(indicies,0,LockFlags.None);
			//device.Indices=ib;
			//device.DrawIndexedPrimitives(PrimitiveType.LineList,0,0,linePoints.Length,0,linePoints.Length-1);
			//vb.Dispose();
			//ib.Dispose();

			//Microsoft.DirectX.Direct3D.Line a;//This class is supposed to exist but does not! Maybe out Line class is blocking it? No idea...


			////Draw the horizontal line accross the tooth chart.
			//Matrix offset=Matrix.Identity;
			//offset.Translate(-0.5f,0,0);
			//device.Transform.World=offset;
			//float lineWidth=(float)(Width/400f)/6f;
			//CustomVertex.PositionColored[] linePoints=new CustomVertex.PositionColored[4];
			////0
			//linePoints[0].X=-(float)WidthProjection/2f;
			//linePoints[0].Y=-lineWidth/2;
			//linePoints[0].Color=Color.White.ToArgb();
			////1
			//linePoints[1].X=-(float)WidthProjection/2f;
			//linePoints[1].Y=lineWidth/2;
			//linePoints[1].Color=Color.White.ToArgb();
			////2
			//linePoints[2].X=(float)WidthProjection/2f;
			//linePoints[2].Y=lineWidth/2;
			//linePoints[2].Color=Color.White.ToArgb();
			////3
			//linePoints[3].X=(float)WidthProjection/2f;
			//linePoints[3].Y=-lineWidth/2;
			//linePoints[3].Color=Color.White.ToArgb();
			////
			//VertexBuffer vb=new VertexBuffer(typeof(CustomVertex.PositionColored),CustomVertex.PositionColored.StrideSize*linePoints.Length,
			//  device,Usage.WriteOnly,CustomVertex.PositionColored.Format,Pool.Managed);
			//vb.SetData(linePoints,0,LockFlags.None);
			//device.SetStreamSource(0,vb,0);
			//int[] indicies=new int[] { 0,2,1,0,3,2 };
			//IndexBuffer ib=new IndexBuffer(typeof(int),indicies.Length,device,Usage.None,Pool.Managed);
			//ib.SetData(indicies,0,LockFlags.None);
			//device.Indices=ib;
			//device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,linePoints.Length,0,indicies.Length/3);
			//vb.Dispose();
			//ib.Dispose();


			//Gl.glColor3f(
			//  (float)Color.White.R/255f,
			//  (float)Color.White.G/255f,
			//  (float)Color.White.B/255f);
			//Gl.glLineWidth((float)Width/400f);//about 1
			
			//Gl.glVertex3f(-(float)WidthProjection/2f,0,0);
			//Gl.glVertex3f((float)WidthProjection/2f,0,0);
		}

	}
}
