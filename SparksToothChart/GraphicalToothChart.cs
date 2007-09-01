using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SparksToothChart {
	public partial class GraphicalToothChart:UserControl {
		private string[] selectedTeeth;
		private bool useInternational;
		private Color colorBackground;
		private Color colorBackSimple=Color.FromArgb(150,145,153);//constant
		private bool simpleMode=true;
		///<summary>True for hardware graphics, false for software graphics.</summary>
		private bool hardwareMode=false;
		private GraphicalToothChartControl toothChart;
		private Color colorText;
		private Color colorTextHighlight;
		private Color colorBackHighlight;
		///<summary>Never visible.  Just used to store the bitmap.</summary>
		private System.Windows.Forms.PictureBox pictBox;
		///<summary>Only used if in simple mode.  Analogous to GraphicalToothChartControl.ListToothGraphics, but with less detail.</summary>
		private ToothGraphicCollection ListToothGraphics;
		///<summary>valid values are 1 to 32 (int). Only used in simple mode.</summary>
		private ArrayList ALSelectedTeeth;
		///<summary>width of entire set of teeth, in mm.</summary>
		private float WidthProjection;
		private bool MouseIsDown;
		///<summary>Mouse move causes this variable to be updated with the current tooth that the mouse is hovering over.</summary>
		private int hotTooth;
		///<summary>The previous hotTooth.  If this is different than hotTooth, then mouse has just now moved to a new tooth.  Can be 0 to represent no previous.</summary>
		private int hotToothOld;
		int preferredPixelFormatNum;

		public GraphicalToothChart() {
			InitializeComponent();
			WidthProjection=130;
			ListToothGraphics=new ToothGraphicCollection();
			ALSelectedTeeth=new ArrayList();
			ResetControls();
		}

		#region Properties

		///<summary>Valid values are 1-32 and A-Z.</summary>
		public string[] SelectedTeeth {
			get {
				if(simpleMode){
					return selectedTeeth;
				}
				else{
					return toothChart.SelectedTeeth;
				}
				
			}
		}

		///<summary>Set true to show international tooth numbers.</summary>
		public bool UseInternational {
			get {
				return useInternational;
			}
			set {
				useInternational=value;
				if(simpleMode) {
					//				 
				}
				else {
					toothChart.UseInternational=value;
				}
			}
		}

		///<summary></summary>
		public Color ColorBackground {
			get {
				return colorBackground;
			}
			set {
				colorBackground=value;
				if(simpleMode){
					//has no effect 
				}
				else{
					toothChart.ColorBackground=value;
				}
			}
		}

		///<summary></summary>
		public Color ColorText{
			set {
				colorText=value;
				if(simpleMode) {
					//
				}
				else {
					toothChart.ColorText=value;
				}
			}
		}

		///<summary></summary>
		public Color ColorTextHighlight {
			set {
				colorTextHighlight=value;
				if(simpleMode) {
					//
				}
				else {
					toothChart.ColorTextHighlight=value;
				}
			}
		}

		///<summary></summary>
		public Color ColorBackHighlight {
			set {
				colorBackHighlight=value;
				if(simpleMode) {
					//
				}
				else {
					toothChart.ColorBackHighlight=value;
				}
			}
		}

		///<summary>Default is true.  In simpleMode, OpenGL does not even get loaded.</summary>
		public bool SimpleMode {
			get {
				return simpleMode;
			}
			set {
				if(Environment.OSVersion.Platform==PlatformID.Unix){
					return;//disallow changing simpleMode if platform is Unix
				}
				simpleMode=value;
				ResetControls();
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
				if(simpleMode){
					return false;
				}
				return toothChart.autoFinish;
			}set{
				if(!simpleMode){
					toothChart.autoFinish=value;
				}
			}
		}

		public int PreferredPixelFormatNumber{
			get{
				return preferredPixelFormatNum;
			}set{
				preferredPixelFormatNum=value;
			}
		}

		#endregion Properties

		private void ResetControls(){
			selectedTeeth=new string[0];
			this.Controls.Clear();
			if(simpleMode){
				this.Invalidate();
			}
			else{
				//pictBox.Visible=false;
				toothChart=new GraphicalToothChartControl(hardwareMode,preferredPixelFormatNum);
				preferredPixelFormatNum=toothChart.SelectedPixelFormatNumber;
				toothChart.ColorText=colorText;
				toothChart.ColorBackground = colorBackground;
				toothChart.Dock = System.Windows.Forms.DockStyle.Fill;
				toothChart.Location = new System.Drawing.Point(0,0);
				toothChart.Name = "toothChart";
				toothChart.Size = new System.Drawing.Size(719,564);//unnecessary?
				//this.toothChart.TabIndex = 0;
				toothChart.UseInternational = useInternational;
				this.Controls.Add(toothChart);
			}
		}

		#region Public Methods

		///<summary>If ListToothGraphics is empty, then this fills it, including the complex process of loading all drawing points from local resources.  Or if not empty, then this resets all 32+20 teeth to default postitions, no restorations, etc. Primary teeth set to visible false.  Also clears selected.  Should surround with SuspendLayout / ResumeLayout.</summary>
		public void ResetTeeth() {
			selectedTeeth=new string[0];
			if(simpleMode){
				if(ListToothGraphics.Count==0) {//so this will only happen once when program first loads.
					ListToothGraphics.Clear();
					ToothGraphic tooth;
					for(int i=1;i<=32;i++) {
						tooth=new ToothGraphic(i.ToString());
						tooth.Visible=true;
						ListToothGraphics.Add(tooth);
						//primary
						if(ToothGraphic.PermToPri(i.ToString())!="") {
							tooth=new ToothGraphic(ToothGraphic.PermToPri(i.ToString()));
							tooth.Visible=false;
							ListToothGraphics.Add(tooth);
						}
					}
				}
				else {//list was already initially filled, but now user needs to reset it.
					for(int i=0;i<ListToothGraphics.Count;i++) {//loop through all perm and pri teeth.
						ListToothGraphics[i].Reset();
					}
				}
				ALSelectedTeeth.Clear();
				selectedTeeth=new string[0];
				this.Invalidate();
			}
			else{
				toothChart.ResetTeeth();
			}
		}

		///<summary>Moves position of tooth.  Rotations first in order listed, then translations.  Tooth doesn't get moved immediately, just when painting.  All changes are cumulative and are in addition to any previous translations and rotations.  So, for instance, if tooth has already been shifted as part of SetToPrimary, then this will move it more.</summary>
		public void MoveTooth(string toothID,float rotate,float tipM,float tipB,float shiftM,float shiftO,float shiftB) {
			if(simpleMode) {
				//do nothing
			}
			else {
				toothChart.MoveTooth(toothID,rotate,tipM,tipB,shiftM,shiftO,shiftB);
			}
		}

		///<summary>Sets the specified permanent tooth to primary as follows: Sets ShowPrimary to true for the perm tooth.  Makes pri tooth visible=true, repositions perm tooth by translating -Y.  Moves primary tooth slightly to M or D sometimes for better alignment.  And if 2nd primary molar, then because of the larger size, it must move all perm molars to distal.  Ignores if invalid perm tooth.</summary>
		public void SetToPrimary(string toothID) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				if(ToothGraphic.IsPrimary(toothID)) {
					return;
				}
				ListToothGraphics[toothID].ShowPrimary=true;
				//ListToothGraphics[toothID].ShiftO-=12;
				ListToothGraphics[toothID].Visible=false;//instead of shiftO
				this.Invalidate();
				if(!ToothGraphic.IsValidToothID(ToothGraphic.PermToPri(toothID))) {
					return;
				}
				ListToothGraphics[ToothGraphic.PermToPri(toothID)].Visible=true;

			}
			else {
				toothChart.SetToPrimary(toothID);
			}
		}

		///<summary>This is used for crowns and for retainers.  Crowns will be visible on missing teeth with implants.  Crowns are visible on F and O views, unlike ponics which are only visible on F view.  If the tooth is not visible, that should be set before this call, because then, this will set the root invisible.</summary>
		public void SetCrown(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].IsCrown=true;
				if(!ListToothGraphics[toothID].Visible) {//tooth not visible, so set root invisible.
					ListToothGraphics[toothID].SetGroupVisibility(ToothGroupType.Cementum,false);
				}
				ListToothGraphics[toothID].SetSurfaceColors("MODBLFIV",color);
				ListToothGraphics[toothID].SetGroupColor(ToothGroupType.Enamel,color);
				this.Invalidate();
			}
			else {
				toothChart.SetCrown(toothID,color);
			}
		}

		///<summary></summary>
		public void SetSurfaceColors(string toothID,string surfaces,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].SetSurfaceColors(surfaces,color);
				this.Invalidate();
			}
			else {
				toothChart.SetSurfaceColors(toothID,surfaces,color);
			}
		}

		///<summary>Used for missing teeth.  This should always be done before setting restorations, because a pontic will cause the tooth to become visible again except for the root.  So if setInvisible after a pontic, then the pontic can't show.</summary>
		public void SetInvisible(string toothID) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].Visible=false;
				this.Invalidate();
			}
			else {
				toothChart.SetInvisible(toothID);
			}
		}

		///<summary>This is just the same as SetInvisible, except that it also hides the number from showing.  This is used, for example, if premolars are missing, and ortho has completely closed the space.  User will not be able to select this tooth because the number is hidden.</summary>
		public void HideTooth(string toothID) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].Visible=false;
				ListToothGraphics[toothID].HideNumber=true;
				this.Invalidate();
			}
			else {
				toothChart.HideTooth(toothID);
			}
		}

		///<summary>This is used for any pontic, including bridges, full dentures, and partials.  It is usually used on a tooth that has already been set invisible.  This routine sets the tooth to visible again, but makes the root invisible.  Then, it sets the entire crown to the specified color.  If the tooth was not initially invisible, then it does not set the root invisible.  Any connector bars for bridges are set separately.</summary>
		public void SetPontic(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].IsPontic=true;
				if(!ListToothGraphics[toothID].Visible) {//tooth not visible, so set root invisible.
					ListToothGraphics[toothID].SetGroupVisibility(ToothGroupType.Cementum,false);
				}
				ListToothGraphics[toothID].SetSurfaceColors("MODBLFIV",color);
				ListToothGraphics[toothID].SetGroupColor(ToothGroupType.Enamel,color);
			}
			else {
				toothChart.SetPontic(toothID,color);
			}
		}

		///<summary>Root canals are initially not visible.  This routine sets the canals visible, changes the color to the one specified, and also sets the cementum for the tooth to be semitransparent so that the canals can be seen.  Also sets the IsRCT flag for the tooth to true.</summary>
		public void SetRCT(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].IsRCT=true;
				ListToothGraphics[toothID].colorRCT=color;
			}
			else {
				toothChart.SetRCT(toothID,color);
			}
		}

		///<summary>This draws a big red extraction X right on top of the tooth.  It's up to the calling application to figure out when it's appropriate to do this.  Even if the tooth has been marked invisible, there's a good chance that this will still get drawn because a tooth can be set visible again for the drawing the pontic.  So the calling application needs to figure out when it's appropriate to draw the X, and not set this otherwise.</summary>
		public void SetBigX(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].DrawBigX=true;
				ListToothGraphics[toothID].colorX=color;
			}
			else {
				toothChart.SetBigX(toothID,color);
			}
		}

		///<summary>Set this tooth to show a BU or post.</summary>
		public void SetBU(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].IsBU=true;
				ListToothGraphics[toothID].colorBU=color;
			}
			else {
				toothChart.SetBU(toothID,color);
			}
		}

		///<summary>Set this tooth to show an implant</summary>
		public void SetImplant(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].IsImplant=true;
				ListToothGraphics[toothID].colorImplant=color;
			}
			else {
				toothChart.SetImplant(toothID,color);
			}
		}

		///<summary>Set this tooth to show a sealant</summary>
		public void SetSealant(string toothID,Color color) {
			if(simpleMode) {
				if(!ToothGraphic.IsValidToothID(toothID)) {
					return;
				}
				ListToothGraphics[toothID].IsSealant=true;
				ListToothGraphics[toothID].colorSealant=color;
			}
			else {
				toothChart.SetSealant(toothID,color);
			}
		}

		///<summary>Returns a bitmap of what is showing in the control.  Used for printing.</summary>
		public Bitmap GetBitmap() {
			Bitmap dummy=new Bitmap(this.Width,this.Height);
			Graphics g=Graphics.FromImage(dummy);
			PaintEventArgs e=new PaintEventArgs(g,new Rectangle(0,0,dummy.Width,dummy.Height));
			if(simpleMode) {
				OnPaint(e);
				return dummy;
			}
			toothChart.Render(e);
			Bitmap result=toothChart.ReadFrontBuffer();
			g.Dispose();
			return result;
		}

		#endregion

		#region Painting

		protected override void OnPaintBackground(PaintEventArgs e) {
			//base.OnPaintBackground(e);//don't draw background
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			if(!simpleMode){
				return;
			}
			Graphics g=e.Graphics;
			g.DrawImage(pictBox.Image,new Rectangle(0,0,this.Width,this.Height));
			g.SmoothingMode=SmoothingMode.HighQuality;
			for(int t=0;t<ListToothGraphics.Count;t++) {//loop through each tooth
				if(ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				DrawFacialView(ListToothGraphics[t],g);
				DrawOcclusalView(ListToothGraphics[t],g);
			}
			DrawNumbers(g);
			g.Dispose();
		}

		///<summary>Only called when in simple graphical mode.</summary>
		private void DrawFacialView(ToothGraphic toothGraphic,Graphics g) {
			float x,y;
			x=GetTransX(toothGraphic.ToothID);
			y=GetTransYfacial(toothGraphic.ToothID);
			if(toothGraphic.Visible
				|| (toothGraphic.IsCrown && toothGraphic.IsImplant)
				|| toothGraphic.IsPontic) {
				//DrawTooth(toothGraphic,g);
			}
			float w=0;
			if(!ToothGraphic.IsPrimary(toothGraphic.ToothID)){
				w=ToothGraphic.GetWidth(toothGraphic.ToothID)/WidthProjection*(float)Width;
			}
			if(!ToothGraphic.IsPrimary(toothGraphic.ToothID) && (!toothGraphic.Visible || toothGraphic.IsPontic)){
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)){
					g.FillRectangle(new SolidBrush(colorBackSimple),x-w/2f,0,w,Height/2f-20);
				}
				else{
					g.FillRectangle(new SolidBrush(colorBackSimple),x-w/2f,Height/2f+20,w,Height/2f-20);
				}
			}
			if(toothGraphic.DrawBigX) {
				float halfxwidth=6;
				float xheight=58;
				float offsetofx=73;
				//toothGraphic.colorX
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {					
					g.DrawLine(new Pen(toothGraphic.colorX),x-halfxwidth,Height/2f-offsetofx-xheight,x+halfxwidth,Height/2f-offsetofx);
					g.DrawLine(new Pen(toothGraphic.colorX),x+halfxwidth,Height/2f-offsetofx-xheight,x-halfxwidth,Height/2f-offsetofx);
				}
				else {//Mandible
					g.DrawLine(new Pen(toothGraphic.colorX),x-halfxwidth,Height/2f+offsetofx+xheight,x+halfxwidth,Height/2f+offsetofx);
					g.DrawLine(new Pen(toothGraphic.colorX),x+halfxwidth,Height/2f+offsetofx+xheight,x-halfxwidth,Height/2f+offsetofx);
				}
			}
			if(toothGraphic.Visible && toothGraphic.IsRCT) {//draw RCT
				//x=,y= etc
				//toothGraphic.colorRCT
				//?
			}
			if(toothGraphic.Visible && toothGraphic.IsBU) {//BU or Post
				//?
			}
			if(toothGraphic.IsImplant) {
				//?
			}
		}

		private void DrawOcclusalView(ToothGraphic toothGraphic,Graphics g) {
			//now the occlusal surface. Absolute pixels instead of mm relative to center.
			float x,y;
			x=GetTransX(toothGraphic.ToothID);
			y=GetTransYocclusal(toothGraphic.ToothID);
			if(toothGraphic.Visible//might not be visible if an implant
				|| (toothGraphic.IsCrown && toothGraphic.IsImplant)//a crown on an implant will paint
			//pontics won't paint, because tooth is invisible
				//but, unlike the regular toothchart, we do want pontics to paint here
				|| toothGraphic.IsPontic)
			{
				DrawToothOcclusal(toothGraphic,g);
			}
			if(toothGraphic.Visible && 
				toothGraphic.IsSealant) {//draw sealant
				//?
			}
		}

		private void DrawNumbers(Graphics g) {
			for(int i=1;i<=32;i++) {
				if(ALSelectedTeeth.Contains(i)) {
					DrawNumber(i,true,true,g);
				}
				else {
					DrawNumber(i,false,true,g);
				}
			}
		}

		///<summary>Gets the rectangle in pixels surrounding a tooth number.  Used to draw the box and to invalidate the area.</summary>
		private RectangleF GetNumberRec(string tooth_id,Graphics g) {
			float xPos=GetTransX(tooth_id);
			float yPos=Height/2f;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				yPos-=14;
			}
			else {
				yPos+=3;
			}
			string displayNum=tooth_id;
			if(useInternational) {
				displayNum=ToothGraphic.ToInternat(displayNum);
			}
			float strWidth=g.MeasureString(displayNum,Font).Width;
			xPos-=strWidth/2f;
			RectangleF rec=new RectangleF(xPos-1,yPos-1,strWidth,12);//this rec has origin at UL
			return rec;
		}

		///<summary>Draws the number and the rectangle behind it.  Draws in the appropriate color</summary>
		private void DrawNumber(int intTooth, bool isSelected, bool isFullRedraw,Graphics g) {
			string tooth_id=intTooth.ToString();
			string displayNum=intTooth.ToString();
			bool hideNumber=false;
			string pri=ToothGraphic.PermToPri(tooth_id);
			try{
				if(ToothGraphic.IsValidToothID(pri)//pri is valid
					&& ListToothGraphics[pri].Visible)//and pri visible
				{
					tooth_id=pri;
				}
				if(isFullRedraw && ListToothGraphics[tooth_id].HideNumber){//if redrawing all numbers, and this is a "hidden" number
					return;//skip
				}
				displayNum=tooth_id;
				if(useInternational) {
					displayNum=ToothGraphic.ToInternat(displayNum);
				}
				hideNumber=ListToothGraphics[tooth_id].HideNumber;
			}
			catch{
				//must be design mode.
			}
			RectangleF rec=GetNumberRec(tooth_id,g);
			if(isSelected){
				g.FillRectangle(new SolidBrush(colorBackHighlight),rec);
				if(!hideNumber){//Only draw if number is not hidden.
					g.DrawString(displayNum,Font,new SolidBrush(colorTextHighlight),rec.X,rec.Y);
				}
			} 
			else{
				g.FillRectangle(new SolidBrush(colorBackground),rec);
				if(!hideNumber) {//Only draw if number is not hidden.
					g.DrawString(displayNum,Font,new SolidBrush(colorText),rec.X,rec.Y);
				}
			}
		}

		///<summary></summary>
		private void DrawToothOcclusal(ToothGraphic toothGraphic,Graphics g) {
			ToothGroup group;
			float x,y;
			Pen outline=new Pen(Color.Gray);
			for(int i=0;i<toothGraphic.Groups.Count;i++) {
				group=(ToothGroup)toothGraphic.Groups[i];
				if(!group.Visible) {
					continue;
				}
				x=GetTransX(toothGraphic.ToothID);
				y=GetTransYocclusal(toothGraphic.ToothID);
				float sqB=4;//half the size of the central sqare. B for Big.
				float cirB=9.5f;//radius of outer circle
				float sqS=3;//S for small
				float cirS=8f;
				GraphicsPath path;
				SolidBrush brush=new SolidBrush(group.PaintColor);
				string dir;
				switch(group.GroupType){
					case ToothGroupType.O:
						g.FillRectangle(brush,x-sqB,y-sqB,2f*sqB,2f*sqB);
						g.DrawRectangle(outline,x-sqB,y-sqB,2f*sqB,2f*sqB);
						break;
					case ToothGroupType.I:
						g.FillRectangle(brush,x-sqS,y-sqS,2f*sqS,2f*sqS);
						g.DrawRectangle(outline,x-sqS,y-sqS,2f*sqS,2f*sqS);
						break;
					case ToothGroupType.B:
						if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)){
							path=GetPath("U",x,y,sqB,cirB);
						}
						else{
							path=GetPath("D",x,y,sqB,cirB);
						}
						g.FillPath(brush,path);
						g.DrawPath(outline,path);
						break;
					case ToothGroupType.F:
						if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
							path=GetPath("U",x,y,sqS,cirS);
						}
						else {
							path=GetPath("D",x,y,sqS,cirS);
						}
						g.FillPath(brush,path);
						g.DrawPath(outline,path);
						break;
					case ToothGroupType.L:
						if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
							dir="D";
						}
						else {
							dir="U";
						}
						if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
							path=GetPath(dir,x,y,sqS,cirS);
						}
						else {
							path=GetPath(dir,x,y,sqB,cirB);
						}
						g.FillPath(brush,path);
						g.DrawPath(outline,path);
						break;
					case ToothGroupType.M:
						if(ToothGraphic.IsRight(toothGraphic.ToothID)){
							dir="R";
						}
						else{
							dir="L";
						}
						if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
							path=GetPath(dir,x,y,sqS,cirS);
						}
						else {
							path=GetPath(dir,x,y,sqB,cirB);
						}
						g.FillPath(brush,path);
						g.DrawPath(outline,path);
						break;
					case ToothGroupType.D:
						if(ToothGraphic.IsRight(toothGraphic.ToothID)) {
							dir="L";
						}
						else {
							dir="R";
						}
						if(ToothGraphic.IsAnterior(toothGraphic.ToothID)) {
							path=GetPath(dir,x,y,sqS,cirS);
						}
						else {
							path=GetPath(dir,x,y,sqB,cirB);
						}
						g.FillPath(brush,path);
						g.DrawPath(outline,path);
						break;
				}
				//group.PaintColor
				//Gl.glCallList(displayListOffset+toothGraphic.GetIndexForDisplayList(group));
			}
		}

		///<summary>sq and cir refer to the radius of those two elements.</summary>
		private GraphicsPath GetPath(string UDLR,float x,float y,float sq,float cir){
			GraphicsPath path=new GraphicsPath();
			float pt=cir*0.7071f;//the x or y dist to the point where the circle is at 45 degrees.
			switch(UDLR){
				case "U":
					path.AddLine(x-sq,y-sq,x+sq,y-sq);
					path.AddLine(x+sq,y-sq,x+pt,y-pt);
					path.AddArc(x-cir,y-cir,cir*2f,cir*2f,360-45,-90);
					path.AddLine(x-pt,y-pt,x-sq,y-sq);
					break;
				case "D":
					path.AddLine(x+sq,y+sq,x-sq,y+sq);
					path.AddLine(x-sq,y+sq,x-pt,y+pt);
					path.AddArc(x-cir,y-cir,cir*2f,cir*2f,90+45,-90);
					path.AddLine(x+pt,y+pt,x+sq,y+sq);
					break;
				case "L":
					path.AddLine(x-sq,y+sq,x-sq,y-sq);
					path.AddLine(x-sq,y-sq,x-pt,y-pt);
					path.AddArc(x-cir,y-cir,cir*2f,cir*2f,180+45,-90);
					path.AddLine(x-pt,y+pt,x-sq,y+sq);
					break;
				case "R":
					path.AddLine(x+sq,y-sq,x+sq,y+sq);
					path.AddLine(x+sq,y+sq,x+pt,y+pt);
					path.AddArc(x-cir,y-cir,cir*2f,cir*2f,45,-90);
					path.AddLine(x+pt,y-pt,x+sq,y-sq);
					break;
			}
			return path;
		}

		///<summary>Pri or perm tooth numbers are valid.  Only locations of perm teeth are stored.  This also converts mm to screen pixels.</summary>
		private float GetTransX(string tooth_id) {
			int toothInt=ToothGraphic.IdToInt(tooth_id);
			if(toothInt==-1) {
				throw new ApplicationException("Invalid tooth number: "+tooth_id);//only for debugging
			}
			float xmm=ToothGraphic.GetDefaultOrthoXpos(toothInt);//in +/- mm from center
			return (WidthProjection/2f+xmm)*Width/WidthProjection;
		}

		///<summary>In control coords rather than mm.</summary>
		private float GetTransYfacial(string tooth_id) {
			float basic=30;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				return Height/2-basic;
			}
			return Height/2+basic;
		}

		private float GetTransYocclusal(string tooth_id) {
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				return Height/2-48f;
			}
			return Height/2+48f;
		}
		#endregion

		#region Mouse And Selections

		///<summary>Always returns a number between 1 and 32.  This isn't perfect, since it only operates on perm teeth, and assumes that any primary tooth will be at the same x pos as its perm tooth.</summary>
		private int GetToothAtPoint(int x,int y) {
			float closestDelta=(float)(Width*2);//start it off really big
			int closestTooth=1;
			float toothPos=0;
			float delta=0;
			//float xPos=(float)((float)(x-Width/2)*WidthProjection/(float)Width);//in mm instead of screen coordinates
			if(y<Height/2) {//max
				for(int i=1;i<=16;i++) {
					if(ListToothGraphics[i.ToString()].HideNumber) {
						continue;
					}
					toothPos=GetTransX(i.ToString());//ToothGraphic.GetDefaultOrthoXpos(i);
					if(x>toothPos) {//xPos>toothPos) {
						delta=x-toothPos;
					}
					else {
						delta=toothPos-x;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=i;
					}
				}
				return closestTooth;
			}
			else {//mand
				for(int i=17;i<=32;i++) {
					if(ListToothGraphics[i.ToString()].HideNumber) {
						continue;
					}
					toothPos=GetTransX(i.ToString());//ToothGraphic.GetDefaultOrthoXpos(i);//in mm.
					if(x>toothPos) {
						delta=x-toothPos;
					}
					else {
						delta=toothPos-x;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=i;
					}
				}
				return closestTooth;
			}
		}

		protected override void OnMouseClick(MouseEventArgs e) {
			base.OnMouseClick(e);
			//int clicked=GetToothAtPoint(e.X,e.Y);

		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			if(ListToothGraphics.Count==0){
				return;
			}
			if(!simpleMode){
				return;
			}
			MouseIsDown=true;
			int toothClicked=GetToothAtPoint(e.X,e.Y);
			//MessageBox.Show(toothClicked.ToString());
			if(ALSelectedTeeth.Contains(toothClicked)) {
				SetSelected(toothClicked,false);
			}
			else {
				SetSelected(toothClicked,true);
			}
			//Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			MouseIsDown=false;
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if(ListToothGraphics.Count==0) {
				return;
			}
			if(!simpleMode) {
				return;
			}
			hotTooth=GetToothAtPoint(e.X,e.Y);
			if(hotTooth==hotToothOld) {//mouse has not moved to another tooth
				return;
			}
			hotToothOld=hotTooth;
			if(MouseIsDown) {//drag action
				if(ALSelectedTeeth.Contains(hotTooth)) {
					SetSelected(hotTooth,false);
				}
				else {
					SetSelected(hotTooth,true);
				}
			}
		}

		///<summary>Used by mousedown and mouse move to set teeth selected or unselected.  Also used externally to set teeth selected.  Draws the changes also.</summary>
		public void SetSelected(int intTooth,bool setValue) {
			if(simpleMode) {
				Graphics g=this.CreateGraphics();
				if(setValue) {
					ALSelectedTeeth.Add(intTooth);
					DrawNumber(intTooth,true,false,g);
				}
				else {
					ALSelectedTeeth.Remove(intTooth);
					DrawNumber(intTooth,false,false,g);
				}
				RectangleF recF=GetNumberRec(intTooth.ToString(),g);
				Rectangle rec=new Rectangle((int)recF.X,(int)recF.Y,(int)recF.Width,(int)recF.Height);
				Invalidate(rec);
				Application.DoEvents();
				if(ALSelectedTeeth.Count==0) {
					selectedTeeth=new string[0];
				}
				else {
					selectedTeeth=new string[ALSelectedTeeth.Count];
					for(int i=0;i<ALSelectedTeeth.Count;i++) {
						if(ToothGraphic.IsValidToothID(ToothGraphic.PermToPri(ALSelectedTeeth[i].ToString()))//pri is valid
						&& ListToothGraphics[ALSelectedTeeth[i].ToString()].ShowPrimary)//and set to show pri
					{
							selectedTeeth[i]=ToothGraphic.PermToPri(ALSelectedTeeth[i].ToString());
						}
						else {
							selectedTeeth[i]=((int)ALSelectedTeeth[i]).ToString();
						}
					}
				}
				g.Dispose();
			}
			else {
				toothChart.SetSelected(intTooth,setValue);
			}
		}

		#endregion Mouse And Selections

	}
}
