using System;
using System.Collections.Generic;
using System.Text;

namespace SparksToothChart {
	///<summary>A face is a single polygon, usually a rectangle.</summary>
	public class Face {
		//public List<VertexNormal> VertexNormals;
		///<summary>A list of indices to the VertexNormal list contained in the ToothGraphic object.</summary>
		public List<int> IndexList;

		public Face() {
			//VertexNormals=new List<VertexNormal>();
			IndexList=new List<int>();
		}
	}
}
