using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using OpenDentBusiness.Imaging;

namespace OpenDental{
	///<summary>Eventually, the user will be able to edit some image display settings and do a Documents.UpdateCur, but they can't actually make changes to the image.</summary>
	public class FormImageViewer : System.Windows.Forms.Form{
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.PictureBox PictureBox1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageListTools;
		private Timer timer1;
		private Point MouseDownOrigin;
		private bool MouseIsDown=false;
		private Document displayedDoc=null;
		///<summary>The offset of the image due to the grab tool. Used as a basis for calculating imageTranslation.</summary>
		PointF imageLocation=new PointF(0,0);
		///<summary>The true offset of the image in screen-space.</summary>
		PointF imageTranslation=new PointF(0,0);
		Bitmap backBuffer=null;
		Graphics backBuffGraph=null;
		Bitmap renderImage=null;
		private Bitmap ImageCurrent=null;
		///<summary>The current zoom of the image. 1 implies normal size, <1 implies the image is shrunk, >1 imples the image is blown-up.</summary>
		float imageZoom=1.0f;
		///<summary>The current amount. The zoomLevel is 0 after an image is loaded. The image is zoomed a factor of (initial image zoom)*(2^zoomLevel)</summary>
		int zoomLevel=0;
		///<summary>Represents the current factor for level of zoom from the initial zoom of the image. This is calculated directly as 2^zoomLevel every time a zoom occurs. Recalculated from zoomLevel each time, so that zoomFactor always hits the exact same values for the exact same zoom levels (not loss of data).</summary>
		float zoomFactor=1;

