using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CodeBase {
	public partial class PrintPanel:UserControl {
		private Bitmap backImage;
		///<summary>Draw to this back buffer, then the window display will take care of the rest.</summary>
		public Graphics backBuffer;
		public Point printOrigin=new Point(0,0);

		public PrintPanel() {
			InitializeComponent();
			backImage=new Bitmap(this.Width,this.Height);
			backBuffer=Graphics.FromImage(backImage);
		}

		private void PrintPanel_SizeChanged(object sender,EventArgs e) {
			//This is required so that panelSurface_SizeChanged is called and so that the print area
			//remains the same as for the entire printpanel control (so no background is visible).
			panelSurface.Size=this.Size;
		}

		private void panelSurface_Paint(object sender,PaintEventArgs e) {
			e.Graphics.DrawImageUnscaled(backImage,printOrigin);
		}

		private void panelSurface_SizeChanged(object sender,EventArgs e) {
			//We must resize the back buffer image if the size of the control increases so that anything drawn on
			//this panel will be saved in the back buffer. If the size of the control shrinks, we do nothing,
			//so that we can preserve any printed information.
			Bitmap newBuffer=null;
			if(this.Width>backImage.Width) {
				if(this.Height>=backImage.Height) {
					newBuffer=new Bitmap(this.Width,this.Height);
				} else {
					newBuffer=new Bitmap(this.Width,backImage.Height);
				}
			} else if(this.Height>backImage.Height) {
				newBuffer=new Bitmap(backImage.Width,this.Height);
			}
			if(newBuffer!=null) {//The buffer was changed
				Graphics g=Graphics.FromImage(newBuffer);
				g.DrawImageUnscaled(backImage,new Point(0,0));
				backBuffer.Dispose();
				backImage.Dispose();
				backImage=newBuffer;
				backBuffer=g;
			}
		}

	}
}