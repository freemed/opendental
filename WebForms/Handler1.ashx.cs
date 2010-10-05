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
	/// </summary>
	[WebService(Namespace="http://opendental.com/")]
	[WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
	public class Handler1:IHttpHandler {

		private long WebSheetFieldDefNum=0;

		public void ProcessRequest(HttpContext context) {

			context.Response.ContentType = "image/gif";
			ODWebServiceEntities db=new ODWebServiceEntities();

			try {


				if(context.Request["WebSheetFieldDefNum"]!=null) {
					Int64.TryParse(context.Request["WebSheetFieldDefNum"].ToString().Trim(),out WebSheetFieldDefNum);
				}

				var sfdObj = db.webforms_sheetfielddef.Where(sfd => sfd.WebSheetFieldDefNum==WebSheetFieldDefNum).First();
				string ImageData=sfdObj.ImageData;
				Bitmap bitmap=PIn.Bitmap(ImageData);
				MemoryStream ms = new MemoryStream();
				// Save to memory using the Jpeg format
				bitmap.Save(ms,ImageFormat.Jpeg);
				// read to end
				byte[] binaryImage = ms.GetBuffer();
				bitmap.Dispose();
				ms.Close();
				context.Response.BinaryWrite(binaryImage);
			}

			catch(Exception ex) {

				throw ex;

			}
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}
