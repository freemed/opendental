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
//using WIALib;
using OpenDental.UI;
using OpenDentBusiness;
using Tao.OpenGl;
using CodeBase;
using xImageDeviceManager;
using OpenDentBusiness.Imaging;
using System.Text.RegularExpressions;
using OpenDental.Imaging;

namespace OpenDental{

	///<summary></summary>
	public class ContrDocs : System.Windows.Forms.UserControl {

		#region Designer Generated Variables

		private System.Windows.Forms.ImageList imageListTree;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageListTools2;
		private System.Windows.Forms.PrintDialog PrintDialog1;
		private System.Drawing.Printing.PrintDocument PrintDocument2;
		private System.Windows.Forms.TreeView TreeDocuments;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuPrefs;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ContextMenu contextTree;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.ContextMenu menuPatient;
		private Panel panelNote;
		private Label label1;
		private TextBox textNote;
		private SignatureBox sigBox;
		private Label label15;
		private Label labelInvalidSig;
		private ContrWindowingSlider brightnessContrastSlider;
		private ODToolBar paintTools;
		private Panel paintToolsUnderline;
		private Panel panel1;
		private Panel panelDrTech;
		private OpenDental.UI.Button buttonClipboard;
		private OpenDental.UI.Button butCBSheet;
		private OpenDental.UI.Button butCameraPic;
		private OpenDental.UI.Button butOpenFolder;
		private System.Windows.Forms.PictureBox PictureBox1;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		// declarations
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")]
		public static extern int TWAIN_AcquireToFilename(IntPtr hwndApp, string bmpFileName); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_SelectImageSource(IntPtr hwndApp); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_AcquireToClipboard(IntPtr hwndApp, long wPixTypes); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_IsAvailable(); 
		///<summary></summary>
		[System.Runtime.InteropServices.DllImport("EZTW32.DLL")] 
		public static extern int TWAIN_EasyVersion();// spk 10/05/04
		private ContextMenu menuForms;

		#endregion

		#region Manually Created Variables

		///<summary>Used to display Topaz signatures on Windows. Is added dynamically to avoid native code references crashing MONO.</summary>
		private Control sigBoxTopaz;
		///<summary>Starts out as false. It's only used when repainting the toolbar, not to test mode.</summary>
		private bool IsCropMode;
		private Family FamCur;
		///<summary>When dragging on Picturebox, this is the starting point in PictureBox coordinates.</summary>
		private Point MouseDownOrigin;
		private bool MouseIsDown;
		///<summary>Set to true when the image in the picture box is currently being translated.</summary>
		private bool dragging=false;
		///<summary>In-memory copies of the images being viewed/edited. No changes are made to these images in memory, they are just kept resident to avoid having to reload the images from disk each time the screen needs to be redrawn.</summary>
		Bitmap[] currentImages=new Bitmap[1];
		///<summary>Used as a basis for calculating image translations.</summary>
		PointF imageLocation;
		///<summary>The true offset of the document image or mount image.</summary>
		PointF imageTranslation;
		///<summary>The current zoom of the currently loaded image/mount. 1 implies normal size, <1 implies the image is shrunk, >1 imples the image/mount is blown-up.</summary>
		float imageZoom=1;
		///<summary>The zoom level is 0 after the current image/mount is loaded. The image is zoomed a factor of (initial image/mount zoom)*(2^zoomLevel).</summary>
		int zoomLevel=0;
		///<summary>Represents the current factor for level of zoom from the initial zoom of the currently loaded image/mount. This is calculated directly as 2^zoomLevel every time a zoom occurs. Recalculated from zoomLevel each time, so that zoomFactor always hits the exact same values for the exact same zoom levels (no loss of data).</summary>
		float zoomFactor=1;
		///<summary>Used to prevent concurrent access to the current images by multiple threads.</summary>
		int[] curImageWidths=new int[1];
		///<summary>Used to prevent concurrent access to the current images by multiple threads.</summary>
		int[] curImageHeights=new int[1];
		///<summary>The image currently on the screen.</summary>
		Bitmap renderImage=null;
		Rectangle cropTangle=new Rectangle(0,0,-1,-1);
		///<summary>Used for performing an xRay image capture on an imaging device.</summary>
		SuniDeviceControl xRayImageController=null;
		///<summary>Thread to handle updating the graphical image to the screen when the current document is an image.</summary>
		Thread myThread=null;
		ApplySettings InvalidatedSettingsFlag;
		///<summary>Used as a thread-safe communication device between the main and worker threads.</summary>
		EventWaitHandle settingHandle=new EventWaitHandle(false,EventResetMode.AutoReset);
		///<summary>Edited by the main thread to reflect selection changes. Read by worker thread.</summary>
		Document settingDoc=null;
		///<summary>Keeps track of the mount settings for the currently selected mount document.</summary>
		Mount settingMount=new Mount();
		///<summary>Set by the main thread and read by the image worker thread. Specifies which image processing tasks are to be performed by the worker thread.</summary>
		ApplySettings settingFlags=ApplySettings.NONE;
		///<summary>Used to perform mouse selections in the TreeDocuments list.</summary>
		string treeIdNumDown="";
		///<summary>Used to keep track of the old document selection by document number (the only guaranteed unique idenifier). This is to help the code be compatible with both Windows and MONO.</summary>
		string oldSelectionIdentifier="";
		///<summary>Used for Invoke() calls in RenderCurrentImage() to safely handle multi-thread access to the picture box.</summary>
		delegate void RenderImageCallback(Document docCopy,int originalWidth,int originalHeight,float zoom,PointF translation);
		///<summary>Used to safe-guard against multi-threading issues when an image capture is completed.</summary>
		delegate void CaptureCallback(object sender,EventArgs e);
		///<summary>Used to protect against multi-threading issues when refreshing a mount during an image capture.</summary>
		delegate void InvalidatesettingsCallback(ApplySettings settings,bool reloadZoomTransCrop);
		///<summary>Keeps track of the document settings for the currently selected document or mount.</summary>
		Document selectionDoc=new Document();
		///<summary>Keeps track of the currently selected mount object (only when a mount is selected).</summary>
		Mount selectionMount=new Mount();
		///<summary>The mount items which correspond to the current selectionMount.</summary>
		MountItem[] selectionMountItems=null;
		///<summary>Keeps track of the currently selected image within the list of currently loaded images.</summary>
		int hotDocument=0;
		///<summary>List of documents currently loaded into the currently selected mount (if any).</summary>
		Document[] mountDocs=null;

		public IImageStore imageStore;

		///<summary>The only reason this is public is for NewPatientForm.com functionality.</summary>
		public Patient PatCur { get { return imageStore == null ? null : imageStore.Patient; } }
		#endregion

