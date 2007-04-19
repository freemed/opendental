using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;

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

	}
}
