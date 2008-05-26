using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace OpenDental.UI
{
	/// <summary>Better and simpler than the MS picturebox.  Always resizes the image to fit in the box.  Never crops or stretches.</summary>
	public class PictureBox : System.Windows.Forms.Control
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Image image;
		private string textNullImage;

		///<summary></summary>
		public PictureBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
		[Category("Appearance"),Description("The image displayed in the PictureBox.")]
		[DefaultValue(null)]
		public Image Image{
			set{
				image=value;
				Invalidate();
			}
			get{
				return image;
			}
		}

		///<summary></summary>
		[Category("Appearance"),Description("The text that will display if the image is null.")]
		public string TextNullImage{
			set{
				textNullImage=value;
				Invalidate();
			}
			get{
				return textNullImage;
			}
		}

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint (e);
			Graphics g=e.Graphics;
			g.InterpolationMode=InterpolationMode.High;
			g.DrawRectangle(Pens.Gray,0,0,Width-1,Height-1);
			if(image==null){
				StringFormat format=new StringFormat();
				format.Alignment=StringAlignment.Center;
				format.LineAlignment=StringAlignment.Center;
				g.DrawString(textNullImage,this.Font,new SolidBrush(Color.Gray),
					new RectangleF(0,0,Width,Height),format);
			}
			else{
				float ratio;
				//Debug.WriteLine("Hratio:"+(float)image.Height/(float)Height+"Wratio:"+(float)image.Width/(float)Width);
				if((float)image.Height/(float)Height > (float)image.Width/(float)Width){//Image is proportionally taller
					ratio=(float)Height/(float)image.Height;
					g.DrawImage(image,new RectangleF(Width/2-((float)image.Width*ratio)/2,0,(float)image.Width*ratio,Height));
				}
				else{//image proportionally wider
					ratio=(float)Width/(float)image.Width;
					g.DrawImage(image,new RectangleF(0,(float)Height/2-((float)image.Height*ratio)/2,Width,(float)image.Height*ratio));
				}
			}
		}

		///<summary></summary>
		protected override void OnResize(EventArgs e) {
			base.OnResize (e);
			Invalidate();
		}





	}


}
