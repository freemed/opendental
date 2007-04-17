using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.UI {
	public partial class PanelOD:System.Windows.Forms.Panel {
		private Color borderColor;

		public PanelOD() {
			InitializeComponent();
			borderColor=Color.Silver;
		}

		///<summary>The color of the border.</summary>
		[Category("Appearance"),Description("The color of the border.")]
		[DefaultValue(typeof(Color),"Silver")]
		public Color BorderColor {
			get {
				return borderColor;
			}
			set {
				borderColor=value;
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			e.Graphics.FillRectangle(new SolidBrush(BackColor),0,0,Width,Height);
			e.Graphics.DrawRectangle(new Pen(borderColor),0,0,Width-1,Height-1);
		}

		protected override void OnResize(EventArgs eventargs) {
			base.OnResize(eventargs);
			this.Invalidate();
		}

	}
}
