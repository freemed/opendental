using System;
using System.Collections.Generic;
using System.Text;

namespace SparksToothChart {
	///<summary>A face is a single polygon, usually a rectangle.  It contains a list of VertexNormals.</summary>
	public class Face {
		public List<VertexNormal> VertexNormals;

		public Face() {
			VertexNormals=new List<VertexNormal>();
		}
	}
}
