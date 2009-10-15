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

		public ToothChartDirectX() {
			InitializeComponent();
			InitializeGraphics();
		}

		public virtual void InitializeGraphics(){
			PresentParameters pp=new PresentParameters();
			pp.Windowed=true;
			pp.SwapEffect=SwapEffect.Discard;
			pp.EnableAutoDepthStencil=true;
			pp.AutoDepthStencilFormat=DepthFormat.D16;
			pp.DeviceWindowHandle=this.Handle;
			device=new Device(0,DeviceType.Hardware,this,CreateFlags.HardwareVertexProcessing,pp);
			device.DeviceReset+=new EventHandler(this.OnDeviceReset);
			OnDeviceReset(device,null);
		}

		///<summary>TODO: Handle the situation when there are suboptimal graphics cards.</summary>
		public virtual void OnDeviceReset(object sender,EventArgs e){
			device=sender as Device;
		}

		protected void Render(){
			//TODO: Render the tooth chart using DirectX commands. 
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
