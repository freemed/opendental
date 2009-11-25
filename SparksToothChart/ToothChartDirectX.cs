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
		public Device device=null;
		///<summary>This is a reference to the TcData object that's at the wrapper level.</summary>
		public ToothChartData TcData;
		private Color specular_color_normal;
		private Color specular_color_cementum;
		private float specularSharpness;
		private Microsoft.DirectX.Direct3D.Font xfont;
		private Microsoft.DirectX.Direct3D.Font xSealantFont;
		private bool MouseIsDown=false;
		[Category("Action"),Description("Occurs when the mouse goes up ending a drawing segment.")]
		public event ToothChartDrawEventHandler SegmentDrawn=null;
		///<summary>Mouse move causes this variable to be updated with the current tooth that the mouse is hovering over.</summary>
		private string hotTooth;
		///<summary>The previous hotTooth.  If this is different than hotTooth, then mouse has just now moved to a new tooth.  Can be 0 to represent no previous.</summary>
		private string hotToothOld;
		private bool deviceLost=true;
		//private DateTime frameBeginTime=DateTime.MinValue;
		//private DateTime frameEndTime=DateTime.MinValue;

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
			device.DeviceLost+=new EventHandler(this.OnDeviceLost);
			device.DeviceResizing+=new CancelEventHandler(this.OnDeviceResizing);
			OnDeviceReset(device,null);
		}

		public void SetSize(Size size){
			this.Size=size;
			Reinitialize();
		}

		public void Reinitialize(){
			CleanupDirectX();
			if(device!=null) {
				device.Dispose();
				device=null;
			}
			InitializeGraphics();
		}

		public void CleanupDirectX(){
			if(xSealantFont!=null){
				xSealantFont.Dispose();
				xSealantFont=null;
			}
			if(xfont!=null){
				xfont.Dispose();
				xfont=null;
			}
			TcData.CleanupDirectX();
		}

		///<summary></summary>
		public void OnDeviceReset(object sender,EventArgs e){
			deviceLost=false;
			CleanupDirectX();
			device=sender as Device;
			//Required for calculating font background rectangle size in ToothChartData.
			TcData.Font=new System.Drawing.Font("Arial",9f);
			xfont=new Microsoft.DirectX.Direct3D.Font(device,
				(int)Math.Round((float)(14*Math.Sqrt(TcData.PixelScaleRatio))),
				(int)Math.Round((float)(5*Math.Sqrt(TcData.PixelScaleRatio))),
				FontWeight.Normal,1,false,CharacterSet.Ansi,Precision.Device,
				FontQuality.ClearType,PitchAndFamily.DefaultPitch,"Arial");
			xSealantFont=new Microsoft.DirectX.Direct3D.Font(device,
				(int)Math.Round(25*TcData.PixelScaleRatio),
				(int)Math.Round(9*TcData.PixelScaleRatio),
				FontWeight.Regular,1,false,CharacterSet.Ansi,Precision.Device,
				FontQuality.ClearType,PitchAndFamily.DefaultPitch,"Arial");
			TcData.PrepareForDirectX(device);
		}

		public void OnDeviceLost(object sender,EventArgs e){
			deviceLost=true;
		}

		public void OnDeviceResizing(object sender,EventArgs e) {
			//Hmm, is this function ever called? I couldn't make it fire with initial testing.
		}

		protected override void OnPaintBackground(PaintEventArgs e) {
			//Do nothing to eliminate flicker. 
		}

		protected override void OnSizeChanged(EventArgs e) {
			Invalidate();//Force the control to redraw. 
		}

		private Matrix ScreenSpaceMatrix() {
			return device.Transform.World*device.Transform.View*device.Transform.Projection;
		}

		protected override void OnPaint(PaintEventArgs pe) {
			//Color backColor=Color.FromArgb(150,145,152);
			if(device==null || deviceLost) {
				//When no rendering context has been set, simply display the control
				//as a black rectangle. This will make the control draw as a blank
				//rectangle when in the designer. 
				pe.Graphics.FillRectangle(new SolidBrush(TcData.ColorBackground),new Rectangle(0,0,Width,Height));
				return;
			}
			//When the OS user is switched then switched back or when coming back from stand by mode, the OS calls the OnPaint function
			//even before it calls the OnDeviceLost() function. When this happens, the render will fail
			//because the DirectX device is not in a valid state to be rendered to. Before the exception is returned from Render(), 
			//a call is made by the OS to OnDeviceLost(), which sets deviceLost=true (when the OnPaint() function begins, deviceLost=false)
			//so that further rendering will not occur with the device in its invalid state.elec
			try {
				Render();
			}
			catch { 
				if(deviceLost){
					//Rendering failed because our device is invalid. Reinitialize the device and cached objects and force
					//the control to be rerendered.
					Reinitialize();
					Invalidate();
				}
			}
		}

		protected void Render() {
			//Set the view and projection matricies for the camera.
			float heightProj=TcData.SizeOriginalProjection.Height;
			float widthProj=TcData.SizeOriginalProjection.Width;
			if(TcData.IsWide) {
				widthProj=heightProj*Width/Height;
			} else {//tall
				heightProj=widthProj*Height/Width;
			}
			device.Transform.Projection=Matrix.OrthoLH(widthProj,heightProj,0,1000.0f);
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
			if(TcData.PerioMode) {
				DrawScenePerio();
			}
			else {
				DrawScene();
			}
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
			//frameBeginTime=DateTime.Now;
			for(int t=0;t<TcData.ListToothGraphics.Count;t++) {//loop through each tooth
				if(TcData.ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				DrawFacialView(TcData.ListToothGraphics[t],defOrient);
				DrawOcclusalView(TcData.ListToothGraphics[t],defOrient);
			}
			//frameEndTime=DateTime.Now;
			DrawDrawingSegments();
			DrawNumbersAndLines();//Numbers and center lines are top-most.			
			device.EndScene();
			device.Present();
		}

		private void DrawScenePerio() {
			device.Clear(ClearFlags.Target|ClearFlags.ZBuffer,TcData.ColorBackground,1.0f,0);
			device.BeginScene();
			Matrix defOrient=Matrix.Scaling(1f,1f,-1f)*Matrix.Translation(0,0,400f);
			List <ToothGraphic> maxillaryTeeth=new List <ToothGraphic> ();
			List <ToothGraphic> mandibleTeeth=new List <ToothGraphic> ();
			for(int t=0;t<TcData.ListToothGraphics.Count;t++) {//loop through each tooth
				if(TcData.ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				if(ToothGraphic.IsMaxillary(TcData.ListToothGraphics[t].ToothID)){
					maxillaryTeeth.Add(TcData.ListToothGraphics[t]);
				}else{
					mandibleTeeth.Add(TcData.ListToothGraphics[t]);
				}
			}
			//Draw the maxillary upper-most row.
			Matrix maxillaryUpperRowMat=Matrix.Translation(0,45f,0)*defOrient;
			DrawPerioRow(maxillaryTeeth,maxillaryUpperRowMat,true,false);
			//Draw the maxillary lower-most row with teeth which have negated z values.
			Matrix maxillaryLowerRowMat=Matrix.Scaling(1f,1f,-1f)*Matrix.Translation(0,12f,0)*defOrient;
			DrawPerioRow(maxillaryTeeth,maxillaryLowerRowMat,true,true);
			//Draw the mandible upper-most row with teeth which have negated z values.
			Matrix mandibleUpperRowMat=Matrix.Scaling(1f,1f,-1f)*Matrix.Translation(0,-12f,0)*defOrient;
			DrawPerioRow(mandibleTeeth,mandibleUpperRowMat,false,true);
			//Draw the mandible lower-most row.
			Matrix mandibleLowerRowMat=Matrix.Translation(0,-45f,0)*defOrient;
			DrawPerioRow(mandibleTeeth,mandibleLowerRowMat,false,false);
			DrawNumbersAndLinesPerio();//Numbers and center lines are top-most.			
			device.EndScene();
			device.Present();
		}

		private void DrawPerioRow(List <ToothGraphic> toothGraphics,Matrix orientation,bool maxillary,bool lingual){
			for(int t=0;t<toothGraphics.Count;t++){
				device.RenderState.ZBufferEnable=true;
				device.RenderState.Lighting=true;
				device.Transform.World=Matrix.Translation(GetTransX(toothGraphics[t].ToothID),0,0)*orientation;
				if(toothGraphics[t].Visible
					|| (toothGraphics[t].IsCrown && toothGraphics[t].IsImplant)
					|| toothGraphics[t].IsPontic) {
					DrawTooth(toothGraphics[t]);
				}
				if(toothGraphics[t].IsImplant) {
				  DrawImplant(toothGraphics[t]);
				}
			}
			device.RenderState.ZBufferEnable=false;
			device.RenderState.Lighting=false;
			Line line=new Line(device);
			float sign=maxillary?1:-1;
			//Separate loop than drawing the teeth, so that we don't need to change
			//the device.RenderState.ZBufferEnable and device.RenderState.Lighting state variables for every
			//tooth. This will help the speed a little.
			for(int t=0;t<toothGraphics.Count;t++){
				SizeF mobTextSize=MeasureStringMm(toothGraphics[t].Mobility);
				//Draw mobility numbers.
				device.Transform.World=Matrix.Translation(GetTransX(toothGraphics[t].ToothID)-mobTextSize.Width/2f,0,0)*orientation;
				PrintString(toothGraphics[t].Mobility,0,maxillary?-1.5f:5.5f,0,toothGraphics[t].colorMobility,xfont);
				Matrix toothLineMat=ScreenSpaceMatrix();
				int intTooth=ToothGraphic.IdToInt(toothGraphics[t].ToothID);
				if(lingual){
					DrawFurcationTriangle(intTooth,PerioSurf.DL,maxillary,toothLineMat);
					DrawFurcationTriangle(intTooth,PerioSurf.L,maxillary,toothLineMat);
					DrawFurcationTriangle(intTooth,PerioSurf.ML,maxillary,toothLineMat);					
				}else{//buccal
					DrawFurcationTriangle(intTooth,PerioSurf.DB,maxillary,toothLineMat);
					DrawFurcationTriangle(intTooth,PerioSurf.B,maxillary,toothLineMat);
					DrawFurcationTriangle(intTooth,PerioSurf.MB,maxillary,toothLineMat);
				}
			}
			//The device.Transform.World matrix must be set before calling Line.Begin()
			//or else your lines end up in the wrong location! This is odd behavior, since you *MUST*
			//pass in your screen matrix when you call Line.DrawTransform(). This must be a DirectX bug.
			device.Transform.World=orientation;
			Matrix lineMat=ScreenSpaceMatrix();
			const float leftX=-65f;
			const float rightX=65f;
			const int mmMax=9;
			//Draw the horizontal line at 0
			line.Antialias=false;
			line.Width=1.5f;
			line.Begin();
			Vector3 p1=new Vector3(leftX,0,0);
			Vector3 p2=new Vector3(rightX,0,0);
			line.DrawTransform(
				new Vector3[] { p1,p2 },
				lineMat,TcData.ColorText);
			line.End();
			//Draw the other horizontal lines
			line=new Line(device);
			line.Antialias=false;
			line.Width=1f;
			line.Begin();
			for(int mm=3;mm<=mmMax;mm+=3) {
				p1=new Vector3(leftX,sign*mm,0);
				p2=new Vector3(rightX,sign*mm,0);
				line.DrawTransform(
					new Vector3[] { p1,p2 },
					lineMat,Color.Gray);
			}
			line.End();
			line.Dispose();
		}

		private void DrawFurcationTriangle(int intTooth,PerioSurf perioSurf,bool maxillary,Matrix toothLineMat){
			int furcationValue=TcData.GetFurcationValue(intTooth,perioSurf);
			if(furcationValue<1||furcationValue>3) {
				return;
			}
			PointF sitePos=TcData.GetFurcationPos(intTooth,perioSurf);
			float sign=maxillary?1:-1;
			const float triSideLenMM=2f;
			Color triColor=Color.Black;
			List<Vector3> triPoints=new List<Vector3>();
			//We form an equilateral triangle.
			triPoints.Add(new Vector3(sitePos.X+triSideLenMM/2f,sitePos.Y+sign*((float)(triSideLenMM*Math.Sqrt(3)/2f)),0));
			triPoints.Add(new Vector3(sitePos.X,sitePos.Y,0));
			triPoints.Add(new Vector3(sitePos.X-triSideLenMM/2f,sitePos.Y+sign*((float)(triSideLenMM*Math.Sqrt(3)/2f)),0));
			if(furcationValue==1) {
				DrawExtended3dLine(new Vector3[] { triPoints[0],triPoints[1],triPoints[2] },0.1f,false,triColor,2f,toothLineMat);
			} else if(furcationValue==2) {
				DrawExtended3dLine(new Vector3[] { triPoints[0],triPoints[1],triPoints[2],triPoints[0] },0.1f,true,triColor,2f,toothLineMat);
			} else if(furcationValue==3) {
				CustomVertex.PositionColored[] solidTriVerts=new CustomVertex.PositionColored[] {
			          new CustomVertex.PositionColored(triPoints[0],triColor.ToArgb()),
			          new CustomVertex.PositionColored(triPoints[1],triColor.ToArgb()),
			          new CustomVertex.PositionColored(triPoints[2],triColor.ToArgb()),
			        };
				VertexBuffer triVb=new VertexBuffer(typeof(CustomVertex.PositionColored),
					CustomVertex.PositionColored.StrideSize*solidTriVerts.Length,
					device,Usage.WriteOnly,CustomVertex.PositionColored.Format,Pool.Managed);
				triVb.SetData(solidTriVerts,0,LockFlags.None);
				int[] triIndicies=new int[] { 0,1,2 };
				IndexBuffer triIb=new IndexBuffer(typeof(int),triIndicies.Length,device,Usage.None,Pool.Managed);
				triIb.SetData(triIndicies,0,LockFlags.None);
				device.VertexFormat=CustomVertex.PositionColored.Format;
				device.SetStreamSource(0,triVb,0);
				device.Indices=triIb;
				device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,solidTriVerts.Length,0,triIndicies.Length/3);
				triIb.Dispose();
				triVb.Dispose();
			} else {
				//invalid value. assume no furcation.
			}
		}

		///<summary>Draws a line strip extending the two point lines which to not include endpoints. 
		///Set extendEndPoints to true to extend the endpoints of the line.</summary>
		private void DrawExtended3dLine(Vector3[] points,float extendDist,bool extendEndPoints,Color color,float lineWidth,Matrix transform){
			//Convert each line strip into very simple two point lines so that line extensions can be calculated more easily below.
			//Items in the array are tuples of (2D point,bool indicating end point).
			List<object> twoPointLines=new List<object>();
			for(int p=0;p<points.Length-1;p++) {
				twoPointLines.Add(points[p]);
				twoPointLines.Add(p==0);
				twoPointLines.Add(points[p+1]);
				twoPointLines.Add(p==points.Length-2);
			}
			Line line=new Line(device);
			line.Antialias=false;
			line.Width=lineWidth;
			line.Begin();
			//Draw each individual two point line. The lines must be broken down from line strips so that when individual two point
			//line locations are modified they do not affect any other two point lines within the same line strip.
			for(int j=0;j<twoPointLines.Count;j+=4) {
				Vector3 p1=(Vector3)twoPointLines[j];
				bool p1IsEndPoint=(bool)twoPointLines[j+1];
				Vector3 p2=(Vector3)twoPointLines[j+2];
				bool p2IsEndPoint=(bool)twoPointLines[j+3];
				Vector3 lineDir=p2-p1;
				lineDir.Normalize();//Gives the line direction a single unit length.
				//Do not extend the endpoints for the ends of the line strips unless extendEndPoints=true.
				if(!p1IsEndPoint || extendEndPoints) {
					p1=p1-extendDist*lineDir;
				}
				//Do not extend the endpoints for the ends of the line strips unless extendEndPoints=true.
				if(!p2IsEndPoint || extendEndPoints) {
					p2=p2+extendDist*lineDir;
				}
				Vector3[] lineVerts=new Vector3[] { p1,p2 };
				line.DrawTransform(lineVerts,transform,color);
			}
			line.End();
			line.Dispose();
		}

		private void DrawFacialView(ToothGraphic toothGraphic,Matrix defOrient) {
			Matrix toothTrans=Matrix.Identity;
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
			line.GlLines=true;
			if(toothGraphic.DrawBigX) {
				//Thickness of line depends on size of window.
				//The line size needs to be slightly larger than in OpenGL because
				//lines are drawn with polygons in DirectX and they are anti-aliased,
				//even when the line.Antialias flag is set.
				line.Width=2.2f*TcData.PixelScaleRatio;
				line.Begin();
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
				line.End();
			}
			if(toothGraphic.Visible && toothGraphic.IsRCT) {//draw RCT
				//Thickness of lines depend on size of window.
				//The line size needs to be slightly larger than in OpenGL because
				//lines are drawn with polygons in DirectX and they are anti-aliased,
				//even when the line.Antialias flag is set.
				line.Width=2.5f*TcData.PixelScaleRatio;
				line.Begin();
				List<LineSimple> linesSimple=toothGraphic.GetRctLines();
				for(int i=0;i<linesSimple.Count;i++) {
					if(linesSimple[i].Vertices.Count<2){
						continue;//Just to avoid internal errors, even though not likely.
					}
					//Convert each line strip into very simple two point lines so that line extensions can be calculated more easily below.
					//Items in the array are tuples of (2D point,bool indicating end point).
					List <object> twoPointLines=new List<object> ();
					for(int j=0;j<linesSimple[i].Vertices.Count-1;j++){
					  twoPointLines.Add(new Vector3(
							linesSimple[i].Vertices[j  ].X,
							linesSimple[i].Vertices[j  ].Y,
							linesSimple[i].Vertices[j  ].Z));
						twoPointLines.Add(j==0);
					  twoPointLines.Add(new Vector3(
							linesSimple[i].Vertices[j+1].X,
							linesSimple[i].Vertices[j+1].Y,
							linesSimple[i].Vertices[j+1].Z));
						twoPointLines.Add(j==linesSimple[i].Vertices.Count-2);
					}
					//Draw each individual two point line. The lines must be broken down from line strips so that when individual two point
					//line locations are modified they do not affect any other two point lines within the same line strip.
					for(int j=0;j<twoPointLines.Count;j+=4){
					  Vector3 p1=(Vector3)twoPointLines[j];
						bool p1IsEndPoint=(bool)twoPointLines[j+1];
					  Vector3 p2=(Vector3)twoPointLines[j+2];
						bool p2IsEndPoint=(bool)twoPointLines[j+3];
					  Vector3 lineDir=p2-p1;
					  lineDir.Normalize();//Gives the line direction a single unit length.
					  float extSize=0.25f;//The number of units to extend each end of the two point line.
						if(!p1IsEndPoint){//Do not extend the endpoints for the ends of the line strips.
							p1=p1-extSize*lineDir;
						}
						if(!p2IsEndPoint){//Do not extend the endpoints for the ends of the line strips.
							p2=p2+extSize*lineDir;
						}
					  Vector3[] lineVerts=new Vector3[] {p1,p2};
					  line.DrawTransform(lineVerts,lineMatrix,toothGraphic.colorRCT);
					}
				}
				line.End();
			}
			ToothGroup groupBU=toothGraphic.GetGroup(ToothGroupType.Buildup);//during debugging, not all teeth have a BU group yet.
			if(toothGraphic.Visible && groupBU!=null && groupBU.Visible) {//BU or Post
				device.RenderState.ZBufferEnable=false;
				device.RenderState.Lighting=true;
				device.Lights[0].Enabled=false;//Disable the scene light.
				device.Lights[1].Ambient=Color.White;
				device.Lights[1].Enabled=true;
				Color colorBU=toothGraphic.GetGroup(ToothGroupType.Buildup).PaintColor;
				device.VertexFormat=CustomVertex.PositionNormal.Format;
				device.SetStreamSource(0,toothGraphic.vb,0);
				Material material=new Material();
				material.Ambient=colorBU;
				device.Material=material;
				device.Indices=groupBU.facesDirectX;
				device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,toothGraphic.VertexNormals.Count,0,groupBU.NumIndicies/3);
				device.Lights[0].Enabled=true;
				device.Lights[1].Enabled=false;
			}
			if(toothGraphic.IsImplant) {
				DrawImplant(toothGraphic);
			}
			line.Dispose();
			device.RenderState.ZBufferEnable=true;
			device.RenderState.Lighting=true;
		}

		private void DrawImplant(ToothGraphic toothGraphic){
			device.RenderState.ZBufferEnable=true;
			device.RenderState.Lighting=true;
			if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
				//flip the implant upside down
				device.Transform.World=Matrix.RotationZ((float)Math.PI)*device.Transform.World;
			}
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
				if(!group.Visible||group.GroupType==ToothGroupType.Buildup) {
					continue;
				}
				device.Indices=group.facesDirectX;
				device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,implantGraphic.VertexNormals.Count,0,group.NumIndicies/3);
			}
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
			device.RenderState.ZBufferEnable=false;
			device.RenderState.Lighting=false;
			float toMm=1f/TcData.ScaleMmToPix;
			if(toothGraphic.Visible && toothGraphic.IsSealant) {//draw sealant
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)){
					PrintString("S",TcData.PixelScaleRatio*(-6f*toMm),TcData.PixelScaleRatio*(-100f*toMm),-6f,toothGraphic.colorSealant,xSealantFont);
				}else{
					PrintString("S",TcData.PixelScaleRatio*(-6f*toMm),TcData.PixelScaleRatio*(22f*toMm),-6f,toothGraphic.colorSealant,xSealantFont);
				}
			}
			device.RenderState.ZBufferEnable=true;
			device.RenderState.Lighting=true;
		}

		private Material GetGroupMaterial(ToothGraphic toothGraphic,ToothGroup group){
			Material material=new Material();
			Color materialColor;
			if(toothGraphic.ShiftO<-10) {//if unerupted
				materialColor=Color.FromArgb(group.PaintColor.A/2,group.PaintColor.R/2,group.PaintColor.G/2,group.PaintColor.B/2);
			} else {
				materialColor=group.PaintColor;
			}
			material.Ambient=materialColor;
			material.Diffuse=materialColor;
			if(group.GroupType==ToothGroupType.Cementum) {
				material.Specular=specular_color_cementum;
			} else if(group.PaintColor.R>245&&group.PaintColor.G>245&&group.PaintColor.B>235) {
				//because DirectX washes out the specular on the enamel, we have to turn it down only for the enamel color
				//for reference, this is the current enamel color: Color.FromArgb(255,250,250,240)
				float specEnamel=.4f;
				material.Specular=Color.FromArgb(255,(int)(255*specEnamel),(int)(255*specEnamel),(int)(255*specEnamel));
			} else {
				material.Specular=specular_color_normal;
			}
			material.SpecularSharpness=specularSharpness;
			return material;
		}

		private void DrawTooth(ToothGraphic toothGraphic) {
			ToothGroup group;
			device.VertexFormat=CustomVertex.PositionNormal.Format;
			device.SetStreamSource(0,toothGraphic.vb,0);
			for(int g=0;g<toothGraphic.Groups.Count;g++) {
				group=(ToothGroup)toothGraphic.Groups[g];
				if(!group.Visible || group.facesDirectX==null || 
					group.GroupType==ToothGroupType.Buildup || group.GroupType==ToothGroupType.None) {
					continue;
				}
				device.Material=GetGroupMaterial(toothGraphic,group);
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
			Line centerLine=new Line(device);
			centerLine.Width=1f*TcData.PixelScaleRatio;
			centerLine.Antialias=false;
			centerLine.Begin();//Must call Line.Begin() in order for Antialias=false to take effect.
			centerLine.Draw(new Vector2[] {
				new Vector2(-1,this.Height/2),
				new Vector2(this.Width,this.Height/2)},
				Color.White);
			centerLine.End();
			//Draw the tooth numbers.
			string tooth_id;
			for(int i=1;i<=52;i++) {
				tooth_id=Tooth.FromOrdinal(i);
				if(TcData.SelectedTeeth.Contains(tooth_id)) {
					DrawNumber(tooth_id,true,0);
				} 
				else {
					DrawNumber(tooth_id,false,0);
				}
			}
			//TimeSpan displayTime=(frameEndTime-frameBeginTime);
			//float fps=1000f/displayTime.Milliseconds;
			//this.PrintString(fps.ToString(),0,0,0,Color.Blue,xfont);
		}

		private void DrawNumbersAndLinesPerio() {
			device.RenderState.Lighting=false;
			device.RenderState.ZBufferEnable=false;
			//Draw the center line.
			Line centerLine=new Line(device);
			centerLine.Width=2.5f;
			centerLine.Antialias=false;
			centerLine.Begin();//Must call Line.Begin() in order for Antialias=false to take effect.
			centerLine.Draw(new Vector2[] {
				new Vector2(-1,this.Height/2),
				new Vector2(this.Width,this.Height/2)},
				Color.Black);
			centerLine.End();
			//Draw the tooth numbers.
			string tooth_id;
			for(int i=1;i<=32;i++) {
				tooth_id=Tooth.FromOrdinal(i);
				//bool isSelected=TcData.SelectedTeeth.Contains(tooth_id);
				float yOffset=ToothGraphic.IsMaxillary(tooth_id)?30:-29;
				DrawNumber(tooth_id,false,yOffset);
			}
		}

		///<summary>Draws the number and the small rectangle behind it.  Draws in the appropriate color.  isFullRedraw means that all of the toothnumbers are being redrawn.  This helps with a few optimizations and with not painting blank rectangles when not needed.</summary>
		private void DrawNumber(string tooth_id,bool isSelected,float offsetY) {
			if(!Tooth.IsValidDB(tooth_id)) {
				return;
			}
			if(TcData.ListToothGraphics[tooth_id].HideNumber) {//if this is a "hidden" number
				return;//skip
			}
			//primary, but not set to show primary letters
			if(Tooth.IsPrimary(tooth_id) && !TcData.ListToothGraphics[Tooth.PriToPerm(tooth_id)].ShowPrimaryLetter){
				return;
			}
			device.RenderState.Lighting=false;
			device.RenderState.ZBufferEnable=false;
			device.Transform.World=Matrix.Identity;
			string displayNum=Tooth.GetToothLabelGraphic(tooth_id,TcData.ToothNumberingNomenclature);
			SizeF labelSize=MeasureStringMm(displayNum);
			RectangleF recMm=TcData.GetNumberRecMm(tooth_id,labelSize);
			recMm.Y+=offsetY;
			Color foreColor=TcData.ColorText;
			if(isSelected) {
				foreColor=TcData.ColorTextHighlight;
				//Draw the background rectangle only if the text is selected.
				int backColorARGB=TcData.ColorBackHighlight.ToArgb();
				CustomVertex.PositionColored[] quadVerts=new CustomVertex.PositionColored[] {
			    new CustomVertex.PositionColored(recMm.X,recMm.Y,0,backColorARGB),//LL
			    new CustomVertex.PositionColored(recMm.X,recMm.Y+recMm.Height,0,backColorARGB),//UL
			    new CustomVertex.PositionColored(recMm.X+recMm.Width,recMm.Y+recMm.Height,0,backColorARGB),//UR
			    new CustomVertex.PositionColored(recMm.X+recMm.Width,recMm.Y,0,backColorARGB),//LR
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
			}
			//Offsets the text by 10% of the rectangle width to ensure that there is at least on pixel of space on
			//the left for the background rectangle.
			PrintString(displayNum,recMm.X+recMm.Width*0.1f,recMm.Y+recMm.Height,0,foreColor,xfont);
		}

		private SizeF MeasureStringMm(string text) {
			Rectangle rectSize=xfont.MeasureString(null,text,DrawTextFormat.VerticalCenter,Color.White);
			//DirectX appears to add more vertical spacing than GDI+. We scale the height here to 80% as a result.
			//DirectX does not appear to add horizontal spacing into the width consideration. As a result, we widen 
			//the output by 25% to ensure that the highlight border around the text has a border all the way around.
			return new SizeF((rectSize.Width*1.25f)/TcData.ScaleMmToPix,(rectSize.Height*0.8f)/TcData.ScaleMmToPix);
		}

		private void PrintString(string text,float x,float y,float z,Color color,Microsoft.DirectX.Direct3D.Font printFont) {
			Vector3 screenPoint=new Vector3(x,y,z);
			screenPoint.Project(device.Viewport,device.Transform.Projection,device.Transform.View,device.Transform.World);
			printFont.DrawText(null,text,new Point((int)Math.Ceiling(screenPoint.X),(int)Math.Floor(screenPoint.Y)),color);
		}

		private void DrawDrawingSegments() {
			device.RenderState.Lighting=false;
			device.RenderState.ZBufferEnable=false;
			device.Transform.World=Matrix.Identity;
			Matrix lineMatrix=ScreenSpaceMatrix();
			Line line=new Line(device);
			line.Width=2.2f*TcData.PixelScaleRatio;
			line.Begin();
			for(int s=0;s<TcData.DrawingSegmentList.Count;s++) {				
				string[] pointStr=TcData.DrawingSegmentList[s].DrawingSegment.Split(';');
				List<Vector3> points=new List<Vector3>();
				for(int p=0;p<pointStr.Length;p++) {
					string[] xy=pointStr[p].Split(',');
					if(xy.Length==2) {
						Point point=new Point((int)(float.Parse(xy[0])),(int)(float.Parse(xy[1])));
						//if we set 0,0 to center, then this is where we would convert it back.
						PointF pointMm=TcData.PointDrawingPixToMm(point);
						points.Add(new Vector3(pointMm.X,pointMm.Y,0f));
					}
				}
				//Convert each line strip into very simple two point lines so that line extensions can be calculated more easily below.
				List<Vector3> twoPointLines=new List<Vector3>();
				for(int j=0;j<points.Count-1;j++) {
					twoPointLines.Add(new Vector3(
						points[j].X,
						points[j].Y,
						points[j].Z));
					twoPointLines.Add(new Vector3(
						points[j+1].X,
						points[j+1].Y,
						points[j+1].Z));
				}
				//Draw each individual two point line. The lines must be broken down from line strips so that when individual two point
		    //line locations are modified they do not affect any other two point lines within the same line strip.
				//All lines are expanded on both sides here, because the drawing could end with a loop and the loop must be closed.
		    for(int j=0;j<twoPointLines.Count;j+=2){
		      Vector3 p1=(Vector3)twoPointLines[j];
		      Vector3 p2=(Vector3)twoPointLines[j+1];
		      Vector3 lineDir=p2-p1;
		      lineDir.Normalize();//Gives the line direction a single unit length.
		      float extSize=0.25f;//The number of units to extend each end of the two point line.
					p1=p1-extSize*lineDir;
					p2=p2+extSize*lineDir;
		      Vector3[] lineVerts=new Vector3[] {p1,p2};
					line.DrawTransform(lineVerts,lineMatrix,TcData.DrawingSegmentList[s].ColorDraw);
		    }
				//no filled circle at intersections
			}
			//Draw the points that make up the segment which is currently being drawn
			//but which has not yet been sent to the database.
			for(int p=1;p<TcData.PointList.Count;p++){
				PointF pMm1=TcData.PointPixToMm(TcData.PointList[p]);
				PointF pMm2=TcData.PointPixToMm(TcData.PointList[p-1]);
				line.DrawTransform(new Vector3[] {
						new Vector3(pMm1.X,pMm1.Y,0f),
						new Vector3(pMm2.X,pMm2.Y,0f)},
					lineMatrix,
					TcData.ColorDrawing);
			}
			line.End();
			line.Dispose();
		}

		///<summary>Returns a bitmap of what is showing in the control.  Used for printing.</summary>
		public Bitmap GetBitmap() {
			Surface backBuffer=device.GetBackBuffer(0,0,BackBufferType.Mono);
			GraphicsStream gs=SurfaceLoader.SaveToStream(ImageFileFormat.Bmp,backBuffer);
			Bitmap bitmap=new Bitmap(gs);
			backBuffer.Dispose();
			return bitmap;
		}

		private void ToothChartDirectX_MouseDown(object sender,MouseEventArgs e) {
			MouseIsDown=true;
			if(TcData.CursorTool==CursorTool.Pointer) {
				string toothClicked=TcData.GetToothAtPoint(e.Location);
				if(TcData.SelectedTeeth.Contains(toothClicked)) {
					SetSelected(toothClicked,false);
				} else {
					SetSelected(toothClicked,true);
				}
				Invalidate();
				Application.DoEvents();//Force redraw.

			} else if(TcData.CursorTool==CursorTool.Pen) {
				TcData.PointList.Add(new Point(e.X,e.Y));
			} else if(TcData.CursorTool==CursorTool.Eraser) {
				//do nothing
			} else if(TcData.CursorTool==CursorTool.ColorChanger) {
				//look for any lines near the "wand".
				//since the line segments are so short, it's sufficient to check end points.
				string[] xy;
				string[] pointStr;
				float x;
				float y;
				float dist;//the distance between the point being tested and the center of the eraser circle.
				float radius=2f;//by trial and error to achieve best feel.
				for(int i=0;i<TcData.DrawingSegmentList.Count;i++) {
					pointStr=TcData.DrawingSegmentList[i].DrawingSegment.Split(';');
					for(int p=0;p<pointStr.Length;p++) {
						xy=pointStr[p].Split(',');
						if(xy.Length==2) {
							x=float.Parse(xy[0]);
							y=float.Parse(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-e.X),2)+Math.Pow(Math.Abs(y-e.Y),2));
							if(dist<=radius) {//testing circle intersection here
								OnSegmentDrawn(TcData.DrawingSegmentList[i].DrawingSegment);
								TcData.DrawingSegmentList[i].ColorDraw=TcData.ColorDrawing;
								Invalidate();
								return; ;
							}
						}
					}
				}
			}
		}

		private void ToothChartDirectX_MouseMove(object sender,MouseEventArgs e) {
			if(TcData.CursorTool==CursorTool.Pointer) {
				hotTooth=TcData.GetToothAtPoint(e.Location);
				if(hotTooth==hotToothOld) {//mouse has not moved to another tooth
					return;
				}
				if(MouseIsDown) {//drag action
					List<string> affectedTeeth=TcData.GetAffectedTeeth(hotToothOld,hotTooth,TcData.PointPixToMm(e.Location).Y);
					for(int i=0;i<affectedTeeth.Count;i++) {
						if(TcData.SelectedTeeth.Contains(affectedTeeth[i])) {
							SetSelected(affectedTeeth[i],false);
						}
						else {
							SetSelected(affectedTeeth[i],true);
						}
					}
					hotToothOld=hotTooth;
					Invalidate();
					Application.DoEvents();
				}
				else {
					hotToothOld=hotTooth;
				}
			} 
			else if(TcData.CursorTool==CursorTool.Pen) {
				if(!MouseIsDown) {
					return;
				}
				TcData.PointList.Add(new Point(e.X,e.Y));
				Invalidate();
				Application.DoEvents();
			} 
			else if(TcData.CursorTool==CursorTool.Eraser) {
				if(!MouseIsDown) {
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
				for(int i=0;i<TcData.DrawingSegmentList.Count;i++) {
					pointStr=TcData.DrawingSegmentList[i].DrawingSegment.Split(';');
					for(int p=0;p<pointStr.Length;p++) {
						xy=pointStr[p].Split(',');
						if(xy.Length==2) {
							x=float.Parse(xy[0]);
							y=float.Parse(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-eraserPt.X),2)+Math.Pow(Math.Abs(y-eraserPt.Y),2));
							if(dist<=radius) {//testing circle intersection here
								OnSegmentDrawn(TcData.DrawingSegmentList[i].DrawingSegment);//triggers a deletion from db.
								TcData.DrawingSegmentList.RemoveAt(i);
								Invalidate();
								Application.DoEvents();
								return; ;
							}
						}
					}
				}
			} 
			else if(TcData.CursorTool==CursorTool.ColorChanger) {
				//do nothing	
			}
		}

		private void ToothChartDirectX_MouseUp(object sender,MouseEventArgs e) {
			MouseIsDown=false;
			if(TcData.CursorTool==CursorTool.Pen) {
				string drawingSegment="";
				for(int i=0;i<TcData.PointList.Count;i++) {
					if(i>0) {
						drawingSegment+=";";
					}
					//I could compensate to center point here:
					drawingSegment+=TcData.PointList[i].X+","+TcData.PointList[i].Y;
				}
				OnSegmentDrawn(drawingSegment);
				TcData.PointList=new List<Point>();
				//Invalidate();
			} else if(TcData.CursorTool==CursorTool.Eraser) {
				//do nothing
			} else if(TcData.CursorTool==CursorTool.ColorChanger) {
				//do nothing
			}
		}

		///<summary></summary>
		protected void OnSegmentDrawn(string drawingSegment) {
			ToothChartDrawEventArgs tArgs=new ToothChartDrawEventArgs(drawingSegment);
			if(SegmentDrawn!=null) {
				SegmentDrawn(this,tArgs);
			}
		}

		///<summary>Used by mousedown and mouse move to set teeth selected or unselected.  A similar method is used externally in the wrapper to set teeth selected.  This private method might be faster since it only draws the changes.</summary>
		private void SetSelected(string tooth_id,bool setValue) {
			TcData.SetSelected(tooth_id,setValue);
			if(setValue) {
				DrawNumber(tooth_id,true,0);
			} else {
				DrawNumber(tooth_id,false,0);
			}
		}

	}
}
