using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace WebCamOD {
	public partial class FormWebCamOD:Form {
		private IntPtr intPtrVideo;
		private VideoCapture vidCapt;
		private string IpAddress192;
		///<summary>This is set to minVal when starting up.  Whenever saving a screenshot, if the purge date is not today, then it runs a purge to keep the number of files from getting too big.  So this will usually happen within 5 minutes of someone clocking in for the day.</summary>
		private DateTime datePurged;

		public FormWebCamOD() {
			InitializeComponent();
		}

		private void FormWebCamOD_Load(object sender,EventArgs e) {
			datePurged=DateTime.MinValue;
			Process[] processes=Process.GetProcessesByName("WebCamOD");
			for(int p=0;p<processes.Length;p++) {
				if(Process.GetCurrentProcess().Id==processes[p].Id) {
					continue;
				}
				//another process was found
				MessageBox.Show("WebCamOD is already running.");
				Application.Exit();
				return;
			}
			//since this tool is only used at HQ, we hard code everything
			bool is192network=false;
			IPHostEntry iphostentry=Dns.GetHostEntry(Environment.MachineName);
			foreach(IPAddress ipaddress in iphostentry.AddressList) {
				if(ipaddress.ToString().StartsWith("192.")) {
					is192network=true;
				}
			}
			DataConnection dbcon=new DataConnection();
			try {
				if(is192network) {
					dbcon.SetDb("192.168.0.200","customers","root","","","",DatabaseType.MySql);
				}
				else {
					dbcon.SetDb("10.10.10.200","customers","root","","","",DatabaseType.MySql);
				}
			}
			catch {
				MessageBox.Show("This tool is not designed for general use.");
				return;
			}
			//get ipaddress on startup
			IpAddress192="";
			foreach(IPAddress ipaddress in iphostentry.AddressList) {
				if(ipaddress.ToString().Contains("192.168.0")) {
					IpAddress192=ipaddress.ToString();
				}
			}
			//if(IpAddress192=="") {
			//	MessageBox.Show("Could not locate ipaddress");
			//	Application.Exit();
			//}
			intPtrVideo=IntPtr.Zero;
			timerPhoneWebCam.Enabled=true;
			timerScreenShots.Enabled=true;
		}

		private void timerPhoneWebCam_Tick(object sender,EventArgs e) {
			if(vidCapt==null) {
				if(intPtrVideo != IntPtr.Zero) {// Release any previous buffer
					Marshal.FreeCoTaskMem(intPtrVideo);
					intPtrVideo=IntPtr.Zero;
				}
				int deviceCount=VideoCapture.GetDeviceCount();
				if(deviceCount>0) {
					try {
						vidCapt=new VideoCapture(0,640,480,24,pictBoxVideo);
						//image capture will now continue below if successful
					}
					catch {
						Phones.SetWebCamImage(IpAddress192,null,Environment.MachineName);
						return;//haven't actually seen this happen since we started properly disposing of vidCapt
					}
				}
				Phones.SetWebCamImage(IpAddress192,null,Environment.MachineName);
			}
			if(vidCapt!=null) {
				if(intPtrVideo != IntPtr.Zero) {// Release any previous buffer
					Marshal.FreeCoTaskMem(intPtrVideo);
					intPtrVideo=IntPtr.Zero;
				}
				Bitmap bitmapSmall=null;
				try {
					intPtrVideo = vidCapt.Click();//will fail if camera unplugged
					Bitmap bitmap= new Bitmap(vidCapt.Width,vidCapt.Height,vidCapt.Stride,PixelFormat.Format24bppRgb,intPtrVideo);
					bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);// If the image is upsidedown
					int w=50;
					int h=(int)(((float)w)/640f*480f);
					bitmapSmall = new Bitmap(w,h);
					using(Graphics g = Graphics.FromImage(bitmapSmall)) {
						g.DrawImage(bitmap,new Rectangle(0,0,bitmapSmall.Width,bitmapSmall.Height));
					}
					bitmap.Dispose();
					bitmap=null;
				}
				catch {
					//bitmapSmall will remain null
					vidCapt.Dispose();
					vidCapt=null;//To prevent the above slow try/catch from happening again and again.
				}
				finally {
					//Marshal.FreeCoTaskMem(intPtrVideo);
				}
				if(IpAddress192!="") {//found entry in phone table matching this machine ip.
					Phones.SetWebCamImage(IpAddress192,bitmapSmall,Environment.MachineName);
				}
				if(bitmapSmall!=null) {
					bitmapSmall.Dispose();
					bitmapSmall=null;
				}
			}
		}

		private void timerScreenShots_Tick(object sender,EventArgs e) {
			//ticks every 5 minutes
			int extension=Phones.IsOnClock(IpAddress192,Environment.MachineName);
			if(extension==0) {//if this person is on break
				return;//don't save a screenshot
			}
			string folder=@"\\192.168.0.189\storage\My\Jordan\ScreenshotsByWorkstation\"+Environment.MachineName;
			if(!Directory.Exists(folder)) {
				Directory.CreateDirectory(folder);
			}
			if(datePurged.Date!=DateTime.Today) {
				string[] files=Directory.GetFiles(folder);
				for(int f=0;f<files.Length;f++) {
					if(files[f].EndsWith("db")) {
						continue;//skip thumbs.db
					}
					DateTime dtCreated=File.GetCreationTime(files[f]);
					if(dtCreated.AddDays(7).Date < DateTime.Today) {
						File.Delete(files[f]);
					}
				}
				datePurged=DateTime.Today;
			}
			//create the image-----------------------------------------------------------------
			Point origin=new Point(0,0);
			int right=0;
			int bottom=0;
			//all screens together form a giant image.  We just need to know where origin is as well as size.
			for(int s=0;s<System.Windows.Forms.Screen.AllScreens.Length;s++) {
				if(System.Windows.Forms.Screen.AllScreens[s].WorkingArea.X < origin.X 
					|| System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Y < origin.Y) 
				{
					//screen must be to top or left of primary.  Use its origin.
					origin=new Point(System.Windows.Forms.Screen.AllScreens[s].WorkingArea.X,System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Y);
				}
				if(System.Windows.Forms.Screen.AllScreens[s].WorkingArea.X+System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Width > right) {
					//screen must be to right of primary.  Use its right-most extension.
					right=System.Windows.Forms.Screen.AllScreens[s].WorkingArea.X+System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Width;
				}
				if(System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Y+System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Height > bottom) {
					//screen must be to bottom of primary.  Use its bottom-most extension.
					bottom=System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Y+System.Windows.Forms.Screen.AllScreens[s].WorkingArea.Height;
				}
			}
			//calculate total width and height, remembering that origin can be negative
			Size sizeAllScreens=new Size(right-origin.X,bottom-origin.Y);//example 100-(-20)=120, or 100-20=80.
			Bitmap bmp=new Bitmap(sizeAllScreens.Width,sizeAllScreens.Height);
			using(Graphics g=Graphics.FromImage(bmp)) {
				g.CopyFromScreen(origin,new Point(0,0),sizeAllScreens);
			}
			//save the image----------------------------------------------------------------------
			//I tried a variety of file types.  The resulting file sizes were very similar. 
			string filename=folder+"\\"+DateTime.Now.ToString("yyyy-MM-dd-hhmmssff")+".jpg";
			bmp.Save(filename);
			//make a thumbnail with height of 50
			int thumbW=(int)((double)bmp.Width/(double)bmp.Height*50d);
			Bitmap bmpThumb=new Bitmap(thumbW,50);
			Graphics gThumb=Graphics.FromImage(bmpThumb);
			gThumb.DrawImage(bmp,0,0,thumbW,50);
			gThumb.Dispose();
			gThumb=null;
			Phones.SetScreenshot(extension,filename,bmpThumb);//IpAddress192,bitmapSmall,Environment.MachineName);
		}






	}
}
