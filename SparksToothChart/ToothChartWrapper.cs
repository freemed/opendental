using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace SparksToothChart {
	public partial class ToothChartWrapper:UserControl {
		private string[] selectedTeeth;
		///<summary>True for hardware graphics, false for software graphics.</summary>
		private bool hardwareMode=false;
		private ToothChartOpenGL toothChartOpenGL;
		private ToothChartDirectX toothChartDirectX;
		private int preferredPixelFormatNum;
		
		///<summary></summary>
		[Category("Action"),Description("Occurs when the mouse goes up ending a drawing segment.")]
		public event ToothChartDrawEventHandler SegmentDrawn=null;
		private DrawingMode drawMode;
		///<summary>This data object will hold nearly all information about what to draw.  It is not exposed publicly, but is instead acted on by methods.</summary>
		private ToothChartData TcData;
		
		public ToothChartWrapper() {
			TcData=new ToothChartData();
			InitializeComponent();
			ResetControls();
		}

		#region Properties

		public DrawingMode DrawMode{
			get{
				return drawMode;
			}
			set{
				if(Environment.OSVersion.Platform==PlatformID.Unix) {
					return;//disallow changing from simpleMode if platform is Unix
				}
				drawMode=value;
				ResetControls();
			}
		}

		///<summary>Valid values are 1-32 and A-Z.</summary>
		public List<string> SelectedTeeth {
			get {
				return TcData.SelectedTeeth;
			}
		}

		///<summary></summary>
		[Browsable(false)]
		public Color ColorBackground {
			get {
				return TcData.ColorBackground;
			}
			set {
				TcData.ColorBackground=value;
				Invalidate();
			}
		}

		///<summary></summary>
		[Browsable(false)]
		public Color ColorText{
			set {
				TcData.ColorText=value;
				Invalidate();
			}
		}

		///<summary></summary>
		[Browsable(false)]
		public Color ColorTextHighlight {
			set {
				TcData.ColorTextHighlight=value;
				Invalidate();
			}
		}

		///<summary></summary>
		[Browsable(false)]
		public Color ColorBackHighlight {
			set {
				TcData.ColorBackHighlight=value;
				Invalidate();
			}
		}

		///<summary>Set to true when using hardware rendering in OpenGL, and false otherwise. This will have no effect when in simple 2D graphics mode.</summary>
		public bool UseHardware{
			get{
				return hardwareMode;
			}
			set{
				hardwareMode=value;
			}
		}

		public bool AutoFinish{
			get{
				if(drawMode==DrawingMode.Simple2D) {
					return false;
				}
				return toothChartOpenGL.autoFinish;
			}
			set{
				if(drawMode!=DrawingMode.Simple2D) {
					toothChartOpenGL.autoFinish=value;
				}
			}
		}

		public int PreferredPixelFormatNumber{
			get{
				return preferredPixelFormatNum;
			}
			set{
				preferredPixelFormatNum=value;
			}
		}

		public CursorTool CursorTool{
			get{
				return TcData.CursorTool;
			}
			set{
				TcData.CursorTool=value;
				if(TcData.CursorTool==CursorTool.Pointer) {
					this.Cursor=Cursors.Default;
				}
				if(TcData.CursorTool==CursorTool.Pen) {
					this.Cursor=new Cursor(GetType(),"Pen.cur");
				}
				if(TcData.CursorTool==CursorTool.Eraser) {
					this.Cursor=new Cursor(GetType(),"EraseCircle.cur");
				}
				if(TcData.CursorTool==CursorTool.ColorChanger) {
					this.Cursor=new Cursor(GetType(),"ColorChanger.cur");
				}
				//if(drawMode!=DrawingMode.Simple2D) {
				//	toothChartOpenGL.CursorTool=value;
				//}
			}
		}

		///<summary>For the freehand drawing tool.</summary>
		public Color ColorDrawing{
			set{
				TcData.ColorDrawing=value;
			}
		}
		#endregion Properties

		protected override void OnInvalidated(InvalidateEventArgs e) {
			base.OnInvalidated(e);
			if(drawMode==DrawingMode.Simple2D) {
				toothChart2D.Invalidate();
			}
			else if(drawMode==DrawingMode.DirectX) {
				toothChartDirectX.Invalidate();
			}
			else if(drawMode==DrawingMode.OpenGL) {
				toothChartOpenGL.Invalidate();
				//toothChartOpenGL.TaoDraw();
			}
		}

		private void ResetControls(){
			selectedTeeth=new string[0];
			this.Controls.Clear();
			if(drawMode==DrawingMode.Simple2D){
				//this.Invalidate();
				toothChart2D=new ToothChart2D();
				toothChart2D.Dock = System.Windows.Forms.DockStyle.Fill;
				toothChart2D.Location = new System.Drawing.Point(0,0);
				toothChart2D.Name = "toothChart2D";
				//toothChart2D.Size = new System.Drawing.Size(719,564);//unnecessary?
				//toothChart2D.SegmentDrawn+=new ToothChartDrawEventHandler(toothChart_SegmentDrawn);
				toothChart2D.TcData=TcData;
				toothChart2D.SuspendLayout();
				this.Controls.Add(toothChart2D);
				ResetTeeth();
				//initialize graphics?
				toothChart2D.ResumeLayout();
			}
			else if(drawMode==DrawingMode.DirectX){
				toothChartDirectX=new ToothChartDirectX();//(hardwareMode,preferredPixelFormatNum);
				//preferredPixelFormatNum=toothChart.SelectedPixelFormatNumber;
				//toothChartDirectX.ColorText=colorText;
				toothChartDirectX.Dock = System.Windows.Forms.DockStyle.Fill;
				toothChartDirectX.Location = new System.Drawing.Point(0,0);
				toothChartDirectX.Name = "toothChartDirectX";
				//toothChartDirectX.Size = new System.Drawing.Size(719,564);//unnecessary?
				//toothChartDirectX.SegmentDrawn+=new ToothChartDrawEventHandler(toothChart_SegmentDrawn);
				toothChartDirectX.TcData=TcData;
				//toothChartDirectX.SuspendLayout?
				this.Controls.Add(toothChartDirectX);
				ResetTeeth();
				toothChartDirectX.InitializeGraphics();
				//toothChartDirectX.ResumeLayout?
			}
			else if(drawMode==DrawingMode.OpenGL){
				toothChartOpenGL=new ToothChartOpenGL(hardwareMode,preferredPixelFormatNum);
				preferredPixelFormatNum=toothChartOpenGL.SelectedPixelFormatNumber;
				//toothChartOpenGL.ColorText=colorText;
				toothChartOpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
				toothChartOpenGL.Location = new System.Drawing.Point(0,0);
				toothChartOpenGL.Name = "toothChartOpenGL";
				//toothChartOpenGL.Size = new System.Drawing.Size(719,564);//unnecessary?
				toothChartOpenGL.TcData=TcData;
				//toothChartOpenGL.SegmentDrawn+=new ToothChartDrawEventHandler(toothChart_SegmentDrawn);
				toothChartOpenGL.SuspendLayout();
				this.Controls.Add(toothChartOpenGL);
				ResetTeeth();
				toothChartOpenGL.InitializeGraphics();//MakeDisplayLists();
				toothChartOpenGL.ResumeLayout();
			}
		}

		#region Public Methods

		///<summary>If ListToothGraphics is empty, then this fills it, including the complex process of loading all drawing points from local resources.  Or if not empty, then this resets all 32+20 teeth to default postitions, no restorations, etc. Primary teeth set to visible false.  Also clears selected.  Should surround with SuspendLayout / ResumeLayout.</summary>
		public void ResetTeeth() {
			//selectedTeeth=new string[0];
			//this will only happen once when program first loads.  Unfortunately, there is no way to tell what the drawMode is going to be when loading the graphics from the file.  So any other initialization must happen in resetControls.
			if(TcData.ListToothGraphics.Count==0) {
				TcData.ListToothGraphics.Clear();
				ToothGraphic tooth;
				for(int i=1;i<=32;i++) {
					tooth=new ToothGraphic(i.ToString());
					tooth.Visible=true;
					TcData.ListToothGraphics.Add(tooth);
					//primary
					if(Tooth.PermToPri(i.ToString())!="") {
						tooth=new ToothGraphic(Tooth.PermToPri(i.ToString()));
						tooth.Visible=false;
						TcData.ListToothGraphics.Add(tooth);
					}
				}
				tooth=new ToothGraphic("implant");
				TcData.ListToothGraphics.Add(tooth);
			}
			else {//list was already initially filled, but now user needs to reset it.
				for(int i=0;i<TcData.ListToothGraphics.Count;i++) {//loop through all perm and pri teeth.
					TcData.ListToothGraphics[i].Reset();
				}
			}
			TcData.SelectedTeeth.Clear();
			TcData.DrawingSegmentList=new List<ToothInitial>();
			TcData.PointList=new List<Point>();
			Invalidate();
		}

		///<summary>Moves position of tooth.  Rotations first in order listed, then translations.  Tooth doesn't get moved immediately, just when painting.  All changes are cumulative and are in addition to any previous translations and rotations.</summary>
		public void MoveTooth(string toothID,float rotate,float tipM,float tipB,float shiftM,float shiftO,float shiftB) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			TcData.ListToothGraphics[toothID].ShiftM+=shiftM;
			TcData.ListToothGraphics[toothID].ShiftO+=shiftO;
			TcData.ListToothGraphics[toothID].ShiftB+=shiftB;
			TcData.ListToothGraphics[toothID].Rotate+=rotate;
			TcData.ListToothGraphics[toothID].TipM+=tipM;
			TcData.ListToothGraphics[toothID].TipB+=tipB;
			Invalidate();
		}

		///<summary>Sets the specified permanent tooth to primary. Works as follows: Sets ShowPrimaryLetter to true for the perm tooth.  Makes pri tooth visible=true.  Also repositions perm tooth by translating -Y.  Moves primary tooth slightly to M or D sometimes for better alignment.  And if 2nd primary molar, then because of the larger size, it must move all perm molars to distal.</summary>
		public void SetPrimary(string toothID) {
			if(!ToothGraphic.IsValidToothID(toothID)) {
				return;
			}
			if(Tooth.IsPrimary(toothID)) {
				return;
			}
			TcData.ListToothGraphics[toothID].ShiftO-=12;
			if(ToothGraphic.IsValidToothID(Tooth.PermToPri(toothID))) {//if there's a primary tooth at this location
				TcData.ListToothGraphics[Tooth.PermToPri(toothID)].Visible=true;//show the primary.
				TcData.ListToothGraphics[toothID].ShowPrimaryLetter=true;
			}		
			//first pri mand molars, shift slightly to M
			if(toothID=="21") {
				TcData.ListToothGraphics["J"].ShiftM+=0.5f;
			}
			if(toothID=="28") {
				TcData.ListToothGraphics["S"].ShiftM+=0.5f;
			}
			//second pri molars are huge, so shift distally for space
			//and move all the perm molars distally too
			if(toothID=="4") {
				TcData.ListToothGraphics["A"].ShiftM-=0.5f;
				TcData.ListToothGraphics["1"].ShiftM-=1;
				TcData.ListToothGraphics["2"].ShiftM-=1;
				TcData.ListToothGraphics["3"].ShiftM-=1;
			}
			if(toothID=="13") {
				TcData.ListToothGraphics["J"].ShiftM-=0.5f;
				TcData.ListToothGraphics["14"].ShiftM-=1;
				TcData.ListToothGraphics["15"].ShiftM-=1;
				TcData.ListToothGraphics["16"].ShiftM-=1;
			}
			if(toothID=="20") {
				TcData.ListToothGraphics["K"].ShiftM-=1.2f;
				TcData.ListToothGraphics["17"].ShiftM-=2.3f;
				TcData.ListToothGraphics["18"].ShiftM-=2.3f;
				TcData.ListToothGraphics["19"].ShiftM-=2.3f;
			}
			if(toothID=="29") {
				TcData.ListToothGraphics["T"].ShiftM-=1.2f;
				TcData.ListToothGraphics["30"].ShiftM-=2.3f;
				TcData.ListToothGraphics["31"].ShiftM-=2.3f;
				TcData.ListToothGraphics["32"].ShiftM-=2.3f;
			}
			Invalidate();
		}

		///<summary>This is used for crowns and for retainers.  Crowns will be visible on missing teeth with implants.  Crowns are visible on F and O views, unlike ponics which are only visible on F view.  If the tooth is not visible, that should be set before this call, because then, this will set the root invisible.</summary>
		public void SetCrown(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].IsCrown=true;
			if(!TcData.ListToothGraphics[toothID].Visible) {//tooth not visible, so set root invisible.
				TcData.ListToothGraphics[toothID].SetGroupVisibility(ToothGroupType.Cementum,false);
			}
			TcData.ListToothGraphics[toothID].SetSurfaceColors("MODBLFIV",color);
			TcData.ListToothGraphics[toothID].SetGroupColor(ToothGroupType.Enamel,color);
			this.Invalidate();
		}

		///<summary>A series of color settings will result in the last ones entered overriding earlier entries.</summary>
		public void SetSurfaceColors(string toothID,string surfaces,Color color) {
			TcData.ListToothGraphics[toothID].SetSurfaceColors(surfaces,color);
			Invalidate();
		}

		///<summary>Used for missing teeth.  This should always be done before setting restorations, because a pontic will cause the tooth to become visible again except for the root.  So if setMissing after a pontic, then the pontic can't show.</summary>
		public void SetMissing(string toothID) {
			TcData.ListToothGraphics[toothID].Visible=false;
			Invalidate();
		}

		///<summary>This is just the same as SetMissing, except that it also hides the number from showing.  This is used, for example, if premolars are missing, and ortho has completely closed the space.  User will not be able to select this tooth because the number is hidden.</summary>
		public void SetHidden(string toothID) {
			TcData.ListToothGraphics[toothID].Visible=false;
			TcData.ListToothGraphics[toothID].HideNumber=true;
			Invalidate();
		}

		///<summary>This is used for any pontic, including bridges, full dentures, and partials.  It is usually used on a tooth that has already been set invisible.  This routine cuases the tooth to show again, but the root needs to be invisible.  Then, it sets the entire crown to the specified color.  If the tooth is already visible, then it does not set the root invisible.</summary>
		public void SetPontic(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].IsPontic=true;
			if(!TcData.ListToothGraphics[toothID].Visible) {
				//tooth not visible, but since IsPontic changes the visibility behavior of the tooth, we need to set the root invisible.
				TcData.ListToothGraphics[toothID].SetGroupVisibility(ToothGroupType.Cementum,false);
			}
			TcData.ListToothGraphics[toothID].SetSurfaceColors("MODBLFIV",color);
			TcData.ListToothGraphics[toothID].SetGroupColor(ToothGroupType.Enamel,color);
			Invalidate();
		}

		///<summary>Root canals are initially not visible.  This routine sets the canals visible, changes the color to the one specified, and also sets the cementum for the tooth to be semitransparent so that the canals can be seen.  Also sets the IsRCT flag for the tooth to true.</summary>
		public void SetRCT(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].IsRCT=true;
			TcData.ListToothGraphics[toothID].colorRCT=color;
			Invalidate();
		}

		///<summary>This draws a big red extraction X right on top of the tooth.  It's up to the calling application to figure out when it's appropriate to do this.  Even if the tooth has been marked invisible, there's a good chance that this will still get drawn because a tooth can be set visible again for the drawing the pontic.  So the calling application needs to figure out when it's appropriate to draw the X, and not set this otherwise.</summary>
		public void SetBigX(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].DrawBigX=true;
			TcData.ListToothGraphics[toothID].colorX=color;
			Invalidate();
		}

		///<summary>Set this tooth to show a BU or post.</summary>
		public void SetBU(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].IsBU=true;
			TcData.ListToothGraphics[toothID].colorBU=color;
			Invalidate();
		}

		///<summary>Set this tooth to show an implant</summary>
		public void SetImplant(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].IsImplant=true;
			TcData.ListToothGraphics[toothID].colorImplant=color;
			Invalidate();
		}

		///<summary>Set this tooth to show a sealant</summary>
		public void SetSealant(string toothID,Color color) {
			TcData.ListToothGraphics[toothID].IsSealant=true;
			TcData.ListToothGraphics[toothID].colorSealant=color;
			Invalidate();
		}

		///<summary></summary>
		public void AddDrawingSegment(ToothInitial drawingSegment) {
			bool alreadyAdded=false;
			for(int i=0;i<TcData.DrawingSegmentList.Count;i++) {
				if(TcData.DrawingSegmentList[i].DrawingSegment==drawingSegment.DrawingSegment) {
					alreadyAdded=true;
					break;
				}
			}
			if(!alreadyAdded){
				TcData.DrawingSegmentList.Add(drawingSegment);
			}
			Invalidate();
			//toothChartOpenGL.AddDrawingSegment(drawingSegment);
		}

		///<summary>Returns a bitmap of what is showing in the control.  Used for printing.</summary>
		public Bitmap GetBitmap() {
			/*
			Bitmap dummy=new Bitmap(this.Width,this.Height);
			Graphics g=Graphics.FromImage(dummy);
			PaintEventArgs e=new PaintEventArgs(g,new Rectangle(0,0,dummy.Width,dummy.Height));
			if(simpleMode) {
				OnPaint(e);
				return dummy;
			}
			toothChartOpenGL.Render(e);
			Bitmap result=toothChartOpenGL.ReadFrontBuffer();
			g.Dispose();
			return result;*/
			return null;
		}

		#endregion

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			TcData.SizeControl=this.Size;
			Invalidate();
		}

		public void SetSelected(string tooth_id,bool setValue) {
			TcData.SetSelected(tooth_id,setValue);
			Invalidate();
		}

		///<summary></summary>
		protected void OnSegmentDrawn(string drawingSegment){
			ToothChartDrawEventArgs tArgs=new ToothChartDrawEventArgs(drawingSegment);
			if(SegmentDrawn!=null){
				SegmentDrawn(this,tArgs);
			}
		}

		private void toothChart_SegmentDrawn(object sender,ToothChartDrawEventArgs e) {
			OnSegmentDrawn(e.DrawingSegement);
		}

		//public void SimulateMouseDown(MouseEventArgs e) {

		//}
		

	}

	public enum CursorTool{
		Pointer,
		Pen,
		Eraser,
		ColorChanger
	}

	///<summary></summary>
	public delegate void ToothChartDrawEventHandler(object sender,ToothChartDrawEventArgs e);

	///<summary></summary>
	public class ToothChartDrawEventArgs{
		private string drawingSegment;
		//private bool isInsert;

		///<summary></summary>
		public ToothChartDrawEventArgs(string drawingSeg){//,bool isInsert){
			this.drawingSegment=drawingSeg;
			//this.isInsert=isInsert;
		}

		///<summary></summary>
		public string DrawingSegement{
			get{ 
				return drawingSegment;
			}
		}

		/*//<summary></summary>
		public bool IsInsert{
			get{ 
				return isInsert;
			}
		}*/
	}

	public enum DrawingMode{
		Simple2D,
		OpenGL,
		DirectX
	}
}
