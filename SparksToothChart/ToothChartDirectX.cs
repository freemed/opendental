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

		private Device device=null;
		private static List <ToothGraphic> ListToothGraphics=null;

		public ToothChartDirectX() {
			InitializeComponent();
			//Initialize the tooth models only once, so that loading can happen more quickly if it is foreced again.
			if(ListToothGraphics==null) {
				ListToothGraphics=new List<ToothGraphic> ();
				ToothGraphic tooth;
				for(int i=1;i<=32;i++) {
					tooth=new ToothGraphic(i.ToString());
					tooth.Visible=true;
					ListToothGraphics.Add(tooth);
					//primary
					//if(ToothGraphic.PermToPri(i.ToString())!="") {
					//  tooth=new ToothGraphic(ToothGraphic.PermToPri(i.ToString()));
					//  tooth.Visible=false;
					//  ListToothGraphics.Add(tooth);
					//}
				}
				//tooth=new ToothGraphic("implant");
				//ListToothGraphics.Add(tooth);
			} else {//list was already initially filled, but now user needs to reset it.
				for(int i=0;i<ListToothGraphics.Count;i++) {//loop through all perm and pri teeth.
					ListToothGraphics[i].Reset();
				}
			}
		}

		///<summary>Must be called after the ToothChartDirectX control has been added to a form and should be called before it is drawn the first time.</summary>
		public void InitializeGraphics(){
			PresentParameters pp=new PresentParameters();
			pp.Windowed=true;
			pp.SwapEffect=SwapEffect.Discard;
			pp.EnableAutoDepthStencil=true;
			pp.AutoDepthStencilFormat=DepthFormat.D16;//Z-buffer depth of 16 bits.
			pp.DeviceWindowHandle=this.Handle;
			device=new Device(0,DeviceType.Hardware,this,CreateFlags.SoftwareVertexProcessing,pp);
			device.DeviceReset+=new EventHandler(this.OnDeviceReset);
			device.RenderState.CullMode=Cull.CounterClockwise;
			device.RenderState.ZBufferEnable=true;
			device.RenderState.ZBufferFunction=Microsoft.DirectX.Direct3D.Compare.Less;
			device.RenderState.Lighting=false;
			device.RenderState.SpecularEnable=false;
			device.Lights[0].Type=LightType.Directional;
			device.Lights[0].Position=new Vector3(0,100,0);
			device.Lights[0].Range=10000.0f;
			//device.Lights[0].Attenuation0=0.01f;
			//device.Lights[0].Attenuation1=0.1f;
			device.Lights[0].Attenuation2=0.01f;
			device.Lights[0].Ambient=System.Drawing.Color.Red;
			device.Lights[0].Diffuse=System.Drawing.Color.Green;
			device.Lights[0].Specular=System.Drawing.Color.Blue;
			device.Lights[0].Direction=new Vector3(1,-1,1);
			device.Lights[0].Enabled=true;
			device.RenderState.Ambient=System.Drawing.Color.FromArgb(0x808080);
			device.RenderState.AlphaTestEnable=false;
			//device.RenderState.AlphaFunction=Microsoft.DirectX.Direct3D.Compare.Always;
			//device.RenderState.SourceBlend=Microsoft.DirectX.Direct3D.Blend.SourceAlpha;
			//device.RenderState.DestinationBlend=Microsoft.DirectX.Direct3D.Blend.DestinationAlpha;
			//device.RenderState.AlphaBlendEnable=false;
			//device.RenderState.AlphaBlendOperation=Microsoft.DirectX.Direct3D.BlendOperation.Add;
			OnDeviceReset(device,null);
			for(int i=0;i<ListToothGraphics.Count;i++) {
				ToothGraphic tooth=ListToothGraphics[i];
				tooth.PrepareForDirextX(device);
				for(int j=0;j<tooth.Groups.Count;j++) {
					ToothGroup group=tooth.Groups[j];
					group.PrepareForDirectX(device);
				}
			}
		}

		///<summary>TODO: Handle the situation when there are suboptimal graphics cards.</summary>
		public void OnDeviceReset(object sender,EventArgs e){
			device=sender as Device;
		}

		protected void Render(){
			Vector3 cameraPos=new Vector3(0,0,-200);
			device.Transform.View=Matrix.LookAtLH(cameraPos,new Vector3(cameraPos.X,cameraPos.Y,cameraPos.Z+1),new Vector3(0.0f,1.0f,0.0f));
			device.Transform.Projection=Matrix.PerspectiveFovLH((float)Math.PI/4,1.0f,1.0f,10000.0f);
			device.VertexFormat=CustomVertex.PositionNormal.Format;
			for(int i=0;i<ListToothGraphics.Count;i++){
				ToothGraphic tooth=ListToothGraphics[i];
				device.SetStreamSource(0,tooth.VertexBuffer,0);
				Matrix toothOrient=Matrix.Identity;
				int toothNum=ToothGraphic.IdToInt(tooth.ToothID);
				Vector3 toothTrans;
				const float toothSpaceWidth=10;
				if(toothNum<17){//Upper
					toothTrans=new Vector3((toothNum-8.5f)*toothSpaceWidth,12,0);
				}else{//Lower
					toothTrans=new Vector3((24.5f-toothNum)*toothSpaceWidth,-12,0);
				}
				toothOrient.Translate(toothTrans);
				device.Transform.World=toothOrient;
				for(int j=0;j<tooth.Groups.Count;j++){
					ToothGroup group=tooth.Groups[j];
					device.Indices=group.facesDirectX;
					device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,tooth.VertexNormals.Count,0,group.NumIndicies/3);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs pe) {
			if(device==null){
				//When no rendering context has been set, simply display the control
				//as a black rectangle. This will make the control draw as a blank
				//rectangle when in the designer. 
				pe.Graphics.FillRectangle(Brushes.Black,new Rectangle(0,0,Width,Height));
				return;
			}
			device.Clear(ClearFlags.Target | ClearFlags.ZBuffer,Color.Black,1.0f,0);
			device.BeginScene();
			Render();
			device.EndScene();
			device.Present();
		}

		protected override void OnPaintBackground(PaintEventArgs e) {
			//Do nothing to eliminate flicker. 
		}

		protected override void OnSizeChanged(EventArgs e) {
			Invalidate();//Force the control to redraw. 
		}

	}
}
