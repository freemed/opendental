/*using System.Runtime.InteropServices;
using System.Text;

namespace OpenDental
{

	///<summary></summary>
	public abstract class EZTwain
{
		//  This file is an automatic translation of \EZTwain\VC\eztwain.h
		//  Generated 2004.09.09 22:19 Pacific Daylight Time by XDefs 1.21


		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Testing123")] public static extern
		System.IntPtr Testing123(string s, int n, System.IntPtr h, double d, int u);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Acquire")] public static extern
		System.IntPtr Acquire(System.IntPtr hwndApp);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_FreeNative")] public static extern
		void FreeNative(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SelectImageSource")] public static extern
		int SelectImageSource(System.IntPtr hwnd);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireNative")] public static extern
		System.IntPtr AcquireNative(System.IntPtr hwndApp, int wPixTypes);

		///<summary></summary>
		public const int TWAIN_BW = 1;
		///<summary></summary>
		public const int TWAIN_GRAY = 2;
		///<summary></summary>
		public const int TWAIN_RGB = 4;
		///<summary></summary>
		public const int TWAIN_PALETTE = 8;
		///<summary></summary>
		public const int TWAIN_CMY = 16;
		///<summary></summary>
		public const int TWAIN_CMYK = 32;
		///<summary></summary>
		public const int TWAIN_ANYTYPE = 0;

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireToClipboard")] public static extern
		int AcquireToClipboard(System.IntPtr hwndApp, int wPixTypes);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireMemory")] public static extern
		System.IntPtr AcquireMemory(System.IntPtr hwnd);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireToFilename")] public static extern
		int AcquireToFilename(System.IntPtr hwndApp, string sFile);


		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireMultipageFile")] public static extern
		int AcquireMultipageFile(System.IntPtr hwndApp, string sFile);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireFile")] public static extern
		int AcquireFile(System.IntPtr hwndApp, int nFF, string sFile);


		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetMultiTransfer")] public static extern
		void SetMultiTransfer(int fYes);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetMultiTransfer")] public static extern
		int GetMultiTransfer();

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetHideUI")] public static extern
		void SetHideUI(int fHide);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetHideUI")] public static extern
		int GetHideUI();

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DisableParent")] public static extern
		void DisableParent(int fYes);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDisableParent")] public static extern
		int GetDisableParent();

///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EasyVersion")] public static extern
		int EasyVersion();

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsAvailable")] public static extern
		int IsAvailable();

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsMultipageAvailable")] public static extern
		int IsMultipageAvailable();

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_State")] public static extern
		int State();
		///<summary></summary>
		public const int TWAIN_PRESESSION = 1;
		///<summary></summary>
		public const int TWAIN_SM_LOADED = 2;
		///<summary></summary>
		public const int TWAIN_SM_OPEN = 3;
		///<summary></summary>
		public const int TWAIN_SOURCE_OPEN = 4;
		///<summary></summary>
		public const int TWAIN_SOURCE_ENABLED = 5;
		///<summary></summary>
		public const int TWAIN_TRANSFER_READY = 6;
		///<summary></summary>
		public const int TWAIN_TRANSFERRING = 7;

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SourceName")] public static extern
		string SourceName();

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSourceName")] public static extern
		void GetSourceName(StringBuilder sName);


///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetResolution(System.IntPtr hdib, double xdpi, double ydpi);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double DIB_XResolution(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double DIB_YResolution(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_Depth(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_PixelType(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_Width(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_Height(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_RowBytes(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_ColorCount(System.IntPtr hdib);

///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_ReadRow(System.IntPtr hdib, int nRow, System.IntPtr prow);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_ReadRowRGB(System.IntPtr hdib, int nRow, System.IntPtr prow);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_WriteRow(System.IntPtr hdib, int r, System.IntPtr pdata);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_WriteRowChannel(System.IntPtr hdib, int r, System.IntPtr pdata, int nChannel);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_WriteRowSafe(System.IntPtr hdib, int r, System.IntPtr pdata, int nbMax);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_ReadRowSafe(System.IntPtr hdib, int nRow, System.IntPtr prow, int nbMax);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_Allocate(int nDepth, int nWidth, int nHeight);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_Free(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_Copy(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		bool DIB_Equal(System.IntPtr hdib1, System.IntPtr hdib2);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetGrayColorTable(System.IntPtr hdib);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetColorTableRGB(System.IntPtr hdib, int i, int R, int G, int B);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_ColorTableR(System.IntPtr hdib, int i);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_ColorTableG(System.IntPtr hdib, int i);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_ColorTableB(System.IntPtr hdib, int i);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_FlipVertical(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_FlipHorizontal(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_Rotate180(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_Rotate90(System.IntPtr hOld, int nSteps);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_Fill(System.IntPtr hdib, int R, int G, int B);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_Negate(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_Convolve(System.IntPtr hdibDst, System.IntPtr hdibKernel, double dNorm, int nOffset);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_MedianFilter(System.IntPtr hdib, int W, int H, int nStyle);
 
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_ScaledCopy(System.IntPtr hOld, int w, int h);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_RegionCopy(System.IntPtr hOld, int x, int y, int w, int h, int FillByte);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_AutoCrop(System.IntPtr hOld, int nOptions);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_AutoDeskew(System.IntPtr hOld, int nOptions);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_ConvertToPixelType(System.IntPtr hOld, int nPT);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SwapRedBlue(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_CreatePalette(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetColorModel(System.IntPtr hdib, int nCM);
		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_ColorModel(System.IntPtr hdib);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetColorCount(System.IntPtr hdib, int n);

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_Blt(System.IntPtr hdibDst, int dx, int dy, System.IntPtr hdibSrc, int sx, int sy, int w, int h, int uRop);
		///<summary></summary>
		public const int EZT_ROP_COPY = 0;
		///<summary></summary>
		public const int EZT_ROP_OR = 1;
		///<summary></summary>
		public const int EZT_ROP_AND = 2;
		///<summary></summary>
		public const int EZT_ROP_XOR = 3;
		///<summary></summary>
		public const int EZT_ROP_ANDNOT = 18;

		///<summary></summary>
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_BltMask(System.IntPtr hdibDst, int dx, int dy, System.IntPtr hdibSrc, int sx, int sy, int w, int h, int uRop, System.IntPtr hdibMask);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_PaintMask(System.IntPtr hdibDst, int dx, int dy, int R, int G, int B, int sx, int sy, int w, int h, int uRop, System.IntPtr hdibMask);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_DrawLine(System.IntPtr hdibDst, int x1, int y1, int x2, int y2, int R, int G, int B);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_DrawText(System.IntPtr hdibDst, string sText, int x, int y, int w, int h);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetTextColor(int R, int G, int B);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetTextAngle(int nDegrees);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetTextHeight(int nH);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetTextFace(string sTypeface);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SetTextFormat(int nFlags);
		public const int EZT_TEXT_NORMAL = 0;
		public const int EZT_TEXT_BOLD = 1;
		public const int EZT_TEXT_ITALIC = 2;
		public const int EZT_TEXT_UNDERLINE = 4;
		public const int EZT_TEXT_STRIKEOUT = 8;
		public const int EZT_TEXT_BOTTOM = 256;
		public const int EZT_TEXT_VCENTER = 512;
		public const int EZT_TEXT_TOP = 0;
		public const int EZT_TEXT_LEFT = 0;
		public const int EZT_TEXT_CENTER = 4096;
		public const int EZT_TEXT_RIGHT = 8192;
		public const int EZT_TEXT_WRAP = 16384;


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_DrawToDC(System.IntPtr hdib, System.IntPtr hDC, int dx, int dy, int w, int h, int sx, int sy);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_Print(System.IntPtr hdib, string sJobname);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_PrintNoPrompt(System.IntPtr hdib, string sJobname);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_PutOnClipboard(System.IntPtr hdib);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_GetFromClipboard();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_OpenInDC(System.IntPtr hdib, System.IntPtr hdc);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_CloseInDC(System.IntPtr hdib, System.IntPtr hdc);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToBmp(System.IntPtr hdib, string sFile);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToBmpFile(System.IntPtr hdib, System.IntPtr fh);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToJpeg(System.IntPtr hdib, string sFile);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToPng(System.IntPtr hdib, string sFile);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToTiff(System.IntPtr hdib, string sFile);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToPdf(System.IntPtr hdib, string sFile);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_WriteToGif(System.IntPtr hdib, string sFile);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_LoadFromFilename(string sFile);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_FormatOfFile(string sFile);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_SelectPageToLoad(int nPage);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int DIB_GetFilePageCount(string sFile);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_LoadPage(string sFile, int nPage);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_ToDibSection(System.IntPtr hdib);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr DIB_FromBitmap(System.IntPtr hbm, System.IntPtr hdc);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void DIB_GetHistogram(System.IntPtr hdib, int nComponent, [MarshalAs(UnmanagedType.LPArray, SizeConst=256)] int[] histo);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_NegotiateXferCount")] public static extern
		int NegotiateXferCount(int nXfers);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibDepth")] public static extern
		int DibDepth(System.IntPtr hdib);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibWidth")] public static extern
		int DibWidth(System.IntPtr hdib);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibHeight")] public static extern
		int DibHeight(System.IntPtr hdib);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibNumColors")] public static extern
		int DibNumColors(System.IntPtr hdib);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibRowBytes")] public static extern
		int DibRowBytes(System.IntPtr hdib);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibReadRow")] public static extern
		void DibReadRow(System.IntPtr hdib, int nRow, System.IntPtr prow);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CreateDibPalette")] public static extern
		System.IntPtr CreateDibPalette(System.IntPtr hdib);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DrawDibToDC")] public static extern
		void DrawDibToDC(System.IntPtr hDC, int dx, int dy, int w, int h, System.IntPtr hdib, int sx, int sy);


		public const int TWFF_TIFF = 0;
		public const int TWFF_BMP = 2;
		public const int TWFF_JFIF = 4;
		public const int TWFF_PNG = 7;
		public const int TWFF_GIF = 98;
		public const int TWFF_PDF = 99;

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsJpegAvailable")] public static extern
		int IsJpegAvailable();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsPngAvailable")] public static extern
		int IsPngAvailable();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsTiffAvailable")] public static extern
		int IsTiffAvailable();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsPdfAvailable")] public static extern
		int IsPdfAvailable();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsGifAvailable")] public static extern
		int IsGifAvailable();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFormatAvailable")] public static extern
		int IsFormatAvailable(int nFF);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetSaveFormat")] public static extern
		int SetSaveFormat(int nFF);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSaveFormat")] public static extern
		int GetSaveFormat();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetJpegQuality")] public static extern
		void SetJpegQuality(int nQ);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetJpegQuality")] public static extern
		int GetJpegQuality();
 
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffStripSize")] public static extern
		void SetTiffStripSize(int nBytes);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetTiffStripSize")] public static extern
		int GetTiffStripSize();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffImageDescription")] public static extern
		int SetTiffImageDescription(string sText);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffDocumentName")] public static extern
		int SetTiffDocumentName(string sText);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffCompression")] public static extern
		int SetTiffCompression(int nPT, int nComp);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetTiffCompression")] public static extern
		int GetTiffCompression(int nPT);
		public const int TIFF_COMP_NONE = 1;
		public const int TIFF_COMP_CCITTRLE = 2;
		public const int TIFF_COMP_CCITTFAX3 = 3;
		public const int TIFF_COMP_CCITTFAX4 = 4;
		public const int TIFF_COMP_LZW = 5;
		public const int TIFF_COMP_JPEG = 7;
		public const int TIFF_COMP_PACKBITS = 32773;


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetFileAppendFlag")] public static extern
		void SetFileAppendFlag(int nAppend);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetFileAppendFlag")] public static extern
		int GetFileAppendFlag();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_WriteNativeToFilename")] public static extern
		int WriteNativeToFilename(System.IntPtr hdib, string sFile);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LoadNativeFromFilename")] public static extern
		System.IntPtr LoadNativeFromFilename(string sFile);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_FormatOfFile")] public static extern
		int FormatOfFile(string sFile);



		public const int MULTIPAGE_TIFF = 0;
		public const int MULTIPAGE_PDF = 1;

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetMultipageFormat")] public static extern
		int SetMultipageFormat(int nFF);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetMultipageFormat")] public static extern
		int GetMultipageFormat();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BeginMultipageFile")] public static extern
		int BeginMultipageFile(string sFile);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibWritePage")] public static extern
		int DibWritePage(System.IntPtr hdib);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EndMultipageFile")] public static extern
		int EndMultipageFile();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_MultipageCount")] public static extern
		int MultipageCount();


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_RegisterApp")] public static extern
		void RegisterApp(int nMajorNum, int nMinorNum, int nLanguage, int nCountry, string sVersion, string sMfg, string sFamily, string sAppTitle);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAppTitle")] public static extern
		void SetAppTitle(string sAppTitle);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetApplicationKey")] public static extern
		void SetApplicationKey(int nKey);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetVendorKey")] public static extern
		void SetVendorKey(string sVendorName, int nKey);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetResultCode")] public static extern
		int GetResultCode();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetConditionCode")] public static extern
		int GetConditionCode();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_UserClosedSource")] public static extern
		int UserClosedSource();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ErrorBox")] public static extern
		void ErrorBox(string sMsg);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SuppressErrorMessages")] public static extern
		int SuppressErrorMessages(int nSuppress);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ReportLastError")] public static extern
		void ReportLastError(string sMsg);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetLastErrorText")] public static extern
		void GetLastErrorText(StringBuilder sMsg);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LastErrorText")] public static extern
		string LastErrorText();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LastErrorCode")] public static extern
		int LastErrorCode();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ClearError")] public static extern
		void ClearError();

		public const int EZTEC_NONE = 0;
		public const int EZTEC_START_TRIPLET_ERRS = 1;
		public const int EZTEC_CAP_GET = 2;
		public const int EZTEC_CAP_SET = 3;
		public const int EZTEC_DSM_FAILURE = 4;
		public const int EZTEC_DS_FAILURE = 5;
		public const int EZTEC_XFER_FAILURE = 6;
		public const int EZTEC_END_TRIPLET_ERRS = 7;
		public const int EZTEC_OPEN_DSM = 8;
		public const int EZTEC_OPEN_DEFAULT_DS = 9;
		public const int EZTEC_NOT_STATE_4 = 10;
		public const int EZTEC_NULL_HCON = 11;
		public const int EZTEC_BAD_HCON = 12;
		public const int EZTEC_BAD_CONTYPE = 13;
		public const int EZTEC_BAD_ITEMTYPE = 14;
		public const int EZTEC_CAP_GET_EMPTY = 15;
		public const int EZTEC_CAP_SET_EMPTY = 16;
		public const int EZTEC_INVALID_HWND = 17;
		public const int EZTEC_PROXY_WINDOW = 18;
		public const int EZTEC_USER_CANCEL = 19;
		public const int EZTEC_RESOLUTION = 20;
		public const int EZTEC_LICENSE = 21;
		public const int EZTEC_JPEG_DLL = 22;
		public const int EZTEC_SOURCE_EXCEPTION = 23;
		public const int EZTEC_LOAD_DSM = 24;
		public const int EZTEC_NO_SUCH_DS = 25;
		public const int EZTEC_OPEN_DS = 26;
		public const int EZTEC_ENABLE_FAILED = 27;
		public const int EZTEC_BAD_MEMXFER = 28;
		public const int EZTEC_JPEG_GRAY_OR_RGB = 29;
		public const int EZTEC_JPEG_BAD_Q = 30;
		public const int EZTEC_BAD_DIB = 31;
		public const int EZTEC_BAD_FILENAME = 32;
		public const int EZTEC_FILE_NOT_FOUND = 33;
		public const int EZTEC_FILE_ACCESS = 34;
		public const int EZTEC_MEMORY = 35;
		public const int EZTEC_JPEG_ERR = 36;
		public const int EZTEC_JPEG_ERR_REPORTED = 37;
		public const int EZTEC_0_PAGES = 38;
		public const int EZTEC_UNK_WRITE_FF = 39;
		public const int EZTEC_NO_TIFF = 40;
		public const int EZTEC_TIFF_WRITE_ERR = 41;
		public const int EZTEC_PDF_WRITE_ERR = 42;
		public const int EZTEC_NO_PDF = 43;
		public const int EZTEC_GIFCON = 44;
		public const int EZTEC_FILE_READ_ERR = 45;
		public const int EZTEC_BAD_REGION = 46;





		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LoadSourceManager")] public static extern
		int LoadSourceManager();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_OpenSourceManager")] public static extern
		int OpenSourceManager(System.IntPtr hwnd);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_OpenDefaultSource")] public static extern
		int OpenDefaultSource();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSourceList")] public static extern
		int GetSourceList();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetNextSourceName")] public static extern
		int GetNextSourceName(StringBuilder sName);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_NextSourceName")] public static extern
		string NextSourceName();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDefaultSourceName")] public static extern
		int GetDefaultSourceName(StringBuilder sName);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DefaultSourceName")] public static extern
		string DefaultSourceName();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_OpenSource")] public static extern
		int OpenSource(string sName);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EnableSource")] public static extern
		int EnableSource(System.IntPtr hwnd);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DisableSource")] public static extern
		int DisableSource();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CloseSource")] public static extern
		int CloseSource();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CloseSourceManager")] public static extern
		int CloseSourceManager(System.IntPtr hwnd);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_UnloadSourceManager")] public static extern
		int UnloadSourceManager();


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsTransferReady")] public static extern
		int IsTransferReady();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EndXfer")] public static extern
		int EndXfer();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AbortAllPendingXfers")] public static extern
		int AbortAllPendingXfers();



		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetXferCount")] public static extern
		int SetXferCount(int nXfers);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_NegotiatePixelTypes")] public static extern
		int NegotiatePixelTypes(int wPixTypes);

		public const int TWUN_INCHES = 0;
		public const int TWUN_CENTIMETERS = 1;
		public const int TWUN_PICAS = 2;
		public const int TWUN_POINTS = 3;
		public const int TWUN_TWIPS = 4;
		public const int TWUN_PIXELS = 5;

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrentUnits")] public static extern
		int GetCurrentUnits();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetUnits")] public static extern
		int SetUnits(int nUnits);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCurrentUnits")] public static extern
		int SetCurrentUnits(int nUnits);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetBitDepth")] public static extern
		int GetBitDepth();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetBitDepth")] public static extern
		int SetBitDepth(int nBits);

		public const int TWPT_BW = 0;
		public const int TWPT_GRAY = 1;
		public const int TWPT_RGB = 2;
		public const int TWPT_PALETTE = 3;
		public const int TWPT_CMY = 4;
		public const int TWPT_CMYK = 5;

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetPixelType")] public static extern
		int GetPixelType();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPixelType")] public static extern
		int SetPixelType(int nPixType);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCurrentPixelType")] public static extern
		int SetCurrentPixelType(int nPixType);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrentResolution")] public static extern
		double GetCurrentResolution();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetYResolution")] public static extern
		double GetYResolution();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetResolution")] public static extern
		int SetResolution(double dRes);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCurrentResolution")] public static extern
		int SetCurrentResolution(double dRes);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetXResolution")] public static extern
		int SetXResolution(double dxRes);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetYResolution")] public static extern
		int SetYResolution(double dyRes);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetContrast")] public static extern
		int SetContrast(double dCon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetBrightness")] public static extern
		int SetBrightness(double dBri);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetThreshold")] public static extern
		int SetThreshold(double dThresh);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrentThreshold")] public static extern
		double GetCurrentThreshold();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetXferMech")] public static extern
		int SetXferMech(int mech);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_XferMech")] public static extern
		int XferMech();
		public const int XFERMECH_NATIVE = 0;
		public const int XFERMECH_FILE = 1;
		public const int XFERMECH_MEMORY = 2;

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SupportsFileXfer")] public static extern
		int SupportsFileXfer();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPaperSize")] public static extern
		int SetPaperSize(int nPaper);
		public const int PAPER_A4LETTER = 1;
		public const int PAPER_B5LETTER = 2;
		public const int PAPER_USLETTER = 3;
		public const int PAPER_USLEGAL = 4;
		public const int PAPER_A5 = 5;
		public const int PAPER_B4 = 6;
		public const int PAPER_B6 = 7;
		public const int PAPER_USLEDGER = 9;
		public const int PAPER_USEXECUTIVE = 10;
		public const int PAPER_A3 = 11;
		public const int PAPER_B3 = 12;
		public const int PAPER_A6 = 13;
		public const int PAPER_C4 = 14;
		public const int PAPER_C5 = 15;
		public const int PAPER_C6 = 16;


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_HasFeeder")] public static extern
		bool HasFeeder();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFeederSelected")] public static extern
		bool IsFeederSelected();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SelectFeeder")] public static extern
		int SelectFeeder(int fYes);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsAutoFeedOn")] public static extern
		bool IsAutoFeedOn();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoFeed")] public static extern
		int SetAutoFeed(int fYes);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFeederLoaded")] public static extern
		bool IsFeederLoaded();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoScan")] public static extern
		int SetAutoScan(int fYes);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDuplexSupport")] public static extern
		int GetDuplexSupport();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EnableDuplex")] public static extern
		int EnableDuplex(int fYes);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsDuplexEnabled")] public static extern
		bool IsDuplexEnabled();


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_HasControllableUI")] public static extern
		int HasControllableUI();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetIndicators")] public static extern
		int SetIndicators(bool bVisible);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Compression")] public static extern
		int Compression();
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCompression")] public static extern
		int SetCompression(int compression);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Tiled")] public static extern
		bool Tiled();
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiled")] public static extern
		int SetTiled(bool bTiled);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PlanarChunky")] public static extern
		int PlanarChunky();
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPlanarChunky")] public static extern
		int SetPlanarChunky(int shape);

		public const int CHUNKY_PIXELS = 0;
		public const int PLANAR_PIXELS = 1;

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PixelFlavor")] public static extern
		int PixelFlavor();
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPixelFlavor")] public static extern
		int SetPixelFlavor(int flavor);

		public const int CHOCOLATE_PIXELS = 0;
		public const int VANILLA_PIXELS = 1;


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetLightPath")] public static extern
		int SetLightPath(bool bTransmissive);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoBright")] public static extern
		int SetAutoBright(bool bOn);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetGamma")] public static extern
		int SetGamma(double dGamma);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetShadow")] public static extern
		int SetShadow(double d);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetHighlight")] public static extern
		int SetHighlight(double d);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetRegion")] public static extern
		void SetRegion(double L, double T, double R, double B);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetRegion")] public static extern
		void ResetRegion();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetImageLayout")] public static extern
		int SetImageLayout(double L, double T, double R, double B);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetImageLayout")] public static extern
		int GetImageLayout(out double L, out double T, out double R, out double B);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDefaultImageLayout")] public static extern
		int GetDefaultImageLayout(out double L, out double T, out double R, out double B);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetImageLayout")] public static extern
		int ResetImageLayout();


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetFrame")] public static extern
		int SetFrame(double L, double T, double R, double B);



		public const int TWON_ARRAY = 3;
		public const int TWON_ENUMERATION = 4;
		public const int TWON_ONEVALUE = 5;
		public const int TWON_RANGE = 6;


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		void CONTAINER_Free(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_Copy(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		bool CONTAINER_Equal(int hconA, int hconB);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_Format(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_ItemCount(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_ItemType(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_TypeSize(int nItemType);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double CONTAINER_FloatValue(int hcon, int n);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_IntValue(int hcon, int n);


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_ContainsValue(int hcon, double d);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_FindValue(int hcon, double d);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double CONTAINER_CurrentValue(int hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double CONTAINER_DefaultValue(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_DefaultIndex(int hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_CurrentIndex(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double CONTAINER_MinValue(int hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double CONTAINER_MaxValue(int hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		double CONTAINER_StepSize(int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_OneValue(int nItemType, double dVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_Range(int nItemType, double dMin, double dMax, double dStep);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_Array(int nItemType, int nItems);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_Enumeration(int nItemType, int nItems);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SetItem(int hcon, int n, double dVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SetItemString(int hcon, int n, string sVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SetItemFrame(int hcon, int n, double l, double t, double r, double b);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SelectDefaultValue(int hcon, double dVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SelectDefaultItem(int hcon, int n);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SelectCurrentValue(int hcon, double dVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_SelectCurrentItem(int hcon, int n);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_DeleteItem(int hcon, int n);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_InsertItem(int hcon, int n, double dVal);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		int CONTAINER_Wrap(int nFormat, System.IntPtr hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr CONTAINER_Unwrap(int hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		System.IntPtr CONTAINER_Handle(int hcon);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
		bool CONTAINER_IsValid(int hcon);


 
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Get")] public static extern
		int Get(int uCap);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDefault")] public static extern
		int GetDefault(int uCap);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrent")] public static extern
		int GetCurrent(int uCap);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Set")] public static extern
		int Set(int uCap, int hcon);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Reset")] public static extern
		int Reset(int uCap);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapBool")] public static extern
		bool GetCapBool(int cap, bool bDefault);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapFix32")] public static extern
		double GetCapFix32(int cap, double dDefault);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapUint16")] public static extern
		int GetCapUint16(int cap, int nDefault);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapFix32")] public static extern
		int SetCapFix32(int Cap, double dVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapOneValue")] public static extern
		int SetCapOneValue(int Cap, int ItemType, int ItemVal);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapFix32R")] public static extern
		int SetCapFix32R(int Cap, int Numerator, int Denominator);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapCurrent")] public static extern
		int GetCapCurrent(int Cap, int ItemType, System.IntPtr pVal);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ToFix32")] public static extern
		int ToFix32(double d);
		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ToFix32R")] public static extern
		int ToFix32R(int Numerator, int Denominator);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Fix32ToFloat")] public static extern
		double Fix32ToFloat(int nfix);



		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DS")] public static extern
		int DS(int DG, int DAT, int MSG, System.IntPtr pData);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Mgr")] public static extern
		int Mgr(int DG, int DAT, int MSG, System.IntPtr pData);




		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AutoClickButton")] public static extern
		void AutoClickButton(string sButtonName);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BreakModalLoop")] public static extern
		void BreakModalLoop();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EmptyMessageQueue")] public static extern
		void EmptyMessageQueue();


		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BuildName")] public static extern
		string BuildName();

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetBuildName")] public static extern
		void GetBuildName(StringBuilder sName);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSourceIdentity")] public static extern
		int GetSourceIdentity(System.IntPtr ptwid);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetImageInfo")] public static extern
		int GetImageInfo(System.IntPtr ptwinfo);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LogFile")] public static extern
		void LogFile(int fLog);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_WriteToLog")] public static extern
		void WriteToLog(string sText);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SelfTest")] public static extern
		int SelfTest(int f);

		[DllImport("Eztwain3.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Blocked")] public static extern
		int Blocked();


		public const int CAP_XFERCOUNT = 1;
		public const int ICAP_COMPRESSION = 256;
		public const int ICAP_PIXELTYPE = 257;
		public const int ICAP_UNITS = 258;
		public const int ICAP_XFERMECH = 259;
		public const int CAP_AUTHOR = 4096;
		public const int CAP_CAPTION = 4097;
		public const int CAP_FEEDERENABLED = 4098;
		public const int CAP_FEEDERLOADED = 4099;
		public const int CAP_TIMEDATE = 4100;
		public const int CAP_SUPPORTEDCAPS = 4101;
		public const int CAP_EXTENDEDCAPS = 4102;
		public const int CAP_AUTOFEED = 4103;
		public const int CAP_CLEARPAGE = 4104;
		public const int CAP_FEEDPAGE = 4105;
		public const int CAP_REWINDPAGE = 4106;
		public const int CAP_INDICATORS = 4107;
		public const int CAP_SUPPORTEDCAPSEXT = 4108;
		public const int CAP_PAPERDETECTABLE = 4109;
		public const int CAP_UICONTROLLABLE = 4110;
		public const int CAP_DEVICEONLINE = 4111;
		public const int CAP_AUTOSCAN = 4112;
		public const int CAP_THUMBNAILSENABLED = 4113;
		public const int CAP_DUPLEX = 4114;
		public const int CAP_DUPLEXENABLED = 4115;
		public const int CAP_ENABLEDSUIONLY = 4116;
		public const int CAP_CUSTOMDSDATA = 4117;
		public const int CAP_ENDORSER = 4118;
		public const int CAP_JOBCONTROL = 4119;
		public const int CAP_ALARMS = 4120;
		public const int CAP_ALARMVOLUME = 4121;
		public const int CAP_AUTOMATICCAPTURE = 4122;
		public const int CAP_TIMEBEFOREFIRSTCAPTURE = 4123;
		public const int CAP_TIMEBETWEENCAPTURES = 4124;
		public const int CAP_CLEARBUFFERS = 4125;
		public const int CAP_MAXBATCHBUFFERS = 4126;
		public const int CAP_DEVICETIMEDATE = 4127;
		public const int CAP_POWERSUPPLY = 4128;
		public const int CAP_CAMERAPREVIEWUI = 4129;
		public const int CAP_DEVICEEVENT = 4130;
		public const int CAP_PAGEMULTIPLEACQUIRE = 4131;
		public const int CAP_SERIALNUMBER = 4132;
		public const int CAP_FILESYSTEM = 4133;
		public const int CAP_PRINTER = 4134;
		public const int CAP_PRINTERENABLED = 4135;
		public const int CAP_PRINTERINDEX = 4136;
		public const int CAP_PRINTERMODE = 4137;
		public const int CAP_PRINTERSTRING = 4138;
		public const int CAP_PRINTERSUFFIX = 4139;
		public const int CAP_LANGUAGE = 4140;
		public const int CAP_FEEDERALIGNMENT = 4141;
		public const int CAP_FEEDERORDER = 4142;
		public const int CAP_PAPERBINDING = 4143;
		public const int CAP_REACQUIREALLOWED = 4144;
		public const int CAP_PASSTHRU = 4145;
		public const int CAP_BATTERYMINUTES = 4146;
		public const int CAP_BATTERYPERCENTAGE = 4147;
		public const int CAP_POWERDOWNTIME = 4148;
		public const int ICAP_AUTOBRIGHT = 4352;
		public const int ICAP_BRIGHTNESS = 4353;
		public const int ICAP_CONTRAST = 4355;
		public const int ICAP_CUSTHALFTONE = 4356;
		public const int ICAP_EXPOSURETIME = 4357;
		public const int ICAP_FILTER = 4358;
		public const int ICAP_FLASHUSED = 4359;
		public const int ICAP_GAMMA = 4360;
		public const int ICAP_HALFTONES = 4361;
		public const int ICAP_HIGHLIGHT = 4362;
		public const int ICAP_IMAGEFILEFORMAT = 4364;
		public const int ICAP_LAMPSTATE = 4365;
		public const int ICAP_LIGHTSOURCE = 4366;
		public const int ICAP_ORIENTATION = 4368;
		public const int ICAP_PHYSICALWIDTH = 4369;
		public const int ICAP_PHYSICALHEIGHT = 4370;
		public const int ICAP_SHADOW = 4371;
		public const int ICAP_FRAMES = 4372;
		public const int ICAP_XNATIVERESOLUTION = 4374;
		public const int ICAP_YNATIVERESOLUTION = 4375;
		public const int ICAP_XRESOLUTION = 4376;
		public const int ICAP_YRESOLUTION = 4377;
		public const int ICAP_MAXFRAMES = 4378;
		public const int ICAP_TILES = 4379;
		public const int ICAP_BITORDER = 4380;
		public const int ICAP_CCITTKFACTOR = 4381;
		public const int ICAP_LIGHTPATH = 4382;
		public const int ICAP_PIXELFLAVOR = 4383;
		public const int ICAP_PLANARCHUNKY = 4384;
		public const int ICAP_ROTATION = 4385;
		public const int ICAP_SUPPORTEDSIZES = 4386;
		public const int ICAP_THRESHOLD = 4387;
		public const int ICAP_XSCALING = 4388;
		public const int ICAP_YSCALING = 4389;
		public const int ICAP_BITORDERCODES = 4390;
		public const int ICAP_PIXELFLAVORCODES = 4391;
		public const int ICAP_JPEGPIXELTYPE = 4392;
		public const int ICAP_TIMEFILL = 4394;
		public const int ICAP_BITDEPTH = 4395;
		public const int ICAP_BITDEPTHREDUCTION = 4396;
		public const int ICAP_UNDEFINEDIMAGESIZE = 4397;
		public const int ICAP_IMAGEDATASET = 4398;
		public const int ICAP_EXTIMAGEINFO = 4399;
		public const int ICAP_MINIMUMHEIGHT = 4400;
		public const int ICAP_MINIMUMWIDTH = 4401;
		public const int ICAP_AUTODISCARDBLANKPAGES = 4404;
		public const int ICAP_FLIPROTATION = 4406;
		public const int ICAP_BARCODEDETECTIONENABLED = 4407;
		public const int ICAP_SUPPORTEDBARCODETYPES = 4408;
		public const int ICAP_BARCODEMAXSEARCHPRIORITIES = 4409;
		public const int ICAP_BARCODESEARCHPRIORITIES = 4410;
		public const int ICAP_BARCODESEARCHMODE = 4411;
		public const int ICAP_BARCODEMAXRETRIES = 4412;
		public const int ICAP_BARCODETIMEOUT = 4413;
		public const int ICAP_ZOOMFACTOR = 4414;
		public const int ICAP_PATCHCODEDETECTIONENABLED = 4415;
		public const int ICAP_SUPPORTEDPATCHCODETYPES = 4416;
		public const int ICAP_PATCHCODEMAXSEARCHPRIORITIES = 4417;
		public const int ICAP_PATCHCODESEARCHPRIORITIES = 4418;
		public const int ICAP_PATCHCODESEARCHMODE = 4419;
		public const int ICAP_PATCHCODEMAXRETRIES = 4420;
		public const int ICAP_PATCHCODETIMEOUT = 4421;
		public const int ICAP_FLASHUSED2 = 4422;
		public const int ICAP_IMAGEFILTER = 4423;
		public const int ICAP_NOISEFILTER = 4424;
		public const int ICAP_OVERSCAN = 4425;
		public const int ICAP_AUTOMATICBORDERDETECTION = 4432;
		public const int ICAP_AUTOMATICDESKEW = 4433;
		public const int ICAP_AUTOMATICROTATE = 4434;

		public const int TWTY_INT8 = 0;
		public const int TWTY_INT16 = 1;
		public const int TWTY_INT32 = 2;
		public const int TWTY_UINT8 = 3;
		public const int TWTY_UINT16 = 4;
		public const int TWTY_UINT32 = 5;
		public const int TWTY_BOOL = 6;
		public const int TWTY_FIX32 = 7;
		public const int TWTY_FRAME = 8;
		public const int TWTY_STR32 = 9;
		public const int TWTY_STR64 = 10;
		public const int TWTY_STR128 = 11;
		public const int TWTY_STR255 = 12;





    }
} // namespace
*/