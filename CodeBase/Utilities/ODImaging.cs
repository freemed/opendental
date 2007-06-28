using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CodeBase {
	public class ODImaging {

		///<summary>Returns the bits per channel, channels per pixel and bytes per pixel of the given pixel format. However, the pixel format must contain the same number of bits per channel. Additionally, future pixel formats may not be supported by this function.</summary>
		public static void GetFormatInfo(PixelFormat pf,ref int bitsPerChannel,ref int channelsPerPixel,ref int bytesPerPixel) {
			int[] formatDataTable=new int[] {
				//	PixelFormat																#bits/channel		#channels/pixel		#bytes/pixel
						(int)PixelFormat.Format16bppGrayScale,		16,							1,								2,
						(int)PixelFormat.Format16bppRgb555,				5,							3,								2,	
						(int)PixelFormat.Format1bppIndexed,				1,							1,								1,
						(int)PixelFormat.Format24bppRgb,					8,							3,								3,
						(int)PixelFormat.Format32bppArgb,					8,							4,								4,
						(int)PixelFormat.Format32bppPArgb,				8,							4,								4,
						(int)PixelFormat.Format32bppRgb,					8,							3,								4,
						(int)PixelFormat.Format48bppRgb,					16,							3,								6,
						(int)PixelFormat.Format4bppIndexed,				4,							1,								1,
						(int)PixelFormat.Format64bppArgb,					16,							4,								8,
						(int)PixelFormat.Format64bppPArgb,				16,							4,								8,
						(int)PixelFormat.Format8bppIndexed,				8,							1,								1,
			};
			for(int i=0;i<formatDataTable.Length;i+=4) {
				if(formatDataTable[i]==(int)pf) {
					bitsPerChannel=formatDataTable[i+1];
					channelsPerPixel=formatDataTable[i+2];
					bytesPerPixel=formatDataTable[i+3];
					return;
				}
			}
			throw new Exception("Cannot get bits per channel, channels per pixel and bytes per pixel for unsupported pixel format ("+
				((int)pf)+")");
		}

		///<summary>Performs anti-alias correction on the input image and returns the anti-aliased image in a new memory location (the  input image is unchanged).</summary>
		public static Bitmap AntiAlias(Bitmap image){
			BitmapData inData=image.LockBits(new Rectangle(0,0,image.Width,image.Height),
				ImageLockMode.ReadWrite,image.PixelFormat);
			byte[] imageBytes=new byte[inData.Stride*inData.Height];//For reading the data quickly and efficiently.
			byte[] resultBytes=new byte[inData.Stride*inData.Height];//For writing the new data quickly and efficiently.
			Marshal.Copy(inData.Scan0,imageBytes,0,imageBytes.Length);//Make new copy of image as bytes.
			Marshal.Copy(inData.Scan0,resultBytes,0,resultBytes.Length);//Make new copy of image as bytes.
			image.UnlockBits(inData);
			int maskSize=3;
			double[] mask=new double[]{
				1/24.0,	1/12.0,	1/24.0,
				1/12.0,	0.5,		1/12.0,
				1/24.0,	1/12.0,	1/24.0,
			};
			for(int x=1;x<image.Width-1;x++){//border pixels unaffected
				for(int y=1;y<image.Height-1;y++) {//border pixels unaffected
					double red=0;
					double green=0;
					double blue=0;
					double alpha=0;					
					for(int i=0;i<maskSize;i++) {
						for(int j=0;j<maskSize;j++) {
							int pixelOffset=inData.Stride*(y-maskSize/2+j)+(x-maskSize/2+i);
							double weight=mask[j*maskSize+i];
							red+=imageBytes[pixelOffset]*weight;
							green+=imageBytes[pixelOffset+1]*weight;
							blue+=imageBytes[pixelOffset+2]*weight;
							alpha+=imageBytes[pixelOffset+3]*weight;
						}
					}
					int resultPixelOffset=inData.Stride*y+x;
					resultBytes[resultPixelOffset]=(byte)red;
					resultBytes[resultPixelOffset+1]=(byte)green;
					resultBytes[resultPixelOffset+2]=(byte)blue;
					resultBytes[resultPixelOffset+3]=(byte)alpha;
				}
			}
			return new Bitmap(image.Width,image.Height,inData.Stride,image.PixelFormat,
				GCHandle.Alloc(resultBytes,GCHandleType.Pinned).AddrOfPinnedObject());
		}

	}
}
