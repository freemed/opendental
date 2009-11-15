using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenDentBusiness;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace SparksToothChart {
	///<summary>This is an object full of data about how to draw the 3D graphical teeth.  It also contains a number of helper functions that need to be shared between the different tooth charts.</summary>
	public class ToothChartData {
		///<summary>A strongly typed collection of ToothGraphics.  This includes all 32 perm and all 20 primary teeth, whether they will be drawn or not.  If a tooth is missing, it gets marked as visible false.  If it's set to primary, then the permanent tooth gets repositioned under the primary, and a primary gets set to visible true.  If a tooth is impacted, it gets repositioned.  Supernumerary graphics are not yet supported, but they might be handled by adding to this list.  "implant" is also stored as another tooth in this collection.  It is just used to store the graphics for any implant.</summary>
		public ToothGraphicCollection ListToothGraphics;
		///<summary>The main color of the background behind the teeth.</summary>
		public Color ColorBackground;
		///<summary>The normal color of the text for tooth numbers and letters.</summary>
		public Color ColorText;
		///<summary>The color of text when a tooth number is highlighted.</summary>
		public Color ColorTextHighlight;
		///<summary>The color of the background highlight rectangle around a selected tooth number.</summary>
		public Color ColorBackHighlight;
		private List<string> selectedTeeth;
		public List<ToothInitial> DrawingSegmentList;
		///<summary>This is the size of the control in screen pixels.</summary>
		private Size sizeControl;
		/// <summary>In tooth mm, exactly how much of the projection to show.</summary>
		public SizeF SizeOriginalProjection=new SizeF(130f,97.34f);
		///<summary>Ratio of pix/mm.  Gets recalculated every time SizeControl changes due to wrapper resize.  Multiply this ratio times a tooth mm measurement to get a pixel equivalent. If starting with a pixel coordinate, then divide it by this ratio to get mm.</summary>
		public float ScaleMmToPix;
		///<summary>Whenever the control is resized, this value is set.  If the control ratio is wider than the 3D chart ratio, then this is true.  There would be extra background space on the sides.  If the ratio is taller than the 3D chart, then extra background on the top and bottom.  Default is (barely) false.</summary>
		public bool IsWide;
		///<summary>This defines a rectangle within the control where the tooth chart is to be drawn.  It will be different than the SizeControl if the control is wider or taller than the projection ratio.  This is set every time the control is resized.  It's in pixels.</summary>
		public Rectangle RectTarget;
		/// <summary>When the drawing feature was originally added, this was the size of the tooth chart. This number must forever be preserved and drawings scaled to account for it.</summary>
		public Size SizeOriginalDrawing=new Size(410,307);//NEVER CHANGE
		///<summary>An enum that indicates which kind of cursor is currently being used.</summary>
		public CursorTool CursorTool;
		///<summary>The color being used for freehand drawing.</summary>
		public Color ColorDrawing;
		///<summary>This font object is used to measure strings.</summary>
		public System.Drawing.Font Font;
		///<summary>A list of points for a line currently being drawn.  Once the mouse is raised, this list gets cleared.</summary>
		public List<Point> PointList;
		///<summary>The size of the current drawing in pixels / the size of the original drawing.  This number is used to scale original drawing to the new size.</summary>
		public float PixelScaleRatio;

		public ToothChartData() {
			ListToothGraphics=new ToothGraphicCollection();
			ColorBackground=Color.FromArgb(150,145,152);//defaults to gray
			ColorText=Color.White;
			ColorTextHighlight=Color.Red;
			ColorBackHighlight=Color.White;
			selectedTeeth=new List<string>();
			sizeControl=SizeOriginalDrawing;
			DrawingSegmentList=new List<ToothInitial>();
			CursorTool=CursorTool.Pointer;
			ColorDrawing=Color.Black;
			Font=new System.Drawing.Font(FontFamily.GenericSansSerif,8.25f);
			PointList=new List<Point>();
		}

		///<summary></summary>
		public ToothChartData Copy() {
			ToothChartData data=new ToothChartData();
			data.ListToothGraphics=this.ListToothGraphics.Copy();
			data.ColorBackground=this.ColorBackground;
			data.ColorText=this.ColorText;
			data.ColorTextHighlight=this.ColorTextHighlight;
			data.ColorBackHighlight=this.ColorBackHighlight;
			data.DrawingSegmentList=this.DrawingSegmentList;
			//some values set when control resized.
			data.Font=this.Font;
			return data;
		}

		///<summary>This gets set whenever the wrapper resizes.  It's the size of the control in screen pixels.</summary>
		public Size SizeControl {
			get {
				return sizeControl;
			}
			set {
				sizeControl=value;
				//compute scaleMmToPix
				if(SizeOriginalProjection.Width/(float)sizeControl.Width < SizeOriginalProjection.Height/(float)sizeControl.Height) { 
					//if wide, use control h.  The default settings will just barely make this false.
					IsWide=true;
					ScaleMmToPix=(float)sizeControl.Height/SizeOriginalProjection.Height;
					RectTarget.Height=sizeControl.Height;
					RectTarget.Y=0;
					RectTarget.Width=(int)(((float)SizeOriginalDrawing.Width/SizeOriginalDrawing.Height)*RectTarget.Height);
					RectTarget.X=(sizeControl.Width-RectTarget.Width)/2;
				}
				else {//otherwise, use control w
					IsWide=false;
					ScaleMmToPix=(float)sizeControl.Width/SizeOriginalProjection.Width;
					RectTarget.Width=sizeControl.Width;
					RectTarget.X=0;
					RectTarget.Height=(int)(((float)SizeOriginalDrawing.Height/SizeOriginalDrawing.Width)*RectTarget.Width);
					RectTarget.Y=(sizeControl.Height-RectTarget.Height)/2;
				}
				PixelScaleRatio=(float)RectTarget.Width/(float)SizeOriginalDrawing.Width;
			}
		}

		///<summary>Valid values are 1-32 and A-Z.  To set which teeth are selected, use SetSelected().</summary>
		public List<string> SelectedTeeth {
			get {
				return selectedTeeth;
			}
		}

		///<summary>Gets the rectangle surrounding a tooth number.  Used to draw the box and the number inside it.  Includes any mesial shifts.</summary>
		public RectangleF GetNumberRecMm(string tooth_id,Graphics g){
			float xPos=0;
			float yPos=0;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				if(Tooth.IsPrimary(tooth_id)) {
					yPos+=5.1f;
				}
				else {
					yPos+=1.3f;
				}
			}
			else {
				if(Tooth.IsPrimary(tooth_id)) {
					yPos-=7.6f;
				}
				else {
					yPos-=3.8f;
				}
			}
			xPos+=GetTransX(tooth_id);
			//fix this.
			//string displayNum=OpenDentBusiness.Tooth.GetToothLabelGraphic(tooth_id);
			//string displayNum=tooth_id;
			//float strWidth=MeasureStringMm(displayNum);
			string displayNum=tooth_id;
			float strWidthMm=g.MeasureString(displayNum,Font).Width/ScaleMmToPix;
			xPos-=strWidthMm/2f;
			//only use the ShiftM portion of the user translation
			if(ToothGraphic.IsRight(tooth_id)) {
				xPos+=ListToothGraphics[tooth_id].ShiftM;
			}
			else {
				xPos-=ListToothGraphics[tooth_id].ShiftM;
			}
			//float toMm=(float)WidthProjection/(float)widthControl;//mm/pix
			float toMm=1f/ScaleMmToPix;
			RectangleF recMm=new RectangleF(xPos-0f*toMm,yPos-2f*toMm,strWidthMm-1f*toMm,12f*toMm);//this rec has origin at LL
			return recMm;
		}

		///<summary>Used by 2D tooth chart to get the rectangle in pixels surrounding a tooth number.  Used to draw the box and the number inside it.</summary>
		public Rectangle GetNumberRecPix(string tooth_id,Graphics g) {
			return ConvertRecToPix(GetNumberRecMm(tooth_id,g));
			/*
			float xPos=GetTransXpix(tooth_id);
			float yPos=sizeControl.Height/2f;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				if(Tooth.IsPrimary(tooth_id)) {
					yPos-=25;
				}
				else {
					yPos-=14;
				}
			}
			else {
				if(Tooth.IsPrimary(tooth_id)) {
					yPos+=14;
				}
				else {
					yPos+=3;
				}
			}
			string displayNum =tooth_id;
			//displayNum =OpenDentBusiness.Tooth.GetToothLabel(tooth_id);
			float strWidth=g.MeasureString(displayNum,Font).Width;
			xPos-=strWidth/2f;
			RectangleF rec=new RectangleF(xPos-1,yPos-1,strWidth,12);//this rec has origin at UL
			return rec;*/
		}

		///<summary>Pri or perm tooth numbers are valid.  Only locations of perm teeth are stored.  This also converts mm to screen pixels.  This is currently only used in 2D mode.</summary>
		public float GetTransXpix(string tooth_id) {
			int toothInt=ToothGraphic.IdToInt(tooth_id);
			if(toothInt==-1) {
				throw new ApplicationException("Invalid tooth number: "+tooth_id);//only for debugging
			}
			float xmm=ToothGraphic.GetDefaultOrthoXpos(toothInt);//in +/- mm from center
			return (sizeControl.Width/2)+(xmm*ScaleMmToPix);
				//( SizeOriginalProjection.Width/2f+xmm)*ScaleMmToPix;//*widthControl/WidthProjection;
		}

		///<summary>In control coords rather than mm.  This is not really meaninful except in 2D mode.  It calculates the location of the facial view in order to draw the big red X.</summary>
		public float GetTransYfacialPix(string tooth_id) {
			float basic=30;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				return sizeControl.Height/2-basic;
			}
			return sizeControl.Height/2+basic;
		}

		///<summary>In control coords rather than mm.  This is not really meaninful except in 2D mode.  It calculates the location of the occlusal surface in order to draw the bullseye.</summary>
		public float GetTransYocclusalPix(string tooth_id,int heightControl) {
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				return heightControl/2-48f;
			}
			return heightControl/2+48f;
		}

		///<summary>Pri or perm tooth numbers are valid.  Only locations of perm teeth are stored.</summary>
		public float GetTransX(string tooth_id) {
			int toothInt=ToothGraphic.IdToInt(tooth_id);
			if(toothInt==-1) {
				throw new ApplicationException("Invalid tooth number: "+tooth_id);//only for debugging
			}
			return ToothGraphic.GetDefaultOrthoXpos(toothInt);
		}

		public float GetTransYfacial(string tooth_id) {
			float basic=29f;
			if(tooth_id=="6" || tooth_id=="11") {
				return basic+1f;
			}
			if(tooth_id=="7" || tooth_id=="10") {
				return basic+1f;
			}
			else if(tooth_id=="8" || tooth_id=="9") {
				return basic+2f;
			}
			else if(tooth_id=="22" || tooth_id=="27") {
				return -basic-2f;
			}
			else if(tooth_id=="23" || tooth_id=="24" || tooth_id=="25" || tooth_id=="26") {
				return -basic-2f;
			}
			else if(ToothGraphic.IsMaxillary(tooth_id)) {
				return basic;
			}
			return -basic;
		}

		public float GetTransYocclusal(string tooth_id) {
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				return 13f;
			}
			return -13f;
		}

		///<summary>This also adjusts the result to account for a control that is not the same proportion as the original.  Result could be outside the projection area.</summary>
		public PointF PointPixToMm(Point pixPoint) {
			/*
			float toMmRatio=(float)WidthProjection/(float)widthControl;//mm/pix
			float mmX=(((float)pixPoint.X)*toMmRatio)-((float)WidthProjection)/2f;
			float idealHeightProjection=(float)WidthProjection*(float)SizeOriginalDrawing.Height/(float)SizeOriginalDrawing.Width;
			float actualHeightProjection=(float)WidthProjection*(float)heightControl/(float)widthControl;
			float mmY=(idealHeightProjection)/2f-(((float)pixPoint.Y)*toMmRatio);
			return new PointF(mmX,mmY);*/
			float toMmRatio=1f/ScaleMmToPix;
			float mmX=(((float)(pixPoint.X-RectTarget.X))*toMmRatio)-((float)SizeOriginalProjection.Width)/2f;
			//float idealHeightProjection=(float)WidthProjection*(float)SizeOriginalDrawing.Height/(float)SizeOriginalDrawing.Width;
			//float actualHeightProjection=(float)WidthProjection*(float)heightControl/(float)widthControl;
			float mmY=(SizeOriginalProjection.Height)/2f-(((float)(pixPoint.Y-RectTarget.Y))*toMmRatio);
			return new PointF(mmX,mmY);
		}

		/// <summary>Takes an original db point as originally entered in unscaled control coordinates, and returns coordinates in scene mm's.</summary>
		public PointF PointDrawingPixToMm(Point pixPoint) {
			float toMmRatio=1f/ScaleMmToPix;
			float mmX=(((float)pixPoint.X*PixelScaleRatio)*toMmRatio)-((float)SizeOriginalProjection.Width)/2f;
			float mmY=((float)SizeOriginalProjection.Height)/2f-(((float)pixPoint.Y*PixelScaleRatio)*toMmRatio);
			return new PointF(mmX,mmY);
		}

		/*
		///<summary>The recPix has origin at UL.  The result has origin at LL.</summary>
		private RectangleF ConvertRecToMm(RectangleF recPix) {
			float w=recPix.Width/ScaleMmToPix;
			float h=recPix.Height/ScaleMmToPix;
			float x=(recPix.X-(float)(sizeControl.Width/2))/ScaleMmToPix;
			float y=(recPix.Bottom-(float)(sizeControl.Height/2))/ScaleMmToPix;
			return new RectangleF(x,y,w,h);			
		}*/


		///<summary>The recMm has origin at LL.  The result has origin at UL and is in control coords.</summary>
		private Rectangle ConvertRecToPix(RectangleF recMm) {
			int w=(int)(recMm.Width*ScaleMmToPix)+1;
			int h=(int)(recMm.Height*ScaleMmToPix);
			int x=(int)(recMm.X*ScaleMmToPix+sizeControl.Width/2);
			int y=(int)((sizeControl.Height/2-recMm.Y*ScaleMmToPix)-h);
			return new Rectangle(x,y,w,h);		
		}

		/*
		///<summary>Always returns a number between 1 and 32.  This isn't perfect, since it only operates on perm teeth, and assumes that any primary tooth will be at the same x pos as its perm tooth.</summary>
		public int GetToothAtPoint(int x,int y) {
			//This version was originally used in 2D chart
			
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
			return 1;
		}*/


		///<summary>Input is pixel coordinates of control.  Always returns a string, 1 through 32 or A through T.  Primary letters are only returned if that tooth is set as primary.</summary>
		public string GetToothAtPoint(Point point) {
			//This version was originally in OpenGL.
			float closestDelta=(float)(SizeOriginalProjection.Width*2);//start it off really big
			string closestTooth="1";
			float toothPos=0;
			float delta=0;
			//convert x and y to mm.  Use those measurements to match the closest tooth.
			//float xPos=(float)((float)(x-Width/2)*WidthProjection/(float)Width);//in mm instead of screen coordinates
			float xPos=PointPixToMm(point).X;
			float yPos=PointPixToMm(point).Y;
			string perm_id;
			bool isPriArea;//this point is where a primary letter might sometimes show
			bool priShowing;
			bool permShowing;
			bool usePri;//otherwise, use perm
			string tooth_id;
			if(yPos>0) {//maxillary
				for(int i=1;i<=16;i++) {
					perm_id=i.ToString();
					if(i>=4 && i<=13){
						if(yPos>5.1f){
							isPriArea=true;
						}
						else{
							isPriArea=false;
						}
					}
					else{
						isPriArea=false;
					}
					//Determine which numbers are showing
					priShowing=false;
					if(ListToothGraphics[perm_id].ShowPrimaryLetter
						&& !ListToothGraphics[Tooth.PermToPri(perm_id)].HideNumber)
					{
						priShowing=true;
					}
					permShowing=true;
					if(ListToothGraphics[perm_id].HideNumber) {
						permShowing=false;
					}
					if(!priShowing && !permShowing) {//if neither # showing
						continue;
					}
					if(priShowing) {
						if(permShowing) {//both showing
							if(isPriArea) {
								usePri=true;
							}
							else {
								usePri=false;
							}
						}
						else {
							usePri=true;
						}
					}
					else {//only perm showing
						usePri=false;
					}
					if(usePri) {
						tooth_id=Tooth.PermToPri(perm_id);
					}
					else {
						tooth_id=perm_id;
					}
					toothPos=ToothGraphic.GetDefaultOrthoXpos(i);
					if(ToothGraphic.IsRight(perm_id)) {
						toothPos+=(int)ListToothGraphics[tooth_id].ShiftM;
					}
					else {
						toothPos-=(int)ListToothGraphics[tooth_id].ShiftM;
					}
					if(xPos>toothPos) {
						delta=xPos-toothPos;
					}
					else {
						delta=toothPos-xPos;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=tooth_id;
					}
				}
				return closestTooth;
			}
			else {//mandibular
				for(int i=17;i<=32;i++) {
					perm_id=i.ToString();
					if(i>=20 && i<=29) {
						if(yPos<-4.4f) {
							isPriArea=true;
						}
						else {
							isPriArea=false;
						}
					}
					else {
						isPriArea=false;
					}
					//Determine which numbers are showing
					priShowing=false;
					if(ListToothGraphics[perm_id].ShowPrimaryLetter
						&& !ListToothGraphics[Tooth.PermToPri(perm_id)].HideNumber) {
						priShowing=true;
					}
					permShowing=true;
					if(ListToothGraphics[perm_id].HideNumber) {
						permShowing=false;
					}
					if(!priShowing && !permShowing) {//if neither # showing
						continue;
					}
					if(priShowing) {
						if(permShowing) {//both showing
							if(isPriArea) {
								usePri=true;
							}
							else {
								usePri=false;
							}
						}
						else {
							usePri=true;
						}
					}
					else {//only perm showing
						usePri=false;
					}
					if(usePri) {
						tooth_id=Tooth.PermToPri(perm_id);
					}
					else {
						tooth_id=perm_id;
					}
					toothPos=ToothGraphic.GetDefaultOrthoXpos(i);
					if(ToothGraphic.IsRight(perm_id)) {
						toothPos+=(int)ListToothGraphics[tooth_id].ShiftM;
					}
					else {
						toothPos-=(int)ListToothGraphics[tooth_id].ShiftM;
					}
					if(xPos>toothPos) {
						delta=xPos-toothPos;
					}
					else {
						delta=toothPos-xPos;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=tooth_id;
					}
				}
				return closestTooth;
			}
		}

		///<summary>When this is used from within a toothchart in response to mouse activity, it is typically followed by explicit drawing instructions that efficiently show the user which teeth are selected.  When this is used from the wrapper, it's typically followed by an Invalidate().</summary>
		public void SetSelected(string tooth_id,bool setValue) {
			if(setValue) {
				if(!SelectedTeeth.Contains(tooth_id)) {
					SelectedTeeth.Add(tooth_id);
				}
				//DrawNumber(tooth_id,true,false);
			}
			else {
				if(SelectedTeeth.Contains(tooth_id)) {
					SelectedTeeth.Remove(tooth_id);
				}
				//DrawNumber(tooth_id,false,false);
			}
			//RectangleF recMm=TcData.GetNumberRecMm(tooth_id,);
			//Rectangle rec=TcData.ConvertRecToPix(recMm);
		}

	}
}
