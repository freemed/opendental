//decided to use SheetFields for everything since all the variables are already there. It's more organized to just have one type of child object on sheets.
/*
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental{
	///<summary>The "background" elements of a sheet, such as images, lines, boxes, static text, etc.</summary>
	class SheetObject {
		///<summary>StaticText,Image,Line, or Box.</summary>
		public SheetObjectType ObjectType;
		///<Summary>In pixels.</Summary>
		public int XPos;
		///<Summary>In pixels.</Summary>
		public int YPos;
		///<Summary>The object will be constrained horizontally to this size.  Not allowed to be zero.</Summary>
		public int Width;
		///<Summary>The object will be constrained vertically to this size.  Not allowed to be 0.  The Sheet constructor makes sure that if this is 0, then it will default to the size dictated by the font.  Once we build a sheet designer, the designer will handle the default size.  So it's not allowed to be zero so that it will be visible on the designer.</Summary>
		public int Height;
		///<Summary></Summary>
		public GrowthBehaviorEnum GrowthBehavior;
		///<Summary></Summary>
		public string TextValue;

		///<Summary>Before printing, this will be the same as YPos.  But during printing, YPos will get changed with each sheet due to growthBehavior.  YPosOriginal allows us to return YPos to it's original setting.</Summary>
		private int yPosOriginal;
		///<Summary></Summary>
		private int heightOriginal;


	}



	public enum SheetObjectType {
		///<Summary></Summary>
		StaticText,
		///<Summary>Not yet supported</Summary>
		Image,
		///<Summary>Not yet supported</Summary>
		Line,
		///<Summary>Not yet supported.  Will probably implement a border around fields first as the preferred way of drawing a box.</Summary>
		Box
	}
}
*/