		///<summary></summary>
		public ContrDocs(){
            Logger.openlog.Log("Initializing Document Module...", Logger.Severity.INFO);
			InitializeComponent();
			//The context menu causes strange bugs in MONO when performing selections on the tree.
			//Perhaps when MONO is more developed we can remove this check.
            //Also, the SigPlusNet() object cannot be instantiated on 64-bit machines, because
            //the code for instantiation exists in a 32-bit native dll. Therefore, we have put
            //the creation code for the topaz box in CodeBase.TopazWrapper.GetTopaz() so that
            //the native code does not exist or get called anywhere in the program unless we are running on a 
            //32-bit version of Windows.
			if(Environment.OSVersion.Platform==PlatformID.Unix || CodeBase.ODEnvironment.Is64BitOperatingSystem()){
				TreeDocuments.ContextMenu=null;
			}else{//Windows OS
                sigBoxTopaz=CodeBase.TopazWrapper.GetTopaz();
				panelNote.Controls.Add(sigBoxTopaz);
				sigBoxTopaz.Location=new System.Drawing.Point(437,15);
				sigBoxTopaz.Name="sigBoxTopaz";
				sigBoxTopaz.Size=new System.Drawing.Size(394,91);
				sigBoxTopaz.TabIndex=93;
				sigBoxTopaz.Text="sigPlusNET1";
				sigBoxTopaz.DoubleClick+=new System.EventHandler(this.sigBoxTopaz_DoubleClick);
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContrDocs));
			this.TreeDocuments = new System.Windows.Forms.TreeView();
			this.contextTree = new System.Windows.Forms.ContextMenu();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
			this.PrintDocument2 = new System.Drawing.Printing.PrintDocument();
			this.imageListTools2 = new System.Windows.Forms.ImageList(this.components);
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuPrefs = new System.Windows.Forms.MenuItem();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.panelNote = new System.Windows.Forms.Panel();
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.paintToolsUnderline = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelDrTech = new System.Windows.Forms.Panel();
			this.buttonClipboard = new OpenDental.UI.Button();
			this.butCBSheet = new OpenDental.UI.Button();
			this.butCameraPic = new OpenDental.UI.Button();
			this.butOpenFolder = new OpenDental.UI.Button();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.paintTools = new OpenDental.UI.ODToolBar();
			this.brightnessContrastSlider = new OpenDental.UI.ContrWindowingSlider();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			this.panelNote.SuspendLayout();
			this.panelDrTech.SuspendLayout();
			this.SuspendLayout();
			// 
			// TreeDocuments
			// 
			this.TreeDocuments.ContextMenu = this.contextTree;
			this.TreeDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TreeDocuments.FullRowSelect = true;
			this.TreeDocuments.HideSelection = false;
			this.TreeDocuments.ImageIndex = 2;
			this.TreeDocuments.ImageList = this.imageListTree;
			this.TreeDocuments.Indent = 20;
			this.TreeDocuments.Location = new System.Drawing.Point(0, 33);
			this.TreeDocuments.Name = "TreeDocuments";
			this.TreeDocuments.SelectedImageIndex = 2;
			this.TreeDocuments.Size = new System.Drawing.Size(228, 519);
			this.TreeDocuments.TabIndex = 0;
			this.TreeDocuments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseDoubleClick);
			this.TreeDocuments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseUp);
			this.TreeDocuments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseMove);
			this.TreeDocuments.MouseLeave += new System.EventHandler(this.TreeDocuments_MouseLeave);
			this.TreeDocuments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeDocuments_MouseDown);
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
			this.imageListTree.Images.SetKeyName(0, "");
			this.imageListTree.Images.SetKeyName(1, "");
			this.imageListTree.Images.SetKeyName(2, "");
			this.imageListTree.Images.SetKeyName(3, "");
			this.imageListTree.Images.SetKeyName(4, "");
			this.imageListTree.Images.SetKeyName(5, "");
			this.imageListTree.Images.SetKeyName(6, "");
			// 
			// PrintDialog1
			// 
			this.PrintDialog1.AllowPrintToFile = false;
			this.PrintDialog1.Document = this.PrintDocument2;
			// 
			// PrintDocument2
			// 
			this.PrintDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
			// 
			// imageListTools2
			// 
			this.imageListTools2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTools2.ImageStream")));
			this.imageListTools2.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTools2.Images.SetKeyName(0, "Pat.gif");
			this.imageListTools2.Images.SetKeyName(1, "print.gif");
			this.imageListTools2.Images.SetKeyName(2, "deleteX.gif");
			this.imageListTools2.Images.SetKeyName(3, "info.gif");
			this.imageListTools2.Images.SetKeyName(4, "scan.gif");
			this.imageListTools2.Images.SetKeyName(5, "");
			this.imageListTools2.Images.SetKeyName(6, "");
			this.imageListTools2.Images.SetKeyName(7, "");
			this.imageListTools2.Images.SetKeyName(8, "");
			this.imageListTools2.Images.SetKeyName(9, "");
			this.imageListTools2.Images.SetKeyName(10, "");
			this.imageListTools2.Images.SetKeyName(11, "");
			this.imageListTools2.Images.SetKeyName(12, "");
			this.imageListTools2.Images.SetKeyName(13, "");
			this.imageListTools2.Images.SetKeyName(14, "");
			this.imageListTools2.Images.SetKeyName(15, "");
			this.imageListTools2.Images.SetKeyName(16, "");
			this.imageListTools2.Images.SetKeyName(17, "copy.gif");
			// 
			// PictureBox1
			// 
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBox1.InitialImage = null;
			this.PictureBox1.Location = new System.Drawing.Point(233, 63);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(703, 370);
			this.PictureBox1.TabIndex = 6;
			this.PictureBox1.TabStop = false;
			this.PictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			this.PictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.PictureBox1.MouseHover += new System.EventHandler(this.PictureBox1_MouseHover);
			this.PictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
			this.PictureBox1.SizeChanged += new System.EventHandler(this.PictureBox1_SizeChanged);
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
			this.panelNote.Location = new System.Drawing.Point(234, 489);
			this.panelNote.Name = "panelNote";
			this.panelNote.Size = new System.Drawing.Size(705, 64);
			this.panelNote.TabIndex = 11;
			this.panelNote.Visible = false;
			this.panelNote.DoubleClick += new System.EventHandler(this.panelNote_DoubleClick);
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(414, 35);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196, 59);
			this.labelInvalidSig.TabIndex = 94;
			this.labelInvalidSig.Text = "Invalid Signature -  Document or note has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInvalidSig.DoubleClick += new System.EventHandler(this.labelInvalidSig_DoubleClick);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(308, 20);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(394, 91);
			this.sigBox.TabIndex = 90;
			this.sigBox.DoubleClick += new System.EventHandler(this.sigBox_DoubleClick);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(305, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(63, 18);
			this.label15.TabIndex = 87;
			this.label15.Text = "Signature";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label15.DoubleClick += new System.EventHandler(this.label15_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
			// 
			// textNote
			// 
			this.textNote.BackColor = System.Drawing.SystemColors.Window;
			this.textNote.Location = new System.Drawing.Point(0, 20);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(302, 91);
			this.textNote.TabIndex = 0;
			this.textNote.DoubleClick += new System.EventHandler(this.textNote_DoubleClick);
			this.textNote.MouseHover += new System.EventHandler(this.textNote_MouseHover);
			// 
			// paintToolsUnderline
			// 
			this.paintToolsUnderline.BackColor = System.Drawing.SystemColors.ControlDark;
			this.paintToolsUnderline.Location = new System.Drawing.Point(236, 56);
			this.paintToolsUnderline.Name = "paintToolsUnderline";
			this.paintToolsUnderline.Size = new System.Drawing.Size(702, 2);
			this.paintToolsUnderline.TabIndex = 15;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Location = new System.Drawing.Point(233, 29);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(2, 29);
			this.panel1.TabIndex = 16;
			// 
			// panelDrTech
			// 
			this.panelDrTech.Controls.Add(this.buttonClipboard);
			this.panelDrTech.Controls.Add(this.butCBSheet);
			this.panelDrTech.Controls.Add(this.butCameraPic);
			this.panelDrTech.Controls.Add(this.butOpenFolder);
			this.panelDrTech.Location = new System.Drawing.Point(863, 0);
			this.panelDrTech.Name = "panelDrTech";
			this.panelDrTech.Size = new System.Drawing.Size(74, 78);
			this.panelDrTech.TabIndex = 17;
			this.panelDrTech.Visible = false;
			// 
			// buttonClipboard
			// 
			this.buttonClipboard.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonClipboard.Autosize = false;
			this.buttonClipboard.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonClipboard.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonClipboard.CornerRadius = 4F;
			this.buttonClipboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonClipboard.Location = new System.Drawing.Point(0, 58);
			this.buttonClipboard.Name = "buttonClipboard";
			this.buttonClipboard.Size = new System.Drawing.Size(72, 20);
			this.buttonClipboard.TabIndex = 85;
			this.buttonClipboard.Text = "Folder to Clipbrd";
			this.buttonClipboard.Click += new System.EventHandler(this.buttonClipboard_Click);
			// 
			// butCBSheet
			// 
			this.butCBSheet.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCBSheet.Autosize = false;
			this.butCBSheet.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCBSheet.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCBSheet.CornerRadius = 4F;
			this.butCBSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butCBSheet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCBSheet.Location = new System.Drawing.Point(0, 39);
			this.butCBSheet.Name = "butCBSheet";
			this.butCBSheet.Size = new System.Drawing.Size(72, 20);
			this.butCBSheet.TabIndex = 84;
			this.butCBSheet.Text = "New C&&B Sheet";
			this.butCBSheet.Click += new System.EventHandler(this.butCBSheet_Click);
			// 
			// butCameraPic
			// 
			this.butCameraPic.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCameraPic.Autosize = true;
			this.butCameraPic.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCameraPic.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCameraPic.CornerRadius = 4F;
			this.butCameraPic.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butCameraPic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCameraPic.Location = new System.Drawing.Point(0, 20);
			this.butCameraPic.Name = "butCameraPic";
			this.butCameraPic.Size = new System.Drawing.Size(72, 20);
			this.butCameraPic.TabIndex = 83;
			this.butCameraPic.Text = "Camera Pics";
			this.butCameraPic.Click += new System.EventHandler(this.butCameraPic_Click);
			// 
			// butOpenFolder
			// 
			this.butOpenFolder.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOpenFolder.Autosize = false;
			this.butOpenFolder.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOpenFolder.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOpenFolder.CornerRadius = 4F;
			this.butOpenFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butOpenFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butOpenFolder.Location = new System.Drawing.Point(0, 1);
			this.butOpenFolder.Name = "butOpenFolder";
			this.butOpenFolder.Size = new System.Drawing.Size(72, 20);
			this.butOpenFolder.TabIndex = 82;
			this.butOpenFolder.Text = "Pt Folder/E-Mail";
			this.butOpenFolder.Click += new System.EventHandler(this.butOpenFolder_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools2;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 10;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// paintTools
			// 
			this.paintTools.ImageList = this.imageListTools2;
			this.paintTools.Location = new System.Drawing.Point(440, 28);
			this.paintTools.Name = "paintTools";
			this.paintTools.Size = new System.Drawing.Size(499, 29);
			this.paintTools.TabIndex = 14;
			this.paintTools.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.paintTools_ButtonClick);
			// 
			// brightnessContrastSlider
			// 
			this.brightnessContrastSlider.Enabled = false;
			this.brightnessContrastSlider.Location = new System.Drawing.Point(240, 36);
			this.brightnessContrastSlider.MaxVal = 255;
			this.brightnessContrastSlider.MinVal = 0;
			this.brightnessContrastSlider.Name = "brightnessContrastSlider";
			this.brightnessContrastSlider.Size = new System.Drawing.Size(194, 14);
			this.brightnessContrastSlider.TabIndex = 12;
			this.brightnessContrastSlider.Text = "contrWindowingSlider1";
			this.brightnessContrastSlider.Scroll += new System.EventHandler(this.brightnessContrastSlider_Scroll);
			this.brightnessContrastSlider.ScrollComplete += new System.EventHandler(this.brightnessContrastSlider_ScrollComplete);
			// 
			// ContrDocs
			// 
			this.Controls.Add(this.panelDrTech);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.paintToolsUnderline);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.paintTools);
			this.Controls.Add(this.brightnessContrastSlider);
			this.Controls.Add(this.panelNote);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.TreeDocuments);
			this.Name = "ContrDocs";
			this.Size = new System.Drawing.Size(939, 585);
			this.Resize += new System.EventHandler(this.ContrDocs_Resize);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			this.panelNote.ResumeLayout(false);
			this.panelNote.PerformLayout();
			this.panelDrTech.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrDocs_Resize(object sender,EventArgs e) {
			ResizeAll();
		}

		///<summary>Resizes all controls in the image module to fit inside the current window, including controls which have varying visibility.</summary>
		private void ResizeAll(){
			TreeDocuments.Height=Height-TreeDocuments.Location.Y-2;
			PictureBox1.Width=Width-PictureBox1.Location.X-4;
			panelNote.Width=PictureBox1.Width;
			panelNote.Height=(int)Math.Min(114,Height-PictureBox1.Location.Y);
			int panelNoteHeight=(panelNote.Visible?panelNote.Height:0);
			PictureBox1.Height=Height-panelNoteHeight-PictureBox1.Location.Y;
			panelNote.Location=new Point(PictureBox1.Left,Height-panelNoteHeight-1);
			paintTools.Location=new Point(brightnessContrastSlider.Location.X+brightnessContrastSlider.Width+4,
				paintTools.Location.Y);
			paintTools.Size=new Size(PictureBox1.Width-brightnessContrastSlider.Width-4,paintTools.Height);
			paintToolsUnderline.Location=new Point(PictureBox1.Location.X,paintToolsUnderline.Location.Y);
			paintToolsUnderline.Width=Width-paintToolsUnderline.Location.X;
		}

		///<summary>Sets the panelnote visibility based on the given document's signature data and the current operating system.</summary>
		private void SetPanelNoteVisibility(Document doc) {
			panelNote.Visible=(doc!=null) && (((doc.Note!=null && doc.Note!="") || (doc.Signature!=null && doc.Signature!="")) && 
				(Environment.OSVersion.Platform!=PlatformID.Unix || !doc.SigIsTopaz));
		}

		///<summary></summary>
		public void InitializeOnStartup(){
			MouseDownOrigin=new Point();
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
			paintTools.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,Lan.g(this,"Print"),"Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,Lan.g(this,"Delete"),"Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",3,Lan.g(this,"Item Info"),"Info"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Sign"),-1,Lan.g(this,"Sign this document"),"Sign"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Scan:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",14,Lan.g(this,"Scan Document"),"ScanDoc"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",16,Lan.g(this,"Scan Radiograph"),"ScanXRay"));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",15,Lan.g(this,"Scan Photo"),"ScanPhoto"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Import"),5,Lan.g(this,"Import From File"),"Import"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),17,Lan.g(this,"Copy displayed image to clipboard"),"Copy"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),6,Lan.g(this,"Paste From Clipboard"),"Paste"));
			button=new ODToolBarButton(Lan.g(this,"Forms"),-1,"","Forms");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			menuForms=new ContextMenu();
			string formDir=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"Forms");
			if(Directory.Exists(formDir)) {
				DirectoryInfo dirInfo=new DirectoryInfo(formDir);
				FileInfo[] fileInfos=dirInfo.GetFiles();
				for(int i=0;i<fileInfos.Length;i++){
					if(Documents.IsAcceptableFileName(fileInfos[i].FullName)){
						menuForms.MenuItems.Add(fileInfos[i].Name,new System.EventHandler(menuForms_Click));
					}
				}
			}
			button.DropDownMenu=menuForms;
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton(Lan.g(this,"Capture"),-1,"Capture Image From Device","Capture");
			button.Style=ODToolBarButtonStyle.ToggleButton;			
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton("",7,Lan.g(this,"Crop Tool"),"Crop");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			button.Pushed=IsCropMode;
			paintTools.Buttons.Add(button);
			button=new ODToolBarButton("",10,Lan.g(this,"Hand Tool"),"Hand");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			button.Pushed=!IsCropMode;
			paintTools.Buttons.Add(button);
			paintTools.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			paintTools.Buttons.Add(new ODToolBarButton("",8,Lan.g(this,"Zoom In"),"ZoomIn"));
			paintTools.Buttons.Add(new ODToolBarButton("",9,Lan.g(this,"Zoom Out"),"ZoomOut"));
			paintTools.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			button=new ODToolBarButton(Lan.g(this,"Rotate:"),-1,"","");
			button.Style=ODToolBarButtonStyle.Label;
			paintTools.Buttons.Add(button);
			paintTools.Buttons.Add(new ODToolBarButton("",11,Lan.g(this,"Flip"),"Flip"));
			paintTools.Buttons.Add(new ODToolBarButton("",12,Lan.g(this,"Rotate Left"),"RotateL"));
			paintTools.Buttons.Add(new ODToolBarButton("",13,Lan.g(this,"Rotate Right"),"RotateR"));

			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.ImagesModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
			paintTools.Invalidate();
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			if(!PrefB.UsingAtoZfolder) {
				MsgBox.Show(this,"Not currently using documents. Turn on the A to Z folders option by going to Setup | Data Paths to enable imaging.");
				this.Enabled=false;
				return;
			}
			this.Enabled=true;
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			imageStore=null;
			//Cancel current image capture.
			xRayImageController.KillXRayThread();
		}

		///<summary>This is public for NewPatientForm functionality.</summary>
  		public void RefreshModuleData(int patNum){
			SelectTreeNode(null);//Clear selection and image and reset visibilities.
			if(patNum==0){
				imageStore=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			if(ImageStore.UpdatePatient == null)
				ImageStore.UpdatePatient = new FileStore.UpdatePatientDelegate(Patients.Update);
			imageStore = ImageStore.GetImageStore(FamCur.GetPatient(patNum));

			if(ParentForm!=null){
				//Added so NewPatientform can have access without showing
				ParentForm.Text=Patients.GetMainTitle(PatCur);
			}
		}

		private void RefreshModuleScreen(){
			ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(this.Enabled && PatCur!=null){
				//Enable tools which must always be accessible when a valid patient is selected.
				EnableAllTools(true);
				//Item specific tools disabled until item chosen.
				EnableAllTreeItemTools(false);
			}else{
				EnableAllTools(false);//Disable entire menu (besides select patient).
			}
			if (((Pref)PrefB.HList["FuchsOptionsOn"]).ValueString == "1") {
				panelDrTech.Visible = true;
			}

			FillPatientButton();
			ToolBarMain.Invalidate();
			paintTools.Invalidate();
			FillDocList(false);
		}

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		///<summary>Applies to all tools and excludes patient selection button.</summary>
		private void EnableAllTools(bool enable) {
			for(int i=0;i<ToolBarMain.Buttons.Count;i++){
				ToolBarMain.Buttons[i].Enabled=enable;			
			}
			ToolBarMain.Buttons["Patient"].Enabled=true;
			ToolBarMain.Buttons["Capture"].Enabled=(ToolBarMain.Buttons["Capture"].Enabled &&
				Environment.OSVersion.Platform!=PlatformID.Unix);
			ToolBarMain.Invalidate();
			for(int i=0;i<paintTools.Buttons.Count;i++){
				paintTools.Buttons[i].Enabled=enable;
			}
			paintTools.Enabled=enable;
			paintTools.Invalidate();
			brightnessContrastSlider.Enabled=enable;
			brightnessContrastSlider.Invalidate();
		}

		///<summary>Defined this way to force future programming to consider which tools are enabled and disabled for every possible tool in the menu.</summary>
		private void EnableTreeItemTools(bool print,bool delete,bool info,bool copy,bool sign,bool brightAndContrast,bool crop,bool hand,bool zoomIn,bool zoomOut,bool flip,bool rotateL,bool rotateR){
			ToolBarMain.Buttons["Print"].Enabled=print;
			ToolBarMain.Buttons["Delete"].Enabled=delete;
			ToolBarMain.Buttons["Info"].Enabled=info;
			ToolBarMain.Buttons["Copy"].Enabled=copy;
			ToolBarMain.Buttons["Sign"].Enabled=sign;
			ToolBarMain.Invalidate();
			paintTools.Buttons["Crop"].Enabled=crop;
			paintTools.Buttons["Hand"].Enabled=hand;
			paintTools.Buttons["ZoomIn"].Enabled=zoomIn;
			paintTools.Buttons["ZoomOut"].Enabled=zoomOut;
			paintTools.Buttons["Flip"].Enabled=flip;
			paintTools.Buttons["RotateR"].Enabled=rotateR;
			paintTools.Buttons["RotateL"].Enabled=rotateL;
			//Enabled if one tool inside is enabled.
			paintTools.Enabled=(brightAndContrast||crop||hand||zoomIn||zoomOut||flip||rotateL||rotateR);
			paintTools.Invalidate();
			brightnessContrastSlider.Enabled=brightAndContrast;
			brightnessContrastSlider.Invalidate();
		}

		private void EnableAllTreeItemTools(bool enable){
			EnableTreeItemTools(enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable,enable);
		}

		///<summary>Selection doesn't only happen by the tree and mouse clicks, but can also happen by automatic processes, such as image import, image paste, etc...</summary>
		private void SelectTreeNode(TreeNode node){
			//Select the node always, but perform additional tasks when necessary (i.e. load an image, or mount).
			TreeDocuments.SelectedNode=node;
			TreeDocuments.Invalidate();
			//We only perform a load if the new selection is different than the old selection.
			string identifier=GetNodeIdentifier(node);
			if(identifier==oldSelectionIdentifier){
				return;
			}
			selectionDoc=new Document();
			oldSelectionIdentifier=identifier;
			//Disable all item tools until the currently selected node is loaded properly in the picture box.
			EnableAllTreeItemTools(false);
			paintTools.Buttons["Hand"].Pushed=true;
			paintTools.Buttons["Crop"].Pushed=false;
			//Stop any current image processing. This will avoid having the renderImage set to a valid image after
			//the current image has been erased. This will also avoid concurrent access to the the currently loaded images by
			//the main and worker threads.
			EraseCurrentImages();
			//selectionDoc=null;
			//selectionMount=null;
			if(identifier.Length<1){//A folder was selected (or unselection, but I am not sure unselection would be possible here).
				//The panel note control is made invisible to start and then made visible for the appropriate documents. This
				//line prevents the possibility of showing a signature box after selecting a folder node.
				panelNote.Visible=false;
				//Make sure the controls are sized properly in the image module since the visibility of the panel note might
				//have just changed.
				ResizeAll();
			}else{//A node that requires processing was selected.
				DataRow obj=(DataRow)node.Tag;
				int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
				int docNum=Convert.ToInt32(obj["DocNum"].ToString());
				if(mountNum!=0){//This is a mount node.
					//Creates a complete initial mount image. No need to call invalidate until changes are made to the mount later.
					selectionMountItems=MountItems.GetItemsForMount(mountNum);
					mountDocs=Documents.GetDocumentsForMountItems(selectionMountItems);
					hotDocument=-1;//No selection to start.
					currentImages=imageStore.RetrieveImage(mountDocs);
					selectionMount=Mounts.GetByNum(mountNum);
					renderImage=new Bitmap(selectionMount.Width,selectionMount.Height);
					RenderMountImage(renderImage,currentImages,selectionMountItems,mountDocs,hotDocument);
					EnableTreeItemTools(true,true,true,true,false,false,false,true,true,true,false,false,false);
				}else{//This is a document node.
					//Reload the doc from the db. We don't just keep reusing the tree data, because it will become more and 
					//more stale with age if the program is left open in the image module for long periods of time.
					selectionDoc=Documents.GetByNum(docNum);
					hotDocument=0;
					currentImages=imageStore.RetrieveImage(new Document[] {selectionDoc});
					SetBrightnessAndContrast();
					EnableAllTools(true);
				}
				curImageWidths=new int[currentImages.Length];
				curImageHeights=new int[currentImages.Length];
				for(int i=0;i<currentImages.Length;i++) {
					if(currentImages[i]!=null){
						curImageWidths[i]=currentImages[i].Width;
						curImageHeights[i]=currentImages[i].Height;
					}
				}
				//Adjust visibility of panel note control based on if the new document has a signature.
				SetPanelNoteVisibility(selectionDoc);
				//Resize controls in our window to adjust for a possible change in the visibility of the panel note control.
				ResizeAll();
				//Refresh the signature and note in case the last document also had a signature.
				FillSignature();
				if(mountNum!=0){//mount
					ReloadZoomTransCrop(renderImage.Width,renderImage.Height,new Document(),
						new Rectangle(0,0,PictureBox1.Width,PictureBox1.Height),out imageZoom,
						out zoomLevel,out zoomFactor,out imageTranslation);
					RenderCurrentImage(new Document(),renderImage.Width,renderImage.Height,imageZoom,imageTranslation);
				}else{//document
					//Render the initial image within the current bounds of the picturebox (if the document is an image).
					InvalidateSettings(ApplySettings.ALL,true);
				}
			}
		}

		///<summary>Gets the category folder name for the given document node.</summary>
		private string GetCurrentFolderName(TreeNode node) {
			if(node!=null) {
				while(node.Parent!=null) {//Find the corresponding root level node.
					node=node.Parent;
				}
				return node.Text;
			}
			return "";
		}

		///<summary>Gets the document category of the current selection. The current selection can be a folder itself, or a document within a folder.</summary>
		private int GetCurrentCategory() {
			return DefB.GetByExactName(DefCat.ImageCats,GetCurrentFolderName(TreeDocuments.SelectedNode));
		}

		///<summary>Returns the current tree node with the given node id.</summary>
		private TreeNode GetNodeById(string nodeId) {
			return GetNodeById(nodeId,TreeDocuments.Nodes);//This defines the root node.
		}

		///<summary>Searches the current object tree for a row which has the given unique document number. This will work for a tree with any number of nested folders, as long as tags are defined only for items which correspond to data rows.</summary>
		private TreeNode GetNodeById(string nodeId,TreeNodeCollection rootNodes) {
			if(nodeId!=null && rootNodes!=null) {
				foreach(TreeNode n in rootNodes) {
					if(n!=null){
						if(GetNodeIdentifier(n)==nodeId) {
							return n;
						}else{//Check the child nodes.
							TreeNode child=GetNodeById(nodeId,n.Nodes);
							if(child!=null) {
								return child;
							}
						}
					}
				}
			}
			return null;
		}

		///<summary>Constructs a unique node identifier that would be the same for nodes with tags containing the same data.</summary>
		private string GetNodeIdentifier(TreeNode node) {
			if(node==null || node.Tag==null || node.Parent==null) {//Invalid node or is a folder node.
				return "";
			}
			DataRow obj=(DataRow)node.Tag;
			return MakeIdentifier(obj["DocNum"].ToString(),obj["MountNum"].ToString());
		}

		///<summary>One of the given parameters must be positive, and the other must be zero.</summary>
		private string MakeIdentifier(string docNum,string mountNum){
			return docNum+"*"+mountNum;
		}

		/// <summary>Refreshes list from db, then fills the treeview.  Set keepSelection to true in order to keep the current selection active.</summary>
		private void FillDocList(bool keepSelection){
			string selectionId=(keepSelection?GetNodeIdentifier(TreeDocuments.SelectedNode):"");
			//Clear current tree contents.
			TreeDocuments.SelectedNode=null;
			TreeDocuments.Nodes.Clear();
			if(PatCur==null){
				return;
			}
			//Add all predefined folder names to the tree.
			for(int i=0;i<DefB.Short[(int)DefCat.ImageCats].Length;i++){
				TreeDocuments.Nodes.Add(new TreeNode(DefB.Short[(int)DefCat.ImageCats][i].ItemName));
				TreeDocuments.Nodes[i].SelectedImageIndex=1;
				TreeDocuments.Nodes[i].ImageIndex=1;
			}
			//Add all relevant documents and mounts as stored in the database to the tree for the current patient.
			DataSet patientDocData=Documents.RefreshForPatient(new string[] { PatCur.PatNum.ToString() });
			DataRowCollection rows=patientDocData.Tables["DocumentList"].Rows;
			for(int i=0;i<rows.Count;i++) {
				TreeNode node=new TreeNode(rows[i]["description"].ToString());
				int parentFolder=Convert.ToInt32(rows[i]["docFolder"].ToString());
				TreeDocuments.Nodes[parentFolder].Nodes.Add(node);
				node.Tag=rows[i];
				node.ImageIndex=2+Convert.ToInt32(rows[i]["ImgType"].ToString());
				node.SelectedImageIndex=node.ImageIndex;
				if(GetNodeIdentifier(node)==selectionId){
					SelectTreeNode(node);
				}				
			}
			TreeDocuments.ExpandAll();//Invalidates tree too.
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Print":
						OnPrint_Click();
						break;
					case "Delete":
						OnDelete_Click();
						break;
					case "Info":
						OnInfo_Click();
						break;
					case "Sign":
						OnSign_Click();
						break;
					case "ScanDoc":
						OnScan_Click("doc");
						break;
					case "ScanXRay":
						OnScan_Click("xray");
						break;
					case "ScanPhoto":
						OnScan_Click("photo");
						break;
					case "Import":
						OnImport_Click();
						break;
					case "Copy":
						OnCopy_Click();
						break;
					case "Paste":
						OnPaste_Click();
						break;
					case "Forms":
						MsgBox.Show(this,"Use the dropdown list.  Add forms to the list by copying image files into your A-Z folder, Forms.  Restart the program to see newly added forms.");
						break;
					case "Capture":
						OnCapture_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void paintTools_ButtonClick(object sender,ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)) {
				switch(e.Button.Tag.ToString()) {
					case "Crop":
						OnCrop_Click();
						break;
					case "Hand":
						OnHand_Click();
						break;
					case "ZoomIn":
						OnZoomIn_Click();
						break;
					case "ZoomOut":
						OnZoomOut_Click();
						break;
					case "Flip":
						OnFlip_Click();
						break;
					case "RotateL":
						OnRotateL_Click();
						break;
					case "RotateR":
						OnRotateR_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)) {
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK){
				OnPatientSelected(formPS.SelectedPatNum);
				ModuleSelected(formPS.SelectedPatNum);
			}
			FillDocList(false);
		}

		private void OnPrint_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Tag==null){
				MsgBox.Show(this,"Cannot print. No document currently selected.");
				return;
			}
			try{
				if(PrintDialog1.ShowDialog()==DialogResult.OK){
					if(PrintDocument2.DefaultPageSettings.PaperSize.Width==0 ||
							PrintDocument2.DefaultPageSettings.PaperSize.Height==0) {
						PrintDocument2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
					}
					PrintDocument2.OriginAtMargins=true;
					PrintDocument2.DefaultPageSettings.Margins=new Margins(50,50,50,50);//Half-inch all around.
					PrintDocument2.Print();
				}
			}
			catch(System.Exception ex){
				MessageBox.Show(Lan.g(this,"An error occurred while printing"), ex.ToString());
			}
		}

		///<summary>If the node does not correspond to a valid document or mount, nothing happens. Otherwise the document/mount record and its corresponding file(s) are deleted.</summary>
		private void OnDelete_Click(){
			DeleteSelection(true);
		}
		
		///<summary>Deletes the current selection from the database and refreshes the tree view.</summary>
		private void DeleteSelection(bool verbose){
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				MsgBox.Show(this,"No item is currently selected");
				return;//No current selection, or some kind of internal error somehow.
			}
			if(TreeDocuments.SelectedNode.Parent==null){
				MsgBox.Show(this,"Cannot delete folders");
				return;
			}
			EnableAllTreeItemTools(false);
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			Document[] docs;
			bool refreshTree=true;
			if(mountNum!=0){//This is a mount object.
				//Delete the mount object.
				Mount mount=Mounts.GetByNum(mountNum);
				//Delete the mount items attached to the mount object.
				MountItem[] mountItems=MountItems.GetItemsForMount(mountNum);
				if(hotDocument>=0 && mountDocs[hotDocument]!=null){
					if(verbose) {
						if(!MsgBox.Show(this,true,"Delete mount xray image?")) {
							return;
						}
					}
					selectionDoc=new Document();
					docs=new Document[1] { mountDocs[hotDocument] };
					mountDocs[hotDocument]=null;
					//Release access to current image so it may be properly deleted.
					if(currentImages[hotDocument]!=null){
						currentImages[hotDocument].Dispose();
						currentImages[hotDocument]=null;
					}
					InvalidateSettings(ApplySettings.ALL,false);
					refreshTree=false;
				}else{
					if(verbose) {
						if(!MsgBox.Show(this,true,"Delete entire mount?")) {
							return;
						}
					}
					docs=mountDocs;
					Mounts.Delete(mount);
					for(int i=0;i<mountItems.Length;i++) {
						MountItems.Delete(mountItems[i]);
					}
					SelectTreeNode(null);//Release access to current image so it may be properly deleted.
				}				
			}else{
				if(verbose) {
					if(!MsgBox.Show(this,true,"Delete document?")) {
						return;
					}
				}
				docs=new Document[1] { Documents.GetByNum(docNum) };
				SelectTreeNode(null);//Release access to current image so it may be properly deleted.
			}
			//Delete all documents involved in deleting this object.
			imageStore.DeleteImage(docs);
			if(refreshTree){
				FillDocList(false);
			}
		}

		private void OnSign_Click(){
			if(TreeDocuments.SelectedNode==null ||				//No selection
				TreeDocuments.SelectedNode.Tag==null ||			//Internal error
				TreeDocuments.SelectedNode.Parent==null){		//This is a folder node.
				return;
			}
			//Show the underlying panel note box while the signature is being filled.
			panelNote.Visible=true;
			ResizeAll();
			//Display the document signature form.
			FormDocSign docSignForm=new FormDocSign(selectionDoc,imageStore);//Updates our local document and saves changes to db also.
			int signLeft=TreeDocuments.Left;
			docSignForm.Location=PointToScreen(new Point(signLeft,this.ClientRectangle.Bottom-docSignForm.Height));
			docSignForm.Width=Math.Max(0,Math.Min(docSignForm.Width,PictureBox1.Right-signLeft));
			docSignForm.ShowDialog();
			FillDocList(true);
			//Adjust visibility of panel note based on changes made to the signature above.
			SetPanelNoteVisibility(selectionDoc);
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
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			textNote.Text=selectionDoc.Note;
			sigBox.Visible=true;
			sigBox.SetTabletState(0);//never accepts input here
			labelInvalidSig.Visible=false;
			//Topaz box is not supported in Unix, since the required dll is Windows native.
			if(Environment.OSVersion.Platform!=PlatformID.Unix) {
				sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Visible=false;
				((Topaz.SigPlusNET)sigBoxTopaz).SetTabletState(0);
			}
			//A machine running Unix will have selectionDoc.SigIsTopaz set to false here, because the visibility of the panelNote
			//will be set to false in the case of Unix and SigIsTopaz. Therefore, the else part of this if-else clause is always
			//run on Unix systems.
			if(selectionDoc.SigIsTopaz) {
				if(selectionDoc.Signature!=null && selectionDoc.Signature!="") {
					sigBox.Visible=false;
					sigBoxTopaz.Visible=true;
					((Topaz.SigPlusNET)sigBoxTopaz).ClearTablet();
					((Topaz.SigPlusNET)sigBoxTopaz).SetSigCompressionMode(0);
					((Topaz.SigPlusNET)sigBoxTopaz).SetEncryptionMode(0);
					((Topaz.SigPlusNET)sigBoxTopaz).SetKeyString(GetHashString(selectionDoc));
					((Topaz.SigPlusNET)sigBoxTopaz).SetEncryptionMode(2);//high encryption
					((Topaz.SigPlusNET)sigBoxTopaz).SetSigCompressionMode(2);//high compression
					((Topaz.SigPlusNET)sigBoxTopaz).SetSigString(selectionDoc.Signature);
                    if (((Topaz.SigPlusNET)sigBoxTopaz).NumberOfTabletPoints() == 0)
                    {
						labelInvalidSig.Visible=true;
					}
				}
			}else{
				sigBox.ClearTablet();
				if(selectionDoc.Signature!=null && selectionDoc.Signature!="") {
					sigBox.Visible=true;
					sigBoxTopaz.Visible=false;
					sigBox.SetKeyString(GetHashString(selectionDoc));
					sigBox.SetSigString(selectionDoc.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.
				}
			}
		}

		private string GetHashString(Document doc) {
			return imageStore.GetHashString(doc);
		}

		///<summary>Valid values for scanType are "doc","xray",and "photo"</summary>
		private void OnScan_Click(string scanType) {
			Bitmap scannedImage=null;
			//Try catch here prevents a crash when a customer who has no scanner installed tries to scan.
			try{
				//A user may have more than one scanning device. 
				//The code below will allow the user to select one.
				long wPIXTypes=TWAIN_SelectImageSource(this.Handle);
				if(wPIXTypes==0) {//user clicked Cancel
					return;
				}
				TWAIN_AcquireToClipboard(this.Handle,wPIXTypes);
				IDataObject oDataObject=Clipboard.GetDataObject();
				if(oDataObject.GetDataPresent(DataFormats.Bitmap,true)) {
					scannedImage=(Bitmap)oDataObject.GetData(DataFormats.Bitmap);
				}else if(oDataObject.GetDataPresent(DataFormats.Dib,true)) {
					scannedImage=(Bitmap)oDataObject.GetData(DataFormats.Dib);
				}else{
					throw new Exception("Unknown image data format.");
				}
			}catch(Exception ex){
				MessageBox.Show("The image could not be acquired from the scanner. "+
					"Please check to see that the scanner is properly connected to the computer. Specific error: "+ex.Message);
				return;
			}
			ImageType imgType;
			if(scanType=="xray"){
				imgType=ImageType.Radiograph;
			}else if(scanType=="photo"){
				imgType=ImageType.Photo;
			}else{//Assume document
				imgType=ImageType.Document;
			}
			bool saved=true;
			Document doc = null;
			try{//Create corresponding image file.
				doc = imageStore.Import(scannedImage, GetCurrentCategory(), imgType);
			}catch{
				saved=false;
				MessageBox.Show(Lan.g(this,"Unable to save document."));
			}
			if(saved){
				FillDocList(false);//Reload and keep new document selected.
				SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
				FormDocInfo formDocInfo=new FormDocInfo(PatCur,doc,GetCurrentFolderName(TreeDocuments.SelectedNode));
				formDocInfo.ShowDialog();
				if(formDocInfo.DialogResult!=DialogResult.OK){
					File.Delete(doc.FileName);
					DeleteSelection(false);
				}else{
					FillDocList(true);//Update tree, in case the new document's icon or category were modified in formDocInfo.
				}
			}
		}

		private void OnImport_Click() {
			OpenFileDialog openFileDialog=new OpenFileDialog();
			openFileDialog.Multiselect=true;
			if(openFileDialog.ShowDialog()!=DialogResult.OK) {
				return;
			}
			string[] fileNames=openFileDialog.FileNames;
			if(fileNames.Length<1){
				return;
			}
			string nodeId="";
			Document doc=null;
			for(int i=0;i<fileNames.Length;i++){
				bool copied = true;
				try {
					doc = imageStore.Import(fileNames[i], GetCurrentCategory());
				}
				catch(Exception ex) {
					MessageBox.Show(Lan.g(this, "Unable to copy file, May be in use: ") + ex.Message + ": " + openFileDialog.FileName);
					copied = false;
				}
				if(copied){
					FillDocList(false);
					SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
					FormDocInfo FormD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(TreeDocuments.SelectedNode));
					FormD.ShowDialog();//some of the fields might get changed, but not the filename
					if(FormD.DialogResult!=DialogResult.OK){
						DeleteSelection(false);
					}else{
						nodeId=MakeIdentifier(doc.DocNum.ToString(),"0");
					}
				}
			}
			//Reselect the last successfully added node when necessary.
			if(doc!=null && MakeIdentifier(doc.DocNum.ToString(),"0")!=nodeId){
				SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
			}
			FillDocList(true);
		}

		private void OnCopy_Click(){
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Tag==null){
				MsgBox.Show(this,"Please select a document before copying");
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"]);
			int docNum=Convert.ToInt32(obj["DocNum"]);
			Bitmap copyImage;
			if(mountNum!=0){//The current selection is a mount?
				if(hotDocument>=0 && mountDocs[hotDocument]!=null){//A mount item is currently selected.
					copyImage=ApplyDocumentSettingsToImage(mountDocs[hotDocument],currentImages[hotDocument],ApplySettings.ALL);
				}else{//Assume the copy is for the entire mount.
					copyImage=renderImage;
				}
			}else{//document
				//Crop and color function has already been applied to the render image.
				copyImage=ApplyDocumentSettingsToImage(Documents.GetByNum(docNum),renderImage,
					ApplySettings.FLIP|ApplySettings.ROTATE);
			}
			if(copyImage!=null){
				Clipboard.SetDataObject(copyImage);
			}
		}

		private void OnPaste_Click() {
			IDataObject clipboard=Clipboard.GetDataObject();
			if(!clipboard.GetDataPresent(DataFormats.Bitmap)){
				MessageBox.Show(Lan.g(this,"No bitmap present on clipboard"));	
				return;
			}
			Document doc;
			try{
				Bitmap pasteImage=(Bitmap)clipboard.GetData(DataFormats.Bitmap);
				doc = imageStore.Import(pasteImage, GetCurrentCategory());
			}catch{
				MessageBox.Show(Lan.g(this,"Error saving document."));
				return;
			}
			FillDocList(false);
			SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
			FormDocInfo formD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(TreeDocuments.SelectedNode));
			formD.ShowDialog();
			if(formD.DialogResult!=DialogResult.OK){
				DeleteSelection(false);
			}else{
				FillDocList(true);
			}
			InvalidateSettings(ApplySettings.ALL,true);
		}

		private void OnCrop_Click() {
			//remember it's testing after the push has been completed
			if(paintTools.Buttons["Crop"].Pushed){ //Crop Mode
				paintTools.Buttons["Hand"].Pushed=false;
				PictureBox1.Cursor=Cursors.Default;
			}else{
				paintTools.Buttons["Crop"].Pushed=true;
			}
			IsCropMode=true;
			paintTools.Invalidate();
		}

		private void OnHand_Click() {
			if(paintTools.Buttons["Hand"].Pushed){//Hand Mode
				paintTools.Buttons["Crop"].Pushed=false;
				PictureBox1.Cursor=Cursors.Hand;
			}else{
				paintTools.Buttons["Hand"].Pushed=true;
			}
			IsCropMode=false;
			paintTools.Invalidate();
		}

		private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e){
			Bitmap prePrintImage=renderImage; //Keep a local pointer to the render image after this point, 
																				//so that the print results cannot be messed up by the current
																				//rendering thread (by changing the renderImage).
			if(prePrintImage==null || prePrintImage.Width<1 || prePrintImage.Height<1 ||
				TreeDocuments.SelectedNode==null ||
				TreeDocuments.SelectedNode.Tag==null ||
				TreeDocuments.SelectedNode.Parent==null){
				e.HasMorePages=false;
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"]);
			int docNum=Convert.ToInt32(obj["DocNum"]);
			Bitmap printImage;
			if(mountNum!=0){//Is this a mount object?
				if(hotDocument>=0 && mountDocs[hotDocument]!=null) {//A mount item is currently selected.
					printImage=ApplyDocumentSettingsToImage(mountDocs[hotDocument],currentImages[hotDocument],ApplySettings.ALL);
				}else{//Assume the printout is for the entire mount.
					printImage=prePrintImage;	//Just print the mount as is, since the mount is always in the same orientation, and
																		//the images it houses are already flipped and rotated to generate the render image.
				}
			}else{//This is a document object.
				//Crop and color function have already been applied to the render image, now do the rest.
				printImage=ApplyDocumentSettingsToImage(Documents.GetByNum(docNum),
					prePrintImage,ApplySettings.FLIP|ApplySettings.ROTATE);
			}			
			RectangleF rectf=e.MarginBounds;
			float ratio=Math.Min(rectf.Width/printImage.Width,rectf.Height/printImage.Height);
			Graphics g=e.Graphics;
			g.InterpolationMode=InterpolationMode.HighQualityBicubic;
			g.CompositingQuality=CompositingQuality.HighQuality;
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.PixelOffsetMode=PixelOffsetMode.HighQuality;
			g.DrawImage(printImage,0,0,(int)(printImage.Width*ratio),(int)(printImage.Height*ratio));
			e.HasMorePages=false;
		}

		private void menuTree_Click(object sender, System.EventArgs e) {
			switch(((MenuItem)sender).Index){
				case 0://print
					OnPrint_Click();
					break;
				case 1://delete
					OnDelete_Click();
					break;
				case 2://info
					OnInfo_Click();
					break;
			}
		}

		private void menuForms_Click(object sender, System.EventArgs e) {
			string formName = ((MenuItem)sender).Text;
			bool copied = true;
			Document doc = null;
			try {
				doc = imageStore.ImportForm(formName, GetCurrentCategory());
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
				copied=false;
			}
			if(copied){
				FillDocList(false);
				SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
				FormDocInfo FormD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(TreeDocuments.SelectedNode));
				FormD.ShowDialog();//some of the fields might get changed, but not the filename
				if(FormD.DialogResult!=DialogResult.OK){
					DeleteSelection(false);
				}else{
					FillDocList(true);//Refresh possible changes in the document due to FormD.
				}
			}
		}

		private void textNote_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void label1_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void label15_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void sigBox_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void sigBoxTopaz_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void labelInvalidSig_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void panelNote_DoubleClick(object sender,EventArgs e) {
			OnSign_Click();
		}

		private void textNote_MouseHover(object sender,EventArgs e) {
			textNote.Cursor=Cursors.IBeam;
		}

		///<summary>Mouse selections were chosen to be implemented in this particular way, just to make the same code work the same way under both Windows and MONO.</summary>
		private void TreeDocuments_MouseDown(object sender,MouseEventArgs e) {
			TreeNode node=TreeDocuments.GetNodeAt(e.Location);
			treeIdNumDown=GetNodeIdentifier(node);
			//Always select the node on a mouse-down press for either right or left buttons.
			//If the left button is pressed, then the document is either being selected or dragged, so
			//setting the image at the beginning of the drag will either display the image as expected, or
			//automatically display the image while the document is being dragged (since it is in a different thread).
			//If the right button is pressed, then the user wants to view the properties of the image they are
			//clicking on, so displaying the image (in a different thread) will give the user a chance to view
			//the image corresponding to a delete, info display, etc...
			SelectTreeNode(node);
		}

		private void TreeDocuments_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			TreeNode node=TreeDocuments.GetNodeAt(e.Location);
			if(treeIdNumDown!="" && GetNodeIdentifier(node)!=treeIdNumDown){
				TreeDocuments.Cursor=Cursors.Hand;
			}else{
				TreeDocuments.Cursor=Cursors.Default;
			}
		}

		///<summary>Mouse selections were chosen to be implemented in this particular way, just to make the same code work the same way under both Windows and MONO.</summary>
		private void TreeDocuments_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(treeIdNumDown==""){
				return;
			}
			TreeNode node=TreeDocuments.GetNodeAt(e.Location);
			TreeNode sourceNode=GetNodeById(treeIdNumDown);			
			//Dragging a document?
			if(e.Button==MouseButtons.Left && GetNodeIdentifier(node)!=treeIdNumDown) {
				TreeDocuments.Cursor=Cursors.Default;
				//Find the destination folder.
				int destinationCategory;
				if(node.Parent!=null) {
					destinationCategory=DefB.Short[(int)DefCat.ImageCats][node.Parent.Index].DefNum;
				}
				else {
					destinationCategory=DefB.Short[(int)DefCat.ImageCats][node.Index].DefNum;
				}
				//Update the object's document category in the database.
				DataRow obj=(DataRow)sourceNode.Tag;
				int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
				int docNum=Convert.ToInt32(obj["DocNum"].ToString());
				string id;
				if(mountNum!=0) {//Mount object.
					Mount mount=Mounts.GetByNum(mountNum);
					mount.DocCategory=destinationCategory;
					Mounts.Update(mount);
					id=MakeIdentifier("0",mount.MountNum.ToString());
				}else{//Document object.
					Document doc=Documents.GetByNum(docNum);
					doc.DocCategory=destinationCategory;
					Documents.Update(doc);
					id=MakeIdentifier(doc.DocNum.ToString(),"0");
				}
				FillDocList(true);
				treeIdNumDown="";
			}
		}

		private void TreeDocuments_MouseLeave(object sender,EventArgs e) {
			TreeDocuments.Cursor=Cursors.Default;
			treeIdNumDown="";
		}

		///<summary>Invalidates some or all of the image settings.  This will cause those settings to be recalculated, either immediately, or when the current ApplySettings thread is finished.  If supplied settings is ApplySettings.NONE, then that part will be skipped.</summary>
		private void InvalidateSettings(ApplySettings settings,bool reloadZoomTransCrop){
			if(this.InvokeRequired){
				InvalidatesettingsCallback c=new InvalidatesettingsCallback(InvalidateSettings);
				Invoke(c,new object[] {settings,reloadZoomTransCrop});
				return;
			}
			//Do not allow image rendering when the paint tools are disabled. This will disable the display image when a folder or non-image document is selected, or when no document is currently selected. The paintTools.Enabled boolean is controlled in SelectTreeNode() and is set to true only if a valid image is currently being displayed.
			if(TreeDocuments.SelectedNode==null || 
				TreeDocuments.SelectedNode.Tag==null || TreeDocuments.SelectedNode.Parent==null){
				EraseCurrentImages();
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(docNum!=0){//Only applied to document nodes.
				if(!paintTools.Enabled){
					EraseCurrentImages();
					return;
				}
				if(reloadZoomTransCrop) {
					//Reloading the image settings only happens when a new image is selected, pasted, scanned, etc...
					//Therefore, the is no need for any current image processing anymore (it would be on a stail image).
					KillMyImageThreads();
					ReloadZoomTransCrop(curImageWidths[0],curImageHeights[0],selectionDoc,
						new Rectangle(0,0,PictureBox1.Width,PictureBox1.Height),
						out imageZoom,out zoomLevel,out zoomFactor,out imageTranslation);
					cropTangle=new Rectangle(0,0,-1,-1);
				}	
			}
			InvalidatedSettingsFlag|=settings;
			//selectionDoc is an individual document instance. Assigning a new document to settingDoc here does not 
			//negatively effect our image application thread, because the thread either will keep its current 
			//reference to the old document, or will apply the settings with this newly assigned document. In either
			//case, the output is either what we expected originally, or is a more accurate image for more recent 
			//settings. We lock here so that we are sure that the resulting document and setting tuple represent
			//a single point in time.
			lock(settingHandle){//Does not actually lock the settingHandle object, but rather locks the variables in the block.
				settingDoc=selectionDoc.Copy();
				settingFlags=InvalidatedSettingsFlag;
				if(docNum!=0){									
					settingMount=null;					
				}else{//Mount
					settingMount=selectionMount.Copy();
				}
			}
			//Tell the thread to start processing (as soon as the thread is created, or as soon as otherwise 
			//possible). Set() has no effect if the handle is already signaled.
			settingHandle.Set();
			if(myThread==null){//Create the thread if it has not been created, or if it was killed for some reason.
				myThread=new Thread((ThreadStart)(delegate { Worker(); }));
				myThread.IsBackground=true;
				myThread.Start();
			}
			InvalidatedSettingsFlag=ApplySettings.NONE;
		}

		///<summary>Applies crop and colors. Then, paints renderImage onto PictureBox1.</summary>
		private void Worker() {
			while(true){
				try{
					//Wait indefinitely for a signal to start processing again. Since the OS handles this function,
					//this thread will not run even a single process cycle until a signal is recieved. This is ideal,
					//since it means that we do not waste any CPU cycles when image processing is not currently needed.
					//At the same time, this function allows us to keep a single thread for as long as possible, so
					//that we do not need to destroy and recreate this thread (except in rare circumstances, such as
					//the deletion of the current image).
					settingHandle.WaitOne(-1,false);
					//The settingDoc may have been reset several times at this point by calls to InvalidateSettings(), but that cannot hurt
					//us here because it simply means that we are getting even more current information than we had when this thread was
					//signaled to start. We lock here so that we are sure that the resulting document and setting tuple represent
					//a single point in time.
					Document curDocCopy;
					Mount curMountCopy;
					ApplySettings applySettings;
					lock(settingHandle){//Does not actually lock the settingHandle object.
						curDocCopy=settingDoc;
						curMountCopy=settingMount;
						applySettings=settingFlags;
					}
					if(settingMount==null){//The current selection is a document.
						//Perform cropping and colorfunction here if one of the two flags is set. Rotation, flip, zoom and translation are
						//taken care of in RenderCurrentImage().
						if(	(applySettings&ApplySettings.COLORFUNCTION)!=ApplySettings.NONE || 
								(applySettings&ApplySettings.CROP)!=ApplySettings.NONE){
							//Ensure that memory management for the renderImage is performed in the worker thread, otherwise the main thread
							//will be slowed when it has to cleanup dozens of old renderImages, which causes a temporary pause in operation.
							if(renderImage!=null){
								//Done like this so that the renderImage is cleared in a single atomic line of code (in case the thread is
								//killed during this step), so that we don't end up with a pointer to a disposed image in the renderImage.
								Bitmap oldRenderImage=renderImage;
								renderImage=null;
								oldRenderImage.Dispose();
							}
							//currentImages[] is guaranteed to exist and be the current. If currentImages gets updated, this thread 
							//gets aborted with a call to KillMyThread(). The only place currentImages[] is invalid is in a call to 
							//EraseCurrentImage(), but at that point, this thread has been terminated.
							renderImage=ApplyDocumentSettingsToImage(curDocCopy,currentImages[hotDocument],
								ApplySettings.CROP|ApplySettings.COLORFUNCTION);
						}
						//Make the current renderImage visible in the picture box, and perform rotation, flip, zoom, and translation on
						//the renderImage.
						RenderCurrentImage(curDocCopy,curImageWidths[hotDocument],curImageHeights[hotDocument],
							imageZoom*zoomFactor,imageTranslation);
					}else{//The current selection is a mount.
						RenderMountFrames(renderImage,selectionMountItems,hotDocument);
						//Render only the modified image over the old mount image.
						//A null document can happen when a new image frame is selected, but there is no image in that frame.
						if(curDocCopy!=null && hotDocument>=0 && applySettings!=ApplySettings.NONE){
							RenderImageIntoMount(renderImage,selectionMountItems[hotDocument],currentImages[hotDocument],curDocCopy);
						}
						RenderCurrentImage(new Document(),renderImage.Width,renderImage.Height,imageZoom*zoomFactor,imageTranslation);
					}
				}catch(ThreadAbortException){
					return;	//Exit as requested. This can happen when the current document is being deleted, 
							//or during shutdown of the program.
				}catch(Exception){
					//We don't draw anyting on error (because most of the time it will be due to the current selection state).
				}
			}
		}

		///<summary>Kills the image processing thread if it is currently running.</summary>
		private void KillMyImageThreads(){
			xRayImageController.KillXRayThread();//Stop the current xRay image thread if it is running.
			if(myThread!=null){//Clear any previous image processing.
				if(myThread.IsAlive){
					myThread.Abort();//this is not recommended because it results in an exception.  But it seems to work.
					myThread.Join();//Wait for thread to stop execution.
				}
				myThread=null;
			}
		}

		///<summary>Handles rendering to the PictureBox of the image in its current state. The image calculations are not performed here, only rendering of the image is performed here, so that we can guarantee a fast display.</summary>
		private void RenderCurrentImage(Document docCopy,int originalWidth,int originalHeight,float zoom,PointF translation) {
			if(!this.Visible){
				return;
			}
			//Helps protect against simultaneous access to the picturebox in both the main and render worker threads.
			if(PictureBox1.InvokeRequired){
				RenderImageCallback c=new RenderImageCallback(RenderCurrentImage);
				Invoke(c,new object[] {docCopy,originalWidth,originalHeight,zoom,translation});
				return;
			}
			int width=PictureBox1.Bounds.Width;
			int height=PictureBox1.Bounds.Height;
			if(width<=0 || height<=0) {
				return;
			}
			Bitmap backBuffer=new Bitmap(width,height);
			Graphics g=Graphics.FromImage(backBuffer);
			try{
				g.Clear(PictureBox1.BackColor);
				g.Transform=GetScreenMatrix(docCopy,originalWidth,originalHeight,zoom,translation);
				g.DrawImage(renderImage,0,0);
				if(cropTangle.Width>0 && cropTangle.Height>0) {//Must be drawn last so it is on top.
					g.ResetTransform();
					g.DrawRectangle(Pens.Blue,cropTangle);
				}
				g.Dispose();
				//Cleanup old back-buffer.
				if(PictureBox1.Image!=null) {
					PictureBox1.Image.Dispose();	//Make sure that the calling thread performs the memory cleanup, instead of relying
																				//on the memory-manager in the main thread (otherwise the graphics get spotty sometimes).
				}
				PictureBox1.Image=backBuffer;
				PictureBox1.Refresh();
			}catch(Exception) {
				g.Dispose();
			}
		}

		private void TreeDocuments_MouseDoubleClick(object sender,MouseEventArgs e) {
			TreeNode clickedNode=TreeDocuments.GetNodeAt(e.Location);
			if(clickedNode==null || GetNodeIdentifier(clickedNode)==""){
				return;
			}
			DataRow obj=(DataRow)clickedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0){//Is this object a mount object?
				FormMountEdit fme=new FormMountEdit(selectionMount);
				fme.ShowDialog();//Edits the selectionMount object directly and updates and changes to the database as well.
				FillDocList(true);//Refresh tree in case description for the mount changed.
				return;
			}
			Document nodeDoc=Documents.GetByNum(docNum);
			string ext = imageStore.GetExtension(nodeDoc);
			if(ext==".jpg" || ext==".jpeg" || ext==".gif") {
				return;
			}
			//We allow anything which ends with a different extention to be viewed in the windows fax viewer.
			//Specifically, multi-page faxes can be viewed more easily by one of our customers using the fax
			//viewer. On Unix systems, it is imagined that an equivalent viewer will launch to allow the image
			//to be viewed.
			if(imageStore.FilePathSupported) {

				try {
					Process.Start(imageStore.GetFilePath(nodeDoc));
				}
				catch(Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}
			else {
				MessageBox.Show(Lan.g(this, "Cannot open the document in an external viewer. This is most likely because the images are stored in a database, which cannot be browsed."));
			}
		}

		private void OnInfo_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0) {//The current selection is a mount.
				FormMountEdit fme=new FormMountEdit(selectionMount);
				fme.ShowDialog();//Edits the selectionMount object directly and updates and changes to the database as well.
				FillDocList(true);//Refresh tree in case description for the mount changed.}
			}else if(docNum!=0){//A document is currently selected.
				//The FormDocInfo object updates the selectionDoc and stores the changes in the database as well.
				FormDocInfo formDocInfo2=new FormDocInfo(PatCur,selectionDoc,GetCurrentFolderName(TreeDocuments.SelectedNode));
				formDocInfo2.ShowDialog();
				if(formDocInfo2.DialogResult!=DialogResult.OK) {
					return;
				}
				FillDocList(true);
			}
		}

		///<summary>This button is disabled for mounts, in which case this code is never called.</summary>
		private void OnZoomIn_Click() {
			zoomLevel++;
			PointF c=new PointF(PictureBox1.ClientRectangle.Width/2.0f,PictureBox1.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-imageTranslation.X,c.Y-imageTranslation.Y);
			imageTranslation=new PointF(imageTranslation.X-p.X,imageTranslation.Y-p.Y);
			zoomFactor=(float)Math.Pow(2,zoomLevel);
			InvalidateSettings(ApplySettings.NONE,false);//Refresh display.
		}

		///<summary>This button is disabled for mounts, in which case this code is never called.</summary>
		private void OnZoomOut_Click() {
			zoomLevel--;
			PointF c=new PointF(PictureBox1.ClientRectangle.Width/2.0f,PictureBox1.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-imageTranslation.X,c.Y-imageTranslation.Y);
			imageTranslation=new PointF(imageTranslation.X+p.X/2.0f,imageTranslation.Y+p.Y/2.0f);
			zoomFactor=(float)Math.Pow(2,zoomLevel);
			InvalidateSettings(ApplySettings.NONE,false);//Refresh display.
		}

		private void DeleteThumbnailImage(Document doc){
			imageStore.DeleteThumbnailImage(doc);
		}

		private void OnFlip_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="" || selectionDoc==null) {
				return;
			}			
			selectionDoc.IsFlipped=!selectionDoc.IsFlipped;
			Documents.Update(selectionDoc);
			DeleteThumbnailImage(selectionDoc);
			InvalidateSettings(ApplySettings.FLIP,false);//Refresh display.
		}

		private void OnRotateL_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="" || selectionDoc==null) {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"]);
			if(mountNum!=0){//Must be rotating an item in a mount (since a mount item must be selected for rotations to be enabled).
				//We only allow mount items to be rotated by 180 degrees, because 90 degree rotations will eventually be handled by
				//the mount designer, and beyond that point, it would be highly unusual for a dentist to want to rotate by 90 degree
				//increments.
				selectionDoc.DegreesRotated-=180;
			}else{//Document
				selectionDoc.DegreesRotated-=90;
			}			
			while(selectionDoc.DegreesRotated<0) {
				selectionDoc.DegreesRotated+=360;
			}
			Documents.Update(selectionDoc);
			DeleteThumbnailImage(selectionDoc);
			InvalidateSettings(ApplySettings.ROTATE,false);//Refresh display.
		}

		private void OnRotateR_Click(){
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="" || selectionDoc==null) {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"]);
			if(mountNum!=0) {//Must be rotating an item in a mount (since a mount item must be selected for rotations to be enabled).
				//We only allow mount items to be rotated by 180 degrees, because 90 degree rotations will eventually be handled by
				//the mount designer, and beyond that point, it would be highly unusual for a dentist to want to rotate by 90 degree
				//increments.
				selectionDoc.DegreesRotated+=180;
			}
			else {//Document
				selectionDoc.DegreesRotated+=90;
			}			
			selectionDoc.DegreesRotated%=360;
			Documents.Update(selectionDoc);
			DeleteThumbnailImage(selectionDoc);
			InvalidateSettings(ApplySettings.ROTATE,false);//Refresh display.
		}

		///<summary>Keeps the back buffer for the picture box to be the same in dimensions as the picture box itself.</summary>
		private void PictureBox1_SizeChanged(object sender,EventArgs e) {
			InvalidateSettings(ApplySettings.NONE,false);//Refresh display.
		}

		///<summary></summary>
		private void PictureBox1_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			MouseDownOrigin=new Point(e.X,e.Y);
			MouseIsDown=true;
			imageLocation=new PointF(imageTranslation.X,imageTranslation.Y);
		}

		private void PictureBox1_MouseHover(object sender,EventArgs e) {
			if(paintTools.Buttons["Hand"].Pushed) {//Hand mode.
				PictureBox1.Cursor=Cursors.Hand;
			}else{
				PictureBox1.Cursor=Cursors.Default;
			}
		}

		private void PictureBox1_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!MouseIsDown){
				return;
			}
			dragging=true;
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			if(paintTools.Buttons["Hand"].Pushed) {//Hand mode.
				imageTranslation=new PointF(imageLocation.X+(e.Location.X-MouseDownOrigin.X),
																		imageLocation.Y+(e.Location.Y-MouseDownOrigin.Y));
			}else if(paintTools.Buttons["Crop"].Pushed){
				float[] intersect=ODMathLib.IntersectRectangles(Math.Min(e.Location.X,MouseDownOrigin.X),
					Math.Min(e.Location.Y,MouseDownOrigin.Y),Math.Abs(e.Location.X-MouseDownOrigin.X),
					Math.Abs(e.Location.Y-MouseDownOrigin.Y),PictureBox1.ClientRectangle.X,PictureBox1.ClientRectangle.Y,
					PictureBox1.ClientRectangle.Width-1,PictureBox1.ClientRectangle.Height-1);
				if(intersect.Length<0){
					cropTangle=new Rectangle(0,0,-1,-1);
				}else{
					cropTangle=new Rectangle((int)intersect[0],(int)intersect[1],(int)intersect[2],(int)intersect[3]);
				}
			}
			InvalidateSettings(ApplySettings.NONE,false);//Refresh display.
		}

		private void PictureBox1_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			bool wasDragging=dragging;
			MouseIsDown=false;
			dragging=false;
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			if(paintTools.Buttons["Hand"].Pushed){
				if(e.Button!=MouseButtons.Left || wasDragging) {
					return;
				}
				DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
				int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
				if(mountNum!=0){//The user may be trying to select an individual image within the current mount.
					PointF relativeMouseLocation=new PointF(
						(MouseDownOrigin.X-imageTranslation.X)/(imageZoom*zoomFactor)+selectionMount.Width/2,
						(MouseDownOrigin.Y-imageTranslation.Y)/(imageZoom*zoomFactor)+selectionMount.Height/2);
					//Unselect all mount frames, and reselect the frame clicked on (if any).
					hotDocument=-1;
					//Assume no item will be selected and enable tools again if an item was actually selected.
					EnableTreeItemTools(true,true,true,true,false,false,false,true,true,true,false,false,false);
					//Enumerate the image locations.
					for(int i=0;i<selectionMountItems.Length;i++){
						RectangleF itemLocation=new RectangleF(selectionMountItems[i].Xpos,selectionMountItems[i].Ypos,
							selectionMountItems[i].Width,selectionMountItems[i].Height);
						if(itemLocation.Contains(relativeMouseLocation)){
							hotDocument=i;//Set the item selection in the mount.
							for(int j=0;j<selectionMountItems.Length;j++){
								if(selectionMountItems[j].OrdinalPos==hotDocument){
									if(mountDocs[j]!=null){
										selectionDoc=mountDocs[j];
										SetBrightnessAndContrast();
										EnableTreeItemTools(true,true,false,true,false,true,false,true,true,true,true,true,true);
									}
								}
							}
						}
					}
					paintTools.Invalidate();
					if(hotDocument<0){//The current selection was unselected.
						xRayImageController.KillXRayThread();//Stop xray capture, because it relies on the current selection to place images.
					}
					InvalidateSettings(ApplySettings.ALL,false);
				}
			}else{//crop mode
				if(cropTangle.Width<=0 || cropTangle.Height<=0) {
					return;
				}
				if(!MsgBox.Show(this,true,"Crop to Rectangle?")){
					cropTangle=new Rectangle(0,0,-1,-1);
					InvalidateSettings(ApplySettings.NONE,false);//Refresh display (since message box was covering).
					return;
				}
				DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
				int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
				int docNum=Convert.ToInt32(obj["DocNum"].ToString());
				float cropZoom=imageZoom*zoomFactor;
				PointF cropTrans=imageTranslation;
				PointF cropPoint1=ScreenPointToUnalteredDocumentPoint(cropTangle.Location,selectionDoc,
					curImageWidths[hotDocument],curImageHeights[hotDocument],cropZoom,cropTrans);
				PointF cropPoint2=ScreenPointToUnalteredDocumentPoint(new Point(cropTangle.Location.X+cropTangle.Width,
					cropTangle.Location.Y+cropTangle.Height),selectionDoc,curImageWidths[hotDocument],curImageHeights[hotDocument],
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
				Rectangle oldCropRect=DocCropRect(selectionDoc,curImageWidths[hotDocument],curImageHeights[hotDocument]);
				float[] finalCropRect=ODMathLib.IntersectRectangles(rawCropRect.X,rawCropRect.Y,rawCropRect.Width,
					rawCropRect.Height,oldCropRect.X,oldCropRect.Y,oldCropRect.Width,oldCropRect.Height);
				//Will return a null intersection when the user chooses a crop rectangle which is
				//entirely outside the current visible portion of the image. Can also return a zero-area rect,
				//if the entire image is cropped away.
				if(finalCropRect.Length!=4 || finalCropRect[2]<=0 || finalCropRect[3]<=0){
					cropTangle=new Rectangle(0,0,-1,-1);
					InvalidateSettings(ApplySettings.NONE,false);//Refresh display (since message box was covering).
					return;
				}
				Rectangle prevCropRect=DocCropRect(selectionDoc,curImageWidths[hotDocument],curImageHeights[hotDocument]);
				selectionDoc.CropX=(int)finalCropRect[0];
				selectionDoc.CropY=(int)finalCropRect[1];
				selectionDoc.CropW=(int)Math.Ceiling(finalCropRect[2]);
				selectionDoc.CropH=(int)Math.Ceiling(finalCropRect[3]);
				Documents.Update(selectionDoc);
				if(docNum!=0){
					DeleteThumbnailImage(selectionDoc);
					Rectangle newCropRect=DocCropRect(selectionDoc,curImageWidths[hotDocument],curImageHeights[hotDocument]);
					//Update the location of the image so that the cropped portion of the image does not move in screen space.
					PointF prevCropCenter=new PointF(prevCropRect.X+prevCropRect.Width/2.0f,prevCropRect.Y+prevCropRect.Height/2.0f);
					PointF newCropCenter=new PointF(newCropRect.X+newCropRect.Width/2.0f,newCropRect.Y+newCropRect.Height/2.0f);
					PointF[] imageCropCenters=new PointF[] {
						prevCropCenter,
						newCropCenter
					};
					Matrix docMat=GetDocumentFlippedRotatedMatrix(selectionDoc);
					docMat.Scale(cropZoom,cropZoom);
					docMat.TransformPoints(imageCropCenters);
					imageTranslation=new PointF(imageTranslation.X+(imageCropCenters[1].X-imageCropCenters[0].X),
																			imageTranslation.Y+(imageCropCenters[1].Y-imageCropCenters[0].Y));
				}
				cropTangle=new Rectangle(0,0,-1,-1);
				InvalidateSettings(ApplySettings.CROP,false);
			}
		}

		private PointF MountSpaceToScreenSpace(PointF p){
			PointF relvec=new PointF(p.X/selectionMount.Width-0.5f,p.Y/selectionMount.Height-0.5f);
			return new PointF(imageTranslation.X+relvec.X*selectionMount.Width*imageZoom*zoomFactor,
				imageTranslation.Y+relvec.Y*selectionMount.Height*imageZoom*zoomFactor);
		}

		private void SetBrightnessAndContrast() {
			if(selectionDoc.WindowingMax==0) {
				//The document brightness/contrast settings have never been set. By default, we use settings
				//which do not alter the original image.
				brightnessContrastSlider.MinVal=0;
				brightnessContrastSlider.MaxVal=255;
			}
			else {
				brightnessContrastSlider.MinVal=selectionDoc.WindowingMin;
				brightnessContrastSlider.MaxVal=selectionDoc.WindowingMax;
			}
		}

		private void brightnessContrastSlider_Scroll(object sender,EventArgs e){
			if(selectionDoc==null){
				return;
			}
			selectionDoc.WindowingMin=brightnessContrastSlider.MinVal;
			selectionDoc.WindowingMax=brightnessContrastSlider.MaxVal;
			InvalidateSettings(ApplySettings.COLORFUNCTION,false);
		}

		private void brightnessContrastSlider_ScrollComplete(object sender,EventArgs e) {
			if(selectionDoc==null){
				return;
			}
			Documents.Update(selectionDoc);
			DeleteThumbnailImage(selectionDoc);
			InvalidateSettings(ApplySettings.COLORFUNCTION,false);
		}

		///<summary>Handles a change in selection of the xRay capture button.</summary>
		private void OnCapture_Click() {
			if(ToolBarMain.Buttons["Capture"].Pushed) {
				int mountNum=0;
				if(GetNodeIdentifier(TreeDocuments.SelectedNode)!=""){//A document or mount is currently selected.
					DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
					mountNum=Convert.ToInt32(obj["MountNum"].ToString());					
				}
				ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
				xRayImageController.SensorType=computerPrefs.SensorType;
				xRayImageController.PortNumber=computerPrefs.SensorPort;
				xRayImageController.Binned=computerPrefs.SensorBinned;
				xRayImageController.ExposureLevel=computerPrefs.SensorExposure;
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
					SelectTreeNode(GetNodeById(MakeIdentifier("0",mount.MountNum.ToString())));
					brightnessContrastSlider.MinVal=PrefB.GetInt("ImageWindowingMin");
					brightnessContrastSlider.MaxVal=PrefB.GetInt("ImageWindowingMax");
				}else {//A mount is currently selected. We must allow the user to insert new images into partially complete mounts.
					//Clear the visible selection so that the user will know when the device is ready for xray exposure.
					RenderMountFrames(renderImage,selectionMountItems,-1);
					RenderCurrentImage(new Document(),renderImage.Width,renderImage.Height,imageZoom*zoomFactor,imageTranslation);
				}
				//Here we can only allow access to the capture button during a capture, because it is too complicated and hard for a 
				//user to follow what is going on if they use the other tools when a capture is taking place.
				EnableAllTools(false);
				ToolBarMain.Buttons["Capture"].Enabled=true;
				ToolBarMain.Invalidate();
				xRayImageController.CaptureXRay();
			}else{//The user unselected the image capture button, so cancel the current image capture.
				xRayImageController.KillXRayThread();//Stop current xRay capture and call OnCaptureFinalize() when done.
			}
		}

		///<summary>Called when the image capture device is ready for exposure.</summary>
		private void OnCaptureReady(object sender,EventArgs e) {
			GetNextUnusedMountItem();
			InvalidateSettings(ApplySettings.NONE,false);//Refresh the selection box change (does not do any image processing here).
		}

		///<summary>Called on successful capture of image.</summary>
		private void OnCaptureComplete(object sender,EventArgs e) {
			if(this.InvokeRequired){
				CaptureCallback c=new CaptureCallback(OnCaptureComplete);
				Invoke(c,new object[] {sender,e});
				return;
			}
			if(hotDocument<0 || mountDocs[hotDocument]!=null) {//Mount is full.
				xRayImageController.KillXRayThread();
				return;
			}
			//Depending on the device being captured from, we need to rotate the images returned from the device by a certain
			//angle, and we need to place the returned images in a specific order within the mount slots. Later, we will allow
			//the user to define the rotations and slot orders, but for now, they will be hard-coded.
			short rotationAngle=0;
			switch(hotDocument){
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
			Document doc = imageStore.ImportCapturedImage(capturedImage, rotationAngle, selectionMountItems[hotDocument].MountItemNum, GetCurrentCategory());
			currentImages[hotDocument]=capturedImage;
			curImageWidths[hotDocument]=capturedImage.Width;
			curImageHeights[hotDocument]=capturedImage.Height;
			mountDocs[hotDocument]=doc;
			selectionDoc=doc;
			SetBrightnessAndContrast();
			//Refresh image in in picture box.
			InvalidateSettings(ApplySettings.ALL,false);
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
			if(hotDocument>0 && mountDocs[hotDocument]!=null) {//The capture finished in a state where a mount item is selected.
				EnableTreeItemTools(true,true,false,true,false,true,false,true,true,true,true,true,true);
			}else{//The capture finished without a mount item selected (so the mount itself is considered to be selected).
				EnableTreeItemTools(true,true,true,true,false,false,false,true,true,true,false,false,false);
			}
		}

		private void GetNextUnusedMountItem() {
			//Advance selection box to the location where the next image will capture to.
			if(hotDocument<0) {
				hotDocument=0;
			}
			int hotStart=hotDocument;
			int d=hotDocument;
			do{
				if(mountDocs[hotDocument]==null) {
					return;//Found an open frame in the mount.
				}
				hotDocument=(hotDocument+1)%mountDocs.Length;
			} while(hotDocument!=hotStart);
			hotDocument=-1;
		}

		///<summary>Kills ImageApplicationThread.  Disposes of both currentImages and renderImage.  Does not actually trigger a refresh of the Picturebox, though.</summary>
		private void EraseCurrentImages(){
			KillMyImageThreads();//Stop any current access to the current image and render image so we can dispose them.
			PictureBox1.Image=null;
			InvalidatedSettingsFlag=ApplySettings.NONE;
			if(currentImages!=null){
				for(int i=0;i<currentImages.Length;i++){
					if(currentImages[i]!=null){
						currentImages[i].Dispose();
						currentImages[i]=null;
					}	
				}
			}
			if(renderImage!=null){
				renderImage.Dispose();
				renderImage=null;
			}
		}

		//===================================== STATIC FUNCTIONS =================================================

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
		public static Matrix GetScreenMatrix(Document doc,int docOriginalImageWidth,int docOriginalImageHeight,
				float imageScale,PointF imageTranslation) {			
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
				int docOriginalImageWidth,int docOriginalImageHeight,float imageScale,PointF imageTranslation){
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

		///<summary>Returns true if the given filename contains a supported file image extension.</summary>
		[Obsolete("Use ImageHelper.HasImageExtension instead.")]
		public static bool HasImageExtension(string fileName) {
			return ImageHelper.HasImageExtension(fileName);
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

		///<summary>Applies the document specified cropping, flip, rotation, brightness and contrast transformations to the image and returns the resulting image. Zoom and translation must be handled by the calling code. The returned image is always a new image that can be modified without affecting the original image. The change in the image's center point is returned into deltaCenter, so that rotation offsets can be properly calculated when displaying the returned image.</summary>
		[Obsolete("Use ImageHelper.ApplyDocumentSettingsToImage instead.")]
		public static Bitmap ApplyDocumentSettingsToImage(Document doc,Bitmap image,ApplySettings settings){
			return ImageHelper.ApplyDocumentSettingsToImage(doc, image, settings);
		}

		///<summary>specify the size of the square to return</summary>
		[Obsolete("Use ImageHelper.GetThumbnail instead.")]
		public static Bitmap GetThumbnail(Image original,int size) {
			return ImageHelper.GetThumbnail(original, size);
		}

		///<summary>Renders the hallow rectangles which represent the individual image frames into the given mount image.</summary>
		[Obsolete("Use ImageHelper.RenderMountFrames instead")]
		private static void RenderMountFrames(Bitmap mountImage,MountItem[] mountItems,int imageSelected){
			ImageHelper.RenderMountFrames(mountImage, mountItems, imageSelected);
		}

		///<summary>Renders the given image using the settings provided by the given document object into the location of the given mountItem object.</summary>
		[Obsolete("Use ImageHlper.RenderImageIntoMount instead.")]
		private static void RenderImageIntoMount(Bitmap mountImage,MountItem mountItem,Bitmap mountItemImage,Document mountItemDoc){
			ImageHelper.RenderImageIntoMount(mountImage, mountItem, mountItemImage, mountItemDoc);
		}

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. Set imageSelected=-1 to unselect all images, or set to an image ordinal to highlight the image. The mount is rendered onto the given mountImage, so it must have been appropriately created by CreateBlankMountImage(). One can create a mount template by passing in arrays of zero length.</summary>
		[Obsolete("Use ImageHelper.RenderMountImage instead.")]
		private static void RenderMountImage(Bitmap mountImage,Bitmap[] originalImages,MountItem[] mountItems,Document[] documents,int imageSelected) {
			ImageHelper.RenderMountImage(mountImage, originalImages, mountItems, documents, imageSelected);
		}

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. For use in other modules.</summary>
		public Bitmap CreateMountImage(Mount mount){
			MountItem[] mountItems=MountItems.GetItemsForMount(mount.MountNum);
			Document[] documents=Documents.GetDocumentsForMountItems(mountItems);
			Bitmap[] originalImages=imageStore.RetrieveImage(documents);
			Bitmap mountImage=new Bitmap(mount.Width,mount.Height);
			RenderMountImage(mountImage,originalImages,mountItems,documents,-1);
			return mountImage;
		}

		private void buttonClipboard_Click(object sender, EventArgs e) {
			if(imageStore.OpenFolderSupported) {
				string theFolder = imageStore.FolderPath;
				Clipboard.SetDataObject(theFolder.ToString());
			}
			else {
				MessageBox.Show(Lan.g(this, "Cannot open the folder in which the images are stored. This is most likely because the images are stored in a database, which cannot be browsed."));
			}
		}

		private void butCBSheet_Click(object sender, EventArgs e) {
			if(imageStore.OpenFolderSupported) {
				this.Cursor = Cursors.AppStarting;

				string ProgName = PrefB.GetString("WordProcessorPath");
				//if (File.Exists(patFolder + @"~C&B Sheet.doc")){
				//open existing file
				//}; 
				//else{
				//copy new file there
				string TheFile = imageStore.FolderPath + "CB-Sheet" + DateTime.Now.ToFileTime() + ".doc";
				try {
					//File.Copy(@"\\server\opendental\data\CB-Sheet.odt", TheFile);
					File.Copy(((PrefB.GetString("DocPath")) + @"\"
						+ "CB-Sheet.doc"), TheFile);
				}
				catch {
				}//}
				//string TheFile = patFolder + @"~C&B Sheet.doc";
				try {
					Process.Start(ProgName, TheFile);
				}
				catch {
				}
				this.Cursor = Cursors.Default;
			}
			else {
				MessageBox.Show(Lan.g(this, "Cannot open the folder in which the images are stored. This is most likely because the images are stored in a database, which cannot be browsed."));
			}
		}

		private void butCameraPic_Click(object sender, EventArgs e) {
			Cursor = Cursors.AppStarting;
			string ProgName = @"C:\WINDOWS\explorer.exe";
			Process.Start(ProgName, (@"\\server/My Documents/~Camera Pictures"));
			Cursor = Cursors.Default;

		}

		private void butOpenFolder_Click(object sender, EventArgs e) {
			if(imageStore.OpenFolderSupported) {
				string ProgName = @"C:\WINDOWS\explorer.exe";
				Process.Start(ProgName, imageStore.FolderPath);
			}
			else {
				MessageBox.Show(Lan.g(this, "Cannot open the folder in which the images are stored. This is most likely because the images are stored in a database, which cannot be browsed."));
			}
		}

	}
}
