using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace SparksToothChart {
	public partial class ToothChart2D:UserControl {
		///<summary>This is a reference to the TcData object that's at the wrapper level.</summary>
		public ToothChartData TcData;
		private bool MouseIsDown;
		///<summary>Mouse move causes this variable to be updated with the current tooth that the mouse is hovering over.</summary>
		private string hotTooth;
		///<summary>The previous hotTooth.  If this is different than hotTooth, then mouse has just now moved to a new tooth.  Can be 0 to represent no previous.</summary>
		private string hotToothOld;
		///<summary>A list of points for a line currently being drawn.  Once the mouse is raised, this list gets cleared.</summary>
		private List<Point> PointList;

		public ToothChart2D() {
			InitializeComponent();
			PointList=new List<Point>();
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			if(DesignMode) {
				e.Graphics.DrawImage(pictBox.Image,new Rectangle(0,0,this.Width,this.Height));
				return;
			}
			//our strategy here will be to draw on a new bitmap.
			Bitmap bitmap=new Bitmap(Width,Height);
			Graphics g=Graphics.FromImage(bitmap);
			g.Clear(TcData.ColorBackground);
			//draw a copy of the tooth chart background
			g.DrawImage(pictBox.Image,TcData.RectTarget);
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.TextRenderingHint=TextRenderingHint.ClearTypeGridFit;
			for(int t=0;t<TcData.ListToothGraphics.Count;t++) {//loop through each tooth
				if(TcData.ListToothGraphics[t].ToothID=="implant") {//this is not an actual tooth.
					continue;
				}
				DrawFacialView(TcData.ListToothGraphics[t],g);
				DrawOcclusalView(TcData.ListToothGraphics[t],g);
			}
			DrawNumbers(g);
			DrawDrawingSegments(g);
			e.Graphics.DrawImage(bitmap,0,0);
			g.Dispose();
		}

		protected override void OnPaintBackground(PaintEventArgs e) {
			//base.OnPaintBackground(e);//don't draw background
		}

		///<summary>Only called when in simple graphical mode.</summary>
		private void DrawFacialView(ToothGraphic toothGraphic,Graphics g) {
			float x,y;
			x=TcData.GetTransXpix(toothGraphic.ToothID);
			y=TcData.GetTransYfacialPix(toothGraphic.ToothID);
			if(toothGraphic.Visible
				|| (toothGraphic.IsCrown && toothGraphic.IsImplant)
				|| toothGraphic.IsPontic) {
				//DrawTooth(toothGraphic,g);
			}
			float w=0;
			if(!Tooth.IsPrimary(toothGraphic.ToothID)) {
				w=ToothGraphic.GetWidth(toothGraphic.ToothID)/TcData.ScaleMmToPix;
					// /TcData.WidthProjection*(float)Width;
			}
			if(!Tooth.IsPrimary(toothGraphic.ToothID) && (!toothGraphic.Visible || toothGraphic.IsPontic)) {
				if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
					//g.FillRectangle(new SolidBrush(colorBackSimple),x-w/2f,0,w,Height/2f-20);
				}
				else {
					//g.FillRectangle(new SolidBrush(colorBackSimple),x-w/2f,Height/2f+20,w,Height/2f-20);
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
			x=TcData.GetTransXpix(toothGraphic.ToothID);
			y=TcData.GetTransYocclusalPix(toothGraphic.ToothID,Height);
			if(toothGraphic.Visible//might not be visible if an implant
				|| (toothGraphic.IsCrown && toothGraphic.IsImplant)//a crown on an implant will paint
			//pontics won't paint, because tooth is invisible
				//but, unlike the regular toothchart, we do want pontics to paint here
				|| toothGraphic.IsPontic) {
				DrawToothOcclusal(toothGraphic,g);
			}
			if(toothGraphic.Visible && 
				toothGraphic.IsSealant) {//draw sealant
				//?
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
				x=TcData.GetTransXpix(toothGraphic.ToothID);
				y=TcData.GetTransYocclusalPix(toothGraphic.ToothID,Height);
				float sqB=4;//half the size of the central sqare. B for Big.
				float cirB=9.5f;//radius of outer circle
				float sqS=3;//S for small
				float cirS=8f;
				GraphicsPath path;
				SolidBrush brush=new SolidBrush(group.PaintColor);
				string dir;
				switch(group.GroupType) {
					case ToothGroupType.O:
						g.FillRectangle(brush,x-sqB,y-sqB,2f*sqB,2f*sqB);
						g.DrawRectangle(outline,x-sqB,y-sqB,2f*sqB,2f*sqB);
						break;
					case ToothGroupType.I:
						g.FillRectangle(brush,x-sqS,y-sqS,2f*sqS,2f*sqS);
						g.DrawRectangle(outline,x-sqS,y-sqS,2f*sqS,2f*sqS);
						break;
					case ToothGroupType.B:
						if(ToothGraphic.IsMaxillary(toothGraphic.ToothID)) {
							path=GetPath("U",x,y,sqB,cirB);
						}
						else {
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
						if(ToothGraphic.IsRight(toothGraphic.ToothID)) {
							dir="R";
						}
						else {
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

		///<summary>Gets a path for the pie shape that represents a tooth surface.  sq and cir refer to the radius of those two elements.</summary>
		private GraphicsPath GetPath(string UDLR,float x,float y,float sq,float cir) {
			GraphicsPath path=new GraphicsPath();
			float pt=cir*0.7071f;//the x or y dist to the point where the circle is at 45 degrees.
			switch(UDLR) {
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

		private void DrawNumbers(Graphics g) {
			string tooth_id;
			for(int i=1;i<=52;i++) {
				tooth_id=Tooth.FromOrdinal(i);
				if(TcData.SelectedTeeth.Contains(tooth_id)) {
					DrawNumber(tooth_id,true,true,g);
				}
				else {
					DrawNumber(tooth_id,false,true,g);
				}
			}
		}

		///<summary>Draws the number and the rectangle behind it.  Draws in the appropriate color</summary>
		private void DrawNumber(string tooth_id,bool isSelected,bool isFullRedraw,Graphics g) {
			if(!Tooth.IsValidDB(tooth_id)) {
				return;
			}
			if(isFullRedraw) {//if redrawing all numbers
				if(TcData.ListToothGraphics[tooth_id].HideNumber) {//and this is a "hidden" number
					return;//skip
				}
				if(Tooth.IsPrimary(tooth_id)
					&& !TcData.ListToothGraphics[Tooth.PriToPerm(tooth_id)].ShowPrimaryLetter)//but not set to show primary letters
				{
					return;
				}
			}
	//fix this.  No calls to OpenDentBusiness that require database.
			//string displayNum=OpenDentBusiness.Tooth.GetToothLabelGraphic(tooth_id);
			string displayNum=tooth_id;
			float toMm=1f/TcData.ScaleMmToPix;
			Rectangle rec=TcData.GetNumberRecPix(tooth_id,g);
			//Rectangle recPix=TcData.ConvertRecToPix(recMm);
			if(isSelected) {
				g.FillRectangle(new SolidBrush(TcData.ColorBackHighlight),rec);
			}
			else {
				g.FillRectangle(new SolidBrush(TcData.ColorBackground),rec);
			}
			if(TcData.ListToothGraphics[tooth_id].HideNumber) {//If number is hidden.
				//do not print string
			}
			else if(Tooth.IsPrimary(tooth_id)
				&& !TcData.ListToothGraphics[Tooth.PriToPerm(tooth_id)].ShowPrimaryLetter) {
				//do not print string
			}
			else if(isSelected) {
				g.DrawString(displayNum,Font,new SolidBrush(TcData.ColorTextHighlight),rec.X,rec.Y);
			}
			else {
				g.DrawString(displayNum,Font,new SolidBrush(TcData.ColorText),rec.X,rec.Y);
			}
		}

		private void DrawDrawingSegments(Graphics g) {
			string[] pointStr;
			List<Point> points;
			Point point;
			string[] xy;
			Pen pen;
			for(int s=0;s<TcData.DrawingSegmentList.Count;s++) {
				pen=new Pen(TcData.DrawingSegmentList[s].ColorDraw,2f);
				pointStr=TcData.DrawingSegmentList[s].DrawingSegment.Split(';');
				points=new List<Point>();
				for(int p=0;p<pointStr.Length;p++) {
					xy=pointStr[p].Split(',');
					if(xy.Length==2) {
						point=new Point(int.Parse(xy[0]),int.Parse(xy[1]));
						points.Add(point);
					}
				}
				for(int i=1;i<points.Count;i++) {
					//if we set 0,0 to center, then this is where we would convert it back.
					g.DrawLine(pen,points[i-1].X,
						points[i-1].Y,
						points[i].X,
						points[i].Y);
				}
			}
		}

		#region Mouse And Selections

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			MouseIsDown=true;
			if(TcData.ListToothGraphics.Count==0){//still starting up?
				return;
			}
			if(TcData.CursorTool==CursorTool.Pointer){
				string toothClicked=TcData.GetToothAtPoint(e.Location);
				if(TcData.SelectedTeeth.Contains(toothClicked)) {
					SetSelected(toothClicked,false);
				}
				else {
					SetSelected(toothClicked,true);
				}
			}
			else if(TcData.CursorTool==CursorTool.Pen) {
				PointList.Add(new Point(e.X,e.Y));
			}
			else if(TcData.CursorTool==CursorTool.Eraser) {
				//do nothing
			}
			else if(TcData.CursorTool==CursorTool.ColorChanger) {
				//look for any lines near the "wand".
				//since the line segments are so short, it's sufficient to check end points.
				string[] xy;
				string[] pointStr;
				float x;
				float y;
				float dist;//the distance between the point being tested and the center of the eraser circle.
				float radius=2f;//by trial and error to achieve best feel.
				//PointF eraserPt=new PointF(e.X+8.49f,e.Y+8.49f);
				for(int i=0;i<TcData.DrawingSegmentList.Count;i++) {
					pointStr=TcData.DrawingSegmentList[i].DrawingSegment.Split(';');
					for(int p=0;p<pointStr.Length;p++){
						xy=pointStr[p].Split(',');
						if(xy.Length==2){
							x=float.Parse(xy[0]);
							y=float.Parse(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-e.X),2)+Math.Pow(Math.Abs(y-e.Y),2));
							if(dist<=radius){//testing circle intersection here
								OnSegmentDrawn(TcData.DrawingSegmentList[i].DrawingSegment);
								TcData.DrawingSegmentList[i].ColorDraw=TcData.ColorDrawing;
								Invalidate();
								return;;
							}
						}
					}
				}	
			}
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if(TcData.ListToothGraphics.Count==0) {
				return;
			}
			if(TcData.CursorTool==CursorTool.Pointer) {
				//if(drawMode==DrawingMode.Simple2D) {
				hotTooth=TcData.GetToothAtPoint(e.Location);
				if(hotTooth==hotToothOld) {//mouse has not moved to another tooth
					return;
				}
				hotToothOld=hotTooth;
				if(MouseIsDown) {//drag action
					if(TcData.SelectedTeeth.Contains(hotTooth)) {
						SetSelected(hotTooth,false);
					}
					else {
						SetSelected(hotTooth,true);
					}
				}
				//}
			}
			else if(TcData.CursorTool==CursorTool.Pen) {
				if(!MouseIsDown){
					return;
				}
				PointList.Add(new Point(e.X,e.Y));
				//just add the last line segment instead of redrawing the whole thing.
				Graphics g=this.CreateGraphics();
				g.SmoothingMode=SmoothingMode.HighQuality;
				Pen pen=new Pen(TcData.ColorDrawing,2f);
				int i=PointList.Count-1;
				g.DrawLine(pen,PointList[i-1].X,PointList[i-1].Y,PointList[i].X,PointList[i].Y);
				g.Dispose();
				//Invalidate();
			}
			else if(TcData.CursorTool==CursorTool.Eraser) {
				if(!MouseIsDown){
					return;
				}
				//look for any lines that intersect the "eraser".
				//since the line segments are so short, it's sufficient to check end points.
				string[] xy;
				string[] pointStr;
				float x;
				float y;
				float dist;//the distance between the point being tested and the center of the eraser circle.
				float radius=8f;//by trial and error to achieve best feel.
				PointF eraserPt=new PointF(e.X+8.49f,e.Y+8.49f);
				for(int i=0;i<TcData.DrawingSegmentList.Count;i++) {
					pointStr=TcData.DrawingSegmentList[i].DrawingSegment.Split(';');
					for(int p=0;p<pointStr.Length;p++){
						xy=pointStr[p].Split(',');
						if(xy.Length==2){
							x=float.Parse(xy[0]);
							y=float.Parse(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-eraserPt.X),2)+Math.Pow(Math.Abs(y-eraserPt.Y),2));
							if(dist<=radius){//testing circle intersection here
								OnSegmentDrawn(TcData.DrawingSegmentList[i].DrawingSegment);//triggers a deletion from db.
								TcData.DrawingSegmentList.RemoveAt(i);
								Invalidate();
								return;;
							}
						}
					}
				}	
			}
			else if(TcData.CursorTool==CursorTool.ColorChanger) {
				//do nothing
			}
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			MouseIsDown=false;
			if(TcData.CursorTool==CursorTool.Pen) {
				string drawingSegment="";
				for(int i=0;i<PointList.Count;i++) {
					if(i>0) {
						drawingSegment+=";";
					}
					//I could compensate to center point here:
					drawingSegment+=PointList[i].X+","+PointList[i].Y;
				}
				OnSegmentDrawn(drawingSegment);
				PointList=new List<Point>();
				//Invalidate();//?
			}
			else if(TcData.CursorTool==CursorTool.Eraser) {
				//do nothing
			}
			else if(TcData.CursorTool==CursorTool.ColorChanger) {
				//do nothing
			}
		}

		///<summary></summary>
		protected void OnSegmentDrawn(string drawingSegment) {
			/*
			ToothChartDrawEventArgs tArgs=new ToothChartDrawEventArgs(drawingSegment);
			if(SegmentDrawn!=null) {
				SegmentDrawn(this,tArgs);
			}*/
		}

		private void toothChart_SegmentDrawn(object sender,ToothChartDrawEventArgs e) {
			//OnSegmentDrawn(e.DrawingSegement);
		}

		///<summary>Used by mousedown and mouse move to set teeth selected or unselected.  A similar method is used externally in the wrapper to set teeth selected.  This private method might be faster since it only draws the changes.</summary>
		private void SetSelected(string tooth_id,bool setValue) {
			Graphics g=this.CreateGraphics();
			if(setValue) {
				TcData.SelectedTeeth.Add(tooth_id);
				DrawNumber(tooth_id,true,false,g);
			}
			else {
				TcData.SelectedTeeth.Remove(tooth_id);
				DrawNumber(tooth_id,false,false,g);
			}
			RectangleF recF=TcData.GetNumberRecPix(tooth_id,g);
			Rectangle rec=new Rectangle((int)recF.X,(int)recF.Y,(int)recF.Width,(int)recF.Height);
			Invalidate(rec);
			Application.DoEvents();
			/*
			if(TcData.ALSelectedTeeth.Count==0) {
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
			}*/
			g.Dispose();
		}

		#endregion Mouse And Selections




	}
}
