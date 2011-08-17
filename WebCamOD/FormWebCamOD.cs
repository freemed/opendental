using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace WebCamOD {
	public partial class FormWebCamOD:Form {
		private IntPtr intPtrVideo;
		private VideoCapture vidCapt;
		private string IpAddress192;

		public FormWebCamOD() {
			InitializeComponent();
		}

		private void FormWebCamOD_Load(object sender,EventArgs e) {
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
			try{
				if(is192network) {
					dbcon.SetDb("192.168.0.200","customers","root","","","",DatabaseType.MySql);
				}
				else {
					dbcon.SetDb("10.10.10.200","customers","root","","","",DatabaseType.MySql);
				}
			}
			catch{
				MessageBox.Show("This tool is not designed for general use.");
				return;
			}
			//get ipaddress on startup
			IpAddress192="";
			foreach(IPAddress ipaddress in iphostentry.AddressList) {
				if(ipaddress.ToString().Contains("192.168")) {
					IpAddress192=ipaddress.ToString();
				}
			}
			if(IpAddress192=="") {
				MessageBox.Show("Could not locate ipaddress");
				Application.Exit();
			}
			intPtrVideo=IntPtr.Zero;
			timerPhoneWebCam.Enabled=true;
		}

		private void timerPhoneWebCam_Tick(object sender,EventArgs e) {
			if(vidCapt==null){
				if(intPtrVideo != IntPtr.Zero){// Release any previous buffer
					Marshal.FreeCoTaskMem(intPtrVideo);
					intPtrVideo=IntPtr.Zero;
				}
				int deviceCount=VideoCapture.GetDeviceCount();
				if(deviceCount>0){
					try{
						vidCapt=new VideoCapture(0,640,480,24,pictBoxVideo);
						//image capture will now continue below if successful
					}
					catch{
						Phones.SetWebCamImage(IpAddress192,null);
						return;//haven't actually seen this happen since we started properly disposing of vidCapt
					}
				}
				Phones.SetWebCamImage(IpAddress192,null);
			}
			if(vidCapt!=null){
				if(intPtrVideo != IntPtr.Zero){// Release any previous buffer
					Marshal.FreeCoTaskMem(intPtrVideo);
					intPtrVideo=IntPtr.Zero;
				}
				Bitmap bitmapSmall=null;
				try{
					intPtrVideo = vidCapt.Click();//will fail if camera unplugged
					Bitmap bitmap= new Bitmap(vidCapt.Width, vidCapt.Height, vidCapt.Stride, PixelFormat.Format24bppRgb, intPtrVideo);
					bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);// If the image is upsidedown
					int w=50;
					int h=(int)(((float)w)/640f*480f);
					bitmapSmall = new Bitmap(w,h);
					using(Graphics g = Graphics.FromImage(bitmapSmall)){
						g.DrawImage(bitmap,new Rectangle(0,0,bitmapSmall.Width,bitmapSmall.Height));
					}
					bitmap.Dispose();
					bitmap=null;
				}
				catch{
					//bitmapSmall will remain null
					vidCapt.Dispose();
					vidCapt=null;//To prevent the above slow try/catch from happening again and again.
				}
				finally{
					//Marshal.FreeCoTaskMem(intPtrVideo);
				}
				if(IpAddress192!="") {//found entry in phone table matching this machine ip.
					Phones.SetWebCamImage(IpAddress192,bitmapSmall);
				}
				if(bitmapSmall!=null){
					bitmapSmall.Dispose();
					bitmapSmall=null;
				}
			}
		}

	

	


	}
}
