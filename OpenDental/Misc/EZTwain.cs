//  EZTwain for C#.
//  XDefs translation of \EZTwain4\VC\Eztwain.h
using System.Runtime.InteropServices;
using System.Text;

namespace OpenDental{

    public abstract class EZTwain{


        /// <summary>
        /// Gets the EZImage as .Net image object.
        /// </summary>
        /// <returns>.Net image object.</returns>
        public static System.Drawing.Image DIB_ToImage(System.IntPtr hdib){
            System.Drawing.Image convertedImage = null;
            if( hdib != System.IntPtr.Zero ) {
                byte[] buffer = new byte[ DIB_Size( hdib ) + 100 ];
                DIB_WriteToBuffer(hdib, EZT_FF_BMP, buffer, buffer.Length );
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream( buffer, false );
                System.Drawing.Image temporaryBitmap = System.Drawing.Bitmap.FromStream( ((System.IO.Stream)memoryStream) );
                convertedImage = (System.Drawing.Image)temporaryBitmap.Clone();
                temporaryBitmap.Dispose();
            }
            return convertedImage;
        }
        
        public static System.IntPtr DIB_FromStream(System.IO.Stream stream){
            System.IntPtr hdib = System.IntPtr.Zero;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            hdib = DIB_LoadFromBuffer(buffer, buffer.Length);
            return hdib;
        }
        
