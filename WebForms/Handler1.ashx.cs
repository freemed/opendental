using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;
using OpenDentBusiness;
using System.IO;

namespace WebForms {
	/// <summary>
	/// Dynamically generates the sheet images which are read from the database.
	/// ToDo: test with various images.formats 
	/// </summary>
	[WebService(Namespace="http://opendental.com/")]
	[WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
	public class Handler1:IHttpHandler {

		private long WebSheetFieldDefID=0;

		public void ProcessRequest(HttpContext context) {
			//TestBackGround(context); return;
			try {
				if(context.Request["WebSheetFieldDefID"]!=null) {
					Int64.TryParse(context.Request["WebSheetFieldDefID"].ToString().Trim(),out WebSheetFieldDefID);
				}
				/*png images are used because the background of rectangles/lines can be set to transparent. For gif images the process of making the background transparent is convoluted*/
				context.Response.ContentType="image/png";
				ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj=db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefID==WebSheetFieldDefID).First();
				SheetFieldType FieldType = (SheetFieldType)sfdObj.FieldType;
				Bitmap bmp=null;
				Graphics g=null;
				
				Pen p=new Pen(Color.Black,2.0f);
				
				if(FieldType==SheetFieldType.Rectangle || FieldType==SheetFieldType.Line) {
					bmp = new Bitmap(sfdObj.Width,sfdObj.Height);
					g = Graphics.FromImage(bmp);
					//g.Clear(Color.Transparent);
				}
				if(FieldType==SheetFieldType.Rectangle) {
					g.DrawRectangle(p,0,0,sfdObj.Width,sfdObj.Height);
				}
				if(FieldType==SheetFieldType.Line) {
					g.DrawLine(p,0,0,sfdObj.Width,sfdObj.Height);
				}	
				if((SheetFieldType)sfdObj.FieldType==SheetFieldType.Image) {
					string ImageData=sfdObj.ImageData;
					bmp=PIn.Bitmap(ImageData);
				}

				/*  These 3 lines are used in lue of the shorter "bmp.Save(context.Response.OutputStream,ImageFormat.Png);" because it does not work with png images.*/
				MemoryStream MemStream=new MemoryStream();
				bmp.Save(MemStream,System.Drawing.Imaging.ImageFormat.Png);
				MemStream.WriteTo(context.Response.OutputStream);

				
				if(FieldType==SheetFieldType.Rectangle || FieldType==SheetFieldType.Line) {
					g.Dispose();
				}
				bmp.Dispose();
				
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
		}

		private void DrawFigure(HttpContext context, long WebSheetFieldDefID) {
			try {
				context.Response.ContentType="image/gif";
				ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj=db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefID==WebSheetFieldDefID).First();
				Bitmap bmp = new Bitmap(sfdObj.Width,sfdObj.Height,PixelFormat.Format24bppRgb);
				Graphics g = Graphics.FromImage(bmp);
				g.Clear(Color.White);
				if((SheetFieldType)sfdObj.FieldType==SheetFieldType.Rectangle) {
					g.DrawRectangle(Pens.Black,sfdObj.XPos,sfdObj.YPos,sfdObj.Width,sfdObj.Height);
				}
				if((SheetFieldType)sfdObj.FieldType==SheetFieldType.Line) {
					g.DrawLine(Pens.Black,sfdObj.XPos,sfdObj.YPos,sfdObj.XPos+sfdObj.Width,sfdObj.YPos+sfdObj.Height);
				}
				bmp.Save(context.Response.OutputStream,ImageFormat.Jpeg);
				g.Dispose();
				bmp.Dispose();
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
	}

		private void PutImage(HttpContext context,long WebSheetFieldDefID) {
				context.Response.ContentType="image/gif";
			ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj=db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefID==WebSheetFieldDefID).First();
				string ImageData=sfdObj.ImageData;
				Bitmap bitmap=PIn.Bitmap(ImageData);
				bitmap.Save(context.Response.OutputStream,ImageFormat.Jpeg);
				bitmap.Dispose();
		}

		public void ProcessRequest12(HttpContext context) {

			context.Response.ContentType="image/gif";
			ODWebServiceEntities db=new ODWebServiceEntities();
			try {

				context.Response.ContentType="image/gif";
				
				int height = 100;
				int width = 200;
				Random r = new Random();
				int x = r.Next(75);

				Bitmap bmp = new Bitmap(width,height,PixelFormat.Format24bppRgb);
				Graphics g = Graphics.FromImage(bmp);

				
				g.Clear(Color.Orange);
				g.DrawRectangle(Pens.White,1,1,width-3,height-3);
				g.DrawRectangle(Pens.Gray,2,2,width-3,height-3);
				g.DrawRectangle(Pens.Black,0,0,width,height);
				g.DrawString("The Code Project",new Font("Arial",12,FontStyle.Italic),
				SystemBrushes.WindowText,new PointF(x,50));

				bmp.Save(context.Response.OutputStream,ImageFormat.Jpeg);
				g.Dispose();
				bmp.Dispose();
				


				//context.Response.BinaryWrite(binaryImage);
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
		}

		private void TestBackGround(HttpContext context) {
			// Create a red and black bitmap to demonstrate transparency.            
			Bitmap tempBMP = new Bitmap(700,700);
			Graphics g = Graphics.FromImage(tempBMP);
			g.FillEllipse(new SolidBrush(Color.Red),0,0,tempBMP.Width,tempBMP.Width);
			g.DrawLine(new Pen(Color.Black),0,0,tempBMP.Width,tempBMP.Width);
			g.DrawLine(new Pen(Color.Black),tempBMP.Width,0,0,tempBMP.Width);
			g.Dispose();


			// Set the transparancy key attributes,at current it is set to the 
			// color of the pixel in top left corner(0,0)
			ImageAttributes attr = new ImageAttributes();
			attr.SetColorKey(tempBMP.GetPixel(0,0),tempBMP.GetPixel(0,0));

			// Draw the image to your output using the transparancy key attributes
			Bitmap outputImage = new Bitmap(700,700);
			g = Graphics.FromImage(outputImage);
			Rectangle destRect = new Rectangle(0,0,tempBMP.Width,tempBMP.Height);
			g.DrawImage(tempBMP,destRect,0,0,tempBMP.Width,tempBMP.Height,GraphicsUnit.Pixel,attr);

			outputImage.Save(context.Response.OutputStream,ImageFormat.Jpeg);
			g.Dispose();
			tempBMP.Dispose();
			

		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}