		///<summary></summary>
		public FormImageViewer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//even the title of this window is set externally, so no Lan.F is necessary
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageViewer));
			this.imageListTools = new System.Windows.Forms.ImageList(this.components);
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageListTools
			// 
			this.imageListTools.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTools.ImageStream")));
			this.imageListTools.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTools.Images.SetKeyName(0,"");
			this.imageListTools.Images.SetKeyName(1,"");
			// 
			// PictureBox1
			// 
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PictureBox1.Location = new System.Drawing.Point(0,29);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(903,669);
			this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PictureBox1.TabIndex = 12;
			this.PictureBox1.TabStop = false;
			this.PictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.PictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 20;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(903,29);
			this.ToolBarMain.TabIndex = 11;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormImageViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(903,698);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.ToolBarMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormImageViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Image Viewer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.FormImageViewer_Resize);
			this.Load += new System.EventHandler(this.FormImageViewer_Load);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormImageViewer_Load(object sender, System.EventArgs e) {
			LayoutToolBar();
		}

		/// <summary>This form will get the necessary images off disk so that it can control layout.</summary>
		public void SetImage(Document thisDocument,string displayTitle){
			//for now, the document is single. Later, it will get groups for composite images/mounts.
			Text=displayTitle;
			displayedDoc=thisDocument;
			ArrayList docNums=new ArrayList();
			docNums.Add(thisDocument.DocNum);
			string fileName=(string)Documents.GetPaths(docNums)[0];
			if(!File.Exists(fileName)){
				MessageBox.Show(fileName+" could not be found.");
				return;
			}try{
				ImageCurrent=new Bitmap(fileName);
				renderImage=ContrDocs.ApplyDocumentSettingsToImage(thisDocument,ImageCurrent,
					ApplySettings.CROP|ApplySettings.COLORFUNCTION);
				if(renderImage==null) {
					imageZoom=1;
					imageTranslation=new PointF(0,0);
				}else{
					float matchWidth=PictureBox1.Width-16;
					matchWidth=(matchWidth<=0?1:matchWidth);
					float matchHeight=PictureBox1.Height-16;
					matchHeight=(matchHeight<=0?1:matchHeight);
					imageZoom=(float)Math.Min(matchWidth/renderImage.Width,matchHeight/renderImage.Height);
					imageTranslation=new PointF(PictureBox1.Width/2.0f,PictureBox1.Height/2.0f);
				}
				zoomLevel=0;
				zoomFactor=1;
	    }catch(System.Exception exception){
		    MessageBox.Show(Lan.g(this,exception.Message)); 
				ImageCurrent=null;
				renderImage=null;
	    }
		}

		private void FormImageViewer_Resize(object sender,System.EventArgs e) {
			if(backBuffGraph!=null) {
				backBuffGraph.Dispose();
			}
			backBuffGraph=null;
			if(backBuffer!=null) {
				backBuffer.Dispose();
			}
			backBuffer=null;
			int width=PictureBox1.Bounds.Width;
			int height=PictureBox1.Bounds.Height;
			if(width>0 && height>0) {
				backBuffer=new Bitmap(width,height);
				backBuffGraph=Graphics.FromImage(backBuffer);
			}
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			//ODToolBarButton button;
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton("",0,Lan.g(this,"Zoom In"),"ZoomIn"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Zoom Out"),"ZoomOut"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"White"),-1,Lan.g(this,"Clear screen to solid white"),"White"));
			ToolBarMain.Invalidate();
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()){
				case "ZoomIn":
					OnZoomIn_Click();
					break;
				case "ZoomOut":
					OnZoomOut_Click();
					break;
				case "White":
					OnWhite_Click();
					break;
			}
		}

		private void OnZoomIn_Click() {
			zoomLevel++;
			PointF c=new PointF(PictureBox1.ClientRectangle.Width/2.0f,PictureBox1.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-imageTranslation.X,c.Y-imageTranslation.Y);
			imageTranslation=new PointF(imageTranslation.X-p.X,imageTranslation.Y-p.Y);
			zoomFactor=(float)Math.Pow(2,zoomLevel);
		}

		private void OnZoomOut_Click() {
			zoomLevel--;
			PointF c=new PointF(PictureBox1.ClientRectangle.Width/2.0f,PictureBox1.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-imageTranslation.X,c.Y-imageTranslation.Y);
			imageTranslation=new PointF(imageTranslation.X+p.X/2.0f,imageTranslation.Y+p.Y/2.0f);
			zoomFactor=(float)Math.Pow(2,zoomLevel);
		}

		private void OnWhite_Click(){
			ImageCurrent=new Bitmap(1,1);
			renderImage=new Bitmap(1,1);
		}

		private void PictureBox1_MouseDown(object sender,MouseEventArgs e) {
			MouseDownOrigin=new Point(e.X,e.Y);
			MouseIsDown=true;
			imageLocation=new PointF(imageTranslation.X,imageTranslation.Y);
			PictureBox1.Cursor=Cursors.Hand;
		}

		private void PictureBox1_MouseMove(object sender,MouseEventArgs e) {
			if(MouseIsDown) {
				if(ImageCurrent!=null) {
					imageTranslation=new PointF(imageLocation.X+(e.Location.X-MouseDownOrigin.X),
						imageLocation.Y+(e.Location.Y-MouseDownOrigin.Y));
				}
			}
		}

		private void PictureBox1_MouseUp(object sender,MouseEventArgs e) {
			MouseIsDown=false;
			PictureBox1.Cursor=Cursors.Default;
		}

		private void timer1_Tick(object sender,EventArgs e) {
			try{
				backBuffGraph.Clear(Pens.White.Color);
				backBuffGraph.Transform=ContrDocs.GetScreenMatrix(displayedDoc,
					ImageCurrent.Width,ImageCurrent.Height,imageZoom*zoomFactor,imageTranslation);
				backBuffGraph.DrawImage(renderImage,0,0);
				Graphics pbg=PictureBox1.CreateGraphics();
				pbg.DrawImage(backBuffer,0,0);
				pbg.Dispose();
			}catch{
				//Not being able to render the image is non-fatal and probably due to a simple change in state
				//or rounding errors.
			}
		}

	}
}