        public static System.IntPtr DIB_FromImage(System.Drawing.Image image){
            System.IO.MemoryStream imgStream = new System.IO.MemoryStream();
            image.Save(imgStream, System.Drawing.Imaging.ImageFormat.Bmp);
            imgStream.Seek(0, System.IO.SeekOrigin.Begin);
            return DIB_FromStream(imgStream);
        }

        


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Testing123")] public static extern
        System.IntPtr Testing123(string s, int n, System.IntPtr h, double d, int u);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetAll")] public static extern
        void ResetAll();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Acquire")] public static extern
        System.IntPtr Acquire(System.IntPtr hwndApp);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SelectImageSource")] public static extern
        bool SelectImageSource(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireToFilename")] public static extern
        int AcquireToFilename(System.IntPtr hwndApp, string sFileName);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireMultipageFile")] public static extern
        int AcquireMultipageFile(System.IntPtr hwndApp, string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireToArray")] public static extern
        int AcquireToArray(System.IntPtr hwnd, [MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int nMax);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireImagesToFiles")] public static extern
        int AcquireImagesToFiles(System.IntPtr hwndApp, string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquirePagesToFiles")] public static extern
        int AcquirePagesToFiles(System.IntPtr hwnd, int nPPF, string sFile);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireMultipage")] public static extern
        int AcquireMultipage(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquiredFileCount")] public static extern
        int AcquiredFileCount();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireCompressed")] public static extern
        System.IntPtr AcquireCompressed(System.IntPtr hwndApp);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireCount")] public static extern
        int AcquireCount();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PromptToContinue")] public static extern
        bool PromptToContinue(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetScanAnotherPagePrompt")] public static extern
        void SetScanAnotherPagePrompt(string sPrompt);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetDefaultScanAnotherPagePrompt")] public static extern
        int SetDefaultScanAnotherPagePrompt(int fYes);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DoSettingsDialog")] public static extern
        int DoSettingsDialog(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EnableSourceUiOnly")] public static extern
        bool EnableSourceUiOnly(System.IntPtr hwnd);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetMultiTransfer")] public static extern
        void SetMultiTransfer(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetMultiTransfer")] public static extern
        bool GetMultiTransfer();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetHideUI")] public static extern
        void SetHideUI(bool bHide);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetHideUI")] public static extern
        bool GetHideUI();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetStopOnEmpty")] public static extern
        void SetStopOnEmpty(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetStopOnEmpty")] public static extern
        bool GetStopOnEmpty();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DisableParent")] public static extern
        void DisableParent(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDisableParent")] public static extern
        bool GetDisableParent();



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EasyVersion")] public static extern
        int EasyVersion();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EasyBuild")] public static extern
        int EasyBuild();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsAvailable")] public static extern
        bool IsAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsMultipageAvailable")] public static extern
        bool IsMultipageAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_State")] public static extern
        int State();
        public const int TWAIN_PRESESSION = 1;
        public const int TWAIN_SM_LOADED = 2;
        public const int TWAIN_SM_OPEN = 3;
        public const int TWAIN_SOURCE_OPEN = 4;
        public const int TWAIN_SOURCE_ENABLED = 5;
        public const int TWAIN_TRANSFER_READY = 6;
        public const int TWAIN_TRANSFERRING = 7;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsDone")] public static extern
        bool IsDone();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SourceName")] private static extern
        System.IntPtr SourceNamePtr();
        public static string SourceName()
        { return Marshal.PtrToStringAnsi(SourceNamePtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSourceName")] public static extern
        void GetSourceName(StringBuilder sName);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_IsValid(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Depth(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_BitsPerPixel(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PixelType(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Width(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Height(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetResolution(System.IntPtr hdib, double xdpi, double ydpi);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetResolutionInt(System.IntPtr hdib, int xdpi, int ydpi);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_XResolution(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_YResolution(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_XResolutionInt(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_YResolutionInt(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_PhysicalWidth(System.IntPtr hdib, int nUnits);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_PhysicalHeight(System.IntPtr hdib, int nUnits);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_RowBytes(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ColorCount(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_SamplesPerPixel(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_BitsPerSample(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_IsCompressed(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Compression(System.IntPtr hdib);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Size(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadData(System.IntPtr hdib, [MarshalAs(UnmanagedType.LPArray)] byte[] pdata, int nbMax);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadRow(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] prow);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadRowRGB(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] prow);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadRowGray(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] prow);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadRowChannel(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] prow, int nChannel);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadRowSample(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] prow, int nSample);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadPixelRGB(System.IntPtr hdib, int x, int y, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadPixelGray(System.IntPtr hdib, int x, int y, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadPixelChannel(System.IntPtr hdib, int x, int y, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int nChannel);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_WriteRow(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] pdata);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_WriteRowChannel(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] pdata, int nChannel);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_WriteRowSample(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] psrc, int nSample);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_WriteRowSafe(System.IntPtr hdib, int r, [MarshalAs(UnmanagedType.LPArray)] byte[] pdata, int nbMax);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ReadRowSafe(System.IntPtr hdib, int nRow, [MarshalAs(UnmanagedType.LPArray)] byte[] prow, int nbMax);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Allocate(int nDepth, int nWidth, int nHeight);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Create(int nPixelType, int nWidth, int nHeight, int nDepth);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Free(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_FreeArray([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int n);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_InUseCount();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Copy(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_Equal(System.IntPtr hdib1, System.IntPtr hdib2);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_AlmostEqual(System.IntPtr hdib1, System.IntPtr hdib2, int nMaxErr);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_MaxError(System.IntPtr hdib1, System.IntPtr hdib2);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetGrayColorTable(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetColorTableRGB(System.IntPtr hdib, int i, int R, int G, int B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_IsVanilla(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_IsChocolate(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ColorTableR(System.IntPtr hdib, int i);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ColorTableG(System.IntPtr hdib, int i);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ColorTableB(System.IntPtr hdib, int i);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_FlipVertical(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_FlipHorizontal(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Rotate180(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Rotate90(System.IntPtr hOld, int nSteps);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_InPlaceRotate90(System.IntPtr hdib, int nSteps);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Fill(System.IntPtr hdib, int R, int G, int B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_FillRectWithColor(System.IntPtr hdib, int x, int y, int w, int h, int R, int G, int B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_FillRectWithColorAlpha(System.IntPtr hdib, int x, int y, int w, int h, int R, int G, int B, int A);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Negate(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_AdjustBC(System.IntPtr hdib, int nB, int nC);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ApplyToneMap8(System.IntPtr hdib, [MarshalAs(UnmanagedType.LPArray)] byte[] map);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_AutoContrast(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Convolve(System.IntPtr hdibDst, System.IntPtr hdibKernel, double dNorm, int nOffset);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Correlate(System.IntPtr hdibDst, System.IntPtr hdibKernel);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_CrossCorrelate(System.IntPtr hdibDst, System.IntPtr hdibTemplate, double dScale, int nMin);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_HorizontalDifference(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_HorizontalCorrelation(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_VerticalCorrelation(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_MedianFilter(System.IntPtr hdib, int W, int H, int nStyle);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_MeanFilter(System.IntPtr hdib, int W, int H);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Smooth(System.IntPtr hdib, double sigma, double opacity);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Sobel(System.IntPtr hdib, int mode, int Thresh);
        public const int SOBEL_HORIZONTAL = 0;
        public const int SOBEL_VERTICAL = 1;
        public const int SOBEL_SUM = 2;
        public const int SOBEL_MAX = 3;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ScaledCopy(System.IntPtr hOld, int w, int h);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_Resize(System.IntPtr hdib, int w, int h);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ScaleToGray(System.IntPtr hdibOld, int nRatio);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Thumbnail(System.IntPtr hdibSource, int MaxWidth, int MaxHeight);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_Resample(System.IntPtr hOld, double xdpi, double ydpi);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_RegionCopy(System.IntPtr hOld, int leftx, int topy, int w, int h, int FillByte);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_AutoCrop(System.IntPtr hOld, int nOpts);

        public const int AUTOCROP_DARK = 1;
        public const int AUTOCROP_LIGHT = 2;
        public const int AUTOCROP_EDGE = 4;
        public const int AUTOCROP_CHECK = 8;
        public const int AUTOCROP_CHECK_BACK = 16;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_GetCropRect(System.IntPtr hdib, int nOptions, out int cropx, out int cropy, out int cropw, out int croph);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_AutoDeskew(System.IntPtr hOld, int nOptions);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_DeskewAngle(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SkewByDegrees(System.IntPtr hdib, double dAngle);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ConvertToPixelType(System.IntPtr hOld, int nPT);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ConvertToFormat(System.IntPtr hOld, int nPT, int nBPP);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_SmartThreshold(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_SimpleThreshold(System.IntPtr hdib, int nT);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_SetConversionThreshold(int nT);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ConversionThreshold();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_FindAdaptiveGlobalThreshold(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ErrorDiffuse(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetConversionColorCount(int n);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ConversionColorCount();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SwapRedBlue(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_CreatePalette(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetColorModel(System.IntPtr hdib, int nCM);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_ColorModel(System.IntPtr hdib);
        public const int EZT_CM_RGB = 0;
        public const int EZT_CM_GRAY = 3;
        public const int EZT_CM_CMYK = 5;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetColorCount(System.IntPtr hdib, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_Blt(System.IntPtr hdibDst, int dx, int dy, System.IntPtr hdibSrc, int sx, int sy, int w, int h, int uRop);
        public const int EZT_ROP_COPY = 0;
        public const int EZT_ROP_OR = 1;
        public const int EZT_ROP_AND = 2;
        public const int EZT_ROP_XOR = 3;
        public const int EZT_ROP_ANDNOT = 0x12;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_BltMask(System.IntPtr hdibDst, int dx, int dy, System.IntPtr hdibSrc, int sx, int sy, int w, int h, int uRop, System.IntPtr hdibMask);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_PaintMask(System.IntPtr hdibDst, int dx, int dy, int R, int G, int B, int sx, int sy, int w, int h, int uRop, System.IntPtr hdibMask);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_DrawLine(System.IntPtr hdibDst, int x1, int y1, int x2, int y2, int R, int G, int B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_DrawText(System.IntPtr hdibDst, string sText, int leftx, int topy, int w, int h);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_ResetTextDrawing();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetTextColor(int R, int G, int B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_TextColor();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_GetTextColor(out int pR, out int pG, out int pB);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetTextAngle(int nDegrees);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetTextHeight(int nH);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetTextFace(string sTypeface);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetTextFormat(int nFlags);
        public const int EZT_TEXT_NORMAL = 0x0;
        public const int EZT_TEXT_BOLD = 0x1;
        public const int EZT_TEXT_ITALIC = 0x2;
        public const int EZT_TEXT_UNDERLINE = 0x4;
        public const int EZT_TEXT_STRIKEOUT = 0x8;
        public const int EZT_TEXT_BOTTOM = 0x100;
        public const int EZT_TEXT_VCENTER = 0x200;
        public const int EZT_TEXT_TOP = 0x0;
        public const int EZT_TEXT_LEFT = 0x0;
        public const int EZT_TEXT_CENTER = 0x1000;
        public const int EZT_TEXT_RIGHT = 0x2000;
        public const int EZT_TEXT_WRAP = 0x4000;
        public const int EZT_TEXT_JUSTIFY = 0x800;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetTextBackgroundColor(int R, int G, int B, int A);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_View(System.IntPtr hdib, string sTitle, System.IntPtr hwndParent);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_SetViewOption(string sOption, string sValue);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_SetViewImage(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_IsViewOpen();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_ViewClose();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_DrawOnWindow(System.IntPtr hdib, System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_DrawToDC(System.IntPtr hdib, System.IntPtr hDC, int dx, int dy, int w, int h, int sx, int sy);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_SpecifyPrinter(string sPrinterName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_EnumeratePrinters();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="DIB_PrinterName")] private static extern
        System.IntPtr DIB_PrinterNamePtr(int i);
        public static string DIB_PrinterName(int i)
        { return Marshal.PtrToStringAnsi(DIB_PrinterNamePtr(i)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_GetPrinterName(int i, StringBuilder PrinterName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetPrintToFit(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_GetPrintToFit();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Print(System.IntPtr hdib, string sJobname);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PrintNoPrompt(System.IntPtr hdib, string sJobname);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PrintFile")] public static extern
        int PrintFile(string sFilename, string sJobname, bool bNoPrompt);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PrintFile(string sFilename, string sJobname, bool bNoPrompt);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PrintArray([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int nCount, string sJobname, bool bNoPrompt);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SetNextPrintJobPageCount(int nPages);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PrintJobBegin(string sJobname, bool bUseDefaultPrinter);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PrintPage(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PrintJobEnd();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_PutOnClipboard(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_CanGetFromClipboard();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_GetFromClipboard();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_FromClipboard();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_OpenInDC(System.IntPtr hdib, System.IntPtr hdc);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_CloseInDC(System.IntPtr hdib, System.IntPtr hdc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_WriteToFilename(System.IntPtr hdib, string sFileName);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_LoadFromFilename(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_FormatOfFile(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_SelectPageToLoad(int nPage);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_GetFilePageCount(string sFileName);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_FilePageCount(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_LoadPage(string sFileName, int nPage);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_LoadArrayFromFilename([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int nMax, string sFilename);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_LoadPagesFromFilename([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int index0, int nMax, string sFilename);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_FormatOfBuffer([MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_PageCountOfBuffer([MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_BufferPageCount([MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_LoadFromBuffer([MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_LoadPageFromBuffer([MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes, int nPage);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_LoadArrayFromBuffer([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int nMax, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_LoadFaxData(System.IntPtr hdib, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBytes, int nFlags);
        public const int FAX_GROUP3_2D = 0x20;
        public const int FAX_GROUP4 = 0x40;
        public const int FAX_BYTE_ALIGNED = 0x80;
        public const int FAX_REQUIRE_EOLS = 0x100;
        public const int FAX_EXPECT_EOB = 0x200;
        public const int FAX_VANILLA = 0x400;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_WriteToBuffer(System.IntPtr hdib, int nFormat, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nbMax);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_WriteArrayToBuffer([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int n, int nFormat, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nbMax);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ToDibSection(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_FromBitmap(System.IntPtr hbm, System.IntPtr hdc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DIB_IsBlank(System.IntPtr hdib, double dDarkness);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_Darkness(System.IntPtr hdibFull);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DIB_GetHistogram(System.IntPtr hdib, int nComponent, [MarshalAs(UnmanagedType.LPArray)] int[] histo);
        public const int COMPONENT_GRAY = 0;
        public const int COMPONENT_RED = 1;
        public const int COMPONENT_GREEN = 2;
        public const int COMPONENT_BLUE = 3;
        public const int COMPONENT_LUMINANCE = 0;
        public const int COMPONENT_SAT = 4;
        public const int COMPONENT_HUE = 5;



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ComponentCopy(System.IntPtr hdib, int nComponent);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_Avg(System.IntPtr hdib, int nComp);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_AvgRegion(System.IntPtr hdib, int nComp, int leftx, int topy, int w, int h);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_AvgRow(System.IntPtr hdib, int nComp, int rowy);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double DIB_AvgColumn(System.IntPtr hdib, int nComp, int colx);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_GetBrightRects(System.IntPtr hdib, int w, int h, int t, [MarshalAs(UnmanagedType.LPArray)] int[] xBlob, [MarshalAs(UnmanagedType.LPArray)] int[] yBlob, [MarshalAs(UnmanagedType.LPArray)] int[] wBlob, [MarshalAs(UnmanagedType.LPArray)] int[] hBlob, int nMax);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ProjectRows(System.IntPtr hdib, int nComp);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ProjectColumns(System.IntPtr hdib, int leftx, int topy, int w, int h, int nComp);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_Posterize(System.IntPtr hdib, int nLevels);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DIB_ForwardDCT(System.IntPtr hdib);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DOC_CreateEmpty();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DOC_Destroy(System.IntPtr hdoc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DOC_ImageCount(System.IntPtr hdoc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_IsModified(System.IntPtr hdoc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DOC_SetModified(System.IntPtr hdoc, bool bIsMod);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="DOC_Filename")] private static extern
        System.IntPtr DOC_FilenamePtr(System.IntPtr hdoc);
        public static string DOC_Filename(System.IntPtr hdoc)
        { return Marshal.PtrToStringAnsi(DOC_FilenamePtr(hdoc)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_SetCurPos(System.IntPtr hdoc, int i);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DOC_CurPos(System.IntPtr hdoc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DOC_OpenReadOnly(string sFilename);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DOC_OpenForUpdate(string sFilename);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void DOC_Reset(System.IntPtr hdoc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_WriteToFile(System.IntPtr hdoc, string filename);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_Save(System.IntPtr hdoc);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_SaveAs(System.IntPtr hdoc, string filename);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DOC_Image(System.IntPtr hdoc, int i);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_SetImage(System.IntPtr hdoc, int i, System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_AppendImage(System.IntPtr hdoc, System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr DOC_ExtractImages(System.IntPtr hdoc, int i, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_DeleteImage(System.IntPtr hdoc, int i);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_DeleteImages(System.IntPtr hdoc, int i, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_InsertImage(System.IntPtr hdoc, int i, System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_InsertImageArray(System.IntPtr hdoc, int i, [MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool DOC_MoveImage(System.IntPtr hdoc, int iOld, int iNew);


        public const int EZT_FF_TIFF = 0;
        public const int EZT_FF_BMP = 2;
        public const int EZT_FF_JFIF = 4;
        public const int EZT_FF_PNG = 7;
        public const int EZT_FF_PDFA = 15;
        public const int EZT_FF_DCX = 97;
        public const int EZT_FF_GIF = 98;
        public const int EZT_FF_PDF = 99;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetFileAppendFlag")] public static extern
        void SetFileAppendFlag(bool bAppend);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetFileAppendFlag")] public static extern
        bool GetFileAppendFlag();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsJpegAvailable")] public static extern
        bool IsJpegAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsPngAvailable")] public static extern
        bool IsPngAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsTiffAvailable")] public static extern
        bool IsTiffAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsPdfAvailable")] public static extern
        bool IsPdfAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsGifAvailable")] public static extern
        bool IsGifAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsDcxAvailable")] public static extern
        bool IsDcxAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFormatAvailable")] public static extern
        bool IsFormatAvailable(int nFF);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_FormatVersion")] public static extern
        int FormatVersion(int nFF);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFileExtensionAvailable")] public static extern
        bool IsFileExtensionAvailable(string sExt);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_FormatFromExtension")] public static extern
        int FormatFromExtension(string sExt, int nFF);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ExtensionFromFormat")] private static extern
        System.IntPtr ExtensionFromFormatPtr(int nFF, string sDefExt);
        public static string ExtensionFromFormat(int nFF, string sDefExt)
        { return Marshal.PtrToStringAnsi(ExtensionFromFormatPtr(nFF,sDefExt)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetExtensionFromFormat")] public static extern
        void GetExtensionFromFormat(int nFF, string sDefExt, StringBuilder szExtension);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetSaveFormat")] public static extern
        bool SetSaveFormat(int nFF);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSaveFormat")] public static extern
        int GetSaveFormat();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetJpegQuality")] public static extern
        void SetJpegQuality(int nQ);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetJpegQuality")] public static extern
        int GetJpegQuality();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffStripSize")] public static extern
        void SetTiffStripSize(int nBytes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetTiffStripSize")] public static extern
        int GetTiffStripSize();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffImageDescription")] public static extern
        bool SetTiffImageDescription(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffDocumentName")] public static extern
        bool SetTiffDocumentName(string sText);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffCompression")] public static extern
        bool SetTiffCompression(int nPT, int nComp);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetTiffCompression")] public static extern
        int GetTiffCompression(int nPT);
        public const int TIFF_COMP_NONE = 1;
        public const int TIFF_COMP_CCITTRLE = 2;
        public const int TIFF_COMP_CCITTFAX3 = 3;
        public const int TIFF_COMP_CCITTFAX4 = 4;
        public const int TIFF_COMP_LZW = 5;
        public const int TIFF_COMP_JPEG = 7;
        public const int TIFF_COMP_PACKBITS = 32773;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagShort")] public static extern
        bool SetTiffTagShort(int nTagId, int sValue);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagLong")] public static extern
        bool SetTiffTagLong(int nTagId, int nValue);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagString")] public static extern
        bool SetTiffTagString(int nTagId, string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagDouble")] public static extern
        bool SetTiffTagDouble(int nTagId, double dValue);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagRational")] public static extern
        bool SetTiffTagRational(int nTagId, double dValue);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagRationalArray")] public static extern
        bool SetTiffTagRationalArray(int nTagId, [MarshalAs(UnmanagedType.LPArray)] double[] dValues, int n);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagBytes")] public static extern
        bool SetTiffTagBytes(int nTagId, [MarshalAs(UnmanagedType.LPArray)] byte[] pdata, int nBytes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiffTagUndefined")] public static extern
        bool SetTiffTagUndefined(int nTagId, [MarshalAs(UnmanagedType.LPArray)] byte[] pdata, int nBytes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetTiffTags")] public static extern
        void ResetTiffTags();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetTiffTagAscii")] public static extern
        bool GetTiffTagAscii(string sFilename, int nPage, int nTag, int nLen, StringBuilder buffer);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_TiffTagAscii")] private static extern
        System.IntPtr TiffTagAsciiPtr(string sFilename, int nPage, int nTag);
        public static string TiffTagAscii(string sFilename, int nPage, int nTag)
        { return Marshal.PtrToStringAnsi(TiffTagAsciiPtr(sFilename,nPage,nTag)); }



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_IsOneOfOurs(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="PDF_DocumentProperty")] private static extern
        System.IntPtr PDF_DocumentPropertyPtr(string sFilename, string sProperty);
        public static string PDF_DocumentProperty(string sFilename, string sProperty)
        { return Marshal.PtrToStringAnsi(PDF_DocumentPropertyPtr(sFilename,sProperty)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int PDF_GetDocumentProperty(string sFilename, string sProperty, StringBuilder buffer, int buflen);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPdfTitle")] public static extern
        bool SetPdfTitle(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPdfAuthor")] public static extern
        bool SetPdfAuthor(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPdfSubject")] public static extern
        bool SetPdfSubject(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPdfKeywords")] public static extern
        bool SetPdfKeywords(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPdfCreator")] public static extern
        bool SetPdfCreator(string sText);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetTitle(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetAuthor(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetSubject(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetKeywords(string sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetCreator(string sText);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetCompression(int nPT, int nComp);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int PDF_GetCompression(int nPT);
        public const int COMPRESSION_DEFAULT = -1;
        public const int COMPRESSION_NONE = 1;
        public const int COMPRESSION_FLATE = 5;
        public const int COMPRESSION_JPEG = 7;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SelectPageSize(int nPaper);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int PDF_SelectedPageSize();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetPDFACompliance(int nLevel);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int PDF_GetPDFACompliance();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_SetAutoSearchable(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_GetAutoSearchable();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_IsEncrypted(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetOpenPassword(string sPassword);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetUserPassword(string sPassword);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetOwnerPassword(string sPassword);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetPermissions(int nPermission);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int PDF_GetPermissions();
        public const int PDF_PERMIT_PRINT = 4;
        public const int PDF_PERMIT_MODIFY = 8;
        public const int PDF_PERMIT_COPY = 16;
        public const int PDF_PERMIT_ANNOTS = 32;
        public const int PDF_PERMIT_ALL = -1;



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_DrawText(double leftx, double topy, string sText);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_DrawInvisibleText(double leftx, double topy, string sText);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetTextVisible(bool bVisible);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_GetTextVisible();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetTextSize(double dfs);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void PDF_SetTextHorizontalScaling(double dhs);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool PDF_WriteOcrText(string text, [MarshalAs(UnmanagedType.LPArray)] int[] ax, [MarshalAs(UnmanagedType.LPArray)] int[] ay, [MarshalAs(UnmanagedType.LPArray)] int[] aw, [MarshalAs(UnmanagedType.LPArray)] int[] ah, double xdpi, double ydpi);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_FormatOfFile")] public static extern
        int FormatOfFile(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PagesInFile")] public static extern
        int PagesInFile(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PromptForOpenFilename")] public static extern
        bool PromptForOpenFilename(StringBuilder sFileName);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ViewFile")] public static extern
        int ViewFile(string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetViewOption")] public static extern
        bool SetViewOption(string sOption, string sValue);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsViewOpen")] public static extern
        bool IsViewOpen();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ViewClose")] public static extern
        bool ViewClose();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetLastViewPosition")] public static extern
        bool GetLastViewPosition(out int pleft, out int ptop, out int pwidth, out int pheight);


        public const int MULTIPAGE_TIFF = 0;
        public const int MULTIPAGE_PDF = 1;
        public const int MULTIPAGE_DCX = 2;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetMultipageFormat")] public static extern
        int SetMultipageFormat(int nFF);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetMultipageFormat")] public static extern
        int GetMultipageFormat();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetLazyWriting")] public static extern
        void SetLazyWriting(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetLazyWriting")] public static extern
        bool GetLazyWriting();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int DIB_WriteArrayToFilename([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int n, string sFileName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BeginMultipageFile")] public static extern
        int BeginMultipageFile(string sFileName);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DibWritePage")] public static extern
        int DibWritePage(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_WritePageAndFree")] public static extern
        int WritePageAndFree(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EndMultipageFile")] public static extern
        int EndMultipageFile();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_MultipageCount")] public static extern
        int MultipageCount();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsMultipageFileOpen")] public static extern
        bool IsMultipageFileOpen();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LastOutputFile")] private static extern
        System.IntPtr LastOutputFilePtr();
        public static string LastOutputFile()
        { return Marshal.PtrToStringAnsi(LastOutputFilePtr()); }


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetOutputPageCount")] public static extern
        void SetOutputPageCount(int nPages);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_FileCopy")] public static extern
        int FileCopy(string sInFile, string sOutFile, int nOptions);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool UPLOAD_IsAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_Version();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_MaxFiles();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool UPLOAD_AddFormField(string fieldName, string fieldValue);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool UPLOAD_AddHeader(string header);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool UPLOAD_AddCookie(string cookie);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void UPLOAD_EnableProgressBar(bool bEnable);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool UPLOAD_IsEnabledProgressBar();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_DibToURL(System.IntPtr hdib, string URL, string fileName, string fieldName);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_DibsToURL([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int n, string URL, string fileName, string fieldName);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_DibsSeparatelyToURL([MarshalAs(UnmanagedType.LPArray)] System.IntPtr[] ahdib, int n, string URL, string fileName, string fieldName);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_FilesToURL(string files, string URL, string fieldName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void UPLOAD_SetProxy(string hostport, string userpwd, bool bTunnel);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="UPLOAD_Response")] private static extern
        System.IntPtr UPLOAD_ResponsePtr();
        public static string UPLOAD_Response()
        { return Marshal.PtrToStringAnsi(UPLOAD_ResponsePtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int UPLOAD_ResponseLength();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void UPLOAD_GetResponse(StringBuilder ResponseText);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void UPLOAD_ClearResponse();



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAppTitle")] public static extern
        void SetAppTitle(string sAppTitle);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetApplicationKey")] public static extern
        void SetApplicationKey(int nKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ApplicationLicense")] public static extern
        void ApplicationLicense(string sAppTitle, int nAppKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_UniversalLicense")] public static extern
        void UniversalLicense(string sLicensee, int nKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_InHouseApplicationLicense")] public static extern
        void InHouseApplicationLicense(string sLicensee, int nKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_RenewTrialLicense")] public static extern
        bool RenewTrialLicense(int uKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SingleMachineLicense")] public static extern
        bool SingleMachineLicense(string sMsg);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_RegisterApp")] public static extern
        void RegisterApp(int nMajorNum, int nMinorNum, int nLanguage, int nCountry, string sVersion, string sMfg, string sFamily, string sAppTitle);

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
        public const int EZTEC_TIFF_ERR = 41;
        public const int EZTEC_PDF_WRITE_ERR = 42;
        public const int EZTEC_NO_PDF = 43;
        public const int EZTEC_GIFCON = 44;
        public const int EZTEC_FILE_READ_ERR = 45;
        public const int EZTEC_BAD_REGION = 46;
        public const int EZTEC_FILE_WRITE = 47;
        public const int EZTEC_NO_DS_OPEN = 48;
        public const int EZTEC_DCXCON = 49;
        public const int EZTEC_NO_BARCODE = 50;
        public const int EZTEC_UNK_READ_FF = 51;
        public const int EZTEC_DIB_FORMAT = 52;
        public const int EZTEC_PRINT_ERR = 53;
        public const int EZTEC_NO_DCX = 54;
        public const int EZTEC_APP_BAD_CON = 55;
        public const int EZTEC_LIC_KEY = 56;
        public const int EZTEC_INVALID_PARAM = 57;
        public const int EZTEC_INTERNAL = 58;
        public const int EZTEC_LOAD_DLL = 59;
        public const int EZTEC_CURL = 60;
        public const int EZTEC_MULTIPAGE_OPEN = 61;
        public const int EZTEC_BAD_SHUTDOWN = 62;
        public const int EZTEC_DLL_VERSION = 63;
        public const int EZTEC_OCR_ERR = 64;
        public const int EZTEC_ONLY_TO_PDF = 65;
        public const int EZTEC_APP_TITLE = 66;
        public const int EZTEC_PATH_CREATE = 67;
        public const int EZTEC_LATE_LIC = 68;
        public const int EZTEC_PDF_PASSWORD = 69;
        public const int EZTEC_PDF_UNSUPPORTED = 70;
        public const int EZTEC_PDF_BAFFLED = 71;
        public const int EZTEC_PDF_INVALID = 72;
        public const int EZTEC_PDF_COMPRESSION = 73;
        public const int EZTEC_NOT_ENOUGH_PAGES = 74;
        public const int EZTEC_DIB_ARRAY_OVERFLOW = 75;
        public const int EZTEC_DEVICE_PAPERJAM = 76;
        public const int EZTEC_DEVICE_DOUBLEFEED = 77;
        public const int EZTEC_DEVICE_COMM = 78;
        public const int EZTEC_DEVICE_INTERLOCK = 79;
        public const int EZTEC_BAD_DOC = 80;
        public const int EZTEC_OTHER_DS_OPEN = 81;
        public const int EZTEC_LIC_NO_LICENSEE = 82;
        public const int EZTEC_LIC_NO_UKEY = 83;
        public const int EZTEC_LIC_NO_APPNAME = 84;



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetResultCode")] public static extern
        int GetResultCode();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetConditionCode")] public static extern
        int GetConditionCode();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_UserClosedSource")] public static extern
        bool UserClosedSource();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ErrorBox")] public static extern
        void ErrorBox(string sMsg);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SuppressErrorMessages")] public static extern
        bool SuppressErrorMessages(bool bYes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ReportLastError")] public static extern
        void ReportLastError(string msg);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetLastErrorText")] public static extern
        void GetLastErrorText(StringBuilder sMsg);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LastErrorText")] private static extern
        System.IntPtr LastErrorTextPtr();
        public static string LastErrorText()
        { return Marshal.PtrToStringAnsi(LastErrorTextPtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LastErrorCode")] public static extern
        int LastErrorCode();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ClearError")] public static extern
        void ClearError();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_RecordError")] public static extern
        void RecordError(int code, string note);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ReportLeaks")] public static extern
        bool ReportLeaks();



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Shutdown")] public static extern
        void Shutdown();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LoadSourceManager")] public static extern
        bool LoadSourceManager();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_OpenSourceManager")] public static extern
        bool OpenSourceManager(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_OpenDefaultSource")] public static extern
        bool OpenDefaultSource();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSourceList")] public static extern
        bool GetSourceList();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetNextSourceName")] public static extern
        bool GetNextSourceName(StringBuilder sName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_NextSourceName")] private static extern
        System.IntPtr NextSourceNamePtr();
        public static string NextSourceName()
        { return Marshal.PtrToStringAnsi(NextSourceNamePtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDefaultSourceName")] public static extern
        bool GetDefaultSourceName(StringBuilder sName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DefaultSourceName")] private static extern
        System.IntPtr DefaultSourceNamePtr();
        public static string DefaultSourceName()
        { return Marshal.PtrToStringAnsi(DefaultSourceNamePtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_OpenSource")] public static extern
        bool OpenSource(string sName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EnableSource")] public static extern
        bool EnableSource(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DisableSource")] public static extern
        bool DisableSource();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CloseSource")] public static extern
        bool CloseSource();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CloseSourceManager")] public static extern
        bool CloseSourceManager(System.IntPtr hwnd);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_UnloadSourceManager")] public static extern
        bool UnloadSourceManager();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EndXfer")] public static extern
        bool EndXfer();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AbortAllPendingXfers")] public static extern
        bool AbortAllPendingXfers();



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetXferCount")] public static extern
        bool SetXferCount(int nXfers);

        public const int TWUN_INCHES = 0;
        public const int TWUN_CENTIMETERS = 1;
        public const int TWUN_PICAS = 2;
        public const int TWUN_POINTS = 3;
        public const int TWUN_TWIPS = 4;
        public const int TWUN_PIXELS = 5;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrentUnits")] public static extern
        int GetCurrentUnits();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetUnits")] public static extern
        bool SetUnits(int nUnits);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCurrentUnits")] public static extern
        bool SetCurrentUnits(int nUnits);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetBitDepth")] public static extern
        int GetBitDepth();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetBitDepth")] public static extern
        bool SetBitDepth(int nBits);

        public const int TWPT_BW = 0;
        public const int TWPT_GRAY = 1;
        public const int TWPT_RGB = 2;
        public const int TWPT_PALETTE = 3;
        public const int TWPT_CMY = 4;
        public const int TWPT_CMYK = 5;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetPixelType")] public static extern
        int GetPixelType();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPixelType")] public static extern
        bool SetPixelType(int nPixType);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCurrentPixelType")] public static extern
        bool SetCurrentPixelType(int nPixType);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrentResolution")] public static extern
        double GetCurrentResolution();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetXResolution")] public static extern
        double GetXResolution();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetYResolution")] public static extern
        double GetYResolution();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetResolution")] public static extern
        bool SetResolution(double dRes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetResolutionInt")] public static extern
        bool SetResolutionInt(int nRes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCurrentResolution")] public static extern
        bool SetCurrentResolution(double dRes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetXResolution")] public static extern
        bool SetXResolution(double dxRes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetYResolution")] public static extern
        bool SetYResolution(double dyRes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetContrast")] public static extern
        bool SetContrast(double dCon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetBrightness")] public static extern
        bool SetBrightness(double dBri);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetThreshold")] public static extern
        bool SetThreshold(double dThresh);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrentThreshold")] public static extern
        double GetCurrentThreshold();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoDeskew")] public static extern
        void SetAutoDeskew(int nMode);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetAutoDeskew")] public static extern
        int GetAutoDeskew();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetBlankPageMode")] public static extern
        void SetBlankPageMode(int nMode);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetBlankPageMode")] public static extern
        int GetBlankPageMode();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetBlankPageThreshold")] public static extern
        void SetBlankPageThreshold(double dDarkness);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetBlankPageThreshold")] public static extern
        double GetBlankPageThreshold();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BlankDiscardCount")] public static extern
        int BlankDiscardCount();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoCrop")] public static extern
        void SetAutoCrop(int nMode);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetAutoCrop")] public static extern
        int GetAutoCrop();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoCropOptions")] public static extern
        void SetAutoCropOptions(int nOpts);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetAutoCropOptions")] public static extern
        int GetAutoCropOptions();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoCropSize")] public static extern
        void SetAutoCropSize(double w, double h, int nUnits);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ClearAutoCropSize")] public static extern
        void ClearAutoCropSize();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoCropSizeRange")] public static extern
        void SetAutoCropSizeRange(double minW, double maxW, double minH, double maxH, int nUnits);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ClearAutoCropSizeRange")] public static extern
        void ClearAutoCropSizeRange();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoContrast")] public static extern
        void SetAutoContrast(int nMode);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetAutoContrast")] public static extern
        int GetAutoContrast();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoOCR")] public static extern
        void SetAutoOCR(int nMode);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetAutoOCR")] public static extern
        int GetAutoOCR();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoNegate")] public static extern
        void SetAutoNegate(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetAutoNegate")] public static extern
        bool GetAutoNegate();



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetXferMech")] public static extern
        bool SetXferMech(int mech);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_XferMech")] public static extern
        int XferMech();
        public const int XFERMECH_NATIVE = 0;
        public const int XFERMECH_FILE = 1;
        public const int XFERMECH_MEMORY = 2;
        public const int XFERMECH_FILE2 = 3;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SupportsFileXfer")] public static extern
        bool SupportsFileXfer();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPaperSize")] public static extern
        bool SetPaperSize(int nPaper);
        public const int PAPER_NONE = 0;
        public const int PAPER_A4LETTER = 1;
        public const int PAPER_A4 = 1;
        public const int PAPER_B5LETTER = 2;
        public const int PAPER_JISB5 = 2;
        public const int PAPER_USLETTER = 3;
        public const int PAPER_USLEGAL = 4;
        public const int PAPER_A5 = 5;
        public const int PAPER_B4 = 6;
        public const int PAPER_ISOB4 = 6;
        public const int PAPER_B6 = 7;
        public const int PAPER_ISOB6 = 7;
        public const int PAPER_USLEDGER = 9;
        public const int PAPER_USEXECUTIVE = 10;
        public const int PAPER_A3 = 11;
        public const int PAPER_B3 = 12;
        public const int PAPER_ISOB3 = 12;
        public const int PAPER_A6 = 13;
        public const int PAPER_C4 = 14;
        public const int PAPER_C5 = 15;
        public const int PAPER_C6 = 16;
        public const int PAPER_4A0 = 17;
        public const int PAPER_2A0 = 18;
        public const int PAPER_A0 = 19;
        public const int PAPER_A1 = 20;
        public const int PAPER_A2 = 21;
        public const int PAPER_A7 = 22;
        public const int PAPER_A8 = 23;
        public const int PAPER_A9 = 24;
        public const int PAPER_A10 = 25;
        public const int PAPER_ISOB0 = 26;
        public const int PAPER_ISOB1 = 27;
        public const int PAPER_ISOB2 = 28;
        public const int PAPER_ISOB5 = 29;
        public const int PAPER_ISOB7 = 30;
        public const int PAPER_ISOB8 = 31;
        public const int PAPER_ISOB9 = 32;
        public const int PAPER_ISOB10 = 33;
        public const int PAPER_JISB0 = 34;
        public const int PAPER_JISB1 = 35;
        public const int PAPER_JISB2 = 36;
        public const int PAPER_JISB3 = 37;
        public const int PAPER_JISB4 = 38;
        public const int PAPER_JISB6 = 39;
        public const int PAPER_JISB7 = 40;
        public const int PAPER_JISB8 = 41;
        public const int PAPER_JISB9 = 42;
        public const int PAPER_JISB10 = 43;
        public const int PAPER_C0 = 44;
        public const int PAPER_C1 = 45;
        public const int PAPER_C2 = 46;
        public const int PAPER_C3 = 47;
        public const int PAPER_C7 = 48;
        public const int PAPER_C8 = 49;
        public const int PAPER_C9 = 50;
        public const int PAPER_C10 = 51;
        public const int PAPER_USSTATEMENT = 52;
        public const int PAPER_BUSINESSCARD = 53;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetPaperDimensions")] public static extern
        bool GetPaperDimensions(int nPaper, int nUnits, out double pdW, out double pdH);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_HasFeeder")] public static extern
        bool HasFeeder();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ProbablyHasFlatbed")] public static extern
        bool ProbablyHasFlatbed();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFeederSelected")] public static extern
        bool IsFeederSelected();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SelectFeeder")] public static extern
        bool SelectFeeder(bool bYes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsAutoFeedOn")] public static extern
        bool IsAutoFeedOn();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoFeed")] public static extern
        bool SetAutoFeed(bool bYes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsFeederLoaded")] public static extern
        bool IsFeederLoaded();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsPaperDetectable")] public static extern
        bool IsPaperDetectable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoScan")] public static extern
        bool SetAutoScan(bool bYes);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CanDoDuplex")] public static extern
        bool CanDoDuplex();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDuplexSupport")] public static extern
        int GetDuplexSupport();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EnableDuplex")] public static extern
        bool EnableDuplex(bool bYes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsDuplexEnabled")] public static extern
        bool IsDuplexEnabled();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_HasControllableUI")] public static extern
        int HasControllableUI();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetIndicators")] public static extern
        bool SetIndicators(bool bVisible);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetIndicators")] public static extern
        bool GetIndicators();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Compression")] public static extern
        int Compression();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCompression")] public static extern
        bool SetCompression(int compression);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Tiled")] public static extern
        bool Tiled();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetTiled")] public static extern
        bool SetTiled(bool bTiled);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PlanarChunky")] public static extern
        int PlanarChunky();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPlanarChunky")] public static extern
        bool SetPlanarChunky(int shape);

        public const int CHUNKY_PIXELS = 0;
        public const int PLANAR_PIXELS = 1;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_PixelFlavor")] public static extern
        int PixelFlavor();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetPixelFlavor")] public static extern
        bool SetPixelFlavor(int flavor);

        public const int CHOCOLATE_PIXELS = 0;
        public const int VANILLA_PIXELS = 1;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetLightPath")] public static extern
        bool SetLightPath(bool bTransmissive);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetAutoBright")] public static extern
        bool SetAutoBright(bool bOn);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetGamma")] public static extern
        bool SetGamma(double dGamma);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetShadow")] public static extern
        bool SetShadow(double d);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetHighlight")] public static extern
        bool SetHighlight(double d);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetRegion")] public static extern
        void SetRegion(double L, double T, double R, double B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetRegion")] public static extern
        void ResetRegion();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetImageLayout")] public static extern
        bool SetImageLayout(double L, double T, double R, double B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetImageLayout")] public static extern
        bool GetImageLayout(out double L, out double T, out double R, out double B);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDefaultImageLayout")] public static extern
        bool GetDefaultImageLayout(out double L, out double T, out double R, out double B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetImageLayout")] public static extern
        bool ResetImageLayout();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetFrame")] public static extern
        bool SetFrame(double L, double T, double R, double B);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetGrayResponse")] public static extern
        bool SetGrayResponse([MarshalAs(UnmanagedType.LPArray)] int[] pcurve);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetColorResponse")] public static extern
        bool SetColorResponse([MarshalAs(UnmanagedType.LPArray)] int[] pred, [MarshalAs(UnmanagedType.LPArray)] int[] pgreen, [MarshalAs(UnmanagedType.LPArray)] int[] pblue);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetGrayResponse")] public static extern
        bool ResetGrayResponse();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ResetColorResponse")] public static extern
        bool ResetColorResponse();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool BARCODE_IsAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_Version();

        public const int EZBAR_ENGINE_NONE = 0;
        public const int EZBAR_ENGINE_NATIVE = 1;
        public const int EZBAR_ENGINE_AXTEL = 2;
        public const int EZBAR_ENGINE_LEADTOOLS15 = 3;
        public const int EZBAR_ENGINE_BLACKICE = 4;
        public const int EZBAR_ENGINE_LEADTOOLS16 = 5;
        public const int EZBAR_ENGINE_INBARCODE = 6;

        public const int EZBAR_ENGINE_LEADTOOLS = 3;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool BARCODE_IsEngineAvailable(int nEngine);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool BARCODE_SelectEngine(int nEngine);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_SelectedEngine();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="BARCODE_EngineName")] private static extern
        System.IntPtr BARCODE_EngineNamePtr(int nEngine);
        public static string BARCODE_EngineName(int nEngine)
        { return Marshal.PtrToStringAnsi(BARCODE_EngineNamePtr(nEngine)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void BARCODE_SetLicenseKey(string sKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_ReadableCodes();
        public const int EZBAR_EAN_13 = 0x1;
        public const int EZBAR_EAN_8 = 0x2;
        public const int EZBAR_UPCA = 0x4;
        public const int EZBAR_UPCE = 0x8;
        public const int EZBAR_CODE_39 = 0x10;
        public const int EZBAR_CODE_39FA = 0x20;
        public const int EZBAR_CODE_128 = 0x40;
        public const int EZBAR_CODE_I25 = 0x80;
        public const int EZBAR_CODA_BAR = 0x100;
        public const int EZBAR_UCCEAN_128 = 0x200;
        public const int EZBAR_CODE_93 = 0x400;
        public const int EZBAR_ANY = -1;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="BARCODE_TypeName")] private static extern
        System.IntPtr BARCODE_TypeNamePtr(int nType);
        public static string BARCODE_TypeName(int nType)
        { return Marshal.PtrToStringAnsi(BARCODE_TypeNamePtr(nType)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool BARCODE_SetDirectionFlags(int nDirFlags);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_GetDirectionFlags();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_AvailableDirectionFlags();

        public const int EZBAR_LEFT_TO_RIGHT = 0x1;
        public const int EZBAR_RIGHT_TO_LEFT = 0x2;
        public const int EZBAR_TOP_TO_BOTTOM = 0x4;
        public const int EZBAR_BOTTOM_TO_TOP = 0x8;
        public const int EZBAR_DIAGONAL = 0x10;
        public const int EZBAR_HORIZONTAL = 0x3;
        public const int EZBAR_VERTICAL = 0xC;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void BARCODE_SetZone(int x, int y, int w, int h);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void BARCODE_NoZone();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_Recognize(System.IntPtr hdib, int nMaxCount, int nType);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="BARCODE_Text")] private static extern
        System.IntPtr BARCODE_TextPtr(int n);
        public static string BARCODE_Text(int n)
        { return Marshal.PtrToStringAnsi(BARCODE_TextPtr(n)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool BARCODE_GetText(int n, StringBuilder Text);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int BARCODE_Type(int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool BARCODE_GetRect(int n, out double L, out double T, out double R, out double B);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_IsAvailable();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_Version();

        public const int EZOCR_ENGINE_NONE = 0;
        public const int EZOCR_ENGINE_TRANSYM = 1;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_IsEngineAvailable(int nEngine);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_SelectEngine(int nEngine);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_SelectedEngine();
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_SelectDefaultEngine();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="OCR_EngineName")] private static extern
        System.IntPtr OCR_EngineNamePtr(int nEngine);
        public static string OCR_EngineName(int nEngine)
        { return Marshal.PtrToStringAnsi(OCR_EngineNamePtr(nEngine)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void OCR_SetEngineKey(string sKey);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void OCR_SetEnginePath(string sPath);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void OCR_SetLineBreak(string sEOL);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_RecognizeDib(System.IntPtr hdib);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_RecognizeDibZone(System.IntPtr hdib, int x, int y, int w, int h);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="OCR_Text")] private static extern
        System.IntPtr OCR_TextPtr();
        public static string OCR_Text()
        { return Marshal.PtrToStringAnsi(OCR_TextPtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_GetText(StringBuilder TextBuffer, int nBufLen);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_TextLength();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int OCR_TextOrientation();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_GetCharPositions([MarshalAs(UnmanagedType.LPArray)] int[] charx, [MarshalAs(UnmanagedType.LPArray)] int[] chary);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_GetCharSizes([MarshalAs(UnmanagedType.LPArray)] int[] charw, [MarshalAs(UnmanagedType.LPArray)] int[] charh);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void OCR_GetResolution(out double xdpi, out double ydpi);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void OCR_ClearText();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_WritePage(System.IntPtr hdib);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_WriteTextToPDF();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void OCR_SetAutoRotatePagesToPDF(bool bYes);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool OCR_GetAutoRotatePagesToPDF();



        public const int TWON_ARRAY = 3;
        public const int TWON_ENUMERATION = 4;
        public const int TWON_ONEVALUE = 5;
        public const int TWON_RANGE = 6;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void CONTAINER_Free(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_Copy(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_Equal(int hconA, int hconB);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_Format(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_ItemCount(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_ItemType(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_TypeSize(int nItemType);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        void CONTAINER_GetStringValue(int hcon, int n, StringBuilder sText);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double CONTAINER_FloatValue(int hcon, int n);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_IntValue(int hcon, int n);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="CONTAINER_StringValue")] private static extern
        System.IntPtr CONTAINER_StringValuePtr(int hcon, int n);
        public static string CONTAINER_StringValue(int hcon, int n)
        { return Marshal.PtrToStringAnsi(CONTAINER_StringValuePtr(hcon,n)); }


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_ContainsValue(int hcon, double d);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_ContainsValueInt(int hcon, int n);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_FindValue(int hcon, double d);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_FindValueInt(int hcon, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double CONTAINER_CurrentValue(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double CONTAINER_DefaultValue(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_CurrentValueInt(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_DefaultValueInt(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_DefaultIndex(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_CurrentIndex(int hcon);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double CONTAINER_MinValue(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double CONTAINER_MaxValue(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_MinValueInt(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_MaxValueInt(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        double CONTAINER_StepSize(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_StepSizeInt(int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_OneValue(int nItemType, double dVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_Range(int nItemType, double dMin, double dMax, double dStep);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_Array(int nItemType, int nItems);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_Enumeration(int nItemType, int nItems);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SetItem(int hcon, int n, double dVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SetItemInt(int hcon, int n, int nVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SetItemString(int hcon, int n, string sVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SetItemFrame(int hcon, int n, double l, double t, double r, double b);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_GetItemFrame(int hcon, int n, out double L, out double T, out double R, out double B);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SelectCurrentValue(int hcon, double dVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SelectCurrentItem(int hcon, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SelectDefaultValue(int hcon, double dVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_SelectDefaultItem(int hcon, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_DeleteItem(int hcon, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_InsertItem(int hcon, int n, double dVal);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        int CONTAINER_Wrap(int nFormat, System.IntPtr hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr CONTAINER_Unwrap(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        System.IntPtr CONTAINER_Handle(int hcon);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true)] public static extern
        bool CONTAINER_IsValid(int hcon);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsCapAvailable")] public static extern
        bool IsCapAvailable(int uCap);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Get")] public static extern
        int Get(int uCap);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetDefault")] public static extern
        int GetDefault(int uCap);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCurrent")] public static extern
        int GetCurrent(int uCap);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Set")] public static extern
        bool Set(int uCap, int hcon);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Reset")] public static extern
        bool Reset(int uCap);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_QuerySupport")] public static extern
        int QuerySupport(int uCap);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapability")] public static extern
        bool SetCapability(int cap, double dValue);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapString")] public static extern
        bool SetCapString(int cap, string sValue);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapBool")] public static extern
        bool SetCapBool(int cap, bool bValue);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapBool")] public static extern
        bool GetCapBool(int cap, bool bDefault);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapFix32")] public static extern
        double GetCapFix32(int cap, double dDefault);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapUint16")] public static extern
        int GetCapUint16(int cap, int nDefault);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapUint32")] public static extern
        int GetCapUint32(int cap, int lDefault);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapFix32")] public static extern
        bool SetCapFix32(int Cap, double dVal);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapOneValue")] public static extern
        bool SetCapOneValue(int Cap, int ItemType, int ItemVal);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCapFix32R")] public static extern
        bool SetCapFix32R(int Cap, int Numerator, int Denominator);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCapCurrent")] public static extern
        bool GetCapCurrent(int Cap, int ItemType, System.IntPtr pVal);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ToFix32")] public static extern
        int ToFix32(double d);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ToFix32R")] public static extern
        int ToFix32R(int Numerator, int Denominator);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Fix32ToFloat")] public static extern
        double Fix32ToFloat(int nfix);


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCustomDataToFile")] public static extern
        bool GetCustomDataToFile(string sFilename);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCustomDataFromFile")] public static extern
        bool SetCustomDataFromFile(string sFilename);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetCustomData")] public static extern
        bool SetCustomData([MarshalAs(UnmanagedType.LPArray)] byte[] data, int nbytes);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetCustomData")] public static extern
        int GetCustomData([MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int bufsize);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_CustomData")] private static extern
        System.IntPtr CustomDataPtr();
        public static string CustomData()
        { return Marshal.PtrToStringAnsi(CustomDataPtr()); }


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsExtendedInfoSupported")] public static extern
        bool IsExtendedInfoSupported();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EnableExtendedInfo")] public static extern
        bool EnableExtendedInfo(int eiCode, bool enabled);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_IsExtendedInfoEnabled")] public static extern
        bool IsExtendedInfoEnabled(int eiCode);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DisableExtendedInfo")] public static extern
        void DisableExtendedInfo();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ExtendedInfoItemCount")] public static extern
        int ExtendedInfoItemCount(int eiCode);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ExtendedInfoItemType")] public static extern
        int ExtendedInfoItemType(int eiCode);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ExtendedInfoInt")] public static extern
        int ExtendedInfoInt(int eiCode, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ExtendedInfoFloat")] public static extern
        double ExtendedInfoFloat(int eiCode, int n);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetExtendedInfoString")] public static extern
        bool GetExtendedInfoString(int eiCode, int n, StringBuilder Buffer, int Bufsize);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_ExtendedInfoString")] private static extern
        System.IntPtr ExtendedInfoStringPtr(int eiCode, int n);
        public static string ExtendedInfoString(int eiCode, int n)
        { return Marshal.PtrToStringAnsi(ExtendedInfoStringPtr(eiCode,n)); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetExtendedInfoFrame")] public static extern
        bool GetExtendedInfoFrame(int eiCode, int n, out double L, out double T, out double R, out double B);

        public const int TWEI_MIN = 0x1200;

        public const int TWEI_BARCODEX = 0x1200;
        public const int TWEI_BARCODEY = 0x1201;
        public const int TWEI_BARCODETEXT = 0x1202;
        public const int TWEI_BARCODETYPE = 0x1203;
        public const int TWEI_DESHADETOP = 0x1204;
        public const int TWEI_DESHADELEFT = 0x1205;
        public const int TWEI_DESHADEHEIGHT = 0x1206;
        public const int TWEI_DESHADEWIDTH = 0x1207;
        public const int TWEI_DESHADESIZE = 0x1208;
        public const int TWEI_SPECKLESREMOVED = 0x1209;
        public const int TWEI_HORZLINEXCOORD = 0x120A;
        public const int TWEI_HORZLINEYCOORD = 0x120B;
        public const int TWEI_HORZLINELENGTH = 0x120C;
        public const int TWEI_HORZLINETHICKNESS = 0x120D;
        public const int TWEI_VERTLINEXCOORD = 0x120E;
        public const int TWEI_VERTLINEYCOORD = 0x120F;
        public const int TWEI_VERTLINELENGTH = 0x1210;
        public const int TWEI_VERTLINETHICKNESS = 0x1211;
        public const int TWEI_PATCHCODE = 0x1212;
        public const int TWEI_ENDORSEDTEXT = 0x1213;
        public const int TWEI_FORMCONFIDENCE = 0x1214;
        public const int TWEI_FORMTEMPLATEMATCH = 0x1215;
        public const int TWEI_FORMTEMPLATEPAGEMATCH = 0x1216;
        public const int TWEI_FORMHORZDOCOFFSET = 0x1217;
        public const int TWEI_FORMVERTDOCOFFSET = 0x1218;
        public const int TWEI_BARCODECOUNT = 0x1219;
        public const int TWEI_BARCODECONFIDENCE = 0x121A;
        public const int TWEI_BARCODEROTATION = 0x121B;
        public const int TWEI_BARCODETEXTLENGTH = 0x121C;
        public const int TWEI_DESHADECOUNT = 0x121D;
        public const int TWEI_DESHADEBLACKCOUNTOLD = 0x121E;
        public const int TWEI_DESHADEBLACKCOUNTNEW = 0x121F;
        public const int TWEI_DESHADEBLACKRLMIN = 0x1220;
        public const int TWEI_DESHADEBLACKRLMAX = 0x1221;
        public const int TWEI_DESHADEWHITECOUNTOLD = 0x1222;
        public const int TWEI_DESHADEWHITECOUNTNEW = 0x1223;
        public const int TWEI_DESHADEWHITERLMIN = 0x1224;
        public const int TWEI_DESHADEWHITERLAVE = 0x1225;
        public const int TWEI_DESHADEWHITERLMAX = 0x1226;
        public const int TWEI_BLACKSPECKLESREMOVED = 0x1227;
        public const int TWEI_WHITESPECKLESREMOVED = 0x1228;
        public const int TWEI_HORZLINECOUNT = 0x1229;
        public const int TWEI_VERTLINECOUNT = 0x122A;
        public const int TWEI_DESKEWSTATUS = 0x122B;
        public const int TWEI_SKEWORIGINALANGLE = 0x122C;
        public const int TWEI_SKEWFINALANGLE = 0x122D;
        public const int TWEI_SKEWCONFIDENCE = 0x122E;
        public const int TWEI_SKEWWINDOWX1 = 0x122F;
        public const int TWEI_SKEWWINDOWY1 = 0x1230;
        public const int TWEI_SKEWWINDOWX2 = 0x1231;
        public const int TWEI_SKEWWINDOWY2 = 0x1232;
        public const int TWEI_SKEWWINDOWX3 = 0x1233;
        public const int TWEI_SKEWWINDOWY3 = 0x1234;
        public const int TWEI_SKEWWINDOWX4 = 0x1235;
        public const int TWEI_SKEWWINDOWY4 = 0x1236;
        public const int TWEI_BOOKNAME = 0x1238;
        public const int TWEI_CHAPTERNUMBER = 0x1239;
        public const int TWEI_DOCUMENTNUMBER = 0x123A;
        public const int TWEI_PAGENUMBER = 0x123B;
        public const int TWEI_CAMERA = 0x123C;
        public const int TWEI_FRAMENUMBER = 0x123D;
        public const int TWEI_FRAME = 0x123E;
        public const int TWEI_PIXELFLAVOR = 0x123F;
        public const int TWEI_ICCPROFILE = 0x1240;
        public const int TWEI_LASTSEGMENT = 0x1241;
        public const int TWEI_SEGMENTNUMBER = 0x1242;
        public const int TWEI_MAGDATA = 0x1243;
        public const int TWEI_MAGTYPE = 0x1244;
        public const int TWEI_PAGESIDE = 0x1245;
        public const int TWEI_FILESYSTEMSOURCE = 0x1246;

        public const int TWEI_MAX = 0x1246;




        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_DS")] public static extern
        bool DS(int DG, int DAT, int MSG, System.IntPtr pData);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Mgr")] public static extern
        bool Mgr(int DG, int DAT, int MSG, System.IntPtr pData);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LogFile")] public static extern
        void LogFile(int fLog);
        public const int EZT_LOG_ON = 1;
        public const int EZT_LOG_FLUSH = 2;
        public const int EZT_LOG_DETAIL = 4;


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetLogFolder")] public static extern
        bool SetLogFolder(string sFolder);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetLogName")] public static extern
        bool SetLogName(string sName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_LogFileName")] private static extern
        System.IntPtr LogFileNamePtr();
        public static string LogFileName()
        { return Marshal.PtrToStringAnsi(LogFileNamePtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_WriteToLog")] public static extern
        void WriteToLog(string sText);



        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BeginAcquireMemory")] public static extern
        bool BeginAcquireMemory(System.IntPtr hwnd, int nRows);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireMemoryBlock")] public static extern
        System.IntPtr AcquireMemoryBlock();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EndAcquireMemory")] public static extern
        bool EndAcquireMemory();


        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AcquireFile")] public static extern
        bool AcquireFile(System.IntPtr hwndApp, int nFF, string sFileName);
        public const int TWAIN_FF_TIFF = 0;
        public const int TWAIN_FF_PICT = 1;
        public const int TWAIN_FF_BMP = 2;
        public const int TWAIN_FF_XBM = 3;
        public const int TWAIN_FF_JFIF = 4;
        public const int TWAIN_FF_FPX = 5;
        public const int TWAIN_FF_TIFFMULTI = 6;
        public const int TWAIN_FF_PNG = 7;
        public const int TWAIN_FF_SPIFF = 8;
        public const int TWAIN_FF_EXIF = 9;
        public const int TWAIN_FF_PDF = 10;
        public const int TWAIN_FF_JP2 = 11;
        public const int TWAIN_FF_JPN = 12;
        public const int TWAIN_FF_JPX = 13;
        public const int TWAIN_FF_DEJAVU = 14;
        public const int TWAIN_FF_PDFA = 15;

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetImageReadyTimeout")] public static extern
        int SetImageReadyTimeout(int nSec);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetImageReadyTimeout")] public static extern
        int GetImageReadyTimeout();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_AutoClickButton")] public static extern
        void AutoClickButton(string sButtonName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BreakModalLoop")] public static extern
        void BreakModalLoop();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_EmptyMessageQueue")] public static extern
        void EmptyMessageQueue();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_BuildName")] private static extern
        System.IntPtr BuildNamePtr();
        public static string BuildName()
        { return Marshal.PtrToStringAnsi(BuildNamePtr()); }

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetBuildName")] public static extern
        void GetBuildName(StringBuilder sName);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetSourceIdentity")] public static extern
        int GetSourceIdentity(System.IntPtr ptwid);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetImageInfo")] public static extern
        int GetImageInfo(System.IntPtr ptwinfo);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SelfTest")] public static extern
        int SelfTest(int f);

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_SetQAMode")] public static extern
        void SetQAMode(int nMode);
        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_GetQAMode")] public static extern
        int GetQAMode();

        [DllImport("Eztwain4.dll", CharSet=CharSet.Ansi, ExactSpelling=true, EntryPoint="TWAIN_Blocked")] public static extern
        bool Blocked();





        public const int CAP_CUSTOMBASE = 0x8000;

        public const int CAP_XFERCOUNT = 0x1;

        public const int ICAP_COMPRESSION = 0x100;
        public const int ICAP_PIXELTYPE = 0x101;
        public const int ICAP_UNITS = 0x102;
        public const int ICAP_XFERMECH = 0x103;

        public const int CAP_AUTHOR = 0x1000;
        public const int CAP_CAPTION = 0x1001;
        public const int CAP_FEEDERENABLED = 0x1002;
        public const int CAP_FEEDERLOADED = 0x1003;
        public const int CAP_TIMEDATE = 0x1004;
        public const int CAP_SUPPORTEDCAPS = 0x1005;
        public const int CAP_EXTENDEDCAPS = 0x1006;
        public const int CAP_AUTOFEED = 0x1007;
        public const int CAP_CLEARPAGE = 0x1008;
        public const int CAP_FEEDPAGE = 0x1009;
        public const int CAP_REWINDPAGE = 0x100A;
        public const int CAP_INDICATORS = 0x100B;
        public const int CAP_SUPPORTEDCAPSEXT = 0x100C;
        public const int CAP_PAPERDETECTABLE = 0x100D;
        public const int CAP_UICONTROLLABLE = 0x100E;
        public const int CAP_DEVICEONLINE = 0x100F;
        public const int CAP_AUTOSCAN = 0x1010;
        public const int CAP_THUMBNAILSENABLED = 0x1011;
        public const int CAP_DUPLEX = 0x1012;
        public const int CAP_DUPLEXENABLED = 0x1013;
        public const int CAP_ENABLEDSUIONLY = 0x1014;
        public const int CAP_CUSTOMDSDATA = 0x1015;
        public const int CAP_ENDORSER = 0x1016;
        public const int CAP_JOBCONTROL = 0x1017;
        public const int CAP_ALARMS = 0x1018;
        public const int CAP_ALARMVOLUME = 0x1019;
        public const int CAP_AUTOMATICCAPTURE = 0x101A;
        public const int CAP_TIMEBEFOREFIRSTCAPTURE = 0x101B;
        public const int CAP_TIMEBETWEENCAPTURES = 0x101C;
        public const int CAP_CLEARBUFFERS = 0x101D;
        public const int CAP_MAXBATCHBUFFERS = 0x101E;
        public const int CAP_DEVICETIMEDATE = 0x101F;
        public const int CAP_POWERSUPPLY = 0x1020;
        public const int CAP_CAMERAPREVIEWUI = 0x1021;
        public const int CAP_DEVICEEVENT = 0x1022;
        public const int CAP_SERIALNUMBER = 0x1024;
        public const int CAP_PRINTER = 0x1026;
        public const int CAP_PRINTERENABLED = 0x1027;
        public const int CAP_PRINTERINDEX = 0x1028;
        public const int CAP_PRINTERMODE = 0x1029;
        public const int CAP_PRINTERSTRING = 0x102A;
        public const int CAP_PRINTERSUFFIX = 0x102B;
        public const int CAP_LANGUAGE = 0x102C;
        public const int CAP_FEEDERALIGNMENT = 0x102D;
        public const int CAP_FEEDERORDER = 0x102E;
        public const int CAP_REACQUIREALLOWED = 0x1030;
        public const int CAP_BATTERYMINUTES = 0x1032;
        public const int CAP_BATTERYPERCENTAGE = 0x1033;

        public const int ICAP_AUTOBRIGHT = 0x1100;
        public const int ICAP_BRIGHTNESS = 0x1101;
        public const int ICAP_CONTRAST = 0x1103;
        public const int ICAP_CUSTHALFTONE = 0x1104;
        public const int ICAP_EXPOSURETIME = 0x1105;
        public const int ICAP_FILTER = 0x1106;
        public const int ICAP_FLASHUSED = 0x1107;
        public const int ICAP_GAMMA = 0x1108;
        public const int ICAP_HALFTONES = 0x1109;
        public const int ICAP_HIGHLIGHT = 0x110A;
        public const int ICAP_IMAGEFILEFORMAT = 0x110C;
        public const int ICAP_LAMPSTATE = 0x110D;
        public const int ICAP_LIGHTSOURCE = 0x110E;
        public const int ICAP_ORIENTATION = 0x1110;
        public const int ICAP_PHYSICALWIDTH = 0x1111;
        public const int ICAP_PHYSICALHEIGHT = 0x1112;
        public const int ICAP_SHADOW = 0x1113;
        public const int ICAP_FRAMES = 0x1114;
        public const int ICAP_XNATIVERESOLUTION = 0x1116;
        public const int ICAP_YNATIVERESOLUTION = 0x1117;
        public const int ICAP_XRESOLUTION = 0x1118;
        public const int ICAP_YRESOLUTION = 0x1119;
        public const int ICAP_MAXFRAMES = 0x111A;
        public const int ICAP_TILES = 0x111B;
        public const int ICAP_BITORDER = 0x111C;
        public const int ICAP_CCITTKFACTOR = 0x111D;
        public const int ICAP_LIGHTPATH = 0x111E;
        public const int ICAP_PIXELFLAVOR = 0x111F;
        public const int ICAP_PLANARCHUNKY = 0x1120;
        public const int ICAP_ROTATION = 0x1121;
        public const int ICAP_SUPPORTEDSIZES = 0x1122;
        public const int ICAP_THRESHOLD = 0x1123;
        public const int ICAP_XSCALING = 0x1124;
        public const int ICAP_YSCALING = 0x1125;
        public const int ICAP_BITORDERCODES = 0x1126;
        public const int ICAP_PIXELFLAVORCODES = 0x1127;
        public const int ICAP_JPEGPIXELTYPE = 0x1128;
        public const int ICAP_TIMEFILL = 0x112A;
        public const int ICAP_BITDEPTH = 0x112B;
        public const int ICAP_BITDEPTHREDUCTION = 0x112C;
        public const int ICAP_UNDEFINEDIMAGESIZE = 0x112D;
        public const int ICAP_IMAGEDATASET = 0x112E;
        public const int ICAP_EXTIMAGEINFO = 0x112F;
        public const int ICAP_MINIMUMHEIGHT = 0x1130;
        public const int ICAP_MINIMUMWIDTH = 0x1131;
        public const int ICAP_FLIPROTATION = 0x1136;
        public const int ICAP_BARCODEDETECTIONENABLED = 0x1137;
        public const int ICAP_SUPPORTEDBARCODETYPES = 0x1138;
        public const int ICAP_BARCODEMAXSEARCHPRIORITIES = 0x1139;
        public const int ICAP_BARCODESEARCHPRIORITIES = 0x113A;
        public const int ICAP_BARCODESEARCHMODE = 0x113B;
        public const int ICAP_BARCODEMAXRETRIES = 0x113C;
        public const int ICAP_BARCODETIMEOUT = 0x113D;
        public const int ICAP_ZOOMFACTOR = 0x113E;
        public const int ICAP_PATCHCODEDETECTIONENABLED = 0x113F;
        public const int ICAP_SUPPORTEDPATCHCODETYPES = 0x1140;
        public const int ICAP_PATCHCODEMAXSEARCHPRIORITIES = 0x1141;
        public const int ICAP_PATCHCODESEARCHPRIORITIES = 0x1142;
        public const int ICAP_PATCHCODESEARCHMODE = 0x1143;
        public const int ICAP_PATCHCODEMAXRETRIES = 0x1144;
        public const int ICAP_PATCHCODETIMEOUT = 0x1145;
        public const int ICAP_FLASHUSED2 = 0x1146;
        public const int ICAP_IMAGEFILTER = 0x1147;
        public const int ICAP_NOISEFILTER = 0x1148;
        public const int ICAP_OVERSCAN = 0x1149;
        public const int ICAP_AUTOMATICBORDERDETECTION = 0x1150;
        public const int ICAP_AUTOMATICDESKEW = 0x1151;
        public const int ICAP_AUTOMATICROTATE = 0x1152;
        public const int ICAP_JPEGQUALITY = 0x1153;

        public const int TWTY_INT8 = 0x0;
        public const int TWTY_INT16 = 0x1;
        public const int TWTY_INT32 = 0x2;
        public const int TWTY_UINT8 = 0x3;
        public const int TWTY_UINT16 = 0x4;
        public const int TWTY_UINT32 = 0x5;
        public const int TWTY_BOOL = 0x6;
        public const int TWTY_FIX32 = 0x7;
        public const int TWTY_FRAME = 0x8;
        public const int TWTY_STR32 = 0x9;
        public const int TWTY_STR64 = 0xA;
        public const int TWTY_STR128 = 0xB;
        public const int TWTY_STR255 = 0xC;
        public const int TWTY_STR1024 = 0xD;
        public const int TWTY_UNI512 = 0xE;

        public const int TWOR_ROT0 = 0;
        public const int TWOR_ROT90 = 1;
        public const int TWOR_ROT180 = 2;
        public const int TWOR_ROT270 = 3;




    }
} // namespace
