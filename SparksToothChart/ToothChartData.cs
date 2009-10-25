using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

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
		///<summary>Valid values are 1-32 and A-Z.</summary>
		public List<string> SelectedTeeth;
		public List<ToothInitial> DrawingSegmentList;
		///<summary>This is the size of the control in screen pixels.</summary>
		private Size sizeControl;
		/// <summary>In tooth mm, exactly how much of the projection to show.</summary>
		public SizeF SizeOriginalProjection=new SizeF(130f,97.34f);
		///<summary>Ratio of pix/mm.  Gets recalculated every time SizeControl changes due to wrapper resize.  Multiply this ratio times a tooth mm measurement to get a pixel equivalent. If starting with a pixel coordinate, then divide it by this ratio to get mm.</summary>
		public float ScaleMmToPix;
		///<summary>Whenever the control is resized, this value is set.  If the control ratio is wider than the 3D chart ratio, then this is true.  There would be extra background space on the sides.  If the ratio is taller than the 3D chart, then extra background on the top and bottom.  Default is (barely) false.</summary>
		public bool IsWide;
		///<summary>This defines a rectangle within the control where the tooth chart it to be drawn.  It will be different than the SizeControl if the control is wider or taller than the projection ratio.  This is set every time the control is resized.  It's in pixels.</summary>
		public Rectangle RectTarget;
		/// <summary>When the drawing feature was originally added, this was the size of the tooth chart. This number must forever be preserved and drawings scaled to account for it.</summary>
		public Size SizeOriginalDrawing=new Size(410,307);//NEVER CHANGE

		public ToothChartData() {
			ListToothGraphics=new ToothGraphicCollection();
			ColorBackground=Color.FromArgb(150,145,152);//defaults to gray
			ColorText=Color.White;
			ColorTextHighlight=Color.Red;
			ColorBackHighlight=Color.White;
			SelectedTeeth=new List<string>();
			sizeControl=SizeOriginalDrawing;
			DrawingSegmentList=new List<ToothInitial>();
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
			}
		}

		///<summary>Gets the rectangle surrounding a tooth number.  Used to draw the box and the number inside it.</summary>
		public RectangleF GetNumberRecMm(string tooth_id,string displayNum,float strWidthMm) {
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
			RectangleF recMm=new RectangleF(xPos-2f*toMm,yPos-2f*toMm,strWidthMm+3f*toMm,12f*toMm);//this rec has origin at LL
			return recMm;
		}

		///<summary>Used by 2D tooth chart to get the rectangle in pixels surrounding a tooth number.  Used to draw the box and the number inside it.</summary>
		public RectangleF GetNumberRecPix(string tooth_id,Graphics g,int widthControl,int heightControl,Font font) {
			float xPos=GetTransXpix(tooth_id,widthControl);
			float yPos=heightControl/2f;
			if(ToothGraphic.IsMaxillary(tooth_id)) {
				yPos-=14;
			}
			else {
				yPos+=3;
			}
			string displayNum =tooth_id;
			//displayNum =OpenDentBusiness.Tooth.GetToothLabel(tooth_id);
			float strWidth=g.MeasureString(displayNum,font).Width;
			xPos-=strWidth/2f;
			RectangleF rec=new RectangleF(xPos-1,yPos-1,strWidth,12);//this rec has origin at UL
			return rec;
		}

		///<summary>Pri or perm tooth numbers are valid.  Only locations of perm teeth are stored.  This also converts mm to screen pixels.  This is currently only used in 2D mode, but it might be useful later in the others.</summary>
		public float GetTransXpix(string tooth_id,int widthControl) {
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

		///<summary>This also adjusts the result up or down along the Y axis to account for a control that is not the same proportion as the original.</summary>
		public PointF PixToMm(Point pixPoint,int widthControl,int heightControl) {
			/*
			float toMmRatio=(float)WidthProjection/(float)widthControl;//mm/pix
			float mmX=(((float)pixPoint.X)*toMmRatio)-((float)WidthProjection)/2f;
			float idealHeightProjection=(float)WidthProjection*(float)SizeOriginalDrawing.Height/(float)SizeOriginalDrawing.Width;
			float actualHeightProjection=(float)WidthProjection*(float)heightControl/(float)widthControl;
			float mmY=(idealHeightProjection)/2f-(((float)pixPoint.Y)*toMmRatio);
			return new PointF(mmX,mmY);*/
			return new PointF(0,0);
		}

		///<summary>First, use GetNumberRecMm to get the rectangle surrounding a tooth num.  The, use this to convert it to control coords.</summary>
		public Rectangle ConvertRecToPix(RectangleF recMm) {
			//float toMm=(float)WidthProjection/(float)widthControl;//mm/pix
			//this.ScaleMmToPix
			//Rectangle recPix=new Rectangle((int)(widthControl/2+recMm.X/toMm),(int)(heightControl/2-recMm.Y/toMm-recMm.Height/toMm),
			//	(int)(recMm.Width/toMm),(int)(recMm.Height/toMm));
			Rectangle recPix=new Rectangle(
				(int)(sizeControl.Width/2+recMm.X*ScaleMmToPix),
				(int)(sizeControl.Height/2-recMm.Y*ScaleMmToPix-recMm.Height*ScaleMmToPix),
				(int)(recMm.Width*ScaleMmToPix),
				(int)(recMm.Height*ScaleMmToPix)
				);
			return recPix;
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
		public string GetToothAtPoint(int x,int y) {
			//This version was originally in OpenGL
			/*float closestDelta=(float)(WidthProjection*2);//start it off really big
			int closestTooth=1;
			float toothPos=0;
			float delta=0;
			float xPos=(float)((float)(x-Width/2)*WidthProjection/(float)Width);//in mm instead of screen coordinates
			if(y<Height/2) {//max
				for(int i=1;i<=16;i++) {
					if(TcData.ListToothGraphics[i.ToString()].HideNumber) {
						continue;
					}
					toothPos=ToothGraphic.GetDefaultOrthoXpos(i);
					if(ToothGraphic.IsRight(i.ToString())) {
						toothPos+=(int)TcData.ListToothGraphics[i.ToString()].ShiftM;//*(float)Width/WidthProjection);
					}
					else {
						toothPos-=(int)TcData.ListToothGraphics[i.ToString()].ShiftM;//*(float)Width/WidthProjection);
					}
					if(xPos>toothPos) {
						delta=xPos-toothPos;
					}
					else {
						delta=toothPos-xPos;
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
					if(TcData.ListToothGraphics[i.ToString()].HideNumber) {
						continue;
					}
					toothPos=ToothGraphic.GetDefaultOrthoXpos(i);//in mm.
					if(ToothGraphic.IsRight(i.ToString())) {
						toothPos+=(int)TcData.ListToothGraphics[i.ToString()].ShiftM;
					}
					else {
						toothPos-=(int)TcData.ListToothGraphics[i.ToString()].ShiftM;
					}
					if(xPos>toothPos) {
						delta=xPos-toothPos;
					}
					else {
						delta=toothPos-xPos;
					}
					if(delta<closestDelta) {
						closestDelta=delta;
						closestTooth=i;
					}
				}
				return closestTooth;
			}*/
			return "1";
		}

	}
}
