using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using CodeBase;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace OpenDentBusiness.Imaging {
	public static class ImageHelper {
		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. Set imageSelected=-1 to unselect all images, or set to an image ordinal to highlight the image. The mount is rendered onto the given mountImage, so it must have been appropriately created by CreateBlankMountImage(). One can create a mount template by passing in arrays of zero length.</summary>
		public static void RenderMountImage(Bitmap mountImage, Bitmap[] originalImages, MountItem[] mountItems, Document[] documents, int imageSelected) {
			using (Graphics g = Graphics.FromImage(mountImage)) {
				//Draw mount encapsulating background rectangle.
				g.Clear(Pens.SlateGray.Color);
				RenderMountFrames(mountImage, mountItems, imageSelected);
				for (int i = 0; i < mountItems.Length; i++) {
					g.FillRectangle(Brushes.Black, mountItems[i].Xpos, mountItems[i].Ypos,
						mountItems[i].Width, mountItems[i].Height);//draw box behind image
					RenderImageIntoMount(mountImage, mountItems[i], originalImages[i], documents[i]);
				}
			}
		}

		///<summary>Renders the hallow rectangles which represent the individual image frames into the given mount image.</summary>
		public static void RenderMountFrames(Bitmap mountImage, MountItem[] mountItems, int imageSelected) {
			using (Graphics g = Graphics.FromImage(mountImage)) {
				//Draw image encapsulating background rectangles.
				for (int i = 0; i < mountItems.Length; i++) {
					Pen highlight;
					if (i == imageSelected) {
						highlight = (Pen)Pens.Yellow.Clone();//highlight desired image.
					}
					else {
						highlight = (Pen)Pens.SlateGray.Clone();//just surround other images with standard border.
					}
					highlight.Width = Math.Max(mountItems[i].Width, mountItems[i].Height) / 100.0f;
					g.DrawRectangle(highlight, mountItems[i].Xpos - highlight.Width / 2, mountItems[i].Ypos - highlight.Width / 2,
						mountItems[i].Width + highlight.Width, mountItems[i].Height + highlight.Width);
				}
			}
		}

		///<summary>Renders the given image using the settings provided by the given document object into the location of the given mountItem object.</summary>
		public static void RenderImageIntoMount(Bitmap mountImage, MountItem mountItem, Bitmap mountItemImage, Document mountItemDoc) {
			if (mountItem == null) {
				return;
			}
			
			using(Graphics g = Graphics.FromImage(mountImage)) {
				g.FillRectangle(Brushes.Black, mountItem.Xpos, mountItem.Ypos, mountItem.Width, mountItem.Height);//draw box behind image
				Bitmap image = ApplyDocumentSettingsToImage(mountItemDoc, mountItemImage, ApplySettings.ALL);
				if (image == null) {
					return;
				}
				float widthScale = ((float)mountItem.Width) / image.Width;
				float heightScale = ((float)mountItem.Height) / image.Height;
				float scale = (widthScale < heightScale ? widthScale : heightScale);
				RectangleF imageRect = new RectangleF(0, 0, scale * image.Width, scale * image.Height);
				imageRect.X = mountItem.Xpos + mountItem.Width / 2 - imageRect.Width / 2;
				imageRect.Y = mountItem.Ypos + mountItem.Height / 2 - imageRect.Height / 2;
				g.DrawImage(image, imageRect);
				image.Dispose();
			}
		}


		///<summary>Returns true if the given filename contains a supported file image extension.</summary>
		public static bool HasImageExtension(string fileName) {
			string ext = Path.GetExtension(fileName).ToLower();
			//The following supported bitmap types were found on a microsoft msdn page:
			return (ext == ".jpg" || ext == ".jpeg" || ext == ".tga" || ext == ".bmp" || ext == ".tif" ||
				ext == ".tiff" || ext == ".gif" || ext == ".emf" || ext == ".exif" || ext == ".ico" || ext == ".png" || ext == ".wmf");
		}

		///<summary>Applies the document specified cropping, flip, rotation, brightness and contrast transformations to the image and returns the resulting image. Zoom and translation must be handled by the calling code. The returned image is always a new image that can be modified without affecting the original image. The change in the image's center point is returned into deltaCenter, so that rotation offsets can be properly calculated when displaying the returned image.</summary>
		public static Bitmap ApplyDocumentSettingsToImage(Document doc, Bitmap image, ApplySettings settings) {
			if (image == null) {//Any operation on a non-existant image produces a non-existant image.
				return null;
			}
			if (doc == null) {//No doc implies no operations, implies that the image should be returned unaltered.
				return (Bitmap)image.Clone();
			}
			//CROP - Implies that the croping rectangle must be saved in raw-image-space coordinates, 
			//with an origin of that equal to the upper left hand portion of the image.
			Rectangle cropResult;
			if ((settings & ApplySettings.CROP) != 0 &&	//Crop not requested.
				doc.CropW > 0 && doc.CropH > 0)//No clip area yet defined, so no clipping is performed.
			{
				float[] cropDims = ODMathLib.IntersectRectangles(0, 0, image.Width, image.Height,//Intersect image rectangle with
					doc.CropX, doc.CropY, doc.CropW, doc.CropH);//document crop rectangle.
				if (cropDims.Length == 0) {//The entire image has been cropped away.
					return null;
				}
				//Rounds dims up, so that data is not lost, but possibly not removing all of what was expected.
				cropResult = new Rectangle((int)cropDims[0], (int)cropDims[1],
					(int)Math.Ceiling(cropDims[2]), (int)Math.Ceiling(cropDims[3]));
			}
			else {
				cropResult = new Rectangle(0, 0, image.Width, image.Height);//No cropping.
			}
			//Always use 32-bit images in memory. We could use 24-bit images here (it works in Windows, but MONO produces
			//output using 32-bit data on a 24-bit image in that case, providing horrible output). Perhaps we can update
			//this when MONO is more fully featured.
			Bitmap cropped = new Bitmap(cropResult.Width, cropResult.Height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(cropped);
			Rectangle croppedDims = new Rectangle(0, 0, cropped.Width, cropped.Height);
			g.DrawImage(image, croppedDims, cropResult, GraphicsUnit.Pixel);
			g.Dispose();
			//FLIP AND ROTATE - must match the operations in GetDocumentFlippedRotatedMatrix().
			if ((settings & ApplySettings.FLIP) != 0) {
				if (doc.IsFlipped) {
					cropped.RotateFlip(RotateFlipType.RotateNoneFlipX);
				}
			}
			if ((settings & ApplySettings.ROTATE) != 0) {
				if (doc.DegreesRotated % 360 == 90) {
					cropped.RotateFlip(RotateFlipType.Rotate90FlipNone);
				}
				else if (doc.DegreesRotated % 360 == 180) {
					cropped.RotateFlip(RotateFlipType.Rotate180FlipNone);
				}
				else if (doc.DegreesRotated % 360 == 270) {
					cropped.RotateFlip(RotateFlipType.Rotate270FlipNone);
				}
			}
			//APPLY BRIGHTNESS AND CONTRAST - 
			//TODO: should be updated later for more general functions 
			//(create inputValues and outputValues from stored db function/table).
			if ((settings & ApplySettings.COLORFUNCTION) != 0 &&
				doc.WindowingMax != 0 && //Do not apply color function if brightness/contrast have never been set (assume normal settings).
				!(doc.WindowingMax == 255 && doc.WindowingMin == 0)) {//Don't apply if brightness/contrast settings are normal.
				float[] inputValues = new float[] {
					doc.WindowingMin/255f,
					doc.WindowingMax/255f,
				};
				float[] outputValues = new float[]{
					0,
					1,
				};
				BitmapData croppedData = null;
				try {
					croppedData = cropped.LockBits(new Rectangle(0, 0, cropped.Width, cropped.Height),
						ImageLockMode.ReadWrite, cropped.PixelFormat);
					unsafe {
						byte* pBits;
						if (croppedData.Stride < 0) {
							pBits = (byte*)croppedData.Scan0.ToPointer() + croppedData.Stride * (croppedData.Height - 1);
						}
						else {
							pBits = (byte*)croppedData.Scan0.ToPointer();
						}
						//The following loop goes through each byte of each 32-bit value and applies the color function to it.
						//Thus, the same transformation is performed to all 4 color components equivalently for each pixel.
						for (int i = 0; i < croppedData.Stride * croppedData.Height; i++) {
							float colorComponent = pBits[i] / 255f;
							float rangedOutput;
							if (colorComponent <= inputValues[0]) {
								rangedOutput = outputValues[0];
							}
							else if (colorComponent >= inputValues[inputValues.Length - 1]) {
								rangedOutput = outputValues[outputValues.Length - 1];
							}
							else {
								int j = 0;
								while (!(inputValues[j] <= colorComponent && colorComponent < inputValues[j + 1])) {
									j++;
								}
								rangedOutput = ((colorComponent - inputValues[j]) * (outputValues[j + 1] - outputValues[j]))
									/ (inputValues[j + 1] - inputValues[j]);
							}
							pBits[i] = (byte)Math.Round(255 * rangedOutput);
						}
					}
				}
				catch {
				}
				finally {
					try {
						cropped.UnlockBits(croppedData);
					}
					catch {
					}
				}
			}
			return cropped;
		}


		///<summary>specify the size of the square to return</summary>
		public static Bitmap GetThumbnail(Image original, int size) {
			Bitmap retVal = new Bitmap(size, size);
			Graphics g = Graphics.FromImage(retVal);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;
			if (original.Height > original.Width) {//original is too tall
				float ratio = (float)size / (float)original.Height;
				float w = (float)original.Width * ratio;
				g.DrawImage(original, (size - w) / 2f, 0, w, (float)size);
			}
			else {//original is too wide
				float ratio = (float)size / (float)original.Width;
				float h = (float)original.Height * ratio;
				g.DrawImage(original, 0, (size - h) / 2f, (float)size, h);
			}
			g.Dispose();
			return retVal;
		}
	}
}
