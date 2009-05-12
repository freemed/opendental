using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using CodeBase;
using System.IO;
using OpenDentBusiness;

namespace OpenDental{

	///<summary>Prints the contents of the active window. Currently only used on the Anesthetic Record forms</summary>
	public class PrintWindowL{
		//
		//Variables used for printing functionality..
		//
		public PrintDialog printDialog;
		public System.IO.Stream streamToPrint;
		public FileStream fileStream;
		public PrintDocument printDocument;
		public IntPtr thisHandle;
		public string streamType;
		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		public static extern bool BitBlt(
			IntPtr hdcDest, // handle to destination DC
			int nXDest, // x-coord of destination upper-left corner
			int nYDest, // y-coord of destination upper-left corner
			int nWidth, // width of destination rectangle
			int nHeight, // height of destination rectangle
			IntPtr hdcSrc, // handle to source DC
			int nXSrc, // x-coordinate of source upper-left corner
			int nYSrc, // y-coordinate of source upper-left corner
			System.Int32 dwRop); // raster operation code
		
			public void printDocument_PrintPage(object sender, PrintPageEventArgs e){
				System.IO.StreamReader streamReader = new StreamReader(this.streamToPrint);
				System.Drawing.Image image = System.Drawing.Image.FromStream(this.streamToPrint);
				int x = e.MarginBounds.X;
				int y = e.MarginBounds.Y;
				int width = image.Width;
				int height = image.Height;
				if  ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
					{
						width =  image.Width * e.MarginBounds.Width /image.Width; 
						height = image.Height * e.MarginBounds.Width/image.Width; 
					}
				else
					{
						width = image.Width * e.MarginBounds.Width / image.Height;
						height = image.Height; //e.MarginBounds.Height;
					}
				System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
				e.Graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height,System.Drawing.GraphicsUnit.Pixel);
		}

		public void Print(IntPtr thisHandle){
			string tempFile = Path.GetTempPath() + "PrintWindow.jpg";
			CaptureWindowToFile(thisHandle, tempFile, System.Drawing.Imaging.ImageFormat.Jpeg) ;
			FileStream fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
			StartPrint(fileStream,"Image");
			fileStream.Close();
			if (System.IO.File.Exists(tempFile)) {
					System.IO.File.Delete(tempFile);
				}
		}

		public void StartPrint(Stream streamToPrint, string streamType){
			printDocument = new PrintDocument();
			this.printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
			this.streamToPrint = streamToPrint;
			this.streamType = streamType;
			System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
			PrintDialog1.AllowSomePages = true;
			PrintDialog1.ShowHelp = true;
			PrintDialog1.Document = printDocument;
			PrintDialog1.UseEXDialog = true; //needed because PrintDialog was not showing on 64 bit Vista systems
				if (PrintDialog1.ShowDialog() == DialogResult.OK) {
						try {
								this.printDocument.Print();
							}
						catch {
								MessageBox.Show("That printer was not found. Please check connections or try another printer");
							}
					}
		}

		public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format){
			Image img = CaptureWindow(handle);
			img.Save(filename,format);
		}

		public static Image CaptureWindow(IntPtr handle){
			// get the hDC of the target window
			IntPtr hdcSrc = User32.GetWindowDC(handle);
			// get the size
			User32.RECT windowRect = new User32.RECT();
			User32.GetWindowRect(handle,ref windowRect);
			int width = windowRect.right - windowRect.left;
			int height = windowRect.bottom - windowRect.top;
			// create a device context we can copy to
			IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
			// create a bitmap we can copy it to,
			// using GetDeviceCaps to get the width/height
			IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc,width,height); 
			// select the bitmap object
			IntPtr hOld = GDI32.SelectObject(hdcDest,hBitmap);
			// bitblt over
			GDI32.BitBlt(hdcDest,0,0,width,height,hdcSrc,0,0,GDI32.SRCCOPY);
			// restore selection
			GDI32.SelectObject(hdcDest,hOld);
			// clean up 
			GDI32.DeleteDC(hdcDest);
			User32.ReleaseDC(handle,hdcSrc);
			// get a .NET image object for it
			Image img = Image.FromHbitmap(hBitmap);
			// free up the Bitmap object
			GDI32.DeleteObject(hBitmap);
				return img;
			}

			private class GDI32{
				public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
					[DllImport("gdi32.dll")]
				public static extern bool BitBlt(IntPtr hObject,int nXDest,int nYDest,
					int nWidth,int nHeight,IntPtr hObjectSource,
					int nXSrc,int nYSrc,int dwRop);
					[DllImport("gdi32.dll")]
				public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC,int nWidth, 
					int nHeight);
					[DllImport("gdi32.dll")]
				public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
					[DllImport("gdi32.dll")]
				public static extern bool DeleteDC(IntPtr hDC);
					[DllImport("gdi32.dll")]
				public static extern bool DeleteObject(IntPtr hObject);
					[DllImport("gdi32.dll")]
				public static extern IntPtr SelectObject(IntPtr hDC,IntPtr hObject);
				}

			private class User32{
					[StructLayout(LayoutKind.Sequential)]
					
			public struct RECT{
					public int left;
					public int top;
					public int right;
					public int bottom;
				}
					[DllImport("user32.dll")]
			public static extern IntPtr GetDesktopWindow();
					[DllImport("user32.dll")]
			public static extern IntPtr GetWindowDC(IntPtr hWnd);
					[DllImport("user32.dll")]
			public static extern IntPtr ReleaseDC(IntPtr hWnd,IntPtr hDC);
					[DllImport("user32.dll")]
			public static extern IntPtr GetWindowRect(IntPtr hWnd,ref RECT rect);
			}
	
	}
}
