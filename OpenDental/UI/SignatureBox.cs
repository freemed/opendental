using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.UI {
	///<summary>This class is specifically designed to duplicate the functionality of the Topaz SigPlusNET control.  So even if I would have done it differenly, I didn't have a choice.</summary>
	public partial class SignatureBox:Control {
		///<summary>0=not accepting input. 1=accepting input.</summary>
		private int tabletState;
		///<summary>Collection of points that will be connected to draw the signature.  0,0 represents pen up.</summary>
		private List<Point> pointList;
		//<summary>0=none, 1=lossless.  Always use 1.</summary>
		//private int compressionMode;
		//<summary>0=clear text. 1=40 bit DES.  2=higher security.</summary>
		//private int encryptionMode;
		///<summary>The hash of the document which will be used to encrypt the signature.</summary>
		private byte[] hash;
		private bool mouseIsDown;



		public SignatureBox() {
			InitializeComponent();
			pointList=new List<Point>();
		}

		///<summary>Set to 1 to activate it to start accepting signatures.  Set to 0 to no longer accept input.</summary>
		public void SetTabletState(int state){
			tabletState=state;
		}

		///<summary>1 if the control is accepting signature input. 0 if not.</summary>
		public int GetTabletState() {
			return tabletState;
		}

		///<summary>Clears the display and the stored signature.</summary>
		public void ClearTablet(){
			pointList=new List<Point>();
			Invalidate();
		}

		public int NumberOfTabletPoints(){
			return pointList.Count;
		}

		/*
		///<summary>0=none, 1=lossless.  2-8 not used?</summary>
		public void SetSigCompressionMode(int compressMode){
			compressionMode=compressMode;
		}

		///<summary>0=clear text. 1=low DES (do not use).  2=high Rijndael.</summary>
		public void SetEncryptionMode(int encryptMode){
			encryptionMode=encryptMode;
		}*/

		///<summary>Set it to "0000000000000000" (16 zeros) to indicate no key string to be used for encryption.</summary>
		public void SetKeyString(string keyStr){
			UTF8Encoding enc=new UTF8Encoding();
			hash=enc.GetBytes(keyStr);
		}

		///<summary>The data that's begin signed.  A 16 byte hash will be created off this data, and used to encrypt the signature.</summary>
		public void SetAutoKeyData(string keyData){
			byte[] data=Encoding.UTF8.GetBytes(keyData);
			HashAlgorithm algorithm=MD5.Create();
			hash=algorithm.ComputeHash(data);//always results in length of 16.
		}

		///<summary>Encrypts signature text and returns a base 64 string so that it can go directly into the database.</summary>
		public string GetSigString(){
			if(pointList.Count==0){
				return "";
			}
			string rawString="";
			for(int i=0;i<pointList.Count;i++){
				if(i>0){
					rawString+=";";
				}
				rawString+=pointList[i].X.ToString()+","+pointList[i].Y.ToString();
			}
			byte[] sigBytes=Encoding.UTF8.GetBytes(rawString);
			MemoryStream ms=new MemoryStream();
			//Compression could have been done here, using DeflateStream
			//A decision was made not to use compression because it would have taken more time and not saved much space.
			//DeflateStream compressedzipStream = new DeflateStream(ms , CompressionMode.Compress, true);
			//Now, we have the compressed bytes.  Need to encrypt them.
			Rijndael crypt=Rijndael.Create();
			crypt.KeySize=128;//16 bytes;
			crypt.Key=hash;
			crypt.IV=new byte[16];
			CryptoStream cs=new CryptoStream(ms,crypt.CreateEncryptor(),CryptoStreamMode.Write);
			cs.Write(sigBytes,0,sigBytes.Length);
			cs.FlushFinalBlock();
			byte[] encryptedBytes=new byte[ms.Length];
			ms.Position=0;
			ms.Read(encryptedBytes,0,(int)ms.Length);
			cs.Dispose();
			ms.Dispose();			
			return Convert.ToBase64String(encryptedBytes);
		}

		///<summary>Unencrypts the signature coming in from the database.  The key data etc needs to be set first before calling this function.</summary>
		public void SetSigString(string sigString){
			pointList=new List<Point>();
			if(sigString==""){
				return;
			}
			//convert base64 string to bytes
			byte[] encryptedBytes=Convert.FromBase64String(sigString);
			//create the streams
			MemoryStream ms=new MemoryStream();
			//ms.Write(encryptedBytes,0,(int)encryptedBytes.Length);
			//create a crypto stream
			Rijndael crypt=Rijndael.Create();
			crypt.KeySize=128;//16 bytes;
			crypt.Key=hash;
			crypt.IV=new byte[16];
			CryptoStream cs=new CryptoStream(ms,crypt.CreateDecryptor(),CryptoStreamMode.Write);
			cs.Write(encryptedBytes,0,encryptedBytes.Length);
			cs.FlushFinalBlock();
			byte[] sigBytes=new byte[ms.Length];
			ms.Position=0;
			ms.Read(sigBytes,0,(int)ms.Length);
			cs.Dispose();
			ms.Dispose();
			//now convert the bytes into a string.
			string rawString=Encoding.UTF8.GetString(sigBytes);
			//convert the raw string into a series of points
			string[] pointArray=rawString.Split(new char[] {';'});
			Point pt;
			string[] coords;
			for(int i=0;i<pointArray.Length;i++){
				coords=pointArray[i].Split(new char[] {','});
				pt=new Point(Convert.ToInt32(coords[0]),Convert.ToInt32(coords[1]));
				pointList.Add(pt);
			}
			Invalidate();
		}

		///<summary></summary>
		protected override void OnPaintBackground(PaintEventArgs pea) {
			//base.OnPaintBackground (pea);
			//don't paint background.  This reduces flickering when using double buffering.
		}

		protected override void OnPaint(PaintEventArgs e) {
			Bitmap doubleBuffer=new Bitmap(Width,Height,e.Graphics);
			Graphics g=Graphics.FromImage(doubleBuffer);
			g.FillRectangle(Brushes.White,0,0,this.Width,this.Height);
			Pen pen=new Pen(Color.Black,2f);
			g.SmoothingMode=SmoothingMode.HighQuality;
			for(int i=1;i<pointList.Count;i++){//skip the first point
				if(pointList[i-1].X==0 && pointList[i-1].Y==0){
					continue;
				}
				if(pointList[i].X==0 && pointList[i].Y==0){
					continue;
				}
				g.DrawLine(pen,pointList[i-1],pointList[i]);
			}
			e.Graphics.DrawImageUnscaled(doubleBuffer,0,0);
			g.Dispose();
			base.OnPaint(e);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			if(tabletState==0){
				return;
			}
			mouseIsDown=true;
			pointList.Add(new Point(e.X,e.Y));
			//Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if(tabletState==0) {
				return;
			}
			if(!mouseIsDown){
				return;
			}
			pointList.Add(new Point(e.X,e.Y));
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			if(tabletState==0) {
				return;
			}
			mouseIsDown=false;
			pointList.Add(new Point(0,0));
		}



	}
}
