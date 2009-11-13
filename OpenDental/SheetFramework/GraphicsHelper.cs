using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace OpenDental {
	public class GraphicsHelper {
		///<summary>This line spacing is specifically picked to match the RichTextBox.</summary>
		private static float lineSpacing=1.05f;
		private static int topPad=2;
		private static int rightPad=5;//helps get measurements better.
		//private static float hScale=.983f;

		///<summary>Since Graphics doesn't have a line height property.  The second graphics object is used for measurement purposes.</summary>
		public static void DrawString(Graphics g,Graphics gfx,string str,Font font,Brush brush,Rectangle bounds) {
			SizeF fit=new SizeF(bounds.Width-rightPad,font.Height);
			StringFormat format=StringFormat.GenericTypographic;
			float pixelsPerLine=lineSpacing * (float)font.Height;
			float lineIdx=0;
			int chars;
			int lines;
			RectangleF layoutRectangle;
			float layoutH;
			for(int ix=0;ix<str.Length;ix+=chars) {
				if(bounds.Y+topPad+pixelsPerLine*lineIdx>bounds.Bottom) {
					break;
				}
				gfx.MeasureString(str.Substring(ix),font,fit,format,out chars,out lines);
				if(bounds.Y+topPad+pixelsPerLine*lineIdx+font.Height > bounds.Bottom) {
					layoutH=bounds.Bottom-(bounds.Y+topPad+pixelsPerLine*lineIdx);
				}
				else {
					layoutH=font.Height+2;
				}
				layoutRectangle=new RectangleF(
					bounds.X,
					(float)(bounds.Y+topPad+pixelsPerLine*lineIdx),
					bounds.Width+50,//any amount of extra padding here will not cause malfunction
					layoutH);
				g.DrawString(str.Substring(ix,chars),font,brush,layoutRectangle);
				lineIdx+=1;
			}
		}

		///<summary>The pdfSharp version of drawstring.  g is used for measurement.  scaleToPix scales xObjects to pixels.</summary>
		public static void DrawStringX(XGraphics xg,Graphics g,double scaleToPix,string str,XFont xfont,XBrush xbrush,XRect xbounds) {
			//There are two coordinate systems here: pixels (used by us) and points (used by PdfSharp).
			//MeasureString and ALL related measurement functions must use pixels.
			//DrawString is the ONLY function that uses points.
			//pixels:
			Rectangle bounds=new Rectangle((int)(scaleToPix*xbounds.Left),
				(int)(scaleToPix*xbounds.Top),
				(int)(scaleToPix*xbounds.Width),
				(int)(scaleToPix*xbounds.Height));
			FontStyle fontstyle=FontStyle.Regular;
			if(xfont.Style==XFontStyle.Bold) {
				fontstyle=FontStyle.Bold;
			}
			//pixels: (except Size is em-size)
			Font font=new Font(xfont.Name,(float)xfont.Size,fontstyle);
			//pixels:
			SizeF fit=new SizeF((float)(bounds.Width-rightPad),(float)(font.Height));
			StringFormat format=StringFormat.GenericTypographic;
			//pixels:
			float pixelsPerLine=lineSpacing * (float)font.Height;
			float lineIdx=0;
			int chars;
			int lines;
			//points:
			RectangleF layoutRectangle;
			for(int ix=0;ix<str.Length;ix+=chars) {
				if(bounds.Y+topPad+pixelsPerLine*lineIdx>bounds.Bottom) {
					break;
				}
				//pixels:
				g.MeasureString(str.Substring(ix),font,fit,format,out chars,out lines);
				//PdfSharp isn't smart enough to cut off the lower half of a line.
				//if(bounds.Y+topPad+pixelsPerLine*lineIdx+font.Height > bounds.Bottom) {
				//	layoutH=bounds.Bottom-(bounds.Y+topPad+pixelsPerLine*lineIdx);
				//}
				//else {
				//	layoutH=font.Height+2;
				//}
				//use points here:
				float adjustTextDown=10f;//this value was arrived at by trial and error.
				layoutRectangle=new RectangleF(
					(float)xbounds.X,
					//(float)(xbounds.Y+(float)topPad/scaleToPix+(pixelsPerLine/scaleToPix)*lineIdx),
					(float)(xbounds.Y+adjustTextDown+(pixelsPerLine/scaleToPix)*lineIdx),
					(float)xbounds.Width+50,//any amount of extra padding here will not cause malfunction
					0);//layoutH);
				xg.DrawString(str.Substring(ix,chars),xfont,xbrush,(double)layoutRectangle.Left,(double)layoutRectangle.Top);
				lineIdx+=1;
			}
		}

		public static int MeasureStringH(Graphics g,string text,Font font,int width) {
			return (int)MeasureString(g,text,font,width).Height;
		}

		///<summary>This also differs from the regular MeasureString in that it will correctly measure trailing carriage returns as requiring another line.</summary>
		public static SizeF MeasureString(Graphics g,string text,Font font,int width) {
			StringFormat format=StringFormat.GenericTypographic;
			float pixelsPerLine=lineSpacing * (float)font.Height;
			int chars;
			int lines;
			SizeF fit=new SizeF(width-rightPad,float.MaxValue);//arbitrarily large height
			g.MeasureString(text,font,fit,format,out chars,out lines);
			float h=topPad + ((float)lines)*pixelsPerLine;
			if(text.EndsWith("\n")) {
				h+=font.Height;//add another line to handle the trailing Carriage return.
			}
			return new SizeF((float)width,h);
		}


	}
}
