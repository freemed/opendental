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

		private long WebSheetFieldDefNum=0;

		public void ProcessRequest(HttpContext context) {


			////context.Response.ContentType="image/gif";
			////ODWebServiceEntities db=new ODWebServiceEntities();
			try {
				if(context.Request["WebSheetFieldDefNum"]!=null) {
					Int64.TryParse(context.Request["WebSheetFieldDefNum"].ToString().Trim(),out WebSheetFieldDefNum);
				}
				context.Response.ContentType="image/gif";
				ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj=db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefNum==WebSheetFieldDefNum).First();
				SheetFieldType FieldType = (SheetFieldType)sfdObj.FieldType;
				Bitmap bmp=null;
				Graphics g=null;

				Pen p=new Pen(Color.Black,2.0f);
				
				if(FieldType==SheetFieldType.Rectangle || FieldType==SheetFieldType.Line) {
					bmp = new Bitmap(sfdObj.Width,sfdObj.Height);
					g = Graphics.FromImage(bmp);
					g.Clear(Color.White);
				}
				if(FieldType==SheetFieldType.Rectangle) {
					g.DrawRectangle(p,sfdObj.XPos,sfdObj.YPos,sfdObj.Width,sfdObj.Height);
				}
				if(FieldType==SheetFieldType.Line) {
					g.DrawLine(p,sfdObj.XPos,sfdObj.YPos,sfdObj.XPos+sfdObj.Width,sfdObj.YPos+sfdObj.Height);
				}
				if((SheetFieldType)sfdObj.FieldType==SheetFieldType.Image) {
					string ImageData=sfdObj.ImageData;
					bmp=PIn.Bitmap(ImageData);
				}
				bmp.Save(context.Response.OutputStream,ImageFormat.Jpeg);
				if(FieldType==SheetFieldType.Rectangle || FieldType==SheetFieldType.Line) {
					g.Dispose();
				}
				bmp.Dispose();
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
			}
		}

		private void DrawFigure(HttpContext context, long WebSheetFieldDefNum) {
			try {
				context.Response.ContentType="image/gif";
				ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj=db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefNum==WebSheetFieldDefNum).First();
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

		private void PutImage(HttpContext context,long WebSheetFieldDefNum) {
				context.Response.ContentType="image/gif";
			ODWebServiceEntities db=new ODWebServiceEntities();
				var sfdObj=db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefNum==WebSheetFieldDefNum).First();
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

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}
