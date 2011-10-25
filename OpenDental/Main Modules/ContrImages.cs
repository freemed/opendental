 /*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//#define ISXP
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text; 
using System.Threading;
using System.Windows.Forms;
using AxAcroPDFLib;
using OpenDental.UI;
using OpenDentBusiness;
using Tao.OpenGl;
using CodeBase;
using xImageDeviceManager;
using System.Text.RegularExpressions;

namespace OpenDental{

	///<summary></summary>
	public class ContrImages:System.Windows.Forms.UserControl {

		#region Designer Generated Variables

		private System.Windows.Forms.ImageList imageListTree;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageListTools2;
		private System.Windows.Forms.TreeView treeDocuments;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuPrefs;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ContextMenu contextTree;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private Panel panelNote;
		private Label label1;
		private TextBox textNote;
		private SignatureBox sigBox;
		private Label label15;
		private Label labelInvalidSig;
		private ContrWindowingSlider sliderBrightnessContrast;
		private ODToolBar ToolBarPaint;
		private Panel panelUnderline;
		private Panel panelVertLine;
		private System.Windows.Forms.PictureBox pictureBoxMain;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private ContextMenu menuForms;
		private ContextMenuStrip MountMenu;

		#endregion

		#region ManuallyCreatedVariables

		///<summary>Used to display Topaz signatures on Windows. Is added dynamically to avoid native code references crashing MONO.</summary>
		private Control SigBoxTopaz;
		///<summary>Starts out as false. It's only used when repainting the toolbar, not to test mode.</summary>
		private bool IsCropMode;
		private Family FamCur;
		///<summary>When dragging on Picturebox, this is the starting point in PictureBox coordinates.</summary>
		private Point PointMouseDown;
		private bool IsMouseDown;
		///<summary>Set to true when the image in the picture box is currently being translated.</summary>
		private bool IsDragging;
		///<summary>In-memory copies of the images being viewed/edited. No changes are made to these images in memory, they are just kept resident to avoid having to reload the images from disk each time the screen needs to be redrawn.  If no mount, this will just be one image.  A mount will contain a series of images.</summary>
		private Bitmap[] ImagesCur=new Bitmap[1];
		///<summary>Used as a basis for calculating image translations.</summary>
		private PointF PointImageCur;
		///<summary>The true offset of the document image or mount image.</summary>
		private PointF PointTranslation;
		///<summary>The current zoom of the currently loaded image/mount. 1 implies normal size, <1 implies the image is shrunk, >1 imples the image/mount is blown-up.</summary>
		private float ZoomImage=1;
		///<summary>The zoom level is 0 after the current image/mount is loaded.  User changes the zoom in integer increments.  ZoomOverall is then (initial image/mount zoom)*(2^ZoomLevel).</summary>
		private int ZoomLevel=0;
		///<summary>Represents the current factor for level of zoom from the initial zoom of the currently loaded image/mount. This is calculated directly as 2^ZoomLevel every time a zoom occurs. Recalculated from ZoomLevel each time, so that ZoomOverall always hits the exact same values for the exact same zoom levels (no loss of data).</summary>
		private float ZoomOverall=1;
		///<summary>Used to prevent concurrent access to the current images by multiple threads.  Each item in array corresponds to an image in a mount.</summary>
		private int[] WidthsImagesCur=new int[1];
		///<summary>Used to prevent concurrent access to the current images by multiple threads.  Each item in array corresponds to an image in a mount.</summary>
		private int[] HeightsImagesCur=new int[1];
		///<summary>The image currently on the screen.  If a mount, this will be an image representing the entire mount.</summary>
		private Bitmap ImageRenderingNow=null;
		private Rectangle RectCrop=new Rectangle(0,0,-1,-1);
		///<summary>Used for performing an xRay image capture on an imaging device.</summary>
		private SuniDeviceControl xRayImageController=null;
		///<summary>Thread to handle updating the graphical image to the screen when the current document is an image.</summary>
		private Thread ThreadImageUpdate=null;
		private ImageSettingFlags ImageSettingFlagsInvalidated;
		///<summary>Used as a thread-safe communication device between the main and worker threads.</summary>
		private EventWaitHandle EventWaitHandleSettings=new EventWaitHandle(false,EventResetMode.AutoReset);
		///<summary>Edited by the main thread to reflect selection changes. Read by worker thread.</summary>
		private Document DocForSettings=null;
		///<summary>Keeps track of the mount settings for the currently selected mount document.</summary>
		private Mount MountForSettings=new Mount();
		///<summary>Indicates which documents to update in the image worker thread. This variable must be locked before accessing it and it must also be the same length as DocsInMount at all times.</summary>
		private bool[] IdxDocsFlaggedForUpdate=null;
		///<summary>Set by the main thread and read by the image worker thread. Specifies which image processing tasks are to be performed by the worker thread.</summary>
		private ImageSettingFlags ImageSettingFlagsForSettings=ImageSettingFlags.NONE;
		///<summary>Used to perform mouse selections in the treeDocuments list.</summary>
		private ImageNodeId NodeIdentifierDown;
		///<summary>Used to keep track of the old document selection by document number (the only guaranteed unique idenifier). This is to help the code be compatible with both Windows and MONO.</summary>
		private ImageNodeId NodeIdentifierOld;
		///<summary>Used for Invoke() calls in RenderCurrentImage() to safely handle multi-thread access to the picture box.</summary>
		private delegate void RenderImageCallback(Document docCopy,int originalWidth,int originalHeight,float zoom,PointF translation);
		///<summary>Used to safe-guard against multi-threading issues when an image capture is completed.</summary>
		private delegate void CaptureCallback(object sender,EventArgs e);
		///<summary>Used to protect against multi-threading issues when refreshing a mount during an image capture.</summary>
		private delegate void InvalidatesettingsCallback(ImageSettingFlags settings,bool reloadZoomTransCrop);
		///<summary>Keeps track of the document settings for the currently selected document or mount.</summary>
		private Document DocSelected=new Document();
		///<summary>Keeps track of the currently selected mount object (only when a mount is selected).</summary>
		private Mount MountSelected=new Mount();
		///<summary>If a mount is currently selected, this is the list of the mount items on it.</summary>
		private List<MountItem> MountItemsForSelected=null;
		///<summary>The index of the currently selected item within a mount.</summary>
		private int IdxSelectedInMount=0;
		///<summary>List of documents within the currently selected mount (if any).</summary>
		private Document[] DocsInMount=null;
		///<summary>The idxSelectedInMount when it is copied.</summary>
		int IdxDocToCopy=-1;
    //private bool allowTopaz;
		DateTime TimeMouseMoved=new DateTime(1,1,1);
		///<summary></summary>
		private Patient PatCur;
		private bool InitializedOnStartup;
		///<summary>Set with each module refresh, and that's where it's set if it doesn't yet exist.  For now, we are not using ImageStore.GetPatientFolder(), because we haven't tested whether it properly updates the patient object.  We don't want to risk using an outdated patient folder path.  And we don't want to waste time refreshing PatCur after every ImageStore.GetPatientFolder().</summary>
		private string PatFolder;
		private AxAcroPDFLib.AxAcroPDF axAcroPDF1=null;
		private long PatNumPrev=0;
		//private List<Def> DefListExpandedCats=new List<Def>();
		private List<long> ExpandedCats=new List<long>();
		///<summary>If this is not zero, then this indicates a different mode special for claimpayment.</summary>
		private long ClaimPaymentNum;
		///<summary></summary>
		[Category("Action"),Description("Occurs when the close button is clicked in the toolbar.")]
		public event EventHandler CloseClick=null;
		#endregion ManuallyCreatedVariables

		///<summary></summary>
		public ContrImages() {
			Logger.openlog.Log("Initializing Document Module...", Logger.Severity.INFO);
			InitializeComponent();
			//The context menu causes strange bugs in MONO when performing selections on the tree.
			//Perhaps when MONO is more developed we can remove this check.
			//Also, the SigPlusNet() object cannot be instantiated on 64-bit machines, because
			//the code for instantiation exists in a 32-bit native dll. Therefore, we have put
			//the creation code for the topaz box in CodeBase.TopazWrapper.GetTopaz() so that
			//the native code does not exist or get called anywhere in the program unless we are running on a 
			//32-bit version of Windows.
			//bool is64=CodeBase.ODEnvironment.Is64BitOperatingSystem();
			bool platformUnix=Environment.OSVersion.Platform==PlatformID.Unix;
			//allowTopaz=(!platformUnix && !is64);
			if(platformUnix) {
				treeDocuments.ContextMenu=null;
			}
			//if(allowTopaz){//Windows OS
				try {
					SigBoxTopaz=CodeBase.TopazWrapper.GetTopaz();
					panelNote.Controls.Add(SigBoxTopaz);
					SigBoxTopaz.Location=sigBox.Location;//new System.Drawing.Point(437,15);
					SigBoxTopaz.Name="sigBoxTopaz";
					SigBoxTopaz.Size=new System.Drawing.Size(362,79);
					SigBoxTopaz.TabIndex=93;
					SigBoxTopaz.Text="sigPlusNET1";
					SigBoxTopaz.DoubleClick+=new System.EventHandler(this.sigBoxTopaz_DoubleClick);
					CodeBase.TopazWrapper.SetTopazState(SigBoxTopaz,0);
				}
				catch { }
			//}
			//We always capture with a Suni device for now.
			//TODO: In the future use a device locator in the xImagingDeviceManager
			//project to return the appropriate general device control.
			xRayImageController=new SuniDeviceControl();
			this.xRayImageController.OnCaptureReady+=new System.EventHandler(this.OnCaptureReady);
			this.xRayImageController.OnCaptureComplete+=new System.EventHandler(this.OnCaptureComplete);
			this.xRayImageController.OnCaptureFinalize+=new System.EventHandler(this.OnCaptureFinalize);
			Logger.openlog.Log("Document Module initialization complete.", Logger.Severity.INFO);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
				xRayImageController.KillXRayThread();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrImages));
			this.treeDocuments = new System.Windows.Forms.TreeView();
			this.contextTree = new System.Windows.Forms.ContextMenu();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.imageListTools2 = new System.Windows.Forms.ImageList(this.components);
			this.pictureBoxMain = new System.Windows.Forms.PictureBox();
			this.MountMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuPrefs = new System.Windows.Forms.MenuItem();
			this.panelNote = new System.Windows.Forms.Panel();
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.panelUnderline = new System.Windows.Forms.Panel();
			this.panelVertLine = new System.Windows.Forms.Panel();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.ToolBarPaint = new OpenDental.UI.ODToolBar();
			this.sliderBrightnessContrast = new OpenDental.UI.ContrWindowingSlider();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
			this.panelNote.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeDocuments
			// 
			this.treeDocuments.ContextMenu = this.contextTree;
			this.treeDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.treeDocuments.FullRowSelect = true;
			this.treeDocuments.HideSelection = false;
			this.treeDocuments.ImageIndex = 2;
			this.treeDocuments.ImageList = this.imageListTree;
			this.treeDocuments.Indent = 20;
			this.treeDocuments.Location = new System.Drawing.Point(0,29);
			this.treeDocuments.Name = "treeDocuments";
			this.treeDocuments.SelectedImageIndex = 2;
			this.treeDocuments.Size = new System.Drawing.Size(228,519);
			this.treeDocuments.TabIndex = 0;
			this.treeDocuments.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeDocuments_AfterCollapse);
			this.treeDocuments.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeDocuments_AfterExpand);
			this.treeDocuments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseDoubleClick);
			this.treeDocuments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseDown);
			this.treeDocuments.MouseLeave += new System.EventHandler(this.TreeDocuments_MouseLeave);
			this.treeDocuments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseMove);
			this.treeDocuments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseUp);
			// 
			// contextTree
			// 
			this.contextTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Print";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Delete";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Info";
			// 
			// imageListTree
			// 
			this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
			this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTree.Images.SetKeyName(0,"");
			this.imageListTree.Images.SetKeyName(1,"");
			this.imageListTree.Images.SetKeyName(2,"");
			this.imageListTree.Images.SetKeyName(3,"");
			this.imageListTree.Images.SetKeyName(4,"");
			this.imageListTree.Images.SetKeyName(5,"");
			this.imageListTree.Images.SetKeyName(6,"");
			// 
			// imageListTools2
			// 
			this.imageListTools2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTools2.ImageStream")));
			this.imageListTools2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTools2.Images.SetKeyName(0,"Pat.gif");
			this.imageListTools2.Images.SetKeyName(1,"print.gif");
			this.imageListTools2.Images.SetKeyName(2,"deleteX.gif");
			this.imageListTools2.Images.SetKeyName(3,"info.gif");
			this.imageListTools2.Images.SetKeyName(4,"scan.gif");
			this.imageListTools2.Images.SetKeyName(5,"import.gif");
			this.imageListTools2.Images.SetKeyName(6,"paste.gif");
			this.imageListTools2.Images.SetKeyName(7,"");
			this.imageListTools2.Images.SetKeyName(8,"ZoomIn.gif");
			this.imageListTools2.Images.SetKeyName(9,"ZoomOut.gif");
			this.imageListTools2.Images.SetKeyName(10,"Hand.gif");
			this.imageListTools2.Images.SetKeyName(11,"flip.gif");
			this.imageListTools2.Images.SetKeyName(12,"rotateL.gif");
			this.imageListTools2.Images.SetKeyName(13,"rotateR.gif");
			this.imageListTools2.Images.SetKeyName(14,"scanDoc.gif");
			this.imageListTools2.Images.SetKeyName(15,"scanPhoto.gif");
			this.imageListTools2.Images.SetKeyName(16,"scanXray.gif");
			this.imageListTools2.Images.SetKeyName(17,"copy.gif");
			this.imageListTools2.Images.SetKeyName(18,"ScanMulti.gif");
			this.imageListTools2.Images.SetKeyName(19,"Export.gif");
			// 
			// pictureBoxMain
			// 
			this.pictureBoxMain.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBoxMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBoxMain.ContextMenuStrip = this.MountMenu;
			this.pictureBoxMain.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxMain.InitialImage = null;
			this.pictureBoxMain.Location = new System.Drawing.Point(233,54);
			this.pictureBoxMain.Name = "pictureBoxMain";
			this.pictureBoxMain.Size = new System.Drawing.Size(703,370);
			this.pictureBoxMain.TabIndex = 6;
			this.pictureBoxMain.TabStop = false;
			this.pictureBoxMain.SizeChanged += new System.EventHandler(this.PictureBox1_SizeChanged);
			this.pictureBoxMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			this.pictureBoxMain.MouseHover += new System.EventHandler(this.PictureBox1_MouseHover);
			this.pictureBoxMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.pictureBoxMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
			// 
			// MountMenu
			// 
			this.MountMenu.Name = "MountMenu";
			this.MountMenu.Size = new System.Drawing.Size(61,4);
			this.MountMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MountMenu_Opening);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuPrefs});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuExit});
			this.menuItem1.Text = "File";
			// 
			// menuExit
			// 
			this.menuExit.Index = 0;
			this.menuExit.Text = "Exit";
			// 
			// menuPrefs
			// 
			this.menuPrefs.Index = 1;
			this.menuPrefs.Text = "Preferences";
			// 
			// panelNote
			// 
			this.panelNote.Controls.Add(this.labelInvalidSig);
			this.panelNote.Controls.Add(this.sigBox);
			this.panelNote.Controls.Add(this.label15);
			this.panelNote.Controls.Add(this.label1);
			this.panelNote.Controls.Add(this.textNote);
			this.panelNote.Location = new System.Drawing.Point(234,485);
			this.panelNote.Name = "panelNote";
			this.panelNote.Size = new System.Drawing.Size(705,64);
			this.panelNote.TabIndex = 11;
			this.panelNote.Visible = false;
			this.panelNote.DoubleClick += new System.EventHandler(this.panelNote_DoubleClick);
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(398,31);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196,59);
			this.labelInvalidSig.TabIndex = 94;
			this.labelInvalidSig.Text = "Invalid Signature -  Document or note has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInvalidSig.DoubleClick += new System.EventHandler(this.labelInvalidSig_DoubleClick);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(308,20);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(362,79);
			this.sigBox.TabIndex = 90;
			this.sigBox.DoubleClick += new System.EventHandler(this.sigBox_DoubleClick);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(305,0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(63,18);
			this.label15.TabIndex = 87;
			this.label15.Text = "Signature";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label15.DoubleClick += new System.EventHandler(this.label15_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38,18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
			// 
			// textNote
			// 
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.Location = new System.Drawing.Point(0,20);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(302,79);
			this.textNote.TabIndex = 0;
			this.textNote.DoubleClick += new System.EventHandler(this.textNote_DoubleClick);
			this.textNote.MouseHover += new System.EventHandler(this.textNote_MouseHover);
			// 
			// panelUnderline
			// 
			this.panelUnderline.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelUnderline.Location = new System.Drawing.Point(236,48);
			this.panelUnderline.Name = "panelUnderline";
			this.panelUnderline.Size = new System.Drawing.Size(702,2);
			this.panelUnderline.TabIndex = 15;
			// 
			// panelVertLine
			// 
			this.panelVertLine.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelVertLine.Location = new System.Drawing.Point(233,25);
			this.panelVertLine.Name = "panelVertLine";
			this.panelVertLine.Size = new System.Drawing.Size(2,25);
			this.panelVertLine.TabIndex = 16;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools2;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,25);
			this.ToolBarMain.TabIndex = 10;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// ToolBarPaint
			// 
			this.ToolBarPaint.ImageList = this.imageListTools2;
			this.ToolBarPaint.Location = new System.Drawing.Point(440,24);
			this.ToolBarPaint.Name = "ToolBarPaint";
			this.ToolBarPaint.Size = new System.Drawing.Size(499,25);
			this.ToolBarPaint.TabIndex = 14;
			this.ToolBarPaint.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.paintTools_ButtonClick);
			// 
			// sliderBrightnessContrast
			// 
			this.sliderBrightnessContrast.Enabled = false;
			this.sliderBrightnessContrast.Location = new System.Drawing.Point(240,32);
			this.sliderBrightnessContrast.MaxVal = 255;
			this.sliderBrightnessContrast.MinVal = 0;
			this.sliderBrightnessContrast.Name = "sliderBrightnessContrast";
			this.sliderBrightnessContrast.Size = new System.Drawing.Size(194,14);
			this.sliderBrightnessContrast.TabIndex = 12;
			this.sliderBrightnessContrast.Text = "contrWindowingSlider1";
			this.sliderBrightnessContrast.Scroll += new System.EventHandler(this.brightnessContrastSlider_Scroll);
			this.sliderBrightnessContrast.ScrollComplete += new System.EventHandler(this.brightnessContrastSlider_ScrollComplete);
			// 
			// ContrImages
			// 
			this.Controls.Add(this.panelVertLine);
			this.Controls.Add(this.panelUnderline);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.ToolBarPaint);
			this.Controls.Add(this.sliderBrightnessContrast);
			this.Controls.Add(this.panelNote);
			this.Controls.Add(this.pictureBoxMain);
			this.Controls.Add(this.treeDocuments);
			this.Name = "ContrImages";
			this.Size = new System.Drawing.Size(939,585);
			this.Resize += new System.EventHandler(this.ContrImages_Resize);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
			this.panelNote.ResumeLayout(false);
			this.panelNote.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrImages_Resize(object sender,EventArgs e) {
			ResizeAll();
		}

		///<summary>Resizes all controls in the image module to fit inside the current window, including controls which have varying visibility.</summary>
		private void ResizeAll(){
			treeDocuments.Height=Height-treeDocuments.Top-2;
			pictureBoxMain.Width=Width-pictureBoxMain.Left-4;
			panelNote.Width=pictureBoxMain.Width;
			panelNote.Height=(int)Math.Min(114,Height-pictureBoxMain.Location.Y);
			int panelNoteHeight=(panelNote.Visible?panelNote.Height:0);
			pictureBoxMain.Height=Height-panelNoteHeight-pictureBoxMain.Top;
			if(axAcroPDF1!=null){
				axAcroPDF1.Location=pictureBoxMain.Location;
				axAcroPDF1.Width=pictureBoxMain.Width;
				axAcroPDF1.Height=pictureBoxMain.Height;
			}
			panelNote.Location=new Point(pictureBoxMain.Left,Height-panelNoteHeight-1);
			ToolBarPaint.Location=new Point(sliderBrightnessContrast.Location.X+sliderBrightnessContrast.Width+4,ToolBarPaint.Location.Y);
			ToolBarPaint.Size=new Size(pictureBoxMain.Width-sliderBrightnessContrast.Width-4,ToolBarPaint.Height);
			panelUnderline.Location=new Point(pictureBoxMain.Location.X,panelUnderline.Location.Y);
			panelUnderline.Width=Width-panelUnderline.Location.X;
		}

		///<summary>Sets the panelnote visibility based on the given document's signature data and the current operating system.</summary>
		private void SetPanelNoteVisibility(Document doc) {
			panelNote.Visible=(doc!=null) && (((doc.Note!=null && doc.Note!="") || (doc.Signature!=null && doc.Signature!="")) && 
				(Environment.OSVersion.Platform!=PlatformID.Unix || !doc.SigIsTopaz));
		}

		///<summary>Also does LayoutToolBar.</summary>
		public void InitializeOnStartup(){
			if(InitializedOnStartup) {
				return;
			}
			InitializedOnStartup=true;
			PointMouseDown=new Point();
			Lan.C(this, new System.Windows.Forms.Control[] {
				//this.button1,
			});
			LayoutToolBar();
			contextTree.MenuItems.Clear();
			contextTree.MenuItems.Add("Print",new System.EventHandler(menuTree_Click));
			contextTree.MenuItems.Add("Delete",new System.EventHandler(menuTree_Click));
			contextTree.MenuItems.Add("Info",new System.EventHandler(menuTree_Click));
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ToolBarPaint.Buttons.Clear();
			ODToolBarButton button;
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Print"),"Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Delete"),"Delete"));
			if(ClaimPaymentNum==0) {
				ToolBarMain.Buttons.Add(new ODToolBarButton("",3,Lan.g(this,"Item Info"),"Info"));
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Sign"),-1,Lan.g(this,"Sign this document"),"Sign"));
			}
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Scan:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",14,Lan.g(this,"Scan Document"),"ScanDoc"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",18,Lan.g(this,"Scan Multi-Page Document"),"ScanMultiDoc"));
			if(ClaimPaymentNum==0) {
				ToolBarMain.Buttons.Add(new ODToolBarButton("",16,Lan.g(this,"Scan Radiograph"),"ScanXRay"));
				ToolBarMain.Buttons.Add(new ODToolBarButton("",15,Lan.g(this,"Scan Photo"),"ScanPhoto"));
			}
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Import"),5,Lan.g(this,"Import From File"),"Import"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Export"),19,Lan.g(this,"Export To File"),"Export"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),17,Lan.g(this,"Copy displayed image to clipboard"),"Copy"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),6,Lan.g(this,"Paste From Clipboard"),"Paste"));
			if(ClaimPaymentNum==0) {
				button=new ODToolBarButton(Lan.g(this,"Templates"),-1,"","Forms");
				button.Style=ODToolBarButtonStyle.DropDownButton;
				menuForms=new ContextMenu();
				string formDir=ODFileUtils.CombinePaths(ImageStore.GetPreferredImagePath(),"Forms");
				if(Directory.Exists(formDir)) {
					DirectoryInfo dirInfo=new DirectoryInfo(formDir);
					FileInfo[] fileInfos=dirInfo.GetFiles();
					for(int i=0;i<fileInfos.Length;i++) {
						if(Documents.IsAcceptableFileName(fileInfos[i].FullName)) {
							menuForms.MenuItems.Add(fileInfos[i].Name,new System.EventHandler(menuForms_Click));
						}
					}
				}
				button.DropDownMenu=menuForms;
				ToolBarMain.Buttons.Add(button);
				button=new ODToolBarButton(Lan.g(this,"Capture"),-1,"Capture Image From Device","Capture");
				button.Style=ODToolBarButtonStyle.ToggleButton;
				ToolBarMain.Buttons.Add(button);
				//Program links:
				ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ImagesModule);
				for(int i=0;i<toolButItems.Count;i++) {
					ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
					ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
						,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
				}
			}
			else {//claimpayment
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,Lan.g(this,"Close window"),"Close"));
			}
			button=new ODToolBarButton("",7,Lan.g(this,"Crop Tool"),"Crop");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			button.Pushed=IsCropMode;
			ToolBarPaint.Buttons.Add(button);
			button=new ODToolBarButton("",10,Lan.g(this,"Hand Tool"),"Hand");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			button.Pushed=!IsCropMode;
			ToolBarPaint.Buttons.Add(button);
			ToolBarPaint.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarPaint.Buttons.Add(new ODToolBarButton("",8,Lan.g(this,"Zoom In"),"ZoomIn"));
			ToolBarPaint.Buttons.Add(new ODToolBarButton("",9,Lan.g(this,"Zoom Out"),"ZoomOut"));
			ToolBarPaint.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Rotate:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarPaint.Buttons.Add(button);
			ToolBarPaint.Buttons.Add(new ODToolBarButton("",11,Lan.g(this,"Flip"),"Flip"));
			ToolBarPaint.Buttons.Add(new ODToolBarButton("",12,Lan.g(this,"Rotate Left"),"RotateL"));
			ToolBarPaint.Buttons.Add(new ODToolBarButton("",13,Lan.g(this,"Rotate Right"),"RotateR"));
			ToolBarMain.Invalidate();
			ToolBarPaint.Invalidate();
			Plugins.HookAddCode(this,"ContrDocs.LayoutToolBar_end",PatCur);
		}

		///<summary>One of two overloads.</summary>
		public void ModuleSelected(long patNum) {
			ModuleSelected(patNum,0);
		}

		///<summary>This overload is needed when jumping to a specific image from FormPatientForms.</summary>
		public void ModuleSelected(long patNum,long docNum) {
			//if(!PrefC.UsingAtoZfolder) {
			//	MsgBox.Show(this,"Not currently using documents. Turn on the A to Z folders option by going to Setup | Data Paths to enable imaging.");
			//	this.Enabled=false;
			//	return;
			//}
			this.Enabled=true;
			RefreshModuleData(patNum);
			RefreshModuleScreen();
			SelectTreeNode(GetNodeById(MakeIdDoc(docNum)));
			Plugins.HookAddCode(this,"ContrImages.ModuleSelected_end",patNum,docNum);
		}

		///<summary>This overload is needed when jumping to a specific image from FormPatientForms.</summary>
		public void ModuleSelectedClaimPayment(long claimPaymentNum) {
			this.Enabled=true;
			ClaimPaymentNum=claimPaymentNum;
			LayoutToolBar();//again
			sliderBrightnessContrast.Visible=false;
			panelVertLine.Visible=false;
			panelUnderline.Visible=false;
			ToolBarPaint.Visible=false;
			pictureBoxMain.Location=new Point(pictureBoxMain.Left,treeDocuments.Top);
			ResizeAll();
			//RefreshModuleData-----------------------------------------------------------------------
			SelectTreeNode(null);//Clear selection and image and reset visibilities.
			//PatFolder=ImageStore.GetPatientFolder(PatCur);//This is where the pat folder gets created if it does not yet exist.
			//RefreshModuleScreen---------------------------------------------------------------------
			EnableAllTools(true);
			EnableAllTreeItemTools(false);
			ToolBarMain.Invalidate();
			ToolBarPaint.Invalidate();
			FillDocList(false);
			if(treeDocuments.Nodes.Count>0) {
				SelectTreeNode(treeDocuments.Nodes[0]);
			}
			//SelectTreeNode(GetNodeById(MakeIdentifier(docNum.ToString(),"0")));
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			//Cancel current image capture.
			xRayImageController.KillXRayThread();
			Plugins.HookAddCode(this,"ContrImages.ModuleUnselected_end");
		}

		///<summary></summary>
		private void RefreshModuleData(long patNum) {
			SelectTreeNode(null);//Clear selection and image and reset visibilities.
			if(patNum==0){
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PatFolder=ImageStore.GetPatientFolder(PatCur);//This is where the pat folder gets created if it does not yet exist.
			ImageStore.AddMissingFilesToDatabase(PatCur);
		}

		private void RefreshModuleScreen(){
			if(this.Enabled && PatCur!=null){
				//Enable tools which must always be accessible when a valid patient is selected.
				EnableAllTools(true);
				//Item specific tools disabled until item chosen.
				EnableAllTreeItemTools(false);
			}
			else{
				EnableAllTools(false);//Disable entire menu (besides select patient).
			}
			ToolBarMain.Invalidate();
			ToolBarPaint.Invalidate();
			FillDocList(false);
		}

		///<summary></summary>
		private void OnPatientSelected(long patNum,string patName,bool hasEmail,string chartNumber) {
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum,patName,hasEmail,chartNumber);
			if(PatientSelected!=null){
				PatientSelected(this,eArgs);
			}
		}

		///<summary>Applies to all tools.</summary>
		private void EnableAllTools(bool enable) {
			for(int i=0;i<ToolBarMain.Buttons.Count;i++){
				ToolBarMain.Buttons[i].Enabled=enable;			
			}
			if(ToolBarMain.Buttons["Capture"]!=null) {
				ToolBarMain.Buttons["Capture"].Enabled=(ToolBarMain.Buttons["Capture"].Enabled && Environment.OSVersion.Platform!=PlatformID.Unix);
			}
			ToolBarMain.Invalidate();
			for(int i=0;i<ToolBarPaint.Buttons.Count;i++){
				ToolBarPaint.Buttons[i].Enabled=enable;
			}
			ToolBarPaint.Enabled=enable;
			ToolBarPaint.Invalidate();
			sliderBrightnessContrast.Enabled=enable;
			sliderBrightnessContrast.Invalidate();
		}

		///<summary>Defined this way to force future programming to consider which tools are enabled and disabled for every possible tool in the menu.</summary>
		private void EnableTreeItemTools(bool print,bool delete,bool info,bool copy,bool sign,bool brightAndContrast,bool crop,bool hand,bool zoomIn,bool zoomOut,bool flip,bool rotateL,bool rotateR,bool export){
			ToolBarMain.Buttons["Print"].Enabled=print;
			ToolBarMain.Buttons["Delete"].Enabled=delete;
			if(ToolBarMain.Buttons["Info"]!=null) {
				ToolBarMain.Buttons["Info"].Enabled=info;
			}
			ToolBarMain.Buttons["Copy"].Enabled=copy;
			if(ToolBarMain.Buttons["Sign"]!=null) {
				ToolBarMain.Buttons["Sign"].Enabled=sign;
			}
			ToolBarMain.Buttons["Export"].Enabled=export;
			ToolBarMain.Invalidate();
			ToolBarPaint.Buttons["Crop"].Enabled=crop;
			ToolBarPaint.Buttons["Hand"].Enabled=hand;
			ToolBarPaint.Buttons["ZoomIn"].Enabled=zoomIn;
			ToolBarPaint.Buttons["ZoomOut"].Enabled=zoomOut;
			ToolBarPaint.Buttons["Flip"].Enabled=flip;
			ToolBarPaint.Buttons["RotateR"].Enabled=rotateR;
			ToolBarPaint.Buttons["RotateL"].Enabled=rotateL;
			//Enabled if one tool inside is enabled.
			ToolBarPaint.Enabled=(brightAndContrast||crop||hand||zoomIn||zoomOut||flip||rotateL||rotateR);
			ToolBarPaint.Invalidate();
			sliderBrightnessContrast.Enabled=brightAndContrast;
			sliderBrightnessContrast.Invalidate();
		}

		private void EnableAllTreeItemTools(bool enable){
			EnableTreeItemTools(enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable);
		}

		///<summary>Selection doesn't only happen by the tree and mouse clicks, but can also happen by automatic processes, such as image import, image paste, etc...</summary>
		private void SelectTreeNode(TreeNode node){
			//Select the node always, but perform additional tasks when necessary (i.e. load an image, or mount).
			treeDocuments.SelectedNode=node;
			treeDocuments.Invalidate();
			//Clear the copy document number for mount item swapping whenever a new mount is potentially selected.
			IdxDocToCopy=-1;
			//We only perform a load if the new selection is different than the old selection.
			ImageNodeId nodeId=new ImageNodeId();
			if(node!=null) {
				nodeId=(ImageNodeId)node.Tag;
			}
			if(nodeId.Equals(NodeIdentifierOld)){
				return;
			}
			pictureBoxMain.Visible=true;
			if(axAcroPDF1!=null){
				axAcroPDF1.Dispose();//Clear any previously loaded Acrobat .pdf file.
			}
			DocSelected=new Document();
			NodeIdentifierOld=nodeId;
			//Disable all item tools until the currently selected node is loaded properly in the picture box.
			EnableAllTreeItemTools(false);
			ToolBarPaint.Buttons["Hand"].Pushed=true;
			ToolBarPaint.Buttons["Crop"].Pushed=false;
			//Stop any current image processing. This will avoid having the ImageRenderingNow set to a valid image after
			//the current image has been erased. This will also avoid concurrent access to the the currently loaded images by
			//the main and worker threads.
			EraseCurrentImages();
			if(nodeId.NodeType==ImageNodeType.Category) {
				//A folder was selected (or unselection, but I am not sure unselection would be possible here).
				//The panel note control is made invisible to start and then made visible for the appropriate documents. This
				//line prevents the possibility of showing a signature box after selecting a folder node.
				panelNote.Visible=false;
				//Make sure the controls are sized properly in the image module since the visibility of the panel note might
				//have just changed.
				ResizeAll();
			}
			else if(nodeId.NodeType==ImageNodeType.Eob) {
				EobAttach eob=EobAttaches.GetOne(nodeId.PriKey);
				ImagesCur=ImageStore.OpenImagesEob(eob);
				if(ImagesCur[0]==null) {
					if(ImageHelper.HasImageExtension(eob.FileName)) {
						MessageBox.Show(Lan.g(this,"File not found: ") + eob.FileName);
					}
					else if(Path.GetExtension(eob.FileName).ToLower()==".pdf") {//Adobe acrobat file.
						try {
							axAcroPDF1=new AxAcroPDFLib.AxAcroPDF();
							this.Controls.Add(axAcroPDF1);
							axAcroPDF1.Visible=true;
							axAcroPDF1.Size=pictureBoxMain.Size;
							axAcroPDF1.Location=pictureBoxMain.Location;
							axAcroPDF1.OnError+=new EventHandler(pdfFileError);
							string pdfFilePath=ODFileUtils.CombinePaths(PatFolder,eob.FileName);
							if(!File.Exists(pdfFilePath)) {
								MessageBox.Show(Lan.g(this,"File not found: ") + eob.FileName);
							}
							else {
								axAcroPDF1.LoadFile(pdfFilePath);//The return status of this function doesn't seem to be helpful.
								pictureBoxMain.Visible=false;
							}
						}
						catch {
							//An exception can happen if they do not have Adobe Acrobat Reader version 8.0 or later installed.
							//Simply ignore this exception and do nothing. We never used to display .pdf files anyway, so we
							//essentially revert back to the old behavior in this case.
						}
					}
				}
			}
			else if(nodeId.NodeType==ImageNodeType.Doc) {
				//Reload the doc from the db. We don't just keep reusing the tree data, because it will become more and 
				//more stale with age if the program is left open in the image module for long periods of time.
				DocSelected=Documents.GetByNum(nodeId.PriKey);
				IdxSelectedInMount=0;
				ImagesCur=ImageStore.OpenImages(new Document[] { DocSelected },PatFolder);
				if(ImagesCur[0]==null) {
					if(ImageHelper.HasImageExtension(DocSelected.FileName)) {
						MessageBox.Show(Lan.g(this,"File not found: ") + DocSelected.FileName);
					}
					else if(Path.GetExtension(DocSelected.FileName).ToLower()==".pdf") {//Adobe acrobat file.
						try {
							axAcroPDF1=new AxAcroPDFLib.AxAcroPDF();
							this.Controls.Add(axAcroPDF1);
							axAcroPDF1.Visible=true;
							axAcroPDF1.Size=pictureBoxMain.Size;
							axAcroPDF1.Location=pictureBoxMain.Location;
							axAcroPDF1.OnError+=new EventHandler(pdfFileError);
							string pdfFilePath=ODFileUtils.CombinePaths(PatFolder,DocSelected.FileName);
							if(!File.Exists(pdfFilePath)) {
								MessageBox.Show(Lan.g(this,"File not found: ") + DocSelected.FileName);
							}
							else {
								axAcroPDF1.LoadFile(pdfFilePath);//The return status of this function doesn't seem to be helpful.
								pictureBoxMain.Visible=false;
							}
						}
						catch {
							//An exception can happen if they do not have Adobe Acrobat Reader version 8.0 or later installed.
							//Simply ignore this exception and do nothing. We never used to display .pdf files anyway, so we
							//essentially revert back to the old behavior in this case.
						}
					}
				}
				SetBrightnessAndContrast();
				EnableTreeItemTools(pictureBoxMain.Visible,true,true,pictureBoxMain.Visible,true,pictureBoxMain.Visible,pictureBoxMain.Visible,pictureBoxMain.Visible,
					pictureBoxMain.Visible,pictureBoxMain.Visible,pictureBoxMain.Visible,pictureBoxMain.Visible,pictureBoxMain.Visible,pictureBoxMain.Visible);
			}
			else if(nodeId.NodeType==ImageNodeType.Mount) {
				//DataRow obj=(DataRow)node.Tag;
				//long mountNum=PIn.Long(obj["MountNum"].ToString());
				//long docNum=PIn.Long(obj["DocNum"].ToString());
				//if(mountNum!=0){//This is a mount node.
				//Creates a complete initial mount image. No need to call invalidate until changes are made to the mount later.
				MountItemsForSelected=MountItems.GetItemsForMount(nodeId.PriKey);
				DocsInMount=Documents.GetDocumentsForMountItems(MountItemsForSelected);
				IdxSelectedInMount=-1;//No selection to start.
				ImagesCur=ImageStore.OpenImages(DocsInMount,PatFolder);
				MountSelected=Mounts.GetByNum(nodeId.PriKey);
				ImageRenderingNow=new Bitmap(MountSelected.Width,MountSelected.Height);
				ImageHelper.RenderMountImage(ImageRenderingNow,ImagesCur,MountItemsForSelected,DocsInMount,IdxSelectedInMount);
				EnableTreeItemTools(true,true,true,true,false,false,false,true,true,true,false,false,false,true);
			}
			if(nodeId.NodeType==ImageNodeType.Doc || nodeId.NodeType==ImageNodeType.Mount) {
				WidthsImagesCur=new int[ImagesCur.Length];
				HeightsImagesCur=new int[ImagesCur.Length];
				for(int i=0;i<ImagesCur.Length;i++) {
					if(ImagesCur[i]!=null){
						WidthsImagesCur[i]=ImagesCur[i].Width;
						HeightsImagesCur[i]=ImagesCur[i].Height;
					}
				}
				//Adjust visibility of panel note control based on if the new document has a signature.
				SetPanelNoteVisibility(DocSelected);
				//Resize controls in our window to adjust for a possible change in the visibility of the panel note control.
				ResizeAll();
				//Refresh the signature and note in case the last document also had a signature.
				FillSignature();
			}
			if(nodeId.NodeType==ImageNodeType.Mount) {
				ReloadZoomTransCrop(ImageRenderingNow.Width,ImageRenderingNow.Height,new Document(),
					new Rectangle(0,0,pictureBoxMain.Width,pictureBoxMain.Height),out ZoomImage,
					out ZoomLevel,out ZoomOverall,out PointTranslation);
				RenderCurrentImage(new Document(),ImageRenderingNow.Width,ImageRenderingNow.Height,ZoomImage,PointTranslation);
			}
			if(nodeId.NodeType==ImageNodeType.Doc) {
				//Render the initial image within the current bounds of the picturebox (if the document is an image).
				InvalidateSettings(ImageSettingFlags.ALL,true);
			}
		}

		private void pdfFileError(object sender, System.EventArgs e) {
			pictureBoxMain.Visible=true;
			ToolBarPaint.Enabled=true;
			sliderBrightnessContrast.Enabled=true;
			axAcroPDF1.Visible=false;
		}

		///<summary>Gets the category folder name for the given document node.</summary>
		private string GetCurrentFolderName(TreeNode node) {
			if(node!=null) {
				while(node.Parent!=null) {//Find the corresponding root level node.
					node=node.Parent;
				}
				return node.Text;
			}
			//We must always return a category if one is available, so that new documents can be properly added.
			if(treeDocuments.Nodes.Count>0) {
				return treeDocuments.Nodes[0].Text;
			}
			return "";
		}

		///<summary>Gets the document category of the current selection. The current selection can be a folder itself, or a document within a folder.</summary>
		private long GetCurrentCategory() {
			return DefC.GetByExactName(DefCat.ImageCats,GetCurrentFolderName(treeDocuments.SelectedNode));
		}

		///<summary>Returns the current tree node with the given node id.</summary>
		private TreeNode GetNodeById(ImageNodeId nodeId) {
			return GetNodeById(nodeId,treeDocuments.Nodes);//This defines the root node.
		}

		///<summary>Searches the current object tree for a row which has the given unique document number. This will work for a tree with any number of nested folders, as long as tags are defined only for items which correspond to data rows.</summary>
		private TreeNode GetNodeById(ImageNodeId nodeId,TreeNodeCollection rootNodes) {
			if(rootNodes==null) {
				return null;
			}
			foreach(TreeNode node in rootNodes) {
				if(node==null){
					continue;
				}
				if(((ImageNodeId)node.Tag).Equals(nodeId)) {
					return node;
				}
				//Check the child nodes.
				TreeNode child=GetNodeById(nodeId,node.Nodes);
				if(child!=null) {
					return child;
				}
			}
			return null;
		}

		//private TreeNode

		/*
		///<summary>Constructs a unique node nodeId that would be the same for nodes with tags containing the same data.</summary>
		private string GetNodeIdentifier(TreeNode node) {
			if(node==null || node.Tag==null){
				return "";
			}
			if(node.Parent==null) {//folder node.

			}
			DataRow datarow=(DataRow)node.Tag;
			return MakeIdentifier(datarow["DocNum"].ToString(),datarow["MountNum"].ToString());
		}*/

		///<summary></summary>
		private ImageNodeId MakeIdDoc(long docNum){
			ImageNodeId nodeId=new ImageNodeId();
			nodeId.NodeType=ImageNodeType.Doc;
			nodeId.PriKey=docNum;
			return nodeId;
			//return docNum+"*"+mountNum;
		}

		///<summary></summary>
		private ImageNodeId MakeIdMount(long mountNum) {
			ImageNodeId nodeId=new ImageNodeId();
			nodeId.NodeType=ImageNodeType.Mount;
			nodeId.PriKey=mountNum;
			return nodeId;
		}

		///<summary></summary>
		private ImageNodeId MakeIdDef(long defNum) {
			ImageNodeId nodeId=new ImageNodeId();
			nodeId.NodeType=ImageNodeType.Category;
			nodeId.PriKey=defNum;
			return nodeId;
		}

		///<summary></summary>
		private ImageNodeId MakeIdEob(long eobAttachNum) {
			ImageNodeId nodeId=new ImageNodeId();
			nodeId.NodeType=ImageNodeType.Eob;
			nodeId.PriKey=eobAttachNum;
			return nodeId;
		}

		/// <summary>Refreshes list from db, then fills the treeview.  Set keepSelection to true in order to keep the current selection active.</summary>
		private void FillDocList(bool keepSelection){
			ImageNodeId nodeIdSelection=new ImageNodeId();
			if(keepSelection) {
				nodeIdSelection=(ImageNodeId)treeDocuments.SelectedNode.Tag;
			}
			//(keepSelection?GetNodeIdentifier(treeDocuments.SelectedNode):"");
			//Clear current tree contents.
			treeDocuments.SelectedNode=null;
			treeDocuments.Nodes.Clear();
			if(ClaimPaymentNum!=0) {
				List<EobAttach> listEobs=EobAttaches.Refresh(ClaimPaymentNum);
				for(int i=0;i<listEobs.Count;i++) {
					TreeNode node=new TreeNode(listEobs[i].FileName);
					node.Tag=MakeIdEob(listEobs[i].EobAttachNum);
					node.ImageIndex=2;
					node.SelectedImageIndex=node.ImageIndex;//redundant?
					treeDocuments.Nodes.Add(node);
					if(((ImageNodeId)node.Tag).Equals(nodeIdSelection)) {
						SelectTreeNode(node);
					}
				}
				return;
			}
			//the rest of this is for normal images module-----------------------------------------------------------------------------------------------------------------
			if(PatCur==null){
				return;
			}
			//Add all predefined folder names to the tree.
			for(int i=0;i<DefC.Short[(int)DefCat.ImageCats].Length;i++){
				treeDocuments.Nodes.Add(new TreeNode(DefC.Short[(int)DefCat.ImageCats][i].ItemName));
				treeDocuments.Nodes[i].Tag=MakeIdDef(DefC.Short[(int)DefCat.ImageCats][i].DefNum);
				treeDocuments.Nodes[i].SelectedImageIndex=1;
				treeDocuments.Nodes[i].ImageIndex=1;
			}
			//Add all relevant documents and mounts as stored in the database to the tree for the current patient.
			DataSet ds=Documents.RefreshForPatient(new string[] { PatCur.PatNum.ToString() });
			DataRowCollection rows=ds.Tables["DocumentList"].Rows;
			for(int i=0;i<rows.Count;i++) {
				TreeNode node=new TreeNode(rows[i]["description"].ToString());
				int parentFolder=PIn.Int(rows[i]["docFolder"].ToString());
				treeDocuments.Nodes[parentFolder].Nodes.Add(node);
				if(rows[i]["DocNum"].ToString()=="0") {//must be a mount
					node.Tag=MakeIdMount(PIn.Long(rows[i]["MountNum"].ToString()));
				}
				else {//doc
					node.Tag=MakeIdDoc(PIn.Long(rows[i]["DocNum"].ToString()));
				}
				node.ImageIndex=2+Convert.ToInt32(rows[i]["ImgType"].ToString());
				node.SelectedImageIndex=node.ImageIndex;
				if(((ImageNodeId)node.Tag).Equals(nodeIdSelection)) {
					SelectTreeNode(node);
				}				
			}
			if(PrefC.GetBool(PrefName.ImagesModuleTreeIsCollapsed)) {
				TreeNode selectedNode=treeDocuments.SelectedNode;//Save the selection so we can reselect after collapsing.
				treeDocuments.CollapseAll();//Invalidates tree and clears selection too.
				treeDocuments.SelectedNode=selectedNode;//This will expand any category/folder nodes necessary to show the selection.
				if(PatNumPrev==PatCur.PatNum) {//Maintain previously expanded nodes when patient not changed.
					for(int i=0;i<ExpandedCats.Count;i++) {
						for(int j=0;j<treeDocuments.Nodes.Count;j++) {
							if(ExpandedCats[i]==((ImageNodeId)treeDocuments.Nodes[j].Tag).PriKey) {
								treeDocuments.Nodes[j].Expand();
								break;
							}
						}
					}
				}
				else {//Patient changed.
					ExpandedCats.Clear();
				}
				PatNumPrev=PatCur.PatNum;
			}
			else {
				treeDocuments.ExpandAll();//Invalidates tree too.
			}
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				switch(e.Button.Tag.ToString()){
					case "Print":
						ToolBarPrint_Click();
						break;
					case "Delete":
						ToolBarDelete_Click();
						break;
					case "Info":
						ToolBarInfo_Click();
						break;
					case "Sign":
						ToolBarSign_Click();
						break;
					case "ScanDoc":
						ToolBarScan_Click("doc");
						break;
					case "ScanMultiDoc":
						ToolBarScanMulti_Click();
						break;
					case "ScanXRay":
						ToolBarScan_Click("xray");
						break;
					case "ScanPhoto":
						ToolBarScan_Click("photo");
						break;
					case "Import":
						ToolBarImport_Click();
						break;
					case "Export":
						ToolBarExport_Click();
						break;
					case "Copy":
						ToolBarCopy_Click();
						break;
					case "Paste":
						ToolBarPaste_Click();
						break;
					case "Forms":
						MsgBox.Show(this,"Use the dropdown list.  Add forms to the list by copying image files into your A-Z folder, Forms.  Restart the program to see newly added forms.");
						break;
					case "Capture":
						ToolBarCapture_Click();
						break;
					case "Close":
						ToolBarClose_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(long)) {
				ProgramL.Execute((long)e.Button.Tag,PatCur);
			}
		}

		private void paintTools_ButtonClick(object sender,ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)) {
				switch(e.Button.Tag.ToString()) {
					case "Crop":
						ToolBarCrop_Click();
						break;
					case "Hand":
						ToolBarHand_Click();
						break;
					case "ZoomIn":
						ToolBarZoomIn_Click();
						break;
					case "ZoomOut":
						ToolBarZoomOut_Click();
						break;
					case "Flip":
						ToolBarFlip_Click();
						break;
					case "RotateL":
						ToolBarRotateL_Click();
						break;
					case "RotateR":
						ToolBarRotateR_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)) {
				ProgramL.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void ToolBarPrint_Click(){
			if(treeDocuments.SelectedNode==null || treeDocuments.SelectedNode.Tag==null) {
				MsgBox.Show(this,"Cannot print. No document currently selected.");
				return;
			}
			try {
				PrintDocument pd=new PrintDocument();
				pd.PrintPage+=new PrintPageEventHandler(printDocument_PrintPage);
				PrintDialog dlg=new PrintDialog();
				dlg.AllowCurrentPage=false;
				dlg.AllowPrintToFile=true;
				dlg.AllowSelection=false;
				dlg.AllowSomePages=false;
				dlg.Document=pd;
				dlg.PrintToFile=false;
				dlg.ShowHelp=true;
				dlg.ShowNetwork=true;
				dlg.UseEXDialog=true; //needed because PrintDialog was not showing on 64 bit Vista systems
				if(dlg.ShowDialog()==DialogResult.OK) {
					if(pd.DefaultPageSettings.PrintableArea.Width==0||
							pd.DefaultPageSettings.PrintableArea.Height==0) {
						pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
					}
					pd.OriginAtMargins=true;
					pd.DefaultPageSettings.Margins=new Margins(50,50,50,50);//Half-inch all around
					pd.Print();
				}
			} 
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"An error occurred while printing")+"\r\n"+ex.ToString());
			}
		}

		///<summary>If the node does not correspond to a valid document or mount, nothing happens. Otherwise the document/mount record and its corresponding file(s) are deleted.</summary>
		private void ToolBarDelete_Click(){
			DeleteSelection(true,true);
		}
		
		///<summary>Deletes the current selection from the database and refreshes the tree view. Set securityCheck false when creating a new document that might get cancelled.</summary>
		private void DeleteSelection(bool verbose,bool securityCheck){
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None) {
				MsgBox.Show(this,"No item is currently selected");
				return;//No current selection, or some kind of internal error somehow.
			}
			if(treeDocuments.SelectedNode.Parent==null){
				MsgBox.Show(this,"Cannot delete folders");
				return;
			}
			DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
			long mountNum=Convert.ToInt64(datarow["MountNum"].ToString());
			long docNum=Convert.ToInt64(datarow["DocNum"].ToString());
			Document doc=Documents.GetByNum(docNum);
			if(securityCheck) {
				if(!Security.IsAuthorized(Permissions.ImageDelete,doc.DateCreated)) {
					return;
				}
			}
			EnableAllTreeItemTools(false);
			Document[] docs;
			bool refreshTree=true;
			if(mountNum!=0){//This is a mount object.
				//Delete the mount object.
				Mount mount=Mounts.GetByNum(mountNum);
				//Delete the mount items attached to the mount object.
				List <MountItem> mountItems=MountItems.GetItemsForMount(mountNum);
				if(IdxSelectedInMount>=0 && DocsInMount[IdxSelectedInMount]!=null){
					if(verbose) {
						if(!MsgBox.Show(this,true,"Delete mount xray image?")) {
							return;
						}
					}
					DocSelected=new Document();
					docs=new Document[1] { DocsInMount[IdxSelectedInMount] };
					DocsInMount[IdxSelectedInMount]=null;
					//Release access to current image so it may be properly deleted.
					if(ImagesCur[IdxSelectedInMount]!=null){
						ImagesCur[IdxSelectedInMount].Dispose();
						ImagesCur[IdxSelectedInMount]=null;
					}
					InvalidateSettings(ImageSettingFlags.ALL,false);
					refreshTree=false;
				}
				else{
					if(verbose) {
						if(!MsgBox.Show(this,true,"Delete entire mount?")) {
							return;
						}
					}
					docs=DocsInMount;
					Mounts.Delete(mount);
					for(int i=0;i<mountItems.Count;i++) {
						MountItems.Delete(mountItems[i]);
					}
					SelectTreeNode(null);//Release access to current image so it may be properly deleted.
				}				
			}
			else{//doc object, not mount
				if(verbose) {
					if(!MsgBox.Show(this,true,"Delete document?")) {
						return;
					}
				}
				docs=new Document[1] { doc };
				SelectTreeNode(null);//Release access to current image so it may be properly deleted.
			}
			//Delete all documents involved in deleting this object.
			//ImageStoreBase.verbose=verbose;
			ImageStore.DeleteImage(docs,PatFolder);
			if(refreshTree){
				FillDocList(false);
			}
		}

		private void ToolBarSign_Click(){
			if(treeDocuments.SelectedNode==null ||				//No selection
				treeDocuments.SelectedNode.Tag==null ||			//Internal error
				treeDocuments.SelectedNode.Parent==null){		//This is a folder node.
				return;
			}
			//Show the underlying panel note box while the signature is being filled.
			panelNote.Visible=true;
			ResizeAll();
			//Display the document signature form.
			FormDocSign docSignForm=new FormDocSign(DocSelected,PatCur);//Updates our local document and saves changes to db also.
			int signLeft=treeDocuments.Left;
			docSignForm.Location=PointToScreen(new Point(signLeft,this.ClientRectangle.Bottom-docSignForm.Height));
			docSignForm.Width=Math.Max(0,Math.Min(docSignForm.Width,pictureBoxMain.Right-signLeft));
			docSignForm.ShowDialog();
			FillDocList(true);
			//Adjust visibility of panel note based on changes made to the signature above.
			SetPanelNoteVisibility(DocSelected);
			//Resize controls in our window to adjust for a possible change in the visibility of the panel note control.
			ResizeAll();
			//Update the signature and note with the new data.
			FillSignature();
		}

		///<summary>DO NOT CALL UNLESS THE CURRENTLY SELECTED NODE IS A DOCUMENT NODE. Fills the panelnote control with the current document signature when the panelnote is visible and when a valid document is currently selected.</summary>
		private void FillSignature() {
			textNote.Text="";
			sigBox.ClearTablet();
			if(!panelNote.Visible) {
				return;
			}
			DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
			textNote.Text=DocSelected.Note;
			labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			sigBox.SetTabletState(0);//never accepts input here
			//Topaz box is not supported in Unix, since the required dll is Windows native.
			if(DocSelected.SigIsTopaz) {
				if(DocSelected.Signature!=null && DocSelected.Signature!="") {
					//if(allowTopaz) {	
					sigBox.Visible=false;
					SigBoxTopaz.Visible=true;
					CodeBase.TopazWrapper.ClearTopaz(SigBoxTopaz);
					CodeBase.TopazWrapper.SetTopazCompressionMode(SigBoxTopaz,0);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(SigBoxTopaz,0);
					string keystring=GetHashString(DocSelected);
					CodeBase.TopazWrapper.SetTopazKeyString(SigBoxTopaz,keystring);
					CodeBase.TopazWrapper.SetTopazEncryptionMode(SigBoxTopaz,2);//high encryption
					CodeBase.TopazWrapper.SetTopazCompressionMode(SigBoxTopaz,2);//high compression
					CodeBase.TopazWrapper.SetTopazSigString(SigBoxTopaz,DocSelected.Signature);
					SigBoxTopaz.Refresh();
					//If sig is not showing, then try encryption mode 3 for signatures signed with old SigPlusNet.dll.
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(SigBoxTopaz)==0) {
						CodeBase.TopazWrapper.SetTopazEncryptionMode(SigBoxTopaz,3);//Unknown mode (told to use via TopazSystems)
						CodeBase.TopazWrapper.SetTopazSigString(SigBoxTopaz,DocSelected.Signature);
					}
					if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(SigBoxTopaz)==0) {
						labelInvalidSig.Visible=true;
					}
					//}
				}
			}
			else {//not topaz
				if(DocSelected.Signature!=null && DocSelected.Signature!="") {
					sigBox.Visible=true;
					//if(allowTopaz) {	
					SigBoxTopaz.Visible=false;
					//}
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(GetHashString(DocSelected));
					//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(DocSelected.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.
				}
			}
		}

		private string GetHashString(Document doc) {
			return ImageStore.GetHashString(doc,PatFolder);
		}

		///<summary>Valid values for scanType are "doc","xray",and "photo"</summary>
		private void ToolBarScan_Click(string scanType) {
			Bitmap bitmapScanned=null;
			IntPtr hdib=IntPtr.Zero;
			//Try catch here prevents a crash when a customer who has no scanner installed tries to scan.
			try {
				//A user may have more than one scanning device. 
				//The code below will allow the user to select one.
				xImageDeviceManager.Obfuscator.ActivateEZTwain();
				bool wPIXTypes=EZTwain.SelectImageSource(this.Handle);
				if(!wPIXTypes) {//user clicked Cancel
					return;
				}
				hdib=EZTwain.AcquireMemory(this.Handle);
				double xdpi=EZTwain.DIB_XResolution(hdib);
				double ydpi=EZTwain.DIB_XResolution(hdib);
				IntPtr hbitmap=EZTwain.DIB_ToDibSection(hdib);
				bitmapScanned=Bitmap.FromHbitmap(hbitmap);
				bitmapScanned.SetResolution((float)xdpi,(float)ydpi);
				Clipboard.SetImage(bitmapScanned);
			}
			catch(Exception ex) {
				MessageBox.Show("The image could not be acquired from the scanner. "+
					"Please check to see that the scanner is properly connected to the computer. Specific error: "+ex.Message);
				return;
			}
			ImageType imgType;
			if(scanType=="xray") {
				imgType=ImageType.Radiograph;
			}
			else if(scanType=="photo") {
				imgType=ImageType.Photo;
			}
			else {//Assume document
				imgType=ImageType.Document;
			}
			bool saved=true;
			Document doc = null;
			try {//Create corresponding image file.
				doc=ImageStore.Import(bitmapScanned,GetCurrentCategory(),imgType,PatCur);
			}
			catch(Exception ex) {
				saved=false;
				MessageBox.Show(Lan.g(this,"Unable to save document")+": "+ex.Message);
			}
			if(bitmapScanned!=null) {
				bitmapScanned.Dispose();
			}
			if(hdib!=IntPtr.Zero) {
				EZTwain.DIB_Free(hdib);
			}
			if(saved) {
				FillDocList(false);//Reload and keep new document selected.
				SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum)));
				FormDocInfo formDocInfo=new FormDocInfo(PatCur,DocSelected,GetCurrentFolderName(treeDocuments.SelectedNode));
				formDocInfo.ShowDialog();
				if(formDocInfo.DialogResult!=DialogResult.OK) {
					DeleteSelection(false,false);
				}
				else {
					FillDocList(true);//Update tree, in case the new document's icon or category were modified in formDocInfo.
				}
			}
		}

		private void ToolBarScanMulti_Click() {
			string tempFile=Path.GetTempFileName().Replace(".tmp", ".pdf");
			xImageDeviceManager.Obfuscator.ActivateEZTwain();
			//it will always use the system default scanner.  No mechanism for picking scanner.
			EZTwain.SetHideUI(PrefC.GetBool(PrefName.ScannerSuppressDialog));//if true, this will bring up the scanner interface for the selected scanner a few lines down
			EZTwain.SetJpegQuality((int)PrefC.GetLong(PrefName.ScannerCompression));
			if(EZTwain.OpenDefaultSource()) {//if it opens the scanner successfully
				EZTwain.SetPixelType(2);//24-bit Color
				EZTwain.SetResolution((int)PrefC.GetLong(PrefName.ScannerResolution));
				EZTwain.AcquireMultipageFile(this.Handle,tempFile);
			}
			else {
				MsgBox.Show(this,"Default scanner could not be opened.  Check that the default scanner works from Windows Control Panel and from Windows Fax and Scan.");
				return;
			}
			int errorCode=EZTwain.LastErrorCode();
			if(errorCode!=0) {
				string message="";
				if(errorCode==(int)EZTwainErrorCode.EZTEC_USER_CANCEL) {//19
					//message="\r\nScanning cancelled.";//do nothing
					return;
				}
				else if(errorCode==(int)EZTwainErrorCode.EZTEC_JPEG_DLL) {//22
					message="\r\nRequired file EZJpeg.dll is missing.";
				}
				else if(errorCode==(int)EZTwainErrorCode.EZTEC_0_PAGES) {//38
					//message="\r\nScanning cancelled.";//do nothing
					return;
				}
				else if(errorCode==(int)EZTwainErrorCode.EZTEC_NO_PDF) {//43
					message="\r\nRequired file EZPdf.dll is missing.";
				}
				else if(errorCode==(int)EZTwainErrorCode.EZTEC_DEVICE_PAPERJAM) {//76
					message="\r\nPaper jam.";
				}
				MessageBox.Show(Lan.g(this,"Unable to scan. Error code: ")+errorCode+((EZTwainErrorCode)errorCode).ToString()+message);
				return;
			}
			ImageNodeId nodeId=new ImageNodeId(); 
			Document doc=null; 
			bool copied = true; 
			try { 
				doc=ImageStore.Import(tempFile, GetCurrentCategory(),PatCur); 
			} 
			catch(Exception ex) { 
				MessageBox.Show(Lan.g(this, "Unable to copy file, May be in use: ") + ex.Message + ": " + tempFile); 
				copied = false; 
			} 
			if(copied){ 
				FillDocList(false); 
				SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum))); 
				FormDocInfo FormD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(treeDocuments.SelectedNode)); 
				FormD.ShowDialog();//some of the fields might get changed, but not the filename 
				if(FormD.DialogResult!=DialogResult.OK){ 
					DeleteSelection(false,false); 
				}
				else{
					nodeId=MakeIdDoc(doc.DocNum);
					DocSelected=doc.Copy();
				} 
			} 
			File.Delete(tempFile);
			//Reselect the last successfully added node when necessary.
			if(doc!=null && !MakeIdDoc(doc.DocNum).Equals(nodeId)) {
				SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum)));
			}
			FillDocList(true);
		}

		private void ToolBarImport_Click() {
			OpenFileDialog openFileDialog=new OpenFileDialog();
			openFileDialog.Multiselect=true;
			if(openFileDialog.ShowDialog()!=DialogResult.OK) {
				return;
			}
			string[] fileNames=openFileDialog.FileNames;
			if(fileNames.Length<1){
				return;
			}
			ImageNodeId nodeId=new ImageNodeId();
			Document doc=null;
			for(int i=0;i<fileNames.Length;i++){
				bool copied = true;
				try {
					doc=ImageStore.Import(fileNames[i],GetCurrentCategory(),PatCur);
				}
				catch(Exception ex) {
					MessageBox.Show(Lan.g(this, "Unable to copy file, May be in use: ") + ex.Message + ": " + openFileDialog.FileName);
					copied = false;
				}
				if(copied){
					FillDocList(false);
					SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum)));
					FormDocInfo FormD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(treeDocuments.SelectedNode));
					FormD.ShowDialog();//some of the fields might get changed, but not the filename
					if(FormD.DialogResult!=DialogResult.OK){
						DeleteSelection(false,false);
					}
					else{
						nodeId=MakeIdDoc(doc.DocNum);
						DocSelected=doc.Copy();
					}
				}
			}
			//Reselect the last successfully added node when necessary.
			if(doc!=null && !MakeIdDoc(doc.DocNum).Equals(nodeId)) {
				SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum)));
			}
			FillDocList(true);
		}
		
		private void ToolBarExport_Click() {
			TreeNode node=treeDocuments.SelectedNode;
			Document docSelected=Documents.GetByNum(DocSelected.DocNum);//seems redundant
			if(node==null || docSelected==null) {
				MsgBox.Show(this,"Please select a document to export.");
				return;
			}
			SaveFileDialog dlg=new SaveFileDialog();
			dlg.Title="Export a Document";
			dlg.FileName=docSelected.FileName;
			if(dlg.ShowDialog()!=DialogResult.OK) {
				return;
			}
			string fileName=dlg.FileName;
			if(fileName.Length<1){
				MsgBox.Show(this,"You must enter a file name.");
				return;
			}
			try {
				ImageStore.Export(fileName,docSelected,PatCur);
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this, "Unable to export file, May be in use: ") + ex.Message + ": " + fileName);
			}
			MessageBox.Show(Lan.g(this,"Document successfully exported to ")+fileName);
		}

		private void ToolBarCopy_Click(){
			if(treeDocuments.SelectedNode==null || treeDocuments.SelectedNode.Tag==null){
				MsgBox.Show(this,"Please select a document before copying");
				return;
			}
			Cursor=Cursors.WaitCursor;
			DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
			long mountNum=Convert.ToInt64(datarow["MountNum"]);
			long docNum=Convert.ToInt64(datarow["DocNum"]);
			Bitmap bitmapCopy;
			if(mountNum!=0){//The current selection is a mount
				if(IdxSelectedInMount>=0 && DocsInMount[IdxSelectedInMount]!=null){//A mount item is currently selected.
					bitmapCopy=ImageHelper.ApplyDocumentSettingsToImage(DocsInMount[IdxSelectedInMount],ImagesCur[IdxSelectedInMount],ImageSettingFlags.ALL);
				}
				else{//Assume the copy is for the entire mount.
					bitmapCopy=(Bitmap)ImageRenderingNow.Clone();
				}
			}
			else{//document
				//Crop and color function has already been applied to the render image.
				bitmapCopy=ImageHelper.ApplyDocumentSettingsToImage(Documents.GetByNum(docNum),ImageRenderingNow,
					ImageSettingFlags.FLIP | ImageSettingFlags.ROTATE);
			}
			if(bitmapCopy!=null){
				Clipboard.SetDataObject(bitmapCopy);
			}
			Cursor=Cursors.Default;
		}

		private void ToolBarPaste_Click() {
			IDataObject clipboard=Clipboard.GetDataObject();
			if(!clipboard.GetDataPresent(DataFormats.Bitmap)) {
				MessageBox.Show(Lan.g(this,"No bitmap present on clipboard"));
				return;
			}
			Bitmap bitmapPaste=(Bitmap)clipboard.GetData(DataFormats.Bitmap);
			Document doc;
			long mountNum=0;
			long docNum=0;
			if(treeDocuments.SelectedNode!=null && treeDocuments.SelectedNode.Tag!=null && treeDocuments.SelectedNode.Tag.GetType()==typeof(DataRow)) {
				DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
				mountNum=Convert.ToInt64(datarow["MountNum"]);
				docNum=Convert.ToInt64(datarow["DocNum"]);
			}
			Cursor=Cursors.WaitCursor;
			if(ClaimPaymentNum!=0) {
				EobAttach eob=null;
				try {
					eob=ImageStore.ImportEobAttach(bitmapPaste,ClaimPaymentNum);
				}
				catch {
					MessageBox.Show(Lan.g(this,"Error saving eob."));
					Cursor=Cursors.Default;
					return;
				}
				FillDocList(false);
				//SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
			}
			else if(mountNum!=0 && IdxSelectedInMount>=0) {//Pasting into the mount item of the currently selected mount.
				if(DocsInMount[IdxSelectedInMount]!=null) {
					if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"Do you want to replace the existing item in this mount location?")) {
						this.Cursor=Cursors.Default;
						return;
					}
					DeleteSelection(false,true);
				}
				try {
					doc=ImageStore.ImportImageToMount(bitmapPaste,0,MountItemsForSelected[IdxSelectedInMount].MountItemNum,GetCurrentCategory(),PatCur);
					doc.WindowingMax=255;
					doc.WindowingMin=0;
					Documents.Update(doc);
				}
				catch {
					MessageBox.Show(Lan.g(this,"Error saving document."));
					Cursor=Cursors.Default;
					return;
				}
				DocsInMount[IdxSelectedInMount]=doc;
				ImagesCur[IdxSelectedInMount]=bitmapPaste;
			}
			else {//Paste the image as its own unique document.
				try {
					doc=ImageStore.Import(bitmapPaste,GetCurrentCategory(),PatCur);
				}
				catch {
					MessageBox.Show(Lan.g(this,"Error saving document."));
					Cursor=Cursors.Default;
					return;
				}
				FillDocList(false);
				SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum)));
				FormDocInfo formD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(treeDocuments.SelectedNode));
				formD.ShowDialog();
				if(formD.DialogResult!=DialogResult.OK) {
					DeleteSelection(false,false);
				}
				else {
					FillDocList(true);
				}
			}
			InvalidateSettings(ImageSettingFlags.ALL,true);
			Cursor=Cursors.Default;
		}

		private void ToolBarCrop_Click() {
			//remember it's testing after the push has been completed
			if(ToolBarPaint.Buttons["Crop"].Pushed){ //Crop Mode
				ToolBarPaint.Buttons["Hand"].Pushed=false;
				pictureBoxMain.Cursor=Cursors.Default;
			}
			else{
				ToolBarPaint.Buttons["Crop"].Pushed=true;
			}
			IsCropMode=true;
			ToolBarPaint.Invalidate();
		}

		private void ToolBarHand_Click() {
			if(ToolBarPaint.Buttons["Hand"].Pushed){//Hand Mode
				ToolBarPaint.Buttons["Crop"].Pushed=false;
				pictureBoxMain.Cursor=Cursors.Hand;
			}
			else{
				ToolBarPaint.Buttons["Hand"].Pushed=true;
			}
			IsCropMode=false;
			ToolBarPaint.Invalidate();
		}

		private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e){
			//Keep a local pointer to the ImageRenderingNow so that the print results cannot be messed up by the current rendering thread (by changing the ImageRenderingNow).
			if(ImageRenderingNow==null) {
				e.HasMorePages=false;
				return;
			}
			Bitmap bitmapCloned=(Bitmap)ImageRenderingNow.Clone(); 
			if(bitmapCloned.Width<1 || bitmapCloned.Height<1
				|| treeDocuments.SelectedNode==null || treeDocuments.SelectedNode.Tag==null || treeDocuments.SelectedNode.Parent==null)
			{
				bitmapCloned.Dispose();
				bitmapCloned=null;
				e.HasMorePages=false;
				return;
			}
			DataRow obj=(DataRow)treeDocuments.SelectedNode.Tag;
			long mountNum=Convert.ToInt64(obj["MountNum"]);
			long docNum=Convert.ToInt64(obj["DocNum"]);
			Bitmap bitmapPrint=null;
			if(mountNum!=0){//mount object
				if(IdxSelectedInMount>=0 && DocsInMount[IdxSelectedInMount]!=null) {//mount item only
					bitmapPrint=ImageHelper.ApplyDocumentSettingsToImage(DocsInMount[IdxSelectedInMount],ImagesCur[IdxSelectedInMount],ImageSettingFlags.ALL);
				}
				else{//Entire mount. Individual images are already rendered onto mount with correct settings.
					bitmapPrint=bitmapCloned;
				}
			}
			else{//document object.
				//Crop and color function have already been applied to the render image, now do the rest.
				bitmapPrint=ImageHelper.ApplyDocumentSettingsToImage(Documents.GetByNum(docNum),bitmapCloned,ImageSettingFlags.FLIP | ImageSettingFlags.ROTATE);
			}			
			RectangleF rectf=e.MarginBounds;
			float ratio=Math.Min(rectf.Width/(float)bitmapPrint.Width,rectf.Height/(float)bitmapPrint.Height);
			Graphics g=e.Graphics;
			g.InterpolationMode=InterpolationMode.HighQualityBicubic;
			g.CompositingQuality=CompositingQuality.HighQuality;
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.PixelOffsetMode=PixelOffsetMode.HighQuality;
			g.DrawImage(bitmapPrint,0,0,(int)(bitmapPrint.Width*ratio),(int)(bitmapPrint.Height*ratio));
			bitmapCloned.Dispose();
			bitmapCloned=null;
			bitmapPrint.Dispose();
			bitmapPrint=null;
			e.HasMorePages=false;
		}

		private void menuTree_Click(object sender, System.EventArgs e) {
			switch(((MenuItem)sender).Index){
				case 0://print
					ToolBarPrint_Click();
					break;
				case 1://delete
					ToolBarDelete_Click();
					break;
				case 2://info
					ToolBarInfo_Click();
					break;
			}
		}

		private void menuForms_Click(object sender, System.EventArgs e) {
			string formName = ((MenuItem)sender).Text;
			bool copied = true;
			Document doc = null;
			try {
				doc=ImageStore.ImportForm(formName, GetCurrentCategory(),PatCur);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				copied=false;
			}
			if(copied){
				FillDocList(false);
				SelectTreeNode(GetNodeById(MakeIdDoc(doc.DocNum)));
				FormDocInfo FormD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(treeDocuments.SelectedNode));
				FormD.ShowDialog();//some of the fields might get changed, but not the filename
				if(FormD.DialogResult!=DialogResult.OK){
					DeleteSelection(false,false);
				}
				else{
					FillDocList(true);//Refresh possible changes in the document due to FormD.
				}
			}
		}

		private void textNote_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void label1_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void label15_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void sigBox_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void sigBoxTopaz_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void labelInvalidSig_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void panelNote_DoubleClick(object sender,EventArgs e) {
			ToolBarSign_Click();
		}

		private void textNote_MouseHover(object sender,EventArgs e) {
			textNote.Cursor=Cursors.IBeam;
		}

		///<summary>Mouse selections were chosen to be implemented in this particular way, just to make the same code work the same way under both Windows and MONO.</summary>
		private void TreeDocuments_MouseDown(object sender,MouseEventArgs e) {
			TreeNode node=treeDocuments.GetNodeAt(e.Location);
			NodeIdentifierDown=(ImageNodeId)node.Tag;
			//Always select the node on a mouse-down press for either right or left buttons.
			//If the left button is pressed, then the document is either being selected or dragged, so
			//setting the image at the beginning of the drag will either display the image as expected, or
			//automatically display the image while the document is being dragged (since it is in a different thread).
			//If the right button is pressed, then the user wants to view the properties of the image they are
			//clicking on, so displaying the image (in a different thread) will give the user a chance to view
			//the image corresponding to a delete, info display, etc...
			SelectTreeNode(node);
			//Remember that a new selection has begun, so that if the document is being dragged, the appropriate delay time can be used.
			TimeMouseMoved=new DateTime(1,1,1);
		}

		private void TreeDocuments_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			TreeNode node=treeDocuments.GetNodeAt(e.Location);
			if(NodeIdentifierDown.NodeType!=ImageNodeType.None && !NodeIdentifierDown.Equals((ImageNodeId)node.Tag)){//The document is being moved probably.
				treeDocuments.Cursor=Cursors.Hand;
				if(TimeMouseMoved.Year==1) {
					TimeMouseMoved=DateTime.Now;
				}
			}
			else{
				treeDocuments.Cursor=Cursors.Default;
			}
		}

		///<summary>Mouse selections were chosen to be implemented in this particular way, just to make the same code work the same way under both Windows and MONO.</summary>
		private void TreeDocuments_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(NodeIdentifierDown.NodeType==ImageNodeType.None){
				return;
			}
			TreeNode node=treeDocuments.GetNodeAt(e.Location);
			TreeNode sourceNode=GetNodeById(NodeIdentifierDown);
			TimeSpan timeSpanDrag=(TimeSpan)(DateTime.Now-TimeMouseMoved);
			//Dragging a document?
			if(e.Button==MouseButtons.Left//Dragging can only happen with the left mouse button.
				&& GetCurrentFolderName(node)!=GetCurrentFolderName(GetNodeById(NodeIdentifierDown)) //Only necessary if the document in question has changed categories.
				&& timeSpanDrag.Milliseconds>250) //Only takes effect if it happens over a period of time longer than .25 seconds.
			{
				treeDocuments.Cursor=Cursors.Default;
				//Find the destination folder.
				long destinationCategory;
				if(node.Parent!=null) {
					destinationCategory=DefC.Short[(int)DefCat.ImageCats][node.Parent.Index].DefNum;
				}
				else {
					destinationCategory=DefC.Short[(int)DefCat.ImageCats][node.Index].DefNum;
				}
				//Update the object's document category in the database.
				DataRow datarow=(DataRow)sourceNode.Tag;
				long mountNum=Convert.ToInt64(datarow["MountNum"].ToString());
				long docNum=Convert.ToInt64(datarow["DocNum"].ToString());
				ImageNodeId id;
				if(mountNum!=0) {//Mount object.
					Mount mount=Mounts.GetByNum(mountNum);
					mount.DocCategory=destinationCategory;
					Mounts.Update(mount);
					id=MakeIdMount(mount.MountNum);
				}
				else{//Document object.
					Document doc=Documents.GetByNum(docNum);
					doc.DocCategory=destinationCategory;
					Documents.Update(doc);
					id=MakeIdDoc(doc.DocNum);
				}
				FillDocList(true);
				NodeIdentifierDown=id;
			}
		}

		private void TreeDocuments_MouseLeave(object sender,EventArgs e) {
			treeDocuments.Cursor=Cursors.Default;
			NodeIdentifierDown=new ImageNodeId();
		}

		private void TreeDocuments_AfterExpand(object sender,TreeViewEventArgs e) {
			ExpandedCats.Add(((ImageNodeId)e.Node.Tag).PriKey);
		}

		private void TreeDocuments_AfterCollapse(object sender,TreeViewEventArgs e) {
			for(int i=0;i<ExpandedCats.Count;i++) {
				if(ExpandedCats[i]==((ImageNodeId)e.Node.Tag).PriKey) {
					ExpandedCats.RemoveAt(i);
					return;
				}
			}
		}

		///<summary>Invalidates some or all of the image settings.  This will cause those settings to be recalculated, either immediately, or when the current ApplySettings thread is finished.  If supplied settings is ApplySettings.NONE, then that part will be skipped.</summary>
		private void InvalidateSettings(ImageSettingFlags settings,bool reloadZoomTransCrop) {
			bool[] docsToUpdate=new bool[this.ImagesCur.Length];
			if(docsToUpdate.Length==1) {//A document is currently selected.
				docsToUpdate[0]=true;//Always mark the document to be updated.
			} 
			else if(docsToUpdate.Length==4) {//4 bite-wing mount is currently selected.
				if(IdxSelectedInMount>=0) {
					//The current active document will be updated.
					docsToUpdate[IdxSelectedInMount]=true;
				}
			}
			InvalidateSettings(settings,reloadZoomTransCrop,docsToUpdate);
		}		

		///<summary>Invalidates some or all of the image settings.  This will cause those settings to be recalculated, either immediately, or when the current ApplySettings thread is finished.  If supplied settings is ApplySettings.NONE, then that part will be skipped.</summary>
		private void InvalidateSettings(ImageSettingFlags settings,bool reloadZoomTransCrop,bool[] docsToUpdate) {
			if(this.InvokeRequired){
				InvalidatesettingsCallback c=new InvalidatesettingsCallback(InvalidateSettings);
				Invoke(c,new object[] {settings,reloadZoomTransCrop});
				return;
			}
			//Do not allow image rendering when the paint tools are disabled. This will disable the display image when a folder or non-image document is selected, or when no document is currently selected. The ToolBarPaint.Enabled boolean is controlled in SelectTreeNode() and is set to true only if a valid image is currently being displayed.
			if(treeDocuments.SelectedNode==null || 
				treeDocuments.SelectedNode.Tag==null || treeDocuments.SelectedNode.Parent==null){
				EraseCurrentImages();
				return;
			}
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.Doc) {//Only applied to document nodes.
				if(!ToolBarPaint.Enabled){
					EraseCurrentImages();
					return;
				}
				if(reloadZoomTransCrop) {
					//Reloading the image settings only happens when a new image is selected, pasted, scanned, etc...
					//Therefore, the is no need for any current image processing anymore (it would be on a stail image).
					KillThreadImageUpdate();
					ReloadZoomTransCrop(WidthsImagesCur[0],HeightsImagesCur[0],DocSelected,
						new Rectangle(0,0,pictureBoxMain.Width,pictureBoxMain.Height),
						out ZoomImage,out ZoomLevel,out ZoomOverall,out PointTranslation);
					RectCrop=new Rectangle(0,0,-1,-1);
				}	
			}
			ImageSettingFlagsInvalidated|=settings;
			//DocSelected is an individual document instance. Assigning a new document to DocForSettings here does not 
			//negatively effect our image application thread, because the thread either will keep its current 
			//reference to the old document, or will apply the settings with this newly assigned document. In either
			//case, the output is either what we expected originally, or is a more accurate image for more recent 
			//settings. We lock here so that we are sure that the resulting document and setting tuple represent
			//a single point in time.
			lock(EventWaitHandleSettings){//Does not actually lock the EventWaitHandleSettings object, but rather locks the variables in the block.
				IdxDocsFlaggedForUpdate=(bool[])docsToUpdate.Clone();
				DocForSettings=DocSelected.Copy();
				ImageSettingFlagsForSettings=ImageSettingFlagsInvalidated;
				if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.Mount) {		
					MountForSettings=MountSelected.Copy();
				}
				else{//all other types
					MountForSettings=null;					
				}
			}
			//Tell the thread to start processing (as soon as the thread is created, or as soon as otherwise 
			//possible). Set() has no effect if the handle is already signaled.
			EventWaitHandleSettings.Set();
			if(ThreadImageUpdate==null){//Create the thread if it has not been created, or if it was killed for some reason.
				ThreadImageUpdate=new Thread((ThreadStart)(delegate { Worker(); }));
				ThreadImageUpdate.IsBackground=true;
				ThreadImageUpdate.Start();
			}
			ImageSettingFlagsInvalidated=ImageSettingFlags.NONE;
		}

		///<summary>Applies crop and colors. Then, paints ImageRenderingNow onto pictureBoxMain.</summary>
		private void Worker() {
			while(true){
				try{
					//Wait indefinitely for a signal to start processing again. Since the OS handles this function,
					//this thread will not run even a single process cycle until a signal is recieved. This is ideal,
					//since it means that we do not waste any CPU cycles when image processing is not currently needed.
					//At the same time, this function allows us to keep a single thread for as long as possible, so
					//that we do not need to destroy and recreate this thread (except in rare circumstances, such as
					//the deletion of the current image).
					EventWaitHandleSettings.WaitOne(-1,false);
					//The DocForSettings may have been reset several times at this point by calls to InvalidateSettings(), but that cannot hurt
					//us here because it simply means that we are getting even more current information than we had when this thread was
					//signaled to start. We lock here so that we are sure that the resulting document and setting tuple represent
					//a single point in time.
					Document curDocCopy;
					Mount curMountCopy;
					ImageSettingFlags applySettings;
					bool[] docsToUpdate;
					lock(EventWaitHandleSettings){//Does not actually lock the EventWaitHandleSettings object.
						curDocCopy=DocForSettings;
						curMountCopy=MountForSettings;
						applySettings=ImageSettingFlagsForSettings;
						docsToUpdate=IdxDocsFlaggedForUpdate;
					}
					if(MountForSettings==null){//The current selection is a document.
						//Perform cropping and colorfunction here if one of the two flags is set. Rotation, flip, zoom and translation are
						//taken care of in RenderCurrentImage().
						if((applySettings & ImageSettingFlags.COLORFUNCTION)!=ImageSettingFlags.NONE || 
								(applySettings & ImageSettingFlags.CROP)!=ImageSettingFlags.NONE) {
							//Ensure that memory management for the ImageRenderingNow is performed in the worker thread, otherwise the main thread
							//will be slowed when it has to cleanup dozens of old renderImages, which causes a temporary pause in operation.
							if(ImageRenderingNow!=null){
								//Done like this so that the ImageRenderingNow is cleared in a single atomic line of code (in case the thread is
								//killed during this step), so that we don't end up with a pointer to a disposed image in the ImageRenderingNow.
								Bitmap oldRenderImage=ImageRenderingNow;
								ImageRenderingNow=null;
								oldRenderImage.Dispose();
							}
							//currentImages[] is guaranteed to exist and be the current. If currentImages gets updated, this thread 
							//gets aborted with a call to KillMyThread(). The only place currentImages[] is invalid is in a call to 
							//EraseCurrentImage(), but at that point, this thread has been terminated.
							ImageRenderingNow=ImageHelper.ApplyDocumentSettingsToImage(curDocCopy,ImagesCur[IdxSelectedInMount],
								ImageSettingFlags.CROP | ImageSettingFlags.COLORFUNCTION);
						}
						//Make the current ImageRenderingNow visible in the picture box, and perform rotation, flip, zoom, and translation on
						//the ImageRenderingNow.
						RenderCurrentImage(curDocCopy,WidthsImagesCur[IdxSelectedInMount],HeightsImagesCur[IdxSelectedInMount],ZoomImage*ZoomOverall,PointTranslation);
					}
					else{//The current selection is a mount.
						ImageHelper.RenderMountFrames(ImageRenderingNow,MountItemsForSelected,IdxSelectedInMount);
						//Render only the modified image over the old mount image.
						//A null document can happen when a new image frame is selected, but there is no image in that frame.
						if(curDocCopy!=null&&applySettings!=ImageSettingFlags.NONE) {
							for(int i=0;i<docsToUpdate.Length;i++) {
								if(docsToUpdate[i]) {
									ImageHelper.RenderImageIntoMount(ImageRenderingNow,MountItemsForSelected[i],ImagesCur[i],curDocCopy);
								}
							}
						}
						RenderCurrentImage(new Document(),ImageRenderingNow.Width,ImageRenderingNow.Height,ZoomImage*ZoomOverall,PointTranslation);
					}
				}
				catch(ThreadAbortException){
					return;	//Exit as requested. This can happen when the current document is being deleted, 
							//or during shutdown of the program.
				}
				catch(Exception){
					//We don't draw anyting on error (because most of the time it will be due to the current selection state).
				}
			}
		}

		///<summary>Kills the image processing thread if it is currently running.</summary>
		private void KillThreadImageUpdate(){
			xRayImageController.KillXRayThread();//Stop the current xRay image thread if it is running.
			if(ThreadImageUpdate!=null){//Clear any previous image processing.
				if(ThreadImageUpdate.IsAlive){
					ThreadImageUpdate.Abort();//this is not recommended because it results in an exception.  But it seems to work.
					ThreadImageUpdate.Join();//Wait for thread to stop execution.
				}
				ThreadImageUpdate=null;
			}
		}

		///<summary>Handles rendering to the PictureBox of the image in its current state. The image calculations are not performed here, only rendering of the image is performed here, so that we can guarantee a fast display.</summary>
		private void RenderCurrentImage(Document docCopy,int originalWidth,int originalHeight,float zoom,PointF translation) {
			if(!this.Visible){
				return;
			}
			//Helps protect against simultaneous access to the picturebox in both the main and render worker threads.
			if(pictureBoxMain.InvokeRequired){
				RenderImageCallback c=new RenderImageCallback(RenderCurrentImage);
				Invoke(c,new object[] {docCopy,originalWidth,originalHeight,zoom,translation});
				return;
			}
			int width=pictureBoxMain.Bounds.Width;
			int height=pictureBoxMain.Bounds.Height;
			if(width<=0 || height<=0) {
				return;
			}
			Bitmap backBuffer=new Bitmap(width,height);
			Graphics g=Graphics.FromImage(backBuffer);
			try{
				g.Clear(pictureBoxMain.BackColor);
				g.Transform=GetScreenMatrix(docCopy,originalWidth,originalHeight,zoom,translation);
				g.DrawImage(ImageRenderingNow,0,0);
				if(RectCrop.Width>0 && RectCrop.Height>0) {//Must be drawn last so it is on top.
					g.ResetTransform();
					g.DrawRectangle(Pens.Blue,RectCrop);
				}
				g.Dispose();
				//Cleanup old back-buffer.
				if(pictureBoxMain.Image!=null) {
					pictureBoxMain.Image.Dispose();	//Make sure that the calling thread performs the memory cleanup, instead of relying
																				//on the memory-manager in the main thread (otherwise the graphics get spotty sometimes).
				}
				pictureBoxMain.Image=backBuffer;
				pictureBoxMain.Refresh();
			}
			catch(Exception) {
				g.Dispose();
			}
		}

		private void TreeDocuments_MouseDoubleClick(object sender,MouseEventArgs e) {
			TreeNode clickedNode=treeDocuments.GetNodeAt(e.Location);
			if(clickedNode==null || ((ImageNodeId)clickedNode.Tag).NodeType==ImageNodeType.None){
				return;
			}
			DataRow row=(DataRow)clickedNode.Tag;
			long mountNum=PIn.Long(row["MountNum"].ToString());
			long docNum=PIn.Long(row["DocNum"].ToString());
			if(mountNum!=0){//Is this object a mount object?
				FormMountEdit fme=new FormMountEdit(MountSelected);
				fme.ShowDialog();//Edits the MountSelected object directly and updates and changes to the database as well.
				FillDocList(true);//Refresh tree in case description for the mount changed.
				return;
			}
			Document nodeDoc=Documents.GetByNum(docNum);
			string ext=ImageStore.GetExtension(nodeDoc);
			if(ext==".jpg" || ext==".jpeg" || ext==".gif") {
				return;
			}
			//We allow anything which ends with a different extention to be viewed in the windows fax viewer.
			//Specifically, multi-page faxes can be viewed more easily by one of our customers using the fax
			//viewer. On Unix systems, it is imagined that an equivalent viewer will launch to allow the image
			//to be viewed.
			try {
				Process.Start(ImageStore.GetFilePath(nodeDoc,PatFolder));
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void ToolBarInfo_Click() {
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None) {
				return;
			}
			DataRow row=(DataRow)treeDocuments.SelectedNode.Tag;
			long mountNum=PIn.Long(row["MountNum"].ToString());
			long docNum=PIn.Long(row["DocNum"].ToString());
			if(mountNum!=0) {//The current selection is a mount.
				FormMountEdit form=new FormMountEdit(MountSelected);
				form.ShowDialog();//Edits the MountSelected object directly and updates and changes to the database as well.
				FillDocList(true);//Refresh tree in case description for the mount changed.}
			}
			else if(docNum!=0){//A document is currently selected.
				//The FormDocInfo object updates the DocSelected and stores the changes in the database as well.
				FormDocInfo formDocInfo2=new FormDocInfo(PatCur,DocSelected,GetCurrentFolderName(treeDocuments.SelectedNode));
				formDocInfo2.ShowDialog();
				if(formDocInfo2.DialogResult!=DialogResult.OK) {
					return;
				}
				FillDocList(true);
			}
		}

		///<summary>This button is disabled for mounts, in which case this code is never called.</summary>
		private void ToolBarZoomIn_Click() {
			ZoomLevel++;
			PointF c=new PointF(pictureBoxMain.ClientRectangle.Width/2.0f,pictureBoxMain.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-PointTranslation.X,c.Y-PointTranslation.Y);
			PointTranslation=new PointF(PointTranslation.X-p.X,PointTranslation.Y-p.Y);
			ZoomOverall=(float)Math.Pow(2,ZoomLevel);
			InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh display.
		}

		///<summary>This button is disabled for mounts, in which case this code is never called.</summary>
		private void ToolBarZoomOut_Click() {
			ZoomLevel--;
			PointF c=new PointF(pictureBoxMain.ClientRectangle.Width/2.0f,pictureBoxMain.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-PointTranslation.X,c.Y-PointTranslation.Y);
			PointTranslation=new PointF(PointTranslation.X+p.X/2.0f,PointTranslation.Y+p.Y/2.0f);
			ZoomOverall=(float)Math.Pow(2,ZoomLevel);
			InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh display.
		}

		private void DeleteThumbnailImage(Document doc){
			ImageStore.DeleteThumbnailImage(doc,PatFolder);
		}

		private void ToolBarFlip_Click() {
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None || DocSelected==null) {
				return;
			}			
			DocSelected.IsFlipped=!DocSelected.IsFlipped;
			//Since the document is always flipped and then rotated in the mathematical functions below, and since we
			//always want the selected image to rotate left to right no matter what orientation the image is in,
			//we must modify the document settings so that the document will always be flipped left to right, but
			//in such a way that the flipping always happens before the rotation.
			if(DocSelected.DegreesRotated==90||DocSelected.DegreesRotated==270) {
				DocSelected.DegreesRotated*=-1;
				while(DocSelected.DegreesRotated<0) {
					DocSelected.DegreesRotated+=360;
				}
				DocSelected.DegreesRotated=(short)(DocSelected.DegreesRotated%360);
			}
			Documents.Update(DocSelected);
			DeleteThumbnailImage(DocSelected);
			InvalidateSettings(ImageSettingFlags.FLIP,false);//Refresh display.
		}

		private void ToolBarRotateL_Click() {
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None || DocSelected==null) {
				return;
			}
			DocSelected.DegreesRotated-=90;		
			while(DocSelected.DegreesRotated<0) {
				DocSelected.DegreesRotated+=360;
			}
			Documents.Update(DocSelected);
			DeleteThumbnailImage(DocSelected);
			InvalidateSettings(ImageSettingFlags.ROTATE,false);//Refresh display.
		}

		private void ToolBarRotateR_Click(){
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None || DocSelected==null) {
				return;
			}
			DocSelected.DegreesRotated+=90;
			DocSelected.DegreesRotated%=360;
			Documents.Update(DocSelected);
			DeleteThumbnailImage(DocSelected);
			InvalidateSettings(ImageSettingFlags.ROTATE,false);//Refresh display.
		}

		///<summary>Keeps the back buffer for the picture box to be the same in dimensions as the picture box itself.</summary>
		private void PictureBox1_SizeChanged(object sender,EventArgs e) {
			if(this.DesignMode) {
				return;
			}
			InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh display.
		}

		///<summary></summary>
		private void PictureBox1_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			PointMouseDown=new Point(e.X,e.Y);
			IsMouseDown=true;
			PointImageCur=new PointF(PointTranslation.X,PointTranslation.Y);
		}

		private void PictureBox1_MouseHover(object sender,EventArgs e) {
			if(ToolBarPaint.Buttons["Hand"].Pushed) {//Hand mode.
				pictureBoxMain.Cursor=Cursors.Hand;
			}
			else{
				pictureBoxMain.Cursor=Cursors.Default;
			}
		}

		private void PictureBox1_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!IsMouseDown){
				return;
			}
			IsDragging=true;
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None) {
				return;
			}
			if(ToolBarPaint.Buttons["Hand"].Pushed) {//Hand mode.
				PointTranslation=new PointF(PointImageCur.X+(e.Location.X-PointMouseDown.X),PointImageCur.Y+(e.Location.Y-PointMouseDown.Y));
			}
			else if(ToolBarPaint.Buttons["Crop"].Pushed){
				float[] intersect=ODMathLib.IntersectRectangles(Math.Min(e.Location.X,PointMouseDown.X),
					Math.Min(e.Location.Y,PointMouseDown.Y),Math.Abs(e.Location.X-PointMouseDown.X),
					Math.Abs(e.Location.Y-PointMouseDown.Y),pictureBoxMain.ClientRectangle.X,pictureBoxMain.ClientRectangle.Y,
					pictureBoxMain.ClientRectangle.Width-1,pictureBoxMain.ClientRectangle.Height-1);
				if(intersect.Length<0){
					RectCrop=new Rectangle(0,0,-1,-1);
				}
				else{
					RectCrop=new Rectangle((int)intersect[0],(int)intersect[1],(int)intersect[2],(int)intersect[3]);
				}
			}
			InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh display.
		}

		///<summary>Returns the index in the DocsInMount array of the given location (relative to the upper left-hand corner of the pictureBoxMain control) or -1 if the location is outside all documents in the current mount. A mount must be currently selected to call this function.</summary>
		private int GetIdxAtMountLocation(Point location) {
			PointF relativeLocation=new PointF(
				(location.X-PointTranslation.X)/(ZoomImage*ZoomOverall)+MountSelected.Width/2,
				(location.Y-PointTranslation.Y)/(ZoomImage*ZoomOverall)+MountSelected.Height/2);
			//Enumerate the image locations.
			for(int i=0;i<MountItemsForSelected.Count;i++) {
				RectangleF itemLocation=new RectangleF(MountItemsForSelected[i].Xpos,MountItemsForSelected[i].Ypos,
					MountItemsForSelected[i].Width,MountItemsForSelected[i].Height);
				if(itemLocation.Contains(relativeLocation)) {
					return i;
				}
			}
			return -1;//No document selected in the current mount.
		}

		private void PictureBox1_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			bool wasDragging=IsDragging;
			IsMouseDown=false;
			IsDragging=false;
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType==ImageNodeType.None) {
				return;
			}
			if(ToolBarPaint.Buttons["Hand"].Pushed){
				if(e.Button!=MouseButtons.Left || wasDragging) {
					return;
				}
				DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
				long mountNum=Convert.ToInt64(datarow["MountNum"].ToString());
				if(mountNum!=0){//The user may be trying to select an individual image within the current mount.
					IdxSelectedInMount=GetIdxAtMountLocation(PointMouseDown);
					//Assume no item will be selected and enable tools again if an item was actually selected.
					EnableTreeItemTools(true,true,true,true,false,false,false,true,true,true,false,false,false,true);
					for(int j=0;j<MountItemsForSelected.Count;j++) {
						if(MountItemsForSelected[j].OrdinalPos==IdxSelectedInMount) {
							if(DocsInMount[j]!=null) {
								DocSelected=DocsInMount[j];
								SetBrightnessAndContrast();
								EnableTreeItemTools(true,true,false,true,false,true,false,true,true,true,true,true,true,true);
							}
						}
					}
					ToolBarPaint.Invalidate();
					if(IdxSelectedInMount<0) {//The current selection was unselected.
						xRayImageController.KillXRayThread();//Stop xray capture, because it relies on the current selection to place images.
					}
					InvalidateSettings(ImageSettingFlags.ALL,false);
				}
			}
			else{//crop mode
				if(RectCrop.Width<=0 || RectCrop.Height<=0) {
					return;
				}
				if(!MsgBox.Show(this,true,"Crop to Rectangle?")){
					RectCrop=new Rectangle(0,0,-1,-1);
					InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh display (since message box was covering).
					return;
				}
				DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
				long mountNum=Convert.ToInt64(datarow["MountNum"].ToString());
				long docNum=Convert.ToInt64(datarow["DocNum"].ToString());
				float cropZoom=ZoomImage*ZoomOverall;
				PointF cropTrans=PointTranslation;
				PointF cropPoint1=ScreenPointToUnalteredDocumentPoint(RectCrop.Location,DocSelected,
					WidthsImagesCur[IdxSelectedInMount],HeightsImagesCur[IdxSelectedInMount],cropZoom,cropTrans);
				PointF cropPoint2=ScreenPointToUnalteredDocumentPoint(new Point(RectCrop.Location.X+RectCrop.Width,
					RectCrop.Location.Y+RectCrop.Height),DocSelected,WidthsImagesCur[IdxSelectedInMount],HeightsImagesCur[IdxSelectedInMount],
					cropZoom,cropTrans);
				//cropPoint1 and cropPoint2 together define an axis-aligned bounding area, or our crop area. 
				//However, the two points have no guaranteed order, thus we must sort them using Math.Min.
				Rectangle rawCropRect=new Rectangle(
					(int)Math.Round((decimal)Math.Min(cropPoint1.X,cropPoint2.X)),
					(int)Math.Round((decimal)Math.Min(cropPoint1.Y,cropPoint2.Y)),
					(int)Math.Ceiling((decimal)Math.Abs(cropPoint1.X-cropPoint2.X)),
					(int)Math.Ceiling((decimal)Math.Abs(cropPoint1.Y-cropPoint2.Y)));
				//We must also intersect the old cropping rectangle with the new cropping rectangle, so that part of
				//the image does not come back as a result of multiple crops.
				Rectangle oldCropRect=DocCropRect(DocSelected,WidthsImagesCur[IdxSelectedInMount],HeightsImagesCur[IdxSelectedInMount]);
				float[] finalCropRect=ODMathLib.IntersectRectangles(rawCropRect.X,rawCropRect.Y,rawCropRect.Width,
					rawCropRect.Height,oldCropRect.X,oldCropRect.Y,oldCropRect.Width,oldCropRect.Height);
				//Will return a null intersection when the user chooses a crop rectangle which is
				//entirely outside the current visible portion of the image. Can also return a zero-area rect,
				//if the entire image is cropped away.
				if(finalCropRect.Length!=4 || finalCropRect[2]<=0 || finalCropRect[3]<=0){
					RectCrop=new Rectangle(0,0,-1,-1);
					InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh display (since message box was covering).
					return;
				}
				Rectangle prevCropRect=DocCropRect(DocSelected,WidthsImagesCur[IdxSelectedInMount],HeightsImagesCur[IdxSelectedInMount]);
				DocSelected.CropX=(int)finalCropRect[0];
				DocSelected.CropY=(int)finalCropRect[1];
				DocSelected.CropW=(int)Math.Ceiling(finalCropRect[2]);
				DocSelected.CropH=(int)Math.Ceiling(finalCropRect[3]);
				Documents.Update(DocSelected);
				if(docNum!=0){
					DeleteThumbnailImage(DocSelected);
					Rectangle newCropRect=DocCropRect(DocSelected,WidthsImagesCur[IdxSelectedInMount],HeightsImagesCur[IdxSelectedInMount]);
					//Update the location of the image so that the cropped portion of the image does not move in screen space.
					PointF prevCropCenter=new PointF(prevCropRect.X+prevCropRect.Width/2.0f,prevCropRect.Y+prevCropRect.Height/2.0f);
					PointF newCropCenter=new PointF(newCropRect.X+newCropRect.Width/2.0f,newCropRect.Y+newCropRect.Height/2.0f);
					PointF[] imageCropCenters=new PointF[] {
						prevCropCenter,
						newCropCenter
					};
					Matrix docMat=GetDocumentFlippedRotatedMatrix(DocSelected);
					docMat.Scale(cropZoom,cropZoom);
					docMat.TransformPoints(imageCropCenters);
					PointTranslation=new PointF(PointTranslation.X+(imageCropCenters[1].X-imageCropCenters[0].X),
																			PointTranslation.Y+(imageCropCenters[1].Y-imageCropCenters[0].Y));
				}
				RectCrop=new Rectangle(0,0,-1,-1);
				InvalidateSettings(ImageSettingFlags.CROP,false);
			}
		}

		private PointF MountSpaceToScreenSpace(PointF p){
			PointF relvec=new PointF(p.X/MountSelected.Width-0.5f,p.Y/MountSelected.Height-0.5f);
			return new PointF(PointTranslation.X+relvec.X*MountSelected.Width*ZoomImage*ZoomOverall,
				PointTranslation.Y+relvec.Y*MountSelected.Height*ZoomImage*ZoomOverall);
		}

		private void SetBrightnessAndContrast() {
			if(DocSelected.WindowingMax==0) {
				//The document brightness/contrast settings have never been set. By default, we use settings
				//which do not alter the original image.
				sliderBrightnessContrast.MinVal=0;
				sliderBrightnessContrast.MaxVal=255;
			}
			else {
				sliderBrightnessContrast.MinVal=DocSelected.WindowingMin;
				sliderBrightnessContrast.MaxVal=DocSelected.WindowingMax;
			}
		}

		private void brightnessContrastSlider_Scroll(object sender,EventArgs e){
			if(DocSelected==null){
				return;
			}
			DocSelected.WindowingMin=sliderBrightnessContrast.MinVal;
			DocSelected.WindowingMax=sliderBrightnessContrast.MaxVal;
			InvalidateSettings(ImageSettingFlags.COLORFUNCTION,false);
		}

		private void brightnessContrastSlider_ScrollComplete(object sender,EventArgs e) {
			if(DocSelected==null){
				return;
			}
			Documents.Update(DocSelected);
			DeleteThumbnailImage(DocSelected);
			InvalidateSettings(ImageSettingFlags.COLORFUNCTION,false);
		}

		///<summary>Handles a change in selection of the xRay capture button.</summary>
		private void ToolBarCapture_Click() {
			if(ToolBarMain.Buttons["Capture"].Pushed) {
				long mountNum=0;
				if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType!=ImageNodeType.None){//A document or mount is currently selected.
					DataRow datarow=(DataRow)treeDocuments.SelectedNode.Tag;
					mountNum=Convert.ToInt64(datarow["MountNum"].ToString());					
				}
				//ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
				xRayImageController.SensorType=ComputerPrefs.LocalComputer.SensorType;
				xRayImageController.PortNumber=ComputerPrefs.LocalComputer.SensorPort;
				xRayImageController.Binned=ComputerPrefs.LocalComputer.SensorBinned;
				xRayImageController.ExposureLevel=ComputerPrefs.LocalComputer.SensorExposure;
				if(mountNum==0) {//No mount is currently selected.
					//Show the user that they are performing an image capture by generating a new mount.
					Mount mount=new Mount();
					mount.DateCreated=DateTime.Today;
					mount.Description="unnamed capture";
					mount.DocCategory=GetCurrentCategory();
					mount.ImgType=ImageType.Mount;
					mount.PatNum=PatCur.PatNum;
					int border=Math.Max(xRayImageController.SensorSize.Width,xRayImageController.SensorSize.Height)/24;
					mount.Width=4*xRayImageController.SensorSize.Width+5*border;
					mount.Height=xRayImageController.SensorSize.Height+2*border;
					mount.MountNum=Mounts.Insert(mount);
					MountItem mountItem=new MountItem();
					mountItem.MountNum=mount.MountNum;
					mountItem.Width=xRayImageController.SensorSize.Width;
					mountItem.Height=xRayImageController.SensorSize.Height;										
					mountItem.Ypos=border;
					mountItem.OrdinalPos=1;
					mountItem.Xpos=border;
					MountItems.Insert(mountItem);
					mountItem.OrdinalPos=0;
					mountItem.Xpos=mountItem.Width+2*border;
					MountItems.Insert(mountItem);
					mountItem.OrdinalPos=2;
					mountItem.Xpos=2*mountItem.Width+3*border;
					MountItems.Insert(mountItem);
					mountItem.OrdinalPos=3;
					mountItem.Xpos=3*mountItem.Width+4*border;
					MountItems.Insert(mountItem);
					FillDocList(false);
					SelectTreeNode(GetNodeById(MakeIdMount(mount.MountNum)));
					sliderBrightnessContrast.MinVal=PrefC.GetInt(PrefName.ImageWindowingMin);
					sliderBrightnessContrast.MaxVal=PrefC.GetInt(PrefName.ImageWindowingMax);
				}
				else {//A mount is currently selected. We must allow the user to insert new images into partially complete mounts.
					//Clear the visible selection so that the user will know when the device is ready for xray exposure.
					ImageHelper.RenderMountFrames(ImageRenderingNow,MountItemsForSelected,-1);
					RenderCurrentImage(new Document(),ImageRenderingNow.Width,ImageRenderingNow.Height,ZoomImage*ZoomOverall,PointTranslation);
				}
				//Here we can only allow access to the capture button during a capture, because it is too complicated and hard for a 
				//user to follow what is going on if they use the other tools when a capture is taking place.
				EnableAllTools(false);
				ToolBarMain.Buttons["Capture"].Enabled=true;
				ToolBarMain.Invalidate();
				xRayImageController.CaptureXRay();
			}
			else{//The user unselected the image capture button, so cancel the current image capture.
				xRayImageController.KillXRayThread();//Stop current xRay capture and call OnCaptureFinalize() when done.
			}
		}

		///<summary>Called when the image capture device is ready for exposure.</summary>
		private void OnCaptureReady(object sender,EventArgs e) {
			GetNextUnusedMountItem();
			InvalidateSettings(ImageSettingFlags.NONE,false);//Refresh the selection box change (does not do any image processing here).
		}

		///<summary>Called on successful capture of image.</summary>
		private void OnCaptureComplete(object sender,EventArgs e) {
			if(this.InvokeRequired){
				CaptureCallback c=new CaptureCallback(OnCaptureComplete);
				Invoke(c,new object[] {sender,e});
				return;
			}
			if(IdxSelectedInMount<0 || DocsInMount[IdxSelectedInMount]!=null) {//Mount is full.
				xRayImageController.KillXRayThread();
				return;
			}
			//Depending on the device being captured from, we need to rotate the images returned from the device by a certain
			//angle, and we need to place the returned images in a specific order within the mount slots. Later, we will allow
			//the user to define the rotations and slot orders, but for now, they will be hard-coded.
			short rotationAngle=0;
			switch(IdxSelectedInMount){
				case(0):
					rotationAngle=90;
					break;
				case(1):
					rotationAngle=90;
					break;
				case(2):
					rotationAngle=270;
					break;
				default://3
					rotationAngle=270;
					break;
			}
			//Create the document object in the database for this mount image.
			Bitmap capturedImage=xRayImageController.capturedImage;
			Document doc=ImageStore.ImportImageToMount(capturedImage,rotationAngle,MountItemsForSelected[IdxSelectedInMount].MountItemNum, GetCurrentCategory(),PatCur);
			ImagesCur[IdxSelectedInMount]=capturedImage;
			WidthsImagesCur[IdxSelectedInMount]=capturedImage.Width;
			HeightsImagesCur[IdxSelectedInMount]=capturedImage.Height;
			DocsInMount[IdxSelectedInMount]=doc;
			DocSelected=doc;
			SetBrightnessAndContrast();
			//Refresh image in in picture box.
			InvalidateSettings(ImageSettingFlags.ALL,false);
			//This capture was successful. Keep capturing more images until the capture is manually aborted.
			//This will cause calls to OnCaptureBegin(), then OnCaptureFinalize().
			xRayImageController.CaptureXRay();
		}

		///<summary>Called when the entire sequence of image captures is complete (possibly because of failure, or a full mount among other things).</summary>
		private void OnCaptureFinalize(object sender,EventArgs e) {
			if(this.InvokeRequired) {
				CaptureCallback c=new CaptureCallback(OnCaptureFinalize);
				Invoke(c,new object[] { sender,e });
				return;
			}
			ToolBarMain.Buttons["Capture"].Pushed=false;
			ToolBarMain.Invalidate();
			EnableAllTools(true);
			if(IdxSelectedInMount>0 && DocsInMount[IdxSelectedInMount]!=null) {//The capture finished in a state where a mount item is selected.
				EnableTreeItemTools(true,true,false,true,false,true,false,true,true,true,true,true,true,true);
			}
			else{//The capture finished without a mount item selected (so the mount itself is considered to be selected).
				EnableTreeItemTools(true,true,true,true,false,false,false,true,true,true,false,false,false,true);
			}
		}

		private void GetNextUnusedMountItem() {
			//Advance selection box to the location where the next image will capture to.
			if(IdxSelectedInMount<0) {
				IdxSelectedInMount=0;
			}
			int hotStart=IdxSelectedInMount;
			int d=IdxSelectedInMount;
			do{
				if(DocsInMount[IdxSelectedInMount]==null) {
					return;//Found an open frame in the mount.
				}
				IdxSelectedInMount=(IdxSelectedInMount+1)%DocsInMount.Length;
			} 
			while(IdxSelectedInMount!=hotStart);
			IdxSelectedInMount=-1;
		}

		///<summary>Kills ImageApplicationThread.  Disposes of both currentImages and ImageRenderingNow.  Does not actually trigger a refresh of the Picturebox, though.</summary>
		private void EraseCurrentImages(){
			KillThreadImageUpdate();//Stop any current access to the current image and render image so we can dispose them.
			pictureBoxMain.Image=null;
			ImageSettingFlagsInvalidated=ImageSettingFlags.NONE;
			if(ImagesCur!=null){
				for(int i=0;i<ImagesCur.Length;i++){
					if(ImagesCur[i]!=null){
						ImagesCur[i].Dispose();
						ImagesCur[i]=null;
					}	
				}
			}
			if(ImageRenderingNow!=null){
				ImageRenderingNow.Dispose();
				ImageRenderingNow=null;
			}
		}

		///<summary>Handles a change in selection of the xRay capture button.</summary>
		private void ToolBarClose_Click() {
			OnCloseClick();
		}

		///<summary></summary>
		protected void OnCloseClick() {
			EventArgs args=new EventArgs();
			if(CloseClick!=null) {
				CloseClick(this,args);
			}
		}

		#region StaticFunctions
		//Static Functions------------------------------------------------------------------------------------------------------------------------------------------------------
		///<summary>Sets global variables: Zoom, translation, and crop to initial starting values where the image fits perfectly within the box.</summary>
		private static void ReloadZoomTransCrop(int docImageWidth,int docImageHeight,Document doc,Rectangle viewport,
			out float zoom,out int zoomLevel,out float zoomFactor,out PointF translation) {
			//Choose an initial zoom so that the image is scaled to fit the destination box size.
			//Keep in mind that bitmaps are not allowed to have either a width or height of 0,
			//so the following equations will always work. The following subtracts from the 
			//destination box width and height to force a little extra white space.
			RectangleF imageRect=CalcImageDims(docImageWidth,docImageHeight,doc);
			float matchWidth=(int)(viewport.Width*0.975f);
			matchWidth=(matchWidth<=0?1:matchWidth);
			float matchHeight=(int)(viewport.Height*0.975f);
			matchHeight=(matchHeight<=0?1:matchHeight);
			zoom=(float)Math.Min(matchWidth/imageRect.Width,matchHeight/imageRect.Height);
			zoomLevel=0;
			zoomFactor=1;
			translation=new PointF(viewport.Left+viewport.Width/2.0f,viewport.Top+viewport.Height/2.0f);
		}

		///<summary>The screen matrix of the image is relative to the upper left of the image, but our calculations are from the center of the image (since the calculations are easier everywhere else if taken from the center). This function converts our calculation matrix into an equivalent screen matrix for display. Assumes document rotations are in 90 degree multiples.</summary>
		public static Matrix GetScreenMatrix(Document doc,int docOriginalImageWidth,int docOriginalImageHeight,float imageScale,PointF imageTranslation) {			
			Matrix docMat=GetDocumentFlippedRotatedMatrix(doc);
			docMat.Scale(imageScale,imageScale);
			Rectangle cropRect=DocCropRect(doc,docOriginalImageWidth,docOriginalImageHeight);
			//The screen matrix of a GDI image is always relative to the upper left hand corner of the image.
			PointF preOrigin=new PointF(-cropRect.Width/2.0f,-cropRect.Height/2.0f);
			PointF[] screenMatPoints=new PointF[]{
				preOrigin,
				new PointF(preOrigin.X+1	,preOrigin.Y  ),
				new PointF(preOrigin.X		,preOrigin.Y+1),
			};
			docMat.TransformPoints(screenMatPoints);
			Matrix screenMat=new Matrix(	screenMatPoints[1].X-screenMatPoints[0].X,//X.X
																		screenMatPoints[1].Y-screenMatPoints[0].Y,//X.Y
																		screenMatPoints[2].X-screenMatPoints[0].X,//Y.X
																		screenMatPoints[2].Y-screenMatPoints[0].Y,//Y.Y
																		screenMatPoints[0].X+imageTranslation.X,	//Dx
																		screenMatPoints[0].Y+imageTranslation.Y);	//Dy
			return screenMat;
		}

		///<summary>Calculates the image dimensions after factoring flip and rotation of the given document.</summary>
		private static RectangleF CalcImageDims(int imageWidth,int imageHeight,Document doc) {
			Matrix orientation=GetScreenMatrix(doc,imageWidth,imageHeight,1,new PointF(0,0));
			PointF[] corners=new PointF[] {
				new PointF(-imageWidth/2,-imageHeight/2),
				new PointF(imageWidth/2,-imageHeight/2),
				new PointF(-imageWidth/2,imageHeight/2),
				new PointF(imageWidth/2,imageHeight/2),
			};
			orientation.TransformPoints(corners);
			float minx=corners[0].X;
			float maxx=minx;
			float miny=corners[0].Y;
			float maxy=miny;
			for(int i=1;i<corners.Length;i++) {
				if(corners[i].X<minx) {
					minx=corners[i].X;
				}
				else if(corners[i].X>maxx) {
					maxx=corners[i].X;
				}
				if(corners[i].Y<miny) {
					miny=corners[i].Y;
				}
				else if(corners[i].Y>maxy) {
					maxy=corners[i].Y;
				}
			}
			return new RectangleF(0,0,maxx-minx,maxy-miny);
		}

		///<summary>Converts a screen-space location into a location which is relative to the given document in its unrotated/unflipped/unscaled/untranslated state.</summary>
		private static PointF ScreenPointToUnalteredDocumentPoint(PointF screenLocation,Document doc,
			int docOriginalImageWidth,int docOriginalImageHeight,float imageScale,PointF imageTranslation)
		{
			Matrix docMat=GetDocumentFlippedRotatedMatrix(doc);
			docMat.Scale(imageScale,imageScale);
			//Now we have a matrix representing the image in its current state-space.
			float[] docMatAxes=docMat.Elements;
			float px=screenLocation.X-imageTranslation.X;
			float py=screenLocation.Y-imageTranslation.Y;
			//The origin of our internal image axis is always relative to the center of the crop rectangle.
			Rectangle docCropRect=DocCropRect(doc,docOriginalImageWidth,docOriginalImageHeight);
			PointF cropRectCenter=new PointF(docCropRect.X+docCropRect.Width/2.0f,
				docCropRect.Y+docCropRect.Height/2.0f);
			return new PointF(
				(cropRectCenter.X+(px*docMatAxes[0]+py*docMatAxes[1])/(imageScale*imageScale)),
				(cropRectCenter.Y+(px*docMatAxes[2]+py*docMatAxes[3])/(imageScale*imageScale)));
		}

		///<summary>Returns a matrix for the given document which represents flipping over the Y-axis before rotating. Of course, if doc.IsFlipped is false, then no flipping is performed, and if doc.DegreesRotated is a multiple of 360 then no rotation is performed.</summary>
		private static Matrix GetDocumentFlippedRotatedMatrix(Document doc) {
			Matrix result=new Matrix(doc.IsFlipped?-1:1,0,//X-axis
																0,1,//Y-axis
																0,0);//Offset/Translation(dx,dy)
			result.Rotate(doc.IsFlipped?-doc.DegreesRotated:doc.DegreesRotated);
			return result;
		}

		public static Rectangle DocCropRect(Document doc,int originalImageWidth,int originalImageHeight) {
			if(doc.CropW>0 && doc.CropH>0){//Crop rectangles of 0 area are considered non-existant (i.e. no cropping).
				return new Rectangle(doc.CropX,doc.CropY,doc.CropW,doc.CropH);
			}
			return new Rectangle(0,0,originalImageWidth,originalImageHeight);
		}

		#endregion StaticFunctions

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. For use in other modules.</summary>
		public Bitmap CreateMountImage(Mount mount){
			List <MountItem> mountItems=MountItems.GetItemsForMount(mount.MountNum);
			Document[] documents=Documents.GetDocumentsForMountItems(mountItems);
			Bitmap[] originalImages=ImageStore.OpenImages(documents,PatFolder);
			Bitmap mountImage=new Bitmap(mount.Width,mount.Height);
			ImageHelper.RenderMountImage(mountImage,originalImages,mountItems,documents,-1);
			return mountImage;
		}

		private void MountMenu_Opening(object sender,CancelEventArgs e) {
			long mountNum=0;
			if(((ImageNodeId)treeDocuments.SelectedNode.Tag).NodeType!=ImageNodeType.None) {//A document or mount is currently selected.
				DataRow obj=(DataRow)treeDocuments.SelectedNode.Tag;
				mountNum=Convert.ToInt64(obj["MountNum"].ToString());
			}
			if(mountNum==0) {
				e.Cancel=true;
				return;//No mount is currently selected so cancel the menu.
			}
			IdxSelectedInMount=GetIdxAtMountLocation(PointMouseDown);
			if(IdxSelectedInMount<0) {
				e.Cancel=true;
				return;//No mount item was clicked on, so cancel the menu.
			}
			IDataObject clipboard=Clipboard.GetDataObject();
			MountMenu.Items.Clear();
			//Only show the copy option in the mount menu if the item in the mount selected contains an image.
			if(DocsInMount[IdxSelectedInMount]!=null) {
				MountMenu.Items.Add("Copy",null,new System.EventHandler(MountMenuCopy_Click));
			}
			//Only show the paste option in the menu if an item is currently on the clipboard.
			if(clipboard.GetDataPresent(DataFormats.Bitmap)) {
				MountMenu.Items.Add("Paste",null,new System.EventHandler(MountMenuPaste_Click));
			}
			//Only show the swap item in the menu if the item on the clipboard exists in the current mount.
			if(IdxDocToCopy>=0 && DocsInMount[IdxSelectedInMount]!=null && IdxSelectedInMount!=IdxDocToCopy) {
				MountMenu.Items.Add("Swap",null,new System.EventHandler(MountMenuSwap_Click));
			}
			//Cancel the menu if no items have been added into it.
			if(MountMenu.Items.Count<1) {
				e.Cancel=true;
				return;
			}
			//Refresh the mount image, since the IdxSelectedInMount may have changed.
			InvalidateSettings(ImageSettingFlags.ALL,false);
		}

		private void MountMenuCopy_Click(object sender,EventArgs e) {
			ToolBarCopy_Click();
			IdxDocToCopy=IdxSelectedInMount;
		}

		private void MountMenuPaste_Click(object sender,EventArgs e) {
			ToolBarPaste_Click();
		}

		private void MountMenuSwap_Click(object sender,EventArgs e) {
			long mountItemNum=DocsInMount[IdxSelectedInMount].MountItemNum;
			DocsInMount[IdxSelectedInMount].MountItemNum=DocsInMount[IdxDocToCopy].MountItemNum;
			DocsInMount[IdxDocToCopy].MountItemNum=mountItemNum;
			Document doc=DocsInMount[IdxSelectedInMount];
			DocsInMount[IdxSelectedInMount]=DocsInMount[IdxDocToCopy];
			DocsInMount[IdxDocToCopy]=doc;
			MountItem mount=MountItemsForSelected[IdxSelectedInMount];
			MountItemsForSelected[IdxSelectedInMount]=MountItemsForSelected[IdxDocToCopy];
			MountItemsForSelected[IdxDocToCopy]=mount;
			Documents.Update(DocsInMount[IdxSelectedInMount]);
			Documents.Update(DocsInMount[IdxDocToCopy]);
			bool[] idxDocsToUpdate=new bool[DocsInMount.Length];
			idxDocsToUpdate[IdxSelectedInMount]=true;
			idxDocsToUpdate[IdxDocToCopy]=true;
			//Make it so that another swap cannot be done without first copying.
			IdxDocToCopy=-1;
			//Update the mount image to reflect the swapped images.
			InvalidateSettings(ImageSettingFlags.ALL,false,idxDocsToUpdate);
		}

		
		
	}

	///<summary>Because this is a struct, equivalency is based on values, not references.</summary>
	public struct ImageNodeId {
		public ImageNodeType NodeType;
		///<summary>The table to which the primary key refers will differ based on the node type.</summary>
		public long PriKey;

		//could use an == overload here, but don't know syntax right now.
	}

	public enum ImageNodeType {
		///<summary>This is the initial empty id.</summary>
		None,
		///<summary>PriKey is DefNum</summary>
		Category,
		///<summary>PriKey is DocNum</summary>
		Doc,
		///<summary>PriKey is MountNum</summary>
		Mount,
		///<summary>PriKey is EobAttachNum</summary>
		Eob
	}
}
