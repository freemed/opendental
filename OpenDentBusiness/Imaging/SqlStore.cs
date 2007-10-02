using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	public class SqlStore : ImageStoreBase {
		protected override Bitmap OpenImage(Document doc) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override byte[] GetBytes(Document doc) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void SaveDocument(Document doc, Bitmap image) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void SaveDocument(Document doc, Bitmap image, ImageFormat format) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void SaveDocument(Document doc, Bitmap image, ImageCodecInfo codec, EncoderParameters encoderParameters) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void SaveDocument(Document doc, string filename) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void DeleteThumbnailImageInternal(Document doc) {
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void DeleteDocument(Document doc) {
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
