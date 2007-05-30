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
		private Topaz.SigPlusNET sigBoxTopaz;
		///<summary>Starts out as false. It's only used when repainting the toolbar, not to test mode.</summary>
		private bool IsCropMode;
		///<summary>The only reason this is public is for NewPatientForm.com functionality.</summary>
		public Patient PatCur;
		private Family FamCur;
		///<summary>When dragging on Picturebox, this is the starting point in PictureBox coordinates.</summary>
		private Point MouseDownOrigin;
		private bool MouseIsDown;
		///<summary>The path to the patient folder, including the letter folder, and ending with \.  It's public for NewPatientForm.com functionality.</summary>
		public string patFolder;
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
		DeviceControl xRayImageController=null;
		///<summary>Thread to handle updating the graphical image to the screen when the current document is an image.</summary>
		Thread myThread=null;
		ApplySettings InvalidatedSettingsFlag;
		///<summary>Used as a thread-safe communication device between the main and worker threads.</summary>
		EventWaitHandle settingHandle=new EventWaitHandle(false,EventResetMode.AutoReset);
		///<summary>Edited by the main thread to reflect selection changes. Read by worker thread.</summary>
		Document settingDoc=null;
		///<summary>Set by the main thread and read by the image worker thread. Specifies which image processing tasks are to be performed by the worker thread.</summary>
		ApplySettings settingFlags=ApplySettings.NONE;
		///<summary>Used to perform mouse selections in the TreeDocuments list.</summary>
		string treeIdNumDown="";
		///<summary>Used to keep track of the old document selection by document number (the only guaranteed unique idenifier). This is to help the code be compatible with both Windows and MONO.</summary>
		string oldSelectionIdentifier="";
		///<summary>Used for Invoke() calls in RenderCurrentImage() to safely handle multi-thread access to the picture box.</summary>
		delegate void RenderImageCallback(Document docCopy,int originalWidth,int originalHeight,float zoom,PointF translation);
		///<summary>Used to safe-guard against multi-threading issues when an image capture is completed.</summary>
		delegate void CaptureCompleteCallback(object sender,EventArgs e);
		///<summary>Keeps track of the document settings for the currently selected document or mount document.</summary>
		Document selectionDoc=new Document();
		///<summary>Keeps track of the currently selected image within the list of currently loaded images.</summary>
		int hotDocument=0;

		#endregion

		///<summary></summary>
		public ContrDocs(){
			InitializeComponent();
			//The context menu causes strange bugs in MONO when performing selections on the tree.
			//Perhaps when MONO is more developed we can remove this check.
			if(Environment.OSVersion.Platform==PlatformID.Unix){
				TreeDocuments.ContextMenu=null;
			}else{
				sigBoxTopaz=new Topaz.SigPlusNET();
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
			this.xRayImageController.OnCaptureBegin+=new System.EventHandler(this.OnCaptureBegin);
			this.xRayImageController.OnCaptureComplete+=new System.EventHandler(this.OnCaptureComplete);
			this.xRayImageController.OnCaptureAbort+=new System.EventHandler(this.OnCaptureAborted);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
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
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.paintTools = new OpenDental.UI.ODToolBar();
			this.brightnessContrastSlider = new OpenDental.UI.ContrWindowingSlider();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			this.panelNote.SuspendLayout();
			this.SuspendLayout();
			// 
			// TreeDocuments
			// 
			this.TreeDocuments.ContextMenu = this.contextTree;
			this.TreeDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.TreeDocuments.FullRowSelect = true;
			this.TreeDocuments.HideSelection = false;
			this.TreeDocuments.ImageIndex = 2;
			this.TreeDocuments.ImageList = this.imageListTree;
			this.TreeDocuments.Indent = 20;
			this.TreeDocuments.Location = new System.Drawing.Point(0,33);
			this.TreeDocuments.Name = "TreeDocuments";
			this.TreeDocuments.SelectedImageIndex = 2;
			this.TreeDocuments.Size = new System.Drawing.Size(228,519);
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
			this.imageListTree.Images.SetKeyName(0,"");
			this.imageListTree.Images.SetKeyName(1,"");
			this.imageListTree.Images.SetKeyName(2,"");
			this.imageListTree.Images.SetKeyName(3,"");
			this.imageListTree.Images.SetKeyName(4,"");
			this.imageListTree.Images.SetKeyName(5,"");
			this.imageListTree.Images.SetKeyName(6,"");
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
			this.imageListTools2.Images.SetKeyName(0,"Pat.gif");
			this.imageListTools2.Images.SetKeyName(1,"print.gif");
			this.imageListTools2.Images.SetKeyName(2,"deleteX.gif");
			this.imageListTools2.Images.SetKeyName(3,"info.gif");
			this.imageListTools2.Images.SetKeyName(4,"scan.gif");
			this.imageListTools2.Images.SetKeyName(5,"");
			this.imageListTools2.Images.SetKeyName(6,"");
			this.imageListTools2.Images.SetKeyName(7,"");
			this.imageListTools2.Images.SetKeyName(8,"");
			this.imageListTools2.Images.SetKeyName(9,"");
			this.imageListTools2.Images.SetKeyName(10,"");
			this.imageListTools2.Images.SetKeyName(11,"");
			this.imageListTools2.Images.SetKeyName(12,"");
			this.imageListTools2.Images.SetKeyName(13,"");
			this.imageListTools2.Images.SetKeyName(14,"");
			this.imageListTools2.Images.SetKeyName(15,"");
			this.imageListTools2.Images.SetKeyName(16,"");
			this.imageListTools2.Images.SetKeyName(17,"copy.gif");
			// 
			// PictureBox1
			// 
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBox1.InitialImage = null;
			this.PictureBox1.Location = new System.Drawing.Point(233,63);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(703,370);
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
			this.panelNote.Location = new System.Drawing.Point(234,489);
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
			this.labelInvalidSig.Location = new System.Drawing.Point(414,35);
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
			this.sigBox.Size = new System.Drawing.Size(394,91);
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
			this.textNote.Size = new System.Drawing.Size(302,91);
			this.textNote.TabIndex = 0;
			this.textNote.DoubleClick += new System.EventHandler(this.textNote_DoubleClick);
			this.textNote.MouseHover += new System.EventHandler(this.textNote_MouseHover);
			// 
			// paintToolsUnderline
			// 
			this.paintToolsUnderline.BackColor = System.Drawing.SystemColors.ControlDark;
			this.paintToolsUnderline.Location = new System.Drawing.Point(236,56);
			this.paintToolsUnderline.Name = "paintToolsUnderline";
			this.paintToolsUnderline.Size = new System.Drawing.Size(702,2);
			this.paintToolsUnderline.TabIndex = 15;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Location = new System.Drawing.Point(233,29);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(2,29);
			this.panel1.TabIndex = 16;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListTools2;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939,29);
			this.ToolBarMain.TabIndex = 10;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// paintTools
			// 
			this.paintTools.ImageList = this.imageListTools2;
			this.paintTools.Location = new System.Drawing.Point(440,28);
			this.paintTools.Name = "paintTools";
			this.paintTools.Size = new System.Drawing.Size(499,29);
			this.paintTools.TabIndex = 14;
			this.paintTools.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.paintTools_ButtonClick);
			// 
			// brightnessContrastSlider
			// 
			this.brightnessContrastSlider.Location = new System.Drawing.Point(240,36);
			this.brightnessContrastSlider.MaxVal = 255;
			this.brightnessContrastSlider.MinVal = 0;
			this.brightnessContrastSlider.Name = "brightnessContrastSlider";
			this.brightnessContrastSlider.Size = new System.Drawing.Size(194,14);
			this.brightnessContrastSlider.TabIndex = 12;
			this.brightnessContrastSlider.Text = "contrWindowingSlider1";
			this.brightnessContrastSlider.Scroll += new System.EventHandler(this.brightnessContrastSlider_Scroll);
			this.brightnessContrastSlider.ScrollComplete += new System.EventHandler(this.brightnessContrastSlider_ScrollComplete);
			// 
			// ContrDocs
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.paintToolsUnderline);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.paintTools);
			this.Controls.Add(this.brightnessContrastSlider);
			this.Controls.Add(this.panelNote);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.TreeDocuments);
			this.Name = "ContrDocs";
			this.Size = new System.Drawing.Size(939,585);
			this.Resize += new System.EventHandler(this.ContrDocs_Resize);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			this.panelNote.ResumeLayout(false);
			this.panelNote.PerformLayout();
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
			panelNote.Visible=(((doc.Note!=null && doc.Note!="") || (doc.Signature!=null && doc.Signature!="")) && 
				(Environment.OSVersion.Platform!=PlatformID.Unix || !doc.SigIsTopaz));
		}

		///<summary></summary>
		public void InstantClasses(){
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
			if(IsCropMode){
				button.Pushed=true;
			}
			paintTools.Buttons.Add(button);
			button=new ODToolBarButton("",10,Lan.g(this,"Hand Tool"),"Hand");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			if(!IsCropMode){
				button.Pushed=true;
			}
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
			PatCur=null;
			//Cancel current image capture by manually untoggling the capture button.
			ToolBarMain.Buttons["Capture"].Pushed=false;
			OnCapture_Click();
		}

		///<summary>This is public for NewPatientForm functionality.</summary>
  	public void RefreshModuleData(int patNum){
			SelectTreeNode(null);//Clear selection and image and reset visibilities.
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			if(ParentForm != null){ //Added so NewPatientform can have access without showing
				ParentForm.Text=Patients.GetMainTitle(PatCur);
			}
			if(PatCur.ImageFolder==""){//creates new folder for patient if none present
				string name=PatCur.LName+PatCur.FName;
				string folder="";
				for(int i=0;i<name.Length;i++){
					if(Char.IsLetter(name,i)){
						folder+=name.Substring(i,1);
					}
				}
				folder+=PatCur.PatNum.ToString();//ensures unique name
				try{
					Patient PatOld=PatCur.Copy();
					PatCur.ImageFolder=folder;
					patFolder=ODFileUtils.CombinePaths(new string[] {	FormPath.GetPreferredImagePath(),
																														PatCur.ImageFolder.Substring(0,1).ToUpper(),
																														PatCur.ImageFolder});
					Directory.CreateDirectory(patFolder);
					Patients.Update(PatCur,PatOld);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					return;
				}
			}
			else{//patient folder already created once
				patFolder=ODFileUtils.CombinePaths(new string [] {	FormPath.GetPreferredImagePath(),
																														PatCur.ImageFolder.Substring(0,1).ToUpper(),
																														PatCur.ImageFolder});
			}
			if(!Directory.Exists(patFolder)){//this makes it more resiliant and allows copies
					//of the opendentaldata folder to be used in read-only situations.
				try{
					Directory.CreateDirectory(patFolder);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Error.  Could not create folder for patient. "));
					return;
				}
			}
			//now find all files in the patient folder that are not in the db and add them
			DirectoryInfo di=new DirectoryInfo(patFolder);
			FileInfo[] fiList=di.GetFiles();
			string[] fileList=new string[fiList.Length];
			for(int i=0;i<fileList.Length;i++){
				fileList[i]=fiList[i].Name;
			}
			int countAdded=Documents.InsertMissing(PatCur,fileList);
			if(countAdded>0){
				MessageBox.Show(countAdded.ToString()+" documents found and added to the first category.");
			}
			//it will refresh in FillDocList																					 
		}

		private void RefreshModuleScreen(){
			ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(this.Enabled && PatCur!=null){
				//ParentForm.Text=((Pref)PrefB.HList["MainWindowTitle"]).ValueString+" - "
				//	+PatCur.GetNameLF();
				ToolBarMain.Buttons["Print"].Enabled=true;
				ToolBarMain.Buttons["Delete"].Enabled=true;
				ToolBarMain.Buttons["Info"].Enabled=true;
				ToolBarMain.Buttons["Import"].Enabled=true;
				ToolBarMain.Buttons["ScanDoc"].Enabled=true;
				ToolBarMain.Buttons["ScanXRay"].Enabled=true;
				ToolBarMain.Buttons["ScanPhoto"].Enabled=true;
				ToolBarMain.Buttons["Copy"].Enabled=true;
				ToolBarMain.Buttons["Paste"].Enabled=true;
				ToolBarMain.Buttons["Forms"].Enabled=true;
				ToolBarMain.Buttons["Capture"].Enabled=true;
				paintTools.Buttons["Crop"].Enabled=true;
				paintTools.Buttons["Hand"].Enabled=true;
				paintTools.Buttons["ZoomIn"].Enabled=true;
				paintTools.Buttons["ZoomOut"].Enabled=true;
				paintTools.Buttons["Flip"].Enabled=true;
				paintTools.Buttons["RotateR"].Enabled=true;
				paintTools.Buttons["RotateL"].Enabled=true;
			}
			else{
				//ParentForm.Text=((Pref)PrefB.HList["MainWindowTitle"]).ValueString;
				//PatCur=new Patient();
				ToolBarMain.Buttons["Print"].Enabled=false;
				ToolBarMain.Buttons["Delete"].Enabled=false;
				ToolBarMain.Buttons["Info"].Enabled=false;
				ToolBarMain.Buttons["Import"].Enabled=false;
				ToolBarMain.Buttons["ScanDoc"].Enabled=false;
				ToolBarMain.Buttons["ScanXRay"].Enabled=false;
				ToolBarMain.Buttons["ScanPhoto"].Enabled=false;
				ToolBarMain.Buttons["Copy"].Enabled=false;
				ToolBarMain.Buttons["Paste"].Enabled=false;
				ToolBarMain.Buttons["Forms"].Enabled=false;
				ToolBarMain.Buttons["Capture"].Enabled=false;
				paintTools.Buttons["Crop"].Enabled=false;
				paintTools.Buttons["Hand"].Enabled=false;
				paintTools.Buttons["ZoomIn"].Enabled=false;
				paintTools.Buttons["ZoomOut"].Enabled=false;
				paintTools.Buttons["Flip"].Enabled=false;
				paintTools.Buttons["RotateR"].Enabled=false;
				paintTools.Buttons["RotateL"].Enabled=false;
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

		///<summary>Selection doesn't only happen by the tree and mouse clicks, but can also happen by automatic processes, such as image import, image paste, etc...</summary>
		private void SelectTreeNode(TreeNode node){
			//Select the node always, but perform additional tasks when necessary (i.e. load an image, or mount).
			TreeDocuments.SelectedNode=node;
			TreeDocuments.Invalidate();
			selectionDoc=new Document();
			//We only perform a load if the new selection is different than the old selection.
			string identifier=GetNodeIdentifier(node);
			if(identifier==oldSelectionIdentifier){
				return;
			}
			oldSelectionIdentifier=identifier;
			//Disable all paint tools until the currently selected node is loaded properly in the picture box.
			paintTools.Enabled=false;
			//Must disable the brightnessContrastSlider seperately, since it is not actually in the paintTools control.
			brightnessContrastSlider.Enabled=false;
			//Stop any current image processing. This will avoid having the renderImage set to a valid image after
			//the current image has been erased. This will also avoid concurrent access to the the currently loaded images by
			//the main and worker threads.
			EraseCurrentImage();
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
					//TODO: set selection doc to orginal image 0 (if available).
					//Creates a complete initial mount image. No need to call invalidate until changes are made to the mount later.
					MountItem[] mountItems=MountItems.GetItemsForMount(mountNum);
					Document[] documents=Documents.GetDocumentsForMountItems(mountItems);
					hotDocument=mountItems.Length;
					if(documents.Length>hotDocument){
						selectionDoc=documents[hotDocument];
					}
					currentImages=GetDocumentImages(documents,patFolder);
					renderImage=CreateMountImage(Mounts.GetByNum(mountNum),currentImages,mountItems,documents,hotDocument);
				}else{//This is a document node.
					//Reload the doc from the db. We don't just keep reusing the tree data, because it will become more and 
					//more stail with age if the program is left open in the image module for long periods of time.
					selectionDoc=Documents.GetByNum(docNum);
					hotDocument=0;
					currentImages=GetDocumentImages(new Document[] {selectionDoc},patFolder);
				}
				//Setup toolbars and factors only if an image is currently selected.
				if(currentImages.Length>hotDocument && currentImages[hotDocument]!=null) {
					if(selectionDoc.WindowingMax==0) {
						//The document brightness/contrast settings have never been set. By default, we use settings
						//which do not alter the original image.
						brightnessContrastSlider.MinVal=0;
						brightnessContrastSlider.MaxVal=255;
					}else{
						brightnessContrastSlider.MinVal=selectionDoc.WindowingMin;
						brightnessContrastSlider.MaxVal=selectionDoc.WindowingMax;
					}
					paintTools.Enabled=true;								//Only allow painting tools to be used when a valid image has been loaded.
					brightnessContrastSlider.Enabled=true;	//The brightnessContrastSlider is not actually part of the paintTools
																									//toolbar, and so it must be enabled or disabled seperately.
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
					float zoom;
					int zoomLevel;
					float zoomFactor;
					PointF trans;
					ReloadZoomTransCrop(renderImage.Width,renderImage.Height,new Document(),
						new Rectangle(0,0,PictureBox1.Width,PictureBox1.Height),out zoom,out zoomLevel,out zoomFactor,out trans);
					RenderCurrentImage(new Document(),renderImage.Width,renderImage.Height,zoom,trans);
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

		/// <summary>Refreshes list from db, then fills the treeview.  Set keepDoc to true in order to keep the current selection active.</summary>
		private void FillDocList(bool keepDoc){
			string selectionId=(keepDoc?GetNodeIdentifier(TreeDocuments.SelectedNode):"");
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
			if(TreeDocuments.SelectedNode==null || TreeDocuments.SelectedNode.Tag==null) {
				MsgBox.Show(this,"No item is currently selected");
				return;//No current selection, or some kind of internal error somehow.
			}
			if(TreeDocuments.SelectedNode.Parent==null){
				MsgBox.Show(this,"Cannot delete folders");
				return;
			}
			if(verbose){
				if(!MsgBox.Show(this,true,"Delete?")){
					return;
				}
			}
			selectionDoc=new Document();
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			Document[] docs;
			if(mountNum!=0){//This is a mount object.
				//Delete the mount object.
				Mount mount=Mounts.GetByNum(mountNum);
				Mounts.Delete(mount);
				//Delete the mount items attached to the mount object.
				MountItem[] mountItems=MountItems.GetItemsForMount(mountNum);
				docs=Documents.GetDocumentsForMountItems(mountItems);
				for(int i=0;i<mountItems.Length;i++){
					MountItems.Delete(mountItems[i]);
				}
			}else{
				docs=new Document[1];
				docs[0]=Documents.GetByNum(docNum);
			}
			//Delete all documents involved in deleting this object.
			for(int i=0;i<docs.Length;i++){
				try{
					SelectTreeNode(null);//Release access to current image so it may be properly deleted.
					string srcFile=ODFileUtils.CombinePaths(patFolder,docs[i].FileName);
					if(File.Exists(srcFile)) {
						File.Delete(srcFile);
					}else if(verbose){
						MsgBox.Show(this,"File could not be found. It may have already been deleted.");
					}
				}catch{
					if(verbose) {
						MsgBox.Show(this,"Could not delete file. It may be in use elsewhere, or may have already been deleted.");
					}
				}
				Documents.Delete(docs[i]);
			}
			FillDocList(false);
		}

		private void OnSign_Click(){
			if(TreeDocuments.SelectedNode==null ||				//No selection
				TreeDocuments.SelectedNode.Tag==null ||			//Internal error
				TreeDocuments.SelectedNode.Parent==null){		//This is a folder node.
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0){//Is this a mount object?
				MsgBox.Show(this,"Cannot sign mount objects");
				return;
			}
			//Show the underlying panel note box while the signature is being filled.
			panelNote.Visible=true;
			ResizeAll();
			//Display the document signature form.
			FormDocSign docSignForm=new FormDocSign(selectionDoc,patFolder);//Updates our local document and saves changes to db also.
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
				sigBoxTopaz.SetTabletState(0);
			}
			//A machine running Unix will have selectionDoc.SigIsTopaz set to false here, because the visibility of the panelNote
			//will be set to false in the case of Unix and SigIsTopaz. Therefore, the else part of this if-else clause is always
			//run on Unix systems.
			if(selectionDoc.SigIsTopaz) {
				if(selectionDoc.Signature!=null && selectionDoc.Signature!="") {
					sigBox.Visible=false;
					sigBoxTopaz.Visible=true;
					sigBoxTopaz.ClearTablet();
					sigBoxTopaz.SetSigCompressionMode(0);
					sigBoxTopaz.SetEncryptionMode(0);
					sigBoxTopaz.SetKeyString(GetHashString(selectionDoc));
					sigBoxTopaz.SetEncryptionMode(2);//high encryption
					sigBoxTopaz.SetSigCompressionMode(2);//high compression
					sigBoxTopaz.SetSigString(selectionDoc.Signature);
					if(sigBoxTopaz.NumberOfTabletPoints()==0) {
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
			//the key data is the bytes of the file, concatenated with the bytes of the note.
			byte[] textbytes;
			if(doc.Note==null) {
				textbytes=Encoding.UTF8.GetBytes("");
			}
			else {
				textbytes=Encoding.UTF8.GetBytes(doc.Note);
			}
			string path=ODFileUtils.CombinePaths(patFolder,doc.FileName);
			if(!File.Exists(path)) {
				return "";
			}
			FileStream fs=new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read);
			int fileLength=(int)fs.Length;
			byte[] buffer=new byte[fileLength+textbytes.Length];
			fs.Read(buffer,0,fileLength);
			fs.Close();
			Array.Copy(textbytes,0,buffer,fileLength,textbytes.Length);
			HashAlgorithm algorithm=MD5.Create();
			byte[] hash=algorithm.ComputeHash(buffer);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
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
			Document doc=new Document();
			if(scanType=="xray"){
				doc.ImgType=ImageType.Radiograph;
			}else if(scanType=="photo"){
				doc.ImgType=ImageType.Photo;
			}else{//Assume document
				doc.ImgType=ImageType.Document;
			}
			doc.FileName=".jpg";
			doc.DateCreated=DateTime.Today;
			doc.PatNum=PatCur.PatNum;		
			doc.DocCategory=GetCurrentCategory();
			Documents.Insert(doc,PatCur);//creates filename and saves to db
			bool saved=true;
			try{//Create corresponding image file.
				ImageCodecInfo myImageCodecInfo;
				ImageCodecInfo[] encoders;
				encoders=ImageCodecInfo.GetImageEncoders();
				myImageCodecInfo=null;
				for(int j=0;j<encoders.Length;j++) {
					if(encoders[j].MimeType=="image/jpeg")
						myImageCodecInfo=encoders[j];
				}
				System.Drawing.Imaging.Encoder myEncoder=System.Drawing.Imaging.Encoder.Quality;
				EncoderParameters myEncoderParameters=new EncoderParameters(1);
				long qualityL=0;
				if(scanType=="xray"){
					qualityL=Convert.ToInt64(((Pref)PrefB.HList["ScannerCompressionRadiographs"]).ValueString);
				}else if(scanType=="photo"){
					qualityL=Convert.ToInt64(((Pref)PrefB.HList["ScannerCompressionPhotos"]).ValueString);
				}else{//Assume document
					//Possible values 0-100?
					qualityL=(long)Convert.ToInt32(((Pref)PrefB.HList["ScannerCompression"]).ValueString);
				}
				EncoderParameter myEncoderParameter=new EncoderParameter(myEncoder,qualityL);
				myEncoderParameters.Param[0]=myEncoderParameter;
				//AutoCrop()?
				scannedImage.Save(ODFileUtils.CombinePaths(patFolder,doc.FileName),myImageCodecInfo,myEncoderParameters);
			}catch{
				saved=false;
				MessageBox.Show(Lan.g(this,"Unable to save document."));
				Documents.Delete(doc);
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
				openFileDialog.FileName=fileNames[i];
				doc=new Document();
				//Document.Insert will use this extension when naming:
				doc.FileName=Path.GetExtension(openFileDialog.FileName);
				doc.DateCreated=DateTime.Today;
				doc.PatNum=PatCur.PatNum;
				doc.ImgType=HasImageExtension(doc.FileName)?ImageType.Photo:ImageType.Document;
				doc.DocCategory=GetCurrentCategory();
				Documents.Insert(doc,PatCur);//this assigns a filename and saves to db
				bool copied=true;
				try{
					File.Copy(openFileDialog.FileName,ODFileUtils.CombinePaths(patFolder,doc.FileName));
				}catch(Exception ex){
					MessageBox.Show(Lan.g(this,"Unable to copy file, May be in use: ")+ex.Message+": "+openFileDialog.FileName);
					Documents.Delete(doc);
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
				copyImage=renderImage;
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
			Document doc=new Document();
			doc.FileName=".jpg";
			doc.DateCreated=DateTime.Today;
			doc.DocCategory=GetCurrentCategory();
			doc.PatNum=PatCur.PatNum;
			doc.ImgType=ImageType.Photo;
			Documents.Insert(doc,PatCur);//this assigns a filename and saves to db
			string srcFile=ODFileUtils.CombinePaths(patFolder,doc.FileName);
			try{
				Bitmap pasteImage=(Bitmap)clipboard.GetData(DataFormats.Bitmap);
				pasteImage.Save(srcFile);
			}catch{
				MessageBox.Show(Lan.g(this,"Error saving document."));
				Documents.Delete(doc);
				return;
			}
			FillDocList(false);
			SelectTreeNode(GetNodeById(MakeIdentifier(doc.DocNum.ToString(),"0")));
			FormDocInfo formD=new FormDocInfo(PatCur,doc,GetCurrentFolderName(TreeDocuments.SelectedNode));
			formD.ShowDialog();
			if(formD.DialogResult!=DialogResult.OK){
				DeleteSelection(false);
				paintTools.Enabled=false;//Force image to clear from screen.
				brightnessContrastSlider.Enabled=false;
			}else{
				FillDocList(true);
				paintTools.Enabled=true;//Allow image to remain on screen.
				brightnessContrastSlider.Enabled=true;
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
				printImage=prePrintImage;	//Just print the mount as is, since the mount is always in the same orientation, and
																	//the images it houses are already flipped and rotated to generate the render image.
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
			string fileName=ODFileUtils.CombinePaths(new string[] {FormPath.GetPreferredImagePath(),
				"Forms",((MenuItem)sender).Text});
			if(!File.Exists(fileName)){
				MessageBox.Show(Lan.g(this,"Could not find file: ")+fileName);
				return;
			}
			Document doc=new Document();
			doc.FileName=Path.GetExtension(fileName);
			doc.DateCreated=DateTime.Today;
			doc.DocCategory=GetCurrentCategory();
			doc.PatNum=PatCur.PatNum;
			doc.ImgType=ImageType.Document;
			Documents.Insert(doc,PatCur);//this assigns a filename and saves to db
			bool copied=true;
			try{
				File.Copy(fileName,ODFileUtils.CombinePaths(patFolder,doc.FileName));
			}catch{
				MessageBox.Show(Lan.g(this,"Unable to copy file. May be in use: ")+fileName);
				copied=false;
				Documents.Delete(doc);
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

		///<Summary>Invalidates some or all of the image settings.  This will cause those settings to be recalculated, either immediately, or when the current ApplySettings thread is finished.  If supplied settings is ApplySettings.NONE, then that part will be skipped.</Summary>
		private void InvalidateSettings(ApplySettings settings, bool reloadZoomTransCrop){
			//Do not allow image rendering when the paint tools are disabled. This will disable the display image when a folder or non-image document is selected, or when no document is currently selected. The paintTools.Enabled boolean is controlled in SelectTreeNode() and is set to true only if a valid image is currently being displayed.
			if(!paintTools.Enabled || TreeDocuments.SelectedNode==null || 
				TreeDocuments.SelectedNode.Tag==null || TreeDocuments.SelectedNode.Parent==null){
				EraseCurrentImage();
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0){//Settings invalidated for a mount object.
				return;//TODO:??????
			}
			InvalidatedSettingsFlag|=settings;
			if(reloadZoomTransCrop){
				//Reloading the image settings only happens when a new image is selected, pasted, scanned, etc...
				//Therefore, the is no need for any current image processing anymore (it would be on a stail image).
				KillMyImageThreads();
				ReloadZoomTransCrop(curImageWidths[0],curImageHeights[0],selectionDoc,
					new Rectangle(0,0,PictureBox1.Width,PictureBox1.Height),
					out imageZoom,out zoomLevel,out zoomFactor,out imageTranslation);
				cropTangle=new Rectangle(0,0,-1,-1);
			}
			//selectionDoc is an individual document instance. Assigning a new document to settingDoc here does not 
			//negatively effect our image application thread, because the thread either will keep its current 
			//reference to the old document, or will apply the settings with this newly assigned document. In either
			//case, the output is either what we expected originally, or is a more accurate image for more recent 
			//settings. We lock here so that we are sure that the resulting document and setting tuple represent
			//a single point in time.
			lock(settingHandle){//Does not actually lock the settingHandle object.
				settingDoc=selectionDoc.Copy();
				settingFlags=InvalidatedSettingsFlag;
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
					ApplySettings applySettings;
					lock(settingHandle){//Does not actually lock the settingHandle object.
						curDocCopy=settingDoc;
						applySettings=settingFlags;
					}
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
				return;
			}
			Document nodeDoc=Documents.GetByNum(docNum);
			string srcFileName=ODFileUtils.CombinePaths(patFolder,nodeDoc.FileName);
			string ext=Path.GetExtension(srcFileName).ToLower();
			if(ext==".jpg" || ext==".jpeg" || ext==".gif") {
				return;
			}
			//We allow anything which ends with a different extention to be viewed in the windows fax viewer.
			//Specifically, multi-page faxes can be viewed more easily by one of our customers using the fax
			//viewer. On Unix systems, it is imagined that an equivalent viewer will launch to allow the image
			//to be viewed.
			try{
				Process.Start(srcFileName);
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}			
		}

		private void OnInfo_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0) {
				return;
			}
			//The FormDocInfo object updates the selectionDoc and stores the changes in the database as well.
			FormDocInfo formDocInfo2=new FormDocInfo(PatCur,selectionDoc,GetCurrentFolderName(TreeDocuments.SelectedNode));
			formDocInfo2.ShowDialog();
			if(formDocInfo2.DialogResult!=DialogResult.OK) {
				return;
			}
			FillDocList(true);
		}

		private void OnZoomIn_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)==""){
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0){
				return;
			}
			zoomLevel++;
			PointF c=new PointF(PictureBox1.ClientRectangle.Width/2.0f,PictureBox1.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-imageTranslation.X,c.Y-imageTranslation.Y);
			imageTranslation=new PointF(imageTranslation.X-p.X,imageTranslation.Y-p.Y);
			zoomFactor=(float)Math.Pow(2,zoomLevel);
			InvalidateSettings(ApplySettings.NONE,false);//Refresh display.
		}

		private void OnZoomOut_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0) {
				return;
			}
			zoomLevel--;
			PointF c=new PointF(PictureBox1.ClientRectangle.Width/2.0f,PictureBox1.ClientRectangle.Height/2.0f);
			PointF p=new PointF(c.X-imageTranslation.X,c.Y-imageTranslation.Y);
			imageTranslation=new PointF(imageTranslation.X+p.X/2.0f,imageTranslation.Y+p.Y/2.0f);
			zoomFactor=(float)Math.Pow(2,zoomLevel);
			InvalidateSettings(ApplySettings.NONE,false);//Refresh display.
		}

		private void DeleteThumbnailImage(Document doc){
			string thumbnailFile=ODFileUtils.CombinePaths(new string[] {patFolder,"Thumbnails",doc.FileName});
			if(File.Exists(thumbnailFile)) {
				try {
					File.Delete(thumbnailFile);
				}
				catch {
					//Two users *might* edit the same image at the same time, so the image might already be deleted.
				}
			}
		}

		private void OnFlip_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0) {
				return;//TODO:
			}else{
				selectionDoc.IsFlipped=!selectionDoc.IsFlipped;
				Documents.Update(selectionDoc);
				DeleteThumbnailImage(selectionDoc);
			}
			InvalidateSettings(ApplySettings.FLIP,false);//Refresh display.
		}

		private void OnRotateL_Click() {
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0) {
				return;//TODO:
			}else{
				selectionDoc.DegreesRotated-=90;
				while(selectionDoc.DegreesRotated<0) {
					selectionDoc.DegreesRotated+=360;
				}
				Documents.Update(selectionDoc);
				DeleteThumbnailImage(selectionDoc);
			}
			InvalidateSettings(ApplySettings.ROTATE,false);//Refresh display.
		}

		private void OnRotateR_Click(){
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0) {
				return;//TODO:
			}else{
				selectionDoc.DegreesRotated=(selectionDoc.DegreesRotated+90)%360;
				Documents.Update(selectionDoc);
				DeleteThumbnailImage(selectionDoc);
			}
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
			MouseIsDown=false;
			if(!paintTools.Buttons["Crop"].Pushed) {//not Crop Mode
				return;
			}
			if(cropTangle.Width<=0 || cropTangle.Height<=0) {
				return;
			}
			if(GetNodeIdentifier(TreeDocuments.SelectedNode)=="") {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0){
				return;
			}
			if(!MsgBox.Show(this,true,"Crop to Rectangle?")){
				cropTangle=new Rectangle(0,0,-1,-1);
				InvalidateSettings(ApplySettings.NONE,false);//Refresh display (since message box was covering).
				return;
			}
			PointF cropPoint1=ScreenPointToUnalteredDocumentPoint(cropTangle.Location,selectionDoc,
				curImageWidths[0],curImageHeights[0],imageZoom*zoomFactor,imageTranslation);
			PointF cropPoint2=ScreenPointToUnalteredDocumentPoint(new Point(cropTangle.Location.X+cropTangle.Width,
				cropTangle.Location.Y+cropTangle.Height),selectionDoc,curImageWidths[0],curImageHeights[0],
				imageZoom*zoomFactor,imageTranslation);
			//cropPoint1 and cropPoint2 together define an axis-aligned bounding area, or our crop area. 
			//However, the two points have no guaranteed order, thus we must sort them using Math.Min.
			Rectangle rawCropRect=new Rectangle(
				(int)Math.Round((decimal)Math.Min(cropPoint1.X,cropPoint2.X)),
				(int)Math.Round((decimal)Math.Min(cropPoint1.Y,cropPoint2.Y)),
				(int)Math.Ceiling((decimal)Math.Abs(cropPoint1.X-cropPoint2.X)),
				(int)Math.Ceiling((decimal)Math.Abs(cropPoint1.Y-cropPoint2.Y)));
			//We must also intersect the old cropping rectangle with the new cropping rectangle, so that part of
			//the image does not come back as a result of multiple crops.
			Rectangle oldCropRect=DocCropRect(selectionDoc,curImageWidths[0],curImageHeights[0]);
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
			Rectangle prevCropRect=DocCropRect(selectionDoc,curImageWidths[0],curImageHeights[0]);
			selectionDoc.CropX=(int)finalCropRect[0];
			selectionDoc.CropY=(int)finalCropRect[1];
			selectionDoc.CropW=(int)Math.Ceiling(finalCropRect[2]);
			selectionDoc.CropH=(int)Math.Ceiling(finalCropRect[3]);
			Documents.Update(selectionDoc);
			DeleteThumbnailImage(selectionDoc);
			Rectangle newCropRect=DocCropRect(selectionDoc,curImageWidths[0],curImageHeights[0]);
			//Update the location of the image so that the cropped portion of the image does not move in screen space.
			PointF prevCropCenter=new PointF(prevCropRect.X+prevCropRect.Width/2.0f,prevCropRect.Y+prevCropRect.Height/2.0f);
			PointF newCropCenter=new PointF(newCropRect.X+newCropRect.Width/2.0f,newCropRect.Y+newCropRect.Height/2.0f);
			PointF[] imageCropCenters=new PointF[] {
				prevCropCenter,
				newCropCenter
			};
			Matrix docMat=GetDocumentFlippedRotatedMatrix(selectionDoc);
			docMat.Scale(imageZoom*zoomFactor,imageZoom*zoomFactor);
			docMat.TransformPoints(imageCropCenters);
			imageTranslation=new PointF(imageTranslation.X+(imageCropCenters[1].X-imageCropCenters[0].X),
																	imageTranslation.Y+(imageCropCenters[1].Y-imageCropCenters[0].Y));
			cropTangle=new Rectangle(0,0,-1,-1);
			InvalidateSettings(ApplySettings.CROP,false);
		}

		private void brightnessContrastSlider_Scroll(object sender,EventArgs e){
			selectionDoc.WindowingMin=brightnessContrastSlider.MinVal;
			selectionDoc.WindowingMax=brightnessContrastSlider.MaxVal;
			InvalidateSettings(ApplySettings.COLORFUNCTION,false);
		}

		private void brightnessContrastSlider_ScrollComplete(object sender,EventArgs e) {
			//The brightness/contrast slider is only enabled when a document or mount is selected, so 
			//we know we have a valid node here always.
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			int mountNum=Convert.ToInt32(obj["MountNum"].ToString());
			int docNum=Convert.ToInt32(obj["DocNum"].ToString());
			if(mountNum!=0){
				return;//TODO: do something here!
			}
			Documents.Update(selectionDoc);
			DeleteThumbnailImage(selectionDoc);
			InvalidateSettings(ApplySettings.COLORFUNCTION,false);
		}

		///<summary>Handles a change in selection of the xRay capture button.</summary>
		private void OnCapture_Click() {
			bool capture=ToolBarMain.Buttons["Capture"].Pushed;
			if(capture){
				//Show the user that they are performing an image capture by generating a new mount.
				Mount mount=new Mount();
				mount.DateCreated=DateTime.Today;
				mount.Description="unnamed capture";
				mount.DocCategory=DefB.Short[(int)DefCat.ImageCats][0].DefNum;//First category.
				mount.ImgType=ImageType.Mount;
				mount.PatNum=PatCur.PatNum;
				mount.MountNum=Mounts.Insert(mount);
				FillDocList(false);
				SelectTreeNode(GetNodeById(MakeIdentifier("0",mount.MountNum.ToString())));
				xRayImageController.CaptureXRay();
			}else{//The user unselected the image capture button, so cancel the current image capture.
				xRayImageController.KillXRayThread();//Stop current xRay capture and call OnCaptureAborted() when done.
			}
		}

		///<summary>Called when an xray capture begins, but after any previous image capture threads are killed.</summary>
		private void OnCaptureBegin(object sender,EventArgs e){
			if(ToolBarMain.InvokeRequired) {
				CaptureCompleteCallback c=new CaptureCompleteCallback(OnCaptureBegin);
				Invoke(c,new object[] { sender,e });
				return;
			}
			ToolBarMain.Buttons["Capture"].Pushed=true;
			ToolBarMain.Invalidate();
		}

		///<summary>Called on successful capture of image.</summary>
		private void OnCaptureComplete(object sender,EventArgs e) {
			if(this.InvokeRequired){
				CaptureCompleteCallback c=new CaptureCompleteCallback(OnCaptureComplete);
				Invoke(c,new object[] {sender,e});
				return;
			}
			//Since we are in the middle of an image capture, we know that a new mount will always be the current selection.
			DataRow mount=(DataRow)TreeDocuments.SelectedNode.Tag;
			//Create the mount item that corresponds to the new document about to be created.
			MountItem mountItem=new MountItem();
			mountItem.MountNum=Convert.ToInt32(mount["MountNum"].ToString());
			//Here we check to see which items are already in the db, in case an image capture session is being continued
			//(in case the user tabs out of the module and back in during the capture, or if power fails and they need
			//to restart their computer, for instance).
			MountItem[] existingMountItems=MountItems.GetItemsForMount(mountItem.MountNum);
			//Depending on the device being captured from, we need to rotate the images returned from the device by a certain
			//angle, and we need to place the returned images in a specific order within the mount slots. Later, we will allow
			//the user to define the rotations and slot orders, but for now, they will be hard-coded.
			bool[] takenOrdinals=new bool[4];
			for(int i=0;i<existingMountItems.Length;i++){
				takenOrdinals[existingMountItems[i].OrdinalPos]=true;
			}
			int rotationAngle=0;
			for(int i=0;i<takenOrdinals.Length;i++){
				if(!takenOrdinals[i]){
					mountItem.OrdinalPos=i;	//The new image ordinal position becomes the lowest available position (in case images
																	//were swapped around after a partial previous capture).
					switch(mountItem.OrdinalPos) {
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
					break;
				}
			}
			mountItem.MountItemNum=MountItems.Insert(mountItem);
			//Create the document object in the database for this mount image.
			string fileExtention=".bmp";//The file extention to save the greyscale image as.
			Bitmap capturedImage=xRayImageController.capturedImage;
			Document doc=new Document();
			doc.MountItemNum=mountItem.MountItemNum;
			doc.DegreesRotated=rotationAngle;
			doc.ImgType=ImageType.Radiograph;
			doc.FileName=fileExtention;
			doc.DateCreated=DateTime.Today;
			doc.PatNum=PatCur.PatNum;
			doc.DocCategory=GetCurrentCategory();
			doc.WindowingMin=PrefB.GetInt("ImageWindowingMin");
			doc.WindowingMax=PrefB.GetInt("ImageWindowingMax");
			Documents.Insert(doc,PatCur);//creates filename and saves to db
			try{
				capturedImage.Save(ODFileUtils.CombinePaths(patFolder,doc.FileName),ImageFormat.Bmp);
			}catch(Exception ex){
				Documents.Delete(doc);
				//Raise an exception in the capture thread.
				throw new Exception(Lan.g(this,"Unable to save captured XRay image as document")+": "+ex.Message);
			}
			if(existingMountItems.Length>=3) {//Mount is full.
				xRayImageController.KillXRayThread();
				return;
			}
			//This capture was successful. Keep capturing more images until the capture is manually aborted.
			//This will cause calls to OnCaptureAborted(), then OnCaptureBegin().
			xRayImageController.CaptureXRay();
		}

		///<summary>Called under any error circumstance resulting from the image capture process, or when one image capture is finishing and launching the next image capture.</summary>
		private void OnCaptureAborted(object sender,EventArgs e) {
			if(ToolBarMain.InvokeRequired) {
				CaptureCompleteCallback c=new CaptureCompleteCallback(OnCaptureAborted);
				Invoke(c,new object[] { sender,e });
				return;
			}
			ToolBarMain.Buttons["Capture"].Pushed=false;
			ToolBarMain.Invalidate();
		}

		///<summary>Kills ImageApplicationThread.  Disposes of both currentImages and renderImage.  Does not actually trigger a refresh of the Picturebox, though.</summary>
		private void EraseCurrentImage(){
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
		public static RectangleF CalcImageDims(int imageWidth,int imageHeight,Document doc) {
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
		public static PointF ScreenPointToUnalteredDocumentPoint(PointF screenLocation,Document doc,
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
		public static bool HasImageExtension(string fileName) {
			string ext=Path.GetExtension(fileName).ToLower();
			//The following supported bitmap types were found on a microsoft msdn page:
			return (ext==".jpg"||ext==".jpeg"||ext==".tga"||ext==".bmp"||ext==".tif"||
				ext==".tiff"||ext==".gif"||ext==".emf"||ext==".exif"||ext==".ico"||ext==".png"||ext==".wmf");
		}

		///<summary>Returns a matrix for the given document which represents flipping over the Y-axis before rotating. Of course, if doc.IsFlipped is false, then no flipping is performed, and if doc.DegreesRotated is a multiple of 360 then no rotation is performed.</summary>
		public static Matrix GetDocumentFlippedRotatedMatrix(Document doc) {
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

		public enum ApplySettings{
      NONE=           0x00,
			ALL=			0xFF,
			CROP=			0x01,
			FLIP=			0x02,
			ROTATE=			0x04,
			COLORFUNCTION=	0x08,
		};

		///<summary>Applies the document specified cropping, flip, rotation, brightness and contrast transformations to the image and returns the resulting image. Zoom and translation must be handled by the calling code. The returned image is always a new image that can be modified without affecting the original image. The change in the image's center point is returned into deltaCenter, so that rotation offsets can be properly calculated when displaying the returned image.</summary>
		public static Bitmap ApplyDocumentSettingsToImage(Document doc,Bitmap image,ApplySettings settings){
			if(image==null){//Any operation on a non-existant image produces a non-existant image.
				return null;
			}
			if(doc==null){//No doc implies no operations, implies that the image should be returned unaltered.
				return (Bitmap)image.Clone();
			}
			//CROP - Implies that the croping rectangle must be saved in raw-image-space coordinates, 
			//with an origin of that equal to the upper left hand portion of the image.
			Rectangle cropResult;
			if((settings&ApplySettings.CROP)!=0 &&	//Crop not requested.
				doc.CropW>0 && doc.CropH>0)//No clip area yet defined, so no clipping is performed.
			{
				float[] cropDims=ODMathLib.IntersectRectangles(0,0,image.Width,image.Height,//Intersect image rectangle with
					doc.CropX,doc.CropY,doc.CropW,doc.CropH);//document crop rectangle.
				if(cropDims.Length==0){//The entire image has been cropped away.
					return null;
				}
				//Rounds dims up, so that data is not lost, but possibly not removing all of what was expected.
				cropResult=new Rectangle((int)cropDims[0],(int)cropDims[1],
					(int)Math.Ceiling(cropDims[2]),(int)Math.Ceiling(cropDims[3]));
			}else{
				cropResult=new Rectangle(0,0,image.Width,image.Height);//No cropping.
			}
			//Always use 32-bit images in memory. We could use 24-bit images here (it works in Windows, but MONO produces
			//output using 32-bit data on a 24-bit image in that case, providing horrible output). Perhaps we can update
			//this when MONO is more fully featured.
			Bitmap cropped=new Bitmap(cropResult.Width,cropResult.Height,PixelFormat.Format32bppArgb);
			Graphics g=Graphics.FromImage(cropped);
			Rectangle croppedDims=new Rectangle(0,0,cropped.Width,cropped.Height);
			g.DrawImage(image,croppedDims,cropResult,GraphicsUnit.Pixel);
			g.Dispose();
			//FLIP AND ROTATE - must match the operations in GetDocumentFlippedRotatedMatrix().
			if((settings&ApplySettings.FLIP)!=0){
				if(doc.IsFlipped){
					cropped.RotateFlip(RotateFlipType.RotateNoneFlipX);
				}
			}
			if((settings&ApplySettings.ROTATE)!=0) {
				if(doc.DegreesRotated%360==90){
					cropped.RotateFlip(RotateFlipType.Rotate90FlipNone);
				}else if(doc.DegreesRotated%360==180) {
					cropped.RotateFlip(RotateFlipType.Rotate180FlipNone);
				}else if(doc.DegreesRotated%360==270){
					cropped.RotateFlip(RotateFlipType.Rotate270FlipNone);
				}
			}
			//APPLY BRIGHTNESS AND CONTRAST - 
			//TODO: should be updated later for more general functions 
			//(create inputValues and outputValues from stored db function/table).
			if((settings&ApplySettings.COLORFUNCTION)!=0 &&
				doc.WindowingMax!=0 && //Do not apply color function if brightness/contrast have never been set (assume normal settings).
				!(doc.WindowingMax==255 && doc.WindowingMin==0)){//Don't apply if brightness/contrast settings are normal.
				float[] inputValues=new float[] {
					doc.WindowingMin/255f,
					doc.WindowingMax/255f,
				};
				float[] outputValues=new float[]{
					0,
					1,
				};
				BitmapData croppedData=null;
				try{
					croppedData=cropped.LockBits(new Rectangle(0,0,cropped.Width,cropped.Height),
						ImageLockMode.ReadWrite,cropped.PixelFormat);
					unsafe{
						byte* pBits;
						if(croppedData.Stride<0){
							pBits=(byte*)croppedData.Scan0.ToPointer()+croppedData.Stride*(croppedData.Height-1);
						}else{
							pBits=(byte*)croppedData.Scan0.ToPointer();
						}
						//The following loop goes through each byte of each 32-bit value and applies the color function to it.
						//Thus, the same transformation is performed to all 4 color components equivalently for each pixel.
						for(int i=0;i<croppedData.Stride*croppedData.Height;i++){
							float colorComponent=pBits[i]/255f;
							float rangedOutput;
							if(colorComponent<=inputValues[0]) {
								rangedOutput=outputValues[0];
							}else if(colorComponent>=inputValues[inputValues.Length-1]) {
								rangedOutput=outputValues[outputValues.Length-1];
							}else{
								int j=0;
								while(!(inputValues[j]<=colorComponent && colorComponent<inputValues[j+1])) {
									j++;
								}
								rangedOutput=((colorComponent-inputValues[j])*(outputValues[j+1]-outputValues[j]))
									/(inputValues[j+1]-inputValues[j]);
							}
							pBits[i]=(byte)Math.Round(255*rangedOutput);
						}
					}
				}catch{
				}finally{
					try{
						cropped.UnlockBits(croppedData);
					}catch{
					}
				}
			}
			return cropped;
		}

		///<summary>specify the size of the square to return</summary>
		public static Bitmap GetThumbnail(Image original,int size) {
			Bitmap retVal=new Bitmap(size,size);
			Graphics g=Graphics.FromImage(retVal);
			g.InterpolationMode=InterpolationMode.HighQualityBicubic;
			g.CompositingQuality=CompositingQuality.HighQuality;
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.PixelOffsetMode=PixelOffsetMode.HighQuality;
			if(original.Height>original.Width) {//original is too tall
				float ratio=(float)size/(float)original.Height;
				float w=(float)original.Width*ratio;
				g.DrawImage(original,(size-w)/2f,0,w,(float)size);
			}else {//original is too wide
				float ratio=(float)size/(float)original.Width;
				float h=(float)original.Height*ratio;
				g.DrawImage(original,0,(size-h)/2f,(float)size,h);
			}
			g.Dispose();
			return retVal;
		}

		///<summary>Returns the selection rectangles for each of the images on the mount, as well as the rectangle which encapsulates the entire mount object when rendered (stored in the end location of the resulting array).</summary>
		public static RectangleF[] GetMountItemBoundingBoxes(Mount mount) {
			RectangleF leftImageBox=new RectangleF(20,20,200,200);
			int[] imageSetOrder=new int[] { 1,0,2,3 };
			int imageSpacing=20;
			int numImages=4;
			RectangleF[] imageBoundingBoxes=new RectangleF[numImages+1];
			int i=0;
			for(;i<numImages;i++) {
				imageBoundingBoxes[i]=new RectangleF();
				imageBoundingBoxes[i].Width=leftImageBox.Width;
				imageBoundingBoxes[i].Height=leftImageBox.Height;
				imageBoundingBoxes[i].Y=leftImageBox.Y;
				imageBoundingBoxes[i].X=leftImageBox.X+imageSetOrder[i]*(leftImageBox.Width+imageSpacing);
			}
			imageBoundingBoxes[i]=new RectangleF(0,0,2*leftImageBox.X+numImages*leftImageBox.Width+(numImages-1)*imageSpacing,
				2*leftImageBox.Y+leftImageBox.Height);
			return imageBoundingBoxes;
		}

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. Set imageSelected=-1 to unselect all images, or set to an image ordinal to highlight the image.</summary>
		private static Bitmap CreateMountImage(Mount mount,Bitmap[] originalImages,MountItem[] mountItems,Document[] documents,int imageSelected) {
			RectangleF[] boundingBoxes=GetMountItemBoundingBoxes(mount);
			if(originalImages==null || mountItems==null ||
				originalImages.Length!=mountItems.Length) {
				return new Bitmap(0,0);
			}
			int width=(int)Math.Ceiling(boundingBoxes[boundingBoxes.Length-1].Width);
			int height=(int)Math.Ceiling(boundingBoxes[boundingBoxes.Length-1].Height);
			if(width<=0 || height<=0) {
				return new Bitmap(0,0);
			}
			Bitmap mountImage=new Bitmap(width,height);
			Graphics g=Graphics.FromImage(mountImage);
			try {
				//Draw mount encapsulating background rectangle.
				g.Clear(Pens.SlateGray.Color);
				//Draw image encapsulating background rectangles.
				for(int i=0;i<boundingBoxes.Length-1;i++){
					g.FillRectangle(Brushes.Black,boundingBoxes[i].X,boundingBoxes[i].Y,
						boundingBoxes[i].Width,boundingBoxes[i].Height);//draw box behind image
					Pen highlight;
					if(i==imageSelected) {
						highlight=(Pen)Pens.Yellow.Clone();//highlight desired image.
					}else{
						highlight=(Pen)Pens.DarkGray.Clone();//just surround other images with standard border.
					}
					highlight.Width=8;//Should be even
					g.DrawRectangle(highlight,boundingBoxes[i].X-highlight.Width/2,boundingBoxes[i].Y-highlight.Width/2,
						boundingBoxes[i].Width+highlight.Width,boundingBoxes[i].Height+highlight.Width);
				}
				for(int i=0;i<mountItems.Length;i++){
					RectangleF imageBounds=boundingBoxes[mountItems[i].OrdinalPos];
					Bitmap image=ApplyDocumentSettingsToImage(documents[i],originalImages[i],ApplySettings.ALL);
					float widthScale=imageBounds.Width/image.Width;
					float heightScale=imageBounds.Height/image.Height;
					float scale=(widthScale<heightScale?widthScale:heightScale);
					RectangleF imageRect=new RectangleF(0,0,scale*image.Width,scale*image.Height);
					imageRect.X=imageBounds.X+imageBounds.Width/2-imageRect.Width/2;
					imageRect.Y=imageBounds.Y+imageBounds.Height/2-imageRect.Height/2;
					g.DrawImage(image,imageRect);
					image.Dispose();
				}
			}catch(Exception){
			}finally{
				g.Dispose();
			}
			return mountImage;
		}

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. For use in other modules.</summary>
		public static Bitmap CreateMountImage(Mount mount,string patFolder){
			MountItem[] mountItems=MountItems.GetItemsForMount(mount.MountNum);
			Document[] documents=Documents.GetDocumentsForMountItems(mountItems);
			Bitmap[] originalImages=GetDocumentImages(documents,patFolder);
			return CreateMountImage(mount,originalImages,mountItems,documents,-1);
		}

		///<summary>The returned image array is the same length as the input array. If a document at index i is not an image, or if the image could not be found, then null is returned in index i of the return array. Otherwise, index i will contain a loaded image from file.</summary>
		public static Bitmap[] GetDocumentImages(Document[] documents,string patFolder){
			if(documents==null){
				return new Bitmap[0];
			}
			Bitmap[] images=new Bitmap[documents.Length];//All images in the list start as null (c# default).
			for(int i=0;i<documents.Length;i++){
				string srcFileName=ODFileUtils.CombinePaths(patFolder,documents[i].FileName);
				if(File.Exists(srcFileName)) {
					if(HasImageExtension(srcFileName)) {
						images[i]=new Bitmap(srcFileName);
					}
				}else{
					MessageBox.Show(Lan.g("ContrDocs","File not found: ")+srcFileName);
				}
			}
			return images;
		}

	}
}
